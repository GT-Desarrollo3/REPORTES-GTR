
-- AGREGAR TIPO DE COMBUSTIBLE A ReportesApp_Combustible_TicketTercero_CostoProveedor

---------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Angel J. Guerra
-- Create date: 05.Agosto.2022
-- Description:	Registrar PRECIOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Logistica_PreciosTerceros_Registrar]
@Opcion INT,
@IdFecha DATETIME, 
@IdCliente INT,
@Precio DECIMAL(16,6),
@Lugar varchar(50),
@Producto VARCHAR(50),
@Usuario VARCHAR(20)
AS
DECLARE @exito VARCHAR(MAX)
DECLARE @Proveedor VARCHAR(200)

SET @exito = '0= Registro Exitoso!'

BEGIN TRAN
BEGIN TRY
	SET @Proveedor = (SELECT p.BUSQUEDA FROM PersonaMast P WHERE P.Persona = @IdCliente)

	IF @Opcion = 1 BEGIN
		IF EXISTS(SELECT * FROM ReportesApp_Combustible_TicketTercero_CostoProveedor 
						   WHERE IDFECHA = @IdFecha and IdCliente =@IdCliente and lugar = @Lugar AND Producto = @Producto) BEGIN
			SET @exito = '-1 = El precio de este producto ya esta registrado'
			ROLLBACK
			GOTO Terminar
		END

		/*IF NOT EXISTS(SELECT * FROM ReportesApp_Combustible_TicketTercero_CostoProveedor WHERE IDFECHA = @IdFecha-1 and IdCliente =@IdCliente and lugar = @Lugar)
		BEGIN
			SET @exito = '-1= No puedes registrar el precio porqe hay fechas pasadas sin precio.'
			ROLLBACK
			GOTO Terminar
		END*/
		
		INSERT INTO ReportesApp_Combustible_TicketTercero_CostoProveedor (IDFECHA,IdCliente,PrecioUnitario,lugar,Producto,UsuarioCrea,FHCrea)
		VALUES(@IdFecha,@IdCliente,@Precio,@Lugar,@Producto,@Usuario,GETDATE())
		SET @exito = '0 = Registro Exitoso!'	
	END
	
	IF @Opcion = 2 BEGIN
		IF EXISTS(SELECT Precio FROM ReportesApp_Combustible_TicketsTercero WHERE CAST(FechaDespacho AS DATE) = CAST(@IdFecha AS DATE)
				  AND Empresa = @Proveedor AND Lugar = @Lugar AND Producto = @Producto AND Anulado != 1) BEGIN
			SET @exito = '-2 = No puede modificar este producto en esta fecha porque ya tiene un registro en el KARDEX.'
			ROLLBACK
			GOTO Terminar
		END
		ELSE BEGIN	
			UPDATE ReportesApp_Combustible_TicketTercero_CostoProveedor
			SET PrecioUnitario = @Precio, lugar = @Lugar, Producto = @Producto, UsuarioCrea = @Usuario, FHModifica = GETDATE()
			WHERE IdFecha =  @IdFecha AND IdCliente = @IdCliente
		
			SET @exito = '0 = Actualizacion Exitosa!'
		END			
	END

	IF @Opcion = 3 BEGIN
		IF EXISTS(SELECT Precio FROM ReportesApp_Combustible_TicketsTercero WHERE CAST(FechaDespacho AS DATE) = CAST(@IdFecha AS DATE)
				  AND Empresa = @Proveedor AND Lugar = @Lugar AND Producto = @Producto AND Anulado != 1) BEGIN
			SET @exito = '-3 = No puede eliminar este producto en esta fecha porque ya tiene un registro en el KARDEX.'
			ROLLBACK
			GOTO Terminar
		END
		ELSE BEGIN		
			DELETE FROM ReportesApp_Combustible_TicketTercero_CostoProveedor
			WHERE IdFecha =  @IdFecha AND IdCliente = @IdCliente AND Producto = @Producto
		
			SET @exito = '0= Eliminado Correctamente!'	
		END		
	END
	
	IF HOST_NAME() IN ('DESKTOP-FNO4LB1', 'JTORIBIO') BEGIN
		SET @exito = '777= TEST OK'
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

TERMINAR:
IF LEFT(@Exito,1)='-' OR cast(left(@Exito,1) AS INT)>0 BEGIN
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
-- Author:		Ing. Jordyn Toribio Esquivel
-- Create date: 15.Junio.2022
-- Description:	Importado de tickets al Kardex
-- =============================================
/*
exec ReportesApp_Combustible_TicketsTercero_Registrar
*/
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_DespachosTerceros_Registrar]
@Codigo INT, 
@Placa VARCHAR(20), 
@FechaDespacho DATETIME, 
@Hora TIME,
@Cantidad NUMERIC(12,3), 
@Precio NUMERIC(12,3), 
@Producto VARCHAR(80), 
@lugar VARCHAR(100),
@IdEmpresa INT,
@Empresa VARCHAR(70),
@Dni VARCHAR(08), 
@Chofer VARCHAR(50), 
@Kilometraje NUMERIC(12,2), 
@NroTicketPreviaje int,
@UsuarioCrea VARCHAR(20),
@Totalizador decimal (12,2)
AS
DECLARE @exito varchar(max)
SET @exito = '0= Registro Exitoso!'

BEGIN TRAN
	BEGIN TRY
		--verificar si hay precio del dia
		IF NOT EXISTS(SELECT * FROM ReportesApp_Combustible_TicketTercero_CostoProveedor WHERE CAST(IDFECHA AS DATE) = CAST(@FechaDespacho AS DATE)
		AND IdCliente = @IdEmpresa AND Lugar = @lugar AND Producto = @Producto) AND  @IdEmpresa NOT IN (1553)
		BEGIN
			SET @exito = '-11 = No hay precio para este producto en esta fecha, favor de avisar a Logistica.'
			ROLLBACK
			GOTO Terminar
		END
		
		IF EXISTS (SELECT * FROM AP_Documentos WHERE DocumentoReferencia = CAST(@Codigo AS VARCHAR) AND LTRIM(RTRIM(Proveedor)) = @IdEmpresa AND DocumentoClasificacion = 'ROC')
		BEGIN
			SET @exito = '-10= El documento '+ @Codigo + ' ya se encuentra registrado.' 
			ROLLBACK
			GOTO Terminar
		END
		
	    --Verifica Periodo que no este cerrado
	    -- -- Estado: N (cerrado), S (Abierto)
		IF EXISTS(	SELECT * FROM SY_PeriodoControl WITH(NOLOCK)
					WHERE SY_PeriodoControl.AplicacionCodigo = 'WH' 
						AND CompaniaSocio = '10000000'
						AND Periodo = CONVERT(VARCHAR(06), @FechaDespacho, 112)
						AND Estado = 'N'
				 )
		BEGIN
			SET @exito = '-1= No procede registro. Periodo ya esta cerrado.'
			ROLLBACK
			GOTO Terminar
		END
		
			
		SET @Placa = REPLACE(REPLACE(ISNULL(@Placa, ''), '-',''),'.','')	
		
		SET @FechaDespacho = CONVERT( VARCHAR(10),CAST(@FechaDespacho AS DATE)) + 'T' +  CONVERT( VARCHAR(12), CAST(@Hora AS TIME))		
		
		DECLARE @Table_Transaccion_Combustible TABLE (	AlmacenCodigo VARCHAR(15), ItemCodigo VARCHAR(15), Condicion VARCHAR(01), Lote VARCHAR(02), 
														FechaDespacho DATETIME, TransaccionCodigo varchar(10), CompaniaSocio varchar(15), 
														TipoDocumento varchar(10), NumeroDocumento VARCHAR(50), CantidadDespachada NUMERIC(12,6), 
														PrecioCombustible NUMERIC(12,6), TipoCambio NUMERIC(12,3), 
														CentroCostoConsumo VARCHAR(15), Proyecto VARCHAR(15), ReferenciaTipoDocumento VARCHAR(02), 
														ReferenciaNumeroDocumento VARCHAR(25), 
														PlacaVehiculo VARCHAR(15), SolicitadoPor INT, AprobadoPor INT, RecibidoPor VARCHAR(200), 
														Sucursal VARCHAR(04), UnidadNegocio VARCHAR(04), UnidadCodigo VARCHAR(04),
														CostoReal NUMERIC(12,6), Periodo VARCHAR(06), Usuario VARCHAR(20), Ticket INT, NroTicketPreViaje INT)

																
		DECLARE @TIPOCAMBIO REAL
		DECLARE @NUMERODOCUMENTO VARCHAR(50)
		DECLARE @NUMERODOCUMENTOINGRESO VARCHAR(50)
		DECLARE @NroTicket INT = 0
		
		IF @IdEmpresa IN (1553)
		BEGIN			
			--OBTENEMOS EL CORRELATIVO DE L PROPIO CON SERIE DE LIMA ()	
			DECLARE @NROTICKETLIMA INT	= 0		
			SET @NROTICKETLIMA =(SELECT  TOP 1 ISNULL(Ticket,0) FROM ReportesApp_Combustible_TicketsSurtidor WHERE Ticket like '50000%' order by Ticket desc)
			SET  @NROTICKETLIMA = ISNULL(@NROTICKETLIMA,0) +1
			set @NroTicket =(SELECT '5'+ RIGHT('00000000' + CONVERT(varchar(10), CAST(@NROTICKETLIMA AS VARCHAR(20))), 8))
			
		END
		ELSE
		BEGIN
			-- Obtenemos de siguiente correlativo de ticket tercero
			SET @NroTicket = (	
								SELECT TOP 1 Ticket
								FROM ReportesApp_Combustible_TicketsTercero WITH(NOLOCK)
								ORDER BY Ticket DESC
							)
			SET @NroTicket = ISNULL(@NroTicket, 0) + 1
		
		END
		
		IF NOT EXISTS(SELECT TOP 1 * FROM OP_TR_Vehiculo WHERE REPLACE(REPLACE(numeroplaca, '-',''),'.','') = REPLACE(REPLACE(@Placa, '-',''),'.',''))
		BEGIN
			SET @exito = '-2= No se encuentra placa.'
			ROLLBACK
			GOTO Terminar
		END					
		
		--VALIDAMOS QUE EL TICKET NO SE DUPLIQUE
		IF @IdEmpresa IN (1553)
		BEGIN
			IF EXISTS(SELECT TOP 1 * 
							FROM ReportesApp_Combustible_TicketsSurtidor 
							WHERE NroTicketPreviaje = @NroTicketPreviaje and Lugar='LIMA' AND LEFT(Ticket,1)='5')						
							
			BEGIN
				SET @exito = '-3= Ya existe ticket lima con este código. No procede, favor de verificar.'
				ROLLBACK
				GOTO Terminar
			END		
		END
		ELSE
		BEGIN		
			IF EXISTS(SELECT TOP 1 * 
							FROM ReportesApp_Combustible_TicketsTercero 
							WHERE REPLACE(REPLACE(placa, '-',''),'.','') = REPLACE(REPLACE(@Placa, '-',''),'.','')
								AND Codigo = @Codigo
								AND ISNULL(Anulado, 0) = 0						
							)
			BEGIN
				SET @exito = '-4= Ya existe ticket tercero con este código. No procede, favor de verificar.'
				ROLLBACK
				GOTO Terminar
			END		
		END
		
		--validamos que el conductor exista
		IF NOT EXISTS(	SELECT IdConductor AS 'ID', RTRIM(Nombre) AS 'RELACION', Documento AS 'DOCUMENTO' 
						FROM OP_TR_Conductor C WITH(NOLOCK)
						WHERE C.Estado='A' AND RTRIM(C.Nombre) = @Chofer
					)
		BEGIN
			SET @exito = '-5= No se encontró conductor ' + @Chofer
			ROLLBACK
			GOTO Terminar
		END	
		
		--OBRENEMOS EL PRECIO DEL COMBUSTIBLE
		DECLARE @PRECIOITEM DECIMAL (16,6)
		DECLARE @CONTOMETROAUTO FLOAT
		IF @IdEmpresa IN (1553)
		BEGIN				 		
			SET @PRECIOITEM = (Select TOP 1  PrecioUnitario from ReportesApp_Combustible_TicketTercero_CostoProveedor 
									where  IdCliente = 1553 AND Producto = @Producto ORDER BY IdFecha DESC/*SELECT TOP 1  PrecioUnitario 
								FROM X_WH_Item_UltimoPrecio      
								WHERE Item = '1702001001'  and    MonedaCodigo = 'LO'
								ORDER BY UltimaFechaModif DESC */)
								
			SET @CONTOMETROAUTO =(select MAX(isnull(ContometroUsuario,0)) from 	ReportesApp_Combustible_TicketsSurtidor )	
		END
		ELSE
		BEGIN					
			SET @PRECIOITEM = (SELECT TOP 1 PrecioUnitario FROM ReportesApp_Combustible_TicketTercero_CostoProveedor 
							   WHERE CAST(IDFECHA AS DATE) = CAST(@FechaDespacho AS DATE)  AND IdCliente = @IdEmpresa AND LUGAR = @lugar AND Producto = @Producto)	
		END	
		
		--OBTEEMOS EL ID DEL USUARIO
		DECLARE @IDUSUARIO INT
		SET @IDUSUARIO = (SELECT Empleado FROM EmpleadoMast WHERE CodigoUsuario = @UsuarioCrea)
		
		--OBTENEMOS EL USUARIO PARA GENERAR LA ORDEN DE COMPRA
		DECLARE @IDUSUARIOCOMPRAS INT
		SET @IDUSUARIOCOMPRAS = (SELECT Empleado FROM EmpleadoMast WHERE CodigoUsuario = @UsuarioCrea)
		
		--Obtenemos el tipo de cambio
		SET @TIPOCAMBIO = (	Select FactorVenta 
							from TipoCambioMast WITH(NOLOCK) 
							Where FechaCambioString = CONVERT(VARCHAR(9),@FechaDespacho,112)
						   )

		--Obtenemos el siguiente numero de documento
		SET @NUMERODOCUMENTO = (	Select ISNULL(CorrelativoNumero, 0) +1 
									from CorrelativosMast WITH(NOLOCK) 
									Where CompaniaCodigo='100000' 
										AND TipoComprobante='WH' 
										AND Serie='NS' 
							)
			
		--Incrementa correlativo
		UPDATE CorrelativosMast SET CorrelativoNumero = (IsNull(CM.CorrelativoNumero,0) + 1) 
		FROM CorrelativosMast CM
		WHERE CM.CompaniaCodigo = '100000' 
			AND CM.TipoComprobante = 'WH' 
			AND CM.Serie = 'NS'
			
		--Obtenemos el siguiente numero de documento
		SET @NUMERODOCUMENTOINGRESO = (	Select (RIGHT('000000' + LTRIM(RTRIM(ISNULL(CorrelativoNumero, 0) +1)),6)) 
									from CorrelativosMast WITH(NOLOCK) 
									Where CompaniaCodigo='100000' 
										AND TipoComprobante='WH' 
										AND Serie='NI' 
							)
		
		--Incrementa correlativo
		UPDATE CorrelativosMast SET CorrelativoNumero = (IsNull(CM.CorrelativoNumero,0) + 1) 
		FROM CorrelativosMast CM
		WHERE CM.CompaniaCodigo = '100000' 
			AND CM.TipoComprobante = 'WH' 
			AND CM.Serie = 'NI'		
		
		IF @IdEmpresa IN (1553)
		BEGIN 
			
			SET @Precio =@PRECIOITEM
			
			INSERT INTO ReportesApp_Combustible_TicketsSurtidor (Ticket, NroTicketPreViaje, Placa, Fecha,Hora, Cantidad, Precio, Producto, Empresa,
																DniGeoTab, ChoferAsignadoGeoTab, KilometrajeGeotab,  EsAnexadoTicketPreViajeKardex,NotaSalida,Surtidor,Pistola,lugar,
																Totalizador,ContometroUsuario)
														VALUES (@NroTicket, @Codigo, @Placa, @FechaDespacho,CAST(@Hora AS DATETIME), @Cantidad, @Precio, @Producto, @Empresa,
																@Dni, @Chofer, @Kilometraje,1,@NUMERODOCUMENTO,1,1,@lugar,(@CONTOMETROAUTO + @Cantidad),@Totalizador)		
				
			--SELECT 'AS'											
			-- Insertamos Nexo Tickets_Carga Combustible_Kardex 
			INSERT INTO ReportesApp_Combustible_Nexo_Ticket_CargaCombustible_Kardex 
				(Ticket, NroTicketPreViaje, FechaTicket, Placa, Compania, NumeroDocumento, Tipo)
			VALUES (@NroTicket, @Codigo, @FechaDespacho, @Placa, '10000000',  @NUMERODOCUMENTO, 'P')	
				
		END
		ELSE
		BEGIN
			SET @Precio =@PRECIOITEM

			INSERT INTO ReportesApp_Combustible_TicketsTercero (Ticket, Codigo, Placa, FechaDespacho, Cantidad, Precio, Producto, Empresa,
																DniGeoTab, ChoferAsignadoGeoTab, Kilometraje, UsuarioCrea, FHRegistra, EsAnexadoCodigoKardex, 
																NotaSalida, Hora,Lugar)
														VALUES (RIGHT('000000000' + LTRIM(RTRIM(@NroTicket)),9), @Codigo, @Placa, @FechaDespacho, @Cantidad, @Precio, @Producto, @Empresa,
																@Dni, @Chofer, @Kilometraje, @UsuarioCrea, GETDATE(), 1, 
																@NUMERODOCUMENTO, @Hora,@lugar)		

			-- Insertamos Nexo Tickets_Carga Combustible_Kardex 
			INSERT INTO ReportesApp_Combustible_Nexo_Ticket_CargaCombustible_Kardex 
				(Ticket, NroTicketPreViaje, FechaTicket, Placa, Compania, NumeroDocumento, Tipo)
			VALUES (RIGHT('000000000' + LTRIM(RTRIM(@NroTicket)),9), @Codigo, @FechaDespacho, @Placa, '10000000',  @NUMERODOCUMENTO, 'T')		
		END
		
		DECLARE @ITEMMAESTRO VARCHAR(20)
		DECLARE @DESCRIPCIONMAESTRO VARCHAR(200), @UNIDADCODIGO VARCHAR(10)
		DECLARE @DESCRIPCIONCORTA VARCHAR(200), @ALMACENCODIGO VARCHAR(6)
		
		IF (@Producto = 'PETROLEO') BEGIN
			IF @IdEmpresa IN (1553) BEGIN
				SET @ITEMMAESTRO = '1702001001' 
				SET @DESCRIPCIONMAESTRO ='PETROLEO LIMA'
				SET @DESCRIPCIONCORTA ='ABASTECIMIENTO LIMA'
				SET @ALMACENCODIGO ='A004'
				SET @UNIDADCODIGO = 'GL'
			END
			ELSE BEGIN
				SET @ITEMMAESTRO = '0000009663' 
				SET @DESCRIPCIONMAESTRO ='PETROLEO RUTA'
				SET @DESCRIPCIONCORTA ='ABASTECIMIENTO EN RUTA'
				SET @ALMACENCODIGO ='A001'
				SET @UNIDADCODIGO = 'GL'
			END
		END
		
		IF (@Producto = 'GAS GNL') BEGIN
			SET @ITEMMAESTRO = '0000011633' 
			SET @DESCRIPCIONMAESTRO ='GAS G.N.L'
			SET @DESCRIPCIONCORTA ='ABASTECIMIENTO EN RUTA'
			SET @ALMACENCODIGO ='A001'
			SET @UNIDADCODIGO = 'KG'
		END

		-- Insertamos tabla temporal @Table_Transaccion_Combustible		
		INSERT INTO @Table_Transaccion_Combustible (AlmacenCodigo, ItemCodigo, Condicion, Lote, FechaDespacho, TransaccionCodigo, CompaniaSocio, 
													TipoDocumento, NumeroDocumento, CantidadDespachada, PrecioCombustible, CostoReal,
													TipoCambio, CentroCostoConsumo, Proyecto, ReferenciaTipoDocumento, ReferenciaNumeroDocumento,
													PlacaVehiculo, Sucursal, UnidadNegocio, Periodo, UnidadCodigo, Usuario, Ticket, NroTicketPreViaje,
													RecibidoPor)
				SELECT @ALMACENCODIGO,@ITEMMAESTRO, '0', '00', @FechaDespacho, 'REQ', '10000000', 
					'NS', @NUMERODOCUMENTO, @Cantidad, @Precio as 'PrecioUnitario', @precio*@Cantidad CostoReal,
					@TIPOCAMBIO, V.CentroCosto, V.Proyecto, 'RQ', NULL AS 'NumeroDespacho', 
					@Placa, 'BTRU', 'TRAN', CONVERT(VARCHAR(06), @FechaDespacho, 112), @UNIDADCODIGO, @UsuarioCrea, 
					RIGHT('000000000' + LTRIM(RTRIM(@NroTicket)),9), @Codigo, @Chofer
				FROM OP_TR_Vehiculo V 
				WHERE V.TipoCombustible NOT IN (1)
					AND v.INDTERCERO = 'P'
					AND V.Estado = '2'
					AND REPLACE(REPLACE(numeroplaca, '-',''),'.','') = REPLACE(REPLACE(RTRIM(@Placa), '-',''),'.','')
		
		
		IF @IdEmpresa  NOT IN (1553)
		BEGIN
				/*************GENERANDO LA ORDEN DE COMPRA DEL DOCUMENTO PARA LA NOTA DE SALIDA **********************************************************/
				DECLARE @CORRELATIVOORDCOMPRA VARCHAR(10)
				
				SET @CORRELATIVOORDCOMPRA= (Select (RIGHT('000000000' + LTRIM(RTRIM(ISNULL(CorrelativoNumero, 0) +1)),10))
				FROM CorrelativosMast
				WHERE ( CorrelativosMast.CompaniaCodigo ='100000' ) AND ( CorrelativosMast.TipoComprobante ='SY' ) AND ( CorrelativosMast.Serie ='WHPO' ) )
				
				update CorrelativosMast SET CorrelativoNumero =CAST (@CORRELATIVOORDCOMPRA AS INTEGER) , 
				UltimaFechaModif =GETDATE()
				WHERE ( CorrelativosMast.CompaniaCodigo ='100000' ) AND ( CorrelativosMast.TipoComprobante ='SY' ) AND ( CorrelativosMast.Serie ='WHPO' ) 
				
				DECLARE @FORMADEPAGO CHAR(3)
				SET @FORMADEPAGO = (SELECT FormadePago FROM ProveedorMast WHERE Proveedor = @IdEmpresa )
				
				IF 	@FORMADEPAGO IS NULL
				BEGIN
					SET @exito = '-7= El proveedor no tiene configurado una forma de pago. Comunicarse con Area Contabilidad e Informar. ' 
					ROLLBACK
					GOTO Terminar	
				END
					
				
				INSERT INTO WH_OrdenCompra ( CompaniaSocio, NumeroOrden, Clasificacion, UnidadNegocio, Proveedor, MonedaCodigo, AlmacenCodigo, LocalForeignFlag, 
								FechaPrometida, FechaPreparacion, PreparadaPor, TipoServicio, MontoBruto, MontoIGV, MontoOtros, MontoFlete, MontoTotal, MontoPendientedePago, 
								TipodeCambio, FormadePago, ImpresionNumero, ViaTransporte, RacionFlag, PagarconAdelantoFlag, Observaciones, Estado, UltimaFechaModif, 
								UltimoUsuario, MontoNoAfecto, AlmacenCodigoIngreso, PlazoEntrega,AprobadaPor,FechaAprobacion ) 
				 SELECT T.CompaniaSocio,@CORRELATIVOORDCOMPRA , 'LOC', 'TRAN', @IdEmpresa, 'LO',T.AlmacenCodigo, 'L',T.FechaDespacho,T.FechaDespacho, @IDUSUARIOCOMPRAS,
							CASE WHEN @lugar = 'TARAPOTO' and @IdEmpresa = 6767 THEN  'NOAFEC'
								WHEN @LUGAR = 'BAGUA GRANDE' and @IdEmpresa IN (6767) THEN 'NOAFEC' -- serbicon, utcubamba 1 y 2
								WHEN @LUGAR = 'MOYOBAMABA' and @IdEmpresa = 6767 THEN 'NOAFEC'						
								ELSE 'IGV' END ,
							 (T.CantidadDespachada * @PRECIOITEM),
							 CASE WHEN @lugar = 'TARAPOTO' and @IdEmpresa = 6767 THEN 0.0000
								WHEN @LUGAR = 'BAGUA GRANDE' and @IdEmpresa IN (6767) THEN 0.0000
								WHEN @LUGAR = 'MOYOBAMABA'  and @IdEmpresa = 6767THEN 0.0000						
								ELSE ((T.CantidadDespachada * @PRECIOITEM) * 0.18) END,
							 0.0000, 0.0000,
							 CASE WHEN @lugar = 'TARAPOTO'  and @IdEmpresa = 6767 THEN (T.CantidadDespachada * @PRECIOITEM)
								WHEN @LUGAR = 'BAGUA GRANDE'  and @IdEmpresa IN (6767) THEN (T.CantidadDespachada * @PRECIOITEM)
								WHEN @LUGAR = 'MOYOBAMABA' and @IdEmpresa = 6767 THEN (T.CantidadDespachada * @PRECIOITEM)						
								ELSE ((T.CantidadDespachada * @PRECIOITEM) +
							 ((T.CantidadDespachada * @PRECIOITEM) * 0.18)) END,
							 CASE WHEN @lugar = 'TARAPOTO' and @IdEmpresa = 6767 THEN (T.CantidadDespachada * @PRECIOITEM)
								WHEN @LUGAR = 'BAGUA GRANDE' and @IdEmpresa IN (6767) THEN (T.CantidadDespachada * @PRECIOITEM)
								WHEN @LUGAR = 'MOYOBAMABA' and @IdEmpresa = 6767 THEN (T.CantidadDespachada * @PRECIOITEM)						
								ELSE ((T.CantidadDespachada * @PRECIOITEM) + ((T.CantidadDespachada * @PRECIOITEM) * 0.18)) END,					 
							 T.TipoCambio, @FORMADEPAGO, 0, 'M', 'N', 'N','COMBUSTIBLE PARA LA UNIDAD: '+T.PlacaVehiculo, 'CO', T.FechaDespacho,@UsuarioCrea, 
							 0.0000, 'A001', 0, @IDUSUARIOCOMPRAS,T.FechaDespacho
				FROM @Table_Transaccion_Combustible T		
				
				-- SELECT T.CompaniaSocio,@CORRELATIVOORDCOMPRA , 'LOC', 'TRAN', @IdEmpresa, 'LO',T.AlmacenCodigo, 'L',T.FechaDespacho,T.FechaDespacho, @IDUSUARIOCOMPRAS, 'IGV',
				--			 (T.CantidadDespachada * @PRECIOITEM),((T.CantidadDespachada * @PRECIOITEM) * 0.18), 0.0000, 0.0000,((T.CantidadDespachada * @PRECIOITEM) +
				--			 ((T.CantidadDespachada * @PRECIOITEM) * 0.18)),((T.CantidadDespachada * @PRECIOITEM) + ((T.CantidadDespachada * @PRECIOITEM) * 0.18)), 
				--			 T.TipoCambio, @FORMADEPAGO, 0, 'M', 'N', 'N','COMBUSTIBLE PARA LA UNIDAD: '+T.PlacaVehiculo, 'CO', T.FechaDespacho,'YCALDERON', 
				--			 0.0000, 'A001', 0, @IDUSUARIOCOMPRAS,T.FechaDespacho
				--FROM @Table_Transaccion_Combustible T
				
				
				INSERT INTO WH_OrdenCompraDetalle ( CompaniaSocio, NumeroOrden, Secuencia, Item, Condicion, UnidadCodigo, Descripcion, CantidadPedida, CantidadRecibida, 
				PrecioUnitario, PrecioUnitarioOtros, PrecioXCantidad, CentroCosto, Comentario, FechaPrometida, Estado, PrecioUnitarioInicial, UltimoUsuario, 
				UltimaFechaModif, Descuento, IGVExoneradoFlag ) 
				
				SELECT T.CompaniaSocio,@CORRELATIVOORDCOMPRA, 1, T.ItemCodigo, '0', @UNIDADCODIGO, @DESCRIPCIONMAESTRO, T.CantidadDespachada, T.CantidadDespachada, @PRECIOITEM, 
				0.000000,(T.CantidadDespachada * @PRECIOITEM), T.CentroCostoConsumo, '',T.FechaDespacho,
				 'CO',@PRECIOITEM, @UsuarioCrea,T.FechaDespacho, 0.0000, 'N' 
				FROM @Table_Transaccion_Combustible T
						
				INSERT INTO WH_OrdenCompraDistribucion ( CompaniaSocio, NumeroOrden, Secuencia, Linea, Account, Monto, Sucursal, CentroCosto )
				SELECT T.CompaniaSocio ,@CORRELATIVOORDCOMPRA, 1, 1, '2521003', (T.CantidadDespachada * @PRECIOITEM),T.Sucursal, T.CentroCostoConsumo
				FROM @Table_Transaccion_Combustible T					
				/*****************************************************************************************************************************************/
					
				
				/*****************************CREACION DE LA NOTA DE INGRESO*************************************************************************/
				
				-- Insertado de Tranbsaccion Cabacera
				INSERT INTO WH_TransaccionHeader(CompaniaSocio, TipoDocumento, NumeroDocumento, TransaccionOperacion, TransaccionCodigo, 
							FechaDocumento, TipodeCambio, Periodo, AlmacenCodigo, AlmacenTraslado, CentroCostoConsumo, Proyecto, 
							ReferenciaTipoDocumento, ReferenciaNumeroDocumento,GuiaProveedor, PlacaVehiculo, SolicitadoPor, 
							AprobadoPor, RecibidoPor, Comentario, Sucursal, UnidadNegocio, UnidadReplicacion, 
							GenerarGuiaFlag, ImprimirGuiaFlag, TransferenciaRecibidaFlag, ValorizacionManualFlag, ValorizacionPendienteFlag, 
							Estado, UltimaFechaModif, UltimoUsuario, MotivoDevolucion, CampoReferencia, ReferenciaNumeroInterno) 
					SELECT T.CompaniaSocio, 'NI',@NUMERODOCUMENTOINGRESO, NULL, 'ROC', 
						T.FechaDespacho, T.TipoCambio, T.Periodo, T.AlmacenCodigo, '', T.CentroCostoConsumo, T.Proyecto, 
						'TK', @Codigo,@Codigo, T.PlacaVehiculo, T.SolicitadoPor, 
						T.AprobadoPor, NULL, '', T.Sucursal, T.UnidadNegocio, 'TRUJ', 	
						'N', 'N', 'N', 'N', 'N', 
						'PR', T.FechaDespacho,  @UsuarioCrea, '', 90, 0
					FROM @Table_Transaccion_Combustible T 
			
			
				-- Insertado de Transaccion Detalle
				INSERT INTO WH_TransaccionDetalle (CompaniaSocio, TipoDocumento, NumeroDocumento, Secuencia, Item, Condicion, Lote, 
							UnidadCodigo, Cantidad, PrecioUnitario, MontoTotal, PrecioUnitarioDolares, MontoTotalDolares, 
							ReferenciaCompaniaSocio, ReferenciaTipoDocumento, ReferenciaNumeroDocumento, ReferenciaSecuencia, UnidadReplicacion, 
							AprobacionTecnicaFlag, FallaFlag, Observaciones, ValorizacionExternaFlag, CantidadFIFO, CentroCosto, afe) 
					SELECT T.CompaniaSocio, 'NI', @NUMERODOCUMENTOINGRESO, 1 AS Secuencia, T.ItemCodigo, T.Condicion, NULL, 		
						T.UnidadCodigo, T.CantidadDespachada, T.PrecioCombustible, T.CostoReal, (T.PrecioCombustible / T.TipoCambio), (T.CostoReal / T.TipoCambio), 			
						T.CompaniaSocio, 'OC', @CORRELATIVOORDCOMPRA, 1, 'TRUJ', 				
						'N', 'N', NULL, 'N', T.CantidadDespachada, T.CentroCostoConsumo, T.Proyecto
					FROM @Table_Transaccion_Combustible T
					
			
				-- Insertado de Kardex	
				INSERT INTO WH_Kardex (AlmacenCodigo, Item, Condicion, Lote, Fecha, TransaccionCodigo, ReferenciaCompaniaSocio, 
										ReferenciaTipoDocumento, ReferenciaNumeroDocumento, ReferenciaSecuencia, Cantidad, CantidadDoble, 
										PrecioUnitario, PrecioUnitarioDolares, 
										MontoTotal, MontoTotalDolares, Periodo, UltimoUsuario, UltimaFechaModif) 
									SELECT T.AlmacenCodigo, T.ItemCodigo, T.Condicion, T.Lote, T.FechaDespacho,'ROC', T.CompaniaSocio, 
										'NI', @NUMERODOCUMENTOINGRESO, 1, CONVERT(decimal(16,6), T.CantidadDespachada ), NULL, 
										T.PrecioCombustible, CONVERT(decimal(16,6), T.PrecioCombustible / T.TipoCambio), 
										CONVERT(decimal(16,6), T.CostoReal),  CONVERT(decimal(16,6), T.CostoReal / T.TipoCambio ), 
										T.Periodo, @UsuarioCrea, T.FechaDespacho 		
									FROM @Table_Transaccion_Combustible T
					--select * from AP_Documentos where ReferenciaNumeroDocumento ='0000056796'				

				-- Actualizamos Stock Actual del LOTE DE ALMACEN
				
				
				/********INGRESAMOS LA INFORMACION PARA CONTABILIDAD***************************************************************************************************/
						
				INSERT INTO AP_Documentos(Proveedor,DocumentoClasificacion,DocumentoReferencia,Fecha,CompaniaSocio,ReferenciaTipoDocumento,ReferenciaNumeroDocumento,
					VariacionPrecioFlag,ObligacionTipoDocumento,ObligacionNumeroDocumento,Comentario,MonedaCodigo,MontoReferencia,MontoTotal,MontoNoAfecto,MontoImpuestos,
					MontoaPagar,BLNumero,MoraAprobadoPor,MoraFechaAprobacion,MoraPenalidadDiaria,MoraMonto,MoraEstado,MoraDocumento,Estado,UltimoUsuario,UltimaFechaModif,
					TransaccionTipoDocumento,TransaccionNumeroDocumento,MontoOtros,TransaccionNumeroMIT,TransaccionDocumento,UnidadNegocio,NumeroPreFactura)
				SELECT @IdEmpresa,'ROC',@Codigo,T.FechaDespacho,T.CompaniaSocio,'OC',@CORRELATIVOORDCOMPRA,NULL,NULL,NULL,@DESCRIPCIONMAESTRO,'LO',NULL,(T.CantidadDespachada * @PRECIOITEM),
				'0.0',((T.CantidadDespachada * @PRECIOITEM)*0.18),((T.CantidadDespachada * @PRECIOITEM)+((T.CantidadDespachada * @PRECIOITEM)*0.18)),NULL,NULL,NULL,NULL,NULL,NULL,NULL,
				'PR',@UsuarioCrea,T.FechaDespacho,'NI',@NUMERODOCUMENTOINGRESO,NULL,NULL,('TK-'+CAST(@Codigo AS VARCHAR(16))),'TRAN',NULL
				FROM @Table_Transaccion_Combustible T
				
				
				INSERT INTO AP_DocumentosDetalle(Proveedor,	DocumentoClasificacion,	DocumentoReferencia,Secuencia,ReferenciaSecuencia,Item,Commodity,Descripcion,Cantidad,
				PrecioUnitario)		
				SELECT @IdEmpresa,'ROC',@Codigo,1,1,T.ItemCodigo,NULL,@DESCRIPCIONMAESTRO,T.CantidadDespachada,@PRECIOITEM
				FROM @Table_Transaccion_Combustible T
				
				/******************************************************************************************************************************************************/		
															
				UPDATE WA SET WA.StockActual = WA.StockActual + @Cantidad
				FROM WH_ItemAlmacenLote WA
					INNER JOIN @Table_Transaccion_Combustible T ON T.AlmacenCodigo = WA.AlmacenCodigo 
																AND T.ItemCodigo = WA.Item 
																AND T.Lote = WA.Lote 
																AND T.Condicion = WA.Condicion  
				WHERE T.AlmacenCodigo = @ALMACENCODIGO
					AND T.ItemCodigo = WA.Item 
					AND T.Lote = WA.Lote 
					AND T.Condicion = WA.Condicion 
				
				/***********************************************************************************************************************************/		
		END
		
		
		/***************************GENERANDO EL REQUERIMIENTO PARA LA ORDEN DE SALIDA******************************************************/
		DECLARE @CORRELATIVOREQUER VARCHAR(10)
		SET @CORRELATIVOREQUER = (Select (RIGHT('000000000' + LTRIM(RTRIM(ISNULL(CorrelativoNumero, 0) +1)),10))
		FROM CorrelativosMast 
		WHERE ( CorrelativosMast.CompaniaCodigo ='100000' ) AND ( CorrelativosMast.TipoComprobante ='SY' ) AND ( CorrelativosMast.Serie ='WHRQ' ) )
		
		DECLARE @AFEST VARCHAR(20)
		SET @AFEST = (SELECT TOP 1 Proyecto FROM OP_TR_Vehiculo 
		WHERE REPLACE(REPLACE(numeroplaca, '-',''),'.','') = REPLACE(REPLACE(RTRIM(@Placa), '-',''),'.','') AND IndTercero ='P')
				
		update CorrelativosMast 
		SET CorrelativoNumero =CAST (@CORRELATIVOREQUER AS INTEGER) , UltimaFechaModif =GETDATE()
		WHERE ( CorrelativosMast.CompaniaCodigo ='100000' ) AND ( CorrelativosMast.TipoComprobante ='SY' ) AND ( CorrelativosMast.Serie ='WHRQ' ) 		
		
		
		INSERT INTO WH_Requisiciones ( CompaniaSocio, RequisicionNumero, Clasificacion, ComprasAlmacenFlag, AlmacenCodigo, MonedaCodigo, FechaRequerida, 
		FechaPreparacion, PreparadaPor, Departamento, PrecioTotal, PrioridadCodigo, DefaultPrime, DefaultAfe, CuantiaMonetariaPendienteFlag, 
		UnidadNegocio, UnidadReplicacion, LocalForeignFlag, Comentarios, Estado, UltimoUsuario, UltimaFechaModif, UltimoUsuarioNumero, 
		TransaccionOperacion, DefaultCampoReferencia, DireccionDestino, UnidadNegocioCompra, RevisionTecnicaPendienteFlag,AprobadaPor ) 
		
		SELECT T.CompaniaSocio, @CORRELATIVOREQUER, 'STO', 'A',T.AlmacenCodigo, 'LO', T.FechaDespacho, T.FechaDespacho, @IDUSUARIO, 'COM',(T.CantidadDespachada * @PRECIOITEM), '1', '010201', 
		@AFEST, 'N', 'TRAN', 'TRUJ', 'L', @DESCRIPCIONCORTA+' '+T.PlacaVehiculo, 'CO',@UsuarioCrea, T.FechaDespacho, @IDUSUARIO, '999', '90', 'Costos', 'TRAN', 'N'
		,@IDUSUARIO 
		FROM @Table_Transaccion_Combustible T
		
		INSERT INTO WH_RequisicionDetalle ( CompaniaSocio, RequisicionNumero, Secuencia, Item, Condicion, UnidadCodigo, Descripcion, ComprasAlmacenFlag, 
		RedefinidoFlag, CantidadPedida, CantidadOrdenCompra, CantidadRecibida, PrecioUnitario, PrecioxCantidad, CotizacionCantidad, CotizacionPrecioUnitario, 
		CotizacionPrecioUnitarioconIGV, CotizacionProveedor, CotizacionRegistros, ControlPresupuestalFlag, Comentario, CentroCosto, Estado, 
		UltimoUsuario, UltimaFechaModif, IGVExoneradoFlag, GenerarContratoFlag, Afe )
		
		SELECT T.CompaniaSocio, @CORRELATIVOREQUER, 1, T.ItemCodigo, '0', @UNIDADCODIGO, @DESCRIPCIONMAESTRO, 'A', 'N', T.CantidadDespachada, 0.0000, T.CantidadDespachada,
		@PRECIOITEM, (T.CantidadDespachada * @PRECIOITEM), 0.0000, 0.000000, 
		0.000000, @IDUSUARIO, @IDUSUARIO, 'S', '', '010201', 'CO',@UsuarioCrea, T.FechaDespacho, 'N', 'N',@AFEST 
		FROM @Table_Transaccion_Combustible T
		
		INSERT INTO WH_RequisitionDistribucion ( RequisicionNumero, Secuencia, Linea, Account, Afe, Monto, CompaniaSocio, Sucursal, CampoReferencia ) 
		SELECT  @CORRELATIVOREQUER, 1, 1, '9000203', @AFEST, 100.0000,T.CompaniaSocio, 'BTRU', '90' 
		FROM @Table_Transaccion_Combustible T	
		/***********************************************************************************************************************************/
		
		/***************************CREACION DE LA NOTA DE SALIDA (DESPACHO)****************************************************************/
		-- Insertado de Tranbsaccion Cabacera
		INSERT INTO WH_TransaccionHeader(CompaniaSocio, TipoDocumento, NumeroDocumento, TransaccionOperacion, TransaccionCodigo, 
					FechaDocumento, TipodeCambio, Periodo, AlmacenCodigo, AlmacenTraslado, CentroCostoConsumo, Proyecto, 
					ReferenciaTipoDocumento, ReferenciaNumeroDocumento, PlacaVehiculo, SolicitadoPor, 
					AprobadoPor, RecibidoPor, Comentario, Sucursal, UnidadNegocio, UnidadReplicacion, 
					GenerarGuiaFlag, ImprimirGuiaFlag, TransferenciaRecibidaFlag, ValorizacionManualFlag, ValorizacionPendienteFlag, 
					Estado, UltimaFechaModif, UltimoUsuario, MotivoDevolucion, CampoReferencia, ReferenciaNumeroInterno) 
			SELECT T.CompaniaSocio, T.TipoDocumento, T.NumeroDocumento, '999', T.TransaccionCodigo, 
				T.FechaDespacho, T.TipoCambio, T.Periodo, T.AlmacenCodigo, '', T.CentroCostoConsumo, T.Proyecto, 
				T.ReferenciaTipoDocumento, @CORRELATIVOREQUER, T.PlacaVehiculo, T.SolicitadoPor, 
				T.AprobadoPor, T.RecibidoPor, '', T.Sucursal, T.UnidadNegocio, 'TRUJ', 	
				'N', 'N', 'N', 'N', 'N', 
				'PR', T.FechaDespacho, T.Usuario, '', 90, 0
			FROM @Table_Transaccion_Combustible T 
	
		
		-- Insertado de Transaccion Detalle
		INSERT INTO WH_TransaccionDetalle (CompaniaSocio, TipoDocumento, NumeroDocumento, Secuencia, Item, Condicion, Lote, 
					UnidadCodigo, Cantidad, PrecioUnitario, MontoTotal, PrecioUnitarioDolares, MontoTotalDolares, 
					ReferenciaCompaniaSocio, ReferenciaTipoDocumento, ReferenciaNumeroDocumento, ReferenciaSecuencia, UnidadReplicacion, 
					AprobacionTecnicaFlag, FallaFlag, Observaciones, ValorizacionExternaFlag, CantidadFIFO, CentroCosto, afe) 
			SELECT T.CompaniaSocio, T.TipoDocumento, T.NumeroDocumento, 1 AS Secuencia, T.ItemCodigo, T.Condicion, T.Lote, 		
				T.UnidadCodigo, T.CantidadDespachada, T.PrecioCombustible, T.CostoReal, (T.PrecioCombustible / T.TipoCambio), (T.CostoReal / T.TipoCambio), 			
				T.CompaniaSocio, T.ReferenciaTipoDocumento,@CORRELATIVOREQUER, 1, 'TRUJ', 				
				'N', 'N', NULL, 'N', T.CantidadDespachada, T.CentroCostoConsumo, T.Proyecto
			FROM @Table_Transaccion_Combustible T
			
			
		-- Insertado de Kardex	
		INSERT INTO WH_Kardex (AlmacenCodigo, Item, Condicion, Lote, Fecha, TransaccionCodigo, ReferenciaCompaniaSocio, 
								ReferenciaTipoDocumento, ReferenciaNumeroDocumento, ReferenciaSecuencia, Cantidad, CantidadDoble, 
								PrecioUnitario, PrecioUnitarioDolares, 
								MontoTotal, MontoTotalDolares, Periodo, UltimoUsuario, UltimaFechaModif) 
							SELECT T.AlmacenCodigo, T.ItemCodigo, T.Condicion, T.Lote, T.FechaDespacho, T.TransaccionCodigo, T.CompaniaSocio, 
								T.TipoDocumento, T.NumeroDocumento, 1, CONVERT(decimal(16,6), T.CantidadDespachada * -1), 0.00, 
								T.PrecioCombustible, CONVERT(decimal(16,6), T.PrecioCombustible / T.TipoCambio), 
								CONVERT(decimal(16,6), T.CostoReal * -1),  CONVERT(decimal(16,6), T.CostoReal / T.TipoCambio * -1), 
								T.Periodo, T.Usuario, T.FechaDespacho 		
							FROM @Table_Transaccion_Combustible T
							

		-- Actualizamos Stock Actual del LOTE DE ALMACEN
											
		UPDATE WA SET WA.StockActual = WA.StockActual + (-1)*@Cantidad
		FROM WH_ItemAlmacenLote WA
			INNER JOIN @Table_Transaccion_Combustible T ON T.AlmacenCodigo = WA.AlmacenCodigo 
														AND T.ItemCodigo = WA.Item 
														AND T.Lote = WA.Lote 
														AND T.Condicion = WA.Condicion  
		WHERE T.AlmacenCodigo = @ALMACENCODIGO
			AND T.ItemCodigo = WA.Item 
			AND T.Lote = WA.Lote 
			AND T.Condicion = WA.Condicion 
		/**************************************************************************************************************************************/	
					
		IF HOST_NAME() IN ('TI01-GT') OR @UsuarioCrea IN ('AGUERRA')
		BEGIN
			SET @exito = '777= TEST OK'
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

TERMINAR:
IF LEFT(@Exito,1)='-' OR cast(left(@Exito,1) AS INT)>0 BEGIN
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
-- Author:		Ing. Jordyn Toribio Esquivel
-- Create date: 15.Junio.2022
-- Description:	Anular tickets terceros del Kardex
-- =============================================
/*
exec ReportesApp_Operaciones_DespachosTerceros_Anular @Ticket=545,@Codigo=498085,@Placa=N'T8Z865',@FechaDespacho=N'08/04/2023 10:19:38',@Hora=N'22:19:38',@Cantidad=48.536,@Precio=0,@Chofer=N'GONGORA MORALES, FRANCISCO ALBERTO',@NotaSalida=606795,@UsuarioAnula=N'JROJAS'
*/
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_DespachosTerceros_Anular]
@Ticket INT,
@Codigo INT, 
@Placa VARCHAR(20), 
@FechaDespacho DATETIME, 
@Hora TIME,
@Cantidad NUMERIC(12,3), 
@Precio NUMERIC(12,3), 
@Chofer VARCHAR(80), 
@NotaSalida INT,
@Producto VARCHAR(80),
@Proveedor VARCHAR(150),
@Lugar VARCHAR(80),
@UsuarioAnula VARCHAR(20)
AS
DECLARE @exito varchar(max)

SET @exito = '0= Anulación Exitosa!'

BEGIN TRAN
BEGIN TRY
	DECLARE @CompaniaSocio VARCHAR(10) = '10000000' 
	DECLARE @Observacion VARCHAR(100) = 'SE HACE UN INGRESO DE ANULACIÓN POR UN ERROR DE NOTA DE SALIDA: ' + CONVERT(VARCHAR(30), ISNULL(@NotaSalida, ''))
	DECLARE @NUMERODOCUMENTO VARCHAR(50)
	DECLARE @Periodo VARCHAR(10) = CONVERT(CHAR(4),YEAR(@FechaDespacho))+CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(@FechaDespacho))),2))

	SET @Placa = REPLACE(REPLACE(ISNULL(@Placa, ''), '-',''),'.','')	
	SET @FechaDespacho = CONVERT(VARCHAR(10),CAST(@FechaDespacho AS  DATE)) + 'T' +  CONVERT( VARCHAR(12), CAST(@Hora AS TIME))

	--Validamos que exista Placa
	IF NOT EXISTS(SELECT TOP 1 * FROM OP_TR_Vehiculo WHERE REPLACE(REPLACE(numeroplaca, '-',''),'.','') = REPLACE(REPLACE(@Placa, '-',''),'.','')) BEGIN
		SET @exito = '-1 = No se encontró la placa.'
		ROLLBACK
		GOTO Terminar
	END
		
	--Validamos que exista conductor
	IF NOT EXISTS(SELECT IdConductor AS 'ID', RTRIM(Nombre) AS 'RELACION', Documento AS 'DOCUMENTO' FROM OP_TR_Conductor C WITH(NOLOCK)
				  WHERE C.Estado='A' AND RTRIM(C.Nombre) = @Chofer) BEGIN
		SET @exito = '-2 = No se encontró al conductor ' + @Chofer
		ROLLBACK
		GOTO Terminar
	END	
	
	--Validamos que ticket no este Anulado
	IF EXISTS(SELECT * FROM ReportesApp_Combustible_TicketsTercero TT WITH(NOLOCK) WHERE TT.Ticket = @Ticket AND FHAnula IS NOT NULL
			  AND UsuarioAnula IS NOT NULL AND ISNULL(Anulado, 0) = 1) BEGIN
		SET @exito = '-3 = El ticket ya fue anulado.'
		ROLLBACK
		GOTO Terminar
	END

	--Validamos que ticket no este anexado a despacho de combustible
	IF EXISTS(SELECT * FROM ReportesApp_Combustible_TicketsTercero TT WITH(NOLOCK) WHERE TT.Ticket = @Ticket AND TT.IDViaje IS NOT NULL) BEGIN
		SET @exito = '-4 = El ticket ya fue anexado con Despacho de Combustible.'
		ROLLBACK
		GOTO Terminar
	END

	-- Validar que el registro se encuentre en el periodo actual
	IF (@Periodo != CONVERT(CHAR(4),YEAR(GETDATE()))+CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(GETDATE()))),2))) BEGIN
		SET @exito = '-5 = El periodo ya se encuentra cerrado.'
		ROLLBACK
		GOTO Terminar
	END
			
	-- Anulamos el ticket tercero
	UPDATE ReportesApp_Combustible_TicketsTercero SET UsuarioAnula = @UsuarioAnula, FHAnula = GETDATE(), Anulado = 1
	WHERE Ticket = @Ticket
	
	DELETE FROM ReportesApp_Combustible_Nexo_Ticket_CargaCombustible_Kardex WHERE NroTicketPreViaje = @Codigo

	-- Anular Orden de Compra
	DELETE FROM WH_OrdenCompraDistribucion WHERE NumeroOrden = (SELECT ReferenciaNumeroDocumento FROM AP_Documentos WHERE DocumentoReferencia = CONVERT(CHAR(20),@Codigo))
	DELETE FROM WH_OrdenCompraDetalle WHERE NumeroOrden = (SELECT ReferenciaNumeroDocumento FROM AP_Documentos WHERE DocumentoReferencia = CONVERT(CHAR(20),@Codigo))
	DELETE FROM WH_OrdenCompra WHERE NumeroOrden = (SELECT ReferenciaNumeroDocumento FROM AP_Documentos WHERE DocumentoReferencia = CONVERT(CHAR(20),@Codigo))

	DELETE FROM WH_TransaccionDetalle WHERE ReferenciaNumeroDocumento = (SELECT CONVERT(CHAR(14),ReferenciaNumeroDocumento) FROM AP_Documentos WHERE DocumentoReferencia = CONVERT(CHAR(20),@Codigo)) AND TipoDocumento = 'NI'
	DELETE FROM WH_TransaccionHeader WHERE ReferenciaNumeroDocumento = CONVERT(CHAR(14),@Codigo) AND TipoDocumento = 'NI'
	
	DELETE FROM WH_Kardex WHERE ReferenciaNumeroDocumento = CONVERT(CHAR(10),@Codigo) AND ReferenciaTipoDocumento = 'NI'
	
	-- Anular Requerimiento
	DELETE FROM AP_DocumentosDetalle WHERE DocumentoReferencia = CONVERT(CHAR(20),@Codigo)
	DELETE FROM AP_Documentos WHERE DocumentoReferencia = CONVERT(CHAR(20),@Codigo)

	DELETE FROM WH_RequisitionDistribucion WHERE RequisicionNumero = (SELECT CONVERT(CHAR(10),ReferenciaNumeroDocumento) FROM WH_TransaccionHeader WHERE NumeroDocumento = CONVERT(CHAR(10),@NotaSalida))
	DELETE FROM WH_RequisicionDetalle WHERE RequisicionNumero = (SELECT CONVERT(CHAR(10),ReferenciaNumeroDocumento) FROM WH_TransaccionHeader WHERE NumeroDocumento = CONVERT(CHAR(10),@NotaSalida))
	DELETE FROM WH_Requisiciones WHERE RequisicionNumero = (SELECT CONVERT(CHAR(10),ReferenciaNumeroDocumento) FROM WH_TransaccionHeader WHERE NumeroDocumento = CONVERT(CHAR(10),@NotaSalida))

	DELETE FROM WH_TransaccionDetalle WHERE NumeroDocumento = CONVERT(CHAR(10),@NotaSalida)
	DELETE FROM WH_TransaccionHeader WHERE NumeroDocumento = CONVERT(CHAR(10),@NotaSalida)
	DELETE FROM WH_Kardex WHERE ReferenciaNumeroDocumento = CONVERT(CHAR(10),@NotaSalida)

	-- Actualizamos Stock Actual del LOTE DE ALMACEN									
	IF (@Producto = 'PETROLEO') BEGIN
		IF ((LTRIM(RTRIM(@Proveedor)) = 'GRUPO TRANSPESA S.A.C.') AND (LTRIM(RTRIM(@Lugar)) = 'LIMA')) BEGIN
			UPDATE WA SET WA.StockActual = WA.StockActual + @Cantidad
			FROM WH_ItemAlmacenLote WA
			WHERE WA.AlmacenCodigo = 'A004' AND LTRIM(RTRIM(Item)) = '1702001001'
		END
		ELSE BEGIN
			UPDATE WA SET WA.StockActual = WA.StockActual + @Cantidad
			FROM WH_ItemAlmacenLote WA
			WHERE WA.AlmacenCodigo = 'A001' AND LTRIM(RTRIM(Item)) = '0000009663'
		END	
	END

	IF (@Producto = 'GAS GNL') BEGIN
		UPDATE WA SET WA.StockActual = WA.StockActual + @Cantidad
		FROM WH_ItemAlmacenLote WA
		WHERE WA.AlmacenCodigo = 'A001' AND LTRIM(RTRIM(Item)) = '0000011633'
	END
						
	IF HOST_NAME() IN ('DESKTOP-FNO4LB1') OR @UsuarioAnula in ('JROJAS') BEGIN
		SET @exito = '777= TEST OK'
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

TERMINAR:
IF LEFT(@Exito,1)='-' OR cast(left(@Exito,1) AS INT)>0 BEGIN
	DECLARE @storedProcedureName VARCHAR(100)
	SELECT @storedProcedureName = OBJECT_NAME(@@PROCID)
	SET @exito = @exito + CHAR(13)+CHAR(13)+'SP Name: '+@storedProcedureName
END

SELECT @exito exito
