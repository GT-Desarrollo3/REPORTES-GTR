
-- TABLA ReportesApp_Mantenimiento_RegistroIncidencias_Cabecera

-- TABLA ReportesApp_Mantenimiento_RegistroIncidencias_Detalle

-- MODIFICAR TABLA ReportesApp_Mantenimiento_RegistroIncidencias_Cabecera

-- CREAR TABLA ReportesApp_Seguridad_RegistroIncidencias_Accidentes

-- CREAR TABLA ReportesApp_Seguridad_RegistroIncidencias_Registros

-- CREAR TABLA ReportesApp_Seguridad_RegistroIncidencias_Detalle

-- CREAR TABLA ReportesApp_Seguridad_RegistroIncidencias_Estado

--------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 17-05-2023
-- Description:	LISTAR REGISTRAR FALTANTES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_ListarFallas]
@NumeroPlaca VARCHAR(20),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@idEstadoFalla INT
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	SET NOCOUNT ON;
	UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
	SET MontoComprobante = NULL
	WHERE MontoComprobante = 0.00
	
	UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
	SET Monto = NULL
	WHERE Monto = 0.00
	
	UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
	SET Galones = NULL
	WHERE Galones = 0.00
	
	UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
	SET PrecioUnitario = NULL
	WHERE PrecioUnitario = 0.00
	
	UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
	SET PrecioTotal = NULL
	WHERE PrecioTotal = 0.00
	
	UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
	SET idTipoRecibo = 4
	WHERE idTipoRecibo = 0
	
	IF(@idEstadoFalla = 0) BEGIN
	SET @idEstadoFalla = NULL
	END
	
	SELECT FM.idFalla AS 'ID', FM.CodFalla, FM.CodEstado, FM.CodEstado + '-' + CONVERT(VARCHAR,FM.idFalla) AS 'CODIGO', ISNULL(IC.idIncidenteC,0) AS 'idIncidenteC',
	EF.Descripcion AS 'ESTADO', FM.NroTicket AS 'NRO_PREVIAJE', FM.TipoFalla AS 'TIPO', OP.Descripcion AS 'PROGRAMACION', V.NumeroPlaca AS 'PLACA', R.NumeroPlaca AS 'SEMIRREMOLQUE',
	A.Descripcion AS 'TIPO_FALLA', IC.MontoTotal AS 'PAGO_INCIDENCIA', FM.Motivo AS 'MOTIVO', FM.Ubicacion AS 'UBICACIÓN', LTRIM(RTRIM(C.Nombre)) AS 'CONDUCTOR',
	CONVERT(VARCHAR,FM.FechaInicio,103) + ' ' + CONVERT(VARCHAR(8),FM.HoraInicio) AS 'FECHA_INICIO', FORMAT(RE.FechaProgramacion,'dd/MM/yyyy') AS 'FECHA_VIAJE',
	RT.Descripcion AS 'RUTA', CONVERT(VARCHAR, FM.FechaSalida,103) + ' ' + CONVERT(VARCHAR(8), FM.HoraSalida) AS 'FECHA_SALIDA',
	CONVERT(VARCHAR, FM.FechaLlegada,103) + ' ' + CONVERT(VARCHAR(8),FM.HoraLlegada) AS 'FECHA_LLEGADA', CONVERT(VARCHAR,FM.FechaTermino,103) + ' ' + CONVERT(VARCHAR(8),FM.HoraTermino)
	AS 'FECHA_TÉRMINO', FM.Duracion AS 'DURACIÓN (DÍAS)', CS.Descripcion AS 'CLASE_SERVICIO', FM.Tecnico AS 'CONDUCTOR_CAMIONETA', FM.idPlaca AS 'PLACA_CAMIONETA',
	FM.Monto AS 'MONTO', FM.Galones AS 'GALONES', FM.PrecioUnitario AS 'PRECIO_UNITARIO', FM.PrecioTotal AS 'PRECIO_TOTAL', FM.NombreTercero AS 'NOMBRE_TECNICO',
	FM.TelefonoTercero AS 'TELÉFONO/CELULAR', TR.Descripcion AS 'RECIBO', FM.Comprobante AS 'N° RECIBO', FM.MontoComprobante AS 'MONTO_COMPROBANTE',
	SV.Descripcion AS 'SISTEMA_VEHÍCULO', FM.Solucion AS 'SOLUCIÓN', LI.Descripcion AS 'ESTADO_LIQUIDACIÓN', FORMAT(FM.FechaLiquidacion,'dd/MM/yyyy') AS 'FECHA_LIQUIDACIÓN',
	FM.UsuarioCreacion, FORMAT(FM.FechaCreacion,'dd/MM/yyyy') AS FechaCreacion, FM.UltimoUsuario, FORMAT(FM.UltimaModificacion,'dd/MM/yyyy') AS UltimaModificacion
	FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros FM
	LEFT JOIN ReportesApp_Operacion_Previaje_Registros RE WITH(NOLOCK) ON FM.NroTicket = RE.NroTicket
	LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = RE.TipoProgramacion
    LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = RE.idTracto
	LEFT JOIN OP_TR_Vehiculo R WITH(NOLOCK) ON R.IdVehiculo = RE.idSemirremolque
	LEFT JOIN OP_TR_Conductor C WITH(NOLOCK) ON C.IdConductor = RE.IdConductor
	LEFT JOIN PersonaMast CL WITH(NOLOCK) ON CL.Persona = RE.IdCliente
	LEFT JOIN OP_TR_RUTA RT WITH(NOLOCK) ON RT.IDRUTA = RE.IdRuta
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_TipoAuxilio A WITH(NOLOCK) ON A.idTipoAuxilio = FM.idTipoAuxilio
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_EstadoFalla EF WITH(NOLOCK) ON EF.idEstadoFalla = FM.idEstadoFalla
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_ClaseServicio CS WITH(NOLOCK) ON CS.idClaseServicio = FM.idClaseServicio
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_TipoRecibo TR WITH(NOLOCK) ON TR.idTipoRecibo = FM.idTipoRecibo
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_Liquidacion LI WITH(NOLOCK) ON LI.idEstadoLiquidacion = FM.idEstadoLiquidacion
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_SistemaVehiculo SV WITH(NOLOCK) ON SV.idSistemaVehiculo = FM.idSistemaVehiculo
    LEFT JOIN ReportesApp_Mantenimiento_RegistroIncidencias_Cabecera IC WITH(NOLOCK) ON IC.idFalla = FM.idFalla
	WHERE ((@NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @NumeroPlaca + '%') AND (FM.FechaInicio BETWEEN @FINICIO AND @FFIN)
			AND (@idEstadoFalla IS NULL OR FM.idEstadoFalla = @idEstadoFalla)) ORDER BY FM.idFalla DESC
END

---------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 14-02-2024
-- Description:	LISTAR MAESTRO DE ITEMS
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Mantenimiento_RegistroIncidencias_ListarMaestroItems]
@Item VARCHAR(300)
AS
BEGIN
	SELECT TOP(25) Item, LTRIM(RTRIM(DescripcionLocal)) AS 'ITEM', UnidadCodigo AS 'UNIDAD', CONVERT(DECIMAL(10,2),PrecioUnitarioLocal) AS 'SOLES',	CONVERT(DECIMAL(10,2),PrecioUnitarioDolares) AS 'DOLARES'	FROM WH_ItemMast WHERE (Estado = 'A') AND (DescripcionLocal IS NULL OR DescripcionLocal LIKE @Item + '%')
END---------------------------------------------------------------------------SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 17-02-2024
-- Description:	REGISTRAR PRESUPUESTO INCIDENCIAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_RegistroIncidencias_RegistrarPresupuesto]
@Opcion INT,
@idIncidenteC INT,
@TipoMaterial VARCHAR(25),
@Material VARCHAR(500),
@Unidad VARCHAR(10),
@PrecioUnitario DECIMAL(10,2),
@Cantidad DECIMAL(10,2),
@ImporteTotal DECIMAL(10,2)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT
DECLARE @correlativo2 INT

SET @Exito = '0 = Exito.'

SET @correlativo = (SELECT MAX(idIncidenteD) FROM ReportesApp_Mantenimiento_RegistroIncidencias_Detalle WHERE idIncidenteC = @idIncidenteC)
SET @correlativo = ISNULL(@correlativo,0) + 1

SET @correlativo2 = (SELECT MAX(idRegistroIncD) FROM ReportesApp_Seguridad_RegistroIncidencias_Detalle WHERE idRegistroInc = @idIncidenteC)
SET @correlativo2 = ISNULL(@correlativo2,0) + 1

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- REGISTRAR INCIDENCIA MTTO
		INSERT INTO ReportesApp_Mantenimiento_RegistroIncidencias_Detalle(idIncidenteD,idIncidenteC,TipoMaterial,Material,Unidad,PrecioUnitario,Cantidad,ImporteTotal)
		VALUES(@correlativo,@idIncidenteC,@TipoMaterial,@Material,@Unidad,@PrecioUnitario,@Cantidad,@ImporteTotal)
	END
	ELSE BEGIN		-- REGISTRAR INCIDENCIA SSOMAC
		INSERT INTO ReportesApp_Seguridad_RegistroIncidencias_Detalle(idRegistroIncD,idRegistroInc,TipoMaterial,Material,Unidad,PrecioUnitario,Cantidad,ImporteTotal)
		VALUES(@correlativo2,@idIncidenteC,@TipoMaterial,@Material,@Unidad,@PrecioUnitario,@Cantidad,@ImporteTotal)
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

---------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 17-02-2024
-- Description:	ELIMINAR PRESUPUESTO INCIDENCIAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_RegistroIncidencias_EliminarPresupuesto]
@Opcion INT,
@idIncidenteC INT,
@idIncidenteD INT
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Presupuesto eliminado.'

IF (@idIncidenteD = 0)
BEGIN
	SET @Exito = '-1 = El presupuesto seleccionado no existe.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- ELIMINAR PRESUPUESTO DE MTTO.
		DELETE FROM ReportesApp_Mantenimiento_RegistroIncidencias_Detalle
		WHERE idIncidenteC = @idIncidenteC AND idIncidenteD = @idIncidenteD
	
		UPDATE ReportesApp_Mantenimiento_RegistroIncidencias_Detalle
		SET idIncidenteD = idIncidenteD - 1
		WHERE idIncidenteD > @idIncidenteD AND idIncidenteC = @idIncidenteC
	END
	ELSE BEGIN		-- ELIMINAR PRESUPUESTO DE SSOMAC
		DELETE FROM ReportesApp_Seguridad_RegistroIncidencias_Detalle
		WHERE idRegistroInc = @idIncidenteC AND idRegistroIncD = @idIncidenteD
	
		UPDATE ReportesApp_Seguridad_RegistroIncidencias_Detalle
		SET idRegistroIncD = idRegistroIncD - 1
		WHERE idRegistroIncD > @idIncidenteD AND idRegistroInc = @idIncidenteC
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

---------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 14-02-2024
-- Description:	INSERTAR REGISTRO INCIDENCIAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_RegistroIncidencias_InsertarIncidencias]
@idFalla INT,
@Descripcion VARCHAR(MAX),
@TipoDanio VARCHAR(50),
@DescripcionDanio VARCHAR(250),
@Observacion VARCHAR(250),
@Imagen VARBINARY(MAX),
@Falla VARBINARY(MAX),
@Usuario VARCHAR(50)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

SET @Exito = '0 = Incidencia Registrada.'

SET @correlativo = (SELECT MAX(idIncidenteC) FROM ReportesApp_Mantenimiento_RegistroIncidencias_Cabecera)
SET @correlativo = ISNULL(@correlativo,0) + 1

BEGIN TRAN
BEGIN TRY
	INSERT INTO ReportesApp_Mantenimiento_RegistroIncidencias_Cabecera(idIncidenteC,idFalla,Descripcion,TipoDanio,DescripcionDanio,Observacion,
	UsuarioCreacion,FechaCreacion,UsuarioModificacion,FechaModificacion,SubTotal,MontoTotal,IGV,Imagen,Falla)
	VALUES(@correlativo, @idFalla, @Descripcion, @TipoDanio, @DescripcionDanio, @Observacion, @Usuario, GETDATE(),@Usuario, GETDATE(),
	0.00, 0.00, 0.00, @Imagen, @Falla)
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

---------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 14-02-2024
-- Description:	LISTAR REGISTRO INCIDENCIAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_RegistroIncidencias_ListarIncidencias]
@Placa VARCHAR(20),
@idOperacion INT,
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@idOperacion = 5) BEGIN
		SELECT IC.idIncidenteC AS 'NRO', IC.idFalla, OP.Descripcion AS 'OPERACION', V.NumeroPlaca AS 'TRACTO', R.NumeroPlaca AS 'SEMIRREMOLQUE', 
		CONVERT(VARCHAR,FM.FechaInicio,103)+' '+CONVERT(VARCHAR,FM.HoraInicio,8) AS 'FECHA_INICIO', LTRIM(RTRIM(C.Nombre)) AS 'CONDUCTOR', FM.Ubicacion AS 'UBICACIÓN',
		FM.Motivo AS 'MOTIVO', IC.MontoTotal AS 'PRECIO_TOTAL', IC.UsuarioCreacion, IC.FechaCreacion, IC.UsuarioModificacion, IC.FechaModificacion,
		IC.Descripcion, IC.TipoDanio, IC.DescripcionDanio, IC.Observacion, P.Telefono, RT.Descripcion AS 'RUTA', IC.Imagen, IC.Falla
		FROM ReportesApp_Mantenimiento_RegistroIncidencias_Cabecera IC
		LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_Registros FM ON FM.idFalla = IC.idFalla
		LEFT JOIN ReportesApp_Operacion_Previaje_Registros RE WITH(NOLOCK) ON FM.NroTicket = RE.NroTicket
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = RE.idTracto
		LEFT JOIN OP_TR_Vehiculo R WITH(NOLOCK) ON R.IdVehiculo = RE.idSemirremolque
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = RE.TipoProgramacion
		LEFT JOIN OP_TR_Conductor C WITH(NOLOCK) ON C.IdConductor = RE.IdConductor
		LEFT JOIN PersonaMast P WITH(NOLOCK) ON P.Persona = C.IdPersona
		LEFT JOIN OP_TR_Ruta RT WITH(NOLOCK) ON RT.IdRuta = RE.IdRuta
		WHERE (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND (FM.FechaInicio BETWEEN @FINICIO AND @FFIN)
		ORDER BY IC.idIncidenteC DESC
	END
	ELSE BEGIN
		SELECT IC.idIncidenteC AS 'NRO', IC.idFalla, OP.Descripcion AS 'OPERACION', V.NumeroPlaca AS 'TRACTO', R.NumeroPlaca AS 'SEMIRREMOLQUE', 
		CONVERT(VARCHAR,FM.FechaInicio,103)+' '+CONVERT(VARCHAR,FM.HoraInicio,8) AS 'FECHA_INICIO', LTRIM(RTRIM(C.Nombre)) AS 'CONDUCTOR', FM.Ubicacion AS 'UBICACIÓN',
		FM.Motivo AS 'MOTIVO', IC.MontoTotal AS 'PRECIO_TOTAL', IC.UsuarioCreacion, IC.FechaCreacion, IC.UsuarioModificacion, IC.FechaModificacion,
		IC.Descripcion, IC.TipoDanio, IC.DescripcionDanio, IC.Observacion, P.Telefono, RT.Descripcion AS 'RUTA', IC.Imagen, IC.Falla
		FROM ReportesApp_Mantenimiento_RegistroIncidencias_Cabecera IC
		LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_Registros FM ON FM.idFalla = IC.idFalla
		LEFT JOIN ReportesApp_Operacion_Previaje_Registros RE WITH(NOLOCK) ON FM.NroTicket = RE.NroTicket
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = RE.idTracto
		LEFT JOIN OP_TR_Vehiculo R WITH(NOLOCK) ON R.IdVehiculo = RE.idSemirremolque
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = RE.TipoProgramacion
		LEFT JOIN OP_TR_Conductor C WITH(NOLOCK) ON C.IdConductor = RE.IdConductor
		LEFT JOIN PersonaMast P WITH(NOLOCK) ON P.Persona = C.IdPersona
		LEFT JOIN OP_TR_Ruta RT WITH(NOLOCK) ON RT.IdRuta = RE.IdRuta
		WHERE (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND (FM.FechaInicio BETWEEN @FINICIO AND @FFIN) AND (RE.TipoProgramacion = @idOperacion)
		ORDER BY IC.idIncidenteC DESC
	END
END

-------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 14-02-2024
-- Description:	FILTRAR REGISTRO INCIDENCIAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_RegistroIncidencias_FiltrarEliminarIncidencias]
@Opcion INT,
@idIncidenteC INT
AS
BEGIN
	/*
	IF (@Opcion = 1) BEGIN		-- FILTRAR INCIDENCIAS
		SELECT IC.idIncidenteC AS 'NRO', V.NumeroPlaca AS 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD',
		CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'OPERACION', IC.FechaIncidente AS 'FECHA_INCIDENTE',
		P1.NombreCompleto AS 'CONDUCTOR', P2.NombreCompleto AS 'SUPERVISOR', CASE WHEN IC.RutaLocal = '' THEN 'NO ENVIADO' ELSE IC.RutaLocal END AS 'INFORME_SEGURIDAD',
		(CASE WHEN IC.RutaLocal = '' THEN 'NO ENVIADO' ELSE 'ENVIADO' END) AS 'ENVIADO', IC.SubTotal, IC.IGV, IC.MontoTotal
		FROM ReportesApp_Mantenimiento_RegistroIncidencias_Cabecera IC
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = IC.idUnidad
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON UC.IdProgramacion = O.IdOperacion
		LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = UC.IdUnidad
		LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TipoVehiculo
		LEFT JOIN PersonaMast P1 ON P1.Persona = IC.idConductor
		LEFT JOIN PersonaMast P2 ON P2.Persona = IC.idSupervisor
		WHERE IC.idIncidenteC = @idIncidenteC
	END
	*/

	IF (@Opcion = 2) BEGIN		-- ELIMINAR INCIDENCIAS
		DECLARE @idFalla INT = (SELECT idFalla FROM ReportesApp_Mantenimiento_RegistroIncidencias_Cabecera WHERE idIncidenteC = @idIncidenteC)
		
		IF ((SELECT Duracion FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros WHERE idFalla = @idFalla) IS NOT NULL) BEGIN
			SELECT '0 = Esta incidencia está siendo tratada en el área de Mtto. No puede ser eliminada' AS Mensaje
		END
		ELSE BEGIN
			DELETE FROM ReportesApp_Mantenimiento_RegistroIncidencias_Detalle WHERE idIncidenteC = @idIncidenteC

			DELETE FROM ReportesApp_Mantenimiento_RegistroIncidencias_Cabecera WHERE idIncidenteC = @idIncidenteC

			DELETE FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros WHERE idFalla = @idFalla

			SELECT '1 = Eliminado' AS Mensaje
		END
	END

	IF (@Opcion = 3) BEGIN		-- LISTAR PRESUPUESTOS
		SELECT ID.idIncidenteD AS '#', ID.idIncidenteC, ID.TipoMaterial AS 'TIPO', ID.Material AS 'DESCRIPCION', ID.Unidad AS 'UND', ID.PrecioUnitario AS 'PRECIO_UND',
		ID.Cantidad AS 'CANT', ID.ImporteTotal AS 'IMPORTE_TOTAL'
		FROM ReportesApp_Mantenimiento_RegistroIncidencias_Detalle ID
		WHERE ID.idIncidenteC = @idIncidenteC
		ORDER BY ID.idIncidenteD ASC
	END

	IF (@Opcion = 4) BEGIN		-- LISTAR UNIDADES
		SELECT UnidadCodigo AS 'CODIGO', UnidadCodigo AS 'UNIDAD' FROM UnidadesMast WHERE Estado = 'A'
	END

	IF (@Opcion = 5) BEGIN		-- LISTAR PRESUPUESTOS SSOMAC
		SELECT SD.idRegistroIncD AS '#', SD.idRegistroInc, SD.TipoMaterial AS 'TIPO', SD.Material AS 'DESCRIPCION', SD.Unidad AS 'UND', SD.PrecioUnitario AS 'PRECIO_UND',
		SD.Cantidad AS 'CANT', SD.ImporteTotal AS 'IMPORTE_TOTAL'
		FROM ReportesApp_Seguridad_RegistroIncidencias_Detalle SD
		WHERE SD.idRegistroInc = @idIncidenteC
		ORDER BY SD.idRegistroIncD ASC
	END
END

-------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 17-02-2024
-- Description:	ACTUALIZAR MONTO INCIDENCIAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_RegistroIncidencias_ActualizarMonto]
@idFalla INT,
@SubTotal DECIMAL(10,2),
@IGV DECIMAL(10,2),
@MontoTotal DECIMAL(10,2),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

SET @Exito = '0 = Monto añadido.'

BEGIN TRAN
BEGIN TRY
	DECLARE @idIncidenteC INT = (SELECT TOP(1) idIncidenteC FROM ReportesApp_Mantenimiento_RegistroIncidencias_Cabecera WHERE idFalla = @idFalla ORDER BY idFalla DESC)
	
	UPDATE ReportesApp_Mantenimiento_RegistroIncidencias_Cabecera
	SET SubTotal = @SubTotal, IGV = @IGV, MontoTotal = @MontoTotal, UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()
	WHERE idIncidenteC = @idIncidenteC

	UPDATE ReportesApp_Mantenimiento_RegistroIncidencias_Detalle
	SET idIncidenteC = @idIncidenteC
	WHERE idIncidenteC = 0
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

------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 24-08-2024
-- Description:	GENERAR REPORTE INCIDENCIAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_RegistroIncidencias_GenerarReporte]
@Opcion INT,
@FiltroFechas INT,
@Periodo VARCHAR(6)
AS
BEGIN
	IF (@FiltroFechas = 1) BEGIN
		IF (@Opcion = 1) BEGIN		-- INCIDENCIAS X PERSONA
			SELECT X.CONDUCTOR, COUNT(X.NRO) AS 'NRO_INCIDENTES' FROM
			(SELECT IC.idIncidenteC AS 'NRO', FM.EstadoAuxilio AS 'ESTADO_AUXILIO', IC.idFalla, OP.Descripcion AS 'OPERACION', V.NumeroPlaca AS 'TRACTO', R.NumeroPlaca AS 'SEMIRREMOLQUE', 
			CONVERT(VARCHAR,FM.FechaInicio,103)+' '+CONVERT(VARCHAR,FM.HoraInicio,8) AS 'FECHA_INICIO', LTRIM(RTRIM(ISNULL(C.Nombre,P.NombreCompleto))) AS 'CONDUCTOR',
			FM.Ubicacion AS 'UBICACIÓN', FM.Motivo AS 'MOTIVO', IC.MontoTotal AS 'PRECIO_TOTAL', IC.UsuarioCreacion, IC.FechaCreacion, IC.UsuarioModificacion,
			IC.FechaModificacion, IC.Descripcion, IC.TipoDanio, IC.DescripcionDanio, IC.Observacion, P.Telefono, RT.Descripcion AS 'RUTA', IC.Imagen, IC.Falla
			FROM ReportesApp_Mantenimiento_RegistroIncidencias_Cabecera IC
			LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_Registros FM ON FM.idFalla = IC.idFalla
			LEFT JOIN ReportesApp_Operacion_Previaje_Registros RE WITH(NOLOCK) ON FM.NroTicket = RE.NroTicket
			LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_ViajeTolvas TL ON TL.CodViajeT = FM.NroTicket
			LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = ISNULL(RE.idTracto,TL.idTracto)
			LEFT JOIN OP_TR_Vehiculo R WITH(NOLOCK) ON R.IdVehiculo = ISNULL(RE.idSemirremolque,TL.idCarreta)
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = ISNULL(RE.TipoProgramacion,1)
			LEFT JOIN OP_TR_Conductor C WITH(NOLOCK) ON C.IdConductor = RE.IdConductor
			LEFT JOIN PersonaMast P WITH(NOLOCK) ON P.Persona = ISNULL(C.IdPersona,TL.PersonaC)
			LEFT JOIN OP_TR_Ruta RT WITH(NOLOCK) ON RT.IdRuta = ISNULL(RE.IdRuta, TL.idRuta)
			WHERE (LTRIM(RTRIM(ISNULL(C.Nombre,P.NombreCompleto))) IS NOT NULL) AND (MONTH(FM.FechaInicio) = LEFT(@Periodo,2))
			AND (YEAR(FM.FechaInicio) = RIGHT(@Periodo,4))) X
			GROUP BY X.CONDUCTOR
			ORDER BY COUNT(X.NRO) DESC
		END

		IF (@Opcion = 2) BEGIN		-- INCIDENCIAS X OPERACION
			SELECT X.OPERACION, COUNT(X.NRO) AS 'NRO_INCIDENTES' FROM
			(SELECT IC.idIncidenteC AS 'NRO', FM.EstadoAuxilio AS 'ESTADO_AUXILIO', IC.idFalla, OP.Descripcion AS 'OPERACION', V.NumeroPlaca AS 'TRACTO', R.NumeroPlaca AS 'SEMIRREMOLQUE', 
			CONVERT(VARCHAR,FM.FechaInicio,103)+' '+CONVERT(VARCHAR,FM.HoraInicio,8) AS 'FECHA_INICIO', LTRIM(RTRIM(ISNULL(C.Nombre,P.NombreCompleto))) AS 'CONDUCTOR',
			FM.Ubicacion AS 'UBICACIÓN', FM.Motivo AS 'MOTIVO', IC.MontoTotal AS 'PRECIO_TOTAL', IC.UsuarioCreacion, IC.FechaCreacion, IC.UsuarioModificacion,
			IC.FechaModificacion, IC.Descripcion, IC.TipoDanio, IC.DescripcionDanio, IC.Observacion, P.Telefono, RT.Descripcion AS 'RUTA', IC.Imagen, IC.Falla
			FROM ReportesApp_Mantenimiento_RegistroIncidencias_Cabecera IC
			LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_Registros FM ON FM.idFalla = IC.idFalla
			LEFT JOIN ReportesApp_Operacion_Previaje_Registros RE WITH(NOLOCK) ON FM.NroTicket = RE.NroTicket
			LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_ViajeTolvas TL ON TL.CodViajeT = FM.NroTicket
			LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = ISNULL(RE.idTracto,TL.idTracto)
			LEFT JOIN OP_TR_Vehiculo R WITH(NOLOCK) ON R.IdVehiculo = ISNULL(RE.idSemirremolque,TL.idCarreta)
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = ISNULL(RE.TipoProgramacion,1)
			LEFT JOIN OP_TR_Conductor C WITH(NOLOCK) ON C.IdConductor = RE.IdConductor
			LEFT JOIN PersonaMast P WITH(NOLOCK) ON P.Persona = ISNULL(C.IdPersona,TL.PersonaC)
			LEFT JOIN OP_TR_Ruta RT WITH(NOLOCK) ON RT.IdRuta = ISNULL(RE.IdRuta, TL.idRuta)
			WHERE (OP.Descripcion IS NOT NULL) AND (MONTH(FM.FechaInicio) = LEFT(@Periodo,2)) AND (YEAR(FM.FechaInicio) = RIGHT(@Periodo,4))) X
			GROUP BY X.OPERACION
			ORDER BY COUNT(X.NRO) DESC
		END
	END
	
	IF (@FiltroFechas = 0) BEGIN
		IF (@Opcion = 1) BEGIN		-- INCIDENCIAS X PERSONA
			SELECT X.CONDUCTOR, COUNT(X.NRO) AS 'NRO_INCIDENTES' FROM
			(SELECT IC.idIncidenteC AS 'NRO', FM.EstadoAuxilio AS 'ESTADO_AUXILIO', IC.idFalla, OP.Descripcion AS 'OPERACION', V.NumeroPlaca AS 'TRACTO', R.NumeroPlaca AS 'SEMIRREMOLQUE', 
			CONVERT(VARCHAR,FM.FechaInicio,103)+' '+CONVERT(VARCHAR,FM.HoraInicio,8) AS 'FECHA_INICIO', LTRIM(RTRIM(ISNULL(C.Nombre,P.NombreCompleto))) AS 'CONDUCTOR',
			FM.Ubicacion AS 'UBICACIÓN', FM.Motivo AS 'MOTIVO', IC.MontoTotal AS 'PRECIO_TOTAL', IC.UsuarioCreacion, IC.FechaCreacion, IC.UsuarioModificacion,
			IC.FechaModificacion, IC.Descripcion, IC.TipoDanio, IC.DescripcionDanio, IC.Observacion, P.Telefono, RT.Descripcion AS 'RUTA', IC.Imagen, IC.Falla
			FROM ReportesApp_Mantenimiento_RegistroIncidencias_Cabecera IC
			LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_Registros FM ON FM.idFalla = IC.idFalla
			LEFT JOIN ReportesApp_Operacion_Previaje_Registros RE WITH(NOLOCK) ON FM.NroTicket = RE.NroTicket
			LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_ViajeTolvas TL ON TL.CodViajeT = FM.NroTicket
			LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = ISNULL(RE.idTracto,TL.idTracto)
			LEFT JOIN OP_TR_Vehiculo R WITH(NOLOCK) ON R.IdVehiculo = ISNULL(RE.idSemirremolque,TL.idCarreta)
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = ISNULL(RE.TipoProgramacion,1)
			LEFT JOIN OP_TR_Conductor C WITH(NOLOCK) ON C.IdConductor = RE.IdConductor
			LEFT JOIN PersonaMast P WITH(NOLOCK) ON P.Persona = ISNULL(C.IdPersona,TL.PersonaC)
			LEFT JOIN OP_TR_Ruta RT WITH(NOLOCK) ON RT.IdRuta = ISNULL(RE.IdRuta, TL.idRuta)
			WHERE (LTRIM(RTRIM(ISNULL(C.Nombre,P.NombreCompleto))) IS NOT NULL)) X
			GROUP BY X.CONDUCTOR
			ORDER BY COUNT(X.NRO) DESC
		END

		IF (@Opcion = 2) BEGIN		-- INCIDENCIAS X OPERACION
			SELECT X.OPERACION, COUNT(X.NRO) AS 'NRO_INCIDENTES' FROM
			(SELECT IC.idIncidenteC AS 'NRO', FM.EstadoAuxilio AS 'ESTADO_AUXILIO', IC.idFalla, OP.Descripcion AS 'OPERACION', V.NumeroPlaca AS 'TRACTO', R.NumeroPlaca AS 'SEMIRREMOLQUE', 
			CONVERT(VARCHAR,FM.FechaInicio,103)+' '+CONVERT(VARCHAR,FM.HoraInicio,8) AS 'FECHA_INICIO', LTRIM(RTRIM(ISNULL(C.Nombre,P.NombreCompleto))) AS 'CONDUCTOR',
			FM.Ubicacion AS 'UBICACIÓN', FM.Motivo AS 'MOTIVO', IC.MontoTotal AS 'PRECIO_TOTAL', IC.UsuarioCreacion, IC.FechaCreacion, IC.UsuarioModificacion,
			IC.FechaModificacion, IC.Descripcion, IC.TipoDanio, IC.DescripcionDanio, IC.Observacion, P.Telefono, RT.Descripcion AS 'RUTA', IC.Imagen, IC.Falla
			FROM ReportesApp_Mantenimiento_RegistroIncidencias_Cabecera IC
			LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_Registros FM ON FM.idFalla = IC.idFalla
			LEFT JOIN ReportesApp_Operacion_Previaje_Registros RE WITH(NOLOCK) ON FM.NroTicket = RE.NroTicket
			LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_ViajeTolvas TL ON TL.CodViajeT = FM.NroTicket
			LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = ISNULL(RE.idTracto,TL.idTracto)
			LEFT JOIN OP_TR_Vehiculo R WITH(NOLOCK) ON R.IdVehiculo = ISNULL(RE.idSemirremolque,TL.idCarreta)
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = ISNULL(RE.TipoProgramacion,1)
			LEFT JOIN OP_TR_Conductor C WITH(NOLOCK) ON C.IdConductor = RE.IdConductor
			LEFT JOIN PersonaMast P WITH(NOLOCK) ON P.Persona = ISNULL(C.IdPersona,TL.PersonaC)
			LEFT JOIN OP_TR_Ruta RT WITH(NOLOCK) ON RT.IdRuta = ISNULL(RE.IdRuta, TL.idRuta)
			WHERE (OP.Descripcion IS NOT NULL)) X
			GROUP BY X.OPERACION
			ORDER BY COUNT(X.NRO) DESC
		END
	END
END

------------------------------------------------------------------------------------
------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 18-07-2024
-- Description:	LISTAR INCIDENTES DE SEGURIDAD
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_RegistroIncidencias_ListarAccidentes]
@Opcion INT
AS
BEGIN
	IF (@Opcion = 1) BEGIN		-- LISTAR ACCIDENTES DE SEGURIDAD
		SELECT idAccidente, Descripcion FROM ReportesApp_Seguridad_RegistroIncidencias_Accidentes
	END

	IF (@Opcion = 2) BEGIN		-- LISTAR TODOS LOS ACCIDENTES
		SELECT 0 AS idAccidente, 'TODOS' AS Descripcion
		UNION
		SELECT idAccidente, Descripcion FROM ReportesApp_Seguridad_RegistroIncidencias_Accidentes
	END
END

------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 19-07-2024
-- Description:	LISTAR REGISTRO INCIDENCIAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_RegistroIncidencias_ListarIncidencias]
@TipoIncidente VARCHAR(250),
@Sede VARCHAR(30),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@TipoIncidente = 'TODOS' AND @Sede = 'Todas') BEGIN
		SELECT RI.idRegistroInc AS '#', RI.idPersona, RI.Persona AS 'NOMBRE_COMPLETO', RI.Area AS 'ÁREA', RI.Sede AS 'SEDE', RI.Grado AS 'GRADO',
		RI.FechaOcurrencia AS 'FECHA_OCURRENCIA', RI.TipoIncidente, A.Descripcion AS 'TIPO_INCIDENTE', RI.TipoDanio AS 'TIPO_DAÑO', RI.MontoTotal AS 'PRECIO_TOTAL',
		RI.Tracto AS 'TRACTO', RI.Carreta AS 'CARRETA', RI.Operacion AS 'OPERACIÓN', RI.DescripcionDanio AS 'DESCRIPCIÓN', RI.Observacion AS 'OBSERVACIÓN',
		RI.Incidente, RI.IAdicional, RI.UsuarioCreacion, RI.FechaCreacion, RI.UsuarioModificacion, RI.FechaModificacion
		FROM ReportesApp_Seguridad_RegistroIncidencias_Registros RI
		LEFT JOIN ReportesApp_Seguridad_RegistroIncidencias_Accidentes A ON A.idAccidente = RI.TipoIncidente
		WHERE (RI.FechaOcurrencia BETWEEN @FINICIO AND @FFIN)
		ORDER BY RI.idRegistroInc DESC
	END

	IF (@TipoIncidente = 'TODOS' AND @Sede != 'Todas') BEGIN
		SELECT RI.idRegistroInc AS '#', RI.idPersona, RI.Persona AS 'NOMBRE_COMPLETO', RI.Area AS 'ÁREA', RI.Sede AS 'SEDE', RI.Grado AS 'GRADO',
		RI.FechaOcurrencia AS 'FECHA_OCURRENCIA', RI.TipoIncidente, A.Descripcion AS 'TIPO_INCIDENTE', RI.TipoDanio AS 'TIPO_DAÑO', RI.MontoTotal AS 'PRECIO_TOTAL',
		RI.Tracto AS 'TRACTO', RI.Carreta AS 'CARRETA', RI.Operacion AS 'OPERACIÓN', RI.DescripcionDanio AS 'DESCRIPCIÓN', RI.Observacion AS 'OBSERVACIÓN',
		RI.Incidente, RI.IAdicional, RI.UsuarioCreacion, RI.FechaCreacion, RI.UsuarioModificacion, RI.FechaModificacion
		FROM ReportesApp_Seguridad_RegistroIncidencias_Registros RI
		LEFT JOIN ReportesApp_Seguridad_RegistroIncidencias_Accidentes A ON A.idAccidente = RI.TipoIncidente
		WHERE (RI.Sede = @Sede) AND (RI.FechaOcurrencia BETWEEN @FINICIO AND @FFIN)
		ORDER BY RI.idRegistroInc DESC
	END

	IF (@TipoIncidente != 'TODOS' AND @Sede = 'Todas') BEGIN
		SELECT RI.idRegistroInc AS '#', RI.idPersona, RI.Persona AS 'NOMBRE_COMPLETO', RI.Area AS 'ÁREA', RI.Sede AS 'SEDE', RI.Grado AS 'GRADO',
		RI.FechaOcurrencia AS 'FECHA_OCURRENCIA', RI.TipoIncidente, A.Descripcion AS 'TIPO_INCIDENTE', RI.TipoDanio AS 'TIPO_DAÑO', RI.MontoTotal AS 'PRECIO_TOTAL',
		RI.Tracto AS 'TRACTO', RI.Carreta AS 'CARRETA', RI.Operacion AS 'OPERACIÓN', RI.DescripcionDanio AS 'DESCRIPCIÓN', RI.Observacion AS 'OBSERVACIÓN',
		RI.Incidente, RI.IAdicional, RI.UsuarioCreacion, RI.FechaCreacion, RI.UsuarioModificacion, RI.FechaModificacion
		FROM ReportesApp_Seguridad_RegistroIncidencias_Registros RI
		LEFT JOIN ReportesApp_Seguridad_RegistroIncidencias_Accidentes A ON A.idAccidente = RI.TipoIncidente
		WHERE (A.Descripcion = @TipoIncidente) AND (RI.FechaOcurrencia BETWEEN @FINICIO AND @FFIN)
		ORDER BY RI.idRegistroInc DESC
	END

	IF (@TipoIncidente != 'TODOS' AND @Sede != 'Todas') BEGIN
		SELECT RI.idRegistroInc AS '#', RI.idPersona, RI.Persona AS 'NOMBRE_COMPLETO', RI.Area AS 'ÁREA', RI.Sede AS 'SEDE', RI.Grado AS 'GRADO',
		RI.FechaOcurrencia AS 'FECHA_OCURRENCIA', RI.TipoIncidente, A.Descripcion AS 'TIPO_INCIDENTE', RI.TipoDanio AS 'TIPO_DAÑO', RI.MontoTotal AS 'PRECIO_TOTAL',
		RI.Tracto AS 'TRACTO', RI.Carreta AS 'CARRETA', RI.Operacion AS 'OPERACIÓN', RI.DescripcionDanio AS 'DESCRIPCIÓN', RI.Observacion AS 'OBSERVACIÓN',
		RI.Incidente, RI.IAdicional, RI.UsuarioCreacion, RI.FechaCreacion, RI.UsuarioModificacion, RI.FechaModificacion
		FROM ReportesApp_Seguridad_RegistroIncidencias_Registros RI
		LEFT JOIN ReportesApp_Seguridad_RegistroIncidencias_Accidentes A ON A.idAccidente = RI.TipoIncidente
		WHERE (A.Descripcion = @TipoIncidente) AND (RI.Sede = @Sede) AND (RI.FechaOcurrencia BETWEEN @FINICIO AND @FFIN)
		ORDER BY RI.idRegistroInc DESC
	END
END

------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 20-07-2024
-- Description:	INSERTAR NUEVO INCIDENTE
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_RegistroIncidencias_InsertarIncidente]
@Incidente VARCHAR(250)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

BEGIN TRAN
BEGIN TRY
	IF (EXISTS(SELECT * FROM ReportesApp_Seguridad_RegistroIncidencias_Accidentes WHERE Descripcion = @Incidente)) BEGIN
		SET @Exito = '-1 = Este incidente ya fue registrado.'
		ROLLBACK
		GOTO Terminar
	END
	ELSE BEGIN
		SET @correlativo = (SELECT MAX(idAccidente) FROM ReportesApp_Seguridad_RegistroIncidencias_Accidentes)
		SET @correlativo = ISNULL(@correlativo,0) + 1

		INSERT INTO ReportesApp_Seguridad_RegistroIncidencias_Accidentes(idAccidente,Descripcion)
		VALUES(@correlativo, @Incidente)

		SET @Exito = '0 = Incidente añadido.'
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
-- Create date: 08-08-2024
-- Description:	BUSCAR INCIDENCIA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_RegistroIncidencias_BuscarIncidencias]
@idIncidenteC INT
AS
BEGIN
	SELECT IC.idIncidenteC, IC.idFalla, A.Descripcion AS 'Auxilio', FM.TipoFalla, FM.FechaInicio, FM.HoraInicio, FM.Ubicacion, FM.Motivo, IC.DescripcionDanio,
	IC.Descripcion, IC.TipoDanio, IC.Observacion, IC.Imagen, IC.Falla
	FROM ReportesApp_Mantenimiento_RegistroIncidencias_Cabecera IC
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_Registros FM ON FM.idFalla = IC.idFalla
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_TipoAuxilio A ON A.idTipoAuxilio = FM.idTipoAuxilio
	WHERE IC.idIncidenteC = @idIncidenteC
END

------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 08-08-2024
-- Description:	ACTUALIZAR REGISTRO INCIDENCIA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_RegistroIncidencias_ActualizarIncidencias]
@idIncidenteC INT,
@idTipoAuxilio INT,
@TipoFalla VARCHAR(50),
@FechaInicio DATE,
@HoraInicio TIME(3),
@Ubicacion VARCHAR(250),
@Motivo VARCHAR(250),
@Descripcion VARCHAR(MAX),
@TipoDanio VARCHAR(50),
@Observacion VARCHAR(250),
@Imagen VARBINARY(MAX),
@Falla VARBINARY(MAX),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Incidencia modificada exitosamente'

BEGIN TRAN
BEGIN TRY
	DECLARE @idFalla INT = (SELECT idFalla FROM ReportesApp_Mantenimiento_RegistroIncidencias_Cabecera WHERE idIncidenteC = @idIncidenteC)

	UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
	SET idTipoAuxilio = @idTipoAuxilio, TipoFalla = @TipoFalla, Motivo = @Motivo, FechaInicio = @FechaInicio, HoraInicio = @HoraInicio, Ubicacion = @Ubicacion,
	UltimoUsuario = @Usuario, UltimaModificacion = GETDATE()
	WHERE idFalla = @idFalla

	UPDATE ReportesApp_Mantenimiento_RegistroIncidencias_Cabecera
	SET DescripcionDanio = @Motivo, Descripcion = @Descripcion, TipoDanio = @TipoDanio, Observacion = @Observacion, Imagen = @Imagen,
	Falla = @Falla, UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()
	WHERE idIncidenteC = @idIncidenteC
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
-------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 18-07-2024
-- Description:	INSERTAR REGISTRO INCIDENCIAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_RegistroIncidencias_InsertarModificarIncidencias]
@Opcion INT,
@idRegistroInc INT,
@idPersona INT,
@Persona VARCHAR(250),
@Area VARCHAR(30),
@Sede VARCHAR(30),
@Grado VARCHAR(30),
@Fecha VARCHAR(10),
@Hora VARCHAR(10),
@TipoIncidente INT,
@TipoDanio VARCHAR(50),
@Tracto VARCHAR(30),
@Carreta VARCHAR(30),
@Operacion VARCHAR(30),
@DescripcionDanio VARCHAR(250),
@Observacion VARCHAR(250),
@Incidente VARBINARY(MAX),
@IAdicional VARBINARY(MAX),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- INSERTAR INCIDENTES
		SET @correlativo = (SELECT MAX(idRegistroInc) FROM ReportesApp_Seguridad_RegistroIncidencias_Registros)
		SET @correlativo = ISNULL(@correlativo,0) + 1

		INSERT INTO ReportesApp_Seguridad_RegistroIncidencias_Registros(idRegistroInc,idPersona,Persona,Area,Sede,Grado,FechaOcurrencia,TipoIncidente,
		TipoDanio,Tracto,Carreta,Operacion,DescripcionDanio,Observacion,Incidente,IAdicional,UsuarioCreacion,FechaCreacion,UsuarioModificacion,FechaModificacion,
		MontoTotal,IGV,SubTotal,Estado)
		VALUES(@correlativo,@idPersona,@Persona,@Area,@Sede,@Grado,CONVERT(DATETIME,@Fecha+' '+@Hora),@TipoIncidente,@TipoDanio,@Tracto,@Carreta,@Operacion,
		@DescripcionDanio,@Observacion,@Incidente,@IAdicional,@Usuario,GETDATE(),@Usuario,GETDATE(),0.00,0.00,0.00,'PENDIENTE')

		IF (EXISTS(SELECT * FROM OP_TR_Conductor WHERE Estado = 'A' AND IdPersona = @idPersona)) BEGIN
			IF (@TipoIncidente IN (1,4)) BEGIN
				EXEC ReportesApp_Operaciones_Conductor_BloquearDesbloquear @Opcion=1, @IDBloqueo=0, @IDPersona=@idPersona, @Motivo = @DescripcionDanio, @User = @Usuario,
				@IdMotivo = 1, @IdDesbloqueo = 1
			END
		END

		INSERT INTO ReportesApp_Seguridad_RegistroIncidencias_Estado(idRegistroInc, EstadoSSOMAC, EstadoMtto, EstadoCostos, ResponsableGEROP)
		VALUES(@correlativo, 'REGISTRADO', 'PENDIENTE', '-', '-')

		SET @Exito = '0 = Incidente Registrado.'
	END

	IF (@Opcion = 2) BEGIN		-- MODIFICAR INCIDENTES
		UPDATE ReportesApp_Seguridad_RegistroIncidencias_Registros
		SET idPersona = @idPersona, Persona = @Persona, Area = @Area, Sede = @Sede, Grado = @Grado, FechaOcurrencia = CONVERT(DATETIME,@Fecha+' '+@Hora),
		TipoIncidente = @TipoIncidente, TipoDanio = @TipoDanio, Tracto = @Tracto, Carreta = @Carreta, Operacion = @Operacion, DescripcionDanio = @DescripcionDanio,
		Observacion = @Observacion, Incidente = @Incidente, IAdicional = @IAdicional, UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()
		WHERE idRegistroInc = @idRegistroInc

		SET @Exito = '0 = Incidente Modificado.'
	END

	IF (@Opcion = 3) BEGIN		-- ELIMINAR INCIDENTES
		IF ((SELECT Estado FROM ReportesApp_Seguridad_RegistroIncidencias_Registros WHERE idRegistroInc = @idRegistroInc) != 'PENDIENTE') BEGIN
			SET @Exito = '-1 = Solo puede eliminar incidencias pendientes por revisar.'
			GOTO Terminar
		END
		ELSE BEGIN
			DELETE FROM ReportesApp_Seguridad_RegistroIncidencias_Registros
			WHERE idRegistroInc = @idRegistroInc

			DELETE FROM ReportesApp_Seguridad_RegistroIncidencias_Estado
			WHERE idRegistroInc = @idRegistroInc

			SET @Exito = '0 = Incidente Eliminado.'
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

------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 05-08-2024
-- Description:	ACTUALIZAR MONTO SSOMAC
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_RegistroIncidencias_ActualizarMontoSSOMAC]
@idIncidenteC INT,
@SubTotal DECIMAL(10,2),
@IGV DECIMAL(10,2),
@MontoTotal DECIMAL(10,2),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Monto añadido.'

BEGIN TRAN
BEGIN TRY
	UPDATE ReportesApp_Seguridad_RegistroIncidencias_Registros
	SET SubTotal = @SubTotal, IGV = @IGV, MontoTotal = @MontoTotal, UsuarioModificacion = @Usuario, Estado = 'PRESUPUESTADO',
	FechaModificacion = GETDATE()
	WHERE idRegistroInc = @idIncidenteC

	UPDATE ReportesApp_Seguridad_RegistroIncidencias_Detalle
	SET idRegistroInc = @idIncidenteC
	WHERE idRegistroInc = 0

	UPDATE ReportesApp_Seguridad_RegistroIncidencias_Estado
	SET EstadoMtto = 'PRESUPUESTADO'
	WHERE idRegistroInc = @idIncidenteC
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
-- Create date: 29-01-2025
-- Description:	LISTAR ESTADO INCIDENCIAS
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Seguridad_RegistroIncidencias_ListarEstadoIncidencias]
@idRegistroInc INT
AS
BEGIN
	SELECT idRegistroInc, EstadoSSOMAC, EstadoMtto, EstadoCostos, ObservacionCostos, ResponsableGEROP, ObservacionGEROP, ImagenGTH, ObservacionGTH
	FROM ReportesApp_Seguridad_RegistroIncidencias_Estado
	WHERE idRegistroInc = @idRegistroInc
END

------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 29-01-2025
-- Description:	ACTUALIZAR ESTADO INCIDENCIAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_RegistroIncidencias_ActualizarEstadoIncidencias]
@Opcion INT,
@idRegistroInc INT,
@Estado VARCHAR(200),
@Observacion VARCHAR(300),
@ValeDcto VARBINARY(MAX),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- INSERTAR APROBACION COSTOS
		IF ((SELECT EstadoMtto FROM ReportesApp_Seguridad_RegistroIncidencias_Estado WHERE idRegistroInc = @idRegistroInc) != 'PRESUPUESTADO') BEGIN
			SET @Exito = '-1 = Aún no se ha registrado el presupuesto de este incidente.'
			GOTO Terminar
		END
		ELSE BEGIN
			UPDATE ReportesApp_Seguridad_RegistroIncidencias_Estado
			SET EstadoCostos = @Estado, ObservacionCostos = @Observacion
			WHERE idRegistroInc = @idRegistroInc
			
			UPDATE ReportesApp_Seguridad_RegistroIncidencias_Registros
			SET Estado = @Estado, UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()
			WHERE idRegistroInc = @idRegistroInc

			SET @Exito = '0 = Estado Actualizado.'
		END
	END

	IF (@Opcion = 2) BEGIN		-- INSERTAR RESPONSABLE
		IF ((SELECT EstadoCostos FROM ReportesApp_Seguridad_RegistroIncidencias_Estado WHERE idRegistroInc = @idRegistroInc) != 'APROBADO') BEGIN
			SET @Exito = '-2 = Aún no se ha aprobado el costo del presupuesto.'
			GOTO Terminar
		END
		ELSE BEGIN
			UPDATE ReportesApp_Seguridad_RegistroIncidencias_Estado
			SET ResponsableGEROP = @Estado, ObservacionGEROP = @Observacion
			WHERE idRegistroInc = @idRegistroInc
			
			IF (@Estado = 'EMPLEADO') BEGIN
				UPDATE ReportesApp_Seguridad_RegistroIncidencias_Registros
				SET Estado = 'REVISADO', UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()
				WHERE idRegistroInc = @idRegistroInc
			END

			IF (@Estado = 'TRANSPESA') BEGIN
				UPDATE ReportesApp_Seguridad_RegistroIncidencias_Registros
				SET Estado = 'CERRADO', UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()
				WHERE idRegistroInc = @idRegistroInc
			END

			IF (@Estado = '-') BEGIN
				UPDATE ReportesApp_Seguridad_RegistroIncidencias_Registros
				SET Estado = '-', UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()
				WHERE idRegistroInc = @idRegistroInc
			END

			SET @Exito = '0 = Estado Actualizado.'
		END
	END

	IF (@Opcion = 3) BEGIN		-- INSERTAR VALE GTH
		IF ((SELECT ResponsableGEROP FROM ReportesApp_Seguridad_RegistroIncidencias_Estado WHERE idRegistroInc = @idRegistroInc) != 'EMPLEADO') BEGIN
			SET @Exito = '-3 = La compañía ha asumido los gastos y no es necesario un vale de descuento.'
			GOTO Terminar
		END
		ELSE BEGIN
			UPDATE ReportesApp_Seguridad_RegistroIncidencias_Estado
			SET ImagenGTH = @ValeDcto, ObservacionGTH = @Observacion
			WHERE idRegistroInc = @idRegistroInc
			
			UPDATE ReportesApp_Seguridad_RegistroIncidencias_Registros
			SET Estado = 'CERRADO', UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()
			WHERE idRegistroInc = @idRegistroInc

			SET @Exito = '0 = Estado Actualizado.'
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