
-- MODIFICAR ReportesApp_Mantenimiento_Solicitud_RegistroDetalle

-----------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 20-09-2024
-- Description:	LISTAR TIPOS DE MTTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_Solicitudes_ListarTiposMtto]
@Opcion INT
AS
BEGIN
	IF (@Opcion = 1) BEGIN  -- LISTAR TIPO MTTO
		SELECT TipoMantenimiento, DescripcionLocal FROM ME_TipoMantenimiento
	END

	IF (@Opcion = 2) BEGIN  -- LISTAR CLASIFICACION MTTO
		SELECT TipoMantenimientoGrupo, DescripcionLocal FROM ME_TipoMantenimientoGrupo
	END
END

------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 06-09-2023
-- Description:	LISTAR SOLICITUDES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_SolicitudDetalle_Listar]
@Opcion INT,
@idSolicitud INT,
@idSolicitudDetalle INT
AS
BEGIN
	IF (@Opcion = 1) BEGIN		-- LISTAR DETALLE SIN IDSOLICITUD
		SELECT SD.idSolicitudDetalle, SD.idSolicitud, C.Descripcion AS 'COMPONENTE', CD.Descripcion AS 'DETALLE', P.Descripcion AS 'POSICION_LLANTA',
		SD.Observacion AS 'OBSERVACION', SD.idOT AS 'OT', SD.Estado AS 'ESTADO'
		FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle SD
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Componente C ON C.idComponente = SD.idComponente
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Componente_Detalle CD ON CD.idComponenteDetalle = SD.idComponenteDetalle
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Componente_PosicionLlanta P ON P.idPosicionLlanta = SD.idPosicionLlanta
		WHERE SD.idSolicitud IS NULL
	END

	IF (@Opcion = 2) BEGIN		-- LISTAR DETALLE CON IDSOLICITUD
		SELECT SD.idSolicitudDetalle, SD.idSolicitud, C.Descripcion AS 'COMPONENTE', CD.Descripcion AS 'DETALLE', P.Descripcion AS 'POSICION_LLANTA',
		SD.Observacion AS 'OBSERVACION', SD.idOT AS 'OT', SD.Estado AS 'ESTADO'
		FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle SD
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Componente C ON C.idComponente = SD.idComponente
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Componente_Detalle CD ON CD.idComponenteDetalle = SD.idComponenteDetalle
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Componente_PosicionLlanta P ON P.idPosicionLlanta = SD.idPosicionLlanta
		WHERE SD.idSolicitud = @idSolicitud
	END

	IF (@Opcion = 3) BEGIN		-- LISTAR DETALLE CON IDSOLICITUD
		SELECT SD.idSolicitudDetalle, SD.idSolicitud, C.Descripcion AS 'COMPONENTE', CD.Descripcion AS 'DETALLE', P.Descripcion AS 'POSICION_LLANTA',
		SD.Observacion AS 'OBSERVACION', SD.CodTarea AS 'COD_TAREA', LTRIM(RTRIM(T.DescripcionLocal)) AS 'TAREA', SD.FechaInicio AS 'INICIO', SD.FechaFin AS 'FIN'
		FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle SD
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Componente C ON C.idComponente = SD.idComponente
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Componente_Detalle CD ON CD.idComponenteDetalle = SD.idComponenteDetalle
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Componente_PosicionLlanta P ON P.idPosicionLlanta = SD.idPosicionLlanta
		LEFT JOIN ME_Actividad T ON LTRIM(RTRIM(T.Actividad)) = SD.CodTarea
		WHERE SD.idSolicitud = @idSolicitud AND SD.idOT IS NULL
	END
END

--------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 20-09-2024
-- Description:	LISTAR ACTIVIDADES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_SolicitudDetalle_ListarActividades]
@Placa VARCHAR(20),
@Actividad VARCHAR(250)
AS
BEGIN 
	DECLARE @Marca VARCHAR(20) = (SELECT LTRIM(RTRIM(Marca)) FROM OP_TR_Vehiculo WHERE NumeroPlaca = @Placa)
	DECLARE @Modelo VARCHAR(250) = (SELECT LTRIM(RTRIM(Modelo)) FROM OP_TR_Vehiculo WHERE NumeroPlaca = @Placa)
	
	SELECT LTRIM(RTRIM(ME_Actividad.Actividad)) AS 'CODIGO', LTRIM(RTRIM(ME_Actividad.DescripcionLocal)) AS 'ACTIVIDAD'
	FROM ME_Actividad, ME_TipoMantenimientoActividad, ME_TipoMantenimiento, ME_TipoMantenimientoGrupo
	WHERE (ME_Actividad.Actividad = ME_TipoMantenimientoActividad.Actividad) and
	(ME_TipoMantenimientoActividad.TipoMantenimiento = ME_TipoMantenimiento.TipoMantenimiento) and
	(ME_TipoMantenimientoGrupo.TipoMantenimientoGrupo = ME_TipoMantenimiento.TipoMantenimientoGrupo) and 
	((ME_Actividad.Estado = 'A')) AND (ME_TipoMantenimiento.TipoMantenimiento = '01') AND (ME_Actividad.Marca = @Marca) AND (ME_Actividad.Modelo = @Modelo)
	AND (@Actividad IS NULL OR ME_Actividad.DescripcionLocal LIKE '%' + @Actividad + '%')
	ORDER BY ME_Actividad.DescripcionLocal ASC 
END

-----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 21-09-2024
-- Description:	AÑADIR TAREA A DETALLE
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_Solicitudes_InsertarTarea]
@Opcion INT,
@idSolicitudDetalle INT,
@idSolicitud INT,
@CodTarea VARCHAR(20),
@FechaInicio DATETIME,
@FechaFin DATETIME
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

SET @Exito = '0 = Tarea Registrada.'

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- INSERTAR TAREA
		UPDATE ReportesApp_Mantenimiento_Solicitud_RegistroDetalle
		SET CodTarea = @CodTarea, FechaInicio = @FechaInicio, FechaFin = @FechaFin
		WHERE idSolicitud = @idSolicitud AND idSolicitudDetalle = @idSolicitudDetalle
	END
	
	IF (@Opcion = 2) BEGIN		-- ELIMINAR TAREA
		UPDATE ReportesApp_Mantenimiento_Solicitud_RegistroDetalle
		SET CodTarea = NULL, FechaInicio = NULL, FechaFin = NULL
		WHERE idSolicitud = @idSolicitud AND idSolicitudDetalle = @idSolicitudDetalle
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
-- Create date: 21-09-2024
-- Description:	REGISTRAR ORDEN DE TRABAJO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_Solicitudes_RegistrarOrdenTrabajo]
@idSolicitud INT,
@Placa VARCHAR(20),
@TipoMtto VARCHAR(20),
@Clasificacion VARCHAR(20),
@Ubicacion VARCHAR(100),
@Descripcion VARCHAR(250),
@FechaInicio DATETIME,
@FechaFin DATETIME,
@Mecanico INT,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @TABLA_TAREAS TABLE(Nro INT, Descripcion VARCHAR(250), CodTarea VARCHAR(6), FechaInicio DATETIME, FechaFin DATETIME)

BEGIN TRAN
BEGIN TRY
	INSERT INTO @TABLA_TAREAS (Nro, Descripcion, CodTarea, FechaInicio, FechaFin)
	SELECT ROW_NUMBER() OVER(ORDER BY SD.idSolicitud ASC) AS 'NRO', SD.Observacion, SD.CodTarea, SD.FechaInicio, SD.FechaFin
	FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle SD
	WHERE (SD.idSolicitud = @idSolicitud) AND (SD.CodTarea IS NOT NULL)

	-- ACTUALIZAR CORRELATIVO DE ORDENES DE TRABAJO
	UPDATE CorrelativosMast
	SET CorrelativoNumero = (SELECT CorrelativoNumero + 1 FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='100000') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie = 'MEOT')),
		UltimaFechaModif = GETDATE()
	WHERE (CorrelativosMast.CompaniaCodigo = '100000') AND (CorrelativosMast.TipoComprobante = 'SY') AND (CorrelativosMast.Serie = 'MEOT')
	
	DECLARE @NroOT INT = (SELECT CorrelativosMast.CorrelativoNumero FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo = '100000') AND (CorrelativosMast.TipoComprobante = 'SY') AND (CorrelativosMast.Serie = 'MEOT'))
	
	-- INSERTAR ORDEN DE TRABAJO
	DECLARE @Proyecto VARCHAR(20) = (SELECT LTRIM(RTRIM(Proyecto)) FROM OP_TR_Vehiculo WHERE Estado = 2 AND LTRIM(RTRIM(NumeroPlaca)) = @Placa)
	DECLARE @CentroCostos VARCHAR(20) = (SELECT LTRIM(RTRIM(CentroCostos)) FROM ME_Maquina WHERE Estado = 'A' AND LTRIM(RTRIM(MaquinaCodigo)) = @Placa)
	DECLARE @CodUbicacion VARCHAR(20) = (CASE WHEN @Ubicacion = 'TRUJILLO' THEN '006001010' WHEN @Ubicacion = 'MOCHE' THEN '006001011' END)
	DECLARE @Activo VARCHAR(20) = (SELECT LTRIM(RTRIM(Activo)) FROM ME_Maquina WHERE Estado = 'A' AND LTRIM(RTRIM(MaquinaCodigo)) = @Placa)
	DECLARE @NroUsuario INT = (SELECT ISNULL(P.Persona,13660) FROM Usuario U LEFT JOIN PersonaMast P ON P.NombreCompleto = U.Nombre WHERE Usuario = @Usuario)

	INSERT INTO ME_OrdenTrabajo (CompaniaSocio, NumeroOrden, TipoProcedencia, TipoMantenimiento, Prioridad, Activo, Descripcion, CentroCostos, Ubicacion,
	PreparadoPor, FechaPreparacion, RequisicionNumero, MontoLocal, MontoDolares, Taller, Periodo, Estado, UltimoUsuario, UltimaFechaModif, PersonaAsignada,
	InternoExternoFlag, TipoMantenimientoGrupo, ModeloFlag, Proyecto, Sucursal, CampoReferencia, MonedaCodigo, MontoVentaTotal, MontoVentaIGV, MontoVentaAfecto,
	EstadoCotizacion, ValorizadoFlag, FacturadoFlag, UnidadNegocio, Origen, AlmacenCodigo, MaquinaCodigo, MaquinaHoraKilometraje, MaquinaHoraKilometrajeReal,
	SituacionMaquina)
	SELECT '10000000', CONVERT(CHAR(10),RIGHT('0000000000'+CONVERT(VARCHAR,@NroOT),10)), 'P09', @TipoMtto, '1', @Activo, @Descripcion, @CentroCostos, @CodUbicacion,
	@NroUsuario, GETDATE(), 'NO TIENE', 0.0000, 0.0000, 'TRUJ', CONVERT(CHAR(4),YEAR(GETDATE()))+CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(GETDATE()))),2)), 'PR',
	@Usuario, GETDATE(), @Mecanico, 'I', @Clasificacion, 'N', @Proyecto, 'BTRU', '90', 'EX', 0.0000, 0.0000, 0.0000, 'NO', 'N', 'N', 'TRAN', 'CO', 'A001', @Placa,
	0.0000, 0.0000, 'OP'

	-- INSERTAR DETALLE DE ORDEN DE TRABAJO
	DECLARE @Contador INT = 1

	WHILE(@Contador <= (SELECT COUNT(*) FROM @TABLA_TAREAS)) BEGIN
		DECLARE @Actividad VARCHAR(6) = (SELECT CodTarea FROM @TABLA_TAREAS WHERE Nro = @Contador)
		DECLARE @Tarea VARCHAR(6) = (SELECT T.Tarea FROM ME_Tarea T, ME_ActividadTarea AT WHERE (T.Tarea = AT.Tarea) AND (AT.Actividad = @Actividad))
		DECLARE @FechaInicio2 DATETIME = (SELECT FechaInicio FROM @TABLA_TAREAS WHERE Nro = @Contador)
		DECLARE @FechaFin2 DATETIME = (SELECT FechaFin FROM @TABLA_TAREAS WHERE Nro = @Contador)

		INSERT INTO ME_OrdenTrabajoTarea (CompaniaSocio, NumeroOrden, Secuencia, Tarea, Actividad, UltimoUsuario, UltimaFechaModif, Estado, Cantidad, FechaInicio, FechaFin)
		SELECT '10000000', CONVERT(CHAR(10),RIGHT('0000000000'+CONVERT(VARCHAR,@NroOT),10)), @Contador, @Tarea, @Actividad, @Usuario, GETDATE(), 'PG', 1.0000, @FechaInicio2, @FechaFin2

		SET @Contador = @Contador + 1
	END

	-- APROBAR ORDEN DE TRABAJO
	UPDATE ME_OrdenTrabajo
	SET Descripcion = @Descripcion, DescripcionServicio = NULL, Observaciones = NULL, CentroCostos = @CentroCostos, AprobadoPor = @NroUsuario,
	FechaAprobacion = GETDATE(), Estado = 'PG', RazonRechazo = NULL, ComentarioTermino = NULL, AprobadoClientePor = NULL, RevisionCreditoPor = NULL,
	FechaAprobacionCliente = NULL, FechaRevisionCredito = NULL, MonedaCodigo = 'EX', MontoVentaTotal = 0.0000, MontoVentaIGV = 0.0000, MontoVentaAfecto = 0.0000,
	FabricanteGarantia = NULL, Cliente = NULL, FormadePago = NULL, NumeroOrdenRelacionado = NULL, MaquinaMantenimientoCodigo = NULL,
	MaquinaHoraKilometrajeReal = 0.0000, FechaProgramada = @FechaInicio, FechaFinEstimada = @FechaFin 
	WHERE CompaniaSocio = '10000000' AND NumeroOrden = CONVERT(CHAR(10),RIGHT('0000000000'+CONVERT(VARCHAR,@NroOT),10))

	-- ASIGNAR OT A SOLICITUD DE MTTO
	UPDATE ReportesApp_Mantenimiento_Solicitud_RegistroDetalle
	SET idOT = CONVERT(CHAR(10),RIGHT('0000000000'+CONVERT(VARCHAR,@NroOT),10)), Estado = 'PENDIENTE'
	WHERE (idSolicitud = @idSolicitud) AND (CodTarea IS NOT NULL)

	SET @Exito = '0 = Orden de Trabajo '+CONVERT(CHAR(10),RIGHT('0000000000'+CONVERT(VARCHAR,@NroOT),10))+' creada satisfactoriamente.'
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



