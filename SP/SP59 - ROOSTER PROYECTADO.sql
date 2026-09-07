
-- CREAR TABLA ReportesApp_Mantenimiento_RoosterProyectado_PersonalView

-- CREAR TABLA ReportesApp_Mantenimiento_RoosterProyectado_Registro

-- CREAR TABLA ReportesApp_Mantenimiento_RoosterProyectado_Estados

--------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 21-06-2024
-- Description:	INGRESAR Y ELIMINAR COMPENSACIONES
-- =============================================
/*
EXEC ReportesApp_Mantenimiento_AsignacionOT_IngresarEliminarComp @Opcion = 1, @CodigoComp = ' ', @Persona = 20701, @CFechaIni = '07/10/2024',
@CHoraIni = '15:00:00', @CFechaFin = '07/10/2024', @CHoraFin = '19:30:00', @TotalHE = '15:09:40', @usuario = 'GREYES'
*/
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_AsignacionOT_IngresarEliminarComp]
@Opcion INT,
@CodigoComp VARCHAR(250),
@Persona INT,
@CFechaIni VARCHAR(20),
@CHoraIni VARCHAR(20),
@CFechaFin VARCHAR(20),
@CHoraFin VARCHAR(20),
@TotalHE VARCHAR(30),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @FechaIni DATETIME
DECLARE @FechaFin DATETIME
DECLARE @Correlativo INT

SET @Exito = '0 = Horas Extras Compensadas.'

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- INGRESAR COMPENSACION
		SET @Correlativo = (SELECT MAX(idCompensar) FROM ReportesApp_Mantenimiento_AsignacionOT_Compensaciones WHERE Anio = YEAR(GETDATE()))
		SET @Correlativo = ISNULL(@Correlativo,0) + 1

		SET @FechaIni = @CFechaIni+' '+@CHoraIni
		SET @FechaFin = @CFechaFin+' '+@CHoraFin

		IF (NOT EXISTS(SELECT * FROM ReportesApp_RRHH_AsistenciaView WHERE Anio = YEAR(@FechaFin) AND Mes = MONTH(@FechaFin)
		AND CodPlanilla = 'OB')) BEGIN
			SET @Exito = '-2 = Por favor, genere el registro de asistencias del personal antes de compensar.'
			ROLLBACK
			GOTO Terminar
		END

		IF (CONVERT(DATETIME,@FechaIni) > CONVERT(DATETIME,@FechaFin)) BEGIN
			SET @Exito = '-1 = La fecha de inicio no puede ser mayor a la fecha fin.'
			ROLLBACK
			GOTO Terminar
		END
		ELSE BEGIN
			INSERT INTO ReportesApp_Mantenimiento_AsignacionOT_Compensaciones(idCompensar,Anio,CodigoComp,Persona,TotalHE,FechaInicio,FechaFin,TiempoComp,UsuarioRegistro,FechaRegistro)
			VALUES(@Correlativo,YEAR(GETDATE()),CONVERT(VARCHAR,YEAR(GETDATE()))+'-'+CONVERT(VARCHAR,@Correlativo),@Persona,@TotalHE,@FechaIni,@FechaFin,
			CONVERT(VARCHAR,CONVERT(TIME,@FechaFin - @FechaIni),108),@Usuario,GETDATE())

			DECLARE @ContadorFecha DATE = CONVERT(DATE, @FechaIni)

			WHILE (@ContadorFecha <= CONVERT(DATE,@FechaFin)) BEGIN
				DELETE FROM ReportesApp_RRHH_Asistencia
				WHERE IDPersona = @Persona AND Fecha = @ContadorFecha
				
				INSERT INTO ReportesApp_RRHH_Asistencia(IdPersona,Planilla,Fecha,IdTipoAsist,ConceptoAcceso,FHRegistra,UserCrea)
				SELECT DISTINCT @Persona, 'OB', @ContadorFecha, 61, 'COH', GETDATE(), @Usuario

				UPDATE V 
				SET V.D1 = CASE WHEN DAY(@ContadorFecha) = 1 THEN 'COH' ELSE V.D1 END,
					V.D2 = CASE WHEN DAY(@ContadorFecha) = 2 THEN 'COH' ELSE V.D2 END,
					V.D3 = CASE WHEN DAY(@ContadorFecha) = 3 THEN 'COH' ELSE V.D3 END,
					V.D4 = CASE WHEN DAY(@ContadorFecha) = 4 THEN 'COH' ELSE V.D4 END,
					V.D5 = CASE WHEN DAY(@ContadorFecha) = 5 THEN 'COH' ELSE V.D5 END,
					V.D6 = CASE WHEN DAY(@ContadorFecha) = 6 THEN 'COH' ELSE V.D6 END,
					V.D7 = CASE WHEN DAY(@ContadorFecha) = 7 THEN 'COH' ELSE V.D7 END,
					V.D8 = CASE WHEN DAY(@ContadorFecha) = 8 THEN 'COH' ELSE V.D8 END,
					V.D9 = CASE WHEN DAY(@ContadorFecha) = 9 THEN 'COH' ELSE V.D9 END,
					V.D10 = CASE WHEN DAY(@ContadorFecha) = 10 THEN 'COH' ELSE V.D10 END,
					V.D11 = CASE WHEN DAY(@ContadorFecha) = 11 THEN 'COH' ELSE V.D11 END,
					V.D12 = CASE WHEN DAY(@ContadorFecha) = 12 THEN 'COH' ELSE V.D12 END,
					V.D13 = CASE WHEN DAY(@ContadorFecha) = 13 THEN 'COH' ELSE V.D13 END,
					V.D14 = CASE WHEN DAY(@ContadorFecha) = 14 THEN 'COH' ELSE V.D14 END,
					V.D15 = CASE WHEN DAY(@ContadorFecha) = 15 THEN 'COH' ELSE V.D15 END,
					V.D16 = CASE WHEN DAY(@ContadorFecha) = 16 THEN 'COH' ELSE V.D16 END,
					V.D17 = CASE WHEN DAY(@ContadorFecha) = 17 THEN 'COH' ELSE V.D17 END,
					V.D18 = CASE WHEN DAY(@ContadorFecha) = 18 THEN 'COH' ELSE V.D18 END,
					V.D19 = CASE WHEN DAY(@ContadorFecha) = 19 THEN 'COH' ELSE V.D19 END,
					V.D20 = CASE WHEN DAY(@ContadorFecha) = 20 THEN 'COH' ELSE V.D20 END,
					V.D21 = CASE WHEN DAY(@ContadorFecha) = 21 THEN 'COH' ELSE V.D21 END,
					V.D22 = CASE WHEN DAY(@ContadorFecha) = 22 THEN 'COH' ELSE V.D22 END,
					V.D23 = CASE WHEN DAY(@ContadorFecha) = 23 THEN 'COH' ELSE V.D23 END,
					V.D24 = CASE WHEN DAY(@ContadorFecha) = 24 THEN 'COH' ELSE V.D24 END,
					V.D25 = CASE WHEN DAY(@ContadorFecha) = 25 THEN 'COH' ELSE V.D25 END,
					V.D26 = CASE WHEN DAY(@ContadorFecha) = 26 THEN 'COH' ELSE V.D26 END,
					V.D27 = CASE WHEN DAY(@ContadorFecha) = 27 THEN 'COH' ELSE V.D27 END,
					V.D28 = CASE WHEN DAY(@ContadorFecha) = 28 THEN 'COH' ELSE V.D28 END,
					V.D29 = CASE WHEN DAY(@ContadorFecha) = 29 THEN 'COH' ELSE V.D29 END,
					V.D30 = CASE WHEN DAY(@ContadorFecha) = 30 THEN 'COH' ELSE V.D30 END,
					V.D31 = CASE WHEN DAY(@ContadorFecha) = 31 THEN 'COH' ELSE V.D31 END
				FROM ReportesApp_RRHH_AsistenciaView V
				WHERE V.IDPersona = @Persona AND V.Anio = YEAR(@ContadorFecha) AND V.Mes = MONTH(@ContadorFecha)
				AND V.CodPlanilla = 'OB'

				SET @ContadorFecha = DATEADD(DAY,1,@ContadorFecha)
			END
		END
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR COMPENSACIÓN
		DECLARE @FechaInicioC DATE = (SELECT FechaInicio FROM ReportesApp_Mantenimiento_AsignacionOT_Compensaciones WHERE CodigoComp = @CodigoComp AND Persona = @Persona)
		DECLARE @FechaFinC DATE = (SELECT FechaFin FROM ReportesApp_Mantenimiento_AsignacionOT_Compensaciones WHERE CodigoComp = @CodigoComp AND Persona = @Persona)
		DECLARE @ContadorFechaC DATE = CONVERT(DATE,@FechaInicioC)

		WHILE (@ContadorFechaC <= CONVERT(DATE,@FechaFinC)) BEGIN
			DELETE FROM ReportesApp_RRHH_Asistencia
			WHERE IDPersona = @Persona AND Fecha = @ContadorFechaC
			
			UPDATE V 
			SET V.D1 = CASE WHEN DAY(@ContadorFechaC) = 1 THEN NULL ELSE V.D1 END,
				V.D2 = CASE WHEN DAY(@ContadorFechaC) = 2 THEN NULL ELSE V.D2 END,
				V.D3 = CASE WHEN DAY(@ContadorFechaC) = 3 THEN NULL ELSE V.D3 END,
				V.D4 = CASE WHEN DAY(@ContadorFechaC) = 4 THEN NULL ELSE V.D4 END,
				V.D5 = CASE WHEN DAY(@ContadorFechaC) = 5 THEN NULL ELSE V.D5 END,
				V.D6 = CASE WHEN DAY(@ContadorFechaC) = 6 THEN NULL ELSE V.D6 END,
				V.D7 = CASE WHEN DAY(@ContadorFechaC) = 7 THEN NULL ELSE V.D7 END,
				V.D8 = CASE WHEN DAY(@ContadorFechaC) = 8 THEN NULL ELSE V.D8 END,
				V.D9 = CASE WHEN DAY(@ContadorFechaC) = 9 THEN NULL ELSE V.D9 END,
				V.D10 = CASE WHEN DAY(@ContadorFechaC) = 10 THEN NULL ELSE V.D10 END,
				V.D11 = CASE WHEN DAY(@ContadorFechaC) = 11 THEN NULL ELSE V.D11 END,
				V.D12 = CASE WHEN DAY(@ContadorFechaC) = 12 THEN NULL ELSE V.D12 END,
				V.D13 = CASE WHEN DAY(@ContadorFechaC) = 13 THEN NULL ELSE V.D13 END,
				V.D14 = CASE WHEN DAY(@ContadorFechaC) = 14 THEN NULL ELSE V.D14 END,
				V.D15 = CASE WHEN DAY(@ContadorFechaC) = 15 THEN NULL ELSE V.D15 END,
				V.D16 = CASE WHEN DAY(@ContadorFechaC) = 16 THEN NULL ELSE V.D16 END,
				V.D17 = CASE WHEN DAY(@ContadorFechaC) = 17 THEN NULL ELSE V.D17 END,
				V.D18 = CASE WHEN DAY(@ContadorFechaC) = 18 THEN NULL ELSE V.D18 END,
				V.D19 = CASE WHEN DAY(@ContadorFechaC) = 19 THEN NULL ELSE V.D19 END,
				V.D20 = CASE WHEN DAY(@ContadorFechaC) = 20 THEN NULL ELSE V.D20 END,
				V.D21 = CASE WHEN DAY(@ContadorFechaC) = 21 THEN NULL ELSE V.D21 END,
				V.D22 = CASE WHEN DAY(@ContadorFechaC) = 22 THEN NULL ELSE V.D22 END,
				V.D23 = CASE WHEN DAY(@ContadorFechaC) = 23 THEN NULL ELSE V.D23 END,
				V.D24 = CASE WHEN DAY(@ContadorFechaC) = 24 THEN NULL ELSE V.D24 END,
				V.D25 = CASE WHEN DAY(@ContadorFechaC) = 25 THEN NULL ELSE V.D25 END,
				V.D26 = CASE WHEN DAY(@ContadorFechaC) = 26 THEN NULL ELSE V.D26 END,
				V.D27 = CASE WHEN DAY(@ContadorFechaC) = 27 THEN NULL ELSE V.D27 END,
				V.D28 = CASE WHEN DAY(@ContadorFechaC) = 28 THEN NULL ELSE V.D28 END,
				V.D29 = CASE WHEN DAY(@ContadorFechaC) = 29 THEN NULL ELSE V.D29 END,
				V.D30 = CASE WHEN DAY(@ContadorFechaC) = 30 THEN NULL ELSE V.D30 END,
				V.D31 = CASE WHEN DAY(@ContadorFechaC) = 31 THEN NULL ELSE V.D31 END
			FROM ReportesApp_RRHH_AsistenciaView V
			WHERE V.IDPersona = @Persona AND V.Anio = YEAR(@ContadorFechaC) AND V.Mes = MONTH(@ContadorFechaC)
			AND V.CodPlanilla = 'OB'
			
			SET @ContadorFechaC = DATEADD(DAY,1,@ContadorFechaC)
		END

		DELETE FROM ReportesApp_Mantenimiento_AsignacionOT_Compensaciones
		WHERE CodigoComp = @CodigoComp AND Persona = @Persona
		
		SET @Exito = '0 = Compensación eliminada.'
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

-------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author Name:	GERARDO REYES
-- Create date: 09/10/2024
-- Description:	LISTAR PERSONAL DE MANTENIMIENTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_RoosterProyectado_ListarMecanicos]
@Opcion INT,
@Periodo VARCHAR(6)
AS
BEGIN
	IF (@Opcion = 1) BEGIN		-- LISTAR PERSONAL DE MTTO
		SELECT DISTINCT P.Persona, LTRIM(RTRIM(P.NombreCompleto)) AS 'NOMBRE', LTRIM(RTRIM(P.Documento)) AS 'DNI', DEP.Description AS 'AREA',
		PE.Descripcion AS 'PUESTO', E.FechaIngreso AS 'F.INGRESO'
		FROM PersonaMast P
		LEFT JOIN EmpleadoMast E ON E.Empleado = P.Persona
		LEFT JOIN hr_puestoempresa PE on PE.CodigoPuesto = E.CodigoCargo
		LEFT JOIN departmentmst DEP on E.DeptoOrganizacion = DEP.Department
		LEFT JOIN ReportesApp_Mantenimiento_RoosterProyectado_PersonalView V ON V.IDPersona = E.Empleado
																		     AND V.Anio = RIGHT(@Periodo,4) AND V.Mes = LEFT(@Periodo,2)
		WHERE E.Estado = 'A' AND E.TipoPlanilla = 'OB' AND LTRIM(RTRIM(DEP.Description)) = 'MANTENIMIENTO' AND V.IDPersona IS NULL
		ORDER BY LTRIM(RTRIM(P.NombreCompleto))
	END

	IF (@Opcion = 2) BEGIN		-- LISTAR MAPEADOS
		SELECT DISTINCT P.Persona, LTRIM(RTRIM(P.NombreCompleto)) AS 'NOMBRE', LTRIM(RTRIM(P.Documento)) AS 'DNI', DEP.Description AS 'AREA',
		PE.Descripcion AS 'PUESTO', E.FechaIngreso AS 'F.INGRESO'
		FROM ReportesApp_Mantenimiento_RoosterProyectado_PersonalView V
		LEFT JOIN PersonaMast P ON P.Persona = V.IDPersona
		LEFT JOIN EmpleadoMast E ON E.Empleado = P.Persona
		LEFT JOIN hr_puestoempresa PE on PE.CodigoPuesto = E.CodigoCargo
		LEFT JOIN departmentmst DEP on E.DeptoOrganizacion = DEP.Department
		WHERE E.Estado = 'A' and V.Anio = RIGHT(@Periodo,4) AND V.Mes = LEFT(@Periodo,2)
		ORDER BY 2
	END

	IF (@Opcion = 3) BEGIN		-- LISTAR ESTADOS
		SELECT Codigo, Estado FROM ReportesApp_Mantenimiento_RoosterProyectado_Estados
		ORDER BY idEstado
	END
END

----------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author Name:	GERARDO REYES
-- Create date: 09/10/2024
-- Description:	MAPEAR PERSONAL DE MANTENIMIENTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_RoosterProyectado_MapearMecanicos]
@IDPersona INT,
@Periodo VARCHAR(6),
@Usuario VARCHAR(20)
AS
DECLARE @exito VARCHAR(MAX)
DECLARE @Dia DATE
DECLARE @FechaIni DATE, @FechaFin DATE
DECLARE @DOMINGOS TABLE(FECHA DATE)
DECLARE @T_PersonalDF TABLE(IDPersona INT, Nombres VARCHAR(250), FechaIni DATE, FechaFin DATE, FechaDF DATE, ItemDF INT)
DECLARE @xFHRegistra DATETIME = GETDATE()
DECLARE @Domingo DATE

SET @exito = '0 = Mapeo generado correctamente.'

SET @Dia = RIGHT(@Periodo,4)+'-'+LEFT(@Periodo,2)+'-01'
SET @FechaIni = DATEADD(mm,DATEDIFF(mm,0,@Dia),0)
SET @FechaFin = CAST(DATEADD(MONTH,DATEDIFF(MONTH,0,@Dia)+1,0)-1 AS DATE)
SET @Domingo = DATEADD(DAY, 7-DATEPART(DW,@FechaIni),@FechaIni)

WHILE @Domingo <= @FechaFin BEGIN
	INSERT INTO @DOMINGOS (FECHA)
	SELECT @Domingo

	SET @Domingo = DATEADD(DAY,7,@Domingo)
END

INSERT INTO @DOMINGOS(FECHA)
SELECT RIGHT(FechaMesDia,4)+'-'+SUBSTRING(FechaMesDia,3,2)+'-'+LEFT(FechaMesDia,2)
FROM PR_CalendarioFeriados
WHERE RIGHT(FechaMesDia,6) = @Periodo

INSERT INTO @T_PersonalDF(IDPersona,Nombres,FechaIni,FechaFin,FechaDF,ItemDF)

SELECT X.IdPersona, X.Nombre, X.FechaDesde, X.FechaHasta, X.FECHA, ROW_NUMBER() OVER(ORDER BY X.IdPersona,X.FECHA) ItemDF
FROM (SELECT P.Persona IdPersona, P.NombreCompleto Nombre, @FechaIni FechaDesde, @FechaFin FechaHasta, D.FECHA,
ROW_NUMBER() OVER (PARTITION BY P.Persona, D.FECHA ORDER BY D.FECHA DESC) Orden
FROM PersonaMast P WITH(NOLOCK)
LEFT JOIN empleadomast PER ON PER.Empleado = P.Persona
INNER JOIN @DOMINGOS D ON @FechaIni <= D.FECHA AND @FechaFin >= D.FECHA
WHERE (PER.Estado = 'A') AND (P.Persona = @IdPersona)) X
WHERE X.Orden = 1

BEGIN TRAN
BEGIN TRY
	/*
	IF @Usuario NOT IN ('GREYES') BEGIN
		SET @exito = '-10 = No tiene acceso a esta operación.'
		ROLLBACK
		GOTO Terminar
	END
	*/

	DECLARE @n INT, @UltimoDiaMes TINYINT = DAY(@fechafin), @IDPersonaDF INT, @FechaDF DATE

	SELECT @n = MIN(ItemDF) FROM @T_PersonalDF

	IF (@n IS NULL) BEGIN
		INSERT INTO ReportesApp_Mantenimiento_RoosterProyectado_PersonalView (IDPersona,Anio,Mes,NroDias)
		VALUES(@IDPersona, RIGHT(@Periodo,4), LEFT(@Periodo,2), @UltimoDiaMes)
	END

	WHILE (@n IS NOT NULL) BEGIN
		SELECT @IDPersonaDF = T.IDPersona, @FechaDF = T.FechaDF
		FROM @T_PersonalDF T
		WHERE ItemDF = @n

		IF NOT EXISTS(SELECT TOP 1 Mes FROM ReportesApp_Mantenimiento_RoosterProyectado_PersonalView WITH(NOLOCK) WHERE IDPersona = @IDPersonaDF 
		AND Anio=YEAR(@FechaDF) AND Mes=MONTH(@FechaDF)) BEGIN
			INSERT INTO ReportesApp_Mantenimiento_RoosterProyectado_PersonalView (IDPersona,Anio,Mes,NroDias)
			VALUES(@IDPersonaDF, YEAR(@FechaDF), MONTH(@FechaDF), @UltimoDiaMes)						 
		END

		PRINT CAST(@n AS VARCHAR(10))+ ' --> @IDPersonaDF:'+ CAST(@IDPersonaDF AS VARCHAR(10)) +' @FechaDF: '+CONVERT(VARCHAR(10),@FechaDF,103)+' >>> DF'

		UPDATE ReportesApp_Mantenimiento_RoosterProyectado_PersonalView SET 
			D1 = CASE WHEN DAY(@FechaDF) = 1 THEN 'DF' ELSE D1 END,
			D2 = CASE WHEN DAY(@FechaDF) = 2 THEN 'DF' ELSE D2 END,
			D3 = CASE WHEN DAY(@FechaDF) = 3 THEN 'DF' ELSE D3 END,
			D4 = CASE WHEN DAY(@FechaDF) = 4 THEN 'DF' ELSE D4 END,
			D5 = CASE WHEN DAY(@FechaDF) = 5 THEN 'DF' ELSE D5 END,
			D6 = CASE WHEN DAY(@FechaDF) = 6 THEN 'DF' ELSE D6 END,
			D7 = CASE WHEN DAY(@FechaDF) = 7 THEN 'DF' ELSE D7 END,
			D8 = CASE WHEN DAY(@FechaDF) = 8 THEN 'DF' ELSE D8 END,
			D9 = CASE WHEN DAY(@FechaDF) = 9 THEN 'DF' ELSE D9 END,
			D10 = CASE WHEN DAY(@FechaDF) = 10 THEN 'DF' ELSE D10 END,
			D11 = CASE WHEN DAY(@FechaDF) = 11 THEN 'DF' ELSE D11 END,
			D12 = CASE WHEN DAY(@FechaDF) = 12 THEN 'DF' ELSE D12 END,
			D13 = CASE WHEN DAY(@FechaDF) = 13 THEN 'DF' ELSE D13 END,
			D14 = CASE WHEN DAY(@FechaDF) = 14 THEN 'DF' ELSE D14 END,
			D15 = CASE WHEN DAY(@FechaDF) = 15 THEN 'DF' ELSE D15 END,
			D16 = CASE WHEN DAY(@FechaDF) = 16 THEN 'DF' ELSE D16 END,
			D17 = CASE WHEN DAY(@FechaDF) = 17 THEN 'DF' ELSE D17 END,
			D18 = CASE WHEN DAY(@FechaDF) = 18 THEN 'DF' ELSE D18 END,
			D19 = CASE WHEN DAY(@FechaDF) = 19 THEN 'DF' ELSE D19 END,
			D20 = CASE WHEN DAY(@FechaDF) = 20 THEN 'DF' ELSE D20 END,
			D21 = CASE WHEN DAY(@FechaDF) = 21 THEN 'DF' ELSE D21 END,
			D22 = CASE WHEN DAY(@FechaDF) = 22 THEN 'DF' ELSE D22 END,
			D23 = CASE WHEN DAY(@FechaDF) = 23 THEN 'DF' ELSE D23 END,
			D24 = CASE WHEN DAY(@FechaDF) = 24 THEN 'DF' ELSE D24 END,
			D25 = CASE WHEN DAY(@FechaDF) = 25 THEN 'DF' ELSE D25 END,
			D26 = CASE WHEN DAY(@FechaDF) = 26 THEN 'DF' ELSE D26 END,
			D27 = CASE WHEN DAY(@FechaDF) = 27 THEN 'DF' ELSE D27 END,
			D28 = CASE WHEN DAY(@FechaDF) = 28 THEN 'DF' ELSE D28 END,
			D29 = CASE WHEN DAY(@FechaDF) = 29 THEN 'DF' ELSE D29 END,
			D30 = CASE WHEN DAY(@FechaDF) = 30 THEN 'DF' ELSE D30 END,
			D31 = CASE WHEN DAY(@FechaDF) = 31 THEN 'DF' ELSE D31 END
		WHERE IDPersona = @IDPersonaDF AND Anio = YEAR(@FechaDF) AND Mes = MONTH(@FechaDF)

		SELECT @n = MIN(ItemDF) FROM @T_PersonalDF WHERE @n < ItemDF
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
IF LEFT(@Exito,1)='-' OR cast(left(@Exito,1) as int)>0 
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
-- Create date: 10/10/2024
-- Description:	QUITAR MECÁNICOS MAPEADOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_RoosterProyectado_QuitarMecánicos]
@IDPersona INT,
@Periodo VARCHAR(6),
@Usuario VARCHAR(20)
AS
DECLARE @exito VARCHAR(MAX)

SET @exito = '0 = Mecánicos eliminados correctamente.'

BEGIN TRAN
BEGIN TRY
	DELETE FROM ReportesApp_Mantenimiento_RoosterProyectado_PersonalView
	WHERE IDPersona = @IDPersona
	AND Anio = CONVERT(INT,RIGHT(@Periodo,4)) AND Mes = CONVERT(INT,LEFT(@Periodo,2))

	DELETE FROM ReportesApp_Mantenimiento_RoosterProyectado_Registro
	WHERE IDPersona = @IDPersona AND MONTH(Fecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(Fecha) = CONVERT(INT,RIGHT(@Periodo,4))
END TRY

BEGIN CATCH
	SET @exito = RTRIM(LTRIM(STR(ERROR_NUMBER()))) + '=' + ERROR_MESSAGE()
	ROLLBACK
	GOTO Terminar
END CATCH

IF @@TRANCOUNT > 0
	COMMIT;

TERMINAR:
IF LEFT(@Exito,1)='-' OR cast(left(@Exito,1) as int)>0 
BEGIN
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
-- Author Name:	GERARDO REYES
-- Create date: 10/10/2024
-- Description:	LISTAR TABLA DE MECÁNICOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_RoosterProyectado_ListarTablaMecanicos]
@Periodo VARCHAR(6),
@Nombre VARCHAR(250),
@Puesto VARCHAR(250)
AS
BEGIN
	DECLARE @Anio SMALLINT = RIGHT(@periodo,4)
	DECLARE @Mes INT = LEFT(@periodo,2)
	DECLARE @DiaInicio INT = 1
	DECLARE @CantDias INT = 0
	DECLARE @FechaIniArmada VARCHAR(10) = '01/'+LEFT(@Periodo,2)+'/'+RIGHT(@Periodo,4)
	DECLARE @FDesde DATE, @FHasta DATE

	SET @FDesde = @FechaIniArmada
	SET @FHasta = DATEADD(ms,-3,DATEADD(mm,0,DATEADD(mm,DATEDIFF(mm,0, @FDesde)+1,0)))

	IF (@FDesde IS NULL) BEGIN
		SET @DiaInicio = 27
	END
	ELSE BEGIN  
		SET @DiaInicio = DAY(@FDesde) 
		SET @CantDias = DATEDIFF(DAY,@FDesde,@FHasta)+1
	END

	DECLARE @T_Prueba TABLE (IDPersona INT, Nombre VARCHAR(250), Puesto VARCHAR(100),
							 D1 VARCHAR(15), D2 VARCHAR(15), D3 VARCHAR(15), D4 VARCHAR(15), D5 VARCHAR(15), D6 VARCHAR(15),
							 D7 VARCHAR(15), D8 VARCHAR(15), D9 VARCHAR(15), D10 VARCHAR(15), D11 VARCHAR(15), D12 VARCHAR(15),
							 D13 VARCHAR(15), D14 VARCHAR(15), D15 VARCHAR(15), D16 VARCHAR(15), D17 VARCHAR(15), D18 VARCHAR(15),
							 D19 VARCHAR(15), D20 VARCHAR(15), D21 VARCHAR(15), D22 VARCHAR(15), D23 VARCHAR(15), D24 VARCHAR(15),
							 D25 VARCHAR(15), D26 VARCHAR(15), D27 VARCHAR(15), D28 VARCHAR(15), D29 VARCHAR(15), D30 VARCHAR(15), D31 VARCHAR(15))

	DECLARE @D1 VARCHAR(15), @D2 VARCHAR(15), @D3 VARCHAR(15), @D4 VARCHAR(15), @D5 VARCHAR(15), @D6 VARCHAR(15), @D7 VARCHAR(15), @D8 VARCHAR(15),
			@D9 VARCHAR(15), @D10 VARCHAR(15), @D11 VARCHAR(15), @D12 VARCHAR(15), @D13 VARCHAR(15), @D14 VARCHAR(15), @D15 VARCHAR(15), @D16 VARCHAR(15),
			@D17 VARCHAR(15), @D18 VARCHAR(15), @D19 VARCHAR(15), @D20 VARCHAR(15), @D21 VARCHAR(15), @D22 VARCHAR(15), @D23 VARCHAR(15), @D24 VARCHAR(15),
			@D25 VARCHAR(15), @D26 VARCHAR(15), @D27 VARCHAR(15), @D28 VARCHAR(15), @D29 VARCHAR(15), @D30 VARCHAR(15), @D31 VARCHAR(15)

	INSERT INTO @T_Prueba (Puesto,Nombre)
	VALUES ('Puesto', '<NOMBRE>')

	DECLARE @FechaIni DATE, @FechaFin DATE, @FechaVer DATE

	IF (@FDesde IS NOT NULL) BEGIN 
		SET @FechaVer = CAST(CAST(@DiaInicio As Varchar(2))+'/'+ RIGHT('0'+ CAST(month(@FDesde) AS VARCHAR(2)),2)+'/'+CAST(@Anio as VARCHAR(4)) AS DATE)
	END
	ELSE BEGIN
		SET @FechaVer = CAST(CAST(@DiaInicio As Varchar(2))+'/'+ RIGHT('0'+ CAST(@Mes AS VARCHAR(2)),2)+'/'+CAST(@Anio as VARCHAR(4)) AS DATE)
	END

	SET @FechaIni = @FechaVer
	SET @FechaFin = DATEADD(D,-1, DATEADD(MONTH,1,@FechaVer))

	INSERT INTO @T_Prueba (IDPersona, Nombre, Puesto)
	SELECT DISTINCT P.Persona, LTRIM(RTRIM(P.NombreCompleto)) AS 'NOMBRE', PE.Descripcion AS 'PUESTO'
	FROM PersonaMast P
	LEFT JOIN EmpleadoMast E ON E.Empleado = P.Persona
	LEFT JOIN hr_puestoempresa PE on PE.CodigoPuesto = E.CodigoCargo
	INNER JOIN ReportesApp_Mantenimiento_RoosterProyectado_PersonalView V ON P.Persona = V.IDPersona
	WHERE (E.Estado = 'A') AND V.Anio = RIGHT(@Periodo,4) AND V.Mes = LEFT(@Periodo,2) AND
	(LTRIM(RTRIM(P.NombreCompleto)) IS NULL OR LTRIM(RTRIM(P.NombreCompleto)) LIKE '%' + @Nombre + '%') AND
	(LTRIM(RTRIM(PE.Descripcion)) IS NULL OR LTRIM(RTRIM(PE.Descripcion)) LIKE '%' + @Puesto + '%')

	SET @FechaVer = @FechaIni

	DECLARE @NroDia INT
	DECLARE @DiaMes INT
	DECLARE @Concatenado VARCHAR(15)
	
	SET @NroDia = 1

	WHILE @FechaVer <= @FechaFin BEGIN
		SET @Concatenado = LEFT(UPPER(DATENAME(WEEKDAY, @FechaVer)), 3) + ''  + RIGHT( '0' + CAST(DAY(@FechaVer) AS VARCHAR(2)),2)

		UPDATE @T_Prueba
		SET D1 = CASE WHEN @NroDia = 1 THEN @Concatenado ELSE D1 END,
			D2 = CASE WHEN @NroDia = 2 THEN @Concatenado ELSE D2 END,
			D3 = CASE WHEN @NroDia = 3 THEN @Concatenado ELSE D3 END,
			D4 = CASE WHEN @NroDia = 4 THEN @Concatenado ELSE D4 END,
			D5 = CASE WHEN @NroDia = 5 THEN @Concatenado ELSE D5 END,
			D6 = CASE WHEN @NroDia = 6 THEN @Concatenado ELSE D6 END,
			D7 = CASE WHEN @NroDia = 7 THEN @Concatenado ELSE D7 END,
			D8 = CASE WHEN @NroDia = 8 THEN @Concatenado ELSE D8 END,
			D9 = CASE WHEN @NroDia = 9 THEN @Concatenado ELSE D9 END,
			D10 = CASE WHEN @NroDia = 10 THEN @Concatenado ELSE D10 END,
			D11 = CASE WHEN @NroDia = 11 THEN @Concatenado ELSE D11 END,
			D12 = CASE WHEN @NroDia = 12 THEN @Concatenado ELSE D12 END,
			D13 = CASE WHEN @NroDia = 13 THEN @Concatenado ELSE D13 END,
			D14 = CASE WHEN @NroDia = 14 THEN @Concatenado ELSE D14 END,
			D15 = CASE WHEN @NroDia = 15 THEN @Concatenado ELSE D15 END,
			D16 = CASE WHEN @NroDia = 16 THEN @Concatenado ELSE D16 END,
			D17 = CASE WHEN @NroDia = 17 THEN @Concatenado ELSE D17 END,
			D18 = CASE WHEN @NroDia = 18 THEN @Concatenado ELSE D18 END,
			D19 = CASE WHEN @NroDia = 19 THEN @Concatenado ELSE D19 END,
			D20 = CASE WHEN @NroDia = 20 THEN @Concatenado ELSE D20 END,
			D21 = CASE WHEN @NroDia = 21 THEN @Concatenado ELSE D21 END,
			D22 = CASE WHEN @NroDia = 22 THEN @Concatenado ELSE D22 END,
			D23 = CASE WHEN @NroDia = 23 THEN @Concatenado ELSE D23 END,
			D24 = CASE WHEN @NroDia = 24 THEN @Concatenado ELSE D24 END,
			D25 = CASE WHEN @NroDia = 25 THEN @Concatenado ELSE D25 END,
			D26 = CASE WHEN @NroDia = 26 THEN @Concatenado ELSE D26 END,
			D27 = CASE WHEN @NroDia = 27 THEN @Concatenado ELSE D27 END,
			D28 = CASE WHEN @NroDia = 28 THEN @Concatenado ELSE D28 END,
			D29 = CASE WHEN @NroDia = 29 THEN @Concatenado ELSE D29 END,
			D30 = CASE WHEN @NroDia = 30 THEN @Concatenado ELSE D30 END,
			D31 = CASE WHEN @NroDia = 31 THEN @Concatenado ELSE D31 END
		WHERE Nombre = '<NOMBRE>'

		SET @DiaMes = DAY(@FechaVer)

		UPDATE @T_Prueba
		SET D1 = CASE WHEN @NroDia = 1 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D1 END)
		ELSE T.D1 END,

		D2 = CASE WHEN @NroDia =2 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D2 END)
		ELSE T.D2 END,

		D3 = CASE WHEN @NroDia = 3 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D3 END)
		ELSE T.D3 END,

		D4 = CASE WHEN @NroDia = 4 THEN
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D4 END)
		ELSE T.D4 END,

		D5 = CASE WHEN @NroDia = 5 THEN  
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D5 END)
		ELSE T.D5 END,

		D6 = CASE WHEN @NroDia =6 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D6 END)
		ELSE T.D6 END,

		D7 = CASE WHEN @NroDia = 7 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D7 END)
		ELSE T.D7 END,

		D8 = CASE WHEN @NroDia = 8 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D8 END)
		ELSE T.D8 END,

		D9 = CASE WHEN @NroDia = 9 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D9 END)
		ELSE T.D9 END,

		D10 = CASE WHEN @NroDia = 10 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D10 END)
		ELSE T.D10 END,

		D11 = CASE WHEN @NroDia = 11 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D11 END)
		ELSE T.D11 END,

		D12 = CASE WHEN @NroDia = 12 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D12 END)
		ELSE T.D12 END,

		D13 = CASE WHEN @NroDia = 13 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D13 END)
		ELSE T.D13 END,

		D14 = CASE WHEN @NroDia = 14 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D14 END)
		ELSE T.D14 END,

		D15 = CASE WHEN @NroDia = 15 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D15 END)
		ELSE T.D15 END,

		D16 = CASE WHEN @NroDia = 16 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D16 END)
		ELSE T.D16 END,

		D17 = CASE WHEN @NroDia = 17 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D17 END)
		ELSE T.D17 END,

		D18 = CASE WHEN @NroDia = 18 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D18 END)
		ELSE T.D18 END,

		D19 = CASE WHEN @NroDia = 19 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D19 END)
		ELSE T.D19 END,

		D20 = CASE WHEN @NroDia = 20 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D20 END)
		ELSE T.D20 END,

		D21 = CASE WHEN @NroDia = 21 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D21 END)
		ELSE T.D21 END,

		D22 = CASE WHEN @NroDia = 22 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D22 END)
		ELSE T.D22 END,

		D23 = CASE WHEN @NroDia = 23 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D23 END)
		ELSE T.D23 END,

		D24 = CASE WHEN @NroDia = 24 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D24 END)
		ELSE T.D24 END,

		D25 = CASE WHEN @NroDia = 25 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D25 END)
		ELSE T.D25 END,

		D26 = CASE WHEN @NroDia = 26 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D26 END)
		ELSE T.D26 END,

		D27 = CASE WHEN @NroDia = 27 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D27 END)
		ELSE T.D27 END,

		D28 = CASE WHEN @NroDia = 28 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D28 END)
		ELSE T.D28 END,

		D29 = CASE WHEN @NroDia = 29 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D29 END)
		ELSE T.D29 END,

		D30 = CASE WHEN @NroDia = 30 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D30 END)
		ELSE T.D30 END,

		D31 = CASE WHEN @NroDia = 31 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D31 END)
		ELSE T.D31 END
		FROM @T_Prueba AS T 
		LEFT JOIN ReportesApp_Mantenimiento_RoosterProyectado_PersonalView AS A WITH(NOLOCK) ON T.IDPersona = A.IDPersona
		WHERE A.Anio = YEAR(@FechaVer) AND A.Mes = MONTH(@FechaVer) 

		SET @FechaVer = DATEADD(D,1,@FechaVer)
			
		IF (@FDesde IS NOT NULL) AND (@NroDia = @CantDias OR @CantDias = 0)  BREAK;
		
		SET @NroDia = @NroDia + 1
	END

	IF (@CantDias = 28) BEGIN
		SELECT @D1=D1, @D2=D2, @D3=D3, @D4=D4, @D5=D5, @D6=D6, @D7=D7, @D8=D8, @D9=D9, @D10=D10, @D11=D11, @D12=D12, @D13=D13, @D14=D14,
		@D15=D15, @D16=D16, @D17=D17, @D18=D18, @D19=D19, @D20=D20, @D21=D21, @D22=D22, @D23=D23, @D24=D24, @D25=D25, @D26=D26, @D27=D27, @D28=D28
		FROM @T_Prueba WHERE Nombre = '<NOMBRE>'
	END

	IF (@CantDias = 29) BEGIN
		SELECT @D1=D1, @D2=D2, @D3=D3, @D4=D4, @D5=D5, @D6=D6, @D7=D7, @D8=D8, @D9=D9, @D10=D10, @D11=D11, @D12=D12, @D13=D13, @D14=D14, @D15=D15,
		@D16=D16, @D17=D17, @D18=D18, @D19=D19, @D20=D20, @D21=D21, @D22=D22, @D23=D23, @D24=D24, @D25=D25, @D26=D26, @D27=D27, @D28=D28, @D29=D29
		FROM @T_Prueba WHERE Nombre = '<NOMBRE>'
	END

	IF (@CantDias = 30) BEGIN
		SELECT @D1=D1, @D2=D2, @D3=D3, @D4=D4, @D5=D5, @D6=D6, @D7=D7, @D8=D8, @D9=D9, @D10=D10, @D11=D11, @D12=D12, @D13=D13, @D14=D14, @D15=D15,
		@D16=D16, @D17=D17, @D18=D18, @D19=D19, @D20=D20, @D21=D21, @D22=D22, @D23=D23, @D24=D24, @D25=D25, @D26=D26, @D27=D27, @D28=D28, @D29=D29, @D30=D30
		FROM @T_Prueba WHERE Nombre = '<NOMBRE>'
	END

	IF (@CantDias = 31) BEGIN
		SELECT @D1=D1, @D2=D2, @D3=D3, @D4=D4, @D5=D5, @D6=D6, @D7=D7, @D8=D8, @D9=D9, @D10=D10, @D11=D11, @D12=D12, @D13=D13, @D14=D14, @D15=D15, @D16=D16, @D17=D17,
		@D18=D18, @D19=D19, @D20=D20, @D21=D21, @D22=D22, @D23=D23, @D24=D24, @D25=D25, @D26=D26, @D27=D27, @D28=D28, @D29=D29, @D30=D30, @D29=D29, @D30=D30, @D31=D31
		FROM @T_Prueba WHERE Nombre = '<NOMBRE>'
	END

	CREATE TABLE #PruebaView (IDPersona INT, Nombre VARCHAR(250), Puesto VARCHAR(100),
							  D1 VARCHAR(15),D2 VARCHAR(15),D3 VARCHAR(15),D4 VARCHAR(15),D5 VARCHAR(15),
							  D6 VARCHAR(15),D7 VARCHAR(15),D8 VARCHAR(15),D9 VARCHAR(15),D10 VARCHAR(15),
							  D11 VARCHAR(15),D12 VARCHAR(15),D13 VARCHAR(15),D14 VARCHAR(15),D15 VARCHAR(15),
							  D16 VARCHAR(15),D17 VARCHAR(15),D18 VARCHAR(15),D19 VARCHAR(15),D20 VARCHAR(15),
							  D21 VARCHAR(15),D22 VARCHAR(15),D23 VARCHAR(15),D24 VARCHAR(15),D25 VARCHAR(15),
							  D26 VARCHAR(15),D27 VARCHAR(15),D28 VARCHAR(15),D29 VARCHAR(15),D30 VARCHAR(15),
							  D31 VARCHAR(15))

	DECLARE @SQL varchar(5000)

	INSERT INTO #PruebaView
	SELECT * FROM @T_Prueba WHERE Nombre <> '<NOMBRE>'

	IF @CantDias = 28 BEGIN
		SET @SQL='SELECT IDPersona, Nombre AS NOMBRE, Puesto AS PUESTO,
			D1 AS '+@D1+',D2 AS '+@D2+',D3 AS '+@D3+',D4 AS '+@D4+',D5 AS '+@D5+',D6 AS '+@D6+',D7 AS '+@D7+',D8 AS '+@D8+
		    ',D9 AS '+@D9+',D10 AS '+@D10+',D11 AS '+@D11+',D12 AS '+@D12+',D13 AS '+@D13+',D14 AS '+@D14+',D15 AS '+@D15+
			',D16 AS '+@D16+',D17 AS '+@D17+',D18 AS '+@D18+',D19 AS '+@D19+',D20 AS '+@D20+',D21 AS '+@D21+',D22 AS '+@D22+
			',D23 AS '+@D23+',D24 AS '+@D24+',D25 AS '+@D25+',D26 AS '+@D26+',D27 AS '+@D27+',D28 AS '+@D28+' FROM #PruebaView ORDER BY Nombre'
		
		EXEC (@SQL)
		DROP TABLE #PruebaView
	END

	IF @CantDias = 29 BEGIN
		SET @SQL='SELECT IDPersona, Nombre AS NOMBRE, Puesto AS PUESTO,
			D1 AS '+@D1+',D2 AS '+@D2+',D3 AS '+@D3+',D4 AS '+@D4+',D5 AS '+@D5+',D6 AS '+@D6+',D7 AS '+@D7+',D8 AS '+@D8+
			',D9 AS '+@D9+',D10 AS '+@D10+',D11 AS '+@D11+',D12 AS '+@D12+',D13 AS '+@D13+',D14 AS '+@D14+',D15 AS '+@D15+
			',D16 AS '+@D16+',D17 AS '+@D17+',D18 AS '+@D18+',D19 AS '+@D19+',D20 AS '+@D20+',D21 AS '+@D21+',D22 AS '+@D22+
			',D23 AS '+@D23+',D24 AS '+@D24+',D25 AS '+@D25+',D26 AS '+@D26+',D27 AS '+@D27+',D28 AS '+@D28+',D29 AS '+@D29+' FROM #PruebaView ORDER BY Nombre'
		
		EXEC (@SQL)
		DROP TABLE #PruebaView	
	END

	IF @CantDias = 30 BEGIN
		SET @SQL='SELECT IDPersona, Nombre AS NOMBRE, Puesto AS PUESTO,
			D1 AS '+@D1+',D2 AS '+@D2+',D3 AS '+@D3+',D4 AS '+@D4+',D5 AS '+@D5+',D6 AS '+@D6+',D7 AS '+@D7+',D8 AS '+@D8+
			',D9 AS '+@D9+',D10 AS '+@D10+',D11 AS '+@D11+',D12 AS '+@D12+',D13 AS '+@D13+',D14 AS '+@D14+',D15 AS '+@D15+
			',D16 AS '+@D16+',D17 AS '+@D17+',D18 AS '+@D18+',D19 AS '+@D19+',D20 AS '+@D20+',D21 AS '+@D21+',D22 AS '+@D22+
			',D23 AS '+@D23+',D24 AS '+@D24+',D25 AS '+@D25+',D26 AS '+@D26+',D27 AS '+@D27+',D28 AS '+@D28+',D29 AS '+@D29+
			',D30 AS '+@D30+' FROM #PruebaView ORDER BY Nombre'
		
		EXEC (@SQL)
		DROP TABLE #PruebaView
	END

	IF @CantDias = 31 BEGIN
		SET @SQL='SELECT IDPersona, Nombre AS NOMBRE, Puesto AS PUESTO,
			D1 AS '+@D1+',D2 AS '+@D2+',D3 AS '+@D3+',D4 AS '+@D4+',D5 AS '+@D5+',D6 AS '+@D6+',D7 AS '+@D7+',D8 AS '+@D8+
			',D9 AS '+@D9+',D10 AS '+@D10+',D11 AS '+@D11+',D12 AS '+@D12+',D13 AS '+@D13+',D14 AS '+@D14+',D15 AS '+@D15+
			',D16 AS '+@D16+',D17 AS '+@D17+',D18 AS '+@D18+',D19 AS '+@D19+',D20 AS '+@D20+',D21 AS '+@D21+',D22 AS '+@D22+
			',D23 AS '+@D23+',D24 AS '+@D24+',D25 AS '+@D25+',D26 AS '+@D26+',D27 AS '+@D27+',D28 AS '+@D28+',D29 AS '+@D29+
			',D30 AS '+@D30+',D31 AS '+@D31+' FROM #PruebaView ORDER BY Nombre'
		
		EXEC (@SQL)
		DROP TABLE #PruebaView
	END
END

-----------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 11/10/2024
-- Description:	INSERTAR ESTADO DE ASISTENCIA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_RoosterProyectado_IngresarEstadoAsistencia]
@Codigo VARCHAR(25),
@Estado VARCHAR(300)
AS
DECLARE @exito VARCHAR(MAX)	
DECLARE @correlativo INT

SET @correlativo = (SELECT MAX(idEstado) FROM ReportesApp_Mantenimiento_RoosterProyectado_Estados)
SET @correlativo = ISNULL(@correlativo,0) + 1 

BEGIN TRAN
BEGIN TRY
	IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_RoosterProyectado_Estados WHERE Codigo = @Codigo)) BEGIN
		SET @exito = '-1 = Ya se registró un estado con este código.'
		ROLLBACK
		GOTO Terminar
	END

	INSERT INTO ReportesApp_Mantenimiento_RoosterProyectado_Estados(idEstado,Codigo,Estado)
	VALUES(@correlativo, @Codigo, @Estado)

	SET @exito = '0 = Estado registrado.'
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

-----------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 11/10/2024
-- Description:	REGISTRAR ESTADO DE ASISTENCIA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_RoosterProyectado_RegistrarAsistencia]
@Periodo VARCHAR(6),
@xmlAsistencia VARCHAR(MAX),
@Codigo VARCHAR(20),
@Usuario VARCHAR(20)
AS
DECLARE @exito VARCHAR(MAX)
DECLARE @correlativo INT, @co2 INT
DECLARE @TEMP_ASIS TABLE (Contador INT, IDPersona INT, Fecha DATE, Codigo VARCHAR(20))
DECLARE @idact INT, @Nro INT

SET @exito = '0 = Asistencia de Personal Registrada.'

SET @correlativo = (SELECT MAX(Contador) FROM @TEMP_ASIS)
SET @correlativo = ISNULL(@correlativo,0) + 1 
SET @Nro = 1

BEGIN TRAN
BEGIN TRY
	IF @xmlAsistencia IS NOT NULL BEGIN  
		EXEC sp_xml_preparedocument @idact OUT, @xmlAsistencia  

		INSERT INTO @TEMP_ASIS(Contador, IDPersona, Fecha, Codigo)  
		SELECT ROW_NUMBER() OVER(ORDER BY @correlativo ASC), IDPersona, Fecha, @Codigo
		FROM OPENXML(@idact,'/r/d',3) WITH(IDPersona INT, Fecha DATE)  
		     
		EXEC sp_xml_removedocument @idact  
	END

	WHILE (@Nro <= (SELECT MAX(Contador) FROM @TEMP_ASIS)) BEGIN
		SET @co2 = (SELECT MAX(idRooster) FROM ReportesApp_Mantenimiento_RoosterProyectado_Registro)
		SET @co2 = ISNULL(@co2,0) + 1 

		DELETE FROM ReportesApp_Mantenimiento_RoosterProyectado_Registro
		WHERE IDPersona = (SELECT IDPersona FROM @TEMP_ASIS WHERE Contador = @Nro) AND Fecha = (SELECT Fecha FROM @TEMP_ASIS WHERE Contador = @Nro)

		INSERT INTO ReportesApp_Mantenimiento_RoosterProyectado_Registro(idRooster,IDPersona,Fecha,Codigo,UsuarioRegistra,FechaRegistra)
		SELECT @co2, P.IDPersona, P.Fecha, P.Codigo, @Usuario, GETDATE()
		FROM @TEMP_ASIS P WHERE P.Contador = @Nro

		DECLARE @x_FechaSelec DATE = (SELECT Fecha FROM @TEMP_ASIS WHERE Contador = @Nro)

		UPDATE P 
		SET P.D1 = CASE WHEN DAY(O.Fecha) = 1 THEN O.Codigo ELSE P.D1 END,
			P.D2 = CASE WHEN DAY(O.Fecha) = 2 THEN O.Codigo ELSE P.D2 END,
			P.D3 = CASE WHEN DAY(O.Fecha) = 3 THEN O.Codigo ELSE P.D3 END,
			P.D4 = CASE WHEN DAY(O.Fecha) = 4 THEN O.Codigo ELSE P.D4 END,
			P.D5 = CASE WHEN DAY(O.Fecha) = 5 THEN O.Codigo ELSE P.D5 END,
			P.D6 = CASE WHEN DAY(O.Fecha) = 6 THEN O.Codigo ELSE P.D6 END,
			P.D7 = CASE WHEN DAY(O.Fecha) = 7 THEN O.Codigo ELSE P.D7 END,
			P.D8 = CASE WHEN DAY(O.Fecha) = 8 THEN O.Codigo ELSE P.D8 END,
			P.D9 = CASE WHEN DAY(O.Fecha) = 9 THEN O.Codigo ELSE P.D9 END,
			P.D10 = CASE WHEN DAY(O.Fecha) = 10 THEN O.Codigo ELSE P.D10 END,
			P.D11 = CASE WHEN DAY(O.Fecha) = 11 THEN O.Codigo ELSE P.D11 END,
			P.D12 = CASE WHEN DAY(O.Fecha) = 12 THEN O.Codigo ELSE P.D12 END,
			P.D13 = CASE WHEN DAY(O.Fecha) = 13 THEN O.Codigo ELSE P.D13 END,
			P.D14 = CASE WHEN DAY(O.Fecha) = 14 THEN O.Codigo ELSE P.D14 END,
			P.D15 = CASE WHEN DAY(O.Fecha) = 15 THEN O.Codigo ELSE P.D15 END,
			P.D16 = CASE WHEN DAY(O.Fecha) = 16 THEN O.Codigo ELSE P.D16 END,
			P.D17 = CASE WHEN DAY(O.Fecha) = 17 THEN O.Codigo ELSE P.D17 END,
			P.D18 = CASE WHEN DAY(O.Fecha) = 18 THEN O.Codigo ELSE P.D18 END,
			P.D19 = CASE WHEN DAY(O.Fecha) = 19 THEN O.Codigo ELSE P.D19 END,
			P.D20 = CASE WHEN DAY(O.Fecha) = 20 THEN O.Codigo ELSE P.D20 END,
			P.D21 = CASE WHEN DAY(O.Fecha) = 21 THEN O.Codigo ELSE P.D21 END,
			P.D22 = CASE WHEN DAY(O.Fecha) = 22 THEN O.Codigo ELSE P.D22 END,
			P.D23 = CASE WHEN DAY(O.Fecha) = 23 THEN O.Codigo ELSE P.D23 END,
			P.D24 = CASE WHEN DAY(O.Fecha) = 24 THEN O.Codigo ELSE P.D24 END,
			P.D25 = CASE WHEN DAY(O.Fecha) = 25 THEN O.Codigo ELSE P.D25 END,
			P.D26 = CASE WHEN DAY(O.Fecha) = 26 THEN O.Codigo ELSE P.D26 END,
			P.D27 = CASE WHEN DAY(O.Fecha) = 27 THEN O.Codigo ELSE P.D27 END,
			P.D28 = CASE WHEN DAY(O.Fecha) = 28 THEN O.Codigo ELSE P.D28 END,
			P.D29 = CASE WHEN DAY(O.Fecha) = 29 THEN O.Codigo ELSE P.D29 END,
			P.D30 = CASE WHEN DAY(O.Fecha) = 30 THEN O.Codigo ELSE P.D30 END,
			P.D31 = CASE WHEN DAY(O.Fecha) = 31 THEN O.Codigo ELSE P.D31 END
		FROM ReportesApp_Mantenimiento_RoosterProyectado_PersonalView P
		LEFT JOIN @TEMP_ASIS O ON P.IDPersona = O.IDPersona AND O.Fecha = @x_FechaSelec
		WHERE P.Anio = RIGHT(@Periodo,4) AND P.Mes = LEFT(@periodo,2)

		SET @Nro = @Nro + 1
	END
END TRY

BEGIN CATCH
	SET @exito = RTRIM(LTRIM(STR(ERROR_NUMBER()))) + '=' + ERROR_MESSAGE()
	ROLLBACK
	GOTO Terminar
END CATCH

IF @@TRANCOUNT > 0
	--ROLLBACK
	COMMIT;

TERMINAR:
IF LEFT(@Exito,1)='-' OR cast(left(@Exito,1) as int)>0 
BEGIN
	DECLARE @storedProcedureName VARCHAR(100)
	SELECT @storedProcedureName = OBJECT_NAME(@@PROCID)
	SET @exito = @exito + CHAR(13)+CHAR(13)+'SP Name: '+@storedProcedureName
END

SELECT @exito exito

----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 11/10/2024
-- Description:	ELIMINAR ESTADO DE ASISTENCIA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_RoosterProyectado_EliminarAsistencia]
@Periodo VARCHAR(6),
@xmlAsistencia VARCHAR(MAX),
@Usuario VARCHAR(20)
AS
DECLARE @exito VARCHAR(MAX)
DECLARE @correlativo INT, @co2 INT
DECLARE @TEMP_ASIS TABLE (Contador INT, IDPersona INT, Fecha DATE)
DECLARE @idact INT, @Nro INT

SET @exito = '0 = Asistencia de Personal Eliminada.'

SET @correlativo = (SELECT MAX(Contador) FROM @TEMP_ASIS)
SET @correlativo = ISNULL(@correlativo,0) + 1 
SET @Nro = 1

BEGIN TRAN
BEGIN TRY
	IF @xmlAsistencia IS NOT NULL BEGIN  
		EXEC sp_xml_preparedocument @idact OUT, @xmlAsistencia  

		INSERT INTO @TEMP_ASIS(Contador, IDPersona, Fecha)  
		SELECT ROW_NUMBER() OVER(ORDER BY @correlativo ASC), IDPersona, Fecha
		FROM OPENXML(@idact,'/r/d',3) WITH(IDPersona INT, Fecha DATE)  
		     
		EXEC sp_xml_removedocument @idact  
	END

	WHILE (@Nro <= (SELECT MAX(Contador) FROM @TEMP_ASIS)) BEGIN
		SET @co2 = (SELECT MAX(idRooster) FROM ReportesApp_Mantenimiento_RoosterProyectado_Registro)
		SET @co2 = ISNULL(@co2,0) + 1 

		DELETE FROM ReportesApp_Mantenimiento_RoosterProyectado_Registro
		WHERE IDPersona = (SELECT IDPersona FROM @TEMP_ASIS WHERE Contador = @Nro) AND Fecha = (SELECT Fecha FROM @TEMP_ASIS WHERE Contador = @Nro)

		DECLARE @x_FechaSelec DATE = (SELECT Fecha FROM @TEMP_ASIS WHERE Contador = @Nro)

		UPDATE P 
		SET P.D1 = CASE WHEN DAY(O.Fecha) = 1 THEN NULL ELSE P.D1 END,
			P.D2 = CASE WHEN DAY(O.Fecha) = 2 THEN NULL ELSE P.D2 END,
			P.D3 = CASE WHEN DAY(O.Fecha) = 3 THEN NULL ELSE P.D3 END,
			P.D4 = CASE WHEN DAY(O.Fecha) = 4 THEN NULL ELSE P.D4 END,
			P.D5 = CASE WHEN DAY(O.Fecha) = 5 THEN NULL ELSE P.D5 END,
			P.D6 = CASE WHEN DAY(O.Fecha) = 6 THEN NULL ELSE P.D6 END,
			P.D7 = CASE WHEN DAY(O.Fecha) = 7 THEN NULL ELSE P.D7 END,
			P.D8 = CASE WHEN DAY(O.Fecha) = 8 THEN NULL ELSE P.D8 END,
			P.D9 = CASE WHEN DAY(O.Fecha) = 9 THEN NULL ELSE P.D9 END,
			P.D10 = CASE WHEN DAY(O.Fecha) = 10 THEN NULL ELSE P.D10 END,
			P.D11 = CASE WHEN DAY(O.Fecha) = 11 THEN NULL ELSE P.D11 END,
			P.D12 = CASE WHEN DAY(O.Fecha) = 12 THEN NULL ELSE P.D12 END,
			P.D13 = CASE WHEN DAY(O.Fecha) = 13 THEN NULL ELSE P.D13 END,
			P.D14 = CASE WHEN DAY(O.Fecha) = 14 THEN NULL ELSE P.D14 END,
			P.D15 = CASE WHEN DAY(O.Fecha) = 15 THEN NULL ELSE P.D15 END,
			P.D16 = CASE WHEN DAY(O.Fecha) = 16 THEN NULL ELSE P.D16 END,
			P.D17 = CASE WHEN DAY(O.Fecha) = 17 THEN NULL ELSE P.D17 END,
			P.D18 = CASE WHEN DAY(O.Fecha) = 18 THEN NULL ELSE P.D18 END,
			P.D19 = CASE WHEN DAY(O.Fecha) = 19 THEN NULL ELSE P.D19 END,
			P.D20 = CASE WHEN DAY(O.Fecha) = 20 THEN NULL ELSE P.D20 END,
			P.D21 = CASE WHEN DAY(O.Fecha) = 21 THEN NULL ELSE P.D21 END,
			P.D22 = CASE WHEN DAY(O.Fecha) = 22 THEN NULL ELSE P.D22 END,
			P.D23 = CASE WHEN DAY(O.Fecha) = 23 THEN NULL ELSE P.D23 END,
			P.D24 = CASE WHEN DAY(O.Fecha) = 24 THEN NULL ELSE P.D24 END,
			P.D25 = CASE WHEN DAY(O.Fecha) = 25 THEN NULL ELSE P.D25 END,
			P.D26 = CASE WHEN DAY(O.Fecha) = 26 THEN NULL ELSE P.D26 END,
			P.D27 = CASE WHEN DAY(O.Fecha) = 27 THEN NULL ELSE P.D27 END,
			P.D28 = CASE WHEN DAY(O.Fecha) = 28 THEN NULL ELSE P.D28 END,
			P.D29 = CASE WHEN DAY(O.Fecha) = 29 THEN NULL ELSE P.D29 END,
			P.D30 = CASE WHEN DAY(O.Fecha) = 30 THEN NULL ELSE P.D30 END,
			P.D31 = CASE WHEN DAY(O.Fecha) = 31 THEN NULL ELSE P.D31 END
		FROM ReportesApp_Mantenimiento_RoosterProyectado_PersonalView P
		LEFT JOIN @TEMP_ASIS O ON P.IDPersona = O.IDPersona AND O.Fecha = @x_FechaSelec
		WHERE P.Anio = RIGHT(@Periodo,4) AND P.Mes = LEFT(@periodo,2)

		SET @Nro = @Nro + 1
	END
END TRY

BEGIN CATCH
	SET @exito = RTRIM(LTRIM(STR(ERROR_NUMBER()))) + '=' + ERROR_MESSAGE()
	ROLLBACK
	GOTO Terminar
END CATCH

IF @@TRANCOUNT > 0
	--ROLLBACK
	COMMIT;

TERMINAR:
IF LEFT(@Exito,1)='-' OR cast(left(@Exito,1) as int)>0 
BEGIN
	DECLARE @storedProcedureName VARCHAR(100)
	SELECT @storedProcedureName = OBJECT_NAME(@@PROCID)
	SET @exito = @exito + CHAR(13)+CHAR(13)+'SP Name: '+@storedProcedureName
END

SELECT @exito exito






