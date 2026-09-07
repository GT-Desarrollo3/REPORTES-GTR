
-- UPDATE Usuario SET Estado = 'A' WHERE Usuario = 'DEMO'
-- UPDATE PersonaMast SET Estado = 'A' WHERE Persona = 0

-----------------------------------------------------------

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_Registro

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_Historial

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_TipoMtto Y LLENARLA

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_Accesorios Y LLENARLA

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_HistorialProceso

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_KMRegistro

-----------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 10-01-2023
-- Description:	LISTAR TIPO DE MANTENIMIENTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarMtto]
@Aceite VARCHAR(10)
AS
BEGIN
	IF (@Aceite LIKE '%MINERAL%') BEGIN
		SELECT idMantenimientoOP, Descripcion FROM ReportesApp_Mantenimiento_MttoPreventivo_OP WHERE idMantenimientoOP IN (1,2)
	END

	IF (@Aceite LIKE '%SINTETICO%') BEGIN
		SELECT idMantenimientoOP, Descripcion FROM ReportesApp_Mantenimiento_MttoPreventivo_OP WHERE idMantenimientoOP IN (3)
	END

	IF (@Aceite = '') BEGIN
		SELECT idMantenimientoOP, Descripcion FROM ReportesApp_Mantenimiento_MttoPreventivo_OP WHERE idMantenimientoOP IN (4)
	END

	IF (@Aceite = 'A') BEGIN
		SELECT idAccesorio, Descripcion FROM ReportesApp_Mantenimiento_MttoPreventivo_Accesorios
		WHERE idAccesorio NOT IN (0)
		ORDER BY Descripcion
	END

	IF (@Aceite = 'B') BEGIN
		SELECT idAccesorio, Descripcion FROM ReportesApp_Mantenimiento_MttoPreventivo_Accesorios ORDER BY Descripcion
	END

	IF (@Aceite = 'C-1') BEGIN
		SELECT idEspecialidad, Descripcion FROM ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_Especialidades ORDER BY idEspecialidad
	END

	IF (@Aceite = 'C-2') BEGIN
		SELECT 0 AS 'idEspecialidad', 'TODAS' AS 'Descripcion'
		UNION
		SELECT idEspecialidad, Descripcion FROM ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_Especialidades ORDER BY idEspecialidad
	END
END

-----------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 10-01-2023
-- Description:	LISTAR TIPO DE OPERACION
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarOperaciones]
@idMttoOP INT
AS
BEGIN
	SELECT Posicion, TipoMantenimiento FROM ReportesApp_Mantenimiento_MttoPreventivo_TipoMtto
	WHERE idMantenimientoOP = @idMttoOP
END

-------------------------------------------------------------

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
		SET @Placa = (SELECT NumeroPlaca FROM OP_TR_Vehiculo WHERE IdVehiculo = @idVehiculo)
		
		SET @correlativo = (SELECT MAX(idRegistro) FROM ReportesApp_Mantenimiento_MttoPreventivo_Registro)
		SET @correlativo = ISNULL(@correlativo,0) + 1

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
		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_Historial(idRegistro,idVehiculo,Aceite,UltimaFecha,UltimoKM,TipoMantenimiento,Usuario,FechaCreacion,idOperacion,PS)
		SELECT idRegistro,idVehiculo,Aceite,UltimaFecha,UltimoKM,TipoMantenimiento,Usuario,FechaCreacion,idOperacion,PS FROM ReportesApp_Mantenimiento_MttoPreventivo_Registro
		WHERE idRegistro = @idRegistro

		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_Registro
		SET UltimaFecha = @UltimaFecha,UltimoKM = @UltimoKM,TipoMantenimiento = @TipoMantenimiento,Frecuencia = @Frecuencia,Usuario = @Usuario,FechaCreacion = GETDATE(),
		idMantenimientoOP = @idMttoOP, PS = @PS
		WHERE idRegistro = @idRegistro 

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

------------------------------------------------------------------

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

------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 15-01-2023
-- Description:	LISTAR MANTENIMIENTOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarControlMtto]
@idVehiculo INT
AS
BEGIN
	SELECT X.idProcesoMtto, X.idVehiculo, X.ACCESORIO, X.FECHA_CAMBIO, X.KM_CAMBIO, X.INTERVALO, X.FECHA_ACTUAL, X.KM_ACTUAL, X.TIEMPO_MESES,
	X.KM_RECORRIDO, X.[KM/DIA], X.KM_FALTANTE, X.[%], (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL)
	END) AS 'FECHA_PROYECTADA', X.Usuario, X.Fecha
	FROM (SELECT PM.idAccesorio, PM.idProcesoMtto, PM.idVehiculo, AC.Descripcion AS 'ACCESORIO', PM.FechaCambio AS 'FECHA_CAMBIO', PM.KMCambio AS 'KM_CAMBIO',
	PM.Intervalo AS 'INTERVALO', KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL', DATEDIFF(MONTH,PM.FechaCambio,KM.Fecha) AS 'TIEMPO_MESES',
	CASE WHEN PM.KMCambio = 0 THEN 0 ELSE KM.KMActual - PM.KMCambio END AS 'KM_RECORRIDO', CASE WHEN DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) = 0 THEN 1 ELSE
	CAST((KM.KMActual - PM.KMCambio) / DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA', PM.Intervalo - (KM.KMActual - PM.KMCambio) AS 'KM_FALTANTE', 
	CASE WHEN PM.Intervalo = 0 THEN 0 ELSE CAST((1-(PM.Intervalo - (KM.KMActual - PM.KMCambio)) / PM.Intervalo) * 100 AS DECIMAL(10,2)) END AS '%',
	PM.Usuario, PM.Fecha FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto PM
	LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Accesorios AC ON AC.idAccesorio = PM.idAccesorio
	LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = PM.idVehiculo
	WHERE PM.idVehiculo = @idVehiculo) X
	ORDER BY X.idAccesorio ASC
END

------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 16-01-2023
-- Description:	LISTAR PROCESOS HISTORIAL
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialProcesos]
@idVehiculo INT,
@idAccesorio INT
AS
BEGIN
	IF (@idAccesorio = 0) BEGIN
		SELECT TOP(30) HP.idProcesoMtto, HP.idVehiculo, AC.Descripcion AS 'ACCESORIO', HP.FechaCambio AS 'FECHA_CAMBIO', HP.KMCambio AS 'KM_CAMBIO',
		HP.Intervalo AS 'INTERVALO', HP.Usuario, HP.Fecha FROM ReportesApp_Mantenimiento_MttoPreventivo_HistorialProceso HP
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Accesorios AC ON AC.idAccesorio = HP.idAccesorio
		WHERE (HP.idVehiculo = @idVehiculo)
		ORDER BY HP.Fecha DESC
	END
	ELSE BEGIN
		SELECT TOP(30) HP.idProcesoMtto, HP.idVehiculo, AC.Descripcion AS 'ACCESORIO', HP.FechaCambio AS 'FECHA_CAMBIO', HP.KMCambio AS 'KM_CAMBIO',
		HP.Intervalo AS 'INTERVALO', HP.Usuario, HP.Fecha FROM ReportesApp_Mantenimiento_MttoPreventivo_HistorialProceso HP
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Accesorios AC ON AC.idAccesorio = HP.idAccesorio
		WHERE (HP.idVehiculo = @idVehiculo) AND (HP.idAccesorio = @idAccesorio)
		ORDER BY HP.Fecha DESC
	END
END

------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 16-01-2023
-- Description:	ELIMINAR PROCESOS MTTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_EliminarProcesos]
@Opcion INT,
@idProcesoMtto INT,
@idVehiculo INT,
@Kilometraje DECIMAL(10,2),
@Intervalo DECIMAL(10,2)
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- ELIMINAR PROCESO
		DELETE FROM ReportesApp_Mantenimiento_MttoPreventivo_Recursos
		WHERE idProcesoMtto = @idProcesoMtto AND idVehiculo = @idVehiculo
		
		DELETE FROM ReportesApp_Mantenimiento_MttoPreventivo_HistorialProceso
		WHERE idProcesoMtto = @idProcesoMtto AND idVehiculo = @idVehiculo

		SET @Exito = '0 = Mantenimiento Eliminado Correctamente.'
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR HISTORIAL
		DELETE FROM ReportesApp_Mantenimiento_MttoPreventivo_HistorialProceso
		WHERE idProcesoMtto = @idProcesoMtto AND idVehiculo = @idVehiculo AND KMCambio = @Kilometraje AND Intervalo = @Intervalo
		SET @Exito = '0 = Mantenimiento Eliminado Correctamente.'
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

------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 16-01-2023
-- Description:	INSERTAR ACCESORIOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_InsertarAccesorio]
@Accesorio VARCHAR(100)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

BEGIN TRAN
BEGIN TRY
	IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_Accesorios WHERE Descripcion = @Accesorio)) BEGIN
		SET @Exito = '-1 = Este accesorio ya fue registrado.'
		ROLLBACK
		GOTO Terminar
	END
	ELSE BEGIN
		SET @correlativo = (SELECT MAX(idAccesorio) FROM ReportesApp_Mantenimiento_MttoPreventivo_Accesorios)
		SET @correlativo = ISNULL(@correlativo,0) + 1

		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_Accesorios(idAccesorio,Descripcion)
		VALUES(@correlativo, @Accesorio)

		SET @Exito = '0 = Accesorio añadido.'
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

------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 04-03-2023
-- Description:	INSERTAR KILOMETRAJE UNIDADES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_InsertarKMUnidad]
@xmlDetalle VARCHAR(MAX),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @idoc INT
DECLARE @TEMP_KM TABLE(
		Placa VARCHAR(20),
		Odometro DECIMAL(10,2))

IF(@xmlDetalle IS NOT NULL) BEGIN
	EXEC sp_xml_preparedocument @idoc OUTPUT, @xmlDetalle
	INSERT INTO @TEMP_KM(Placa, Odometro)
	SELECT * FROM OPENXML(@idoc,'/r/d',1)
	WITH (Placa VARCHAR(20), Odometro DECIMAL(10,2));
	EXEC sp_xml_removedocument @idoc;
END

BEGIN TRAN
BEGIN TRY
	UPDATE KM
	SET KM.KMActual = KM.ValorAdicional + CAST(UT.Odometro AS DECIMAL(10,2)), KM.Fecha = GETDATE()
	FROM ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM
	LEFT JOIN @TEMP_KM UT ON REPLACE(REPLACE(UT.Placa,'-',''),'.','') = REPLACE(REPLACE(KM.Placa,'-',''),'.','')
	WHERE REPLACE(REPLACE(UT.Placa,'-',''),'.','') = REPLACE(REPLACE(KM.Placa,'-',''),'.','')

	DELETE FROM ReportesApp_Mantenimiento_MttoPreventivo_KMRegistro
	WHERE CONVERT(DATE,Fecha) = CONVERT(DATE, GETDATE()) AND idTipoVehiculo IN (1,3)

	INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_KMRegistro (idKilometraje, idTipoVehiculo, Fecha, Kilometraje)
	SELECT idKilometraje, idTipoVehiculo, GETDATE(), KMActual FROM ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas WHERE idTipoVehiculo IN (1,3)
	
	SET @Exito = '0 = Kilometraje de Unidades Actualizado.'
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
SELECT @Exito exito

------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 24-01-2024
-- Description:	MODIFICAR KM DE UNIDADES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ActualizarKMUnidad]
@idVehiculo INT,
@Fecha DATETIME,
@UltKM DECIMAL(10,2)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @idKilometraje INT
DECLARE @ValorAdicional DECIMAL(10,2)

BEGIN TRAN
BEGIN TRY
	SET @idKilometraje = (SELECT idKilometraje FROM ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas WHERE idVehiculo = @idVehiculo)
	SET @ValorAdicional = (SELECT ISNULL(ValorAdicional,0) FROM ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas WHERE idVehiculo = @idVehiculo)

	IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_KMRegistro WHERE idKilometraje = @idKilometraje AND CONVERT(DATE,Fecha) = CONVERT(DATE,@Fecha))) BEGIN
		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_KMRegistro
		SET Fecha = @Fecha, Kilometraje = @UltKM + @ValorAdicional
		WHERE idKilometraje = @idKilometraje AND CONVERT(DATE,Fecha) = CONVERT(DATE,@Fecha)
		
		IF (CONVERT(DATE,@Fecha) = CONVERT(DATE,GETDATE())) BEGIN
			UPDATE ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas
			SET KMActual = @UltKM + @ValorAdicional, Fecha = @Fecha
			WHERE idVehiculo = @idVehiculo
		END

		SET @Exito = '0 = Kilometraje actualizado correctamente.'
	END
	ELSE BEGIN
		IF (CONVERT(DATE,@Fecha) <= CONVERT(DATE,GETDATE())) BEGIN
			INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_KMRegistro(idKilometraje,Fecha,Kilometraje)
			VALUES(@idKilometraje, @Fecha, @UltKM + @ValorAdicional)

			IF (CONVERT(DATE,@Fecha) = CONVERT(DATE,GETDATE())) BEGIN
				UPDATE ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas
				SET KMActual = @UltKM + @ValorAdicional, Fecha = @Fecha
				WHERE idVehiculo = @idVehiculo
			END

			SET @Exito = '0 = Kilometraje registrado correctamente.'
		END
		ELSE BEGIN
			SET @Exito = '-1 = El KM de esta unidad no se pudo actualizar porque no fue registrado en esa fecha.'
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

------------------------------------------------------------------
------------------------------------------------------------------

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_KMRegistroMaquinas

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_HistorialMaquinas

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMttoMaquinas

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_HistorialProcesoMaquinas

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_UnidadesTerceras Y LLENARLA

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_Recursos

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_RecursosMaquina

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_MaestroDesbloqueo

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 20-03-2023
-- Description:	LISTAR TIPO DE MAQUINA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarGrupoMaquina]
@Opcion INT
AS
BEGIN
	IF (@Opcion = 1) BEGIN		-- LISTAR TIPOS DE MÁQUINA
		SELECT LTRIM(RTRIM(TipoMaquinaGrupo)) AS 'TipoMaquinaGrupo', LTRIM(RTRIM(DescripcionLocal)) AS 'DescripcionLocal' FROM ME_MaquinaTipoGrupo
		WHERE TipoMaquinaGrupo IN ('0007','0009','0010')
		UNION
		SELECT '0099', 'UNIDADES TERCERAS'
	END
	
	IF (@Opcion = 2) BEGIN		-- LISTAR GRUPOS DE MÁQUINA
		SELECT '0000' AS 'TipoMaquina', 'TODOS' AS 'DescripcionLocal'
		UNION
		SELECT LTRIM(RTRIM(TipoMaquina)) AS 'TipoMaquina', LTRIM(RTRIM(DescripcionLocal)) AS 'DescripcionLocal' FROM ME_MaquinaTipo
		WHERE TipoMaquinaGrupo IN ('0007','0009','0010')
		UNION
		SELECT '0099', 'UNIDADES TERCERAS'
	END

	IF (@Opcion = 3) BEGIN		-- LISTAR TODOS LOS TIPOS DE MÁQUINAS
		SELECT '0000' AS 'TipoMaquina', 'TODOS' AS 'DescripcionLocal'
		UNION
		SELECT '0001' AS 'TipoMaquina', 'TRACTO' AS 'DescripcionLocal'
		UNION
		SELECT '0002' AS 'TipoMaquina', 'SEMIRREMOLQUE' AS 'DescripcionLocal'
		UNION
		SELECT LTRIM(RTRIM(TipoMaquina)) AS 'TipoMaquina', LTRIM(RTRIM(DescripcionLocal)) AS 'DescripcionLocal' FROM ME_MaquinaTipo
		WHERE TipoMaquinaGrupo IN ('0007','0009','0010')
		UNION
		SELECT '0099', 'UNIDADES TERCERAS'
	END

	IF (@Opcion = 4) BEGIN		-- LISTAR TIPOS DE MAQUINAS ESTACIONARIAS
		SELECT '0000' AS 'Equipo', 'TODOS' AS 'EquipoNombre'
		UNION
		SELECT DISTINCT EquipoNombre AS 'Equipo', EquipoNombre
		FROM ReportesApp_Mantenimiento_MttoPreventivo_RegistroEquipos
	END
END

------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 25-03-2024
-- Description:	BUSCAR MAQUINAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_BuscarMaquinas]
@Opcion INT,
@Placa VARCHAR(20),
@TipoMaquina VARCHAR(10)
AS
BEGIN
	IF (@Opcion = 1) BEGIN
		IF (@TipoMaquina = '0099') BEGIN
			SELECT 0 AS 'IdVehiculo', LTRIM(RTRIM(UT.NumeroPlaca)) AS 'PLACA', 5 AS 'IdProgramacion',
			'SIN OPERACION' AS 'OPERACION', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', UT.Modelo AS 'MODELO',
			CAST(ISNULL(KM.KMActual,0) AS DECIMAL(10,2)) AS 'KILOMETRAJE'
			FROM ReportesApp_Mantenimiento_MttoPreventivo_UnidadesTerceras UT
			LEFT JOIN ME_MaquinaMarca ME ON UT.Marca = ME.Marca
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(UT.NumeroPlaca))
			WHERE (@Placa IS NULL OR UT.NumeroPlaca LIKE '%' + @Placa + '%')
		END
		ELSE BEGIN
			SELECT ISNULL(V.IdVehiculo,CONVERT(INT,LTRIM(RTRIM(ISNULL(M.Proyecto,M.afe))))) AS 'IdVehiculo', LTRIM(RTRIM(M.MaquinaCodigo)) AS 'PLACA', 
			ISNULL(UC.IdProgramacion,5) AS 'IdProgramacion', ISNULL(O.Descripcion,'SIN OPERACION') AS 'OPERACION', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA',
			M.Modelo AS 'MODELO', CAST(ISNULL(KM.KMActual,0) AS DECIMAL(10,2)) AS 'KILOMETRAJE'
			FROM ME_Maquina M
			LEFT JOIN OP_TR_Vehiculo V ON LTRIM(RTRIM(V.Proyecto)) = LTRIM(RTRIM(ISNULL(M.Proyecto,afe)))
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON  O.IdOperacion = UC.IdProgramacion 
			LEFT JOIN ME_MaquinaMarca ME ON M.Marca = ME.Marca
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(M.MaquinaCodigo))
			WHERE (M.Estado = 'A') AND (LTRIM(RTRIM(M.TipoMaquinaGrupo)) = @TipoMaquina) AND (@Placa IS NULL OR M.MaquinaCodigo LIKE '%' + @Placa + '%')
		END
	END
	
	IF (@Opcion = 2) BEGIN
		SELECT TOP(15) ISNULL(V.IdVehiculo,CONVERT(INT,LTRIM(RTRIM(ISNULL(M.Proyecto,M.afe))))) AS 'IdVehiculo', LTRIM(RTRIM(M.MaquinaCodigo)) AS 'PLACA', 
		ISNULL(UC.IdProgramacion,5) AS 'IdProgramacion', ISNULL(O.Descripcion,'SIN OPERACION') AS 'OPERACION', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA',
		M.Modelo AS 'MODELO', CAST(ISNULL(KM.KMActual,0) AS DECIMAL(10,2)) AS 'KILOMETRAJE'
		FROM ME_Maquina M
		LEFT JOIN OP_TR_Vehiculo V ON LTRIM(RTRIM(V.Proyecto)) = LTRIM(RTRIM(ISNULL(M.Proyecto,afe)))
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON  O.IdOperacion = UC.IdProgramacion 
		LEFT JOIN ME_MaquinaMarca ME ON M.Marca = ME.Marca
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(M.MaquinaCodigo))
		WHERE (M.Estado = 'A') AND (@Placa IS NULL OR M.MaquinaCodigo LIKE '%' + @Placa + '%')
		UNION
		SELECT 0 AS 'IdVehiculo', LTRIM(RTRIM(UT.NumeroPlaca)) AS 'PLACA', 5 AS 'IdProgramacion',
		'SIN OPERACION' AS 'OPERACION', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', UT.Modelo AS 'MODELO',
		CAST(ISNULL(KM.KMActual,0) AS DECIMAL(10,2)) AS 'KILOMETRAJE'
		FROM ReportesApp_Mantenimiento_MttoPreventivo_UnidadesTerceras UT
		LEFT JOIN ME_MaquinaMarca ME ON UT.Marca = ME.Marca
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(UT.NumeroPlaca))
		WHERE (@Placa IS NULL OR UT.NumeroPlaca LIKE '%' + @Placa + '%')
	END
END

------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 10-01-2023
-- Description:	BUSCAR TRACTOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_BuscarTractos]
@Placa VARCHAR(20),
@TipoUnidad INT
AS
BEGIN
	SELECT V.IdVehiculo, V.NumeroPlaca AS 'PLACA', UC.IdProgramacion, CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION',
	LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', V.Modelo AS 'MODELO', CAST(ISNULL(KM.KMActual,0) AS DECIMAL(10,2)) AS 'KILOMETRAJE'
	FROM ReportesApp_Operacion_MaestroUnidadesConductor UC
	LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
	LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON  O.IdOperacion = UC.IdProgramacion 
	LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
	LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = V.IdVehiculo
	WHERE (V.Estado = 2) AND (UC.TipoUnidad = @TipoUnidad) AND (@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%')
END

------------------------------------------------------------------

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
@Dueno VARCHAR(250),
@Ubicacion VARCHAR(250),
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

		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas(idRegistroM,MaquinaCodigo,Aceite,Frecuencia,Dueno,Ubicacion,UltimaFecha,UltimoKM,
		TipoMantenimiento,Usuario,FechaCreacion,idMantenimientoOP,PS)
		VALUES(@correlativo, LTRIM(RTRIM(@MaquinaCodigo)), @Aceite, @Frecuencia, @Dueno, @Ubicacion, @UltimaFecha, @UltimoKM, @TipoMantenimiento,
		@Usuario, GETDATE(),@idMttoOP,@PS)

		SET @Exito = '0 = Mantenimiento Programado Correctamente.'
	END
	
	IF (@Opcion = 2) BEGIN
		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_HistorialMaquinas(idRegistro,MaquinaCodigo,Aceite,UltimaFecha,UltimoKM,PS,TipoMantenimiento,Usuario,FechaCreacion)
		SELECT idRegistroM,MaquinaCodigo,Aceite,UltimaFecha,UltimoKM,PS,TipoMantenimiento,Usuario,FechaCreacion
		FROM ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas
		WHERE idRegistroM = @idRegistroM

		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas
		SET UltimaFecha = @UltimaFecha,UltimoKM = @UltimoKM,TipoMantenimiento = @TipoMantenimiento, Frecuencia = @Frecuencia, Dueno = @Dueno,
		Ubicacion = @Ubicacion, Usuario = @Usuario, FechaCreacion = GETDATE(), idMantenimientoOP = @idMttoOP, PS = @PS
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

------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 10-01-2023
-- Description:	LISTAR MANTENIMIENTOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarMttos]
@Placa VARCHAR(20),
@idTipoVehiculo INT,
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'

BEGIN
	IF (@idTipoVehiculo = 0) BEGIN
		SELECT X.NRO, X.idVehiculo, X.PLACA, X.TIPO_UNIDAD, X.SUBTIPO_UNIDAD, X.OPERACION, X.ACEITE, X.MARCA, X.MODELO, X.[FREC/KM], X.FECHA_UM,
		X.KM_ANTERIOR, X.idMantenimientoOP, X.PS, X.TIPO_MTTO, X.FECHA_ACTUAL, X.KM_ACTUAL, X.[PORCENTAJE (%)],
		(CASE WHEN X.[PORCENTAJE (%)] < 60 THEN 'CONFORME'
			  WHEN X.[PORCENTAJE (%)] > 60 AND X.[PORCENTAJE (%)] < 100 THEN 'POR VENCER'
			  WHEN X.[PORCENTAJE (%)] > 100 THEN 'VENCIDO'
			  ELSE '' END) AS 'ESTADO', X.[KM/DIA], X.KM_ESTIMADO, X.DIFERENCIA_KM, X.PROXIMO_MTTO,
		DATEPART(WEEK, (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END)) AS 'SEMANA',
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		X.ULTIMO_USUARIO, X.ULTIMA_FECHA FROM 
		(SELECT MR.idRegistro AS 'NRO', MR.idVehiculo, V.NumeroPlaca AS 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD', SV.Descripcion AS 'SUBTIPO_UNIDAD', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion
		END AS 'OPERACION', MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', V.Modelo AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
		MR.TipoMantenimiento AS 'TIPO_MTTO', (SELECT ISNULL(TipoMantenimiento,CASE WHEN MR.Aceite = 'SINTETICO' THEN 'M0' ELSE 'M1' END) FROM ReportesApp_Mantenimiento_MttoPreventivo_TipoMtto WHERE idMantenimientoOP = MR.idMantenimientoOP AND Posicion = MR.PS + 1) AS 'PROXIMO_MTTO',
		KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL', CAST((KM.KMActual - MR.UltimoKM) / MR.Frecuencia * 100 AS DECIMAL(10,2)) AS 'PORCENTAJE (%)',
		CASE WHEN DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) = 0 THEN 1 ELSE
		CAST((KM.KMActual - MR.UltimoKM) / DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA',
		CAST(MR.Frecuencia + MR.UltimoKM AS DECIMAL(10,2)) AS 'KM_ESTIMADO', CAST((MR.Frecuencia + MR.UltimoKM) - KM.KMActual AS DECIMAL(10,2)) AS 'DIFERENCIA_KM',
		MR.Usuario AS 'ULTIMO_USUARIO', MR.FechaCreacion AS 'ULTIMA_FECHA'
		FROM ReportesApp_Mantenimiento_MttoPreventivo_Registro MR
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = MR.idVehiculo
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON UC.IdProgramacion = O.IdOperacion
		LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
		LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TipoVehiculo
		LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
		LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = MR.idVehiculo) X
		WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND
		(CASE WHEN (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) <= CONVERT(DATE,GETDATE()) THEN GETDATE()
		ELSE (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) END BETWEEN @FINICIO AND @FFIN)
		ORDER BY X.PLACA ASC
	END
	ELSE BEGIN
		SELECT X.NRO, X.idVehiculo, X.PLACA, X.TIPO_UNIDAD, X.SUBTIPO_UNIDAD, X.OPERACION, X.ACEITE, X.MARCA, X.MODELO, X.[FREC/KM], X.FECHA_UM,
		X.KM_ANTERIOR, X.idMantenimientoOP, X.PS, X.TIPO_MTTO, X.FECHA_ACTUAL, X.KM_ACTUAL, X.[PORCENTAJE (%)],
		(CASE WHEN X.[PORCENTAJE (%)] < 60 THEN 'CONFORME'
			  WHEN X.[PORCENTAJE (%)] > 60 AND X.[PORCENTAJE (%)] < 100 THEN 'POR VENCER'
			  WHEN X.[PORCENTAJE (%)] > 100 THEN 'VENCIDO'
			  ELSE '' END) AS 'ESTADO', X.[KM/DIA], X.KM_ESTIMADO, X.DIFERENCIA_KM, X.PROXIMO_MTTO,
		DATEPART(WEEK, (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END)) AS 'SEMANA',
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		X.ULTIMO_USUARIO, X.ULTIMA_FECHA FROM 
		(SELECT MR.idRegistro AS 'NRO', MR.idVehiculo, V.NumeroPlaca AS 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD', SV.Descripcion AS 'SUBTIPO_UNIDAD', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion
		END AS 'OPERACION', MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', V.Modelo AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
		MR.TipoMantenimiento AS 'TIPO_MTTO', (SELECT ISNULL(TipoMantenimiento,CASE WHEN MR.Aceite = 'SINTETICO' THEN 'M0' ELSE 'M1' END) FROM ReportesApp_Mantenimiento_MttoPreventivo_TipoMtto WHERE idMantenimientoOP = MR.idMantenimientoOP AND Posicion = MR.PS + 1) AS 'PROXIMO_MTTO',
		KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL', CAST((KM.KMActual - MR.UltimoKM) / MR.Frecuencia * 100 AS DECIMAL(10,2)) AS 'PORCENTAJE (%)',
		CASE WHEN DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) = 0 THEN 1 ELSE
		CAST((KM.KMActual - MR.UltimoKM) / DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA',
		CAST(MR.Frecuencia + MR.UltimoKM AS DECIMAL(10,2)) AS 'KM_ESTIMADO', CAST((MR.Frecuencia + MR.UltimoKM) - KM.KMActual AS DECIMAL(10,2)) AS 'DIFERENCIA_KM',
		MR.Usuario AS 'ULTIMO_USUARIO', MR.FechaCreacion AS 'ULTIMA_FECHA'
		FROM ReportesApp_Mantenimiento_MttoPreventivo_Registro MR
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = MR.idVehiculo
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON UC.IdProgramacion = O.IdOperacion
		LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
		LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TipoVehiculo
		LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
		LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = MR.idVehiculo
		WHERE TV.idTipoVehiculo = @idTipoVehiculo) X
		WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND
		(CASE WHEN (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) <= CONVERT(DATE,GETDATE()) THEN GETDATE()
		ELSE (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) END BETWEEN @FINICIO AND @FFIN)
		ORDER BY X.PLACA ASC
	END
END

------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 25-03-2023
-- Description:	LISTAR MANTENIMIENTOS MAQUINAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarMttosMaquinas]
@Placa VARCHAR(20),
@TipoMaquina VARCHAR(10),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'

BEGIN
	IF (@TipoMaquina = '0000') BEGIN
		SELECT X.NRO, X.USUARIO, X.UBICACION, X.PLACA, X.GRUPO, X.MAQUINA, X.ACEITE, X.MARCA, X.MODELO, X.[FREC/KM], X.FECHA_UM,
		X.KM_ANTERIOR, X.idMantenimientoOP, X.PS, X.TIPO_MTTO, X.FECHA_ACTUAL, X.KM_ACTUAL, X.[PORCENTAJE (%)],
		(CASE WHEN X.[PORCENTAJE (%)] < 60 THEN 'CONFORME'
			  WHEN X.[PORCENTAJE (%)] >= 60 AND X.[PORCENTAJE (%)] < 100 THEN 'POR VENCER'
			  WHEN X.[PORCENTAJE (%)] >= 100 THEN 'VENCIDO'
			  ELSE '' END) AS 'ESTADO', X.[KM/DIA], X.KM_ESTIMADO, X.DIFERENCIA_KM, X.PROXIMO_MTTO,
		CASE WHEN DATEPART(W,(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END))
		IN (6,7) THEN DATEPART(WW,DATEADD(DAY,7,(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END)))
		ELSE DATEPART(WW,(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END)) END AS 'SEMANA',
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		X.ULTIMO_USUARIO, X.ULTIMA_FECHA FROM 
		(SELECT DISTINCT MR.idRegistroM AS 'NRO', LTRIM(RTRIM(MR.MaquinaCodigo)) AS 'PLACA', LTRIM(RTRIM(ISNULL(TG.DescripcionLocal,'UNIDADES TERCERAS'))) AS 'GRUPO', LTRIM(RTRIM(T.DescripcionLocal)) AS 'MAQUINA',
		MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', ISNULL(M.Modelo,UT.Modelo) AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.Dueno AS 'USUARIO',
		MR.Ubicacion AS 'UBICACION', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS, MR.TipoMantenimiento AS 'TIPO_MTTO',
		(SELECT ISNULL(TipoMantenimiento,CASE WHEN MR.Aceite = 'SINTETICO' THEN 'M0' ELSE 'M1' END) FROM ReportesApp_Mantenimiento_MttoPreventivo_TipoMtto WHERE idMantenimientoOP = MR.idMantenimientoOP AND Posicion = MR.PS + 1) AS 'PROXIMO_MTTO',
		KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL', CAST((KM.KMActual - MR.UltimoKM) / MR.Frecuencia * 100 AS DECIMAL(10,2)) AS 'PORCENTAJE (%)',
		CASE WHEN DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) = 0 THEN 1 ELSE
		CAST((KM.KMActual - MR.UltimoKM) / DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA',
		CAST(MR.Frecuencia + MR.UltimoKM AS DECIMAL(10,2)) AS 'KM_ESTIMADO', CAST((MR.Frecuencia + MR.UltimoKM) - KM.KMActual AS DECIMAL(10,2)) AS 'DIFERENCIA_KM',
		MR.Usuario AS 'ULTIMO_USUARIO', MR.FechaCreacion AS 'ULTIMA_FECHA'
		FROM ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas MR
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_UnidadesTerceras UT ON LTRIM(RTRIM(UT.NumeroPlaca)) = LTRIM(RTRIM(MR.MaquinaCodigo))
		LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo))
		LEFT JOIN ME_MaquinaMarca ME ON ISNULL(M.Marca,UT.Marca) = ME.Marca
		LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
		LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo)) AND (M.Estado = 'A')) X
		WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND
		(CASE WHEN (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) <= CONVERT(DATE,GETDATE()) THEN GETDATE()
		ELSE (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) END BETWEEN @FINICIO AND @FFIN)
		ORDER BY X.PLACA ASC
	END
	ELSE BEGIN
		IF (@TipoMaquina = '0099') BEGIN
			SELECT X.NRO, X.USUARIO, X.UBICACION, X.PLACA, X.GRUPO, X.MAQUINA, X.ACEITE, X.MARCA, X.MODELO, X.[FREC/KM], X.FECHA_UM,
			X.KM_ANTERIOR, X.idMantenimientoOP, X.PS, X.TIPO_MTTO, X.FECHA_ACTUAL, X.KM_ACTUAL, X.[PORCENTAJE (%)],
			(CASE WHEN X.[PORCENTAJE (%)] < 60 THEN 'CONFORME'
				  WHEN X.[PORCENTAJE (%)] >= 60 AND X.[PORCENTAJE (%)] < 100 THEN 'POR VENCER'
				  WHEN X.[PORCENTAJE (%)] >= 100 THEN 'VENCIDO'
				  ELSE '' END) AS 'ESTADO', X.[KM/DIA], X.KM_ESTIMADO, X.DIFERENCIA_KM, X.PROXIMO_MTTO,
			CASE WHEN DATEPART(W,(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END))
			IN (6,7) THEN DATEPART(WW,DATEADD(DAY,7,(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END)))
			ELSE DATEPART(WW,(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END)) END AS 'SEMANA',
			(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
			X.ULTIMO_USUARIO, X.ULTIMA_FECHA FROM 
			(SELECT DISTINCT MR.idRegistroM AS 'NRO', LTRIM(RTRIM(MR.MaquinaCodigo)) AS 'PLACA', 'UNIDADES TERCERAS' AS 'GRUPO', ' ' AS 'MAQUINA',
			MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', UT.Modelo AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.Dueno AS 'USUARIO',
			MR.Ubicacion AS 'UBICACION', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS, MR.TipoMantenimiento AS 'TIPO_MTTO',
			(SELECT ISNULL(TipoMantenimiento,CASE WHEN MR.Aceite = 'SINTETICO' THEN 'M0' ELSE 'M1' END) FROM ReportesApp_Mantenimiento_MttoPreventivo_TipoMtto WHERE idMantenimientoOP = MR.idMantenimientoOP AND Posicion = MR.PS + 1) AS 'PROXIMO_MTTO',
			KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL', CAST((KM.KMActual - MR.UltimoKM) / MR.Frecuencia * 100 AS DECIMAL(10,2)) AS 'PORCENTAJE (%)',
			CASE WHEN DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) = 0 THEN 1 ELSE
			CAST((KM.KMActual - MR.UltimoKM) / DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA',
			CAST(MR.Frecuencia + MR.UltimoKM AS DECIMAL(10,2)) AS 'KM_ESTIMADO', CAST((MR.Frecuencia + MR.UltimoKM) - KM.KMActual AS DECIMAL(10,2)) AS 'DIFERENCIA_KM',
			MR.Usuario AS 'ULTIMO_USUARIO', MR.FechaCreacion AS 'ULTIMA_FECHA'
			FROM ReportesApp_Mantenimiento_MttoPreventivo_UnidadesTerceras UT
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas MR ON LTRIM(RTRIM(UT.NumeroPlaca)) = LTRIM(RTRIM(MR.MaquinaCodigo))
			LEFT JOIN ME_MaquinaMarca ME ON UT.Marca = ME.Marca
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo))) X
			WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND
			(CASE WHEN (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) <= CONVERT(DATE,GETDATE()) THEN GETDATE()
			ELSE (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) END BETWEEN @FINICIO AND @FFIN)
			ORDER BY X.PLACA ASC
		END
		ELSE BEGIN
			SELECT X.NRO, X.USUARIO, X.UBICACION, X.PLACA, X.GRUPO, X.MAQUINA, X.ACEITE, X.MARCA, X.MODELO, X.[FREC/KM], X.FECHA_UM,
			X.KM_ANTERIOR, X.idMantenimientoOP, X.PS, X.TIPO_MTTO, X.FECHA_ACTUAL, X.KM_ACTUAL, X.[PORCENTAJE (%)],
			(CASE WHEN X.[PORCENTAJE (%)] < 60 THEN 'CONFORME'
				  WHEN X.[PORCENTAJE (%)] >= 60 AND X.[PORCENTAJE (%)] < 100 THEN 'POR VENCER'
				  WHEN X.[PORCENTAJE (%)] >= 100 THEN 'VENCIDO'
				  ELSE '' END) AS 'ESTADO', X.[KM/DIA], X.KM_ESTIMADO, X.DIFERENCIA_KM, X.PROXIMO_MTTO,
			DATEPART(WEEK, (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END)) AS 'SEMANA',
			(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
			X.ULTIMO_USUARIO, X.ULTIMA_FECHA FROM 
			(SELECT DISTINCT MR.idRegistroM AS 'NRO', LTRIM(RTRIM(MR.MaquinaCodigo)) AS 'PLACA', LTRIM(RTRIM(TG.DescripcionLocal)) AS 'GRUPO', LTRIM(RTRIM(T.DescripcionLocal)) AS 'MAQUINA',
			MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', M.Modelo AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.Dueno AS 'USUARIO',
			MR.Ubicacion AS 'UBICACION', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS, MR.TipoMantenimiento AS 'TIPO_MTTO',
			(SELECT ISNULL(TipoMantenimiento,CASE WHEN MR.Aceite = 'SINTETICO' THEN 'M0' ELSE 'M1' END) FROM ReportesApp_Mantenimiento_MttoPreventivo_TipoMtto WHERE idMantenimientoOP = MR.idMantenimientoOP AND Posicion = MR.PS + 1) AS 'PROXIMO_MTTO',
			KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL', CAST((KM.KMActual - MR.UltimoKM) / MR.Frecuencia * 100 AS DECIMAL(10,2)) AS 'PORCENTAJE (%)',
			CASE WHEN DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) = 0 THEN 1 ELSE
			CAST((KM.KMActual - MR.UltimoKM) / DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA',
			CAST(MR.Frecuencia + MR.UltimoKM AS DECIMAL(10,2)) AS 'KM_ESTIMADO', CAST((MR.Frecuencia + MR.UltimoKM) - KM.KMActual AS DECIMAL(10,2)) AS 'DIFERENCIA_KM',
			MR.Usuario AS 'ULTIMO_USUARIO', MR.FechaCreacion AS 'ULTIMA_FECHA'
			FROM ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas MR
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo)) 
			LEFT JOIN ME_MaquinaMarca ME ON M.Marca = ME.Marca
			LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
			LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo))
			WHERE LTRIM(RTRIM(T.TipoMaquina)) = @TipoMaquina AND (M.Estado = 'A')) X
			WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND
			(CASE WHEN (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) <= CONVERT(DATE,GETDATE()) THEN GETDATE()
			ELSE (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) END BETWEEN @FINICIO AND @FFIN)
			ORDER BY X.PLACA ASC
		END
	END
END

------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 16-01-2023
-- Description:	LISTAR REGISTRO DIARIO DE KM
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarRegistroKM]
@Placa VARCHAR(20),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	SELECT ROW_NUMBER() OVER(ORDER BY V.NumeroPlaca ASC) AS 'N°', KM.Estado AS 'ESTADO', V.IdVehiculo, V.NumeroPlaca AS 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD',
	CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION', CONVERT(VARCHAR,KR.Fecha,103) AS 'FECHA',
	KR.Kilometraje AS 'KM_REAL', KM.ValorAdicional AS 'KM_ADICIONAL_X_UNIDAD', KR.Kilometraje - KM.ValorAdicional AS 'KM_ACTUALIZADO'
	FROM ReportesApp_Mantenimiento_MttoPreventivo_KMRegistro KR
	LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idKilometraje = KR.idKilometraje
	LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = KM.idVehiculo
	LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
	LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TipoVehiculo
	LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = UC.IdProgramacion 
	WHERE (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND (KR.Fecha BETWEEN @FINICIO AND @FFIN) AND (V.Estado = 2)
	ORDER BY V.NumeroPlaca ASC
END

------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 26-03-2023
-- Description:	LISTAR REGISTRO DIARIO DE KM
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarRegistroKMMaquina]
@Placa VARCHAR(20),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	SELECT ROW_NUMBER() OVER(ORDER BY KR.MaquinaCodigo ASC) AS 'N°', KM.Estado AS 'ESTADO', LTRIM(RTRIM(KR.MaquinaCodigo)) AS 'PLACA',
	LTRIM(RTRIM(ISNULL(TG.DescripcionLocal,'UNIDADES TERCERAS'))) AS 'GRUPO', LTRIM(RTRIM(T.DescripcionLocal)) AS 'MAQUINA', CONVERT(VARCHAR,KR.Fecha,103) AS 'FECHA',
	KR.Kilometraje AS 'KM_REAL', KM.ValorAdicional AS 'KM_ADICIONAL_X_UNIDAD', KR.Kilometraje - KM.ValorAdicional AS 'KM_ACTUALIZADO'
	FROM ReportesApp_Mantenimiento_MttoPreventivo_KMRegistroMaquinas KR
	LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON KM.idKilometraje = KR.idKilometraje
	LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(KM.MaquinaCodigo)) AND (M.Estado = 'A')
	LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
	LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
	WHERE (M.MaquinaCodigo IS NULL OR M.MaquinaCodigo LIKE '%' + @Placa + '%') AND (KR.Fecha BETWEEN @FINICIO AND @FFIN)
	ORDER BY M.MaquinaCodigo ASC, KR.Fecha DESC
END

------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 26-03-2024
-- Description:	MODIFICAR KM DE MAQUINARIAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ActualizarKMMaquinas]
@MaquinaCodigo VARCHAR(80),
@Fecha DATETIME,
@UltKM DECIMAL(10,2)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @idKilometraje INT
DECLARE @ValorAdicional DECIMAL(10,2)

BEGIN TRAN
BEGIN TRY
	SET @idKilometraje = (SELECT idKilometraje FROM ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas WHERE LTRIM(RTRIM(MaquinaCodigo)) = LTRIM(RTRIM(@MaquinaCodigo)))
	SET @ValorAdicional = (SELECT ISNULL(ValorAdicional,0) FROM ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas WHERE LTRIM(RTRIM(MaquinaCodigo)) = LTRIM(RTRIM(@MaquinaCodigo)))

	IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_KMRegistroMaquinas WHERE idKilometraje = @idKilometraje AND CONVERT(DATE,Fecha) = CONVERT(DATE,@Fecha))) BEGIN
		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_KMRegistroMaquinas
		SET Fecha = @Fecha, Kilometraje = @UltKM + @ValorAdicional
		WHERE idKilometraje = @idKilometraje AND CONVERT(DATE,Fecha) = CONVERT(DATE,@Fecha)
		
		IF (CONVERT(DATE,@Fecha) = (SELECT TOP(1) CONVERT(DATE,Fecha) FROM ReportesApp_Mantenimiento_MttoPreventivo_KMRegistroMaquinas
		WHERE MaquinaCodigo = @MaquinaCodigo ORDER BY Fecha DESC)) BEGIN
			UPDATE ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas
			SET KMActual = @UltKM + @ValorAdicional, Fecha = @Fecha
			WHERE LTRIM(RTRIM(MaquinaCodigo)) = LTRIM(RTRIM(@MaquinaCodigo))
		END

		SET @Exito = '0 = Kilometraje actualizado correctamente.'
	END
	ELSE BEGIN
		IF (CONVERT(DATE,@Fecha) <= CONVERT(DATE,GETDATE())) BEGIN
			INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_KMRegistroMaquinas(idKilometraje,Fecha,Kilometraje, MaquinaCodigo)
			VALUES(@idKilometraje, @Fecha, @UltKM + @ValorAdicional, @MaquinaCodigo)

			IF (CONVERT(DATE,@Fecha) = (SELECT TOP(1) CONVERT(DATE,Fecha) FROM ReportesApp_Mantenimiento_MttoPreventivo_KMRegistroMaquinas
			WHERE MaquinaCodigo = @MaquinaCodigo ORDER BY Fecha DESC)) BEGIN
				UPDATE ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas
				SET KMActual = @UltKM + @ValorAdicional, Fecha = @Fecha
				WHERE LTRIM(RTRIM(MaquinaCodigo)) = LTRIM(RTRIM(@MaquinaCodigo))
			END

			SET @Exito = '0 = Kilometraje registrado correctamente.'
		END
		ELSE BEGIN
			SET @Exito = '-1 = El KM de esta unidad no se pudo actualizar porque no fue registrado en esa fecha.'
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

------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 09-03-2024
-- Description:	MODIFICAR FRECUENCIA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ModificarFrecuencia]
@Opcion INT,
@idVehiculo INT,
@MaquinaCodigo VARCHAR(50),
@Frecuencia INT
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- FRECUENCIA DE VEHÍCULOS
		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_Registro
		SET Frecuencia = @Frecuencia
		WHERE idVehiculo = @idVehiculo
	END
	
	IF (@Opcion = 2) BEGIN		-- FRECUENCIA DE MAQUINARIAS
		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas
		SET Frecuencia = @Frecuencia
		WHERE LTRIM(RTRIM(MaquinaCodigo)) = LTRIM(RTRIM(@MaquinaCodigo))
	END
	SET @Exito = '0 = Frecuencia actualizada.'
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

------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 10-01-2023
-- Description:	LISTAR MANTENIMIENTOS HISTORIAL
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialMtto]
@Placa VARCHAR(20),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@idTipoVehiculo INT
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@idTipoVehiculo = 0) BEGIN
		SELECT V.NumeroPlaca AS 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION',
		MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', V.Modelo AS 'MODELO', MR.UltimaFecha AS 'FECHA_ANTERIOR', MR.UltimoKM AS 'KM',
		MR.PS, MR.TipoMantenimiento AS 'MTTO_ANTERIOR', MR.Usuario AS 'ULTIMO_USUARIO', MR.FechaCreacion AS 'ULTIMA_FECHA'
		FROM ReportesApp_Mantenimiento_MttoPreventivo_Historial MR
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = MR.idVehiculo
		LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = MR.idOperacion
		LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
		LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TipoVehiculo
		WHERE (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND (MR.FechaCreacion BETWEEN @FINICIO AND @FFIN)
		ORDER BY MR.FechaCreacion DESC
	END
	ELSE BEGIN
		SELECT V.NumeroPlaca AS 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION',
		MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', V.Modelo AS 'MODELO', MR.UltimaFecha AS 'FECHA_ANTERIOR', MR.UltimoKM AS 'KM',
		MR.PS, MR.TipoMantenimiento AS 'MTTO_ANTERIOR', MR.Usuario AS 'ULTIMO_USUARIO', MR.FechaCreacion AS 'ULTIMA_FECHA'
		FROM ReportesApp_Mantenimiento_MttoPreventivo_Historial MR
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = MR.idVehiculo
		LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = MR.idOperacion
		LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
		LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TipoVehiculo
		WHERE (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND (MR.FechaCreacion BETWEEN @FINICIO AND @FFIN) AND (TV.idTipoVehiculo = @idTipoVehiculo)
		ORDER BY MR.FechaCreacion DESC
	END
END

------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 26-03-2024
-- Description:	LISTAR HISTORIAL DE MAQUINARIAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialMaquinarias]
@Placa VARCHAR(20),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@TipoMaquina VARCHAR(10)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@TipoMaquina = '0000') BEGIN
		SELECT LTRIM(RTRIM(MR.MaquinaCodigo)) AS 'PLACA', LTRIM(RTRIM(ISNULL(TG.DescripcionLocal,'UNIDADES TERCERAS'))) AS 'GRUPO', LTRIM(RTRIM(T.DescripcionLocal)) AS 'MAQUINA',
		MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', ISNULL(M.Modelo,UT.Modelo) AS 'MODELO', MR.UltimaFecha AS 'FECHA_ANTERIOR', MR.UltimoKM AS 'KM',
		MR.PS, MR.TipoMantenimiento AS 'MTTO_ANTERIOR', MR.Usuario AS 'ULTIMO_USUARIO', MR.FechaCreacion AS 'ULTIMA_FECHA'
		FROM ReportesApp_Mantenimiento_MttoPreventivo_HistorialMaquinas MR
		LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo)) AND (M.Estado = 'A')
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_UnidadesTerceras UT ON LTRIM(RTRIM(UT.NumeroPlaca)) = LTRIM(RTRIM(MR.MaquinaCodigo))
		LEFT JOIN ME_MaquinaMarca ME ON ISNULL(M.Marca,UT.Marca) = ME.Marca
		LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
		LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
		WHERE (MR.MaquinaCodigo IS NULL OR MR.MaquinaCodigo LIKE '%' + @Placa + '%') AND (MR.FechaCreacion BETWEEN @FINICIO AND @FFIN)
		ORDER BY MR.FechaCreacion DESC
	END
	ELSE BEGIN
		IF (@TipoMaquina = '0099') BEGIN
			SELECT LTRIM(RTRIM(MR.MaquinaCodigo)) AS 'PLACA', 'UNIDADES TERCERAS' AS 'GRUPO', ' ' AS 'MAQUINA', MR.Aceite AS 'ACEITE',
			LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', UT.Modelo AS 'MODELO', MR.UltimaFecha AS 'FECHA_ANTERIOR', MR.UltimoKM AS 'KM',
			MR.PS, MR.TipoMantenimiento AS 'MTTO_ANTERIOR', MR.Usuario AS 'ULTIMO_USUARIO', MR.FechaCreacion AS 'ULTIMA_FECHA'
			FROM ReportesApp_Mantenimiento_MttoPreventivo_UnidadesTerceras UT
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_HistorialMaquinas MR ON LTRIM(RTRIM(UT.NumeroPlaca)) = LTRIM(RTRIM(MR.MaquinaCodigo))
			LEFT JOIN ME_MaquinaMarca ME ON UT.Marca = ME.Marca
			WHERE (MR.MaquinaCodigo IS NULL OR MR.MaquinaCodigo LIKE '%' + @Placa + '%') AND (MR.FechaCreacion BETWEEN @FINICIO AND @FFIN)
			ORDER BY MR.FechaCreacion DESC
		END
		ELSE BEGIN
			SELECT LTRIM(RTRIM(MR.MaquinaCodigo)) AS 'PLACA', LTRIM(RTRIM(TG.DescripcionLocal)) AS 'GRUPO', LTRIM(RTRIM(T.DescripcionLocal)) AS 'MAQUINA', 
			MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', M.Modelo AS 'MODELO', MR.UltimaFecha AS 'FECHA_ANTERIOR', MR.UltimoKM AS 'KM',
			MR.PS, MR.TipoMantenimiento AS 'MTTO_ANTERIOR', MR.Usuario AS 'ULTIMO_USUARIO', MR.FechaCreacion AS 'ULTIMA_FECHA'
			FROM ReportesApp_Mantenimiento_MttoPreventivo_HistorialMaquinas MR
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo)) AND (M.Estado = 'A')
			LEFT JOIN ME_MaquinaMarca ME ON M.Marca = ME.Marca
			LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
			LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
			WHERE (MR.MaquinaCodigo IS NULL OR MR.MaquinaCodigo LIKE '%' + @Placa + '%') AND (MR.FechaCreacion BETWEEN @FINICIO AND @FFIN) AND (LTRIM(RTRIM(T.TipoMaquina)) = @TipoMaquina)
			ORDER BY MR.FechaCreacion DESC
		END
	END
END

-----------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 11-01-2023
-- Description:	FILTRAR MANTENIMIENTOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_FiltrarMttos]
@Opcion INT,
@idRegistro INT
AS
BEGIN
	IF (@Opcion = 1) BEGIN
		SELECT V.NumeroPlaca AS 'PLACA', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION', MR.Aceite AS 'ACEITE',
		LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', V.Modelo AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM1',
		MR.TipoMantenimiento AS 'TIPO_MTTO', KM.KMActual AS 'KM2', MR.PS
		FROM ReportesApp_Mantenimiento_MttoPreventivo_Registro MR
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = MR.idVehiculo
		LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = MR.idOperacion
		LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = MR.idVehiculo
		WHERE (MR.idRegistro = @idRegistro)
	END
	
	IF (@Opcion = 2) BEGIN
		SELECT LTRIM(RTRIM(MR.MaquinaCodigo)) AS 'PLACA', MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', ISNULL(M.Modelo,UT.Modelo) AS 'MODELO',
		MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM1', MR.TipoMantenimiento AS 'TIPO_MTTO', KM.KMActual AS 'KM2', MR.PS,
		MR.Dueno AS 'USUARIO', MR.Ubicacion AS 'UBICACION'
		FROM ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas MR
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_UnidadesTerceras UT ON LTRIM(RTRIM(UT.NumeroPlaca)) = LTRIM(RTRIM(MR.MaquinaCodigo))
		LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo)) AND (M.Estado = 'A')
		LEFT JOIN ME_MaquinaMarca ME ON ISNULL(M.Marca,UT.Marca) = ME.Marca
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo))
		WHERE (MR.idRegistroM = @idRegistro)
	END
END

------------------------------------------------------------------

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

------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 27-03-2023
-- Description:	LISTAR MANTENIMIENTOS DE MAQUINAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarControlMttoMaquinas]
@MaquinaCodigo VARCHAR(50)
AS
BEGIN
	SELECT X.idProcesoMtto, X.MaquinaCodigo, X.ACCESORIO, X.FECHA_CAMBIO, X.KM_CAMBIO, X.INTERVALO, X.FECHA_ACTUAL, X.KM_ACTUAL, X.TIEMPO_MESES,
	X.KM_RECORRIDO, X.[KM/DIA], X.KM_FALTANTE, X.[%], (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL)
	END) AS 'FECHA_PROYECTADA', X.Usuario, X.Fecha
	FROM (SELECT PM.idAccesorio, PM.idProcesoMtto, PM.MaquinaCodigo, AC.Descripcion AS 'ACCESORIO', PM.FechaCambio AS 'FECHA_CAMBIO', PM.KMCambio AS 'KM_CAMBIO',
	PM.Intervalo AS 'INTERVALO', KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL', DATEDIFF(MONTH,PM.FechaCambio,KM.Fecha) AS 'TIEMPO_MESES',
	CASE WHEN PM.KMCambio = 0 THEN 0 ELSE KM.KMActual - PM.KMCambio END AS 'KM_RECORRIDO', CASE WHEN DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) = 0 THEN 1 ELSE
	CAST((KM.KMActual - PM.KMCambio) / DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA', PM.Intervalo - (KM.KMActual - PM.KMCambio) AS 'KM_FALTANTE',
	CASE WHEN PM.Intervalo = 0 THEN 0 ELSE CAST((1-(PM.Intervalo - (KM.KMActual - PM.KMCambio)) / PM.Intervalo) * 100 AS DECIMAL(10,2)) END AS '%',
	PM.Usuario, PM.Fecha FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMttoMaquinas PM
	LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Accesorios AC ON AC.idAccesorio = PM.idAccesorio
	LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(PM.MaquinaCodigo))
	WHERE LTRIM(RTRIM(PM.MaquinaCodigo)) = LTRIM(RTRIM(@MaquinaCodigo))) X
	ORDER BY X.idAccesorio ASC
END

------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 27-03-2023
-- Description:	LISTAR PROCESOS HISTORIAL MAQUINAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialProcesosMaquinas]
@MaquinaCodigo VARCHAR(50),
@idAccesorio INT
AS
BEGIN
	IF (@idAccesorio = 0) BEGIN
		SELECT TOP(30) HP.idProcesoMtto, HP.MaquinaCodigo, AC.Descripcion AS 'ACCESORIO', HP.FechaCambio AS 'FECHA_CAMBIO', HP.KMCambio AS 'KM_CAMBIO',
		HP.Intervalo AS 'INTERVALO', HP.Usuario, HP.Fecha FROM ReportesApp_Mantenimiento_MttoPreventivo_HistorialProcesoMaquinas HP
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Accesorios AC ON AC.idAccesorio = HP.idAccesorio
		WHERE LTRIM(RTRIM(HP.MaquinaCodigo)) = LTRIM(RTRIM(@MaquinaCodigo))
		ORDER BY HP.Fecha DESC
	END
	ELSE BEGIN
		SELECT TOP(30) HP.idProcesoMtto, HP.MaquinaCodigo, AC.Descripcion AS 'ACCESORIO', HP.FechaCambio AS 'FECHA_CAMBIO', HP.KMCambio AS 'KM_CAMBIO',
		HP.Intervalo AS 'INTERVALO', HP.Usuario, HP.Fecha FROM ReportesApp_Mantenimiento_MttoPreventivo_HistorialProcesoMaquinas HP
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Accesorios AC ON AC.idAccesorio = HP.idAccesorio
		WHERE (LTRIM(RTRIM(HP.MaquinaCodigo)) = LTRIM(RTRIM(@MaquinaCodigo))) AND (HP.idAccesorio = @idAccesorio)
		ORDER BY HP.Fecha DESC
	END
END

------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 27-03-2023
-- Description:	ELIMINAR PROCESOS MTTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_EliminarProcesosMaquinas]
@Opcion INT,
@idProcesoMtto INT,
@MaquinaCodigo VARCHAR(50),
@Kilometraje DECIMAL(10,2),
@Intervalo DECIMAL(10,2)
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- ELIMINAR PROCESO
		DELETE FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMttoMaquinas
		WHERE idProcesoMtto = @idProcesoMtto AND (LTRIM(RTRIM(MaquinaCodigo)) = LTRIM(RTRIM(@MaquinaCodigo)))

		SET @Exito = '0 = Mantenimiento Eliminado Correctamente.'
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR HISTORIAL
		DELETE FROM ReportesApp_Mantenimiento_MttoPreventivo_HistorialProcesoMaquinas
		WHERE idProcesoMtto = @idProcesoMtto AND (LTRIM(RTRIM(MaquinaCodigo)) = LTRIM(RTRIM(@MaquinaCodigo))) AND KMCambio = @Kilometraje AND Intervalo = @Intervalo
		SET @Exito = '0 = Mantenimiento Eliminado Correctamente.'
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

-----------------------------------------------------------------------------
-----------------------------------------------------------------------------

DECLARE @TEMP_KM TABLE(
		Placa VARCHAR(20),
		Odometro DECIMAL(10,2),
		Fecha DATETIME)

INSERT INTO @TEMP_KM(Placa,Odometro,Fecha)
SELECT H.NumeroPlaca, H.Odometro, H.Fecha_Registro
FROM (SELECT H.*, ROW_NUMBER() OVER (PARTITION BY H.NumeroPlaca ORDER BY H.Odometro DESC) AS RN
FROM ReportesApp_Combustible_OdometroUTHistorico H WHERE Odometro != 0 AND MONTH(Fecha_Registro) = MONTH(GETDATE()) AND YEAR(Fecha_Registro) = YEAR(GETDATE())) H
WHERE H.RN = 1
ORDER BY H.NumeroPlaca 

UPDATE KM
SET KM.KMActual = KM.ValorAdicional + UT.Odometro, KM.Fecha = GETDATE()
FROM ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM
LEFT JOIN @TEMP_KM UT ON REPLACE(REPLACE(LTRIM(RTRIM(UT.Placa)),'-',''),'.','') = REPLACE(REPLACE(LTRIM(RTRIM(KM.Placa)),'-',''),'.','')
WHERE REPLACE(REPLACE(LTRIM(RTRIM(UT.Placa)),'-',''),'.','') = REPLACE(REPLACE(LTRIM(RTRIM(KM.Placa)),'-',''),'.','')

INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_KMRegistro (idKilometraje, idTipoVehiculo, Fecha, Kilometraje)
SELECT idKilometraje, idTipoVehiculo, GETDATE(), KMActual FROM ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas WHERE idTipoVehiculo IN (1)

/*
IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_KMRegistro WHERE CONVERT(DATE,Fecha) = CONVERT(DATE,GETDATE()))) BEGIN
	UPDATE KR
	SET KR.Fecha = GETDATE(), KR.Kilometraje = KM.KMActual
	FROM ReportesApp_Mantenimiento_MttoPreventivo_KMRegistro KR
	LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idKilometraje = KR.idKilometraje
	WHERE CONVERT(DATE,KR.Fecha) = CONVERT(DATE,GETDATE())
END
*/

-------------------------------------------------------------------

UPDATE KM 
SET KM.KMActual = ISNULL(BX.TOTAL_KM,1), KM.Fecha = GETDATE()
FROM ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM
LEFT JOIN
(SELECT X.SEMIRREMOLQUE, SUM(X.KM) AS 'TOTAL_KM' FROM
(SELECT DISTINCT V.IdViaje, RO.Descripcion, V.FechaProgramada, VH.NumeroPlaca AS 'PLACA', VC.NumeroPlaca AS 'SEMIRREMOLQUE', C.CPUKilometraje AS 'KM'
		 FROM OP_TR_VIAJE V
		 INNER JOIN OP_TR_CargaCombustible C ON C.IdViaje = V.IdViaje
		 INNER JOIN OP_TR_VEHICULO VH ON VH.IdVehiculo = V.IdVehiculo
		 INNER JOIN OP_TR_VEHICULO VC ON VC.IdVehiculo = V.IdCarreta
		 LEFT JOIN ReportesApp_Operacion_Previaje_Registros RP WITH(NOLOCK) ON CAST(RP.CodViaje AS VARCHAR(12)) = V.Codigo
		 LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones RO WITH(NOLOCK) ON RO.IdOperacion = RP.TipoProgramacion
		 INNER JOIN ReportesApp_Combustible_Nexo_Ticket_CargaCombustible_Kardex K WITH(NOLOCK) ON K.IdViaje = V.IdViaje AND K.IdCarga = C.IdCarga
		 WHERE C.Estado = 2
		 AND V.FechaProgramada BETWEEN '01/01/2000' AND GETDATE()) X
 GROUP BY X.SEMIRREMOLQUE) BX
ON LTRIM(RTRIM(KM.Placa)) = LTRIM(RTRIM(BX.SEMIRREMOLQUE))
WHERE LTRIM(RTRIM(KM.Placa)) = LTRIM(RTRIM(BX.SEMIRREMOLQUE)) AND KM.idTipoVehiculo = 2

INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_KMRegistro (idKilometraje, idTipoVehiculo, Fecha, Kilometraje)
SELECT idKilometraje, idTipoVehiculo, GETDATE(), ISNULL(KMActual,1) FROM ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas
WHERE idTipoVehiculo = 2

/*
UPDATE KM 
SET KM.KMActual = BX.TOTAL_KM
From ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM
LEFT JOIN
(SELECT X.SEMIRREMOLQUE, SUM(X.KM) AS 'TOTAL_KM' FROM
(SELECT DISTINCT COMBUSTIBLE.FechaDespacho AS DESPACHO
		   ,V.FechaProgramada FECHAPROG_VIAJE
		   ,V.CODIGO VIAJE
		   ,CASE WHEN RO.Descripcion IS NULL THEN 'TOLVAS' ELSE RO.Descripcion END  AS 'OPERACION', 
			CASE WHEN v.Estado = 1 THEN 'PENDIENTE'
				 WHEN V.Estado = 2 THEN 'PROGRAMADO'
				 WHEN V.Estado = 3 THEN 'EJECUCION'
				 WHEN V.Estado = 4 THEN 'COMPLETADO'
				 WHEN V.ESTADO = 8 THEN 'CANCELADO'
				 WHEN V.Estado = 9 THEN 'ANULADO' ELSE '' END AS 'ESTADO',
			VEHICULO.NumeroPlaca AS PLACA,
			ME_MAQUINAMARCA.DESCRIPCION AS MARCA,
			carreta.NumeroPlaca as SEMIRREMOLQUE,
			CONDUCTOR.Nombre
			CONDUCTOR,
			OP_TR_Ruta.Descripcion AS RUTA,
				CPUKilometraje AS KM
		    ,T.VueltasViaje Vueltas 
		FROM OP_TR_VIAJE v WITH(NOLOCK) 
			INNER JOIN OP_TR_RUTA WITH(NOLOCK) ON OP_TR_RUTA.IDRUTA = v.IDRUTA 
			INNER JOIN OP_TR_CONDUCTOR AS CONDUCTOR WITH(NOLOCK)ON CONDUCTOR.IDCONDUCTOR = v.IDCONDUCTOR 
			LEFT JOIN OP_TR_CONDUCTOR AS AYUDANTE WITH(NOLOCK)ON AYUDANTE.IDCONDUCTOR = v.IDAYUDANTE 
			INNER JOIN OP_TR_VEHICULO AS VEHICULO WITH(NOLOCK) ON VEHICULO.IDVEHICULO = v.IDVEHICULO 
			INNER JOIN ME_MAQUINAMARCA WITH(NOLOCK) ON ME_MAQUINAMARCA.MARCA = VEHICULO.MARCA
			INNER JOIN GE_VARIOS AS TIPOVEHICULO WITH(NOLOCK)ON TIPOVEHICULO.SECUENCIAL = VEHICULO.TIPOVEHICULO  --LEFT
																		AND TIPOVEHICULO.CODIGOTABLA = 'TIPOVEHICULO' 
			LEFT JOIN OP_TR_VEHICULO AS CARRETA WITH(NOLOCK) ON CARRETA.IDVEHICULO = v.IDCARRETA 
			INNER JOIN OP_GE_OTDETALLE WITH(NOLOCK) ON v.IDVIAJE = OP_GE_OTDETALLE.IDVIAJE
														AND OP_GE_OTDETALLE.TipoMovimiento = 'TRA' 
			INNER JOIN OP_GE_OTPRODUCTO WITH(NOLOCK) ON OP_GE_OTDETALLE.IDOT = OP_GE_OTPRODUCTO.IDOT 
															AND OP_GE_OTDETALLE.LINEAPRODUCTO = OP_GE_OTPRODUCTO.LINEA
															AND OP_GE_OTPRODUCTO.TipoMovimiento = 'TRA' 
			LEFT JOIN OP_GE_OT WITH(NOLOCK) ON OP_GE_OT.IDOT = OP_GE_OTPRODUCTO.IDOT 
												AND OP_GE_OT.TipoMovimiento = 'TRA' 
			INNER JOIN OP_GE_CONTRATODETALLE WITH(NOLOCK) ON OP_GE_CONTRATODETALLE.IDCONTRATO = OP_GE_OTPRODUCTO.IDCONTRATO 
				AND OP_GE_CONTRATODETALLE.GRUPOSERVICIOOP = 'T' AND ((ISNULL(OP_GE_CONTRATODETALLE.INDGENERAL,1) = 1 
				AND OP_GE_CONTRATODETALLE.LINEAPRODUCTO = OP_GE_OTPRODUCTO.LINEACONTRATO) 
				OR ISNULL(OP_GE_CONTRATODETALLE.INDGENERAL,1) = 2) 		
			INNER JOIN OP_TR_CargaCombustible AS COMBUSTIBLE WITH(NOLOCK) ON COMBUSTIBLE.IdViaje = v.IdViaje	  
			LEFT JOIN ReportesApp_Operacion_Previaje_Registros RP WITH(NOLOCK) ON CAST(RP.CodViaje AS VARCHAR(12)) = v.Codigo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones RO WITH(NOLOCK) ON RO.IdOperacion = RP.TipoProgramacion    
			LEFT JOIN ReportesApp_Operacion_GuiasImportadasAltra OG WITH(NOLOCK) ON OG.CodigoViaje = V.Codigo AND (OG.Fecha BETWEEN '01/01/2023' AND GETDATE()) 
		    INNER JOIN (
							SELECT CAST(TK.Ticket AS VARCHAR(20)) Ticket, TK.NroTicketPreViaje, TK.Placa, TK.IdViaje, NT.IdCarga, 
									'PROPIO' AS Grifo,
									CASE WHEN TK.Lugar = 'LIMA' THEN 'SURTIDOR LIMA' 
									ELSE 'SURTIDOR TRUJILLO' END AS Surtidor,
									TK.ReservaControl, TK.UreaControl, TK.VueltasViaje, TK.Fecha, TK.ChoferAsignadoGeotab, NT.FechaDespacho, NT.NumeroDocumento
							FROM ReportesApp_Combustible_Nexo_Ticket_CargaCombustible_Kardex NT WITH(NOLOCK)
								INNER JOIN ReportesApp_Combustible_TicketsSurtidor TK  WITH(NOLOCK) ON TK.Ticket = NT.Ticket
																										--AND TK.NroTicketPreViaje = NT.NroTicketPreViaje
																										AND TK.Placa = NT.Placa
																										AND TK.IDViaje = NT.IDViaje
							WHERE TK.IDViaje IS NOT NULL
								AND NT.IDViaje IS NOT NULL
								AND NT.Tipo = 'P'
								AND (NT.FechaDespacho BETWEEN '01/01/2023' AND GETDATE()) 
								
							UNION
							SELECT CONVERT(VARCHAR(20),RIGHT('000000000' + LTRIM(RTRIM(TT.Ticket)),9)), TT.Codigo NroTicketPreViaje, TT.Placa, TT.IdViaje,  NT.IdCarga, 'TERCERO'  Grifo, TT.Empresa Surtidor,
								TT.ReservaControl, TT.UreaControl, TT.VueltasViaje, TT.FechaDespacho AS Fecha, TT.ChoferAsignadoGeotab, NT.FechaDespacho, NT.NumeroDocumento 
							FROM ReportesApp_Combustible_Nexo_Ticket_CargaCombustible_Kardex NT  WITH(NOLOCK)
								INNER JOIN ReportesApp_Combustible_TicketsTercero TT  WITH(NOLOCK) ON TT.Ticket = NT.Ticket
																									AND TT.Codigo = NT.NroTicketPreViaje
																									AND TT.Placa = NT.Placa
																									AND TT.IDViaje = NT.IDViaje
							WHERE TT.IDViaje IS NOT NULL
								AND NT.IDViaje IS NOT NULL
								AND NT.Tipo = 'T'
								AND (NT.FechaDespacho BETWEEN '01/01/2023' AND GETDATE()) 
						) T ON T.IDViaje = COMBUSTIBLE.IdViaje
							AND T.IdCarga = COMBUSTIBLE.IdCarga
							AND T.NumeroDocumento = COMBUSTIBLE.NumeroDocumentoSalida
		    LEFT JOIN OP_TR_DespachoCombustible DESPACHO ON DESPACHO.IdCarga=T.IdCarga --20/09/2022: Joel: Se agregó nuevo Left para traer ConsumoSW y DIF (TELEMETRIA)
		WHERE v.FechaProgramada BETWEEN '01/01/2023' AND GETDATE()
			AND COMBUSTIBLE.Estado = 2 ) X
			GROUP BY X.SEMIRREMOLQUE) BX
ON LTRIM(RTRIM(KM.Placa)) = LTRIM(RTRIM(BX.SEMIRREMOLQUE))
WHERE LTRIM(RTRIM(KM.Placa)) = LTRIM(RTRIM(BX.SEMIRREMOLQUE))

INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_KMRegistro (idKilometraje, Fecha, Kilometraje)
SELECT idKilometraje, GETDATE(), KMActual FROM ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas
WHERE idTipoVehiculo = 2
*/