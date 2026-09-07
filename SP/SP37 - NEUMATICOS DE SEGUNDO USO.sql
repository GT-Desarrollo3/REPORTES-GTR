
-- CREAR TABLA ReportesApp_Neumaticos_SegundoUso_Medida Y LLENARLA

-- CREAR TABLA ReportesApp_Neumaticos_SegundoUso_Marca Y LLENARLA

-- CREAR TABLA ReportesApp_Neumaticos_SegundoUso_Disenio Y LLENARLA

-- CREAR TABLA ReportesApp_Neumaticos_SegundoUso_Reencauche
-- Y BORRAR ReportesApp_Operaciones_MaestroNeumatico_SegundoUso
-- Y ReportesApp_Neumaticos_SalidaHistorial_SegundoUso

-- CREAR TABLA ReportesApp_Neumaticos_Ingreso_SegundoUso

-- CREAR TABLA ReportesApp_Neumaticos_Salida_SegundoUso

-- CREAR TABLA ReportesApp_Neumaticos_SegundoUso_Reclamos

-- CREAR TABLA ReportesApp_Neumaticos_HistorialIngreso_SegundoUso

------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 09-04-2024
-- Description:	LISTAR NEUMÁTICOS SEGUNDO USO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Neumatico_Listar_SegundoUso]
@Opcion INT
AS
BEGIN
	IF (@Opcion = 1) BEGIN		-- LISTAR NEUMÁTICOS REENCAUCHADOS PARA ALMACÉN
		SELECT Medida AS 'MEDIDA', COUNT(idReencauche) AS 'TOTAL_NEUMATICOS'
		FROM ReportesApp_Neumaticos_SegundoUso_Reencauche
		WHERE Observacion != 'REENCAUCHADO'
		GROUP BY Medida
	END

	IF (@Opcion = 2) BEGIN		-- LISTAR MEDIDA DE NEUMÁTICOS
		SELECT idMedida, Descripcion FROM ReportesApp_Neumaticos_SegundoUso_Medida
	END

	IF (@Opcion = 3) BEGIN		-- LISTAR MARCA DE NEUMÁTICOS
		SELECT idMarca, Descripcion FROM ReportesApp_Neumaticos_SegundoUso_Marca
	END

	IF (@Opcion = 4) BEGIN		-- LISTAR MEDIDA DE NEUMÁTICOS
		SELECT '0' AS 'idMedida', 'TODAS' as 'Descripcion'
		UNION
		SELECT idMedida, Descripcion FROM ReportesApp_Neumaticos_SegundoUso_Medida
	END

	IF (@Opcion = 5) BEGIN		-- LISTAR DISEÑO DE MARCAS
		SELECT Costo, Descripcion FROM ReportesApp_Neumaticos_SegundoUso_Disenio
	END
END

------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 25-03-2024
-- Description:	REGISTRAR NEUMATICO A REENCAUCHE
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Neumatico_Maestro_SegundoUso_Registrar]
@CodNeumatico INT,
@Medida VARCHAR(150),
@Marca VARCHAR(250),
@Proveedor VARCHAR(200),
@FechaEnvio DATETIME,
@Estado CHAR(5),
@DocumentoEvaluacion VARCHAR(150),
@GRR VARCHAR(150),
@Usuario VARCHAR(20),
@Imagen VARBINARY(MAX)
AS
DECLARE @idReencauche INT 
DECLARE @Mensaje VARCHAR(MAX)

SET @Mensaje = '0 = Neumático Registrado Correctamente.'

BEGIN TRAN
BEGIN TRY
	IF (EXISTS(SELECT * FROM ReportesApp_Neumaticos_SegundoUso_Reencauche WHERE CodNeumatico = @CodNeumatico AND Observacion != 'REENCAUCHADO')) BEGIN
		SET @Mensaje = '-1 = El código ' + CONVERT(VARCHAR,@CodNeumatico) + ' ya está registrado en la lista de neumáticos.'
		ROLLBACK
		GOTO Terminar
	END
	
	SET @idReencauche = (SELECT MAX(idReencauche) FROM ReportesApp_Neumaticos_SegundoUso_Reencauche)
	SET @idReencauche = ISNULL(@idReencauche,0) + 1 
	
	INSERT INTO ReportesApp_Neumaticos_SegundoUso_Reencauche(idReencauche,CodNeumatico,Medida,Marca,Proveedor,FechaEnvio,Estado,
	DocumentoEvaluacion,GRR,Imagen,Observacion,UsuarioCrea,FechaCrea,UsuarioModif,FechaModif)
	SELECT @idReencauche, @CodNeumatico, @Medida, @Marca, @Proveedor, @FechaEnvio, @Estado, @DocumentoEvaluacion, @GRR, @Imagen,
	'PENDIENTE', @Usuario, GETDATE(), @Usuario, GETDATE()
END TRY

BEGIN CATCH
	SET @Mensaje = RTRIM(LTRIM(STR(ERROR_NUMBER()))) + '=' + ERROR_MESSAGE()
	ROLLBACK
	GOTO Terminar
END CATCH

IF @@TRANCOUNT > 0 
   COMMIT;
   --ROLLBACK
	
TERMINAR:
IF LEFT(@Mensaje,1)='-' OR cast(left(@Mensaje,1) AS INT)<0 BEGIN
	DECLARE @storedProcedureName VARCHAR(100)
	SELECT @storedProcedureName = OBJECT_NAME(@@PROCID)
	SET @Mensaje = @Mensaje + CHAR(13)+CHAR(13)+'SP Name: '+@storedProcedureName 
END

SELECT @Mensaje exito

------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 09-04-2024
-- Description:	LISTAR NEUMÁTICOS A REENCAUCHAR
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Neumatico_ListarReencauche_SegundoUso]
@Codigo VARCHAR(250),
@Medida VARCHAR(150),
@Observacion VARCHAR(150),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@Observacion = 'TODOS') BEGIN
		IF (@Medida = 'TODAS') BEGIN
			SELECT idReencauche, CodNeumatico AS 'CODIGO', CONVERT(VARCHAR,FechaEnvio,103) AS 'FECHA_ENVIO', Medida AS 'MEDIDA',
			Marca AS 'MARCA', Estado AS 'ESTADO', Proveedor AS 'PROVEEDOR', DocumentoEvaluacion AS 'DOCUMENTO_EVALUACION', GRR AS 'GUIA_REMITENTE',
			Observacion AS 'OBSERVACION', CONVERT(VARCHAR,FechaRecepcion,103) AS 'FECHA_RECEPCION', GRR2 AS 'GUIA_RECEPCION', Disenio AS 'DISEÑO',
			Costo AS 'COSTO ($)', Imagen AS 'IMAGEN', UsuarioCrea, FechaCrea, UsuarioModif, FechaModif
			FROM ReportesApp_Neumaticos_SegundoUso_Reencauche
			WHERE (@Codigo IS NULL OR CONVERT(VARCHAR,CodNeumatico) LIKE '%' + @Codigo + '%')
			AND (FechaEnvio BETWEEN @FINICIO AND @FFIN)
			ORDER BY idReencauche DESC
		END
		ELSE BEGIN
			SELECT idReencauche, CodNeumatico AS 'CODIGO', CONVERT(VARCHAR,FechaEnvio,103) AS 'FECHA_ENVIO', Medida AS 'MEDIDA',
			Marca AS 'MARCA', Estado AS 'ESTADO', Proveedor AS 'PROVEEDOR', DocumentoEvaluacion AS 'DOCUMENTO_EVALUACION', GRR AS 'GUIA_REMITENTE',
			Observacion AS 'OBSERVACION', CONVERT(VARCHAR,FechaRecepcion,103) AS 'FECHA_RECEPCION', GRR2 AS 'GUIA_RECEPCION', Disenio AS 'DISEÑO',
			Costo AS 'COSTO ($)', Imagen AS 'IMAGEN', UsuarioCrea, FechaCrea, UsuarioModif, FechaModif
			FROM ReportesApp_Neumaticos_SegundoUso_Reencauche
			WHERE (@Codigo IS NULL OR CONVERT(VARCHAR,CodNeumatico) LIKE '%' + @Codigo + '%')
			AND (FechaEnvio BETWEEN @FINICIO AND @FFIN) AND (Medida = @Medida)
			ORDER BY idReencauche DESC
		END
	END
	ELSE BEGIN
		IF (@Medida = 'TODAS') BEGIN
			SELECT idReencauche, CodNeumatico AS 'CODIGO', CONVERT(VARCHAR,FechaEnvio,103) AS 'FECHA_ENVIO', Medida AS 'MEDIDA',
			Marca AS 'MARCA', Estado AS 'ESTADO', Proveedor AS 'PROVEEDOR', DocumentoEvaluacion AS 'DOCUMENTO_EVALUACION', GRR AS 'GUIA_REMITENTE',
			Observacion AS 'OBSERVACION', CONVERT(VARCHAR,FechaRecepcion,103) AS 'FECHA_RECEPCION', GRR2 AS 'GUIA_RECEPCION', Disenio AS 'DISEÑO',
			Costo AS 'COSTO ($)', Imagen AS 'IMAGEN', UsuarioCrea, FechaCrea, UsuarioModif, FechaModif
			FROM ReportesApp_Neumaticos_SegundoUso_Reencauche
			WHERE (@Codigo IS NULL OR CONVERT(VARCHAR,CodNeumatico) LIKE '%' + @Codigo + '%')
			AND (FechaEnvio BETWEEN @FINICIO AND @FFIN) AND (Observacion = @Observacion)
			ORDER BY idReencauche DESC
		END
		ELSE BEGIN
			SELECT idReencauche, CodNeumatico AS 'CODIGO', CONVERT(VARCHAR,FechaEnvio,103) AS 'FECHA_ENVIO', Medida AS 'MEDIDA',
			Marca AS 'MARCA', Estado AS 'ESTADO', Proveedor AS 'PROVEEDOR', DocumentoEvaluacion AS 'DOCUMENTO_EVALUACION', GRR AS 'GUIA_REMITENTE',
			Observacion AS 'OBSERVACION', CONVERT(VARCHAR,FechaRecepcion,103) AS 'FECHA_RECEPCION', GRR2 AS 'GUIA_RECEPCION', Disenio AS 'DISEÑO',
			Costo AS 'COSTO ($)', Imagen AS 'IMAGEN', UsuarioCrea, FechaCrea, UsuarioModif, FechaModif
			FROM ReportesApp_Neumaticos_SegundoUso_Reencauche
			WHERE (@Codigo IS NULL OR CONVERT(VARCHAR,CodNeumatico) LIKE '%' + @Codigo + '%')
			AND (FechaEnvio BETWEEN @FINICIO AND @FFIN) AND (Observacion = @Observacion) AND (Medida = @Medida)
			ORDER BY idReencauche DESC
		END
	END
END

------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 25-03-2024
-- Description:	ELIMINAR NEUMATICO DE SEGUNDO USO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Neumatico_Maestro_SegundoUso_EliminarNeumaticos]
@idReencauche INT
AS
DECLARE @Mensaje VARCHAR(MAX)

SET @Mensaje = '0 = Estado Eliminado Correctamente.'

BEGIN TRAN
BEGIN TRY
	IF ((SELECT Observacion FROM ReportesApp_Neumaticos_SegundoUso_Reencauche WHERE idReencauche = @idReencauche) = 'REENCAUCHADO') BEGIN
		SET @Mensaje = '-1 = Este neumático no se puede quitar porque ya ha sido reencauchado.'
		ROLLBACK
		GOTO Terminar
	END

	DELETE FROM ReportesApp_Neumaticos_SegundoUso_Reencauche
	WHERE idReencauche = @idReencauche
END TRY

BEGIN CATCH
	SET @Mensaje = RTRIM(LTRIM(STR(ERROR_NUMBER()))) + '=' + ERROR_MESSAGE()
	ROLLBACK
	GOTO Terminar
END CATCH

IF @@TRANCOUNT > 0 
   COMMIT;
   --ROLLBACK
	
TERMINAR:
IF LEFT(@Mensaje,1)='-' OR cast(left(@Mensaje,1) AS INT)<0 BEGIN
	DECLARE @storedProcedureName VARCHAR(100)
	SELECT @storedProcedureName = OBJECT_NAME(@@PROCID)
	SET @Mensaje = @Mensaje + CHAR(13)+CHAR(13)+'SP Name: '+@storedProcedureName 
END

SELECT @Mensaje exito

------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 10-04-2024
-- Description:	INSERTAR NEUMÁTICOS REENCAUCHADOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Neumatico_SegundoUso_InsertarReencauchados]
@idReencauche INT,
@FechaRecepcion DATETIME,
@Estado VARCHAR(150),
@GRR VARCHAR(150),
@Disenio VARCHAR(150),
@Costo DECIMAL(10,2),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX) = '0 = Estado Actualizado Correctamente.'

BEGIN TRAN
BEGIN TRY
	UPDATE ReportesApp_Neumaticos_SegundoUso_Disenio
	SET Costo = @Costo
	WHERE Descripcion = @Disenio

	DECLARE @Medida VARCHAR(150) = (SELECT Medida FROM ReportesApp_Neumaticos_SegundoUso_Reencauche WHERE idReencauche = @idReencauche)
	--DECLARE @CantidadIngresada INT = (SELECT CantidadIngreso FROM ReportesApp_Neumaticos_Ingreso_SegundoUso WHERE Medida = @Medida AND LTRIM(RTRIM(GRR)) = LTRIM(RTRIM(@GRR)))
	DECLARE @NeumaticosIngresados INT = (SELECT COUNT(*) FROM ReportesApp_Neumaticos_SegundoUso_Reencauche WHERE Observacion = 'REENCAUCHADO'
										AND LTRIM(RTRIM(GRR2)) = LTRIM(RTRIM(@GRR)) AND (Medida = @Medida))

	/*
	IF ((@NeumaticosIngresados >= @CantidadIngresada) AND (@Estado = 'REENCAUCHADO')) BEGIN
		SET @Exito = '-1 = No puede exceder la cantidad de neumáticos ' + @Medida + ' ingresados en la guía ' + @GRR + ' - TOTAL: ' + CONVERT(VARCHAR,@CantidadIngresada)
		ROLLBACK
		GOTO Terminar
	END
	*/

	UPDATE ReportesApp_Neumaticos_SegundoUso_Reencauche
	SET Observacion = @Estado, FechaRecepcion = @FechaRecepcion, GRR2 = @GRR, Disenio = @Disenio, Costo = @Costo, UsuarioModif = @Usuario, FechaModif = GETDATE()
	WHERE idReencauche = @idReencauche

	IF (@Estado = 'PENDIENTE' OR @Estado = 'DE BAJA') BEGIN
		UPDATE ReportesApp_Neumaticos_SegundoUso_Reencauche
		SET GRR2 = NULL, FechaRecepcion = NULL, Disenio = NULL, Costo = NULL
		WHERE idReencauche = @idReencauche
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

--------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 09-04-2024
-- Description:	FILTRAR NEUMATICO DE ALMACEN
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Neumatico_FiltrarNeumatico_SegundoUso]
@CodigoNeu VARCHAR(100)
AS
BEGIN
	SELECT Item AS 'CODIGO', DescripcionLocal AS 'NEUMATICO'
	FROM WH_ItemMast
	WHERE LTRIM(RTRIM(Item)) LIKE '%' + @CodigoNeu + '%' AND Estado = 'A'
END

--------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 30-03-2024
-- Description:	REGISTRAR INGRESO DE SEGUNDO USO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Neumatico_RegistrarIngreso_SegundoUso]
@GRR VARCHAR(150),
@FechaIngreso DATETIME,
@Cantidad INT,
@Item VARCHAR(100),
@UsuarioCrea VARCHAR(30)
AS
DECLARE @Exito VARCHAR(MAX) = '0 = Ingreso Registrado Correctamente'
DECLARE @idIngresoNeu INT
DECLARE @idRegistro INT

BEGIN TRAN
BEGIN TRY
	SET @idRegistro = (SELECT MAX(idRegistro) FROM ReportesApp_Neumaticos_RegistroIngreso_SegundoUso)
	SET @idRegistro = ISNULL(@idRegistro,0) + 1

	SET @idIngresoNeu = (SELECT MAX(idIngresoNeu) FROM ReportesApp_Neumaticos_Ingreso_SegundoUso)
	SET @idIngresoNeu = ISNULL(@idIngresoNeu,0) + 1

	INSERT INTO ReportesApp_Neumaticos_RegistroIngreso_SegundoUso(idRegistro,CodigoNeu,GRR,FechaIngreso,CantidadIngreso,UsuarioCrea)
	SELECT @idRegistro, LTRIM(RTRIM(@Item)), @GRR, @FechaIngreso, @Cantidad, @UsuarioCrea

	IF (EXISTS(SELECT * FROM ReportesApp_Neumaticos_Ingreso_SegundoUso WHERE LTRIM(RTRIM(CodigoNeu)) = LTRIM(RTRIM(@Item)))) BEGIN
		UPDATE ReportesApp_Neumaticos_Ingreso_SegundoUso
		SET Cantidad = Cantidad + @Cantidad
		WHERE LTRIM(RTRIM(CodigoNeu)) = LTRIM(RTRIM(@Item))
	END
	ELSE BEGIN
		INSERT INTO ReportesApp_Neumaticos_Ingreso_SegundoUso (idIngresoNeu, CodigoNeu, Cantidad)
		VALUES(@idIngresoNeu, @Item, @Cantidad)
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

SELECT @Exito 'Mensaje'

------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		SEM CHAVEZ
-- Create date: 01-04-2024
-- Description:	LISTAR INGRESO DE NEUMATICOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Neumatico_ListarIngreso_SegundoUso]
@Opcion INT,
@Nombre VARCHAR(150),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@Opcion = 1) BEGIN		-- LISTAR INGRESOS
		SELECT I.idIngresoNeu, LTRIM(RTRIM(I.CodigoNeu)) AS 'ITEM', IM.DescripcionLocal AS 'NEUMÁTICO', I.Cantidad AS 'CANTIDAD'
		FROM ReportesApp_Neumaticos_Ingreso_SegundoUso I
		LEFT JOIN WH_ItemMast IM ON LTRIM(RTRIM(IM.Item)) = LTRIM(RTRIM(I.CodigoNeu))
		WHERE (IM.DescripcionLocal IS NULL OR IM.DescripcionLocal LIKE '%' + @Nombre + '%')
		ORDER BY I.idIngresoNeu DESC
	END

	IF (@Opcion = 2) BEGIN		-- LISTAR SALIDAS
		SELECT TOP(50) S.idSalida, S.idIngresoNeu, S.FechaSalida AS 'FECHA_SALIDA', LTRIM(RTRIM(I.CodigoNeu)) AS 'ITEM',
		IM.DescripcionLocal AS 'NEUMÁTICO', S.CantidadIngreso AS 'CANTIDAD', S.OrdenTrabajo AS 'OT', OT.MaquinaCodigo AS 'PLACA',
		ISNULL(LTRIM(RTRIM(P.NombreCompleto)),'-') AS 'MECANICO', S.UsuarioCrea AS 'USUARIO_CREA'
		FROM ReportesApp_Neumaticos_Salida_SegundoUso S
		LEFT JOIN ReportesApp_Neumaticos_Ingreso_SegundoUso I ON I.idIngresoNeu = S.idIngresoNeu
		LEFT JOIN WH_ItemMast IM ON LTRIM(RTRIM(IM.Item)) = LTRIM(RTRIM(I.CodigoNeu))
		LEFT JOIN ME_OrdenTrabajo OT ON LTRIM(RTRIM(OT.NumeroOrden)) = LTRIM(RTRIM(S.OrdenTrabajo))
		LEFT JOIN PersonaMast P ON P.Persona = OT.PersonaAsignada
		WHERE (OT.MaquinaCodigo IS NULL OR OT.MaquinaCodigo LIKE '%' + @Nombre + '%') AND (S.FechaSalida BETWEEN @FINICIO AND @FFIN)
		ORDER BY S.FechaSalida DESC
	END
END

------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 13-04-2024
-- Description:	LISTAR OTS PROGRAMADAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Neumatico_ListarOT_SegundoUso]
@NumeroOrden VARCHAR(150)
AS
BEGIN
	SELECT TOP(25) ISNULL(OT.PersonaAsignada,-1), OT.NumeroOrden AS 'OT', OT.Descripcion AS 'DESCRIPCION', OT.MaquinaCodigo AS 'PLACA',
	ISNULL(P.NombreCompleto,'-') AS 'MECANICO'
	FROM ME_OrdenTrabajo OT
	LEFT JOIN PersonaMast P ON P.Persona = OT.PersonaAsignada
	WHERE (OT.CompaniaSocio = '10000000') AND (OT.Estado = 'PG' OR OT.Estado = 'CO') AND (OT.NumeroOrden LIKE '%' + @NumeroOrden + '%')
END

------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 30-03-2024
-- Description:	REGISTRAR SALIDA DE NEUMÁTICOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Neumatico_RegistrarSalida_SegundoUso] 
@idIngreso INT,
@OT VARCHAR(20),
@Cantidad INT,
@FechaSalida DATETIME,
@UsuarioCrea VARCHAR(30)
AS
DECLARE @Exito VARCHAR(MAX) = '0 = Salida Registrada Correctamente.'
DECLARE @idSalida INT

BEGIN TRAN
BEGIN TRY
	IF (@Cantidad > (SELECT Cantidad FROM ReportesApp_Neumaticos_Ingreso_SegundoUso WHERE idIngresoNeu = @idIngreso)) BEGIN
		SET @exito = '-1 = No puede retirar una cantidad mayor a la ingresada.'
		ROLLBACK
		GOTO Terminar
	END
	
	DECLARE @CodigoNeu VARCHAR(100) = (SELECT CodigoNeu FROM ReportesApp_Neumaticos_Ingreso_SegundoUso WHERE idIngresoNeu = @idIngreso)

	IF (EXISTS(SELECT * FROM ReportesApp_Neumaticos_Salida_SegundoUso WHERE LTRIM(RTRIM(OrdenTrabajo)) = LTRIM(RTRIM(@OT)) AND
	LTRIM(RTRIM(CodigoNeu)) = LTRIM(RTRIM(@CodigoNeu)))) BEGIN
		SET @exito = '-2 = No puede volver a registrar un ítem que ya ha sido registrado en una OT.'
		ROLLBACK
		GOTO Terminar
	END

	SET @idSalida = (SELECT MAX(idSalida) FROM ReportesApp_Neumaticos_Salida_SegundoUso)
	SET @idSalida = ISNULL(@idSalida,0) + 1
	
	/*
	IF (EXISTS(SELECT * FROM ReportesApp_Neumaticos_Salida_SegundoUso WHERE LTRIM(RTRIM(OrdenTrabajo)) = LTRIM(RTRIM(@OT)) AND idIngresoNeu = @idIngreso)) BEGIN
		UPDATE ReportesApp_Neumaticos_Salida_SegundoUso
		SET CantidadIngreso = CantidadIngreso + @Cantidad, FechaSalida = @FechaSalida, UsuarioCrea = @UsuarioCrea
		WHERE LTRIM(RTRIM(OrdenTrabajo)) = LTRIM(RTRIM(@OT)) AND idIngresoNeu = @idIngreso
	END
	*/

	INSERT INTO ReportesApp_Neumaticos_Salida_SegundoUso(idSalida,idIngresoNeu,OrdenTrabajo,CantidadIngreso,FechaSalida,UsuarioCrea,CodigoNeu)
	SELECT @idSalida, @idIngreso, @OT, @Cantidad, @FechaSalida, @UsuarioCrea, @CodigoNeu

	UPDATE ReportesApp_Neumaticos_Ingreso_SegundoUso
	SET Cantidad = Cantidad - @Cantidad
	WHERE idIngresoNeu = @idIngreso
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

SELECT @Exito 'Mensaje'

------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 30-03-2024
-- Description:	ANULAR SALIDA DE NEUMÁTICOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Neumatico_AnularSalida_SegundoUso] 
@idRegistro INT,
@Usuario VARCHAR(30)
AS
DECLARE @Exito VARCHAR(MAX) = '0 = Salida Anulada Correctamente.'

BEGIN TRAN
BEGIN TRY
	DECLARE @Item VARCHAR(100) = (SELECT LTRIM(RTRIM(CodigoNeu)) FROM ReportesApp_Neumaticos_RegistroIngreso_SegundoUso WHERE idRegistro = @idRegistro)
	--DECLARE @idIngresoNeu INT = (SELECT idIngresoNeu FROM ReportesApp_Neumaticos_Ingreso_SegundoUso WHERE LTRIM(RTRIM(CodigoNeu)) = @Item)
	DECLARE @CantidadQuitar INT = (SELECT CantidadIngreso FROM ReportesApp_Neumaticos_RegistroIngreso_SegundoUso WHERE idRegistro = @idRegistro)

	UPDATE ReportesApp_Neumaticos_Ingreso_SegundoUso
	SET Cantidad = Cantidad - @CantidadQuitar
	WHERE LTRIM(RTRIM(CodigoNeu)) = @Item

	INSERT INTO ReportesApp_Neumaticos_HistorialIngreso_SegundoUso (idRegistroH, CodigoNeu, GRR, FechaIngreso, CantidadIngreso, UsuarioElimina, FechaElimina)
	SELECT idRegistro, CodigoNeu, GRR, FechaIngreso, CantidadIngreso, UsuarioCrea, GETDATE()
	FROM ReportesApp_Neumaticos_RegistroIngreso_SegundoUso
	WHERE idRegistro = @idRegistro 

	DELETE FROM ReportesApp_Neumaticos_RegistroIngreso_SegundoUso
	WHERE idRegistro = @idRegistro

	/*
	IF ((SELECT Cantidad FROM ReportesApp_Neumaticos_Ingreso_SegundoUso WHERE LTRIM(RTRIM(CodigoNeu)) = @Item) = 0) BEGIN
		DELETE FROM ReportesApp_Neumaticos_Ingreso_SegundoUso
		WHERE LTRIM(RTRIM(CodigoNeu)) = @Item
	END
	*/
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

SELECT @Exito exito

------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 08-04-2024
-- Description:	REGISTRAR RETORNO DE NEUMÁTICOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Neumatico_RegistrarRetorno_SegundoUso] 
@idSalida INT,
@idIngresoNeu INT,
@Cantidad INT,
@Usuario VARCHAR(30)
AS
DECLARE @Exito VARCHAR(MAX) = '0 = Los neumáticos han sido devueltos.'

BEGIN TRAN
BEGIN TRY
	IF (@Cantidad > (SELECT CantidadIngreso FROM ReportesApp_Neumaticos_Salida_SegundoUso WHERE idSalida = @idSalida)) BEGIN
		SET @exito = '-1 = No puede devolver una cantidad mayor a la ingresada.'
		ROLLBACK
		GOTO Terminar
	END
	
	UPDATE ReportesApp_Neumaticos_Ingreso_SegundoUso
	SET Cantidad = Cantidad + @Cantidad
	WHERE idIngresoNeu = @idIngresoNeu
	
	UPDATE ReportesApp_Neumaticos_Salida_SegundoUso
	SET CantidadIngreso = CantidadIngreso - @Cantidad
	WHERE idSalida = @idSalida

	IF ((SELECT CantidadIngreso FROM ReportesApp_Neumaticos_Salida_SegundoUso WHERE idSalida = @idSalida) = 0) BEGIN
		DELETE FROM ReportesApp_Neumaticos_Salida_SegundoUso
		WHERE idSalida = @idSalida
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

SELECT @Exito exito

------------------------------------------------------------------------
------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 23-04-2024
-- Description:	REGISTRAR RECLAMO DE NEUMÁTICOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Neumatico_RegistrarReclamo_SegundoUso] 
@idRegistro INT,
@Motivo VARCHAR(250),
@FechaReclamo DATETIME,
@CantidadReclamo INT,
@GRReclamo VARCHAR(150),
@Usuario VARCHAR(30)
AS
DECLARE @Exito VARCHAR(MAX) = '0 = El reclamo ha sido registrado.'
DECLARE @idReclamo INT

BEGIN TRAN
BEGIN TRY
	IF (@CantidadReclamo > (SELECT CantidadIngreso FROM ReportesApp_Neumaticos_RegistroIngreso_SegundoUso WHERE idRegistro = @idRegistro)) BEGIN
		SET @exito = '-1 = No puede registrar una cantidad mayor a la del ingreso.'
		ROLLBACK
		GOTO Terminar
	END
	
	SET @idReclamo = (SELECT MAX(idReclamo) FROM ReportesApp_Neumaticos_SegundoUso_Reclamos)
	SET @idReclamo = ISNULL(@idReclamo,0) + 1

	INSERT INTO ReportesApp_Neumaticos_SegundoUso_Reclamos(idReclamo,idRegistro,Motivo,FechaReclamo,CantidadReclamo,GRReclamo,
	Estado,UsuarioCrea,FechaCrea,UsuarioModifica,FechaModifica)
	SELECT @idReclamo, @idRegistro, @Motivo, @FechaReclamo, @CantidadReclamo, @GRReclamo, 'PENDIENTE', @Usuario, GETDATE(), @Usuario, GETDATE()

	DECLARE @Item VARCHAR(100) = (SELECT LTRIM(RTRIM(CodigoNeu)) FROM ReportesApp_Neumaticos_RegistroIngreso_SegundoUso WHERE idRegistro = @idRegistro)

	UPDATE ReportesApp_Neumaticos_RegistroIngreso_SegundoUso
	SET CantidadIngreso = CantidadIngreso - @CantidadReclamo
	WHERE idRegistro = @idRegistro
	
	UPDATE ReportesApp_Neumaticos_Ingreso_SegundoUso
	SET Cantidad = Cantidad - @CantidadReclamo
	WHERE LTRIM(RTRIM(CodigoNeu)) = @Item
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

SELECT @Exito exito

--------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 18-04-2024
-- Description:	LISTAR INGRESO DE NEUMATICOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Neumatico_SegundoUso_ListarIngresos]
@Opcion INT,
@Guia VARCHAR(50),
@Neumatico VARCHAR(250),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@Opcion = 1) BEGIN		-- LISTAR INGRESOS
		SELECT I.idRegistro, LTRIM(RTRIM(I.CodigoNeu)) AS 'ITEM', IM.DescripcionLocal AS 'NEUMÁTICO', I.CantidadIngreso AS 'CANTIDAD',
		CONVERT(VARCHAR,I.FechaIngreso,103) AS 'FECHA_INGRESO', I.GRR AS 'GUIA_REMITENTE', I.UsuarioCrea AS 'USUARIO_INGRESO'
		FROM ReportesApp_Neumaticos_RegistroIngreso_SegundoUso I
		LEFT JOIN WH_ItemMast IM ON LTRIM(RTRIM(IM.Item)) = LTRIM(RTRIM(I.CodigoNeu))
		WHERE (I.GRR IS NULL OR I.GRR LIKE '%' + @Guia + '%') AND (I.FechaIngreso BETWEEN @FINICIO AND @FFIN)
		AND (IM.DescripcionLocal IS NULL OR IM.DescripcionLocal LIKE '%' + @Neumatico + '%')
		ORDER BY I.FechaIngreso DESC
	END

	IF (@Opcion = 2) BEGIN		-- LISTAR RECLAMOS
		SELECT R.idReclamo, R.idRegistro, LTRIM(RTRIM(I.CodigoNeu)) AS 'ITEM', IM.DescripcionLocal AS 'NEUMÁTICO', I.GRR AS 'GUIA_REMITENTE',
		CONVERT(VARCHAR,R.FechaReclamo,103) AS 'FECHA_RECLAMO', R.GRReclamo AS 'GUIA_RECLAMO', R.CantidadReclamo AS 'CANTIDAD',
		R.Estado AS 'ESTADO', R.UsuarioCrea, R.FechaCrea
		FROM ReportesApp_Neumaticos_SegundoUso_Reclamos R
		LEFT JOIN ReportesApp_Neumaticos_RegistroIngreso_SegundoUso I ON I.idRegistro = R.idRegistro
		LEFT JOIN WH_ItemMast IM ON LTRIM(RTRIM(IM.Item)) = LTRIM(RTRIM(I.CodigoNeu))
		WHERE (R.GRReclamo IS NULL OR R.GRReclamo LIKE '%' + @Guia + '%') AND (R.FechaReclamo BETWEEN @FINICIO AND @FFIN)
		AND (IM.DescripcionLocal IS NULL OR IM.DescripcionLocal LIKE '%' + @Neumatico + '%')
		ORDER BY R.idReclamo DESC
	END
END

--------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 23-03-2024
-- Description:	ACTUALIZAR ESTADO DE RECLAMOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Neumatico_ActualizarReclamo_SegundoUso] 
@idReclamo INT,
@Estado VARCHAR(30)
AS
DECLARE @Exito VARCHAR(MAX) = '0 = Reclamo actualizado.'

BEGIN TRAN
BEGIN TRY
	IF (@Estado = 'PENDIENTE') BEGIN
		UPDATE ReportesApp_Neumaticos_SegundoUso_Reclamos
		SET Estado = 'ATENDIDO'
		WHERE idReclamo = @idReclamo 
	END

	IF (@Estado = 'ATENDIDO') BEGIN
		UPDATE ReportesApp_Neumaticos_SegundoUso_Reclamos
		SET Estado = 'PENDIENTE'
		WHERE idReclamo = @idReclamo 
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

SELECT @Exito exito

--------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 23-03-2024
-- Description:	QUITAR RECLAMO DE NEUMÁTICOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Neumatico_QuitarReclamo_SegundoUso] 
@idRegistro INT,
@idReclamo INT
AS
DECLARE @Exito VARCHAR(MAX) = '0 = Reclamo anulado correctamente.'

BEGIN TRAN
BEGIN TRY
	DECLARE @Item VARCHAR(100) = (SELECT LTRIM(RTRIM(CodigoNeu)) FROM ReportesApp_Neumaticos_RegistroIngreso_SegundoUso WHERE idRegistro = @idRegistro)
	DECLARE @CantidadReclamo INT = (SELECT CantidadReclamo FROM ReportesApp_Neumaticos_SegundoUso_Reclamos WHERE idReclamo = @idReclamo)

	UPDATE ReportesApp_Neumaticos_RegistroIngreso_SegundoUso
	SET CantidadIngreso = CantidadIngreso + @CantidadReclamo
	WHERE idRegistro = @idRegistro
	
	UPDATE ReportesApp_Neumaticos_Ingreso_SegundoUso
	SET Cantidad = Cantidad + @CantidadReclamo
	WHERE LTRIM(RTRIM(CodigoNeu)) = @Item

	DELETE FROM ReportesApp_Neumaticos_SegundoUso_Reclamos
	WHERE idReclamo = @idReclamo
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

SELECT @Exito exito