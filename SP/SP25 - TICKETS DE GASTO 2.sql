
-- UPDATE Usuario SET Estado = 'A' WHERE Usuario = 'DEMO'
-- UPDATE PersonaMast SET Estado = 'A' WHERE Persona = 0

-----------------------------------------------------------

-- AGREGAR GASTO DIFERENCIAL A ReportesApp_Operaciones_TicketGasto_Registro

-----------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 20-07-2023
-- Description:	INSERTAR GASTO CABECERA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_InsertarGasto]
@IdRuta INT,
@IdOperacion INT,
@TotalDias INT,
@GastoTotal DECIMAL(10,2),
@Detalle INT
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @SinAdicional DECIMAL(10,2)
DECLARE @correlativo INT

SET @Exito = '0 = Gasto asignado.'

IF (@IdRuta IS NULL AND @IdOperacion IS NULL)
BEGIN
	SET @Exito = '-1 = No se pudo asignar el gasto.'
	GOTO Terminar
END

SET @correlativo = (SELECT MAX(idGastoxRutaC) FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera)
SET @correlativo = ISNULL(@correlativo,0) + 1

BEGIN TRAN
BEGIN TRY
	IF (@IdRuta = (SELECT IdRuta FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera WHERE IdRuta = @IdRuta AND IdOperacion = @IdOperacion)) BEGIN
		UPDATE ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera
		SET GastoTotal = @GastoTotal, TotalDias = @TotalDias
		WHERE idGastoxRutaC = (SELECT idGastoxRutaC FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera WHERE IdRuta = @IdRuta AND IdOperacion = @IdOperacion)
		
		UPDATE ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle
		SET idGastoxRutaC = (SELECT idGastoxRutaC FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera WHERE IdRuta = @IdRuta AND IdOperacion = @IdOperacion)
		WHERE idGastoxRutaC IS NULL

		SET @SinAdicional = (SELECT ISNULL(SUM(Gasto),0) FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle
		WHERE idGastoxRutaC = (SELECT idGastoxRutaC FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera
		WHERE IdRuta = @IdRuta AND IdOperacion = @IdOperacion) AND Adicional = 0)

		UPDATE ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera
		SET GastoTotal = @SinAdicional
		WHERE idGastoxRutaC = (SELECT idGastoxRutaC FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera WHERE IdRuta = @IdRuta AND IdOperacion = @IdOperacion)
		
		SET @Exito = '0 = Gasto actualizado.'
	END
	ELSE BEGIN
		IF (@Detalle = 1) BEGIN
			UPDATE ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle
			SET idGastoxRutaC = @correlativo
			WHERE idGastoxRutaC IS NULL
		END
		
		INSERT INTO ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera(idGastoxRutaC, TotalDias, IdRuta, IdOperacion)
		VALUES (@correlativo, @TotalDias, @IdRuta, @IdOperacion)

		SET @SinAdicional = (SELECT ISNULL(SUM(Gasto),0) FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle
		WHERE idGastoxRutaC = @correlativo AND Adicional = 0)

		UPDATE ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera
		SET GastoTotal = @SinAdicional
		WHERE idGastoxRutaC = @correlativo
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

-----------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 20-07-2023
-- Description:	INSERTAR GASTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_InsertarGastoDetalle]
@idTipoGasto INT,
@Descripcion VARCHAR(250),
@Gasto DECIMAL(10,2),
@idTiempo INT,
@Adicional INT,
@idPeaje INT
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

SET @Exito = '0 = Gasto añadido.'

SET @correlativo = (SELECT MAX(idGastoxRutaD) FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle)
SET @correlativo = ISNULL(@correlativo,0) + 1

/*
IF(@idTipoGasto = (SELECT idTipoGasto FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle WHERE idTipoGasto = @idTipoGasto AND idGastoxRutaC IS NULL)) BEGIN
	SET @Exito = '-1 = Este tipo de gasto ya fue añadido.'
	GOTO Terminar
END
*/

BEGIN TRAN
BEGIN TRY
	INSERT INTO ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle(idGastoxRutaD, idTipoGasto, Descripcion, Gasto, idTiempo, Adicional, idPeaje)
	VALUES(@correlativo, @idTipoGasto, @Descripcion, @Gasto, @idTiempo, @Adicional, @idPeaje)
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

-----------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 20-07-2023
-- Description:	LISTAR DETALLE DE GASTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ListarGastoDetalle]
@IdRuta INT,
@IdOperacion INT
AS
BEGIN
	(SELECT GD.idGastoxRutaD, TT.Descripcion AS 'Tiempo', T.Descripcion AS 'TipoGasto', GD.Descripcion, GD.Gasto AS 'Efectivo',
	CASE WHEN GD.Adicional = 0 THEN 'NO' ELSE 'SÍ' END AS 'Adicional'
	FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera GC
	LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle GD ON GD.idGastoxRutaC = GC.idGastoxRutaC
	LEFT JOIN ReportesApp_Operaciones_TicketGasto_TipoGasto T ON T.idTipoGasto = GD.idTipoGasto
	LEFT JOIN ReportesApp_Operaciones_TicketGasto_Tiempo TT ON TT.idTiempo = GD.idTiempo
	WHERE GC.IdRuta = @IdRuta AND GC.IdOperacion = @IdOperacion)
	UNION
	(SELECT GD.idGastoxRutaD, TT.Descripcion AS 'Tiempo', T.Descripcion AS 'TipoGasto', GD.Descripcion, GD.Gasto AS 'Efectivo',
	CASE WHEN GD.Adicional = 0 THEN 'NO' ELSE 'SÍ' END AS 'Adicional'
	FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle GD
	LEFT JOIN ReportesApp_Operaciones_TicketGasto_TipoGasto T ON T.idTipoGasto = GD.idTipoGasto
	LEFT JOIN ReportesApp_Operaciones_TicketGasto_Tiempo TT ON TT.idTiempo = GD.idTiempo
	WHERE idGastoxRutaC IS NULL)
END

-----------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 07-08-2023
-- Description:	LISTAR CABECERA DE GASTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ListarGastoCabecera]
@IdRuta INT,
@IdOperacion INT,
@idTiempo INT
AS
BEGIN
	IF(@idTiempo = 1) BEGIN
		SELECT GC.TotalDias, SUM(GD.Gasto) AS 'GastoTotal' FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle GD
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera GC ON GC.idGastoxRutaC = GD.idGastoxRutaC
		WHERE GC.IdRuta = @IdRuta AND GC.IdOperacion = @IdOperacion AND GD.idTiempo = @idTiempo AND GD.Adicional = 0
		GROUP BY (GC.TotalDias)
	END
	ELSE BEGIN
		SELECT GC.TotalDias, ROUND(SUM(GD.Gasto) + 4,-1) AS 'GastoTotal' FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle GD
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera GC ON GC.idGastoxRutaC = GD.idGastoxRutaC
		WHERE GC.IdRuta = @IdRuta AND GC.IdOperacion = @IdOperacion AND GD.idTiempo = 1 AND GD.Adicional = 0
		GROUP BY (GC.TotalDias)
	END
END

-----------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 25-11-2023
-- Description:	AGREGAR DIREFENCIAL RUTA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_AgregarDiferencialRuta]
@NroTicket INT,
@idGastoxRutaC INT,
@Usuario VARCHAR(50)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @GastoDiferencial DECIMAL(10,2)
DECLARE @CodGasto VARCHAR(30)
DECLARE @Nro INT
DECLARE @Nro2 INT
DECLARE @Nro3 INT
DECLARE @Nro4 INT
DECLARE @GastoTotal DECIMAL(10,2)
DECLARE @NroConductor INT
DECLARE @NroUsuario INT
DECLARE @NombreConductor VARCHAR(250)
DECLARE @NombreUsuario VARCHAR(250)
DECLARE @BancoConductor CHAR(3)
DECLARE @CuentaConductor CHAR(20)
DECLARE @TipoCambio MONEY
DECLARE @UltimoNroPago INT
DECLARE @MesActual INT
DECLARE @NroVoucher INT
DECLARE @Ruta VARCHAR(250)
DECLARE @Motivo VARCHAR(250)
DECLARE @CuentaBancaria CHAR(15)
DECLARE @Sucursal CHAR(4)
DECLARE @UnidadReplicacion CHAR(4)

SET @Exito = '0 = Gasto actualizado.'

IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_TicketGasto_UsuarioSucursal WHERE Usuario = @Usuario AND Estado = 'A')) BEGIN
	SET @CuentaBancaria = (SELECT CuentaBancaria FROM ReportesApp_Operaciones_TicketGasto_UsuarioSucursal WHERE Usuario = @Usuario)
	SET @Sucursal = (SELECT Sucursal FROM ReportesApp_Operaciones_TicketGasto_UsuarioSucursal WHERE Usuario = @Usuario)
	SET @UnidadReplicacion = (SELECT TOP(1) UnidadReplicacion FROM AP_GastoAdelanto WHERE NumeroDocumentoInterno = @CodGasto AND Descripcion LIKE '%' + 'GASTOS DE VIAJE ' + '%' AND Estado != 'AN')
	--SET @UnidadReplicacion = (SELECT UnidadReplicacion FROM ReportesApp_Operaciones_TicketGasto_UsuarioSucursal WHERE Usuario = @Usuario)
END
ELSE BEGIN
	SET @Exito = '-1 = Este usuario está inactivo en Tabla Sucursales o no tiene una sede registrada. Revisar con Sistemas'
	GOTO Terminar
END

SET @CodGasto = (SELECT CodGasto FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE NroTicket = @NroTicket)
SET @Motivo = 'REINTEGRO POR CAMBIO DE RUTA: PL - ' + @CodGasto

BEGIN TRAN
BEGIN TRY
	--OBTENER DIFERENCIA DE GASTO DE RUTA
	SET @GastoDiferencial = (SELECT GastoDiferencial FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE NroTicket = @NroTicket)

	IF (@GastoDiferencial > 0) BEGIN	--AÑADIR GASTO ADICIONAL EN ORDEN DE PAGO
		-- Actualizar correlativo de adelantos
		UPDATE CorrelativosMast
		SET CorrelativoNumero = (SELECT CorrelativoNumero + 1 FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='APAD')),
			UltimaFechaModif = GETDATE()
		WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='APAD')
		SET @Nro = (SELECT CorrelativosMast.CorrelativoNumero FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='APAD'))
		
		SET @GastoTotal = @GastoDiferencial
		SET @NroConductor = (SELECT P.Persona FROM ReportesApp_Operacion_Previaje_Registros R
							LEFT JOIN OP_TR_Conductor C ON C.IdConductor = R.IdConductor
							LEFT JOIN PersonaMast P ON P.Persona = C.IdPersona WHERE R.NroTicket = @NroTicket)
		SET @NombreConductor = (SELECT P.NombreCompleto FROM ReportesApp_Operacion_Previaje_Registros R
								LEFT JOIN OP_TR_Conductor C ON C.IdConductor = R.IdConductor
								LEFT JOIN PersonaMast P ON P.Persona = C.IdPersona WHERE R.NroTicket = @NroTicket)
		SET @BancoConductor = (SELECT P.BancoMonedaLocal FROM ReportesApp_Operacion_Previaje_Registros R
							   LEFT JOIN OP_TR_Conductor C ON C.IdConductor = R.IdConductor
							   LEFT JOIN PersonaMast P ON P.Persona = C.IdPersona WHERE R.NroTicket = @NroTicket)
		SET @CuentaConductor = (SELECT P.CuentaMonedaLocal FROM ReportesApp_Operacion_Previaje_Registros R
								LEFT JOIN OP_TR_Conductor C ON C.IdConductor = R.IdConductor
								LEFT JOIN PersonaMast P ON P.Persona = C.IdPersona WHERE R.NroTicket = @NroTicket)
		SET @NroUsuario = (SELECT P.Persona FROM Usuario U LEFT JOIN PersonaMast P ON P.NombreCompleto = U.Nombre WHERE Usuario = @Usuario)
		SET @Ruta = (SELECT V.Descripcion FROM ReportesApp_Operacion_Previaje_Registros R LEFT JOIN OP_TR_Ruta V ON R.IdRuta = V.IdRuta WHERE R.NroTicket = @NroTicket)
	
		DELETE FROM AP_GastoAdelantoSustento WHERE (AP_GastoAdelantoSustento.UnidadReplicacion =@UnidadReplicacion) AND (AP_GastoAdelantoSustento.TipoAdelanto ='E') AND (AP_GastoAdelantoSustento.NumeroAdelanto = @Nro) 

		--Insertar y aprobar adelanto de gasto
		INSERT INTO AP_GastoAdelanto(NumeroAdelanto,CompaniaSocio,Clasificacion,FechaDocumento,TipoPago,Persona,PreparadoPor,FechaPreparacion,MonedaDocumento,MontoTotal,SaldoAdelanto,Descripcion,Estado,UltimoUsuario,
									UltimaFechaModif,FechaEsperadaPago,TipoAdelanto,NumeroDocumentoInterno,FlujodeCaja,UnidadReplicacion,PartidaPresupuestal,PersonaPagara,ConceptoGasto,CentroCostos,EmpleadoAutorizado,
									UnidadNegocio,RegComprasMontoAfecto,RegComprasMontoNoAfecto,RegComprasMontoIgv,CuentaBancaria)
		VALUES (@Nro,'10000000','ER',GETDATE(),'EF', @NroConductor, @NroUsuario, GETDATE(), 'LO', @GastoTotal, @GastoTotal, @Motivo,
		'PR', @Usuario, GETDATE(), GETDATE(), 'E', @CodGasto, '051', @UnidadReplicacion, '9999', @NroConductor, '0006', '010201', @NroUsuario, 'TRAN', 0.00, 0.00, 0.00, @CuentaBancaria)
	
		INSERT INTO AP_GastoAdelantoSustento(UnidadReplicacion,TipoAdelanto,NumeroAdelanto,Secuencia,ConceptoGasto,NumeroVeces,Moneda,PrecioUnitario,MontoTotal,UltimoUsuario,UltimaFechaModif,CuentaContable,MontoPresupuesto)
		VALUES (@UnidadReplicacion, 'E', @Nro, 1, '0006', 8, 'LO', @GastoTotal, @GastoTotal, @Usuario, GETDATE(), '1413002', 0.00)

		UPDATE AP_GastoAdelanto
		SET AprobadoPor = @NroUsuario, FechaAprobacion = GETDATE(), Estado = 'AP'
		WHERE NumeroAdelanto = @Nro AND TipoAdelanto = 'E' AND UnidadReplicacion = @UnidadReplicacion

		--Insertar obligacion
		UPDATE CorrelativosMast
		SET CorrelativoNumero = (SELECT CorrelativoNumero + 1 FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie = 'APNO')),
			UltimaFechaModif = GETDATE()
		WHERE (CorrelativosMast.CompaniaCodigo = '999999') AND (CorrelativosMast.TipoComprobante = 'SY') AND (CorrelativosMast.Serie = 'APNO')
		SET @Nro2 = (SELECT CorrelativosMast.CorrelativoNumero FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo = '999999') AND (CorrelativosMast.TipoComprobante = 'SY') AND (CorrelativosMast.Serie = 'APNO'))

		INSERT INTO Obligaciones (Proveedor,TipoDocumento,NumeroDocumento,CuentaBancaria,CompaniaCodigo,TipoPago,FechaRegistro,FechaVencimiento,FechaVencimientoOriginal,GenerarPago,TipoServicio,MonedaDocumento,ConversionRequerida,
							  MonedaPago,MontoObligacion,MontoImpuestoVentas,MontoNoAfecto,MontoImponible,MontoAdelantos,MontoImpuestos,NetoMonedaLocal,NetoMonedaExtranjera,TipoDeCambio,AprobadoPor,AprobadoCP1,AprobadoCP2,
							  IngresadoPor,RevisadoPor,EstadoDocumento,ContabilizacionPendiente,ChequeIndividual,Voucher,NumeroPago,NumeroProceso,ProcesoSecuencia,RegistroNumero,Comentarios,UltimaFechaModif,UnidadNegocio,
							  FacturaAfectaSplitFlag,FactorRValidacion,UnidadReplicacion,CanjeRegistroNumero,MontoPagoParcial,NumeroDocumentoInterno,CentroCosto,PartidaPresupuestal,FlujodeCaja,CentroCostoCP,FechaRecepcion,
							  ProveedorPagarA,ControlPresupuestalFlag,CargoFlag,MontoCreditoFiscal,FechaDocumento,AfectoIGVFlag,DiferidoFlag,AdelantoFlag)
		VALUES (@NroConductor,'AE',@UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@Nro),@CuentaBancaria,'100000','EF',GETDATE(),GETDATE(),GETDATE(),'S','INAFEC','LO','D','LO',@GastoTotal,0.00,0.00,@GastoTotal,0.00,0.00,@GastoTotal,
		0.00,0,0,0,0,@NroUsuario, @NroUsuario, 'RV', 'S', 'N', CONVERT(CHAR(4),YEAR(GETDATE()))+CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(GETDATE()))),2)),0,0,0,@Nro2, @Motivo, GETDATE(),
		'TRAN','N','N',@UnidadReplicacion,0,0.00,@CodGasto,'010201','9999','051','010201',GETDATE(),@NroConductor, 'N', 'N', 0.00, GETDATE(), 'N', 'N', 'N')

		INSERT INTO ObligacionesXCuenta (Descripcion,Proveedor,NumeroDocumento,Linea,Monto,CentroCosto,CuentaContable,PartidaPresupuestal,Persona,DocumentoReferencia,Sucursal,FlujodeCaja,NoAfectoIGVFlag,TipoDocumento)
		VALUES (@Motivo,@NroConductor,@UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@Nro),1,@GastoTotal,'010201','1413002','9999', @NroConductor,
		'AE-'+@UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@Nro),@Sucursal, '051', 'N', 'AE')

		UPDATE AP_GastoAdelanto
		SET MonedaDocumento = 'LO',ObligacionTipoDocumento = 'AE',ObligacionNumeroDocumento = @UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@Nro),Estado = 'TR'
		WHERE UnidadReplicacion = @UnidadReplicacion AND TipoAdelanto = 'E' AND NumeroAdelanto = @Nro 

		INSERT INTO AP_ObligacionFlujo (Proveedor,TipoDocumento,NumeroDocumento,Secuencia,AreaDocumentaria,Comentario,Estado,UltimoUsuario,UltimaFechaModif)
		VALUES (@NroConductor, 'AE',@UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@Nro), 1,'001','Recepción Inicial','A',@Usuario,GETDATE())

		--Insertar Orden de Pago
		INSERT INTO OrdenPago (SistemaFuente,Proveedor,TipoDocumento,NumeroDocumento,PagarA,CompaniaCodigo,CuentaBancaria,TipoPago,ChequeIndividual,FechaVencimiento,MonedaDocumento,MonedaPago,MontoMoneda,TipodeCambio,
							   BatchNumber,FechaTransferencia,Estado,FlujodeCaja,RegistroNumero,FechaDocumento,PagoDiferidoFlag,UnidadNegocio)
		VALUES ('AP',@NroConductor,'AE',@UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@Nro),@NombreConductor,'100000',@CuentaBancaria,'EF','N',GETDATE(),'LO','LO',@GastoTotal,0,0,GETDATE(),'AP','051', @Nro2, GETDATE(),'N','TRAN')

		SET @TipoCambio = (CONVERT(MONEY,(SELECT TipoCambioMast.FactorVenta FROM TipoCambioMast WHERE TipoCambioMast.MonedaCodigo ='EX' AND TipoCambioMast.MonedaCambioCodigo ='LO' AND TipoCambioMast.FechaCambio =CONVERT(DATE,GETDATE()))))

		UPDATE Obligaciones
		SET AprobadoCP1 = @NroUsuario, EstadoDocumento = 'AP', Voucher = CONVERT(CHAR(4),YEAR(GETDATE()))+CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(GETDATE()))),2)),
			FechaAprobacion = GETDATE(), TipodeCambioProvision = @TipoCambio, UltimoUsuario = @Usuario, UltimaFechaModif = GETDATE()
		WHERE Proveedor = @NroConductor AND TipoDocumento = 'AE' AND NumeroDocumento = @UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@Nro)
	
		DELETE XOrdenPago WHERE XOrdenPago.Usuario = @Usuario
	
		INSERT INTO XOrdenPago (TipoRegistro,SistemaFuente,Proveedor,TipoDocumento,NumeroDocumento,CompaniaCodigo,PagarA,CuentaBancaria,TipoPago,ChequeIndividual,MonedaPago,FechaVencimiento,Estado,MonedaDocumento,MontoMoneda,Usuario,MontoOriginal)
		VALUES ('D','AP',@NroConductor,'AE',@UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@Nro),'100000',@NombreConductor,@CuentaBancaria,'EF','N','LO',GETDATE(),'A','LO',@GastoTotal,@Usuario,@GastoTotal)
	
		INSERT INTO XOrdenPago (TipoRegistro,SistemaFuente,Proveedor,TipoDocumento,NumeroDocumento,CompaniaCodigo,PagarA,CuentaBancaria,TipoPago,ChequeIndividual,MonedaPago,Estado,MontoMoneda,Usuario)
		VALUES ('S','XX',@NroConductor,'ZZ','000001','100000',@NombreConductor,@CuentaBancaria,'EF','N','LO','A',@GastoTotal,@Usuario)

		UPDATE ReportesApp_Operaciones_TicketGasto_Registro
		SET GastoDiferencial = NULL
		WHERE NroTicket = @NroTicket
	END
	ELSE BEGIN
		SET @Exito = '-1 = No se puede generar un reintegro cuando el gasto diferencial es igual o menor.'
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

---------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 25-07-2023
-- Description:	LISTAR REGISTRO DE GASTO X RUTA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ListarRegistro]
@NroTicket INT
AS
BEGIN
	SET NOCOUNT ON;
    SELECT TG.CodGasto, LTRIM(RTRIM(C.Nombre)) AS 'CONDUCTOR', V.NumeroPlaca AS 'TRACTO', R.NumeroPlaca AS 'SEMIRREMOLQUE',
	RT.Descripcion AS 'RUTA', LTRIM(RTRIM(CL.Busqueda)) AS 'CLIENTE', F.FechaProgramacion AS 'FECHA_VIAJE', GR.TotalDias AS 'DIAS',
    T.Descripcion AS 'TIPO_GASTO', GD.Gasto AS 'GASTO', TG.TotalEntregado AS 'GASTO_TOTAL',
	ISNULL(TG.GastoDiferencial,0) AS 'GASTO_DIFERENCIAL', ISNULL(TG.GastoDiferencialV,0) AS 'VIATICOS'
    FROM ReportesApp_Operaciones_TicketGasto_Registro TG
    LEFT JOIN ReportesApp_Operacion_Previaje_Registros F WITH(NOLOCK) ON F.NroTicket = TG.NroTicket
    LEFT JOIN OP_TR_Conductor C WITH(NOLOCK) ON C.IdConductor = TG.ViConductor
    LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = F.idTracto
	LEFT JOIN OP_TR_Vehiculo R WITH(NOLOCK) ON R.IdVehiculo = F.idSemirremolque
    LEFT JOIN OP_TR_RUTA RT WITH(NOLOCK) ON RT.IDRUTA = F.IdRuta
    LEFT JOIN PersonaMast CL WITH(NOLOCK) ON CL.Persona = F.IdCliente
    LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera GR WITH(NOLOCK) ON GR.idGastoxRutaC = TG.idGastoXRutaC
    LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle GD WITH(NOLOCK) ON GD.idGastoxRutaC = TG.idGastoXRutaC
    LEFT JOIN ReportesApp_Operaciones_TicketGasto_TipoGasto T WITH(NOLOCK) ON T.idTipoGasto = GD.idTipoGasto
    WHERE TG.NroTicket = @NroTicket
END

----------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 01-08-2023
-- Description:	AGREGAR COMPROBANTE DE LIQUIDACION
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ComprobanteLiquidaciones]
@idNroLiquidacion INT,
@CodGasto VARCHAR(50),
@FechaLiquidacion DATETIME,
@TipoImpuesto CHAR(5),
@NroRUC CHAR(20),
@NombreCompleto VARCHAR(350),
@CodigoDocumento CHAR(10),
@NroDocumento VARCHAR(50),
@MontoAfecto DECIMAL(10,2),
@MontoNoAfecto DECIMAL(10,2),
@MontoImpuestos DECIMAL(10,2),
@MontoPagado DECIMAL(10,2)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Comprobante registrado con éxito.'

IF(@NroDocumento != '                    ') BEGIN
	IF(EXISTS(SELECT * FROM ReportesApp_Operaciones_TicketGasto_Liquidacion WHERE NroDocumento = @NroDocumento)) BEGIN
		SET @Exito = '-1 = Este comprobante ya fue ingresado en la Planilla: PL-' + (SELECT CodGasto FROM ReportesApp_Operaciones_TicketGasto_Liquidacion WHERE NroDocumento = @NroDocumento)
		GOTO Terminar
	END
END

BEGIN TRAN
BEGIN TRY
	UPDATE ReportesApp_Operaciones_TicketGasto_Liquidacion
	SET FechaLiquidacion = @FechaLiquidacion, TipoImpuesto = @TipoImpuesto, NroRUC = @NroRUC, NombreCompleto = @NombreCompleto, CodigoDocumento = @CodigoDocumento,
		NroDocumento = @NroDocumento, MontoAfecto = @MontoAfecto, MontoNoAfecto = @MontoNoAfecto, MontoImpuestos = @MontoImpuestos, MontoPagado = @MontoPagado
	WHERE idNroLiquidacion = @idNroLiquidacion AND CodGasto = @CodGasto
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
-- Create date: 27-11-2023
-- Description:	REIMPRIMIR TICKET
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ImprimirDiferencial]
@NroTicket INT
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Gasto actualizado.'

BEGIN TRAN
BEGIN TRY
	SET NOCOUNT ON;
	DECLARE @IdRutaActual INT
	DECLARE @NuevoGR INT
	DECLARE @GastoAnterior DECIMAL(10,2)
	DECLARE @GastoActual DECIMAL(10,2)
	DECLARE @GastoDiferencial DECIMAL(10,2)

	SET @IdRutaActual = (SELECT IdRuta FROM ReportesApp_Operacion_Previaje_Registros WHERE NroTicket = @NroTicket)
	SET @NuevoGR = (SELECT idGastoxRutaC FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera WHERE IdRuta = @IdRutaActual)

	SET @GastoAnterior = (SELECT TotalEntregado FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE NroTicket = @NroTicket)
	SET @GastoActual = (SELECT Viaticos FROM ReportesApp_Operacion_Previaje_Registros WHERE NroTicket = @NroTicket)
	SET @GastoDiferencial = @GastoActual - @GastoAnterior

	IF((@GastoActual - @GastoAnterior) > 0) BEGIN
		UPDATE ReportesApp_Operaciones_TicketGasto_Registro
		SET GastoDiferencial = @GastoDiferencial, TotalEntregado = TotalEntregado + @GastoDiferencial, idGastoXRutaC = ISNULL(@NuevoGR,0)
		WHERE NroTicket = @NroTicket
	END
	ELSE BEGIN
		UPDATE ReportesApp_Operaciones_TicketGasto_Registro
		SET idGastoXRutaC = ISNULL(@NuevoGR,0)
		WHERE NroTicket = @NroTicket
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

---------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 27-11-2023
-- Description:	PAGAR VIATICOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_PagarViaticos]
@NroTicket INT,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @Monto DECIMAL(10,8)
DECLARE @CodGasto VARCHAR(30)
DECLARE @Nro INT
DECLARE @Nro2 INT
DECLARE @GastoTotal DECIMAL(10,2)
DECLARE @NroConductor INT
DECLARE @NroUsuario INT
DECLARE @NombreConductor VARCHAR(250)
DECLARE @NombreUsuario VARCHAR(250)
DECLARE @Motivo VARCHAR(250)
DECLARE @TipoCambio MONEY
DECLARE @CuentaBancaria CHAR(15)
DECLARE @Sucursal CHAR(4)
DECLARE @UnidadReplicacion CHAR(4)

SET @Exito = '0 = Gasto actualizado.'

SET @CodGasto = (SELECT CodGasto FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE NroTicket = @NroTicket AND EstadoLiquidacion = 0)
SET @Motivo = 'VIATICO ADICIONAL: PL - ' + @CodGasto

SET @GastoTotal = (SELECT GastoDiferencialV FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE NroTicket = @NroTicket AND EstadoLiquidacion = 0)
IF (@GastoTotal IS NULL) BEGIN
	SET @Exito = '-1 = Esta planilla no tiene viáticos adicionales.'
	GOTO Terminar
END

IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_TicketGasto_UsuarioSucursal WHERE Usuario = @Usuario AND Estado = 'A')) BEGIN
	SET @CuentaBancaria = (SELECT CuentaBancaria FROM ReportesApp_Operaciones_TicketGasto_UsuarioSucursal WHERE Usuario = @Usuario)
	SET @Sucursal = (SELECT Sucursal FROM ReportesApp_Operaciones_TicketGasto_UsuarioSucursal WHERE Usuario = @Usuario)
	SET @UnidadReplicacion = (SELECT TOP(1) UnidadReplicacion FROM AP_GastoAdelanto WHERE NumeroDocumentoInterno = @CodGasto AND Descripcion LIKE '%' + 'GASTOS DE VIAJE ' + '%' AND Estado != 'AN')
END
ELSE BEGIN
	SET @Exito = '-1 = Este usuario está inactivo o no tiene una sede registrada.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	UPDATE CorrelativosMast
	SET CorrelativoNumero = (SELECT CorrelativoNumero + 1 FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='APAD')),
		UltimaFechaModif = GETDATE()
	WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='APAD')
	SET @Nro = (SELECT CorrelativosMast.CorrelativoNumero FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='APAD'))
		
	SET @NroConductor = (SELECT P.Persona FROM ReportesApp_Operacion_Previaje_Registros R
						LEFT JOIN OP_TR_Conductor C ON C.IdConductor = R.IdConductor
						LEFT JOIN PersonaMast P ON P.Persona = C.IdPersona WHERE R.NroTicket = @NroTicket)
	SET @NombreConductor = (SELECT P.NombreCompleto FROM ReportesApp_Operacion_Previaje_Registros R
							LEFT JOIN OP_TR_Conductor C ON C.IdConductor = R.IdConductor
							LEFT JOIN PersonaMast P ON P.Persona = C.IdPersona WHERE R.NroTicket = @NroTicket)
	SET @NroUsuario = (SELECT P.Persona FROM Usuario U LEFT JOIN PersonaMast P ON P.NombreCompleto = U.Nombre WHERE Usuario = @Usuario)
	
	DELETE FROM AP_GastoAdelantoSustento WHERE (AP_GastoAdelantoSustento.UnidadReplicacion = @UnidadReplicacion) AND (AP_GastoAdelantoSustento.TipoAdelanto ='E') AND (AP_GastoAdelantoSustento.NumeroAdelanto = @Nro) 

	--Insertar y aprobar adelanto de gasto
	INSERT INTO AP_GastoAdelanto(NumeroAdelanto,CompaniaSocio,Clasificacion,FechaDocumento,TipoPago,Persona,PreparadoPor,FechaPreparacion,MonedaDocumento,MontoTotal,SaldoAdelanto,Descripcion,Estado,UltimoUsuario,
								UltimaFechaModif,FechaEsperadaPago,TipoAdelanto,NumeroDocumentoInterno,FlujodeCaja,UnidadReplicacion,PartidaPresupuestal,PersonaPagara,ConceptoGasto,CentroCostos,EmpleadoAutorizado,
								UnidadNegocio,RegComprasMontoAfecto,RegComprasMontoNoAfecto,RegComprasMontoIgv,CuentaBancaria)
	VALUES (@Nro,'10000000','ER',GETDATE(),'EF', @NroConductor, @NroUsuario, GETDATE(), 'LO', @GastoTotal, @GastoTotal, @Motivo,
	'PR', @Usuario, GETDATE(), GETDATE(), 'E', @CodGasto, '051', @UnidadReplicacion, '9999', @NroConductor, '0006', '010201', @NroUsuario, 'TRAN', 0.00, 0.00, 0.00, @CuentaBancaria)
	
	INSERT INTO AP_GastoAdelantoSustento(UnidadReplicacion,TipoAdelanto,NumeroAdelanto,Secuencia,ConceptoGasto,NumeroVeces,Moneda,PrecioUnitario,MontoTotal,UltimoUsuario,UltimaFechaModif,CuentaContable,MontoPresupuesto)
	VALUES (@UnidadReplicacion, 'E', @Nro, 1, '0006', 8, 'LO', @GastoTotal, @GastoTotal, @Usuario, GETDATE(), '1413002', 0.00)

	UPDATE AP_GastoAdelanto
	SET AprobadoPor = @NroUsuario, FechaAprobacion = GETDATE(), Estado = 'AP'
	WHERE NumeroAdelanto = @Nro AND TipoAdelanto = 'E' AND UnidadReplicacion = @UnidadReplicacion 

	--Insertar obligacion
	UPDATE CorrelativosMast
	SET CorrelativoNumero = (SELECT CorrelativoNumero + 1 FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie = 'APNO')),
		UltimaFechaModif = GETDATE()
	WHERE (CorrelativosMast.CompaniaCodigo = '999999') AND (CorrelativosMast.TipoComprobante = 'SY') AND (CorrelativosMast.Serie = 'APNO')
	SET @Nro2 = (SELECT CorrelativosMast.CorrelativoNumero FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo = '999999') AND (CorrelativosMast.TipoComprobante = 'SY') AND (CorrelativosMast.Serie = 'APNO'))

	INSERT INTO Obligaciones (Proveedor,TipoDocumento,NumeroDocumento,CuentaBancaria,CompaniaCodigo,TipoPago,FechaRegistro,FechaVencimiento,FechaVencimientoOriginal,GenerarPago,TipoServicio,MonedaDocumento,ConversionRequerida,
						  MonedaPago,MontoObligacion,MontoImpuestoVentas,MontoNoAfecto,MontoImponible,MontoAdelantos,MontoImpuestos,NetoMonedaLocal,NetoMonedaExtranjera,TipoDeCambio,AprobadoPor,AprobadoCP1,AprobadoCP2,
						  IngresadoPor,RevisadoPor,EstadoDocumento,ContabilizacionPendiente,ChequeIndividual,Voucher,NumeroPago,NumeroProceso,ProcesoSecuencia,RegistroNumero,Comentarios,UltimaFechaModif,UnidadNegocio,
						  FacturaAfectaSplitFlag,FactorRValidacion,UnidadReplicacion,CanjeRegistroNumero,MontoPagoParcial,NumeroDocumentoInterno,CentroCosto,PartidaPresupuestal,FlujodeCaja,CentroCostoCP,FechaRecepcion,
						  ProveedorPagarA,ControlPresupuestalFlag,CargoFlag,MontoCreditoFiscal,FechaDocumento,AfectoIGVFlag,DiferidoFlag,AdelantoFlag)
	VALUES (@NroConductor,'AE',@UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@Nro),@CuentaBancaria,'100000','EF',GETDATE(),GETDATE(),GETDATE(),'S','INAFEC','LO','D','LO',@GastoTotal,0.00,0.00,@GastoTotal,0.00,0.00,@GastoTotal,
	0.00,0,0,0,0,@NroUsuario, @NroUsuario, 'RV', 'S', 'N', CONVERT(CHAR(4),YEAR(GETDATE()))+CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(GETDATE()))),2)),0,0,0,@Nro2, @Motivo, GETDATE(),
	'TRAN','N','N',@UnidadReplicacion,0,0.00,@CodGasto,'010201','9999','051','010201',GETDATE(),@NroConductor, 'N', 'N', 0.00, GETDATE(), 'N', 'N', 'N')

	INSERT INTO ObligacionesXCuenta (Descripcion,Proveedor,NumeroDocumento,Linea,Monto,CentroCosto,CuentaContable,PartidaPresupuestal,Persona,DocumentoReferencia,Sucursal,FlujodeCaja,NoAfectoIGVFlag,TipoDocumento)
	VALUES (@Motivo,@NroConductor,@UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@Nro),1,@GastoTotal,'010201','1413002','9999', @NroConductor,
	'AE-'+@UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@Nro),@Sucursal, '051', 'N', 'AE')

	UPDATE AP_GastoAdelanto
	SET MonedaDocumento = 'LO',ObligacionTipoDocumento = 'AE',ObligacionNumeroDocumento = @UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@Nro),Estado = 'TR'
	WHERE UnidadReplicacion = @UnidadReplicacion AND TipoAdelanto = 'E' AND NumeroAdelanto = @Nro 

	INSERT INTO AP_ObligacionFlujo (Proveedor,TipoDocumento,NumeroDocumento,Secuencia,AreaDocumentaria,Comentario,Estado,UltimoUsuario,UltimaFechaModif)
	VALUES (@NroConductor, 'AE',@UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@Nro), 1,'001','Recepción Inicial','A',@Usuario,GETDATE())

	--Insertar Orden de Pago
	INSERT INTO OrdenPago (SistemaFuente,Proveedor,TipoDocumento,NumeroDocumento,PagarA,CompaniaCodigo,CuentaBancaria,TipoPago,ChequeIndividual,FechaVencimiento,MonedaDocumento,MonedaPago,MontoMoneda,TipodeCambio,
						   BatchNumber,FechaTransferencia,Estado,FlujodeCaja,RegistroNumero,FechaDocumento,PagoDiferidoFlag,UnidadNegocio)
	VALUES ('AP',@NroConductor,'AE',@UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@Nro),@NombreConductor,'100000',@CuentaBancaria,'EF','N',GETDATE(),'LO','LO',@GastoTotal,0,0,GETDATE(),'AP','051', @Nro2, GETDATE(),'N','TRAN')

	SET @TipoCambio = (CONVERT(MONEY,(SELECT TipoCambioMast.FactorVenta FROM TipoCambioMast WHERE TipoCambioMast.MonedaCodigo ='EX' AND TipoCambioMast.MonedaCambioCodigo ='LO' AND TipoCambioMast.FechaCambio =CONVERT(DATE,GETDATE()))))

	UPDATE Obligaciones
	SET AprobadoCP1 = @NroUsuario, EstadoDocumento = 'AP', Voucher = CONVERT(CHAR(4),YEAR(GETDATE()))+CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(GETDATE()))),2)),
		FechaAprobacion = GETDATE(), TipodeCambioProvision = @TipoCambio, UltimoUsuario = @Usuario, UltimaFechaModif = GETDATE()
	WHERE Proveedor = @NroConductor AND TipoDocumento = 'AE' AND NumeroDocumento = @UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@Nro)
	
	DELETE XOrdenPago WHERE XOrdenPago.Usuario = @Usuario
	
	INSERT INTO XOrdenPago (TipoRegistro,SistemaFuente,Proveedor,TipoDocumento,NumeroDocumento,CompaniaCodigo,PagarA,CuentaBancaria,TipoPago,ChequeIndividual,MonedaPago,FechaVencimiento,Estado,MonedaDocumento,MontoMoneda,Usuario,MontoOriginal)
	VALUES ('D','AP',@NroConductor,'AE',@UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@Nro),'100000',@NombreConductor,@CuentaBancaria,'EF','N','LO',GETDATE(),'A','LO',@GastoTotal,@Usuario,@GastoTotal)
	
	INSERT INTO XOrdenPago (TipoRegistro,SistemaFuente,Proveedor,TipoDocumento,NumeroDocumento,CompaniaCodigo,PagarA,CuentaBancaria,TipoPago,ChequeIndividual,MonedaPago,Estado,MontoMoneda,Usuario)
	VALUES ('S','XX',@NroConductor,'ZZ','000001','100000',@NombreConductor,@CuentaBancaria,'EF','N','LO','A',@GastoTotal,@Usuario)

	UPDATE ReportesApp_Operaciones_TicketGasto_Registro
	SET GastoDiferencialV = NULL
	WHERE NroTicket = @NroTicket
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
-- Create date: 01-08-2023
-- Description:	INSERTAR BOLETAS LIQUIDACIONES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_InsertarLiquidaciones]
@CodGasto VARCHAR(50),
@FechaLiquidacion DATETIME,
@TipoImpuesto CHAR(5),
@ConceptoGasto CHAR(5),
@DescripcionGasto VARCHAR(200),
@NroRUC CHAR(20),
@NombreCompleto VARCHAR(350),
@CodigoDocumento CHAR(10),
@NroDocumento VARCHAR(50),
@MontoAfecto DECIMAL(10,2),
@MontoNoAfecto DECIMAL(10,2),
@MontoImpuestos DECIMAL(10,2),
@MontoPagado DECIMAL(10,2)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT
DECLARE @NroComprobante INT

SET @Exito = '0 = Exito.'

SET @correlativo = (SELECT MAX(idNroLiquidacion) FROM ReportesApp_Operaciones_TicketGasto_Liquidacion WHERE CodGasto = @CodGasto)
SET @correlativo = ISNULL(@correlativo,0) + 1

IF(@NroDocumento != '                    ') BEGIN
	IF(EXISTS(SELECT * FROM ReportesApp_Operaciones_TicketGasto_Liquidacion WHERE LTRIM(RTRIM(NroDocumento)) = LTRIM(RTRIM(@NroDocumento))
	AND LTRIM(RTRIM(NroRUC)) = LTRIM(RTRIM(@NroRUC)) AND LTRIM(RTRIM(@ConceptoGasto)) = '0002')) BEGIN
		SET @Exito = '-1 = Este comprobante ya fue ingresado en la Planilla PL-' + (SELECT CodGasto FROM ReportesApp_Operaciones_TicketGasto_Liquidacion
		WHERE LTRIM(RTRIM(NroDocumento)) = LTRIM(RTRIM(@NroDocumento)) AND LTRIM(RTRIM(NroRUC)) = LTRIM(RTRIM(@NroRUC)))
		GOTO Terminar
	END
END

IF (LTRIM(RTRIM(@ConceptoGasto)) = '0078') BEGIN
	SET @NroComprobante = (SELECT MAX(NroCorrelativo) FROM ReportesApp_Operaciones_TicketGasto_Correlativo WHERE Anio = YEAR(GETDATE()))
	SET @NroComprobante = ISNULL(@NroComprobante,0) + 1

	UPDATE ReportesApp_Operaciones_TicketGasto_Correlativo
	SET NroCorrelativo = @NroComprobante, Anio = YEAR(GETDATE()), Codigo = CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@NroComprobante)),6)) + '-' + SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2)
END

IF(EXISTS(SELECT * FROM ReportesApp_Operaciones_TicketGasto_Liquidacion WHERE CONVERT(DATE,FechaLiquidacion) = CONVERT(DATE,@FechaLiquidacion) AND LTRIM(RTRIM(@ConceptoGasto)) = '0001'
AND LTRIM(RTRIM(NroDocumento)) = LTRIM(RTRIM(@NroDocumento)))) BEGIN
	SET @Exito = '-1 = El conductor ya tiene este peaje registrado en la planilla: ' +
				 (SELECT TOP(1) CodGasto FROM ReportesApp_Operaciones_TicketGasto_Liquidacion WHERE CONVERT(DATE,FechaLiquidacion) = CONVERT(DATE,@FechaLiquidacion) AND LTRIM(RTRIM(@ConceptoGasto)) = '0001'
				  AND LTRIM(RTRIM(NroDocumento)) = LTRIM(RTRIM(@NroDocumento)))
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	IF (LTRIM(RTRIM(@ConceptoGasto)) = '0078') BEGIN
		INSERT INTO ReportesApp_Operaciones_TicketGasto_Liquidacion(idNroLiquidacion,CodGasto,FechaLiquidacion,TipoImpuesto,ConceptoGasto,DescripcionGasto,NroRUC,NombreCompleto,
				CodigoDocumento,NroDocumento,MontoAfecto,MontoNoAfecto,MontoImpuestos,MontoPagado, Contador)
		VALUES(@correlativo,@CodGasto,@FechaLiquidacion,@TipoImpuesto,@ConceptoGasto,@DescripcionGasto,@NroRUC,@NombreCompleto,@CodigoDocumento,@NroDocumento,@MontoAfecto,@MontoNoAfecto,
				@MontoImpuestos,@MontoPagado, CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@NroComprobante)),6)) + '-' + SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2))
	END
	ELSE BEGIN
		INSERT INTO ReportesApp_Operaciones_TicketGasto_Liquidacion(idNroLiquidacion,CodGasto,FechaLiquidacion,TipoImpuesto,ConceptoGasto,DescripcionGasto,NroRUC,NombreCompleto,
				CodigoDocumento,NroDocumento,MontoAfecto,MontoNoAfecto,MontoImpuestos,MontoPagado)
		VALUES(@correlativo,@CodGasto,@FechaLiquidacion,@TipoImpuesto,@ConceptoGasto,@DescripcionGasto,@NroRUC,@NombreCompleto,@CodigoDocumento,@NroDocumento,@MontoAfecto,@MontoNoAfecto,
				@MontoImpuestos,@MontoPagado)
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
-- Create date: 31-07-2023
-- Description:	LISTAR CONCEPTOS DE GASTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ListarConceptosGasto]
@Accion INT,
@CodGasto VARCHAR(50)
AS
BEGIN
	IF @Accion = 1 BEGIN     --LISTAR TIPOS DE GASTO
		SELECT * FROM ReportesApp_Operaciones_TicketGasto_ConceptoGasto
		WHERE ConceptoGasto NOT IN ('0062','0003','DVP')
	END
	
	IF @Accion = 2 BEGIN     --LISTAR TIPOS DE DOCUMENTOS
		SELECT CodigoDocumento, Descripcion FROM ReportesApp_Operaciones_TicketGasto_TipoDocumento
		ORDER BY idTipoDocumento 
	END
	
	IF @Accion = 3 BEGIN     --LISTAR RECIBOS INGRESADOS
		SELECT L.idNroLiquidacion AS 'Nro', L.CodGasto AS 'PLANILLA', L.FechaLiquidacion AS 'FECHA_EMISION', L.TipoImpuesto, (CASE WHEN L.TipoImpuesto = 'I' THEN 'COMPRA' ELSE 'NINGUNO' END) AS 'TIPO_IMPUESTO',
			   L.ConceptoGasto, CG.DescripcionLocal AS 'CONCEPTO_GASTO', L.DescripcionGasto AS 'DESCRIPCION', L.NroRUC AS 'RUC', L.NombreCompleto AS 'NOMBRE_COMPLETO', L.CodigoDocumento, L.NroDocumento AS 'COMPROBANTE',
			   L.MontoAfecto AS 'MONTO_AFECTO', L.MontoNoAfecto AS 'MONTO_NO_AFECTO', L.MontoImpuestos AS 'MONTO_IMPUESTOS', L.MontoPagado AS 'MONTO_PAGADO', L.Contador
		FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_ConceptoGasto CG ON L.ConceptoGasto = CG.ConceptoGasto
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_TipoDocumento D ON L.CodigoDocumento = D.CodigoDocumento
		WHERE CodGasto = @CodGasto
		ORDER BY idNroLiquidacion DESC
	END
	
	IF @Accion = 4 BEGIN     --LISTAR TIPOS DE VIÁTICO
		SELECT * FROM ReportesApp_Operaciones_TicketGasto_TipoViaticos
		ORDER BY idTipoViatico
	END

	IF @Accion = 5 BEGIN     --ACTUALIZAR CORRELATIVO - COMPROBANTES SIN SUSTENTO
		DECLARE @c1 INT
		SET @c1 = (SELECT NroCorrelativo FROM ReportesApp_Operaciones_TicketGasto_Correlativo WHERE idCorrelativo = 1 AND Anio = YEAR(GETDATE()))
		SET @c1 = ISNULL(@c1,0) + 1
		
		UPDATE ReportesApp_Operaciones_TicketGasto_Correlativo
		SET NroCorrelativo = @c1, Anio = YEAR(GETDATE())
		WHERE idCorrelativo = 1

		UPDATE ReportesApp_Operaciones_TicketGasto_Correlativo
		SET Codigo = CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(NroCorrelativo)),6)) + '-' + SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2)
		WHERE idCorrelativo = 1

		SELECT Codigo FROM ReportesApp_Operaciones_TicketGasto_Correlativo WHERE idCorrelativo = 1
	END

	IF @Accion = 6 BEGIN     --ACTUALIZAR CORRELATIVO - TICKET DESPACHO
		DECLARE @c2 INT
		SET @c2 = (SELECT NroCorrelativo FROM ReportesApp_Operaciones_TicketGasto_Correlativo WHERE idCorrelativo = 2 AND Anio = YEAR(GETDATE()))
		SET @c2 = ISNULL(@c2,0) + 1
		
		UPDATE ReportesApp_Operaciones_TicketGasto_Correlativo
		SET NroCorrelativo = @c2, Anio = YEAR(GETDATE())
		WHERE idCorrelativo = 2

		UPDATE ReportesApp_Operaciones_TicketGasto_Correlativo
		SET Codigo = CONVERT(INT,SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@c2)),6)))
		WHERE idCorrelativo = 2

		SELECT Codigo FROM ReportesApp_Operaciones_TicketGasto_Correlativo WHERE idCorrelativo = 2
	END

	IF @Accion= 7 BEGIN		--LISTAR PEAJES
		SELECT GP.idPeaje, P.Descripcion AS 'Peaje'
		FROM ReportesApp_Operaciones_TicketGasto_GastoPeaje GP
		LEFT JOIN OP_TR_Peaje P ON GP.IdPeaje = P.IdPeaje
		WHERE GP.Monto > 20
		ORDER BY P.Descripcion
	END
END

---------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 13-12-2023
-- Description:	INSERTAR REGISTRO VIATICOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_RegistrarViaticos]
@idConductor INT,
@FechaViatico DATETIME,
@Comprobante VARCHAR(50),
@Monto DECIMAL(10,2),
@NroRUC CHAR(20),
@Planilla VARCHAR(50)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

SET @correlativo = (SELECT MAX(idListaViatico) FROM ReportesApp_Operaciones_TicketGasto_ListaViaticos WHERE @idConductor = idConductor)
SET @correlativo = ISNULL(@correlativo,0) + 1

SET @Exito = '0 = Viático registrado.'

IF(EXISTS(SELECT * FROM ReportesApp_Operaciones_TicketGasto_ListaViaticos WHERE LTRIM(RTRIM(Comprobante)) = @Comprobante AND LTRIM(RTRIM(NroRUC)) = LTRIM(RTRIM(@NroRUC))
AND @Comprobante NOT LIKE '%ALIM%')) BEGIN
	SET @Exito = '-2 = Este comprobante ya fue ingresado en la Planilla PL-' +
				 (SELECT TOP(1) Planilla FROM ReportesApp_Operaciones_TicketGasto_ListaViaticos WHERE LTRIM(RTRIM(Comprobante)) = @Comprobante AND
				 LTRIM(RTRIM(NroRUC)) = LTRIM(RTRIM(@NroRUC))) + ' del conductor ' + (SELECT TOP(1) C.Nombre FROM ReportesApp_Operaciones_TicketGasto_ListaViaticos V
				 LEFT JOIN OP_TR_Conductor C ON C.idConductor = V.idConductor WHERE LTRIM(RTRIM(V.Comprobante)) = @Comprobante AND
				 LTRIM(RTRIM(V.NroRUC)) = LTRIM(RTRIM(@NroRUC)))

	DELETE FROM ReportesApp_Operaciones_TicketGasto_ListaViaticos
	WHERE LTRIM(RTRIM(Comprobante)) = @Comprobante AND LTRIM(RTRIM(NroRUC)) = LTRIM(RTRIM(@NroRUC)) AND RTRIM(Planilla) = @Planilla
	
	GOTO Terminar
END

IF(EXISTS(SELECT * FROM ReportesApp_Operaciones_TicketGasto_ListaViaticos WHERE FechaViatico = CONVERT(DATE,@FechaViatico) AND idConductor = @idConductor)) BEGIN
	SET @Exito = '-1 = El conductor ya tiene un viático registrado en esta fecha en la Planilla PL-' +
				 (SELECT TOP(1) Planilla FROM ReportesApp_Operaciones_TicketGasto_ListaViaticos WHERE FechaViatico = CONVERT(DATE,@FechaViatico)
				 AND idConductor = @idConductor) + ' del conductor ' + (SELECT TOP(1) C.Nombre FROM ReportesApp_Operaciones_TicketGasto_ListaViaticos V
				 LEFT JOIN OP_TR_Conductor C ON C.idConductor = V.idConductor WHERE V.FechaViatico = CONVERT(DATE,@FechaViatico) AND V.idConductor = @idConductor)
	
	DELETE FROM ReportesApp_Operaciones_TicketGasto_ListaViaticos
	WHERE LTRIM(RTRIM(Comprobante)) = @Comprobante AND LTRIM(RTRIM(NroRUC)) = LTRIM(RTRIM(@NroRUC)) AND RTRIM(Planilla) = @Planilla
		
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	INSERT INTO ReportesApp_Operaciones_TicketGasto_ListaViaticos (idListaViatico, idConductor, FechaViatico, Comprobante, Monto, NroRUC, Planilla)
	VALUES (@correlativo, @idConductor, CONVERT(DATE,@FechaViatico), @Comprobante, @Monto, @NroRUC, @Planilla)
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
-- Create date: 01-08-2023
-- Description:	ELIMINAR LIQUIDACION
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_EliminarLiquidaciones]
@idNroLiquidacion INT,
@CodGasto VARCHAR(50)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @CountMax INT

SET @Exito = '0 = Recibo eliminado.'

IF (@idNroLiquidacion = 0)
BEGIN
	SET @Exito = '-1 = El recibo seleccionado no existe.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	IF ((SELECT LTRIM(RTRIM(ConceptoGasto)) FROM ReportesApp_Operaciones_TicketGasto_Liquidacion WHERE idNroLiquidacion = @idNroLiquidacion AND CodGasto = @CodGasto) = '0002') BEGIN
		DELETE FROM ReportesApp_Operaciones_TicketGasto_ListaViaticos
		WHERE Comprobante = (SELECT NroDocumento FROM ReportesApp_Operaciones_TicketGasto_Liquidacion WHERE idNroLiquidacion = @idNroLiquidacion AND CodGasto = @CodGasto)
	END

	IF ((SELECT LTRIM(RTRIM(ConceptoGasto)) FROM ReportesApp_Operaciones_TicketGasto_Liquidacion WHERE idNroLiquidacion = @idNroLiquidacion AND CodGasto = @CodGasto) = '0107') BEGIN
		DELETE FROM ReportesApp_Operaciones_TicketGasto_ListaViaticos
		WHERE Comprobante = (SELECT NroDocumento FROM ReportesApp_Operaciones_TicketGasto_Liquidacion WHERE idNroLiquidacion = @idNroLiquidacion AND CodGasto = @CodGasto)
		AND Planilla = @CodGasto
	END

	DELETE FROM ReportesApp_Operaciones_TicketGasto_Liquidacion
	WHERE idNroLiquidacion = @idNroLiquidacion AND CodGasto = @CodGasto
	
	UPDATE ReportesApp_Operaciones_TicketGasto_Liquidacion
	SET idNroLiquidacion = idNroLiquidacion - 1
	WHERE idNroLiquidacion > @idNroLiquidacion AND CodGasto = @CodGasto
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
-- Create date: 24-08-2023
-- Description:	LISTAR REGISTRO DE VIATICOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ListarRegistroViaticos]
@Opcion INT,
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@NombreConductor VARCHAR(250)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@Opcion = 1) BEGIN		-- VIÁTICOS ADICIONALES
		SELECT V.idViatico, C.Nombre AS 'CONDUCTOR', V.Fecha AS 'FECHA_VIATICO', V.CodGasto AS 'PLANILLA', RT.Descripcion AS 'RUTA_VIAJE',
		V.Descripcion AS 'VIATICO', V.Monto AS 'IMPORTE', V.Motivo AS 'MOTIVO'
		FROM ReportesApp_Operaciones_TicketGasto_Viaticos V
		LEFT JOIN OP_TR_Conductor C ON C.IdConductor = V.IdConductor
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_TipoViaticos TV ON TV.idTipoViatico = V.idTipoViatico
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_Registro R ON R.CodGasto = V.CodGasto
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera GC ON GC.idGastoxRutaC = R.idGastoxRutaC
		LEFT JOIN OP_TR_RUTA RT ON RT.IdRuta = GC.IdRuta
		WHERE ((V.Fecha BETWEEN @FINICIO AND @FFIN) AND (C.Nombre LIKE '%' + @NombreConductor + '%'))
		ORDER BY V.Fecha DESC
	END

	IF (@Opcion = 2) BEGIN		-- REGISTRO DE VIÁTICOS
		(SELECT V.idListaViatico, V.IdConductor, C.Nombre AS 'CONDUCTOR', V.FechaViatico AS 'FECHA_VIATICO', L.DescripcionGasto,
		L.CodGasto AS 'PLANILLA', V.Comprobante AS 'COMPROBANTE', L.NombreCompleto AS 'PROVEEDOR', V.Monto AS 'IMPORTE'
		FROM ReportesApp_Operaciones_TicketGasto_ListaViaticos V
		LEFT JOIN OP_TR_Conductor C ON C.IdConductor = V.IdConductor
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_Liquidacion L ON LTRIM(RTRIM(L.NroDocumento)) = LTRIM(RTRIM(V.Comprobante))
		AND LTRIM(RTRIM(L.NroRUC)) = LTRIM(RTRIM(V.NroRUC)) AND (L.ConceptoGasto IN ('0002 ','0077 ')) AND (L.CodGasto = V.Planilla)
		WHERE ((L.CodGasto IS NOT NULL) AND (V.FechaViatico BETWEEN @FINICIO AND @FFIN) AND (C.Nombre LIKE '%' + @NombreConductor + '%')))
		UNION
		(SELECT V.idListaViatico, V.IdConductor, C.Nombre AS 'CONDUCTOR', V.FechaViatico AS 'FECHA_VIATICO', L.DescripcionGasto,
		V.Planilla AS 'PLANILLA', V.Comprobante AS 'COMPROBANTE', C.Nombre AS 'PROVEEDOR', V.Monto AS 'IMPORTE'
		FROM ReportesApp_Operaciones_TicketGasto_ListaViaticos V
		LEFT JOIN OP_TR_Conductor C ON C.IdConductor = V.IdConductor
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_Liquidacion L ON LTRIM(RTRIM(L.CodGasto)) = LTRIM(RTRIM(V.Planilla)) AND LTRIM(RTRIM(L.NroDocumento)) = LTRIM(RTRIM(V.Comprobante))
		WHERE ((V.Comprobante LIKE 'ALIM%') AND (V.Comprobante IS NOT NULL) AND (V.FechaViatico BETWEEN @FINICIO AND @FFIN) AND (C.Nombre LIKE '%' + @NombreConductor + '%')))
		ORDER BY V.FechaViatico DESC
	END
END

----------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 21-07-2023
-- Description:	INSERTAR TICKET DE GASTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_RegistrarTicketGasto]
@NroTicket INT,
@idGastoxRutaC INT,
@TotalEntregado DECIMAL(10,2),
@Usuario VARCHAR(50)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT
DECLARE @IdConductor INT
DECLARE @Contador INT
DECLARE @idOperacion INT

SET @Exito = '0 = Planilla Registrada.'

SET @IdConductor = (SELECT IdConductor FROM ReportesApp_Operacion_Previaje_Registros WHERE NroTicket = @NroTicket)
SET @Contador = (SELECT COUNT(TG.ViConductor) FROM ReportesApp_Operaciones_TicketGasto_Registro TG WHERE TG.EstadoLiquidacion = 0 AND TG.ViConductor = @IdConductor)

/*
IF (@Contador >= 4)
BEGIN
	SET @Exito = '-1 = Este conductor aún tiene 4 planillas por liquidar.'
	GOTO Terminar
END
*/

SET @correlativo = (SELECT MAX(idTicketGasto) FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE Anio = YEAR(GETDATE()))
SET @correlativo = ISNULL(@correlativo,0) + 1


BEGIN TRAN
BEGIN TRY
	INSERT INTO ReportesApp_Operaciones_TicketGasto_Registro(idTicketGasto, Anio, CodGasto, NroTicket, idGastoXRutaC, TotalEntregado, FechaCreacion, UsuarioCreacion,EstadoLiquidacion,Permiso)
	VALUES(@correlativo, YEAR(GETDATE()), SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo)),6)), @NroTicket, @idGastoxRutaC, @TotalEntregado, GETDATE(), @Usuario, 0, 0)

	UPDATE ReportesApp_Operaciones_TicketGasto_Registro
	SET ViConductor = (SELECT IdConductor FROM ReportesApp_Operacion_Previaje_Registros WHERE NroTicket = @NroTicket), IdOperacion = (SELECT TipoProgramacion FROM ReportesApp_Operacion_Previaje_Registros WHERE NroTicket = @NroTicket)
	WHERE CodGasto = SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo)),6))
	
	UPDATE ReportesApp_Operacion_Previaje_Registros
	SET Planilla = SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo)),6))
	WHERE NroTicket = @NroTicket
	
	SET @Exito = '0 = Planilla Registrada. PL - ' + SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo)),6))
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
-- Create date: 19-08-2023
-- Description:	ELIMINAR PLANILLA SIN PAGAR
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_EliminarPlanilla]
@NroProgramacion INT,
@CodGasto VARCHAR(50),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Planilla eliminada.'

BEGIN TRAN
BEGIN TRY
	IF (EXISTS(SELECT * FROM AP_GastoAdelanto WHERE NumeroDocumentoInterno = @CodGasto AND (Estado = 'PA'))) BEGIN
		IF ((SELECT EstadoLiquidacion FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE CodGasto = @CodGasto) = 0) BEGIN
			SET @Exito = '-1 = Esta planilla ya se encuentra pagada. Favor de ir a liquidarla para desvincularla.'
			ROLLBACK
			GOTO Terminar
		END
		ELSE BEGIN
			UPDATE ReportesApp_Operaciones_TicketGasto_Viaticos
			SET CodGasto = NULL
			WHERE CodGasto = @CodGasto

			UPDATE ReportesApp_Operacion_Previaje_Registros
			SET Planilla = ''
			WHERE NroTicket = @NroProgramacion
		END
	END
	ELSE BEGIN
		UPDATE ReportesApp_Operaciones_TicketGasto_Viaticos
		SET CodGasto = NULL
		WHERE CodGasto = @CodGasto

		UPDATE ReportesApp_Operacion_Previaje_Registros
		SET Planilla = ''
		WHERE NroTicket = @NroProgramacion

		UPDATE ReportesApp_Operaciones_TicketGasto_Registro
		SET EstadoLiquidacion = 2, UsuarioLiquidacion = @Usuario, FechaLiquidacion = GETDATE()
		WHERE CodGasto = @CodGasto
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
-- Create date: 24-08-2023
-- Description:	LISTAR VIATICO SIN PROG
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ListarViaticoProg]
@idConductor INT
AS
BEGIN
	SELECT TOP(1) C.Nombre AS 'CONDUCTOR', V.Fecha AS 'FECHA_VIATICO', V.CodGasto AS 'PLANILLA', V.Descripcion AS 'TIPO_VIATICO',
	V.Monto AS 'IMPORTE' FROM ReportesApp_Operaciones_TicketGasto_Viaticos V
	LEFT JOIN OP_TR_Conductor C ON C.IdConductor = V.IdConductor
	LEFT JOIN ReportesApp_Operaciones_TicketGasto_TipoViaticos TV ON TV.idTipoViatico = V.idTipoViatico
	WHERE V.idConductor = @idConductor
	ORDER BY V.Fecha DESC
END

-------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 23-08-2023
-- Description:	GENERAR REINTEGRO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_GenerarReintegro]
@idConductor INT,
@Programacion INT,
@Placa VARCHAR(15),
@FechaLiquidacion DATETIME,
@Planilla INT,
@Gasto DECIMAL(10,2),
@Total DECIMAL(10,2),
@Reintegro DECIMAL(10,2),
@DescripcionRG VARCHAR(200),
@Usuario VARCHAR(100)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @Nro INT
DECLARE @NroRP INT
DECLARE @NroConductor INT
DECLARE @NroUsuario INT
DECLARE @NroAdelanto INT
DECLARE @MontoTotal MONEY
DECLARE @Proyecto VARCHAR(15)
DECLARE @TipoCambio MONEY
DECLARE @Persona INT
DECLARE @Contador INT
DECLARE @CuentaContable CHAR(20)
DECLARE @Devolucion DECIMAL(10,2)
DECLARE @Descuento DECIMAL(10,2)
DECLARE @Absoluto DECIMAL(10,2)
DECLARE @CodigoDocumento CHAR(10)
DECLARE @CuentaBancaria CHAR(15)
DECLARE @Sucursal CHAR(4)
DECLARE @UnidadReplicacion CHAR(4)

IF (EXISTS(SELECT Descripcion FROM AP_GastoAdelanto WHERE Descripcion = 'GV PLLA ' + CONVERT(VARCHAR(30),@Planilla) + ' (R)' AND Estado != 'AN')) BEGIN
	SET @Exito = '-1 = Este reintegro ya ha sido generado.'
	GOTO Terminar
END

IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_TicketGasto_UsuarioSucursal WHERE Usuario = @Usuario AND Estado = 'A')) BEGIN
	SET @CuentaBancaria = (SELECT CuentaBancaria FROM ReportesApp_Operaciones_TicketGasto_UsuarioSucursal WHERE Usuario = @Usuario)
	SET @Sucursal = (SELECT Sucursal FROM ReportesApp_Operaciones_TicketGasto_UsuarioSucursal WHERE Usuario = @Usuario)
	SET @UnidadReplicacion = (SELECT TOP(1) UnidadReplicacion FROM AP_GastoAdelanto WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Descripcion LIKE '%' + 'GASTOS DE VIAJE ' + '%' AND Estado != 'AN')
END
ELSE BEGIN
	SET @Exito = '-1 = Este usuario está inactivo o no tiene una sede registrada.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	SET @Placa = LTRIM(RTRIM(@Placa))
	SET @Absoluto = ABS(@Reintegro)

	UPDATE CorrelativosMast
	SET CorrelativoNumero = (SELECT CorrelativoNumero + 1 FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='APAD')),
		UltimaFechaModif = GETDATE()
	WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='APAD')
	
	SET @Nro = (SELECT CorrelativosMast.CorrelativoNumero FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='APAD'))

	DELETE FROM AP_GastoAdelantoSustento WHERE (AP_GastoAdelantoSustento.UnidadReplicacion = @UnidadReplicacion) AND (AP_GastoAdelantoSustento.TipoAdelanto ='E') AND (AP_GastoAdelantoSustento.NumeroAdelanto = @Nro) 

	--INSERTAR DATOS EN CABECERA DE REPORTES DE GASTO
	SET @NroConductor = (SELECT P.Persona FROM ReportesApp_Operaciones_TicketGasto_Registro R
						 LEFT JOIN OP_TR_Conductor C ON C.IdConductor = R.ViConductor
						 LEFT JOIN PersonaMast P ON P.Persona = C.IdPersona WHERE CONVERT(VARCHAR(30),@Planilla) = R.CodGasto)		 
	SET @NroUsuario = (SELECT P.Persona FROM Usuario U LEFT JOIN PersonaMast P ON P.NombreCompleto = U.Nombre WHERE Usuario = @Usuario)
	SET @Proyecto = (SELECT Proyecto FROM OP_TR_Vehiculo WHERE NumeroPlaca = @Placa AND Estado = 2)
	
	--INSERTAR ADELANTO DE GASTO
	INSERT INTO AP_GastoAdelanto(NumeroAdelanto,CompaniaSocio,Clasificacion,FechaDocumento,TipoPago,Persona,PreparadoPor,FechaPreparacion,MonedaDocumento,MontoTotal,SaldoAdelanto,Descripcion,Estado,UltimoUsuario,
								 UltimaFechaModif,FechaEsperadaPago,TipoAdelanto,NumeroDocumentoInterno,FlujodeCaja,UnidadReplicacion,PartidaPresupuestal,PersonaPagara,ConceptoGasto,CentroCostos,EmpleadoAutorizado,
								 UnidadNegocio,RegComprasMontoAfecto,RegComprasMontoNoAfecto,RegComprasMontoIgv,CuentaBancaria)
	VALUES (@Nro,'10000000','ER',@FechaLiquidacion,'EF', @NroConductor, @NroUsuario, GETDATE(), 'LO', @Absoluto, @Absoluto, 'GV PLLA ' + CONVERT(VARCHAR(30),@Planilla) + ' (R)',
			'PR', @Usuario, GETDATE(), GETDATE(), 'E', CONVERT(VARCHAR(30),@Planilla), '051', @UnidadReplicacion, '9999', @NroConductor, '0006', '010201', @NroUsuario, 'TRAN', 0.00, 0.00, 0.00, @CuentaBancaria)

	INSERT INTO AP_GastoAdelantoSustento(UnidadReplicacion,TipoAdelanto,NumeroAdelanto,Secuencia,ConceptoGasto,NumeroVeces,Moneda,PrecioUnitario,MontoTotal,UltimoUsuario,UltimaFechaModif,CuentaContable,MontoPresupuesto)
	VALUES (@UnidadReplicacion, 'E', @Nro, 1, '0006', 8, 'LO', @Absoluto, @Absoluto, @Usuario, GETDATE(), '1413002', 0.00)

	UPDATE AP_GastoAdelanto
	SET AprobadoPor = @NroUsuario, FechaAprobacion = GETDATE(), Estado = 'AP'
	WHERE NumeroAdelanto = @Nro AND TipoAdelanto = 'E' AND UnidadReplicacion = @UnidadReplicacion

	--INSERTAR OBLIGACIÓN
	DECLARE @Nro2 INT
			
	UPDATE CorrelativosMast
	SET CorrelativoNumero = (SELECT CorrelativoNumero + 1 FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie = 'APNO')),
		UltimaFechaModif = GETDATE()
	WHERE (CorrelativosMast.CompaniaCodigo = '999999') AND (CorrelativosMast.TipoComprobante = 'SY') AND (CorrelativosMast.Serie = 'APNO')
	SET @Nro2 = (SELECT CorrelativosMast.CorrelativoNumero FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo = '999999') AND (CorrelativosMast.TipoComprobante = 'SY') AND (CorrelativosMast.Serie = 'APNO'))

	INSERT INTO Obligaciones (Proveedor,TipoDocumento,NumeroDocumento,CuentaBancaria,CompaniaCodigo,TipoPago,FechaRegistro,FechaVencimiento,FechaVencimientoOriginal,GenerarPago,TipoServicio,MonedaDocumento,ConversionRequerida,
							  MonedaPago,MontoObligacion,MontoImpuestoVentas,MontoNoAfecto,MontoImponible,MontoAdelantos,MontoImpuestos,NetoMonedaLocal,NetoMonedaExtranjera,TipoDeCambio,AprobadoPor,AprobadoCP1,AprobadoCP2,
							  IngresadoPor,RevisadoPor,EstadoDocumento,ContabilizacionPendiente,ChequeIndividual,Voucher,NumeroPago,NumeroProceso,ProcesoSecuencia,RegistroNumero,Comentarios,UltimaFechaModif,UnidadNegocio,
							  FacturaAfectaSplitFlag,FactorRValidacion,UnidadReplicacion,CanjeRegistroNumero,MontoPagoParcial,NumeroDocumentoInterno,CentroCosto,PartidaPresupuestal,FlujodeCaja,CentroCostoCP,FechaRecepcion,
							  ProveedorPagarA,ControlPresupuestalFlag,CargoFlag,MontoCreditoFiscal,FechaDocumento,AfectoIGVFlag,DiferidoFlag,AdelantoFlag)
	VALUES (@NroConductor,'AE',@UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@Nro),@CuentaBancaria,'100000','EF',GETDATE(),GETDATE(),GETDATE(),'S','INAFEC','LO','D','LO',@Absoluto,0.00,0.00,@Absoluto,0.00,0.00,@Absoluto,
	0.00,0,0,0,0,@NroUsuario, @NroUsuario, 'RV', 'S', 'N', CONVERT(CHAR(4),YEAR(GETDATE()))+CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(GETDATE()))),2)),0,0,0,@Nro2,'GV PLLA ' + CONVERT(VARCHAR(30),@Planilla) + ' (R)',
	GETDATE(),'TRAN','N','N',@UnidadReplicacion,0,0.00,CONVERT(VARCHAR(30),@Planilla),'010201','9999','051','010201',GETDATE(),@NroConductor, 'N', 'N', 0.00, GETDATE(), 'N', 'N', 'N')

	INSERT INTO ObligacionesXCuenta (Descripcion,Proveedor,NumeroDocumento,Linea,Monto,CentroCosto,CuentaContable,PartidaPresupuestal,Persona,DocumentoReferencia,Sucursal,FlujodeCaja,NoAfectoIGVFlag,TipoDocumento)
	VALUES ('GV PLLA ' + CONVERT(VARCHAR(30),@Planilla) + ' (R)',@NroConductor,@UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@Nro),1,@Absoluto,'010201','1413002','9999', @NroConductor,
	'AE-'+@UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@Nro),@Sucursal, '051', 'N', 'AE')

	UPDATE AP_GastoAdelanto
	SET MonedaDocumento = 'LO',ObligacionTipoDocumento = 'AE',ObligacionNumeroDocumento = @UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@Nro),Estado = 'TR'
	WHERE UnidadReplicacion = @UnidadReplicacion AND TipoAdelanto = 'E' AND NumeroAdelanto = @Nro 

	INSERT INTO AP_ObligacionFlujo (Proveedor,TipoDocumento,NumeroDocumento,Secuencia,AreaDocumentaria,Comentario,Estado,UltimoUsuario,UltimaFechaModif)
	VALUES (@NroConductor, 'AE',@UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@Nro), 1,'001','Recepción Inicial','A',@Usuario,GETDATE())

	DECLARE @NombreConductor VARCHAR(250)
	SET @NombreConductor = (SELECT P.NombreCompleto FROM ReportesApp_Operaciones_TicketGasto_Registro R
						 LEFT JOIN OP_TR_Conductor C ON C.IdConductor = R.ViConductor
						 LEFT JOIN PersonaMast P ON P.Persona = C.IdPersona WHERE CONVERT(VARCHAR(30),@Planilla) = R.CodGasto)		 

	--INSERTAR ORDEN DE PAGO
	INSERT INTO OrdenPago (SistemaFuente,Proveedor,TipoDocumento,NumeroDocumento,PagarA,CompaniaCodigo,CuentaBancaria,TipoPago,ChequeIndividual,FechaVencimiento,MonedaDocumento,MonedaPago,MontoMoneda,TipodeCambio,
						   BatchNumber,FechaTransferencia,Estado,FlujodeCaja,RegistroNumero,FechaDocumento,PagoDiferidoFlag,UnidadNegocio)
	VALUES ('AP',@NroConductor,'AE',@UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@Nro),@NombreConductor,'100000',@CuentaBancaria,'EF','N',GETDATE(),'LO','LO',@Absoluto,0,0,GETDATE(),'AP','051', @Nro2, GETDATE(),'N','TRAN')

	SET @TipoCambio = (CONVERT(MONEY,(SELECT TipoCambioMast.FactorVenta FROM TipoCambioMast WHERE TipoCambioMast.MonedaCodigo ='EX' AND TipoCambioMast.MonedaCambioCodigo ='LO' AND TipoCambioMast.FechaCambio =CONVERT(DATE,GETDATE()))))

	UPDATE Obligaciones
	SET AprobadoCP1 = @NroUsuario, EstadoDocumento = 'AP', Voucher = CONVERT(CHAR(4),YEAR(GETDATE()))+CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(GETDATE()))),2)),
		FechaAprobacion = GETDATE(), TipodeCambioProvision = @TipoCambio, UltimoUsuario = @Usuario, UltimaFechaModif = GETDATE()
	WHERE Proveedor = @NroConductor AND TipoDocumento = 'AE' AND NumeroDocumento = @UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@Nro)

	DELETE XOrdenPago WHERE XOrdenPago.Usuario = @Usuario

	INSERT INTO XOrdenPago (TipoRegistro,SistemaFuente,Proveedor,TipoDocumento,NumeroDocumento,CompaniaCodigo,PagarA,CuentaBancaria,TipoPago,ChequeIndividual,MonedaPago,FechaVencimiento,Estado,MonedaDocumento,MontoMoneda,Usuario,MontoOriginal)
	VALUES ('D','AP',@NroConductor,'AE',@UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@Nro),'100000',@NombreConductor,@CuentaBancaria,'EF','N','LO',GETDATE(),'A','LO',@Absoluto,@Usuario,@Absoluto)
	
	INSERT INTO XOrdenPago (TipoRegistro,SistemaFuente,Proveedor,TipoDocumento,NumeroDocumento,CompaniaCodigo,PagarA,CuentaBancaria,TipoPago,ChequeIndividual,MonedaPago,Estado,MontoMoneda,Usuario)
	VALUES ('S','XX',@NroConductor,'ZZ','000001','100000',@NombreConductor,@CuentaBancaria,'EF','N','LO','A',@Absoluto,@Usuario)

	SET @Exito = '0 = Reintegro Generado Exitosamente.'
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
-- Create date: 02-08-2023
-- Description:	LIQUIDAR TICKETS DE GASTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_LiquidarTicketGasto]
@idConductor INT,
@Programacion INT,
@Placa VARCHAR(15),
@FechaLiquidacion DATETIME,
@Planilla INT,
@Gasto DECIMAL(10,2),
@Total DECIMAL(10,2),
@Reintegro DECIMAL(10,2),
@DescripcionRG VARCHAR(200),
@Usuario VARCHAR(100)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @Nro INT
DECLARE @NroRP INT
DECLARE @NroConductor INT
DECLARE @NroUsuario INT
DECLARE @NroAdelanto INT
DECLARE @MontoTotal MONEY
DECLARE @Proyecto VARCHAR(15)
DECLARE @TipoCambio MONEY
DECLARE @Persona INT
DECLARE @Contador INT
DECLARE @CuentaContable CHAR(20)
DECLARE @Devolucion DECIMAL(10,2)
DECLARE @Descuento DECIMAL(10,2)
DECLARE @Absoluto DECIMAL(10,2)
DECLARE @CodigoDocumento CHAR(10)
DECLARE @NombreConductor VARCHAR(350)
DECLARE @CuentaBancaria CHAR(15)
DECLARE @Sucursal CHAR(4)
DECLARE @Sucursal2 VARCHAR(4)
DECLARE @UnidadReplicacion CHAR(4)

SET @Exito = '0 = Comprobantes Liquidados Exitosamente.'

IF (@Reintegro > 0.00) BEGIN
	SET @Exito = '-1 = El saldo debe cuadrar con el gasto.'
	GOTO Terminar
END

IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_TicketGasto_UsuarioSucursal WHERE Usuario = @Usuario AND Estado = 'A')) BEGIN
	SET @CuentaBancaria = (SELECT CuentaBancaria FROM ReportesApp_Operaciones_TicketGasto_UsuarioSucursal WHERE Usuario = @Usuario)
	SET @Sucursal = (SELECT Sucursal FROM ReportesApp_Operaciones_TicketGasto_UsuarioSucursal WHERE Usuario = @Usuario)
	SET @Sucursal2 = (SELECT CONVERT(VARCHAR(4),Sucursal) FROM ReportesApp_Operaciones_TicketGasto_UsuarioSucursal WHERE Usuario = @Usuario)
	SET @UnidadReplicacion = (SELECT TOP(1) UnidadReplicacion FROM AP_GastoAdelanto WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Descripcion LIKE '%' + 'GASTOS DE VIAJE ' + '%' AND Estado != 'AN')
END
ELSE BEGIN
	SET @Exito = '-1 = Este usuario está inactivo en Tabla Sucursales o no tiene una sede registrada. Revisar con Sistemas'
	GOTO Terminar
END

--Se colocó control AlcoholTest: 08-03-2024.
-------------------------------------------
IF (NOT EXISTS(SELECT TOP 1(1) FROM ReportesApp_Seguridad_AlcoholTest_Registro WHERE idPersona = @Persona and cast(Fecha as date)=cast(getdate() as date)) AND (SELECT TOP 1 Estado FROM PersonaMast where Persona=@Persona)='A' ) BEGIN
	SET @Exito = '-2 = La persona a la que liquida, debe pasar su control Alcohol Test. No puede concluir el proceso. Enviarla a Garita.'
	GOTO Terminar
END

IF (EXISTS(SELECT * FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Estado NOT IN ('AN','PA'))) BEGIN
	DECLARE @AdelantosPendientes INT = (SELECT COUNT(*) FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Estado NOT IN ('AN','PA'))
	
	SET @Exito = '-2 = La planilla '+CONVERT(VARCHAR(30),@Planilla)+' contiene '+ CONVERT(VARCHAR,@AdelantosPendientes) +' adelanto(s) que aún no se han pagado.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	SET @Placa = LTRIM(RTRIM(@Placa))
	
	--ACTUALIZAR CORRELATIVO DE REPORTES DE GASTO
	UPDATE CorrelativosMast
	SET CorrelativoNumero = (SELECT CorrelativoNumero + 1 FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie = 'APER')),
		UltimaFechaModif = GETDATE()
	WHERE (CorrelativosMast.CompaniaCodigo = '999999') AND (CorrelativosMast.TipoComprobante = 'SY') AND (CorrelativosMast.Serie = 'APER')
	SET @NroRP = (SELECT CorrelativosMast.CorrelativoNumero FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo = '999999') AND (CorrelativosMast.TipoComprobante = 'SY') AND (CorrelativosMast.Serie = 'APER'))
	
	DELETE FROM AP_CajaChicaDistribucion WHERE (AP_CajaChicaDistribucion.CajaChicaReporteFlag='R') AND (AP_CajaChicaDistribucion.UnidadReplicacion=@UnidadReplicacion) AND (AP_CajaChicaDistribucion.CajaChicaNumero=@NroRP)
	
	--INSERTAR DATOS EN CABECERA DE REPORTES DE GASTO
	/*SET @NroConductor = (SELECT P.Persona FROM ReportesApp_Operacion_Previaje_Registros R
						 LEFT JOIN OP_TR_Conductor C ON C.IdConductor = R.IdConductor
						 LEFT JOIN PersonaMast P ON P.Persona = C.IdPersona WHERE R.NroTicket = @Programacion)
	*/
	SET @NroConductor = (SELECT P.Persona FROM ReportesApp_Operaciones_TicketGasto_Registro R
						 LEFT JOIN OP_TR_Conductor C ON C.IdConductor = R.ViConductor
						 LEFT JOIN PersonaMast P ON P.Persona = C.IdPersona WHERE CONVERT(VARCHAR(30),@Planilla) = R.CodGasto)				 
	SET @NroUsuario = (SELECT P.Persona FROM Usuario U LEFT JOIN PersonaMast P ON P.NombreCompleto = U.Nombre WHERE Usuario = @Usuario)
	SET @NroAdelanto = (SELECT MIN(NumeroAdelanto) FROM AP_GastoAdelanto WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(20),@Planilla) AND Estado != 'AN')
	SET @MontoTotal = (SELECT SUM(MontoTotal) FROM AP_GastoAdelanto WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(20),@Planilla) AND Estado != 'AN')
	SET @Proyecto = (SELECT Proyecto FROM OP_TR_Vehiculo WHERE NumeroPlaca = @Placa AND Estado = 2)
		 		 
	INSERT INTO AP_CajaChica (UnidadReplicacion,CajaChicaNumero,CompaniaSocio,MonedaPago,MontoTotal,Descripcion,Beneficiario,PreparadoPor,FechaPreparacion,UltimoUsuario,UltimaFechaModif,Clasificacion,DefaultAfe,
				CajaChicaReporteFlag,MontoAdelantos,MontoNeto,NumeroAdelanto,DefaultPrime,NumeroDocumentoInterno,PersonaPagara,UnidadNegocio,TipoPago,Estado,FlujodeCaja,DefaultCampoReferencia)
	SELECT @UnidadReplicacion, @NroRP, '10000000', 'LO', @Gasto, @DescripcionRG, @NroConductor, @NroUsuario, @FechaLiquidacion, @Usuario, GETDATE(), 'ER', @Proyecto, 'R', @MontoTotal, 0.00, @NroAdelanto, '010201', CONVERT(CHAR,@Planilla),
	@NroConductor, 'TRAN', 'CH', 'PR', '051', '90'
	
	--INSERTAR DATOS DE DETALLE DE REPORTES DE GASTO
	SET @TipoCambio = (CONVERT(MONEY,(SELECT TipoCambioMast.FactorVenta FROM TipoCambioMast WHERE TipoCambioMast.MonedaCodigo ='EX' AND TipoCambioMast.MonedaCambioCodigo ='LO' AND TipoCambioMast.FechaCambio =CONVERT(DATE,@FechaLiquidacion))))
	SET @Devolucion = 0.00
	SET @Descuento = 0.00
	SET @Absoluto = 0.00
	SET @Contador = 1
	
	WHILE(@Contador <= (SELECT COUNT(idNroLiquidacion) FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla))) BEGIN
		IF((SELECT NroDocumento FROM ReportesApp_Operaciones_TicketGasto_Liquidacion WHERE CodGasto = @Planilla AND idNroLiquidacion = @Contador) != '') BEGIN
			SET @CodigoDocumento = (SELECT L.CodigoDocumento FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador)

			IF (@CodigoDocumento = 'OT') BEGIN
				SET @NombreConductor = (SELECT P.NombreCompleto FROM ReportesApp_Operaciones_TicketGasto_Registro R
										LEFT JOIN OP_TR_Conductor C ON C.IdConductor = R.ViConductor
										LEFT JOIN PersonaMast P ON P.Persona = C.IdPersona WHERE CONVERT(VARCHAR(30),@Planilla) = R.CodGasto)

				INSERT INTO AP_CajaChicaDetalle (UnidadReplicacion,CajaChicaNumero,Secuencia,ConceptoGasto,Fecha,Descripcion,MontoMonedaPago,TipoImpuestoFlag,MonedaDocumento,TipodeCambio,MontoAfecto,MontoNoAfecto,MontoImpuesto,
							MontoTotal,RegistroComprasPeriodo,FechaDocumento,CajaChicaReporteFlag,CampoReferencia,RetencionFlag,RetencionMonto,CentroCosto,MontoAFPAporte,MontoAFPComision,MontoAFPSeguro,TipoDocumento,NumeroDocumento,
							Proveedor, ProveedorNombre)
				SELECT @UnidadReplicacion,@NroRP,L.idNroLiquidacion,L.ConceptoGasto,L.FechaLiquidacion,L.DescripcionGasto,L.MontoPagado,L.TipoImpuesto,'LO',@TipoCambio,L.MontoAfecto,L.MontoNoAfecto,L.MontoImpuestos,L.MontoPagado - L.MontoImpuestos,'000000',
					    L.FechaLiquidacion, 'R', '90', 'N', 0.00, '010201', 0.00, 0.00, 0.00,L.CodigoDocumento,L.NroDocumento, @NroConductor, @NombreConductor
				FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador

				IF((SELECT L.ConceptoGasto FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador) = '0062') BEGIN
					SET @Descuento = (SELECT L.MontoPagado FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador)
				END
			END
			ELSE BEGIN
				IF((SELECT L.ConceptoGasto FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador) = '0107') BEGIN
					SET @NombreConductor = (SELECT P.NombreCompleto FROM ReportesApp_Operaciones_TicketGasto_Registro R
											LEFT JOIN OP_TR_Conductor C ON C.IdConductor = R.ViConductor
											LEFT JOIN PersonaMast P ON P.Persona = C.IdPersona WHERE CONVERT(VARCHAR(30),@Planilla) = R.CodGasto)

					INSERT INTO AP_CajaChicaDetalle (UnidadReplicacion,CajaChicaNumero,Secuencia,ConceptoGasto,Fecha,Descripcion,MontoMonedaPago,TipoImpuestoFlag,MonedaDocumento,TipodeCambio,MontoAfecto,MontoNoAfecto,MontoImpuesto,
								MontoTotal,RegistroComprasPeriodo,FechaDocumento,CajaChicaReporteFlag,CampoReferencia,RetencionFlag,RetencionMonto,CentroCosto,MontoAFPAporte,MontoAFPComision,MontoAFPSeguro, Proveedor, ProveedorNombre, NumeroDocumento)
					SELECT @UnidadReplicacion,@NroRP,L.idNroLiquidacion,L.ConceptoGasto,L.FechaLiquidacion,L.DescripcionGasto,L.MontoPagado,L.TipoImpuesto,'LO',@TipoCambio,L.MontoAfecto,L.MontoNoAfecto,L.MontoImpuestos,L.MontoPagado - L.MontoImpuestos,'000000',
						   L.FechaLiquidacion, 'R', '90', 'N', 0.00, '010201', 0.00, 0.00, 0.00, @NroConductor, @NombreConductor, L.NroDocumento
					FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador
				END
				ELSE BEGIN
					INSERT INTO AP_CajaChicaDetalle (UnidadReplicacion,CajaChicaNumero,Secuencia,ConceptoGasto,Fecha,Descripcion,MontoMonedaPago,TipoImpuestoFlag,DocumentoFiscal,Proveedor,TipoDocumento,MonedaDocumento,TipodeCambio,
								MontoAfecto,MontoNoAfecto,MontoImpuesto,MontoTotal,ProveedorNombre,RegistroComprasPeriodo,FechaDocumento,CajaChicaReporteFlag,NumeroDocumento,CampoReferencia,RetencionFlag,RetencionMonto,CentroCosto,
								MontoAFPAporte,MontoAFPComision,MontoAFPSeguro)
					SELECT @UnidadReplicacion,@NroRP,L.idNroLiquidacion,L.ConceptoGasto,L.FechaLiquidacion,L.DescripcionGasto,L.MontoPagado,L.TipoImpuesto,L.NroRUC,(SELECT Persona FROM PersonaMast WHERE NombreCompleto = L.NombreCompleto),
							L.CodigoDocumento,'LO',@TipoCambio,L.MontoAfecto,L.MontoNoAfecto,L.MontoImpuestos,L.MontoPagado - L.MontoImpuestos,L.NombreCompleto,'000000',L.FechaLiquidacion,'R',L.NroDocumento, '90', 'N', 0.00, '010201', 0.00, 0.00, 0.00
					FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador
				END
			END

			SET @CuentaContable = (SELECT CuentaContable FROM AP_ConceptoGasto WHERE ConceptoGasto = (SELECT L.ConceptoGasto FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador))
			
			IF ((SELECT L.MontoAfecto FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador) != 0) BEGIN
				INSERT INTO AP_CajaChicaDistribucion (CajaChicaReporteFlag,UnidadReplicacion,CajaChicaNumero,Secuencia,Linea,Afe,Monto,CuentaContable,CentroCosto,Sucursal,CampoReferencia,MontoTotal,ConceptoGasto)
				SELECT 'R', @UnidadReplicacion, @NroRP, idNroLiquidacion, 1, @Proyecto, L.MontoAfecto, @CuentaContable, '010201', @Sucursal, '90', L.MontoPagado, L.ConceptoGasto
				FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador
				SET @Contador = @Contador + 1
			END
			ELSE BEGIN
				INSERT INTO AP_CajaChicaDistribucion (CajaChicaReporteFlag,UnidadReplicacion,CajaChicaNumero,Secuencia,Linea,Afe,Monto,CuentaContable,CentroCosto,Sucursal,CampoReferencia,MontoTotal,ConceptoGasto)
				SELECT 'R', @UnidadReplicacion, @NroRP, idNroLiquidacion, 1, @Proyecto, L.MontoNoAfecto, @CuentaContable, '010201', @Sucursal, '90', L.MontoPagado, L.ConceptoGasto
				FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador
				SET @Contador = @Contador + 1
			END
		END
		ELSE BEGIN
			SET @CodigoDocumento = (SELECT L.CodigoDocumento FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador)
			SET @CuentaContable = (SELECT CuentaContable FROM AP_ConceptoGasto WHERE ConceptoGasto = (SELECT L.ConceptoGasto FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador))

			IF((SELECT L.ConceptoGasto FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador) = '0105') BEGIN
				SET @NombreConductor = (SELECT P.NombreCompleto FROM ReportesApp_Operaciones_TicketGasto_Registro R
										LEFT JOIN OP_TR_Conductor C ON C.IdConductor = R.ViConductor
										LEFT JOIN PersonaMast P ON P.Persona = C.IdPersona WHERE CONVERT(VARCHAR(30),@Planilla) = R.CodGasto)

				INSERT INTO AP_CajaChicaDetalle (UnidadReplicacion,CajaChicaNumero,Secuencia,ConceptoGasto,Fecha,Descripcion,MontoMonedaPago,TipoImpuestoFlag,MonedaDocumento,TipodeCambio,MontoAfecto,MontoNoAfecto,MontoImpuesto,
							MontoTotal,RegistroComprasPeriodo,FechaDocumento,CajaChicaReporteFlag,CampoReferencia,RetencionFlag,RetencionMonto,CentroCosto,MontoAFPAporte,MontoAFPComision,MontoAFPSeguro, Proveedor, ProveedorNombre)
				SELECT @UnidadReplicacion,@NroRP,L.idNroLiquidacion,L.ConceptoGasto,L.FechaLiquidacion,L.DescripcionGasto,L.MontoPagado,L.TipoImpuesto,'LO',@TipoCambio,L.MontoAfecto,L.MontoNoAfecto,L.MontoImpuestos,L.MontoPagado - L.MontoImpuestos,'000000',
					   L.FechaLiquidacion, 'R', '90', 'N', 0.00, '010201', 0.00, 0.00, 0.00, @NroConductor, @NombreConductor
				FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador
			END
			ELSE BEGIN
				INSERT INTO AP_CajaChicaDetalle (UnidadReplicacion,CajaChicaNumero,Secuencia,ConceptoGasto,Fecha,Descripcion,MontoMonedaPago,TipoImpuestoFlag,MonedaDocumento,TipodeCambio,MontoAfecto,MontoNoAfecto,MontoImpuesto,
							MontoTotal,RegistroComprasPeriodo,FechaDocumento,CajaChicaReporteFlag,CampoReferencia,RetencionFlag,RetencionMonto,CentroCosto,MontoAFPAporte,MontoAFPComision,MontoAFPSeguro)
				SELECT @UnidadReplicacion,@NroRP,L.idNroLiquidacion,L.ConceptoGasto,L.FechaLiquidacion,L.DescripcionGasto,L.MontoPagado,L.TipoImpuesto,'LO',@TipoCambio,L.MontoAfecto,L.MontoNoAfecto,L.MontoImpuestos,L.MontoPagado - L.MontoImpuestos,'000000',
					   L.FechaLiquidacion, 'R', '90', 'N', 0.00, '010201', 0.00, 0.00, 0.00
				FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador
			END

			IF((SELECT L.ConceptoGasto FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador) = '0078') BEGIN
				SET @Devolucion = (SELECT L.MontoPagado FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador)
			END

			INSERT INTO AP_CajaChicaDistribucion (CajaChicaReporteFlag,UnidadReplicacion,CajaChicaNumero,Secuencia,Linea,Afe,Monto,CuentaContable,CentroCosto,Sucursal,CampoReferencia,MontoTotal,ConceptoGasto)
			SELECT 'R', @UnidadReplicacion, @NroRP, idNroLiquidacion, 1, @Proyecto, L.MontoPagado, @CuentaContable, '010201', @Sucursal, '90', L.MontoPagado, L.ConceptoGasto
			FROM ReportesApp_Operaciones_TicketGasto_Liquidacion L WHERE L.CodGasto = CONVERT(VARCHAR,@Planilla) AND L.idNroLiquidacion = @Contador
			SET @Contador = @Contador + 1
		END
	END

	--INSERTAR GASTO DE ADELANTO
	IF ((SELECT COUNT(A.NumeroAdelanto) FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Estado != 'AN') > 1) BEGIN
		DECLARE @TABLA_ADELANTOS TABLE(Nro INT, NumeroAdelanto INT, MontoTotal DECIMAL(10,2), Descripcion VARCHAR(250), Persona INT, Estado VARCHAR(5))
		DECLARE @Contador2 INT = 1

		INSERT INTO @TABLA_ADELANTOS(Nro, NumeroAdelanto, MontoTotal, Descripcion, Persona, Estado)
		SELECT ROW_NUMBER() OVER(ORDER BY NumeroAdelanto ASC) AS 'NRO', NumeroAdelanto, MontoTotal, Descripcion, Persona, Estado
		FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Descripcion NOT LIKE 'GV PLLA ' + '%' AND Estado = 'PA'
		ORDER BY NumeroAdelanto ASC

		WHILE(@Contador2 <= (SELECT COUNT(Nro) FROM @TABLA_ADELANTOS)) BEGIN
			IF (EXISTS(SELECT * FROM @TABLA_ADELANTOS WHERE Nro = @Contador2 AND Descripcion LIKE '%' + 'GASTOS DE VIAJE ' + '%')) BEGIN
				INSERT INTO AP_GastoAdelantoPagos (UnidadReplicacion,TipoAdelanto,NumeroAdelanto,Secuencia,ObligacionPagoFlag,ObligacionProveedor,ObligacionTipoDocumento,ObligacionNumeroDocumento,Monto,Estado,UltimoUsuario,UltimaFechaModif)
				SELECT TOP(1) @UnidadReplicacion, 'E', A.NumeroAdelanto, 1, 'O', A.Persona, 'ER', @UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@NroRP), A.MontoTotal, 'PR', @Usuario, GETDATE()
				FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Descripcion LIKE '%' + 'GASTOS DE VIAJE ' + '%' AND Estado != 'AN'
			END
			ELSE BEGIN
				DECLARE @GastoDiferencial DECIMAL(10,2)
				DECLARE @AdelantoAdicional INT
				DECLARE @Persona2 INT
				
				SET @AdelantoAdicional = (SELECT NumeroAdelanto FROM @TABLA_ADELANTOS A WHERE Nro = @Contador2)
				SET @GastoDiferencial = (SELECT MontoTotal FROM @TABLA_ADELANTOS A WHERE Nro = @Contador2)
				SET @Persona2 = (SELECT Persona FROM @TABLA_ADELANTOS A WHERE Nro = @Contador2)

				UPDATE AP_CajaChica
				SET MontoAdelantos = MontoAdelantos + @GastoDiferencial, NumeroAdelanto = ISNULL(@AdelantoAdicional,NumeroAdelanto)
				WHERE CajaChicaNumero = @NroRP

				--SELECT * FROM AP_GastoAdelantoPagos WHERE ObligacionNumeroDocumento = 'TRUJ-257591'
			
				INSERT INTO AP_GastoAdelantoPagos (UnidadReplicacion,TipoAdelanto,NumeroAdelanto,Secuencia,ObligacionPagoFlag,ObligacionProveedor,ObligacionTipoDocumento,ObligacionNumeroDocumento,Monto,Estado,UltimoUsuario,UltimaFechaModif)
				SELECT @UnidadReplicacion, 'E', @AdelantoAdicional, 1, 'O', @Persona2, 'ER', @UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@NroRP), @GastoDiferencial, 'PR', @Usuario, GETDATE()
			END

			SET @Contador2 = @Contador2 + 1
		END

		/*
		DECLARE @GastoDiferencial DECIMAL(10,2)
		DECLARE @AdelantoAdicional INT

		IF (EXISTS(SELECT * FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Descripcion LIKE 'VIATICO ADICIONAL:' + '%' AND Estado != 'AN')) BEGIN
			SET @AdelantoAdicional = (SELECT TOP(1) NumeroAdelanto FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Descripcion LIKE 'VIATICO ADICIONAL:' + '%' AND Estado != 'AN')
			SET @GastoDiferencial = (SELECT TOP(1) MontoTotal FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Descripcion LIKE 'VIATICO ADICIONAL:' + '%' AND Estado != 'AN')
		END
		ELSE BEGIN
			SET @AdelantoAdicional = (SELECT TOP(1) NumeroAdelanto FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Descripcion LIKE 'REINTEGRO POR CAMBIO DE RUTA' + '%' AND Estado != 'AN')
			SET @GastoDiferencial = (SELECT TOP(1) MontoTotal FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Descripcion LIKE 'REINTEGRO POR CAMBIO DE RUTA' + '%' AND Estado != 'AN')
		END

		UPDATE AP_CajaChica
		SET MontoAdelantos = MontoAdelantos + @GastoDiferencial, NumeroAdelanto = ISNULL(@AdelantoAdicional,NumeroAdelanto)
		WHERE CajaChicaNumero = @NroRP

		IF (EXISTS(SELECT * FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Descripcion LIKE 'VIATICO ADICIONAL:' + '%' AND Estado != 'AN')) BEGIN
			UPDATE AP_GastoAdelantoPagos
			SET Monto = @Total - @GastoDiferencial
			WHERE ObligacionNumeroDocumento = @UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@NroRP)
			
			INSERT INTO AP_GastoAdelantoPagos (UnidadReplicacion,TipoAdelanto,NumeroAdelanto,Secuencia,ObligacionPagoFlag,ObligacionProveedor,ObligacionTipoDocumento,ObligacionNumeroDocumento,Monto,Estado,UltimoUsuario,UltimaFechaModif)
			SELECT TOP(1) @UnidadReplicacion, 'E', @AdelantoAdicional, 1, 'O', A.Persona, 'ER', @UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@NroRP), @GastoDiferencial, 'PR', @Usuario, GETDATE()
			FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Descripcion LIKE '%' + 'VIATICO ADICIONAL:' + '%' AND Estado != 'AN'
		END
		ELSE BEGIN
			IF (EXISTS(SELECT * FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Descripcion LIKE '%' + 'REINTEGRO POR CAMBIO DE RUTA:' + '%' AND Estado != 'AN')) BEGIN
				INSERT INTO AP_GastoAdelantoPagos (UnidadReplicacion,TipoAdelanto,NumeroAdelanto,Secuencia,ObligacionPagoFlag,ObligacionProveedor,ObligacionTipoDocumento,ObligacionNumeroDocumento,Monto,Estado,UltimoUsuario,UltimaFechaModif)
				SELECT TOP(1) @UnidadReplicacion, 'E', @AdelantoAdicional, 1, 'O', A.Persona, 'ER', @UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@NroRP), @GastoDiferencial, 'PR', @Usuario, GETDATE()
				FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Descripcion LIKE '%' + 'REINTEGRO POR CAMBIO DE RUTA:' + '%' AND Estado != 'AN'
			END
		END
		*/
	END
	ELSE BEGIN
		INSERT INTO AP_GastoAdelantoPagos (UnidadReplicacion,TipoAdelanto,NumeroAdelanto,Secuencia,ObligacionPagoFlag,ObligacionProveedor,ObligacionTipoDocumento,ObligacionNumeroDocumento,Monto,Estado,UltimoUsuario,UltimaFechaModif)
		SELECT TOP(1) @UnidadReplicacion, 'E', A.NumeroAdelanto, 1, 'O', A.Persona, 'ER', @UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@NroRP), @Total, 'PR', @Usuario, GETDATE()
		FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Descripcion LIKE '%' + 'GASTOS DE VIAJE ' + '%' AND Estado != 'AN'
	END
	
	IF (@Reintegro = 0.00) BEGIN
		UPDATE ReportesApp_Operaciones_TicketGasto_Registro
		SET NroRepGasto = @NroRP, TotalGastado = @Total - (@Devolucion + @Descuento), Vuelto = @Devolucion + @Reintegro, Descuento = @Descuento, Reintegro = 0.00, FechaLiquidacion = @FechaLiquidacion, UsuarioLiquidacion = @Usuario, EstadoLiquidacion = 1, Sucursal = @Sucursal2
		WHERE CodGasto = CONVERT(VARCHAR(30),@Planilla)
	END
	ELSE BEGIN
		IF (@Reintegro < 0.00) BEGIN
			SET @Absoluto = ABS(@Reintegro)

			--ACTUALIZAR NRO PARA QUE BUSQUE EL REINTEGRO
			SET @Nro = (SELECT MAX(NumeroAdelanto) FROM AP_GastoAdelanto WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Descripcion LIKE 'GV PLLA ' + '%' AND Estado != 'AN')

			UPDATE AP_CajaChica
			SET MontoTotal = @Gasto + @Absoluto, MontoAdelantos = @Gasto + @Absoluto, NumeroAdelanto = @Nro
			WHERE CajaChicaNumero = @NroRP

			DECLARE @AdeMenor INT
			SET @AdeMenor = (SELECT MIN(A.NumeroAdelanto) FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Estado != 'AN')

			--UPDATE AP_GastoAdelantoPagos
			--SET Monto = (SELECT MontoTotal FROM AP_GastoAdelanto WHERE NumeroAdelanto = @AdeMenor AND Estado != 'AN')
			--WHERE NumeroAdelanto = (SELECT MIN(A.NumeroAdelanto) FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Estado != 'AN')

			IF (EXISTS(SELECT * FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND CuentaBancaria = 'FONDO GV LIMA' AND Descripcion LIKE 'PLLA ' + '%' AND Estado != 'AN')) BEGIN
				INSERT INTO AP_GastoAdelantoPagos (UnidadReplicacion,TipoAdelanto,NumeroAdelanto,Secuencia,ObligacionPagoFlag,ObligacionProveedor,ObligacionTipoDocumento,ObligacionNumeroDocumento,Monto,Estado,UltimoUsuario,UltimaFechaModif)
				SELECT TOP(1) @UnidadReplicacion, 'E', A.NumeroAdelanto, 1, 'O', A.Persona, 'ER', @UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@NroRP), A.MontoTotal, 'PR', @Usuario, GETDATE()
				FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND CuentaBancaria = 'FONDO GV LIMA' AND Descripcion LIKE 'PLLA ' + '%' AND Estado != 'AN'
			END
			ELSE BEGIN
				IF (EXISTS(SELECT * FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND CuentaBancaria = 'FONDO GV PIURA' AND Descripcion LIKE 'PLLA ' + '%' AND Estado != 'AN')) BEGIN
					INSERT INTO AP_GastoAdelantoPagos (UnidadReplicacion,TipoAdelanto,NumeroAdelanto,Secuencia,ObligacionPagoFlag,ObligacionProveedor,ObligacionTipoDocumento,ObligacionNumeroDocumento,Monto,Estado,UltimoUsuario,UltimaFechaModif)
					SELECT TOP(1) @UnidadReplicacion, 'E', A.NumeroAdelanto, 1, 'O', A.Persona, 'ER', @UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@NroRP), A.MontoTotal, 'PR', @Usuario, GETDATE()
					FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND CuentaBancaria = 'FONDO GV PIURA' AND Descripcion LIKE 'PLLA ' + '%' AND Estado != 'AN'
				END
				ELSE BEGIN
					INSERT INTO AP_GastoAdelantoPagos (UnidadReplicacion,TipoAdelanto,NumeroAdelanto,Secuencia,ObligacionPagoFlag,ObligacionProveedor,ObligacionTipoDocumento,ObligacionNumeroDocumento,Monto,Estado,UltimoUsuario,UltimaFechaModif)
					SELECT TOP(1) @UnidadReplicacion, 'E', @Nro, 1, 'O', A.Persona, 'ER', @UnidadReplicacion+'-'+CONVERT(VARCHAR(10),@NroRP), @Absoluto, 'PR', @Usuario, GETDATE()
					FROM AP_GastoAdelanto A WHERE NumeroDocumentoInterno = CONVERT(VARCHAR(30),@Planilla) AND Estado != 'AN'
				END
			END
			
			UPDATE ReportesApp_Operaciones_TicketGasto_Registro
			SET NroRepGasto = @NroRP, TotalGastado = @Total, Vuelto = 0.00, Descuento = 0.00, Reintegro = @Absoluto, FechaLiquidacion = @FechaLiquidacion, UsuarioLiquidacion = @Usuario, EstadoLiquidacion = 1, Sucursal = @Sucursal2
			WHERE CodGasto = CONVERT(VARCHAR(30),@Planilla)
		END
	END
	SET @Exito = '0 = Comprobantes Liquidados Exitosamente. RP: ' + CONVERT(VARCHAR(10),@NroRP)
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

---------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 07-10-2023
-- Description:	BUSCAR GASTO UNITARIO DE PEAJE
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_BuscarGastoPeaje]
@idPeaje INT,
@Opcion INT,
@NroRUC VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON;
	IF (@Opcion = 1) BEGIN		--LISTAR DESDE EL MAESTRO
		SELECT LTRIM(RTRIM(GP.NroRUC)) AS 'NroRUC', LTRIM(RTRIM(PM.NombreCompleto)) AS NombreCompleto, P.IdPeaje,
		P.Descripcion AS 'PEAJE', GP.Monto, GP.MontoAfecto, GP.MontoImpuesto
		FROM ReportesApp_Operaciones_TicketGasto_GastoPeaje GP
		LEFT JOIN PersonaMast PM ON LTRIM(RTRIM(PM.DocumentoFiscal)) = LTRIM(RTRIM(GP.NroRUC))
		LEFT JOIN OP_TR_Peaje P ON GP.idPeaje = P.IdPeaje
		WHERE GP.idPeaje = @idPeaje
	END
	
	IF (@Opcion = 2) BEGIN		-- LISTAR PEAJES DEL RUC
		SELECT TOP(1) GP.NroRUC, LTRIM(RTRIM(PM.NombreCompleto)) AS NombreCompleto, ISNULL(GP.Monto,0.00) AS 'Monto',
		ISNULL(GP.MontoAfecto,0.00) AS 'MontoAfecto', ISNULL(GP.MontoImpuesto,0.00) AS 'MontoImpuesto'
		FROM ReportesApp_Operaciones_TicketGasto_GastoPeaje GP
		LEFT JOIN PersonaMast PM ON LTRIM(RTRIM(PM.DocumentoFiscal)) = LTRIM(RTRIM(GP.NroRUC))
		WHERE LTRIM(RTRIM(GP.NroRUC)) = LTRIM(RTRIM(@NroRUC)) 
	END
END

--------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 24-11-2023
-- Description:	LISTAR GASTO DE RUTA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ListarGastoRuta]
@idOperacion INT,
@Ruta VARCHAR(250)
AS
BEGIN
	IF (@idOperacion = 5) BEGIN
		SELECT GC.idGastoxRutaC AS 'Nro', O.IdOperacion, O.Descripcion AS 'Operacion', R.IdRuta, R.Descripcion AS 'Ruta', CONVERT(INT, GC.TotalDias) AS 'TotalDias',
		T.Descripcion AS 'Tiempo', CG.DescripcionLocal AS 'ConceptoGasto', TG.Descripcion AS 'TipoGasto', GD.Descripcion, GD.Gasto, GC.GastoTotal
		FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera GC
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle GD ON GD.idGastoxRutaC = GC.idGastoxRutaC
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = GC.IdOperacion
		LEFT JOIN OP_TR_Ruta R ON R.IdRuta = GC.IdRuta
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_Tiempo T ON T.idTiempo = GD.idTiempo
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_TipoGasto TG ON TG.idTipoGasto = GD.idTipoGasto
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_ConceptoGasto CG ON CG.ConceptoGasto = TG.ConceptoGasto
		WHERE (R.Descripcion IS NULL OR R.Descripcion LIKE '%' + @Ruta + '%')
		ORDER BY GC.idGastoxRutaC DESC
	END
	ELSE BEGIN
		SELECT GC.idGastoxRutaC AS 'Nro', O.IdOperacion, O.Descripcion AS 'Operacion', R.IdRuta, R.Descripcion AS 'Ruta', CONVERT(INT, GC.TotalDias) AS 'TotalDias',
		T.Descripcion AS 'Tiempo', CG.DescripcionLocal AS 'ConceptoGasto', TG.Descripcion AS 'TipoGasto', GD.Descripcion, GD.Gasto, GC.GastoTotal
		FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera GC
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle GD ON GD.idGastoxRutaC = GC.idGastoxRutaC
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = GC.IdOperacion
		LEFT JOIN OP_TR_Ruta R ON R.IdRuta = GC.IdRuta
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_Tiempo T ON T.idTiempo = GD.idTiempo
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_TipoGasto TG ON TG.idTipoGasto = GD.idTipoGasto
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_ConceptoGasto CG ON CG.ConceptoGasto = TG.ConceptoGasto
		WHERE (R.Descripcion IS NULL OR R.Descripcion LIKE '%' + @Ruta + '%') AND (GC.IdOperacion = @idOperacion)
		ORDER BY GC.idGastoxRutaC DESC
	END
END

---------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 28-11-2023
-- Description:	INSERTAR VIATICOS DE CONDUCTORES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_InsertarViaticos]
@NroTicket INT,
@CodGasto VARCHAR(50),
@idConductor INT,
@Fecha DATETIME,
@idTipoViatico INT,
@Monto DECIMAL(10,2),
@Descripcion VARCHAR(250)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

SET @Exito = '0 = Exito.'
SET @correlativo = (SELECT MAX(idViatico) FROM ReportesApp_Operaciones_TicketGasto_Viaticos WHERE idConductor = @idConductor)
SET @correlativo = ISNULL(@correlativo,0) + 1

/*
IF(EXISTS(SELECT * FROM ReportesApp_Operaciones_TicketGasto_Viaticos WHERE idTipoViatico = @idTipoViatico AND CONVERT(DATE,Fecha) = CONVERT(DATE,@Fecha) AND idConductor = @idConductor)) BEGIN
	SET @Exito = '-1 = El conductor ya tiene este viático registrado.'
	GOTO Terminar
END
*/

IF((SELECT idTipoViatico FROM ReportesApp_Operaciones_TicketGasto_Viaticos WHERE idConductor = @idConductor AND CONVERT(DATE,Fecha) = CONVERT(DATE,@Fecha)) = 1) BEGIN
	SET @Exito = '-1 = El conductor ya tiene este viático registrado.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	INSERT INTO ReportesApp_Operaciones_TicketGasto_Viaticos(idViatico,CodGasto,idConductor,Fecha,idTipoViatico,Monto,NroTicket,Descripcion)
	VALUES(@correlativo,@CodGasto,@idConductor,@Fecha,@idTipoViatico,@Monto,@NroTicket,@Descripcion)

	IF (@CodGasto = '') BEGIN
		UPDATE ReportesApp_Operacion_Previaje_Registros
		SET Viaticos = Viaticos + @Monto
		WHERE NroTicket = @NroTicket
	END
	ELSE BEGIN
		UPDATE ReportesApp_Operaciones_TicketGasto_Registro
		SET TotalEntregado = TotalEntregado + @Monto, GastoDiferencialV = ISNULL(GastoDiferencialV,0) + @Monto
		WHERE CodGasto = @CodGasto

		UPDATE ReportesApp_Operacion_Previaje_Registros
		SET Viaticos = Viaticos + @Monto
		WHERE NroTicket = @NroTicket
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
-- Create date: 15-08-2023
-- Description:	ELIMINAR VIATICOS DE CONDUCTORES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_EliminarViaticos]
@NroTicket INT,
@idViatico INT,
@idConductor INT,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @Monto DECIMAL(10,2)
DECLARE @CodGasto VARCHAR(30)
DECLARE @Estado VARCHAR(30)

SET @Exito = '0 = Recibo eliminado.'

IF (@idViatico = 0)
BEGIN
	SET @Exito = '-1 = El viatico seleccionado no existe.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	SET @Monto = (SELECT Monto FROM ReportesApp_Operaciones_TicketGasto_Viaticos WHERE idViatico = @idViatico AND idConductor = @idConductor)
	SET @CodGasto = (SELECT CodGasto FROM ReportesApp_Operaciones_TicketGasto_Viaticos WHERE idViatico = @idViatico AND idConductor = @idConductor)
	
	IF (@CodGasto LIKE '%0%') BEGIN
		SET @Estado = ISNULL((SELECT TOP 1 CASE WHEN NumeroDocumentoInterno IS NOT NULL THEN 'PAGADA' ELSE ' ' END FROM AP_GastoAdelanto
		WHERE RTRIM(NumeroDocumentoInterno) = @CodGasto AND (Estado = 'PA' OR Estado = 'AL') AND Descripcion LIKE '%ADICIONAL%' ORDER BY NumeroAdelanto),'PENDIENTE')
	END
	ELSE BEGIN
		SET @Estado = 'PENDIENTE'
	END

	IF (@Estado != 'PAGADA') BEGIN
		UPDATE ReportesApp_Operacion_Previaje_Registros
		SET Viaticos = Viaticos - @Monto
		WHERE NroTicket = @NroTicket

		IF ((SELECT GastoDiferencialV FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE
		CodGasto = (SELECT CodGasto FROM ReportesApp_Operaciones_TicketGasto_Viaticos WHERE idViatico = @idViatico AND idConductor = @idConductor)) IS NOT NULL)
		BEGIN
			UPDATE ReportesApp_Operaciones_TicketGasto_Registro
			SET TotalEntregado = TotalEntregado - @Monto, GastoDiferencialV = ISNULL(GastoDiferencialV,0) - @Monto
			WHERE CodGasto = (SELECT CodGasto FROM ReportesApp_Operaciones_TicketGasto_Viaticos WHERE idViatico = @idViatico AND idConductor = @idConductor)
		END
		ELSE BEGIN
			UPDATE ReportesApp_Operaciones_TicketGasto_Registro
			SET TotalEntregado = TotalEntregado - @Monto, GastoDiferencialV = @Monto
			WHERE CodGasto = (SELECT CodGasto FROM ReportesApp_Operaciones_TicketGasto_Viaticos WHERE idViatico = @idViatico AND idConductor = @idConductor)
		END

		/*
		IF ((SELECT EstadoLiquidacion FROM ReportesApp_Operaciones_TicketGasto_Registro
		WHERE CodGasto = (SELECT CodGasto FROM ReportesApp_Operaciones_TicketGasto_Viaticos WHERE idViatico = @idViatico AND idConductor = @idConductor)) = 3)
		BEGIN
			UPDATE ReportesApp_Operaciones_TicketGasto_Registro
			SET EstadoLiquidacion = 2, UsuarioLiquidacion = @Usuario, FechaLiquidacion = GETDATE()
			WHERE CodGasto = (SELECT CodGasto FROM ReportesApp_Operaciones_TicketGasto_Viaticos WHERE idViatico = @idViatico AND idConductor = @idConductor)
		END
		*/

		INSERT INTO ReportesApp_Operaciones_TicketGasto_Viaticos_Historial (idViatico,CodGasto,NroTicket,idConductor,Fecha,idTipoViatico,Monto,
					Descripcion,UsuarioElimina,FechaElimina)
		SELECT idViatico,CodGasto,NroTicket,idConductor,Fecha,idTipoViatico,Monto,Descripcion,@Usuario,GETDATE()
		FROM ReportesApp_Operaciones_TicketGasto_Viaticos
		WHERE idViatico = @idViatico AND idConductor = @idConductor

		DELETE FROM ReportesApp_Operaciones_TicketGasto_Viaticos
		WHERE idViatico = @idViatico AND idConductor = @idConductor
	END
	ELSE BEGIN
		SET @Exito = '-1 = El viático seleccionado ya se encuentra pagado, no se puede eliminar.'
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
-- Create date: 14-08-2023
-- Description:	LISTAR VIATICOS DE CONDUCTORES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ListarViaticos]
@Opcion INT,
@idConductor INT
AS
BEGIN
	SELECT TOP(20) V.idViatico, V.NroTicket, V.idConductor, V.Fecha AS 'FECHA_VIATICO', V.CodGasto AS 'PLANILLA', V.Descripcion AS 'VIATICO', V.Monto AS 'IMPORTE',
	R.EstadoLiquidacion FROM ReportesApp_Operaciones_TicketGasto_Viaticos V
	LEFT JOIN ReportesApp_Operaciones_TicketGasto_Registro R ON R.CodGasto = V.CodGasto
	WHERE V.idConductor = @idConductor
	ORDER BY V.idViatico DESC
END

---------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 20-12-2023
-- Description:	INSERTAR VIATICOS SIN PROGRAMACION
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ViaticoSinProg]
@idConductor INT,
@Fecha DATETIME,
@idTipoViatico INT,
@Monto DECIMAL(10,2),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT
DECLARE @correlativo2 INT

SET @Exito = '0 = Viático registrado.'

IF(EXISTS(SELECT * FROM ReportesApp_Operaciones_TicketGasto_Viaticos WHERE idTipoViatico = @idTipoViatico AND CONVERT(DATE,Fecha) = CONVERT(DATE,@Fecha) AND idConductor = @idConductor)) BEGIN
	SET @Exito = '-1 = El conductor ya tiene este viático registrado.'
	GOTO Terminar
END

IF((SELECT idTipoViatico FROM ReportesApp_Operaciones_TicketGasto_Viaticos WHERE idConductor = @idConductor AND CONVERT(DATE,Fecha) = CONVERT(DATE,@Fecha)) = 1) BEGIN
	SET @Exito = '-1 = El conductor ya tiene este viático registrado.'
	GOTO Terminar
END

SET @correlativo = (SELECT MAX(idViatico) FROM ReportesApp_Operaciones_TicketGasto_Viaticos WHERE idConductor = @idConductor)
SET @correlativo = ISNULL(@correlativo,0) + 1

SET @correlativo2 = (SELECT MAX(idTicketGasto) FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE Anio = YEAR(GETDATE()))
SET @correlativo2 = ISNULL(@correlativo2,0) + 1

BEGIN TRAN
BEGIN TRY
	INSERT INTO ReportesApp_Operaciones_TicketGasto_Registro(idTicketGasto, Anio, CodGasto, TotalEntregado, FechaCreacion, UsuarioCreacion, EstadoLiquidacion, ViConductor,Permiso, NroTicket, idGastoXRutaC)
	VALUES(@correlativo2, YEAR(GETDATE()), SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo2)),6)), @Monto, GETDATE(), @Usuario, 0, @idConductor, 0,
		-CONVERT(INT,SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo2)),6))), 0)

	INSERT INTO ReportesApp_Operaciones_TicketGasto_Viaticos(idViatico,CodGasto,idConductor,Fecha,idTipoViatico,Monto)
	VALUES(@correlativo,SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo2)),6)),@idConductor,@Fecha,@idTipoViatico,@Monto)
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
-- Create date: 10-08-2023
-- Description:	LISTAR PLANILLAS LIQUIDADAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ListarPlanillasLiquidadas]
@IdOperacion INT,
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@NombreConductor VARCHAR(250),
@Planilla VARCHAR(50),
@Estado INT
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@IdOperacion = 5) BEGIN
		IF @Estado = 1 BEGIN
			SELECT TG.idTicketGasto, ISNULL(P.IdRuta,TG.IdRuta) AS 'IdRuta', ISNULL(P.TipoProgramacion,TG.IdOperacion) AS 'TipoProgramacion', TG.ViConductor AS 'IdConductor',
			TG.NroTicket, TG.NroRepGasto AS 'RG', TG.Sucursal AS 'SUCURSAL', TG.FechaCreacion AS 'FECHA', TG.CodGasto AS 'PLANILLA', O.Descripcion AS 'OPERACION', C.Nombre AS 'CONDUCTOR',
			V.NumeroPlaca AS 'TRACTO', VJ.Descripcion AS 'RUTA', TG.TotalEntregado AS 'IMPORTE_TOTAL', ISNULL(TG.TotalGastado,0) AS 'TOTAL_GASTO', ISNULL(TG.Vuelto,0) AS 'VUELTO',
			ISNULL(TG.Descuento,0) AS 'DESCUENTO', ISNULL(TG.Reintegro,0) AS 'REINTEGRO', TG.UsuarioLiquidacion AS 'USUARIO',
			CONVERT(CHAR(10),TG.FechaLiquidacion,103)+RIGHT(RTRIM(CONVERT(CHAR(26),TG.FechaLiquidacion,22)),12) AS 'FECHA_LIQUIDACION',
			LTRIM(RTRIM(Dp.DescripcionCorta))+' - '+LTRIM(RTRIM(PV.DescripcionCorta)) AS 'ORIGEN_CONDUCTOR', LTRIM(RTRIM(Zp.DescripcionCorta)) AS 'ZONA_CONDUCTOR'
			FROM ReportesApp_Operaciones_TicketGasto_Registro TG
			LEFT JOIN ReportesApp_Operacion_Previaje_Registros P ON P.NroTicket = TG.NroTicket
			LEFT JOIN OP_TR_Ruta VJ ON VJ.IdRuta = ISNULL(P.IdRuta,TG.IdRuta)
			LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = ISNULL(P.idTracto,TG.IdTracto)
			LEFT JOIN OP_TR_Conductor C ON C.IdConductor = TG.ViConductor
			LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera GC ON GC.idGastoXRutaC = TG.idGastoXRutaC
			LEFT JOIN PersonaMast PM ON LTRIM(RTRIM(PM.Documento)) = LTRIM(RTRIM(C.Documento))
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = TG.IdOperacion
			LEFT JOIN Departamento Dp ON dp.Departamento = PM.Departamento
			LEFT JOIN Provincia Pv ON pv.Departamento = PM.Departamento AND pv.Provincia = PM.Provincia
			LEFT JOIN ZonaPostal Zp on Zp.Departamento = PM.Departamento AND zp.Provincia = PM.Provincia AND zp.CodigoPostal = PM.CodigoPostal
			WHERE ((TG.FechaLiquidacion BETWEEN @FINICIO AND @FFIN) AND (TG.EstadoLiquidacion = @Estado) AND
			(TG.CodGasto IS NULL OR TG.CodGasto LIKE '%' + @Planilla + '%') AND (C.Nombre IS NULL OR C.Nombre LIKE '%' + @NombreConductor + '%'))
			ORDER BY TG.NroRepGasto DESC
		END

		IF @Estado = 0 BEGIN
			SELECT TG.idTicketGasto, ISNULL(P.IdRuta,TG.IdRuta) AS 'IdRuta', ISNULL(P.TipoProgramacion,TG.IdOperacion) AS 'TipoProgramacion', TG.idGastoXRutaC, TG.NroTicket,
			TG.ViConductor AS 'IdConductor', TG.ViConductor, TG.FechaCreacion AS 'FECHA', TG.CodGasto AS 'PLANILLA', ISNULL((SELECT TOP 1 CASE WHEN NumeroDocumentoInterno IS NOT NULL THEN ' ' ELSE 'PENDIENTE' END
			FROM AP_GastoAdelanto WHERE NumeroDocumentoInterno = TG.CodGasto AND (Estado = 'TR' OR Estado = 'PA') ORDER BY NumeroAdelanto),'') AS 'CAJA', O.Descripcion AS 'OPERACION',
			C.Nombre AS 'CONDUCTOR', V.NumeroPlaca AS 'TRACTO', VJ.Descripcion AS 'RUTA', TG.TotalEntregado - ISNULL(TG.GastoDiferencialV,0) - ISNULL(GastoDiferencial,0) AS 'IMPORTE_TOTAL',
			ISNULL(TG.GastoDiferencialV,0) AS 'ADICIONAL', ISNULL(TG.GastoDiferencial,0) AS 'CAMBIO_RUTA', ISNULL(TG.TotalGastado,0) AS 'TOTAL_GASTO', ISNULL(TG.Vuelto,0) AS 'VUELTO',
			ISNULL(TG.Descuento,0) AS 'DESCUENTO', ISNULL(TG.Reintegro,0) AS 'REINTEGRO', TG.UsuarioCreacion AS 'USUARIO', LTRIM(RTRIM(Dp.DescripcionCorta))+' - '+LTRIM(RTRIM(PV.DescripcionCorta)) AS 'ORIGEN_CONDUCTOR',
			LTRIM(RTRIM(Zp.DescripcionCorta)) AS 'ZONA_CONDUCTOR', TG.Permiso,
			CASE WHEN TG.Permiso = 0 THEN ' ' ELSE 'OK' END AS 'AUTORIZACION' FROM ReportesApp_Operaciones_TicketGasto_Registro TG
			LEFT JOIN ReportesApp_Operacion_Previaje_Registros P ON P.NroTicket = TG.NroTicket
			LEFT JOIN OP_TR_Ruta VJ ON VJ.IdRuta = ISNULL(P.IdRuta,TG.IdRuta)
			LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = ISNULL(P.idTracto,TG.IdTracto)
			LEFT JOIN OP_TR_Conductor C ON C.IdConductor = TG.ViConductor
			LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera GC ON GC.idGastoXRutaC = TG.idGastoXRutaC
			LEFT JOIN PersonaMast PM ON LTRIM(RTRIM(PM.Documento)) = LTRIM(RTRIM(C.Documento))
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = TG.IdOperacion
			LEFT JOIN Departamento Dp ON dp.Departamento = PM.Departamento
			LEFT JOIN Provincia Pv ON pv.Departamento = PM.Departamento AND pv.Provincia = PM.Provincia
			LEFT JOIN ZonaPostal Zp on Zp.Departamento = PM.Departamento AND zp.Provincia = PM.Provincia AND zp.CodigoPostal = PM.CodigoPostal
			--INNER JOIN AP_GastoAdelanto A ON A.NumeroDocumentoInterno = TG.CodGasto
			WHERE ((TG.FechaCreacion BETWEEN @FINICIO AND @FFIN) AND (TG.EstadoLiquidacion = @Estado) AND
			(TG.CodGasto IS NULL OR TG.CodGasto LIKE '%' + @Planilla + '%') AND (C.Nombre IS NULL OR C.Nombre LIKE '%' + @NombreConductor + '%'))
			ORDER BY idTicketGasto DESC
		END
	END
	ELSE BEGIN
		IF @Estado = 1 BEGIN
			SELECT TG.idTicketGasto, ISNULL(P.IdRuta,TG.IdRuta) AS 'IdRuta', ISNULL(P.TipoProgramacion,TG.IdOperacion) AS 'TipoProgramacion', TG.ViConductor AS 'IdConductor',
			TG.NroTicket, TG.NroRepGasto AS 'RG', TG.Sucursal AS 'SUCURSAL', TG.FechaCreacion AS 'FECHA', TG.CodGasto AS 'PLANILLA', O.Descripcion AS 'OPERACION', C.Nombre AS 'CONDUCTOR',
			V.NumeroPlaca AS 'TRACTO', VJ.Descripcion AS 'RUTA', TG.TotalEntregado AS 'IMPORTE_TOTAL', ISNULL(TG.TotalGastado,0) AS 'TOTAL_GASTO', ISNULL(TG.Vuelto,0) AS 'VUELTO',
			ISNULL(TG.Descuento,0) AS 'DESCUENTO', ISNULL(TG.Reintegro,0) AS 'REINTEGRO', TG.UsuarioLiquidacion AS 'USUARIO',
			CONVERT(CHAR(10),TG.FechaLiquidacion,103)+RIGHT(RTRIM(CONVERT(CHAR(26),TG.FechaLiquidacion,22)),12) AS 'FECHA_LIQUIDACION',
			LTRIM(RTRIM(Dp.DescripcionCorta))+' - '+LTRIM(RTRIM(PV.DescripcionCorta)) AS 'ORIGEN_CONDUCTOR', LTRIM(RTRIM(Zp.DescripcionCorta)) AS 'ZONA_CONDUCTOR'
			FROM ReportesApp_Operaciones_TicketGasto_Registro TG
			LEFT JOIN ReportesApp_Operacion_Previaje_Registros P ON P.NroTicket = TG.NroTicket
			LEFT JOIN OP_TR_Ruta VJ ON VJ.IdRuta = ISNULL(P.IdRuta,TG.IdRuta)
			LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = ISNULL(P.idTracto,TG.IdTracto)
			LEFT JOIN OP_TR_Conductor C ON C.IdConductor = TG.ViConductor
			LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera GC ON GC.idGastoXRutaC = TG.idGastoXRutaC
			LEFT JOIN PersonaMast PM ON LTRIM(RTRIM(PM.Documento)) = LTRIM(RTRIM(C.Documento))
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = TG.IdOperacion
			LEFT JOIN Departamento Dp ON dp.Departamento = PM.Departamento
			LEFT JOIN Provincia Pv ON pv.Departamento = PM.Departamento AND pv.Provincia = PM.Provincia
			LEFT JOIN ZonaPostal Zp on Zp.Departamento = PM.Departamento AND zp.Provincia = PM.Provincia AND zp.CodigoPostal = PM.CodigoPostal
			WHERE ((TG.IdOperacion = @IdOperacion) AND (TG.FechaLiquidacion BETWEEN @FINICIO AND @FFIN) AND (TG.EstadoLiquidacion = @Estado) AND
			(TG.CodGasto IS NULL OR TG.CodGasto LIKE '%' + @Planilla + '%') AND (C.Nombre IS NULL OR C.Nombre LIKE '%' + @NombreConductor + '%'))
			ORDER BY TG.NroRepGasto DESC
		END

		IF @Estado = 0 BEGIN
			SELECT TG.idTicketGasto, ISNULL(P.IdRuta,TG.IdRuta) AS 'IdRuta', ISNULL(P.TipoProgramacion,TG.IdOperacion) AS 'TipoProgramacion', TG.idGastoXRutaC, TG.NroTicket,
			TG.ViConductor AS 'IdConductor', TG.ViConductor, TG.FechaCreacion AS 'FECHA', TG.CodGasto AS 'PLANILLA', ISNULL((SELECT TOP 1 CASE WHEN NumeroDocumentoInterno IS NOT NULL THEN ' ' ELSE 'PENDIENTE' END
			FROM AP_GastoAdelanto WHERE NumeroDocumentoInterno = TG.CodGasto AND (Estado = 'TR' OR Estado = 'PA') ORDER BY NumeroAdelanto),'') AS 'CAJA', O.Descripcion AS 'OPERACION',
			C.Nombre AS 'CONDUCTOR', V.NumeroPlaca AS 'TRACTO', VJ.Descripcion AS 'RUTA', TG.TotalEntregado - ISNULL(TG.GastoDiferencialV,0) - ISNULL(GastoDiferencial,0) AS 'IMPORTE_TOTAL',
			ISNULL(TG.GastoDiferencialV,0) AS 'ADICIONAL', ISNULL(TG.GastoDiferencial,0) AS 'CAMBIO_RUTA', ISNULL(TG.TotalGastado,0) AS 'TOTAL_GASTO', ISNULL(TG.Vuelto,0) AS 'VUELTO',
			ISNULL(TG.Descuento,0) AS 'DESCUENTO', ISNULL(TG.Reintegro,0) AS 'REINTEGRO', TG.UsuarioCreacion AS 'USUARIO', LTRIM(RTRIM(Dp.DescripcionCorta))+' - '+LTRIM(RTRIM(PV.DescripcionCorta)) AS 'ORIGEN_CONDUCTOR',
			LTRIM(RTRIM(Zp.DescripcionCorta)) AS 'ZONA_CONDUCTOR', TG.Permiso,
			CASE WHEN TG.Permiso = 0 THEN ' ' ELSE 'OK' END AS 'AUTORIZACION' FROM ReportesApp_Operaciones_TicketGasto_Registro TG
			LEFT JOIN ReportesApp_Operacion_Previaje_Registros P ON P.NroTicket = TG.NroTicket
			LEFT JOIN OP_TR_Ruta VJ ON VJ.IdRuta = ISNULL(P.IdRuta,TG.IdRuta)
			LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = ISNULL(P.idTracto,TG.IdTracto)
			LEFT JOIN OP_TR_Conductor C ON C.IdConductor = TG.ViConductor
			LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera GC ON GC.idGastoXRutaC = TG.idGastoXRutaC
			LEFT JOIN PersonaMast PM ON LTRIM(RTRIM(PM.Documento)) = LTRIM(RTRIM(C.Documento))
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = TG.IdOperacion
			LEFT JOIN Departamento Dp ON dp.Departamento = PM.Departamento
			LEFT JOIN Provincia Pv ON pv.Departamento = PM.Departamento AND pv.Provincia = PM.Provincia
			LEFT JOIN ZonaPostal Zp on Zp.Departamento = PM.Departamento AND zp.Provincia = PM.Provincia AND zp.CodigoPostal = PM.CodigoPostal
			--INNER JOIN AP_GastoAdelanto A ON A.NumeroDocumentoInterno = TG.CodGasto
			WHERE ((TG.IdOperacion = @IdOperacion) AND (TG.FechaCreacion BETWEEN @FINICIO AND @FFIN) AND (TG.EstadoLiquidacion = @Estado) AND
			(TG.CodGasto IS NULL OR TG.CodGasto LIKE '%' + @Planilla + '%') AND (C.Nombre IS NULL OR C.Nombre LIKE '%' + @NombreConductor + '%'))
			ORDER BY idTicketGasto DESC
		END
	END
END

---------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 21-07-2023
-- Description:	ADJUNTAR PLANILLA DE VIAJE
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_AdjuntarPlanilla]
@CodGasto VARCHAR(50),
@idConductor INT,
@NroTicket INT,
@idOperacion INT,
@idGastoxRutaC INT,
@Usuario VARCHAR(50)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @Contador INT
DECLARE @Gasto INT

SET @Contador = (SELECT COUNT(PR.IdConductor) FROM ReportesApp_Operaciones_TicketGasto_Registro TG LEFT JOIN ReportesApp_Operacion_Previaje_Registros PR ON TG.NroTicket = PR.NroTicket WHERE TG.EstadoLiquidacion = 0 AND PR.IdConductor = @idConductor)

/*
IF (@Contador = 2)
BEGIN
	SET @Exito = '-1 = Este conductor aún tiene 2 planillas por liquidar.'
	GOTO Terminar
END
*/

BEGIN TRAN
BEGIN TRY
	SET @Gasto = (SELECT GastoTotal FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera WHERE idGastoxRutaC = @idGastoxRutaC AND IdOperacion = @idOperacion)

	UPDATE ReportesApp_Operaciones_TicketGasto_Registro
	SET NroTicket = @NroTicket, idGastoXRutaC = @idGastoxRutaC, FechaCreacion = GETDATE(), TotalEntregado = TotalEntregado + @Gasto, GastoDiferencialV = @Gasto, UsuarioCreacion = @Usuario, EstadoLiquidacion = 0
	WHERE CodGasto = @CodGasto

	UPDATE ReportesApp_Operacion_Previaje_Registros
	SET Planilla = @CodGasto
	WHERE NroTicket = @NroTicket
	
	SET @Exito = '0 = Planilla Anexada. PL - ' + @CodGasto
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

--------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 23-08-2023
-- Description:	GENERAR PAGO DE PLANILLA SIN VIAJE
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_PagarSinViaje]
@idConductor INT,
@Planilla INT,
@Monto DECIMAL(10,2),
@Usuario VARCHAR(100)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @Nro INT
DECLARE @NroConductor INT
DECLARE @NroUsuario INT
DECLARE @NroAdelanto INT
DECLARE @MontoTotal DECIMAL(10,2)
DECLARE @GastoDiferencialV DECIMAL(10,2)
DECLARE @GastoDiferencial DECIMAL(10,2)
DECLARE @Proyecto VARCHAR(15)
DECLARE @TipoCambio MONEY
DECLARE @Persona INT
DECLARE @Contador INT
DECLARE @CodigoDocumento CHAR(10)
DECLARE @CuentaBancaria CHAR(15)
DECLARE @Sucursal CHAR(4)
DECLARE @UnidadReplicacion CHAR(4)

IF(EXISTS(SELECT * FROM Obligaciones WHERE NumeroDocumentoInterno = CONVERT(CHAR(20),@Planilla) AND EstadoDocumento = 'AP')) BEGIN
	SET @Exito = '-1 = El pago de este viaje ya fue registrado.'
	GOTO Terminar
END

IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_TicketGasto_UsuarioSucursal WHERE Usuario = @Usuario AND Estado = 'A')) BEGIN
	SET @CuentaBancaria = (SELECT CuentaBancaria FROM ReportesApp_Operaciones_TicketGasto_UsuarioSucursal WHERE Usuario = @Usuario)
	SET @Sucursal = (SELECT Sucursal FROM ReportesApp_Operaciones_TicketGasto_UsuarioSucursal WHERE Usuario = @Usuario)
	SET @UnidadReplicacion = (SELECT UnidadReplicacion FROM ReportesApp_Operaciones_TicketGasto_UsuarioSucursal WHERE Usuario = @Usuario)
END
ELSE BEGIN
	SET @Exito = '-1 = Este usuario está inactivo o no tiene una sede registrada.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	SET @GastoDiferencial = (SELECT GastoDiferencial FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE CodGasto = CONVERT(VARCHAR,@Planilla))
	SET @GastoDiferencialV = (SELECT GastoDiferencialV FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE CodGasto = CONVERT(VARCHAR,@Planilla))
	SET @MontoTotal = @Monto + ISNULL(@GastoDiferencial,0) + ISNULL(@GastoDiferencialV,0)

	UPDATE CorrelativosMast
	SET CorrelativoNumero = (SELECT CorrelativoNumero + 1 FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='APAD')),
		UltimaFechaModif = GETDATE()
	WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='APAD')
	
	SET @Nro = (SELECT CorrelativosMast.CorrelativoNumero FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='APAD'))

	DELETE FROM AP_GastoAdelantoSustento WHERE (AP_GastoAdelantoSustento.UnidadReplicacion ='TRUJ') AND (AP_GastoAdelantoSustento.TipoAdelanto ='E') AND (AP_GastoAdelantoSustento.NumeroAdelanto = @Nro) 

	--INSERTAR DATOS EN CABECERA DE REPORTES DE GASTO
	SET @NroConductor = (SELECT P.Persona FROM PersonaMast P LEFT JOIN OP_TR_Conductor C ON P.Persona = C.IdPersona WHERE C.IdConductor = @idConductor)				 
	SET @NroUsuario = (SELECT P.Persona FROM Usuario U LEFT JOIN PersonaMast P ON P.NombreCompleto = U.Nombre WHERE Usuario = @Usuario)
	
	--INSERTAR ADELANTO DE GASTO
	INSERT INTO AP_GastoAdelanto(NumeroAdelanto,CompaniaSocio,Clasificacion,FechaDocumento,TipoPago,Persona,PreparadoPor,FechaPreparacion,MonedaDocumento,MontoTotal,SaldoAdelanto,Descripcion,Estado,UltimoUsuario,
								 UltimaFechaModif,FechaEsperadaPago,TipoAdelanto,NumeroDocumentoInterno,FlujodeCaja,UnidadReplicacion,PartidaPresupuestal,PersonaPagara,ConceptoGasto,CentroCostos,EmpleadoAutorizado,
								 UnidadNegocio,RegComprasMontoAfecto,RegComprasMontoNoAfecto,RegComprasMontoIgv,CuentaBancaria)
	VALUES (@Nro,'10000000','ER',GETDATE(),'EF', @NroConductor, @NroUsuario, GETDATE(), 'LO', @MontoTotal, @MontoTotal, 'GASTOS DE VIAJE - PL ' + CONVERT(VARCHAR(30),@Planilla),
	'PR', @Usuario, GETDATE(), GETDATE(), 'E', CONVERT(VARCHAR(30),@Planilla), '051', 'TRUJ', '9999', @NroConductor, '0006', '010201', @NroUsuario, 'TRAN', 0.00, 0.00, 0.00, @CuentaBancaria)

	INSERT INTO AP_GastoAdelantoSustento(UnidadReplicacion,TipoAdelanto,NumeroAdelanto,Secuencia,ConceptoGasto,NumeroVeces,Moneda,PrecioUnitario,MontoTotal,UltimoUsuario,UltimaFechaModif,CuentaContable,MontoPresupuesto)
	VALUES ('TRUJ', 'E', @Nro, 1, '0006', 8, 'LO', @MontoTotal, @MontoTotal, @Usuario, GETDATE(), '1413002', 0.00)

	UPDATE AP_GastoAdelanto
	SET AprobadoPor = @NroUsuario, FechaAprobacion = GETDATE(), Estado = 'AP'
	WHERE NumeroAdelanto = @Nro AND TipoAdelanto = 'E' AND UnidadReplicacion = 'TRUJ'

	--INSERTAR OBLIGACIÓN
	DECLARE @Nro2 INT
			
	UPDATE CorrelativosMast
	SET CorrelativoNumero = (SELECT CorrelativoNumero + 1 FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie = 'APNO')),
		UltimaFechaModif = GETDATE()
	WHERE (CorrelativosMast.CompaniaCodigo = '999999') AND (CorrelativosMast.TipoComprobante = 'SY') AND (CorrelativosMast.Serie = 'APNO')
	SET @Nro2 = (SELECT CorrelativosMast.CorrelativoNumero FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo = '999999') AND (CorrelativosMast.TipoComprobante = 'SY') AND (CorrelativosMast.Serie = 'APNO'))

	INSERT INTO Obligaciones (Proveedor,TipoDocumento,NumeroDocumento,CuentaBancaria,CompaniaCodigo,TipoPago,FechaRegistro,FechaVencimiento,FechaVencimientoOriginal,GenerarPago,TipoServicio,MonedaDocumento,ConversionRequerida,
							  MonedaPago,MontoObligacion,MontoImpuestoVentas,MontoNoAfecto,MontoImponible,MontoAdelantos,MontoImpuestos,NetoMonedaLocal,NetoMonedaExtranjera,TipoDeCambio,AprobadoPor,AprobadoCP1,AprobadoCP2,
							  IngresadoPor,RevisadoPor,EstadoDocumento,ContabilizacionPendiente,ChequeIndividual,Voucher,NumeroPago,NumeroProceso,ProcesoSecuencia,RegistroNumero,Comentarios,UltimaFechaModif,UnidadNegocio,
							  FacturaAfectaSplitFlag,FactorRValidacion,UnidadReplicacion,CanjeRegistroNumero,MontoPagoParcial,NumeroDocumentoInterno,CentroCosto,PartidaPresupuestal,FlujodeCaja,CentroCostoCP,FechaRecepcion,
							  ProveedorPagarA,ControlPresupuestalFlag,CargoFlag,MontoCreditoFiscal,FechaDocumento,AfectoIGVFlag,DiferidoFlag,AdelantoFlag)
	VALUES (@NroConductor,'AE','TRUJ-'+CONVERT(VARCHAR(10),@Nro),@CuentaBancaria,'100000','EF',GETDATE(),GETDATE(),GETDATE(),'S','INAFEC','LO','D','LO',@MontoTotal,0.00,0.00,@MontoTotal,0.00,0.00,@MontoTotal,
	0.00,0,0,0,0,@NroUsuario, @NroUsuario, 'RV', 'S', 'N', CONVERT(CHAR(4),YEAR(GETDATE()))+CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(GETDATE()))),2)),0,0,0,@Nro2, 'GASTOS DE VIAJE - PL ' + CONVERT(VARCHAR(30),@Planilla),
	GETDATE(),'TRAN','N','N','TRUJ',0,0.00,CONVERT(VARCHAR(30),@Planilla),'010201','9999','051','010201',GETDATE(),@NroConductor, 'N', 'N', 0.00, GETDATE(), 'N', 'N', 'N')

	INSERT INTO ObligacionesXCuenta (Descripcion,Proveedor,NumeroDocumento,Linea,Monto,CentroCosto,CuentaContable,PartidaPresupuestal,Persona,DocumentoReferencia,Sucursal,FlujodeCaja,NoAfectoIGVFlag,TipoDocumento)
	VALUES ('GASTOS DE VIAJE - PL ' + CONVERT(VARCHAR(30),@Planilla),@NroConductor,'TRUJ-'+CONVERT(VARCHAR(10),@Nro),1,@MontoTotal,'010201','1413002','9999', @NroConductor,
	'AE-TRUJ-'+CONVERT(VARCHAR(10),@Nro),@Sucursal, '051', 'N', 'AE')

	UPDATE AP_GastoAdelanto
	SET MonedaDocumento = 'LO',ObligacionTipoDocumento = 'AE',ObligacionNumeroDocumento = 'TRUJ-'+CONVERT(VARCHAR(10),@Nro),Estado = 'TR'
	WHERE UnidadReplicacion = 'TRUJ' AND TipoAdelanto = 'E' AND NumeroAdelanto = @Nro 

	INSERT INTO AP_ObligacionFlujo (Proveedor,TipoDocumento,NumeroDocumento,Secuencia,AreaDocumentaria,Comentario,Estado,UltimoUsuario,UltimaFechaModif)
	VALUES (@NroConductor, 'AE','TRUJ-'+CONVERT(VARCHAR(10),@Nro), 1,'001','Recepción Inicial','A',@Usuario,GETDATE())

	DECLARE @NombreConductor VARCHAR(250)
	SET @NombreConductor = (SELECT P.NombreCompleto FROM PersonaMast P LEFT JOIN OP_TR_Conductor C ON P.Persona = C.IdPersona WHERE C.IdConductor = @idConductor)

	--INSERTAR ORDEN DE PAGO
	INSERT INTO OrdenPago (SistemaFuente,Proveedor,TipoDocumento,NumeroDocumento,PagarA,CompaniaCodigo,CuentaBancaria,TipoPago,ChequeIndividual,FechaVencimiento,MonedaDocumento,MonedaPago,MontoMoneda,TipodeCambio,
						   BatchNumber,FechaTransferencia,Estado,FlujodeCaja,RegistroNumero,FechaDocumento,PagoDiferidoFlag,UnidadNegocio)
	VALUES ('AP',@NroConductor,'AE','TRUJ-'+CONVERT(VARCHAR(10),@Nro),@NombreConductor,'100000',@CuentaBancaria,'EF','N',GETDATE(),'LO','LO',@MontoTotal,0,0,GETDATE(),'AP','051', @Nro2, GETDATE(),'N','TRAN')

	SET @TipoCambio = (CONVERT(MONEY,(SELECT TipoCambioMast.FactorVenta FROM TipoCambioMast WHERE TipoCambioMast.MonedaCodigo ='EX' AND TipoCambioMast.MonedaCambioCodigo ='LO' AND TipoCambioMast.FechaCambio =CONVERT(DATE,GETDATE()))))

	UPDATE Obligaciones
	SET AprobadoCP1 = @NroUsuario, EstadoDocumento = 'AP', Voucher = CONVERT(CHAR(4),YEAR(GETDATE()))+CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(GETDATE()))),2)),
		FechaAprobacion = GETDATE(), TipodeCambioProvision = @TipoCambio, UltimoUsuario = @Usuario, UltimaFechaModif = GETDATE()
	WHERE Proveedor = @NroConductor AND TipoDocumento = 'AE' AND NumeroDocumento = 'TRUJ-'+CONVERT(VARCHAR(10),@Nro)

	DELETE XOrdenPago WHERE XOrdenPago.Usuario = @Usuario

	INSERT INTO XOrdenPago (TipoRegistro,SistemaFuente,Proveedor,TipoDocumento,NumeroDocumento,CompaniaCodigo,PagarA,CuentaBancaria,TipoPago,ChequeIndividual,MonedaPago,FechaVencimiento,Estado,MonedaDocumento,MontoMoneda,Usuario,MontoOriginal)
	VALUES ('D','AP',@NroConductor,'AE','TRUJ-'+CONVERT(VARCHAR(10),@Nro),'100000',@NombreConductor,@CuentaBancaria,'EF','N','LO',GETDATE(),'A','LO',@MontoTotal,@Usuario,@MontoTotal)
	
	INSERT INTO XOrdenPago (TipoRegistro,SistemaFuente,Proveedor,TipoDocumento,NumeroDocumento,CompaniaCodigo,PagarA,CuentaBancaria,TipoPago,ChequeIndividual,MonedaPago,Estado,MontoMoneda,Usuario)
	VALUES ('S','XX',@NroConductor,'ZZ','000001','100000',@NombreConductor,@CuentaBancaria,'EF','N','LO','A',@MontoTotal,@Usuario)

	UPDATE ReportesApp_Operaciones_TicketGasto_Registro
	SET GastoDiferencialV = NULL, GastoDiferencial = NULL
	WHERE CodGasto = CONVERT(VARCHAR,@Planilla)

	SET @Exito = '0 = Gasto Pagado Exitosamente.'
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

---------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 14-08-2023
-- Description:	LISTAR PLANILLAS SIN PROGRAMACION
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ListarPlanillasSinViaje]
@Conductor VARCHAR(200),
@Ruta VARCHAR(200)
AS
BEGIN
	SELECT TG.CodGasto AS 'CODIGO', C.Nombre AS 'CONDUCTOR', TG.idGastoXRutaC, VJ.Descripcion AS 'RUTA', TG.TotalEntregado AS 'MONTO'
	FROM ReportesApp_Operaciones_TicketGasto_Registro TG
	LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera GC ON TG.idGastoXRutaC = GC.idGastoXRutaC
	LEFT JOIN OP_TR_Conductor C ON C.IdConductor = TG.ViConductor
	LEFT JOIN OP_TR_Ruta VJ ON VJ.IdRuta = GC.IdRuta
	LEFT JOIN ReportesApp_Operacion_Previaje_Registros R ON R.NroTicket = TG.NroTicket
	WHERE (@Conductor IS NULL OR C.Nombre LIKE '%' + @Conductor + '%') AND (@Ruta IS NULL OR VJ.Descripcion LIKE '%' + @Ruta + '%')
	AND (EstadoLiquidacion = 0)
	ORDER BY TG.CodGasto DESC

	/*(SELECT TG.CodGasto AS 'CODIGO', C.Nombre AS 'CONDUCTOR', TG.idGastoXRutaC, VJ.Descripcion AS 'RUTA', TG.TotalEntregado AS 'MONTO'
	FROM ReportesApp_Operaciones_TicketGasto_Registro TG
	LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera GC ON TG.idGastoXRutaC = GC.idGastoXRutaC
	LEFT JOIN OP_TR_Conductor C ON C.IdConductor = TG.ViConductor
	LEFT JOIN OP_TR_Ruta VJ ON VJ.IdRuta = GC.IdRuta
	LEFT JOIN ReportesApp_Operacion_Previaje_Registros R ON R.NroTicket = TG.NroTicket
	WHERE R.TipoProgramacion = 8 AND R.Planilla != ''
	AND (TG.CodGasto IS NULL OR TG.CodGasto LIKE '%' + @CodGasto + '%') AND (VJ.Descripcion IS NULL OR VJ.Descripcion LIKE '%' + @Ruta + '%'))
	*/
END

-----------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 27-12-2023
-- Description:	MODIFICAR PLANILLA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ActualizarPlanilla]
@NroTicket INT,
@Planilla VARCHAR(50),
@Usuario VARCHAR(50)
--@idConductor INT,
--@Gasto DECIMAL(10,2)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @GastoActualizado DECIMAL(10,2)

BEGIN TRAN
BEGIN TRY
	SET @GastoActualizado = (SELECT Viaticos FROM ReportesApp_Operacion_Previaje_Registros WHERE NroTicket = @NroTicket)

	UPDATE ReportesApp_Operaciones_TicketGasto_Registro
	SET UsuarioCreacion = @Usuario, FechaCreacion = GETDATE(), TotalEntregado = @GastoActualizado, GastoDiferencial = NULL, GastoDiferencialV = NULL
	WHERE CodGasto = @Planilla

	UPDATE ReportesApp_Operacion_Previaje_Registros
	SET Planilla = @Planilla, UsuarioModifica = @Usuario, FechaModifica = GETDATE()
	WHERE NroTicket = @NroTicket

	/*
	UPDATE ReportesApp_Operaciones_TicketGasto_Registro
	SET UsuarioCreacion = @Usuario, FechaCreacion = GETDATE(), GastoDiferencial = @GastoActualizado
	WHERE CodGasto = @Planilla

	UPDATE ReportesApp_Operacion_Previaje_Registros
	SET Planilla = @Planilla, UsuarioModifica = @Usuario, FechaModifica = GETDATE()
	WHERE NroTicket = @NroTicket
	*/

	SET @Exito = '0 = Planilla asignada exitosamente.'
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

--------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 29-12-2023
-- Description: AUTORIZAR REINTEGRO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_AutorizarReintegro]
@Planilla VARCHAR(50),
@Usuario VARCHAR(50)
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	UPDATE ReportesApp_Operaciones_TicketGasto_Registro
	SET Permiso = 1
	WHERE CodGasto = @Planilla

	SET @Exito = '0 = Reintegro autorizado.'
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

--------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 19-01-2024
-- Description: BUSCAR REPORTE GASTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_BuscarReporteGasto]
@Planilla VARCHAR(50),
@Usuario VARCHAR(50)
AS
DECLARE @NroRP INT
DECLARE @MontoGastado DECIMAL(10,2)
DECLARE @MontoEntregado DECIMAL(10,2)
DECLARE @Vuelto DECIMAL(10,2)
DECLARE @UltimoUsuario VARCHAR(100)
DECLARE @UltimaFecha DATETIME
DECLARE @Exito VARCHAR(MAX)

IF ((SELECT NroRepGasto FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE CodGasto = @Planilla) IS NOT NULL) BEGIN
	SET @Exito = '-1 = Esta planilla ya tiene un Reporte de Gasto asignado.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	SET @MontoEntregado = (SELECT TotalEntregado FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE CodGasto = @Planilla)

	IF (EXISTS(SELECT CajaChicaNumero FROM AP_CajaChica WHERE LTRIM(RTRIM(NumeroDocumentoInterno)) = @Planilla)) BEGIN
		SET @NroRP = (SELECT CajaChicaNumero FROM AP_CajaChica WHERE LTRIM(RTRIM(NumeroDocumentoInterno)) = @Planilla)
		SET @MontoGastado = (SELECT CONVERT(DECIMAL(10,2), MontoTotal) FROM AP_CajaChica WHERE CajaChicaNumero = @NroRP)
		SET @UltimoUsuario = (SELECT UltimoUsuario FROM AP_CajaChica WHERE CajaChicaNumero = @NroRP)
		SET @UltimaFecha = (SELECT FechaPreparacion FROM AP_CajaChica WHERE CajaChicaNumero = @NroRP)

		IF (@MontoGastado = @MontoEntregado) BEGIN
			IF (EXISTS(SELECT * FROM AP_CajaChicaDetalle WHERE CajaChicaNumero = @NroRP AND (LTRIM(RTRIM(ConceptoGasto)) = '0078'))) BEGIN
				SET @Vuelto = (SELECT SUM(MontoTotal) FROM AP_CajaChicaDetalle WHERE CajaChicaNumero = @NroRP AND (LTRIM(RTRIM(ConceptoGasto)) = '0078'))
			END
			ELSE BEGIN
				SET @Vuelto = 0.00
			END

			UPDATE ReportesApp_Operaciones_TicketGasto_Registro
			SET NroRepGasto = @NroRP, TotalGastado = @MontoGastado - @Vuelto, Vuelto = @Vuelto, Descuento = 0.00, Reintegro = 0.00, EstadoLiquidacion = 1,
				UsuarioLiquidacion = @UltimoUsuario, FechaLiquidacion = @UltimaFecha
			WHERE CodGasto = @Planilla
		END
		ELSE BEGIN
			UPDATE ReportesApp_Operaciones_TicketGasto_Registro
			SET NroRepGasto = @NroRP, TotalGastado = @MontoGastado, Vuelto = 0.00, Descuento = 0.00, Reintegro = @MontoGastado - @MontoEntregado, EstadoLiquidacion = 1,
			 	UsuarioLiquidacion = @UltimoUsuario, FechaLiquidacion = @UltimaFecha
			WHERE CodGasto = @Planilla
		END
		SET @Exito = '0 = Reporte de Gasto encontrado. RP: ' + CONVERT(VARCHAR(10),@NroRP)
	END
	ELSE BEGIN
		SET @Exito = '-1 = Esta planilla aún no tiene un reporte de gasto.'
		ROLLBACK
		GOTO Terminar
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

--------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 22-01-2024
-- Description:	LISTAR GASTOS ADICIONALES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ListarGastosAdicionales]
@Opcion INT,
@IdRuta INT,
@IdOperacion INT,
@idGastoxRutaD INT
AS
BEGIN
	SET NOCOUNT ON;
	IF (@Opcion = 1) BEGIN		-- LISTAR GASTOS ADICIONALES
		SELECT idGastoxRutaD, Descripcion FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle
		WHERE (idGastoxRutaC = (SELECT idGastoxRutaC FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera WHERE IdRuta = @IdRuta AND IdOperacion = @IdOperacion))
		AND (Adicional = 1)
	END
	
	IF (@Opcion = 2) BEGIN		-- LISTAR GASTO POR ID
		SELECT idGastoxRutaD, Gasto FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle
		WHERE (idGastoxRutaD = @idGastoxRutaD)
	END

	IF (@Opcion = 3) BEGIN		-- LISTAR SUMA DE GASTOS ADICIONALES
		SELECT ISNULL(SUM(Gasto),0) AS 'REINTEGRO' FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle
		WHERE (idGastoxRutaC = (SELECT idGastoxRutaC FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera WHERE IdRuta = @IdRuta AND IdOperacion = @IdOperacion))
		AND (Adicional = 1)  
	END

	IF (@Opcion = 4) BEGIN		-- LISTAR GASTOS DE MAESTRO
		SELECT ISNULL(GastoTotal,0) AS 'GASTO' FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera WHERE IdRuta = @IdRuta AND IdOperacion = @IdOperacion
	END

	IF (@Opcion = 5) BEGIN		-- LISTAR RUTAS CON SU GASTO POR OPERACION
		SELECT GC.IdRuta, R.Descripcion AS 'RUTA' FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera GC
		LEFT JOIN OP_TR_RUTA R ON R.IdRuta = GC.IdRuta
		WHERE GC.IdOperacion = @IdOperacion AND R.Estado = 2
	END
END

--------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 30-01-2023
-- Description:	LISTAR PLANILLAS PENDIENTES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ListarPlanillasPendientes]
@Conductor VARCHAR(200),
@IdOperacion INT
AS
BEGIN
	IF (@IdOperacion = 5) BEGIN
		SELECT C.Nombre AS 'CONDUCTOR', O.Descripcion AS 'OPERACION', TG.FechaCreacion AS 'FECHA',
		TG.NroTicket AS 'PROGRAMACION', TG.CodGasto AS 'PLANILLA', TG.TotalEntregado AS 'MONTO_TOTAL' 
		FROM ReportesApp_Operaciones_TicketGasto_Registro TG
		LEFT JOIN OP_TR_Conductor C ON C.IdConductor = TG.ViConductor
		LEFT JOIN ReportesApp_Operacion_Previaje_Registros R ON R.NroTicket = TG.NroTicket
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = R.TipoProgramacion
		WHERE (EstadoLiquidacion = 0) AND (R.TipoProgramacion NOT IN (8,9,6)) AND (@Conductor IS NULL OR C.Nombre LIKE '%' + @Conductor + '%')
		ORDER BY C.Nombre ASC
	END
	ELSE BEGIN
		SELECT C.Nombre AS 'CONDUCTOR', O.Descripcion AS 'OPERACION', TG.FechaCreacion AS 'FECHA',
		TG.NroTicket AS 'PROGRAMACION', TG.CodGasto AS 'PLANILLA', TG.TotalEntregado AS 'MONTO_TOTAL' 
		FROM ReportesApp_Operaciones_TicketGasto_Registro TG
		LEFT JOIN OP_TR_Conductor C ON C.IdConductor = TG.ViConductor
		LEFT JOIN ReportesApp_Operacion_Previaje_Registros R ON R.NroTicket = TG.NroTicket
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = R.TipoProgramacion
		WHERE (EstadoLiquidacion = 0) AND (R.TipoProgramacion = @IdOperacion) AND (@Conductor IS NULL OR C.Nombre LIKE '%' + @Conductor + '%')
		ORDER BY C.Nombre ASC
	END
END

--------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 19-02-2024
-- Description:	MODIFICAR FECHA VIATICO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ModificarFechaViatico]
@idListaViatico INT,
@idConductor INT,
@Planilla VARCHAR(50),
@NroComprobante VARCHAR(50),
@FechaViatico DATETIME
AS
DECLARE @NroRP INT
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Datos del Viático Actualizados.'

BEGIN TRAN
BEGIN TRY
	--SET @NroRP = (SELECT CajaChicaNumero FROM AP_CajaChica WHERE LTRIM(RTRIM(NumeroDocumentoInterno)) = @Planilla)

	UPDATE ReportesApp_Operaciones_TicketGasto_ListaViaticos
	SET FechaViatico = @FechaViatico, Comprobante = @NroComprobante
	WHERE idListaViatico = @idListaViatico AND idConductor = @idConductor
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
-- Create date: 11-03-2023
-- Description:	GENERAR REPORTE DE PLANILLAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_GenerarReportePlanillas]
@Sucursal VARCHAR(4),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@Detalle INT
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@Detalle = 1) BEGIN
		IF (@Sucursal = 'B') BEGIN
			SELECT DISTINCT CD.Sucursal AS 'SUCURSAL', CJ.CajaChicaNumero AS 'RG', CJ.CompaniaSocio AS 'COMPAÑÍA', P.Busqueda AS 'BENEFICIARIO',			CJ.Descripcion AS 'DESCRIPCIÓN', (CASE WHEN CJ.MonedaPago = 'LO' THEN 'Local' ELSE 'Extranjera' END) AS 'MONEDA', CJ.MontoTotal AS 'MONTO_TOTAL',			CONVERT(VARCHAR,CJ.FechaPreparacion,103) AS 'PREPARACIÓN', D.Secuencia, D.Descripcion AS 'GASTO', D.MontoMonedaPago AS 'MONTO',			CJ.NumeroAdelanto AS 'ADELANTO', P2.Busqueda AS 'USUARIO_LIQUIDACION'			FROM AP_CajaChica CJ			INNER JOIN PersonaMast P ON (CJ.Beneficiario = P.Persona)			LEFT JOIN PersonaMast P2 ON (CJ.PreparadoPor = P2.Persona)			LEFT JOIN AP_CajaChicaDistribucion CD ON (CD.CajaChicaNumero = CJ.CajaChicaNumero)			LEFT JOIN AP_CajaChicaDetalle D ON (D.CajaChicaNumero = CJ.CajaChicaNumero)			WHERE (CJ.CajaChicaReporteFlag = 'R') AND (CJ.CompaniaSocio = '10000000') AND (CJ.Clasificacion = 'ER') AND (CJ.MonedaPago = 'LO')			AND (CJ.FechaPreparacion BETWEEN @FINICIO AND @FFIN)			ORDER BY CJ.CajaChicaNumero, D.Secuencia
		END
	
		IF (@Sucursal = 'BTRU') BEGIN
			SELECT DISTINCT CD.Sucursal AS 'SUCURSAL', CJ.CajaChicaNumero AS 'RG', CJ.CompaniaSocio AS 'COMPAÑÍA', P.Busqueda AS 'BENEFICIARIO',			CJ.Descripcion AS 'DESCRIPCIÓN', (CASE WHEN CJ.MonedaPago = 'LO' THEN 'Local' ELSE 'Extranjera' END) AS 'MONEDA', CJ.MontoTotal AS 'MONTO_TOTAL',			CONVERT(VARCHAR,CJ.FechaPreparacion,103) AS 'PREPARACIÓN', D.Secuencia, D.Descripcion AS 'GASTO', D.MontoMonedaPago AS 'MONTO',			CJ.NumeroAdelanto AS 'ADELANTO', P2.Busqueda AS 'USUARIO_LIQUIDACION'			FROM AP_CajaChica CJ			INNER JOIN PersonaMast P ON (CJ.Beneficiario = P.Persona)			LEFT JOIN PersonaMast P2 ON (CJ.PreparadoPor = P2.Persona)			LEFT JOIN AP_CajaChicaDistribucion CD ON (CD.CajaChicaNumero = CJ.CajaChicaNumero)			LEFT JOIN AP_CajaChicaDetalle D ON (D.CajaChicaNumero = CJ.CajaChicaNumero)			WHERE (CJ.CajaChicaReporteFlag = 'R') AND (CJ.CompaniaSocio = '10000000') AND (CJ.Clasificacion = 'ER') AND (CJ.MonedaPago = 'LO')			AND (CD.Sucursal = 'BTRU') AND (CJ.FechaPreparacion BETWEEN @FINICIO AND @FFIN)			ORDER BY CJ.CajaChicaNumero, D.Secuencia
		END

		IF (@Sucursal = 'BLIM') BEGIN
			SELECT DISTINCT CD.Sucursal AS 'SUCURSAL', CJ.CajaChicaNumero AS 'RG', CJ.CompaniaSocio AS 'COMPAÑÍA', P.Busqueda AS 'BENEFICIARIO',			CJ.Descripcion AS 'DESCRIPCIÓN', (CASE WHEN CJ.MonedaPago = 'LO' THEN 'Local' ELSE 'Extranjera' END) AS 'MONEDA', CJ.MontoTotal AS 'MONTO_TOTAL',			CONVERT(VARCHAR,CJ.FechaPreparacion,103) AS 'PREPARACIÓN', D.Secuencia, D.Descripcion AS 'GASTO', D.MontoMonedaPago AS 'MONTO',			CJ.NumeroAdelanto AS 'ADELANTO', P2.Busqueda AS 'USUARIO_LIQUIDACION'			FROM AP_CajaChica CJ			INNER JOIN PersonaMast P ON (CJ.Beneficiario = P.Persona)			LEFT JOIN PersonaMast P2 ON (CJ.PreparadoPor = P2.Persona)			LEFT JOIN AP_CajaChicaDistribucion CD ON (CD.CajaChicaNumero = CJ.CajaChicaNumero)			LEFT JOIN AP_CajaChicaDetalle D ON (D.CajaChicaNumero = CJ.CajaChicaNumero)			WHERE (CJ.CajaChicaReporteFlag = 'R') AND (CJ.CompaniaSocio = '10000000') AND (CJ.Clasificacion = 'ER') AND (CJ.MonedaPago = 'LO')			AND (CD.Sucursal = 'BLIM') AND (CJ.FechaPreparacion BETWEEN @FINICIO AND @FFIN)			ORDER BY CJ.CajaChicaNumero, D.Secuencia
		END

		IF (@Sucursal = 'PAIT') BEGIN
			SELECT DISTINCT CD.Sucursal AS 'SUCURSAL', CJ.CajaChicaNumero AS 'RG', CJ.CompaniaSocio AS 'COMPAÑÍA', P.Busqueda AS 'BENEFICIARIO',			CJ.Descripcion AS 'DESCRIPCIÓN', (CASE WHEN CJ.MonedaPago = 'LO' THEN 'Local' ELSE 'Extranjera' END) AS 'MONEDA', CJ.MontoTotal AS 'MONTO_TOTAL',			CONVERT(VARCHAR,CJ.FechaPreparacion,103) AS 'PREPARACIÓN', D.Secuencia, D.Descripcion AS 'GASTO', D.MontoMonedaPago AS 'MONTO',			CJ.NumeroAdelanto AS 'ADELANTO', P2.Busqueda AS 'USUARIO_LIQUIDACION'			FROM AP_CajaChica CJ			INNER JOIN PersonaMast P ON (CJ.Beneficiario = P.Persona)			LEFT JOIN PersonaMast P2 ON (CJ.PreparadoPor = P2.Persona)			LEFT JOIN AP_CajaChicaDistribucion CD ON (CD.CajaChicaNumero = CJ.CajaChicaNumero)			LEFT JOIN AP_CajaChicaDetalle D ON (D.CajaChicaNumero = CJ.CajaChicaNumero)			WHERE (CJ.CajaChicaReporteFlag = 'R') AND (CJ.CompaniaSocio = '10000000') AND (CJ.Clasificacion = 'ER') AND (CJ.MonedaPago = 'LO')			AND (CD.Sucursal = 'PAIT') AND (CJ.FechaPreparacion BETWEEN @FINICIO AND @FFIN)			ORDER BY CJ.CajaChicaNumero, D.Secuencia
		END
	END

	IF (@Detalle = 0) BEGIN
		IF (@Sucursal = 'B') BEGIN
			SELECT DISTINCT CD.Sucursal AS 'SUCURSAL', CJ.CajaChicaNumero AS 'RG', CJ.CompaniaSocio AS 'COMPAÑÍA', P.Busqueda AS 'BENEFICIARIO',			CJ.Descripcion AS 'DESCRIPCIÓN', (CASE WHEN CJ.MonedaPago = 'LO' THEN 'Local' ELSE 'Extranjera' END) AS 'MONEDA', CJ.MontoTotal AS 'MONTO_TOTAL',			CONVERT(VARCHAR,CJ.FechaPreparacion,103) AS 'PREPARACIÓN', CJ.NumeroAdelanto AS 'ADELANTO', P2.Busqueda AS 'USUARIO_LIQUIDACION'			FROM AP_CajaChica CJ			INNER JOIN PersonaMast P ON (CJ.Beneficiario = P.Persona)			LEFT JOIN PersonaMast P2 ON (CJ.PreparadoPor = P2.Persona)			LEFT JOIN AP_CajaChicaDistribucion CD ON (CD.CajaChicaNumero = CJ.CajaChicaNumero)			WHERE (CJ.CajaChicaReporteFlag = 'R') AND (CJ.CompaniaSocio = '10000000') AND (CJ.Clasificacion = 'ER') AND (CJ.MonedaPago = 'LO')			AND (CJ.FechaPreparacion BETWEEN @FINICIO AND @FFIN)			ORDER BY CJ.CajaChicaNumero
		END

		IF (@Sucursal = 'BTRU') BEGIN
			SELECT DISTINCT CD.Sucursal AS 'SUCURSAL', CJ.CajaChicaNumero AS 'RG', CJ.CompaniaSocio AS 'COMPAÑÍA', P.Busqueda AS 'BENEFICIARIO',			CJ.Descripcion AS 'DESCRIPCIÓN', (CASE WHEN CJ.MonedaPago = 'LO' THEN 'Local' ELSE 'Extranjera' END) AS 'MONEDA', CJ.MontoTotal AS 'MONTO_TOTAL',			CONVERT(VARCHAR,CJ.FechaPreparacion,103) AS 'PREPARACIÓN', CJ.NumeroAdelanto AS 'ADELANTO', P2.Busqueda AS 'USUARIO_LIQUIDACION'			FROM AP_CajaChica CJ			INNER JOIN PersonaMast P ON (CJ.Beneficiario = P.Persona)			LEFT JOIN PersonaMast P2 ON (CJ.PreparadoPor = P2.Persona)			LEFT JOIN AP_CajaChicaDistribucion CD ON (CD.CajaChicaNumero = CJ.CajaChicaNumero)			WHERE (CJ.CajaChicaReporteFlag = 'R') AND (CJ.CompaniaSocio = '10000000') AND (CJ.Clasificacion = 'ER') AND (CJ.MonedaPago = 'LO')			AND (CD.Sucursal = 'BTRU') AND (CJ.FechaPreparacion BETWEEN @FINICIO AND @FFIN)			ORDER BY CJ.CajaChicaNumero
		END

		IF (@Sucursal = 'BLIM') BEGIN
			SELECT DISTINCT CD.Sucursal AS 'SUCURSAL', CJ.CajaChicaNumero AS 'RG', CJ.CompaniaSocio AS 'COMPAÑÍA', P.Busqueda AS 'BENEFICIARIO',			CJ.Descripcion AS 'DESCRIPCIÓN', (CASE WHEN CJ.MonedaPago = 'LO' THEN 'Local' ELSE 'Extranjera' END) AS 'MONEDA', CJ.MontoTotal AS 'MONTO_TOTAL',			CONVERT(VARCHAR,CJ.FechaPreparacion,103) AS 'PREPARACIÓN', CJ.NumeroAdelanto AS 'ADELANTO', P2.Busqueda AS 'USUARIO_LIQUIDACION'			FROM AP_CajaChica CJ			INNER JOIN PersonaMast P ON (CJ.Beneficiario = P.Persona)			LEFT JOIN PersonaMast P2 ON (CJ.PreparadoPor = P2.Persona)			LEFT JOIN AP_CajaChicaDistribucion CD ON (CD.CajaChicaNumero = CJ.CajaChicaNumero)			WHERE (CJ.CajaChicaReporteFlag = 'R') AND (CJ.CompaniaSocio = '10000000') AND (CJ.Clasificacion = 'ER') AND (CJ.MonedaPago = 'LO')			AND (CD.Sucursal = 'BLIM') AND (CJ.FechaPreparacion BETWEEN @FINICIO AND @FFIN)			ORDER BY CJ.CajaChicaNumero
		END

		IF (@Sucursal = 'PAIT') BEGIN
			SELECT DISTINCT CD.Sucursal AS 'SUCURSAL', CJ.CajaChicaNumero AS 'RG', CJ.CompaniaSocio AS 'COMPAÑÍA', P.Busqueda AS 'BENEFICIARIO',			CJ.Descripcion AS 'DESCRIPCIÓN', (CASE WHEN CJ.MonedaPago = 'LO' THEN 'Local' ELSE 'Extranjera' END) AS 'MONEDA', CJ.MontoTotal AS 'MONTO_TOTAL',			CONVERT(VARCHAR,CJ.FechaPreparacion,103) AS 'PREPARACIÓN', CJ.NumeroAdelanto AS 'ADELANTO', P2.Busqueda AS 'USUARIO_LIQUIDACION'			FROM AP_CajaChica CJ			INNER JOIN PersonaMast P ON (CJ.Beneficiario = P.Persona)			LEFT JOIN PersonaMast P2 ON (CJ.PreparadoPor = P2.Persona)			LEFT JOIN AP_CajaChicaDistribucion CD ON (CD.CajaChicaNumero = CJ.CajaChicaNumero)			WHERE (CJ.CajaChicaReporteFlag = 'R') AND (CJ.CompaniaSocio = '10000000') AND (CJ.Clasificacion = 'ER') AND (CJ.MonedaPago = 'LO')			AND (CD.Sucursal = 'PAIT') AND (CJ.FechaPreparacion BETWEEN @FINICIO AND @FFIN)			ORDER BY CJ.CajaChicaNumero
		END
	END
END

--------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 22-03-2024
-- Description:	CONTAR PLANILLAS PENDIENTES
-- =============================================
/*
EXEC ReportesApp_Operaciones_TicketGasto_ContadorPlanillasPendientes 186
EXEC ReportesApp_Operaciones_TicketGasto_ContadorPlanillasPendientes 11561
*/
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ContadorPlanillasPendientes]
@idConductor INT
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @Persona INT
DECLARE @NombreCompleto VARCHAR(350)
DECLARE @Contador INT
DECLARE @TEMP_ADELANTOS TABLE(Nro INT, NumeroAdelanto INT, Planilla VARCHAR(20), IdProgramacion INT, Sucursal VARCHAR(10), TotalDias INT)
DECLARE @i INT

SET @Exito = '0 = No hay planillas pendientes.'

BEGIN
	SET @Persona = (SELECT P.Persona FROM OP_TR_Conductor C LEFT JOIN PersonaMast P ON P.Persona = C.IdPersona WHERE C.IdConductor = @idConductor)
	SET @NombreCompleto = (SELECT LTRIM(RTRIM(P.NombreCompleto)) FROM OP_TR_Conductor C LEFT JOIN PersonaMast P ON P.Persona = C.IdPersona WHERE C.IdConductor = @idConductor)
	SET @Contador = (SELECT COUNT(A.NumeroAdelanto) FROM AP_GastoAdelanto A					LEFT JOIN PersonaMast P ON (A.Persona = P.Persona)					LEFT JOIN AP_CajaChica C ON LTRIM(RTRIM(C.NumeroDocumentoInterno)) = LTRIM(RTRIM(A.NumeroDocumentoInterno))					LEFT JOIN ReportesApp_Operaciones_TicketGasto_Registro TG ON LTRIM(RTRIM(TG.CodGasto)) = LTRIM(RTRIM(A.NumeroDocumentoInterno))					LEFT JOIN ReportesApp_Operacion_Previaje_Registros R ON R.NroTicket = TG.NroTicket					WHERE (A.TipoAdelanto = 'E') AND (A.UnidadNegocio IN ('0001','0002','TRAN')) AND YEAR(A.FechaDocumento)>'2023' AND (A.Estado = 'PA') AND ((R.Estado = 9)
					AND (TG.EstadoLiquidacion = 0)) AND (C.CajaChicaNumero IS NULL) AND (A.Descripcion LIKE '%' + 'GASTOS DE VIAJE' + '%') AND (A.Persona = @Persona))

	IF (@Contador >= 2) BEGIN
		
		SET @Exito = '-1 = El conductor ' + @NombreCompleto +' tiene ' + CONVERT(VARCHAR,@Contador) + ' planillas pendientes de rendir, no puede emitir gasto. Política G.G. Debe pasar a liquidar.'
	
		SELECT @exito exito
		
		/*
		IF (@idConductor IN (11542)) BEGIN
			SET @Exito = '0 = No hay planillas pendientes.'
		END
		ELSE BEGIN
			SET @Exito = '-1 = El conductor ' + @NombreCompleto +' tiene ' + CONVERT(VARCHAR,@Contador) + ' planillas pendientes de rendir, no puede emitir gasto. Política G.G. Debe pasar a liquidar.'
		END
		
		SELECT @exito exito
		*/
	END

	IF (@Contador = 1) BEGIN		INSERT INTO @TEMP_ADELANTOS		SELECT ROW_NUMBER() OVER(ORDER BY A.NumeroAdelanto ASC), A.NumeroAdelanto, LTRIM(RTRIM(A.NumeroDocumentoInterno)), TG.IdOperacion, R.Sucursal,		DATEDIFF(DAY,A.FechaDocumento,GETDATE())		FROM AP_GastoAdelanto A		LEFT JOIN PersonaMast P ON (A.Persona = P.Persona)		LEFT JOIN AP_CajaChica C ON LTRIM(RTRIM(C.NumeroDocumentoInterno)) = LTRIM(RTRIM(A.NumeroDocumentoInterno))		LEFT JOIN ReportesApp_Operaciones_TicketGasto_Registro TG ON TG.CodGasto = LTRIM(RTRIM(A.NumeroDocumentoInterno))		LEFT JOIN ReportesApp_Operacion_Previaje_Registros R ON R.NroTicket = TG.NroTicket		WHERE (A.TipoAdelanto = 'E') AND (A.UnidadNegocio IN ('0001','0002','TRAN')) AND YEAR(A.FechaDocumento)>'2023' AND (A.Estado = 'PA') AND ((R.Estado = 9)
		OR (TG.EstadoLiquidacion = 0)) AND (C.CajaChicaNumero IS NULL) AND (A.Descripcion LIKE '%' + 'GASTOS DE VIAJE' + '%') AND (A.Persona = @Persona)
		ORDER BY A.NumeroAdelanto

		SET @i = 1
		WHILE(@i <= (SELECT COUNT(Nro) FROM @TEMP_ADELANTOS)) BEGIN
			IF ((SELECT IdProgramacion FROM @TEMP_ADELANTOS WHERE Nro = @i) = 1) BEGIN		-- TOLVAS
				IF ((SELECT TotalDias FROM @TEMP_ADELANTOS WHERE Nro = @i) >= 4) BEGIN
					SET @Exito = '-2 = El conductor ' + @NombreCompleto +' tiene la planilla ' + (SELECT Planilla FROM @TEMP_ADELANTOS WHERE Nro = @i)
					+ ' pendiente de rendir con más de 4 días de antiguedad, no puede emitir gasto. Política G.G. Debe pasar a liquidar.'
				END
				ELSE BEGIN
					SET @Exito = '0 = No hay planillas pendientes.' 
				END
			END

			IF ((SELECT IdProgramacion FROM @TEMP_ADELANTOS WHERE Nro = @i) = 2) BEGIN		-- LINDLEY
				IF ((SELECT TotalDias FROM @TEMP_ADELANTOS WHERE Nro = @i) >= 10) BEGIN
					SET @Exito = '-2 = El conductor ' + @NombreCompleto +' tiene la planilla ' + (SELECT Planilla FROM @TEMP_ADELANTOS WHERE Nro = @i)
					+' pendiente de rendir con más de 10 días de antiguedad, no puede emitir gasto. Política G.G. Debe pasar a liquidar.'
				END
				ELSE BEGIN
					SET @Exito = '0 = No hay planillas pendientes.' 
				END
			END

			IF ((SELECT IdProgramacion FROM @TEMP_ADELANTOS WHERE Nro = @i) = 4) BEGIN		-- GENERAL
				IF ((SELECT TotalDias FROM @TEMP_ADELANTOS WHERE Nro = @i) >= 10) BEGIN
					SET @Exito = '-2 = El conductor ' + @NombreCompleto +' tiene la planilla ' + (SELECT Planilla FROM @TEMP_ADELANTOS WHERE Nro = @i)
					+' pendiente de rendir con más de 10 días de antiguedad, no puede emitir gasto. Política G.G. Debe pasar a liquidar.'
				END
				ELSE BEGIN
					SET @Exito = '0 = No hay planillas pendientes.' 
				END
			END

			IF ((SELECT IdProgramacion FROM @TEMP_ADELANTOS WHERE Nro = @i) = 3) BEGIN		-- LIMAGAS - TRUJILLO
				IF (((SELECT TotalDias FROM @TEMP_ADELANTOS WHERE Nro = @i) >= 7) AND ((SELECT Sucursal FROM @TEMP_ADELANTOS WHERE Nro = @i) = 'TRUJILLO')) BEGIN
					SET @Exito = '-2 = El conductor ' + @NombreCompleto +' tiene la planilla ' + (SELECT Planilla FROM @TEMP_ADELANTOS WHERE Nro = @i)
					+' pendiente de rendir con más de 7 días de antiguedad, no puede emitir gasto. Política G.G. Debe pasar a liquidar.'
				END
				ELSE BEGIN
					SET @Exito = '0 = No hay planillas pendientes.' 
				END
			END

			IF ((SELECT IdProgramacion FROM @TEMP_ADELANTOS WHERE Nro = @i) = 3) BEGIN		-- LIMAGAS - LIMA
				IF (((SELECT TotalDias FROM @TEMP_ADELANTOS WHERE Nro = @i) >= 15) AND ((SELECT Sucursal FROM @TEMP_ADELANTOS WHERE Nro = @i) = 'LIMA')) BEGIN
					SET @Exito = '-2 = El conductor ' + @NombreCompleto +' tiene la planilla ' + (SELECT Planilla FROM @TEMP_ADELANTOS WHERE Nro = @i)
					+' pendiente de rendir con más de 15 días de antiguedad, no puede emitir gasto. Política G.G. Debe pasar a liquidar.'
				END
				ELSE BEGIN
					SET @Exito = '0 = No hay planillas pendientes.' 
				END
			END

			SET @i = @i + 1
		END

		SELECT @exito exito
	END

	IF (@Contador = 0) BEGIN
		SELECT @exito exito
	END
END

---------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 24-06-2024
-- Description:	LISTAR REPORTE DE REINTEGROS
-- =============================================
/*
EXEC ReportesApp_Operaciones_TicketGasto_GenerarReporteReintegros @Sucursal='BTRU', @FechaInicio='01/06/2024',@FechaFin='28/06/2024'
*/
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_GenerarReporteReintegros]
@Sucursal VARCHAR(4),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
DECLARE @TEMP_CG TABLE(RP INT, CONCEPTO VARCHAR(500), GASTO DECIMAL(10,2))
BEGIN
	INSERT INTO @TEMP_CG(RP,CONCEPTO,GASTO)
	SELECT CD.CajaChicaNumero AS 'RP', CD.ConceptoGasto+' - '+C.DescripcionLocal AS 'CONCEPTO', SUM(CD.MontoMonedaPago) AS 'GASTO'
	FROM AP_CajaChicaDetalle CD
	LEFT JOIN AP_ConceptoGasto C ON C.ConceptoGasto = CD.ConceptoGasto
	LEFT JOIN AP_CajaChica CJ ON CD.CajaChicaNumero = CJ.CajaChicaNumero
	LEFT JOIN ReportesApp_Operaciones_TicketGasto_Registro R ON R.CodGasto = CJ.NumeroDocumentoInterno
	WHERE (CJ.CajaChicaReporteFlag = 'R') AND (CJ.CompaniaSocio = '10000000') AND (CJ.Clasificacion = 'ER') AND (R.Reintegro != 0.00) AND
	(CJ.FechaPreparacion BETWEEN @FINICIO AND @FFIN)
	GROUP BY CD.CajaChicaNumero, CD.ConceptoGasto, C.DescripcionLocal
	ORDER BY CD.CajaChicaNumero, CD.ConceptoGasto

	IF (@Sucursal = 'B') BEGIN
		SELECT DISTINCT CJ.CompaniaSocio AS 'COMPAÑÍA', CD.Sucursal AS 'SUCURSAL', CJ.CajaChicaNumero AS 'RG', CJ.NumeroDocumentoInterno AS 'PLANILLA', PR.CodViaje AS 'COD_VIAJE',		CONVERT(VARCHAR,CJ.FechaPreparacion,103) AS 'PREPARACIÓN', P.Busqueda AS 'BENEFICIARIO', CJ.Descripcion AS 'DESCRIPCION', R.TotalEntregado AS 'IMPORTE_ENTREGADO',		CJ.MontoTotal AS 'GASTO_TOTAL', X.CONCEPTO, X.GASTO, P2.Busqueda AS 'USUARIO_LIQUIDACION'
		FROM AP_CajaChica CJ		INNER JOIN PersonaMast P ON (CJ.Beneficiario = P.Persona)		INNER JOIN PersonaMast P2 ON (CJ.PreparadoPor = P2.Persona)		LEFT JOIN AP_CajaChicaDistribucion CD ON (CD.CajaChicaNumero = CJ.CajaChicaNumero)		LEFT JOIN AP_CajaChicaDetalle D ON (D.CajaChicaNumero = CJ.CajaChicaNumero)		LEFT JOIN ReportesApp_Operaciones_TicketGasto_Registro R ON R.CodGasto = CJ.NumeroDocumentoInterno		LEFT JOIN ReportesApp_Operacion_Previaje_Registros PR ON PR.NroTicket = R.NroTicket		INNER JOIN @TEMP_CG X ON (CJ.CajaChicaNumero = X.RP)		WHERE (CJ.CompaniaSocio = '10000000') AND (CJ.Clasificacion = 'ER') AND (CJ.FechaPreparacion BETWEEN @FINICIO AND @FFIN)
		ORDER BY CJ.CajaChicaNumero, CJ.NumeroDocumentoInterno 
	END
	ELSE BEGIN
		SELECT DISTINCT CJ.CompaniaSocio AS 'COMPAÑÍA', CD.Sucursal AS 'SUCURSAL', CJ.CajaChicaNumero AS 'RG', CJ.NumeroDocumentoInterno AS 'PLANILLA', PR.CodViaje AS 'COD_VIAJE',		CONVERT(VARCHAR,CJ.FechaPreparacion,103) AS 'PREPARACIÓN', P.Busqueda AS 'BENEFICIARIO', CJ.Descripcion AS 'DESCRIPCION', R.TotalEntregado AS 'IMPORTE_ENTREGADO',		CJ.MontoTotal AS 'GASTO_TOTAL', X.CONCEPTO, X.GASTO, P2.Busqueda AS 'USUARIO_LIQUIDACION'
		FROM AP_CajaChica CJ		INNER JOIN PersonaMast P ON (CJ.Beneficiario = P.Persona)		INNER JOIN PersonaMast P2 ON (CJ.PreparadoPor = P2.Persona)		LEFT JOIN AP_CajaChicaDistribucion CD ON (CD.CajaChicaNumero = CJ.CajaChicaNumero)		LEFT JOIN AP_CajaChicaDetalle D ON (D.CajaChicaNumero = CJ.CajaChicaNumero)		LEFT JOIN ReportesApp_Operaciones_TicketGasto_Registro R ON R.CodGasto = CJ.NumeroDocumentoInterno		LEFT JOIN ReportesApp_Operacion_Previaje_Registros PR ON PR.NroTicket = R.NroTicket		INNER JOIN @TEMP_CG X ON (CJ.CajaChicaNumero = X.RP)		WHERE (CJ.CompaniaSocio = '10000000') AND (CJ.Clasificacion = 'ER') AND (LTRIM(RTRIM(CD.Sucursal)) = @Sucursal) AND
		(CJ.FechaPreparacion BETWEEN @FINICIO AND @FFIN)
		ORDER BY CJ.CajaChicaNumero, CJ.NumeroDocumentoInterno 
	END
END

-----------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 28-06-2024
-- Description:	LISTAR REPORTE DE REINTEGROS
-- =============================================
/*
EXEC ReportesApp_Operaciones_TicketGasto_ListarGastoXConcepto @Sucursal='BTRU', @FechaInicio='01/06/2024',@FechaFin='28/06/2024'
*/
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ListarGastoXConcepto]
@Sucursal VARCHAR(4),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@Sucursal = 'B') BEGIN
		SELECT TG.NroRepGasto AS 'RG', CJ.Descripcion AS 'DESCRIPCION', T.ConceptoGasto+' - '+C.DescripcionLocal AS 'CONCEPTO', SUM(GD.Gasto) AS 'GASTO'
		FROM ReportesApp_Operaciones_TicketGasto_Registro TG
		LEFT JOIN ReportesApp_Operacion_Previaje_Registros R ON R.NroTicket = TG.NroTicket
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera GC ON GC.IdRuta = ISNULL(R.IdRuta,TG.IdRuta) AND GC.IdOperacion = TG.IdOperacion
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle GD ON GD.idGastoxRutaC = GC.idGastoxRutaC AND GD.Adicional = 0
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_TipoGasto T ON T.idTipoGasto = GD.idTipoGasto
		LEFT JOIN AP_ConceptoGasto C ON C.ConceptoGasto = T.ConceptoGasto
		LEFT JOIN AP_CajaChica CJ ON TG.CodGasto = CJ.NumeroDocumentoInterno
		LEFT JOIN AP_CajaChicaDistribucion CD ON (CD.CajaChicaNumero = CJ.CajaChicaNumero) AND (CD.Secuencia = 1)
		WHERE (TG.Reintegro != 0.00) AND (TG.FechaLiquidacion BETWEEN @FINICIO AND @FFIN)
		GROUP BY TG.NroRepGasto, CJ.Descripcion, T.ConceptoGasto, C.DescripcionLocal
		ORDER BY TG.NroRepGasto ASC
	END
	ELSE BEGIN
		SELECT TG.NroRepGasto AS 'RG', CJ.Descripcion AS 'DESCRIPCION', T.ConceptoGasto+' - '+C.DescripcionLocal AS 'CONCEPTO', SUM(GD.Gasto) AS 'GASTO'
		FROM ReportesApp_Operaciones_TicketGasto_Registro TG
		LEFT JOIN ReportesApp_Operacion_Previaje_Registros R ON R.NroTicket = TG.NroTicket
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera GC ON GC.IdRuta = ISNULL(R.IdRuta,TG.IdRuta) AND GC.IdOperacion = TG.IdOperacion
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaDetalle GD ON GD.idGastoxRutaC = GC.idGastoxRutaC AND GD.Adicional = 0
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_TipoGasto T ON T.idTipoGasto = GD.idTipoGasto
		LEFT JOIN AP_ConceptoGasto C ON C.ConceptoGasto = T.ConceptoGasto
		LEFT JOIN AP_CajaChica CJ ON TG.CodGasto = CJ.NumeroDocumentoInterno
		LEFT JOIN AP_CajaChicaDistribucion CD ON (CD.CajaChicaNumero = CJ.CajaChicaNumero) AND (CD.Secuencia = 1)
		WHERE (TG.Reintegro != 0.00) AND (LTRIM(RTRIM(CD.Sucursal)) = @Sucursal) AND (TG.FechaLiquidacion BETWEEN @FINICIO AND @FFIN)
		GROUP BY TG.NroRepGasto, CJ.Descripcion, T.ConceptoGasto, C.DescripcionLocal
		ORDER BY TG.NroRepGasto ASC
	END
END

--------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 24-05-2025
-- Description:	LISTAR ADELANTOS DE GASTOS ADICIONALES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ListarAdelantosAdicionales]
@IdOperacion INT,
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@IdOperacion = 5) BEGIN
		SELECT TG.CodGasto AS 'PLANILLA', O.Descripcion AS 'OPERACION', RTRIM(PM.NombreCompleto) AS 'CONDUCTOR',		VJ.Descripcion AS 'RUTA', CONVERT(DECIMAL(10,2),A.MontoTotal) AS 'MONTO_ADICIONAL', TG.TotalEntregado AS 'MONTO_TOTAL',		(SELECT LTRIM(RTRIM(STUFF((SELECT ', ' + CONVERT(VARCHAR,Motivo) FROM ReportesApp_Operaciones_TicketGasto_Viaticos WHERE CodGasto = TG.CodGasto
		ORDER BY idViatico ASC FOR XML PATH ('')),1,1,'')))) AS 'DESCRIPCION',		TG.UsuarioCreacion AS 'USUARIO_CREA', A.FechaPreparacion AS 'FECHA_CREA'		FROM ReportesApp_Operaciones_TicketGasto_Registro TG		LEFT JOIN AP_GastoAdelanto A WITH(NOLOCK) ON (A.NumeroDocumentoInterno = LTRIM(RTRIM(TG.CodGasto)))		LEFT JOIN ReportesApp_Operacion_Previaje_Registros P WITH(NOLOCK) ON P.NroTicket = TG.NroTicket
		LEFT JOIN OP_TR_Ruta VJ WITH(NOLOCK) ON VJ.IdRuta = ISNULL(P.IdRuta,TG.IdRuta)
		LEFT JOIN PersonaMast PM WITH(NOLOCK) ON A.Persona = PM.Persona
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O WITH(NOLOCK) ON O.IdOperacion = TG.IdOperacion
		WHERE (A.Descripcion LIKE '%VIATICO ADICIONAL%') AND (A.Estado IN ('PA','AL')) AND (A.FechaPreparacion BETWEEN @FINICIO AND @FFIN)
		ORDER BY TG.CodGasto DESC
	END
	ELSE BEGIN
		SELECT TG.CodGasto AS 'PLANILLA', O.Descripcion AS 'OPERACION', RTRIM(PM.NombreCompleto) AS 'CONDUCTOR',		VJ.Descripcion AS 'RUTA', CONVERT(DECIMAL(10,2),A.MontoTotal) AS 'MONTO_ADICIONAL', TG.TotalEntregado AS 'MONTO_TOTAL',		(SELECT LTRIM(RTRIM(STUFF((SELECT ', ' + CONVERT(VARCHAR,Motivo) FROM ReportesApp_Operaciones_TicketGasto_Viaticos WHERE CodGasto = TG.CodGasto
		ORDER BY idViatico ASC FOR XML PATH ('')),1,1,'')))) AS 'DESCRIPCION',		TG.UsuarioCreacion AS 'USUARIO_CREA', A.FechaPreparacion AS 'FECHA_CREA'		FROM ReportesApp_Operaciones_TicketGasto_Registro TG		LEFT JOIN AP_GastoAdelanto A WITH(NOLOCK) ON (A.NumeroDocumentoInterno = LTRIM(RTRIM(TG.CodGasto)))		LEFT JOIN ReportesApp_Operacion_Previaje_Registros P WITH(NOLOCK) ON P.NroTicket = TG.NroTicket
		LEFT JOIN OP_TR_Ruta VJ WITH(NOLOCK) ON VJ.IdRuta = ISNULL(P.IdRuta,TG.IdRuta)
		LEFT JOIN PersonaMast PM WITH(NOLOCK) ON A.Persona = PM.Persona
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O WITH(NOLOCK) ON O.IdOperacion = TG.IdOperacion
		WHERE (A.Descripcion LIKE '%VIATICO ADICIONAL%') AND (A.Estado IN ('PA','AL')) AND (TG.IdOperacion = @IdOperacion)
		AND (A.FechaPreparacion BETWEEN @FINICIO AND @FFIN)
		ORDER BY TG.CodGasto DESC
	END
END

--------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 15-05-2025
-- Description:	ACTUALIZAR ASISTENCIAS PLANILLAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_AsistenciaPlanillas2]
@CodGasto VARCHAR(30)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Asistencia registrada exitosamente.'

BEGIN TRAN
BEGIN TRY
	DECLARE @NroTicket INT = (SELECT NroTicket FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE CodGasto = @CodGasto)
	DECLARE @IdConductor INT = (SELECT ViConductor FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE CodGasto = @CodGasto)
	DECLARE @IdOperacion INT = (SELECT IdOperacion FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE CodGasto = @CodGasto)
	DECLARE @NroPersona INT = (SELECT P.Persona FROM PersonaMast P
							   LEFT JOIN OP_TR_Conductor C ON RTRIM(C.TipoDocumento) = RTRIM(P.TipoDocumento) AND RTRIM(C.Documento) = RTRIM(P.Documento)
							   WHERE IdConductor = @IdConductor)
	DECLARE @IdRuta INT
	DECLARE @TiempoHoras DECIMAL(10,2)
	DECLARE @FechaInicio DATETIME
	DECLARE @FechaFin DATETIME

	IF (@IdOperacion = 1) BEGIN
		SET @IdRuta = (SELECT IdRuta FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE CodGasto = @CodGasto)
		SET @TiempoHoras = (SELECT TotalDias FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE CodGasto = @CodGasto)
		SET @FechaInicio = (SELECT FechaDocumento FROM AP_GastoAdelanto WHERE (Estado = 'PA' OR Estado = 'AL') AND Descripcion LIKE '%GASTOS DE VIAJE - %'
							AND NumeroDocumentoInterno = @CodGasto)
		SET @FechaFin = (SELECT DATEADD(DAY,@TiempoHoras - 1,@FechaInicio))
	END
	ELSE BEGIN
		SET @IdRuta = (SELECT IdRuta FROM ReportesApp_Operacion_Previaje_Registros WHERE NroTicket = @NroTicket)
		SET @TiempoHoras = (SELECT Tiempo FROM OP_TR_Ruta WHERE IdRuta = @IdRuta)
		SET @FechaInicio = (SELECT FechaDocumento FROM AP_GastoAdelanto WHERE (Estado = 'PA' OR Estado = 'AL') AND Descripcion LIKE '%GASTOS DE VIAJE - %'
							AND NumeroDocumentoInterno = @CodGasto)
		SET @FechaFin = (SELECT DATEADD(HOUR,@TiempoHoras,@FechaInicio))
	END

	DECLARE @Usuario VARCHAR(20) = (SELECT UsuarioCreacion FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE CodGasto = @CodGasto)

	IF (EXISTS(SELECT * FROM ReportesApp_RRHH_AsistenciaView WHERE Anio = YEAR(@FechaInicio) AND Mes = MONTH(@FechaInicio) AND CodPlanilla = 'CD' AND IDPersona = @NroPersona)) BEGIN
		WHILE (CONVERT(DATE,@FechaInicio) <= CONVERT(DATE,@FechaFin)) BEGIN
			IF (NOT EXISTS(SELECT * FROM ReportesApp_RRHH_Asistencia WHERE IdPersona = @NroPersona AND CONVERT(DATE,Fecha) = CONVERT(DATE,@FechaInicio))) BEGIN
				INSERT INTO ReportesApp_RRHH_Asistencia(IdPersona,Planilla,Fecha,IdTipoAsist,ConceptoAcceso,FHRegistra,UserCrea)
				VALUES(@NroPersona,'CD',@FechaInicio,32,'ASIS',GETDATE(),@Usuario)

				UPDATE V 
				SET V.D1 = CASE WHEN DAY(@FechaInicio) = 1 AND (V.D1 IS NULL OR V.D1 = 'DF') THEN 'A' ELSE V.D1 END,
					V.D2 = CASE WHEN DAY(@FechaInicio) = 2 AND (V.D2 IS NULL OR V.D2 = 'DF') THEN 'A' ELSE V.D2 END,
					V.D3 = CASE WHEN DAY(@FechaInicio) = 3 AND (V.D3 IS NULL OR V.D3 = 'DF') THEN 'A' ELSE V.D3 END,
					V.D4 = CASE WHEN DAY(@FechaInicio) = 4 AND (V.D4 IS NULL OR V.D4 = 'DF') THEN 'A' ELSE V.D4 END,
					V.D5 = CASE WHEN DAY(@FechaInicio) = 5 AND (V.D5 IS NULL OR V.D5 = 'DF') THEN 'A' ELSE V.D5 END,
					V.D6 = CASE WHEN DAY(@FechaInicio) = 6 AND (V.D6 IS NULL OR V.D6 = 'DF') THEN 'A' ELSE V.D6 END,
					V.D7 = CASE WHEN DAY(@FechaInicio) = 7 AND (V.D7 IS NULL OR V.D7 = 'DF') THEN 'A' ELSE V.D7 END,
					V.D8 = CASE WHEN DAY(@FechaInicio) = 8 AND (V.D8 IS NULL OR V.D8 = 'DF') THEN 'A' ELSE V.D8 END,
					V.D9 = CASE WHEN DAY(@FechaInicio) = 9 AND (V.D9 IS NULL OR V.D9 = 'DF') THEN 'A' ELSE V.D9 END,
					V.D10 = CASE WHEN DAY(@FechaInicio) = 10 AND (V.D10 IS NULL OR V.D10 = 'DF') THEN 'A' ELSE V.D10 END,
					V.D11 = CASE WHEN DAY(@FechaInicio) = 11 AND (V.D11 IS NULL OR V.D11 = 'DF') THEN 'A' ELSE V.D11 END,
					V.D12 = CASE WHEN DAY(@FechaInicio) = 12 AND (V.D12 IS NULL OR V.D12 = 'DF') THEN 'A' ELSE V.D12 END,
					V.D13 = CASE WHEN DAY(@FechaInicio) = 13 AND (V.D13 IS NULL OR V.D13 = 'DF') THEN 'A' ELSE V.D13 END,
					V.D14 = CASE WHEN DAY(@FechaInicio) = 14 AND (V.D14 IS NULL OR V.D14 = 'DF') THEN 'A' ELSE V.D14 END,
					V.D15 = CASE WHEN DAY(@FechaInicio) = 15 AND (V.D15 IS NULL OR V.D15 = 'DF') THEN 'A' ELSE V.D15 END,
					V.D16 = CASE WHEN DAY(@FechaInicio) = 16 AND (V.D16 IS NULL OR V.D16 = 'DF') THEN 'A' ELSE V.D16 END,
					V.D17 = CASE WHEN DAY(@FechaInicio) = 17 AND (V.D17 IS NULL OR V.D17 = 'DF') THEN 'A' ELSE V.D17 END,
					V.D18 = CASE WHEN DAY(@FechaInicio) = 18 AND (V.D18 IS NULL OR V.D18 = 'DF') THEN 'A' ELSE V.D18 END,
					V.D19 = CASE WHEN DAY(@FechaInicio) = 19 AND (V.D19 IS NULL OR V.D19 = 'DF') THEN 'A' ELSE V.D19 END,
					V.D20 = CASE WHEN DAY(@FechaInicio) = 20 AND (V.D20 IS NULL OR V.D20 = 'DF') THEN 'A' ELSE V.D20 END,
					V.D21 = CASE WHEN DAY(@FechaInicio) = 21 AND (V.D21 IS NULL OR V.D21 = 'DF') THEN 'A' ELSE V.D21 END,
					V.D22 = CASE WHEN DAY(@FechaInicio) = 22 AND (V.D22 IS NULL OR V.D22 = 'DF') THEN 'A' ELSE V.D22 END,
					V.D23 = CASE WHEN DAY(@FechaInicio) = 23 AND (V.D23 IS NULL OR V.D23 = 'DF') THEN 'A' ELSE V.D23 END,
					V.D24 = CASE WHEN DAY(@FechaInicio) = 24 AND (V.D24 IS NULL OR V.D24 = 'DF') THEN 'A' ELSE V.D24 END,
					V.D25 = CASE WHEN DAY(@FechaInicio) = 25 AND (V.D25 IS NULL OR V.D25 = 'DF') THEN 'A' ELSE V.D25 END,
					V.D26 = CASE WHEN DAY(@FechaInicio) = 26 AND (V.D26 IS NULL OR V.D26 = 'DF') THEN 'A' ELSE V.D26 END,
					V.D27 = CASE WHEN DAY(@FechaInicio) = 27 AND (V.D27 IS NULL OR V.D27 = 'DF') THEN 'A' ELSE V.D27 END,
					V.D28 = CASE WHEN DAY(@FechaInicio) = 28 AND (V.D28 IS NULL OR V.D28 = 'DF') THEN 'A' ELSE V.D28 END,
					V.D29 = CASE WHEN DAY(@FechaInicio) = 29 AND (V.D29 IS NULL OR V.D29 = 'DF') THEN 'A' ELSE V.D29 END,
					V.D30 = CASE WHEN DAY(@FechaInicio) = 30 AND (V.D30 IS NULL OR V.D30 = 'DF') THEN 'A' ELSE V.D30 END,
					V.D31 = CASE WHEN DAY(@FechaInicio) = 31 AND (V.D31 IS NULL OR V.D31 = 'DF') THEN 'A' ELSE V.D31 END
				FROM ReportesApp_RRHH_AsistenciaView V
				WHERE V.Anio = YEAR(@FechaInicio) AND V.Mes = MONTH(@FechaInicio) AND V.CodPlanilla = 'CD' AND IDPersona = @NroPersona
			END
			ELSE BEGIN
				IF ((SELECT IDTipoAsist FROM ReportesApp_RRHH_Asistencia WHERE IdPersona = @NroPersona AND CONVERT(DATE,Fecha) = CONVERT(DATE,@FechaInicio)) IN (29,32)) BEGIN
					UPDATE ReportesApp_RRHH_Asistencia
					SET IDEstablecimiento = NULL, IDTipoAsist = 32, ConceptoAcceso = 'ASIS', UserCrea = @Usuario, FHRegistra = GETDATE()
					WHERE IDPersona = @NroPersona AND CONVERT(DATE,Fecha) = CONVERT(DATE,@FechaInicio) 
					
					UPDATE V 
					SET V.D1 = CASE WHEN DAY(@FechaInicio) = 1 AND (V.D1 IS NULL OR V.D1 = 'DF') THEN 'A' ELSE V.D1 END,
						V.D2 = CASE WHEN DAY(@FechaInicio) = 2 AND (V.D2 IS NULL OR V.D2 = 'DF') THEN 'A' ELSE V.D2 END,
						V.D3 = CASE WHEN DAY(@FechaInicio) = 3 AND (V.D3 IS NULL OR V.D3 = 'DF') THEN 'A' ELSE V.D3 END,
						V.D4 = CASE WHEN DAY(@FechaInicio) = 4 AND (V.D4 IS NULL OR V.D4 = 'DF') THEN 'A' ELSE V.D4 END,
						V.D5 = CASE WHEN DAY(@FechaInicio) = 5 AND (V.D5 IS NULL OR V.D5 = 'DF') THEN 'A' ELSE V.D5 END,
						V.D6 = CASE WHEN DAY(@FechaInicio) = 6 AND (V.D6 IS NULL OR V.D6 = 'DF') THEN 'A' ELSE V.D6 END,
						V.D7 = CASE WHEN DAY(@FechaInicio) = 7 AND (V.D7 IS NULL OR V.D7 = 'DF') THEN 'A' ELSE V.D7 END,
						V.D8 = CASE WHEN DAY(@FechaInicio) = 8 AND (V.D8 IS NULL OR V.D8 = 'DF') THEN 'A' ELSE V.D8 END,
						V.D9 = CASE WHEN DAY(@FechaInicio) = 9 AND (V.D9 IS NULL OR V.D9 = 'DF') THEN 'A' ELSE V.D9 END,
						V.D10 = CASE WHEN DAY(@FechaInicio) = 10 AND (V.D10 IS NULL OR V.D10 = 'DF') THEN 'A' ELSE V.D10 END,
						V.D11 = CASE WHEN DAY(@FechaInicio) = 11 AND (V.D11 IS NULL OR V.D11 = 'DF') THEN 'A' ELSE V.D11 END,
						V.D12 = CASE WHEN DAY(@FechaInicio) = 12 AND (V.D12 IS NULL OR V.D12 = 'DF') THEN 'A' ELSE V.D12 END,
						V.D13 = CASE WHEN DAY(@FechaInicio) = 13 AND (V.D13 IS NULL OR V.D13 = 'DF') THEN 'A' ELSE V.D13 END,
						V.D14 = CASE WHEN DAY(@FechaInicio) = 14 AND (V.D14 IS NULL OR V.D14 = 'DF') THEN 'A' ELSE V.D14 END,
						V.D15 = CASE WHEN DAY(@FechaInicio) = 15 AND (V.D15 IS NULL OR V.D15 = 'DF') THEN 'A' ELSE V.D15 END,
						V.D16 = CASE WHEN DAY(@FechaInicio) = 16 AND (V.D16 IS NULL OR V.D16 = 'DF') THEN 'A' ELSE V.D16 END,
						V.D17 = CASE WHEN DAY(@FechaInicio) = 17 AND (V.D17 IS NULL OR V.D17 = 'DF') THEN 'A' ELSE V.D17 END,
						V.D18 = CASE WHEN DAY(@FechaInicio) = 18 AND (V.D18 IS NULL OR V.D18 = 'DF') THEN 'A' ELSE V.D18 END,
						V.D19 = CASE WHEN DAY(@FechaInicio) = 19 AND (V.D19 IS NULL OR V.D19 = 'DF') THEN 'A' ELSE V.D19 END,
						V.D20 = CASE WHEN DAY(@FechaInicio) = 20 AND (V.D20 IS NULL OR V.D20 = 'DF') THEN 'A' ELSE V.D20 END,
						V.D21 = CASE WHEN DAY(@FechaInicio) = 21 AND (V.D21 IS NULL OR V.D21 = 'DF') THEN 'A' ELSE V.D21 END,
						V.D22 = CASE WHEN DAY(@FechaInicio) = 22 AND (V.D22 IS NULL OR V.D22 = 'DF') THEN 'A' ELSE V.D22 END,
						V.D23 = CASE WHEN DAY(@FechaInicio) = 23 AND (V.D23 IS NULL OR V.D23 = 'DF') THEN 'A' ELSE V.D23 END,
						V.D24 = CASE WHEN DAY(@FechaInicio) = 24 AND (V.D24 IS NULL OR V.D24 = 'DF') THEN 'A' ELSE V.D24 END,
						V.D25 = CASE WHEN DAY(@FechaInicio) = 25 AND (V.D25 IS NULL OR V.D25 = 'DF') THEN 'A' ELSE V.D25 END,
						V.D26 = CASE WHEN DAY(@FechaInicio) = 26 AND (V.D26 IS NULL OR V.D26 = 'DF') THEN 'A' ELSE V.D26 END,
						V.D27 = CASE WHEN DAY(@FechaInicio) = 27 AND (V.D27 IS NULL OR V.D27 = 'DF') THEN 'A' ELSE V.D27 END,
						V.D28 = CASE WHEN DAY(@FechaInicio) = 28 AND (V.D28 IS NULL OR V.D28 = 'DF') THEN 'A' ELSE V.D28 END,
						V.D29 = CASE WHEN DAY(@FechaInicio) = 29 AND (V.D29 IS NULL OR V.D29 = 'DF') THEN 'A' ELSE V.D29 END,
						V.D30 = CASE WHEN DAY(@FechaInicio) = 30 AND (V.D30 IS NULL OR V.D30 = 'DF') THEN 'A' ELSE V.D30 END,
						V.D31 = CASE WHEN DAY(@FechaInicio) = 31 AND (V.D31 IS NULL OR V.D31 = 'DF') THEN 'A' ELSE V.D31 END
					FROM ReportesApp_RRHH_AsistenciaView V
					WHERE V.Anio = YEAR(@FechaInicio) AND V.Mes = MONTH(@FechaInicio) AND V.CodPlanilla = 'CD' AND IDPersona = @NroPersona
				END
			END

			SET @FechaInicio = DATEADD(DAY,1,@FechaInicio)
		END
	END
	ELSE BEGIN
		SET @exito= '-2 = No se ha mapeado el registro de asistencia para este periodo. Favor de avisar al área de GTH.'
		ROLLBACK
		GOTO Terminar
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

--------------------------------------------------------------------
--------------------------------------------------------------------

DECLARE @TEMP_PLANILLAS TABLE(
	Nro INT,
	Planilla VARCHAR(50),
	Usuario VARCHAR(20)
)
DECLARE @Contador INT
DECLARE @Planilla2 VARCHAR(50)
DECLARE @Usuario2 VARCHAR(50)

SET @Contador = 1

INSERT INTO @TEMP_PLANILLAS
SELECT ROW_NUMBER() OVER(ORDER BY CodGasto ASC), CodGasto, 'DEMO' FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE EstadoLiquidacion = 0

WHILE(@Contador <= (SELECT COUNT(Nro) FROM @TEMP_PLANILLAS)) BEGIN
	SET @Planilla2 = (SELECT Planilla FROM @TEMP_PLANILLAS WHERE Nro = @Contador)
	SET @Usuario2 = (SELECT Usuario FROM @TEMP_PLANILLAS WHERE Nro = @Contador)

	EXEC ReportesApp_Operaciones_TicketGasto_BuscarReporteGasto @Planilla=@Planilla2, @Usuario=@Usuario2

	SET @Contador = @Contador + 1
END

--------------------------------------------------------------------
--------------------------------------------------------------------

--LISTA DE SEMIRREMOLQUES
SELECT CASE WHEN VH.Estado = 2 THEN 'OPERATIVO' ELSE 'INOPERATIVO' END AS 'ESTADO', VH.NumeroPlaca AS 'UNIDAD',
CASE WHEN VH.TipoVehiculo = 1 THEN 'TRACTO' ELSE 'SEMIRREMOLQUE' END AS 'TIPO', ST.DESCRIPCION AS 'TIPO_UNIDAD',
LTRIM(RTRIM(ME.Descripcion)) AS 'MARCA', VH.Modelo AS 'MODELO', VH.Neumaticos AS 'RUEDAS', VH.SerieMotor AS 'MOTOR', VH.NumeroChasis AS 'CHASIS',
CAST(ISNULL(VH.PesoSeco,0) AS DECIMAL(10,2)) AS 'PESO_NETO', CAST(ISNULL(VH.Capacidad,0) AS DECIMAL(10,2)) AS 'CARGA_UTIL',
CAST(ISNULL(VH.PesoBruto,0) AS DECIMAL(10,2)) AS 'PESO_BRUTO', CAST(ISNULL(VH.Ejes,0) AS INT) AS 'EJES'
FROM OP_TR_Vehiculo VH
LEFT JOIN GE_VARIOS AS TV WITH(NOLOCK) ON TV.SECUENCIAL = VH.TIPOVEHICULO AND TV.CODIGOTABLA = 'TIPOVEHICULO'
LEFT JOIN OP_TR_SUBTIPOVEHICULO AS ST WITH(NOLOCK) ON ST.SUBTIPOVEHICULO = VH.SUBTIPOVEHICULO
LEFT JOIN ME_MaquinaMarca ME ON VH.Marca = ME.Marca
WHERE VH.Estado = 2 AND VH.TipoVehiculo = 2

--------------------------------------------------------------------
--------------------------------------------------------------------

/*
--INSERTAR TIPO DE CAMBIO DIARIO
INSERT INTO TipoCambioMast (FechaCambio, MonedaCodigo, MonedaCambioCodigo, Factor, FactorCompra, FactorVenta, FactorPromedio, FactorCompraSBS, FactorVentaSBS, Estado, UltimaFechaModif, UltimoUsuario, FechaCambioString, TasaTamex, TasaTamn, TasaAnualTAMEX, TasaAnualTAMN, FactorCobranzaVenta)
VALUES (CONVERT(DATETIME, '20231127'), 'EX', 'LO', 0, 3.500000, 3.500000, 3.500000, 0.000000, 0.000000, 'A', GETDATE(), 'DEMO', '20231127', 0.000000, 0.000000, 0.0000, 0.0000, 0.000000)
*/

/*
DELETE FROM AP_GastoAdelanto WHERE UltimoUsuario = 'GREYES'
DELETE FROM AP_GastoAdelantoSustento WHERE UltimoUsuario = 'GREYES'
DELETE FROM Obligaciones WHERE IngresadoPor = 22529
DELETE FROM ObligacionesXCuenta WHERE NumeroDocumento = 'TRUJ-264417'
DELETE FROM AP_ObligacionFlujo WHERE UltimoUsuario = 'GREYES'
DELETE FROM OrdenPago WHERE NumeroDocumento = 'TRUJ-264417'
DELETE FROM XOrdenPago WHERE Usuario = 'GREYES'

UPDATE CorrelativosMast
SET CorrelativoNumero = 264416
WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='APAD')
UPDATE CorrelativosMast
SET CorrelativoNumero = 835076
WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='APNO')
*/