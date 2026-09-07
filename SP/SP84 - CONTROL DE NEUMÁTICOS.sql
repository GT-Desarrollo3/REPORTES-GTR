
-- CREAR TABLA ReportesApp_Neumatico_ControlNeumaticos_Registro Y LLENARLA

-- CREAR TABLA ReportesApp_Neumatico_ControlNeumaticos_ReporteCumplimiento

-- CREAR TABLA ReportesApp_Neumatico_ControlNeumaticos_Historial

----------------------------------------------------------------------------------

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
DECLARE @correlativoN INT
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

		SET @correlativoN = (SELECT MAX(idRegistro) FROM ReportesApp_Neumatico_ControlNeumaticos_Registro)
		SET @correlativoN = ISNULL(@correlativoN,0) + 1

		SET @Placa = (SELECT NumeroPlaca FROM OP_TR_Vehiculo WHERE IdVehiculo = @idVehiculo)

		IF (@idTipoVehiculo != 2) BEGIN
			INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas(idKilometraje,Estado,idVehiculo,Placa,Fecha,KMActual,ValorAdicional)
			VALUES(@correlativoKM,'OPERATIVO',@idVehiculo,@Placa,GETDATE(),@UltimoKM,0)

			INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_KMRegistro(idKilometraje,Fecha,Kilometraje)
			VALUES(@correlativoKM,GETDATE(),@UltimoKM)

			INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_Registro(idRegistro,idVehiculo,Aceite,Frecuencia,UltimaFecha,UltimoKM,TipoMantenimiento,Usuario,FechaCreacion,idOperacion,idMantenimientoOP,PS)
			VALUES(@correlativo, @idVehiculo, @Aceite, @Frecuencia, @UltimaFecha, @UltimoKM, @TipoMantenimiento, @Usuario, GETDATE(),@idOperacion,@idMttoOP,@PS)

			INSERT INTO ReportesApp_Neumatico_ControlNeumaticos_Registro(idRegistro,idVehiculo,NumeroPlaca,UltimaFecha,UltimoKM,L1E1,L1E2,L1E3,L2E1,L2E2,L2E3,Usuario,FechaCreacion)
			VALUES(@correlativoN,@idVehiculo,@Placa,@UltimaFecha,@UltimoKM,0,0,0,0,0,0,@Usuario,GETDATE())
		END
		ELSE BEGIN
			INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas(idKilometraje,Estado,idVehiculo,Placa,Fecha,KMActual,ValorAdicional)
			VALUES(@correlativoKM,'OPERATIVO',@idVehiculo,@Placa,GETDATE(),@UltimoKM,0)

			INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_KMRegistro(idKilometraje,Fecha,Kilometraje)
			VALUES(@correlativoKM,GETDATE(),@UltimoKM)

			INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_Registro(idRegistro,idVehiculo,Aceite,Frecuencia,UltimaFecha,UltimoKM,TipoMantenimiento,Usuario,FechaCreacion,idOperacion,idMantenimientoOP,PS)
			VALUES(@correlativo, @idVehiculo, '', @Frecuencia, @UltimaFecha, @UltimoKM, @TipoMantenimiento, @Usuario, GETDATE(),@idOperacion,@idMttoOP,@PS)
		END

		SET @Exito = '0 = Mantenimiento Programado Correctamente.'
	END
	
	IF (@Opcion = 2) BEGIN
		DECLARE @idVehiculo2 INT = (SELECT idVehiculo FROM ReportesApp_Mantenimiento_MttoPreventivo_Registro WHERE idRegistro = @idRegistro)
		DECLARE @Placa2 VARCHAR(20) = (SELECT NumeroPlaca FROM OP_TR_Vehiculo WHERE IdVehiculo = @idVehiculo2)

		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_Historial(idRegistro,idVehiculo,Aceite,UltimaFecha,UltimoKM,TipoMantenimiento,Usuario,FechaCreacion,idOperacion,PS)
		SELECT idRegistro,idVehiculo,Aceite,UltimaFecha,UltimoKM,TipoMantenimiento,Usuario,FechaCreacion,idOperacion,PS
		FROM ReportesApp_Mantenimiento_MttoPreventivo_Registro
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

----------------------------------------------------------------------------------

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
DECLARE @correlativoN INT

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

		SET @correlativoN = (SELECT MAX(idRegistro) FROM ReportesApp_Neumatico_ControlNeumaticos_Registro)
		SET @correlativoN = ISNULL(@correlativoN,0) + 1

		SET @TipoMaquina = (SELECT TipoMaquina FROM ME_Maquina WHERE MaquinaCodigo = @MaquinaCodigo)

		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas(idKilometraje,Estado,MaquinaCodigo,TipoMaquina,Fecha,KMActual,ValorAdicional)
		VALUES(@correlativoKM,'OPERATIVO',LTRIM(RTRIM(@MaquinaCodigo)),ISNULL(@TipoMaquina,'0099'),GETDATE(),@UltimoKM,0)

		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_KMRegistroMaquinas(idKilometraje,MaquinaCodigo,Fecha,Kilometraje)
		VALUES(@correlativoKM,LTRIM(RTRIM(@MaquinaCodigo)),GETDATE(),@UltimoKM)

		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas(idRegistroM,MaquinaCodigo,Aceite,Frecuencia,Dueno,Ubicacion,UltimaFecha,UltimoKM,
		TipoMantenimiento,Usuario,FechaCreacion,idMantenimientoOP,PS)
		VALUES(@correlativo, LTRIM(RTRIM(@MaquinaCodigo)), @Aceite, @Frecuencia, @Dueno, @Ubicacion, @UltimaFecha, @UltimoKM, @TipoMantenimiento,
		@Usuario, GETDATE(),@idMttoOP,@PS)

		INSERT INTO ReportesApp_Neumatico_ControlNeumaticos_Registro(idRegistro,NumeroPlaca,UltimaFecha,UltimoKM,L1E1,L1E2,L1E3,L2E1,L2E2,L2E3,Usuario,FechaCreacion)
		VALUES(@correlativoN,LTRIM(RTRIM(@MaquinaCodigo)),@UltimaFecha,@UltimoKM,0,0,0,0,0,0,@Usuario,GETDATE())

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

------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 17-09-2025
-- Description:	LISTAR CONTROL NEUMATICOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Neumatico_ControlNeumaticos_ListarRegistros]
@Opcion INT,
@Placa VARCHAR(20),
@TipoMaquina VARCHAR(10),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@Opcion = 1) BEGIN		-- TRACTOS
		SELECT X.idRegistro, X.idVehiculo, X.PLACA, X.TIPO_UNIDAD, X.SUBTIPO_UNIDAD, X.OPERACION, X.MARCA, X.MODELO, X.[FREC/KM], X.FECHA_ANTERIOR,
		X.KM_ANTERIOR, X.L1E1, X.L1E2, X.L1E3, X.L2E1, X.L2E2, X.L2E3, X.FECHA_ACTUAL, X.KM_ACTUAL, X.[PORCENTAJE (%)],
		(CASE WHEN X.[PORCENTAJE (%)] >= 0 AND X.[PORCENTAJE (%)] < 60 THEN 'CONFORME'
			  WHEN X.[PORCENTAJE (%)] >= 60 AND X.[PORCENTAJE (%)] < 100 THEN 'POR VENCER'
			  WHEN X.[PORCENTAJE (%)] >= 100 OR X.[PORCENTAJE (%)] < 0 THEN 'VENCIDO'
			  ELSE '' END) AS 'ESTADO', X.[KM/DIA], X.KM_ESTIMADO, X.DIFERENCIA_KM,
		CASE WHEN DATEPART(W,(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END))
		IN (6,7) THEN DATEPART(WW,DATEADD(DAY,7,(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END)))
		ELSE DATEPART(WW,(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END)) END AS 'SEMANA',
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		X.ULTIMO_USUARIO, X.ULTIMA_FECHA FROM 
		(SELECT DISTINCT CN.idRegistro, CN.idVehiculo, CN.NumeroPlaca AS 'PLACA', 
		TV.Descripcion AS 'TIPO_UNIDAD', SV.Descripcion AS 'SUBTIPO_UNIDAD', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion
		END AS 'OPERACION', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', V.Modelo AS 'MODELO', CN.Frecuencia AS 'FREC/KM', 
		CN.UltimaFecha AS 'FECHA_ANTERIOR', CN.UltimoKM AS 'KM_ANTERIOR', CN.L1E1, CN.L1E2, CN.L1E3, CN.L2E1, CN.L2E2, CN.L2E3, KM.Fecha AS 'FECHA_ACTUAL',
		KM.KMActual AS 'KM_ACTUAL', ISNULL(CAST((KM.KMActual - CN.UltimoKM) / CN.Frecuencia * 100 AS DECIMAL(10,2)),0) AS 'PORCENTAJE (%)',
		CASE WHEN DATEDIFF(DAY, CN.UltimaFecha, KM.Fecha) = 0 THEN 1 ELSE
		CAST((KM.KMActual - CN.UltimoKM) / DATEDIFF(DAY, CN.UltimaFecha, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA',
		CAST(CN.Frecuencia + CN.UltimoKM AS DECIMAL(10,2)) AS 'KM_ESTIMADO', CAST((CN.Frecuencia + CN.UltimoKM) - KM.KMActual AS DECIMAL(10,2)) AS 'DIFERENCIA_KM',
		CN.Usuario AS 'ULTIMO_USUARIO', CN.FechaCreacion AS 'ULTIMA_FECHA'
		FROM ReportesApp_Neumatico_ControlNeumaticos_Registro CN
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Registro MR ON MR.idVehiculo = CN.idVehiculo
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = CN.idVehiculo
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON UC.IdProgramacion = O.IdOperacion
		LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad 
		LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TipoVehiculo
		LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
		LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = CN.idVehiculo
		WHERE (CN.idVehiculo IS NOT NULL) AND (V.Estado = 2)) X
		WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND
		(CASE WHEN (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) <= CONVERT(DATE,GETDATE()) THEN GETDATE()
		ELSE (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) END BETWEEN @FINICIO AND @FFIN)
		ORDER BY X.PLACA ASC
	END

	IF (@Opcion = 2) BEGIN		-- MAQUINAS
		IF (@TipoMaquina = '0000') BEGIN
			SELECT X.idRegistro, X.MAQUINA, X.GRUPO, X.TIPO_MAQUINA, X.MARCA, X.MODELO, X.DUEÑO, X.UBICACION, X.[FREC/KM], X.FECHA_ANTERIOR, X.KM_ANTERIOR,
			X.L1E1, X.L1E2, X.L1E3, X.L2E1, X.L2E2, X.L2E3, X.FECHA_ACTUAL, X.KM_ACTUAL, X.[PORCENTAJE (%)],
			(CASE WHEN X.[PORCENTAJE (%)] >= 0 AND X.[PORCENTAJE (%)] < 60 THEN 'CONFORME'
				  WHEN X.[PORCENTAJE (%)] >= 60 AND X.[PORCENTAJE (%)] < 100 THEN 'POR VENCER'
				  WHEN X.[PORCENTAJE (%)] >= 100 OR X.[PORCENTAJE (%)] < 0 THEN 'VENCIDO'
				  ELSE '' END) AS 'ESTADO', X.[KM/DIA], X.KM_ESTIMADO, X.DIFERENCIA_KM,
			CASE WHEN DATEPART(W,(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END))
			IN (6,7) THEN DATEPART(WW,DATEADD(DAY,7,(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END)))
			ELSE DATEPART(WW,(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END)) END AS 'SEMANA',
			(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
			X.ULTIMO_USUARIO, X.ULTIMA_FECHA FROM 
			(SELECT DISTINCT CN.idRegistro, LTRIM(RTRIM(CN.NumeroPlaca)) AS 'MAQUINA', LTRIM(RTRIM(ISNULL(TG.DescripcionLocal,'UNIDADES TERCERAS'))) AS 'GRUPO',
			LTRIM(RTRIM(T.DescripcionLocal)) AS 'TIPO_MAQUINA', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', ISNULL(M.Modelo,UT.Modelo) AS 'MODELO', MR.Frecuencia AS 'FREC/KM',
			MR.Dueno AS 'DUEÑO', MR.Ubicacion AS 'UBICACION', CN.UltimaFecha AS 'FECHA_ANTERIOR', CN.UltimoKM AS 'KM_ANTERIOR', CN.L1E1, CN.L1E2, CN.L1E3, CN.L2E1, CN.L2E2, CN.L2E3,
			KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL', CAST((KM.KMActual - CN.UltimoKM) / MR.Frecuencia * 100 AS DECIMAL(10,2)) AS 'PORCENTAJE (%)',
			CASE WHEN DATEDIFF(DAY, CN.UltimaFecha, KM.Fecha) = 0 THEN 1 ELSE
			CAST((KM.KMActual - CN.UltimoKM) / DATEDIFF(DAY, CN.UltimaFecha, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA',
			CAST(MR.Frecuencia + CN.UltimoKM AS DECIMAL(10,2)) AS 'KM_ESTIMADO', CAST((MR.Frecuencia + CN.UltimoKM) - KM.KMActual AS DECIMAL(10,2)) AS 'DIFERENCIA_KM',
			CN.Usuario AS 'ULTIMO_USUARIO', CN.FechaCreacion AS 'ULTIMA_FECHA'
			FROM ReportesApp_Neumatico_ControlNeumaticos_Registro CN
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas MR ON MR.MaquinaCodigo = CN.NumeroPlaca
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_UnidadesTerceras UT ON LTRIM(RTRIM(UT.NumeroPlaca)) = LTRIM(RTRIM(MR.MaquinaCodigo))
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo))
			LEFT JOIN ME_MaquinaMarca ME ON ISNULL(M.Marca,UT.Marca) = ME.Marca
			LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
			LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo)) AND (M.Estado = 'A')
			WHERE (CN.idVehiculo IS NULL)) X
			WHERE (X.MAQUINA IS NULL OR X.MAQUINA LIKE '%' + @Placa + '%') AND
			(CASE WHEN (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) <= CONVERT(DATE,GETDATE()) THEN GETDATE()
			ELSE (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) END BETWEEN @FINICIO AND @FFIN)
			ORDER BY X.MAQUINA ASC
		END
		ELSE BEGIN
			IF (@TipoMaquina = '0099') BEGIN
				SELECT X.idRegistro, X.MAQUINA, X.GRUPO, X.TIPO_MAQUINA, X.MARCA, X.MODELO, X.DUEÑO, X.UBICACION, X.[FREC/KM], X.FECHA_ANTERIOR, X.KM_ANTERIOR,
				X.L1E1, X.L1E2, X.L1E3, X.L2E1, X.L2E2, X.L2E3, X.FECHA_ACTUAL, X.KM_ACTUAL, X.[PORCENTAJE (%)],
				(CASE WHEN X.[PORCENTAJE (%)] >= 0 AND X.[PORCENTAJE (%)] < 60 THEN 'CONFORME'
					  WHEN X.[PORCENTAJE (%)] >= 60 AND X.[PORCENTAJE (%)] < 100 THEN 'POR VENCER'
					  WHEN X.[PORCENTAJE (%)] >= 100 OR X.[PORCENTAJE (%)] < 0 THEN 'VENCIDO'
					  ELSE '' END) AS 'ESTADO', X.[KM/DIA], X.KM_ESTIMADO, X.DIFERENCIA_KM,
				CASE WHEN DATEPART(W,(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END))
				IN (6,7) THEN DATEPART(WW,DATEADD(DAY,7,(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END)))
				ELSE DATEPART(WW,(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END)) END AS 'SEMANA',
				(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
				X.ULTIMO_USUARIO, X.ULTIMA_FECHA FROM 
				(SELECT DISTINCT CN.idRegistro, LTRIM(RTRIM(CN.NumeroPlaca)) AS 'MAQUINA', 'UNIDADES TERCERAS' AS 'GRUPO', ' ' AS 'TIPO_MAQUINA',
				LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', UT.Modelo AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.Dueno AS 'DUEÑO', MR.Ubicacion AS 'UBICACION',
				CN.UltimaFecha AS 'FECHA_ANTERIOR', CN.UltimoKM AS 'KM_ANTERIOR', CN.L1E1, CN.L1E2, CN.L1E3, CN.L2E1, CN.L2E2, CN.L2E3, KM.Fecha AS 'FECHA_ACTUAL',
				KM.KMActual AS 'KM_ACTUAL', CAST((KM.KMActual - CN.UltimoKM) / MR.Frecuencia * 100 AS DECIMAL(10,2)) AS 'PORCENTAJE (%)',
				CASE WHEN DATEDIFF(DAY, CN.UltimaFecha, KM.Fecha) = 0 THEN 1 ELSE
				CAST((KM.KMActual - CN.UltimoKM) / DATEDIFF(DAY, CN.UltimaFecha, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA',
				CAST(MR.Frecuencia + CN.UltimoKM AS DECIMAL(10,2)) AS 'KM_ESTIMADO', CAST((MR.Frecuencia + CN.UltimoKM) - KM.KMActual AS DECIMAL(10,2)) AS 'DIFERENCIA_KM',
				CN.Usuario AS 'ULTIMO_USUARIO', CN.FechaCreacion AS 'ULTIMA_FECHA'
				FROM ReportesApp_Mantenimiento_MttoPreventivo_UnidadesTerceras UT
				LEFT JOIN ReportesApp_Neumatico_ControlNeumaticos_Registro CN ON LTRIM(RTRIM(UT.NumeroPlaca)) = LTRIM(RTRIM(CN.NumeroPlaca))
				LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas MR ON LTRIM(RTRIM(UT.NumeroPlaca)) = LTRIM(RTRIM(MR.MaquinaCodigo))
				LEFT JOIN ME_MaquinaMarca ME ON UT.Marca = ME.Marca
				LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo))) X
				WHERE (X.MAQUINA IS NULL OR X.MAQUINA LIKE '%' + @Placa + '%') AND
				(CASE WHEN (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) <= CONVERT(DATE,GETDATE()) THEN GETDATE()
				ELSE (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) END BETWEEN @FINICIO AND @FFIN)
				ORDER BY X.MAQUINA ASC
			END
			ELSE BEGIN
				SELECT X.idRegistro, X.MAQUINA, X.GRUPO, X.TIPO_MAQUINA, X.MARCA, X.MODELO, X.DUEÑO, X.UBICACION, X.[FREC/KM], X.FECHA_ANTERIOR, X.KM_ANTERIOR,
				X.L1E1, X.L1E2, X.L1E3, X.L2E1, X.L2E2, X.L2E3, X.FECHA_ACTUAL, X.KM_ACTUAL, X.[PORCENTAJE (%)],
				(CASE WHEN X.[PORCENTAJE (%)] >= 0 AND X.[PORCENTAJE (%)] < 60 THEN 'CONFORME'
					  WHEN X.[PORCENTAJE (%)] >= 60 AND X.[PORCENTAJE (%)] < 100 THEN 'POR VENCER'
					  WHEN X.[PORCENTAJE (%)] >= 100 OR X.[PORCENTAJE (%)] < 0 THEN 'VENCIDO'
					  ELSE '' END) AS 'ESTADO', X.[KM/DIA], X.KM_ESTIMADO, X.DIFERENCIA_KM,
				CASE WHEN DATEPART(W,(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END))
				IN (6,7) THEN DATEPART(WW,DATEADD(DAY,7,(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END)))
				ELSE DATEPART(WW,(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END)) END AS 'SEMANA',
				(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
				X.ULTIMO_USUARIO, X.ULTIMA_FECHA FROM 
				(SELECT DISTINCT CN.idRegistro, LTRIM(RTRIM(CN.NumeroPlaca)) AS 'MAQUINA', LTRIM(RTRIM(TG.DescripcionLocal)) AS 'GRUPO', LTRIM(RTRIM(T.DescripcionLocal)) AS 'TIPO_MAQUINA',
				LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', M.Modelo AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.Dueno AS 'DUEÑO', MR.Ubicacion AS 'UBICACION',
				CN.UltimaFecha AS 'FECHA_ANTERIOR', CN.UltimoKM AS 'KM_ANTERIOR', CN.L1E1, CN.L1E2, CN.L1E3, CN.L2E1, CN.L2E2, CN.L2E3, KM.Fecha AS 'FECHA_ACTUAL',
				KM.KMActual AS 'KM_ACTUAL', CAST((KM.KMActual - CN.UltimoKM) / MR.Frecuencia * 100 AS DECIMAL(10,2)) AS 'PORCENTAJE (%)',
				CASE WHEN DATEDIFF(DAY, CN.UltimaFecha, KM.Fecha) = 0 THEN 1 ELSE
				CAST((KM.KMActual - CN.UltimoKM) / DATEDIFF(DAY, CN.UltimaFecha, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA',
				CAST(MR.Frecuencia + CN.UltimoKM AS DECIMAL(10,2)) AS 'KM_ESTIMADO', CAST((MR.Frecuencia + CN.UltimoKM) - KM.KMActual AS DECIMAL(10,2)) AS 'DIFERENCIA_KM',
				CN.Usuario AS 'ULTIMO_USUARIO', CN.FechaCreacion AS 'ULTIMA_FECHA'
				FROM ReportesApp_Neumatico_ControlNeumaticos_Registro CN
				LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas MR ON MR.MaquinaCodigo = CN.NumeroPlaca
				LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo)) 
				LEFT JOIN ME_MaquinaMarca ME ON M.Marca = ME.Marca
				LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
				LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
				LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo))
				WHERE LTRIM(RTRIM(T.TipoMaquina)) = @TipoMaquina AND (M.Estado = 'A')) X
				WHERE (X.MAQUINA IS NULL OR X.MAQUINA LIKE '%' + @Placa + '%') AND
				(CASE WHEN (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) <= CONVERT(DATE,GETDATE()) THEN GETDATE()
				ELSE (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) END BETWEEN @FINICIO AND @FFIN)
				ORDER BY X.MAQUINA ASC
			END
		END
	END

	IF (@Opcion = 3) BEGIN		-- HISTORIAL TRACTOS
		SELECT H.idRegistro, V.NumeroPlaca AS 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION',
		LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', V.Modelo AS 'MODELO', CONVERT(VARCHAR,H.UltimaFecha,103) AS 'FECHA_ANTERIOR', H.UltimoKM AS 'KM_ANTERIOR',
		H.L1E1, H.L1E2, H.L1E3, H.L2E1, H.L2E2, H.L2E3, H.Usuario AS 'ULTIMO_USUARIO', H.FechaCreacion AS 'ULTIMA_FECHA'
		FROM ReportesApp_Neumatico_ControlNeumaticos_Historial H
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = H.idVehiculo
		LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = UC.IdProgramacion
		LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
		LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TipoVehiculo
		WHERE (H.idVehiculo IS NOT NULL) AND (V.Estado = 2) AND (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND
		(H.FechaCreacion BETWEEN @FINICIO AND @FFIN)
		ORDER BY H.FechaCreacion DESC
	END

	IF (@Opcion = 4) BEGIN		-- HISTORIAL MAQUINAS
		IF (@TipoMaquina = '0000') BEGIN
			SELECT H.idRegistro, H.NumeroPlaca AS 'MAQUINA', LTRIM(RTRIM(ISNULL(TG.DescripcionLocal,'UNIDADES TERCERAS'))) AS 'GRUPO',
			LTRIM(RTRIM(T.DescripcionLocal)) AS 'TIPO_MAQUINA', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', ISNULL(M.Modelo,UT.Modelo) AS 'MODELO',
			CONVERT(VARCHAR,H.UltimaFecha,103) AS 'FECHA_ANTERIOR', H.UltimoKM AS 'KM_ANTERIOR', H.L1E1, H.L1E2, H.L1E3, H.L2E1, H.L2E2, H.L2E3,
			H.Usuario AS 'ULTIMO_USUARIO', H.FechaCreacion AS 'ULTIMA_FECHA' FROM ReportesApp_Neumatico_ControlNeumaticos_Historial H
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = H.NumeroPlaca AND (M.Estado = 'A')
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_UnidadesTerceras UT ON LTRIM(RTRIM(UT.NumeroPlaca)) = H.NumeroPlaca
			LEFT JOIN ME_MaquinaMarca ME ON ISNULL(M.Marca,UT.Marca) = ME.Marca
			LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
			LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
			WHERE (H.idVehiculo IS NULL) AND (H.NumeroPlaca IS NULL OR H.NumeroPlaca LIKE '%' + @Placa + '%') AND
			(H.FechaCreacion BETWEEN @FINICIO AND @FFIN)
			ORDER BY H.FechaCreacion DESC
		END
		ELSE BEGIN
			IF (@TipoMaquina = '0099') BEGIN
				SELECT H.idRegistro, H.NumeroPlaca AS 'MAQUINA', 'UNIDADES TERCERAS' AS 'GRUPO', ' ' AS 'TIPO_MAQUINA', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA',
				UT.Modelo AS 'MODELO', CONVERT(VARCHAR,H.UltimaFecha,103) AS 'FECHA_ANTERIOR', H.UltimoKM AS 'KM_ANTERIOR', H.L1E1, H.L1E2, H.L1E3, H.L2E1,
				H.L2E2, H.L2E3, H.Usuario AS 'ULTIMO_USUARIO', H.FechaCreacion AS 'ULTIMA_FECHA'
				FROM ReportesApp_Mantenimiento_MttoPreventivo_UnidadesTerceras UT
				LEFT JOIN ReportesApp_Neumatico_ControlNeumaticos_Historial H ON LTRIM(RTRIM(UT.NumeroPlaca)) = H.NumeroPlaca
				LEFT JOIN ME_MaquinaMarca ME ON UT.Marca = ME.Marca
				WHERE (H.NumeroPlaca IS NULL OR H.NumeroPlaca LIKE '%' + @Placa + '%') AND (H.FechaCreacion BETWEEN @FINICIO AND @FFIN)
				ORDER BY H.FechaCreacion DESC
			END
			ELSE BEGIN
				SELECT H.idRegistro, H.NumeroPlaca AS 'MAQUINA', LTRIM(RTRIM(TG.DescripcionLocal)) AS 'GRUPO', LTRIM(RTRIM(T.DescripcionLocal)) AS 'MAQUINA',
				LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', M.Modelo AS 'MODELO', CONVERT(VARCHAR,H.UltimaFecha,103) AS 'FECHA_ANTERIOR',
				H.UltimoKM AS 'KM_ANTERIOR', H.L1E1, H.L1E2, H.L1E3, H.L2E1, H.L2E2, H.L2E3, H.Usuario AS 'ULTIMO_USUARIO', H.FechaCreacion AS 'ULTIMA_FECHA' 
				FROM ReportesApp_Neumatico_ControlNeumaticos_Historial H
				LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = H.NumeroPlaca AND (M.Estado = 'A')
				LEFT JOIN ME_MaquinaMarca ME ON M.Marca = ME.Marca
				LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
				LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
				WHERE (H.NumeroPlaca IS NULL OR H.NumeroPlaca LIKE '%' + @Placa + '%') AND (H.FechaCreacion BETWEEN @FINICIO AND @FFIN) AND
				(LTRIM(RTRIM(T.TipoMaquina)) = @TipoMaquina)
				ORDER BY H.FechaCreacion DESC
			END
		END
	END
END

------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 18-09-2025
-- Description:	ACTUALIZAR SOLICITUD DE NEUMATICOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Neumatico_ControlNeumaticos_ActualizarRegistros]
@idRegistro INT,
@UltimaFecha DATETIME,
@UltimoKM DECIMAL(10,2),
@L1E1 INT, @L1E2 INT, @L1E3 INT,
@L2E1 INT, @L2E2 INT, @L2E3 INT,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	DECLARE @FechaAnterior DATETIME = (SELECT UltimaFecha FROM ReportesApp_Neumatico_ControlNeumaticos_Registro WHERE idRegistro = @idRegistro)

	IF (CONVERT(DATE,@UltimaFecha) < CONVERT(DATE,@FechaAnterior)) BEGIN
		SET @Exito = '-1 = No puede registrar una fecha menor a la del último registro.'
		ROLLBACK
		GOTO Terminar
	END
	ELSE BEGIN
		INSERT INTO ReportesApp_Neumatico_ControlNeumaticos_Historial(idRegistro,idVehiculo,NumeroPlaca,UltimaFecha,UltimoKM,L1E1,L1E2,L1E3,
		L2E1,L2E2,L2E3,Usuario,FechaCreacion)
		SELECT idRegistro, idVehiculo, NumeroPlaca, UltimaFecha, UltimoKM, L1E1, L1E2, L1E3, L2E1, L2E2, L2E3, Usuario, FechaCreacion
		FROM ReportesApp_Neumatico_ControlNeumaticos_Registro
		WHERE idRegistro = @idRegistro

		UPDATE ReportesApp_Neumatico_ControlNeumaticos_Registro
		SET UltimaFecha = @UltimaFecha, UltimoKM = @UltimoKM, L1E1 = @L1E1, L1E2 = @L1E2, L1E3 = @L1E3, L2E1 = @L2E1, L2E2 = @L2E2, L2E3 = @L2E3,
		Usuario = @Usuario, FechaCreacion = GETDATE()
		WHERE idRegistro = @idRegistro

		DECLARE @Placa2 VARCHAR(20) = (SELECT NumeroPlaca FROM ReportesApp_Neumatico_ControlNeumaticos_Registro WHERE idRegistro = @idRegistro)

		UPDATE ReportesApp_Neumatico_ControlNeumaticos_ReporteCumplimiento
		SET FechaCumplimiento = CONVERT(DATE,@UltimaFecha), Estado = 'EJECUTADO'
		WHERE Placa = @Placa2 AND CONVERT(DATE,@UltimaFecha) >= FCInicio AND CONVERT(DATE,@UltimaFecha) <= FCFin

		SET @Exito = '0 = Registro Actualizado Correctamente.'
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

------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 23-09-2024
-- Description:	GENERAR CUMPLIMIENTO NEUMATICO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Neumatico_ControlNeumaticos_GenerarCumplimiento]
@Placa VARCHAR(50),
@Operacion VARCHAR(50),
@TipoUnidad VARCHAR(250),
@Marca VARCHAR(250),
@Alineamiento VARCHAR(20),
@FechaProgramada DATE,
@FCInicio DATE,
@FCFin DATE
AS
DECLARE @Contador INT 
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (EXISTS(SELECT TOP(1) * FROM ReportesApp_Neumatico_ControlNeumaticos_ReporteCumplimiento WHERE Placa = @Placa AND FCInicio = @FCInicio AND FCFin = @FCFin ORDER BY Nro DESC)) BEGIN
		SET @Exito = '-1 = La unidad '+@Placa+' ya tiene un cumplimiento registrado en esta semana.'
		ROLLBACK
		GOTO Terminar
	END

	SET @Contador = (SELECT MAX(Nro) FROM ReportesApp_Neumatico_ControlNeumaticos_ReporteCumplimiento)
	SET @Contador = ISNULL(@Contador,0) + 1

	IF (EXISTS(SELECT TOP(1) * FROM ReportesApp_Neumatico_ControlNeumaticos_ReporteCumplimiento WHERE Placa = @Placa AND Estado IN ('PROGRAMADO','REPROGRAMADO') ORDER BY Nro DESC)) BEGIN
		UPDATE ReportesApp_Neumatico_ControlNeumaticos_ReporteCumplimiento
		SET Observacion = 'NO INGRESÓ POR OPERACIÓN'
		WHERE Nro = (SELECT TOP(1) Nro FROM ReportesApp_Neumatico_ControlNeumaticos_ReporteCumplimiento WHERE Placa = @Placa AND Estado IN ('PROGRAMADO','REPROGRAMADO') ORDER BY Nro DESC)
	
		INSERT INTO ReportesApp_Neumatico_ControlNeumaticos_ReporteCumplimiento(Nro,Placa,Operacion,TipoUnidad,Marca,Alineamiento,FechaProgramada,FCInicio,FCFin,Estado)
		VALUES(@Contador,@Placa,@Operacion,@TipoUnidad,@Marca,@Alineamiento,@FechaProgramada,@FCInicio,@FCFin,'REPROGRAMADO')
	END
	ELSE BEGIN
		INSERT INTO ReportesApp_Neumatico_ControlNeumaticos_ReporteCumplimiento(Nro,Placa,Operacion,TipoUnidad,Marca,Alineamiento,FechaProgramada,FCInicio,FCFin,Estado)
		VALUES(@Contador,@Placa,@Operacion,@TipoUnidad,@Marca,@Alineamiento,@FechaProgramada,@FCInicio,@FCFin,'PROGRAMADO')
	END

	DECLARE @NroSemana INT = (SELECT CASE WHEN DATEPART(W,@FCInicio) IN (6,7) THEN DATEPART(WW,DATEADD(DAY,7,@FCInicio)) ELSE DATEPART(WW,@FCInicio) END)

	UPDATE ReportesApp_Neumatico_ControlNeumaticos_ReporteCumplimiento
	SET NroSemana = @NroSemana
	WHERE Nro = @Contador

	UPDATE RC
	SET RC.SAB = CASE WHEN DATEPART(W,@FechaProgramada) = 6 AND @FechaProgramada >= @FCInicio AND @FechaProgramada <= @FCFin THEN 'ALI' ELSE RC.SAB END,
		RC.DOM = CASE WHEN DATEPART(W,@FechaProgramada) = 7 AND @FechaProgramada >= @FCInicio AND @FechaProgramada <= @FCFin THEN 'ALI' ELSE RC.DOM END,
		RC.LUN = CASE WHEN DATEPART(W,@FechaProgramada) = 1 AND @FechaProgramada >= @FCInicio AND @FechaProgramada <= @FCFin THEN 'ALI' ELSE RC.LUN END,
		RC.MAR = CASE WHEN DATEPART(W,@FechaProgramada) = 2 AND @FechaProgramada >= @FCInicio AND @FechaProgramada <= @FCFin THEN 'ALI' ELSE RC.MAR END,
		RC.MIE = CASE WHEN DATEPART(W,@FechaProgramada) = 3 AND @FechaProgramada >= @FCInicio AND @FechaProgramada <= @FCFin THEN 'ALI' ELSE RC.MIE END,
		RC.JUE = CASE WHEN DATEPART(W,@FechaProgramada) = 4 AND @FechaProgramada >= @FCInicio AND @FechaProgramada <= @FCFin THEN 'ALI' ELSE RC.JUE END,
		RC.VIE = CASE WHEN DATEPART(W,@FechaProgramada) = 5 AND @FechaProgramada >= @FCInicio AND @FechaProgramada <= @FCFin THEN 'ALI' ELSE RC.VIE END
	FROM ReportesApp_Neumatico_ControlNeumaticos_ReporteCumplimiento RC
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

----------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 23-09-2025
-- Description:	LISTAR CUMPLIMIENTO NEUMATICOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Neumatico_ControlNeumaticos_ListarCumplimiento]
@Anio INT,
@NroSemana INT
AS
BEGIN
	DECLARE @T_Prueba TABLE (Nro VARCHAR(20), Placa VARCHAR(50), Operacion VARCHAR(50), TipoUnidad VARCHAR(250), Marca VARCHAR(70),
							 Tarea VARCHAR(100), FechaProgramada VARCHAR(70), FCInicio VARCHAR(70), FCFin VARCHAR(70), NroSemana VARCHAR(10),
							 FechaCumplimiento VARCHAR(70), Estado VARCHAR(30), Observacion VARCHAR(250), FechaIngreso VARCHAR(70), SAB VARCHAR(15),
							 DOM VARCHAR(15), LUN VARCHAR(15), MAR VARCHAR(15), MIE VARCHAR(15), JUE VARCHAR(15), VIE VARCHAR(15))
	
	DECLARE @SAB VARCHAR(15), @DOM VARCHAR(15), @LUN VARCHAR(15), @MAR VARCHAR(15), @MIE VARCHAR(15), @JUE VARCHAR(15), @VIE VARCHAR(15)

	INSERT INTO @T_Prueba (Nro,Placa,Operacion,TipoUnidad,Marca,Tarea,FechaProgramada,FCInicio,FCFin,NroSemana,FechaCumplimiento,Estado,Observacion,FechaIngreso)
	VALUES ('Nro','<PLACA>','Operacion','TipoUnidad','Marca','Tarea','FechaProgramada','FCInicio','FCFin','NroSemana','FechaCumplimiento','Estado','Observacion','FechaIngreso')
	
	INSERT INTO @T_Prueba (Nro,Placa,Operacion,TipoUnidad,Marca,Tarea,FechaProgramada,FCInicio,FCFin,NroSemana,FechaCumplimiento,Estado,Observacion,FechaIngreso,
	SAB,DOM,LUN,MAR,MIE,JUE,VIE)
	SELECT CONVERT(VARCHAR,Nro), Placa, Operacion, TipoUnidad, Marca, Alineamiento, CONVERT(VARCHAR,FechaProgramada,103), CONVERT(VARCHAR,FCInicio,103),
	CONVERT(VARCHAR,FCFin,103), CONVERT(VARCHAR,NroSemana), CONVERT(VARCHAR,FechaCumplimiento,103), Estado, Observacion, CONVERT(VARCHAR,FechaIngreso,103),
	SAB, DOM, LUN, MAR, MIE, JUE, VIE FROM ReportesApp_Neumatico_ControlNeumaticos_ReporteCumplimiento
	WHERE YEAR(FCFin) = @Anio AND NroSemana = @NroSemana

	DECLARE @FCInicio DATE = (SELECT TOP(1) FCInicio FROM ReportesApp_Neumatico_ControlNeumaticos_ReporteCumplimiento WHERE YEAR(FCFin) = @Anio AND NroSemana = @NroSemana)
	DECLARE @FCFin DATE = (SELECT TOP(1) FCFin FROM ReportesApp_Neumatico_ControlNeumaticos_ReporteCumplimiento WHERE YEAR(FCFin) = @Anio AND NroSemana = @NroSemana)

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
							 Tarea VARCHAR(100), FechaProgramada VARCHAR(70), FCInicio VARCHAR(70), FCFin VARCHAR(70), NroSemana VARCHAR(10),
							 FechaCumplimiento VARCHAR(70), Estado VARCHAR(30), Observacion VARCHAR(250), FechaIngreso VARCHAR(70),
							 SAB VARCHAR(15), DOM VARCHAR(15), LUN VARCHAR(15), MAR VARCHAR(15), MIE VARCHAR(15), JUE VARCHAR(15), VIE VARCHAR(15))

	INSERT INTO #PruebaView
	SELECT * FROM @T_Prueba WHERE Placa <> '<PLACA>'

	SET @SQL = 'SELECT Nro, Placa AS PLACA, Operacion AS OPERACION, TipoUnidad AS TIPO_UNIDAD, Marca AS MARCA, NroSemana AS SEMANA, FechaProgramada AS FECHA_PROG,
				FCInicio AS FECHA_INICIO, FCFin AS FECHA_FIN, SAB AS '+@SAB+', DOM AS '+@DOM+', LUN AS '+@LUN+', MAR AS '+@MAR+', MIE AS '+@MIE+', JUE AS '+@JUE+
				', VIE AS '+@VIE+', FechaIngreso AS FECHA_INGRESO, Estado AS ESTADO, FechaCumplimiento AS FECHA_EJECUCION, Observacion AS OBSERVACION
				FROM #PruebaView ORDER BY Placa'
	EXEC (@SQL)
	DROP TABLE #PruebaView
END

-------------------------------------------------------------------------

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
		CONVERT(VARCHAR,CAST(100.0 * ISNULL(C.TotalCumplidas,0) / NULLIF(P.TotalUnidades,0) AS DECIMAL(10,2))) + ' %' AS 'PORCENTAJE'
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
		CONVERT(VARCHAR,CAST(100.0 * ISNULL(C.TotalCumplidas,0) / NULLIF(P.TotalUnidades,0) AS DECIMAL(10,2))) + ' %' AS 'PORCENTAJE'
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
		CONVERT(VARCHAR,CAST(100.0 * ISNULL(C.TotalCumplidas,0) / NULLIF(P.TotalUnidades,0) AS DECIMAL(10,2))) + ' %' AS 'PORCENTAJE'
		FROM @TABLA_PORC3 P
		LEFT JOIN @TABLA_CUMP3 C ON P.TipoUnidad = C.TipoUnidad
	END

	---------------------------------------------------------------------------------------

	IF (@Opcion = 4) BEGIN		-- CONTAR NEUMATICOS ALINEADOS
		DECLARE @TotalMttoProg INT = (SELECT COUNT(*) FROM ReportesApp_Neumatico_ControlNeumaticos_ReporteCumplimiento
		WHERE YEAR(FCFin) = @Anio AND NroSemana = @NroSemana)

		DECLARE @TotalMttoEjec INT = (SELECT COUNT(*) FROM ReportesApp_Neumatico_ControlNeumaticos_ReporteCumplimiento
		WHERE Estado = 'EJECUTADO' AND YEAR(FCFin) = @Anio AND NroSemana = @NroSemana)

		SELECT @TotalMttoProg AS 'TOTAL_UNIDADES', @TotalMttoEjec AS 'TOTAL_EJECUTADOS'
	END

	IF (@Opcion = 5) BEGIN		-- PORCENTAJE ACTIVIDADES
		DECLARE @TABLA_PORC4 TABLE (TipoUnidad VARCHAR(50), TotalUnidades INT)
		DECLARE @TABLA_CUMP4 TABLE (TipoUnidad VARCHAR(50), TotalCumplidas INT)

		INSERT INTO @TABLA_PORC4(TipoUnidad, TotalUnidades)
		SELECT TipoUnidad, COUNT(*)
		FROM ReportesApp_Neumatico_ControlNeumaticos_ReporteCumplimiento
		WHERE YEAR(FCFin) = @Anio AND NroSemana = @NroSemana
		GROUP BY TipoUnidad

		INSERT INTO @TABLA_CUMP4(TipoUnidad, TotalCumplidas)
		SELECT TipoUnidad, COUNT(*)
		FROM ReportesApp_Neumatico_ControlNeumaticos_ReporteCumplimiento
		WHERE Estado = 'EJECUTADO' AND YEAR(FCFin) = @Anio AND NroSemana = @NroSemana
		GROUP BY TipoUnidad

		SELECT P.TipoUnidad AS 'TIPO_UNIDAD', P.TotalUnidades AS 'TOTAL_UNIDADES', ISNULL(C.TotalCumplidas,0) AS 'TOTAL_CUMPLIDAS',
		CONVERT(VARCHAR,CAST(100.0 * ISNULL(C.TotalCumplidas,0) / NULLIF(P.TotalUnidades,0) AS DECIMAL(10,2))) + ' %' AS 'PORCENTAJE'

		FROM @TABLA_PORC4 P
		LEFT JOIN @TABLA_CUMP4 C ON P.TipoUnidad = C.TipoUnidad
	END
END

--------------------------------------------------------------------------

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

	IF (@Opcion = 4) BEGIN		-- ELIMINAR ALINEAMIENTO
		DELETE FROM ReportesApp_Neumatico_ControlNeumaticos_ReporteCumplimiento
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

----------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 25-09-2025
-- Description: PROGRAMAR CUMPLIMIENTO DE ALINEAMIENTOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Neumatico_ControlNeumaticos_ProgramarCumplimiento]
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
	IF (@Opcion = 1) BEGIN		-- ACTUALIZAR ESTADO
		DECLARE @FCInicio DATE = (SELECT FCInicio FROM ReportesApp_Neumatico_ControlNeumaticos_ReporteCumplimiento WHERE Nro = @Nro)
		DECLARE @FCFin DATE = (SELECT FCFin FROM ReportesApp_Neumatico_ControlNeumaticos_ReporteCumplimiento WHERE Nro = @Nro)

		IF (@FechaCump < @FCInicio OR @FechaCump > @FCFin) BEGIN
			SET @Exito = '-1 = No puede programar esta inspección en un día que no corresponda a esta semana.'
			ROLLBACK
			GOTO Terminar
		END

		UPDATE ReportesApp_Neumatico_ControlNeumaticos_ReporteCumplimiento
		SET SAB = NULL, DOM = NULL, LUN = NULL, MAR = NULL, MIE = NULL, JUE = NULL, VIE = NULL
		WHERE Nro = @Nro

		IF (@Estado = 'EJECUTADO') BEGIN
			UPDATE ReportesApp_Neumatico_ControlNeumaticos_ReporteCumplimiento
			SET FechaCumplimiento = @FechaCump, Estado = @Estado, Observacion = @Observacion,
			SAB = CASE WHEN DATEPART(W,@FechaCump) = 6 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'ALI' ELSE SAB END,
			DOM = CASE WHEN DATEPART(W,@FechaCump) = 7 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'ALI' ELSE DOM END,
			LUN = CASE WHEN DATEPART(W,@FechaCump) = 1 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'ALI' ELSE LUN END,
			MAR = CASE WHEN DATEPART(W,@FechaCump) = 2 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'ALI' ELSE MAR END,
			MIE = CASE WHEN DATEPART(W,@FechaCump) = 3 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'ALI' ELSE MIE END,
			JUE = CASE WHEN DATEPART(W,@FechaCump) = 4 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'ALI' ELSE JUE END,
			VIE = CASE WHEN DATEPART(W,@FechaCump) = 5 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'ALI' ELSE VIE END
			WHERE Nro = @Nro
		END
		ELSE BEGIN
			UPDATE ReportesApp_Neumatico_ControlNeumaticos_ReporteCumplimiento
			SET FechaCumplimiento = NULL, Estado = @Estado, Observacion = @Observacion,
			SAB = CASE WHEN DATEPART(W,@FechaCump) = 6 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'ALI' ELSE SAB END,
			DOM = CASE WHEN DATEPART(W,@FechaCump) = 7 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'ALI' ELSE DOM END,
			LUN = CASE WHEN DATEPART(W,@FechaCump) = 1 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'ALI' ELSE LUN END,
			MAR = CASE WHEN DATEPART(W,@FechaCump) = 2 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'ALI' ELSE MAR END,
			MIE = CASE WHEN DATEPART(W,@FechaCump) = 3 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'ALI' ELSE MIE END,
			JUE = CASE WHEN DATEPART(W,@FechaCump) = 4 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'ALI' ELSE JUE END,
			VIE = CASE WHEN DATEPART(W,@FechaCump) = 5 AND @FechaCump >= FCInicio AND @FechaCump <= FCFin THEN 'ALI' ELSE VIE END
			WHERE Nro = @Nro
		END

		SET @Exito = '0 = Cumplimiento actualizado correctamente.'
	END

	IF (@Opcion = 2) BEGIN		-- ACTUALIZAR FECHA DE INGRESO
		UPDATE ReportesApp_Neumatico_ControlNeumaticos_ReporteCumplimiento
		SET FechaIngreso = @FechaCump
		WHERE Nro = @Nro

		SET @Exito = '0 = Ingreso registrado correctamente.'
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

----------------------------------------------------------------------------------

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
	DECLARE @Placa VARCHAR(20) = (SELECT Placa FROM ReportesApp_Mantenimiento_MttoPreventivo_ReporteCumplimiento WHERE Nro = @Nro)

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
			SET FechaCumplimiento = NULL, Estado = @Estado, Observacion = @Observacion,
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

		UPDATE ReportesApp_Neumatico_ControlNeumaticos_ReporteCumplimiento
		SET FechaIngreso = @FechaProgramada
		WHERE FCInicio = @FCInicio AND FCFin = @FCFin AND Placa = @Placa

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

----------------------------------------------------------------------------------

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
							 Alineamiento VARCHAR(30), SAB VARCHAR(15), DOM VARCHAR(15), LUN VARCHAR(15), MAR VARCHAR(15), MIE VARCHAR(15), JUE VARCHAR(15),
							 VIE VARCHAR(15))
	
	DECLARE @SAB VARCHAR(15), @DOM VARCHAR(15), @LUN VARCHAR(15), @MAR VARCHAR(15), @MIE VARCHAR(15), @JUE VARCHAR(15), @VIE VARCHAR(15)

	INSERT INTO @T_Prueba (Nro, Operacion, Placa, TipoUnidad, Marca, MttoPreventivo, FechaProgramada, FCInicio, FCFin, NroSemana, Estado, FechaCumplimiento,
						   Observacion, FechaIngreso, ObservacionOP, Alineamiento)
	VALUES ('Nro', 'Operacion', '<PLACA>', 'TipoUnidad', 'Marca', 'MttoPreventivo', 'FechaProgramada', 'FCInicio', 'FCFin', 'NroSemana', 'Estado',
			'FechaCumplimiento', 'Observacion','FechaIngreso','ObservacionOP','Alineamiento')
	
	INSERT INTO @T_Prueba (Nro,Operacion,Placa,TipoUnidad,Marca,MttoPreventivo,FechaProgramada,FCInicio,FCFin,NroSemana,Estado,Observacion,FechaCumplimiento,
	FechaIngreso,ObservacionOP,Alineamiento,SAB,DOM,LUN,MAR,MIE,JUE,VIE)
	SELECT CONVERT(VARCHAR,MP.Nro), MP.Operacion, MP.Placa, MP.TipoUnidad, MP.Marca, MP.MttoPreventivo, CONVERT(VARCHAR,MP.FechaProgramada,103),
	CONVERT(VARCHAR,MP.FCInicio,103), CONVERT(VARCHAR,MP.FCFin,103), CONVERT(VARCHAR,MP.NroSemana), MP.Estado, MP.Observacion, 
	CONVERT(VARCHAR,MP.FechaCumplimiento,103), CONVERT(VARCHAR,MP.FechaIngreso,103), MP.ObservacionOP,
	(SELECT CASE WHEN CN.Estado = 'EJECUTADO' THEN 'EJECUTADO' ELSE 'PENDIENTE' END FROM ReportesApp_Neumatico_ControlNeumaticos_ReporteCumplimiento CN
	WHERE CN.Placa = MP.Placa AND CN.FCInicio = MP.FCInicio AND CN.FCFin = MP.FCFin), MP.SAB, MP.DOM, MP.LUN, MP.MAR, MP.MIE, MP.JUE, MP.VIE
	FROM ReportesApp_Mantenimiento_MttoPreventivo_ReporteCumplimiento MP
	WHERE YEAR(MP.FCFin) = @Anio AND MP.NroSemana = @NroSemana

	DECLARE @FCInicio DATE = (SELECT TOP(1) FCInicio FROM ReportesApp_Mantenimiento_MttoPreventivo_ReporteCumplimiento WHERE YEAR(FCFin) = @Anio AND NroSemana = @NroSemana)
	DECLARE @FCFin DATE = (SELECT TOP(1) FCFin FROM ReportesApp_Mantenimiento_MttoPreventivo_ReporteCumplimiento WHERE YEAR(FCFin) = @Anio AND NroSemana = @NroSemana)

	WHILE (@FCInicio <= @FCFin) BEGIN
		DECLARE @Concatenado VARCHAR(15) = LEFT(UPPER(DATENAME(WEEKDAY, @FCInicio)), 3) + '' + RIGHT( '0' + CAST(DAY(@FCInicio) AS VARCHAR(2)),2)

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
							 Alineamiento VARCHAR(30), SAB VARCHAR(15), DOM VARCHAR(15), LUN VARCHAR(15), MAR VARCHAR(15), MIE VARCHAR(15), JUE VARCHAR(15),
							 VIE VARCHAR(15))

	INSERT INTO #PruebaView
	SELECT * FROM @T_Prueba WHERE Placa <> '<PLACA>'

	SET @SQL = 'SELECT Nro, Placa AS PLACA, Operacion AS OPERACION, TipoUnidad AS TIPO_UNIDAD, Marca AS MARCA, MttoPreventivo AS MTTO_PREVENTIVO,
				FechaProgramada AS FECHA_PROG, NroSemana AS SEMANA, FCInicio AS FECHA_INICIO, FCFin AS FECHA_FIN, SAB AS '+@SAB+', DOM AS '+@DOM+',
				LUN AS '+@LUN+', MAR AS '+@MAR+', MIE AS '+@MIE+', JUE AS '+@JUE+', VIE AS '+@VIE+', Alineamiento AS ALINEAMIENTO, FechaIngreso AS FECHA_INGRESO,
				Estado AS ESTADO, FechaCumplimiento AS FECHA_EJECUCION, Observacion AS OBSERVACION, ObservacionOP AS OBSERVACION_OP
				FROM #PruebaView ORDER BY CONVERT(DATE,FechaProgramada)'
	EXEC (@SQL)
	DROP TABLE #PruebaView
END



SELECT * FROM ReportesApp_Neumatico_ControlNeumaticos_Registro

SELECT * FROM ReportesApp_Neumatico_ControlNeumaticos_Historial

SELECT * FROM ReportesApp_Neumatico_ControlNeumaticos_ReporteCumplimiento