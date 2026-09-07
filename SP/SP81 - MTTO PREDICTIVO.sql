
-- CREAR TABLA ReportesApp_Mantenimiento_MttoPredictivo_Tecnica

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPredictivo_Sistema

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPredictivo_Registro

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPredictivo_Historial

------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 07-07-2025
-- Description:	LISTAR TECNICAS Y SISTEMAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPredictivo_ListarTecnicaSistema]
@Opcion INT,
@idTecnica INT
AS
BEGIN
	IF (@Opcion = 1) BEGIN		-- LISTAR TECNICAS
		SELECT idTecnica, Descripcion FROM ReportesApp_Mantenimiento_MttoPredictivo_Tecnica
	END

	IF (@Opcion = 2) BEGIN		-- LISTAR SISTEMAS
		SELECT idSistema, Descripcion FROM ReportesApp_Mantenimiento_MttoPredictivo_Sistema
		WHERE idTecnica = @idTecnica
	END
END

----------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 07-07-2025
-- Description:	REGISTRAR MANTENIMIENTO PREDICTIVO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPredictivo_RegistrarModificar]
@Placa VARCHAR(20),
@idTecnica INT,
@idSistema INT,
@Fecha DATETIME,
@TipoAceite VARCHAR(250),
@Recomendacion VARCHAR(500),
@Estado VARCHAR(30),
@DirectorioPDF VARCHAR(MAX),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

BEGIN TRAN
BEGIN TRY
	IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPredictivo_Registro WHERE Placa = @Placa AND idTecnica = @idTecnica AND idSistema = @idSistema)) BEGIN
		IF ((SELECT Fecha FROM ReportesApp_Mantenimiento_MttoPredictivo_Registro WHERE Placa = @Placa AND idTecnica = @idTecnica AND
		idSistema = @idSistema) = CONVERT(DATE,@Fecha)) BEGIN
			UPDATE ReportesApp_Mantenimiento_MttoPredictivo_Registro
			SET TipoAceite = @TipoAceite, Recomendacion = @Recomendacion, Estado = @Estado, DirectorioPDF = @DirectorioPDF
			WHERE Placa = @Placa AND idTecnica = @idTecnica AND idSistema = @idSistema
		END
		ELSE BEGIN
			INSERT INTO ReportesApp_Mantenimiento_MttoPredictivo_Historial(idMttoPD, Placa, idTecnica, idSistema, Fecha, TipoAceite, Recomendacion, Estado,
			DirectorioPDF, UltimoUsuario, UltimaFecha)
			SELECT idMttoPD, Placa, idTecnica, idSistema, Fecha, TipoAceite, Recomendacion, Estado, DirectorioPDF, @Usuario, GETDATE()
			FROM ReportesApp_Mantenimiento_MttoPredictivo_Registro
			WHERE Placa = @Placa AND idTecnica = @idTecnica AND idSistema = @idSistema

			UPDATE ReportesApp_Mantenimiento_MttoPredictivo_Registro
			SET Fecha = @Fecha, TipoAceite = @TipoAceite, Recomendacion = @Recomendacion, Estado = @Estado, DirectorioPDF = @DirectorioPDF,
			UltimoUsuario = @Usuario, UltimaFecha = GETDATE()
			WHERE Placa = @Placa AND idTecnica = @idTecnica AND idSistema = @idSistema
		END

		SET @Exito = '0 = Mantenimiento Actualizado Correctamente.'
	END
	ELSE BEGIN
		SET @correlativo = (SELECT MAX(idMttoPD) FROM ReportesApp_Mantenimiento_MttoPredictivo_Registro)
		SET @correlativo = ISNULL(@correlativo,0) + 1

		INSERT INTO ReportesApp_Mantenimiento_MttoPredictivo_Registro(idMttoPD,Placa,idTecnica,idSistema,Fecha,TipoAceite,Recomendacion,Estado,DirectorioPDF,UltimoUsuario,UltimaFecha)
		VALUES(@correlativo, @Placa, @idTecnica, @idSistema, @Fecha, @TipoAceite, @Recomendacion, @Estado, @DirectorioPDF, @Usuario, GETDATE())

		SET @Exito = '0 = Mantenimiento Registrado Correctamente.'
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
-- Create date: 07-07-2025
-- Description:	LISTAR MANTENIMIENTO PREDICTIVO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPredictivo_ListarRegistro]
@Placa VARCHAR(30),
@TipoUnidad INT,
@idTecnica INT,
@idSistema INT
AS
BEGIN
	IF (@TipoUnidad = 0) BEGIN
		SELECT MP.idMttoPD AS 'NRO', MP.Placa AS 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD', SV.Descripcion AS 'SUBTIPO_UNIDAD', 
		CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA',
		V.Modelo AS 'MODELO', MP.idTecnica, T.Descripcion AS 'TECNICA', MP.idSistema, S.Descripcion AS 'SISTEMA', MP.Fecha AS 'FECHA', MP.Estado AS 'ESTADO',
		MP.TipoAceite AS 'TIPO_LUBRICANTE', MP.Recomendacion AS 'RECOMENDACION', MP.DirectorioPDF AS 'INFORME', MP.UltimoUsuario, MP.UltimaFecha
		FROM ReportesApp_Mantenimiento_MttoPredictivo_Registro MP
		LEFT JOIN OP_TR_Vehiculo V ON V.NumeroPlaca = MP.Placa
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON UC.IdProgramacion = O.IdOperacion
		LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TipoVehiculo
		LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
		LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
		LEFT JOIN ReportesApp_Mantenimiento_MttoPredictivo_Tecnica T ON T.idTecnica = MP.idTecnica
		LEFT JOIN ReportesApp_Mantenimiento_MttoPredictivo_Sistema S ON S.idTecnica = MP.idTecnica AND S.idSistema = MP.idSistema
		WHERE (MP.Placa IS NULL OR MP.Placa LIKE '%' + @Placa + '%') AND (MP.idTecnica = @idTecnica) AND (MP.idSistema = @idSistema)
		ORDER BY MP.UltimaFecha DESC
	END
	ELSE BEGIN
		SELECT MP.idMttoPD AS 'NRO', MP.Placa AS 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD', SV.Descripcion AS 'SUBTIPO_UNIDAD', 
		CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA',
		V.Modelo AS 'MODELO', MP.idTecnica, T.Descripcion AS 'TECNICA', MP.idSistema, S.Descripcion AS 'SISTEMA', MP.Fecha AS 'FECHA', MP.Estado AS 'ESTADO',
		MP.TipoAceite AS 'TIPO_LUBRICANTE', MP.Recomendacion AS 'RECOMENDACION', MP.DirectorioPDF AS 'INFORME', MP.UltimoUsuario, MP.UltimaFecha
		FROM ReportesApp_Mantenimiento_MttoPredictivo_Registro MP
		LEFT JOIN OP_TR_Vehiculo V ON V.NumeroPlaca = MP.Placa
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON UC.IdProgramacion = O.IdOperacion
		LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TipoVehiculo
		LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
		LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
		LEFT JOIN ReportesApp_Mantenimiento_MttoPredictivo_Tecnica T ON T.idTecnica = MP.idTecnica
		LEFT JOIN ReportesApp_Mantenimiento_MttoPredictivo_Sistema S ON S.idTecnica = MP.idTecnica AND S.idSistema = MP.idSistema
		WHERE (MP.Placa IS NULL OR MP.Placa LIKE '%' + @Placa + '%') AND (MP.idTecnica = @idTecnica) AND (MP.idSistema = @idSistema)
		AND (TV.idTipoVehiculo = @TipoUnidad)
		ORDER BY MP.UltimaFecha DESC
	END
END

----------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 10-07-2025
-- Description:	LISTAR MANTENIMIENTO PREDICTIVO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPredictivo_ListarHistorial]
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@Placa VARCHAR(30),
@TipoUnidad INT,
@idTecnica INT,
@idSistema INT
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@TipoUnidad = 0) BEGIN
		SELECT MP.Placa AS 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD', SV.Descripcion AS 'SUBTIPO_UNIDAD', 
		CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA',
		V.Modelo AS 'MODELO', T.Descripcion AS 'TECNICA', S.Descripcion AS 'SISTEMA', MP.Fecha AS 'FECHA', MP.Estado AS 'ESTADO',
		MP.TipoAceite AS 'TIPO_LUBRICANTE', MP.Recomendacion AS 'RECOMENDACION', MP.DirectorioPDF AS 'INFORME', MP.UltimoUsuario, MP.UltimaFecha
		FROM ReportesApp_Mantenimiento_MttoPredictivo_Historial MP
		LEFT JOIN OP_TR_Vehiculo V ON V.NumeroPlaca = MP.Placa
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON UC.IdProgramacion = O.IdOperacion
		LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TipoVehiculo
		LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
		LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
		LEFT JOIN ReportesApp_Mantenimiento_MttoPredictivo_Tecnica T ON T.idTecnica = MP.idTecnica
		LEFT JOIN ReportesApp_Mantenimiento_MttoPredictivo_Sistema S ON S.idTecnica = MP.idTecnica AND S.idSistema = MP.idSistema
		WHERE (MP.Placa IS NULL OR MP.Placa LIKE '%' + @Placa + '%') AND (MP.Fecha BETWEEN @FINICIO AND @FFIN)
		AND (MP.idTecnica = @idTecnica) AND (MP.idSistema = @idSistema)
		ORDER BY MP.UltimaFecha DESC
	END
	ELSE BEGIN
		SELECT MP.Placa AS 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD', SV.Descripcion AS 'SUBTIPO_UNIDAD', 
		CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION', LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA',
		V.Modelo AS 'MODELO', T.Descripcion AS 'TECNICA', S.Descripcion AS 'SISTEMA', MP.Fecha AS 'FECHA', MP.Estado AS 'ESTADO',
		MP.TipoAceite AS 'TIPO_LUBRICANTE', MP.Recomendacion AS 'RECOMENDACION', MP.DirectorioPDF AS 'INFORME', MP.UltimoUsuario, MP.UltimaFecha
		FROM ReportesApp_Mantenimiento_MttoPredictivo_Historial MP
		LEFT JOIN OP_TR_Vehiculo V ON V.NumeroPlaca = MP.Placa
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON UC.IdProgramacion = O.IdOperacion
		LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TipoVehiculo
		LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
		LEFT JOIN ME_MaquinaMarca ME ON V.Marca = ME.Marca
		LEFT JOIN ReportesApp_Mantenimiento_MttoPredictivo_Tecnica T ON T.idTecnica = MP.idTecnica
		LEFT JOIN ReportesApp_Mantenimiento_MttoPredictivo_Sistema S ON S.idTecnica = MP.idTecnica AND S.idSistema = MP.idSistema
		WHERE (MP.Placa IS NULL OR MP.Placa LIKE '%' + @Placa + '%') AND (MP.Fecha BETWEEN @FINICIO AND @FFIN) AND (MP.idTecnica = @idTecnica)
		AND (MP.idSistema = @idSistema) AND (TV.idTipoVehiculo = @TipoUnidad) 
		ORDER BY MP.UltimaFecha DESC
	END
END





