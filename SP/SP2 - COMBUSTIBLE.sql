
---------CREAR TABLA DE RUTAS DE VIAJES: ReportesApp_Combustible_RutaViajes (idRutaViajes INT, IdRuta INT, Codigo VARCHAR(15), Descripcion VARCHAR(80))

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 18-04-2023
-- Description:	LISTAR RUTAS DE VIAJES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Combustible_ListarRutaViajes]
@filtro VARCHAR(200)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT R.IdRuta AS 'idRutaViajes', R.IdRuta AS 'IdRuta', R.Codigo AS 'Codigo', R.Descripcion AS 'Descripcion'
	FROM OP_TR_Ruta R
	WHERE Estado = 2 AND (@filtro IS NULL OR R.Descripcion LIKE '%'+ @filtro + '%')
	ORDER BY R.IdRuta

    /*
	SELECT RV.idRutaViajes, RV.IdRuta, RV.Codigo, RV.Descripcion FROM ReportesApp_Combustible_RutaViajes RV
    WHERE (@filtro IS NULL OR RV.Descripcion LIKE '%'+ @filtro + '%')
    ORDER BY RV.idRutaViajes
	*/
END


---------CREAR TABLA DE RUTAS DE ABASTECIMIENTO: ReportesApp_Combustible_RutaAbastecimiento (idRutaAbastecimiento INT, Descripcion VARCHAR(100))
---------INSERTAR VALORES DEL EXCEL

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 20-04-2023
-- Description:	EDITAR RUTAS DE ABASTECIMIENTO
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Combustible_EditarRutaAbastecimiento]
@idRutaAbastecimiento INT,
@Descripcion VARCHAR(80)
AS
DECLARE @Exito VARCHAR(MAX)
BEGIN
	IF(@idRutaAbastecimiento = (SELECT R.idRutaAbastecimiento FROM ReportesApp_Combustible_RutasAsignadas R WHERE R.idRutaAbastecimiento = @idRutaAbastecimiento))
	BEGIN
		SET @Exito = 'Para actualizar esta ruta, primero debe desvincularla.' 
	END
	ELSE
	BEGIN
		UPDATE ReportesApp_Combustible_RutaAbastecimiento
		SET Descripcion = @Descripcion
		WHERE idRutaAbastecimiento = @idRutaAbastecimiento
		SET @Exito = 'Modificado correctamente.' 
	END
	SELECT @Exito AS 'Mensaje'
END

----------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 20-04-2023
-- Description:	ELIMINAR RUTAS DE ABASTECIMIENTO
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Combustible_EliminarRutaAbastecimiento]
@idRutaAbastecimiento INT
AS
DECLARE @Exito VARCHAR(MAX)
BEGIN
	IF(@idRutaAbastecimiento = (SELECT R.idRutaAbastecimiento FROM ReportesApp_Combustible_RutasAsignadas R WHERE R.idRutaAbastecimiento = @idRutaAbastecimiento))
	BEGIN
		SET @Exito = 'Para eliminar esta ruta, primero debe desvincularla.' 
	END
	ELSE
	BEGIN
		DELETE FROM ReportesApp_Combustible_RutaAbastecimiento
		WHERE idRutaAbastecimiento = @idRutaAbastecimiento
		SET @Exito = 'Eliminado correctamente.'
	END
	SELECT @Exito AS 'Mensaje'
END

---------CREAR TABLA DE RUTAS ASIGNADAS: ReportesApp_Combustible_RutasAsignadas (idRutaAsignada INT, idRutaViajes INT, idRutaAbastecimiento INT)

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 18-04-2023
-- Description:	ASIGNAR RUTAS DE ABASTECIMIENTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Combustible_AsignarRutaAbastecimiento]
@idRutaViajes INT,
@idRutaAbastecimiento INT
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT
DECLARE @idRuta INT
BEGIN
BEGIN TRAN
BEGIN TRY
	SET @idRuta = (SELECT IdRuta FROM OP_TR_Ruta WHERE IdRuta = @idRutaViajes)
	SET @correlativo = (SELECT MAX(idRutaAsignada) + 1 FROM ReportesApp_Combustible_RutasAsignadas)
	SET @correlativo = ISNULL(@correlativo,0) 
	INSERT INTO ReportesApp_Combustible_RutasAsignadas(idRutaAsignada,idRutaViajes,idRutaAbastecimiento)
		SELECT @correlativo, RV.IdRuta, RA.idRutaAbastecimiento FROM OP_TR_Ruta RV 
		INNER JOIN ReportesApp_Combustible_RutaAbastecimiento RA ON RV.IdRuta = @idRuta AND RA.idRutaAbastecimiento = @idRutaAbastecimiento
		SET @Exito = '0=Asignado Correctamente'

	/*
	SET @idRuta = (SELECT IdRuta FROM ReportesApp_Combustible_RutaViajes WHERE idRutaViajes = @idRutaViajes)
	SET @correlativo = (SELECT MAX(idRutaAsignada) + 1 FROM ReportesApp_Combustible_RutasAsignadas)
	SET @correlativo = ISNULL(@correlativo,0) 
	INSERT INTO ReportesApp_Combustible_RutasAsignadas(idRutaAsignada,idRutaViajes,idRutaAbastecimiento)
		SELECT @correlativo, RV.IdRuta, RA.idRutaAbastecimiento FROM ReportesApp_Combustible_RutaViajes RV 
		INNER JOIN ReportesApp_Combustible_RutaAbastecimiento RA ON RV.IdRuta = @idRuta AND RA.idRutaAbastecimiento = @idRutaAbastecimiento
		SET @Exito = '0=Asignado Correctamente'
	*/
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
SELECT @exito 'Mensaje'
END

----------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 18-04-2023
-- Description:	LISTAR RUTAS DE VIAJE ASIGNADAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Combustible_ListarRutasAsignadas]
@filtro VARCHAR(200)
AS
BEGIN
	SET NOCOUNT ON;
    SELECT R.idRutaAsignada AS 'ID', R.idRutaViajes AS 'IDRUTA', RV.Descripcion AS 'RUTA DE VIAJE', R.idRutaAbastecimiento AS 'IDRUTAAB', RA.Descripcion AS 'RUTA DE ABASTECIMIENTO'
    FROM ReportesApp_Combustible_RutasAsignadas R INNER JOIN OP_TR_Ruta RV ON RV.IdRuta = R.idRutaViajes
    INNER JOIN ReportesApp_Combustible_RutaAbastecimiento RA ON RA.idRutaAbastecimiento = R.idRutaAbastecimiento
    WHERE (@filtro IS NULL OR RA.Descripcion LIKE '%'+ @filtro + '%')

	/*
	SELECT R.idRutaAsignada AS 'ID', R.idRutaViajes AS 'IDRUTA', RV.Descripcion AS 'RUTA DE VIAJE', R.idRutaAbastecimiento AS 'IDRUTAAB', RA.Descripcion AS 'RUTA DE ABASTECIMIENTO'
    FROM ReportesApp_Combustible_RutasAsignadas R INNER JOIN ReportesApp_Combustible_RutaViajes RV ON RV.IdRuta = R.idRutaViajes
    INNER JOIN ReportesApp_Combustible_RutaAbastecimiento RA ON RA.idRutaAbastecimiento = R.idRutaAbastecimiento
    WHERE (@filtro IS NULL OR RA.Descripcion LIKE '%'+ @filtro + '%')
	*/
END

----------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 20-04-2023
-- Description:	DESVINCULAR RUTAS DE VIAJE ASIGNADAS
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Combustible_EliminarRutasAsignadas]
@idRutaAsignada INT
AS
BEGIN
	SET NOCOUNT ON;
    DELETE FROM ReportesApp_Combustible_RutasAsignadas WHERE idRutaAsignada = @idRutaAsignada
END

----------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 20-04-2023
-- Description:	FILTRAR RUTAS ASIGNADAS POR ID
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Combustible_FiltrarRutasAsignadas]
@IdRuta INT
AS
BEGIN
	SELECT R.idRutaAsignada AS 'ID', RV.IdRuta AS 'IDRUTA', RV.Descripcion AS 'RUTA DE VIAJE', RA.idRutaAbastecimiento AS 'IDRUTAAB', RA.Descripcion AS 'RUTA DE ABASTECIMIENTO'
    FROM ReportesApp_Combustible_RutasAsignadas R INNER JOIN OP_TR_Ruta RV ON RV.IdRuta = R.idRutaViajes
    INNER JOIN ReportesApp_Combustible_RutaAbastecimiento RA ON RA.idRutaAbastecimiento = R.idRutaAbastecimiento
    WHERE RV.IdRuta = @IdRuta

	/*
	SELECT R.idRutaAsignada AS 'ID', RV.IdRuta AS 'IDRUTA', RV.Descripcion AS 'RUTA DE VIAJE', RA.idRutaAbastecimiento AS 'IDRUTAAB', RA.Descripcion AS 'RUTA DE ABASTECIMIENTO'
    FROM ReportesApp_Combustible_RutasAsignadas R INNER JOIN ReportesApp_Combustible_RutaViajes RV ON RV.IdRuta = R.idRutaViajes
    INNER JOIN ReportesApp_Combustible_RutaAbastecimiento RA ON RA.idRutaAbastecimiento = R.idRutaAbastecimiento
	INNER JOIN OP_TR_RUTA RT ON RT.IdRuta = R.idRutaViajes
    WHERE RV.IdRuta = @IdRuta
	*/
END

---------CREAR TABLA ReportesApp_Combustible_ReporteRutaAbastecimiento (idReporteRA INT, fechaInicio DATETIME, codigoViaje VARCHAR(20), IdRuta INT, codigoRV VARCHAR(50), DescripcionRV VARCHAR(200), idRutaAbastecimiento INT, DescripcionRA VARCHAR(200), fechaAnexa DATETIME)

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 18-04-2023
-- Description:	INSERTAR REPORTES CON RUTAS DE ABASTECIMIENTO
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Combustible_InsertarTablaRutasAbastecimiento]
@usuario VARCHAR(30),
@idViaje INT,
@fechaDespacho DATETIME,
@xmlRutas VARCHAR(MAX) = ''
AS
BEGIN
BEGIN TRAN
BEGIN TRY
	DECLARE @docHandle INT
	DECLARE @idReporteRA INT
	DECLARE @TEMP_EPPS TABLE(
		IdRuta INT,
		DescripcionRV VARCHAR(100),
		idRutaAbastecimiento INT,
		DescripcionRA VARCHAR(100))
	IF @xmlRutas <> ''
	BEGIN
		EXEC sp_xml_preparedocument @docHandle OUTPUT, @xmlRutas;
		INSERT INTO @TEMP_EPPS
		SELECT * FROM OPENXML(@docHandle, N'/r/surtidor')
		WITH (IdRuta INT, DescripcionRV VARCHAR(100), idRutaAbastecimiento INT, DescripcionRA VARCHAR(100));
		EXEC sp_xml_removedocument @docHandle;
	END
	SET @idReporteRA = (SELECT MAX(idReporteRA) FROM ReportesApp_Combustible_ReporteRutaAbastecimiento)
	SET @idReporteRA = ISNULL(@idReporteRA, 0)
	INSERT INTO ReportesApp_Combustible_ReporteRutaAbastecimiento(idReporteRA, fechaInicio, codigoViaje, IdRuta, codigoRV, DescripcionRV, idRutaAbastecimiento, DescripcionRA, fechaAnexa)
		SELECT @idReporteRA + ROW_NUMBER() OVER(ORDER BY OP_TR_VIAJE.IDVIAJE ASC), @fechaDespacho, OP_TR_VIAJE.CODIGO, OP_TR_RUTA.IDRUTA, OP_TR_RUTA.Codigo, OP_TR_RUTA.Descripcion,
		E.idRutaAbastecimiento, E.DescripcionRA, GETDATE()
		FROM OP_TR_VIAJE LEFT JOIN OP_TR_RUTA ON OP_TR_RUTA.IDRUTA = OP_TR_VIAJE.IDRUTA
		LEFT JOIN @TEMP_EPPS E ON E.IdRuta = OP_TR_RUTA.IDRUTA
		WHERE OP_TR_VIAJE.IDVIAJE = @idViaje
	IF @usuario = 'SCHAVEZ'
	BEGIN
		ROLLBACK
	END
END TRY

BEGIN CATCH
	ROLLBACK
END CATCH

IF @@TRANCOUNT > 0 
	COMMIT;
    --ROLLBACK
END

----------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 24-04-2023
-- Description:	FILTRAR REPORTES DE RUTAS AB
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Combustible_FiltrarReportesAbastecimiento]
@fechaDespacho DATETIME,
@codigoViaje VARCHAR(20)
AS
BEGIN
    SELECT RA.idRutaAbastecimiento AS 'ID', RA.DescripcionRA AS 'RUTA DE ABASTECIMIENTO' FROM ReportesApp_Combustible_ReporteRutaAbastecimiento RA
    WHERE RA.codigoViaje = @codigoViaje AND CONVERT(DATE,RA.fechaInicio) = CONVERT(DATE,@fechaDespacho)
END

----------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 22-04-2023
-- Description:	LISTAR REPORTE DE DESPACHOS   
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Combustible_ListarReporteDespachos]
	@FECHINI DATETIME,
	@FECHFIN DATETIME,
	@CONDUCTOR VARCHAR(50),
	@TRACTO VARCHAR(10)
	--WITH RECOMPILE  --SIRVE PARA RECALCULAR LA MEJOR OPTIMIZACION DE CARGA (NO BUSCA EN CACHÉ)
AS
DECLARE @FINICIO DATETIME = @FECHINI
DECLARE @FFIN DATETIME = @FECHFIN
DECLARE @CONDUCTOR1 VARCHAR(50) = @CONDUCTOR
DECLARE @TRACTO1 VARCHAR(10) = @TRACTO
	BEGIN
		SELECT DISTINCT COMBUSTIBLE.FechaDespacho AS DESPACHO
		   ,V.CODIGO,CASE WHEN RO.Descripcion IS NULL THEN 'TOLVAS' ELSE RO.Descripcion END  AS 'OPERACION',  -- Al no existir la operacion en la tabla de tolvas y no tener relacion con la tabla de Previaje cuando en previaje sea null  se mostrará como TOLVA (TEMPORALMENTE)  Updated em Chavez (11.11.2022)
			CASE WHEN v.Estado = 1 THEN 'PENDIENTE'
				 WHEN V.Estado = 2 THEN 'PROGRAMADO'
				 WHEN V.Estado = 3 THEN 'EJECUCION'
				 WHEN V.Estado = 4 THEN 'COMPLETADO'
				 WHEN V.ESTADO = 8 THEN 'CANCELADO'
				 WHEN V.Estado = 9 THEN 'ANULADO' ELSE '' END AS 'ESTADO',
			VEHICULO.NumeroPlaca AS PLACA,
			ME_MAQUINAMARCA.DESCRIPCION AS MARCA,
			MAQUINA.Modelo as 'MODELO',
			MAQUINA.AnoAdquisicion AS AÑO,
			carreta.NumeroPlaca as SEMIRREMOLQUE,
			/*CASE WHEN T.ChoferAsignadoGeotab IS null or T.ChoferAsignadoGeotab  = '' THEN*/ CONDUCTOR.Nombre
			--	ELSE T.ChoferAsignadoGeotabEND AS
			 CONDUCTOR,
			OP_TR_Ruta.Descripcion AS RUTA,
			AB.idRutaAbastecimiento AS 'ID_RA',
			AB.DescripcionRA AS 'RUTA ABASTECIMIENTO',
				CPUKilometraje AS KM,COMBUSTIBLE.CantidadDespachada AS CONSUMO_FISICO
				,DESPACHO.ComsumoSW AS CONSUMO_SW--COMBUSTIBLE.CPUCantidad AS CONSUMO_SW
				,CASE WHEN COMBUSTIBLE.CPUCantidad=0 THEN CAST(100.00 AS VARCHAR(20)) ELSE  CAST(CAST((CantidadDespachada-CPUCantidad)/CPUCantidad*100 AS DECIMAL(10,2)) AS VARCHAR(20)) END AS '% DIF. CSF vs CSW'
				,CAST(CPUKilometraje/CantidadDespachada AS DECIMAL(10,2)) AS 'REND KM/GL'
				,DESPACHO.[Dif.CF_vs_SW]  AS DIF--,CAST(CantidadDespachada-CPUCantidad AS DECIMAL(10,2)) AS DIF
			,COMBUSTIBLE.IdCarga
			,COMBUSTIBLE.IdViaje
			,COMBUSTIBLE.TipoDocumentoSalida +'-'+ COMBUSTIBLE.NumeroDocumentoSalida AS NumeroDocumentoSalida	    	   
		    , T.Ticket AS Ticket_SURTIDOR 
		    , T.NroTicketPreViaje AS CodigoPreviaje
		    , T.Fecha Ticket_Fecha 
		    , T.ReservaControl TELEMETRIA
		    , T.UreaControl Urea
		    , T.VueltasViaje Vueltas 
		    , T.Grifo
		    , T.Surtidor
		    ,T.UserAnexaViaje as 'Usuario Anexa'
		    ,t.FechaAnexa
		FROM OP_TR_VIAJE v WITH(NOLOCK) 
			INNER JOIN OP_TR_RUTA WITH(NOLOCK) ON OP_TR_RUTA.IDRUTA = v.IDRUTA 
			INNER JOIN OP_TR_CONDUCTOR AS CONDUCTOR WITH(NOLOCK)ON CONDUCTOR.IDCONDUCTOR = v.IDCONDUCTOR 
			LEFT JOIN OP_TR_CONDUCTOR AS AYUDANTE WITH(NOLOCK)ON AYUDANTE.IDCONDUCTOR = v.IDAYUDANTE 
			INNER JOIN OP_TR_VEHICULO AS VEHICULO WITH(NOLOCK) ON VEHICULO.IDVEHICULO = v.IDVEHICULO 
			INNER JOIN ME_MAQUINAMARCA WITH(NOLOCK) ON ME_MAQUINAMARCA.MARCA = VEHICULO.MARCA
			INNER JOIN GE_VARIOS AS TIPOVEHICULO WITH(NOLOCK)ON TIPOVEHICULO.SECUENCIAL = VEHICULO.TIPOVEHICULO  --LEFT
																		AND TIPOVEHICULO.CODIGOTABLA = 'TIPOVEHICULO' 
			LEFT JOIN OP_TR_VEHICULO AS CARRETA WITH(NOLOCK) ON CARRETA.IDVEHICULO = v.IDCARRETA 
			INNER JOIN OP_GE_OTDETALLE WITH(NOLOCK) ON v.IDVIAJE = OP_GE_OTDETALLE.IDVIAJE
														AND OP_GE_OTDETALLE.TipoMovimiento = 'TRA' 
			INNER JOIN OP_GE_OTPRODUCTO WITH(NOLOCK) ON OP_GE_OTDETALLE.IDOT = OP_GE_OTPRODUCTO.IDOT 
															AND OP_GE_OTDETALLE.LINEAPRODUCTO = OP_GE_OTPRODUCTO.LINEA
															AND OP_GE_OTPRODUCTO.TipoMovimiento = 'TRA' 
			LEFT JOIN OP_GE_OT WITH(NOLOCK) ON OP_GE_OT.IDOT = OP_GE_OTPRODUCTO.IDOT 
												AND OP_GE_OT.TipoMovimiento = 'TRA' 
			INNER JOIN OP_GE_CONTRATODETALLE WITH(NOLOCK) ON OP_GE_CONTRATODETALLE.IDCONTRATO = OP_GE_OTPRODUCTO.IDCONTRATO 
				AND OP_GE_CONTRATODETALLE.GRUPOSERVICIOOP = 'T' AND ((ISNULL(OP_GE_CONTRATODETALLE.INDGENERAL,1) = 1 
				AND OP_GE_CONTRATODETALLE.LINEAPRODUCTO = OP_GE_OTPRODUCTO.LINEACONTRATO) 
				OR ISNULL(OP_GE_CONTRATODETALLE.INDGENERAL,1) = 2) 		
			INNER JOIN OP_TR_CargaCombustible AS COMBUSTIBLE WITH(NOLOCK) ON COMBUSTIBLE.IdViaje = v.IdViaje	  
			LEFT JOIN ReportesApp_Operacion_Previaje_Registros RP WITH(NOLOCK) ON CAST(RP.CodViaje AS VARCHAR(12)) = v.Codigo AND RP.Anio = YEAR(V.FechaProgramada)
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones RO WITH(NOLOCK) ON RO.IdOperacion = RP.TipoProgramacion    
			LEFT JOIN ReportesApp_Operacion_GuiasImportadasAltra OG WITH(NOLOCK) ON OG.CodigoViaje = V.Codigo AND (OG.Fecha  BETWEEN @FINICIO AND @FFIN) 
		    INNER JOIN (
							SELECT CAST(TK.Ticket AS VARCHAR(20)) Ticket, TK.NroTicketPreViaje, TK.Placa, TK.IdViaje, NT.IdCarga, 
									'PROPIO' AS Grifo, 'SURTIDOR TRUJILLO' Surtidor,tk.UserAnexaViaje,tk.FHAnexaViaje as 'FechaAnexa',
									TK.ReservaControl, TK.UreaControl, TK.VueltasViaje, TK.Fecha, TK.ChoferAsignadoGeotab, NT.FechaDespacho, NT.NumeroDocumento
							FROM ReportesApp_Combustible_Nexo_Ticket_CargaCombustible_Kardex NT WITH(NOLOCK)
								INNER JOIN ReportesApp_Combustible_TicketsSurtidor TK  WITH(NOLOCK) ON TK.Ticket = NT.Ticket
																										AND TK.NroTicketPreViaje = NT.NroTicketPreViaje
																										AND TK.Placa = NT.Placa
																										AND TK.IDViaje = NT.IDViaje
							WHERE TK.IDViaje IS NOT NULL
								AND NT.IDViaje IS NOT NULL
								AND NT.Tipo = 'P'
								AND (NT.FechaDespacho BETWEEN @FECHINI AND @FECHFIN) 
								
							UNION
							SELECT CONVERT(VARCHAR(20),RIGHT('000000000' + LTRIM(RTRIM(TT.Ticket)),9)), TT.Codigo NroTicketPreViaje, TT.Placa, TT.IdViaje,  NT.IdCarga, 
							'TERCERO'  Grifo, TT.Empresa Surtidor, tt.UsuarioAnexaViaje,TT.FHAnexaViaje AS 'FechaAnexa',
								TT.ReservaControl, TT.UreaControl, TT.VueltasViaje, TT.FechaDespacho AS Fecha, TT.ChoferAsignadoGeotab, NT.FechaDespacho, NT.NumeroDocumento 
							FROM ReportesApp_Combustible_Nexo_Ticket_CargaCombustible_Kardex NT  WITH(NOLOCK)
								INNER JOIN ReportesApp_Combustible_TicketsTercero TT  WITH(NOLOCK) ON TT.Ticket = NT.Ticket
																									AND TT.Codigo = NT.NroTicketPreViaje
																									AND TT.Placa = NT.Placa
																									AND TT.IDViaje = NT.IDViaje
							WHERE TT.IDViaje IS NOT NULL
								AND NT.IDViaje IS NOT NULL
								AND NT.Tipo = 'T'
								AND (NT.FechaDespacho BETWEEN @FINICIO AND @FFIN) 
						) T ON T.IDViaje = COMBUSTIBLE.IdViaje
							AND T.IdCarga = COMBUSTIBLE.IdCarga
							AND T.NumeroDocumento = CONVERT(INT, COMBUSTIBLE.NumeroDocumentoSalida)
		    LEFT JOIN OP_TR_DespachoCombustible DESPACHO ON DESPACHO.IdCarga=T.IdCarga --20/09/2022: Joel: Se agregó nuevo Left para traer ConsumoSW y DIF (TELEMETRIA)
		    LEFT JOIN ME_Maquina AS MAQUINA WITH(NOLOCK) ON MAQUINA.MaquinaCodigo = VEHICULO.NumeroPlaca
		    LEFT JOIN ReportesApp_Combustible_ReporteRutaAbastecimiento AB ON V.Codigo = AB.codigoViaje AND V.IdRuta = AB.IdRuta AND CONVERT(DATE,COMBUSTIBLE.FechaDespacho) = CONVERT(DATE,AB.fechaInicio)
		WHERE COMBUSTIBLE.FechaDespacho BETWEEN @FINICIO AND @FFIN
			AND COMBUSTIBLE.Estado = 2 
			AND CONDUCTOR.Nombre LIKE '%'+ @CONDUCTOR1 +'%' 
			AND REPLACE(REPLACE(VEHICULO.NumeroPlaca,'-',''),'.','') LIKE '%'+REPLACE(REPLACE(@TRACTO1,'-',''),'.','') +'%'
			ORDER BY COMBUSTIBLE.FechaDespacho DESC
END

