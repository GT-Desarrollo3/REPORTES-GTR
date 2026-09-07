
-- CREAR TABLA ReportesApp_Operaciones_Previajes_TiemposViajes

-- CREAR TABLA ReportesApp_Operaciones_Previajes_UbicacionRuta

-- CREAR TABLA ReportesApp_Operaciones_Previajes_Pernoctes

-- CREAR TABLA ReportesApp_Operaciones_Previajes_TiempoAtencion

-----------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 23-12-2024
-- Description:	LISTAR UBICACIONES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Previajes_ListarUbicaciones]
@Opcion INT,
@Operacion VARCHAR(250),
@Estado VARCHAR(100),
@RutaViaje VARCHAR(250)
AS
BEGIN
	IF (@Opcion = 1) BEGIN		-- LISTAR RUTA
		SELECT DISTINCT Ruta FROM ReportesApp_Operaciones_Previajes_UbicacionRuta
	END 

	IF (@Opcion = 2) BEGIN		-- LISTAR UBICACIONES
		IF (@Estado = 'IDA') BEGIN
			SELECT Ubicacion, CONVERT(VARCHAR,ISNULL(AvanceIda,0.00) * 100) AS 'AVANCE'
			FROM ReportesApp_Operaciones_Previajes_UbicacionRuta
			WHERE Ruta = @RutaViaje AND Operacion = @Operacion
			ORDER BY AvanceIda
		END
		ELSE BEGIN
			SELECT Ubicacion, CONVERT(VARCHAR,ISNULL(AvanceRet,0.00) * 100) AS 'AVANCE'
			FROM ReportesApp_Operaciones_Previajes_UbicacionRuta
			WHERE Ruta = @RutaViaje AND Operacion = @Operacion
			ORDER BY AvanceRet
		END
	END
END

-----------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 17/12/2024
-- Description:	FILTRAR TIEMPO DE VIAJES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Previajes_FiltrarTiemposViajes]
@NroTicket INT
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @FechaVacia DATETIME = CONVERT(DATETIME,CONVERT(VARCHAR,CONVERT(DATE,GETDATE()),103) + ' 00:00:00')
	DECLARE @Operacion INT = (SELECT TipoProgramacion FROM ReportesApp_Operacion_Previaje_Registros WHERE NroTicket = @NroTicket)
	
	IF (@Operacion = 2) BEGIN
		SELECT ISNULL(TV.SalidaBase,@FechaVacia) AS 'SALIDA_BASE', ISNULL(TV.LlegadaPlanta,@FechaVacia) AS 'LLEGADA_PLANTA', ISNULL(TV.IngresoPlanta,@FechaVacia) AS 'INGRESO_PLANTA',
		ISNULL(TV.InicioAtencion,@FechaVacia) AS 'INICIO_ATENCION', ISNULL(TV.FinAtencion,@FechaVacia) AS 'FIN_ATENCION', ISNULL(TV.EntregaGuia,@FechaVacia) AS 'ENTREGA_GUIA',
		ISNULL(TV.SalidaPlanta,@FechaVacia) AS 'SALIDA_PLANTA', ISNULL(TV.SalidaRuta,@FechaVacia) AS 'SALIDA_RUTA', ISNULL(TV.LlegadaCDA,@FechaVacia) AS 'LLEGADA_CDA',
		ISNULL(TV.InicioDescarga,@FechaVacia) AS 'INICIO_DESCARGA', ISNULL(TV.FinDescarga,@FechaVacia) AS 'FIN_DESCARGA', ISNULL(TV.LlegadaCDA2,@FechaVacia) AS 'LLEGADA_CDA_2',
		ISNULL(TV.InicioDescarga2,@FechaVacia) AS 'INICIO_DESCARGA_2', ISNULL(TV.FinDescarga2,@FechaVacia) AS 'FIN_DESCARGA_2', ISNULL(TV.LlegadaBase,@FechaVacia) AS 'LLEGADA_BASE',
		ISNULL(TV.Ubicacion,' ') AS 'UBICACION', CONVERT(VARCHAR,ISNULL(TV.PorcTransito,0.00)) + ' %' AS 'PORCENTAJE', ISNULL(TV.Estado,' ') AS 'ESTADO'
		FROM ReportesApp_Operacion_Previaje_Registros PR
		LEFT JOIN ReportesApp_Operaciones_Previajes_TiemposViajes TV ON TV.NroTicket = PR.NroTicket
		WHERE PR.NroTicket = @NroTicket
	END
	ELSE BEGIN
		SELECT ISNULL(TV.SalidaBase,@FechaVacia) AS 'SALIDA_BASE', ISNULL(TV.LlegadaCarga,@FechaVacia) AS 'LLEGADA_CARGA', ISNULL(TV.Carga,@FechaVacia) AS 'CARGA',
		ISNULL(TV.SalidaPlanta,@FechaVacia) AS 'SALIDA_PLANTA', ISNULL(TV.LlegadaDescarga,@FechaVacia) AS 'LLEGADA_DESCARGA', ISNULL(TV.InicioDescarga,@FechaVacia) AS 'INICIO_DESCARGA',
		ISNULL(TV.SalidaDescarga,@FechaVacia) AS 'SALIDA_DESCARGA', ISNULL(TV.LlegadaBase,@FechaVacia) AS 'LLEGADA_BASE', ISNULL(TV.Ubicacion,' ') AS 'UBICACION', ISNULL(TV.Estado,' ') AS 'ESTADO'
		FROM ReportesApp_Operacion_Previaje_Registros PR
		LEFT JOIN ReportesApp_Operaciones_Previajes_TiemposViajes TV ON TV.NroTicket = PR.NroTicket
		WHERE PR.NroTicket = @NroTicket
	END
END

-----------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		REYES HORNA, GERARDO
-- Create date: 16/12/2024
-- Description:	LISTAR TIEMPOS VIAJE
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Previajes_ListarTiemposViajes] 
@Previaje VARCHAR(20),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@Ruta VARCHAR(160),
@Programacion VARCHAR(50),
@Placa VARCHAR(50),
@Conductor VARCHAR(250)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@Programacion = 'LINDLEY') BEGIN
		SELECT PR.NroTicket AS 'PREVIAJE', PR.CodViaje AS 'CODIGO_VIAJE', PR.idRuta, CONVERT(VARCHAR,PR.FechaProgramacion,103) AS 'FECHA_PROGRAMACION',		O.Descripcion AS 'PROGRAMACION', LTRIM(RTRIM(RT.Descripcion)) AS 'RUTA', LTRIM(RTRIM(C.Nombre)) AS 'CONDUCTOR', V.NumeroPlaca AS 'TRACTO',		R.NumeroPlaca AS 'SEMIRREMOLQUE', PR.Sucursal AS 'SUCURSAL', TV.Estado AS 'ESTADO', TV.RutaViaje AS 'RUTA_VIAJE', TV.EstadoViaje AS 'ESTADO_VIAJE',		TV.Ubicacion AS 'UBICACION', TV.PorcTransito AS 'PORC_TRANSITO', TV.SalidaBase AS 'SALIDA_BASE', TV.LlegadaPlanta AS 'LLEGADA_PLANTA',		TV.IngresoPlanta AS 'INGRESO_PLANTA', TV.InicioAtencion AS 'INICIO_ATENCION', TV.FinAtencion AS 'FIN_ATENCION', TV.EntregaGuia AS 'ENTREGA_GUIA',		TV.SalidaPlanta AS 'SALIDA_PLANTA', TV.SalidaRuta AS 'SALIDA_RUTA', TV.LlegadaCDA AS 'LLEGADA_CDA', TV.InicioDescarga AS 'INICIO_DESCARGA',		TV.FinDescarga AS 'FIN_DESCARGA', TV.LlegadaCDA2 AS 'LLEGADA_CDA_2', TV.InicioDescarga2 AS 'INICIO_DESCARGA_2', TV.FinDescarga2 AS 'FIN_DESCARGA_2',		TV.LlegadaBase AS 'LLEGADA_BASE', TV.UsuarioCrea, TV.FechaCrea		FROM ReportesApp_Operacion_Previaje_Registros PR		LEFT JOIN ReportesApp_Operacion_Previajes_Consolidados PC ON PR.IdProgramacion = PC.IdProgramacion AND PR.Anio = PC.Anio		LEFT JOIN OP_TR_RUTA RT WITH(NOLOCK) ON RT.IDRUTA = ISNULL(PC.IdRuta,PR.IdRuta)		LEFT JOIN OP_TR_Conductor C WITH(NOLOCK) ON C.IdConductor = PR.IdConductor		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = PR.idTracto		LEFT JOIN OP_TR_Vehiculo R WITH(NOLOCK) ON R.IdVehiculo = PR.idSemirremolque		LEFT JOIN ReportesApp_Operaciones_Previajes_TiemposViajes TV ON TV.NroTicket = PR.NroTicket		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON PR.TipoProgramacion = O.IdOperacion		WHERE (PR.NroTicket LIKE '%' + @Previaje + '%') AND (PR.FechaProgramacion BETWEEN @FINICIO AND @FFIN) AND		(@Ruta IS NULL OR LTRIM(RTRIM(RT.Descripcion)) LIKE '%' + @Ruta + '%') AND (@Conductor IS NULL OR LTRIM(RTRIM(C.Nombre)) LIKE '%' + @Conductor + '%')		AND ((@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') OR (@Placa IS NULL OR R.NumeroPlaca LIKE '%' + @Placa + '%')) AND (O.Descripcion = @Programacion)
		ORDER BY PR.FechaProgramacion DESC
	END
	ELSE BEGIN
		SELECT PR.NroTicket AS 'PREVIAJE', PR.CodViaje AS 'CODIGO_VIAJE', PR.idRuta, CONVERT(VARCHAR,PR.FechaProgramacion,103) AS 'FECHA_PROGRAMACION',		O.Descripcion AS 'PROGRAMACION', LTRIM(RTRIM(RT.Descripcion)) AS 'RUTA', LTRIM(RTRIM(C.Nombre)) AS 'CONDUCTOR', V.NumeroPlaca AS 'TRACTO',		R.NumeroPlaca AS 'SEMIRREMOLQUE', PR.Sucursal AS 'SUCURSAL', TV.Estado AS 'ESTADO', TV.RutaViaje AS 'RUTA_VIAJE', TV.EstadoViaje AS 'ESTADO_VIAJE',		TV.Ubicacion AS 'UBICACION', TV.PorcTransito AS 'PORC_TRANSITO', TV.SalidaBase AS 'SALIDA_BASE', TV.LlegadaCarga AS 'LLEGADA_CARGA',		TV.Carga AS 'INICIO_CARGA', TV.SalidaPlanta AS 'SALIDA_PLANTA', TV.LlegadaDescarga AS 'LLEGADA_DESCARGA', TV.InicioDescarga AS 'INICIO_DESCARGA',		TV.SalidaDescarga AS 'SALIDA_DESCARGA', TV.LlegadaBase AS 'LLEGADA_BASE', TV.UsuarioCrea, TV.FechaCrea		FROM ReportesApp_Operacion_Previaje_Registros PR		LEFT JOIN OP_TR_RUTA RT WITH(NOLOCK) ON RT.IDRUTA = PR.IdRuta		LEFT JOIN OP_TR_Conductor C WITH(NOLOCK) ON C.IdConductor = PR.IdConductor		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = PR.idTracto		LEFT JOIN OP_TR_Vehiculo R WITH(NOLOCK) ON R.IdVehiculo = PR.idSemirremolque		LEFT JOIN ReportesApp_Operaciones_Previajes_TiemposViajes TV ON TV.NroTicket = PR.NroTicket		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON PR.TipoProgramacion = O.IdOperacion		WHERE (@Previaje IS NULL OR PR.NroTicket LIKE '%' + @Previaje + '%') AND (PR.FechaProgramacion BETWEEN @FINICIO AND @FFIN) AND		(@Ruta IS NULL OR LTRIM(RTRIM(RT.Descripcion)) LIKE '%' + @Ruta + '%') AND (@Conductor IS NULL OR LTRIM(RTRIM(C.Nombre)) LIKE '%' + @Conductor + '%')		AND ((@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') OR (@Placa IS NULL OR R.NumeroPlaca LIKE '%' + @Placa + '%')) AND (O.Descripcion = @Programacion)
		ORDER BY PR.FechaProgramacion DESC
	END
END

-------------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 17-12-2024
-- Description:	REGISTRAR Y ELIMINAR TIEMPOS VIAJE
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Previajes_RegistrarEliminarTiempoViajes]
@Opcion INT,
@NroTicket INT,
@SalidaBase DATETIME,
@LlegadaPlanta DATETIME,
@IngresoPlanta DATETIME,
@InicioAtencion DATETIME,
@FinAtencion DATETIME,
@EntregaGuia DATETIME,
@SalidaPlanta DATETIME,
@SalidaRuta DATETIME,
@LlegadaCDA DATETIME,
@InicioDescarga DATETIME,
@FinDescarga DATETIME,
@LlegadaCDA2 DATETIME,
@InicioDescarga2 DATETIME,
@FinDescarga2 DATETIME,
@LlegadaBase DATETIME,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- INGRESAR TIEMPO
		IF (@LlegadaPlanta < @SalidaBase AND (DATEPART(HOUR,@LlegadaPlanta) != 0 AND DATEPART(MINUTE,@LlegadaPlanta) != 0 AND DATEPART(SECOND,@LlegadaPlanta) != 0)) BEGIN
			SET @Exito = '-1 = No puede registrar una fecha menor a la(s) ya registrada(s).'
			ROLLBACK
			GOTO Terminar
		END

		IF (@IngresoPlanta < @LlegadaPlanta AND (DATEPART(HOUR,@IngresoPlanta) != 0 AND DATEPART(MINUTE,@IngresoPlanta) != 0 AND DATEPART(SECOND,@IngresoPlanta) != 0)) BEGIN
			SET @Exito = '-1 = No puede registrar una fecha menor a la(s) ya registrada(s).'
			ROLLBACK
			GOTO Terminar
		END

		IF (@InicioAtencion < @IngresoPlanta AND (DATEPART(HOUR,@InicioAtencion) != 0 AND DATEPART(MINUTE,@InicioAtencion) != 0 AND DATEPART(SECOND,@InicioAtencion) != 0)) BEGIN
			SET @Exito = '-1 = No puede registrar una fecha menor a la(s) ya registrada(s).'
			ROLLBACK
			GOTO Terminar
		END

		IF (@FinAtencion < @InicioAtencion AND (DATEPART(HOUR,@FinAtencion) != 0 AND DATEPART(MINUTE,@FinAtencion) != 0 AND DATEPART(SECOND,@FinAtencion) != 0)) BEGIN
			SET @Exito = '-1 = No puede registrar una fecha menor a la(s) ya registrada(s).'
			ROLLBACK
			GOTO Terminar
		END

		IF (@EntregaGuia < @FinAtencion AND (DATEPART(HOUR,@EntregaGuia) != 0 AND DATEPART(MINUTE,@EntregaGuia) != 0 AND DATEPART(SECOND,@EntregaGuia) != 0)) BEGIN
			SET @Exito = '-1 = No puede registrar una fecha menor a la(s) ya registrada(s).'
			ROLLBACK
			GOTO Terminar
		END

		IF (@SalidaPlanta < @EntregaGuia AND (DATEPART(HOUR,@SalidaPlanta) != 0 AND DATEPART(MINUTE,@SalidaPlanta) != 0 AND DATEPART(SECOND,@SalidaPlanta) != 0)) BEGIN
			SET @Exito = '-1 = No puede registrar una fecha menor a la(s) ya registrada(s).'
			ROLLBACK
			GOTO Terminar
		END

		IF (@SalidaRuta < @SalidaPlanta AND (DATEPART(HOUR,@SalidaRuta) != 0 AND DATEPART(MINUTE,@SalidaRuta) != 0 AND DATEPART(SECOND,@SalidaRuta) != 0)) BEGIN
			SET @Exito = '-1 = No puede registrar una fecha menor a la(s) ya registrada(s).'
			ROLLBACK
			GOTO Terminar
		END

		IF (@LlegadaCDA < @SalidaRuta AND (DATEPART(HOUR,@LlegadaCDA) != 0 AND DATEPART(MINUTE,@LlegadaCDA) != 0 AND DATEPART(SECOND,@LlegadaCDA) != 0)) BEGIN
			SET @Exito = '-1 = No puede registrar una fecha menor a la(s) ya registrada(s).'
			ROLLBACK
			GOTO Terminar
		END

		IF (@InicioDescarga < @LlegadaCDA AND (DATEPART(HOUR,@InicioDescarga) != 0 AND DATEPART(MINUTE,@InicioDescarga) != 0 AND DATEPART(SECOND,@InicioDescarga) != 0)) BEGIN
			SET @Exito = '-1 = No puede registrar una fecha menor a la(s) ya registrada(s).'
			ROLLBACK
			GOTO Terminar
		END

		IF (@FinDescarga < @InicioDescarga AND (DATEPART(HOUR,@FinDescarga) != 0 AND DATEPART(MINUTE,@FinDescarga) != 0 AND DATEPART(SECOND,@FinDescarga) != 0)) BEGIN
			SET @Exito = '-1 = No puede registrar una fecha menor a la(s) ya registrada(s).'
			ROLLBACK
			GOTO Terminar
		END

		IF (@InicioDescarga2 < @LlegadaCDA2 AND (DATEPART(HOUR,@InicioDescarga2) != 0 AND DATEPART(MINUTE,@InicioDescarga2) != 0 AND DATEPART(SECOND,@InicioDescarga2) != 0)) BEGIN
			SET @Exito = '-1 = No puede registrar una fecha menor a la(s) ya registrada(s).'
			ROLLBACK
			GOTO Terminar
		END

		IF (@FinDescarga2 < @InicioDescarga2 AND (DATEPART(HOUR,@FinDescarga2) != 0 AND DATEPART(MINUTE,@FinDescarga2) != 0 AND DATEPART(SECOND,@FinDescarga2) != 0)) BEGIN
			SET @Exito = '-1 = No puede registrar una fecha menor a la(s) ya registrada(s).'
			ROLLBACK
			GOTO Terminar
		END

		IF (@LlegadaBase < @FinDescarga AND (DATEPART(HOUR,@LlegadaBase) != 0 AND DATEPART(MINUTE,@LlegadaBase) != 0 AND DATEPART(SECOND,@LlegadaBase) != 0)) BEGIN
			SET @Exito = '-1 = No puede registrar una fecha menor a la(s) ya registrada(s).'
			ROLLBACK
			GOTO Terminar
		END

		IF (DATEPART(MINUTE,@SalidaBase) = 0 AND DATEPART(HOUR,@SalidaBase) = 0 AND DATEPART(SECOND,@SalidaBase) = 0) BEGIN SET @SalidaBase = NULL END
		IF (DATEPART(MINUTE,@LlegadaPlanta) = 0 AND DATEPART(HOUR,@LlegadaPlanta) = 0 AND DATEPART(SECOND,@LlegadaPlanta) = 0) BEGIN SET @LlegadaPlanta = NULL END
		IF (DATEPART(MINUTE,@IngresoPlanta) = 0 AND DATEPART(HOUR,@IngresoPlanta) = 0 AND DATEPART(SECOND,@IngresoPlanta) = 0) BEGIN SET @IngresoPlanta = NULL END
		IF (DATEPART(MINUTE,@InicioAtencion) = 0 AND DATEPART(HOUR,@InicioAtencion) = 0 AND DATEPART(SECOND,@InicioAtencion) = 0) BEGIN SET @InicioAtencion = NULL END
		IF (DATEPART(MINUTE,@FinAtencion) = 0 AND DATEPART(HOUR,@FinAtencion) = 0 AND DATEPART(SECOND,@FinAtencion) = 0) BEGIN SET @FinAtencion = NULL END
		IF (DATEPART(MINUTE,@EntregaGuia) = 0 AND DATEPART(HOUR,@EntregaGuia) = 0 AND DATEPART(SECOND,@EntregaGuia) = 0) BEGIN SET @EntregaGuia = NULL END
		IF (DATEPART(MINUTE,@SalidaPlanta) = 0 AND DATEPART(HOUR,@SalidaPlanta) = 0 AND DATEPART(SECOND,@SalidaPlanta) = 0) BEGIN SET @SalidaPlanta = NULL END
		IF (DATEPART(MINUTE,@SalidaRuta) = 0 AND DATEPART(HOUR,@SalidaRuta) = 0 AND DATEPART(SECOND,@SalidaRuta) = 0) BEGIN SET @SalidaRuta = NULL END
		IF (DATEPART(MINUTE,@LlegadaCDA) = 0 AND DATEPART(HOUR,@LlegadaCDA) = 0 AND DATEPART(SECOND,@LlegadaCDA) = 0) BEGIN SET @LlegadaCDA = NULL END
		IF (DATEPART(MINUTE,@InicioDescarga) = 0 AND DATEPART(HOUR,@InicioDescarga) = 0 AND DATEPART(SECOND,@InicioDescarga) = 0) BEGIN SET @InicioDescarga = NULL END
		IF (DATEPART(MINUTE,@FinDescarga) = 0 AND DATEPART(HOUR,@FinDescarga) = 0 AND DATEPART(SECOND,@FinDescarga) = 0) BEGIN SET @FinDescarga = NULL END
		IF (DATEPART(MINUTE,@LlegadaCDA2) = 0 AND DATEPART(HOUR,@LlegadaCDA2) = 0 AND DATEPART(SECOND,@LlegadaCDA2) = 0) BEGIN SET @LlegadaCDA2 = NULL END
		IF (DATEPART(MINUTE,@InicioDescarga2) = 0 AND DATEPART(HOUR,@InicioDescarga2) = 0 AND DATEPART(SECOND,@InicioDescarga2) = 0) BEGIN SET @InicioDescarga2 = NULL END
		IF (DATEPART(MINUTE,@FinDescarga2) = 0 AND DATEPART(HOUR,@FinDescarga2) = 0 AND DATEPART(SECOND,@FinDescarga2) = 0) BEGIN SET @FinDescarga2 = NULL END
		IF (DATEPART(MINUTE,@LlegadaBase) = 0 AND DATEPART(HOUR,@LlegadaBase) = 0 AND DATEPART(SECOND,@LlegadaBase) = 0) BEGIN SET @LlegadaBase = NULL END

		IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_Previajes_TiemposViajes WHERE NroTicket = @NroTicket)) BEGIN
			UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
			SET SalidaBase = @SalidaBase, LlegadaPlanta = @LlegadaPlanta, IngresoPlanta = @IngresoPlanta, InicioAtencion = @InicioAtencion, FinAtencion = @FinAtencion,
				EntregaGuia = @EntregaGuia, SalidaPlanta = @SalidaPlanta, SalidaRuta = @SalidaRuta, LlegadaCDA = @LlegadaCDA, InicioDescarga = @InicioDescarga,
				FinDescarga = @FinDescarga, LlegadaCDA2 = @LlegadaCDA2, InicioDescarga2 = @InicioDescarga2, FinDescarga2 = @FinDescarga2, LlegadaBase = @LlegadaBase,
				UsuarioCrea = @Usuario, FechaCrea = GETDATE()
			WHERE NroTicket = @NroTicket

			IF (@LlegadaBase IS NULL) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'PENDIENTE'
				WHERE NroTicket = @NroTicket
			END
			ELSE BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'FINALIZADO'
				WHERE NroTicket = @NroTicket
			END

			SET @Exito = '0 = El registro ha sido modificado correctamente.'

			/*
			IF (DATEPART(HOUR,@SalidaBase) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'SALIDA', PorcTransito = 0
				WHERE NroTicket = @NroTicket 

				SET @Exito = '0 = El registro ha sido modificado correctamente.'
			END

			IF (DATEPART(HOUR,@LlegadaPlanta) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'FUERA DE PLANTA', PorcTransito = 0
				WHERE NroTicket = @NroTicket 

				SET @Exito = '0 = El registro ha sido modificado correctamente.'
			END

			IF (DATEPART(HOUR,@IngresoPlanta) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'ESPERA CARGA', PorcTransito = 0
				WHERE NroTicket = @NroTicket 

				SET @Exito = '0 = El registro ha sido modificado correctamente.'
			END

			IF (DATEPART(HOUR,@InicioAtencion) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'CARGANDO', PorcTransito = 0
				WHERE NroTicket = @NroTicket 

				SET @Exito = '0 = El registro ha sido modificado correctamente.'
			END

			IF (DATEPART(HOUR,@FinAtencion) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'CARGANDO', PorcTransito = 0
				WHERE NroTicket = @NroTicket 

				SET @Exito = '0 = El registro ha sido modificado correctamente.'
			END

			IF (DATEPART(HOUR,@EntregaGuia) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'CARGANDO', PorcTransito = 0
				WHERE NroTicket = @NroTicket 

				SET @Exito = '0 = El registro ha sido modificado correctamente.'
			END

			IF (DATEPART(HOUR,@SalidaPlanta) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'CARGADO', PorcTransito = 0
				WHERE NroTicket = @NroTicket 

				SET @Exito = '0 = El registro ha sido modificado correctamente.'
			END

			IF (DATEPART(HOUR,@SalidaRuta) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'EN TRÁNSITO', PorcTransito = @Avance
				WHERE NroTicket = @NroTicket 

				SET @Exito = '0 = El registro ha sido modificado correctamente.'
			END

			IF (DATEPART(HOUR,@LlegadaCDA) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'ESPERA DESCARGA', PorcTransito = 100
				WHERE NroTicket = @NroTicket 

				SET @Exito = '0 = El registro ha sido modificado correctamente.'
			END

			IF (DATEPART(HOUR,@InicioDescarga) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'DESCARGANDO', PorcTransito = 100
				WHERE NroTicket = @NroTicket 

				SET @Exito = '0 = El registro ha sido modificado correctamente.'
			END

			IF (DATEPART(HOUR,@FinDescarga) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'RETORNO', PorcTransito = 100
				WHERE NroTicket = @NroTicket 

				SET @Exito = '0 = El registro ha sido modificado correctamente.'
			END

			IF (DATEPART(HOUR,@LlegadaCDA2) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'ESPERA DESCARGA', PorcTransito = 100
				WHERE NroTicket = @NroTicket 

				SET @Exito = '0 = El registro ha sido modificado correctamente.'
			END

			IF (DATEPART(HOUR,@InicioDescarga2) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'DESCARGANDO', PorcTransito = 100
				WHERE NroTicket = @NroTicket 

				SET @Exito = '0 = El registro ha sido modificado correctamente.'
			END

			IF (DATEPART(HOUR,@FinDescarga2) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'RETORNO', PorcTransito = 100
				WHERE NroTicket = @NroTicket 

				SET @Exito = '0 = El registro ha sido modificado correctamente.'
			END

			IF (DATEPART(HOUR,@LlegadaBase) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'FINALIZADO', PorcTransito = 100
				WHERE NroTicket = @NroTicket 

				SET @Exito = '0 = El registro ha sido modificado correctamente.'
			END
			*/
		END
		ELSE BEGIN
			INSERT INTO ReportesApp_Operaciones_Previajes_TiemposViajes (NroTicket, SalidaBase, LlegadaPlanta, IngresoPlanta, InicioAtencion, FinAtencion, EntregaGuia,
			SalidaPlanta, SalidaRuta, LlegadaCDA, InicioDescarga, FinDescarga, LlegadaCDA2, InicioDescarga2, FinDescarga2, LlegadaBase, UsuarioCrea, FechaCrea)
			VALUES(@NroTicket, @SalidaBase, @LlegadaPlanta, @IngresoPlanta, @InicioAtencion, @FinAtencion, @EntregaGuia, @SalidaPlanta, @SalidaRuta, @LlegadaCDA,
			@InicioDescarga, @FinDescarga, @LlegadaCDA2, @InicioDescarga2, @FinDescarga2, @LlegadaBase, @Usuario, GETDATE())

			IF (@LlegadaBase IS NULL) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'PENDIENTE'
				WHERE NroTicket = @NroTicket
			END
			ELSE BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'FINALIZADO'
				WHERE NroTicket = @NroTicket
			END

			/*
			IF (DATEPART(HOUR,@SalidaBase) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'SALIDA', PorcTransito = 0
				WHERE NroTicket = @NroTicket 
			END

			IF (DATEPART(HOUR,@LlegadaPlanta) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'FUERA DE PLANTA', PorcTransito = 0
				WHERE NroTicket = @NroTicket 
			END

			IF (DATEPART(HOUR,@IngresoPlanta) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'ESPERA CARGA', PorcTransito = 0
				WHERE NroTicket = @NroTicket 
			END

			IF (DATEPART(HOUR,@InicioAtencion) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'CARGANDO', PorcTransito = 0
				WHERE NroTicket = @NroTicket 
			END

			IF (DATEPART(HOUR,@FinAtencion) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'CARGANDO', PorcTransito = 0
				WHERE NroTicket = @NroTicket 
			END

			IF (DATEPART(HOUR,@EntregaGuia) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'CARGANDO', PorcTransito = 0
				WHERE NroTicket = @NroTicket 
			END

			IF (DATEPART(HOUR,@SalidaPlanta) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'CARGADO', PorcTransito = 0
				WHERE NroTicket = @NroTicket 
			END

			IF (DATEPART(HOUR,@SalidaRuta) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'EN TRÁNSITO', PorcTransito = @Avance
				WHERE NroTicket = @NroTicket 
			END

			IF (DATEPART(HOUR,@LlegadaCDA) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'ESPERA DESCARGA', PorcTransito = 100
				WHERE NroTicket = @NroTicket 
			END

			IF (DATEPART(HOUR,@InicioDescarga) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'DESCARGANDO', PorcTransito = 100
				WHERE NroTicket = @NroTicket 
			END

			IF (DATEPART(HOUR,@FinDescarga) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'RETORNO', PorcTransito = 100
				WHERE NroTicket = @NroTicket 
			END

			IF (DATEPART(HOUR,@LlegadaCDA2) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'ESPERA DESCARGA', PorcTransito = 100
				WHERE NroTicket = @NroTicket 
			END

			IF (DATEPART(HOUR,@InicioDescarga2) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'DESCARGANDO', PorcTransito = 100
				WHERE NroTicket = @NroTicket 
			END

			IF (DATEPART(HOUR,@FinDescarga2) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'RETORNO', PorcTransito = 100
				WHERE NroTicket = @NroTicket 
			END

			IF (DATEPART(HOUR,@LlegadaBase) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'FINALIZADO', PorcTransito = 100
				WHERE NroTicket = @NroTicket 
			END
			*/

			SET @Exito = '0 = El registro ha sido creado correctamente.'
		END
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR TIEMPO
		DELETE FROM ReportesApp_Operaciones_Previajes_TiemposViajes
		WHERE NroTicket = @NroTicket

		SET @Exito = '0 = El registro ha sido eliminado correctamente.'
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

------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 11-04-2025
-- Description:	REGISTRAR Y ELIMINAR TIEMPOS LIMAGAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Previajes_RegistrarEliminarTiempoViajesLimagas]
@Opcion INT,
@NroTicket INT,
@SalidaBase DATETIME,
@LlegadaCarga DATETIME,
@Carga DATETIME,
@SalidaPlanta DATETIME,
@LlegadaDescarga DATETIME,
@InicioDescarga DATETIME,
@SalidaDescarga DATETIME,
@LlegadaBase DATETIME,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- INGRESAR TIEMPO
		IF (@LlegadaCarga < @SalidaBase AND (DATEPART(HOUR,@LlegadaCarga) != 0 AND DATEPART(MINUTE,@LlegadaCarga) != 0 AND DATEPART(SECOND,@LlegadaCarga) != 0)) BEGIN
			SET @Exito = '-1 = No puede registrar una fecha menor a la(s) ya registrada(s).'
			ROLLBACK
			GOTO Terminar
		END

		IF (@Carga < @LlegadaCarga AND (DATEPART(HOUR,@Carga) != 0 AND DATEPART(MINUTE,@Carga) != 0 AND DATEPART(SECOND,@Carga) != 0)) BEGIN
			SET @Exito = '-1 = No puede registrar una fecha menor a la(s) ya registrada(s).'
			ROLLBACK
			GOTO Terminar
		END

		IF (@SalidaPlanta < @Carga AND (DATEPART(HOUR,@SalidaPlanta) != 0 AND DATEPART(MINUTE,@SalidaPlanta) != 0 AND DATEPART(SECOND,@SalidaPlanta) != 0)) BEGIN
			SET @Exito = '-1 = No puede registrar una fecha menor a la(s) ya registrada(s).'
			ROLLBACK
			GOTO Terminar
		END

		IF (@LlegadaDescarga < @SalidaPlanta AND (DATEPART(HOUR,@LlegadaDescarga) != 0 AND DATEPART(MINUTE,@LlegadaDescarga) != 0 AND DATEPART(SECOND,@LlegadaDescarga) != 0)) BEGIN
			SET @Exito = '-1 = No puede registrar una fecha menor a la(s) ya registrada(s).'
			ROLLBACK
			GOTO Terminar
		END

		IF (@InicioDescarga < @LlegadaDescarga AND (DATEPART(HOUR,@InicioDescarga) != 0 AND DATEPART(MINUTE,@InicioDescarga) != 0 AND DATEPART(SECOND,@InicioDescarga) != 0)) BEGIN
			SET @Exito = '-1 = No puede registrar una fecha menor a la(s) ya registrada(s).'
			ROLLBACK
			GOTO Terminar
		END

		IF (@SalidaDescarga < @InicioDescarga AND (DATEPART(HOUR,@SalidaDescarga) != 0 AND DATEPART(MINUTE,@SalidaDescarga) != 0 AND DATEPART(SECOND,@SalidaDescarga) != 0)) BEGIN
			SET @Exito = '-1 = No puede registrar una fecha menor a la(s) ya registrada(s).'
			ROLLBACK
			GOTO Terminar
		END

		IF (@LlegadaBase < @SalidaDescarga AND (DATEPART(HOUR,@LlegadaBase) != 0 AND DATEPART(MINUTE,@LlegadaBase) != 0 AND DATEPART(SECOND,@LlegadaBase) != 0)) BEGIN
			SET @Exito = '-1 = No puede registrar una fecha menor a la(s) ya registrada(s).'
			ROLLBACK
			GOTO Terminar
		END

		IF (DATEPART(MINUTE,@SalidaBase) = 0 AND DATEPART(HOUR,@SalidaBase) = 0 AND DATEPART(SECOND,@SalidaBase) = 0) BEGIN SET @SalidaBase = NULL END
		IF (DATEPART(MINUTE,@LlegadaCarga) = 0 AND DATEPART(HOUR,@LlegadaCarga) = 0 AND DATEPART(SECOND,@LlegadaCarga) = 0) BEGIN SET @LlegadaCarga = NULL END
		IF (DATEPART(MINUTE,@Carga) = 0 AND DATEPART(HOUR,@Carga) = 0 AND DATEPART(SECOND,@Carga) = 0) BEGIN SET @Carga = NULL END
		IF (DATEPART(MINUTE,@SalidaPlanta) = 0 AND DATEPART(HOUR,@SalidaPlanta) = 0 AND DATEPART(SECOND,@SalidaPlanta) = 0) BEGIN SET @SalidaPlanta = NULL END
		IF (DATEPART(MINUTE,@LlegadaDescarga) = 0 AND DATEPART(HOUR,@LlegadaDescarga) = 0 AND DATEPART(SECOND,@LlegadaDescarga) = 0) BEGIN SET @LlegadaDescarga = NULL END
		IF (DATEPART(MINUTE,@InicioDescarga) = 0 AND DATEPART(HOUR,@InicioDescarga) = 0 AND DATEPART(SECOND,@InicioDescarga) = 0) BEGIN SET @InicioDescarga = NULL END
		IF (DATEPART(MINUTE,@SalidaDescarga) = 0 AND DATEPART(HOUR,@SalidaDescarga) = 0 AND DATEPART(SECOND,@SalidaDescarga) = 0) BEGIN SET @SalidaDescarga = NULL END
		IF (DATEPART(MINUTE,@LlegadaBase) = 0 AND DATEPART(HOUR,@LlegadaBase) = 0 AND DATEPART(SECOND,@LlegadaBase) = 0) BEGIN SET @LlegadaBase = NULL END

		IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_Previajes_TiemposViajes WHERE NroTicket = @NroTicket)) BEGIN
			UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
			SET SalidaBase = @SalidaBase, LlegadaCarga = @LlegadaCarga, Carga = @Carga, SalidaPlanta = @SalidaPlanta, LlegadaDescarga = @LlegadaDescarga,
			InicioDescarga = @InicioDescarga, SalidaDescarga = @SalidaDescarga, LlegadaBase = @LlegadaBase, UsuarioCrea = @Usuario, FechaCrea = GETDATE()
			WHERE NroTicket = @NroTicket

			IF (@LlegadaBase IS NULL) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'PENDIENTE'
				WHERE NroTicket = @NroTicket
			END
			ELSE BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'FINALIZADO'
				WHERE NroTicket = @NroTicket
			END

			SET @Exito = '0 = El registro ha sido modificado correctamente.'

			/*
			IF (DATEPART(HOUR,@SalidaBase) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'SALIDA'
				WHERE NroTicket = @NroTicket 

				SET @Exito = '0 = El registro ha sido modificado correctamente.'
			END
			
			IF (DATEPART(HOUR,@LlegadaCarga) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'CARGANDO'
				WHERE NroTicket = @NroTicket 

				SET @Exito = '0 = El registro ha sido modificado correctamente.'
			END

			IF (DATEPART(HOUR,@Carga) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'CARGADO'
				WHERE NroTicket = @NroTicket 

				SET @Exito = '0 = El registro ha sido modificado correctamente.'
			END

			IF (DATEPART(HOUR,@SalidaPlanta) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'EN TRÁNSITO'
				WHERE NroTicket = @NroTicket 

				SET @Exito = '0 = El registro ha sido modificado correctamente.'
			END

			IF (DATEPART(HOUR,@LlegadaDescarga) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'ESPERA DESCARGA'
				WHERE NroTicket = @NroTicket 

				SET @Exito = '0 = El registro ha sido modificado correctamente.'
			END

			IF (DATEPART(HOUR,@InicioDescarga) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'DESCARGANDO'
				WHERE NroTicket = @NroTicket 

				SET @Exito = '0 = El registro ha sido modificado correctamente.'
			END

			IF (DATEPART(HOUR,@SalidaDescarga) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'RETORNO'
				WHERE NroTicket = @NroTicket 

				SET @Exito = '0 = El registro ha sido modificado correctamente.'
			END

			IF (DATEPART(HOUR,@LlegadaBase) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'FINALIZADO'
				WHERE NroTicket = @NroTicket 

				SET @Exito = '0 = El registro ha sido modificado correctamente.'
			END
			*/
		END
		ELSE BEGIN
			INSERT INTO ReportesApp_Operaciones_Previajes_TiemposViajes (NroTicket, SalidaBase, LlegadaCarga, Carga, SalidaPlanta, LlegadaDescarga,
			InicioDescarga, SalidaDescarga, LlegadaBase, UsuarioCrea, FechaCrea)
			VALUES(@NroTicket, @SalidaBase, @LlegadaCarga, @Carga, @SalidaPlanta, @LlegadaDescarga, @InicioDescarga, @SalidaDescarga, @LlegadaBase,
			@Usuario, GETDATE())

			IF (@LlegadaBase IS NULL) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'PENDIENTE'
				WHERE NroTicket = @NroTicket
			END
			ELSE BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'FINALIZADO'
				WHERE NroTicket = @NroTicket
			END

			/*
			IF (DATEPART(HOUR,@SalidaBase) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'SALIDA'
				WHERE NroTicket = @NroTicket 
			END
			
			IF (DATEPART(HOUR,@LlegadaCarga) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'CARGANDO'
				WHERE NroTicket = @NroTicket 
			END

			IF (DATEPART(HOUR,@Carga) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'CARGADO'
				WHERE NroTicket = @NroTicket 
			END

			IF (DATEPART(HOUR,@SalidaPlanta) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'EN TRÁNSITO'
				WHERE NroTicket = @NroTicket 
			END

			IF (DATEPART(HOUR,@LlegadaDescarga) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'ESPERA DESCARGA'
				WHERE NroTicket = @NroTicket 
			END

			IF (DATEPART(HOUR,@InicioDescarga) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'DESCARGANDO'
				WHERE NroTicket = @NroTicket 
			END

			IF (DATEPART(HOUR,@SalidaDescarga) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'RETORNO'
				WHERE NroTicket = @NroTicket 
			END

			IF (DATEPART(HOUR,@LlegadaBase) != 0) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'FINALIZADO'
				WHERE NroTicket = @NroTicket 
			END
			*/

			SET @Exito = '0 = El registro ha sido creado correctamente.'
		END
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR TIEMPO
		DELETE FROM ReportesApp_Operaciones_Previajes_TiemposViajes
		WHERE NroTicket = @NroTicket

		SET @Exito = '0 = El registro ha sido eliminado correctamente.'
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

------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 27-08-2025
-- Description:	IMPORTAR TIEMPOS DE VIAJE
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Previajes_ImportarTiempoViajes]
@Opcion INT,
@xmlDetalle VARCHAR(MAX),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @idoc INT
DECLARE @TEMP_TV TABLE(Nro INT, NroTicket VARCHAR(100), RutaViaje VARCHAR(250), SalidaBase VARCHAR(100), LlegadaPlanta VARCHAR(100), IngresoPlanta VARCHAR(100),
					   InicioAtencion VARCHAR(100), FinAtencion VARCHAR(100), EntregaGuia VARCHAR(100), SalidaPlanta VARCHAR(100), SalidaRuta VARCHAR(100),
					   LlegadaCDA VARCHAR(100), InicioDescarga VARCHAR(100), FinDescarga VARCHAR(100), LlegadaCDA2 VARCHAR(100), InicioDescarga2 VARCHAR(100),
					   FinDescarga2 VARCHAR(100), LlegadaBase VARCHAR(100))
DECLARE @TEMP_TV2 TABLE(Nro INT, NroTicket VARCHAR(100), RutaViaje VARCHAR(250), SalidaBase DATETIME, LlegadaPlanta DATETIME, IngresoPlanta DATETIME,
						InicioAtencion DATETIME, FinAtencion DATETIME, EntregaGuia DATETIME, SalidaPlanta DATETIME, SalidaRuta DATETIME, LlegadaCDA DATETIME,
						InicioDescarga DATETIME, FinDescarga DATETIME, LlegadaCDA2 DATETIME, InicioDescarga2 DATETIME, FinDescarga2 DATETIME, LlegadaBase DATETIME)

DECLARE @TEMP_TV_L TABLE(Nro INT, NroTicket VARCHAR(100), RutaViaje VARCHAR(250), SalidaBase VARCHAR(100), LlegadaCarga VARCHAR(100), Carga VARCHAR(100),
						 SalidaPlanta VARCHAR(100), LlegadaDescarga VARCHAR(100), InicioDescarga VARCHAR(100), SalidaDescarga VARCHAR(100), LlegadaBase VARCHAR(100))
DECLARE @TEMP_TV2_L TABLE(Nro INT, NroTicket VARCHAR(100), RutaViaje VARCHAR(250), SalidaBase DATETIME, LlegadaCarga DATETIME, Carga DATETIME, SalidaPlanta DATETIME,
						  LlegadaDescarga DATETIME, InicioDescarga DATETIME, SalidaDescarga DATETIME, LlegadaBase DATETIME)

IF(@xmlDetalle IS NOT NULL) BEGIN
	IF (@Opcion = 1) BEGIN		-- LINDLEY
		EXEC sp_xml_preparedocument @idoc OUTPUT, @xmlDetalle
		INSERT INTO @TEMP_TV(Nro,NroTicket,RutaViaje,SalidaBase,LlegadaPlanta,IngresoPlanta,InicioAtencion,FinAtencion,EntregaGuia,SalidaPlanta,SalidaRuta,
		LlegadaCDA,InicioDescarga,FinDescarga,LlegadaCDA2,InicioDescarga2,FinDescarga2,LlegadaBase)
		SELECT ROW_NUMBER() OVER(ORDER BY PREVIAJE ASC), * FROM OPENXML(@idoc,'/r/d',1)
		WITH (PREVIAJE INT, RUTA_VIAJE VARCHAR(250), SALIDA_BASE VARCHAR(100), LLEGADA_PLANTA VARCHAR(100), INGRESO_PLANTA VARCHAR(100), INICIO_ATENCION VARCHAR(100),
		FIN_ATENCION VARCHAR(100), ENTREGA_GUIA VARCHAR(100), SALIDA_PLANTA VARCHAR(100), SALIDA_RUTA VARCHAR(100), LLEGADA_CDA VARCHAR(100), INICIO_DESCARGA VARCHAR(100),
		FIN_DESCARGA VARCHAR(100), LLEGADA_CDA_2 VARCHAR(100), INICIO_DESCARGA_2 VARCHAR(100), FIN_DESCARGA_2 VARCHAR(100), LLEGADA_BASE VARCHAR(100));
		EXEC sp_xml_removedocument @idoc;

		INSERT INTO @TEMP_TV2(Nro,NroTicket,RutaViaje,SalidaBase,LlegadaPlanta,IngresoPlanta,InicioAtencion,FinAtencion,EntregaGuia,SalidaPlanta,SalidaRuta,
		LlegadaCDA,InicioDescarga,FinDescarga,LlegadaCDA2,InicioDescarga2,FinDescarga2,LlegadaBase)
		SELECT Nro,ISNULL(NroTicket,'-1'),RutaViaje,
		ISNULL(TRY_CONVERT(DATETIME2(0), REPLACE(LEFT(SalidaBase,19),'T',' '), 120), SalidaBase),
		ISNULL(TRY_CONVERT(DATETIME2(0), REPLACE(LEFT(LlegadaPlanta,19),'T',' '), 120), LlegadaPlanta),
		ISNULL(TRY_CONVERT(DATETIME2(0), REPLACE(LEFT(IngresoPlanta,19),'T',' '), 120), IngresoPlanta),
		ISNULL(TRY_CONVERT(DATETIME2(0), REPLACE(LEFT(InicioAtencion,19),'T',' '), 120), InicioAtencion),
		ISNULL(TRY_CONVERT(DATETIME2(0), REPLACE(LEFT(FinAtencion,19),'T',' '), 120), FinAtencion),
		ISNULL(TRY_CONVERT(DATETIME2(0), REPLACE(LEFT(EntregaGuia,19),'T',' '), 120), EntregaGuia),
		ISNULL(TRY_CONVERT(DATETIME2(0), REPLACE(LEFT(SalidaPlanta,19),'T',' '), 120), SalidaPlanta),
		ISNULL(TRY_CONVERT(DATETIME2(0), REPLACE(LEFT(SalidaRuta,19),'T',' '), 120), SalidaRuta),
		ISNULL(TRY_CONVERT(DATETIME2(0), REPLACE(LEFT(LlegadaCDA,19),'T',' '), 120), LlegadaCDA),
		ISNULL(TRY_CONVERT(DATETIME2(0), REPLACE(LEFT(InicioDescarga,19),'T',' '), 120), InicioDescarga),
		ISNULL(TRY_CONVERT(DATETIME2(0), REPLACE(LEFT(FinDescarga,19),'T',' '), 120), FinDescarga),
		ISNULL(TRY_CONVERT(DATETIME2(0), REPLACE(LEFT(LlegadaCDA2,19),'T',' '), 120), LlegadaCDA2),
		ISNULL(TRY_CONVERT(DATETIME2(0), REPLACE(LEFT(InicioDescarga2,19),'T',' '), 120), InicioDescarga2),
		ISNULL(TRY_CONVERT(DATETIME2(0), REPLACE(LEFT(FinDescarga2,19),'T',' '), 120), FinDescarga2),
		ISNULL(TRY_CONVERT(DATETIME2(0), REPLACE(LEFT(LlegadaBase,19),'T',' '), 120), LlegadaBase)
		FROM @TEMP_TV
	END

	IF (@Opcion = 2) BEGIN		-- LIMAGAS - VOLCAN
		EXEC sp_xml_preparedocument @idoc OUTPUT, @xmlDetalle
		INSERT INTO @TEMP_TV_L(Nro,NroTicket,RutaViaje,SalidaBase,LlegadaCarga,Carga,SalidaPlanta,LlegadaDescarga,InicioDescarga,SalidaDescarga,LlegadaBase)
		SELECT ROW_NUMBER() OVER(ORDER BY PREVIAJE ASC), * FROM OPENXML(@idoc,'/r/d',1)
		WITH (PREVIAJE INT, RUTA_VIAJE VARCHAR(250), SALIDA_BASE VARCHAR(100), LLEGADA_CARGA VARCHAR(100), INICIO_CARGA VARCHAR(100), SALIDA_PLANTA VARCHAR(100),
		LLEGADA_DESCARGA VARCHAR(100), INICIO_DESCARGA VARCHAR(100), SALIDA_DESCARGA VARCHAR(100), LLEGADA_BASE VARCHAR(100));
		EXEC sp_xml_removedocument @idoc;

		INSERT INTO @TEMP_TV2_L(Nro,NroTicket,RutaViaje,SalidaBase,LlegadaCarga,Carga,SalidaPlanta,LlegadaDescarga,InicioDescarga,SalidaDescarga,LlegadaBase)
		SELECT Nro,ISNULL(NroTicket,'-1'),RutaViaje,
		ISNULL(TRY_CONVERT(DATETIME2(0), REPLACE(LEFT(SalidaBase,19),'T',' '), 120), SalidaBase),
		ISNULL(TRY_CONVERT(DATETIME2(0), REPLACE(LEFT(LlegadaCarga,19),'T',' '), 120), LlegadaCarga),
		ISNULL(TRY_CONVERT(DATETIME2(0), REPLACE(LEFT(Carga,19),'T',' '), 120), Carga),
		ISNULL(TRY_CONVERT(DATETIME2(0), REPLACE(LEFT(SalidaPlanta,19),'T',' '), 120), SalidaPlanta),
		ISNULL(TRY_CONVERT(DATETIME2(0), REPLACE(LEFT(LlegadaDescarga,19),'T',' '), 120), LlegadaDescarga),
		ISNULL(TRY_CONVERT(DATETIME2(0), REPLACE(LEFT(InicioDescarga,19),'T',' '), 120), InicioDescarga),
		ISNULL(TRY_CONVERT(DATETIME2(0), REPLACE(LEFT(SalidaDescarga,19),'T',' '), 120), SalidaDescarga),
		ISNULL(TRY_CONVERT(DATETIME2(0), REPLACE(LEFT(LlegadaBase,19),'T',' '), 120), LlegadaBase)
		FROM @TEMP_TV_L
	END
END

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- LINDLEY
		DECLARE @Contador INT = 1

		WHILE (@Contador <= (SELECT COUNT(Nro) FROM @TEMP_TV2)) BEGIN
			DECLARE @NroTicket INT = (SELECT CONVERT(INT,NroTicket) FROM @TEMP_TV2 WHERE Nro = @Contador)
			DECLARE @RutaViaje VARCHAR(250) = (SELECT RutaViaje FROM @TEMP_TV2 WHERE Nro = @Contador)
			DECLARE @SalidaBase DATETIME = (SELECT SalidaBase FROM @TEMP_TV2 WHERE Nro = @Contador)
			DECLARE @LlegadaPlanta DATETIME = (SELECT LlegadaPlanta FROM @TEMP_TV2 WHERE Nro = @Contador)
			DECLARE @IngresoPlanta DATETIME = (SELECT IngresoPlanta FROM @TEMP_TV2 WHERE Nro = @Contador)
			DECLARE @InicioAtencion DATETIME = (SELECT InicioAtencion FROM @TEMP_TV2 WHERE Nro = @Contador)
			DECLARE @FinAtencion DATETIME = (SELECT FinAtencion FROM @TEMP_TV2 WHERE Nro = @Contador)
			DECLARE @EntregaGuia DATETIME = (SELECT EntregaGuia FROM @TEMP_TV2 WHERE Nro = @Contador)
			DECLARE @SalidaPlanta DATETIME = (SELECT SalidaPlanta FROM @TEMP_TV2 WHERE Nro = @Contador)
			DECLARE @SalidaRuta DATETIME = (SELECT SalidaRuta FROM @TEMP_TV2 WHERE Nro = @Contador)
			DECLARE @LlegadaCDA DATETIME = (SELECT LlegadaCDA FROM @TEMP_TV2 WHERE Nro = @Contador)
			DECLARE @InicioDescarga DATETIME = (SELECT InicioDescarga FROM @TEMP_TV2 WHERE Nro = @Contador)
			DECLARE @FinDescarga DATETIME = (SELECT FinDescarga FROM @TEMP_TV2 WHERE Nro = @Contador)
			DECLARE @LlegadaCDA2 DATETIME = (SELECT LlegadaCDA2 FROM @TEMP_TV2 WHERE Nro = @Contador)
			DECLARE @InicioDescarga2 DATETIME = (SELECT InicioDescarga2 FROM @TEMP_TV2 WHERE Nro = @Contador)
			DECLARE @FinDescarga2 DATETIME = (SELECT FinDescarga2 FROM @TEMP_TV2 WHERE Nro = @Contador)
			DECLARE @LlegadaBase DATETIME = (SELECT LlegadaBase FROM @TEMP_TV2 WHERE Nro = @Contador)

			IF (NOT EXISTS(SELECT * FROM ReportesApp_Operacion_Previaje_Registros WHERE NroTicket = @NroTicket)) BEGIN
				SET @NroTicket = (SELECT NroTicket FROM ReportesApp_Operacion_Previaje_Registros WHERE CodViaje = CONVERT(VARCHAR,@NroTicket))
			END

			IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_Previajes_TiemposViajes WHERE NroTicket = @NroTicket)) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET RutaViaje = @RutaViaje, SalidaBase = @SalidaBase, LlegadaPlanta = @LlegadaPlanta, IngresoPlanta = @IngresoPlanta, InicioAtencion = @InicioAtencion,
					FinAtencion = @FinAtencion, EntregaGuia = @EntregaGuia, SalidaPlanta = @SalidaPlanta, SalidaRuta = @SalidaRuta, LlegadaCDA = @LlegadaCDA,
					InicioDescarga = @InicioDescarga, FinDescarga = @FinDescarga, LlegadaCDA2 = @LlegadaCDA2, InicioDescarga2 = @InicioDescarga2, FinDescarga2 = @FinDescarga2,
					LlegadaBase = @LlegadaBase, UsuarioCrea = @Usuario, FechaCrea = GETDATE()
				WHERE NroTicket = @NroTicket
			END
			ELSE BEGIN
				INSERT INTO ReportesApp_Operaciones_Previajes_TiemposViajes (NroTicket, RutaViaje, SalidaBase, LlegadaPlanta, IngresoPlanta, InicioAtencion, FinAtencion,
				EntregaGuia, SalidaPlanta, SalidaRuta, LlegadaCDA, InicioDescarga, FinDescarga, LlegadaCDA2, InicioDescarga2, FinDescarga2, LlegadaBase, UsuarioCrea, FechaCrea)
				VALUES(@NroTicket, @RutaViaje, @SalidaBase, @LlegadaPlanta, @IngresoPlanta, @InicioAtencion, @FinAtencion, @EntregaGuia, @SalidaPlanta, @SalidaRuta,
				@LlegadaCDA, @InicioDescarga, @FinDescarga, @LlegadaCDA2, @InicioDescarga2, @FinDescarga2, @LlegadaBase, @Usuario, GETDATE())
			END

			IF (@LlegadaBase IS NOT NULL) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'FINALIZADO'
				WHERE NroTicket = @NroTicket
			END
			ELSE BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'PENDIENTE'
				WHERE NroTicket = @NroTicket
			END

			SET @Contador = @Contador + 1
		END
	END

	IF (@Opcion = 2) BEGIN		-- LIMAGAS - VOLCAN
		DECLARE @Contador2 INT = 1

		WHILE (@Contador2 <= (SELECT COUNT(Nro) FROM @TEMP_TV2_L)) BEGIN
			DECLARE @NroTicket2 INT = (SELECT NroTicket FROM @TEMP_TV2_L WHERE Nro = @Contador2)
			DECLARE @RutaViaje2 VARCHAR(250) = (SELECT RutaViaje FROM @TEMP_TV2_L WHERE Nro = @Contador2)
			DECLARE @SalidaBase2 DATETIME = (SELECT SalidaBase FROM @TEMP_TV2_L WHERE Nro = @Contador2)
			DECLARE @LlegadaCarga2 DATETIME = (SELECT LlegadaCarga FROM @TEMP_TV2_L WHERE Nro = @Contador2)
			DECLARE @Carga2 DATETIME = (SELECT Carga FROM @TEMP_TV2_L WHERE Nro = @Contador2)
			DECLARE @SalidaPlanta2 DATETIME = (SELECT Carga FROM @TEMP_TV2_L WHERE Nro = @Contador2)
			DECLARE @LlegadaDescarga2 DATETIME = (SELECT LlegadaDescarga FROM @TEMP_TV2_L WHERE Nro = @Contador2)
			DECLARE @InicioDescargaL2 DATETIME = (SELECT InicioDescarga FROM @TEMP_TV2_L WHERE Nro = @Contador2)
			DECLARE @SalidaDescarga2 DATETIME = (SELECT SalidaDescarga FROM @TEMP_TV2_L WHERE Nro = @Contador2)
			DECLARE @LlegadaBase2 DATETIME = (SELECT LlegadaBase FROM @TEMP_TV2_L WHERE Nro = @Contador2)

			IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_Previajes_TiemposViajes WHERE NroTicket = @NroTicket)) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET RutaViaje = @RutaViaje2, SalidaBase = @SalidaBase2, LlegadaCarga = @LlegadaCarga2, Carga = @Carga2, SalidaPlanta = @SalidaPlanta2,
				LlegadaDescarga = @LlegadaDescarga2, InicioDescarga = @InicioDescargaL2, SalidaDescarga = @SalidaDescarga2, LlegadaBase = @LlegadaBase2,
				UsuarioCrea = @Usuario, FechaCrea = GETDATE()
				WHERE NroTicket = @NroTicket2
			END
			ELSE BEGIN
				INSERT INTO ReportesApp_Operaciones_Previajes_TiemposViajes (NroTicket, RutaViaje, SalidaBase, LlegadaCarga, Carga, SalidaPlanta, LlegadaDescarga,
				InicioDescarga, SalidaDescarga, LlegadaBase, UsuarioCrea, FechaCrea)
				VALUES(@NroTicket2, @RutaViaje2, @SalidaBase2, @LlegadaCarga2, @Carga2, @SalidaPlanta2, @LlegadaDescarga2, @InicioDescargaL2, @SalidaDescarga2, @LlegadaBase2,
				@Usuario, GETDATE())
			END

			IF (@LlegadaBase2 IS NOT NULL) BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'FINALIZADO'
				WHERE NroTicket = @NroTicket2
			END
			ELSE BEGIN
				UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
				SET Estado = 'PENDIENTE'
				WHERE NroTicket = @NroTicket2
			END

			SET @Contador2 = @Contador2 + 1
		END
	END
	
	SET @Exito = '0 = Tiempos de Viajes Importados Correctamente.'
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
SELECT @Exito exito

------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 11-09-2025
-- Description:	INSERTAR DATOS DE VIAJE
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Previajes_ModificarTiempoViajes]
@NroTicket INT,
@RutaViaje VARCHAR(250),
@EstadoViaje VARCHAR(20),
@Ubicacion VARCHAR(250),
@PorcTransito DECIMAL(10,2),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_Previajes_TiemposViajes WHERE NroTicket = @NroTicket)) BEGIN
		UPDATE ReportesApp_Operaciones_Previajes_TiemposViajes
		SET RutaViaje = @RutaViaje, EstadoViaje = @EstadoViaje, Ubicacion = @Ubicacion, PorcTransito = @PorcTransito, UsuarioCrea = @Usuario, FechaCrea = GETDATE()
		WHERE NroTicket = @NroTicket
	END
	ELSE BEGIN
		INSERT INTO ReportesApp_Operaciones_Previajes_TiemposViajes (NroTicket, RutaViaje, EstadoViaje, Ubicacion, PorcTransito, UsuarioCrea, FechaCrea)
		VALUES(@NroTicket, @RutaViaje, @EstadoViaje, @Ubicacion, @PorcTransito, @Usuario, GETDATE())
	END

	SET @Exito = '0 = Tiempos de Viajes Importados Correctamente.'
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

------------------------------------------------------------------------------------
------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 26-12-2024
-- Description:	REGISTRAR Y ELIMINAR TIEMPOS PERNOCTE
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Previajes_RegistrarEliminarPernocte]
@Opcion INT,
@NroTicket INT,
@idPernocte INT,
@FechaInicio DATETIME,
@FechaFin DATETIME,
@Ubicacion VARCHAR(250),
@TipoPernocte VARCHAR(20),
@Usuario VARCHAR(20)
AS
DECLARE @Contador INT 
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- INGRESAR TIEMPO
		SET @Contador = (SELECT MAX(idPernocte) FROM ReportesApp_Operaciones_Previajes_Pernoctes WHERE NroTicket = @NroTicket)
		SET @Contador = ISNULL(@Contador,0) + 1 

		INSERT INTO ReportesApp_Operaciones_Previajes_Pernoctes (NroTicket, idPernocte, InicioPernocte, FinPernocte, Ubicacion, TipoPernocte, UsuarioCrea, FechaCrea)
		VALUES(@NroTicket, @Contador, @FechaInicio, @FechaFin, @Ubicacion, @TipoPernocte, @Usuario, GETDATE())

		SET @Exito = '0 = El pernocte ha sido registrado correctamente.'
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR TIEMPO
		DELETE FROM ReportesApp_Operaciones_Previajes_Pernoctes
		WHERE NroTicket = @NroTicket AND idPernocte = @idPernocte

		UPDATE ReportesApp_Operaciones_Previajes_Pernoctes
		SET idPernocte = idPernocte - 1
		WHERE idPernocte > @idPernocte AND NroTicket = @NroTicket

		SET @Exito = '0 = El pernocte ha sido eliminado correctamente.'
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

--------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 26/12/2024
-- Description:	FILTRAR TIEMPO DE VIAJES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Previajes_FiltrarTiemposPernoctes]
@NroTicket INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT idPernocte AS 'NRO', NroTicket, CONVERT(VARCHAR,InicioPernocte,103)+' '+CONVERT(VARCHAR,InicioPernocte,8) AS 'INICIO_PERNOCTE',
	CONVERT(VARCHAR,FinPernocte,103)+' '+CONVERT(VARCHAR,FinPernocte,8) AS 'FIN_PERNOCTE', TipoPernocte AS 'TIPO', Ubicacion AS 'UBICACION'
	FROM ReportesApp_Operaciones_Previajes_Pernoctes
	WHERE NroTicket = @NroTicket
	ORDER BY idPernocte
END

---------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		REYES HORNA, GERARDO
-- Create date: 26/12/2024
-- Description:	LISTAR TIEMPOS VIAJE
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Previajes_ListarTiemposPernocte] 
@Previaje VARCHAR(20),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@Ruta VARCHAR(160),
@Programacion VARCHAR(50),
@Placa VARCHAR(50),
@Conductor VARCHAR(250)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@Programacion = 'TODAS') BEGIN
		SELECT PR.NroTicket AS 'PREVIAJE', PR.idRuta, CONVERT(VARCHAR,PR.FechaProgramacion,103) AS 'FECHA_PROGRAMACION',		O.Descripcion AS 'PROGRAMACION', LTRIM(RTRIM(RT.Descripcion)) AS 'RUTA', LTRIM(RTRIM(C.Nombre)) AS 'CONDUCTOR', V.NumeroPlaca AS 'TRACTO',		R.NumeroPlaca AS 'SEMIRREMOLQUE', PR.Sucursal as 'SUCURSAL', CASE WHEN PR.Estado = 1 THEN 'PROGRAMADO'		WHEN PR.Estado = 2 THEN 'PENDIENTE EN LLEGAR'
		WHEN PR.Estado = 3 THEN 'EN ESPERA'
		WHEN PR.Estado = 4 THEN 'CARGA EN BASE'
		WHEN PR.Estado = 5 THEN 'COLA IN'
		WHEN PR.Estado = 6 THEN 'COLA OUT'
		WHEN PR.Estado = 7 THEN 'CARGANDO'
		WHEN PR.Estado = 8 THEN 'FUERA DE PLANTA' 
		WHEN PR.Estado = 9 THEN 'ATENDIDO'
		WHEN PR.Estado = 10 THEN 'ANULADO'
		WHEN PR.Estado = 11 THEN 'TERMINADO'
		WHEN PR.Estado = 12 THEN 'EN BASE'
		END AS 'ESTADO', PER.idPernocte, PER.TipoPernocte AS 'TIPO', CONVERT(VARCHAR,PER.InicioPernocte,103)+' '+CONVERT(VARCHAR,PER.InicioPernocte,8) AS 'INICIO_PERNOCTE', 		CONVERT(VARCHAR,PER.FinPernocte,103)+' '+CONVERT(VARCHAR,PER.FinPernocte,8) AS 'FIN_PERNOCTE', PER.Ubicacion AS 'UBICACION'		FROM ReportesApp_Operaciones_Previajes_Pernoctes PER		LEFT JOIN ReportesApp_Operacion_Previaje_Registros PR WITH(NOLOCK) ON PER.NroTicket = PR.NroTicket		LEFT JOIN OP_TR_RUTA RT WITH(NOLOCK) ON RT.IDRUTA = PR.IdRuta		LEFT JOIN OP_TR_Conductor C WITH(NOLOCK) ON C.IdConductor = PR.IdConductor		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = PR.idTracto		LEFT JOIN OP_TR_Vehiculo R WITH(NOLOCK) ON R.IdVehiculo = PR.idSemirremolque		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON PR.TipoProgramacion = O.IdOperacion		WHERE (@Previaje IS NULL OR PR.NroTicket LIKE '%' + @Previaje + '%') AND (PR.FechaProgramacion BETWEEN @FINICIO AND @FFIN) AND		(@Ruta IS NULL OR LTRIM(RTRIM(RT.Descripcion)) LIKE '%' + @Ruta + '%') AND (@Conductor IS NULL OR LTRIM(RTRIM(C.Nombre)) LIKE '%' + @Conductor + '%')		AND ((@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') OR (@Placa IS NULL OR R.NumeroPlaca LIKE '%' + @Placa + '%')) AND (PR.TipoProgramacion IN (2,3,4))
		ORDER BY PR.FechaProgramacion DESC
	END
	ELSE BEGIN
		SELECT PR.NroTicket AS 'PREVIAJE', PR.idRuta, CONVERT(VARCHAR,PR.FechaProgramacion,103) AS 'FECHA_PROGRAMACION',		O.Descripcion AS 'PROGRAMACION', LTRIM(RTRIM(RT.Descripcion)) AS 'RUTA', LTRIM(RTRIM(C.Nombre)) AS 'CONDUCTOR', V.NumeroPlaca AS 'TRACTO',		R.NumeroPlaca AS 'SEMIRREMOLQUE', PR.Sucursal as 'SUCURSAL', CASE WHEN PR.Estado = 1 THEN 'PROGRAMADO'		WHEN PR.Estado = 2 THEN 'PENDIENTE EN LLEGAR'
		WHEN PR.Estado = 3 THEN 'EN ESPERA'
		WHEN PR.Estado = 4 THEN 'CARGA EN BASE'
		WHEN PR.Estado = 5 THEN 'COLA IN'
		WHEN PR.Estado = 6 THEN 'COLA OUT'
		WHEN PR.Estado = 7 THEN 'CARGANDO'
		WHEN PR.Estado = 8 THEN 'FUERA DE PLANTA' 
		WHEN PR.Estado = 9 THEN 'ATENDIDO'
		WHEN PR.Estado = 10 THEN 'ANULADO'
		WHEN PR.Estado = 11 THEN 'TERMINADO'
		WHEN PR.Estado = 12 THEN 'EN BASE'
		END AS 'ESTADO', PER.idPernocte, PER.TipoPernocte AS 'TIPO', CONVERT(VARCHAR,PER.InicioPernocte,103)+' '+CONVERT(VARCHAR,PER.InicioPernocte,8) AS 'INICIO_PERNOCTE', 		CONVERT(VARCHAR,PER.FinPernocte,103)+' '+CONVERT(VARCHAR,PER.FinPernocte,8) AS 'FIN_PERNOCTE', PER.TipoPernocte AS 'TIPO', PER.Ubicacion AS 'UBICACION'		FROM ReportesApp_Operaciones_Previajes_Pernoctes PER		LEFT JOIN ReportesApp_Operacion_Previaje_Registros PR WITH(NOLOCK) ON PER.NroTicket = PR.NroTicket		LEFT JOIN OP_TR_RUTA RT WITH(NOLOCK) ON RT.IDRUTA = PR.IdRuta		LEFT JOIN OP_TR_Conductor C WITH(NOLOCK) ON C.IdConductor = PR.IdConductor		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = PR.idTracto		LEFT JOIN OP_TR_Vehiculo R WITH(NOLOCK) ON R.IdVehiculo = PR.idSemirremolque		LEFT JOIN ReportesApp_Operaciones_Previajes_TiemposViajes TV ON TV.NroTicket = PR.NroTicket		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON PR.TipoProgramacion = O.IdOperacion		WHERE (@Previaje IS NULL OR PR.NroTicket LIKE '%' + @Previaje + '%') AND (PR.FechaProgramacion BETWEEN @FINICIO AND @FFIN) AND		(@Ruta IS NULL OR LTRIM(RTRIM(RT.Descripcion)) LIKE '%' + @Ruta + '%') AND (@Conductor IS NULL OR LTRIM(RTRIM(C.Nombre)) LIKE '%' + @Conductor + '%')		AND ((@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') OR (@Placa IS NULL OR R.NumeroPlaca LIKE '%' + @Placa + '%')) AND (O.Descripcion = @Programacion)
		ORDER BY PR.FechaProgramacion DESC
	END
END

---------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		REYES HORNA, GERARDO
-- Create date: 30/12/2024
-- Description:	REGISTRAR TIEMPO DE ATENCION
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Previajes_RegistrarTiempoAtencion]
@Opcion INT,
@idTiempoAtencion INT,
@Destino VARCHAR(250),
@HorarioLV VARCHAR(250),
@HorarioS VARCHAR(250),
@TiempoAtencion TIME(0),
@idOperacion INT,
@idRuta INT,
@UsuarioCrea VARCHAR(20)
AS
DECLARE @Correlativo INT
DECLARE @exito VARCHAR(MAX)

IF (@Opcion = 1) BEGIN
	SET @exito = '0 = ¡Registro Exitoso!'
END
ELSE BEGIN
	SET @exito = '0 = ¡Desvinculación Exitosa!'
END

IF (@Opcion = 1) BEGIN
	IF EXISTS(SELECT * FROM ReportesApp_Operaciones_Previajes_TiempoAtencion WHERE Destino = @Destino AND idRuta = @idRuta AND idOperacion = @idOperacion) BEGIN
		SET @exito='-1 = El destino '+@Destino+' ya tiene asignada esta ruta en esta operación.'
		GOTO Terminar
	END
END

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN
		SET @Correlativo = (SELECT MAX(idTiempoAtencion) FROM ReportesApp_Operaciones_Previajes_TiempoAtencion)
		SET @Correlativo = ISNULL(@Correlativo,0) + 1

		INSERT INTO ReportesApp_Operaciones_Previajes_TiempoAtencion(idTiempoAtencion,Destino,HorarioLV,HorarioS,TiempoAtencion,idOperacion,idRuta,
		UsuarioCrea,FechaCrea)
		VALUES(@Correlativo,@Destino,@HorarioLV,@HorarioS,@TiempoAtencion,@idOperacion,@idRuta,@UsuarioCrea,GETDATE())
	END
	ELSE BEGIN
		DELETE FROM ReportesApp_Operaciones_Previajes_TiempoAtencion WHERE idTiempoAtencion = @idTiempoAtencion
	END
END TRY

BEGIN CATCH
	SET @exito = RTRIM(LTRIM(STR(ERROR_NUMBER()))) + '=' + ERROR_MESSAGE()
	ROLLBACK
	GOTO Terminar
END CATCH

IF @@TRANCOUNT > 0
	COMMIT;

TERMINAR:
IF LEFT(@Exito,1)='-' OR cast(left(@Exito,1) as int)>0 BEGIN
	DECLARE @storedProcedureName VARCHAR(100)
	SELECT @storedProcedureName = OBJECT_NAME(@@PROCID)
	SET @exito = @exito + CHAR(13)+CHAR(13)+'SP Name: '+@storedProcedureName
END

SELECT @exito exito

---------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		REYES HORNA, GERARDO
-- Create date: 30/12/2024
-- Description:	LISTAR TIEMPOS DE ATENCION
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Previajes_ListarTiempoAtencion] 
@Destino VARCHAR(250)
AS
BEGIN
	SELECT TA.idTiempoAtencion, O.Descripcion AS 'PROGRAMACION', TA.Destino AS 'DESTINO', TA.TiempoAtencion AS 'TIEMPO_ATENCION',	TA.HorarioLV AS 'LUNES_VIERNES', TA.HorarioS AS 'SABADO', LTRIM(RTRIM(RT.Codigo)) AS 'CODIGO', LTRIM(RTRIM(RT.Descripcion)) AS 'RUTA'	FROM ReportesApp_Operaciones_Previajes_TiempoAtencion TA	LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON TA.idOperacion = O.IdOperacion	LEFT JOIN OP_TR_RUTA RT WITH(NOLOCK) ON RT.IDRUTA = TA.idRuta	WHERE (@Destino IS NULL OR TA.Destino LIKE '%' + @Destino + '%')	ORDER BY TA.Destino
END






