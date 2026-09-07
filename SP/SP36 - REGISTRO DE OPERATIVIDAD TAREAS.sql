
-------------------------- OTROS --------------------------

DECLARE @OP_PLACAS TABLE (Numero INT, IDCondicion INT, IDTracto INT, FInicio DATE, Usuario VARCHAR(80))
DECLARE @OP_PLACAS_M TABLE (Numero INT, IDCondicion INT, IDTracto INT, FInicio DATE, FTermino DATETIME, Usuario VARCHAR(80))
DECLARE @Contador INT = 1
DECLARE @idCondicion INT, @IDTracto INT, @FInicio DATE, @FechaTermino DATETIME, @Usuario VARCHAR(80)

INSERT INTO @OP_PLACAS(Numero, IDCondicion, IDTracto, FInicio, Usuario)
SELECT DISTINCT ROW_NUMBER() OVER(ORDER BY B.IdBloqueo ASC), 12, B.IdUnidad, CONVERT(DATE,GETDATE()), 'AUTOMATICO'
FROM ReportesApp_Operacion_UnidadesBloqueadas B
WHERE (Descripcion LIKE '%LEGAL%')

WHILE (@Contador <= (SELECT COUNT(Numero) FROM @OP_PLACAS)) BEGIN
	SET @idCondicion = (SELECT IDCondicion FROM @OP_PLACAS WHERE Numero = @Contador)
	SET @IDTracto = (SELECT IDTracto FROM @OP_PLACAS WHERE Numero = @Contador)
	SET @FInicio = (SELECT FInicio FROM @OP_PLACAS WHERE Numero = @Contador)
	SET @Usuario = (SELECT Usuario FROM @OP_PLACAS WHERE Numero = @Contador)

	EXEC ReportesApp_Operaciones_Operatividad_ActualizarViajeOtros @IDCondicion = 12, @IDTracto = @IDTracto, @FechaInicio = @FInicio, @Usuario = @Usuario
	
	SET @Contador = @Contador + 1
END

DELETE FROM @OP_PLACAS
SET @Contador = 1

INSERT INTO @OP_PLACAS(Numero, IDCondicion, IDTracto, FInicio, Usuario)
SELECT DISTINCT ROW_NUMBER() OVER(ORDER BY B.IdBloqueo ASC), 14, B.IdUnidad, CONVERT(DATE,GETDATE()), 'AUTOMATICO'
FROM ReportesApp_Operacion_UnidadesBloqueadas B
WHERE (Descripcion LIKE '%MACK ROJO%')

WHILE (@Contador <= (SELECT COUNT(Numero) FROM @OP_PLACAS)) BEGIN
	SET @idCondicion = (SELECT IDCondicion FROM @OP_PLACAS WHERE Numero = @Contador)
	SET @IDTracto = (SELECT IDTracto FROM @OP_PLACAS WHERE Numero = @Contador)
	SET @FInicio = (SELECT FInicio FROM @OP_PLACAS WHERE Numero = @Contador)
	SET @Usuario = (SELECT Usuario FROM @OP_PLACAS WHERE Numero = @Contador)

	EXEC ReportesApp_Operaciones_Operatividad_ActualizarViajeOtros @IDCondicion = 14, @IDTracto = @IDTracto, @FechaInicio = @FInicio, @Usuario = @Usuario
	
	SET @Contador = @Contador + 1
END

DELETE FROM @OP_PLACAS
SET @Contador = 1

INSERT INTO @OP_PLACAS(Numero, IDCondicion, IDTracto, FInicio, Usuario)
SELECT DISTINCT ROW_NUMBER() OVER(ORDER BY B.IdBloqueo ASC), 8, B.IdUnidad, CONVERT(DATE,GETDATE()), 'AUTOMATICO'
FROM ReportesApp_Operacion_UnidadesBloqueadas B
WHERE (Descripcion NOT LIKE '%MACK ROJO%' AND Descripcion NOT LIKE '%LEGAL%')

WHILE (@Contador <= (SELECT COUNT(Numero) FROM @OP_PLACAS)) BEGIN
	SET @idCondicion = (SELECT IDCondicion FROM @OP_PLACAS WHERE Numero = @Contador)
	SET @IDTracto = (SELECT IDTracto FROM @OP_PLACAS WHERE Numero = @Contador)
	SET @FInicio = (SELECT FInicio FROM @OP_PLACAS WHERE Numero = @Contador)
	SET @Usuario = (SELECT Usuario FROM @OP_PLACAS WHERE Numero = @Contador)

	EXEC ReportesApp_Operaciones_Operatividad_ActualizarViajeOtros @IDCondicion = 8, @IDTracto = @IDTracto, @FechaInicio = @FInicio, @Usuario = @Usuario
	
	SET @Contador = @Contador + 1
END

DELETE FROM @OP_PLACAS
SET @Contador = 1

INSERT INTO @OP_PLACAS_M(Numero, IDCondicion, IDTracto, FInicio, FTermino, Usuario)
SELECT ROW_NUMBER() OVER(ORDER BY X.idTracto ASC), 6, X.idTracto, CONVERT(DATE,GETDATE()), X.FechaTermino, 'AUTOMATICO'
FROM (SELECT DISTINCT S.idTracto, ISNULL(S.FechaEntrega,ISNULL(S.FechaReprog,ISNULL(S.FechaProg,S.FechaRecepcion))) AS 'FechaTermino'
FROM ReportesApp_Mantenimiento_Solicitud_Registro S
WHERE Estado IN ('RECEPCIONADO','PROGRAMADO','REPROGRAMADO') AND TipoMtto LIKE '%CORRECTIVO%' AND idBase IN (1,2,8) AND
FechaRecepcion BETWEEN '01/01/2024 00:00:00' AND GETDATE()
UNION
SELECT DISTINCT S.idTracto, ISNULL(S.FechaEntrega,ISNULL(S.FechaReprog,ISNULL(S.FechaProg,S.FechaRecepcion))) AS 'FechaTermino'
FROM ReportesApp_Mantenimiento_Solicitud_Registro S
WHERE CONVERT(DATE,FechaEntrega) = CONVERT(DATE,GETDATE()) AND TipoMtto LIKE '%CORRECTIVO%' AND idBase IN (1,2,8) AND
CONVERT(TIME,FechaEntrega) >= '16:00:00') X

WHILE (@Contador <= (SELECT COUNT(Numero) FROM @OP_PLACAS_M)) BEGIN
	SET @idCondicion = (SELECT IDCondicion FROM @OP_PLACAS_M WHERE Numero = @Contador)
	SET @IDTracto = (SELECT IDTracto FROM @OP_PLACAS_M WHERE Numero = @Contador)
	SET @FInicio = (SELECT FInicio FROM @OP_PLACAS_M WHERE Numero = @Contador)
	SET @Usuario = (SELECT Usuario FROM @OP_PLACAS_M WHERE Numero = @Contador)
	SET @FechaTermino = (SELECT FTermino FROM @OP_PLACAS_M WHERE Numero = @Contador)

	EXEC ReportesApp_Operaciones_Operatividad_ActualizarViajeOtros @IDCondicion = 6, @IDTracto = @IDTracto, @FechaInicio = @FInicio, @Usuario = @Usuario
	
	UPDATE ReportesApp_Operaciones_Operatividad_Registro
	SET FechaTermino = @FechaTermino
	WHERE IdUnidad = @IDTracto AND Fecha = @FInicio AND idCondicion = 6

	SET @Contador = @Contador + 1
END

DELETE FROM @OP_PLACAS
DELETE FROM @OP_PLACAS_M
SET @Contador = 1

INSERT INTO @OP_PLACAS_M(Numero, IDCondicion, IDTracto, FInicio, FTermino, Usuario)
SELECT ROW_NUMBER() OVER(ORDER BY X.idTracto ASC), 5, X.idTracto, CONVERT(DATE,GETDATE()), X.FechaTermino, 'AUTOMATICO'
FROM (SELECT DISTINCT S.idTracto, ISNULL(S.FechaEntrega,ISNULL(S.FechaReprog,ISNULL(S.FechaProg,S.FechaRecepcion))) AS 'FechaTermino'
FROM ReportesApp_Mantenimiento_Solicitud_Registro S
WHERE Estado IN ('RECEPCIONADO','PROGRAMADO','REPROGRAMADO') AND TipoMtto LIKE '%PREVENTIVO%' AND idBase IN (1,2,8) AND
FechaRecepcion BETWEEN '01/01/2024 00:00:00' AND GETDATE()
UNION
SELECT DISTINCT S.idTracto, ISNULL(S.FechaEntrega,ISNULL(S.FechaReprog,ISNULL(S.FechaProg,S.FechaRecepcion))) AS 'FechaTermino'
FROM ReportesApp_Mantenimiento_Solicitud_Registro S
WHERE CONVERT(DATE,FechaEntrega) = CONVERT(DATE,GETDATE()) AND TipoMtto LIKE '%PREVENTIVO%' AND idBase IN (1,2,8) AND
CONVERT(TIME,FechaEntrega) >= '16:00:00') X

WHILE (@Contador <= (SELECT COUNT(Numero) FROM @OP_PLACAS_M)) BEGIN
	SET @idCondicion = (SELECT IDCondicion FROM @OP_PLACAS_M WHERE Numero = @Contador)
	SET @IDTracto = (SELECT IDTracto FROM @OP_PLACAS_M WHERE Numero = @Contador)
	SET @FInicio = (SELECT FInicio FROM @OP_PLACAS_M WHERE Numero = @Contador)
	SET @Usuario = (SELECT Usuario FROM @OP_PLACAS_M WHERE Numero = @Contador)
	SET @FechaTermino = (SELECT FTermino FROM @OP_PLACAS_M WHERE Numero = @Contador)

	EXEC ReportesApp_Operaciones_Operatividad_ActualizarViajeOtros @IDCondicion = 5, @IDTracto = @IDTracto, @FechaInicio = @FInicio, @Usuario = @Usuario
	
	UPDATE ReportesApp_Operaciones_Operatividad_Registro
	SET FechaTermino = @FechaTermino
	WHERE IdUnidad = @IDTracto AND Fecha = @FInicio AND idCondicion = 5

	SET @Contador = @Contador + 1
END

DELETE FROM @OP_PLACAS
DELETE FROM @OP_PLACAS_M
SET @Contador = 1

INSERT INTO @OP_PLACAS(Numero, IDCondicion, IDTracto, FInicio, Usuario)
SELECT ROW_NUMBER() OVER(ORDER BY X.idTracto ASC), 13, X.idTracto, CONVERT(DATE,GETDATE()), 'AUTOMATICO'
FROM (SELECT DISTINCT S.idTracto
FROM ReportesApp_Mantenimiento_Solicitud_Registro S
WHERE Estado IN ('RECEPCIONADO','PROGRAMADO','REPROGRAMADO') AND idBase NOT IN (1,2,8) AND
FechaRecepcion BETWEEN '01/01/2024 00:00:00' AND GETDATE()
UNION
SELECT DISTINCT S.idTracto
FROM ReportesApp_Mantenimiento_Solicitud_Registro S
WHERE CONVERT(DATE,FechaEntrega) = CONVERT(DATE,GETDATE()) AND idBase NOT IN (1,2,8) AND
CONVERT(TIME,FechaEntrega) >= '16:00:00') X

WHILE (@Contador <= (SELECT COUNT(Numero) FROM @OP_PLACAS)) BEGIN
	SET @idCondicion = (SELECT IDCondicion FROM @OP_PLACAS WHERE Numero = @Contador)
	SET @IDTracto = (SELECT IDTracto FROM @OP_PLACAS WHERE Numero = @Contador)
	SET @FInicio = (SELECT FInicio FROM @OP_PLACAS WHERE Numero = @Contador)
	SET @Usuario = (SELECT Usuario FROM @OP_PLACAS WHERE Numero = @Contador)

	EXEC ReportesApp_Operaciones_Operatividad_ActualizarViajeOtros @IDCondicion = 13, @IDTracto = @IDTracto, @FechaInicio = @FInicio, @Usuario = @Usuario
	
	SET @Contador = @Contador + 1
END

DELETE FROM @OP_PLACAS
SET @Contador = 1

INSERT INTO @OP_PLACAS(Numero, IDCondicion, IDTracto, FInicio, Usuario)
SELECT DISTINCT ROW_NUMBER() OVER(ORDER BY P.IdUnidad ASC), 10, P.IdUnidad, CONVERT(DATE,GETDATE()), 'AUTOMATICO'
FROM ReportesApp_Operaciones_Operatividad_PlacasView P
LEFT JOIN ReportesApp_Operaciones_Operatividad_Registro R ON P.IdUnidad = R.IdUnidad AND R.Fecha = CONVERT(DATE,GETDATE())
WHERE P.Anio = YEAR(GETDATE()) AND P.Mes = MONTH(GETDATE()) AND R.IdUnidad IS NULL

WHILE (@Contador <= (SELECT COUNT(Numero) FROM @OP_PLACAS)) BEGIN
	SET @idCondicion = (SELECT IDCondicion FROM @OP_PLACAS WHERE Numero = @Contador)
	SET @IDTracto = (SELECT IDTracto FROM @OP_PLACAS WHERE Numero = @Contador)
	SET @FInicio = (SELECT FInicio FROM @OP_PLACAS WHERE Numero = @Contador)
	SET @Usuario = (SELECT Usuario FROM @OP_PLACAS WHERE Numero = @Contador)

	EXEC ReportesApp_Operaciones_Operatividad_ActualizarViajeOtros @IDCondicion = 10, @IDTracto = @IDTracto, @FechaInicio = @FInicio, @Usuario = @Usuario
	
	SET @Contador = @Contador + 1
END

-------------------------- EJECUTAR --------------------------

DECLARE @OP_PLACAS TABLE (Numero INT, IDTicket INT, Anio SMALLINT, IDTracto INT, Estado INT, TipoProgramacion INT,
FInicio DATETIME, FFin DATETIME, Usuario VARCHAR(80))
DECLARE @Contador INT = 1

INSERT INTO @OP_PLACAS(Numero, IDTicket, Anio, IDTracto, Estado, TipoProgramacion, FInicio, FFin, Usuario)
SELECT ROW_NUMBER() OVER(ORDER BY X.IdProgramacion ASC), X.IdProgramacion, X.Anio, X.idTracto, X.Estado, X.TipoProgramacion,
X.FechaInicio, X.FechaFin, X.Usuario FROM
(SELECT IdProgramacion, Anio, idTracto, Estado, TipoProgramacion, FechaProgramacion AS 'FechaInicio', FechaTermino AS 'FechaFin', 'AUTOMATICO' AS 'Usuario'
FROM ReportesApp_Operacion_Previaje_Registros
WHERE CONVERT(DATE,GETDATE()) BETWEEN CONVERT(DATE,FechaProgramacion) AND CONVERT(DATE,FechaTermino)  
AND TipoProgramacion IN (2,3,4,10) AND Estado IN (1,9) AND idTracto != -1 AND FechaTermino IS NOT NULL
UNION
SELECT IdProgramacion, Anio, idTracto, Estado, TipoProgramacion, FechaProgramacion AS 'FechaInicio', FechaFin AS 'FechaFin', 'AUTOMATICO' AS 'Usuario'
FROM ReportesApp_Operacion_Previaje_Registros
WHERE CONVERT(DATE,GETDATE()) BETWEEN CONVERT(DATE,FechaProgramacion) AND CONVERT(DATE,FechaFin)  
AND TipoProgramacion IN (2,3,4,10) AND Estado IN (1,9) AND idTracto != -1 AND FechaTermino IS NULL) X

WHILE (@Contador <= (SELECT COUNT(Numero) FROM @OP_PLACAS)) BEGIN
	DECLARE @IDTicket INT = (SELECT IDTicket FROM @OP_PLACAS WHERE Numero = @Contador)
	DECLARE @Anio SMALLINT = (SELECT Anio FROM @OP_PLACAS WHERE Numero = @Contador)
	DECLARE @IDTracto INT = (SELECT IDTracto FROM @OP_PLACAS WHERE Numero = @Contador)
	DECLARE @Estado INT = (SELECT Estado FROM @OP_PLACAS WHERE Numero = @Contador)
	DECLARE @TipoProgramacion INT = (SELECT TipoProgramacion FROM @OP_PLACAS WHERE Numero = @Contador)
	DECLARE @FInicio DATETIME = (SELECT FInicio FROM @OP_PLACAS WHERE Numero = @Contador)
	DECLARE @FFin DATETIME = (SELECT FFin FROM @OP_PLACAS WHERE Numero = @Contador)
	DECLARE @Usuario VARCHAR(80) = (SELECT Usuario FROM @OP_PLACAS WHERE Numero = @Contador)

	EXEC ReportesApp_Operaciones_Previajes_ActualizarViajeEstado @IDTicket = @IDTicket, @Anio = @Anio, @IDTracto = @IDTracto,
	@Estado = @Estado, @TipoProgramacion = @TipoProgramacion, @FInicio = @FInicio, @FFin = @FFin, @Usuario = @Usuario
	
	SET @Contador = @Contador + 1
END

DECLARE @ContOp INT = 1
DECLARE @Periodo VARCHAR(6) = CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(GETDATE()))),2))+CONVERT(CHAR(4),YEAR(GETDATE()))
WHILE (@ContOp <= 4) BEGIN
	IF (@ContOp = 4) BEGIN
		SET @ContOp = 10
	END 

	EXEC ReportesApp_Operaciones_Operatividad_RegistrarIngresos @idOperacion = @ContOp, @Periodo=@Periodo, @Usuario = 'CGUZMAN'

	SET @ContOp = @ContOp + 1
END

-------------------------- TOLVAS --------------------------

DECLARE @OP_PLACAS TABLE (Numero INT, NroTicket INT, IDTracto INT, FInicio DATETIME, Usuario VARCHAR(80))
DECLARE @Contador INT = 1

INSERT INTO @OP_PLACAS(Numero, NroTicket, IDTracto, FInicio, Usuario)
SELECT ROW_NUMBER() OVER(ORDER BY X.NroTicket ASC), X.NroTicket, X.idUnidad, X.FechaViaje, X.Usuario FROM
(SELECT DISTINCT T.NroTicket, T.idUnidad, T.FechaViaje, 'AUTOMATICO' AS Usuario
FROM ReportesApp_Operaciones_Viajes_Tolvas T 
WHERE (EstadoViaje IN ('EJECUCION','COMPLETADO')) AND CONVERT(DATE,T.FechaViaje) >= CONVERT(DATE,GETDATE())) X

WHILE (@Contador <= (SELECT COUNT(Numero) FROM @OP_PLACAS)) BEGIN
	DECLARE @NroTicket INT = (SELECT NroTicket FROM @OP_PLACAS WHERE Numero = @Contador)
	DECLARE @IDTracto INT = (SELECT IDTracto FROM @OP_PLACAS WHERE Numero = @Contador)
	DECLARE @FInicio DATE = (SELECT FInicio FROM @OP_PLACAS WHERE Numero = @Contador)
	DECLARE @Usuario VARCHAR(80) = (SELECT Usuario FROM @OP_PLACAS WHERE Numero = @Contador)

	EXEC ReportesApp_Operaciones_Operatividad_ActualizarViajeTolvas @NroTicket = @NroTicket, @IDTracto = @IDTracto, @FechaInicio = @FInicio, @Usuario = @Usuario
	
	SET @Contador = @Contador + 1
END


-------------------------- OTRAS CARRETAS --------------------------

DECLARE @OP_PLACAS TABLE (Numero INT, IDCondicion INT, IDTracto INT, FInicio DATE, Usuario VARCHAR(80))
DECLARE @OP_PLACAS_M TABLE (Numero INT, IDCondicion INT, IDTracto INT, FInicio DATE, FTermino DATETIME, Usuario VARCHAR(80))
DECLARE @Contador INT = 1
DECLARE @idCondicion INT, @IDTracto INT, @FInicio DATE, @FechaTermino DATETIME, @Usuario VARCHAR(80)

INSERT INTO @OP_PLACAS(Numero, IDCondicion, IDTracto, FInicio, Usuario)
SELECT DISTINCT ROW_NUMBER() OVER(ORDER BY B.IdBloqueo ASC), 12, B.IdUnidad, CONVERT(DATE,GETDATE()), 'AUTOMATICO'
FROM ReportesApp_Operacion_UnidadesBloqueadas B
WHERE (Descripcion LIKE '%LEGAL%')

WHILE (@Contador <= (SELECT COUNT(Numero) FROM @OP_PLACAS)) BEGIN
	SET @idCondicion = (SELECT IDCondicion FROM @OP_PLACAS WHERE Numero = @Contador)
	SET @IDTracto = (SELECT IDTracto FROM @OP_PLACAS WHERE Numero = @Contador)
	SET @FInicio = (SELECT FInicio FROM @OP_PLACAS WHERE Numero = @Contador)
	SET @Usuario = (SELECT Usuario FROM @OP_PLACAS WHERE Numero = @Contador)

	EXEC ReportesApp_Operaciones_Operatividad_ActualizarViajeOtrosCarretas @IDCondicion = 12, @IDTracto = @IDTracto, @FechaInicio = @FInicio, @Usuario = @Usuario
	
	SET @Contador = @Contador + 1
END

DELETE FROM @OP_PLACAS
SET @Contador = 1

INSERT INTO @OP_PLACAS(Numero, IDCondicion, IDTracto, FInicio, Usuario)
SELECT DISTINCT ROW_NUMBER() OVER(ORDER BY B.IdBloqueo ASC), 14, B.IdUnidad, CONVERT(DATE,GETDATE()), 'AUTOMATICO'
FROM ReportesApp_Operacion_UnidadesBloqueadas B
WHERE (Descripcion LIKE '%MACK ROJO%')

WHILE (@Contador <= (SELECT COUNT(Numero) FROM @OP_PLACAS)) BEGIN
	SET @idCondicion = (SELECT IDCondicion FROM @OP_PLACAS WHERE Numero = @Contador)
	SET @IDTracto = (SELECT IDTracto FROM @OP_PLACAS WHERE Numero = @Contador)
	SET @FInicio = (SELECT FInicio FROM @OP_PLACAS WHERE Numero = @Contador)
	SET @Usuario = (SELECT Usuario FROM @OP_PLACAS WHERE Numero = @Contador)

	EXEC ReportesApp_Operaciones_Operatividad_ActualizarViajeOtrosCarretas @IDCondicion = 14, @IDTracto = @IDTracto, @FechaInicio = @FInicio, @Usuario = @Usuario
	
	SET @Contador = @Contador + 1
END

DELETE FROM @OP_PLACAS
SET @Contador = 1

INSERT INTO @OP_PLACAS(Numero, IDCondicion, IDTracto, FInicio, Usuario)
SELECT DISTINCT ROW_NUMBER() OVER(ORDER BY B.IdBloqueo ASC), 8, B.IdUnidad, CONVERT(DATE,GETDATE()), 'AUTOMATICO'
FROM ReportesApp_Operacion_UnidadesBloqueadas B
WHERE (Descripcion NOT LIKE '%MACK ROJO%' AND Descripcion NOT LIKE '%LEGAL%')

WHILE (@Contador <= (SELECT COUNT(Numero) FROM @OP_PLACAS)) BEGIN
	SET @idCondicion = (SELECT IDCondicion FROM @OP_PLACAS WHERE Numero = @Contador)
	SET @IDTracto = (SELECT IDTracto FROM @OP_PLACAS WHERE Numero = @Contador)
	SET @FInicio = (SELECT FInicio FROM @OP_PLACAS WHERE Numero = @Contador)
	SET @Usuario = (SELECT Usuario FROM @OP_PLACAS WHERE Numero = @Contador)

	EXEC ReportesApp_Operaciones_Operatividad_ActualizarViajeOtrosCarretas @IDCondicion = 8, @IDTracto = @IDTracto, @FechaInicio = @FInicio, @Usuario = @Usuario
	
	SET @Contador = @Contador + 1
END

DELETE FROM @OP_PLACAS
SET @Contador = 1

INSERT INTO @OP_PLACAS_M(Numero, IDCondicion, IDTracto, FInicio, FTermino, Usuario)
SELECT ROW_NUMBER() OVER(ORDER BY X.idTracto ASC), 6, X.idTracto, CONVERT(DATE,GETDATE()), X.FechaTermino, 'AUTOMATICO'
FROM (SELECT DISTINCT S.idTracto, ISNULL(S.FechaEntrega,ISNULL(S.FechaReprog,ISNULL(S.FechaProg,S.FechaRecepcion))) AS 'FechaTermino'
FROM ReportesApp_Mantenimiento_Solicitud_Registro S
WHERE Estado IN ('RECEPCIONADO','PROGRAMADO','REPROGRAMADO') AND TipoMtto LIKE '%CORRECTIVO%' AND idBase IN (1,2,8) AND
FechaRecepcion BETWEEN '01/01/2024 00:00:00' AND GETDATE()
UNION
SELECT DISTINCT S.idTracto, ISNULL(S.FechaEntrega,ISNULL(S.FechaReprog,ISNULL(S.FechaProg,S.FechaRecepcion))) AS 'FechaTermino'
FROM ReportesApp_Mantenimiento_Solicitud_Registro S
WHERE CONVERT(DATE,FechaEntrega) = CONVERT(DATE,GETDATE()) AND TipoMtto LIKE '%CORRECTIVO%' AND idBase IN (1,2,8) AND
CONVERT(TIME,FechaEntrega) >= '16:00:00') X

WHILE (@Contador <= (SELECT COUNT(Numero) FROM @OP_PLACAS_M)) BEGIN
	SET @idCondicion = (SELECT IDCondicion FROM @OP_PLACAS_M WHERE Numero = @Contador)
	SET @IDTracto = (SELECT IDTracto FROM @OP_PLACAS_M WHERE Numero = @Contador)
	SET @FInicio = (SELECT FInicio FROM @OP_PLACAS_M WHERE Numero = @Contador)
	SET @Usuario = (SELECT Usuario FROM @OP_PLACAS_M WHERE Numero = @Contador)
	SET @FechaTermino = (SELECT FTermino FROM @OP_PLACAS_M WHERE Numero = @Contador)

	EXEC ReportesApp_Operaciones_Operatividad_ActualizarViajeOtrosCarretas @IDCondicion = 6, @IDTracto = @IDTracto, @FechaInicio = @FInicio, @Usuario = @Usuario
	
	UPDATE ReportesApp_Operaciones_Operatividad_RegistroCarretas
	SET FechaTermino = @FechaTermino
	WHERE IdUnidad = @IDTracto AND Fecha = @FInicio AND idCondicion = 6
	
	SET @Contador = @Contador + 1
END

DELETE FROM @OP_PLACAS
DELETE FROM @OP_PLACAS_M
SET @Contador = 1

INSERT INTO @OP_PLACAS_M(Numero, IDCondicion, IDTracto, FInicio, FTermino, Usuario)
SELECT ROW_NUMBER() OVER(ORDER BY X.idTracto ASC), 5, X.idTracto, CONVERT(DATE,GETDATE()), X.FechaTermino, 'AUTOMATICO'
FROM (SELECT DISTINCT S.idTracto, ISNULL(S.FechaEntrega,ISNULL(S.FechaReprog,ISNULL(S.FechaProg,S.FechaRecepcion))) AS 'FechaTermino'
FROM ReportesApp_Mantenimiento_Solicitud_Registro S
WHERE Estado IN ('RECEPCIONADO','PROGRAMADO','REPROGRAMADO') AND TipoMtto LIKE '%PREVENTIVO%' AND idBase IN (1,2,8) AND
FechaRecepcion BETWEEN '01/01/2024 00:00:00' AND GETDATE()
UNION
SELECT DISTINCT S.idTracto, ISNULL(S.FechaEntrega,ISNULL(S.FechaReprog,ISNULL(S.FechaProg,S.FechaRecepcion))) AS 'FechaTermino'
FROM ReportesApp_Mantenimiento_Solicitud_Registro S
WHERE CONVERT(DATE,FechaEntrega) = CONVERT(DATE,GETDATE()) AND TipoMtto LIKE '%PREVENTIVO%' AND idBase IN (1,2,8) AND
CONVERT(TIME,FechaEntrega) >= '16:00:00') X

WHILE (@Contador <= (SELECT COUNT(Numero) FROM @OP_PLACAS_M)) BEGIN
	SET @idCondicion = (SELECT IDCondicion FROM @OP_PLACAS_M WHERE Numero = @Contador)
	SET @IDTracto = (SELECT IDTracto FROM @OP_PLACAS_M WHERE Numero = @Contador)
	SET @FInicio = (SELECT FInicio FROM @OP_PLACAS_M WHERE Numero = @Contador)
	SET @Usuario = (SELECT Usuario FROM @OP_PLACAS_M WHERE Numero = @Contador)
	SET @FechaTermino = (SELECT FTermino FROM @OP_PLACAS_M WHERE Numero = @Contador)

	EXEC ReportesApp_Operaciones_Operatividad_ActualizarViajeOtrosCarretas @IDCondicion = 5, @IDTracto = @IDTracto, @FechaInicio = @FInicio, @Usuario = @Usuario
	
	UPDATE ReportesApp_Operaciones_Operatividad_RegistroCarretas
	SET FechaTermino = @FechaTermino
	WHERE IdUnidad = @IDTracto AND Fecha = @FInicio AND idCondicion = 5

	SET @Contador = @Contador + 1
END

DELETE FROM @OP_PLACAS
DELETE FROM @OP_PLACAS_M
SET @Contador = 1

INSERT INTO @OP_PLACAS(Numero, IDCondicion, IDTracto, FInicio, Usuario)
SELECT ROW_NUMBER() OVER(ORDER BY X.idTracto ASC), 13, X.idTracto, CONVERT(DATE,GETDATE()), 'AUTOMATICO'
FROM (SELECT DISTINCT S.idTracto
FROM ReportesApp_Mantenimiento_Solicitud_Registro S
WHERE Estado IN ('RECEPCIONADO','PROGRAMADO','REPROGRAMADO') AND idBase NOT IN (1,2,8) AND FechaRecepcion BETWEEN '01/01/2024 00:00:00' AND GETDATE()
UNION
SELECT DISTINCT S.idTracto
FROM ReportesApp_Mantenimiento_Solicitud_Registro S
WHERE CONVERT(DATE,FechaEntrega) = CONVERT(DATE,GETDATE()) AND idBase NOT IN (1,2,8) AND CONVERT(TIME,FechaEntrega) >= '16:00:00') X

WHILE (@Contador <= (SELECT COUNT(Numero) FROM @OP_PLACAS)) BEGIN
	SET @idCondicion = (SELECT IDCondicion FROM @OP_PLACAS WHERE Numero = @Contador)
	SET @IDTracto = (SELECT IDTracto FROM @OP_PLACAS WHERE Numero = @Contador)
	SET @FInicio = (SELECT FInicio FROM @OP_PLACAS WHERE Numero = @Contador)
	SET @Usuario = (SELECT Usuario FROM @OP_PLACAS WHERE Numero = @Contador)

	EXEC ReportesApp_Operaciones_Operatividad_ActualizarViajeOtrosCarretas @IDCondicion = 13, @IDTracto = @IDTracto, @FechaInicio = @FInicio, @Usuario = @Usuario
	
	SET @Contador = @Contador + 1
END

DELETE FROM @OP_PLACAS
SET @Contador = 1

INSERT INTO @OP_PLACAS(Numero, IDCondicion, IDTracto, FInicio, Usuario)
SELECT DISTINCT ROW_NUMBER() OVER(ORDER BY P.IdUnidad ASC), 10, P.IdUnidad, CONVERT(DATE,GETDATE()), 'AUTOMATICO'
FROM ReportesApp_Operaciones_Operatividad_CarretasView P
LEFT JOIN ReportesApp_Operaciones_Operatividad_RegistroCarretas R ON P.IdUnidad = R.IdUnidad AND R.Fecha = CONVERT(DATE,GETDATE())
WHERE P.Anio = YEAR(GETDATE()) AND P.Mes = MONTH(GETDATE()) AND R.IdUnidad IS NULL

WHILE (@Contador <= (SELECT COUNT(Numero) FROM @OP_PLACAS)) BEGIN
	SET @idCondicion = (SELECT IDCondicion FROM @OP_PLACAS WHERE Numero = @Contador)
	SET @IDTracto = (SELECT IDTracto FROM @OP_PLACAS WHERE Numero = @Contador)
	SET @FInicio = (SELECT FInicio FROM @OP_PLACAS WHERE Numero = @Contador)
	SET @Usuario = (SELECT Usuario FROM @OP_PLACAS WHERE Numero = @Contador)

	EXEC ReportesApp_Operaciones_Operatividad_ActualizarViajeOtrosCarretas @IDCondicion = 10, @IDTracto = @IDTracto, @FechaInicio = @FInicio, @Usuario = @Usuario
	
	SET @Contador = @Contador + 1
END

-------------------------- EJECUTAR CARRETAS --------------------------

DECLARE @OP_PLACAS TABLE (Numero INT, IDTicket INT, Anio SMALLINT, IDTracto INT, Estado INT, TipoProgramacion INT,
FInicio DATETIME, FFin DATETIME, Usuario VARCHAR(80))
DECLARE @Contador INT = 1

INSERT INTO @OP_PLACAS(Numero, IDTicket, Anio, IDTracto, Estado, TipoProgramacion, FInicio, FFin, Usuario)
SELECT ROW_NUMBER() OVER(ORDER BY X.IdProgramacion ASC), X.IdProgramacion, X.Anio, X.idSemirremolque, X.Estado, X.TipoProgramacion,
X.FechaInicio, X.FechaFin, X.Usuario FROM
(SELECT IdProgramacion, Anio, idSemirremolque, Estado, TipoProgramacion, FechaProgramacion AS 'FechaInicio', FechaTermino AS 'FechaFin', 'AUTOMATICO' AS 'Usuario'
FROM ReportesApp_Operacion_Previaje_Registros
WHERE CONVERT(DATE,GETDATE()) BETWEEN CONVERT(DATE,FechaProgramacion) AND CONVERT(DATE,FechaTermino)  
AND TipoProgramacion IN (2,3,4,10) AND Estado IN (1,9) AND idSemirremolque != -1 AND FechaTermino IS NOT NULL
UNION
SELECT IdProgramacion, Anio, idSemirremolque, Estado, TipoProgramacion, FechaProgramacion AS 'FechaInicio', FechaFin AS 'FechaFin', 'AUTOMATICO' AS 'Usuario'
FROM ReportesApp_Operacion_Previaje_Registros
WHERE CONVERT(DATE,GETDATE()) BETWEEN CONVERT(DATE,FechaProgramacion) AND CONVERT(DATE,FechaFin)  
AND TipoProgramacion IN (2,3,4,10) AND Estado IN (1,9) AND idSemirremolque != -1 AND FechaTermino IS NULL) X

WHILE (@Contador <= (SELECT COUNT(Numero) FROM @OP_PLACAS)) BEGIN
	DECLARE @IDTicket INT = (SELECT IDTicket FROM @OP_PLACAS WHERE Numero = @Contador)
	DECLARE @Anio SMALLINT = (SELECT Anio FROM @OP_PLACAS WHERE Numero = @Contador)
	DECLARE @IDTracto INT = (SELECT IDTracto FROM @OP_PLACAS WHERE Numero = @Contador)
	DECLARE @Estado INT = (SELECT Estado FROM @OP_PLACAS WHERE Numero = @Contador)
	DECLARE @TipoProgramacion INT = (SELECT TipoProgramacion FROM @OP_PLACAS WHERE Numero = @Contador)
	DECLARE @FInicio DATETIME = (SELECT FInicio FROM @OP_PLACAS WHERE Numero = @Contador)
	DECLARE @FFin DATETIME = (SELECT FFin FROM @OP_PLACAS WHERE Numero = @Contador)
	DECLARE @Usuario VARCHAR(80) = (SELECT Usuario FROM @OP_PLACAS WHERE Numero = @Contador)

	EXEC ReportesApp_Operaciones_Operatividad_ActualizarEstadoCarretas @IDTicket = @IDTicket, @Anio = @Anio, @IDTracto = @IDTracto,
	@Estado = @Estado, @TipoProgramacion = @TipoProgramacion, @FInicio = @FInicio, @FFin = @FFin, @Usuario = @Usuario
	
	SET @Contador = @Contador + 1
END


-------------------------- TOLVAS CARRETAS --------------------------

DECLARE @OP_PLACAS TABLE (Numero INT, NroTicket INT, IDTracto INT, FInicio DATETIME, Usuario VARCHAR(80))
DECLARE @Contador INT = 1

INSERT INTO @OP_PLACAS(Numero, NroTicket, IDTracto, FInicio, Usuario)
SELECT ROW_NUMBER() OVER(ORDER BY X.NroTicket ASC), X.NroTicket, X.idCarreta, X.FechaViaje, X.Usuario FROM
(SELECT DISTINCT T.NroTicket, T.idCarreta, T.FechaViaje, 'AUTOMATICO' AS Usuario
FROM ReportesApp_Operaciones_Viajes_Tolvas T 
WHERE (EstadoViaje IN ('EJECUCION','COMPLETADO')) AND CONVERT(DATE,T.FechaViaje) >= CONVERT(DATE,GETDATE())) X

WHILE (@Contador <= (SELECT COUNT(Numero) FROM @OP_PLACAS)) BEGIN
	DECLARE @NroTicket INT = (SELECT NroTicket FROM @OP_PLACAS WHERE Numero = @Contador)
	DECLARE @IDTracto INT = (SELECT IDTracto FROM @OP_PLACAS WHERE Numero = @Contador)
	DECLARE @FInicio DATE = (SELECT FInicio FROM @OP_PLACAS WHERE Numero = @Contador)
	DECLARE @Usuario VARCHAR(80) = (SELECT Usuario FROM @OP_PLACAS WHERE Numero = @Contador)

	EXEC ReportesApp_Operaciones_Operatividad_ActualizarViajeTolvasCarretas @NroTicket = @NroTicket, @IDTracto = @IDTracto, @FechaInicio = @FInicio, @Usuario = @Usuario
	
	SET @Contador = @Contador + 1
END



