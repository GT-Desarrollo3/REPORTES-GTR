
-- CREAR TABLA ReportesApp_Operaciones_Operatividad_CarretasView

-- CREAR TABLA ReportesApp_Operaciones_Operatividad_RegistroCarretas

-- CREAR TABLA ReportesApp_Operaciones_Operatividad_ViewCarretas

----------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 04-04-2024
-- Description:	MAPEAR TRACTOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Operatividad_MapearTractos]
@IdUnidad INT,
@TipoUnidad VARCHAR(50),
@Periodo VARCHAR(6),
@Usuario VARCHAR(20)
AS
DECLARE @exito VARCHAR(MAX)
DECLARE @Dia DATE
DECLARE @FechaIni DATE, @FechaFin DATE
DECLARE @DOMINGOS TABLE(FECHA DATE)
DECLARE @T_TractoDF TABLE(IdUnidad INT, TipoUnidad VARCHAR(50), Placa VARCHAR(50), FechaIni DATE, FechaFin DATE, FechaDF DATE, ItemDF INT)
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

INSERT INTO @T_TractoDF(IdUnidad,TipoUnidad,Placa,FechaIni,FechaFin,FechaDF,ItemDF)
SELECT X.IdUnidad,X.Descripcion, X.NumeroPlaca,X.FechaDesde,X.FechaHasta,X.FECHA,
	ROW_NUMBER() OVER(ORDER BY X.IdUnidad,X.FECHA) ItemDF
FROM
( SELECT UC.IdUnidad, TV.Descripcion, V.NumeroPlaca, @FechaIni FechaDesde ,@FechaFin FechaHasta,D.FECHA,
		 ROW_NUMBER() OVER (PARTITION BY UC.IdUnidad, D.FECHA ORDER BY D.FECHA DESC) Orden
  FROM ReportesApp_Operacion_MaestroUnidadesConductor UC
  LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
  LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TIPOVEHICULO
  LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = UC.IdProgramacion
  INNER JOIN @DOMINGOS D ON @FechaIni <= D.FECHA AND @FechaFin >= D.FECHA
  WHERE UC.IdUnidad = @IdUnidad ) X
WHERE X.Orden = 1

BEGIN TRAN
BEGIN TRY
	IF @Usuario NOT IN ('CBELTRAN','LQUEZADA','GREYES') BEGIN
		SET @exito = '-10 = No tiene acceso a esta operación.'
		ROLLBACK
		GOTO Terminar
	END
	
	DECLARE @n INT, @UltimoDiaMes TINYINT = DAY(@fechafin), @IdUnidadDF INT, @FechaDF DATE

	SELECT @n = MIN(ItemDF) FROM @T_TractoDF

	IF (@n IS NULL) BEGIN
		IF (@TipoUnidad = 'TRACTO') BEGIN
			INSERT INTO ReportesApp_Operaciones_Operatividad_PlacasView (IdUnidad,Anio,Mes,NroDias)
			VALUES(@IdUnidad, RIGHT(@Periodo,4), LEFT(@Periodo,2), @UltimoDiaMes)
		END

		IF (@TipoUnidad = 'SEMIRREMOLQUE') BEGIN
			INSERT INTO ReportesApp_Operaciones_Operatividad_CarretasView (IdUnidad,Anio,Mes,NroDias)
			VALUES(@IdUnidad, RIGHT(@Periodo,4), LEFT(@Periodo,2), @UltimoDiaMes)
		END
	END

	WHILE (@n IS NOT NULL) BEGIN
		SELECT @IdUnidadDF = T.IdUnidad, @FechaDF = T.FechaDF
		FROM @T_TractoDF T
		WHERE ItemDF = @n

		IF (NOT EXISTS(SELECT TOP 1 Mes FROM ReportesApp_Operaciones_Operatividad_PlacasView WITH(NOLOCK) WHERE IdUnidad = @IdUnidadDF 
		   AND Anio=YEAR(@FechaDF) AND Mes=MONTH(@FechaDF)) AND @TipoUnidad = 'TRACTO') BEGIN
			INSERT INTO ReportesApp_Operaciones_Operatividad_PlacasView (IdUnidad,Anio,Mes,NroDias)
			VALUES(@IdUnidadDF, YEAR(@FechaDF), MONTH(@FechaDF), @UltimoDiaMes)						 
		END

		IF (NOT EXISTS(SELECT TOP 1 Mes FROM ReportesApp_Operaciones_Operatividad_CarretasView WITH(NOLOCK) WHERE IdUnidad = @IdUnidadDF 
		   AND Anio=YEAR(@FechaDF) AND Mes=MONTH(@FechaDF)) AND @TipoUnidad = 'SEMIRREMOLQUE') BEGIN
			INSERT INTO ReportesApp_Operaciones_Operatividad_CarretasView (IdUnidad,Anio,Mes,NroDias)
			VALUES(@IdUnidadDF, YEAR(@FechaDF), MONTH(@FechaDF), @UltimoDiaMes)						 
		END

		PRINT CAST(@n AS VARCHAR(10))+ ' --> @IdUnidadDF:'+ CAST(@IdUnidadDF AS VARCHAR(10)) +' @FechaDF: '+CONVERT(VARCHAR(10),@FechaDF,103)+' >>> DF'

		IF (@TipoUnidad = 'TRACTO') BEGIN
			UPDATE ReportesApp_Operaciones_Operatividad_PlacasView SET 
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
			WHERE IdUnidad = @IdUnidadDF AND Anio = YEAR(@FechaDF) AND Mes = MONTH(@FechaDF)
		END
		
		IF (@TipoUnidad = 'SEMIRREMOLQUE') BEGIN
			UPDATE ReportesApp_Operaciones_Operatividad_CarretasView SET 
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
			WHERE IdUnidad = @IdUnidadDF AND Anio = YEAR(@FechaDF) AND Mes = MONTH(@FechaDF)
		END

		SELECT @n = MIN(ItemDF) FROM @T_TractoDF WHERE @n < ItemDF
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

---------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 01-04-2024
-- Description:	QUITAR TRACTOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Operatividad_QuitarTractos]
@IdUnidad INT,
@TipoUnidad VARCHAR(50),
@Periodo VARCHAR(6),
@Usuario VARCHAR(20)
AS
DECLARE @exito VARCHAR(MAX)

SET @exito = '0 = Mapeo eliminado correctamente.'

BEGIN TRAN
BEGIN TRY
	IF (@TipoUnidad = 'TRACTO') BEGIN
		DELETE FROM ReportesApp_Operaciones_Operatividad_PlacasView
		WHERE IdUnidad = @IdUnidad
		AND Anio = CONVERT(INT,RIGHT(@Periodo,4)) AND Mes = CONVERT(INT,LEFT(@Periodo,2))

		DELETE FROM ReportesApp_Operaciones_Operatividad_Registro
		WHERE IdUnidad = @IdUnidad AND MONTH(Fecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(Fecha) = CONVERT(INT,RIGHT(@Periodo,4))
	END

	IF (@TipoUnidad = 'SEMIRREMOLQUE') BEGIN
		DELETE FROM ReportesApp_Operaciones_Operatividad_CarretasView
		WHERE IdUnidad = @IdUnidad
		AND Anio = CONVERT(INT,RIGHT(@Periodo,4)) AND Mes = CONVERT(INT,LEFT(@Periodo,2))

		DELETE FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas
		WHERE IdUnidad = @IdUnidad AND MONTH(Fecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(Fecha) = CONVERT(INT,RIGHT(@Periodo,4))
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

--------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author Name:	GERARDO REYES
-- Create date: 05-09-2024
-- Description:	LISTAR TABLA DE CARRETAS
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Operaciones_Operatividad_ListarTablaOperatividadCarreta]
@Periodo VARCHAR(6),
@Placa VARCHAR(30),
@Programacion VARCHAR(30),
@Carreta VARCHAR(100)
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

	DECLARE @T_Prueba TABLE (IdUnidad INT, Programacion VARCHAR(100), Placa VARCHAR(50), TipoVehiculo VARCHAR(100),
							 D1 VARCHAR(15), D2 VARCHAR(15), D3 VARCHAR(15), D4 VARCHAR(15), D5 VARCHAR(15), D6 VARCHAR(15),
							 D7 VARCHAR(15), D8 VARCHAR(15), D9 VARCHAR(15), D10 VARCHAR(15), D11 VARCHAR(15), D12 VARCHAR(15),
							 D13 VARCHAR(15), D14 VARCHAR(15), D15 VARCHAR(15), D16 VARCHAR(15), D17 VARCHAR(15), D18 VARCHAR(15),
							 D19 VARCHAR(15), D20 VARCHAR(15), D21 VARCHAR(15), D22 VARCHAR(15), D23 VARCHAR(15), D24 VARCHAR(15),
							 D25 VARCHAR(15), D26 VARCHAR(15), D27 VARCHAR(15), D28 VARCHAR(15), D29 VARCHAR(15), D30 VARCHAR(15), D31 VARCHAR(15))

	DECLARE @D1 VARCHAR(15), @D2 VARCHAR(15), @D3 VARCHAR(15), @D4 VARCHAR(15), @D5 VARCHAR(15), @D6 VARCHAR(15), @D7 VARCHAR(15), @D8 VARCHAR(15),
			@D9 VARCHAR(15), @D10 VARCHAR(15), @D11 VARCHAR(15), @D12 VARCHAR(15), @D13 VARCHAR(15), @D14 VARCHAR(15), @D15 VARCHAR(15), @D16 VARCHAR(15),
			@D17 VARCHAR(15), @D18 VARCHAR(15), @D19 VARCHAR(15), @D20 VARCHAR(15), @D21 VARCHAR(15), @D22 VARCHAR(15), @D23 VARCHAR(15), @D24 VARCHAR(15),
			@D25 VARCHAR(15), @D26 VARCHAR(15), @D27 VARCHAR(15), @D28 VARCHAR(15), @D29 VARCHAR(15), @D30 VARCHAR(15), @D31 VARCHAR(15)

	INSERT INTO @T_Prueba (Programacion,Placa,TipoVehiculo)
	VALUES ('Programacion', '<PLACA>', '<TIPO_VEHICULO>')

	DECLARE @FechaIni DATE, @FechaFin DATE, @FechaVer DATE

	IF (@FDesde IS NOT NULL) BEGIN 
		SET @FechaVer = CAST(CAST(@DiaInicio As Varchar(2))+'/'+ RIGHT('0'+ CAST(month(@FDesde) AS VARCHAR(2)),2)+'/'+CAST(@Anio as VARCHAR(4)) AS DATE)
	END
	ELSE BEGIN
		SET @FechaVer = CAST(CAST(@DiaInicio As Varchar(2))+'/'+ RIGHT('0'+ CAST(@Mes AS VARCHAR(2)),2)+'/'+CAST(@Anio as VARCHAR(4)) AS DATE)
	END

	SET @FechaIni = @FechaVer
	SET @FechaFin = DATEADD(D,-1, DATEADD(MONTH,1,@FechaVer))

	IF (@Programacion = 'TODO') BEGIN
		IF (@Carreta = 'TODOS') BEGIN
			INSERT INTO @T_Prueba (IdUnidad, Programacion, Placa, TipoVehiculo)
			SELECT DISTINCT UC.IdUnidad, CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'PROGRAMACION', V.NumeroPlaca 'PLACA',
			SV.Descripcion AS 'SUBTIPO_UNIDAD' FROM ReportesApp_Operacion_MaestroUnidadesConductor UC
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
			LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = UC.IdProgramacion
			INNER JOIN ReportesApp_Operaciones_Operatividad_CarretasView P ON P.IdUnidad = UC.IdUnidad
			WHERE P.Anio = RIGHT(@Periodo,4) AND P.Mes = LEFT(@Periodo,2) AND (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%')
		END
		ELSE BEGIN
			INSERT INTO @T_Prueba (IdUnidad, Programacion, Placa, TipoVehiculo)
			SELECT DISTINCT UC.IdUnidad, CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'PROGRAMACION', V.NumeroPlaca 'PLACA',
			SV.Descripcion AS 'SUBTIPO_UNIDAD' FROM ReportesApp_Operacion_MaestroUnidadesConductor UC
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
			LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = UC.IdProgramacion
			INNER JOIN ReportesApp_Operaciones_Operatividad_CarretasView P ON P.IdUnidad = UC.IdUnidad
			WHERE P.Anio = RIGHT(@Periodo,4) AND P.Mes = LEFT(@Periodo,2) AND (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%')
			AND (SV.Descripcion = @Carreta)
		END
	END
	ELSE BEGIN
		IF (@Carreta = 'TODOS') BEGIN
			INSERT INTO @T_Prueba (IdUnidad, Programacion, Placa, TipoVehiculo)
			SELECT DISTINCT UC.IdUnidad, CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'PROGRAMACION', V.NumeroPlaca 'PLACA',
			SV.Descripcion AS 'SUBTIPO_UNIDAD' FROM ReportesApp_Operacion_MaestroUnidadesConductor UC
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
			LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = UC.IdProgramacion
			INNER JOIN ReportesApp_Operaciones_Operatividad_CarretasView P ON P.IdUnidad = UC.IdUnidad
			WHERE P.Anio = RIGHT(@Periodo,4) AND P.Mes = LEFT(@Periodo,2) AND (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%')
			AND (O.Descripcion = @Programacion)
		END
		ELSE BEGIN
			INSERT INTO @T_Prueba (IdUnidad, Programacion, Placa, TipoVehiculo)
			SELECT DISTINCT UC.IdUnidad, CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'PROGRAMACION', V.NumeroPlaca 'PLACA',
			SV.Descripcion AS 'SUBTIPO_UNIDAD' FROM ReportesApp_Operacion_MaestroUnidadesConductor UC
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
			LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = UC.IdProgramacion
			INNER JOIN ReportesApp_Operaciones_Operatividad_CarretasView P ON P.IdUnidad = UC.IdUnidad
			WHERE P.Anio = RIGHT(@Periodo,4) AND P.Mes = LEFT(@Periodo,2) AND (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%')
			AND (O.Descripcion = @Programacion) AND (SV.Descripcion = @Carreta)
		END
	END

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
		WHERE Placa = '<PLACA>'

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
		LEFT JOIN ReportesApp_Operaciones_Operatividad_CarretasView AS A WITH(NOLOCK) ON T.IdUnidad = A.IdUnidad 
		WHERE A.Anio = YEAR(@FechaVer) AND A.Mes = MONTH(@FechaVer) 

		SET @FechaVer = DATEADD(D,1,@FechaVer)
			
		IF (@FDesde IS NOT NULL) AND (@NroDia = @CantDias OR @CantDias = 0)  BREAK;
		
		SET @NroDia = @NroDia + 1
	END

	IF (@CantDias = 28) BEGIN
		SELECT @D1=D1, @D2=D2, @D3=D3, @D4=D4, @D5=D5, @D6=D6, @D7=D7, @D8=D8, @D9=D9, @D10=D10, @D11=D11, @D12=D12, @D13=D13, @D14=D14,
		@D15=D15, @D16=D16, @D17=D17, @D18=D18, @D19=D19, @D20=D20, @D21=D21, @D22=D22, @D23=D23, @D24=D24, @D25=D25, @D26=D26, @D27=D27, @D28=D28
		FROM @T_Prueba WHERE Placa = '<PLACA>'
	END

	IF (@CantDias = 29) BEGIN
		SELECT @D1=D1, @D2=D2, @D3=D3, @D4=D4, @D5=D5, @D6=D6, @D7=D7, @D8=D8, @D9=D9, @D10=D10, @D11=D11, @D12=D12, @D13=D13, @D14=D14, @D15=D15,
		@D16=D16, @D17=D17, @D18=D18, @D19=D19, @D20=D20, @D21=D21, @D22=D22, @D23=D23, @D24=D24, @D25=D25, @D26=D26, @D27=D27, @D28=D28, @D29=D29
		FROM @T_Prueba WHERE Placa = '<PLACA>'
	END

	IF (@CantDias = 30) BEGIN
		SELECT @D1=D1, @D2=D2, @D3=D3, @D4=D4, @D5=D5, @D6=D6, @D7=D7, @D8=D8, @D9=D9, @D10=D10, @D11=D11, @D12=D12, @D13=D13, @D14=D14, @D15=D15,
		@D16=D16, @D17=D17, @D18=D18, @D19=D19, @D20=D20, @D21=D21, @D22=D22, @D23=D23, @D24=D24, @D25=D25, @D26=D26, @D27=D27, @D28=D28, @D29=D29, @D30=D30
		FROM @T_Prueba WHERE Placa = '<PLACA>'
	END

	IF (@CantDias = 31) BEGIN
		SELECT @D1=D1, @D2=D2, @D3=D3, @D4=D4, @D5=D5, @D6=D6, @D7=D7, @D8=D8, @D9=D9, @D10=D10, @D11=D11, @D12=D12, @D13=D13, @D14=D14, @D15=D15, @D16=D16, @D17=D17,
		@D18=D18, @D19=D19, @D20=D20, @D21=D21, @D22=D22, @D23=D23, @D24=D24, @D25=D25, @D26=D26, @D27=D27, @D28=D28, @D29=D29, @D30=D30, @D29=D29, @D30=D30, @D31=D31
		FROM @T_Prueba WHERE Placa = '<PLACA>'
	END

	CREATE TABLE #PruebaView (IdUnidad INT, Programacion VARCHAR(100), Placa VARCHAR(50), TipoVehiculo VARCHAR(100),
							  D1 VARCHAR(15),D2 VARCHAR(15),D3 VARCHAR(15),D4 VARCHAR(15),D5 VARCHAR(15),
							  D6 VARCHAR(15),D7 VARCHAR(15),D8 VARCHAR(15),D9 VARCHAR(15),D10 VARCHAR(15),
							  D11 VARCHAR(15),D12 VARCHAR(15),D13 VARCHAR(15),D14 VARCHAR(15),D15 VARCHAR(15),
							  D16 VARCHAR(15),D17 VARCHAR(15),D18 VARCHAR(15),D19 VARCHAR(15),D20 VARCHAR(15),
							  D21 VARCHAR(15),D22 VARCHAR(15),D23 VARCHAR(15),D24 VARCHAR(15),D25 VARCHAR(15),
							  D26 VARCHAR(15),D27 VARCHAR(15),D28 VARCHAR(15),D29 VARCHAR(15),D30 VARCHAR(15),
							  D31 VARCHAR(15))

	DECLARE @SQL varchar(5000)

	INSERT INTO #PruebaView
	SELECT * FROM @T_Prueba WHERE Placa <> '<PLACA>'

	IF @CantDias = 28 BEGIN
		SET @SQL='SELECT IdUnidad, Placa AS PLACA, TipoVehiculo AS TIPO_VEHICULO, Programacion AS PROGRAMACION,
			D1 AS '+@D1+',D2 AS '+@D2+',D3 AS '+@D3+',D4 AS '+@D4+',D5 AS '+@D5+',D6 AS '+@D6+',D7 AS '+@D7+',D8 AS '+@D8+
		    ',D9 AS '+@D9+',D10 AS '+@D10+',D11 AS '+@D11+',D12 AS '+@D12+',D13 AS '+@D13+',D14 AS '+@D14+',D15 AS '+@D15+
			',D16 AS '+@D16+',D17 AS '+@D17+',D18 AS '+@D18+',D19 AS '+@D19+',D20 AS '+@D20+',D21 AS '+@D21+',D22 AS '+@D22+
			',D23 AS '+@D23+',D24 AS '+@D24+',D25 AS '+@D25+',D26 AS '+@D26+',D27 AS '+@D27+',D28 AS '+@D28+' FROM #PruebaView ORDER BY Placa'
		
		EXEC (@SQL)
		DROP TABLE #PruebaView
	END

	IF @CantDias = 29 BEGIN
		SET @SQL='SELECT IdUnidad, Placa AS PLACA, TipoVehiculo AS TIPO_VEHICULO, Programacion AS PROGRAMACION, 
			D1 AS '+@D1+',D2 AS '+@D2+',D3 AS '+@D3+',D4 AS '+@D4+',D5 AS '+@D5+',D6 AS '+@D6+',D7 AS '+@D7+',D8 AS '+@D8+
			',D9 AS '+@D9+',D10 AS '+@D10+',D11 AS '+@D11+',D12 AS '+@D12+',D13 AS '+@D13+',D14 AS '+@D14+',D15 AS '+@D15+
			',D16 AS '+@D16+',D17 AS '+@D17+',D18 AS '+@D18+',D19 AS '+@D19+',D20 AS '+@D20+',D21 AS '+@D21+',D22 AS '+@D22+
			',D23 AS '+@D23+',D24 AS '+@D24+',D25 AS '+@D25+',D26 AS '+@D26+',D27 AS '+@D27+',D28 AS '+@D28+',D29 AS '+@D29+' FROM #PruebaView ORDER BY Placa'
		
		EXEC (@SQL)
		DROP TABLE #PruebaView	
	END

	IF @CantDias = 30 BEGIN
		SET @SQL='SELECT IdUnidad, Placa AS PLACA, TipoVehiculo AS TIPO_VEHICULO, Programacion AS PROGRAMACION, 
			D1 AS '+@D1+',D2 AS '+@D2+',D3 AS '+@D3+',D4 AS '+@D4+',D5 AS '+@D5+',D6 AS '+@D6+',D7 AS '+@D7+',D8 AS '+@D8+
			',D9 AS '+@D9+',D10 AS '+@D10+',D11 AS '+@D11+',D12 AS '+@D12+',D13 AS '+@D13+',D14 AS '+@D14+',D15 AS '+@D15+
			',D16 AS '+@D16+',D17 AS '+@D17+',D18 AS '+@D18+',D19 AS '+@D19+',D20 AS '+@D20+',D21 AS '+@D21+',D22 AS '+@D22+
			',D23 AS '+@D23+',D24 AS '+@D24+',D25 AS '+@D25+',D26 AS '+@D26+',D27 AS '+@D27+',D28 AS '+@D28+',D29 AS '+@D29+
			',D30 AS '+@D30+' FROM #PruebaView ORDER BY Placa'
		
		EXEC (@SQL)
		DROP TABLE #PruebaView
	END

	IF @CantDias = 31 BEGIN
		SET @SQL='SELECT IdUnidad, Placa AS PLACA, TipoVehiculo AS TIPO_VEHICULO, Programacion AS PROGRAMACION, 
			D1 AS '+@D1+',D2 AS '+@D2+',D3 AS '+@D3+',D4 AS '+@D4+',D5 AS '+@D5+',D6 AS '+@D6+',D7 AS '+@D7+',D8 AS '+@D8+
			',D9 AS '+@D9+',D10 AS '+@D10+',D11 AS '+@D11+',D12 AS '+@D12+',D13 AS '+@D13+',D14 AS '+@D14+',D15 AS '+@D15+
			',D16 AS '+@D16+',D17 AS '+@D17+',D18 AS '+@D18+',D19 AS '+@D19+',D20 AS '+@D20+',D21 AS '+@D21+',D22 AS '+@D22+
			',D23 AS '+@D23+',D24 AS '+@D24+',D25 AS '+@D25+',D26 AS '+@D26+',D27 AS '+@D27+',D28 AS '+@D28+',D29 AS '+@D29+
			',D30 AS '+@D30+',D31 AS '+@D31+' FROM #PruebaView ORDER BY Placa'
		
		EXEC (@SQL)
		DROP TABLE #PruebaView
	END
END

-----------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 06-09-2024
-- Description:	MAPEAR CONDICIONES CARRETAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Operatividad_MapearCondicionesCarretas]
@idCondicion INT,
@Periodo VARCHAR(6),
@Usuario VARCHAR(20)
AS
DECLARE @exito VARCHAR(MAX)
DECLARE @Dia DATE
DECLARE @FechaIni DATE, @FechaFin DATE
DECLARE @DOMINGOS TABLE(FECHA DATE)
DECLARE @T_CondicionDF TABLE(idCondicion INT, Operativas VARCHAR(50), Condicion VARCHAR(250), FechaIni DATE, FechaFin DATE, FechaDF DATE, ItemDF INT)
-- GERARDO - NUEVO
DECLARE @T_CondicionPL TABLE(idCondicion INT, Operativas VARCHAR(50), Condicion VARCHAR(250), FechaIni DATE, FechaFin DATE, FechaDF DATE, ItemDF INT)
DECLARE @T_CondicionTL TABLE(idCondicion INT, Operativas VARCHAR(50), Condicion VARCHAR(250), FechaIni DATE, FechaFin DATE, FechaDF DATE, ItemDF INT)
DECLARE @T_CondicionCS TABLE(idCondicion INT, Operativas VARCHAR(50), Condicion VARCHAR(250), FechaIni DATE, FechaFin DATE, FechaDF DATE, ItemDF INT)
DECLARE @T_CondicionCR TABLE(idCondicion INT, Operativas VARCHAR(50), Condicion VARCHAR(250), FechaIni DATE, FechaFin DATE, FechaDF DATE, ItemDF INT)
-- GERARDO - NUEVO
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

INSERT INTO @T_CondicionDF(idCondicion,Operativas,Condicion,FechaIni,FechaFin,FechaDF,ItemDF)
SELECT X.idCondicion,X.Operativas, X.Condicion,X.FechaDesde,X.FechaHasta,X.FECHA,
	ROW_NUMBER() OVER(ORDER BY X.idCondicion,X.FECHA) ItemDF
FROM
( SELECT C.idCondicion, C.Operativas, C.Condicion, @FechaIni FechaDesde ,@FechaFin FechaHasta,D.FECHA,
		 ROW_NUMBER() OVER (PARTITION BY C.idCondicion, D.FECHA ORDER BY D.FECHA DESC) Orden
  FROM ReportesApp_Operaciones_Operatividad_Condiciones C WITH(NOLOCK)
  INNER JOIN @DOMINGOS D ON @FechaIni <= D.FECHA AND @FechaFin >= D.FECHA
  WHERE C.idCondicion = @idCondicion ) X
WHERE X.Orden = 1

-- GERARDO - NUEVO
INSERT INTO @T_CondicionPL(idCondicion,Operativas,Condicion,FechaIni,FechaFin,FechaDF,ItemDF)
SELECT X.idCondicion,X.Operativas,X.Condicion,X.FechaDesde,X.FechaHasta,X.FECHA,ROW_NUMBER() OVER(ORDER BY X.idCondicion,X.FECHA) ItemDF
FROM (SELECT C.idCondicion, C.Operativas, C.Condicion, @FechaIni FechaDesde, @FechaFin FechaHasta,D.FECHA, ROW_NUMBER() OVER (PARTITION BY C.idCondicion, D.FECHA ORDER BY D.FECHA DESC) Orden
	  FROM ReportesApp_Operaciones_Operatividad_Condiciones C WITH(NOLOCK)
	  INNER JOIN @DOMINGOS D ON @FechaIni <= D.FECHA AND @FechaFin >= D.FECHA
	  WHERE C.idCondicion = @idCondicion) X
WHERE X.Orden = 1

INSERT INTO @T_CondicionTL(idCondicion,Operativas,Condicion,FechaIni,FechaFin,FechaDF,ItemDF)
SELECT X.idCondicion,X.Operativas,X.Condicion,X.FechaDesde,X.FechaHasta,X.FECHA,ROW_NUMBER() OVER(ORDER BY X.idCondicion,X.FECHA) ItemDF
FROM (SELECT C.idCondicion, C.Operativas, C.Condicion, @FechaIni FechaDesde, @FechaFin FechaHasta,D.FECHA, ROW_NUMBER() OVER (PARTITION BY C.idCondicion, D.FECHA ORDER BY D.FECHA DESC) Orden
	  FROM ReportesApp_Operaciones_Operatividad_Condiciones C WITH(NOLOCK)
	  INNER JOIN @DOMINGOS D ON @FechaIni <= D.FECHA AND @FechaFin >= D.FECHA
	  WHERE C.idCondicion = @idCondicion) X
WHERE X.Orden = 1

INSERT INTO @T_CondicionCS(idCondicion,Operativas,Condicion,FechaIni,FechaFin,FechaDF,ItemDF)
SELECT X.idCondicion,X.Operativas,X.Condicion,X.FechaDesde,X.FechaHasta,X.FECHA,ROW_NUMBER() OVER(ORDER BY X.idCondicion,X.FECHA) ItemDF
FROM (SELECT C.idCondicion, C.Operativas, C.Condicion, @FechaIni FechaDesde, @FechaFin FechaHasta,D.FECHA, ROW_NUMBER() OVER (PARTITION BY C.idCondicion, D.FECHA ORDER BY D.FECHA DESC) Orden
	  FROM ReportesApp_Operaciones_Operatividad_Condiciones C WITH(NOLOCK)
	  INNER JOIN @DOMINGOS D ON @FechaIni <= D.FECHA AND @FechaFin >= D.FECHA
	  WHERE C.idCondicion = @idCondicion) X
WHERE X.Orden = 1

INSERT INTO @T_CondicionCR(idCondicion,Operativas,Condicion,FechaIni,FechaFin,FechaDF,ItemDF)
SELECT X.idCondicion,X.Operativas,X.Condicion,X.FechaDesde,X.FechaHasta,X.FECHA,ROW_NUMBER() OVER(ORDER BY X.idCondicion,X.FECHA) ItemDF
FROM (SELECT C.idCondicion, C.Operativas, C.Condicion, @FechaIni FechaDesde, @FechaFin FechaHasta,D.FECHA, ROW_NUMBER() OVER (PARTITION BY C.idCondicion, D.FECHA ORDER BY D.FECHA DESC) Orden
	  FROM ReportesApp_Operaciones_Operatividad_Condiciones C WITH(NOLOCK)
	  INNER JOIN @DOMINGOS D ON @FechaIni <= D.FECHA AND @FechaFin >= D.FECHA
	  WHERE C.idCondicion = @idCondicion) X
WHERE X.Orden = 1
-- GERARDO - NUEVO

BEGIN TRAN
BEGIN TRY
	IF (@idCondicion NOT IN (7,9)) BEGIN
		IF (NOT EXISTS(SELECT * FROM ReportesApp_Operaciones_Operatividad_ViewCarretas WHERE idCondicion = @idCondicion
		AND Anio = RIGHT(@Periodo,4) AND Mes = LEFT(@Periodo,2))) BEGIN		-- CONTINUAR
			DECLARE @n INT, @UltimoDiaMes TINYINT = DAY(@fechafin), @idCondicionDF INT, @FechaDF DATE

			SELECT @n = MIN(ItemDF) FROM @T_CondicionDF

			IF (@n IS NULL) BEGIN
				INSERT INTO ReportesApp_Operaciones_Operatividad_ViewCarretas(idCondicion,Anio,Mes,NroDias)
				VALUES(@idCondicion, RIGHT(@Periodo,4), LEFT(@Periodo,2), @UltimoDiaMes)--,'02','Sys-Task',@xFHRegistra)	
			END

			WHILE (@n IS NOT NULL) BEGIN
				SELECT @idCondicionDF = T.idCondicion, @FechaDF = T.FechaDF
				FROM @T_CondicionDF T
				WHERE ItemDF = @n

				IF NOT EXISTS(SELECT TOP 1 Mes FROM ReportesApp_Operaciones_Operatividad_ViewCarretas WITH(NOLOCK) WHERE idCondicion = @idCondicionDF 
				AND Anio = YEAR(@FechaDF) AND Mes = MONTH(@FechaDF)) BEGIN
					INSERT INTO ReportesApp_Operaciones_Operatividad_ViewCarretas(idCondicion,Anio,Mes,NroDias)
					VALUES(@idCondicionDF, YEAR(@FechaDF), MONTH(@FechaDF), @UltimoDiaMes)						 
				END

				PRINT CAST(@n AS VARCHAR(10))+ ' --> @idCondicionDF:'+ CAST(@idCondicionDF AS VARCHAR(10)) +' @FechaDF: '+CONVERT(VARCHAR(10),@FechaDF,103)+' >>> 0'

				UPDATE ReportesApp_Operaciones_Operatividad_ViewCarretas SET 
					D1 = CASE WHEN DAY(@FechaDF) = 1 THEN '0' ELSE D1 END,
					D2 = CASE WHEN DAY(@FechaDF) = 2 THEN '0' ELSE D2 END,
					D3 = CASE WHEN DAY(@FechaDF) = 3 THEN '0' ELSE D3 END,
					D4 = CASE WHEN DAY(@FechaDF) = 4 THEN '0' ELSE D4 END,
					D5 = CASE WHEN DAY(@FechaDF) = 5 THEN '0' ELSE D5 END,
					D6 = CASE WHEN DAY(@FechaDF) = 6 THEN '0' ELSE D6 END,
					D7 = CASE WHEN DAY(@FechaDF) = 7 THEN '0' ELSE D7 END,
					D8 = CASE WHEN DAY(@FechaDF) = 8 THEN '0' ELSE D8 END,
					D9 = CASE WHEN DAY(@FechaDF) = 9 THEN '0' ELSE D9 END,
					D10 = CASE WHEN DAY(@FechaDF) = 10 THEN '0' ELSE D10 END,
					D11 = CASE WHEN DAY(@FechaDF) = 11 THEN '0' ELSE D11 END,
					D12 = CASE WHEN DAY(@FechaDF) = 12 THEN '0' ELSE D12 END,
					D13 = CASE WHEN DAY(@FechaDF) = 13 THEN '0' ELSE D13 END,
					D14 = CASE WHEN DAY(@FechaDF) = 14 THEN '0' ELSE D14 END,
					D15 = CASE WHEN DAY(@FechaDF) = 15 THEN '0' ELSE D15 END,
					D16 = CASE WHEN DAY(@FechaDF) = 16 THEN '0' ELSE D16 END,
					D17 = CASE WHEN DAY(@FechaDF) = 17 THEN '0' ELSE D17 END,
					D18 = CASE WHEN DAY(@FechaDF) = 18 THEN '0' ELSE D18 END,
					D19 = CASE WHEN DAY(@FechaDF) = 19 THEN '0' ELSE D19 END,
					D20 = CASE WHEN DAY(@FechaDF) = 20 THEN '0' ELSE D20 END,
					D21 = CASE WHEN DAY(@FechaDF) = 21 THEN '0' ELSE D21 END,
					D22 = CASE WHEN DAY(@FechaDF) = 22 THEN '0' ELSE D22 END,
					D23 = CASE WHEN DAY(@FechaDF) = 23 THEN '0' ELSE D23 END,
					D24 = CASE WHEN DAY(@FechaDF) = 24 THEN '0' ELSE D24 END,
					D25 = CASE WHEN DAY(@FechaDF) = 25 THEN '0' ELSE D25 END,
					D26 = CASE WHEN DAY(@FechaDF) = 26 THEN '0' ELSE D26 END,
					D27 = CASE WHEN DAY(@FechaDF) = 27 THEN '0' ELSE D27 END,
					D28 = CASE WHEN DAY(@FechaDF) = 28 THEN '0' ELSE D28 END,
					D29 = CASE WHEN DAY(@FechaDF) = 29 THEN '0' ELSE D29 END,
					D30 = CASE WHEN DAY(@FechaDF) = 30 THEN '0' ELSE D30 END,
					D31 = CASE WHEN DAY(@FechaDF) = 31 THEN '0' ELSE D31 END
				WHERE idCondicion = @idCondicionDF AND Anio = YEAR(@FechaDF) AND Mes = MONTH(@FechaDF)

				SELECT @n = MIN(ItemDF) FROM @T_CondicionDF WHERE @n < ItemDF
			END
		END
	END

	-- GERARDO - 22/10
	IF (@idCondicion IN (1,4,5,6,10,8)) BEGIN
		IF (NOT EXISTS(SELECT * FROM ReportesApp_Operaciones_Operatividad_ViewPlataformas WHERE idCondicionPL = @idCondicion
		AND Anio = RIGHT(@Periodo,4) AND Mes = LEFT(@Periodo,2))) BEGIN
			DECLARE @n2 INT, @UltimoDiaMes2 TINYINT = DAY(@fechafin), @idCondicionDF2 INT, @FechaDF2 DATE

			SELECT @n2 = MIN(ItemDF) FROM @T_CondicionPL

			IF (@n2 IS NULL) BEGIN
				INSERT INTO ReportesApp_Operaciones_Operatividad_ViewPlataformas(idCondicionPL,Anio,Mes,NroDias)
				VALUES(@idCondicion, RIGHT(@Periodo,4), LEFT(@Periodo,2), @UltimoDiaMes2)
			END

			WHILE (@n2 IS NOT NULL) BEGIN
				SELECT @idCondicionDF2 = T.idCondicion, @FechaDF2 = T.FechaDF
				FROM @T_CondicionPL T
				WHERE ItemDF = @n2

				IF NOT EXISTS(SELECT TOP 1 Mes FROM ReportesApp_Operaciones_Operatividad_ViewPlataformas WITH(NOLOCK) WHERE idCondicionPL = @idCondicionDF2 
				AND Anio = YEAR(@FechaDF2) AND Mes = MONTH(@FechaDF2)) BEGIN
					INSERT INTO ReportesApp_Operaciones_Operatividad_ViewPlataformas(idCondicionPL,Anio,Mes,NroDias)
					VALUES(@idCondicionDF2, YEAR(@FechaDF2), MONTH(@FechaDF2), @UltimoDiaMes2)						 
				END

				PRINT CAST(@n2 AS VARCHAR(10))+ ' --> @idCondicionDF2:'+ CAST(@idCondicionDF2 AS VARCHAR(10)) +' @FechaDF2: '+CONVERT(VARCHAR(10),@FechaDF2,103)+' >>> 0'

				UPDATE ReportesApp_Operaciones_Operatividad_ViewPlataformas SET 
					D1 = CASE WHEN DAY(@FechaDF2) = 1 THEN '0' ELSE D1 END,
					D2 = CASE WHEN DAY(@FechaDF2) = 2 THEN '0' ELSE D2 END,
					D3 = CASE WHEN DAY(@FechaDF2) = 3 THEN '0' ELSE D3 END,
					D4 = CASE WHEN DAY(@FechaDF2) = 4 THEN '0' ELSE D4 END,
					D5 = CASE WHEN DAY(@FechaDF2) = 5 THEN '0' ELSE D5 END,
					D6 = CASE WHEN DAY(@FechaDF2) = 6 THEN '0' ELSE D6 END,
					D7 = CASE WHEN DAY(@FechaDF2) = 7 THEN '0' ELSE D7 END,
					D8 = CASE WHEN DAY(@FechaDF2) = 8 THEN '0' ELSE D8 END,
					D9 = CASE WHEN DAY(@FechaDF2) = 9 THEN '0' ELSE D9 END,
					D10 = CASE WHEN DAY(@FechaDF2) = 10 THEN '0' ELSE D10 END,
					D11 = CASE WHEN DAY(@FechaDF2) = 11 THEN '0' ELSE D11 END,
					D12 = CASE WHEN DAY(@FechaDF2) = 12 THEN '0' ELSE D12 END,
					D13 = CASE WHEN DAY(@FechaDF2) = 13 THEN '0' ELSE D13 END,
					D14 = CASE WHEN DAY(@FechaDF2) = 14 THEN '0' ELSE D14 END,
					D15 = CASE WHEN DAY(@FechaDF2) = 15 THEN '0' ELSE D15 END,
					D16 = CASE WHEN DAY(@FechaDF2) = 16 THEN '0' ELSE D16 END,
					D17 = CASE WHEN DAY(@FechaDF2) = 17 THEN '0' ELSE D17 END,
					D18 = CASE WHEN DAY(@FechaDF2) = 18 THEN '0' ELSE D18 END,
					D19 = CASE WHEN DAY(@FechaDF2) = 19 THEN '0' ELSE D19 END,
					D20 = CASE WHEN DAY(@FechaDF2) = 20 THEN '0' ELSE D20 END,
					D21 = CASE WHEN DAY(@FechaDF2) = 21 THEN '0' ELSE D21 END,
					D22 = CASE WHEN DAY(@FechaDF2) = 22 THEN '0' ELSE D22 END,
					D23 = CASE WHEN DAY(@FechaDF2) = 23 THEN '0' ELSE D23 END,
					D24 = CASE WHEN DAY(@FechaDF2) = 24 THEN '0' ELSE D24 END,
					D25 = CASE WHEN DAY(@FechaDF2) = 25 THEN '0' ELSE D25 END,
					D26 = CASE WHEN DAY(@FechaDF2) = 26 THEN '0' ELSE D26 END,
					D27 = CASE WHEN DAY(@FechaDF2) = 27 THEN '0' ELSE D27 END,
					D28 = CASE WHEN DAY(@FechaDF2) = 28 THEN '0' ELSE D28 END,
					D29 = CASE WHEN DAY(@FechaDF2) = 29 THEN '0' ELSE D29 END,
					D30 = CASE WHEN DAY(@FechaDF2) = 30 THEN '0' ELSE D30 END,
					D31 = CASE WHEN DAY(@FechaDF2) = 31 THEN '0' ELSE D31 END
				WHERE idCondicionPL = @idCondicionDF2 AND Anio = YEAR(@FechaDF2) AND Mes = MONTH(@FechaDF2)

				SELECT @n2 = MIN(ItemDF) FROM @T_CondicionPL WHERE @n2 < ItemDF
			END
		END
	END

	IF (@idCondicion IN (3,16,5,6,10,8)) BEGIN
		IF (NOT EXISTS(SELECT * FROM ReportesApp_Operaciones_Operatividad_ViewTolvas WHERE idCondicionTL = @idCondicion
		AND Anio = RIGHT(@Periodo,4) AND Mes = LEFT(@Periodo,2))) BEGIN
			DECLARE @n3 INT, @UltimoDiaMes3 TINYINT = DAY(@fechafin), @idCondicionDF3 INT, @FechaDF3 DATE

			SELECT @n3 = MIN(ItemDF) FROM @T_CondicionTL

			IF (@n3 IS NULL) BEGIN
				INSERT INTO ReportesApp_Operaciones_Operatividad_ViewTolvas(idCondicionTL,Anio,Mes,NroDias)
				VALUES(@idCondicion, RIGHT(@Periodo,4), LEFT(@Periodo,2), @UltimoDiaMes3)
			END

			WHILE (@n3 IS NOT NULL) BEGIN
				SELECT @idCondicionDF3 = T.idCondicion, @FechaDF3 = T.FechaDF
				FROM @T_CondicionTL T
				WHERE ItemDF = @n3

				IF NOT EXISTS(SELECT TOP 1 Mes FROM ReportesApp_Operaciones_Operatividad_ViewTolvas WITH(NOLOCK) WHERE idCondicionTL = @idCondicionDF3
				AND Anio = YEAR(@FechaDF3) AND Mes = MONTH(@FechaDF3)) BEGIN
					INSERT INTO ReportesApp_Operaciones_Operatividad_ViewTolvas(idCondicionTL,Anio,Mes,NroDias)
					VALUES(@idCondicionDF3, YEAR(@FechaDF3), MONTH(@FechaDF3), @UltimoDiaMes3)						 
				END

				PRINT CAST(@n3 AS VARCHAR(10))+ ' --> @idCondicionDF3:'+ CAST(@idCondicionDF3 AS VARCHAR(10)) +' @FechaDF3: '+CONVERT(VARCHAR(10),@FechaDF3,103)+' >>> 0'

				UPDATE ReportesApp_Operaciones_Operatividad_ViewTolvas SET 
					D1 = CASE WHEN DAY(@FechaDF3) = 1 THEN '0' ELSE D1 END,
					D2 = CASE WHEN DAY(@FechaDF3) = 2 THEN '0' ELSE D2 END,
					D3 = CASE WHEN DAY(@FechaDF3) = 3 THEN '0' ELSE D3 END,
					D4 = CASE WHEN DAY(@FechaDF3) = 4 THEN '0' ELSE D4 END,
					D5 = CASE WHEN DAY(@FechaDF3) = 5 THEN '0' ELSE D5 END,
					D6 = CASE WHEN DAY(@FechaDF3) = 6 THEN '0' ELSE D6 END,
					D7 = CASE WHEN DAY(@FechaDF3) = 7 THEN '0' ELSE D7 END,
					D8 = CASE WHEN DAY(@FechaDF3) = 8 THEN '0' ELSE D8 END,
					D9 = CASE WHEN DAY(@FechaDF3) = 9 THEN '0' ELSE D9 END,
					D10 = CASE WHEN DAY(@FechaDF3) = 10 THEN '0' ELSE D10 END,
					D11 = CASE WHEN DAY(@FechaDF3) = 11 THEN '0' ELSE D11 END,
					D12 = CASE WHEN DAY(@FechaDF3) = 12 THEN '0' ELSE D12 END,
					D13 = CASE WHEN DAY(@FechaDF3) = 13 THEN '0' ELSE D13 END,
					D14 = CASE WHEN DAY(@FechaDF3) = 14 THEN '0' ELSE D14 END,
					D15 = CASE WHEN DAY(@FechaDF3) = 15 THEN '0' ELSE D15 END,
					D16 = CASE WHEN DAY(@FechaDF3) = 16 THEN '0' ELSE D16 END,
					D17 = CASE WHEN DAY(@FechaDF3) = 17 THEN '0' ELSE D17 END,
					D18 = CASE WHEN DAY(@FechaDF3) = 18 THEN '0' ELSE D18 END,
					D19 = CASE WHEN DAY(@FechaDF3) = 19 THEN '0' ELSE D19 END,
					D20 = CASE WHEN DAY(@FechaDF3) = 20 THEN '0' ELSE D20 END,
					D21 = CASE WHEN DAY(@FechaDF3) = 21 THEN '0' ELSE D21 END,
					D22 = CASE WHEN DAY(@FechaDF3) = 22 THEN '0' ELSE D22 END,
					D23 = CASE WHEN DAY(@FechaDF3) = 23 THEN '0' ELSE D23 END,
					D24 = CASE WHEN DAY(@FechaDF3) = 24 THEN '0' ELSE D24 END,
					D25 = CASE WHEN DAY(@FechaDF3) = 25 THEN '0' ELSE D25 END,
					D26 = CASE WHEN DAY(@FechaDF3) = 26 THEN '0' ELSE D26 END,
					D27 = CASE WHEN DAY(@FechaDF3) = 27 THEN '0' ELSE D27 END,
					D28 = CASE WHEN DAY(@FechaDF3) = 28 THEN '0' ELSE D28 END,
					D29 = CASE WHEN DAY(@FechaDF3) = 29 THEN '0' ELSE D29 END,
					D30 = CASE WHEN DAY(@FechaDF3) = 30 THEN '0' ELSE D30 END,
					D31 = CASE WHEN DAY(@FechaDF3) = 31 THEN '0' ELSE D31 END
				WHERE idCondicionTL = @idCondicionDF3 AND Anio = YEAR(@FechaDF3) AND Mes = MONTH(@FechaDF3)

				SELECT @n3 = MIN(ItemDF) FROM @T_CondicionTL WHERE @n3 < ItemDF
			END
		END
	END

	IF (@idCondicion IN (2,5,6,10,8)) BEGIN
		IF (NOT EXISTS(SELECT * FROM ReportesApp_Operaciones_Operatividad_ViewCisternas WHERE idCondicionCS = @idCondicion
		AND Anio = RIGHT(@Periodo,4) AND Mes = LEFT(@Periodo,2))) BEGIN
			DECLARE @n4 INT, @UltimoDiaMes4 TINYINT = DAY(@fechafin), @idCondicionDF4 INT, @FechaDF4 DATE

			SELECT @n4 = MIN(ItemDF) FROM @T_CondicionCS

			IF (@n4 IS NULL) BEGIN
				INSERT INTO ReportesApp_Operaciones_Operatividad_ViewCisternas(idCondicionCS,Anio,Mes,NroDias)
				VALUES(@idCondicion, RIGHT(@Periodo,4), LEFT(@Periodo,2), @UltimoDiaMes4)
			END

			WHILE (@n4 IS NOT NULL) BEGIN
				SELECT @idCondicionDF4 = T.idCondicion, @FechaDF4 = T.FechaDF
				FROM @T_CondicionCS T
				WHERE ItemDF = @n4

				IF NOT EXISTS(SELECT TOP 1 Mes FROM ReportesApp_Operaciones_Operatividad_ViewCisternas WITH(NOLOCK) WHERE idCondicionCS = @idCondicionDF4
				AND Anio = YEAR(@FechaDF4) AND Mes = MONTH(@FechaDF4)) BEGIN
					INSERT INTO ReportesApp_Operaciones_Operatividad_ViewCisternas(idCondicionCS,Anio,Mes,NroDias)
					VALUES(@idCondicionDF4, YEAR(@FechaDF4), MONTH(@FechaDF4), @UltimoDiaMes4)						 
				END

				PRINT CAST(@n4 AS VARCHAR(10))+ ' --> @idCondicionDF4:'+ CAST(@idCondicionDF4 AS VARCHAR(10)) +' @FechaDF4: '+CONVERT(VARCHAR(10),@FechaDF4,103)+' >>> 0'

				UPDATE ReportesApp_Operaciones_Operatividad_ViewCisternas SET 
					D1 = CASE WHEN DAY(@FechaDF4) = 1 THEN '0' ELSE D1 END,
					D2 = CASE WHEN DAY(@FechaDF4) = 2 THEN '0' ELSE D2 END,
					D3 = CASE WHEN DAY(@FechaDF4) = 3 THEN '0' ELSE D3 END,
					D4 = CASE WHEN DAY(@FechaDF4) = 4 THEN '0' ELSE D4 END,
					D5 = CASE WHEN DAY(@FechaDF4) = 5 THEN '0' ELSE D5 END,
					D6 = CASE WHEN DAY(@FechaDF4) = 6 THEN '0' ELSE D6 END,
					D7 = CASE WHEN DAY(@FechaDF4) = 7 THEN '0' ELSE D7 END,
					D8 = CASE WHEN DAY(@FechaDF4) = 8 THEN '0' ELSE D8 END,
					D9 = CASE WHEN DAY(@FechaDF4) = 9 THEN '0' ELSE D9 END,
					D10 = CASE WHEN DAY(@FechaDF4) = 10 THEN '0' ELSE D10 END,
					D11 = CASE WHEN DAY(@FechaDF4) = 11 THEN '0' ELSE D11 END,
					D12 = CASE WHEN DAY(@FechaDF4) = 12 THEN '0' ELSE D12 END,
					D13 = CASE WHEN DAY(@FechaDF4) = 13 THEN '0' ELSE D13 END,
					D14 = CASE WHEN DAY(@FechaDF4) = 14 THEN '0' ELSE D14 END,
					D15 = CASE WHEN DAY(@FechaDF4) = 15 THEN '0' ELSE D15 END,
					D16 = CASE WHEN DAY(@FechaDF4) = 16 THEN '0' ELSE D16 END,
					D17 = CASE WHEN DAY(@FechaDF4) = 17 THEN '0' ELSE D17 END,
					D18 = CASE WHEN DAY(@FechaDF4) = 18 THEN '0' ELSE D18 END,
					D19 = CASE WHEN DAY(@FechaDF4) = 19 THEN '0' ELSE D19 END,
					D20 = CASE WHEN DAY(@FechaDF4) = 20 THEN '0' ELSE D20 END,
					D21 = CASE WHEN DAY(@FechaDF4) = 21 THEN '0' ELSE D21 END,
					D22 = CASE WHEN DAY(@FechaDF4) = 22 THEN '0' ELSE D22 END,
					D23 = CASE WHEN DAY(@FechaDF4) = 23 THEN '0' ELSE D23 END,
					D24 = CASE WHEN DAY(@FechaDF4) = 24 THEN '0' ELSE D24 END,
					D25 = CASE WHEN DAY(@FechaDF4) = 25 THEN '0' ELSE D25 END,
					D26 = CASE WHEN DAY(@FechaDF4) = 26 THEN '0' ELSE D26 END,
					D27 = CASE WHEN DAY(@FechaDF4) = 27 THEN '0' ELSE D27 END,
					D28 = CASE WHEN DAY(@FechaDF4) = 28 THEN '0' ELSE D28 END,
					D29 = CASE WHEN DAY(@FechaDF4) = 29 THEN '0' ELSE D29 END,
					D30 = CASE WHEN DAY(@FechaDF4) = 30 THEN '0' ELSE D30 END,
					D31 = CASE WHEN DAY(@FechaDF4) = 31 THEN '0' ELSE D31 END
				WHERE idCondicionCS = @idCondicionDF4 AND Anio = YEAR(@FechaDF4) AND Mes = MONTH(@FechaDF4)

				SELECT @n4 = MIN(ItemDF) FROM @T_CondicionCS WHERE @n4 < ItemDF
			END
		END
	END

	IF (@idCondicion IN (1,4,5,6,13,10,8)) BEGIN
		IF (NOT EXISTS(SELECT * FROM ReportesApp_Operaciones_Operatividad_ViewCortineras WHERE idCondicionCR = @idCondicion
		AND Anio = RIGHT(@Periodo,4) AND Mes = LEFT(@Periodo,2))) BEGIN
			DECLARE @n5 INT, @UltimoDiaMes5 TINYINT = DAY(@fechafin), @idCondicionDF5 INT, @FechaDF5 DATE

			SELECT @n5 = MIN(ItemDF) FROM @T_CondicionCR

			IF (@n5 IS NULL) BEGIN
				INSERT INTO ReportesApp_Operaciones_Operatividad_ViewCortineras(idCondicionCR,Anio,Mes,NroDias)
				VALUES(@idCondicion, RIGHT(@Periodo,4), LEFT(@Periodo,2), @UltimoDiaMes5)
			END

			WHILE (@n5 IS NOT NULL) BEGIN
				SELECT @idCondicionDF5 = T.idCondicion, @FechaDF5 = T.FechaDF
				FROM @T_CondicionCR T
				WHERE ItemDF = @n5

				IF NOT EXISTS(SELECT TOP 1 Mes FROM ReportesApp_Operaciones_Operatividad_ViewCortineras WITH(NOLOCK) WHERE idCondicionCR = @idCondicionDF5
				AND Anio = YEAR(@FechaDF5) AND Mes = MONTH(@FechaDF5)) BEGIN
					INSERT INTO ReportesApp_Operaciones_Operatividad_ViewCortineras(idCondicionCR,Anio,Mes,NroDias)
					VALUES(@idCondicionDF5, YEAR(@FechaDF5), MONTH(@FechaDF5), @UltimoDiaMes5)						 
				END

				PRINT CAST(@n5 AS VARCHAR(10))+ ' --> @idCondicionDF5:'+ CAST(@idCondicionDF5 AS VARCHAR(10)) +' @FechaDF5: '+CONVERT(VARCHAR(10),@FechaDF5,103)+' >>> 0'

				UPDATE ReportesApp_Operaciones_Operatividad_ViewCortineras SET 
					D1 = CASE WHEN DAY(@FechaDF5) = 1 THEN '0' ELSE D1 END,
					D2 = CASE WHEN DAY(@FechaDF5) = 2 THEN '0' ELSE D2 END,
					D3 = CASE WHEN DAY(@FechaDF5) = 3 THEN '0' ELSE D3 END,
					D4 = CASE WHEN DAY(@FechaDF5) = 4 THEN '0' ELSE D4 END,
					D5 = CASE WHEN DAY(@FechaDF5) = 5 THEN '0' ELSE D5 END,
					D6 = CASE WHEN DAY(@FechaDF5) = 6 THEN '0' ELSE D6 END,
					D7 = CASE WHEN DAY(@FechaDF5) = 7 THEN '0' ELSE D7 END,
					D8 = CASE WHEN DAY(@FechaDF5) = 8 THEN '0' ELSE D8 END,
					D9 = CASE WHEN DAY(@FechaDF5) = 9 THEN '0' ELSE D9 END,
					D10 = CASE WHEN DAY(@FechaDF5) = 10 THEN '0' ELSE D10 END,
					D11 = CASE WHEN DAY(@FechaDF5) = 11 THEN '0' ELSE D11 END,
					D12 = CASE WHEN DAY(@FechaDF5) = 12 THEN '0' ELSE D12 END,
					D13 = CASE WHEN DAY(@FechaDF5) = 13 THEN '0' ELSE D13 END,
					D14 = CASE WHEN DAY(@FechaDF5) = 14 THEN '0' ELSE D14 END,
					D15 = CASE WHEN DAY(@FechaDF5) = 15 THEN '0' ELSE D15 END,
					D16 = CASE WHEN DAY(@FechaDF5) = 16 THEN '0' ELSE D16 END,
					D17 = CASE WHEN DAY(@FechaDF5) = 17 THEN '0' ELSE D17 END,
					D18 = CASE WHEN DAY(@FechaDF5) = 18 THEN '0' ELSE D18 END,
					D19 = CASE WHEN DAY(@FechaDF5) = 19 THEN '0' ELSE D19 END,
					D20 = CASE WHEN DAY(@FechaDF5) = 20 THEN '0' ELSE D20 END,
					D21 = CASE WHEN DAY(@FechaDF5) = 21 THEN '0' ELSE D21 END,
					D22 = CASE WHEN DAY(@FechaDF5) = 22 THEN '0' ELSE D22 END,
					D23 = CASE WHEN DAY(@FechaDF5) = 23 THEN '0' ELSE D23 END,
					D24 = CASE WHEN DAY(@FechaDF5) = 24 THEN '0' ELSE D24 END,
					D25 = CASE WHEN DAY(@FechaDF5) = 25 THEN '0' ELSE D25 END,
					D26 = CASE WHEN DAY(@FechaDF5) = 26 THEN '0' ELSE D26 END,
					D27 = CASE WHEN DAY(@FechaDF5) = 27 THEN '0' ELSE D27 END,
					D28 = CASE WHEN DAY(@FechaDF5) = 28 THEN '0' ELSE D28 END,
					D29 = CASE WHEN DAY(@FechaDF5) = 29 THEN '0' ELSE D29 END,
					D30 = CASE WHEN DAY(@FechaDF5) = 30 THEN '0' ELSE D30 END,
					D31 = CASE WHEN DAY(@FechaDF5) = 31 THEN '0' ELSE D31 END
				WHERE idCondicionCR = @idCondicionDF5 AND Anio = YEAR(@FechaDF5) AND Mes = MONTH(@FechaDF5)

				SELECT @n5 = MIN(ItemDF) FROM @T_CondicionCR WHERE @n5 < ItemDF
			END
		END
	END
	-- GERARDO - 22/10
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

-----------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author Name:	GERARDO REYES
-- Create date: 06-09-2024
-- Description:	LISTAR TABLA DE OPERATIVIDAD - CARRETA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Operatividad_ListarOperatividadCarreta]
@Periodo VARCHAR(6)
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

	DECLARE @T_Prueba TABLE (idCondicion INT, Operativas VARCHAR(50), Condicion VARCHAR(250), Orden INT,
							 D1 VARCHAR(15), D2 VARCHAR(15), D3 VARCHAR(15), D4 VARCHAR(15), D5 VARCHAR(15), D6 VARCHAR(15),
							 D7 VARCHAR(15), D8 VARCHAR(15), D9 VARCHAR(15), D10 VARCHAR(15), D11 VARCHAR(15), D12 VARCHAR(15),
							 D13 VARCHAR(15), D14 VARCHAR(15), D15 VARCHAR(15), D16 VARCHAR(15), D17 VARCHAR(15), D18 VARCHAR(15),
							 D19 VARCHAR(15), D20 VARCHAR(15), D21 VARCHAR(15), D22 VARCHAR(15), D23 VARCHAR(15), D24 VARCHAR(15),
							 D25 VARCHAR(15), D26 VARCHAR(15), D27 VARCHAR(15), D28 VARCHAR(15), D29 VARCHAR(15), D30 VARCHAR(15), D31 VARCHAR(15))

	DECLARE @D1 VARCHAR(15), @D2 VARCHAR(15), @D3 VARCHAR(15), @D4 VARCHAR(15), @D5 VARCHAR(15), @D6 VARCHAR(15), @D7 VARCHAR(15), @D8 VARCHAR(15),
			@D9 VARCHAR(15), @D10 VARCHAR(15), @D11 VARCHAR(15), @D12 VARCHAR(15), @D13 VARCHAR(15), @D14 VARCHAR(15), @D15 VARCHAR(15), @D16 VARCHAR(15),
			@D17 VARCHAR(15), @D18 VARCHAR(15), @D19 VARCHAR(15), @D20 VARCHAR(15), @D21 VARCHAR(15), @D22 VARCHAR(15), @D23 VARCHAR(15), @D24 VARCHAR(15),
			@D25 VARCHAR(15), @D26 VARCHAR(15), @D27 VARCHAR(15), @D28 VARCHAR(15), @D29 VARCHAR(15), @D30 VARCHAR(15), @D31 VARCHAR(15)

	INSERT INTO @T_Prueba (Operativas,Condicion)
	VALUES ('Operativas', '<CONDICION>')

	DECLARE @FechaIni DATE, @FechaFin DATE, @FechaVer DATE

	IF (@FDesde IS NOT NULL) BEGIN 
		SET @FechaVer = CAST(CAST(@DiaInicio As Varchar(2))+'/'+ RIGHT('0'+ CAST(month(@FDesde) AS VARCHAR(2)),2)+'/'+CAST(@Anio as VARCHAR(4)) AS DATE)
	END
	ELSE BEGIN
		SET @FechaVer = CAST(CAST(@DiaInicio As Varchar(2))+'/'+ RIGHT('0'+ CAST(@Mes AS VARCHAR(2)),2)+'/'+CAST(@Anio as VARCHAR(4)) AS DATE)
	END

	SET @FechaIni = @FechaVer
	SET @FechaFin = DATEADD(D,-1, DATEADD(MONTH,1,@FechaVer))

	INSERT INTO @T_Prueba (idCondicion, Operativas, Condicion, Orden)
	SELECT DISTINCT C.idCondicion, C.Operativas AS 'CONCEPTO', C.Condicion AS 'CONDICION', C.Orden
	FROM ReportesApp_Operaciones_Operatividad_Condiciones C
	INNER JOIN ReportesApp_Operaciones_Operatividad_ViewCarretas T ON T.idCondicion = C.idCondicion 
	WHERE T.Anio = RIGHT(@Periodo,4) AND T.Mes = LEFT(@Periodo,2)

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
		WHERE Condicion = '<CONDICION>'

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
		LEFT JOIN ReportesApp_Operaciones_Operatividad_ViewCarretas AS A WITH(NOLOCK) ON T.idCondicion = A.idCondicion 
		WHERE A.Anio = YEAR(@FechaVer) AND A.Mes = MONTH(@FechaVer) 

		SET @FechaVer = DATEADD(D,1,@FechaVer)
			
		IF (@FDesde IS NOT NULL) AND (@NroDia = @CantDias OR @CantDias = 0)  BREAK;
		
		SET @NroDia = @NroDia + 1
	END

	IF (@CantDias = 28) BEGIN
		SELECT @D1=D1, @D2=D2, @D3=D3, @D4=D4, @D5=D5, @D6=D6, @D7=D7, @D8=D8, @D9=D9, @D10=D10, @D11=D11, @D12=D12, @D13=D13, @D14=D14,
		@D15=D15, @D16=D16, @D17=D17, @D18=D18, @D19=D19, @D20=D20, @D21=D21, @D22=D22, @D23=D23, @D24=D24, @D25=D25, @D26=D26, @D27=D27, @D28=D28
		FROM @T_Prueba WHERE Condicion = '<CONDICION>'
	END

	IF (@CantDias = 29) BEGIN
		SELECT @D1=D1, @D2=D2, @D3=D3, @D4=D4, @D5=D5, @D6=D6, @D7=D7, @D8=D8, @D9=D9, @D10=D10, @D11=D11, @D12=D12, @D13=D13, @D14=D14, @D15=D15,
		@D16=D16, @D17=D17, @D18=D18, @D19=D19, @D20=D20, @D21=D21, @D22=D22, @D23=D23, @D24=D24, @D25=D25, @D26=D26, @D27=D27, @D28=D28, @D29=D29
		FROM @T_Prueba WHERE Condicion = '<CONDICION>'
	END

	IF (@CantDias = 30) BEGIN
		SELECT @D1=D1, @D2=D2, @D3=D3, @D4=D4, @D5=D5, @D6=D6, @D7=D7, @D8=D8, @D9=D9, @D10=D10, @D11=D11, @D12=D12, @D13=D13, @D14=D14, @D15=D15,
		@D16=D16, @D17=D17, @D18=D18, @D19=D19, @D20=D20, @D21=D21, @D22=D22, @D23=D23, @D24=D24, @D25=D25, @D26=D26, @D27=D27, @D28=D28, @D29=D29, @D30=D30
		FROM @T_Prueba WHERE Condicion = '<CONDICION>'
	END

	IF (@CantDias = 31) BEGIN
		SELECT @D1=D1, @D2=D2, @D3=D3, @D4=D4, @D5=D5, @D6=D6, @D7=D7, @D8=D8, @D9=D9, @D10=D10, @D11=D11, @D12=D12, @D13=D13, @D14=D14, @D15=D15, @D16=D16, @D17=D17,
		@D18=D18, @D19=D19, @D20=D20, @D21=D21, @D22=D22, @D23=D23, @D24=D24, @D25=D25, @D26=D26, @D27=D27, @D28=D28, @D29=D29, @D30=D30, @D29=D29, @D30=D30, @D31=D31
		FROM @T_Prueba WHERE Condicion = '<CONDICION>'	
	END

	CREATE TABLE #PruebaView (idCondicion INT, Operativas VARCHAR(50), Condicion VARCHAR(250), Orden INT,
							  D1 VARCHAR(15),D2 VARCHAR(15),D3 VARCHAR(15),D4 VARCHAR(15),D5 VARCHAR(15),
							  D6 VARCHAR(15),D7 VARCHAR(15),D8 VARCHAR(15),D9 VARCHAR(15),D10 VARCHAR(15),
							  D11 VARCHAR(15),D12 VARCHAR(15),D13 VARCHAR(15),D14 VARCHAR(15),D15 VARCHAR(15),
							  D16 VARCHAR(15),D17 VARCHAR(15),D18 VARCHAR(15),D19 VARCHAR(15),D20 VARCHAR(15),
							  D21 VARCHAR(15),D22 VARCHAR(15),D23 VARCHAR(15),D24 VARCHAR(15),D25 VARCHAR(15),
							  D26 VARCHAR(15),D27 VARCHAR(15),D28 VARCHAR(15),D29 VARCHAR(15),D30 VARCHAR(15),
							  D31 VARCHAR(15))

	DECLARE @SQL varchar(5000)

	INSERT INTO #PruebaView
	SELECT * FROM @T_Prueba WHERE Condicion<>'<CONDICION>'

	IF @CantDias = 28 BEGIN
		SET @SQL='SELECT idCondicion, Operativas AS CONCEPTO, Condicion AS CONDICION,
			D1 AS '+@D1+',D2 AS '+@D2+',D3 AS '+@D3+',D4 AS '+@D4+',D5 AS '+@D5+',D6 AS '+@D6+',D7 AS '+@D7+',D8 AS '+@D8+
		    ',D9 AS '+@D9+',D10 AS '+@D10+',D11 AS '+@D11+',D12 AS '+@D12+',D13 AS '+@D13+',D14 AS '+@D14+',D15 AS '+@D15+
			',D16 AS '+@D16+',D17 AS '+@D17+',D18 AS '+@D18+',D19 AS '+@D19+',D20 AS '+@D20+',D21 AS '+@D21+',D22 AS '+@D22+
			',D23 AS '+@D23+',D24 AS '+@D24+',D25 AS '+@D25+',D26 AS '+@D26+',D27 AS '+@D27+',D28 AS '+@D28+' FROM #PruebaView ORDER BY Orden'
		
		EXEC (@SQL)
		DROP TABLE #PruebaView
	END

	IF @CantDias = 29 BEGIN
		SET @SQL='SELECT idCondicion, Operativas AS CONCEPTO, Condicion AS CONDICION,
			D1 AS '+@D1+',D2 AS '+@D2+',D3 AS '+@D3+',D4 AS '+@D4+',D5 AS '+@D5+',D6 AS '+@D6+',D7 AS '+@D7+',D8 AS '+@D8+
			',D9 AS '+@D9+',D10 AS '+@D10+',D11 AS '+@D11+',D12 AS '+@D12+',D13 AS '+@D13+',D14 AS '+@D14+',D15 AS '+@D15+
			',D16 AS '+@D16+',D17 AS '+@D17+',D18 AS '+@D18+',D19 AS '+@D19+',D20 AS '+@D20+',D21 AS '+@D21+',D22 AS '+@D22+
			',D23 AS '+@D23+',D24 AS '+@D24+',D25 AS '+@D25+',D26 AS '+@D26+',D27 AS '+@D27+',D28 AS '+@D28+',D29 AS '+@D29+' FROM #PruebaView ORDER BY Orden'
		
		EXEC (@SQL)
		DROP TABLE #PruebaView	
	END

	IF @CantDias = 30 BEGIN
		SET @SQL='SELECT idCondicion, Operativas AS CONCEPTO, Condicion AS CONDICION,
			D1 AS '+@D1+',D2 AS '+@D2+',D3 AS '+@D3+',D4 AS '+@D4+',D5 AS '+@D5+',D6 AS '+@D6+',D7 AS '+@D7+',D8 AS '+@D8+
			',D9 AS '+@D9+',D10 AS '+@D10+',D11 AS '+@D11+',D12 AS '+@D12+',D13 AS '+@D13+',D14 AS '+@D14+',D15 AS '+@D15+
			',D16 AS '+@D16+',D17 AS '+@D17+',D18 AS '+@D18+',D19 AS '+@D19+',D20 AS '+@D20+',D21 AS '+@D21+',D22 AS '+@D22+
			',D23 AS '+@D23+',D24 AS '+@D24+',D25 AS '+@D25+',D26 AS '+@D26+',D27 AS '+@D27+',D28 AS '+@D28+',D29 AS '+@D29+
			',D30 AS '+@D30+' FROM #PruebaView ORDER BY Orden'
		
		EXEC (@SQL)
		DROP TABLE #PruebaView
	END

	IF @CantDias = 31 BEGIN
		SET @SQL='SELECT idCondicion, Operativas AS CONCEPTO, Condicion AS CONDICION,
			D1 AS '+@D1+',D2 AS '+@D2+',D3 AS '+@D3+',D4 AS '+@D4+',D5 AS '+@D5+',D6 AS '+@D6+',D7 AS '+@D7+',D8 AS '+@D8+
			',D9 AS '+@D9+',D10 AS '+@D10+',D11 AS '+@D11+',D12 AS '+@D12+',D13 AS '+@D13+',D14 AS '+@D14+',D15 AS '+@D15+
			',D16 AS '+@D16+',D17 AS '+@D17+',D18 AS '+@D18+',D19 AS '+@D19+',D20 AS '+@D20+',D21 AS '+@D21+',D22 AS '+@D22+
			',D23 AS '+@D23+',D24 AS '+@D24+',D25 AS '+@D25+',D26 AS '+@D26+',D27 AS '+@D27+',D28 AS '+@D28+',D29 AS '+@D29+
			',D30 AS '+@D30+',D31 AS '+@D31+' FROM #PruebaView ORDER BY Orden'
		
		EXEC (@SQL)
		DROP TABLE #PruebaView
	END
END

-----------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 07-09-2024
-- Description:	REGISTRAR OPERATIVIDAD - CARRETAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Operatividad_RegistrarCarretas]
@Periodo VARCHAR(6),
@xmlOperatividad VARCHAR(MAX),
@idCondicion INT,
@idOperacion INT,
@Usuario VARCHAR(20)
AS
DECLARE @exito VARCHAR(MAX)
DECLARE @Operacion VARCHAR(5), @Condicion VARCHAR(30)
DECLARE @correlativo INT, @co2 INT
DECLARE @OP_PLACAS TABLE (Contador INT, IdUnidad INT, Fecha DATE, idOperacion INT, idCondicion INT)
DECLARE @CondicionCompleta VARCHAR(50)
DECLARE @idact INT 
DECLARE @Nro INT
DECLARE @CantidadCondiciones INT

SET @exito = '0 = Condición de Unidad Registrada.'

SET @correlativo = (SELECT MAX(Contador) FROM @OP_PLACAS)
SET @correlativo = ISNULL(@correlativo,0) + 1 
SET @Nro = 1

BEGIN TRAN
BEGIN TRY
	IF @xmlOperatividad IS NOT NULL BEGIN  
		EXEC sp_xml_preparedocument @idact OUT, @xmlOperatividad  

		INSERT INTO @OP_PLACAS(Contador, IdUnidad, Fecha, idOperacion, idCondicion)  
		SELECT ROW_NUMBER() OVER(ORDER BY @correlativo ASC), IdUnidad, Fecha, @idOperacion, @idCondicion
		FROM OPENXML(@idact,'/r/d',3) WITH(IdUnidad INT, Fecha DATE)  
		     
		EXEC sp_xml_removedocument @idact  
	END

	WHILE (@Nro <= (SELECT MAX(Contador) FROM @OP_PLACAS)) BEGIN
		SET @co2 = (SELECT MAX(idOperatividad) FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas)
		SET @co2 = ISNULL(@co2,0) + 1 

		DECLARE @CondicionAnterior INT = (SELECT idCondicion FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas
		WHERE IdUnidad = (SELECT P.IdUnidad FROM @OP_PLACAS P WHERE P.Contador = @Nro) AND Fecha = (SELECT P.Fecha FROM @OP_PLACAS P WHERE P.Contador = @Nro))

		DELETE FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas
		WHERE IdUnidad = (SELECT P.IdUnidad FROM @OP_PLACAS P WHERE P.Contador = @Nro) AND Fecha = (SELECT P.Fecha FROM @OP_PLACAS P WHERE P.Contador = @Nro)

		INSERT INTO ReportesApp_Operaciones_Operatividad_RegistroCarretas(idOperatividad,IdUnidad,Fecha,idCondicion,IdOperacion,UsuarioRegistra,FechaRegistra)
		SELECT @co2, P.IdUnidad, P.Fecha, P.idCondicion, P.idOperacion, @Usuario, GETDATE()
		FROM @OP_PLACAS P WHERE P.Contador = @Nro

		SET @Operacion = (SELECT CASE WHEN P.idOperacion = 1 THEN 'TL'
									  WHEN P.idOperacion = 2 THEN 'ACL'
									  WHEN P.idOperacion = 3 THEN 'LG'
									  WHEN P.idOperacion = 4 THEN 'G'
									  WHEN P.idOperacion = 10 THEN 'V'
									  ELSE 'O' END
									  FROM @OP_PLACAS P WHERE P.Contador = @Nro)

		SET @Condicion = (SELECT C.Codigo FROM @OP_PLACAS P
						  LEFT JOIN ReportesApp_Operaciones_Operatividad_Condiciones C ON C.idCondicion = P.idCondicion
						  WHERE P.Contador = @Nro)

		IF (@idCondicion IN (1,2,3,4,16)) BEGIN
			SET @CondicionCompleta = @Condicion
		END
		ELSE BEGIN
			SET @CondicionCompleta = @Condicion + ' (' + @Operacion + ')'
		END

		DECLARE @x_IdUnidad INT, @x_idCondicion INT

		SET @x_IdUnidad = (SELECT IdUnidad FROM @OP_PLACAS WHERE Contador = @Nro)
		SET @x_idCondicion = (SELECT idCondicion FROM @OP_PLACAS WHERE Contador = @Nro)

		IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_Operatividad_Permisos WHERE idCondicion = @x_idCondicion AND Usuario = @Usuario AND Activo = 1)) BEGIN
			DECLARE @x_FechaSelec DATE, @x_DiaSelec INT

			SET @x_FechaSelec = (SELECT Fecha FROM @OP_PLACAS WHERE Contador = @Nro)
			SET @x_DiaSelec = (SELECT DAY(Fecha) FROM @OP_PLACAS WHERE Contador = @Nro)
			SET @CantidadCondiciones = (SELECT COUNT(*) FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas
										WHERE Fecha = @x_FechaSelec AND idCondicion = @x_idCondicion)

			DECLARE @TipoUnidad VARCHAR(150) = (SELECT LTRIM(RTRIM(SV.Descripcion)) FROM OP_TR_VEHICULO VH
												LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = VH.SubTipoVehiculo
												WHERE VH.IdVehiculo = @x_IdUnidad)
			
			UPDATE P 
			SET P.D1 = CASE WHEN DAY(O.Fecha) = 1 THEN @CondicionCompleta ELSE P.D1 END,
				P.D2 = CASE WHEN DAY(O.Fecha) = 2 THEN @CondicionCompleta ELSE P.D2 END,
				P.D3 = CASE WHEN DAY(O.Fecha) = 3 THEN @CondicionCompleta ELSE P.D3 END,
				P.D4 = CASE WHEN DAY(O.Fecha) = 4 THEN @CondicionCompleta ELSE P.D4 END,
				P.D5 = CASE WHEN DAY(O.Fecha) = 5 THEN @CondicionCompleta ELSE P.D5 END,
				P.D6 = CASE WHEN DAY(O.Fecha) = 6 THEN @CondicionCompleta ELSE P.D6 END,
				P.D7 = CASE WHEN DAY(O.Fecha) = 7 THEN @CondicionCompleta ELSE P.D7 END,
				P.D8 = CASE WHEN DAY(O.Fecha) = 8 THEN @CondicionCompleta ELSE P.D8 END,
				P.D9 = CASE WHEN DAY(O.Fecha) = 9 THEN @CondicionCompleta ELSE P.D9 END,
				P.D10 = CASE WHEN DAY(O.Fecha) = 10 THEN @CondicionCompleta ELSE P.D10 END,
				P.D11 = CASE WHEN DAY(O.Fecha) = 11 THEN @CondicionCompleta ELSE P.D11 END,
				P.D12 = CASE WHEN DAY(O.Fecha) = 12 THEN @CondicionCompleta ELSE P.D12 END,
				P.D13 = CASE WHEN DAY(O.Fecha) = 13 THEN @CondicionCompleta ELSE P.D13 END,
				P.D14 = CASE WHEN DAY(O.Fecha) = 14 THEN @CondicionCompleta ELSE P.D14 END,
				P.D15 = CASE WHEN DAY(O.Fecha) = 15 THEN @CondicionCompleta ELSE P.D15 END,
				P.D16 = CASE WHEN DAY(O.Fecha) = 16 THEN @CondicionCompleta ELSE P.D16 END,
				P.D17 = CASE WHEN DAY(O.Fecha) = 17 THEN @CondicionCompleta ELSE P.D17 END,
				P.D18 = CASE WHEN DAY(O.Fecha) = 18 THEN @CondicionCompleta ELSE P.D18 END,
				P.D19 = CASE WHEN DAY(O.Fecha) = 19 THEN @CondicionCompleta ELSE P.D19 END,
				P.D20 = CASE WHEN DAY(O.Fecha) = 20 THEN @CondicionCompleta ELSE P.D20 END,
				P.D21 = CASE WHEN DAY(O.Fecha) = 21 THEN @CondicionCompleta ELSE P.D21 END,
				P.D22 = CASE WHEN DAY(O.Fecha) = 22 THEN @CondicionCompleta ELSE P.D22 END,
				P.D23 = CASE WHEN DAY(O.Fecha) = 23 THEN @CondicionCompleta ELSE P.D23 END,
				P.D24 = CASE WHEN DAY(O.Fecha) = 24 THEN @CondicionCompleta ELSE P.D24 END,
				P.D25 = CASE WHEN DAY(O.Fecha) = 25 THEN @CondicionCompleta ELSE P.D25 END,
				P.D26 = CASE WHEN DAY(O.Fecha) = 26 THEN @CondicionCompleta ELSE P.D26 END,
				P.D27 = CASE WHEN DAY(O.Fecha) = 27 THEN @CondicionCompleta ELSE P.D27 END,
				P.D28 = CASE WHEN DAY(O.Fecha) = 28 THEN @CondicionCompleta ELSE P.D28 END,
				P.D29 = CASE WHEN DAY(O.Fecha) = 29 THEN @CondicionCompleta ELSE P.D29 END,
				P.D30 = CASE WHEN DAY(O.Fecha) = 30 THEN @CondicionCompleta ELSE P.D30 END,
				P.D31 = CASE WHEN DAY(O.Fecha) = 31 THEN @CondicionCompleta ELSE P.D31 END
			FROM ReportesApp_Operaciones_Operatividad_CarretasView P
			LEFT JOIN @OP_PLACAS O ON P.IdUnidad = O.IdUnidad AND O.Fecha = @x_FechaSelec
			WHERE P.Anio = RIGHT(@Periodo,4) AND P.Mes = LEFT(@periodo,2)

			UPDATE T 
			SET T.D1 = CASE WHEN DAY(O.Fecha) = 1 THEN (CASE WHEN T.D1 - 1 = 0 THEN NULL ELSE T.D1 - 1 END) ELSE T.D1 END,
				T.D2 = CASE WHEN DAY(O.Fecha) = 2 THEN (CASE WHEN T.D2 - 1 = 0 THEN NULL ELSE T.D2 - 1 END) ELSE T.D2 END,
				T.D3 = CASE WHEN DAY(O.Fecha) = 3 THEN (CASE WHEN T.D3 - 1 = 0 THEN NULL ELSE T.D3 - 1 END) ELSE T.D3 END,
				T.D4 = CASE WHEN DAY(O.Fecha) = 4 THEN (CASE WHEN T.D4 - 1 = 0 THEN NULL ELSE T.D4 - 1 END) ELSE T.D4 END,
				T.D5 = CASE WHEN DAY(O.Fecha) = 5 THEN (CASE WHEN T.D5 - 1 = 0 THEN NULL ELSE T.D5 - 1 END) ELSE T.D5 END,
				T.D6 = CASE WHEN DAY(O.Fecha) = 6 THEN (CASE WHEN T.D6 - 1 = 0 THEN NULL ELSE T.D6 - 1 END) ELSE T.D6 END,
				T.D7 = CASE WHEN DAY(O.Fecha) = 7 THEN (CASE WHEN T.D7 - 1 = 0 THEN NULL ELSE T.D7 - 1 END) ELSE T.D7 END,
				T.D8 = CASE WHEN DAY(O.Fecha) = 8 THEN (CASE WHEN T.D8 - 1 = 0 THEN NULL ELSE T.D8 - 1 END) ELSE T.D8 END,
				T.D9 = CASE WHEN DAY(O.Fecha) = 9 THEN (CASE WHEN T.D9 - 1 = 0 THEN NULL ELSE T.D9 - 1 END) ELSE T.D9 END,
				T.D10 = CASE WHEN DAY(O.Fecha) = 10 THEN (CASE WHEN T.D10 - 1 = 0 THEN NULL ELSE T.D10 - 1 END) ELSE T.D10 END,
				T.D11 = CASE WHEN DAY(O.Fecha) = 11 THEN (CASE WHEN T.D11 - 1 = 0 THEN NULL ELSE T.D11 - 1 END) ELSE T.D11 END,
				T.D12 = CASE WHEN DAY(O.Fecha) = 12 THEN (CASE WHEN T.D12 - 1 = 0 THEN NULL ELSE T.D12 - 1 END) ELSE T.D12 END,
				T.D13 = CASE WHEN DAY(O.Fecha) = 13 THEN (CASE WHEN T.D13 - 1 = 0 THEN NULL ELSE T.D13 - 1 END) ELSE T.D13 END,
				T.D14 = CASE WHEN DAY(O.Fecha) = 14 THEN (CASE WHEN T.D14 - 1 = 0 THEN NULL ELSE T.D14 - 1 END) ELSE T.D14 END,
				T.D15 = CASE WHEN DAY(O.Fecha) = 15 THEN (CASE WHEN T.D15 - 1 = 0 THEN NULL ELSE T.D15 - 1 END) ELSE T.D15 END,
				T.D16 = CASE WHEN DAY(O.Fecha) = 16 THEN (CASE WHEN T.D16 - 1 = 0 THEN NULL ELSE T.D16 - 1 END) ELSE T.D16 END,
				T.D17 = CASE WHEN DAY(O.Fecha) = 17 THEN (CASE WHEN T.D17 - 1 = 0 THEN NULL ELSE T.D17 - 1 END) ELSE T.D17 END,
				T.D18 = CASE WHEN DAY(O.Fecha) = 18 THEN (CASE WHEN T.D18 - 1 = 0 THEN NULL ELSE T.D18 - 1 END) ELSE T.D18 END,
				T.D19 = CASE WHEN DAY(O.Fecha) = 19 THEN (CASE WHEN T.D19 - 1 = 0 THEN NULL ELSE T.D19 - 1 END) ELSE T.D19 END,
				T.D20 = CASE WHEN DAY(O.Fecha) = 20 THEN (CASE WHEN T.D20 - 1 = 0 THEN NULL ELSE T.D20 - 1 END) ELSE T.D20 END,
				T.D21 = CASE WHEN DAY(O.Fecha) = 21 THEN (CASE WHEN T.D21 - 1 = 0 THEN NULL ELSE T.D21 - 1 END) ELSE T.D21 END,
				T.D22 = CASE WHEN DAY(O.Fecha) = 22 THEN (CASE WHEN T.D22 - 1 = 0 THEN NULL ELSE T.D22 - 1 END) ELSE T.D22 END,
				T.D23 = CASE WHEN DAY(O.Fecha) = 23 THEN (CASE WHEN T.D23 - 1 = 0 THEN NULL ELSE T.D23 - 1 END) ELSE T.D23 END,
				T.D24 = CASE WHEN DAY(O.Fecha) = 24 THEN (CASE WHEN T.D24 - 1 = 0 THEN NULL ELSE T.D24 - 1 END) ELSE T.D24 END,
				T.D25 = CASE WHEN DAY(O.Fecha) = 25 THEN (CASE WHEN T.D25 - 1 = 0 THEN NULL ELSE T.D25 - 1 END) ELSE T.D25 END,
				T.D26 = CASE WHEN DAY(O.Fecha) = 26 THEN (CASE WHEN T.D26 - 1 = 0 THEN NULL ELSE T.D26 - 1 END) ELSE T.D26 END,
				T.D27 = CASE WHEN DAY(O.Fecha) = 27 THEN (CASE WHEN T.D27 - 1 = 0 THEN NULL ELSE T.D27 - 1 END) ELSE T.D27 END,
				T.D28 = CASE WHEN DAY(O.Fecha) = 28 THEN (CASE WHEN T.D28 - 1 = 0 THEN NULL ELSE T.D28 - 1 END) ELSE T.D28 END,
				T.D29 = CASE WHEN DAY(O.Fecha) = 29 THEN (CASE WHEN T.D29 - 1 = 0 THEN NULL ELSE T.D29 - 1 END) ELSE T.D29 END,
				T.D30 = CASE WHEN DAY(O.Fecha) = 30 THEN (CASE WHEN T.D30 - 1 = 0 THEN NULL ELSE T.D30 - 1 END) ELSE T.D30 END,
				T.D31 = CASE WHEN DAY(O.Fecha) = 31 THEN (CASE WHEN T.D31 - 1 = 0 THEN NULL ELSE T.D31 - 1 END) ELSE T.D31 END
			FROM ReportesApp_Operaciones_Operatividad_ViewCarretas T
			LEFT JOIN @OP_PLACAS O ON T.idCondicion = @CondicionAnterior AND O.Fecha = @x_FechaSelec
			WHERE T.Anio = RIGHT(@Periodo,4) AND T.Mes = LEFT(@periodo,2)

			UPDATE T 
			SET T.D1 = CASE WHEN DAY(O.Fecha) = 1 THEN @CantidadCondiciones ELSE T.D1 END,
				T.D2 = CASE WHEN DAY(O.Fecha) = 2 THEN @CantidadCondiciones ELSE T.D2 END,
				T.D3 = CASE WHEN DAY(O.Fecha) = 3 THEN @CantidadCondiciones ELSE T.D3 END,
				T.D4 = CASE WHEN DAY(O.Fecha) = 4 THEN @CantidadCondiciones ELSE T.D4 END,
				T.D5 = CASE WHEN DAY(O.Fecha) = 5 THEN @CantidadCondiciones ELSE T.D5 END,
				T.D6 = CASE WHEN DAY(O.Fecha) = 6 THEN @CantidadCondiciones ELSE T.D6 END,
				T.D7 = CASE WHEN DAY(O.Fecha) = 7 THEN @CantidadCondiciones ELSE T.D7 END,
				T.D8 = CASE WHEN DAY(O.Fecha) = 8 THEN @CantidadCondiciones ELSE T.D8 END,
				T.D9 = CASE WHEN DAY(O.Fecha) = 9 THEN @CantidadCondiciones ELSE T.D9 END,
				T.D10 = CASE WHEN DAY(O.Fecha) = 10 THEN @CantidadCondiciones ELSE T.D10 END,
				T.D11 = CASE WHEN DAY(O.Fecha) = 11 THEN @CantidadCondiciones ELSE T.D11 END,
				T.D12 = CASE WHEN DAY(O.Fecha) = 12 THEN @CantidadCondiciones ELSE T.D12 END,
				T.D13 = CASE WHEN DAY(O.Fecha) = 13 THEN @CantidadCondiciones ELSE T.D13 END,
				T.D14 = CASE WHEN DAY(O.Fecha) = 14 THEN @CantidadCondiciones ELSE T.D14 END,
				T.D15 = CASE WHEN DAY(O.Fecha) = 15 THEN @CantidadCondiciones ELSE T.D15 END,
				T.D16 = CASE WHEN DAY(O.Fecha) = 16 THEN @CantidadCondiciones ELSE T.D16 END,
				T.D17 = CASE WHEN DAY(O.Fecha) = 17 THEN @CantidadCondiciones ELSE T.D17 END,
				T.D18 = CASE WHEN DAY(O.Fecha) = 18 THEN @CantidadCondiciones ELSE T.D18 END,
				T.D19 = CASE WHEN DAY(O.Fecha) = 19 THEN @CantidadCondiciones ELSE T.D19 END,
				T.D20 = CASE WHEN DAY(O.Fecha) = 20 THEN @CantidadCondiciones ELSE T.D20 END,
				T.D21 = CASE WHEN DAY(O.Fecha) = 21 THEN @CantidadCondiciones ELSE T.D21 END,
				T.D22 = CASE WHEN DAY(O.Fecha) = 22 THEN @CantidadCondiciones ELSE T.D22 END,
				T.D23 = CASE WHEN DAY(O.Fecha) = 23 THEN @CantidadCondiciones ELSE T.D23 END,
				T.D24 = CASE WHEN DAY(O.Fecha) = 24 THEN @CantidadCondiciones ELSE T.D24 END,
				T.D25 = CASE WHEN DAY(O.Fecha) = 25 THEN @CantidadCondiciones ELSE T.D25 END,
				T.D26 = CASE WHEN DAY(O.Fecha) = 26 THEN @CantidadCondiciones ELSE T.D26 END,
				T.D27 = CASE WHEN DAY(O.Fecha) = 27 THEN @CantidadCondiciones ELSE T.D27 END,
				T.D28 = CASE WHEN DAY(O.Fecha) = 28 THEN @CantidadCondiciones ELSE T.D28 END,
				T.D29 = CASE WHEN DAY(O.Fecha) = 29 THEN @CantidadCondiciones ELSE T.D29 END,
				T.D30 = CASE WHEN DAY(O.Fecha) = 30 THEN @CantidadCondiciones ELSE T.D30 END,
				T.D31 = CASE WHEN DAY(O.Fecha) = 31 THEN @CantidadCondiciones ELSE T.D31 END
			FROM ReportesApp_Operaciones_Operatividad_ViewCarretas T
			LEFT JOIN @OP_PLACAS O ON T.idCondicion = O.idCondicion AND O.Fecha = @x_FechaSelec
			WHERE T.Anio = RIGHT(@Periodo,4) AND T.Mes = LEFT(@periodo,2)

			IF (@TipoUnidad = 'CORTINERA' AND (@idCondicion IN (1,2,3,4,16,5,6,13,10,8) OR @CondicionAnterior IN (1,2,3,4,16,5,6,13,10,8))) BEGIN
				SET @CantidadCondiciones = (SELECT COUNT(*) FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas C
											LEFT JOIN OP_TR_VEHICULO VH ON C.IdUnidad = VH.IdVehiculo
											LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = VH.SubTipoVehiculo
											WHERE C.Fecha = @x_FechaSelec AND C.idCondicion = @x_idCondicion AND LTRIM(RTRIM(SV.Descripcion)) = @TipoUnidad)

				UPDATE T 
				SET T.D1 = CASE WHEN DAY(O.Fecha) = 1 THEN (CASE WHEN T.D1 - 1 = 0 THEN NULL ELSE T.D1 - 1 END) ELSE T.D1 END,
					T.D2 = CASE WHEN DAY(O.Fecha) = 2 THEN (CASE WHEN T.D2 - 1 = 0 THEN NULL ELSE T.D2 - 1 END) ELSE T.D2 END,
					T.D3 = CASE WHEN DAY(O.Fecha) = 3 THEN (CASE WHEN T.D3 - 1 = 0 THEN NULL ELSE T.D3 - 1 END) ELSE T.D3 END,
					T.D4 = CASE WHEN DAY(O.Fecha) = 4 THEN (CASE WHEN T.D4 - 1 = 0 THEN NULL ELSE T.D4 - 1 END) ELSE T.D4 END,
					T.D5 = CASE WHEN DAY(O.Fecha) = 5 THEN (CASE WHEN T.D5 - 1 = 0 THEN NULL ELSE T.D5 - 1 END) ELSE T.D5 END,
					T.D6 = CASE WHEN DAY(O.Fecha) = 6 THEN (CASE WHEN T.D6 - 1 = 0 THEN NULL ELSE T.D6 - 1 END) ELSE T.D6 END,
					T.D7 = CASE WHEN DAY(O.Fecha) = 7 THEN (CASE WHEN T.D7 - 1 = 0 THEN NULL ELSE T.D7 - 1 END) ELSE T.D7 END,
					T.D8 = CASE WHEN DAY(O.Fecha) = 8 THEN (CASE WHEN T.D8 - 1 = 0 THEN NULL ELSE T.D8 - 1 END) ELSE T.D8 END,
					T.D9 = CASE WHEN DAY(O.Fecha) = 9 THEN (CASE WHEN T.D9 - 1 = 0 THEN NULL ELSE T.D9 - 1 END) ELSE T.D9 END,
					T.D10 = CASE WHEN DAY(O.Fecha) = 10 THEN (CASE WHEN T.D10 - 1 = 0 THEN NULL ELSE T.D10 - 1 END) ELSE T.D10 END,
					T.D11 = CASE WHEN DAY(O.Fecha) = 11 THEN (CASE WHEN T.D11 - 1 = 0 THEN NULL ELSE T.D11 - 1 END) ELSE T.D11 END,
					T.D12 = CASE WHEN DAY(O.Fecha) = 12 THEN (CASE WHEN T.D12 - 1 = 0 THEN NULL ELSE T.D12 - 1 END) ELSE T.D12 END,
					T.D13 = CASE WHEN DAY(O.Fecha) = 13 THEN (CASE WHEN T.D13 - 1 = 0 THEN NULL ELSE T.D13 - 1 END) ELSE T.D13 END,
					T.D14 = CASE WHEN DAY(O.Fecha) = 14 THEN (CASE WHEN T.D14 - 1 = 0 THEN NULL ELSE T.D14 - 1 END) ELSE T.D14 END,
					T.D15 = CASE WHEN DAY(O.Fecha) = 15 THEN (CASE WHEN T.D15 - 1 = 0 THEN NULL ELSE T.D15 - 1 END) ELSE T.D15 END,
					T.D16 = CASE WHEN DAY(O.Fecha) = 16 THEN (CASE WHEN T.D16 - 1 = 0 THEN NULL ELSE T.D16 - 1 END) ELSE T.D16 END,
					T.D17 = CASE WHEN DAY(O.Fecha) = 17 THEN (CASE WHEN T.D17 - 1 = 0 THEN NULL ELSE T.D17 - 1 END) ELSE T.D17 END,
					T.D18 = CASE WHEN DAY(O.Fecha) = 18 THEN (CASE WHEN T.D18 - 1 = 0 THEN NULL ELSE T.D18 - 1 END) ELSE T.D18 END,
					T.D19 = CASE WHEN DAY(O.Fecha) = 19 THEN (CASE WHEN T.D19 - 1 = 0 THEN NULL ELSE T.D19 - 1 END) ELSE T.D19 END,
					T.D20 = CASE WHEN DAY(O.Fecha) = 20 THEN (CASE WHEN T.D20 - 1 = 0 THEN NULL ELSE T.D20 - 1 END) ELSE T.D20 END,
					T.D21 = CASE WHEN DAY(O.Fecha) = 21 THEN (CASE WHEN T.D21 - 1 = 0 THEN NULL ELSE T.D21 - 1 END) ELSE T.D21 END,
					T.D22 = CASE WHEN DAY(O.Fecha) = 22 THEN (CASE WHEN T.D22 - 1 = 0 THEN NULL ELSE T.D22 - 1 END) ELSE T.D22 END,
					T.D23 = CASE WHEN DAY(O.Fecha) = 23 THEN (CASE WHEN T.D23 - 1 = 0 THEN NULL ELSE T.D23 - 1 END) ELSE T.D23 END,
					T.D24 = CASE WHEN DAY(O.Fecha) = 24 THEN (CASE WHEN T.D24 - 1 = 0 THEN NULL ELSE T.D24 - 1 END) ELSE T.D24 END,
					T.D25 = CASE WHEN DAY(O.Fecha) = 25 THEN (CASE WHEN T.D25 - 1 = 0 THEN NULL ELSE T.D25 - 1 END) ELSE T.D25 END,
					T.D26 = CASE WHEN DAY(O.Fecha) = 26 THEN (CASE WHEN T.D26 - 1 = 0 THEN NULL ELSE T.D26 - 1 END) ELSE T.D26 END,
					T.D27 = CASE WHEN DAY(O.Fecha) = 27 THEN (CASE WHEN T.D27 - 1 = 0 THEN NULL ELSE T.D27 - 1 END) ELSE T.D27 END,
					T.D28 = CASE WHEN DAY(O.Fecha) = 28 THEN (CASE WHEN T.D28 - 1 = 0 THEN NULL ELSE T.D28 - 1 END) ELSE T.D28 END,
					T.D29 = CASE WHEN DAY(O.Fecha) = 29 THEN (CASE WHEN T.D29 - 1 = 0 THEN NULL ELSE T.D29 - 1 END) ELSE T.D29 END,
					T.D30 = CASE WHEN DAY(O.Fecha) = 30 THEN (CASE WHEN T.D30 - 1 = 0 THEN NULL ELSE T.D30 - 1 END) ELSE T.D30 END,
					T.D31 = CASE WHEN DAY(O.Fecha) = 31 THEN (CASE WHEN T.D31 - 1 = 0 THEN NULL ELSE T.D31 - 1 END) ELSE T.D31 END
				FROM ReportesApp_Operaciones_Operatividad_ViewCortineras T
				LEFT JOIN @OP_PLACAS O ON T.idCondicionCR = @CondicionAnterior AND O.Fecha = @x_FechaSelec
				WHERE T.Anio = RIGHT(@Periodo,4) AND T.Mes = LEFT(@periodo,2)

				UPDATE T 
				SET T.D1 = CASE WHEN DAY(O.Fecha) = 1 THEN @CantidadCondiciones ELSE T.D1 END,
					T.D2 = CASE WHEN DAY(O.Fecha) = 2 THEN @CantidadCondiciones ELSE T.D2 END,
					T.D3 = CASE WHEN DAY(O.Fecha) = 3 THEN @CantidadCondiciones ELSE T.D3 END,
					T.D4 = CASE WHEN DAY(O.Fecha) = 4 THEN @CantidadCondiciones ELSE T.D4 END,
					T.D5 = CASE WHEN DAY(O.Fecha) = 5 THEN @CantidadCondiciones ELSE T.D5 END,
					T.D6 = CASE WHEN DAY(O.Fecha) = 6 THEN @CantidadCondiciones ELSE T.D6 END,
					T.D7 = CASE WHEN DAY(O.Fecha) = 7 THEN @CantidadCondiciones ELSE T.D7 END,
					T.D8 = CASE WHEN DAY(O.Fecha) = 8 THEN @CantidadCondiciones ELSE T.D8 END,
					T.D9 = CASE WHEN DAY(O.Fecha) = 9 THEN @CantidadCondiciones ELSE T.D9 END,
					T.D10 = CASE WHEN DAY(O.Fecha) = 10 THEN @CantidadCondiciones ELSE T.D10 END,
					T.D11 = CASE WHEN DAY(O.Fecha) = 11 THEN @CantidadCondiciones ELSE T.D11 END,
					T.D12 = CASE WHEN DAY(O.Fecha) = 12 THEN @CantidadCondiciones ELSE T.D12 END,
					T.D13 = CASE WHEN DAY(O.Fecha) = 13 THEN @CantidadCondiciones ELSE T.D13 END,
					T.D14 = CASE WHEN DAY(O.Fecha) = 14 THEN @CantidadCondiciones ELSE T.D14 END,
					T.D15 = CASE WHEN DAY(O.Fecha) = 15 THEN @CantidadCondiciones ELSE T.D15 END,
					T.D16 = CASE WHEN DAY(O.Fecha) = 16 THEN @CantidadCondiciones ELSE T.D16 END,
					T.D17 = CASE WHEN DAY(O.Fecha) = 17 THEN @CantidadCondiciones ELSE T.D17 END,
					T.D18 = CASE WHEN DAY(O.Fecha) = 18 THEN @CantidadCondiciones ELSE T.D18 END,
					T.D19 = CASE WHEN DAY(O.Fecha) = 19 THEN @CantidadCondiciones ELSE T.D19 END,
					T.D20 = CASE WHEN DAY(O.Fecha) = 20 THEN @CantidadCondiciones ELSE T.D20 END,
					T.D21 = CASE WHEN DAY(O.Fecha) = 21 THEN @CantidadCondiciones ELSE T.D21 END,
					T.D22 = CASE WHEN DAY(O.Fecha) = 22 THEN @CantidadCondiciones ELSE T.D22 END,
					T.D23 = CASE WHEN DAY(O.Fecha) = 23 THEN @CantidadCondiciones ELSE T.D23 END,
					T.D24 = CASE WHEN DAY(O.Fecha) = 24 THEN @CantidadCondiciones ELSE T.D24 END,
					T.D25 = CASE WHEN DAY(O.Fecha) = 25 THEN @CantidadCondiciones ELSE T.D25 END,
					T.D26 = CASE WHEN DAY(O.Fecha) = 26 THEN @CantidadCondiciones ELSE T.D26 END,
					T.D27 = CASE WHEN DAY(O.Fecha) = 27 THEN @CantidadCondiciones ELSE T.D27 END,
					T.D28 = CASE WHEN DAY(O.Fecha) = 28 THEN @CantidadCondiciones ELSE T.D28 END,
					T.D29 = CASE WHEN DAY(O.Fecha) = 29 THEN @CantidadCondiciones ELSE T.D29 END,
					T.D30 = CASE WHEN DAY(O.Fecha) = 30 THEN @CantidadCondiciones ELSE T.D30 END,
					T.D31 = CASE WHEN DAY(O.Fecha) = 31 THEN @CantidadCondiciones ELSE T.D31 END
				FROM ReportesApp_Operaciones_Operatividad_ViewCortineras T
				LEFT JOIN @OP_PLACAS O ON T.idCondicionCR = O.idCondicion AND O.Fecha = @x_FechaSelec
				WHERE T.Anio = RIGHT(@Periodo,4) AND T.Mes = LEFT(@periodo,2)
			END

			IF (@TipoUnidad = 'PLATAFORMA' AND (@idCondicion IN (1,2,3,4,16,5,6,10,8) OR @CondicionAnterior IN (1,2,3,4,16,5,6,10,8))) BEGIN
				SET @CantidadCondiciones = (SELECT COUNT(*) FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas C
											LEFT JOIN OP_TR_VEHICULO VH ON C.IdUnidad = VH.IdVehiculo
											LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = VH.SubTipoVehiculo
											WHERE C.Fecha = @x_FechaSelec AND C.idCondicion = @x_idCondicion AND LTRIM(RTRIM(SV.Descripcion)) = @TipoUnidad)

				UPDATE T 
				SET T.D1 = CASE WHEN DAY(O.Fecha) = 1 THEN (CASE WHEN T.D1 - 1 = 0 THEN NULL ELSE T.D1 - 1 END) ELSE T.D1 END,
					T.D2 = CASE WHEN DAY(O.Fecha) = 2 THEN (CASE WHEN T.D2 - 1 = 0 THEN NULL ELSE T.D2 - 1 END) ELSE T.D2 END,
					T.D3 = CASE WHEN DAY(O.Fecha) = 3 THEN (CASE WHEN T.D3 - 1 = 0 THEN NULL ELSE T.D3 - 1 END) ELSE T.D3 END,
					T.D4 = CASE WHEN DAY(O.Fecha) = 4 THEN (CASE WHEN T.D4 - 1 = 0 THEN NULL ELSE T.D4 - 1 END) ELSE T.D4 END,
					T.D5 = CASE WHEN DAY(O.Fecha) = 5 THEN (CASE WHEN T.D5 - 1 = 0 THEN NULL ELSE T.D5 - 1 END) ELSE T.D5 END,
					T.D6 = CASE WHEN DAY(O.Fecha) = 6 THEN (CASE WHEN T.D6 - 1 = 0 THEN NULL ELSE T.D6 - 1 END) ELSE T.D6 END,
					T.D7 = CASE WHEN DAY(O.Fecha) = 7 THEN (CASE WHEN T.D7 - 1 = 0 THEN NULL ELSE T.D7 - 1 END) ELSE T.D7 END,
					T.D8 = CASE WHEN DAY(O.Fecha) = 8 THEN (CASE WHEN T.D8 - 1 = 0 THEN NULL ELSE T.D8 - 1 END) ELSE T.D8 END,
					T.D9 = CASE WHEN DAY(O.Fecha) = 9 THEN (CASE WHEN T.D9 - 1 = 0 THEN NULL ELSE T.D9 - 1 END) ELSE T.D9 END,
					T.D10 = CASE WHEN DAY(O.Fecha) = 10 THEN (CASE WHEN T.D10 - 1 = 0 THEN NULL ELSE T.D10 - 1 END) ELSE T.D10 END,
					T.D11 = CASE WHEN DAY(O.Fecha) = 11 THEN (CASE WHEN T.D11 - 1 = 0 THEN NULL ELSE T.D11 - 1 END) ELSE T.D11 END,
					T.D12 = CASE WHEN DAY(O.Fecha) = 12 THEN (CASE WHEN T.D12 - 1 = 0 THEN NULL ELSE T.D12 - 1 END) ELSE T.D12 END,
					T.D13 = CASE WHEN DAY(O.Fecha) = 13 THEN (CASE WHEN T.D13 - 1 = 0 THEN NULL ELSE T.D13 - 1 END) ELSE T.D13 END,
					T.D14 = CASE WHEN DAY(O.Fecha) = 14 THEN (CASE WHEN T.D14 - 1 = 0 THEN NULL ELSE T.D14 - 1 END) ELSE T.D14 END,
					T.D15 = CASE WHEN DAY(O.Fecha) = 15 THEN (CASE WHEN T.D15 - 1 = 0 THEN NULL ELSE T.D15 - 1 END) ELSE T.D15 END,
					T.D16 = CASE WHEN DAY(O.Fecha) = 16 THEN (CASE WHEN T.D16 - 1 = 0 THEN NULL ELSE T.D16 - 1 END) ELSE T.D16 END,
					T.D17 = CASE WHEN DAY(O.Fecha) = 17 THEN (CASE WHEN T.D17 - 1 = 0 THEN NULL ELSE T.D17 - 1 END) ELSE T.D17 END,
					T.D18 = CASE WHEN DAY(O.Fecha) = 18 THEN (CASE WHEN T.D18 - 1 = 0 THEN NULL ELSE T.D18 - 1 END) ELSE T.D18 END,
					T.D19 = CASE WHEN DAY(O.Fecha) = 19 THEN (CASE WHEN T.D19 - 1 = 0 THEN NULL ELSE T.D19 - 1 END) ELSE T.D19 END,
					T.D20 = CASE WHEN DAY(O.Fecha) = 20 THEN (CASE WHEN T.D20 - 1 = 0 THEN NULL ELSE T.D20 - 1 END) ELSE T.D20 END,
					T.D21 = CASE WHEN DAY(O.Fecha) = 21 THEN (CASE WHEN T.D21 - 1 = 0 THEN NULL ELSE T.D21 - 1 END) ELSE T.D21 END,
					T.D22 = CASE WHEN DAY(O.Fecha) = 22 THEN (CASE WHEN T.D22 - 1 = 0 THEN NULL ELSE T.D22 - 1 END) ELSE T.D22 END,
					T.D23 = CASE WHEN DAY(O.Fecha) = 23 THEN (CASE WHEN T.D23 - 1 = 0 THEN NULL ELSE T.D23 - 1 END) ELSE T.D23 END,
					T.D24 = CASE WHEN DAY(O.Fecha) = 24 THEN (CASE WHEN T.D24 - 1 = 0 THEN NULL ELSE T.D24 - 1 END) ELSE T.D24 END,
					T.D25 = CASE WHEN DAY(O.Fecha) = 25 THEN (CASE WHEN T.D25 - 1 = 0 THEN NULL ELSE T.D25 - 1 END) ELSE T.D25 END,
					T.D26 = CASE WHEN DAY(O.Fecha) = 26 THEN (CASE WHEN T.D26 - 1 = 0 THEN NULL ELSE T.D26 - 1 END) ELSE T.D26 END,
					T.D27 = CASE WHEN DAY(O.Fecha) = 27 THEN (CASE WHEN T.D27 - 1 = 0 THEN NULL ELSE T.D27 - 1 END) ELSE T.D27 END,
					T.D28 = CASE WHEN DAY(O.Fecha) = 28 THEN (CASE WHEN T.D28 - 1 = 0 THEN NULL ELSE T.D28 - 1 END) ELSE T.D28 END,
					T.D29 = CASE WHEN DAY(O.Fecha) = 29 THEN (CASE WHEN T.D29 - 1 = 0 THEN NULL ELSE T.D29 - 1 END) ELSE T.D29 END,
					T.D30 = CASE WHEN DAY(O.Fecha) = 30 THEN (CASE WHEN T.D30 - 1 = 0 THEN NULL ELSE T.D30 - 1 END) ELSE T.D30 END,
					T.D31 = CASE WHEN DAY(O.Fecha) = 31 THEN (CASE WHEN T.D31 - 1 = 0 THEN NULL ELSE T.D31 - 1 END) ELSE T.D31 END
				FROM ReportesApp_Operaciones_Operatividad_ViewPlataformas T
				LEFT JOIN @OP_PLACAS O ON T.idCondicionPL = @CondicionAnterior AND O.Fecha = @x_FechaSelec
				WHERE T.Anio = RIGHT(@Periodo,4) AND T.Mes = LEFT(@periodo,2)

				UPDATE T 
				SET T.D1 = CASE WHEN DAY(O.Fecha) = 1 THEN @CantidadCondiciones ELSE T.D1 END,
					T.D2 = CASE WHEN DAY(O.Fecha) = 2 THEN @CantidadCondiciones ELSE T.D2 END,
					T.D3 = CASE WHEN DAY(O.Fecha) = 3 THEN @CantidadCondiciones ELSE T.D3 END,
					T.D4 = CASE WHEN DAY(O.Fecha) = 4 THEN @CantidadCondiciones ELSE T.D4 END,
					T.D5 = CASE WHEN DAY(O.Fecha) = 5 THEN @CantidadCondiciones ELSE T.D5 END,
					T.D6 = CASE WHEN DAY(O.Fecha) = 6 THEN @CantidadCondiciones ELSE T.D6 END,
					T.D7 = CASE WHEN DAY(O.Fecha) = 7 THEN @CantidadCondiciones ELSE T.D7 END,
					T.D8 = CASE WHEN DAY(O.Fecha) = 8 THEN @CantidadCondiciones ELSE T.D8 END,
					T.D9 = CASE WHEN DAY(O.Fecha) = 9 THEN @CantidadCondiciones ELSE T.D9 END,
					T.D10 = CASE WHEN DAY(O.Fecha) = 10 THEN @CantidadCondiciones ELSE T.D10 END,
					T.D11 = CASE WHEN DAY(O.Fecha) = 11 THEN @CantidadCondiciones ELSE T.D11 END,
					T.D12 = CASE WHEN DAY(O.Fecha) = 12 THEN @CantidadCondiciones ELSE T.D12 END,
					T.D13 = CASE WHEN DAY(O.Fecha) = 13 THEN @CantidadCondiciones ELSE T.D13 END,
					T.D14 = CASE WHEN DAY(O.Fecha) = 14 THEN @CantidadCondiciones ELSE T.D14 END,
					T.D15 = CASE WHEN DAY(O.Fecha) = 15 THEN @CantidadCondiciones ELSE T.D15 END,
					T.D16 = CASE WHEN DAY(O.Fecha) = 16 THEN @CantidadCondiciones ELSE T.D16 END,
					T.D17 = CASE WHEN DAY(O.Fecha) = 17 THEN @CantidadCondiciones ELSE T.D17 END,
					T.D18 = CASE WHEN DAY(O.Fecha) = 18 THEN @CantidadCondiciones ELSE T.D18 END,
					T.D19 = CASE WHEN DAY(O.Fecha) = 19 THEN @CantidadCondiciones ELSE T.D19 END,
					T.D20 = CASE WHEN DAY(O.Fecha) = 20 THEN @CantidadCondiciones ELSE T.D20 END,
					T.D21 = CASE WHEN DAY(O.Fecha) = 21 THEN @CantidadCondiciones ELSE T.D21 END,
					T.D22 = CASE WHEN DAY(O.Fecha) = 22 THEN @CantidadCondiciones ELSE T.D22 END,
					T.D23 = CASE WHEN DAY(O.Fecha) = 23 THEN @CantidadCondiciones ELSE T.D23 END,
					T.D24 = CASE WHEN DAY(O.Fecha) = 24 THEN @CantidadCondiciones ELSE T.D24 END,
					T.D25 = CASE WHEN DAY(O.Fecha) = 25 THEN @CantidadCondiciones ELSE T.D25 END,
					T.D26 = CASE WHEN DAY(O.Fecha) = 26 THEN @CantidadCondiciones ELSE T.D26 END,
					T.D27 = CASE WHEN DAY(O.Fecha) = 27 THEN @CantidadCondiciones ELSE T.D27 END,
					T.D28 = CASE WHEN DAY(O.Fecha) = 28 THEN @CantidadCondiciones ELSE T.D28 END,
					T.D29 = CASE WHEN DAY(O.Fecha) = 29 THEN @CantidadCondiciones ELSE T.D29 END,
					T.D30 = CASE WHEN DAY(O.Fecha) = 30 THEN @CantidadCondiciones ELSE T.D30 END,
					T.D31 = CASE WHEN DAY(O.Fecha) = 31 THEN @CantidadCondiciones ELSE T.D31 END
				FROM ReportesApp_Operaciones_Operatividad_ViewPlataformas T
				LEFT JOIN @OP_PLACAS O ON T.idCondicionPL = O.idCondicion AND O.Fecha = @x_FechaSelec
				WHERE T.Anio = RIGHT(@Periodo,4) AND T.Mes = LEFT(@periodo,2)
			END

			IF (@TipoUnidad = 'TOLVA' AND (@idCondicion IN (1,2,3,4,16,5,6,10,8) OR @CondicionAnterior IN (1,2,3,4,16,5,6,10,8))) BEGIN
				SET @CantidadCondiciones = (SELECT COUNT(*) FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas C
											LEFT JOIN OP_TR_VEHICULO VH ON C.IdUnidad = VH.IdVehiculo
											LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = VH.SubTipoVehiculo
											WHERE C.Fecha = @x_FechaSelec AND C.idCondicion = @x_idCondicion AND LTRIM(RTRIM(SV.Descripcion)) = @TipoUnidad)

				UPDATE T 
				SET T.D1 = CASE WHEN DAY(O.Fecha) = 1 THEN (CASE WHEN T.D1 - 1 = 0 THEN NULL ELSE T.D1 - 1 END) ELSE T.D1 END,
					T.D2 = CASE WHEN DAY(O.Fecha) = 2 THEN (CASE WHEN T.D2 - 1 = 0 THEN NULL ELSE T.D2 - 1 END) ELSE T.D2 END,
					T.D3 = CASE WHEN DAY(O.Fecha) = 3 THEN (CASE WHEN T.D3 - 1 = 0 THEN NULL ELSE T.D3 - 1 END) ELSE T.D3 END,
					T.D4 = CASE WHEN DAY(O.Fecha) = 4 THEN (CASE WHEN T.D4 - 1 = 0 THEN NULL ELSE T.D4 - 1 END) ELSE T.D4 END,
					T.D5 = CASE WHEN DAY(O.Fecha) = 5 THEN (CASE WHEN T.D5 - 1 = 0 THEN NULL ELSE T.D5 - 1 END) ELSE T.D5 END,
					T.D6 = CASE WHEN DAY(O.Fecha) = 6 THEN (CASE WHEN T.D6 - 1 = 0 THEN NULL ELSE T.D6 - 1 END) ELSE T.D6 END,
					T.D7 = CASE WHEN DAY(O.Fecha) = 7 THEN (CASE WHEN T.D7 - 1 = 0 THEN NULL ELSE T.D7 - 1 END) ELSE T.D7 END,
					T.D8 = CASE WHEN DAY(O.Fecha) = 8 THEN (CASE WHEN T.D8 - 1 = 0 THEN NULL ELSE T.D8 - 1 END) ELSE T.D8 END,
					T.D9 = CASE WHEN DAY(O.Fecha) = 9 THEN (CASE WHEN T.D9 - 1 = 0 THEN NULL ELSE T.D9 - 1 END) ELSE T.D9 END,
					T.D10 = CASE WHEN DAY(O.Fecha) = 10 THEN (CASE WHEN T.D10 - 1 = 0 THEN NULL ELSE T.D10 - 1 END) ELSE T.D10 END,
					T.D11 = CASE WHEN DAY(O.Fecha) = 11 THEN (CASE WHEN T.D11 - 1 = 0 THEN NULL ELSE T.D11 - 1 END) ELSE T.D11 END,
					T.D12 = CASE WHEN DAY(O.Fecha) = 12 THEN (CASE WHEN T.D12 - 1 = 0 THEN NULL ELSE T.D12 - 1 END) ELSE T.D12 END,
					T.D13 = CASE WHEN DAY(O.Fecha) = 13 THEN (CASE WHEN T.D13 - 1 = 0 THEN NULL ELSE T.D13 - 1 END) ELSE T.D13 END,
					T.D14 = CASE WHEN DAY(O.Fecha) = 14 THEN (CASE WHEN T.D14 - 1 = 0 THEN NULL ELSE T.D14 - 1 END) ELSE T.D14 END,
					T.D15 = CASE WHEN DAY(O.Fecha) = 15 THEN (CASE WHEN T.D15 - 1 = 0 THEN NULL ELSE T.D15 - 1 END) ELSE T.D15 END,
					T.D16 = CASE WHEN DAY(O.Fecha) = 16 THEN (CASE WHEN T.D16 - 1 = 0 THEN NULL ELSE T.D16 - 1 END) ELSE T.D16 END,
					T.D17 = CASE WHEN DAY(O.Fecha) = 17 THEN (CASE WHEN T.D17 - 1 = 0 THEN NULL ELSE T.D17 - 1 END) ELSE T.D17 END,
					T.D18 = CASE WHEN DAY(O.Fecha) = 18 THEN (CASE WHEN T.D18 - 1 = 0 THEN NULL ELSE T.D18 - 1 END) ELSE T.D18 END,
					T.D19 = CASE WHEN DAY(O.Fecha) = 19 THEN (CASE WHEN T.D19 - 1 = 0 THEN NULL ELSE T.D19 - 1 END) ELSE T.D19 END,
					T.D20 = CASE WHEN DAY(O.Fecha) = 20 THEN (CASE WHEN T.D20 - 1 = 0 THEN NULL ELSE T.D20 - 1 END) ELSE T.D20 END,
					T.D21 = CASE WHEN DAY(O.Fecha) = 21 THEN (CASE WHEN T.D21 - 1 = 0 THEN NULL ELSE T.D21 - 1 END) ELSE T.D21 END,
					T.D22 = CASE WHEN DAY(O.Fecha) = 22 THEN (CASE WHEN T.D22 - 1 = 0 THEN NULL ELSE T.D22 - 1 END) ELSE T.D22 END,
					T.D23 = CASE WHEN DAY(O.Fecha) = 23 THEN (CASE WHEN T.D23 - 1 = 0 THEN NULL ELSE T.D23 - 1 END) ELSE T.D23 END,
					T.D24 = CASE WHEN DAY(O.Fecha) = 24 THEN (CASE WHEN T.D24 - 1 = 0 THEN NULL ELSE T.D24 - 1 END) ELSE T.D24 END,
					T.D25 = CASE WHEN DAY(O.Fecha) = 25 THEN (CASE WHEN T.D25 - 1 = 0 THEN NULL ELSE T.D25 - 1 END) ELSE T.D25 END,
					T.D26 = CASE WHEN DAY(O.Fecha) = 26 THEN (CASE WHEN T.D26 - 1 = 0 THEN NULL ELSE T.D26 - 1 END) ELSE T.D26 END,
					T.D27 = CASE WHEN DAY(O.Fecha) = 27 THEN (CASE WHEN T.D27 - 1 = 0 THEN NULL ELSE T.D27 - 1 END) ELSE T.D27 END,
					T.D28 = CASE WHEN DAY(O.Fecha) = 28 THEN (CASE WHEN T.D28 - 1 = 0 THEN NULL ELSE T.D28 - 1 END) ELSE T.D28 END,
					T.D29 = CASE WHEN DAY(O.Fecha) = 29 THEN (CASE WHEN T.D29 - 1 = 0 THEN NULL ELSE T.D29 - 1 END) ELSE T.D29 END,
					T.D30 = CASE WHEN DAY(O.Fecha) = 30 THEN (CASE WHEN T.D30 - 1 = 0 THEN NULL ELSE T.D30 - 1 END) ELSE T.D30 END,
					T.D31 = CASE WHEN DAY(O.Fecha) = 31 THEN (CASE WHEN T.D31 - 1 = 0 THEN NULL ELSE T.D31 - 1 END) ELSE T.D31 END
				FROM ReportesApp_Operaciones_Operatividad_ViewTolvas T
				LEFT JOIN @OP_PLACAS O ON T.idCondicionTL = @CondicionAnterior AND O.Fecha = @x_FechaSelec
				WHERE T.Anio = RIGHT(@Periodo,4) AND T.Mes = LEFT(@periodo,2)

				UPDATE T 
				SET T.D1 = CASE WHEN DAY(O.Fecha) = 1 THEN @CantidadCondiciones ELSE T.D1 END,
					T.D2 = CASE WHEN DAY(O.Fecha) = 2 THEN @CantidadCondiciones ELSE T.D2 END,
					T.D3 = CASE WHEN DAY(O.Fecha) = 3 THEN @CantidadCondiciones ELSE T.D3 END,
					T.D4 = CASE WHEN DAY(O.Fecha) = 4 THEN @CantidadCondiciones ELSE T.D4 END,
					T.D5 = CASE WHEN DAY(O.Fecha) = 5 THEN @CantidadCondiciones ELSE T.D5 END,
					T.D6 = CASE WHEN DAY(O.Fecha) = 6 THEN @CantidadCondiciones ELSE T.D6 END,
					T.D7 = CASE WHEN DAY(O.Fecha) = 7 THEN @CantidadCondiciones ELSE T.D7 END,
					T.D8 = CASE WHEN DAY(O.Fecha) = 8 THEN @CantidadCondiciones ELSE T.D8 END,
					T.D9 = CASE WHEN DAY(O.Fecha) = 9 THEN @CantidadCondiciones ELSE T.D9 END,
					T.D10 = CASE WHEN DAY(O.Fecha) = 10 THEN @CantidadCondiciones ELSE T.D10 END,
					T.D11 = CASE WHEN DAY(O.Fecha) = 11 THEN @CantidadCondiciones ELSE T.D11 END,
					T.D12 = CASE WHEN DAY(O.Fecha) = 12 THEN @CantidadCondiciones ELSE T.D12 END,
					T.D13 = CASE WHEN DAY(O.Fecha) = 13 THEN @CantidadCondiciones ELSE T.D13 END,
					T.D14 = CASE WHEN DAY(O.Fecha) = 14 THEN @CantidadCondiciones ELSE T.D14 END,
					T.D15 = CASE WHEN DAY(O.Fecha) = 15 THEN @CantidadCondiciones ELSE T.D15 END,
					T.D16 = CASE WHEN DAY(O.Fecha) = 16 THEN @CantidadCondiciones ELSE T.D16 END,
					T.D17 = CASE WHEN DAY(O.Fecha) = 17 THEN @CantidadCondiciones ELSE T.D17 END,
					T.D18 = CASE WHEN DAY(O.Fecha) = 18 THEN @CantidadCondiciones ELSE T.D18 END,
					T.D19 = CASE WHEN DAY(O.Fecha) = 19 THEN @CantidadCondiciones ELSE T.D19 END,
					T.D20 = CASE WHEN DAY(O.Fecha) = 20 THEN @CantidadCondiciones ELSE T.D20 END,
					T.D21 = CASE WHEN DAY(O.Fecha) = 21 THEN @CantidadCondiciones ELSE T.D21 END,
					T.D22 = CASE WHEN DAY(O.Fecha) = 22 THEN @CantidadCondiciones ELSE T.D22 END,
					T.D23 = CASE WHEN DAY(O.Fecha) = 23 THEN @CantidadCondiciones ELSE T.D23 END,
					T.D24 = CASE WHEN DAY(O.Fecha) = 24 THEN @CantidadCondiciones ELSE T.D24 END,
					T.D25 = CASE WHEN DAY(O.Fecha) = 25 THEN @CantidadCondiciones ELSE T.D25 END,
					T.D26 = CASE WHEN DAY(O.Fecha) = 26 THEN @CantidadCondiciones ELSE T.D26 END,
					T.D27 = CASE WHEN DAY(O.Fecha) = 27 THEN @CantidadCondiciones ELSE T.D27 END,
					T.D28 = CASE WHEN DAY(O.Fecha) = 28 THEN @CantidadCondiciones ELSE T.D28 END,
					T.D29 = CASE WHEN DAY(O.Fecha) = 29 THEN @CantidadCondiciones ELSE T.D29 END,
					T.D30 = CASE WHEN DAY(O.Fecha) = 30 THEN @CantidadCondiciones ELSE T.D30 END,
					T.D31 = CASE WHEN DAY(O.Fecha) = 31 THEN @CantidadCondiciones ELSE T.D31 END
				FROM ReportesApp_Operaciones_Operatividad_ViewTolvas T
				LEFT JOIN @OP_PLACAS O ON T.idCondicionTL = O.idCondicion AND O.Fecha = @x_FechaSelec
				WHERE T.Anio = RIGHT(@Periodo,4) AND T.Mes = LEFT(@periodo,2)
			END

			IF (@TipoUnidad = 'CISTERNA' AND (@idCondicion IN (1,2,3,4,16,5,6,10,8) OR @CondicionAnterior IN (1,2,3,4,16,5,6,10,8))) BEGIN
				SET @CantidadCondiciones = (SELECT COUNT(*) FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas C
											LEFT JOIN OP_TR_VEHICULO VH ON C.IdUnidad = VH.IdVehiculo
											LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = VH.SubTipoVehiculo
											WHERE C.Fecha = @x_FechaSelec AND C.idCondicion = @x_idCondicion AND LTRIM(RTRIM(SV.Descripcion)) = @TipoUnidad)

				UPDATE T 
				SET T.D1 = CASE WHEN DAY(O.Fecha) = 1 THEN (CASE WHEN T.D1 - 1 = 0 THEN NULL ELSE T.D1 - 1 END) ELSE T.D1 END,
					T.D2 = CASE WHEN DAY(O.Fecha) = 2 THEN (CASE WHEN T.D2 - 1 = 0 THEN NULL ELSE T.D2 - 1 END) ELSE T.D2 END,
					T.D3 = CASE WHEN DAY(O.Fecha) = 3 THEN (CASE WHEN T.D3 - 1 = 0 THEN NULL ELSE T.D3 - 1 END) ELSE T.D3 END,
					T.D4 = CASE WHEN DAY(O.Fecha) = 4 THEN (CASE WHEN T.D4 - 1 = 0 THEN NULL ELSE T.D4 - 1 END) ELSE T.D4 END,
					T.D5 = CASE WHEN DAY(O.Fecha) = 5 THEN (CASE WHEN T.D5 - 1 = 0 THEN NULL ELSE T.D5 - 1 END) ELSE T.D5 END,
					T.D6 = CASE WHEN DAY(O.Fecha) = 6 THEN (CASE WHEN T.D6 - 1 = 0 THEN NULL ELSE T.D6 - 1 END) ELSE T.D6 END,
					T.D7 = CASE WHEN DAY(O.Fecha) = 7 THEN (CASE WHEN T.D7 - 1 = 0 THEN NULL ELSE T.D7 - 1 END) ELSE T.D7 END,
					T.D8 = CASE WHEN DAY(O.Fecha) = 8 THEN (CASE WHEN T.D8 - 1 = 0 THEN NULL ELSE T.D8 - 1 END) ELSE T.D8 END,
					T.D9 = CASE WHEN DAY(O.Fecha) = 9 THEN (CASE WHEN T.D9 - 1 = 0 THEN NULL ELSE T.D9 - 1 END) ELSE T.D9 END,
					T.D10 = CASE WHEN DAY(O.Fecha) = 10 THEN (CASE WHEN T.D10 - 1 = 0 THEN NULL ELSE T.D10 - 1 END) ELSE T.D10 END,
					T.D11 = CASE WHEN DAY(O.Fecha) = 11 THEN (CASE WHEN T.D11 - 1 = 0 THEN NULL ELSE T.D11 - 1 END) ELSE T.D11 END,
					T.D12 = CASE WHEN DAY(O.Fecha) = 12 THEN (CASE WHEN T.D12 - 1 = 0 THEN NULL ELSE T.D12 - 1 END) ELSE T.D12 END,
					T.D13 = CASE WHEN DAY(O.Fecha) = 13 THEN (CASE WHEN T.D13 - 1 = 0 THEN NULL ELSE T.D13 - 1 END) ELSE T.D13 END,
					T.D14 = CASE WHEN DAY(O.Fecha) = 14 THEN (CASE WHEN T.D14 - 1 = 0 THEN NULL ELSE T.D14 - 1 END) ELSE T.D14 END,
					T.D15 = CASE WHEN DAY(O.Fecha) = 15 THEN (CASE WHEN T.D15 - 1 = 0 THEN NULL ELSE T.D15 - 1 END) ELSE T.D15 END,
					T.D16 = CASE WHEN DAY(O.Fecha) = 16 THEN (CASE WHEN T.D16 - 1 = 0 THEN NULL ELSE T.D16 - 1 END) ELSE T.D16 END,
					T.D17 = CASE WHEN DAY(O.Fecha) = 17 THEN (CASE WHEN T.D17 - 1 = 0 THEN NULL ELSE T.D17 - 1 END) ELSE T.D17 END,
					T.D18 = CASE WHEN DAY(O.Fecha) = 18 THEN (CASE WHEN T.D18 - 1 = 0 THEN NULL ELSE T.D18 - 1 END) ELSE T.D18 END,
					T.D19 = CASE WHEN DAY(O.Fecha) = 19 THEN (CASE WHEN T.D19 - 1 = 0 THEN NULL ELSE T.D19 - 1 END) ELSE T.D19 END,
					T.D20 = CASE WHEN DAY(O.Fecha) = 20 THEN (CASE WHEN T.D20 - 1 = 0 THEN NULL ELSE T.D20 - 1 END) ELSE T.D20 END,
					T.D21 = CASE WHEN DAY(O.Fecha) = 21 THEN (CASE WHEN T.D21 - 1 = 0 THEN NULL ELSE T.D21 - 1 END) ELSE T.D21 END,
					T.D22 = CASE WHEN DAY(O.Fecha) = 22 THEN (CASE WHEN T.D22 - 1 = 0 THEN NULL ELSE T.D22 - 1 END) ELSE T.D22 END,
					T.D23 = CASE WHEN DAY(O.Fecha) = 23 THEN (CASE WHEN T.D23 - 1 = 0 THEN NULL ELSE T.D23 - 1 END) ELSE T.D23 END,
					T.D24 = CASE WHEN DAY(O.Fecha) = 24 THEN (CASE WHEN T.D24 - 1 = 0 THEN NULL ELSE T.D24 - 1 END) ELSE T.D24 END,
					T.D25 = CASE WHEN DAY(O.Fecha) = 25 THEN (CASE WHEN T.D25 - 1 = 0 THEN NULL ELSE T.D25 - 1 END) ELSE T.D25 END,
					T.D26 = CASE WHEN DAY(O.Fecha) = 26 THEN (CASE WHEN T.D26 - 1 = 0 THEN NULL ELSE T.D26 - 1 END) ELSE T.D26 END,
					T.D27 = CASE WHEN DAY(O.Fecha) = 27 THEN (CASE WHEN T.D27 - 1 = 0 THEN NULL ELSE T.D27 - 1 END) ELSE T.D27 END,
					T.D28 = CASE WHEN DAY(O.Fecha) = 28 THEN (CASE WHEN T.D28 - 1 = 0 THEN NULL ELSE T.D28 - 1 END) ELSE T.D28 END,
					T.D29 = CASE WHEN DAY(O.Fecha) = 29 THEN (CASE WHEN T.D29 - 1 = 0 THEN NULL ELSE T.D29 - 1 END) ELSE T.D29 END,
					T.D30 = CASE WHEN DAY(O.Fecha) = 30 THEN (CASE WHEN T.D30 - 1 = 0 THEN NULL ELSE T.D30 - 1 END) ELSE T.D30 END,
					T.D31 = CASE WHEN DAY(O.Fecha) = 31 THEN (CASE WHEN T.D31 - 1 = 0 THEN NULL ELSE T.D31 - 1 END) ELSE T.D31 END
				FROM ReportesApp_Operaciones_Operatividad_ViewCisternas T
				LEFT JOIN @OP_PLACAS O ON T.idCondicionCS = @CondicionAnterior AND O.Fecha = @x_FechaSelec
				WHERE T.Anio = RIGHT(@Periodo,4) AND T.Mes = LEFT(@periodo,2)

				UPDATE T 
				SET T.D1 = CASE WHEN DAY(O.Fecha) = 1 THEN @CantidadCondiciones ELSE T.D1 END,
					T.D2 = CASE WHEN DAY(O.Fecha) = 2 THEN @CantidadCondiciones ELSE T.D2 END,
					T.D3 = CASE WHEN DAY(O.Fecha) = 3 THEN @CantidadCondiciones ELSE T.D3 END,
					T.D4 = CASE WHEN DAY(O.Fecha) = 4 THEN @CantidadCondiciones ELSE T.D4 END,
					T.D5 = CASE WHEN DAY(O.Fecha) = 5 THEN @CantidadCondiciones ELSE T.D5 END,
					T.D6 = CASE WHEN DAY(O.Fecha) = 6 THEN @CantidadCondiciones ELSE T.D6 END,
					T.D7 = CASE WHEN DAY(O.Fecha) = 7 THEN @CantidadCondiciones ELSE T.D7 END,
					T.D8 = CASE WHEN DAY(O.Fecha) = 8 THEN @CantidadCondiciones ELSE T.D8 END,
					T.D9 = CASE WHEN DAY(O.Fecha) = 9 THEN @CantidadCondiciones ELSE T.D9 END,
					T.D10 = CASE WHEN DAY(O.Fecha) = 10 THEN @CantidadCondiciones ELSE T.D10 END,
					T.D11 = CASE WHEN DAY(O.Fecha) = 11 THEN @CantidadCondiciones ELSE T.D11 END,
					T.D12 = CASE WHEN DAY(O.Fecha) = 12 THEN @CantidadCondiciones ELSE T.D12 END,
					T.D13 = CASE WHEN DAY(O.Fecha) = 13 THEN @CantidadCondiciones ELSE T.D13 END,
					T.D14 = CASE WHEN DAY(O.Fecha) = 14 THEN @CantidadCondiciones ELSE T.D14 END,
					T.D15 = CASE WHEN DAY(O.Fecha) = 15 THEN @CantidadCondiciones ELSE T.D15 END,
					T.D16 = CASE WHEN DAY(O.Fecha) = 16 THEN @CantidadCondiciones ELSE T.D16 END,
					T.D17 = CASE WHEN DAY(O.Fecha) = 17 THEN @CantidadCondiciones ELSE T.D17 END,
					T.D18 = CASE WHEN DAY(O.Fecha) = 18 THEN @CantidadCondiciones ELSE T.D18 END,
					T.D19 = CASE WHEN DAY(O.Fecha) = 19 THEN @CantidadCondiciones ELSE T.D19 END,
					T.D20 = CASE WHEN DAY(O.Fecha) = 20 THEN @CantidadCondiciones ELSE T.D20 END,
					T.D21 = CASE WHEN DAY(O.Fecha) = 21 THEN @CantidadCondiciones ELSE T.D21 END,
					T.D22 = CASE WHEN DAY(O.Fecha) = 22 THEN @CantidadCondiciones ELSE T.D22 END,
					T.D23 = CASE WHEN DAY(O.Fecha) = 23 THEN @CantidadCondiciones ELSE T.D23 END,
					T.D24 = CASE WHEN DAY(O.Fecha) = 24 THEN @CantidadCondiciones ELSE T.D24 END,
					T.D25 = CASE WHEN DAY(O.Fecha) = 25 THEN @CantidadCondiciones ELSE T.D25 END,
					T.D26 = CASE WHEN DAY(O.Fecha) = 26 THEN @CantidadCondiciones ELSE T.D26 END,
					T.D27 = CASE WHEN DAY(O.Fecha) = 27 THEN @CantidadCondiciones ELSE T.D27 END,
					T.D28 = CASE WHEN DAY(O.Fecha) = 28 THEN @CantidadCondiciones ELSE T.D28 END,
					T.D29 = CASE WHEN DAY(O.Fecha) = 29 THEN @CantidadCondiciones ELSE T.D29 END,
					T.D30 = CASE WHEN DAY(O.Fecha) = 30 THEN @CantidadCondiciones ELSE T.D30 END,
					T.D31 = CASE WHEN DAY(O.Fecha) = 31 THEN @CantidadCondiciones ELSE T.D31 END
				FROM ReportesApp_Operaciones_Operatividad_ViewCisternas T
				LEFT JOIN @OP_PLACAS O ON T.idCondicionCS = O.idCondicion AND O.Fecha = @x_FechaSelec
				WHERE T.Anio = RIGHT(@Periodo,4) AND T.Mes = LEFT(@periodo,2)
			END
		END
		ELSE BEGIN
			SET @Exito = '-1 = No tiene permiso para registrar esta condición en la unidad.'
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

-----------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 17-06-2024
-- Description:	LISTAR TRACTOS OPERATIVIDAD
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Operatividad_ListarTractosOP]
@Opcion INT,
@idCondicion INT,
@Fecha DATE,
@TipoUnidad VARCHAR(100),
@Usuario VARCHAR(20)
AS
BEGIN
	IF (@Opcion = 1) BEGIN		-- LISTAR OPERATIVIDAD - TRACTOS
		IF (@idCondicion IN (5,6)) BEGIN
			SELECT OP.idOperatividad, OP.IdUnidad, VH.NumeroPlaca AS 'PLACA', SV.Descripcion AS 'TIPO_UNIDAD', O.Descripcion AS 'OPERACION',
			(SELECT COUNT(*) FROM ReportesApp_Operaciones_Operatividad_Registro WHERE IdUnidad = OP.IdUnidad AND idCondicion = OP.idCondicion
			AND YEAR(Fecha) = YEAR(@Fecha) AND MONTH(Fecha) = MONTH(@Fecha)) AS 'DIAS_ACUMULADOS',
			CONVERT(VARCHAR,OP.FechaTermino,103)+' '+CONVERT(VARCHAR,OP.FechaTermino,8) AS 'FECHA_TERMINO', OP.Comentario AS 'COMENTARIO',
			OP.UsuarioRegistra, OP.FechaRegistra
			FROM ReportesApp_Operaciones_Operatividad_Registro OP
			LEFT JOIN OP_TR_VEHICULO VH ON VH.IdVehiculo = OP.IdUnidad
			LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = VH.SubTipoVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = OP.IdOperacion
			WHERE OP.idCondicion = @idCondicion AND OP.Fecha = @Fecha
			ORDER BY VH.NumeroPlaca
		END
		ELSE BEGIN
			SELECT OP.idOperatividad, OP.IdUnidad, VH.NumeroPlaca AS 'PLACA', SV.Descripcion AS 'TIPO_UNIDAD', O.Descripcion AS 'OPERACION',
			(SELECT COUNT(*) FROM ReportesApp_Operaciones_Operatividad_Registro WHERE IdUnidad = OP.IdUnidad AND idCondicion = OP.idCondicion
			AND YEAR(Fecha) = YEAR(@Fecha) AND MONTH(Fecha) = MONTH(@Fecha)) AS 'DIAS_ACUMULADOS', OP.Comentario AS 'COMENTARIO',
			OP.UsuarioRegistra, OP.FechaRegistra
			FROM ReportesApp_Operaciones_Operatividad_Registro OP
			LEFT JOIN OP_TR_VEHICULO VH ON VH.IdVehiculo = OP.IdUnidad
			LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = VH.SubTipoVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = OP.IdOperacion
			WHERE OP.idCondicion = @idCondicion AND OP.Fecha = @Fecha
			ORDER BY VH.NumeroPlaca
		END
	END

	IF (@Opcion = 2) BEGIN		-- LISTAR OPERATIVIDAD - CARRETAS
		IF (@TipoUnidad = 'TODOS') BEGIN
			IF (@idCondicion IN (5,6)) BEGIN
				SELECT OP.idOperatividad, OP.IdUnidad, VH.NumeroPlaca AS 'PLACA', SV.Descripcion AS 'TIPO_UNIDAD', O.Descripcion AS 'OPERACION',
				(SELECT COUNT(*) FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas WHERE IdUnidad = OP.IdUnidad AND idCondicion = OP.idCondicion
				AND YEAR(Fecha) = YEAR(@Fecha) AND MONTH(Fecha) = MONTH(@Fecha)) AS 'DIAS_ACUMULADOS',
				CONVERT(VARCHAR,OP.FechaTermino,103)+' '+CONVERT(VARCHAR,OP.FechaTermino,8) AS 'FECHA_TERMINO', OP.Comentario AS 'COMENTARIO',
				OP.UsuarioRegistra, OP.FechaRegistra
				FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas OP
				LEFT JOIN OP_TR_VEHICULO VH ON VH.IdVehiculo = OP.IdUnidad
				LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = VH.SubTipoVehiculo
				LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = OP.IdOperacion
				WHERE OP.idCondicion = @idCondicion AND OP.Fecha = @Fecha
				ORDER BY VH.NumeroPlaca
			END
			ELSE BEGIN
				SELECT OP.idOperatividad, OP.IdUnidad, VH.NumeroPlaca AS 'PLACA', SV.Descripcion AS 'TIPO_UNIDAD', O.Descripcion AS 'OPERACION',
				(SELECT COUNT(*) FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas WHERE IdUnidad = OP.IdUnidad AND idCondicion = OP.idCondicion
				AND YEAR(Fecha) = YEAR(@Fecha) AND MONTH(Fecha) = MONTH(@Fecha)) AS 'DIAS_ACUMULADOS', OP.Comentario AS 'COMENTARIO',
				OP.UsuarioRegistra, OP.FechaRegistra
				FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas OP
				LEFT JOIN OP_TR_VEHICULO VH ON VH.IdVehiculo = OP.IdUnidad
				LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = VH.SubTipoVehiculo
				LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = OP.IdOperacion
				WHERE OP.idCondicion = @idCondicion AND OP.Fecha = @Fecha
				ORDER BY VH.NumeroPlaca
			END
		END
		ELSE BEGIN
			IF (@idCondicion IN (5,6)) BEGIN
				SELECT OP.idOperatividad, OP.IdUnidad, VH.NumeroPlaca AS 'PLACA', SV.Descripcion AS 'TIPO_UNIDAD', O.Descripcion AS 'OPERACION',
				(SELECT COUNT(*) FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas WHERE IdUnidad = OP.IdUnidad AND idCondicion = OP.idCondicion
				AND YEAR(Fecha) = YEAR(@Fecha) AND MONTH(Fecha) = MONTH(@Fecha)) AS 'DIAS_ACUMULADOS',
				CONVERT(VARCHAR,OP.FechaTermino,103)+' '+CONVERT(VARCHAR,OP.FechaTermino,8) AS 'FECHA_TERMINO', OP.Comentario AS 'COMENTARIO',
				OP.UsuarioRegistra, OP.FechaRegistra
				FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas OP
				LEFT JOIN OP_TR_VEHICULO VH ON VH.IdVehiculo = OP.IdUnidad
				LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = VH.SubTipoVehiculo
				LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = OP.IdOperacion
				WHERE OP.idCondicion = @idCondicion AND OP.Fecha = @Fecha AND LTRIM(RTRIM(SV.Descripcion)) = @TipoUnidad
				ORDER BY VH.NumeroPlaca
			END
			ELSE BEGIN
				SELECT OP.idOperatividad, OP.IdUnidad, VH.NumeroPlaca AS 'PLACA', SV.Descripcion AS 'TIPO_UNIDAD', O.Descripcion AS 'OPERACION',
				(SELECT COUNT(*) FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas WHERE IdUnidad = OP.IdUnidad AND idCondicion = OP.idCondicion
				AND YEAR(Fecha) = YEAR(@Fecha) AND MONTH(Fecha) = MONTH(@Fecha)) AS 'DIAS_ACUMULADOS', OP.Comentario AS 'COMENTARIO',
				OP.UsuarioRegistra, OP.FechaRegistra
				FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas OP
				LEFT JOIN OP_TR_VEHICULO VH ON VH.IdVehiculo = OP.IdUnidad
				LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = VH.SubTipoVehiculo
				LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = OP.IdOperacion
				WHERE OP.idCondicion = @idCondicion AND OP.Fecha = @Fecha AND LTRIM(RTRIM(SV.Descripcion)) = @TipoUnidad
				ORDER BY VH.NumeroPlaca
			END
		END
	END
END

-----------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 12-08-2025
-- Description:	INGRESAR COMENTARIOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Operatividad_IngresarComentarios]
@idOperatividad INT,
@TipoUnidad VARCHAR(50),
@Comentario VARCHAR(250)
AS	
DECLARE @exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@TipoUnidad = 'TRACTO') BEGIN
		UPDATE ReportesApp_Operaciones_Operatividad_Registro
		SET Comentario = @Comentario
		WHERE idOperatividad = @idOperatividad
	END
	ELSE BEGIN
		UPDATE ReportesApp_Operaciones_Operatividad_RegistroCarretas
		SET Comentario = @Comentario
		WHERE idOperatividad = @idOperatividad
	END

	SET @exito = '0 = Comentario añadido.'
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

-----------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 09-09-2024
-- Description:	GUARDAR VIAJE ESTADO CARRETAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Operatividad_ActualizarEstadoCarretas]
@IDTicket INT,
@Anio SMALLINT,
@IDTracto INT,
@Estado INT,
@TipoProgramacion INT,
@FInicio DATETIME,
@FFin DATETIME,
@Usuario VARCHAR(80)
AS	
DECLARE @exito VARCHAR(MAX)	

BEGIN TRAN
BEGIN TRY
	DECLARE @NroTicket INT = (SELECT NroTicket FROM ReportesApp_Operacion_Previaje_Registros WHERE IdProgramacion = @IDTicket AND Anio = @Anio)
	DECLARE @FechaInicio DATE = (SELECT CONVERT(DATE,@FInicio))
	DECLARE @FechaFin DATE = (SELECT CONVERT(DATE,@FFin))

	IF (@Estado IN (1,9) AND @TipoProgramacion IN (1,2,3,4,10) AND EXISTS(SELECT * FROM ReportesApp_Operaciones_Operatividad_CarretasView WHERE IdUnidad = @IDTracto
	AND Mes = MONTH(@FechaInicio) AND Anio = YEAR(@FechaInicio))) BEGIN
		WHILE (@FechaInicio <= @FechaFin) BEGIN
			DECLARE @correlativo INT
			DECLARE @idCondicion INT = (SELECT CASE WHEN @TipoProgramacion = 1 THEN 3 WHEN @TipoProgramacion = 2 THEN 1 WHEN @TipoProgramacion = 3 THEN 2
										WHEN @TipoProgramacion = 10 THEN 16 ELSE 4 END)
			DECLARE @CondicionAnterior INT = (SELECT idCondicion FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas WHERE IdUnidad = @IDTracto AND Fecha = @FechaInicio)

			DELETE FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas
			WHERE IdUnidad = @IDTracto AND Fecha = @FechaInicio

			SET @correlativo = (SELECT MAX(idOperatividad) FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas)
			SET @correlativo = ISNULL(@correlativo,0) + 1

			INSERT INTO ReportesApp_Operaciones_Operatividad_RegistroCarretas(idOperatividad,IdUnidad,Fecha,idCondicion,IdOperacion,UsuarioRegistra,FechaRegistra)
			VALUES(@correlativo, @IDTracto, @FechaInicio, @idCondicion, @TipoProgramacion, @Usuario, GETDATE())

			DECLARE @x_FechaSelec DATE = @FechaInicio
			DECLARE @x_DiaSelec INT = (SELECT DAY(@FechaInicio))
			DECLARE @Condicion VARCHAR(50) = (SELECT C.Codigo FROM ReportesApp_Operaciones_Operatividad_Condiciones C WHERE C.idCondicion = @idCondicion)
			DECLARE @CantidadCondiciones INT = (SELECT COUNT(*) FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas
										WHERE Fecha = @x_FechaSelec AND idCondicion = @idCondicion)
			DECLARE @TipoUnidad VARCHAR(150) = (SELECT LTRIM(RTRIM(SV.Descripcion)) FROM OP_TR_VEHICULO VH
												LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = VH.SubTipoVehiculo
												WHERE VH.IdVehiculo = @IDTracto)

			IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_Operatividad_CarretasView WHERE Anio = YEAR(@x_FechaSelec) AND Mes = MONTH(@x_FechaSelec)
			AND IdUnidad = @IDTracto)) BEGIN
				UPDATE P 
				SET P.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN @Condicion ELSE P.D1 END,
					P.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN @Condicion ELSE P.D2 END,
					P.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN @Condicion ELSE P.D3 END,
					P.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN @Condicion ELSE P.D4 END,
					P.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN @Condicion ELSE P.D5 END,
					P.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN @Condicion ELSE P.D6 END,
					P.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN @Condicion ELSE P.D7 END,
					P.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN @Condicion ELSE P.D8 END,
					P.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN @Condicion ELSE P.D9 END,
					P.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN @Condicion ELSE P.D10 END,
					P.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN @Condicion ELSE P.D11 END,
					P.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN @Condicion ELSE P.D12 END,
					P.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN @Condicion ELSE P.D13 END,
					P.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN @Condicion ELSE P.D14 END,
					P.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN @Condicion ELSE P.D15 END,
					P.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN @Condicion ELSE P.D16 END,
					P.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN @Condicion ELSE P.D17 END,
					P.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN @Condicion ELSE P.D18 END,
					P.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN @Condicion ELSE P.D19 END,
					P.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN @Condicion ELSE P.D20 END,
					P.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN @Condicion ELSE P.D21 END,
					P.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN @Condicion ELSE P.D22 END,
					P.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN @Condicion ELSE P.D23 END,
					P.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN @Condicion ELSE P.D24 END,
					P.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN @Condicion ELSE P.D25 END,
					P.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN @Condicion ELSE P.D26 END,
					P.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN @Condicion ELSE P.D27 END,
					P.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN @Condicion ELSE P.D28 END,
					P.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN @Condicion ELSE P.D29 END,
					P.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN @Condicion ELSE P.D30 END,
					P.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN @Condicion ELSE P.D31 END
				FROM ReportesApp_Operaciones_Operatividad_CarretasView P
				WHERE P.Anio = YEAR(@x_FechaSelec) AND P.Mes = MONTH(@x_FechaSelec) AND P.IdUnidad = @IDTracto
			END

			IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_Operatividad_ViewCarretas WHERE Anio = YEAR(@x_FechaSelec) AND Mes = MONTH(@x_FechaSelec))) BEGIN
				UPDATE T 
				SET T.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN (CASE WHEN T.D1 - 1 = 0 THEN NULL ELSE T.D1 - 1 END) ELSE T.D1 END,
					T.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN (CASE WHEN T.D2 - 1 = 0 THEN NULL ELSE T.D2 - 1 END) ELSE T.D2 END,
					T.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN (CASE WHEN T.D3 - 1 = 0 THEN NULL ELSE T.D3 - 1 END) ELSE T.D3 END,
					T.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN (CASE WHEN T.D4 - 1 = 0 THEN NULL ELSE T.D4 - 1 END) ELSE T.D4 END,
					T.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN (CASE WHEN T.D5 - 1 = 0 THEN NULL ELSE T.D5 - 1 END) ELSE T.D5 END,
					T.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN (CASE WHEN T.D6 - 1 = 0 THEN NULL ELSE T.D6 - 1 END) ELSE T.D6 END,
					T.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN (CASE WHEN T.D7 - 1 = 0 THEN NULL ELSE T.D7 - 1 END) ELSE T.D7 END,
					T.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN (CASE WHEN T.D8 - 1 = 0 THEN NULL ELSE T.D8 - 1 END) ELSE T.D8 END,
					T.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN (CASE WHEN T.D9 - 1 = 0 THEN NULL ELSE T.D9 - 1 END) ELSE T.D9 END,
					T.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN (CASE WHEN T.D10 - 1 = 0 THEN NULL ELSE T.D10 - 1 END) ELSE T.D10 END,
					T.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN (CASE WHEN T.D11 - 1 = 0 THEN NULL ELSE T.D11 - 1 END) ELSE T.D11 END,
					T.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN (CASE WHEN T.D12 - 1 = 0 THEN NULL ELSE T.D12 - 1 END) ELSE T.D12 END,
					T.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN (CASE WHEN T.D13 - 1 = 0 THEN NULL ELSE T.D13 - 1 END) ELSE T.D13 END,
					T.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN (CASE WHEN T.D14 - 1 = 0 THEN NULL ELSE T.D14 - 1 END) ELSE T.D14 END,
					T.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN (CASE WHEN T.D15 - 1 = 0 THEN NULL ELSE T.D15 - 1 END) ELSE T.D15 END,
					T.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN (CASE WHEN T.D16 - 1 = 0 THEN NULL ELSE T.D16 - 1 END) ELSE T.D16 END,
					T.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN (CASE WHEN T.D17 - 1 = 0 THEN NULL ELSE T.D17 - 1 END) ELSE T.D17 END,
					T.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN (CASE WHEN T.D18 - 1 = 0 THEN NULL ELSE T.D18 - 1 END) ELSE T.D18 END,
					T.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN (CASE WHEN T.D19 - 1 = 0 THEN NULL ELSE T.D19 - 1 END) ELSE T.D19 END,
					T.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN (CASE WHEN T.D20 - 1 = 0 THEN NULL ELSE T.D20 - 1 END) ELSE T.D20 END,
					T.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN (CASE WHEN T.D21 - 1 = 0 THEN NULL ELSE T.D21 - 1 END) ELSE T.D21 END,
					T.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN (CASE WHEN T.D22 - 1 = 0 THEN NULL ELSE T.D22 - 1 END) ELSE T.D22 END,
					T.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN (CASE WHEN T.D23 - 1 = 0 THEN NULL ELSE T.D23 - 1 END) ELSE T.D23 END,
					T.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN (CASE WHEN T.D24 - 1 = 0 THEN NULL ELSE T.D24 - 1 END) ELSE T.D24 END,
					T.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN (CASE WHEN T.D25 - 1 = 0 THEN NULL ELSE T.D25 - 1 END) ELSE T.D25 END,
					T.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN (CASE WHEN T.D26 - 1 = 0 THEN NULL ELSE T.D26 - 1 END) ELSE T.D26 END,
					T.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN (CASE WHEN T.D27 - 1 = 0 THEN NULL ELSE T.D27 - 1 END) ELSE T.D27 END,
					T.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN (CASE WHEN T.D28 - 1 = 0 THEN NULL ELSE T.D28 - 1 END) ELSE T.D28 END,
					T.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN (CASE WHEN T.D29 - 1 = 0 THEN NULL ELSE T.D29 - 1 END) ELSE T.D29 END,
					T.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN (CASE WHEN T.D30 - 1 = 0 THEN NULL ELSE T.D30 - 1 END) ELSE T.D30 END,
					T.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN (CASE WHEN T.D31 - 1 = 0 THEN NULL ELSE T.D31 - 1 END) ELSE T.D31 END
				FROM ReportesApp_Operaciones_Operatividad_ViewCarretas T
				WHERE T.Anio = YEAR(@x_FechaSelec) AND T.Mes = MONTH(@x_FechaSelec) AND T.idCondicion = @CondicionAnterior

				UPDATE T 
				SET T.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN @CantidadCondiciones ELSE T.D1 END,
					T.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN @CantidadCondiciones ELSE T.D2 END,
					T.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN @CantidadCondiciones ELSE T.D3 END,
					T.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN @CantidadCondiciones ELSE T.D4 END,
					T.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN @CantidadCondiciones ELSE T.D5 END,
					T.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN @CantidadCondiciones ELSE T.D6 END,
					T.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN @CantidadCondiciones ELSE T.D7 END,
					T.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN @CantidadCondiciones ELSE T.D8 END,
					T.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN @CantidadCondiciones ELSE T.D9 END,
					T.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN @CantidadCondiciones ELSE T.D10 END,
					T.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN @CantidadCondiciones ELSE T.D11 END,
					T.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN @CantidadCondiciones ELSE T.D12 END,
					T.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN @CantidadCondiciones ELSE T.D13 END,
					T.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN @CantidadCondiciones ELSE T.D14 END,
					T.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN @CantidadCondiciones ELSE T.D15 END,
					T.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN @CantidadCondiciones ELSE T.D16 END,
					T.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN @CantidadCondiciones ELSE T.D17 END,
					T.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN @CantidadCondiciones ELSE T.D18 END,
					T.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN @CantidadCondiciones ELSE T.D19 END,
					T.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN @CantidadCondiciones ELSE T.D20 END,
					T.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN @CantidadCondiciones ELSE T.D21 END,
					T.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN @CantidadCondiciones ELSE T.D22 END,
					T.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN @CantidadCondiciones ELSE T.D23 END,
					T.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN @CantidadCondiciones ELSE T.D24 END,
					T.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN @CantidadCondiciones ELSE T.D25 END,
					T.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN @CantidadCondiciones ELSE T.D26 END,
					T.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN @CantidadCondiciones ELSE T.D27 END,
					T.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN @CantidadCondiciones ELSE T.D28 END,
					T.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN @CantidadCondiciones ELSE T.D29 END,
					T.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN @CantidadCondiciones ELSE T.D30 END,
					T.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN @CantidadCondiciones ELSE T.D31 END
				FROM ReportesApp_Operaciones_Operatividad_ViewCarretas T
				WHERE T.Anio = YEAR(@x_FechaSelec) AND T.Mes = MONTH(@x_FechaSelec) AND T.idCondicion = @idCondicion

				IF (@TipoUnidad = 'CORTINERA' AND (@idCondicion IN (1,2,3,4,16,5,6,13,10,8) OR @CondicionAnterior IN (1,2,3,4,16,5,6,13,10,8))) BEGIN
					SET @CantidadCondiciones = (SELECT COUNT(*) FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas C
												LEFT JOIN OP_TR_VEHICULO VH ON C.IdUnidad = VH.IdVehiculo
												LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = VH.SubTipoVehiculo
												WHERE C.Fecha = @x_FechaSelec AND C.idCondicion = @idCondicion AND LTRIM(RTRIM(SV.Descripcion)) = @TipoUnidad)

					UPDATE T 
					SET T.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN (CASE WHEN T.D1 - 1 = 0 THEN NULL ELSE T.D1 - 1 END) ELSE T.D1 END,
						T.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN (CASE WHEN T.D2 - 1 = 0 THEN NULL ELSE T.D2 - 1 END) ELSE T.D2 END,
						T.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN (CASE WHEN T.D3 - 1 = 0 THEN NULL ELSE T.D3 - 1 END) ELSE T.D3 END,
						T.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN (CASE WHEN T.D4 - 1 = 0 THEN NULL ELSE T.D4 - 1 END) ELSE T.D4 END,
						T.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN (CASE WHEN T.D5 - 1 = 0 THEN NULL ELSE T.D5 - 1 END) ELSE T.D5 END,
						T.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN (CASE WHEN T.D6 - 1 = 0 THEN NULL ELSE T.D6 - 1 END) ELSE T.D6 END,
						T.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN (CASE WHEN T.D7 - 1 = 0 THEN NULL ELSE T.D7 - 1 END) ELSE T.D7 END,
						T.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN (CASE WHEN T.D8 - 1 = 0 THEN NULL ELSE T.D8 - 1 END) ELSE T.D8 END,
						T.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN (CASE WHEN T.D9 - 1 = 0 THEN NULL ELSE T.D9 - 1 END) ELSE T.D9 END,
						T.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN (CASE WHEN T.D10 - 1 = 0 THEN NULL ELSE T.D10 - 1 END) ELSE T.D10 END,
						T.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN (CASE WHEN T.D11 - 1 = 0 THEN NULL ELSE T.D11 - 1 END) ELSE T.D11 END,
						T.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN (CASE WHEN T.D12 - 1 = 0 THEN NULL ELSE T.D12 - 1 END) ELSE T.D12 END,
						T.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN (CASE WHEN T.D13 - 1 = 0 THEN NULL ELSE T.D13 - 1 END) ELSE T.D13 END,
						T.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN (CASE WHEN T.D14 - 1 = 0 THEN NULL ELSE T.D14 - 1 END) ELSE T.D14 END,
						T.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN (CASE WHEN T.D15 - 1 = 0 THEN NULL ELSE T.D15 - 1 END) ELSE T.D15 END,
						T.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN (CASE WHEN T.D16 - 1 = 0 THEN NULL ELSE T.D16 - 1 END) ELSE T.D16 END,
						T.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN (CASE WHEN T.D17 - 1 = 0 THEN NULL ELSE T.D17 - 1 END) ELSE T.D17 END,
						T.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN (CASE WHEN T.D18 - 1 = 0 THEN NULL ELSE T.D18 - 1 END) ELSE T.D18 END,
						T.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN (CASE WHEN T.D19 - 1 = 0 THEN NULL ELSE T.D19 - 1 END) ELSE T.D19 END,
						T.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN (CASE WHEN T.D20 - 1 = 0 THEN NULL ELSE T.D20 - 1 END) ELSE T.D20 END,
						T.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN (CASE WHEN T.D21 - 1 = 0 THEN NULL ELSE T.D21 - 1 END) ELSE T.D21 END,
						T.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN (CASE WHEN T.D22 - 1 = 0 THEN NULL ELSE T.D22 - 1 END) ELSE T.D22 END,
						T.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN (CASE WHEN T.D23 - 1 = 0 THEN NULL ELSE T.D23 - 1 END) ELSE T.D23 END,
						T.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN (CASE WHEN T.D24 - 1 = 0 THEN NULL ELSE T.D24 - 1 END) ELSE T.D24 END,
						T.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN (CASE WHEN T.D25 - 1 = 0 THEN NULL ELSE T.D25 - 1 END) ELSE T.D25 END,
						T.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN (CASE WHEN T.D26 - 1 = 0 THEN NULL ELSE T.D26 - 1 END) ELSE T.D26 END,
						T.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN (CASE WHEN T.D27 - 1 = 0 THEN NULL ELSE T.D27 - 1 END) ELSE T.D27 END,
						T.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN (CASE WHEN T.D28 - 1 = 0 THEN NULL ELSE T.D28 - 1 END) ELSE T.D28 END,
						T.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN (CASE WHEN T.D29 - 1 = 0 THEN NULL ELSE T.D29 - 1 END) ELSE T.D29 END,
						T.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN (CASE WHEN T.D30 - 1 = 0 THEN NULL ELSE T.D30 - 1 END) ELSE T.D30 END,
						T.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN (CASE WHEN T.D31 - 1 = 0 THEN NULL ELSE T.D31 - 1 END) ELSE T.D31 END
					FROM ReportesApp_Operaciones_Operatividad_ViewCortineras T
					WHERE T.Anio = YEAR(@x_FechaSelec) AND T.Mes = MONTH(@x_FechaSelec) AND T.idCondicionCR = @CondicionAnterior

					UPDATE T 
					SET T.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN @CantidadCondiciones ELSE T.D1 END,
						T.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN @CantidadCondiciones ELSE T.D2 END,
						T.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN @CantidadCondiciones ELSE T.D3 END,
						T.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN @CantidadCondiciones ELSE T.D4 END,
						T.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN @CantidadCondiciones ELSE T.D5 END,
						T.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN @CantidadCondiciones ELSE T.D6 END,
						T.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN @CantidadCondiciones ELSE T.D7 END,
						T.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN @CantidadCondiciones ELSE T.D8 END,
						T.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN @CantidadCondiciones ELSE T.D9 END,
						T.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN @CantidadCondiciones ELSE T.D10 END,
						T.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN @CantidadCondiciones ELSE T.D11 END,
						T.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN @CantidadCondiciones ELSE T.D12 END,
						T.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN @CantidadCondiciones ELSE T.D13 END,
						T.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN @CantidadCondiciones ELSE T.D14 END,
						T.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN @CantidadCondiciones ELSE T.D15 END,
						T.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN @CantidadCondiciones ELSE T.D16 END,
						T.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN @CantidadCondiciones ELSE T.D17 END,
						T.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN @CantidadCondiciones ELSE T.D18 END,
						T.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN @CantidadCondiciones ELSE T.D19 END,
						T.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN @CantidadCondiciones ELSE T.D20 END,
						T.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN @CantidadCondiciones ELSE T.D21 END,
						T.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN @CantidadCondiciones ELSE T.D22 END,
						T.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN @CantidadCondiciones ELSE T.D23 END,
						T.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN @CantidadCondiciones ELSE T.D24 END,
						T.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN @CantidadCondiciones ELSE T.D25 END,
						T.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN @CantidadCondiciones ELSE T.D26 END,
						T.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN @CantidadCondiciones ELSE T.D27 END,
						T.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN @CantidadCondiciones ELSE T.D28 END,
						T.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN @CantidadCondiciones ELSE T.D29 END,
						T.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN @CantidadCondiciones ELSE T.D30 END,
						T.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN @CantidadCondiciones ELSE T.D31 END
					FROM ReportesApp_Operaciones_Operatividad_ViewCortineras T
					WHERE T.Anio = YEAR(@x_FechaSelec) AND T.Mes = MONTH(@x_FechaSelec) AND T.idCondicionCR = @idCondicion
				END

				IF (@TipoUnidad = 'PLATAFORMA' AND (@idCondicion IN (1,2,3,4,16,5,6,10,8) OR @CondicionAnterior IN (1,2,3,4,16,5,6,10,8))) BEGIN
					SET @CantidadCondiciones = (SELECT COUNT(*) FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas C
												LEFT JOIN OP_TR_VEHICULO VH ON C.IdUnidad = VH.IdVehiculo
												LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = VH.SubTipoVehiculo
												WHERE C.Fecha = @x_FechaSelec AND C.idCondicion = @idCondicion AND LTRIM(RTRIM(SV.Descripcion)) = @TipoUnidad)

					UPDATE T 
					SET T.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN (CASE WHEN T.D1 - 1 = 0 THEN NULL ELSE T.D1 - 1 END) ELSE T.D1 END,
						T.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN (CASE WHEN T.D2 - 1 = 0 THEN NULL ELSE T.D2 - 1 END) ELSE T.D2 END,
						T.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN (CASE WHEN T.D3 - 1 = 0 THEN NULL ELSE T.D3 - 1 END) ELSE T.D3 END,
						T.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN (CASE WHEN T.D4 - 1 = 0 THEN NULL ELSE T.D4 - 1 END) ELSE T.D4 END,
						T.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN (CASE WHEN T.D5 - 1 = 0 THEN NULL ELSE T.D5 - 1 END) ELSE T.D5 END,
						T.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN (CASE WHEN T.D6 - 1 = 0 THEN NULL ELSE T.D6 - 1 END) ELSE T.D6 END,
						T.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN (CASE WHEN T.D7 - 1 = 0 THEN NULL ELSE T.D7 - 1 END) ELSE T.D7 END,
						T.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN (CASE WHEN T.D8 - 1 = 0 THEN NULL ELSE T.D8 - 1 END) ELSE T.D8 END,
						T.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN (CASE WHEN T.D9 - 1 = 0 THEN NULL ELSE T.D9 - 1 END) ELSE T.D9 END,
						T.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN (CASE WHEN T.D10 - 1 = 0 THEN NULL ELSE T.D10 - 1 END) ELSE T.D10 END,
						T.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN (CASE WHEN T.D11 - 1 = 0 THEN NULL ELSE T.D11 - 1 END) ELSE T.D11 END,
						T.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN (CASE WHEN T.D12 - 1 = 0 THEN NULL ELSE T.D12 - 1 END) ELSE T.D12 END,
						T.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN (CASE WHEN T.D13 - 1 = 0 THEN NULL ELSE T.D13 - 1 END) ELSE T.D13 END,
						T.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN (CASE WHEN T.D14 - 1 = 0 THEN NULL ELSE T.D14 - 1 END) ELSE T.D14 END,
						T.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN (CASE WHEN T.D15 - 1 = 0 THEN NULL ELSE T.D15 - 1 END) ELSE T.D15 END,
						T.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN (CASE WHEN T.D16 - 1 = 0 THEN NULL ELSE T.D16 - 1 END) ELSE T.D16 END,
						T.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN (CASE WHEN T.D17 - 1 = 0 THEN NULL ELSE T.D17 - 1 END) ELSE T.D17 END,
						T.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN (CASE WHEN T.D18 - 1 = 0 THEN NULL ELSE T.D18 - 1 END) ELSE T.D18 END,
						T.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN (CASE WHEN T.D19 - 1 = 0 THEN NULL ELSE T.D19 - 1 END) ELSE T.D19 END,
						T.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN (CASE WHEN T.D20 - 1 = 0 THEN NULL ELSE T.D20 - 1 END) ELSE T.D20 END,
						T.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN (CASE WHEN T.D21 - 1 = 0 THEN NULL ELSE T.D21 - 1 END) ELSE T.D21 END,
						T.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN (CASE WHEN T.D22 - 1 = 0 THEN NULL ELSE T.D22 - 1 END) ELSE T.D22 END,
						T.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN (CASE WHEN T.D23 - 1 = 0 THEN NULL ELSE T.D23 - 1 END) ELSE T.D23 END,
						T.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN (CASE WHEN T.D24 - 1 = 0 THEN NULL ELSE T.D24 - 1 END) ELSE T.D24 END,
						T.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN (CASE WHEN T.D25 - 1 = 0 THEN NULL ELSE T.D25 - 1 END) ELSE T.D25 END,
						T.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN (CASE WHEN T.D26 - 1 = 0 THEN NULL ELSE T.D26 - 1 END) ELSE T.D26 END,
						T.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN (CASE WHEN T.D27 - 1 = 0 THEN NULL ELSE T.D27 - 1 END) ELSE T.D27 END,
						T.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN (CASE WHEN T.D28 - 1 = 0 THEN NULL ELSE T.D28 - 1 END) ELSE T.D28 END,
						T.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN (CASE WHEN T.D29 - 1 = 0 THEN NULL ELSE T.D29 - 1 END) ELSE T.D29 END,
						T.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN (CASE WHEN T.D30 - 1 = 0 THEN NULL ELSE T.D30 - 1 END) ELSE T.D30 END,
						T.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN (CASE WHEN T.D31 - 1 = 0 THEN NULL ELSE T.D31 - 1 END) ELSE T.D31 END
					FROM ReportesApp_Operaciones_Operatividad_ViewPlataformas T
					WHERE T.Anio = YEAR(@x_FechaSelec) AND T.Mes = MONTH(@x_FechaSelec) AND T.idCondicionPL = @CondicionAnterior

					UPDATE T 
					SET T.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN @CantidadCondiciones ELSE T.D1 END,
						T.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN @CantidadCondiciones ELSE T.D2 END,
						T.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN @CantidadCondiciones ELSE T.D3 END,
						T.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN @CantidadCondiciones ELSE T.D4 END,
						T.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN @CantidadCondiciones ELSE T.D5 END,
						T.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN @CantidadCondiciones ELSE T.D6 END,
						T.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN @CantidadCondiciones ELSE T.D7 END,
						T.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN @CantidadCondiciones ELSE T.D8 END,
						T.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN @CantidadCondiciones ELSE T.D9 END,
						T.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN @CantidadCondiciones ELSE T.D10 END,
						T.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN @CantidadCondiciones ELSE T.D11 END,
						T.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN @CantidadCondiciones ELSE T.D12 END,
						T.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN @CantidadCondiciones ELSE T.D13 END,
						T.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN @CantidadCondiciones ELSE T.D14 END,
						T.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN @CantidadCondiciones ELSE T.D15 END,
						T.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN @CantidadCondiciones ELSE T.D16 END,
						T.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN @CantidadCondiciones ELSE T.D17 END,
						T.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN @CantidadCondiciones ELSE T.D18 END,
						T.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN @CantidadCondiciones ELSE T.D19 END,
						T.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN @CantidadCondiciones ELSE T.D20 END,
						T.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN @CantidadCondiciones ELSE T.D21 END,
						T.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN @CantidadCondiciones ELSE T.D22 END,
						T.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN @CantidadCondiciones ELSE T.D23 END,
						T.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN @CantidadCondiciones ELSE T.D24 END,
						T.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN @CantidadCondiciones ELSE T.D25 END,
						T.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN @CantidadCondiciones ELSE T.D26 END,
						T.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN @CantidadCondiciones ELSE T.D27 END,
						T.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN @CantidadCondiciones ELSE T.D28 END,
						T.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN @CantidadCondiciones ELSE T.D29 END,
						T.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN @CantidadCondiciones ELSE T.D30 END,
						T.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN @CantidadCondiciones ELSE T.D31 END
					FROM ReportesApp_Operaciones_Operatividad_ViewPlataformas T
					WHERE T.Anio = YEAR(@x_FechaSelec) AND T.Mes = MONTH(@x_FechaSelec) AND T.idCondicionPL = @idCondicion
				END

				IF (@TipoUnidad = 'TOLVA' AND (@idCondicion IN (1,2,3,4,16,5,6,10,8) OR @CondicionAnterior IN (1,2,3,4,16,5,6,10,8))) BEGIN
					SET @CantidadCondiciones = (SELECT COUNT(*) FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas C
												LEFT JOIN OP_TR_VEHICULO VH ON C.IdUnidad = VH.IdVehiculo
												LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = VH.SubTipoVehiculo
												WHERE C.Fecha = @x_FechaSelec AND C.idCondicion = @idCondicion AND LTRIM(RTRIM(SV.Descripcion)) = @TipoUnidad)

					UPDATE T 
					SET T.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN (CASE WHEN T.D1 - 1 = 0 THEN NULL ELSE T.D1 - 1 END) ELSE T.D1 END,
						T.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN (CASE WHEN T.D2 - 1 = 0 THEN NULL ELSE T.D2 - 1 END) ELSE T.D2 END,
						T.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN (CASE WHEN T.D3 - 1 = 0 THEN NULL ELSE T.D3 - 1 END) ELSE T.D3 END,
						T.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN (CASE WHEN T.D4 - 1 = 0 THEN NULL ELSE T.D4 - 1 END) ELSE T.D4 END,
						T.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN (CASE WHEN T.D5 - 1 = 0 THEN NULL ELSE T.D5 - 1 END) ELSE T.D5 END,
						T.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN (CASE WHEN T.D6 - 1 = 0 THEN NULL ELSE T.D6 - 1 END) ELSE T.D6 END,
						T.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN (CASE WHEN T.D7 - 1 = 0 THEN NULL ELSE T.D7 - 1 END) ELSE T.D7 END,
						T.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN (CASE WHEN T.D8 - 1 = 0 THEN NULL ELSE T.D8 - 1 END) ELSE T.D8 END,
						T.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN (CASE WHEN T.D9 - 1 = 0 THEN NULL ELSE T.D9 - 1 END) ELSE T.D9 END,
						T.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN (CASE WHEN T.D10 - 1 = 0 THEN NULL ELSE T.D10 - 1 END) ELSE T.D10 END,
						T.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN (CASE WHEN T.D11 - 1 = 0 THEN NULL ELSE T.D11 - 1 END) ELSE T.D11 END,
						T.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN (CASE WHEN T.D12 - 1 = 0 THEN NULL ELSE T.D12 - 1 END) ELSE T.D12 END,
						T.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN (CASE WHEN T.D13 - 1 = 0 THEN NULL ELSE T.D13 - 1 END) ELSE T.D13 END,
						T.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN (CASE WHEN T.D14 - 1 = 0 THEN NULL ELSE T.D14 - 1 END) ELSE T.D14 END,
						T.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN (CASE WHEN T.D15 - 1 = 0 THEN NULL ELSE T.D15 - 1 END) ELSE T.D15 END,
						T.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN (CASE WHEN T.D16 - 1 = 0 THEN NULL ELSE T.D16 - 1 END) ELSE T.D16 END,
						T.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN (CASE WHEN T.D17 - 1 = 0 THEN NULL ELSE T.D17 - 1 END) ELSE T.D17 END,
						T.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN (CASE WHEN T.D18 - 1 = 0 THEN NULL ELSE T.D18 - 1 END) ELSE T.D18 END,
						T.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN (CASE WHEN T.D19 - 1 = 0 THEN NULL ELSE T.D19 - 1 END) ELSE T.D19 END,
						T.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN (CASE WHEN T.D20 - 1 = 0 THEN NULL ELSE T.D20 - 1 END) ELSE T.D20 END,
						T.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN (CASE WHEN T.D21 - 1 = 0 THEN NULL ELSE T.D21 - 1 END) ELSE T.D21 END,
						T.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN (CASE WHEN T.D22 - 1 = 0 THEN NULL ELSE T.D22 - 1 END) ELSE T.D22 END,
						T.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN (CASE WHEN T.D23 - 1 = 0 THEN NULL ELSE T.D23 - 1 END) ELSE T.D23 END,
						T.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN (CASE WHEN T.D24 - 1 = 0 THEN NULL ELSE T.D24 - 1 END) ELSE T.D24 END,
						T.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN (CASE WHEN T.D25 - 1 = 0 THEN NULL ELSE T.D25 - 1 END) ELSE T.D25 END,
						T.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN (CASE WHEN T.D26 - 1 = 0 THEN NULL ELSE T.D26 - 1 END) ELSE T.D26 END,
						T.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN (CASE WHEN T.D27 - 1 = 0 THEN NULL ELSE T.D27 - 1 END) ELSE T.D27 END,
						T.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN (CASE WHEN T.D28 - 1 = 0 THEN NULL ELSE T.D28 - 1 END) ELSE T.D28 END,
						T.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN (CASE WHEN T.D29 - 1 = 0 THEN NULL ELSE T.D29 - 1 END) ELSE T.D29 END,
						T.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN (CASE WHEN T.D30 - 1 = 0 THEN NULL ELSE T.D30 - 1 END) ELSE T.D30 END,
						T.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN (CASE WHEN T.D31 - 1 = 0 THEN NULL ELSE T.D31 - 1 END) ELSE T.D31 END
					FROM ReportesApp_Operaciones_Operatividad_ViewTolvas T
					WHERE T.Anio = YEAR(@x_FechaSelec) AND T.Mes = MONTH(@x_FechaSelec) AND T.idCondicionTL = @CondicionAnterior

					UPDATE T 
					SET T.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN @CantidadCondiciones ELSE T.D1 END,
						T.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN @CantidadCondiciones ELSE T.D2 END,
						T.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN @CantidadCondiciones ELSE T.D3 END,
						T.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN @CantidadCondiciones ELSE T.D4 END,
						T.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN @CantidadCondiciones ELSE T.D5 END,
						T.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN @CantidadCondiciones ELSE T.D6 END,
						T.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN @CantidadCondiciones ELSE T.D7 END,
						T.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN @CantidadCondiciones ELSE T.D8 END,
						T.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN @CantidadCondiciones ELSE T.D9 END,
						T.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN @CantidadCondiciones ELSE T.D10 END,
						T.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN @CantidadCondiciones ELSE T.D11 END,
						T.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN @CantidadCondiciones ELSE T.D12 END,
						T.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN @CantidadCondiciones ELSE T.D13 END,
						T.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN @CantidadCondiciones ELSE T.D14 END,
						T.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN @CantidadCondiciones ELSE T.D15 END,
						T.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN @CantidadCondiciones ELSE T.D16 END,
						T.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN @CantidadCondiciones ELSE T.D17 END,
						T.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN @CantidadCondiciones ELSE T.D18 END,
						T.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN @CantidadCondiciones ELSE T.D19 END,
						T.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN @CantidadCondiciones ELSE T.D20 END,
						T.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN @CantidadCondiciones ELSE T.D21 END,
						T.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN @CantidadCondiciones ELSE T.D22 END,
						T.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN @CantidadCondiciones ELSE T.D23 END,
						T.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN @CantidadCondiciones ELSE T.D24 END,
						T.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN @CantidadCondiciones ELSE T.D25 END,
						T.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN @CantidadCondiciones ELSE T.D26 END,
						T.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN @CantidadCondiciones ELSE T.D27 END,
						T.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN @CantidadCondiciones ELSE T.D28 END,
						T.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN @CantidadCondiciones ELSE T.D29 END,
						T.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN @CantidadCondiciones ELSE T.D30 END,
						T.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN @CantidadCondiciones ELSE T.D31 END
					FROM ReportesApp_Operaciones_Operatividad_ViewTolvas T
					WHERE T.Anio = YEAR(@x_FechaSelec) AND T.Mes = MONTH(@x_FechaSelec) AND T.idCondicionTL = @idCondicion
				END

				IF (@TipoUnidad = 'CISTERNA' AND (@idCondicion IN (1,2,3,4,16,5,6,10,8) OR @CondicionAnterior IN (1,2,3,4,16,5,6,10,8))) BEGIN
					SET @CantidadCondiciones = (SELECT COUNT(*) FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas C
												LEFT JOIN OP_TR_VEHICULO VH ON C.IdUnidad = VH.IdVehiculo
												LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = VH.SubTipoVehiculo
												WHERE C.Fecha = @x_FechaSelec AND C.idCondicion = @idCondicion AND LTRIM(RTRIM(SV.Descripcion)) = @TipoUnidad)

					UPDATE T 
					SET T.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN (CASE WHEN T.D1 - 1 = 0 THEN NULL ELSE T.D1 - 1 END) ELSE T.D1 END,
						T.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN (CASE WHEN T.D2 - 1 = 0 THEN NULL ELSE T.D2 - 1 END) ELSE T.D2 END,
						T.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN (CASE WHEN T.D3 - 1 = 0 THEN NULL ELSE T.D3 - 1 END) ELSE T.D3 END,
						T.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN (CASE WHEN T.D4 - 1 = 0 THEN NULL ELSE T.D4 - 1 END) ELSE T.D4 END,
						T.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN (CASE WHEN T.D5 - 1 = 0 THEN NULL ELSE T.D5 - 1 END) ELSE T.D5 END,
						T.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN (CASE WHEN T.D6 - 1 = 0 THEN NULL ELSE T.D6 - 1 END) ELSE T.D6 END,
						T.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN (CASE WHEN T.D7 - 1 = 0 THEN NULL ELSE T.D7 - 1 END) ELSE T.D7 END,
						T.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN (CASE WHEN T.D8 - 1 = 0 THEN NULL ELSE T.D8 - 1 END) ELSE T.D8 END,
						T.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN (CASE WHEN T.D9 - 1 = 0 THEN NULL ELSE T.D9 - 1 END) ELSE T.D9 END,
						T.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN (CASE WHEN T.D10 - 1 = 0 THEN NULL ELSE T.D10 - 1 END) ELSE T.D10 END,
						T.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN (CASE WHEN T.D11 - 1 = 0 THEN NULL ELSE T.D11 - 1 END) ELSE T.D11 END,
						T.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN (CASE WHEN T.D12 - 1 = 0 THEN NULL ELSE T.D12 - 1 END) ELSE T.D12 END,
						T.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN (CASE WHEN T.D13 - 1 = 0 THEN NULL ELSE T.D13 - 1 END) ELSE T.D13 END,
						T.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN (CASE WHEN T.D14 - 1 = 0 THEN NULL ELSE T.D14 - 1 END) ELSE T.D14 END,
						T.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN (CASE WHEN T.D15 - 1 = 0 THEN NULL ELSE T.D15 - 1 END) ELSE T.D15 END,
						T.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN (CASE WHEN T.D16 - 1 = 0 THEN NULL ELSE T.D16 - 1 END) ELSE T.D16 END,
						T.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN (CASE WHEN T.D17 - 1 = 0 THEN NULL ELSE T.D17 - 1 END) ELSE T.D17 END,
						T.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN (CASE WHEN T.D18 - 1 = 0 THEN NULL ELSE T.D18 - 1 END) ELSE T.D18 END,
						T.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN (CASE WHEN T.D19 - 1 = 0 THEN NULL ELSE T.D19 - 1 END) ELSE T.D19 END,
						T.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN (CASE WHEN T.D20 - 1 = 0 THEN NULL ELSE T.D20 - 1 END) ELSE T.D20 END,
						T.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN (CASE WHEN T.D21 - 1 = 0 THEN NULL ELSE T.D21 - 1 END) ELSE T.D21 END,
						T.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN (CASE WHEN T.D22 - 1 = 0 THEN NULL ELSE T.D22 - 1 END) ELSE T.D22 END,
						T.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN (CASE WHEN T.D23 - 1 = 0 THEN NULL ELSE T.D23 - 1 END) ELSE T.D23 END,
						T.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN (CASE WHEN T.D24 - 1 = 0 THEN NULL ELSE T.D24 - 1 END) ELSE T.D24 END,
						T.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN (CASE WHEN T.D25 - 1 = 0 THEN NULL ELSE T.D25 - 1 END) ELSE T.D25 END,
						T.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN (CASE WHEN T.D26 - 1 = 0 THEN NULL ELSE T.D26 - 1 END) ELSE T.D26 END,
						T.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN (CASE WHEN T.D27 - 1 = 0 THEN NULL ELSE T.D27 - 1 END) ELSE T.D27 END,
						T.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN (CASE WHEN T.D28 - 1 = 0 THEN NULL ELSE T.D28 - 1 END) ELSE T.D28 END,
						T.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN (CASE WHEN T.D29 - 1 = 0 THEN NULL ELSE T.D29 - 1 END) ELSE T.D29 END,
						T.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN (CASE WHEN T.D30 - 1 = 0 THEN NULL ELSE T.D30 - 1 END) ELSE T.D30 END,
						T.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN (CASE WHEN T.D31 - 1 = 0 THEN NULL ELSE T.D31 - 1 END) ELSE T.D31 END
					FROM ReportesApp_Operaciones_Operatividad_ViewCisternas T
					WHERE T.Anio = YEAR(@x_FechaSelec) AND T.Mes = MONTH(@x_FechaSelec) AND T.idCondicionCS = @CondicionAnterior

					UPDATE T 
					SET T.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN @CantidadCondiciones ELSE T.D1 END,
						T.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN @CantidadCondiciones ELSE T.D2 END,
						T.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN @CantidadCondiciones ELSE T.D3 END,
						T.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN @CantidadCondiciones ELSE T.D4 END,
						T.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN @CantidadCondiciones ELSE T.D5 END,
						T.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN @CantidadCondiciones ELSE T.D6 END,
						T.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN @CantidadCondiciones ELSE T.D7 END,
						T.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN @CantidadCondiciones ELSE T.D8 END,
						T.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN @CantidadCondiciones ELSE T.D9 END,
						T.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN @CantidadCondiciones ELSE T.D10 END,
						T.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN @CantidadCondiciones ELSE T.D11 END,
						T.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN @CantidadCondiciones ELSE T.D12 END,
						T.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN @CantidadCondiciones ELSE T.D13 END,
						T.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN @CantidadCondiciones ELSE T.D14 END,
						T.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN @CantidadCondiciones ELSE T.D15 END,
						T.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN @CantidadCondiciones ELSE T.D16 END,
						T.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN @CantidadCondiciones ELSE T.D17 END,
						T.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN @CantidadCondiciones ELSE T.D18 END,
						T.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN @CantidadCondiciones ELSE T.D19 END,
						T.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN @CantidadCondiciones ELSE T.D20 END,
						T.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN @CantidadCondiciones ELSE T.D21 END,
						T.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN @CantidadCondiciones ELSE T.D22 END,
						T.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN @CantidadCondiciones ELSE T.D23 END,
						T.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN @CantidadCondiciones ELSE T.D24 END,
						T.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN @CantidadCondiciones ELSE T.D25 END,
						T.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN @CantidadCondiciones ELSE T.D26 END,
						T.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN @CantidadCondiciones ELSE T.D27 END,
						T.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN @CantidadCondiciones ELSE T.D28 END,
						T.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN @CantidadCondiciones ELSE T.D29 END,
						T.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN @CantidadCondiciones ELSE T.D30 END,
						T.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN @CantidadCondiciones ELSE T.D31 END
					FROM ReportesApp_Operaciones_Operatividad_ViewCisternas T
					WHERE T.Anio = YEAR(@x_FechaSelec) AND T.Mes = MONTH(@x_FechaSelec) AND T.idCondicionCS = @idCondicion
				END
			END
		
			SET @FechaInicio = DATEADD(DAY,1,@FechaInicio)
		END

		SET @exito = '0 = Reporte actualizado'
	END
	ELSE BEGIN
		SET @exito = '-1 = No se pudo registrar el reporte'
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

-----------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 09-09-2024
-- Description:	GUARDAR VIAJE ESTADO - TOLVAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Operatividad_ActualizarViajeTolvasCarretas]
@NroTicket INT,
@IDTracto INT,
@FechaInicio DATE,
@Usuario VARCHAR(80)
AS	
DECLARE @exito VARCHAR(MAX)	

BEGIN TRAN
BEGIN TRY
	IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_Operatividad_CarretasView WHERE IdUnidad = @IDTracto AND Mes = MONTH(@FechaInicio) AND Anio = YEAR(@FechaInicio))) BEGIN
		DECLARE @correlativo INT
		DECLARE @idCondicion INT = 3
		DECLARE @CondicionAnterior INT = (SELECT idCondicion FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas WHERE IdUnidad = @IDTracto AND Fecha = @FechaInicio)

		DELETE FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas
		WHERE IdUnidad = @IDTracto AND Fecha = @FechaInicio

		SET @correlativo = (SELECT MAX(idOperatividad) FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas)
		SET @correlativo = ISNULL(@correlativo,0) + 1

		INSERT INTO ReportesApp_Operaciones_Operatividad_RegistroCarretas(idOperatividad,IdUnidad,Fecha,idCondicion,IdOperacion,UsuarioRegistra,FechaRegistra)
		VALUES(@correlativo, @IDTracto, @FechaInicio, @idCondicion, 1, @Usuario, GETDATE())

		DECLARE @x_FechaSelec DATE = @FechaInicio
		DECLARE @x_DiaSelec INT = (SELECT DAY(@FechaInicio))
		DECLARE @Condicion VARCHAR(50) = (SELECT C.Codigo FROM ReportesApp_Operaciones_Operatividad_Condiciones C WHERE C.idCondicion = @idCondicion)
		DECLARE @CantidadCondiciones INT = (SELECT COUNT(*) FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas WHERE Fecha = @x_FechaSelec AND idCondicion = @idCondicion)
		DECLARE @TipoUnidad VARCHAR(150) = (SELECT LTRIM(RTRIM(SV.Descripcion)) FROM OP_TR_VEHICULO VH
												LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = VH.SubTipoVehiculo
												WHERE VH.IdVehiculo = @IDTracto)

		IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_Operatividad_CarretasView WHERE Anio = YEAR(@x_FechaSelec) AND Mes = MONTH(@x_FechaSelec) AND IdUnidad = @IDTracto)) BEGIN
			UPDATE P 
			SET P.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN @Condicion ELSE P.D1 END,
				P.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN @Condicion ELSE P.D2 END,
				P.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN @Condicion ELSE P.D3 END,
				P.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN @Condicion ELSE P.D4 END,
				P.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN @Condicion ELSE P.D5 END,
				P.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN @Condicion ELSE P.D6 END,
				P.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN @Condicion ELSE P.D7 END,
				P.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN @Condicion ELSE P.D8 END,
				P.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN @Condicion ELSE P.D9 END,
				P.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN @Condicion ELSE P.D10 END,
				P.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN @Condicion ELSE P.D11 END,
				P.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN @Condicion ELSE P.D12 END,
				P.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN @Condicion ELSE P.D13 END,
				P.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN @Condicion ELSE P.D14 END,
				P.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN @Condicion ELSE P.D15 END,
				P.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN @Condicion ELSE P.D16 END,
				P.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN @Condicion ELSE P.D17 END,
				P.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN @Condicion ELSE P.D18 END,
				P.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN @Condicion ELSE P.D19 END,
				P.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN @Condicion ELSE P.D20 END,
				P.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN @Condicion ELSE P.D21 END,
				P.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN @Condicion ELSE P.D22 END,
				P.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN @Condicion ELSE P.D23 END,
				P.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN @Condicion ELSE P.D24 END,
				P.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN @Condicion ELSE P.D25 END,
				P.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN @Condicion ELSE P.D26 END,
				P.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN @Condicion ELSE P.D27 END,
				P.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN @Condicion ELSE P.D28 END,
				P.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN @Condicion ELSE P.D29 END,
				P.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN @Condicion ELSE P.D30 END,
				P.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN @Condicion ELSE P.D31 END
			FROM ReportesApp_Operaciones_Operatividad_CarretasView P
			WHERE P.Anio = YEAR(@x_FechaSelec) AND P.Mes = MONTH(@x_FechaSelec) AND P.IdUnidad = @IDTracto
		END

		IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_Operatividad_ViewCarretas WHERE Anio = YEAR(@x_FechaSelec) AND Mes = MONTH(@x_FechaSelec))) BEGIN
			UPDATE T 
			SET T.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN (CASE WHEN T.D1 - 1 = 0 THEN NULL ELSE T.D1 - 1 END) ELSE T.D1 END,
				T.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN (CASE WHEN T.D2 - 1 = 0 THEN NULL ELSE T.D2 - 1 END) ELSE T.D2 END,
				T.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN (CASE WHEN T.D3 - 1 = 0 THEN NULL ELSE T.D3 - 1 END) ELSE T.D3 END,
				T.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN (CASE WHEN T.D4 - 1 = 0 THEN NULL ELSE T.D4 - 1 END) ELSE T.D4 END,
				T.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN (CASE WHEN T.D5 - 1 = 0 THEN NULL ELSE T.D5 - 1 END) ELSE T.D5 END,
				T.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN (CASE WHEN T.D6 - 1 = 0 THEN NULL ELSE T.D6 - 1 END) ELSE T.D6 END,
				T.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN (CASE WHEN T.D7 - 1 = 0 THEN NULL ELSE T.D7 - 1 END) ELSE T.D7 END,
				T.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN (CASE WHEN T.D8 - 1 = 0 THEN NULL ELSE T.D8 - 1 END) ELSE T.D8 END,
				T.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN (CASE WHEN T.D9 - 1 = 0 THEN NULL ELSE T.D9 - 1 END) ELSE T.D9 END,
				T.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN (CASE WHEN T.D10 - 1 = 0 THEN NULL ELSE T.D10 - 1 END) ELSE T.D10 END,
				T.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN (CASE WHEN T.D11 - 1 = 0 THEN NULL ELSE T.D11 - 1 END) ELSE T.D11 END,
				T.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN (CASE WHEN T.D12 - 1 = 0 THEN NULL ELSE T.D12 - 1 END) ELSE T.D12 END,
				T.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN (CASE WHEN T.D13 - 1 = 0 THEN NULL ELSE T.D13 - 1 END) ELSE T.D13 END,
				T.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN (CASE WHEN T.D14 - 1 = 0 THEN NULL ELSE T.D14 - 1 END) ELSE T.D14 END,
				T.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN (CASE WHEN T.D15 - 1 = 0 THEN NULL ELSE T.D15 - 1 END) ELSE T.D15 END,
				T.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN (CASE WHEN T.D16 - 1 = 0 THEN NULL ELSE T.D16 - 1 END) ELSE T.D16 END,
				T.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN (CASE WHEN T.D17 - 1 = 0 THEN NULL ELSE T.D17 - 1 END) ELSE T.D17 END,
				T.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN (CASE WHEN T.D18 - 1 = 0 THEN NULL ELSE T.D18 - 1 END) ELSE T.D18 END,
				T.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN (CASE WHEN T.D19 - 1 = 0 THEN NULL ELSE T.D19 - 1 END) ELSE T.D19 END,
				T.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN (CASE WHEN T.D20 - 1 = 0 THEN NULL ELSE T.D20 - 1 END) ELSE T.D20 END,
				T.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN (CASE WHEN T.D21 - 1 = 0 THEN NULL ELSE T.D21 - 1 END) ELSE T.D21 END,
				T.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN (CASE WHEN T.D22 - 1 = 0 THEN NULL ELSE T.D22 - 1 END) ELSE T.D22 END,
				T.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN (CASE WHEN T.D23 - 1 = 0 THEN NULL ELSE T.D23 - 1 END) ELSE T.D23 END,
				T.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN (CASE WHEN T.D24 - 1 = 0 THEN NULL ELSE T.D24 - 1 END) ELSE T.D24 END,
				T.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN (CASE WHEN T.D25 - 1 = 0 THEN NULL ELSE T.D25 - 1 END) ELSE T.D25 END,
				T.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN (CASE WHEN T.D26 - 1 = 0 THEN NULL ELSE T.D26 - 1 END) ELSE T.D26 END,
				T.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN (CASE WHEN T.D27 - 1 = 0 THEN NULL ELSE T.D27 - 1 END) ELSE T.D27 END,
				T.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN (CASE WHEN T.D28 - 1 = 0 THEN NULL ELSE T.D28 - 1 END) ELSE T.D28 END,
				T.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN (CASE WHEN T.D29 - 1 = 0 THEN NULL ELSE T.D29 - 1 END) ELSE T.D29 END,
				T.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN (CASE WHEN T.D30 - 1 = 0 THEN NULL ELSE T.D30 - 1 END) ELSE T.D30 END,
				T.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN (CASE WHEN T.D31 - 1 = 0 THEN NULL ELSE T.D31 - 1 END) ELSE T.D31 END
			FROM ReportesApp_Operaciones_Operatividad_ViewCarretas T
			WHERE T.Anio = YEAR(@x_FechaSelec) AND T.Mes = MONTH(@x_FechaSelec) AND T.idCondicion = @CondicionAnterior

			UPDATE T 
			SET T.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN @CantidadCondiciones ELSE T.D1 END,
				T.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN @CantidadCondiciones ELSE T.D2 END,
				T.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN @CantidadCondiciones ELSE T.D3 END,
				T.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN @CantidadCondiciones ELSE T.D4 END,
				T.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN @CantidadCondiciones ELSE T.D5 END,
				T.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN @CantidadCondiciones ELSE T.D6 END,
				T.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN @CantidadCondiciones ELSE T.D7 END,
				T.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN @CantidadCondiciones ELSE T.D8 END,
				T.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN @CantidadCondiciones ELSE T.D9 END,
				T.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN @CantidadCondiciones ELSE T.D10 END,
				T.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN @CantidadCondiciones ELSE T.D11 END,
				T.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN @CantidadCondiciones ELSE T.D12 END,
				T.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN @CantidadCondiciones ELSE T.D13 END,
				T.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN @CantidadCondiciones ELSE T.D14 END,
				T.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN @CantidadCondiciones ELSE T.D15 END,
				T.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN @CantidadCondiciones ELSE T.D16 END,
				T.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN @CantidadCondiciones ELSE T.D17 END,
				T.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN @CantidadCondiciones ELSE T.D18 END,
				T.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN @CantidadCondiciones ELSE T.D19 END,
				T.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN @CantidadCondiciones ELSE T.D20 END,
				T.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN @CantidadCondiciones ELSE T.D21 END,
				T.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN @CantidadCondiciones ELSE T.D22 END,
				T.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN @CantidadCondiciones ELSE T.D23 END,
				T.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN @CantidadCondiciones ELSE T.D24 END,
				T.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN @CantidadCondiciones ELSE T.D25 END,
				T.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN @CantidadCondiciones ELSE T.D26 END,
				T.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN @CantidadCondiciones ELSE T.D27 END,
				T.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN @CantidadCondiciones ELSE T.D28 END,
				T.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN @CantidadCondiciones ELSE T.D29 END,
				T.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN @CantidadCondiciones ELSE T.D30 END,
				T.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN @CantidadCondiciones ELSE T.D31 END
			FROM ReportesApp_Operaciones_Operatividad_ViewCarretas T
			WHERE T.Anio = YEAR(@x_FechaSelec) AND T.Mes = MONTH(@x_FechaSelec) AND T.idCondicion = @idCondicion

			IF (@TipoUnidad = 'CORTINERA' AND (@idCondicion IN (1,2,3,4,16,5,6,13,10,8) OR @CondicionAnterior IN (1,2,3,4,16,5,6,13,10,8))) BEGIN
				SET @CantidadCondiciones = (SELECT COUNT(*) FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas C
											LEFT JOIN OP_TR_VEHICULO VH ON C.IdUnidad = VH.IdVehiculo
											LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = VH.SubTipoVehiculo
											WHERE C.Fecha = @x_FechaSelec AND C.idCondicion = @idCondicion AND LTRIM(RTRIM(SV.Descripcion)) = @TipoUnidad)

				UPDATE T 
				SET T.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN (CASE WHEN T.D1 - 1 = 0 THEN NULL ELSE T.D1 - 1 END) ELSE T.D1 END,
					T.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN (CASE WHEN T.D2 - 1 = 0 THEN NULL ELSE T.D2 - 1 END) ELSE T.D2 END,
					T.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN (CASE WHEN T.D3 - 1 = 0 THEN NULL ELSE T.D3 - 1 END) ELSE T.D3 END,
					T.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN (CASE WHEN T.D4 - 1 = 0 THEN NULL ELSE T.D4 - 1 END) ELSE T.D4 END,
					T.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN (CASE WHEN T.D5 - 1 = 0 THEN NULL ELSE T.D5 - 1 END) ELSE T.D5 END,
					T.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN (CASE WHEN T.D6 - 1 = 0 THEN NULL ELSE T.D6 - 1 END) ELSE T.D6 END,
					T.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN (CASE WHEN T.D7 - 1 = 0 THEN NULL ELSE T.D7 - 1 END) ELSE T.D7 END,
					T.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN (CASE WHEN T.D8 - 1 = 0 THEN NULL ELSE T.D8 - 1 END) ELSE T.D8 END,
					T.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN (CASE WHEN T.D9 - 1 = 0 THEN NULL ELSE T.D9 - 1 END) ELSE T.D9 END,
					T.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN (CASE WHEN T.D10 - 1 = 0 THEN NULL ELSE T.D10 - 1 END) ELSE T.D10 END,
					T.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN (CASE WHEN T.D11 - 1 = 0 THEN NULL ELSE T.D11 - 1 END) ELSE T.D11 END,
					T.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN (CASE WHEN T.D12 - 1 = 0 THEN NULL ELSE T.D12 - 1 END) ELSE T.D12 END,
					T.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN (CASE WHEN T.D13 - 1 = 0 THEN NULL ELSE T.D13 - 1 END) ELSE T.D13 END,
					T.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN (CASE WHEN T.D14 - 1 = 0 THEN NULL ELSE T.D14 - 1 END) ELSE T.D14 END,
					T.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN (CASE WHEN T.D15 - 1 = 0 THEN NULL ELSE T.D15 - 1 END) ELSE T.D15 END,
					T.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN (CASE WHEN T.D16 - 1 = 0 THEN NULL ELSE T.D16 - 1 END) ELSE T.D16 END,
					T.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN (CASE WHEN T.D17 - 1 = 0 THEN NULL ELSE T.D17 - 1 END) ELSE T.D17 END,
					T.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN (CASE WHEN T.D18 - 1 = 0 THEN NULL ELSE T.D18 - 1 END) ELSE T.D18 END,
					T.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN (CASE WHEN T.D19 - 1 = 0 THEN NULL ELSE T.D19 - 1 END) ELSE T.D19 END,
					T.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN (CASE WHEN T.D20 - 1 = 0 THEN NULL ELSE T.D20 - 1 END) ELSE T.D20 END,
					T.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN (CASE WHEN T.D21 - 1 = 0 THEN NULL ELSE T.D21 - 1 END) ELSE T.D21 END,
					T.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN (CASE WHEN T.D22 - 1 = 0 THEN NULL ELSE T.D22 - 1 END) ELSE T.D22 END,
					T.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN (CASE WHEN T.D23 - 1 = 0 THEN NULL ELSE T.D23 - 1 END) ELSE T.D23 END,
					T.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN (CASE WHEN T.D24 - 1 = 0 THEN NULL ELSE T.D24 - 1 END) ELSE T.D24 END,
					T.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN (CASE WHEN T.D25 - 1 = 0 THEN NULL ELSE T.D25 - 1 END) ELSE T.D25 END,
					T.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN (CASE WHEN T.D26 - 1 = 0 THEN NULL ELSE T.D26 - 1 END) ELSE T.D26 END,
					T.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN (CASE WHEN T.D27 - 1 = 0 THEN NULL ELSE T.D27 - 1 END) ELSE T.D27 END,
					T.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN (CASE WHEN T.D28 - 1 = 0 THEN NULL ELSE T.D28 - 1 END) ELSE T.D28 END,
					T.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN (CASE WHEN T.D29 - 1 = 0 THEN NULL ELSE T.D29 - 1 END) ELSE T.D29 END,
					T.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN (CASE WHEN T.D30 - 1 = 0 THEN NULL ELSE T.D30 - 1 END) ELSE T.D30 END,
					T.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN (CASE WHEN T.D31 - 1 = 0 THEN NULL ELSE T.D31 - 1 END) ELSE T.D31 END
				FROM ReportesApp_Operaciones_Operatividad_ViewCortineras T
				WHERE T.Anio = YEAR(@x_FechaSelec) AND T.Mes = MONTH(@x_FechaSelec) AND T.idCondicionCR = @CondicionAnterior

				UPDATE T 
				SET T.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN @CantidadCondiciones ELSE T.D1 END,
					T.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN @CantidadCondiciones ELSE T.D2 END,
					T.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN @CantidadCondiciones ELSE T.D3 END,
					T.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN @CantidadCondiciones ELSE T.D4 END,
					T.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN @CantidadCondiciones ELSE T.D5 END,
					T.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN @CantidadCondiciones ELSE T.D6 END,
					T.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN @CantidadCondiciones ELSE T.D7 END,
					T.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN @CantidadCondiciones ELSE T.D8 END,
					T.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN @CantidadCondiciones ELSE T.D9 END,
					T.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN @CantidadCondiciones ELSE T.D10 END,
					T.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN @CantidadCondiciones ELSE T.D11 END,
					T.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN @CantidadCondiciones ELSE T.D12 END,
					T.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN @CantidadCondiciones ELSE T.D13 END,
					T.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN @CantidadCondiciones ELSE T.D14 END,
					T.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN @CantidadCondiciones ELSE T.D15 END,
					T.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN @CantidadCondiciones ELSE T.D16 END,
					T.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN @CantidadCondiciones ELSE T.D17 END,
					T.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN @CantidadCondiciones ELSE T.D18 END,
					T.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN @CantidadCondiciones ELSE T.D19 END,
					T.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN @CantidadCondiciones ELSE T.D20 END,
					T.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN @CantidadCondiciones ELSE T.D21 END,
					T.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN @CantidadCondiciones ELSE T.D22 END,
					T.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN @CantidadCondiciones ELSE T.D23 END,
					T.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN @CantidadCondiciones ELSE T.D24 END,
					T.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN @CantidadCondiciones ELSE T.D25 END,
					T.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN @CantidadCondiciones ELSE T.D26 END,
					T.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN @CantidadCondiciones ELSE T.D27 END,
					T.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN @CantidadCondiciones ELSE T.D28 END,
					T.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN @CantidadCondiciones ELSE T.D29 END,
					T.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN @CantidadCondiciones ELSE T.D30 END,
					T.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN @CantidadCondiciones ELSE T.D31 END
				FROM ReportesApp_Operaciones_Operatividad_ViewCortineras T
				WHERE T.Anio = YEAR(@x_FechaSelec) AND T.Mes = MONTH(@x_FechaSelec) AND T.idCondicionCR = @idCondicion
			END

			IF (@TipoUnidad = 'PLATAFORMA' AND (@idCondicion IN (1,2,3,4,16,5,6,10,8) OR @CondicionAnterior IN (1,2,3,4,16,5,6,10,8))) BEGIN
				SET @CantidadCondiciones = (SELECT COUNT(*) FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas C
											LEFT JOIN OP_TR_VEHICULO VH ON C.IdUnidad = VH.IdVehiculo
											LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = VH.SubTipoVehiculo
											WHERE C.Fecha = @x_FechaSelec AND C.idCondicion = @idCondicion AND LTRIM(RTRIM(SV.Descripcion)) = @TipoUnidad)

				UPDATE T 
				SET T.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN (CASE WHEN T.D1 - 1 = 0 THEN NULL ELSE T.D1 - 1 END) ELSE T.D1 END,
					T.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN (CASE WHEN T.D2 - 1 = 0 THEN NULL ELSE T.D2 - 1 END) ELSE T.D2 END,
					T.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN (CASE WHEN T.D3 - 1 = 0 THEN NULL ELSE T.D3 - 1 END) ELSE T.D3 END,
					T.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN (CASE WHEN T.D4 - 1 = 0 THEN NULL ELSE T.D4 - 1 END) ELSE T.D4 END,
					T.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN (CASE WHEN T.D5 - 1 = 0 THEN NULL ELSE T.D5 - 1 END) ELSE T.D5 END,
					T.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN (CASE WHEN T.D6 - 1 = 0 THEN NULL ELSE T.D6 - 1 END) ELSE T.D6 END,
					T.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN (CASE WHEN T.D7 - 1 = 0 THEN NULL ELSE T.D7 - 1 END) ELSE T.D7 END,
					T.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN (CASE WHEN T.D8 - 1 = 0 THEN NULL ELSE T.D8 - 1 END) ELSE T.D8 END,
					T.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN (CASE WHEN T.D9 - 1 = 0 THEN NULL ELSE T.D9 - 1 END) ELSE T.D9 END,
					T.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN (CASE WHEN T.D10 - 1 = 0 THEN NULL ELSE T.D10 - 1 END) ELSE T.D10 END,
					T.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN (CASE WHEN T.D11 - 1 = 0 THEN NULL ELSE T.D11 - 1 END) ELSE T.D11 END,
					T.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN (CASE WHEN T.D12 - 1 = 0 THEN NULL ELSE T.D12 - 1 END) ELSE T.D12 END,
					T.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN (CASE WHEN T.D13 - 1 = 0 THEN NULL ELSE T.D13 - 1 END) ELSE T.D13 END,
					T.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN (CASE WHEN T.D14 - 1 = 0 THEN NULL ELSE T.D14 - 1 END) ELSE T.D14 END,
					T.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN (CASE WHEN T.D15 - 1 = 0 THEN NULL ELSE T.D15 - 1 END) ELSE T.D15 END,
					T.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN (CASE WHEN T.D16 - 1 = 0 THEN NULL ELSE T.D16 - 1 END) ELSE T.D16 END,
					T.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN (CASE WHEN T.D17 - 1 = 0 THEN NULL ELSE T.D17 - 1 END) ELSE T.D17 END,
					T.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN (CASE WHEN T.D18 - 1 = 0 THEN NULL ELSE T.D18 - 1 END) ELSE T.D18 END,
					T.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN (CASE WHEN T.D19 - 1 = 0 THEN NULL ELSE T.D19 - 1 END) ELSE T.D19 END,
					T.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN (CASE WHEN T.D20 - 1 = 0 THEN NULL ELSE T.D20 - 1 END) ELSE T.D20 END,
					T.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN (CASE WHEN T.D21 - 1 = 0 THEN NULL ELSE T.D21 - 1 END) ELSE T.D21 END,
					T.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN (CASE WHEN T.D22 - 1 = 0 THEN NULL ELSE T.D22 - 1 END) ELSE T.D22 END,
					T.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN (CASE WHEN T.D23 - 1 = 0 THEN NULL ELSE T.D23 - 1 END) ELSE T.D23 END,
					T.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN (CASE WHEN T.D24 - 1 = 0 THEN NULL ELSE T.D24 - 1 END) ELSE T.D24 END,
					T.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN (CASE WHEN T.D25 - 1 = 0 THEN NULL ELSE T.D25 - 1 END) ELSE T.D25 END,
					T.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN (CASE WHEN T.D26 - 1 = 0 THEN NULL ELSE T.D26 - 1 END) ELSE T.D26 END,
					T.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN (CASE WHEN T.D27 - 1 = 0 THEN NULL ELSE T.D27 - 1 END) ELSE T.D27 END,
					T.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN (CASE WHEN T.D28 - 1 = 0 THEN NULL ELSE T.D28 - 1 END) ELSE T.D28 END,
					T.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN (CASE WHEN T.D29 - 1 = 0 THEN NULL ELSE T.D29 - 1 END) ELSE T.D29 END,
					T.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN (CASE WHEN T.D30 - 1 = 0 THEN NULL ELSE T.D30 - 1 END) ELSE T.D30 END,
					T.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN (CASE WHEN T.D31 - 1 = 0 THEN NULL ELSE T.D31 - 1 END) ELSE T.D31 END
				FROM ReportesApp_Operaciones_Operatividad_ViewPlataformas T
				WHERE T.Anio = YEAR(@x_FechaSelec) AND T.Mes = MONTH(@x_FechaSelec) AND T.idCondicionPL = @CondicionAnterior

				UPDATE T 
				SET T.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN @CantidadCondiciones ELSE T.D1 END,
					T.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN @CantidadCondiciones ELSE T.D2 END,
					T.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN @CantidadCondiciones ELSE T.D3 END,
					T.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN @CantidadCondiciones ELSE T.D4 END,
					T.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN @CantidadCondiciones ELSE T.D5 END,
					T.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN @CantidadCondiciones ELSE T.D6 END,
					T.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN @CantidadCondiciones ELSE T.D7 END,
					T.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN @CantidadCondiciones ELSE T.D8 END,
					T.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN @CantidadCondiciones ELSE T.D9 END,
					T.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN @CantidadCondiciones ELSE T.D10 END,
					T.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN @CantidadCondiciones ELSE T.D11 END,
					T.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN @CantidadCondiciones ELSE T.D12 END,
					T.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN @CantidadCondiciones ELSE T.D13 END,
					T.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN @CantidadCondiciones ELSE T.D14 END,
					T.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN @CantidadCondiciones ELSE T.D15 END,
					T.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN @CantidadCondiciones ELSE T.D16 END,
					T.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN @CantidadCondiciones ELSE T.D17 END,
					T.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN @CantidadCondiciones ELSE T.D18 END,
					T.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN @CantidadCondiciones ELSE T.D19 END,
					T.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN @CantidadCondiciones ELSE T.D20 END,
					T.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN @CantidadCondiciones ELSE T.D21 END,
					T.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN @CantidadCondiciones ELSE T.D22 END,
					T.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN @CantidadCondiciones ELSE T.D23 END,
					T.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN @CantidadCondiciones ELSE T.D24 END,
					T.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN @CantidadCondiciones ELSE T.D25 END,
					T.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN @CantidadCondiciones ELSE T.D26 END,
					T.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN @CantidadCondiciones ELSE T.D27 END,
					T.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN @CantidadCondiciones ELSE T.D28 END,
					T.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN @CantidadCondiciones ELSE T.D29 END,
					T.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN @CantidadCondiciones ELSE T.D30 END,
					T.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN @CantidadCondiciones ELSE T.D31 END
				FROM ReportesApp_Operaciones_Operatividad_ViewPlataformas T
				WHERE T.Anio = YEAR(@x_FechaSelec) AND T.Mes = MONTH(@x_FechaSelec) AND T.idCondicionPL = @idCondicion
			END

			IF (@TipoUnidad = 'TOLVA' AND (@idCondicion IN (1,2,3,4,16,5,6,10,8) OR @CondicionAnterior IN (1,2,3,4,16,5,6,10,8))) BEGIN
				SET @CantidadCondiciones = (SELECT COUNT(*) FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas C
											LEFT JOIN OP_TR_VEHICULO VH ON C.IdUnidad = VH.IdVehiculo
											LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = VH.SubTipoVehiculo
											WHERE C.Fecha = @x_FechaSelec AND C.idCondicion = @idCondicion AND LTRIM(RTRIM(SV.Descripcion)) = @TipoUnidad)

				UPDATE T 
				SET T.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN (CASE WHEN T.D1 - 1 = 0 THEN NULL ELSE T.D1 - 1 END) ELSE T.D1 END,
					T.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN (CASE WHEN T.D2 - 1 = 0 THEN NULL ELSE T.D2 - 1 END) ELSE T.D2 END,
					T.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN (CASE WHEN T.D3 - 1 = 0 THEN NULL ELSE T.D3 - 1 END) ELSE T.D3 END,
					T.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN (CASE WHEN T.D4 - 1 = 0 THEN NULL ELSE T.D4 - 1 END) ELSE T.D4 END,
					T.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN (CASE WHEN T.D5 - 1 = 0 THEN NULL ELSE T.D5 - 1 END) ELSE T.D5 END,
					T.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN (CASE WHEN T.D6 - 1 = 0 THEN NULL ELSE T.D6 - 1 END) ELSE T.D6 END,
					T.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN (CASE WHEN T.D7 - 1 = 0 THEN NULL ELSE T.D7 - 1 END) ELSE T.D7 END,
					T.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN (CASE WHEN T.D8 - 1 = 0 THEN NULL ELSE T.D8 - 1 END) ELSE T.D8 END,
					T.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN (CASE WHEN T.D9 - 1 = 0 THEN NULL ELSE T.D9 - 1 END) ELSE T.D9 END,
					T.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN (CASE WHEN T.D10 - 1 = 0 THEN NULL ELSE T.D10 - 1 END) ELSE T.D10 END,
					T.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN (CASE WHEN T.D11 - 1 = 0 THEN NULL ELSE T.D11 - 1 END) ELSE T.D11 END,
					T.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN (CASE WHEN T.D12 - 1 = 0 THEN NULL ELSE T.D12 - 1 END) ELSE T.D12 END,
					T.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN (CASE WHEN T.D13 - 1 = 0 THEN NULL ELSE T.D13 - 1 END) ELSE T.D13 END,
					T.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN (CASE WHEN T.D14 - 1 = 0 THEN NULL ELSE T.D14 - 1 END) ELSE T.D14 END,
					T.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN (CASE WHEN T.D15 - 1 = 0 THEN NULL ELSE T.D15 - 1 END) ELSE T.D15 END,
					T.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN (CASE WHEN T.D16 - 1 = 0 THEN NULL ELSE T.D16 - 1 END) ELSE T.D16 END,
					T.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN (CASE WHEN T.D17 - 1 = 0 THEN NULL ELSE T.D17 - 1 END) ELSE T.D17 END,
					T.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN (CASE WHEN T.D18 - 1 = 0 THEN NULL ELSE T.D18 - 1 END) ELSE T.D18 END,
					T.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN (CASE WHEN T.D19 - 1 = 0 THEN NULL ELSE T.D19 - 1 END) ELSE T.D19 END,
					T.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN (CASE WHEN T.D20 - 1 = 0 THEN NULL ELSE T.D20 - 1 END) ELSE T.D20 END,
					T.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN (CASE WHEN T.D21 - 1 = 0 THEN NULL ELSE T.D21 - 1 END) ELSE T.D21 END,
					T.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN (CASE WHEN T.D22 - 1 = 0 THEN NULL ELSE T.D22 - 1 END) ELSE T.D22 END,
					T.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN (CASE WHEN T.D23 - 1 = 0 THEN NULL ELSE T.D23 - 1 END) ELSE T.D23 END,
					T.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN (CASE WHEN T.D24 - 1 = 0 THEN NULL ELSE T.D24 - 1 END) ELSE T.D24 END,
					T.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN (CASE WHEN T.D25 - 1 = 0 THEN NULL ELSE T.D25 - 1 END) ELSE T.D25 END,
					T.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN (CASE WHEN T.D26 - 1 = 0 THEN NULL ELSE T.D26 - 1 END) ELSE T.D26 END,
					T.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN (CASE WHEN T.D27 - 1 = 0 THEN NULL ELSE T.D27 - 1 END) ELSE T.D27 END,
					T.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN (CASE WHEN T.D28 - 1 = 0 THEN NULL ELSE T.D28 - 1 END) ELSE T.D28 END,
					T.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN (CASE WHEN T.D29 - 1 = 0 THEN NULL ELSE T.D29 - 1 END) ELSE T.D29 END,
					T.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN (CASE WHEN T.D30 - 1 = 0 THEN NULL ELSE T.D30 - 1 END) ELSE T.D30 END,
					T.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN (CASE WHEN T.D31 - 1 = 0 THEN NULL ELSE T.D31 - 1 END) ELSE T.D31 END
				FROM ReportesApp_Operaciones_Operatividad_ViewTolvas T
				WHERE T.Anio = YEAR(@x_FechaSelec) AND T.Mes = MONTH(@x_FechaSelec) AND T.idCondicionTL = @CondicionAnterior

				UPDATE T 
				SET T.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN @CantidadCondiciones ELSE T.D1 END,
					T.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN @CantidadCondiciones ELSE T.D2 END,
					T.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN @CantidadCondiciones ELSE T.D3 END,
					T.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN @CantidadCondiciones ELSE T.D4 END,
					T.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN @CantidadCondiciones ELSE T.D5 END,
					T.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN @CantidadCondiciones ELSE T.D6 END,
					T.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN @CantidadCondiciones ELSE T.D7 END,
					T.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN @CantidadCondiciones ELSE T.D8 END,
					T.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN @CantidadCondiciones ELSE T.D9 END,
					T.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN @CantidadCondiciones ELSE T.D10 END,
					T.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN @CantidadCondiciones ELSE T.D11 END,
					T.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN @CantidadCondiciones ELSE T.D12 END,
					T.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN @CantidadCondiciones ELSE T.D13 END,
					T.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN @CantidadCondiciones ELSE T.D14 END,
					T.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN @CantidadCondiciones ELSE T.D15 END,
					T.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN @CantidadCondiciones ELSE T.D16 END,
					T.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN @CantidadCondiciones ELSE T.D17 END,
					T.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN @CantidadCondiciones ELSE T.D18 END,
					T.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN @CantidadCondiciones ELSE T.D19 END,
					T.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN @CantidadCondiciones ELSE T.D20 END,
					T.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN @CantidadCondiciones ELSE T.D21 END,
					T.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN @CantidadCondiciones ELSE T.D22 END,
					T.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN @CantidadCondiciones ELSE T.D23 END,
					T.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN @CantidadCondiciones ELSE T.D24 END,
					T.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN @CantidadCondiciones ELSE T.D25 END,
					T.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN @CantidadCondiciones ELSE T.D26 END,
					T.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN @CantidadCondiciones ELSE T.D27 END,
					T.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN @CantidadCondiciones ELSE T.D28 END,
					T.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN @CantidadCondiciones ELSE T.D29 END,
					T.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN @CantidadCondiciones ELSE T.D30 END,
					T.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN @CantidadCondiciones ELSE T.D31 END
				FROM ReportesApp_Operaciones_Operatividad_ViewTolvas T
				WHERE T.Anio = YEAR(@x_FechaSelec) AND T.Mes = MONTH(@x_FechaSelec) AND T.idCondicionTL = @idCondicion
			END

			IF (@TipoUnidad = 'CISTERNA' AND (@idCondicion IN (1,2,3,4,16,5,6,10,8) OR @CondicionAnterior IN (1,2,3,4,16,5,6,10,8))) BEGIN
				SET @CantidadCondiciones = (SELECT COUNT(*) FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas C
											LEFT JOIN OP_TR_VEHICULO VH ON C.IdUnidad = VH.IdVehiculo
											LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = VH.SubTipoVehiculo
											WHERE C.Fecha = @x_FechaSelec AND C.idCondicion = @idCondicion AND LTRIM(RTRIM(SV.Descripcion)) = @TipoUnidad)

				UPDATE T 
				SET T.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN (CASE WHEN T.D1 - 1 = 0 THEN NULL ELSE T.D1 - 1 END) ELSE T.D1 END,
					T.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN (CASE WHEN T.D2 - 1 = 0 THEN NULL ELSE T.D2 - 1 END) ELSE T.D2 END,
					T.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN (CASE WHEN T.D3 - 1 = 0 THEN NULL ELSE T.D3 - 1 END) ELSE T.D3 END,
					T.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN (CASE WHEN T.D4 - 1 = 0 THEN NULL ELSE T.D4 - 1 END) ELSE T.D4 END,
					T.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN (CASE WHEN T.D5 - 1 = 0 THEN NULL ELSE T.D5 - 1 END) ELSE T.D5 END,
					T.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN (CASE WHEN T.D6 - 1 = 0 THEN NULL ELSE T.D6 - 1 END) ELSE T.D6 END,
					T.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN (CASE WHEN T.D7 - 1 = 0 THEN NULL ELSE T.D7 - 1 END) ELSE T.D7 END,
					T.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN (CASE WHEN T.D8 - 1 = 0 THEN NULL ELSE T.D8 - 1 END) ELSE T.D8 END,
					T.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN (CASE WHEN T.D9 - 1 = 0 THEN NULL ELSE T.D9 - 1 END) ELSE T.D9 END,
					T.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN (CASE WHEN T.D10 - 1 = 0 THEN NULL ELSE T.D10 - 1 END) ELSE T.D10 END,
					T.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN (CASE WHEN T.D11 - 1 = 0 THEN NULL ELSE T.D11 - 1 END) ELSE T.D11 END,
					T.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN (CASE WHEN T.D12 - 1 = 0 THEN NULL ELSE T.D12 - 1 END) ELSE T.D12 END,
					T.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN (CASE WHEN T.D13 - 1 = 0 THEN NULL ELSE T.D13 - 1 END) ELSE T.D13 END,
					T.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN (CASE WHEN T.D14 - 1 = 0 THEN NULL ELSE T.D14 - 1 END) ELSE T.D14 END,
					T.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN (CASE WHEN T.D15 - 1 = 0 THEN NULL ELSE T.D15 - 1 END) ELSE T.D15 END,
					T.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN (CASE WHEN T.D16 - 1 = 0 THEN NULL ELSE T.D16 - 1 END) ELSE T.D16 END,
					T.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN (CASE WHEN T.D17 - 1 = 0 THEN NULL ELSE T.D17 - 1 END) ELSE T.D17 END,
					T.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN (CASE WHEN T.D18 - 1 = 0 THEN NULL ELSE T.D18 - 1 END) ELSE T.D18 END,
					T.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN (CASE WHEN T.D19 - 1 = 0 THEN NULL ELSE T.D19 - 1 END) ELSE T.D19 END,
					T.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN (CASE WHEN T.D20 - 1 = 0 THEN NULL ELSE T.D20 - 1 END) ELSE T.D20 END,
					T.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN (CASE WHEN T.D21 - 1 = 0 THEN NULL ELSE T.D21 - 1 END) ELSE T.D21 END,
					T.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN (CASE WHEN T.D22 - 1 = 0 THEN NULL ELSE T.D22 - 1 END) ELSE T.D22 END,
					T.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN (CASE WHEN T.D23 - 1 = 0 THEN NULL ELSE T.D23 - 1 END) ELSE T.D23 END,
					T.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN (CASE WHEN T.D24 - 1 = 0 THEN NULL ELSE T.D24 - 1 END) ELSE T.D24 END,
					T.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN (CASE WHEN T.D25 - 1 = 0 THEN NULL ELSE T.D25 - 1 END) ELSE T.D25 END,
					T.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN (CASE WHEN T.D26 - 1 = 0 THEN NULL ELSE T.D26 - 1 END) ELSE T.D26 END,
					T.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN (CASE WHEN T.D27 - 1 = 0 THEN NULL ELSE T.D27 - 1 END) ELSE T.D27 END,
					T.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN (CASE WHEN T.D28 - 1 = 0 THEN NULL ELSE T.D28 - 1 END) ELSE T.D28 END,
					T.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN (CASE WHEN T.D29 - 1 = 0 THEN NULL ELSE T.D29 - 1 END) ELSE T.D29 END,
					T.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN (CASE WHEN T.D30 - 1 = 0 THEN NULL ELSE T.D30 - 1 END) ELSE T.D30 END,
					T.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN (CASE WHEN T.D31 - 1 = 0 THEN NULL ELSE T.D31 - 1 END) ELSE T.D31 END
				FROM ReportesApp_Operaciones_Operatividad_ViewCisternas T
				WHERE T.Anio = YEAR(@x_FechaSelec) AND T.Mes = MONTH(@x_FechaSelec) AND T.idCondicionCS = @CondicionAnterior

				UPDATE T 
				SET T.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN @CantidadCondiciones ELSE T.D1 END,
					T.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN @CantidadCondiciones ELSE T.D2 END,
					T.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN @CantidadCondiciones ELSE T.D3 END,
					T.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN @CantidadCondiciones ELSE T.D4 END,
					T.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN @CantidadCondiciones ELSE T.D5 END,
					T.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN @CantidadCondiciones ELSE T.D6 END,
					T.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN @CantidadCondiciones ELSE T.D7 END,
					T.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN @CantidadCondiciones ELSE T.D8 END,
					T.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN @CantidadCondiciones ELSE T.D9 END,
					T.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN @CantidadCondiciones ELSE T.D10 END,
					T.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN @CantidadCondiciones ELSE T.D11 END,
					T.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN @CantidadCondiciones ELSE T.D12 END,
					T.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN @CantidadCondiciones ELSE T.D13 END,
					T.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN @CantidadCondiciones ELSE T.D14 END,
					T.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN @CantidadCondiciones ELSE T.D15 END,
					T.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN @CantidadCondiciones ELSE T.D16 END,
					T.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN @CantidadCondiciones ELSE T.D17 END,
					T.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN @CantidadCondiciones ELSE T.D18 END,
					T.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN @CantidadCondiciones ELSE T.D19 END,
					T.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN @CantidadCondiciones ELSE T.D20 END,
					T.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN @CantidadCondiciones ELSE T.D21 END,
					T.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN @CantidadCondiciones ELSE T.D22 END,
					T.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN @CantidadCondiciones ELSE T.D23 END,
					T.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN @CantidadCondiciones ELSE T.D24 END,
					T.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN @CantidadCondiciones ELSE T.D25 END,
					T.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN @CantidadCondiciones ELSE T.D26 END,
					T.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN @CantidadCondiciones ELSE T.D27 END,
					T.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN @CantidadCondiciones ELSE T.D28 END,
					T.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN @CantidadCondiciones ELSE T.D29 END,
					T.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN @CantidadCondiciones ELSE T.D30 END,
					T.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN @CantidadCondiciones ELSE T.D31 END
				FROM ReportesApp_Operaciones_Operatividad_ViewCisternas T
				WHERE T.Anio = YEAR(@x_FechaSelec) AND T.Mes = MONTH(@x_FechaSelec) AND T.idCondicionCS = @idCondicion
			END
		END

		SET @exito = '0 = Reporte actualizado'
	END
	ELSE BEGIN
		SET @exito = '-1 = No se pudo registrar el reporte'
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

-----------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 26-08-2024
-- Description:	GUARDAR VIAJE ESTADO - OTROS ESTADOS CARRETAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Operatividad_ActualizarViajeOtrosCarretas]
@idCondicion INT,
@IDTracto INT,
@FechaInicio DATE,
@Usuario VARCHAR(80)
AS	
DECLARE @exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_Operatividad_CarretasView WHERE IdUnidad = @IDTracto AND Mes = MONTH(@FechaInicio) AND Anio = YEAR(@FechaInicio))) BEGIN
		DECLARE @correlativo INT
		DECLARE @idOperacion INT = (SELECT IdProgramacion FROM ReportesApp_Operacion_MaestroUnidadesConductor WHERE IdUnidad = @IDTracto)
		DECLARE @Operacion VARCHAR(10) = (CASE WHEN @idOperacion = 1 THEN 'TL'
									  WHEN @idOperacion = 2 THEN 'ACL'
									  WHEN @idOperacion = 3 THEN 'LG'
									  WHEN @idOperacion = 4 THEN 'G'
									  WHEN @idOperacion = 10 THEN 'V'
									  ELSE 'O' END)
		DECLARE @CondicionAnterior INT = (SELECT idCondicion FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas WHERE IdUnidad = @IDTracto AND Fecha = @FechaInicio)

		DELETE FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas
		WHERE IdUnidad = @IDTracto AND Fecha = @FechaInicio

		SET @correlativo = (SELECT MAX(idOperatividad) FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas)
		SET @correlativo = ISNULL(@correlativo,0) + 1

		INSERT INTO ReportesApp_Operaciones_Operatividad_RegistroCarretas(idOperatividad,IdUnidad,Fecha,idCondicion,IdOperacion,UsuarioRegistra,FechaRegistra)
		VALUES(@correlativo, @IDTracto, @FechaInicio, @idCondicion, @idOperacion, @Usuario, GETDATE())

		DECLARE @x_FechaSelec DATE = @FechaInicio
		DECLARE @x_DiaSelec INT = (SELECT DAY(@FechaInicio))
		DECLARE @Condicion VARCHAR(50) = (SELECT C.Codigo FROM ReportesApp_Operaciones_Operatividad_Condiciones C WHERE C.idCondicion = @idCondicion)
		DECLARE @CondicionCompleta VARCHAR(50) = @Condicion + ' (' + @Operacion + ')'
		DECLARE @CantidadCondiciones INT = (SELECT COUNT(*) FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas WHERE Fecha = @x_FechaSelec AND idCondicion = @idCondicion)
		DECLARE @TipoUnidad VARCHAR(150) = (SELECT LTRIM(RTRIM(SV.Descripcion)) FROM OP_TR_VEHICULO VH
												LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = VH.SubTipoVehiculo
												WHERE VH.IdVehiculo = @IDTracto)


		IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_Operatividad_CarretasView WHERE Anio = YEAR(@x_FechaSelec) AND Mes = MONTH(@x_FechaSelec) AND IdUnidad = @IDTracto)) BEGIN
			UPDATE P 
			SET P.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN @CondicionCompleta ELSE P.D1 END,
				P.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN @CondicionCompleta ELSE P.D2 END,
				P.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN @CondicionCompleta ELSE P.D3 END,
				P.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN @CondicionCompleta ELSE P.D4 END,
				P.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN @CondicionCompleta ELSE P.D5 END,
				P.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN @CondicionCompleta ELSE P.D6 END,
				P.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN @CondicionCompleta ELSE P.D7 END,
				P.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN @CondicionCompleta ELSE P.D8 END,
				P.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN @CondicionCompleta ELSE P.D9 END,
				P.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN @CondicionCompleta ELSE P.D10 END,
				P.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN @CondicionCompleta ELSE P.D11 END,
				P.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN @CondicionCompleta ELSE P.D12 END,
				P.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN @CondicionCompleta ELSE P.D13 END,
				P.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN @CondicionCompleta ELSE P.D14 END,
				P.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN @CondicionCompleta ELSE P.D15 END,
				P.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN @CondicionCompleta ELSE P.D16 END,
				P.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN @CondicionCompleta ELSE P.D17 END,
				P.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN @CondicionCompleta ELSE P.D18 END,
				P.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN @CondicionCompleta ELSE P.D19 END,
				P.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN @CondicionCompleta ELSE P.D20 END,
				P.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN @CondicionCompleta ELSE P.D21 END,
				P.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN @CondicionCompleta ELSE P.D22 END,
				P.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN @CondicionCompleta ELSE P.D23 END,
				P.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN @CondicionCompleta ELSE P.D24 END,
				P.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN @CondicionCompleta ELSE P.D25 END,
				P.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN @CondicionCompleta ELSE P.D26 END,
				P.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN @CondicionCompleta ELSE P.D27 END,
				P.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN @CondicionCompleta ELSE P.D28 END,
				P.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN @CondicionCompleta ELSE P.D29 END,
				P.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN @CondicionCompleta ELSE P.D30 END,
				P.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN @CondicionCompleta ELSE P.D31 END
			FROM ReportesApp_Operaciones_Operatividad_CarretasView P
			WHERE P.Anio = YEAR(@x_FechaSelec) AND P.Mes = MONTH(@x_FechaSelec) AND P.IdUnidad = @IDTracto
		END

		IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_Operatividad_ViewCarretas WHERE Anio = YEAR(@x_FechaSelec) AND Mes = MONTH(@x_FechaSelec))) BEGIN
			UPDATE T 
			SET T.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN (CASE WHEN T.D1 - 1 = 0 THEN NULL ELSE T.D1 - 1 END) ELSE T.D1 END,
				T.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN (CASE WHEN T.D2 - 1 = 0 THEN NULL ELSE T.D2 - 1 END) ELSE T.D2 END,
				T.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN (CASE WHEN T.D3 - 1 = 0 THEN NULL ELSE T.D3 - 1 END) ELSE T.D3 END,
				T.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN (CASE WHEN T.D4 - 1 = 0 THEN NULL ELSE T.D4 - 1 END) ELSE T.D4 END,
				T.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN (CASE WHEN T.D5 - 1 = 0 THEN NULL ELSE T.D5 - 1 END) ELSE T.D5 END,
				T.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN (CASE WHEN T.D6 - 1 = 0 THEN NULL ELSE T.D6 - 1 END) ELSE T.D6 END,
				T.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN (CASE WHEN T.D7 - 1 = 0 THEN NULL ELSE T.D7 - 1 END) ELSE T.D7 END,
				T.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN (CASE WHEN T.D8 - 1 = 0 THEN NULL ELSE T.D8 - 1 END) ELSE T.D8 END,
				T.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN (CASE WHEN T.D9 - 1 = 0 THEN NULL ELSE T.D9 - 1 END) ELSE T.D9 END,
				T.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN (CASE WHEN T.D10 - 1 = 0 THEN NULL ELSE T.D10 - 1 END) ELSE T.D10 END,
				T.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN (CASE WHEN T.D11 - 1 = 0 THEN NULL ELSE T.D11 - 1 END) ELSE T.D11 END,
				T.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN (CASE WHEN T.D12 - 1 = 0 THEN NULL ELSE T.D12 - 1 END) ELSE T.D12 END,
				T.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN (CASE WHEN T.D13 - 1 = 0 THEN NULL ELSE T.D13 - 1 END) ELSE T.D13 END,
				T.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN (CASE WHEN T.D14 - 1 = 0 THEN NULL ELSE T.D14 - 1 END) ELSE T.D14 END,
				T.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN (CASE WHEN T.D15 - 1 = 0 THEN NULL ELSE T.D15 - 1 END) ELSE T.D15 END,
				T.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN (CASE WHEN T.D16 - 1 = 0 THEN NULL ELSE T.D16 - 1 END) ELSE T.D16 END,
				T.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN (CASE WHEN T.D17 - 1 = 0 THEN NULL ELSE T.D17 - 1 END) ELSE T.D17 END,
				T.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN (CASE WHEN T.D18 - 1 = 0 THEN NULL ELSE T.D18 - 1 END) ELSE T.D18 END,
				T.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN (CASE WHEN T.D19 - 1 = 0 THEN NULL ELSE T.D19 - 1 END) ELSE T.D19 END,
				T.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN (CASE WHEN T.D20 - 1 = 0 THEN NULL ELSE T.D20 - 1 END) ELSE T.D20 END,
				T.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN (CASE WHEN T.D21 - 1 = 0 THEN NULL ELSE T.D21 - 1 END) ELSE T.D21 END,
				T.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN (CASE WHEN T.D22 - 1 = 0 THEN NULL ELSE T.D22 - 1 END) ELSE T.D22 END,
				T.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN (CASE WHEN T.D23 - 1 = 0 THEN NULL ELSE T.D23 - 1 END) ELSE T.D23 END,
				T.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN (CASE WHEN T.D24 - 1 = 0 THEN NULL ELSE T.D24 - 1 END) ELSE T.D24 END,
				T.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN (CASE WHEN T.D25 - 1 = 0 THEN NULL ELSE T.D25 - 1 END) ELSE T.D25 END,
				T.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN (CASE WHEN T.D26 - 1 = 0 THEN NULL ELSE T.D26 - 1 END) ELSE T.D26 END,
				T.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN (CASE WHEN T.D27 - 1 = 0 THEN NULL ELSE T.D27 - 1 END) ELSE T.D27 END,
				T.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN (CASE WHEN T.D28 - 1 = 0 THEN NULL ELSE T.D28 - 1 END) ELSE T.D28 END,
				T.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN (CASE WHEN T.D29 - 1 = 0 THEN NULL ELSE T.D29 - 1 END) ELSE T.D29 END,
				T.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN (CASE WHEN T.D30 - 1 = 0 THEN NULL ELSE T.D30 - 1 END) ELSE T.D30 END,
				T.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN (CASE WHEN T.D31 - 1 = 0 THEN NULL ELSE T.D31 - 1 END) ELSE T.D31 END
			FROM ReportesApp_Operaciones_Operatividad_ViewCarretas T
			WHERE T.Anio = YEAR(@x_FechaSelec) AND T.Mes = MONTH(@x_FechaSelec) AND T.idCondicion = @CondicionAnterior

			UPDATE T 
			SET T.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN @CantidadCondiciones ELSE T.D1 END,
				T.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN @CantidadCondiciones ELSE T.D2 END,
				T.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN @CantidadCondiciones ELSE T.D3 END,
				T.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN @CantidadCondiciones ELSE T.D4 END,
				T.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN @CantidadCondiciones ELSE T.D5 END,
				T.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN @CantidadCondiciones ELSE T.D6 END,
				T.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN @CantidadCondiciones ELSE T.D7 END,
				T.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN @CantidadCondiciones ELSE T.D8 END,
				T.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN @CantidadCondiciones ELSE T.D9 END,
				T.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN @CantidadCondiciones ELSE T.D10 END,
				T.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN @CantidadCondiciones ELSE T.D11 END,
				T.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN @CantidadCondiciones ELSE T.D12 END,
				T.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN @CantidadCondiciones ELSE T.D13 END,
				T.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN @CantidadCondiciones ELSE T.D14 END,
				T.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN @CantidadCondiciones ELSE T.D15 END,
				T.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN @CantidadCondiciones ELSE T.D16 END,
				T.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN @CantidadCondiciones ELSE T.D17 END,
				T.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN @CantidadCondiciones ELSE T.D18 END,
				T.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN @CantidadCondiciones ELSE T.D19 END,
				T.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN @CantidadCondiciones ELSE T.D20 END,
				T.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN @CantidadCondiciones ELSE T.D21 END,
				T.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN @CantidadCondiciones ELSE T.D22 END,
				T.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN @CantidadCondiciones ELSE T.D23 END,
				T.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN @CantidadCondiciones ELSE T.D24 END,
				T.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN @CantidadCondiciones ELSE T.D25 END,
				T.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN @CantidadCondiciones ELSE T.D26 END,
				T.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN @CantidadCondiciones ELSE T.D27 END,
				T.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN @CantidadCondiciones ELSE T.D28 END,
				T.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN @CantidadCondiciones ELSE T.D29 END,
				T.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN @CantidadCondiciones ELSE T.D30 END,
				T.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN @CantidadCondiciones ELSE T.D31 END
			FROM ReportesApp_Operaciones_Operatividad_ViewCarretas T
			WHERE T.Anio = YEAR(@x_FechaSelec) AND T.Mes = MONTH(@x_FechaSelec) AND T.idCondicion = @idCondicion

			IF (@TipoUnidad = 'CORTINERA' AND (@idCondicion IN (1,2,3,4,16,5,6,13,10,8) OR @CondicionAnterior IN (1,2,3,4,16,5,6,13,10,8))) BEGIN
				SET @CantidadCondiciones = (SELECT COUNT(*) FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas C
											LEFT JOIN OP_TR_VEHICULO VH ON C.IdUnidad = VH.IdVehiculo
											LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = VH.SubTipoVehiculo
											WHERE C.Fecha = @x_FechaSelec AND C.idCondicion = @idCondicion AND LTRIM(RTRIM(SV.Descripcion)) = @TipoUnidad)

				UPDATE T 
				SET T.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN (CASE WHEN T.D1 - 1 = 0 THEN NULL ELSE T.D1 - 1 END) ELSE T.D1 END,
					T.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN (CASE WHEN T.D2 - 1 = 0 THEN NULL ELSE T.D2 - 1 END) ELSE T.D2 END,
					T.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN (CASE WHEN T.D3 - 1 = 0 THEN NULL ELSE T.D3 - 1 END) ELSE T.D3 END,
					T.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN (CASE WHEN T.D4 - 1 = 0 THEN NULL ELSE T.D4 - 1 END) ELSE T.D4 END,
					T.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN (CASE WHEN T.D5 - 1 = 0 THEN NULL ELSE T.D5 - 1 END) ELSE T.D5 END,
					T.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN (CASE WHEN T.D6 - 1 = 0 THEN NULL ELSE T.D6 - 1 END) ELSE T.D6 END,
					T.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN (CASE WHEN T.D7 - 1 = 0 THEN NULL ELSE T.D7 - 1 END) ELSE T.D7 END,
					T.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN (CASE WHEN T.D8 - 1 = 0 THEN NULL ELSE T.D8 - 1 END) ELSE T.D8 END,
					T.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN (CASE WHEN T.D9 - 1 = 0 THEN NULL ELSE T.D9 - 1 END) ELSE T.D9 END,
					T.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN (CASE WHEN T.D10 - 1 = 0 THEN NULL ELSE T.D10 - 1 END) ELSE T.D10 END,
					T.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN (CASE WHEN T.D11 - 1 = 0 THEN NULL ELSE T.D11 - 1 END) ELSE T.D11 END,
					T.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN (CASE WHEN T.D12 - 1 = 0 THEN NULL ELSE T.D12 - 1 END) ELSE T.D12 END,
					T.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN (CASE WHEN T.D13 - 1 = 0 THEN NULL ELSE T.D13 - 1 END) ELSE T.D13 END,
					T.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN (CASE WHEN T.D14 - 1 = 0 THEN NULL ELSE T.D14 - 1 END) ELSE T.D14 END,
					T.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN (CASE WHEN T.D15 - 1 = 0 THEN NULL ELSE T.D15 - 1 END) ELSE T.D15 END,
					T.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN (CASE WHEN T.D16 - 1 = 0 THEN NULL ELSE T.D16 - 1 END) ELSE T.D16 END,
					T.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN (CASE WHEN T.D17 - 1 = 0 THEN NULL ELSE T.D17 - 1 END) ELSE T.D17 END,
					T.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN (CASE WHEN T.D18 - 1 = 0 THEN NULL ELSE T.D18 - 1 END) ELSE T.D18 END,
					T.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN (CASE WHEN T.D19 - 1 = 0 THEN NULL ELSE T.D19 - 1 END) ELSE T.D19 END,
					T.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN (CASE WHEN T.D20 - 1 = 0 THEN NULL ELSE T.D20 - 1 END) ELSE T.D20 END,
					T.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN (CASE WHEN T.D21 - 1 = 0 THEN NULL ELSE T.D21 - 1 END) ELSE T.D21 END,
					T.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN (CASE WHEN T.D22 - 1 = 0 THEN NULL ELSE T.D22 - 1 END) ELSE T.D22 END,
					T.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN (CASE WHEN T.D23 - 1 = 0 THEN NULL ELSE T.D23 - 1 END) ELSE T.D23 END,
					T.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN (CASE WHEN T.D24 - 1 = 0 THEN NULL ELSE T.D24 - 1 END) ELSE T.D24 END,
					T.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN (CASE WHEN T.D25 - 1 = 0 THEN NULL ELSE T.D25 - 1 END) ELSE T.D25 END,
					T.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN (CASE WHEN T.D26 - 1 = 0 THEN NULL ELSE T.D26 - 1 END) ELSE T.D26 END,
					T.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN (CASE WHEN T.D27 - 1 = 0 THEN NULL ELSE T.D27 - 1 END) ELSE T.D27 END,
					T.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN (CASE WHEN T.D28 - 1 = 0 THEN NULL ELSE T.D28 - 1 END) ELSE T.D28 END,
					T.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN (CASE WHEN T.D29 - 1 = 0 THEN NULL ELSE T.D29 - 1 END) ELSE T.D29 END,
					T.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN (CASE WHEN T.D30 - 1 = 0 THEN NULL ELSE T.D30 - 1 END) ELSE T.D30 END,
					T.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN (CASE WHEN T.D31 - 1 = 0 THEN NULL ELSE T.D31 - 1 END) ELSE T.D31 END
				FROM ReportesApp_Operaciones_Operatividad_ViewCortineras T
				WHERE T.Anio = YEAR(@x_FechaSelec) AND T.Mes = MONTH(@x_FechaSelec) AND T.idCondicionCR = @CondicionAnterior

				UPDATE T 
				SET T.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN @CantidadCondiciones ELSE T.D1 END,
					T.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN @CantidadCondiciones ELSE T.D2 END,
					T.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN @CantidadCondiciones ELSE T.D3 END,
					T.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN @CantidadCondiciones ELSE T.D4 END,
					T.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN @CantidadCondiciones ELSE T.D5 END,
					T.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN @CantidadCondiciones ELSE T.D6 END,
					T.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN @CantidadCondiciones ELSE T.D7 END,
					T.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN @CantidadCondiciones ELSE T.D8 END,
					T.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN @CantidadCondiciones ELSE T.D9 END,
					T.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN @CantidadCondiciones ELSE T.D10 END,
					T.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN @CantidadCondiciones ELSE T.D11 END,
					T.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN @CantidadCondiciones ELSE T.D12 END,
					T.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN @CantidadCondiciones ELSE T.D13 END,
					T.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN @CantidadCondiciones ELSE T.D14 END,
					T.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN @CantidadCondiciones ELSE T.D15 END,
					T.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN @CantidadCondiciones ELSE T.D16 END,
					T.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN @CantidadCondiciones ELSE T.D17 END,
					T.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN @CantidadCondiciones ELSE T.D18 END,
					T.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN @CantidadCondiciones ELSE T.D19 END,
					T.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN @CantidadCondiciones ELSE T.D20 END,
					T.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN @CantidadCondiciones ELSE T.D21 END,
					T.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN @CantidadCondiciones ELSE T.D22 END,
					T.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN @CantidadCondiciones ELSE T.D23 END,
					T.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN @CantidadCondiciones ELSE T.D24 END,
					T.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN @CantidadCondiciones ELSE T.D25 END,
					T.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN @CantidadCondiciones ELSE T.D26 END,
					T.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN @CantidadCondiciones ELSE T.D27 END,
					T.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN @CantidadCondiciones ELSE T.D28 END,
					T.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN @CantidadCondiciones ELSE T.D29 END,
					T.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN @CantidadCondiciones ELSE T.D30 END,
					T.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN @CantidadCondiciones ELSE T.D31 END
				FROM ReportesApp_Operaciones_Operatividad_ViewCortineras T
				WHERE T.Anio = YEAR(@x_FechaSelec) AND T.Mes = MONTH(@x_FechaSelec) AND T.idCondicionCR = @idCondicion
			END

			IF (@TipoUnidad = 'PLATAFORMA' AND (@idCondicion IN (1,2,3,4,16,5,6,10,8) OR @CondicionAnterior IN (1,2,3,4,16,5,6,10,8))) BEGIN
				SET @CantidadCondiciones = (SELECT COUNT(*) FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas C
											LEFT JOIN OP_TR_VEHICULO VH ON C.IdUnidad = VH.IdVehiculo
											LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = VH.SubTipoVehiculo
											WHERE C.Fecha = @x_FechaSelec AND C.idCondicion = @idCondicion AND LTRIM(RTRIM(SV.Descripcion)) = @TipoUnidad)

				UPDATE T 
				SET T.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN (CASE WHEN T.D1 - 1 = 0 THEN NULL ELSE T.D1 - 1 END) ELSE T.D1 END,
					T.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN (CASE WHEN T.D2 - 1 = 0 THEN NULL ELSE T.D2 - 1 END) ELSE T.D2 END,
					T.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN (CASE WHEN T.D3 - 1 = 0 THEN NULL ELSE T.D3 - 1 END) ELSE T.D3 END,
					T.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN (CASE WHEN T.D4 - 1 = 0 THEN NULL ELSE T.D4 - 1 END) ELSE T.D4 END,
					T.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN (CASE WHEN T.D5 - 1 = 0 THEN NULL ELSE T.D5 - 1 END) ELSE T.D5 END,
					T.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN (CASE WHEN T.D6 - 1 = 0 THEN NULL ELSE T.D6 - 1 END) ELSE T.D6 END,
					T.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN (CASE WHEN T.D7 - 1 = 0 THEN NULL ELSE T.D7 - 1 END) ELSE T.D7 END,
					T.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN (CASE WHEN T.D8 - 1 = 0 THEN NULL ELSE T.D8 - 1 END) ELSE T.D8 END,
					T.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN (CASE WHEN T.D9 - 1 = 0 THEN NULL ELSE T.D9 - 1 END) ELSE T.D9 END,
					T.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN (CASE WHEN T.D10 - 1 = 0 THEN NULL ELSE T.D10 - 1 END) ELSE T.D10 END,
					T.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN (CASE WHEN T.D11 - 1 = 0 THEN NULL ELSE T.D11 - 1 END) ELSE T.D11 END,
					T.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN (CASE WHEN T.D12 - 1 = 0 THEN NULL ELSE T.D12 - 1 END) ELSE T.D12 END,
					T.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN (CASE WHEN T.D13 - 1 = 0 THEN NULL ELSE T.D13 - 1 END) ELSE T.D13 END,
					T.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN (CASE WHEN T.D14 - 1 = 0 THEN NULL ELSE T.D14 - 1 END) ELSE T.D14 END,
					T.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN (CASE WHEN T.D15 - 1 = 0 THEN NULL ELSE T.D15 - 1 END) ELSE T.D15 END,
					T.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN (CASE WHEN T.D16 - 1 = 0 THEN NULL ELSE T.D16 - 1 END) ELSE T.D16 END,
					T.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN (CASE WHEN T.D17 - 1 = 0 THEN NULL ELSE T.D17 - 1 END) ELSE T.D17 END,
					T.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN (CASE WHEN T.D18 - 1 = 0 THEN NULL ELSE T.D18 - 1 END) ELSE T.D18 END,
					T.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN (CASE WHEN T.D19 - 1 = 0 THEN NULL ELSE T.D19 - 1 END) ELSE T.D19 END,
					T.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN (CASE WHEN T.D20 - 1 = 0 THEN NULL ELSE T.D20 - 1 END) ELSE T.D20 END,
					T.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN (CASE WHEN T.D21 - 1 = 0 THEN NULL ELSE T.D21 - 1 END) ELSE T.D21 END,
					T.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN (CASE WHEN T.D22 - 1 = 0 THEN NULL ELSE T.D22 - 1 END) ELSE T.D22 END,
					T.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN (CASE WHEN T.D23 - 1 = 0 THEN NULL ELSE T.D23 - 1 END) ELSE T.D23 END,
					T.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN (CASE WHEN T.D24 - 1 = 0 THEN NULL ELSE T.D24 - 1 END) ELSE T.D24 END,
					T.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN (CASE WHEN T.D25 - 1 = 0 THEN NULL ELSE T.D25 - 1 END) ELSE T.D25 END,
					T.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN (CASE WHEN T.D26 - 1 = 0 THEN NULL ELSE T.D26 - 1 END) ELSE T.D26 END,
					T.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN (CASE WHEN T.D27 - 1 = 0 THEN NULL ELSE T.D27 - 1 END) ELSE T.D27 END,
					T.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN (CASE WHEN T.D28 - 1 = 0 THEN NULL ELSE T.D28 - 1 END) ELSE T.D28 END,
					T.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN (CASE WHEN T.D29 - 1 = 0 THEN NULL ELSE T.D29 - 1 END) ELSE T.D29 END,
					T.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN (CASE WHEN T.D30 - 1 = 0 THEN NULL ELSE T.D30 - 1 END) ELSE T.D30 END,
					T.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN (CASE WHEN T.D31 - 1 = 0 THEN NULL ELSE T.D31 - 1 END) ELSE T.D31 END
				FROM ReportesApp_Operaciones_Operatividad_ViewPlataformas T
				WHERE T.Anio = YEAR(@x_FechaSelec) AND T.Mes = MONTH(@x_FechaSelec) AND T.idCondicionPL = @CondicionAnterior

				UPDATE T 
				SET T.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN @CantidadCondiciones ELSE T.D1 END,
					T.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN @CantidadCondiciones ELSE T.D2 END,
					T.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN @CantidadCondiciones ELSE T.D3 END,
					T.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN @CantidadCondiciones ELSE T.D4 END,
					T.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN @CantidadCondiciones ELSE T.D5 END,
					T.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN @CantidadCondiciones ELSE T.D6 END,
					T.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN @CantidadCondiciones ELSE T.D7 END,
					T.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN @CantidadCondiciones ELSE T.D8 END,
					T.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN @CantidadCondiciones ELSE T.D9 END,
					T.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN @CantidadCondiciones ELSE T.D10 END,
					T.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN @CantidadCondiciones ELSE T.D11 END,
					T.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN @CantidadCondiciones ELSE T.D12 END,
					T.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN @CantidadCondiciones ELSE T.D13 END,
					T.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN @CantidadCondiciones ELSE T.D14 END,
					T.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN @CantidadCondiciones ELSE T.D15 END,
					T.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN @CantidadCondiciones ELSE T.D16 END,
					T.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN @CantidadCondiciones ELSE T.D17 END,
					T.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN @CantidadCondiciones ELSE T.D18 END,
					T.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN @CantidadCondiciones ELSE T.D19 END,
					T.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN @CantidadCondiciones ELSE T.D20 END,
					T.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN @CantidadCondiciones ELSE T.D21 END,
					T.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN @CantidadCondiciones ELSE T.D22 END,
					T.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN @CantidadCondiciones ELSE T.D23 END,
					T.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN @CantidadCondiciones ELSE T.D24 END,
					T.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN @CantidadCondiciones ELSE T.D25 END,
					T.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN @CantidadCondiciones ELSE T.D26 END,
					T.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN @CantidadCondiciones ELSE T.D27 END,
					T.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN @CantidadCondiciones ELSE T.D28 END,
					T.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN @CantidadCondiciones ELSE T.D29 END,
					T.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN @CantidadCondiciones ELSE T.D30 END,
					T.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN @CantidadCondiciones ELSE T.D31 END
				FROM ReportesApp_Operaciones_Operatividad_ViewPlataformas T
				WHERE T.Anio = YEAR(@x_FechaSelec) AND T.Mes = MONTH(@x_FechaSelec) AND T.idCondicionPL = @idCondicion
			END

			IF (@TipoUnidad = 'TOLVA' AND (@idCondicion IN (1,2,3,4,16,5,6,10,8) OR @CondicionAnterior IN (1,2,3,4,16,5,6,10,8))) BEGIN
				SET @CantidadCondiciones = (SELECT COUNT(*) FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas C
											LEFT JOIN OP_TR_VEHICULO VH ON C.IdUnidad = VH.IdVehiculo
											LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = VH.SubTipoVehiculo
											WHERE C.Fecha = @x_FechaSelec AND C.idCondicion = @idCondicion AND LTRIM(RTRIM(SV.Descripcion)) = @TipoUnidad)

				UPDATE T 
				SET T.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN (CASE WHEN T.D1 - 1 = 0 THEN NULL ELSE T.D1 - 1 END) ELSE T.D1 END,
					T.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN (CASE WHEN T.D2 - 1 = 0 THEN NULL ELSE T.D2 - 1 END) ELSE T.D2 END,
					T.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN (CASE WHEN T.D3 - 1 = 0 THEN NULL ELSE T.D3 - 1 END) ELSE T.D3 END,
					T.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN (CASE WHEN T.D4 - 1 = 0 THEN NULL ELSE T.D4 - 1 END) ELSE T.D4 END,
					T.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN (CASE WHEN T.D5 - 1 = 0 THEN NULL ELSE T.D5 - 1 END) ELSE T.D5 END,
					T.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN (CASE WHEN T.D6 - 1 = 0 THEN NULL ELSE T.D6 - 1 END) ELSE T.D6 END,
					T.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN (CASE WHEN T.D7 - 1 = 0 THEN NULL ELSE T.D7 - 1 END) ELSE T.D7 END,
					T.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN (CASE WHEN T.D8 - 1 = 0 THEN NULL ELSE T.D8 - 1 END) ELSE T.D8 END,
					T.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN (CASE WHEN T.D9 - 1 = 0 THEN NULL ELSE T.D9 - 1 END) ELSE T.D9 END,
					T.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN (CASE WHEN T.D10 - 1 = 0 THEN NULL ELSE T.D10 - 1 END) ELSE T.D10 END,
					T.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN (CASE WHEN T.D11 - 1 = 0 THEN NULL ELSE T.D11 - 1 END) ELSE T.D11 END,
					T.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN (CASE WHEN T.D12 - 1 = 0 THEN NULL ELSE T.D12 - 1 END) ELSE T.D12 END,
					T.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN (CASE WHEN T.D13 - 1 = 0 THEN NULL ELSE T.D13 - 1 END) ELSE T.D13 END,
					T.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN (CASE WHEN T.D14 - 1 = 0 THEN NULL ELSE T.D14 - 1 END) ELSE T.D14 END,
					T.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN (CASE WHEN T.D15 - 1 = 0 THEN NULL ELSE T.D15 - 1 END) ELSE T.D15 END,
					T.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN (CASE WHEN T.D16 - 1 = 0 THEN NULL ELSE T.D16 - 1 END) ELSE T.D16 END,
					T.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN (CASE WHEN T.D17 - 1 = 0 THEN NULL ELSE T.D17 - 1 END) ELSE T.D17 END,
					T.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN (CASE WHEN T.D18 - 1 = 0 THEN NULL ELSE T.D18 - 1 END) ELSE T.D18 END,
					T.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN (CASE WHEN T.D19 - 1 = 0 THEN NULL ELSE T.D19 - 1 END) ELSE T.D19 END,
					T.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN (CASE WHEN T.D20 - 1 = 0 THEN NULL ELSE T.D20 - 1 END) ELSE T.D20 END,
					T.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN (CASE WHEN T.D21 - 1 = 0 THEN NULL ELSE T.D21 - 1 END) ELSE T.D21 END,
					T.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN (CASE WHEN T.D22 - 1 = 0 THEN NULL ELSE T.D22 - 1 END) ELSE T.D22 END,
					T.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN (CASE WHEN T.D23 - 1 = 0 THEN NULL ELSE T.D23 - 1 END) ELSE T.D23 END,
					T.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN (CASE WHEN T.D24 - 1 = 0 THEN NULL ELSE T.D24 - 1 END) ELSE T.D24 END,
					T.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN (CASE WHEN T.D25 - 1 = 0 THEN NULL ELSE T.D25 - 1 END) ELSE T.D25 END,
					T.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN (CASE WHEN T.D26 - 1 = 0 THEN NULL ELSE T.D26 - 1 END) ELSE T.D26 END,
					T.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN (CASE WHEN T.D27 - 1 = 0 THEN NULL ELSE T.D27 - 1 END) ELSE T.D27 END,
					T.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN (CASE WHEN T.D28 - 1 = 0 THEN NULL ELSE T.D28 - 1 END) ELSE T.D28 END,
					T.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN (CASE WHEN T.D29 - 1 = 0 THEN NULL ELSE T.D29 - 1 END) ELSE T.D29 END,
					T.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN (CASE WHEN T.D30 - 1 = 0 THEN NULL ELSE T.D30 - 1 END) ELSE T.D30 END,
					T.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN (CASE WHEN T.D31 - 1 = 0 THEN NULL ELSE T.D31 - 1 END) ELSE T.D31 END
				FROM ReportesApp_Operaciones_Operatividad_ViewTolvas T
				WHERE T.Anio = YEAR(@x_FechaSelec) AND T.Mes = MONTH(@x_FechaSelec) AND T.idCondicionTL = @CondicionAnterior

				UPDATE T 
				SET T.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN @CantidadCondiciones ELSE T.D1 END,
					T.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN @CantidadCondiciones ELSE T.D2 END,
					T.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN @CantidadCondiciones ELSE T.D3 END,
					T.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN @CantidadCondiciones ELSE T.D4 END,
					T.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN @CantidadCondiciones ELSE T.D5 END,
					T.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN @CantidadCondiciones ELSE T.D6 END,
					T.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN @CantidadCondiciones ELSE T.D7 END,
					T.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN @CantidadCondiciones ELSE T.D8 END,
					T.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN @CantidadCondiciones ELSE T.D9 END,
					T.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN @CantidadCondiciones ELSE T.D10 END,
					T.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN @CantidadCondiciones ELSE T.D11 END,
					T.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN @CantidadCondiciones ELSE T.D12 END,
					T.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN @CantidadCondiciones ELSE T.D13 END,
					T.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN @CantidadCondiciones ELSE T.D14 END,
					T.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN @CantidadCondiciones ELSE T.D15 END,
					T.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN @CantidadCondiciones ELSE T.D16 END,
					T.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN @CantidadCondiciones ELSE T.D17 END,
					T.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN @CantidadCondiciones ELSE T.D18 END,
					T.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN @CantidadCondiciones ELSE T.D19 END,
					T.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN @CantidadCondiciones ELSE T.D20 END,
					T.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN @CantidadCondiciones ELSE T.D21 END,
					T.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN @CantidadCondiciones ELSE T.D22 END,
					T.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN @CantidadCondiciones ELSE T.D23 END,
					T.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN @CantidadCondiciones ELSE T.D24 END,
					T.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN @CantidadCondiciones ELSE T.D25 END,
					T.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN @CantidadCondiciones ELSE T.D26 END,
					T.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN @CantidadCondiciones ELSE T.D27 END,
					T.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN @CantidadCondiciones ELSE T.D28 END,
					T.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN @CantidadCondiciones ELSE T.D29 END,
					T.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN @CantidadCondiciones ELSE T.D30 END,
					T.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN @CantidadCondiciones ELSE T.D31 END
				FROM ReportesApp_Operaciones_Operatividad_ViewTolvas T
				WHERE T.Anio = YEAR(@x_FechaSelec) AND T.Mes = MONTH(@x_FechaSelec) AND T.idCondicionTL = @idCondicion
			END

			IF (@TipoUnidad = 'CISTERNA' AND (@idCondicion IN (1,2,3,4,16,5,6,10,8) OR @CondicionAnterior IN (1,2,3,4,16,5,6,10,8))) BEGIN
				SET @CantidadCondiciones = (SELECT COUNT(*) FROM ReportesApp_Operaciones_Operatividad_RegistroCarretas C
											LEFT JOIN OP_TR_VEHICULO VH ON C.IdUnidad = VH.IdVehiculo
											LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = VH.SubTipoVehiculo
											WHERE C.Fecha = @x_FechaSelec AND C.idCondicion = @idCondicion AND LTRIM(RTRIM(SV.Descripcion)) = @TipoUnidad)

				UPDATE T 
				SET T.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN (CASE WHEN T.D1 - 1 = 0 THEN NULL ELSE T.D1 - 1 END) ELSE T.D1 END,
					T.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN (CASE WHEN T.D2 - 1 = 0 THEN NULL ELSE T.D2 - 1 END) ELSE T.D2 END,
					T.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN (CASE WHEN T.D3 - 1 = 0 THEN NULL ELSE T.D3 - 1 END) ELSE T.D3 END,
					T.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN (CASE WHEN T.D4 - 1 = 0 THEN NULL ELSE T.D4 - 1 END) ELSE T.D4 END,
					T.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN (CASE WHEN T.D5 - 1 = 0 THEN NULL ELSE T.D5 - 1 END) ELSE T.D5 END,
					T.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN (CASE WHEN T.D6 - 1 = 0 THEN NULL ELSE T.D6 - 1 END) ELSE T.D6 END,
					T.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN (CASE WHEN T.D7 - 1 = 0 THEN NULL ELSE T.D7 - 1 END) ELSE T.D7 END,
					T.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN (CASE WHEN T.D8 - 1 = 0 THEN NULL ELSE T.D8 - 1 END) ELSE T.D8 END,
					T.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN (CASE WHEN T.D9 - 1 = 0 THEN NULL ELSE T.D9 - 1 END) ELSE T.D9 END,
					T.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN (CASE WHEN T.D10 - 1 = 0 THEN NULL ELSE T.D10 - 1 END) ELSE T.D10 END,
					T.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN (CASE WHEN T.D11 - 1 = 0 THEN NULL ELSE T.D11 - 1 END) ELSE T.D11 END,
					T.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN (CASE WHEN T.D12 - 1 = 0 THEN NULL ELSE T.D12 - 1 END) ELSE T.D12 END,
					T.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN (CASE WHEN T.D13 - 1 = 0 THEN NULL ELSE T.D13 - 1 END) ELSE T.D13 END,
					T.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN (CASE WHEN T.D14 - 1 = 0 THEN NULL ELSE T.D14 - 1 END) ELSE T.D14 END,
					T.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN (CASE WHEN T.D15 - 1 = 0 THEN NULL ELSE T.D15 - 1 END) ELSE T.D15 END,
					T.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN (CASE WHEN T.D16 - 1 = 0 THEN NULL ELSE T.D16 - 1 END) ELSE T.D16 END,
					T.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN (CASE WHEN T.D17 - 1 = 0 THEN NULL ELSE T.D17 - 1 END) ELSE T.D17 END,
					T.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN (CASE WHEN T.D18 - 1 = 0 THEN NULL ELSE T.D18 - 1 END) ELSE T.D18 END,
					T.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN (CASE WHEN T.D19 - 1 = 0 THEN NULL ELSE T.D19 - 1 END) ELSE T.D19 END,
					T.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN (CASE WHEN T.D20 - 1 = 0 THEN NULL ELSE T.D20 - 1 END) ELSE T.D20 END,
					T.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN (CASE WHEN T.D21 - 1 = 0 THEN NULL ELSE T.D21 - 1 END) ELSE T.D21 END,
					T.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN (CASE WHEN T.D22 - 1 = 0 THEN NULL ELSE T.D22 - 1 END) ELSE T.D22 END,
					T.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN (CASE WHEN T.D23 - 1 = 0 THEN NULL ELSE T.D23 - 1 END) ELSE T.D23 END,
					T.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN (CASE WHEN T.D24 - 1 = 0 THEN NULL ELSE T.D24 - 1 END) ELSE T.D24 END,
					T.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN (CASE WHEN T.D25 - 1 = 0 THEN NULL ELSE T.D25 - 1 END) ELSE T.D25 END,
					T.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN (CASE WHEN T.D26 - 1 = 0 THEN NULL ELSE T.D26 - 1 END) ELSE T.D26 END,
					T.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN (CASE WHEN T.D27 - 1 = 0 THEN NULL ELSE T.D27 - 1 END) ELSE T.D27 END,
					T.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN (CASE WHEN T.D28 - 1 = 0 THEN NULL ELSE T.D28 - 1 END) ELSE T.D28 END,
					T.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN (CASE WHEN T.D29 - 1 = 0 THEN NULL ELSE T.D29 - 1 END) ELSE T.D29 END,
					T.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN (CASE WHEN T.D30 - 1 = 0 THEN NULL ELSE T.D30 - 1 END) ELSE T.D30 END,
					T.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN (CASE WHEN T.D31 - 1 = 0 THEN NULL ELSE T.D31 - 1 END) ELSE T.D31 END
				FROM ReportesApp_Operaciones_Operatividad_ViewCisternas T
				WHERE T.Anio = YEAR(@x_FechaSelec) AND T.Mes = MONTH(@x_FechaSelec) AND T.idCondicionCS = @CondicionAnterior

				UPDATE T 
				SET T.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN @CantidadCondiciones ELSE T.D1 END,
					T.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN @CantidadCondiciones ELSE T.D2 END,
					T.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN @CantidadCondiciones ELSE T.D3 END,
					T.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN @CantidadCondiciones ELSE T.D4 END,
					T.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN @CantidadCondiciones ELSE T.D5 END,
					T.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN @CantidadCondiciones ELSE T.D6 END,
					T.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN @CantidadCondiciones ELSE T.D7 END,
					T.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN @CantidadCondiciones ELSE T.D8 END,
					T.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN @CantidadCondiciones ELSE T.D9 END,
					T.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN @CantidadCondiciones ELSE T.D10 END,
					T.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN @CantidadCondiciones ELSE T.D11 END,
					T.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN @CantidadCondiciones ELSE T.D12 END,
					T.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN @CantidadCondiciones ELSE T.D13 END,
					T.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN @CantidadCondiciones ELSE T.D14 END,
					T.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN @CantidadCondiciones ELSE T.D15 END,
					T.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN @CantidadCondiciones ELSE T.D16 END,
					T.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN @CantidadCondiciones ELSE T.D17 END,
					T.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN @CantidadCondiciones ELSE T.D18 END,
					T.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN @CantidadCondiciones ELSE T.D19 END,
					T.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN @CantidadCondiciones ELSE T.D20 END,
					T.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN @CantidadCondiciones ELSE T.D21 END,
					T.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN @CantidadCondiciones ELSE T.D22 END,
					T.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN @CantidadCondiciones ELSE T.D23 END,
					T.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN @CantidadCondiciones ELSE T.D24 END,
					T.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN @CantidadCondiciones ELSE T.D25 END,
					T.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN @CantidadCondiciones ELSE T.D26 END,
					T.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN @CantidadCondiciones ELSE T.D27 END,
					T.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN @CantidadCondiciones ELSE T.D28 END,
					T.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN @CantidadCondiciones ELSE T.D29 END,
					T.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN @CantidadCondiciones ELSE T.D30 END,
					T.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN @CantidadCondiciones ELSE T.D31 END
				FROM ReportesApp_Operaciones_Operatividad_ViewCisternas T
				WHERE T.Anio = YEAR(@x_FechaSelec) AND T.Mes = MONTH(@x_FechaSelec) AND T.idCondicionCS = @idCondicion
			END
		END

		SET @exito = '0 = Reporte actualizado'
	END
	ELSE BEGIN
		SET @exito = '-1 = No se pudo registrar el reporte'
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

-----------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------

DECLARE @OP_PLACAS TABLE (Numero INT, IDTicket INT, Anio SMALLINT, IDTracto INT, Estado INT, TipoProgramacion INT,
FInicio DATETIME, FFin DATETIME, Usuario VARCHAR(80))
DECLARE @Contador INT = 1

INSERT INTO @OP_PLACAS(Numero, IDTicket, Anio, IDTracto, Estado, TipoProgramacion, FInicio, FFin, Usuario)
SELECT ROW_NUMBER() OVER(ORDER BY X.IdProgramacion ASC), X.IdProgramacion, X.Anio, X.idSemirremolque, X.Estado, X.TipoProgramacion,
X.FechaInicio, X.FechaFin, X.Usuario FROM
(SELECT IdProgramacion, Anio, idSemirremolque, Estado, TipoProgramacion,
FechaProgramacion AS 'FechaInicio', GETDATE() AS 'FechaFin', 'AUTOMATICO' AS 'Usuario'
FROM ReportesApp_Operacion_Previaje_Registros
WHERE CONVERT(DATE,FechaProgramacion) >= CONVERT(DATE,DATEADD(DAY,-5,GETDATE())) AND (FechaTermino IS NULL) AND TipoProgramacion IN (1,2,3,4) AND Estado IN (1,9) AND idSemirremolque != -1
UNION
SELECT IdProgramacion, Anio, idSemirremolque, Estado, TipoProgramacion,
FechaProgramacion AS 'FechaInicio', GETDATE() AS 'FechaFin', 'AUTOMATICO' AS 'Usuario'
FROM ReportesApp_Operacion_Previaje_Registros
WHERE CONVERT(DATE,FechaTermino) >= CONVERT(DATE,DATEADD(DAY,-5,GETDATE())) AND TipoProgramacion IN (1,2,3,4) AND Estado IN (1,9) AND idSemirremolque != -1) X

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

------------------------------------------------------------------

DECLARE @OP_PLACAS TABLE (Numero INT, NroTicket INT, IDTracto INT, FInicio DATETIME, Usuario VARCHAR(80))
DECLARE @Contador INT = 1

INSERT INTO @OP_PLACAS(Numero, NroTicket, IDTracto, FInicio, Usuario)
SELECT DISTINCT ROW_NUMBER() OVER(ORDER BY T.NroTicket ASC), T.NroTicket, T.idCarreta, T.FechaViaje, 'AUTOMATICO'
FROM ReportesApp_Operaciones_Viajes_Tolvas T 
WHERE (EstadoViaje IN ('EJECUCION','COMPLETADO')) AND CONVERT(DATE,T.FechaViaje) = CONVERT(DATE,GETDATE())

WHILE (@Contador <= (SELECT COUNT(Numero) FROM @OP_PLACAS)) BEGIN
	DECLARE @NroTicket INT = (SELECT NroTicket FROM @OP_PLACAS WHERE Numero = @Contador)
	DECLARE @IDTracto INT = (SELECT IDTracto FROM @OP_PLACAS WHERE Numero = @Contador)
	DECLARE @FInicio DATE = (SELECT FInicio FROM @OP_PLACAS WHERE Numero = @Contador)
	DECLARE @Usuario VARCHAR(80) = (SELECT Usuario FROM @OP_PLACAS WHERE Numero = @Contador)

	EXEC ReportesApp_Operaciones_Operatividad_ActualizarViajeTolvasCarretas @NroTicket = @NroTicket, @IDTracto = @IDTracto, @FechaInicio = @FInicio, @Usuario = @Usuario
	
	SET @Contador = @Contador + 1
END

--------------------------------------------------------------------------------------------------------------

DECLARE @OP_PLACAS TABLE (Numero INT, IDCondicion INT, IDTracto INT, FInicio DATE, Usuario VARCHAR(80))
DECLARE @Contador INT = 1
DECLARE @idCondicion INT, @IDTracto INT, @FInicio DATE, @Usuario VARCHAR(80)

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

INSERT INTO @OP_PLACAS(Numero, IDCondicion, IDTracto, FInicio, Usuario)
SELECT DISTINCT ROW_NUMBER() OVER(ORDER BY S.idSolicitud ASC), 6, S.idTracto, CONVERT(DATE,GETDATE()), 'AUTOMATICO'
FROM ReportesApp_Mantenimiento_Solicitud_Registro S
WHERE Estado = 'RECEPCIONADO' AND FechaRecepcion BETWEEN '01/08/2024' AND GETDATE()
UNION
SELECT DISTINCT ROW_NUMBER() OVER(ORDER BY S.idSolicitud ASC), 6, S.idTracto, CONVERT(DATE,GETDATE()), 'AUTOMATICO'
FROM ReportesApp_Mantenimiento_Solicitud_Registro S
WHERE CONVERT(DATE,FechaEntrega) = CONVERT(DATE,GETDATE()) AND CONVERT(TIME,FechaEntrega) >= '16:00:00'

WHILE (@Contador <= (SELECT COUNT(Numero) FROM @OP_PLACAS)) BEGIN
	SET @idCondicion = (SELECT IDCondicion FROM @OP_PLACAS WHERE Numero = @Contador)
	SET @IDTracto = (SELECT IDTracto FROM @OP_PLACAS WHERE Numero = @Contador)
	SET @FInicio = (SELECT FInicio FROM @OP_PLACAS WHERE Numero = @Contador)
	SET @Usuario = (SELECT Usuario FROM @OP_PLACAS WHERE Numero = @Contador)

	EXEC ReportesApp_Operaciones_Operatividad_ActualizarViajeOtrosCarretas @IDCondicion = 6, @IDTracto = @IDTracto, @FechaInicio = @FInicio, @Usuario = @Usuario
	
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