
-- CREAR TABLA ReportesApp_Operaciones_PendientesDiarios_CorreosPersonal Y LLENARLA

-- CREAR TABLA ReportesApp_Operaciones_PendientesDiarios_Actividades

-------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 12-03-2024
-- Description:	INSERTAR PENDIENTES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_PendientesDiarios_InsertarActividad]
@Persona INT,
@Descripcion VARCHAR(350),
@Nivel VARCHAR(20),
@FechaInicio DATETIME,
@FProyectada1 DATETIME,
@Seguimiento VARCHAR(350),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

SET @Exito = '0 = Actividad Registrada.'

IF (@FechaInicio > @FProyectada1)
BEGIN
	SET @Exito = '-10 = La Fecha de Inicio no puede ser mayor a la Fecha Proyectada.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	SET @correlativo = (SELECT MAX(idActividad) FROM ReportesApp_Operaciones_PendientesDiarios_Actividades)
	SET @correlativo = ISNULL(@correlativo,0) + 1

    INSERT INTO ReportesApp_Operaciones_PendientesDiarios_Actividades(idActividad, Descripcion, Persona, Nivel, Estado, FechaInicio, Contador, FProyectada1,
				FProyectada2, FProyectada3, Dias, Seguimiento, UsuarioCreacion, FechaCreacion, UsuarioModificacion, FechaModificacion)
	VALUES(@correlativo, @Descripcion, @Persona, @Nivel, 'PENDIENTE', @FechaInicio, 1, @FProyectada1, @FProyectada1, @FProyectada1,
	DATEDIFF(DAY,@FechaInicio, @FProyectada1), @Seguimiento, @Usuario, GETDATE(), @Usuario, GETDATE())
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

--------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: (09-04-2024)
-- Description:	LISTAR PENDIENTES DIARIOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_PendientesDiarios_ListarActividades]
@FechaP INT,
@Responsable VARCHAR(250),
@Area VARCHAR(50),
@Estado VARCHAR(50),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	UPDATE PD
	SET PD.Dias = DATEDIFF(DAY,GETDATE(),PD.FProyectada1)
	FROM ReportesApp_Operaciones_PendientesDiarios_Actividades PD
	WHERE (PD.Estado = 'PENDIENTE' OR PD.Estado = 'EN PROCESO') AND PD.Contador = 1 AND CONVERT(DATE,GETDATE()) >= CONVERT(DATE,PD.FechaInicio)

	UPDATE PD
	SET PD.Dias = DATEDIFF(DAY,GETDATE(),PD.FProyectada2)
	FROM ReportesApp_Operaciones_PendientesDiarios_Actividades PD
	WHERE (PD.Estado = 'PENDIENTE' OR PD.Estado = 'EN PROCESO') AND PD.Contador = 2 AND CONVERT(DATE,GETDATE()) >= CONVERT(DATE,PD.FechaInicio)

	UPDATE PD
	SET PD.Dias = DATEDIFF(DAY,GETDATE(),PD.FProyectada3)
	FROM ReportesApp_Operaciones_PendientesDiarios_Actividades PD
	WHERE (PD.Estado = 'PENDIENTE' OR PD.Estado = 'EN PROCESO') AND PD.Contador = 3 AND CONVERT(DATE,GETDATE()) >= CONVERT(DATE,PD.FechaInicio)

	IF (@FechaP = 0) BEGIN		-- SIN FECHA
		IF (@Estado = 'TODOS') BEGIN
			IF (@Area = 'TODAS') BEGIN
				SELECT PD.idActividad AS 'NRO', PD.Descripcion AS 'ACTIVIDAD', PD.Persona, LTRIM(RTRIM(P.NombreCompleto)) AS 'PERSONAL',
				A1.Description AS 'AREA', LTRIM(RTRIM(C1.Descripcion)) AS 'CARGO', PD.Nivel AS 'NIVEL', PD.FechaInicio AS 'FECHA_INICIO',
				(CASE WHEN PD.Contador = 1 THEN PD.FProyectada1 WHEN PD.Contador = 2 THEN PD.FProyectada2 WHEN PD.Contador = 3 THEN PD.FProyectada3 END)
				AS 'FECHA_PROYECTADA', PD.Contador - 1 AS 'Reprog', PD.Dias AS 'DÍAS', PD.Estado AS 'ESTADO', PD.Seguimiento AS 'SEGUIMIENTO', PD.UsuarioCreacion,
				PD.FechaCreacion, PD.UsuarioModificacion, PD.FechaModificacion
				FROM ReportesApp_Operaciones_PendientesDiarios_Actividades PD
				LEFT JOIN PersonaMast P ON P.Persona = PD.Persona
				LEFT JOIN EmpleadoMast E ON P.Persona = E.Empleado
				LEFT JOIN departmentmst A1 ON A1.Department = E.DeptoOrganizacion
				LEFT JOIN hr_puestoempresa C1 ON C1.CodigoPuesto = E.CodigoCargo
				WHERE (@Responsable IS NULL OR LTRIM(RTRIM(P.NombreCompleto)) LIKE '%' + @Responsable + '%')
				ORDER BY PD.FechaInicio DESC
			END
			ELSE BEGIN
				SELECT PD.idActividad AS 'NRO', PD.Descripcion AS 'ACTIVIDAD', PD.Persona, LTRIM(RTRIM(P.NombreCompleto)) AS 'PERSONAL',
				A1.Description AS 'AREA', LTRIM(RTRIM(C1.Descripcion)) AS 'CARGO', PD.Nivel AS 'NIVEL', PD.FechaInicio AS 'FECHA_INICIO',
				(CASE WHEN PD.Contador = 1 THEN PD.FProyectada1 WHEN PD.Contador = 2 THEN PD.FProyectada2 WHEN PD.Contador = 3 THEN PD.FProyectada3 END)
				AS 'FECHA_PROYECTADA', PD.Contador - 1 AS 'Reprog', PD.Dias AS 'DÍAS', PD.Estado AS 'ESTADO', PD.Seguimiento AS 'SEGUIMIENTO', PD.UsuarioCreacion, 
				PD.FechaCreacion, PD.UsuarioModificacion, PD.FechaModificacion
				FROM ReportesApp_Operaciones_PendientesDiarios_Actividades PD
				LEFT JOIN PersonaMast P ON P.Persona = PD.Persona
				LEFT JOIN EmpleadoMast E ON P.Persona = E.Empleado
				LEFT JOIN departmentmst A1 ON A1.Department = E.DeptoOrganizacion
				LEFT JOIN hr_puestoempresa C1 ON C1.CodigoPuesto = E.CodigoCargo
				WHERE (@Responsable IS NULL OR LTRIM(RTRIM(P.NombreCompleto)) LIKE '%' + @Responsable + '%') AND (@Area = A1.Description)
				ORDER BY PD.FechaInicio DESC
			END
		END
		ELSE BEGIN
			IF (@Area = 'TODAS') BEGIN
				SELECT PD.idActividad AS 'NRO', PD.Descripcion AS 'ACTIVIDAD', PD.Persona, LTRIM(RTRIM(P.NombreCompleto)) AS 'PERSONAL',
				A1.Description AS 'AREA', LTRIM(RTRIM(C1.Descripcion)) AS 'CARGO', PD.Nivel AS 'NIVEL', PD.FechaInicio AS 'FECHA_INICIO',
				(CASE WHEN PD.Contador = 1 THEN PD.FProyectada1 WHEN PD.Contador = 2 THEN PD.FProyectada2 WHEN PD.Contador = 3 THEN PD.FProyectada3 END)
				AS 'FECHA_PROYECTADA', PD.Contador - 1  AS 'Reprog', PD.Dias AS 'DÍAS', PD.Estado AS 'ESTADO', PD.Seguimiento AS 'SEGUIMIENTO', PD.UsuarioCreacion,
				PD.FechaCreacion, PD.UsuarioModificacion, PD.FechaModificacion
				FROM ReportesApp_Operaciones_PendientesDiarios_Actividades PD
				LEFT JOIN PersonaMast P ON P.Persona = PD.Persona
				LEFT JOIN EmpleadoMast E ON P.Persona = E.Empleado
				LEFT JOIN departmentmst A1 ON A1.Department = E.DeptoOrganizacion
				LEFT JOIN hr_puestoempresa C1 ON C1.CodigoPuesto = E.CodigoCargo
				WHERE (@Responsable IS NULL OR LTRIM(RTRIM(P.NombreCompleto)) LIKE '%' + @Responsable + '%')
				AND (@Estado = PD.Estado)
				ORDER BY PD.FechaInicio DESC
			END
			ELSE BEGIN
				SELECT PD.idActividad AS 'NRO', PD.Descripcion AS 'ACTIVIDAD', PD.Persona, LTRIM(RTRIM(P.NombreCompleto)) AS 'PERSONAL',
				A1.Description AS 'AREA', LTRIM(RTRIM(C1.Descripcion)) AS 'CARGO', PD.Nivel AS 'NIVEL', PD.FechaInicio AS 'FECHA_INICIO',
				(CASE WHEN PD.Contador = 1 THEN PD.FProyectada1 WHEN PD.Contador = 2 THEN PD.FProyectada2 WHEN PD.Contador = 3 THEN PD.FProyectada3 END)
				AS 'FECHA_PROYECTADA', PD.Contador - 1 AS 'Reprog', PD.Dias AS 'DÍAS', PD.Estado AS 'ESTADO', PD.Seguimiento AS 'SEGUIMIENTO', PD.UsuarioCreacion,
				PD.FechaCreacion, PD.UsuarioModificacion, PD.FechaModificacion
				FROM ReportesApp_Operaciones_PendientesDiarios_Actividades PD
				LEFT JOIN PersonaMast P ON P.Persona = PD.Persona
				LEFT JOIN EmpleadoMast E ON P.Persona = E.Empleado
				LEFT JOIN departmentmst A1 ON A1.Department = E.DeptoOrganizacion
				LEFT JOIN hr_puestoempresa C1 ON C1.CodigoPuesto = E.CodigoCargo
				WHERE (@Responsable IS NULL OR LTRIM(RTRIM(P.NombreCompleto)) LIKE '%' + @Responsable + '%') AND (@Area = A1.Description) AND (@Estado = PD.Estado)
				ORDER BY PD.FechaInicio DESC
			END
		END
	END
	ELSE BEGIN
		IF (@Estado = 'TODOS') BEGIN
			IF (@Area = 'TODAS') BEGIN
				SELECT PD.idActividad AS 'NRO', PD.Descripcion AS 'ACTIVIDAD', PD.Persona, LTRIM(RTRIM(P.NombreCompleto)) AS 'PERSONAL',
				A1.Description AS 'AREA', LTRIM(RTRIM(C1.Descripcion)) AS 'CARGO', PD.Nivel AS 'NIVEL', PD.FechaInicio AS 'FECHA_INICIO',
				(CASE WHEN PD.Contador = 1 THEN PD.FProyectada1 WHEN PD.Contador = 2 THEN PD.FProyectada2 WHEN PD.Contador = 3 THEN PD.FProyectada3 END)
				AS 'FECHA_PROYECTADA', PD.Contador - 1 AS 'Reprog', PD.Dias AS 'DÍAS', PD.Estado AS 'ESTADO', PD.Seguimiento AS 'SEGUIMIENTO', PD.UsuarioCreacion,
				PD.FechaCreacion, PD.UsuarioModificacion, PD.FechaModificacion
				FROM ReportesApp_Operaciones_PendientesDiarios_Actividades PD
				LEFT JOIN PersonaMast P ON P.Persona = PD.Persona
				LEFT JOIN EmpleadoMast E ON P.Persona = E.Empleado
				LEFT JOIN departmentmst A1 ON A1.Department = E.DeptoOrganizacion
				LEFT JOIN hr_puestoempresa C1 ON C1.CodigoPuesto = E.CodigoCargo
				WHERE (@Responsable IS NULL OR LTRIM(RTRIM(P.NombreCompleto)) LIKE '%' + @Responsable + '%') AND (PD.FechaInicio BETWEEN @FINICIO AND @FFIN)
				ORDER BY PD.FechaInicio DESC
			END
			ELSE BEGIN
				SELECT PD.idActividad AS 'NRO', PD.Descripcion AS 'ACTIVIDAD', PD.Persona, LTRIM(RTRIM(P.NombreCompleto)) AS 'PERSONAL',
				A1.Description AS 'AREA', LTRIM(RTRIM(C1.Descripcion)) AS 'CARGO', PD.Nivel AS 'NIVEL', PD.FechaInicio AS 'FECHA_INICIO',
				(CASE WHEN PD.Contador = 1 THEN PD.FProyectada1 WHEN PD.Contador = 2 THEN PD.FProyectada2 WHEN PD.Contador = 3 THEN PD.FProyectada3 END)
				AS 'FECHA_PROYECTADA', PD.Contador - 1 AS 'Reprog', PD.Dias AS 'DÍAS', PD.Estado AS 'ESTADO', PD.Seguimiento AS 'SEGUIMIENTO', PD.UsuarioCreacion, 
				PD.FechaCreacion, PD.UsuarioModificacion, PD.FechaModificacion
				FROM ReportesApp_Operaciones_PendientesDiarios_Actividades PD
				LEFT JOIN PersonaMast P ON P.Persona = PD.Persona
				LEFT JOIN EmpleadoMast E ON P.Persona = E.Empleado
				LEFT JOIN departmentmst A1 ON A1.Department = E.DeptoOrganizacion
				LEFT JOIN hr_puestoempresa C1 ON C1.CodigoPuesto = E.CodigoCargo
				WHERE (@Responsable IS NULL OR LTRIM(RTRIM(P.NombreCompleto)) LIKE '%' + @Responsable + '%') AND (@Area = A1.Description)
				AND (PD.FechaInicio BETWEEN @FINICIO AND @FFIN)
				ORDER BY PD.FechaInicio DESC
			END
		END
		ELSE BEGIN
			IF (@Area = 'TODAS') BEGIN
				SELECT PD.idActividad AS 'NRO', PD.Descripcion AS 'ACTIVIDAD', PD.Persona, LTRIM(RTRIM(P.NombreCompleto)) AS 'PERSONAL',
				A1.Description AS 'AREA', LTRIM(RTRIM(C1.Descripcion)) AS 'CARGO', PD.Nivel AS 'NIVEL', PD.FechaInicio AS 'FECHA_INICIO',
				(CASE WHEN PD.Contador = 1 THEN PD.FProyectada1 WHEN PD.Contador = 2 THEN PD.FProyectada2 WHEN PD.Contador = 3 THEN PD.FProyectada3 END)
				AS 'FECHA_PROYECTADA', PD.Contador - 1  AS 'Reprog', PD.Dias AS 'DÍAS', PD.Estado AS 'ESTADO', PD.Seguimiento AS 'SEGUIMIENTO', PD.UsuarioCreacion,
				PD.FechaCreacion, PD.UsuarioModificacion, PD.FechaModificacion
				FROM ReportesApp_Operaciones_PendientesDiarios_Actividades PD
				LEFT JOIN PersonaMast P ON P.Persona = PD.Persona
				LEFT JOIN EmpleadoMast E ON P.Persona = E.Empleado
				LEFT JOIN departmentmst A1 ON A1.Department = E.DeptoOrganizacion
				LEFT JOIN hr_puestoempresa C1 ON C1.CodigoPuesto = E.CodigoCargo
				WHERE (@Responsable IS NULL OR LTRIM(RTRIM(P.NombreCompleto)) LIKE '%' + @Responsable + '%') AND (PD.FechaInicio BETWEEN @FINICIO AND @FFIN)
				AND (@Estado = PD.Estado)
				ORDER BY PD.FechaInicio DESC
			END
			ELSE BEGIN
				SELECT PD.idActividad AS 'NRO', PD.Descripcion AS 'ACTIVIDAD', PD.Persona, LTRIM(RTRIM(P.NombreCompleto)) AS 'PERSONAL',
				A1.Description AS 'AREA', LTRIM(RTRIM(C1.Descripcion)) AS 'CARGO', PD.Nivel AS 'NIVEL', PD.FechaInicio AS 'FECHA_INICIO',
				(CASE WHEN PD.Contador = 1 THEN PD.FProyectada1 WHEN PD.Contador = 2 THEN PD.FProyectada2 WHEN PD.Contador = 3 THEN PD.FProyectada3 END)
				AS 'FECHA_PROYECTADA', PD.Contador - 1 AS 'Reprog', PD.Dias AS 'DÍAS', PD.Estado AS 'ESTADO', PD.Seguimiento AS 'SEGUIMIENTO', PD.UsuarioCreacion,
				PD.FechaCreacion, PD.UsuarioModificacion, PD.FechaModificacion
				FROM ReportesApp_Operaciones_PendientesDiarios_Actividades PD
				LEFT JOIN PersonaMast P ON P.Persona = PD.Persona
				LEFT JOIN EmpleadoMast E ON P.Persona = E.Empleado
				LEFT JOIN departmentmst A1 ON A1.Department = E.DeptoOrganizacion
				LEFT JOIN hr_puestoempresa C1 ON C1.CodigoPuesto = E.CodigoCargo
				WHERE (@Responsable IS NULL OR LTRIM(RTRIM(P.NombreCompleto)) LIKE '%' + @Responsable + '%') AND (@Area = A1.Description)
				AND (@Estado = PD.Estado) AND (PD.FechaInicio BETWEEN @FINICIO AND @FFIN)
				ORDER BY PD.FechaInicio DESC
			END
		END
	END
END

---------------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 12-03-2024
-- Description:	FILTRAR PENDIENTES DIARIOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_PendientesDiarios_FiltrarActividades]
@idActividad INT
AS
BEGIN
	SELECT PD.idActividad, PD.Persona, PD.Descripcion, LTRIM(RTRIM(P.NombreCompleto)) AS 'Responsable', PD.Nivel, PD.Estado,
	PD.FechaInicio, PD.Contador, PD.FProyectada1, PD.FProyectada2, PD.FProyectada3, PD.Seguimiento
	FROM ReportesApp_Operaciones_PendientesDiarios_Actividades PD
	LEFT JOIN PersonaMast P ON P.Persona = PD.Persona
	WHERE PD.idActividad = @idActividad
END

----------------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: (09-04-2024)
-- Description:	MODIFICAR PENDIENTES DIARIOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_PendientesDiarios_ModificarActividades]
@Opcion INT,
@idActividad INT,
@Persona INT,
@Estado VARCHAR(50),
@Seguimiento VARCHAR(350),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Estado Actualizado.'

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- ELIMINAR PENDIENTES
		IF ((SELECT CONVERT(DATE,FechaInicio) FROM ReportesApp_Operaciones_PendientesDiarios_Actividades WHERE idActividad = @idActividad) < CONVERT(DATE,GETDATE())
		AND (SELECT Estado FROM ReportesApp_Operaciones_PendientesDiarios_Actividades WHERE idActividad = @idActividad) IN ('PENDIENTE','EN PROCESO')) BEGIN
			SET @Exito = '-1 = Este pendiente está en curso y no puede eliminarse.'
			ROLLBACK
			GOTO Terminar
		END
		
		DELETE FROM ReportesApp_Operaciones_PendientesDiarios_Actividades
		WHERE idActividad = @idActividad
	END

	IF (@Opcion = 2) BEGIN		-- ACTUALIZAR ESTADO DE PENDIENTES
		UPDATE ReportesApp_Operaciones_PendientesDiarios_Actividades
		SET Persona = @Persona, Estado = @Estado, Seguimiento = @Seguimiento, UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()
		WHERE idActividad = @idActividad
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

----------------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 13-03-2024
-- Description:	REPROGRAMAR PENDIENTES DIARIOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_PendientesDiarios_ReprogramarActividades]
@idActividad INT,
@FechaReprog DATETIME,
@Contador INT,
@Seguimiento VARCHAR(350),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Actividad Reprogramada.'

IF (@Contador = 2) BEGIN
	SET @Exito = '-1 = No puede reprogramar esta actividad más de 2 veces.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	IF (@Contador = 0) BEGIN
		IF (CONVERT(DATE,@FechaReprog) <= (SELECT CONVERT(DATE,FProyectada1) FROM ReportesApp_Operaciones_PendientesDiarios_Actividades WHERE idActividad = @idActividad)) BEGIN
			SET @Exito = '-2 = La fecha ingresada no puede ser menor a la última fecha programada.'
			ROLLBACK
			GOTO Terminar
		END
		ELSE BEGIN
			UPDATE ReportesApp_Operaciones_PendientesDiarios_Actividades
			SET FProyectada2 = @FechaReprog, FProyectada3 = @FechaReprog, Contador = 2, Dias = DATEDIFF(DAY,FechaInicio, @FechaReprog), Seguimiento = @Seguimiento,
				UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()
			WHERE idActividad = @idActividad
		END
	END

	IF (@Contador = 1) BEGIN
		IF (CONVERT(DATE,@FechaReprog) <= (SELECT CONVERT(DATE,FProyectada2) FROM ReportesApp_Operaciones_PendientesDiarios_Actividades WHERE idActividad = @idActividad)) BEGIN
			SET @Exito = '-2 = La fecha ingresada no puede ser menor a la última fecha programada.'
			ROLLBACK
			GOTO Terminar
		END
		ELSE BEGIN
			UPDATE ReportesApp_Operaciones_PendientesDiarios_Actividades
			SET FProyectada3 = @FechaReprog, Contador = 3, Dias = DATEDIFF(DAY,FechaInicio, @FechaReprog), Seguimiento = @Seguimiento,
				UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()
			WHERE idActividad = @idActividad
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

----------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 14-03-2023
-- Description:	CORREO DE AVISO DE ACTIVIDAD
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_PendientesDiarios_CorreoActividad]
AS
BEGIN
	DECLARE @Mensaje AS VARCHAR(MAX), @Asunto VARCHAR(300)
	DECLARE @Contador INT
	DECLARE @CORREO TABLE (Persona INT, Correo VARCHAR(300))
	DECLARE @TEMP_CORREO TABLE(
		Numero INT,
		Persona INT,
		Correo VARCHAR(300))

	UPDATE PD
	SET PD.Dias = DATEDIFF(DAY,GETDATE(),PD.FProyectada1)
	FROM ReportesApp_Operaciones_PendientesDiarios_Actividades PD
	WHERE (PD.Estado = 'PENDIENTE' OR PD.Estado = 'EN PROCESO') AND PD.Contador = 1 AND CONVERT(DATE,GETDATE()) >= CONVERT(DATE,PD.FechaInicio)

	UPDATE PD
	SET PD.Dias = DATEDIFF(DAY,GETDATE(),PD.FProyectada2)
	FROM ReportesApp_Operaciones_PendientesDiarios_Actividades PD
	WHERE (PD.Estado = 'PENDIENTE' OR PD.Estado = 'EN PROCESO') AND PD.Contador = 2 AND CONVERT(DATE,GETDATE()) >= CONVERT(DATE,PD.FechaInicio)

	UPDATE PD
	SET PD.Dias = DATEDIFF(DAY,GETDATE(),PD.FProyectada3)
	FROM ReportesApp_Operaciones_PendientesDiarios_Actividades PD
	WHERE (PD.Estado = 'PENDIENTE' OR PD.Estado = 'EN PROCESO') AND PD.Contador = 3 AND CONVERT(DATE,GETDATE()) >= CONVERT(DATE,PD.FechaInicio)
	
	INSERT INTO @CORREO(Persona, Correo)
	SELECT DISTINCT C.idPersona, C.Correo
	FROM ReportesApp_Operaciones_PendientesDiarios_CorreosPersonal C
	LEFT JOIN ReportesApp_Operaciones_PendientesDiarios_Actividades PD ON C.idPersona = PD.Persona
	WHERE (PD.Estado = 'PENDIENTE' OR PD.Estado = 'EN PROCESO')

	INSERT INTO @TEMP_CORREO(Numero, Persona, Correo)
	SELECT ROW_NUMBER() OVER(ORDER BY Correo ASC), Persona, Correo
	FROM @CORREO

	SET @Contador = 1

	WHILE (@Contador <= (SELECT COUNT(Numero) FROM @TEMP_CORREO)) BEGIN
		DECLARE @ActividadPendiente VARCHAR(MAX) = ''

		SELECT @ActividadPendiente = @ActividadPendiente + '<tr>'
									   + '<td style="background-color: #EEE8AA">' + LTRIM(RTRIM(P.NombreCompleto)) + '</td>'
									   + '<td style="background-color: #EEE8AA">' + PD.Descripcion + '</td>'
									   + '<td style="background-color: #EEE8AA">' + PD.Nivel + '</td>'
									   + '<td style="background-color: #EEE8AA">' + PD.Estado + '</td>'
									   + '<td style="background-color: #EEE8AA">' + CONVERT(VARCHAR,PD.FechaInicio,103) + '</td>'
									   + '<td style="background-color: #EEE8AA">' + CONVERT(VARCHAR,PD.FProyectada3,103) + '</td>'
									   + '<td style="background-color: #EEE8AA">' + CONVERT(VARCHAR,PD.Dias) + '</td>'
									   + '<td style="background-color: #EEE8AA">' + PD.Seguimiento + '</td>'
									   + '</tr>'   
		FROM ReportesApp_Operaciones_PendientesDiarios_Actividades PD
		LEFT JOIN PersonaMast P ON P.Persona = PD.Persona
		WHERE (PD.Persona = (SELECT Persona FROM @TEMP_CORREO WHERE Numero = @Contador)) AND PD.Estado IN ('PENDIENTE','EN PROCESO')
	
		SET @Asunto = 'ALERTA DE ACTIVIDADES PENDIENTES'

		SET @Mensaje = '<p><h2>LISTA DE ACTIVIDADES PENDIENTES PARA EL DÍA DE HOY</h2></p>'
						+'<p>Estas son las actividades cuya fecha proyectada se han cumplido el día de hoy: </p>'
						+ '<p><table border="1" cellspacing="1" cellpadding="1" >
							<tr>
								<td align="center" style="background-color:orange; color: black"><b>RESPONSABLE</b></td>
								<td align="center" style="background-color:orange; color: black"><b>ACTIVIDAD</b></td>
								<td align="center" style="background-color:orange; color: black"><b>NIVEL</b></td>
								<td align="center" style="background-color:orange; color: black"><b>ESTADO</b></td>
								<td align="center" style="background-color:orange; color: black"><b>FECHA INICIO</b></td>
								<td align="center" style="background-color:orange; color: black"><b>FECHA PROYECTADA</b></td>
								<td align="center" style="background-color:orange; color: black"><b>CONTADOR DÍAS</b></td>
								<td align="center" style="background-color:orange; color: black"><b>SEGUIMIENTO</b></td>
							</tr>
							<tbody>'
							+ ISNULL(@ActividadPendiente, '') 	
							+'</tbody>
							</table>
						</p>'
						+'<p>Fecha de envío: '+CONVERT(CHAR(10),GETDATE(),103)+RIGHT(RTRIM(CONVERT(CHAR(26),GETDATE(),22)),12)+'</p>'
						+'<BR>'
						+'<p><font face="verdana" size="2" color="DarkRed">Tecnologías de la Información - GRUPO TRANSPESA</font></p>'
	
		DECLARE @Destinatario VARCHAR(MAX)
		SET @Destinatario = (SELECT Correo FROM @TEMP_CORREO WHERE Numero = @Contador)

		EXEC msdb.dbo.sp_send_dbmail 
			@profile_name='AVISODESISTEMA',
			@recipients = @Destinatario,
			--@blind_copy_recipients = 'asistenteauditoria@transpesa.com.pe;jrojas@transpesa.com.pe;desarrollo@transpesa.com.pe;desarrollo2@transpesa.com.pe;' 
			@subject = @Asunto,
			@body_format = 'HTML',
			@body = @Mensaje

		SET @Contador = @Contador + 1
	END
END


SELECT * FROM [msdb].[dbo].[sysmail_allitems]
ORDER BY [send_request_date] DESC