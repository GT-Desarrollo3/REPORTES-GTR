
-- CREAR TABLA ReportesApp_Logistica_EvaluacionOfertas_EvaluacionCabecera

-- CREAR TABLA ReportesApp_Logistica_EvaluacionOfertas_EvaluacionDetalle

------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 06/03/2025
-- Description:	INSERTAR DETALLE EVALUACION
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Logistica_EvaluacionOfertas_DetalleInsertar]
@idEvaluacionC INT,
@Proveedor VARCHAR(300),
@PrecioUnitario DECIMAL(10,2),
@FormaPago DECIMAL(10,2),
@PrecioTotal DECIMAL(10,2),
@CostoFinanciero DECIMAL(10,2),
@PrecioEquivalente DECIMAL(10,2)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

SET @Exito = '0 = Monto añadido.'

IF (EXISTS(SELECT Proveedor FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionDetalle WHERE Proveedor = @Proveedor AND idEvaluacionC = @idEvaluacionC))
BEGIN
	SET @Exito = '-1 = Este proveedor ya fue registrado.'
	GOTO Terminar
END

SET @correlativo = (SELECT MAX(idEvaluacionD) FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionDetalle WHERE idEvaluacionC = @idEvaluacionC)
SET @correlativo = ISNULL(@correlativo,0) + 1

BEGIN TRAN
BEGIN TRY
	INSERT INTO ReportesApp_Logistica_EvaluacionOfertas_EvaluacionDetalle(idEvaluacionC,idEvaluacionD,Proveedor,PrecioUnitario,FormaPago,PrecioTotal,
	CostoFinanciero,PrecioEquivalente)
	VALUES(@idEvaluacionC, @correlativo, @Proveedor, @PrecioUnitario, @FormaPago, @PrecioTotal, @CostoFinanciero, @PrecioEquivalente)

	DECLARE @PrecioMinimo DECIMAL(10,2) = (SELECT MIN(PrecioEquivalente) FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionDetalle WHERE idEvaluacionC = @idEvaluacionC)

	UPDATE ED
	SET ED.PuntajeEconomico = @PrecioMinimo / ED.PrecioEquivalente * 100, ValorMinimo = @PrecioMinimo
	FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionDetalle ED
	WHERE ED.idEvaluacionC = @idEvaluacionC

	UPDATE ED
	SET ED.PuntajeTotal = (ED.PuntajeEconomico * 0.4) + (ISNULL(ED.PuntajeTecnico,0) * 0.6)
	FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionDetalle ED
	WHERE ED.idEvaluacionC = @idEvaluacionC
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

----------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 07/03/2025
-- Description:	LISTAR DETALLE EVALUACION
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Logistica_EvaluacionOfertas_DetalleListar]
@idEvaluacionC INT
AS
BEGIN
	SELECT idEvaluacionC, idEvaluacionD AS 'NRO', Proveedor AS 'PROVEEDOR', PrecioUnitario AS 'PRECIO_UNITARIO', FormaPago AS 'FORMA_PAGO',
	PrecioTotal AS 'PRECIO_TOTAL', CostoFinanciero AS 'COSTO_FINANCIERO', PrecioEquivalente AS 'PRECIO_EQUIVALENTE', ValorMinimo AS 'VALOR_MINIMO',
	PuntajeEconomico AS 'PUNTAJE_ECONOMICO', PuntajeTecnico AS 'PUNTAJE_TECNICO', PuntajeTotal AS 'PUNTAJE_TOTAL'
	FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionDetalle
	WHERE idEvaluacionC = @idEvaluacionC
	ORDER BY idEvaluacionD DESC
END

----------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 12/03/2025
-- Description:	EXPORTAR EVALUACION
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Logistica_EvaluacionOfertas_ExportarExcel]
@idEvaluacionC INT
AS
BEGIN
	SELECT 1 AS 'NRO', 'CÓDIGO: ' AS ' ', EC.Codigo AS 'CUADRO COMPARATIVO DE OFERTAS', ' ' AS ' ', ' ' AS '  ', ' ' AS ' ', ' ' AS ' '
	FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionCabecera EC
	WHERE EC.idEvaluacionC = @idEvaluacionC
	UNION 
	SELECT 2 AS 'NRO', 'FECHA: ' AS ' ', CONVERT(VARCHAR,EC.FechaCreacion,103) AS 'CUADRO COMPARATIVO DE OFERTAS', 'GESTOR COMPRAS: ' AS ' ', EC.UsuarioCreacion AS '  ', ' ' AS ' ', ' ' AS ' '
	FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionCabecera EC
	WHERE EC.idEvaluacionC = @idEvaluacionC
	UNION
	SELECT 3 AS 'NRO', ' ' AS ' ', ' ' AS 'CUADRO COMPARATIVO DE OFERTAS', ' ' AS ' ', ' ' AS '  ', ' ' AS ' ', ' ' AS ' '
	FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionCabecera EC
	WHERE EC.idEvaluacionC = @idEvaluacionC
	UNION
	SELECT 4 AS 'NRO', ' ' AS ' ', 'SERVICIO SOLICITADO' AS 'CUADRO COMPARATIVO DE OFERTAS', ' ' AS ' ', ' ' AS '  ', ' ' AS ' ', ' ' AS ' '
	FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionCabecera EC
	WHERE EC.idEvaluacionC = @idEvaluacionC
	UNION
	SELECT 5 AS 'NRO', 'SERVICIO: ' AS ' ', EC.Servicio AS 'CUADRO COMPARATIVO DE OFERTAS', 'UNIDAD: ' AS ' ', EC.Unidad AS ' ', 'CANTIDAD: ' AS ' ', CONVERT(VARCHAR,EC.Cantidad) AS ' '
	FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionCabecera EC
	WHERE EC.idEvaluacionC = @idEvaluacionC
	UNION
	SELECT 6 AS 'NRO', 'PRECIO UNITARIO: ' AS ' ', CONVERT(VARCHAR,EC.PrecioUnitario) AS 'CUADRO COMPARATIVO DE OFERTAS', 'PRECIO TOTAL: ' AS ' ', CONVERT(VARCHAR,EC.PrecioTotal) AS '  ', ' ' AS ' ', ' ' AS ' '
	FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionCabecera EC
	WHERE EC.idEvaluacionC = @idEvaluacionC
	UNION
	SELECT 7 AS 'NRO', ' ' AS ' ', ' ' AS 'CUADRO COMPARATIVO DE OFERTAS', ' ' AS ' ', ' ' AS '  ', ' ' AS ' ', ' ' AS ' '
	FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionCabecera EC
	WHERE EC.idEvaluacionC = @idEvaluacionC
	UNION
	SELECT 8 AS 'NRO', ' ' AS ' ', 'AHORRO' AS 'CUADRO COMPARATIVO DE OFERTAS', ' ' AS ' ', ' ' AS '  ', ' ' AS ' ', ' ' AS ' '
	FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionCabecera EC
	WHERE EC.idEvaluacionC = @idEvaluacionC
	UNION
	SELECT 9 AS 'NRO', 'VALOR MÍNIMO: ' AS ' ', CONVERT(VARCHAR,EC.ValorMinimo) AS 'CUADRO COMPARATIVO DE OFERTAS', 'PROMEDIO OFERTA: ' AS ' ', CONVERT(VARCHAR,EC.PromedioOferta) AS '  ', ' ' AS ' ', ' ' AS ' '
	FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionCabecera EC
	WHERE EC.idEvaluacionC = @idEvaluacionC
	UNION
	SELECT 10 AS 'NRO', 'DESCUENTO: ' AS ' ', CONVERT(VARCHAR,EC.Descuento) + '%' AS 'CUADRO COMPARATIVO DE OFERTAS', 'IMPORTE AHORRADO: ' AS ' ', CONVERT(VARCHAR,EC.ImporteAhorrado) AS '  ', 'PORCENTAJE AHORRO: ' AS ' ', CONVERT(VARCHAR,EC.PorcentajeAhorro) + '%' AS ' '
	FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionCabecera EC
	WHERE EC.idEvaluacionC = @idEvaluacionC
	UNION
	SELECT 11 AS 'NRO', 'CONCLUSIONES: ' AS ' ', EC.Conclusiones AS 'CUADRO COMPARATIVO DE OFERTAS', ' ' AS ' ', ' ' AS '  ', ' ' AS ' ', ' ' AS ' '
	FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionCabecera EC
	WHERE EC.idEvaluacionC = @idEvaluacionC

	SELECT idEvaluacionD AS 'NRO', Proveedor AS 'PROVEEDOR', PrecioUnitario AS 'PRECIO UNITARIO', FormaPago AS 'FORMA PAGO',
	PrecioTotal AS 'PRECIO TOTAL', CostoFinanciero AS 'COSTO FINANCIERO', PrecioEquivalente AS 'PRECIO EQUIVALENTE',
	PuntajeEconomico AS 'PUNTAJE ECONOMICO', PuntajeTecnico AS 'PUNTAJE TECNICO', PuntajeTotal AS 'PUNTAJE TOTAL'
	FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionDetalle
	WHERE idEvaluacionC = @idEvaluacionC
	ORDER BY idEvaluacionD ASC
END

----------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 07/03/2025
-- Description:	EDITAR DETALLE EVALUACION
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Logistica_EvaluacionOfertas_DetalleEditar]
@Opcion INT,
@idEvaluacionC INT,
@idEvaluacionD INT,
@Puntaje DECIMAL(10,2)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Modificado correctamente.'

BEGIN TRAN
BEGIN TRY
	DECLARE @PrecioMinimo DECIMAL(10,2)

	IF (@Opcion = 1) BEGIN  -- ASIGNAR MÍNIMO
		SET @PrecioMinimo = (SELECT PrecioEquivalente FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionDetalle
							 WHERE idEvaluacionC = @idEvaluacionC AND idEvaluacionD = @idEvaluacionD)
		
		UPDATE ED
		SET ED.PuntajeEconomico = @PrecioMinimo / ED.PrecioEquivalente * 100, ED.ValorMinimo = @PrecioMinimo
		FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionDetalle ED
		WHERE ED.idEvaluacionC = @idEvaluacionC

		UPDATE ED
		SET ED.PuntajeTotal = (ED.PuntajeEconomico * 0.4) + (ISNULL(ED.PuntajeTecnico,0) * 0.6)
		FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionDetalle ED
		WHERE ED.idEvaluacionC = @idEvaluacionC
	END

	IF (@Opcion = 2) BEGIN  -- ASIGNAR PUNTAJE TECNICO
		UPDATE ReportesApp_Logistica_EvaluacionOfertas_EvaluacionDetalle
		SET PuntajeTecnico = @Puntaje, PuntajeTotal = (PuntajeEconomico * 0.4) + (@Puntaje * 0.6)
		WHERE idEvaluacionC = @idEvaluacionC AND idEvaluacionD = @idEvaluacionD 
	END

	IF (@Opcion = 3) BEGIN  -- ELIMINAR OFERTA
		DELETE FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionDetalle
		WHERE idEvaluacionC = @idEvaluacionC AND idEvaluacionD = @idEvaluacionD 
		
		SET @PrecioMinimo = (SELECT TOP(1) ValorMinimo FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionDetalle WHERE idEvaluacionC = @idEvaluacionC)

		UPDATE ED
		SET ED.PuntajeEconomico = @PrecioMinimo / ED.PrecioEquivalente * 100, ED.ValorMinimo = @PrecioMinimo
		FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionDetalle ED
		WHERE ED.idEvaluacionC = @idEvaluacionC

		UPDATE ED
		SET ED.PuntajeTotal = (ED.PuntajeEconomico * 0.4) + (ISNULL(ED.PuntajeTecnico,0) * 0.6) 
		FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionDetalle ED
		WHERE ED.idEvaluacionC = @idEvaluacionC

		UPDATE ReportesApp_Logistica_EvaluacionOfertas_EvaluacionDetalle
		SET idEvaluacionD = idEvaluacionD - 1
		WHERE idEvaluacionD > @idEvaluacionD AND idEvaluacionC = @idEvaluacionC
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
-- Create date: 07/03/2025
-- Description:	INSERTAR EVALUACION CABECERA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Logistica_EvaluacionOfertas_InsertarEvaluacion]
@Opcion INT,
@idEvaluacionC INT,
@Servicio VARCHAR(500),
@Unidad VARCHAR(10),
@Cantidad DECIMAL(10,2),
@PrecioUnitario DECIMAL(10,2),
@PrecioTotal DECIMAL(10,2),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

SET @correlativo = (SELECT MAX(idEvaluacionC) FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionCabecera)
SET @correlativo = ISNULL(@correlativo,0) + 1

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- INSERTAR
		DECLARE @Numero INT
		DECLARE @Codigo VARCHAR(50)

		SET @Numero = (SELECT COUNT(idEvaluacionC) FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionCabecera WHERE YEAR(FechaCreacion) = YEAR(GETDATE()))
		SET @Numero = ISNULL(@Numero,0) + 1
		SET @Codigo = 'CT-01-'+SUBSTRING(CONVERT(VARCHAR,YEAR(GETDATE())),3,2)+'-'+CONVERT(VARCHAR,RIGHT('0000'+LTRIM(RTRIM(@Numero)),4))

		INSERT INTO ReportesApp_Logistica_EvaluacionOfertas_EvaluacionCabecera(idEvaluacionC,Codigo,Servicio,Unidad,Cantidad,PrecioUnitario,PrecioTotal,Descuento,UsuarioCreacion,FechaCreacion)
		VALUES(@correlativo,@Codigo,@Servicio,@Unidad,@Cantidad,@PrecioUnitario,@PrecioTotal,0,@Usuario,GETDATE())

		UPDATE ReportesApp_Logistica_EvaluacionOfertas_EvaluacionDetalle
		SET idEvaluacionC = @correlativo
		WHERE idEvaluacionC = 0

		DECLARE @ValorMinimo DECIMAL(10,2) = (SELECT TOP(1) ValorMinimo FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionDetalle WHERE idEvaluacionC = @correlativo)
		DECLARE @PromedioOferta DECIMAL(10,2) = (SELECT CAST(AVG(PrecioEquivalente) AS DECIMAL(10,2)) FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionDetalle
												 WHERE idEvaluacionC = @correlativo)
		DECLARE @ValorMayor DECIMAL(10,2) = (SELECT CASE WHEN @PrecioTotal > @PromedioOferta THEN @PrecioTotal ELSE @PromedioOferta END)

		UPDATE ReportesApp_Logistica_EvaluacionOfertas_EvaluacionCabecera
		SET ValorMinimo = @ValorMinimo, PromedioOferta = @PromedioOferta, ImporteFinal = @ValorMinimo, ImporteAhorrado = @ValorMayor - @ValorMinimo, 
		PorcentajeAhorro = ((@ValorMayor - @ValorMinimo) / @ValorMayor) * 100
		WHERE idEvaluacionC = @correlativo

		SET @Exito = '0 = Evaluación '+ @Codigo +' creada exitosamente.'
	END

	IF (@Opcion = 2) BEGIN		-- ACTUALIZAR
		DECLARE @ValorMinimo2 DECIMAL(10,2) = (SELECT TOP(1) ValorMinimo FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionDetalle WHERE idEvaluacionC = @idEvaluacionC)
		DECLARE @PromedioOferta2 DECIMAL(10,2) = (SELECT CAST(AVG(PrecioEquivalente) AS DECIMAL(10,2)) FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionDetalle
												 WHERE idEvaluacionC = @idEvaluacionC)
		DECLARE @ValorMayor2 DECIMAL(10,2) = (SELECT CASE WHEN @PrecioTotal > @PromedioOferta2 THEN @PrecioTotal ELSE @PromedioOferta2 END)
		DECLARE @DescuentoNegociado DECIMAL(10,2) = (SELECT @ValorMinimo2 * Descuento / 100 FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionCabecera
												     WHERE idEvaluacionC = @idEvaluacionC)

		UPDATE ReportesApp_Logistica_EvaluacionOfertas_EvaluacionCabecera
		SET ValorMinimo = @ValorMinimo2, PromedioOferta = @PromedioOferta2, ImporteFinal = @ValorMinimo2 - @DescuentoNegociado,
		ImporteAhorrado = @ValorMayor2 - (@ValorMinimo2 - @DescuentoNegociado), PorcentajeAhorro = ((@ValorMayor2 - (@ValorMinimo2 - @DescuentoNegociado)) / @ValorMayor2) * 100
		WHERE idEvaluacionC = @idEvaluacionC

		SET @Exito = '0 = Evaluación modificada exitosamente.'
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

----------------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 08/03/2025
-- Description:	LISTAR EVALUACION CABECERA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Logistica_EvaluacionOfertas_ListarEvaluaciones]
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@Codigo VARCHAR(250)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	SELECT EC.idEvaluacionC, EC.Codigo AS 'CODIGO', EC.Servicio AS 'SERVICIO', EC.Unidad AS 'UNIDAD', EC.Cantidad AS 'CANTIDAD',
	EC.PrecioUnitario AS 'PRECIO_UNITARIO', EC.PrecioTotal AS 'PRECIO_TOTAL', EC.ValorMinimo AS 'VALOR_MINIMO', EC.PromedioOferta AS 'PROMEDIO_OFERTA', 
	EC.Descuento AS 'DESCUENTO', EC.ImporteFinal AS 'IMPORTE_FINAL', EC.ImporteAhorrado AS 'IMPORTE_AHORRADO', EC.PorcentajeAhorro AS 'PORCENTAJE_AHORRO',
	EC.Conclusiones AS 'CONCLUSIONES', EC.UsuarioCreacion AS 'USUARIO_EVALUACION', EC.FechaCreacion AS 'FECHA_EVALUACION'
	FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionCabecera EC
	WHERE (EC.Codigo IS NULL OR EC.Codigo LIKE '%' + @Codigo + '%') AND (EC.FechaCreacion BETWEEN @FINICIO AND @FFIN)
	ORDER BY EC.idEvaluacionC DESC
END

-------------------------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 07/03/2025
-- Description:	EDITAR ADICIONALES EVALUACION
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Logistica_EvaluacionOfertas_AgregarAdicionales]
@Opcion INT,
@idEvaluacionC INT,
@Descuento DECIMAL(10,2),
@Conclusiones VARCHAR(350)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Modificado correctamente.'

BEGIN TRAN
BEGIN TRY
	DECLARE @PrecioMinimo DECIMAL(10,2)

	IF (@Opcion = 1) BEGIN  -- ELIMINAR EVALUACION
		DELETE FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionDetalle
		WHERE idEvaluacionC = @idEvaluacionC

		DELETE FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionCabecera
		WHERE idEvaluacionC = @idEvaluacionC
	END

	IF (@Opcion = 2) BEGIN  -- AÑADIR PORCENTAJE ADICIONAL
		DECLARE @ValorMinimo2 DECIMAL(10,2) = (SELECT ValorMinimo FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionCabecera WHERE idEvaluacionC = @idEvaluacionC)
		DECLARE @DescuentoNegociado DECIMAL(10,2) = (SELECT @ValorMinimo2 * @Descuento / 100 FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionCabecera
												     WHERE idEvaluacionC = @idEvaluacionC)
		DECLARE @ValorMayor2 DECIMAL(10,2) = (SELECT CASE WHEN PrecioTotal > PromedioOferta THEN PrecioTotal ELSE PromedioOferta END FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionCabecera
										      WHERE idEvaluacionC = @idEvaluacionC)

		UPDATE ReportesApp_Logistica_EvaluacionOfertas_EvaluacionCabecera
		SET Conclusiones = @Conclusiones, ImporteFinal = @ValorMinimo2 - @DescuentoNegociado, Descuento = @Descuento,
		ImporteAhorrado = @ValorMayor2 - (@ValorMinimo2 - @DescuentoNegociado), PorcentajeAhorro = ((@ValorMayor2 - (@ValorMinimo2 - @DescuentoNegociado)) / @ValorMayor2) * 100
		WHERE idEvaluacionC = @idEvaluacionC
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

-------------------------------------------------------------------------------------------------------------

/*
DELETE FROM ReportesApp_Logistica_EvaluacionOfertas_EvaluacionCabecera

UPDATE ReportesApp_Logistica_EvaluacionOfertas_EvaluacionDetalle
SET idEvaluacionC = 0
*/