
-- CREAR TABLA ReportesApp_Operaciones_PlanConductores_Regimen

-----------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 24-10-2025
-- Description:	LISTAR PLAN DE CONDUCTORES
-- =============================================
/*
EXEC ReportesApp_Operaciones_PlanConductores_Listar @Fecha = '24/10/2025'
*/
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_PlanConductores_Listar]
@Fecha DATE
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @T_PlanConductores TABLE (idRegimen INT, Fecha DATE, Operacion VARCHAR(30), DiasTrabajo INT, DiasDescanso INT, Regimen VARCHAR(20), NroUnidades INT,
	RatioRegimen DECIMAL(10,2), RatioVacaciones DECIMAL(10,2), RatioTotal DECIMAL(10,2), CondRequerido INT, CondActual INT, Variacion INT)
	DECLARE @T_TotalUnidades TABLE (idOperacion INT, Total INT)
	DECLARE @T_Asistencias TABLE(idOperacion INT, Total INT)

	INSERT INTO @T_TotalUnidades
	SELECT UC.IdProgramacion, COUNT(UC.IdUnidad)
	FROM ReportesApp_Operacion_MaestroUnidadesConductor UC
	LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
	WHERE (V.TipoVehiculo = 1) AND (V.Estado = 2)
	GROUP BY UC.IdProgramacion
	ORDER BY UC.IdProgramacion

	INSERT INTO @T_Asistencias
	SELECT C.CodigoEnapu, COUNT(A.IDPersona)
	FROM ReportesApp_RRHH_AsistenciaView A
	LEFT JOIN OP_TR_Conductor C ON C.IdPersona = A.IDPersona
	LEFT JOIN EmpleadoMast E ON E.Empleado = A.IDPersona
	LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = C.CodigoEnapu
	WHERE A.CodPlanilla = 'CD' AND A.Anio = YEAR(@Fecha) AND A.Mes = MONTH(@Fecha) AND C.CodigoEnapu IN (1,2,3,10) AND E.Estado = 'A' 
	GROUP BY C.CodigoEnapu
	ORDER BY C.CodigoEnapu

	INSERT INTO @T_PlanConductores(idRegimen, Fecha, Operacion, DiasTrabajo, DiasDescanso, Regimen, NroUnidades, RatioRegimen, RatioVacaciones, CondActual)
	SELECT R.idRegimen, @Fecha, O.Descripcion, R.DiasTrabajo, R.DiasDescanso, CONVERT(VARCHAR,R.DiasTrabajo) + ' X ' + CONVERT(VARCHAR,R.DiasDescanso),
	U.Total, CAST(DiasTrabajo + DiasDescanso AS DECIMAL(10,2)) / CAST(DiasTrabajo AS DECIMAL(10,2)), CAST(13 AS DECIMAL(10,2)) / 12, A.Total
	FROM ReportesApp_Operaciones_PlanConductores_Regimen R
	LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = R.idOperacion
	LEFT JOIN @T_TotalUnidades U ON U.idOperacion = R.idOperacion
	LEFT JOIN @T_Asistencias A ON A.idOperacion = R.idOperacion

	UPDATE @T_PlanConductores
	SET RatioTotal = RatioRegimen * RatioVacaciones

	UPDATE @T_PlanConductores
	SET CondRequerido = ROUND(RatioTotal * NroUnidades,0)

	UPDATE @T_PlanConductores
	SET Variacion = CondRequerido - CondActual

	SELECT Operacion AS 'OPERACION', Regimen AS 'REGIMEN', NroUnidades AS 'NRO UT', RatioRegimen AS 'RATIO REGIMEN', RatioVacaciones AS 'RATIO VACACIONES',
	RatioTotal AS 'RATIO TOTAL', CondRequerido AS 'COND REQUERIDOS', CondActual 'COND ACTUALES', Variacion AS 'VARIACION'
	FROM @T_PlanConductores
	UNION ALL
	SELECT 'TOTAL' AS 'OPERACION', NULL AS 'REGIMEN', SUM(NroUnidades) AS 'NRO UT', NULL AS 'RATIO REGIMEN', NULL AS 'RATIO VACACIONES', NULL AS 'RATIO TOTAL',
	SUM(CondRequerido) AS 'COND REQUERIDOS', SUM(CondActual) AS 'COND ACTUALES', SUM(Variacion) AS 'VARIACION' 
	FROM @T_PlanConductores
END

