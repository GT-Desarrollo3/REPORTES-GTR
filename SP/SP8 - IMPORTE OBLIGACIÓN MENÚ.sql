SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 30-06-2023
-- Description:	INSERTAR OBLIGACIONES DE MENU
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_RRHH_Obligaciones_Menu_Insertar]
@xmlDetalle VARCHAR(MAX),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @idoc INT
DECLARE @NumeroDocumento VARCHAR(30)
DECLARE @Correlativo INT
DECLARE @TotalMonto DECIMAL(12,2)
DECLARE @TEMP_MENU TABLE(
		Proveedor VARCHAR(10),
		TipoDocumento CHAR(2),
		Codigo INT,
		NombreCompleto VARCHAR(250),
		TipoEmpleado CHAR(2),
		CuentaContable CHAR(20),
		CentroCosto CHAR(10),
		Sucursal CHAR(4),
		FlujodeCaja CHAR(4),
		DiferidoFlag CHAR(1),
		NoAfectoIGVFlag CHAR(1),
		NumeroDocumento CHAR(14),
		DescuentoTotal DECIMAL(12,2))

IF(@xmlDetalle IS NOT NULL) BEGIN
	EXEC sp_xml_preparedocument @idoc OUTPUT, @xmlDetalle
	INSERT INTO @TEMP_MENU(Proveedor,TipoDocumento,Codigo,NombreCompleto,TipoEmpleado,CuentaContable,CentroCosto,Sucursal,FlujodeCaja,DiferidoFlag,NoAfectoIGVFlag,NumeroDocumento,DescuentoTotal)
	SELECT * FROM OPENXML(@idoc,'/r/d',1)
	WITH (Proveedor VARCHAR(10), TipoDocumento CHAR(2), Codigo INT, NombreCompleto VARCHAR(250), TipoEmpleado CHAR(2), CuentaContable CHAR(20), CentroCostos CHAR(10),
		  Sucursal CHAR(4), FlujodeCaja CHAR(4), DiferidoFlag CHAR(1), NoAfectoIgvFlag CHAR(1), DNI CHAR(14), DescuentoTotal DECIMAL(12,2));
	EXEC sp_xml_removedocument @idoc;
END

SET @Exito = '0 = Obligación generada.'
SET @NumeroDocumento = UPPER(DATENAME(MONTH,GETDATE())) + '-' + SUBSTRING(CONVERT(VARCHAR(10), YEAR(GETDATE())),3,2)
SET @TotalMonto = (SELECT SUM(M.DescuentoTotal) FROM @TEMP_MENU M)
SET @Correlativo = (SELECT CorrelativoNumero FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='APNO'))

IF (@NumeroDocumento = (SELECT NumeroDocumento FROM Obligaciones WHERE NumeroDocumento = @NumeroDocumento)) BEGIN
	SET @Exito = '-1 = La obligación de este mes ya se encuentra registrada.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	UPDATE CorrelativosMast
	SET CorrelativoNumero = (SELECT CorrelativoNumero + 1 FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='APNO')),
		UltimaFechaModif = GETDATE()
	WHERE (CorrelativosMast.CompaniaCodigo ='999999') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='APNO') 
	
	INSERT INTO Obligaciones(Proveedor,TipoDocumento,NumeroDocumento,CuentaBancaria,CompaniaCodigo,UnidadNegocio,TipoPago,FechaDocumento,FechaRegistro,FechaVencimiento,FechaVencimientoOriginal,FechaRecepcion,GenerarPago,
	TipoServicio,MonedaDocumento,ConversionRequerida,MonedaPago,MontoObligacion,MontoImpuestoVentas,MontoNoAfecto,MontoImponible,MontoAdelantos,MontoImpuestos,NetoMonedaLocal,NetoMonedaExtranjera,MontoPagoParcial,
	TipoDeCambio,IngresadoPor,EstadoDocumento,ContabilizacionPendiente,FacturaAfectaSplitFlag,ChequeIndividual,RegistroNumero,FactorRValidacion,Comentarios,UnidadReplicacion,ProveedorPagarA,FlujodeCaja,CentroCosto,
	CentroCostoCP,ControlPresupuestalFlag,CargoFlag,MontoCreditoFiscal,AfectoIGVFlag,DiferidoFlag,UltimoUsuario,UltimaFechaModif,TransferenciaExcluidaFlag,PagoCajaChicaFlag,PagoDiferidoFlag,DetraccionCodigoFlag,Proyecto)
	VALUES(16029,'PL',@NumeroDocumento,'54-0100003345','100000','TRAN','AB',GETDATE(),GETDATE(),DATEADD(DAY,7,GETDATE()),DATEADD(DAY,7,GETDATE()),GETDATE(),'S','INAFEC','LO','D','LO',@TotalMonto,
	0.00,@TotalMonto,0.00,0.00,0.00,@TotalMonto,0.00,0.00,0.00,0,'RV','S','N','N',@Correlativo,'N','Adelanto de remuneración (Menú del 23 de '+DATENAME(MONTH,DATEADD(MONTH,-1,GETDATE()))+' al 22 de '+DATENAME(MONTH,GETDATE())+')',
	'TRUJ',16029,'021','060105','060105','N','N',0.00,'N','N',@Usuario,GETDATE(),'N','N','N','N','1063100026')
	
	INSERT INTO ObligacionesXCuenta(TipoDocumento,NumeroDocumento,Linea,Monto,Proveedor,Persona,CuentaContable,Proyecto,DiferidoFlag,Sucursal,NoAfectoIGVFlag,CentroCosto,FlujodeCaja)
	SELECT M.TipoDocumento,@NumeroDocumento,ROW_NUMBER() OVER(ORDER BY Codigo ASC),M.DescuentoTotal,M.Proveedor,M.Codigo,M.CuentaContable,'1063100026',M.DiferidoFlag,M.Sucursal,M.NoAfectoIGVFlag,M.CentroCosto,M.FlujodeCaja
	FROM @TEMP_MENU M
	
	INSERT INTO AP_ObligacionFlujo(Proveedor,TipoDocumento,NumeroDocumento,Secuencia,AreaDocumentaria,Comentario,Estado,UltimoUsuario,UltimaFechaModif)
	VALUES (16029, 'PL', @NumeroDocumento, 1,'001','Recepción Inicial','A',@Usuario,GETDATE())
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

/*
exec ReportesApp_RRHH_Obligaciones_Menu_Insertar @xmlDetalle=N'<r>  <d N_x00B0_="55" Obligacion="16029" Proveedor="16029" TipoDocumento="PL" Codigo="20636" NombreCompleto="PIZAN MALDONADO ERIKA LOIDI" TipoEmpleado="EM" CuentaContable="1412001" CentroCostos="060105" Sucursal="BTRU" FlujodeCaja="021" DiferidoFlag="N" NoAfectoIgvFlag="N" DNI="47366767" DescuentoTotal="135" />  <d N_x00B0_="43" Obligacion="16029" Proveedor="16029" TipoDocumento="PL" Codigo="21910" NombreCompleto="POLO VERA JHAN CARLOS" TipoEmpleado="OB" CuentaContable="1412002" CentroCostos="060105" Sucursal="BTRU" FlujodeCaja="021" DiferidoFlag="N" NoAfectoIgvFlag="N" DNI="71492301" DescuentoTotal="150" />  <d N_x00B0_="52" Obligacion="16029" Proveedor="16029" TipoDocumento="PL" Codigo="82" NombreCompleto="QUIROZ SOSA ELISER" TipoEmpleado="OB" CuentaContable="1412002" CentroCostos="060105" Sucursal="BTRU" FlujodeCaja="021" DiferidoFlag="N" NoAfectoIgvFlag="N" DNI="42950016" DescuentoTotal="142.5" />  <d N_x00B0_="21" Obligacion="16029" Proveedor="16029" TipoDocumento="PL" Codigo="21810" NombreCompleto="RAMOS SALVADOR KATHERIN SOLEDAD" TipoEmpleado="EM" CuentaContable="1412001" CentroCostos="060105" Sucursal="BTRU" FlujodeCaja="021" DiferidoFlag="N" NoAfectoIgvFlag="N" DNI="75023432" DescuentoTotal="30" />  <d N_x00B0_="53" Obligacion="16029" Proveedor="16029" TipoDocumento="PL" Codigo="22529" NombreCompleto="REYES HORNA GERARDO MANUEL" TipoEmpleado="EM" CuentaContable="1412001" CentroCostos="060105" Sucursal="BTRU" FlujodeCaja="021" DiferidoFlag="N" NoAfectoIgvFlag="N" DNI="74696561" DescuentoTotal="150" /></r>',
@Usuario=N'GREYES'
*/

/* OBLIGACIONES CABECERA */
SELECT * FROM Obligaciones WHERE NumeroDocumento = 'AGOSTO-23'
/* OBLIGACIONES DETALLE */
SELECT * FROM ObligacionesXCuenta WHERE NumeroDocumento = 'AGOSTO-23'
/* OBLIGACIONES DETALLE */
SELECT * FROM AP_ObligacionFlujo WHERE NumeroDocumento = 'AGOSTO-23'


DELETE FROM Obligaciones WHERE NumeroDocumento = 'AGOSTO-23'
DELETE FROM ObligacionesXCuenta WHERE NumeroDocumento = 'AGOSTO-23'
DELETE FROM AP_ObligacionFlujo WHERE NumeroDocumento = 'AGOSTO-23'


/* LISTA DE OBLIGACIONES */
SELECT Obligaciones.CompaniaCodigo, Obligaciones.Proveedor, Obligaciones.NumeroDocumento, Obligaciones.FechaDocumento, Obligaciones.MonedaDocumento, Obligaciones.EstadoDocumento, Obligaciones.FechaVencimiento, Obligaciones.TipoDocumento, Obligaciones.ResponsableCodigo, Obligaciones.FechaPago, PersonaMast.Busqueda, Obligaciones.RegistroNumero, Obligaciones.MontoObligacion, Obligaciones.MontoAdelantos, Obligaciones.NumeroDocumentoInterno, Obligaciones.CentroCosto, Obligaciones.IngresadoPor, Obligaciones.Voucher, Obligaciones.ControlPresupuestalFlag, Obligaciones.TipoPago, Obligaciones.FechaRegistro, SY_CampoCalculado.Flag01, Obligaciones.UnidadNegocio, Obligaciones.PagoDiferidoFlag,
Obligaciones.Proyecto FROM Obligaciones
LEFT JOIN PersonaMast ON
( Obligaciones.Proveedor = PersonaMast.Persona ),
SY_CampoCalculado WHERE ( SY_CampoCalculado.RegistroNumero = 1 ) AND
( Obligaciones.NumeroDocumento >= '' )
AND CompaniaCodigo = '100000' AND EstadoDocumento = 'RV' AND Obligaciones.FechaRegistro >= '30/05/2023 00:00:00' AND Obligaciones.FechaRegistro <= '30/05/2023 23:59:59' ORDER BY Obligaciones.CompaniaCodigo, PersonaMast.Busqueda, Obligaciones.NumeroDocumento 




/*
SELECT Obligaciones.Proveedor, Obligaciones.TipoDocumento, Obligaciones.NumeroDocumento, Obligaciones.CuentaBancaria, Obligaciones.CompaniaCodigo, 
Obligaciones.UnidadNegocio, Obligaciones.ResponsableCodigo, Obligaciones.TipoPago, Obligaciones.FechaDocumento, Obligaciones.FechaRegistro, 
Obligaciones.FechaVencimiento, Obligaciones.FechaVencimientoOriginal, Obligaciones.FechaRecepcion, Obligaciones.FechaPago, Obligaciones.GenerarPago, 
Obligaciones.TipoServicio, Obligaciones.MonedaDocumento, Obligaciones.ConversionRequerida, Obligaciones.MonedaPago, 
Obligaciones.ReferenciaTipoDocumento, Obligaciones.ReferenciaNumeroDocumento, Obligaciones.ObligacionRelacionadaTipo, 
Obligaciones.ObligacionRelacionadaNumero, Obligaciones.MontoObligacion, Obligaciones.MontoImpuestoVentas, Obligaciones.MontoNoAfecto, Obligaciones.MontoImponible, Obligaciones.MontoAdelantos, Obligaciones.MontoImpuestos, Obligaciones.NetoMonedaLocal, Obligaciones.NetoMonedaExtranjera, Obligaciones.MontoPagoParcial, Obligaciones.TipoDeCambio, Obligaciones.AprobadoPor, Obligaciones.AprobadoCP1, Obligaciones.AprobadoCP2, Obligaciones.IngresadoPor, Obligaciones.RevisadoPor, Obligaciones.RetenidoPor, Obligaciones.EstadoDocumento, 
Obligaciones.ContabilizacionPendiente, Obligaciones.FacturaAfectaSplitFlag, Obligaciones.ChequeIndividual, Obligaciones.Voucher, Obligaciones.VoucherAnulacion, Obligaciones.FechaVoucher, Obligaciones.NumeroPago, Obligaciones.NumeroProceso, Obligaciones.ProcesoSecuencia, Obligaciones.RegistroNumero, Obligaciones.CanjeRegistroNumero, Obligaciones.FactorRValidacion, Obligaciones.Comentarios, Obligaciones.ComentariosAdicional, Obligaciones.RazonRechazo, Obligaciones.UnidadReplicacion, Obligaciones.ProveedorPagarA, Obligaciones.PartidaPresupuestal, Obligaciones.FlujodeCaja, Obligaciones.CentroCosto, Obligaciones.CentroCostoCP, Obligaciones.FechaAprobacion, Obligaciones.ControlPresupuestalFlag, Obligaciones.NumeroDocumentoInterno, Obligaciones.CargoFlag, Obligaciones.MontoCreditoFiscal, Obligaciones.TipodeCambioProvision, Obligaciones.AfectoIGVFlag, Obligaciones.PagaraNombre, Obligaciones.DiferidoFlag, Obligaciones.UltimoUsuario, Obligaciones.UltimaFechaModif, Obligaciones.AdelantoFlag, Obligaciones.TransferenciaExcluidaFlag, 'S', Obligaciones.ReferenciaCodigoInterno, Obligaciones.PagoCajaChicaFlag, Obligaciones.PagoDiferidoFlag, Obligaciones.ArchivoAdjunto, Obligaciones.DetraccionCodigoFlag, Obligaciones.DetraccionCodigo, Obligaciones.DefaultCampoReferencia, Obligaciones.DetraccionDocumento, '               ' As numerodosificacion, '               ' As numeroorden, Obligaciones.Observaciones, Obligaciones.DetraccionMontoReferencial, Obligaciones.Proyecto,
Obligaciones.TipodeCambio_Cre FROM Obligaciones WHERE ( Obligaciones.Proveedor = 16029 ) AND ( Obligaciones.TipoDocumento = 'PL' ) AND ( Obligaciones.NumeroDocumento = 'MAYO-23' ) 

select  * from ObligacionesXCuenta  WHERE NumeroDocumento = 'MAYO-23' AND Proveedor = 16029

INSERT INTO ObligacionesXCuenta (Proveedor,TipoDocumento,NumeroDocumento,Linea,Monto,CuentaContable,CentroCosto,Persona,FlujodeCaja,DiferidoFlag,Sucursal,NoAfectoIGVFlag)
SELECT 16029,'PL','MAYO-23',ROW_NUMBER() OVER(ORDER BY Proveedor ASC),[TOTAL DESCUENTO],RTRIM(CuentaContable),RTRIM(CentroCostos),CONVERT(INT,CODIGO),FlujodeCaja,DiferidoFlag,RTRIM(Sucursal),NoAfectoIGVFlag from [dbo].[Importar$] where Codigo is not null

UPDATE ObligacionesXCuenta SET Proyecto = '1063100026'WHERE NumeroDocumento = 'MAYO-23' AND Proveedor = 16029

SELECT * FROM Importar$

--REGISTRO NUMERO CORRELATIVO
SELECT * FROM CorrelativosMast WHERE ( CorrelativosMast.CompaniaCodigo ='999999' ) AND ( CorrelativosMast.TipoComprobante ='SY' ) AND ( CorrelativosMast.Serie ='APNO' )
SELECT ParametrosMast.Texto FROM ParametrosMast WHERE ( ParametrosMast.CompaniaCodigo ='999999' ) AND ( ParametrosMast.AplicacionCodigo ='AP' ) AND ( ParametrosMast.ParametroClave ='INVOREGNO' )  
--TOMAR EL ÚLTIMO MÁS 1
*/