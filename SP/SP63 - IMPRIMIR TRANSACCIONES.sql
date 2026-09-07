
-- CREAR TABLA ReportesApp_Logistica_TransaccionesMtto_ItemsDivemotor

-- CREAR TABLA ReportesApp_Logistica_TransaccionesMtto_RegistroOT

------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 02-12-2023
-- Description:	LISTAR TRANSACCIONES MTTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Logistica_TransaccionesMtto_ListarTransacciones]
@NumeroOT VARCHAR(20),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	SELECT LTRIM(RTRIM(OTR.NumeroOrden)) AS 'ORDEN_TRABAJO', LTRIM(RTRIM(OT.Descripcion)) AS 'DESCRIPCION', CONVERT(VARCHAR,OT.FechaProgramada,103) AS 'FECHA',	LTRIM(RTRIM(AC.CostCenter))+' - '+LTRIM(RTRIM(AC.LocalName)) AS 'CENTRO_COSTO', OT.Proyecto AS 'PROYECTO', P.InternalNumber AS 'UNIDAD'	FROM ME_OrdenTrabajoRecurso OTR	INNER JOIN ME_OrdenTrabajo OT ON (OTR.CompaniaSocio = OT.CompaniaSocio AND OTR.NumeroOrden = OT.NumeroOrden)	INNER JOIN ME_Recurso R ON (OTR.Recurso = R.Recurso)	LEFT JOIN afemst P ON (OT.Proyecto = P.afe)	LEFT JOIN AC_CostCenterMst AC ON (AC.CostCenter = OT.CentroCostos)	INNER JOIN ReportesApp_Logistica_TransaccionesMtto_ItemsDivemotor ID ON LTRIM(RTRIM(ID.CodigoItem)) = LTRIM(RTRIM(OTR.Recurso))	WHERE ((OTR.CantidadPedida <> 0) AND (R.GrupoRecurso = 'MA') AND (OT.Estado = 'AP' OR OT.Estado = 'PG') AND OTR.CompaniaSocio = '10000000')	AND (@NumeroOT IS NULL OR OTR.NumeroOrden LIKE '%' + @NumeroOT + '%') AND (OT.FechaProgramada BETWEEN @FINICIO AND @FFIN)	GROUP BY OTR.NumeroOrden, OT.FechaProgramada, OT.Descripcion, AC.CostCenter, AC.LocalName, OT.Proyecto, P.InternalNumber	ORDER BY OTR.NumeroOrden DESCEND--------------------------------------------------------------------------------SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 02-12-2023
-- Description:	LISTAR DETALLE TRANSACCIONES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Logistica_TransaccionesMtto_ListarDetalleTransaccion]
@Opcion INT,
@NumeroOT VARCHAR(20)
AS
BEGIN
	IF (@Opcion = 1) BEGIN		-- OT CON ÍTEMS DE DIVEMOTORS		SELECT OTR.Secuencia AS 'NRO', LTRIM(RTRIM(OTR.Recurso)) AS 'CODIGO', OTR.UnidadCodigo AS 'UND',		LTRIM(RTRIM(OTR.Descripcion)) AS 'ITEM', OTR.CantidadPedida AS 'CANTIDAD_PEDIDA', ROT.Estado AS 'ESTADO'		FROM ME_OrdenTrabajoRecurso OTR		INNER JOIN ME_Recurso R ON (OTR.Recurso = R.Recurso)		INNER JOIN WH_ItemMast I ON (OTR.Recurso = I.Item)		INNER JOIN ReportesApp_Logistica_TransaccionesMtto_ItemsDivemotor ID ON LTRIM(RTRIM(ID.CodigoItem)) = LTRIM(RTRIM(OTR.Recurso))		LEFT JOIN ReportesApp_Logistica_TransaccionesMtto_RegistroOT ROT ON LTRIM(RTRIM(ROT.CodigoItem)) = LTRIM(RTRIM(OTR.Recurso)) AND		(LTRIM(RTRIM(OTR.NumeroOrden)) = LTRIM(RTRIM(ROT.NumeroOrden))) /*AND (ROT.idRegistroOT = OTR.Secuencia)*/, SY_CampoCalculado		WHERE ((OTR.CantidadPedida > 0) AND (R.GrupoRecurso = 'MA') AND (SY_CampoCalculado.RegistroNumero = 1) AND		(OTR.CompaniaSocio = '10000000') AND (OTR.NumeroOrden = @NumeroOT))
	END
	
	IF (@Opcion = 2) BEGIN
		SELECT idItem AS 'NRO', CodigoItem AS 'CODIGO', DescripcionItem AS 'ITEM', UsuarioCrea, FechaCrea
		FROM ReportesApp_Logistica_TransaccionesMtto_ItemsDivemotor
		ORDER BY idItem DESC
	END
END

--------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 04-12-2024
-- Description:	IMPRIMIR TICKET
-- =============================================
/*
EXEC ReportesApp_Logistica_TransaccionesMtto_ImprimirTicket @NumeroOT = '0000116871',
@xmlItem = '<?xml version="1.0" encoding="UTF-8"?><r><items NRO="10" CODIGO="0110008016" ESTADO="" /><items NRO="11" CODIGO="0110008016" ESTADO="" /><items NRO="1" CODIGO="0110002026" ESTADO="" /></r>'
*/
ALTER PROCEDURE [dbo].[ReportesApp_Logistica_TransaccionesMtto_ImprimirTicket]
@NumeroOT VARCHAR(20),
@xmlItem VARCHAR(MAX),
@Usuario VARCHAR(20)
AS
DECLARE @idoc INT
DECLARE @TEMP_ITEM TABLE(Nro INT, Codigo VARCHAR(350), Estado VARCHAR(350))
BEGIN
	IF (@xmlItem IS NOT NULL) BEGIN
		EXEC sp_xml_preparedocument @idoc OUTPUT, @xmlItem
		INSERT INTO @TEMP_ITEM(Nro, Codigo, Estado)
		SELECT * FROM OPENXML(@idoc,N'/r/items')
		WITH (NRO INT, CODIGO VARCHAR(350), ESTADO VARCHAR(350));
		EXEC sp_xml_removedocument @idoc;
	END

	DECLARE @Contador INT = (SELECT MAX(idRegistroOT) FROM ReportesApp_Logistica_TransaccionesMtto_RegistroOT)
	SET @Contador = ISNULL(@Contador,0) + 1

	INSERT INTO ReportesApp_Logistica_TransaccionesMtto_RegistroOT (idRegistroOT, NumeroOrden, CodigoItem, DescripcionItem, Unidad, Cantidad, FechaImpresion, Estado, UsuarioImpresion)
	SELECT OTR.Secuencia, LTRIM(RTRIM(@NumeroOT)), LTRIM(RTRIM(OTR.Recurso)), LTRIM(RTRIM(OTR.Descripcion)), OTR.UnidadCodigo, OTR.CantidadPedida, GETDATE(), 'IMPRESO', @Usuario
	FROM ME_OrdenTrabajoRecurso OTR
	INNER JOIN ReportesApp_Logistica_TransaccionesMtto_ItemsDivemotor ID ON LTRIM(RTRIM(ID.CodigoItem)) = LTRIM(RTRIM(OTR.Recurso))
	INNER JOIN @TEMP_ITEM IT ON /*IT.Nro = OTR.Secuencia AND*/ LTRIM(RTRIM(IT.Codigo)) = LTRIM(RTRIM(OTR.Recurso))
	WHERE OTR.NumeroOrden = @NumeroOT AND OTR.CantidadPedida > 0

	SELECT ROW_NUMBER() OVER(ORDER BY OTR.Recurso) AS 'NRO', LTRIM(RTRIM(OTR.NumeroOrden)) AS 'ORDEN_TRABAJO', LTRIM(RTRIM(OT.Descripcion)) AS 'DESCRIPCION',	CONVERT(VARCHAR,OT.FechaProgramada,103) AS 'FECHA', LTRIM(RTRIM(AC.CostCenter))+' - '+LTRIM(RTRIM(AC.LocalName)) AS 'CENTRO_COSTO', P.InternalNumber AS 'UNIDAD',	LTRIM(RTRIM(OTR.Recurso)) AS 'CODIGO', OTR.UnidadCodigo AS 'UND', LTRIM(RTRIM(OTR.Descripcion)) AS 'ITEM', OTR.CantidadPedida AS 'CANTIDAD_PEDIDA'
	FROM ME_OrdenTrabajoRecurso OTR
	INNER JOIN ME_OrdenTrabajo OT ON (OTR.CompaniaSocio = OT.CompaniaSocio AND OTR.NumeroOrden = OT.NumeroOrden)
	INNER JOIN ReportesApp_Logistica_TransaccionesMtto_ItemsDivemotor ID ON LTRIM(RTRIM(ID.CodigoItem)) = LTRIM(RTRIM(OTR.Recurso))
	LEFT JOIN afemst P ON (OT.Proyecto = P.afe)	LEFT JOIN AC_CostCenterMst AC ON (AC.CostCenter = OT.CentroCostos)	INNER JOIN ME_Recurso R ON (OTR.Recurso = R.Recurso)	INNER JOIN WH_ItemMast I ON (OTR.Recurso = I.Item)
	INNER JOIN @TEMP_ITEM IT ON /*IT.Nro = OTR.Secuencia AND*/ LTRIM(RTRIM(IT.Codigo)) = LTRIM(RTRIM(OTR.Recurso))
	WHERE OTR.NumeroOrden = @NumeroOT AND OTR.CantidadPedida > 0
END

--------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 04-12-2024
-- Description:	LISTAR REGISTROS DE OT
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Logistica_TransaccionesMtto_ListarRegistroOT]
@NumeroOT VARCHAR(20),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	SELECT RT.idRegistroOT, RT.NumeroOrden AS 'ORDEN_TRABAJO', LTRIM(RTRIM(OT.Descripcion)) AS 'DESCRIPCION', LTRIM(RTRIM(RT.CodigoItem)) AS 'ITEM',	RT.Unidad AS 'UND', LTRIM(RTRIM(RT.DescripcionItem)) AS 'DESCRIPCION_ITEM', RT.Cantidad AS 'CANTIDAD_PEDIDA', RT.FechaImpresion AS 'FECHA_IMPRESION',	RT.Estado AS 'ESTADO' FROM ReportesApp_Logistica_TransaccionesMtto_RegistroOT RT 	INNER JOIN ME_OrdenTrabajo OT ON (OT.CompaniaSocio = '10000000' AND RT.NumeroOrden = OT.NumeroOrden)	WHERE (@NumeroOT IS NULL OR RT.NumeroOrden LIKE '%' + @NumeroOT + '%') AND (RT.FechaImpresion BETWEEN @FINICIO AND @FFIN)	ORDER BY RT.FechaImpresion DESCEND

--------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 03-12-2024
-- Description:	REGISTRAR Y ELIMINAR ITEMS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Logistica_TransaccionesMtto_InsertarEliminarItem]
@Opcion INT,
@CodigoItem VARCHAR(30),
@DescripcionItem VARCHAR(350),
@Usuario VARCHAR(20)
AS
DECLARE @Contador INT
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- INSERTAR ÍTEM
		IF (EXISTS(SELECT * FROM ReportesApp_Logistica_TransaccionesMtto_ItemsDivemotor WHERE CodigoItem = @CodigoItem)) BEGIN
			SET @Exito = '-1 = Este ítem ya ha sido registrado en la lista.'
			ROLLBACK
			GOTO Terminar
		END
		
		SET @Contador = (SELECT MAX(idItem) FROM ReportesApp_Logistica_TransaccionesMtto_ItemsDivemotor)
		SET @Contador = ISNULL(@Contador,0) + 1
		
		INSERT INTO ReportesApp_Logistica_TransaccionesMtto_ItemsDivemotor(idItem,CodigoItem,DescripcionItem,UsuarioCrea,FechaCrea)
		VALUES(@Contador, @CodigoItem, @DescripcionItem, @Usuario, GETDATE())
		
		SET @Exito = '0 = El ítem ' + @DescripcionItem + ' ha sido registrado en Divemotor.'
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR ÍTEM
		DELETE FROM ReportesApp_Logistica_TransaccionesMtto_ItemsDivemotor
		WHERE CodigoItem = @CodigoItem

		SET @Exito = '0 = El ítem ha sido eliminado correctamente.'
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

--------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 27-12-2024
-- Description:	ELIMINAR TICKETS IMPRESOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Logistica_TransaccionesMtto_EliminarTicketsImpresos]
@idRegistroOT INT,
@CodigoItem VARCHAR(30),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	DELETE FROM ReportesApp_Logistica_TransaccionesMtto_RegistroOT
	WHERE LTRIM(RTRIM(CodigoItem)) = @CodigoItem AND idRegistroOT = @idRegistroOT

	SET @Exito = '0 = El item impreso ha sido anulado correctamente.'
END TRY

BEGIN CATCH
	SET @Exito = RTRIM(LTRIM(STR(ERROR_NUMBER()))) + '=' + ERROR_MESSAGE()
	ROLLBACK
	GOTO Terminar
END CATCH

IF @@TRANCOUNT > 0 
   COMMIT;
   --ROLLBAC
	
TERMINAR:
IF LEFT(@Exito,1)='-' OR cast(left(@Exito,1) AS INT)<0 BEGIN
	DECLARE @storedProcedureName VARCHAR(100)
	SELECT @storedProcedureName = OBJECT_NAME(@@PROCID)
	SET @Exito = @Exito + CHAR(13)+CHAR(13)+'SP Name: '+@storedProcedureName 
END

SELECT @Exito exito