
-- CREAR TABLA ReportesApp_Mantenimiento_FallasMecanicas_AnalisisFallas

-----------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 08/04/2025
-- Description:	INSERTAR ÁNALISIS DE FALLAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_CrearAnalisisFallas]
@idFalla INT,
@Tracto VARCHAR(50),
@Carreta VARCHAR(50),
@Operacion VARCHAR(50),
@Usuario VARCHAR(30)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

SET @correlativo = (SELECT MAX(idAnalisisFalla) FROM ReportesApp_Mantenimiento_FallasMecanicas_AnalisisFallas)
SET @correlativo = ISNULL(@correlativo,0) + 1

BEGIN TRAN
BEGIN TRY
	IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_FallasMecanicas_AnalisisFallas WHERE idFalla = @idFalla))
	BEGIN
		UPDATE ReportesApp_Mantenimiento_FallasMecanicas_AnalisisFallas
		SET Respuesta1 = NULL, Respuesta2 = NULL, Respuesta3 = NULL, Respuesta4 = NULL, CausaRaiz = NULL, Estado = 'PENDIENTE'
		WHERE idFalla = @idFalla
	END
	ELSE BEGIN
		INSERT INTO ReportesApp_Mantenimiento_FallasMecanicas_AnalisisFallas(idAnalisisFalla,idFalla,Tracto,Carreta,Operacion,
		Estado,UsuarioCrea,FechaCrea,UsuarioModifica,FechaModifica)
		VALUES(@correlativo, @idFalla, @Tracto, @Carreta, @Operacion, 'PENDIENTE', @Usuario, GETDATE(), @Usuario, GETDATE())
	END

	SET @Exito = '0 = Análisis añadido.'
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
-- Create date: 08/04/2025
-- Description:	INSERTAR ÁNALISIS DE FALLAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_InsertarAnalisisFallas]
@idFalla INT,
@Contador INT,
@Respuesta VARCHAR(350),
@Usuario VARCHAR(30)
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@Contador = 1) BEGIN
		UPDATE ReportesApp_Mantenimiento_FallasMecanicas_AnalisisFallas
		SET Respuesta1 = @Respuesta, UsuarioModifica = @Usuario, FechaModifica = GETDATE()
		WHERE idFalla = @idFalla

		SET @Exito = '0 = Respuesta añadida.'
	END

	IF (@Contador = 2) BEGIN
		UPDATE ReportesApp_Mantenimiento_FallasMecanicas_AnalisisFallas
		SET Respuesta2 = @Respuesta, UsuarioModifica = @Usuario, FechaModifica = GETDATE()
		WHERE idFalla = @idFalla

		SET @Exito = '0 = Respuesta añadida.'
	END

	IF (@Contador = 3) BEGIN
		UPDATE ReportesApp_Mantenimiento_FallasMecanicas_AnalisisFallas
		SET Respuesta3 = @Respuesta, UsuarioModifica = @Usuario, FechaModifica = GETDATE()
		WHERE idFalla = @idFalla

		SET @Exito = '0 = Respuesta añadida.'
	END

	IF (@Contador = 4) BEGIN
		UPDATE ReportesApp_Mantenimiento_FallasMecanicas_AnalisisFallas
		SET Respuesta4 = @Respuesta, UsuarioModifica = @Usuario, FechaModifica = GETDATE()
		WHERE idFalla = @idFalla

		SET @Exito = '0 = Respuesta añadida.'
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

---------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 12-04-2025
-- Description:	LISTAR ANALISIS DE FALLAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_ListarAnalisisFallas]
@NumeroPlaca VARCHAR(20),
@Operacion VARCHAR(20),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@Estado VARCHAR(25)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@Operacion = 'TODO') BEGIN
		IF (@Estado = 'TODAS') BEGIN
			SELECT AF.idAnalisisFalla AS 'NRO', AF.FechaCrea AS 'FECHA', AF.idFalla, AF.Tracto AS 'TRACTO', AF.Carreta AS 'CARRETA', AF.Operacion AS 'OPERACION',
			F.Motivo AS 'FALLA', F.Solucion AS 'SOLUCION', AF.Respuesta1 AS 'RESPUESTA_1', AF.Respuesta2 AS 'RESPUESTA_2', AF.Respuesta3 AS 'RESPUESTA_3',
			AF.Respuesta4 AS 'RESPUESTA_4', AF.CausaRaiz AS 'CAUSA_RAIZ', AF.Estado AS 'ESTADO', AF.UsuarioModifica AS 'ULTIMO_USUARIO', AF.FechaModifica AS 'ULTIMA_FECHA'
			FROM ReportesApp_Mantenimiento_FallasMecanicas_AnalisisFallas AF
			LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_Registros F ON F.idFalla = AF.idFalla
			WHERE ((@NumeroPlaca IS NULL OR AF.Tracto LIKE '%' + @NumeroPlaca + '%') OR (@NumeroPlaca IS NULL OR AF.Carreta LIKE '%' + @NumeroPlaca + '%')) AND
			(AF.FechaCrea BETWEEN @FINICIO AND @FFIN)
			ORDER BY AF.idAnalisisFalla DESC
		END
		ELSE BEGIN
			SELECT AF.idAnalisisFalla AS 'NRO', AF.FechaCrea AS 'FECHA', AF.idFalla, AF.Tracto AS 'TRACTO', AF.Carreta AS 'CARRETA', AF.Operacion AS 'OPERACION',
			F.Motivo AS 'FALLA', F.Solucion AS 'SOLUCION', AF.Respuesta1 AS 'RESPUESTA_1', AF.Respuesta2 AS 'RESPUESTA_2', AF.Respuesta3 AS 'RESPUESTA_3',
			AF.Respuesta4 AS 'RESPUESTA_4', AF.CausaRaiz AS 'CAUSA_RAIZ', AF.Estado AS 'ESTADO', AF.UsuarioModifica AS 'ULTIMO_USUARIO', AF.FechaModifica AS 'ULTIMA_FECHA'
			FROM ReportesApp_Mantenimiento_FallasMecanicas_AnalisisFallas AF
			LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_Registros F ON F.idFalla = AF.idFalla
			WHERE ((@NumeroPlaca IS NULL OR AF.Tracto LIKE '%' + @NumeroPlaca + '%') OR (@NumeroPlaca IS NULL OR AF.Carreta LIKE '%' + @NumeroPlaca + '%')) AND
			(AF.FechaCrea BETWEEN @FINICIO AND @FFIN) AND (AF.Estado = @Estado)
			ORDER BY AF.idAnalisisFalla DESC
		END
	END
	ELSE BEGIN
		IF (@Estado = 'TODAS') BEGIN
			SELECT AF.idAnalisisFalla AS 'NRO', AF.FechaCrea AS 'FECHA', AF.idFalla, AF.Tracto AS 'TRACTO', AF.Carreta AS 'CARRETA', AF.Operacion AS 'OPERACION',
			F.Motivo AS 'FALLA', F.Solucion AS 'SOLUCION', AF.Respuesta1 AS 'RESPUESTA_1', AF.Respuesta2 AS 'RESPUESTA_2', AF.Respuesta3 AS 'RESPUESTA_3',
			AF.Respuesta4 AS 'RESPUESTA_4', AF.CausaRaiz AS 'CAUSA_RAIZ', AF.Estado AS 'ESTADO', AF.UsuarioModifica AS 'ULTIMO_USUARIO', AF.FechaModifica AS 'ULTIMA_FECHA'
			FROM ReportesApp_Mantenimiento_FallasMecanicas_AnalisisFallas AF
			LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_Registros F ON F.idFalla = AF.idFalla
			WHERE ((@NumeroPlaca IS NULL OR AF.Tracto LIKE '%' + @NumeroPlaca + '%') OR (@NumeroPlaca IS NULL OR AF.Carreta LIKE '%' + @NumeroPlaca + '%')) AND
			(AF.FechaCrea BETWEEN @FINICIO AND @FFIN) AND (AF.Operacion = @Operacion)
			ORDER BY AF.idAnalisisFalla DESC
		END
		ELSE BEGIN
			SELECT AF.idAnalisisFalla AS 'NRO', AF.FechaCrea AS 'FECHA', AF.idFalla, AF.Tracto AS 'TRACTO', AF.Carreta AS 'CARRETA', AF.Operacion AS 'OPERACION',
			F.Motivo AS 'FALLA', F.Solucion AS 'SOLUCION', AF.Respuesta1 AS 'RESPUESTA_1', AF.Respuesta2 AS 'RESPUESTA_2', AF.Respuesta3 AS 'RESPUESTA_3',
			AF.Respuesta4 AS 'RESPUESTA_4', AF.CausaRaiz AS 'CAUSA_RAIZ', AF.Estado AS 'ESTADO', AF.UsuarioModifica AS 'ULTIMO_USUARIO', AF.FechaModifica AS 'ULTIMA_FECHA'
			FROM ReportesApp_Mantenimiento_FallasMecanicas_AnalisisFallas AF
			LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_Registros F ON F.idFalla = AF.idFalla
			WHERE ((@NumeroPlaca IS NULL OR AF.Tracto LIKE '%' + @NumeroPlaca + '%') OR (@NumeroPlaca IS NULL OR AF.Carreta LIKE '%' + @NumeroPlaca + '%')) AND
			(AF.FechaCrea BETWEEN @FINICIO AND @FFIN) AND (AF.Operacion = @Operacion) AND (AF.Estado = @Estado)
			ORDER BY AF.idAnalisisFalla DESC
		END
	END
END

---------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 14/04/2025
-- Description:	INSERTAR RAÍZ FALLA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_InsertarEliminarRaizFalla]
@Opcion INT,
@idAnalisisFalla INT,
@RaizFalla VARCHAR(350),
@Usuario VARCHAR(30)
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- INSERTAR CAUSA RAIZ
		UPDATE ReportesApp_Mantenimiento_FallasMecanicas_AnalisisFallas
		SET CausaRaiz = @RaizFalla, Estado = 'COMPLETADA', UsuarioModifica = @Usuario, FechaModifica = GETDATE()
		WHERE idAnalisisFalla = @idAnalisisFalla
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR CAUSA RAIZ
		UPDATE ReportesApp_Mantenimiento_FallasMecanicas_AnalisisFallas
		SET CausaRaiz = NULL, Estado = 'PENDIENTE', UsuarioModifica = @Usuario, FechaModifica = GETDATE()
		WHERE idAnalisisFalla = @idAnalisisFalla
	END

	SET @Exito = '0 = Análisis Actualizado.'
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


---------------------------------------------------------------------------------
/*
DELETE FROM ReportesApp_Mantenimiento_FallasMecanicas_AnalisisFallas
*/

SELECT * FROM ReportesApp_Mantenimiento_FallasMecanicas_AnalisisFallas
WHERE idFalla = 1916

/*
UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
SET idEstadoFalla = 1, Tecnico = NULL
WHERE idFalla IN (1916,1913,1915)
*/


