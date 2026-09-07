
-- CREAR TABLA ReportesApp_Operaciones_ControlItems_KitVolcan_Items

-- CREAR TABLA ReportesApp_Operaciones_ControlItems_KitVolcan_Registro

-- CREAR TABLA ReportesApp_Operaciones_ControlItems_KitVolcan_Historial

----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 30-06-2025
-- Description:	LISTAR ITEMS DE KIT VOLCAN
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_KitVolcan_ListarItems]
@Opcion INT,
@CodigoItem VARCHAR(20)
AS
BEGIN
	IF (@Opcion = 1) BEGIN		-- LISTAR ULTIMO CODIGO
		IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ControlItems_KitVolcan_Items WHERE RTRIM(CodigoItemAlmacen) = RTRIM(@CodigoItem))) BEGIN
			SELECT TOP(1) CodigoInterno AS 'CODIGO' 
			FROM ReportesApp_Operaciones_ControlItems_KitVolcan_Items
			WHERE RTRIM(CodigoItemAlmacen) = RTRIM(@CodigoItem)
			ORDER BY idItemV DESC
		END
		ELSE BEGIN
			SELECT 'N-0' AS 'CODIGO'
		END
	END
	
	IF (@Opcion = 2) BEGIN		-- LISTAR ITEMS DE KIT VOLCAN
		SELECT I.idItemV AS 'NRO', I.CodigoInterno AS 'CODIGO', I.CodigoItemAlmacen AS 'ITEM', I.Descripcion AS 'IMPLEMENTO',
		C.Descripcion AS 'CATEGORIA'
		FROM ReportesApp_Operaciones_ControlItems_KitVolcan_Items I
		LEFT JOIN ReportesApp_Mantenimiento_Herramientas_Categoria C ON C.IDCategoria = I.IDCategoria
		WHERE Estado = 0
		ORDER BY idItemV ASC
	END
END

-----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 30-06-2025
-- Description:	REGISTRAR Y ELIMINAR ITEMS DE VOLCAN
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_KitVolcan_RegistrarImplemento]
@Opcion INT,
@idItemV INT,
@CodigoItemAlmacen VARCHAR(20),
@CodigoInterno VARCHAR(20),
@IDCategoria INT,
@Descripcion VARCHAR(250),
@Usuario VARCHAR(20)
AS
DECLARE @correlativo INT 
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- REGISTRAR
		IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ControlItems_KitVolcan_Items WHERE CodigoItemAlmacen = @CodigoItemAlmacen AND CodigoInterno = @CodigoInterno)) BEGIN
			SET @Exito = '-1 = Este implemento ya se encuentra registrado en la lista.'
			ROLLBACK
			GOTO Terminar
		END
	
		SET @correlativo = (SELECT MAX(idItemV) FROM ReportesApp_Operaciones_ControlItems_KitVolcan_Items)
		SET @correlativo = ISNULL(@correlativo,0) + 1 
	
		INSERT INTO ReportesApp_Operaciones_ControlItems_KitVolcan_Items(idItemV,CodigoItemAlmacen,CodigoInterno,IDCategoria,Descripcion,Estado,UsuarioCrea,FechaCrea)
		SELECT @correlativo, @CodigoItemAlmacen, @CodigoInterno, @IDCategoria, @Descripcion, 0, @Usuario, GETDATE()

		SET @Exito = '0 = Implemento Registrado Correctamente.'
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR
		IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ControlItems_KitVolcan_Registro WHERE idItemV = @idItemV)) BEGIN
			SET @Exito = '-1 = No puede eliminar este implemento porque ya se encuentra asignado.'
			ROLLBACK
			GOTO Terminar
		END

		DELETE FROM ReportesApp_Operaciones_ControlItems_KitVolcan_Items
		WHERE idItemV = @idItemV

		SET @Exito = '0 = Implemento Eliminado Correctamente.'
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

-----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 30-06-2025
-- Description:	REGISTRAR Y ELIMINAR ITEMS DE KIT VOLCAN
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_KitVolcan_RegistrarEliminarItems]
@Opcion INT,
@NumeroItem INT,
@Persona INT,
@idTracto INT,
@idCarreta INT,
@Usuario VARCHAR(20)
AS
DECLARE @idKitVolcan INT 
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- REGISTRAR ITEMS
		DECLARE @TipoTracto INT = (SELECT TipoVehiculo FROM OP_TR_VEHICULO WHERE IdVehiculo = @idTracto)
		DECLARE @TipoCarreta INT = (SELECT TipoVehiculo FROM OP_TR_VEHICULO WHERE IdVehiculo = @idCarreta)

		IF (@TipoTracto = 2) BEGIN
			SET @exito = '-2 = Error al intentar programar un semirremolque en el campo de tractos o viceversa.'	
			ROLLBACK
			GOTO Terminar
		END

		IF (@TipoCarreta NOT IN (2)) BEGIN
			SET @exito = '-2 = Error al intentar programar una unidad en el campo de semirremolques o viceversa.'	
			ROLLBACK
			GOTO Terminar
		END
	
		SET @idKitVolcan = (SELECT MAX(idKitVolcan) FROM ReportesApp_Operaciones_ControlItems_KitVolcan_Registro)
		SET @idKitVolcan = ISNULL(@idKitVolcan,0) + 1 

		UPDATE ReportesApp_Operaciones_ControlItems_KitVolcan_Items
		SET Estado = 1
		WHERE idItemV = @NumeroItem
	
		INSERT INTO ReportesApp_Operaciones_ControlItems_KitVolcan_Registro(idKitVolcan,idItemV,Persona,idTracto,idCarreta,UsuarioRegistra,FHoraRegistra)
		SELECT @idKitVolcan, @NumeroItem, @Persona, @idTracto, @idCarreta, @Usuario, GETDATE()

		SET @Exito = '0 = Implementos asignados correctamente.'
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR ITEMS
		UPDATE ReportesApp_Operaciones_ControlItems_KitVolcan_Items
		SET Estado = 0
		WHERE idItemV = @NumeroItem

		INSERT INTO ReportesApp_Operaciones_ControlItems_KitVolcan_Historial(idKitVolcanH,CodigoItemAlmacen,CodigoInterno,Descripcion,Persona,idTracto,
		idCarreta,UsuarioRegistra,FHoraRegistra,UsuarioElimina,FHoraElimina)
		SELECT R.idKitVolcan, I.CodigoItemAlmacen, I.CodigoInterno, I.Descripcion, R.Persona, R.idTracto, R.idCarreta, R.UsuarioRegistra,
		R.FHoraRegistra, @Usuario, GETDATE()
		FROM ReportesApp_Operaciones_ControlItems_KitVolcan_Registro R
		LEFT JOIN ReportesApp_Operaciones_ControlItems_KitVolcan_Items I ON I.idItemV = R.idItemV
		WHERE R.idItemV = @NumeroItem AND R.idTracto = @idTracto AND R.idCarreta = @idCarreta AND R.Persona = @Persona

		DELETE FROM ReportesApp_Operaciones_ControlItems_KitVolcan_Registro
		WHERE idItemV = @NumeroItem AND idTracto = @idTracto AND idCarreta = @idCarreta AND Persona = @Persona

		SET @Exito = '0 = Implementos eliminados correctamente.'
	END

	IF (@Opcion = 3) BEGIN		-- ACTUALIZAR EMPLEADO
		DECLARE @TipoTracto2 INT = (SELECT TipoVehiculo FROM OP_TR_VEHICULO WHERE IdVehiculo = @idTracto)
		DECLARE @TipoCarreta2 INT = (SELECT TipoVehiculo FROM OP_TR_VEHICULO WHERE IdVehiculo = @idCarreta)

		IF (@TipoTracto2 = 2) BEGIN
			SET @exito = '-2 = Error al intentar programar un semirremolque en el campo de tractos o viceversa.'	
			ROLLBACK
			GOTO Terminar
		END

		IF (@TipoCarreta2 NOT IN (2)) BEGIN
			SET @exito = '-2 = Error al intentar programar una unidad en el campo de semirremolques o viceversa.'	
			ROLLBACK
			GOTO Terminar
		END

		INSERT INTO ReportesApp_Operaciones_ControlItems_KitVolcan_Historial(idKitVolcanH,CodigoItemAlmacen,CodigoInterno,Descripcion,Persona,idTracto,
		idCarreta,UsuarioRegistra,FHoraRegistra,UsuarioElimina,FHoraElimina)
		SELECT R.idKitVolcan, I.CodigoItemAlmacen, I.CodigoInterno, I.Descripcion, R.Persona, R.idTracto, R.idCarreta, R.UsuarioRegistra,
		R.FHoraRegistra, @Usuario, GETDATE()
		FROM ReportesApp_Operaciones_ControlItems_KitVolcan_Registro R
		LEFT JOIN ReportesApp_Operaciones_ControlItems_KitVolcan_Items I ON I.idItemV = R.idItemV
		WHERE R.idKitVolcan = @NumeroItem

		UPDATE ReportesApp_Operaciones_ControlItems_KitVolcan_Registro
		SET idTracto = @idTracto, idCarreta = @idCarreta, Persona = @Persona, UsuarioRegistra = @Usuario, FHoraRegistra = GETDATE()
		WHERE idKitVolcan = @NumeroItem

		SET @Exito = '0 = Entrega modificada correctamente.'
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

------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 30-06-2025
-- Description:	LISTAR REGISTROS DE KIT VOLCAN
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_KitVolcan_ListarRegistros]
@Tracto VARCHAR(50),
@Carreta VARCHAR(50),
@Empleado VARCHAR(250),
@Implemento VARCHAR(250)
AS
BEGIN
	SELECT R.idKitVolcan, R.idTracto, T.NumeroPlaca AS 'TRACTO', O1.Descripcion AS 'OPERACION_TRACTO', /*R.idCarreta, C.NumeroPlaca AS 'CARRETA',*/
	O2.Descripcion AS 'OPERACION_CARRETA', R.Persona, RTRIM(PM.NombreCompleto) AS 'EMPLEADO', R.idItemV, I.CodigoItemAlmacen AS 'COD_ALMACEN',
	I.CodigoInterno AS 'COD_INTERNO', I.Descripcion AS 'IMPLEMENTO', CT.Descripcion AS 'CATEGORIA', R.UsuarioRegistra AS 'USUARIO_ENTREGA',
	R.FHoraRegistra AS 'FECHA_ENTREGA'
	FROM ReportesApp_Operaciones_ControlItems_KitVolcan_Registro R
	LEFT JOIN ReportesApp_Operaciones_ControlItems_KitVolcan_Items I ON I.idItemV = R.idItemV
	LEFT JOIN OP_TR_Vehiculo T ON T.IdVehiculo = R.IdTracto AND T.Estado = 2
	LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC1 ON UC1.IdUnidad = T.IdVehiculo
	LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O1 ON O1.IdOperacion = UC1.IdProgramacion
	LEFT JOIN OP_TR_Vehiculo C ON C.IdVehiculo = R.idCarreta AND C.Estado = 2
	LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC2 ON UC2.IdUnidad = C.IdVehiculo
	LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O2 ON O2.IdOperacion = UC2.IdProgramacion
	LEFT JOIN PersonaMast PM ON PM.Persona = R.Persona
	LEFT JOIN ReportesApp_Mantenimiento_Herramientas_Categoria CT ON CT.IDCategoria = I.IDCategoria
	WHERE (@Tracto = '' OR T.NumeroPlaca LIKE '%' + @Tracto + '%') AND (@Carreta = '' OR C.NumeroPlaca LIKE '%' + @Carreta + '%')
	AND (@Implemento = '' OR I.Descripcion LIKE '%' + @Implemento + '%') AND (@Empleado = '' OR RTRIM(PM.NombreCompleto) LIKE '%' + @Empleado + '%')
	ORDER BY R.idKitVolcan DESC
END

--------------------------------------------------------------------------------

SELECT * FROM ReportesApp_Combustible_TicketsSurtidor  WHERE IDViaje = 443930


SELECT * FROM OP_TR_DespachoCombustible D INNER JOIN OP_TR_Viaje V ON D.Viaje = V.Codigo 
WHERE V.IDViaje = 443930

SELECT * FROM OP_TR_DespachoCombustible WHERE Viaje = '400248-X'

/*
UPDATE OP_TR_DespachoCombustible
SET Viaje = '400248-X'
WHERE Viaje = '400248'
*/

-------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------

-- CREAR TABLA ReportesApp_Operaciones_ControlItems_KitLimagas_Items

-- CREAR TABLA ReportesApp_Operaciones_ControlItems_KitLimagas_Registro

-- CREAR TABLA ReportesApp_Operaciones_ControlItems_KitLimagas_Historial

----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 23-12-2025
-- Description:	LISTAR ITEMS DE KIT LIMAGAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_KitLimagas_ListarItems]
@Opcion INT,
@CodigoItem VARCHAR(20)
AS
BEGIN
	IF (@Opcion = 1) BEGIN		-- LISTAR ULTIMO CODIGO
		IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ControlItems_KitLimagas_Items WHERE RTRIM(CodigoItemAlmacen) = RTRIM(@CodigoItem))) BEGIN
			SELECT TOP(1) CodigoInterno AS 'CODIGO' 
			FROM ReportesApp_Operaciones_ControlItems_KitLimagas_Items
			WHERE RTRIM(CodigoItemAlmacen) = RTRIM(@CodigoItem)
			ORDER BY idItemL DESC
		END
		ELSE BEGIN
			SELECT 'N-0' AS 'CODIGO'
		END
	END
	
	IF (@Opcion = 2) BEGIN		-- LISTAR ITEMS DE KIT VOLCAN DISPONIBLES
		SELECT I.idItemL AS 'NRO', I.CodigoInterno AS 'CODIGO', I.CodigoItemAlmacen AS 'ITEM', I.Descripcion AS 'IMPLEMENTO',
		C.Descripcion AS 'CATEGORIA'
		FROM ReportesApp_Operaciones_ControlItems_KitLimagas_Items I
		LEFT JOIN ReportesApp_Mantenimiento_Herramientas_Categoria C ON C.IDCategoria = I.IDCategoria
		WHERE Estado = 0
		ORDER BY idItemL ASC
	END

	IF (@Opcion = 3) BEGIN		-- LISTAR TODOS LOS ITEMS DE KIT VOLCAN
		SELECT I.CodigoInterno AS 'CODIGO', I.CodigoItemAlmacen AS 'ITEM', I.Descripcion AS 'IMPLEMENTO',
		C.Descripcion AS 'CATEGORIA'
		FROM ReportesApp_Operaciones_ControlItems_KitLimagas_Items I
		LEFT JOIN ReportesApp_Mantenimiento_Herramientas_Categoria C ON C.IDCategoria = I.IDCategoria
		ORDER BY idItemL ASC
	END
END

------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 23-12-2025
-- Description:	REGISTRAR Y ELIMINAR ITEMS DE LIMAGAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_KitLimagas_RegistrarImplemento]
@Opcion INT,
@idItemL INT,
@CodigoItemAlmacen VARCHAR(20),
@CodigoInterno VARCHAR(20),
@IDCategoria INT,
@Descripcion VARCHAR(250),
@Usuario VARCHAR(20)
AS
DECLARE @correlativo INT 
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- REGISTRAR
		IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ControlItems_KitLimagas_Items WHERE CodigoItemAlmacen = @CodigoItemAlmacen AND CodigoInterno = @CodigoInterno)) BEGIN
			SET @Exito = '-1 = Este implemento ya se encuentra registrado en la lista.'
			ROLLBACK
			GOTO Terminar
		END
	
		SET @correlativo = (SELECT MAX(idItemL) FROM ReportesApp_Operaciones_ControlItems_KitLimagas_Items)
		SET @correlativo = ISNULL(@correlativo,0) + 1 
	
		INSERT INTO ReportesApp_Operaciones_ControlItems_KitLimagas_Items(idItemL,CodigoItemAlmacen,CodigoInterno,IDCategoria,Descripcion,Estado,UsuarioCrea,FechaCrea)
		SELECT @correlativo, @CodigoItemAlmacen, @CodigoInterno, @IDCategoria, @Descripcion, 0, @Usuario, GETDATE()

		SET @Exito = '0 = Implemento Registrado Correctamente.'
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR
		IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ControlItems_KitLimagas_Registro WHERE idItemL = @idItemL)) BEGIN
			SET @Exito = '-1 = No puede eliminar este implemento porque ya se encuentra asignado.'
			ROLLBACK
			GOTO Terminar
		END

		DELETE FROM ReportesApp_Operaciones_ControlItems_KitLimagas_Items
		WHERE idItemL = @idItemL

		SET @Exito = '0 = Implemento Eliminado Correctamente.'
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

------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 23-12-2025
-- Description:	REGISTRAR Y ELIMINAR ITEMS DE KIT LIMAGAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_KitLimagas_RegistrarEliminarItems]
@Opcion INT,
@NumeroItem INT,
@Persona INT,
@idTracto INT,
@idCarreta INT,
@Usuario VARCHAR(20)
AS
DECLARE @idKitLimagas INT 
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- REGISTRAR ITEMS
		DECLARE @TipoTracto INT = (SELECT TipoVehiculo FROM OP_TR_VEHICULO WHERE IdVehiculo = @idTracto)
		DECLARE @TipoCarreta INT = (SELECT TipoVehiculo FROM OP_TR_VEHICULO WHERE IdVehiculo = @idCarreta)

		IF (@TipoTracto = 2) BEGIN
			SET @exito = '-2 = Error al intentar programar un semirremolque en el campo de tractos o viceversa.'	
			ROLLBACK
			GOTO Terminar
		END

		IF (@TipoCarreta NOT IN (2)) BEGIN
			SET @exito = '-2 = Error al intentar programar una unidad en el campo de semirremolques o viceversa.'	
			ROLLBACK
			GOTO Terminar
		END
	
		SET @idKitLimagas = (SELECT MAX(idKitLimagas) FROM ReportesApp_Operaciones_ControlItems_KitLimagas_Registro)
		SET @idKitLimagas = ISNULL(@idKitLimagas,0) + 1 

		UPDATE ReportesApp_Operaciones_ControlItems_KitLimagas_Items
		SET Estado = 1
		WHERE idItemL = @NumeroItem
	
		INSERT INTO ReportesApp_Operaciones_ControlItems_KitLimagas_Registro(idKitLimagas,idItemL,Persona,idTracto,idCarreta,UsuarioRegistra,FHoraRegistra)
		SELECT @idKitLimagas, @NumeroItem, @Persona, @idTracto, @idCarreta, @Usuario, GETDATE()

		SET @Exito = '0 = Implementos asignados correctamente.'
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR ITEMS
		UPDATE ReportesApp_Operaciones_ControlItems_KitLimagas_Items
		SET Estado = 0
		WHERE idItemL = @NumeroItem

		INSERT INTO ReportesApp_Operaciones_ControlItems_KitLimagas_Historial(idKitLimagasH,CodigoItemAlmacen,CodigoInterno,Descripcion,Persona,idTracto,
		idCarreta,UsuarioRegistra,FHoraRegistra,UsuarioElimina,FHoraElimina)
		SELECT R.idKitLimagas, I.CodigoItemAlmacen, I.CodigoInterno, I.Descripcion, R.Persona, R.idTracto, R.idCarreta, R.UsuarioRegistra,
		R.FHoraRegistra, @Usuario, GETDATE()
		FROM ReportesApp_Operaciones_ControlItems_KitLimagas_Registro R
		LEFT JOIN ReportesApp_Operaciones_ControlItems_KitLimagas_Items I ON I.idItemL = R.idItemL
		WHERE R.idItemL = @NumeroItem AND R.idTracto = @idTracto AND R.idCarreta = @idCarreta AND R.Persona = @Persona

		DELETE FROM ReportesApp_Operaciones_ControlItems_KitLimagas_Registro
		WHERE idItemL = @NumeroItem AND idTracto = @idTracto AND idCarreta = @idCarreta AND Persona = @Persona

		SET @Exito = '0 = Implementos eliminados correctamente.'
	END

	IF (@Opcion = 3) BEGIN		-- ACTUALIZAR EMPLEADO
		DECLARE @TipoTracto2 INT = (SELECT TipoVehiculo FROM OP_TR_VEHICULO WHERE IdVehiculo = @idTracto)
		DECLARE @TipoCarreta2 INT = (SELECT TipoVehiculo FROM OP_TR_VEHICULO WHERE IdVehiculo = @idCarreta)

		IF (@TipoTracto2 = 2) BEGIN
			SET @exito = '-2 = Error al intentar programar un semirremolque en el campo de tractos o viceversa.'	
			ROLLBACK
			GOTO Terminar
		END

		IF (@TipoCarreta2 NOT IN (2)) BEGIN
			SET @exito = '-2 = Error al intentar programar una unidad en el campo de semirremolques o viceversa.'	
			ROLLBACK
			GOTO Terminar
		END

		INSERT INTO ReportesApp_Operaciones_ControlItems_KitLimagas_Historial(idKitLimagasH,CodigoItemAlmacen,CodigoInterno,Descripcion,Persona,idTracto,
		idCarreta,UsuarioRegistra,FHoraRegistra,UsuarioElimina,FHoraElimina)
		SELECT R.idKitLimagas, I.CodigoItemAlmacen, I.CodigoInterno, I.Descripcion, R.Persona, R.idTracto, R.idCarreta, R.UsuarioRegistra,
		R.FHoraRegistra, @Usuario, GETDATE()
		FROM ReportesApp_Operaciones_ControlItems_KitLimagas_Registro R
		LEFT JOIN ReportesApp_Operaciones_ControlItems_KitLimagas_Items I ON I.idItemL = R.idItemL
		WHERE R.idKitLimagas = @NumeroItem

		UPDATE ReportesApp_Operaciones_ControlItems_KitLimagas_Registro
		SET idTracto = @idTracto, idCarreta = @idCarreta, Persona = @Persona, UsuarioRegistra = @Usuario, FHoraRegistra = GETDATE()
		WHERE idKitLimagas = @NumeroItem

		SET @Exito = '0 = Entrega modificada correctamente.'
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

------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 23-12-2025
-- Description:	LISTAR REGISTROS DE KIT LIMAGAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_KitLimagas_ListarRegistros]
@Tracto VARCHAR(50),
@Carreta VARCHAR(50),
@Empleado VARCHAR(250),
@Implemento VARCHAR(250)
AS
BEGIN
	SELECT R.idKitLimagas, R.idTracto, T.NumeroPlaca AS 'TRACTO', O1.Descripcion AS 'OPERACION_TRACTO', /*R.idCarreta, C.NumeroPlaca AS 'CARRETA',*/
	O2.Descripcion AS 'OPERACION_CARRETA', R.Persona, RTRIM(PM.NombreCompleto) AS 'EMPLEADO', R.idItemL, I.CodigoItemAlmacen AS 'COD_ALMACEN',
	I.CodigoInterno AS 'COD_INTERNO', I.Descripcion AS 'IMPLEMENTO', CT.Descripcion AS 'CATEGORIA', R.UsuarioRegistra AS 'USUARIO_ENTREGA',
	R.FHoraRegistra AS 'FECHA_ENTREGA'
	FROM ReportesApp_Operaciones_ControlItems_KitLimagas_Registro R
	LEFT JOIN ReportesApp_Operaciones_ControlItems_KitLimagas_Items I ON I.idItemL = R.idItemL
	LEFT JOIN OP_TR_Vehiculo T ON T.IdVehiculo = R.IdTracto AND T.Estado = 2
	LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC1 ON UC1.IdUnidad = T.IdVehiculo
	LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O1 ON O1.IdOperacion = UC1.IdProgramacion
	LEFT JOIN OP_TR_Vehiculo C ON C.IdVehiculo = R.idCarreta AND C.Estado = 2
	LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC2 ON UC2.IdUnidad = C.IdVehiculo
	LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O2 ON O2.IdOperacion = UC2.IdProgramacion
	LEFT JOIN PersonaMast PM ON PM.Persona = R.Persona
	LEFT JOIN ReportesApp_Mantenimiento_Herramientas_Categoria CT ON CT.IDCategoria = I.IDCategoria
	WHERE (@Tracto = '' OR T.NumeroPlaca LIKE '%' + @Tracto + '%') AND (@Carreta = '' OR C.NumeroPlaca LIKE '%' + @Carreta + '%')
	AND (@Implemento = '' OR I.Descripcion LIKE '%' + @Implemento + '%') AND (@Empleado = '' OR RTRIM(PM.NombreCompleto) LIKE '%' + @Empleado + '%')
	ORDER BY R.idKitLimagas DESC
END





