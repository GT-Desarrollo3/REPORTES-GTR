
-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_Especialidades

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_Registros

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_RegistrosMaquinas

----------------------------------------------------------------------------------

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

--------------------------------------------------------------------------------------------------

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

		DELETE FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto
		WHERE idProcesoMtto = @idProcesoMtto AND idVehiculo = @idVehiculo
		
		DELETE FROM ReportesApp_Mantenimiento_MttoPreventivo_HistorialProceso
		WHERE idProcesoMtto = @idProcesoMtto AND idVehiculo = @idVehiculo

		DELETE FROM ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_Registros
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

--------------------------------------------------------------------------------------------------

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
		DELETE FROM ReportesApp_Mantenimiento_MttoPreventivo_RecursosMaquina
		WHERE idProcesoMtto = @idProcesoMtto AND (LTRIM(RTRIM(MaquinaCodigo)) = LTRIM(RTRIM(@MaquinaCodigo)))
		
		DELETE FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMttoMaquinas
		WHERE idProcesoMtto = @idProcesoMtto AND (LTRIM(RTRIM(MaquinaCodigo)) = LTRIM(RTRIM(@MaquinaCodigo)))

		DELETE FROM ReportesApp_Mantenimiento_MttoPreventivo_HistorialProcesoMaquinas
		WHERE idProcesoMtto = @idProcesoMtto AND (LTRIM(RTRIM(MaquinaCodigo)) = LTRIM(RTRIM(@MaquinaCodigo)))

		DELETE FROM ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_RegistrosMaquinas
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

--------------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 17/03/2025
-- Description:	INGRESAR RECURSOS DE ACCESORIOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_IngresarEliminarMO]
@Opcion INT,
@idManoObra INT,
@idProcesoMtto INT,
@idVehiculo INT,
@Especialidad VARCHAR(50),
@Tiempo VARCHAR(100),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- CREAR MANO DE OBRA
		SET @correlativo = (SELECT MAX(idManoObra) FROM ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_Registros WHERE idProcesoMtto = @idProcesoMtto AND idVehiculo = @idVehiculo)
		SET @correlativo = ISNULL(@correlativo,0) + 1
		
		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_Registros(idManoObra,idProcesoMtto,idVehiculo,Especialidad,Tiempo,UsuarioCreacion,FechaCreacion)
		VALUES(@correlativo,@idProcesoMtto,@idVehiculo,@Especialidad,@Tiempo,@Usuario,GETDATE())

		SET @Exito = '0 = La mano de obra ha sido asignada correctamente.'
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR MANO DE OBRA
		DELETE FROM ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_Registros
		WHERE idManoObra = @idManoObra AND idProcesoMtto = @idProcesoMtto AND idVehiculo = @idVehiculo

		SET @Exito = '0 = Insumo eliminado.'
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

----------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 17/03/2025
-- Description:	INGRESAR RECURSOS DE ACCESORIOS MÁQUINAS
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_IngresarEliminarMOMaquinas]
@Opcion INT,
@idManoObra INT,
@idProcesoMtto INT,
@MaquinaCodigo VARCHAR(50),
@Especialidad VARCHAR(50),
@Tiempo VARCHAR(100),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- CREAR MANO DE OBRA MÁQUINAS
		SET @correlativo = (SELECT MAX(idManoObra) FROM ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_RegistrosMaquinas
							WHERE idProcesoMtto = @idProcesoMtto AND (LTRIM(RTRIM(MaquinaCodigo)) = LTRIM(RTRIM(@MaquinaCodigo))))
		SET @correlativo = ISNULL(@correlativo,0) + 1
		
		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_RegistrosMaquinas(idManoObra,idProcesoMtto,MaquinaCodigo,Especialidad,Tiempo,UsuarioCreacion,FechaCreacion)
		VALUES(@correlativo,@idProcesoMtto,@MaquinaCodigo,@Especialidad,@Tiempo,@Usuario,GETDATE())

		SET @Exito = '0 = La mano de obra ha sido asignada correctamente.'
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR MANO DE OBRA MÁQUINAS
		DELETE FROM ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_RegistrosMaquinas
		WHERE idManoObra = @idManoObra AND idProcesoMtto = @idProcesoMtto AND (LTRIM(RTRIM(MaquinaCodigo)) = LTRIM(RTRIM(@MaquinaCodigo)))

		SET @Exito = '0 = Insumo eliminado.'
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

----------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 18/03/2025
-- Description:	LISTAR MANO DE OBRA DE PROCESOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ManoObra_ListarManoObra]
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@Placa VARCHAR(20),
@TipoMaquina VARCHAR(50),
@Especialidad VARCHAR(100)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@TipoMaquina = 'TODOS') BEGIN
		IF (@Especialidad = 'TODAS') BEGIN
			(SELECT X.idManoObra, X.idProcesoMtto, X.idVehiculo, X.PLACA, X.TIPO_UNIDAD, X.SUBTIPO_UNIDAD, X.OPERACION, X.ACTIVIDAD, X.ESPECIALIDAD,
			X.TIEMPO_DURACION, X.FECHA_CAMBIO, X.KM_CAMBIO, X.INTERVALO, X.FECHA_ACTUAL, X.KM_ACTUAL, X.TIEMPO_MESES, X.KM_RECORRIDO, X.[KM/DIA], X.KM_FALTANTE, X.[%],
			(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA', X.UsuarioCreacion, X.FechaCreacion
			FROM (SELECT MO.idManoObra, MO.idProcesoMtto, MO.idVehiculo, V.NumeroPlaca AS 'PLACA', ISNULL(TV.Descripcion,'UNIDADES TERCERAS') AS 'TIPO_UNIDAD', 
			SV.Descripcion AS 'SUBTIPO_UNIDAD', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION', AC.Descripcion AS 'ACTIVIDAD',
			MO.Especialidad AS 'ESPECIALIDAD', MO.Tiempo AS 'TIEMPO_DURACION', PM.FechaCambio AS 'FECHA_CAMBIO', PM.KMCambio AS 'KM_CAMBIO', PM.Intervalo AS 'INTERVALO',
			KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL', DATEDIFF(MONTH,PM.FechaCambio,KM.Fecha) AS 'TIEMPO_MESES',
			CASE WHEN PM.KMCambio = 0 THEN 0 ELSE KM.KMActual - PM.KMCambio END AS 'KM_RECORRIDO', CASE WHEN DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) = 0 THEN 1 ELSE
			CAST((KM.KMActual - PM.KMCambio) / DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA', PM.Intervalo - (KM.KMActual - PM.KMCambio) AS 'KM_FALTANTE', 
			CASE WHEN PM.Intervalo = 0 THEN 0 ELSE CAST((1-(PM.Intervalo - (KM.KMActual - PM.KMCambio)) / PM.Intervalo) * 100 AS DECIMAL(10,2)) END AS '%',
			MO.UsuarioCreacion, MO.FechaCreacion
			FROM ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_Registros MO
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto PM ON PM.idProcesoMtto = MO.idProcesoMtto AND PM.idVehiculo = MO.idVehiculo
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = PM.idVehiculo
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Accesorios AC ON AC.idAccesorio = PM.idAccesorio
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = MO.idVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON UC.IdProgramacion = O.IdOperacion
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
			LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TipoVehiculo
			LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
			WHERE V.Estado = 2) X
			WHERE ((CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) BETWEEN @FINICIO AND @FFIN)
			AND (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%'))
			UNION
			(SELECT X.idManoObra, X.idProcesoMtto, 0 AS 'idVehiculo', X.PLACA, X.TIPO_UNIDAD, X.SUBTIPO_UNIDAD, X.OPERACION, X.ACTIVIDAD, X.ESPECIALIDAD,
			X.TIEMPO_DURACION, X.FECHA_CAMBIO, X.KM_CAMBIO, X.INTERVALO, X.FECHA_ACTUAL, X.KM_ACTUAL, X.TIEMPO_MESES, X.KM_RECORRIDO, X.[KM/DIA], X.KM_FALTANTE, X.[%],
			(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA', X.UsuarioCreacion, X.FechaCreacion
			FROM (SELECT MO.idManoObra, MO.idProcesoMtto, MO.MaquinaCodigo AS 'PLACA', LTRIM(RTRIM(ISNULL(T.DescripcionLocal,'UNIDADES TERCERAS'))) AS 'TIPO_UNIDAD', 
			LTRIM(RTRIM(TG.DescripcionLocal)) AS 'SUBTIPO_UNIDAD', 'SIN OPERACION' AS 'OPERACION', AC.Descripcion AS 'ACTIVIDAD', MO.Especialidad AS 'ESPECIALIDAD', MO.Tiempo AS 'TIEMPO_DURACION',
			PM.FechaCambio AS 'FECHA_CAMBIO', PM.KMCambio AS 'KM_CAMBIO', PM.Intervalo AS 'INTERVALO', KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL',
			DATEDIFF(MONTH,PM.FechaCambio,KM.Fecha) AS 'TIEMPO_MESES', CASE WHEN PM.KMCambio = 0 THEN 0 ELSE KM.KMActual - PM.KMCambio END AS 'KM_RECORRIDO', CASE WHEN DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) = 0
			THEN 1 ELSE CAST((KM.KMActual - PM.KMCambio) / DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA', PM.Intervalo - (KM.KMActual - PM.KMCambio) AS 'KM_FALTANTE', 
			CASE WHEN PM.Intervalo = 0 THEN 0 ELSE CAST((1-(PM.Intervalo - (KM.KMActual - PM.KMCambio)) / PM.Intervalo) * 100 AS DECIMAL(10,2)) END AS '%',
			MO.UsuarioCreacion, MO.FechaCreacion
			FROM ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_RegistrosMaquinas MO
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMttoMaquinas PM ON PM.idProcesoMtto = MO.idProcesoMtto AND PM.MaquinaCodigo = MO.MaquinaCodigo
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(PM.MaquinaCodigo)) AND (M.Estado = 'A')
			LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
			LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Accesorios AC ON AC.idAccesorio = PM.idAccesorio
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(PM.MaquinaCodigo))) X
			WHERE ((CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) BETWEEN @FINICIO AND @FFIN)
			AND (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%'))
			ORDER BY X.PLACA, X.idProcesoMtto ASC
		END
		ELSE BEGIN
			(SELECT X.idManoObra, X.idProcesoMtto, X.idVehiculo, X.PLACA, X.TIPO_UNIDAD, X.SUBTIPO_UNIDAD, X.OPERACION, X.ACTIVIDAD, X.ESPECIALIDAD,
			X.TIEMPO_DURACION, X.FECHA_CAMBIO, X.KM_CAMBIO, X.INTERVALO, X.FECHA_ACTUAL, X.KM_ACTUAL, X.TIEMPO_MESES, X.KM_RECORRIDO, X.[KM/DIA], X.KM_FALTANTE, X.[%],
			(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA', X.UsuarioCreacion, X.FechaCreacion
			FROM (SELECT MO.idManoObra, MO.idProcesoMtto, MO.idVehiculo, V.NumeroPlaca AS 'PLACA', ISNULL(TV.Descripcion,'UNIDADES TERCERAS') AS 'TIPO_UNIDAD', 
			SV.Descripcion AS 'SUBTIPO_UNIDAD', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION', AC.Descripcion AS 'ACTIVIDAD',
			MO.Especialidad AS 'ESPECIALIDAD', MO.Tiempo AS 'TIEMPO_DURACION', PM.FechaCambio AS 'FECHA_CAMBIO', PM.KMCambio AS 'KM_CAMBIO', PM.Intervalo AS 'INTERVALO',
			KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL', DATEDIFF(MONTH,PM.FechaCambio,KM.Fecha) AS 'TIEMPO_MESES',
			CASE WHEN PM.KMCambio = 0 THEN 0 ELSE KM.KMActual - PM.KMCambio END AS 'KM_RECORRIDO', CASE WHEN DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) = 0 THEN 1 ELSE
			CAST((KM.KMActual - PM.KMCambio) / DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA', PM.Intervalo - (KM.KMActual - PM.KMCambio) AS 'KM_FALTANTE', 
			CASE WHEN PM.Intervalo = 0 THEN 0 ELSE CAST((1-(PM.Intervalo - (KM.KMActual - PM.KMCambio)) / PM.Intervalo) * 100 AS DECIMAL(10,2)) END AS '%',
			MO.UsuarioCreacion, MO.FechaCreacion
			FROM ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_Registros MO
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto PM ON PM.idProcesoMtto = MO.idProcesoMtto AND PM.idVehiculo = MO.idVehiculo
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = PM.idVehiculo
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Accesorios AC ON AC.idAccesorio = PM.idAccesorio
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = MO.idVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON UC.IdProgramacion = O.IdOperacion
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
			LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TipoVehiculo
			LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
			WHERE V.Estado = 2) X
			WHERE ((CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) BETWEEN @FINICIO AND @FFIN)
			AND (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND (X.ESPECIALIDAD = @Especialidad))
			UNION
			(SELECT X.idManoObra, X.idProcesoMtto, 0 AS 'idVehiculo', X.PLACA, X.TIPO_UNIDAD, X.SUBTIPO_UNIDAD, X.OPERACION, X.ACTIVIDAD, X.ESPECIALIDAD,
			X.TIEMPO_DURACION, X.FECHA_CAMBIO, X.KM_CAMBIO, X.INTERVALO, X.FECHA_ACTUAL, X.KM_ACTUAL, X.TIEMPO_MESES, X.KM_RECORRIDO, X.[KM/DIA], X.KM_FALTANTE, X.[%],
			(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA', X.UsuarioCreacion, X.FechaCreacion
			FROM (SELECT MO.idManoObra, MO.idProcesoMtto, MO.MaquinaCodigo AS 'PLACA', LTRIM(RTRIM(ISNULL(T.DescripcionLocal,'UNIDADES TERCERAS'))) AS 'TIPO_UNIDAD', 
			LTRIM(RTRIM(TG.DescripcionLocal)) AS 'SUBTIPO_UNIDAD', 'SIN OPERACION' AS 'OPERACION', AC.Descripcion AS 'ACTIVIDAD', MO.Especialidad AS 'ESPECIALIDAD', MO.Tiempo AS 'TIEMPO_DURACION',
			PM.FechaCambio AS 'FECHA_CAMBIO', PM.KMCambio AS 'KM_CAMBIO', PM.Intervalo AS 'INTERVALO', KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL',
			DATEDIFF(MONTH,PM.FechaCambio,KM.Fecha) AS 'TIEMPO_MESES', CASE WHEN PM.KMCambio = 0 THEN 0 ELSE KM.KMActual - PM.KMCambio END AS 'KM_RECORRIDO', CASE WHEN DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) = 0
			THEN 1 ELSE CAST((KM.KMActual - PM.KMCambio) / DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA', PM.Intervalo - (KM.KMActual - PM.KMCambio) AS 'KM_FALTANTE', 
			CASE WHEN PM.Intervalo = 0 THEN 0 ELSE CAST((1-(PM.Intervalo - (KM.KMActual - PM.KMCambio)) / PM.Intervalo) * 100 AS DECIMAL(10,2)) END AS '%',
			MO.UsuarioCreacion, MO.FechaCreacion
			FROM ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_RegistrosMaquinas MO
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMttoMaquinas PM ON PM.idProcesoMtto = MO.idProcesoMtto AND PM.MaquinaCodigo = MO.MaquinaCodigo
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(PM.MaquinaCodigo)) AND (M.Estado = 'A')
			LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
			LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Accesorios AC ON AC.idAccesorio = PM.idAccesorio
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(PM.MaquinaCodigo))) X
			WHERE ((CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) BETWEEN @FINICIO AND @FFIN)
			AND (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND (X.ESPECIALIDAD = @Especialidad))
			ORDER BY X.PLACA, X.idProcesoMtto ASC
		END
	END
	ELSE BEGIN
		IF (@Especialidad = 'TODAS') BEGIN
			(SELECT X.idManoObra, X.idProcesoMtto, X.idVehiculo, X.PLACA, X.TIPO_UNIDAD, X.SUBTIPO_UNIDAD, X.OPERACION, X.ACTIVIDAD, X.ESPECIALIDAD,
			X.TIEMPO_DURACION, X.FECHA_CAMBIO, X.KM_CAMBIO, X.INTERVALO, X.FECHA_ACTUAL, X.KM_ACTUAL, X.TIEMPO_MESES, X.KM_RECORRIDO, X.[KM/DIA], X.KM_FALTANTE, X.[%],
			(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA', X.UsuarioCreacion, X.FechaCreacion
			FROM (SELECT MO.idManoObra, MO.idProcesoMtto, MO.idVehiculo, V.NumeroPlaca AS 'PLACA', ISNULL(TV.Descripcion,'UNIDADES TERCERAS') AS 'TIPO_UNIDAD', 
			SV.Descripcion AS 'SUBTIPO_UNIDAD', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION', AC.Descripcion AS 'ACTIVIDAD',
			MO.Especialidad AS 'ESPECIALIDAD', MO.Tiempo AS 'TIEMPO_DURACION', PM.FechaCambio AS 'FECHA_CAMBIO', PM.KMCambio AS 'KM_CAMBIO', PM.Intervalo AS 'INTERVALO',
			KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL', DATEDIFF(MONTH,PM.FechaCambio,KM.Fecha) AS 'TIEMPO_MESES',
			CASE WHEN PM.KMCambio = 0 THEN 0 ELSE KM.KMActual - PM.KMCambio END AS 'KM_RECORRIDO', CASE WHEN DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) = 0 THEN 1 ELSE
			CAST((KM.KMActual - PM.KMCambio) / DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA', PM.Intervalo - (KM.KMActual - PM.KMCambio) AS 'KM_FALTANTE', 
			CASE WHEN PM.Intervalo = 0 THEN 0 ELSE CAST((1-(PM.Intervalo - (KM.KMActual - PM.KMCambio)) / PM.Intervalo) * 100 AS DECIMAL(10,2)) END AS '%',
			MO.UsuarioCreacion, MO.FechaCreacion
			FROM ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_Registros MO
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto PM ON PM.idProcesoMtto = MO.idProcesoMtto AND PM.idVehiculo = MO.idVehiculo
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = PM.idVehiculo
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Accesorios AC ON AC.idAccesorio = PM.idAccesorio
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = MO.idVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON UC.IdProgramacion = O.IdOperacion
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
			LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TipoVehiculo
			LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
			WHERE V.Estado = 2) X
			WHERE ((CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) BETWEEN @FINICIO AND @FFIN)
			AND (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND (X.TIPO_UNIDAD = @TipoMaquina))
			UNION
			(SELECT X.idManoObra, X.idProcesoMtto, 0 AS 'idVehiculo', X.PLACA, X.TIPO_UNIDAD, X.SUBTIPO_UNIDAD, X.OPERACION, X.ACTIVIDAD, X.ESPECIALIDAD,
			X.TIEMPO_DURACION, X.FECHA_CAMBIO, X.KM_CAMBIO, X.INTERVALO, X.FECHA_ACTUAL, X.KM_ACTUAL, X.TIEMPO_MESES, X.KM_RECORRIDO, X.[KM/DIA], X.KM_FALTANTE, X.[%],
			(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA', X.UsuarioCreacion, X.FechaCreacion
			FROM (SELECT MO.idManoObra, MO.idProcesoMtto, MO.MaquinaCodigo AS 'PLACA', LTRIM(RTRIM(ISNULL(T.DescripcionLocal,'UNIDADES TERCERAS'))) AS 'TIPO_UNIDAD', 
			LTRIM(RTRIM(TG.DescripcionLocal)) AS 'SUBTIPO_UNIDAD', 'SIN OPERACION' AS 'OPERACION', AC.Descripcion AS 'ACTIVIDAD', MO.Especialidad AS 'ESPECIALIDAD', MO.Tiempo AS 'TIEMPO_DURACION',
			PM.FechaCambio AS 'FECHA_CAMBIO', PM.KMCambio AS 'KM_CAMBIO', PM.Intervalo AS 'INTERVALO', KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL',
			DATEDIFF(MONTH,PM.FechaCambio,KM.Fecha) AS 'TIEMPO_MESES', CASE WHEN PM.KMCambio = 0 THEN 0 ELSE KM.KMActual - PM.KMCambio END AS 'KM_RECORRIDO', CASE WHEN DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) = 0
			THEN 1 ELSE CAST((KM.KMActual - PM.KMCambio) / DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA', PM.Intervalo - (KM.KMActual - PM.KMCambio) AS 'KM_FALTANTE', 
			CASE WHEN PM.Intervalo = 0 THEN 0 ELSE CAST((1-(PM.Intervalo - (KM.KMActual - PM.KMCambio)) / PM.Intervalo) * 100 AS DECIMAL(10,2)) END AS '%',
			MO.UsuarioCreacion, MO.FechaCreacion
			FROM ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_RegistrosMaquinas MO
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMttoMaquinas PM ON PM.idProcesoMtto = MO.idProcesoMtto AND PM.MaquinaCodigo = MO.MaquinaCodigo
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(PM.MaquinaCodigo)) AND (M.Estado = 'A')
			LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
			LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Accesorios AC ON AC.idAccesorio = PM.idAccesorio
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(PM.MaquinaCodigo))) X
			WHERE ((CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) BETWEEN @FINICIO AND @FFIN)
			AND (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND (X.TIPO_UNIDAD = @TipoMaquina))
			ORDER BY X.PLACA, X.idProcesoMtto ASC
		END
		ELSE BEGIN
			(SELECT X.idManoObra, X.idProcesoMtto, X.idVehiculo, X.PLACA, X.TIPO_UNIDAD, X.SUBTIPO_UNIDAD, X.OPERACION, X.ACTIVIDAD, X.ESPECIALIDAD,
			X.TIEMPO_DURACION, X.FECHA_CAMBIO, X.KM_CAMBIO, X.INTERVALO, X.FECHA_ACTUAL, X.KM_ACTUAL, X.TIEMPO_MESES, X.KM_RECORRIDO, X.[KM/DIA], X.KM_FALTANTE, X.[%],
			(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA', X.UsuarioCreacion, X.FechaCreacion
			FROM (SELECT MO.idManoObra, MO.idProcesoMtto, MO.idVehiculo, V.NumeroPlaca AS 'PLACA', ISNULL(TV.Descripcion,'UNIDADES TERCERAS') AS 'TIPO_UNIDAD', 
			SV.Descripcion AS 'SUBTIPO_UNIDAD', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION', AC.Descripcion AS 'ACTIVIDAD',
			MO.Especialidad AS 'ESPECIALIDAD', MO.Tiempo AS 'TIEMPO_DURACION', PM.FechaCambio AS 'FECHA_CAMBIO', PM.KMCambio AS 'KM_CAMBIO', PM.Intervalo AS 'INTERVALO',
			KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL', DATEDIFF(MONTH,PM.FechaCambio,KM.Fecha) AS 'TIEMPO_MESES',
			CASE WHEN PM.KMCambio = 0 THEN 0 ELSE KM.KMActual - PM.KMCambio END AS 'KM_RECORRIDO', CASE WHEN DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) = 0 THEN 1 ELSE
			CAST((KM.KMActual - PM.KMCambio) / DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA', PM.Intervalo - (KM.KMActual - PM.KMCambio) AS 'KM_FALTANTE', 
			CASE WHEN PM.Intervalo = 0 THEN 0 ELSE CAST((1-(PM.Intervalo - (KM.KMActual - PM.KMCambio)) / PM.Intervalo) * 100 AS DECIMAL(10,2)) END AS '%',
			MO.UsuarioCreacion, MO.FechaCreacion
			FROM ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_Registros MO
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto PM ON PM.idProcesoMtto = MO.idProcesoMtto AND PM.idVehiculo = MO.idVehiculo
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = PM.idVehiculo
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Accesorios AC ON AC.idAccesorio = PM.idAccesorio
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = MO.idVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON UC.IdProgramacion = O.IdOperacion
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
			LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TipoVehiculo
			LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
			WHERE V.Estado = 2) X
			WHERE ((CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) BETWEEN @FINICIO AND @FFIN)
			AND (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND (X.TIPO_UNIDAD = @TipoMaquina) AND (X.ESPECIALIDAD = @Especialidad))
			UNION
			(SELECT X.idManoObra, X.idProcesoMtto, 0 AS 'idVehiculo', X.PLACA, X.TIPO_UNIDAD, X.SUBTIPO_UNIDAD, X.OPERACION, X.ACTIVIDAD, X.ESPECIALIDAD,
			X.TIEMPO_DURACION, X.FECHA_CAMBIO, X.KM_CAMBIO, X.INTERVALO, X.FECHA_ACTUAL, X.KM_ACTUAL, X.TIEMPO_MESES, X.KM_RECORRIDO, X.[KM/DIA], X.KM_FALTANTE, X.[%],
			(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA', X.UsuarioCreacion, X.FechaCreacion
			FROM (SELECT MO.idManoObra, MO.idProcesoMtto, MO.MaquinaCodigo AS 'PLACA', LTRIM(RTRIM(ISNULL(T.DescripcionLocal,'UNIDADES TERCERAS'))) AS 'TIPO_UNIDAD', 
			LTRIM(RTRIM(TG.DescripcionLocal)) AS 'SUBTIPO_UNIDAD', 'SIN OPERACION' AS 'OPERACION', AC.Descripcion AS 'ACTIVIDAD', MO.Especialidad AS 'ESPECIALIDAD', MO.Tiempo AS 'TIEMPO_DURACION',
			PM.FechaCambio AS 'FECHA_CAMBIO', PM.KMCambio AS 'KM_CAMBIO', PM.Intervalo AS 'INTERVALO', KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL',
			DATEDIFF(MONTH,PM.FechaCambio,KM.Fecha) AS 'TIEMPO_MESES', CASE WHEN PM.KMCambio = 0 THEN 0 ELSE KM.KMActual - PM.KMCambio END AS 'KM_RECORRIDO', CASE WHEN DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) = 0
			THEN 1 ELSE CAST((KM.KMActual - PM.KMCambio) / DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA', PM.Intervalo - (KM.KMActual - PM.KMCambio) AS 'KM_FALTANTE', 
			CASE WHEN PM.Intervalo = 0 THEN 0 ELSE CAST((1-(PM.Intervalo - (KM.KMActual - PM.KMCambio)) / PM.Intervalo) * 100 AS DECIMAL(10,2)) END AS '%',
			MO.UsuarioCreacion, MO.FechaCreacion
			FROM ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_RegistrosMaquinas MO
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMttoMaquinas PM ON PM.idProcesoMtto = MO.idProcesoMtto AND PM.MaquinaCodigo = MO.MaquinaCodigo
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(PM.MaquinaCodigo)) AND (M.Estado = 'A')
			LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
			LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Accesorios AC ON AC.idAccesorio = PM.idAccesorio
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(PM.MaquinaCodigo))) X
			WHERE ((CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) BETWEEN @FINICIO AND @FFIN)
			AND (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND (X.TIPO_UNIDAD = @TipoMaquina) AND (X.ESPECIALIDAD = @Especialidad))
			ORDER BY X.PLACA, X.idProcesoMtto ASC
		END
	END
END

-------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 19-03-2025
-- Description:	LISTAR RESUMEN DE MANO DE OBRA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_ListarResumen]
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@Especialidad VARCHAR(300)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	DECLARE @T_MANO_OBRA TABLE(Especialidad VARCHAR(300), Tiempo VARCHAR(100), FechaProyectada DATE)

	IF (@Especialidad = 'TODAS') BEGIN
		INSERT INTO @T_MANO_OBRA(Especialidad,Tiempo,FechaProyectada)
		(SELECT X.ESPECIALIDAD, X.TIEMPO_DURACION, (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA'
		FROM (SELECT MO.Especialidad AS 'ESPECIALIDAD', MO.Tiempo AS 'TIEMPO_DURACION', CASE WHEN DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) = 0 THEN 1 ELSE
		CAST((KM.KMActual - PM.KMCambio) / DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA', PM.Intervalo - (KM.KMActual - PM.KMCambio) AS 'KM_FALTANTE',
		KM.Fecha AS 'FECHA_ACTUAL' FROM ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_Registros MO
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto PM ON PM.idProcesoMtto = MO.idProcesoMtto AND PM.idVehiculo = MO.idVehiculo
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = PM.idVehiculo
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Accesorios AC ON AC.idAccesorio = PM.idAccesorio
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = MO.idVehiculo
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON UC.IdProgramacion = O.IdOperacion
		LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
		LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TipoVehiculo
		LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo) X
		WHERE ((CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) BETWEEN @FINICIO AND @FFIN))
		UNION
		(SELECT X.ESPECIALIDAD, X.TIEMPO_DURACION, (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA'
		FROM (SELECT MO.Especialidad AS 'ESPECIALIDAD', MO.Tiempo AS 'TIEMPO_DURACION', CASE WHEN DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) = 0 THEN 1 ELSE
		CAST((KM.KMActual - PM.KMCambio) / DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA', PM.Intervalo - (KM.KMActual - PM.KMCambio) AS 'KM_FALTANTE', 
		KM.Fecha AS 'FECHA_ACTUAL' FROM ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_RegistrosMaquinas MO
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMttoMaquinas PM ON PM.idProcesoMtto = MO.idProcesoMtto AND PM.MaquinaCodigo = MO.MaquinaCodigo
		LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(PM.MaquinaCodigo)) AND (M.Estado = 'A')
		LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
		LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Accesorios AC ON AC.idAccesorio = PM.idAccesorio
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(PM.MaquinaCodigo))) X
		WHERE ((CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) BETWEEN @FINICIO AND @FFIN))
		ORDER BY X.ESPECIALIDAD
	END
	ELSE BEGIN
		INSERT INTO @T_MANO_OBRA(Especialidad,Tiempo,FechaProyectada)
		(SELECT X.ESPECIALIDAD, X.TIEMPO_DURACION, (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA'
		FROM (SELECT MO.Especialidad AS 'ESPECIALIDAD', MO.Tiempo AS 'TIEMPO_DURACION', CASE WHEN DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) = 0 THEN 1 ELSE
		CAST((KM.KMActual - PM.KMCambio) / DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA', PM.Intervalo - (KM.KMActual - PM.KMCambio) AS 'KM_FALTANTE',
		KM.Fecha AS 'FECHA_ACTUAL' FROM ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_Registros MO
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto PM ON PM.idProcesoMtto = MO.idProcesoMtto AND PM.idVehiculo = MO.idVehiculo
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = PM.idVehiculo
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Accesorios AC ON AC.idAccesorio = PM.idAccesorio
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = MO.idVehiculo
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON UC.IdProgramacion = O.IdOperacion
		LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
		LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TipoVehiculo
		LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo) X
		WHERE ((CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) BETWEEN @FINICIO AND @FFIN)
		AND (X.ESPECIALIDAD = @Especialidad))
		UNION
		(SELECT X.ESPECIALIDAD, X.TIEMPO_DURACION, (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA'
		FROM (SELECT MO.Especialidad AS 'ESPECIALIDAD', MO.Tiempo AS 'TIEMPO_DURACION', CASE WHEN DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) = 0 THEN 1 ELSE
		CAST((KM.KMActual - PM.KMCambio) / DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA', PM.Intervalo - (KM.KMActual - PM.KMCambio) AS 'KM_FALTANTE', 
		KM.Fecha AS 'FECHA_ACTUAL' FROM ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_RegistrosMaquinas MO
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMttoMaquinas PM ON PM.idProcesoMtto = MO.idProcesoMtto AND PM.MaquinaCodigo = MO.MaquinaCodigo
		LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(PM.MaquinaCodigo)) AND (M.Estado = 'A')
		LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
		LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Accesorios AC ON AC.idAccesorio = PM.idAccesorio
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(PM.MaquinaCodigo))) X
		WHERE ((CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) BETWEEN @FINICIO AND @FFIN)
		AND (X.ESPECIALIDAD = @Especialidad))
		ORDER BY X.ESPECIALIDAD
	END
	
	SELECT Especialidad AS 'ESPECIALIDAD',
	CONVERT(VARCHAR,CAST(DATEADD(SECOND,SUM(DATEDIFF(SECOND,0,cast(Tiempo AS DATETIME))), 0) AS DATETIME),8) AS 'TIEMPO_DURACION'
	FROM @T_MANO_OBRA
	GROUP BY Especialidad
	ORDER BY Especialidad ASC
END



SELECT * FROM Usuario
WHERE Usuario = 'MARANDAR'