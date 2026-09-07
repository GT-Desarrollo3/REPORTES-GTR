
-- CREAR TABLA ReportesApp_Seguridad_GestionSeguridad_ObjetivosEstrategicos

-- CREAR TABLA ReportesApp_Seguridad_GestionSeguridad_ObjetivosOperativos

-- CREAR TABLA ReportesApp_Seguridad_GestionSeguridad_Actividades

-- CREAR TABLA ReportesApp_Seguridad_GestionSeguridad_OEstrategicoOperativo

-- CREAR TABLA ReportesApp_Seguridad_GestionSeguridad_OOperativoActividad

-- CREAR TABLA ReportesApp_Seguridad_GestionSeguridad_Registro

-------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 23-02-2024
-- Description:	INSERTAR OBJETIVOS Y ACTIVIDADES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_GestionSeguridad_InsertarObjetivosActividades]
@Opcion INT,
@Descripcion VARCHAR(250),
@xml VARCHAR(MAX)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT
DECLARE @Orden INT
DECLARE @i INT
DECLARE @idoc INT
DECLARE @TEMP_ESTRATEGICO TABLE(
		Marca BIT,
		Orden INT,
		idObjetivoE INT,
		ObjetivoE VARCHAR(250))

SET @Exito = '0 = Objetivo Registrado.'

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN
		SET @correlativo = (SELECT MAX(idObjetivoE) FROM ReportesApp_Seguridad_GestionSeguridad_ObjetivosEstrategicos)
		SET @correlativo = ISNULL(@correlativo,0) + 1

		INSERT INTO ReportesApp_Seguridad_GestionSeguridad_ObjetivosEstrategicos(idObjetivoE,ObjetivoE)
		VALUES(@correlativo, @Descripcion)
	END

	IF (@Opcion = 2) BEGIN
		SET @correlativo = (SELECT MAX(idObjetivoO) FROM ReportesApp_Seguridad_GestionSeguridad_ObjetivosOperativos)
		SET @correlativo = ISNULL(@correlativo,0) + 1

		INSERT INTO ReportesApp_Seguridad_GestionSeguridad_ObjetivosOperativos(idObjetivoO,ObjetivoO)
		VALUES(@correlativo, @Descripcion)

		EXEC sp_xml_preparedocument @idoc OUTPUT, @xml

		INSERT INTO @TEMP_ESTRATEGICO(Marca, Orden, idObjetivoE, ObjetivoE)
		SELECT Marca, ROW_NUMBER() OVER(ORDER BY idObjetivoE ASC), idObjetivoE, ObjetivoE
		FROM OPENXML(@idoc,'/r/d',1)
		WITH (Marca BIT, idObjetivoE INT, ObjetivoE VARCHAR(250))

		SET @i = 1
		WHILE (@i <= (SELECT COUNT(*) FROM @TEMP_ESTRATEGICO)) BEGIN
			SET @Orden = (SELECT MAX(OrdenEO) FROM ReportesApp_Seguridad_GestionSeguridad_OEstrategicoOperativo WHERE idObjetivoO = @correlativo)
			SET @Orden = ISNULL(@Orden,0) + 1

			INSERT INTO ReportesApp_Seguridad_GestionSeguridad_OEstrategicoOperativo(OrdenEO,idObjetivoO,idObjetivoE)
			SELECT @Orden, @correlativo, E.idObjetivoE FROM @TEMP_ESTRATEGICO E WHERE E.Orden = @i

			SET @i = @i + 1
		END
	END

	IF (@Opcion = 3) BEGIN
		SET @correlativo = (SELECT MAX(idActividad) FROM ReportesApp_Seguridad_GestionSeguridad_Actividades)
		SET @correlativo = ISNULL(@correlativo,0) + 1

		INSERT INTO ReportesApp_Seguridad_GestionSeguridad_Actividades(idActividad,Actividad)
		VALUES(@correlativo, @Descripcion)

		EXEC sp_xml_preparedocument @idoc OUTPUT, @xml

		INSERT INTO @TEMP_ESTRATEGICO(Marca, Orden, idObjetivoE, ObjetivoE)
		SELECT Marca, ROW_NUMBER() OVER(ORDER BY idObjetivoO ASC), idObjetivoO, ObjetivoO
		FROM OPENXML(@idoc,'/r/d',1)
		WITH (Marca BIT, idObjetivoO INT, ObjetivoO VARCHAR(250))

		SET @i = 1
		WHILE (@i <= (SELECT COUNT(*) FROM @TEMP_ESTRATEGICO)) BEGIN
			SET @Orden = (SELECT MAX(OrdenOA) FROM ReportesApp_Seguridad_GestionSeguridad_OOperativoActividad WHERE idActividad = @correlativo)
			SET @Orden = ISNULL(@Orden,0) + 1

			INSERT INTO ReportesApp_Seguridad_GestionSeguridad_OOperativoActividad(OrdenOA,idActividad,idObjetivoO)
			SELECT @Orden, @correlativo, E.idObjetivoE FROM @TEMP_ESTRATEGICO E WHERE E.Orden = @i

			SET @i = @i + 1
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

-------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 23-01-2024
-- Description:	LISTAR OBJETIVOS Y ACTIVIDADES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_GestionSeguridad_ListarObjetivosActividades]
@Opcion INT,
@Descripcion VARCHAR(250)
AS
BEGIN
	IF (@Opcion = 1) BEGIN
		SELECT idObjetivoE AS 'NRO', 
		REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(ObjetivoE,'Á','A'),'É','E'),'Í','I'),'Ó','O'),'Ú','U') AS 'OBJETIVO_ESTRATEGICO'
		FROM ReportesApp_Seguridad_GestionSeguridad_ObjetivosEstrategicos
		WHERE (ObjetivoE IS NULL OR ObjetivoE LIKE '%' + @Descripcion + '%')
		ORDER BY idObjetivoE DESC
	END

	IF (@Opcion = 2) BEGIN
		SELECT O.idObjetivoO AS 'NRO', O.ObjetivoO AS 'OBJETIVO_OPERATIVO', OE.idObjetivoE, E.ObjetivoE AS 'OBJETIVO_ESTRATEGICO'
		FROM ReportesApp_Seguridad_GestionSeguridad_ObjetivosOperativos O
		LEFT JOIN ReportesApp_Seguridad_GestionSeguridad_OEstrategicoOperativo OE ON O.idObjetivoO = OE.idObjetivoO
		LEFT JOIN ReportesApp_Seguridad_GestionSeguridad_ObjetivosEstrategicos E ON E.idObjetivoE = OE.idObjetivoE
		WHERE (O.ObjetivoO IS NULL OR O.ObjetivoO LIKE '%' + @Descripcion + '%')
		ORDER BY O.idObjetivoO DESC
	END

	IF (@Opcion = 21) BEGIN
		SELECT idObjetivoO AS 'NRO',
		REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(ObjetivoO,'Á','A'),'É','E'),'Í','I'),'Ó','O'),'Ú','U') AS 'OBJETIVO_OPERATIVO'
		FROM ReportesApp_Seguridad_GestionSeguridad_ObjetivosOperativos
		WHERE (ObjetivoO IS NULL OR ObjetivoO LIKE '%' + @Descripcion + '%')
		ORDER BY idObjetivoO DESC
	END

	IF (@Opcion = 3) BEGIN
		SELECT A.idActividad AS 'NRO', A.Actividad AS 'ACTIVIDAD', OA.idObjetivoO, O.ObjetivoO AS 'OBJETIVO_OPERATIVO', E.ObjetivoE AS 'OBJETIVO_ESTRATEGICO'
		FROM ReportesApp_Seguridad_GestionSeguridad_Actividades A
		LEFT JOIN ReportesApp_Seguridad_GestionSeguridad_OOperativoActividad OA ON A.idActividad = OA.idActividad
		LEFT JOIN ReportesApp_Seguridad_GestionSeguridad_ObjetivosOperativos O ON O.idObjetivoO = OA.idObjetivoO
		LEFT JOIN ReportesApp_Seguridad_GestionSeguridad_OEstrategicoOperativo OE ON O.idObjetivoO = OE.idObjetivoO
		LEFT JOIN ReportesApp_Seguridad_GestionSeguridad_ObjetivosEstrategicos E ON E.idObjetivoE = OE.idObjetivoE
		WHERE (A.Actividad IS NULL OR A.Actividad LIKE '%' + @Descripcion + '%')
		ORDER BY A.idActividad DESC
	END
END

-------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 23-01-2024
-- Description:	ELIMINAR OBJETIVOS Y ACTIVIDADES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_GestionSeguridad_EliminarObjetivosActividades]
@Opcion INT,
@idAO INT
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

SET @Exito = '0 = Objetivo Eliminado.'

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN
		IF(EXISTS(SELECT * FROM ReportesApp_Seguridad_GestionSeguridad_OEstrategicoOperativo WHERE idObjetivoE = @idAO)) BEGIN
			SET @Exito = '-1 = Este objetivo no se puede eliminar porque se encuentra enlazado.'
			GOTO Terminar
		END
		ELSE BEGIN
			DELETE FROM ReportesApp_Seguridad_GestionSeguridad_ObjetivosEstrategicos
			WHERE idObjetivoE = @idAO
		END
	END

	IF (@Opcion = 2) BEGIN
		IF(EXISTS(SELECT * FROM ReportesApp_Seguridad_GestionSeguridad_OOperativoActividad WHERE idObjetivoO = @idAO)) BEGIN
			SET @Exito = '-1 = Este objetivo no se puede eliminar porque se encuentra enlazado.'
			GOTO Terminar
		END
		ELSE BEGIN
			DELETE FROM ReportesApp_Seguridad_GestionSeguridad_ObjetivosOperativos
			WHERE idObjetivoO = @idAO

			DELETE FROM ReportesApp_Seguridad_GestionSeguridad_OEstrategicoOperativo
			WHERE idObjetivoO = @idAO
		END
	END

	IF (@Opcion = 3) BEGIN
		DELETE FROM ReportesApp_Seguridad_GestionSeguridad_OOperativoActividad
		WHERE idActividad = @idAO

		DELETE FROM ReportesApp_Seguridad_GestionSeguridad_Actividades
		WHERE idActividad = @idAO
	END

	IF (@Opcion = 4) BEGIN		-- ELIMINAR GESTIÓN DE SEGURIDAD
		DELETE FROM ReportesApp_Seguridad_GestionSeguridad_Registro
		WHERE idGestionActividad = @idAO

		DELETE FROM ReportesApp_Seguridad_GestionSeguridad_Responsables
		WHERE idGestionActividad = @idAO

		DELETE FROM ReportesApp_Seguridad_GestionSeguridad_Cronograma
		WHERE idGestionActividad = @idAO
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

-------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 26-02-2024
-- Description:	VINCULAR OBJETIVOS Y ACTIVIDADES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_GestionSeguridad_AsignarObjetivosActividades]
@Opcion INT,
@idOO INT,
@xml VARCHAR(MAX)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @Orden INT
DECLARE @i INT
DECLARE @idoc INT
DECLARE @TEMP_ESTRATEGICO TABLE(
		Marca BIT,
		Orden INT,
		idObjetivoE INT,
		ObjetivoE VARCHAR(250))

SET @Exito = '0 = Objetivo Vinculado.'

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 2) BEGIN
		EXEC sp_xml_preparedocument @idoc OUTPUT, @xml

		INSERT INTO @TEMP_ESTRATEGICO(Marca, Orden, idObjetivoE, ObjetivoE)
		SELECT Marca, ROW_NUMBER() OVER(ORDER BY idObjetivoE ASC), idObjetivoE, ObjetivoE
		FROM OPENXML(@idoc,'/r/d',1)
		WITH (Marca BIT, idObjetivoE INT, ObjetivoE VARCHAR(250))

		SET @i = 1
		WHILE (@i <= (SELECT COUNT(*) FROM @TEMP_ESTRATEGICO)) BEGIN
			IF (EXISTS(SELECT * FROM ReportesApp_Seguridad_GestionSeguridad_OEstrategicoOperativo WHERE idObjetivoO = @idOO
						AND idObjetivoE = (SELECT E.idObjetivoE FROM @TEMP_ESTRATEGICO E WHERE E.Orden = @i))) BEGIN
				SET @Opcion = 2
			END
			ELSE BEGIN
				SET @Orden = (SELECT MAX(OrdenEO) FROM ReportesApp_Seguridad_GestionSeguridad_OEstrategicoOperativo WHERE idObjetivoO = @idOO)
				SET @Orden = ISNULL(@Orden,0) + 1

				INSERT INTO ReportesApp_Seguridad_GestionSeguridad_OEstrategicoOperativo(OrdenEO,idObjetivoO,idObjetivoE)
				SELECT @Orden, @idOO, E.idObjetivoE FROM @TEMP_ESTRATEGICO E WHERE E.Orden = @i
			END

			SET @i = @i + 1
		END
	END

	IF (@Opcion = 3) BEGIN
		EXEC sp_xml_preparedocument @idoc OUTPUT, @xml

		INSERT INTO @TEMP_ESTRATEGICO(Marca, Orden, idObjetivoE, ObjetivoE)
		SELECT Marca, ROW_NUMBER() OVER(ORDER BY idObjetivoO ASC), idObjetivoO, ObjetivoO
		FROM OPENXML(@idoc,'/r/d',1)
		WITH (Marca BIT, idObjetivoO INT, ObjetivoO VARCHAR(250))

		SET @i = 1
		WHILE (@i <= (SELECT COUNT(*) FROM @TEMP_ESTRATEGICO)) BEGIN
			IF (EXISTS(SELECT * FROM ReportesApp_Seguridad_GestionSeguridad_OOperativoActividad WHERE idActividad = @idOO
						AND idObjetivoO = (SELECT E.idObjetivoE FROM @TEMP_ESTRATEGICO E WHERE E.Orden = @i))) BEGIN
				SET @Opcion = 3
			END
			ELSE BEGIN
				SET @Orden = (SELECT MAX(OrdenOA) FROM ReportesApp_Seguridad_GestionSeguridad_OOperativoActividad WHERE idActividad = @idOO)
				SET @Orden = ISNULL(@Orden,0) + 1

				INSERT INTO ReportesApp_Seguridad_GestionSeguridad_OOperativoActividad(OrdenOA,idActividad,idObjetivoO)
				SELECT @Orden, @idOO, E.idObjetivoE FROM @TEMP_ESTRATEGICO E WHERE E.Orden = @i
			END

			SET @i = @i + 1
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

-------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 26-02-2024
-- Description:	ELIMINAR OBJETIVOS Y ACTIVIDADES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_GestionSeguridad_DesvincularObjetivosActividades]
@Opcion INT,
@idObjetivo INT,
@idActividad INT
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Objetivo Desvinculado.'

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 2) BEGIN
		DELETE FROM ReportesApp_Seguridad_GestionSeguridad_OEstrategicoOperativo
		WHERE idObjetivoO= @idObjetivo AND idObjetivoE = @idActividad
	END

	IF (@Opcion = 3) BEGIN
		DELETE FROM ReportesApp_Seguridad_GestionSeguridad_OOperativoActividad
		WHERE idActividad= @idObjetivo AND idObjetivoO = @idActividad
	END
END TRY

BEGIN CATCH
	SET @exito = RTRIM(LTRIM(STR(ERROR_NUMBER()))) + '=' + ERROR_MESSAGE()
	ROLLBACK
	GOTO Terminar
END CATCH

IF @@TRANCOUNT > 0
	COMMIT;

TERMINAR:
IF LEFT(@Exito,1)='-' OR cast(left(@Exito,1) AS INT)>0 BEGIN
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
-- Create date: 28-02-2024
-- Description:	LISTAR PERSONAL ACTIVO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_GestionSeguridad_ListarPersonal]
@Opcion INT,
@Filtro VARCHAR(250)
AS
BEGIN
	IF (@Opcion = 1) BEGIN		-- LISTAR PERSONAL CON CARGOS
		SELECT DISTINCT TOP(25) P.Persona, LTRIM(RTRIM(P.NombreCompleto)) AS 'Nombre', A1.Description AS 'Area', LTRIM(RTRIM(C1.Descripcion)) AS 'Cargo' 
		FROM PersonaMast P
		LEFT JOIN EmpleadoMast E ON P.Persona = E.Empleado
		LEFT JOIN departmentmst A1 ON A1.Department = E.DeptoOrganizacion
		LEFT JOIN hr_puestoempresa C1 ON C1.CodigoPuesto = E.CodigoCargo
		WHERE (P.Estado = 'A') AND (P.EsEmpleado = 'S') AND (E.Estado = 'A') AND (P.NombreCompleto LIKE '%' + @Filtro + '%')
	END
	
	IF (@Opcion = 2) BEGIN		-- LISTAR ACTIVIDADES
		SELECT idActividad, Actividad FROM ReportesApp_Seguridad_GestionSeguridad_Actividades
		WHERE (Actividad LIKE '%' + @Filtro + '%')
	END

	IF (@Opcion = 3) BEGIN		-- LISTAR AREAS
		SELECT '000' AS 'department', 'TODAS' AS 'description'
		UNION
		(SELECT department, description FROM departmentmst
		WHERE department NOT IN ('999','AME','AML','AYM','FAC','FP','OPA','OPI','OPL','REC','SAL'))
	END
END

----------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 29-02-2024
-- Description:	GENERAR GESTIÓN DE ACTIVIDADES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_GestionSeguridad_GenerarGestionActividades]
@Persona INT,
@idActividad INT,
@Peso DECIMAL(10,2),
@Validacion VARCHAR(20),
@Cronograma VARCHAR(30),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo1 INT

SET @Exito = '0 = Reporte Generado Correctamente.'

SET @correlativo1 = (SELECT MAX(idGestionActividad) FROM ReportesApp_Seguridad_GestionSeguridad_Registro)
SET @correlativo1 = ISNULL(@correlativo1,0) + 1

BEGIN TRAN
BEGIN TRY
	INSERT INTO ReportesApp_Seguridad_GestionSeguridad_Registro (idGestionActividad, idPersonaLider, idActividad, Validacion, Cronograma,
	Porcentaje, UsuarioCreacion, FechaCreacion, UsuarioModificacion, FechaModificacion)
	VALUES (@correlativo1, @Persona, @idActividad, @Validacion, @Cronograma, 0.00, @Usuario, GETDATE(), @Usuario, GETDATE())

	INSERT INTO ReportesApp_Seguridad_GestionSeguridad_Responsables (idResponsable, idGestionActividad, Persona, Peso, EsLider)
	VALUES (1, @correlativo1, @Persona, @Peso, 'L')
END TRY

BEGIN CATCH
	SET @exito = RTRIM(LTRIM(STR(ERROR_NUMBER()))) + '=' + ERROR_MESSAGE()
	ROLLBACK
	GOTO Terminar
END CATCH

IF @@TRANCOUNT > 0
	COMMIT;

TERMINAR:
IF LEFT(@Exito,1)='-' OR cast(left(@Exito,1) AS INT)>0 BEGIN
	DECLARE @storedProcedureName VARCHAR(100)
	SELECT @storedProcedureName = OBJECT_NAME(@@PROCID)
	SET @exito = @exito + CHAR(13)+CHAR(13)+'SP Name: '+@storedProcedureName
END

SELECT @exito exito

--------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 29-02-2024
-- Description:	LISTAR GESTION ACTIVIDADES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_GestionSeguridad_ListarGestionActividades]
@Actividad VARCHAR(250),
@Area VARCHAR(250),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@Area = 'TODAS') BEGIN
		SELECT GS.idGestionActividad AS '#', GS.idActividad, A.Actividad AS 'ACTIVIDAD', O.ObjetivoO AS 'OBJETIVO_OPERATIVO', GS.Validacion AS 'VALIDACION',
		A1.Description AS 'AREA', LTRIM(RTRIM(C1.Descripcion)) AS 'CARGO', LTRIM(RTRIM(P.NombreCompleto)) AS 'PERSONAL',
		(CASE WHEN R.EsLider = 'L' THEN 'LÍDER' ELSE '' END) AS 'ES_LIDER', R.Peso AS 'PESO_RESPONSABLE',
		GS.Cronograma AS 'CRONOGRAMA', GS.Porcentaje AS 'PORCENTAJE', CAST((R.Peso * (GS.Porcentaje/100)) AS DECIMAL(10,2)) AS 'PROMEDIO',
		GS.UsuarioCreacion, GS.FechaCreacion, GS.UsuarioModificacion, GS.FechaModificacion
		FROM ReportesApp_Seguridad_GestionSeguridad_Responsables R
		LEFT JOIN ReportesApp_Seguridad_GestionSeguridad_Registro GS ON R.idGestionActividad = GS.idGestionActividad
		LEFT JOIN PersonaMast P ON P.Persona = R.Persona
		LEFT JOIN EmpleadoMast E ON P.Persona = E.Empleado
		LEFT JOIN departmentmst A1 ON A1.Department = E.DeptoOrganizacion
		LEFT JOIN hr_puestoempresa C1 ON C1.CodigoPuesto = E.CodigoCargo
		LEFT JOIN ReportesApp_Seguridad_GestionSeguridad_Actividades A ON A.idActividad = GS.idActividad
		LEFT JOIN ReportesApp_Seguridad_GestionSeguridad_OOperativoActividad OA ON OA.idActividad = GS.idActividad
		LEFT JOIN ReportesApp_Seguridad_GestionSeguridad_ObjetivosOperativos O ON O.idObjetivoO = OA.idObjetivoO
		WHERE (@Actividad IS NULL OR A.Actividad LIKE '%' + @Actividad + '%') AND (GS.FechaCreacion BETWEEN @FINICIO AND @FFIN)
		ORDER BY GS.idGestionActividad, R.idResponsable
	END
	ELSE BEGIN
		SELECT GS.idGestionActividad AS '#', GS.idActividad, A.Actividad AS 'ACTIVIDAD', O.ObjetivoO AS 'OBJETIVO_OPERATIVO', GS.Validacion AS 'VALIDACION',
		A1.Description AS 'AREA', LTRIM(RTRIM(C1.Descripcion)) AS 'CARGO', LTRIM(RTRIM(P.NombreCompleto)) AS 'PERSONAL', 
		(CASE WHEN R.EsLider = 'L' THEN 'LÍDER' ELSE '' END) AS 'ES_LIDER', R.Peso AS 'PESO_RESPONSABLE',
		GS.Cronograma AS 'CRONOGRAMA', GS.Porcentaje AS 'PORCENTAJE', CAST((R.Peso * (GS.Porcentaje/100)) AS DECIMAL(10,2)) AS 'PROMEDIO'
		FROM ReportesApp_Seguridad_GestionSeguridad_Responsables R
		LEFT JOIN ReportesApp_Seguridad_GestionSeguridad_Registro GS ON R.idGestionActividad = GS.idGestionActividad
		LEFT JOIN  PersonaMast P ON P.Persona = R.Persona
		LEFT JOIN EmpleadoMast E ON P.Persona = E.Empleado
		LEFT JOIN departmentmst A1 ON A1.Department = E.DeptoOrganizacion
		LEFT JOIN hr_puestoempresa C1 ON C1.CodigoPuesto = E.CodigoCargo
		LEFT JOIN ReportesApp_Seguridad_GestionSeguridad_Actividades A ON A.idActividad = GS.idActividad
		LEFT JOIN ReportesApp_Seguridad_GestionSeguridad_OOperativoActividad OA ON OA.idActividad = GS.idActividad
		LEFT JOIN ReportesApp_Seguridad_GestionSeguridad_ObjetivosOperativos O ON O.idObjetivoO = OA.idObjetivoO
		WHERE (@Actividad IS NULL OR A.Actividad LIKE '%' + @Actividad + '%') AND (@Area = A1.Description) AND (GS.FechaCreacion BETWEEN @FINICIO AND @FFIN)
		ORDER BY GS.idGestionActividad, R.idResponsable
	END
END

--------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 04-03-2024
-- Description:	LISTAR PERSONAL RESPONSABLE
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_GestionSeguridad_ListarPersonalResponsable]
@idGestionActividad INT
AS
BEGIN
	SELECT PR.idResponsable, PR.idGestionActividad, PR.Persona, LTRIM(RTRIM(P.NombreCompleto)) AS 'PERSONAL_RESPONSABLE', A1.Description AS 'AREA',
	LTRIM(RTRIM(C1.Descripcion)) AS 'CARGO', PR.Peso AS 'PESO', PR.EsLider AS 'ES_LIDER'
	FROM ReportesApp_Seguridad_GestionSeguridad_Responsables PR
	LEFT JOIN ReportesApp_Seguridad_GestionSeguridad_Registro GS ON PR.idGestionActividad = GS.idGestionActividad
	LEFT JOIN PersonaMast P ON P.Persona = PR.Persona
	LEFT JOIN EmpleadoMast E ON P.Persona = E.Empleado
	LEFT JOIN departmentmst A1 ON A1.Department = E.DeptoOrganizacion
	LEFT JOIN hr_puestoempresa C1 ON C1.CodigoPuesto = E.CodigoCargo
	WHERE PR.idGestionActividad = @idGestionActividad
	ORDER BY idResponsable
END

--------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 29-02-2024
-- Description:	ASIGNAR PERSONAL RESPONSABLE
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_GestionSeguridad_AsignarPersonalResponsable]
@idGestionActividad INT,
@Persona INT,
@Peso DECIMAL(10,2)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

SET @Exito = '0 = Personal Asignado Correctamente.'

SET @correlativo = (SELECT MAX(idResponsable) FROM ReportesApp_Seguridad_GestionSeguridad_Responsables WHERE idGestionActividad = @idGestionActividad)
SET @correlativo = ISNULL(@correlativo,0) + 1

BEGIN TRAN
BEGIN TRY
	IF (EXISTS(SELECT * FROM ReportesApp_Seguridad_GestionSeguridad_Responsables WHERE idGestionActividad = @idGestionActividad AND Persona = @Persona)) BEGIN
		UPDATE ReportesApp_Seguridad_GestionSeguridad_Responsables
		SET Peso = @Peso
		WHERE idGestionActividad = @idGestionActividad AND Persona = @Persona
	END
	ELSE BEGIN
		INSERT INTO ReportesApp_Seguridad_GestionSeguridad_Responsables (idResponsable, idGestionActividad, Persona, Peso, EsLider)
		VALUES (@correlativo, @idGestionActividad, @Persona, @Peso, '')
	END
END TRY

BEGIN CATCH
	SET @exito = RTRIM(LTRIM(STR(ERROR_NUMBER()))) + '=' + ERROR_MESSAGE()
	ROLLBACK
	GOTO Terminar
END CATCH

IF @@TRANCOUNT > 0
	COMMIT;

TERMINAR:
IF LEFT(@Exito,1)='-' OR cast(left(@Exito,1) AS INT)>0 BEGIN
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
-- Create date: 05-03-2024
-- Description:	BUSCAR PERSONAL RESPONSABLE
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_GestionSeguridad_BuscarPersonalResponsable]
@Opcion INT,
@idResponsable INT,
@idGestionActividad INT
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN 
	IF (@Opcion = 1) BEGIN		-- BUSCAR PERSONAL RESPONSABLE
		SELECT R.Persona, LTRIM(RTRIM(P.NombreCompleto)) AS 'PERSONAL_RESPONSABLE', R.Peso
		FROM ReportesApp_Seguridad_GestionSeguridad_Responsables R
		LEFT JOIN PersonaMast P ON P.Persona = R.Persona
		WHERE R.idResponsable = @idResponsable AND R.idGestionActividad = @idGestionActividad
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR RESPONSABLE
		IF ((SELECT EsLider FROM ReportesApp_Seguridad_GestionSeguridad_Responsables WHERE idGestionActividad = @idGestionActividad AND idResponsable = @idResponsable) = 'L') BEGIN
			SET @Exito = '-1 = No puede eliminar al líder de la actividad.'
		END
		ELSE BEGIN
			DELETE FROM ReportesApp_Seguridad_GestionSeguridad_Responsables
			WHERE idResponsable = @idResponsable AND idGestionActividad = @idGestionActividad

			SET @Exito = '0 = Personal Eliminado Correctamente.'

			SELECT @exito exito 
		END
	END
END

--------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 04-03-2024
-- Description:	BUSCAR USUARIO VALIDACION
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_GestionSeguridad_ValidarUsuario]
@idGestionActividad INT,
@Validacion VARCHAR(20),
@Usuario VARCHAR(20)
AS
DECLARE @UsuarioLider VARCHAR(20)
DECLARE @Exito VARCHAR(MAX)

BEGIN
	IF (@Validacion = 'LIDER') BEGIN
		SET @UsuarioLider = (SELECT U.Usuario AS 'Usuario' FROM Usuario U LEFT JOIN PersonaMast P ON P.NombreCompleto = U.Nombre
							 WHERE P.Persona = (SELECT GS.idPersonaLider FROM ReportesApp_Seguridad_GestionSeguridad_Registro GS WHERE GS.idGestionActividad = @idGestionActividad))

		IF ((@Usuario = @UsuarioLider) OR (@Usuario IN ('LYSETHV','GINOLI','JGAMBINI','IBONILLA','GREYES','JROJAS'))) BEGIN
			SET @Exito = '0 = Usuario verificado.'
		END
		ELSE BEGIN
			SET @Exito = '-1 = Usuario no verificado.'
		END
	END

	IF (@Validacion = 'SSOMAC') BEGIN
		IF (@Usuario IN ('LYSETHV','GINOLI','JGAMBINI','IBONILLA','GREYES','JROJAS')) BEGIN
			SET @Exito = '0 = Usuario verificado.'
		END
		ELSE BEGIN
			SET @Exito = '-1 = Usuario no verificado.'
		END
	END

	SELECT @Exito exito
END

-------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 06-03-2024
-- Description:	ASIGNAR CRONOGRAMA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_GestionSeguridad_AsignarCronograma]
@idCronograma INT,
@idGestionActividad INT,
@FechaCronograma DATETIME,
@Meta DECIMAL(10,2),
@Meta2 DECIMAL(10,2),
@TipoCronograma VARCHAR(30)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @Promedio DECIMAL(10,2)
DECLARE @correlativo INT

SET @Exito = '0 = Personal Asignado Correctamente.'

SET @correlativo = (SELECT MAX(idCronograma) FROM ReportesApp_Seguridad_GestionSeguridad_Cronograma WHERE idGestionActividad = @idGestionActividad)
SET @correlativo = ISNULL(@correlativo,0) + 1

BEGIN TRAN
BEGIN TRY
	IF (@TipoCronograma = 'DIARIA') BEGIN
		IF (EXISTS(SELECT * FROM ReportesApp_Seguridad_GestionSeguridad_Cronograma WHERE idGestionActividad = @idGestionActividad AND
		CONVERT(DATE,Fecha) = CONVERT(DATE,@FechaCronograma))) BEGIN
			UPDATE ReportesApp_Seguridad_GestionSeguridad_Cronograma
			SET Meta = @Meta,
				Porcentaje = (CASE WHEN (ValorCumplido/@Meta)*100 >= 100.00 THEN 100
							  WHEN (ValorCumplido/@Meta)*100 >= 90.00 AND (ValorCumplido/@Meta)*100 < 100.00 THEN 60
						      ELSE 0 END)
			WHERE idGestionActividad = @idGestionActividad AND CONVERT(DATE,Fecha) = CONVERT(DATE,@FechaCronograma)
		END
		ELSE BEGIN
			INSERT INTO ReportesApp_Seguridad_GestionSeguridad_Cronograma (idCronograma, idGestionActividad, Fecha, Estado, Meta, Meta2, ValorCumplido, Porcentaje)
			VALUES (@correlativo, @idGestionActividad, @FechaCronograma, 'PENDIENTE', @Meta, 0.00, 0.00, 0.00)
		END
	END

	IF (@TipoCronograma = 'AVANCE') BEGIN
		IF (EXISTS(SELECT * FROM ReportesApp_Seguridad_GestionSeguridad_Cronograma WHERE idGestionActividad = @idGestionActividad AND
		idCronograma = @idCronograma)) BEGIN
			UPDATE ReportesApp_Seguridad_GestionSeguridad_Cronograma
			SET Fecha = @FechaCronograma
			WHERE idGestionActividad = @idGestionActividad AND idCronograma = @idCronograma
		END
		ELSE BEGIN
			SET @correlativo = 1

			WHILE (@correlativo <= CONVERT(INT,@Meta)) BEGIN
				INSERT INTO ReportesApp_Seguridad_GestionSeguridad_Cronograma (idCronograma, idGestionActividad, Fecha, Estado, Meta, Meta2, ValorCumplido, Porcentaje)
				VALUES (@correlativo, @idGestionActividad, DATEADD(DAY,@correlativo-1,@FechaCronograma), 'PENDIENTE', 100, 0.00, 0.00, 0.00)

				SET @correlativo = @correlativo + 1
			END
		END
	END

	IF (@TipoCronograma = 'META TOTAL') BEGIN
		IF (EXISTS(SELECT * FROM ReportesApp_Seguridad_GestionSeguridad_Cronograma WHERE idGestionActividad = @idGestionActividad AND
		CONVERT(DATE,Fecha) = CONVERT(DATE,@FechaCronograma))) BEGIN
			UPDATE ReportesApp_Seguridad_GestionSeguridad_Cronograma
			SET Meta = @Meta, Meta2 = @Meta2
			WHERE idGestionActividad = @idGestionActividad
		END
		ELSE BEGIN
			INSERT INTO ReportesApp_Seguridad_GestionSeguridad_Cronograma (idCronograma, idGestionActividad, Fecha, Estado, Meta, Meta2, ValorCumplido, Porcentaje)
			VALUES (@correlativo, @idGestionActividad, @FechaCronograma, 'PENDIENTE', @Meta, @Meta2, 0.00, 0.00)
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

TERMINAR:
IF LEFT(@Exito,1)='-' OR cast(left(@Exito,1) AS INT)>0 BEGIN
	DECLARE @storedProcedureName VARCHAR(100)
	SELECT @storedProcedureName = OBJECT_NAME(@@PROCID)
	SET @exito = @exito + CHAR(13)+CHAR(13)+'SP Name: '+@storedProcedureName
END

SELECT @exito exito

-----------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 06-03-2024
-- Description:	ELIMINAR CRONOGRAMA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_GestionSeguridad_ModificarCronograma]
@Opcion INT,
@idGestionActividad INT,
@idCronograma INT,
@MetaUsuario DECIMAL(10,2),
@TipoCronograma VARCHAR(30),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @Promedio DECIMAL(10,2)
DECLARE @correlativo INT
DECLARE @MetaExcelente DECIMAL(10,2)
DECLARE @MetaAceptable DECIMAL(10,2)

SET @Exito = '0 = Cronograma Actualizado Correctamente.'

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- MODIFICAR CRONOGRAMA
		IF (@TipoCronograma = 'DIARIA') BEGIN
			UPDATE ReportesApp_Seguridad_GestionSeguridad_Cronograma
			SET ValorCumplido = @MetaUsuario, Estado = 'COMPLETADO',
			Porcentaje = (CASE WHEN (@MetaUsuario/Meta)*100 >= 100.00 THEN 100
							   WHEN (@MetaUsuario/Meta)*100 >= 90.00 AND (@MetaUsuario/Meta)*100 < 100.00 THEN 60
							   ELSE 0 END)
			WHERE idCronograma = @idCronograma AND idGestionActividad = @idGestionActividad
		
			SET @Promedio = (SELECT ISNULL(AVG(Porcentaje),0) FROM ReportesApp_Seguridad_GestionSeguridad_Cronograma WHERE idGestionActividad = @idGestionActividad) 

			UPDATE ReportesApp_Seguridad_GestionSeguridad_Registro
			SET Porcentaje = CAST(@Promedio AS DECIMAL(10,2)), UsuarioModificacion = @Usuario, FechaModificacion = GETDATE() --CAST((CASE WHEN Meta = 0.00 THEN 0.00 ELSE (@MetaUsuario/Meta)*100 END) AS DECIMAL(10,2))
			WHERE idGestionActividad = @idGestionActividad
		END
		
		IF (@TipoCronograma = 'AVANCE') BEGIN
			UPDATE ReportesApp_Seguridad_GestionSeguridad_Cronograma
			SET ValorCumplido = 100, Estado = 'COMPLETADO', Porcentaje = 100
			WHERE idCronograma = @idCronograma AND idGestionActividad = @idGestionActividad

			SET @Promedio = (SELECT ISNULL(AVG(Porcentaje),0) FROM ReportesApp_Seguridad_GestionSeguridad_Cronograma WHERE idGestionActividad = @idGestionActividad) 

			UPDATE ReportesApp_Seguridad_GestionSeguridad_Registro
			SET Porcentaje = CAST(@Promedio AS DECIMAL(10,2)), UsuarioModificacion = @Usuario, FechaModificacion = GETDATE() --CAST((CASE WHEN Meta = 0.00 THEN 0.00 ELSE (@MetaUsuario/Meta)*100 END) AS DECIMAL(10,2))
			WHERE idGestionActividad = @idGestionActividad
		END

		IF (@TipoCronograma = 'META TOTAL') BEGIN
			UPDATE ReportesApp_Seguridad_GestionSeguridad_Cronograma
			SET ValorCumplido = @MetaUsuario, Estado = 'COMPLETADO', Porcentaje = @MetaUsuario
			WHERE idCronograma = @idCronograma AND idGestionActividad = @idGestionActividad

			SET @Promedio = (SELECT ISNULL(AVG(Porcentaje),0) FROM ReportesApp_Seguridad_GestionSeguridad_Cronograma WHERE idGestionActividad = @idGestionActividad) 
			SET @MetaExcelente = (SELECT TOP(1) Meta FROM ReportesApp_Seguridad_GestionSeguridad_Cronograma WHERE idGestionActividad = @idGestionActividad ORDER BY idCronograma ASC)
			SET @MetaAceptable = (SELECT TOP(1) Meta2 FROM ReportesApp_Seguridad_GestionSeguridad_Cronograma WHERE idGestionActividad = @idGestionActividad ORDER BY idCronograma ASC)

			UPDATE ReportesApp_Seguridad_GestionSeguridad_Registro
			SET Porcentaje = (CASE WHEN @Promedio >= @MetaExcelente THEN 100
							   WHEN @Promedio >= @MetaAceptable AND @Promedio < @MetaExcelente THEN 60
							   ELSE 0 END)
			WHERE idGestionActividad = @idGestionActividad
		END
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR CRONOGRAMA
		DELETE FROM ReportesApp_Seguridad_GestionSeguridad_Cronograma
		WHERE idCronograma = @idCronograma AND idGestionActividad = @idGestionActividad

		SET @Promedio = (SELECT ISNULL(AVG(Porcentaje),0) FROM ReportesApp_Seguridad_GestionSeguridad_Cronograma WHERE idGestionActividad = @idGestionActividad) 

		IF (@TipoCronograma = 'META TOTAL') BEGIN
			SET @MetaExcelente = (SELECT TOP(1) Meta FROM ReportesApp_Seguridad_GestionSeguridad_Cronograma WHERE idGestionActividad = @idGestionActividad ORDER BY idCronograma ASC)
			SET @MetaAceptable = (SELECT TOP(1) Meta2 FROM ReportesApp_Seguridad_GestionSeguridad_Cronograma WHERE idGestionActividad = @idGestionActividad ORDER BY idCronograma ASC)

			UPDATE ReportesApp_Seguridad_GestionSeguridad_Registro
			SET Porcentaje = (CASE WHEN @Promedio >= @MetaExcelente THEN 100
							   WHEN @Promedio >= @MetaAceptable AND @Promedio < @MetaExcelente THEN 60
							   ELSE 0 END)
			WHERE idGestionActividad = @idGestionActividad
		END
		ELSE BEGIN
			UPDATE ReportesApp_Seguridad_GestionSeguridad_Registro
			SET Porcentaje = CAST(@Promedio AS DECIMAL(10,2))
			WHERE idGestionActividad = @idGestionActividad
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

TERMINAR:
IF LEFT(@Exito,1)='-' OR cast(left(@Exito,1) AS INT)>0 BEGIN
	DECLARE @storedProcedureName VARCHAR(100)
	SELECT @storedProcedureName = OBJECT_NAME(@@PROCID)
	SET @exito = @exito + CHAR(13)+CHAR(13)+'SP Name: '+@storedProcedureName
END

SELECT @exito exito

-------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 04-03-2024
-- Description:	LISTAR CRONOGRAMA ACTIVIDADES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_GestionSeguridad_ListarCronogramaActividades]
@idGestionActividad INT,
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@TipoCronograma VARCHAR(30)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@TipoCronograma = 'DIARIA' OR @TipoCronograma = 'AVANCE') BEGIN
		SELECT C.idCronograma, C.idGestionActividad, CONVERT(VARCHAR,C.Fecha,103) AS 'FECHA', C.Estado AS 'ESTADO', C.Meta AS 'META',
		C.ValorCumplido AS 'VALOR_CUMPLIDO', C.Porcentaje AS 'PORCENTAJE'
		FROM ReportesApp_Seguridad_GestionSeguridad_Cronograma C
		WHERE C.idGestionActividad = @idGestionActividad AND (C.Fecha BETWEEN @FINICIO AND @FFIN)
		ORDER BY C.Fecha DESC
	END
	
	IF (@TipoCronograma = 'META TOTAL') BEGIN
		SELECT C.idCronograma, C.idGestionActividad, CONVERT(VARCHAR,C.Fecha,103) AS 'FECHA', C.Estado AS 'ESTADO', C.Meta AS 'M_EXCELENTE',
		C.Meta2 AS 'M_ACEPTABLE', C.ValorCumplido AS 'VALOR_CUMPLIDO', C.Porcentaje AS 'PORCENTAJE'
		FROM ReportesApp_Seguridad_GestionSeguridad_Cronograma C
		WHERE C.idGestionActividad = @idGestionActividad AND (C.Fecha BETWEEN @FINICIO AND @FFIN)
		ORDER BY C.Fecha DESC
	END
END