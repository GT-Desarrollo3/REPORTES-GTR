
-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_ReporteCumplimiento

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cumplimiento

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto_Cumplimiento

------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 07-08-2024
-- Description:	GENERAR CUMPLIMIENTO - ACTIVIDADES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_GenerarCumpActividad]
@Placa VARCHAR(50),
@Operacion VARCHAR(50),
@TipoUnidad VARCHAR(250),
@Actividad VARCHAR(350),
@ProxFecha DATE,
@FCInicio DATE,
@FCFin DATE
AS
DECLARE @Contador INT 
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (EXISTS(SELECT TOP(1) * FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto_Cumplimiento WHERE Placa = @Placa AND FCInicio = @FCInicio AND FCFin = @FCFin ORDER BY Nro DESC)) BEGIN
		SET @Exito = '-1 = La unidad '+@Placa+' ya tiene un cumplimiento registrado en esta semana.'
		ROLLBACK
		GOTO Terminar
	END

	SET @Contador = (SELECT MAX(Nro) FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto_Cumplimiento)
	SET @Contador = ISNULL(@Contador,0) + 1

	IF (EXISTS(SELECT TOP(1) * FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto_Cumplimiento WHERE Placa = @Placa AND Estado IN ('PROGRAMADO','REPROGRAMADO') ORDER BY Nro DESC)) BEGIN
		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto_Cumplimiento
		SET Observacion = 'NO INGRESÓ POR OPERACIÓN'
		WHERE Nro = (SELECT TOP(1) Nro FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto_Cumplimiento WHERE Placa = @Placa AND Estado IN ('PROGRAMADO','REPROGRAMADO') ORDER BY Nro DESC)
	
		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto_Cumplimiento(Nro,Placa,Operacion,TipoUnidad,Actividad,ProxFecha,FCInicio,FCFin,Estado)
		VALUES(@Contador,@Placa,@Operacion,@TipoUnidad,@Actividad,@ProxFecha,@FCInicio,@FCFin,'REPROGRAMADO')
	END
	ELSE BEGIN
		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto_Cumplimiento(Nro,Placa,Operacion,TipoUnidad,Actividad,ProxFecha,FCInicio,FCFin,Estado)
		VALUES(@Contador,@Placa,@Operacion,@TipoUnidad,@Actividad,@ProxFecha,@FCInicio,@FCFin,'PROGRAMADO')
	END

	DECLARE @NroSemana INT = (SELECT CASE WHEN DATEPART(W,@FCInicio) IN (6,7) THEN DATEPART(WW,DATEADD(DAY,7,@FCInicio)) ELSE DATEPART(WW,@FCInicio) END)

	UPDATE ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto_Cumplimiento
	SET NroSemana = @NroSemana
	WHERE Nro = @Contador

	UPDATE IC
	SET IC.SAB = CASE WHEN DATEPART(W,@ProxFecha) = 6 AND @ProxFecha >= @FCInicio AND @ProxFecha <= @FCFin THEN 'X' ELSE IC.SAB END,
		IC.DOM = CASE WHEN DATEPART(W,@ProxFecha) = 7 AND @ProxFecha >= @FCInicio AND @ProxFecha <= @FCFin THEN 'X' ELSE IC.DOM END,
		IC.LUN = CASE WHEN DATEPART(W,@ProxFecha) = 1 AND @ProxFecha >= @FCInicio AND @ProxFecha <= @FCFin THEN 'X' ELSE IC.LUN END,
		IC.MAR = CASE WHEN DATEPART(W,@ProxFecha) = 2 AND @ProxFecha >= @FCInicio AND @ProxFecha <= @FCFin THEN 'X' ELSE IC.MAR END,
		IC.MIE = CASE WHEN DATEPART(W,@ProxFecha) = 3 AND @ProxFecha >= @FCInicio AND @ProxFecha <= @FCFin THEN 'X' ELSE IC.MIE END,
		IC.JUE = CASE WHEN DATEPART(W,@ProxFecha) = 4 AND @ProxFecha >= @FCInicio AND @ProxFecha <= @FCFin THEN 'X' ELSE IC.JUE END,
		IC.VIE = CASE WHEN DATEPART(W,@ProxFecha) = 5 AND @ProxFecha >= @FCInicio AND @ProxFecha <= @FCFin THEN 'X' ELSE IC.VIE END
	FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto_Cumplimiento IC
	WHERE IC.Nro = @Contador

	SET @Exito = '0 = El cumplimiento semanal ha sido generado exitosamente.'
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

------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 07-08-2025
-- Description:	LISTAR CUMPLIMIENTO ACTIVIDADES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarCumpActividad]
@Anio INT,
@NroSemana INT
AS
BEGIN
	DECLARE @T_Prueba TABLE (Nro VARCHAR(20), Placa VARCHAR(50), Operacion VARCHAR(50), TipoUnidad VARCHAR(250), Actividad VARCHAR(350),
							 ProxFecha VARCHAR(70), FCInicio VARCHAR(70), FCFin VARCHAR(70), NroSemana VARCHAR(10), FechaCumplimiento VARCHAR(70),
							 Estado VARCHAR(30), Observacion VARCHAR(250), FechaIngreso VARCHAR(70), ObservacionOP VARCHAR(250), SAB VARCHAR(15),
							 DOM VARCHAR(15), LUN VARCHAR(15), MAR VARCHAR(15), MIE VARCHAR(15), JUE VARCHAR(15), VIE VARCHAR(15))
	
	DECLARE @SAB VARCHAR(15), @DOM VARCHAR(15), @LUN VARCHAR(15), @MAR VARCHAR(15), @MIE VARCHAR(15), @JUE VARCHAR(15), @VIE VARCHAR(15)

	INSERT INTO @T_Prueba (Nro,Placa,Operacion,TipoUnidad,Actividad,ProxFecha,FCInicio,FCFin,NroSemana,FechaCumplimiento,Estado,Observacion,FechaIngreso, ObservacionOP)
	VALUES ('Nro','<PLACA>','Operacion','TipoUnidad','Actividad','ProxFecha','FCInicio','FCFin','NroSemana','FechaCumplimiento','Estado','Observacion','FechaIngreso','ObservacionOP')
	
	INSERT INTO @T_Prueba (Nro,Placa,Operacion,TipoUnidad,Actividad,ProxFecha,FCInicio,FCFin,NroSemana,FechaCumplimiento,Estado,Observacion,FechaIngreso,
	ObservacionOP,SAB,DOM,LUN,MAR,MIE,JUE,VIE)
	SELECT CONVERT(VARCHAR,Nro), Placa, Operacion, TipoUnidad, Actividad, CONVERT(VARCHAR,ProxFecha,103), CONVERT(VARCHAR,FCInicio,103), CONVERT(VARCHAR,FCFin,103),
	CONVERT(VARCHAR,NroSemana), CONVERT(VARCHAR,FechaCumplimiento,103), Estado, Observacion, CONVERT(VARCHAR,FechaIngreso,103), ObservacionOP, SAB, DOM, LUN, MAR, MIE, JUE, VIE
	FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto_Cumplimiento
	WHERE YEAR(FCFin) = @Anio AND NroSemana = @NroSemana

	DECLARE @FCInicio DATE = (SELECT TOP(1) FCInicio FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto_Cumplimiento WHERE YEAR(FCFin) = @Anio AND NroSemana = @NroSemana)
	DECLARE @FCFin DATE = (SELECT TOP(1) FCFin FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto_Cumplimiento WHERE YEAR(FCFin) = @Anio AND NroSemana = @NroSemana)

	WHILE (@FCInicio <= @FCFin) BEGIN
		DECLARE @Concatenado VARCHAR(15) = LEFT(UPPER(DATENAME(WEEKDAY, @FCInicio)), 3) + ''  + RIGHT( '0' + CAST(DAY(@FCInicio) AS VARCHAR(2)),2)

		UPDATE @T_Prueba
		SET SAB = CASE WHEN DATEPART(W,@FCInicio) = 6 THEN @Concatenado ELSE SAB END,
			DOM = CASE WHEN DATEPART(W,@FCInicio) = 7 THEN @Concatenado ELSE DOM END,
			LUN = CASE WHEN DATEPART(W,@FCInicio) = 1 THEN @Concatenado ELSE LUN END,
			MAR = CASE WHEN DATEPART(W,@FCInicio) = 2 THEN @Concatenado ELSE MAR END,
			MIE = CASE WHEN DATEPART(W,@FCInicio) = 3 THEN @Concatenado ELSE MIE END,
			JUE = CASE WHEN DATEPART(W,@FCInicio) = 4 THEN @Concatenado ELSE JUE END,
			VIE = CASE WHEN DATEPART(W,@FCInicio) = 5 THEN @Concatenado ELSE VIE END
		WHERE Placa = '<PLACA>'

		SET @FCInicio = (SELECT DATEADD(DAY,1,@FCInicio))
	END

	SET @SAB = (SELECT SAB FROM @T_Prueba WHERE Placa = '<PLACA>')
	SET @DOM = (SELECT DOM FROM @T_Prueba WHERE Placa = '<PLACA>')
	SET @LUN = (SELECT LUN FROM @T_Prueba WHERE Placa = '<PLACA>')
	SET @MAR = (SELECT MAR FROM @T_Prueba WHERE Placa = '<PLACA>')
	SET @MIE = (SELECT MIE FROM @T_Prueba WHERE Placa = '<PLACA>')
	SET @JUE = (SELECT JUE FROM @T_Prueba WHERE Placa = '<PLACA>')
	SET @VIE = (SELECT VIE FROM @T_Prueba WHERE Placa = '<PLACA>')

	DECLARE @SQL VARCHAR(5000)
	CREATE TABLE #PruebaView (Nro VARCHAR(20), Placa VARCHAR(50), Operacion VARCHAR(50), TipoUnidad VARCHAR(250), Actividad VARCHAR(350),
							 ProxFecha VARCHAR(70), FCInicio VARCHAR(70), FCFin VARCHAR(70), NroSemana VARCHAR(10), FechaCumplimiento VARCHAR(70),
							 Estado VARCHAR(30), Observacion VARCHAR(250), FechaIngreso VARCHAR(70), ObservacionOP VARCHAR(250), SAB VARCHAR(15),
							 DOM VARCHAR(15), LUN VARCHAR(15), MAR VARCHAR(15), MIE VARCHAR(15), JUE VARCHAR(15), VIE VARCHAR(15))

	INSERT INTO #PruebaView
	SELECT * FROM @T_Prueba WHERE Placa <> '<PLACA>'

	SET @SQL = 'SELECT Nro, Placa AS PLACA, Operacion AS OPERACION, TipoUnidad AS TIPO_UNIDAD, Actividad AS ACTIVIDAD, ProxFecha AS FECHA_PROYECTADA,
				NroSemana AS SEMANA, FCInicio AS FECHA_INICIO, FCFin AS FECHA_FIN, SAB AS '+@SAB+', DOM AS '+@DOM+', LUN AS '+@LUN+', MAR AS '+@MAR+',
				MIE AS '+@MIE+', JUE AS '+@JUE+', VIE AS '+@VIE+', FechaIngreso AS FECHA_INGRESO, Estado AS ESTADO, FechaCumplimiento AS FECHA_EJECUCION,
				Observacion AS OBSERVACION, ObservacionOP AS OBSERVACION_OP FROM #PruebaView ORDER BY CONVERT(DATE,ProxFecha)'
	EXEC (@SQL)
	DROP TABLE #PruebaView
END

------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 09-08-2025
-- Description:	CONTADOR DE ACTIVIDADES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ContarCumpActividades]
@Anio INT,
@NroSemana INT
AS
BEGIN
	DECLARE @TotalMttoProg INT = (SELECT COUNT(*) FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto_Cumplimiento
	WHERE YEAR(FCFin) = @Anio AND NroSemana = @NroSemana)

	DECLARE @TotalMttoEjec INT = (SELECT COUNT(*) FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto_Cumplimiento
	WHERE Estado = 'EJECUTADO' AND YEAR(FCFin) = @Anio AND NroSemana = @NroSemana)

	SELECT @TotalMttoProg AS 'TOTAL_UNIDADES', @TotalMttoEjec AS 'TOTAL_EJECUTADOS'
END

------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 09-08-2025
-- Description: MODIFICAR CUMPLIMIENTO DE ACTIVIDADES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ProgramarCumpActividades]
@Opcion INT,
@Nro INT,
@FechaCump DATE,
@Estado VARCHAR(50),
@Observacion VARCHAR(350),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	DECLARE @FCInicio DATE = (SELECT FCInicio FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto_Cumplimiento WHERE Nro = @Nro)
	DECLARE @FCFin DATE = (SELECT FCFin FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto_Cumplimiento WHERE Nro = @Nro)

	IF (@Opcion = 1) BEGIN		-- PROGRAMAR CUMPLIMIENTO
		IF (@FechaCump < @FCInicio OR @FechaCump > @FCFin) BEGIN
			SET @Exito = '-1 = No puede programar esta actividad en un día que no corresponda a esta semana.'
			ROLLBACK
			GOTO Terminar
		END

		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto_Cumplimiento
		SET SAB = NULL, DOM = NULL, LUN = NULL, MAR = NULL, MIE = NULL, JUE = NULL, VIE = NULL
		WHERE Nro = @Nro

		IF (@Estado = 'EJECUTADO') BEGIN
			UPDATE ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto_Cumplimiento
			SET FechaCumplimiento = @FechaCump, Estado = @Estado, Observacion = @Observacion,
			SAB = CASE WHEN DATEPART(W,@FechaCump) = 6 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'X' ELSE SAB END,
			DOM = CASE WHEN DATEPART(W,@FechaCump) = 7 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'X' ELSE DOM END,
			LUN = CASE WHEN DATEPART(W,@FechaCump) = 1 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'X' ELSE LUN END,
			MAR = CASE WHEN DATEPART(W,@FechaCump) = 2 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'X' ELSE MAR END,
			MIE = CASE WHEN DATEPART(W,@FechaCump) = 3 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'X' ELSE MIE END,
			JUE = CASE WHEN DATEPART(W,@FechaCump) = 4 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'X' ELSE JUE END,
			VIE = CASE WHEN DATEPART(W,@FechaCump) = 5 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'X' ELSE VIE END
			WHERE Nro = @Nro
		END
		ELSE BEGIN
			UPDATE ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto_Cumplimiento
			SET Estado = @Estado, Observacion = @Observacion,
			SAB = CASE WHEN DATEPART(W,@FechaCump) = 6 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'X' ELSE SAB END,
			DOM = CASE WHEN DATEPART(W,@FechaCump) = 7 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'X' ELSE DOM END,
			LUN = CASE WHEN DATEPART(W,@FechaCump) = 1 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'X' ELSE LUN END,
			MAR = CASE WHEN DATEPART(W,@FechaCump) = 2 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'X' ELSE MAR END,
			MIE = CASE WHEN DATEPART(W,@FechaCump) = 3 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'X' ELSE MIE END,
			JUE = CASE WHEN DATEPART(W,@FechaCump) = 4 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'X' ELSE JUE END,
			VIE = CASE WHEN DATEPART(W,@FechaCump) = 5 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'X' ELSE VIE END
			WHERE Nro = @Nro
		END

		SET @Exito = '0 = Registro actualizado correctamente.'
	END

	IF (@Opcion = 2) BEGIN		-- PROGRAMAR INGRESO
		IF (EXISTS(SELECT FechaIngreso FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto_Cumplimiento WHERE Nro = @Nro) AND
		(CONVERT(DATE,GETDATE()) <= @FechaCump)) BEGIN
			UPDATE ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto_Cumplimiento
			SET FechaIngreso = @FechaCump, UsuarioIngreso = @Usuario
			WHERE Nro = @Nro

			DECLARE @Placa VARCHAR(30) = (SELECT Placa FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto_Cumplimiento WHERE Nro = @Nro)
			DECLARE @idPlaca INT = (SELECT IdVehiculo FROM OP_TR_VEHICULO WHERE NumeroPlaca = @Placa)
			DECLARE @FechaCump2 DATE = (SELECT DATEADD(DAY,1,@FechaCump))

			IF (CONVERT(DATE,GETDATE()) = @FechaCump) BEGIN
				IF EXISTS (SELECT * FROM ReportesApp_Operacion_UnidadesBloqueadas WHERE IdUnidad = @idPlaca) BEGIN
					SET @Exito = '0 = La unidad ya se encuentra bloqueada. Fecha registrada correctamente.'
				END
				ELSE BEGIN
					INSERT INTO ReportesApp_Operacion_UnidadesBloqueadas (IdCodigoPreviaje,FechaProgramacion,TipoProgramacion,IdUnidad,Motivo,Area,
																		  Descripcion,UsuarioBloquea,FechaBloquea,FechaInicio,FechaFin,Bloqueo)
					VALUES(0, GETDATE(), 0, @idPlaca, 'ACTIVIDAD DE MTTO. PREVENTIVO PENDIENTE', 'OPERACIONES', 'ACTIVIDAD DE MTTO. PREVENTIVO PENDIENTE', @Usuario, GETDATE(), @FechaCump, @FechaCump2, 1)
																	
					SET @Exito = '0 = Unidad bloqueada. Fecha registrada correctamente.'
				END
			END
			ELSE BEGIN
				IF EXISTS (SELECT * FROM ReportesApp_Operacion_UnidadesBloqueadas WHERE IdUnidad = @idPlaca) BEGIN
					SET @Exito = '0 = La unidad ya se encuentra bloqueada. Fecha registrada correctamente.'
				END
				ELSE BEGIN
					SET @Exito = '0 = Fecha registrada correctamente.'
				END
			END
		END
		ELSE BEGIN
			SET @Exito = '-1 = No puede reprogramar esta actividad en una fecha después del ingreso programado.'
			ROLLBACK
			GOTO Terminar
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

------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 15-01-2023
-- Description:	GENERAR REGISTRO DE MANTENIMIENTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_AgregarModificarControl]
@idVehiculo INT,
@idAccesorio INT,
@KMCambio DECIMAL(10,2),
@FechaCambio DATETIME,
@Intervalo DECIMAL(10,2),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

BEGIN TRAN
BEGIN TRY
	IF (CONVERT(DATE,@FechaCambio) >= CONVERT(DATE,GETDATE())) BEGIN
		SET @Exito = '-1 = No puede registrar fechas que sean mayores a la fecha actual.'
		ROLLBACK
		GOTO Terminar
	END
	ELSE BEGIN
		IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto WHERE idVehiculo = @idVehiculo AND idAccesorio = @idAccesorio)) BEGIN
			INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_HistorialProceso(idProcesoMtto, idVehiculo, idAccesorio, FechaCambio, KMCambio, Intervalo, Usuario, Fecha)
			SELECT idProcesoMtto,idVehiculo,idAccesorio,FechaCambio,KMCambio,Intervalo, @Usuario, GETDATE()
			FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto
			WHERE idVehiculo = @idVehiculo AND idAccesorio = @idAccesorio

			DECLARE @Placa VARCHAR(30) = (SELECT NumeroPlaca FROM OP_TR_Vehiculo WHERE IdVehiculo = @idVehiculo)
			DECLARE @Actividad VARCHAR(250) = (SELECT Descripcion FROM ReportesApp_Mantenimiento_MttoPreventivo_Accesorios WHERE idAccesorio = @idAccesorio)

			UPDATE ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto_Cumplimiento
			SET SAB = NULL, DOM = NULL, LUN = NULL, MAR = NULL, MIE = NULL, JUE = NULL, VIE = NULL
			WHERE (Placa = @Placa) AND (Actividad = @Actividad) AND (@FechaCambio BETWEEN FCInicio AND FCFin)
			
			UPDATE ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto_Cumplimiento
			SET FechaCumplimiento = @FechaCambio, Estado = 'EJECUTADO',
			SAB = CASE WHEN DATEPART(W,@FechaCambio) = 6 AND @FechaCambio >= FCInicio AND @FechaCambio <= FCFin THEN 'X' ELSE SAB END,
			DOM = CASE WHEN DATEPART(W,@FechaCambio) = 7 AND @FechaCambio >= FCInicio AND @FechaCambio <= FCFin THEN 'X' ELSE DOM END,
			LUN = CASE WHEN DATEPART(W,@FechaCambio) = 1 AND @FechaCambio >= FCInicio AND @FechaCambio <= FCFin THEN 'X' ELSE LUN END,
			MAR = CASE WHEN DATEPART(W,@FechaCambio) = 2 AND @FechaCambio >= FCInicio AND @FechaCambio <= FCFin THEN 'X' ELSE MAR END,
			MIE = CASE WHEN DATEPART(W,@FechaCambio) = 3 AND @FechaCambio >= FCInicio AND @FechaCambio <= FCFin THEN 'X' ELSE MIE END,
			JUE = CASE WHEN DATEPART(W,@FechaCambio) = 4 AND @FechaCambio >= FCInicio AND @FechaCambio <= FCFin THEN 'X' ELSE JUE END,
			VIE = CASE WHEN DATEPART(W,@FechaCambio) = 5 AND @FechaCambio >= FCInicio AND @FechaCambio <= FCFin THEN 'X' ELSE VIE END
			WHERE (Placa = @Placa) AND (Actividad = @Actividad) AND (@FechaCambio BETWEEN FCInicio AND FCFin)

			UPDATE ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto
			SET KMCambio = @KMCambio, FechaCambio = @FechaCambio, Intervalo = @Intervalo, Usuario = @Usuario, Fecha = GETDATE()
			WHERE idVehiculo = @idVehiculo AND idAccesorio = @idAccesorio

			SET @Exito = '0 = Mantenimiento Actualizado Correctamente.'
		END
		ELSE BEGIN
			SET @correlativo = (SELECT MAX(idProcesoMtto) FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto WHERE idVehiculo = @idVehiculo)
			SET @correlativo = ISNULL(@correlativo,0) + 1

			INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto(idProcesoMtto,idVehiculo,idAccesorio,FechaCambio,KMCambio,Intervalo,Usuario,Fecha)
			VALUES(@correlativo, @idVehiculo, @idAccesorio, @FechaCambio, @KMCambio, @Intervalo, @Usuario, GETDATE())

			SET @Exito = '0 = Mantenimiento Registrado Correctamente.'
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

------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 27-03-2023
-- Description:	GENERAR CONTROL MANTENIMIENTO - MAQUINAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_AgregarModificarControlMaquinas]
@MaquinaCodigo VARCHAR(50),
@idAccesorio INT,
@KMCambio DECIMAL(10,2),
@FechaCambio DATETIME,
@Intervalo DECIMAL(10,2),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

BEGIN TRAN
BEGIN TRY
	IF (CONVERT(DATE,@FechaCambio) >= CONVERT(DATE,GETDATE())) BEGIN
		SET @Exito = '-1 = No puede registrar fechas que sean mayores a la fecha actual.'
		ROLLBACK
		GOTO Terminar
	END
	ELSE BEGIN
		IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMttoMaquinas WHERE LTRIM(RTRIM(MaquinaCodigo)) = LTRIM(RTRIM(@MaquinaCodigo)) AND idAccesorio = @idAccesorio)) BEGIN
			INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_HistorialProcesoMaquinas(idProcesoMtto, MaquinaCodigo, idAccesorio, FechaCambio, KMCambio, Intervalo, Usuario, Fecha)
			SELECT idProcesoMtto,MaquinaCodigo,idAccesorio,FechaCambio,KMCambio,Intervalo, @Usuario, GETDATE()
			FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMttoMaquinas
			WHERE LTRIM(RTRIM(MaquinaCodigo)) = LTRIM(RTRIM(@MaquinaCodigo)) AND idAccesorio = @idAccesorio
			
			DECLARE @Actividad VARCHAR(250) = (SELECT Descripcion FROM ReportesApp_Mantenimiento_MttoPreventivo_Accesorios WHERE idAccesorio = @idAccesorio)

			UPDATE ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto_Cumplimiento
			SET SAB = NULL, DOM = NULL, LUN = NULL, MAR = NULL, MIE = NULL, JUE = NULL, VIE = NULL
			WHERE (Placa = @MaquinaCodigo) AND (Actividad = @Actividad) AND (@FechaCambio BETWEEN FCInicio AND FCFin)

			UPDATE ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto_Cumplimiento
			SET FechaCumplimiento = @FechaCambio, Estado = 'EJECUTADO',
			SAB = CASE WHEN DATEPART(W,@FechaCambio) = 6 AND @FechaCambio >= FCInicio AND @FechaCambio <= FCFin THEN 'X' ELSE SAB END,
			DOM = CASE WHEN DATEPART(W,@FechaCambio) = 7 AND @FechaCambio >= FCInicio AND @FechaCambio <= FCFin THEN 'X' ELSE DOM END,
			LUN = CASE WHEN DATEPART(W,@FechaCambio) = 1 AND @FechaCambio >= FCInicio AND @FechaCambio <= FCFin THEN 'X' ELSE LUN END,
			MAR = CASE WHEN DATEPART(W,@FechaCambio) = 2 AND @FechaCambio >= FCInicio AND @FechaCambio <= FCFin THEN 'X' ELSE MAR END,
			MIE = CASE WHEN DATEPART(W,@FechaCambio) = 3 AND @FechaCambio >= FCInicio AND @FechaCambio <= FCFin THEN 'X' ELSE MIE END,
			JUE = CASE WHEN DATEPART(W,@FechaCambio) = 4 AND @FechaCambio >= FCInicio AND @FechaCambio <= FCFin THEN 'X' ELSE JUE END,
			VIE = CASE WHEN DATEPART(W,@FechaCambio) = 5 AND @FechaCambio >= FCInicio AND @FechaCambio <= FCFin THEN 'X' ELSE VIE END
			WHERE (Placa = @MaquinaCodigo) AND (Actividad = @Actividad) AND (@FechaCambio BETWEEN FCInicio AND FCFin)

			UPDATE ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMttoMaquinas
			SET KMCambio = @KMCambio, FechaCambio = @FechaCambio, Intervalo = @Intervalo, Usuario = @Usuario, Fecha = GETDATE()
			WHERE LTRIM(RTRIM(MaquinaCodigo)) = LTRIM(RTRIM(@MaquinaCodigo)) AND idAccesorio = @idAccesorio

			SET @Exito = '0 = Mantenimiento Actualizado Correctamente.'
		END
		ELSE BEGIN
			SET @correlativo = (SELECT MAX(idProcesoMtto) FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMttoMaquinas WHERE LTRIM(RTRIM(MaquinaCodigo)) = LTRIM(RTRIM(@MaquinaCodigo)))
			SET @correlativo = ISNULL(@correlativo,0) + 1

			INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMttoMaquinas(idProcesoMtto,MaquinaCodigo,idAccesorio,FechaCambio,KMCambio,Intervalo,Usuario,Fecha)
			VALUES(@correlativo, @MaquinaCodigo, @idAccesorio, @FechaCambio, @KMCambio, @Intervalo, @Usuario, GETDATE())

			SET @Exito = '0 = Mantenimiento Registrado Correctamente.'
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

------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 24-05-2025
-- Description:	GENERAR CONTROL MANTENIMIENTO - EQUIPOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_AgregarModificarControlEquipos]
@idRegistro INT,
@idAccesorio INT,
@FechaCambio DATETIME,
@Periodo INT,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

BEGIN TRAN
BEGIN TRY
	IF (CONVERT(DATE,@FechaCambio) > CONVERT(DATE,GETDATE())) BEGIN
		SET @Exito = '-1 = No puede registrar fechas que sean mayores a la fecha actual.'
		ROLLBACK
		GOTO Terminar
	END
	ELSE BEGIN
		IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMttoEquipos WHERE idRegistro = @idRegistro AND idAccesorio = @idAccesorio)) BEGIN
			INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_HistorialProcesoEquipos(idProcesoMtto,idRegistro,idAccesorio,FechaCambio,Periodo,Usuario,Fecha)
			SELECT idProcesoMtto,idRegistro,idAccesorio,FechaCambio,Periodo,@Usuario,GETDATE()
			FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMttoEquipos
			WHERE idRegistro = @idRegistro AND idAccesorio = @idAccesorio

			DECLARE @Codigo VARCHAR(30) = (SELECT Codigo FROM ReportesApp_Mantenimiento_MttoPreventivo_RegistroEquipos WHERE idRegistro = @idRegistro)
			DECLARE @Actividad VARCHAR(250) = (SELECT Descripcion FROM ReportesApp_Mantenimiento_MttoPreventivo_Accesorios WHERE idAccesorio = @idAccesorio)

			UPDATE ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto_Cumplimiento
			SET SAB = NULL, DOM = NULL, LUN = NULL, MAR = NULL, MIE = NULL, JUE = NULL, VIE = NULL
			WHERE (Placa = @Codigo) AND (Actividad = @Actividad) AND (@FechaCambio BETWEEN FCInicio AND FCFin)
			
			UPDATE ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto_Cumplimiento
			SET FechaCumplimiento = @FechaCambio, Estado = 'EJECUTADO',
			SAB = CASE WHEN DATEPART(W,@FechaCambio) = 6 AND @FechaCambio >= FCInicio AND @FechaCambio <= FCFin THEN 'X' ELSE SAB END,
			DOM = CASE WHEN DATEPART(W,@FechaCambio) = 7 AND @FechaCambio >= FCInicio AND @FechaCambio <= FCFin THEN 'X' ELSE DOM END,
			LUN = CASE WHEN DATEPART(W,@FechaCambio) = 1 AND @FechaCambio >= FCInicio AND @FechaCambio <= FCFin THEN 'X' ELSE LUN END,
			MAR = CASE WHEN DATEPART(W,@FechaCambio) = 2 AND @FechaCambio >= FCInicio AND @FechaCambio <= FCFin THEN 'X' ELSE MAR END,
			MIE = CASE WHEN DATEPART(W,@FechaCambio) = 3 AND @FechaCambio >= FCInicio AND @FechaCambio <= FCFin THEN 'X' ELSE MIE END,
			JUE = CASE WHEN DATEPART(W,@FechaCambio) = 4 AND @FechaCambio >= FCInicio AND @FechaCambio <= FCFin THEN 'X' ELSE JUE END,
			VIE = CASE WHEN DATEPART(W,@FechaCambio) = 5 AND @FechaCambio >= FCInicio AND @FechaCambio <= FCFin THEN 'X' ELSE VIE END
			WHERE (Placa = @Codigo) AND (Actividad = @Actividad) AND (@FechaCambio BETWEEN FCInicio AND FCFin)

			UPDATE ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMttoEquipos
			SET FechaCambio = @FechaCambio, Periodo = @Periodo, Usuario = @Usuario, Fecha = GETDATE()
			WHERE idRegistro = @idRegistro AND idAccesorio = @idAccesorio

			SET @Exito = '0 = Mantenimiento Actualizado Correctamente.'
		END
		ELSE BEGIN
			SET @correlativo = (SELECT MAX(idProcesoMtto) FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMttoEquipos WHERE idRegistro = @idRegistro)
			SET @correlativo = ISNULL(@correlativo,0) + 1

			INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMttoEquipos(idProcesoMtto,idRegistro,idAccesorio,FechaCambio,Periodo,Usuario,Fecha)
			VALUES(@correlativo, @idRegistro, @idAccesorio, @FechaCambio, @Periodo, @Usuario, GETDATE())

			SET @Exito = '0 = Mantenimiento Registrado Correctamente.'
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

------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 05-02-2024
-- Description:	GENERAR CUMPLIMIENTO - INSPECCIONES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_GenerarCumpInspeccion]
@Placa VARCHAR(50),
@Operacion VARCHAR(50),
@TipoUnidad VARCHAR(250),
@Marca VARCHAR(250),
@ProxInspeccion DATE,
@FCInicio DATE,
@FCFin DATE
AS
DECLARE @Contador INT 
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (EXISTS(SELECT TOP(1) * FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cumplimiento WHERE Placa = @Placa AND FCInicio = @FCInicio AND FCFin = @FCFin ORDER BY Nro DESC)) BEGIN
		SET @Exito = '-1 = La unidad '+@Placa+' ya tiene un cumplimiento registrado en esta semana.'
		ROLLBACK
		GOTO Terminar
	END

	SET @Contador = (SELECT MAX(Nro) FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cumplimiento)
	SET @Contador = ISNULL(@Contador,0) + 1

	IF (EXISTS(SELECT TOP(1) * FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cumplimiento WHERE Placa = @Placa AND Estado IN ('PROGRAMADO','REPROGRAMADO') ORDER BY Nro DESC)) BEGIN
		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cumplimiento
		SET Observacion = 'NO INGRESÓ POR OPERACIÓN'
		WHERE Nro = (SELECT TOP(1) Nro FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cumplimiento WHERE Placa = @Placa AND Estado IN ('PROGRAMADO','REPROGRAMADO') ORDER BY Nro DESC)
	
		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cumplimiento(Nro,Placa,Operacion,TipoUnidad,Marca,ProxInspeccion,FCInicio,FCFin,Estado)
		VALUES(@Contador,@Placa,@Operacion,@TipoUnidad,@Marca,@ProxInspeccion,@FCInicio,@FCFin,'REPROGRAMADO')
	END
	ELSE BEGIN
		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cumplimiento(Nro,Placa,Operacion,TipoUnidad,Marca,ProxInspeccion,FCInicio,FCFin,Estado)
		VALUES(@Contador,@Placa,@Operacion,@TipoUnidad,@Marca,@ProxInspeccion,@FCInicio,@FCFin,'PROGRAMADO')
	END

	DECLARE @NroSemana INT = (SELECT CASE WHEN DATEPART(W,@FCInicio) IN (6,7) THEN DATEPART(WW,DATEADD(DAY,7,@FCInicio)) ELSE DATEPART(WW,@FCInicio) END)

	UPDATE ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cumplimiento
	SET NroSemana = @NroSemana
	WHERE Nro = @Contador

	UPDATE IC
	SET IC.SAB = CASE WHEN DATEPART(W,@ProxInspeccion) = 6 AND @ProxInspeccion >= @FCInicio AND @ProxInspeccion <= @FCFin THEN 'INS' ELSE IC.SAB END,
		IC.DOM = CASE WHEN DATEPART(W,@ProxInspeccion) = 7 AND @ProxInspeccion >= @FCInicio AND @ProxInspeccion <= @FCFin THEN 'INS' ELSE IC.DOM END,
		IC.LUN = CASE WHEN DATEPART(W,@ProxInspeccion) = 1 AND @ProxInspeccion >= @FCInicio AND @ProxInspeccion <= @FCFin THEN 'INS' ELSE IC.LUN END,
		IC.MAR = CASE WHEN DATEPART(W,@ProxInspeccion) = 2 AND @ProxInspeccion >= @FCInicio AND @ProxInspeccion <= @FCFin THEN 'INS' ELSE IC.MAR END,
		IC.MIE = CASE WHEN DATEPART(W,@ProxInspeccion) = 3 AND @ProxInspeccion >= @FCInicio AND @ProxInspeccion <= @FCFin THEN 'INS' ELSE IC.MIE END,
		IC.JUE = CASE WHEN DATEPART(W,@ProxInspeccion) = 4 AND @ProxInspeccion >= @FCInicio AND @ProxInspeccion <= @FCFin THEN 'INS' ELSE IC.JUE END,
		IC.VIE = CASE WHEN DATEPART(W,@ProxInspeccion) = 5 AND @ProxInspeccion >= @FCInicio AND @ProxInspeccion <= @FCFin THEN 'INS' ELSE IC.VIE END
	FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cumplimiento IC
	WHERE IC.Nro = @Contador

	SET @Exito = '0 = El cumplimiento semanal ha sido generado exitosamente.'
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

------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 05-02-2025
-- Description:	LISTAR CUMPLIMIENTO INSPECCIONES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarCumpInspeccion]
@Anio INT,
@NroSemana INT
AS
BEGIN
	DECLARE @T_Prueba TABLE (Nro VARCHAR(20), Placa VARCHAR(50), Operacion VARCHAR(50), TipoUnidad VARCHAR(250), Marca VARCHAR(70),
							 ProxInspeccion VARCHAR(70), FCInicio VARCHAR(70), FCFin VARCHAR(70), NroSemana VARCHAR(10), FechaCumplimiento VARCHAR(70),
							 Estado VARCHAR(30), Observacion VARCHAR(250), FechaIngreso VARCHAR(70), ObservacionOP VARCHAR(250), SAB VARCHAR(15),
							 DOM VARCHAR(15), LUN VARCHAR(15), MAR VARCHAR(15), MIE VARCHAR(15), JUE VARCHAR(15), VIE VARCHAR(15))
	
	DECLARE @SAB VARCHAR(15), @DOM VARCHAR(15), @LUN VARCHAR(15), @MAR VARCHAR(15), @MIE VARCHAR(15), @JUE VARCHAR(15), @VIE VARCHAR(15)

	INSERT INTO @T_Prueba (Nro,Placa,Operacion,TipoUnidad,Marca,ProxInspeccion,FCInicio,FCFin,NroSemana,FechaCumplimiento,Estado,Observacion,FechaIngreso, ObservacionOP)
	VALUES ('Nro','<PLACA>','Operacion','TipoUnidad','Marca','ProxInspeccion','FCInicio','FCFin','NroSemana','FechaCumplimiento','Estado','Observacion','FechaIngreso','ObservacionOP')
	
	INSERT INTO @T_Prueba (Nro,Placa,Operacion,TipoUnidad,Marca,ProxInspeccion,FCInicio,FCFin,NroSemana,FechaCumplimiento,Estado,Observacion,FechaIngreso,
	ObservacionOP,SAB,DOM,LUN,MAR,MIE,JUE,VIE)
	SELECT CONVERT(VARCHAR,Nro), Placa, Operacion, TipoUnidad, Marca, CONVERT(VARCHAR,ProxInspeccion,103), CONVERT(VARCHAR,FCInicio,103), CONVERT(VARCHAR,FCFin,103),
	CONVERT(VARCHAR,NroSemana), CONVERT(VARCHAR,FechaCumplimiento,103), Estado, Observacion, CONVERT(VARCHAR,FechaIngreso,103), ObservacionOP, SAB, DOM, LUN, MAR, MIE, JUE, VIE
	FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cumplimiento
	WHERE YEAR(FCFin) = @Anio AND NroSemana = @NroSemana

	DECLARE @FCInicio DATE = (SELECT TOP(1) FCInicio FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cumplimiento WHERE YEAR(FCFin) = @Anio AND NroSemana = @NroSemana)
	DECLARE @FCFin DATE = (SELECT TOP(1) FCFin FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cumplimiento WHERE YEAR(FCFin) = @Anio AND NroSemana = @NroSemana)

	WHILE (@FCInicio <= @FCFin) BEGIN
		DECLARE @Concatenado VARCHAR(15) = LEFT(UPPER(DATENAME(WEEKDAY, @FCInicio)), 3) + ''  + RIGHT( '0' + CAST(DAY(@FCInicio) AS VARCHAR(2)),2)

		UPDATE @T_Prueba
		SET SAB = CASE WHEN DATEPART(W,@FCInicio) = 6 THEN @Concatenado ELSE SAB END,
			DOM = CASE WHEN DATEPART(W,@FCInicio) = 7 THEN @Concatenado ELSE DOM END,
			LUN = CASE WHEN DATEPART(W,@FCInicio) = 1 THEN @Concatenado ELSE LUN END,
			MAR = CASE WHEN DATEPART(W,@FCInicio) = 2 THEN @Concatenado ELSE MAR END,
			MIE = CASE WHEN DATEPART(W,@FCInicio) = 3 THEN @Concatenado ELSE MIE END,
			JUE = CASE WHEN DATEPART(W,@FCInicio) = 4 THEN @Concatenado ELSE JUE END,
			VIE = CASE WHEN DATEPART(W,@FCInicio) = 5 THEN @Concatenado ELSE VIE END
		WHERE Placa = '<PLACA>'

		SET @FCInicio = (SELECT DATEADD(DAY,1,@FCInicio))
	END

	SET @SAB = (SELECT SAB FROM @T_Prueba WHERE Placa = '<PLACA>')
	SET @DOM = (SELECT DOM FROM @T_Prueba WHERE Placa = '<PLACA>')
	SET @LUN = (SELECT LUN FROM @T_Prueba WHERE Placa = '<PLACA>')
	SET @MAR = (SELECT MAR FROM @T_Prueba WHERE Placa = '<PLACA>')
	SET @MIE = (SELECT MIE FROM @T_Prueba WHERE Placa = '<PLACA>')
	SET @JUE = (SELECT JUE FROM @T_Prueba WHERE Placa = '<PLACA>')
	SET @VIE = (SELECT VIE FROM @T_Prueba WHERE Placa = '<PLACA>')

	DECLARE @SQL VARCHAR(5000)
	CREATE TABLE #PruebaView (Nro VARCHAR(20), Placa VARCHAR(50), Operacion VARCHAR(50), TipoUnidad VARCHAR(250), Marca VARCHAR(70),
							 ProxInspeccion VARCHAR(70), FCInicio VARCHAR(70), FCFin VARCHAR(70), NroSemana VARCHAR(10), FechaCumplimiento VARCHAR(70),
							 Estado VARCHAR(30), Observacion VARCHAR(250), FechaIngreso VARCHAR(70), ObservacionOP VARCHAR(250), SAB VARCHAR(15),
							 DOM VARCHAR(15), LUN VARCHAR(15), MAR VARCHAR(15), MIE VARCHAR(15), JUE VARCHAR(15), VIE VARCHAR(15))

	INSERT INTO #PruebaView
	SELECT * FROM @T_Prueba WHERE Placa <> '<PLACA>'

	SET @SQL = 'SELECT Nro, Placa AS PLACA, Operacion AS OPERACION, TipoUnidad AS TIPO_UNIDAD, Marca AS MARCA, ProxInspeccion AS PROX_INSPECCION,
				NroSemana AS SEMANA, FCInicio AS FECHA_INICIO, FCFin AS FECHA_FIN, SAB AS '+@SAB+', DOM AS '+@DOM+', LUN AS '+@LUN+', MAR AS '+@MAR+',
				MIE AS '+@MIE+', JUE AS '+@JUE+', VIE AS '+@VIE+', FechaIngreso AS FECHA_INGRESO, Estado AS ESTADO, FechaCumplimiento AS FECHA_EJECUCION,
				Observacion AS OBSERVACION, ObservacionOP AS OBSERVACION_OP FROM #PruebaView ORDER BY CONVERT(DATE,ProxInspeccion)'
	EXEC (@SQL)
	DROP TABLE #PruebaView
END

------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 05-02-2025
-- Description:	CONTADOR DE INSPECCIONES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ContarCumpInspecciones]
@Anio INT,
@NroSemana INT
AS
BEGIN
	DECLARE @TotalMttoProg INT = (SELECT COUNT(*) FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cumplimiento
	WHERE YEAR(FCFin) = @Anio AND NroSemana = @NroSemana)

	DECLARE @TotalMttoEjec INT = (SELECT COUNT(*) FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cumplimiento
	WHERE Estado = 'EJECUTADO' AND YEAR(FCFin) = @Anio AND NroSemana = @NroSemana)

	SELECT @TotalMttoProg AS 'TOTAL_UNIDADES', @TotalMttoEjec AS 'TOTAL_EJECUTADOS'
END

-------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 06-02-2025
-- Description: MODIFICAR CUMPLIMIENTO DE INSPECCIONES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ProgramarCumpInspeccion]
@Opcion INT,
@Nro INT,
@FechaCump DATE,
@Estado VARCHAR(50),
@Observacion VARCHAR(350),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	DECLARE @FCInicio DATE = (SELECT FCInicio FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cumplimiento WHERE Nro = @Nro)
	DECLARE @FCFin DATE = (SELECT FCFin FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cumplimiento WHERE Nro = @Nro)

	IF (@Opcion = 1) BEGIN		-- PROGRAMAR CUMPLIMIENTO
		IF (@FechaCump < @FCInicio OR @FechaCump > @FCFin) BEGIN
			SET @Exito = '-1 = No puede programar esta inspección en un día que no corresponda a esta semana.'
			ROLLBACK
			GOTO Terminar
		END

		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cumplimiento
		SET SAB = NULL, DOM = NULL, LUN = NULL, MAR = NULL, MIE = NULL, JUE = NULL, VIE = NULL
		WHERE Nro = @Nro

		IF (@Estado = 'EJECUTADO') BEGIN
			UPDATE ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cumplimiento
			SET FechaCumplimiento = @FechaCump, Estado = @Estado, Observacion = @Observacion,
			SAB = CASE WHEN DATEPART(W,@FechaCump) = 6 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'INS' ELSE SAB END,
			DOM = CASE WHEN DATEPART(W,@FechaCump) = 7 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'INS' ELSE DOM END,
			LUN = CASE WHEN DATEPART(W,@FechaCump) = 1 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'INS' ELSE LUN END,
			MAR = CASE WHEN DATEPART(W,@FechaCump) = 2 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'INS' ELSE MAR END,
			MIE = CASE WHEN DATEPART(W,@FechaCump) = 3 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'INS' ELSE MIE END,
			JUE = CASE WHEN DATEPART(W,@FechaCump) = 4 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'INS' ELSE JUE END,
			VIE = CASE WHEN DATEPART(W,@FechaCump) = 5 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'INS' ELSE VIE END
			WHERE Nro = @Nro
		END
		ELSE BEGIN
			UPDATE ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cumplimiento
			SET Estado = @Estado, Observacion = @Observacion,
			SAB = CASE WHEN DATEPART(W,@FechaCump) = 6 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'INS' ELSE SAB END,
			DOM = CASE WHEN DATEPART(W,@FechaCump) = 7 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'INS' ELSE DOM END,
			LUN = CASE WHEN DATEPART(W,@FechaCump) = 1 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'INS' ELSE LUN END,
			MAR = CASE WHEN DATEPART(W,@FechaCump) = 2 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'INS' ELSE MAR END,
			MIE = CASE WHEN DATEPART(W,@FechaCump) = 3 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'INS' ELSE MIE END,
			JUE = CASE WHEN DATEPART(W,@FechaCump) = 4 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'INS' ELSE JUE END,
			VIE = CASE WHEN DATEPART(W,@FechaCump) = 5 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'INS' ELSE VIE END
			WHERE Nro = @Nro
		END

		SET @Exito = '0 = Registro actualizado correctamente.'
	END

	IF (@Opcion = 2) BEGIN		-- PROGRAMAR INGRESO
		IF (EXISTS(SELECT FechaIngreso FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cumplimiento WHERE Nro = @Nro) AND
		(CONVERT(DATE,GETDATE()) <= @FechaCump)) BEGIN
			UPDATE ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cumplimiento
			SET FechaIngreso = @FechaCump, UsuarioIngreso = @Usuario
			WHERE Nro = @Nro

			DECLARE @Placa VARCHAR(30) = (SELECT Placa FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cumplimiento WHERE Nro = @Nro)
			DECLARE @idPlaca INT = (SELECT IdVehiculo FROM OP_TR_VEHICULO WHERE NumeroPlaca = @Placa)
			DECLARE @FechaCump2 DATE = (SELECT DATEADD(DAY,1,@FechaCump))

			IF (CONVERT(DATE,GETDATE()) = @FechaCump) BEGIN
				IF EXISTS (SELECT * FROM ReportesApp_Operacion_UnidadesBloqueadas WHERE IdUnidad = @idPlaca) BEGIN
					SET @Exito = '0 = La unidad ya se encuentra bloqueada. Fecha registrada correctamente.'
				END
				ELSE BEGIN
					INSERT INTO ReportesApp_Operacion_UnidadesBloqueadas (IdCodigoPreviaje,FechaProgramacion,TipoProgramacion,IdUnidad,Motivo,Area,
																		  Descripcion,UsuarioBloquea,FechaBloquea,FechaInicio,FechaFin,Bloqueo)
					VALUES(0, GETDATE(), 0, @idPlaca, 'INSPECCIÓN PENDIENTE', 'OPERACIONES', 'INSPECCIÓN PENDIENTE', @Usuario, GETDATE(), @FechaCump, @FechaCump2, 1)
																	
					SET @Exito = '0 = Unidad bloqueada. Fecha registrada correctamente.'
				END
			END
			ELSE BEGIN
				IF EXISTS (SELECT * FROM ReportesApp_Operacion_UnidadesBloqueadas WHERE IdUnidad = @idPlaca) BEGIN
					SET @Exito = '0 = La unidad ya se encuentra bloqueada. Fecha registrada correctamente.'
				END
				ELSE BEGIN
					SET @Exito = '0 = Fecha registrada correctamente.'
				END
			END
		END
		ELSE BEGIN
			SET @Exito = '-1 = No puede reprogramar esta inspección en una fecha después del ingreso programado.'
			ROLLBACK
			GOTO Terminar
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

-------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 14-11-2024
-- Description: ELIMINAR REGISTROS DE CUMPLIMIENTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_EliminarCumplimiento]
@Opcion INT,
@Nro INT
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- ELIMINAR MTTO. PREVENTIVO
		DELETE FROM ReportesApp_Mantenimiento_MttoPreventivo_ReporteCumplimiento
		WHERE Nro = @Nro

		SET @Exito = '0 = Registro eliminado correctamente.'
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR INSPECCION
		DELETE FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cumplimiento
		WHERE Nro = @Nro

		SET @Exito = '0 = Registro eliminado correctamente.'
	END

	IF (@Opcion = 3) BEGIN		-- ELIMINAR ACTIVIDAD
		DELETE FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto_Cumplimiento
		WHERE Nro = @Nro

		SET @Exito = '0 = Registro eliminado correctamente.'
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

------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 05-12-2024
-- Description:	LISTAR PORCENTAJE CUMPLIMIENTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_AgruparPorcentaje]
@Opcion INT,
@Anio INT,
@NroSemana INT
AS
BEGIN
	IF (@Opcion = 1) BEGIN		-- PORCENTAJE MTTOS. PROGRAMADOS
		DECLARE @TABLA_PORC TABLE (TipoUnidad VARCHAR(50), TotalUnidades INT)
		DECLARE @TABLA_CUMP TABLE (TipoUnidad VARCHAR(50), TotalCumplidas INT)

		INSERT INTO @TABLA_PORC(TipoUnidad, TotalUnidades)
		SELECT TipoUnidad, COUNT(*)
		FROM ReportesApp_Mantenimiento_MttoPreventivo_ReporteCumplimiento
		WHERE YEAR(FCFin) = @Anio AND NroSemana = @NroSemana
		GROUP BY TipoUnidad

		INSERT INTO @TABLA_CUMP(TipoUnidad, TotalCumplidas)
		SELECT TipoUnidad, COUNT(*)
		FROM ReportesApp_Mantenimiento_MttoPreventivo_ReporteCumplimiento
		WHERE Estado = 'EJECUTADO' AND YEAR(FCFin) = @Anio AND NroSemana = @NroSemana
		GROUP BY TipoUnidad

		SELECT P.TipoUnidad AS 'TIPO_UNIDAD', P.TotalUnidades AS 'TOTAL_UNIDADES', ISNULL(C.TotalCumplidas,0) AS 'TOTAL_CUMPLIDAS',
		CONVERT(VARCHAR,CONVERT(DECIMAL(10,2),ISNULL((C.TotalCumplidas * 100) / P.TotalUnidades,0))) + ' %' AS 'PORCENTAJE'
		FROM @TABLA_PORC P
		LEFT JOIN @TABLA_CUMP C ON P.TipoUnidad = C.TipoUnidad
	END

	IF (@Opcion = 2) BEGIN		-- PORCENTAJE INSPECCIONES	
		DECLARE @TABLA_PORC2 TABLE (TipoUnidad VARCHAR(50), TotalUnidades INT)
		DECLARE @TABLA_CUMP2 TABLE (TipoUnidad VARCHAR(50), TotalCumplidas INT)

		INSERT INTO @TABLA_PORC2(TipoUnidad, TotalUnidades)
		SELECT TipoUnidad, COUNT(*)
		FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cumplimiento
		WHERE YEAR(FCFin) = @Anio AND NroSemana = @NroSemana
		GROUP BY TipoUnidad

		INSERT INTO @TABLA_CUMP2(TipoUnidad, TotalCumplidas)
		SELECT TipoUnidad, COUNT(*)
		FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cumplimiento
		WHERE Estado = 'EJECUTADO' AND YEAR(FCFin) = @Anio AND NroSemana = @NroSemana
		GROUP BY TipoUnidad

		SELECT P.TipoUnidad AS 'TIPO_UNIDAD', P.TotalUnidades AS 'TOTAL_UNIDADES', ISNULL(C.TotalCumplidas,0) AS 'TOTAL_CUMPLIDAS',
		CONVERT(VARCHAR,CONVERT(DECIMAL(10,2),ISNULL((C.TotalCumplidas * 100) / P.TotalUnidades,0))) + ' %' AS 'PORCENTAJE'
		FROM @TABLA_PORC2 P
		LEFT JOIN @TABLA_CUMP2 C ON P.TipoUnidad = C.TipoUnidad
	END

	IF (@Opcion = 3) BEGIN		-- PORCENTAJE ACTIVIDADES
		DECLARE @TABLA_PORC3 TABLE (TipoUnidad VARCHAR(50), TotalUnidades INT)
		DECLARE @TABLA_CUMP3 TABLE (TipoUnidad VARCHAR(50), TotalCumplidas INT)

		INSERT INTO @TABLA_PORC3(TipoUnidad, TotalUnidades)
		SELECT TipoUnidad, COUNT(*)
		FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto_Cumplimiento
		WHERE YEAR(FCFin) = @Anio AND NroSemana = @NroSemana
		GROUP BY TipoUnidad

		INSERT INTO @TABLA_CUMP3(TipoUnidad, TotalCumplidas)
		SELECT TipoUnidad, COUNT(*)
		FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto_Cumplimiento
		WHERE Estado = 'EJECUTADO' AND YEAR(FCFin) = @Anio AND NroSemana = @NroSemana
		GROUP BY TipoUnidad

		SELECT P.TipoUnidad AS 'TIPO_UNIDAD', P.TotalUnidades AS 'TOTAL_UNIDADES', ISNULL(C.TotalCumplidas,0) AS 'TOTAL_CUMPLIDAS',
		CONVERT(VARCHAR,CONVERT(DECIMAL(10,2),ISNULL((C.TotalCumplidas * 100) / P.TotalUnidades,0))) + ' %' AS 'PORCENTAJE'
		FROM @TABLA_PORC3 P
		LEFT JOIN @TABLA_CUMP3 C ON P.TipoUnidad = C.TipoUnidad
	END
END

------------------------------------------------------------------------------------
------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 13-11-2024
-- Description:	GENERAR REPORTE CUMPLIMIENTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_GenerarCumplimiento]
@Placa VARCHAR(50),
@Operacion VARCHAR(50),
@TipoUnidad VARCHAR(250),
@Marca VARCHAR(250),
@MttoPreventivo VARCHAR(20),
@FechaProgramada DATE,
@FCInicio DATE,
@FCFin DATE
AS
DECLARE @Contador INT 
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (EXISTS(SELECT TOP(1) * FROM ReportesApp_Mantenimiento_MttoPreventivo_ReporteCumplimiento WHERE Placa = @Placa AND FCInicio = @FCInicio AND FCFin = @FCFin ORDER BY Nro DESC)) BEGIN
		SET @Exito = '-1 = La unidad '+@Placa+' ya tiene un cumplimiento registrado en esta semana.'
		ROLLBACK
		GOTO Terminar
	END

	SET @Contador = (SELECT MAX(Nro) FROM ReportesApp_Mantenimiento_MttoPreventivo_ReporteCumplimiento)
	SET @Contador = ISNULL(@Contador,0) + 1

	IF (EXISTS(SELECT TOP(1) * FROM ReportesApp_Mantenimiento_MttoPreventivo_ReporteCumplimiento WHERE Placa = @Placa AND Estado IN ('PROGRAMADO','REPROGRAMADO') ORDER BY Nro DESC)) BEGIN
		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_ReporteCumplimiento
		SET Observacion = 'NO INGRESÓ POR OPERACIÓN'
		WHERE Nro = (SELECT TOP(1) Nro FROM ReportesApp_Mantenimiento_MttoPreventivo_ReporteCumplimiento WHERE Placa = @Placa AND Estado IN ('PROGRAMADO','REPROGRAMADO') ORDER BY Nro DESC)
	
		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_ReporteCumplimiento(Nro,Placa,Operacion,TipoUnidad,Marca,MttoPreventivo,FechaProgramada,FCInicio,FCFin,Estado)
		VALUES(@Contador,@Placa,@Operacion,@TipoUnidad,@Marca,@MttoPreventivo,@FechaProgramada,@FCInicio,@FCFin,'REPROGRAMADO')
	END
	ELSE BEGIN
		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_ReporteCumplimiento(Nro,Placa,Operacion,TipoUnidad,Marca,MttoPreventivo,FechaProgramada,FCInicio,FCFin,Estado)
		VALUES(@Contador,@Placa,@Operacion,@TipoUnidad,@Marca,@MttoPreventivo,@FechaProgramada,@FCInicio,@FCFin,'PROGRAMADO')
	END

	DECLARE @NroSemana INT = (SELECT CASE WHEN DATEPART(W,@FCInicio) IN (6,7) THEN DATEPART(WW,DATEADD(DAY,7,@FCInicio)) ELSE DATEPART(WW,@FCInicio) END)

	UPDATE ReportesApp_Mantenimiento_MttoPreventivo_ReporteCumplimiento
	SET NroSemana = @NroSemana
	WHERE Nro = @Contador

	UPDATE RC
	SET RC.SAB = CASE WHEN DATEPART(W,@FechaProgramada) = 6 AND @FechaProgramada >= @FCInicio AND @FechaProgramada <= @FCFin THEN @MttoPreventivo ELSE RC.SAB END,
		RC.DOM = CASE WHEN DATEPART(W,@FechaProgramada) = 7 AND @FechaProgramada >= @FCInicio AND @FechaProgramada <= @FCFin THEN @MttoPreventivo ELSE RC.DOM END,
		RC.LUN = CASE WHEN DATEPART(W,@FechaProgramada) = 1 AND @FechaProgramada >= @FCInicio AND @FechaProgramada <= @FCFin THEN @MttoPreventivo ELSE RC.LUN END,
		RC.MAR = CASE WHEN DATEPART(W,@FechaProgramada) = 2 AND @FechaProgramada >= @FCInicio AND @FechaProgramada <= @FCFin THEN @MttoPreventivo ELSE RC.MAR END,
		RC.MIE = CASE WHEN DATEPART(W,@FechaProgramada) = 3 AND @FechaProgramada >= @FCInicio AND @FechaProgramada <= @FCFin THEN @MttoPreventivo ELSE RC.MIE END,
		RC.JUE = CASE WHEN DATEPART(W,@FechaProgramada) = 4 AND @FechaProgramada >= @FCInicio AND @FechaProgramada <= @FCFin THEN @MttoPreventivo ELSE RC.JUE END,
		RC.VIE = CASE WHEN DATEPART(W,@FechaProgramada) = 5 AND @FechaProgramada >= @FCInicio AND @FechaProgramada <= @FCFin THEN @MttoPreventivo ELSE RC.VIE END
	FROM ReportesApp_Mantenimiento_MttoPreventivo_ReporteCumplimiento RC
	WHERE RC.Nro = @Contador

	SET @Exito = '0 = El cumplimiento semanal ha sido generado exitosamente.'
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

--------------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 14-11-2024
-- Description:	LISTAR REPORTE CUMPLIMIENTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarCumplimiento]
@Anio INT,
@NroSemana INT
AS
BEGIN
	DECLARE @T_Prueba TABLE (Nro VARCHAR(20), Operacion VARCHAR(50), Placa VARCHAR(50), TipoUnidad VARCHAR(250), Marca VARCHAR(70),
							 MttoPreventivo VARCHAR(30), FechaProgramada VARCHAR(70), FCInicio VARCHAR(70), FCFin VARCHAR(70), NroSemana VARCHAR(10),
							 Estado VARCHAR(30), Observacion VARCHAR(250), FechaCumplimiento VARCHAR(70), FechaIngreso VARCHAR(70), ObservacionOP VARCHAR(250),
							 SAB VARCHAR(15), DOM VARCHAR(15), LUN VARCHAR(15), MAR VARCHAR(15), MIE VARCHAR(15), JUE VARCHAR(15), VIE VARCHAR(15))
	
	DECLARE @SAB VARCHAR(15), @DOM VARCHAR(15), @LUN VARCHAR(15), @MAR VARCHAR(15), @MIE VARCHAR(15), @JUE VARCHAR(15), @VIE VARCHAR(15)

	INSERT INTO @T_Prueba (Nro, Operacion, Placa, TipoUnidad, Marca, MttoPreventivo, FechaProgramada, FCInicio, FCFin, NroSemana, Estado, FechaCumplimiento,
						   Observacion, FechaIngreso, ObservacionOP)
	VALUES ('Nro', 'Operacion', '<PLACA>', 'TipoUnidad', 'Marca', 'MttoPreventivo', 'FechaProgramada', 'FCInicio', 'FCFin', 'NroSemana', 'Estado',
			'FechaCumplimiento', 'Observacion','FechaIngreso','ObservacionOP')
	
	INSERT INTO @T_Prueba (Nro,Operacion,Placa,TipoUnidad,Marca,MttoPreventivo,FechaProgramada,FCInicio,FCFin,NroSemana,Estado,Observacion,FechaCumplimiento,FechaIngreso,ObservacionOP,SAB,DOM,LUN,MAR,MIE,JUE,VIE)
	SELECT CONVERT(VARCHAR,Nro), Operacion, Placa, TipoUnidad, Marca, MttoPreventivo, CONVERT(VARCHAR,FechaProgramada,103), CONVERT(VARCHAR,FCInicio,103),
	CONVERT(VARCHAR,FCFin,103), CONVERT(VARCHAR,NroSemana), Estado, Observacion, CONVERT(VARCHAR,FechaCumplimiento,103), CONVERT(VARCHAR,FechaIngreso,103),
	ObservacionOP, SAB, DOM, LUN, MAR, MIE, JUE, VIE
	FROM ReportesApp_Mantenimiento_MttoPreventivo_ReporteCumplimiento
	WHERE YEAR(FCFin) = @Anio AND NroSemana = @NroSemana

	DECLARE @FCInicio DATE = (SELECT TOP(1) FCInicio FROM ReportesApp_Mantenimiento_MttoPreventivo_ReporteCumplimiento WHERE YEAR(FCFin) = @Anio AND NroSemana = @NroSemana)
	DECLARE @FCFin DATE = (SELECT TOP(1) FCFin FROM ReportesApp_Mantenimiento_MttoPreventivo_ReporteCumplimiento WHERE YEAR(FCFin) = @Anio AND NroSemana = @NroSemana)

	WHILE (@FCInicio <= @FCFin) BEGIN
		DECLARE @Concatenado VARCHAR(15) = LEFT(UPPER(DATENAME(WEEKDAY, @FCInicio)), 3) + ''  + RIGHT( '0' + CAST(DAY(@FCInicio) AS VARCHAR(2)),2)

		UPDATE @T_Prueba
		SET SAB = CASE WHEN DATEPART(W,@FCInicio) = 6 THEN @Concatenado ELSE SAB END,
			DOM = CASE WHEN DATEPART(W,@FCInicio) = 7 THEN @Concatenado ELSE DOM END,
			LUN = CASE WHEN DATEPART(W,@FCInicio) = 1 THEN @Concatenado ELSE LUN END,
			MAR = CASE WHEN DATEPART(W,@FCInicio) = 2 THEN @Concatenado ELSE MAR END,
			MIE = CASE WHEN DATEPART(W,@FCInicio) = 3 THEN @Concatenado ELSE MIE END,
			JUE = CASE WHEN DATEPART(W,@FCInicio) = 4 THEN @Concatenado ELSE JUE END,
			VIE = CASE WHEN DATEPART(W,@FCInicio) = 5 THEN @Concatenado ELSE VIE END
		WHERE Placa = '<PLACA>'

		SET @FCInicio = (SELECT DATEADD(DAY,1,@FCInicio))
	END

	SET @SAB = (SELECT SAB FROM @T_Prueba WHERE Placa = '<PLACA>')
	SET @DOM = (SELECT DOM FROM @T_Prueba WHERE Placa = '<PLACA>')
	SET @LUN = (SELECT LUN FROM @T_Prueba WHERE Placa = '<PLACA>')
	SET @MAR = (SELECT MAR FROM @T_Prueba WHERE Placa = '<PLACA>')
	SET @MIE = (SELECT MIE FROM @T_Prueba WHERE Placa = '<PLACA>')
	SET @JUE = (SELECT JUE FROM @T_Prueba WHERE Placa = '<PLACA>')
	SET @VIE = (SELECT VIE FROM @T_Prueba WHERE Placa = '<PLACA>')

	DECLARE @SQL VARCHAR(5000)
	CREATE TABLE #PruebaView (Nro VARCHAR(20), Operacion VARCHAR(50), Placa VARCHAR(50), TipoUnidad VARCHAR(250), Marca VARCHAR(70),
							 MttoPreventivo VARCHAR(30), FechaProgramada VARCHAR(70), FCInicio VARCHAR(70), FCFin VARCHAR(70), NroSemana VARCHAR(10),
							 Estado VARCHAR(30), Observacion VARCHAR(250), FechaCumplimiento VARCHAR(70), FechaIngreso VARCHAR(70), ObservacionOP VARCHAR(250),
							 SAB VARCHAR(15), DOM VARCHAR(15), LUN VARCHAR(15), MAR VARCHAR(15), MIE VARCHAR(15), JUE VARCHAR(15), VIE VARCHAR(15))

	INSERT INTO #PruebaView
	SELECT * FROM @T_Prueba WHERE Placa <> '<PLACA>'

	SET @SQL = 'SELECT Nro, Placa AS PLACA, Operacion AS OPERACION, TipoUnidad AS TIPO_UNIDAD, Marca AS MARCA, MttoPreventivo AS MTTO_PREVENTIVO,
				FechaProgramada AS FECHA_PROG, NroSemana AS SEMANA, FCInicio AS FECHA_INICIO, FCFin AS FECHA_FIN, SAB AS '+@SAB+', DOM AS '+@DOM+',
				LUN AS '+@LUN+', MAR AS '+@MAR+', MIE AS '+@MIE+', JUE AS '+@JUE+', VIE AS '+@VIE+', FechaIngreso AS FECHA_INGRESO, Estado AS ESTADO,
				FechaCumplimiento AS FECHA_EJECUCION, Observacion AS OBSERVACION, ObservacionOP AS OBSERVACION_OP FROM #PruebaView ORDER BY CONVERT(DATE,FechaProgramada)'
	EXEC (@SQL)
	DROP TABLE #PruebaView
END

-----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 10-01-2023
-- Description:	GENERAR REGISTRO DE MANTENIMIENTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_GenerarModificarMtto]
@Opcion INT,
@idRegistro INT,
@idVehiculo INT,
@idTipoVehiculo INT,
@Aceite VARCHAR(10),
@Frecuencia INT,
@UltimaFecha DATETIME,
@UltimoKM DECIMAL(10,2),
@TipoMantenimiento VARCHAR(10),
@idMttoOP INT,
@PS INT,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT
DECLARE @correlativoKM INT
DECLARE @idOperacion INT
DECLARE @Placa VARCHAR(20)

BEGIN TRAN
BEGIN TRY
	SET @idOperacion = (SELECT IdProgramacion FROM ReportesApp_Operacion_MaestroUnidadesConductor WHERE IdUnidad = @idVehiculo)
	
	IF (@Opcion = 1) BEGIN
		IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_Registro WHERE idVehiculo = @idVehiculo)) BEGIN
			SET @Exito = '-1 = Esta unidad ya ha sido registrada.'
			ROLLBACK
			GOTO Terminar
		END

		SET @correlativoKM = (SELECT MAX(idKilometraje) FROM ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas)
		SET @correlativoKM = ISNULL(@correlativoKM,0) + 1
		
		SET @correlativo = (SELECT MAX(idRegistro) FROM ReportesApp_Mantenimiento_MttoPreventivo_Registro)
		SET @correlativo = ISNULL(@correlativo,0) + 1

		SET @Placa = (SELECT NumeroPlaca FROM OP_TR_Vehiculo WHERE IdVehiculo = @idVehiculo)

		IF (@idTipoVehiculo != 2) BEGIN
			INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas(idKilometraje,Estado,idVehiculo,Placa,Fecha,KMActual,ValorAdicional)
			VALUES(@correlativoKM,'OPERATIVO',@idVehiculo,@Placa,GETDATE(),@UltimoKM,0)

			INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_KMRegistro(idKilometraje,Fecha,Kilometraje)
			VALUES(@correlativoKM,GETDATE(),@UltimoKM)

			INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_Registro(idRegistro,idVehiculo,Aceite,Frecuencia,UltimaFecha,UltimoKM,TipoMantenimiento,Usuario,FechaCreacion,idOperacion,idMantenimientoOP,PS)
			VALUES(@correlativo, @idVehiculo, @Aceite, @Frecuencia, @UltimaFecha, @UltimoKM, @TipoMantenimiento, @Usuario, GETDATE(),@idOperacion,@idMttoOP,@PS)
		END
		ELSE BEGIN
			INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_Registro(idRegistro,idVehiculo,Aceite,Frecuencia,UltimaFecha,UltimoKM,TipoMantenimiento,Usuario,FechaCreacion,idOperacion,idMantenimientoOP,PS)
			VALUES(@correlativo, @idVehiculo, '', @Frecuencia, @UltimaFecha, @UltimoKM, @TipoMantenimiento, @Usuario, GETDATE(),@idOperacion,@idMttoOP,@PS)
		END

		SET @Exito = '0 = Mantenimiento Programado Correctamente.'
	END
	
	IF (@Opcion = 2) BEGIN
		DECLARE @idVehiculo2 INT = (SELECT idVehiculo FROM ReportesApp_Mantenimiento_MttoPreventivo_Registro WHERE idRegistro = @idRegistro)
		DECLARE @Placa2 VARCHAR(20) = (SELECT NumeroPlaca FROM OP_TR_Vehiculo WHERE IdVehiculo = @idVehiculo2)

		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_Historial(idRegistro,idVehiculo,Aceite,UltimaFecha,UltimoKM,TipoMantenimiento,Usuario,FechaCreacion,idOperacion,PS)
		SELECT idRegistro,idVehiculo,Aceite,UltimaFecha,UltimoKM,TipoMantenimiento,Usuario,FechaCreacion,idOperacion,PS FROM ReportesApp_Mantenimiento_MttoPreventivo_Registro
		WHERE idRegistro = @idRegistro

		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_Registro
		SET UltimaFecha = @UltimaFecha,UltimoKM = @UltimoKM,TipoMantenimiento = @TipoMantenimiento,Frecuencia = @Frecuencia,Usuario = @Usuario,FechaCreacion = GETDATE(),
		idMantenimientoOP = @idMttoOP, PS = @PS
		WHERE idRegistro = @idRegistro

		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_ReporteCumplimiento
		SET FechaCumplimiento = CONVERT(DATE,@UltimaFecha), Estado = 'EJECUTADO'
		WHERE Placa = @Placa2 AND CONVERT(DATE,@UltimaFecha) >= FCInicio AND CONVERT(DATE,@UltimaFecha) <= FCFin

		SET @Exito = '0 = Mantenimiento Actualizado Correctamente.'
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

-----------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 25-03-2024
-- Description:	GENERAR MANTENIMIENTO MAQUINARIA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_GenerarModificarMaquina]
@Opcion INT,
@idRegistroM INT,
@MaquinaCodigo VARCHAR(50),
@Aceite VARCHAR(10),
@Frecuencia INT,
@UltimaFecha DATETIME,
@UltimoKM DECIMAL(10,2),
@TipoMantenimiento VARCHAR(10),
@idMttoOP INT,
@PS INT,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @TipoMaquina VARCHAR(4)
DECLARE @correlativo INT
DECLARE @correlativoKM INT

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN
		IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas WHERE MaquinaCodigo = @MaquinaCodigo)) BEGIN
			SET @Exito = '-1 = Esta máquina ya ha sido registrada.'
			ROLLBACK
			GOTO Terminar
		END

		SET @correlativoKM = (SELECT MAX(idKilometraje) FROM ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas)
		SET @correlativoKM = ISNULL(@correlativoKM,0) + 1
		
		SET @correlativo = (SELECT MAX(idRegistroM) FROM ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas)
		SET @correlativo = ISNULL(@correlativo,0) + 1

		SET @TipoMaquina = (SELECT TipoMaquina FROM ME_Maquina WHERE MaquinaCodigo = @MaquinaCodigo)

		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas(idKilometraje,Estado,MaquinaCodigo,TipoMaquina,Fecha,KMActual,ValorAdicional)
		VALUES(@correlativoKM,'OPERATIVO',LTRIM(RTRIM(@MaquinaCodigo)),ISNULL(@TipoMaquina,'0099'),GETDATE(),@UltimoKM,0)

		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_KMRegistroMaquinas(idKilometraje,MaquinaCodigo,Fecha,Kilometraje)
		VALUES(@correlativoKM,LTRIM(RTRIM(@MaquinaCodigo)),GETDATE(),@UltimoKM)

		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas(idRegistroM,MaquinaCodigo,Aceite,Frecuencia,UltimaFecha,UltimoKM,TipoMantenimiento,Usuario,FechaCreacion,idMantenimientoOP,PS)
		VALUES(@correlativo, LTRIM(RTRIM(@MaquinaCodigo)), @Aceite, @Frecuencia, @UltimaFecha, @UltimoKM, @TipoMantenimiento, @Usuario, GETDATE(),@idMttoOP,@PS)

		SET @Exito = '0 = Mantenimiento Programado Correctamente.'
	END
	
	IF (@Opcion = 2) BEGIN
		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_HistorialMaquinas(idRegistro,MaquinaCodigo,Aceite,UltimaFecha,UltimoKM,PS,TipoMantenimiento,Usuario,FechaCreacion)
		SELECT idRegistroM,MaquinaCodigo,Aceite,UltimaFecha,UltimoKM,PS,TipoMantenimiento,Usuario,FechaCreacion
		FROM ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas
		WHERE idRegistroM = @idRegistroM

		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas
		SET UltimaFecha = @UltimaFecha,UltimoKM = @UltimoKM,TipoMantenimiento = @TipoMantenimiento, Frecuencia = @Frecuencia,Usuario = @Usuario,FechaCreacion = GETDATE(),
		idMantenimientoOP = @idMttoOP, PS = @PS
		WHERE idRegistroM = @idRegistroM

		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_ReporteCumplimiento
		SET FechaCumplimiento = CONVERT(DATE,@UltimaFecha), Estado = 'EJECUTADO'
		WHERE Placa = @MaquinaCodigo AND CONVERT(DATE,@UltimaFecha) >= FCInicio AND CONVERT(DATE,@UltimaFecha) <= FCFin

		SET @Exito = '0 = Mantenimiento Actualizado Correctamente.'
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

-----------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 14-11-2024
-- Description: MODIFICAR REGISTROS DE CUMPLIMIENTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ProgramarCumplimiento]
@Opcion INT,
@Nro INT,
@TipoMtto VARCHAR(20),
@FechaProgramada DATE,
@Estado VARCHAR(50),
@Observacion VARCHAR(350)
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	DECLARE @FCInicio DATE = (SELECT FCInicio FROM ReportesApp_Mantenimiento_MttoPreventivo_ReporteCumplimiento WHERE Nro = @Nro)
	DECLARE @FCFin DATE = (SELECT FCFin FROM ReportesApp_Mantenimiento_MttoPreventivo_ReporteCumplimiento WHERE Nro = @Nro)

	IF (@Opcion = 1) BEGIN		-- PROGRAMAR CUMPLIMIENTO
		IF (@FechaProgramada < @FCInicio OR @FechaProgramada > @FCFin) BEGIN
			SET @Exito = '-1 = No puede programar este mantenimiento en un día que no corresponda a esta semana.'
			ROLLBACK
			GOTO Terminar
		END

		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_ReporteCumplimiento
		SET SAB = NULL, DOM = NULL, LUN = NULL, MAR = NULL, MIE = NULL, JUE = NULL, VIE = NULL
		WHERE Nro = @Nro

		IF (@Estado = 'EJECUTADO') BEGIN
			UPDATE ReportesApp_Mantenimiento_MttoPreventivo_ReporteCumplimiento
			SET FechaCumplimiento = @FechaProgramada, Estado = @Estado, Observacion = @Observacion,
			SAB = CASE WHEN DATEPART(W,@FechaProgramada) = 6 AND @FechaProgramada >= FCInicio AND @FechaProgramada <= FCFin THEN @TipoMtto ELSE SAB END,
			DOM = CASE WHEN DATEPART(W,@FechaProgramada) = 7 AND @FechaProgramada >= FCInicio AND @FechaProgramada <= FCFin THEN @TipoMtto ELSE DOM END,
			LUN = CASE WHEN DATEPART(W,@FechaProgramada) = 1 AND @FechaProgramada >= FCInicio AND @FechaProgramada <= FCFin THEN @TipoMtto ELSE LUN END,
			MAR = CASE WHEN DATEPART(W,@FechaProgramada) = 2 AND @FechaProgramada >= FCInicio AND @FechaProgramada <= FCFin THEN @TipoMtto ELSE MAR END,
			MIE = CASE WHEN DATEPART(W,@FechaProgramada) = 3 AND @FechaProgramada >= FCInicio AND @FechaProgramada <= FCFin THEN @TipoMtto ELSE MIE END,
			JUE = CASE WHEN DATEPART(W,@FechaProgramada) = 4 AND @FechaProgramada >= FCInicio AND @FechaProgramada <= FCFin THEN @TipoMtto ELSE JUE END,
			VIE = CASE WHEN DATEPART(W,@FechaProgramada) = 5 AND @FechaProgramada >= FCInicio AND @FechaProgramada <= FCFin THEN @TipoMtto ELSE VIE END
			WHERE Nro = @Nro
		END
		ELSE BEGIN
			UPDATE ReportesApp_Mantenimiento_MttoPreventivo_ReporteCumplimiento
			SET Estado = @Estado, Observacion = @Observacion,
			SAB = CASE WHEN DATEPART(W,@FechaProgramada) = 6 AND @FechaProgramada >= FCInicio AND @FechaProgramada <= FCFin THEN @TipoMtto ELSE SAB END,
			DOM = CASE WHEN DATEPART(W,@FechaProgramada) = 7 AND @FechaProgramada >= FCInicio AND @FechaProgramada <= FCFin THEN @TipoMtto ELSE DOM END,
			LUN = CASE WHEN DATEPART(W,@FechaProgramada) = 1 AND @FechaProgramada >= FCInicio AND @FechaProgramada <= FCFin THEN @TipoMtto ELSE LUN END,
			MAR = CASE WHEN DATEPART(W,@FechaProgramada) = 2 AND @FechaProgramada >= FCInicio AND @FechaProgramada <= FCFin THEN @TipoMtto ELSE MAR END,
			MIE = CASE WHEN DATEPART(W,@FechaProgramada) = 3 AND @FechaProgramada >= FCInicio AND @FechaProgramada <= FCFin THEN @TipoMtto ELSE MIE END,
			JUE = CASE WHEN DATEPART(W,@FechaProgramada) = 4 AND @FechaProgramada >= FCInicio AND @FechaProgramada <= FCFin THEN @TipoMtto ELSE JUE END,
			VIE = CASE WHEN DATEPART(W,@FechaProgramada) = 5 AND @FechaProgramada >= FCInicio AND @FechaProgramada <= FCFin THEN @TipoMtto ELSE VIE END
			WHERE Nro = @Nro
		END

		SET @Exito = '0 = Registro actualizado correctamente.'
	END

	IF (@Opcion = 2) BEGIN		-- PROGRAMAR INGRESO
		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_ReporteCumplimiento
		SET FechaIngreso = @FechaProgramada
		WHERE Nro = @Nro

		SET @Exito = '0 = Fecha registrada correctamente.'
	END

	IF (@Opcion = 3) BEGIN		-- INGRESAR OBSERVACION - MTTO
		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_ReporteCumplimiento
		SET ObservacionOP = @Observacion
		WHERE Nro = @Nro

		SET @Exito = '0 = Observación añadida correctamente.'
	END

	IF (@Opcion = 4) BEGIN		-- INGRESAR OBSERVACION - INSPECCION
		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cumplimiento
		SET ObservacionOP = @Observacion
		WHERE Nro = @Nro

		SET @Exito = '0 = Observación añadida correctamente.'
	END

	IF (@Opcion = 5) BEGIN		-- INGRESAR OBSERVACION - ACTIVIDAD
		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto_Cumplimiento
		SET ObservacionOP = @Observacion
		WHERE Nro = @Nro

		SET @Exito = '0 = Observación añadida correctamente.'
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

-----------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 23-11-2024
-- Description:	CONTADOR DE UNIDADES PROGRAMADAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ContarMttosProgramados]
@Anio INT,
@NroSemana INT
AS
BEGIN
	DECLARE @TotalMttoProg INT = (SELECT COUNT(*) FROM ReportesApp_Mantenimiento_MttoPreventivo_ReporteCumplimiento
	WHERE YEAR(FCFin) = @Anio AND NroSemana = @NroSemana)

	DECLARE @TotalMttoEjec INT = (SELECT COUNT(*) FROM ReportesApp_Mantenimiento_MttoPreventivo_ReporteCumplimiento
	WHERE Estado = 'EJECUTADO' AND YEAR(FCFin) = @Anio AND NroSemana = @NroSemana)

	SELECT @TotalMttoProg AS 'TOTAL_UNIDADES', @TotalMttoEjec AS 'TOTAL_EJECUTADOS'
END

-----------------------------------------------------------------------------------

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
			THEN 'PROGRAMADO' ELSE 'NO PROGRAMADO' END) AS 'PROGRAMACION_VIAJE', SM.Estado AS 'ESTADO', (SELECT TOP(1) Destino FROM ReportesApp_Operacion_Previaje_Registros WHERE CONVERT(DATE,FechaProgramacion) = CONVERT(DATE,GETDATE())
			AND (Estado = 1) AND (TipoProgramacion IN (1,2,3,4)) AND (idTracto = V.IdVehiculo OR idSemirremolque = V.IdVehiculo)) AS 'DESTINO', B.DescripcionBase AS 'UBICACION', (SELECT TOP(1) Observacion
			FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle WHERE idSolicitud = SM.idSolicitud) AS 'MOTIVO', CASE WHEN ((SELECT Estado FROM ReportesApp_Mantenimiento_Solicitud_PedidoRepuesto
			WHERE idSolicitud = SM.idSolicitud) = 'PEDIDO') THEN 'LOG' ELSE 'MTTO' END AS 'RESPONSABLE', CONVERT(VARCHAR,SM.FechaRecepcion,103) AS 'FECHA_INICIO',
			CASE WHEN SM.Estado = 'REPROGRAMADO' THEN CONVERT(VARCHAR,SM.FechaReprog,103) ELSE CONVERT(VARCHAR,SM.FechaProg,103) END AS 'FECHA_PROYECTADA',
			DATEDIFF(DAY,SM.FechaRecepcion,GETDATE()) AS 'DÍAS', P.Descripcion AS 'PEDIDO', CONVERT(VARCHAR,P.FechaPedido,103) + ' ' + CONVERT(VARCHAR,P.FechaPedido,8) AS 'FECHA_SOLICITADA', 
			CONVERT(VARCHAR,P.FechaLlegadaP,103) + ' ' + CONVERT(VARCHAR,P.FechaLlegadaP,8) AS 'FECHA_LLEGADA', P.Estado AS 'ESTADO_PEDIDO',
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
			THEN 'PROGRAMADO' ELSE 'NO PROGRAMADO' END) AS 'PROGRAMACION_VIAJE', SM.Estado AS 'ESTADO', (SELECT TOP(1) Destino FROM ReportesApp_Operacion_Previaje_Registros WHERE CONVERT(DATE,FechaProgramacion) = CONVERT(DATE,GETDATE())
			AND (Estado = 1) AND (TipoProgramacion IN (1,2,3,4)) AND (idTracto = V.IdVehiculo OR idSemirremolque = V.IdVehiculo)) AS 'DESTINO', B.DescripcionBase AS 'UBICACION', (SELECT TOP(1) Observacion
			FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle WHERE idSolicitud = SM.idSolicitud) AS 'MOTIVO', CASE WHEN ((SELECT Estado FROM ReportesApp_Mantenimiento_Solicitud_PedidoRepuesto
			WHERE idSolicitud = SM.idSolicitud) = 'PEDIDO') THEN 'LOG' ELSE 'MTTO' END AS 'RESPONSABLE', CONVERT(VARCHAR,SM.FechaRecepcion,103) AS 'FECHA_INICIO',
			CASE WHEN SM.Estado = 'REPROGRAMADO' THEN CONVERT(VARCHAR,SM.FechaReprog,103) ELSE CONVERT(VARCHAR,SM.FechaProg,103) END AS 'FECHA_PROYECTADA',
			DATEDIFF(DAY,SM.FechaRecepcion,GETDATE()) AS 'DÍAS', P.Descripcion AS 'PEDIDO', CONVERT(VARCHAR,P.FechaPedido,103) + ' ' + CONVERT(VARCHAR,P.FechaPedido,8) AS 'FECHA_SOLICITADA',
			CONVERT(VARCHAR,P.FechaLlegadaP,103) + ' ' + CONVERT(VARCHAR,P.FechaLlegadaP,8) AS 'FECHA_LLEGADA', P.Estado AS 'ESTADO_PEDIDO',
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
			THEN 'PROGRAMADO' ELSE 'NO PROGRAMADO' END) AS 'PROGRAMACION_VIAJE', SM.Estado AS 'ESTADO', (SELECT TOP(1) Destino FROM ReportesApp_Operacion_Previaje_Registros WHERE CONVERT(DATE,FechaProgramacion) = CONVERT(DATE,GETDATE())
			AND (Estado = 1) AND (TipoProgramacion IN (1,2,3,4)) AND (idTracto = V.IdVehiculo OR idSemirremolque = V.IdVehiculo)) AS 'DESTINO', B.DescripcionBase AS 'UBICACION', (SELECT TOP(1) Observacion
			FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle WHERE idSolicitud = SM.idSolicitud) AS 'MOTIVO', CASE WHEN ((SELECT Estado FROM ReportesApp_Mantenimiento_Solicitud_PedidoRepuesto
			WHERE idSolicitud = SM.idSolicitud) = 'PEDIDO') THEN 'LOG' ELSE 'MTTO' END AS 'RESPONSABLE', CONVERT(VARCHAR,SM.FechaRecepcion,103) AS 'FECHA_INICIO',
			CASE WHEN SM.Estado = 'REPROGRAMADO' THEN CONVERT(VARCHAR,SM.FechaReprog,103) ELSE CONVERT(VARCHAR,SM.FechaProg,103) END AS 'FECHA_PROYECTADA',
			DATEDIFF(DAY,SM.FechaRecepcion,GETDATE()) AS 'DÍAS', P.Descripcion AS 'PEDIDO', CONVERT(VARCHAR,P.FechaPedido,103) + ' ' + CONVERT(VARCHAR,P.FechaPedido,8) AS 'FECHA_SOLICITADA',
			CONVERT(VARCHAR,P.FechaLlegadaP,103) + ' ' + CONVERT(VARCHAR,P.FechaLlegadaP,8) AS 'FECHA_LLEGADA', P.Estado AS 'ESTADO_PEDIDO',
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
			THEN 'PROGRAMADO' ELSE 'NO PROGRAMADO' END) AS 'PROGRAMACION_VIAJE', SM.Estado AS 'ESTADO', (SELECT TOP(1) Destino FROM ReportesApp_Operacion_Previaje_Registros WHERE CONVERT(DATE,FechaProgramacion) = CONVERT(DATE,GETDATE())
			AND (Estado = 1) AND (TipoProgramacion IN (1,2,3,4)) AND (idTracto = V.IdVehiculo OR idSemirremolque = V.IdVehiculo)) AS 'DESTINO', B.DescripcionBase AS 'UBICACION', (SELECT TOP(1) Observacion
			FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle WHERE idSolicitud = SM.idSolicitud) AS 'MOTIVO', CASE WHEN ((SELECT Estado FROM ReportesApp_Mantenimiento_Solicitud_PedidoRepuesto
			WHERE idSolicitud = SM.idSolicitud) = 'PEDIDO') THEN 'LOG' ELSE 'MTTO' END AS 'RESPONSABLE', CONVERT(VARCHAR,SM.FechaRecepcion,103) AS 'FECHA_INICIO',
			CASE WHEN SM.Estado = 'REPROGRAMADO' THEN CONVERT(VARCHAR,SM.FechaReprog,103) ELSE CONVERT(VARCHAR,SM.FechaProg,103) END AS 'FECHA_PROYECTADA',
			DATEDIFF(DAY,SM.FechaRecepcion,GETDATE()) AS 'DÍAS', P.Descripcion AS 'PEDIDO', CONVERT(VARCHAR,P.FechaPedido,103) + ' ' + CONVERT(VARCHAR,P.FechaPedido,8) AS 'FECHA_SOLICITADA',
			CONVERT(VARCHAR,P.FechaLlegadaP,103) + ' ' + CONVERT(VARCHAR,P.FechaLlegadaP,8) AS 'FECHA_LLEGADA', P.Estado AS 'ESTADO_PEDIDO',
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

-----------------------------------------------------------------------------
-----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ConductorUnidades_CreaModifica]
@Accion INT,
@IdRegistro INT,
@Idunidad INT,
@IdOperacion INT,
@Observacion TEXT,
@Usuario VARCHAR(30),
@Mochila INT,
@Tomafuerza INT,
@Urea INT,
@Manguera INT,
@Transmision VARCHAR(12),
@Peso DECIMAL(10,2),
@Galones DECIMAL(10,2),
@TipoCortina VARCHAR(40),
@ModeloChasis VARCHAR(40),
@Nivel VARCHAR(40),
@TipoNivel VARCHAR(40),
@Suspension VARCHAR(40),
@Piso VARCHAR(40),
@MaterialPiso VARCHAR(40)
AS
DECLARE @correlativo INT
DECLARE @TipoUnidad INT
DECLARE @SubTipoUnidad INT
DECLARE @exito VARCHAR(MAX)	

BEGIN TRAN
BEGIN TRY
	--VALIDACIONES PARA EVITAR LA DUPLICIDAD DE ASIGNACIONES
	IF EXISTS(SELECT * FROM ReportesApp_Operacion_MaestroUnidadesConductor WHERE IdUnidad = @Idunidad AND IdRegistro <> @IdRegistro) BEGIN
		SET @Exito = '-1=La UT ya se encuentra registrada...¡'
		ROLLBACK
		GOTO Terminar
	END
	-----------------------------------------------------------------------------------------
	IF (@Accion = 1) BEGIN	
		SET @correlativo = (SELECT MAX(IdRegistro) FROM ReportesApp_Operacion_MaestroUnidadesConductor)
		SET @correlativo = ISNULL(@correlativo,0) + 1

		SET @TipoUnidad = (SELECT TipoVehiculo FROM OP_TR_Vehiculo WHERE IdVehiculo = @Idunidad)
		SET @SubTipoUnidad = (SELECT SubTipoVehiculo FROM OP_TR_Vehiculo WHERE IdVehiculo = @Idunidad)
		
		IF ((SELECT TipoVehiculo FROM OP_TR_Vehiculo WHERE IdVehiculo = @Idunidad) = 1) BEGIN
			INSERT INTO ReportesApp_Operacion_MaestroUnidadesConductor(IdRegistro,FechaRegistro,IdUnidad,IdProgramacion,Observacion,UsuarioCrea,FechaCrea,
																	   Mochila,Tomafuerza,Urea,Manguera,Transmision,Peso,Galones)
			VALUES(@correlativo,GETDATE(),@Idunidad,@IdOperacion,@Observacion,@Usuario,GETDATE(),@Mochila,@Tomafuerza,@Urea,@Manguera,@Transmision,@Peso,@Galones)
		END
		ELSE BEGIN
			INSERT INTO ReportesApp_Operacion_MaestroUnidadesConductor(IdRegistro,FechaRegistro,IdUnidad,IdProgramacion,Observacion,UsuarioCrea,FechaCrea,Peso,Galones,
																	   TipoCortina,ModeloChasis,Nivel,TipoNivel,Suspension,Piso,MaterialPiso)
			VALUES(@correlativo,GETDATE(),@Idunidad,@IdOperacion,@Observacion,@Usuario,GETDATE(),@Peso,@Galones,@TipoCortina,@ModeloChasis,@Nivel,@TipoNivel,
				   @Suspension,@Piso,@MaterialPiso)
		END

		UPDATE ReportesApp_Operacion_MaestroUnidadesConductor
		SET TipoUnidad = @TipoUnidad, SubTipoUnidad = @SubTipoUnidad
		WHERE IdUnidad = @Idunidad

		SET @exito='0= Registro Exitoso...¡'
	END

	IF (@Accion = 2) BEGIN 
		IF (@Idunidad = -1) BEGIN
		 SET @Idunidad = NULL
		END

		SET @TipoUnidad = (SELECT TipoVehiculo FROM OP_TR_Vehiculo WHERE IdVehiculo = @Idunidad)
		SET @SubTipoUnidad = (SELECT SubTipoVehiculo FROM OP_TR_Vehiculo WHERE IdVehiculo = @Idunidad)
	
		IF ((SELECT TipoUnidad FROM ReportesApp_Operacion_MaestroUnidadesConductor WHERE IdRegistro = @IdRegistro) = 1) BEGIN
			UPDATE ReportesApp_Operacion_MaestroUnidadesConductor
			SET IdUnidad = @Idunidad,IdProgramacion = @IdOperacion,Observacion=@Observacion, UsuarioModifica= @Usuario,FechaModifica= GETDATE(),
			Mochila = @Mochila,TomaFuerza = @Tomafuerza, Urea = @Urea, Manguera = @Manguera, Transmision = @Transmision, TipoUnidad = @TipoUnidad,
			SubTipoUnidad = @SubTipoUnidad, Peso = @Peso, Galones = @Galones
			WHERE IdRegistro = @IdRegistro
		END
		ELSE BEGIN
			UPDATE ReportesApp_Operacion_MaestroUnidadesConductor
			SET IdUnidad = @Idunidad,IdProgramacion = @IdOperacion,Observacion=@Observacion, UsuarioModifica = @Usuario,FechaModifica= GETDATE(),
			TipoUnidad = @TipoUnidad, SubTipoUnidad = @SubTipoUnidad, Peso = @Peso, Galones = @Galones, TipoCortina = @TipoCortina, ModeloChasis = @ModeloChasis,
			Nivel = @Nivel, TipoNivel = @TipoNivel, Suspension = @Suspension, Piso = @Piso, MaterialPiso = @MaterialPiso
			WHERE IdRegistro = @IdRegistro
		END

		INSERT INTO ReportesApp_Operacion_MaestroUnidadConductor_Historico (idregistro,FechaRegistro,IdUnidad,Observacion,UsuarioModifica,FechaModifica)
		VALUES(@IdRegistro,GETDATE(),@Idunidad,@Observacion,@Usuario,GETDATE())
	
		SET @exito='0= Actualización Exitosa...¡'
	END
	
	/*IF HOST_NAME() IN ('TI01-GT')
	BEGIN
		SET @exito = '777= TEST OK'
		ROLLBACK
		GOTO Terminar
	END*/
		
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
-- Create date: 03-01-2024
-- Description:	LISTAR CONDUCTOR UNIDADES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ConductorUnidades_Listar]
@TipoVehiculo INT,
@SubTipoVehiculo INT,
@idOperacion INT,
@Placa VARCHAR(10)
AS
BEGIN
	DECLARE @TEMP_ULTIV TABLE (NRO_TICKET INT, ID_UNIDAD INT, FECHA_PROG DATETIME)

	INSERT @TEMP_ULTIV (NRO_TICKET, ID_UNIDAD, FECHA_PROG)
	SELECT R1.NroTicket, R1.idTracto, R1.FechaProgramacion FROM ReportesApp_Operacion_Previaje_Registros R1
	WHERE R1.NroTicket = (SELECT TOP(1) R2.NroTicket FROM ReportesApp_Operacion_Previaje_Registros R2 WHERE R2.idTracto = R1.idTracto AND R2.Estado = 9
	ORDER BY R2.FechaProgramacion DESC)
	UNION
	SELECT R1.NroTicket, R1.idSemirremolque, R1.FechaProgramacion FROM ReportesApp_Operacion_Previaje_Registros R1
	WHERE R1.NroTicket = (SELECT TOP(1) R2.NroTicket FROM ReportesApp_Operacion_Previaje_Registros R2 WHERE R2.idSemirremolque = R1.idSemirremolque AND R2.Estado = 9
	AND R2.idSemirremolque NOT IN (1274,2191,3829,5135,2396,4915,3233,4890,1266) ORDER BY R2.FechaProgramacion DESC)

	IF (@TipoVehiculo = 0 AND @SubTipoVehiculo = 0 AND @idOperacion = 5) BEGIN
		SELECT IdRegistro 'N°',uc.IdUnidad 'ID',FechaRegistro 'FECHA', CASE WHEN V.Estado = 2 THEN 'OPERATIVO' ELSE 'INOPERATIVO' END AS 'ESTADO',
		CASE WHEN BL.IdBloqueo IS NULL THEN 'NO' ELSE 'SÍ' END AS 'BLOQUEADO', V.NumeroPlaca 'UNIDAD', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', 
		V.Modelo AS 'MODELO', M.AnoAdquisicion AS 'AÑO', TV.idTipoVehiculo, TV.Descripcion AS 'TIPO_UNIDAD', SV.SubTipoVehiculo, SV.Descripcion AS 'SUBTIPO_UNIDAD',
		ISNULL(Transmision,'') AS 'TRANSMISION', ISNULL(uc.IdProgramacion,-1) IDOPERACION,
		CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION_UNIDAD', UC.Peso AS 'PESO',
		CASE WHEN ISNULL(UC.MOCHILA,0)<1 THEN 0 ELSE 1 END AS 'MOCHILA', UC.Galones AS 'GALONES',
		CASE WHEN ISNULL(UC.TOMAFUERZA,0)<1 THEN 0 ELSE 1 END AS 'TOMAFUERZA',
		CASE WHEN ISNULL(UC.UREA,0)<1 THEN 0 ELSE 1 END AS 'UREA',
		CASE WHEN ISNULL(UC.MANGUERA,0)<1 THEN 0 ELSE 1 END AS 'MANGUERA DE AIRE', UC.Observacion OBSERVACION, U.NRO_TICKET AS 'ULTIMA_PROG',
		U.FECHA_PROG AS 'ULTIMA_FECHA', UC.TipoCortina AS 'CORTINA', UC.ModeloChasis AS 'CHASIS', UC.Nivel AS 'NIVEL', UC.TipoNivel AS 'TIPO_NIVEL', UC.Suspension AS 'SUSPENSION',
		UC.Piso AS 'PISO', UC.MaterialPiso AS 'MATERIAL_PISO', UC.UsuarioCrea 'USUARIO_CREA', UC.FechaCrea 'FECHA_CREA', UC.UsuarioModifica 'USUARIO_MODIFICA',UC.FechaModifica 'FECHA_MODIFICA'
		FROM ReportesApp_Operacion_MaestroUnidadesConductor UC
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
			LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TIPOVEHICULO
			LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
			LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = UC.IdProgramacion
			LEFT JOIN ReportesApp_Operacion_UnidadesBloqueadas BL ON BL.IdUnidad = UC.IdUnidad
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(V.NumeroPlaca))
			LEFT JOIN @TEMP_ULTIV U ON U.ID_UNIDAD = UC.IdUnidad
		WHERE (V.Estado = 2) AND (@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%')
		ORDER BY IdRegistro DESC
	END

	IF (@TipoVehiculo != 0 AND @SubTipoVehiculo = 0 AND @idOperacion = 5) BEGIN
		SELECT IdRegistro 'N°',uc.IdUnidad 'ID',FechaRegistro 'FECHA', CASE WHEN V.Estado = 2 THEN 'OPERATIVO' ELSE 'INOPERATIVO' END AS 'ESTADO',
		CASE WHEN BL.IdBloqueo IS NULL THEN 'NO' ELSE 'SÍ' END AS 'BLOQUEADO', V.NumeroPlaca 'UNIDAD', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', 
		V.Modelo AS 'MODELO', M.AnoAdquisicion AS 'AÑO', TV.idTipoVehiculo, TV.Descripcion AS 'TIPO_UNIDAD', SV.SubTipoVehiculo, SV.Descripcion AS 'SUBTIPO_UNIDAD',
		ISNULL(Transmision,'') AS 'TRANSMISION', ISNULL(uc.IdProgramacion,-1) IDOPERACION,
		CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION_UNIDAD', UC.Peso AS 'PESO',
		CASE WHEN ISNULL(UC.MOCHILA,0)<1 THEN 0 ELSE 1 END AS 'MOCHILA', UC.Galones AS 'GALONES',
		CASE WHEN ISNULL(UC.TOMAFUERZA,0)<1 THEN 0 ELSE 1 END AS 'TOMAFUERZA',
		CASE WHEN ISNULL(UC.UREA,0)<1 THEN 0 ELSE 1 END AS 'UREA',
		CASE WHEN ISNULL(UC.MANGUERA,0)<1 THEN 0 ELSE 1 END AS 'MANGUERA DE AIRE', UC.Observacion OBSERVACION, U.NRO_TICKET AS 'ULTIMA_PROG',
		U.FECHA_PROG AS 'ULTIMA_FECHA', UC.TipoCortina AS 'CORTINA', UC.ModeloChasis AS 'CHASIS', UC.Nivel AS 'NIVEL', UC.TipoNivel AS 'TIPO_NIVEL', UC.Suspension AS 'SUSPENSION',
		UC.Piso AS 'PISO', UC.MaterialPiso AS 'MATERIAL_PISO', UC.UsuarioCrea 'USUARIO_CREA', UC.FechaCrea 'FECHA_CREA', UC.UsuarioModifica 'USUARIO_MODIFICA',UC.FechaModifica 'FECHA_MODIFICA'
		FROM ReportesApp_Operacion_MaestroUnidadesConductor UC
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
			LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TIPOVEHICULO
			LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
			LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = UC.IdProgramacion 
			LEFT JOIN ReportesApp_Operacion_UnidadesBloqueadas BL ON BL.IdUnidad = UC.IdUnidad
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(V.NumeroPlaca))
			LEFT JOIN @TEMP_ULTIV U ON U.ID_UNIDAD = UC.IdUnidad
		WHERE (TV.idTipoVehiculo = @TipoVehiculo) AND (V.Estado = 2) AND (@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%')
		ORDER BY IdRegistro DESC
	END

	IF (@TipoVehiculo != 0 AND @SubTipoVehiculo = 0 AND @idOperacion != 5) BEGIN
		SELECT IdRegistro 'N°',uc.IdUnidad 'ID',FechaRegistro 'FECHA', CASE WHEN V.Estado = 2 THEN 'OPERATIVO' ELSE 'INOPERATIVO' END AS 'ESTADO',
		CASE WHEN BL.IdBloqueo IS NULL THEN 'NO' ELSE 'SÍ' END AS 'BLOQUEADO', V.NumeroPlaca 'UNIDAD', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', 
		V.Modelo AS 'MODELO', M.AnoAdquisicion AS 'AÑO', TV.idTipoVehiculo, TV.Descripcion AS 'TIPO_UNIDAD', SV.SubTipoVehiculo, SV.Descripcion AS 'SUBTIPO_UNIDAD',
		ISNULL(Transmision,'') AS 'TRANSMISION', ISNULL(uc.IdProgramacion,-1) IDOPERACION,
		CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION_UNIDAD', UC.Peso AS 'PESO',
		CASE WHEN ISNULL(UC.MOCHILA,0)<1 THEN 0 ELSE 1 END AS 'MOCHILA', UC.Galones AS 'GALONES',
		CASE WHEN ISNULL(UC.TOMAFUERZA,0)<1 THEN 0 ELSE 1 END AS 'TOMAFUERZA',
		CASE WHEN ISNULL(UC.UREA,0)<1 THEN 0 ELSE 1 END AS 'UREA',
		CASE WHEN ISNULL(UC.MANGUERA,0)<1 THEN 0 ELSE 1 END AS 'MANGUERA DE AIRE', UC.Observacion OBSERVACION, U.NRO_TICKET AS 'ULTIMA_PROG',
		U.FECHA_PROG AS 'ULTIMA_FECHA', UC.TipoCortina AS 'CORTINA', UC.ModeloChasis AS 'CHASIS', UC.Nivel AS 'NIVEL', UC.TipoNivel AS 'TIPO_NIVEL', UC.Suspension AS 'SUSPENSION',
		UC.Piso AS 'PISO', UC.MaterialPiso AS 'MATERIAL_PISO', UC.UsuarioCrea 'USUARIO_CREA', UC.FechaCrea 'FECHA_CREA', UC.UsuarioModifica 'USUARIO_MODIFICA',UC.FechaModifica 'FECHA_MODIFICA'
		FROM ReportesApp_Operacion_MaestroUnidadesConductor UC
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
			LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TIPOVEHICULO
			LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
			LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = UC.IdProgramacion 
			LEFT JOIN ReportesApp_Operacion_UnidadesBloqueadas BL ON BL.IdUnidad = UC.IdUnidad
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(V.NumeroPlaca))
			LEFT JOIN @TEMP_ULTIV U ON U.ID_UNIDAD = UC.IdUnidad
		WHERE (TV.idTipoVehiculo = @TipoVehiculo) AND (uc.IdProgramacion = @idOperacion) AND (V.Estado = 2)
		AND (@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%')
		ORDER BY IdRegistro DESC
	END

	IF (@TipoVehiculo != 0 AND @SubTipoVehiculo != 0 AND @idOperacion != 5) BEGIN
		SELECT IdRegistro 'N°',uc.IdUnidad 'ID',FechaRegistro 'FECHA', CASE WHEN V.Estado = 2 THEN 'OPERATIVO' ELSE 'INOPERATIVO' END AS 'ESTADO',
		CASE WHEN BL.IdBloqueo IS NULL THEN 'NO' ELSE 'SÍ' END AS 'BLOQUEADO', V.NumeroPlaca 'UNIDAD', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', 
		V.Modelo AS 'MODELO', M.AnoAdquisicion AS 'AÑO', TV.idTipoVehiculo, TV.Descripcion AS 'TIPO_UNIDAD', SV.SubTipoVehiculo, SV.Descripcion AS 'SUBTIPO_UNIDAD',
		ISNULL(Transmision,'') AS 'TRANSMISION', ISNULL(uc.IdProgramacion,-1) IDOPERACION,
		CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION_UNIDAD', UC.Peso AS 'PESO',
		CASE WHEN ISNULL(UC.MOCHILA,0)<1 THEN 0 ELSE 1 END AS 'MOCHILA', UC.Galones AS 'GALONES',
		CASE WHEN ISNULL(UC.TOMAFUERZA,0)<1 THEN 0 ELSE 1 END AS 'TOMAFUERZA',
		CASE WHEN ISNULL(UC.UREA,0)<1 THEN 0 ELSE 1 END AS 'UREA',
		CASE WHEN ISNULL(UC.MANGUERA,0)<1 THEN 0 ELSE 1 END AS 'MANGUERA DE AIRE', UC.Observacion OBSERVACION, U.NRO_TICKET AS 'ULTIMA_PROG',
		U.FECHA_PROG AS 'ULTIMA_FECHA', UC.TipoCortina AS 'CORTINA', UC.ModeloChasis AS 'CHASIS', UC.Nivel AS 'NIVEL', UC.TipoNivel AS 'TIPO_NIVEL', UC.Suspension AS 'SUSPENSION',
		UC.Piso AS 'PISO', UC.MaterialPiso AS 'MATERIAL_PISO', UC.UsuarioCrea 'USUARIO_CREA', UC.FechaCrea 'FECHA_CREA', UC.UsuarioModifica 'USUARIO_MODIFICA',UC.FechaModifica 'FECHA_MODIFICA'
		FROM ReportesApp_Operacion_MaestroUnidadesConductor UC
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
			LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TIPOVEHICULO
			LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
			LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON  O.IdOperacion = UC.IdProgramacion
			LEFT JOIN ReportesApp_Operacion_UnidadesBloqueadas BL ON BL.IdUnidad = UC.IdUnidad
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(V.NumeroPlaca))
			LEFT JOIN @TEMP_ULTIV U ON U.ID_UNIDAD = UC.IdUnidad 
		WHERE (TV.idTipoVehiculo = @TipoVehiculo) AND (SV.SubTipoVehiculo = @SubTipoVehiculo) AND (uc.IdProgramacion = @idOperacion) AND (V.Estado = 2)
		AND (@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%')
		ORDER BY IdRegistro DESC
	END

	IF (@TipoVehiculo != 0 AND @SubTipoVehiculo != 0 AND @idOperacion = 5) BEGIN
		SELECT IdRegistro 'N°',uc.IdUnidad 'ID',FechaRegistro 'FECHA', CASE WHEN V.Estado = 2 THEN 'OPERATIVO' ELSE 'INOPERATIVO' END AS 'ESTADO',
		CASE WHEN BL.IdBloqueo IS NULL THEN 'NO' ELSE 'SÍ' END AS 'BLOQUEADO', V.NumeroPlaca 'UNIDAD', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', 
		V.Modelo AS 'MODELO', M.AnoAdquisicion AS 'AÑO', TV.idTipoVehiculo, TV.Descripcion AS 'TIPO_UNIDAD', SV.SubTipoVehiculo, SV.Descripcion AS 'SUBTIPO_UNIDAD',
		ISNULL(Transmision,'') AS 'TRANSMISION', ISNULL(uc.IdProgramacion,-1) IDOPERACION,
		CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION_UNIDAD', UC.Peso AS 'PESO',
		CASE WHEN ISNULL(UC.MOCHILA,0)<1 THEN 0 ELSE 1 END AS 'MOCHILA', UC.Galones AS 'GALONES',
		CASE WHEN ISNULL(UC.TOMAFUERZA,0)<1 THEN 0 ELSE 1 END AS 'TOMAFUERZA',
		CASE WHEN ISNULL(UC.UREA,0)<1 THEN 0 ELSE 1 END AS 'UREA',
		CASE WHEN ISNULL(UC.MANGUERA,0)<1 THEN 0 ELSE 1 END AS 'MANGUERA DE AIRE', UC.Observacion OBSERVACION, U.NRO_TICKET AS 'ULTIMA_PROG',
		U.FECHA_PROG AS 'ULTIMA_FECHA', UC.TipoCortina AS 'CORTINA', UC.ModeloChasis AS 'CHASIS', UC.Nivel AS 'NIVEL', UC.TipoNivel AS 'TIPO_NIVEL', UC.Suspension AS 'SUSPENSION',
		UC.Piso AS 'PISO', UC.MaterialPiso AS 'MATERIAL_PISO', UC.UsuarioCrea 'USUARIO_CREA', UC.FechaCrea 'FECHA_CREA', UC.UsuarioModifica 'USUARIO_MODIFICA',UC.FechaModifica 'FECHA_MODIFICA'
		FROM ReportesApp_Operacion_MaestroUnidadesConductor UC
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
			LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TIPOVEHICULO
			LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
			LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON  O.IdOperacion = UC.IdProgramacion
			LEFT JOIN ReportesApp_Operacion_UnidadesBloqueadas BL ON BL.IdUnidad = UC.IdUnidad
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(V.NumeroPlaca))
			LEFT JOIN @TEMP_ULTIV U ON U.ID_UNIDAD = UC.IdUnidad 
		WHERE (TV.idTipoVehiculo = @TipoVehiculo) AND (SV.SubTipoVehiculo = @SubTipoVehiculo) AND (V.Estado = 2) AND (@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%')
		ORDER BY IdRegistro DESC
	END

	IF (@TipoVehiculo = 0 AND @SubTipoVehiculo = 0 AND @idOperacion != 5) BEGIN
		SELECT IdRegistro 'N°',uc.IdUnidad 'ID',FechaRegistro 'FECHA', CASE WHEN V.Estado = 2 THEN 'OPERATIVO' ELSE 'INOPERATIVO' END AS 'ESTADO',
		CASE WHEN BL.IdBloqueo IS NULL THEN 'NO' ELSE 'SÍ' END AS 'BLOQUEADO', V.NumeroPlaca 'UNIDAD', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', 
		V.Modelo AS 'MODELO', M.AnoAdquisicion AS 'AÑO', TV.idTipoVehiculo, TV.Descripcion AS 'TIPO_UNIDAD', SV.SubTipoVehiculo, SV.Descripcion AS 'SUBTIPO_UNIDAD',
		ISNULL(Transmision,'') AS 'TRANSMISION', ISNULL(uc.IdProgramacion,-1) IDOPERACION,
		CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION_UNIDAD', UC.Peso AS 'PESO',
		CASE WHEN ISNULL(UC.MOCHILA,0)<1 THEN 0 ELSE 1 END AS 'MOCHILA', UC.Galones AS 'GALONES',
		CASE WHEN ISNULL(UC.TOMAFUERZA,0)<1 THEN 0 ELSE 1 END AS 'TOMAFUERZA',
		CASE WHEN ISNULL(UC.UREA,0)<1 THEN 0 ELSE 1 END AS 'UREA',
		CASE WHEN ISNULL(UC.MANGUERA,0)<1 THEN 0 ELSE 1 END AS 'MANGUERA DE AIRE', UC.Observacion OBSERVACION, U.NRO_TICKET AS 'ULTIMA_PROG',
		U.FECHA_PROG AS 'ULTIMA_FECHA', UC.TipoCortina AS 'CORTINA', UC.ModeloChasis AS 'CHASIS', UC.Nivel AS 'NIVEL', UC.TipoNivel AS 'TIPO_NIVEL', UC.Suspension AS 'SUSPENSION',
		UC.Piso AS 'PISO', UC.MaterialPiso AS 'MATERIAL_PISO', UC.UsuarioCrea 'USUARIO_CREA', UC.FechaCrea 'FECHA_CREA', UC.UsuarioModifica 'USUARIO_MODIFICA',UC.FechaModifica 'FECHA_MODIFICA'
		FROM ReportesApp_Operacion_MaestroUnidadesConductor UC
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
			LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TIPOVEHICULO
			LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
			LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON  O.IdOperacion = UC.IdProgramacion
			LEFT JOIN ReportesApp_Operacion_UnidadesBloqueadas BL ON BL.IdUnidad = UC.IdUnidad
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(V.NumeroPlaca))
			LEFT JOIN @TEMP_ULTIV U ON U.ID_UNIDAD = UC.IdUnidad 
		WHERE (V.Estado = 2) AND (@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND (uc.IdProgramacion = @idOperacion)
		ORDER BY IdRegistro DESC
	END
END




