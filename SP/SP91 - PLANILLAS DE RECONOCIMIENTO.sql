
-- CREAR TABLA ReportesApp_Operaciones_TicketGasto_GastosReconocimiento Y LLENARLA

-----------------------------------------------------------------------------------

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
@Usuario VARCHAR(20),
@CodGasto VARCHAR(30)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @Monto DECIMAL(10,8)
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
SET @Motivo = 'VIATICO ADICIONAL: PL - ' + @CodGasto

SET @GastoTotal = (SELECT GastoDiferencialV FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE CodGasto = @CodGasto AND EstadoLiquidacion = 0 AND GastoDiferencialV > 0)
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
		
	SET @NroConductor = (SELECT P.Persona FROM ReportesApp_Operaciones_TicketGasto_Registro R
						LEFT JOIN OP_TR_Conductor C ON C.IdConductor = R.ViConductor
						LEFT JOIN PersonaMast P ON P.Persona = C.IdPersona WHERE R.CodGasto = @CodGasto)
	SET @NombreConductor = (SELECT P.NombreCompleto FROM ReportesApp_Operaciones_TicketGasto_Registro R
							LEFT JOIN OP_TR_Conductor C ON C.IdConductor = R.ViConductor
							LEFT JOIN PersonaMast P ON P.Persona = C.IdPersona WHERE R.CodGasto = @CodGasto)
	SET @NroUsuario = (SELECT TOP(1) P.Persona FROM Usuario U LEFT JOIN PersonaMast P ON P.NombreCompleto = U.Nombre WHERE Usuario = @Usuario AND P.Estado = 'A' ORDER BY P.Persona DESC)
	
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
	WHERE CodGasto = @CodGasto
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

----------------------------------------------------------------------

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
@CodGasto VARCHAR(30)
AS
BEGIN
	SET NOCOUNT ON;
    SELECT TOP(1) TG.CodGasto, LTRIM(RTRIM(C.Nombre)) AS 'CONDUCTOR', O.Descripcion AS 'PROGRAMACION', V.NumeroPlaca AS 'TRACTO',
	R.NumeroPlaca AS 'SEMIRREMOLQUE', RT.Descripcion AS 'RUTA', LTRIM(RTRIM(CL.Busqueda)) AS 'CLIENTE', F.FechaProgramacion AS 'FECHA_VIAJE',
	GR.TotalDias AS 'DIAS', T.Descripcion AS 'TIPO_GASTO', GD.Gasto AS 'GASTO', TG.TotalEntregado AS 'GASTO_TOTAL',
	ISNULL(TG.GastoDiferencial,0) AS 'GASTO_DIFERENCIAL', ISNULL(TG.GastoDiferencialV,0) AS 'VIATICOS', TG.Observacion AS 'OBSERVACION',
	CONVERT(VARCHAR,TG.FechaCreacion,103)+' '+CONVERT(VARCHAR,TG.FechaCreacion,8) AS 'FECHA_EMISION'
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
	LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = F.TipoProgramacion
    WHERE TG.CodGasto = @CodGasto AND TG.EstadoLiquidacion = 0
	ORDER BY TG.FechaCreacion DESC
END

----------------------------------------------------------------------

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
@NroTicket INT,
@CodGasto VARCHAR(30)
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

	SET @GastoAnterior = (SELECT TotalEntregado FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE CodGasto = @CodGasto)
	SET @GastoActual = (SELECT Viaticos FROM ReportesApp_Operacion_Previaje_Registros WHERE NroTicket = @NroTicket)
	SET @GastoDiferencial = @GastoActual - @GastoAnterior

	IF(@NuevoGR = 0) BEGIN
		UPDATE ReportesApp_Operaciones_TicketGasto_Registro
		SET /*GastoDiferencial = @GastoDiferencial, TotalEntregado = TotalEntregado + @GastoDiferencial,*/ idGastoXRutaC = ISNULL(@NuevoGR,69)
		WHERE CodGasto = @CodGasto
	END
	ELSE BEGIN
		UPDATE ReportesApp_Operaciones_TicketGasto_Registro
		SET idGastoXRutaC = @NuevoGR
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

----------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 05/11/2025
-- Description:	LISTAR PREVIAJE DE RECONOCIMIENTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ListarPreviajeR]
@NroTicket INT
AS
BEGIN
	SELECT R.IdConductor, RTRIM(C1.Nombre) AS 'CONDUCTOR_ORIGINAL', R.IdConductorApoyo, RTRIM(C2.Nombre) AS 'CONDUCTOR_APOYO',
	R.IdRuta, ISNULL(GR.Total,0.00) AS 'GASTO'
	FROM ReportesApp_Operacion_Previaje_Registros R
	LEFT JOIN OP_TR_Conductor C1 ON C1.IdConductor = R.IdConductor
	LEFT JOIN OP_TR_Conductor C2 ON C2.IdConductor = R.IdConductorApoyo
	LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastosReconocimiento GR ON GR.idRuta = R.IdRuta
	WHERE R.NroTicket = @NroTicket
END

----------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 06-11-2025
-- Description:	REGISTRAR PLANILLA - RECONOCIMIENTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_RegistrarPlanillaR]
@NroTicket INT,
@Planilla VARCHAR(20),
@idConductorR INT,
@GastoRuta DECIMAL(10,2),
@Usuario VARCHAR(50)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT
DECLARE @correlativo2 INT
DECLARE @Persona INT
DECLARE @NombreCompleto VARCHAR(350)

SET @Exito = '0 = Planilla Registrada.'
SET @Persona = (SELECT P.Persona FROM OP_TR_Conductor C LEFT JOIN PersonaMast P ON P.Persona = C.IdPersona WHERE C.IdConductor = @idConductorR)
SET @NombreCompleto = (SELECT LTRIM(RTRIM(P.NombreCompleto)) FROM OP_TR_Conductor C LEFT JOIN PersonaMast P ON P.Persona = C.IdPersona WHERE C.IdConductor = @idConductorR)

SET @correlativo = (SELECT MAX(idTicketGasto) FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE Anio = YEAR(GETDATE()))
SET @correlativo = ISNULL(@correlativo,0) + 1

BEGIN TRAN
BEGIN TRY
	INSERT INTO ReportesApp_Operaciones_TicketGasto_Registro(idTicketGasto, Anio, CodGasto, ViConductor, IdOperacion, NroTicket, idGastoXRutaC, TotalEntregado, FechaCreacion,
	UsuarioCreacion,EstadoLiquidacion,Permiso,Observacion)
	SELECT @correlativo, YEAR(GETDATE()), SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo)),6)),
	@idConductorR, IdOperacion, @NroTicket, idGastoXRutaC, @GastoRuta, GETDATE(), @Usuario, 0, 0, 'PLANILLA RECONOCIMIENTO'
	FROM ReportesApp_Operaciones_TicketGasto_Registro
	WHERE CodGasto = @Planilla
	
	SET @correlativo2 = (SELECT MAX(idTicketDespacho) FROM ReportesApp_Operaciones_Previajes_TicketDespacho)
	SET @correlativo2 = ISNULL(@correlativo2,0) + 1

	INSERT INTO ReportesApp_Operaciones_Previajes_TicketDespacho(idTicketDespacho,CodigoPreviaje,Fecha,UsuarioCrea,FechaCrea)
	VALUES(@correlativo2,CONVERT(INT,SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo)),6))),
	GETDATE(),@Usuario,GETDATE())

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
