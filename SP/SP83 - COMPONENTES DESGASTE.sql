
-- CREAR TABLA ReportesApp_Mantenimiento_CompDesgaste_Componentes Y LLENARLA

-- CREAR TABLA ReportesApp_Mantenimiento_CompDesgaste_RegistroTracto

-- CREAR TABLA ReportesApp_Mantenimiento_CompDesgaste_RegistroCarreta

-------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 04-09-2024
-- Description:	CREAR NUEVO COMPONENTE
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_CompDesgaste_CrearComponente]
@idPlaca INT,
@idComponente INT,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

BEGIN TRAN
BEGIN TRY
	DECLARE @TipoUnidad INT = (SELECT TipoVehiculo FROM OP_TR_Vehiculo WHERE IdVehiculo = @idPlaca)

	IF (@TipoUnidad = 2) BEGIN		-- CARRETA
		IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_CompDesgaste_RegistroCarreta WHERE idPlaca = @idPlaca)) BEGIN
			SET @Exito = '-1 = Ya existe un reporte para esta unidad. Para actualizar información, modifique desde la tabla.'
			ROLLBACK
			GOTO Terminar
		END

		SET @correlativo = (SELECT MAX(idCompDesgasteC) FROM ReportesApp_Mantenimiento_CompDesgaste_RegistroCarreta)
		SET @correlativo = ISNULL(@correlativo,0) + 1 

		INSERT INTO ReportesApp_Mantenimiento_CompDesgaste_RegistroCarreta(idCompDesgasteC,idPlaca,idComponente,FechaAnterior,Milimetro,Estado,UsuarioCrea,FechaCrea)
		VALUES(@correlativo,@idPlaca,@idComponente,DATEFROMPARTS(YEAR(GETDATE()), 1, 1),1,1,@Usuario,GETDATE())
	END

	IF (@TipoUnidad = 1) BEGIN		-- TRACTO
		IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_CompDesgaste_RegistroTracto WHERE idPlaca = @idPlaca)) BEGIN
			SET @Exito = '-1 = Ya existe un reporte para esta unidad. Para actualizar información, modifique desde la tabla.'
			ROLLBACK
			GOTO Terminar
		END
		
		SET @correlativo = (SELECT MAX(idCompDesgasteT) FROM ReportesApp_Mantenimiento_CompDesgaste_RegistroTracto)
		SET @correlativo = ISNULL(@correlativo,0) + 1 

		INSERT INTO ReportesApp_Mantenimiento_CompDesgaste_RegistroTracto(idCompDesgasteT,idPlaca,idComponente,FechaAnterior,Actividad,Nro,Estado,UsuarioCrea,FechaCrea)
		VALUES(@correlativo,@idPlaca,@idComponente,DATEFROMPARTS(YEAR(GETDATE()), 1, 1),'REGULACION',1,1,@Usuario,GETDATE())
	END

	SET @Exito = '0 = Reporte creado correctamente.'
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
-- Create date: 04-09-2024
-- Description:	LISTAR COMPONENTES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_CompDesgaste_ListarComponentes]
@Opcion INT,
@Placa VARCHAR(50),
@Operacion VARCHAR(100),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@Opcion = 1) BEGIN		-- LISTAR REGISTROS
		-- LISTAR KINPIN
		IF (@Operacion = 'TODO') BEGIN
			SELECT RC.idCompDesgasteC, RC.idPlaca, V.NumeroPlaca AS 'PLACA', STV.Descripcion AS 'TIPO', ISNULL(OP.Descripcion,'SIN PROGRAMACION') AS 'OPERACION',
			RC.idComponente, C.Descripcion AS 'COMPONENTE', RC.FechaAnterior AS 'FECHA_ANTERIOR', RC.Milimetro AS 'MM',
			CASE WHEN RC.Milimetro >= 50 AND RC.Milimetro <= 52 THEN 'NORMAL' WHEN RC.Milimetro >= 49 AND RC.Milimetro <= 49.9 THEN 'PRECAUCIÓN'
			WHEN RC.Milimetro >= 45 AND RC.Milimetro <= 48.9 THEN 'CRÍTICO' END AS 'STATUS', RC.FechaCrea AS 'FECHA', RC.UsuarioCrea AS 'USUARIO'
			FROM ReportesApp_Mantenimiento_CompDesgaste_RegistroCarreta RC
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = RC.idPlaca
			LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_SubTipoVehiculo STV WITH(NOLOCK) ON STV.idSubTipoVehiculo = V.SubTipoVehiculo
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC WITH(NOLOCK) ON V.IdVehiculo = UC.IdUnidad
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = UC.IdProgramacion
			LEFT JOIN ReportesApp_Mantenimiento_CompDesgaste_Componentes C ON C.idComponente = RC.idComponente
			WHERE (RC.idComponente = 1) AND (RC.Estado = 1) AND (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND
			(RC.FechaCrea BETWEEN @FINICIO AND @FFIN)
			ORDER BY V.NumeroPlaca DESC, RC.idCompDesgasteC DESC

			SELECT RT.idCompDesgasteT, RT.idPlaca, V.NumeroPlaca AS 'PLACA', STV.Descripcion AS 'TIPO', ISNULL(OP.Descripcion,'SIN PROGRAMACION') AS 'OPERACION',
			RT.idComponente, C.Descripcion AS 'COMPONENTE', RT.FechaAnterior AS 'FECHA_ANTERIOR', RT.Actividad AS 'ACTIVIDAD', RT.Nro AS 'NRO',
			CASE WHEN RT.Nro >= 0 AND RT.Nro <= 5 THEN 'NORMAL' WHEN RT.Nro >= 6 AND RT.Nro <= 7 THEN 'PRECAUCIÓN'
			WHEN RT.Nro >= 8 THEN 'CRÍTICO' END AS 'PROGRAMACION', RT.FechaCrea AS 'FECHA', RT.UsuarioCrea AS 'USUARIO'
			FROM ReportesApp_Mantenimiento_CompDesgaste_RegistroTracto RT
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = RT.idPlaca
			LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_SubTipoVehiculo STV WITH(NOLOCK) ON STV.idSubTipoVehiculo = V.SubTipoVehiculo
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC WITH(NOLOCK) ON V.IdVehiculo = UC.IdUnidad
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = UC.IdProgramacion
			LEFT JOIN ReportesApp_Mantenimiento_CompDesgaste_Componentes C ON C.idComponente = RT.idComponente
			WHERE (RT.idComponente = 2) AND (RT.Estado = 1) AND (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND
			(RT.FechaCrea BETWEEN @FINICIO AND @FFIN)
			ORDER BY V.NumeroPlaca DESC, RT.idCompDesgasteT DESC
		END
		ELSE BEGIN
			SELECT RC.idCompDesgasteC, RC.idPlaca, V.NumeroPlaca AS 'PLACA', STV.Descripcion AS 'TIPO', ISNULL(OP.Descripcion,'SIN PROGRAMACION') AS 'OPERACION',
			RC.idComponente, C.Descripcion AS 'COMPONENTE', RC.FechaAnterior AS 'FECHA_ANTERIOR', RC.Milimetro AS 'MM',
			CASE WHEN RC.Milimetro >= 50 AND RC.Milimetro <= 52 THEN 'NORMAL' WHEN RC.Milimetro >= 49 AND RC.Milimetro <= 49.9 THEN 'PRECAUCIÓN'
			WHEN RC.Milimetro >= 45 AND RC.Milimetro <= 48.9 THEN 'CRÍTICO' END AS 'STATUS', RC.FechaCrea AS 'FECHA', RC.UsuarioCrea AS 'USUARIO'
			FROM ReportesApp_Mantenimiento_CompDesgaste_RegistroCarreta RC
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = RC.idPlaca
			LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_SubTipoVehiculo STV WITH(NOLOCK) ON STV.idSubTipoVehiculo = V.SubTipoVehiculo
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC WITH(NOLOCK) ON V.IdVehiculo = UC.IdUnidad
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = UC.IdProgramacion
			LEFT JOIN ReportesApp_Mantenimiento_CompDesgaste_Componentes C ON C.idComponente = RC.idComponente
			WHERE (RC.idComponente = 1) AND (RC.Estado = 1) AND (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND
			(OP.Descripcion IS NULL OR OP.Descripcion LIKE '%' + @Operacion + '%') AND (RC.FechaCrea BETWEEN @FINICIO AND @FFIN)
			ORDER BY V.NumeroPlaca DESC, RC.idCompDesgasteC DESC

			SELECT RT.idCompDesgasteT, RT.idPlaca, V.NumeroPlaca AS 'PLACA', STV.Descripcion AS 'TIPO', ISNULL(OP.Descripcion,'SIN PROGRAMACION') AS 'OPERACION',
			RT.idComponente, C.Descripcion AS 'COMPONENTE', RT.FechaAnterior AS 'FECHA_ANTERIOR', RT.Actividad AS 'ACTIVIDAD', RT.Nro AS 'NRO',
			CASE WHEN RT.Nro >= 0 AND RT.Nro <= 5 THEN 'NORMAL' WHEN RT.Nro >= 6 AND RT.Nro <= 7 THEN 'PRECAUCIÓN'
			WHEN RT.Nro >= 8 THEN 'CRÍTICO' END AS 'PROGRAMACION', RT.FechaCrea AS 'FECHA', RT.UsuarioCrea AS 'USUARIO'
			FROM ReportesApp_Mantenimiento_CompDesgaste_RegistroTracto RT
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = RT.idPlaca
			LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_SubTipoVehiculo STV WITH(NOLOCK) ON STV.idSubTipoVehiculo = V.SubTipoVehiculo
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC WITH(NOLOCK) ON V.IdVehiculo = UC.IdUnidad
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = UC.IdProgramacion
			LEFT JOIN ReportesApp_Mantenimiento_CompDesgaste_Componentes C ON C.idComponente = RT.idComponente
			WHERE (RT.idComponente = 2) AND (RT.Estado = 1) AND (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND
			(OP.Descripcion IS NULL OR OP.Descripcion LIKE '%' + @Operacion + '%') AND (RT.FechaCrea BETWEEN @FINICIO AND @FFIN)
			ORDER BY V.NumeroPlaca DESC, RT.idCompDesgasteT DESC
		END
	END
	ELSE BEGIN		-- LISTAR HISTORIAL
		-- LISTAR KINPIN
		IF (@Operacion = 'TODO') BEGIN
			SELECT RC.idCompDesgasteC, RC.idPlaca, V.NumeroPlaca AS 'PLACA', STV.Descripcion AS 'TIPO', ISNULL(OP.Descripcion,'SIN PROGRAMACION') AS 'OPERACION',
			RC.idComponente, C.Descripcion AS 'COMPONENTE', RC.FechaAnterior AS 'FECHA_ANTERIOR', RC.Milimetro AS 'MM',
			CASE WHEN RC.Milimetro >= 50 AND RC.Milimetro <= 52 THEN 'NORMAL' WHEN RC.Milimetro >= 49 AND RC.Milimetro <= 49.9 THEN 'PRECAUCIÓN'
			WHEN RC.Milimetro >= 45 AND RC.Milimetro <= 48.9 THEN 'CRÍTICO' END AS 'STATUS', RC.FechaCrea AS 'FECHA', RC.UsuarioCrea AS 'USUARIO'
			FROM ReportesApp_Mantenimiento_CompDesgaste_RegistroCarreta RC
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = RC.idPlaca
			LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_SubTipoVehiculo STV WITH(NOLOCK) ON STV.idSubTipoVehiculo = V.SubTipoVehiculo
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC WITH(NOLOCK) ON V.IdVehiculo = UC.IdUnidad
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = UC.IdProgramacion
			LEFT JOIN ReportesApp_Mantenimiento_CompDesgaste_Componentes C ON C.idComponente = RC.idComponente
			WHERE (RC.idComponente = 1) AND (RC.Estado = 0) AND (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND
			(RC.FechaCrea BETWEEN @FINICIO AND @FFIN)
			ORDER BY V.NumeroPlaca DESC, RC.idCompDesgasteC DESC

			SELECT RT.idCompDesgasteT, RT.idPlaca, V.NumeroPlaca AS 'PLACA', STV.Descripcion AS 'TIPO', ISNULL(OP.Descripcion,'SIN PROGRAMACION') AS 'OPERACION',
			RT.idComponente, C.Descripcion AS 'COMPONENTE', RT.FechaAnterior AS 'FECHA_ANTERIOR', RT.Actividad AS 'ACTIVIDAD', RT.Nro AS 'NRO',
			CASE WHEN RT.Nro >= 0 AND RT.Nro <= 5 THEN 'NORMAL' WHEN RT.Nro >= 6 AND RT.Nro <= 7 THEN 'PRECAUCIÓN'
			WHEN RT.Nro >= 8 THEN 'CRÍTICO' END AS 'PROGRAMACION', RT.FechaCrea AS 'FECHA', RT.UsuarioCrea AS 'USUARIO'
			FROM ReportesApp_Mantenimiento_CompDesgaste_RegistroTracto RT
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = RT.idPlaca
			LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_SubTipoVehiculo STV WITH(NOLOCK) ON STV.idSubTipoVehiculo = V.SubTipoVehiculo
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC WITH(NOLOCK) ON V.IdVehiculo = UC.IdUnidad
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = UC.IdProgramacion
			LEFT JOIN ReportesApp_Mantenimiento_CompDesgaste_Componentes C ON C.idComponente = RT.idComponente
			WHERE (RT.idComponente = 2) AND (RT.Estado = 0) AND (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND
			(RT.FechaCrea BETWEEN @FINICIO AND @FFIN)
			ORDER BY V.NumeroPlaca DESC, RT.idCompDesgasteT DESC
		END
		ELSE BEGIN
			SELECT RC.idCompDesgasteC, RC.idPlaca, V.NumeroPlaca AS 'PLACA', STV.Descripcion AS 'TIPO', ISNULL(OP.Descripcion,'SIN PROGRAMACION') AS 'OPERACION',
			RC.idComponente, C.Descripcion AS 'COMPONENTE', RC.FechaAnterior AS 'FECHA_ANTERIOR', RC.Milimetro AS 'MM',
			CASE WHEN RC.Milimetro >= 50 AND RC.Milimetro <= 52 THEN 'NORMAL' WHEN RC.Milimetro >= 49 AND RC.Milimetro <= 49.9 THEN 'PRECAUCIÓN'
			WHEN RC.Milimetro >= 45 AND RC.Milimetro <= 48.9 THEN 'CRÍTICO' END AS 'STATUS', RC.FechaCrea AS 'FECHA', RC.UsuarioCrea AS 'USUARIO'
			FROM ReportesApp_Mantenimiento_CompDesgaste_RegistroCarreta RC
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = RC.idPlaca
			LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_SubTipoVehiculo STV WITH(NOLOCK) ON STV.idSubTipoVehiculo = V.SubTipoVehiculo
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC WITH(NOLOCK) ON V.IdVehiculo = UC.IdUnidad
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = UC.IdProgramacion
			LEFT JOIN ReportesApp_Mantenimiento_CompDesgaste_Componentes C ON C.idComponente = RC.idComponente
			WHERE (RC.idComponente = 1) AND (RC.Estado = 0) AND (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND
			(OP.Descripcion IS NULL OR OP.Descripcion LIKE '%' + @Operacion + '%') AND (RC.FechaCrea BETWEEN @FINICIO AND @FFIN)
			ORDER BY V.NumeroPlaca DESC, RC.idCompDesgasteC DESC

			SELECT RT.idCompDesgasteT, RT.idPlaca, V.NumeroPlaca AS 'PLACA', STV.Descripcion AS 'TIPO', ISNULL(OP.Descripcion,'SIN PROGRAMACION') AS 'OPERACION',
			RT.idComponente, C.Descripcion AS 'COMPONENTE', RT.FechaAnterior AS 'FECHA_ANTERIOR', RT.Actividad AS 'ACTIVIDAD', RT.Nro AS 'NRO',
			CASE WHEN RT.Nro >= 0 AND RT.Nro <= 5 THEN 'NORMAL' WHEN RT.Nro >= 6 AND RT.Nro <= 7 THEN 'PRECAUCIÓN'
			WHEN RT.Nro >= 8 THEN 'CRÍTICO' END AS 'PROGRAMACION', RT.FechaCrea AS 'FECHA', RT.UsuarioCrea AS 'USUARIO'
			FROM ReportesApp_Mantenimiento_CompDesgaste_RegistroTracto RT
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = RT.idPlaca
			LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_SubTipoVehiculo STV WITH(NOLOCK) ON STV.idSubTipoVehiculo = V.SubTipoVehiculo
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC WITH(NOLOCK) ON V.IdVehiculo = UC.IdUnidad
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = UC.IdProgramacion
			LEFT JOIN ReportesApp_Mantenimiento_CompDesgaste_Componentes C ON C.idComponente = RT.idComponente
			WHERE (RT.idComponente = 2) AND (RT.Estado = 0) AND (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND
			(OP.Descripcion IS NULL OR OP.Descripcion LIKE '%' + @Operacion + '%') AND (RT.FechaCrea BETWEEN @FINICIO AND @FFIN)
			ORDER BY V.NumeroPlaca DESC, RT.idCompDesgasteT DESC
		END
	END
END

-------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 04-09-2024
-- Description:	ACTUALIZAR COMPONENTE CARRETA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_CompDesgaste_ActualizarComponenteC]
@idPlaca INT,
@idComponente INT,
@FechaAnterior DATE,
@Milimetro DECIMAL(10,2),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

BEGIN TRAN
BEGIN TRY
	DECLARE @UltFecha DATE = (SELECT FechaAnterior FROM ReportesApp_Mantenimiento_CompDesgaste_RegistroCarreta WHERE (idPlaca = @idPlaca) AND (Estado = 1))
	
	IF (@FechaAnterior <= @UltFecha) BEGIN
		SET @Exito = '-1 = No puede actualizar un registro con una fecha menor a la más reciente.'
		ROLLBACK
		GOTO Terminar
	END

	UPDATE ReportesApp_Mantenimiento_CompDesgaste_RegistroCarreta
	SET Estado = 0
	WHERE (idPlaca = @idPlaca) AND (Estado = 1)

	SET @correlativo = (SELECT MAX(idCompDesgasteC) FROM ReportesApp_Mantenimiento_CompDesgaste_RegistroCarreta)
	SET @correlativo = ISNULL(@correlativo,0) + 1 

	INSERT INTO ReportesApp_Mantenimiento_CompDesgaste_RegistroCarreta(idCompDesgasteC,idPlaca,idComponente,FechaAnterior,Milimetro,Estado,UsuarioCrea,FechaCrea)
	VALUES(@correlativo,@idPlaca,@idComponente,@FechaAnterior,@Milimetro,1,@Usuario,GETDATE())

	SET @Exito = '0 = Reporte actualizado correctamente.'
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
-- Create date: 08-09-2024
-- Description:	ACTUALIZAR COMPONENTE TRACTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_CompDesgaste_ActualizarComponenteT]
@idPlaca INT,
@idComponente INT,
@FechaAnterior DATE,
@Actividad VARCHAR(30),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

BEGIN TRAN
BEGIN TRY
	DECLARE @UltFecha DATE = (SELECT FechaAnterior FROM ReportesApp_Mantenimiento_CompDesgaste_RegistroTracto WHERE (idPlaca = @idPlaca) AND (Estado = 1))
	DECLARE @Nro INT = (SELECT Nro FROM ReportesApp_Mantenimiento_CompDesgaste_RegistroTracto WHERE (idPlaca = @idPlaca) AND (Estado = 1))

	IF (@FechaAnterior <= @UltFecha) BEGIN
		SET @Exito = '-1 = No puede actualizar un registro con una fecha menor a la más reciente.'
		ROLLBACK
		GOTO Terminar
	END

	UPDATE ReportesApp_Mantenimiento_CompDesgaste_RegistroTracto
	SET Estado = 0
	WHERE (idPlaca = @idPlaca) AND (Estado = 1)

	SET @correlativo = (SELECT MAX(idCompDesgasteT) FROM ReportesApp_Mantenimiento_CompDesgaste_RegistroTracto)
	SET @correlativo = ISNULL(@correlativo,0) + 1 

	IF (@Actividad = 'REGULACION') BEGIN
		INSERT INTO ReportesApp_Mantenimiento_CompDesgaste_RegistroTracto(idCompDesgasteT,idPlaca,idComponente,FechaAnterior,Actividad,Nro,Estado,UsuarioCrea,FechaCrea)
		VALUES(@correlativo,@idPlaca,@idComponente,@FechaAnterior,@Actividad,@Nro + 1,1,@Usuario,GETDATE())
	END
	ELSE BEGIN
		INSERT INTO ReportesApp_Mantenimiento_CompDesgaste_RegistroTracto(idCompDesgasteT,idPlaca,idComponente,FechaAnterior,Actividad,Nro,Estado,UsuarioCrea,FechaCrea)
		VALUES(@correlativo,@idPlaca,@idComponente,@FechaAnterior,@Actividad,0,1,@Usuario,GETDATE())
	END

	SET @Exito = '0 = Reporte actualizado correctamente.'
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




