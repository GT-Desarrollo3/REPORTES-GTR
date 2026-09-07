
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 30-07-2024
-- Description:	GENERAR TARIFAS PARA OT
-- =============================================
/*
exec ReportesApp_Operaciones_ControlTarifas_InsertarTarifas @xmlTarifas=N'<r>  <d CodRuta="000236" Ruta="CAJAMARCA -  TRUJILLO " CodProducto="573" Producto="PTER /ENVASES/CAJAS/PARIHUELAS" Tarifa="1510.95" Operacion="LINDLEY" CodCliente="20323" Cliente="AC LOGISTICA DEL PERU S.A.C" Recorrido="Ida y Vuelta" Peso="Normal" Calculo="Fijo" />  <d CodRuta="000410" Ruta="CHICLAYO - TRUJILLO" CodProducto="573" Producto="PTER /ENVASES/CAJAS/PARIHUELAS" Tarifa="950" Operacion="LINDLEY" CodCliente="20323" Cliente="AC LOGISTICA DEL PERU S.A.C" Recorrido="Ida y Vuelta" Peso="Normal" Calculo="Fijo" />  <d CodRuta="000251" Ruta="CALLAO - CALLAO - CALLAO" CodProducto="782" Producto="GAS LICUADO DE PETROLEO" Tarifa="21" Operacion="LIMAGAS" CodCliente="15328" Cliente="LIMA GAS S A" Recorrido="Ida" Peso="Cliente" Calculo="Variable" />  <d CodRuta="000924" Ruta="CALLAO - CHICLAYO - CALLAO" CodProducto="782" Producto="GAS LICUADO DE PETROLEO" Tarifa="279.01" Operacion="LIMAGAS" CodCliente="15328" Cliente="LIMA GAS S A" Recorrido="Ida" Peso="Normal" Calculo="Fijo" /></r>',@Usuario=N'GREYES'
*/
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ControlTarifas_InsertarTarifas]
@xmlTarifas VARCHAR(MAX),
@Moneda VARCHAR(20),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @idoc INT
DECLARE @TEMP_TARIFAS TABLE(
		Nro INT,
		CodRuta VARCHAR(6),
		CodProducto VARCHAR(6),
		Tarifa DECIMAL(10,2),
		Operacion VARCHAR(20),
		CodCliente INT,
		Recorrido VARCHAR(30),
		Peso VARCHAR(30),
		Calculo VARCHAR(30))

IF(@xmlTarifas IS NOT NULL) BEGIN
	EXEC sp_xml_preparedocument @idoc OUTPUT, @xmlTarifas
	INSERT INTO @TEMP_TARIFAS(Nro, CodRuta, CodProducto, Tarifa, Operacion, CodCliente, Recorrido, Peso, Calculo)
	SELECT ROW_NUMBER() OVER(ORDER BY CodRuta ASC), * FROM OPENXML(@idoc,'/r/d',1)
	WITH (CodRuta VARCHAR(6), CodProducto VARCHAR(6), Tarifa DECIMAL(10,2), Operacion VARCHAR(20), CodCliente INT, Recorrido VARCHAR(30),
	Peso VARCHAR(30), Calculo VARCHAR(30));
	EXEC sp_xml_removedocument @idoc;
END

BEGIN TRAN
BEGIN TRY
	DECLARE @Contador INT
	SET @Contador = 1

	WHILE (@Contador <= (SELECT COUNT(Nro) FROM @TEMP_TARIFAS)) BEGIN
		DECLARE @Persona INT = (SELECT CodCliente FROM @TEMP_TARIFAS WHERE Nro = @Contador)
		DECLARE @CodProducto VARCHAR(6) = (SELECT RIGHT('000000'+LTRIM(RTRIM(CodProducto)),6) FROM @TEMP_TARIFAS WHERE Nro = @Contador)
		DECLARE @UMBase VARCHAR(3) = (SELECT UMBase FROM OP_AL_PRODUCTO WHERE RTRIM(Codigo) = @CodProducto)
		DECLARE @Operacion VARCHAR(20) = (SELECT Operacion FROM @TEMP_TARIFAS WHERE Nro = @Contador)
		DECLARE @CodRuta VARCHAR(6) = (SELECT CodRuta FROM @TEMP_TARIFAS WHERE Nro = @Contador)
		DECLARE @IdRuta INT = (SELECT IdRuta FROM OP_TR_Ruta WHERE Codigo = LTRIM(RTRIM(@CodRuta)))
		DECLARE @TipoMoneda VARCHAR(2)
		DECLARE @Cantidad DECIMAL(16,6) = (SELECT CASE WHEN @Operacion = 'LINDLEY' THEN 95000.00 WHEN @Operacion = 'LIMAGAS' THEN 450000.00
		WHEN @Operacion = 'VOLCAN' THEN 100000.00 END)
		
		-- OBTENER ÚLTIMO CONTRATO VIGENTE DE CLIENTE
		DECLARE @IDContrato INT = (SELECT TOP(1) IdContrato FROM OP_GE_CONTRATO WHERE IdClienteFacturacion = @Persona AND Estado = 1
								   ORDER BY FechaFinValidez DESC)
		DECLARE @FechaInicio DATETIME = (SELECT TOP(1) FechaInicioValidez FROM OP_GE_CONTRATO WHERE IdContrato = @IDContrato)
		DECLARE @IDVendedor INT = (SELECT TOP(1) IdVendedor FROM OP_GE_CONTRATO WHERE IdContrato = @IDContrato)
		DECLARE @IDPeso INT = (SELECT CASE WHEN Peso = 'Normal' THEN 1 WHEN Peso = 'Cliente' THEN 2 END FROM @TEMP_TARIFAS WHERE Nro = @Contador)
		
		-- INGRESAR PRODUCTO EN CONTRATO
		DECLARE @Linea1 INT
		SET @Linea1 = (SELECT MAX(Linea) FROM OP_GE_ContratoProducto WHERE IdContrato = @IDContrato)
		SET @Linea1 = ISNULL(@Linea1,0) + 1

		IF (@Operacion = 'LINDLEY' OR @Operacion = 'VOLCAN') BEGIN
			INSERT INTO OP_GE_ContratoProducto (IdContrato, Linea, Producto, IndCosto, UMBase, CantidadBase, FactorUsoBase, UMUso, CantidadUso, TipoMovimiento,
			IndServAlmacen, IndServTransporte, IndTercero, IdRuta, Estado, FechaInicio, UsuarioCreacion, FechaCreacion, UsuarioModificacion, FechaModificacion, IndOrigenPeso)
			VALUES (@IDContrato,@Linea1, @CodProducto, 2, @UMBase, @Cantidad, 1.00, @UMBase, @Cantidad, 'TRA', 1, 2, 'P', @IdRuta, 2, @FechaInicio, @Usuario, GETDATE(), 
			@Usuario, GETDATE(), @IDPeso)
		END
	
		IF (@Operacion = 'LIMAGAS') BEGIN
			INSERT INTO OP_GE_ContratoProducto (IdContrato, Linea, Producto, IndCosto, UMBase, CantidadBase, FactorUsoBase, UMUso, CantidadUso, TipoMovimiento,
			IndServAlmacen, IndServTransporte, IndTercero, IdRuta, Estado, FechaInicio, UsuarioCreacion, FechaCreacion, UsuarioModificacion, FechaModificacion, IndOrigenPeso)
			VALUES (@IDContrato, @Linea1, @CodProducto, 2, @UMBase, @Cantidad, 1.00, @UMBase, @Cantidad, 'TRA', 1, 2, 'P', @IdRuta, 2, @FechaInicio, @Usuario, GETDATE(),
			@Usuario, GETDATE(), @IDPeso)
		END

		-- GENERAR OT
		DECLARE @FechaFin DATETIME = (SELECT TOP(1) FechaFinValidez FROM OP_GE_CONTRATO WHERE IdContrato = @IDContrato)
		DECLARE @IdOT INT
		SET @IdOT = (SELECT MAX(IdOT) FROM OP_GE_OT)
		SET @IdOT = ISNULL(@IdOT,0) + 1

		IF (@Operacion = 'LINDLEY' OR @Operacion = 'VOLCAN') BEGIN
			INSERT INTO OP_GE_OT (IdOT, TipoOT, Codigo, IdContrato, IdVendedor, IdClienteFacturacion, TipoMovimiento, PeriodoEmision, FechaEmision, FechaProceso,
			Estado, EstadoAnterior, UsuarioCreacion, FechaCreacion, UsuarioModificacion, FechaModificacion, FechaFinValidez)
			VALUES (@IdOT, 'T1', CONVERT(VARCHAR(6),RIGHT('000000'+LTRIM(RTRIM(@IdOT)),6)), @IDContrato, @IDVendedor, @Persona, 'TRA', CONVERT(CHAR(4),YEAR(GETDATE()))+
			CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(GETDATE()))),2)), GETDATE(), GETDATE(), 1, 0, @Usuario, GETDATE(), @Usuario, GETDATE(), @FechaFin)

			INSERT INTO OP_GE_OTProducto (IdOT, Linea, IdContrato, LineaContrato, Producto, IndCosto, UMBase, CantidadBase, FactorUsoBase, UMUso, CantidadUso, TipoMovimiento,
			IndServAlmacen, IndServTransporte, IndTercero, IdRuta, FechaInicio, FechaFin, Estado, UsuarioCreacion, FechaCreacion, UsuarioModificacion, FechaModificacion,
			CantBasePendAlm, CantBasePendTrans, CantUsoPendAlm, CantUsoPendTrans, IndOrigenPeso)
			VALUES (@IdOT, 1, @IDContrato, @Linea1, @CodProducto, 2, @UMBase, @Cantidad, 1.00, @UMBase, @Cantidad, 'TRA', 1, 2, 'P', @IdRuta, GETDATE(), @FechaFin, 2,
			@Usuario, GETDATE(), @Usuario, GETDATE(), @Cantidad, @Cantidad, @Cantidad, @Cantidad, @IDPeso)
		END

		IF (@Operacion = 'LIMAGAS') BEGIN
			INSERT INTO OP_GE_OT (IdOT, TipoOT, Codigo, IdContrato, IdVendedor, IdClienteFacturacion, TipoMovimiento, PeriodoEmision, FechaEmision, FechaProceso,
			Estado, EstadoAnterior, UsuarioCreacion, FechaCreacion, UsuarioModificacion, FechaModificacion, FechaFinValidez)
			VALUES (@IdOT, 'T1', CONVERT(VARCHAR(6),RIGHT('000000'+LTRIM(RTRIM(@IdOT)),6)), @IDContrato, @IDVendedor, @Persona, 'TRA', CONVERT(CHAR(4),YEAR(GETDATE()))+
			CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(GETDATE()))),2)), GETDATE(), GETDATE(), 1, 0, @Usuario, GETDATE(), @Usuario, GETDATE(), @FechaFin)

			INSERT INTO OP_GE_OTProducto (IdOT, Linea, IdContrato, LineaContrato, Producto, IndCosto, UMBase, CantidadBase, FactorUsoBase, UMUso, CantidadUso, TipoMovimiento,
			IndServAlmacen, IndServTransporte, IndTercero, IdRuta, FechaInicio, FechaFin, Estado, UsuarioCreacion, FechaCreacion, UsuarioModificacion, FechaModificacion,
			CantBasePendAlm, CantBasePendTrans, CantUsoPendAlm, CantUsoPendTrans, IndOrigenPeso)
			VALUES (@IdOT, 1, @IDContrato, @Linea1, @CodProducto, 2, @UMBase, @Cantidad, 1.00, @UMBase, @Cantidad, 'TRA', 1, 2, 'P', @IdRuta, GETDATE(), @FechaFin, 2,
			@Usuario, GETDATE(), @Usuario, GETDATE(), @Cantidad, @Cantidad, @Cantidad, @Cantidad, @IDPeso)
		END

		-- AGREGAR DETALLE A CONTRATO
		DECLARE @IDCalculo INT = (SELECT CASE WHEN Calculo = 'Fijo' THEN 1 WHEN Calculo = 'Variable' THEN 2 END FROM @TEMP_TARIFAS WHERE Nro = @Contador)
		DECLARE @IDRecorrido INT = (SELECT CASE WHEN Recorrido = 'Ida' THEN 1 WHEN Recorrido = 'Vuelta' THEN 2 ELSE 3 END FROM @TEMP_TARIFAS WHERE Nro = @Contador)
		DECLARE @Tarifa DECIMAL(10,2) = (SELECT Tarifa FROM @TEMP_TARIFAS WHERE Nro = @Contador)
		DECLARE @Linea2 INT
		SET @Linea2 = (SELECT MAX(Linea) FROM OP_GE_ContratoDetalle WHERE IdContrato = @IDContrato)
		SET @Linea2 = ISNULL(@Linea2,0) + 1

		IF (@Moneda = 'SOLES') BEGIN
			SET @TipoMoneda = 'LO'
		END
		ELSE BEGIN
			SET @TipoMoneda = 'EX'
		END

		IF (@Operacion = 'LINDLEY' OR @Operacion = 'VOLCAN') BEGIN
			INSERT INTO OP_GE_ContratoDetalle (IdContrato, Linea, LineaProducto, UMBase, Cantidad, IndGeneral, IndInventario, TipoCalculo, TipoTarifa, TipoFacturacion,
			TipoMovimientoCobro, IndRecorrido, Mna, GrupoServicioOP, Servicio, Tarifa, TipoMinimo, FechaInicio, FechaFin, Estado, UsuarioCreacion, FechaCreacion,
			UsuarioModificacion, FechaModificacion, TipoCalculoCT)
			VALUES (@IDContrato, @Linea2, @Linea1, @UMBase, 1.00, 1, 2, @IDCalculo, 1, 2, 'TRA', @IDRecorrido, @TipoMoneda, 'T', '000001', @Tarifa, 1, @FechaInicio, @FechaFin, 2,
			@Usuario, GETDATE(), @Usuario, GETDATE(), 1)
		END
		IF (@Operacion = 'LIMAGAS') BEGIN
			INSERT INTO OP_GE_ContratoDetalle (IdContrato, Linea, LineaProducto, UMBase, Cantidad, IndGeneral, IndInventario, TipoCalculo, TipoTarifa, TipoFacturacion,
			TipoMovimientoCobro, IndRecorrido, Mna, GrupoServicioOP, Servicio, Tarifa, TipoMinimo, FechaInicio, FechaFin, Estado, UsuarioCreacion, FechaCreacion,
			UsuarioModificacion, FechaModificacion, TipoCalculoCT)
			VALUES (@IDContrato, @Linea2, @Linea1, @UMBase, 1.00, 1, 2, @IDCalculo, 1, 2, 'TRA', @IDRecorrido, @TipoMoneda, 'T', '000001', @Tarifa, 1, @FechaInicio, @FechaFin, 2,
			@Usuario, GETDATE(), @Usuario, GETDATE(), 1)
		END

		UPDATE OP_GE_Contrato
		SET UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()
		WHERE IdContrato = @IDContrato 

		SET @Contador = @Contador + 1
	END

	SET @Exito = '0 = Tarifas generadas exitosamente.'
END TRY

BEGIN CATCH
	SET @exito = RTRIM(LTRIM(STR(ERROR_NUMBER()))) + '=' + ERROR_MESSAGE()
	ROLLBACK
	GOTO Terminar
END CATCH

IF @@TRANCOUNT > 0 
	COMMIT;
    --ROLLBACK
    
TERMINAR:
IF LEFT(@Exito,1)='-' OR cast(left(@Exito,1) AS INT)<0
BEGIN
	DECLARE @storedProcedureName VARCHAR(100)
	SELECT @storedProcedureName = OBJECT_NAME(@@PROCID)
	SET @exito = @exito + CHAR(13)+CHAR(13)+'SP Name: '+@storedProcedureName
END
SELECT @Exito exito

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 31-07-2024
-- Description:	LISTAR TARIFAS DE CLIENTES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ControlTarifas_ListarTarifas]
@Ruta VARCHAR(350),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@Cliente VARCHAR(30)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@Cliente = 'LINDLEY') BEGIN
		SELECT OP.IdOT, OT.FechaCreacion AS 'FECHA_CREACION', OT.IdClienteFacturacion AS 'IdCliente',
		P.NombreCompleto AS 'CLIENTE', OP.Producto AS 'IdProducto', AP.Nombre AS 'PRODUCTO', OP.IdRuta, R.Descripcion AS 'RUTA',
		CONVERT(DECIMAL(10,2),ISNULL(CD.Tarifa,0)) AS 'TARIFA', CASE WHEN CD.Mna = 'LO' THEN 'SOLES' WHEN CD.Mna = 'EX' THEN 'DÓLARES' END AS 'MONEDA',
		CASE WHEN OP.IndOrigenPeso = 1 THEN 'NORMAL' WHEN OP.IndOrigenPeso = 2 THEN 'CLIENTE' END AS 'PESO', CASE WHEN CD.TipoCalculo = 1 THEN 'FIJO'
		WHEN CD.TipoCalculo = 2 THEN 'VARIABLE' END AS 'CÁLCULO', CASE WHEN CD.IndRecorrido = 1 THEN 'IDA' WHEN CD.IndRecorrido = 2 THEN 'VUELTA'
		WHEN CD.IndRecorrido = 3 THEN 'IDA Y VUELTA' END AS 'RECORRIDO'
		FROM OP_GE_OTProducto OP
		LEFT JOIN OP_GE_OT OT ON OT.IdOT = OP.IdOT
		LEFT JOIN PersonaMast P ON P.Persona = OT.IdClienteFacturacion
		LEFT JOIN OP_AL_Producto AP ON AP.Codigo = OP.Producto
		LEFT JOIN OP_TR_Ruta R ON R.IdRuta = OP.IdRuta
		LEFT JOIN OP_GE_ContratoDetalle CD ON CD.IdContrato = OP.IdContrato AND CD.LineaProducto = OP.LineaContrato
		WHERE OT.IdClienteFacturacion = 20323 AND LTRIM(RTRIM(OP.Producto)) = '000573' AND (@Ruta IS NULL OR R.Descripcion LIKE '%' + @Ruta + '%')
		AND (OT.FechaCreacion BETWEEN @FINICIO AND @FFIN)
		ORDER BY IdOT DESC
	END

	IF (@Cliente = 'LIMAGAS') BEGIN
		SELECT OP.IdOT, OT.FechaCreacion AS 'FECHA_CREACION', OT.IdClienteFacturacion AS 'IdCliente',
		P.NombreCompleto AS 'CLIENTE', OP.Producto AS 'IdProducto', AP.Nombre AS 'PRODUCTO', OP.IdRuta, R.Descripcion AS 'RUTA',
		CONVERT(DECIMAL(10,2),ISNULL(CD.Tarifa,0)) AS 'TARIFA', CASE WHEN CD.Mna = 'LO' THEN 'SOLES' WHEN CD.Mna = 'EX' THEN 'DÓLARES' END AS 'MONEDA',
		CASE WHEN OP.IndOrigenPeso = 1 THEN 'NORMAL' WHEN OP.IndOrigenPeso = 2 THEN 'CLIENTE' END AS 'PESO', CASE WHEN CD.TipoCalculo = 1 THEN 'FIJO'
		WHEN CD.TipoCalculo = 2 THEN 'VARIABLE' END AS 'CÁLCULO', CASE WHEN CD.IndRecorrido = 1 THEN 'IDA' WHEN CD.IndRecorrido = 2 THEN 'VUELTA'
		WHEN CD.IndRecorrido = 3 THEN 'IDA Y VUELTA' END AS 'RECORRIDO'
		FROM OP_GE_OTProducto OP
		LEFT JOIN OP_GE_OT OT ON OT.IdOT = OP.IdOT
		LEFT JOIN PersonaMast P ON P.Persona = OT.IdClienteFacturacion
		LEFT JOIN OP_AL_Producto AP ON AP.Codigo = OP.Producto
		LEFT JOIN OP_TR_Ruta R ON R.IdRuta = OP.IdRuta
		LEFT JOIN OP_GE_ContratoDetalle CD ON CD.IdContrato = OP.IdContrato AND CD.LineaProducto = OP.LineaContrato
		WHERE OT.IdClienteFacturacion = 15328 AND LTRIM(RTRIM(OP.Producto)) = '000782' AND (@Ruta IS NULL OR R.Descripcion LIKE '%' + @Ruta + '%')
		AND (OT.FechaCreacion BETWEEN @FINICIO AND @FFIN)
		ORDER BY IdOT DESC
	END

	IF (@Cliente = 'VOLCAN') BEGIN
		SELECT OP.IdOT, OT.FechaCreacion AS 'FECHA_CREACION', OT.IdClienteFacturacion AS 'IdCliente',
		P.NombreCompleto AS 'CLIENTE', OP.Producto AS 'IdProducto', AP.Nombre AS 'PRODUCTO', OP.IdRuta, R.Descripcion AS 'RUTA',
		CONVERT(DECIMAL(10,2),ISNULL(CD.Tarifa,0)) AS 'TARIFA', CASE WHEN CD.Mna = 'LO' THEN 'SOLES' WHEN CD.Mna = 'EX' THEN 'DÓLARES' END AS 'MONEDA',
		CASE WHEN OP.IndOrigenPeso = 1 THEN 'NORMAL' WHEN OP.IndOrigenPeso = 2 THEN 'CLIENTE' END AS 'PESO', CASE WHEN CD.TipoCalculo = 1 THEN 'FIJO'
		WHEN CD.TipoCalculo = 2 THEN 'VARIABLE' END AS 'CÁLCULO', CASE WHEN CD.IndRecorrido = 1 THEN 'IDA' WHEN CD.IndRecorrido = 2 THEN 'VUELTA'
		WHEN CD.IndRecorrido = 3 THEN 'IDA Y VUELTA' END AS 'RECORRIDO'
		FROM OP_GE_OTProducto OP
		LEFT JOIN OP_GE_OT OT ON OT.IdOT = OP.IdOT
		LEFT JOIN PersonaMast P ON P.Persona = OT.IdClienteFacturacion
		LEFT JOIN OP_AL_Producto AP ON AP.Codigo = OP.Producto
		LEFT JOIN OP_TR_Ruta R ON R.IdRuta = OP.IdRuta
		LEFT JOIN OP_GE_ContratoDetalle CD ON CD.IdContrato = OP.IdContrato AND CD.LineaProducto = OP.LineaContrato
		WHERE OT.IdClienteFacturacion IN (25167,25231,4354) AND LTRIM(RTRIM(OP.Producto)) IN ('000816','000870','000871')
		AND (@Ruta IS NULL OR R.Descripcion LIKE '%' + @Ruta + '%') AND (OT.FechaCreacion BETWEEN @FINICIO AND @FFIN)
		ORDER BY IdOT DESC
	END
END