/*
Para saber el Stock:
SELECT StockActual FROM WH_ItemAlmacenLote WHERE Item = '1701001001' AND Lote = '00' AND AlmacenCodigo = 'A001'

Items:
select * from WH_ItemAlmacen  
select * from WH_AlmacenMast 
*/

-- CREAR TABLA ReportesApp_Logistica_AlertaStock (IdAlerta INT, Item CHAR(20), DescripcionCompleta VARCHAR(250), StockMinimo INT, TiempoAlerta INT)

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 11-01-2024
-- Description:	LISTAR ALMACENES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Logistica_AlertaStock_ListarAlmacenes]
AS
BEGIN
	SELECT AlmacenCodigo, DescripcionLocal FROM WH_AlmacenMast
	WHERE AlmacenCodigo IN ('A001','A004','A010','ALTRA','CONSIGNA01')
END

------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 08-05-2023
-- Description:	LISTAR ITEMS DE LOGISTICA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Logistica_AlertaStock_ListarItems]
@filtro VARCHAR(250),
@CodigoAlmacen CHAR(10)
AS
BEGIN
	SELECT TOP(40) CM.Item AS CODIGO, LTRIM(RTRIM(IM.DescripcionLocal)) AS ITEM, CM.StockNuevo AS STOCK_ACTUAL, AM.DescripcionLocal AS ALMACEN
	FROM WH_CierreMensual CM
	LEFT JOIN WH_AlmacenMast AM ON AM.AlmacenCodigo = CM.AlmacenCodigo
	LEFT JOIN WH_ItemMast IM ON IM.Item = CM.Item
	WHERE (IM.Estado = 'A') AND (@filtro IS NULL OR IM.DescripcionCompleta LIKE '%'+ @filtro + '%') AND (LTRIM(RTRIM(CM.AlmacenCodigo)) = LTRIM(RTRIM(@CodigoAlmacen)))
	AND CM.Periodo = '202312' --CONVERT(CHAR(4),YEAR(GETDATE()))+CONVERT(CHAR(2),RIGHT('00'+LTRIM(RTRIM(MONTH(GETDATE()))),2))
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 08-05-2023
-- Description:	FILTRAR ITEMS DE LOGISTICA (NO ES NECESARIO)
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Logistica_FiltrarItem]
@filtro CHAR(20)
AS
BEGIN
	DECLARE @respuesta VARCHAR(MAX)
	SET NOCOUNT ON;
    SET @respuesta = (SELECT DescripcionCompleta AS Descripcion FROM WH_ItemMast WHERE (Item LIKE '%'+ LTRIM(RTRIM(@filtro)) + '%'))
    SELECT @respuesta 'Respuesta'
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 09-05-2023
-- Description:	INSERTAR ALERTAS DE STOCK
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Logistica_AlertaStock_Insertar]
@Item CHAR(20),
@DescripcionCompleta VARCHAR(250),
@StockMinimo INT,
@TiempoAlerta INT,
@CodigoAlmacen CHAR(10)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

SET @Exito = '0=Añadida Correctamente'
SET @correlativo = (SELECT MAX(IdAlerta) + 1 FROM ReportesApp_Logistica_AlertaStock)
SET @correlativo = ISNULL(@correlativo,1) 
	
IF (@DescripcionCompleta = (SELECT DescripcionCompleta FROM ReportesApp_Logistica_AlertaStock WHERE DescripcionCompleta = @DescripcionCompleta AND LTRIM(RTRIM(CodigoAlmacen)) = LTRIM(RTRIM(@CodigoAlmacen))))
BEGIN
	SET @Exito = '-1=Este item ya fue añadido.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	INSERT INTO ReportesApp_Logistica_AlertaStock(IdAlerta, Item, DescripcionCompleta, StockMinimo, TiempoAlerta, CodigoAlmacen)
	VALUES(@correlativo, @Item, @DescripcionCompleta, @StockMinimo, @TiempoAlerta, @CodigoAlmacen)
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
-- Create date: 08-05-2023
-- Description:	LISTAR ALERTAS DE STOCK
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Logistica_AlertaStock_ListarAlertas]
@filtro VARCHAR(200)
AS
BEGIN
	SET NOCOUNT ON;
    SELECT A.IdAlerta, LTRIM(RTRIM(A.Item)) AS CODIGO, A.DescripcionCompleta AS ITEM, CONVERT(INT,L.StockActual) AS STOCK_ACTUAL, A.StockMinimo AS STOCK_MINIMO,
	LTRIM(RTRIM(AM.DescripcionLocal)) AS ALMACEN
	FROM ReportesApp_Logistica_AlertaStock A
	LEFT JOIN WH_AlmacenMast AM ON LTRIM(RTRIM(AM.AlmacenCodigo)) = LTRIM(RTRIM(A.CodigoAlmacen))
	LEFT JOIN WH_ItemAlmacenLote L ON LTRIM(RTRIM(L.Item)) = LTRIM(RTRIM(A.Item)) AND LTRIM(RTRIM(L.AlmacenCodigo)) = LTRIM(RTRIM(A.CodigoAlmacen))
	ORDER BY IdAlerta
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 09-05-2023
-- Description:	ELIMINAR ALERTAS DE STOCK
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Logistica_AlertaStock_Eliminar]
@IdAlerta INT
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0=Eliminada Correctamente'

BEGIN TRAN
BEGIN TRY
	DELETE FROM ReportesApp_Logistica_AlertaStock WHERE IdAlerta = @IdAlerta
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

-------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 12-01-2023
-- Description:	GENERAR ALERTA DE ITEMS EN STOCK
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Logistica_AlertaStock_CorreoAlertaStock]
AS
BEGIN
	DECLARE @Mensaje AS VARCHAR(MAX), @Asunto VARCHAR(300)
	DECLARE @AlertaStock VARCHAR(MAX) = ''

	SELECT @AlertaStock = @AlertaStock + '<tr>'
									   + '<td style="background-color: #EEE8AA">' + LTRIM(RTRIM(A.Item)) + '</td>'
									   + '<td style="background-color: #EEE8AA">' + LTRIM(RTRIM(A.DescripcionCompleta)) + '</td>'
									   + '<td style="background-color: #EEE8AA">' + CONVERT(VARCHAR,CONVERT(INT,L.StockActual)) + '</td>'
									   + '<td style="background-color: #EEE8AA">' + LTRIM(RTRIM(AM.DescripcionLocal)) + '</td>'
									   + '<td style="background-color: #EEE8AA">' + CONVERT(VARCHAR,A.StockMinimo) + '</td>'
									   + '</tr>'   
	FROM ReportesApp_Logistica_AlertaStock A
	LEFT JOIN WH_AlmacenMast AM ON LTRIM(RTRIM(AM.AlmacenCodigo)) = LTRIM(RTRIM(A.CodigoAlmacen))
	LEFT JOIN WH_ItemAlmacenLote L ON LTRIM(RTRIM(L.Item)) = LTRIM(RTRIM(A.Item)) AND LTRIM(RTRIM(L.AlmacenCodigo)) = LTRIM(RTRIM(A.CodigoAlmacen))
	WHERE (CONVERT(INT,L.StockActual) < A.StockMinimo)
	
	SET @Asunto = 'ALERTA DE STOCK DE ITEMS'

	SET @Mensaje = '<p><h2>LISTA DE ITEMS CON POCO STOCK EN ALMACEN</h2></p>'
				  +'<p>Estas son los ítems cuyo stock en los almacenes es menor al mínimo programado: </p>'
				  + '<p><table border="1" cellspacing="1" cellpadding="1" >
					    <tr>
						    <td align="center" style="background-color:cyan; color: black"><b>CÓDIGO</b></td>
							<td align="center" style="background-color:cyan; color: black"><b>ITEM</b></td>
							<td align="center" style="background-color:cyan; color: black"><b>STOCK ACTUAL</b></td>
							<td align="center" style="background-color:cyan; color: black"><b>ALMACEN</b></td>
							<td align="center" style="background-color:cyan; color: black"><b>STOCK MÍNIMO</b></td>
						</tr>
						<tbody>'
						+ ISNULL(@AlertaStock, '') 	
						+'</tbody>
						</table>
					</p>'
				  +'<p>Fecha de envío: '+CONVERT(CHAR(10),GETDATE(),103)+RIGHT(RTRIM(CONVERT(CHAR(26),GETDATE(),22)),12)+'</p>'
				  +'<BR>'
				  +'<p><font face="verdana" size="2" color="DarkRed">Tecnologías de la Información - GRUPO TRANSPESA</font></p>'
	
	EXEC msdb.dbo.sp_send_dbmail 
		 @profile_name='AVISODESISTEMA',
		 @recipients = 'desarrollo2@transpesa.com.pe',
		 --@blind_copy_recipients = 'asistenteauditoria@transpesa.com.pe;jrojas@transpesa.com.pe;desarrollo@transpesa.com.pe;desarrollo2@transpesa.com.pe;' 
		 @subject = @Asunto,
		 @body_format = 'HTML',
		 @body = @Mensaje	
END
