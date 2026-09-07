
SELECT * FROM ReportesApp_RRHH_Asistencia_Tipo
32	A	Asistencia
55	CO	Compensacion
56  CN  Compensa Noche
57  CA  Comp. Adelantada
41	V	Vacaciones
35	SB	Subsidio
58	LT	Permiso LUTO
50	DM	Descanso Medico

------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 14-08-2025
-- Description:	LISTAR INDICADOR DE ASISTENCIA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_RRHH_AsistenciasView_ListarIndicadores]
@Fecha DATE
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @Dia INT = DAY(@Fecha)
    DECLARE @Columna SYSNAME = 'D' + CAST(@Dia AS VARCHAR(2))
	DECLARE @SQL NVARCHAR(MAX)

	CREATE TABLE #T_Asistencias (IDPersona INT, Fecha DATE, Operacion VARCHAR(30), D1 VARCHAR(10), D2 VARCHAR(10), D3 VARCHAR(10), D4 VARCHAR(10), D5 VARCHAR(10),
	D6 VARCHAR(10), D7 VARCHAR(10), D8 VARCHAR(10), D9 VARCHAR(10), D10 VARCHAR(10), D11 VARCHAR(10), D12 VARCHAR(10), D13 VARCHAR(10), D14 VARCHAR(10),
	D15 VARCHAR(10), D16 VARCHAR(10), D17 VARCHAR(10), D18 VARCHAR(10), D19 VARCHAR(10), D20 VARCHAR(10), D21 VARCHAR(10), D22 VARCHAR(10), D23 VARCHAR(10),
	D24 VARCHAR(10), D25 VARCHAR(10), D26 VARCHAR(10), D27 VARCHAR(10), D28 VARCHAR(10), D29 VARCHAR(10), D30 VARCHAR(10), D31 VARCHAR(10))

	INSERT INTO #T_Asistencias
	SELECT X.IDPersona, @Fecha, O.Descripcion, X.D1, X.D2, X.D3, X.D4, X.D5, X.D6, X.D7, X.D8, X.D9, X.D10, X.D11, X.D12, X.D13, X.D14, X.D15,
	X.D16, X.D17, X.D18, X.D19, X.D20, X.D21, X.D22, X.D23, X.D24, X.D25, X.D26, X.D27, X.D28, X.D29, X.D30, X.D31
	FROM (SELECT A.* FROM ReportesApp_RRHH_AsistenciaView A
	WHERE A.CodPlanilla = 'CD' AND A.Anio = YEAR(@Fecha) AND A.Mes = MONTH(@Fecha)) X
	LEFT JOIN OP_TR_Conductor C ON C.IdPersona = X.IDPersona
	LEFT JOIN EmpleadoMast E ON E.Empleado = C.IdPersona
	LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = C.CodigoEnapu
	WHERE C.CodigoEnapu IN (1,2,3,10) AND E.Estado = 'A'

	SET @SQL = '
        WITH Conteo AS (
            SELECT Fecha AS FECHA, Operacion AS OPERACION,
            SUM(CASE WHEN ' + QUOTENAME(@Columna) + ' = ''A'' THEN 1 ELSE 0 END) AS ASISTIO,
            SUM(CASE WHEN ' + QUOTENAME(@Columna) + ' = ''CO'' THEN 1 ELSE 0 END) AS COMPENSACION,
			SUM(CASE WHEN ' + QUOTENAME(@Columna) + ' = ''CA'' THEN 1 ELSE 0 END) AS COMP_ADELANTADA,
			SUM(CASE WHEN ' + QUOTENAME(@Columna) + ' = ''CN'' THEN 1 ELSE 0 END) AS COMP_NOCHE,
            SUM(CASE WHEN ' + QUOTENAME(@Columna) + ' = ''V'' THEN 1 ELSE 0 END) AS VACACIONES,
			SUM(CASE WHEN ' + QUOTENAME(@Columna) + ' = ''SB'' THEN 1 ELSE 0 END) AS SUBSIDIO,
            SUM(CASE WHEN ' + QUOTENAME(@Columna) + ' = ''LT'' THEN 1 ELSE 0 END) AS LICENCIA_LUTO,
            SUM(CASE WHEN ' + QUOTENAME(@Columna) + ' = ''DM'' THEN 1 ELSE 0 END) AS DESCANSO_MEDICO,
			SUM(CASE WHEN ' + QUOTENAME(@Columna) + ' IS NULL OR ' + QUOTENAME(@Columna) + ' = ''DF'' THEN 1 ELSE 0 END) AS NO_LISTADO
            FROM #T_Asistencias
            GROUP BY Fecha, Operacion
        ),

		Datos AS (
			SELECT 0 AS Orden, FECHA, OPERACION, ASISTIO, COMPENSACION, COMP_ADELANTADA, COMP_NOCHE, VACACIONES, SUBSIDIO, LICENCIA_LUTO, DESCANSO_MEDICO, NO_LISTADO,
			(ASISTIO + COMPENSACION + COMP_ADELANTADA + COMP_NOCHE + VACACIONES + SUBSIDIO + LICENCIA_LUTO + DESCANSO_MEDICO + NO_LISTADO) AS TOTAL
			FROM Conteo
			WHERE OPERACION = ''LINDLEY''
			UNION ALL
			SELECT 1 AS Orden, FECHA, OPERACION, ASISTIO, COMPENSACION, COMP_ADELANTADA, COMP_NOCHE, VACACIONES, SUBSIDIO, LICENCIA_LUTO, DESCANSO_MEDICO, NO_LISTADO,
			(ASISTIO + COMPENSACION + COMP_ADELANTADA + COMP_NOCHE + VACACIONES + SUBSIDIO + LICENCIA_LUTO + DESCANSO_MEDICO + NO_LISTADO) AS TOTAL
			FROM Conteo
			WHERE OPERACION != ''LINDLEY''
			UNION ALL
			SELECT 2 AS Orden, MAX(FECHA) AS FECHA, ''TOTAL'' AS OPERACION, SUM(ASISTIO), SUM(COMPENSACION), SUM(COMP_ADELANTADA),
			SUM(COMP_NOCHE), SUM(VACACIONES), SUM(SUBSIDIO), SUM(LICENCIA_LUTO), SUM(DESCANSO_MEDICO), SUM(NO_LISTADO),
			SUM(ASISTIO + COMPENSACION + COMP_ADELANTADA + COMP_NOCHE + VACACIONES + SUBSIDIO + LICENCIA_LUTO + DESCANSO_MEDICO + NO_LISTADO) AS TOTAL
			FROM Conteo
		)

        SELECT CONVERT(VARCHAR,FECHA,103) AS FECHA, OPERACION, ASISTIO, COMPENSACION, COMP_ADELANTADA, COMP_NOCHE, VACACIONES, SUBSIDIO,
		LICENCIA_LUTO, DESCANSO_MEDICO, NO_LISTADO, TOTAL
		FROM Datos
		ORDER BY Orden, OPERACION'

	EXEC sp_executesql @SQL
END

-----------------------------------------------------------------------

-- CREAR TABLA ReportesApp_RRHH_Asistencias_CompensacionesVolcan

-----------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 19-08-2025
-- Description:	CONTAR COMPENSACIONES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_RRHH_Asistencias_ContarCompensaciones]
@idPersona INT
AS
BEGIN
	DECLARE @CD_VOLCAN TABLE (Numero INT, Persona INT, CompPendientes INT)
	DECLARE @TotalAsistencia INT, @CantidadComp INT, @TotalCompensaciones INT, @CompPendientes INT 
	DECLARE @Contador INT = 1

	INSERT INTO @CD_VOLCAN (Numero, Persona)
	SELECT ROW_NUMBER() OVER(ORDER BY IdPersona ASC) AS 'NRO', IdPersona
	FROM OP_TR_Conductor
	WHERE CodigoEnapu = 10 AND Estado = 'A'

	WHILE (@Contador <= (SELECT COUNT(Numero) FROM @CD_VOLCAN)) BEGIN
		DECLARE @Persona INT = (SELECT Persona FROM @CD_VOLCAN WHERE Numero = @Contador)
		DECLARE @FechaIngreso DATE = (SELECT CONVERT(DATE,FechaIngreso) FROM EmpleadoMast WHERE Empleado = @Persona AND Estado = 'A')
			
		SET @TotalAsistencia = (SELECT COUNT(*) FROM ReportesApp_RRHH_Asistencia WHERE IDTipoAsist = 32 AND IDPersona = @Persona
								AND AsisExtendida IS NULL AND Fecha <= CONVERT(DATE,GETDATE()) AND Fecha > @FechaIngreso)
		SET @CantidadComp = (SELECT COUNT(*) FROM ReportesApp_RRHH_Asistencia WHERE IDTipoAsist IN (55,57) AND IDPersona = @Persona
							 AND Fecha > @FechaIngreso)

		SET @TotalCompensaciones = @TotalAsistencia / 3
		SET @CompPendientes = @TotalCompensaciones - @CantidadComp

		IF (EXISTS(SELECT * FROM ReportesApp_RRHH_Asistencias_CompensacionesVolcan WHERE idPersona = @Persona)) BEGIN
			UPDATE ReportesApp_RRHH_Asistencias_CompensacionesVolcan
			SET CompPendientes = @CompPendientes
			WHERE idPersona = @Persona
		END
		ELSE BEGIN
			INSERT INTO ReportesApp_RRHH_Asistencias_CompensacionesVolcan
			SELECT @Persona, @CompPendientes
		END

		SET @Contador = @Contador + 1
	END
	
	SELECT ISNULL(CompPendientes,0) AS 'COMP_PENDIENTES'
	FROM ReportesApp_RRHH_Asistencias_CompensacionesVolcan
	WHERE idPersona = @idPersona
END

-----------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 19-08-2025
-- Description:	REGISTRAR COMPENSACION VOLCAN
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_RRHH_Asistencias_Compensar_Volcan]
@IDPersona INT,
@FechaCompensa DATE,
@Usuario VARCHAR(20)
AS
DECLARE @exito VARCHAR(MAX)

SET @exito = '0 = Compensación registrada exitosamente.'

DECLARE @Asistencias TABLE (IdPersona INT, Fecha DATE, IdTipoAsist INT, Letra VARCHAR(2))
DECLARE @Fechas TABLE (PK INT IDENTITY(1,1) PRIMARY KEY, Fecha DATE)

DECLARE @LetraAsistencia VARCHAR(2) = 'CO'

INSERT INTO @Asistencias (IdPersona, Fecha, IdTipoAsist, Letra)
VALUES (@IDPersona, @FechaCompensa, 55, 'CO')

INSERT INTO @Fechas (Fecha)
SELECT DISTINCT Fecha FROM @Asistencias

--VERIFICAMOS QUE EN LA FECHA NO HAYA, LICENCIAS DESDE RRHH:
DECLARE @Tmp_Personas_Con_Licencias TABLE (IDPersona INT, Nombre VARCHAR(100), TipoLicencia CHAR(2), DescripcionLicencia VARCHAR(50), FInicio DATETIME,
										   FFinal DATETIME, dias INT)

INSERT INTO @Tmp_Personas_Con_Licencias(IDPersona,Nombre,TipoLicencia,DescripcionLicencia,FInicio,FFinal,dias)
SELECT HR_LICENCIAS.EMPLEADO, NOMBRECOMPLETO, TIPOLICENCIA, DescripcionLocal, FECHAINICIO, FECHAFINAL, DATEDIFF(DAY,FECHAINICIO,FECHAfinal) + 1 as dias
FROM HR_LICENCIAS, PERSONAMAST, AS_CARNETIDENTIFICACION, MA_MiscelaneosDetalle, EMPLEADOMAST
LEFT JOIN AC_COSTCENTERMST ON EMPLEADOMAST.CENTROCOSTOS = AC_COSTCENTERMST.COSTCENTER
WHERE HR_LICENCIAS.EMPLEADO = PERSONAMAST.PERSONA AND EMPLEADOMAST.EMPLEADO = PERSONAMAST.PERSONA AND EMPLEADOMAST.ESTADO = 'A'
AND AS_CARNETIDENTIFICACION.Empleado = Empleadomast.empleado AND MA_MiscelaneosDetalle.CodigoElemento = HR_LICENCIAS.TipoLicencia AND MA_MiscelaneosDetalle.AplicacionCodigo = 'HR'
AND MA_MiscelaneosDetalle.CodigoTabla = 'LICENCIA' AND MA_MiscelaneosDetalle.Compania = '999999' AND
(EmpleadoMast.CompaniaSocio IN ('10000000')) AND (EmpleadoMast.TipoPlanilla IN ('EM','OB','PR')) AND (EmpleadoMast.LocaciondePago = 'S' OR 'S' = 'S') 
AND (YEAR(HR_LICENCIAS.FechaInicio) = YEAR(@FechaCompensa) AND MONTH(HR_LICENCIAS.FechaInicio) = MONTH(@FechaCompensa))

IF EXISTS(SELECT TOP 1 (1) FROM @Asistencias A INNER JOIN @Tmp_Personas_Con_Licencias L ON L.IDPersona = A.IdPersona
WHERE A.Fecha BETWEEN L.FInicio AND L.FFinal) BEGIN
	DECLARE @x_FechaModifLic VARCHAR(30), @x_ConductorLic VARCHAR(100), @x_MotivoLic VARCHAR(50), @x_FechasLic VARCHAR(50), @x_diasLic CHAR(2)
		
	SELECT TOP 1 @x_ConductorLic = L.Nombre, @x_FechaModifLic = CAST(A.Fecha AS VARCHAR(11)), @x_MotivoLic = L.DescripcionLicencia,
	@x_FechasLic = CAST(L.FInicio AS VARCHAR(11))+ ' AL '+CAST(L.FFinal AS VARCHAR(11)), @x_diasLic = CAST(L.dias AS VARCHAR)
	FROM @Asistencias A 
	INNER JOIN @Tmp_Personas_Con_Licencias L ON L.IDPersona=A.IdPersona
	WHERE A.Fecha BETWEEN L.FInicio AND L.FFinal
		
	SET @exito = '-2 = No puede Modificar la Asistencia: '+ @x_FechaModifLic +CHAR(13)
				 +'Conductor: '+@x_ConductorLic+CHAR(13)+
				 +'Motivo: '+@x_MotivoLic+CHAR(13)+
				 +'Fecha: '+@x_FechasLic+CHAR(13)+
				 +'Cant.: '+@x_diasLic+' Dias.'

	GOTO Terminar
END

-- CARGA DE VACACIONES:
DECLARE @Tmp_Vacaciones TABLE (PK INT IDENTITY(1,1), IDPersona INT, Trabajador VARCHAR(100), FInicio DATETIME, FFinal DATETIME, dias INT,
CompaniaSocio VARCHAR(8), ConceptoAcceso VARCHAR(5), IdTipoAsist INT, CodTipo CHAR(1))

INSERT INTO @Tmp_Vacaciones(IDPersona,Trabajador,FInicio,FFinal,dias,CompaniaSocio,ConceptoAcceso,IdTipoAsist,CodTipo)
SELECT Empleado IDPersona, P.NombreCompleto, V.FechaInicio, V.FechaFin, V.DiasUtilizacion, CompaniaSocio, 'VACA', 41, 'V'  
FROM PR_VacacionUtilizacion V
LEFT JOIN PersonaMast P ON P.Persona = V.Empleado 
WHERE (YEAR(V.FechaInicio) = YEAR(@FechaCompensa) AND MONTH(V.FechaInicio) = MONTH(@FechaCompensa)) AND TipoUtilizacion = 'GOC'

IF EXISTS(SELECT TOP 1(1) FROM @Asistencias A INNER JOIN @Tmp_Vacaciones V ON V.IDPersona = A.IdPersona
WHERE A.Fecha BETWEEN V.FInicio AND V.FFinal) BEGIN
	SELECT TOP 1 @x_ConductorLic = L.Trabajador, @x_FechaModifLic = CAST(A.Fecha AS VARCHAR(11)), @x_MotivoLic = 'Vacaciones',
	@x_FechasLic = CAST(L.FInicio AS VARCHAR(11))+' AL '+CAST(L.FFinal AS VARCHAR(11)), @x_diasLic = CAST(L.dias AS VARCHAR)
	FROM @Asistencias A 
	INNER JOIN @Tmp_Vacaciones L ON L.IDPersona = A.IdPersona
	WHERE A.Fecha BETWEEN L.FInicio AND L.FFinal
		
	SET @exito = '-3 = Existe conflicto con conductor '+CAST(@x_ConductorLic AS VARCHAR)+CHAR(13)+
				 @x_FechaModifLic+', Vacac. de RRHH.'

	GOTO Terminar
END

IF EXISTS(SELECT TOP 1 AD.FECHA FROM ReportesApp_RRHH_Asistencia_CompensacionesAdelantadas AD
INNER JOIN @Asistencias A ON A.IdPersona = AD.IDPERSONA AND A.Fecha = AD.FECHA) BEGIN
	SET @exito = '-4 = Se ha encontrado fechas vinculadas a Compensación Adelantadas. '+CHAR(13)
				 +' No pueden ser Modificadas. Las debe liberar antes con Control Gestión.'

	GOTO Terminar
END

DECLARE @x_FIniciaContrato DATE = (SELECT FechaIngreso FROM EmpleadoMast WHERE Empleado = @IDPersona)

IF (@FechaCompensa <= @x_FIniciaContrato) BEGIN
	SET @exito = '-5 = No puede programar una compensación antes de la fecha de ingreso del trabajador.'

	GOTO Terminar
END

DECLARE @NroComp INT = (SELECT CompPendientes FROM ReportesApp_RRHH_Asistencias_CompensacionesVolcan WHERE idPersona = @IDPersona)

IF (ISNULL(@NroComp,0) <= 0) BEGIN
	SET @exito = '-7 = No puede registrar la compensacion por tener aún '+CAST(@NroComp AS VARCHAR)+' días pendientes de asistencia.'
	Goto Terminar
END

DECLARE @I INT = 1
DECLARE @MAX INT = (SELECT MAX(PK) FROM @Fechas)
DECLARE @x_FechaSelec DATE, @x_DiaSelec INT

BEGIN TRAN
BEGIN TRY
	IF EXISTS(SELECT TOP 1(1) FROM ReportesApp_RRHH_Asistencia_Compensaciones WHERE IDPersona = @IDPersona AND FechaCompensa = @FechaCompensa) BEGIN
		SET @exito = '-6 = El día '+CAST(@FechaCompensa as VARCHAR(11))+' ya se encuentra compensado.'
		ROLLBACK
		GOTO Terminar
	END
	ELSE BEGIN
		INSERT INTO ReportesApp_RRHH_Asistencia_Compensaciones(IDPersona, FechaTrabajada,IdTipoAsisTrabajada,FechaCompensa,IdTipoAsisCompensa,FHRegistra,UsuarioRegistra)
		VALUES(@IDPersona, @FechaCompensa, 32, @FechaCompensa, 55, GETDATE(), @Usuario)

		--SE VERIFICA SI HAY ASISTENCIAS YA REGISTRADAS, LAS ELIMINADOS 
		DELETE R FROM ReportesApp_RRHH_Asistencia R
		INNER JOIN @Asistencias A ON A.IdPersona = R.IDPersona AND A.Fecha = R.Fecha

		INSERT INTO ReportesApp_RRHH_Asistencia (IdPersona, Planilla, Fecha, IdTipoAsist, ConceptoAcceso, FHRegistra, UserCrea)
		SELECT DISTINCT A.IdPersona, 'CD', A.Fecha, A.IdTipoAsist, T.ConceptoAcceso, GETDATE(), @Usuario
		FROM @Asistencias A
		INNER JOIN ReportesApp_RRHH_Asistencia_Tipo T ON T.IdTipoAsist = A.IdTipoAsist

		WHILE @I <= @MAX BEGIN
			SELECT @x_FechaSelec = FECHA, @x_DiaSelec = DAY(FECHA) 
			FROM @Fechas
			WHERE PK = @I

			UPDATE V 
	        SET V.D1 = CASE WHEN DAY(A.Fecha) = 1 THEN @LetraAsistencia ELSE V.D1 END,
				V.D2 = CASE WHEN DAY(A.Fecha) = 2 THEN @LetraAsistencia ELSE V.D2 END,
				V.D3 = CASE WHEN DAY(A.Fecha) = 3 THEN @LetraAsistencia ELSE V.D3 END,
				V.D4 = CASE WHEN DAY(A.Fecha) = 4 THEN @LetraAsistencia ELSE V.D4 END,
				V.D5 = CASE WHEN DAY(A.Fecha) = 5 THEN @LetraAsistencia ELSE V.D5 END,
				V.D6 = CASE WHEN DAY(A.Fecha) = 6 THEN @LetraAsistencia ELSE V.D6 END,
				V.D7 = CASE WHEN DAY(A.Fecha) = 7 THEN @LetraAsistencia ELSE V.D7 END,
				V.D8 = CASE WHEN DAY(A.Fecha) = 8 THEN @LetraAsistencia ELSE V.D8 END,
				V.D9 = CASE WHEN DAY(A.Fecha) = 9 THEN @LetraAsistencia ELSE V.D9 END,
				V.D10 = CASE WHEN DAY(A.Fecha) = 10 THEN @LetraAsistencia ELSE V.D10 END,
				V.D11 = CASE WHEN DAY(A.Fecha) = 11 THEN @LetraAsistencia ELSE V.D11 END,
				V.D12 = CASE WHEN DAY(A.Fecha) = 12 THEN @LetraAsistencia ELSE V.D12 END,
				V.D13 = CASE WHEN DAY(A.Fecha) = 13 THEN @LetraAsistencia ELSE V.D13 END,
				V.D14 = CASE WHEN DAY(A.Fecha) = 14 THEN @LetraAsistencia ELSE V.D14 END,
				V.D15 = CASE WHEN DAY(A.Fecha) = 15 THEN @LetraAsistencia ELSE V.D15 END,
				V.D16 = CASE WHEN DAY(A.Fecha) = 16 THEN @LetraAsistencia ELSE V.D16 END,
				V.D17 = CASE WHEN DAY(A.Fecha) = 17 THEN @LetraAsistencia ELSE V.D17 END,
				V.D18 = CASE WHEN DAY(A.Fecha) = 18 THEN @LetraAsistencia ELSE V.D18 END,
				V.D19 = CASE WHEN DAY(A.Fecha) = 19 THEN @LetraAsistencia ELSE V.D19 END,
				V.D20 = CASE WHEN DAY(A.Fecha) = 20 THEN @LetraAsistencia ELSE V.D20 END,
				V.D21 = CASE WHEN DAY(A.Fecha) = 21 THEN @LetraAsistencia ELSE V.D21 END,
				V.D22 = CASE WHEN DAY(A.Fecha) = 22 THEN @LetraAsistencia ELSE V.D22 END,
				V.D23 = CASE WHEN DAY(A.Fecha) = 23 THEN @LetraAsistencia ELSE V.D23 END,
				V.D24 = CASE WHEN DAY(A.Fecha) = 24 THEN @LetraAsistencia ELSE V.D24 END,
				V.D25 = CASE WHEN DAY(A.Fecha) = 25 THEN @LetraAsistencia ELSE V.D25 END,
				V.D26 = CASE WHEN DAY(A.Fecha) = 26 THEN @LetraAsistencia ELSE V.D26 END,
				V.D27 = CASE WHEN DAY(A.Fecha) = 27 THEN @LetraAsistencia ELSE V.D27 END,
				V.D28 = CASE WHEN DAY(A.Fecha) = 28 THEN @LetraAsistencia ELSE V.D28 END,
				V.D29 = CASE WHEN DAY(A.Fecha) = 29 THEN @LetraAsistencia ELSE V.D29 END,
				V.D30 = CASE WHEN DAY(A.Fecha) = 30 THEN @LetraAsistencia ELSE V.D30 END,
				V.D31 = CASE WHEN DAY(A.Fecha) = 31 THEN @LetraAsistencia ELSE V.D31 END
			FROM ReportesApp_RRHH_AsistenciaView V
			LEFT JOIN @Asistencias A ON A.IDPersona = V.IDPersona AND A.Fecha = @x_FechaSelec
			WHERE V.Anio = YEAR(@FechaCompensa) AND V.Mes = MONTH(@FechaCompensa) AND V.CodPlanilla = 'CD'
			
			SET @I = @I + 1
		END
	END

	IF @Usuario NOT IN ('EPIZAN','LVILLANUEVA','CGUZMAN','JHERRERAI','JALBAN','ECHAVEZ','GREYES','HMINCHOLA','APEREZ','MARANDAR','AACHAVEZ','CHIDALGO') BEGIN 
		SET @exito = '-10 = No tienes permiso para esta acción.'
		ROLLBACK
		GOTO Terminar
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

--------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 19-08-2025
-- Description:	ELIMINAR COMPENSACION VOLCAN
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_RRHH_Asistencias_EliminarCompensacion_Volcan]
@IDPersona INT,
@IDTipoAsist INT,
@Fecha DATE,
@Usuario VARCHAR(20)
AS
DECLARE @exito VARCHAR(MAX)

SET @exito = '0 = Liberación Exitosa.'

DECLARE @Asistencias TABLE (IdPersona INT, Fecha DATE, IdTipoAsist INT, Letra VARCHAR(2))
DECLARE @Fechas TABLE (PK INT IDENTITY(1,1) PRIMARY KEY, fecha DATE)
DECLARE @LetraAsistencia VARCHAR(2) = NULL

INSERT INTO @Asistencias(IdPersona, Fecha, IdTipoAsist,Letra)
VALUES (@IDPersona, @Fecha, 0, 'CO')

INSERT INTO @Fechas (fecha)
SELECT DISTINCT Fecha FROM @Asistencias

DECLARE @I INT = 1
DECLARE @MAX INT = (SELECT MAX(PK) FROM @Fechas)
DECLARE @x_FechaSelec DATE, @x_DiaSelec INT

BEGIN TRAN
BEGIN TRY
	DELETE FROM ReportesApp_RRHH_Asistencia_Compensaciones
	WHERE IDPersona = @IDPersona AND FechaTrabajada = @Fecha AND FechaCompensa = @Fecha

	DELETE R
	FROM ReportesApp_RRHH_Asistencia R
	INNER JOIN @Asistencias A ON A.IdPersona = R.IDPersona AND A.Fecha = R.Fecha

	WHILE @I <= @MAX BEGIN
		SELECT @x_FechaSelec = FECHA, @x_DiaSelec = DAY(FECHA) 
		FROM @Fechas
		WHERE PK = @I

		UPDATE V 
	    SET V.D1 = CASE WHEN DAY(A.Fecha) = 1 THEN @LetraAsistencia ELSE V.D1 END,
		    V.D2 = CASE WHEN DAY(A.Fecha) = 2 THEN @LetraAsistencia ELSE V.D2 END,
	        V.D3 = CASE WHEN DAY(A.Fecha) = 3 THEN @LetraAsistencia ELSE V.D3 END,
			V.D4 = CASE WHEN DAY(A.Fecha) = 4 THEN @LetraAsistencia ELSE V.D4 END,
			V.D5 = CASE WHEN DAY(A.Fecha) = 5 THEN @LetraAsistencia ELSE V.D5 END,
			V.D6 = CASE WHEN DAY(A.Fecha) = 6 THEN @LetraAsistencia ELSE V.D6 END,
			V.D7 = CASE WHEN DAY(A.Fecha) = 7 THEN @LetraAsistencia ELSE V.D7 END,
			V.D8 = CASE WHEN DAY(A.Fecha) = 8 THEN @LetraAsistencia ELSE V.D8 END,
			V.D9 = CASE WHEN DAY(A.Fecha) = 9 THEN @LetraAsistencia ELSE V.D9 END,
			V.D10 = CASE WHEN DAY(A.Fecha) = 10 THEN @LetraAsistencia ELSE V.D10 END,
			V.D11 = CASE WHEN DAY(A.Fecha) = 11 THEN @LetraAsistencia ELSE V.D11 END,
			V.D12 = CASE WHEN DAY(A.Fecha) = 12 THEN @LetraAsistencia ELSE V.D12 END,
			V.D13 = CASE WHEN DAY(A.Fecha) = 13 THEN @LetraAsistencia ELSE V.D13 END,
			V.D14 = CASE WHEN DAY(A.Fecha) = 14 THEN @LetraAsistencia ELSE V.D14 END,
			V.D15 = CASE WHEN DAY(A.Fecha) = 15 THEN @LetraAsistencia ELSE V.D15 END,
			V.D16 = CASE WHEN DAY(A.Fecha) = 16 THEN @LetraAsistencia ELSE V.D16 END,
			V.D17 = CASE WHEN DAY(A.Fecha) = 17 THEN @LetraAsistencia ELSE V.D17 END,
			V.D18 = CASE WHEN DAY(A.Fecha) = 18 THEN @LetraAsistencia ELSE V.D18 END,
			V.D19 = CASE WHEN DAY(A.Fecha) = 19 THEN @LetraAsistencia ELSE V.D19 END,
			V.D20 = CASE WHEN DAY(A.Fecha) = 20 THEN @LetraAsistencia ELSE V.D20 END,
			V.D21 = CASE WHEN DAY(A.Fecha) = 21 THEN @LetraAsistencia ELSE V.D21 END,
			V.D22 = CASE WHEN DAY(A.Fecha) = 22 THEN @LetraAsistencia ELSE V.D22 END,
			V.D23 = CASE WHEN DAY(A.Fecha) = 23 THEN @LetraAsistencia ELSE V.D23 END,
			V.D24 = CASE WHEN DAY(A.Fecha) = 24 THEN @LetraAsistencia ELSE V.D24 END,
			V.D25 = CASE WHEN DAY(A.Fecha) = 25 THEN @LetraAsistencia ELSE V.D25 END,
			V.D26 = CASE WHEN DAY(A.Fecha) = 26 THEN @LetraAsistencia ELSE V.D26 END,
			V.D27 = CASE WHEN DAY(A.Fecha) = 27 THEN @LetraAsistencia ELSE V.D27 END,
			V.D28 = CASE WHEN DAY(A.Fecha) = 28 THEN @LetraAsistencia ELSE V.D28 END,
			V.D29 = CASE WHEN DAY(A.Fecha) = 29 THEN @LetraAsistencia ELSE V.D29 END,
			V.D30 = CASE WHEN DAY(A.Fecha) = 30 THEN @LetraAsistencia ELSE V.D30 END,
			V.D31 = CASE WHEN DAY(A.Fecha) = 31 THEN @LetraAsistencia ELSE V.D31 END
		FROM ReportesApp_RRHH_AsistenciaView V
		LEFT JOIN @Asistencias A ON A.IDPersona = V.IDPersona AND A.Fecha = @x_FechaSelec
		WHERE V.Anio = YEAR(@Fecha) AND V.Mes = MONTH(@Fecha) AND V.CodPlanilla = 'CD'

		SET @I = @I + 1
	END

	IF @Usuario NOT IN ('SCHAVEZ','JMIGUEL','LVILLANUEVA','NPEREZ','CGUZMAN','GREYES','HMINCHOLA','JHERRERAI','JALBAN','EPIZAN','CHIDALGO') BEGIN
		SET @exito = '-10 = No tienes permiso para esta acción.'
		ROLLBACK
		GOTO Terminar
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
IF LEFT(@Exito,1)='-' OR cast(left(@Exito,1) as int)>0 BEGIN
	DECLARE @storedProcedureName VARCHAR(100)
	SELECT @storedProcedureName = OBJECT_NAME(@@PROCID)
	SET @exito = @exito + CHAR(13)+CHAR(13)+'SP Name: '+@storedProcedureName
END

SELECT @exito exito

--------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 19-08-2025
-- Description:	LISTAR COMPENSACION VOLCAN
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_RRHH_Asistencias_ListarCompensacion_Volcan]
@Opcion INT,
@IDPersona INT,
@FechaIni DATE,
@FechaFin DATE
AS
BEGIN
	DECLARE @x_FIniciaContrato DATE
	
	SELECT @x_FIniciaContrato = FechaIngreso FROM EmpleadoMast WHERE Empleado = @IDPersona

	IF (@Opcion = 1) BEGIN		-- COMPENSACION
		SELECT A.IDPersona, A.IDTipoAsist, ROW_NUMBER() OVER(ORDER BY A.IDPersona ASC) AS 'NRO',
		UPPER(DATENAME(dw,A.Fecha)) AS 'DIA', CONVERT(VARCHAR,A.Fecha,103) AS 'FECHA'
		FROM ReportesApp_RRHH_Asistencia A
		WHERE (A.IDPersona = @IDPersona) AND (A.IDTipoAsist = 55) AND (A.Fecha BETWEEN @FechaIni AND @FechaFin)
		AND (A.Fecha > @x_FIniciaContrato)
		ORDER BY 3 DESC
	END

	IF (@Opcion = 2) BEGIN		-- COMPENSACION ADELANTADA
		SELECT CA.IDPersona, 57 AS IDTipoAsist, ROW_NUMBER() OVER(ORDER BY CA.IDPersona ASC) AS 'NRO',
		UPPER(DATENAME(DW,CA.Fecha)) AS 'DIA', CONVERT(VARCHAR,CA.Fecha,103) AS 'FECHA_COMP',
		ISNULL(CONVERT(VARCHAR,CA.FechaAsiste,103),' ') AS 'FECHA_ASISTE',
		CASE WHEN (SELECT IDTipoAsist FROM ReportesApp_RRHH_Asistencia WHERE IDPersona = CA.IDPersona AND Fecha = CA.FechaAsiste) = 32
		THEN 'COMPENSADO' ELSE 'PENDIENTE' END AS 'ESTADO'
		FROM ReportesApp_RRHH_Asistencia_CompensacionesAdelantadas CA
		WHERE CA.IDPersona = @IDPersona AND CA.Fecha BETWEEN @FechaIni AND @FechaFin AND CA.Fecha > @x_FIniciaContrato
		ORDER BY CA.Fecha DESC 
	END
END

---------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 22-08-2025
-- Description:	COMPENSACION ADELANTADA VOLCAN
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_RRHH_Asistencias_CompensarAdelantado_Volcan]
@IDPersona INT,
@FechaSeleccionada DATE,
@FechaCompensa DATE,
@Usuario VARCHAR(20)
AS
DECLARE @exito VARCHAR(MAX)

SET @exito = '0 = Compensación registrada exitosamente.'

DECLARE @Asistencias TABLE (IdPersona INT, Fecha DATE, IdTipoAsist INT, Letra VARCHAR(2))
DECLARE @Fechas TABLE (PK INT IDENTITY(1,1) PRIMARY KEY, Fecha DATE)

DECLARE @LetraAsistencia VARCHAR(2) = 'CA'

INSERT INTO @Asistencias (IdPersona, Fecha, IdTipoAsist, Letra)
VALUES (@IDPersona, @FechaSeleccionada, 57, 'CA')

INSERT INTO @Fechas (Fecha)
SELECT DISTINCT Fecha FROM @Asistencias

DECLARE @x_FIniciaContrato DATE = (SELECT FechaIngreso FROM EmpleadoMast WHERE Empleado = @IDPersona)

IF (@FechaSeleccionada <= @x_FIniciaContrato) BEGIN
	SET @exito = '-1 = No puede programar una compensación antes de la fecha de ingreso del trabajador.'

	GOTO Terminar
END

--VERIFICAMOS QUE EN LA FECHA NO HAYA, LICENCIAS DESDE RRHH:
DECLARE @Tmp_Personas_Con_Licencias TABLE (IDPersona INT, Nombre VARCHAR(100), TipoLicencia CHAR(2), DescripcionLicencia VARCHAR(50), FInicio DATETIME,
										   FFinal DATETIME, dias INT)

INSERT INTO @Tmp_Personas_Con_Licencias(IDPersona,Nombre,TipoLicencia,DescripcionLicencia,FInicio,FFinal,dias)
SELECT HR_LICENCIAS.EMPLEADO, NOMBRECOMPLETO, TIPOLICENCIA, DescripcionLocal, FECHAINICIO, FECHAFINAL, DATEDIFF(DAY,FECHAINICIO,FECHAfinal) + 1 as dias
FROM HR_LICENCIAS, PERSONAMAST, AS_CARNETIDENTIFICACION, MA_MiscelaneosDetalle, EMPLEADOMAST
LEFT JOIN AC_COSTCENTERMST ON EMPLEADOMAST.CENTROCOSTOS = AC_COSTCENTERMST.COSTCENTER
WHERE HR_LICENCIAS.EMPLEADO = PERSONAMAST.PERSONA AND EMPLEADOMAST.EMPLEADO = PERSONAMAST.PERSONA AND EMPLEADOMAST.ESTADO = 'A'
AND AS_CARNETIDENTIFICACION.Empleado = Empleadomast.empleado AND MA_MiscelaneosDetalle.CodigoElemento=HR_LICENCIAS.TipoLicencia AND MA_MiscelaneosDetalle.AplicacionCodigo = 'HR'
AND MA_MiscelaneosDetalle.CodigoTabla = 'LICENCIA' AND MA_MiscelaneosDetalle.Compania = '999999' AND
(EmpleadoMast.CompaniaSocio IN ('10000000')) AND (EmpleadoMast.TipoPlanilla IN ('EM','OB','PR')) AND (EmpleadoMast.LocaciondePago = 'S' OR 'S' = 'S') 
AND (YEAR(HR_LICENCIAS.FechaInicio) = YEAR(@FechaSeleccionada) AND MONTH(HR_LICENCIAS.FechaInicio) = MONTH(@FechaSeleccionada))

IF EXISTS(SELECT TOP 1 (1) FROM @Asistencias A INNER JOIN @Tmp_Personas_Con_Licencias L ON L.IDPersona = A.IdPersona
WHERE A.Fecha BETWEEN L.FInicio AND L.FFinal) BEGIN
	DECLARE @x_FechaModifLic VARCHAR(30), @x_ConductorLic VARCHAR(100), @x_MotivoLic VARCHAR(50), @x_FechasLic VARCHAR(50), @x_diasLic CHAR(2)
		
	SELECT TOP 1 @x_ConductorLic = L.Nombre, @x_FechaModifLic = CAST(A.Fecha AS VARCHAR(11)), @x_MotivoLic = L.DescripcionLicencia,
	@x_FechasLic = CAST(L.FInicio AS VARCHAR(11))+ ' AL '+CAST(L.FFinal AS VARCHAR(11)), @x_diasLic = CAST(L.dias AS VARCHAR)
	FROM @Asistencias A 
	INNER JOIN @Tmp_Personas_Con_Licencias L ON L.IDPersona = A.IdPersona
	WHERE A.Fecha BETWEEN L.FInicio AND L.FFinal
		
	SET @exito = '-1 = No puede Modificar la Asistencia: '+ @x_FechaModifLic +CHAR(13)
				 +'Conductor: '+@x_ConductorLic+CHAR(13)+
				 +'Motivo: '+@x_MotivoLic+CHAR(13)+
				 +'Fecha: '+@x_FechasLic+CHAR(13)+
				 +'Cant.: '+@x_diasLic+' Dias.'
	
	GOTO Terminar
END

-- CARGA DE VACACIONES:
DECLARE @Tmp_Vacaciones TABLE (PK INT IDENTITY(1,1), IDPersona INT, Trabajador VARCHAR(100), FInicio DATETIME, FFinal DATETIME, dias INT,
CompaniaSocio VARCHAR(8), ConceptoAcceso VARCHAR(5), IdTipoAsist INT, CodTipo CHAR(1))

INSERT INTO @Tmp_Vacaciones(IDPersona,Trabajador,FInicio,FFinal,dias,CompaniaSocio,ConceptoAcceso,IdTipoAsist,CodTipo)
SELECT Empleado IDPersona, P.NombreCompleto, V.FechaInicio, V.FechaFin, V.DiasUtilizacion, CompaniaSocio, 'VACA', 41, 'V'  
FROM PR_VacacionUtilizacion V
LEFT JOIN PersonaMast P ON P.Persona = V.Empleado 
WHERE (YEAR(V.FechaInicio) = YEAR(@FechaSeleccionada) AND MONTH(V.FechaInicio) = MONTH(@FechaSeleccionada)) AND TipoUtilizacion = 'GOC'

IF EXISTS(SELECT TOP 1(1) FROM @Asistencias A INNER JOIN @Tmp_Vacaciones V ON V.IDPersona = A.IdPersona
WHERE A.Fecha BETWEEN V.FInicio AND V.FFinal) BEGIN
	SELECT TOP 1 @x_ConductorLic = L.Trabajador, @x_FechaModifLic = CAST(A.Fecha AS VARCHAR(11)), @x_MotivoLic = 'Vacaciones',
	@x_FechasLic = CAST(L.FInicio AS VARCHAR(11))+' AL '+CAST(L.FFinal AS VARCHAR(11)), @x_diasLic = CAST(L.dias AS VARCHAR)
	FROM @Asistencias A 
	INNER JOIN @Tmp_Vacaciones L ON L.IDPersona = A.IdPersona
	WHERE A.Fecha BETWEEN L.FInicio AND L.FFinal
		
	SET @exito = '-2 = Existe conflicto con conductor '+CAST(@x_ConductorLic AS VARCHAR)+CHAR(13)+
				 +@x_FechaModifLic+', Vacac. de RRHH.'
	GOTO Terminar
END

--Restriccion: Si tiene suspensión debe liberarse con Supervisión.
IF EXISTS (SELECT TOP 1(1) FROM ReportesApp_RRHH_Asistencia R INNER JOIN @Asistencias A ON A.IdPersona = R.IDPersona AND A.Fecha = R.Fecha
WHERE R.IDTipoAsist = 7) --Suspensión
BEGIN
	SET @exito = '-3 = La fecha seleccionada tiene suspensión y como tal, no puede ser modificada. Consultar con Sistemas'
	GOTO Terminar
END

IF EXISTS (SELECT TOP 1(1) FROM ReportesApp_RRHH_Asistencia_DescansoF R INNER JOIN @Asistencias A ON A.IdPersona = R.IDPersona AND A.Fecha = R.Fecha) 
BEGIN
	SET @exito = '-4 = No puede compensar una Fecha de Descanso Físico. Revisar.'
	GOTO Terminar
END

DECLARE @DxCN INT = (SELECT COUNT(*) FROM ReportesApp_RRHH_Asistencia_Noches R INNER JOIN @Asistencias A ON A.IdPersona = R.IDPersona AND A.Fecha = R.Fecha
					 AND FechaCompensa IS NULL AND R.Fecha > @x_FIniciaContrato)

IF ISNULL(@DxCN,0) > 0 BEGIN
	SET @exito = '-5 = No puede registrar la compensacion adelantada por tener aún '+CAST(@DxCN AS VARCHAR)+' noches pendientes de compensar. Revisar.'
	GOTO Terminar
END

DECLARE @NroComp INT = (SELECT CompPendientes FROM ReportesApp_RRHH_Asistencias_CompensacionesVolcan WHERE idPersona = @IDPersona)

IF ISNULL(@NroComp,0) > 0 BEGIN
	SET @exito = '-6 = No puede registrar la compensacion adelantada por tener aún '+CAST(@NroComp AS VARCHAR)+' días pendientes de compensar. Revisar.'
	Goto Terminar
END

DECLARE @I INT = 1
DECLARE @MAX INT = (SELECT MAX(PK) FROM @Fechas)
DECLARE @x_FechaSelec DATE, @x_DiaSelec INT

BEGIN TRAN
BEGIN TRY
	IF EXISTS(SELECT TOP 1(1) FROM ReportesApp_RRHH_Asistencia_CompensacionesAdelantadas WHERE Fecha = @FechaSeleccionada and IDPersona = @IDPersona) BEGIN
		SET @exito = '-7 = La fecha a compensar de este empleado ya se encuentra registrada.'
		ROLLBACK
		GOTO Terminar
	END

	IF EXISTS(SELECT TOP 1(1) FROM ReportesApp_RRHH_Asistencia_CompensacionesAdelantadas WHERE FechaAsiste = @FechaCompensa and IDPersona = @IDPersona) BEGIN
		SET @exito = '-7 = La fecha de asistencia de este empleado ya se encuentra registrada.'
		ROLLBACK
		GOTO Terminar
	END

	DELETE R
	FROM ReportesApp_RRHH_Asistencia R
	INNER JOIN @Asistencias A ON A.IdPersona = R.IDPersona AND A.Fecha = R.Fecha

	INSERT INTO ReportesApp_RRHH_Asistencia (IdPersona, Planilla, Fecha, IdTipoAsist, ConceptoAcceso, FHRegistra ,UserCrea)
	SELECT DISTINCT A.IdPersona, 'CD', A.Fecha, A.IdTipoAsist, T.ConceptoAcceso, GETDATE(), @Usuario
	FROM @Asistencias A
	INNER JOIN ReportesApp_RRHH_Asistencia_Tipo T ON T.IdTipoAsist = A.IdTipoAsist
	
	INSERT INTO ReportesApp_RRHH_Asistencia_CompensacionesAdelantadas (IDPersona, Fecha, FechaAsiste, Motivo, UserRegistra, FHRegistra)
	VALUES (@IDPersona, @FechaSeleccionada, @FechaCompensa, 'SIN PRODUCCION - SIN DIAS X COMPENSAR', @Usuario, GETDATE())

	WHILE @I <= @MAX BEGIN
		SELECT @x_FechaSelec = FECHA, @x_DiaSelec = DAY(FECHA) FROM @Fechas WHERE PK = @I

		UPDATE V 
	    SET V.D1 = CASE WHEN DAY(A.Fecha) = 1 THEN @LetraAsistencia ELSE V.D1 END,
			V.D2 = CASE WHEN DAY(A.Fecha) = 2 THEN @LetraAsistencia ELSE V.D2 END,
			V.D3 = CASE WHEN DAY(A.Fecha) = 3 THEN @LetraAsistencia ELSE V.D3 END,
			V.D4 = CASE WHEN DAY(A.Fecha) = 4 THEN @LetraAsistencia ELSE V.D4 END,
			V.D5 = CASE WHEN DAY(A.Fecha) = 5 THEN @LetraAsistencia ELSE V.D5 END,
			V.D6 = CASE WHEN DAY(A.Fecha) = 6 THEN @LetraAsistencia ELSE V.D6 END,
			V.D7 = CASE WHEN DAY(A.Fecha) = 7 THEN @LetraAsistencia ELSE V.D7 END,
			V.D8 = CASE WHEN DAY(A.Fecha) = 8 THEN @LetraAsistencia ELSE V.D8 END,
			V.D9 = CASE WHEN DAY(A.Fecha) = 9 THEN @LetraAsistencia ELSE V.D9 END,
			V.D10 = CASE WHEN DAY(A.Fecha) = 10 THEN @LetraAsistencia ELSE V.D10 END,
			V.D11 = CASE WHEN DAY(A.Fecha) = 11 THEN @LetraAsistencia ELSE V.D11 END,
			V.D12 = CASE WHEN DAY(A.Fecha) = 12 THEN @LetraAsistencia ELSE V.D12 END,
			V.D13 = CASE WHEN DAY(A.Fecha) = 13 THEN @LetraAsistencia ELSE V.D13 END,
			V.D14 = CASE WHEN DAY(A.Fecha) = 14 THEN @LetraAsistencia ELSE V.D14 END,
			V.D15 = CASE WHEN DAY(A.Fecha) = 15 THEN @LetraAsistencia ELSE V.D15 END,
			V.D16 = CASE WHEN DAY(A.Fecha) = 16 THEN @LetraAsistencia ELSE V.D16 END,
			V.D17 = CASE WHEN DAY(A.Fecha) = 17 THEN @LetraAsistencia ELSE V.D17 END,
			V.D18 = CASE WHEN DAY(A.Fecha) = 18 THEN @LetraAsistencia ELSE V.D18 END,
			V.D19 = CASE WHEN DAY(A.Fecha) = 19 THEN @LetraAsistencia ELSE V.D19 END,
			V.D20 = CASE WHEN DAY(A.Fecha) = 20 THEN @LetraAsistencia ELSE V.D20 END,
			V.D21 = CASE WHEN DAY(A.Fecha) = 21 THEN @LetraAsistencia ELSE V.D21 END,
			V.D22 = CASE WHEN DAY(A.Fecha) = 22 THEN @LetraAsistencia ELSE V.D22 END,
			V.D23 = CASE WHEN DAY(A.Fecha) = 23 THEN @LetraAsistencia ELSE V.D23 END,
			V.D24 = CASE WHEN DAY(A.Fecha) = 24 THEN @LetraAsistencia ELSE V.D24 END,
			V.D25 = CASE WHEN DAY(A.Fecha) = 25 THEN @LetraAsistencia ELSE V.D25 END,
			V.D26 = CASE WHEN DAY(A.Fecha) = 26 THEN @LetraAsistencia ELSE V.D26 END,
			V.D27 = CASE WHEN DAY(A.Fecha) = 27 THEN @LetraAsistencia ELSE V.D27 END,
			V.D28 = CASE WHEN DAY(A.Fecha) = 28 THEN @LetraAsistencia ELSE V.D28 END,
			V.D29 = CASE WHEN DAY(A.Fecha) = 29 THEN @LetraAsistencia ELSE V.D29 END,
			V.D30 = CASE WHEN DAY(A.Fecha) = 30 THEN @LetraAsistencia ELSE V.D30 END,
			V.D31 = CASE WHEN DAY(A.Fecha) = 31 THEN @LetraAsistencia ELSE V.D31 END
		FROM ReportesApp_RRHH_AsistenciaView V
		LEFT JOIN @Asistencias A ON A.IDPersona = V.IDPersona AND A.Fecha = @x_FechaSelec
		WHERE V.Anio = YEAR(@FechaSeleccionada) AND V.Mes = MONTH(@FechaSeleccionada) AND V.CodPlanilla = 'CD'

		SET @I = @I + 1
	END

	IF @Usuario NOT IN('LVILLANUEVA','NPEREZ','CGUZMAN','HMINCHOLA','JHERRERAI','JALBAN','EPIZAN','MARANDAR') BEGIN
		SET @exito = '-10 = No tienes permiso para esta acción'
		ROLLBACK
		GOTO Terminar
	END	  	

	--AVISO DEL SISTEMA--
	--*********************--
	DECLARE @NombreConductor VARCHAR(200)
	DECLARE @Mensaje AS VARCHAR(MAX), @Asunto VARCHAR(max)
	DECLARE @Datos VARCHAR(MAX) = ''

	SET @NombreConductor = (SELECT NombreCompleto FROM PersonaMast where Persona = @IDPersona)
		
	SET @Asunto = 'COMPENSACIÓN ADELANTADA ('+CAST(@FechaSeleccionada AS VARCHAR)+' por el día '+CAST(@FechaCompensa AS VARCHAR)+'): '+ISNULL(@NombreConductor,'') 
	SET @Mensaje =  '<p><h2>'+@Asunto+' </h2></p>' +
					'<p>	
						<table border="1" cellspacing="1" cellpadding="1" >
							<thead align="center" style="background-color:#88CD7B;">
								<tr>
									<td align="center" width="90"><b>EVENTO</b></td>
									<td align="center" width="90"><b>Motivo</b></td>
									<td align="center" width="150"><b>Usuario</b></td>
									<td align="center" width="150"><b>F.Hora</b></td>												
								</tr>
							</thead>
							<tbody>'
									+ '<td>COMPENSACION ADELANTADA</td>' 
									+ '<td>SIN PRODUCCION - SIN DIAS X COMPENSAR</td>' 
									+ '<td>' + ISNULL(@Usuario,'') + '</td>' 												
									+ '<td>'+CONVERT(CHAR(10),GETDATE(),103) + ' ' + RIGHT(CONVERT(VARCHAR,GETDATE(),22),10)+'</td>' 							
							+'</tbody>
						</table>
					</p>'
				+'<p><font face="verdana" size="2" color="DarkRed"> Tecnologías de la Información - GRUPO TRANSPESA</font></p>'

	EXEC msdb.dbo.sp_send_dbmail 
		@profile_name='AVISODESISTEMA',
		@recipients='controldocumentario@transpesa.com.pe;legal@transpesa.com.pe;auditoria@transpesa.com.pe;gerenciaoperaciones@transpesa.com.pe;talentohumano@transpesa.com.pe;asistente.gth@transpesa.com.pe',
		@subject=@Asunto,
		@body_format = 'HTML', 
		@body=@Mensaje
				
	--FIN AVISO DEL SISTEMA--
	--*********************--
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

----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 22-08-2025
-- Description:	ELIMINAR COMPENSACION ADELANTADA VOLCAN
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_RRHH_Asistencias_EliminarCompAdelantado_Volcan]
@IDPersona INT,
@IDTipoAsist INT,
@FechaComp DATE,
@FechaAsist DATE,
@Usuario VARCHAR(20)
AS
DECLARE @exito VARCHAR(MAX)

SET @exito = '0 = Liberación Exitosa.'

DECLARE @Asistencias TABLE (IdPersona INT, Fecha DATE, IdTipoAsist INT, Letra VARCHAR(2))
DECLARE @Fechas TABLE (PK INT IDENTITY(1,1) PRIMARY KEY, fecha DATE)
DECLARE @LetraAsistencia VARCHAR(2) = NULL

INSERT INTO @Asistencias(IdPersona, Fecha, IdTipoAsist, Letra)
VALUES (@IDPersona, @FechaComp, 0, @LetraAsistencia)

INSERT INTO @Fechas (fecha)
SELECT DISTINCT Fecha FROM @Asistencias

DECLARE @I INT = 1
DECLARE @MAX INT = (SELECT MAX(PK) FROM @Fechas)
DECLARE @x_FechaSelec DATE, @x_DiaSelec INT

BEGIN TRAN
BEGIN TRY
	DELETE FROM ReportesApp_RRHH_Asistencia_CompensacionesAdelantadas WHERE IDPersona = @IDPersona AND Fecha = @FechaComp

	DELETE R FROM ReportesApp_RRHH_Asistencia R
	INNER JOIN @Asistencias A ON A.IdPersona = R.IDPersona AND A.Fecha = R.Fecha

	WHILE @I <= @MAX BEGIN
		SELECT @x_FechaSelec = FECHA, @x_DiaSelec = DAY(FECHA) 
		FROM @Fechas WHERE PK = @I

		UPDATE V 
	    SET V.D1 = CASE WHEN DAY(A.Fecha) = 1 THEN @LetraAsistencia ELSE V.D1 END,
		    V.D2 = CASE WHEN DAY(A.Fecha) = 2 THEN @LetraAsistencia ELSE V.D2 END,
	        V.D3 = CASE WHEN DAY(A.Fecha) = 3 THEN @LetraAsistencia ELSE V.D3 END,
			V.D4 = CASE WHEN DAY(A.Fecha) = 4 THEN @LetraAsistencia ELSE V.D4 END,
			V.D5 = CASE WHEN DAY(A.Fecha) = 5 THEN @LetraAsistencia ELSE V.D5 END,
			V.D6 = CASE WHEN DAY(A.Fecha) = 6 THEN @LetraAsistencia ELSE V.D6 END,
			V.D7 = CASE WHEN DAY(A.Fecha) = 7 THEN @LetraAsistencia ELSE V.D7 END,
			V.D8 = CASE WHEN DAY(A.Fecha) = 8 THEN @LetraAsistencia ELSE V.D8 END,
			V.D9 = CASE WHEN DAY(A.Fecha) = 9 THEN @LetraAsistencia ELSE V.D9 END,
			V.D10 = CASE WHEN DAY(A.Fecha) = 10 THEN @LetraAsistencia ELSE V.D10 END,
			V.D11 = CASE WHEN DAY(A.Fecha) = 11 THEN @LetraAsistencia ELSE V.D11 END,
			V.D12 = CASE WHEN DAY(A.Fecha) = 12 THEN @LetraAsistencia ELSE V.D12 END,
			V.D13 = CASE WHEN DAY(A.Fecha) = 13 THEN @LetraAsistencia ELSE V.D13 END,
			V.D14 = CASE WHEN DAY(A.Fecha) = 14 THEN @LetraAsistencia ELSE V.D14 END,
			V.D15 = CASE WHEN DAY(A.Fecha) = 15 THEN @LetraAsistencia ELSE V.D15 END,
			V.D16 = CASE WHEN DAY(A.Fecha) = 16 THEN @LetraAsistencia ELSE V.D16 END,
			V.D17 = CASE WHEN DAY(A.Fecha) = 17 THEN @LetraAsistencia ELSE V.D17 END,
			V.D18 = CASE WHEN DAY(A.Fecha) = 18 THEN @LetraAsistencia ELSE V.D18 END,
			V.D19 = CASE WHEN DAY(A.Fecha) = 19 THEN @LetraAsistencia ELSE V.D19 END,
			V.D20 = CASE WHEN DAY(A.Fecha) = 20 THEN @LetraAsistencia ELSE V.D20 END,
			V.D21 = CASE WHEN DAY(A.Fecha) = 21 THEN @LetraAsistencia ELSE V.D21 END,
			V.D22 = CASE WHEN DAY(A.Fecha) = 22 THEN @LetraAsistencia ELSE V.D22 END,
			V.D23 = CASE WHEN DAY(A.Fecha) = 23 THEN @LetraAsistencia ELSE V.D23 END,
			V.D24 = CASE WHEN DAY(A.Fecha) = 24 THEN @LetraAsistencia ELSE V.D24 END,
			V.D25 = CASE WHEN DAY(A.Fecha) = 25 THEN @LetraAsistencia ELSE V.D25 END,
			V.D26 = CASE WHEN DAY(A.Fecha) = 26 THEN @LetraAsistencia ELSE V.D26 END,
			V.D27 = CASE WHEN DAY(A.Fecha) = 27 THEN @LetraAsistencia ELSE V.D27 END,
			V.D28 = CASE WHEN DAY(A.Fecha) = 28 THEN @LetraAsistencia ELSE V.D28 END,
			V.D29 = CASE WHEN DAY(A.Fecha) = 29 THEN @LetraAsistencia ELSE V.D29 END,
			V.D30 = CASE WHEN DAY(A.Fecha) = 30 THEN @LetraAsistencia ELSE V.D30 END,
			V.D31 = CASE WHEN DAY(A.Fecha) = 31 THEN @LetraAsistencia ELSE V.D31 END
		FROM ReportesApp_RRHH_AsistenciaView V
		LEFT JOIN @Asistencias A ON A.IDPersona = V.IDPersona AND A.Fecha = @x_FechaSelec
		WHERE V.Anio = YEAR(@FechaComp) AND V.Mes = MONTH(@FechaComp) AND V.CodPlanilla = 'CD'

		SET @I = @I + 1
	END

	IF @Usuario NOT IN ('SCHAVEZ','JMIGUEL','LVILLANUEVA','NPEREZ','CGUZMAN','GREYES','HMINCHOLA','JHERRERAI','JALBAN','EPIZAN','CHIDALGO') BEGIN
		SET @exito = '-10 = No tienes permiso para esta acción.'
		ROLLBACK
		GOTO Terminar
	END

	--AVISO DEL SISTEMA--
	--*********************--
	DECLARE  @NombreConductor VARCHAR(200)
	DECLARE @Mensaje AS VARCHAR(MAX),@Asunto VARCHAR(max)
	DECLARE @Datos VARCHAR(MAX) = ''
	SET @NombreConductor = (SELECT NombreCompleto FROM PersonaMast where Persona= @IDPersona)
		
	SET @Asunto = 'LIBERACION - COMPENSACIÓN ADELANTADA ('+CAST(@FechaComp AS VARCHAR)+' por el día '+CAST(@FechaAsist AS VARCHAR)+'): '+ISNULL(@NombreConductor,'') 
	SET @Mensaje = '<p><h2>'+@Asunto+' </h2></p>'
					+ '<p>	
						<table border="1" cellspacing="1" cellpadding="1" >
							<thead align="center" style="background-color:#88CD7B;">
								<tr>
									<td align="center" width="90"><b>EVENTO</b></td>
									<td align="center" width="90"><b>Motivo</b></td>
									<td align="center" width="150"><b>Usuario</b></td>
									<td align="center" width="150"><b>F.Hora</b></td>												
								</tr>
							</thead>
							<tbody>'
									+ '<td>COMPENSACION ADELANTADA</td>' 
									+ '<td>LIBERACION</td>' 
									+ '<td>' + ISNULL(@Usuario,'') + '</td>' 												
									+ '<td>'+CONVERT(CHAR(10),GETDATE(),103) + ' ' + RIGHT(CONVERT(varchar,GETDATE(),22),10)+'</td>' 							
							+'</tbody>
						</table>
					</p>'
				+'<p><font face="verdana" size="2" color="DarkRed"> Tecnologías de la Información - GRUPO TRANSPESA</font></p>'
	
	EXEC msdb.dbo.sp_send_dbmail 
		@profile_name='AVISODESISTEMA',
		@recipients='auditoria@transpesa.com.pe;gerenciaoperaciones@transpesa.com.pe;talentohumano@transpesa.com.pe;asistente.gth@transpesa.com.pe',
		@subject=@Asunto,
		@body_format = 'HTML', 
		@body=@Mensaje
				
	--FIN AVISO DEL SISTEMA--
	--*********************--
END TRY

BEGIN CATCH
	SET @exito = RTRIM(LTRIM(STR(ERROR_NUMBER()))) + '=' + ERROR_MESSAGE()
	ROLLBACK
	GOTO Terminar
END CATCH

IF @@TRANCOUNT > 0
	COMMIT;

TERMINAR:
IF LEFT(@Exito,1)='-' OR cast(left(@Exito,1) as int)>0 BEGIN
	DECLARE @storedProcedureName VARCHAR(100)
	SELECT @storedProcedureName = OBJECT_NAME(@@PROCID)
	SET @exito = @exito + CHAR(13)+CHAR(13)+'SP Name: '+@storedProcedureName
END

SELECT @exito exito

----------------------------------------------------------------------------

SELECT * FROM PersonaMast WHERE NombreCompleto LIKE '%ABUNDO %'

SELECT * FROM ReportesApp_RRHH_Asistencia
SELECT * FROM ReportesApp_RRHH_Asistencia_Compensaciones



