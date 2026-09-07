
-- MODIFICAR TABLA ReportesApp_Mantenimiento_FallasMecanicas_Registros

-------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 27-05-2023
-- Description:	INSERTAR REGISTRO DE FALLAS MECÁNICAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_Insertar]
@NroTicket INT,
@idTipoAuxilio INT,
@TipoFalla VARCHAR(50),
@Motivo VARCHAR(250),
@FechaInicio DATE,
@HoraInicio TIME(3),
@Ubicacion VARCHAR(500),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

SET @Exito = '0 = Falla Mecánica Añadida. N°: '
SET @correlativo = (SELECT MAX(idFalla) FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros)
SET @correlativo = ISNULL(@correlativo,0) + 1 

IF (@FechaInicio > GETDATE())
BEGIN
	SET @Exito = '-1 = Solo puede elegir hasta el día de hoy.'
	GOTO Terminar
END

IF (@NroTicket = 0)
BEGIN
	SET @Exito = '-2 = El ticket seleccionado no existe.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	INSERT INTO ReportesApp_Mantenimiento_FallasMecanicas_Registros(idFalla, NroTicket, CodFalla, TipoFalla, CodEstado, idEstadoFalla, idTipoAuxilio, Motivo,
	FechaInicio, HoraInicio, Ubicacion, idEstadoLiquidacion, UsuarioCreacion, FechaCreacion, UltimoUsuario, UltimaModificacion, EstadoAuxilio)
	SELECT @correlativo, @NroTicket, SUBSTRING(Descripcion,1,2) + '-' + CONVERT(varchar(20),RIGHT('000'+LTRIM(RTRIM(@correlativo)),3)), @TipoFalla,
	'FM', 1, @idTipoAuxilio, @Motivo, @FechaInicio, @HoraInicio, @Ubicacion, 1, @Usuario, GETDATE(), @Usuario, GETDATE(), 'EN COORDINACIÓN'
	FROM ReportesApp_Mantenimiento_FallasMecanicas_TipoAuxilio WHERE idTipoAuxilio = @idTipoAuxilio

	SET @Exito = '0 = Falla Mecánica Añadida. N°: ' + CONVERT(VARCHAR,@correlativo)
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

-------------------------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 12-07-2024
-- Description:	INSERTAR AUXILIO MECÁNICO - TOLVAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_InsertarTolvas]
@Tracto VARCHAR(20),
@Carreta VARCHAR(20),
@FechaViaje DATETIME,
@PersonaC INT,
@idRuta INT,
@idTipoAuxilio INT,
@TipoFalla VARCHAR(50),
@Motivo VARCHAR(250),
@FechaInicio DATE,
@HoraInicio TIME(3),
@Ubicacion VARCHAR(500),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

SET @Exito = '0 = Falla Mecánica Añadida. N°: '
SET @correlativo = (SELECT MAX(idFalla) FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros)
SET @correlativo = ISNULL(@correlativo,0) + 1 

IF (@FechaInicio > GETDATE())
BEGIN
	SET @Exito = '-1 = Solo puede elegir hasta el día de hoy.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	DECLARE @idTracto INT = (SELECT IdVehiculo FROM OP_TR_VEHICULO WHERE NumeroPlaca = @Tracto)
	DECLARE @idCarreta INT = (SELECT IdVehiculo FROM OP_TR_VEHICULO WHERE NumeroPlaca = @Carreta)
	
	DECLARE @correlativo2 INT = (SELECT MAX(idViajeT) FROM ReportesApp_Mantenimiento_FallasMecanicas_ViajeTolvas WHERE Anio = YEAR(GETDATE()))
	SET @correlativo2 = ISNULL(@correlativo2,0) + 1 

	DECLARE @CodViajeT INT = CONVERT(INT,SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo2)),6)))

	INSERT INTO ReportesApp_Mantenimiento_FallasMecanicas_ViajeTolvas (idViajeT, Anio, CodViajeT, idTracto, Tracto, idCarreta, Carreta, FechaViaje, PersonaC, idRuta)
	VALUES (@correlativo2, YEAR(GETDATE()), @CodViajeT, @idTracto, @Tracto, @idCarreta, @Carreta, @FechaViaje, @PersonaC, @idRuta)

	INSERT INTO ReportesApp_Mantenimiento_FallasMecanicas_Registros(idFalla, NroTicket, CodFalla, TipoFalla, CodEstado, idEstadoFalla, idTipoAuxilio, Motivo, FechaInicio, HoraInicio,
	Ubicacion, idEstadoLiquidacion, UsuarioCreacion, FechaCreacion, UltimoUsuario, UltimaModificacion, EstadoAuxilio)
	SELECT @correlativo, @CodViajeT, SUBSTRING(Descripcion,1,2) + '-' + CONVERT(varchar(20),RIGHT('000'+LTRIM(RTRIM(@correlativo)),3)), @TipoFalla, 'FM', 1, @idTipoAuxilio,
	@Motivo, @FechaInicio, @HoraInicio, @Ubicacion, 1, @Usuario, GETDATE(), @Usuario, GETDATE(), 'EN COORDINACIÓN'
	FROM ReportesApp_Mantenimiento_FallasMecanicas_TipoAuxilio WHERE idTipoAuxilio = @idTipoAuxilio

	SET @Exito = '0 = Falla Mecánica Añadida. N°: ' + CONVERT(VARCHAR,@correlativo)
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

--------------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 17-05-2023
-- Description:	LISTAR REGISTRAR FALTANTES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_ListarFallas]
@NumeroPlaca VARCHAR(20),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@idEstadoFalla INT
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	SET NOCOUNT ON;
	UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
	SET MontoComprobante = NULL
	WHERE MontoComprobante = 0.00
	
	UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
	SET Monto = NULL
	WHERE Monto = 0.00
	
	UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
	SET Galones = NULL
	WHERE Galones = 0.00
	
	UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
	SET PrecioUnitario = NULL
	WHERE PrecioUnitario = 0.00
	
	UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
	SET PrecioTotal = NULL
	WHERE PrecioTotal = 0.00
	
	UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
	SET idTipoRecibo = 4
	WHERE idTipoRecibo = 0
	
	IF(@idEstadoFalla = 0) BEGIN
	SET @idEstadoFalla = NULL
	END
	
	SELECT FM.idFalla AS 'ID', FM.CodFalla, FM.CodEstado, FM.CodEstado + '-' + CONVERT(VARCHAR,FM.idFalla) AS 'CODIGO', ISNULL(IC.idIncidenteC,0) AS 'idIncidenteC',
	EF.Descripcion AS 'ESTADO', FM.EstadoAuxilio AS 'ESTADO_AUXILIO', DATEDIFF(HOUR,CONVERT(DATETIME,CONVERT(VARCHAR,FM.FechaInicio,103) + ' ' + CONVERT(VARCHAR(8),FM.HoraInicio)),
	CONVERT(DATETIME,CONVERT(VARCHAR,ISNULL(FM.FechaTermino,CONVERT(DATE,GETDATE())),103) + ' ' + CONVERT(VARCHAR(8),ISNULL(FM.HoraTermino,CONVERT(TIME,GETDATE()))))) AS 'HORAS_DEMORA',
	FM.NroTicket AS 'NRO_PREVIAJE', OP.Descripcion AS 'PROGRAMACION', V.NumeroPlaca AS 'PLACA', R.NumeroPlaca AS 'SEMIRREMOLQUE', LTRIM(RTRIM(ISNULL(C.Nombre,P.NombreCompleto))) AS 'CONDUCTOR',
	RT.Descripcion AS 'RUTA', FORMAT(ISNULL(RE.FechaProgramacion,TL.FechaViaje),'dd/MM/yyyy') AS 'FECHA_VIAJE', FM.TipoFalla AS 'TIPO', A.Descripcion AS 'TIPO_FALLA', FM.Motivo AS 'MOTIVO',
	FM.Ubicacion AS 'UBICACIÓN', CS.Descripcion AS 'CLASE_SERVICIO', CONVERT(VARCHAR,FM.FechaInicio,103) + ' ' + CONVERT(VARCHAR(8),FM.HoraInicio) AS 'FECHA_INICIO',
	CONVERT(VARCHAR, FM.FechaSalida,103) + ' ' + CONVERT(VARCHAR(8), FM.HoraSalida) AS 'FECHA_SALIDA', CONVERT(VARCHAR, FM.FechaLlegada,103) + ' ' + CONVERT(VARCHAR(8),FM.HoraLlegada)
	AS 'FECHA_LLEGADA', CONVERT(VARCHAR,FM.FechaTermino,103) + ' ' + CONVERT(VARCHAR(8),FM.HoraTermino) AS 'FECHA_TÉRMINO', FM.Duracion AS 'DURACIÓN (DÍAS)', 
	IC.MontoTotal AS 'PAGO_INCIDENCIA', FM.Tecnico AS 'CONDUCTOR_CAMIONETA', FM.idPlaca AS 'PLACA_CAMIONETA',
	FM.Monto AS 'MONTO', FM.Galones AS 'GALONES', FM.PrecioUnitario AS 'PRECIO_UNITARIO', FM.PrecioTotal AS 'PRECIO_TOTAL', FM.NombreTercero AS 'NOMBRE_TECNICO',
	FM.TelefonoTercero AS 'TELÉFONO/CELULAR', TR.Descripcion AS 'RECIBO', FM.Comprobante AS 'N° RECIBO', FM.MontoComprobante AS 'MONTO_COMPROBANTE', SV.Descripcion AS 'SISTEMA_VEHÍCULO',
	SSV.Descripcion AS 'SUB_SISTEMA', FM.Solucion AS 'SOLUCIÓN', LI.Descripcion AS 'ESTADO_LIQUIDACIÓN', FORMAT(FM.FechaLiquidacion,'dd/MM/yyyy') AS 'FECHA_LIQUIDACIÓN',
	FM.UsuarioCreacion, FORMAT(FM.FechaCreacion,'dd/MM/yyyy') AS FechaCreacion, FM.UltimoUsuario, FORMAT(FM.UltimaModificacion,'dd/MM/yyyy') AS UltimaModificacion
	FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros FM
	LEFT JOIN ReportesApp_Operacion_Previaje_Registros RE WITH(NOLOCK) ON FM.NroTicket = RE.NroTicket
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_ViajeTolvas TL ON TL.CodViajeT = FM.NroTicket
	LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = ISNULL(RE.TipoProgramacion,1)
    LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = ISNULL(RE.idTracto,TL.idTracto)
	LEFT JOIN OP_TR_Vehiculo R WITH(NOLOCK) ON R.IdVehiculo = ISNULL(RE.idSemirremolque,TL.idCarreta)
	LEFT JOIN OP_TR_Conductor C WITH(NOLOCK) ON C.IdConductor = RE.IdConductor
	LEFT JOIN PersonaMast P WITH(NOLOCK) ON P.Persona = TL.PersonaC
	LEFT JOIN OP_TR_RUTA RT WITH(NOLOCK) ON RT.IDRUTA = ISNULL(RE.IdRuta, TL.idRuta)
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_TipoAuxilio A WITH(NOLOCK) ON A.idTipoAuxilio = FM.idTipoAuxilio
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_EstadoFalla EF WITH(NOLOCK) ON EF.idEstadoFalla = FM.idEstadoFalla
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_ClaseServicio CS WITH(NOLOCK) ON CS.idClaseServicio = FM.idClaseServicio
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_TipoRecibo TR WITH(NOLOCK) ON TR.idTipoRecibo = FM.idTipoRecibo
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_Liquidacion LI WITH(NOLOCK) ON LI.idEstadoLiquidacion = FM.idEstadoLiquidacion
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_SistemaVehiculo SV WITH(NOLOCK) ON SV.idSistemaVehiculo = FM.idSistemaVehiculo
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_SubSistemaVehiculo SSV WITH(NOLOCK) ON SSV.idSubSistema = FM.idSubSistema
    LEFT JOIN ReportesApp_Mantenimiento_RegistroIncidencias_Cabecera IC WITH(NOLOCK) ON IC.idFalla = FM.idFalla
	WHERE ((@NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @NumeroPlaca + '%') AND (FM.FechaInicio BETWEEN @FINICIO AND @FFIN)
			AND (@idEstadoFalla IS NULL OR FM.idEstadoFalla = @idEstadoFalla) AND (FM.idTipoAuxilio != 4)) ORDER BY FM.idFalla DESC
END

---------------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 02-06-2023
-- Description:	CERRAR FALLA MECÁNICA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_CerrarFallaMecanica]
@idFalla INT,
@valorSalida INT,
@EstadoAuxilio VARCHAR(50),
@Fecha DATE,
@Hora TIME(3),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Falla Mecánica Actualizada.'

IF (@idFalla = 0)
BEGIN
	SET @Exito = '-1 = La falla seleccionada no existe.'
	GOTO Terminar
END

IF((SELECT EstadoAuxilio FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros WHERE idFalla = @idFalla) = 'ATENDIDO')
BEGIN
	SET @Exito = '-3 = La falla seleccionada ya está cerrada.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	IF (@valorSalida = 1) BEGIN
		UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
		SET idClaseServicio = 1
		WHERE idFalla = @idFalla
	END
	ELSE BEGIN
		UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
		SET idClaseServicio = 2
		WHERE idFalla = @idFalla
	END

	IF (@EstadoAuxilio = 'EN RUTA') BEGIN
		UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
		SET FechaSalida = @Fecha, HoraSalida = @Hora, UltimoUsuario = @Usuario, EstadoAuxilio = @EstadoAuxilio, UltimaModificacion = GETDATE()
		WHERE idFalla = @idFalla
	END

	IF (@EstadoAuxilio = 'EN ATENCIÓN') BEGIN
		UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
		SET FechaLlegada = @Fecha, HoraLlegada = @Hora, UltimoUsuario = @Usuario, EstadoAuxilio = @EstadoAuxilio, UltimaModificacion = GETDATE()
		WHERE idFalla = @idFalla
	END

	IF (@EstadoAuxilio = 'ATENDIDO') BEGIN
		UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
		SET FechaTermino = @Fecha, HoraTermino = @Hora, Duracion = DATEDIFF(day, FechaInicio, @Fecha), EstadoAuxilio = @EstadoAuxilio,
		UltimoUsuario = @Usuario, UltimaModificacion = GETDATE()
		WHERE idFalla = @idFalla
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

-------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 14-02-2024
-- Description:	LISTAR REGISTRO INCIDENCIAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_RegistroIncidencias_ListarIncidencias]
@Placa VARCHAR(20),
@idOperacion INT,
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@idOperacion = 5) BEGIN
		SELECT IC.idIncidenteC AS 'NRO', IC.idFalla, OP.Descripcion AS 'OPERACION', V.NumeroPlaca AS 'TRACTO', R.NumeroPlaca AS 'SEMIRREMOLQUE', 
		CONVERT(VARCHAR,FM.FechaInicio,103)+' '+CONVERT(VARCHAR,FM.HoraInicio,8) AS 'FECHA_INICIO', LTRIM(RTRIM(ISNULL(C.Nombre,P.NombreCompleto))) AS 'CONDUCTOR',
		FM.Ubicacion AS 'UBICACIÓN', FM.Motivo AS 'MOTIVO', IC.MontoTotal AS 'PRECIO_TOTAL', IC.UsuarioCreacion, IC.FechaCreacion, IC.UsuarioModificacion,
		IC.FechaModificacion, IC.Descripcion, IC.TipoDanio, IC.DescripcionDanio, IC.Observacion, P.Telefono, RT.Descripcion AS 'RUTA', IC.Imagen, IC.Falla
		FROM ReportesApp_Mantenimiento_RegistroIncidencias_Cabecera IC
		LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_Registros FM ON FM.idFalla = IC.idFalla
		LEFT JOIN ReportesApp_Operacion_Previaje_Registros RE WITH(NOLOCK) ON FM.NroTicket = RE.NroTicket
		LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_ViajeTolvas TL ON TL.CodViajeT = FM.NroTicket
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = ISNULL(RE.idTracto,TL.idTracto)
		LEFT JOIN OP_TR_Vehiculo R WITH(NOLOCK) ON R.IdVehiculo = ISNULL(RE.idSemirremolque,TL.idCarreta)
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = ISNULL(RE.TipoProgramacion,1)
		LEFT JOIN OP_TR_Conductor C WITH(NOLOCK) ON C.IdConductor = RE.IdConductor
		LEFT JOIN PersonaMast P WITH(NOLOCK) ON P.Persona = ISNULL(C.IdPersona,TL.PersonaC)
		LEFT JOIN OP_TR_Ruta RT WITH(NOLOCK) ON RT.IdRuta = ISNULL(RE.IdRuta, TL.idRuta)
		WHERE (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND (FM.FechaInicio BETWEEN @FINICIO AND @FFIN)
		ORDER BY IC.idIncidenteC DESC
	END
	ELSE BEGIN
		SELECT IC.idIncidenteC AS 'NRO', IC.idFalla, OP.Descripcion AS 'OPERACION', V.NumeroPlaca AS 'TRACTO', R.NumeroPlaca AS 'SEMIRREMOLQUE', 
		CONVERT(VARCHAR,FM.FechaInicio,103)+' '+CONVERT(VARCHAR,FM.HoraInicio,8) AS 'FECHA_INICIO', LTRIM(RTRIM(ISNULL(C.Nombre,P.NombreCompleto))) AS 'CONDUCTOR',
		FM.Ubicacion AS 'UBICACIÓN', FM.Motivo AS 'MOTIVO', IC.MontoTotal AS 'PRECIO_TOTAL', IC.UsuarioCreacion, IC.FechaCreacion, IC.UsuarioModificacion,
		IC.FechaModificacion, IC.Descripcion, IC.TipoDanio, IC.DescripcionDanio, IC.Observacion, P.Telefono, RT.Descripcion AS 'RUTA', IC.Imagen, IC.Falla
		FROM ReportesApp_Mantenimiento_RegistroIncidencias_Cabecera IC
		LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_Registros FM ON FM.idFalla = IC.idFalla
		LEFT JOIN ReportesApp_Operacion_Previaje_Registros RE WITH(NOLOCK) ON FM.NroTicket = RE.NroTicket
		LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_ViajeTolvas TL ON TL.CodViajeT = FM.NroTicket
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = ISNULL(RE.idTracto,TL.idTracto)
		LEFT JOIN OP_TR_Vehiculo R WITH(NOLOCK) ON R.IdVehiculo = ISNULL(RE.idSemirremolque,TL.idCarreta)
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = ISNULL(RE.TipoProgramacion,1)
		LEFT JOIN OP_TR_Conductor C WITH(NOLOCK) ON C.IdConductor = RE.IdConductor
		LEFT JOIN PersonaMast P WITH(NOLOCK) ON P.Persona = ISNULL(C.IdPersona,TL.PersonaC)
		LEFT JOIN OP_TR_Ruta RT WITH(NOLOCK) ON RT.IdRuta = ISNULL(RE.IdRuta, TL.idRuta)
		WHERE (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND (FM.FechaInicio BETWEEN @FINICIO AND @FFIN) AND (RE.TipoProgramacion = @idOperacion)
		ORDER BY IC.idIncidenteC DESC
	END
END

----------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 30-05-2023
-- Description:	ELIMINAR FALLAS MECÁNICAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_EliminarFalla]
@idFalla INT
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Falla Mecánica Eliminada.'

IF (@idFalla = 0)
BEGIN
	SET @Exito = '-2 = La falla seleccionada no existe.'
	GOTO Terminar
END

IF((SELECT idEstadoFalla FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros WHERE idFalla = @idFalla) = 2)
BEGIN
	SET @Exito = '-3 = La falla seleccionada ya se encuentra solucionada.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	DELETE FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros
	WHERE idFalla = @idFalla

	DECLARE @idIncidenteC INT = (SELECT idIncidenteC FROM ReportesApp_Mantenimiento_RegistroIncidencias_Cabecera WHERE idFalla = @idFalla)

	DELETE FROM ReportesApp_Mantenimiento_RegistroIncidencias_Cabecera
	WHERE idFalla = @idFalla

	DELETE FROM ReportesApp_Mantenimiento_RegistroIncidencias_Detalle
	WHERE idIncidenteC = @idIncidenteC
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

-----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 14-02-2024
-- Description:	FILTRAR REGISTRO INCIDENCIAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_RegistroIncidencias_FiltrarEliminarIncidencias]
@Opcion INT,
@idIncidenteC INT
AS
BEGIN
	/*
	IF (@Opcion = 1) BEGIN		-- FILTRAR INCIDENCIAS
		SELECT IC.idIncidenteC AS 'NRO', V.NumeroPlaca AS 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD',
		CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION', IC.FechaIncidente AS 'FECHA_INCIDENTE',
		P1.NombreCompleto AS 'CONDUCTOR', P2.NombreCompleto AS 'SUPERVISOR', CASE WHEN IC.RutaLocal = '' THEN 'NO ENVIADO' ELSE IC.RutaLocal END AS 'INFORME_SEGURIDAD',
		(CASE WHEN IC.RutaLocal = '' THEN 'NO ENVIADO' ELSE 'ENVIADO' END) AS 'ENVIADO', IC.SubTotal, IC.IGV, IC.MontoTotal
		FROM ReportesApp_Mantenimiento_RegistroIncidencias_Cabecera IC
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = IC.idUnidad
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON UC.IdProgramacion = O.IdOperacion
		LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
		LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TipoVehiculo
		LEFT JOIN PersonaMast P1 ON P1.Persona = IC.idConductor
		LEFT JOIN PersonaMast P2 ON P2.Persona = IC.idSupervisor
		WHERE IC.idIncidenteC = @idIncidenteC
	END
	*/

	IF (@Opcion = 2) BEGIN		-- ELIMINAR INCIDENCIAS
		DECLARE @idFalla INT = (SELECT idFalla FROM ReportesApp_Mantenimiento_RegistroIncidencias_Cabecera WHERE idIncidenteC = @idIncidenteC)
		
		IF ((SELECT Duracion FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros WHERE idFalla = @idFalla) IS NOT NULL) BEGIN
			SELECT '0 = Esta incidencia está siendo tratada en el área de Mtto. No puede ser eliminada' AS Mensaje
		END
		ELSE BEGIN
			DELETE FROM ReportesApp_Mantenimiento_RegistroIncidencias_Detalle WHERE idIncidenteC = @idIncidenteC

			DELETE FROM ReportesApp_Mantenimiento_RegistroIncidencias_Cabecera WHERE idIncidenteC = @idIncidenteC

			DELETE FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros WHERE idFalla = @idFalla

			SELECT '1 = Eliminado' AS Mensaje
		END
	END

	IF (@Opcion = 3) BEGIN		-- LISTAR PRESUPUESTOS
		SELECT ID.idIncidenteD AS '#', ID.idIncidenteC, ID.TipoMaterial AS 'TIPO', ID.Material AS 'DESCRIPCION', ID.Unidad AS 'UND', ID.PrecioUnitario AS 'PRECIO_UND',
		ID.Cantidad AS 'CANT', ID.ImporteTotal AS 'IMPORTE_TOTAL'
		FROM ReportesApp_Mantenimiento_RegistroIncidencias_Detalle ID
		WHERE ID.idIncidenteC = @idIncidenteC
		ORDER BY ID.idIncidenteD ASC
	END

	IF (@Opcion = 4) BEGIN		-- LISTAR UNIDADES
		SELECT UnidadCodigo AS 'CODIGO', UnidadCodigo AS 'UNIDAD' FROM UnidadesMast WHERE Estado = 'A'
	END
END