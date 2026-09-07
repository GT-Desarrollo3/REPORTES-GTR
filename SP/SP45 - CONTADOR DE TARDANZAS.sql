
-- CREAR TABLA ReportesApp_RRHH_Asistencias_JustificacionTardanza

-------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 27-06-2024
-- Description:	CREAR Y ELIMINAR MOTIVO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_RRHH_Asistencias_CrearEliminarMotivo]
@Opcion INT,
@idMotivo INT,
@idPersona INT,
@FechaIngreso DATETIME,
@Motivo VARCHAR(300),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- CREAR MOTIVO
		IF (EXISTS(SELECT * FROM ReportesApp_RRHH_Asistencias_MotivoTardanza WHERE idPersona = @idPersona AND FechaIngreso = @FechaIngreso)) BEGIN
			SET @Exito = '-1 = Esta tardanza ya tiene un motivo registrado.'
			ROLLBACK
			GOTO Terminar
		END

		SET @correlativo = (SELECT MAX(idMotivo) FROM ReportesApp_RRHH_Asistencias_MotivoTardanza)
		SET @correlativo = ISNULL(@correlativo,0) + 1

		INSERT INTO ReportesApp_RRHH_Asistencias_MotivoTardanza(idMotivo,idPersona,FechaIngreso,Motivo,UsuarioCrea,FechaCrea)
		VALUES(@correlativo,@idPersona,@FechaIngreso,@Motivo,@Usuario,GETDATE())

		SET @Exito = '0 = Motivo Ingresado Correctamente.'
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR MOTIVO
		DELETE FROM ReportesApp_RRHH_Asistencias_MotivoTardanza
		WHERE idPersona = @idPersona AND FechaIngreso = @FechaIngreso

		SET @Exito = '0 = Motivo Eliminado Correctamente.'
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

-------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		SEM CHAVEZ
-- Create date: 26-06-2024
-- Description:	LISTAR REGISTRO DE TARDANZAS
-- =============================================
/*
exec ReportesApp_RRHH_Asistencias_ListarTardanzas @Opcion=1,@FechaInicio=N'01-03-2025',@FechaFin=N'14-03-2025',@HoraInicio=N'08:05:59',@HoraFin=N'10:00:00',@Empleado=N'',@Turno=N'M'
*/
ALTER PROCEDURE [dbo].[ReportesApp_RRHH_Asistencias_ListarTardanzas]
@Opcion INT,
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@HoraInicio VARCHAR(11),
@HoraFin VARCHAR(11),
@Empleado VARCHAR(250),
@Turno VARCHAR(5)
AS
DECLARE @FINICIO DATETIME = @FechaInicio + ' 00:00'
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@Opcion = 1) BEGIN		-- LISTAR CANTIDAD DE TARDANZAS
		DECLARE @TEMP_ASIS TABLE (PERSONA INT, DNI VARCHAR(20), EMPLEADO VARCHAR(300), AREA VARCHAR(300), PUESTO VARCHAR(300), TARDANZAS INT)
		
		INSERT @TEMP_ASIS (PERSONA, DNI, EMPLEADO, AREA, PUESTO, TARDANZAS)
		(SELECT X.PERSONA, X.DNI, X.EMPLEADO, X.AREA, X.PUESTO, COUNT(X.HORA_LLEGADA) AS 'TARDANZAS'
		FROM (SELECT DISTINCT E.Empleado AS 'PERSONA', LTRIM(RTRIM(p.Documento)) AS 'DNI', RTRIM(P.NombreCompleto) AS 'EMPLEADO', dep.Description AS 'AREA',
		 LTRIM(RTRIM(puesto.Descripcion)) AS 'PUESTO', FORMAT(AD.[time], 'dd/MM/yyyy HH:mm') AS 'HORA_LLEGADA', MT.Motivo
		FROM [ZKAccess]..acc_monitor_log AD 
		LEFT JOIN EmpleadoMast E ON AD.pin=E.Empleado
		LEFT JOIN PersonaMast P ON E.Empleado = P.Persona
		LEFT JOIN hr_puestoempresa puesto on puesto.CodigoPuesto = e.CodigoCargo
		LEFT JOIN departmentmst dep on dep.Department = e.DeptoOrganizacion
		LEFT JOIN ReportesApp_RRHH_Asistencias_MotivoTardanza MT ON MT.idPersona = E.Empleado AND CONVERT(DATETIME,FORMAT(AD.[time], 'dd/MM/yyyy HH:mm')) = MT.FechaIngreso
		WHERE (E.Estado = 'A') /*AND AD.ID not in (61251685)*/ AND (MT.Motivo IS NULL) AND (CAST(AD.[time] AS DATE) BETWEEN @FINICIO AND @FFIN) 
		AND  (CONVERT(TIME,AD.[time]) >= CONVERT(TIME,@HoraInicio)) AND (CONVERT(TIME,AD.[time]) <= CONVERT(TIME,@HoraFin))
		AND (ad.[time] NOT IN ('19-05-2025 08:17:04.000','17-05-2025 08:15:45.000','05-05-2025 08:15:43.000','30-04-2025 08:23:36.000','29-04-2025 08:17:02.000','28-04-2025 08:45:50.000','25-04-2025 08:32:29.000','24-04-2025 08:20:18.000','23-04-2025 08:16:04.000','15-04-2025 08:20:06.000','12-04-2025 08:19:04.000','11-04-2025 08:21:29.000','10-04-2024 08:22:09.000','09-04-2025 08:20:58.000','07-04-2025 08:18:56.000','25-03-2025 08:15:33.000','22-03-2025 08:07:00.000','19-03-2025 08:25:32.000','13-03-2025 08:11:07.000','06-03-2025 08:13:33.000','05-03-2025 08:19:57.000','03-03-2025 08:06:23.000'))
		AND (RTRIM(P.NombreCompleto) IS NULL OR RTRIM(P.NombreCompleto) LIKE '%' + @Empleado + '%') AND (E.Empleado != 0)) X
		GROUP BY X.PERSONA, X.DNI, X.EMPLEADO, X.AREA, X.PUESTO)
		
		INSERT @TEMP_ASIS (PERSONA, DNI, EMPLEADO, AREA, PUESTO, TARDANZAS)
		(SELECT X.PERSONA, X.DNI, X.EMPLEADO, X.AREA, X.PUESTO, COUNT(X.HORA_LLEGADA) AS 'TARDANZAS'
		FROM (SELECT DISTINCT E.Empleado AS 'PERSONA', LTRIM(RTRIM(p.Documento)) AS 'DNI', RTRIM(AE.NombreCompleto) AS 'EMPLEADO', dep.Description AS 'AREA',
		LTRIM(RTRIM(puesto.Descripcion)) AS 'PUESTO', FORMAT(AE.FechaServidor, 'dd/MM/yyyy HH:mm') AS 'HORA_LLEGADA', MT.Motivo
		FROM ReportesApp_RRHH_AsistenciaExterna AE
		LEFT JOIN EmpleadoMast E ON AE.IdPersona = E.Empleado
		LEFT JOIN PersonaMast P ON E.Empleado = P.Persona
		LEFT JOIN hr_puestoempresa puesto on puesto.CodigoPuesto = e.CodigoCargo
		LEFT JOIN departmentmst dep on dep.Department = e.DeptoOrganizacion
		LEFT JOIN ReportesApp_RRHH_Asistencias_MotivoTardanza MT ON MT.idPersona = E.Empleado AND CONVERT(DATETIME,FORMAT(AE.FechaServidor, 'dd/MM/yyyy HH:mm')) = MT.FechaIngreso
		WHERE (E.Estado = 'A') AND (MT.Motivo IS NULL) AND (AE.TipoRegistro = 'INGRESO') AND (CAST(AE.FechaServidor AS DATE) BETWEEN @FINICIO AND @FFIN)
		AND (CONVERT(TIME,AE.FechaServidor) >= CONVERT(TIME,@HoraInicio)) AND (CONVERT(TIME,AE.FechaServidor) <= CONVERT(TIME,@HoraFin))
		AND (RTRIM(AE.NombreCompleto) IS NULL OR RTRIM(AE.NombreCompleto) LIKE '%' + @Empleado + '%') AND (E.Empleado != 0)) X
		GROUP BY X.PERSONA, X.DNI, X.EMPLEADO, X.AREA, X.PUESTO)
		
		SELECT A.PERSONA, A.DNI, A.EMPLEADO, A.AREA, A.PUESTO, SUM(A.TARDANZAS) AS 'TARDANZAS'
		FROM @TEMP_ASIS A
		GROUP BY A.PERSONA, A.DNI, A.EMPLEADO, A.AREA, A.PUESTO
		ORDER BY A.EMPLEADO ASC
	END

	IF (@Opcion = 2) BEGIN		-- LISTAR TARDANZAS POR USUARIO
		DECLARE @TEMP_ASIS2 TABLE (PERSONA INT, DNI VARCHAR(20), EMPLEADO VARCHAR(300), HORA_LLEGADA VARCHAR(300))
		
		INSERT INTO @TEMP_ASIS2 (PERSONA, DNI, EMPLEADO, HORA_LLEGADA)
		SELECT DISTINCT E.Empleado AS 'PERSONA', LTRIM(RTRIM(p.Documento)) AS 'DNI', RTRIM(P.NombreCompleto) AS 'EMPLEADO',
		FORMAT(AD.[time], 'dd/MM/yyyy HH:mm') AS 'HORA_LLEGADA'
		FROM [ZKAccess]..acc_monitor_log AD 
		LEFT JOIN EmpleadoMast E ON AD.pin=E.Empleado
		LEFT JOIN PersonaMast P ON E.Empleado = P.Persona
		LEFT JOIN hr_puestoempresa puesto on puesto.CodigoPuesto = e.CodigoCargo
		LEFT JOIN departmentmst dep on dep.Department = e.DeptoOrganizacion
		WHERE (E.Estado = 'A') AND (CAST(AD.[time] AS DATE) BETWEEN @FINICIO AND @FFIN) AND (E.Empleado != 0)
		AND (CONVERT(TIME,AD.[time]) >= CONVERT(TIME,@HoraInicio)) AND (CONVERT(TIME,AD.[time]) <= CONVERT(TIME,@HoraFin))
		AND (RTRIM(P.NombreCompleto) LIKE '%' + @Empleado + '%') AND ad.[time] NOT IN ('19-05-2025 08:17:04.000','17-05-2025 08:15:45.000','05-05-2025 08:15:43.000','30-04-2025 08:23:36.000','29-04-2025 08:17:02.000','28-04-2025 08:45:50.000','25-04-2025 08:32:29.000','24-04-2025 08:20:18.000','23-04-2025 08:16:04.000','15-04-2025 08:20:06.000','12-04-2025 08:19:04.000','11-04-2025 08:21:29.000','10-04-2024 08:22:09.000','09-04-2025 08:20:58.000','07-04-2025 08:18:56.000','25-03-2025 08:15:33.000','22-03-2025 08:07:00.000','19-03-2025 08:25:32.000','13-03-2025 08:11:07.000','06-03-2025 08:13:33.000','05-03-2025 08:19:57.000','03-03-2025 08:06:23.000') AND AD.ID not in (61251685)
		ORDER BY RTRIM(P.NombreCompleto) ASC

		INSERT INTO @TEMP_ASIS2 (PERSONA, DNI, EMPLEADO, HORA_LLEGADA)
		SELECT DISTINCT E.Empleado AS 'PERSONA', LTRIM(RTRIM(p.Documento)) AS 'DNI', RTRIM(AE.NombreCompleto) AS 'EMPLEADO',
		FORMAT(AE.FechaServidor, 'dd/MM/yyyy HH:mm') AS 'HORA_LLEGADA'
		FROM ReportesApp_RRHH_AsistenciaExterna AE
		LEFT JOIN EmpleadoMast E ON AE.IdPersona = E.Empleado
		LEFT JOIN PersonaMast P ON E.Empleado = P.Persona
		LEFT JOIN hr_puestoempresa puesto on puesto.CodigoPuesto = e.CodigoCargo
		LEFT JOIN departmentmst dep on dep.Department = e.DeptoOrganizacion
		WHERE (E.Estado = 'A') AND (CAST(AE.FechaServidor AS DATE) BETWEEN @FINICIO AND @FFIN) AND (E.Empleado != 0)
		AND (CONVERT(TIME,AE.FechaServidor) >= CONVERT(TIME,@HoraInicio)) AND (CONVERT(TIME,AE.FechaServidor) <= CONVERT(TIME,@HoraFin))
		AND (RTRIM(AE.NombreCompleto) LIKE '%' + @Empleado + '%')
		ORDER BY RTRIM(AE.NombreCompleto) ASC

		SELECT S.*, MT.Motivo AS 'MOTIVO'
		FROM @TEMP_ASIS2 S
		LEFT JOIN ReportesApp_RRHH_Asistencias_MotivoTardanza MT ON MT.idPersona = S.PERSONA AND CONVERT(DATETIME,S.HORA_LLEGADA) = MT.FechaIngreso
		ORDER BY CONVERT(DATETIME,S.HORA_LLEGADA) DESC
	END
END

----------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		SEM CHAVEZ
-- Create date: 22-05-2025
-- Description:	EXPORTAR REGISTRO DE TARDANZAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_RRHH_Asistencias_ListarTardanzasExcel]
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@HoraInicio VARCHAR(11),
@HoraFin VARCHAR(11),
@Empleado VARCHAR(250)
AS
DECLARE @FINICIO DATETIME = @FechaInicio + ' 00:00'
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	DECLARE @TEMP_ASIS TABLE (PERSONA INT, DNI VARCHAR(20), EMPLEADO VARCHAR(300), AREA VARCHAR(300), PUESTO VARCHAR(300), TARDANZAS INT)

	INSERT @TEMP_ASIS (PERSONA, DNI, EMPLEADO, AREA, PUESTO, TARDANZAS)
	(SELECT X.PERSONA, X.DNI, X.EMPLEADO, X.AREA, X.PUESTO, COUNT(X.HORA_LLEGADA) AS 'TARDANZAS'
	FROM (SELECT DISTINCT E.Empleado AS 'PERSONA', LTRIM(RTRIM(p.Documento)) AS 'DNI', RTRIM(P.NombreCompleto) AS 'EMPLEADO', dep.Description AS 'AREA',
		LTRIM(RTRIM(puesto.Descripcion)) AS 'PUESTO', FORMAT(AD.[time], 'dd/MM/yyyy HH:mm') AS 'HORA_LLEGADA', MT.Motivo
	FROM [ZKAccess]..acc_monitor_log AD 
	LEFT JOIN EmpleadoMast E ON AD.pin=E.Empleado
	LEFT JOIN PersonaMast P ON E.Empleado = P.Persona
	LEFT JOIN hr_puestoempresa puesto on puesto.CodigoPuesto = e.CodigoCargo
	LEFT JOIN departmentmst dep on dep.Department = e.DeptoOrganizacion
	LEFT JOIN ReportesApp_RRHH_Asistencias_MotivoTardanza MT ON MT.idPersona = E.Empleado AND CONVERT(DATETIME,FORMAT(AD.[time], 'dd/MM/yyyy HH:mm')) = MT.FechaIngreso
	WHERE (E.Estado = 'A') /*AND AD.ID not in (61251685)*/ AND (MT.Motivo IS NULL) AND (CAST(AD.[time] AS DATE) BETWEEN @FINICIO AND @FFIN) 
	AND  (CONVERT(TIME,AD.[time]) >= CONVERT(TIME,@HoraInicio)) AND (CONVERT(TIME,AD.[time]) <= CONVERT(TIME,@HoraFin))
	AND (ad.[time] NOT IN ('19-05-2025 08:17:04.000','17-05-2025 08:15:45.000','05-05-2025 08:15:43.000','30-04-2025 08:23:36.000','29-04-2025 08:17:02.000','28-04-2025 08:45:50.000','25-04-2025 08:32:29.000','24-04-2025 08:20:18.000','23-04-2025 08:16:04.000','15-04-2025 08:20:06.000','12-04-2025 08:19:04.000','11-04-2025 08:21:29.000','10-04-2024 08:22:09.000','09-04-2025 08:20:58.000','07-04-2025 08:18:56.000','25-03-2025 08:15:33.000','22-03-2025 08:07:00.000','19-03-2025 08:25:32.000','13-03-2025 08:11:07.000','06-03-2025 08:13:33.000','05-03-2025 08:19:57.000','03-03-2025 08:06:23.000'))
	AND (RTRIM(P.NombreCompleto) IS NULL OR RTRIM(P.NombreCompleto) LIKE '%' + @Empleado + '%') AND (E.Empleado != 0)) X
	GROUP BY X.PERSONA, X.DNI, X.EMPLEADO, X.AREA, X.PUESTO)

	INSERT @TEMP_ASIS (PERSONA, DNI, EMPLEADO, AREA, PUESTO, TARDANZAS)
	(SELECT X.PERSONA, X.DNI, X.EMPLEADO, X.AREA, X.PUESTO, COUNT(X.HORA_LLEGADA) AS 'TARDANZAS'
	FROM (SELECT DISTINCT E.Empleado AS 'PERSONA', LTRIM(RTRIM(p.Documento)) AS 'DNI', RTRIM(AE.NombreCompleto) AS 'EMPLEADO', dep.Description AS 'AREA',
	LTRIM(RTRIM(puesto.Descripcion)) AS 'PUESTO', FORMAT(AE.FechaServidor, 'dd/MM/yyyy HH:mm') AS 'HORA_LLEGADA', MT.Motivo
	FROM ReportesApp_RRHH_AsistenciaExterna AE
	LEFT JOIN EmpleadoMast E ON AE.IdPersona = E.Empleado
	LEFT JOIN PersonaMast P ON E.Empleado = P.Persona
	LEFT JOIN hr_puestoempresa puesto on puesto.CodigoPuesto = e.CodigoCargo
	LEFT JOIN departmentmst dep on dep.Department = e.DeptoOrganizacion
	LEFT JOIN ReportesApp_RRHH_Asistencias_MotivoTardanza MT ON MT.idPersona = E.Empleado AND CONVERT(DATETIME,FORMAT(AE.FechaServidor, 'dd/MM/yyyy HH:mm')) = MT.FechaIngreso
	WHERE (E.Estado = 'A') AND (MT.Motivo IS NULL) AND (AE.TipoRegistro = 'INGRESO') AND (CAST(AE.FechaServidor AS DATE) BETWEEN @FINICIO AND @FFIN)
	AND (CONVERT(TIME,AE.FechaServidor) >= CONVERT(TIME,@HoraInicio)) AND (CONVERT(TIME,AE.FechaServidor) <= CONVERT(TIME,@HoraFin))
	AND (RTRIM(AE.NombreCompleto) IS NULL OR RTRIM(AE.NombreCompleto) LIKE '%' + @Empleado + '%') AND (E.Empleado != 0)) X
	GROUP BY X.PERSONA, X.DNI, X.EMPLEADO, X.AREA, X.PUESTO)

	DECLARE @TEMP_ASIS2 TABLE (PERSONA INT, DNI VARCHAR(20), EMPLEADO VARCHAR(300), HORA_LLEGADA VARCHAR(300))

	INSERT INTO @TEMP_ASIS2 (PERSONA, DNI, EMPLEADO, HORA_LLEGADA)
	SELECT DISTINCT E.Empleado AS 'PERSONA', LTRIM(RTRIM(p.Documento)) AS 'DNI', RTRIM(P.NombreCompleto) AS 'EMPLEADO',
	FORMAT(AD.[time], 'dd/MM/yyyy HH:mm') AS 'HORA_LLEGADA'
	FROM [ZKAccess]..acc_monitor_log AD 
	LEFT JOIN EmpleadoMast E ON AD.pin=E.Empleado
	LEFT JOIN PersonaMast P ON E.Empleado = P.Persona
	LEFT JOIN hr_puestoempresa puesto on puesto.CodigoPuesto = e.CodigoCargo
	LEFT JOIN departmentmst dep on dep.Department = e.DeptoOrganizacion
	WHERE (E.Estado = 'A') AND (CAST(AD.[time] AS DATE) BETWEEN @FINICIO AND @FFIN) AND (E.Empleado != 0)
	AND (RTRIM(P.NombreCompleto) IS NULL OR RTRIM(P.NombreCompleto) LIKE '%' + @Empleado + '%')
	AND (CONVERT(TIME,AD.[time]) >= CONVERT(TIME,@HoraInicio)) AND (CONVERT(TIME,AD.[time]) <= CONVERT(TIME,@HoraFin))
	AND ad.[time] NOT IN ('19-05-2025 08:17:04.000','17-05-2025 08:15:45.000','05-05-2025 08:15:43.000','30-04-2025 08:23:36.000','29-04-2025 08:17:02.000','28-04-2025 08:45:50.000','25-04-2025 08:32:29.000','24-04-2025 08:20:18.000','23-04-2025 08:16:04.000','15-04-2025 08:20:06.000','12-04-2025 08:19:04.000','11-04-2025 08:21:29.000','10-04-2024 08:22:09.000','09-04-2025 08:20:58.000','07-04-2025 08:18:56.000','25-03-2025 08:15:33.000','22-03-2025 08:07:00.000','19-03-2025 08:25:32.000','13-03-2025 08:11:07.000','06-03-2025 08:13:33.000','05-03-2025 08:19:57.000','03-03-2025 08:06:23.000') AND AD.ID not in (61251685)
	ORDER BY RTRIM(P.NombreCompleto) ASC

	INSERT INTO @TEMP_ASIS2 (PERSONA, DNI, EMPLEADO, HORA_LLEGADA)
	SELECT DISTINCT E.Empleado AS 'PERSONA', LTRIM(RTRIM(p.Documento)) AS 'DNI', RTRIM(AE.NombreCompleto) AS 'EMPLEADO',
	FORMAT(AE.FechaServidor, 'dd/MM/yyyy HH:mm') AS 'HORA_LLEGADA'
	FROM ReportesApp_RRHH_AsistenciaExterna AE
	LEFT JOIN EmpleadoMast E ON AE.IdPersona = E.Empleado
	LEFT JOIN PersonaMast P ON E.Empleado = P.Persona
	LEFT JOIN hr_puestoempresa puesto on puesto.CodigoPuesto = e.CodigoCargo
	LEFT JOIN departmentmst dep on dep.Department = e.DeptoOrganizacion
	WHERE (E.Estado = 'A') AND (CAST(AE.FechaServidor AS DATE) BETWEEN @FINICIO AND @FFIN) AND (E.Empleado != 0)
	AND (RTRIM(AE.NombreCompleto) IS NULL OR RTRIM(AE.NombreCompleto) LIKE '%' + @Empleado + '%')
	AND (CONVERT(TIME,AE.FechaServidor) >= CONVERT(TIME,@HoraInicio)) AND (CONVERT(TIME,AE.FechaServidor) <= CONVERT(TIME,@HoraFin))
	ORDER BY RTRIM(AE.NombreCompleto) ASC

	SELECT A.PERSONA, A.EMPLEADO, CONVERT(VARCHAR,E.FechaIngreso,103) AS 'FECHA_INGRESO', A.AREA, A.PUESTO, SUM(A.TARDANZAS) AS 'TARDANZAS', S2.HORA_LLEGADA
	FROM @TEMP_ASIS A
	LEFT JOIN @TEMP_ASIS2 S2 ON S2.PERSONA = A.PERSONA
	LEFT JOIN ReportesApp_RRHH_Asistencias_MotivoTardanza MT ON MT.idPersona = S2.PERSONA AND CONVERT(DATETIME,S2.HORA_LLEGADA) = MT.FechaIngreso
	LEFT JOIN EmpleadoMast E ON E.Empleado = A.PERSONA
	WHERE MT.Motivo IS NULL
	GROUP BY A.PERSONA, A.DNI, A.EMPLEADO, E.FechaIngreso, A.AREA, A.PUESTO, S2.HORA_LLEGADA
	ORDER BY A.EMPLEADO, CONVERT(DATETIME,S2.HORA_LLEGADA) ASC
END

