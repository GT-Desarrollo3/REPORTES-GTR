
-- CREAR TABLA ReportesApp_Mantenimiento_FallasMecanicas_TipoAuxilio (idTipoAuxilio INT, Descripcion VARCHAR(50)) Y LLENARLA

-- CREAR TABLA ReportesApp_Mantenimiento_FallasMecanicas_ClaseServicio (idClaseServicio INT, Descripcion VARCHAR(50)) Y LLENARLA

-- CREAR TABLA ReportesApp_Mantenimiento_FallasMecanicas_EstadoFalla (idEstadoFalla INT, Descripcion VARCHAR(50)) Y LLENARLA

-- CREAR TABLA ReportesApp_Mantenimiento_FallasMecanicas_Registros (idFalla INT, CodFalla VARCHAR(30), NroTicket INT, idEstadoFalla INT, idTipoAuxilio INT, Motivo VARCHAR(250), FechaInicio DATE, HoraInicio TIME(3), Ubicacion VARCHAR(500), UsuarioCreacion VARCHAR(50), FechaCreacion DATETIME, UltimoUsuario VARCHAR(50), UltimaModificacion DATETIME  --  idClaseServicio INT, Comprobante VARCHAR(30), MontoComprobante DECIMAL(8,2), Tecnico VARCHAR(250), idPlaca VARCHAR(20), Monto DECIMAL(8,2), Solucion VARCHAR(250), FechaTermino DATE, HoraTermino TIME(3), Duracion INT)

-- CREAR TABLA ReportesApp_Mantenimiento_FallasMecanicas_ImportarWord

-- CREAR TABLA ReportesApp_Mantenimiento_FallasMecanicas_TipoRecibo (idTipoRecibo INT, Descripcion VARCHAR(50)) Y LLENARLA

-- CREAR TABLA ReportesApp_Mantenimiento_FallasMecanicas_Liquidacion (idEstadoLiquidacion INT, Descripcion VARCHAR(50)) Y LLENARLA

-- CREAR TABLA ReportesApp_Mantenimiento_FallasMecanicas_Gasto (idGasto INT, GastoTotal DECIMAL(8,2), idFalla INT)

-- CREAR TABLA ReportesApp_Mantenimiento_FallasMecanicas_GastoDetalle (idGastoDetalle INT, idGasto INT, Descripcion VARCHAR(100), Monto DECIMAL(8,2), fechaGasto DATETIME)

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 27-05-2023
-- Description:	INSERTAR REGISTRO DE FALLAS MECÁNICAS
-- =============================================
/*
EXEC ReportesApp_Mantenimiento_FallasMecanicas_Insertar @NroTicket = 2416924, @idTipoAuxilio = 1, @TipoFalla = 'PARADA', @Motivo = 'DFSDDSG',
@FechaInicio = '14/10/2024', @HoraInicio = '15:29:55', @Ubicacion = DFSD, @BloqueoAuto = 1, @Usuario = 'GREYES'
*/
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
	DECLARE @idTracto INT = (SELECT idTracto FROM ReportesApp_Operacion_Previaje_Registros WHERE NroTicket = @NroTicket)
	DECLARE @idCarreta INT = (SELECT idSemirremolque FROM ReportesApp_Operacion_Previaje_Registros WHERE NroTicket = @NroTicket)
	DECLARE @idConductor INT = (SELECT IdConductor FROM ReportesApp_Operacion_Previaje_Registros WHERE NroTicket = @NroTicket)
	DECLARE @idPersona INT = (SELECT Persona FROM PersonaMast WHERE Documento = (SELECT Documento FROM OP_TR_Conductor WHERE IdConductor = @idConductor))

	INSERT INTO ReportesApp_Mantenimiento_FallasMecanicas_Registros(idFalla, NroTicket, CodFalla, TipoFalla, CodEstado, idEstadoFalla, idTipoAuxilio, Motivo,
	FechaInicio, HoraInicio, Ubicacion, idEstadoLiquidacion, UsuarioCreacion, FechaCreacion, UltimoUsuario, UltimaModificacion, EstadoAuxilio, idTracto, idCarreta, UnidadAfectada)
	SELECT @correlativo, @NroTicket, SUBSTRING(Descripcion,1,2) + '-' + CONVERT(varchar(20),RIGHT('000'+LTRIM(RTRIM(@correlativo)),3)), @TipoFalla,
	'FM', 1, @idTipoAuxilio, @Motivo, @FechaInicio, @HoraInicio, @Ubicacion, 1, @Usuario, GETDATE(), @Usuario, GETDATE(), 'EN COORDINACIÓN', @idTracto, @idCarreta, 'TRACTO'
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
IF LEFT(@Exito,1)='-' OR cast(left(@Exito,1) AS INT)<0 BEGIN
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
-- Create date: 27-05-2023
-- Description:	LISTAR REGISTRO DE PREVIAJES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_ListarRegistro]
@NroTicket INT
AS
BEGIN
	SET NOCOUNT ON;
    SELECT OP.IdOperacion, OP.Descripcion AS 'PROGRAMACION', F.FechaProgramacion AS 'FECHA_VIAJE', F.idTracto, V.NumeroPlaca AS 'TRACTO', 
	F.IdConductor, LTRIM(RTRIM(C.Nombre)) AS 'CONDUCTOR', F.idSemirremolque, R.NumeroPlaca AS 'SEMIRREMOLQUE', F.IdRuta, RT.Descripcion AS 'RUTA',
	LTRIM(RTRIM(CL.Busqueda)) as 'CLIENTE', FM.idTipoAuxilio, FM.Motivo, FM.Ubicacion
    FROM ReportesApp_Operacion_Previaje_Registros F
    LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_Registros FM WITH(NOLOCK) ON F.NroTicket = FM.NroTicket
    LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = F.idTracto
    LEFT JOIN OP_TR_Viaje VJ WITH(NOLOCK) ON VJ.Codigo = CAST(F.CodViaje AS VARCHAR(12))
    LEFT JOIN OP_TR_Conductor C WITH(NOLOCK) ON C.IdConductor = F.IdConductor
    LEFT JOIN OP_TR_Vehiculo R WITH(NOLOCK) ON R.IdVehiculo = F.idSemirremolque
    LEFT JOIN OP_TR_RUTA RT WITH(NOLOCK) ON RT.IDRUTA = F.IdRuta
    LEFT JOIN PersonaMast CL WITH(NOLOCK) ON CL.Persona = F.IdCliente
    LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = F.TipoProgramacion
    WHERE F.NroTicket = @NroTicket
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 27-05-2023
-- Description:	EDITAR REGISTRO DE FALLAS MECÁNICAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_ModificarFalla]
@idFalla INT,
@idTipoAuxilio INT,
@TipoFalla VARCHAR(50),
@Motivo VARCHAR(250),
@Ubicacion VARCHAR(500),
@UnidadAfectada VARCHAR(150),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Falla Mecánica Modificada.'

IF (@idFalla = 0)
BEGIN
	SET @Exito = '-1 = La falla seleccionada no existe.'
	GOTO Terminar
END

IF((SELECT idEstadoFalla FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros WHERE idFalla = @idFalla) = 2)
BEGIN
	SET @Exito = '-2 = La falla seleccionada ya se encuentra solucionada.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	IF (@UnidadAfectada = 'TRACTO') BEGIN
		UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
		SET idTipoAuxilio = @idTipoAuxilio,
		TipoFalla = @TipoFalla,
		Motivo = @Motivo,
		Ubicacion = @Ubicacion,
		UnidadAfectada = 'TRACTO',
		UltimoUsuario = @Usuario,
		UltimaModificacion = GETDATE()
		WHERE idFalla = @idFalla
	END
	ELSE BEGIN
		DECLARE @idCarreta INT = (SELECT idCarreta FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros WHERE idFalla = @idFalla)
		DECLARE @TipoSR VARCHAR(50) = (SELECT STV.Descripcion FROM ReportesApp_Operaciones_ConductorUnidades_SubTipoVehiculo STV
									   LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON STV.idSubTipoVehiculo = V.SubTipoVehiculo
									   WHERE V.IdVehiculo = @idCarreta)

		UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
		SET idTipoAuxilio = @idTipoAuxilio,
		TipoFalla = @TipoFalla,
		Motivo = @Motivo,
		Ubicacion = @Ubicacion,
		UnidadAfectada = @TipoSR,
		UltimoUsuario = @Usuario,
		UltimaModificacion = GETDATE()
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

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 17-05-2023
-- Description:	LISTAR REGISTRAR FALTANTES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_ListarFallaEditar]
@idFalla INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT FM.idFalla AS 'ID', FM.CodFalla AS 'CÓDIGO', EF.Descripcion AS 'ESTADO', FM.NroTicket AS 'NRO_TICKET', OP.Descripcion AS 'PROGRAMACION', V.NumeroPlaca AS 'PLACA',
	R.NumeroPlaca AS 'SEMIRREMOLQUE', RE.FechaProgramacion AS 'FECHA_VIAJE', LTRIM(RTRIM(C.Nombre)) AS 'CONDUCTOR', LTRIM(RTRIM(CL.Busqueda)) as 'CLIENTE',
	RT.Descripcion AS 'RUTA', FM.idTipoAuxilio, A.Descripcion AS 'TIPO_FALLA', FM.Motivo AS 'MOTIVO', FM.Ubicacion AS 'UBICACIÓN', FM.FechaInicio AS 'FECHA_INICIO',
	FM.HoraInicio AS 'HORA_INICIO', FM.Tecnico AS 'CONDUCTOR_CAMIONETA', FM.idPlaca AS 'PLACA_CAMIONETA', FM.Monto AS 'MONTO', FM.Galones AS 'GALONES',
	FM.PrecioUnitario AS 'PRECIO_UNITARIO', FM.PrecioTotal AS 'PRECIO_TOTAL', FM.NombreTercero AS 'NOMBRE_TECNICO', FM.TelefonoTercero AS 'TELEFONO_TERCER0',
	FM.Comprobante AS 'COMPROBANTE', FM.MontoComprobante AS 'MONTO_COMPROBANTE', FM.Solucion AS 'SOLUCIÓN', FM.FechaTermino AS 'FECHA_TÉRMINO',
	FM.HoraTermino AS 'HORA_TÉRMINO', FM.Duracion AS 'DURACIÓN'
	FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros FM
	LEFT JOIN ReportesApp_Operacion_Previaje_Registros RE WITH(NOLOCK) ON FM.NroTicket = RE.NroTicket
	LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = RE.TipoProgramacion
    LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = RE.idTracto
	LEFT JOIN OP_TR_Vehiculo R WITH(NOLOCK) ON R.IdVehiculo = RE.idSemirremolque
	LEFT JOIN OP_TR_Conductor C WITH(NOLOCK) ON C.IdConductor = RE.IdConductor
	LEFT JOIN PersonaMast CL WITH(NOLOCK) ON CL.Persona = RE.IdCliente
	LEFT JOIN OP_TR_RUTA RT WITH(NOLOCK) ON RT.IDRUTA = RE.IdRuta
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_TipoAuxilio A WITH(NOLOCK) ON A.idTipoAuxilio = FM.idTipoAuxilio
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_EstadoFalla EF WITH(NOLOCK) ON EF.idEstadoFalla = FM.idEstadoFalla
    WHERE FM.idFalla = @idFalla
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 30-05-2023
-- Description:	ELIMINAR FALLAS MECÁNICAS
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_EliminarFalla]
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
-- Create date: 02-06-2023
-- Description:	CERRAR FALLA MECÁNICA
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_CerrarFallaMecanica]
@idFalla INT,
@FechaTermino DATE,
@HoraTermino TIME(3),
@FechaSalida DATE,
@HoraSalida TIME(3),
@FechaLlegada DATE,
@HoraLlegada TIME(3),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Falla Mecánica Cerrada.'

IF (@idFalla = 0)
BEGIN
	SET @Exito = '-1 = La falla seleccionada no existe.'
	GOTO Terminar
END

IF((SELECT Duracion FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros WHERE idFalla = @idFalla) IS NOT NULL)
BEGIN
	SET @Exito = '-3 = La falla seleccionada ya está cerrada.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
	SET FechaTermino = @FechaTermino,
	HoraTermino = @HoraTermino,
	FechaSalida = @FechaSalida,
	HoraSalida = @HoraSalida,
	FechaLlegada = @FechaLlegada,
	HoraLlegada = @HoraLlegada,
	Duracion = DATEDIFF(day, FechaInicio, @FechaTermino),
	UltimoUsuario = @Usuario,
	UltimaModificacion = GETDATE()
	WHERE idFalla = @idFalla
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
-- Create date: 02-06-2023
-- Description:	CERRAR FALLA MECÁNICA
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_CerrarFallaMecanicaTerceros]
@idFalla INT,
@FechaTermino DATE,
@HoraTermino TIME(3),
@FechaLlegada DATE,
@HoraLlegada TIME(3),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Falla Mecánica Cerrada.'

IF (@idFalla = 0)
BEGIN
	SET @Exito = '-1 = La falla seleccionada no existe.'
	GOTO Terminar
END

IF((SELECT Duracion FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros WHERE idFalla = @idFalla) IS NOT NULL)
BEGIN
	SET @Exito = '-3 = La falla seleccionada ya está cerrada.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
	SET FechaTermino = @FechaTermino,
	HoraTermino = @HoraTermino,
	FechaLlegada = @FechaLlegada,
	HoraLlegada = @HoraLlegada,
	Duracion = DATEDIFF(day, FechaInicio, @FechaTermino),
	UltimoUsuario = @Usuario,
	UltimaModificacion = GETDATE()
	WHERE idFalla = @idFalla
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
-- Create date: 30-05-2023
-- Description:	AGREGAR SOLUCIÓN A FALLAS MECÁNICAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_InsertarSolucion]
@idFalla INT,
@NombreTercero VARCHAR(250),
@TelefonoTercero VARCHAR(20),
@idTipoRecibo INT,
@Comprobante VARCHAR(30),
@MontoComprobante DECIMAL(8,2),
@Tecnico VARCHAR(250),
@idPlaca VARCHAR(20),
@Galones DECIMAL(8,2),
@PrecioUnitario DECIMAL(8,2),
@PrecioTotal DECIMAL(8,2),
@Monto DECIMAL(8,2),
@idSistemaVehiculo INT,
@idSubSistema INT,
@Solucion VARCHAR(250),
@Usuario VARCHAR(20),
@MttoCorrectivo INT
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo2 INT

SET @Exito = '0 = Solución Añadida.'

IF (@idFalla = 0)
BEGIN
	SET @Exito = '-1 = La falla seleccionada no existe.'
	GOTO Terminar
END

IF((SELECT idEstadoFalla FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros WHERE idFalla = @idFalla) = 2)
BEGIN
	SET @Exito = '-3 = La falla seleccionada ya se encuentra solucionada.'
	GOTO Terminar
END

IF((SELECT FechaTermino FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros WHERE idFalla = @idFalla) IS NULL)
BEGIN
	SET @Exito = '-4 = Primero debe cerrar la falla mecánica.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	IF (@Tecnico = '') BEGIN
		UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
		SET idClaseServicio = 2
		WHERE idFalla = @idFalla
	END
	ELSE BEGIN
		IF (@Comprobante = '') BEGIN
		UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
		SET idClaseServicio = 1
		WHERE idFalla = @idFalla
		END
	END
	
	UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
	SET idEstadoFalla = 2,
	NombreTercero = @NombreTercero,
	TelefonoTercero = @TelefonoTercero,
	idTipoRecibo = @idTipoRecibo,
	Comprobante = @Comprobante,
	MontoComprobante = @MontoComprobante,
	Tecnico = @Tecnico,
	idPlaca = @idPlaca,
	Galones = @Galones,
	PrecioUnitario = @PrecioUnitario,
	PrecioTotal = @PrecioTotal,
	Monto = @Monto,
	idSistemaVehiculo = @idSistemaVehiculo,
	idSubSistema = @idSubSistema,
	Solucion = @Solucion,
	UltimoUsuario = @Usuario,
	UltimaModificacion = GETDATE()
	WHERE idFalla = @idFalla

	IF (@Monto = 0.00 AND @PrecioTotal = 0.00 AND (SELECT idClaseServicio FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros WHERE idFalla = @idFalla) = 1) BEGIN
		UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
		SET idEstadoLiquidacion = 2, FechaLiquidacion = GETDATE()
		WHERE idFalla = @idFalla
	END
	
	INSERT INTO ReportesApp_Mantenimiento_FallasMecanicas_ImportarWord (idFalla,CodFalla,Tipo,Estado,NroTicket,Programacion,NumeroPlaca,Semirremolque,
				FechaViaje,Conductor,Cliente,Ruta,TipoFalla,Motivo,Ubicacion,FechaInicio,HoraInicio,ClaseServicio,Tecnico,PlacaTecnico,Galones,
				PrecioTotal,Monto,NombreTercero,TelefonoTercero,TipoRecibo,Comprobante,MontoComprobante,Solucion,FechaTermino,HoraTermino,
				Duracion,FechaLlegada,HoraLlegada,FechaSalida,HoraSalida,SistemaVehiculo,SubSistemaVehiculo,UnidadAfectada)
	SELECT FM.idFalla, FM.CodFalla, FM.TipoFalla, EF.Descripcion, FM.NroTicket, OP.Descripcion, V.NumeroPlaca, R.NumeroPlaca, RE.FechaProgramacion,
	LTRIM(RTRIM(C.Nombre)), LTRIM(RTRIM(CL.Busqueda)), RT.Descripcion, A.Descripcion, FM.Motivo, FM.Ubicacion, FM.FechaInicio, FM.HoraInicio,
	CS.Descripcion, FM.Tecnico, FM.idPlaca, FM.Galones, FM.PrecioTotal, FM.Monto, FM.NombreTercero, FM.TelefonoTercero,
	TR.Descripcion, FM.Comprobante, FM.MontoComprobante, FM.Solucion, FM.FechaTermino, FM.HoraTermino, FM.Duracion, FM.FechaLlegada, FM.HoraLlegada,
	FM.FechaSalida, FM.HoraSalida, SV.Descripcion, SSV.Descripcion, FM.UnidadAfectada FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros FM
	LEFT JOIN ReportesApp_Operacion_Previaje_Registros RE WITH(NOLOCK) ON FM.NroTicket = RE.NroTicket
	LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = RE.TipoProgramacion
    LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = ISNULL(FM.idTracto,RE.idTracto)
	LEFT JOIN OP_TR_Vehiculo R WITH(NOLOCK) ON R.IdVehiculo = ISNULL(FM.idCarreta,RE.idSemirremolque)
	LEFT JOIN OP_TR_Conductor C WITH(NOLOCK) ON C.IdConductor = RE.IdConductor
	LEFT JOIN PersonaMast CL WITH(NOLOCK) ON CL.Persona = RE.IdCliente
	LEFT JOIN OP_TR_RUTA RT WITH(NOLOCK) ON RT.IDRUTA = RE.IdRuta
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_TipoAuxilio A WITH(NOLOCK) ON A.idTipoAuxilio = FM.idTipoAuxilio
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_EstadoFalla EF WITH(NOLOCK) ON EF.idEstadoFalla = FM.idEstadoFalla
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_ClaseServicio CS WITH(NOLOCK) ON CS.idClaseServicio = FM.idClaseServicio
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_TipoRecibo TR WITH(NOLOCK) ON TR.idTipoRecibo = FM.idTipoRecibo
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_SistemaVehiculo SV WITH(NOLOCK) ON SV.idSistemaVehiculo = FM.idSistemaVehiculo
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_SubSistemaVehiculo SSV WITH(NOLOCK) ON SSV.idSubSistema = FM.idSubSistema
	WHERE(FM.idFalla = @idFalla)

	IF (@MttoCorrectivo = 1) BEGIN
		SET @correlativo2 = (SELECT MAX(idMttoC) FROM ReportesApp_Mantenimiento_MttoCorrectivo_Registro)
		SET @correlativo2 = ISNULL(@correlativo2, 0) + 1

		INSERT INTO ReportesApp_Mantenimiento_MttoCorrectivo_Registro(idMttoC, Placa, Operacion, Origen, FechaReportada, Sistema,
		Descripcion, Observacion, Estado, UsuarioCreacion, FechaCreacion, UsuarioModificacion, FechaModificacion)
		SELECT @correlativo2, V.NumeroPlaca, OP.Descripcion, 'AUXILIO', CONVERT(DATETIME,FM.FechaInicio), SV.Descripcion, SSV.Descripcion,
		FM.Motivo, 'PENDIENTE', @Usuario, GETDATE(), @Usuario, GETDATE()
		FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros FM
		LEFT JOIN ReportesApp_Operacion_Previaje_Registros RE WITH(NOLOCK) ON FM.NroTicket = RE.NroTicket
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = ISNULL(FM.idTracto,RE.idTracto)
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = RE.TipoProgramacion
		LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_SistemaVehiculo SV WITH(NOLOCK) ON SV.idSistemaVehiculo = FM.idSistemaVehiculo
		LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_SubSistemaVehiculo SSV WITH(NOLOCK) ON SSV.idSubSistema = FM.idSubSistema
		WHERE (FM.idFalla = @idFalla)
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
-- Create date: 27-05-2023
-- Description:	LISTAR ASUME DE FALTANTES
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_ListarAuxilio]
AS
BEGIN
	SELECT idTipoAuxilio, Descripcion
	FROM ReportesApp_Mantenimiento_FallasMecanicas_TipoAuxilio
END

-----------------------------------------------

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
	
	SELECT DISTINCT FM.idFalla AS 'ID', FM.CodFalla, FM.CodEstado, FM.CodEstado + '-' + CONVERT(VARCHAR,FM.idFalla) AS 'CODIGO', ISNULL(IC.idIncidenteC,0) AS 'idIncidenteC',
	EF.Descripcion AS 'ESTADO', FM.EstadoAuxilio AS 'ESTADO_AUXILIO', DATEDIFF(HOUR,CONVERT(DATETIME,CONVERT(VARCHAR,FM.FechaInicio,103) + ' ' + CONVERT(VARCHAR(8),FM.HoraInicio)),
	CONVERT(DATETIME,CONVERT(VARCHAR,ISNULL(FM.FechaTermino,CONVERT(DATE,GETDATE())),103) + ' ' + CONVERT(VARCHAR(8),ISNULL(FM.HoraTermino,CONVERT(TIME,GETDATE()))))) AS 'HORAS_DEMORA',
	FM.NroTicket AS 'NRO_PREVIAJE', OP.Descripcion AS 'PROGRAMACION', V.NumeroPlaca AS 'PLACA', R.NumeroPlaca AS 'SEMIRREMOLQUE', FM.UnidadAfectada AS 'UNIDAD_AFECTADA',
	CASE WHEN EXISTS(SELECT * FROM ReportesApp_Operaciones_ControlItems_RegistroKitNeumaticos WHERE idTracto = V.IdVehiculo) THEN 'SÍ' ELSE 'NO' END AS 'KIT_NEUMATICO',
	LTRIM(RTRIM(ISNULL(C.Nombre,P.NombreCompleto))) AS 'CONDUCTOR', RT.Descripcion AS 'RUTA', FORMAT(ISNULL(RE.FechaProgramacion,TL.FechaViaje),'dd/MM/yyyy') AS 'FECHA_VIAJE',
	FM.TipoFalla AS 'TIPO', A.Descripcion AS 'TIPO_FALLA', FM.Motivo AS 'MOTIVO', FM.Ubicacion AS 'UBICACIÓN', CS.Descripcion AS 'CLASE_SERVICIO',
	CONVERT(VARCHAR,FM.FechaInicio,103) + ' ' + CONVERT(VARCHAR(8),FM.HoraInicio) AS 'FECHA_INICIO', CONVERT(VARCHAR, FM.FechaSalida,103) + ' ' + CONVERT(VARCHAR(8), FM.HoraSalida) AS 'FECHA_SALIDA',
	CONVERT(VARCHAR, FM.FechaLlegada,103) + ' ' + CONVERT(VARCHAR(8),FM.HoraLlegada) AS 'FECHA_LLEGADA', CONVERT(VARCHAR,FM.FechaTermino,103) + ' ' + CONVERT(VARCHAR(8),FM.HoraTermino) AS 'FECHA_TÉRMINO',
	FM.Duracion AS 'DURACIÓN (DÍAS)', IC.MontoTotal AS 'PAGO_INCIDENCIA', FM.Tecnico AS 'CONDUCTOR_CAMIONETA', FM.idPlaca AS 'PLACA_CAMIONETA',
	FM.Monto AS 'MONTO', FM.Galones AS 'GALONES', FM.PrecioUnitario AS 'PRECIO_UNITARIO', FM.PrecioTotal AS 'PRECIO_TOTAL', FM.NombreTercero AS 'NOMBRE_TECNICO',
	FM.TelefonoTercero AS 'TELÉFONO/CELULAR', TR.Descripcion AS 'RECIBO', FM.Comprobante AS 'N° RECIBO', FM.MontoComprobante AS 'MONTO_COMPROBANTE', SV.Descripcion AS 'SISTEMA_VEHÍCULO',
	SSV.Descripcion AS 'SUB_SISTEMA', FM.Solucion AS 'SOLUCIÓN', LI.Descripcion AS 'ESTADO_LIQUIDACIÓN', FORMAT(FM.FechaLiquidacion,'dd/MM/yyyy') AS 'FECHA_LIQUIDACIÓN',
	FM.UsuarioCreacion, FORMAT(FM.FechaCreacion,'dd/MM/yyyy') AS FechaCreacion, FM.UltimoUsuario, FORMAT(FM.UltimaModificacion,'dd/MM/yyyy') AS UltimaModificacion
	FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros FM
	LEFT JOIN ReportesApp_Operacion_Previaje_Registros RE WITH(NOLOCK) ON FM.NroTicket = RE.NroTicket
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_ViajeTolvas TL ON TL.CodViajeT = FM.NroTicket
	LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = ISNULL(RE.TipoProgramacion,1)
    LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = ISNULL(ISNULL(FM.idTracto,RE.idTracto),TL.idTracto)
	LEFT JOIN OP_TR_Vehiculo R WITH(NOLOCK) ON R.IdVehiculo = ISNULL(ISNULL(FM.idCarreta,RE.idSemirremolque),TL.idCarreta)
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
	LEFT JOIN ReportesApp_Operaciones_ControlItems_RegistroKitNeumaticos KN WITH(NOLOCK) ON V.IdVehiculo = KN.idTracto
	WHERE ((@NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @NumeroPlaca + '%') AND (FM.FechaInicio BETWEEN @FINICIO AND @FFIN)
			AND (@idEstadoFalla IS NULL OR FM.idEstadoFalla = @idEstadoFalla)) ORDER BY FM.idFalla DESC
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 17-05-2023
-- Description:	LISTAR REGISTRAR FALTANTES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_ListarSolucion]
@idFalla INT
AS
BEGIN
	IF((SELECT Monto FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros WHERE idFalla = @idFalla) IS NULL) BEGIN
		UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
		SET Monto = 0.00
		WHERE idFalla = @idFalla
	END
	
	IF((SELECT Galones FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros WHERE idFalla = @idFalla) IS NULL) BEGIN
		UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
		SET Galones = 0.00
		WHERE idFalla = @idFalla
	END
	
	IF((SELECT PrecioUnitario FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros WHERE idFalla = @idFalla) IS NULL) BEGIN
		UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
		SET PrecioUnitario = 0.00
		WHERE idFalla = @idFalla
	END
	
	IF((SELECT PrecioTotal FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros WHERE idFalla = @idFalla) IS NULL) BEGIN
		UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
		SET PrecioTotal = 0.00
		WHERE idFalla = @idFalla
	END

	SELECT CS.Descripcion AS 'CLASE_SERVICIO', FM.Tecnico AS 'CONDUCTOR_CAMIONETA', FM.idPlaca AS 'PLACA_CAMIONETA', FM.Monto AS 'MONTO', FM.Galones AS 'GALONES',
	FM.PrecioUnitario AS 'PRECIO_UNITARIO', FM.PrecioTotal AS 'PRECIO_TOTAL', FM.NombreTercero AS 'NOMBRE', FM.TelefonoTercero AS 'NUMERO', FM.idTipoRecibo AS 'RECIBO',
	FM.Comprobante AS 'COMPROBANTE', FM.MontoComprobante AS 'MONTO_COMPROBANTE', FM.idSistemaVehiculo AS 'SISTEMA_VEHÍCULO', ISNULL(FM.idSubSistema,1) AS 'SUB_SISTEMA',
	FM.Solucion AS 'SOLUCIÓN', FORMAT(FM.FechaTermino,'dd/MM/yyyy') AS 'FECHA_TÉRMINO', FM.HoraTermino AS 'HORA_TÉRMINO', FM.idEstadoLiquidacion AS 'LIQUIDACION',
	FORMAT(FM.FechaLiquidacion,'dd/MM/yyyy') AS 'FECHA_LIQUIDACIÓN', FM.Duracion AS 'DURACIÓN (DÍAS)' FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros FM
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_ClaseServicio CS WITH(NOLOCK) ON CS.idClaseServicio = FM.idClaseServicio
    WHERE FM.idFalla = @idFalla
END

------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 06-06-2023
-- Description:	LISTAR PERSONAL
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_ListarPersonalTransporte]
@Filtro VARCHAR(100)
AS
BEGIN
	SELECT TOP(20)
	Persona, RTRIM(LTRIM(NombreCompleto))AS 'NOMBRECOMPLETO', Busqueda, Documento
	FROM PersonaMast WITH(NOLOCK) WHERE ((EsEmpleado = 'S') AND (Estado = 'A') AND (Busqueda LIKE '%' + @Filtro + '%'))
END

------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 01-06-2023
-- Description:	LISTAR PLACAS DE TRANSPORTE
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_ListarPlacasTransporte]
@NroPlaca VARCHAR(30)
AS
BEGIN
	SELECT TOP(100)
	V.NumeroPlaca, V.NumeroPlaca AS 'PLACA' FROM OP_TR_Vehiculo V
    WHERE V.NumeroPlaca LIKE '%' + @NroPlaca + '%'
END

------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 01-06-2023
-- Description:	LISTAR SISTEMAS DE VEHÍCULOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_ListarSistemasVehiculos]
AS
BEGIN
	SELECT idSistemaVehiculo, Descripcion
	FROM ReportesApp_Mantenimiento_FallasMecanicas_SistemaVehiculo
	WHERE Estado = 1
END

------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 07-05-2023
-- Description:	LISTAR SUBSISTEMAS DE VEHÍCULOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_ListarSubSistemasVehiculos]
@idSistemaVehiculo INT
AS
BEGIN
	SELECT idSubSistema, Descripcion
	FROM ReportesApp_Mantenimiento_FallasMecanicas_SubSistemaVehiculo
	WHERE idSistemaVehiculo = @idSistemaVehiculo
END

------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 01-06-2023
-- Description:	LISTAR TIPO DE RECIBOS
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_ListarRecibos]
AS
BEGIN
	SELECT idTipoRecibo, Descripcion
	FROM ReportesApp_Mantenimiento_FallasMecanicas_TipoRecibo
	WHERE idTipoRecibo != 4
END

------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 01-06-2023
-- Description:	LISTAR ESTADO DE LIQUIDACIÓN
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_ListarLiquidacion]
AS
BEGIN
	SELECT idEstadoLiquidacion, Descripcion
	FROM ReportesApp_Mantenimiento_FallasMecanicas_Liquidacion
END

------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 02-06-2023
-- Description:	LIQUIDAR FALLA MECÁNICA
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_LiquidarFallaMecanica]
@idFalla INT,
@FechaLiquidacion DATETIME,
@idEstadoLiquidacion INT,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Falla Mecánica Liquidada.'

IF (@idFalla = 0)
BEGIN
	SET @Exito = '-1 = La falla seleccionada no existe.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
	SET FechaLiquidacion = @FechaLiquidacion,
	idEstadoLiquidacion = @idEstadoLiquidacion,
	UltimoUsuario = @Usuario,
	UltimaModificacion = GETDATE()
	WHERE idFalla = @idFalla
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
-- Create date: 03-06-2023
-- Description:	INSERTAR MONTO
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_InsertarMontoDetalle]
@Descripcion VARCHAR(100),
@Gasto DECIMAL(8,2),
@TipoRecibo INT,
@Comprobante VARCHAR(50),
@FechaGasto DATETIME
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

SET @Exito = '0 = Monto añadido.'

SET @correlativo = (SELECT MAX(idGastoDetalle) FROM ReportesApp_Mantenimiento_FallasMecanicas_GastoDetalle)
SET @correlativo = ISNULL(@correlativo,0) + 1

BEGIN TRAN
BEGIN TRY
	INSERT INTO ReportesApp_Mantenimiento_FallasMecanicas_GastoDetalle(idGastoDetalle, Descripcion, Gasto, TipoRecibo, Comprobante, fechaGasto)
	VALUES(@correlativo, @Descripcion, @Gasto, @TipoRecibo, @Comprobante, @FechaGasto)
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
-- Create date: 03-06-2023
-- Description:	LISTAR DETALLE DE MONTO
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_ListarMontoDetalle]
@idFalla INT
AS
BEGIN
	(SELECT GD.idGastoDetalle, GD.Descripcion, GD.Gasto, R.Descripcion AS 'Comprobante', GD.Comprobante AS 'Nro_Comprobante', GD.fechaGasto AS 'Fecha'
	FROM ReportesApp_Mantenimiento_FallasMecanicas_Gasto G
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_GastoDetalle GD ON GD.idGasto = G.idGasto
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_TipoRecibo R ON R.idTipoRecibo = GD.TipoRecibo
	WHERE idFalla = @idFalla)
	UNION
	(SELECT GD.idGastoDetalle, GD.Descripcion, GD.Gasto, R.Descripcion AS 'Comprobante', GD.Comprobante AS 'Nro_Comprobante', GD.fechaGasto AS 'Fecha'
	FROM ReportesApp_Mantenimiento_FallasMecanicas_GastoDetalle GD
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_TipoRecibo R ON R.idTipoRecibo = GD.TipoRecibo
	WHERE idGasto IS NULL)	
END

------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 03-06-2023
-- Description:	INSERTAR MONTO
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_EliminarMontoDetalle]
@idGastoDetalle INT
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Monto eliminado.'

IF (@idGastoDetalle = 0)
BEGIN
	SET @Exito = '-1 = El gasto seleccionado no existe.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	DELETE FROM ReportesApp_Mantenimiento_FallasMecanicas_GastoDetalle
	WHERE idGastoDetalle = @idGastoDetalle
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
-- Create date: 03-06-2023
-- Description:	INSERTAR MONTO
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_InsertarMonto]
@idFalla INT
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT
DECLARE @Suma DECIMAL(8,2)

SET @Exito = '0 = Monto añadido.'

IF (@idFalla = 0)
BEGIN
	SET @Exito = '-1 = La falla seleccionada no existe.'
	GOTO Terminar
END

SET @correlativo = (SELECT MAX(idGasto) FROM ReportesApp_Mantenimiento_FallasMecanicas_Gasto)
SET @correlativo = ISNULL(@correlativo,0) + 1

BEGIN TRAN
BEGIN TRY
	IF (@idFalla = (SELECT idFalla FROM ReportesApp_Mantenimiento_FallasMecanicas_Gasto WHERE idFalla = @idFalla)) BEGIN
		UPDATE ReportesApp_Mantenimiento_FallasMecanicas_GastoDetalle
		SET idGasto = (SELECT idGasto FROM ReportesApp_Mantenimiento_FallasMecanicas_Gasto WHERE idFalla = @idFalla)
		WHERE idGasto IS NULL
		
		SET @Suma = (SELECT SUM(Gasto) FROM ReportesApp_Mantenimiento_FallasMecanicas_GastoDetalle WHERE idGasto = (SELECT idGasto FROM ReportesApp_Mantenimiento_FallasMecanicas_Gasto WHERE idFalla = @idFalla))
			
		UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Gasto
		SET GastoTotal = @Suma
		WHERE idGasto = (SELECT idGasto FROM ReportesApp_Mantenimiento_FallasMecanicas_Gasto WHERE idFalla = @idFalla)
	END
	ELSE BEGIN
		UPDATE ReportesApp_Mantenimiento_FallasMecanicas_GastoDetalle
		SET idGasto = @correlativo
		WHERE idGasto IS NULL
		
		SET @Suma = (SELECT SUM(Gasto) FROM ReportesApp_Mantenimiento_FallasMecanicas_GastoDetalle WHERE idGasto = @correlativo)
	
		INSERT INTO ReportesApp_Mantenimiento_FallasMecanicas_Gasto(idGasto, GastoTotal, idFalla)
		VALUES (@correlativo, @Suma, @idFalla)
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
-- Create date: 23-06-2023
-- Description:	LISTAR ÚLTIMA ORDEN DE COMPRA
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_ListarPrecioPetroleo]
AS
BEGIN
	SELECT TOP(1) WH_TransaccionDetalle.TipoDocumento, WH_TransaccionDetalle.NumeroDocumento,
	WH_TransaccionDetalle.Item, WH_TransaccionDetalle.Cantidad, CAST(WH_TransaccionDetalle.PrecioUnitario AS DECIMAL(10,2)) AS PrecioUnitario,
	WH_TransaccionDetalle.MontoTotal, WH_ItemMast.DescripcionLocal
	FROM WH_TransaccionDetalle LEFT OUTER JOIN WH_ItemMast ON WH_TransaccionDetalle.Item = WH_ItemMast.Item
	WHERE (WH_TransaccionDetalle.Item = '1701001001') AND (WH_TransaccionDetalle.TipoDocumento = 'NI') AND (WH_TransaccionDetalle.ReferenciaTipoDocumento = 'OC')
	ORDER BY WH_TransaccionDetalle.NumeroDocumento DESC
END

------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 25-10-2023
-- Description:	GENERAR LISTA DE FALLAS PENDIENTES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_CorreoFallasPendientes]
AS
BEGIN
	DECLARE @Mensaje AS VARCHAR(MAX), @Asunto VARCHAR(300)
	DECLARE @LiquidacionesPendientes VARCHAR(MAX) = ''

	SELECT @LiquidacionesPendientes = @LiquidacionesPendientes + '<tr>'
									  + '<td style="background-color: #EEE8AA">' + FM.CodEstado+'-'+CONVERT(VARCHAR,FM.idFalla) + '</td>'
									  + '<td style="background-color: #EEE8AA">' + V.NumeroPlaca + '</td>'
									  + '<td style="background-color: #EEE8AA">' + ISNULL(R.NumeroPlaca,'-') + '</td>'
									  + '<td style="background-color: #EEE8AA">' + A.Descripcion + '</td>'
									  + '<td style="background-color: #EEE8AA">' + FM.Motivo + '</td>'
									  + '<td style="background-color: #EEE8AA">' + FM.Ubicacion + '</td>'
									  + '<td style="background-color: #EEE8AA">' + CONVERT(CHAR(10),FM.FechaInicio,103)+' '+RIGHT(RTRIM(CONVERT(CHAR(26),FM.HoraInicio,22)),12) + '</td>'
									  + '<td style="background-color: #EEE8AA">' + CONVERT(CHAR(10),FM.FechaTermino,103)+' '+RIGHT(RTRIM(CONVERT(CHAR(26),FM.HoraTermino,22)),12) + '</td>'
									  + '<td style="background-color: #EEE8AA">' + CS.Descripcion + '</td>'
									  + '<td style="background-color: #EEE8AA">' + CONVERT(VARCHAR, DATEDIFF(DAY, FM.FechaTermino, GETDATE())) + '</td>'
									  + '<td style="background-color: #EEE8AA">' + LI.Descripcion + '</td>'
									  + '</tr>' 
	FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros FM
	LEFT JOIN ReportesApp_Operacion_Previaje_Registros RE WITH(NOLOCK) ON FM.NroTicket = RE.NroTicket
	LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = RE.idTracto
	LEFT JOIN OP_TR_Vehiculo R WITH(NOLOCK) ON R.IdVehiculo = RE.idSemirremolque
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_TipoAuxilio A WITH(NOLOCK) ON A.idTipoAuxilio = FM.idTipoAuxilio
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_ClaseServicio CS WITH(NOLOCK) ON CS.idClaseServicio = FM.idClaseServicio
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_Liquidacion LI WITH(NOLOCK) ON LI.idEstadoLiquidacion = FM.idEstadoLiquidacion
	WHERE FM.idEstadoFalla = 2 AND FM.idEstadoLiquidacion = 1 AND (FM.FechaTermino BETWEEN DATEADD(DAY,-7,GETDATE()) AND GETDATE())
	ORDER BY FM.CodEstado ASC
	
	SET @Asunto = 'LIQUIDACIÓN DE AUXILIOS DE UNIDADES'

	SET @Mensaje = '<p><h2>LISTA SEMANAL DE FALLAS DE UNIDADES POR LIQUIDAR</h2></p>'
				  +'<p>Estas son las fallas mecánicas que quedan por liquidar esta semana: </p>'
				  + '<p><table border="1" cellspacing="1" cellpadding="1" >
					    <tr>
						    <td align="center" style="background-color:red; color: white"><b>CÓDIGO</b></td>
							<td align="center" style="background-color:red; color: white"><b>TRACTO</b></td>
							<td align="center" style="background-color:red; color: white"><b>SEMIRREMOLQUE</b></td>
							<td align="center" style="background-color:red; color: white"><b>TIPO DE FALLA</b></td>
							<td align="center" style="background-color:red; color: white"><b>FALLA MECÁNICA</b></td>
							<td align="center" style="background-color:red; color: white"><b>UBICACIÓN</b></td>
							<td align="center" style="background-color:red; color: white"><b>FECHA DE INICIO</b></td>
							<td align="center" style="background-color:red; color: white"><b>FECHA DE SOLUCIÓN</b></td>
							<td align="center" style="background-color:red; color: white"><b>CLASE DE SERVICIO</b></td>
							<td align="center" style="background-color:red; color: white"><b>DÍAS TRASCURRIDOS</b></td>
							<td align="center" style="background-color:red; color: white"><b>LIQUIDACIÓN</b></td>
						</tr>
						<tbody>'
						+ ISNULL(@LiquidacionesPendientes, '') 	
						+'</tbody>
						</table>
					</p>'
				  +'<p>Fecha de envío: '+CONVERT(CHAR(10),GETDATE(),103)+RIGHT(RTRIM(CONVERT(CHAR(26),GETDATE(),22)),12)+'</p>'
				  +'<BR>'
				  +'<p><font face="verdana" size="2" color="DarkRed">Tecnologías de la Información - GRUPO TRANSPESA</font></p>'
	
	EXEC msdb.dbo.sp_send_dbmail 
		 @profile_name='AVISODESISTEMA',
		 @recipients = 'desarrollo2@transpesa.com.pe',
		 --@blind_copy_recipients = 'asistenteauditoria@transpesa.com.pe;jrojas@transpesa.com.pe;desarrollo@transpesa.com.pe;desarrollo2@transpesa.com.pe;' 
		 @subject = @Asunto,
		 @body_format = 'HTML',
		 @body = @Mensaje	
END

------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 26-10-2023
-- Description:	GENERAR LISTA DE AUXILIOS MECÁNICOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_CorreoReporteAuxilios]
AS
BEGIN
	DECLARE @Mensaje AS VARCHAR(MAX), @Asunto VARCHAR(300)
	DECLARE @ReporteAuxilios VARCHAR(MAX) = ''

	SELECT @ReporteAuxilios = @ReporteAuxilios + '<tr>'
							  + '<td style="background-color: #FFFACD">' + CONVERT(VARCHAR,V.NumeroPlaca) + '</td>'
							  + '<td style="background-color: #FFFACD">' + CONVERT(VARCHAR,COUNT(FM.idFalla)) + '</td>'
							  + '<td style="background-color: #FFFACD">' + CONVERT(VARCHAR,SUM(ISNULL(FM.Monto,0)+ISNULL(FM.PrecioTotal,0))) + '</td>'
							  + '<td style="background-color: #FFFACD">' + CONVERT(VARCHAR,SUM(ISNULL(FM.MontoComprobante,0))) + '</td>'
							  + '<td style="background-color: #FFFACD">' + CONVERT(VARCHAR,SUM(ISNULL(FM.Monto,0)+ISNULL(FM.MontoComprobante,0)+ISNULL(FM.PrecioTotal,0))) + '</td>'
							  + '</tr>'
	FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros FM
	LEFT JOIN ReportesApp_Operacion_Previaje_Registros RE WITH(NOLOCK) ON FM.NroTicket = RE.NroTicket
	LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = RE.idTracto
	WHERE FM.idEstadoFalla = 2 AND FM.idEstadoLiquidacion = 2 AND YEAR(FM.FechaTermino) = YEAR(GETDATE())
	GROUP BY V.NumeroPlaca
	ORDER BY SUM(ISNULL(FM.Monto,0)+ISNULL(FM.MontoComprobante,0)+ISNULL(FM.PrecioTotal,0)) DESC
	
	SET @Asunto = 'REPORTE DE AUXILIOS MECÁNICOS'

	SET @Mensaje = '<p><h2>LISTA DE MONTOS DE AUXILIOS MECÁNICOS</h2></p>'
				  +'<p>Estos son los costos de los auxilios mecánicos reportados durante el presente año: </p>'
				  + '<p><table border="1" cellspacing="1" cellpadding="1" >
					    <tr>
						    <td align="center" style="background-color:cyan"><b>UNIDAD</b></td>
							<td align="center" style="background-color:cyan"><b>TOTAL DE AUXILIOS</b></td>
							<td align="center" style="background-color:cyan"><b>MONTOS TRANSPESA</b></td>
							<td align="center" style="background-color:cyan"><b>MONTOS TERCEROS</b></td>
							<td align="center" style="background-color:cyan"><b>MONTO TOTAL</b></td>
						</tr>
						<tbody>'
						+ ISNULL(@ReporteAuxilios, '') 	
						+'</tbody>
						</table>
					</p>'
				  +'<p>Fecha de envío: '+CONVERT(CHAR(10),GETDATE(),103)+RIGHT(RTRIM(CONVERT(CHAR(26),GETDATE(),22)),12)+'</p>'
				  +'<BR>'
				  +'<p><font face="verdana" size="2" color="DarkRed">Tecnologías de la Información - GRUPO TRANSPESA</font></p>'
	
	EXEC msdb.dbo.sp_send_dbmail 
		 @profile_name='AVISODESISTEMA', -- REAL: @profile_name = 'AVISODESISTEMA',  -- PRUEBAS: @profile_name = 'AVISODELSISTEMA',
		 @recipients = 'desarrollo2@transpesa.com.pe',
		 --@blind_copy_recipients = 'asistenteauditoria@transpesa.com.pe;jrojas@transpesa.com.pe;desarrollo@transpesa.com.pe;desarrollo2@transpesa.com.pe;' 
		 @subject = @Asunto,
		 @body_format = 'HTML',
		 @body = @Mensaje	
END

-------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------

--COMANDO PARA EXCEL DE LIQUIDACIONES

SELECT W.idFalla, W.Tecnico, W.PlacaTecnico, W.FechaInicio, W.HoraInicio, W.FechaTermino, W.HoraTermino, W.Conductor, W.Ruta, W.NumeroPlaca,
W.Semirremolque, W.Ubicacion, W.TipoFalla, W.Motivo, W.Solucion, D.idGastoDetalle, D.fechaGasto, D.Comprobante, D.Descripcion, D.Gasto, C.GastoTotal
FROM ReportesApp_Mantenimiento_FallasMecanicas_ImportarWord W
LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_Gasto C ON W.idFalla = C.idFalla
LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_GastoDetalle D ON D.idGasto = C.idGasto
WHERE W.ClaseServicio = 'GT TRANSPESA' ORDER BY W.idFalla DESC, D.idGastoDetalle DESC



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 16-02-2026
-- Description:	LISTAR SOLUCION AUXILIOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_ListarSolucionAuxilio]
@idFalla INT,
@Servicio VARCHAR(20)
AS
BEGIN
	IF (@Servicio = 'GT TRANSPESA') BEGIN
		SELECT FM.NroTicket, OP.Descripcion AS 'Programacion', V.NumeroPlaca, R.NumeroPlaca AS 'Semirremolque', FM.UnidadAfectada, RE.FechaProgramacion AS 'FechaViaje',
		LTRIM(RTRIM(C.Nombre)) AS 'Conductor', RT.Descripcion AS 'Ruta', A.Descripcion AS 'TipoFalla', FM.Motivo, FM.Ubicacion,
		CONVERT(VARCHAR,FM.FechaInicio,103)+' '+CONVERT(VARCHAR,FM.HoraInicio,8) AS 'FechaInicio', CS.Descripcion AS 'ClaseServicio', FM.Tecnico, FM.idPlaca AS 'PlacaTecnico',
		CONVERT(VARCHAR,FM.FechaSalida,103)+' '+CONVERT(VARCHAR,FM.HoraSalida,8) AS 'FechaSalida', CONVERT(VARCHAR,FM.FechaLlegada,103)+' '+CONVERT(VARCHAR,FM.HoraLlegada,8) AS 'FechaLlegada',
		FM.Galones, FM.PrecioTotal, FM.Monto, SV.Descripcion AS 'SistemaVehiculo', FM.Solucion, CONVERT(VARCHAR,FM.FechaTermino,103)+' '+CONVERT(VARCHAR,FM.HoraTermino,8) AS 'FechaTermino',
		FM.Duracion, IC.Falla AS 'Imagen' FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros FM
		LEFT JOIN ReportesApp_Operacion_Previaje_Registros RE WITH(NOLOCK) ON FM.NroTicket = RE.NroTicket
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = RE.TipoProgramacion
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = ISNULL(FM.idTracto,RE.idTracto)
		LEFT JOIN OP_TR_Vehiculo R WITH(NOLOCK) ON R.IdVehiculo = ISNULL(FM.idCarreta,RE.idSemirremolque)
		LEFT JOIN OP_TR_Conductor C WITH(NOLOCK) ON C.IdConductor = RE.IdConductor
		LEFT JOIN PersonaMast CL WITH(NOLOCK) ON CL.Persona = RE.IdCliente
		LEFT JOIN OP_TR_RUTA RT WITH(NOLOCK) ON RT.IDRUTA = RE.IdRuta
		LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_TipoAuxilio A WITH(NOLOCK) ON A.idTipoAuxilio = FM.idTipoAuxilio
		LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_EstadoFalla EF WITH(NOLOCK) ON EF.idEstadoFalla = FM.idEstadoFalla
		LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_ClaseServicio CS WITH(NOLOCK) ON CS.idClaseServicio = FM.idClaseServicio
		LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_TipoRecibo TR WITH(NOLOCK) ON TR.idTipoRecibo = FM.idTipoRecibo
		LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_SistemaVehiculo SV WITH(NOLOCK) ON SV.idSistemaVehiculo = FM.idSistemaVehiculo
		LEFT JOIN ReportesApp_Mantenimiento_RegistroIncidencias_Cabecera IC ON FM.idFalla = IC.idFalla
		WHERE(FM.idFalla = @idFalla)
	END
	ELSE BEGIN
		SELECT FM.NroTicket, OP.Descripcion AS 'Programacion', V.NumeroPlaca, R.NumeroPlaca AS 'Semirremolque', FM.UnidadAfectada, RE.FechaProgramacion AS 'FechaViaje',
		LTRIM(RTRIM(C.Nombre)) AS 'Conductor', RT.Descripcion AS 'Ruta', A.Descripcion AS 'TipoFalla', FM.Motivo, FM.Ubicacion,
		CONVERT(VARCHAR,FM.FechaInicio,103)+' '+CONVERT(VARCHAR,FM.HoraInicio,8) AS 'FechaInicio', CS.Descripcion AS 'ClaseServicio', 
		FM.NombreTercero, FM.TelefonoTercero, CONVERT(VARCHAR,FM.FechaLlegada,103)+' '+CONVERT(VARCHAR,FM.HoraLlegada,8) AS 'FechaLlegada',
		TR.Descripcion AS 'TipoRecibo', FM.Comprobante, FM.MontoComprobante, SV.Descripcion AS 'SistemaVehiculo', FM.Solucion,
		CONVERT(VARCHAR,FM.FechaTermino,103)+' '+CONVERT(VARCHAR,FM.HoraTermino,8) AS 'FechaTermino', FM.Duracion, IC.Falla AS 'Imagen'
		FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros FM
		LEFT JOIN ReportesApp_Operacion_Previaje_Registros RE WITH(NOLOCK) ON FM.NroTicket = RE.NroTicket
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = RE.TipoProgramacion
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = ISNULL(FM.idTracto,RE.idTracto)
		LEFT JOIN OP_TR_Vehiculo R WITH(NOLOCK) ON R.IdVehiculo = ISNULL(FM.idCarreta,RE.idSemirremolque)
		LEFT JOIN OP_TR_Conductor C WITH(NOLOCK) ON C.IdConductor = RE.IdConductor
		LEFT JOIN PersonaMast CL WITH(NOLOCK) ON CL.Persona = RE.IdCliente
		LEFT JOIN OP_TR_RUTA RT WITH(NOLOCK) ON RT.IDRUTA = RE.IdRuta
		LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_TipoAuxilio A WITH(NOLOCK) ON A.idTipoAuxilio = FM.idTipoAuxilio
		LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_EstadoFalla EF WITH(NOLOCK) ON EF.idEstadoFalla = FM.idEstadoFalla
		LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_ClaseServicio CS WITH(NOLOCK) ON CS.idClaseServicio = FM.idClaseServicio
		LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_TipoRecibo TR WITH(NOLOCK) ON TR.idTipoRecibo = FM.idTipoRecibo
		LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_SistemaVehiculo SV WITH(NOLOCK) ON SV.idSistemaVehiculo = FM.idSistemaVehiculo
		LEFT JOIN ReportesApp_Mantenimiento_RegistroIncidencias_Cabecera IC ON FM.idFalla = IC.idFalla
		WHERE(FM.idFalla = @idFalla)
	END
END


