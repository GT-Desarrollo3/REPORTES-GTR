                      
-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_PlacasView

-------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 22-05-2024
-- Description: BUSCAR PLAN DE MTTO
-- =============================================
/*
EXEC ReportesApp_Mantenimiento_MttoPreventivo_BuscarPlanMtto @Periodo = '042024', @Placa = '', @Programacion = 'TODO', @TipoUnidad = 'TRACTO'
*/
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_BuscarPlanMtto]
@Periodo VARCHAR(6),
@Placa VARCHAR(30),
@Programacion VARCHAR(30),
@TipoUnidad VARCHAR(30)
AS
DECLARE @Dia DATE
DECLARE @FechaIni DATE, @FechaFin DATE, @Domingo DATE
DECLARE @Exito VARCHAR(20) = 'Registro Exitoso'
BEGIN
	SET @Dia = RIGHT(@Periodo,4)+'-'+LEFT(@Periodo,2)+'-01'
	SET @FechaIni = DATEADD(mm,DATEDIFF(mm,0,@Dia),0)
	SET @FechaFin = CAST(DATEADD(MONTH,DATEDIFF(MONTH,0,@Dia)+1,0)-1 AS DATE)
	SET @Domingo = DATEADD(DAY, 7-DATEPART(DW,@FechaIni),@FechaIni)

	WHILE @Domingo <= @FechaFin BEGIN
		SET @Domingo = DATEADD(DAY,7,@Domingo)
	END

	DECLARE @UltimoDiaMes TINYINT = DAY(@fechafin)

	DELETE FROM ReportesApp_Mantenimiento_MttoPreventivo_PlacasView
	DBCC CHECKIDENT(ReportesApp_Mantenimiento_MttoPreventivo_PlacasView, RESEED, 0)

	IF (@Programacion = 'TODO' AND @TipoUnidad = 'TODOS') BEGIN
		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_PlacasView(Placa,Operacion,TipoUnidad,UltimoMtto,UltimaFecha,ProxMtto,FechaProyectada,DiferenciaKM,Anio,Mes,NroDias)
		(SELECT X.PLACA, X.OPERACION, X.TIPO_UNIDAD, X.TIPO_MTTO, X.FECHA_UM, X.PROXIMO_MTTO,
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		(CASE WHEN X.DIFERENCIA_KM < 0 THEN 0.00 ELSE X.DIFERENCIA_KM END) AS 'DIFERENCIA_KM', RIGHT(@Periodo,4), LEFT(@Periodo,2), @UltimoDiaMes
		FROM (SELECT MR.idRegistro AS 'NRO', MR.idVehiculo, V.NumeroPlaca AS 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION',
			  MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', V.Modelo AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
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
			  LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
			  LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = MR.idVehiculo
			  WHERE UC.IdProgramacion != 6) X
			  WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND MONTH(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) = LEFT(@Periodo,2)
			  AND YEAR(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) = RIGHT(@Periodo,4))
		UNION
		(SELECT X.PLACA, 'LOCAL', X.MAQUINA, X.TIPO_MTTO, X.FECHA_UM, X.PROXIMO_MTTO,
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		(CASE WHEN X.DIFERENCIA_KM < 0 THEN 0.00 ELSE X.DIFERENCIA_KM END) AS 'DIFERENCIA_KM', RIGHT(@Periodo,4), LEFT(@Periodo,2), @UltimoDiaMes
		FROM (SELECT DISTINCT MR.idRegistroM AS 'NRO', LTRIM(RTRIM(MR.MaquinaCodigo)) AS 'PLACA', LTRIM(RTRIM(ISNULL(TG.DescripcionLocal,'UNIDADES TERCERAS'))) AS 'GRUPO', LTRIM(RTRIM(T.DescripcionLocal)) AS 'MAQUINA',
			MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', ISNULL(M.Modelo,UT.Modelo) AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
			MR.TipoMantenimiento AS 'TIPO_MTTO', (SELECT ISNULL(TipoMantenimiento,CASE WHEN MR.Aceite = 'SINTETICO' THEN 'M0' ELSE 'M1' END) FROM ReportesApp_Mantenimiento_MttoPreventivo_TipoMtto WHERE idMantenimientoOP = MR.idMantenimientoOP AND Posicion = MR.PS + 1) AS 'PROXIMO_MTTO',
			KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL', CAST((KM.KMActual - MR.UltimoKM) / MR.Frecuencia * 100 AS DECIMAL(10,2)) AS 'PORCENTAJE (%)',
			CASE WHEN DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) = 0 THEN 1 ELSE
			CAST((KM.KMActual - MR.UltimoKM) / DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA',
			CAST(MR.Frecuencia + MR.UltimoKM AS DECIMAL(10,2)) AS 'KM_ESTIMADO', CAST((MR.Frecuencia + MR.UltimoKM) - KM.KMActual AS DECIMAL(10,2)) AS 'DIFERENCIA_KM',
			MR.Usuario AS 'ULTIMO_USUARIO', MR.FechaCreacion AS 'ULTIMA_FECHA'
			FROM ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas MR
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_UnidadesTerceras UT ON LTRIM(RTRIM(UT.NumeroPlaca)) = LTRIM(RTRIM(MR.MaquinaCodigo))
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo)) AND (M.Estado = 'A')
			LEFT JOIN ME_MaquinaMarca ME ON ISNULL(M.Marca,UT.Marca) = ME.Marca
			LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
			LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo))) X
			WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND MONTH(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) = LEFT(@Periodo,2)
			AND YEAR(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) = RIGHT(@Periodo,4))
		ORDER BY X.PLACA ASC

		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_PlacasView(Placa,Operacion,TipoUnidad,UltimoMtto,UltimaFecha,ProxMtto,FechaProyectada,DiferenciaKM,Anio,Mes,NroDias)
		(SELECT X.PLACA, X.OPERACION, X.TIPO_UNIDAD, X.TIPO_MTTO + ' (E)', X.FECHA_UM, X.PROXIMO_MTTO,
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		(CASE WHEN X.DIFERENCIA_KM < 0 THEN 0.00 ELSE X.DIFERENCIA_KM END) AS 'DIFERENCIA_KM', RIGHT(@Periodo,4), LEFT(@Periodo,2), @UltimoDiaMes
		FROM (SELECT MR.idRegistro AS 'NRO', MR.idVehiculo, V.NumeroPlaca AS 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION',
			  MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', V.Modelo AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
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
			  LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
			  LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = MR.idVehiculo
			  WHERE UC.IdProgramacion != 6) X
			  WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND MONTH(X.FECHA_UM) = LEFT(@Periodo,2) AND YEAR(X.FECHA_UM) = RIGHT(@Periodo,4))
		UNION
		(SELECT X.PLACA, 'LOCAL', X.MAQUINA, X.TIPO_MTTO + ' (E)', X.FECHA_UM, X.PROXIMO_MTTO,
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		(CASE WHEN X.DIFERENCIA_KM < 0 THEN 0.00 ELSE X.DIFERENCIA_KM END) AS 'DIFERENCIA_KM', RIGHT(@Periodo,4), LEFT(@Periodo,2), @UltimoDiaMes
		FROM (SELECT DISTINCT MR.idRegistroM AS 'NRO', LTRIM(RTRIM(MR.MaquinaCodigo)) AS 'PLACA', LTRIM(RTRIM(ISNULL(TG.DescripcionLocal,'UNIDADES TERCERAS'))) AS 'GRUPO', LTRIM(RTRIM(T.DescripcionLocal)) AS 'MAQUINA',
			MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', ISNULL(M.Modelo,UT.Modelo) AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
			MR.TipoMantenimiento AS 'TIPO_MTTO', (SELECT ISNULL(TipoMantenimiento,CASE WHEN MR.Aceite = 'SINTETICO' THEN 'M0' ELSE 'M1' END) FROM ReportesApp_Mantenimiento_MttoPreventivo_TipoMtto WHERE idMantenimientoOP = MR.idMantenimientoOP AND Posicion = MR.PS + 1) AS 'PROXIMO_MTTO',
			KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL', CAST((KM.KMActual - MR.UltimoKM) / MR.Frecuencia * 100 AS DECIMAL(10,2)) AS 'PORCENTAJE (%)',
			CASE WHEN DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) = 0 THEN 1 ELSE
			CAST((KM.KMActual - MR.UltimoKM) / DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA',
			CAST(MR.Frecuencia + MR.UltimoKM AS DECIMAL(10,2)) AS 'KM_ESTIMADO', CAST((MR.Frecuencia + MR.UltimoKM) - KM.KMActual AS DECIMAL(10,2)) AS 'DIFERENCIA_KM',
			MR.Usuario AS 'ULTIMO_USUARIO', MR.FechaCreacion AS 'ULTIMA_FECHA'
			FROM ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas MR
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_UnidadesTerceras UT ON LTRIM(RTRIM(UT.NumeroPlaca)) = LTRIM(RTRIM(MR.MaquinaCodigo))
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo)) AND (M.Estado = 'A')
			LEFT JOIN ME_MaquinaMarca ME ON ISNULL(M.Marca,UT.Marca) = ME.Marca
			LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
			LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo))) X
			WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND MONTH(X.FECHA_UM) = LEFT(@Periodo,2) AND YEAR(X.FECHA_UM) = RIGHT(@Periodo,4))
		ORDER BY X.PLACA ASC
	END

	IF (@Programacion != 'TODO' AND @TipoUnidad = 'TODOS') BEGIN
		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_PlacasView(Placa,Operacion,TipoUnidad,UltimoMtto,UltimaFecha,ProxMtto,FechaProyectada,DiferenciaKM,Anio,Mes,NroDias)
		(SELECT X.PLACA, X.OPERACION, X.TIPO_UNIDAD, X.TIPO_MTTO, X.FECHA_UM, X.PROXIMO_MTTO,
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		(CASE WHEN X.DIFERENCIA_KM < 0 THEN 0.00 ELSE X.DIFERENCIA_KM END) AS 'DIFERENCIA_KM', RIGHT(@Periodo,4), LEFT(@Periodo,2), @UltimoDiaMes
		FROM (SELECT MR.idRegistro AS 'NRO', MR.idVehiculo, V.NumeroPlaca AS 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION',
			  MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', V.Modelo AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
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
			  LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
			  LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = MR.idVehiculo
			  WHERE UC.IdProgramacion != 6) X
			  WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND MONTH(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) = LEFT(@Periodo,2)
			  AND YEAR(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) = RIGHT(@Periodo,4) AND X.OPERACION = @Programacion)
		UNION
		(SELECT X.PLACA, 'LOCAL', X.MAQUINA, X.TIPO_MTTO, X.FECHA_UM, X.PROXIMO_MTTO,
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		(CASE WHEN X.DIFERENCIA_KM < 0 THEN 0.00 ELSE X.DIFERENCIA_KM END) AS 'DIFERENCIA_KM', RIGHT(@Periodo,4), LEFT(@Periodo,2), @UltimoDiaMes
		FROM (SELECT DISTINCT MR.idRegistroM AS 'NRO', LTRIM(RTRIM(MR.MaquinaCodigo)) AS 'PLACA', LTRIM(RTRIM(ISNULL(TG.DescripcionLocal,'UNIDADES TERCERAS'))) AS 'GRUPO', LTRIM(RTRIM(T.DescripcionLocal)) AS 'MAQUINA',
			MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', ISNULL(M.Modelo,UT.Modelo) AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
			MR.TipoMantenimiento AS 'TIPO_MTTO', (SELECT ISNULL(TipoMantenimiento,CASE WHEN MR.Aceite = 'SINTETICO' THEN 'M0' ELSE 'M1' END) FROM ReportesApp_Mantenimiento_MttoPreventivo_TipoMtto WHERE idMantenimientoOP = MR.idMantenimientoOP AND Posicion = MR.PS + 1) AS 'PROXIMO_MTTO',
			KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL', CAST((KM.KMActual - MR.UltimoKM) / MR.Frecuencia * 100 AS DECIMAL(10,2)) AS 'PORCENTAJE (%)',
			CASE WHEN DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) = 0 THEN 1 ELSE
			CAST((KM.KMActual - MR.UltimoKM) / DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA',
			CAST(MR.Frecuencia + MR.UltimoKM AS DECIMAL(10,2)) AS 'KM_ESTIMADO', CAST((MR.Frecuencia + MR.UltimoKM) - KM.KMActual AS DECIMAL(10,2)) AS 'DIFERENCIA_KM',
			MR.Usuario AS 'ULTIMO_USUARIO', MR.FechaCreacion AS 'ULTIMA_FECHA'
			FROM ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas MR
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_UnidadesTerceras UT ON LTRIM(RTRIM(UT.NumeroPlaca)) = LTRIM(RTRIM(MR.MaquinaCodigo))
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo)) AND (M.Estado = 'A')
			LEFT JOIN ME_MaquinaMarca ME ON ISNULL(M.Marca,UT.Marca) = ME.Marca
			LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
			LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo))) X
			WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND MONTH(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) = LEFT(@Periodo,2)
			AND YEAR(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) = RIGHT(@Periodo,4) AND @Programacion = 'LOCAL')
		ORDER BY X.PLACA ASC

		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_PlacasView(Placa,Operacion,TipoUnidad,UltimoMtto,UltimaFecha,ProxMtto,FechaProyectada,DiferenciaKM,Anio,Mes,NroDias)
		(SELECT X.PLACA, X.OPERACION, X.TIPO_UNIDAD, X.TIPO_MTTO + ' (E)', X.FECHA_UM, X.PROXIMO_MTTO,
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		(CASE WHEN X.DIFERENCIA_KM < 0 THEN 0.00 ELSE X.DIFERENCIA_KM END) AS 'DIFERENCIA_KM', RIGHT(@Periodo,4), LEFT(@Periodo,2), @UltimoDiaMes
		FROM (SELECT MR.idRegistro AS 'NRO', MR.idVehiculo, V.NumeroPlaca AS 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION',
			  MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', V.Modelo AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
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
			  LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
			  LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = MR.idVehiculo
			  WHERE UC.IdProgramacion != 6) X
			  WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND MONTH(X.FECHA_UM) = LEFT(@Periodo,2) AND YEAR(X.FECHA_UM) = RIGHT(@Periodo,4) AND X.OPERACION = @Programacion)
		UNION
		(SELECT X.PLACA, 'LOCAL', X.MAQUINA, X.TIPO_MTTO + ' (E)', X.FECHA_UM, X.PROXIMO_MTTO,
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		(CASE WHEN X.DIFERENCIA_KM < 0 THEN 0.00 ELSE X.DIFERENCIA_KM END) AS 'DIFERENCIA_KM', RIGHT(@Periodo,4), LEFT(@Periodo,2), @UltimoDiaMes
		FROM (SELECT DISTINCT MR.idRegistroM AS 'NRO', LTRIM(RTRIM(MR.MaquinaCodigo)) AS 'PLACA', LTRIM(RTRIM(ISNULL(TG.DescripcionLocal,'UNIDADES TERCERAS'))) AS 'GRUPO', LTRIM(RTRIM(T.DescripcionLocal)) AS 'MAQUINA',
			MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', ISNULL(M.Modelo,UT.Modelo) AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
			MR.TipoMantenimiento AS 'TIPO_MTTO', (SELECT ISNULL(TipoMantenimiento,CASE WHEN MR.Aceite = 'SINTETICO' THEN 'M0' ELSE 'M1' END) FROM ReportesApp_Mantenimiento_MttoPreventivo_TipoMtto WHERE idMantenimientoOP = MR.idMantenimientoOP AND Posicion = MR.PS + 1) AS 'PROXIMO_MTTO',
			KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL', CAST((KM.KMActual - MR.UltimoKM) / MR.Frecuencia * 100 AS DECIMAL(10,2)) AS 'PORCENTAJE (%)',
			CASE WHEN DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) = 0 THEN 1 ELSE
			CAST((KM.KMActual - MR.UltimoKM) / DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA',
			CAST(MR.Frecuencia + MR.UltimoKM AS DECIMAL(10,2)) AS 'KM_ESTIMADO', CAST((MR.Frecuencia + MR.UltimoKM) - KM.KMActual AS DECIMAL(10,2)) AS 'DIFERENCIA_KM',
			MR.Usuario AS 'ULTIMO_USUARIO', MR.FechaCreacion AS 'ULTIMA_FECHA'
			FROM ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas MR
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_UnidadesTerceras UT ON LTRIM(RTRIM(UT.NumeroPlaca)) = LTRIM(RTRIM(MR.MaquinaCodigo))
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo)) AND (M.Estado = 'A')
			LEFT JOIN ME_MaquinaMarca ME ON ISNULL(M.Marca,UT.Marca) = ME.Marca
			LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
			LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo))) X
			WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND MONTH(X.FECHA_UM) = LEFT(@Periodo,2) AND YEAR(X.FECHA_UM) = RIGHT(@Periodo,4) AND @Programacion = 'LOCAL')
		ORDER BY X.PLACA ASC
	END

	IF (@Programacion = 'TODO' AND @TipoUnidad != 'TODOS') BEGIN
		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_PlacasView(Placa,Operacion,TipoUnidad,UltimoMtto,UltimaFecha,ProxMtto,FechaProyectada,DiferenciaKM,Anio,Mes,NroDias)
		(SELECT X.PLACA, X.OPERACION, X.TIPO_UNIDAD, X.TIPO_MTTO, X.FECHA_UM, X.PROXIMO_MTTO,
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		(CASE WHEN X.DIFERENCIA_KM < 0 THEN 0.00 ELSE X.DIFERENCIA_KM END) AS 'DIFERENCIA_KM', RIGHT(@Periodo,4), LEFT(@Periodo,2), @UltimoDiaMes
		FROM (SELECT MR.idRegistro AS 'NRO', MR.idVehiculo, V.NumeroPlaca AS 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION',
			  MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', V.Modelo AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
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
			  LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
			  LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = MR.idVehiculo
			  WHERE UC.IdProgramacion != 6) X
			  WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND MONTH(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) = LEFT(@Periodo,2)
			  AND YEAR(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) = RIGHT(@Periodo,4) AND X.TIPO_UNIDAD = @TipoUnidad)
		UNION
		(SELECT X.PLACA, 'LOCAL', X.MAQUINA, X.TIPO_MTTO, X.FECHA_UM, X.PROXIMO_MTTO,
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		(CASE WHEN X.DIFERENCIA_KM < 0 THEN 0.00 ELSE X.DIFERENCIA_KM END) AS 'DIFERENCIA_KM', RIGHT(@Periodo,4), LEFT(@Periodo,2), @UltimoDiaMes
		FROM (SELECT DISTINCT MR.idRegistroM AS 'NRO', LTRIM(RTRIM(MR.MaquinaCodigo)) AS 'PLACA', LTRIM(RTRIM(ISNULL(TG.DescripcionLocal,'UNIDADES TERCERAS'))) AS 'GRUPO', LTRIM(RTRIM(T.DescripcionLocal)) AS 'MAQUINA',
			MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', ISNULL(M.Modelo,UT.Modelo) AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
			MR.TipoMantenimiento AS 'TIPO_MTTO', (SELECT ISNULL(TipoMantenimiento,CASE WHEN MR.Aceite = 'SINTETICO' THEN 'M0' ELSE 'M1' END) FROM ReportesApp_Mantenimiento_MttoPreventivo_TipoMtto WHERE idMantenimientoOP = MR.idMantenimientoOP AND Posicion = MR.PS + 1) AS 'PROXIMO_MTTO',
			KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL', CAST((KM.KMActual - MR.UltimoKM) / MR.Frecuencia * 100 AS DECIMAL(10,2)) AS 'PORCENTAJE (%)',
			CASE WHEN DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) = 0 THEN 1 ELSE
			CAST((KM.KMActual - MR.UltimoKM) / DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA',
			CAST(MR.Frecuencia + MR.UltimoKM AS DECIMAL(10,2)) AS 'KM_ESTIMADO', CAST((MR.Frecuencia + MR.UltimoKM) - KM.KMActual AS DECIMAL(10,2)) AS 'DIFERENCIA_KM',
			MR.Usuario AS 'ULTIMO_USUARIO', MR.FechaCreacion AS 'ULTIMA_FECHA'
			FROM ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas MR
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_UnidadesTerceras UT ON LTRIM(RTRIM(UT.NumeroPlaca)) = LTRIM(RTRIM(MR.MaquinaCodigo))
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo)) AND (M.Estado = 'A')
			LEFT JOIN ME_MaquinaMarca ME ON ISNULL(M.Marca,UT.Marca) = ME.Marca
			LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
			LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo))) X
			WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND MONTH(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) = LEFT(@Periodo,2)
			AND YEAR(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) = RIGHT(@Periodo,4) AND X.MAQUINA = @TipoUnidad)
		ORDER BY X.PLACA ASC

		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_PlacasView(Placa,Operacion,TipoUnidad,UltimoMtto,UltimaFecha,ProxMtto,FechaProyectada,DiferenciaKM,Anio,Mes,NroDias)
		(SELECT X.PLACA, X.OPERACION, X.TIPO_UNIDAD, X.TIPO_MTTO + ' (E)', X.FECHA_UM, X.PROXIMO_MTTO,
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		(CASE WHEN X.DIFERENCIA_KM < 0 THEN 0.00 ELSE X.DIFERENCIA_KM END) AS 'DIFERENCIA_KM', RIGHT(@Periodo,4), LEFT(@Periodo,2), @UltimoDiaMes
		FROM (SELECT MR.idRegistro AS 'NRO', MR.idVehiculo, V.NumeroPlaca AS 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION',
			  MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', V.Modelo AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
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
			  LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
			  LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = MR.idVehiculo
			  WHERE UC.IdProgramacion != 6) X
			  WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND MONTH(X.FECHA_UM) = LEFT(@Periodo,2) AND YEAR(X.FECHA_UM) = RIGHT(@Periodo,4) AND X.TIPO_UNIDAD = @TipoUnidad)
		UNION
		(SELECT X.PLACA, 'LOCAL', X.MAQUINA, X.TIPO_MTTO + ' (E)', X.FECHA_UM, X.PROXIMO_MTTO,
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		(CASE WHEN X.DIFERENCIA_KM < 0 THEN 0.00 ELSE X.DIFERENCIA_KM END) AS 'DIFERENCIA_KM', RIGHT(@Periodo,4), LEFT(@Periodo,2), @UltimoDiaMes
		FROM (SELECT DISTINCT MR.idRegistroM AS 'NRO', LTRIM(RTRIM(MR.MaquinaCodigo)) AS 'PLACA', LTRIM(RTRIM(ISNULL(TG.DescripcionLocal,'UNIDADES TERCERAS'))) AS 'GRUPO', LTRIM(RTRIM(T.DescripcionLocal)) AS 'MAQUINA',
			MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', ISNULL(M.Modelo,UT.Modelo) AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
			MR.TipoMantenimiento AS 'TIPO_MTTO', (SELECT ISNULL(TipoMantenimiento,CASE WHEN MR.Aceite = 'SINTETICO' THEN 'M0' ELSE 'M1' END) FROM ReportesApp_Mantenimiento_MttoPreventivo_TipoMtto WHERE idMantenimientoOP = MR.idMantenimientoOP AND Posicion = MR.PS + 1) AS 'PROXIMO_MTTO',
			KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL', CAST((KM.KMActual - MR.UltimoKM) / MR.Frecuencia * 100 AS DECIMAL(10,2)) AS 'PORCENTAJE (%)',
			CASE WHEN DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) = 0 THEN 1 ELSE
			CAST((KM.KMActual - MR.UltimoKM) / DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA',
			CAST(MR.Frecuencia + MR.UltimoKM AS DECIMAL(10,2)) AS 'KM_ESTIMADO', CAST((MR.Frecuencia + MR.UltimoKM) - KM.KMActual AS DECIMAL(10,2)) AS 'DIFERENCIA_KM',
			MR.Usuario AS 'ULTIMO_USUARIO', MR.FechaCreacion AS 'ULTIMA_FECHA'
			FROM ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas MR
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_UnidadesTerceras UT ON LTRIM(RTRIM(UT.NumeroPlaca)) = LTRIM(RTRIM(MR.MaquinaCodigo))
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo)) AND (M.Estado = 'A')
			LEFT JOIN ME_MaquinaMarca ME ON ISNULL(M.Marca,UT.Marca) = ME.Marca
			LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
			LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo))) X
			WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND MONTH(X.FECHA_UM) = LEFT(@Periodo,2) AND YEAR(X.FECHA_UM) = RIGHT(@Periodo,4) AND X.MAQUINA = @TipoUnidad)
		ORDER BY X.PLACA ASC
	END

	IF (@Programacion != 'TODO' AND @TipoUnidad != 'TODOS') BEGIN
		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_PlacasView(Placa,Operacion,TipoUnidad,UltimoMtto,UltimaFecha,ProxMtto,FechaProyectada,DiferenciaKM,Anio,Mes,NroDias)
		(SELECT X.PLACA, X.OPERACION, X.TIPO_UNIDAD, X.TIPO_MTTO, X.FECHA_UM, X.PROXIMO_MTTO,
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		(CASE WHEN X.DIFERENCIA_KM < 0 THEN 0.00 ELSE X.DIFERENCIA_KM END) AS 'DIFERENCIA_KM', RIGHT(@Periodo,4), LEFT(@Periodo,2), @UltimoDiaMes
		FROM (SELECT MR.idRegistro AS 'NRO', MR.idVehiculo, V.NumeroPlaca AS 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION',
			  MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', V.Modelo AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
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
			  LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
			  LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = MR.idVehiculo
			  WHERE UC.IdProgramacion != 6) X
			  WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND MONTH(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) = LEFT(@Periodo,2)
			  AND YEAR(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) = RIGHT(@Periodo,4) AND X.OPERACION = @Programacion AND X.TIPO_UNIDAD = @TipoUnidad)
		UNION
		(SELECT X.PLACA, 'LOCAL', X.MAQUINA, X.TIPO_MTTO, X.FECHA_UM, X.PROXIMO_MTTO,
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		(CASE WHEN X.DIFERENCIA_KM < 0 THEN 0.00 ELSE X.DIFERENCIA_KM END) AS 'DIFERENCIA_KM', RIGHT(@Periodo,4), LEFT(@Periodo,2), @UltimoDiaMes
		FROM (SELECT DISTINCT MR.idRegistroM AS 'NRO', LTRIM(RTRIM(MR.MaquinaCodigo)) AS 'PLACA', LTRIM(RTRIM(ISNULL(TG.DescripcionLocal,'UNIDADES TERCERAS'))) AS 'GRUPO', LTRIM(RTRIM(T.DescripcionLocal)) AS 'MAQUINA',
			MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', ISNULL(M.Modelo,UT.Modelo) AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
			MR.TipoMantenimiento AS 'TIPO_MTTO', (SELECT ISNULL(TipoMantenimiento,CASE WHEN MR.Aceite = 'SINTETICO' THEN 'M0' ELSE 'M1' END) FROM ReportesApp_Mantenimiento_MttoPreventivo_TipoMtto WHERE idMantenimientoOP = MR.idMantenimientoOP AND Posicion = MR.PS + 1) AS 'PROXIMO_MTTO',
			KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL', CAST((KM.KMActual - MR.UltimoKM) / MR.Frecuencia * 100 AS DECIMAL(10,2)) AS 'PORCENTAJE (%)',
			CASE WHEN DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) = 0 THEN 1 ELSE
			CAST((KM.KMActual - MR.UltimoKM) / DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA',
			CAST(MR.Frecuencia + MR.UltimoKM AS DECIMAL(10,2)) AS 'KM_ESTIMADO', CAST((MR.Frecuencia + MR.UltimoKM) - KM.KMActual AS DECIMAL(10,2)) AS 'DIFERENCIA_KM',
			MR.Usuario AS 'ULTIMO_USUARIO', MR.FechaCreacion AS 'ULTIMA_FECHA'
			FROM ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas MR
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_UnidadesTerceras UT ON LTRIM(RTRIM(UT.NumeroPlaca)) = LTRIM(RTRIM(MR.MaquinaCodigo))
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo)) AND (M.Estado = 'A')
			LEFT JOIN ME_MaquinaMarca ME ON ISNULL(M.Marca,UT.Marca) = ME.Marca
			LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
			LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo))) X
			WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND MONTH(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) = LEFT(@Periodo,2)
			AND YEAR(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) = RIGHT(@Periodo,4) AND @Programacion = 'LOCAL' AND X.MAQUINA = @TipoUnidad)
		ORDER BY X.PLACA ASC

		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_PlacasView(Placa,Operacion,TipoUnidad,UltimoMtto,UltimaFecha,ProxMtto,FechaProyectada,DiferenciaKM,Anio,Mes,NroDias)
		(SELECT X.PLACA, X.OPERACION, X.TIPO_UNIDAD, X.TIPO_MTTO + ' (E)', X.FECHA_UM, X.PROXIMO_MTTO,
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		(CASE WHEN X.DIFERENCIA_KM < 0 THEN 0.00 ELSE X.DIFERENCIA_KM END) AS 'DIFERENCIA_KM', RIGHT(@Periodo,4), LEFT(@Periodo,2), @UltimoDiaMes
		FROM (SELECT MR.idRegistro AS 'NRO', MR.idVehiculo, V.NumeroPlaca AS 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION',
			  MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', V.Modelo AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
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
			  LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
			  LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = MR.idVehiculo
			  WHERE UC.IdProgramacion != 6) X
			  WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND MONTH(X.FECHA_UM) = LEFT(@Periodo,2) AND YEAR(X.FECHA_UM) = RIGHT(@Periodo,4) AND X.OPERACION = @Programacion AND X.TIPO_UNIDAD = @TipoUnidad)
		UNION
		(SELECT X.PLACA, 'LOCAL', X.MAQUINA, X.TIPO_MTTO + ' (E)', X.FECHA_UM, X.PROXIMO_MTTO,
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		(CASE WHEN X.DIFERENCIA_KM < 0 THEN 0.00 ELSE X.DIFERENCIA_KM END) AS 'DIFERENCIA_KM', RIGHT(@Periodo,4), LEFT(@Periodo,2), @UltimoDiaMes
		FROM (SELECT DISTINCT MR.idRegistroM AS 'NRO', LTRIM(RTRIM(MR.MaquinaCodigo)) AS 'PLACA', LTRIM(RTRIM(ISNULL(TG.DescripcionLocal,'UNIDADES TERCERAS'))) AS 'GRUPO', LTRIM(RTRIM(T.DescripcionLocal)) AS 'MAQUINA',
			MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', ISNULL(M.Modelo,UT.Modelo) AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
			MR.TipoMantenimiento AS 'TIPO_MTTO', (SELECT ISNULL(TipoMantenimiento,CASE WHEN MR.Aceite = 'SINTETICO' THEN 'M0' ELSE 'M1' END) FROM ReportesApp_Mantenimiento_MttoPreventivo_TipoMtto WHERE idMantenimientoOP = MR.idMantenimientoOP AND Posicion = MR.PS + 1) AS 'PROXIMO_MTTO',
			KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL', CAST((KM.KMActual - MR.UltimoKM) / MR.Frecuencia * 100 AS DECIMAL(10,2)) AS 'PORCENTAJE (%)',
			CASE WHEN DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) = 0 THEN 1 ELSE
			CAST((KM.KMActual - MR.UltimoKM) / DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA',
			CAST(MR.Frecuencia + MR.UltimoKM AS DECIMAL(10,2)) AS 'KM_ESTIMADO', CAST((MR.Frecuencia + MR.UltimoKM) - KM.KMActual AS DECIMAL(10,2)) AS 'DIFERENCIA_KM',
			MR.Usuario AS 'ULTIMO_USUARIO', MR.FechaCreacion AS 'ULTIMA_FECHA'
			FROM ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas MR
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_UnidadesTerceras UT ON LTRIM(RTRIM(UT.NumeroPlaca)) = LTRIM(RTRIM(MR.MaquinaCodigo))
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo)) AND (M.Estado = 'A')
			LEFT JOIN ME_MaquinaMarca ME ON ISNULL(M.Marca,UT.Marca) = ME.Marca
			LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
			LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo))) X
			WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND MONTH(X.FECHA_UM) = LEFT(@Periodo,2) AND YEAR(X.FECHA_UM) = RIGHT(@Periodo,4) AND @Programacion = 'LOCAL' AND X.MAQUINA = @TipoUnidad)
		ORDER BY X.PLACA ASC
	END

	DECLARE @Nro INT = 1
	WHILE (@Nro <= (SELECT MAX(Nro) FROM ReportesApp_Mantenimiento_MttoPreventivo_PlacasView)) BEGIN
		DECLARE @x_ProxMtto VARCHAR(20) = (SELECT ProxMtto FROM ReportesApp_Mantenimiento_MttoPreventivo_PlacasView WHERE Nro = @Nro)
		DECLARE @x_FechaSelec DATETIME = (SELECT FechaProyectada FROM ReportesApp_Mantenimiento_MttoPreventivo_PlacasView WHERE Nro = @Nro)

		DECLARE @x_UltimoMtto VARCHAR(20) = (SELECT UltimoMtto FROM ReportesApp_Mantenimiento_MttoPreventivo_PlacasView WHERE Nro = @Nro)
		DECLARE @x_UltimaFecha DATETIME = (SELECT UltimaFecha FROM ReportesApp_Mantenimiento_MttoPreventivo_PlacasView WHERE Nro = @Nro)

		UPDATE P
		SET P.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 AND MONTH(@x_FechaSelec) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_FechaSelec) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_ProxMtto ELSE P.D1 END,
			P.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 AND MONTH(@x_FechaSelec) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_FechaSelec) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_ProxMtto ELSE P.D2 END,
			P.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 AND MONTH(@x_FechaSelec) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_FechaSelec) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_ProxMtto ELSE P.D3 END,
			P.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 AND MONTH(@x_FechaSelec) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_FechaSelec) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_ProxMtto ELSE P.D4 END,
			P.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 AND MONTH(@x_FechaSelec) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_FechaSelec) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_ProxMtto ELSE P.D5 END,
			P.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 AND MONTH(@x_FechaSelec) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_FechaSelec) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_ProxMtto ELSE P.D6 END,
			P.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 AND MONTH(@x_FechaSelec) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_FechaSelec) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_ProxMtto ELSE P.D7 END,
			P.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 AND MONTH(@x_FechaSelec) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_FechaSelec) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_ProxMtto ELSE P.D8 END,
			P.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 AND MONTH(@x_FechaSelec) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_FechaSelec) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_ProxMtto ELSE P.D9 END,
			P.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 AND MONTH(@x_FechaSelec) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_FechaSelec) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_ProxMtto ELSE P.D10 END,
			P.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 AND MONTH(@x_FechaSelec) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_FechaSelec) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_ProxMtto ELSE P.D11 END,
			P.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 AND MONTH(@x_FechaSelec) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_FechaSelec) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_ProxMtto ELSE P.D12 END,
			P.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 AND MONTH(@x_FechaSelec) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_FechaSelec) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_ProxMtto ELSE P.D13 END,
			P.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 AND MONTH(@x_FechaSelec) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_FechaSelec) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_ProxMtto ELSE P.D14 END,
			P.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 AND MONTH(@x_FechaSelec) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_FechaSelec) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_ProxMtto ELSE P.D15 END,
			P.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 AND MONTH(@x_FechaSelec) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_FechaSelec) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_ProxMtto ELSE P.D16 END,
			P.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 AND MONTH(@x_FechaSelec) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_FechaSelec) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_ProxMtto ELSE P.D17 END,
			P.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 AND MONTH(@x_FechaSelec) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_FechaSelec) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_ProxMtto ELSE P.D18 END,
			P.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 AND MONTH(@x_FechaSelec) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_FechaSelec) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_ProxMtto ELSE P.D19 END,
			P.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 AND MONTH(@x_FechaSelec) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_FechaSelec) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_ProxMtto ELSE P.D20 END,
			P.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 AND MONTH(@x_FechaSelec) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_FechaSelec) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_ProxMtto ELSE P.D21 END,
			P.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 AND MONTH(@x_FechaSelec) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_FechaSelec) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_ProxMtto ELSE P.D22 END,
			P.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 AND MONTH(@x_FechaSelec) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_FechaSelec) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_ProxMtto ELSE P.D23 END,
			P.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 AND MONTH(@x_FechaSelec) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_FechaSelec) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_ProxMtto ELSE P.D24 END,
			P.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 AND MONTH(@x_FechaSelec) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_FechaSelec) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_ProxMtto ELSE P.D25 END,
			P.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 AND MONTH(@x_FechaSelec) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_FechaSelec) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_ProxMtto ELSE P.D26 END,
			P.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 AND MONTH(@x_FechaSelec) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_FechaSelec) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_ProxMtto ELSE P.D27 END,
			P.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 AND MONTH(@x_FechaSelec) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_FechaSelec) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_ProxMtto ELSE P.D28 END,
			P.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 AND MONTH(@x_FechaSelec) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_FechaSelec) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_ProxMtto ELSE P.D29 END,
			P.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 AND MONTH(@x_FechaSelec) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_FechaSelec) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_ProxMtto ELSE P.D30 END,
			P.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 AND MONTH(@x_FechaSelec) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_FechaSelec) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_ProxMtto ELSE P.D31 END
			FROM ReportesApp_Mantenimiento_MttoPreventivo_PlacasView P
			WHERE P.Nro = @Nro
		
		UPDATE P
		SET P.D1 = CASE WHEN DAY(@x_UltimaFecha) = 1 AND MONTH(@x_UltimaFecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_UltimaFecha) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_UltimoMtto ELSE P.D1 END,
			P.D2 = CASE WHEN DAY(@x_UltimaFecha) = 2 AND MONTH(@x_UltimaFecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_UltimaFecha) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_UltimoMtto ELSE P.D2 END,
			P.D3 = CASE WHEN DAY(@x_UltimaFecha) = 3 AND MONTH(@x_UltimaFecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_UltimaFecha) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_UltimoMtto ELSE P.D3 END,
			P.D4 = CASE WHEN DAY(@x_UltimaFecha) = 4 AND MONTH(@x_UltimaFecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_UltimaFecha) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_UltimoMtto ELSE P.D4 END,
			P.D5 = CASE WHEN DAY(@x_UltimaFecha) = 5 AND MONTH(@x_UltimaFecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_UltimaFecha) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_UltimoMtto ELSE P.D5 END,
			P.D6 = CASE WHEN DAY(@x_UltimaFecha) = 6 AND MONTH(@x_UltimaFecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_UltimaFecha) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_UltimoMtto ELSE P.D6 END,
			P.D7 = CASE WHEN DAY(@x_UltimaFecha) = 7 AND MONTH(@x_UltimaFecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_UltimaFecha) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_UltimoMtto ELSE P.D7 END,
			P.D8 = CASE WHEN DAY(@x_UltimaFecha) = 8 AND MONTH(@x_UltimaFecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_UltimaFecha) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_UltimoMtto ELSE P.D8 END,
			P.D9 = CASE WHEN DAY(@x_UltimaFecha) = 9 AND MONTH(@x_UltimaFecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_UltimaFecha) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_UltimoMtto ELSE P.D9 END,
			P.D10 = CASE WHEN DAY(@x_UltimaFecha) = 10 AND MONTH(@x_UltimaFecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_UltimaFecha) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_UltimoMtto ELSE P.D10 END,
			P.D11 = CASE WHEN DAY(@x_UltimaFecha) = 11 AND MONTH(@x_UltimaFecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_UltimaFecha) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_UltimoMtto ELSE P.D11 END,
			P.D12 = CASE WHEN DAY(@x_UltimaFecha) = 12 AND MONTH(@x_UltimaFecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_UltimaFecha) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_UltimoMtto ELSE P.D12 END,
			P.D13 = CASE WHEN DAY(@x_UltimaFecha) = 13 AND MONTH(@x_UltimaFecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_UltimaFecha) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_UltimoMtto ELSE P.D13 END,
			P.D14 = CASE WHEN DAY(@x_UltimaFecha) = 14 AND MONTH(@x_UltimaFecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_UltimaFecha) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_UltimoMtto ELSE P.D14 END,
			P.D15 = CASE WHEN DAY(@x_UltimaFecha) = 15 AND MONTH(@x_UltimaFecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_UltimaFecha) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_UltimoMtto ELSE P.D15 END,
			P.D16 = CASE WHEN DAY(@x_UltimaFecha) = 16 AND MONTH(@x_UltimaFecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_UltimaFecha) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_UltimoMtto ELSE P.D16 END,
			P.D17 = CASE WHEN DAY(@x_UltimaFecha) = 17 AND MONTH(@x_UltimaFecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_UltimaFecha) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_UltimoMtto ELSE P.D17 END,
			P.D18 = CASE WHEN DAY(@x_UltimaFecha) = 18 AND MONTH(@x_UltimaFecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_UltimaFecha) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_UltimoMtto ELSE P.D18 END,
			P.D19 = CASE WHEN DAY(@x_UltimaFecha) = 19 AND MONTH(@x_UltimaFecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_UltimaFecha) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_UltimoMtto ELSE P.D19 END,
			P.D20 = CASE WHEN DAY(@x_UltimaFecha) = 20 AND MONTH(@x_UltimaFecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_UltimaFecha) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_UltimoMtto ELSE P.D20 END,
			P.D21 = CASE WHEN DAY(@x_UltimaFecha) = 21 AND MONTH(@x_UltimaFecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_UltimaFecha) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_UltimoMtto ELSE P.D21 END,
			P.D22 = CASE WHEN DAY(@x_UltimaFecha) = 22 AND MONTH(@x_UltimaFecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_UltimaFecha) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_UltimoMtto ELSE P.D22 END,
			P.D23 = CASE WHEN DAY(@x_UltimaFecha) = 23 AND MONTH(@x_UltimaFecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_UltimaFecha) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_UltimoMtto ELSE P.D23 END,
			P.D24 = CASE WHEN DAY(@x_UltimaFecha) = 24 AND MONTH(@x_UltimaFecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_UltimaFecha) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_UltimoMtto ELSE P.D24 END,
			P.D25 = CASE WHEN DAY(@x_UltimaFecha) = 25 AND MONTH(@x_UltimaFecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_UltimaFecha) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_UltimoMtto ELSE P.D25 END,
			P.D26 = CASE WHEN DAY(@x_UltimaFecha) = 26 AND MONTH(@x_UltimaFecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_UltimaFecha) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_UltimoMtto ELSE P.D26 END,
			P.D27 = CASE WHEN DAY(@x_UltimaFecha) = 27 AND MONTH(@x_UltimaFecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_UltimaFecha) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_UltimoMtto ELSE P.D27 END,
			P.D28 = CASE WHEN DAY(@x_UltimaFecha) = 28 AND MONTH(@x_UltimaFecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_UltimaFecha) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_UltimoMtto ELSE P.D28 END,
			P.D29 = CASE WHEN DAY(@x_UltimaFecha) = 29 AND MONTH(@x_UltimaFecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_UltimaFecha) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_UltimoMtto ELSE P.D29 END,
			P.D30 = CASE WHEN DAY(@x_UltimaFecha) = 30 AND MONTH(@x_UltimaFecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_UltimaFecha) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_UltimoMtto ELSE P.D30 END,
			P.D31 = CASE WHEN DAY(@x_UltimaFecha) = 31 AND MONTH(@x_UltimaFecha) = CONVERT(INT,LEFT(@Periodo,2)) AND YEAR(@x_UltimaFecha) = CONVERT(INT,RIGHT(@Periodo,4)) THEN @x_UltimoMtto ELSE P.D31 END
			FROM ReportesApp_Mantenimiento_MttoPreventivo_PlacasView P
			WHERE P.Nro = @Nro

		SET @Nro = @Nro + 1
	END

	SELECT @Exito exito
END

-------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 22-05-2024
-- Description: BUSCAR PLAN DE MTTO
-- =============================================
/*
EXEC ReportesApp_Mantenimiento_MttoPreventivo_BuscarPlanMtto @Periodo = '042024', @Placa = '', @Programacion = 'TODO', @TipoUnidad = 'TRACTO'
*/
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_BuscarPlanMttoFecha]
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@Placa VARCHAR(30),
@Programacion VARCHAR(30),
@TipoUnidad VARCHAR(30)
AS
DECLARE @FINICIO DATE, @FFIN DATE, @Domingo DATE
DECLARE @Exito VARCHAR(20) = 'Registro Exitoso'
BEGIN
	SET @FINICIO = @FechaInicio
	SET @FFIN = @FechaFin
	SET @Domingo = DATEADD(DAY, 7-DATEPART(DW,@FINICIO),@FINICIO)
	SET @FFIN = DATEADD(DAY,1,@FFIN)

	WHILE @Domingo <= @FFIN BEGIN
		SET @Domingo = DATEADD(DAY,7,@Domingo)
	END

	DECLARE @UltimoDiaMes TINYINT = DATEDIFF(DAY,@FINICIO,@FFIN)

	DELETE FROM ReportesApp_Mantenimiento_MttoPreventivo_PlacasView
	DBCC CHECKIDENT(ReportesApp_Mantenimiento_MttoPreventivo_PlacasView, RESEED, 0)

	IF (@Programacion = 'TODO' AND @TipoUnidad = 'TODOS') BEGIN
		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_PlacasView(Placa,Operacion,TipoUnidad,UltimoMtto,UltimaFecha,ProxMtto,FechaProyectada,DiferenciaKM,Anio,Mes,NroDias)
		(SELECT X.PLACA, X.OPERACION, X.TIPO_UNIDAD, X.TIPO_MTTO, X.FECHA_UM, X.PROXIMO_MTTO,
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		(CASE WHEN X.DIFERENCIA_KM < 0 THEN 0.00 ELSE X.DIFERENCIA_KM END) AS 'DIFERENCIA_KM', YEAR(@FFIN), MONTH(@FFIN), @UltimoDiaMes
		FROM (SELECT MR.idRegistro AS 'NRO', MR.idVehiculo, V.NumeroPlaca AS 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION',
			  MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', V.Modelo AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
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
			  LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
			  LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = MR.idVehiculo
			  WHERE UC.IdProgramacion != 6) X
		WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) BETWEEN @FINICIO AND @FFIN)
		UNION
		(SELECT X.PLACA, 'LOCAL', X.MAQUINA, X.TIPO_MTTO, X.FECHA_UM, X.PROXIMO_MTTO,
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		(CASE WHEN X.DIFERENCIA_KM < 0 THEN 0.00 ELSE X.DIFERENCIA_KM END) AS 'DIFERENCIA_KM', YEAR(@FFIN), MONTH(@FFIN), @UltimoDiaMes
		FROM (SELECT DISTINCT MR.idRegistroM AS 'NRO', LTRIM(RTRIM(MR.MaquinaCodigo)) AS 'PLACA', LTRIM(RTRIM(ISNULL(TG.DescripcionLocal,'UNIDADES TERCERAS'))) AS 'GRUPO', LTRIM(RTRIM(T.DescripcionLocal)) AS 'MAQUINA',
			MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', ISNULL(M.Modelo,UT.Modelo) AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
			MR.TipoMantenimiento AS 'TIPO_MTTO', (SELECT ISNULL(TipoMantenimiento,CASE WHEN MR.Aceite = 'SINTETICO' THEN 'M0' ELSE 'M1' END) FROM ReportesApp_Mantenimiento_MttoPreventivo_TipoMtto WHERE idMantenimientoOP = MR.idMantenimientoOP AND Posicion = MR.PS + 1) AS 'PROXIMO_MTTO',
			KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL', CAST((KM.KMActual - MR.UltimoKM) / MR.Frecuencia * 100 AS DECIMAL(10,2)) AS 'PORCENTAJE (%)',
			CASE WHEN DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) = 0 THEN 1 ELSE
			CAST((KM.KMActual - MR.UltimoKM) / DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA',
			CAST(MR.Frecuencia + MR.UltimoKM AS DECIMAL(10,2)) AS 'KM_ESTIMADO', CAST((MR.Frecuencia + MR.UltimoKM) - KM.KMActual AS DECIMAL(10,2)) AS 'DIFERENCIA_KM',
			MR.Usuario AS 'ULTIMO_USUARIO', MR.FechaCreacion AS 'ULTIMA_FECHA'
			FROM ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas MR
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_UnidadesTerceras UT ON LTRIM(RTRIM(UT.NumeroPlaca)) = LTRIM(RTRIM(MR.MaquinaCodigo))
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo)) AND (M.Estado = 'A')
			LEFT JOIN ME_MaquinaMarca ME ON ISNULL(M.Marca,UT.Marca) = ME.Marca
			LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
			LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo))) X
		WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) BETWEEN @FINICIO AND @FFIN)
		ORDER BY X.PLACA ASC

		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_PlacasView(Placa,Operacion,TipoUnidad,UltimoMtto,UltimaFecha,ProxMtto,FechaProyectada,DiferenciaKM,Anio,Mes,NroDias)
		(SELECT X.PLACA, X.OPERACION, X.TIPO_UNIDAD, X.TIPO_MTTO + ' (E)', X.FECHA_UM, X.PROXIMO_MTTO,
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		(CASE WHEN X.DIFERENCIA_KM < 0 THEN 0.00 ELSE X.DIFERENCIA_KM END) AS 'DIFERENCIA_KM', YEAR(@FFIN), MONTH(@FFIN), @UltimoDiaMes
		FROM (SELECT MR.idRegistro AS 'NRO', MR.idVehiculo, V.NumeroPlaca AS 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION',
			  MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', V.Modelo AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
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
			  LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
			  LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = MR.idVehiculo
			  WHERE UC.IdProgramacion != 6) X
		WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND (X.FECHA_UM BETWEEN @FINICIO AND @FFIN))
		UNION
		(SELECT X.PLACA, 'LOCAL', X.MAQUINA, X.TIPO_MTTO + ' (E)', X.FECHA_UM, X.PROXIMO_MTTO,
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		(CASE WHEN X.DIFERENCIA_KM < 0 THEN 0.00 ELSE X.DIFERENCIA_KM END) AS 'DIFERENCIA_KM', YEAR(@FFIN), MONTH(@FFIN), @UltimoDiaMes
		FROM (SELECT DISTINCT MR.idRegistroM AS 'NRO', LTRIM(RTRIM(MR.MaquinaCodigo)) AS 'PLACA', LTRIM(RTRIM(ISNULL(TG.DescripcionLocal,'UNIDADES TERCERAS'))) AS 'GRUPO', LTRIM(RTRIM(T.DescripcionLocal)) AS 'MAQUINA',
			MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', ISNULL(M.Modelo,UT.Modelo) AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
			MR.TipoMantenimiento AS 'TIPO_MTTO', (SELECT ISNULL(TipoMantenimiento,CASE WHEN MR.Aceite = 'SINTETICO' THEN 'M0' ELSE 'M1' END) FROM ReportesApp_Mantenimiento_MttoPreventivo_TipoMtto WHERE idMantenimientoOP = MR.idMantenimientoOP AND Posicion = MR.PS + 1) AS 'PROXIMO_MTTO',
			KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL', CAST((KM.KMActual - MR.UltimoKM) / MR.Frecuencia * 100 AS DECIMAL(10,2)) AS 'PORCENTAJE (%)',
			CASE WHEN DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) = 0 THEN 1 ELSE
			CAST((KM.KMActual - MR.UltimoKM) / DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA',
			CAST(MR.Frecuencia + MR.UltimoKM AS DECIMAL(10,2)) AS 'KM_ESTIMADO', CAST((MR.Frecuencia + MR.UltimoKM) - KM.KMActual AS DECIMAL(10,2)) AS 'DIFERENCIA_KM',
			MR.Usuario AS 'ULTIMO_USUARIO', MR.FechaCreacion AS 'ULTIMA_FECHA'
			FROM ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas MR
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_UnidadesTerceras UT ON LTRIM(RTRIM(UT.NumeroPlaca)) = LTRIM(RTRIM(MR.MaquinaCodigo))
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo)) AND (M.Estado = 'A')
			LEFT JOIN ME_MaquinaMarca ME ON ISNULL(M.Marca,UT.Marca) = ME.Marca
			LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
			LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo))) X
		WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND (X.FECHA_UM BETWEEN @FINICIO AND @FFIN))
		ORDER BY X.PLACA ASC
	END

	IF (@Programacion != 'TODO' AND @TipoUnidad = 'TODOS') BEGIN
		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_PlacasView(Placa,Operacion,TipoUnidad,UltimoMtto,UltimaFecha,ProxMtto,FechaProyectada,DiferenciaKM,Anio,Mes,NroDias)
		(SELECT X.PLACA, X.OPERACION, X.TIPO_UNIDAD, X.TIPO_MTTO, X.FECHA_UM, X.PROXIMO_MTTO,
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		(CASE WHEN X.DIFERENCIA_KM < 0 THEN 0.00 ELSE X.DIFERENCIA_KM END) AS 'DIFERENCIA_KM', YEAR(@FFIN), MONTH(@FFIN), @UltimoDiaMes
		FROM (SELECT MR.idRegistro AS 'NRO', MR.idVehiculo, V.NumeroPlaca AS 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION',
			  MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', V.Modelo AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
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
			  LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
			  LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = MR.idVehiculo
			  WHERE UC.IdProgramacion != 6) X
		WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) BETWEEN @FINICIO AND @FFIN
		AND X.OPERACION = @Programacion)
		UNION
		(SELECT X.PLACA, 'LOCAL', X.MAQUINA, X.TIPO_MTTO, X.FECHA_UM, X.PROXIMO_MTTO,
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		(CASE WHEN X.DIFERENCIA_KM < 0 THEN 0.00 ELSE X.DIFERENCIA_KM END) AS 'DIFERENCIA_KM', YEAR(@FFIN), MONTH(@FFIN), @UltimoDiaMes
		FROM (SELECT DISTINCT MR.idRegistroM AS 'NRO', LTRIM(RTRIM(MR.MaquinaCodigo)) AS 'PLACA', LTRIM(RTRIM(ISNULL(TG.DescripcionLocal,'UNIDADES TERCERAS'))) AS 'GRUPO', LTRIM(RTRIM(T.DescripcionLocal)) AS 'MAQUINA',
			MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', ISNULL(M.Modelo,UT.Modelo) AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
			MR.TipoMantenimiento AS 'TIPO_MTTO', (SELECT ISNULL(TipoMantenimiento,CASE WHEN MR.Aceite = 'SINTETICO' THEN 'M0' ELSE 'M1' END) FROM ReportesApp_Mantenimiento_MttoPreventivo_TipoMtto WHERE idMantenimientoOP = MR.idMantenimientoOP AND Posicion = MR.PS + 1) AS 'PROXIMO_MTTO',
			KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL', CAST((KM.KMActual - MR.UltimoKM) / MR.Frecuencia * 100 AS DECIMAL(10,2)) AS 'PORCENTAJE (%)',
			CASE WHEN DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) = 0 THEN 1 ELSE
			CAST((KM.KMActual - MR.UltimoKM) / DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA',
			CAST(MR.Frecuencia + MR.UltimoKM AS DECIMAL(10,2)) AS 'KM_ESTIMADO', CAST((MR.Frecuencia + MR.UltimoKM) - KM.KMActual AS DECIMAL(10,2)) AS 'DIFERENCIA_KM',
			MR.Usuario AS 'ULTIMO_USUARIO', MR.FechaCreacion AS 'ULTIMA_FECHA'
			FROM ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas MR
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_UnidadesTerceras UT ON LTRIM(RTRIM(UT.NumeroPlaca)) = LTRIM(RTRIM(MR.MaquinaCodigo))
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo)) AND (M.Estado = 'A')
			LEFT JOIN ME_MaquinaMarca ME ON ISNULL(M.Marca,UT.Marca) = ME.Marca
			LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
			LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo))) X
		WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) BETWEEN @FINICIO AND @FFIN
		AND @Programacion = 'LOCAL')
		ORDER BY X.PLACA ASC

		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_PlacasView(Placa,Operacion,TipoUnidad,UltimoMtto,UltimaFecha,ProxMtto,FechaProyectada,DiferenciaKM,Anio,Mes,NroDias)
		(SELECT X.PLACA, X.OPERACION, X.TIPO_UNIDAD, X.TIPO_MTTO + ' (E)', X.FECHA_UM, X.PROXIMO_MTTO,
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		(CASE WHEN X.DIFERENCIA_KM < 0 THEN 0.00 ELSE X.DIFERENCIA_KM END) AS 'DIFERENCIA_KM', YEAR(@FFIN), MONTH(@FFIN), @UltimoDiaMes
		FROM (SELECT MR.idRegistro AS 'NRO', MR.idVehiculo, V.NumeroPlaca AS 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION',
			  MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', V.Modelo AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
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
			  LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
			  LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = MR.idVehiculo
			  WHERE UC.IdProgramacion != 6) X
		WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND (X.FECHA_UM BETWEEN @FINICIO AND @FFIN) AND (X.OPERACION = @Programacion))
		UNION
		(SELECT X.PLACA, 'LOCAL', X.MAQUINA, X.TIPO_MTTO + ' (E)', X.FECHA_UM, X.PROXIMO_MTTO,
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		(CASE WHEN X.DIFERENCIA_KM < 0 THEN 0.00 ELSE X.DIFERENCIA_KM END) AS 'DIFERENCIA_KM', YEAR(@FFIN), MONTH(@FFIN), @UltimoDiaMes
		FROM (SELECT DISTINCT MR.idRegistroM AS 'NRO', LTRIM(RTRIM(MR.MaquinaCodigo)) AS 'PLACA', LTRIM(RTRIM(ISNULL(TG.DescripcionLocal,'UNIDADES TERCERAS'))) AS 'GRUPO', LTRIM(RTRIM(T.DescripcionLocal)) AS 'MAQUINA',
			MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', ISNULL(M.Modelo,UT.Modelo) AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
			MR.TipoMantenimiento AS 'TIPO_MTTO', (SELECT ISNULL(TipoMantenimiento,CASE WHEN MR.Aceite = 'SINTETICO' THEN 'M0' ELSE 'M1' END) FROM ReportesApp_Mantenimiento_MttoPreventivo_TipoMtto WHERE idMantenimientoOP = MR.idMantenimientoOP AND Posicion = MR.PS + 1) AS 'PROXIMO_MTTO',
			KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL', CAST((KM.KMActual - MR.UltimoKM) / MR.Frecuencia * 100 AS DECIMAL(10,2)) AS 'PORCENTAJE (%)',
			CASE WHEN DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) = 0 THEN 1 ELSE
			CAST((KM.KMActual - MR.UltimoKM) / DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA',
			CAST(MR.Frecuencia + MR.UltimoKM AS DECIMAL(10,2)) AS 'KM_ESTIMADO', CAST((MR.Frecuencia + MR.UltimoKM) - KM.KMActual AS DECIMAL(10,2)) AS 'DIFERENCIA_KM',
			MR.Usuario AS 'ULTIMO_USUARIO', MR.FechaCreacion AS 'ULTIMA_FECHA'
			FROM ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas MR
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_UnidadesTerceras UT ON LTRIM(RTRIM(UT.NumeroPlaca)) = LTRIM(RTRIM(MR.MaquinaCodigo))
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo)) AND (M.Estado = 'A')
			LEFT JOIN ME_MaquinaMarca ME ON ISNULL(M.Marca,UT.Marca) = ME.Marca
			LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
			LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo))) X
		WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND (X.FECHA_UM BETWEEN @FINICIO AND @FFIN) AND (@Programacion = 'LOCAL'))
		ORDER BY X.PLACA ASC
	END

	IF (@Programacion = 'TODO' AND @TipoUnidad != 'TODOS') BEGIN
		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_PlacasView(Placa,Operacion,TipoUnidad,UltimoMtto,UltimaFecha,ProxMtto,FechaProyectada,DiferenciaKM,Anio,Mes,NroDias)
		(SELECT X.PLACA, X.OPERACION, X.TIPO_UNIDAD, X.TIPO_MTTO, X.FECHA_UM, X.PROXIMO_MTTO,
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		(CASE WHEN X.DIFERENCIA_KM < 0 THEN 0.00 ELSE X.DIFERENCIA_KM END) AS 'DIFERENCIA_KM', YEAR(@FFIN), MONTH(@FFIN), @UltimoDiaMes
		FROM (SELECT MR.idRegistro AS 'NRO', MR.idVehiculo, V.NumeroPlaca AS 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION',
			  MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', V.Modelo AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
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
			  LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
			  LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = MR.idVehiculo
			  WHERE UC.IdProgramacion != 6) X
		WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) BETWEEN @FINICIO AND @FFIN
		AND X.TIPO_UNIDAD = @TipoUnidad)
		UNION
		(SELECT X.PLACA, 'LOCAL', X.MAQUINA, X.TIPO_MTTO, X.FECHA_UM, X.PROXIMO_MTTO,
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		(CASE WHEN X.DIFERENCIA_KM < 0 THEN 0.00 ELSE X.DIFERENCIA_KM END) AS 'DIFERENCIA_KM', YEAR(@FFIN), MONTH(@FFIN), @UltimoDiaMes
		FROM (SELECT DISTINCT MR.idRegistroM AS 'NRO', LTRIM(RTRIM(MR.MaquinaCodigo)) AS 'PLACA', LTRIM(RTRIM(ISNULL(TG.DescripcionLocal,'UNIDADES TERCERAS'))) AS 'GRUPO', LTRIM(RTRIM(T.DescripcionLocal)) AS 'MAQUINA',
			MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', ISNULL(M.Modelo,UT.Modelo) AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
			MR.TipoMantenimiento AS 'TIPO_MTTO', (SELECT ISNULL(TipoMantenimiento,CASE WHEN MR.Aceite = 'SINTETICO' THEN 'M0' ELSE 'M1' END) FROM ReportesApp_Mantenimiento_MttoPreventivo_TipoMtto WHERE idMantenimientoOP = MR.idMantenimientoOP AND Posicion = MR.PS + 1) AS 'PROXIMO_MTTO',
			KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL', CAST((KM.KMActual - MR.UltimoKM) / MR.Frecuencia * 100 AS DECIMAL(10,2)) AS 'PORCENTAJE (%)',
			CASE WHEN DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) = 0 THEN 1 ELSE
			CAST((KM.KMActual - MR.UltimoKM) / DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA',
			CAST(MR.Frecuencia + MR.UltimoKM AS DECIMAL(10,2)) AS 'KM_ESTIMADO', CAST((MR.Frecuencia + MR.UltimoKM) - KM.KMActual AS DECIMAL(10,2)) AS 'DIFERENCIA_KM',
			MR.Usuario AS 'ULTIMO_USUARIO', MR.FechaCreacion AS 'ULTIMA_FECHA'
			FROM ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas MR
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_UnidadesTerceras UT ON LTRIM(RTRIM(UT.NumeroPlaca)) = LTRIM(RTRIM(MR.MaquinaCodigo))
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo)) AND (M.Estado = 'A')
			LEFT JOIN ME_MaquinaMarca ME ON ISNULL(M.Marca,UT.Marca) = ME.Marca
			LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
			LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo))) X
		WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) BETWEEN @FINICIO AND @FFIN
		AND X.MAQUINA = @TipoUnidad)
		ORDER BY X.PLACA ASC

		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_PlacasView(Placa,Operacion,TipoUnidad,UltimoMtto,UltimaFecha,ProxMtto,FechaProyectada,DiferenciaKM,Anio,Mes,NroDias)
		(SELECT X.PLACA, X.OPERACION, X.TIPO_UNIDAD, X.TIPO_MTTO + ' (E)', X.FECHA_UM, X.PROXIMO_MTTO,
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		(CASE WHEN X.DIFERENCIA_KM < 0 THEN 0.00 ELSE X.DIFERENCIA_KM END) AS 'DIFERENCIA_KM', YEAR(@FFIN), MONTH(@FFIN), @UltimoDiaMes
		FROM (SELECT MR.idRegistro AS 'NRO', MR.idVehiculo, V.NumeroPlaca AS 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION',
			  MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', V.Modelo AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
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
			  LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
			  LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = MR.idVehiculo
			  WHERE UC.IdProgramacion != 6) X
		WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND (X.FECHA_UM BETWEEN @FINICIO AND @FFIN) AND X.TIPO_UNIDAD = @TipoUnidad)
		UNION
		(SELECT X.PLACA, 'LOCAL', X.MAQUINA, X.TIPO_MTTO + ' (E)', X.FECHA_UM, X.PROXIMO_MTTO,
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		(CASE WHEN X.DIFERENCIA_KM < 0 THEN 0.00 ELSE X.DIFERENCIA_KM END) AS 'DIFERENCIA_KM', YEAR(@FFIN), MONTH(@FFIN), @UltimoDiaMes
		FROM (SELECT DISTINCT MR.idRegistroM AS 'NRO', LTRIM(RTRIM(MR.MaquinaCodigo)) AS 'PLACA', LTRIM(RTRIM(ISNULL(TG.DescripcionLocal,'UNIDADES TERCERAS'))) AS 'GRUPO', LTRIM(RTRIM(T.DescripcionLocal)) AS 'MAQUINA',
			MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', ISNULL(M.Modelo,UT.Modelo) AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
			MR.TipoMantenimiento AS 'TIPO_MTTO', (SELECT ISNULL(TipoMantenimiento,CASE WHEN MR.Aceite = 'SINTETICO' THEN 'M0' ELSE 'M1' END) FROM ReportesApp_Mantenimiento_MttoPreventivo_TipoMtto WHERE idMantenimientoOP = MR.idMantenimientoOP AND Posicion = MR.PS + 1) AS 'PROXIMO_MTTO',
			KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL', CAST((KM.KMActual - MR.UltimoKM) / MR.Frecuencia * 100 AS DECIMAL(10,2)) AS 'PORCENTAJE (%)',
			CASE WHEN DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) = 0 THEN 1 ELSE
			CAST((KM.KMActual - MR.UltimoKM) / DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA',
			CAST(MR.Frecuencia + MR.UltimoKM AS DECIMAL(10,2)) AS 'KM_ESTIMADO', CAST((MR.Frecuencia + MR.UltimoKM) - KM.KMActual AS DECIMAL(10,2)) AS 'DIFERENCIA_KM',
			MR.Usuario AS 'ULTIMO_USUARIO', MR.FechaCreacion AS 'ULTIMA_FECHA'
			FROM ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas MR
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_UnidadesTerceras UT ON LTRIM(RTRIM(UT.NumeroPlaca)) = LTRIM(RTRIM(MR.MaquinaCodigo))
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo)) AND (M.Estado = 'A')
			LEFT JOIN ME_MaquinaMarca ME ON ISNULL(M.Marca,UT.Marca) = ME.Marca
			LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
			LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo))) X
		WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND (X.FECHA_UM BETWEEN @FINICIO AND @FFIN) AND X.MAQUINA = @TipoUnidad)
		ORDER BY X.PLACA ASC
	END

	IF (@Programacion != 'TODO' AND @TipoUnidad != 'TODOS') BEGIN
		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_PlacasView(Placa,Operacion,TipoUnidad,UltimoMtto,UltimaFecha,ProxMtto,FechaProyectada,DiferenciaKM,Anio,Mes,NroDias)
		(SELECT X.PLACA, X.OPERACION, X.TIPO_UNIDAD, X.TIPO_MTTO, X.FECHA_UM, X.PROXIMO_MTTO,
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		(CASE WHEN X.DIFERENCIA_KM < 0 THEN 0.00 ELSE X.DIFERENCIA_KM END) AS 'DIFERENCIA_KM', YEAR(@FFIN), MONTH(@FFIN), @UltimoDiaMes
		FROM (SELECT MR.idRegistro AS 'NRO', MR.idVehiculo, V.NumeroPlaca AS 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION',
			  MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', V.Modelo AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
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
			  LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
			  LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = MR.idVehiculo
			  WHERE UC.IdProgramacion != 6) X
		WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) BETWEEN @FINICIO AND @FFIN
		AND X.OPERACION = @Programacion AND X.TIPO_UNIDAD = @TipoUnidad)
		UNION
		(SELECT X.PLACA, 'LOCAL', X.MAQUINA, X.TIPO_MTTO, X.FECHA_UM, X.PROXIMO_MTTO,
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		(CASE WHEN X.DIFERENCIA_KM < 0 THEN 0.00 ELSE X.DIFERENCIA_KM END) AS 'DIFERENCIA_KM', YEAR(@FFIN), MONTH(@FFIN), @UltimoDiaMes
		FROM (SELECT DISTINCT MR.idRegistroM AS 'NRO', LTRIM(RTRIM(MR.MaquinaCodigo)) AS 'PLACA', LTRIM(RTRIM(ISNULL(TG.DescripcionLocal,'UNIDADES TERCERAS'))) AS 'GRUPO', LTRIM(RTRIM(T.DescripcionLocal)) AS 'MAQUINA',
			MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', ISNULL(M.Modelo,UT.Modelo) AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
			MR.TipoMantenimiento AS 'TIPO_MTTO', (SELECT ISNULL(TipoMantenimiento,CASE WHEN MR.Aceite = 'SINTETICO' THEN 'M0' ELSE 'M1' END) FROM ReportesApp_Mantenimiento_MttoPreventivo_TipoMtto WHERE idMantenimientoOP = MR.idMantenimientoOP AND Posicion = MR.PS + 1) AS 'PROXIMO_MTTO',
			KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL', CAST((KM.KMActual - MR.UltimoKM) / MR.Frecuencia * 100 AS DECIMAL(10,2)) AS 'PORCENTAJE (%)',
			CASE WHEN DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) = 0 THEN 1 ELSE
			CAST((KM.KMActual - MR.UltimoKM) / DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA',
			CAST(MR.Frecuencia + MR.UltimoKM AS DECIMAL(10,2)) AS 'KM_ESTIMADO', CAST((MR.Frecuencia + MR.UltimoKM) - KM.KMActual AS DECIMAL(10,2)) AS 'DIFERENCIA_KM',
			MR.Usuario AS 'ULTIMO_USUARIO', MR.FechaCreacion AS 'ULTIMA_FECHA'
			FROM ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas MR
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_UnidadesTerceras UT ON LTRIM(RTRIM(UT.NumeroPlaca)) = LTRIM(RTRIM(MR.MaquinaCodigo))
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo)) AND (M.Estado = 'A')
			LEFT JOIN ME_MaquinaMarca ME ON ISNULL(M.Marca,UT.Marca) = ME.Marca
			LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
			LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo))) X
		WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) BETWEEN @FINICIO AND @FFIN
		AND @Programacion = 'LOCAL' AND X.MAQUINA = @TipoUnidad)
		ORDER BY X.PLACA ASC

		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_PlacasView(Placa,Operacion,TipoUnidad,UltimoMtto,UltimaFecha,ProxMtto,FechaProyectada,DiferenciaKM,Anio,Mes,NroDias)
		(SELECT X.PLACA, X.OPERACION, X.TIPO_UNIDAD, X.TIPO_MTTO + ' (E)', X.FECHA_UM, X.PROXIMO_MTTO,
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		(CASE WHEN X.DIFERENCIA_KM < 0 THEN 0.00 ELSE X.DIFERENCIA_KM END) AS 'DIFERENCIA_KM', YEAR(@FFIN), MONTH(@FFIN), @UltimoDiaMes
		FROM (SELECT MR.idRegistro AS 'NRO', MR.idVehiculo, V.NumeroPlaca AS 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION',
			  MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', V.Modelo AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
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
			  LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
			  LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = MR.idVehiculo
			  WHERE UC.IdProgramacion != 6) X
		WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND (X.FECHA_UM BETWEEN @FINICIO AND @FFIN) AND X.OPERACION = @Programacion AND X.TIPO_UNIDAD = @TipoUnidad)
		UNION
		(SELECT X.PLACA, 'LOCAL', X.MAQUINA, X.TIPO_MTTO + ' (E)', X.FECHA_UM, X.PROXIMO_MTTO,
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA',
		(CASE WHEN X.DIFERENCIA_KM < 0 THEN 0.00 ELSE X.DIFERENCIA_KM END) AS 'DIFERENCIA_KM', YEAR(@FFIN), MONTH(@FFIN), @UltimoDiaMes
		FROM (SELECT DISTINCT MR.idRegistroM AS 'NRO', LTRIM(RTRIM(MR.MaquinaCodigo)) AS 'PLACA', LTRIM(RTRIM(ISNULL(TG.DescripcionLocal,'UNIDADES TERCERAS'))) AS 'GRUPO', LTRIM(RTRIM(T.DescripcionLocal)) AS 'MAQUINA',
			MR.Aceite AS 'ACEITE', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', ISNULL(M.Modelo,UT.Modelo) AS 'MODELO', MR.Frecuencia AS 'FREC/KM', MR.UltimaFecha AS 'FECHA_UM', MR.UltimoKM AS 'KM_ANTERIOR', MR.idMantenimientoOP, MR.PS,
			MR.TipoMantenimiento AS 'TIPO_MTTO', (SELECT ISNULL(TipoMantenimiento,CASE WHEN MR.Aceite = 'SINTETICO' THEN 'M0' ELSE 'M1' END) FROM ReportesApp_Mantenimiento_MttoPreventivo_TipoMtto WHERE idMantenimientoOP = MR.idMantenimientoOP AND Posicion = MR.PS + 1) AS 'PROXIMO_MTTO',
			KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL', CAST((KM.KMActual - MR.UltimoKM) / MR.Frecuencia * 100 AS DECIMAL(10,2)) AS 'PORCENTAJE (%)',
			CASE WHEN DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) = 0 THEN 1 ELSE
			CAST((KM.KMActual - MR.UltimoKM) / DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA',
			CAST(MR.Frecuencia + MR.UltimoKM AS DECIMAL(10,2)) AS 'KM_ESTIMADO', CAST((MR.Frecuencia + MR.UltimoKM) - KM.KMActual AS DECIMAL(10,2)) AS 'DIFERENCIA_KM',
			MR.Usuario AS 'ULTIMO_USUARIO', MR.FechaCreacion AS 'ULTIMA_FECHA'
			FROM ReportesApp_Mantenimiento_MttoPreventivo_RegistroMaquinas MR
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_UnidadesTerceras UT ON LTRIM(RTRIM(UT.NumeroPlaca)) = LTRIM(RTRIM(MR.MaquinaCodigo))
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo)) AND (M.Estado = 'A')
			LEFT JOIN ME_MaquinaMarca ME ON ISNULL(M.Marca,UT.Marca) = ME.Marca
			LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
			LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(MR.MaquinaCodigo))) X
		WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND (X.FECHA_UM BETWEEN @FINICIO AND @FFIN) AND @Programacion = 'LOCAL' AND X.MAQUINA = @TipoUnidad)
		ORDER BY X.PLACA ASC
	END

	DECLARE @Nro INT = 1
	WHILE (@Nro <= (SELECT MAX(Nro) FROM ReportesApp_Mantenimiento_MttoPreventivo_PlacasView)) BEGIN
		DECLARE @x_ProxMtto VARCHAR(20) = (SELECT ProxMtto FROM ReportesApp_Mantenimiento_MttoPreventivo_PlacasView WHERE Nro = @Nro)
		DECLARE @x_FechaSelec DATETIME = (SELECT FechaProyectada FROM ReportesApp_Mantenimiento_MttoPreventivo_PlacasView WHERE Nro = @Nro)

		DECLARE @x_UltimoMtto VARCHAR(20) = (SELECT UltimoMtto FROM ReportesApp_Mantenimiento_MttoPreventivo_PlacasView WHERE Nro = @Nro)
		DECLARE @x_UltimaFecha DATETIME = (SELECT UltimaFecha FROM ReportesApp_Mantenimiento_MttoPreventivo_PlacasView WHERE Nro = @Nro)

		UPDATE P
		SET P.D1 = CASE WHEN DAY(@x_FechaSelec) = 1 AND (@x_FechaSelec BETWEEN @FINICIO AND @FFIN) THEN @x_ProxMtto ELSE P.D1 END,
			P.D2 = CASE WHEN DAY(@x_FechaSelec) = 2 AND (@x_FechaSelec BETWEEN @FINICIO AND @FFIN) THEN @x_ProxMtto ELSE P.D2 END,
			P.D3 = CASE WHEN DAY(@x_FechaSelec) = 3 AND (@x_FechaSelec BETWEEN @FINICIO AND @FFIN) THEN @x_ProxMtto ELSE P.D3 END,
			P.D4 = CASE WHEN DAY(@x_FechaSelec) = 4 AND (@x_FechaSelec BETWEEN @FINICIO AND @FFIN) THEN @x_ProxMtto ELSE P.D4 END,
			P.D5 = CASE WHEN DAY(@x_FechaSelec) = 5 AND (@x_FechaSelec BETWEEN @FINICIO AND @FFIN) THEN @x_ProxMtto ELSE P.D5 END,
			P.D6 = CASE WHEN DAY(@x_FechaSelec) = 6 AND (@x_FechaSelec BETWEEN @FINICIO AND @FFIN) THEN @x_ProxMtto ELSE P.D6 END,
			P.D7 = CASE WHEN DAY(@x_FechaSelec) = 7 AND (@x_FechaSelec BETWEEN @FINICIO AND @FFIN) THEN @x_ProxMtto ELSE P.D7 END,
			P.D8 = CASE WHEN DAY(@x_FechaSelec) = 8 AND (@x_FechaSelec BETWEEN @FINICIO AND @FFIN) THEN @x_ProxMtto ELSE P.D8 END,
			P.D9 = CASE WHEN DAY(@x_FechaSelec) = 9 AND (@x_FechaSelec BETWEEN @FINICIO AND @FFIN) THEN @x_ProxMtto ELSE P.D9 END,
			P.D10 = CASE WHEN DAY(@x_FechaSelec) = 10 AND (@x_FechaSelec BETWEEN @FINICIO AND @FFIN) THEN @x_ProxMtto ELSE P.D10 END,
			P.D11 = CASE WHEN DAY(@x_FechaSelec) = 11 AND (@x_FechaSelec BETWEEN @FINICIO AND @FFIN) THEN @x_ProxMtto ELSE P.D11 END,
			P.D12 = CASE WHEN DAY(@x_FechaSelec) = 12 AND (@x_FechaSelec BETWEEN @FINICIO AND @FFIN) THEN @x_ProxMtto ELSE P.D12 END,
			P.D13 = CASE WHEN DAY(@x_FechaSelec) = 13 AND (@x_FechaSelec BETWEEN @FINICIO AND @FFIN) THEN @x_ProxMtto ELSE P.D13 END,
			P.D14 = CASE WHEN DAY(@x_FechaSelec) = 14 AND (@x_FechaSelec BETWEEN @FINICIO AND @FFIN) THEN @x_ProxMtto ELSE P.D14 END,
			P.D15 = CASE WHEN DAY(@x_FechaSelec) = 15 AND (@x_FechaSelec BETWEEN @FINICIO AND @FFIN) THEN @x_ProxMtto ELSE P.D15 END,
			P.D16 = CASE WHEN DAY(@x_FechaSelec) = 16 AND (@x_FechaSelec BETWEEN @FINICIO AND @FFIN) THEN @x_ProxMtto ELSE P.D16 END,
			P.D17 = CASE WHEN DAY(@x_FechaSelec) = 17 AND (@x_FechaSelec BETWEEN @FINICIO AND @FFIN) THEN @x_ProxMtto ELSE P.D17 END,
			P.D18 = CASE WHEN DAY(@x_FechaSelec) = 18 AND (@x_FechaSelec BETWEEN @FINICIO AND @FFIN) THEN @x_ProxMtto ELSE P.D18 END,
			P.D19 = CASE WHEN DAY(@x_FechaSelec) = 19 AND (@x_FechaSelec BETWEEN @FINICIO AND @FFIN) THEN @x_ProxMtto ELSE P.D19 END,
			P.D20 = CASE WHEN DAY(@x_FechaSelec) = 20 AND (@x_FechaSelec BETWEEN @FINICIO AND @FFIN) THEN @x_ProxMtto ELSE P.D20 END,
			P.D21 = CASE WHEN DAY(@x_FechaSelec) = 21 AND (@x_FechaSelec BETWEEN @FINICIO AND @FFIN) THEN @x_ProxMtto ELSE P.D21 END,
			P.D22 = CASE WHEN DAY(@x_FechaSelec) = 22 AND (@x_FechaSelec BETWEEN @FINICIO AND @FFIN) THEN @x_ProxMtto ELSE P.D22 END,
			P.D23 = CASE WHEN DAY(@x_FechaSelec) = 23 AND (@x_FechaSelec BETWEEN @FINICIO AND @FFIN) THEN @x_ProxMtto ELSE P.D23 END,
			P.D24 = CASE WHEN DAY(@x_FechaSelec) = 24 AND (@x_FechaSelec BETWEEN @FINICIO AND @FFIN) THEN @x_ProxMtto ELSE P.D24 END,
			P.D25 = CASE WHEN DAY(@x_FechaSelec) = 25 AND (@x_FechaSelec BETWEEN @FINICIO AND @FFIN) THEN @x_ProxMtto ELSE P.D25 END,
			P.D26 = CASE WHEN DAY(@x_FechaSelec) = 26 AND (@x_FechaSelec BETWEEN @FINICIO AND @FFIN) THEN @x_ProxMtto ELSE P.D26 END,
			P.D27 = CASE WHEN DAY(@x_FechaSelec) = 27 AND (@x_FechaSelec BETWEEN @FINICIO AND @FFIN) THEN @x_ProxMtto ELSE P.D27 END,
			P.D28 = CASE WHEN DAY(@x_FechaSelec) = 28 AND (@x_FechaSelec BETWEEN @FINICIO AND @FFIN) THEN @x_ProxMtto ELSE P.D28 END,
			P.D29 = CASE WHEN DAY(@x_FechaSelec) = 29 AND (@x_FechaSelec BETWEEN @FINICIO AND @FFIN) THEN @x_ProxMtto ELSE P.D29 END,
			P.D30 = CASE WHEN DAY(@x_FechaSelec) = 30 AND (@x_FechaSelec BETWEEN @FINICIO AND @FFIN) THEN @x_ProxMtto ELSE P.D30 END,
			P.D31 = CASE WHEN DAY(@x_FechaSelec) = 31 AND (@x_FechaSelec BETWEEN @FINICIO AND @FFIN) THEN @x_ProxMtto ELSE P.D31 END
			FROM ReportesApp_Mantenimiento_MttoPreventivo_PlacasView P
			WHERE P.Nro = @Nro
		
		UPDATE P
		SET P.D1 = CASE WHEN DAY(@x_UltimaFecha) = 1 AND (@x_UltimaFecha BETWEEN @FINICIO AND @FFIN) THEN @x_UltimoMtto ELSE P.D1 END,
			P.D2 = CASE WHEN DAY(@x_UltimaFecha) = 2 AND (@x_UltimaFecha BETWEEN @FINICIO AND @FFIN) THEN @x_UltimoMtto ELSE P.D2 END,
			P.D3 = CASE WHEN DAY(@x_UltimaFecha) = 3 AND (@x_UltimaFecha BETWEEN @FINICIO AND @FFIN) THEN @x_UltimoMtto ELSE P.D3 END,
			P.D4 = CASE WHEN DAY(@x_UltimaFecha) = 4 AND (@x_UltimaFecha BETWEEN @FINICIO AND @FFIN) THEN @x_UltimoMtto ELSE P.D4 END,
			P.D5 = CASE WHEN DAY(@x_UltimaFecha) = 5 AND (@x_UltimaFecha BETWEEN @FINICIO AND @FFIN) THEN @x_UltimoMtto ELSE P.D5 END,
			P.D6 = CASE WHEN DAY(@x_UltimaFecha) = 6 AND (@x_UltimaFecha BETWEEN @FINICIO AND @FFIN) THEN @x_UltimoMtto ELSE P.D6 END,
			P.D7 = CASE WHEN DAY(@x_UltimaFecha) = 7 AND (@x_UltimaFecha BETWEEN @FINICIO AND @FFIN) THEN @x_UltimoMtto ELSE P.D7 END,
			P.D8 = CASE WHEN DAY(@x_UltimaFecha) = 8 AND (@x_UltimaFecha BETWEEN @FINICIO AND @FFIN) THEN @x_UltimoMtto ELSE P.D8 END,
			P.D9 = CASE WHEN DAY(@x_UltimaFecha) = 9 AND (@x_UltimaFecha BETWEEN @FINICIO AND @FFIN) THEN @x_UltimoMtto ELSE P.D9 END,
			P.D10 = CASE WHEN DAY(@x_UltimaFecha) = 10 AND (@x_UltimaFecha BETWEEN @FINICIO AND @FFIN) THEN @x_UltimoMtto ELSE P.D10 END,
			P.D11 = CASE WHEN DAY(@x_UltimaFecha) = 11 AND (@x_UltimaFecha BETWEEN @FINICIO AND @FFIN) THEN @x_UltimoMtto ELSE P.D11 END,
			P.D12 = CASE WHEN DAY(@x_UltimaFecha) = 12 AND (@x_UltimaFecha BETWEEN @FINICIO AND @FFIN) THEN @x_UltimoMtto ELSE P.D12 END,
			P.D13 = CASE WHEN DAY(@x_UltimaFecha) = 13 AND (@x_UltimaFecha BETWEEN @FINICIO AND @FFIN) THEN @x_UltimoMtto ELSE P.D13 END,
			P.D14 = CASE WHEN DAY(@x_UltimaFecha) = 14 AND (@x_UltimaFecha BETWEEN @FINICIO AND @FFIN) THEN @x_UltimoMtto ELSE P.D14 END,
			P.D15 = CASE WHEN DAY(@x_UltimaFecha) = 15 AND (@x_UltimaFecha BETWEEN @FINICIO AND @FFIN) THEN @x_UltimoMtto ELSE P.D15 END,
			P.D16 = CASE WHEN DAY(@x_UltimaFecha) = 16 AND (@x_UltimaFecha BETWEEN @FINICIO AND @FFIN) THEN @x_UltimoMtto ELSE P.D16 END,
			P.D17 = CASE WHEN DAY(@x_UltimaFecha) = 17 AND (@x_UltimaFecha BETWEEN @FINICIO AND @FFIN) THEN @x_UltimoMtto ELSE P.D17 END,
			P.D18 = CASE WHEN DAY(@x_UltimaFecha) = 18 AND (@x_UltimaFecha BETWEEN @FINICIO AND @FFIN) THEN @x_UltimoMtto ELSE P.D18 END,
			P.D19 = CASE WHEN DAY(@x_UltimaFecha) = 19 AND (@x_UltimaFecha BETWEEN @FINICIO AND @FFIN) THEN @x_UltimoMtto ELSE P.D19 END,
			P.D20 = CASE WHEN DAY(@x_UltimaFecha) = 20 AND (@x_UltimaFecha BETWEEN @FINICIO AND @FFIN) THEN @x_UltimoMtto ELSE P.D20 END,
			P.D21 = CASE WHEN DAY(@x_UltimaFecha) = 21 AND (@x_UltimaFecha BETWEEN @FINICIO AND @FFIN) THEN @x_UltimoMtto ELSE P.D21 END,
			P.D22 = CASE WHEN DAY(@x_UltimaFecha) = 22 AND (@x_UltimaFecha BETWEEN @FINICIO AND @FFIN) THEN @x_UltimoMtto ELSE P.D22 END,
			P.D23 = CASE WHEN DAY(@x_UltimaFecha) = 23 AND (@x_UltimaFecha BETWEEN @FINICIO AND @FFIN) THEN @x_UltimoMtto ELSE P.D23 END,
			P.D24 = CASE WHEN DAY(@x_UltimaFecha) = 24 AND (@x_UltimaFecha BETWEEN @FINICIO AND @FFIN) THEN @x_UltimoMtto ELSE P.D24 END,
			P.D25 = CASE WHEN DAY(@x_UltimaFecha) = 25 AND (@x_UltimaFecha BETWEEN @FINICIO AND @FFIN) THEN @x_UltimoMtto ELSE P.D25 END,
			P.D26 = CASE WHEN DAY(@x_UltimaFecha) = 26 AND (@x_UltimaFecha BETWEEN @FINICIO AND @FFIN) THEN @x_UltimoMtto ELSE P.D26 END,
			P.D27 = CASE WHEN DAY(@x_UltimaFecha) = 27 AND (@x_UltimaFecha BETWEEN @FINICIO AND @FFIN) THEN @x_UltimoMtto ELSE P.D27 END,
			P.D28 = CASE WHEN DAY(@x_UltimaFecha) = 28 AND (@x_UltimaFecha BETWEEN @FINICIO AND @FFIN) THEN @x_UltimoMtto ELSE P.D28 END,
			P.D29 = CASE WHEN DAY(@x_UltimaFecha) = 29 AND (@x_UltimaFecha BETWEEN @FINICIO AND @FFIN) THEN @x_UltimoMtto ELSE P.D29 END,
			P.D30 = CASE WHEN DAY(@x_UltimaFecha) = 30 AND (@x_UltimaFecha BETWEEN @FINICIO AND @FFIN) THEN @x_UltimoMtto ELSE P.D30 END,
			P.D31 = CASE WHEN DAY(@x_UltimaFecha) = 31 AND (@x_UltimaFecha BETWEEN @FINICIO AND @FFIN) THEN @x_UltimoMtto ELSE P.D31 END
			FROM ReportesApp_Mantenimiento_MttoPreventivo_PlacasView P
			WHERE P.Nro = @Nro

		SET @Nro = @Nro + 1
	END

	SELECT @Exito exito
END

-------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 09-08-2024
-- Description:	CONTADOR DE UNIDADES EN MTTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ContarPlanMtto]
@Periodo VARCHAR(6)
AS
BEGIN
	DECLARE @TotalMEjecutados INT = (SELECT COUNT(*) FROM ReportesApp_Mantenimiento_MttoPreventivo_PlacasView WHERE UltimoMtto LIKE '%(E)%')
	DECLARE @TotalMPendientes INT = (SELECT COUNT(*) FROM ReportesApp_Mantenimiento_MttoPreventivo_PlacasView WHERE
									 YEAR(FechaProyectada) = Anio AND MONTH(FechaProyectada) = Mes)

	SELECT @TotalMPendientes AS 'PENDIENTES', @TotalMEjecutados AS 'EJECUTADOS'
END

-------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 09-08-2024
-- Description:	CONTADOR DE UNIDADES EN MTTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ContarPlanMttoFechas]
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11)
AS
BEGIN
	DECLARE @FINICIO DATETIME, @FFIN DATETIME

	SET @FINICIO = @FechaInicio
	SET @FFIN = @FechaFin + ' 23:59'

	DECLARE @TotalMEjecutados INT = (SELECT COUNT(*) FROM ReportesApp_Mantenimiento_MttoPreventivo_PlacasView WHERE UltimoMtto LIKE '%(E)%')
	DECLARE @TotalMPendientes INT = (SELECT COUNT(*) FROM ReportesApp_Mantenimiento_MttoPreventivo_PlacasView WHERE FechaProyectada BETWEEN @FINICIO AND @FFIN)

	SELECT @TotalMPendientes AS 'PENDIENTES', @TotalMEjecutados AS 'EJECUTADOS'
END

-------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 23-05-2024
-- Description: LISTAR PLAN DE MTTO
-- =============================================
/*
EXEC ReportesApp_Mantenimiento_MttoPreventivo_ListarPlanMtto @Periodo = '052024'
*/
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarPlanMtto]
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

	DECLARE @T_Prueba TABLE (Nro VARCHAR(20), Placa VARCHAR(50), Operacion VARCHAR(50), TipoUnidad VARCHAR(250), UltimaFecha VARCHAR(30),
							 DiferenciaKM VARCHAR(30), D1 VARCHAR(15), D2 VARCHAR(15), D3 VARCHAR(15), D4 VARCHAR(15), D5 VARCHAR(15), D6 VARCHAR(15),
							 D7 VARCHAR(15), D8 VARCHAR(15), D9 VARCHAR(15), D10 VARCHAR(15), D11 VARCHAR(15), D12 VARCHAR(15),
							 D13 VARCHAR(15), D14 VARCHAR(15), D15 VARCHAR(15), D16 VARCHAR(15), D17 VARCHAR(15), D18 VARCHAR(15),
							 D19 VARCHAR(15), D20 VARCHAR(15), D21 VARCHAR(15), D22 VARCHAR(15), D23 VARCHAR(15), D24 VARCHAR(15),
							 D25 VARCHAR(15), D26 VARCHAR(15), D27 VARCHAR(15), D28 VARCHAR(15), D29 VARCHAR(15), D30 VARCHAR(15), D31 VARCHAR(15))

	DECLARE @D1 VARCHAR(15), @D2 VARCHAR(15), @D3 VARCHAR(15), @D4 VARCHAR(15), @D5 VARCHAR(15), @D6 VARCHAR(15), @D7 VARCHAR(15), @D8 VARCHAR(15),
			@D9 VARCHAR(15), @D10 VARCHAR(15), @D11 VARCHAR(15), @D12 VARCHAR(15), @D13 VARCHAR(15), @D14 VARCHAR(15), @D15 VARCHAR(15), @D16 VARCHAR(15),
			@D17 VARCHAR(15), @D18 VARCHAR(15), @D19 VARCHAR(15), @D20 VARCHAR(15), @D21 VARCHAR(15), @D22 VARCHAR(15), @D23 VARCHAR(15), @D24 VARCHAR(15),
			@D25 VARCHAR(15), @D26 VARCHAR(15), @D27 VARCHAR(15), @D28 VARCHAR(15), @D29 VARCHAR(15), @D30 VARCHAR(15), @D31 VARCHAR(15)

	INSERT INTO @T_Prueba (Nro, Placa, Operacion, TipoUnidad, UltimaFecha, DiferenciaKM)
	VALUES ('Nro', '<PLACA>', 'Operacion', 'TipoUnidad', 'UltimaFecha', 'DiferenciaKM')

	DECLARE @FechaIni DATE, @FechaFin DATE, @FechaVer DATE

	IF (@FDesde IS NOT NULL) BEGIN 
		SET @FechaVer = CAST(CAST(@DiaInicio AS VARCHAR(2))+'/'+ RIGHT('0'+ CAST(month(@FDesde) AS VARCHAR(2)),2)+'/'+CAST(@Anio as VARCHAR(4)) AS DATE)
	END
	ELSE BEGIN
		SET @FechaVer = CAST(CAST(@DiaInicio AS VARCHAR(2))+'/'+ RIGHT('0'+ CAST(@Mes AS VARCHAR(2)),2)+'/'+CAST(@Anio as VARCHAR(4)) AS DATE)
	END

	SET @FechaIni = @FechaVer
	SET @FechaFin = DATEADD(D,-1, DATEADD(MONTH,1,@FechaVer))

	INSERT INTO @T_Prueba (Nro, Placa, Operacion, TipoUnidad, UltimaFecha, DiferenciaKM)
	SELECT CONVERT(VARCHAR,MAX(Nro)), Placa, Operacion, TipoUnidad, CONVERT(VARCHAR,UltimaFecha,103), DiferenciaKM
	FROM ReportesApp_Mantenimiento_MttoPreventivo_PlacasView
	GROUP BY Placa, Operacion, TipoUnidad, CONVERT(VARCHAR,UltimaFecha,103), DiferenciaKM

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
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_PlacasView AS A WITH(NOLOCK) ON CONVERT(VARCHAR,T.Nro) = CONVERT(VARCHAR,A.Nro)
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

	CREATE TABLE #PruebaView (Nro VARCHAR(20), Placa VARCHAR(50), Operacion VARCHAR(50), TipoUnidad VARCHAR(250), UltimaFecha VARCHAR(30),
							  DiferenciaKM VARCHAR(30), D1 VARCHAR(15),D2 VARCHAR(15),D3 VARCHAR(15),D4 VARCHAR(15),D5 VARCHAR(15),
							  D6 VARCHAR(15),D7 VARCHAR(15),D8 VARCHAR(15),D9 VARCHAR(15),D10 VARCHAR(15),
							  D11 VARCHAR(15),D12 VARCHAR(15),D13 VARCHAR(15),D14 VARCHAR(15),D15 VARCHAR(15),
							  D16 VARCHAR(15),D17 VARCHAR(15),D18 VARCHAR(15),D19 VARCHAR(15),D20 VARCHAR(15),
							  D21 VARCHAR(15),D22 VARCHAR(15),D23 VARCHAR(15),D24 VARCHAR(15),D25 VARCHAR(15),
							  D26 VARCHAR(15),D27 VARCHAR(15),D28 VARCHAR(15),D29 VARCHAR(15),D30 VARCHAR(15),
							  D31 VARCHAR(15))

	DECLARE @SQL VARCHAR(5000)

	INSERT INTO #PruebaView
	SELECT * FROM @T_Prueba WHERE Placa <> '<PLACA>'

	IF @CantDias = 28 BEGIN
		SET @SQL='SELECT Nro, Placa AS PLACA, Operacion AS OPERACION, TipoUnidad AS TIPO_UNIDAD, DiferenciaKM AS KM_DIFERENCIA, UltimaFecha AS ULTIMA_FECHA,
			D1 AS '+@D1+',D2 AS '+@D2+',D3 AS '+@D3+',D4 AS '+@D4+',D5 AS '+@D5+',D6 AS '+@D6+',D7 AS '+@D7+',D8 AS '+@D8+
		    ',D9 AS '+@D9+',D10 AS '+@D10+',D11 AS '+@D11+',D12 AS '+@D12+',D13 AS '+@D13+',D14 AS '+@D14+',D15 AS '+@D15+
			',D16 AS '+@D16+',D17 AS '+@D17+',D18 AS '+@D18+',D19 AS '+@D19+',D20 AS '+@D20+',D21 AS '+@D21+',D22 AS '+@D22+
			',D23 AS '+@D23+',D24 AS '+@D24+',D25 AS '+@D25+',D26 AS '+@D26+',D27 AS '+@D27+',D28 AS '+@D28+' FROM #PruebaView ORDER BY Placa'
		
		EXEC (@SQL)
		DROP TABLE #PruebaView
	END

	IF @CantDias = 29 BEGIN
		SET @SQL='SELECT Nro, Placa AS PLACA, Operacion AS OPERACION, TipoUnidad AS TIPO_UNIDAD, DiferenciaKM AS KM_DIFERENCIA, UltimaFecha AS ULTIMA_FECHA,
			D1 AS '+@D1+',D2 AS '+@D2+',D3 AS '+@D3+',D4 AS '+@D4+',D5 AS '+@D5+',D6 AS '+@D6+',D7 AS '+@D7+',D8 AS '+@D8+
			',D9 AS '+@D9+',D10 AS '+@D10+',D11 AS '+@D11+',D12 AS '+@D12+',D13 AS '+@D13+',D14 AS '+@D14+',D15 AS '+@D15+
			',D16 AS '+@D16+',D17 AS '+@D17+',D18 AS '+@D18+',D19 AS '+@D19+',D20 AS '+@D20+',D21 AS '+@D21+',D22 AS '+@D22+
			',D23 AS '+@D23+',D24 AS '+@D24+',D25 AS '+@D25+',D26 AS '+@D26+',D27 AS '+@D27+',D28 AS '+@D28+',D29 AS '+@D29+' FROM #PruebaView ORDER BY Placa'
		
		EXEC (@SQL)
		DROP TABLE #PruebaView	
	END

	IF @CantDias = 30 BEGIN
		SET @SQL='SELECT Nro, Placa AS PLACA, Operacion AS OPERACION, TipoUnidad AS TIPO_UNIDAD, DiferenciaKM AS KM_DIFERENCIA, UltimaFecha AS ULTIMA_FECHA,
			D1 AS '+@D1+',D2 AS '+@D2+',D3 AS '+@D3+',D4 AS '+@D4+',D5 AS '+@D5+',D6 AS '+@D6+',D7 AS '+@D7+',D8 AS '+@D8+
			',D9 AS '+@D9+',D10 AS '+@D10+',D11 AS '+@D11+',D12 AS '+@D12+',D13 AS '+@D13+',D14 AS '+@D14+',D15 AS '+@D15+
			',D16 AS '+@D16+',D17 AS '+@D17+',D18 AS '+@D18+',D19 AS '+@D19+',D20 AS '+@D20+',D21 AS '+@D21+',D22 AS '+@D22+
			',D23 AS '+@D23+',D24 AS '+@D24+',D25 AS '+@D25+',D26 AS '+@D26+',D27 AS '+@D27+',D28 AS '+@D28+',D29 AS '+@D29+
			',D30 AS '+@D30+' FROM #PruebaView ORDER BY Placa'
		
		EXEC (@SQL)
		DROP TABLE #PruebaView
	END

	IF @CantDias = 31 BEGIN
		SET @SQL='SELECT Nro, Placa AS PLACA, Operacion AS OPERACION, TipoUnidad AS TIPO_UNIDAD, DiferenciaKM AS KM_DIFERENCIA, UltimaFecha AS ULTIMA_FECHA,
			D1 AS '+@D1+',D2 AS '+@D2+',D3 AS '+@D3+',D4 AS '+@D4+',D5 AS '+@D5+',D6 AS '+@D6+',D7 AS '+@D7+',D8 AS '+@D8+
			',D9 AS '+@D9+',D10 AS '+@D10+',D11 AS '+@D11+',D12 AS '+@D12+',D13 AS '+@D13+',D14 AS '+@D14+',D15 AS '+@D15+
			',D16 AS '+@D16+',D17 AS '+@D17+',D18 AS '+@D18+',D19 AS '+@D19+',D20 AS '+@D20+',D21 AS '+@D21+',D22 AS '+@D22+
			',D23 AS '+@D23+',D24 AS '+@D24+',D25 AS '+@D25+',D26 AS '+@D26+',D27 AS '+@D27+',D28 AS '+@D28+',D29 AS '+@D29+
			',D30 AS '+@D30+',D31 AS '+@D31+' FROM #PruebaView ORDER BY Placa'
		
		EXEC (@SQL)
		DROP TABLE #PruebaView
	END
END

------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 03-06-2024
-- Description:	ALERTAR KILOMETRAJES DE UNIDADES
-- =============================================
/*
exec [ReportesApp_Mantenimiento_MttoPreventivo_AlertarKMUnidades] @idTracto = 1269 , @idRuta =1171
*/
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_AlertarKMUnidades]
@idTracto INT,
@idRuta INT,
@Sucursal VARCHAR(30)
AS
BEGIN
	DECLARE @Programacion INT = (SELECT IdProgramacion FROM ReportesApp_Operacion_MaestroUnidadesConductor WHERE IdUnidad = @idTracto)
	DECLARE @KMRuta DECIMAL(10,2) = (SELECT CAST(Distancia AS DECIMAL(10,2)) FROM OP_TR_RUTA WHERE IdRuta = @idRuta)
	DECLARE @DiferenciaKM DECIMAL(10,2) = (SELECT CAST((MR.Frecuencia + MR.UltimoKM) - KM.KMActual AS DECIMAL(10,2))
										   FROM ReportesApp_Mantenimiento_MttoPreventivo_Registro MR
										   LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = MR.idVehiculo
										   WHERE MR.idVehiculo = @idTracto)
	DECLARE @ProxMtto VARCHAR(10) = (SELECT ISNULL(TM.TipoMantenimiento, CASE WHEN MR.Aceite = 'SINTETICO' THEN 'M0' ELSE 'M1' END)
									 FROM ReportesApp_Mantenimiento_MttoPreventivo_TipoMtto TM LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Registro MR
									 ON TM.idMantenimientoOP = MR.idMantenimientoOP WHERE MR.idVehiculo = @idTracto AND TM.Posicion = MR.PS + 1)

	IF ((ISNULL(@DiferenciaKM,99999) > @KMRuta)) BEGIN
		SELECT @KMRuta AS 'RUTA', @DiferenciaKM AS 'TRACTO', @ProxMtto AS 'PROXIMO', 1 AS 'ESTADO'
	END
	ELSE BEGIN
		IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_MaestroDesbloqueo WHERE idRuta = @idRuta AND idVehiculo = @idTracto
		AND Estado = 'ACTIVO')) BEGIN
			SELECT @KMRuta AS 'RUTA', @DiferenciaKM AS 'TRACTO', @ProxMtto AS 'PROXIMO', 1 AS 'ESTADO'
		END
		ELSE BEGIN
			SELECT @KMRuta AS 'RUTA', @DiferenciaKM AS 'TRACTO', @ProxMtto AS 'PROXIMO', 0 AS 'ESTADO'
		END
	END
END

------------------------------------------------------------------
------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 18-06-2024
-- Description:	INGRESAR RECURSOS DE ACCESORIOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_CrearEliminarRecursos]
@Opcion INT,
@idRecursoMtto INT,
@idProcesoMtto INT,
@idVehiculo INT,
@Item VARCHAR(30),
@Descripcion VARCHAR(500),
@Cantidad DECIMAL(10,2),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- CREAR RECURSO
		SET @correlativo = (SELECT MAX(idRecursoMtto) FROM ReportesApp_Mantenimiento_MttoPreventivo_Recursos WHERE idProcesoMtto = @idProcesoMtto AND idVehiculo = @idVehiculo)
		SET @correlativo = ISNULL(@correlativo,0) + 1

		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_Recursos(idRecursoMtto,idProcesoMtto,idVehiculo,Item,Descripcion,Cantidad,UsuarioCreacion,FechaCreacion)
		VALUES(@correlativo,@idProcesoMtto,@idVehiculo,@Item,@Descripcion,@Cantidad,@Usuario,GETDATE())
				
		SET @Exito = '0 = El ítem ha sido asignado correctamente a este accesorio.'
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR RECURSO
		DELETE FROM ReportesApp_Mantenimiento_MttoPreventivo_Recursos
		WHERE idRecursoMtto = @idRecursoMtto AND idProcesoMtto = @idProcesoMtto AND idVehiculo = @idVehiculo

		SET @Exito = '0 = Item eliminado.'
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

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 18-06-2024
-- Description:	LISTAR RECURSOS POR ACCESORIO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarRecursosAccesorio]
@idProcesoMtto INT,
@idVehiculo INT
AS
BEGIN
	SELECT idRecursoMtto, idProcesoMtto, idVehiculo, Item AS 'CÓDIGO', Descripcion AS 'ÍTEM', Cantidad AS 'CANTIDAD',
	UsuarioCreacion, FechaCreacion FROM ReportesApp_Mantenimiento_MttoPreventivo_Recursos
	WHERE idProcesoMtto = @idProcesoMtto AND idVehiculo = @idVehiculo
	ORDER BY idRecursoMtto ASC
END

-----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 18-06-2024
-- Description:	LISTAR RECURSOS POR UNIDAD
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarRecursos]
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@Placa VARCHAR(20),
@TipoMaquina VARCHAR(50),
@Actividad VARCHAR(100)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@TipoMaquina = 'TODOS') BEGIN
		(SELECT X.idProcesoMtto, X.idVehiculo, ISNULL(X.idRecursoMtto,0) AS 'idRecursoMtto', X.PLACA, X.TIPO_UNIDAD, X.SUBTIPO_UNIDAD, X.OPERACION, X.ACTIVIDAD, X.ITEM,
		X.DESCRIPCION_ITEM, X.CANTIDAD, CASE WHEN X.ITEM IS NULL THEN NULL ELSE (SELECT TOP(1) CONVERT(DECIMAL(10,2),ROUND(PrecioUnitario,2)) FROM WH_TransaccionDetalle WHERE ((WH_TransaccionDetalle.CompaniaSocio = '10000000')
		AND (TipoDocumento = 'NI') AND (Item = X.ITEM)) ORDER BY NumeroDocumento DESC) END AS PRECIO_UNITARIO,
		CASE WHEN X.ITEM IS NULL THEN NULL ELSE (SELECT TOP(1) CONVERT(DECIMAL(10,2),ROUND(PrecioUnitario * X.CANTIDAD,2)) FROM WH_TransaccionDetalle WHERE ((WH_TransaccionDetalle.CompaniaSocio = '10000000')
		AND (TipoDocumento = 'NI') AND (Item = X.ITEM)) ORDER BY NumeroDocumento DESC) END AS PRECIO_TOTAL,
		X.FECHA_CAMBIO, X.KM_CAMBIO, X.INTERVALO, X.FECHA_ACTUAL, X.KM_ACTUAL, X.TIEMPO_MESES, X.KM_RECORRIDO, X.[KM/DIA], X.KM_FALTANTE, X.[%],
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA', X.Usuario, X.Fecha
		FROM (SELECT PM.idAccesorio, PM.idProcesoMtto, PM.idVehiculo, V.NumeroPlaca AS 'PLACA', ISNULL(TV.Descripcion,'UNIDADES TERCERAS') AS 'TIPO_UNIDAD', SV.Descripcion AS 'SUBTIPO_UNIDAD', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion
		END AS 'OPERACION', AC.Descripcion AS 'ACTIVIDAD', PM.FechaCambio AS 'FECHA_CAMBIO', PM.KMCambio AS 'KM_CAMBIO', PM.Intervalo AS 'INTERVALO', KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL',
		DATEDIFF(MONTH,PM.FechaCambio,KM.Fecha) AS 'TIEMPO_MESES', CASE WHEN PM.KMCambio = 0 THEN 0 ELSE KM.KMActual - PM.KMCambio END AS 'KM_RECORRIDO', CASE WHEN DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) = 0 THEN 1 ELSE
		CAST((KM.KMActual - PM.KMCambio) / DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA', PM.Intervalo - (KM.KMActual - PM.KMCambio) AS 'KM_FALTANTE', 
		CASE WHEN PM.Intervalo = 0 THEN 0 ELSE CAST((1-(PM.Intervalo - (KM.KMActual - PM.KMCambio)) / PM.Intervalo) * 100 AS DECIMAL(10,2)) END AS '%', RM.idRecursoMtto, RM.Item AS 'ITEM',
		RM.Descripcion AS 'DESCRIPCION_ITEM', RM.Cantidad AS 'CANTIDAD', PM.Usuario, PM.Fecha FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto PM
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Accesorios AC ON AC.idAccesorio = PM.idAccesorio
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = PM.idVehiculo
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = PM.idVehiculo
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON UC.IdProgramacion = O.IdOperacion
		LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
		LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TipoVehiculo
		LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Recursos RM ON PM.idProcesoMtto = RM.idProcesoMtto AND PM.idVehiculo = RM.idVehiculo
		WHERE V.Estado = 2) X
		WHERE ((CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) BETWEEN @FINICIO AND @FFIN) AND
		(X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND (X.ACTIVIDAD IS NULL OR X.ACTIVIDAD LIKE '%' + @Actividad + '%'))
		UNION
		(SELECT X.idProcesoMtto, 0 AS 'idVehiculo', ISNULL(X.idRecursoMtto,0) AS 'idRecursoMtto', X.PLACA, X.TIPO_UNIDAD, X.SUBTIPO_UNIDAD, X.OPERACION, X.ACTIVIDAD, X.ITEM,
		X.DESCRIPCION_ITEM, X.CANTIDAD, CASE WHEN X.ITEM IS NULL THEN NULL ELSE (SELECT TOP(1) CONVERT(DECIMAL(10,2),ROUND(PrecioUnitario,2)) FROM WH_TransaccionDetalle WHERE ((WH_TransaccionDetalle.CompaniaSocio = '10000000')
		AND (TipoDocumento = 'NI') AND (Item = X.ITEM)) ORDER BY NumeroDocumento DESC) END AS PRECIO_UNITARIO,
		CASE WHEN X.ITEM IS NULL THEN NULL ELSE (SELECT TOP(1) CONVERT(DECIMAL(10,2),ROUND(PrecioUnitario * X.CANTIDAD,2)) FROM WH_TransaccionDetalle WHERE ((WH_TransaccionDetalle.CompaniaSocio = '10000000')
		AND (TipoDocumento = 'NI') AND (Item = X.ITEM)) ORDER BY NumeroDocumento DESC) END AS PRECIO_TOTAL,
		X.FECHA_CAMBIO, X.KM_CAMBIO, X.INTERVALO, X.FECHA_ACTUAL, X.KM_ACTUAL, X.TIEMPO_MESES, X.KM_RECORRIDO, X.[KM/DIA], X.KM_FALTANTE, X.[%],
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA', X.Usuario, X.Fecha
		FROM (SELECT PM.idAccesorio, PM.idProcesoMtto, PM.MaquinaCodigo AS 'PLACA', LTRIM(RTRIM(TG.DescripcionLocal)) AS 'SUBTIPO_UNIDAD', LTRIM(RTRIM(ISNULL(T.DescripcionLocal,'UNIDADES TERCERAS'))) AS 'TIPO_UNIDAD', 'SIN OPERACION' AS 'OPERACION',
		AC.Descripcion AS 'ACTIVIDAD', PM.FechaCambio AS 'FECHA_CAMBIO', PM.KMCambio AS 'KM_CAMBIO', PM.Intervalo AS 'INTERVALO', KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL', 
		DATEDIFF(MONTH,PM.FechaCambio,KM.Fecha) AS 'TIEMPO_MESES', CASE WHEN PM.KMCambio = 0 THEN 0 ELSE KM.KMActual - PM.KMCambio END AS 'KM_RECORRIDO', CASE WHEN DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) = 0 THEN 1 ELSE
		CAST((KM.KMActual - PM.KMCambio) / DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA', PM.Intervalo - (KM.KMActual - PM.KMCambio) AS 'KM_FALTANTE',
		CASE WHEN PM.Intervalo = 0 THEN 0 ELSE CAST((1-(PM.Intervalo - (KM.KMActual - PM.KMCambio)) / PM.Intervalo) * 100 AS DECIMAL(10,2)) END AS '%', RM.idRecursoMtto, RM.Item AS 'ITEM',
		RM.Descripcion AS 'DESCRIPCION_ITEM', RM.Cantidad AS 'CANTIDAD', PM.Usuario, PM.Fecha
		FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMttoMaquinas PM
		LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(PM.MaquinaCodigo)) AND (M.Estado = 'A')
		LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
		LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Accesorios AC ON AC.idAccesorio = PM.idAccesorio
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(PM.MaquinaCodigo))
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_RecursosMaquina RM ON PM.idProcesoMtto = RM.idProcesoMtto AND PM.MaquinaCodigo = RM.MaquinaCodigo) X
		WHERE ((CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) BETWEEN @FINICIO AND @FFIN) AND
		(X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND (X.ACTIVIDAD IS NULL OR X.ACTIVIDAD LIKE '%' + @Actividad + '%'))
		ORDER BY X.PLACA, X.idProcesoMtto ASC
	END
	ELSE BEGIN
		(SELECT X.idProcesoMtto, X.idVehiculo, ISNULL(X.idRecursoMtto,0) AS 'idRecursoMtto', X.PLACA, X.TIPO_UNIDAD, X.SUBTIPO_UNIDAD, X.OPERACION, X.ACTIVIDAD, X.ITEM,
		X.DESCRIPCION_ITEM, X.CANTIDAD, CASE WHEN X.ITEM IS NULL THEN NULL ELSE (SELECT TOP(1) CONVERT(DECIMAL(10,2),ROUND(PrecioUnitario,2)) FROM WH_TransaccionDetalle WHERE ((WH_TransaccionDetalle.CompaniaSocio = '10000000')
		AND (TipoDocumento = 'NI') AND (Item = X.ITEM)) ORDER BY NumeroDocumento DESC) END AS PRECIO_UNITARIO, 
		CASE WHEN X.ITEM IS NULL THEN NULL ELSE (SELECT TOP(1) CONVERT(DECIMAL(10,2),ROUND(PrecioUnitario * X.CANTIDAD,2)) FROM WH_TransaccionDetalle WHERE ((WH_TransaccionDetalle.CompaniaSocio = '10000000')
		AND (TipoDocumento = 'NI') AND (Item = X.ITEM)) ORDER BY NumeroDocumento DESC) END AS PRECIO_TOTAL,
		X.FECHA_CAMBIO, X.KM_CAMBIO, X.INTERVALO, X.FECHA_ACTUAL, X.KM_ACTUAL, X.TIEMPO_MESES, X.KM_RECORRIDO, X.[KM/DIA], X.KM_FALTANTE, X.[%],
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA', X.Usuario, X.Fecha
		FROM (SELECT PM.idAccesorio, PM.idProcesoMtto, PM.idVehiculo, V.NumeroPlaca AS 'PLACA', ISNULL(TV.Descripcion,'UNIDADES TERCERAS') AS 'TIPO_UNIDAD', SV.Descripcion AS 'SUBTIPO_UNIDAD', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion
		END AS 'OPERACION', AC.Descripcion AS 'ACTIVIDAD', PM.FechaCambio AS 'FECHA_CAMBIO', PM.KMCambio AS 'KM_CAMBIO', PM.Intervalo AS 'INTERVALO', KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL',
		DATEDIFF(MONTH,PM.FechaCambio,KM.Fecha) AS 'TIEMPO_MESES', CASE WHEN PM.KMCambio = 0 THEN 0 ELSE KM.KMActual - PM.KMCambio END AS 'KM_RECORRIDO', CASE WHEN DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) = 0 THEN 1 ELSE
		CAST((KM.KMActual - PM.KMCambio) / DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA', PM.Intervalo - (KM.KMActual - PM.KMCambio) AS 'KM_FALTANTE', 
		CASE WHEN PM.Intervalo = 0 THEN 0 ELSE CAST((1-(PM.Intervalo - (KM.KMActual - PM.KMCambio)) / PM.Intervalo) * 100 AS DECIMAL(10,2)) END AS '%', RM.idRecursoMtto, RM.Item AS 'ITEM',
		RM.Descripcion AS 'DESCRIPCION_ITEM', RM.Cantidad AS 'CANTIDAD', PM.Usuario, PM.Fecha FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto PM
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Accesorios AC ON AC.idAccesorio = PM.idAccesorio
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = PM.idVehiculo
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = PM.idVehiculo
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON UC.IdProgramacion = O.IdOperacion
		LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
		LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TipoVehiculo
		LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Recursos RM ON PM.idProcesoMtto = RM.idProcesoMtto AND PM.idVehiculo = RM.idVehiculo
		WHERE V.Estado = 2) X
		WHERE ((CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) BETWEEN @FINICIO AND @FFIN) AND
		(X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND (X.TIPO_UNIDAD = @TipoMaquina) AND (X.ACTIVIDAD IS NULL OR X.ACTIVIDAD LIKE '%' + @Actividad + '%'))
		UNION
		(SELECT X.idProcesoMtto, 0 AS 'idVehiculo', ISNULL(X.idRecursoMtto,0) AS 'idRecursoMtto', X.PLACA, X.TIPO_UNIDAD, X.SUBTIPO_UNIDAD, X.OPERACION, X.ACTIVIDAD, X.ITEM,
		X.DESCRIPCION_ITEM, X.CANTIDAD, CASE WHEN X.ITEM IS NULL THEN NULL ELSE (SELECT TOP(1) CONVERT(DECIMAL(10,2),ROUND(PrecioUnitario,2)) FROM WH_TransaccionDetalle WHERE ((WH_TransaccionDetalle.CompaniaSocio = '10000000')
		AND (TipoDocumento = 'NI') AND (Item = X.ITEM)) ORDER BY NumeroDocumento DESC) END AS PRECIO_UNITARIO,
		CASE WHEN X.ITEM IS NULL THEN NULL ELSE (SELECT TOP(1) CONVERT(DECIMAL(10,2),ROUND(PrecioUnitario * X.CANTIDAD,2)) FROM WH_TransaccionDetalle WHERE ((WH_TransaccionDetalle.CompaniaSocio = '10000000')
		AND (TipoDocumento = 'NI') AND (Item = X.ITEM)) ORDER BY NumeroDocumento DESC) END AS PRECIO_TOTAL,
		X.FECHA_CAMBIO, X.KM_CAMBIO, X.INTERVALO, X.FECHA_ACTUAL, X.KM_ACTUAL, X.TIEMPO_MESES, X.KM_RECORRIDO, X.[KM/DIA], X.KM_FALTANTE, X.[%],
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA', X.Usuario, X.Fecha
		FROM (SELECT PM.idAccesorio, PM.idProcesoMtto, PM.MaquinaCodigo AS 'PLACA', LTRIM(RTRIM(TG.DescripcionLocal)) AS 'SUBTIPO_UNIDAD', LTRIM(RTRIM(ISNULL(T.DescripcionLocal,'UNIDADES TERCERAS'))) AS 'TIPO_UNIDAD', 'SIN OPERACION' AS 'OPERACION',
		AC.Descripcion AS 'ACTIVIDAD', PM.FechaCambio AS 'FECHA_CAMBIO', PM.KMCambio AS 'KM_CAMBIO', PM.Intervalo AS 'INTERVALO', KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL', 
		DATEDIFF(MONTH,PM.FechaCambio,KM.Fecha) AS 'TIEMPO_MESES', CASE WHEN PM.KMCambio = 0 THEN 0 ELSE KM.KMActual - PM.KMCambio END AS 'KM_RECORRIDO', CASE WHEN DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) = 0 THEN 1 ELSE
		CAST((KM.KMActual - PM.KMCambio) / DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA', PM.Intervalo - (KM.KMActual - PM.KMCambio) AS 'KM_FALTANTE',
		CASE WHEN PM.Intervalo = 0 THEN 0 ELSE CAST((1-(PM.Intervalo - (KM.KMActual - PM.KMCambio)) / PM.Intervalo) * 100 AS DECIMAL(10,2)) END AS '%', RM.idRecursoMtto, RM.Item AS 'ITEM',
		RM.Descripcion AS 'DESCRIPCION_ITEM', RM.Cantidad AS 'CANTIDAD', PM.Usuario, PM.Fecha
		FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMttoMaquinas PM
		LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(PM.MaquinaCodigo)) AND (M.Estado = 'A')
		LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
		LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Accesorios AC ON AC.idAccesorio = PM.idAccesorio
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(PM.MaquinaCodigo))
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_RecursosMaquina RM ON PM.idProcesoMtto = RM.idProcesoMtto AND PM.MaquinaCodigo = RM.MaquinaCodigo) X
		WHERE ((CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) BETWEEN @FINICIO AND @FFIN) AND
		(X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND (X.TIPO_UNIDAD = @TipoMaquina) AND (X.ACTIVIDAD IS NULL OR X.ACTIVIDAD LIKE '%' + @Actividad + '%'))
		ORDER BY X.PLACA, X.idProcesoMtto ASC
	END
END

------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 18-06-2024
-- Description:	INGRESAR RECURSOS DE ACCESORIOS - MAQUINA
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_CrearEliminarRecursosMaquina]
@Opcion INT,
@idRecursoMtto INT,
@idProcesoMtto INT,
@MaquinaCodigo VARCHAR(20),
@Item VARCHAR(30),
@Descripcion VARCHAR(500),
@Cantidad DECIMAL(10,2),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- CREAR RECURSO
		SET @correlativo = (SELECT MAX(idRecursoMtto) FROM ReportesApp_Mantenimiento_MttoPreventivo_RecursosMaquina WHERE idProcesoMtto = @idProcesoMtto AND MaquinaCodigo = @MaquinaCodigo)
		SET @correlativo = ISNULL(@correlativo,0) + 1

		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_RecursosMaquina(idRecursoMtto,idProcesoMtto,MaquinaCodigo,Item,Descripcion,Cantidad,UsuarioCreacion,FechaCreacion)
		VALUES(@correlativo,@idProcesoMtto,@MaquinaCodigo,@Item,@Descripcion,@Cantidad,@Usuario,GETDATE())
				
		SET @Exito = '0 = El ítem ha sido asignado correctamente a esta actividad.'
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR RECURSO
		DELETE FROM ReportesApp_Mantenimiento_MttoPreventivo_RecursosMaquina
		WHERE idRecursoMtto = @idRecursoMtto AND idProcesoMtto = @idProcesoMtto AND MaquinaCodigo = @MaquinaCodigo

		SET @Exito = '0 = Item eliminado.'
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

-------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 18-06-2024
-- Description:	LISTAR RECURSOS POR ACCESORIO - MAQUINA
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarRecursosAccesorioMaquina]
@idProcesoMtto INT,
@MaquinaCodigo VARCHAR(20)
AS
BEGIN
	SELECT idRecursoMtto, idProcesoMtto, MaquinaCodigo, Item AS 'CÓDIGO', Descripcion AS 'ÍTEM', Cantidad AS 'CANTIDAD',
	UsuarioCreacion, FechaCreacion FROM ReportesApp_Mantenimiento_MttoPreventivo_RecursosMaquina
	WHERE idProcesoMtto = @idProcesoMtto AND MaquinaCodigo = @MaquinaCodigo
	ORDER BY idRecursoMtto ASC
END

--------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 19-06-2024
-- Description:	LISTAR RESUMEN DE RECURSOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarResumenRecursos]
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@Item VARCHAR(300)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	DECLARE @T_RECURSOS TABLE(Item VARCHAR(30), Descripcion VARCHAR(500), Cantidad DECIMAL(10,2), FechaProyectada DATE, Periodo VARCHAR(6))

	INSERT INTO @T_RECURSOS(Item,Descripcion,Cantidad,FechaProyectada)
	SELECT X.ITEM, X.DESCRIPCION_ITEM, X.CANTIDAD,
	(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA'
	FROM (SELECT RM.Item AS 'ITEM', RM.Descripcion AS 'DESCRIPCION_ITEM', RM.Cantidad AS 'CANTIDAD', KM.Fecha AS 'FECHA_ACTUAL', PM.Intervalo - (KM.KMActual - PM.KMCambio) AS 'KM_FALTANTE', 
	CASE WHEN DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) = 0 THEN 1 ELSE CAST((KM.KMActual - PM.KMCambio) / DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA'
	FROM ReportesApp_Mantenimiento_MttoPreventivo_Recursos RM
	LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto PM ON PM.idProcesoMtto = RM.idProcesoMtto AND PM.idVehiculo = RM.idVehiculo
	LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = PM.idVehiculo) X
	WHERE ((X.DESCRIPCION_ITEM IS NULL OR X.DESCRIPCION_ITEM LIKE '%' + @Item + '%') AND (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE()
	ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) BETWEEN @FINICIO AND @FFIN)	--'01/01/2022' AND '08/08/9990')
	UNION
	SELECT X.ITEM, X.DESCRIPCION_ITEM, X.CANTIDAD,
	(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA'
	FROM (SELECT RM.Item AS 'ITEM', RM.Descripcion AS 'DESCRIPCION_ITEM', RM.Cantidad AS 'CANTIDAD', KM.Fecha AS 'FECHA_ACTUAL', PM.Intervalo - (KM.KMActual - PM.KMCambio) AS 'KM_FALTANTE',
	CASE WHEN DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) = 0 THEN 1 ELSE CAST((KM.KMActual - PM.KMCambio) / DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA'
	FROM ReportesApp_Mantenimiento_MttoPreventivo_RecursosMaquina RM
	LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMttoMaquinas PM ON PM.idProcesoMtto = RM.idProcesoMtto AND PM.MaquinaCodigo = RM.MaquinaCodigo
	LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(PM.MaquinaCodigo))) X
	WHERE ((X.DESCRIPCION_ITEM IS NULL OR X.DESCRIPCION_ITEM LIKE '%' + @Item + '%') AND (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE()
	ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) BETWEEN @FINICIO AND @FFIN)  --'01/01/2022' AND '08/08/9990')

	UPDATE @T_RECURSOS
	SET Periodo = RIGHT('00' + CONVERT(VARCHAR(20),MONTH(FechaProyectada)),2) + CONVERT(VARCHAR,YEAR(FechaProyectada))

	SELECT Periodo, Item, Descripcion, '' AS 'FechaProyectada', SUM(Cantidad) AS 'TOTAL' 
	FROM @T_RECURSOS
	GROUP BY Periodo, Item, Descripcion
	ORDER BY Descripcion ASC
END

---------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 25-06-2024
-- Description:	INGRESAR Y ELIMINAR DESBLOQUEO DE TRACTOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_IngresarEliminarDesbloqueo]
@Opcion INT,
@idDesbloqueo INT,
@idVehiculo INT,
@idRuta INT,
@FechaCompromiso DATETIME,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- CREAR DESBLOQUEO
		IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_MaestroDesbloqueo WHERE idVehiculo = @idVehiculo AND idRuta = @idRuta
		AND Estado = 'ACTIVO')) BEGIN
			SET @Exito = '-1 = Esta unidad ya ha sido desbloqueada para esta ruta.'
			ROLLBACK
			GOTO Terminar
		END

		SET @correlativo = (SELECT MAX(idDesbloqueo) FROM ReportesApp_Mantenimiento_MttoPreventivo_MaestroDesbloqueo)
		SET @correlativo = ISNULL(@correlativo,0) + 1

		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_MaestroDesbloqueo(idDesbloqueo,idVehiculo,idRuta,FechaCompromiso,Estado,UsuarioCreacion,FechaCreacion)
		VALUES(@correlativo,@idVehiculo,@idRuta,@FechaCompromiso,'ACTIVO',@Usuario,GETDATE())

		SET @Exito = '0 = Unidad Desbloqueada Correctamente.'
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR DESBLOQUEO
		DELETE FROM ReportesApp_Mantenimiento_MttoPreventivo_MaestroDesbloqueo
		WHERE idDesbloqueo = @idDesbloqueo

		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_MaestroDesbloqueo
		SET idDesbloqueo = idDesbloqueo - 1
		WHERE idDesbloqueo > @idDesbloqueo

		SET @Exito = '0 = Desbloqueo Eliminado Correctamente.'
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

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 25-06-2024
-- Description:	LISTAR DESBLOQUEOS DE UNIDADES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarDesbloqueoUnidades]
@Placa VARCHAR(50),
@Ruta VARCHAR(300)
AS
BEGIN
	SELECT MD.idDesbloqueo, V.NumeroPlaca AS 'PLACA', R.Descripcion AS 'RUTA', CONVERT(VARCHAR,MD.FechaCompromiso,103) AS 'FECHA_COMPROMISO',
	MD.Estado AS 'ESTADO', MD.UsuarioCreacion, MD.FechaCreacion
	FROM ReportesApp_Mantenimiento_MttoPreventivo_MaestroDesbloqueo MD
	LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = MD.idVehiculo
	LEFT JOIN OP_TR_Ruta R ON R.IdRuta = MD.idRuta
	WHERE (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND (R.Descripcion IS NULL OR R.Descripcion LIKE '%' + @Ruta + '%')
	ORDER BY MD.idDesbloqueo DESC
END

-------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 18-06-2024
-- Description:	LISTAR ACTIVIDADES DE MTTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarActividades]
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@Placa VARCHAR(20),
@TipoMaquina VARCHAR(50),
@Actividad VARCHAR(100)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@TipoMaquina = 'TODOS') BEGIN
		(SELECT DISTINCT X.idProcesoMtto, X.idVehiculo, X.ACTIVIDAD, X.PLACA, X.TIPO_UNIDAD, X.SUBTIPO_UNIDAD, X.OPERACION, X.FECHA_CAMBIO,
		X.KM_CAMBIO, X.INTERVALO, X.FECHA_ACTUAL, X.KM_ACTUAL, X.TIEMPO_MESES, X.KM_RECORRIDO, X.[KM/DIA], X.KM_FALTANTE, X.[%],
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA', X.Usuario, X.Fecha
		FROM (SELECT DISTINCT PM.idAccesorio, PM.idProcesoMtto, PM.idVehiculo, V.NumeroPlaca AS 'PLACA', ISNULL(TV.Descripcion,'UNIDADES TERCERAS') AS 'TIPO_UNIDAD', SV.Descripcion AS 'SUBTIPO_UNIDAD', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion
		END AS 'OPERACION', AC.Descripcion AS 'ACTIVIDAD', PM.FechaCambio AS 'FECHA_CAMBIO', PM.KMCambio AS 'KM_CAMBIO', PM.Intervalo AS 'INTERVALO', KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL',
		DATEDIFF(MONTH,PM.FechaCambio,KM.Fecha) AS 'TIEMPO_MESES', CASE WHEN PM.KMCambio = 0 THEN 0 ELSE KM.KMActual - PM.KMCambio END AS 'KM_RECORRIDO', CASE WHEN DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) = 0 THEN 1 ELSE
		CAST((KM.KMActual - PM.KMCambio) / DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA', PM.Intervalo - (KM.KMActual - PM.KMCambio) AS 'KM_FALTANTE', 
		CASE WHEN PM.Intervalo = 0 THEN 0 ELSE CAST((1-(PM.Intervalo - (KM.KMActual - PM.KMCambio)) / PM.Intervalo) * 100 AS DECIMAL(10,2)) END AS '%', PM.Usuario, PM.Fecha
		FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto PM
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Accesorios AC ON AC.idAccesorio = PM.idAccesorio
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = PM.idVehiculo
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = PM.idVehiculo
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON UC.IdProgramacion = O.IdOperacion
		LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
		LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TipoVehiculo
		LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
		WHERE V.Estado = 2) X
		WHERE ((CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) BETWEEN @FINICIO AND @FFIN) AND
		(X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND (X.ACTIVIDAD IS NULL OR X.ACTIVIDAD LIKE '%' + @Actividad + '%'))
		UNION
		(SELECT DISTINCT X.idProcesoMtto, 0 AS 'idVehiculo', X.ACTIVIDAD, X.PLACA, X.TIPO_UNIDAD, X.SUBTIPO_UNIDAD, X.OPERACION, X.FECHA_CAMBIO,
		X.KM_CAMBIO, X.INTERVALO, X.FECHA_ACTUAL, X.KM_ACTUAL, X.TIEMPO_MESES, X.KM_RECORRIDO, X.[KM/DIA], X.KM_FALTANTE, X.[%],
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA', X.Usuario, X.Fecha
		FROM (SELECT DISTINCT PM.idAccesorio, PM.idProcesoMtto, PM.MaquinaCodigo AS 'PLACA', LTRIM(RTRIM(TG.DescripcionLocal)) AS 'SUBTIPO_UNIDAD', LTRIM(RTRIM(ISNULL(T.DescripcionLocal,'UNIDADES TERCERAS'))) AS 'TIPO_UNIDAD', 'SIN OPERACION' AS 'OPERACION',
		AC.Descripcion AS 'ACTIVIDAD', PM.FechaCambio AS 'FECHA_CAMBIO', PM.KMCambio AS 'KM_CAMBIO', PM.Intervalo AS 'INTERVALO', KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL', 
		DATEDIFF(MONTH,PM.FechaCambio,KM.Fecha) AS 'TIEMPO_MESES', CASE WHEN PM.KMCambio = 0 THEN 0 ELSE KM.KMActual - PM.KMCambio END AS 'KM_RECORRIDO', CASE WHEN DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) = 0 THEN 1 ELSE
		CAST((KM.KMActual - PM.KMCambio) / DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA', PM.Intervalo - (KM.KMActual - PM.KMCambio) AS 'KM_FALTANTE',
		CASE WHEN PM.Intervalo = 0 THEN 0 ELSE CAST((1-(PM.Intervalo - (KM.KMActual - PM.KMCambio)) / PM.Intervalo) * 100 AS DECIMAL(10,2)) END AS '%', PM.Usuario, PM.Fecha
		FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMttoMaquinas PM
		LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(PM.MaquinaCodigo)) AND (M.Estado = 'A')
		LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
		LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Accesorios AC ON AC.idAccesorio = PM.idAccesorio
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(PM.MaquinaCodigo))) X
		WHERE ((CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) BETWEEN @FINICIO AND @FFIN) AND
		(X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND (X.ACTIVIDAD IS NULL OR X.ACTIVIDAD LIKE '%' + @Actividad + '%'))
		ORDER BY X.ACTIVIDAD, X.idProcesoMtto ASC
	END
	ELSE BEGIN
		(SELECT DISTINCT X.idProcesoMtto, X.idVehiculo, X.ACTIVIDAD, X.PLACA, X.TIPO_UNIDAD, X.SUBTIPO_UNIDAD, X.OPERACION, X.FECHA_CAMBIO,
		X.KM_CAMBIO, X.INTERVALO, X.FECHA_ACTUAL, X.KM_ACTUAL, X.TIEMPO_MESES, X.KM_RECORRIDO, X.[KM/DIA], X.KM_FALTANTE, X.[%],
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA', X.Usuario, X.Fecha
		FROM (SELECT DISTINCT PM.idAccesorio, PM.idProcesoMtto, PM.idVehiculo, V.NumeroPlaca AS 'PLACA', ISNULL(TV.Descripcion,'UNIDADES TERCERAS') AS 'TIPO_UNIDAD', SV.Descripcion AS 'SUBTIPO_UNIDAD', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion
		END AS 'OPERACION', AC.Descripcion AS 'ACTIVIDAD', PM.FechaCambio AS 'FECHA_CAMBIO', PM.KMCambio AS 'KM_CAMBIO', PM.Intervalo AS 'INTERVALO', KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL',
		DATEDIFF(MONTH,PM.FechaCambio,KM.Fecha) AS 'TIEMPO_MESES', CASE WHEN PM.KMCambio = 0 THEN 0 ELSE KM.KMActual - PM.KMCambio END AS 'KM_RECORRIDO', CASE WHEN DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) = 0 THEN 1 ELSE
		CAST((KM.KMActual - PM.KMCambio) / DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA', PM.Intervalo - (KM.KMActual - PM.KMCambio) AS 'KM_FALTANTE', 
		CASE WHEN PM.Intervalo = 0 THEN 0 ELSE CAST((1-(PM.Intervalo - (KM.KMActual - PM.KMCambio)) / PM.Intervalo) * 100 AS DECIMAL(10,2)) END AS '%', PM.Usuario, PM.Fecha
		FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMtto PM
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Accesorios AC ON AC.idAccesorio = PM.idAccesorio
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = PM.idVehiculo
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = PM.idVehiculo
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON UC.IdProgramacion = O.IdOperacion
		LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
		LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TipoVehiculo
		LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
		WHERE V.Estado = 2) X
		WHERE ((CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) BETWEEN @FINICIO AND @FFIN) AND
		(X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND (X.TIPO_UNIDAD = @TipoMaquina) AND (X.ACTIVIDAD IS NULL OR X.ACTIVIDAD LIKE '%' + @Actividad + '%'))
		UNION
		(SELECT DISTINCT X.idProcesoMtto, 0 AS 'idVehiculo', X.ACTIVIDAD, X.PLACA, X.TIPO_UNIDAD, X.SUBTIPO_UNIDAD, X.OPERACION, X.FECHA_CAMBIO,
		X.KM_CAMBIO, X.INTERVALO, X.FECHA_ACTUAL, X.KM_ACTUAL, X.TIEMPO_MESES, X.KM_RECORRIDO, X.[KM/DIA], X.KM_FALTANTE, X.[%],
		(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) AS 'FECHA_PROYECTADA', X.Usuario, X.Fecha
		FROM (SELECT DISTINCT PM.idAccesorio, PM.idProcesoMtto, PM.MaquinaCodigo AS 'PLACA', LTRIM(RTRIM(TG.DescripcionLocal)) AS 'SUBTIPO_UNIDAD', LTRIM(RTRIM(ISNULL(T.DescripcionLocal,'UNIDADES TERCERAS'))) AS 'TIPO_UNIDAD', 'SIN OPERACION' AS 'OPERACION',
		AC.Descripcion AS 'ACTIVIDAD', PM.FechaCambio AS 'FECHA_CAMBIO', PM.KMCambio AS 'KM_CAMBIO', PM.Intervalo AS 'INTERVALO', KM.Fecha AS 'FECHA_ACTUAL', KM.KMActual AS 'KM_ACTUAL', 
		DATEDIFF(MONTH,PM.FechaCambio,KM.Fecha) AS 'TIEMPO_MESES', CASE WHEN PM.KMCambio = 0 THEN 0 ELSE KM.KMActual - PM.KMCambio END AS 'KM_RECORRIDO', CASE WHEN DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) = 0 THEN 1 ELSE
		CAST((KM.KMActual - PM.KMCambio) / DATEDIFF(DAY, PM.FechaCambio, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA', PM.Intervalo - (KM.KMActual - PM.KMCambio) AS 'KM_FALTANTE',
		CASE WHEN PM.Intervalo = 0 THEN 0 ELSE CAST((1-(PM.Intervalo - (KM.KMActual - PM.KMCambio)) / PM.Intervalo) * 100 AS DECIMAL(10,2)) END AS '%', PM.Usuario, PM.Fecha
		FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMttoMaquinas PM
		LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(M.MaquinaCodigo)) = LTRIM(RTRIM(PM.MaquinaCodigo)) AND (M.Estado = 'A')
		LEFT JOIN ME_MaquinaTipoGrupo TG ON LTRIM(RTRIM(TG.TipoMaquinaGrupo)) = LTRIM(RTRIM(M.TipoMaquinaGrupo))
		LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina))
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Accesorios AC ON AC.idAccesorio = PM.idAccesorio
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacasMaquinas KM ON LTRIM(RTRIM(KM.MaquinaCodigo)) = LTRIM(RTRIM(PM.MaquinaCodigo))) X
		WHERE ((CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND(X.KM_FALTANTE/X.[KM/DIA],0),X.FECHA_ACTUAL) END) BETWEEN @FINICIO AND @FFIN) AND
		(X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND (X.TIPO_UNIDAD = @TipoMaquina) AND (X.ACTIVIDAD IS NULL OR X.ACTIVIDAD LIKE '%' + @Actividad + '%'))
		ORDER BY X.ACTIVIDAD, X.idProcesoMtto ASC
	END
END

-------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 13-08-2024
-- Description:	LISTAR MAESTRO DE ITEMS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarMaestroItems]
@Item VARCHAR(300)
AS
BEGIN
	SELECT LTRIM(RTRIM(Item)) AS 'CODIGO', LTRIM(RTRIM(DescripcionLocal)) AS 'ITEM'	FROM WH_ItemMast WHERE (Estado = 'A') AND (Item IS NULL OR Item LIKE '%' + @Item + '%')
END

-------------------------------------------------------------------------------
-------------------------------------------------------------------------------

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_InspeccionProcesos Y LLENARLA

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Detalle

--------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 15-08-2024
-- Description:	CREAR NUEVA INSPECCIÓN
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_CrearInspeccion]
@Placa VARCHAR(50),
@Tipo VARCHAR(250),
@SubTipo VARCHAR(250),
@Operacion VARCHAR(50),
@Marca VARCHAR(250),
@Modelo VARCHAR(250),
@FechaProyectada DATE,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

BEGIN TRAN
BEGIN TRY
	UPDATE ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera
	SET Ultimo = 0, Activo = 0
	WHERE (idInspeccionC = (SELECT MAX(idInspeccionC) FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera WHERE Placa = @Placa))

	SET @correlativo = (SELECT MAX(idInspeccionC) FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera)
	SET @correlativo = ISNULL(@correlativo,0) + 1 

	INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera(idInspeccionC,Placa,Tipo,SubTipo,Operacion,Marca,Modelo,FechaProyectada,
	FechaInicio,FechaFin,UsuarioCrea,FechaCrea,Ultimo,Activo)
	VALUES(@correlativo,@Placa,@Tipo,@SubTipo,@Operacion,@Marca,@Modelo,@FechaProyectada,GETDATE(),GETDATE(),@Usuario,GETDATE(),1,1)

	IF (@SubTipo = 'TRACTO') BEGIN
		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Detalle(idInspeccionD,idInspeccionC,idProceso,Estado)
		SELECT idProceso, @correlativo, idProceso, 'BUENO' FROM ReportesApp_Mantenimiento_MttoPreventivo_InspeccionProcesos
	END
	ELSE BEGIN
		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_DetalleCR(idInspeccionD,idInspeccionC,idProcesoCR,Estado)
		SELECT idProcesoCR, @correlativo, idProcesoCR, 'BUENO' FROM ReportesApp_Mantenimiento_MttoPreventivo_InspeccionProcesosCR
	END

	SET @Exito = '0 = Inspeccion creada correctamente.'
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

--------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 14-08-2024
-- Description:	LISTAR INSPECCIONES DE UNIDADES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarInspeccionesUnidad]
@Opcion INT,
@Placa VARCHAR(250),
@idInspeccionC INT
AS
BEGIN
	IF (@Opcion = 1) BEGIN		-- LISTAR MECÁNICOS CON TURNOS
		SELECT Persona, NombreCompleto AS 'MECÁNICO', Turno AS 'TURNO'
		FROM ReportesApp_Mantenimiento_AsignacionOT_Mecanicos
		WHERE (NombreCompleto LIKE '%' + @Placa + '%')
	END

	IF (@Opcion = 2) BEGIN		-- LISTAR INSPECCIONES POR UNIDAD
		SELECT ID.idInspeccionC, ID.idInspeccionD, IC.Placa, IP.TipoProceso AS 'COMPONENTE', IP.ParteTracto AS 'PROCESO', IC.FechaInicio AS 'INICIO_INSPECCION',		IC.FechaFin AS 'FIN_INSPECCION', ISNULL(IC.Mecanico,-1) AS 'Mecanico1', LTRIM(RTRIM(P1.NombreCompleto)) AS 'MECÁNICO', ISNULL(IC.Electrico,-1) AS 'Electrico2',		LTRIM(RTRIM(P2.NombreCompleto)) AS 'ELÉCTRICO', ISNULL(IC.Neumatico,-1) AS 'Neumatico3', LTRIM(RTRIM(P3.NombreCompleto)) AS 'NEUMÁTICO',		ISNULL(IC.Soldador,-1) AS 'Soldador4', LTRIM(RTRIM(P4.NombreCompleto)) AS 'SOLDADOR', ID.Estado AS 'ESTADO', ID.Observacion AS 'OBSERVACION'		FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Detalle ID		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera IC ON IC.idInspeccionC = ID.idInspeccionC		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_InspeccionProcesos IP ON IP.idProceso = ID.idProceso		LEFT JOIN PersonaMast P1 ON P1.Persona = IC.Mecanico		LEFT JOIN PersonaMast P2 ON P2.Persona = IC.Electrico		LEFT JOIN PersonaMast P3 ON P3.Persona = IC.Neumatico		LEFT JOIN PersonaMast P4 ON P4.Persona = IC.Soldador		WHERE (IC.Placa = @Placa) AND		ID.idInspeccionC = (SELECT MAX(idInspeccionC) FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera WHERE IC.Placa = @Placa)		ORDER BY ID.idInspeccionD ASC
	END

	IF (@Opcion = 3) BEGIN		-- BUSCAR INSPECCIONES POR ID
		SELECT ID.idInspeccionC, ID.idInspeccionD, IC.Placa, IP.TipoProceso AS 'COMPONENTE', IP.ParteTracto AS 'PROCESO', IC.FechaInicio AS 'INICIO_INSPECCION',		IC.FechaFin AS 'FIN_INSPECCION', ISNULL(IC.Mecanico,-1) AS 'Mecanico1', LTRIM(RTRIM(P1.NombreCompleto)) AS 'MECÁNICO', ISNULL(IC.Electrico,-1) AS 'Electrico2',		LTRIM(RTRIM(P2.NombreCompleto)) AS 'ELÉCTRICO', ISNULL(IC.Neumatico,-1) AS 'Neumatico3', LTRIM(RTRIM(P3.NombreCompleto)) AS 'NEUMÁTICO',		ISNULL(IC.Soldador,-1) AS 'Soldador4', LTRIM(RTRIM(P4.NombreCompleto)) AS 'SOLDADOR', ID.Estado AS 'ESTADO', ID.Observacion AS 'OBSERVACION'		FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Detalle ID		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera IC ON IC.idInspeccionC = ID.idInspeccionC		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_InspeccionProcesos IP ON IP.idProceso = ID.idProceso		LEFT JOIN PersonaMast P1 ON P1.Persona = IC.Mecanico		LEFT JOIN PersonaMast P2 ON P2.Persona = IC.Electrico		LEFT JOIN PersonaMast P3 ON P3.Persona = IC.Neumatico		LEFT JOIN PersonaMast P4 ON P4.Persona = IC.Soldador		WHERE ID.idInspeccionC = @idInspeccionC		ORDER BY ID.idInspeccionD ASC
	END

	IF (@Opcion = 4) BEGIN		-- ELIMINAR INSPECCIONES
		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera
		SET Activo = 0
		WHERE idInspeccionC = @idInspeccionC

		DECLARE @exito VARCHAR(50) = '0 = Inspeccion eliminada correctamente.'

		SELECT @exito exito
	END

	IF (@Opcion = 5) BEGIN		-- LISTAR INSPECCIONES POR UNIDAD - CORTINERAS
		--DECLARE @Placa2 VARCHAR(20) = (SELECT Placa FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera WHERE idInspeccionC = @idInspeccionC)
		/*DECLARE @TipoV VARCHAR(250) = (SELECT SV.Descripcion FROM OP_TR_Vehiculo V LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo									   WHERE V.NumeroPlaca = @Placa)*/		SELECT ID.idInspeccionC, ID.idInspeccionD, IC.Placa, IP.ParteTracto AS 'COMPONENTE', IP.TipoProceso AS 'PROCESO', IC.FechaInicio AS 'INICIO_INSPECCION',		IC.FechaFin AS 'FIN_INSPECCION', ISNULL(IC.Mecanico,-1) AS 'Mecanico1', LTRIM(RTRIM(P1.NombreCompleto)) AS 'MECÁNICO', ISNULL(IC.Electrico,-1) AS 'Electrico2',		LTRIM(RTRIM(P2.NombreCompleto)) AS 'ELÉCTRICO', ISNULL(IC.Neumatico,-1) AS 'Neumatico3', LTRIM(RTRIM(P3.NombreCompleto)) AS 'NEUMÁTICO',		ISNULL(IC.Soldador,-1) AS 'Soldador4', LTRIM(RTRIM(P4.NombreCompleto)) AS 'SOLDADOR', ID.Estado AS 'ESTADO', ID.Observacion AS 'OBSERVACION'		FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_DetalleCR ID		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera IC ON IC.idInspeccionC = ID.idInspeccionC		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_InspeccionProcesosCR IP ON IP.idProcesoCR = ID.idProcesoCR		LEFT JOIN PersonaMast P1 ON P1.Persona = IC.Mecanico		LEFT JOIN PersonaMast P2 ON P2.Persona = IC.Electrico		LEFT JOIN PersonaMast P3 ON P3.Persona = IC.Neumatico		LEFT JOIN PersonaMast P4 ON P4.Persona = IC.Soldador		WHERE (IC.Placa = @Placa) AND		ID.idInspeccionC = (SELECT MAX(idInspeccionC) FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera WHERE IC.Placa = @Placa)		ORDER BY ID.idInspeccionD ASC
	END

	IF (@Opcion = 6) BEGIN		-- BUSCAR INSPECCIONES POR ID - CORTINERA
		-- DECLARE @TipoV2 VARCHAR(250) = (SELECT SV.Descripcion FROM OP_TR_Vehiculo V LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo		--							   WHERE V.NumeroPlaca = @Placa)		SELECT ID.idInspeccionC, ID.idInspeccionD, IC.Placa, IP.ParteTracto AS 'COMPONENTE', IP.TipoProceso AS 'PROCESO', IC.FechaInicio AS 'INICIO_INSPECCION',		IC.FechaFin AS 'FIN_INSPECCION', ISNULL(IC.Mecanico,-1) AS 'Mecanico1', LTRIM(RTRIM(P1.NombreCompleto)) AS 'MECÁNICO', ISNULL(IC.Electrico,-1) AS 'Electrico2',		LTRIM(RTRIM(P2.NombreCompleto)) AS 'ELÉCTRICO', ISNULL(IC.Neumatico,-1) AS 'Neumatico3', LTRIM(RTRIM(P3.NombreCompleto)) AS 'NEUMÁTICO',		ISNULL(IC.Soldador,-1) AS 'Soldador4', LTRIM(RTRIM(P4.NombreCompleto)) AS 'SOLDADOR', ID.Estado AS 'ESTADO', ID.Observacion AS 'OBSERVACION'		FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_DetalleCR ID		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera IC ON IC.idInspeccionC = ID.idInspeccionC		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_InspeccionProcesosCR IP ON IP.idProcesoCR = ID.idProcesoCR		LEFT JOIN PersonaMast P1 ON P1.Persona = IC.Mecanico		LEFT JOIN PersonaMast P2 ON P2.Persona = IC.Electrico		LEFT JOIN PersonaMast P3 ON P3.Persona = IC.Neumatico		LEFT JOIN PersonaMast P4 ON P4.Persona = IC.Soldador		WHERE ID.idInspeccionC = @idInspeccionC		ORDER BY ID.idInspeccionD ASC
	END
END

-------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 15-08-2024
-- Description:	ACTUALIZAR INSPECCIÓN DE UNIDAD
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ActualizarInspeccion]
@Opcion INT,
@idInspeccionC INT,
@idInspeccionD INT,
@Estado VARCHAR(30),
@Observacion VARCHAR(250),
@Mecanico INT,
@Electrico INT,
@Neumatico INT,
@Soldador INT,
@FechaInicio DATETIME,
@FechaFin DATETIME
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo2 INT

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- AGREGAR ESTADO
		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Detalle
		SET Estado = @Estado, Observacion = @Observacion
		WHERE idInspeccionC = @idInspeccionC AND idInspeccionD = @idInspeccionD

		IF (@Estado = 'MALO') BEGIN
			DECLARE @NroPlaca VARCHAR(20) = (SELECT Placa FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera WHERE idInspeccionC = @idInspeccionC)
			
			SET @correlativo2 = (SELECT MAX(idMttoC) FROM ReportesApp_Mantenimiento_MttoCorrectivo_Registro)
			SET @correlativo2 = ISNULL(@correlativo2, 0) + 1

			INSERT INTO ReportesApp_Mantenimiento_MttoCorrectivo_Registro(idMttoC, Placa, Operacion, Origen, FechaReportada, Sistema,
			Descripcion, Observacion, Estado, UsuarioCreacion, FechaCreacion, UsuarioModificacion, FechaModificacion)
			SELECT @correlativo2, @NroPlaca, IC.Operacion, 'INSPECCION', IC.FechaCrea, IP.TipoProceso, IP.ParteTracto, ID.Observacion,
			'PENDIENTE', IC.UsuarioCrea, GETDATE(), IC.UsuarioCrea, GETDATE()
			FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Detalle ID
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera IC ON IC.idInspeccionC = ID.idInspeccionC
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_InspeccionProcesos IP ON IP.idProceso = ID.idProceso
			WHERE ID.idInspeccionC = @idInspeccionC AND ID.idInspeccionD = @idInspeccionD
		END

		SET @Exito = '0 = Inspeccion actualizada correctamente.'
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR ESTADO
		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Detalle
		SET Estado = NULL, Observacion = NULL
		WHERE idInspeccionC = @idInspeccionC AND idInspeccionD = @idInspeccionD

		SET @Exito = '0 = Inspeccion anulada correctamente.'
	END

	IF (@Opcion = 3) BEGIN		-- EDITAR INSPECCION
		IF (@FechaInicio >= @FechaFin) BEGIN
			SET @Exito = '-1 = La fecha de inicio no puede ser mayor a la fecha fin.'
			ROLLBACK
			GOTO Terminar
		END
		ELSE BEGIN
			UPDATE ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera
			SET Mecanico = @Mecanico, Electrico = @Electrico, Neumatico = @Neumatico, Soldador = @Soldador, FechaInicio = @FechaInicio, FechaFin = @FechaFin
			WHERE idInspeccionC = @idInspeccionC

			DECLARE @Placa VARCHAR(50) = (SELECT Placa FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera WHERE idInspeccionC = @idInspeccionC)

			IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cumplimiento WHERE Placa = @Placa AND
			CONVERT(DATE,@FechaInicio) BETWEEN FCInicio AND FCFin)) BEGIN
				UPDATE ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cumplimiento
				SET SAB = NULL, DOM = NULL, LUN = NULL, MAR = NULL, MIE = NULL, JUE = NULL, VIE = NULL
				WHERE Placa = @Placa AND CONVERT(DATE,@FechaInicio) BETWEEN FCInicio AND FCFin

				UPDATE ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cumplimiento
				SET FechaCumplimiento = CONVERT(DATE,@FechaInicio), Estado = 'EJECUTADO',
				SAB = CASE WHEN DATEPART(W,@FechaInicio) = 6 AND @FechaInicio >= FCInicio AND @FechaInicio <= FCFin THEN 'INS' ELSE SAB END,
				DOM = CASE WHEN DATEPART(W,@FechaInicio) = 7 AND @FechaInicio >= FCInicio AND @FechaInicio <= FCFin THEN 'INS' ELSE DOM END,
				LUN = CASE WHEN DATEPART(W,@FechaInicio) = 1 AND @FechaInicio >= FCInicio AND @FechaInicio <= FCFin THEN 'INS' ELSE LUN END,
				MAR = CASE WHEN DATEPART(W,@FechaInicio) = 2 AND @FechaInicio >= FCInicio AND @FechaInicio <= FCFin THEN 'INS' ELSE MAR END,
				MIE = CASE WHEN DATEPART(W,@FechaInicio) = 3 AND @FechaInicio >= FCInicio AND @FechaInicio <= FCFin THEN 'INS' ELSE MIE END,
				JUE = CASE WHEN DATEPART(W,@FechaInicio) = 4 AND @FechaInicio >= FCInicio AND @FechaInicio <= FCFin THEN 'INS' ELSE JUE END,
				VIE = CASE WHEN DATEPART(W,@FechaInicio) = 5 AND @FechaInicio >= FCInicio AND @FechaInicio <= FCFin THEN 'INS' ELSE VIE END
				WHERE Placa = @Placa AND CONVERT(DATE,@FechaInicio) BETWEEN FCInicio AND FCFin
			END
		END

		SET @Exito = '0 = Inspeccion actualizada correctamente.'
	END

	IF (@Opcion = 4) BEGIN		-- ELIMINAR ESTADO - CORTINERA
		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_DetalleCR
		SET Estado = NULL, Observacion = NULL
		WHERE idInspeccionC = @idInspeccionC AND idInspeccionD = @idInspeccionD

		SET @Exito = '0 = Inspeccion anulada correctamente.'
	END

	IF (@Opcion = 5) BEGIN		-- AGREGAR ESTADO - CORTINERA
		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_DetalleCR
		SET Estado = @Estado, Observacion = @Observacion
		WHERE idInspeccionC = @idInspeccionC AND idInspeccionD = @idInspeccionD

		IF (@Estado = 'MALO') BEGIN
			DECLARE @NroPlaca2 VARCHAR(20) = (SELECT Placa FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera WHERE idInspeccionC = @idInspeccionC)
			
			SET @correlativo2 = (SELECT MAX(idMttoC) FROM ReportesApp_Mantenimiento_MttoCorrectivo_Registro)
			SET @correlativo2 = ISNULL(@correlativo2, 0) + 1

			INSERT INTO ReportesApp_Mantenimiento_MttoCorrectivo_Registro(idMttoC, Placa, Operacion, Origen, FechaReportada, Sistema,
			Descripcion, Observacion, Estado, UsuarioCreacion, FechaCreacion, UsuarioModificacion, FechaModificacion)
			SELECT @correlativo2, @NroPlaca2, IC.Operacion, 'INSPECCION', IC.FechaCrea, ' ', IP.TipoProceso, ID.Observacion,
			'PENDIENTE', IC.UsuarioCrea, GETDATE(), IC.UsuarioCrea, GETDATE()
			FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_DetalleCR ID
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera IC ON IC.idInspeccionC = ID.idInspeccionC
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_InspeccionProcesosCR IP ON IP.idProcesoCR = ID.idProcesoCR
			WHERE ID.idInspeccionC = @idInspeccionC AND ID.idInspeccionD = @idInspeccionD
		END

		SET @Exito = '0 = Inspeccion actualizada correctamente.'
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

-------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 14-08-2024
-- Description:	LISTAR INSPECCIONES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarInspecciones]
@Opcion INT,
@Fecha VARCHAR(5),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@Placa VARCHAR(20),
@TipoMaquina VARCHAR(50)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@Opcion = 1) BEGIN		-- LISTAR ÚLTIMAS INSPECCIONES
		IF (@Fecha = 'FP') BEGIN
			IF (@TipoMaquina = 'TODOS') BEGIN
				SELECT X.NRO, X.idInspeccionC, X.idVehiculo, X.PLACA, X.TIPO_UNIDAD, X.SUBTIPO_UNIDAD, X.OPERACION, X.MARCA, X.MODELO,
				DATEADD(DAY,-10,(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END))
				AS 'PROX_INSPECCION', (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END)
				AS 'PROX_MTTO', X.INICIO_INSPECCION, X.FIN_INSPECCION, X.Mecanico1, X.MECÁNICO, X.Electrico2, X.ELÉCTRICO, X.Neumatico3, X.NEUMÁTICO, X.Soldador4, X.SOLDADOR, X.ESTADO
				FROM (SELECT MR.idRegistro AS 'NRO', IC.idInspeccionC, MR.idVehiculo, V.NumeroPlaca AS 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD', SV.Descripcion AS 'SUBTIPO_UNIDAD',
				CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', V.Modelo AS 'MODELO',
				ISNULL(IC.Mecanico,-1) AS 'Mecanico1', LTRIM(RTRIM(P1.NombreCompleto)) AS 'MECÁNICO', ISNULL(IC.Electrico,-1) AS 'Electrico2',
				LTRIM(RTRIM(P2.NombreCompleto)) AS 'ELÉCTRICO', ISNULL(IC.Neumatico,-1) AS 'Neumatico3', LTRIM(RTRIM(P3.NombreCompleto)) AS 'NEUMÁTICO',
				ISNULL(IC.Soldador,-1) AS 'Soldador4', LTRIM(RTRIM(P4.NombreCompleto)) AS 'SOLDADOR',
				CASE WHEN DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) = 0 THEN 1 ELSE CAST((KM.KMActual - MR.UltimoKM) / DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA',
				CAST(MR.Frecuencia + MR.UltimoKM AS DECIMAL(10,2)) AS 'KM_ESTIMADO', KM.KMActual AS 'KM_ACTUAL', KM.Fecha AS 'FECHA_ACTUAL', IC.FechaInicio AS 'INICIO_INSPECCION', 
				IC.FechaFin AS 'FIN_INSPECCION', (CASE WHEN (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Detalle WHERE Observacion != '' AND idInspeccionC = IC.idInspeccionC) OR
				EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_DetalleCR WHERE Observacion != '' AND idInspeccionC = IC.idInspeccionC)) THEN 'OBSERVADO'
				ELSE 'CONFORME' END) AS 'ESTADO'
				FROM ReportesApp_Mantenimiento_MttoPreventivo_Registro MR
				LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = MR.idVehiculo
				LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON UC.IdProgramacion = O.IdOperacion
				LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad AND Estado = 2 
				LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TipoVehiculo 
				LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
				LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
				LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = MR.idVehiculo
				LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera IC ON IC.Placa = V.NumeroPlaca AND (IC.Activo = 1 AND IC.Ultimo = 1)
				LEFT JOIN PersonaMast P1 ON P1.Persona = IC.Mecanico				LEFT JOIN PersonaMast P2 ON P2.Persona = IC.Electrico				LEFT JOIN PersonaMast P3 ON P3.Persona = IC.Neumatico
				LEFT JOIN PersonaMast P4 ON P4.Persona = IC.Soldador
				WHERE (V.TipoVehiculo = 1 OR V.TipoVehiculo = 2 /*OR SV.SubTipoVehiculo = 5*/)) X
				WHERE (DATEADD(DAY,-10,(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END)) 
				BETWEEN @FINICIO AND @FFIN) AND (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%')
				ORDER BY X.PLACA
			END
			ELSE BEGIN
				SELECT X.NRO, X.idInspeccionC, X.idVehiculo, X.PLACA, X.TIPO_UNIDAD, X.SUBTIPO_UNIDAD, X.OPERACION, X.MARCA, X.MODELO,
				DATEADD(DAY,-10,(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END))
				AS 'PROX_INSPECCION', (CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END)
				AS 'PROX_MTTO', X.INICIO_INSPECCION, X.FIN_INSPECCION, X.Mecanico1, X.MECÁNICO, X.Electrico2, X.ELÉCTRICO, X.Neumatico3, X.NEUMÁTICO, X.Soldador4, X.SOLDADOR, X.ESTADO
				FROM (SELECT MR.idRegistro AS 'NRO', IC.idInspeccionC, MR.idVehiculo, V.NumeroPlaca AS 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD', SV.Descripcion AS 'SUBTIPO_UNIDAD',
				CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', V.Modelo AS 'MODELO',
				ISNULL(IC.Mecanico,-1) AS 'Mecanico1', LTRIM(RTRIM(P1.NombreCompleto)) AS 'MECÁNICO', ISNULL(IC.Electrico,-1) AS 'Electrico2',
				LTRIM(RTRIM(P2.NombreCompleto)) AS 'ELÉCTRICO', ISNULL(IC.Neumatico,-1) AS 'Neumatico3', LTRIM(RTRIM(P3.NombreCompleto)) AS 'NEUMÁTICO',
				ISNULL(IC.Soldador,-1) AS 'Soldador4', LTRIM(RTRIM(P4.NombreCompleto)) AS 'SOLDADOR',
				CASE WHEN DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) = 0 THEN 1 ELSE CAST((KM.KMActual - MR.UltimoKM) / DATEDIFF(DAY, MR.UltimaFecha, KM.Fecha) AS DECIMAL(10,2)) END AS 'KM/DIA',
				CAST(MR.Frecuencia + MR.UltimoKM AS DECIMAL(10,2)) AS 'KM_ESTIMADO', KM.KMActual AS 'KM_ACTUAL', KM.Fecha AS 'FECHA_ACTUAL', IC.FechaInicio AS 'INICIO_INSPECCION', 
				IC.FechaFin AS 'FIN_INSPECCION', (CASE WHEN (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Detalle WHERE Observacion != '' AND idInspeccionC = IC.idInspeccionC) OR
				EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_DetalleCR WHERE Observacion != '' AND idInspeccionC = IC.idInspeccionC)) THEN 'OBSERVADO'
				ELSE 'CONFORME' END) AS 'ESTADO'
				FROM ReportesApp_Mantenimiento_MttoPreventivo_Registro MR
				LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = MR.idVehiculo
				LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON UC.IdProgramacion = O.IdOperacion
				LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad AND Estado = 2 
				LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TipoVehiculo 
				LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
				LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
				LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = MR.idVehiculo
				LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera IC ON IC.Placa = V.NumeroPlaca AND (IC.Activo = 1 AND IC.Ultimo = 1)
				LEFT JOIN PersonaMast P1 ON P1.Persona = IC.Mecanico				LEFT JOIN PersonaMast P2 ON P2.Persona = IC.Electrico				LEFT JOIN PersonaMast P3 ON P3.Persona = IC.Neumatico
				LEFT JOIN PersonaMast P4 ON P4.Persona = IC.Soldador
				WHERE (V.TipoVehiculo = 1 OR V.TipoVehiculo = 2 /*OR SV.SubTipoVehiculo = 5*/)) X
				WHERE (DATEADD(DAY,-10,(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END)) 
				BETWEEN @FINICIO AND @FFIN) AND (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Placa + '%') AND (X.TIPO_UNIDAD = @TipoMaquina)
				ORDER BY X.PLACA
			END
		END
		
		/*
		IF (@Fecha = 'FR') BEGIN
			IF (@TipoMaquina = 'TODOS') BEGIN
				SELECT IC.idInspeccionC AS 'NRO', IC.Placa AS 'PLACA', IC.Tipo AS 'TIPO_UNIDAD', IC.SubTipo AS 'SUBTIPO_UNIDAD', IC.Operacion AS 'OPERACION',
				IC.Marca AS 'MARCA', IC.Modelo AS 'MODELO', ISNULL(IC.Mecanico,-1) AS 'Mecanico1', LTRIM(RTRIM(P1.NombreCompleto)) AS 'MECÁNICO',				ISNULL(IC.Electrico,-1) AS 'Electrico2', LTRIM(RTRIM(P2.NombreCompleto)) AS 'ELÉCTRICO', ISNULL(IC.Neumatico,-1) AS 'Neumatico3',
				LTRIM(RTRIM(P3.NombreCompleto)) AS 'NEUMÁTICO', ISNULL(IC.Soldador,-1) AS 'Soldador4', LTRIM(RTRIM(P4.NombreCompleto)) AS 'SOLDADOR',
				DATEADD(DAY,-10,FechaProyectada) AS 'PROX_INSPECCION', FechaProyectada AS 'PROX_MTTO',
				IC.FechaCrea AS 'FECHA_INSPECCION', (CASE WHEN (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Detalle
				WHERE Observacion != '' AND idInspeccionC = IC.idInspeccionC)) THEN 'OBSERVADO' ELSE 'CONFORME' END) AS 'ESTADO',
				(CASE WHEN (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Pedidos WHERE idInspeccionC = IC.idInspeccionC))
				THEN 'PEDIDO' ELSE 'SIN PEDIR' END) AS 'ESTADO_PEDIDO'
				FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera IC
				LEFT JOIN PersonaMast P1 ON P1.Persona = IC.Mecanico				LEFT JOIN PersonaMast P2 ON P2.Persona = IC.Electrico				LEFT JOIN PersonaMast P3 ON P3.Persona = IC.Neumatico
				LEFT JOIN PersonaMast P4 ON P4.Persona = IC.Soldador
				WHERE (IC.Ultimo = 1) AND (IC.Activo = 1)
				AND (IC.FechaCrea BETWEEN @FINICIO AND @FFIN) AND (IC.Placa IS NULL OR IC.Placa LIKE '%' + @Placa + '%') 
			END
			ELSE BEGIN
				SELECT IC.idInspeccionC AS 'NRO', IC.Placa AS 'PLACA', IC.Tipo AS 'TIPO_UNIDAD', IC.SubTipo AS 'SUBTIPO_UNIDAD', IC.Operacion AS 'OPERACION',
				IC.Marca AS 'MARCA', IC.Modelo AS 'MODELO', ISNULL(IC.Mecanico,-1) AS 'Mecanico1', LTRIM(RTRIM(P1.NombreCompleto)) AS 'MECÁNICO',				ISNULL(IC.Electrico,-1) AS 'Electrico2', LTRIM(RTRIM(P2.NombreCompleto)) AS 'ELÉCTRICO', ISNULL(IC.Neumatico,-1) AS 'Neumatico3',
				LTRIM(RTRIM(P3.NombreCompleto)) AS 'NEUMÁTICO', ISNULL(IC.Soldador,-1) AS 'Soldador4', LTRIM(RTRIM(P4.NombreCompleto)) AS 'SOLDADOR',
				DATEADD(DAY,-10,FechaProyectada) AS 'PROX_INSPECCION', FechaProyectada AS 'PROX_MTTO',
				IC.FechaCrea AS 'FECHA_INSPECCION', (CASE WHEN (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Detalle
				WHERE Observacion != '' AND idInspeccionC = IC.idInspeccionC)) THEN 'OBSERVADO' ELSE 'CONFORME' END) AS 'ESTADO',
				(CASE WHEN (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Pedidos WHERE idInspeccionC = IC.idInspeccionC))
				THEN 'PEDIDO' ELSE 'SIN PEDIR' END) AS 'ESTADO_PEDIDO'
				FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera IC
				LEFT JOIN PersonaMast P1 ON P1.Persona = IC.Mecanico				LEFT JOIN PersonaMast P2 ON P2.Persona = IC.Electrico				LEFT JOIN PersonaMast P3 ON P3.Persona = IC.Neumatico
				LEFT JOIN PersonaMast P4 ON P4.Persona = IC.Soldador
				WHERE (IC.Ultimo = 1) AND (IC.Activo = 1)
				AND (IC.FechaCrea BETWEEN @FINICIO AND @FFIN) AND (IC.Placa IS NULL OR IC.Placa LIKE '%' + @Placa + '%')
				AND (IC.Tipo = @TipoMaquina)
			END
		END
		*/
	END

	IF (@Opcion = 2) BEGIN		-- LISTAR HISTORIAL DE INSPECCIONES
		IF (@TipoMaquina = 'TODOS') BEGIN
			SELECT IC.idInspeccionC AS 'NRO', IC.Placa AS 'PLACA', IC.Tipo AS 'TIPO_UNIDAD', IC.SubTipo AS 'SUBTIPO_UNIDAD', IC.Operacion AS 'OPERACION',
			IC.Marca AS 'MARCA', IC.Modelo AS 'MODELO', ISNULL(IC.Mecanico,-1) AS 'Mecanico1', LTRIM(RTRIM(P1.NombreCompleto)) AS 'MECÁNICO',			ISNULL(IC.Electrico,-1) AS 'Electrico2', LTRIM(RTRIM(P2.NombreCompleto)) AS 'ELÉCTRICO', ISNULL(IC.Neumatico,-1) AS 'Neumatico3',
			LTRIM(RTRIM(P3.NombreCompleto)) AS 'NEUMÁTICO', ISNULL(IC.Soldador,-1) AS 'Soldador4', LTRIM(RTRIM(P4.NombreCompleto)) AS 'SOLDADOR',
			DATEADD(DAY,-10,IC.FechaProyectada) AS 'PROX_INSPECCION', IC.FechaProyectada AS 'PROX_MTTO', IC.FechaInicio AS 'INICIO_INSPECCION', 
			IC.FechaFin AS 'FIN_INSPECCION', (CASE WHEN (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Detalle WHERE Observacion != '' AND
			idInspeccionC = IC.idInspeccionC) OR EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_DetalleCR WHERE Observacion != ''
			AND idInspeccionC = IC.idInspeccionC)) THEN 'OBSERVADO' ELSE 'CONFORME' END) AS 'ESTADO'
			FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera IC
			LEFT JOIN PersonaMast P1 ON P1.Persona = IC.Mecanico			LEFT JOIN PersonaMast P2 ON P2.Persona = IC.Electrico			LEFT JOIN PersonaMast P3 ON P3.Persona = IC.Neumatico
			LEFT JOIN PersonaMast P4 ON P4.Persona = IC.Soldador
			WHERE (IC.Ultimo = 0) AND (IC.Activo = 0) AND (IC.FechaInicio BETWEEN @FINICIO AND @FFIN) AND (IC.Placa IS NULL OR IC.Placa LIKE '%' + @Placa + '%') 
			ORDER BY IC.FechaInicio DESC
		END
		ELSE BEGIN
			SELECT IC.idInspeccionC AS 'NRO', IC.Placa AS 'PLACA', IC.Tipo AS 'TIPO_UNIDAD', IC.SubTipo AS 'SUBTIPO_UNIDAD', IC.Operacion AS 'OPERACION',
			IC.Marca AS 'MARCA', IC.Modelo AS 'MODELO', ISNULL(IC.Mecanico,-1) AS 'Mecanico1', LTRIM(RTRIM(P1.NombreCompleto)) AS 'MECÁNICO',			ISNULL(IC.Electrico,-1) AS 'Electrico2', LTRIM(RTRIM(P2.NombreCompleto)) AS 'ELÉCTRICO', ISNULL(IC.Neumatico,-1) AS 'Neumatico3',
			LTRIM(RTRIM(P3.NombreCompleto)) AS 'NEUMÁTICO', ISNULL(IC.Soldador,-1) AS 'Soldador4', LTRIM(RTRIM(P4.NombreCompleto)) AS 'SOLDADOR',
			DATEADD(DAY,-10,IC.FechaProyectada) AS 'PROX_INSPECCION', IC.FechaProyectada AS 'PROX_MTTO', IC.FechaInicio AS 'INICIO_INSPECCION', 
			IC.FechaFin AS 'FIN_INSPECCION', (CASE WHEN (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Detalle WHERE Observacion != '' AND
			idInspeccionC = IC.idInspeccionC) OR EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_DetalleCR WHERE Observacion != ''
			AND idInspeccionC = IC.idInspeccionC)) THEN 'OBSERVADO' ELSE 'CONFORME' END) AS 'ESTADO'
			FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera IC
			LEFT JOIN PersonaMast P1 ON P1.Persona = IC.Mecanico			LEFT JOIN PersonaMast P2 ON P2.Persona = IC.Electrico			LEFT JOIN PersonaMast P3 ON P3.Persona = IC.Neumatico
			LEFT JOIN PersonaMast P4 ON P4.Persona = IC.Soldador
			WHERE (IC.Ultimo = 0) AND (IC.Activo = 0) AND (IC.FechaInicio BETWEEN @FINICIO AND @FFIN) AND (IC.Placa IS NULL OR IC.Placa LIKE '%' + @Placa + '%')
			AND (IC.Tipo = @TipoMaquina)
			ORDER BY IC.FechaInicio DESC
		END
	END

	IF (@Opcion = 3) BEGIN		-- LISTAR OBSERVACIONES
		IF (@TipoMaquina = 'TODOS') BEGIN
			SELECT ID.idInspeccionD, ID.idInspeccionC, IC.Placa AS 'PLACA', IC.Tipo AS 'TIPO_UNIDAD', IC.SubTipo AS 'SUBTIPO_UNIDAD', IC.Operacion AS 'OPERACION',
			IC.Marca AS 'MARCA', IC.Modelo AS 'MODELO', IC.FechaInicio AS 'INICIO_INSPECCION', IC.FechaFin AS 'FIN_INSPECCION', IP.TipoProceso AS 'COMPONENTE',
			IP.ParteTracto AS 'PROCESO', ID.Observacion AS 'OBSERVACION'
			FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Detalle ID
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera IC ON IC.idInspeccionC = ID.idInspeccionC
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_InspeccionProcesos IP ON IP.idProceso = ID.idProceso
			WHERE (ID.Observacion != '') AND (IC.Activo = 1) AND
			(IC.FechaCrea BETWEEN @FINICIO AND @FFIN) AND (IC.Placa IS NULL OR IC.Placa LIKE '%' + @Placa + '%')
			UNION
			SELECT ID.idInspeccionD, ID.idInspeccionC, IC.Placa AS 'PLACA', IC.Tipo AS 'TIPO_UNIDAD', IC.SubTipo AS 'SUBTIPO_UNIDAD', IC.Operacion AS 'OPERACION',
			IC.Marca AS 'MARCA', IC.Modelo AS 'MODELO', IC.FechaInicio AS 'INICIO_INSPECCION', IC.FechaFin AS 'FIN_INSPECCION', ' ' AS 'COMPONENTE',
			IP.TipoProceso AS 'PROCESO', ID.Observacion AS 'OBSERVACION'
			FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_DetalleCR ID
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera IC ON IC.idInspeccionC = ID.idInspeccionC
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_InspeccionProcesosCR IP ON IP.idProcesoCR = ID.idProcesoCR
			WHERE (ID.Observacion != '') AND (IC.Activo = 1) AND
			(IC.FechaInicio BETWEEN @FINICIO AND @FFIN) AND (IC.Placa IS NULL OR IC.Placa LIKE '%' + @Placa + '%')
			ORDER BY IC.FechaInicio DESC
		END
		ELSE BEGIN
			SELECT ID.idInspeccionD, ID.idInspeccionC, IC.Placa AS 'PLACA', IC.Tipo AS 'TIPO_UNIDAD', IC.SubTipo AS 'SUBTIPO_UNIDAD', IC.Operacion AS 'OPERACION',
			IC.Marca AS 'MARCA', IC.Modelo AS 'MODELO', IC.FechaInicio AS 'INICIO_INSPECCION', IC.FechaFin AS 'FIN_INSPECCION', IP.TipoProceso AS 'COMPONENTE',
			IP.ParteTracto AS 'PROCESO', ID.Observacion AS 'OBSERVACION'
			FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Detalle ID
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera IC ON IC.idInspeccionC = ID.idInspeccionC
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_InspeccionProcesos IP ON IP.idProceso = ID.idProceso
			WHERE (ID.Observacion != '') AND (IC.Activo = 1) AND
			(IC.FechaCrea BETWEEN @FINICIO AND @FFIN) AND (IC.Placa IS NULL OR IC.Placa LIKE '%' + @Placa + '%') AND (IC.Tipo = @TipoMaquina)
			UNION
			SELECT ID.idInspeccionD, ID.idInspeccionC, IC.Placa AS 'PLACA', IC.Tipo AS 'TIPO_UNIDAD', IC.SubTipo AS 'SUBTIPO_UNIDAD', IC.Operacion AS 'OPERACION',
			IC.Marca AS 'MARCA', IC.Modelo AS 'MODELO', IC.FechaInicio AS 'INICIO_INSPECCION', IC.FechaFin AS 'FIN_INSPECCION', ' ' AS 'COMPONENTE',
			IP.TipoProceso AS 'PROCESO', ID.Observacion AS 'OBSERVACION'
			FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_DetalleCR ID
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera IC ON IC.idInspeccionC = ID.idInspeccionC
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_InspeccionProcesosCR IP ON IP.idProcesoCR = ID.idProcesoCR
			WHERE (ID.Observacion != '') AND (IC.Activo = 1) AND
			(IC.FechaInicio BETWEEN @FINICIO AND @FFIN) AND (IC.Placa IS NULL OR IC.Placa LIKE '%' + @Placa + '%') AND (IC.Tipo = @TipoMaquina)
			ORDER BY IC.FechaInicio DESC
		END
	END
END

-------------------------------------------------------------------------------
-------------------------------------------------------------------------------

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Pedidos

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 19-08-2024
-- Description:	LISTAR PEDIDOS DE INSPECCIONES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarPedidosInspecciones]
@idInspeccionC INT
AS
BEGIN	SELECT P.idNroPedido AS 'NRO', P.idInspeccionC, IC.Placa AS 'PLACA', P.Item AS 'CODIGO', P.Descripcion AS 'ITEM', P.Cantidad AS 'CANTIDAD',	P.FechaCreacion AS 'FECHA_PEDIDO', (SELECT TOP(1) TH.FechaDocumento FROM ME_OrdenTrabajoRecurso R
	INNER JOIN ME_OrdenTrabajo OT WITH(NOLOCK) ON R.CompaniaSocio = OT.CompaniaSocio AND R.NumeroOrden = OT.NumeroOrden
	LEFT JOIN WH_TransaccionDetalle TD WITH(NOLOCK) ON R.NumeroOrden = TD.ReferenciaNumeroDocumento AND R.Recurso = TD.Item AND TD.ReferenciaTipoDocumento = 'OT' 
	INNER JOIN WH_TransaccionHeader TH WITH(NOLOCK) ON TH.CompaniaSocio = TD.CompaniaSocio AND TH.TipoDocumento = TD.TipoDocumento AND
													   TH.NumeroDocumento = TD.NumeroDocumento AND TH.AlmacenCodigo = OT.AlmacenCodigo AND TH.Estado<>'AN'
	INNER JOIN WH_AlmacenMast A ON A.AlmacenCodigo = TH.AlmacenCodigo AND A.TipoAlmacen='P'
	WHERE LTRIM(RTRIM(OT.MaquinaCodigo)) = IC.Placa AND LTRIM(RTRIM(R.Recurso)) = P.Item ORDER BY TH.FechaDocumento DESC) AS 'FECHA_DESPACHO',	DATEDIFF(DAY,(SELECT TOP(1) TH.FechaDocumento FROM ME_OrdenTrabajoRecurso R
	INNER JOIN ME_OrdenTrabajo OT WITH(NOLOCK) ON R.CompaniaSocio = OT.CompaniaSocio AND R.NumeroOrden = OT.NumeroOrden
	LEFT JOIN WH_TransaccionDetalle TD WITH(NOLOCK) ON R.NumeroOrden = TD.ReferenciaNumeroDocumento AND R.Recurso = TD.Item AND TD.ReferenciaTipoDocumento = 'OT' 
	INNER JOIN WH_TransaccionHeader TH WITH(NOLOCK) ON TH.CompaniaSocio = TD.CompaniaSocio AND TH.TipoDocumento = TD.TipoDocumento AND
													   TH.NumeroDocumento = TD.NumeroDocumento AND TH.AlmacenCodigo = OT.AlmacenCodigo AND TH.Estado<>'AN'
	INNER JOIN WH_AlmacenMast A ON A.AlmacenCodigo = TH.AlmacenCodigo AND A.TipoAlmacen='P'
	WHERE LTRIM(RTRIM(OT.MaquinaCodigo)) = IC.Placa AND LTRIM(RTRIM(R.Recurso)) = P.Item ORDER BY TH.FechaDocumento DESC),P.FechaCreacion) AS 'INTERVALO'	FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Pedidos P	LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera IC ON IC.idInspeccionC = P.idInspeccionC	WHERE P.idInspeccionC = @idInspeccionC	ORDER BY P.idNroPedido DESC
END

------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 19-08-2024
-- Description:	INSERTAR PEDIDOS DE INSPECCIONES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_InsertarPedidosInspecciones]
@Opcion INT,
@idNroPedido INT,
@idInspeccionC INT,
@Item VARCHAR(30),
@Descripcion VARCHAR(500),
@Cantidad DECIMAL(10,2),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- CREAR PEDIDO
		SET @correlativo = (SELECT MAX(idNroPedido) FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Pedidos WHERE idInspeccionC = @idInspeccionC)
		SET @correlativo = ISNULL(@correlativo,0) + 1

		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Pedidos(idNroPedido,idInspeccionC,Item,Descripcion,Cantidad,UsuarioCreacion,FechaCreacion)
		VALUES(@correlativo,@idInspeccionC,LTRIM(RTRIM(@Item)),@Descripcion,@Cantidad,@Usuario,GETDATE())
				
		SET @Exito = '0 = Se ha generado el pedido para esta inspección.'
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR PEDIDOS
		DELETE FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Pedidos
		WHERE idInspeccionC = @idInspeccionC AND idNroPedido = @idNroPedido

		SET @Exito = '0 = Pedido eliminado correctamente.'
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

-------------------------------------------------------------------------------
-------------------------------------------------------------------------------

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_RegistroEquipos

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_HistorialEquipos

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMttoEquipos

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_HistorialProcesoEquipos

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 20-05-2025
-- Description:	GENERAR REGISTRO DE EQUIPOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_GenerarModificarEquipos]
@Opcion INT,
@idRegistro INT,
@Codigo VARCHAR(50),
@Periodo INT,
@TipoMaquina VARCHAR(250),
@EquipoNombre VARCHAR(100),
@Equipo VARCHAR(300),
@Ubicacion VARCHAR(250),
@Marca VARCHAR(250),
@Modelo VARCHAR(250),
@UltimaFecha DATETIME,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- INSERTAR
		SET @correlativo = (SELECT MAX(idRegistro) FROM ReportesApp_Mantenimiento_MttoPreventivo_RegistroEquipos)
		SET @correlativo = ISNULL(@correlativo,0) + 1

		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_RegistroEquipos (idRegistro, Codigo, Periodo, TipoMaquina, EquipoNombre, Equipo,
		Ubicacion, Marca, Modelo, UltimaFecha, Usuario, FechaCreacion)
		VALUES(@correlativo, @Codigo, @Periodo, @TipoMaquina, @EquipoNombre, @Equipo, @Ubicacion, @Marca, @Modelo, @UltimaFecha, @Usuario, GETDATE())

		SET @Exito = '0 = Mantenimiento Programado Correctamente.'
	END

	IF (@Opcion = 2) BEGIN		-- ACTUALIZAR FECHA
		IF ((SELECT CONVERT(DATE,UltimaFecha) FROM ReportesApp_Mantenimiento_MttoPreventivo_RegistroEquipos WHERE idRegistro = @idRegistro) >= CONVERT(DATE,GETDATE())) BEGIN
			SET @Exito = '-1 = No puede registrar fechas que sean mayores a la fecha actual.'
			ROLLBACK
			GOTO Terminar
		END
		
		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_HistorialEquipos (idRegistro, Codigo, Periodo, TipoMaquina, EquipoNombre, Equipo,
		Ubicacion, Marca, Modelo, UltimaFecha, Usuario, FechaCreacion)
		SELECT idRegistro, Codigo, Periodo, TipoMaquina, EquipoNombre, Equipo, Ubicacion, Marca, Modelo, UltimaFecha, Usuario, GETDATE()
		FROM ReportesApp_Mantenimiento_MttoPreventivo_RegistroEquipos
		WHERE idRegistro = @idRegistro
		
		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_RegistroEquipos
		SET UltimaFecha = @UltimaFecha, Usuario = @Usuario, FechaCreacion = GETDATE()
		WHERE idRegistro = @idRegistro 

		SET @Exito = '0 = Fecha Actualizada Correctamente.'
	END

	IF (@Opcion = 3) BEGIN		-- EDITAR DATOS
		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_RegistroEquipos
		SET Codigo = @Codigo, TipoMaquina = @TipoMaquina, EquipoNombre = @EquipoNombre, Equipo = @Equipo, Periodo = @Periodo, Ubicacion = @Ubicacion,
		Marca = @Marca, Modelo = @Modelo, Usuario = @Usuario, FechaCreacion = GETDATE()
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

--------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 21-05-2025
-- Description:	LISTAR MANTENIMIENTOS EQUIPOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarMttosEquipos]
@Equipo VARCHAR(350),
@Tipo VARCHAR(250),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'

BEGIN
	IF (@Tipo = 'TODOS') BEGIN
		SELECT X.idRegistro, X.PLACA, X.GRUPO, X.MAQUINARIA, X.DESCRIPCION, X.UBICACION, X.MARCA, X.MODELO, X.PERIODO, X.FECHA_ULTIMA,
		X.FECHA_ACTUAL, X.[PORCENTAJE (%)], (CASE WHEN X.[PORCENTAJE (%)] < 60 THEN 'CONFORME'
		WHEN X.[PORCENTAJE (%)] >= 60 AND X.[PORCENTAJE (%)] < 100 THEN 'POR VENCER'
		WHEN X.[PORCENTAJE (%)] >= 100 THEN 'VENCIDO' ELSE '' END) AS 'ESTADO',
		X.DIAS_PASADOS, X.DIAS_FALTANTES, X.PROXIMA_FECHA, X.UsuarioRegistro, X.FechaRegistro FROM
		(SELECT E.idRegistro, E.Codigo AS 'PLACA', E.TipoMaquina AS 'GRUPO', E.EquipoNombre AS 'MAQUINARIA', E.Equipo AS 'DESCRIPCION',
		E.Ubicacion AS 'UBICACION', E.Marca AS 'MARCA', E.Modelo AS 'MODELO', E.Periodo AS 'PERIODO', E.UltimaFecha AS 'FECHA_ULTIMA', GETDATE() AS 'FECHA_ACTUAL',
		CAST(CONVERT(DECIMAL,DATEDIFF(DAY, E.UltimaFecha, GETDATE())) / CONVERT(DECIMAL,E.Periodo) * 100 AS DECIMAL(10,2)) AS 'PORCENTAJE (%)',
		DATEDIFF(DAY, E.UltimaFecha, GETDATE()) AS 'DIAS_PASADOS', E.Periodo - DATEDIFF(DAY, E.UltimaFecha, GETDATE()) AS 'DIAS_FALTANTES',
		DATEADD(DAY,E.Periodo,E.UltimaFecha) AS 'PROXIMA_FECHA', E.Usuario AS 'UsuarioRegistro', E.FechaCreacion AS 'FechaRegistro'
		FROM ReportesApp_Mantenimiento_MttoPreventivo_RegistroEquipos E) X
		WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Equipo + '%')
		AND (X.PROXIMA_FECHA BETWEEN @FINICIO AND @FFIN)
		ORDER BY X.PLACA ASC
	END
	ELSE BEGIN
		SELECT X.idRegistro, X.PLACA, X.GRUPO, X.MAQUINARIA, X.DESCRIPCION, X.UBICACION, X.MARCA, X.MODELO, X.PERIODO, X.FECHA_ULTIMA,
		X.FECHA_ACTUAL, X.[PORCENTAJE (%)], (CASE WHEN X.[PORCENTAJE (%)] < 60 THEN 'CONFORME'
		WHEN X.[PORCENTAJE (%)] >= 60 AND X.[PORCENTAJE (%)] < 100 THEN 'POR VENCER'
		WHEN X.[PORCENTAJE (%)] >= 100 THEN 'VENCIDO' ELSE '' END) AS 'ESTADO',
		X.DIAS_PASADOS, X.DIAS_FALTANTES, X.PROXIMA_FECHA, X.UsuarioRegistro, X.FechaRegistro FROM
		(SELECT E.idRegistro, E.Codigo AS 'PLACA', E.TipoMaquina AS 'GRUPO', E.EquipoNombre AS 'MAQUINARIA', E.Equipo AS 'DESCRIPCION',
		E.Ubicacion AS 'UBICACION', E.Marca AS 'MARCA', E.Modelo AS 'MODELO', E.Periodo AS 'PERIODO', E.UltimaFecha AS 'FECHA_ULTIMA', GETDATE() AS 'FECHA_ACTUAL',
		CAST(CONVERT(DECIMAL,DATEDIFF(DAY, E.UltimaFecha, GETDATE())) / CONVERT(DECIMAL,E.Periodo) * 100 AS DECIMAL(10,2)) AS 'PORCENTAJE (%)',
		DATEDIFF(DAY, E.UltimaFecha, GETDATE()) AS 'DIAS_PASADOS', E.Periodo - DATEDIFF(DAY, E.UltimaFecha, GETDATE()) AS 'DIAS_FALTANTES',
		DATEADD(DAY,E.Periodo,E.UltimaFecha) AS 'PROXIMA_FECHA', E.Usuario AS 'UsuarioRegistro', E.FechaCreacion AS 'FechaRegistro'
		FROM ReportesApp_Mantenimiento_MttoPreventivo_RegistroEquipos E) X
		WHERE (X.PLACA IS NULL OR X.PLACA LIKE '%' + @Equipo + '%') AND (X.MAQUINARIA = @Tipo)
		AND (X.PROXIMA_FECHA BETWEEN @FINICIO AND @FFIN)
		ORDER BY X.PLACA ASC
	END
END

--------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 22-05-2025
-- Description:	LISTAR HISTORIAL DE EQUIPOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialEquipos]
@Equipo VARCHAR(250),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@TipoMaquina VARCHAR(250)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@TipoMaquina = 'TODOS') BEGIN
		SELECT Codigo AS 'PLACA', TipoMaquina AS 'GRUPO', EquipoNombre AS 'MAQUINARIA', Equipo AS 'DESCRIPCION',
		Ubicacion AS 'UBICACION', Marca AS 'MARCA', Modelo AS 'MODELO', Periodo AS 'PERIODO', UltimaFecha AS 'FECHA_ULTIMA',
		Usuario AS 'UsuarioRegistro', FechaCreacion AS 'FechaRegistro'
		FROM ReportesApp_Mantenimiento_MttoPreventivo_HistorialEquipos
		WHERE (Codigo IS NULL OR Codigo LIKE '%' + @Equipo + '%') AND (UltimaFecha BETWEEN @FINICIO AND @FFIN)
		ORDER BY UltimaFecha DESC
	END
	ELSE BEGIN
		SELECT Codigo AS 'PLACA', TipoMaquina AS 'GRUPO', EquipoNombre AS 'MAQUINARIA', Equipo AS 'DESCRIPCION',
		Ubicacion AS 'UBICACION', Marca AS 'MARCA', Modelo AS 'MODELO', Periodo AS 'PERIODO', UltimaFecha AS 'FECHA_ULTIMA',
		Usuario AS 'UsuarioRegistro', FechaCreacion AS 'FechaRegistro'
		FROM ReportesApp_Mantenimiento_MttoPreventivo_HistorialEquipos
		WHERE (Codigo IS NULL OR Codigo LIKE '%' + @Equipo + '%') AND (EquipoNombre = @TipoMaquina)
		AND (UltimaFecha BETWEEN @FINICIO AND @FFIN)
		ORDER BY UltimaFecha DESC
	END
END

-------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 24-05-2025
-- Description:	GENERAR CONTROL MANTENIMIENTO - EQUIPOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_AgregarModificarControlEquipos]
@idRegistro INT,
@idAccesorio INT,
@FechaCambio DATETIME,
@Periodo INT,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

BEGIN TRAN
BEGIN TRY
	IF (CONVERT(DATE,@FechaCambio) > CONVERT(DATE,GETDATE())) BEGIN
		SET @Exito = '-1 = No puede registrar fechas que sean mayores a la fecha actual.'
		ROLLBACK
		GOTO Terminar
	END
	ELSE BEGIN
		IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMttoEquipos WHERE idRegistro = @idRegistro AND idAccesorio = @idAccesorio)) BEGIN
			INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_HistorialProcesoEquipos(idProcesoMtto,idRegistro,idAccesorio,FechaCambio,Periodo,Usuario,Fecha)
			SELECT idProcesoMtto,idRegistro,idAccesorio,FechaCambio,Periodo,@Usuario,GETDATE()
			FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMttoEquipos
			WHERE idRegistro = @idRegistro AND idAccesorio = @idAccesorio

			UPDATE ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMttoEquipos
			SET FechaCambio = @FechaCambio, Periodo = @Periodo, Usuario = @Usuario, Fecha = GETDATE()
			WHERE idRegistro = @idRegistro AND idAccesorio = @idAccesorio

			SET @Exito = '0 = Mantenimiento Actualizado Correctamente.'
		END
		ELSE BEGIN
			SET @correlativo = (SELECT MAX(idProcesoMtto) FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMttoEquipos WHERE idRegistro = @idRegistro)
			SET @correlativo = ISNULL(@correlativo,0) + 1

			INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMttoEquipos(idProcesoMtto,idRegistro,idAccesorio,FechaCambio,Periodo,Usuario,Fecha)
			VALUES(@correlativo, @idRegistro, @idAccesorio, @FechaCambio, @Periodo, @Usuario, GETDATE())

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

-------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 24-05-2025
-- Description:	LISTAR MANTENIMIENTOS DE EQUIPOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarControlMttoEquipos]
@idRegistro INT
AS
BEGIN
	SELECT X.idProcesoMtto, X.idAccesorio, X.ACCESORIO, X.idRegistro, X.FECHA_CAMBIO, X.PERIODO, X.FECHA_ACTUAL, X.[%],
	(CASE WHEN X.[%] < 60 THEN 'CONFORME'
	WHEN X.[%] >= 60 AND X.[%] < 100 THEN 'POR VENCER'
	WHEN X.[%] >= 100 THEN 'VENCIDO' ELSE '' END) AS 'ESTADO',
	X.DIAS_PASADOS, X.DIAS_FALTANTES, X.PROXIMA_FECHA, X.UsuarioRegistro, X.FechaRegistro FROM
	(SELECT PM.idProcesoMtto, PM.idAccesorio, AC.Descripcion AS 'ACCESORIO', PM.idRegistro,
	PM.FechaCambio AS 'FECHA_CAMBIO', PM.Periodo AS 'PERIODO', GETDATE() AS 'FECHA_ACTUAL',
	CAST(CONVERT(DECIMAL,DATEDIFF(DAY, PM.FechaCambio, GETDATE())) / CONVERT(DECIMAL,PM.Periodo) * 100 AS DECIMAL(10,2)) AS '%',
	DATEDIFF(DAY, PM.FechaCambio, GETDATE()) AS 'DIAS_PASADOS', PM.Periodo - DATEDIFF(DAY, PM.FechaCambio, GETDATE()) AS 'DIAS_FALTANTES',
	DATEADD(DAY,PM.Periodo,PM.FechaCambio) AS 'PROXIMA_FECHA', PM.Usuario AS 'UsuarioRegistro', PM.Fecha AS 'FechaRegistro'
	FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMttoEquipos PM
	LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Accesorios AC ON AC.idAccesorio = PM.idAccesorio
	WHERE PM.idRegistro = @idRegistro) X
	ORDER BY X.idAccesorio ASC
END

-------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 26-05-2025
-- Description:	LISTAR PROCESOS HISTORIAL EQUIPOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarHistorialProcesosEquipos]
@idRegistro INT,
@idAccesorio INT
AS
BEGIN
	IF (@idAccesorio = 0) BEGIN
		SELECT TOP(30) PM.idProcesoMtto, PM.idRegistro, AC.Descripcion AS 'ACCESORIO', PM.FechaCambio AS 'FECHA_CAMBIO',
		PM.Periodo AS 'PERIODO', PM.Usuario, PM.Fecha FROM ReportesApp_Mantenimiento_MttoPreventivo_HistorialProcesoEquipos PM
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Accesorios AC ON AC.idAccesorio = PM.idAccesorio
		WHERE PM.idRegistro = @idRegistro
		ORDER BY PM.Fecha DESC
	END
	ELSE BEGIN
		SELECT TOP(30) PM.idProcesoMtto, PM.idRegistro, AC.Descripcion AS 'ACCESORIO', PM.FechaCambio AS 'FECHA_CAMBIO',
		PM.Periodo AS 'PERIODO', PM.Usuario, PM.Fecha FROM ReportesApp_Mantenimiento_MttoPreventivo_HistorialProcesoEquipos PM
		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Accesorios AC ON AC.idAccesorio = PM.idAccesorio
		WHERE (PM.idRegistro = @idRegistro) AND (PM.idAccesorio = @idAccesorio)
		ORDER BY PM.Fecha DESC
	END
END

-------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 26-05-2025
-- Description:	ELIMINAR PROCESOS MTTO EQUIPOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_EliminarProcesosEquipos]
@Opcion INT,
@idProcesoMtto INT,
@idRegistro INT,
@Periodo INT
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- ELIMINAR PROCESO
		DELETE FROM ReportesApp_Mantenimiento_MttoPreventivo_ProcesoMttoEquipos
		WHERE idProcesoMtto = @idProcesoMtto AND idRegistro = @idRegistro
		
		DELETE FROM ReportesApp_Mantenimiento_MttoPreventivo_HistorialProcesoEquipos
		WHERE idProcesoMtto = @idProcesoMtto AND idRegistro = @idRegistro

		/*
		DELETE FROM ReportesApp_Mantenimiento_MttoPreventivo_Recursos
		WHERE idProcesoMtto = @idProcesoMtto AND idVehiculo = @idVehiculo

		DELETE FROM ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_Registros
		WHERE idProcesoMtto = @idProcesoMtto AND idVehiculo = @idVehiculo
		*/

		SET @Exito = '0 = Mantenimiento Eliminado Correctamente.'
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR HISTORIAL
		DELETE FROM ReportesApp_Mantenimiento_MttoPreventivo_HistorialProcesoEquipos
		WHERE idProcesoMtto = @idProcesoMtto AND idRegistro = @idRegistro AND Periodo = @Periodo
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

-------------------------------------------------------------------------------------------

/*
DECLARE @ListaTractos TABLE (Nro INT, Placa VARCHAR(50), Tipo VARCHAR(250), SubTipo VARCHAR(250), Operacion VARCHAR(50), Marca VARCHAR(250),
Modelo VARCHAR(250), FechaProyectada DATE)
DECLARE @Contador INT = 1

INSERT INTO @ListaTractos (Nro, Placa, Tipo, SubTipo, Operacion, Marca, Modelo, FechaProyectada)
SELECT ROW_NUMBER() OVER (ORDER BY X.PLACA), X.PLACA, X.TIPO_UNIDAD, X.SUBTIPO_UNIDAD, X.OPERACION, X.MARCA, X.MODELO,
CONVERT(DATE,(CASE WHEN ROUND(X.[KM/DIA],0) <= 0 THEN GETDATE() ELSE DATEADD(DAY,ROUND((X.KM_ESTIMADO-X.KM_ACTUAL)/X.[KM/DIA],0),X.FECHA_ACTUAL) END)) AS 'FECHA_PROYECTADA' FROM 
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
WHERE X.TIPO_UNIDAD = 'TRACTO'

WHILE (@Contador <= (SELECT COUNT(*) FROM @ListaTractos)) BEGIN
	DECLARE @P VARCHAR(50) = (SELECT Placa FROM @ListaTractos WHERE Nro = @Contador)
	DECLARE @T VARCHAR(250) = (SELECT Tipo FROM @ListaTractos WHERE Nro = @Contador)
	DECLARE @S VARCHAR(250) = (SELECT SubTipo FROM @ListaTractos WHERE Nro = @Contador)
	DECLARE @O VARCHAR(50) = (SELECT Operacion FROM @ListaTractos WHERE Nro = @Contador)
	DECLARE @MA VARCHAR(250) = (SELECT Marca FROM @ListaTractos WHERE Nro = @Contador)
	DECLARE @MO VARCHAR(250) = (SELECT Modelo FROM @ListaTractos WHERE Nro = @Contador)
	DECLARE @FP DATE = (SELECT FechaProyectada FROM @ListaTractos WHERE Nro = @Contador)

	EXEC ReportesApp_Mantenimiento_MttoPreventivo_CrearInspeccion
	@Placa = @P, @Tipo = @T, @SubTipo = @S, @Operacion = @O, @Marca = @MA, @Modelo = @MO, @FechaProyectada = @FP, @Usuario = 'MARANDAR'

	SET @Contador = @Contador + 1
END
*/


