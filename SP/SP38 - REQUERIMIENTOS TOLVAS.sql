
-- MODIFICAR ReportesApp_Operaciones_Previajes_Tolvas_Cabecera

-- CREAR ReportesApp_Operaciones_Previajes_Tolvas_TipoProceso

------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 22-04-2023
-- Description:	LISTAR OPERACIONES DE TOLVAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Previajes_Tolvas_ListarOperaciones]
AS
BEGIN
	SELECT idTipoProceso, Descripcion
	FROM ReportesApp_Operaciones_Previajes_Tolvas_TipoProceso
END

------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 19-06-2023
-- Description:	INSERTAR TOLVAS DE PREVIAJES
-- =============================================
/*
exec ReportesApp_Operaciones_Previajes_Tolvas_Insertar @IdOT=22392,@Tarifa=4.800000,@idCliente=1027,@Cliente=N'AVICOLA YUGOSLAVIA S.A.C.',@idRemitente=1027,@Remitente=N'AVICOLA YUGOSLAVIA S.A.C.',@idPartida=28,@DireccionPartida=N'CALLE CORDOVA S/N',@idDestinatario=1027,@Destinatario=N'AVICOLA YUGOSLAVIA S.A.C.',@idDestino=27,@DireccionDestino=N'KM 557 PAN NORTE  MOCHE - TRUJILLO',@FechaProgramacion='2023-09-15 09:39:43.830',@idProducto=661,@Producto=N'MAIZ A GRANEL',@Ruta=N'PTO.SALAVERRY - MOCHE',@idRuta=594,
@xmlPlacaConductor= '<r>
  <d idPlaca="77" Placa="T4T895" idCarreta="924" Carreta="T9K998" idConductor="11729" Conductor="GUTIERREZ ASMAT, EDISON RICARDO" TarjetaTracto="131702360" TarjetaCarreta="131702369" Guias="0" />
  <d idPlaca="81" Placa="T4T902" idCarreta="413" Carreta="T9L974" idConductor="11728" Conductor="LEON BECERRA, JUAN CARLOS" TarjetaTracto="131702351" TarjetaCarreta="131702373" Guias="0" />
  <d idPlaca="60" Placa="T2A915" idCarreta="746" Carreta="T9K999" idConductor="480" Conductor="QUIÑONES ISIDRO, MANUEL FRIDOLINO" TarjetaTracto="13M22000325E" TarjetaCarreta="131702379" Guias="0" />
  <d idPlaca="82" Placa="T4T903" idCarreta="5366" Carreta="TLE977" idConductor="154" Conductor="PLAZA GUERRA, JULIO CESAR" TarjetaTracto="131702361" TarjetaCarreta="13M22000739E" Guias="0" />
  <d idPlaca="75" Placa="T3M854" idCarreta="5114" Carreta="TKJ999" idConductor="10547" Conductor="RUIZ FLORES, WILMER ALFONSO" TarjetaTracto="13M22000220E" TarjetaCarreta="13M21001641E" Guias="0" />
  <d idPlaca="79" Placa="T4T897" idCarreta="5112" Carreta="TKJ996" idConductor="8728" Conductor="MIRANDA LOPEZ, PEDRO WILMAR" TarjetaTracto="13M22000222E" TarjetaCarreta="13M21002226E" Guias="0" />
  <d idPlaca="64" Placa="T2A923" idCarreta="5227" Carreta="TKK988" idConductor="9845" Conductor="PAREDES ZAVALETA JOSE ALBERTO" TarjetaTracto="13M22000407E" TarjetaCarreta="13M21002231E" Guias="0" />
  <d idPlaca="47" Placa="T3B836" idCarreta="5256" Carreta="TKJ998" idConductor="11049" Conductor="SEVILLA CASTRO, CESAR AUGUSTO" TarjetaTracto="131702317" TarjetaCarreta="13M21002227E" Guias="0" />
  <d idPlaca="76" Placa="T3M889" idCarreta="1289" Carreta="TAX980" idConductor="10592" Conductor="VALDEZ ALCANTARA, BALTAZAR" TarjetaTracto="13M22000221E" TarjetaCarreta="13M22000639E" Guias="0" />
  <d idPlaca="2189" Placa="T8B947" idCarreta="263" Carreta="T4W972" idConductor="32" Conductor="GUTIERREZ CENTENO, JUAN YSAAC" TarjetaTracto="13M21000943E" TarjetaCarreta="13M21001736E" Guias="0" />
  <d idPlaca="58" Placa="T2A909" idCarreta="5115" Carreta="TKK972" idConductor="10657" Conductor="FLORES CARRION JOEL MARTIN" TarjetaTracto="13M22000216E" TarjetaCarreta="13M21002230E" Guias="0" />
</r>',@UMUso=N'TN',@Tiempo=N'2.000000',@Distancia=N'18.000000',@Usuario=N'JHOSELINC'

*/
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Previajes_Tolvas_Insertar]
@IdOT INT,
@idCliente INT,
@Cliente VARCHAR(100),
@Tarifa DECIMAL(16,6),
@idRemitente INT,
@Remitente VARCHAR(150),
@idPartida INT,
@DireccionPartida VARCHAR(200),
@idDestinatario INT,
@Destinatario VARCHAR(150),
@idDestino INT,
@DireccionDestino VARCHAR(200),
@FechaProgramacion DATE,
@idProducto INT,
@Producto VARCHAR(200),
@idRuta INT,
@Ruta VARCHAR(200),
@xmlPlacaConductor VARCHAR(MAX),
@Usuario VARCHAR(20),
@UMUso VARCHAR(3),
@Distancia VARCHAR(12),
@Tiempo VARCHAR(10),
@TipoProceso VARCHAR(50),
@Motonave VARCHAR(100)
AS
DECLARE @UnidadEnUso VARCHAR(12)
DECLARE @Exito VARCHAR(MAX)
DECLARE @UltimaOT INT
DECLARE @idoc INT
DECLARE @correlativo INT
DECLARE @Anio INT
DECLARE @NroTicket INT
DECLARE @Español VARCHAR(200)
DECLARE @TEMP_EPPS TABLE(idPlaca INT,
						 Placa VARCHAR(20),
						 idCarreta INT,
						 Carreta VARCHAR(20),
						 idConductor INT,
						 Conductor VARCHAR(250),
						 TarjetaCirculacionTracto VARCHAR(15),
						 TarjetaCirculacionCarreta VARCHAR(15))

IF EXISTS (SELECT oc.IdPersona	FROM ReportesApp_Operacion_ConductoresBloqueados B 
INNER JOIN  OP_TR_Conductor OC ON B.IDPersona = OC.IdPersona
INNER JOIN @TEMP_EPPS T ON T.idConductor = OC.IdConductor) BEGIN
	SET @Exito = '-1=Conductor'+ (SELECT TOP 1 OC.Descripcion FROM ReportesApp_Operacion_ConductoresBloqueados B 
								  INNER JOIN OP_TR_Conductor OC ON B.IDPersona = OC.IdPersona
								  INNER JOIN @TEMP_EPPS T ON T.idConductor = OC.IdConductor) +' se encuentra bloqueado, comunicarse con Auditoria'
	GOTO Terminar
END

SET @Español = '<?xml version="1.0" encoding="ISO-8859-1"?> '
SET @xmlPlacaConductor = @Español+' '+ @xmlPlacaConductor

IF(@xmlPlacaConductor IS NOT NULL) BEGIN
	EXEC sp_xml_preparedocument @idoc OUTPUT, @xmlPlacaConductor
	INSERT INTO @TEMP_EPPS(idPlaca, Placa, idCarreta, Carreta, idConductor, Conductor,TarjetaCirculacionTracto,TarjetaCirculacionCarreta)
	SELECT * FROM OPENXML(@idoc,'/r/d',1)
	WITH (idPlaca INT, Placa VARCHAR(20), idCarreta INT, Carreta VARCHAR(20), idConductor INT, Conductor VARCHAR(250),TarjetaTracto VARCHAR(15),TarjetaCarreta VARCHAR(15));
	EXEC sp_xml_removedocument @idoc;
END

SET @correlativo = (SELECT MAX(idPreviajeTolvas) FROM ReportesApp_Operaciones_Previajes_Tolvas_Cabecera WHERE Anio = YEAR(GETDATE()))
SET @correlativo = ISNULL(@correlativo,0) + 1

BEGIN TRAN
BEGIN TRY
	IF EXISTS (SELECT * FROM ReportesApp_Operaciones_Previajes_Tolvas_Cabecera WHERE IdOT = @IdOT AND Estado = 'PROGRAMADO') BEGIN
		SET @Exito = '-77=La ot: '+ CONVERT(VARCHAR(8),@IdOT) + ' esta asignada a una operacion programada, cerrar la programacion '+ (SELECT TOP 1 CONVERT(VARCHAR(8),NroTicket) FROM ReportesApp_Operaciones_Previajes_Tolvas_Cabecera WHERE IdOT = @IdOT AND Estado = 'PROGRAMADO')  +' para continuar'
		ROLLBACK
		GOTO Terminar
	END
	
	SET @UnidadEnUso = (SELECT TOP 1 T.Placa  
						FROM ReportesApp_Operaciones_Previajes_Tolvas_Cabecera C 
							  INNER JOIN ReportesApp_Operaciones_Previajes_Tolvas_Detalle D ON C.idPreviajeTolvas = D.idPreviajeTolvas 
							  INNER JOIN @TEMP_EPPS T ON T.idPlaca = d.idPlaca
						WHERE C.Estado = 'PROGRAMADO')
	
	
	/*IF EXISTS (SELECT *
			   FROM ReportesApp_Operaciones_Previajes_Tolvas_Cabecera C 
					  INNER JOIN ReportesApp_Operaciones_Previajes_Tolvas_Detalle D ON C.idPreviajeTolvas = D.idPreviajeTolvas 
					  INNER JOIN @TEMP_EPPS T ON T.idPlaca = d.idPlaca
			   WHERE  C.Estado = 'PROGRAMADO' )
		BEGIN
		
			SET @Exito = '-77=La Unidad: '+ @UnidadEnUso + ' esta asignada a una operacion programada, cerrar la programacion '+ (SELECT TOP 1 CONVERT(VARCHAR(8),NroTicket) 
																																  FROM ReportesApp_Operaciones_Previajes_Tolvas_Cabecera 
																																  WHERE Estado = 'PROGRAMADO')  +' para continuar'
			ROLLBACK
			GOTO Terminar
		
		END*/
	

	SET @Anio = YEAR(GETDATE())
	SET @NroTicket = CONVERT(INT,('1' + CONVERT(VARCHAR(8),RIGHT(YEAR(GETDATE()),2))+CONVERT(VARCHAR(8),@correlativo)))

	INSERT INTO ReportesApp_Operaciones_Previajes_Tolvas_Cabecera(idPreviajeTolvas,Anio,NroTicket,IdOT,Tarifa,idCliente,Cliente,idRemitente,Remitente,idPartida,DireccionPartida,idDestinatario,Destinatario,
	idDestino, DireccionDestino, FechaProgramacion, idProducto,Producto, idRuta,Ruta, Estado, UsuarioCreacion, FechaCreacion, UltimoUsuario, UltimaModificacion,UMUso,Distancia,Tiempo,TipoProceso,Motonave)
	VALUES(@correlativo ,YEAR(GETDATE()), CONVERT(INT,('1' + CONVERT(VARCHAR(8),RIGHT(YEAR(GETDATE()),2))+CONVERT(VARCHAR(8),@correlativo))), @IdOT,@Tarifa,@idCliente,@Cliente, @idRemitente, @Remitente, @idPartida, @DireccionPartida, @idDestinatario,
	@Destinatario, @idDestino, @DireccionDestino, @FechaProgramacion,@idProducto ,@Producto,@idRuta ,@Ruta, 'PROGRAMADO', @Usuario, GETDATE(), @Usuario, GETDATE(),@UMUso,@Distancia,@Tiempo, @TipoProceso, @Motonave)
		   
	INSERT INTO ReportesApp_Operaciones_Previajes_Tolvas_Detalle(idPreviajeTolvas, Anio, idPlaca, Placa, idCarreta, Carreta, idConductor, Conductor,TarjetaCirculacionTracto,TarjetaCirculacionCarreta)
	SELECT @correlativo, YEAR(GETDATE()), E.idPlaca, E.Placa, E.idCarreta, E.Carreta, E.idConductor, E.Conductor,TarjetaCirculacionTracto,TarjetaCirculacionCarreta
	FROM @TEMP_EPPS E
	
	SET @Exito = '0 = Previaje de Tolva añadida.'

	--select * from ReportesApp_Operaciones_Previajes_Tolvas_Cabecera
	--select * from ReportesApp_Operacion_Previaje_Registros
	--SELECT * FROM ReportesApp_Operaciones_Previajes_Tolvas_Detalle
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
IF LEFT(@Exito,1)='-' OR cast(left(@Exito,1) AS INT)<0 BEGIN
	DECLARE @storedProcedureName VARCHAR(100)
	SELECT @storedProcedureName = OBJECT_NAME(@@PROCID)
	SET @exito = @exito + CHAR(13)+CHAR(13)+'SP Name: '+@storedProcedureName
END

SELECT @exito 'exito',@correlativo 'idPreviajeTolvas',@Anio 'Anio','PROGRAMADO' as 'Estado',@NroTicket 'NroTicket'

----------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:,,<Sem Chavez>
-- Create date: <01-07-2023>
-- Description:,<Listar Viajes por programacion y placa>
-- =============================================
/*
exec ReportesApp_Operaciones_ListarViajesTolvas @NroTicket=1239,@idPreviajeTolvas=2023,@anio=9,@placa=NULL,@conductor=NULL,@anulados=0
exec ReportesApp_Operaciones_ListarViajesTolvas @NroTicket=1231,@idPreviajeTolvas=1,@anio=2023,@placa=NULL,@conductor=NULL,@anulados=0
exec ReportesApp_Operaciones_ListarViajesTolvas @NroTicket=12342,@idPreviajeTolvas=11,@anio=2023,@placa=NULL,@conductor=NULL,@anulados=1
*/
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ListarViajesTolvas]
@NroTicket INT,
@idPreviajeTolvas INT,
@anio INT,
@placa VARCHAR(6),
@conductor varchar(150),
@anulados INT
AS
BEGIN
IF @anulados = 0 BEGIN
	SELECT DISTINCT T.idPreviajeTolvas,T.Anio,T.NroTicket,C.idCliente,C.Cliente,T.idOT,T.idGuiaElectronica,ISNULL(T.LineaOT,1) AS 'LineaOT',ISNULL(G.EstadoGuia,'TERCERO') AS 'EstadoGuia',ISNULL(G.EstadoSunat,'TERCERO') AS 'EstadoSunat',T.idViaje,T.Viaje,T.FechaViaje,T.TipoGuia,T.Serie,T.Numero,T.TipoRem as 'TipoGuiaRem',T.GuiaRemision,T.idUnidad,V.IndTercero,
		   T.Tracto,T.idCarreta,T.Carreta,T.Peso,T.PesoCliente,T.Merma,T.idRuta,T.Ruta, C.TipoProceso 'Operacion', C.Motonave, c.idPartida,c.idDestino,T.NroTicketPesajeCliente,T.idProducto,T.Producto,T.UMBase,T.Distancia,T.Tiempo ,T.NombresConductor+', '+T.ApellidosConductor AS 'Conductor',
		   G.FechaInicio_Traslado as 'FechaGuia',T.idConductor,T.NombresConductor,T.ApellidosConductor,T.UsuarioRegistro,T.FechaRegistro,DT.DocumentoRelacion AS 'Facturado',T.Observacion,T.OrdenServicio,T.UsuarioRegistraPeso,T.FechaRegistraPeso,1 as idTipoProgramacion,T.EstadoViaje,T.CodProgramaProceso
	FROM ReportesApp_Operaciones_Viajes_Tolvas T 
			INNER JOIN ReportesApp_Operaciones_Previajes_Tolvas_Cabecera C ON T.idPreviajeTolvas = C.idPreviajeTolvas AND T.Anio = C.Anio
		    INNER JOIN OP_TR_Vehiculo V ON V.IdVehiculo = T.idUnidad
			LEFT JOIN ReportesApp_Operaciones_GuiasElectronicas G ON SerieGuia = T.Serie AND G.NumeroGuia = T.Numero AND G.TipoGuia = T.TipoGuia
			LEFT JOIN OP_GE_OTDetalle DT ON DT.IdViaje = T.IdViaje
	WHERE T.NroTicket = @NroTicket AND (T.Tracto like '%'+@placa+'%'  OR @placa IS NULL) AND (T.NombresConductor+', '+T.ApellidosConductor like '%'+@conductor+'%' OR @conductor IS NULL) 
		    AND T.EstadoViaje != 'ANULADO'
END
ELSE IF @anulados = 1 BEGIN
	SELECT DISTINCT T.idPreviajeTolvas,T.Anio,T.NroTicket,C.idCliente,C.Cliente,T.idGuiaElectronica,T.idOT,ISNULL(T.LineaOT,1) AS 'LineaOT',ISNULL(G.EstadoGuia,'TERCERO')AS 'EstadoGuia',ISNULL(G.EstadoSunat,'TERCERO') AS 'EstadoSunat',T.idViaje,T.Viaje,T.FechaViaje,T.TipoGuia,T.Serie,T.Numero,T.TipoRem,T.GuiaRemision,T.idUnidad,V.IndTercero,
		   T.Tracto,T.idCarreta,T.Carreta,T.Peso,T.PesoCliente,T.Merma,T.idRuta,T.Ruta,C.TipoProceso 'Operacion', C.Motonave,c.idPartida,c.idDestino,T.NroTicketPesajeCliente,T.idProducto,T.Producto,T.UMBase,T.Distancia,T.Tiempo ,T.NombresConductor+', '+T.ApellidosConductor AS 'Conductor',
		   G.FechaInicio_Traslado as 'FechaGuia',T.idConductor,T.NombresConductor,T.ApellidosConductor,DT.DocumentoRelacion AS 'Facturado',T.UsuarioRegistro,T.FechaRegistro,T.Observacion,T.OrdenServicio,T.UsuarioRegistraPeso,T.FechaRegistraPeso,1 as idTipoProgramacion,T.EstadoViaje,T.CodProgramaProceso
	FROM  ReportesApp_Operaciones_Viajes_Tolvas T 
			INNER JOIN ReportesApp_Operaciones_Previajes_Tolvas_Cabecera C ON T.idPreviajeTolvas = C.idPreviajeTolvas AND T.Anio = C.Anio
		    INNER JOIN OP_TR_Vehiculo V ON V.IdVehiculo = T.idUnidad
			LEFT JOIN ReportesApp_Operaciones_GuiasElectronicas G ON SerieGuia = T.Serie AND G.NumeroGuia = T.Numero AND G.TipoGuia = T.TipoGuia
			LEFT JOIN OP_GE_OTDetalle DT ON DT.IdViaje = T.IdViaje
	WHERE T.NroTicket = @NroTicket AND (T.Tracto like '%'+@placa+'%'  OR @placa IS NULL) AND (T.NombresConductor+', '+T.ApellidosConductor like '%'+@conductor+'%' OR @conductor IS NULL) 
		    AND T.EstadoViaje IN ('ANULADO','EJECUCION','COMPLETADO')
END
END

------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 20-06-2023
-- Description:	SELECCIONAR TOLVAS DE PREVIAJES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Previajes_Tolvas_SeleccionarPreviaje]
@idPreviajeTolvas INT,
@Anio INT
AS
BEGIN
	SELECT PT.idPreviajeTolvas,PT.NroTicket, PT.Anio, PT.IdOT, PT.Tarifa,PT.idCliente,PT.Cliente, PT.idRemitente, PT.Remitente, PT.idPartida,PT.DireccionPartida, PT.idDestinatario,
	PT.Destinatario, PT.idDestino, PT.DireccionDestino, PT.FechaProgramacion, PT.TipoProceso,PT.Motonave,idProducto,PT.Producto,PT.idRuta,PT.Ruta, PD.idPreviajeTolvas, PD.Anio, PD.idPlaca, PD.Placa,
	PD.idCarreta, PD.Carreta, PD.idConductor, PD.Conductor ,TarjetaCirculacionTracto, TarjetaCirculacionCarreta, UMUso,Distancia,Tiempo,Estado, ISNULL(PD.Cantidad,0) AS 'Cantidad'
	FROM ReportesApp_Operaciones_Previajes_Tolvas_Cabecera PT
	LEFT JOIN ReportesApp_Operaciones_Previajes_Tolvas_Detalle PD ON (PT.idPreviajeTolvas = PD.idPreviajeTolvas AND PT.Anio = PD.Anio)
	WHERE PT.idPreviajeTolvas = @idPreviajeTolvas AND PT.Anio = @Anio
END

-----------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 20-06-2023
-- Description:	IMPORTAR REPORTE SUMARIZADOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Previajes_Tolvas_ImportarReporteSumarizado]
@NroTicket INT
AS
BEGIN
	SELECT DISTINCT ROW_NUMBER() OVER(ORDER BY T.NroTicket ASC) AS 'ITEM', T.NroTicket, T.Viaje AS 'VIAJE', CONVERT(VARCHAR,T.FechaViaje,103) AS 'FECHA',
	C.Cliente AS 'CLIENTE', T.Tracto AS 'TRACTO', T.Carreta AS 'CARRETA', T.NombresConductor+', '+T.ApellidosConductor AS 'CONDUCTOR',
	T.Producto AS 'DESCRIPCION', T.Ruta AS 'RUTA', T.Serie+'-'+T.Numero AS 'G/T', T.GuiaRemision AS 'G/R', C.Tarifa AS 'FLETE',
	T.PesoCliente AS 'PESO_DESCARGA', T.Peso AS 'PESO_CARGA', T.Merma AS 'MERMA', C.Tarifa * T.PesoCliente AS 'TOTAL'
	FROM ReportesApp_Operaciones_Viajes_Tolvas T 
	INNER JOIN ReportesApp_Operaciones_Previajes_Tolvas_Cabecera C ON T.idPreviajeTolvas = C.idPreviajeTolvas AND T.Anio = C.Anio
	INNER JOIN OP_TR_Vehiculo V ON V.IdVehiculo = T.idUnidad
	LEFT JOIN ReportesApp_Operaciones_GuiasElectronicas G ON SerieGuia = T.Serie AND G.NumeroGuia = T.Numero AND G.TipoGuia = T.TipoGuia
	LEFT JOIN OP_GE_OTDetalle DT ON DT.IdViaje = T.IdViaje
	WHERE T.NroTicket = @NroTicket AND T.idViaje != 0 AND G.EstadoSunat = 'APROBADO'
END

------------------------------------------------------------------------------------

SELECT * FROM OP_GE_OTDetalle
SELECT * FROM ReportesApp_Operaciones_Previajes_Tolvas_Cabecera

-- MULTIPLICAR PESO CLIENTE X TARIFA PARA OBTENER TOTAL
-- EN LISTA DE OPERACIONES TOLVAS, AGREGAR OPCIÓN PARA IMPORTAR TABLA DE SEGUNDO REQUERIMIENTO