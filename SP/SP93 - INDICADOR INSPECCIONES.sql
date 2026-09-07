SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 29-11-2024
-- Description:	LISTAR INDICADOR INSPECCION
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarIndicadorInspeccion]
@Opcion INT,
@Fecha DATETIME,
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@Placa VARCHAR(20),
@Operacion VARCHAR(50)
AS
BEGIN
    DECLARE @FechaDia DATE = CONVERT(DATE, @Fecha)
    DECLARE @FechaDiaFin DATE = DATEADD(DAY, 1, @FechaDia)
    DECLARE @FechaInicioDia DATE = CONVERT(DATE, @FechaInicio);
    DECLARE @FechaFinDia DATE = CONVERT(DATE, @FechaFin);

    IF (@Opcion = 1) BEGIN		-- LISTAR DATOS DE INSPECCION 
        SELECT CONVERT(VARCHAR(10), @FechaDia, 103) AS 'FECHA', V.NumeroPlaca AS 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD',
		CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA',
		V.Modelo AS 'MODELO', CASE WHEN EXISTS (SELECT 1 FROM ReportesApp_Operacion_Previaje_Registros R WHERE R.idTracto = V.idVehiculo
        AND R.TipoProgramacion IN (1,2,3,4,10) AND R.Estado IN (1,9) AND R.NroTicket IS NOT NULL AND CONVERT(DATE,R.FechaInicio) = @FechaDia)
        THEN 'VIAJE' END AS 'PROGRAMACION', CASE WHEN EXISTS (SELECT 1 FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera I
        WHERE I.Placa = V.NumeroPlaca AND I.idInspeccionC IS NOT NULL AND CONVERT(DATE,I.FechaInicio) = @FechaDia) THEN 'INSPECCION' END AS 'INSPECCION'
        FROM ReportesApp_Operacion_MaestroUnidadesConductor UC
        LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
        LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo TV WITH (NOLOCK) ON TV.idTipoVehiculo = V.TIPOVEHICULO
        LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
        LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
        LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = UC.IdProgramacion
        WHERE TV.idTipoVehiculo = 1 AND SV.SubTipoVehiculo = 1 AND V.Estado = 2 AND (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%')
        AND (@Operacion = 'TODOS' OR CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END = @Operacion)
    END

    IF (@Opcion = 2) BEGIN		-- LISTAR RESUMEN DE INSPECCION
        IF OBJECT_ID('tempdb..#T_INDICADOR') IS NOT NULL
		DROP TABLE #T_INDICADOR

        ;WITH Fechas AS (SELECT @FechaInicioDia AS FECHA
		UNION ALL
		SELECT DATEADD(DAY, 1, FECHA) FROM Fechas WHERE FECHA < @FechaFinDia),
        
		BaseUnidades AS (SELECT UC.IdUnidad, V.IdVehiculo, V.NumeroPlaca, CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS OPERACION
        FROM ReportesApp_Operacion_MaestroUnidadesConductor UC
        LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
        LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo TV WITH (NOLOCK) ON TV.idTipoVehiculo = V.TIPOVEHICULO
        LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
        LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
        LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = UC.IdProgramacion
        WHERE TV.idTipoVehiculo = 1 AND SV.SubTipoVehiculo = 1 AND V.Estado = 2)

        SELECT F.FECHA, B.NumeroPlaca AS 'PLACA', B.OPERACION, CASE WHEN EXISTS (SELECT 1 FROM ReportesApp_Operacion_Previaje_Registros R
        WHERE R.idTracto = B.IdVehiculo AND R.TipoProgramacion IN (1,2,3,4,10) AND R.Estado IN (1,9) AND R.NroTicket IS NOT NULL
        AND CONVERT(DATE,R.FechaInicio) = F.FECHA) THEN 'VIAJE' END AS 'PROGRAMACION',
		CASE WHEN EXISTS (SELECT 1 FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera I WHERE I.Placa = B.NumeroPlaca
        AND I.idInspeccionC IS NOT NULL AND CONVERT(DATE,I.FechaInicio) = F.FECHA) THEN 'INSPECCION' END AS 'INSPECCION'
        INTO #T_INDICADOR
		FROM Fechas F
        CROSS JOIN BaseUnidades B
        WHERE B.OPERACION = @Operacion
        OPTION (MAXRECURSION 0)

        SELECT CONVERT(VARCHAR(10), FECHA, 103) AS 'FECHA', OPERACION, SUM(CASE WHEN PROGRAMACION = 'VIAJE' THEN 1 ELSE 0 END) AS PROG,
		SUM(CASE WHEN INSPECCION = 'INSPECCION' THEN 1 ELSE 0 END) AS INSPE, CONVERT(DECIMAL(10,2),
		ROUND(CASE WHEN SUM(CASE WHEN PROGRAMACION = 'VIAJE' THEN 1 ELSE 0 END) = 0 THEN 0 ELSE 
        SUM(CASE WHEN INSPECCION = 'INSPECCION' THEN 1 ELSE 0 END) * 100.0 / SUM(CASE WHEN PROGRAMACION = 'VIAJE' THEN 1 ELSE 0 END) END, 0)) AS 'PORCENTAJE (%)'
        FROM #T_INDICADOR
        GROUP BY FECHA, OPERACION
        ORDER BY FECHA;
    END
END

---------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 02-12-2024
-- Description:	LISTAR UNIDADES DE INDICADOR
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarIndicadorUnidades]
@Opcion INT,
@Fecha DATETIME,
@Operacion VARCHAR(50)
AS
BEGIN
	IF (@Opcion = 1) BEGIN		-- LISTAR PROGRAMACIONES
		SELECT X.PLACA, X.TIPO_UNIDAD, X.MARCA, X.MODELO FROM
		(SELECT V.NumeroPlaca 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION',
		LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', V.Modelo AS 'MODELO', (SELECT TOP(1) CASE WHEN NroTicket IS NOT NULL THEN 'VIAJE' ELSE NULL END
		FROM ReportesApp_Operacion_Previaje_Registros
		WHERE (idTracto = V.idVehiculo) AND (CONVERT(DATE,@Fecha) = CONVERT(DATE,FechaInicio)) AND (TipoProgramacion IN (1,2,3,4,10)) AND (Estado IN (1,9))) AS 'PROGRAMACION'
		FROM ReportesApp_Operacion_MaestroUnidadesConductor UC
		LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
		LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TIPOVEHICULO
		LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
		LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = UC.IdProgramacion
		WHERE (TV.idTipoVehiculo = 1) AND (SV.SubTipoVehiculo = 1) AND (V.Estado = 2)) X
		WHERE NOT (PROGRAMACION IS NULL) AND (X.OPERACION = @Operacion)
	END

	IF (@Opcion = 2) BEGIN		-- LISTAR INSPECCIONES
		SELECT X.PLACA, X.TIPO_UNIDAD, X.MARCA, X.MODELO FROM
		(SELECT V.NumeroPlaca 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION',
		LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', V.Modelo AS 'MODELO', (SELECT TOP(1) CASE WHEN idInspeccionC IS NOT NULL THEN 'INSPECCION' 
		ELSE NULL END FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera WHERE (Placa = V.NumeroPlaca) AND
		(CONVERT(DATE,@Fecha) = CONVERT(DATE,FechaInicio))) AS 'INSPECCION'
		FROM ReportesApp_Operacion_MaestroUnidadesConductor UC
		LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
		LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TIPOVEHICULO
		LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
		LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = UC.IdProgramacion
		WHERE (TV.idTipoVehiculo = 1) AND (SV.SubTipoVehiculo = 1) AND (V.Estado = 2)) X
		WHERE NOT (INSPECCION IS NULL) AND (X.OPERACION = @Operacion)
	END
END


