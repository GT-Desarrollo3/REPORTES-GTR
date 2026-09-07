
--Crear tabla ReportesApp_Combustible_TicketsUrea_Cabecera
--Crear tabla ReportesApp_Combustible_TicketsUrea_Detalle

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 03-07-2023
-- Description:	CREAR TICKETS DE DESPACHO DE ÚREA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Combustible_Tickets_InsertarUrea]
@CodViaje VARCHAR(15),
@xmlTicket VARCHAR(MAX),
@Urea DECIMAL(16,2)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativoC INT
DECLARE @idoc INT
DECLARE @TEMP_DESPACHO TABLE(
		TicketSurtidor INT,
		TicketPreviaje INT,
		Tipo CHAR(1))
		
SET @Exito = '0 = Ticket de Urea Registrado.'
SET @correlativoC = (SELECT MAX(idTicketUreaCab) FROM ReportesApp_Combustible_TicketsUrea_Cabecera)
SET @correlativoC = ISNULL(@correlativoC,0) + 1

IF(@xmlTicket IS NOT NULL) BEGIN
	EXEC sp_xml_preparedocument @idoc OUTPUT, @xmlTicket
	INSERT INTO @TEMP_DESPACHO(TicketSurtidor,TicketPreviaje,Tipo)
	SELECT * FROM OPENXML(@idoc,N'/r/surtidor')
	WITH (ticket INT, codigo INT, tipo CHAR(1));
	EXEC sp_xml_removedocument @idoc;
END

BEGIN TRAN
BEGIN TRY
	INSERT INTO ReportesApp_Combustible_TicketsUrea_Cabecera(idTicketUreaCab,CodViaje,Placa,Fecha,Hora,Cantidad,Producto,Empresa)
	SELECT @correlativoC,CONVERT(INT,V.Codigo),VH.NumeroPlaca,CONVERT(DATE,C.FechaDespacho,3),CONVERT(TIME(3),C.FechaDespacho),@Urea,'UREA ADBLUE GREEN','TRANSPESA'
	FROM OP_TR_CargaCombustible C WITH(NOLOCK)
	LEFT JOIN OP_TR_VEHICULO VH WITH(NOLOCK) ON VH.IdVehiculo = C.IdVehiculo
	LEFT JOIN OP_TR_VIAJE V WITH(NOLOCK) ON V.IdViaje = C.IdViaje
	WHERE C.NumeroDocumentoSalida IS NOT NULL AND V.Codigo = @CodViaje
	
	INSERT INTO ReportesApp_Combustible_TicketsUrea_Detalle(idTicketUreaDet,idTicketUreaCab,TicketSurtidor,TicketPreviaje,Tipo,EsAnexado)
	SELECT ROW_NUMBER() OVER(ORDER BY @correlativoC ASC),@correlativoC,TD.TicketSurtidor,TD.TicketPreviaje,TD.Tipo,0
	FROM @TEMP_DESPACHO TD
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

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 06-07-2023
-- Description:	IMPORTAR TICKETS DE ÚREA A KARDEX
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Combustible_Tickets_ImportarUrea]
@CodViaje VARCHAR(15),
@Usuario CHAR(30)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Ticket importado con éxito.'

BEGIN TRAN
BEGIN TRY
	DECLARE @FechaEmpieza DATE = '01/01/2020'
	DECLARE @CantidadTicket INT = 0
	DECLARE @Nro INT
	DECLARE @NroTran INT
	DECLARE @correlativo INT
	DECLARE @TC REAL
	DECLARE @TicketSurtidor INT
	DECLARE @PrecioUnitario DECIMAL(16,2)
	DECLARE @NUMERODOCUMENTO VARCHAR(50)
		
	DECLARE @TableTicketUrea TABLE(Nro INT, idTicketUreaCab INT, Placa VARCHAR(20), Fecha DATETIME, Cantidad NUMERIC(12,3),
								   Producto VARCHAR(30), Empresa VARCHAR(30), NroTicketPreViaje INT, Proyecto VARCHAR(15),TipoCombustible INT)
								   
	DECLARE @TableTicketUrea_Confirmados TABLE(Nro INT, idTicketUreaCab INT, Placa VARCHAR(20), Fecha DATETIME, Cantidad NUMERIC(12,3),
											   Producto VARCHAR(30), Empresa VARCHAR(30), NroTicketPreViaje INT,Proyecto VARCHAR(15),
											   TipoCombustible INT, Confirmado INT, TipoProgramacion INT) 

	SET @correlativo = (SELECT MAX(idNexo) FROM ReportesApp_Combustible_TicketsUrea_NexoKardex)
	SET @correlativo = ISNULL(@correlativo,0) + 1
	
	INSERT INTO @TableTicketUrea
	SELECT ROW_NUMBER() OVER(ORDER BY TU.Fecha ASC, TU.Hora ASC) AS Nro, TU.idTicketUreaCab,
		   CASE WHEN TU.Placa='CF1' THEN 'CF1' --'CARFRONT01'
				WHEN TU.Placa='CF2' THEN 'CF2' --'CARFRONT02'
				WHEN TU.Placa='CF3' THEN 'CF3' --'CARFRONT03'
				WHEN TU.Placa='CF4' THEN 'CF4' --'CARFRONT04'
				WHEN TU.Placa='CF5' THEN 'CF5' --'CARFRONT05'
				WHEN TU.Placa='CF6' THEN 'CF6' --'CARFRONT06'
				WHEN TU.Placa='CF8' THEN 'CF8' --'CARFRONT08' 
				ELSE TU.Placa 
			END,
			CONVERT(DATETIME,TU.Fecha) + CONVERT(DATETIME,TU.Hora), TU.Cantidad, TU.Producto, TU.Empresa, UD.TicketPreviaje,V.Proyecto,V.TipoCombustible
	FROM ReportesApp_Combustible_TicketsUrea_Cabecera TU WITH(NOLOCK)
	INNER JOIN ReportesApp_Combustible_TicketsUrea_Detalle UD ON UD.idTicketUreaCab = TU.idTicketUreaCab
	INNER JOIN OP_TR_Vehiculo V ON V.NumeroPlaca = TU.Placa 
	WHERE UD.EsAnexado = 0 AND V.TipoCombustible NOT IN (1) AND V.INDTERCERO = 'P'
		  AND TU.Fecha > @FechaEmpieza
	ORDER By Fecha ASC
	
	INSERT INTO @TableTicketUrea_Confirmados 
	SELECT U.*, CASE WHEN X.IdProgramacion IS NULL THEN 0 ELSE 1 END, X.TipoProgramacion 
	FROM @TableTicketUrea U
	LEFT JOIN (SELECT R.IdProgramacion,R.NroTicket,R.FechaProgramacion,V.NumeroPlaca AS Placa, 
					  R.TipoProgramacion, R.idTracto
			   FROM ReportesApp_Operacion_Previaje_Registros R WITH(NOLOCK)
			   INNER JOIN OP_TR_Vehiculo V on V.IdVehiculo = R.idTracto AND V.INDTERCERO = 'P' AND V.TipoCombustible NOT IN (1)
			   WHERE R.FechaProgramacion > @FechaEmpieza AND R.Estado <> 10)X
	ON X.Placa = U.Placa AND X.NroTicket = U.NroTicketPreViaje AND X.FechaProgramacion > @FechaEmpieza
	WHERE U.Fecha > @FechaEmpieza
	ORDER BY U.Fecha ASC
	
	--ACTUALIZAR CORRELATIVO DE REQUISICIONES
	UPDATE CorrelativosMast
	SET CorrelativoNumero = (SELECT CorrelativoNumero + 1 FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='100000') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='WHRQ')),
		UltimaFechaModif = GETDATE()
	WHERE (CorrelativosMast.CompaniaCodigo ='100000') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='WHRQ') 
	
	SET @Nro = (SELECT CorrelativoNumero FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='100000') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='WHRQ'))
	SET @PrecioUnitario = (SELECT TOP(1) CAST(WH_TransaccionDetalle.PrecioUnitario AS DECIMAL(10,2)) FROM WH_TransaccionDetalle
	WHERE (WH_TransaccionDetalle.Item = '0000009664') AND (WH_TransaccionDetalle.TipoDocumento = 'NI') AND (WH_TransaccionDetalle.ReferenciaTipoDocumento = 'OC')
	ORDER BY WH_TransaccionDetalle.NumeroDocumento DESC)
	
	SET @TicketSurtidor = (SELECT TOP(1) D.TicketSurtidor FROM ReportesApp_Combustible_TicketsUrea_Detalle D
						   INNER JOIN ReportesApp_Combustible_TicketsUrea_Cabecera C ON C.idTicketUreaCab = D.idTicketUreaCab WHERE C.CodViaje = CONVERT(INT,@CodViaje))

	--INSERTAR REQUERIMIENTO
	INSERT INTO WH_Requisiciones(CompaniaSocio,RequisicionNumero,Clasificacion,ComprasAlmacenFlag,AlmacenCodigo,MonedaCodigo,FechaRequerida,FechaPreparacion,PreparadaPor,Departamento,PrecioTotal,
								 PrioridadCodigo,DefaultPrime,DefaultAfe,CuantiaMonetariaPendienteFlag,UnidadNegocio,UnidadReplicacion,LocalForeignFlag,Comentarios,Estado,UltimoUsuario,UltimaFechaModif,
							   	 UltimoUsuarioNumero,TransaccionOperacion,DefaultCampoReferencia,DireccionDestino,UnidadNegocioCompra,RevisionTecnicaPendienteFlag)
	SELECT DISTINCT '10000000',CONVERT(CHAR(20),RIGHT('0000000000'+LTRIM(RTRIM(@Nro)),10)),'STO', 'A', 'A001', 'LO', GETDATE(), GETDATE(), 22084, 'COM', CAST(TC.Cantidad*@PrecioUnitario AS DECIMAL(10,2)), '1', '010201', UC.Proyecto, 'N', 'TRAN', 'TRUJ',
	'L', 'PLACA: '+TC.Placa+' / TICKET SURTIDOR: '+CONVERT(VARCHAR(20),@TicketSurtidor),'RV',@Usuario, GETDATE(), 22084, '999', '90', 'Costos', 'TRAN', 'N'
	FROM ReportesApp_Combustible_TicketsUrea_Cabecera TC
	INNER JOIN @TableTicketUrea_Confirmados UC ON UC.idTicketUreaCab = TC.idTicketUreaCab
	WHERE TC.CodViaje = CONVERT(INT,@CodViaje)
	
	INSERT INTO WH_RequisicionDetalle(CompaniaSocio,RequisicionNumero,Secuencia,Item,Condicion,UnidadCodigo,Descripcion,ComprasAlmacenFlag,RedefinidoFlag,CantidadPedida,CantidadOrdenCompra,CantidadRecibida,
									  PrecioUnitario,PrecioxCantidad,CotizacionCantidad,CotizacionPrecioUnitario,CotizacionPrecioUnitarioconIGV,CotizacionProveedor,CotizacionRegistros,ControlPresupuestalFlag,Comentario,CentroCosto,Estado,UltimoUsuario,
									  UltimaFechaModif,IGVExoneradoFlag,GenerarContratoFlag,CuentaContable,Afe)
	SELECT DISTINCT '10000000', CONVERT(CHAR(20),RIGHT('0000000000'+LTRIM(RTRIM(@Nro)),10)),1,'0000009664', '0', 'LT', 'UREA ADBLUE GREEN', 'A', 'N', TC.Cantidad, 0.00, 0.00, @PrecioUnitario, CAST(TC.Cantidad*@PrecioUnitario AS DECIMAL(10,2)),
	0.00, 0.00, 0.00, 0, 0, 'S', '', '010201', 'PR', @Usuario, GETDATE(), 'N', 'N', '9000203', UC.Proyecto
	FROM ReportesApp_Combustible_TicketsUrea_Cabecera TC
	INNER JOIN @TableTicketUrea_Confirmados UC ON UC.idTicketUreaCab = TC.idTicketUreaCab
	WHERE TC.CodViaje = CONVERT(INT,@CodViaje)
	
	INSERT INTO WH_RequisitionDistribucion(RequisicionNumero,Secuencia,Linea,Account,Afe,Monto,CompaniaSocio,Sucursal,CampoReferencia)
	SELECT DISTINCT CONVERT(CHAR(20),RIGHT('0000000000'+LTRIM(RTRIM(@Nro)),10)), 1, 1, '9000203', UC.Proyecto, 100.0000, '10000000', 'BTRU', '90'
	FROM ReportesApp_Combustible_TicketsUrea_Cabecera TC
	INNER JOIN @TableTicketUrea_Confirmados UC ON UC.idTicketUreaCab = TC.idTicketUreaCab
	WHERE TC.CodViaje = CONVERT(INT,@CodViaje)

	--APROBAR REQUERIMIENTO
	UPDATE WH_Requisiciones
	SET ComprasAlmacenFlag = 'A', AlmacenCodigo = 'A001', FechaAprobacion = GETDATE(), AprobadaPor = 22084, RazonRechazo = NULL, Estado = 'AP',
		RevisionTecnicaPendienteFlag = 'N' WHERE CompaniaSocio = '10000000' AND RequisicionNumero = CONVERT(CHAR(20),RIGHT('0000000000'+LTRIM(RTRIM(@Nro)),10))
	UPDATE WH_RequisicionDetalle
	SET ComprasAlmacenFlag = 'A', Estado = 'PE' WHERE CompaniaSocio = '10000000' AND RequisicionNumero = CONVERT(CHAR(20),RIGHT('0000000000'+LTRIM(RTRIM(@Nro)),10)) AND Secuencia = 1 
	
	/*
	SELECT * FROM WH_Requisiciones WHERE CompaniaSocio = '10000000' AND RequisicionNumero = '0000202342'
	SELECT * FROM WH_RequisicionDetalle WHERE CompaniaSocio = '10000000' AND RequisicionNumero = '0000202342' AND Secuencia = 1 
	*/
	
	--ACTUALIZAR CORRELATIVO TRANSACCIÓN
	UPDATE CorrelativosMast
	SET CorrelativoNumero=(SELECT CorrelativoNumero + 1 FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='100000') AND (CorrelativosMast.TipoComprobante ='WH') AND (CorrelativosMast.Serie ='NS')),
	UltimaFechaModif=GETDATE()
	WHERE (CorrelativosMast.CompaniaCodigo ='100000') AND (CorrelativosMast.TipoComprobante ='WH') AND (CorrelativosMast.Serie ='NS')
	
	SET @NroTran = (SELECT CorrelativoNumero FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='100000') AND (CorrelativosMast.TipoComprobante ='WH') AND (CorrelativosMast.Serie ='NS'))
	SET @TC = (SELECT CAST(TipoCambioMast.FactorVenta AS DECIMAL(4,2)) FROM TipoCambioMast WHERE (TipoCambioMast.FechaCambio = (SELECT Fecha FROM ReportesApp_Combustible_TicketsUrea_Cabecera WHERE CodViaje = CONVERT(INT,@CodViaje))))
	
	--INSERTAR TRANSACCIÓN
	INSERT INTO WH_TransaccionHeader(CompaniaSocio,TipoDocumento,NumeroDocumento,TransaccionOperacion,TransaccionCodigo,FechaDocumento,TipodeCambio,Periodo,AlmacenCodigo,CentroCostoConsumo,
									 Proyecto,ReferenciaTipoDocumento,ReferenciaNumeroDocumento,ReferenciaNumeroInterno,ReferenciaInterno,DireccionDestino,Sucursal,CampoReferencia,GenerarGuiaFlag,
									 ImprimirGuiaFlag,ValorizacionManualFlag,ValorizacionPendienteFlag,Comentario,UnidadNegocio,UnidadReplicacion,Estado,UltimaFechaModif,UltimoUsuario,IngresadoPor)
	SELECT DISTINCT '10000000', 'NS', CONVERT(CHAR(10),@NroTran), '999', 'REQ', CONVERT(DATETIME, TC.Fecha)+CONVERT(DATETIME, TC.Hora), @TC, CONVERT(CHAR(4),YEAR(GETDATE()))+CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(GETDATE()))),2)),
	'A001','010201',CONVERT(CHAR(10),UC.Proyecto),'RQ',CONVERT(CHAR(20),RIGHT('0000000000'+LTRIM(RTRIM(@Nro)),10)),0,'','Costos','BTRU','90','N','N','N','N',TC.Placa,'TRAN','TRUJ','PR', GETDATE(),
	@Usuario,22084 FROM ReportesApp_Combustible_TicketsUrea_Cabecera TC
	INNER JOIN @TableTicketUrea_Confirmados UC ON UC.idTicketUreaCab = TC.idTicketUreaCab
	WHERE TC.CodViaje = CONVERT(INT,@CodViaje)
	
	INSERT INTO WH_TransaccionDetalle(CompaniaSocio,TipoDocumento,NumeroDocumento,Secuencia,Item,Condicion,Lote,UnidadCodigo,Cantidad,CantidadFIFO,PrecioUnitario,MontoTotal,PrecioUnitarioDolares,
									  MontoTotalDolares,ReferenciaCompaniaSocio,ReferenciaTipoDocumento,ReferenciaNumeroDocumento,ReferenciaSecuencia,UnidadReplicacion,AprobacionTecnicaFlag,FallaFlag,
									  CentroCosto,Afe)
	SELECT DISTINCT '10000000','NS',CONVERT(CHAR(10),@NroTran), 1,'0000009664','0','00','LT',CONVERT(MONEY,TC.Cantidad),CONVERT(MONEY,TC.Cantidad),0.00,0.00,0.00,0.00,'10000000','RQ',
	CONVERT(CHAR(20),RIGHT('0000000000'+LTRIM(RTRIM(@Nro)),10)),1,'TRUJ','N','N','010201',CONVERT(CHAR(10),UC.Proyecto) FROM ReportesApp_Combustible_TicketsUrea_Cabecera TC
	INNER JOIN @TableTicketUrea_Confirmados UC ON UC.idTicketUreaCab = TC.idTicketUreaCab
	WHERE TC.CodViaje = CONVERT(INT,@CodViaje)
	
	/*
	SELECT * FROM WH_TransaccionHeader WHERE NumeroDocumento = '610959'
	SELECT * FROM WH_TransaccionDetalle WHERE NumeroDocumento = '610959'
	*/
		
	--INSERTAR AL KARDEX
	INSERT INTO WH_Kardex(AlmacenCodigo,Item,Condicion,Lote,Fecha,TransaccionCodigo,ReferenciaCompaniaSocio,ReferenciaTipoDocumento,ReferenciaNumeroDocumento,ReferenciaSecuencia,Cantidad,
						  PrecioUnitario,PrecioUnitarioDolares,MontoTotal,MontoTotalDolares,Periodo,UltimoUsuario,UltimaFechaModif)
	SELECT DISTINCT	'A001','0000009664','0','00',GETDATE(),'REQ','10000000','NS',CONVERT(CHAR(10),@NroTran),1,-TC.Cantidad,0.00,0.00,0.00,0.00,
	CONVERT(CHAR(4),YEAR(GETDATE()))+CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(GETDATE()))),2)),@Usuario,GETDATE()
	FROM ReportesApp_Combustible_TicketsUrea_Cabecera TC
	INNER JOIN @TableTicketUrea_Confirmados UC ON UC.idTicketUreaCab = TC.idTicketUreaCab
	WHERE TC.CodViaje = CONVERT(INT,@CodViaje)
	
	INSERT INTO ReportesApp_Combustible_TicketsUrea_NexoKardex(idNexo, idTicketUreaCab, ReferenciaNumeroDocumento)
	SELECT DISTINCT @correlativo, TC.idTicketUreaCab, CONVERT(CHAR(10),@NroTran)
	FROM ReportesApp_Combustible_TicketsUrea_Cabecera TC
	INNER JOIN @TableTicketUrea_Confirmados UC ON UC.idTicketUreaCab = TC.idTicketUreaCab
	WHERE TC.CodViaje = CONVERT(INT,@CodViaje)
	
	--ACTUALIZAR LOTE
	UPDATE WH_ItemAlmacenLote
	SET StockActual = CONVERT(MONEY,(SELECT StockActual FROM WH_ItemAlmacenLote WHERE Item = '0000009664')-(SELECT TC.Cantidad FROM ReportesApp_Combustible_TicketsUrea_Cabecera TC WHERE TC.CodViaje = CONVERT(INT,@CodViaje))),
		StockComprometido = 0.0000, StockActualDoble = NULL
	WHERE Item = '0000009664' AND Condicion = '0' AND AlmacenCodigo = 'A001' AND Lote = '00' 

	--ACTUALIZAR REQUERIMIENTOS
	UPDATE WH_RequisicionDetalle
	SET CantidadRecibida = (SELECT TC.Cantidad FROM ReportesApp_Combustible_TicketsUrea_Cabecera TC WHERE TC.CodViaje = CONVERT(INT,@CodViaje)),
		Estado = 'CO'
	WHERE CompaniaSocio = '10000000' AND Secuencia = 1 AND RequisicionNumero = CONVERT(CHAR(20),RIGHT('0000000000'+LTRIM(RTRIM(@Nro)),10))
	UPDATE WH_Requisiciones
	SET Estado = 'CO'
	WHERE CompaniaSocio = '10000000' AND RequisicionNumero = CONVERT(CHAR(20),RIGHT('0000000000'+LTRIM(RTRIM(@Nro)),10))

	UPDATE D SET D.EsAnexado = CASE WHEN TU.Confirmado = 1 THEN (CASE WHEN TU.Cantidad > 0 THEN 1 ELSE 2 END) ELSE 2 END
	FROM ReportesApp_Combustible_TicketsUrea_Detalle D
	LEFT JOIN @TableTicketUrea_Confirmados TU ON TU.NroTicketPreViaje = D.TicketPreviaje
	AND TU.idTicketUreaCab = D.idTicketUreaCab
	WHERE D.EsAnexado = 0 AND D.idTicketUreaCab = (SELECT idTicketUreaCab FROM ReportesApp_Combustible_TicketsUrea_Cabecera WHERE CodViaje = CONVERT(INT,@CodViaje))
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

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 08-07-2023
-- Description:	REVISAR UREA DE UNIDADES
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Combustible_Tickets_RevisarUrea]
@Placa VARCHAR(15)
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF ((SELECT C.UREA FROM ReportesApp_Operacion_MaestroUnidadesConductor C
		 LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo=C.IdUnidad WHERE V.NumeroPlaca=@Placa) = 1) BEGIN
		SET @Exito = '1 = Esta unidad tiene úrea.'
	END
	ELSE BEGIN
		SET @Exito = '0 = Esta unidad no tiene úrea.'
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

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 12-07-2023
-- Description:	ELIMINAR TICKETS DE ÚREA DEL KARDEX
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Combustible_Tickets_EliminarUrea]
@Ticket VARCHAR(9),
@Tipo VARCHAR(1) = 'PROPIO',
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Ticket de úrea eliminado con éxito.'

IF @Tipo = 'P' BEGIN
	IF NOT EXISTS(SELECT TOP(1) TicketSurtidor FROM ReportesApp_Combustible_TicketsUrea_Detalle WHERE CONVERT(VARCHAR(9),TicketSurtidor) = @Ticket)
	BEGIN
		SET @exito = '-1 = El Ticket de úrea ' + @Ticket + ' no existe.'
		GOTO Terminar
	END	
END

BEGIN TRAN
BEGIN TRY
	DECLARE @fechaUrea DATE = ( SELECT TOP 1 fecha FROM ReportesApp_Combustible_TicketsUrea_Cabecera c inner join ReportesApp_Combustible_TicketsUrea_Detalle d on c.idticketureacab = d.idticketureacab WHERE CONVERT(VARCHAR(9),TicketSurtidor) = @Ticket ) 

	IF (SELECT IndDisponible FROM OP_GE_CONTROLPERIODO WHERE PERIODO = SUBSTRING(CONVERT (VARCHAR(50) ,@fechaUrea ,120),0,5) +SUBSTRING(CONVERT (VARCHAR(50) ,@fechaUrea ,120),6,2))=1
	BEGIN
		SET @exito= '-1= El Periodo se encuentra cerrado, no es posible anular urea!'
		ROLLBACK
		GOTO Terminar
	END

	DECLARE @CodigoViaje INT
	DECLARE @idCabecera INT
	DECLARE @Cantidad MONEY
	DECLARE @NotaSalida CHAR(10)
	DECLARE @Requerimiento CHAR(15)

	SET @idCabecera = (SELECT idTicketUreaCab FROM ReportesApp_Combustible_TicketsUrea_Detalle WHERE CONVERT(VARCHAR(9),TicketSurtidor) = @Ticket)
	SET @CodigoViaje = (SELECT CodViaje FROM ReportesApp_Combustible_TicketsUrea_Cabecera WHERE idTicketUreaCab = @idCabecera)
	SET @NotaSalida = (SELECT ReferenciaNumeroDocumento FROM ReportesApp_Combustible_TicketsUrea_NexoKardex WHERE idTicketUreaCab = @idCabecera)
	SET @Cantidad = (SELECT Cantidad FROM WH_TransaccionDetalle WHERE NumeroDocumento = @NotaSalida)
	SET @Requerimiento = (SELECT ReferenciaNumeroDocumento FROM WH_TransaccionHeader WHERE NumeroDocumento = @NotaSalida)

	INSERT INTO ReportesApp_Combustible_EliminarUrea_Historial (TicketSurtidor,CodViaje,NotaSalida,Cantidad,Requerimiento,FechaAnula,UsuarioAnula)
	VALUES (@idCabecera,@CodigoViaje,@NotaSalida,@Cantidad,@Requerimiento,getdate(),null)

	DELETE FROM ReportesApp_Combustible_TicketsUrea_NexoKardex WHERE ReferenciaNumeroDocumento = @NotaSalida

	---------------------------------------------------------------------

	--DELETE FROM WH_Kardex WHERE ReferenciaNumeroDocumento = @NotaSalida

	-- ACTUALIZAR CORRELATIVO DE NOTA INGRESO
	UPDATE CorrelativosMast
	SET CorrelativoNumero = (SELECT CorrelativoNumero + 1 FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='100000') AND (CorrelativosMast.TipoComprobante ='WH') AND (CorrelativosMast.Serie ='NI')),
		UltimaFechaModif = GETDATE()
	WHERE (CorrelativosMast.CompaniaCodigo ='100000') AND (CorrelativosMast.TipoComprobante ='WH') AND (CorrelativosMast.Serie ='NI') 
	
	DECLARE @NroNI INT = (SELECT CorrelativoNumero FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='100000') AND (CorrelativosMast.TipoComprobante ='WH') AND (CorrelativosMast.Serie ='NI'))
	DECLARE @NroUsuario INT = (SELECT P.Persona FROM Usuario U LEFT JOIN PersonaMast P ON P.NombreCompleto = U.Nombre WHERE Usuario = @Usuario)

	-- INSERTAR TRANSACCION DE NOTA INGRESO
	INSERT INTO WH_TransaccionHeader (CompaniaSocio, TipoDocumento, NumeroDocumento, TransaccionOperacion, TransaccionCodigo, FechaDocumento, TipodeCambio, 
	Periodo, AlmacenCodigo, CentroCostoConsumo, Proyecto, ReferenciaTipoDocumento, ReferenciaNumeroDocumento, ReferenciaNumeroInterno, ReferenciaInterno,
	DireccionDestino, Sucursal, CampoReferencia, GenerarGuiaFlag, ImprimirGuiaFlag, ValorizacionManualFlag, ValorizacionPendienteFlag, Comentario,
	UnidadNegocio, UnidadReplicacion, Estado, UltimaFechaModif, UltimoUsuario, IngresadoPor)
	SELECT DISTINCT CompaniaSocio, 'NI', @NroNI, '999', 'ARE', GETDATE(), TipodeCambio, CONVERT(CHAR(4),YEAR(GETDATE()))+CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(GETDATE()))),2)),
	'A001', CentroCostoConsumo, Proyecto, 'NS', @NotaSalida,  0, '', DireccionDestino, Sucursal, CampoReferencia, 'N', 'N', 'N', 'N', 'ANULACION UREA',
	'TRAN', 'TRUJ', 'PR', GETDATE(), @Usuario, @NroUsuario
	FROM WH_TransaccionHeader
	WHERE TipoDocumento = 'NS' AND NumeroDocumento = @NotaSalida

	INSERT INTO WH_TransaccionDetalle (CompaniaSocio, TipoDocumento, NumeroDocumento, Secuencia, Item, Condicion, Lote, UnidadCodigo, Cantidad,
	CantidadFIFO, PrecioUnitario, MontoTotal, PrecioUnitarioDolares, MontoTotalDolares, ReferenciaCompaniaSocio, ReferenciaTipoDocumento,
	ReferenciaNumeroDocumento, ReferenciaSecuencia, UnidadReplicacion, AprobacionTecnicaFlag, FallaFlag, ValorizacionExternaFlag, CentroCosto, Afe)
	SELECT CompaniaSocio, 'NI', @NroNI, 1, Item, '0', '00', 'LT', Cantidad, CantidadFIFO, PrecioUnitario, MontoTotal, PrecioUnitarioDolares,
	MontoTotalDolares, ReferenciaCompaniaSocio, ReferenciaTipoDocumento, ReferenciaNumeroDocumento, 1, UnidadReplicacion, 'N', 'N', 'N', CentroCosto, afe
	FROM WH_TransaccionDetalle 
	WHERE TipoDocumento = 'NS' AND NumeroDocumento = @NotaSalida

	-- INSERTAR KARDEX DE NOTA INGRESO
	INSERT INTO WH_Kardex (AlmacenCodigo, Item, Condicion, Lote, Fecha, TransaccionCodigo, ReferenciaCompaniaSocio, ReferenciaTipoDocumento,
	ReferenciaNumeroDocumento, ReferenciaSecuencia, Cantidad, PrecioUnitario, PrecioUnitarioDolares, MontoTotal, MontoTotalDolares, Periodo,
	UltimoUsuario, UltimaFechaModif)
	SELECT AlmacenCodigo, Item, '0', '00', GETDATE(), 'ARE', ReferenciaCompaniaSocio, 'NI', @NroNI, 1, Cantidad * -1, PrecioUnitario, PrecioUnitarioDolares,
	MontoTotal * -1, MontoTotalDolares * -1, CONVERT(CHAR(4),YEAR(GETDATE()))+CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(GETDATE()))),2)), @Usuario, GETDATE()
	FROM WH_Kardex
	WHERE ReferenciaTipoDocumento = 'NS' AND ReferenciaNumeroDocumento = @NotaSalida

	-- ACTUALIZAR REQUISICIONES
	DECLARE @CodAlmacen VARCHAR(4) = (select TOP 1 CodAlmacen from ReportesApp_Combustible_RegistrarUreaPorSucursal WHERE idTicketUreaCab = @idCabecera)

	UPDATE WH_ItemAlmacenLote
	SET StockActual = CONVERT(MONEY,(SELECT StockActual FROM WH_ItemAlmacenLote WHERE Item = '0000009664') + @Cantidad),
		StockComprometido = 0.0000, StockActualDoble = NULL
	WHERE Item = '0000009664' AND Condicion = '0' AND AlmacenCodigo = @CodAlmacen AND Lote = '00'

	UPDATE WH_RequisicionDetalle
	SET CantidadRecibida = 0.00, Estado = 'PE'
	WHERE CompaniaSocio = '10000000' AND Secuencia = 1 AND RequisicionNumero = @Requerimiento

	UPDATE WH_Requisiciones SET Estado = 'AN' WHERE CompaniaSocio = '10000000' AND RequisicionNumero = @Requerimiento

	UPDATE WH_TransaccionHeader
	SET ReferenciaTipoDocumento = 'NI', ReferenciaNumeroDocumento = @NroNI, Estado = 'AN', UltimoUsuario = @Usuario, UltimaFechaModif = GETDATE()
	WHERE (CompaniaSocio ='10000000') AND (TipoDocumento ='NS') AND (WH_TransaccionHeader.NumeroDocumento = @NotaSalida) 

	--------------------------------------------------------------

	DELETE FROM ReportesApp_Combustible_TicketsUrea_Detalle WHERE idTicketUreaCab = @idCabecera
	DELETE FROM ReportesApp_Combustible_TicketsUrea_Cabecera WHERE idTicketUreaCab = @idCabecera

	DELETE FROM ReportesApp_Combustible_RegistrarUreaPorSucursal WHERE NotaSalida = @NotaSalida
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

-----------------------------------------------

--MODIFICAR ReportesApp_Combustible_Reporte_Diario


/*
1 VIAJE CON UNA RUTA
exec ReportesApp_Combustible_RegistrarDespachoPropios @IdVehiculo=1270,@IdConductor=11472,@IdViaje=336317,@TipoCombustible=2,@Kilometraje=894379.00,@CantidadProgramada=200.000000,@TipoDespacho=1,@IndOrigen=1,@IndAutomatico=1,@CantidadDespachada=39.112,@CantidadMarcador=0,@FechaDespacho=N'11/05/2023 19:21:58',@IdMotivo=1,@IdSurtidor=1,@IdDespachador=21573,@Estado=2,@UsuarioCreacion=N'SCHAVEZ',@UsuarioModificacion=N'SCHAVEZ',@Compania=N'10000000',@PrecioCombustible=7.44,@CostoReal=291,@IndMovSalida=2,@TipoDocumentoSalida=N'NS',@LineaDocumentoSalida=1,@CPUCantidad=48.00,@CPUKilometraje=440.000,@CPUPorcentaje=0.81000000000000005,@KilometrajeFinal=894819.000,@Reserva=12.00,@Urea=12.6,@Vueltas1=3.00,@xmlTickets=N'<?xml version="1.0" encoding="UTF-8"?><r><surtidor ticket="300028140" placa="T7F851" codigo="239726" tipo="P" /></r>',@Categoria1=N'',@Categoria2=N'',@Categoria3=N'',@Peso1=N'',@Peso2=N'',@Peso3=N'',@Origen1=N'',@Origen2=N'',@Origen3=N'',@Destino1=N'System.Data.DataRowView',@Destino2=N'System.Data.DataRowView',@Destino3=N'System.Data.DataRowView',@Ruta1=N'',@Ruta2=N'',@Ruta3=N'',@Recorrido1=0,@Recorrido2=0,@Recorrido3=0,@TipoCarreta=N'CORTINERA',@RecorridoTotal=440.000,@ComsumoSW=48.00,@ComsumoMeta=0,@ComsumoFisico=39.112,@RendimientoVirtual=0,@RendimientoSW=9.17,@RendimientoFisico=11.25,@Dif_CF_vs_SW=-8.89,@Dif_CF_vs_Meta=39.11,@P_CF_vs_SW=0.81000000000000005,@P_CF_vs_Meta=1
exec ReportesApp_Combustible_Tickets_InsertarUrea @CodViaje=N'293145',@xmlTicket=N'<?xml version="1.0" encoding="UTF-8"?><r><surtidor ticket="300028140" placa="T7F851" codigo="239726" tipo="P" /></r>',@Urea=12.6

1 VIAJE CON DOS RUTAS
exec ReportesApp_Combustible_RegistrarDespachoPropios @IdVehiculo=5132,@IdConductor=21,@IdViaje=336307,@TipoCombustible=2,@Kilometraje=135908.00,@CantidadProgramada=160.000000,@TipoDespacho=1,@IndOrigen=1,@IndAutomatico=1,@CantidadDespachada=7.995,@CantidadMarcador=0,@FechaDespacho=N'07/07/2022 10:59:59',@IdMotivo=1,@IdSurtidor=1,@IdDespachador=21573,@Estado=2,@UsuarioCreacion=N'SCHAVEZ',@UsuarioModificacion=N'SCHAVEZ',@Compania=N'10000000',@PrecioCombustible=7.44,@CostoReal=59,@IndMovSalida=2,@TipoDocumentoSalida=N'NS',@LineaDocumentoSalida=1,@CPUCantidad=12.69,@CPUKilometraje=-86566.000,@CPUPorcentaje=0.63,@KilometrajeFinal=49342.000,@Reserva=12.00,@Urea=6.69,@Vueltas1=0,@xmlTickets=N'<?xml version="1.0" encoding="UTF-8"?><r><surtidor ticket="300012093" placa="TBB942" codigo="227255" tipo="P" /><surtidor ticket="300011820" placa="TBB942" codigo="226960" tipo="P" /></r>',@Categoria1=N'',@Categoria2=N'',@Categoria3=N'',@Peso1=N'',@Peso2=N'',@Peso3=N'',@Origen1=N'',@Origen2=N'',@Origen3=N'',@Destino1=N'',@Destino2=N'',@Destino3=N'',@Ruta1=N'',@Ruta2=N'',@Ruta3=N'',@Recorrido1=0,@Recorrido2=0,@Recorrido3=0,@TipoCarreta=N'CORTINERA',@RecorridoTotal=-86566.000,@ComsumoSW=12.69,@ComsumoMeta=0,@ComsumoFisico=7.995,@RendimientoVirtual=0,@RendimientoSW=-6821.59,@RendimientoFisico=-10827.52,@Dif_CF_vs_SW=-4.70,@Dif_CF_vs_Meta=8.00,@P_CF_vs_SW=0.63,@P_CF_vs_Meta=1
exec ReportesApp_Combustible_Tickets_InsertarUrea @CodViaje=N'293135',@xmlTicket=N'<?xml version="1.0" encoding="UTF-8"?><r><surtidor ticket="300012093" placa="TBB942" codigo="227255" tipo="P" /><surtidor ticket="300011820" placa="TBB942" codigo="226960" tipo="P" /></r>',@Urea=6.69
*/

SELECT V.NumeroPlaca, C.Urea FROM ReportesApp_Operacion_MaestroUnidadesConductor C
LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo=C.IdUnidad
WHERE C.Urea = 1

/*
SELECT * FROM OP_TR_VIAJE V WHERE Codigo = '292941'
SELECT * FROM OP_TR_CargaCombustible WHERE IdViaje = 336113

DELETE FROM OP_TR_CargaCombustible WHERE IdViaje = 336113
*/	

SELECT * FROM ReportesApp_Combustible_TicketsUrea_Cabecera ORDER BY idTicketUreaCab
SELECT * FROM ReportesApp_Combustible_TicketsUrea_Detalle ORDER BY idTicketUreaCab
SELECT * FROM ReportesApp_Combustible_TicketsUrea_NexoKardex ORDER BY idTicketUreaCab

DELETE FROM ReportesApp_Combustible_TicketsUrea_Cabecera WHERE CodViaje = '292941'
DELETE FROM ReportesApp_Combustible_TicketsUrea_Detalle WHERE idTicketUreaCab = 2