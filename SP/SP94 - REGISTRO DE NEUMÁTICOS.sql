
-- CREAR TABLA ReportesApp_Mantenimiento_ControlNeumaticos_Registro

-- CREAR TABLA ReportesApp_Mantenimiento_ControlNeumaticos_Movimiento

----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 20-12-2025
-- Description:	LISTAR MARCAS Y MODELOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlNeumaticos_ListarMarcasModelos]
@Opcion INT
AS
BEGIN
	IF (@Opcion = 1) BEGIN  -- LISTAR MARCAS 
		SELECT 0 AS 'idMarca', 'TODAS' AS 'Descripcion'
		UNION
		SELECT idMarca, Descripcion FROM ReportesApp_Neumaticos_SegundoUso_Marca
	END

	IF (@Opcion = 2) BEGIN  -- LISTAR MARCAS NEUMATICOS
		SELECT idMarca, Descripcion FROM ReportesApp_Neumaticos_SegundoUso_Marca
	END

	IF (@Opcion = 3) BEGIN  -- LISTAR MEDIDA
		SELECT idMedida, Descripcion FROM ReportesApp_Neumaticos_SegundoUso_Medida
	END

	IF (@Opcion = 4) BEGIN  -- LISTAR MODELO
		SELECT idModelo, Descripcion FROM ReportesApp_Neumaticos_SegundoUso_Modelo
	END
END

----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 20-12-2025
-- Description:	REGISTRAR Y EDITAR NEUMATICOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlNeumaticos_RegistrarEditarNeumaticos]
@Opcion INT,
@Codigo VARCHAR(20),
@DOT VARCHAR(20),
@Marca VARCHAR(100),
@Medida VARCHAR(100),
@Modelo VARCHAR(100),
@Tipo VARCHAR(20),
@NSK DECIMAL(10,2),
@KM DECIMAL(10,2),
@Precio DECIMAL(10,2),
@FechaInicio DATE,
@Usuario VARCHAR(20)
AS
DECLARE @idRegistroN INT 
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- REGISTRAR NEUMÁTICO
		IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_ControlNeumaticos_Registro WHERE Codigo = @Codigo AND Marca = @Marca AND Tipo = @Tipo)) BEGIN
			SET @Exito = '-1 = Este código de neumático ya ha sido registrado.'
			ROLLBACK
			GOTO Terminar
		END
		
		SET @idRegistroN = (SELECT MAX(idRegistroN) FROM ReportesApp_Mantenimiento_ControlNeumaticos_Registro)
		SET @idRegistroN = ISNULL(@idRegistroN,0) + 1 

		INSERT INTO ReportesApp_Mantenimiento_ControlNeumaticos_Registro(idRegistroN,Codigo,DOT,Estado,Marca,Medida,Modelo,Tipo,NSK,Precio,FechaInicio,
		KM,[SOL/KM],UsuarioCreacion,FechaCreacion)
		SELECT @idRegistroN, @Codigo, @DOT, CASE WHEN @Tipo = 'ORIGINAL' THEN 'DISPONIBLE' WHEN @Tipo = 'SCRAP' THEN 'DESCARTADO' ELSE 'ASIGNADO' END,
		@Marca, @Medida, @Modelo, @Tipo, @NSK, @Precio, @FechaInicio, @KM, CAST(COALESCE(@Precio / NULLIF(@KM, 0), 0) AS DECIMAL(18,2)), @Usuario, GETDATE()

		SET @Exito = '0 = Neumático Registrado.'
	END

	IF (@Opcion = 2) BEGIN		-- EDITAR NEUMÁTICO
		IF (NOT EXISTS(SELECT * FROM ReportesApp_Mantenimiento_ControlNeumaticos_Registro WHERE Codigo = @Codigo AND Marca = @Marca AND Tipo = @Tipo)) BEGIN
			IF (@Tipo = 'SCRAP') BEGIN
				SET @idRegistroN = (SELECT MAX(idRegistroN) FROM ReportesApp_Mantenimiento_ControlNeumaticos_Registro)
				SET @idRegistroN = ISNULL(@idRegistroN,0) + 1 
					
				INSERT INTO ReportesApp_Mantenimiento_ControlNeumaticos_Registro(idRegistroN,Codigo,DOT,Estado,Marca,Medida,Modelo,Tipo,NSK,Precio,FechaInicio,
				KM,[SOL/KM],UsuarioCreacion,FechaCreacion)
				SELECT @idRegistroN, @Codigo, @DOT, 'DESCARTADO', @Marca, @Medida, @Modelo, @Tipo, @NSK, @Precio, @FechaInicio, @KM,
				CAST(COALESCE(@Precio / NULLIF(@KM, 0), 0) AS DECIMAL(18,2)), @Usuario, GETDATE()

				UPDATE ReportesApp_Mantenimiento_ControlNeumaticos_Registro
				SET Estado = 'DESCARTADO'
				WHERE Codigo = @Codigo AND Marca = @Marca

				SET @Exito = '0 = Neumático Actualizado.'
			END
			ELSE BEGIN
				SET @Exito = '-1 = No existe un neumático con este tipo en el registro.'
				ROLLBACK
				GOTO Terminar
			END
		END
		ELSE BEGIN
			UPDATE ReportesApp_Mantenimiento_ControlNeumaticos_Registro
			SET DOT = @DOT, Marca = @Marca, Medida = @Medida, Modelo = @Modelo, NSK = @NSK, Precio = @Precio, KM = @KM,
			[SOL/KM] = CAST(COALESCE(@Precio / NULLIF(@KM, 0), 0) AS DECIMAL(18,2)), FechaInicio = @FechaInicio, UsuarioCreacion = @Usuario,
			FechaCreacion = GETDATE()
			WHERE Codigo = @Codigo AND Marca = @Marca AND Tipo = @Tipo

			SET @Exito = '0 = Neumático Actualizado.'
		END
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

----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 07-01-2025
-- Description:	LISTAR REGISTRO NEUMÁTICOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlNeumaticos_ListarNeumaticos]
@Codigo VARCHAR(50),
@Marca VARCHAR(50),
@Estado VARCHAR(50)
AS
BEGIN
	WITH
	base AS (SELECT R.Codigo, R.Estado, R.DOT, R.Marca, R.Medida, R.Modelo, R.Tipo, R.NSK, R.Precio, R.FechaInicio, R.KM, R.[SOL/KM], R.UsuarioCreacion,
	R.FechaCreacion, ROW_NUMBER() OVER (PARTITION BY R.Codigo, R.Marca, R.Tipo ORDER BY R.FechaCreacion DESC) AS rn
    FROM ReportesApp_Mantenimiento_ControlNeumaticos_Registro R),
	orig AS (SELECT * FROM base WHERE rn = 1 AND Tipo = 'ORIGINAL'),
	r1 AS (SELECT * FROM base WHERE rn = 1 AND Tipo = '1R'),
	r2 AS (SELECT * FROM base WHERE rn = 1 AND Tipo = '2R'),
	r3 AS (SELECT * FROM base WHERE rn = 1 AND Tipo = '3R'),
	scrap AS (SELECT * FROM base WHERE rn = 1 AND Tipo = 'SCRAP')

	SELECT o.Codigo 'COD', o.Estado 'ESTADO', o.DOT 'DOT', o.Marca 'MARCA', o.Medida 'MEDIDA', o.Modelo 'MODELO',
    o.FechaInicio 'FECHA_INICIO', o.Precio 'PRECIO', o.Tipo 'TIPO', o.NSK 'NSK', o.KM 'KM', o.[SOL/KM] '[SOL/KM]',
    r1.Tipo 'TIPO_1R', r1.NSK 'NSK_1R', r1.KM 'KM_1R', r1.[SOL/KM] '[SOL/KM]_1R',
	r2.Tipo 'TIPO_2R', r2.NSK 'NSK_2R', r2.KM 'KM_2R', r2.[SOL/KM] '[SOL/KM]_2R',
	r3.Tipo 'TIPO_3R', r3.NSK 'NSK_3R', r3.KM 'KM_3R', r3.[SOL/KM] '[SOL/KM]_3R',
	scrap.Tipo 'TIPO_S', scrap.NSK 'NSK_S', scrap.KM 'KM_S', scrap.[SOL/KM] '[SOL/KM]_S',
	t.TOTAL_KM 'TOTAL_KM', CASE WHEN t.TOTAL_KM = 0 THEN CAST(0 AS DECIMAL(18,2))
	ELSE CAST(ROUND(CAST(o.Precio AS decimal(18,2)) / t.TOTAL_KM, 2) AS DECIMAL(18,2)) END AS '[SOL/KM]_TOTAL',
	o.UsuarioCreacion, o.FechaCreacion
	FROM orig o
	LEFT JOIN r1 ON r1.Codigo = o.Codigo AND r1.Marca = o.Marca
	LEFT JOIN r2 ON r2.Codigo = o.Codigo AND r2.Marca = o.Marca
	LEFT JOIN r3 ON r3.Codigo = o.Codigo AND r3.Marca = o.Marca
	LEFT JOIN scrap ON scrap.Codigo = o.Codigo AND scrap.Marca = o.Marca
	CROSS APPLY (SELECT TOTAL_KM = COALESCE(o.KM, 0) + COALESCE(r1.KM, 0) + COALESCE(r2.KM, 0) + COALESCE(r3.KM, 0) + COALESCE(scrap.KM, 0)) t
	WHERE (o.Codigo IS NULL OR o.Codigo LIKE '%' + @Codigo + '%') AND (@Marca = 'TODAS' OR o.Marca = @Marca) AND
	(@Estado = 'TODOS' OR o.Estado = @Estado)
	ORDER BY o.Codigo DESC, o.FechaCreacion DESC
END

----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 10-01-2026
-- Description:	INSTALAR Y DESINSTALAR NEUMATICOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlNeumaticos_InstalarDesinstalar]
@Opcion INT,
@idMovimientoN INT,
@Codigo VARCHAR(20),
@Marca VARCHAR(100),
@idVehiculo INT,
@Tipo VARCHAR(20),
@Posicion INT,
@Fecha DATE,
@KM DECIMAL(10,2),
@NSK DECIMAL(10,2),
@Usuario VARCHAR(20)
AS
DECLARE @ContadorR INT 
DECLARE @ContadorM INT 
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- INSTALAR NEUMÁTICO
		SET @ContadorR = (SELECT MAX(idRegistroN) FROM ReportesApp_Mantenimiento_ControlNeumaticos_Registro)
		SET @ContadorR = ISNULL(@ContadorR,0) + 1 

		UPDATE ReportesApp_Mantenimiento_ControlNeumaticos_Registro
		SET Estado = 'ASIGNADO'
		WHERE Codigo = @Codigo AND Marca = @Marca AND Tipo = 'ORIGINAL'

		SET @ContadorM = (SELECT MAX(idMovimientoN) FROM ReportesApp_Mantenimiento_ControlNeumaticos_Movimiento)
		SET @ContadorM = ISNULL(@ContadorM,0) + 1 

		INSERT INTO ReportesApp_Mantenimiento_ControlNeumaticos_Movimiento(idMovimientoN, Codigo, Marca, idVehiculo, Tipo, Posicion, FechaInstalacion,
		KM, NSK, UsuarioCrea, FechaCrea, UsuarioModifica, FechaModifica)
		SELECT @ContadorM, @Codigo, @Marca, @idVehiculo, @Tipo, @Posicion, @Fecha, @KM, @NSK, @Usuario, GETDATE(), @Usuario, GETDATE()
		
		IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_ControlNeumaticos_Registro WHERE Codigo = @Codigo AND Marca = @Marca AND Tipo = @Tipo)) BEGIN
			WITH
			Mov AS (SELECT M.Codigo, M.Marca, M.Tipo, CASE WHEN SUM(ISNULL(M.KMD,KM.KMActual) - ISNULL(M.KM,0)) < 0 THEN 0
			ELSE SUM(ISNULL(M.KMD,KM.KMActual) - ISNULL(M.KM,0)) END 'KM_Total',
			SUM(ISNULL(M.NSK,0) - ISNULL(M.NSKD,0)) 'NSK_Total'
			FROM ReportesApp_Mantenimiento_ControlNeumaticos_Movimiento M
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = M.idVehiculo
			GROUP BY M.Codigo, M.Marca, M.Tipo)

			UPDATE R
			SET R.NSK = M.NSK_Total, R.KM = M.KM_Total
			FROM ReportesApp_Mantenimiento_ControlNeumaticos_Registro R
			LEFT JOIN Mov M ON M.Codigo = R.Codigo AND M.Marca  = R.Marca AND M.Tipo = R.Tipo
			WHERE R.Codigo = @Codigo AND R.Marca = @Marca AND R.Tipo = @Tipo

			UPDATE R
			SET R.[SOL/KM] = CAST(COALESCE(R.Precio / NULLIF(R.KM, 0), 0) AS DECIMAL(18,2))
			FROM ReportesApp_Mantenimiento_ControlNeumaticos_Registro R
			WHERE R.Codigo = @Codigo AND R.Marca = @Marca AND R.Tipo = @Tipo

			SET @Exito = '0 = Neumático Instalado Exitosamente.'
		END
		ELSE BEGIN
			INSERT INTO ReportesApp_Mantenimiento_ControlNeumaticos_Registro(idRegistroN, Codigo, DOT, Estado, Marca, Medida, Modelo, Tipo,
			NSK, Precio, FechaInicio, KM, [SOL/KM], UsuarioCreacion, FechaCreacion)
			SELECT TOP(1) @ContadorR, Codigo, DOT, 'ASIGNADO', Marca, Medida, Modelo, @Tipo, @NSK, Precio, @Fecha, @KM, CAST(Precio/@KM AS DECIMAL(18,2)),
			@Usuario, GETDATE()
			FROM ReportesApp_Mantenimiento_ControlNeumaticos_Registro
			WHERE Codigo = @Codigo AND Marca = @Marca
		END

		SET @Exito = '0 = Neumático Instalado Exitosamente.'
	END

	IF (@Opcion = 2) BEGIN		-- DESINSTALAR NEUMÁTICO
		UPDATE ReportesApp_Mantenimiento_ControlNeumaticos_Movimiento
		SET FechaDesinstalacion = @Fecha, KMD = @KM, NSKD = @NSK, UsuarioModifica = @Usuario, FechaModifica = GETDATE()
		WHERE idMovimientoN = @idMovimientoN

		;WITH
		Mov AS (SELECT M.Codigo, M.Marca, M.Tipo, CASE WHEN SUM(ISNULL(M.KMD,KM.KMActual) - ISNULL(M.KM,0)) < 0 THEN 0
		ELSE SUM(ISNULL(M.KMD,KM.KMActual) - ISNULL(M.KM,0)) END 'KM_Total',
		SUM(ISNULL(M.NSK,0) - ISNULL(M.NSKD,0)) 'NSK_Total'
		FROM ReportesApp_Mantenimiento_ControlNeumaticos_Movimiento M
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = M.idVehiculo
		GROUP BY M.Codigo, M.Marca, M.Tipo)

		UPDATE R
		SET R.NSK = M.NSK_Total, R.KM = M.KM_Total
		FROM ReportesApp_Mantenimiento_ControlNeumaticos_Registro R
		LEFT JOIN Mov M ON M.Codigo = R.Codigo AND M.Marca  = R.Marca AND M.Tipo = R.Tipo
		WHERE R.Codigo = @Codigo AND R.Marca = @Marca AND R.Tipo = @Tipo

		UPDATE R
		SET R.[SOL/KM] = CAST(COALESCE(R.Precio / NULLIF(R.KM, 0), 0) AS DECIMAL(18,2))
		FROM ReportesApp_Mantenimiento_ControlNeumaticos_Registro R
		WHERE R.Codigo = @Codigo AND R.Marca = @Marca AND R.Tipo = @Tipo

		UPDATE ReportesApp_Mantenimiento_ControlNeumaticos_Registro
		SET Estado = 'DISPONIBLE'
		WHERE Codigo = @Codigo AND Marca = @Marca

		SET @Exito = '0 = Neumático Desinstalado Exitosamente.'
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

----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 12-01-2025
-- Description:	LISTAR MOVIMIENTOS NEUMÁTICOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlNeumaticos_ListarMovimientos]
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@Codigo VARCHAR(50),
@Placa VARCHAR(50)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	SELECT X.idMovimientoN, X.idVehiculo, X.PLACA, X.TIPO_UNIDAD, X.OPERACION, X.POSICION, X.COD, X.MARCA, X.MODELO, X.MEDIDA, X.TIPO,
	X.FECHA_INSTALACION, X.NSK, X.KM, X.FECHA_ACTUAL, X.KM_ACTUAL, X.FECHA_DESINSTALACION, X.NSK_D, X.KM_D, X.KM_TOTAL, X.NSK_TOTAL,
	CAST(CASE WHEN ISNULL(X.NSK_TOTAL,0) = 0.00 THEN 0.00 ELSE ISNULL(X.KM_TOTAL,0.00) / X.NSK_TOTAL END AS DECIMAL(18,2)) AS 'KM/NSK',
	X.UsuarioCrea, X.FechaCrea, X.UsuarioModifica, X.FechaModifica
	FROM (SELECT MN.idMovimientoN, MN.idVehiculo, V.NumeroPlaca AS 'PLACA', SV.Descripcion AS 'TIPO_UNIDAD', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION'
	ELSE O.Descripcion END AS 'OPERACION', MN.Posicion AS 'POSICION', MR.Codigo AS 'COD', MR.Marca AS 'MARCA', MR.Modelo AS 'MODELO',
	MR.Medida AS 'MEDIDA', MN.Tipo AS 'TIPO', CONVERT(VARCHAR,MN.FechaInstalacion,103) AS 'FECHA_INSTALACION', MN.NSK AS 'NSK', MN.KM AS 'KM',
	CONVERT(VARCHAR,KM.Fecha,103) AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL', CONVERT(VARCHAR,MN.FechaDesinstalacion,103) AS 'FECHA_DESINSTALACION',
	ISNULL(MN.NSKD,0) AS 'NSK_D', ISNULL(MN.KMD,0) AS 'KM_D', ISNULL(MN.KMD,KM.KMActual) - MN.KM AS 'KM_TOTAL', MN.NSK - ISNULL(MN.NSKD,0) AS 'NSK_TOTAL',
	MN.UsuarioCrea, MN.FechaCrea, MN.UsuarioModifica, MN.FechaModifica
	FROM ReportesApp_Mantenimiento_ControlNeumaticos_Movimiento MN
	LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = MN.idVehiculo
	LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON UC.IdProgramacion = O.IdOperacion
	LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad 
	LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
	LEFT JOIN ReportesApp_Mantenimiento_ControlNeumaticos_Registro MR ON MR.Codigo = MN.Codigo AND MR.Marca = MN.Marca AND MR.Tipo = MN.Tipo
	LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = MN.idVehiculo
	WHERE (MN.Codigo IS NULL OR MN.Codigo LIKE '%' + @Codigo + '%') AND (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%')
	AND (MN.FechaInstalacion BETWEEN @FINICIO AND @FFIN)) X
	ORDER BY X.idMovimientoN DESC
END

