
-- CREAR TABLA ReportesApp_Operaciones_ProgramacionVC_Registros

-- CREAR TABLA ReportesApp_Operaciones_ProgramacionVC_PersonalView

-------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 10-06-2025
-- Description:	LISTAR CONDUCTORES - VAC/COM
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ProgramacionVC_ListarConductores]
@OpcionVC INT,
@Filtro VARCHAR(250)
AS
BEGIN
	IF (@OpcionVC = 1) BEGIN		-- LISTAR VACACIONES
		SELECT DISTINCT P.Persona AS 'PERSONA', RTRIM(LTRIM(P.NombreCompleto)) AS 'CONDUCTOR',
		ISNULL((SELECT TOP(1) PendientePagoAnterior FROM PR_VacacionPeriodo WHERE Empleado = P.Persona AND CompaniaSocio = '10000000' ORDER BY Ano DESC),0) AS 'DIAS'
		FROM empleadomast E
		INNER JOIN  DBO.PersonaMast P ON P.Persona = E.Empleado 
		LEFT JOIN OP_TR_Conductor C ON C.IdPersona = P.Persona
		INNER JOIN PR_TipoPlanilla tp on tp.TipoPlanilla = E.TipoPlanilla
		INNER JOIN ReportesApp_RRHH_AsistenciaView V ON V.IDPersona = E.Empleado
		WHERE (V.CodPlanilla = 'CD') AND (E.Estado = 'A') AND (C.Estado = 'A')
		AND (ISNULL((SELECT TOP(1) PendientePagoAnterior FROM PR_VacacionPeriodo WHERE Empleado = P.Persona AND CompaniaSocio = '10000000' ORDER BY Ano DESC),0) != 0)
		AND (P.NombreCompleto LIKE '%' + @Filtro + '%')
		ORDER BY RTRIM(LTRIM(P.NombreCompleto))
	END

	IF (@OpcionVC = 2) BEGIN		-- LISTAR COMPENSACIONES
		SELECT * FROM
		(SELECT D.IDPersona AS 'PERSONA', RTRIM(LTRIM(P.NombreCompleto)) AS 'CONDUCTOR', ISNULL(COUNT(D.Fecha),0) AS 'DIAS'
		FROM ReportesApp_RRHH_Asistencia A
		INNER JOIN ReportesApp_RRHH_Asistencia_DescansoF D ON D.IDPersona=A.IDPersona AND D.Fecha=A.Fecha
		LEFT JOIN ReportesApp_RRHH_Asistencia_Compensaciones C ON C.IDPersona=A.IDPersona AND C.FechaTrabajada=A.Fecha
		LEFT JOIN PersonaMast P ON P.Persona = D.IDPersona
		INNER JOIN OP_TR_Conductor CO ON CO.IdPersona = P.Persona
		LEFT JOIN empleadomast E ON P.Persona = E.Empleado
		WHERE (A.IDTipoAsist = 32) AND (C.FechaCompensa IS NULL) AND (E.Estado = 'A') AND (CO.Estado = 'A') AND (CO.CodigoEnapu IN (1,2,3,4))
		GROUP BY D.IDPersona, P.NombreCompleto
		UNION
		SELECT CV.idPersona AS 'PERSONA', RTRIM(LTRIM(PE.NombreCompleto)) AS 'CONDUCTOR', ISNULL(CV.CompPendientes,0) AS 'DIAS'
		FROM ReportesApp_RRHH_Asistencias_CompensacionesVolcan CV
		LEFT JOIN PersonaMast PE ON PE.Persona = CV.idPersona
		LEFT JOIN OP_TR_Conductor CD ON CD.IdPersona = PE.Persona
		WHERE (CD.Estado = 'A')) X
		WHERE (X.CONDUCTOR LIKE '%' + @Filtro + '%')
		ORDER BY X.CONDUCTOR
	END

	IF (@OpcionVC = 3) BEGIN		-- LISTAR ASISTENCIAS
		SELECT DISTINCT P.Persona AS 'PERSONA', RTRIM(LTRIM(P.NombreCompleto)) AS 'CONDUCTOR', 0 AS 'DIAS'
		FROM empleadomast E
		INNER JOIN  DBO.PersonaMast P ON P.Persona = E.Empleado 
		LEFT JOIN OP_TR_Conductor C ON C.IdPersona = P.Persona
		INNER JOIN PR_TipoPlanilla tp on tp.TipoPlanilla = E.TipoPlanilla
		INNER JOIN ReportesApp_RRHH_AsistenciaView V ON V.IDPersona = E.Empleado
		WHERE (V.CodPlanilla = 'CD') AND (E.Estado = 'A') AND (C.Estado = 'A') AND (P.NombreCompleto LIKE '%' + @Filtro + '%')
		ORDER BY RTRIM(LTRIM(P.NombreCompleto))
	END
END

-------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 10-06-2025
-- Description:	LISTAR VACACIONES DE CONDUCTOR
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ProgramacionVC_ListarVacacionesConductor]
@OpcionVC INT,
@Persona INT
AS
BEGIN
	IF (@OpcionVC = 1) BEGIN		-- LISTAR VACACIONES
		SELECT TOP(10) VP.Empleado AS 'PERSONA', P.Ano+'-'+CAST(P.Ano+1 as varchar) AS PERIODO,
		CONVERT(VARCHAR,VP.FechaInicio,103) AS 'INICIO', CONVERT(VARCHAR,VP.FechaFin,103) AS 'FIN', VP.DiasPago AS 'DÍAS'
		FROM PR_VacacionPago VP
		LEFT JOIN PR_VacacionPeriodo P ON P.NumeroPeriodo = VP.NumeroPeriodo AND VP.Empleado = P.Empleado
		WHERE VP.Empleado = @Persona 
		ORDER BY VP.PERIODO DESC
	END
	
	IF (@OpcionVC = 2) BEGIN		-- LISTAR COMPENSACIONES
		DECLARE @IDOperacion INT = (SELECT CodigoEnapu FROM OP_TR_Conductor WHERE IdPersona = @Persona)

		IF (@IDOperacion = 10) BEGIN
			DECLARE @x_FIniciaContrato DATE = (SELECT FechaIngreso FROM EmpleadoMast WHERE Empleado = @Persona)

			SELECT TOP(20) A.IDPersona AS 'PERSONA', UPPER(DATENAME(DW,A.Fecha)) AS 'DIA', CONVERT(VARCHAR,A.Fecha,103) AS 'FECHA',
			CASE WHEN VC.Aprobado = 1 THEN 'APROBADO' ELSE 'PENDIENTE' END AS 'ESTADO'
			FROM ReportesApp_RRHH_Asistencia A
			LEFT JOIN ReportesApp_Operaciones_ProgramacionVC_Registros VC ON (VC.IDPersona = A.IDPersona) AND (A.Fecha = VC.FechaFin)
			WHERE (A.IDPersona = @Persona) AND (A.IDTipoAsist = 55) AND (A.Fecha > @x_FIniciaContrato)
			ORDER BY A.Fecha DESC
		END
		ELSE BEGIN 
			SELECT D.IDPersona AS 'PERSONA', DATENAME(WEEKDAY, D.Fecha) AS 'DIA', CONVERT(VARCHAR,D.Fecha,103) AS 'DIA_PENDIENTE',
			CONVERT(VARCHAR,VC.FechaFin,103) AS 'DIA_COMPENSAR', CASE WHEN VC.Aprobado = 1 THEN 'APROBADO' ELSE 'PENDIENTE' END AS 'ESTADO'
			FROM ReportesApp_RRHH_Asistencia A
			INNER JOIN ReportesApp_RRHH_Asistencia_DescansoF D ON D.IDPersona=A.IDPersona AND D.Fecha=A.Fecha
			LEFT JOIN ReportesApp_RRHH_Asistencia_Compensaciones C ON C.IDPersona=A.IDPersona AND C.FechaTrabajada=A.Fecha
			LEFT JOIN PersonaMast P ON P.Persona = D.IDPersona
			INNER JOIN OP_TR_Conductor CO ON CO.IdPersona = P.Persona
			LEFT JOIN empleadomast E ON P.Persona = E.Empleado
			LEFT JOIN ReportesApp_Operaciones_ProgramacionVC_Registros VC ON (VC.IDPersona = D.IDPersona) AND (D.Fecha = VC.FechaIni)
			WHERE (A.IDTipoAsist = 32) AND (C.FechaCompensa IS NULL) AND (E.Estado = 'A') AND (CO.Estado = 'A') AND D.IDPersona = @Persona
			ORDER BY P.NombreCompleto, D.Fecha DESC
		END
	END

	IF (@OpcionVC = 3) BEGIN		-- LISTAR ASISTENCIAS
		SELECT TOP(10) VC.IDPersona AS 'PERSONA', CONVERT(VARCHAR,VC.FechaIni,103) AS 'INICIO', CONVERT(VARCHAR,VC.FechaFin,103) AS 'FIN'
		FROM ReportesApp_Operaciones_ProgramacionVC_Registros VC
		WHERE (VC.IDPersona = @Persona) AND (VC.Codigo = 'A ')
		ORDER BY VC.FechaFin DESC
	END
END

-------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 11/12/2024
-- Description:	REGISTRAR PROGRAMACION ASISTENCIAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ProgramacionVC_RegistrarAsistencias]
@Periodo VARCHAR(6),
@Persona INT,
@DiasPendientes INT,
@FechaInicio DATE,
@FechaFin DATE,
@Codigo VARCHAR(20),
@Usuario VARCHAR(20)
AS
DECLARE @exito VARCHAR(MAX)
DECLARE @co2 INT

SET @exito = '0 = Asistencias del Conductor Registradas.'

BEGIN TRAN
BEGIN TRY
	DECLARE @NombreCompleto VARCHAR(350) = (SELECT LTRIM(RTRIM(P.NombreCompleto)) FROM PersonaMast P WHERE P.Persona = @Persona)
	
	IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE IDPersona = @Persona AND Codigo LIKE '%(AP)%' AND @FechaInicio BETWEEN FechaIni AND FechaFin)) BEGIN
		SET @Exito = '-1 = El conductor ya tiene programaciones en esta fecha.'
		ROLLBACK
		GOTO Terminar
	END

	IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE IDPersona = @Persona AND Codigo LIKE '%(AP)%' AND @FechaFin BETWEEN FechaIni AND FechaFin)) BEGIN
		SET @Exito = '-1 = El conductor ya tiene programaciones en esta fecha.'
		ROLLBACK
		GOTO Terminar
	END

	IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE IDPersona = @Persona AND Codigo LIKE '%(PR)%' AND @FechaInicio BETWEEN FechaIni AND FechaFin)) BEGIN
		SET @Exito = '-1 = El conductor ya tiene programaciones en esta fecha.'
		ROLLBACK
		GOTO Terminar
	END

	IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE IDPersona = @Persona AND Codigo LIKE '%(PR)%' AND @FechaFin BETWEEN FechaIni AND FechaFin)) BEGIN
		SET @Exito = '-1 = El conductor ya tiene programaciones en esta fecha.'
		ROLLBACK
		GOTO Terminar
	END
	ELSE BEGIN
		SET @co2 = (SELECT MAX(idProgVC) FROM ReportesApp_Operaciones_ProgramacionVC_Registros)
		SET @co2 = ISNULL(@co2,0) + 1 

		DECLARE @Dia DATE = CONVERT(VARCHAR,YEAR(@FechaInicio))+'-'+CONVERT(VARCHAR,MONTH(@FechaInicio))+'-01'
		DECLARE @FechaFin2 DATE = CAST(DATEADD(MONTH,DATEDIFF(MONTH,0,@Dia)+1,0)-1 AS DATE)
		DECLARE @UltimoDiaMes TINYINT = DAY(@FechaFin2)

		DECLARE @Dia2 DATE = CONVERT(VARCHAR,YEAR(@FechaFin))+'-'+CONVERT(VARCHAR,MONTH(@FechaFin))+'-01'
		DECLARE @FechaFin3 DATE = CAST(DATEADD(MONTH,DATEDIFF(MONTH,0,@Dia2)+1,0)-1 AS DATE)
		DECLARE @UltimoDiaMes2 TINYINT = DAY(@FechaFin3)

		INSERT INTO ReportesApp_Operaciones_ProgramacionVC_Registros(idProgVC,IDPersona,Codigo,FRetorno,FechaIni,FechaFin,FechaRetorno,NumeroDias,Aprobado,UsuarioRegistra,FechaRegistra,UsuarioAprueba,FechaAprueba)
		SELECT @co2, @Persona, @Codigo, 0, @FechaInicio, @FechaFin, @FechaFin, DATEDIFF(DAY,@FechaInicio,@FechaFin) + 1, 0, @Usuario, GETDATE(), @Usuario, GETDATE()

		IF (NOT EXISTS(SELECT * FROM ReportesApp_Operaciones_ProgramacionVC_PersonalView WHERE IDPersona = @Persona AND (Anio = YEAR(@FechaInicio)
		AND Mes = MONTH(@FechaInicio)))) BEGIN
			INSERT INTO ReportesApp_Operaciones_ProgramacionVC_PersonalView (IDPersona,Anio,Mes,NroDias)
			SELECT @Persona, YEAR(@FechaInicio), MONTH(@FechaInicio), @UltimoDiaMes

			IF (@FechaFin > @FechaFin2 AND NOT EXISTS(SELECT * FROM ReportesApp_Operaciones_ProgramacionVC_PersonalView WHERE IDPersona = @Persona AND Anio = YEAR(@FechaFin)
			AND Mes = MONTH(@FechaFin))) BEGIN
				INSERT INTO ReportesApp_Operaciones_ProgramacionVC_PersonalView (IDPersona,Anio,Mes,NroDias)
				SELECT @Persona, YEAR(@FechaFin), MONTH(@FechaFin), @UltimoDiaMes2
			END
		END

		WHILE (@FechaInicio <= @FechaFin) BEGIN
			UPDATE P 
			SET P.D1 = CASE WHEN DAY(@FechaInicio) = 1 THEN @Codigo ELSE P.D1 END,
				P.D2 = CASE WHEN DAY(@FechaInicio) = 2 THEN @Codigo ELSE P.D2 END,
				P.D3 = CASE WHEN DAY(@FechaInicio) = 3 THEN @Codigo ELSE P.D3 END,
				P.D4 = CASE WHEN DAY(@FechaInicio) = 4 THEN @Codigo ELSE P.D4 END,
				P.D5 = CASE WHEN DAY(@FechaInicio) = 5 THEN @Codigo ELSE P.D5 END,
				P.D6 = CASE WHEN DAY(@FechaInicio) = 6 THEN @Codigo ELSE P.D6 END,
				P.D7 = CASE WHEN DAY(@FechaInicio) = 7 THEN @Codigo ELSE P.D7 END,
				P.D8 = CASE WHEN DAY(@FechaInicio) = 8 THEN @Codigo ELSE P.D8 END,
				P.D9 = CASE WHEN DAY(@FechaInicio) = 9 THEN @Codigo ELSE P.D9 END,
				P.D10 = CASE WHEN DAY(@FechaInicio) = 10 THEN @Codigo ELSE P.D10 END,
				P.D11 = CASE WHEN DAY(@FechaInicio) = 11 THEN @Codigo ELSE P.D11 END,
				P.D12 = CASE WHEN DAY(@FechaInicio) = 12 THEN @Codigo ELSE P.D12 END,
				P.D13 = CASE WHEN DAY(@FechaInicio) = 13 THEN @Codigo ELSE P.D13 END,
				P.D14 = CASE WHEN DAY(@FechaInicio) = 14 THEN @Codigo ELSE P.D14 END,
				P.D15 = CASE WHEN DAY(@FechaInicio) = 15 THEN @Codigo ELSE P.D15 END,
				P.D16 = CASE WHEN DAY(@FechaInicio) = 16 THEN @Codigo ELSE P.D16 END,
				P.D17 = CASE WHEN DAY(@FechaInicio) = 17 THEN @Codigo ELSE P.D17 END,
				P.D18 = CASE WHEN DAY(@FechaInicio) = 18 THEN @Codigo ELSE P.D18 END,
				P.D19 = CASE WHEN DAY(@FechaInicio) = 19 THEN @Codigo ELSE P.D19 END,
				P.D20 = CASE WHEN DAY(@FechaInicio) = 20 THEN @Codigo ELSE P.D20 END,
				P.D21 = CASE WHEN DAY(@FechaInicio) = 21 THEN @Codigo ELSE P.D21 END,
				P.D22 = CASE WHEN DAY(@FechaInicio) = 22 THEN @Codigo ELSE P.D22 END,
				P.D23 = CASE WHEN DAY(@FechaInicio) = 23 THEN @Codigo ELSE P.D23 END,
				P.D24 = CASE WHEN DAY(@FechaInicio) = 24 THEN @Codigo ELSE P.D24 END,
				P.D25 = CASE WHEN DAY(@FechaInicio) = 25 THEN @Codigo ELSE P.D25 END,
				P.D26 = CASE WHEN DAY(@FechaInicio) = 26 THEN @Codigo ELSE P.D26 END,
				P.D27 = CASE WHEN DAY(@FechaInicio) = 27 THEN @Codigo ELSE P.D27 END,
				P.D28 = CASE WHEN DAY(@FechaInicio) = 28 THEN @Codigo ELSE P.D28 END,
				P.D29 = CASE WHEN DAY(@FechaInicio) = 29 THEN @Codigo ELSE P.D29 END,
				P.D30 = CASE WHEN DAY(@FechaInicio) = 30 THEN @Codigo ELSE P.D30 END,
				P.D31 = CASE WHEN DAY(@FechaInicio) = 31 THEN @Codigo ELSE P.D31 END
			FROM ReportesApp_Operaciones_ProgramacionVC_PersonalView P
			WHERE P.IDPersona = @Persona AND P.Anio = YEAR(@FechaInicio) AND P.Mes = MONTH(@FechaInicio)

			IF (@FechaInicio > @FechaFin2) BEGIN
				UPDATE P 
				SET P.D1 = CASE WHEN DAY(@FechaInicio) = 1 THEN @Codigo ELSE P.D1 END,
					P.D2 = CASE WHEN DAY(@FechaInicio) = 2 THEN @Codigo ELSE P.D2 END,
					P.D3 = CASE WHEN DAY(@FechaInicio) = 3 THEN @Codigo ELSE P.D3 END,
					P.D4 = CASE WHEN DAY(@FechaInicio) = 4 THEN @Codigo ELSE P.D4 END,
					P.D5 = CASE WHEN DAY(@FechaInicio) = 5 THEN @Codigo ELSE P.D5 END,
					P.D6 = CASE WHEN DAY(@FechaInicio) = 6 THEN @Codigo ELSE P.D6 END,
					P.D7 = CASE WHEN DAY(@FechaInicio) = 7 THEN @Codigo ELSE P.D7 END,
					P.D8 = CASE WHEN DAY(@FechaInicio) = 8 THEN @Codigo ELSE P.D8 END,
					P.D9 = CASE WHEN DAY(@FechaInicio) = 9 THEN @Codigo ELSE P.D9 END,
					P.D10 = CASE WHEN DAY(@FechaInicio) = 10 THEN @Codigo ELSE P.D10 END,
					P.D11 = CASE WHEN DAY(@FechaInicio) = 11 THEN @Codigo ELSE P.D11 END,
					P.D12 = CASE WHEN DAY(@FechaInicio) = 12 THEN @Codigo ELSE P.D12 END,
					P.D13 = CASE WHEN DAY(@FechaInicio) = 13 THEN @Codigo ELSE P.D13 END,
					P.D14 = CASE WHEN DAY(@FechaInicio) = 14 THEN @Codigo ELSE P.D14 END,
					P.D15 = CASE WHEN DAY(@FechaInicio) = 15 THEN @Codigo ELSE P.D15 END,
					P.D16 = CASE WHEN DAY(@FechaInicio) = 16 THEN @Codigo ELSE P.D16 END,
					P.D17 = CASE WHEN DAY(@FechaInicio) = 17 THEN @Codigo ELSE P.D17 END,
					P.D18 = CASE WHEN DAY(@FechaInicio) = 18 THEN @Codigo ELSE P.D18 END,
					P.D19 = CASE WHEN DAY(@FechaInicio) = 19 THEN @Codigo ELSE P.D19 END,
					P.D20 = CASE WHEN DAY(@FechaInicio) = 20 THEN @Codigo ELSE P.D20 END,
					P.D21 = CASE WHEN DAY(@FechaInicio) = 21 THEN @Codigo ELSE P.D21 END,
					P.D22 = CASE WHEN DAY(@FechaInicio) = 22 THEN @Codigo ELSE P.D22 END,
					P.D23 = CASE WHEN DAY(@FechaInicio) = 23 THEN @Codigo ELSE P.D23 END,
					P.D24 = CASE WHEN DAY(@FechaInicio) = 24 THEN @Codigo ELSE P.D24 END,
					P.D25 = CASE WHEN DAY(@FechaInicio) = 25 THEN @Codigo ELSE P.D25 END,
					P.D26 = CASE WHEN DAY(@FechaInicio) = 26 THEN @Codigo ELSE P.D26 END,
					P.D27 = CASE WHEN DAY(@FechaInicio) = 27 THEN @Codigo ELSE P.D27 END,
					P.D28 = CASE WHEN DAY(@FechaInicio) = 28 THEN @Codigo ELSE P.D28 END,
					P.D29 = CASE WHEN DAY(@FechaInicio) = 29 THEN @Codigo ELSE P.D29 END,
					P.D30 = CASE WHEN DAY(@FechaInicio) = 30 THEN @Codigo ELSE P.D30 END,
					P.D31 = CASE WHEN DAY(@FechaInicio) = 31 THEN @Codigo ELSE P.D31 END
				FROM ReportesApp_Operaciones_ProgramacionVC_PersonalView P
				WHERE P.IDPersona = @Persona AND P.Anio = YEAR(@FechaFin) AND P.Mes = MONTH(@FechaFin)
			END

			SET @FechaInicio = DATEADD(DAY,1,@FechaInicio)
		END
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

-------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 10/06/2024
-- Description:	REGISTRAR PROGRAMACION VACACIONES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ProgramacionVC_RegistrarVacaciones]
@Periodo VARCHAR(6),
@Persona INT,
@DiasPendientes INT,
@FechaInicio DATE,
@FechaFin DATE,
@Codigo VARCHAR(20),
@FechaRetorno INT,
@Usuario VARCHAR(20)
AS
DECLARE @exito VARCHAR(MAX)
DECLARE @co2 INT

SET @exito = '0 = Vacaciones del Conductor Registradas.'

BEGIN TRAN
BEGIN TRY
	DECLARE @NombreCompleto VARCHAR(350) = (SELECT LTRIM(RTRIM(P.NombreCompleto)) FROM PersonaMast P WHERE P.Persona = @Persona)
	DECLARE @Contador INT = (SELECT COUNT(A.NumeroAdelanto) FROM AP_GastoAdelanto A							 LEFT JOIN PersonaMast P ON (A.Persona = P.Persona)							 LEFT JOIN AP_CajaChica C ON LTRIM(RTRIM(C.NumeroDocumentoInterno)) = LTRIM(RTRIM(A.NumeroDocumentoInterno))							 LEFT JOIN ReportesApp_Operaciones_TicketGasto_Registro TG ON LTRIM(RTRIM(TG.CodGasto)) = LTRIM(RTRIM(A.NumeroDocumentoInterno))							 LEFT JOIN ReportesApp_Operacion_Previaje_Registros R ON R.NroTicket = TG.NroTicket							 WHERE (A.TipoAdelanto = 'E') AND (A.UnidadNegocio IN ('0001','0002','TRAN')) AND YEAR(A.FechaDocumento)>'2023' AND (A.Estado = 'PA') AND (R.Estado IN (1,9))
							 AND (TG.EstadoLiquidacion = 0) AND (C.CajaChicaNumero IS NULL) AND (A.Descripcion LIKE '%' + 'GASTOS DE VIAJE' + '%') AND (A.Persona = @Persona))
	DECLARE @CodigoOP INT = (SELECT CodigoEnapu FROM OP_TR_Conductor WHERE IdPersona = @Persona AND Estado = 'A')
	DECLARE @idConductor INT = (SELECT IdConductor FROM OP_TR_Conductor WHERE IdPersona = @Persona AND Estado = 'A')
	DECLARE @TEMP_ADELANTOS TABLE(Nro INT, NumeroAdelanto INT, Planilla VARCHAR(20), IdProgramacion INT, Sucursal VARCHAR(10), TotalDias INT)
	DECLARE @i INT

	IF (@CodigoOP IN (1,2,3,4,10)) BEGIN
		IF (@Contador > 0) BEGIN
			SET @Exito = '-1 = El conductor ' + @NombreCompleto +' tiene ' + CONVERT(VARCHAR,@Contador) + ' planillas pendientes de rendir, no se le pueden programar vacaciones. Debe pasar a liquidar.'
			ROLLBACK
			GOTO Terminar
		END
	END

	IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE IDPersona = @Persona AND Codigo LIKE '%VA%' AND @FechaInicio BETWEEN FechaIni AND FechaFin)) BEGIN
		SET @Exito = '-1 = El conductor ya tiene programaciones en esta fecha.'
		ROLLBACK
		GOTO Terminar
	END

	IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE IDPersona = @Persona AND Codigo LIKE '%VA%' AND @FechaFin BETWEEN FechaIni AND FechaFin)) BEGIN
		SET @Exito = '-1 = El conductor ya tiene programaciones en esta fecha.'
		ROLLBACK
		GOTO Terminar
	END
	
	IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE IDPersona = @Persona AND Codigo LIKE '%CO%' AND @FechaInicio BETWEEN FechaFin AND DATEADD(DAY,-1,FechaRetorno))) BEGIN
		SET @Exito = '-1 = El conductor ya tiene programaciones en esta fecha.'
		ROLLBACK
		GOTO Terminar
	END

	IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE IDPersona = @Persona AND Codigo LIKE '%CO%' AND @FechaFin BETWEEN FechaFin AND DATEADD(DAY,-1,FechaRetorno))) BEGIN
		SET @Exito = '-1 = El conductor ya tiene programaciones en esta fecha.'
		ROLLBACK
		GOTO Terminar
	END
	/*
	IF (MONTH(@FechaFin) > LEFT(@periodo,2)) BEGIN
		SET @Exito = '-2 = Solo puede programar vacaciones en este periodo.'
		ROLLBACK
		GOTO Terminar
	END

	IF (MONTH(@FechaInicio) > LEFT(@periodo,2)) BEGIN
		SET @Exito = '-2 = Solo puede programar vacaciones en este periodo.'
		ROLLBACK
		GOTO Terminar
	END
	*/
	ELSE BEGIN
		SET @co2 = (SELECT MAX(idProgVC) FROM ReportesApp_Operaciones_ProgramacionVC_Registros)
		SET @co2 = ISNULL(@co2,0) + 1 

		DECLARE @Dia DATE = CONVERT(VARCHAR,YEAR(@FechaInicio))+'-'+CONVERT(VARCHAR,MONTH(@FechaInicio))+'-01'
		DECLARE @FechaFin2 DATE = CAST(DATEADD(MONTH,DATEDIFF(MONTH,0,@Dia)+1,0)-1 AS DATE)
		DECLARE @UltimoDiaMes TINYINT = DAY(@FechaFin2)

		DECLARE @Dia2 DATE = CONVERT(VARCHAR,YEAR(@FechaFin))+'-'+CONVERT(VARCHAR,MONTH(@FechaFin))+'-01'
		DECLARE @FechaFin3 DATE = CAST(DATEADD(MONTH,DATEDIFF(MONTH,0,@Dia2)+1,0)-1 AS DATE)
		DECLARE @UltimoDiaMes2 TINYINT = DAY(@FechaFin3)

		INSERT INTO ReportesApp_Operaciones_ProgramacionVC_Registros(idProgVC,IDPersona,Codigo,FRetorno,FechaIni,FechaFin,FechaRetorno,NumeroDias,Aprobado,UsuarioRegistra,FechaRegistra,UsuarioAprueba,FechaAprueba)
		SELECT @co2, @Persona, @Codigo, @FechaRetorno, @FechaInicio, @FechaFin, DATEADD(DAY,1,@FechaFin), DATEDIFF(DAY,@FechaInicio,@FechaFin) + 1, 0, @Usuario, GETDATE(), @Usuario, GETDATE()

		IF (NOT EXISTS(SELECT * FROM ReportesApp_Operaciones_ProgramacionVC_PersonalView WHERE IDPersona = @Persona AND (Anio = YEAR(@FechaInicio) 
		AND Mes = MONTH(@FechaInicio)))) BEGIN
			INSERT INTO ReportesApp_Operaciones_ProgramacionVC_PersonalView (IDPersona,Anio,Mes,NroDias)
			SELECT @Persona, YEAR(@FechaInicio), MONTH(@FechaInicio), @UltimoDiaMes

			IF (@FechaFin > @FechaFin2 AND NOT EXISTS(SELECT * FROM ReportesApp_Operaciones_ProgramacionVC_PersonalView WHERE IDPersona = @Persona AND Anio = YEAR(@FechaFin)
			AND Mes = MONTH(@FechaFin))) BEGIN
				INSERT INTO ReportesApp_Operaciones_ProgramacionVC_PersonalView (IDPersona,Anio,Mes,NroDias)
				SELECT @Persona, YEAR(@FechaFin), MONTH(@FechaFin), @UltimoDiaMes2
			END
		END

		WHILE (@FechaInicio <= @FechaFin) BEGIN
			UPDATE P 
			SET P.D1 = CASE WHEN DAY(@FechaInicio) = 1 THEN @Codigo ELSE P.D1 END,
				P.D2 = CASE WHEN DAY(@FechaInicio) = 2 THEN @Codigo ELSE P.D2 END,
				P.D3 = CASE WHEN DAY(@FechaInicio) = 3 THEN @Codigo ELSE P.D3 END,
				P.D4 = CASE WHEN DAY(@FechaInicio) = 4 THEN @Codigo ELSE P.D4 END,
				P.D5 = CASE WHEN DAY(@FechaInicio) = 5 THEN @Codigo ELSE P.D5 END,
				P.D6 = CASE WHEN DAY(@FechaInicio) = 6 THEN @Codigo ELSE P.D6 END,
				P.D7 = CASE WHEN DAY(@FechaInicio) = 7 THEN @Codigo ELSE P.D7 END,
				P.D8 = CASE WHEN DAY(@FechaInicio) = 8 THEN @Codigo ELSE P.D8 END,
				P.D9 = CASE WHEN DAY(@FechaInicio) = 9 THEN @Codigo ELSE P.D9 END,
				P.D10 = CASE WHEN DAY(@FechaInicio) = 10 THEN @Codigo ELSE P.D10 END,
				P.D11 = CASE WHEN DAY(@FechaInicio) = 11 THEN @Codigo ELSE P.D11 END,
				P.D12 = CASE WHEN DAY(@FechaInicio) = 12 THEN @Codigo ELSE P.D12 END,
				P.D13 = CASE WHEN DAY(@FechaInicio) = 13 THEN @Codigo ELSE P.D13 END,
				P.D14 = CASE WHEN DAY(@FechaInicio) = 14 THEN @Codigo ELSE P.D14 END,
				P.D15 = CASE WHEN DAY(@FechaInicio) = 15 THEN @Codigo ELSE P.D15 END,
				P.D16 = CASE WHEN DAY(@FechaInicio) = 16 THEN @Codigo ELSE P.D16 END,
				P.D17 = CASE WHEN DAY(@FechaInicio) = 17 THEN @Codigo ELSE P.D17 END,
				P.D18 = CASE WHEN DAY(@FechaInicio) = 18 THEN @Codigo ELSE P.D18 END,
				P.D19 = CASE WHEN DAY(@FechaInicio) = 19 THEN @Codigo ELSE P.D19 END,
				P.D20 = CASE WHEN DAY(@FechaInicio) = 20 THEN @Codigo ELSE P.D20 END,
				P.D21 = CASE WHEN DAY(@FechaInicio) = 21 THEN @Codigo ELSE P.D21 END,
				P.D22 = CASE WHEN DAY(@FechaInicio) = 22 THEN @Codigo ELSE P.D22 END,
				P.D23 = CASE WHEN DAY(@FechaInicio) = 23 THEN @Codigo ELSE P.D23 END,
				P.D24 = CASE WHEN DAY(@FechaInicio) = 24 THEN @Codigo ELSE P.D24 END,
				P.D25 = CASE WHEN DAY(@FechaInicio) = 25 THEN @Codigo ELSE P.D25 END,
				P.D26 = CASE WHEN DAY(@FechaInicio) = 26 THEN @Codigo ELSE P.D26 END,
				P.D27 = CASE WHEN DAY(@FechaInicio) = 27 THEN @Codigo ELSE P.D27 END,
				P.D28 = CASE WHEN DAY(@FechaInicio) = 28 THEN @Codigo ELSE P.D28 END,
				P.D29 = CASE WHEN DAY(@FechaInicio) = 29 THEN @Codigo ELSE P.D29 END,
				P.D30 = CASE WHEN DAY(@FechaInicio) = 30 THEN @Codigo ELSE P.D30 END,
				P.D31 = CASE WHEN DAY(@FechaInicio) = 31 THEN @Codigo ELSE P.D31 END
			FROM ReportesApp_Operaciones_ProgramacionVC_PersonalView P
			WHERE P.IDPersona = @Persona AND P.Anio = YEAR(@FechaInicio) AND P.Mes = MONTH(@FechaInicio)

			IF (@FechaInicio > @FechaFin2) BEGIN
				UPDATE P 
				SET P.D1 = CASE WHEN DAY(@FechaInicio) = 1 THEN @Codigo ELSE P.D1 END,
					P.D2 = CASE WHEN DAY(@FechaInicio) = 2 THEN @Codigo ELSE P.D2 END,
					P.D3 = CASE WHEN DAY(@FechaInicio) = 3 THEN @Codigo ELSE P.D3 END,
					P.D4 = CASE WHEN DAY(@FechaInicio) = 4 THEN @Codigo ELSE P.D4 END,
					P.D5 = CASE WHEN DAY(@FechaInicio) = 5 THEN @Codigo ELSE P.D5 END,
					P.D6 = CASE WHEN DAY(@FechaInicio) = 6 THEN @Codigo ELSE P.D6 END,
					P.D7 = CASE WHEN DAY(@FechaInicio) = 7 THEN @Codigo ELSE P.D7 END,
					P.D8 = CASE WHEN DAY(@FechaInicio) = 8 THEN @Codigo ELSE P.D8 END,
					P.D9 = CASE WHEN DAY(@FechaInicio) = 9 THEN @Codigo ELSE P.D9 END,
					P.D10 = CASE WHEN DAY(@FechaInicio) = 10 THEN @Codigo ELSE P.D10 END,
					P.D11 = CASE WHEN DAY(@FechaInicio) = 11 THEN @Codigo ELSE P.D11 END,
					P.D12 = CASE WHEN DAY(@FechaInicio) = 12 THEN @Codigo ELSE P.D12 END,
					P.D13 = CASE WHEN DAY(@FechaInicio) = 13 THEN @Codigo ELSE P.D13 END,
					P.D14 = CASE WHEN DAY(@FechaInicio) = 14 THEN @Codigo ELSE P.D14 END,
					P.D15 = CASE WHEN DAY(@FechaInicio) = 15 THEN @Codigo ELSE P.D15 END,
					P.D16 = CASE WHEN DAY(@FechaInicio) = 16 THEN @Codigo ELSE P.D16 END,
					P.D17 = CASE WHEN DAY(@FechaInicio) = 17 THEN @Codigo ELSE P.D17 END,
					P.D18 = CASE WHEN DAY(@FechaInicio) = 18 THEN @Codigo ELSE P.D18 END,
					P.D19 = CASE WHEN DAY(@FechaInicio) = 19 THEN @Codigo ELSE P.D19 END,
					P.D20 = CASE WHEN DAY(@FechaInicio) = 20 THEN @Codigo ELSE P.D20 END,
					P.D21 = CASE WHEN DAY(@FechaInicio) = 21 THEN @Codigo ELSE P.D21 END,
					P.D22 = CASE WHEN DAY(@FechaInicio) = 22 THEN @Codigo ELSE P.D22 END,
					P.D23 = CASE WHEN DAY(@FechaInicio) = 23 THEN @Codigo ELSE P.D23 END,
					P.D24 = CASE WHEN DAY(@FechaInicio) = 24 THEN @Codigo ELSE P.D24 END,
					P.D25 = CASE WHEN DAY(@FechaInicio) = 25 THEN @Codigo ELSE P.D25 END,
					P.D26 = CASE WHEN DAY(@FechaInicio) = 26 THEN @Codigo ELSE P.D26 END,
					P.D27 = CASE WHEN DAY(@FechaInicio) = 27 THEN @Codigo ELSE P.D27 END,
					P.D28 = CASE WHEN DAY(@FechaInicio) = 28 THEN @Codigo ELSE P.D28 END,
					P.D29 = CASE WHEN DAY(@FechaInicio) = 29 THEN @Codigo ELSE P.D29 END,
					P.D30 = CASE WHEN DAY(@FechaInicio) = 30 THEN @Codigo ELSE P.D30 END,
					P.D31 = CASE WHEN DAY(@FechaInicio) = 31 THEN @Codigo ELSE P.D31 END
				FROM ReportesApp_Operaciones_ProgramacionVC_PersonalView P
				WHERE P.IDPersona = @Persona AND P.Anio = YEAR(@FechaFin) AND P.Mes = MONTH(@FechaFin)
			END

			SET @FechaInicio = DATEADD(DAY,1,@FechaInicio)
		END
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

-----------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 10/06/2024
-- Description:	REGISTRAR PROGRAMACION COMPENSACIONES
-- =============================================
/*
exec ReportesApp_Operaciones_ProgramacionVC_RegistrarCompensaciones @Periodo=N'082025',@Persona=25078,@DiasPendientes=19,@FechaInicio='24/08/2025 10:57:32',
@FechaFin='30/08/2025 10:57:32',@Codigo=N'CO (PR)',@Usuario=N'GREYES'
*/
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ProgramacionVC_RegistrarCompensaciones]
@Periodo VARCHAR(6),
@Persona INT,
@DiasPendientes INT,
@FechaInicio DATE,
@FechaFin DATE,
@Codigo VARCHAR(20),
@FechaRetorno INT,
@Usuario VARCHAR(20)
AS
DECLARE @exito VARCHAR(MAX)
DECLARE @Nro INT = 1
DECLARE @co2 INT, @co3 INT

SET @exito = '0 = Compensaciones del Conductor Registradas.'

BEGIN TRAN
BEGIN TRY
	DECLARE @NombreCompleto VARCHAR(350) = (SELECT LTRIM(RTRIM(P.NombreCompleto)) FROM PersonaMast P WHERE P.Persona = @Persona)
	DECLARE @Contador2 INT = (SELECT COUNT(A.NumeroAdelanto) FROM AP_GastoAdelanto A							 LEFT JOIN PersonaMast P ON (A.Persona = P.Persona)							 LEFT JOIN AP_CajaChica C ON LTRIM(RTRIM(C.NumeroDocumentoInterno)) = LTRIM(RTRIM(A.NumeroDocumentoInterno))							 LEFT JOIN ReportesApp_Operaciones_TicketGasto_Registro TG ON LTRIM(RTRIM(TG.CodGasto)) = LTRIM(RTRIM(A.NumeroDocumentoInterno))							 LEFT JOIN ReportesApp_Operacion_Previaje_Registros R ON R.NroTicket = TG.NroTicket							 WHERE (A.TipoAdelanto = 'E') AND (A.UnidadNegocio IN ('0001','0002','TRAN')) AND YEAR(A.FechaDocumento) > '2023' AND (A.Estado = 'PA') AND (R.Estado IN (1,9))
							 AND (TG.EstadoLiquidacion = 0) AND (C.CajaChicaNumero IS NULL) AND (A.Descripcion LIKE '%' + 'GASTOS DE VIAJE' + '%') AND (A.Persona = @Persona))
	DECLARE @CodigoOP INT = (SELECT CodigoEnapu FROM OP_TR_Conductor WHERE IdPersona = @Persona AND Estado = 'A')
	DECLARE @idConductor INT = (SELECT IdConductor FROM OP_TR_Conductor WHERE IdPersona = @Persona AND Estado = 'A')
	DECLARE @TEMP_ADELANTOS TABLE(Nro INT, NumeroAdelanto INT, Planilla VARCHAR(20), IdProgramacion INT, Sucursal VARCHAR(10), TotalDias INT)
	DECLARE @i INT

	/*
	IF (@CodigoOP IN (1,2,3,4,10)) BEGIN
		IF (@Contador2 > 0) BEGIN
			SET @Exito = '-1 = El conductor ' + @NombreCompleto +' tiene ' + CONVERT(VARCHAR,@Contador2) + ' planillas pendientes de rendir, no se le pueden programar compensaciones. Debe pasar a liquidar.'
			ROLLBACK
			GOTO Terminar
		END
	END
	*/

	IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE IDPersona = @Persona AND Codigo LIKE '%VA%' AND @FechaInicio BETWEEN FechaIni AND FechaFin)) BEGIN
		SET @Exito = '-1 = El conductor ya tiene programaciones en esta fecha.'
		ROLLBACK
		GOTO Terminar
	END

	IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE IDPersona = @Persona AND Codigo LIKE '%VA%' AND @FechaFin BETWEEN FechaIni AND FechaFin)) BEGIN
		SET @Exito = '-1 = El conductor ya tiene programaciones en esta fecha.'
		ROLLBACK
		GOTO Terminar
	END
	
	IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE IDPersona = @Persona AND Codigo LIKE '%CO%' AND @FechaInicio BETWEEN FechaFin AND DATEADD(DAY,-1,FechaRetorno))) BEGIN
		SET @Exito = '-1 = El conductor ya tiene programaciones en esta fecha.'
		ROLLBACK
		GOTO Terminar
	END

	IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE IDPersona = @Persona AND Codigo LIKE '%CO%' AND @FechaFin BETWEEN FechaFin AND DATEADD(DAY,-1,FechaRetorno))) BEGIN
		SET @Exito = '-1 = El conductor ya tiene programaciones en esta fecha.'
		ROLLBACK
		GOTO Terminar
	END

	/*
	IF (MONTH(@FechaInicio) > LEFT(@periodo,2)) BEGIN
		SET @Exito = '-2 = Solo puede programar vacaciones en este periodo.'
		ROLLBACK
		GOTO Terminar
	END

	IF (MONTH(@FechaFin) > LEFT(@periodo,2)) BEGIN
		SET @Exito = '-2 = Solo puede programar compensaciones en este periodo.'
		ROLLBACK
		GOTO Terminar
	END
	*/
	ELSE BEGIN
		SET DATEFIRST 7

		DECLARE @IDOperacion INT = (SELECT CodigoEnapu FROM OP_TR_Conductor WHERE IdPersona = @Persona)

		IF (@IDOperacion = 10) BEGIN
			WHILE (@FechaInicio <= @FechaFin) BEGIN
				SET @co3 = (SELECT MAX(idProgVC) FROM ReportesApp_Operaciones_ProgramacionVC_Registros)
				SET @co3 = ISNULL(@co3,0) + 1

				DECLARE @DiaV DATE = CONVERT(VARCHAR,YEAR(@FechaInicio))+'-'+CONVERT(VARCHAR,MONTH(@FechaInicio))+'-01'
				DECLARE @FechaFinV DATE = CAST(DATEADD(MONTH,DATEDIFF(MONTH,0,@DiaV)+1,0)-1 AS DATE)
				DECLARE @UltimoDiaMesV TINYINT = DAY(@FechaFinV)

				DECLARE @Dia2 DATE = CONVERT(VARCHAR,YEAR(@FechaFin))+'-'+CONVERT(VARCHAR,MONTH(@FechaFin))+'-01'
				DECLARE @FechaFin3 DATE = CAST(DATEADD(MONTH,DATEDIFF(MONTH,0,@Dia2)+1,0)-1 AS DATE)
				DECLARE @UltimoDiaMes2 TINYINT = DAY(@FechaFin3)

				INSERT INTO ReportesApp_Operaciones_ProgramacionVC_Registros(idProgVC,IDPersona,Codigo,FRetorno,FechaIni,FechaFin,FechaRetorno,NumeroDias,Aprobado,UsuarioRegistra,FechaRegistra,UsuarioAprueba,FechaAprueba)
				SELECT @co3, @Persona, @Codigo, @FechaRetorno, @FechaInicio, @FechaInicio, DATEADD(DAY,1,@FechaFin), 0, 0, @Usuario, GETDATE(), @Usuario, GETDATE()

				IF (NOT EXISTS(SELECT * FROM ReportesApp_Operaciones_ProgramacionVC_PersonalView WHERE IDPersona = @Persona AND (Anio = YEAR(@FechaInicio) 
				AND Mes = MONTH(@FechaInicio)))) BEGIN
					INSERT INTO ReportesApp_Operaciones_ProgramacionVC_PersonalView (IDPersona,Anio,Mes,NroDias)
					SELECT @Persona, YEAR(@FechaInicio), MONTH(@FechaInicio), @UltimoDiaMesV
					
					IF (@FechaFin > @FechaFinV AND NOT EXISTS(SELECT * FROM ReportesApp_Operaciones_ProgramacionVC_PersonalView WHERE IDPersona = @Persona AND Anio = YEAR(@FechaFin)
					AND Mes = MONTH(@FechaFin))) BEGIN
						INSERT INTO ReportesApp_Operaciones_ProgramacionVC_PersonalView (IDPersona,Anio,Mes,NroDias)
						SELECT @Persona, YEAR(@FechaFin), MONTH(@FechaFin), @UltimoDiaMes2
					END
				END

				UPDATE P 
				SET P.D1 = CASE WHEN DAY(@FechaInicio) = 1 THEN @Codigo ELSE P.D1 END,
					P.D2 = CASE WHEN DAY(@FechaInicio) = 2 THEN @Codigo ELSE P.D2 END,
					P.D3 = CASE WHEN DAY(@FechaInicio) = 3 THEN @Codigo ELSE P.D3 END,
					P.D4 = CASE WHEN DAY(@FechaInicio) = 4 THEN @Codigo ELSE P.D4 END,
					P.D5 = CASE WHEN DAY(@FechaInicio) = 5 THEN @Codigo ELSE P.D5 END,
					P.D6 = CASE WHEN DAY(@FechaInicio) = 6 THEN @Codigo ELSE P.D6 END,
					P.D7 = CASE WHEN DAY(@FechaInicio) = 7 THEN @Codigo ELSE P.D7 END,
					P.D8 = CASE WHEN DAY(@FechaInicio) = 8 THEN @Codigo ELSE P.D8 END,
					P.D9 = CASE WHEN DAY(@FechaInicio) = 9 THEN @Codigo ELSE P.D9 END,
					P.D10 = CASE WHEN DAY(@FechaInicio) = 10 THEN @Codigo ELSE P.D10 END,
					P.D11 = CASE WHEN DAY(@FechaInicio) = 11 THEN @Codigo ELSE P.D11 END,
					P.D12 = CASE WHEN DAY(@FechaInicio) = 12 THEN @Codigo ELSE P.D12 END,
					P.D13 = CASE WHEN DAY(@FechaInicio) = 13 THEN @Codigo ELSE P.D13 END,
					P.D14 = CASE WHEN DAY(@FechaInicio) = 14 THEN @Codigo ELSE P.D14 END,
					P.D15 = CASE WHEN DAY(@FechaInicio) = 15 THEN @Codigo ELSE P.D15 END,
					P.D16 = CASE WHEN DAY(@FechaInicio) = 16 THEN @Codigo ELSE P.D16 END,
					P.D17 = CASE WHEN DAY(@FechaInicio) = 17 THEN @Codigo ELSE P.D17 END,
					P.D18 = CASE WHEN DAY(@FechaInicio) = 18 THEN @Codigo ELSE P.D18 END,
					P.D19 = CASE WHEN DAY(@FechaInicio) = 19 THEN @Codigo ELSE P.D19 END,
					P.D20 = CASE WHEN DAY(@FechaInicio) = 20 THEN @Codigo ELSE P.D20 END,
					P.D21 = CASE WHEN DAY(@FechaInicio) = 21 THEN @Codigo ELSE P.D21 END,
					P.D22 = CASE WHEN DAY(@FechaInicio) = 22 THEN @Codigo ELSE P.D22 END,
					P.D23 = CASE WHEN DAY(@FechaInicio) = 23 THEN @Codigo ELSE P.D23 END,
					P.D24 = CASE WHEN DAY(@FechaInicio) = 24 THEN @Codigo ELSE P.D24 END,
					P.D25 = CASE WHEN DAY(@FechaInicio) = 25 THEN @Codigo ELSE P.D25 END,
					P.D26 = CASE WHEN DAY(@FechaInicio) = 26 THEN @Codigo ELSE P.D26 END,
					P.D27 = CASE WHEN DAY(@FechaInicio) = 27 THEN @Codigo ELSE P.D27 END,
					P.D28 = CASE WHEN DAY(@FechaInicio) = 28 THEN @Codigo ELSE P.D28 END,
					P.D29 = CASE WHEN DAY(@FechaInicio) = 29 THEN @Codigo ELSE P.D29 END,
					P.D30 = CASE WHEN DAY(@FechaInicio) = 30 THEN @Codigo ELSE P.D30 END,
					P.D31 = CASE WHEN DAY(@FechaInicio) = 31 THEN @Codigo ELSE P.D31 END
				FROM ReportesApp_Operaciones_ProgramacionVC_PersonalView P
				WHERE P.IDPersona = @Persona AND P.Anio = YEAR(@FechaInicio) AND P.Mes = MONTH(@FechaInicio)

				SET @FechaInicio = DATEADD(DAY,1,@FechaInicio)
			END

			IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE FechaFin = FechaRetorno)) BEGIN
				DECLARE @FRetorno3 DATE = (SELECT TOP(1) FechaRetorno FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE FechaFin = FechaRetorno)
			
				UPDATE ReportesApp_Operaciones_ProgramacionVC_Registros
				SET FechaRetorno = DATEADD(DAY,1,FechaRetorno)
				WHERE IDPersona = @Persona AND FechaRetorno = @FRetorno3
			END

			IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE DATEPART(WEEKDAY, FechaRetorno) = 1)) BEGIN
				DECLARE @FRetorno4 DATE = (SELECT TOP(1) FechaRetorno FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE DATEPART(WEEKDAY, FechaRetorno) = 1)
			
				UPDATE ReportesApp_Operaciones_ProgramacionVC_Registros
				SET FechaRetorno = DATEADD(DAY,1,FechaRetorno)
				WHERE IDPersona = @Persona AND FechaRetorno = @FRetorno4
			END
		END
		ELSE BEGIN
			-- CONTAR DOMINGOS
			DECLARE @contador INT = 0;
			DECLARE @fecha_actual DATE = @FechaInicio;		
			DECLARE @total_domingos INT = 0;					

			WHILE (@fecha_actual <= @FechaFin) BEGIN
				IF DATEPART(WEEKDAY, @fecha_actual) = 1
					SET @total_domingos = @total_domingos + 1;

				SET @fecha_actual = DATEADD(DAY, 1, @fecha_actual); 
			END
		
			IF (@total_domingos > 0) BEGIN
				SET @FechaFin = DATEADD(DAY,@total_domingos,@FechaFin)
			END

			-- CONTAR FERIADOS
			DECLARE @FERIADO TABLE (FechaFeriado DATE)

			INSERT INTO @FERIADO(FechaFeriado)
			SELECT CONVERT(DATE, STUFF(STUFF(FechaMesDia, 3, 0, '/'), 6, 0, '/'))
			FROM PR_CalendarioFeriados
			WHERE CONVERT(DATE, STUFF(STUFF(FechaMesDia, 3, 0, '/'), 6, 0, '/')) BETWEEN @FechaInicio AND @FechaFin
			AND DATEPART(WEEKDAY, CONVERT(DATE, STUFF(STUFF(FechaMesDia, 3, 0, '/'), 6, 0, '/'))) != 1
		
			DECLARE @TotalFeriados INT = (SELECT COUNT(*) FROM @FERIADO)

			IF (@TotalFeriados > 0) BEGIN
				SET @FechaFin = DATEADD(DAY,@TotalFeriados,@FechaFin)
			END

			DECLARE @TEMP_COMP TABLE (Numero INT, IDPersona INT, FechaPendiente DATE)

			INSERT INTO @TEMP_COMP(Numero, IDPersona, FechaPendiente)
			SELECT TOP(DATEDIFF(DAY,@FechaInicio,@FechaFin) + 1 - @total_domingos - @TotalFeriados) ROW_NUMBER() OVER(ORDER BY D.IDPersona ASC), D.IDPersona, D.Fecha
			FROM ReportesApp_RRHH_Asistencia A
			INNER JOIN ReportesApp_RRHH_Asistencia_DescansoF D ON D.IDPersona=A.IDPersona AND D.Fecha=A.Fecha
			LEFT JOIN ReportesApp_RRHH_Asistencia_Compensaciones C ON C.IDPersona=A.IDPersona AND C.FechaTrabajada=A.Fecha
			LEFT JOIN PersonaMast P ON P.Persona = D.IDPersona
			INNER JOIN OP_TR_Conductor CO ON CO.IdPersona = P.Persona
			LEFT JOIN empleadomast E ON P.Persona = E.Empleado
			LEFT JOIN ReportesApp_Operaciones_ProgramacionVC_Registros VC ON (VC.IDPersona = D.IDPersona) AND (D.Fecha = VC.FechaIni)
			WHERE (A.IDTipoAsist = 32) AND (C.FechaCompensa IS NULL) AND (E.Estado = 'A') AND (CO.Estado = 'A') AND (VC.FechaFin IS NULL)
			AND D.IDPersona = @Persona
			ORDER BY D.Fecha ASC

			DECLARE @FirstProg INT = NULL
			DECLARE @LastProg  INT = NULL

			WHILE (@Nro <= (SELECT MAX(Numero) FROM @TEMP_COMP)) BEGIN
				DECLARE @FINI DATE = (SELECT FechaPendiente FROM @TEMP_COMP WHERE Numero = @Nro)

				-- BUSCAR SIGUIENTE DÍA HÁBIL (NO DOMINGO, NO FERIADO)
				WHILE DATEPART(WEEKDAY, @FechaInicio) = 1 OR EXISTS (SELECT 1 FROM PR_CalendarioFeriados CF WHERE
				CONVERT(DATE, STUFF(STUFF(CF.FechaMesDia, 3, 0, '/'), 6, 0, '/')) = @FechaInicio) BEGIN
					SET @FechaInicio = DATEADD(DAY, 1, @FechaInicio)
				END

				SET @co2 = (SELECT MAX(idProgVC) FROM ReportesApp_Operaciones_ProgramacionVC_Registros)
				SET @co2 = ISNULL(@co2,0) + 1

				IF @FirstProg IS NULL SET @FirstProg = @co2
				SET @LastProg = @co2

				DECLARE @Dia DATE = CONVERT(VARCHAR,YEAR(@FechaInicio))+'-'+CONVERT(VARCHAR,MONTH(@FechaInicio))+'-01'
				DECLARE @FechaFin2 DATE = CAST(DATEADD(MONTH,DATEDIFF(MONTH,0,@Dia)+1,0)-1 AS DATE)
				DECLARE @UltimoDiaMes TINYINT = DAY(@FechaFin2)

				DECLARE @Dia3 DATE = CONVERT(VARCHAR,YEAR(@FechaFin))+'-'+CONVERT(VARCHAR,MONTH(@FechaFin))+'-01'
				DECLARE @FechaFin4 DATE = CAST(DATEADD(MONTH,DATEDIFF(MONTH,0,@Dia3)+1,0)-1 AS DATE)
				DECLARE @UltimoDiaMes3 TINYINT = DAY(@FechaFin4)

				INSERT INTO ReportesApp_Operaciones_ProgramacionVC_Registros(idProgVC,IDPersona,Codigo,FRetorno,FechaIni,FechaFin,FechaRetorno,NumeroDias,Aprobado,UsuarioRegistra,FechaRegistra,UsuarioAprueba,FechaAprueba)
				SELECT @co2, @Persona, @Codigo, @FechaRetorno, @FINI, @FechaInicio, @FechaInicio, 0, 0, @Usuario, GETDATE(), @Usuario, GETDATE()

				IF (NOT EXISTS(SELECT * FROM ReportesApp_Operaciones_ProgramacionVC_PersonalView WHERE IDPersona = @Persona AND (Anio = YEAR(@FechaInicio) 
				AND Mes = MONTH(@FechaInicio)))) BEGIN
					INSERT INTO ReportesApp_Operaciones_ProgramacionVC_PersonalView (IDPersona,Anio,Mes,NroDias)
					SELECT @Persona, YEAR(@FechaInicio), MONTH(@FechaInicio), @UltimoDiaMes
					
					IF (@FechaFin > @FechaFin2 AND NOT EXISTS(SELECT * FROM ReportesApp_Operaciones_ProgramacionVC_PersonalView WHERE IDPersona = @Persona AND Anio = YEAR(@FechaFin)
					AND Mes = MONTH(@FechaFin))) BEGIN
						INSERT INTO ReportesApp_Operaciones_ProgramacionVC_PersonalView (IDPersona,Anio,Mes,NroDias)
						SELECT @Persona, YEAR(@FechaFin), MONTH(@FechaFin), @UltimoDiaMes3
					END
				END

				UPDATE P 
				SET P.D1 = CASE WHEN DAY(@FechaInicio) = 1 THEN @Codigo ELSE P.D1 END,
					P.D2 = CASE WHEN DAY(@FechaInicio) = 2 THEN @Codigo ELSE P.D2 END,
					P.D3 = CASE WHEN DAY(@FechaInicio) = 3 THEN @Codigo ELSE P.D3 END,
					P.D4 = CASE WHEN DAY(@FechaInicio) = 4 THEN @Codigo ELSE P.D4 END,
					P.D5 = CASE WHEN DAY(@FechaInicio) = 5 THEN @Codigo ELSE P.D5 END,
					P.D6 = CASE WHEN DAY(@FechaInicio) = 6 THEN @Codigo ELSE P.D6 END,
					P.D7 = CASE WHEN DAY(@FechaInicio) = 7 THEN @Codigo ELSE P.D7 END,
					P.D8 = CASE WHEN DAY(@FechaInicio) = 8 THEN @Codigo ELSE P.D8 END,
					P.D9 = CASE WHEN DAY(@FechaInicio) = 9 THEN @Codigo ELSE P.D9 END,
					P.D10 = CASE WHEN DAY(@FechaInicio) = 10 THEN @Codigo ELSE P.D10 END,
					P.D11 = CASE WHEN DAY(@FechaInicio) = 11 THEN @Codigo ELSE P.D11 END,
					P.D12 = CASE WHEN DAY(@FechaInicio) = 12 THEN @Codigo ELSE P.D12 END,
					P.D13 = CASE WHEN DAY(@FechaInicio) = 13 THEN @Codigo ELSE P.D13 END,
					P.D14 = CASE WHEN DAY(@FechaInicio) = 14 THEN @Codigo ELSE P.D14 END,
					P.D15 = CASE WHEN DAY(@FechaInicio) = 15 THEN @Codigo ELSE P.D15 END,
					P.D16 = CASE WHEN DAY(@FechaInicio) = 16 THEN @Codigo ELSE P.D16 END,
					P.D17 = CASE WHEN DAY(@FechaInicio) = 17 THEN @Codigo ELSE P.D17 END,
					P.D18 = CASE WHEN DAY(@FechaInicio) = 18 THEN @Codigo ELSE P.D18 END,
					P.D19 = CASE WHEN DAY(@FechaInicio) = 19 THEN @Codigo ELSE P.D19 END,
					P.D20 = CASE WHEN DAY(@FechaInicio) = 20 THEN @Codigo ELSE P.D20 END,
					P.D21 = CASE WHEN DAY(@FechaInicio) = 21 THEN @Codigo ELSE P.D21 END,
					P.D22 = CASE WHEN DAY(@FechaInicio) = 22 THEN @Codigo ELSE P.D22 END,
					P.D23 = CASE WHEN DAY(@FechaInicio) = 23 THEN @Codigo ELSE P.D23 END,
					P.D24 = CASE WHEN DAY(@FechaInicio) = 24 THEN @Codigo ELSE P.D24 END,
					P.D25 = CASE WHEN DAY(@FechaInicio) = 25 THEN @Codigo ELSE P.D25 END,
					P.D26 = CASE WHEN DAY(@FechaInicio) = 26 THEN @Codigo ELSE P.D26 END,
					P.D27 = CASE WHEN DAY(@FechaInicio) = 27 THEN @Codigo ELSE P.D27 END,
					P.D28 = CASE WHEN DAY(@FechaInicio) = 28 THEN @Codigo ELSE P.D28 END,
					P.D29 = CASE WHEN DAY(@FechaInicio) = 29 THEN @Codigo ELSE P.D29 END,
					P.D30 = CASE WHEN DAY(@FechaInicio) = 30 THEN @Codigo ELSE P.D30 END,
					P.D31 = CASE WHEN DAY(@FechaInicio) = 31 THEN @Codigo ELSE P.D31 END
				FROM ReportesApp_Operaciones_ProgramacionVC_PersonalView P
				WHERE P.IDPersona = @Persona AND P.Anio = YEAR(@FechaInicio) AND P.Mes = MONTH(@FechaInicio)

				SET @FechaInicio = DATEADD(DAY,1,@FechaInicio)
				SET @Nro = @Nro + 1
			END

			DECLARE @FechaRetornoReal DATE = @FechaInicio
			
			WHILE DATEPART(WEEKDAY, @FechaRetornoReal) = 1 OR EXISTS (SELECT 1 FROM PR_CalendarioFeriados CF
            WHERE CONVERT(DATE, STUFF(STUFF(CF.FechaMesDia, 3, 0, '/'), 6, 0, '/')) = @FechaRetornoReal) BEGIN
				SET @FechaRetornoReal = DATEADD(DAY,1,@FechaRetornoReal);
			END

			IF @FirstProg IS NOT NULL BEGIN
				UPDATE ReportesApp_Operaciones_ProgramacionVC_Registros
				SET FechaRetorno = @FechaRetornoReal
				WHERE IDPersona = @Persona AND idProgVC BETWEEN @FirstProg AND @LastProg
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

-------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 11-06-2025
-- Description:	LISTAR PROGRAMACIONES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ProgramacionVC_ListarProgramacionesVC]
@OpcionVC INT,
@Periodo VARCHAR(6),
@Conductor VARCHAR(250),
@Operacion VARCHAR(10)
AS
BEGIN
	IF (@OpcionVC = 1) BEGIN		-- LISTAR VACACIONES
		IF (@Operacion = 5) BEGIN
			SELECT R.idProgVC, R.IDPersona AS 'CODIGO', RTRIM(P.Documento) AS 'DOCUMENTO', RTRIM(LTRIM(P.NombreCompleto)) AS 'CONDUCTOR',
			CASE WHEN C.CodigoEnapu = '1' THEN 'TOLVAS'
				 WHEN C.CodigoEnapu = '2' THEN 'LINDLEY'
				 WHEN C.CodigoEnapu = '3' THEN 'LIMAGAS'
				 WHEN C.CodigoEnapu = '4' THEN 'GENERAL'
				 WHEN C.CodigoEnapu = '10' THEN 'VOLCAN'
				 ELSE 'SIN OPERACION' END AS 'OPERACION',
			CONVERT(VARCHAR(10),E.FechaIngreso,103) AS 'FECHA_INGRESO', 'VACACIONES' AS 'PROGRAMACION',
			CONVERT(VARCHAR(10),R.FechaIni,103) AS 'FECHA_INICIO', CONVERT(VARCHAR(10),R.FechaFin,103) AS 'FECHA_FIN', R.NumeroDias AS 'DÍAS',
			CONVERT(VARCHAR(10),R.FechaRetorno,103) AS 'FECHA_RETORNO', CASE WHEN VU.FechaInicio IS NOT NULL THEN 'SÍ' ELSE 'NO' END AS 'REGISTRADO',
			ISNULL(R.FRetorno,0) AS 'RETORNO', R.UsuarioRegistra, R.FechaRegistra, R.UsuarioAprueba,
			R.FechaAprueba FROM ReportesApp_Operaciones_ProgramacionVC_Registros R
			LEFT JOIN EmpleadoMast E ON E.Empleado = R.IDPersona
			INNER JOIN DBO.PersonaMast P ON P.Persona = E.Empleado 
			LEFT JOIN OP_TR_Conductor C ON C.IdPersona = P.Persona AND C.Estado='A'
			LEFT JOIN PR_VacacionUtilizacion VU ON VU.Empleado = R.IDPersona AND CONVERT(DATE,VU.FechaInicio) = CONVERT(DATE,R.FechaIni)
			WHERE (R.Codigo LIKE '%VA%') AND (C.Estado = 'A') AND (E.Estado = 'A') AND (YEAR(R.FechaIni) = RIGHT(@periodo,4))
			AND (MONTH(R.FechaIni) = LEFT(@periodo,2)) AND (@Conductor IS NULL OR LTRIM(RTRIM(P.NombreCompleto)) LIKE '%' + @Conductor + '%')
			ORDER BY R.FechaRegistra DESC, R.FechaIni DESC
		END
		ELSE BEGIN
			SELECT R.idProgVC, R.IDPersona AS 'CODIGO', RTRIM(P.Documento) AS 'DOCUMENTO', RTRIM(LTRIM(P.NombreCompleto)) AS 'CONDUCTOR',
			CASE WHEN C.CodigoEnapu = '1' THEN 'TOLVAS'
				 WHEN C.CodigoEnapu = '2' THEN 'LINDLEY'
				 WHEN C.CodigoEnapu = '3' THEN 'LIMAGAS'
				 WHEN C.CodigoEnapu = '4' THEN 'GENERAL'
				 WHEN C.CodigoEnapu = '10' THEN 'VOLCAN'
				 ELSE 'SIN OPERACION' END AS 'OPERACION', 
			CONVERT(VARCHAR(10),E.FechaIngreso,103) AS 'FECHA_INGRESO', 'VACACIONES' AS 'PROGRAMACION',
			CONVERT(VARCHAR(10),R.FechaIni,103) AS 'FECHA_INICIO', CONVERT(VARCHAR(10),R.FechaFin,103) AS 'FECHA_FIN', R.NumeroDias AS 'DÍAS',
			CONVERT(VARCHAR(10),R.FechaRetorno,103) AS 'FECHA_RETORNO', CASE WHEN VU.FechaInicio IS NOT NULL THEN 'SÍ' ELSE 'NO' END AS 'REGISTRADO',
			ISNULL(R.FRetorno,0) AS 'RETORNO', R.UsuarioRegistra, R.FechaRegistra, R.UsuarioAprueba,
			R.FechaAprueba FROM ReportesApp_Operaciones_ProgramacionVC_Registros R
			LEFT JOIN EmpleadoMast E ON E.Empleado = R.IDPersona
			INNER JOIN DBO.PersonaMast P ON P.Persona = E.Empleado 
			LEFT JOIN OP_TR_Conductor C ON C.IdPersona = P.Persona AND C.Estado='A'
			LEFT JOIN PR_VacacionUtilizacion VU ON VU.Empleado = R.IDPersona AND CONVERT(DATE,VU.FechaInicio) = CONVERT(DATE,R.FechaIni)
			WHERE (R.Codigo LIKE '%VA%') AND (C.Estado = 'A') AND (E.Estado = 'A') AND (YEAR(R.FechaIni) = RIGHT(@periodo,4))
			AND (MONTH(R.FechaIni) = LEFT(@periodo,2)) AND (@Conductor IS NULL OR LTRIM(RTRIM(P.NombreCompleto)) LIKE '%' + @Conductor + '%')
			AND (C.CodigoEnapu = @Operacion)
			ORDER BY R.FechaRegistra DESC, R.FechaIni DESC
		END
	END

	IF (@OpcionVC = 2) BEGIN		-- LISTAR COMPENSACIONES
		IF (@Operacion = 5) BEGIN
			SELECT R.idProgVC, R.IDPersona AS 'CODIGO', RTRIM(P.Documento) AS 'DOCUMENTO', RTRIM(LTRIM(P.NombreCompleto)) AS 'CONDUCTOR', 
			CASE WHEN C.CodigoEnapu = '1' THEN 'TOLVAS'
				 WHEN C.CodigoEnapu = '2' THEN 'LINDLEY'
				 WHEN C.CodigoEnapu = '3' THEN 'LIMAGAS'
				 WHEN C.CodigoEnapu = '4' THEN 'GENERAL'
				 WHEN C.CodigoEnapu = '10' THEN 'VOLCAN'
				 ELSE 'SIN OPERACION' END AS 'OPERACION', 
			CONVERT(VARCHAR(10),E.FechaIngreso,103) AS 'FECHA_INGRESO', 'COMPENSACIÓN' AS 'PROGRAMACION',
			DATENAME(WEEKDAY, R.FechaIni) AS 'DIA_PENDIENTE', CONVERT(VARCHAR(10),R.FechaIni,103) AS 'FECHA_PENDIENTE',
			CONVERT(VARCHAR(10),R.FechaFin,103) AS 'FECHA_COMPENSACION', CONVERT(VARCHAR(10),R.FechaRetorno,103) AS 'FECHA_RETORNO',
			ISNULL(R.FRetorno,0) AS 'RETORNO', CASE WHEN CO.FechaTrabajada IS NOT NULL THEN 'SÍ' ELSE 'NO' END AS 'REGISTRADO',
			R.UsuarioRegistra, R.FechaRegistra, R.UsuarioAprueba, R.FechaAprueba
			FROM ReportesApp_Operaciones_ProgramacionVC_Registros R
			LEFT JOIN EmpleadoMast E ON E.Empleado = R.IDPersona
			INNER JOIN DBO.PersonaMast P ON P.Persona = E.Empleado 
			LEFT JOIN OP_TR_Conductor C ON C.IdPersona = P.Persona AND C.Estado='A'
			LEFT JOIN ReportesApp_RRHH_Asistencia_Compensaciones CO ON CO.IDPersona = R.IDPersona AND CONVERT(DATE,CO.FechaTrabajada) = CONVERT(DATE,R.FechaIni) AND CONVERT(DATE,CO.FechaCompensa) = CONVERT(DATE,R.FechaFin)
			WHERE (R.Codigo LIKE '%CO%') AND (C.Estado = 'A') AND (E.Estado = 'A') AND (YEAR(R.FechaFin) = RIGHT(@periodo,4))
			AND (MONTH(R.FechaFin) = LEFT(@periodo,2)) AND (@Conductor IS NULL OR LTRIM(RTRIM(P.NombreCompleto)) LIKE '%' + @Conductor + '%')
			ORDER BY R.FechaRegistra DESC, R.FechaFin DESC
		END
		ELSE BEGIN
			SELECT R.idProgVC, R.IDPersona AS 'CODIGO', RTRIM(P.Documento) AS 'DOCUMENTO', RTRIM(LTRIM(P.NombreCompleto)) AS 'CONDUCTOR', 
			CASE WHEN C.CodigoEnapu = '1' THEN 'TOLVAS'
				 WHEN C.CodigoEnapu = '2' THEN 'LINDLEY'
				 WHEN C.CodigoEnapu = '3' THEN 'LIMAGAS'
				 WHEN C.CodigoEnapu = '4' THEN 'GENERAL'
				 WHEN C.CodigoEnapu = '10' THEN 'VOLCAN'
				 ELSE 'SIN OPERACION' END AS 'OPERACION', 
			CONVERT(VARCHAR(10),E.FechaIngreso,103) AS 'FECHA_INGRESO', 'COMPENSACIÓN' AS 'PROGRAMACION',
			DATENAME(WEEKDAY, R.FechaIni) AS 'DIA_PENDIENTE', CONVERT(VARCHAR(10),R.FechaIni,103) AS 'FECHA_PENDIENTE',
			CONVERT(VARCHAR(10),R.FechaFin,103) AS 'FECHA_COMPENSACION', CONVERT(VARCHAR(10),R.FechaRetorno,103) AS 'FECHA_RETORNO',
			CASE WHEN CO.FechaTrabajada IS NOT NULL THEN 'SÍ' ELSE 'NO' END AS 'REGISTRADO',
			ISNULL(R.FRetorno,0) AS 'RETORNO', R.UsuarioRegistra, R.FechaRegistra, R.UsuarioAprueba,
			R.FechaAprueba FROM ReportesApp_Operaciones_ProgramacionVC_Registros R
			LEFT JOIN EmpleadoMast E ON E.Empleado = R.IDPersona
			INNER JOIN DBO.PersonaMast P ON P.Persona = E.Empleado 
			LEFT JOIN OP_TR_Conductor C ON C.IdPersona = P.Persona AND C.Estado='A'
			LEFT JOIN ReportesApp_RRHH_Asistencia_Compensaciones CO ON CO.IDPersona = R.IDPersona AND CONVERT(DATE,CO.FechaTrabajada) = CONVERT(DATE,R.FechaIni) AND CONVERT(DATE,CO.FechaCompensa) = CONVERT(DATE,R.FechaFin)
			WHERE (R.Codigo LIKE '%CO%') AND (C.Estado = 'A') AND (E.Estado = 'A') AND (YEAR(R.FechaFin) = RIGHT(@periodo,4))
			AND (MONTH(R.FechaFin) = LEFT(@periodo,2)) AND (@Conductor IS NULL OR LTRIM(RTRIM(P.NombreCompleto)) LIKE '%' + @Conductor + '%')
			AND (C.CodigoEnapu = @Operacion)
			ORDER BY R.FechaRegistra DESC, R.FechaFin DESC
		END
	END

	IF (@OpcionVC = 3) BEGIN		-- LISTAR ASISTENCIAS
		IF (@Operacion = 5) BEGIN
			SELECT R.idProgVC, R.IDPersona AS 'CODIGO', RTRIM(P.Documento) AS 'DOCUMENTO', RTRIM(LTRIM(P.NombreCompleto)) AS 'CONDUCTOR',
			CASE WHEN C.CodigoEnapu = '1' THEN 'TOLVAS'
				 WHEN C.CodigoEnapu = '2' THEN 'LINDLEY'
				 WHEN C.CodigoEnapu = '3' THEN 'LIMAGAS'
				 WHEN C.CodigoEnapu = '4' THEN 'GENERAL'
				 WHEN C.CodigoEnapu = '10' THEN 'VOLCAN'
				 ELSE 'SIN OPERACION' END AS 'OPERACION',
			CONVERT(VARCHAR(10),E.FechaIngreso,103) AS 'FECHA_INGRESO', 'ASISTENCIA' AS 'PROGRAMACION',
			CONVERT(VARCHAR(10),R.FechaIni,103) AS 'FECHA_INICIO', CONVERT(VARCHAR(10),R.FechaFin,103) AS 'FECHA_FIN', R.NumeroDias AS 'DÍAS',
			ISNULL(R.FRetorno,0) AS 'RETORNO', R.UsuarioRegistra, R.FechaRegistra
			FROM ReportesApp_Operaciones_ProgramacionVC_Registros R
			LEFT JOIN EmpleadoMast E ON E.Empleado = R.IDPersona
			INNER JOIN DBO.PersonaMast P ON P.Persona = E.Empleado 
			LEFT JOIN OP_TR_Conductor C ON C.IdPersona = P.Persona AND C.Estado='A'
			WHERE (R.Codigo = 'A ') AND (C.Estado = 'A') AND (E.Estado = 'A') AND (YEAR(R.FechaIni) = RIGHT(@periodo,4))
			AND (MONTH(R.FechaIni) = LEFT(@periodo,2)) AND (@Conductor IS NULL OR LTRIM(RTRIM(P.NombreCompleto)) LIKE '%' + @Conductor + '%')
			ORDER BY R.FechaRegistra DESC, R.FechaIni DESC
		END
		ELSE BEGIN
			SELECT R.idProgVC, R.IDPersona AS 'CODIGO', RTRIM(P.Documento) AS 'DOCUMENTO', RTRIM(LTRIM(P.NombreCompleto)) AS 'CONDUCTOR',
			CASE WHEN C.CodigoEnapu = '1' THEN 'TOLVAS'
				 WHEN C.CodigoEnapu = '2' THEN 'LINDLEY'
				 WHEN C.CodigoEnapu = '3' THEN 'LIMAGAS'
				 WHEN C.CodigoEnapu = '4' THEN 'GENERAL'
				 WHEN C.CodigoEnapu = '10' THEN 'VOLCAN'
				 ELSE 'SIN OPERACION' END AS 'OPERACION',
			CONVERT(VARCHAR(10),E.FechaIngreso,103) AS 'FECHA_INGRESO', 'ASISTENCIA' AS 'PROGRAMACION',
			CONVERT(VARCHAR(10),R.FechaIni,103) AS 'FECHA_INICIO', CONVERT(VARCHAR(10),R.FechaFin,103) AS 'FECHA_FIN', R.NumeroDias AS 'DÍAS',
			ISNULL(R.FRetorno,0) AS 'RETORNO', R.UsuarioRegistra, R.FechaRegistra
			FROM ReportesApp_Operaciones_ProgramacionVC_Registros R
			LEFT JOIN EmpleadoMast E ON E.Empleado = R.IDPersona
			INNER JOIN DBO.PersonaMast P ON P.Persona = E.Empleado 
			LEFT JOIN OP_TR_Conductor C ON C.IdPersona = P.Persona AND C.Estado='A'
			WHERE (R.Codigo = 'A ') AND (C.Estado = 'A') AND (E.Estado = 'A') AND (YEAR(R.FechaIni) = RIGHT(@periodo,4))
			AND (MONTH(R.FechaIni) = LEFT(@periodo,2)) AND (@Conductor IS NULL OR LTRIM(RTRIM(P.NombreCompleto)) LIKE '%' + @Conductor + '%')
			AND (C.CodigoEnapu = @Operacion)
			ORDER BY R.FechaRegistra DESC, R.FechaIni DESC
		END
	END
END

-------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 11/06/2024
-- Description:	ELIMINAR PROGRAMACIONES VC
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ProgramacionVC_EliminarProgramacionesVC]
@OpcionVC INT,
@Periodo VARCHAR(6),
@idProgVC INT,
@IDPersona INT,
@FechaRetorno DATE
AS
DECLARE @exito VARCHAR(MAX)

SET @exito = '0 = Programación de Conductor Eliminada.'

BEGIN TRAN
BEGIN TRY
	IF (@OpcionVC = 1) BEGIN
		DECLARE @FechaIni DATE = (SELECT FechaIni FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE idProgVC = @idProgVC)
		DECLARE @FechaFin DATE = (SELECT FechaFin FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE idProgVC = @idProgVC)

		DECLARE @Dia DATE = CONVERT(VARCHAR,YEAR(@FechaIni))+'-'+CONVERT(VARCHAR,MONTH(@FechaIni))+'-01'
		DECLARE @FechaFinV DATE = CAST(DATEADD(MONTH,DATEDIFF(MONTH,0,@Dia)+1,0)-1 AS DATE)
		DECLARE @UltimoDiaMes TINYINT = DAY(@FechaFinV)

		DECLARE @Dia2 DATE = CONVERT(VARCHAR,YEAR(@FechaFin))+'-'+CONVERT(VARCHAR,MONTH(@FechaFin))+'-01'
		DECLARE @FechaFinV2 DATE = CAST(DATEADD(MONTH,DATEDIFF(MONTH,0,@Dia2)+1,0)-1 AS DATE)
		DECLARE @UltimoDiaMes2 TINYINT = DAY(@FechaFinV2)

		WHILE (@FechaIni <= @FechaFin) BEGIN
			UPDATE P 
			SET P.D1 = CASE WHEN DAY(@FechaIni) = 1 THEN NULL ELSE P.D1 END,
				P.D2 = CASE WHEN DAY(@FechaIni) = 2 THEN NULL ELSE P.D2 END,
				P.D3 = CASE WHEN DAY(@FechaIni) = 3 THEN NULL ELSE P.D3 END,
				P.D4 = CASE WHEN DAY(@FechaIni) = 4 THEN NULL ELSE P.D4 END,
				P.D5 = CASE WHEN DAY(@FechaIni) = 5 THEN NULL ELSE P.D5 END,
				P.D6 = CASE WHEN DAY(@FechaIni) = 6 THEN NULL ELSE P.D6 END,
				P.D7 = CASE WHEN DAY(@FechaIni) = 7 THEN NULL ELSE P.D7 END,
				P.D8 = CASE WHEN DAY(@FechaIni) = 8 THEN NULL ELSE P.D8 END,
				P.D9 = CASE WHEN DAY(@FechaIni) = 9 THEN NULL ELSE P.D9 END,
				P.D10 = CASE WHEN DAY(@FechaIni) = 10 THEN NULL ELSE P.D10 END,
				P.D11 = CASE WHEN DAY(@FechaIni) = 11 THEN NULL ELSE P.D11 END,
				P.D12 = CASE WHEN DAY(@FechaIni) = 12 THEN NULL ELSE P.D12 END,
				P.D13 = CASE WHEN DAY(@FechaIni) = 13 THEN NULL ELSE P.D13 END,
				P.D14 = CASE WHEN DAY(@FechaIni) = 14 THEN NULL ELSE P.D14 END,
				P.D15 = CASE WHEN DAY(@FechaIni) = 15 THEN NULL ELSE P.D15 END,
				P.D16 = CASE WHEN DAY(@FechaIni) = 16 THEN NULL ELSE P.D16 END,
				P.D17 = CASE WHEN DAY(@FechaIni) = 17 THEN NULL ELSE P.D17 END,
				P.D18 = CASE WHEN DAY(@FechaIni) = 18 THEN NULL ELSE P.D18 END,
				P.D19 = CASE WHEN DAY(@FechaIni) = 19 THEN NULL ELSE P.D19 END,
				P.D20 = CASE WHEN DAY(@FechaIni) = 20 THEN NULL ELSE P.D20 END,
				P.D21 = CASE WHEN DAY(@FechaIni) = 21 THEN NULL ELSE P.D21 END,
				P.D22 = CASE WHEN DAY(@FechaIni) = 22 THEN NULL ELSE P.D22 END,
				P.D23 = CASE WHEN DAY(@FechaIni) = 23 THEN NULL ELSE P.D23 END,
				P.D24 = CASE WHEN DAY(@FechaIni) = 24 THEN NULL ELSE P.D24 END,
				P.D25 = CASE WHEN DAY(@FechaIni) = 25 THEN NULL ELSE P.D25 END,
				P.D26 = CASE WHEN DAY(@FechaIni) = 26 THEN NULL ELSE P.D26 END,
				P.D27 = CASE WHEN DAY(@FechaIni) = 27 THEN NULL ELSE P.D27 END,
				P.D28 = CASE WHEN DAY(@FechaIni) = 28 THEN NULL ELSE P.D28 END,
				P.D29 = CASE WHEN DAY(@FechaIni) = 29 THEN NULL ELSE P.D29 END,
				P.D30 = CASE WHEN DAY(@FechaIni) = 30 THEN NULL ELSE P.D30 END,
				P.D31 = CASE WHEN DAY(@FechaIni) = 31 THEN NULL ELSE P.D31 END
			FROM ReportesApp_Operaciones_ProgramacionVC_PersonalView P
			WHERE P.IDPersona = @IDPersona AND P.Anio = YEAR(@FechaIni) AND P.Mes = MONTH(@FechaIni)

			IF (@FechaIni > @FechaFinV) BEGIN
				UPDATE P 
				SET P.D1 = CASE WHEN DAY(@FechaIni) = 1 THEN NULL ELSE P.D1 END,
					P.D2 = CASE WHEN DAY(@FechaIni) = 2 THEN NULL ELSE P.D2 END,
					P.D3 = CASE WHEN DAY(@FechaIni) = 3 THEN NULL ELSE P.D3 END,
					P.D4 = CASE WHEN DAY(@FechaIni) = 4 THEN NULL ELSE P.D4 END,
					P.D5 = CASE WHEN DAY(@FechaIni) = 5 THEN NULL ELSE P.D5 END,
					P.D6 = CASE WHEN DAY(@FechaIni) = 6 THEN NULL ELSE P.D6 END,
					P.D7 = CASE WHEN DAY(@FechaIni) = 7 THEN NULL ELSE P.D7 END,
					P.D8 = CASE WHEN DAY(@FechaIni) = 8 THEN NULL ELSE P.D8 END,
					P.D9 = CASE WHEN DAY(@FechaIni) = 9 THEN NULL ELSE P.D9 END,
					P.D10 = CASE WHEN DAY(@FechaIni) = 10 THEN NULL ELSE P.D10 END,
					P.D11 = CASE WHEN DAY(@FechaIni) = 11 THEN NULL ELSE P.D11 END,
					P.D12 = CASE WHEN DAY(@FechaIni) = 12 THEN NULL ELSE P.D12 END,
					P.D13 = CASE WHEN DAY(@FechaIni) = 13 THEN NULL ELSE P.D13 END,
					P.D14 = CASE WHEN DAY(@FechaIni) = 14 THEN NULL ELSE P.D14 END,
					P.D15 = CASE WHEN DAY(@FechaIni) = 15 THEN NULL ELSE P.D15 END,
					P.D16 = CASE WHEN DAY(@FechaIni) = 16 THEN NULL ELSE P.D16 END,
					P.D17 = CASE WHEN DAY(@FechaIni) = 17 THEN NULL ELSE P.D17 END,
					P.D18 = CASE WHEN DAY(@FechaIni) = 18 THEN NULL ELSE P.D18 END,
					P.D19 = CASE WHEN DAY(@FechaIni) = 19 THEN NULL ELSE P.D19 END,
					P.D20 = CASE WHEN DAY(@FechaIni) = 20 THEN NULL ELSE P.D20 END,
					P.D21 = CASE WHEN DAY(@FechaIni) = 21 THEN NULL ELSE P.D21 END,
					P.D22 = CASE WHEN DAY(@FechaIni) = 22 THEN NULL ELSE P.D22 END,
					P.D23 = CASE WHEN DAY(@FechaIni) = 23 THEN NULL ELSE P.D23 END,
					P.D24 = CASE WHEN DAY(@FechaIni) = 24 THEN NULL ELSE P.D24 END,
					P.D25 = CASE WHEN DAY(@FechaIni) = 25 THEN NULL ELSE P.D25 END,
					P.D26 = CASE WHEN DAY(@FechaIni) = 26 THEN NULL ELSE P.D26 END,
					P.D27 = CASE WHEN DAY(@FechaIni) = 27 THEN NULL ELSE P.D27 END,
					P.D28 = CASE WHEN DAY(@FechaIni) = 28 THEN NULL ELSE P.D28 END,
					P.D29 = CASE WHEN DAY(@FechaIni) = 29 THEN NULL ELSE P.D29 END,
					P.D30 = CASE WHEN DAY(@FechaIni) = 30 THEN NULL ELSE P.D30 END,
					P.D31 = CASE WHEN DAY(@FechaIni) = 31 THEN NULL ELSE P.D31 END
				FROM ReportesApp_Operaciones_ProgramacionVC_PersonalView P
				WHERE P.IDPersona = @IDPersona AND P.Anio = YEAR(@FechaFin) AND P.Mes = MONTH(@FechaFin)
			END

			SET @FechaIni = DATEADD(DAY,1,@FechaIni)
		END

		DELETE FROM ReportesApp_Operaciones_ProgramacionVC_Registros
		WHERE idProgVC = @idProgVC
	END

	IF (@OpcionVC = 2) BEGIN
		DECLARE @ELIMINAR TABLE (Numero INT, IDPersona INT, FechaFin DATE)
		
		INSERT INTO @ELIMINAR(Numero, IDPersona, FechaFin)
		SELECT ROW_NUMBER() OVER(ORDER BY idProgVC ASC), IDPersona, FechaFin
		FROM ReportesApp_Operaciones_ProgramacionVC_Registros
		WHERE FechaRetorno = @FechaRetorno AND IDPersona = @IDPersona
		ORDER BY FechaFin ASC

		DECLARE @Nro INT = 1
		WHILE (@Nro <= (SELECT MAX(Numero) FROM @ELIMINAR)) BEGIN
			DECLARE @FINI DATE = (SELECT FechaFin FROM @ELIMINAR WHERE Numero = @Nro)

			UPDATE P 
			SET P.D1 = CASE WHEN DAY(@FINI) = 1 THEN NULL ELSE P.D1 END,
				P.D2 = CASE WHEN DAY(@FINI) = 2 THEN NULL ELSE P.D2 END,
				P.D3 = CASE WHEN DAY(@FINI) = 3 THEN NULL ELSE P.D3 END,
				P.D4 = CASE WHEN DAY(@FINI) = 4 THEN NULL ELSE P.D4 END,
				P.D5 = CASE WHEN DAY(@FINI) = 5 THEN NULL ELSE P.D5 END,
				P.D6 = CASE WHEN DAY(@FINI) = 6 THEN NULL ELSE P.D6 END,
				P.D7 = CASE WHEN DAY(@FINI) = 7 THEN NULL ELSE P.D7 END,
				P.D8 = CASE WHEN DAY(@FINI) = 8 THEN NULL ELSE P.D8 END,
				P.D9 = CASE WHEN DAY(@FINI) = 9 THEN NULL ELSE P.D9 END,
				P.D10 = CASE WHEN DAY(@FINI) = 10 THEN NULL ELSE P.D10 END,
				P.D11 = CASE WHEN DAY(@FINI) = 11 THEN NULL ELSE P.D11 END,
				P.D12 = CASE WHEN DAY(@FINI) = 12 THEN NULL ELSE P.D12 END,
				P.D13 = CASE WHEN DAY(@FINI) = 13 THEN NULL ELSE P.D13 END,
				P.D14 = CASE WHEN DAY(@FINI) = 14 THEN NULL ELSE P.D14 END,
				P.D15 = CASE WHEN DAY(@FINI) = 15 THEN NULL ELSE P.D15 END,
				P.D16 = CASE WHEN DAY(@FINI) = 16 THEN NULL ELSE P.D16 END,
				P.D17 = CASE WHEN DAY(@FINI) = 17 THEN NULL ELSE P.D17 END,
				P.D18 = CASE WHEN DAY(@FINI) = 18 THEN NULL ELSE P.D18 END,
				P.D19 = CASE WHEN DAY(@FINI) = 19 THEN NULL ELSE P.D19 END,
				P.D20 = CASE WHEN DAY(@FINI) = 20 THEN NULL ELSE P.D20 END,
				P.D21 = CASE WHEN DAY(@FINI) = 21 THEN NULL ELSE P.D21 END,
				P.D22 = CASE WHEN DAY(@FINI) = 22 THEN NULL ELSE P.D22 END,
				P.D23 = CASE WHEN DAY(@FINI) = 23 THEN NULL ELSE P.D23 END,
				P.D24 = CASE WHEN DAY(@FINI) = 24 THEN NULL ELSE P.D24 END,
				P.D25 = CASE WHEN DAY(@FINI) = 25 THEN NULL ELSE P.D25 END,
				P.D26 = CASE WHEN DAY(@FINI) = 26 THEN NULL ELSE P.D26 END,
				P.D27 = CASE WHEN DAY(@FINI) = 27 THEN NULL ELSE P.D27 END,
				P.D28 = CASE WHEN DAY(@FINI) = 28 THEN NULL ELSE P.D28 END,
				P.D29 = CASE WHEN DAY(@FINI) = 29 THEN NULL ELSE P.D29 END,
				P.D30 = CASE WHEN DAY(@FINI) = 30 THEN NULL ELSE P.D30 END,
				P.D31 = CASE WHEN DAY(@FINI) = 31 THEN NULL ELSE P.D31 END
			FROM ReportesApp_Operaciones_ProgramacionVC_PersonalView P
			WHERE P.IDPersona = @IDPersona AND P.Anio = YEAR(@FINI) AND P.Mes = MONTH(@FINI)

			SET @Nro = @Nro + 1
		END

		DELETE FROM ReportesApp_Operaciones_ProgramacionVC_Registros
		WHERE FechaRetorno = @FechaRetorno AND IDPersona = @IDPersona
	END

	IF (@OpcionVC = 3) BEGIN
		DECLARE @FechaIni2 DATE = (SELECT FechaIni FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE idProgVC = @idProgVC)
		DECLARE @FechaFin2 DATE = (SELECT FechaFin FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE idProgVC = @idProgVC)

		DECLARE @Dia3 DATE = CONVERT(VARCHAR,YEAR(@FechaIni2))+'-'+CONVERT(VARCHAR,MONTH(@FechaIni2))+'-01'
		DECLARE @FechaFinV3 DATE = CAST(DATEADD(MONTH,DATEDIFF(MONTH,0,@Dia3)+1,0)-1 AS DATE)
		DECLARE @UltimoDiaMes3 TINYINT = DAY(@FechaFinV3)

		DECLARE @Dia4 DATE = CONVERT(VARCHAR,YEAR(@FechaFin2))+'-'+CONVERT(VARCHAR,MONTH(@FechaFin2))+'-01'
		DECLARE @FechaFinV4 DATE = CAST(DATEADD(MONTH,DATEDIFF(MONTH,0,@Dia4)+1,0)-1 AS DATE)
		DECLARE @UltimoDiaMes4 TINYINT = DAY(@FechaFinV4)

		WHILE (@FechaIni2 <= @FechaFin2) BEGIN
			UPDATE P 
			SET P.D1 = CASE WHEN DAY(@FechaIni2) = 1 THEN NULL ELSE P.D1 END,
				P.D2 = CASE WHEN DAY(@FechaIni2) = 2 THEN NULL ELSE P.D2 END,
				P.D3 = CASE WHEN DAY(@FechaIni2) = 3 THEN NULL ELSE P.D3 END,
				P.D4 = CASE WHEN DAY(@FechaIni2) = 4 THEN NULL ELSE P.D4 END,
				P.D5 = CASE WHEN DAY(@FechaIni2) = 5 THEN NULL ELSE P.D5 END,
				P.D6 = CASE WHEN DAY(@FechaIni2) = 6 THEN NULL ELSE P.D6 END,
				P.D7 = CASE WHEN DAY(@FechaIni2) = 7 THEN NULL ELSE P.D7 END,
				P.D8 = CASE WHEN DAY(@FechaIni2) = 8 THEN NULL ELSE P.D8 END,
				P.D9 = CASE WHEN DAY(@FechaIni2) = 9 THEN NULL ELSE P.D9 END,
				P.D10 = CASE WHEN DAY(@FechaIni2) = 10 THEN NULL ELSE P.D10 END,
				P.D11 = CASE WHEN DAY(@FechaIni2) = 11 THEN NULL ELSE P.D11 END,
				P.D12 = CASE WHEN DAY(@FechaIni2) = 12 THEN NULL ELSE P.D12 END,
				P.D13 = CASE WHEN DAY(@FechaIni2) = 13 THEN NULL ELSE P.D13 END,
				P.D14 = CASE WHEN DAY(@FechaIni2) = 14 THEN NULL ELSE P.D14 END,
				P.D15 = CASE WHEN DAY(@FechaIni2) = 15 THEN NULL ELSE P.D15 END,
				P.D16 = CASE WHEN DAY(@FechaIni2) = 16 THEN NULL ELSE P.D16 END,
				P.D17 = CASE WHEN DAY(@FechaIni2) = 17 THEN NULL ELSE P.D17 END,
				P.D18 = CASE WHEN DAY(@FechaIni2) = 18 THEN NULL ELSE P.D18 END,
				P.D19 = CASE WHEN DAY(@FechaIni2) = 19 THEN NULL ELSE P.D19 END,
				P.D20 = CASE WHEN DAY(@FechaIni2) = 20 THEN NULL ELSE P.D20 END,
				P.D21 = CASE WHEN DAY(@FechaIni2) = 21 THEN NULL ELSE P.D21 END,
				P.D22 = CASE WHEN DAY(@FechaIni2) = 22 THEN NULL ELSE P.D22 END,
				P.D23 = CASE WHEN DAY(@FechaIni2) = 23 THEN NULL ELSE P.D23 END,
				P.D24 = CASE WHEN DAY(@FechaIni2) = 24 THEN NULL ELSE P.D24 END,
				P.D25 = CASE WHEN DAY(@FechaIni2) = 25 THEN NULL ELSE P.D25 END,
				P.D26 = CASE WHEN DAY(@FechaIni2) = 26 THEN NULL ELSE P.D26 END,
				P.D27 = CASE WHEN DAY(@FechaIni2) = 27 THEN NULL ELSE P.D27 END,
				P.D28 = CASE WHEN DAY(@FechaIni2) = 28 THEN NULL ELSE P.D28 END,
				P.D29 = CASE WHEN DAY(@FechaIni2) = 29 THEN NULL ELSE P.D29 END,
				P.D30 = CASE WHEN DAY(@FechaIni2) = 30 THEN NULL ELSE P.D30 END,
				P.D31 = CASE WHEN DAY(@FechaIni2) = 31 THEN NULL ELSE P.D31 END
			FROM ReportesApp_Operaciones_ProgramacionVC_PersonalView P
			WHERE P.IDPersona = @IDPersona AND P.Anio = YEAR(@FechaIni2) AND P.Mes = MONTH(@FechaIni2)

			IF (@FechaIni2 > @FechaFinV3) BEGIN
				UPDATE P 
				SET P.D1 = CASE WHEN DAY(@FechaIni2) = 1 THEN NULL ELSE P.D1 END,
					P.D2 = CASE WHEN DAY(@FechaIni2) = 2 THEN NULL ELSE P.D2 END,
					P.D3 = CASE WHEN DAY(@FechaIni2) = 3 THEN NULL ELSE P.D3 END,
					P.D4 = CASE WHEN DAY(@FechaIni2) = 4 THEN NULL ELSE P.D4 END,
					P.D5 = CASE WHEN DAY(@FechaIni2) = 5 THEN NULL ELSE P.D5 END,
					P.D6 = CASE WHEN DAY(@FechaIni2) = 6 THEN NULL ELSE P.D6 END,
					P.D7 = CASE WHEN DAY(@FechaIni2) = 7 THEN NULL ELSE P.D7 END,
					P.D8 = CASE WHEN DAY(@FechaIni2) = 8 THEN NULL ELSE P.D8 END,
					P.D9 = CASE WHEN DAY(@FechaIni2) = 9 THEN NULL ELSE P.D9 END,
					P.D10 = CASE WHEN DAY(@FechaIni2) = 10 THEN NULL ELSE P.D10 END,
					P.D11 = CASE WHEN DAY(@FechaIni2) = 11 THEN NULL ELSE P.D11 END,
					P.D12 = CASE WHEN DAY(@FechaIni2) = 12 THEN NULL ELSE P.D12 END,
					P.D13 = CASE WHEN DAY(@FechaIni2) = 13 THEN NULL ELSE P.D13 END,
					P.D14 = CASE WHEN DAY(@FechaIni2) = 14 THEN NULL ELSE P.D14 END,
					P.D15 = CASE WHEN DAY(@FechaIni2) = 15 THEN NULL ELSE P.D15 END,
					P.D16 = CASE WHEN DAY(@FechaIni2) = 16 THEN NULL ELSE P.D16 END,
					P.D17 = CASE WHEN DAY(@FechaIni2) = 17 THEN NULL ELSE P.D17 END,
					P.D18 = CASE WHEN DAY(@FechaIni2) = 18 THEN NULL ELSE P.D18 END,
					P.D19 = CASE WHEN DAY(@FechaIni2) = 19 THEN NULL ELSE P.D19 END,
					P.D20 = CASE WHEN DAY(@FechaIni2) = 20 THEN NULL ELSE P.D20 END,
					P.D21 = CASE WHEN DAY(@FechaIni2) = 21 THEN NULL ELSE P.D21 END,
					P.D22 = CASE WHEN DAY(@FechaIni2) = 22 THEN NULL ELSE P.D22 END,
					P.D23 = CASE WHEN DAY(@FechaIni2) = 23 THEN NULL ELSE P.D23 END,
					P.D24 = CASE WHEN DAY(@FechaIni2) = 24 THEN NULL ELSE P.D24 END,
					P.D25 = CASE WHEN DAY(@FechaIni2) = 25 THEN NULL ELSE P.D25 END,
					P.D26 = CASE WHEN DAY(@FechaIni2) = 26 THEN NULL ELSE P.D26 END,
					P.D27 = CASE WHEN DAY(@FechaIni2) = 27 THEN NULL ELSE P.D27 END,
					P.D28 = CASE WHEN DAY(@FechaIni2) = 28 THEN NULL ELSE P.D28 END,
					P.D29 = CASE WHEN DAY(@FechaIni2) = 29 THEN NULL ELSE P.D29 END,
					P.D30 = CASE WHEN DAY(@FechaIni2) = 30 THEN NULL ELSE P.D30 END,
					P.D31 = CASE WHEN DAY(@FechaIni2) = 31 THEN NULL ELSE P.D31 END
				FROM ReportesApp_Operaciones_ProgramacionVC_PersonalView P
				WHERE P.IDPersona = @IDPersona AND P.Anio = YEAR(@FechaFin2) AND P.Mes = MONTH(@FechaFin2)
			END

			SET @FechaIni2 = DATEADD(DAY,1,@FechaIni2)
		END

		DELETE FROM ReportesApp_Operaciones_ProgramacionVC_Registros
		WHERE idProgVC = @idProgVC
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

-------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 12/06/2024
-- Description:	APROBAR PROGRAMACIONES VC
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ProgramacionVC_AprobarProgramacionesVC]
@OpcionVC INT,
@Periodo VARCHAR(6),
@idProgVC INT,
@IDPersona INT,
@FechaRetorno DATE,
@Usuario VARCHAR(20)
AS
DECLARE @exito VARCHAR(MAX)

SET @exito = '0 = Programación de Conductor Aprobada.'

BEGIN TRAN
BEGIN TRY
	DECLARE @EstadoVC INT = (SELECT Aprobado FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE idProgVC = @idProgVC)

	IF (@OpcionVC = 1) BEGIN
		IF (@Usuario NOT IN ('LLEYTHON','KRAMIREZ','GREYES') AND @EstadoVC = 0) BEGIN
			SET @Exito = '-1 = Las vacaciones solo pueden ser aprobadas por la jefatura del área de Operaciones.'
			ROLLBACK
			GOTO Terminar
		END
		ELSE BEGIN
			DECLARE @FechaIni DATE = (SELECT FechaIni FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE idProgVC = @idProgVC)
			DECLARE @FechaFin DATE = (SELECT FechaFin FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE idProgVC = @idProgVC)

			DECLARE @Dia DATE = CONVERT(VARCHAR,YEAR(@FechaIni))+'-'+CONVERT(VARCHAR,MONTH(@FechaIni))+'-01'
			DECLARE @FechaFinV DATE = CAST(DATEADD(MONTH,DATEDIFF(MONTH,0,@Dia)+1,0)-1 AS DATE)
			DECLARE @UltimoDiaMes TINYINT = DAY(@FechaFinV)

			DECLARE @Dia2 DATE = CONVERT(VARCHAR,YEAR(@FechaFin))+'-'+CONVERT(VARCHAR,MONTH(@FechaFin))+'-01'
			DECLARE @FechaFinV2 DATE = CAST(DATEADD(MONTH,DATEDIFF(MONTH,0,@Dia2)+1,0)-1 AS DATE)
			DECLARE @UltimoDiaMes2 TINYINT = DAY(@FechaFinV2)

			IF (@EstadoVC = 0) BEGIN 
				UPDATE ReportesApp_Operaciones_ProgramacionVC_Registros
				SET Codigo = 'VA (AP)', Aprobado = 1, UsuarioAprueba = @Usuario, FechaAprueba = GETDATE()
				WHERE idProgVC = @idProgVC
			END
			ELSE BEGIN
				UPDATE ReportesApp_Operaciones_ProgramacionVC_Registros
				SET Codigo = 'VA (AP)'
				WHERE idProgVC = @idProgVC
			END

			WHILE (@FechaIni <= @FechaFin) BEGIN
				UPDATE P 
				SET P.D1 = CASE WHEN DAY(@FechaIni) = 1 THEN 'VA (AP)' ELSE P.D1 END,
					P.D2 = CASE WHEN DAY(@FechaIni) = 2 THEN 'VA (AP)' ELSE P.D2 END,
					P.D3 = CASE WHEN DAY(@FechaIni) = 3 THEN 'VA (AP)' ELSE P.D3 END,
					P.D4 = CASE WHEN DAY(@FechaIni) = 4 THEN 'VA (AP)' ELSE P.D4 END,
					P.D5 = CASE WHEN DAY(@FechaIni) = 5 THEN 'VA (AP)' ELSE P.D5 END,
					P.D6 = CASE WHEN DAY(@FechaIni) = 6 THEN 'VA (AP)' ELSE P.D6 END,
					P.D7 = CASE WHEN DAY(@FechaIni) = 7 THEN 'VA (AP)' ELSE P.D7 END,
					P.D8 = CASE WHEN DAY(@FechaIni) = 8 THEN 'VA (AP)' ELSE P.D8 END,
					P.D9 = CASE WHEN DAY(@FechaIni) = 9 THEN 'VA (AP)' ELSE P.D9 END,
					P.D10 = CASE WHEN DAY(@FechaIni) = 10 THEN 'VA (AP)' ELSE P.D10 END,
					P.D11 = CASE WHEN DAY(@FechaIni) = 11 THEN 'VA (AP)' ELSE P.D11 END,
					P.D12 = CASE WHEN DAY(@FechaIni) = 12 THEN 'VA (AP)' ELSE P.D12 END,
					P.D13 = CASE WHEN DAY(@FechaIni) = 13 THEN 'VA (AP)' ELSE P.D13 END,
					P.D14 = CASE WHEN DAY(@FechaIni) = 14 THEN 'VA (AP)' ELSE P.D14 END,
					P.D15 = CASE WHEN DAY(@FechaIni) = 15 THEN 'VA (AP)' ELSE P.D15 END,
					P.D16 = CASE WHEN DAY(@FechaIni) = 16 THEN 'VA (AP)' ELSE P.D16 END,
					P.D17 = CASE WHEN DAY(@FechaIni) = 17 THEN 'VA (AP)' ELSE P.D17 END,
					P.D18 = CASE WHEN DAY(@FechaIni) = 18 THEN 'VA (AP)' ELSE P.D18 END,
					P.D19 = CASE WHEN DAY(@FechaIni) = 19 THEN 'VA (AP)' ELSE P.D19 END,
					P.D20 = CASE WHEN DAY(@FechaIni) = 20 THEN 'VA (AP)' ELSE P.D20 END,
					P.D21 = CASE WHEN DAY(@FechaIni) = 21 THEN 'VA (AP)' ELSE P.D21 END,
					P.D22 = CASE WHEN DAY(@FechaIni) = 22 THEN 'VA (AP)' ELSE P.D22 END,
					P.D23 = CASE WHEN DAY(@FechaIni) = 23 THEN 'VA (AP)' ELSE P.D23 END,
					P.D24 = CASE WHEN DAY(@FechaIni) = 24 THEN 'VA (AP)' ELSE P.D24 END,
					P.D25 = CASE WHEN DAY(@FechaIni) = 25 THEN 'VA (AP)' ELSE P.D25 END,
					P.D26 = CASE WHEN DAY(@FechaIni) = 26 THEN 'VA (AP)' ELSE P.D26 END,
					P.D27 = CASE WHEN DAY(@FechaIni) = 27 THEN 'VA (AP)' ELSE P.D27 END,
					P.D28 = CASE WHEN DAY(@FechaIni) = 28 THEN 'VA (AP)' ELSE P.D28 END,
					P.D29 = CASE WHEN DAY(@FechaIni) = 29 THEN 'VA (AP)' ELSE P.D29 END,
					P.D30 = CASE WHEN DAY(@FechaIni) = 30 THEN 'VA (AP)' ELSE P.D30 END,
					P.D31 = CASE WHEN DAY(@FechaIni) = 31 THEN 'VA (AP)' ELSE P.D31 END
				FROM ReportesApp_Operaciones_ProgramacionVC_PersonalView P
				WHERE P.IDPersona = @IDPersona AND P.Anio = YEAR(@FechaIni) AND P.Mes = MONTH(@FechaIni)

				IF (@FechaIni > @FechaFinV) BEGIN
					UPDATE P 
					SET P.D1 = CASE WHEN DAY(@FechaIni) = 1 THEN 'VA (AP)' ELSE P.D1 END,
						P.D2 = CASE WHEN DAY(@FechaIni) = 2 THEN 'VA (AP)' ELSE P.D2 END,
						P.D3 = CASE WHEN DAY(@FechaIni) = 3 THEN 'VA (AP)' ELSE P.D3 END,
						P.D4 = CASE WHEN DAY(@FechaIni) = 4 THEN 'VA (AP)' ELSE P.D4 END,
						P.D5 = CASE WHEN DAY(@FechaIni) = 5 THEN 'VA (AP)' ELSE P.D5 END,
						P.D6 = CASE WHEN DAY(@FechaIni) = 6 THEN 'VA (AP)' ELSE P.D6 END,
						P.D7 = CASE WHEN DAY(@FechaIni) = 7 THEN 'VA (AP)' ELSE P.D7 END,
						P.D8 = CASE WHEN DAY(@FechaIni) = 8 THEN 'VA (AP)' ELSE P.D8 END,
						P.D9 = CASE WHEN DAY(@FechaIni) = 9 THEN 'VA (AP)' ELSE P.D9 END,
						P.D10 = CASE WHEN DAY(@FechaIni) = 10 THEN 'VA (AP)' ELSE P.D10 END,
						P.D11 = CASE WHEN DAY(@FechaIni) = 11 THEN 'VA (AP)' ELSE P.D11 END,
						P.D12 = CASE WHEN DAY(@FechaIni) = 12 THEN 'VA (AP)' ELSE P.D12 END,
						P.D13 = CASE WHEN DAY(@FechaIni) = 13 THEN 'VA (AP)' ELSE P.D13 END,
						P.D14 = CASE WHEN DAY(@FechaIni) = 14 THEN 'VA (AP)' ELSE P.D14 END,
						P.D15 = CASE WHEN DAY(@FechaIni) = 15 THEN 'VA (AP)' ELSE P.D15 END,
						P.D16 = CASE WHEN DAY(@FechaIni) = 16 THEN 'VA (AP)' ELSE P.D16 END,
						P.D17 = CASE WHEN DAY(@FechaIni) = 17 THEN 'VA (AP)' ELSE P.D17 END,
						P.D18 = CASE WHEN DAY(@FechaIni) = 18 THEN 'VA (AP)' ELSE P.D18 END,
						P.D19 = CASE WHEN DAY(@FechaIni) = 19 THEN 'VA (AP)' ELSE P.D19 END,
						P.D20 = CASE WHEN DAY(@FechaIni) = 20 THEN 'VA (AP)' ELSE P.D20 END,
						P.D21 = CASE WHEN DAY(@FechaIni) = 21 THEN 'VA (AP)' ELSE P.D21 END,
						P.D22 = CASE WHEN DAY(@FechaIni) = 22 THEN 'VA (AP)' ELSE P.D22 END,
						P.D23 = CASE WHEN DAY(@FechaIni) = 23 THEN 'VA (AP)' ELSE P.D23 END,
						P.D24 = CASE WHEN DAY(@FechaIni) = 24 THEN 'VA (AP)' ELSE P.D24 END,
						P.D25 = CASE WHEN DAY(@FechaIni) = 25 THEN 'VA (AP)' ELSE P.D25 END,
						P.D26 = CASE WHEN DAY(@FechaIni) = 26 THEN 'VA (AP)' ELSE P.D26 END,
						P.D27 = CASE WHEN DAY(@FechaIni) = 27 THEN 'VA (AP)' ELSE P.D27 END,
						P.D28 = CASE WHEN DAY(@FechaIni) = 28 THEN 'VA (AP)' ELSE P.D28 END,
						P.D29 = CASE WHEN DAY(@FechaIni) = 29 THEN 'VA (AP)' ELSE P.D29 END,
						P.D30 = CASE WHEN DAY(@FechaIni) = 30 THEN 'VA (AP)' ELSE P.D30 END,
						P.D31 = CASE WHEN DAY(@FechaIni) = 31 THEN 'VA (AP)' ELSE P.D31 END
					FROM ReportesApp_Operaciones_ProgramacionVC_PersonalView P
					WHERE P.IDPersona = @IDPersona AND P.Anio = YEAR(@FechaFin) AND P.Mes = MONTH(@FechaFin)
				END

				SET @FechaIni = DATEADD(DAY,1,@FechaIni)
			END

			-- CORREO DE AVISO DEL SISTEMA
			/*
			DECLARE @CORREO_VC VARCHAR(MAX) = ''
			DECLARE @Asunto VARCHAR(500) = ''
			DECLARE @Mensaje VARCHAR(MAX) = ''

			SELECT @CORREO_VC = @CORREO_VC + '<tr style="background-color:#EAEDED">'
								+ '<td >' + UPPER(ISNULL(RTRIM(P.Documento),'')) + '</td>'
								+ '<td >' + UPPER(ISNULL(RTRIM(P.NombreCompleto),'')) + '</td>'
								+ '<td >' + CONVERT(VARCHAR,R.FechaIni,103) + '</td>'
								+ '<td >' + CONVERT(VARCHAR,R.FechaFin,103) + '</td>'
								+ '<td >' + CONVERT(VARCHAR,R.NumeroDias) + '</td>'
								+ '<td >' + CONVERT(VARCHAR,R.FechaRetorno,103) + '</td>'
								+ '<td >' + UPPER(ISNULL(RTRIM(R.UsuarioRegistra),'')) + '</td>'
								+ '<td >' + CONVERT(VARCHAR,R.FechaRegistra,103)+' '+CONVERT(VARCHAR,R.FechaRegistra,8) + '</td>'
								+ '<td >' + UPPER(ISNULL(RTRIM(R.UsuarioAprueba),'')) + '</td>'
								+ '<td >' + CONVERT(VARCHAR,R.FechaAprueba,103)+' '+CONVERT(VARCHAR,R.FechaAprueba,8) + '</td>'
								+ '</tr>'   
			FROM ReportesApp_Operaciones_ProgramacionVC_Registros R
			LEFT JOIN PersonaMast P ON P.Persona = R.IDPersona
			WHERE idProgVC = @idProgVC

			SET @Asunto = 'VACACIONES DE CONDUCTOR PROGRAMADAS'

			SET @Mensaje = '<p><h2>ALERTA DE PROGRAMACIÓN DE VACACIONES POR EL ÁREA DE OPERACIONES</h2></p>'
							  + '<p>	
									<table border="1" cellspacing="1" cellpadding="1"  style="font-size: 10px;">
										<thead align="center" style="background-color:#98FB98; font-size: 13px;">
											<tr>
												<td align="center" width="100"><b>DOCUMENTO</b></td>
												<td align="center" width="200"><b>CONDUCTOR</b></td>
												<td align="center" width="100"><b>FECHA INICIO</b></td>
												<td align="center" width="100"><b>FECHA FIN</b></td>
												<td align="center" width="100"><b>DÍAS</b></td>
												<td align="center" width="100"><b>FECHA RETORNO</b></td>
												<td align="center" width="100"><b>USUARIO REGISTRA</b></td>
												<td align="center" width="100"><b>FECHA REGISTRA</b></td>
												<td align="center" width="100"><b>USUARIO APRUEBA</b></td>
												<td align="center" width="100"><b>FECHA APRUEBA</b></td>
											</tr>
										</thead>
										<tbody>'
										+ ISNULL(@CORREO_VC, '') 	
								+'		</tbody>
									</table>
								</p>'	
						  +'<p> Fecha y Hora: ' + CONVERT(CHAR(10),GETDATE(),103) + RIGHT(RTRIM(CONVERT(CHAR(26),GETDATE(),22)), 12) + '</p>'
						  +'<p><font face="verdana" size="2" color="DarkRed"> Tecnologías de la Información - GRUPO TRANSPESA</font></p>'

		    EXEC msdb.dbo.sp_send_dbmail 
			@profile_name = 'AVISODESISTEMA',
			@recipients = 'sescobedo@transpesa.com.pe;talentohumano@transpesa.com.pe;jefeoperacioneslima@transpesa.com.pe;lleython@transpesa.com.pe;operacionestrujillo@transpesa.com.pe;supervisoroperaciones@transpesa.com.pe;supervisoroperaciones1@transpesa.com.pe;operacionesLima@transpesa.com.pe'
			--@recipients = 'operacionestrujillo4@transpesa.com.pe; programacionlindley@transpesa.com.pe; operacionestrujillo3@transpesa.com.pe; operacionestrujillo@transpesa.com.pe; Operaciones@transpesa.com.pe; lleython@transpesa.com.pe; dpesantes@transpesa.com.pe; operacionestrujillo1@transpesa.com.pe'
			--,@blind_copy_recipients = 'lleython@transpesa.com.pe;auditoria@transpesa.com.pe;jrojas@transpesa.com.pe;desarrollo3@transpesa.com.pe;auditoria@transpesa.com.pe;gerenteoperaciones@transpesa.com.pe' 
			,@subject = @Asunto
			,@body_format = 'HTML' 
			,@body = @Mensaje
			*/
		END
	END

	IF (@OpcionVC = 2) BEGIN
		IF (@EstadoVC = 0) BEGIN 
			UPDATE ReportesApp_Operaciones_ProgramacionVC_Registros
			SET Codigo = 'CO (AP)', Aprobado = 1, UsuarioAprueba = @Usuario, FechaAprueba = GETDATE()
			WHERE FechaRetorno = @FechaRetorno AND IDPersona = @IDPersona
		END
		ELSE BEGIN
			UPDATE ReportesApp_Operaciones_ProgramacionVC_Registros
			SET Codigo = 'CO (AP)'
			WHERE FechaRetorno = @FechaRetorno AND IDPersona = @IDPersona
		END
		
		DECLARE @APROBAR TABLE (Numero INT, IDPersona INT, FechaFin DATE)

		INSERT INTO @APROBAR(Numero, IDPersona, FechaFin)
		SELECT ROW_NUMBER() OVER(ORDER BY idProgVC ASC), IDPersona, FechaFin
		FROM ReportesApp_Operaciones_ProgramacionVC_Registros
		WHERE FechaRetorno = @FechaRetorno AND IDPersona = @IDPersona
		ORDER BY FechaFin ASC

		DECLARE @Nro INT = 1
		WHILE (@Nro <= (SELECT MAX(Numero) FROM @APROBAR)) BEGIN
			DECLARE @FINI DATE = (SELECT FechaFin FROM @APROBAR WHERE Numero = @Nro)

			UPDATE P 
			SET P.D1 = CASE WHEN DAY(@FINI) = 1 THEN 'CO (AP)' ELSE P.D1 END,
				P.D2 = CASE WHEN DAY(@FINI) = 2 THEN 'CO (AP)' ELSE P.D2 END,
				P.D3 = CASE WHEN DAY(@FINI) = 3 THEN 'CO (AP)' ELSE P.D3 END,
				P.D4 = CASE WHEN DAY(@FINI) = 4 THEN 'CO (AP)' ELSE P.D4 END,
				P.D5 = CASE WHEN DAY(@FINI) = 5 THEN 'CO (AP)' ELSE P.D5 END,
				P.D6 = CASE WHEN DAY(@FINI) = 6 THEN 'CO (AP)' ELSE P.D6 END,
				P.D7 = CASE WHEN DAY(@FINI) = 7 THEN 'CO (AP)' ELSE P.D7 END,
				P.D8 = CASE WHEN DAY(@FINI) = 8 THEN 'CO (AP)' ELSE P.D8 END,
				P.D9 = CASE WHEN DAY(@FINI) = 9 THEN 'CO (AP)' ELSE P.D9 END,
				P.D10 = CASE WHEN DAY(@FINI) = 10 THEN 'CO (AP)' ELSE P.D10 END,
				P.D11 = CASE WHEN DAY(@FINI) = 11 THEN 'CO (AP)' ELSE P.D11 END,
				P.D12 = CASE WHEN DAY(@FINI) = 12 THEN 'CO (AP)' ELSE P.D12 END,
				P.D13 = CASE WHEN DAY(@FINI) = 13 THEN 'CO (AP)' ELSE P.D13 END,
				P.D14 = CASE WHEN DAY(@FINI) = 14 THEN 'CO (AP)' ELSE P.D14 END,
				P.D15 = CASE WHEN DAY(@FINI) = 15 THEN 'CO (AP)' ELSE P.D15 END,
				P.D16 = CASE WHEN DAY(@FINI) = 16 THEN 'CO (AP)' ELSE P.D16 END,
				P.D17 = CASE WHEN DAY(@FINI) = 17 THEN 'CO (AP)' ELSE P.D17 END,
				P.D18 = CASE WHEN DAY(@FINI) = 18 THEN 'CO (AP)' ELSE P.D18 END,
				P.D19 = CASE WHEN DAY(@FINI) = 19 THEN 'CO (AP)' ELSE P.D19 END,
				P.D20 = CASE WHEN DAY(@FINI) = 20 THEN 'CO (AP)' ELSE P.D20 END,
				P.D21 = CASE WHEN DAY(@FINI) = 21 THEN 'CO (AP)' ELSE P.D21 END,
				P.D22 = CASE WHEN DAY(@FINI) = 22 THEN 'CO (AP)' ELSE P.D22 END,
				P.D23 = CASE WHEN DAY(@FINI) = 23 THEN 'CO (AP)' ELSE P.D23 END,
				P.D24 = CASE WHEN DAY(@FINI) = 24 THEN 'CO (AP)' ELSE P.D24 END,
				P.D25 = CASE WHEN DAY(@FINI) = 25 THEN 'CO (AP)' ELSE P.D25 END,
				P.D26 = CASE WHEN DAY(@FINI) = 26 THEN 'CO (AP)' ELSE P.D26 END,
				P.D27 = CASE WHEN DAY(@FINI) = 27 THEN 'CO (AP)' ELSE P.D27 END,
				P.D28 = CASE WHEN DAY(@FINI) = 28 THEN 'CO (AP)' ELSE P.D28 END,
				P.D29 = CASE WHEN DAY(@FINI) = 29 THEN 'CO (AP)' ELSE P.D29 END,
				P.D30 = CASE WHEN DAY(@FINI) = 30 THEN 'CO (AP)' ELSE P.D30 END,
				P.D31 = CASE WHEN DAY(@FINI) = 31 THEN 'CO (AP)' ELSE P.D31 END
			FROM ReportesApp_Operaciones_ProgramacionVC_PersonalView P
			WHERE P.IDPersona = @IDPersona AND P.Anio = YEAR(@FINI) AND P.Mes = MONTH(@FINI)

			SET @Nro = @Nro + 1
		END
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

SELECT TOP(10) * FROM [msdb].[dbo].[sysmail_allitems]
ORDER BY [send_request_date] DESC

-------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 14/06/2024
-- Description:	REGISTRAR PROGRAMACION SPRING
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ProgramacionVC_RegistrarProgramacionSPRING]
@OpcionVC INT,
@Periodo VARCHAR(6),
@idProgVC INT,
@IDPersona INT,
@FechaRetorno DATE,
@Usuario VARCHAR(20)
AS
DECLARE @exito2 VARCHAR(MAX)

SET @exito2 = '0 = Programación de Conductor Registrada.'

BEGIN TRAN
BEGIN TRY
	IF (@OpcionVC = 1) BEGIN
		IF (@Usuario NOT IN ('LLEYTHON','KRAMIREZ','GREYES')) BEGIN
			SET @exito2 = '-1 = Las vacaciones solo pueden ser registradas por el encargado del área de Operaciones.'
			ROLLBACK
			GOTO Terminar
		END
		ELSE BEGIN
			DECLARE @Secuencia INT = (SELECT MAX(Secuencia) FROM PR_VacacionUtilizacion WHERE Empleado = @IDPersona)
			SET @Secuencia = ISNULL(@Secuencia,0) + 1
			DECLARE @FechaIni DATETIME = (SELECT FechaIni FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE idProgVC = @idProgVC)
			DECLARE @FechaFin DATETIME = (SELECT FechaFin FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE idProgVC = @idProgVC)

			IF ((SELECT TOP(1) PendientePagoAnterior FROM PR_VacacionPeriodo WHERE Empleado = @IDPersona ORDER BY Ano DESC) > 30) BEGIN
				DECLARE @Periodo1 INT
				IF (EXISTS(SELECT NumeroPeriodo FROM PR_VacacionPeriodo WHERE Empleado = @IDPersona AND Ano = YEAR(GETDATE()))) BEGIN
					SET @Periodo1 = (SELECT NumeroPeriodo FROM PR_VacacionPeriodo WHERE Empleado = @IDPersona AND Ano = YEAR(GETDATE()) - 2)
				END
				ELSE BEGIN
					SET @Periodo1 = (SELECT NumeroPeriodo FROM PR_VacacionPeriodo WHERE Empleado = @IDPersona AND Ano = YEAR(GETDATE()) - 3)
				END
				DECLARE @NumeroDias2 INT = (SELECT NumeroDias FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE idProgVC = @idProgVC)  --5
				DECLARE @PendienteReal INT = (SELECT PendientesReales FROM PR_VacacionPeriodo WHERE Empleado = @IDPersona AND NumeroPeriodo = @Periodo1) --2

				IF (@NumeroDias2 <= @PendienteReal) BEGIN
					INSERT INTO PR_VacacionUtilizacion (Empleado, Secuencia, NumeroPeriodo, FechaInicio, FechaFin, TipoUtilizacion, DiasUtilizacion,
					UltimaFechaModif, UltimoUsuario, CompaniaSocio)
					VALUES (@IDPersona, @Secuencia, @Periodo1, @FechaIni, @FechaFin, 'GOC', @NumeroDias2, GETDATE(), @Usuario, '10000000')
				END
				ELSE BEGIN
					INSERT INTO PR_VacacionUtilizacion (Empleado, Secuencia, NumeroPeriodo, FechaInicio, FechaFin, TipoUtilizacion, DiasUtilizacion,
					UltimaFechaModif, UltimoUsuario, CompaniaSocio)
					VALUES (@IDPersona, @Secuencia, @Periodo1, @FechaIni, DATEADD(DAY,@PendienteReal - 1,@FechaIni), 'GOC', @PendienteReal, GETDATE(), @Usuario, '10000000')

					SET @Secuencia = @Secuencia + 1
					DECLARE @Periodo3 INT
					IF (EXISTS(SELECT NumeroPeriodo FROM PR_VacacionPeriodo WHERE Empleado = @IDPersona AND Ano = YEAR(GETDATE()))) BEGIN
						SET @Periodo3 = (SELECT NumeroPeriodo FROM PR_VacacionPeriodo WHERE Empleado = @IDPersona AND Ano = YEAR(GETDATE()) - 1)
					END
					ELSE BEGIN
						SET @Periodo3 = (SELECT NumeroPeriodo FROM PR_VacacionPeriodo WHERE Empleado = @IDPersona AND Ano = YEAR(GETDATE()) - 2)
					END

					INSERT INTO PR_VacacionUtilizacion (Empleado, Secuencia, NumeroPeriodo, FechaInicio, FechaFin, TipoUtilizacion, DiasUtilizacion,
					UltimaFechaModif, UltimoUsuario, CompaniaSocio)
					VALUES (@IDPersona, @Secuencia, @Periodo3, DATEADD(DAY,@PendienteReal,@FechaIni), @FechaFin, 'GOC', @NumeroDias2 - @PendienteReal, GETDATE(), @Usuario, '10000000')
				END
			END
			ELSE BEGIN
				DECLARE @Periodo2 INT
				IF (EXISTS(SELECT NumeroPeriodo FROM PR_VacacionPeriodo WHERE Empleado = @IDPersona AND Ano = YEAR(GETDATE()))) BEGIN
					SET @Periodo2 = (SELECT NumeroPeriodo FROM PR_VacacionPeriodo WHERE Empleado = @IDPersona AND Ano = YEAR(GETDATE()) - 1)
				END
				ELSE BEGIN
					SET @Periodo2 = (SELECT NumeroPeriodo FROM PR_VacacionPeriodo WHERE Empleado = @IDPersona AND Ano = YEAR(GETDATE()) - 2)
				END
				DECLARE @NumeroDias INT = (SELECT NumeroDias FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE idProgVC = @idProgVC)

				IF (MONTH(@FechaIni) != MONTH(@FechaFin)) BEGIN
					DECLARE @UltimaFecha DATETIME = CAST(DATEADD(MONTH,DATEDIFF(MONTH,0,@FechaIni)+1,0)-1 AS DATE)
					DECLARE @PrimeraFecha DATETIME = DATEADD(DAY,1,@UltimaFecha)
					DECLARE @NroDiaI INT = DATEDIFF(DAY,@FechaIni,@UltimaFecha) + 1
					DECLARE @NroDiaF INT = DATEDIFF(DAY,@PrimeraFecha,@FechaFin) + 1

					INSERT INTO PR_VacacionUtilizacion (Empleado, Secuencia, NumeroPeriodo, FechaInicio, FechaFin, TipoUtilizacion, DiasUtilizacion,
					UltimaFechaModif, UltimoUsuario, CompaniaSocio)
					VALUES (@IDPersona, @Secuencia, @Periodo2, @FechaIni, @UltimaFecha, 'GOC', @NroDiaI, GETDATE(), @Usuario, '10000000')

					DECLARE @Secuencia2 INT = (SELECT MAX(Secuencia) FROM PR_VacacionUtilizacion WHERE Empleado = @IDPersona)
					SET @Secuencia2 = ISNULL(@Secuencia2,0) + 1

					INSERT INTO PR_VacacionUtilizacion (Empleado, Secuencia, NumeroPeriodo, FechaInicio, FechaFin, TipoUtilizacion, DiasUtilizacion,
					UltimaFechaModif, UltimoUsuario, CompaniaSocio)
					VALUES (@IDPersona, @Secuencia2, @Periodo2, @PrimeraFecha, @FechaFin, 'GOC', @NroDiaF, GETDATE(), @Usuario, '10000000')
				END
				ELSE BEGIN
					INSERT INTO PR_VacacionUtilizacion (Empleado, Secuencia, NumeroPeriodo, FechaInicio, FechaFin, TipoUtilizacion, DiasUtilizacion,
					UltimaFechaModif, UltimoUsuario, CompaniaSocio)
					VALUES (@IDPersona, @Secuencia, @Periodo2, @FechaIni, @FechaFin, 'GOC', @NumeroDias, GETDATE(), @Usuario, '10000000')
				END
			END
		END
	END
	
	IF (@OpcionVC = 2) BEGIN
		DECLARE @REGISTRAR TABLE (Numero INT, IDPersona INT, FechaInicio DATE, FechaFin DATE)
		
		INSERT INTO @REGISTRAR(Numero, IDPersona, FechaInicio, FechaFin)
		SELECT ROW_NUMBER() OVER(ORDER BY idProgVC ASC), IDPersona, FechaIni, FechaFin
		FROM ReportesApp_Operaciones_ProgramacionVC_Registros
		WHERE FechaRetorno = @FechaRetorno AND IDPersona = @IDPersona
		ORDER BY FechaFin ASC

		DECLARE @Nro INT = 1
		WHILE (@Nro <= (SELECT MAX(Numero) FROM @REGISTRAR)) BEGIN
			DECLARE @PERSONA INT = (SELECT IDPersona FROM @REGISTRAR WHERE Numero = @Nro)
			DECLARE @FINI DATE = (SELECT FechaInicio FROM @REGISTRAR WHERE Numero = @Nro)
			DECLARE @FFIN DATE = (SELECT FechaFin FROM @REGISTRAR WHERE Numero = @Nro)

			INSERT INTO ReportesApp_RRHH_Asistencia_Compensaciones(IDPersona,FechaTrabajada,IdTipoAsisTrabajada,FechaCompensa,IdTipoAsisCompensa,FHRegistra,UsuarioRegistra)
			VALUES(@PERSONA, @FINI, 32, @FFIN, 55, GETDATE(), @Usuario)

			DECLARE @Asistencias TABLE (IdPersona INT, Fecha DATE, IdTipoAsist INT, Letra VARCHAR(2))
			DECLARE @Fechas TABLE (PK INT IDENTITY(1,1) PRIMARY KEY, fecha DATE)
						
			INSERT INTO @Asistencias(IdPersona, Fecha, IdTipoAsist, Letra)
			VALUES (@PERSONA, @FFIN, 55, 'CO')

			INSERT INTO @Fechas (fecha)
			SELECT DISTINCT Fecha FROM @Asistencias

			DELETE R
			FROM ReportesApp_RRHH_Asistencia R
			INNER JOIN @Asistencias A ON A.IdPersona = R.IDPersona AND A.Fecha = R.Fecha

			INSERT INTO ReportesApp_RRHH_Asistencia(IdPersona,Planilla,Fecha,IdTipoAsist,ConceptoAcceso,FHRegistra,UserCrea)
			SELECT DISTINCT A.IdPersona, 'CD', A.Fecha, A.IdTipoAsist, T.ConceptoAcceso, GETDATE(), @Usuario
			FROM @Asistencias A
			INNER JOIN ReportesApp_RRHH_Asistencia_Tipo T ON T.IdTipoAsist = A.IdTipoAsist

			DECLARE @I INT = 1
			DECLARE @MAX INT = (SELECT MAX(PK) FROM @Fechas)
			DECLARE @x_FechaSelec DATE, @x_DiaSelec INT

			WHILE @I<=@MAX BEGIN
				SELECT @x_FechaSelec = FECHA, @x_DiaSelec = DAY(FECHA) 
				FROM @Fechas WHERE PK = @I	

				UPDATE V 
	            SET V.D1=CASE WHEN DAY(A.Fecha) = 1 THEN 'CO' ELSE V.D1 END,
					V.D2=CASE WHEN DAY(A.Fecha) = 2 THEN 'CO' ELSE V.D2 END,
					V.D3=CASE WHEN DAY(A.Fecha) = 3 THEN 'CO' ELSE V.D3 END,
					V.D4=CASE WHEN DAY(A.Fecha) = 4 THEN 'CO' ELSE V.D4 END,
					V.D5=CASE WHEN DAY(A.Fecha) = 5 THEN 'CO' ELSE V.D5 END,
					V.D6=CASE WHEN DAY(A.Fecha) = 6 THEN 'CO' ELSE V.D6 END,
					V.D7=CASE WHEN DAY(A.Fecha) = 7 THEN 'CO' ELSE V.D7 END,
					V.D8=CASE WHEN DAY(A.Fecha) = 8 THEN 'CO' ELSE V.D8 END,
					V.D9=CASE WHEN DAY(A.Fecha) = 9 THEN 'CO' ELSE V.D9 END,
					V.D10=CASE WHEN DAY(A.Fecha) = 10 THEN 'CO' ELSE V.D10 END,
					V.D11=CASE WHEN DAY(A.Fecha) = 11 THEN 'CO' ELSE V.D11 END,
					V.D12=CASE WHEN DAY(A.Fecha) = 12 THEN 'CO' ELSE V.D12 END,
					V.D13=CASE WHEN DAY(A.Fecha) = 13 THEN 'CO' ELSE V.D13 END,
					V.D14=CASE WHEN DAY(A.Fecha) = 14 THEN 'CO' ELSE V.D14 END,
					V.D15=CASE WHEN DAY(A.Fecha) = 15 THEN 'CO' ELSE V.D15 END,
					V.D16=CASE WHEN DAY(A.Fecha) = 16 THEN 'CO' ELSE V.D16 END,
					V.D17=CASE WHEN DAY(A.Fecha) = 17 THEN 'CO' ELSE V.D17 END,
					V.D18=CASE WHEN DAY(A.Fecha) = 18 THEN 'CO' ELSE V.D18 END,
					V.D19=CASE WHEN DAY(A.Fecha) = 19 THEN 'CO' ELSE V.D19 END,
					V.D20=CASE WHEN DAY(A.Fecha) = 20 THEN 'CO' ELSE V.D20 END,
					V.D21=CASE WHEN DAY(A.Fecha) = 21 THEN 'CO' ELSE V.D21 END,
					V.D22=CASE WHEN DAY(A.Fecha) = 22 THEN 'CO' ELSE V.D22 END,
					V.D23=CASE WHEN DAY(A.Fecha) = 23 THEN 'CO' ELSE V.D23 END,
					V.D24=CASE WHEN DAY(A.Fecha) = 24 THEN 'CO' ELSE V.D24 END,
					V.D25=CASE WHEN DAY(A.Fecha) = 25 THEN 'CO' ELSE V.D25 END,
					V.D26=CASE WHEN DAY(A.Fecha) = 26 THEN 'CO' ELSE V.D26 END,
					V.D27=CASE WHEN DAY(A.Fecha) = 27 THEN 'CO' ELSE V.D27 END,
					V.D28=CASE WHEN DAY(A.Fecha) = 28 THEN 'CO' ELSE V.D28 END,
					V.D29=CASE WHEN DAY(A.Fecha) = 29 THEN 'CO' ELSE V.D29 END,
					V.D30=CASE WHEN DAY(A.Fecha) = 30 THEN 'CO' ELSE V.D30 END,
					V.D31=CASE WHEN DAY(A.Fecha) = 31 THEN 'CO' ELSE V.D31 END
				FROM ReportesApp_RRHH_AsistenciaView V
				LEFT JOIN @Asistencias A ON A.IDPersona = V.IDPersona AND A.Fecha = @x_FechaSelec
				WHERE V.Anio = YEAR(A.Fecha) AND V.Mes = MONTH(A.Fecha) AND V.CodPlanilla = 'CD'

				SET @I = @I + 1
			END

			SET @Nro = @Nro + 1
		END
	END
END TRY

BEGIN CATCH
	SET @exito2 = RTRIM(LTRIM(STR(ERROR_NUMBER()))) + '=' + ERROR_MESSAGE()
	ROLLBACK
	GOTO Terminar
END CATCH

IF @@TRANCOUNT > 0
	--ROLLBACK
	COMMIT;

TERMINAR:
IF LEFT(@Exito2,1)='-' OR cast(left(@Exito2,1) as int)>0 
BEGIN
	DECLARE @storedProcedureName VARCHAR(100)
	SELECT @storedProcedureName = OBJECT_NAME(@@PROCID)
	SET @exito2 = @exito2 + CHAR(13)+CHAR(13)+'SP Name: '+@storedProcedureName
END

SELECT @exito2 exito2

-------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 13-06-2025
-- Description:	LISTAR PROGRAMACIONES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ProgramacionVC_ListarProgramacionConductor]
@OpcionVC INT,
@idProgVC INT,
@IDPersona INT,
@FechaRetorno DATE
AS
BEGIN
	IF (@OpcionVC = 1) BEGIN		-- LISTAR VACACIONES
		SELECT RTRIM(P.Documento) AS 'DOCUMENTO', RTRIM(LTRIM(P.NombreCompleto)) AS 'CONDUCTOR',
		CONVERT(VARCHAR(10),R.FechaIni,103) AS 'FECHA_INICIO', CONVERT(VARCHAR(10),R.FechaFin,103) AS 'FECHA_FIN',
		'Vacaciones de '+CONVERT(VARCHAR,R.NumeroDias)+' días.' AS 'DÍAS', CONVERT(VARCHAR(10),R.FechaRetorno,103) AS 'FECHA_RETORNO',
		R.UsuarioRegistra AS 'USUARIO_REGISTRA', CONVERT(VARCHAR(10),R.FechaRegistra,103)+' '+CONVERT(VARCHAR(10),R.FechaRegistra,8) AS 'FECHA_REGISTRA',
		R.UsuarioAprueba AS 'USUARIO', CONVERT(VARCHAR(10),R.FechaAprueba,103)+' '+CONVERT(VARCHAR(10),R.FechaAprueba,8) AS 'FECHA_APRUEBA'
		FROM ReportesApp_Operaciones_ProgramacionVC_Registros R
		LEFT JOIN EmpleadoMast E ON E.Empleado = R.IDPersona
		INNER JOIN  DBO.PersonaMast P ON P.Persona = E.Empleado 
		LEFT JOIN OP_TR_Conductor C ON C.IdPersona = P.Persona AND C.Estado='A'
		WHERE (C.Estado = 'A') AND (E.Estado = 'A') AND (R.idProgVC = @idProgVC)
	END

	IF (@OpcionVC = 2) BEGIN		-- LISTAR COMPENSACIONES
		DECLARE @DiasInicio VARCHAR(350) = (SELECT LTRIM(RTRIM(STUFF((SELECT ', ' + CONVERT(VARCHAR,FechaIni,103)
										   FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE (FechaRetorno = @FechaRetorno)
										   AND (IDPersona = @IDPersona) ORDER BY FechaIni ASC FOR XML PATH ('')),1,1,''))))
		DECLARE @DiasFin VARCHAR(350) = (SELECT LTRIM(RTRIM(STUFF((SELECT ', ' + CONVERT(VARCHAR,FechaFin,103)
										 FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE (FechaRetorno = @FechaRetorno)
										 AND (IDPersona = @IDPersona) ORDER BY FechaFin ASC FOR XML PATH ('')),1,1,''))))
		DECLARE @CantidadDias INT = (SELECT COUNT(*) FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE (FechaRetorno = @FechaRetorno)
									 AND (IDPersona = @IDPersona))

		SELECT TOP(1) RTRIM(P.Documento) AS 'DOCUMENTO', RTRIM(LTRIM(P.NombreCompleto)) AS 'CONDUCTOR',
		@DiasFin AS 'FECHAS', 'Compensación de '+CONVERT(VARCHAR,@CantidadDias)+' días: '+@DiasInicio AS 'DÍAS',
		CONVERT(VARCHAR(10),R.FechaRetorno,103) AS 'FECHA_RETORNO',
		R.UsuarioRegistra AS 'USUARIO_REGISTRA', CONVERT(VARCHAR(10),R.FechaRegistra,103)+' '+CONVERT(VARCHAR(10),R.FechaRegistra,8) AS 'FECHA_REGISTRA',
		R.UsuarioAprueba AS 'USUARIO', CONVERT(VARCHAR(10),R.FechaAprueba,103)+' '+CONVERT(VARCHAR(10),R.FechaAprueba,8) AS 'FECHA_APRUEBA'
		FROM ReportesApp_Operaciones_ProgramacionVC_Registros R
		LEFT JOIN EmpleadoMast E ON E.Empleado = R.IDPersona
		INNER JOIN  DBO.PersonaMast P ON P.Persona = E.Empleado 
		LEFT JOIN OP_TR_Conductor C ON C.IdPersona = P.Persona AND C.Estado='A'
		WHERE (R.FechaRetorno = @FechaRetorno) AND (R.IDPersona = @IDPersona) AND (C.Estado = 'A') AND (E.Estado = 'A')
	END
END

-------------------------------------------------------------------------
-------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author Name:	GERARDO REYES
-- Create date: 03-06-2025
-- Description:	LISTAR PROGRAMACION VACACIONES
-- =============================================
/*
EXEC ReportesApp_Operaciones_ProgramacionVC_ListarProgramacionVC @Periodo = '062025'
*/
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ProgramacionVC_ListarProgramacionVC]
@Periodo VARCHAR(6),
@Conductor VARCHAR(250),
@Operacion VARCHAR(10)
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

	DECLARE @T_Prueba TABLE (IDPersona INT, Nombre VARCHAR(300), Mes VARCHAR(15), Anio VARCHAR(15), FechaIngreso VARCHAR(100), Operacion VARCHAR(100),
							 VacacionesProg VARCHAR(50), VacacionesPend VARCHAR(50), CompensacionProg VARCHAR(50), CompensacionPend VARCHAR(50),
							 D1 VARCHAR(15), D2 VARCHAR(15), D3 VARCHAR(15), D4 VARCHAR(15), D5 VARCHAR(15), D6 VARCHAR(15), D7 VARCHAR(15),
						     D8 VARCHAR(15), D9 VARCHAR(15), D10 VARCHAR(15), D11 VARCHAR(15), D12 VARCHAR(15), D13 VARCHAR(15), D14 VARCHAR(15),
							 D15 VARCHAR(15), D16 VARCHAR(15), D17 VARCHAR(15), D18 VARCHAR(15), D19 VARCHAR(15), D20 VARCHAR(15), D21 VARCHAR(15),
							 D22 VARCHAR(15), D23 VARCHAR(15), D24 VARCHAR(15), D25 VARCHAR(15), D26 VARCHAR(15), D27 VARCHAR(15), D28 VARCHAR(15),
							 D29 VARCHAR(15), D30 VARCHAR(15), D31 VARCHAR(15))

	DECLARE @D1 CHAR(5),@D2 CHAR(5),@D3 CHAR(5),@D4 CHAR(5),@D5 CHAR(5),@D6 CHAR(5),@D7 CHAR(5),@D8 CHAR(5),@D9 CHAR(5),
			@D10 CHAR(5),@D11 CHAR(5),@D12 CHAR(5),@D13 CHAR(5),@D14 CHAR(5),@D15 CHAR(5),@D16 CHAR(5),@D17 CHAR(5),@D18 CHAR(5),
			@D19 CHAR(5),@D20 CHAR(5),@D21 CHAR(5),@D22 CHAR(5),@D23 CHAR(5),@D24 CHAR(5),@D25 CHAR(5),@D26 CHAR(5),@D27 CHAR(5),
			@D28 CHAR(5),@D29 CHAR(5),@D30 CHAR(5),@D31 CHAR(5)

	INSERT INTO @T_Prueba (Nombre, Mes, Anio, FechaIngreso, Operacion, VacacionesProg, VacacionesPend, CompensacionProg, CompensacionPend)
	VALUES ('<TRABAJADOR>','Mes','Anio','F.Ingreso','Operacion','VacacionesProg','VacacionesPend','CompensacionProg','CompensacionPend')

	DECLARE @FechaIni DATE, @FechaFin DATE, @FechaVer DATE

	IF (@FDesde IS NOT NULL) BEGIN 
		SET @FechaVer = CAST(CAST(@DiaInicio As Varchar(2))+'/'+ RIGHT('0'+ CAST(month(@FDesde) AS VARCHAR(2)),2)+'/'+CAST(@Anio as VARCHAR(4)) AS DATE)
	END
	ELSE BEGIN
		SET @FechaVer = CAST(CAST(@DiaInicio As Varchar(2))+'/'+ RIGHT('0'+ CAST(@Mes AS VARCHAR(2)),2)+'/'+CAST(@Anio as VARCHAR(4)) AS DATE)
	END

	SET @FechaIni = @FechaVer
	SET @FechaFin = DATEADD(D,-1, DATEADD(MONTH,1,@FechaVer))

	IF (@Operacion = '5') BEGIN
		INSERT INTO @T_Prueba (IDPersona, Mes, Anio, Nombre, FechaIngreso, Operacion, VacacionesPend)
		SELECT DISTINCT P.Persona, V.Mes, V.Anio, RTRIM(LTRIM(P.NombreCompleto)), CONVERT(VARCHAR(10),E.FechaIngreso,103),
		CASE WHEN C.CodigoEnapu = '1' THEN 'TOLVAS'
			 WHEN C.CodigoEnapu = '2' THEN 'LINDLEY'
			 WHEN C.CodigoEnapu = '3' THEN 'LIMAGAS'
			 WHEN C.CodigoEnapu = '4' THEN 'GENERAL'
			 WHEN C.CodigoEnapu = '10' THEN 'VOLCAN'
			 ELSE 'SIN OPERACION' END,
		ISNULL((SELECT TOP(1) PendientePagoAnterior FROM PR_VacacionPeriodo WHERE Empleado = P.Persona AND CompaniaSocio = '10000000' ORDER BY Ano DESC),0)
		FROM empleadomast E
		INNER JOIN  DBO.PersonaMast P ON P.Persona = E.Empleado 
		LEFT JOIN OP_TR_Conductor C ON C.IdPersona = P.Persona AND C.Estado='A'
		INNER JOIN PR_TipoPlanilla tp on tp.TipoPlanilla = E.TipoPlanilla
		INNER JOIN ReportesApp_RRHH_AsistenciaView V ON V.IDPersona = E.Empleado
		WHERE V.Anio = RIGHT(@periodo,4) and V.Mes = LEFT(@periodo,2) AND (V.CodPlanilla = 'CD' AND C.Estado = 'A')
		AND (@Conductor IS NULL OR LTRIM(RTRIM(P.NombreCompleto)) LIKE '%' + @Conductor + '%')
	END
	ELSE BEGIN
		INSERT INTO @T_Prueba (IDPersona, Mes, Anio, Nombre, FechaIngreso, Operacion, VacacionesPend)
		SELECT DISTINCT P.Persona, V.Mes, V.Anio, RTRIM(LTRIM(P.NombreCompleto)), CONVERT(VARCHAR(10),E.FechaIngreso,103),
		CASE WHEN C.CodigoEnapu = '1' THEN 'TOLVAS'
			 WHEN C.CodigoEnapu = '2' THEN 'LINDLEY'
			 WHEN C.CodigoEnapu = '3' THEN 'LIMAGAS'
			 WHEN C.CodigoEnapu = '4' THEN 'GENERAL'
			 WHEN C.CodigoEnapu = '10' THEN 'VOLCAN'
			 ELSE 'SIN OPERACION' END,
		ISNULL((SELECT TOP(1) PendientePagoAnterior FROM PR_VacacionPeriodo WHERE Empleado = P.Persona AND CompaniaSocio = '10000000' ORDER BY Ano DESC),0)
		FROM empleadomast E
		INNER JOIN  DBO.PersonaMast P ON P.Persona = E.Empleado 
		LEFT JOIN OP_TR_Conductor C ON C.IdPersona = P.Persona AND C.Estado='A'
		INNER JOIN PR_TipoPlanilla tp on tp.TipoPlanilla = E.TipoPlanilla
		INNER JOIN ReportesApp_RRHH_AsistenciaView V ON V.IDPersona = E.Empleado
		WHERE V.Anio = RIGHT(@periodo,4) and V.Mes = LEFT(@periodo,2) AND (V.CodPlanilla = 'CD' AND C.Estado = 'A')
		AND (@Conductor IS NULL OR LTRIM(RTRIM(P.NombreCompleto)) LIKE '%' + @Conductor + '%') AND (C.CodigoEnapu = @Operacion)
	END

	DECLARE @T_DiasComp TABLE (IDPersona INT, Cantidad INT)
	INSERT INTO @T_DiasComp (IDPersona, Cantidad)
	SELECT D.IDPersona, ISNULL(COUNT(D.Fecha),0)
	FROM ReportesApp_RRHH_Asistencia A
	INNER JOIN ReportesApp_RRHH_Asistencia_DescansoF D ON D.IDPersona=A.IDPersona AND D.Fecha=A.Fecha
	LEFT JOIN ReportesApp_RRHH_Asistencia_Compensaciones C ON C.IDPersona=A.IDPersona AND C.FechaTrabajada=A.Fecha
	LEFT JOIN OP_TR_Conductor C1 ON C1.IdPersona = D.IDPersona AND C1.Estado='A'
	WHERE A.IDTipoAsist = 32 AND C.FechaCompensa IS NULL AND C1.CodigoEnapu IN (1,2,3,4)
	GROUP BY D.IDPersona
	ORDER BY D.IDPersona

	INSERT INTO @T_DiasComp (IDPersona, Cantidad)
	SELECT * FROM ReportesApp_RRHH_Asistencias_CompensacionesVolcan

	UPDATE T
	SET T.CompensacionPend = CONVERT(VARCHAR,C.Cantidad)
	FROM @T_Prueba T
	LEFT JOIN @T_DiasComp C ON T.IdPersona = C.IDPersona
	WHERE T.IdPersona = C.IDPersona

	UPDATE T
	SET T.VacacionesProg = (SELECT
							CASE WHEN D1 LIKE '%VA%' THEN 1 ELSE 0 END +
							CASE WHEN D2 LIKE '%VA%' THEN 1 ELSE 0 END +
							CASE WHEN D3 LIKE '%VA%' THEN 1 ELSE 0 END +
							CASE WHEN D4 LIKE '%VA%' THEN 1 ELSE 0 END +
							CASE WHEN D5 LIKE '%VA%' THEN 1 ELSE 0 END +
							CASE WHEN D6 LIKE '%VA%' THEN 1 ELSE 0 END +
							CASE WHEN D7 LIKE '%VA%' THEN 1 ELSE 0 END +
							CASE WHEN D8 LIKE '%VA%' THEN 1 ELSE 0 END +
							CASE WHEN D9 LIKE '%VA%' THEN 1 ELSE 0 END +
							CASE WHEN D10 LIKE '%VA%' THEN 1 ELSE 0 END +
							CASE WHEN D11 LIKE '%VA%' THEN 1 ELSE 0 END +
							CASE WHEN D12 LIKE '%VA%' THEN 1 ELSE 0 END +
							CASE WHEN D13 LIKE '%VA%' THEN 1 ELSE 0 END +
							CASE WHEN D14 LIKE '%VA%' THEN 1 ELSE 0 END +
							CASE WHEN D15 LIKE '%VA%' THEN 1 ELSE 0 END +
							CASE WHEN D16 LIKE '%VA%' THEN 1 ELSE 0 END +
							CASE WHEN D17 LIKE '%VA%' THEN 1 ELSE 0 END +
							CASE WHEN D18 LIKE '%VA%' THEN 1 ELSE 0 END +
							CASE WHEN D19 LIKE '%VA%' THEN 1 ELSE 0 END +
							CASE WHEN D20 LIKE '%VA%' THEN 1 ELSE 0 END +
							CASE WHEN D21 LIKE '%VA%' THEN 1 ELSE 0 END +
							CASE WHEN D22 LIKE '%VA%' THEN 1 ELSE 0 END +
							CASE WHEN D23 LIKE '%VA%' THEN 1 ELSE 0 END +
							CASE WHEN D24 LIKE '%VA%' THEN 1 ELSE 0 END +
							CASE WHEN D25 LIKE '%VA%' THEN 1 ELSE 0 END +
							CASE WHEN D26 LIKE '%VA%' THEN 1 ELSE 0 END +
							CASE WHEN D27 LIKE '%VA%' THEN 1 ELSE 0 END +
							CASE WHEN D28 LIKE '%VA%' THEN 1 ELSE 0 END +
							CASE WHEN D29 LIKE '%VA%' THEN 1 ELSE 0 END +
							CASE WHEN D30 LIKE '%VA%' THEN 1 ELSE 0 END +
							CASE WHEN D31 LIKE '%VA%' THEN 1 ELSE 0 END
							FROM ReportesApp_Operaciones_ProgramacionVC_PersonalView C2 WHERE C2.IDPersona = T.IDPersona
							AND C2.Mes = @Mes AND C2.Anio = @Anio),
	T.CompensacionProg = (SELECT COUNT(NumeroDias) FROM ReportesApp_Operaciones_ProgramacionVC_Registros C2 WHERE C2.IDPersona = T.IDPersona AND
						  MONTH(C2.FechaFin) = @Mes AND YEAR(C2.FechaFin) = @Anio AND C2.Codigo LIKE '%CO%')
	FROM @T_Prueba T
	LEFT JOIN ReportesApp_Operaciones_ProgramacionVC_Registros C ON C.IDPersona = T.IDPersona AND CONVERT(INT,T.Mes) = @Mes AND CONVERT(INT,T.Anio) = @Anio
	WHERE C.IDPersona = T.IDPersona AND CONVERT(INT,T.Mes) = @Mes AND CONVERT(INT,T.Anio) = @Anio AND T.Nombre NOT IN ('<TRABAJADOR>')

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
		WHERE Nombre='<TRABAJADOR>'

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

		D2 = CASE WHEN @NroDia = 2 THEN 
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
		LEFT JOIN ReportesApp_RRHH_AsistenciaView A WITH(NOLOCK) ON T.IDPersona = A.IdPersona
		WHERE A.Anio = YEAR(@FechaVer) AND A.Mes = MONTH(@FechaVer) 

		SET @FechaVer = DATEADD(D,1,@FechaVer)
			
		IF (@FDesde IS NOT NULL) AND (@NroDia = @CantDias OR @CantDias = 0)  BREAK;
		
		SET @NroDia = @NroDia + 1
	END

	UPDATE P 
	SET P.D1 = CASE WHEN O.D1 IS NULL THEN P.D1 ELSE O.D1 END,
		P.D2 = CASE WHEN O.D2 IS NULL THEN P.D2 ELSE O.D2 END,
		P.D3 = CASE WHEN O.D3 IS NULL THEN P.D3 ELSE O.D3 END,
		P.D4 = CASE WHEN O.D4 IS NULL THEN P.D4 ELSE O.D4 END,
		P.D5 = CASE WHEN O.D5 IS NULL THEN P.D5 ELSE O.D5 END,
		P.D6 = CASE WHEN O.D6 IS NULL THEN P.D6 ELSE O.D6 END,
		P.D7 = CASE WHEN O.D7 IS NULL THEN P.D7 ELSE O.D7 END,
		P.D8 = CASE WHEN O.D8 IS NULL THEN P.D8 ELSE O.D8 END,
		P.D9 = CASE WHEN O.D9 IS NULL THEN P.D9 ELSE O.D9 END,
		P.D10 = CASE WHEN O.D10 IS NULL THEN P.D10 ELSE O.D10 END,
		P.D11 = CASE WHEN O.D11 IS NULL THEN P.D11 ELSE O.D11 END,
		P.D12 = CASE WHEN O.D12 IS NULL THEN P.D12 ELSE O.D12 END,
		P.D13 = CASE WHEN O.D13 IS NULL THEN P.D13 ELSE O.D13 END,
		P.D14 = CASE WHEN O.D14 IS NULL THEN P.D14 ELSE O.D14 END,
		P.D15 = CASE WHEN O.D15 IS NULL THEN P.D15 ELSE O.D15 END,
		P.D16 = CASE WHEN O.D16 IS NULL THEN P.D16 ELSE O.D16 END,
		P.D17 = CASE WHEN O.D17 IS NULL THEN P.D17 ELSE O.D17 END,
		P.D18 = CASE WHEN O.D18 IS NULL THEN P.D18 ELSE O.D18 END,
		P.D19 = CASE WHEN O.D19 IS NULL THEN P.D19 ELSE O.D19 END,
		P.D20 = CASE WHEN O.D20 IS NULL THEN P.D20 ELSE O.D20 END,
		P.D21 = CASE WHEN O.D21 IS NULL THEN P.D21 ELSE O.D21 END,
		P.D22 = CASE WHEN O.D22 IS NULL THEN P.D22 ELSE O.D22 END,
		P.D23 = CASE WHEN O.D23 IS NULL THEN P.D23 ELSE O.D23 END,
		P.D24 = CASE WHEN O.D24 IS NULL THEN P.D24 ELSE O.D24 END,
		P.D25 = CASE WHEN O.D25 IS NULL THEN P.D25 ELSE O.D25 END,
		P.D26 = CASE WHEN O.D26 IS NULL THEN P.D26 ELSE O.D26 END,
		P.D27 = CASE WHEN O.D27 IS NULL THEN P.D27 ELSE O.D27 END,
		P.D28 = CASE WHEN O.D28 IS NULL THEN P.D28 ELSE O.D28 END,
		P.D29 = CASE WHEN O.D29 IS NULL THEN P.D29 ELSE O.D29 END,
		P.D30 = CASE WHEN O.D30 IS NULL THEN P.D30 ELSE O.D30 END,
		P.D31 = CASE WHEN O.D31 IS NULL THEN P.D31 ELSE O.D31 END
	FROM @T_Prueba P
	LEFT JOIN ReportesApp_Operaciones_ProgramacionVC_PersonalView O
	ON P.IDPersona = O.IDPersona AND O.Anio = RIGHT(@Periodo,4) AND O.Mes = LEFT(@periodo,2)
	WHERE P.IDPersona = O.IDPersona AND O.Anio = RIGHT(@Periodo,4) AND O.Mes = LEFT(@periodo,2) AND P.Nombre NOT IN ('<TRABAJADOR>')

	IF (@CantDias = 28) BEGIN
		SELECT @D1=D1, @D2=D2, @D3=D3, @D4=D4, @D5=D5, @D6=D6, @D7=D7, @D8=D8, @D9=D9, @D10=D10, @D11=D11, @D12=D12, @D13=D13, @D14=D14,
		@D15=D15, @D16=D16, @D17=D17, @D18=D18, @D19=D19, @D20=D20, @D21=D21, @D22=D22, @D23=D23, @D24=D24, @D25=D25, @D26=D26, @D27=D27, @D28=D28
		FROM @T_Prueba WHERE Nombre = '<TRABAJADOR>'
	END

	IF (@CantDias = 29) BEGIN
		SELECT @D1=D1, @D2=D2, @D3=D3, @D4=D4, @D5=D5, @D6=D6, @D7=D7, @D8=D8, @D9=D9, @D10=D10, @D11=D11, @D12=D12, @D13=D13, @D14=D14, @D15=D15,
		@D16=D16, @D17=D17, @D18=D18, @D19=D19, @D20=D20, @D21=D21, @D22=D22, @D23=D23, @D24=D24, @D25=D25, @D26=D26, @D27=D27, @D28=D28, @D29=D29
		FROM @T_Prueba WHERE Nombre = '<TRABAJADOR>'
	END

	IF (@CantDias = 30) BEGIN
		SELECT @D1=D1, @D2=D2, @D3=D3, @D4=D4, @D5=D5, @D6=D6, @D7=D7, @D8=D8, @D9=D9, @D10=D10, @D11=D11, @D12=D12, @D13=D13, @D14=D14, @D15=D15,
		@D16=D16, @D17=D17, @D18=D18, @D19=D19, @D20=D20, @D21=D21, @D22=D22, @D23=D23, @D24=D24, @D25=D25, @D26=D26, @D27=D27, @D28=D28, @D29=D29, @D30=D30
		FROM @T_Prueba WHERE Nombre = '<TRABAJADOR>'
	END

	IF (@CantDias = 31) BEGIN
		SELECT @D1=D1, @D2=D2, @D3=D3, @D4=D4, @D5=D5, @D6=D6, @D7=D7, @D8=D8, @D9=D9, @D10=D10, @D11=D11, @D12=D12, @D13=D13, @D14=D14, @D15=D15, @D16=D16, @D17=D17,
		@D18=D18, @D19=D19, @D20=D20, @D21=D21, @D22=D22, @D23=D23, @D24=D24, @D25=D25, @D26=D26, @D27=D27, @D28=D28, @D29=D29, @D30=D30, @D29=D29, @D30=D30, @D31=D31
		FROM @T_Prueba WHERE Nombre = '<TRABAJADOR>'
	END

	CREATE TABLE #PruebaView (IDPersona INT, Nombre VARCHAR(300), Mes VARCHAR(15), Anio VARCHAR(15), FechaIngreso VARCHAR(100), Operacion VARCHAR(100),
							  VacacionesProg VARCHAR(50), VacacionesPend VARCHAR(50), CompensacionProg VARCHAR(50), CompensacionPend VARCHAR(50),
							  D1 VARCHAR(15), D2 VARCHAR(15), D3 VARCHAR(15), D4 VARCHAR(15), D5 VARCHAR(15), D6 VARCHAR(15), D7 VARCHAR(15),
							  D8 VARCHAR(15), D9 VARCHAR(15), D10 VARCHAR(15), D11 VARCHAR(15), D12 VARCHAR(15), D13 VARCHAR(15), D14 VARCHAR(15),
							  D15 VARCHAR(15), D16 VARCHAR(15), D17 VARCHAR(15), D18 VARCHAR(15), D19 VARCHAR(15), D20 VARCHAR(15), D21 VARCHAR(15),
							  D22 VARCHAR(15), D23 VARCHAR(15), D24 VARCHAR(15), D25 VARCHAR(15), D26 VARCHAR(15), D27 VARCHAR(15), D28 VARCHAR(15),
							  D29 VARCHAR(15), D30 VARCHAR(15), D31 VARCHAR(15))

	DECLARE @SQL varchar(5000)

	INSERT INTO #PruebaView
	SELECT * FROM @T_Prueba WHERE Nombre <> '<TRABAJADOR>'

	IF @CantDias = 28 BEGIN
		SET @SQL='SELECT IDPersona, Nombre AS CONDUCTOR, FechaIngreso AS FECHA_INGRESO, Operacion AS PROGRAMACION, ISNULL(VacacionesProg,0) AS VAC_PROG,
			ISNULL(VacacionesPend,0) AS VAC_PEND, ISNULL(CompensacionProg,0) AS COMP_PROG, ISNULL(CompensacionPend,0) AS COMP_PEND, D1 AS '+@D1+
			',D2 AS '+@D2+',D3 AS '+@D3+',D4 AS '+@D4+',D5 AS '+@D5+',D6 AS '+@D6+',D7 AS '+@D7+',D8 AS '+@D8+',D9 AS '+@D9+',D10 AS '+@D10+
			',D11 AS '+@D11+',D12 AS '+@D12+',D13 AS '+@D13+',D14 AS '+@D14+',D15 AS '+@D15+',D16 AS '+@D16+',D17 AS '+@D17+',D18 AS '+@D18+
			',D19 AS '+@D19+',D20 AS '+@D20+',D21 AS '+@D21+',D22 AS '+@D22+',D23 AS '+@D23+',D24 AS '+@D24+',D25 AS '+@D25+',D26 AS '+@D26+
			',D27 AS '+@D27+',D28 AS '+@D28+' FROM #PruebaView ORDER BY Nombre'
		
		EXEC (@SQL)
		DROP TABLE #PruebaView
	END

	IF @CantDias = 29 BEGIN
		SET @SQL='SELECT IDPersona, Nombre AS CONDUCTOR, FechaIngreso AS FECHA_INGRESO, Operacion AS PROGRAMACION, ISNULL(VacacionesProg,0) AS VAC_PROG,
			ISNULL(VacacionesPend,0) AS VAC_PEND, ISNULL(CompensacionProg,0) AS COMP_PROG, ISNULL(CompensacionPend,0) AS COMP_PEND, D1 AS '+@D1+
			',D2 AS '+@D2+',D3 AS '+@D3+',D4 AS '+@D4+',D5 AS '+@D5+',D6 AS '+@D6+',D7 AS '+@D7+',D8 AS '+@D8+',D9 AS '+@D9+',D10 AS '+@D10+
			',D11 AS '+@D11+',D12 AS '+@D12+',D13 AS '+@D13+',D14 AS '+@D14+',D15 AS '+@D15+',D16 AS '+@D16+',D17 AS '+@D17+',D18 AS '+@D18+
			',D19 AS '+@D19+',D20 AS '+@D20+',D21 AS '+@D21+',D22 AS '+@D22+',D23 AS '+@D23+',D24 AS '+@D24+',D25 AS '+@D25+',D26 AS '+@D26+
			',D27 AS '+@D27+',D28 AS '+@D28+',D29 AS '+@D29+' FROM #PruebaView ORDER BY Nombre'
		
		EXEC (@SQL)
		DROP TABLE #PruebaView	
	END

	IF @CantDias = 30 BEGIN
		SET @SQL='SELECT IDPersona, Nombre AS CONDUCTOR, FechaIngreso AS FECHA_INGRESO, Operacion AS PROGRAMACION, ISNULL(VacacionesProg,0) AS VAC_PROG,
			ISNULL(VacacionesPend,0) AS VAC_PEND, ISNULL(CompensacionProg,0) AS COMP_PROG, ISNULL(CompensacionPend,0) AS COMP_PEND, D1 AS '+@D1+
			',D2 AS '+@D2+',D3 AS '+@D3+',D4 AS '+@D4+',D5 AS '+@D5+',D6 AS '+@D6+',D7 AS '+@D7+',D8 AS '+@D8+',D9 AS '+@D9+',D10 AS '+@D10+
			',D11 AS '+@D11+',D12 AS '+@D12+',D13 AS '+@D13+',D14 AS '+@D14+',D15 AS '+@D15+',D16 AS '+@D16+',D17 AS '+@D17+',D18 AS '+@D18+
			',D19 AS '+@D19+',D20 AS '+@D20+',D21 AS '+@D21+',D22 AS '+@D22+',D23 AS '+@D23+',D24 AS '+@D24+',D25 AS '+@D25+',D26 AS '+@D26+
			',D27 AS '+@D27+',D28 AS '+@D28+',D29 AS '+@D29+',D30 AS '+@D30+' FROM #PruebaView ORDER BY Nombre'
		
		EXEC (@SQL)
		DROP TABLE #PruebaView
	END

	IF @CantDias = 31 BEGIN
		SET @SQL='SELECT IDPersona, Nombre AS CONDUCTOR, FechaIngreso AS FECHA_INGRESO, Operacion AS PROGRAMACION, ISNULL(VacacionesProg,0) AS VAC_PROG,
			ISNULL(VacacionesPend,0) AS VAC_PEND, ISNULL(CompensacionProg,0) AS COMP_PROG, ISNULL(CompensacionPend,0) AS COMP_PEND, D1 AS '+@D1+
			',D2 AS '+@D2+',D3 AS '+@D3+',D4 AS '+@D4+',D5 AS '+@D5+',D6 AS '+@D6+',D7 AS '+@D7+',D8 AS '+@D8+',D9 AS '+@D9+',D10 AS '+@D10+
			',D11 AS '+@D11+',D12 AS '+@D12+',D13 AS '+@D13+',D14 AS '+@D14+',D15 AS '+@D15+',D16 AS '+@D16+',D17 AS '+@D17+',D18 AS '+@D18+
			',D19 AS '+@D19+',D20 AS '+@D20+',D21 AS '+@D21+',D22 AS '+@D22+',D23 AS '+@D23+',D24 AS '+@D24+',D25 AS '+@D25+',D26 AS '+@D26+
			',D27 AS '+@D27+',D28 AS '+@D28+',D29 AS '+@D29+',D30 AS '+@D30+',D31 AS '+@D31+' FROM #PruebaView ORDER BY Nombre'
		
		EXEC (@SQL)
		DROP TABLE #PruebaView
	END
END

-------------------------------------------------------------------------------------------------------------------
/*
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 04/06/2024
-- Description:	REGISTRAR PROGRAMACION VACACIONES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ProgramacionVC_RegistrarProgramacionVC]
@Periodo VARCHAR(6),
@xmlProgramacion VARCHAR(MAX),
@Codigo VARCHAR(20),
@Usuario VARCHAR(20)
AS
DECLARE @exito VARCHAR(MAX)
DECLARE @correlativo INT, @co2 INT
DECLARE @TEMP_ASIS TABLE (Contador INT, IDPersona INT, Fecha DATE, Codigo VARCHAR(20), Pendiente INT, Programada INT)
DECLARE @idact INT, @Nro INT

SET @exito = '0 = Programación de Conductor Registrada.'

SET @correlativo = (SELECT MAX(Contador) FROM @TEMP_ASIS)
SET @correlativo = ISNULL(@correlativo,0) + 1 
SET @Nro = 1

BEGIN TRAN
BEGIN TRY
	IF @xmlProgramacion IS NOT NULL BEGIN  
		EXEC sp_xml_preparedocument @idact OUT, @xmlProgramacion  

		INSERT INTO @TEMP_ASIS(Contador, IDPersona, Fecha, Codigo, Pendiente, Programada)  
		SELECT ROW_NUMBER() OVER(ORDER BY @correlativo ASC), IDPersona, Fecha, @Codigo, Pendiente, Programada
		FROM OPENXML(@idact,'/r/d',3) WITH(IDPersona INT, Fecha DATE, Pendiente INT, Programada INT)  
		     
		EXEC sp_xml_removedocument @idact  
	END

	WHILE (@Nro <= (SELECT MAX(Contador) FROM @TEMP_ASIS)) BEGIN
		SET @co2 = (SELECT MAX(idProgVC) FROM ReportesApp_Operaciones_ProgramacionVC_Registros)
		SET @co2 = ISNULL(@co2,0) + 1 

		DECLARE @Pend INT = (SELECT Pendiente FROM @TEMP_ASIS WHERE Contador = @Nro)
		DECLARE @Prog INT = (SELECT Programada FROM @TEMP_ASIS WHERE Contador = @Nro)

		IF (@Prog < @Pend) BEGIN
			IF (NOT EXISTS(SELECT * FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE IDPersona = (SELECT IDPersona FROM @TEMP_ASIS WHERE Contador = @Nro)
			AND Fecha = (SELECT Fecha FROM @TEMP_ASIS WHERE Contador = @Nro))) BEGIN
				INSERT INTO ReportesApp_Operaciones_ProgramacionVC_Registros(idProgVC,IDPersona,Fecha,Codigo,Aprobado,UsuarioRegistra,FechaRegistra,UsuarioAprueba,FechaAprueba)
				SELECT @co2, P.IDPersona, P.Fecha, P.Codigo, 0, @Usuario, GETDATE(), @Usuario, GETDATE()
				FROM @TEMP_ASIS P WHERE P.Contador = @Nro
			END
			ELSE BEGIN
				IF ((SELECT Aprobado FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE IDPersona = (SELECT IDPersona FROM @TEMP_ASIS WHERE Contador = @Nro)
				AND Fecha = (SELECT Fecha FROM @TEMP_ASIS WHERE Contador = @Nro)) = 0) BEGIN 
					UPDATE ReportesApp_Operaciones_ProgramacionVC_Registros
					SET Codigo = @Codigo, UsuarioRegistra = @Usuario, FechaRegistra = GETDATE(), UsuarioAprueba = @Usuario, FechaAprueba = GETDATE()
					WHERE IDPersona = (SELECT IDPersona FROM @TEMP_ASIS WHERE Contador = @Nro) AND Fecha = (SELECT Fecha FROM @TEMP_ASIS WHERE Contador = @Nro)
				END
			END

			IF (NOT EXISTS(SELECT * FROM ReportesApp_Operaciones_ProgramacionVC_PersonalView WHERE IDPersona = (SELECT IDPersona FROM @TEMP_ASIS WHERE Contador = @Nro)
			AND Anio = (SELECT YEAR(Fecha) FROM @TEMP_ASIS WHERE Contador = @Nro) AND Mes = (SELECT MONTH(Fecha) FROM @TEMP_ASIS WHERE Contador = @Nro))) BEGIN
				DECLARE @Dia DATE = RIGHT(@Periodo,4)+'-'+LEFT(@Periodo,2)+'-01'
				DECLARE @FechaFin DATE = CAST(DATEADD(MONTH,DATEDIFF(MONTH,0,@Dia)+1,0)-1 AS DATE)
				DECLARE @UltimoDiaMes TINYINT = DAY(@fechafin)

				INSERT INTO ReportesApp_Operaciones_ProgramacionVC_PersonalView (IDPersona,Anio,Mes,NroDias)
				SELECT P.IDPersona, RIGHT(@Periodo,4), LEFT(@Periodo,2), @UltimoDiaMes
				FROM @TEMP_ASIS P WHERE P.Contador = @Nro
			END

			IF ((SELECT Aprobado FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE IDPersona = (SELECT IDPersona FROM @TEMP_ASIS WHERE Contador = @Nro)
				AND Fecha = (SELECT Fecha FROM @TEMP_ASIS WHERE Contador = @Nro)) = 0) BEGIN 
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
				FROM ReportesApp_Operaciones_ProgramacionVC_PersonalView P
				LEFT JOIN @TEMP_ASIS O ON P.IDPersona = O.IDPersona AND O.Fecha = @x_FechaSelec
				WHERE P.Anio = RIGHT(@Periodo,4) AND P.Mes = LEFT(@periodo,2)
			END
		END
		ELSE BEGIN
			SET @exito = '-1 = No puede programar a este conductor porque no tiene días pendientes.'
			ROLLBACK
			GOTO Terminar
		END

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

--------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 05/06/2024
-- Description:	ELIMINAR PROGRAMACION VACACIONES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ProgramacionVC_EliminarProgramacionVC]
@Periodo VARCHAR(6),
@xmlProgramacion VARCHAR(MAX),
@Usuario VARCHAR(20)
AS
DECLARE @exito VARCHAR(MAX)
DECLARE @correlativo INT, @co2 INT
DECLARE @TEMP_ASIS TABLE (Contador INT, IDPersona INT, Fecha DATE)
DECLARE @idact INT, @Nro INT

SET @exito = '0 = Programación de Conductor Eliminada.'

SET @correlativo = (SELECT MAX(Contador) FROM @TEMP_ASIS)
SET @correlativo = ISNULL(@correlativo,0) + 1 
SET @Nro = 1

BEGIN TRAN
BEGIN TRY
	IF @xmlProgramacion IS NOT NULL BEGIN  
		EXEC sp_xml_preparedocument @idact OUT, @xmlProgramacion  

		INSERT INTO @TEMP_ASIS(Contador, IDPersona, Fecha)  
		SELECT ROW_NUMBER() OVER(ORDER BY @correlativo ASC), IDPersona, Fecha
		FROM OPENXML(@idact,'/r/d',3) WITH(IDPersona INT, Fecha DATE)  
		     
		EXEC sp_xml_removedocument @idact  
	END

	WHILE (@Nro <= (SELECT MAX(Contador) FROM @TEMP_ASIS)) BEGIN
		IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE IDPersona = (SELECT IDPersona FROM @TEMP_ASIS WHERE Contador = @Nro)
		AND Fecha = (SELECT Fecha FROM @TEMP_ASIS WHERE Contador = @Nro) AND Aprobado = 0)) BEGIN
			DELETE FROM ReportesApp_Operaciones_ProgramacionVC_Registros
			WHERE IDPersona = (SELECT IDPersona FROM @TEMP_ASIS WHERE Contador = @Nro) AND Fecha = (SELECT Fecha FROM @TEMP_ASIS WHERE Contador = @Nro)
			AND Aprobado = 0

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
			FROM ReportesApp_Operaciones_ProgramacionVC_PersonalView P
			LEFT JOIN @TEMP_ASIS O ON P.IDPersona = O.IDPersona AND O.Fecha = @x_FechaSelec
			WHERE P.Anio = RIGHT(@Periodo,4) AND P.Mes = LEFT(@periodo,2)
		END

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

-------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 05/06/2024
-- Description:	APROBAR PROGRAMACION VACACIONES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ProgramacionVC_AprobarProgramacionVC]
@Periodo VARCHAR(6),
@xmlProgramacion VARCHAR(MAX),
@Usuario VARCHAR(20)
AS
DECLARE @exito VARCHAR(MAX)
DECLARE @correlativo INT, @co2 INT
DECLARE @TEMP_ASIS TABLE (Contador INT, IDPersona INT, Fecha DATE)
DECLARE @idact INT, @Nro INT

SET @exito = '0 = Programación de Conductor Aprobada.'

SET @correlativo = (SELECT MAX(Contador) FROM @TEMP_ASIS)
SET @correlativo = ISNULL(@correlativo,0) + 1 
SET @Nro = 1

BEGIN TRAN
BEGIN TRY
	IF @xmlProgramacion IS NOT NULL BEGIN  
		EXEC sp_xml_preparedocument @idact OUT, @xmlProgramacion  

		INSERT INTO @TEMP_ASIS(Contador, IDPersona, Fecha)  
		SELECT ROW_NUMBER() OVER(ORDER BY @correlativo ASC), IDPersona, Fecha
		FROM OPENXML(@idact,'/r/d',3) WITH(IDPersona INT, Fecha DATE)  
		     
		EXEC sp_xml_removedocument @idact  
	END

	WHILE (@Nro <= (SELECT MAX(Contador) FROM @TEMP_ASIS)) BEGIN
		IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ProgramacionVC_Registros WHERE IDPersona = (SELECT IDPersona FROM @TEMP_ASIS WHERE Contador = @Nro)
		AND Fecha = (SELECT Fecha FROM @TEMP_ASIS WHERE Contador = @Nro) AND Aprobado = 0)) BEGIN
			DECLARE @Codigo VARCHAR(20) = (SELECT SUBSTRING(Codigo,0,3) FROM ReportesApp_Operaciones_ProgramacionVC_Registros
			WHERE IDPersona = (SELECT IDPersona FROM @TEMP_ASIS WHERE Contador = @Nro) AND Fecha = (SELECT Fecha FROM @TEMP_ASIS WHERE Contador = @Nro)
			AND Aprobado = 0)
			
			UPDATE ReportesApp_Operaciones_ProgramacionVC_Registros
			SET Codigo = @Codigo + ' (AP)', Aprobado = 1, UsuarioAprueba = @Usuario, FechaAprueba = GETDATE()
			WHERE IDPersona = (SELECT IDPersona FROM @TEMP_ASIS WHERE Contador = @Nro) AND Fecha = (SELECT Fecha FROM @TEMP_ASIS WHERE Contador = @Nro)
			AND Aprobado = 0

			DECLARE @x_FechaSelec DATE = (SELECT Fecha FROM @TEMP_ASIS WHERE Contador = @Nro)

			UPDATE P 
			SET P.D1 = CASE WHEN DAY(O.Fecha) = 1 THEN @Codigo + ' (AP)' ELSE P.D1 END,
				P.D2 = CASE WHEN DAY(O.Fecha) = 2 THEN @Codigo + ' (AP)' ELSE P.D2 END,
				P.D3 = CASE WHEN DAY(O.Fecha) = 3 THEN @Codigo + ' (AP)' ELSE P.D3 END,
				P.D4 = CASE WHEN DAY(O.Fecha) = 4 THEN @Codigo + ' (AP)' ELSE P.D4 END,
				P.D5 = CASE WHEN DAY(O.Fecha) = 5 THEN @Codigo + ' (AP)' ELSE P.D5 END,
				P.D6 = CASE WHEN DAY(O.Fecha) = 6 THEN @Codigo + ' (AP)' ELSE P.D6 END,
				P.D7 = CASE WHEN DAY(O.Fecha) = 7 THEN @Codigo + ' (AP)' ELSE P.D7 END,
				P.D8 = CASE WHEN DAY(O.Fecha) = 8 THEN @Codigo + ' (AP)' ELSE P.D8 END,
				P.D9 = CASE WHEN DAY(O.Fecha) = 9 THEN @Codigo + ' (AP)' ELSE P.D9 END,
				P.D10 = CASE WHEN DAY(O.Fecha) = 10 THEN @Codigo + ' (AP)' ELSE P.D10 END,
				P.D11 = CASE WHEN DAY(O.Fecha) = 11 THEN @Codigo + ' (AP)' ELSE P.D11 END,
				P.D12 = CASE WHEN DAY(O.Fecha) = 12 THEN @Codigo + ' (AP)' ELSE P.D12 END,
				P.D13 = CASE WHEN DAY(O.Fecha) = 13 THEN @Codigo + ' (AP)' ELSE P.D13 END,
				P.D14 = CASE WHEN DAY(O.Fecha) = 14 THEN @Codigo + ' (AP)' ELSE P.D14 END,
				P.D15 = CASE WHEN DAY(O.Fecha) = 15 THEN @Codigo + ' (AP)' ELSE P.D15 END,
				P.D16 = CASE WHEN DAY(O.Fecha) = 16 THEN @Codigo + ' (AP)' ELSE P.D16 END,
				P.D17 = CASE WHEN DAY(O.Fecha) = 17 THEN @Codigo + ' (AP)' ELSE P.D17 END,
				P.D18 = CASE WHEN DAY(O.Fecha) = 18 THEN @Codigo + ' (AP)' ELSE P.D18 END,
				P.D19 = CASE WHEN DAY(O.Fecha) = 19 THEN @Codigo + ' (AP)' ELSE P.D19 END,
				P.D20 = CASE WHEN DAY(O.Fecha) = 20 THEN @Codigo + ' (AP)' ELSE P.D20 END,
				P.D21 = CASE WHEN DAY(O.Fecha) = 21 THEN @Codigo + ' (AP)' ELSE P.D21 END,
				P.D22 = CASE WHEN DAY(O.Fecha) = 22 THEN @Codigo + ' (AP)' ELSE P.D22 END,
				P.D23 = CASE WHEN DAY(O.Fecha) = 23 THEN @Codigo + ' (AP)' ELSE P.D23 END,
				P.D24 = CASE WHEN DAY(O.Fecha) = 24 THEN @Codigo + ' (AP)' ELSE P.D24 END,
				P.D25 = CASE WHEN DAY(O.Fecha) = 25 THEN @Codigo + ' (AP)' ELSE P.D25 END,
				P.D26 = CASE WHEN DAY(O.Fecha) = 26 THEN @Codigo + ' (AP)' ELSE P.D26 END,
				P.D27 = CASE WHEN DAY(O.Fecha) = 27 THEN @Codigo + ' (AP)' ELSE P.D27 END,
				P.D28 = CASE WHEN DAY(O.Fecha) = 28 THEN @Codigo + ' (AP)' ELSE P.D28 END,
				P.D29 = CASE WHEN DAY(O.Fecha) = 29 THEN @Codigo + ' (AP)' ELSE P.D29 END,
				P.D30 = CASE WHEN DAY(O.Fecha) = 30 THEN @Codigo + ' (AP)' ELSE P.D30 END,
				P.D31 = CASE WHEN DAY(O.Fecha) = 31 THEN @Codigo + ' (AP)' ELSE P.D31 END
			FROM ReportesApp_Operaciones_ProgramacionVC_PersonalView P
			LEFT JOIN @TEMP_ASIS O ON P.IDPersona = O.IDPersona AND O.Fecha = @x_FechaSelec
			WHERE P.Anio = RIGHT(@Periodo,4) AND P.Mes = LEFT(@periodo,2)
		END

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

-------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 06-06-2025
-- Description:	IMPRIMIR TICKET PROGRAMACIONES
-- =============================================
/*

*/
CREATE PROCEDURE [dbo].[ReportesApp_Operaciones_ProgramacionVC_ImprimirTicket]
@xmlProgramacion VARCHAR(MAX)
AS
DECLARE @idoc INT
DECLARE @correlativo INT
DECLARE @TEMP_IMPR TABLE (Contador INT, IDPersona INT, Fecha DATE)

SET @correlativo = (SELECT MAX(Contador) FROM @TEMP_IMPR)
SET @correlativo = ISNULL(@correlativo,0) + 1 

BEGIN
	IF (@xmlProgramacion IS NOT NULL) BEGIN
		EXEC sp_xml_preparedocument @idoc OUTPUT, @xmlProgramacion
		
		INSERT INTO @TEMP_IMPR(Contador, IDPersona, Fecha)  
		SELECT ROW_NUMBER() OVER(ORDER BY @correlativo ASC), IDPersona, Fecha
		FROM OPENXML(@idoc,'/r/d',3) WITH(IDPersona INT, Fecha DATE)  
		
		EXEC sp_xml_removedocument @idoc;
	END

	SELECT IM.Contador, RTRIM(P.NombreCompleto) AS 'NOMBRE', RTRIM(P.DocumentoIdentidad) AS 'DNI',
	CASE WHEN R.Codigo = 'CO (AP)' THEN 'COMPENSACION' ELSE 'VACACIONES' END AS 'TIPO', CONVERT(VARCHAR(100),R.Fecha,103) AS 'FECHA'
	FROM ReportesApp_Operaciones_ProgramacionVC_Registros R
	INNER JOIN @TEMP_IMPR IM ON IM.IDPersona = R.IDPersona AND IM.Fecha = R.Fecha
	LEFT JOIN PersonaMast P ON P.Persona = R.IDPersona
	ORDER BY R.Fecha ASC

	/*
	DECLARE @Contador INT = (SELECT MAX(idRegistroOT) FROM ReportesApp_Logistica_TransaccionesMtto_RegistroOT)
	SET @Contador = ISNULL(@Contador,0) + 1

	INSERT INTO ReportesApp_Logistica_TransaccionesMtto_RegistroOT (idRegistroOT, NumeroOrden, CodigoItem, DescripcionItem, Unidad, Cantidad, FechaImpresion, Estado)
	SELECT OTR.Secuencia, LTRIM(RTRIM(@NumeroOT)), LTRIM(RTRIM(OTR.Recurso)), LTRIM(RTRIM(OTR.Descripcion)), OTR.UnidadCodigo, OTR.CantidadPedida, GETDATE(), 'IMPRESO'
	FROM ME_OrdenTrabajoRecurso OTR
	INNER JOIN ReportesApp_Logistica_TransaccionesMtto_ItemsDivemotor ID ON LTRIM(RTRIM(ID.CodigoItem)) = LTRIM(RTRIM(OTR.Recurso))
	INNER JOIN @TEMP_ITEM IT ON IT.Nro = OTR.Secuencia AND LTRIM(RTRIM(IT.Codigo)) = LTRIM(RTRIM(OTR.Recurso))
	WHERE OTR.NumeroOrden = @NumeroOT AND OTR.CantidadPedida > 0

	SELECT ROW_NUMBER() OVER(ORDER BY OTR.Recurso) AS 'NRO', LTRIM(RTRIM(OTR.NumeroOrden)) AS 'ORDEN_TRABAJO', LTRIM(RTRIM(OT.Descripcion)) AS 'DESCRIPCION',	CONVERT(VARCHAR,OT.FechaProgramada,103) AS 'FECHA', LTRIM(RTRIM(AC.CostCenter))+' - '+LTRIM(RTRIM(AC.LocalName)) AS 'CENTRO_COSTO', P.InternalNumber AS 'UNIDAD',	LTRIM(RTRIM(OTR.Recurso)) AS 'CODIGO', OTR.UnidadCodigo AS 'UND', LTRIM(RTRIM(OTR.Descripcion)) AS 'ITEM', OTR.CantidadPedida AS 'CANTIDAD_PEDIDA'
	FROM ME_OrdenTrabajoRecurso OTR
	INNER JOIN ME_OrdenTrabajo OT ON (OTR.CompaniaSocio = OT.CompaniaSocio AND OTR.NumeroOrden = OT.NumeroOrden)
	INNER JOIN ReportesApp_Logistica_TransaccionesMtto_ItemsDivemotor ID ON LTRIM(RTRIM(ID.CodigoItem)) = LTRIM(RTRIM(OTR.Recurso))
	LEFT JOIN afemst P ON (OT.Proyecto = P.afe)	LEFT JOIN AC_CostCenterMst AC ON (AC.CostCenter = OT.CentroCostos)	INNER JOIN ME_Recurso R ON (OTR.Recurso = R.Recurso)	INNER JOIN WH_ItemMast I ON (OTR.Recurso = I.Item)
	INNER JOIN @TEMP_ITEM IT ON IT.Nro = OTR.Secuencia AND LTRIM(RTRIM(IT.Codigo)) = LTRIM(RTRIM(OTR.Recurso))
	WHERE OTR.NumeroOrden = @NumeroOT AND OTR.CantidadPedida > 0
	*/
END
*/