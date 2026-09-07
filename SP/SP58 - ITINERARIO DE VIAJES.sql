
-- CREAR TABLA ReportesApp_Operaciones_ItinerarioViajes_PuntoParada

-- CREAR TABLA ReportesApp_Operaciones_ItinerarioViajes_Paradas

-- CREAR TABLA ReportesApp_Operaciones_ItinerarioViajes_TiempoViaje

-- CREAR TABLA ReportesApp_Operaciones_ItinerarioViajes_Consolidado

-- CREAR TABLA ReportesApp_Operaciones_ItinerarioViajes_Consolidado_Detalle

------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 27-09-2024
-- Description:	LISTAR PUNTOS PARADA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ItinerarioViajes_ListarPuntosParada]
@Opcion INT
AS
BEGIN
	IF (@Opcion = 1) BEGIN  -- LISTAR PUNTOS PARADA
		SELECT idPuntoParada, Descripcion FROM ReportesApp_Operaciones_ItinerarioViajes_PuntoParada
	END
END

--------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 27-09-2024
-- Description: REGISTRAR PUNTO PARADA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ItinerarioViajes_RegistrarPuntoParada]
@Descripcion VARCHAR(250)
AS
DECLARE @Contador INT 
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ItinerarioViajes_PuntoParada WHERE Descripcion = @Descripcion)) BEGIN
		SET @Exito = '-1 = Esta parada ya ha sido registrada.'
		ROLLBACK
		GOTO Terminar
	END
	ELSE BEGIN
		SET @Contador = (SELECT MAX(idPuntoParada) FROM ReportesApp_Operaciones_ItinerarioViajes_PuntoParada)
		SET @Contador = ISNULL(@Contador,0) + 1 

		INSERT INTO ReportesApp_Operaciones_ItinerarioViajes_PuntoParada(idPuntoParada, Descripcion)
		VALUES (@Contador, @Descripcion)

		SET @Exito = '0 = Parada registrada correctamente.'
	END
END TRY

BEGIN CATCH
	SET @Exito = RTRIM(LTRIM(STR(ERROR_NUMBER()))) + '=' + ERROR_MESSAGE()
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
	SET @Exito = @Exito + CHAR(13)+CHAR(13)+'SP Name: '+@storedProcedureName 
END

SELECT @Exito exito

------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 27-09-2024
-- Description: CREAR PARADA A RUTA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ItinerarioViajes_RegistrarEliminarParada]
@Opcion INT,
@idParada INT,
@Trafico VARCHAR(30),
@idRuta INT,
@PuntoInicio VARCHAR(250),
@PuntoParada VARCHAR(250),
@Horas TIME(7),
@UsuarioCreacion VARCHAR(20)
AS
DECLARE @Contador INT 
DECLARE @Contador2 INT 
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- REGISTRAR PARADA
		IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ItinerarioViajes_Paradas WHERE idRuta = @idRuta AND PuntoInicio = @PuntoInicio AND
			PuntoParada = @PuntoParada)) BEGIN
			SET @Exito = '-1 = Esta ruta ya cuenta con los puntos de parada ingresados.'
			ROLLBACK
			GOTO Terminar
		END
		ELSE BEGIN
			SET @Contador = (SELECT MAX(idParada) FROM ReportesApp_Operaciones_ItinerarioViajes_Paradas WHERE idRuta = @idRuta)
			SET @Contador = ISNULL(@Contador,0) + 1 

			INSERT INTO ReportesApp_Operaciones_ItinerarioViajes_Paradas(idParada, Trafico, idRuta, PuntoInicio, PuntoParada, Horas, UsuarioCreacion, FechaCreacion)
			VALUES (@Contador, @Trafico, @idRuta, @PuntoInicio, @PuntoParada, @Horas, @UsuarioCreacion, GETDATE())

			IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ItinerarioViajes_TiempoViaje WHERE idRuta = @idRuta)) BEGIN
				DECLARE @Suma INT = (SELECT SUM(DATEDIFF(SECOND,0,Horas)) FROM ReportesApp_Operaciones_ItinerarioViajes_Paradas WHERE idRuta = @idRuta)

				UPDATE ReportesApp_Operaciones_ItinerarioViajes_TiempoViaje
				SET TiempoViaje = (SELECT CAST(DATEADD(SECOND, @Suma, 0) AS TIME))
				WHERE idRuta = @idRuta
			END
			ELSE BEGIN
				SET @Contador2 = (SELECT MAX(idTotalParada) FROM ReportesApp_Operaciones_ItinerarioViajes_TiempoViaje)
				SET @Contador2 = ISNULL(@Contador2,0) + 1 

				INSERT INTO ReportesApp_Operaciones_ItinerarioViajes_TiempoViaje(idTotalParada, idRuta, TiempoViaje)
				VALUES (@Contador2, @idRuta, @Horas)
			END

			SET @Exito = '0 = Parada registrada exitosamente.'
		END
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR PARADA
		DECLARE @Total INT = (SELECT SUM(DATEDIFF(SECOND,0,Horas)) FROM ReportesApp_Operaciones_ItinerarioViajes_Paradas WHERE idRuta = @idRuta)
		DECLARE @SegundosParada INT = (SELECT DATEDIFF(SECOND,0,Horas) FROM ReportesApp_Operaciones_ItinerarioViajes_Paradas WHERE idParada = @idParada AND idRuta = @idRuta)
		DECLARE @Resta INT = @Total - @SegundosParada
		
		DELETE FROM ReportesApp_Operaciones_ItinerarioViajes_Paradas
		WHERE idParada = @idParada AND idRuta = @idRuta
		
		IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ItinerarioViajes_Paradas WHERE idRuta = @idRuta)) BEGIN
			UPDATE ReportesApp_Operaciones_ItinerarioViajes_TiempoViaje
			SET TiempoViaje = (SELECT CAST(DATEADD(SECOND, @Resta, 0) AS TIME))
			WHERE idRuta = @idRuta
		END
		ELSE BEGIN
			DELETE FROM ReportesApp_Operaciones_ItinerarioViajes_TiempoViaje
			WHERE idRuta = @idRuta
		END

		SET @Exito = '0 = Parada eliminada correctamente.'
	END
END TRY

BEGIN CATCH
	SET @Exito = RTRIM(LTRIM(STR(ERROR_NUMBER()))) + '=' + ERROR_MESSAGE()
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
	SET @Exito = @Exito + CHAR(13)+CHAR(13)+'SP Name: '+@storedProcedureName 
END

SELECT @Exito exito

------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 28-09-2024
-- Description:	LISTAR PARADAS Y RUTAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ItinerarioViajes_ListarParadasRutas]
@Opcion INT,
@Ruta VARCHAR(250)
AS
BEGIN
	IF (@Opcion = 1) BEGIN		-- LISTAR PARADAS
		SELECT P.idParada, P.idRuta, RT.Descripcion AS 'RUTA', P.Trafico AS 'TRÁFICO', P.PuntoInicio AS 'PUNTO_INICIO',
		P.PuntoParada AS 'PUNTO_PARADA', P.Horas AS 'HORAS_APROX'
		FROM ReportesApp_Operaciones_ItinerarioViajes_Paradas P
		LEFT JOIN OP_TR_RUTA RT WITH(NOLOCK) ON RT.IdRuta = P.idRuta
		WHERE (@Ruta IS NULL OR RT.Descripcion LIKE '%' + @Ruta + '%')
		ORDER BY RT.Descripcion, P.idParada
	END

	IF (@Opcion = 2) BEGIN		-- LISTAR RUTAS
		SELECT TV.idTotalParada AS 'NRO', TV.idRuta, RT.Descripcion AS 'RUTA', TV.TiempoViaje AS 'TIEMPO_VIAJE'
		FROM ReportesApp_Operaciones_ItinerarioViajes_TiempoViaje TV
		LEFT JOIN OP_TR_RUTA RT WITH(NOLOCK) ON RT.IdRuta = TV.idRuta
		WHERE (@Ruta IS NULL OR RT.Descripcion LIKE '%' + @Ruta + '%')
		ORDER BY TV.idTotalParada
	END
END

-------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 30-09-2024
-- Description: REGISTRAR CONSOLIDADO VIAJE
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ItinerarioViajes_RegistrarConsolidado]
@Opcion INT,
@idConsolidado INT,
@NroPreviaje INT,
@FechaProg DATETIME,
@idTracto INT,
@idCarreta INT,
@idRuta INT,
@idConductor INT,
@FechaViaje DATETIME,
@Usuario VARCHAR(20)
AS
DECLARE @Contador INT 
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- INSERTAR CONSOLIDADO
		IF (@FechaViaje <= DATEADD(HOUR,-1,GETDATE())) BEGIN
			SET @Exito = '-1 = No puede programar el itinerario de un viaje anterior a la fecha de hoy.'
			ROLLBACK
			GOTO Terminar
		END
		
		IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ItinerarioViajes_Consolidado WHERE NroPreviaje = @NroPreviaje)) BEGIN
			SET @Exito = '-2 = Este viaje ya ha sido registrado en el consolidado. Por favor, revise el consolidado.'
			ROLLBACK
			GOTO Terminar
		END
		ELSE BEGIN
			SET @Contador = (SELECT MAX(idConsolidado) FROM ReportesApp_Operaciones_ItinerarioViajes_Consolidado)
			SET @Contador = ISNULL(@Contador,0) + 1 

			INSERT INTO ReportesApp_Operaciones_ItinerarioViajes_Consolidado(idConsolidado, NroPreviaje, FechaProg, idTracto, idCarreta, idRuta,
			idConductor, FechaViaje, UsuarioCreacion, FechaCreacion)
			VALUES (@Contador, @NroPreviaje, @FechaProg, @idTracto, @idCarreta, @idRuta, @idConductor, @FechaViaje, @Usuario, GETDATE())

			SET @Exito = '0 = Viaje registrado correctamente. Por favor, revise el consolidado.'
		END
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR CONSOLIDADO
		IF ((SELECT FechaTermino FROM ReportesApp_Operaciones_ItinerarioViajes_Consolidado WHERE idConsolidado = @idConsolidado) IS NOT NULL) BEGIN
			SET @Exito = '-1 = Este viaje ya tiene una fecha de término. No se puede eliminar.'
			ROLLBACK
			GOTO Terminar
		END
		ELSE BEGIN
			DELETE FROM ReportesApp_Operaciones_ItinerarioViajes_Consolidado_Detalle
			WHERE idConsolidado = @idConsolidado

			DELETE FROM ReportesApp_Operaciones_ItinerarioViajes_Consolidado
			WHERE idConsolidado = @idConsolidado

			SET @Exito = '0 = Viaje eliminado correctamente. Por favor, revise el consolidado.'
		END
	END
END TRY

BEGIN CATCH
	SET @Exito = RTRIM(LTRIM(STR(ERROR_NUMBER()))) + '=' + ERROR_MESSAGE()
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
	SET @Exito = @Exito + CHAR(13)+CHAR(13)+'SP Name: '+@storedProcedureName 
END

SELECT @Exito exito

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 30-09-2024
-- Description:	LISTAR CONSOLIDADO VIAJES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ItinerarioViajes_ListarConsolidado]
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@Vehiculo VARCHAR(20),
@Conductor VARCHAR(250),
@Ruta VARCHAR(250)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	SELECT C.idConsolidado, C.NroPreviaje AS 'CÓDIGO', C.idRuta, RT.Descripcion AS 'RUTA', V1.NumeroPlaca AS 'TRACTO',
	V2.NumeroPlaca AS 'CARRETA', CO.Nombre AS 'CONDUCTOR', C.FechaViaje AS 'FECHA_VIAJE', TV.TiempoViaje AS 'HORAS_VIAJE',
	DATEADD(SECOND,DATEDIFF(SECOND,0,TV.TiempoViaje),C.FechaViaje) AS 'FECHA_ESTIMADA', C.FechaTermino AS 'FECHA_TERMINO',
	CASE WHEN C.FechaTermino IS NULL THEN
	CONVERT(VARCHAR(12), (DATEDIFF(SECOND,GETDATE(),DATEADD(SECOND,DATEDIFF(SECOND,0,TV.TiempoViaje),C.FechaViaje))) /60/60/24) + ' D ' +
	CONVERT(VARCHAR(12), ABS(DATEDIFF(SECOND,GETDATE(),DATEADD(SECOND,DATEDIFF(SECOND,0,TV.TiempoViaje),C.FechaViaje))) /60/60 % 24) + ' H ' +
	CONVERT(VARCHAR(2), ABS(DATEDIFF(SECOND,GETDATE(),DATEADD(SECOND,DATEDIFF(SECOND,0,TV.TiempoViaje),C.FechaViaje))) /60 % 60) + ' M'
	ELSE
	CONVERT(VARCHAR(12), (DATEDIFF(SECOND,C.FechaTermino,DATEADD(SECOND,DATEDIFF(SECOND,0,TV.TiempoViaje),C.FechaViaje))) /60/60/24) + ' D ' +
	CONVERT(VARCHAR(12), ABS(DATEDIFF(SECOND,C.FechaTermino,DATEADD(SECOND,DATEDIFF(SECOND,0,TV.TiempoViaje),C.FechaViaje))) /60/60 % 24) + ' H ' +
	CONVERT(VARCHAR(2), ABS(DATEDIFF(SECOND,C.FechaTermino,DATEADD(SECOND,DATEDIFF(SECOND,0,TV.TiempoViaje),C.FechaViaje))) /60 % 60) + ' M'
	END AS 'TIEMPO_RESTANTE',
	CASE WHEN C.FechaTermino IS NULL THEN
		CASE WHEN DATEADD(SECOND,DATEDIFF(SECOND,0,TV.TiempoViaje),C.FechaViaje) <= GETDATE() THEN '  '
		WHEN DATEADD(SECOND,DATEDIFF(SECOND,0,TV.TiempoViaje),C.FechaViaje) > GETDATE() THEN ' ' ELSE '   ' END 
	ELSE
		CASE WHEN DATEADD(SECOND,DATEDIFF(SECOND,0,TV.TiempoViaje),C.FechaViaje) > C.FechaTermino THEN ' '
		WHEN DATEADD(SECOND,DATEDIFF(SECOND,0,TV.TiempoViaje),C.FechaViaje) <= C.FechaTermino THEN '  ' ELSE '   ' END 
	END AS 'ESTADO', C.UsuarioCreacion, C.FechaCreacion
	FROM ReportesApp_Operaciones_ItinerarioViajes_Consolidado C
	LEFT JOIN OP_TR_RUTA RT WITH(NOLOCK) ON RT.IdRuta = C.idRuta
	LEFT JOIN OP_TR_Vehiculo V1 WITH(NOLOCK) ON V1.IdVehiculo = C.idTracto
	LEFT JOIN OP_TR_Vehiculo V2 WITH(NOLOCK) ON V2.IdVehiculo = C.idCarreta
	LEFT JOIN OP_TR_Conductor CO WITH(NOLOCK) ON CO.IdConductor = C.idConductor
	LEFT JOIN ReportesApp_Operaciones_ItinerarioViajes_TiempoViaje TV ON TV.idRuta = C.idRuta
	WHERE (@Conductor IS NULL OR CO.Nombre LIKE '%' + @Conductor + '%') AND (@Ruta IS NULL OR RT.Descripcion LIKE '%' + @Ruta + '%') AND
	((@Vehiculo IS NULL OR V1.NumeroPlaca LIKE '%' + @Vehiculo + '%') OR (@Vehiculo IS NULL OR V2.NumeroPlaca LIKE '%' + @Vehiculo + '%')) AND
	(C.FechaViaje BETWEEN @FINICIO AND @FFIN)
END

--------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 01-10-2024
-- Description:	LISTAR CONSOLIDADO DETALLE
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ItinerarioViajes_ListarConsolidadoDetalle]
@idConsolidado INT
AS
BEGIN
	SELECT C.idConsolidado, P.idParada, P.PuntoInicio AS 'PUNTO_INICIO', P.PuntoParada AS 'PUNTO_PARADA', P.Horas AS 'HORAS_APROX',
	CD.HoraDuracion AS 'HORA_DURACION'
	FROM ReportesApp_Operaciones_ItinerarioViajes_Consolidado C
	LEFT JOIN ReportesApp_Operaciones_ItinerarioViajes_Paradas P ON C.idRuta = P.idRuta
	LEFT JOIN ReportesApp_Operaciones_ItinerarioViajes_Consolidado_Detalle CD ON CD.idConsolidado = C.idConsolidado AND CD.idParada = P.idParada
	WHERE C.idConsolidado = @idConsolidado
	ORDER BY P.idParada ASC
END

------------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 01-10-2024
-- Description: REGISTRAR CONSOLIDADO DETALLE
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ItinerarioViajes_RegistrarConsolidadoDetalle]
@idConsolidado INT, --2
@idParada INT, --1
@HoraDuracion TIME(7) -- 03:00:00
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ItinerarioViajes_Consolidado_Detalle WHERE idConsolidado = @idConsolidado AND idParada = @idParada)) BEGIN
		SET @Exito = '-1 = Elimine el tiempo de viaje ya ingresado en esta parada para volver a ingresar.'
		ROLLBACK
		GOTO Terminar
	END
	ELSE BEGIN
		INSERT INTO ReportesApp_Operaciones_ItinerarioViajes_Consolidado_Detalle(idConsolidado,idParada,HoraDuracion)
		VALUES(@idConsolidado,@idParada,@HoraDuracion)

		IF ((SELECT FechaTermino FROM ReportesApp_Operaciones_ItinerarioViajes_Consolidado WHERE idConsolidado = @idConsolidado) IS NULL) BEGIN
			UPDATE ReportesApp_Operaciones_ItinerarioViajes_Consolidado
			SET FechaTermino = DATEADD(SECOND,DATEDIFF(SECOND,0,@HoraDuracion),FechaViaje)
			WHERE idConsolidado = @idConsolidado
		END
		ELSE BEGIN
			UPDATE ReportesApp_Operaciones_ItinerarioViajes_Consolidado
			SET FechaTermino = DATEADD(SECOND,DATEDIFF(SECOND,0,@HoraDuracion),FechaTermino)
			WHERE idConsolidado = @idConsolidado
		END

		SET @Exito = '0 = Tiempo asignado exitosamente.'
	END
END TRY

BEGIN CATCH
	SET @Exito = RTRIM(LTRIM(STR(ERROR_NUMBER()))) + '=' + ERROR_MESSAGE()
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
	SET @Exito = @Exito + CHAR(13)+CHAR(13)+'SP Name: '+@storedProcedureName 
END

SELECT @Exito exito

------------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 01-10-2024
-- Description: ELIMINAR CONSOLIDADO DETALLE
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ItinerarioViajes_EliminarConsolidadoDetalle]
@idConsolidado INT,
@idParada INT
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	DECLARE @HoraDuracion TIME(7) = (SELECT HoraDuracion FROM ReportesApp_Operaciones_ItinerarioViajes_Consolidado_Detalle
									 WHERE idConsolidado = @idConsolidado AND idParada = @idParada)
	
	DELETE FROM ReportesApp_Operaciones_ItinerarioViajes_Consolidado_Detalle
	WHERE idConsolidado = @idConsolidado AND idParada = @idParada
	
	IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ItinerarioViajes_Consolidado_Detalle WHERE idConsolidado = @idConsolidado)) BEGIN
		UPDATE ReportesApp_Operaciones_ItinerarioViajes_Consolidado
		SET FechaTermino = DATEADD(SECOND,-DATEDIFF(SECOND,0,@HoraDuracion),FechaTermino)
		WHERE idConsolidado = @idConsolidado
	END
	ELSE BEGIN
		UPDATE ReportesApp_Operaciones_ItinerarioViajes_Consolidado
		SET FechaTermino = NULL
		WHERE idConsolidado = @idConsolidado
	END

	SET @Exito = '0 = Tiempo eliminado exitosamente.'
END TRY

BEGIN CATCH
	SET @Exito = RTRIM(LTRIM(STR(ERROR_NUMBER()))) + '=' + ERROR_MESSAGE()
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
	SET @Exito = @Exito + CHAR(13)+CHAR(13)+'SP Name: '+@storedProcedureName 
END

SELECT @Exito exito

------------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 02-10-2024
-- Description:	LISTAR FECHA TERMINO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ItinerarioViajes_ListarFechaTermino]
@idConsolidado INT
AS
BEGIN
	SELECT C.FechaTermino
	FROM ReportesApp_Operaciones_ItinerarioViajes_Consolidado C
	WHERE C.idConsolidado = @idConsolidado
END