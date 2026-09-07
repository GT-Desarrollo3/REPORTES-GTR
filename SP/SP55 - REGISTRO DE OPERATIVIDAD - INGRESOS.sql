
-- CREAR TABLA ReportesApp_Operaciones_Operatividad_Ingresos

------------------------------------------------------------------------------------------

SELECT *, CASE WHEN X.IdOperacion = 1 THEN X.CANTIDAD * 1098 WHEN X.IdOperacion = 2 THEN X.CANTIDAD * 1315
WHEN X.IdOperacion = 3 THEN X.CANTIDAD * 1165 WHEN X.IdOperacion = 4 THEN X.CANTIDAD * 1185
WHEN X.IdOperacion = 10 THEN X.CANTIDAD * 1315 END AS 'INGRESO PERDIDO'
FROM (SELECT Fecha, IdOperacion, COUNT(IdOperacion) AS 'CANTIDAD'
FROM ReportesApp_Operaciones_Operatividad_Registro
WHERE idCondicion NOT IN (1,2,3,4,16) AND YEAR(Fecha) = 2025 AND MONTH(Fecha) = 8
GROUP BY Fecha, IdOperacion) X
ORDER BY X.Fecha ASC, X.IdOperacion ASC

-----------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 16-09-2024
-- Description:	INSERTAR INGRESOS X OPERACION
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Operatividad_InsertarIngresosOperacion]
@idOperacion INT,
@Periodo VARCHAR(6),
@Usuario VARCHAR(20)
AS
DECLARE @exito VARCHAR(MAX)
DECLARE @Dia DATE
DECLARE @FechaIni DATE, @FechaFin DATE
DECLARE @DOMINGOS TABLE(FECHA DATE)
DECLARE @T_OperacionDF TABLE(idOperacion INT, Descripcion VARCHAR(250), FechaIni DATE, FechaFin DATE, FechaDF DATE, ItemDF INT)
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

IF (@idOperacion = 4) BEGIN
	SET @idOperacion = 10
END 

INSERT INTO @T_OperacionDF(idOperacion,Descripcion,FechaIni,FechaFin,FechaDF,ItemDF)
SELECT X.IdOperacion,X.Descripcion,X.FechaDesde,X.FechaHasta,X.FECHA,
	ROW_NUMBER() OVER(ORDER BY X.idOperacion,X.FECHA) ItemDF
FROM
( SELECT O.IdOperacion, O.Descripcion, @FechaIni FechaDesde ,@FechaFin FechaHasta, D.FECHA
  FROM ReportesApp_Operacion_Previaje_Operaciones O WITH(NOLOCK)
  INNER JOIN @DOMINGOS D ON @FechaIni <= D.FECHA AND @FechaFin >= D.FECHA
  WHERE O.IdOperacion = @idOperacion ) X

BEGIN TRAN
BEGIN TRY
	IF (NOT EXISTS(SELECT * FROM ReportesApp_Operaciones_Operatividad_Ingresos WHERE idOperacion = @idOperacion
	AND Anio = RIGHT(@Periodo,4) AND Mes = LEFT(@Periodo,2))) BEGIN		-- CONTINUAR
		DECLARE @n INT, @UltimoDiaMes TINYINT = DAY(@fechafin), @idOperacionDF INT, @DescripcionDF VARCHAR(250), @FechaDF DATE

		SELECT @n = MIN(ItemDF) FROM @T_OperacionDF

		IF (@n IS NULL) BEGIN
			INSERT INTO ReportesApp_Operaciones_Operatividad_Ingresos (idOperacion,Anio,Mes,NroDias)
			VALUES(@idOperacion, RIGHT(@Periodo,4), LEFT(@Periodo,2), @UltimoDiaMes)
		END

		WHILE (@n IS NOT NULL) BEGIN
			SELECT @idOperacionDF = T.idOperacion, @FechaDF = T.FechaDF, @DescripcionDF = T.Descripcion
			FROM @T_OperacionDF T
			WHERE ItemDF = @n

			IF NOT EXISTS(SELECT TOP 1 Mes FROM ReportesApp_Operaciones_Operatividad_Ingresos WITH(NOLOCK) WHERE idOperacion = @idOperacionDF 
			AND Anio = YEAR(@FechaDF) AND Mes = MONTH(@FechaDF)) BEGIN
				INSERT INTO ReportesApp_Operaciones_Operatividad_Ingresos (idOperacion,Descripcion,Anio,Mes,NroDias,Orden)
				VALUES(@idOperacionDF, 'INGRESO PERDIDO - '+@DescripcionDF, YEAR(@FechaDF), MONTH(@FechaDF), @UltimoDiaMes,
				CASE WHEN @idOperacionDF = 1 THEN 3 WHEN @idOperacionDF = 2 THEN 1 WHEN @idOperacionDF = 3 THEN 2 
				WHEN @idOperacionDF = 10 THEN 4 END)
			END

			PRINT CAST(@n AS VARCHAR(10))+ ' --> @idOperacionDF:'+ CAST(@idOperacionDF AS VARCHAR(10)) +' @FechaDF: '+CONVERT(VARCHAR(10),@FechaDF,103)+' >>> 0'

			UPDATE ReportesApp_Operaciones_Operatividad_Ingresos SET 
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
			WHERE idOperacion = @idOperacionDF AND Anio = YEAR(@FechaDF) AND Mes = MONTH(@FechaDF)

			SELECT @n = MIN(ItemDF) FROM @T_OperacionDF WHERE @n < ItemDF
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
-- Create date: 05-04-2024
-- Description:	REGISTRAR OPERATIVIDAD - PLACAS
-- =============================================
/*
exec ReportesApp_Operaciones_Operatividad_RegistrarPlacas @Periodo=N'042024',@xmlOperatividad=N'<r><d IdUnidad="1" Fecha="01/04/2024" /></r>',
														  @idCondicion=1,@idOperacion=2,@Usuario=N'GREYES'
*/
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Operatividad_RegistrarPlacas]
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
		SET @co2 = (SELECT MAX(idOperatividad) FROM ReportesApp_Operaciones_Operatividad_Registro)
		SET @co2 = ISNULL(@co2,0) + 1 

		DECLARE @CondicionAnterior INT = (SELECT idCondicion FROM ReportesApp_Operaciones_Operatividad_Registro
		WHERE IdUnidad = (SELECT P.IdUnidad FROM @OP_PLACAS P WHERE P.Contador = @Nro) AND Fecha = (SELECT P.Fecha FROM @OP_PLACAS P WHERE P.Contador = @Nro))

		DELETE FROM ReportesApp_Operaciones_Operatividad_Registro
		WHERE IdUnidad = (SELECT P.IdUnidad FROM @OP_PLACAS P WHERE P.Contador = @Nro) AND Fecha = (SELECT P.Fecha FROM @OP_PLACAS P WHERE P.Contador = @Nro)

		INSERT INTO ReportesApp_Operaciones_Operatividad_Registro(idOperatividad,IdUnidad,Fecha,idCondicion,IdOperacion,UsuarioRegistra,FechaRegistra)
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
			SET @CantidadCondiciones = (SELECT COUNT(*) FROM ReportesApp_Operaciones_Operatividad_Registro
										WHERE Fecha = @x_FechaSelec AND idCondicion = @x_idCondicion)
			
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
			FROM ReportesApp_Operaciones_Operatividad_PlacasView P
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
			FROM ReportesApp_Operaciones_Operatividad_View T
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
			FROM ReportesApp_Operaciones_Operatividad_View T
			LEFT JOIN @OP_PLACAS O ON T.idCondicion = O.idCondicion AND O.Fecha = @x_FechaSelec
			WHERE T.Anio = RIGHT(@Periodo,4) AND T.Mes = LEFT(@periodo,2)
		END
		ELSE BEGIN
			SET @Exito = '-1 = No tiene permiso para registrar esta condición en la unidad.'
			ROLLBACK
			GOTO Terminar
		END

		DECLARE @ContOp INT = 1

		WHILE (@ContOp <= 4) BEGIN
			IF (@ContOp = 4) BEGIN
				SET @ContOp = 10
			END 

			EXEC ReportesApp_Operaciones_Operatividad_RegistrarIngresos @idOperacion = @ContOp, @Periodo=@Periodo, @Usuario = @Usuario

			SET @ContOp = @ContOp + 1
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
-- Create date: 16-09-2024
-- Description:	REGISTRAR INGRESOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Operatividad_RegistrarIngresos]
@idOperacion INT,
@Periodo VARCHAR(6),
@Usuario VARCHAR(80)
AS	
DECLARE @exito VARCHAR(MAX)	

BEGIN TRAN
BEGIN TRY
	DECLARE @T_Prueba TABLE (Fecha DATE, IdOperacion INT, IngresoPerdido DECIMAL(10,2))
	DECLARE @Anio VARCHAR(4) = RIGHT(@Periodo,4)
	DECLARE @Mes VARCHAR(2) = LEFT(@Periodo,2)
	DECLARE @Dia INT = 1
	DECLARE @CantidadDias INT = (SELECT NroDias FROM ReportesApp_Operaciones_Operatividad_Ingresos WHERE Anio = @Anio AND Mes = @Mes AND idOperacion = @idOperacion)

	INSERT INTO @T_Prueba (Fecha, IdOperacion, IngresoPerdido)
	SELECT X.Fecha, X.IdOperacion, CASE WHEN X.IdOperacion = 1 THEN X.CANTIDAD * 1098 WHEN X.IdOperacion = 2 THEN X.CANTIDAD * 1315
	WHEN X.IdOperacion = 3 THEN X.CANTIDAD * 1165 WHEN X.IdOperacion = 4 THEN X.CANTIDAD * 1185 WHEN X.IdOperacion = 10 THEN X.CANTIDAD * 1315 END AS 'INGRESO PERDIDO'
	FROM (SELECT Fecha, IdOperacion, COUNT(IdOperacion) AS 'CANTIDAD'
	FROM ReportesApp_Operaciones_Operatividad_Registro
	WHERE idCondicion NOT IN (1,2,3,4,16) AND YEAR(Fecha) = RIGHT(@Periodo,4) AND MONTH(Fecha) = LEFT(@periodo,2) AND IdOperacion = @idOperacion
	GROUP BY Fecha, IdOperacion) X
	ORDER BY X.Fecha ASC, X.IdOperacion ASC

	UPDATE T 
	SET T.D1 = NULL, T.D2 = NULL, T.D3 = NULL, T.D4 = NULL, T.D5 = NULL, T.D6 = NULL, T.D7 = NULL, T.D8 = NULL, T.D9 = NULL, T.D10 = NULL,
		T.D11 = NULL, T.D12 = NULL, T.D13 = NULL, T.D14 = NULL, T.D15 = NULL, T.D16 = NULL, T.D17 = NULL, T.D18 = NULL, T.D19 = NULL, T.D20 = NULL,
		T.D21 = NULL, T.D22 = NULL, T.D23 = NULL, T.D24 = NULL, T.D25 = NULL, T.D26 = NULL, T.D27 = NULL, T.D28 = NULL, T.D29 = NULL, T.D30 = NULL, T.D31 = NULL
	FROM ReportesApp_Operaciones_Operatividad_Ingresos T
	WHERE T.Anio = RIGHT(@Periodo,4) AND T.Mes = LEFT(@periodo,2) AND T.idOperacion = @idOperacion

	WHILE (@Dia <= @CantidadDias) BEGIN
		DECLARE @FechaDia VARCHAR(2) = CONVERT(VARCHAR(2),RIGHT('00'+LTRIM(RTRIM(@Dia)),2))
		DECLARE @x_FechaSelec DATE = CONVERT(DATE,@FechaDia+'/'+@Mes+'/'+@Anio)

		IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_Operatividad_Ingresos WHERE Anio = YEAR(@x_FechaSelec) AND Mes = MONTH(@x_FechaSelec)
		AND idOperacion = @idOperacion)) BEGIN
			DECLARE @IngresoPerdido VARCHAR(50) = (SELECT CONVERT(VARCHAR(250),FORMAT(IngresoPerdido,'#,##0')) FROM @T_Prueba WHERE Fecha = @x_FechaSelec AND idOperacion = @idOperacion)
		
			UPDATE T 
			SET T.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 THEN @IngresoPerdido ELSE T.D1 END,
				T.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 THEN @IngresoPerdido ELSE T.D2 END,
				T.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 THEN @IngresoPerdido ELSE T.D3 END,
				T.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 THEN @IngresoPerdido ELSE T.D4 END,
				T.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 THEN @IngresoPerdido ELSE T.D5 END,
				T.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 THEN @IngresoPerdido ELSE T.D6 END,
				T.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 THEN @IngresoPerdido ELSE T.D7 END,
				T.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 THEN @IngresoPerdido ELSE T.D8 END,
				T.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 THEN @IngresoPerdido ELSE T.D9 END,
				T.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 THEN @IngresoPerdido ELSE T.D10 END,
				T.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 THEN @IngresoPerdido ELSE T.D11 END,
				T.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 THEN @IngresoPerdido ELSE T.D12 END,
				T.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 THEN @IngresoPerdido ELSE T.D13 END,
				T.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 THEN @IngresoPerdido ELSE T.D14 END,
				T.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 THEN @IngresoPerdido ELSE T.D15 END,
				T.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 THEN @IngresoPerdido ELSE T.D16 END,
				T.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 THEN @IngresoPerdido ELSE T.D17 END,
				T.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 THEN @IngresoPerdido ELSE T.D18 END,
				T.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 THEN @IngresoPerdido ELSE T.D19 END,
				T.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 THEN @IngresoPerdido ELSE T.D20 END,
				T.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 THEN @IngresoPerdido ELSE T.D21 END,
				T.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 THEN @IngresoPerdido ELSE T.D22 END,
				T.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 THEN @IngresoPerdido ELSE T.D23 END,
				T.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 THEN @IngresoPerdido ELSE T.D24 END,
				T.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 THEN @IngresoPerdido ELSE T.D25 END,
				T.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 THEN @IngresoPerdido ELSE T.D26 END,
				T.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 THEN @IngresoPerdido ELSE T.D27 END,
				T.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 THEN @IngresoPerdido ELSE T.D28 END,
				T.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 THEN @IngresoPerdido ELSE T.D29 END,
				T.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 THEN @IngresoPerdido ELSE T.D30 END,
				T.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 THEN @IngresoPerdido ELSE T.D31 END
			FROM ReportesApp_Operaciones_Operatividad_Ingresos T
			WHERE T.Anio = YEAR(@x_FechaSelec) AND T.Mes = MONTH(@x_FechaSelec) AND T.idOperacion = @idOperacion
	
			SET @exito = '0 = Reporte actualizado'
		END
		ELSE BEGIN
			SET @exito = '-1 = No se pudo registrar el reporte'
		END

		SET @Dia = @Dia + 1
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

------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author Name:	GERARDO REYES
-- Create date: 06-09-2024
-- Description:	LISTAR TABLA DE OPERATIVIDAD - INGRESO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Operatividad_ListarTablaIngresos]
@Periodo VARCHAR(6)
AS
BEGIN
	DECLARE @Anio SMALLINT = RIGHT(@Periodo,4)
	DECLARE @Mes INT = LEFT(@Periodo,2)
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

	DECLARE @T_Prueba TABLE (idOperacion INT, Operativas VARCHAR(50), Operacion VARCHAR(250), Orden INT,
							 D1 VARCHAR(15), D2 VARCHAR(15), D3 VARCHAR(15), D4 VARCHAR(15), D5 VARCHAR(15), D6 VARCHAR(15),
							 D7 VARCHAR(15), D8 VARCHAR(15), D9 VARCHAR(15), D10 VARCHAR(15), D11 VARCHAR(15), D12 VARCHAR(15),
							 D13 VARCHAR(15), D14 VARCHAR(15), D15 VARCHAR(15), D16 VARCHAR(15), D17 VARCHAR(15), D18 VARCHAR(15),
							 D19 VARCHAR(15), D20 VARCHAR(15), D21 VARCHAR(15), D22 VARCHAR(15), D23 VARCHAR(15), D24 VARCHAR(15),
							 D25 VARCHAR(15), D26 VARCHAR(15), D27 VARCHAR(15), D28 VARCHAR(15), D29 VARCHAR(15), D30 VARCHAR(15), D31 VARCHAR(15))

	DECLARE @D1 VARCHAR(15), @D2 VARCHAR(15), @D3 VARCHAR(15), @D4 VARCHAR(15), @D5 VARCHAR(15), @D6 VARCHAR(15), @D7 VARCHAR(15), @D8 VARCHAR(15),
			@D9 VARCHAR(15), @D10 VARCHAR(15), @D11 VARCHAR(15), @D12 VARCHAR(15), @D13 VARCHAR(15), @D14 VARCHAR(15), @D15 VARCHAR(15), @D16 VARCHAR(15),
			@D17 VARCHAR(15), @D18 VARCHAR(15), @D19 VARCHAR(15), @D20 VARCHAR(15), @D21 VARCHAR(15), @D22 VARCHAR(15), @D23 VARCHAR(15), @D24 VARCHAR(15),
			@D25 VARCHAR(15), @D26 VARCHAR(15), @D27 VARCHAR(15), @D28 VARCHAR(15), @D29 VARCHAR(15), @D30 VARCHAR(15), @D31 VARCHAR(15)

	INSERT INTO @T_Prueba (Operativas,Operacion)
	VALUES ('Operativas', '<OPERACION>')

	DECLARE @FechaIni DATE, @FechaFin DATE, @FechaVer DATE

	IF (@FDesde IS NOT NULL) BEGIN 
		SET @FechaVer = CAST(CAST(@DiaInicio As Varchar(2))+'/'+ RIGHT('0'+ CAST(month(@FDesde) AS VARCHAR(2)),2)+'/'+CAST(@Anio as VARCHAR(4)) AS DATE)
	END
	ELSE BEGIN
		SET @FechaVer = CAST(CAST(@DiaInicio As Varchar(2))+'/'+ RIGHT('0'+ CAST(@Mes AS VARCHAR(2)),2)+'/'+CAST(@Anio as VARCHAR(4)) AS DATE)
	END

	SET @FechaIni = @FechaVer
	SET @FechaFin = DATEADD(D,-1, DATEADD(MONTH,1,@FechaVer))

	INSERT INTO @T_Prueba (idOperacion, Operativas, Operacion, Orden)
	SELECT DISTINCT idOperacion, 'NO OPERATIVAS', Descripcion, Orden
	FROM ReportesApp_Operaciones_Operatividad_Ingresos
	WHERE Anio = RIGHT(@Periodo,4) AND Mes = LEFT(@Periodo,2)

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
		WHERE Operacion = '<OPERACION>'

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
		LEFT JOIN ReportesApp_Operaciones_Operatividad_Ingresos AS A WITH(NOLOCK) ON T.idOperacion = A.idOperacion 
		WHERE A.Anio = YEAR(@FechaVer) AND A.Mes = MONTH(@FechaVer) 

		SET @FechaVer = DATEADD(D,1,@FechaVer)
			
		IF (@FDesde IS NOT NULL) AND (@NroDia = @CantDias OR @CantDias = 0)  BREAK;
		
		SET @NroDia = @NroDia + 1
	END

	IF (@CantDias = 28) BEGIN
		SELECT @D1=D1, @D2=D2, @D3=D3, @D4=D4, @D5=D5, @D6=D6, @D7=D7, @D8=D8, @D9=D9, @D10=D10, @D11=D11, @D12=D12, @D13=D13, @D14=D14,
		@D15=D15, @D16=D16, @D17=D17, @D18=D18, @D19=D19, @D20=D20, @D21=D21, @D22=D22, @D23=D23, @D24=D24, @D25=D25, @D26=D26, @D27=D27, @D28=D28
		FROM @T_Prueba WHERE Operacion = '<OPERACION>'
	END

	IF (@CantDias = 29) BEGIN
		SELECT @D1=D1, @D2=D2, @D3=D3, @D4=D4, @D5=D5, @D6=D6, @D7=D7, @D8=D8, @D9=D9, @D10=D10, @D11=D11, @D12=D12, @D13=D13, @D14=D14, @D15=D15,
		@D16=D16, @D17=D17, @D18=D18, @D19=D19, @D20=D20, @D21=D21, @D22=D22, @D23=D23, @D24=D24, @D25=D25, @D26=D26, @D27=D27, @D28=D28, @D29=D29
		FROM @T_Prueba WHERE Operacion = '<OPERACION>'
	END

	IF (@CantDias = 30) BEGIN
		SELECT @D1=D1, @D2=D2, @D3=D3, @D4=D4, @D5=D5, @D6=D6, @D7=D7, @D8=D8, @D9=D9, @D10=D10, @D11=D11, @D12=D12, @D13=D13, @D14=D14, @D15=D15,
		@D16=D16, @D17=D17, @D18=D18, @D19=D19, @D20=D20, @D21=D21, @D22=D22, @D23=D23, @D24=D24, @D25=D25, @D26=D26, @D27=D27, @D28=D28, @D29=D29, @D30=D30
		FROM @T_Prueba WHERE Operacion = '<OPERACION>'
	END

	IF (@CantDias = 31) BEGIN
		SELECT @D1=D1, @D2=D2, @D3=D3, @D4=D4, @D5=D5, @D6=D6, @D7=D7, @D8=D8, @D9=D9, @D10=D10, @D11=D11, @D12=D12, @D13=D13, @D14=D14, @D15=D15, @D16=D16, @D17=D17,
		@D18=D18, @D19=D19, @D20=D20, @D21=D21, @D22=D22, @D23=D23, @D24=D24, @D25=D25, @D26=D26, @D27=D27, @D28=D28, @D29=D29, @D30=D30, @D29=D29, @D30=D30, @D31=D31
		FROM @T_Prueba WHERE Operacion = '<OPERACION>'
	END

	CREATE TABLE #PruebaView (idOperacion INT, Operativas VARCHAR(50), Operacion VARCHAR(250), Orden INT,
							  D1 VARCHAR(15),D2 VARCHAR(15),D3 VARCHAR(15),D4 VARCHAR(15),D5 VARCHAR(15),
							  D6 VARCHAR(15),D7 VARCHAR(15),D8 VARCHAR(15),D9 VARCHAR(15),D10 VARCHAR(15),
							  D11 VARCHAR(15),D12 VARCHAR(15),D13 VARCHAR(15),D14 VARCHAR(15),D15 VARCHAR(15),
							  D16 VARCHAR(15),D17 VARCHAR(15),D18 VARCHAR(15),D19 VARCHAR(15),D20 VARCHAR(15),
							  D21 VARCHAR(15),D22 VARCHAR(15),D23 VARCHAR(15),D24 VARCHAR(15),D25 VARCHAR(15),
							  D26 VARCHAR(15),D27 VARCHAR(15),D28 VARCHAR(15),D29 VARCHAR(15),D30 VARCHAR(15),
							  D31 VARCHAR(15))

	DECLARE @SQL varchar(5000)

	INSERT INTO #PruebaView
	SELECT * FROM @T_Prueba WHERE Operacion<>'<OPERACION>'

	IF @CantDias = 28 BEGIN
		SET @SQL='SELECT idOperacion, Operativas AS CONCEPTO, Operacion AS OPERACION,
			D1 AS '+@D1+',D2 AS '+@D2+',D3 AS '+@D3+',D4 AS '+@D4+',D5 AS '+@D5+',D6 AS '+@D6+',D7 AS '+@D7+',D8 AS '+@D8+
		    ',D9 AS '+@D9+',D10 AS '+@D10+',D11 AS '+@D11+',D12 AS '+@D12+',D13 AS '+@D13+',D14 AS '+@D14+',D15 AS '+@D15+
			',D16 AS '+@D16+',D17 AS '+@D17+',D18 AS '+@D18+',D19 AS '+@D19+',D20 AS '+@D20+',D21 AS '+@D21+',D22 AS '+@D22+
			',D23 AS '+@D23+',D24 AS '+@D24+',D25 AS '+@D25+',D26 AS '+@D26+',D27 AS '+@D27+',D28 AS '+@D28+' FROM #PruebaView ORDER BY Orden'
		
		EXEC (@SQL)
		DROP TABLE #PruebaView
	END

	IF @CantDias = 29 BEGIN
		SET @SQL='SELECT idOperacion, Operativas AS CONCEPTO, Operacion AS OPERACION,
			D1 AS '+@D1+',D2 AS '+@D2+',D3 AS '+@D3+',D4 AS '+@D4+',D5 AS '+@D5+',D6 AS '+@D6+',D7 AS '+@D7+',D8 AS '+@D8+
			',D9 AS '+@D9+',D10 AS '+@D10+',D11 AS '+@D11+',D12 AS '+@D12+',D13 AS '+@D13+',D14 AS '+@D14+',D15 AS '+@D15+
			',D16 AS '+@D16+',D17 AS '+@D17+',D18 AS '+@D18+',D19 AS '+@D19+',D20 AS '+@D20+',D21 AS '+@D21+',D22 AS '+@D22+
			',D23 AS '+@D23+',D24 AS '+@D24+',D25 AS '+@D25+',D26 AS '+@D26+',D27 AS '+@D27+',D28 AS '+@D28+',D29 AS '+@D29+' FROM #PruebaView ORDER BY Orden'
		
		EXEC (@SQL)
		DROP TABLE #PruebaView	
	END

	IF @CantDias = 30 BEGIN
		SET @SQL='SELECT idOperacion, Operativas AS CONCEPTO, Operacion AS OPERACION,
			D1 AS '+@D1+',D2 AS '+@D2+',D3 AS '+@D3+',D4 AS '+@D4+',D5 AS '+@D5+',D6 AS '+@D6+',D7 AS '+@D7+',D8 AS '+@D8+
			',D9 AS '+@D9+',D10 AS '+@D10+',D11 AS '+@D11+',D12 AS '+@D12+',D13 AS '+@D13+',D14 AS '+@D14+',D15 AS '+@D15+
			',D16 AS '+@D16+',D17 AS '+@D17+',D18 AS '+@D18+',D19 AS '+@D19+',D20 AS '+@D20+',D21 AS '+@D21+',D22 AS '+@D22+
			',D23 AS '+@D23+',D24 AS '+@D24+',D25 AS '+@D25+',D26 AS '+@D26+',D27 AS '+@D27+',D28 AS '+@D28+',D29 AS '+@D29+
			',D30 AS '+@D30+' FROM #PruebaView ORDER BY Orden'
		
		EXEC (@SQL)
		DROP TABLE #PruebaView
	END

	IF @CantDias = 31 BEGIN
		SET @SQL='SELECT idOperacion, Operativas AS CONCEPTO, Operacion AS OPERACION,
			D1 AS '+@D1+',D2 AS '+@D2+',D3 AS '+@D3+',D4 AS '+@D4+',D5 AS '+@D5+',D6 AS '+@D6+',D7 AS '+@D7+',D8 AS '+@D8+
			',D9 AS '+@D9+',D10 AS '+@D10+',D11 AS '+@D11+',D12 AS '+@D12+',D13 AS '+@D13+',D14 AS '+@D14+',D15 AS '+@D15+
			',D16 AS '+@D16+',D17 AS '+@D17+',D18 AS '+@D18+',D19 AS '+@D19+',D20 AS '+@D20+',D21 AS '+@D21+',D22 AS '+@D22+
			',D23 AS '+@D23+',D24 AS '+@D24+',D25 AS '+@D25+',D26 AS '+@D26+',D27 AS '+@D27+',D28 AS '+@D28+',D29 AS '+@D29+
			',D30 AS '+@D30+',D31 AS '+@D31+' FROM #PruebaView ORDER BY Orden'
		
		EXEC (@SQL)
		DROP TABLE #PruebaView
	END
END


