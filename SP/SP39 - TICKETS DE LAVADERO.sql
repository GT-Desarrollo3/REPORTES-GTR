
-- CREAR TABLA ReportesApp_Mantenimiento_TicketsLavadero_Registro

-----------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 26-04-2024
-- Description:	BUSCAR MÁQUINAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_TicketsLavadero_BuscarMaquinas]
@Placa VARCHAR(20)
AS
BEGIN
	SELECT TOP(10) LTRIM(RTRIM(M.MaquinaCodigo)) AS 'PLACA',
	CASE WHEN ISNULL(LTRIM(RTRIM(T.DescripcionLocal)),'') = 'TRACTO CAMION' THEN 'TRACTO'
	WHEN ISNULL(LTRIM(RTRIM(T.DescripcionLocal)),'') IN ('CORTINERA','PLATAFORMA','TOLVA','CISTERNA') THEN 'SEMIRREMOLQUE'
	ELSE ISNULL(LTRIM(RTRIM(T.DescripcionLocal)),'') END AS 'MAQUINA', ISNULL(O.Descripcion,'SIN OPERACION') AS 'OPERACION'
	FROM ME_Maquina M
	LEFT JOIN OP_TR_Vehiculo V ON LTRIM(RTRIM(V.Proyecto)) = LTRIM(RTRIM(ISNULL(M.Proyecto,afe))) AND V.Estado = 2
	LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
	LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo TV ON TV.idTipoVehiculo = V.TipoVehiculo
	LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON  O.IdOperacion = UC.IdProgramacion 
	LEFT JOIN ME_MaquinaMarca ME ON M.Marca = ME.Marca AND ME.Estado = 'A'
	LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina)) AND (T.Estado = 'A')
	LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_KMPlacas KM ON KM.idVehiculo = V.IdVehiculo
	WHERE (M.Estado = 'A') AND (@Placa IS NULL OR M.MaquinaCodigo LIKE '%' + @Placa + '%')
END

-----------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 26-04-2024
-- Description:	GENERAR TICKET LAVADERO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_TicketsLavadero_GenerarTicket]
@MaquinaCodigo VARCHAR(50),
@FechaProg DATETIME,
@Operacion VARCHAR(30),
@TipoLavado VARCHAR(50),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

SET @Exito = '0 = Ticket Generado.'

BEGIN TRAN
BEGIN TRY
	SET @correlativo = (SELECT MAX(idTicketLavadero) FROM ReportesApp_Mantenimiento_TicketsLavadero_Registro WHERE Anio = YEAR(GETDATE()))
	SET @correlativo = ISNULL(@correlativo,0) + 1

	INSERT INTO ReportesApp_Mantenimiento_TicketsLavadero_Registro(idTicketLavadero, Anio, CodLavado, MaquinaCodigo, Operacion, FechaProg, Estado,
	UsuarioCrea, FechaCrea, UsuarioModifica, FechaModifica, Validacion, TipoLavado)
	VALUES(@correlativo, YEAR(GETDATE()), SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo)),6)),
	@MaquinaCodigo, @Operacion, @FechaProg, 'PENDIENTE', @Usuario, GETDATE(), @Usuario, GETDATE(), 0, @TipoLavado)

	SET @Exito = SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo)),6))
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

----------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 26-04-2024
-- Description:	LISTAR TICKET LAVADERO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_TicketsLavadero_ListarTicket]
@CodLavado VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT R.CodLavado, R.MaquinaCodigo AS 'PLACA', ISNULL(LTRIM(RTRIM(T.DescripcionLocal)),'') AS 'MAQUINA',
	ISNULL(O.Descripcion,R.Operacion) AS 'OPERACION', CONVERT(VARCHAR,R.FechaProg,103) AS 'FECHA', R.TipoLavado AS 'LAVADO'
	FROM ReportesApp_Mantenimiento_TicketsLavadero_Registro R
	LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(R.MaquinaCodigo)) = LTRIM(RTRIM(M.MaquinaCodigo))
	LEFT JOIN OP_TR_Vehiculo V ON LTRIM(RTRIM(V.Proyecto)) = LTRIM(RTRIM(ISNULL(M.Proyecto,afe))) AND V.Estado = 2
	LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
	LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON  O.IdOperacion = UC.IdProgramacion 
	LEFT JOIN ME_MaquinaMarca ME ON M.Marca = ME.Marca AND ME.Estado = 'A'
	LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina)) AND (T.Estado = 'A')
	WHERE (R.CodLavado = @CodLavado)
END

-----------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 26-04-2024
-- Description:	LISTAR TICKET LAVADERO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_TicketsLavadero_Listar]
@CodLavado VARCHAR(50),
@Placa VARCHAR(50),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@Estado VARCHAR(20),
@Operacion VARCHAR(50)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@Estado = 'TODOS') BEGIN
		IF (@Operacion = 'TODO') BEGIN
			SELECT R.CodLavado AS 'CODIGO', ISNULL(O.Descripcion,R.Operacion) AS 'OPERACION', R.MaquinaCodigo AS 'PLACA',
			R.TipoLavado AS 'LAVADO', R.FechaProg AS 'FECHA_EJECUCION', R.Estado AS 'ESTADO', R.UsuarioCrea, R.FechaCrea,
			R.UsuarioModifica, R.FechaModifica, CAST(R.Validacion AS BIT) AS 'VALIDACION'
			FROM ReportesApp_Mantenimiento_TicketsLavadero_Registro R
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(R.MaquinaCodigo)) = LTRIM(RTRIM(M.MaquinaCodigo))
			LEFT JOIN OP_TR_Vehiculo V ON LTRIM(RTRIM(V.Proyecto)) = LTRIM(RTRIM(ISNULL(M.Proyecto,afe))) AND V.Estado = 2
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON  O.IdOperacion = UC.IdProgramacion 
			LEFT JOIN ME_MaquinaMarca ME ON M.Marca = ME.Marca AND ME.Estado = 'A'
			LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina)) AND (T.Estado = 'A')
			WHERE (@CodLavado IS NULL OR R.CodLavado LIKE '%' + @CodLavado + '%') AND (@Placa IS NULL OR R.MaquinaCodigo LIKE '%' + @Placa + '%')
			AND (R.FechaProg BETWEEN @FINICIO AND @FFIN)
		END
		ELSE BEGIN
			SELECT R.CodLavado AS 'CODIGO', ISNULL(O.Descripcion,R.Operacion) AS 'OPERACION', R.MaquinaCodigo AS 'PLACA',
			R.TipoLavado AS 'LAVADO', R.FechaProg AS 'FECHA_EJECUCION', R.Estado AS 'ESTADO', R.UsuarioCrea, R.FechaCrea,
			R.UsuarioModifica, R.FechaModifica, CAST(R.Validacion AS BIT) AS 'VALIDACION'
			FROM ReportesApp_Mantenimiento_TicketsLavadero_Registro R
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(R.MaquinaCodigo)) = LTRIM(RTRIM(M.MaquinaCodigo))
			LEFT JOIN OP_TR_Vehiculo V ON LTRIM(RTRIM(V.Proyecto)) = LTRIM(RTRIM(ISNULL(M.Proyecto,afe))) AND V.Estado = 2
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON  O.IdOperacion = UC.IdProgramacion 
			LEFT JOIN ME_MaquinaMarca ME ON M.Marca = ME.Marca AND ME.Estado = 'A'
			LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina)) AND (T.Estado = 'A')
			WHERE (@CodLavado IS NULL OR R.CodLavado LIKE '%' + @CodLavado + '%') AND (@Placa IS NULL OR R.MaquinaCodigo LIKE '%' + @Placa + '%')
			AND (R.FechaProg BETWEEN @FINICIO AND @FFIN) AND (@Operacion = ISNULL(O.Descripcion,R.Operacion))
		END
	END
	ELSE BEGIN
		IF (@Operacion = 'TODO') BEGIN
			SELECT R.CodLavado AS 'CODIGO', ISNULL(O.Descripcion,R.Operacion) AS 'OPERACION', R.MaquinaCodigo AS 'PLACA',
			R.TipoLavado AS 'LAVADO', R.FechaProg AS 'FECHA_EJECUCION', R.Estado AS 'ESTADO', R.UsuarioCrea, R.FechaCrea,
			R.UsuarioModifica, R.FechaModifica, CAST(R.Validacion AS BIT) AS 'VALIDACION'
			FROM ReportesApp_Mantenimiento_TicketsLavadero_Registro R
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(R.MaquinaCodigo)) = LTRIM(RTRIM(M.MaquinaCodigo))
			LEFT JOIN OP_TR_Vehiculo V ON LTRIM(RTRIM(V.Proyecto)) = LTRIM(RTRIM(ISNULL(M.Proyecto,afe))) AND V.Estado = 2
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON  O.IdOperacion = UC.IdProgramacion 
			LEFT JOIN ME_MaquinaMarca ME ON M.Marca = ME.Marca AND ME.Estado = 'A'
			LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina)) AND (T.Estado = 'A')
			WHERE (@CodLavado IS NULL OR R.CodLavado LIKE '%' + @CodLavado + '%') AND (@Placa IS NULL OR R.MaquinaCodigo LIKE '%' + @Placa + '%')
			AND (R.FechaProg BETWEEN @FINICIO AND @FFIN) AND (@Estado = R.Estado)
		END
		ELSE BEGIN
			SELECT R.CodLavado AS 'CODIGO', ISNULL(O.Descripcion,R.Operacion) AS 'OPERACION', R.MaquinaCodigo AS 'PLACA',
			R.TipoLavado AS 'LAVADO', R.FechaProg AS 'FECHA_EJECUCION', R.Estado AS 'ESTADO', R.UsuarioCrea, R.FechaCrea,
			R.UsuarioModifica, R.FechaModifica, CAST(R.Validacion AS BIT) AS 'VALIDACION'
			FROM ReportesApp_Mantenimiento_TicketsLavadero_Registro R
			LEFT JOIN ME_Maquina M ON LTRIM(RTRIM(R.MaquinaCodigo)) = LTRIM(RTRIM(M.MaquinaCodigo))
			LEFT JOIN OP_TR_Vehiculo V ON LTRIM(RTRIM(V.Proyecto)) = LTRIM(RTRIM(ISNULL(M.Proyecto,afe))) AND V.Estado = 2
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON  O.IdOperacion = UC.IdProgramacion 
			LEFT JOIN ME_MaquinaMarca ME ON M.Marca = ME.Marca AND ME.Estado = 'A'
			LEFT JOIN ME_MaquinaTipo T ON LTRIM(RTRIM(T.TipoMaquina)) = LTRIM(RTRIM(M.TipoMaquina)) AND (T.Estado = 'A')
			WHERE (@CodLavado IS NULL OR R.CodLavado LIKE '%' + @CodLavado + '%') AND (@Placa IS NULL OR R.MaquinaCodigo LIKE '%' + @Placa + '%')
			AND (R.FechaProg BETWEEN @FINICIO AND @FFIN) AND (@Operacion = ISNULL(O.Descripcion,R.Operacion)) AND (@Estado = R.Estado)
			AND (@Operacion = ISNULL(O.Descripcion,'SIN OPERACION'))
		END
	END
END

-----------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 27-04-2024
-- Description:	ELIMINAR TICKET LAVADERO
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Mantenimiento_TicketsLavadero_EliminarTicket]
@CodLavado VARCHAR(50)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Ticket eliminado.'

BEGIN TRAN
BEGIN TRY
	DELETE FROM ReportesApp_Mantenimiento_TicketsLavadero_Registro
	WHERE CodLavado = @CodLavado
END TRY

BEGIN CATCH
	SET @exito = RTRIM(LTRIM(STR(ERROR_NUMBER()))) + '=' + ERROR_MESSAGE()
	ROLLBACK
	GOTO Terminar
END CATCH

IF @@TRANCOUNT > 0
	COMMIT;

TERMINAR:
IF LEFT(@Exito,1)='-' OR cast(left(@Exito,1) AS INT)>0 BEGIN
	DECLARE @storedProcedureName VARCHAR(100)
	SELECT @storedProcedureName = OBJECT_NAME(@@PROCID)
	SET @exito = @exito + CHAR(13)+CHAR(13)+'SP Name: '+@storedProcedureName
END

SELECT @exito exito

-------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 27-04-2024
-- Description:	ACTUALIZAR TICKET LAVADERO
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Mantenimiento_TicketsLavadero_ActualizarTicket]
@CodLavado VARCHAR(50),
@FechaProg DATETIME,
@Estado VARCHAR(50),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Ticket actualizado correctamente.'

BEGIN TRAN
BEGIN TRY
	IF (CONVERT(DATE,@FechaProg) < CONVERT(DATE,GETDATE())) BEGIN
		SET @Exito = '-1 = No puede reprogramar un lavado en una fecha menor a la actual.'
		ROLLBACK
		GOTO Terminar
	END

	UPDATE ReportesApp_Mantenimiento_TicketsLavadero_Registro
	SET Estado = @Estado, FechaProg = @FechaProg, UsuarioModifica = @Usuario, FechaModifica = GETDATE()
	WHERE CodLavado = @CodLavado
END TRY

BEGIN CATCH
	SET @exito = RTRIM(LTRIM(STR(ERROR_NUMBER()))) + '=' + ERROR_MESSAGE()
	ROLLBACK
	GOTO Terminar
END CATCH

IF @@TRANCOUNT > 0
	COMMIT;

TERMINAR:
IF LEFT(@Exito,1)='-' OR cast(left(@Exito,1) AS INT)>0 BEGIN
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
-- Create date: 09-09-2024
-- Description:	MARCAR VALIDACION TICKET
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_TicketsLavadero_MarcarValidacion]
@CodLavado VARCHAR(50),
@Validacion BIT,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Ticket actualizado correctamente.'

BEGIN TRAN
BEGIN TRY
	UPDATE ReportesApp_Mantenimiento_TicketsLavadero_Registro
	SET Validacion = @Validacion
	WHERE CodLavado = @CodLavado
END TRY

BEGIN CATCH
	SET @exito = RTRIM(LTRIM(STR(ERROR_NUMBER()))) + '=' + ERROR_MESSAGE()
	ROLLBACK
	GOTO Terminar
END CATCH

IF @@TRANCOUNT > 0
	COMMIT;

TERMINAR:
IF LEFT(@Exito,1)='-' OR cast(left(@Exito,1) AS INT)>0 BEGIN
	DECLARE @storedProcedureName VARCHAR(100)
	SELECT @storedProcedureName = OBJECT_NAME(@@PROCID)
	SET @exito = @exito + CHAR(13)+CHAR(13)+'SP Name: '+@storedProcedureName
END

SELECT @exito exito