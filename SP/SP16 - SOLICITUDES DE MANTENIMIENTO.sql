
-- UPDATE Usuario SET Estado = 'A' WHERE Usuario = 'DEMO'

-- MODIFICAR TABLA ReportesApp_Mantenimiento_FallasMecanicas_Registros Y LLENARLA

-- MODIFICAR TABLA ReportesApp_Mantenimiento_FallasMecanicas_ImportarWord Y LLENARLA

-- CREAR TABLA ReportesApp_Mantenimiento_Solicitud_Componente Y LLENARLA

-- CREAR TABLA ReportesApp_Mantenimiento_Solicitud_Componente_Detalle Y LLENARLA

-- CREAR TABLA ReportesApp_Mantenimiento_Solicitud_Componente_PosicionLlanta Y LLENARLA

-- CREAR TABLA ReportesApp_Mantenimiento_Solicitud_Base Y LLENARLA

-- CREAR TABLA ReportesApp_Mantenimiento_Solicitud_Cisterna Y LLENARLA

-- CREAR TABLA ReportesApp_Mantenimiento_Solicitud_RegistroDetalle

-- CREAR TABLA ReportesApp_Mantenimiento_Solicitud_Registro

-- CREAR TABLA ReportesApp_Mantenimiento_Solicitud_PedidoRepuesto

-- CREAR TABLA ReportesApp_Mantenimiento_Solicitud_UbicacionTaller Y LLENARLA

-- CREAR TABLA ReportesApp_Mantenimiento_Solicitud_UbicacionHistorial

---------------------------------------------------------------------

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

SET @Exito = '0 = Falla Mecánica Añadida.'
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
	INSERT INTO ReportesApp_Mantenimiento_FallasMecanicas_Registros(idFalla, NroTicket, CodFalla, TipoFalla, CodEstado, idEstadoFalla, idTipoAuxilio, Motivo, FechaInicio, HoraInicio, Ubicacion, idEstadoLiquidacion, UsuarioCreacion, FechaCreacion, UltimoUsuario, UltimaModificacion)
	SELECT @correlativo, @NroTicket,
	SUBSTRING(Descripcion,1,2) + '-' + CONVERT(varchar(20),RIGHT('000'+LTRIM(RTRIM(@correlativo)),3)), @TipoFalla, 'FM',
	1, @idTipoAuxilio, @Motivo, @FechaInicio, @HoraInicio, @Ubicacion, 1, @Usuario, GETDATE(), @Usuario, GETDATE()
	FROM ReportesApp_Mantenimiento_FallasMecanicas_TipoAuxilio WHERE idTipoAuxilio = @idTipoAuxilio
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
-- Description:	EDITAR REGISTRO DE FALLAS MECÁNICAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_ModificarFalla]
@idFalla INT,
@idTipoAuxilio INT,
@TipoFalla VARCHAR(50),
@Motivo VARCHAR(250),
@Ubicacion VARCHAR(500),
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
	UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
	SET idTipoAuxilio = @idTipoAuxilio,
	TipoFalla = @TipoFalla,
	Motivo = @Motivo,
	Ubicacion = @Ubicacion,
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
@Solucion VARCHAR(250),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

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
	Solucion = @Solucion,
	UltimoUsuario = @Usuario,
	UltimaModificacion = GETDATE()
	WHERE idFalla = @idFalla
	
	INSERT INTO ReportesApp_Mantenimiento_FallasMecanicas_ImportarWord (idFalla,CodFalla,Tipo,Estado,NroTicket,Programacion,NumeroPlaca,Semirremolque,
				FechaViaje,Conductor,Cliente,Ruta,TipoFalla,Motivo,Ubicacion,FechaInicio,HoraInicio,ClaseServicio,Tecnico,PlacaTecnico,Galones,
				PrecioTotal,Monto,NombreTercero,TelefonoTercero,TipoRecibo,Comprobante,MontoComprobante,Solucion,FechaTermino,HoraTermino,
				Duracion,FechaLlegada,HoraLlegada,FechaSalida,HoraSalida,SistemaVehiculo)
	SELECT FM.idFalla, FM.CodFalla, FM.TipoFalla, EF.Descripcion, FM.NroTicket, OP.Descripcion, V.NumeroPlaca, R.NumeroPlaca, RE.FechaProgramacion,
	LTRIM(RTRIM(C.Nombre)), LTRIM(RTRIM(CL.Busqueda)), RT.Descripcion, A.Descripcion, FM.Motivo, FM.Ubicacion, FM.FechaInicio, FM.HoraInicio,
	CS.Descripcion, FM.Tecnico, FM.idPlaca, FM.Galones, FM.PrecioTotal, FM.Monto, FM.NombreTercero, FM.TelefonoTercero,
	TR.Descripcion, FM.Comprobante, FM.MontoComprobante, FM.Solucion, FM.FechaTermino, FM.HoraTermino, FM.Duracion, FM.FechaLlegada, FM.HoraLlegada,
	FM.FechaSalida, FM.HoraSalida, SV.Descripcion FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros FM
	LEFT JOIN ReportesApp_Operacion_Previaje_Registros RE WITH(NOLOCK) ON FM.NroTicket = RE.NroTicket
	LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = RE.TipoProgramacion
    LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = RE.idTracto
	LEFT JOIN OP_TR_Vehiculo R WITH(NOLOCK) ON R.IdVehiculo = RE.idSemirremolque
	LEFT JOIN OP_TR_Conductor C WITH(NOLOCK) ON C.IdConductor = RE.IdConductor
	LEFT JOIN PersonaMast CL WITH(NOLOCK) ON CL.Persona = RE.IdCliente
	LEFT JOIN OP_TR_RUTA RT WITH(NOLOCK) ON RT.IDRUTA = RE.IdRuta
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_TipoAuxilio A WITH(NOLOCK) ON A.idTipoAuxilio = FM.idTipoAuxilio
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_EstadoFalla EF WITH(NOLOCK) ON EF.idEstadoFalla = FM.idEstadoFalla
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_ClaseServicio CS WITH(NOLOCK) ON CS.idClaseServicio = FM.idClaseServicio
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_TipoRecibo TR WITH(NOLOCK) ON TR.idTipoRecibo = FM.idTipoRecibo
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_SistemaVehiculo SV WITH(NOLOCK) ON SV.idSistemaVehiculo = FM.idSistemaVehiculo
	WHERE(FM.idFalla = @idFalla)
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
	SELECT FM.idFalla AS 'ID', FM.CodFalla, FM.CodEstado, FM.CodEstado + '-' + CONVERT(VARCHAR,FM.idFalla) AS 'CODIGO', EF.Descripcion AS 'ESTADO', FM.NroTicket AS 'NRO_TICKET', 
	FM.TipoFalla AS 'TIPO', OP.Descripcion AS 'PROGRAMACION', V.NumeroPlaca AS 'PLACA', R.NumeroPlaca AS 'SEMIRREMOLQUE', FORMAT(RE.FechaProgramacion, 'dd/MM/yyyy') AS 'FECHA_VIAJE',
	LTRIM(RTRIM(C.Nombre)) AS 'CONDUCTOR', LTRIM(RTRIM(CL.Busqueda)) as 'CLIENTE', RT.Descripcion AS 'RUTA', FM.idTipoAuxilio, A.Descripcion AS 'TIPO_FALLA', FM.Motivo AS 'MOTIVO',
	FM.Ubicacion AS 'UBICACIÓN', FORMAT(FM.FechaInicio, 'dd/MM/yyyy') AS 'FECHA_INICIO', FM.HoraInicio AS 'HORA_INICIO', FM.Tecnico AS 'CONDUCTOR_CAMIONETA', FM.idPlaca AS 'PLACA_CAMIONETA',
	FM.Monto AS 'MONTO', FM.Galones AS 'GALONES', FM.PrecioUnitario AS 'PRECIO_UNITARIO', FM.PrecioTotal AS 'PRECIO_TOTAL', FM.NombreTercero AS 'NOMBRE_TECNICO',
	FM.TelefonoTercero AS 'TELEFONO_TERCER0', FM.Comprobante AS 'COMPROBANTE', FM.MontoComprobante AS 'MONTO_COMPROBANTE', FM.Solucion AS 'SOLUCIÓN',
	FORMAT(FM.FechaTermino, 'dd/MM/yyyy') AS 'FECHA_TÉRMINO', FM.HoraTermino AS 'HORA_TÉRMINO', FM.Duracion AS 'DURACIÓN'
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
	
	SELECT FM.idFalla AS 'ID', FM.CodFalla, FM.CodEstado, FM.CodEstado + '-' + CONVERT(VARCHAR,FM.idFalla) AS 'CODIGO', EF.Descripcion AS 'ESTADO',
	FM.NroTicket AS 'NRO_PREVIAJE', FM.TipoFalla AS 'TIPO', OP.Descripcion AS 'PROGRAMACION', V.NumeroPlaca AS 'PLACA', R.NumeroPlaca AS 'SEMIRREMOLQUE',
	A.Descripcion AS 'TIPO_FALLA', FM.Motivo AS 'MOTIVO', FM.Ubicacion AS 'UBICACIÓN', LTRIM(RTRIM(C.Nombre)) AS 'CONDUCTOR',
	CONVERT(VARCHAR,FM.FechaInicio,103) + ' ' + CONVERT(VARCHAR(8),FM.HoraInicio) AS 'FECHA_INICIO', FORMAT(RE.FechaProgramacion,'dd/MM/yyyy') AS 'FECHA_VIAJE',
	RT.Descripcion AS 'RUTA', CONVERT(VARCHAR, FM.FechaSalida,103) + ' ' + CONVERT(VARCHAR(8), FM.HoraSalida) AS 'FECHA_SALIDA',
	CONVERT(VARCHAR, FM.FechaLlegada,103) + ' ' + CONVERT(VARCHAR(8),FM.HoraLlegada) AS 'FECHA_LLEGADA', CONVERT(VARCHAR,FM.FechaTermino,103) + ' ' + CONVERT(VARCHAR(8),FM.HoraTermino)
	AS 'FECHA_TÉRMINO', FM.Duracion AS 'DURACIÓN (DÍAS)', CS.Descripcion AS 'CLASE_SERVICIO', FM.Tecnico AS 'CONDUCTOR_CAMIONETA', FM.idPlaca AS 'PLACA_CAMIONETA',
	FM.Monto AS 'MONTO', FM.Galones AS 'GALONES', FM.PrecioUnitario AS 'PRECIO_UNITARIO', FM.PrecioTotal AS 'PRECIO_TOTAL', FM.NombreTercero AS 'NOMBRE_TECNICO',
	FM.TelefonoTercero AS 'TELÉFONO/CELULAR', TR.Descripcion AS 'RECIBO', FM.Comprobante AS 'N° RECIBO', FM.MontoComprobante AS 'MONTO_COMPROBANTE',
	SV.Descripcion AS 'SISTEMA_VEHÍCULO', FM.Solucion AS 'SOLUCIÓN', LI.Descripcion AS 'ESTADO_LIQUIDACIÓN', FORMAT(FM.FechaLiquidacion,'dd/MM/yyyy') AS 'FECHA_LIQUIDACIÓN',
	FM.UsuarioCreacion, FORMAT(FM.FechaCreacion,'dd/MM/yyyy') AS FechaCreacion, FM.UltimoUsuario, FORMAT(FM.UltimaModificacion,'dd/MM/yyyy') AS UltimaModificacion
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
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_ClaseServicio CS WITH(NOLOCK) ON CS.idClaseServicio = FM.idClaseServicio
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_TipoRecibo TR WITH(NOLOCK) ON TR.idTipoRecibo = FM.idTipoRecibo
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_Liquidacion LI WITH(NOLOCK) ON LI.idEstadoLiquidacion = FM.idEstadoLiquidacion
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_SistemaVehiculo SV WITH(NOLOCK) ON SV.idSistemaVehiculo = FM.idSistemaVehiculo
    WHERE ((@NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @NumeroPlaca + '%') AND (FM.FechaInicio BETWEEN @FINICIO AND @FFIN)
			AND (@idEstadoFalla IS NULL OR FM.idEstadoFalla = @idEstadoFalla)) ORDER BY idFalla DESC
END

-----------------------------------------------
/*
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 05-09-2023
-- Description:	BÚSQUEDA VIAJES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_Solicitudes_BuscarViajes]
@Placa VARCHAR(250)
AS
BEGIN
	SELECT TOP(25) R.NroTicket, V.NumeroPlaca AS 'PLACA', S.NumeroPlaca AS 'SEMIRREMOLQUE', RT.Descripcion AS 'RUTA', R.FechaProgramacion AS 'FECHA',
	OP.Descripcion AS 'OPERACION', C.Nombre AS 'NOMBRE', ISNULL(UT.Odometro,0) AS 'ODOMETRO'
	FROM ReportesApp_Operacion_Previaje_Registros R
	LEFT JOIN OP_TR_Conductor C WITH(NOLOCK) ON C.IdConductor = R.IdConductor
	LEFT JOIN OP_TR_Ruta RT WITH(NOLOCK) ON RT.IdRuta = R.IdRuta
	LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = R.idTracto
	LEFT JOIN OP_TR_Vehiculo S WITH(NOLOCK) ON S.IdVehiculo = R.idSemirremolque
	LEFT JOIN ReportesApp_Combustible_OdometroUT UT WITH(NOLOCK) ON V.NumeroPlaca = UT.NumeroPlaca
	LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = R.TipoProgramacion
	WHERE ((@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') OR (@Placa IS NULL OR S.NumeroPlaca LIKE '%' + @Placa + '%')) AND (R.TipoProgramacion IN (1,2,3,4))
	ORDER BY R.FechaProgramacion DESC
END
*/
-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 15-12-2023
-- Description:	LISTAR INFORMACIÓN DE PLACAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_Solicitudes_ListarUnidades]
@Placa VARCHAR(20)
AS
BEGIN
	SELECT TOP(25) V.IdVehiculo, V.NumeroPlaca AS 'PLACA', STV.Descripcion AS 'TIPO', ISNULL(UC.IdProgramacion,-1),
	ISNULL(OP.Descripcion,'SIN PROGRAMACION') AS 'PROGRAMACION', ISNULL(UT.Odometro,0) AS 'ODOMETRO'
	FROM OP_TR_Vehiculo V
	LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC WITH(NOLOCK) ON V.IdVehiculo = UC.IdUnidad
	LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = UC.IdProgramacion
	LEFT JOIN ReportesApp_Combustible_OdometroUT UT WITH(NOLOCK) ON V.NumeroPlaca = UT.NumeroPlaca
	LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_SubTipoVehiculo STV WITH(NOLOCK) ON STV.idSubTipoVehiculo = V.SubTipoVehiculo
	WHERE (V.Estado = 2) AND (@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%')
END

-----------------------------------------------
/*
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 30-11-2023
-- Description:	BUSCAR TRACTOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlBaterias_BuscarTractos]
@Placa VARCHAR(20)
AS
BEGIN
	SELECT TOP(25) V.IdVehiculo, V.NumeroPlaca AS 'PLACA', SV.Descripcion AS 'TIPO_VEHICULO'
	FROM OP_TR_Vehiculo V
	LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
	WHERE (V.Estado = 2) AND (@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%')
END
*/
-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 06-09-2023
-- Description:	LISTAR COMPONENTES MANTENIMIENTO
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Mantenimiento_Solicitudes_ListarComponentes]
@Opcion INT,
@idComponente INT
AS
BEGIN
	IF (@Opcion = 1) BEGIN  -- LISTAR COMPONENTES
		SELECT idComponente, Descripcion
		FROM ReportesApp_Mantenimiento_Solicitud_Componente
	END

	IF (@Opcion = 2) BEGIN  -- LISTAR COMPONENTES DETALLE
		SELECT idComponenteDetalle, Descripcion
		FROM ReportesApp_Mantenimiento_Solicitud_Componente_Detalle
		WHERE (idComponente = @idComponente OR idComponente = 0)
	END

	IF (@Opcion = 3) BEGIN  -- LISTAR POSICION DE NEUMÁTICOS
		SELECT idPosicionLlanta, Descripcion
		FROM ReportesApp_Mantenimiento_Solicitud_Componente_PosicionLlanta
	END
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 22-09-2023
-- Description:	LISTAR BASES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_Solicitudes_ListarBases]
@Opcion INT
AS
BEGIN
	IF (@Opcion = 1) BEGIN
		SELECT idBase, DescripcionBase
		FROM ReportesApp_Mantenimiento_Solicitud_Base
	END
	
	IF (@Opcion = 2) BEGIN
		SELECT idBase, DescripcionBase
		FROM ReportesApp_Mantenimiento_Solicitud_Base
		WHERE idBase != 0
	END

	IF (@Opcion = 3) BEGIN
		SELECT idCisterna, Descripcion
		FROM ReportesApp_Mantenimiento_Solicitud_Cisterna
	END

	IF (@Opcion = 4) BEGIN
		SELECT idUbicacionTaller, Descripcion
		FROM ReportesApp_Mantenimiento_Solicitud_UbicacionTaller
	END
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 06-09-2023
-- Description:	INSERTAR DETALLE SOLICITUD
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_Solicitudes_SolicitudDetalle_Insertar]
@idComponente INT,
@idComponenteDetalle INT,
@idPosicionLlanta INT,
@Observacion VARCHAR(300)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

SET @Exito = '0 = Monto añadido.'

IF (@idComponente = (SELECT idComponente FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle WHERE idComponente = @idComponente AND idComponenteDetalle = @idComponenteDetalle AND idPosicionLlanta = @idPosicionLlanta AND idSolicitud IS NULL)
	AND @idComponenteDetalle = (SELECT idComponenteDetalle FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle WHERE idComponente = @idComponente AND idComponenteDetalle = @idComponenteDetalle AND idPosicionLlanta = @idPosicionLlanta AND idSolicitud IS NULL)
	AND @idPosicionLlanta = (SELECT idPosicionLlanta FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle WHERE idComponente = @idComponente AND idComponenteDetalle = @idComponenteDetalle AND idPosicionLlanta = @idPosicionLlanta AND idSolicitud IS NULL))
BEGIN
	SET @Exito = '-1 = Este detalle ya fue registrado.'
	GOTO Terminar
END

SET @correlativo = (SELECT MAX(idSolicitudDetalle) FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle WHERE idSolicitud IS NULL)
SET @correlativo = ISNULL(@correlativo,0) + 1

BEGIN TRAN
BEGIN TRY
	INSERT INTO ReportesApp_Mantenimiento_Solicitud_RegistroDetalle(idSolicitudDetalle, idComponente, idComponenteDetalle, idPosicionLlanta, Observacion, Estado)
	VALUES(@correlativo, @idComponente, @idComponenteDetalle, @idPosicionLlanta, @Observacion, 'PENDIENTE')
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

------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 04-10-2024
-- Description:	INSERTAR DETALLE SOLICITUD
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_Solicitudes_SolicitudDetalle_Insertar2]
@idSolicitud INT,
@idComponente INT,
@idComponenteDetalle INT,
@idPosicionLlanta INT,
@Observacion VARCHAR(300)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

SET @Exito = '0 = Monto añadido.'

IF (@idComponente = (SELECT idComponente FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle WHERE idComponente = @idComponente AND idComponenteDetalle = @idComponenteDetalle AND idPosicionLlanta = @idPosicionLlanta AND idSolicitud = @idSolicitud)
	AND @idComponenteDetalle = (SELECT idComponenteDetalle FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle WHERE idComponente = @idComponente AND idComponenteDetalle = @idComponenteDetalle AND idPosicionLlanta = @idPosicionLlanta AND idSolicitud = @idSolicitud)
	AND @idPosicionLlanta = (SELECT idPosicionLlanta FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle WHERE idComponente = @idComponente AND idComponenteDetalle = @idComponenteDetalle AND idPosicionLlanta = @idPosicionLlanta AND idSolicitud = @idSolicitud))
BEGIN
	SET @Exito = '-1 = Este detalle ya fue registrado.'
	GOTO Terminar
END

SET @correlativo = (SELECT MAX(idSolicitudDetalle) FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle WHERE idSolicitud = @idSolicitud)
SET @correlativo = ISNULL(@correlativo,0) + 1

BEGIN TRAN
BEGIN TRY
	INSERT INTO ReportesApp_Mantenimiento_Solicitud_RegistroDetalle(idSolicitudDetalle, idSolicitud, idComponente, idComponenteDetalle, idPosicionLlanta, Observacion, Estado)
	VALUES(@correlativo, @idSolicitud, @idComponente, @idComponenteDetalle, @idPosicionLlanta, @Observacion, 'PENDIENTE')
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

------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 06-09-2023
-- Description:	LISTAR SOLICITUDES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_SolicitudDetalle_Listar]
@Opcion INT,
@idSolicitud INT,
@idSolicitudDetalle INT
AS
BEGIN
	IF (@Opcion = 1) BEGIN  -- LISTAR DETALLE SIN IDSOLICITUD
		SELECT SD.idSolicitudDetalle, SD.idSolicitud, C.Descripcion AS 'COMPONENTE', CD.Descripcion AS 'DETALLE', P.Descripcion AS 'POSICION_LLANTA',
		SD.Observacion AS 'OBSERVACION', SD.idOT AS 'OT', SD.Estado AS 'ESTADO'
		FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle SD
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Componente C ON C.idComponente = SD.idComponente
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Componente_Detalle CD ON CD.idComponenteDetalle = SD.idComponenteDetalle
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Componente_PosicionLlanta P ON P.idPosicionLlanta = SD.idPosicionLlanta
		WHERE SD.idSolicitud IS NULL
	END

	IF (@Opcion = 2) BEGIN  -- LISTAR DETALLE CON IDSOLICITUD
		SELECT SD.idSolicitudDetalle, SD.idSolicitud, C.Descripcion AS 'COMPONENTE', CD.Descripcion AS 'DETALLE', P.Descripcion AS 'POSICION_LLANTA',
		SD.Observacion AS 'OBSERVACION', SD.idOT AS 'OT', SD.Estado AS 'ESTADO'
		FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle SD
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Componente C ON C.idComponente = SD.idComponente
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Componente_Detalle CD ON CD.idComponenteDetalle = SD.idComponenteDetalle
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Componente_PosicionLlanta P ON P.idPosicionLlanta = SD.idPosicionLlanta
		WHERE SD.idSolicitud = @idSolicitud
	END
END

------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 06-09-2023
-- Description:	EDITAR SOLICITUDES DETALLES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_SolicitudDetalle_Editar]
@Opcion INT,
@idSolicitudDetalle INT,
@idSolicitud INT,
@OT VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Modificado correctamente.'

IF (@idSolicitudDetalle = 0) BEGIN
	SET @Exito = '-1 = La fila seleccionado no existe.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN  -- ELIMINAR
		IF @idSolicitud = 0 BEGIN
			DELETE FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle
			WHERE idSolicitudDetalle = @idSolicitudDetalle AND idSolicitud IS NULL
		END
		ELSE BEGIN
			DELETE FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle
			WHERE idSolicitudDetalle = @idSolicitudDetalle AND idSolicitud = @idSolicitud
		END
	END

	IF (@Opcion = 2) BEGIN  -- ASIGNAR OT
		UPDATE ReportesApp_Mantenimiento_Solicitud_RegistroDetalle
		SET idOT = @OT
		WHERE idSolicitudDetalle = @idSolicitudDetalle AND idSolicitud = @idSolicitud
	END

	IF (@Opcion = 3) BEGIN  -- CAMBIAR ESTADO DETALLE
		IF ((SELECT Estado FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle WHERE idSolicitudDetalle = @idSolicitudDetalle
		AND idSolicitud = @idSolicitud) = 'PENDIENTE') BEGIN
			UPDATE ReportesApp_Mantenimiento_Solicitud_RegistroDetalle
			SET Estado = 'TERMINADO'
			WHERE idSolicitudDetalle = @idSolicitudDetalle AND idSolicitud = @idSolicitud
		END
		ELSE BEGIN
			UPDATE ReportesApp_Mantenimiento_Solicitud_RegistroDetalle
			SET Estado = 'PENDIENTE'
			WHERE idSolicitudDetalle = @idSolicitudDetalle AND idSolicitud = @idSolicitud
		END
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
-- Create date: 07-09-2023
-- Description:	LISTAR OT POR PLACA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_Solicitudes_FiltrarOT]
@Placa VARCHAR(20)
AS
BEGIN
	SELECT ME_OrdenTrabajo.NumeroOrden AS 'Nro_OT', CONVERT(NVARCHAR,ME_OrdenTrabajo.FechaProgramada,103) AS 'FechaOT', ME_OrdenTrabajo.MaquinaCodigo AS 'PLACA',
	LTRIM(RTRIM(ISNULL(ME_MaquinaTipo.descripcionlocal,''))) AS 'TIPO', LTRIM(RTRIM(ME_OrdenTrabajo.Descripcion)) AS 'DESCRIPCION'
	FROM ME_OrdenTrabajo
	LEFT OUTER JOIN ME_Ubicacion ON ME_OrdenTrabajo.Ubicacion = ME_Ubicacion.Ubicacion
	LEFT OUTER JOIN PersonaMast PersonaMast_a ON ME_OrdenTrabajo.PreparadoPor = PersonaMast_a.Persona
	LEFT OUTER JOIN FA_Activo ON ME_OrdenTrabajo.Activo = FA_Activo.Activo
	LEFT OUTER JOIN PersonaMast PersonaMast_b ON ME_OrdenTrabajo.Cliente = PersonaMast_b.Persona
	INNER JOIN ME_Maquina WITH(NOLOCK) ON (ME_OrdenTrabajo.MaquinaCodigo=ME_Maquina.MaquinaCodigo)
	INNER JOIN ME_MaquinaMarca WITH(NOLOCK) ON(ME_Maquina.Marca=ME_MaquinaMarca.Marca)
	INNER JOIN ME_MaquinaTipo WITH(NOLOCK) ON  (ME_MaquinaTipo.TipoMaquina= ME_Maquina.TipoMaquina )
	WHERE (ME_OrdenTrabajo.Estado = 'PG') AND (ME_OrdenTrabajo.MaquinaCodigo = @Placa)
	ORDER BY ME_OrdenTrabajo.NumeroOrden DESC 
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 08-09-2023
-- Description:	INSERTAR SOLICITUD
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_Solicitudes_Insertar]
@idTracto INT,
@TipoOperacion INT,
@TipoMtto VARCHAR(100),
@TipoTrabajo VARCHAR(100),
@Kilometraje DECIMAL(16,2),
@idCisterna INT,
@UnidadFalla VARCHAR(20),
@Usuario VARCHAR(50)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT
DECLARE @correlativo2 INT

SET @Exito = '0 = Solicitud añadida.'

IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_Solicitud_Registro WHERE idTracto = @idTracto)) BEGIN
	IF ((SELECT TOP(1) Estado FROM ReportesApp_Mantenimiento_Solicitud_Registro WHERE idTracto = @idTracto ORDER BY FechaCreacion DESC) != 'COMPLETADO') BEGIN
		SET @Exito = '-1 = Esta placa ya ha sido registrada en una solicitud pendiente el día '+
					(SELECT TOP(1) CONVERT(VARCHAR,FechaCreacion,103) FROM ReportesApp_Mantenimiento_Solicitud_Registro WHERE idTracto = @idTracto ORDER BY FechaCreacion DESC)+'.'
		GOTO Terminar
	END
END

SET @correlativo = (SELECT MAX(idSolicitud) FROM ReportesApp_Mantenimiento_Solicitud_Registro)
SET @correlativo = ISNULL(@correlativo, 0) + 1

BEGIN TRAN
BEGIN TRY
	INSERT INTO ReportesApp_Mantenimiento_Solicitud_Registro(idSolicitud, idTracto, TipoOperacion, Kilometraje, idBase, idCisterna, CodEstado, Estado, UnidadFalla,
															 UsuarioCreacion, FechaCreacion, TipoMtto, TipoTrabajo, UbicacionTaller)
	VALUES(@correlativo, @idTracto, @TipoOperacion, @Kilometraje, -1, @idCisterna, 'SM', 'SOLICITADO', @UnidadFalla, @Usuario, GETDATE(), @TipoMtto, @TipoTrabajo, '')

	UPDATE ReportesApp_Mantenimiento_Solicitud_RegistroDetalle 
	SET idSolicitud = @correlativo
	WHERE idSolicitud IS NULL

	DECLARE @Contador INT = 1

	WHILE (@Contador <= (SELECT MAX(idSolicitudDetalle) FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle WHERE idSolicitud = @correlativo)) BEGIN
		SET @correlativo2 = (SELECT MAX(idMttoC) FROM ReportesApp_Mantenimiento_MttoCorrectivo_Registro)
		SET @correlativo2 = ISNULL(@correlativo2, 0) + 1

		DECLARE @Placa VARCHAR(20) = (SELECT NumeroPlaca FROM OP_TR_Vehiculo WHERE IdVehiculo = @idTracto)

		INSERT INTO ReportesApp_Mantenimiento_MttoCorrectivo_Registro(idMttoC, Placa, Operacion, Origen, FechaReportada, Sistema, Descripcion, Observacion,
		Estado, UsuarioCreacion, FechaCreacion, UsuarioModificacion, FechaModificacion)
		SELECT @correlativo2, @Placa, OP.Descripcion, 'SOLICITUD', RC.FechaCreacion, C1.Descripcion, CD.Descripcion, RD.Observacion, 'PENDIENTE',
		@Usuario, GETDATE(), @Usuario, GETDATE()
		FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle RD
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Registro RC ON RC.idSolicitud = RD.idSolicitud
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC WITH(NOLOCK) ON UC.IdUnidad = RC.idTracto
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = UC.IdProgramacion
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Componente C1 ON C1.idComponente = RD.idComponente
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Componente_Detalle CD ON CD.idComponenteDetalle = RD.idComponenteDetalle
		WHERE RD.idSolicitud = @correlativo AND RD.idSolicitudDetalle = @Contador

		SET @Contador = @Contador + 1
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
-- Create date: 09-09-2023
-- Description:	LISTAR SOLICITUDES MANTENIMIENTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_Solicitudes_Listar]
@NumeroPlaca VARCHAR(20),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@idBase INT,
@EstadoFalla VARCHAR(25)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@EstadoFalla = '') BEGIN
		SELECT SM.idSolicitud, SM.CodEstado + '-' + CONVERT(VARCHAR,SM.idSolicitud) AS 'CODIGO', SM.TipoMtto AS 'TIPO_MTTO', SM.TipoTrabajo AS 'TIPO_TRABAJO', OP.Descripcion AS 'PROGRAMACION',
		V.NumeroPlaca AS 'PLACA', ISNULL((SELECT TOP(1) FechaInicio FROM ReportesApp_Operacion_Previaje_Registros WHERE Estado = 1 AND idTracto = SM.idTracto ORDER BY FechaInicio DESC),
		(SELECT TOP(1) FechaInicio FROM ReportesApp_Operacion_Previaje_Registros WHERE Estado = 1 AND idSemirremolque = SM.idTracto ORDER BY FechaInicio DESC)) AS 'ULTIMA_FECHA_PROG',
		STV.Descripcion AS 'TIPO', SM.UnidadFalla AS 'FALLA', SM.Kilometraje AS 'KILOMETRAJE', B.DescripcionBase AS 'UBICACIÓN', SM.UbicacionTaller AS 'TALLER',
		SM.FechaCreacion AS 'FECHA_SOLICITUD', SM.FechaEstimada AS 'FECHA_ESTIMADA', SM.FechaRecepcion AS 'FECHA_RECEPCION', P.Estado AS 'ESTADO_REPUESTO', SM.FechaProg AS 'FECHA_PROYECTADA',
		SM.FechaReprog AS 'FECHA_REPROGRAMADA', SM.FechaEntrega AS 'FECHA_ENTREGADA', SM.Estado AS 'ESTADO', SM.UsuarioCreacion, SM.FechaCreacion, SM.UltimoUsuario, SM.UltimaModificacion
		FROM ReportesApp_Mantenimiento_Solicitud_Registro SM
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = SM.idTracto
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC WITH(NOLOCK) ON UC.IdUnidad = V.IdVehiculo
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = UC.IdProgramacion
		LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_SubTipoVehiculo STV WITH(NOLOCK) ON STV.idSubTipoVehiculo = V.SubTipoVehiculo
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Base B ON B.idBase = SM.idBase
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_PedidoRepuesto P WITH(NOLOCK) ON P.idSolicitud = SM.idSolicitud
		WHERE ((@NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @NumeroPlaca + '%') AND (SM.FechaCreacion IS NULL OR SM.FechaCreacion BETWEEN @FINICIO AND @FFIN)
		AND (@idBase = 0 OR SM.idBase = @idBase))
		ORDER BY SM.idSolicitud DESC
	END
	ELSE BEGIN
		IF (@EstadoFalla = 'PROGRAMADO') BEGIN
			SELECT SM.idSolicitud, SM.CodEstado + '-' + CONVERT(VARCHAR,SM.idSolicitud) AS 'CODIGO', SM.TipoMtto AS 'TIPO_MTTO', SM.TipoTrabajo AS 'TIPO_TRABAJO', OP.Descripcion AS 'PROGRAMACION',
			V.NumeroPlaca AS 'PLACA', ISNULL((SELECT TOP(1) FechaInicio FROM ReportesApp_Operacion_Previaje_Registros WHERE Estado = 1 AND idTracto = SM.idTracto ORDER BY FechaInicio DESC),
			(SELECT TOP(1) FechaInicio FROM ReportesApp_Operacion_Previaje_Registros WHERE Estado = 1 AND idSemirremolque = SM.idTracto ORDER BY FechaInicio DESC)) AS 'ULTIMA_FECHA_PROG',
			STV.Descripcion AS 'TIPO', SM.UnidadFalla AS 'FALLA', SM.Kilometraje AS 'KILOMETRAJE', B.DescripcionBase AS 'UBICACIÓN', SM.UbicacionTaller AS 'TALLER',
			SM.FechaCreacion AS 'FECHA_SOLICITUD', SM.FechaEstimada AS 'FECHA_ESTIMADA', SM.FechaRecepcion AS 'FECHA_RECEPCION', P.Estado AS 'ESTADO_REPUESTO', SM.FechaProg AS 'FECHA_PROYECTADA',
			SM.FechaReprog AS 'FECHA_REPROGRAMADA', SM.FechaEntrega AS 'FECHA_ENTREGADA', SM.Estado AS 'ESTADO', SM.UsuarioCreacion, SM.FechaCreacion, SM.UltimoUsuario, SM.UltimaModificacion
			FROM ReportesApp_Mantenimiento_Solicitud_Registro SM
			LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = SM.idTracto
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC WITH(NOLOCK) ON UC.IdUnidad = V.IdVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = UC.IdProgramacion
			LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_SubTipoVehiculo STV WITH(NOLOCK) ON STV.idSubTipoVehiculo = V.SubTipoVehiculo
			LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Base B ON B.idBase = SM.idBase
			LEFT JOIN ReportesApp_Mantenimiento_Solicitud_PedidoRepuesto P WITH(NOLOCK) ON P.idSolicitud = SM.idSolicitud
			WHERE ((@NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @NumeroPlaca + '%') AND (SM.FechaCreacion IS NULL OR SM.FechaCreacion BETWEEN @FINICIO AND @FFIN)
			AND (@idBase = 0 OR SM.idBase = @idBase) AND (SM.Estado IN ('PROGRAMADO','REPROGRAMADO')))
			ORDER BY SM.idSolicitud DESC
		END
		ELSE BEGIN
			SELECT SM.idSolicitud, SM.CodEstado + '-' + CONVERT(VARCHAR,SM.idSolicitud) AS 'CODIGO', SM.TipoMtto AS 'TIPO_MTTO', SM.TipoTrabajo AS 'TIPO_TRABAJO', OP.Descripcion AS 'PROGRAMACION',
			V.NumeroPlaca AS 'PLACA', ISNULL((SELECT TOP(1) FechaInicio FROM ReportesApp_Operacion_Previaje_Registros WHERE Estado = 1 AND idTracto = SM.idTracto ORDER BY FechaInicio DESC),
			(SELECT TOP(1) FechaInicio FROM ReportesApp_Operacion_Previaje_Registros WHERE Estado = 1 AND idSemirremolque = SM.idTracto ORDER BY FechaInicio DESC)) AS 'ULTIMA_FECHA_PROG',
			STV.Descripcion AS 'TIPO', SM.UnidadFalla AS 'FALLA', SM.Kilometraje AS 'KILOMETRAJE', B.DescripcionBase AS 'UBICACIÓN', SM.UbicacionTaller AS 'TALLER',
			SM.FechaCreacion AS 'FECHA_SOLICITUD', SM.FechaEstimada AS 'FECHA_ESTIMADA', SM.FechaRecepcion AS 'FECHA_RECEPCION', P.Estado AS 'ESTADO_REPUESTO', SM.FechaProg AS 'FECHA_PROYECTADA',
			SM.FechaReprog AS 'FECHA_REPROGRAMADA', SM.FechaEntrega AS 'FECHA_ENTREGADA', SM.Estado AS 'ESTADO', SM.UsuarioCreacion, SM.FechaCreacion, SM.UltimoUsuario, SM.UltimaModificacion
			FROM ReportesApp_Mantenimiento_Solicitud_Registro SM
			LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = SM.idTracto
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC WITH(NOLOCK) ON UC.IdUnidad = V.IdVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = UC.IdProgramacion
			LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_SubTipoVehiculo STV WITH(NOLOCK) ON STV.idSubTipoVehiculo = V.SubTipoVehiculo
			LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Base B ON B.idBase = SM.idBase
			LEFT JOIN ReportesApp_Mantenimiento_Solicitud_PedidoRepuesto P WITH(NOLOCK) ON P.idSolicitud = SM.idSolicitud
			WHERE ((@NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @NumeroPlaca + '%') AND (SM.FechaCreacion IS NULL OR SM.FechaCreacion BETWEEN @FINICIO AND @FFIN)
			AND (@idBase = 0 OR SM.idBase = @idBase) AND (@EstadoFalla IS NULL OR SM.Estado LIKE '%' + @EstadoFalla + '%'))
			ORDER BY SM.idSolicitud DESC
		END
	END
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 11-09-2023
-- Description:	OBTENER ESTADO DE UNA PLACA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_Solicitudes_ObtenerEstado]
@NumeroPlaca VARCHAR(20)
AS
BEGIN
	SELECT TOP(1) SM.Estado AS 'ESTADO', SM.UnidadFalla AS 'FALLA' FROM ReportesApp_Mantenimiento_Solicitud_Registro SM
	LEFT JOIN ReportesApp_Operacion_Previaje_Registros RE WITH(NOLOCK) ON SM.NroTicket = RE.NroTicket
	LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = SM.idTracto
	WHERE (V.NumeroPlaca = @NumeroPlaca)
	ORDER BY SM.idSolicitud DESC
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 09-09-2023
-- Description:	LISTAR SOLICITUD MANTENIMIENTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_Solicitudes_ListarSolicitud]
@idSolicitud INT
AS
BEGIN
	SELECT SM.idSolicitud, V.NumeroPlaca AS 'PLACA', STV.Descripcion AS 'TIPO', SM.UnidadFalla AS 'FALLA', OP.Descripcion AS 'OPERACION', SM.TipoMtto AS 'TIPO_MTTO', SM.TipoTrabajo AS 'TIPO_TRABAJO',
	SM.UbicacionTaller, SM.Kilometraje, CS.Descripcion AS 'CISTERNA', B.DescripcionBase, SD.idSolicitud AS 'IDC', SD.idSolicitudDetalle, SD.idComponente, C1.Descripcion AS 'COMPONENTE',
	SD.idComponenteDetalle, CD.Descripcion AS 'DETALLE', SD.idPosicionLlanta, P.Descripcion AS 'POSICION_LLANTA', SD.Observacion AS 'OBSERVACION', SD.idOT AS 'OT', SD.Estado AS 'ESTADO',
	ISNULL(SM.FechaProg,0) AS 'FechaProg', ISNULL(SM.FechaReprog,0) AS 'FechaReprog', SM.Motivo, ISNULL(SM.FechaEntrega,0) AS 'FechaEntrega', SM.Observacion AS 'OB'
	FROM ReportesApp_Mantenimiento_Solicitud_Registro SM
	LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = SM.idTracto
	LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC WITH(NOLOCK) ON UC.IdUnidad = V.IdVehiculo
	LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = UC.IdProgramacion
	LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_SubTipoVehiculo STV WITH(NOLOCK) ON STV.idSubTipoVehiculo = V.SubTipoVehiculo
	LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Cisterna CS ON CS.idCisterna = SM.idCisterna
	LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Base B ON B.idBase = SM.idBase
	LEFT JOIN ReportesApp_Mantenimiento_Solicitud_RegistroDetalle SD WITH(NOLOCK) ON SD.idSolicitud = SM.idSolicitud
	LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Componente C1 ON C1.idComponente = SD.idComponente
	LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Componente_Detalle CD ON CD.idComponenteDetalle = SD.idComponenteDetalle
	LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Componente_PosicionLlanta P ON P.idPosicionLlanta = SD.idPosicionLlanta
	WHERE (SM.idSolicitud = @idSolicitud)
	ORDER BY SD.idSolicitudDetalle
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 11-09-2023
-- Description:	EDITAR SOLICITUDES DE MANTENIMIENTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_Solicitudes_Modificar]
@idSolicitud INT, 
@Opcion INT,
@Usuario VARCHAR(50)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Modificada correctamente.'

IF (@idSolicitud = 0)
BEGIN
	SET @Exito = '-1 = La fila seleccionado no existe.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN  -- ELIMINAR
		DELETE FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle
		WHERE idSolicitud = @idSolicitud

		DELETE FROM ReportesApp_Mantenimiento_Solicitud_Registro
		WHERE idSolicitud = @idSolicitud 

		DELETE FROM ReportesApp_Mantenimiento_Solicitud_PedidoRepuesto
		WHERE idSolicitud = @idSolicitud
	END
	
	/*
	IF (@Opcion = 2) BEGIN  -- RECEPCIONAR UNIDAD
		DECLARE @FechaEstimada DATETIME
		SET @FechaEstimada = (SELECT FechaEstimada FROM ReportesApp_Mantenimiento_Solicitud_Registro WHERE idSolicitud = @idSolicitud)
		
		UPDATE ReportesApp_Mantenimiento_Solicitud_Registro
		SET FechaRecepcion = ISNULL(@FechaEstimada,GETDATE()), Estado = 'RECEPCIONADO', UltimoUsuario = @Usuario, UltimaModificacion = GETDATE()
		WHERE idSolicitud = @idSolicitud 

		SET @Exito = '0 = Unidad recepcionada correctamente.'
	END
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
-- Create date: 14-09-2023
-- Description:	PROGRAMAR Y REPROGRAMAR SOLICITUDES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_Solicitudes_Programar]
@Opcion INT,
@idSolicitud INT, 
@FechaProgramacion DATETIME,
@Motivo VARCHAR(500),
@Usuario VARCHAR(50)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Programada correctamente.'

IF (@idSolicitud = 0)
BEGIN
	SET @Exito = '-1 = La fila seleccionado no existe.'
	GOTO Terminar
END

/*
IF (@Opcion = 1 AND @FechaProgramacion < GETDATE())
BEGIN
	SET @Exito = '-2 = No puede ingresar fechas que ya pasaron.'
	GOTO Terminar
END

IF (@Opcion = 1 AND @FechaProgramacion < (SELECT ISNULL(FechaLlegadaP,0) FROM ReportesApp_Mantenimiento_Solicitud_PedidoRepuesto WHERE idSolicitud = @idSolicitud))
BEGIN
	SET @Exito = '-2 = No puede ingresar una fecha menor a la programada.'
	GOTO Terminar
END

IF (@Opcion = 2 AND @FechaProgramacion < (SELECT FechaProg FROM ReportesApp_Mantenimiento_Solicitud_Registro WHERE idSolicitud = @idSolicitud))
BEGIN
	SET @Exito = '-2 = No puede ingresar una fecha menor a la programada.'
	GOTO Terminar
END

IF (@Opcion = 3 AND ((@FechaProgramacion < (SELECT FechaProg FROM ReportesApp_Mantenimiento_Solicitud_Registro WHERE idSolicitud = @idSolicitud)) OR
					(@FechaProgramacion < (SELECT FechaReprog FROM ReportesApp_Mantenimiento_Solicitud_Registro WHERE idSolicitud = @idSolicitud))))
BEGIN
	SET @Exito = '-2 = No puede ingresar una fecha menor a la programada.'
	GOTO Terminar
END
*/

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN   -- PROGRAMAR FECHA
		UPDATE ReportesApp_Mantenimiento_Solicitud_Registro
		SET FechaProg = @FechaProgramacion, UltimoUsuario = @Usuario, Estado = 'PROGRAMADO', UltimaModificacion = GETDATE()
		WHERE idSolicitud = @idSolicitud 
	END

	IF (@Opcion = 2) BEGIN   -- REPROGRAMAR FECHA
		UPDATE ReportesApp_Mantenimiento_Solicitud_Registro
		SET FechaReprog = @FechaProgramacion, Motivo = @Motivo, UltimoUsuario = @Usuario, Estado = 'REPROGRAMADO', UltimaModificacion = GETDATE()
		WHERE idSolicitud = @idSolicitud 
		SET @Exito = '0 = Reprogramada correctamente.'
	END

	IF (@Opcion = 3) BEGIN  -- TERMINAR MANTENIMIENTO
		/*
		UPDATE ReportesApp_Mantenimiento_Solicitud_RegistroDetalle
		SET Estado = 'TERMINADO'
		WHERE idSolicitud = @idSolicitud
		*/

		UPDATE ReportesApp_Mantenimiento_Solicitud_Registro
		SET FechaEntrega = @FechaProgramacion, Observacion = @Motivo, UltimoUsuario = @Usuario, Estado = 'COMPLETADO', UltimaModificacion = GETDATE()
		WHERE idSolicitud = @idSolicitud 
		SET @Exito = '0 = Mantenimiento Completo.'

		DECLARE @Mensaje AS VARCHAR(MAX), @Asunto VARCHAR(300)
		DECLARE @CorreoTerminado VARCHAR(MAX) = ''

		SELECT @CorreoTerminado = @CorreoTerminado + '<tr>'
									   + '<td style="background-color: #EEE8AA">' + SM.CodEstado + '-' + CONVERT(VARCHAR,SM.idSolicitud) + '</td>'
									   + '<td style="background-color: #EEE8AA">' + OP.Descripcion + '</td>'
									   + '<td style="background-color: #EEE8AA">' + V.NumeroPlaca + '</td>'
									   + '<td style="background-color: #EEE8AA">' + STV.Descripcion + '</td>'
									   + '<td style="background-color: #EEE8AA">' + CONVERT(VARCHAR,SM.Kilometraje) + '</td>'
									   + '<td style="background-color: #EEE8AA">' + B.DescripcionBase + '</td>'
									   + '<td style="background-color: #EEE8AA">' + CONVERT(VARCHAR,SM.FechaCreacion,103) +' '+CONVERT(VARCHAR,SM.FechaCreacion,24) + '</td>'
									   + '<td style="background-color: #EEE8AA">' + CONVERT(VARCHAR,SM.FechaEntrega,103) +' '+CONVERT(VARCHAR,SM.FechaEntrega,24) + '</td>'
									   + '<td style="background-color: #EEE8AA">' + C.Descripcion + '</td>'
									   + '<td style="background-color: #EEE8AA">' + CD.Descripcion + '</td>'
									   + '<td style="background-color: #EEE8AA">' + P.Descripcion + '</td>'
									   + '<td style="background-color: #EEE8AA">' + SD.Observacion + '</td>'
									   + '<td style="background-color: #EEE8AA">' + SM.Observacion + '</td>'
									   + '<td style="background-color: #EEE8AA">' + SM.Estado + '</td>'
									   + '</tr>'
		FROM ReportesApp_Mantenimiento_Solicitud_Registro SM
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = SM.idTracto
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC WITH(NOLOCK) ON UC.IdUnidad = V.IdVehiculo
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = UC.IdProgramacion
		LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_SubTipoVehiculo STV WITH(NOLOCK) ON STV.idSubTipoVehiculo = V.SubTipoVehiculo
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Base B ON B.idBase = SM.idBase
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_RegistroDetalle SD ON SD.idSolicitud = SM.idSolicitud
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Componente C ON C.idComponente = SD.idComponente
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Componente_Detalle CD ON CD.idComponenteDetalle = SD.idComponenteDetalle
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Componente_PosicionLlanta P ON P.idPosicionLlanta = SD.idPosicionLlanta
		WHERE SM.idSolicitud = @idSolicitud

		SET @Asunto = 'SOLICITUD DE MANTENIMIENTO COMPLETADA'

		SET @Mensaje = '<p><h2>AVISO DE SOLICITUD ATENDIDA</h2></p>'
				  +'<p>La solicitud de mantenimiento para esta unidad acaba de ser completada: </p>'
				  + '<p><table border="1" cellspacing="1" cellpadding="1" >
					    <tr>
						    <td align="center" style="background-color:cyan; color: black"><b>CÓDIGO</b></td>
							<td align="center" style="background-color:cyan; color: black"><b>PROGRAMACIÓN</b></td>
							<td align="center" style="background-color:cyan; color: black"><b>PLACA</b></td>
							<td align="center" style="background-color:cyan; color: black"><b>TIPO</b></td>
							<td align="center" style="background-color:cyan; color: black"><b>KILOMETRAJE</b></td>
							<td align="center" style="background-color:cyan; color: black"><b>BASE</b></td>
							<td align="center" style="background-color:cyan; color: black"><b>FECHA_SOLICITADA</b></td>
							<td align="center" style="background-color:cyan; color: black"><b>FECHA_ENTREGADA</b></td>
							<td align="center" style="background-color:cyan; color: black"><b>COMPONENTE</b></td>
							<td align="center" style="background-color:cyan; color: black"><b>DETALLE</b></td>
							<td align="center" style="background-color:cyan; color: black"><b>POSICION_LLANTA</b></td>
							<td align="center" style="background-color:cyan; color: black"><b>SOLICITUD</b></td>
							<td align="center" style="background-color:cyan; color: black"><b>OBSERVACIÓN</b></td>
							<td align="center" style="background-color:cyan; color: black"><b>ESTADO</b></td>
						</tr>
						<tbody>'
						+ ISNULL(@CorreoTerminado, '') 	
						+'</tbody>
						</table>
					</p>'
				  +'<p>Fecha de envío: '+CONVERT(CHAR(10),GETDATE(),103)+RIGHT(RTRIM(CONVERT(CHAR(26),GETDATE(),22)),12)+'</p>'
				  +'<BR>'
				  +'<p><font face="verdana" size="2" color="DarkRed">Tecnologías de la Información - GRUPO TRANSPESA</font></p>'

		EXEC msdb.dbo.sp_send_dbmail 
			 @profile_name='AVISODESISTEMA',
			 @recipients = 'ccombustible@transpesa.com.pe;combustible@transpesa.com.pe;mantenimiento2@transpesa.com.pe;gerenciaoperaciones@transpesa.com.pe',
			 --@blind_copy_recipients = 'asistenteauditoria@transpesa.com.pe;jrojas@transpesa.com.pe;desarrollo@transpesa.com.pe;desarrollo2@transpesa.com.pe;' 
			 @subject = @Asunto,
			 @body_format = 'HTML',
			 @body = @Mensaje	
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
-- Create date: 16-09-2023
-- Description:	INSERTAR PEDIDOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_Solicitudes_Pedidos]
@Opcion INT,
@idSolicitud INT,
@Requerimiento VARCHAR(20),
@Descripcion VARCHAR(500),
@FechaLlegadaP DATETIME
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

SET @Exito = '0 = Pedido registrado.'

IF (@Opcion = 3 AND @FechaLlegadaP < (SELECT FechaPedido FROM ReportesApp_Mantenimiento_Solicitud_PedidoRepuesto WHERE idSolicitud = @idSolicitud))
BEGIN
	SET @Exito = '-2 = No puede ingresar una fecha menor a la registrada.'
	GOTO Terminar
END

IF (@Opcion = 1 AND EXISTS(SELECT * FROM ReportesApp_Mantenimiento_Solicitud_PedidoRepuesto WHERE idSolicitud = @idSolicitud))
BEGIN
	SET @Exito = '-1 = Esta solicitud ya tiene un requerimiento anexado.'
	GOTO Terminar
END

SET @correlativo = (SELECT MAX(idPedido) FROM ReportesApp_Mantenimiento_Solicitud_PedidoRepuesto)
SET @correlativo = ISNULL(@correlativo, 0) + 1

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN  -- INSERTAR PEDIDO
		INSERT INTO ReportesApp_Mantenimiento_Solicitud_PedidoRepuesto(idPedido, idSolicitud, Requerimiento, Estado, FechaPedido, Descripcion)
		VALUES(@correlativo, @idSolicitud, @Requerimiento, 'PEDIDO', @FechaLlegadaP, @Descripcion)
	END

	IF (@Opcion = 4) BEGIN  -- ELIMINAR PEDIDO
		DELETE FROM ReportesApp_Mantenimiento_Solicitud_PedidoRepuesto
		WHERE idSolicitud = @idSolicitud

		SET @Exito = '0 = Pedido eliminado.'
	END

	IF (@Opcion = 5) BEGIN	-- LISTAR REQUERIMIENTOS
		SELECT TOP(25) LTRIM(RTRIM(RequisicionNumero)) AS 'Nro', LTRIM(RTRIM(Comentarios)) AS 'REPUESTO',		CONVERT(VARCHAR,FechaPreparacion,103)+' '+CONVERT(VARCHAR,FechaPreparacion,8) AS 'FECHA' FROM WH_Requisiciones		WHERE Clasificacion NOT IN ('SER') AND Departamento IN ('MAN','NEU') AND Estado IN ('PR','AP') AND (RequisicionNumero LIKE '%' + @Requerimiento + '%')
		ORDER BY FechaPreparacion DESC
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
-- Create date: 16-09-2023
-- Description:	LISTAR PEDIDO REPUESTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_Solicitudes_ListarPedido]
@idSolicitud INT
AS
BEGIN
	SELECT SM.idSolicitud, P.Requerimiento, P.Descripcion,
	CONVERT(VARCHAR,P.FechaPedido,103)+' '+CONVERT(VARCHAR,P.FechaPedido,8) AS 'FechaPedido', ISNULL(P.FechaLlegadaP,0) AS 'FechaLlegadaP'
	FROM ReportesApp_Mantenimiento_Solicitud_PedidoRepuesto P
	LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Registro SM WITH(NOLOCK) ON P.idSolicitud = SM.idSolicitud
	WHERE (P.idSolicitud = @idSolicitud)
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 06-06-2024
-- Description:	BUSCAR PEDIDO REPUESTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_Solicitudes_BuscarNotaIngreso]
@Opcion INT,
@idSolicitud INT,
@Requerimiento VARCHAR(20)
AS
BEGIN
	DECLARE @FechaLlegada DATETIME

	SELECT TOP(1) @FechaLlegada = TH.FechaDocumento 
	FROM WH_TransaccionHeader TH
	INNER JOIN WH_TransaccionDetalle TD ON TH.CompaniaSocio=TD.CompaniaSocio AND TH.TipoDocumento=TD.TipoDocumento AND TH.NumeroDocumento=TD.NumeroDocumento
	INNER JOIN WH_OrdenCompraRequisicion OC ON OC.NumeroOrden=TD.ReferenciaNumeroDocumento AND OC.RequisicionCompaniaSocio='10000000'
	INNER JOIN WH_OrdenCompra ORDEN ON orden.CompaniaSocio=oc.CompaniaSocio and ORDEN.NumeroOrden=OC.NumeroOrden 
	WHERE TD.TipoDocumento='NI' AND TH.Estado<>'AN' AND ORDEN.Estado<>'AN' AND LTRIM(RTRIM(OC.RequisicionNumero)) = @Requerimiento

	IF (@FechaLlegada IS NOT NULL) BEGIN
		UPDATE ReportesApp_Mantenimiento_Solicitud_PedidoRepuesto
		SET FechaLlegadaP = @FechaLlegada, Estado = 'ENTREGADO'
		WHERE idSolicitud = @idSolicitud

		IF (@Opcion = 1) BEGIN
			SELECT CONVERT(VARCHAR,@FechaLlegada,103)+' '+CONVERT(VARCHAR,@FechaLlegada,8) AS 'FechaLlegadaP'
		END
	END
END

/*
SELECT * FROM ReportesApp_Mantenimiento_Solicitud_PedidoRepuesto

UPDATE ReportesApp_Mantenimiento_Solicitud_PedidoRepuesto
SET Requerimiento = '0000218643', FechaLlegadaP = NULL, Estado = 'PEDIDO'
*/
-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 22-09-2023
-- Description:	INSERTAR NUEVO AUXILIO
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_InsertarSistema]
@NuevoSistema VARCHAR(100)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

SET @Exito = '0 = Solicitud añadida.'

IF (EXISTS(SELECT Descripcion FROM ReportesApp_Mantenimiento_FallasMecanicas_SistemaVehiculo WHERE Descripcion = @NuevoSistema)) BEGIN
	SET @Exito = '-1 = Este sistema ya fue registrado.'
	GOTO Terminar
END

SET @correlativo = (SELECT MAX(idSistemaVehiculo) FROM ReportesApp_Mantenimiento_FallasMecanicas_SistemaVehiculo)
SET @correlativo = ISNULL(@correlativo, 0) + 1

BEGIN TRAN
BEGIN TRY
	INSERT INTO ReportesApp_Mantenimiento_FallasMecanicas_SistemaVehiculo(idSistemaVehiculo, Descripcion)
	VALUES(@correlativo, @NuevoSistema)
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

-----------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 29-11-2023
-- Description:	INSERTAR FECHA ESTIMADA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_Solicitudes_InsertarFechaEstimada]
@OpcionFecha INT,
@idSolicitud INT,
@FechaEstimada DATETIME,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@OpcionFecha = 1) BEGIN		-- INGRESAR FECHA ESTIMADA
		UPDATE ReportesApp_Mantenimiento_Solicitud_Registro
		SET FechaEstimada = @FechaEstimada
		WHERE idSolicitud = @idSolicitud

		IF ((SELECT FechaEstimada FROM ReportesApp_Mantenimiento_Solicitud_Registro WHERE idSolicitud = @idSolicitud) IS NOT NULL) BEGIN
			SET @Exito = '0 = Fecha de Estimación Actualizada.'
		END
		ELSE BEGIN
			SET @Exito = '0 = Fecha de Estimación Registrada.'
		END
	END
	
	IF (@OpcionFecha = 2) BEGIN		-- INGRESAR FECHA DE RECEPCION
		DECLARE @TractoAnterior INT = (SELECT idTracto FROM ReportesApp_Mantenimiento_Solicitud_Registro WHERE idSolicitud = @idSolicitud)

		IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_Solicitud_Registro WHERE Estado = 'RECEPCIONADO' AND idTracto = @TractoAnterior)) BEGIN
			SET @Exito = '-1 = Esta unidad ya se encuentra recepcionada en otra solicitud.'
			ROLLBACK
			GOTO Terminar
		END
		ELSE BEGIN
			UPDATE ReportesApp_Mantenimiento_Solicitud_Registro
			SET FechaRecepcion = @FechaEstimada, Estado = 'RECEPCIONADO', UltimoUsuario = @Usuario, UltimaModificacion = GETDATE()
			WHERE idSolicitud = @idSolicitud

			IF ((SELECT FechaRecepcion FROM ReportesApp_Mantenimiento_Solicitud_Registro WHERE idSolicitud = @idSolicitud) IS NOT NULL) BEGIN
				SET @Exito = '0 = Fecha de Recepción Actualizada.'
			END
			ELSE BEGIN
				SET @Exito = '0 = Fecha de Recepción Registrada.'
			END
		END
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

---------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 06-06-2025
-- Description:	INSERTAR FECHA DE RECEPCIÓN
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_Solicitudes_InsertarFechaRecepcion]
@idSolicitud INT,
@FechaRecepcion DATETIME,
@idBase INT,
@UbicacionTaller VARCHAR(100),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	DECLARE @TractoAnterior INT = (SELECT idTracto FROM ReportesApp_Mantenimiento_Solicitud_Registro WHERE idSolicitud = @idSolicitud)

	IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_Solicitud_Registro WHERE Estado = 'RECEPCIONADO' AND idTracto = @TractoAnterior AND YEAR(FechaRecepcion) >= 2024)) BEGIN
		SET @Exito = '-1 = Esta unidad ya se encuentra recepcionada en otra solicitud.'
		ROLLBACK
		GOTO Terminar
	END

	IF ((@idBase IN (1,8)) AND (@UbicacionTaller IS NULL OR @UbicacionTaller = '')) BEGIN
		SET @Exito = '-2 = Por favor, ingrese la ubicación del taller.'
		ROLLBACK
		GOTO Terminar
	END
	ELSE BEGIN
		UPDATE ReportesApp_Mantenimiento_Solicitud_Registro
		SET FechaRecepcion = @FechaRecepcion, Estado = 'RECEPCIONADO', UltimoUsuario = @Usuario, UltimaModificacion = GETDATE(),
			idBase = @idBase, UbicacionTaller = @UbicacionTaller
		WHERE idSolicitud = @idSolicitud

		IF ((SELECT FechaRecepcion FROM ReportesApp_Mantenimiento_Solicitud_Registro WHERE idSolicitud = @idSolicitud) IS NOT NULL) BEGIN
			SET @Exito = '0 = Fecha de Recepción Actualizada.'
		END
		ELSE BEGIN
			SET @Exito = '0 = Fecha de Recepción Registrada.'
		END
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

---------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 07-12-2023
-- Description:	LISTAR COMPONENTES DEFECTUOSOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_SolicitudDetalle_ListarDefectuosos]
@NumeroPlaca VARCHAR(20),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@EstadoFalla VARCHAR(25)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@EstadoFalla = ' ') BEGIN
		SELECT ISNULL(SM.idSolicitud,'') AS idSolicitud, SM.CodEstado + '-' + CONVERT(VARCHAR,SM.idSolicitud) AS 'CODIGO', SD.idSolicitudDetalle,
		OP.Descripcion AS 'PROGRAMACION', V.NumeroPlaca AS 'PLACA', SM.UnidadFalla AS 'FALLA', SD.Estado AS 'ESTADO', C.Descripcion AS 'COMPONENTE',
		CD.Descripcion AS 'DETALLE', P.Descripcion AS 'POSICION_LLANTA', SD.Observacion AS 'OBSERVACION', ISNULL(SD.idOT,'-') AS 'OT', SM.Estado AS 'ESTADO_SOLICITUD'
		FROM ReportesApp_Mantenimiento_Solicitud_Registro SM
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = SM.idTracto
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC WITH(NOLOCK) ON UC.IdUnidad = V.IdVehiculo
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = UC.IdProgramacion
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_RegistroDetalle SD WITH(NOLOCK) ON SD.idSolicitud = SM.idSolicitud
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Componente C ON C.idComponente = SD.idComponente
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Componente_Detalle CD ON CD.idComponenteDetalle = SD.idComponenteDetalle
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Componente_PosicionLlanta P ON P.idPosicionLlanta = SD.idPosicionLlanta
		WHERE ((@NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @NumeroPlaca + '%') AND (SM.FechaCreacion IS NULL OR SM.FechaCreacion BETWEEN @FINICIO AND @FFIN))
		ORDER BY SM.idSolicitud DESC
	END
	ELSE BEGIN
		SELECT ISNULL(SM.idSolicitud,'') AS idSolicitud, SM.CodEstado + '-' + CONVERT(VARCHAR,SM.idSolicitud) AS 'CODIGO', SD.idSolicitudDetalle,
		OP.Descripcion AS 'PROGRAMACION', V.NumeroPlaca AS 'PLACA', SM.UnidadFalla AS 'FALLA', SD.Estado AS 'ESTADO', C.Descripcion AS 'COMPONENTE',
		CD.Descripcion AS 'DETALLE', P.Descripcion AS 'POSICION_LLANTA', SD.Observacion AS 'OBSERVACION', ISNULL(SD.idOT,'-') AS 'OT', SM.Estado AS 'ESTADO_SOLICITUD'
		FROM ReportesApp_Mantenimiento_Solicitud_Registro SM
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = SM.idTracto
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC WITH(NOLOCK) ON UC.IdUnidad = V.IdVehiculo
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = UC.IdProgramacion
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_RegistroDetalle SD WITH(NOLOCK) ON SD.idSolicitud = SM.idSolicitud
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Componente C ON C.idComponente = SD.idComponente
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Componente_Detalle CD ON CD.idComponenteDetalle = SD.idComponenteDetalle
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Componente_PosicionLlanta P ON P.idPosicionLlanta = SD.idPosicionLlanta
		WHERE ((@NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @NumeroPlaca + '%') AND (SM.FechaCreacion IS NULL OR SM.FechaCreacion BETWEEN @FINICIO AND @FFIN)
		AND (@EstadoFalla IS NULL OR SD.Estado LIKE '%' + @EstadoFalla + '%'))
		ORDER BY SM.idSolicitud DESC
	END
END

---------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 08-01-2024
-- Description:	MODIFICAR DETALLE SOLICITUD
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_Solicitudes_SolicitudDetalle_Modificar]
@idSolicitud INT,
@idSolicitudDetalle INT,
@idComponente INT,
@idComponenteDetalle INT,
@idPosicionLlanta INT,
@Observacion VARCHAR(300)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

SET @Exito = '0 = Solicitud Modificada.'

BEGIN TRAN
BEGIN TRY
	UPDATE ReportesApp_Mantenimiento_Solicitud_RegistroDetalle
	SET idComponente = @idComponente, idComponenteDetalle = @idComponenteDetalle, idPosicionLlanta = @idPosicionLlanta, Observacion = @Observacion
	WHERE idSolicitudDetalle = @idSolicitudDetalle AND idSolicitud = @idSolicitud 
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
---------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 24-04-2023
-- Description:	LISTAR ESTADO DE UNIDADES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_Solicitudes_ListarEstado]
@NumeroPlaca VARCHAR(20),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@Ubicacion INT,
@Estado VARCHAR(40),
@EstadoProg VARCHAR(30)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	DECLARE @Contador INT = 1
	WHILE (@Contador <= (SELECT MAX(idPedido) FROM ReportesApp_Mantenimiento_Solicitud_PedidoRepuesto)) BEGIN
		DECLARE @idSolicitud INT = (SELECT ISNULL(idSolicitud,0) FROM ReportesApp_Mantenimiento_Solicitud_PedidoRepuesto WHERE idPedido = @Contador)
		DECLARE @Requerimiento VARCHAR(20) = (SELECT Requerimiento FROM ReportesApp_Mantenimiento_Solicitud_PedidoRepuesto WHERE idPedido = @Contador)
		
		EXEC ReportesApp_Mantenimiento_Solicitudes_BuscarNotaIngreso 2, @idSolicitud, @Requerimiento

		SET @Contador = @Contador + 1
	END

	IF (@Estado = 'TODOS') BEGIN
		IF (@EstadoProg = 'TODOS') BEGIN
			SELECT SM.idSolicitud, STV.Descripcion AS 'TIPO', V.NumeroPlaca AS 'PLACA', (CASE WHEN EXISTS(SELECT * FROM ReportesApp_Operacion_Previaje_Registros
			WHERE CONVERT(DATE,FechaProgramacion) = CONVERT(DATE,GETDATE()) AND (idTracto = V.IdVehiculo OR idSemirremolque = V.IdVehiculo) AND (Estado = 1) AND (TipoProgramacion IN (1,2,3,4)))
			THEN 'PROGRAMADO' ELSE 'NO PROGRAMADO' END) AS 'PROGRAMACION', (SELECT TOP(1) Destino FROM ReportesApp_Operacion_Previaje_Registros WHERE CONVERT(DATE,FechaProgramacion) = CONVERT(DATE,GETDATE())
			AND (Estado = 1) AND (TipoProgramacion IN (1,2,3,4)) AND (idTracto = V.IdVehiculo OR idSemirremolque = V.IdVehiculo)) AS 'DESTINO', B.DescripcionBase AS 'UBICACION', (SELECT TOP(1) Observacion
			FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle WHERE idSolicitud = SM.idSolicitud) AS 'MOTIVO', CASE WHEN ((SELECT Estado FROM ReportesApp_Mantenimiento_Solicitud_PedidoRepuesto
			WHERE idSolicitud = SM.idSolicitud) = 'PEDIDO') THEN 'LOG' ELSE 'MTTO' END AS 'RESPONSABLE', CONVERT(VARCHAR,SM.FechaRecepcion,103) AS 'FECHA_INICIO',
			CASE WHEN SM.Estado = 'REPROGRAMADO' THEN CONVERT(VARCHAR,SM.FechaReprog,103) ELSE CONVERT(VARCHAR,SM.FechaProg,103) END AS 'FECHA_PROYECTADA',
			DATEDIFF(DAY,SM.FechaRecepcion,GETDATE()) AS 'DÍAS', P.Descripcion AS 'PEDIDO', CONVERT(VARCHAR,P.FechaPedido,103) + ' ' + CONVERT(VARCHAR,P.FechaPedido,8) AS 'FECHA_SOLICITADA', 
			CONVERT(VARCHAR,P.FechaLlegadaP,103) + ' ' + CONVERT(VARCHAR,P.FechaLlegadaP,8) AS 'FECHA_LLEGADA', P.Estado AS 'ESTADO',
			DATEDIFF(DAY,ISNULL(P.FechaPedido,GETDATE()),ISNULL(P.FechaLlegadaP,GETDATE())) AS 'DÍAS_PEDIDO'
			FROM ReportesApp_Mantenimiento_Solicitud_Registro SM
			LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = SM.idTracto
			LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_SubTipoVehiculo STV WITH(NOLOCK) ON STV.idSubTipoVehiculo = V.SubTipoVehiculo
			LEFT JOIN ReportesApp_Mantenimiento_Solicitud_PedidoRepuesto P WITH(NOLOCK) ON P.idSolicitud = SM.idSolicitud
			LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Base B WITH(NOLOCK) ON B.idBase = SM.idBase
			WHERE (@NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @NumeroPlaca + '%') AND (SM.FechaRecepcion BETWEEN @FINICIO AND @FFIN)
			AND (SM.Estado IN ('RECEPCIONADO','PROGRAMADO','REPROGRAMADO') AND (@Ubicacion = 0 OR SM.idBase = @Ubicacion))
		END
		ELSE BEGIN
			SELECT SM.idSolicitud, STV.Descripcion AS 'TIPO', V.NumeroPlaca AS 'PLACA', (CASE WHEN EXISTS(SELECT * FROM ReportesApp_Operacion_Previaje_Registros
			WHERE CONVERT(DATE,FechaProgramacion) = CONVERT(DATE,GETDATE()) AND (idTracto = V.IdVehiculo OR idSemirremolque = V.IdVehiculo) AND (Estado = 1) AND (TipoProgramacion IN (1,2,3,4)))
			THEN 'PROGRAMADO' ELSE 'NO PROGRAMADO' END) AS 'PROGRAMACION', (SELECT TOP(1) Destino FROM ReportesApp_Operacion_Previaje_Registros WHERE CONVERT(DATE,FechaProgramacion) = CONVERT(DATE,GETDATE())
			AND (Estado = 1) AND (TipoProgramacion IN (1,2,3,4)) AND (idTracto = V.IdVehiculo OR idSemirremolque = V.IdVehiculo)) AS 'DESTINO', B.DescripcionBase AS 'UBICACION', (SELECT TOP(1) Observacion
			FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle WHERE idSolicitud = SM.idSolicitud) AS 'MOTIVO', CASE WHEN ((SELECT Estado FROM ReportesApp_Mantenimiento_Solicitud_PedidoRepuesto
			WHERE idSolicitud = SM.idSolicitud) = 'PEDIDO') THEN 'LOG' ELSE 'MTTO' END AS 'RESPONSABLE', CONVERT(VARCHAR,SM.FechaRecepcion,103) AS 'FECHA_INICIO',
			CASE WHEN SM.Estado = 'REPROGRAMADO' THEN CONVERT(VARCHAR,SM.FechaReprog,103) ELSE CONVERT(VARCHAR,SM.FechaProg,103) END AS 'FECHA_PROYECTADA',
			DATEDIFF(DAY,SM.FechaRecepcion,GETDATE()) AS 'DÍAS', P.Descripcion AS 'PEDIDO', CONVERT(VARCHAR,P.FechaPedido,103) + ' ' + CONVERT(VARCHAR,P.FechaPedido,8) AS 'FECHA_SOLICITADA',
			CONVERT(VARCHAR,P.FechaLlegadaP,103) + ' ' + CONVERT(VARCHAR,P.FechaLlegadaP,8) AS 'FECHA_LLEGADA', P.Estado AS 'ESTADO',
			DATEDIFF(DAY,ISNULL(P.FechaPedido,GETDATE()),ISNULL(P.FechaLlegadaP,GETDATE())) AS 'DÍAS_PEDIDO'
			FROM ReportesApp_Mantenimiento_Solicitud_Registro SM
			LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = SM.idTracto
			LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_SubTipoVehiculo STV WITH(NOLOCK) ON STV.idSubTipoVehiculo = V.SubTipoVehiculo
			LEFT JOIN ReportesApp_Mantenimiento_Solicitud_PedidoRepuesto P WITH(NOLOCK) ON P.idSolicitud = SM.idSolicitud
			LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Base B WITH(NOLOCK) ON B.idBase = SM.idBase
			WHERE (@NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @NumeroPlaca + '%') AND (SM.FechaRecepcion BETWEEN @FINICIO AND @FFIN)
			AND (SM.Estado IN ('RECEPCIONADO','PROGRAMADO','REPROGRAMADO') AND (@Ubicacion = 0 OR SM.idBase = @Ubicacion)) AND (CASE WHEN EXISTS(SELECT * FROM ReportesApp_Operacion_Previaje_Registros
			WHERE CONVERT(DATE,FechaProgramacion) = CONVERT(DATE,GETDATE()) AND (idTracto = V.IdVehiculo OR idSemirremolque = V.IdVehiculo) AND (Estado = 1) AND (TipoProgramacion IN (1,2,3,4)))
			THEN 'PROGRAMADO' ELSE 'NO PROGRAMADO' END) = @EstadoProg
		END
	END
	ELSE BEGIN
		IF (@EstadoProg = 'TODOS') BEGIN
			SELECT SM.idSolicitud, STV.Descripcion AS 'TIPO', V.NumeroPlaca AS 'PLACA', (CASE WHEN EXISTS(SELECT * FROM ReportesApp_Operacion_Previaje_Registros
			WHERE CONVERT(DATE,FechaProgramacion) = CONVERT(DATE,GETDATE()) AND (idTracto = V.IdVehiculo OR idSemirremolque = V.IdVehiculo) AND (Estado = 1) AND (TipoProgramacion IN (1,2,3,4)))
			THEN 'PROGRAMADO' ELSE 'NO PROGRAMADO' END) AS 'PROGRAMACION', (SELECT TOP(1) Destino FROM ReportesApp_Operacion_Previaje_Registros WHERE CONVERT(DATE,FechaProgramacion) = CONVERT(DATE,GETDATE())
			AND (Estado = 1) AND (TipoProgramacion IN (1,2,3,4)) AND (idTracto = V.IdVehiculo OR idSemirremolque = V.IdVehiculo)) AS 'DESTINO', B.DescripcionBase AS 'UBICACION', (SELECT TOP(1) Observacion
			FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle WHERE idSolicitud = SM.idSolicitud) AS 'MOTIVO', CASE WHEN ((SELECT Estado FROM ReportesApp_Mantenimiento_Solicitud_PedidoRepuesto
			WHERE idSolicitud = SM.idSolicitud) = 'PEDIDO') THEN 'LOG' ELSE 'MTTO' END AS 'RESPONSABLE', CONVERT(VARCHAR,SM.FechaRecepcion,103) AS 'FECHA_INICIO',
			CASE WHEN SM.Estado = 'REPROGRAMADO' THEN CONVERT(VARCHAR,SM.FechaReprog,103) ELSE CONVERT(VARCHAR,SM.FechaProg,103) END AS 'FECHA_PROYECTADA',
			DATEDIFF(DAY,SM.FechaRecepcion,GETDATE()) AS 'DÍAS', P.Descripcion AS 'PEDIDO', CONVERT(VARCHAR,P.FechaPedido,103) + ' ' + CONVERT(VARCHAR,P.FechaPedido,8) AS 'FECHA_SOLICITADA',
			CONVERT(VARCHAR,P.FechaLlegadaP,103) + ' ' + CONVERT(VARCHAR,P.FechaLlegadaP,8) AS 'FECHA_LLEGADA', P.Estado AS 'ESTADO',
			DATEDIFF(DAY,ISNULL(P.FechaPedido,GETDATE()),ISNULL(P.FechaLlegadaP,GETDATE())) AS 'DÍAS_PEDIDO'
			FROM ReportesApp_Mantenimiento_Solicitud_Registro SM
			LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = SM.idTracto
			LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_SubTipoVehiculo STV WITH(NOLOCK) ON STV.idSubTipoVehiculo = V.SubTipoVehiculo
			LEFT JOIN ReportesApp_Mantenimiento_Solicitud_PedidoRepuesto P WITH(NOLOCK) ON P.idSolicitud = SM.idSolicitud
			LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Base B WITH(NOLOCK) ON B.idBase = SM.idBase
			WHERE (@NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @NumeroPlaca + '%') AND (SM.FechaRecepcion BETWEEN @FINICIO AND @FFIN)
			AND (SM.Estado IN ('RECEPCIONADO','PROGRAMADO','REPROGRAMADO') AND (@Ubicacion = 0 OR SM.idBase = @Ubicacion)) AND (P.Estado = @Estado)
		END
		ELSE BEGIN
			SELECT SM.idSolicitud, STV.Descripcion AS 'TIPO', V.NumeroPlaca AS 'PLACA', (CASE WHEN EXISTS(SELECT * FROM ReportesApp_Operacion_Previaje_Registros
			WHERE CONVERT(DATE,FechaProgramacion) = CONVERT(DATE,GETDATE()) AND (idTracto = V.IdVehiculo OR idSemirremolque = V.IdVehiculo) AND (Estado = 1) AND (TipoProgramacion IN (1,2,3,4)))
			THEN 'PROGRAMADO' ELSE 'NO PROGRAMADO' END) AS 'PROGRAMACION', (SELECT TOP(1) Destino FROM ReportesApp_Operacion_Previaje_Registros WHERE CONVERT(DATE,FechaProgramacion) = CONVERT(DATE,GETDATE())
			AND (Estado = 1) AND (TipoProgramacion IN (1,2,3,4)) AND (idTracto = V.IdVehiculo OR idSemirremolque = V.IdVehiculo)) AS 'DESTINO', B.DescripcionBase AS 'UBICACION', (SELECT TOP(1) Observacion
			FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle WHERE idSolicitud = SM.idSolicitud) AS 'MOTIVO', CASE WHEN ((SELECT Estado FROM ReportesApp_Mantenimiento_Solicitud_PedidoRepuesto
			WHERE idSolicitud = SM.idSolicitud) = 'PEDIDO') THEN 'LOG' ELSE 'MTTO' END AS 'RESPONSABLE', CONVERT(VARCHAR,SM.FechaRecepcion,103) AS 'FECHA_INICIO',
			CASE WHEN SM.Estado = 'REPROGRAMADO' THEN CONVERT(VARCHAR,SM.FechaReprog,103) ELSE CONVERT(VARCHAR,SM.FechaProg,103) END AS 'FECHA_PROYECTADA',
			DATEDIFF(DAY,SM.FechaRecepcion,GETDATE()) AS 'DÍAS', P.Descripcion AS 'PEDIDO', CONVERT(VARCHAR,P.FechaPedido,103) + ' ' + CONVERT(VARCHAR,P.FechaPedido,8) AS 'FECHA_SOLICITADA',
			CONVERT(VARCHAR,P.FechaLlegadaP,103) + ' ' + CONVERT(VARCHAR,P.FechaLlegadaP,8) AS 'FECHA_LLEGADA', P.Estado AS 'ESTADO',
			DATEDIFF(DAY,ISNULL(P.FechaPedido,GETDATE()),ISNULL(P.FechaLlegadaP,GETDATE())) AS 'DÍAS_PEDIDO'
			FROM ReportesApp_Mantenimiento_Solicitud_Registro SM
			LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = SM.idTracto
			LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_SubTipoVehiculo STV WITH(NOLOCK) ON STV.idSubTipoVehiculo = V.SubTipoVehiculo
			LEFT JOIN ReportesApp_Mantenimiento_Solicitud_PedidoRepuesto P WITH(NOLOCK) ON P.idSolicitud = SM.idSolicitud
			LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Base B WITH(NOLOCK) ON B.idBase = SM.idBase
			WHERE (@NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @NumeroPlaca + '%') AND (SM.FechaRecepcion BETWEEN @FINICIO AND @FFIN)
			AND (SM.Estado IN ('RECEPCIONADO','PROGRAMADO','REPROGRAMADO') AND (@Ubicacion = 0 OR SM.idBase = @Ubicacion)) AND (P.Estado = @Estado) AND (CASE WHEN EXISTS(SELECT * FROM ReportesApp_Operacion_Previaje_Registros
			WHERE CONVERT(DATE,FechaProgramacion) = CONVERT(DATE,GETDATE()) AND (idTracto = V.IdVehiculo OR idSemirremolque = V.IdVehiculo) AND (Estado = 1) AND (TipoProgramacion IN (1,2,3,4)))
			THEN 'PROGRAMADO' ELSE 'NO PROGRAMADO' END) = @EstadoProg
		END
	END
END

/*
UPDATE ReportesApp_Mantenimiento_Solicitud_PedidoRepuesto
SET Estado = 'PEDIDO', FechaLlegadaP = NULL

UPDATE ReportesApp_Mantenimiento_Solicitud_PedidoRepuesto
SET Requerimiento = /*'0000219630'*/ '0000219010'
WHERE idPedido = 2
*/

----------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 17-05-2024
-- Description:	INSERTAR NUEVA UBICACION
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Mantenimiento_Solicitudes_InsertarUbicacion]
@Ubicacion VARCHAR(300)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

SET @Exito = '0 = Ubicación Registrada.'

BEGIN TRAN
BEGIN TRY
	SET @correlativo = (SELECT MAX(idBase) FROM ReportesApp_Mantenimiento_Solicitud_Base)
	SET @correlativo = ISNULL(@correlativo, 0) + 1

	INSERT INTO ReportesApp_Mantenimiento_Solicitud_Base
	SELECT @correlativo, @Ubicacion
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
-- Create date: 31-01-2025
-- Description:	LISTAR TALLERES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_Solicitudes_ListarTalleres]
@Taller VARCHAR(50)
AS
BEGIN
	SELECT TOP(10) idUbicacionTaller, Descripcion AS 'TALLER'
	FROM ReportesApp_Mantenimiento_Solicitud_UbicacionTaller
	WHERE (@Taller IS NULL OR Descripcion LIKE '%' + @Taller + '%')
END

-----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 31-01-2025
-- Description:	BUSCAR TALLERES DISPONIBLES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_Solicitudes_BuscarTalleres]
@Taller VARCHAR(50)
AS
DECLARE @Exito VARCHAR(MAX)
BEGIN
	IF (EXISTS(SELECT TOP(1) * FROM ReportesApp_Mantenimiento_Solicitud_Registro WHERE (UbicacionTaller = @Taller)
	AND (Estado != 'COMPLETADO') ORDER BY idSolicitud DESC)) BEGIN
		DECLARE @FechaCreacion VARCHAR(80) = (SELECT TOP(1) CONVERT(VARCHAR,FechaCreacion,103) FROM ReportesApp_Mantenimiento_Solicitud_Registro
											 WHERE (UbicacionTaller = @Taller) AND (Estado != 'COMPLETADO') ORDER BY idSolicitud DESC)

		SET @Exito = '-1 = Este taller ya ha sido seleccionado en una solicitud registrada el día '+@FechaCreacion+'.' 
	END
	ELSE BEGIN
		SET @Exito = '0 = Taller disponible.'
	END

	SELECT @exito exito
END

--------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 01-02-2025
-- Description:	LISTAR UNIDADES EN TALLER
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_Solicitudes_ListarUnidadTaller]
@NumeroPlaca VARCHAR(20)
AS
BEGIN
	SELECT SM.idSolicitud, SM.UbicacionTaller AS 'TALLER', V.NumeroPlaca AS 'PLACA', STV.Descripcion AS 'TIPO', SM.TipoMtto AS 'TIPO_MTTO',
	OP.Descripcion AS 'PROGRAMACION', CONVERT(VARCHAR,SM.FechaCreacion,103)+' '+CONVERT(VARCHAR,SM.FechaCreacion,8) AS 'FECHA_SOLICITUD'
	FROM ReportesApp_Mantenimiento_Solicitud_Registro SM
	LEFT JOIN ReportesApp_Mantenimiento_Solicitud_UbicacionTaller T ON T.Descripcion = SM.UbicacionTaller
	LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = SM.idTracto
	LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC WITH(NOLOCK) ON UC.IdUnidad = V.IdVehiculo
	LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = UC.IdProgramacion
	LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_SubTipoVehiculo STV WITH(NOLOCK) ON STV.idSubTipoVehiculo = V.SubTipoVehiculo
	WHERE (CONVERT(DATE,SM.FechaCreacion) >= '01/02/2025') AND (SM.Estado != 'COMPLETADO') AND (SM.UbicacionTaller != '')
	AND (@NumeroPlaca IS NULL OR NumeroPlaca LIKE '%' + @NumeroPlaca + '%')
	ORDER BY T.idUbicacionTaller
END

--------------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 03-02-2025
-- Description:	MODIFICAR UBICACION DE TALLER
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_Solicitudes_ModificarUbicacion]
@idSolicitud INT,
@idBase INT,
@Taller VARCHAR(300),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

SET @Exito = '0 = Ubicación Modificada.'

BEGIN TRAN
BEGIN TRY
	DECLARE @AnteriorTaller VARCHAR(300) = (SELECT UbicacionTaller FROM ReportesApp_Mantenimiento_Solicitud_Registro WHERE idSolicitud = @idSolicitud)
	DECLARE @AnteriorUbicacion INT = (SELECT idBase FROM ReportesApp_Mantenimiento_Solicitud_Registro WHERE idSolicitud = @idSolicitud)

	SET @correlativo = (SELECT MAX(idUbicacionHistorial) FROM ReportesApp_Mantenimiento_Solicitud_UbicacionHistorial)
	SET @correlativo = ISNULL(@correlativo, 0) + 1

	INSERT INTO ReportesApp_Mantenimiento_Solicitud_UbicacionHistorial(idUbicacionHistorial,idSolicitud,AnteriorUbicacion,AnteriorTaller,NuevaUbicacion,
	NuevoTaller,UsuarioCreacion,FechaCreacion)
	VALUES (@correlativo, @idSolicitud, @AnteriorUbicacion, @AnteriorTaller, @idBase, @Taller, @Usuario, GETDATE())

	UPDATE ReportesApp_Mantenimiento_Solicitud_Registro
	SET UbicacionTaller = @Taller, idBase = @idBase
	WHERE idSolicitud = @idSolicitud
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