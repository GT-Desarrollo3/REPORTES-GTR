-- Crear tabla ReportesApp_Operaciones_TicketGasto_TipoGasto y llenarla

-- Crear tabla ReportesApp_Operaciones_TicketGasto_Registro

-- Crear tabla ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera

-- Crear tabla ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle

-- Crear tabla ReportesApp_Operaciones_TicketGasto_Recibo

-- Crear tabla ReportesApp_Operaciones_TicketGasto_TipoDocumento y llenarla

-- Crear tabla ReportesApp_Operaciones_TicketGasto_ConceptoGasto y llenarla

-- Crear tabla ReportesApp_Operaciones_TicketGasto_Liquidacion

-- Crear tabla ReportesApp_Operaciones_TicketGasto_Viaticos

-- Crear tabla ReportesApp_Operaciones_TicketGasto_TipoViaticos y llenarla

-- Crear tabla ReportesApp_Operaciones_TicketGasto_Peajes y llenarla

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 20-07-2023
-- Description:	LISTAR TIPOS DE GASTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ListarTipoGasto]
AS
BEGIN
	SELECT idTipoGasto, Descripcion
	FROM ReportesApp_Operaciones_TicketGasto_TipoGasto
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 14-11-2023
-- Description:	LISTAR TIEMPOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ListarTiempos]
AS
BEGIN
	SELECT idTiempo, Descripcion
	FROM ReportesApp_Operaciones_TicketGasto_Tiempo
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 20-07-2023
-- Description:	LISTAR OPERACIONES PREVIAJES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ListarOperaciones]
AS
BEGIN
	SELECT * FROM ReportesApp_Operacion_Previaje_Operaciones
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 19-05-2023
-- Description:	LISTAR REGISTRO DE PREVIAJES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Previajes_Faltantes_ListarRegistro]
@NroTicket INT
AS
BEGIN
	SET NOCOUNT ON;
    SELECT F.FechaProgramacion AS 'FECHA_VIAJE', OP.IdOperacion, OP.Descripcion AS 'PROGRAMACION', V.NumeroPlaca AS 'TRACTO',
	LTRIM(RTRIM(C.Nombre)) AS 'CONDUCTOR', R.NumeroPlaca AS 'SEMIRREMOLQUE', RT.IdRuta, RT.Descripcion AS 'RUTA',
	LTRIM(RTRIM(CL.Busqueda)) as 'CLIENTE', F.CodViaje AS 'CODIGO_VIAJE', F.FechaDescarga AS 'FECHA_DESCARGA',
    (SELECT TOP 1 ISNULL(Serie,'')+'-'+ISNULL (Numero,'') FROM OP_TR_Guia WHERE IdViaje = VJ.IdViaje AND IdConductor = F.IdConductor AND ESTADO NOT IN (9)) AS 'GT',
	(SELECT TOP 1 ISNULL(GUIAREMITENTE,'') FROM OP_TR_Guia WHERE IdViaje = VJ.IdViaje AND IdConductor = F.IdConductor AND ESTADO NOT IN (9)) AS 'GR'
    FROM ReportesApp_Operacion_Previaje_Registros F
    LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = F.idTracto
    LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = F.TipoProgramacion
    LEFT JOIN OP_TR_Viaje VJ WITH(NOLOCK) ON VJ.Codigo = CAST(F.CodViaje AS VARCHAR(12))
    LEFT JOIN OP_TR_Conductor C WITH(NOLOCK) ON C.IdConductor = F.IdConductor
    LEFT JOIN OP_TR_Vehiculo R WITH(NOLOCK) ON R.IdVehiculo = F.idSemirremolque
    LEFT JOIN OP_TR_RUTA RT WITH(NOLOCK) ON RT.IDRUTA = F.IdRuta
    LEFT JOIN PersonaMast CL WITH(NOLOCK) ON CL.Persona = F.IdCliente
    WHERE F.NroTicket = @NroTicket
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 20-07-2023
-- Description:	LISTAR GASTOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ListarGasto]
@IdRuta INT,
@IdOperacion INT
AS
BEGIN
	SET NOCOUNT ON;
    SELECT * FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera
    WHERE IdRuta = @IdRuta AND IdOperacion = @IdOperacion
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 20-07-2023
-- Description:	BUSCAR GASTOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_BuscarGasto]
@NroTicket INT
AS
BEGIN
	SET NOCOUNT ON;
    SELECT * FROM ReportesApp_Operaciones_TicketGasto_Registro
    WHERE NroTicket = @NroTicket
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 20-07-2023
-- Description:	INSERTAR GASTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_InsertarGastoDetalle]
@idTipoGasto INT,
@Descripcion VARCHAR(250),
@Gasto DECIMAL(10,2),
@idTiempo INT,
@Adicional INT
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

SET @Exito = '0 = Gasto añadido.'

SET @correlativo = (SELECT MAX(idGastoxRutaD) FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle)
SET @correlativo = ISNULL(@correlativo,0) + 1

/*
IF(@idTipoGasto = (SELECT idTipoGasto FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle WHERE idTipoGasto = @idTipoGasto AND idGastoxRutaC IS NULL)) BEGIN
	SET @Exito = '-1 = Este tipo de gasto ya fue añadido.'
	GOTO Terminar
END
*/

BEGIN TRAN
BEGIN TRY
	INSERT INTO ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle(idGastoxRutaD, idTipoGasto, Descripcion, Gasto, idTiempo, Adicional)
	VALUES(@correlativo, @idTipoGasto, @Descripcion, @Gasto, @idTiempo, @Adicional)
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
SELECT @exito exito

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 20-07-2023
-- Description:	LISTAR DETALLE DE GASTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ListarGastoDetalle]
@IdRuta INT,
@IdOperacion INT
AS
BEGIN
	(SELECT GD.idGastoxRutaD, TT.Descripcion AS 'Tiempo', T.Descripcion AS 'TipoGasto', GD.Descripcion, GD.Gasto AS 'Efectivo',
	CASE WHEN GD.Adicional = 0 THEN 'NO' ELSE 'SÍ' END AS 'Adicional'
	FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera GC
	LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle GD ON GD.idGastoxRutaC = GC.idGastoxRutaC
	LEFT JOIN ReportesApp_Operaciones_TicketGasto_TipoGasto T ON T.idTipoGasto = GD.idTipoGasto
	LEFT JOIN ReportesApp_Operaciones_TicketGasto_Tiempo TT ON TT.idTiempo = GD.idTiempo
	WHERE GC.IdRuta = @IdRuta AND GC.IdOperacion = @IdOperacion)
	UNION
	(SELECT GD.idGastoxRutaD, TT.Descripcion AS 'Tiempo', T.Descripcion AS 'TipoGasto', GD.Descripcion, GD.Gasto AS 'Efectivo',
	CASE WHEN GD.Adicional = 0 THEN 'NO' ELSE 'SÍ' END AS 'Adicional'
	FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle GD
	LEFT JOIN ReportesApp_Operaciones_TicketGasto_TipoGasto T ON T.idTipoGasto = GD.idTipoGasto
	LEFT JOIN ReportesApp_Operaciones_TicketGasto_Tiempo TT ON TT.idTiempo = GD.idTiempo
	WHERE idGastoxRutaC IS NULL)
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 20-07-2023
-- Description:	ELIMINAR DETALLE DE GASTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_EliminarGastoDetalle]
@idGastoxRutaD INT
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Gasto eliminado.'

IF (@idGastoxRutaD = 0)
BEGIN
	SET @Exito = '-1 = El gasto seleccionado no existe.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	DELETE FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle
	WHERE idGastoxRutaD = @idGastoxRutaD
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
SELECT @exito exito

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 20-07-2023
-- Description:	INSERTAR GASTO CABECERA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_InsertarGasto]
@IdRuta INT,
@IdOperacion INT,
@TotalDias INT,
@GastoTotal DECIMAL(10,2),
@Detalle INT
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @SinAdicional DECIMAL(10,2)
DECLARE @correlativo INT

SET @Exito = '0 = Gasto asignado.'

IF (@IdRuta IS NULL AND @IdOperacion IS NULL)
BEGIN
	SET @Exito = '-1 = No se pudo asignar el gasto.'
	GOTO Terminar
END

SET @correlativo = (SELECT MAX(idGastoxRutaC) FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera)
SET @correlativo = ISNULL(@correlativo,0) + 1

BEGIN TRAN
BEGIN TRY
	IF (@IdRuta = (SELECT IdRuta FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera WHERE IdRuta = @IdRuta AND IdOperacion = @IdOperacion)) BEGIN
		UPDATE ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera
		SET GastoTotal = @GastoTotal, TotalDias = @TotalDias
		WHERE idGastoxRutaC = (SELECT idGastoxRutaC FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera WHERE IdRuta = @IdRuta AND IdOperacion = @IdOperacion)
		
		UPDATE ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle
		SET idGastoxRutaC = (SELECT idGastoxRutaC FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera WHERE IdRuta = @IdRuta AND IdOperacion = @IdOperacion)
		WHERE idGastoxRutaC IS NULL

		SET @SinAdicional = (SELECT ISNULL(SUM(Gasto),0) FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle
		WHERE idGastoxRutaC = (SELECT idGastoxRutaC FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera
		WHERE IdRuta = @IdRuta AND IdOperacion = @IdOperacion) AND Adicional = 0)

		UPDATE ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera
		SET GastoTotal = @SinAdicional
		WHERE idGastoxRutaC = (SELECT idGastoxRutaC FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera WHERE IdRuta = @IdRuta AND IdOperacion = @IdOperacion)
		
		SET @Exito = '0 = Gasto actualizado.'
	END
	ELSE BEGIN
		IF (@Detalle = 1) BEGIN
			UPDATE ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle
			SET idGastoxRutaC = @correlativo
			WHERE idGastoxRutaC IS NULL
		END
		
		INSERT INTO ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera(idGastoxRutaC, TotalDias, IdRuta, IdOperacion)
		VALUES (@correlativo, @TotalDias, @IdRuta, @IdOperacion)

		SET @SinAdicional = (SELECT ISNULL(SUM(Gasto),0) FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle
		WHERE idGastoxRutaC = @correlativo AND Adicional = 0)

		UPDATE ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera
		SET GastoTotal = @SinAdicional
		WHERE idGastoxRutaC = @correlativo
	END
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
SELECT @exito exito

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 21-07-2023
-- Description:	INSERTAR TICKET DE GASTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_RegistrarTicketGasto]
@NroTicket INT,
@idGastoxRutaC INT,
@TotalEntregado DECIMAL(10,2),
@Usuario VARCHAR(50)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT
DECLARE @IdConductor INT
DECLARE @Contador INT
DECLARE @idOperacion INT

SET @Exito = '0 = Planilla Registrada.'

SET @IdConductor = (SELECT IdConductor FROM ReportesApp_Operacion_Previaje_Registros WHERE NroTicket = @NroTicket)
SET @Contador = (SELECT COUNT(TG.ViConductor) FROM ReportesApp_Operaciones_TicketGasto_Registro TG WHERE TG.EstadoLiquidacion = 0 AND TG.ViConductor = @IdConductor)

/*
IF (@Contador >= 4)
BEGIN
	SET @Exito = '-1 = Este conductor aún tiene 4 planillas por liquidar.'
	GOTO Terminar
END
*/

SET @correlativo = (SELECT MAX(idTicketGasto) FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE Anio = YEAR(GETDATE()))
SET @correlativo = ISNULL(@correlativo,0) + 1


BEGIN TRAN
BEGIN TRY
	INSERT INTO ReportesApp_Operaciones_TicketGasto_Registro(idTicketGasto, Anio, CodGasto, NroTicket, idGastoXRutaC, TotalEntregado, FechaCreacion, UsuarioCreacion,EstadoLiquidacion,Permiso)
	VALUES(@correlativo, YEAR(GETDATE()), SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo)),6)), @NroTicket, @idGastoxRutaC, @TotalEntregado, GETDATE(), @Usuario, 0, 0)

	UPDATE ReportesApp_Operaciones_TicketGasto_Registro
	SET ViConductor = (SELECT IdConductor FROM ReportesApp_Operacion_Previaje_Registros WHERE NroTicket = @NroTicket), IdOperacion = (SELECT TipoProgramacion FROM ReportesApp_Operacion_Previaje_Registros WHERE NroTicket = @NroTicket)
	WHERE CodGasto = SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo)),6))
	
	UPDATE ReportesApp_Operacion_Previaje_Registros
	SET Planilla = SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo)),6))
	WHERE NroTicket = @NroTicket
	
	SET @Exito = '0 = Planilla Registrada. PL - ' + SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo)),6))
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
SELECT @exito exito

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 21-07-2023
-- Description:	INSERTAR TICKET DE GASTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ImprimirPago]
@NroTicket INT,
@idGastoxRutaC INT,
@CodGasto VARCHAR(30),
@Usuario VARCHAR(50)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @Nro INT
DECLARE @Nro2 INT
DECLARE @Nro3 INT
DECLARE @Nro4 INT
DECLARE @GastoTotal DECIMAL(10,2)
DECLARE @NroConductor INT
DECLARE @NroUsuario INT
DECLARE @NombreConductor VARCHAR(250)
DECLARE @NombreUsuario VARCHAR(250)
DECLARE @BancoConductor CHAR(3)
DECLARE @CuentaConductor CHAR(20)
DECLARE @TipoCambio MONEY
DECLARE @UltimoNroPago INT
DECLARE @MesActual INT
DECLARE @NroVoucher INT
DECLARE @Ruta VARCHAR(250)
DECLARE @CuentaBancaria CHAR(15)
DECLARE @Sucursal CHAR(4)
DECLARE @UnidadReplicacion CHAR(4)

SET @Exito = '0 = Pago registrado.'

IF (@CodGasto = '0') BEGIN
	SET @CodGasto = (SELECT TOP(1) CodGasto FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE NroTicket = @NroTicket AND EstadoLiquidacion = 0 ORDER BY FechaCreacion DESC)
END

IF(EXISTS(SELECT * FROM Obligaciones WHERE NumeroDocumentoInterno = @CodGasto AND EstadoDocumento = 'AP')) BEGIN
	SET @Exito = '-1 = El pago de este viaje ya fue registrado.'
	GOTO Terminar
END

IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_TicketGasto_UsuarioSucursal WHERE Usuario = @Usuario AND Estado = 'A')) BEGIN
	SET @CuentaBancaria = (SELECT CuentaBancaria FROM ReportesApp_Operaciones_TicketGasto_UsuarioSucursal WHERE Usuario = @Usuario)
	SET @Sucursal = (SELECT Sucursal FROM ReportesApp_Operaciones_TicketGasto_UsuarioSucursal WHERE Usuario = @Usuario)
	SET @UnidadReplicacion = 'TRUJ'
END
ELSE BEGIN
	SET @Exito = '-1 = Este usuario está inactivo o no tiene una sede registrada.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	--ACTUALIZAR CORRELATIVO DE ADELANTOS
	UPDATE CorrelativosMast
	SET CorrelativoNumero = (SELECT CorrelativoNumero + 1 FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='APAD')),
		UltimaFechaModif = GETDATE()
	WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='APAD')
	
	SET @Nro = (SELECT CorrelativosMast.CorrelativoNumero FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='APAD'))
	SET @GastoTotal = (SELECT TOP(1) TotalEntregado FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE CodGasto = @CodGasto AND EstadoLiquidacion = 0 ORDER BY FechaCreacion DESC)
	SET @NroConductor = (SELECT P.Persona FROM ReportesApp_Operacion_Previaje_Registros R
						 LEFT JOIN OP_TR_Conductor C ON C.IdConductor = R.IdConductor
						 LEFT JOIN PersonaMast P ON P.Persona = C.IdPersona WHERE R.NroTicket = @NroTicket)
	SET @NombreConductor = (SELECT P.NombreCompleto FROM ReportesApp_Operacion_Previaje_Registros R
							LEFT JOIN OP_TR_Conductor C ON C.IdConductor = R.IdConductor
							LEFT JOIN PersonaMast P ON P.Persona = C.IdPersona WHERE R.NroTicket = @NroTicket)
	SET @BancoConductor = (SELECT P.BancoMonedaLocal FROM ReportesApp_Operacion_Previaje_Registros R
						   LEFT JOIN OP_TR_Conductor C ON C.IdConductor = R.IdConductor
						   LEFT JOIN PersonaMast P ON P.Persona = C.IdPersona WHERE R.NroTicket = @NroTicket)
	SET @CuentaConductor = (SELECT P.CuentaMonedaLocal FROM ReportesApp_Operacion_Previaje_Registros R
						    LEFT JOIN OP_TR_Conductor C ON C.IdConductor = R.IdConductor
						    LEFT JOIN PersonaMast P ON P.Persona = C.IdPersona WHERE R.NroTicket = @NroTicket)
	SET @NroUsuario = (SELECT P.Persona FROM Usuario U LEFT JOIN PersonaMast P ON P.NombreCompleto = U.Nombre WHERE U.Usuario = @Usuario AND P.Estado = 'A' AND P.TipoDocumento = 'D')
	SET @Ruta = (SELECT V.Descripcion FROM ReportesApp_Operacion_Previaje_Registros R LEFT JOIN OP_TR_Ruta V ON R.IdRuta = V.IdRuta WHERE R.NroTicket = @NroTicket)
	
	DELETE FROM AP_GastoAdelantoSustento WHERE (AP_GastoAdelantoSustento.UnidadReplicacion ='TRUJ') AND (AP_GastoAdelantoSustento.TipoAdelanto ='E') AND (AP_GastoAdelantoSustento.NumeroAdelanto = @Nro) 
	
	--INSERTAR ADELANTO DE GASTO
	INSERT INTO AP_GastoAdelanto(NumeroAdelanto,CompaniaSocio,Clasificacion,FechaDocumento,TipoPago,Persona,PreparadoPor,FechaPreparacion,MonedaDocumento,MontoTotal,SaldoAdelanto,Descripcion,Estado,UltimoUsuario,
								 UltimaFechaModif,FechaEsperadaPago,TipoAdelanto,NumeroDocumentoInterno,FlujodeCaja,UnidadReplicacion,PartidaPresupuestal,PersonaPagara,ConceptoGasto,CentroCostos,EmpleadoAutorizado,
								 UnidadNegocio,RegComprasMontoAfecto,RegComprasMontoNoAfecto,RegComprasMontoIgv,CuentaBancaria)
	VALUES (@Nro,'10000000','ER',GETDATE(),'EF', @NroConductor, @NroUsuario, GETDATE(), 'LO', @GastoTotal, @GastoTotal, 'GASTOS DE VIAJE - PL ' + @CodGasto + ' - PRVJ: '+ CONVERT(VARCHAR,@NroTicket),
	'PR', @Usuario, GETDATE(), GETDATE(), 'E', @CodGasto, '051', 'TRUJ', '9999', @NroConductor, '0006', '010201', @NroUsuario, 'TRAN', 0.00, 0.00, 0.00, @CuentaBancaria)
	
	INSERT INTO AP_GastoAdelantoSustento(UnidadReplicacion,TipoAdelanto,NumeroAdelanto,Secuencia,ConceptoGasto,NumeroVeces,Moneda,PrecioUnitario,MontoTotal,UltimoUsuario,UltimaFechaModif,CuentaContable,MontoPresupuesto)
	VALUES ('TRUJ', 'E', @Nro, 1, '0006', 8, 'LO', @GastoTotal, @GastoTotal, @Usuario, GETDATE(), '1413002', 0.00)

	--APROBAR ADELANTO DE GASTO
	UPDATE AP_GastoAdelanto
	SET AprobadoPor = @NroUsuario,
		FechaAprobacion = GETDATE(), Estado = 'AP'
	WHERE NumeroAdelanto = @Nro AND TipoAdelanto = 'E' AND UnidadReplicacion = 'TRUJ'
	
	--INSERTAR OBLIGACIÓN
	UPDATE CorrelativosMast
	SET CorrelativoNumero = (SELECT CorrelativoNumero + 1 FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie = 'APNO')),
		UltimaFechaModif = GETDATE()
	WHERE (CorrelativosMast.CompaniaCodigo = '999999') AND (CorrelativosMast.TipoComprobante = 'SY') AND (CorrelativosMast.Serie = 'APNO')
	
	SET @Nro2 = (SELECT CorrelativosMast.CorrelativoNumero FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo = '999999') AND (CorrelativosMast.TipoComprobante = 'SY') AND (CorrelativosMast.Serie = 'APNO'))

	INSERT INTO Obligaciones (Proveedor,TipoDocumento,NumeroDocumento,CuentaBancaria,CompaniaCodigo,TipoPago,FechaRegistro,FechaVencimiento,FechaVencimientoOriginal,GenerarPago,TipoServicio,MonedaDocumento,ConversionRequerida,
							  MonedaPago,MontoObligacion,MontoImpuestoVentas,MontoNoAfecto,MontoImponible,MontoAdelantos,MontoImpuestos,NetoMonedaLocal,NetoMonedaExtranjera,TipoDeCambio,AprobadoPor,AprobadoCP1,AprobadoCP2,
							  IngresadoPor,RevisadoPor,EstadoDocumento,ContabilizacionPendiente,ChequeIndividual,Voucher,NumeroPago,NumeroProceso,ProcesoSecuencia,RegistroNumero,Comentarios,UltimaFechaModif,UnidadNegocio,
							  FacturaAfectaSplitFlag,FactorRValidacion,UnidadReplicacion,CanjeRegistroNumero,MontoPagoParcial,NumeroDocumentoInterno,CentroCosto,PartidaPresupuestal,FlujodeCaja,CentroCostoCP,FechaRecepcion,
							  ProveedorPagarA,ControlPresupuestalFlag,CargoFlag,MontoCreditoFiscal,FechaDocumento,AfectoIGVFlag,DiferidoFlag,AdelantoFlag)
	VALUES (@NroConductor,'AE','TRUJ-'+CONVERT(VARCHAR(10),@Nro),@CuentaBancaria,'100000','EF',GETDATE(),GETDATE(),GETDATE(),'S','INAFEC','LO','D','LO',@GastoTotal,0.00,0.00,@GastoTotal,0.00,0.00,@GastoTotal,
	0.00,0,0,0,0,@NroUsuario, @NroUsuario, 'RV', 'S', 'N', CONVERT(CHAR(4),YEAR(GETDATE()))+CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(GETDATE()))),2)),0,0,0,@Nro2,'GASTOS DE VIAJE - PL ' + @CodGasto +
	' - PRVJ: '+ CONVERT(VARCHAR,@NroTicket), GETDATE(),'TRAN','N','N','TRUJ',0,0.00,@CodGasto,'010201','9999','051','010201',GETDATE(),@NroConductor, 'N', 'N', 0.00, GETDATE(), 'N', 'N', 'N')
	
	INSERT INTO ObligacionesXCuenta (Descripcion,Proveedor,NumeroDocumento,Linea,Monto,CentroCosto,CuentaContable,PartidaPresupuestal,Persona,DocumentoReferencia,Sucursal,FlujodeCaja,NoAfectoIGVFlag,TipoDocumento)
	VALUES ('GASTOS DE VIAJE - PL ' + @CodGasto + ' - PRVJ: '+ CONVERT(VARCHAR,@NroTicket),@NroConductor,'TRUJ-'+CONVERT(VARCHAR(10),@Nro),1,@GastoTotal,'010201','1413002','9999', @NroConductor,
	'AE-TRUJ-'+CONVERT(VARCHAR(10),@Nro),@Sucursal, '051', 'N', 'AE')
	
	UPDATE AP_GastoAdelanto
	SET MonedaDocumento = 'LO',ObligacionTipoDocumento = 'AE',ObligacionNumeroDocumento = 'TRUJ-'+CONVERT(VARCHAR(10),@Nro),Estado = 'TR'
	WHERE UnidadReplicacion = 'TRUJ' AND TipoAdelanto = 'E' AND NumeroAdelanto = @Nro 
	
	INSERT INTO AP_ObligacionFlujo (Proveedor,TipoDocumento,NumeroDocumento,Secuencia,AreaDocumentaria,Comentario,Estado,UltimoUsuario,UltimaFechaModif)
	VALUES (@NroConductor, 'AE','TRUJ-'+CONVERT(VARCHAR(10),@Nro), 1,'001','Recepción Inicial','A',@Usuario,GETDATE())
	
	--INSERTAR ORDEN DE PAGO
	INSERT INTO OrdenPago (SistemaFuente,Proveedor,TipoDocumento,NumeroDocumento,PagarA,CompaniaCodigo,CuentaBancaria,TipoPago,ChequeIndividual,FechaVencimiento,MonedaDocumento,MonedaPago,MontoMoneda,TipodeCambio,
						   BatchNumber,FechaTransferencia,Estado,FlujodeCaja,RegistroNumero,FechaDocumento,PagoDiferidoFlag,UnidadNegocio)
	VALUES ('AP',@NroConductor,'AE','TRUJ-'+CONVERT(VARCHAR(10),@Nro),@NombreConductor,'100000',@CuentaBancaria,'EF','N',GETDATE(),'LO','LO',@GastoTotal,0,0,GETDATE(),'AP','051', @Nro2, GETDATE(),'N','TRAN')

	SET @TipoCambio = (CONVERT(MONEY,(SELECT TipoCambioMast.FactorVenta FROM TipoCambioMast WHERE TipoCambioMast.MonedaCodigo ='EX' AND TipoCambioMast.MonedaCambioCodigo ='LO' AND TipoCambioMast.FechaCambio =CONVERT(DATE,GETDATE()))))

	UPDATE Obligaciones
	SET AprobadoCP1 = @NroUsuario, EstadoDocumento = 'AP', Voucher = CONVERT(CHAR(4),YEAR(GETDATE()))+CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(GETDATE()))),2)),
		FechaAprobacion = GETDATE(), TipodeCambioProvision = @TipoCambio, UltimoUsuario = @Usuario, UltimaFechaModif = GETDATE()
	WHERE Proveedor = @NroConductor AND TipoDocumento = 'AE' AND NumeroDocumento = 'TRUJ-'+CONVERT(VARCHAR(10),@Nro)
	
	DELETE XOrdenPago WHERE XOrdenPago.Usuario = @Usuario
	
	INSERT INTO XOrdenPago (TipoRegistro,SistemaFuente,Proveedor,TipoDocumento,NumeroDocumento,CompaniaCodigo,PagarA,CuentaBancaria,TipoPago,ChequeIndividual,MonedaPago,FechaVencimiento,Estado,MonedaDocumento,MontoMoneda,Usuario,MontoOriginal)
	VALUES ('D','AP',@NroConductor,'AE','TRUJ-'+CONVERT(VARCHAR(10),@Nro),'100000',@NombreConductor,@CuentaBancaria,'EF','N','LO',GETDATE(),'A','LO',@GastoTotal,@Usuario,@GastoTotal)
	
	INSERT INTO XOrdenPago (TipoRegistro,SistemaFuente,Proveedor,TipoDocumento,NumeroDocumento,CompaniaCodigo,PagarA,CuentaBancaria,TipoPago,ChequeIndividual,MonedaPago,Estado,MontoMoneda,Usuario)
	VALUES ('S','XX',@NroConductor,'ZZ','000001','100000',@NombreConductor,@CuentaBancaria,'EF','N','LO','A',@GastoTotal,@Usuario)

	UPDATE ReportesApp_Operaciones_TicketGasto_Registro
	SET GastoDiferencial = NULL, GastoDiferencialV = NULL
	WHERE NroTicket = @NroTicket
	
	/*
	--INSERTAR PAGO
	UPDATE CorrelativosMast
	SET CorrelativoNumero = (SELECT CorrelativoNumero + 1 FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie = 'APRU')),
		UltimaFechaModif = GETDATE()
	WHERE (CorrelativosMast.CompaniaCodigo = '999999') AND (CorrelativosMast.TipoComprobante = 'SY') AND (CorrelativosMast.Serie = 'APRU')
	
	SET @Nro3 = (SELECT CorrelativosMast.CorrelativoNumero FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo = '999999') AND (CorrelativosMast.TipoComprobante = 'SY') AND (CorrelativosMast.Serie = 'APRU'))

	UPDATE AP_GastoAdelanto
	SET Estado ='PA'
	WHERE (AP_GastoAdelanto.UnidadReplicacion='TRUJ') AND (AP_GastoAdelanto.TipoAdelanto='E') AND (AP_GastoAdelanto.NumeroAdelanto=@Nro)
	
	UPDATE Obligaciones
	SET FechaPago = GETDATE(), NetoMonedaLocal = @GastoTotal, NetoMonedaExtranjera = CONVERT(MONEY,@GastoTotal / @TipoCambio), EstadoDocumento = 'PA', NumeroProceso = @Nro3, ProcesoSecuencia = 1, TipoDeCambio = CONVERT(REAL,@TipoCambio),
	MontoRetenidoLocal = 0.0000, MontoRetenidoDolares = 0.0000
	WHERE Proveedor = @NroConductor AND TipoDocumento = 'AE' AND NumeroDocumento = 'TRUJ-'+CONVERT(VARCHAR(10),@Nro)
	
	DELETE FROM OrdenPago WHERE SistemaFuente = 'AP' AND Proveedor = @NroConductor AND TipoDocumento = 'AE' AND NumeroDocumento = 'TRUJ-'+CONVERT(VARCHAR(10),@Nro)
	
	INSERT INTO Pagos (NumeroProceso,Secuencia,TipoPago,CuentaBancaria,CompaniaCodigo,UnidadNegocio,NumeroPago,Proveedor,PagarA,MonedaPago,FechaPago,PagoMonedaLocal,PagoMonedaExtranjera,TipodeCambio,DepositoBanco,DepositoCuenta,OrigenGeneracion,
					   Estado,EstadoEntrega,EstadoChequeManual,ContabilizacionPendiente,NegociacionFlag,FlujodeCaja,NoNegociableFlag,CobradoFlag,UltimoUsuario,UltimaFechaModif,CertificadoImpresionFlag,MontoRetenidoLocal,MontoRetenidoDolares,
					   DiferidoFlag,Sucursal,FechaImpresion,DescripcionChequeManual)
	VALUES (@Nro3,1,'EF','FONDO GV TRUX','100000','TRAN',0,@NroConductor,@NombreConductor,'LO',GETDATE(),@GastoTotal,CONVERT(MONEY,@GastoTotal / @TipoCambio),@TipoCambio,@BancoConductor,@CuentaConductor,'A','GE','C',' ','S','N','051','S','N',
	@Usuario, GETDATE(), 'N', 0.00, 0.00, 'N', 'BTRU', GETDATE(), 'GASTOS DE VIAJE - PL ' + @CodGasto + ' - PRVJ: '+ CONVERT(VARCHAR,@NroTicket))
	
	SET @UltimoNroPago = (SELECT UltimoNumeroPago + 1 FROM UltimoNumeroPago WHERE UltimoNumeroPago.CuentaBancaria ='FONDO GV TRUX' AND UltimoNumeroPago.TipoPago ='EF')
	
	UPDATE Obligaciones
	SET NumeroPago = (SELECT UltimoNumeroPago FROM UltimoNumeroPago WHERE UltimoNumeroPago.CuentaBancaria ='FONDO GV TRUX' AND UltimoNumeroPago.TipoPago ='EF')
	WHERE (Obligaciones.NumeroProceso = @Nro3) AND (Obligaciones.ProcesoSecuencia = 1)

	--INSERTAR TRANSACCION
	UPDATE CorrelativosMast
	SET CorrelativoNumero = (SELECT CorrelativoNumero + 1 FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie = 'APBT')),
		UltimaFechaModif = GETDATE()
	WHERE (CorrelativosMast.CompaniaCodigo = '999999') AND (CorrelativosMast.TipoComprobante = 'SY') AND (CorrelativosMast.Serie = 'APBT')
	
	SET @Nro4 = (SELECT CorrelativosMast.CorrelativoNumero FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo = '999999') AND (CorrelativosMast.TipoComprobante = 'SY') AND (CorrelativosMast.Serie = 'APBT'))

	INSERT INTO AP_BancoTransaccion (UnidadReplicacion,NumeroTransaccion,Secuencia,CompaniaSocio,TipoTransaccion,CuentaBancaria,FechaTransaccion,Negociacion,PeriodoContable,VoucherNo,Moneda,MontoLocal,MontoDolares,MontoGasto,TipodeCambio,
									 Comentario,Proveedor,NumeroPago,EmpleadoResponsable,PreparadoPor,FechaPreparacion,Estado,UltimoUsuario,UltimaFechaModif,PagosNumeroProceso,PagosSecuencia,FlujodeCaja,Sucursal)
	VALUES ('TRUJ', @Nro4, 0, '10000000','PAG','FONDO GV TRUX',GETDATE(),'00',CONVERT(CHAR(4),YEAR(GETDATE()))+CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(GETDATE()))),2)),' ','LO', -@GastoTotal, -CONVERT(MONEY,@GastoTotal / @TipoCambio),
	0.00,@TipoCambio,LTRIM(RTRIM(@NombreConductor)) + ' - GASTOS DE VIAJE - PL ' + @CodGasto + ' - PRVJ: '+CONVERT(VARCHAR,@NroTicket),@NroConductor,@UltimoNroPago,@NroUsuario,@NroUsuario,GETDATE(),'AP',@Usuario,GETDATE(),@Nro3,1,'051','BTRU')
	
	UPDATE AP_CuentaBancariaBalance
	SET SaldoActual = (SELECT SaldoActual FROM AP_CuentaBancariaBalance WHERE CuentaBancaria = 'FONDO GV TRUX' AND Negociacion = '00')-@GastoTotal
	WHERE CuentaBancaria = 'FONDO GV TRUX' AND Negociacion = '00'
	
	UPDATE Pagos
	SET NumeroPago = @UltimoNroPago, Estado = 'IM', VoucherPago = CONVERT(CHAR(4),YEAR(GETDATE()))+CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(GETDATE()))),2)), NumeroFormulario = ''
	WHERE NumeroProceso = @Nro3 AND Secuencia = 1 
	
	UPDATE UltimoNumeroPago
	SET UltimoNumeroPago = @UltimoNroPago, UltimoUsuario = @Usuario
	WHERE CuentaBancaria ='FONDO GV TRUX' AND TipoPago = 'EF'
	
	--GENERAR VOUCHER
	DELETE FROM VoucherInterfaseWork WHERE application ='A1'
	
	SET @MesActual = (SELECT CONVERT(INT,MONTH(GETDATE())))
	IF (@MesActual = 1) BEGIN
		UPDATE lastvouchernumber SET month01 = month01 + 1 WHERE (lastvouchernumber.companyowner ='10000000') AND (lastvouchernumber.year = CONVERT(CHAR(4),YEAR(GETDATE()))) AND (lastvouchernumber.type ='PG')
		SET @NroVoucher = (SELECT month01 FROM lastvouchernumber WHERE (lastvouchernumber.companyowner ='10000000') AND (lastvouchernumber.year = CONVERT(CHAR(4),YEAR(GETDATE()))) AND (lastvouchernumber.type ='PG'))
	END
	IF (@MesActual = 2) BEGIN
		UPDATE lastvouchernumber SET month02 = month02 + 1 WHERE (lastvouchernumber.companyowner ='10000000') AND (lastvouchernumber.year = CONVERT(CHAR(4),YEAR(GETDATE()))) AND (lastvouchernumber.type ='PG')
		SET @NroVoucher = (SELECT month02 FROM lastvouchernumber WHERE (lastvouchernumber.companyowner ='10000000') AND (lastvouchernumber.year = CONVERT(CHAR(4),YEAR(GETDATE()))) AND (lastvouchernumber.type ='PG'))
	END
	IF (@MesActual = 3) BEGIN
		UPDATE lastvouchernumber SET month03 = month03 + 1 WHERE (lastvouchernumber.companyowner ='10000000') AND (lastvouchernumber.year = CONVERT(CHAR(4),YEAR(GETDATE()))) AND (lastvouchernumber.type ='PG')
		SET @NroVoucher = (SELECT month03 FROM lastvouchernumber WHERE (lastvouchernumber.companyowner ='10000000') AND (lastvouchernumber.year = CONVERT(CHAR(4),YEAR(GETDATE()))) AND (lastvouchernumber.type ='PG'))
	END
	IF (@MesActual = 4) BEGIN
		UPDATE lastvouchernumber SET month04 = month04 + 1 WHERE (lastvouchernumber.companyowner ='10000000') AND (lastvouchernumber.year = CONVERT(CHAR(4),YEAR(GETDATE()))) AND (lastvouchernumber.type ='PG')
		SET @NroVoucher = (SELECT month04 FROM lastvouchernumber WHERE (lastvouchernumber.companyowner ='10000000') AND (lastvouchernumber.year = CONVERT(CHAR(4),YEAR(GETDATE()))) AND (lastvouchernumber.type ='PG'))
	END		
	IF (@MesActual = 5) BEGIN
		UPDATE lastvouchernumber SET month05 = month05 + 1 WHERE (lastvouchernumber.companyowner ='10000000') AND (lastvouchernumber.year = CONVERT(CHAR(4),YEAR(GETDATE()))) AND (lastvouchernumber.type ='PG')
		SET @NroVoucher = (SELECT month05 FROM lastvouchernumber WHERE (lastvouchernumber.companyowner ='10000000') AND (lastvouchernumber.year = CONVERT(CHAR(4),YEAR(GETDATE()))) AND (lastvouchernumber.type ='PG'))
	END
	IF (@MesActual = 6) BEGIN
		UPDATE lastvouchernumber SET month06 = month06 + 1 WHERE (lastvouchernumber.companyowner ='10000000') AND (lastvouchernumber.year = CONVERT(CHAR(4),YEAR(GETDATE()))) AND (lastvouchernumber.type ='PG')
		SET @NroVoucher = (SELECT month06 FROM lastvouchernumber WHERE (lastvouchernumber.companyowner ='10000000') AND (lastvouchernumber.year = CONVERT(CHAR(4),YEAR(GETDATE()))) AND (lastvouchernumber.type ='PG'))
	END	
	IF (@MesActual = 7) BEGIN
		UPDATE lastvouchernumber SET month07 = month07 + 1 WHERE (lastvouchernumber.companyowner ='10000000') AND (lastvouchernumber.year = CONVERT(CHAR(4),YEAR(GETDATE()))) AND (lastvouchernumber.type ='PG')
		SET @NroVoucher = (SELECT month07 FROM lastvouchernumber WHERE (lastvouchernumber.companyowner ='10000000') AND (lastvouchernumber.year = CONVERT(CHAR(4),YEAR(GETDATE()))) AND (lastvouchernumber.type ='PG'))
	END
	IF (@MesActual = 8) BEGIN
		UPDATE lastvouchernumber SET month08 = month08 + 1 WHERE (lastvouchernumber.companyowner ='10000000') AND (lastvouchernumber.year = CONVERT(CHAR(4),YEAR(GETDATE()))) AND (lastvouchernumber.type ='PG')
		SET @NroVoucher = (SELECT month08 FROM lastvouchernumber WHERE (lastvouchernumber.companyowner ='10000000') AND (lastvouchernumber.year = CONVERT(CHAR(4),YEAR(GETDATE()))) AND (lastvouchernumber.type ='PG'))
	END	
	IF (@MesActual = 9) BEGIN
		UPDATE lastvouchernumber SET month09 = month09 + 1 WHERE (lastvouchernumber.companyowner ='10000000') AND (lastvouchernumber.year = CONVERT(CHAR(4),YEAR(GETDATE()))) AND (lastvouchernumber.type ='PG')
		SET @NroVoucher = (SELECT month09 FROM lastvouchernumber WHERE (lastvouchernumber.companyowner ='10000000') AND (lastvouchernumber.year = CONVERT(CHAR(4),YEAR(GETDATE()))) AND (lastvouchernumber.type ='PG'))
	END
	IF (@MesActual = 10) BEGIN
		UPDATE lastvouchernumber SET month10 = month10 + 1 WHERE (lastvouchernumber.companyowner ='10000000') AND (lastvouchernumber.year = CONVERT(CHAR(4),YEAR(GETDATE()))) AND (lastvouchernumber.type ='PG')
		SET @NroVoucher = (SELECT month10 FROM lastvouchernumber WHERE (lastvouchernumber.companyowner ='10000000') AND (lastvouchernumber.year = CONVERT(CHAR(4),YEAR(GETDATE()))) AND (lastvouchernumber.type ='PG'))
	END	
	IF (@MesActual = 11) BEGIN
		UPDATE lastvouchernumber SET month11 = month11 + 1 WHERE (lastvouchernumber.companyowner ='10000000') AND (lastvouchernumber.year = CONVERT(CHAR(4),YEAR(GETDATE()))) AND (lastvouchernumber.type ='PG')
		SET @NroVoucher = (SELECT month11 FROM lastvouchernumber WHERE (lastvouchernumber.companyowner ='10000000') AND (lastvouchernumber.year = CONVERT(CHAR(4),YEAR(GETDATE()))) AND (lastvouchernumber.type ='PG'))
	END
	IF (@MesActual = 12) BEGIN
		UPDATE lastvouchernumber SET month12 = month12 + 1 WHERE (lastvouchernumber.companyowner ='10000000') AND (lastvouchernumber.year = CONVERT(CHAR(4),YEAR(GETDATE()))) AND (lastvouchernumber.type ='PG')
		SET @NroVoucher = (SELECT month12 FROM lastvouchernumber WHERE (lastvouchernumber.companyowner ='10000000') AND (lastvouchernumber.year = CONVERT(CHAR(4),YEAR(GETDATE()))) AND (lastvouchernumber.type ='PG'))
	END	
	
	INSERT INTO voucherheader (period,currency,vouchertype,ledger,vouchersource,department,runnumber,dollarcredits,dollardebits,localcredits,localdebits,totallines,totalerrorlines,prepareddate,approveddate,reprintnumber,lastuser,lastdate,
				status,vouchertitle,companyowner,exchangerate,voucherno,BusinessUnit,postingnumber,preparedby,approvedby,exchangerateperiod,VoucherDate,ReplicationUnit)
	VALUES (CONVERT(CHAR(4),YEAR(GETDATE()))+CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(GETDATE()))),2)), 'LO', 'N', '00', 'AUTOCPPR', 'AC', 0, -CONVERT(MONEY,@GastoTotal / @TipoCambio), CONVERT(MONEY,@GastoTotal / @TipoCambio),
			-@GastoTotal, @GastoTotal, 2, 0, GETDATE(), GETDATE(), 0, @Usuario, GETDATE(), 'AP', 'Pago - '+CONVERT(VARCHAR,@NroConductor)+' - '+@NombreConductor, '10000000', @TipoCambio,'PG'+RIGHT('0000'+LTRIM(RTRIM(@NroVoucher)),4),'TRAN', 0,
			@NroUsuario, @NroUsuario, GETDATE(), GETDATE(), 'TRUJ')
	
	INSERT INTO voucherdetail (period,companyowner,voucherline,vendor,postedamountlocal,postedamountdollar,status,voucherno,localamount,dollaramount,variabledate,CashFlowCode,Account,invoice,description,checknumber,Sucursal,thirdamount)
	VALUES (CONVERT(CHAR(4),YEAR(GETDATE()))+CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(GETDATE()))),2)), '10000000', 1, @NroConductor, 0.00, 0.00, 'V', 'PG'+RIGHT('0000'+LTRIM(RTRIM(@NroVoucher)),4), -@GastoTotal, -CONVERT(MONEY,@GastoTotal / @TipoCambio),
			GETDATE(), '051', '1020005', 'EF-'+RIGHT('0000000000'+LTRIM(RTRIM(@UltimoNroPago)),10), @NombreConductor, RIGHT('0000000000'+LTRIM(RTRIM(@UltimoNroPago)),10),'BTRU',0.00)
	
	INSERT INTO voucherdetail (period,companyowner,voucherline,vendor,postedamountlocal,postedamountdollar,status,voucherno,localamount,dollaramount,variabledate,CostCenter,Account,invoice,description,Sucursal,thirdamount)
	VALUES (CONVERT(CHAR(4),YEAR(GETDATE()))+CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(GETDATE()))),2)), '10000000', 2, @NroConductor, 0.00, 0.00, 'V', 'PG'+RIGHT('0000'+LTRIM(RTRIM(@NroVoucher)),4), @GastoTotal, CONVERT(MONEY,@GastoTotal / @TipoCambio),
			GETDATE(), '010201', '1413002', 'AE-TRUJ-'+CONVERT(VARCHAR(10),@Nro), 'GASTOS DE VIAJE - PL '+@CodGasto, 'BTRU', 0.00)
	
	IF (@MesActual = 1) BEGIN
		UPDATE accountbalance SET dollar01 = dollar01 - CONVERT(MONEY,@GastoTotal / @TipoCambio), local01 = local01 - @GastoTotal, dollarbal01 = dollarbal01 - CONVERT(MONEY,@GastoTotal / @TipoCambio), localbal01 = localbal01 - @GastoTotal
		WHERE companyowner = '10000000' AND ledger = '00' AND year = CONVERT(CHAR(4),YEAR(GETDATE())) AND account = '1020005' AND Location = '9999' 
		UPDATE accountbalance SET dollar01 = dollar01 + CONVERT(MONEY,@GastoTotal / @TipoCambio), local01 = local01 + @GastoTotal, dollarbal01 = dollarbal01 + CONVERT(MONEY,@GastoTotal / @TipoCambio), localbal01 = localbal01 + @GastoTotal
		WHERE companyowner = '10000000' AND ledger = '00' AND year = CONVERT(CHAR(4),YEAR(GETDATE())) AND account = '1413002' AND Location = '9999' 
	END
	IF (@MesActual = 2) BEGIN
		UPDATE accountbalance SET dollar02 = dollar02 - CONVERT(MONEY,@GastoTotal / @TipoCambio), local02 = local02 - @GastoTotal, dollarbal02 = dollarbal02 - CONVERT(MONEY,@GastoTotal / @TipoCambio), localbal02 = localbal02 - @GastoTotal
		WHERE companyowner = '10000000' AND ledger = '00' AND year = CONVERT(CHAR(4),YEAR(GETDATE())) AND account = '1020005' AND Location = '9999' 
		UPDATE accountbalance SET dollar02 = dollar02 + CONVERT(MONEY,@GastoTotal / @TipoCambio), local02 = local02 + @GastoTotal, dollarbal02 = dollarbal02 + CONVERT(MONEY,@GastoTotal / @TipoCambio), localbal02 = localbal02 + @GastoTotal
		WHERE companyowner = '10000000' AND ledger = '00' AND year = CONVERT(CHAR(4),YEAR(GETDATE())) AND account = '1413002' AND Location = '9999' 
	END
	IF (@MesActual = 3) BEGIN
		UPDATE accountbalance SET dollar03 = dollar03 - CONVERT(MONEY,@GastoTotal / @TipoCambio), local03 = local03 - @GastoTotal, dollarbal03 = dollarbal03 - CONVERT(MONEY,@GastoTotal / @TipoCambio), localbal03 = localbal03 - @GastoTotal
		WHERE companyowner = '10000000' AND ledger = '00' AND year = CONVERT(CHAR(4),YEAR(GETDATE())) AND account = '1020005' AND Location = '9999' 
		UPDATE accountbalance SET dollar03 = dollar03 + CONVERT(MONEY,@GastoTotal / @TipoCambio), local03 = local03 + @GastoTotal, dollarbal03 = dollarbal03 + CONVERT(MONEY,@GastoTotal / @TipoCambio), localbal03 = localbal03 + @GastoTotal
		WHERE companyowner = '10000000' AND ledger = '00' AND year = CONVERT(CHAR(4),YEAR(GETDATE())) AND account = '1413002' AND Location = '9999' 
	END
	IF (@MesActual = 4) BEGIN
		UPDATE accountbalance SET dollar04 = dollar04 - CONVERT(MONEY,@GastoTotal / @TipoCambio), local04 = local04 - @GastoTotal, dollarbal04 = dollarbal04 - CONVERT(MONEY,@GastoTotal / @TipoCambio), localbal04 = localbal04 - @GastoTotal
		WHERE companyowner = '10000000' AND ledger = '00' AND year = CONVERT(CHAR(4),YEAR(GETDATE())) AND account = '1020005' AND Location = '9999' 
		UPDATE accountbalance SET dollar04 = dollar04 + CONVERT(MONEY,@GastoTotal / @TipoCambio), local04 = local04 + @GastoTotal, dollarbal04 = dollarbal04 + CONVERT(MONEY,@GastoTotal / @TipoCambio), localbal04 = localbal04 + @GastoTotal
		WHERE companyowner = '10000000' AND ledger = '00' AND year = CONVERT(CHAR(4),YEAR(GETDATE())) AND account = '1413002' AND Location = '9999' 
	END
	IF (@MesActual = 5) BEGIN
		UPDATE accountbalance SET dollar05 = dollar05 - CONVERT(MONEY,@GastoTotal / @TipoCambio), local05 = local05 - @GastoTotal, dollarbal05 = dollarbal05 - CONVERT(MONEY,@GastoTotal / @TipoCambio), localbal05 = localbal05 - @GastoTotal
		WHERE companyowner = '10000000' AND ledger = '00' AND year = CONVERT(CHAR(4),YEAR(GETDATE())) AND account = '1020005' AND Location = '9999' 
		UPDATE accountbalance SET dollar05 = dollar05 + CONVERT(MONEY,@GastoTotal / @TipoCambio), local05 = local05 + @GastoTotal, dollarbal05 = dollarbal05 + CONVERT(MONEY,@GastoTotal / @TipoCambio), localbal05 = localbal05 + @GastoTotal
		WHERE companyowner = '10000000' AND ledger = '00' AND year = CONVERT(CHAR(4),YEAR(GETDATE())) AND account = '1413002' AND Location = '9999' 
	END
	IF (@MesActual = 6) BEGIN
		UPDATE accountbalance SET dollar06 = dollar06 - CONVERT(MONEY,@GastoTotal / @TipoCambio), local06 = local06 - @GastoTotal, dollarbal06 = dollarbal06 - CONVERT(MONEY,@GastoTotal / @TipoCambio), localbal06 = localbal06 - @GastoTotal
		WHERE companyowner = '10000000' AND ledger = '00' AND year = CONVERT(CHAR(4),YEAR(GETDATE())) AND account = '1020005' AND Location = '9999' 
		UPDATE accountbalance SET dollar06 = dollar06 + CONVERT(MONEY,@GastoTotal / @TipoCambio), local06 = local06 + @GastoTotal, dollarbal06 = dollarbal06 + CONVERT(MONEY,@GastoTotal / @TipoCambio), localbal06 = localbal06 + @GastoTotal
		WHERE companyowner = '10000000' AND ledger = '00' AND year = CONVERT(CHAR(4),YEAR(GETDATE())) AND account = '1413002' AND Location = '9999' 
	END
	IF (@MesActual = 7) BEGIN
		UPDATE accountbalance SET dollar07 = dollar07 - CONVERT(MONEY,@GastoTotal / @TipoCambio), local07 = local07 - @GastoTotal, dollarbal07 = dollarbal07 - CONVERT(MONEY,@GastoTotal / @TipoCambio), localbal07 = localbal07 - @GastoTotal
		WHERE companyowner = '10000000' AND ledger = '00' AND year = CONVERT(CHAR(4),YEAR(GETDATE())) AND account = '1020005' AND Location = '9999' 
		UPDATE accountbalance SET dollar07 = dollar07 + CONVERT(MONEY,@GastoTotal / @TipoCambio), local07 = local07 + @GastoTotal, dollarbal07 = dollarbal07 + CONVERT(MONEY,@GastoTotal / @TipoCambio), localbal07 = localbal07 + @GastoTotal
		WHERE companyowner = '10000000' AND ledger = '00' AND year = CONVERT(CHAR(4),YEAR(GETDATE())) AND account = '1413002' AND Location = '9999' 
	END
	IF (@MesActual = 8) BEGIN
		UPDATE accountbalance SET dollar08 = dollar08 - CONVERT(MONEY,@GastoTotal / @TipoCambio), local08 = local08 - @GastoTotal, dollarbal08 = dollarbal08 - CONVERT(MONEY,@GastoTotal / @TipoCambio), localbal08 = localbal08 - @GastoTotal
		WHERE companyowner = '10000000' AND ledger = '00' AND year = CONVERT(CHAR(4),YEAR(GETDATE())) AND account = '1020005' AND Location = '9999' 
		UPDATE accountbalance SET dollar08 = dollar08 + CONVERT(MONEY,@GastoTotal / @TipoCambio), local08 = local08 + @GastoTotal, dollarbal08 = dollarbal08 + CONVERT(MONEY,@GastoTotal / @TipoCambio), localbal08 = localbal08 + @GastoTotal
		WHERE companyowner = '10000000' AND ledger = '00' AND year = CONVERT(CHAR(4),YEAR(GETDATE())) AND account = '1413002' AND Location = '9999' 
	END
	IF (@MesActual = 9) BEGIN
		UPDATE accountbalance SET dollar09 = dollar09 - CONVERT(MONEY,@GastoTotal / @TipoCambio), local09 = local09 - @GastoTotal, dollarbal09 = dollarbal09 - CONVERT(MONEY,@GastoTotal / @TipoCambio), localbal09 = localbal09 - @GastoTotal
		WHERE companyowner = '10000000' AND ledger = '00' AND year = CONVERT(CHAR(4),YEAR(GETDATE())) AND account = '1020005' AND Location = '9999' 
		UPDATE accountbalance SET dollar09 = dollar09 + CONVERT(MONEY,@GastoTotal / @TipoCambio), local09 = local09 + @GastoTotal, dollarbal09 = dollarbal09 + CONVERT(MONEY,@GastoTotal / @TipoCambio), localbal09 = localbal09 + @GastoTotal
		WHERE companyowner = '10000000' AND ledger = '00' AND year = CONVERT(CHAR(4),YEAR(GETDATE())) AND account = '1413002' AND Location = '9999' 
	END
	IF (@MesActual = 10) BEGIN
		UPDATE accountbalance SET dollar10 = dollar10 - CONVERT(MONEY,@GastoTotal / @TipoCambio), local10 = local10 - @GastoTotal, dollarbal10 = dollarbal10 - CONVERT(MONEY,@GastoTotal / @TipoCambio), localbal10 = localbal10 - @GastoTotal
		WHERE companyowner = '10000000' AND ledger = '00' AND year = CONVERT(CHAR(4),YEAR(GETDATE())) AND account = '1020005' AND Location = '9999' 
		UPDATE accountbalance SET dollar10 = dollar10 + CONVERT(MONEY,@GastoTotal / @TipoCambio), local10 = local10 + @GastoTotal, dollarbal10 = dollarbal10 + CONVERT(MONEY,@GastoTotal / @TipoCambio), localbal10 = localbal10 + @GastoTotal
		WHERE companyowner = '10000000' AND ledger = '00' AND year = CONVERT(CHAR(4),YEAR(GETDATE())) AND account = '1413002' AND Location = '9999' 
	END
	IF (@MesActual = 11) BEGIN
		UPDATE accountbalance SET dollar11 = dollar11 - CONVERT(MONEY,@GastoTotal / @TipoCambio), local11 = local11 - @GastoTotal, dollarbal11 = dollarbal11 - CONVERT(MONEY,@GastoTotal / @TipoCambio), localbal11 = localbal11 - @GastoTotal
		WHERE companyowner = '10000000' AND ledger = '00' AND year = CONVERT(CHAR(4),YEAR(GETDATE())) AND account = '1020005' AND Location = '9999' 
		UPDATE accountbalance SET dollar11 = dollar11 + CONVERT(MONEY,@GastoTotal / @TipoCambio), local11 = local11 + @GastoTotal, dollarbal11 = dollarbal11 + CONVERT(MONEY,@GastoTotal / @TipoCambio), localbal11 = localbal11 + @GastoTotal
		WHERE companyowner = '10000000' AND ledger = '00' AND year = CONVERT(CHAR(4),YEAR(GETDATE())) AND account = '1413002' AND Location = '9999' 
	END
	IF (@MesActual = 12) BEGIN
		UPDATE accountbalance SET dollar12 = dollar12 - CONVERT(MONEY,@GastoTotal / @TipoCambio), local12 = local12 - @GastoTotal, dollarbal12 = dollarbal12 - CONVERT(MONEY,@GastoTotal / @TipoCambio), localbal12 = localbal12 - @GastoTotal
		WHERE companyowner = '10000000' AND ledger = '00' AND year = CONVERT(CHAR(4),YEAR(GETDATE())) AND account = '1020005' AND Location = '9999' 
		UPDATE accountbalance SET dollar12 = dollar12 + CONVERT(MONEY,@GastoTotal / @TipoCambio), local12 = local12 + @GastoTotal, dollarbal12 = dollarbal12 + CONVERT(MONEY,@GastoTotal / @TipoCambio), localbal12 = localbal12 + @GastoTotal
		WHERE companyowner = '10000000' AND ledger = '00' AND year = CONVERT(CHAR(4),YEAR(GETDATE())) AND account = '1413002' AND Location = '9999' 
	END
	
	UPDATE voucherheader SET status = 'PR' WHERE period = CONVERT(CHAR(4),YEAR(GETDATE()))+CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(GETDATE()))),2)) AND companyowner = '10000000' AND voucherno = 'PG'+RIGHT('0000'+LTRIM(RTRIM(@NroVoucher)),4)
	
	UPDATE voucherdetail SET postedamountlocal = -@GastoTotal, postedamountdollar = -CONVERT(MONEY,@GastoTotal / @TipoCambio) WHERE period = CONVERT(CHAR(4),YEAR(GETDATE()))+CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(GETDATE()))),2))
	AND companyowner = '10000000' AND voucherno = 'PG'+RIGHT('0000'+LTRIM(RTRIM(@NroVoucher)),4) AND voucherline = 1 
	UPDATE voucherdetail SET postedamountlocal = @GastoTotal, postedamountdollar = CONVERT(MONEY,@GastoTotal / @TipoCambio) WHERE period = CONVERT(CHAR(4),YEAR(GETDATE()))+CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(GETDATE()))),2))
	AND companyowner = '10000000' AND voucherno = 'PG'+RIGHT('0000'+LTRIM(RTRIM(@NroVoucher)),4) AND voucherline = 2 
	
	UPDATE Pagos SET VoucherPago = CONVERT(CHAR(4),YEAR(GETDATE()))+CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(GETDATE()))),2))+'PG'+RIGHT('0000'+LTRIM(RTRIM(@NroVoucher)),4), ContabilizacionPendiente = 'N' WHERE NumeroProceso = @Nro3 AND Secuencia = 1 
	
	--CREAR PLANTILLA PARA IMPRIMIR
	UPDATE ReportesApp_Operacion_Previaje_Registros
	SET Planilla = @CodGasto
	WHERE NroTicket = @NroTicket
	
	SET @NombreUsuario = (SELECT TOP 1 busqueda FROM Obligaciones, PersonaMast WHERE Obligaciones.IngresadoPor = PersonaMast.Persona AND Obligaciones.NumeroProceso = @Nro3 AND Obligaciones.ProcesoSecuencia = 1)
	
	INSERT INTO ReportesApp_Operaciones_TicketGasto_Recibo(PrepagoNo,FechaPago,HoraPago,TipoCambio,NroPago,NombreConductor,BancoConductor,CuentaConductor,
														   MontoPago,MontoExtranjero,NroDocumento,DocInterno,NroRegistro,Comentarios,NombreUsuario)
	SELECT @Nro3, CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME(1)), @TipoCambio, @UltimoNroPago, @NombreConductor, @BancoConductor, @CuentaConductor, NetoMonedaLocal, CONVERT(DECIMAL(10,2),@GastoTotal / @TipoCambio),
	TipoDocumento+'-'+NumeroDocumento, NumeroDocumentoInterno, RegistroNumero, Comentarios, @NombreUsuario
	FROM Obligaciones WHERE (NumeroProceso = @Nro3) AND (ProcesoSecuencia = 1)
	*/
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
SELECT @exito exito

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 25-07-2023
-- Description:	LISTAR REGISTRO DE GASTO X RUTA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ListarRegistro]
@NroTicket INT
AS
BEGIN
	SET NOCOUNT ON;
    SELECT TOP(1) TG.CodGasto, LTRIM(RTRIM(C.Nombre)) AS 'CONDUCTOR', O.Descripcion AS 'PROGRAMACION', V.NumeroPlaca AS 'TRACTO',
	R.NumeroPlaca AS 'SEMIRREMOLQUE', RT.Descripcion AS 'RUTA', LTRIM(RTRIM(CL.Busqueda)) AS 'CLIENTE', F.FechaProgramacion AS 'FECHA_VIAJE',
	GR.TotalDias AS 'DIAS', T.Descripcion AS 'TIPO_GASTO', GD.Gasto AS 'GASTO', TG.TotalEntregado AS 'GASTO_TOTAL',
	ISNULL(TG.GastoDiferencial,0) AS 'GASTO_DIFERENCIAL', ISNULL(TG.GastoDiferencialV,0) AS 'VIATICOS', TG.Observacion AS 'OBSERVACION'
    FROM ReportesApp_Operaciones_TicketGasto_Registro TG
    LEFT JOIN ReportesApp_Operacion_Previaje_Registros F WITH(NOLOCK) ON F.NroTicket = TG.NroTicket
    LEFT JOIN OP_TR_Conductor C WITH(NOLOCK) ON C.IdConductor = TG.ViConductor
    LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = F.idTracto
	LEFT JOIN OP_TR_Vehiculo R WITH(NOLOCK) ON R.IdVehiculo = F.idSemirremolque
    LEFT JOIN OP_TR_RUTA RT WITH(NOLOCK) ON RT.IDRUTA = F.IdRuta
    LEFT JOIN PersonaMast CL WITH(NOLOCK) ON CL.Persona = F.IdCliente
    LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera GR WITH(NOLOCK) ON GR.idGastoxRutaC = TG.idGastoXRutaC
    LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle GD WITH(NOLOCK) ON GD.idGastoxRutaC = TG.idGastoXRutaC
    LEFT JOIN ReportesApp_Operaciones_TicketGasto_TipoGasto T WITH(NOLOCK) ON T.idTipoGasto = GD.idTipoGasto
	LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = F.TipoProgramacion
    WHERE TG.NroTicket = @NroTicket AND TG.EstadoLiquidacion = 0
	ORDER BY TG.FechaCreacion DESC
END

------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 27-07-2023
-- Description:	BUSCAR CONDUCTORES ACTIVOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_BuscarConductores]
@Filtro VARCHAR(100)
AS
BEGIN
	SELECT IdConductor AS 'Codigo', Nombre FROM OP_TR_Conductor
	WITH(NOLOCK) WHERE ((Estado = 'A') AND (Nombre LIKE '%' + @Filtro + '%'))
END

------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 27-07-2023
-- Description:	BUSCAR VIAJES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_BuscarViajes]
@IdConductor INT,
@Ruta VARCHAR(250)
AS
BEGIN
	SELECT R.NroTicket AS 'Programacion', R.FechaProgramacion AS 'Fecha', h.NumeroPlaca AS 'Placa', V.Descripcion AS 'Ruta'
	FROM ReportesApp_Operacion_Previaje_Registros R
	INNER JOIN OP_TR_Conductor c WITH(NOLOCK) ON R.IdConductor = c.IdConductor
	INNER JOIN OP_TR_Vehiculo h WITH(NOLOCK) ON h.IdVehiculo = R.idTracto
	INNER JOIN OP_TR_Ruta V WITH(NOLOCK) ON r.IdRuta = v.IdRuta
	WHERE R.Estado = 1
	--WHERE YEAR(R.FechaProgramacion) > YEAR(DATEADD(YEAR,-1,GETDATE()))
	AND R.IdConductor = @IdConductor AND (@Ruta IS NULL OR V.Descripcion LIKE '%' + @Ruta + '%')
	ORDER BY R.NroTicket DESC
END

------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 27-07-2023
-- Description:	BUSCAR REGISTROS SIN LIQUIDAR
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_BuscarPlanillas]
@Programacion INT
AS
BEGIN
	SELECT TG.CodGasto AS 'Planilla', TG.NroTicket AS 'Programacion', TG.TotalEntregado, GR.TotalDias AS 'Dias', TG.FechaCreacion
	FROM ReportesApp_Operaciones_TicketGasto_Registro TG
	LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera GR WITH(NOLOCK) ON GR.idGastoxRutaC = TG.idGastoXRutaC
	WHERE TG.EstadoLiquidacion = 0 AND TG.NroTicket = @Programacion
END

------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 31-07-2023
-- Description:	LISTAR CONCEPTOS DE GASTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ListarConceptosGasto]
@Accion INT,
@CodGasto VARCHAR(50)
AS
BEGIN
	IF @Accion = 1 BEGIN     --LISTAR TIPOS DE GASTO
		SELECT * FROM ReportesApp_Operaciones_TicketGasto_ConceptoGasto
		WHERE ConceptoGasto NOT IN ('0062','0003','DVP')
	END
	
	IF @Accion = 2 BEGIN     --LISTAR TIPOS DE DOCUMENTOS
		SELECT CodigoDocumento, Descripcion FROM ReportesApp_Operaciones_TicketGasto_TipoDocumento
		ORDER BY idTipoDocumento 
	END
	
	IF @Accion = 3 BEGIN     --LISTAR RECIBOS INGRESADOS
		SELECT L.idNroLiquidacion AS 'Nro', L.CodGasto AS 'PLANILLA', L.FechaLiquidacion AS 'FECHA_EMISION', L.TipoImpuesto, (CASE WHEN L.TipoImpuesto = 'I' THEN 'COMPRA' ELSE 'NINGUNO' END) AS 'TIPO_IMPUESTO',
			   L.ConceptoGasto, CG.DescripcionLocal AS 'CONCEPTO_GASTO', L.DescripcionGasto AS 'DESCRIPCION', L.NroRUC AS 'RUC', L.NombreCompleto AS 'NOMBRE_COMPLETO', L.CodigoDocumento, L.NroDocumento AS 'COMPROBANTE',
			   L.MontoAfecto AS 'MONTO_AFECTO', L.MontoNoAfecto AS 'MONTO_NO_AFECTO', L.MontoImpuestos AS 'MONTO_IMPUESTOS', L.MontoPagado AS 'MONTO_PAGADO', L.Contador
		FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_ConceptoGasto CG ON L.ConceptoGasto = CG.ConceptoGasto
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_TipoDocumento D ON L.CodigoDocumento = D.CodigoDocumento
		WHERE CodGasto = @CodGasto
		ORDER BY idNroLiquidacion DESC
	END
	
	IF @Accion = 4 BEGIN     --LISTAR TIPOS DE VIÁTICO
		SELECT * FROM ReportesApp_Operaciones_TicketGasto_TipoViaticos
		ORDER BY idTipoViatico
	END

	IF @Accion = 5 BEGIN     --ACTUALIZAR CORRELATIVO - COMPROBANTES SIN SUSTENTO
		DECLARE @c1 INT
		SET @c1 = (SELECT NroCorrelativo FROM ReportesApp_Operaciones_TicketGasto_Correlativo WHERE idCorrelativo = 1 AND Anio = YEAR(GETDATE()))
		SET @c1 = ISNULL(@c1,0) + 1
		
		UPDATE ReportesApp_Operaciones_TicketGasto_Correlativo
		SET NroCorrelativo = @c1, Anio = YEAR(GETDATE())
		WHERE idCorrelativo = 1

		UPDATE ReportesApp_Operaciones_TicketGasto_Correlativo
		SET Codigo = CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(NroCorrelativo)),6)) + '-' + SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2)
		WHERE idCorrelativo = 1

		SELECT Codigo FROM ReportesApp_Operaciones_TicketGasto_Correlativo WHERE idCorrelativo = 1
	END

	IF @Accion = 6 BEGIN     --ACTUALIZAR CORRELATIVO - TICKET DESPACHO
		DECLARE @c2 INT
		SET @c2 = (SELECT NroCorrelativo FROM ReportesApp_Operaciones_TicketGasto_Correlativo WHERE idCorrelativo = 2 AND Anio = YEAR(GETDATE()))
		SET @c2 = ISNULL(@c2,0) + 1
		
		UPDATE ReportesApp_Operaciones_TicketGasto_Correlativo
		SET NroCorrelativo = @c2, Anio = YEAR(GETDATE())
		WHERE idCorrelativo = 2

		UPDATE ReportesApp_Operaciones_TicketGasto_Correlativo
		SET Codigo = CONVERT(INT,SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@c2)),6)))
		WHERE idCorrelativo = 2

		SELECT Codigo FROM ReportesApp_Operaciones_TicketGasto_Correlativo WHERE idCorrelativo = 2
	END

	IF @Accion= 7 BEGIN		--LISTAR PEAJES
		SELECT GP.idPeaje, P.Descripcion AS 'Peaje'
		FROM ReportesApp_Operaciones_TicketGasto_GastoPeaje GP
		LEFT JOIN OP_TR_Peaje P ON GP.IdPeaje = P.IdPeaje
		WHERE GP.Monto > 30
		ORDER BY P.Descripcion
	END
END

------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 31-07-2023
-- Description:	LISTAR NOMBRE DE RUC
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ListarNombreRUC]
@NroRUC VARCHAR(200)
AS
BEGIN
	SELECT TOP(40) LTRIM(RTRIM(Persona)) AS Persona, LTRIM(RTRIM(DocumentoFiscal)) AS DocumentoFiscal, LTRIM(RTRIM(NombreCompleto)) AS NombreCompleto FROM PersonaMast
	WHERE ((DocumentoFiscal LIKE '%' + @NroRuc + '%') OR (NombreCompleto LIKE '%' + @NroRuc + '%')) AND (Estado = 'A')
END

------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 01-08-2023
-- Description:	INSERTAR BOLETAS LIQUIDACIONES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_InsertarLiquidaciones]
@CodGasto VARCHAR(50),
@FechaLiquidacion DATETIME,
@TipoImpuesto CHAR(5),
@ConceptoGasto CHAR(5),
@DescripcionGasto VARCHAR(200),
@NroRUC CHAR(20),
@NombreCompleto VARCHAR(350),
@CodigoDocumento CHAR(10),
@NroDocumento VARCHAR(50),
@MontoAfecto DECIMAL(10,2),
@MontoNoAfecto DECIMAL(10,2),
@MontoImpuestos DECIMAL(10,2),
@MontoPagado DECIMAL(10,2)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT
DECLARE @NroComprobante INT

SET @Exito = '0 = Exito.'

SET @correlativo = (SELECT MAX(idNroLiquidacion) FROM ReportesApp_Operaciones_TicketGasto_Liquidacion WHERE CodGasto = @CodGasto)
SET @correlativo = ISNULL(@correlativo,0) + 1

IF(@NroDocumento != '                    ') BEGIN
	IF(EXISTS(SELECT * FROM ReportesApp_Operaciones_TicketGasto_Liquidacion WHERE NroDocumento = @NroDocumento AND NroRUC = @NroRUC AND @ConceptoGasto = '0002 ')) BEGIN
		SET @Exito = '-1 = Este comprobante ya fue ingresado en la Planilla: PL-' + (SELECT CodGasto FROM ReportesApp_Operaciones_TicketGasto_Liquidacion WHERE NroDocumento = @NroDocumento)
		GOTO Terminar
	END
END

IF (@ConceptoGasto = '0078 ') BEGIN
	SET @NroComprobante = (SELECT MAX(NroCorrelativo) FROM ReportesApp_Operaciones_TicketGasto_Correlativo WHERE Anio = YEAR(GETDATE()))
	SET @NroComprobante = ISNULL(@NroComprobante,0) + 1

	UPDATE ReportesApp_Operaciones_TicketGasto_Correlativo
	SET NroCorrelativo = @NroComprobante, Anio = YEAR(GETDATE()), Codigo = CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@NroComprobante)),6)) + '-' + SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2)
END

BEGIN TRAN
BEGIN TRY
	IF (@ConceptoGasto = '0078 ') BEGIN
		INSERT INTO ReportesApp_Operaciones_TicketGasto_Liquidacion(idNroLiquidacion,CodGasto,FechaLiquidacion,TipoImpuesto,ConceptoGasto,DescripcionGasto,NroRUC,NombreCompleto,
				CodigoDocumento,NroDocumento,MontoAfecto,MontoNoAfecto,MontoImpuestos,MontoPagado, Contador)
		VALUES(@correlativo,@CodGasto,@FechaLiquidacion,@TipoImpuesto,@ConceptoGasto,@DescripcionGasto,@NroRUC,@NombreCompleto,@CodigoDocumento,@NroDocumento,@MontoAfecto,@MontoNoAfecto,
				@MontoImpuestos,@MontoPagado, CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@NroComprobante)),6)) + '-' + SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2))
	END
	ELSE BEGIN
		INSERT INTO ReportesApp_Operaciones_TicketGasto_Liquidacion(idNroLiquidacion,CodGasto,FechaLiquidacion,TipoImpuesto,ConceptoGasto,DescripcionGasto,NroRUC,NombreCompleto,
				CodigoDocumento,NroDocumento,MontoAfecto,MontoNoAfecto,MontoImpuestos,MontoPagado)
		VALUES(@correlativo,@CodGasto,@FechaLiquidacion,@TipoImpuesto,@ConceptoGasto,@DescripcionGasto,@NroRUC,@NombreCompleto,@CodigoDocumento,@NroDocumento,@MontoAfecto,@MontoNoAfecto,
				@MontoImpuestos,@MontoPagado)
	END
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
SELECT @exito exito

------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 01-08-2023
-- Description:	ELIMINAR LIQUIDACION
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_EliminarLiquidaciones]
@idNroLiquidacion INT,
@CodGasto VARCHAR(50)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @CountMax INT

SET @Exito = '0 = Recibo eliminado.'

IF (@idNroLiquidacion = 0)
BEGIN
	SET @Exito = '-1 = El recibo seleccionado no existe.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	IF ((SELECT LTRIM(RTRIM(ConceptoGasto)) FROM ReportesApp_Operaciones_TicketGasto_Liquidacion WHERE idNroLiquidacion = @idNroLiquidacion AND CodGasto = @CodGasto) = '0002') BEGIN
		DELETE FROM ReportesApp_Operaciones_TicketGasto_ListaViaticos
		WHERE Comprobante = (SELECT NroDocumento FROM ReportesApp_Operaciones_TicketGasto_Liquidacion WHERE idNroLiquidacion = @idNroLiquidacion AND CodGasto = @CodGasto)
	END

	IF ((SELECT LTRIM(RTRIM(ConceptoGasto)) FROM ReportesApp_Operaciones_TicketGasto_Liquidacion WHERE idNroLiquidacion = @idNroLiquidacion AND CodGasto = @CodGasto) = '0107') BEGIN
		DELETE FROM ReportesApp_Operaciones_TicketGasto_ListaViaticos
		WHERE Comprobante = (SELECT NroDocumento FROM ReportesApp_Operaciones_TicketGasto_Liquidacion WHERE idNroLiquidacion = @idNroLiquidacion AND CodGasto = @CodGasto)
		AND Planilla = @CodGasto
	END

	DELETE FROM ReportesApp_Operaciones_TicketGasto_Liquidacion
	WHERE idNroLiquidacion = @idNroLiquidacion AND CodGasto = @CodGasto
	
	UPDATE ReportesApp_Operaciones_TicketGasto_Liquidacion
	SET idNroLiquidacion = idNroLiquidacion - 1
	WHERE idNroLiquidacion > @idNroLiquidacion AND CodGasto = @CodGasto
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
SELECT @exito exito

------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 02-08-2023
-- Description:	LIQUIDAR TICKETS DE GASTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_LiquidarTicketGasto]
@idConductor INT,
@Programacion INT,
@Placa VARCHAR(15),
@FechaLiquidacion DATETIME,
@Planilla INT,
@Gasto DECIMAL(10,2),
@Total DECIMAL(10,2),
@Reintegro DECIMAL(10,2),
@DescripcionRG VARCHAR(200),
@Usuario VARCHAR(100)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @Nro INT
DECLARE @NroRP INT
DECLARE @NroConductor INT
DECLARE @NroUsuario INT
DECLARE @NroAdelanto INT
DECLARE @MontoTotal MONEY
DECLARE @Proyecto VARCHAR(15)
DECLARE @TipoCambio MONEY
DECLARE @Persona INT
DECLARE @Contador INT
DECLARE @CuentaContable CHAR(20)
DECLARE @Devolucion DECIMAL(10,2)
DECLARE @Descuento DECIMAL(10,2)
DECLARE @Absoluto DECIMAL(10,2)
DECLARE @CodigoDocumento CHAR(10)
DECLARE @NombreConductor VARCHAR(350)
DECLARE @CuentaBancaria CHAR(15)
DECLARE @Sucursal CHAR(4)
DECLARE @Sucursal2 VARCHAR(4)
DECLARE @UnidadReplicacion CHAR(4)

SET @Exito = '0 = Comprobantes Liquidados Exitosamente.'

IF (@Reintegro > 0.00) BEGIN
	SET @Exito = '-1 = El saldo debe cuadrar con el gasto.'
	GOTO Terminar
END

IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_TicketGasto_UsuarioSucursal WHERE Usuario = @Usuario AND Estado = 'A')) BEGIN
	SET @CuentaBancaria = (SELECT CuentaBancaria FROM ReportesApp_Operaciones_TicketGasto_UsuarioSucursal WHERE Usuario = @Usuario)
	SET @Sucursal = (SELECT Sucursal FROM ReportesApp_Operaciones_TicketGasto_UsuarioSucursal WHERE Usuario = @Usuario)
	SET @Sucursal2 = (SELECT CONVERT(VARCHAR(4),Sucursal) FROM ReportesApp_Operaciones_TicketGasto_UsuarioSucursal WHERE Usuario = @Usuario)
	SET @UnidadReplicacion = (SELECT TOP(1) UnidadReplicacion FROM AP_GastoAdelanto WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Descripcion LIKE '%' + 'GASTOS DE VIAJE ' + '%' AND Estado != 'AN')
END
ELSE BEGIN
	SET @Exito = '-1 = Este usuario está inactivo en Tabla Sucursales o no tiene una sede registrada. Revisar con Sistemas'
	GOTO Terminar
END

--Se colocó control AlcoholTest: 08-03-2024.
-------------------------------------------
IF (NOT EXISTS(SELECT TOP 1(1) FROM ReportesApp_Seguridad_AlcoholTest_Registro WHERE idPersona = @Persona and cast(Fecha as date)=cast(getdate() as date)) AND (SELECT TOP 1 Estado FROM PersonaMast where Persona=@Persona)='A' ) BEGIN
	SET @Exito = '-2 = La persona a la que liquida, debe pasar su control Alcohol Test. No puede concluir el proceso. Enviarla a Garita.'
	GOTO Terminar
END

IF (EXISTS(SELECT * FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Estado NOT IN ('AN','PA'))) BEGIN
	DECLARE @AdelantosPendientes INT = (SELECT COUNT(*) FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Estado NOT IN ('AN','PA'))
	
	SET @Exito = '-2 = La planilla '+CONVERT(VARCHAR(30),@Planilla)+' contiene '+ CONVERT(VARCHAR,@AdelantosPendientes) +' adelanto(s) que aún no se han pagado.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	SET @Placa = LTRIM(RTRIM(@Placa))
	
	--ACTUALIZAR CORRELATIVO DE REPORTES DE GASTO
	UPDATE CorrelativosMast
	SET CorrelativoNumero = (SELECT CorrelativoNumero + 1 FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie = 'APER')),
		UltimaFechaModif = GETDATE()
	WHERE (CorrelativosMast.CompaniaCodigo = '999999') AND (CorrelativosMast.TipoComprobante = 'SY') AND (CorrelativosMast.Serie = 'APER')
	SET @NroRP = (SELECT CorrelativosMast.CorrelativoNumero FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo = '999999') AND (CorrelativosMast.TipoComprobante = 'SY') AND (CorrelativosMast.Serie = 'APER'))
	
	DELETE FROM AP_CajaChicaDistribucion WHERE (AP_CajaChicaDistribucion.CajaChicaReporteFlag='R') AND (AP_CajaChicaDistribucion.UnidadReplicacion=@UnidadReplicacion) AND (AP_CajaChicaDistribucion.CajaChicaNumero=@NroRP)
	
	--INSERTAR DATOS EN CABECERA DE REPORTES DE GASTO
	/*SET @NroConductor = (SELECT P.Persona FROM ReportesApp_Operacion_Previaje_Registros R
						 LEFT JOIN OP_TR_Conductor C ON C.IdConductor = R.IdConductor
						 LEFT JOIN PersonaMast P ON P.Persona = C.IdPersona WHERE R.NroTicket = @Programacion)
	*/
	SET @NroConductor = (SELECT P.Persona FROM ReportesApp_Operaciones_TicketGasto_Registro R
						 LEFT JOIN OP_TR_Conductor C ON C.IdConductor = R.ViConductor
						 LEFT JOIN PersonaMast P ON P.Persona = C.IdPersona WHERE CONVERT(VARCHAR(30),@Planilla) = R.CodGasto)				 
	SET @NroUsuario = (SELECT P.Persona FROM Usuario U LEFT JOIN PersonaMast P ON P.NombreCompleto = U.Nombre WHERE Usuario = @Usuario)
	SET @NroAdelanto = (SELECT MIN(NumeroAdelanto) FROM AP_GastoAdelanto WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(20),@Planilla) AND Estado != 'AN')
	SET @MontoTotal = (SELECT SUM(MontoTotal) FROM AP_GastoAdelanto WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(20),@Planilla) AND Estado != 'AN')
	SET @Proyecto = (SELECT Proyecto FROM OP_TR_Vehiculo WHERE NumeroPlaca = @Placa AND Estado = 2)

	INSERT INTO AP_CajaChica (UnidadReplicacion,CajaChicaNumero,CompaniaSocio,MonedaPago,MontoTotal,Descripcion,Beneficiario,PreparadoPor,FechaPreparacion,UltimoUsuario,UltimaFechaModif,Clasificacion,DefaultAfe,
				CajaChicaReporteFlag,MontoAdelantos,MontoNeto,NumeroAdelanto,DefaultPrime,NumeroDocumentoInterno,PersonaPagara,UnidadNegocio,TipoPago,Estado,FlujodeCaja,DefaultCampoReferencia)
	SELECT @UnidadReplicacion, @NroRP, '10000000', 'LO', @Gasto, @DescripcionRG, @NroConductor, @NroUsuario, @FechaLiquidacion, @Usuario, GETDATE(), 'ER', @Proyecto, 'R', @MontoTotal, 0.00, @NroAdelanto, '010201', CONVERT(CHAR,@Planilla),
	@NroConductor, 'TRAN', 'CH', 'PR', '051', '90'
	
	--INSERTAR DATOS DE DETALLE DE REPORTES DE GASTO
	SET @TipoCambio = (CONVERT(MONEY,(SELECT TipoCambioMast.FactorVenta FROM TipoCambioMast WHERE TipoCambioMast.MonedaCodigo ='EX' AND TipoCambioMast.MonedaCambioCodigo ='LO' AND TipoCambioMast.FechaCambio =CONVERT(DATE,@FechaLiquidacion))))
	SET @Devolucion = 0.00
	SET @Descuento = 0.00
	SET @Absoluto = 0.00
	SET @Contador = 1
	
	WHILE(@Contador <= (SELECT COUNT(idNroLiquidacion) FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla))) BEGIN
		IF((SELECT NroDocumento FROM ReportesApp_Operaciones_TicketGasto_Liquidacion WHERE CodGasto = @Planilla AND idNroLiquidacion = @Contador) != '') BEGIN
			SET @CodigoDocumento = (SELECT L.CodigoDocumento FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador)

			IF (@CodigoDocumento = 'OT') BEGIN
				SET @NombreConductor = (SELECT P.NombreCompleto FROM ReportesApp_Operaciones_TicketGasto_Registro R
										LEFT JOIN OP_TR_Conductor C ON C.IdConductor = R.ViConductor
										LEFT JOIN PersonaMast P ON P.Persona = C.IdPersona WHERE CONVERT(VARCHAR(30),@Planilla) = R.CodGasto)

				INSERT INTO AP_CajaChicaDetalle (UnidadReplicacion,CajaChicaNumero,Secuencia,ConceptoGasto,Fecha,Descripcion,MontoMonedaPago,TipoImpuestoFlag,MonedaDocumento,TipodeCambio,MontoAfecto,MontoNoAfecto,MontoImpuesto,
							MontoTotal,RegistroComprasPeriodo,FechaDocumento,CajaChicaReporteFlag,CampoReferencia,RetencionFlag,RetencionMonto,CentroCosto,MontoAFPAporte,MontoAFPComision,MontoAFPSeguro,TipoDocumento,NumeroDocumento,
							Proveedor, ProveedorNombre)
				SELECT @UnidadReplicacion,@NroRP,L.idNroLiquidacion,L.ConceptoGasto,L.FechaLiquidacion,L.DescripcionGasto,L.MontoPagado,L.TipoImpuesto,'LO',@TipoCambio,L.MontoAfecto,L.MontoNoAfecto,L.MontoImpuestos,L.MontoPagado - L.MontoImpuestos,'000000',
					    L.FechaLiquidacion, 'R', '90', 'N', 0.00, '010201', 0.00, 0.00, 0.00,L.CodigoDocumento,L.NroDocumento, @NroConductor, @NombreConductor
				FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador

				IF((SELECT L.ConceptoGasto FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador) = '0062') BEGIN
					SET @Descuento = (SELECT L.MontoPagado FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador)
				END
			END
			ELSE BEGIN
				IF((SELECT L.ConceptoGasto FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador) = '0107') BEGIN
					SET @NombreConductor = (SELECT P.NombreCompleto FROM ReportesApp_Operaciones_TicketGasto_Registro R
											LEFT JOIN OP_TR_Conductor C ON C.IdConductor = R.ViConductor
											LEFT JOIN PersonaMast P ON P.Persona = C.IdPersona WHERE CONVERT(VARCHAR(30),@Planilla) = R.CodGasto)

					INSERT INTO AP_CajaChicaDetalle (UnidadReplicacion,CajaChicaNumero,Secuencia,ConceptoGasto,Fecha,Descripcion,MontoMonedaPago,TipoImpuestoFlag,MonedaDocumento,TipodeCambio,MontoAfecto,MontoNoAfecto,MontoImpuesto,
								MontoTotal,RegistroComprasPeriodo,FechaDocumento,CajaChicaReporteFlag,CampoReferencia,RetencionFlag,RetencionMonto,CentroCosto,MontoAFPAporte,MontoAFPComision,MontoAFPSeguro, Proveedor, ProveedorNombre, NumeroDocumento)
					SELECT @UnidadReplicacion,@NroRP,L.idNroLiquidacion,L.ConceptoGasto,L.FechaLiquidacion,L.DescripcionGasto,L.MontoPagado,L.TipoImpuesto,'LO',@TipoCambio,L.MontoAfecto,L.MontoNoAfecto,L.MontoImpuestos,L.MontoPagado - L.MontoImpuestos,'000000',
						   L.FechaLiquidacion, 'R', '90', 'N', 0.00, '010201', 0.00, 0.00, 0.00, @NroConductor, @NombreConductor, L.NroDocumento
					FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador
				END
				ELSE BEGIN
					INSERT INTO AP_CajaChicaDetalle (UnidadReplicacion,CajaChicaNumero,Secuencia,ConceptoGasto,Fecha,Descripcion,MontoMonedaPago,TipoImpuestoFlag,DocumentoFiscal,Proveedor,TipoDocumento,MonedaDocumento,TipodeCambio,
								MontoAfecto,MontoNoAfecto,MontoImpuesto,MontoTotal,ProveedorNombre,RegistroComprasPeriodo,FechaDocumento,CajaChicaReporteFlag,NumeroDocumento,CampoReferencia,RetencionFlag,RetencionMonto,CentroCosto,
								MontoAFPAporte,MontoAFPComision,MontoAFPSeguro)
					SELECT @UnidadReplicacion,@NroRP,L.idNroLiquidacion,L.ConceptoGasto,L.FechaLiquidacion,L.DescripcionGasto,L.MontoPagado,L.TipoImpuesto,L.NroRUC,(SELECT Persona FROM PersonaMast WHERE NombreCompleto = L.NombreCompleto),
							L.CodigoDocumento,'LO',@TipoCambio,L.MontoAfecto,L.MontoNoAfecto,L.MontoImpuestos,L.MontoPagado - L.MontoImpuestos,L.NombreCompleto,'000000',L.FechaLiquidacion,'R',L.NroDocumento, '90', 'N', 0.00, '010201', 0.00, 0.00, 0.00
					FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador
				END
			END

			SET @CuentaContable = (SELECT CuentaContable FROM AP_ConceptoGasto WHERE ConceptoGasto = (SELECT L.ConceptoGasto FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador))
			
			IF ((SELECT L.MontoAfecto FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador) != 0) BEGIN
				INSERT INTO AP_CajaChicaDistribucion (CajaChicaReporteFlag,UnidadReplicacion,CajaChicaNumero,Secuencia,Linea,Afe,Monto,CuentaContable,CentroCosto,Sucursal,CampoReferencia,MontoTotal,ConceptoGasto)
				SELECT 'R', @UnidadReplicacion, @NroRP, idNroLiquidacion, 1, @Proyecto, L.MontoAfecto, @CuentaContable, '010201', @Sucursal, '90', L.MontoPagado, L.ConceptoGasto
				FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador
				SET @Contador = @Contador + 1
			END
			ELSE BEGIN
				INSERT INTO AP_CajaChicaDistribucion (CajaChicaReporteFlag,UnidadReplicacion,CajaChicaNumero,Secuencia,Linea,Afe,Monto,CuentaContable,CentroCosto,Sucursal,CampoReferencia,MontoTotal,ConceptoGasto)
				SELECT 'R', @UnidadReplicacion, @NroRP, idNroLiquidacion, 1, @Proyecto, L.MontoNoAfecto, @CuentaContable, '010201', @Sucursal, '90', L.MontoPagado, L.ConceptoGasto
				FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador
				SET @Contador = @Contador + 1
			END
		END
		ELSE BEGIN
			SET @CodigoDocumento = (SELECT L.CodigoDocumento FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador)
			SET @CuentaContable = (SELECT CuentaContable FROM AP_ConceptoGasto WHERE ConceptoGasto = (SELECT L.ConceptoGasto FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador))

			IF((SELECT L.ConceptoGasto FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador) = '0105') BEGIN
				SET @NombreConductor = (SELECT P.NombreCompleto FROM ReportesApp_Operaciones_TicketGasto_Registro R
										LEFT JOIN OP_TR_Conductor C ON C.IdConductor = R.ViConductor
										LEFT JOIN PersonaMast P ON P.Persona = C.IdPersona WHERE CONVERT(VARCHAR(30),@Planilla) = R.CodGasto)

				INSERT INTO AP_CajaChicaDetalle (UnidadReplicacion,CajaChicaNumero,Secuencia,ConceptoGasto,Fecha,Descripcion,MontoMonedaPago,TipoImpuestoFlag,MonedaDocumento,TipodeCambio,MontoAfecto,MontoNoAfecto,MontoImpuesto,
							MontoTotal,RegistroComprasPeriodo,FechaDocumento,CajaChicaReporteFlag,CampoReferencia,RetencionFlag,RetencionMonto,CentroCosto,MontoAFPAporte,MontoAFPComision,MontoAFPSeguro, Proveedor, ProveedorNombre)
				SELECT @UnidadReplicacion,@NroRP,L.idNroLiquidacion,L.ConceptoGasto,L.FechaLiquidacion,L.DescripcionGasto,L.MontoPagado,L.TipoImpuesto,'LO',@TipoCambio,L.MontoAfecto,L.MontoNoAfecto,L.MontoImpuestos,L.MontoPagado - L.MontoImpuestos,'000000',
					   L.FechaLiquidacion, 'R', '90', 'N', 0.00, '010201', 0.00, 0.00, 0.00, @NroConductor, @NombreConductor
				FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador
			END
			ELSE BEGIN
				INSERT INTO AP_CajaChicaDetalle (UnidadReplicacion,CajaChicaNumero,Secuencia,ConceptoGasto,Fecha,Descripcion,MontoMonedaPago,TipoImpuestoFlag,MonedaDocumento,TipodeCambio,MontoAfecto,MontoNoAfecto,MontoImpuesto,
							MontoTotal,RegistroComprasPeriodo,FechaDocumento,CajaChicaReporteFlag,CampoReferencia,RetencionFlag,RetencionMonto,CentroCosto,MontoAFPAporte,MontoAFPComision,MontoAFPSeguro)
				SELECT @UnidadReplicacion,@NroRP,L.idNroLiquidacion,L.ConceptoGasto,L.FechaLiquidacion,L.DescripcionGasto,L.MontoPagado,L.TipoImpuesto,'LO',@TipoCambio,L.MontoAfecto,L.MontoNoAfecto,L.MontoImpuestos,L.MontoPagado - L.MontoImpuestos,'000000',
					   L.FechaLiquidacion, 'R', '90', 'N', 0.00, '010201', 0.00, 0.00, 0.00
				FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador
			END

			IF((SELECT L.ConceptoGasto FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador) = '0078') BEGIN
				SET @Devolucion = (SELECT L.MontoPagado FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador)
			END

			INSERT INTO AP_CajaChicaDistribucion (CajaChicaReporteFlag,UnidadReplicacion,CajaChicaNumero,Secuencia,Linea,Afe,Monto,CuentaContable,CentroCosto,Sucursal,CampoReferencia,MontoTotal,ConceptoGasto)
			SELECT 'R', @UnidadReplicacion, @NroRP, idNroLiquidacion, 1, @Proyecto, L.MontoPagado, @CuentaContable, '010201', @Sucursal, '90', L.MontoPagado, L.ConceptoGasto
			FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador
			SET @Contador = @Contador + 1
		END
	END

	--INSERTAR GASTO DE ADELANTO
	IF ((SELECT COUNT(A.NumeroAdelanto) FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Estado != 'AN') > 1) BEGIN
		DECLARE @TABLA_ADELANTOS TABLE(Nro INT, NumeroAdelanto INT, MontoTotal DECIMAL(10,2), Descripcion VARCHAR(250), Persona INT, Estado VARCHAR(5))
		DECLARE @Contador2 INT = 1

		INSERT INTO @TABLA_ADELANTOS(Nro, NumeroAdelanto, MontoTotal, Descripcion, Persona, Estado)
		SELECT ROW_NUMBER() OVER(ORDER BY NumeroAdelanto ASC) AS 'NRO', NumeroAdelanto, MontoTotal, Descripcion, Persona, Estado
		FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Descripcion NOT LIKE 'GV PLLA ' + '%' AND Estado = 'PA'
		ORDER BY NumeroAdelanto ASC

		WHILE(@Contador2 <= (SELECT COUNT(Nro) FROM @TABLA_ADELANTOS)) BEGIN
			IF (EXISTS(SELECT * FROM @TABLA_ADELANTOS WHERE Nro = @Contador2 AND Descripcion LIKE '%' + 'GASTOS DE VIAJE ' + '%')) BEGIN
				INSERT INTO AP_GastoAdelantoPagos (UnidadReplicacion,TipoAdelanto,NumeroAdelanto,Secuencia,ObligacionPagoFlag,ObligacionProveedor,ObligacionTipoDocumento,ObligacionNumeroDocumento,Monto,Estado,UltimoUsuario,UltimaFechaModif)
				SELECT TOP(1) @UnidadReplicacion, 'E', A.NumeroAdelanto, 1, 'O', A.Persona, 'ER', @UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@NroRP), A.MontoTotal, 'PR', @Usuario, GETDATE()
				FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Descripcion LIKE '%' + 'GASTOS DE VIAJE ' + '%' AND Estado != 'AN'
			END
			ELSE BEGIN
				DECLARE @GastoDiferencial DECIMAL(10,2)
				DECLARE @AdelantoAdicional INT
				DECLARE @Persona2 INT
				
				SET @AdelantoAdicional = (SELECT NumeroAdelanto FROM @TABLA_ADELANTOS A WHERE Nro = @Contador2)
				SET @GastoDiferencial = (SELECT MontoTotal FROM @TABLA_ADELANTOS A WHERE Nro = @Contador2)
				SET @Persona2 = (SELECT Persona FROM @TABLA_ADELANTOS A WHERE Nro = @Contador2)

				UPDATE AP_CajaChica
				SET MontoTotal = @Gasto, MontoAdelantos = @Gasto, NumeroAdelanto = ISNULL(@AdelantoAdicional,NumeroAdelanto)
				WHERE CajaChicaNumero = @NroRP
			
				INSERT INTO AP_GastoAdelantoPagos (UnidadReplicacion,TipoAdelanto,NumeroAdelanto,Secuencia,ObligacionPagoFlag,ObligacionProveedor,ObligacionTipoDocumento,ObligacionNumeroDocumento,Monto,Estado,UltimoUsuario,UltimaFechaModif)
				SELECT @UnidadReplicacion, 'E', @AdelantoAdicional, 1, 'O', @Persona2, 'ER', @UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@NroRP), @GastoDiferencial, 'PR', @Usuario, GETDATE()
			END

			SET @Contador2 = @Contador2 + 1
		END

		/*
		DECLARE @GastoDiferencial DECIMAL(10,2)
		DECLARE @AdelantoAdicional INT

		IF (EXISTS(SELECT * FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Descripcion LIKE 'VIATICO ADICIONAL:' + '%' AND Estado != 'AN')) BEGIN
			SET @AdelantoAdicional = (SELECT TOP(1) NumeroAdelanto FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Descripcion LIKE 'VIATICO ADICIONAL:' + '%' AND Estado != 'AN')
			SET @GastoDiferencial = (SELECT TOP(1) MontoTotal FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Descripcion LIKE 'VIATICO ADICIONAL:' + '%' AND Estado != 'AN')
		END
		ELSE BEGIN
			SET @AdelantoAdicional = (SELECT TOP(1) NumeroAdelanto FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Descripcion LIKE 'REINTEGRO POR CAMBIO DE RUTA' + '%' AND Estado != 'AN')
			SET @GastoDiferencial = (SELECT TOP(1) MontoTotal FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Descripcion LIKE 'REINTEGRO POR CAMBIO DE RUTA' + '%' AND Estado != 'AN')
		END

		UPDATE AP_CajaChica
		SET MontoAdelantos = MontoAdelantos + @GastoDiferencial, NumeroAdelanto = ISNULL(@AdelantoAdicional,NumeroAdelanto)
		WHERE CajaChicaNumero = @NroRP

		IF (EXISTS(SELECT * FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Descripcion LIKE 'VIATICO ADICIONAL:' + '%' AND Estado != 'AN')) BEGIN
			UPDATE AP_GastoAdelantoPagos
			SET Monto = @Total - @GastoDiferencial
			WHERE ObligacionNumeroDocumento = @UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@NroRP)
			
			INSERT INTO AP_GastoAdelantoPagos (UnidadReplicacion,TipoAdelanto,NumeroAdelanto,Secuencia,ObligacionPagoFlag,ObligacionProveedor,ObligacionTipoDocumento,ObligacionNumeroDocumento,Monto,Estado,UltimoUsuario,UltimaFechaModif)
			SELECT TOP(1) @UnidadReplicacion, 'E', @AdelantoAdicional, 1, 'O', A.Persona, 'ER', @UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@NroRP), @GastoDiferencial, 'PR', @Usuario, GETDATE()
			FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Descripcion LIKE '%' + 'VIATICO ADICIONAL:' + '%' AND Estado != 'AN'
		END
		ELSE BEGIN
			IF (EXISTS(SELECT * FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Descripcion LIKE '%' + 'REINTEGRO POR CAMBIO DE RUTA:' + '%' AND Estado != 'AN')) BEGIN
				INSERT INTO AP_GastoAdelantoPagos (UnidadReplicacion,TipoAdelanto,NumeroAdelanto,Secuencia,ObligacionPagoFlag,ObligacionProveedor,ObligacionTipoDocumento,ObligacionNumeroDocumento,Monto,Estado,UltimoUsuario,UltimaFechaModif)
				SELECT TOP(1) @UnidadReplicacion, 'E', @AdelantoAdicional, 1, 'O', A.Persona, 'ER', @UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@NroRP), @GastoDiferencial, 'PR', @Usuario, GETDATE()
				FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Descripcion LIKE '%' + 'REINTEGRO POR CAMBIO DE RUTA:' + '%' AND Estado != 'AN'
			END
		END
		*/
	END
	ELSE BEGIN
		INSERT INTO AP_GastoAdelantoPagos (UnidadReplicacion,TipoAdelanto,NumeroAdelanto,Secuencia,ObligacionPagoFlag,ObligacionProveedor,ObligacionTipoDocumento,ObligacionNumeroDocumento,Monto,Estado,UltimoUsuario,UltimaFechaModif)
		SELECT TOP(1) @UnidadReplicacion, 'E', A.NumeroAdelanto, 1, 'O', A.Persona, 'ER', @UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@NroRP), @Total, 'PR', @Usuario, GETDATE()
		FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Descripcion LIKE '%' + 'GASTOS DE VIAJE ' + '%' AND Estado != 'AN'
	END
	
	IF (@Reintegro = 0.00) BEGIN
		UPDATE ReportesApp_Operaciones_TicketGasto_Registro
		SET NroRepGasto = @NroRP, TotalGastado = @Total - (@Devolucion + @Descuento), Vuelto = @Devolucion + @Reintegro, Descuento = @Descuento, Reintegro = 0.00, FechaLiquidacion = @FechaLiquidacion, UsuarioLiquidacion = @Usuario, EstadoLiquidacion = 1, Sucursal = @Sucursal2
		WHERE CodGasto = CONVERT(VARCHAR(30),@Planilla)
	END
	ELSE BEGIN
		IF (@Reintegro < 0.00) BEGIN
			SET @Absoluto = ABS(@Reintegro)

			--ACTUALIZAR NRO PARA QUE BUSQUE EL REINTEGRO
			SET @Nro = (SELECT MAX(NumeroAdelanto) FROM AP_GastoAdelanto WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Descripcion LIKE 'GV PLLA ' + '%' AND Estado != 'AN')

			UPDATE AP_CajaChica
			SET MontoTotal = @Gasto + @Absoluto, MontoAdelantos = @Gasto + @Absoluto, NumeroAdelanto = @Nro
			WHERE CajaChicaNumero = @NroRP

			DECLARE @AdeMenor INT
			SET @AdeMenor = (SELECT MIN(A.NumeroAdelanto) FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Estado != 'AN')

			--UPDATE AP_GastoAdelantoPagos
			--SET Monto = (SELECT MontoTotal FROM AP_GastoAdelanto WHERE NumeroAdelanto = @AdeMenor AND Estado != 'AN')
			--WHERE NumeroAdelanto = (SELECT MIN(A.NumeroAdelanto) FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Estado != 'AN')

			IF (EXISTS(SELECT * FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND CuentaBancaria = 'FONDO GV LIMA' AND Descripcion LIKE 'PLLA ' + '%' AND Estado != 'AN')) BEGIN
				INSERT INTO AP_GastoAdelantoPagos (UnidadReplicacion,TipoAdelanto,NumeroAdelanto,Secuencia,ObligacionPagoFlag,ObligacionProveedor,ObligacionTipoDocumento,ObligacionNumeroDocumento,Monto,Estado,UltimoUsuario,UltimaFechaModif)
				SELECT TOP(1) @UnidadReplicacion, 'E', A.NumeroAdelanto, 1, 'O', A.Persona, 'ER', @UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@NroRP), A.MontoTotal, 'PR', @Usuario, GETDATE()
				FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND CuentaBancaria = 'FONDO GV LIMA' AND Descripcion LIKE 'PLLA ' + '%' AND Estado != 'AN'
			END
			ELSE BEGIN
				IF (EXISTS(SELECT * FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND CuentaBancaria = 'FONDO GV PIURA' AND Descripcion LIKE 'PLLA ' + '%' AND Estado != 'AN')) BEGIN
					INSERT INTO AP_GastoAdelantoPagos (UnidadReplicacion,TipoAdelanto,NumeroAdelanto,Secuencia,ObligacionPagoFlag,ObligacionProveedor,ObligacionTipoDocumento,ObligacionNumeroDocumento,Monto,Estado,UltimoUsuario,UltimaFechaModif)
					SELECT TOP(1) @UnidadReplicacion, 'E', A.NumeroAdelanto, 1, 'O', A.Persona, 'ER', @UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@NroRP), A.MontoTotal, 'PR', @Usuario, GETDATE()
					FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND CuentaBancaria = 'FONDO GV PIURA' AND Descripcion LIKE 'PLLA ' + '%' AND Estado != 'AN'
				END
				ELSE BEGIN
					INSERT INTO AP_GastoAdelantoPagos (UnidadReplicacion,TipoAdelanto,NumeroAdelanto,Secuencia,ObligacionPagoFlag,ObligacionProveedor,ObligacionTipoDocumento,ObligacionNumeroDocumento,Monto,Estado,UltimoUsuario,UltimaFechaModif)
					SELECT TOP(1) @UnidadReplicacion, 'E', @Nro, 1, 'O', A.Persona, 'ER', @UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@NroRP), @Absoluto, 'PR', @Usuario, GETDATE()
					FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Estado != 'AN'
				END
			END
			
			UPDATE ReportesApp_Operaciones_TicketGasto_Registro
			SET NroRepGasto = @NroRP, TotalGastado = @Total, Vuelto = 0.00, Descuento = 0.00, Reintegro = @Absoluto, FechaLiquidacion = @FechaLiquidacion, UsuarioLiquidacion = @Usuario, EstadoLiquidacion = 1, Sucursal = @Sucursal2
			WHERE CodGasto = CONVERT(VARCHAR(30),@Planilla)
		END
	END
	SET @Exito = '0 = Comprobantes Liquidados Exitosamente. RP: ' + CONVERT(VARCHAR(10),@NroRP)
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
SELECT @exito exito

------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 10-08-2023
-- Description:	LISTAR PLANILLAS LIQUIDADAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ListarPlanillasLiquidadas]
@IdOperacion INT,
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@NombreConductor VARCHAR(250),
@Planilla VARCHAR(50),
@Ruta VARCHAR(250),
@Estado INT
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@IdOperacion = 5) BEGIN
		IF @Estado = 1 BEGIN
			SELECT TG.idTicketGasto, ISNULL(P.IdRuta,TG.IdRuta) AS 'IdRuta', ISNULL(P.TipoProgramacion,TG.IdOperacion) AS 'TipoProgramacion', TG.ViConductor AS 'IdConductor',
			TG.NroTicket, TG.NroRepGasto AS 'RG', TG.Sucursal AS 'SUCURSAL', TG.FechaCreacion AS 'FECHA', TG.CodGasto AS 'PLANILLA', O.Descripcion AS 'OPERACION', C.Nombre AS 'CONDUCTOR',
			V.NumeroPlaca AS 'TRACTO', VJ.Descripcion AS 'RUTA', TG.TotalEntregado AS 'IMPORTE_TOTAL', ISNULL(TG.TotalGastado,0) AS 'TOTAL_GASTO', ISNULL(TG.Vuelto,0) AS 'VUELTO',
			ISNULL(TG.Descuento,0) AS 'DESCUENTO', ISNULL(TG.Reintegro,0) AS 'REINTEGRO', TG.UsuarioLiquidacion AS 'USUARIO',
			CONVERT(CHAR(10),TG.FechaLiquidacion,103)+RIGHT(RTRIM(CONVERT(CHAR(26),TG.FechaLiquidacion,22)),12) AS 'FECHA_LIQUIDACION',
			LTRIM(RTRIM(Dp.DescripcionCorta))+' - '+LTRIM(RTRIM(PV.DescripcionCorta)) AS 'ORIGEN_CONDUCTOR', LTRIM(RTRIM(Zp.DescripcionCorta)) AS 'ZONA_CONDUCTOR', TG.Observacion AS 'OBSERVACION'
			FROM ReportesApp_Operaciones_TicketGasto_Registro TG
			LEFT JOIN ReportesApp_Operacion_Previaje_Registros P ON P.NroTicket = TG.NroTicket
			LEFT JOIN OP_TR_Ruta VJ ON VJ.IdRuta = ISNULL(P.IdRuta,TG.IdRuta)
			LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = ISNULL(TG.IdTracto,P.idTracto)
			LEFT JOIN OP_TR_Conductor C ON C.IdConductor = TG.ViConductor
			LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera GC ON GC.idGastoXRutaC = TG.idGastoXRutaC
			LEFT JOIN PersonaMast PM ON LTRIM(RTRIM(PM.Documento)) = LTRIM(RTRIM(C.Documento))
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = TG.IdOperacion
			LEFT JOIN Departamento Dp ON dp.Departamento = PM.Departamento
			LEFT JOIN Provincia Pv ON pv.Departamento = PM.Departamento AND pv.Provincia = PM.Provincia
			LEFT JOIN ZonaPostal Zp on Zp.Departamento = PM.Departamento AND zp.Provincia = PM.Provincia AND zp.CodigoPostal = PM.CodigoPostal
			WHERE ((TG.FechaLiquidacion BETWEEN @FINICIO AND @FFIN) AND (TG.EstadoLiquidacion = @Estado) AND (VJ.Descripcion IS NULL OR VJ.Descripcion LIKE '%' + @Ruta + '%') AND
			(TG.CodGasto IS NULL OR TG.CodGasto LIKE '%' + @Planilla + '%') AND (C.Nombre IS NULL OR C.Nombre LIKE '%' + @NombreConductor + '%'))
			ORDER BY TG.NroRepGasto DESC
		END

		IF @Estado = 0 BEGIN
			SELECT TG.idTicketGasto, ISNULL(P.IdRuta,TG.IdRuta) AS 'IdRuta', ISNULL(P.TipoProgramacion,TG.IdOperacion) AS 'TipoProgramacion', ISNULL(TG.idGastoXRutaC,22) AS 'idGastoXRutaC', TG.NroTicket,
			TG.ViConductor AS 'IdConductor', TG.ViConductor, TG.FechaCreacion AS 'FECHA', TG.CodGasto AS 'PLANILLA', ISNULL((SELECT TOP 1 CASE WHEN NumeroDocumentoInterno IS NOT NULL THEN ' ' ELSE 'PENDIENTE' END
			FROM AP_GastoAdelanto WHERE NumeroDocumentoInterno = TG.CodGasto AND (Estado = 'PA' OR Estado = 'AL') ORDER BY NumeroAdelanto),'') AS 'CAJA', O.Descripcion AS 'OPERACION',
			C.Nombre AS 'CONDUCTOR', V.NumeroPlaca AS 'TRACTO', VJ.Descripcion AS 'RUTA', TG.TotalEntregado - ISNULL(TG.GastoDiferencialV,0) - ISNULL(GastoDiferencial,0) AS 'IMPORTE_TOTAL',
			ISNULL(TG.GastoDiferencialV,0) AS 'ADICIONAL', ISNULL(TG.GastoDiferencial,0) AS 'CAMBIO_RUTA', ISNULL(TG.TotalGastado,0) AS 'TOTAL_GASTO', ISNULL(TG.Vuelto,0) AS 'VUELTO',
			ISNULL(TG.Descuento,0) AS 'DESCUENTO', ISNULL(TG.Reintegro,0) AS 'REINTEGRO', TG.UsuarioCreacion AS 'USUARIO', LTRIM(RTRIM(Dp.DescripcionCorta))+' - '+LTRIM(RTRIM(PV.DescripcionCorta)) AS 'ORIGEN_CONDUCTOR',
			LTRIM(RTRIM(Zp.DescripcionCorta)) AS 'ZONA_CONDUCTOR', TG.Permiso, CASE WHEN TG.Permiso = 0 THEN ' ' ELSE 'OK' END AS 'AUTORIZACION',
			TG.Observacion AS 'OBSERVACION' FROM ReportesApp_Operaciones_TicketGasto_Registro TG
			LEFT JOIN ReportesApp_Operacion_Previaje_Registros P ON P.NroTicket = TG.NroTicket
			LEFT JOIN OP_TR_Ruta VJ ON VJ.IdRuta = ISNULL(P.IdRuta,TG.IdRuta)
			LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = ISNULL(TG.IdTracto,P.idTracto)
			LEFT JOIN OP_TR_Conductor C ON C.IdConductor = TG.ViConductor
			LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera GC ON GC.idGastoXRutaC = TG.idGastoXRutaC
			LEFT JOIN PersonaMast PM ON LTRIM(RTRIM(PM.Documento)) = LTRIM(RTRIM(C.Documento))
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = TG.IdOperacion
			LEFT JOIN Departamento Dp ON dp.Departamento = PM.Departamento
			LEFT JOIN Provincia Pv ON pv.Departamento = PM.Departamento AND pv.Provincia = PM.Provincia
			LEFT JOIN ZonaPostal Zp on Zp.Departamento = PM.Departamento AND zp.Provincia = PM.Provincia AND zp.CodigoPostal = PM.CodigoPostal
			--INNER JOIN AP_GastoAdelanto A ON A.NumeroDocumentoInterno = TG.CodGasto
			WHERE ((TG.FechaCreacion BETWEEN @FINICIO AND @FFIN) AND (TG.EstadoLiquidacion = @Estado) AND (VJ.Descripcion IS NULL OR VJ.Descripcion LIKE '%' + @Ruta + '%') AND
			(TG.CodGasto IS NULL OR TG.CodGasto LIKE '%' + @Planilla + '%') AND (C.Nombre IS NULL OR C.Nombre LIKE '%' + @NombreConductor + '%'))
			ORDER BY idTicketGasto DESC
		END

		IF @Estado = 2 BEGIN
			SELECT TG.FechaCreacion AS 'FECHA', TG.CodGasto AS 'PLANILLA', O.Descripcion AS 'OPERACION', C.Nombre AS 'CONDUCTOR',
			LTRIM(RTRIM(Dp.DescripcionCorta))+' - '+LTRIM(RTRIM(PV.DescripcionCorta)) AS 'ORIGEN_CONDUCTOR', LTRIM(RTRIM(Zp.DescripcionCorta)) AS 'ZONA_CONDUCTOR',
			V.NumeroPlaca AS 'TRACTO', VJ.Descripcion AS 'RUTA', TG.TotalEntregado - ISNULL(TG.GastoDiferencialV,0) - ISNULL(GastoDiferencial,0) AS 'IMPORTE_TOTAL',
			ISNULL(TG.GastoDiferencialV,0) AS 'ADICIONAL', TG.UsuarioCreacion AS 'USUARIO_CREA', TG.UsuarioLiquidacion AS 'USUARIO_ANULA', TG.FechaLiquidacion AS 'FECHA_ANULA'
			FROM ReportesApp_Operaciones_TicketGasto_Registro TG
			LEFT JOIN ReportesApp_Operacion_Previaje_Registros P ON P.NroTicket = TG.NroTicket
			LEFT JOIN OP_TR_Ruta VJ ON VJ.IdRuta = ISNULL(P.IdRuta,TG.IdRuta)
			LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = ISNULL(TG.IdTracto,P.idTracto)
			LEFT JOIN OP_TR_Conductor C ON C.IdConductor = TG.ViConductor
			LEFT JOIN PersonaMast PM ON LTRIM(RTRIM(PM.Documento)) = LTRIM(RTRIM(C.Documento))
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = TG.IdOperacion
			LEFT JOIN Departamento Dp ON dp.Departamento = PM.Departamento
			LEFT JOIN Provincia Pv ON pv.Departamento = PM.Departamento AND pv.Provincia = PM.Provincia
			LEFT JOIN ZonaPostal Zp on Zp.Departamento = PM.Departamento AND zp.Provincia = PM.Provincia AND zp.CodigoPostal = PM.CodigoPostal
			WHERE ((TG.FechaLiquidacion BETWEEN @FINICIO AND @FFIN) AND (TG.EstadoLiquidacion = @Estado) AND (VJ.Descripcion IS NULL OR VJ.Descripcion LIKE '%' + @Ruta + '%') AND
			(TG.CodGasto IS NULL OR TG.CodGasto LIKE '%' + @Planilla + '%') AND (C.Nombre IS NULL OR C.Nombre LIKE '%' + @NombreConductor + '%'))
			ORDER BY TG.idTicketGasto DESC
		END
	END
	ELSE BEGIN
		IF @Estado = 1 BEGIN
			SELECT TG.idTicketGasto, ISNULL(P.IdRuta,TG.IdRuta) AS 'IdRuta', ISNULL(P.TipoProgramacion,TG.IdOperacion) AS 'TipoProgramacion', TG.ViConductor AS 'IdConductor',
			TG.NroTicket, TG.NroRepGasto AS 'RG', TG.Sucursal AS 'SUCURSAL', TG.FechaCreacion AS 'FECHA', TG.CodGasto AS 'PLANILLA', O.Descripcion AS 'OPERACION', C.Nombre AS 'CONDUCTOR',
			V.NumeroPlaca AS 'TRACTO', VJ.Descripcion AS 'RUTA', TG.TotalEntregado AS 'IMPORTE_TOTAL', ISNULL(TG.TotalGastado,0) AS 'TOTAL_GASTO', ISNULL(TG.Vuelto,0) AS 'VUELTO',
			ISNULL(TG.Descuento,0) AS 'DESCUENTO', ISNULL(TG.Reintegro,0) AS 'REINTEGRO', TG.UsuarioLiquidacion AS 'USUARIO',
			CONVERT(CHAR(10),TG.FechaLiquidacion,103)+RIGHT(RTRIM(CONVERT(CHAR(26),TG.FechaLiquidacion,22)),12) AS 'FECHA_LIQUIDACION',
			LTRIM(RTRIM(Dp.DescripcionCorta))+' - '+LTRIM(RTRIM(PV.DescripcionCorta)) AS 'ORIGEN_CONDUCTOR', LTRIM(RTRIM(Zp.DescripcionCorta)) AS 'ZONA_CONDUCTOR',
			TG.Observacion AS 'OBSERVACION' FROM ReportesApp_Operaciones_TicketGasto_Registro TG
			LEFT JOIN ReportesApp_Operacion_Previaje_Registros P ON P.NroTicket = TG.NroTicket
			LEFT JOIN OP_TR_Ruta VJ ON VJ.IdRuta = ISNULL(P.IdRuta,TG.IdRuta)
			LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = ISNULL(TG.IdTracto,P.idTracto)
			LEFT JOIN OP_TR_Conductor C ON C.IdConductor = TG.ViConductor
			LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera GC ON GC.idGastoXRutaC = TG.idGastoXRutaC
			LEFT JOIN PersonaMast PM ON LTRIM(RTRIM(PM.Documento)) = LTRIM(RTRIM(C.Documento))
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = TG.IdOperacion
			LEFT JOIN Departamento Dp ON dp.Departamento = PM.Departamento
			LEFT JOIN Provincia Pv ON pv.Departamento = PM.Departamento AND pv.Provincia = PM.Provincia
			LEFT JOIN ZonaPostal Zp on Zp.Departamento = PM.Departamento AND zp.Provincia = PM.Provincia AND zp.CodigoPostal = PM.CodigoPostal
			WHERE ((TG.IdOperacion = @IdOperacion) AND (TG.FechaLiquidacion BETWEEN @FINICIO AND @FFIN) AND (TG.EstadoLiquidacion = @Estado) AND (VJ.Descripcion IS NULL OR VJ.Descripcion LIKE '%' + @Ruta + '%')
			AND (TG.CodGasto IS NULL OR TG.CodGasto LIKE '%' + @Planilla + '%') AND (C.Nombre IS NULL OR C.Nombre LIKE '%' + @NombreConductor + '%'))
			ORDER BY TG.NroRepGasto DESC
		END

		IF @Estado = 0 BEGIN
			SELECT TG.idTicketGasto, ISNULL(P.IdRuta,TG.IdRuta) AS 'IdRuta', ISNULL(P.TipoProgramacion,TG.IdOperacion) AS 'TipoProgramacion', ISNULL(TG.idGastoXRutaC,22) AS 'idGastoXRutaC', TG.NroTicket,
			TG.ViConductor AS 'IdConductor', TG.ViConductor, TG.FechaCreacion AS 'FECHA', TG.CodGasto AS 'PLANILLA', ISNULL((SELECT TOP 1 CASE WHEN NumeroDocumentoInterno IS NOT NULL THEN ' ' ELSE 'PENDIENTE' END
			FROM AP_GastoAdelanto WHERE NumeroDocumentoInterno = TG.CodGasto AND (Estado = 'PA' OR Estado = 'AL') ORDER BY NumeroAdelanto),'') AS 'CAJA', O.Descripcion AS 'OPERACION',
			C.Nombre AS 'CONDUCTOR', V.NumeroPlaca AS 'TRACTO', VJ.Descripcion AS 'RUTA', TG.TotalEntregado - ISNULL(TG.GastoDiferencialV,0) - ISNULL(GastoDiferencial,0) AS 'IMPORTE_TOTAL',
			ISNULL(TG.GastoDiferencialV,0) AS 'ADICIONAL', ISNULL(TG.GastoDiferencial,0) AS 'CAMBIO_RUTA', ISNULL(TG.TotalGastado,0) AS 'TOTAL_GASTO', ISNULL(TG.Vuelto,0) AS 'VUELTO',
			ISNULL(TG.Descuento,0) AS 'DESCUENTO', ISNULL(TG.Reintegro,0) AS 'REINTEGRO', TG.UsuarioCreacion AS 'USUARIO', LTRIM(RTRIM(Dp.DescripcionCorta))+' - '+LTRIM(RTRIM(PV.DescripcionCorta)) AS 'ORIGEN_CONDUCTOR',
			LTRIM(RTRIM(Zp.DescripcionCorta)) AS 'ZONA_CONDUCTOR', TG.Permiso, CASE WHEN TG.Permiso = 0 THEN ' ' ELSE 'OK' END AS 'AUTORIZACION',
			TG.Observacion AS 'OBSERVACION' FROM ReportesApp_Operaciones_TicketGasto_Registro TG
			LEFT JOIN ReportesApp_Operacion_Previaje_Registros P ON P.NroTicket = TG.NroTicket
			LEFT JOIN OP_TR_Ruta VJ ON VJ.IdRuta = ISNULL(P.IdRuta,TG.IdRuta)
			LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = ISNULL(TG.IdTracto,P.idTracto)
			LEFT JOIN OP_TR_Conductor C ON C.IdConductor = TG.ViConductor
			LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera GC ON GC.idGastoXRutaC = TG.idGastoXRutaC
			LEFT JOIN PersonaMast PM ON LTRIM(RTRIM(PM.Documento)) = LTRIM(RTRIM(C.Documento))
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = TG.IdOperacion
			LEFT JOIN Departamento Dp ON dp.Departamento = PM.Departamento
			LEFT JOIN Provincia Pv ON pv.Departamento = PM.Departamento AND pv.Provincia = PM.Provincia
			LEFT JOIN ZonaPostal Zp on Zp.Departamento = PM.Departamento AND zp.Provincia = PM.Provincia AND zp.CodigoPostal = PM.CodigoPostal
			--INNER JOIN AP_GastoAdelanto A ON A.NumeroDocumentoInterno = TG.CodGasto
			WHERE ((TG.IdOperacion = @IdOperacion) AND (TG.FechaCreacion BETWEEN @FINICIO AND @FFIN) AND (TG.EstadoLiquidacion = @Estado) AND (VJ.Descripcion IS NULL OR VJ.Descripcion LIKE '%' + @Ruta + '%')
			AND (TG.CodGasto IS NULL OR TG.CodGasto LIKE '%' + @Planilla + '%') AND (C.Nombre IS NULL OR C.Nombre LIKE '%' + @NombreConductor + '%'))
			ORDER BY idTicketGasto DESC
		END

		IF @Estado = 2 BEGIN
			SELECT TG.FechaCreacion AS 'FECHA', TG.CodGasto AS 'PLANILLA', O.Descripcion AS 'OPERACION', C.Nombre AS 'CONDUCTOR',
			LTRIM(RTRIM(Dp.DescripcionCorta))+' - '+LTRIM(RTRIM(PV.DescripcionCorta)) AS 'ORIGEN_CONDUCTOR', LTRIM(RTRIM(Zp.DescripcionCorta)) AS 'ZONA_CONDUCTOR',
			V.NumeroPlaca AS 'TRACTO', VJ.Descripcion AS 'RUTA', TG.TotalEntregado - ISNULL(TG.GastoDiferencialV,0) - ISNULL(GastoDiferencial,0) AS 'IMPORTE_TOTAL',
			ISNULL(TG.GastoDiferencialV,0) AS 'ADICIONAL', TG.UsuarioCreacion AS 'USUARIO_CREA', TG.UsuarioLiquidacion AS 'USUARIO_ANULA', TG.FechaLiquidacion AS 'FECHA_ANULA'
			FROM ReportesApp_Operaciones_TicketGasto_Registro TG
			LEFT JOIN ReportesApp_Operacion_Previaje_Registros P ON P.NroTicket = TG.NroTicket
			LEFT JOIN OP_TR_Ruta VJ ON VJ.IdRuta = ISNULL(P.IdRuta,TG.IdRuta)
			LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = ISNULL(TG.IdTracto,P.idTracto)
			LEFT JOIN OP_TR_Conductor C ON C.IdConductor = TG.ViConductor
			LEFT JOIN PersonaMast PM ON LTRIM(RTRIM(PM.Documento)) = LTRIM(RTRIM(C.Documento))
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = TG.IdOperacion
			LEFT JOIN Departamento Dp ON dp.Departamento = PM.Departamento
			LEFT JOIN Provincia Pv ON pv.Departamento = PM.Departamento AND pv.Provincia = PM.Provincia
			LEFT JOIN ZonaPostal Zp on Zp.Departamento = PM.Departamento AND zp.Provincia = PM.Provincia AND zp.CodigoPostal = PM.CodigoPostal
			WHERE ((TG.IdOperacion = @IdOperacion) AND (TG.FechaLiquidacion BETWEEN @FINICIO AND @FFIN) AND (TG.EstadoLiquidacion = @Estado) AND (VJ.Descripcion IS NULL OR VJ.Descripcion LIKE '%' + @Ruta + '%')
			AND (TG.CodGasto IS NULL OR TG.CodGasto LIKE '%' + @Planilla + '%') AND (C.Nombre IS NULL OR C.Nombre LIKE '%' + @NombreConductor + '%'))
			ORDER BY idTicketGasto DESC
		END
	END
END

------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 10-08-2023
-- Description:	LISTAR RECIBOS LIQUIDADOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ListarRecibosLiquidados]
@CodGasto VARCHAR(50)
AS
BEGIN
	/*
	SELECT L.CodGasto AS 'PLANILLA', L.FechaLiquidacion AS 'FECHA_EMISION', CG.DescripcionLocal AS 'CONCEPTO_GASTO', L.DescripcionGasto AS 'DESCRIPCION',
		   L.NombreCompleto AS 'NOMBRE_COMPLETO', L.CodigoDocumento AS 'CODIGO', L.NroDocumento AS 'COMPROBANTE', L.MontoAfecto AS 'MONTO_AFECTO',
		   L.MontoNoAfecto AS 'MONTO_NO_AFECTO', L.MontoImpuestos AS 'MONTO_IMPUESTO', L.MontoPagado AS 'MONTO_TOTAL' 
	FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L
	LEFT JOIN ReportesApp_Operaciones_TicketGasto_ConceptoGasto CG ON L.ConceptoGasto = CG.ConceptoGasto
	LEFT JOIN ReportesApp_Operaciones_TicketGasto_TipoDocumento D ON L.CodigoDocumento = D.CodigoDocumento
	WHERE CodGasto = @CodGasto
	ORDER BY idNroLiquidacion
	*/
	
	SELECT CJ.NumeroDocumentoInterno AS 'PLANILLA', CONVERT(DATE,CD.FechaDocumento) AS 'FECHA_EMISION', CD.ConceptoGasto AS 'CONCEPTO_GASTO',
	CD.Descripcion AS 'DESCRIPCION', CD.ProveedorNombre AS 'PROVEEDOR', RTRIM(CD.NumeroDocumento) AS 'COMPROBANTE', CD.MontoAfecto AS 'MONTO_AFECTO',
	CD.MontoImpuesto AS 'MONTO_IMPUESTO', CD.MontoMonedaPago AS 'MONTO_TOTAL'
	FROM AP_CajaChicaDetalle CD
	LEFT JOIN AP_CajaChica CJ ON (CJ.CajaChicaNumero = CD.CajaChicaNumero)
	WHERE CJ.NumeroDocumentoInterno = LTRIM(RTRIM(@CodGasto))
END

------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 18-08-2023
-- Description:	ACTUALIZAR PLANILLAS LIQUIDADAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ActualizarRecibosLiquidados]
@CodGasto VARCHAR(50),
@ImporteTotal DECIMAL(10,2)
AS
DECLARE @Suma DECIMAL(10,2)
DECLARE @Reintegro DECIMAL(10,2)
DECLARE @Vuelto DECIMAL(10,2)
DECLARE @Descuento DECIMAL(10,2)

DECLARE @Contador INT
DECLARE @FechaAct DATETIME
DECLARE @TipoImpuestoAct CHAR(5)
DECLARE @ConceptoGastoAct CHAR(5)
DECLARE @DescripcionAct VARCHAR(200)
DECLARE @NroRUCAct CHAR(20)
DECLARE @ProveedorNombreAct VARCHAR(350)
DECLARE @CodigoDocumentoAct CHAR(10)
DECLARE @NroDocumentoAct VARCHAR(50)

BEGIN
	SET @Contador = 1

	SET @Reintegro = 0.00
	SET @Vuelto = 0.00
	SET @Descuento = 0.00
	SET @Suma = (SELECT SUM(CD.MontoMonedaPago) FROM AP_CajaChicaDetalle CD LEFT JOIN AP_CajaChica CJ ON (CJ.CajaChicaNumero = CD.CajaChicaNumero)
				 WHERE CJ.NumeroDocumentoInterno = LTRIM(RTRIM(@CodGasto)) AND CD.ConceptoGasto != '0078' AND CD.ConceptoGasto != '0062')

	IF (@Suma > @ImporteTotal) BEGIN
		SET @Reintegro = @Suma - @ImporteTotal
	END
	ELSE BEGIN
		SET @Vuelto = (SELECT CD.MontoMonedaPago FROM AP_CajaChicaDetalle CD LEFT JOIN AP_CajaChica CJ ON (CJ.CajaChicaNumero = CD.CajaChicaNumero)
					   WHERE CJ.NumeroDocumentoInterno = LTRIM(RTRIM(@CodGasto)) AND CD.ConceptoGasto = '0078')
		IF (EXISTS(SELECT CD.MontoMonedaPago FROM AP_CajaChicaDetalle CD LEFT JOIN AP_CajaChica CJ ON (CJ.CajaChicaNumero = CD.CajaChicaNumero)
				   WHERE CJ.NumeroDocumentoInterno = LTRIM(RTRIM(@CodGasto)) AND CD.ConceptoGasto = '0062')) BEGIN
			SET @Descuento = (SELECT CD.MontoMonedaPago FROM AP_CajaChicaDetalle CD LEFT JOIN AP_CajaChica CJ ON (CJ.CajaChicaNumero = CD.CajaChicaNumero)
							  WHERE CJ.NumeroDocumentoInterno = LTRIM(RTRIM(@CodGasto)) AND CD.ConceptoGasto = '0062')
		END
	END
	
	/*
	WHILE (@Contador <= (SELECT COUNT(idNroLiquidacion) FROM ReportesApp_Operaciones_TicketGasto_Liquidacion WHERE CodGasto = @CodGasto)) BEGIN
		SET @FechaAct = (SELECT CD.Fecha FROM AP_CajaChicaDetalle CD LEFT JOIN AP_CajaChica CJ ON (CJ.CajaChicaNumero = CD.CajaChicaNumero) WHERE LTRIM(RTRIM(CJ.NumeroDocumentoInterno)) = @CodGasto AND CD.Secuencia = @Contador)
		SET @TipoImpuestoAct = (SELECT CD.TipoImpuestoFlag FROM AP_CajaChicaDetalle CD LEFT JOIN AP_CajaChica CJ ON (CJ.CajaChicaNumero = CD.CajaChicaNumero) WHERE LTRIM(RTRIM(CJ.NumeroDocumentoInterno)) = @CodGasto AND CD.Secuencia = @Contador)
		SET @ConceptoGastoAct = (SELECT CD.ConceptoGasto FROM AP_CajaChicaDetalle CD LEFT JOIN AP_CajaChica CJ ON (CJ.CajaChicaNumero = CD.CajaChicaNumero) WHERE LTRIM(RTRIM(CJ.NumeroDocumentoInterno)) = @CodGasto AND CD.Secuencia = @Contador)
		SET @DescripcionAct = (SELECT CD.Descripcion FROM AP_CajaChicaDetalle CD LEFT JOIN AP_CajaChica CJ ON (CJ.CajaChicaNumero = CD.CajaChicaNumero) WHERE LTRIM(RTRIM(CJ.NumeroDocumentoInterno)) = @CodGasto AND CD.Secuencia = @Contador)
		SET @NroRUCAct = (SELECT CD.DocumentoFiscal FROM AP_CajaChicaDetalle CD LEFT JOIN AP_CajaChica CJ ON (CJ.CajaChicaNumero = CD.CajaChicaNumero) WHERE LTRIM(RTRIM(CJ.NumeroDocumentoInterno)) = @CodGasto AND CD.Secuencia = @Contador)
		SET @ProveedorNombreAct = (SELECT CD.ProveedorNombre FROM AP_CajaChicaDetalle CD LEFT JOIN AP_CajaChica CJ ON (CJ.CajaChicaNumero = CD.CajaChicaNumero) WHERE LTRIM(RTRIM(CJ.NumeroDocumentoInterno)) = @CodGasto AND CD.Secuencia = @Contador)
		SET @CodigoDocumentoAct = (SELECT CD.TipoDocumento FROM AP_CajaChicaDetalle CD LEFT JOIN AP_CajaChica CJ ON (CJ.CajaChicaNumero = CD.CajaChicaNumero) WHERE LTRIM(RTRIM(CJ.NumeroDocumentoInterno)) = @CodGasto AND CD.Secuencia = @Contador)
		SET @NroDocumentoAct = (SELECT * FROM AP_CajaChicaDetalle CD LEFT JOIN AP_CajaChica CJ ON (CJ.CajaChicaNumero = CD.CajaChicaNumero) WHERE LTRIM(RTRIM(CJ.NumeroDocumentoInterno)) = @CodGasto AND CD.Secuencia = @Contador)

		UPDATE ReportesApp_Operaciones_TicketGasto_Liquidacion
		SET FechaLiquidacion = @FechaAct, TipoImpuesto = @TipoImpuestoAct, ConceptoGasto = @ConceptoGastoAct, DescripcionGasto = @DescripcionAct, NroRUC = @NroRUCAct,
			NombreCompleto = @ProveedorNombreAct, CodigoDocumento = @CodigoDocumentoAct, NroDocumento = @NroDocumentoAct
		WHERE idNroLiquidacion = @Contador AND CodGasto = @CodGasto
		
		SET @Contador = @Contador + 1
	END
	*/

	UPDATE ReportesApp_Operaciones_TicketGasto_Registro
	SET TotalGastado = @Suma, Reintegro = @Reintegro, Descuento = @Descuento, Vuelto = @Vuelto
	WHERE CodGasto = @CodGasto
END

------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 28-11-2023
-- Description:	INSERTAR VIATICOS DE CONDUCTORES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_InsertarViaticos]
@NroTicket INT,
@CodGasto VARCHAR(50),
@idConductor INT,
@Fecha DATETIME,
@idTipoViatico INT,
@Monto DECIMAL(10,2),
@Descripcion VARCHAR(250),
@Motivo VARCHAR(250)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

SET @Exito = '0 = Exito.'
SET @correlativo = (SELECT MAX(idViatico) FROM ReportesApp_Operaciones_TicketGasto_Viaticos WHERE idConductor = @idConductor)
SET @correlativo = ISNULL(@correlativo,0) + 1

/*
IF(EXISTS(SELECT * FROM ReportesApp_Operaciones_TicketGasto_Viaticos WHERE idTipoViatico = @idTipoViatico AND CONVERT(DATE,Fecha) = CONVERT(DATE,@Fecha) AND idConductor = @idConductor)) BEGIN
	SET @Exito = '-1 = El conductor ya tiene este viático registrado.'
	GOTO Terminar
END
*/

IF((SELECT idTipoViatico FROM ReportesApp_Operaciones_TicketGasto_Viaticos WHERE idConductor = @idConductor AND CONVERT(DATE,Fecha) = CONVERT(DATE,@Fecha)) = 1) BEGIN
	SET @Exito = '-1 = El conductor ya tiene este viático registrado.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	INSERT INTO ReportesApp_Operaciones_TicketGasto_Viaticos(idViatico,CodGasto,idConductor,Fecha,idTipoViatico,Monto,NroTicket,Descripcion,Motivo)
	VALUES(@correlativo,@CodGasto,@idConductor,@Fecha,@idTipoViatico,@Monto,@NroTicket,@Descripcion,@Motivo)

	IF (@CodGasto = '') BEGIN
		UPDATE ReportesApp_Operacion_Previaje_Registros
		SET Viaticos = Viaticos + @Monto
		WHERE NroTicket = @NroTicket
	END
	ELSE BEGIN
		UPDATE ReportesApp_Operaciones_TicketGasto_Registro
		SET TotalEntregado = TotalEntregado + @Monto, GastoDiferencialV = ISNULL(GastoDiferencialV,0) + @Monto
		WHERE CodGasto = @CodGasto

		UPDATE ReportesApp_Operacion_Previaje_Registros
		SET Viaticos = Viaticos + @Monto
		WHERE NroTicket = @NroTicket
	END
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
SELECT @exito exito

------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 14-08-2023
-- Description:	LISTAR VIATICOS DE CONDUCTORES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ListarViaticos]
@Opcion INT,
@idConductor INT
AS
BEGIN
	SELECT TOP(20) V.idViatico, V.NroTicket, V.idConductor, V.Fecha AS 'FECHA_VIATICO', V.CodGasto AS 'PLANILLA', V.Descripcion AS 'VIATICO',
	V.Monto AS 'IMPORTE', V.Motivo AS 'MOTIVO', R.EstadoLiquidacion FROM ReportesApp_Operaciones_TicketGasto_Viaticos V
	LEFT JOIN ReportesApp_Operaciones_TicketGasto_Registro R ON R.CodGasto = V.CodGasto
	WHERE V.idConductor = @idConductor
	ORDER BY V.idViatico DESC
END

------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 24-08-2023
-- Description:	LISTAR REGISTRO DE VIATICOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ListarRegistroViaticos]
@Opcion INT,
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@NombreConductor VARCHAR(250)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@Opcion = 1) BEGIN		-- VIÁTICOS ADICIONALES
		SELECT V.idViatico, C.Nombre AS 'CONDUCTOR', V.Fecha AS 'FECHA_VIATICO', V.CodGasto AS 'PLANILLA', RT.Descripcion AS 'RUTA_VIAJE',
		V.Descripcion AS 'VIATICO', V.Monto AS 'IMPORTE', V.Motivo AS 'MOTIVO'
		FROM ReportesApp_Operaciones_TicketGasto_Viaticos V
		LEFT JOIN OP_TR_Conductor C ON C.IdConductor = V.IdConductor
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_TipoViaticos TV ON TV.idTipoViatico = V.idTipoViatico
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_Registro R ON R.CodGasto = V.CodGasto
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera GC ON GC.idGastoxRutaC = R.idGastoxRutaC
		LEFT JOIN OP_TR_RUTA RT ON RT.IdRuta = GC.IdRuta
		WHERE ((V.Fecha BETWEEN @FINICIO AND @FFIN) AND (C.Nombre LIKE '%' + @NombreConductor + '%'))
		ORDER BY V.Fecha DESC
	END

	IF (@Opcion = 2) BEGIN		-- REGISTRO DE VIÁTICOS
		(SELECT V.idListaViatico, V.IdConductor, C.Nombre AS 'CONDUCTOR', V.FechaViatico AS 'FECHA_VIATICO', L.DescripcionGasto,
		L.CodGasto AS 'PLANILLA', V.Comprobante AS 'COMPROBANTE', L.NombreCompleto AS 'PROVEEDOR', V.Monto AS 'IMPORTE'
		FROM ReportesApp_Operaciones_TicketGasto_ListaViaticos V
		LEFT JOIN OP_TR_Conductor C ON C.IdConductor = V.IdConductor
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_Liquidacion L ON LTRIM(RTRIM(L.NroDocumento)) = LTRIM(RTRIM(V.Comprobante))
		AND LTRIM(RTRIM(L.NroRUC)) = LTRIM(RTRIM(V.NroRUC)) AND (L.ConceptoGasto = '0002 ') AND (L.CodGasto = V.Planilla)
		WHERE ((L.CodGasto IS NOT NULL) AND (V.FechaViatico BETWEEN @FINICIO AND @FFIN) AND (C.Nombre LIKE '%' + @NombreConductor + '%')))
		UNION
		(SELECT V.idListaViatico, V.IdConductor, C.Nombre AS 'CONDUCTOR', V.FechaViatico AS 'FECHA_VIATICO', L.DescripcionGasto,
		V.Planilla AS 'PLANILLA', V.Comprobante AS 'COMPROBANTE', C.Nombre AS 'PROVEEDOR', V.Monto AS 'IMPORTE'
		FROM ReportesApp_Operaciones_TicketGasto_ListaViaticos V
		LEFT JOIN OP_TR_Conductor C ON C.IdConductor = V.IdConductor
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_Liquidacion L ON LTRIM(RTRIM(L.CodGasto)) = LTRIM(RTRIM(V.Planilla)) AND LTRIM(RTRIM(L.NroDocumento)) = LTRIM(RTRIM(V.Comprobante))
		WHERE ((V.Comprobante LIKE 'ALIM%') AND (V.Comprobante IS NOT NULL) AND (V.FechaViatico BETWEEN @FINICIO AND @FFIN) AND (C.Nombre LIKE '%' + @NombreConductor + '%')))
		ORDER BY V.FechaViatico DESC
	END
END

------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 15-08-2023
-- Description:	ELIMINAR VIATICOS DE CONDUCTORES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_EliminarViaticos]
@NroTicket INT,
@idViatico INT,
@idConductor INT,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @Monto DECIMAL(10,2)
DECLARE @CodGasto VARCHAR(30)
DECLARE @Estado VARCHAR(30)

SET @Exito = '0 = Recibo eliminado.'

IF (@idViatico = 0)
BEGIN
	SET @Exito = '-1 = El viatico seleccionado no existe.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	SET @Monto = (SELECT Monto FROM ReportesApp_Operaciones_TicketGasto_Viaticos WHERE idViatico = @idViatico AND idConductor = @idConductor)
	SET @CodGasto = (SELECT CodGasto FROM ReportesApp_Operaciones_TicketGasto_Viaticos WHERE idViatico = @idViatico AND idConductor = @idConductor)
	
	IF (@CodGasto LIKE '%0%') BEGIN
		SET @Estado = ISNULL((SELECT TOP 1 CASE WHEN NumeroDocumentoInterno IS NOT NULL THEN 'PAGADA' ELSE ' ' END FROM AP_GastoAdelanto
		WHERE RTRIM(NumeroDocumentoInterno) = @CodGasto AND (Estado = 'PA' OR Estado = 'AL') AND Descripcion LIKE '%ADICIONAL%' ORDER BY NumeroAdelanto),'PENDIENTE')
	END
	ELSE BEGIN
		SET @Estado = 'PENDIENTE'
	END

	IF (@Estado != 'PAGADA') BEGIN
		UPDATE ReportesApp_Operacion_Previaje_Registros
		SET Viaticos = Viaticos - @Monto
		WHERE NroTicket = @NroTicket

		IF ((SELECT GastoDiferencialV FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE
		CodGasto = (SELECT CodGasto FROM ReportesApp_Operaciones_TicketGasto_Viaticos WHERE idViatico = @idViatico AND idConductor = @idConductor)) IS NOT NULL)
		BEGIN
			UPDATE ReportesApp_Operaciones_TicketGasto_Registro
			SET TotalEntregado = TotalEntregado - @Monto, GastoDiferencialV = ISNULL(GastoDiferencialV,0) - @Monto
			WHERE CodGasto = (SELECT CodGasto FROM ReportesApp_Operaciones_TicketGasto_Viaticos WHERE idViatico = @idViatico AND idConductor = @idConductor)
		END
		ELSE BEGIN
			UPDATE ReportesApp_Operaciones_TicketGasto_Registro
			SET TotalEntregado = TotalEntregado - @Monto, GastoDiferencialV = @Monto
			WHERE CodGasto = (SELECT CodGasto FROM ReportesApp_Operaciones_TicketGasto_Viaticos WHERE idViatico = @idViatico AND idConductor = @idConductor)
		END

		/*
		IF ((SELECT EstadoLiquidacion FROM ReportesApp_Operaciones_TicketGasto_Registro
		WHERE CodGasto = (SELECT CodGasto FROM ReportesApp_Operaciones_TicketGasto_Viaticos WHERE idViatico = @idViatico AND idConductor = @idConductor)) = 3)
		BEGIN
			UPDATE ReportesApp_Operaciones_TicketGasto_Registro
			SET EstadoLiquidacion = 2, UsuarioLiquidacion = @Usuario, FechaLiquidacion = GETDATE()
			WHERE CodGasto = (SELECT CodGasto FROM ReportesApp_Operaciones_TicketGasto_Viaticos WHERE idViatico = @idViatico AND idConductor = @idConductor)
		END
		*/

		INSERT INTO ReportesApp_Operaciones_TicketGasto_Viaticos_Historial (idViatico,CodGasto,NroTicket,idConductor,Fecha,idTipoViatico,Monto,
					Descripcion,Motivo,UsuarioElimina,FechaElimina)
		SELECT idViatico,CodGasto,NroTicket,idConductor,Fecha,idTipoViatico,Monto,Descripcion,Motivo,@Usuario,GETDATE()
		FROM ReportesApp_Operaciones_TicketGasto_Viaticos
		WHERE idViatico = @idViatico AND idConductor = @idConductor

		DELETE FROM ReportesApp_Operaciones_TicketGasto_Viaticos
		WHERE idViatico = @idViatico AND idConductor = @idConductor
	END
	ELSE BEGIN
		SET @Exito = '-1 = El viático seleccionado ya se encuentra pagado, no se puede eliminar.'
	END
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
SELECT @exito exito

------------------------------------------------

--IMPRIMIR SUSTENTO DE LIQUIDACIÓN

SELECT TOP(100) CJ.NumeroDocumentoInterno AS 'Planilla', CJ.UnidadReplicacion + '-' + CONVERT(VARCHAR,CJ.CajaChicaNumero) AS 'Reporte de Gasto', CONVERT(DATE,CJ.FechaPreparacion) AS 'FechaLiquidacion',
C.Nombre AS 'Conductor', CJ.Descripcion, TG.TotalEntregado AS 'Entregado', ISNULL(TG.TotalGastado,0) AS 'Gastado', ISNULL(TG.Vuelto,0) AS 'Vuelto', ISNULL(TG.Descuento,0) AS 'Descuento',
ISNULL(TG.Reintegro,0) AS 'Reintegro', TG.UsuarioLiquidacion AS 'Usuario', CD.Secuencia, CONVERT(DATE,CD.Fecha) AS 'Fecha', CD.Descripcion AS 'Concepto', ISNULL(CD.ProveedorNombre,' ') AS 'Proveedor',
ISNULL(CD.TipoDocumento+' - '+LTRIM(RTRIM(CD.NumeroDocumento)),' ') AS 'Documento', CD.MontoAfecto, CD.MontoNoAfecto, CD.MontoImpuesto, CD.MontoMonedaPago
FROM AP_CajaChica CJ
INNER JOIN AP_CajaChicaDetalle CD ON CJ.CajaChicaNumero = CD.CajaChicaNumero
LEFT JOIN ReportesApp_Operaciones_TicketGasto_Registro TG ON TG.CodGasto = CJ.NumeroDocumentoInterno
LEFT JOIN ReportesApp_Operacion_Previaje_Registros P ON P.NroTicket = TG.NroTicket
LEFT JOIN OP_TR_Conductor C ON C.IdConductor = P.IdConductor
LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera GC ON GC.idGastoXRutaC = TG.idGastoXRutaC
WHERE TG.EstadoLiquidacion = 1
ORDER BY CJ.NumeroDocumentoInterno DESC

------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 19-08-2023
-- Description:	ELIMINAR PLANILLA SIN PAGAR
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_EliminarPlanilla]
@NroProgramacion INT,
@CodGasto VARCHAR(50),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Planilla eliminada.'

BEGIN TRAN
BEGIN TRY
	IF (EXISTS(SELECT * FROM AP_GastoAdelanto WHERE NumeroDocumentoInterno = @CodGasto AND (Estado = 'PA'))) BEGIN
		IF ((SELECT EstadoLiquidacion FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE CodGasto = @CodGasto) = 0) BEGIN
			SET @Exito = '-1 = Esta planilla ya se encuentra pagada. Favor de ir a liquidarla para desvincularla.'
			ROLLBACK
			GOTO Terminar
		END
		ELSE BEGIN
			UPDATE ReportesApp_Operaciones_TicketGasto_Viaticos
			SET CodGasto = NULL
			WHERE CodGasto = @CodGasto

			UPDATE ReportesApp_Operacion_Previaje_Registros
			SET Planilla = ''
			WHERE NroTicket = @NroProgramacion
		END
	END
	ELSE BEGIN
		UPDATE ReportesApp_Operaciones_TicketGasto_Viaticos
		SET CodGasto = NULL
		WHERE CodGasto = @CodGasto

		UPDATE ReportesApp_Operacion_Previaje_Registros
		SET Planilla = ''
		WHERE NroTicket = @NroProgramacion

		UPDATE ReportesApp_Operaciones_TicketGasto_Registro
		SET EstadoLiquidacion = 2, UsuarioLiquidacion = @Usuario, FechaLiquidacion = GETDATE()
		WHERE CodGasto = @CodGasto
	END
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
SELECT @exito exito

------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 12-05-2025
-- Description:	LISTAR VIATICOS POR TICKET
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ListarViaticosTicket]
@Opcion INT,
@NroTicket INT,
@idRuta INT
AS
BEGIN
	IF (@Opcion = 1) BEGIN		-- LISTAR VIÁTICO ORIGINAL
		SELECT TotalEntregado - ISNULL(GastoDiferencialV,0) - ISNULL(GastoDiferencial,0) AS 'Viatico', CodGasto AS 'Planilla'
		FROM ReportesApp_Operaciones_TicketGasto_Registro
		WHERE NroTicket = @NroTicket
	END

	IF (@Opcion = 2) BEGIN		-- LISTAR PLANILLA
		SELECT CodGasto AS 'Planilla'
		FROM ReportesApp_Operaciones_TicketGasto_Registro
		WHERE NroTicket = @NroTicket
	END
END

------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 12-05-2025
-- Description:	GENERAR PLANILLA EVENTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_GenerarPlanillaEvento]
@Planilla VARCHAR(30),
@NroTicket INT,
@TipoProgramacion INT,
@idRuta INT,
@IdConductor INT,
@TotalEntregado DECIMAL(10,2),
@Usuario VARCHAR(50)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT
DECLARE @idGastoxRutaC INT

SET @Exito = '0 = Planilla Registrada.'

SET @correlativo = (SELECT MAX(idTicketGasto) FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE Anio = YEAR(GETDATE()))
SET @correlativo = ISNULL(@correlativo,0) + 1

BEGIN TRAN
BEGIN TRY
	SET @idGastoxRutaC = (SELECT idGastoxRutaC FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera WHERE IdRuta = @idRuta AND IdOperacion = @TipoProgramacion)

	INSERT INTO ReportesApp_Operaciones_TicketGasto_Registro(idTicketGasto, Anio, CodGasto, NroTicket, idGastoXRutaC, TotalEntregado, FechaCreacion,
	UsuarioCreacion,EstadoLiquidacion,Permiso,Observacion)
	VALUES(@correlativo, YEAR(GETDATE()), SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo)),6)),
	@NroTicket, @idGastoxRutaC, @TotalEntregado, GETDATE(), @Usuario, 0, 0, 'PLANILLA EVENTO')

	UPDATE ReportesApp_Operaciones_TicketGasto_Registro
	SET ViConductor = @IdConductor, IdOperacion = @TipoProgramacion
	WHERE CodGasto = SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo)),6))

	UPDATE ReportesApp_Operacion_Previaje_Registros
	SET Planilla = SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo)),6)),
	Viaticos = @TotalEntregado
	WHERE NroTicket = @NroTicket
	
	SET @Exito = '0 = Planilla de Evento Registrada. PLE - ' + SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo)),6))
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
SELECT @exito exito

------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 15-05-2025
-- Description:	ACTUALIZAR ASISTENCIAS PLANILLAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_AsistenciaPlanillas]
@CodGasto VARCHAR(30)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Asistencia registrada exitosamente.'

BEGIN TRAN
BEGIN TRY
	DECLARE @NroTicket INT = (SELECT NroTicket FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE CodGasto = @CodGasto)
	DECLARE @IdConductor INT = (SELECT ViConductor FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE CodGasto = @CodGasto)
	DECLARE @IdOperacion INT = (SELECT IdOperacion FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE CodGasto = @CodGasto)
	DECLARE @NroPersona INT = (SELECT P.Persona FROM PersonaMast P
							   LEFT JOIN OP_TR_Conductor C ON RTRIM(C.TipoDocumento) = RTRIM(P.TipoDocumento) AND RTRIM(C.Documento) = RTRIM(P.Documento)
							   WHERE IdConductor = @IdConductor)
	DECLARE @IdRuta INT

	IF (@IdOperacion = 1) BEGIN
		SET @IdRuta = (SELECT IdRuta FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE CodGasto = @CodGasto)
	END
	ELSE BEGIN
		SET @IdRuta = (SELECT IdRuta FROM ReportesApp_Operacion_Previaje_Registros WHERE NroTicket = @NroTicket)
	END

	DECLARE @TiempoHoras DECIMAL(10,2) = (SELECT Tiempo FROM OP_TR_Ruta WHERE IdRuta = @IdRuta)
	DECLARE @Usuario VARCHAR(20) = (SELECT UsuarioCreacion FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE CodGasto = @CodGasto)
	DECLARE @FechaInicio DATETIME = (SELECT GETDATE())
	DECLARE @FechaFin DATETIME = (SELECT DATEADD(HOUR,@TiempoHoras,@FechaInicio))

	IF (EXISTS(SELECT * FROM ReportesApp_RRHH_AsistenciaView WHERE Anio = YEAR(@FechaInicio) AND Mes = MONTH(@FechaInicio) AND CodPlanilla = 'CD' AND IDPersona = @NroPersona)) BEGIN
		WHILE (CONVERT(DATE,@FechaInicio) <= CONVERT(DATE,@FechaFin)) BEGIN
			IF (NOT EXISTS(SELECT * FROM ReportesApp_RRHH_Asistencia WHERE IdPersona = @NroPersona AND CONVERT(DATE,Fecha) = CONVERT(DATE,@FechaInicio))) BEGIN
				INSERT INTO ReportesApp_RRHH_Asistencia(IdPersona,Planilla,Fecha,IdTipoAsist,ConceptoAcceso,FHRegistra,UserCrea)
				VALUES(@NroPersona,'CD',@FechaInicio,32,'ASIS',GETDATE(),@Usuario)

				UPDATE V 
				SET V.D1 = CASE WHEN DAY(@FechaInicio) = 1 AND (V.D1 IS NULL OR V.D1 = 'DF') THEN 'A' ELSE V.D1 END,
					V.D2 = CASE WHEN DAY(@FechaInicio) = 2 AND (V.D2 IS NULL OR V.D2 = 'DF') THEN 'A' ELSE V.D2 END,
					V.D3 = CASE WHEN DAY(@FechaInicio) = 3 AND (V.D3 IS NULL OR V.D3 = 'DF') THEN 'A' ELSE V.D3 END,
					V.D4 = CASE WHEN DAY(@FechaInicio) = 4 AND (V.D4 IS NULL OR V.D4 = 'DF') THEN 'A' ELSE V.D4 END,
					V.D5 = CASE WHEN DAY(@FechaInicio) = 5 AND (V.D5 IS NULL OR V.D5 = 'DF') THEN 'A' ELSE V.D5 END,
					V.D6 = CASE WHEN DAY(@FechaInicio) = 6 AND (V.D6 IS NULL OR V.D6 = 'DF') THEN 'A' ELSE V.D6 END,
					V.D7 = CASE WHEN DAY(@FechaInicio) = 7 AND (V.D7 IS NULL OR V.D7 = 'DF') THEN 'A' ELSE V.D7 END,
					V.D8 = CASE WHEN DAY(@FechaInicio) = 8 AND (V.D8 IS NULL OR V.D8 = 'DF') THEN 'A' ELSE V.D8 END,
					V.D9 = CASE WHEN DAY(@FechaInicio) = 9 AND (V.D9 IS NULL OR V.D9 = 'DF') THEN 'A' ELSE V.D9 END,
					V.D10 = CASE WHEN DAY(@FechaInicio) = 10 AND (V.D10 IS NULL OR V.D10 = 'DF') THEN 'A' ELSE V.D10 END,
					V.D11 = CASE WHEN DAY(@FechaInicio) = 11 AND (V.D11 IS NULL OR V.D11 = 'DF') THEN 'A' ELSE V.D11 END,
					V.D12 = CASE WHEN DAY(@FechaInicio) = 12 AND (V.D12 IS NULL OR V.D12 = 'DF') THEN 'A' ELSE V.D12 END,
					V.D13 = CASE WHEN DAY(@FechaInicio) = 13 AND (V.D13 IS NULL OR V.D13 = 'DF') THEN 'A' ELSE V.D13 END,
					V.D14 = CASE WHEN DAY(@FechaInicio) = 14 AND (V.D14 IS NULL OR V.D14 = 'DF') THEN 'A' ELSE V.D14 END,
					V.D15 = CASE WHEN DAY(@FechaInicio) = 15 AND (V.D15 IS NULL OR V.D15 = 'DF') THEN 'A' ELSE V.D15 END,
					V.D16 = CASE WHEN DAY(@FechaInicio) = 16 AND (V.D16 IS NULL OR V.D16 = 'DF') THEN 'A' ELSE V.D16 END,
					V.D17 = CASE WHEN DAY(@FechaInicio) = 17 AND (V.D17 IS NULL OR V.D17 = 'DF') THEN 'A' ELSE V.D17 END,
					V.D18 = CASE WHEN DAY(@FechaInicio) = 18 AND (V.D18 IS NULL OR V.D18 = 'DF') THEN 'A' ELSE V.D18 END,
					V.D19 = CASE WHEN DAY(@FechaInicio) = 19 AND (V.D19 IS NULL OR V.D19 = 'DF') THEN 'A' ELSE V.D19 END,
					V.D20 = CASE WHEN DAY(@FechaInicio) = 20 AND (V.D20 IS NULL OR V.D20 = 'DF') THEN 'A' ELSE V.D20 END,
					V.D21 = CASE WHEN DAY(@FechaInicio) = 21 AND (V.D21 IS NULL OR V.D21 = 'DF') THEN 'A' ELSE V.D21 END,
					V.D22 = CASE WHEN DAY(@FechaInicio) = 22 AND (V.D22 IS NULL OR V.D22 = 'DF') THEN 'A' ELSE V.D22 END,
					V.D23 = CASE WHEN DAY(@FechaInicio) = 23 AND (V.D23 IS NULL OR V.D23 = 'DF') THEN 'A' ELSE V.D23 END,
					V.D24 = CASE WHEN DAY(@FechaInicio) = 24 AND (V.D24 IS NULL OR V.D24 = 'DF') THEN 'A' ELSE V.D24 END,
					V.D25 = CASE WHEN DAY(@FechaInicio) = 25 AND (V.D25 IS NULL OR V.D25 = 'DF') THEN 'A' ELSE V.D25 END,
					V.D26 = CASE WHEN DAY(@FechaInicio) = 26 AND (V.D26 IS NULL OR V.D26 = 'DF') THEN 'A' ELSE V.D26 END,
					V.D27 = CASE WHEN DAY(@FechaInicio) = 27 AND (V.D27 IS NULL OR V.D27 = 'DF') THEN 'A' ELSE V.D27 END,
					V.D28 = CASE WHEN DAY(@FechaInicio) = 28 AND (V.D28 IS NULL OR V.D28 = 'DF') THEN 'A' ELSE V.D28 END,
					V.D29 = CASE WHEN DAY(@FechaInicio) = 29 AND (V.D29 IS NULL OR V.D29 = 'DF') THEN 'A' ELSE V.D29 END,
					V.D30 = CASE WHEN DAY(@FechaInicio) = 30 AND (V.D30 IS NULL OR V.D30 = 'DF') THEN 'A' ELSE V.D30 END,
					V.D31 = CASE WHEN DAY(@FechaInicio) = 31 AND (V.D31 IS NULL OR V.D31 = 'DF') THEN 'A' ELSE V.D31 END
				FROM ReportesApp_RRHH_AsistenciaView V
				WHERE V.Anio = YEAR(@FechaInicio) AND V.Mes = MONTH(@FechaInicio) AND V.CodPlanilla = 'CD' AND IDPersona = @NroPersona
			END
			ELSE BEGIN
				IF ((SELECT IDTipoAsist FROM ReportesApp_RRHH_Asistencia WHERE IdPersona = @NroPersona AND CONVERT(DATE,Fecha) = CONVERT(DATE,@FechaInicio)) = 29) BEGIN
					UPDATE ReportesApp_RRHH_Asistencia
					SET IDEstablecimiento = NULL, IDTipoAsist = 32, ConceptoAcceso = 'ASIS', UserCrea = @Usuario, FHRegistra = GETDATE()
					WHERE IDPersona = @NroPersona AND CONVERT(DATE,Fecha) = CONVERT(DATE,@FechaInicio) 
					
					UPDATE V 
					SET V.D1 = CASE WHEN DAY(@FechaInicio) = 1 AND (V.D1 IS NULL OR V.D1 = 'DF') THEN 'A' ELSE V.D1 END,
						V.D2 = CASE WHEN DAY(@FechaInicio) = 2 AND (V.D2 IS NULL OR V.D2 = 'DF') THEN 'A' ELSE V.D2 END,
						V.D3 = CASE WHEN DAY(@FechaInicio) = 3 AND (V.D3 IS NULL OR V.D3 = 'DF') THEN 'A' ELSE V.D3 END,
						V.D4 = CASE WHEN DAY(@FechaInicio) = 4 AND (V.D4 IS NULL OR V.D4 = 'DF') THEN 'A' ELSE V.D4 END,
						V.D5 = CASE WHEN DAY(@FechaInicio) = 5 AND (V.D5 IS NULL OR V.D5 = 'DF') THEN 'A' ELSE V.D5 END,
						V.D6 = CASE WHEN DAY(@FechaInicio) = 6 AND (V.D6 IS NULL OR V.D6 = 'DF') THEN 'A' ELSE V.D6 END,
						V.D7 = CASE WHEN DAY(@FechaInicio) = 7 AND (V.D7 IS NULL OR V.D7 = 'DF') THEN 'A' ELSE V.D7 END,
						V.D8 = CASE WHEN DAY(@FechaInicio) = 8 AND (V.D8 IS NULL OR V.D8 = 'DF') THEN 'A' ELSE V.D8 END,
						V.D9 = CASE WHEN DAY(@FechaInicio) = 9 AND (V.D9 IS NULL OR V.D9 = 'DF') THEN 'A' ELSE V.D9 END,
						V.D10 = CASE WHEN DAY(@FechaInicio) = 10 AND (V.D10 IS NULL OR V.D10 = 'DF') THEN 'A' ELSE V.D10 END,
						V.D11 = CASE WHEN DAY(@FechaInicio) = 11 AND (V.D11 IS NULL OR V.D11 = 'DF') THEN 'A' ELSE V.D11 END,
						V.D12 = CASE WHEN DAY(@FechaInicio) = 12 AND (V.D12 IS NULL OR V.D12 = 'DF') THEN 'A' ELSE V.D12 END,
						V.D13 = CASE WHEN DAY(@FechaInicio) = 13 AND (V.D13 IS NULL OR V.D13 = 'DF') THEN 'A' ELSE V.D13 END,
						V.D14 = CASE WHEN DAY(@FechaInicio) = 14 AND (V.D14 IS NULL OR V.D14 = 'DF') THEN 'A' ELSE V.D14 END,
						V.D15 = CASE WHEN DAY(@FechaInicio) = 15 AND (V.D15 IS NULL OR V.D15 = 'DF') THEN 'A' ELSE V.D15 END,
						V.D16 = CASE WHEN DAY(@FechaInicio) = 16 AND (V.D16 IS NULL OR V.D16 = 'DF') THEN 'A' ELSE V.D16 END,
						V.D17 = CASE WHEN DAY(@FechaInicio) = 17 AND (V.D17 IS NULL OR V.D17 = 'DF') THEN 'A' ELSE V.D17 END,
						V.D18 = CASE WHEN DAY(@FechaInicio) = 18 AND (V.D18 IS NULL OR V.D18 = 'DF') THEN 'A' ELSE V.D18 END,
						V.D19 = CASE WHEN DAY(@FechaInicio) = 19 AND (V.D19 IS NULL OR V.D19 = 'DF') THEN 'A' ELSE V.D19 END,
						V.D20 = CASE WHEN DAY(@FechaInicio) = 20 AND (V.D20 IS NULL OR V.D20 = 'DF') THEN 'A' ELSE V.D20 END,
						V.D21 = CASE WHEN DAY(@FechaInicio) = 21 AND (V.D21 IS NULL OR V.D21 = 'DF') THEN 'A' ELSE V.D21 END,
						V.D22 = CASE WHEN DAY(@FechaInicio) = 22 AND (V.D22 IS NULL OR V.D22 = 'DF') THEN 'A' ELSE V.D22 END,
						V.D23 = CASE WHEN DAY(@FechaInicio) = 23 AND (V.D23 IS NULL OR V.D23 = 'DF') THEN 'A' ELSE V.D23 END,
						V.D24 = CASE WHEN DAY(@FechaInicio) = 24 AND (V.D24 IS NULL OR V.D24 = 'DF') THEN 'A' ELSE V.D24 END,
						V.D25 = CASE WHEN DAY(@FechaInicio) = 25 AND (V.D25 IS NULL OR V.D25 = 'DF') THEN 'A' ELSE V.D25 END,
						V.D26 = CASE WHEN DAY(@FechaInicio) = 26 AND (V.D26 IS NULL OR V.D26 = 'DF') THEN 'A' ELSE V.D26 END,
						V.D27 = CASE WHEN DAY(@FechaInicio) = 27 AND (V.D27 IS NULL OR V.D27 = 'DF') THEN 'A' ELSE V.D27 END,
						V.D28 = CASE WHEN DAY(@FechaInicio) = 28 AND (V.D28 IS NULL OR V.D28 = 'DF') THEN 'A' ELSE V.D28 END,
						V.D29 = CASE WHEN DAY(@FechaInicio) = 29 AND (V.D29 IS NULL OR V.D29 = 'DF') THEN 'A' ELSE V.D29 END,
						V.D30 = CASE WHEN DAY(@FechaInicio) = 30 AND (V.D30 IS NULL OR V.D30 = 'DF') THEN 'A' ELSE V.D30 END,
						V.D31 = CASE WHEN DAY(@FechaInicio) = 31 AND (V.D31 IS NULL OR V.D31 = 'DF') THEN 'A' ELSE V.D31 END
					FROM ReportesApp_RRHH_AsistenciaView V
					WHERE V.Anio = YEAR(@FechaInicio) AND V.Mes = MONTH(@FechaInicio) AND V.CodPlanilla = 'CD' AND IDPersona = @NroPersona
				END
			END

			SET @FechaInicio = DATEADD(DAY,1,@FechaInicio)
		END
	END
	ELSE BEGIN
		SET @exito= '-2 = No se ha mapeado el registro de asistencia para este periodo. Favor de avisar al área de GTH.'
		ROLLBACK
		GOTO Terminar
	END
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
SELECT @exito exito

------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 16-05-2025
-- Description:	LISTAR ADELANTOS DE PLANILLA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ListarAdelantosPlanilla]
@CodGasto VARCHAR(50)
AS
BEGIN
	SELECT A.NumeroAdelanto AS 'NRO_ADELANTO', A.FechaDocumento AS 'FECHA_EMISION', A.Descripcion AS 'DESCRIPCION', A.MontoTotal AS 'MONTO_TOTAL', 	A.CuentaBancaria AS 'CUENTA_BANCARIA', A.Estado AS 'ESTADO', A.UltimoUsuario, A.UltimaFechaModif	FROM AP_GastoAdelanto A	WHERE (A.NumeroDocumentoInterno = LTRIM(RTRIM(@CodGasto)))	ORDER BY A.NumeroAdelanto
END



------------------------------------------------
------------------------------------------------

/*
--INSERTAR TIPO DE CAMBIO DIARIO
INSERT INTO TipoCambioMast (FechaCambio, MonedaCodigo, MonedaCambioCodigo, Factor, FactorCompra, FactorVenta, FactorPromedio, FactorCompraSBS, FactorVentaSBS, Estado, UltimaFechaModif, UltimoUsuario, FechaCambioString, TasaTamex, TasaTamn, TasaAnualTAMEX, TasaAnualTAMN, FactorCobranzaVenta)
VALUES (CONVERT(DATETIME, '20250705'), 'EX', 'LO', 0, 3.500000, 3.500000, 3.500000, 0.000000, 0.000000, 'A', GETDATE(), 'DEMO', '20250705', 0.000000, 0.000000, 0.0000, 0.0000, 0.000000)
*/

/*
SELECT * FROM AP_GastoAdelanto WHERE UltimoUsuario = 'GREYES'
SELECT * FROM AP_GastoAdelantoSustento WHERE UltimoUsuario = 'GREYES'
SELECT * FROM Obligaciones WHERE IngresadoPor = 22529
SELECT * FROM ObligacionesXCuenta WHERE NumeroDocumento = 'TRUJ-249319'
SELECT * FROM AP_ObligacionFlujo WHERE UltimoUsuario = 'GREYES'
SELECT * FROM OrdenPago WHERE NumeroDocumento = 'TRUJ-249319'
SELECT * FROM XOrdenPago WHERE Usuario = 'GREYES'

SELECT * FROM Obligaciones WHERE IngresadoPor = 22529
SELECT * FROM Pagos WHERE UltimoUsuario = 'GREYES'
SELECT * FROM AP_BancoTransaccion WHERE UltimoUsuario = 'GREYES'
SELECT AP_CuentaBancariaBalance.SaldoActual FROM AP_CuentaBancariaBalance WHERE ( AP_CuentaBancariaBalance.CuentaBancaria = 'FONDO GV TRUX' ) AND ( AP_CuentaBancariaBalance.Negociacion = '00' ) 
SELECT * FROM voucherheader WHERE period = '202308'
SELECT * FROM voucherdetail WHERE period = '202308'
SELECT * FROM accountbalance WHERE companyowner = '10000000' AND ledger = '00' AND year = '2023' AND account = '1020005' AND Location = '9999'
SELECT * FROM accountbalance WHERE companyowner = '10000000' AND ledger = '00' AND year = '2023' AND account = '1413002' AND Location = '9999' 

SELECT * FROM AP_CajaChica WHERE UltimoUsuario = 'GREYES'
SELECT * FROM AP_CajaChicaDetalle WHERE CajaChicaNumero = 234919
SELECT * FROM AP_CajaChicaDistribucion WHERE CajaChicaNumero = 234919
SELECT * FROM AP_GastoAdelanto WHERE UltimoUsuario = 'GREYES' AND Estado = 'AP'
SELECT * FROM AP_GastoAdelantoPagos WHERE UltimoUsuario = 'GREYES'
SELECT * FROM AP_GastoAdelantoSustento WHERE UltimoUsuario = 'GREYES' AND NumeroAdelanto = 249322
*/

/*
DELETE FROM ReportesApp_Operacion_Previaje_Registros WHERE NroTicket = 239881
DELETE FROM ReportesApp_Operaciones_TicketGasto_Registro
DELETE FROM AP_GastoAdelanto WHERE UltimoUsuario = 'GREYES'
DELETE FROM AP_GastoAdelantoSustento WHERE UltimoUsuario = 'GREYES'
DELETE FROM Obligaciones WHERE IngresadoPor = 22529
DELETE FROM ObligacionesXCuenta WHERE NumeroDocumento = 'TRUJ-249319'
DELETE FROM AP_ObligacionFlujo WHERE UltimoUsuario = 'GREYES'
DELETE FROM OrdenPago WHERE NumeroDocumento = 'TRUJ-249319'
DELETE FROM XOrdenPago WHERE Usuario = 'GREYES'
DELETE FROM Pagos WHERE UltimoUsuario = 'GREYES'
DELETE FROM AP_BancoTransaccion WHERE UltimoUsuario = 'GREYES'
DELETE FROM voucherheader WHERE period = '202310'
DELETE FROM voucherdetail WHERE period = '202310'
DELETE FROM ReportesApp_Operaciones_TicketGasto_Recibo

DELETE FROM AP_CajaChica WHERE CajaChicaNumero = 234919
DELETE FROM AP_CajaChicaDetalle WHERE CajaChicaNumero = 234919
DELETE FROM AP_CajaChicaDistribucion WHERE CajaChicaNumero = 234919
DELETE FROM AP_GastoAdelanto WHERE NumeroAdelanto = 249319
DELETE FROM AP_GastoAdelantoPagos WHERE NumeroAdelanto = 249319
DELETE FROM AP_GastoAdelantoSustento WHERE NumeroAdelanto = 249319

UPDATE CorrelativosMast
SET CorrelativoNumero = 249318
WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='APAD')
UPDATE CorrelativosMast
SET CorrelativoNumero = 79137
WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='APNO')
UPDATE CorrelativosMast
SET CorrelativoNumero = 316184
WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='APRU')
UPDATE UltimoNumeroPago
SET UltimoNumeroPago = 181098
WHERE UltimoNumeroPago.CuentaBancaria ='FONDO GV TRUX' AND UltimoNumeroPago.TipoPago ='EF'
UPDATE CorrelativosMast
SET CorrelativoNumero = 534997
WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='APBT')
UPDATE AP_CuentaBancariaBalance
SET SaldoActual = -47747086.21
WHERE CuentaBancaria = 'FONDO GV TRUX' AND Negociacion = '00'
UPDATE lastvouchernumber SET month09 = 0 WHERE (lastvouchernumber.companyowner ='10000000') AND (lastvouchernumber.year = '2023') AND (lastvouchernumber.type ='PG')
UPDATE accountbalance SET dollar09 = 0.00, local09 = 0.00, dollarbal09 = 0.00, localbal09 = 0.00
WHERE companyowner = '10000000' AND ledger = '00' AND year = CONVERT(CHAR(4),YEAR(GETDATE())) AND account = '1020005' AND Location = '9999' 
UPDATE accountbalance SET dollar09 = 0.00, local09 = 0.00, dollarbal09 = 0.00, localbal09 = 0.00
WHERE companyowner = '10000000' AND ledger = '00' AND year = CONVERT(CHAR(4),YEAR(GETDATE())) AND account = '1413002' AND Location = '9999' 

UPDATE CorrelativosMast
SET CorrelativoNumero = 234918
WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='APER') 
*/