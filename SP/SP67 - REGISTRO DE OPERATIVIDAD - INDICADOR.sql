
-- CREAR TABLA ReportesApp_Operaciones_Operatividad_IndicadorView

-- CREAR TABLA ReportesApp_Operaciones_Operatividad_IndicadorIngresos

-------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------

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

------------------------------------------------------------------------------------------------------

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

------------------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 02-07-2025
-- Description:	LISTAR TOTAL DE TRACTOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Operatividad_ListarTractosTotal]
@Mes INT,
@Anio INT
AS
BEGIN
	SELECT ISNULL(COUNT(*),0) AS 'NRO' FROM ReportesApp_Operaciones_Operatividad_Registro
	WHERE MONTH(Fecha) = @Mes AND YEAR(Fecha) = @Anio AND DAY(Fecha) = 1
END

------------------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 03-01-2025
-- Description:	MAPEAR INDICADORES CONDICIONES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Operatividad_MapearIndicadores]
@Periodo VARCHAR(6),
@Usuario VARCHAR(20)
AS
DECLARE @exito VARCHAR(MAX)

SET @exito = '0 = Mapeo generado correctamente.'

BEGIN TRAN
BEGIN TRY
	IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_Operatividad_IndicadorView WHERE Anio = RIGHT(@Periodo,4))) BEGIN
		SET @Exito = '-1 = Ya existe un indicador creado para este año.'
		ROLLBACK
		GOTO Terminar
	END

	IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_Operatividad_IndicadorIngresos WHERE Anio = RIGHT(@Periodo,4))) BEGIN
		SET @Exito = '-1 = Ya existe un indicador creado para este año.'
		ROLLBACK
		GOTO Terminar
	END


	DECLARE @i INT = 1, @j INT = 1

	WHILE @i <= 16 BEGIN
		INSERT INTO ReportesApp_Operaciones_Operatividad_IndicadorView (idCondicion,Anio)
		VALUES(@i, RIGHT(@Periodo,4))
		
		SET @i = @i + 1
	END

	WHILE @j <= 4 BEGIN
		IF (@j = 4) BEGIN
			SET @j = 10
		END 

		INSERT INTO ReportesApp_Operaciones_Operatividad_IndicadorIngresos (idOperacion,Descripcion,Anio,Orden)
		SELECT O.IdOperacion, 'INGRESO PERDIDO - '+O.Descripcion, RIGHT(@Periodo,4), CASE WHEN @j = 1 THEN 3
	    WHEN @j = 2 THEN 1 WHEN @j = 3 THEN 2 WHEN @j = 10 THEN 4 END
		FROM ReportesApp_Operacion_Previaje_Operaciones O WITH(NOLOCK)
		WHERE O.IdOperacion = @j
		
		SET @j = @j + 1
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

-----------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author Name:	GERARDO REYES
-- Create date: 03-01-2025
-- Description:	LISTAR TABLA DE INDICADORES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Operatividad_ListarIndicadores]
@Opcion INT,
@Periodo VARCHAR(6)
AS
BEGIN
	DECLARE @Anio SMALLINT = RIGHT(@periodo,4)
	DECLARE @Mes SMALLINT = 1
	DECLARE @i INT = 1, @j INT = 1

	WHILE @Mes <= 12 BEGIN
		DECLARE @NumDias DECIMAL(10,2) = (SELECT DAY(EOMONTH('01/'+CONVERT(VARCHAR,@Mes)+'/'+CONVERT(VARCHAR,@Anio))))

		WHILE @i <= 16 BEGIN
			DECLARE @Vacios INT

			IF (@i IN (3)) BEGIN
				IF (@NumDias = 31) BEGIN
					SET @Vacios = (SELECT CASE WHEN D1 = 0 OR D1 IS NULL THEN 1 ELSE 0 END + CASE WHEN D2 = 0 OR D2 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D3 = 0 OR D3 IS NULL THEN 1 ELSE 0 END + CASE WHEN D4 = 0 OR D4 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D5 = 0 OR D5 IS NULL THEN 1 ELSE 0 END + CASE WHEN D6 = 0 OR D6 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D7 = 0 OR D7 IS NULL THEN 1 ELSE 0 END + CASE WHEN D8 = 0 OR D8 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D9 = 0 OR D9 IS NULL THEN 1 ELSE 0 END + CASE WHEN D10 = 0 OR D10 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D11 = 0 OR D11 IS NULL THEN 1 ELSE 0 END + CASE WHEN D12 = 0 OR D12 IS NULL THEN 1 ELSE 0 END
								   + CASE WHEN D13 = 0 OR D13 IS NULL THEN 1 ELSE 0 END + CASE WHEN D14 = 0 OR D14 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D15 = 0 OR D15 IS NULL THEN 1 ELSE 0 END + CASE WHEN D16 = 0 OR D16 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D17 = 0 OR D17 IS NULL THEN 1 ELSE 0 END + CASE WHEN D18 = 0 OR D18 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D19 = 0 OR D19 IS NULL THEN 1 ELSE 0 END + CASE WHEN D20 = 0 OR D20 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D21 = 0 OR D21 IS NULL THEN 1 ELSE 0 END + CASE WHEN D22 = 0 OR D22 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D23 = 0 OR D23 IS NULL THEN 1 ELSE 0 END + CASE WHEN D24 = 0 OR D24 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D25 = 0 OR D25 IS NULL THEN 1 ELSE 0 END + CASE WHEN D26 = 0 OR D26 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D27 = 0 OR D27 IS NULL THEN 1 ELSE 0 END + CASE WHEN D28 = 0 OR D28 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D29 = 0 OR D29 IS NULL THEN 1 ELSE 0 END + CASE WHEN D30 = 0 OR D30 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D31 = 0 OR D31 IS NULL THEN 1 ELSE 0 END AS CONTEO
								   FROM ReportesApp_Operaciones_Operatividad_View
								   WHERE idCondicion = @i AND Anio = @Anio AND Mes = @Mes)
				END

				IF (@NumDias = 30) BEGIN
					SET @Vacios = (SELECT CASE WHEN D1 = 0 OR D1 IS NULL THEN 1 ELSE 0 END + CASE WHEN D2 = 0 OR D2 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D3 = 0 OR D3 IS NULL THEN 1 ELSE 0 END + CASE WHEN D4 = 0 OR D4 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D5 = 0 OR D5 IS NULL THEN 1 ELSE 0 END + CASE WHEN D6 = 0 OR D6 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D7 = 0 OR D7 IS NULL THEN 1 ELSE 0 END + CASE WHEN D8 = 0 OR D8 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D9 = 0 OR D9 IS NULL THEN 1 ELSE 0 END + CASE WHEN D10 = 0 OR D10 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D11 = 0 OR D11 IS NULL THEN 1 ELSE 0 END + CASE WHEN D12 = 0 OR D12 IS NULL THEN 1 ELSE 0 END
								   + CASE WHEN D13 = 0 OR D13 IS NULL THEN 1 ELSE 0 END + CASE WHEN D14 = 0 OR D14 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D15 = 0 OR D15 IS NULL THEN 1 ELSE 0 END + CASE WHEN D16 = 0 OR D16 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D17 = 0 OR D17 IS NULL THEN 1 ELSE 0 END + CASE WHEN D18 = 0 OR D18 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D19 = 0 OR D19 IS NULL THEN 1 ELSE 0 END + CASE WHEN D20 = 0 OR D20 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D21 = 0 OR D21 IS NULL THEN 1 ELSE 0 END + CASE WHEN D22 = 0 OR D22 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D23 = 0 OR D23 IS NULL THEN 1 ELSE 0 END + CASE WHEN D24 = 0 OR D24 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D25 = 0 OR D25 IS NULL THEN 1 ELSE 0 END + CASE WHEN D26 = 0 OR D26 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D27 = 0 OR D27 IS NULL THEN 1 ELSE 0 END + CASE WHEN D28 = 0 OR D28 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D29 = 0 OR D29 IS NULL THEN 1 ELSE 0 END + CASE WHEN D30 = 0 OR D30 IS NULL THEN 1 ELSE 0 END 
								   FROM ReportesApp_Operaciones_Operatividad_View
								   WHERE idCondicion = @i AND Anio = @Anio AND Mes = @Mes)
				END

				IF (@NumDias = 29) BEGIN
					SET @Vacios = (SELECT CASE WHEN D1 = 0 OR D1 IS NULL THEN 1 ELSE 0 END + CASE WHEN D2 = 0 OR D2 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D3 = 0 OR D3 IS NULL THEN 1 ELSE 0 END + CASE WHEN D4 = 0 OR D4 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D5 = 0 OR D5 IS NULL THEN 1 ELSE 0 END + CASE WHEN D6 = 0 OR D6 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D7 = 0 OR D7 IS NULL THEN 1 ELSE 0 END + CASE WHEN D8 = 0 OR D8 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D9 = 0 OR D9 IS NULL THEN 1 ELSE 0 END + CASE WHEN D10 = 0 OR D10 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D11 = 0 OR D11 IS NULL THEN 1 ELSE 0 END + CASE WHEN D12 = 0 OR D12 IS NULL THEN 1 ELSE 0 END
								   + CASE WHEN D13 = 0 OR D13 IS NULL THEN 1 ELSE 0 END + CASE WHEN D14 = 0 OR D14 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D15 = 0 OR D15 IS NULL THEN 1 ELSE 0 END + CASE WHEN D16 = 0 OR D16 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D17 = 0 OR D17 IS NULL THEN 1 ELSE 0 END + CASE WHEN D18 = 0 OR D18 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D19 = 0 OR D19 IS NULL THEN 1 ELSE 0 END + CASE WHEN D20 = 0 OR D20 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D21 = 0 OR D21 IS NULL THEN 1 ELSE 0 END + CASE WHEN D22 = 0 OR D22 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D23 = 0 OR D23 IS NULL THEN 1 ELSE 0 END + CASE WHEN D24 = 0 OR D24 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D25 = 0 OR D25 IS NULL THEN 1 ELSE 0 END + CASE WHEN D26 = 0 OR D26 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D27 = 0 OR D27 IS NULL THEN 1 ELSE 0 END + CASE WHEN D28 = 0 OR D28 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D29 = 0 OR D29 IS NULL THEN 1 ELSE 0 END AS CONTEO
								   FROM ReportesApp_Operaciones_Operatividad_View
								   WHERE idCondicion = @i AND Anio = @Anio AND Mes = @Mes)
				END

				IF (@NumDias = 28) BEGIN
					SET @Vacios = (SELECT CASE WHEN D1 = 0 OR D1 IS NULL THEN 1 ELSE 0 END + CASE WHEN D2 = 0 OR D2 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D3 = 0 OR D3 IS NULL THEN 1 ELSE 0 END + CASE WHEN D4 = 0 OR D4 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D5 = 0 OR D5 IS NULL THEN 1 ELSE 0 END + CASE WHEN D6 = 0 OR D6 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D7 = 0 OR D7 IS NULL THEN 1 ELSE 0 END + CASE WHEN D8 = 0 OR D8 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D9 = 0 OR D9 IS NULL THEN 1 ELSE 0 END + CASE WHEN D10 = 0 OR D10 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D11 = 0 OR D11 IS NULL THEN 1 ELSE 0 END + CASE WHEN D12 = 0 OR D12 IS NULL THEN 1 ELSE 0 END
								   + CASE WHEN D13 = 0 OR D13 IS NULL THEN 1 ELSE 0 END + CASE WHEN D14 = 0 OR D14 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D15 = 0 OR D15 IS NULL THEN 1 ELSE 0 END + CASE WHEN D16 = 0 OR D16 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D17 = 0 OR D17 IS NULL THEN 1 ELSE 0 END + CASE WHEN D18 = 0 OR D18 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D19 = 0 OR D19 IS NULL THEN 1 ELSE 0 END + CASE WHEN D20 = 0 OR D20 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D21 = 0 OR D21 IS NULL THEN 1 ELSE 0 END + CASE WHEN D22 = 0 OR D22 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D23 = 0 OR D23 IS NULL THEN 1 ELSE 0 END + CASE WHEN D24 = 0 OR D24 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D25 = 0 OR D25 IS NULL THEN 1 ELSE 0 END + CASE WHEN D26 = 0 OR D26 IS NULL THEN 1 ELSE 0 END 
								   + CASE WHEN D27 = 0 OR D27 IS NULL THEN 1 ELSE 0 END + CASE WHEN D28 = 0 OR D28 IS NULL THEN 1 ELSE 0 END AS CONTEO
								   FROM ReportesApp_Operaciones_Operatividad_View
								   WHERE idCondicion = @i AND Anio = @Anio AND Mes = @Mes)
				END
			END

			DECLARE @CantidadTractos DECIMAL(10,2) = (SELECT ISNULL(COUNT(*),0) AS 'TOTAL_CONDICIONES' FROM ReportesApp_Operaciones_Operatividad_Registro
										             WHERE YEAR(Fecha) = @Anio AND MONTH(Fecha) = @Mes AND idCondicion = @i GROUP BY idCondicion)
			DECLARE @Promedio INT

			IF (@i IN (3)) BEGIN
				SET @Promedio = (SELECT ISNULL(CONVERT(INT,ROUND(@CantidadTractos/(@NumDias - 4),0)),0))
			END
			ELSE BEGIN
				SET @Promedio = (SELECT ISNULL(CONVERT(INT,ROUND(@CantidadTractos/@NumDias,0)),0))
			END

			UPDATE ReportesApp_Operaciones_Operatividad_IndicadorView SET
			ENE = CASE WHEN @Mes = 1 THEN CONVERT(VARCHAR,@Promedio) ELSE ENE END,
			FEB = CASE WHEN @Mes = 2 THEN CONVERT(VARCHAR,@Promedio) ELSE FEB END,
			MAR = CASE WHEN @Mes = 3 THEN CONVERT(VARCHAR,@Promedio) ELSE MAR END,
			ABR = CASE WHEN @Mes = 4 THEN CONVERT(VARCHAR,@Promedio) ELSE ABR END,
			MAY = CASE WHEN @Mes = 5 THEN CONVERT(VARCHAR,@Promedio) ELSE MAY END,
			JUN = CASE WHEN @Mes = 6 THEN CONVERT(VARCHAR,@Promedio) ELSE JUN END,
			JUL = CASE WHEN @Mes = 7 THEN CONVERT(VARCHAR,@Promedio) ELSE JUL END,
			AGO = CASE WHEN @Mes = 8 THEN CONVERT(VARCHAR,@Promedio) ELSE AGO END,
			[SET] = CASE WHEN @Mes = 9 THEN CONVERT(VARCHAR,@Promedio) ELSE [SET] END,
			OCT = CASE WHEN @Mes = 10 THEN CONVERT(VARCHAR,@Promedio) ELSE OCT END,
			NOV = CASE WHEN @Mes = 11 THEN CONVERT(VARCHAR,@Promedio) ELSE NOV END,
			[DIC] = CASE WHEN @Mes = 12 THEN CONVERT(VARCHAR,@Promedio) ELSE [DIC] END
			WHERE Anio = RIGHT(@periodo,4) AND idCondicion = @i

			SET @i = @i + 1
		END

		-- INDICADOR INGRESOS
		WHILE @j <= 4 BEGIN
			IF (@j = 4) BEGIN
				SET @j = 10
			END 

			DECLARE @SumaTotal DECIMAL(10,3)

			IF (@NumDias = 31) BEGIN
				SET @SumaTotal = (SELECT CONVERT(DECIMAL(10,3),ISNULL(D1,0)) + CONVERT(DECIMAL(10,3),ISNULL(D2,0)) + CONVERT(DECIMAL(10,3),ISNULL(D3,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D4,0)) + CONVERT(DECIMAL(10,3),ISNULL(D5,0)) + CONVERT(DECIMAL(10,3),ISNULL(D6,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D7,0)) + CONVERT(DECIMAL(10,3),ISNULL(D8,0)) + CONVERT(DECIMAL(10,3),ISNULL(D9,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D10,0)) + CONVERT(DECIMAL(10,3),ISNULL(D11,0)) + CONVERT(DECIMAL(10,3),ISNULL(D12,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D13,0)) + CONVERT(DECIMAL(10,3),ISNULL(D14,0)) + CONVERT(DECIMAL(10,3),ISNULL(D15,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D16,0)) + CONVERT(DECIMAL(10,3),ISNULL(D17,0)) + CONVERT(DECIMAL(10,3),ISNULL(D18,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D19,0)) + CONVERT(DECIMAL(10,3),ISNULL(D20,0)) + CONVERT(DECIMAL(10,3),ISNULL(D21,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D22,0)) + CONVERT(DECIMAL(10,3),ISNULL(D23,0)) + CONVERT(DECIMAL(10,3),ISNULL(D24,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D25,0)) + CONVERT(DECIMAL(10,3),ISNULL(D26,0)) + CONVERT(DECIMAL(10,3),ISNULL(D27,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D28,0)) + CONVERT(DECIMAL(10,3),ISNULL(D29,0)) + CONVERT(DECIMAL(10,3),ISNULL(D30,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D31,0)) AS CONTEO
								FROM ReportesApp_Operaciones_Operatividad_Ingresos
								WHERE Anio = @Anio AND Mes = @Mes AND idOperacion = @j)
			END

			IF (@NumDias = 30) BEGIN
				SET @SumaTotal = (SELECT CONVERT(DECIMAL(10,3),ISNULL(D1,0)) + CONVERT(DECIMAL(10,3),ISNULL(D2,0)) + CONVERT(DECIMAL(10,3),ISNULL(D3,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D4,0)) + CONVERT(DECIMAL(10,3),ISNULL(D5,0)) + CONVERT(DECIMAL(10,3),ISNULL(D6,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D7,0)) + CONVERT(DECIMAL(10,3),ISNULL(D8,0)) + CONVERT(DECIMAL(10,3),ISNULL(D9,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D10,0)) + CONVERT(DECIMAL(10,3),ISNULL(D11,0)) + CONVERT(DECIMAL(10,3),ISNULL(D12,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D13,0)) + CONVERT(DECIMAL(10,3),ISNULL(D14,0)) + CONVERT(DECIMAL(10,3),ISNULL(D15,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D16,0)) + CONVERT(DECIMAL(10,3),ISNULL(D17,0)) + CONVERT(DECIMAL(10,3),ISNULL(D18,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D19,0)) + CONVERT(DECIMAL(10,3),ISNULL(D20,0)) + CONVERT(DECIMAL(10,3),ISNULL(D21,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D22,0)) + CONVERT(DECIMAL(10,3),ISNULL(D23,0)) + CONVERT(DECIMAL(10,3),ISNULL(D24,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D25,0)) + CONVERT(DECIMAL(10,3),ISNULL(D26,0)) + CONVERT(DECIMAL(10,3),ISNULL(D27,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D28,0)) + CONVERT(DECIMAL(10,3),ISNULL(D29,0)) + CONVERT(DECIMAL(10,3),ISNULL(D30,0)) AS CONTEO
								FROM ReportesApp_Operaciones_Operatividad_Ingresos
								WHERE Anio = @Anio AND Mes = @Mes AND idOperacion = @j)
			END

			IF (@NumDias = 29) BEGIN
				SET @SumaTotal = (SELECT CONVERT(DECIMAL(10,3),ISNULL(D1,0)) + CONVERT(DECIMAL(10,3),ISNULL(D2,0)) + CONVERT(DECIMAL(10,3),ISNULL(D3,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D4,0)) + CONVERT(DECIMAL(10,3),ISNULL(D5,0)) + CONVERT(DECIMAL(10,3),ISNULL(D6,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D7,0)) + CONVERT(DECIMAL(10,3),ISNULL(D8,0)) + CONVERT(DECIMAL(10,3),ISNULL(D9,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D10,0)) + CONVERT(DECIMAL(10,3),ISNULL(D11,0)) + CONVERT(DECIMAL(10,3),ISNULL(D12,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D13,0)) + CONVERT(DECIMAL(10,3),ISNULL(D14,0)) + CONVERT(DECIMAL(10,3),ISNULL(D15,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D16,0)) + CONVERT(DECIMAL(10,3),ISNULL(D17,0)) + CONVERT(DECIMAL(10,3),ISNULL(D18,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D19,0)) + CONVERT(DECIMAL(10,3),ISNULL(D20,0)) + CONVERT(DECIMAL(10,3),ISNULL(D21,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D22,0)) + CONVERT(DECIMAL(10,3),ISNULL(D23,0)) + CONVERT(DECIMAL(10,3),ISNULL(D24,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D25,0)) + CONVERT(DECIMAL(10,3),ISNULL(D26,0)) + CONVERT(DECIMAL(10,3),ISNULL(D27,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D28,0)) + CONVERT(DECIMAL(10,3),ISNULL(D29,0)) AS CONTEO
								FROM ReportesApp_Operaciones_Operatividad_Ingresos
								WHERE Anio = @Anio AND Mes = @Mes AND idOperacion = @j)
			END

			IF (@NumDias = 28) BEGIN
				SET @SumaTotal = (SELECT CONVERT(DECIMAL(10,3),ISNULL(D1,0)) + CONVERT(DECIMAL(10,3),ISNULL(D2,0)) + CONVERT(DECIMAL(10,3),ISNULL(D3,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D4,0)) + CONVERT(DECIMAL(10,3),ISNULL(D5,0)) + CONVERT(DECIMAL(10,3),ISNULL(D6,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D7,0)) + CONVERT(DECIMAL(10,3),ISNULL(D8,0)) + CONVERT(DECIMAL(10,3),ISNULL(D9,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D10,0)) + CONVERT(DECIMAL(10,3),ISNULL(D11,0)) + CONVERT(DECIMAL(10,3),ISNULL(D12,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D13,0)) + CONVERT(DECIMAL(10,3),ISNULL(D14,0)) + CONVERT(DECIMAL(10,3),ISNULL(D15,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D16,0)) + CONVERT(DECIMAL(10,3),ISNULL(D17,0)) + CONVERT(DECIMAL(10,3),ISNULL(D18,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D19,0)) + CONVERT(DECIMAL(10,3),ISNULL(D20,0)) + CONVERT(DECIMAL(10,3),ISNULL(D21,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D22,0)) + CONVERT(DECIMAL(10,3),ISNULL(D23,0)) + CONVERT(DECIMAL(10,3),ISNULL(D24,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D25,0)) + CONVERT(DECIMAL(10,3),ISNULL(D26,0)) + CONVERT(DECIMAL(10,3),ISNULL(D27,0))
								+ CONVERT(DECIMAL(10,3),ISNULL(D28,0)) AS CONTEO
								FROM ReportesApp_Operaciones_Operatividad_Ingresos
								WHERE Anio = @Anio AND Mes = @Mes AND idOperacion = @j)
			END

			UPDATE ReportesApp_Operaciones_Operatividad_IndicadorIngresos SET
			ENE = CASE WHEN @Mes = 1 THEN CONVERT(VARCHAR,@SumaTotal) ELSE ENE END,
			FEB = CASE WHEN @Mes = 2 THEN CONVERT(VARCHAR,@SumaTotal) ELSE FEB END,
			MAR = CASE WHEN @Mes = 3 THEN CONVERT(VARCHAR,@SumaTotal) ELSE MAR END,
			ABR = CASE WHEN @Mes = 4 THEN CONVERT(VARCHAR,@SumaTotal) ELSE ABR END,
			MAY = CASE WHEN @Mes = 5 THEN CONVERT(VARCHAR,@SumaTotal) ELSE MAY END,
			JUN = CASE WHEN @Mes = 6 THEN CONVERT(VARCHAR,@SumaTotal) ELSE JUN END,
			JUL = CASE WHEN @Mes = 7 THEN CONVERT(VARCHAR,@SumaTotal) ELSE JUL END,
			AGO = CASE WHEN @Mes = 8 THEN CONVERT(VARCHAR,@SumaTotal) ELSE AGO END,
			[SET] = CASE WHEN @Mes = 9 THEN CONVERT(VARCHAR,@SumaTotal) ELSE [SET] END,
			OCT = CASE WHEN @Mes = 10 THEN CONVERT(VARCHAR,@SumaTotal) ELSE OCT END,
			NOV = CASE WHEN @Mes = 11 THEN CONVERT(VARCHAR,@SumaTotal) ELSE NOV END,
			[DIC] = CASE WHEN @Mes = 12 THEN CONVERT(VARCHAR,@SumaTotal) ELSE [DIC] END
			WHERE Anio = RIGHT(@periodo,4) AND idOperacion = @j

			SET @j = @j + 1
		END

		SET @Mes = @Mes + 1
		SET @i = 1
		SET @j = 1
	END

	IF (@Opcion = 1) BEGIN		-- LISTAR INDICADOR VISTA
		SELECT IV.idCondicion, IV.Anio AS 'AÑO', C.Operativas AS 'CONCEPTO', C.Condicion AS 'CONDICION', IV.ENE AS 'ENE', IV.FEB AS 'FEB',
		IV.MAR AS 'MAR', IV.ABR AS 'ABR', IV.MAY AS 'MAY', IV.JUN AS 'JUN', IV.JUL AS 'JUL', IV.AGO AS 'AGO', IV.[SET] AS 'SET',
		IV.OCT AS 'OCT', IV.NOV AS 'NOV', IV.[DIC] AS 'DIC'
		FROM ReportesApp_Operaciones_Operatividad_IndicadorView IV
		LEFT JOIN ReportesApp_Operaciones_Operatividad_Condiciones C ON C.idCondicion = IV.idCondicion
		WHERE IV.Anio = @Anio
		ORDER BY C.Orden
	END

	IF (@Opcion = 2) BEGIN		-- LISTAR INDICADOR INGRESOS
		SELECT I.idOperacion, I.Anio AS 'AÑO', 'NO OPERATIVAS' AS 'CONCEPTO', I.Descripcion AS 'OPERACION', I.ENE AS 'ENE', I.FEB AS 'FEB',
		I.MAR AS 'MAR', I.ABR AS 'ABR', I.MAY AS 'MAY', I.JUN AS 'JUN', I.JUL AS 'JUL', I.AGO AS 'AGO', I.[SET] AS 'SET',
		I.OCT AS 'OCT', I.NOV AS 'NOV', I.[DIC] AS 'DIC'
		FROM ReportesApp_Operaciones_Operatividad_IndicadorIngresos I
		WHERE I.Anio = @Anio
		ORDER BY I.Orden
	END
END

---------------------------------------------------------------------------------------
