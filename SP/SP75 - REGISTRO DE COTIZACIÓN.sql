
-- CREAR TABLA ReportesApp_Costos_Cotizaciones_Implementos

-- CREAR TABLA ReportesApp_Costos_Cotizaciones_RegistroCabecera

-- CREAR TABLA ReportesApp_Costos_Cotizaciones_RegistroDetalle

-- CREAR TABLA ReportesApp_Costos_Cotizaciones_Rendimiento

-----------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		REYES HORNA, GERARDO
-- Create date: 27/03/2025
-- Description:	LISTAR IMPLEMENTOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Costos_Cotizaciones_AgregarListarImplementos] 
@Opcion INT,
@Tipo VARCHAR(50),
@Descripcion VARCHAR(350)
AS
BEGIN
	IF (@Opcion = 1) BEGIN		-- LISTAR IMPLEMENTOS
		SELECT idImplemento, Descripcion
		FROM ReportesApp_Costos_Cotizaciones_Implementos
		WHERE Tipo = @Tipo
	END

	IF (@Opcion = 2) BEGIN		-- AGREGAR IMPLEMENTOS
		DECLARE @correlativo INT
		DECLARE @Exito VARCHAR(MAX)

		SET @correlativo = (SELECT MAX(idImplemento) FROM ReportesApp_Costos_Cotizaciones_Implementos)
		SET @correlativo = ISNULL(@correlativo,0) + 1 

		INSERT INTO ReportesApp_Costos_Cotizaciones_Implementos (idImplemento, Tipo, Descripcion)
		VALUES(@correlativo, @Tipo, @Descripcion)

		SET @Exito = '0 = Implemento añadido correctamente.'
		SELECT @exito exito
	END

	IF (@Opcion = 3) BEGIN		-- LISTAR RUTAS Y KM
		SELECT IdRuta, RTRIM(Descripcion) AS 'RUTA', CONVERT(DECIMAL(10,2),Distancia) AS 'KM', CONVERT(DECIMAL(10,2),Tiempo) AS 'HORAS'
		FROM OP_TR_Ruta
		WHERE Estado = '2' AND LTRIM(Descripcion) LIKE '%' + @Descripcion + '%' 
		ORDER BY Descripcion
	END
END

---------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 27/03/2025
-- Description:	INSERTAR DETALLE COTIZACION
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Costos_Cotizaciones_DetalleInsertar]
@Opcion INT,
@idCotizacionC INT,
@idImplemento INT,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

SET @correlativo = (SELECT MAX(idCotizacionD) FROM ReportesApp_Costos_Cotizaciones_RegistroDetalle WHERE idCotizacionC = @idCotizacionC)
SET @correlativo = ISNULL(@correlativo,0) + 1

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN
		IF (EXISTS(SELECT idImplemento FROM ReportesApp_Costos_Cotizaciones_RegistroDetalle WHERE idImplemento = @idImplemento AND idCotizacionC = @idCotizacionC))
		BEGIN
			SET @Exito = '-1 = Este implemento ya fue registrado.'
			ROLLBACK
			GOTO Terminar
		END
		ELSE BEGIN
			INSERT INTO ReportesApp_Costos_Cotizaciones_RegistroDetalle(idCotizacionD,idCotizacionC,idImplemento,UsuarioCrea,FechaCrea)
			VALUES(@correlativo, @idCotizacionC, @idImplemento, @Usuario, GETDATE())

			SET @Exito = '0 = Implemento añadido.'
		END
	END

	IF (@Opcion = 2) BEGIN
		DECLARE @idCotizacionD INT = (SELECT idCotizacionD FROM ReportesApp_Costos_Cotizaciones_RegistroDetalle
									  WHERE idImplemento = @idImplemento AND idCotizacionC = @idCotizacionC)
		
		DELETE FROM ReportesApp_Costos_Cotizaciones_RegistroDetalle
		WHERE idImplemento = @idImplemento AND idCotizacionC = @idCotizacionC

		UPDATE ReportesApp_Costos_Cotizaciones_RegistroDetalle
		SET idCotizacionD = idCotizacionD - 1
		WHERE idCotizacionD > @idCotizacionD AND idCotizacionC = @idCotizacionC

		SET @Exito = '0 = Implemento eliminado.'
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

---------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 27/03/2025
-- Description:	LISTAR DETALLE COTIZACION
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Costos_Cotizaciones_DetalleListar]
@idCotizacionC INT
AS
BEGIN
	SELECT RD.idCotizacionC, RD.idCotizacionD AS 'NRO', RD.idImplemento, I.Tipo AS 'TIPO', I.Descripcion AS 'IMPLEMENTO',
	RD.UsuarioCrea, RD.FechaCrea
	FROM ReportesApp_Costos_Cotizaciones_RegistroDetalle RD
	LEFT JOIN ReportesApp_Costos_Cotizaciones_Implementos I ON I.idImplemento = RD.idImplemento
	WHERE RD.idCotizacionC = @idCotizacionC
	ORDER BY RD.idCotizacionD DESC
END

---------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 27-03-2025
-- Description:	CREAR NUEVA COTIZACION
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Costos_Cotizaciones_RegistrarEditarCotizacion]
@Opcion INT,
@idCotizacionC INT,
@CO INT, @P INT, @T INT, @CI INT, @O INT,
@idRuta INT,
@TipoViaje VARCHAR(50),
@PuntoInicio VARCHAR(250),
@PuntoFin VARCHAR(250),
@Frecuencia DECIMAL(10,2),
@Producto VARCHAR(350),
@RUCCliente VARCHAR(11),
@Telefono VARCHAR(20),
@Contacto VARCHAR(250),
@ValorProducto DECIMAL(10,2),
@Embalaje VARCHAR(250),
@Responsable VARCHAR(50),
@Duracion TIME(7),
@ContratoInicio DATE,
@ContratoFin DATE,
@Permisos VARCHAR(350),
@Tonelaje DECIMAL(10,2),
@PermisosAdicionales VARCHAR(350),
@HorarioIni TIME(7),
@HorarioFin TIME(7),
@Flota VARCHAR(20),
@Mermas DECIMAL(10,2),
@Standby VARCHAR(20),
@Politicas VARCHAR(20),
@FechaInicio DATE,
@NroConductor INT,
@Origen VARCHAR(350),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- INSERTAR COTIZACIÓN
		SET @correlativo = (SELECT MAX(idCotizacionC) FROM ReportesApp_Costos_Cotizaciones_RegistroCabecera)
		SET @correlativo = ISNULL(@correlativo,0) + 1

		INSERT INTO ReportesApp_Costos_Cotizaciones_RegistroCabecera(idCotizacionC,CO,P,T,CI,O,idRuta,TipoViaje,PuntoInicio,PuntoFin,Frecuencia,Producto,RUCCliente,Telefono,Contacto,
		ValorProducto,Embalaje,Responsable,Duracion,ContratoInicio,ContratoFin,Permisos,Tonelaje,PermisosAdicionales,HorarioIni,HorarioFin,Flota,Mermas,[Standby],Politicas,FechaInicio,
		NroConductor,Origen,UsuarioCreacion,FechaCreacion,UsuarioModificacion,FechaModificacion,FechaRegistrado,FechaCotizado,Estado)
		VALUES(@correlativo,@CO,@P,@T,@CI,@O,@idRuta,@TipoViaje,@PuntoInicio,@PuntoFin,@Frecuencia,@Producto,@RUCCliente,@Telefono,@Contacto,@ValorProducto,@Embalaje,@Responsable,
		@Duracion,@ContratoInicio,@ContratoFin,@Permisos,@Tonelaje,@PermisosAdicionales,@HorarioIni,@HorarioFin,@Flota,@Mermas,@Standby,@Politicas,@FechaInicio,@NroConductor,@Origen,
		@Usuario,GETDATE(),@Usuario,GETDATE(),GETDATE(),GETDATE(),'SOLICITADO')

		UPDATE ReportesApp_Costos_Cotizaciones_RegistroDetalle
		SET idCotizacionC = @correlativo
		WHERE idCotizacionC = 0

		SET @Exito = '0 = Solicitud registrada exitosamente.'
	END

	IF (@Opcion = 2) BEGIN		-- ACTUALIZAR
		UPDATE ReportesApp_Costos_Cotizaciones_RegistroCabecera
		SET CO = @CO, P = @P, T = @T, CI = @CI, O = @O, idRuta = @idRuta, TipoViaje = @TipoViaje, PuntoInicio = @PuntoInicio, PuntoFin = @PuntoFin, Frecuencia = @Frecuencia,
		Producto = @Producto, RUCCliente = @RUCCliente, Telefono = @Telefono, Contacto = @Contacto, ValorProducto = @ValorProducto, Embalaje = @Embalaje, Responsable = @Responsable,
		Duracion = @Duracion, ContratoInicio = @ContratoInicio, ContratoFin = @ContratoFin, Permisos = @Permisos, Tonelaje = @Tonelaje, PermisosAdicionales = @PermisosAdicionales,
		HorarioIni = @HorarioIni, HorarioFin = @HorarioFin, Flota = @Flota, Mermas = @Mermas,[Standby] = @Standby, Politicas = @Politicas, FechaInicio = @FechaInicio, NroConductor = @NroConductor,
		Origen = @Origen, UsuarioModificacion = @Usuario, FechaModificacion = GETDATE(), FechaRegistrado = GETDATE(), Estado = 'REGISTRADO'
		WHERE idCotizacionC = @idCotizacionC

		UPDATE ReportesApp_Costos_Cotizaciones_RegistroDetalle
		SET idCotizacionC = @idCotizacionC
		WHERE idCotizacionC = 0

		SET @Exito = '0 = Cotización completada exitosamente.'
	END

	IF (@Opcion = 3) BEGIN		-- ACTUALIZAR
		UPDATE ReportesApp_Costos_Cotizaciones_RegistroCabecera
		SET CO = @CO, P = @P, T = @T, CI = @CI, O = @O, idRuta = @idRuta, TipoViaje = @TipoViaje, PuntoInicio = @PuntoInicio, PuntoFin = @PuntoFin, Frecuencia = @Frecuencia,
		Producto = @Producto, RUCCliente = @RUCCliente, Telefono = @Telefono, Contacto = @Contacto, ValorProducto = @ValorProducto, Embalaje = @Embalaje, Responsable = @Responsable,
		Duracion = @Duracion, ContratoInicio = @ContratoInicio, ContratoFin = @ContratoFin, Permisos = @Permisos, Tonelaje = @Tonelaje, PermisosAdicionales = @PermisosAdicionales,
		HorarioIni = @HorarioIni, HorarioFin = @HorarioFin, Flota = @Flota, Mermas = @Mermas,[Standby] = @Standby, Politicas = @Politicas, FechaInicio = @FechaInicio, NroConductor = @NroConductor,
		Origen = @Origen, UsuarioModificacion = @Usuario, FechaModificacion = GETDATE(), FechaRegistrado = GETDATE(), Estado = 'SOLICITADO'
		WHERE idCotizacionC = @idCotizacionC

		UPDATE ReportesApp_Costos_Cotizaciones_RegistroDetalle
		SET idCotizacionC = @idCotizacionC
		WHERE idCotizacionC = 0

		SET @Exito = '0 = Solicitud modificada exitosamente.'
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
-- Create date: 28/03/2025
-- Description:	LISTAR COTIZACIONES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Costos_Cotizaciones_ListarRegistroCotizaciones]
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@Cliente VARCHAR(250),
@Flota VARCHAR(30),
@Estado VARCHAR(50)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@Flota = 'TODOS' AND @Estado = 'TODOS') BEGIN
		SELECT CD.idCotizacionC AS 'NRO', CD.CO AS '1', CASE WHEN CD.CO = 1 THEN 'X' ELSE ' ' END AS 'CO', CD.P AS '2', CASE WHEN CD.P = 1 THEN 'X' ELSE ' ' END AS 'P',
		CD.T AS '3', CASE WHEN CD.T = 1 THEN 'X' ELSE ' ' END AS 'T', CD.CI AS '4', CASE WHEN CD.CI = 1 THEN 'X' ELSE ' ' END AS 'CI', CD.O AS '5', CASE WHEN CD.O = 1 THEN 'X' ELSE ' ' END AS 'O', CD.Estado AS 'ESTADO',
		CD.TipoViaje AS 'TIPO_VIAJE', CD.idRuta, RTRIM(R.Descripcion) AS 'RUTA', CONVERT(DECIMAL(10,2),R.Distancia) AS 'KM', CONVERT(DECIMAL(10,2),R.Tiempo) AS 'HORAS',
		CD.PuntoInicio AS 'PUNTO_INICIO', CD.PuntoFin AS 'PUNTO_FIN', CD.Frecuencia AS 'FRECUENCIA', CD.RUCCliente AS 'RUC', LTRIM(RTRIM(P.NombreCompleto)) AS 'CLIENTE', CD.Telefono AS 'TELEFONO',
		CD.Contacto AS 'CONTACTO', CD.Producto AS 'PRODUCTO', CD.ValorProducto AS 'VALOR_PRODUCTO', CD.Embalaje AS 'EMBALAJE', CD.Responsable AS 'RESPONSABLE', CONVERT(VARCHAR,CD.Duracion,8) AS 'HORAS_DURACION',
		CD.ContratoInicio, CD.ContratoFin, CONVERT(VARCHAR,CD.ContratoInicio,103)+' - '+CONVERT(VARCHAR,CD.ContratoFin,103) AS 'CONTRATO', CD.Permisos AS 'PERMISOS', CD.Tonelaje AS 'TONELAJE',
		CD.PermisosAdicionales AS 'GASTOS_ADICIONALES', CD.HorarioIni, CD.HorarioFin, CONVERT(VARCHAR,CD.HorarioIni,8)+' - '+CONVERT(VARCHAR,CD.HorarioFin,8) AS 'HORARIO', CD.Flota AS 'FLOTA',
		CD.HistorialCrediticio AS 'HISTORIAL_CREDITICIO', CD.Mermas AS 'MERMAS', CD.[Standby] AS 'STANDBY', CD.Politicas AS 'POLÍTICAS', CD.FechaInicio AS 'FECHA_INICIO',
		CD.NroConductor AS 'NRO_CONDUCTOR', CD.Origen AS 'ORIGEN', CD.UsuarioModificacion AS 'ULTIMO_USUARIO', CD.FechaCreacion AS 'FECHA_SOLICITUD', CD.FechaRegistrado AS 'FECHA_REGISTRO',
		CD.FechaCotizado AS 'FECHA_COTIZACION', CD.UsuarioCreacion, CD.FechaModificacion
		FROM ReportesApp_Costos_Cotizaciones_RegistroCabecera CD
		LEFT JOIN PersonaMast P ON LTRIM(RTRIM(P.DocumentoFiscal)) = CD.RUCCliente
		LEFT JOIN OP_TR_Ruta R ON R.IdRuta = CD.idRuta
		WHERE (CD.FechaInicio BETWEEN @FINICIO AND @FFIN) AND (P.NombreCompleto IS NULL OR P.NombreCompleto LIKE '%' + @Cliente + '%')
		ORDER BY CD.idCotizacionC DESC
	END

	IF (@Flota != 'TODOS' AND @Estado = 'TODOS') BEGIN
		SELECT CD.idCotizacionC AS 'NRO', CD.CO AS '1', CASE WHEN CD.CO = 1 THEN 'X' ELSE ' ' END AS 'CO', CD.P AS '2', CASE WHEN CD.P = 1 THEN 'X' ELSE ' ' END AS 'P',
		CD.T AS '3', CASE WHEN CD.T = 1 THEN 'X' ELSE ' ' END AS 'T', CD.CI AS '4', CASE WHEN CD.CI = 1 THEN 'X' ELSE ' ' END AS 'CI', CD.O AS '5', CASE WHEN CD.O = 1 THEN 'X' ELSE ' ' END AS 'O', CD.Estado AS 'ESTADO',
		CD.TipoViaje AS 'TIPO_VIAJE', CD.idRuta, RTRIM(R.Descripcion) AS 'RUTA', CONVERT(DECIMAL(10,2),R.Distancia) AS 'KM', CONVERT(DECIMAL(10,2),R.Tiempo) AS 'HORAS',
		CD.PuntoInicio AS 'PUNTO_INICIO', CD.PuntoFin AS 'PUNTO_FIN', CD.Frecuencia AS 'FRECUENCIA', CD.RUCCliente AS 'RUC', LTRIM(RTRIM(P.NombreCompleto)) AS 'CLIENTE', CD.Telefono AS 'TELEFONO',
		CD.Contacto AS 'CONTACTO', CD.Producto AS 'PRODUCTO', CD.ValorProducto AS 'VALOR_PRODUCTO', CD.Embalaje AS 'EMBALAJE', CD.Responsable AS 'RESPONSABLE', CONVERT(VARCHAR,CD.Duracion,8) AS 'HORAS_DURACION',
		CD.ContratoInicio, CD.ContratoFin, CONVERT(VARCHAR,CD.ContratoInicio,103)+' - '+CONVERT(VARCHAR,CD.ContratoFin,103) AS 'CONTRATO', CD.Permisos AS 'PERMISOS', CD.Tonelaje AS 'TONELAJE',
		CD.PermisosAdicionales AS 'GASTOS_ADICIONALES', CD.HorarioIni, CD.HorarioFin, CONVERT(VARCHAR,CD.HorarioIni,8)+' - '+CONVERT(VARCHAR,CD.HorarioFin,8) AS 'HORARIO', CD.Flota AS 'FLOTA',
		CD.HistorialCrediticio AS 'HISTORIAL_CREDITICIO', CD.Mermas AS 'MERMAS', CD.[Standby] AS 'STANDBY', CD.Politicas AS 'POLÍTICAS', CD.FechaInicio AS 'FECHA_INICIO',
		CD.NroConductor AS 'NRO_CONDUCTOR', CD.Origen AS 'ORIGEN', CD.UsuarioModificacion AS 'ULTIMO_USUARIO', CD.FechaCreacion AS 'FECHA_SOLICITUD', CD.FechaRegistrado AS 'FECHA_REGISTRO',
		CD.FechaCotizado AS 'FECHA_COTIZACION', CD.UsuarioCreacion, CD.FechaModificacion
		FROM ReportesApp_Costos_Cotizaciones_RegistroCabecera CD
		LEFT JOIN PersonaMast P ON LTRIM(RTRIM(P.DocumentoFiscal)) = CD.RUCCliente
		LEFT JOIN OP_TR_Ruta R ON R.IdRuta = CD.idRuta
		WHERE (CD.FechaInicio BETWEEN @FINICIO AND @FFIN) AND (P.NombreCompleto IS NULL OR P.NombreCompleto LIKE '%' + @Cliente + '%') AND (CD.Flota = @Flota)
		ORDER BY CD.idCotizacionC DESC
	END

	IF (@Flota = 'TODOS' AND @Estado != 'TODOS') BEGIN
		SELECT CD.idCotizacionC AS 'NRO', CD.CO AS '1', CASE WHEN CD.CO = 1 THEN 'X' ELSE ' ' END AS 'CO', CD.P AS '2', CASE WHEN CD.P = 1 THEN 'X' ELSE ' ' END AS 'P',
		CD.T AS '3', CASE WHEN CD.T = 1 THEN 'X' ELSE ' ' END AS 'T', CD.CI AS '4', CASE WHEN CD.CI = 1 THEN 'X' ELSE ' ' END AS 'CI', CD.O AS '5', CASE WHEN CD.O = 1 THEN 'X' ELSE ' ' END AS 'O', CD.Estado AS 'ESTADO',
		CD.TipoViaje AS 'TIPO_VIAJE', CD.idRuta, RTRIM(R.Descripcion) AS 'RUTA', CONVERT(DECIMAL(10,2),R.Distancia) AS 'KM', CONVERT(DECIMAL(10,2),R.Tiempo) AS 'HORAS',
		CD.PuntoInicio AS 'PUNTO_INICIO', CD.PuntoFin AS 'PUNTO_FIN', CD.Frecuencia AS 'FRECUENCIA', CD.RUCCliente AS 'RUC', LTRIM(RTRIM(P.NombreCompleto)) AS 'CLIENTE', CD.Telefono AS 'TELEFONO',
		CD.Contacto AS 'CONTACTO', CD.Producto AS 'PRODUCTO', CD.ValorProducto AS 'VALOR_PRODUCTO', CD.Embalaje AS 'EMBALAJE', CD.Responsable AS 'RESPONSABLE', CONVERT(VARCHAR,CD.Duracion,8) AS 'HORAS_DURACION',
		CD.ContratoInicio, CD.ContratoFin, CONVERT(VARCHAR,CD.ContratoInicio,103)+' - '+CONVERT(VARCHAR,CD.ContratoFin,103) AS 'CONTRATO', CD.Permisos AS 'PERMISOS', CD.Tonelaje AS 'TONELAJE',
		CD.PermisosAdicionales AS 'GASTOS_ADICIONALES', CD.HorarioIni, CD.HorarioFin, CONVERT(VARCHAR,CD.HorarioIni,8)+' - '+CONVERT(VARCHAR,CD.HorarioFin,8) AS 'HORARIO', CD.Flota AS 'FLOTA',
		CD.HistorialCrediticio AS 'HISTORIAL_CREDITICIO', CD.Mermas AS 'MERMAS', CD.[Standby] AS 'STANDBY', CD.Politicas AS 'POLÍTICAS', CD.FechaInicio AS 'FECHA_INICIO',
		CD.NroConductor AS 'NRO_CONDUCTOR', CD.Origen AS 'ORIGEN', CD.UsuarioModificacion AS 'ULTIMO_USUARIO', CD.FechaCreacion AS 'FECHA_SOLICITUD', CD.FechaRegistrado AS 'FECHA_REGISTRO',
		CD.FechaCotizado AS 'FECHA_COTIZACION', CD.UsuarioCreacion, CD.FechaModificacion
		FROM ReportesApp_Costos_Cotizaciones_RegistroCabecera CD
		LEFT JOIN PersonaMast P ON LTRIM(RTRIM(P.DocumentoFiscal)) = CD.RUCCliente
		LEFT JOIN OP_TR_Ruta R ON R.IdRuta = CD.idRuta
		WHERE (CD.FechaInicio BETWEEN @FINICIO AND @FFIN) AND (P.NombreCompleto IS NULL OR P.NombreCompleto LIKE '%' + @Cliente + '%') AND (CD.Estado = @Estado)
		ORDER BY CD.idCotizacionC DESC
	END

	IF (@Flota != 'TODOS' AND @Estado != 'TODOS') BEGIN
		SELECT CD.idCotizacionC AS 'NRO', CD.CO AS '1', CASE WHEN CD.CO = 1 THEN 'X' ELSE ' ' END AS 'CO', CD.P AS '2', CASE WHEN CD.P = 1 THEN 'X' ELSE ' ' END AS 'P',
		CD.T AS '3', CASE WHEN CD.T = 1 THEN 'X' ELSE ' ' END AS 'T', CD.CI AS '4', CASE WHEN CD.CI = 1 THEN 'X' ELSE ' ' END AS 'CI', CD.O AS '5', CASE WHEN CD.O = 1 THEN 'X' ELSE ' ' END AS 'O', CD.Estado AS 'ESTADO',
		CD.TipoViaje AS 'TIPO_VIAJE', CD.idRuta, RTRIM(R.Descripcion) AS 'RUTA', CONVERT(DECIMAL(10,2),R.Distancia) AS 'KM', CONVERT(DECIMAL(10,2),R.Tiempo) AS 'HORAS',
		CD.PuntoInicio AS 'PUNTO_INICIO', CD.PuntoFin AS 'PUNTO_FIN', CD.Frecuencia AS 'FRECUENCIA', CD.RUCCliente AS 'RUC', LTRIM(RTRIM(P.NombreCompleto)) AS 'CLIENTE', CD.Telefono AS 'TELEFONO',
		CD.Contacto AS 'CONTACTO', CD.Producto AS 'PRODUCTO', CD.ValorProducto AS 'VALOR_PRODUCTO', CD.Embalaje AS 'EMBALAJE', CD.Responsable AS 'RESPONSABLE', CONVERT(VARCHAR,CD.Duracion,8) AS 'HORAS_DURACION',
		CD.ContratoInicio, CD.ContratoFin, CONVERT(VARCHAR,CD.ContratoInicio,103)+' - '+CONVERT(VARCHAR,CD.ContratoFin,103) AS 'CONTRATO', CD.Permisos AS 'PERMISOS', CD.Tonelaje AS 'TONELAJE',
		CD.PermisosAdicionales AS 'GASTOS_ADICIONALES', CD.HorarioIni, CD.HorarioFin, CONVERT(VARCHAR,CD.HorarioIni,8)+' - '+CONVERT(VARCHAR,CD.HorarioFin,8) AS 'HORARIO', CD.Flota AS 'FLOTA',
		CD.HistorialCrediticio AS 'HISTORIAL_CREDITICIO', CD.Mermas AS 'MERMAS', CD.[Standby] AS 'STANDBY', CD.Politicas AS 'POLÍTICAS', CD.FechaInicio AS 'FECHA_INICIO',
		CD.NroConductor AS 'NRO_CONDUCTOR', CD.Origen AS 'ORIGEN', CD.UsuarioModificacion AS 'ULTIMO_USUARIO', CD.FechaCreacion AS 'FECHA_SOLICITUD', CD.FechaRegistrado AS 'FECHA_REGISTRO',
		CD.FechaCotizado AS 'FECHA_COTIZACION', CD.UsuarioCreacion, CD.FechaModificacion
		FROM ReportesApp_Costos_Cotizaciones_RegistroCabecera CD
		LEFT JOIN PersonaMast P ON LTRIM(RTRIM(P.DocumentoFiscal)) = CD.RUCCliente
		LEFT JOIN OP_TR_Ruta R ON R.IdRuta = CD.idRuta
		WHERE (CD.FechaInicio BETWEEN @FINICIO AND @FFIN) AND (P.NombreCompleto IS NULL OR P.NombreCompleto LIKE '%' + @Cliente + '%') AND (CD.Estado = @Estado)
		AND (CD.Flota = @Flota)
		ORDER BY CD.idCotizacionC DESC
	END
END

-----------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 28/03/2025
-- Description:	LISTAR DETALLE COTIZACION
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Costos_Cotizaciones_ModificarEliminarCotizaciones]
@Opcion INT,
@idCotizacionC INT,
@Historial VARCHAR(50),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- ELIMINAR COTIZACION
		DELETE FROM ReportesApp_Costos_Cotizaciones_RegistroCabecera
		WHERE idCotizacionC = @idCotizacionC

		DELETE FROM ReportesApp_Costos_Cotizaciones_RegistroDetalle
		WHERE idCotizacionC = @idCotizacionC

		SET @Exito = '0 = Cotizacion eliminada.'
	END
	
	IF (@Opcion = 2) BEGIN		-- AÑADIR HISTORIAL CREDITICIO
		UPDATE ReportesApp_Costos_Cotizaciones_RegistroCabecera
		SET HistorialCrediticio = @Historial
		WHERE idCotizacionC = @idCotizacionC

		SET @Exito = '0 = Hisorial añadido.'
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

-----------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 07/04/2025
-- Description:	LISTAR DATOS COTIZACIONES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Costos_Cotizaciones_ListarDatos]
@Opcion INT,
@idRuta INT,
@Ruta VARCHAR(350),
@FechaRegistro DATETIME
AS
BEGIN
	IF (@Opcion = 1) BEGIN		-- LISTAR RENDIMIENTOS DE COMBUSTIBLE
		/*
		DECLARE @TEMP_CARGA TABLE (idCarga INT, idViaje INT, NumeroDocumento INT)
		DECLARE @TEMP_REND TABLE (Numero INT, Fecha DATE, Ruta VARCHAR(300), RendKM DECIMAL(10,2))
		
		INSERT INTO @TEMP_CARGA(idCarga, idViaje, NumeroDocumento)
		SELECT NT.IdCarga, TK.IdViaje, NT.NumeroDocumento
		FROM ReportesApp_Combustible_Nexo_Ticket_CargaCombustible_Kardex NT WITH(NOLOCK)
		INNER JOIN ReportesApp_Combustible_TicketsSurtidor TK WITH(NOLOCK) ON TK.Ticket = NT.Ticket AND TK.NroTicketPreViaje = NT.NroTicketPreViaje
		AND TK.Placa = NT.Placa AND TK.IDViaje = NT.IDViaje
		WHERE TK.IDViaje IS NOT NULL AND NT.IDViaje IS NOT NULL AND NT.Tipo = 'P' AND (NT.FechaDespacho BETWEEN CONVERT(DATE,DATEADD(MONTH,-2,GETDATE())) AND CONVERT(DATE,GETDATE())) 
		UNION
		SELECT NT.IdCarga, TT.IDViaje, NT.NumeroDocumento
		FROM ReportesApp_Combustible_Nexo_Ticket_CargaCombustible_Kardex NT WITH(NOLOCK)
		INNER JOIN ReportesApp_Combustible_TicketsTercero TT WITH(NOLOCK) ON TT.Ticket = NT.Ticket AND TT.Codigo = NT.NroTicketPreViaje
		AND TT.Placa = NT.Placa AND TT.IDViaje = NT.IDViaje
		WHERE TT.IDViaje IS NOT NULL AND NT.IDViaje IS NOT NULL AND NT.Tipo = 'T' AND (NT.FechaDespacho BETWEEN CONVERT(DATE,DATEADD(MONTH,-2,GETDATE())) AND CONVERT(DATE,GETDATE()))

		INSERT INTO @TEMP_REND(Numero, Fecha, Ruta, RendKM)
		SELECT ROW_NUMBER() OVER(ORDER BY COMBUSTIBLE.FechaDespacho ASC) AS 'NRO', COMBUSTIBLE.FechaDespacho AS 'FECHA',
		CASE WHEN (SELECT count(IdCarga) FROM OP_TR_DespachoCombustible WHERE IdCarga = DESPACHO.IdCarga) > 1 THEN RTRIM(DESPACHO.Ruta1)
		ELSE RTRIM(OP_TR_RUTA.Descripcion) END as 'RUTA', DESPACHO.RendimientoFisico AS 'REND KM/GL'
		FROM OP_TR_VIAJE V
		INNER JOIN OP_TR_RUTA WITH(NOLOCK) ON OP_TR_RUTA.IDRUTA = v.IDRUTA 
		INNER JOIN OP_TR_CargaCombustible AS COMBUSTIBLE WITH(NOLOCK) ON COMBUSTIBLE.IdViaje = v.IdViaje
		INNER JOIN @TEMP_CARGA T ON T.idViaje = COMBUSTIBLE.IdViaje AND T.idCarga = COMBUSTIBLE.IdCarga AND T.NumeroDocumento = COMBUSTIBLE.NumeroDocumentoSalida
		LEFT JOIN OP_TR_DespachoCombustible DESPACHO ON DESPACHO.IdCarga=T.IdCarga
		WHERE COMBUSTIBLE.FechaDespacho BETWEEN CONVERT(DATE,DATEADD(MONTH,-2,GETDATE())) AND CONVERT(DATE,GETDATE()) AND COMBUSTIBLE.Estado = 2 AND 
		CASE WHEN (SELECT count(IdCarga) FROM OP_TR_DespachoCombustible WHERE IdCarga = DESPACHO.IdCarga) > 1 THEN RTRIM(DESPACHO.Ruta1)
		ELSE RTRIM(OP_TR_RUTA.Descripcion) END LIKE '%'+@Ruta+'%'
		ORDER BY COMBUSTIBLE.FechaDespacho DESC

		DECLARE @RendMaximo DECIMAL(10,2) = (SELECT MAX(RendKM) FROM @TEMP_REND)
		DECLARE @RendMinimo DECIMAL(10,2) = (SELECT MIN(RendKM) FROM @TEMP_REND)
		DECLARE @RendProm DECIMAL(10,2) = (SELECT AVG(RendKM) FROM @TEMP_REND)

		INSERT INTO ReportesApp_Costos_Cotizaciones_Rendimiento(RendMaximo, RendMinimo, RendProm)
		SELECT ISNULL(@RendMaximo,0.00) AS 'MAXIMO', ISNULL(@RendMinimo,0.00) AS 'MINIMO', ISNULL(@RendProm,0.00) AS 'PROMEDIO'
		*/

		SELECT RendMaximo AS 'MAXIMO', RendMinimo AS 'MINIMO', RendProm AS 'PROMEDIO'
		FROM ReportesApp_Costos_Cotizaciones_Rendimiento
		WHERE Ruta = @Ruta
	END

	IF (@Opcion = 2) BEGIN		-- LISTAR RENDIMIENTOS DE COMBUSTIBLE
		SELECT TOP(1) WH_Kardex.PrecioUnitario FROM WH_Kardex		INNER JOIN WH_TransaccionDetalle ON ( WH_Kardex.ReferenciaCompaniaSocio = WH_TransaccionDetalle.CompaniaSocio ) and		( WH_Kardex.ReferenciaTipoDocumento = WH_TransaccionDetalle.TipoDocumento ) and ( WH_Kardex.ReferenciaNumeroDocumento = WH_TransaccionDetalle.NumeroDocumento )		and ( WH_Kardex.ReferenciaSecuencia = WH_TransaccionDetalle.Secuencia )		INNER JOIN WH_TransaccionHeader ON ( WH_TransaccionHeader.CompaniaSocio = WH_TransaccionDetalle.CompaniaSocio ) and		( WH_TransaccionHeader.TipoDocumento = WH_TransaccionDetalle.TipoDocumento ) and ( WH_TransaccionHeader.NumeroDocumento = WH_TransaccionDetalle.NumeroDocumento )		INNER JOIN WH_AlmacenMast ON ( WH_Kardex.AlmacenCodigo = WH_AlmacenMast.AlmacenCodigo )		WHERE (WH_TransaccionHeader.CompaniaSocio = '10000000') AND (WH_TransaccionHeader.TipoDocumento = 'NI') AND (WH_Kardex.Item = '1701001001')		AND (WH_AlmacenMast.TipoAlmacen = 'P') 		ORDER BY WH_TransaccionHeader.FechaDocumento DESC
	END

	IF (@Opcion = 3) BEGIN		-- LISTAR DATOS DE MANTENIMIENTO
		DECLARE @MetricaMtto1 DECIMAL(10,2) = (SELECT SUM(MTR_MANTENIMIENTO) FROM STG_RENTABILIDAD WHERE MONTH(FECHA) = MONTH(DATEADD(MONTH,-3,@FechaRegistro)) AND YEAR(FECHA) = YEAR(DATEADD(MONTH,-3,@FechaRegistro)))
		DECLARE @MetricaMtto2 DECIMAL(10,2) = (SELECT SUM(MTR_MANTENIMIENTO) FROM STG_RENTABILIDAD WHERE MONTH(FECHA) = MONTH(DATEADD(MONTH,-2,@FechaRegistro)) AND YEAR(FECHA) = YEAR(DATEADD(MONTH,-2,@FechaRegistro)))
		DECLARE @MetricaMtto3 DECIMAL(10,2) = (SELECT SUM(MTR_MANTENIMIENTO) FROM STG_RENTABILIDAD WHERE MONTH(FECHA) = MONTH(DATEADD(MONTH,-1,@FechaRegistro)) AND YEAR(FECHA) = YEAR(DATEADD(MONTH,-1,@FechaRegistro)))
		
		DECLARE @MetricaKM1 DECIMAL(10,2) = (SELECT SUM(MTR_KM) FROM STG_RENTABILIDAD WHERE MONTH(FECHA) = MONTH(DATEADD(MONTH,-3,@FechaRegistro)) AND YEAR(FECHA) = YEAR(DATEADD(MONTH,-3,@FechaRegistro)))
		DECLARE @MetricaKM2 DECIMAL(10,2) = (SELECT SUM(MTR_KM) FROM STG_RENTABILIDAD WHERE MONTH(FECHA) = MONTH(DATEADD(MONTH,-2,@FechaRegistro)) AND YEAR(FECHA) = YEAR(DATEADD(MONTH,-2,@FechaRegistro)))
		DECLARE @MetricaKM3 DECIMAL(10,2) = (SELECT SUM(MTR_KM) FROM STG_RENTABILIDAD WHERE MONTH(FECHA) = MONTH(DATEADD(MONTH,-1,@FechaRegistro)) AND YEAR(FECHA) = YEAR(DATEADD(MONTH,-1,@FechaRegistro)))
		
		DECLARE @MetricaNeu1 DECIMAL(10,2) = (SELECT SUM(MTR_NEUMATICOS) FROM STG_RENTABILIDAD WHERE MONTH(FECHA) = MONTH(DATEADD(MONTH,-3,@FechaRegistro)) AND YEAR(FECHA) = YEAR(DATEADD(MONTH,-3,@FechaRegistro)))
		DECLARE @MetricaNeu2 DECIMAL(10,2) = (SELECT SUM(MTR_NEUMATICOS) FROM STG_RENTABILIDAD WHERE MONTH(FECHA) = MONTH(DATEADD(MONTH,-2,@FechaRegistro)) AND YEAR(FECHA) = YEAR(DATEADD(MONTH,-2,@FechaRegistro)))
		DECLARE @MetricaNeu3 DECIMAL(10,2) = (SELECT SUM(MTR_NEUMATICOS) FROM STG_RENTABILIDAD WHERE MONTH(FECHA) = MONTH(DATEADD(MONTH,-1,@FechaRegistro)) AND YEAR(FECHA) = YEAR(DATEADD(MONTH,-1,@FechaRegistro)))

		DECLARE @Periodo1 VARCHAR(20) = (SELECT TOP(1) Periodo FROM STG_RENTABILIDAD WHERE MONTH(FECHA) = MONTH(DATEADD(MONTH,-3,@FechaRegistro)) AND YEAR(FECHA) = YEAR(DATEADD(MONTH,-3,@FechaRegistro)))
		DECLARE @Periodo2 VARCHAR(20) = (SELECT TOP(1) Periodo FROM STG_RENTABILIDAD WHERE MONTH(FECHA) = MONTH(DATEADD(MONTH,-2,@FechaRegistro)) AND YEAR(FECHA) = YEAR(DATEADD(MONTH,-2,@FechaRegistro)))
		DECLARE @Periodo3 VARCHAR(20) = (SELECT TOP(1) Periodo FROM STG_RENTABILIDAD WHERE MONTH(FECHA) = MONTH(DATEADD(MONTH,-1,@FechaRegistro)) AND YEAR(FECHA) = YEAR(DATEADD(MONTH,-1,@FechaRegistro)))

		DECLARE @TotalMtto1 DECIMAL(10,2) = @MetricaMtto1 / @MetricaKM1
		DECLARE @TotalMtto2 DECIMAL(10,2) = @MetricaMtto2 / @MetricaKM2
		DECLARE @TotalMtto3 DECIMAL(10,2) = @MetricaMtto3 / @MetricaKM3

		DECLARE @TotalNeu1 DECIMAL(10,2) = @MetricaNeu1 / @MetricaKM1
		DECLARE @TotalNeu2 DECIMAL(10,2) = @MetricaNeu2 / @MetricaKM2
		DECLARE @TotalNeu3 DECIMAL(10,2) = @MetricaNeu3 / @MetricaKM3

		DECLARE @TotalIngresos1 DECIMAL(10,2) = (SELECT SUM(TotalIngresos) FROM pr_planillaempleado WHERE CentroCosto IN ('020301','020401') AND Periodo LIKE '%'+@Periodo1+'%') / @MetricaKM1
		DECLARE @TotalIngresos2 DECIMAL(10,2) = (SELECT SUM(TotalIngresos) FROM pr_planillaempleado WHERE CentroCosto IN ('020301','020401') AND Periodo LIKE '%'+@Periodo2+'%') / @MetricaKM2
		DECLARE @TotalIngresos3 DECIMAL(10,2) = (SELECT SUM(TotalIngresos) FROM pr_planillaempleado WHERE CentroCosto IN ('020301','020401') AND Periodo LIKE '%'+@Periodo3+'%') / @MetricaKM3
		DECLARE @ManoObra DECIMAL(10,2) = (@TotalIngresos1 + @TotalIngresos2 + @TotalIngresos3) / 3

		DECLARE @T_DEPRECIACION TABLE(Periodo VARCHAR(10), DescripcionLocal VARCHAR(350), Depreciacion DECIMAL(10,2))
		
		INSERT INTO @T_DEPRECIACION(Periodo,DescripcionLocal,Depreciacion)
		SELECT FA_ActivoHistoria.Periodo, FA_Categoria.DescripcionLocal, SUM(ALocalDeprMes) as Depreciacion		FROM FA_ActivoHistoria		INNER JOIN FA_Activo ON ( FA_ActivoHistoria.Activo = FA_Activo.Activo )		INNER JOIN FA_Categoria ON ( FA_Categoria.Categoria = FA_Activo.Categoria )		LEFT JOIN FA_ActivoContabilidad ON (FA_ActivoHistoria.Activo = FA_ActivoContabilidad.Activo) AND (FA_ActivoHistoria.LedgerGroup = FA_ActivoContabilidad.LedgerGroup)		LEFT OUTER JOIN accountmst ON FA_Categoria.CuentaHistorica = accountmst.account		LEFT OUTER JOIN FA_EstadoConservacion ON FA_Activo.EstadoConservacion = FA_EstadoConservacion.CodigoEstado		INNER JOIN FA_CategoriaContabilidad ON ( FA_CategoriaContabilidad.Categoria = FA_Categoria.Categoria)		LEFT JOIN AC_CostCenterMst ON FA_Activo.CentroCostos = AC_CostCenterMst.CostCenter		WHERE ((FA_Activo.CompaniaSocio = '10000000') AND (FA_ActivoHistoria.Periodo = @Periodo1) AND (FA_ActivoHistoria.LedgerGroup = 'F')		AND FA_CategoriaContabilidad.LedgerGroup = 'F') AND FA_Activo.PeriodoIngreso <= @Periodo1		GROUP BY FA_Activo.CompaniaSocio, FA_ActivoHistoria.Periodo, FA_Activo.Categoria, FA_Activo.Activo, FA_Categoria.DescripcionLocal

		INSERT INTO @T_DEPRECIACION(Periodo,DescripcionLocal,Depreciacion)
		SELECT FA_ActivoHistoria.Periodo, FA_Categoria.DescripcionLocal, SUM(ALocalDeprMes) as Depreciacion		FROM FA_ActivoHistoria		INNER JOIN FA_Activo ON ( FA_ActivoHistoria.Activo = FA_Activo.Activo )		INNER JOIN FA_Categoria ON ( FA_Categoria.Categoria = FA_Activo.Categoria )		LEFT JOIN FA_ActivoContabilidad ON (FA_ActivoHistoria.Activo = FA_ActivoContabilidad.Activo) AND (FA_ActivoHistoria.LedgerGroup = FA_ActivoContabilidad.LedgerGroup)		LEFT OUTER JOIN accountmst ON FA_Categoria.CuentaHistorica = accountmst.account		LEFT OUTER JOIN FA_EstadoConservacion ON FA_Activo.EstadoConservacion = FA_EstadoConservacion.CodigoEstado		INNER JOIN FA_CategoriaContabilidad ON ( FA_CategoriaContabilidad.Categoria = FA_Categoria.Categoria)		LEFT JOIN AC_CostCenterMst ON FA_Activo.CentroCostos = AC_CostCenterMst.CostCenter		WHERE ((FA_Activo.CompaniaSocio = '10000000') AND (FA_ActivoHistoria.Periodo = @Periodo2) AND (FA_ActivoHistoria.LedgerGroup = 'F')		AND FA_CategoriaContabilidad.LedgerGroup = 'F') AND FA_Activo.PeriodoIngreso <= @Periodo2		GROUP BY FA_Activo.CompaniaSocio, FA_ActivoHistoria.Periodo, FA_Activo.Categoria, FA_Activo.Activo, FA_Categoria.DescripcionLocal

		INSERT INTO @T_DEPRECIACION(Periodo,DescripcionLocal,Depreciacion)
		SELECT FA_ActivoHistoria.Periodo, FA_Categoria.DescripcionLocal, SUM(ALocalDeprMes) as Depreciacion		FROM FA_ActivoHistoria		INNER JOIN FA_Activo ON ( FA_ActivoHistoria.Activo = FA_Activo.Activo )		INNER JOIN FA_Categoria ON ( FA_Categoria.Categoria = FA_Activo.Categoria )		LEFT JOIN FA_ActivoContabilidad ON (FA_ActivoHistoria.Activo = FA_ActivoContabilidad.Activo) AND (FA_ActivoHistoria.LedgerGroup = FA_ActivoContabilidad.LedgerGroup)		LEFT OUTER JOIN accountmst ON FA_Categoria.CuentaHistorica = accountmst.account		LEFT OUTER JOIN FA_EstadoConservacion ON FA_Activo.EstadoConservacion = FA_EstadoConservacion.CodigoEstado		INNER JOIN FA_CategoriaContabilidad ON ( FA_CategoriaContabilidad.Categoria = FA_Categoria.Categoria)		LEFT JOIN AC_CostCenterMst ON FA_Activo.CentroCostos = AC_CostCenterMst.CostCenter		WHERE ((FA_Activo.CompaniaSocio = '10000000') AND (FA_ActivoHistoria.Periodo = @Periodo3) AND (FA_ActivoHistoria.LedgerGroup = 'F')		AND FA_CategoriaContabilidad.LedgerGroup = 'F') AND FA_Activo.PeriodoIngreso <= @Periodo3		GROUP BY FA_Activo.CompaniaSocio, FA_ActivoHistoria.Periodo, FA_Activo.Categoria, FA_Activo.Activo, FA_Categoria.DescripcionLocal

		DECLARE @MetricaDep1 DECIMAL(10,2) = (SELECT SUM(Depreciacion) FROM @T_DEPRECIACION WHERE Periodo = @Periodo1)
		DECLARE @MetricaDep2 DECIMAL(10,2) = (SELECT SUM(Depreciacion) FROM @T_DEPRECIACION WHERE Periodo = @Periodo2)
		DECLARE @MetricaDep3 DECIMAL(10,2) = (SELECT SUM(Depreciacion) FROM @T_DEPRECIACION WHERE Periodo = @Periodo3)

		DECLARE @TotalDep1 DECIMAL(10,2) = @MetricaDep1 / @MetricaKM1
		DECLARE @TotalDep2 DECIMAL(10,2) = @MetricaDep2 / @MetricaKM2
		DECLARE @TotalDep3 DECIMAL(10,2) = @MetricaDep3 / @MetricaKM3
		DECLARE @Depreciacion DECIMAL(10,2) = (@TotalDep1 + @TotalDep2 + @TotalDep3) / 3

		SELECT @Periodo1+': ' AS 'PERIODO_1', ISNULL(@TotalMtto1,0.00) AS 'MTTO_1', ISNULL(@TotalNeu1,0.00) AS 'NEU_1', @Periodo2+': ' AS 'PERIODO_2',
		ISNULL(@TotalMtto2,0.00) AS 'MTTO_2', ISNULL(@TotalNeu2,0.00) AS 'NEU_2', @Periodo3+': ' AS 'PERIODO_3', ISNULL(@TotalMtto3,0.00) AS 'MTTO_3',
		ISNULL(@TotalNeu3,0.00) AS 'NEU_3', ISNULL(@ManoObra,0.00) AS 'MANO_OBRA', ISNULL(@Depreciacion,0.00) AS 'DEPRECIACION'
	END

	IF (@Opcion = 4) BEGIN		-- LISTAR DATOS DE GASTOS Y PEAJES
		SELECT CONVERT(DECIMAL(10,2),ISNULL(SUM(X.PEAJE),0.00)) AS 'PEAJE' FROM
		(SELECT R.IDRUTA, R.CODIGO, R.DESCRIPCION AS 'RUTA', RT.IDTRAMO, T.Descripcion AS 'TRAMO', TP.IDPEAJE, P.Descripcion AS 'PEAJE_DESC',
		ISNULL(PC.Factor * 6,0) AS 'PEAJE'
		FROM OP_TR_RUTA R WITH(NOLOCK)
		LEFT JOIN OP_TR_RutaTramo RT WITH(NOLOCK) ON RT.IdRuta = R.IdRuta
		LEFT JOIN OP_TR_Tramo T WITH(NOLOCK) ON T.IdTramo = RT.IdTramo
		LEFT JOIN OP_TR_TramoPeaje TP WITH(NOLOCK) ON TP.IdTramo = T.IdTramo
		LEFT JOIN OP_TR_Peaje P ON P.IdPeaje = TP.IdPeaje
		LEFT JOIN OP_TR_PeajeCosto PC ON PC.IdPeaje = P.IdPeaje
		WHERE (R.Estado = 2) AND (RT.Estado = 2) AND (T.Estado = 2) AND (TP.Estado = 2) AND (P.Estado = 2) AND (PC.Estado = 2) AND (PC.TipoVehiculo = 1)
		AND (PC.SubTipoVehiculo = 1) AND (R.IDRUTA = @idRuta)) X
	END
END
