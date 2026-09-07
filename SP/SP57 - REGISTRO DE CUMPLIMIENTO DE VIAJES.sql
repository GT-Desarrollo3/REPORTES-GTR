
-- CREAR TABLA ReportesApp_Operaciones_CumplimientoViajes_GrupoViaje Y LLENARLA

-- CREAR TABLA ReportesApp_Operaciones_CumplimientoViajes_GrupoViaje_Detalle

-- CREAR TABLA ReportesApp_Operaciones_CumplimientoViajes_Registro

----------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 24-09-2024
-- Description:	LISTAR GRUPOS DE VIAJE
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_CumplimientoViajes_ListarGrupoViaje]
@Opcion INT
AS
BEGIN
	IF (@Opcion = 1) BEGIN		-- LISTAR GRUPO VIAJE - LIMA
		SELECT idGrupoViaje, Descripcion FROM ReportesApp_Operaciones_CumplimientoViajes_GrupoViaje
		WHERE idGrupoViaje <= 3
	END

	IF (@Opcion = 2) BEGIN		-- LISTAR GRUPO VIAJE - NORTE
		SELECT idGrupoViaje, Descripcion FROM ReportesApp_Operaciones_CumplimientoViajes_GrupoViaje
		WHERE idGrupoViaje > 3
	END

	IF (@Opcion = 3) BEGIN		-- LISTAR DETALLE RUTAS - LIMA
		SELECT GD.idGrupoViajeD AS 'NRO', GD.idGrupoViaje, GV.Descripcion AS 'GRUPO_VIAJE', RTRIM(R.Descripcion) AS 'RUTA'
		FROM ReportesApp_Operaciones_CumplimientoViajes_GrupoViaje_Detalle GD
		LEFT JOIN ReportesApp_Operaciones_CumplimientoViajes_GrupoViaje GV ON GV.idGrupoViaje = GD.idGrupoViaje
		LEFT JOIN OP_TR_Ruta R ON R.idRuta = GD.idRuta
		WHERE GD.idGrupoViaje <= 3
		ORDER BY GD.idGrupoViaje, GD.idGrupoViajeD
	END

	IF (@Opcion = 4) BEGIN		-- LISTAR DETALLE RUTAS - NORTE
		SELECT GD.idGrupoViajeD AS 'NRO', GD.idGrupoViaje, GV.Descripcion AS 'GRUPO_VIAJE', RTRIM(R.Descripcion) AS 'RUTA'
		FROM ReportesApp_Operaciones_CumplimientoViajes_GrupoViaje_Detalle GD
		LEFT JOIN ReportesApp_Operaciones_CumplimientoViajes_GrupoViaje GV ON GV.idGrupoViaje = GD.idGrupoViaje
		LEFT JOIN OP_TR_Ruta R ON R.idRuta = GD.idRuta
		WHERE GD.idGrupoViaje > 3
		ORDER BY GD.idGrupoViaje, GD.idGrupoViajeD
	END
END

-------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 24-09-2024
-- Description: ASIGNAR RUTA A GRUPO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_CumplimientoViajes_AsignarGrupo]
@idRuta INT,
@idGrupoViaje INT
AS
DECLARE @Contador INT 
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_CumplimientoViajes_GrupoViaje_Detalle WHERE idRuta = @idRuta)) BEGIN
		SET @Exito = '-1 = Esta ruta de viaje ya pertenece a un grupo.'
		ROLLBACK
		GOTO Terminar
	END
	ELSE BEGIN
		SET @Contador = (SELECT MAX(idGrupoViajeD) FROM ReportesApp_Operaciones_CumplimientoViajes_GrupoViaje_Detalle WHERE idGrupoViaje = @idGrupoViaje)
		SET @Contador = ISNULL(@Contador,0) + 1 

		INSERT INTO ReportesApp_Operaciones_CumplimientoViajes_GrupoViaje_Detalle(idGrupoViajeD, idGrupoViaje, idRuta)
		VALUES (@Contador, @idGrupoViaje, @idRuta)

		SET @Exito = '0 = Ruta asignada correctamente.'
	END
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

-------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 24-09-2024
-- Description: ELIMINAR RUTA A GRUPO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_CumplimientoViajes_EliminarGrupo]
@idGrupoViajeD INT,
@idGrupoViaje INT
AS
DECLARE @Contador INT 
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	DELETE FROM ReportesApp_Operaciones_CumplimientoViajes_GrupoViaje_Detalle
	WHERE idGrupoViajeD = @idGrupoViajeD AND idGrupoViaje = @idGrupoViaje

	UPDATE ReportesApp_Operaciones_CumplimientoViajes_GrupoViaje_Detalle
	SET idGrupoViajeD = idGrupoViajeD - 1
	WHERE idGrupoViajeD > @idGrupoViajeD AND idGrupoViaje = @idGrupoViaje

	SET @Exito = '0 = Ruta eliminada correctamente.'
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

-------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 24-09-2024
-- Description: REGISTRAR CUMPLIMIENTO DIARIO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_CumplimientoViajes_RegistrarEditarCumplimiento]
@Opcion INT,
@idGrupoViaje INT,
@FechaCumplimiento DATE,
@Disponibles INT,
@Proyectados INT,
@Detalle VARCHAR(250),
@Usuario VARCHAR(20)
AS
DECLARE @Contador INT 
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- AGREGAR
		IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_CumplimientoViajes_Registro WHERE idGrupoViaje = @idGrupoViaje AND FechaCumplimiento = @FechaCumplimiento)) BEGIN
			UPDATE ReportesApp_Operaciones_CumplimientoViajes_Registro
			SET NroDisponible = @Disponibles, NroProyReq = @Proyectados, Detalle = @Detalle, Usuario = @Usuario, FechaCreacion = GETDATE()
			WHERE idGrupoViaje = @idGrupoViaje AND FechaCumplimiento = @FechaCumplimiento

			SET @Exito = '0 = Cumplimiento actualizado correctamente.'
		END
		ELSE BEGIN
			SET @Contador = (SELECT MAX(idCumplimiento) FROM ReportesApp_Operaciones_CumplimientoViajes_Registro)
			SET @Contador = ISNULL(@Contador,0) + 1 

			INSERT INTO ReportesApp_Operaciones_CumplimientoViajes_Registro(idCumplimiento, idGrupoViaje, FechaCumplimiento, NroDisponible, NroProyReq, Detalle, Usuario, FechaCreacion)
			VALUES (@Contador, @idGrupoViaje, @FechaCumplimiento, @Disponibles, @Proyectados, @Detalle, @Usuario, GETDATE())

			SET @Exito = '0 = Cumplimiento registrado correctamente.'
		END

		DECLARE @Contador1 INT, @Total INT
		SET @Contador1 = 1
		SET @Total = 0

		WHILE (@Contador1 <= (SELECT COUNT(*) FROM ReportesApp_Operaciones_CumplimientoViajes_GrupoViaje_Detalle WHERE idGrupoViaje = @idGrupoViaje)) BEGIN
			DECLARE @IdRuta INT = (SELECT idRuta FROM ReportesApp_Operaciones_CumplimientoViajes_GrupoViaje_Detalle WHERE idGrupoViaje = @idGrupoViaje AND
									idGrupoViajeD = @Contador1)
			
			IF (@idGrupoViaje = 1 OR @idGrupoViaje = 3) BEGIN
				SET @Total = @Total + (SELECT COUNT(*) FROM ReportesApp_Operacion_Previaje_Registros WHERE (TipoProgramacion = 2) AND (Estado = 9) AND
				CONVERT(DATE,FechaProgramacion) = @FechaCumplimiento AND (Sucursal = 'LIMA') AND IdRuta = @IdRuta)
			END
			ELSE BEGIN
				SET @Total = @Total + (SELECT COUNT(*) FROM ReportesApp_Operacion_Previaje_Registros WHERE (TipoProgramacion = 2) AND (Estado = 9) AND
				CONVERT(DATE,FechaProgramacion) = @FechaCumplimiento AND (Sucursal = 'TRUJILLO') AND IdRuta = @IdRuta)
			END

			SET @Contador1 = @Contador1 + 1
		END

		UPDATE ReportesApp_Operaciones_CumplimientoViajes_Registro
		SET Cumplimiento = @Total
		WHERE idGrupoViaje = @idGrupoViaje AND FechaCumplimiento = @FechaCumplimiento
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR
		UPDATE ReportesApp_Operaciones_CumplimientoViajes_Registro
		SET NroDisponible = 0, NroProyReq = 0, Detalle = ''
		WHERE idGrupoViaje = @idGrupoViaje AND FechaCumplimiento = @FechaCumplimiento

		SET @Exito = '0 = Cumplimiento eliminado correctamente.'
	END
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

-----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 25-09-2024
-- Description:	LISTAR CUMPLIMIENTO DIARIO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_CumplimientoViajes_ListarCumplimiento]
@idGrupoViaje INT,
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@idGrupoViaje = 0) BEGIN
		SELECT X.idCumplimiento, X.FECHA, X.idGrupoViaje, X.GRUPO_VIAJE, X.DISPONIBLE, X.[PROY / REQ], X.CUMPLIMIENTO,
		CASE WHEN X.[PROY / REQ] = 0 THEN 0.00 ELSE (X.CUMPLIMIENTO * 100 / X.[PROY / REQ]) END AS 'PORCENTAJE', X.DETALLE
		FROM (SELECT CV.idCumplimiento, CONVERT(DATE,CV.FechaCumplimiento) AS 'FECHA', CV.idGrupoViaje, GV.DESCRIPCION AS 'GRUPO_VIAJE',
		--CASE WHEN CV.idGrupoViaje = 1 THEN ' ' WHEN CV.idGrupoViaje = 2 THEN '  ' ELSE '   ' END AS 'GRUPO_VIAJE',
		CV.NroDisponible AS 'DISPONIBLE', CV.NroProyReq AS 'PROY / REQ', CV.Cumplimiento AS 'CUMPLIMIENTO', CV.Detalle AS 'DETALLE'
		FROM ReportesApp_Operaciones_CumplimientoViajes_Registro CV
		LEFT JOIN ReportesApp_Operaciones_CumplimientoViajes_GrupoViaje GV ON GV.idGrupoViaje = CV.idGrupoViaje
		WHERE (CV.FechaCumplimiento BETWEEN @FINICIO AND @FFIN)) X
		ORDER BY X.FECHA DESC, X.idGrupoViaje ASC
	END

	IF (@idGrupoViaje IN (1,2,3)) BEGIN
		SELECT X.idCumplimiento, X.FECHA, X.idGrupoViaje, X.GRUPO_VIAJE, X.DISPONIBLE, X.[PROY / REQ], X.CUMPLIMIENTO,
		CASE WHEN X.[PROY / REQ] = 0 THEN 0.00 ELSE (X.CUMPLIMIENTO * 100 / X.[PROY / REQ]) END AS 'PORCENTAJE', X.DETALLE
		FROM (SELECT CV.idCumplimiento, CONVERT(DATE,CV.FechaCumplimiento) AS 'FECHA', CV.idGrupoViaje, GV.DESCRIPCION AS 'GRUPO_VIAJE',
		--CASE WHEN CV.idGrupoViaje = 1 THEN ' ' WHEN CV.idGrupoViaje = 2 THEN '  ' ELSE '   ' END AS 'GRUPO_VIAJE',
		CV.NroDisponible AS 'DISPONIBLE', CV.NroProyReq AS 'PROY / REQ', CV.Cumplimiento AS 'CUMPLIMIENTO', CV.Detalle AS 'DETALLE'
		FROM ReportesApp_Operaciones_CumplimientoViajes_Registro CV
		LEFT JOIN ReportesApp_Operaciones_CumplimientoViajes_GrupoViaje GV ON GV.idGrupoViaje = CV.idGrupoViaje
		WHERE (CV.idGrupoViaje = @idGrupoViaje) AND (CV.FechaCumplimiento BETWEEN @FINICIO AND @FFIN)) X
		ORDER BY X.FECHA DESC, X.idGrupoViaje ASC
	END

	IF (@idGrupoViaje = 4) BEGIN
		SELECT X.idCumplimiento, X.FECHA, X.idGrupoViaje, X.GRUPO_VIAJE, X.DISPONIBLE, X.[PROY / REQ], X.CUMPLIMIENTO,
		CASE WHEN X.[PROY / REQ] = 0 THEN 0.00 ELSE (X.CUMPLIMIENTO * 100 / X.[PROY / REQ]) END AS 'PORCENTAJE', X.DETALLE
		FROM (SELECT CV.idCumplimiento, CONVERT(DATE,CV.FechaCumplimiento) AS 'FECHA', CV.idGrupoViaje, GV.DESCRIPCION AS 'GRUPO_VIAJE',
		CV.NroDisponible AS 'DISPONIBLE', CV.NroProyReq AS 'PROY / REQ', CV.Cumplimiento AS 'CUMPLIMIENTO', CV.Detalle AS 'DETALLE'
		FROM ReportesApp_Operaciones_CumplimientoViajes_Registro CV
		LEFT JOIN ReportesApp_Operaciones_CumplimientoViajes_GrupoViaje GV ON GV.idGrupoViaje = CV.idGrupoViaje
		WHERE (CV.idGrupoViaje > 3) AND (CV.FechaCumplimiento BETWEEN @FINICIO AND @FFIN)) X
		ORDER BY X.FECHA DESC, X.idGrupoViaje ASC
	END

	IF (@idGrupoViaje = 5) BEGIN
		DECLARE @DISPONIBLE TABLE(FechaCumplimiento DATE, NroDisponible INT)
		DECLARE @PROYECTADOS TABLE(FechaCumplimiento DATE, NroProyReq INT)
		DECLARE @CUMPLIMIENTO TABLE(FechaCumplimiento DATE, Cumplimiento INT)

		INSERT INTO @DISPONIBLE
		SELECT CONVERT(DATE,CV.FechaCumplimiento) AS 'FechaCumplimiento', SUM(CV.NroDisponible) AS 'NroDisponible'
		FROM ReportesApp_Operaciones_CumplimientoViajes_Registro CV
		LEFT JOIN ReportesApp_Operaciones_CumplimientoViajes_GrupoViaje GV ON GV.idGrupoViaje = CV.idGrupoViaje
		WHERE (CV.idGrupoViaje > 3) AND (CV.FechaCumplimiento BETWEEN @FINICIO AND @FFIN)
		GROUP BY CV.FechaCumplimiento

		INSERT INTO @PROYECTADOS
		SELECT CONVERT(DATE,CV.FechaCumplimiento) AS 'FechaCumplimiento', SUM(CV.NroProyReq) AS 'NroProyReq'
		FROM ReportesApp_Operaciones_CumplimientoViajes_Registro CV
		LEFT JOIN ReportesApp_Operaciones_CumplimientoViajes_GrupoViaje GV ON GV.idGrupoViaje = CV.idGrupoViaje
		WHERE (CV.idGrupoViaje > 3) AND (CV.FechaCumplimiento BETWEEN @FINICIO AND @FFIN)
		GROUP BY CV.FechaCumplimiento

		INSERT INTO @CUMPLIMIENTO
		SELECT CONVERT(DATE,CV.FechaCumplimiento) AS 'FechaCumplimiento', SUM(CV.Cumplimiento) AS 'Cumplimiento'
		FROM ReportesApp_Operaciones_CumplimientoViajes_Registro CV
		LEFT JOIN ReportesApp_Operaciones_CumplimientoViajes_GrupoViaje GV ON GV.idGrupoViaje = CV.idGrupoViaje
		WHERE (CV.idGrupoViaje > 3) AND (CV.FechaCumplimiento BETWEEN @FINICIO AND @FFIN)
		GROUP BY CV.FechaCumplimiento

		SELECT D.FechaCumplimiento AS 'FECHA', D.NroDisponible AS 'DISPONIBLE', P.NroProyReq AS 'PROY / REQ',
		C.Cumplimiento AS 'CUMPLIMIENTO', CASE WHEN P.NroProyReq = 0 THEN 0.00 ELSE (C.Cumplimiento * 100 / P.NroProyReq) END AS 'PORCENTAJE'
		FROM @DISPONIBLE D
		LEFT JOIN @PROYECTADOS P ON P.FechaCumplimiento = D.FechaCumplimiento
		LEFT JOIN @CUMPLIMIENTO C ON C.FechaCumplimiento = D.FechaCumplimiento
		ORDER BY D.FechaCumplimiento DESC
	END

	IF (@idGrupoViaje = 6) BEGIN
		DECLARE @DISPONIBLE2 TABLE(GrupoViaje VARCHAR(140), NroDisponible INT)
		DECLARE @PROYECTADOS2 TABLE(GrupoViaje VARCHAR(140), NroProyReq INT)
		DECLARE @CUMPLIMIENTO2 TABLE(GrupoViaje VARCHAR(140), Cumplimiento INT)

		INSERT INTO @DISPONIBLE2
		SELECT GV.DESCRIPCION AS 'GRUPO_VIAJE', SUM(CV.NroDisponible) AS 'NroDisponible'
		FROM ReportesApp_Operaciones_CumplimientoViajes_Registro CV
		LEFT JOIN ReportesApp_Operaciones_CumplimientoViajes_GrupoViaje GV ON GV.idGrupoViaje = CV.idGrupoViaje
		WHERE (CV.idGrupoViaje > 3) AND (CV.FechaCumplimiento BETWEEN @FINICIO AND @FFIN)
		GROUP BY GV.DESCRIPCION

		INSERT INTO @PROYECTADOS2
		SELECT GV.DESCRIPCION AS 'GRUPO_VIAJE', SUM(CV.NroProyReq) AS 'NroProyReq'
		FROM ReportesApp_Operaciones_CumplimientoViajes_Registro CV
		LEFT JOIN ReportesApp_Operaciones_CumplimientoViajes_GrupoViaje GV ON GV.idGrupoViaje = CV.idGrupoViaje
		WHERE (CV.idGrupoViaje > 3) AND (CV.FechaCumplimiento BETWEEN @FINICIO AND @FFIN)
		GROUP BY GV.DESCRIPCION

		INSERT INTO @CUMPLIMIENTO2
		SELECT GV.DESCRIPCION AS 'GRUPO_VIAJE', SUM(CV.Cumplimiento) AS 'Cumplimiento'
		FROM ReportesApp_Operaciones_CumplimientoViajes_Registro CV
		LEFT JOIN ReportesApp_Operaciones_CumplimientoViajes_GrupoViaje GV ON GV.idGrupoViaje = CV.idGrupoViaje
		WHERE (CV.idGrupoViaje > 3) AND (CV.FechaCumplimiento BETWEEN @FINICIO AND @FFIN)
		GROUP BY GV.DESCRIPCION

		SELECT D.GrupoViaje AS 'GRUPO_VIAJE', D.NroDisponible AS 'DISPONIBLE', P.NroProyReq AS 'PROY / REQ',
		C.Cumplimiento AS 'CUMPLIMIENTO', CASE WHEN P.NroProyReq = 0 THEN 0.00 ELSE (C.Cumplimiento * 100 / P.NroProyReq) END AS 'PORCENTAJE'
		FROM @DISPONIBLE2 D
		LEFT JOIN @PROYECTADOS2 P ON P.GrupoViaje = D.GrupoViaje
		LEFT JOIN @CUMPLIMIENTO2 C ON C.GrupoViaje = D.GrupoViaje
		ORDER BY D.GrupoViaje ASC
	END
END

-----------------------------------------------------------------------------

DECLARE @Contador INT = 1
DECLARE @NroDisponibles INT, @NroProyectados INT
DECLARE @Fecha DATE = (SELECT DATEADD(DAY,-7,CONVERT(DATE,GETDATE())))

WHILE (@Fecha <= CONVERT(DATE,GETDATE())) BEGIN
	WHILE (@Contador <= 18) BEGIN
		IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_CumplimientoViajes_Registro WHERE (idGrupoViaje = @Contador) AND (FechaCumplimiento = @Fecha)))
		BEGIN
			SET @NroDisponibles = (SELECT NroDisponible FROM ReportesApp_Operaciones_CumplimientoViajes_Registro WHERE (idGrupoViaje = @Contador) AND
								  (FechaCumplimiento = @Fecha))
			SET @NroProyectados = (SELECT NroProyReq FROM ReportesApp_Operaciones_CumplimientoViajes_Registro WHERE (idGrupoViaje = @Contador) AND
							      (FechaCumplimiento = @Fecha))
		END
		ELSE BEGIN
			SET @NroDisponibles = 0
			SET @NroProyectados = 0
		END

		DECLARE @Descripcion VARCHAR(250) = (SELECT Detalle FROM ReportesApp_Operaciones_CumplimientoViajes_Registro WHERE (idGrupoViaje = @Contador) AND
											(FechaCumplimiento = @Fecha))

		EXEC ReportesApp_Operaciones_CumplimientoViajes_RegistrarEditarCumplimiento @Opcion = 1, @idGrupoViaje = @Contador, @FechaCumplimiento = @Fecha,
		@Disponibles = @NroDisponibles, @Proyectados = @NroProyectados, @Detalle = @Descripcion, @Usuario = 'CBELTRAN'
	
		SET @Contador = @Contador + 1
	END

	SET @Contador = 1
	SET @Fecha = (SELECT DATEADD(DAY,1,CONVERT(DATE,@Fecha)))
END

