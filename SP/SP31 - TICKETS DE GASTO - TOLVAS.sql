
-- REEMPLAZAR frmTicketDespacho
-- MODIFICAR FrmPreviajeTolvasPlantillas (AGREGAR BOTÓN DE PLANILLAS)
-- INSERTAR frmListaPlanillasTolvas
-- INSERTAR frmGenerarPlanillaTolvas
-- INSERTAR frmGastosAdicionalesTolvas
-- REEMPLAZAR frmListarPlanillas
-- REEMPLAZAR frmLiquidarGastosViaje

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 31-01-2023
-- Description:	REGISTRAR PLANILLA - TOLVAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_RegistrarPlanillaTolvas]
@idConductor INT,
@idTracto INT,
@idRuta INT,
@idGastoxRutaC INT,
@TotalEntregado DECIMAL(10,2),
@FechaViaje DATETIME,
@TotalDias INT,
@Usuario VARCHAR(50)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT
DECLARE @Persona INT
DECLARE @NombreCompleto VARCHAR(350)
DECLARE @Contador INT
DECLARE @TEMP_ADELANTOS TABLE(Nro INT, NumeroAdelanto INT, Planilla VARCHAR(20), IdProgramacion INT, Sucursal VARCHAR(10), TotalDias INT)
DECLARE @i INT

SET @Exito = '0 = Planilla Registrada.'

SET @Persona = (SELECT P.Persona FROM OP_TR_Conductor C LEFT JOIN PersonaMast P ON P.Persona = C.IdPersona WHERE C.IdConductor = @idConductor)
SET @NombreCompleto = (SELECT LTRIM(RTRIM(P.NombreCompleto)) FROM OP_TR_Conductor C LEFT JOIN PersonaMast P ON P.Persona = C.IdPersona WHERE C.IdConductor = @idConductor)
SET @Contador = (SELECT COUNT(A.NumeroAdelanto) FROM AP_GastoAdelanto A				LEFT JOIN PersonaMast P ON (A.Persona = P.Persona)				LEFT JOIN AP_CajaChica C ON LTRIM(RTRIM(C.NumeroDocumentoInterno)) = LTRIM(RTRIM(A.NumeroDocumentoInterno))				LEFT JOIN ReportesApp_Operaciones_TicketGasto_Registro TG ON TG.CodGasto = LTRIM(RTRIM(A.NumeroDocumentoInterno))				LEFT JOIN ReportesApp_Operacion_Previaje_Registros R ON R.NroTicket = TG.NroTicket				WHERE (A.TipoAdelanto = 'E') AND (A.UnidadNegocio IN ('0001','0002','TRAN')) AND YEAR(A.FechaDocumento)>'2023' AND (A.Estado = 'PA')				AND (TG.EstadoLiquidacion = 0) AND (C.CajaChicaNumero IS NULL) AND (A.Descripcion LIKE '%' + 'GASTOS DE VIAJE' + '%') AND (A.Persona = @Persona))

IF (@Contador >= 2) BEGIN
	SET @Exito = '-1 = El conductor ' + @NombreCompleto +' tiene ' + CONVERT(VARCHAR,@Contador) + ' planillas pendientes de rendir, no puede emitir gasto. Política G.G. Debe pasar a liquidar.'
	GOTO Terminar
END

IF (@Contador = 1) BEGIN	INSERT INTO @TEMP_ADELANTOS	SELECT ROW_NUMBER() OVER(ORDER BY A.NumeroAdelanto ASC), A.NumeroAdelanto, LTRIM(RTRIM(A.NumeroDocumentoInterno)), TG.IdOperacion, 'TRUJILLO',	DATEDIFF(DAY,A.FechaDocumento,GETDATE())	FROM AP_GastoAdelanto A	LEFT JOIN PersonaMast P ON (A.Persona = P.Persona)	LEFT JOIN AP_CajaChica C ON LTRIM(RTRIM(C.NumeroDocumentoInterno)) = LTRIM(RTRIM(A.NumeroDocumentoInterno))	LEFT JOIN ReportesApp_Operaciones_TicketGasto_Registro TG ON TG.CodGasto = LTRIM(RTRIM(A.NumeroDocumentoInterno))	LEFT JOIN ReportesApp_Operacion_Previaje_Registros R ON R.NroTicket = TG.NroTicket	WHERE (A.TipoAdelanto = 'E') AND (A.UnidadNegocio IN ('0001','0002','TRAN')) AND YEAR(A.FechaDocumento)>'2023' AND (A.Estado = 'PA')	AND (TG.EstadoLiquidacion = 0) AND (C.CajaChicaNumero IS NULL) AND (A.Descripcion LIKE '%' + 'GASTOS DE VIAJE' + '%') AND (A.Persona = @Persona)
	ORDER BY A.NumeroAdelanto

	SET @i = 1
	WHILE(@i <= (SELECT COUNT(Nro) FROM @TEMP_ADELANTOS)) BEGIN
		IF ((SELECT IdProgramacion FROM @TEMP_ADELANTOS WHERE Nro = @i) = 1) BEGIN		-- TOLVAS
			IF ((SELECT TotalDias FROM @TEMP_ADELANTOS WHERE Nro = @i) >= 4) BEGIN
				SET @Exito = '-2 = El conductor ' + @NombreCompleto +' tiene la planilla ' + (SELECT Planilla FROM @TEMP_ADELANTOS WHERE Nro = @i)
					+ ' pendiente de rendir con más de 4 días de antiguedad, no puede emitir gasto. Política G.G. Debe pasar a liquidar.'
				GOTO Terminar
			END
			ELSE BEGIN
				SET @Exito = '0 = No hay planillas pendientes.' 
			END
		END

		IF ((SELECT IdProgramacion FROM @TEMP_ADELANTOS WHERE Nro = @i) = 2) BEGIN		-- LINDLEY
			IF ((SELECT TotalDias FROM @TEMP_ADELANTOS WHERE Nro = @i) >= 10) BEGIN
				SET @Exito = '-2 = El conductor ' + @NombreCompleto +' tiene la planilla ' + (SELECT Planilla FROM @TEMP_ADELANTOS WHERE Nro = @i)
					+' pendiente de rendir con más de 10 días de antiguedad, no puede emitir gasto. Política G.G. Debe pasar a liquidar.'
				GOTO Terminar
			END
			ELSE BEGIN
				SET @Exito = '0 = No hay planillas pendientes.' 
			END
		END

		IF ((SELECT IdProgramacion FROM @TEMP_ADELANTOS WHERE Nro = @i) = 4) BEGIN		-- GENERAL
			IF ((SELECT TotalDias FROM @TEMP_ADELANTOS WHERE Nro = @i) >= 10) BEGIN
				SET @Exito = '-2 = El conductor ' + @NombreCompleto +' tiene la planilla ' + (SELECT Planilla FROM @TEMP_ADELANTOS WHERE Nro = @i)
					+' pendiente de rendir con más de 10 días de antiguedad, no puede emitir gasto. Política G.G. Debe pasar a liquidar.'
				GOTO Terminar
			END
			ELSE BEGIN
				SET @Exito = '0 = No hay planillas pendientes.' 
			END
		END

		IF ((SELECT IdProgramacion FROM @TEMP_ADELANTOS WHERE Nro = @i) = 3) BEGIN		-- LIMAGAS - TRUJILLO
			IF (((SELECT TotalDias FROM @TEMP_ADELANTOS WHERE Nro = @i) >= 7) AND ((SELECT Sucursal FROM @TEMP_ADELANTOS WHERE Nro = @i) = 'TRUJILLO')) BEGIN
				SET @Exito = '-2 = El conductor ' + @NombreCompleto +' tiene la planilla ' + (SELECT Planilla FROM @TEMP_ADELANTOS WHERE Nro = @i)
					+' pendiente de rendir con más de 7 días de antiguedad, no puede emitir gasto. Política G.G. Debe pasar a liquidar.'
				GOTO Terminar
			END
			ELSE BEGIN
				SET @Exito = '0 = No hay planillas pendientes.' 
			END
		END

		IF ((SELECT IdProgramacion FROM @TEMP_ADELANTOS WHERE Nro = @i) = 3) BEGIN		-- LIMAGAS - LIMA
			IF (((SELECT TotalDias FROM @TEMP_ADELANTOS WHERE Nro = @i) >= 15) AND ((SELECT Sucursal FROM @TEMP_ADELANTOS WHERE Nro = @i) = 'LIMA')) BEGIN
				SET @Exito = '-2 = El conductor ' + @NombreCompleto +' tiene la planilla ' + (SELECT Planilla FROM @TEMP_ADELANTOS WHERE Nro = @i)
					+' pendiente de rendir con más de 15 días de antiguedad, no puede emitir gasto. Política G.G. Debe pasar a liquidar.'
				GOTO Terminar
			END
			ELSE BEGIN
				SET @Exito = '0 = No hay planillas pendientes.' 
			END
		END

		SET @i = @i + 1
	END
END

/*
IF (CONVERT(DATE,@FechaViaje) < (SELECT TOP(1) DATEADD(DAY,TotalDias,FechaViaje) FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE IdOperacion = 1 AND ViConductor = @idConductor ORDER BY FechaViaje DESC)) BEGIN
	SET @Exito = '-1 = El conductor ya tiene una planilla programada en esa fecha.'
	GOTO Terminar
END
*/

SET @correlativo = (SELECT MAX(idTicketGasto) FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE Anio = YEAR(GETDATE()))
SET @correlativo = ISNULL(@correlativo,0) + 1

BEGIN TRAN
BEGIN TRY
	INSERT INTO ReportesApp_Operaciones_TicketGasto_Registro(idTicketGasto, Anio, CodGasto, IdRuta, IdOperacion, IdTracto, FechaViaje, TotalDias, ViConductor,
															 idGastoXRutaC, TotalEntregado, FechaCreacion, UsuarioCreacion,EstadoLiquidacion,Permiso)
	VALUES(@correlativo, YEAR(GETDATE()), SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo)),6)),
		   @idRuta, 1, @idTracto, CONVERT(DATE,@FechaViaje), @TotalDias, @idConductor, @idGastoxRutaC, @TotalEntregado, GETDATE(), @Usuario, 0, 0)
	
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

---------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 31-01-2023
-- Description:	BUSCAR PLANILLA TOLVAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_BuscarPlanillaTolvas]
@CodGasto INT
AS
BEGIN
	SET NOCOUNT ON;
    SELECT TG.CodGasto, LTRIM(RTRIM(C.Nombre)) AS 'CONDUCTOR', V.NumeroPlaca AS 'TRACTO', RT.Descripcion AS 'RUTA',
	CONVERT(VARCHAR,TG.FechaViaje,103) AS 'FECHA_VIAJE', TG.TotalEntregado AS 'GASTO_TOTAL', ISNULL(TG.GastoDiferencialV,0) AS 'ADICIONAL'
    FROM ReportesApp_Operaciones_TicketGasto_Registro TG
    LEFT JOIN OP_TR_Conductor C WITH(NOLOCK) ON C.IdConductor = TG.ViConductor
    LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = TG.IdTracto
    LEFT JOIN OP_TR_RUTA RT WITH(NOLOCK) ON RT.IDRUTA = TG.IdRuta
    LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera GR WITH(NOLOCK) ON GR.idGastoxRutaC = TG.idGastoXRutaC
    LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle GD WITH(NOLOCK) ON GD.idGastoxRutaC = TG.idGastoXRutaC
    LEFT JOIN ReportesApp_Operaciones_TicketGasto_TipoGasto T WITH(NOLOCK) ON T.idTipoGasto = GD.idTipoGasto
    WHERE TG.CodGasto = @CodGasto
END

---------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 01-02-2023
-- Description:	LISTAR PLANILLAS TOLVAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ListarPlanillasTolvas]
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@NombreConductor VARCHAR(250),
@Planilla VARCHAR(50),
@Pendientes INT
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF @Pendientes = 0 BEGIN
		SELECT TG.idTicketGasto, TG.IdRuta, TG.IdOperacion AS 'TipoProgramacion', TG.IdTracto, TG.ViConductor AS 'IdConductor', TG.idGastoXRutaC,
		CONVERT(VARCHAR,TG.FechaViaje,103) AS 'FECHA_VIAJE', TG.CodGasto AS 'PLANILLA', ISNULL((SELECT TOP 1 CASE WHEN NumeroDocumentoInterno IS NOT NULL THEN ' ' ELSE 'PENDIENTE' END
		FROM AP_GastoAdelanto WHERE NumeroDocumentoInterno = TG.CodGasto AND (Estado = 'TR' OR Estado = 'PA') ORDER BY NumeroAdelanto),'') AS 'CAJA',
		C.Nombre AS 'CONDUCTOR', V.NumeroPlaca AS 'TRACTO', VJ.Descripcion AS 'RUTA', TG.TotalDias AS 'DÍAS',
		TG.TotalEntregado - ISNULL(TG.GastoDiferencialV,0) - ISNULL(TG.GastoDiferencial,0) AS 'IMPORTE_TOTAL',
		ISNULL(TG.GastoDiferencialV,0) AS 'ADICIONAL', ISNULL(TG.GastoDiferencial,0) AS 'CAMBIO_RUTA', TG.UsuarioCreacion AS 'USUARIO'
		FROM ReportesApp_Operaciones_TicketGasto_Registro TG
		LEFT JOIN OP_TR_Ruta VJ ON VJ.IdRuta = TG.IdRuta
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = TG.IdTracto
		LEFT JOIN OP_TR_Conductor C ON C.IdConductor = TG.ViConductor
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera GC ON GC.idGastoXRutaC = TG.idGastoXRutaC
		WHERE (TG.IdOperacion = 1) AND (TG.FechaViaje BETWEEN @FINICIO AND @FFIN) AND (TG.CodGasto IS NULL OR TG.CodGasto LIKE '%' + @Planilla + '%')
		AND (C.Nombre IS NULL OR C.Nombre LIKE '%' + @NombreConductor + '%')
		ORDER BY idTicketGasto DESC
	END

	IF @Pendientes = 1 BEGIN
		SELECT TG.idTicketGasto, TG.IdRuta, TG.IdOperacion AS 'TipoProgramacion', TG.IdTracto, TG.ViConductor AS 'IdConductor', TG.idGastoXRutaC,
		CONVERT(VARCHAR,TG.FechaViaje,103) AS 'FECHA_VIAJE', TG.CodGasto AS 'PLANILLA', ISNULL((SELECT TOP 1 CASE WHEN NumeroDocumentoInterno IS NOT NULL THEN ' ' ELSE 'PENDIENTE' END
		FROM AP_GastoAdelanto WHERE NumeroDocumentoInterno = TG.CodGasto AND (Estado = 'TR' OR Estado = 'PA') ORDER BY NumeroAdelanto),'') AS 'CAJA',
		C.Nombre AS 'CONDUCTOR', V.NumeroPlaca AS 'TRACTO', VJ.Descripcion AS 'RUTA', TG.TotalDias AS 'DÍAS',
		TG.TotalEntregado - ISNULL(TG.GastoDiferencialV,0) - ISNULL(TG.GastoDiferencial,0) AS 'IMPORTE_TOTAL',
		ISNULL(TG.GastoDiferencialV,0) AS 'ADICIONAL', ISNULL(TG.GastoDiferencial,0) AS 'CAMBIO_RUTA', TG.UsuarioCreacion AS 'USUARIO'
		FROM ReportesApp_Operaciones_TicketGasto_Registro TG
		LEFT JOIN OP_TR_Ruta VJ ON VJ.IdRuta = TG.IdRuta
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = TG.IdTracto
		LEFT JOIN OP_TR_Conductor C ON C.IdConductor = TG.ViConductor
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera GC ON GC.idGastoXRutaC = TG.idGastoXRutaC
		WHERE (TG.IdOperacion = 1) AND (TG.EstadoLiquidacion = 0) AND (TG.FechaViaje BETWEEN @FINICIO AND @FFIN) AND (TG.CodGasto IS NULL OR TG.CodGasto LIKE '%' + @Planilla + '%')
		AND (C.Nombre IS NULL OR C.Nombre LIKE '%' + @NombreConductor + '%')
		ORDER BY idTicketGasto DESC
	END
END

--------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 01-02-2023
-- Description:	ELIMINAR PLANILLAS - TOLVAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_EliminarPlanillaTolvas]
@CodGasto VARCHAR(50),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Planilla eliminada.'

/*
IF(EXISTS(SELECT * FROM Obligaciones WHERE NumeroDocumentoInterno = CONVERT(CHAR(20),@CodGasto) AND EstadoDocumento = 'AP')) BEGIN
	SET @Exito = '-1 = No puede eliminar esta planilla porque el pago ya fue aprobado.'
	GOTO Terminar
END
*/

BEGIN TRAN
BEGIN TRY
	IF (EXISTS(SELECT * FROM AP_GastoAdelanto WHERE NumeroDocumentoInterno = @CodGasto AND (Estado = 'PA'))) BEGIN
		SET @Exito = '-1 = Esta planilla ya se encuentra pagada. Favor de ir a liquidarla para desvincularla.'
		ROLLBACK
		GOTO Terminar
	END
	ELSE BEGIN
		UPDATE ReportesApp_Operaciones_TicketGasto_Viaticos
		SET CodGasto = NULL
		WHERE CodGasto = @CodGasto

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

--------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 01-02-2023
-- Description:	INSERTAR VIATICOS - TOLVAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_InsertarViaticosTolvas]
@CodGasto VARCHAR(50),
@idConductor INT,
@Fecha DATETIME,
@idTipoViatico INT,
@Monto DECIMAL(10,2),
@Descripcion VARCHAR(250),
@Motivo VARCHAR(350)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

SET @Exito = '0 = Exito.'
SET @correlativo = (SELECT MAX(idViatico) FROM ReportesApp_Operaciones_TicketGasto_Viaticos WHERE idConductor = @idConductor)
SET @correlativo = ISNULL(@correlativo,0) + 1

/*
IF(EXISTS(SELECT * FROM ReportesApp_Operaciones_TicketGasto_Viaticos WHERE CONVERT(DATE,Fecha) = CONVERT(DATE,@Fecha) AND idConductor = @idConductor)) BEGIN
	SET @Exito = '-1 = El conductor ya tiene este viático registrado.'
	GOTO Terminar
END
*/

BEGIN TRAN
BEGIN TRY
	INSERT INTO ReportesApp_Operaciones_TicketGasto_Viaticos(idViatico,CodGasto,idConductor,Fecha,idTipoViatico,Monto,Descripcion,Motivo)
	VALUES(@correlativo,@CodGasto,@idConductor,@Fecha,@idTipoViatico,@Monto,@Descripcion,@Motivo)

	UPDATE ReportesApp_Operaciones_TicketGasto_Registro
	SET TotalEntregado = TotalEntregado + @Monto, GastoDiferencialV = ISNULL(GastoDiferencialV,0) + @Monto
	WHERE CodGasto = @CodGasto
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

----------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 01-02-2023
-- Description:	ELIMINAR VIATICOS - TOLVAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_EliminarGastosTolvas]
@idViatico INT,
@idConductor INT,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @Monto DECIMAL(10,2)

SET @Exito = '0 = Gasto eliminado.'

IF (@idViatico = 0)
BEGIN
	SET @Exito = '-1 = El gasto seleccionado no existe.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	SET @Monto = (SELECT Monto FROM ReportesApp_Operaciones_TicketGasto_Viaticos WHERE idViatico = @idViatico AND idConductor = @idConductor)

	UPDATE ReportesApp_Operaciones_TicketGasto_Registro
	SET TotalEntregado = TotalEntregado - @Monto, GastoDiferencialV = ISNULL(GastoDiferencialV,0) - @Monto
	WHERE CodGasto = (SELECT CodGasto FROM ReportesApp_Operaciones_TicketGasto_Viaticos WHERE idViatico = @idViatico AND idConductor = @idConductor)

	INSERT INTO ReportesApp_Operaciones_TicketGasto_Viaticos_Historial (idViatico,CodGasto,NroTicket,idConductor,Fecha,idTipoViatico,Monto,
				Descripcion,Motivo,UsuarioElimina,FechaElimina)
	SELECT idViatico,CodGasto,NroTicket,idConductor,Fecha,idTipoViatico,Monto,Descripcion,Motivo,@Usuario,GETDATE()
	FROM ReportesApp_Operaciones_TicketGasto_Viaticos
	WHERE idViatico = @idViatico AND idConductor = @idConductor

	DELETE FROM ReportesApp_Operaciones_TicketGasto_Viaticos
	WHERE idViatico = @idViatico AND idConductor = @idConductor
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

------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 23-08-2023
-- Description:	GENERAR PAGO DE PLANILLA SIN VIAJE
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_PagarSinViaje]
@idConductor INT,
@Planilla INT,
@Monto DECIMAL(10,2),
@Usuario VARCHAR(100)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @Nro INT
DECLARE @NroConductor INT
DECLARE @NroUsuario INT
DECLARE @NroAdelanto INT
DECLARE @MontoTotal DECIMAL(10,2)
DECLARE @GastoDiferencialV DECIMAL(10,2)
DECLARE @GastoDiferencial DECIMAL(10,2)
DECLARE @Proyecto VARCHAR(15)
DECLARE @TipoCambio MONEY
DECLARE @Persona INT
DECLARE @Contador INT
DECLARE @CodigoDocumento CHAR(10)
DECLARE @CuentaBancaria CHAR(15)
DECLARE @Sucursal CHAR(4)
DECLARE @UnidadReplicacion CHAR(4)

IF(EXISTS(SELECT * FROM Obligaciones WHERE NumeroDocumentoInterno = CONVERT(CHAR(20),@Planilla) AND EstadoDocumento = 'AP')) BEGIN
	SET @Exito = '-1 = El pago de este viaje ya fue registrado.'
	GOTO Terminar
END

IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_TicketGasto_UsuarioSucursal WHERE Usuario = @Usuario AND Estado = 'A')) BEGIN
	SET @CuentaBancaria = (SELECT CuentaBancaria FROM ReportesApp_Operaciones_TicketGasto_UsuarioSucursal WHERE Usuario = @Usuario)
	SET @Sucursal = (SELECT Sucursal FROM ReportesApp_Operaciones_TicketGasto_UsuarioSucursal WHERE Usuario = @Usuario)
	SET @UnidadReplicacion = (SELECT UnidadReplicacion FROM ReportesApp_Operaciones_TicketGasto_UsuarioSucursal WHERE Usuario = @Usuario)
END
ELSE BEGIN
	SET @Exito = '-1 = Este usuario está inactivo o no tiene una sede registrada.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	SET @GastoDiferencial = (SELECT GastoDiferencial FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE CodGasto = CONVERT(VARCHAR,@Planilla))
	SET @GastoDiferencialV = (SELECT GastoDiferencialV FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE CodGasto = CONVERT(VARCHAR,@Planilla))
	SET @MontoTotal = @Monto + ISNULL(@GastoDiferencial,0) + ISNULL(@GastoDiferencialV,0)

	UPDATE CorrelativosMast
	SET CorrelativoNumero = (SELECT CorrelativoNumero + 1 FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='APAD')),
		UltimaFechaModif = GETDATE()
	WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='APAD')
	
	SET @Nro = (SELECT CorrelativosMast.CorrelativoNumero FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='APAD'))

	DELETE FROM AP_GastoAdelantoSustento WHERE (AP_GastoAdelantoSustento.UnidadReplicacion ='TRUJ') AND (AP_GastoAdelantoSustento.TipoAdelanto ='E') AND (AP_GastoAdelantoSustento.NumeroAdelanto = @Nro) 

	--INSERTAR DATOS EN CABECERA DE REPORTES DE GASTO
	SET @NroConductor = (SELECT P.Persona FROM PersonaMast P LEFT JOIN OP_TR_Conductor C ON P.Persona = C.IdPersona WHERE C.IdConductor = @idConductor)				 
	SET @NroUsuario = (SELECT P.Persona FROM Usuario U LEFT JOIN PersonaMast P ON P.NombreCompleto = U.Nombre WHERE Usuario = @Usuario)
	
	--INSERTAR ADELANTO DE GASTO
	INSERT INTO AP_GastoAdelanto(NumeroAdelanto,CompaniaSocio,Clasificacion,FechaDocumento,TipoPago,Persona,PreparadoPor,FechaPreparacion,MonedaDocumento,MontoTotal,SaldoAdelanto,Descripcion,Estado,UltimoUsuario,
								 UltimaFechaModif,FechaEsperadaPago,TipoAdelanto,NumeroDocumentoInterno,FlujodeCaja,UnidadReplicacion,PartidaPresupuestal,PersonaPagara,ConceptoGasto,CentroCostos,EmpleadoAutorizado,
								 UnidadNegocio,RegComprasMontoAfecto,RegComprasMontoNoAfecto,RegComprasMontoIgv,CuentaBancaria)
	VALUES (@Nro,'10000000','ER',GETDATE(),'EF', @NroConductor, @NroUsuario, GETDATE(), 'LO', @MontoTotal, @MontoTotal, 'GASTOS DE VIAJE - PL ' + CONVERT(VARCHAR(30),@Planilla),
	'PR', @Usuario, GETDATE(), GETDATE(), 'E', CONVERT(VARCHAR(30),@Planilla), '051', 'TRUJ', '9999', @NroConductor, '0006', '010201', @NroUsuario, 'TRAN', 0.00, 0.00, 0.00, @CuentaBancaria)

	INSERT INTO AP_GastoAdelantoSustento(UnidadReplicacion,TipoAdelanto,NumeroAdelanto,Secuencia,ConceptoGasto,NumeroVeces,Moneda,PrecioUnitario,MontoTotal,UltimoUsuario,UltimaFechaModif,CuentaContable,MontoPresupuesto)
	VALUES ('TRUJ', 'E', @Nro, 1, '0006', 8, 'LO', @MontoTotal, @MontoTotal, @Usuario, GETDATE(), '1413002', 0.00)

	UPDATE AP_GastoAdelanto
	SET AprobadoPor = @NroUsuario, FechaAprobacion = GETDATE(), Estado = 'AP'
	WHERE NumeroAdelanto = @Nro AND TipoAdelanto = 'E' AND UnidadReplicacion = 'TRUJ'

	--INSERTAR OBLIGACIÓN
	DECLARE @Nro2 INT
			
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
	VALUES (@NroConductor,'AE','TRUJ-'+CONVERT(VARCHAR(10),@Nro),@CuentaBancaria,'100000','EF',GETDATE(),GETDATE(),GETDATE(),'S','INAFEC','LO','D','LO',@MontoTotal,0.00,0.00,@MontoTotal,0.00,0.00,@MontoTotal,
	0.00,0,0,0,0,@NroUsuario, @NroUsuario, 'RV', 'S', 'N', CONVERT(CHAR(4),YEAR(GETDATE()))+CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(GETDATE()))),2)),0,0,0,@Nro2, 'GASTOS DE VIAJE - PL ' + CONVERT(VARCHAR(30),@Planilla),
	GETDATE(),'TRAN','N','N','TRUJ',0,0.00,CONVERT(VARCHAR(30),@Planilla),'010201','9999','051','010201',GETDATE(),@NroConductor, 'N', 'N', 0.00, GETDATE(), 'N', 'N', 'N')

	INSERT INTO ObligacionesXCuenta (Descripcion,Proveedor,NumeroDocumento,Linea,Monto,CentroCosto,CuentaContable,PartidaPresupuestal,Persona,DocumentoReferencia,Sucursal,FlujodeCaja,NoAfectoIGVFlag,TipoDocumento)
	VALUES ('GASTOS DE VIAJE - PL ' + CONVERT(VARCHAR(30),@Planilla),@NroConductor,'TRUJ-'+CONVERT(VARCHAR(10),@Nro),1,@MontoTotal,'010201','1413002','9999', @NroConductor,
	'AE-TRUJ-'+CONVERT(VARCHAR(10),@Nro),@Sucursal, '051', 'N', 'AE')

	UPDATE AP_GastoAdelanto
	SET MonedaDocumento = 'LO',ObligacionTipoDocumento = 'AE',ObligacionNumeroDocumento = 'TRUJ-'+CONVERT(VARCHAR(10),@Nro),Estado = 'TR'
	WHERE UnidadReplicacion = 'TRUJ' AND TipoAdelanto = 'E' AND NumeroAdelanto = @Nro 

	INSERT INTO AP_ObligacionFlujo (Proveedor,TipoDocumento,NumeroDocumento,Secuencia,AreaDocumentaria,Comentario,Estado,UltimoUsuario,UltimaFechaModif)
	VALUES (@NroConductor, 'AE','TRUJ-'+CONVERT(VARCHAR(10),@Nro), 1,'001','Recepción Inicial','A',@Usuario,GETDATE())

	DECLARE @NombreConductor VARCHAR(250)
	SET @NombreConductor = (SELECT P.NombreCompleto FROM PersonaMast P LEFT JOIN OP_TR_Conductor C ON P.Persona = C.IdPersona WHERE C.IdConductor = @idConductor)

	--INSERTAR ORDEN DE PAGO
	INSERT INTO OrdenPago (SistemaFuente,Proveedor,TipoDocumento,NumeroDocumento,PagarA,CompaniaCodigo,CuentaBancaria,TipoPago,ChequeIndividual,FechaVencimiento,MonedaDocumento,MonedaPago,MontoMoneda,TipodeCambio,
						   BatchNumber,FechaTransferencia,Estado,FlujodeCaja,RegistroNumero,FechaDocumento,PagoDiferidoFlag,UnidadNegocio)
	VALUES ('AP',@NroConductor,'AE','TRUJ-'+CONVERT(VARCHAR(10),@Nro),@NombreConductor,'100000',@CuentaBancaria,'EF','N',GETDATE(),'LO','LO',@MontoTotal,0,0,GETDATE(),'AP','051', @Nro2, GETDATE(),'N','TRAN')

	SET @TipoCambio = (CONVERT(MONEY,(SELECT TipoCambioMast.FactorVenta FROM TipoCambioMast WHERE TipoCambioMast.MonedaCodigo ='EX' AND TipoCambioMast.MonedaCambioCodigo ='LO' AND TipoCambioMast.FechaCambio =CONVERT(DATE,GETDATE()))))

	UPDATE Obligaciones
	SET AprobadoCP1 = @NroUsuario, EstadoDocumento = 'AP', Voucher = CONVERT(CHAR(4),YEAR(GETDATE()))+CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(GETDATE()))),2)),
		FechaAprobacion = GETDATE(), TipodeCambioProvision = @TipoCambio, UltimoUsuario = @Usuario, UltimaFechaModif = GETDATE()
	WHERE Proveedor = @NroConductor AND TipoDocumento = 'AE' AND NumeroDocumento = 'TRUJ-'+CONVERT(VARCHAR(10),@Nro)

	DELETE XOrdenPago WHERE XOrdenPago.Usuario = @Usuario

	INSERT INTO XOrdenPago (TipoRegistro,SistemaFuente,Proveedor,TipoDocumento,NumeroDocumento,CompaniaCodigo,PagarA,CuentaBancaria,TipoPago,ChequeIndividual,MonedaPago,FechaVencimiento,Estado,MonedaDocumento,MontoMoneda,Usuario,MontoOriginal)
	VALUES ('D','AP',@NroConductor,'AE','TRUJ-'+CONVERT(VARCHAR(10),@Nro),'100000',@NombreConductor,@CuentaBancaria,'EF','N','LO',GETDATE(),'A','LO',@MontoTotal,@Usuario,@MontoTotal)
	
	INSERT INTO XOrdenPago (TipoRegistro,SistemaFuente,Proveedor,TipoDocumento,NumeroDocumento,CompaniaCodigo,PagarA,CuentaBancaria,TipoPago,ChequeIndividual,MonedaPago,Estado,MontoMoneda,Usuario)
	VALUES ('S','XX',@NroConductor,'ZZ','000001','100000',@NombreConductor,@CuentaBancaria,'EF','N','LO','A',@MontoTotal,@Usuario)

	UPDATE ReportesApp_Operaciones_TicketGasto_Registro
	SET GastoDiferencialV = NULL, GastoDiferencial = NULL
	WHERE CodGasto = CONVERT(VARCHAR,@Planilla)

	SET @Exito = '0 = Gasto Pagado Exitosamente.'
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

------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 03-02-2023
-- Description:	BUSCAR PROVEEDOR
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_BuscarProveedor]
@Proveedor VARCHAR(250)
AS
BEGIN
	SELECT TOP(20) LTRIM(RTRIM(PM.DocumentoFiscal)) AS NroRUC, LTRIM(RTRIM(PM.NombreCompleto)) AS NombreCompleto
	FROM PersonaMast PM
	WHERE PM.EsProveedor = 'S' AND PM.EsEmpleado = 'N' AND Estado = 'A'
	AND (LTRIM(RTRIM(PM.NombreCompleto)) LIKE '%' + @Proveedor + '%')
END

------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 03-02-2023
-- Description:	INSERTAR / MODIFICAR PEAJES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_InsertarModificarPeajes]
@idPeaje INT,
@Peaje DECIMAL(10,2)
AS
DECLARE @TEMP_PEAJE TABLE(Nro INT,
					idGastoxRutaC INT)
DECLARE @correlativo INT
DECLARE @Contador INT
DECLARE @NroRUC CHAR(20)
DECLARE @idGasto2 INT
DECLARE @Exito VARCHAR(MAX)
DECLARE @MontoAfecto DECIMAL(10,2)
DECLARE @MontoImpuesto DECIMAL(10,2)
BEGIN TRAN
BEGIN TRY
	SET @MontoAfecto = (@Peaje * 100) / 118
	SET @MontoImpuesto = @Peaje - @MontoAfecto

	IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_TicketGasto_GastoPeaje WHERE idPeaje = @idPeaje)) BEGIN
		SET @NroRUC = (SELECT LTRIM(RTRIM(PM.DocumentoFiscal)) FROM OP_TR_Peaje P LEFT JOIN PersonaMast PM ON PM.Persona = P.Proveedor WHERE P.idPeaje = @idPeaje)

		UPDATE ReportesApp_Operaciones_TicketGasto_GastoPeaje
		SET NroRUC = @NroRUC, Monto = @Peaje, MontoAfecto = @MontoAfecto, MontoImpuesto = @MontoImpuesto
		WHERE idPeaje = @idPeaje
		
		UPDATE ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle
		SET Gasto = @Peaje
		WHERE idPeaje = @idPeaje

		SET @Contador = 1

		INSERT INTO @TEMP_PEAJE
		SELECT ROW_NUMBER() OVER(ORDER BY idGastoxRutaC ASC), idGastoxRutaC FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle WHERE idPeaje = @idPeaje

		WHILE(@Contador <= (SELECT COUNT(Nro) FROM @TEMP_PEAJE)) BEGIN
			SET @idGasto2 = (SELECT idGastoxRutaC FROM @TEMP_PEAJE WHERE Nro = @Contador)
			
			UPDATE ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera
			SET GastoTotal = (SELECT SUM(Gasto) FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle WHERE Adicional = 0 AND idGastoxRutaC = @idGasto2)
			WHERE idGastoxRutaC = @idGasto2

			SET @Contador = @Contador + 1
		END

		SET @Exito = '0 = Precio de Peaje Actualizado.'
	END
	ELSE BEGIN
		SET @NroRUC = (SELECT LTRIM(RTRIM(PM.DocumentoFiscal)) FROM OP_TR_Peaje P LEFT JOIN PersonaMast PM ON PM.Persona = P.Proveedor WHERE P.idPeaje = @idPeaje)

		INSERT INTO ReportesApp_Operaciones_TicketGasto_GastoPeaje (idPeaje, NroRUC, Monto, MontoAfecto, MontoImpuesto)
		VALUES(@idPeaje, @NroRUC, @Peaje, @MontoAfecto, @MontoImpuesto)

		SET @Exito = '0 = Precio de Peaje Creado.'
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

------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 23-08-2023
-- Description:	PAGAR VIATICOS DE TOLVAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_PagarViaticosTolvas]
@idConductor INT,
@Planilla INT,
@Usuario VARCHAR(100)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @Nro INT
DECLARE @NroConductor INT
DECLARE @NroUsuario INT
DECLARE @NroAdelanto INT
DECLARE @MontoTotal DECIMAL(10,2)
DECLARE @Motivo VARCHAR(250)
DECLARE @GastoDiferencialV DECIMAL(10,2)
DECLARE @GastoDiferencial DECIMAL(10,2)
DECLARE @Proyecto VARCHAR(15)
DECLARE @TipoCambio MONEY
DECLARE @CuentaBancaria CHAR(15)
DECLARE @Sucursal CHAR(4)

SET @Motivo = 'VIATICO ADICIONAL: PL - ' + CONVERT(VARCHAR(30),@Planilla)

SET @MontoTotal = (SELECT GastoDiferencialV FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE CodGasto = CONVERT(VARCHAR(30),@Planilla))
IF (@MontoTotal IS NULL) BEGIN
	SET @Exito = '-1 = Esta planilla no tiene viáticos adicionales.'
	GOTO Terminar
END

IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_TicketGasto_UsuarioSucursal WHERE Usuario = @Usuario AND Estado = 'A')) BEGIN
	SET @CuentaBancaria = (SELECT CuentaBancaria FROM ReportesApp_Operaciones_TicketGasto_UsuarioSucursal WHERE Usuario = @Usuario)
	SET @Sucursal = (SELECT Sucursal FROM ReportesApp_Operaciones_TicketGasto_UsuarioSucursal WHERE Usuario = @Usuario)
END
ELSE BEGIN
	SET @Exito = '-1 = Este usuario está inactivo o no tiene una sede registrada.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	SET @GastoDiferencial = (SELECT GastoDiferencial FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE CodGasto = CONVERT(VARCHAR,@Planilla))
	SET @GastoDiferencialV = (SELECT GastoDiferencialV FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE CodGasto = CONVERT(VARCHAR,@Planilla))
	SET @MontoTotal = ISNULL(@GastoDiferencial,0) + ISNULL(@GastoDiferencialV,0)

	UPDATE CorrelativosMast
	SET CorrelativoNumero = (SELECT CorrelativoNumero + 1 FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='APAD')),
		UltimaFechaModif = GETDATE()
	WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='APAD')

	SET @Nro = (SELECT CorrelativosMast.CorrelativoNumero FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='APAD'))

	DELETE FROM AP_GastoAdelantoSustento WHERE (AP_GastoAdelantoSustento.UnidadReplicacion ='TRUJ') AND (AP_GastoAdelantoSustento.TipoAdelanto ='E') AND (AP_GastoAdelantoSustento.NumeroAdelanto = @Nro) 

	--INSERTAR DATOS EN CABECERA DE REPORTES DE GASTO
	SET @NroConductor = (SELECT P.Persona FROM PersonaMast P LEFT JOIN OP_TR_Conductor C ON P.Persona = C.IdPersona WHERE C.IdConductor = @idConductor)				 
	SET @NroUsuario = (SELECT P.Persona FROM Usuario U LEFT JOIN PersonaMast P ON P.NombreCompleto = U.Nombre WHERE Usuario = @Usuario)

	--INSERTAR ADELANTO DE GASTO
	INSERT INTO AP_GastoAdelanto(NumeroAdelanto,CompaniaSocio,Clasificacion,FechaDocumento,TipoPago,Persona,PreparadoPor,FechaPreparacion,MonedaDocumento,MontoTotal,SaldoAdelanto,Descripcion,Estado,UltimoUsuario,
								UltimaFechaModif,FechaEsperadaPago,TipoAdelanto,NumeroDocumentoInterno,FlujodeCaja,UnidadReplicacion,PartidaPresupuestal,PersonaPagara,ConceptoGasto,CentroCostos,EmpleadoAutorizado,
								UnidadNegocio,RegComprasMontoAfecto,RegComprasMontoNoAfecto,RegComprasMontoIgv,CuentaBancaria)
	VALUES (@Nro,'10000000','ER',GETDATE(),'EF', @NroConductor, @NroUsuario, GETDATE(), 'LO', @MontoTotal, @MontoTotal, @Motivo,
	'PR', @Usuario, GETDATE(), GETDATE(), 'E', CONVERT(VARCHAR(30),@Planilla), '051', 'TRUJ', '9999', @NroConductor, '0006', '010201', @NroUsuario, 'TRAN', 0.00, 0.00, 0.00, @CuentaBancaria)
	
	INSERT INTO AP_GastoAdelantoSustento(UnidadReplicacion,TipoAdelanto,NumeroAdelanto,Secuencia,ConceptoGasto,NumeroVeces,Moneda,PrecioUnitario,MontoTotal,UltimoUsuario,UltimaFechaModif,CuentaContable,MontoPresupuesto)
	VALUES ('TRUJ', 'E', @Nro, 1, '0006', 8, 'LO', @MontoTotal, @MontoTotal, @Usuario, GETDATE(), '1413002', 0.00)

	UPDATE AP_GastoAdelanto
	SET AprobadoPor = @NroUsuario, FechaAprobacion = GETDATE(), Estado = 'AP'
	WHERE NumeroAdelanto = @Nro AND TipoAdelanto = 'E' AND UnidadReplicacion = 'TRUJ'

	--INSERTAR OBLIGACIÓN
	UPDATE CorrelativosMast
	SET CorrelativoNumero = (SELECT CorrelativoNumero + 1 FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie = 'APNO')),
		UltimaFechaModif = GETDATE()
	WHERE (CorrelativosMast.CompaniaCodigo = '999999') AND (CorrelativosMast.TipoComprobante = 'SY') AND (CorrelativosMast.Serie = 'APNO')
	
	DECLARE @Nro2 INT = (SELECT CorrelativosMast.CorrelativoNumero FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo = '999999') AND (CorrelativosMast.TipoComprobante = 'SY') AND (CorrelativosMast.Serie = 'APNO'))

	INSERT INTO Obligaciones (Proveedor,TipoDocumento,NumeroDocumento,CuentaBancaria,CompaniaCodigo,TipoPago,FechaRegistro,FechaVencimiento,FechaVencimientoOriginal,GenerarPago,TipoServicio,MonedaDocumento,ConversionRequerida,
							  MonedaPago,MontoObligacion,MontoImpuestoVentas,MontoNoAfecto,MontoImponible,MontoAdelantos,MontoImpuestos,NetoMonedaLocal,NetoMonedaExtranjera,TipoDeCambio,AprobadoPor,AprobadoCP1,AprobadoCP2,
							  IngresadoPor,RevisadoPor,EstadoDocumento,ContabilizacionPendiente,ChequeIndividual,Voucher,NumeroPago,NumeroProceso,ProcesoSecuencia,RegistroNumero,Comentarios,UltimaFechaModif,UnidadNegocio,
							  FacturaAfectaSplitFlag,FactorRValidacion,UnidadReplicacion,CanjeRegistroNumero,MontoPagoParcial,NumeroDocumentoInterno,CentroCosto,PartidaPresupuestal,FlujodeCaja,CentroCostoCP,FechaRecepcion,
							  ProveedorPagarA,ControlPresupuestalFlag,CargoFlag,MontoCreditoFiscal,FechaDocumento,AfectoIGVFlag,DiferidoFlag,AdelantoFlag)
	VALUES (@NroConductor,'AE','TRUJ-'+CONVERT(VARCHAR(10),@Nro),@CuentaBancaria,'100000','EF',GETDATE(),GETDATE(),GETDATE(),'S','INAFEC','LO','D','LO',@MontoTotal,0.00,0.00,@MontoTotal,0.00,0.00,@MontoTotal,
	0.00,0,0,0,0,@NroUsuario, @NroUsuario, 'RV', 'S', 'N', CONVERT(CHAR(4),YEAR(GETDATE()))+CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(GETDATE()))),2)),0,0,0,@Nro2, @Motivo, GETDATE(),
	'TRAN','N','N','TRUJ',0,0.00,CONVERT(VARCHAR(30),@Planilla),'010201','9999','051','010201',GETDATE(),@NroConductor, 'N', 'N', 0.00, GETDATE(), 'N', 'N', 'N')

	INSERT INTO ObligacionesXCuenta (Descripcion,Proveedor,NumeroDocumento,Linea,Monto,CentroCosto,CuentaContable,PartidaPresupuestal,Persona,DocumentoReferencia,Sucursal,FlujodeCaja,NoAfectoIGVFlag,TipoDocumento)
	VALUES (@Motivo,@NroConductor,'TRUJ-'+CONVERT(VARCHAR(10),@Nro),1,@MontoTotal,'010201','1413002','9999', @NroConductor,
	'AE-TRUJ-'+CONVERT(VARCHAR(10),@Nro),@Sucursal, '051', 'N', 'AE')

	UPDATE AP_GastoAdelanto
	SET MonedaDocumento = 'LO',ObligacionTipoDocumento = 'AE',ObligacionNumeroDocumento = 'TRUJ-'+CONVERT(VARCHAR(10),@Nro),Estado = 'TR'
	WHERE UnidadReplicacion = 'TRUJ' AND TipoAdelanto = 'E' AND NumeroAdelanto = @Nro 

	INSERT INTO AP_ObligacionFlujo (Proveedor,TipoDocumento,NumeroDocumento,Secuencia,AreaDocumentaria,Comentario,Estado,UltimoUsuario,UltimaFechaModif)
	VALUES (@NroConductor, 'AE','TRUJ-'+CONVERT(VARCHAR(10),@Nro), 1,'001','Recepción Inicial','A',@Usuario,GETDATE())

	DECLARE @NombreConductor VARCHAR(250)
	SET @NombreConductor = (SELECT P.NombreCompleto FROM PersonaMast P LEFT JOIN OP_TR_Conductor C ON P.Persona = C.IdPersona WHERE C.IdConductor = @idConductor)

	--INSERTAR ORDEN DE PAGO
	INSERT INTO OrdenPago (SistemaFuente,Proveedor,TipoDocumento,NumeroDocumento,PagarA,CompaniaCodigo,CuentaBancaria,TipoPago,ChequeIndividual,FechaVencimiento,MonedaDocumento,MonedaPago,MontoMoneda,TipodeCambio,
						   BatchNumber,FechaTransferencia,Estado,FlujodeCaja,RegistroNumero,FechaDocumento,PagoDiferidoFlag,UnidadNegocio)
	VALUES ('AP',@NroConductor,'AE','TRUJ-'+CONVERT(VARCHAR(10),@Nro),@NombreConductor,'100000',@CuentaBancaria,'EF','N',GETDATE(),'LO','LO',@MontoTotal,0,0,GETDATE(),'AP','051', @Nro2, GETDATE(),'N','TRAN')

	SET @TipoCambio = (CONVERT(MONEY,(SELECT TipoCambioMast.FactorVenta FROM TipoCambioMast WHERE TipoCambioMast.MonedaCodigo ='EX' AND TipoCambioMast.MonedaCambioCodigo ='LO' AND TipoCambioMast.FechaCambio =CONVERT(DATE,GETDATE()))))

	UPDATE Obligaciones
	SET AprobadoCP1 = @NroUsuario, EstadoDocumento = 'AP', Voucher = CONVERT(CHAR(4),YEAR(GETDATE()))+CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(GETDATE()))),2)),
		FechaAprobacion = GETDATE(), TipodeCambioProvision = @TipoCambio, UltimoUsuario = @Usuario, UltimaFechaModif = GETDATE()
	WHERE Proveedor = @NroConductor AND TipoDocumento = 'AE' AND NumeroDocumento = 'TRUJ-'+CONVERT(VARCHAR(10),@Nro)

	DELETE XOrdenPago WHERE XOrdenPago.Usuario = @Usuario

	INSERT INTO XOrdenPago (TipoRegistro,SistemaFuente,Proveedor,TipoDocumento,NumeroDocumento,CompaniaCodigo,PagarA,CuentaBancaria,TipoPago,ChequeIndividual,MonedaPago,FechaVencimiento,Estado,MonedaDocumento,MontoMoneda,Usuario,MontoOriginal)
	VALUES ('D','AP',@NroConductor,'AE','TRUJ-'+CONVERT(VARCHAR(10),@Nro),'100000',@NombreConductor,@CuentaBancaria,'EF','N','LO',GETDATE(),'A','LO',@MontoTotal,@Usuario,@MontoTotal)

	INSERT INTO XOrdenPago (TipoRegistro,SistemaFuente,Proveedor,TipoDocumento,NumeroDocumento,CompaniaCodigo,PagarA,CuentaBancaria,TipoPago,ChequeIndividual,MonedaPago,Estado,MontoMoneda,Usuario)
	VALUES ('S','XX',@NroConductor,'ZZ','000001','100000',@NombreConductor,@CuentaBancaria,'EF','N','LO','A',@MontoTotal,@Usuario)

	UPDATE ReportesApp_Operaciones_TicketGasto_Registro
	SET GastoDiferencialV = NULL, GastoDiferencial = NULL
	WHERE CodGasto = CONVERT(VARCHAR,@Planilla)

	SET @Exito = '0 = Gasto actualizado.'
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

------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 03-02-2023
-- Description:	BUSCAR PROVEEDOR
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_FiltrarPeaje]
@Planilla INT,
@IdRuta INT
AS
BEGIN
	DECLARE @idGastoXRutaC INT = (SELECT TOP(1) ISNULL(idGastoxRutaC,69) FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera WHERE IdRuta = @IdRuta)

	IF (@idGastoxRutaC = 0 OR @idGastoxRutaC IS NULL) BEGIN
		SET @idGastoxRutaC = 69
	END

	SELECT DISTINCT GD.idPeaje, GD.Descripcion AS 'PEAJE' FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle GD
	WHERE GD.idPeaje != 0 AND GD.idGastoxRutaC = @idGastoxRutaC
END

---------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 06-02-2023
-- Description:	LISTAR PEAJES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ListarPeajes]
AS
BEGIN
	SELECT P.idPeaje, P.Descripcion AS 'PEAJE', PM.DocumentoFiscal AS 'RUC', PM.NombreCompleto AS 'PROVEEDOR', GP.Monto AS 'PRECIO'
	FROM OP_TR_Peaje P
	LEFT JOIN PersonaMast PM ON PM.Persona = P.Proveedor
	LEFT JOIN OP_TR_PeajeCosto PC ON PC.IdPeaje = P.IdPeaje
	LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoPeaje GP ON GP.idPeaje = P.IdPeaje
	WHERE P.Estado = 2 AND PC.TipoVehiculo = 1 AND PC.SubTipoVehiculo = 1
	ORDER BY PM.DocumentoFiscal
END