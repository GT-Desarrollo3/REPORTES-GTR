
-- UPDATE Usuario SET Estado = 'A' WHERE Usuario = 'DEMO'

-----------------------------------------------

-- CREAR TABLA ReportesApp_Mantenimiento_FallasMecanicas_AgendaRubro Y LLENARLA

-- CREAR TABLA ReportesApp_Mantenimiento_FallasMecanicas_AgendaContactos

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 21-11-2023
-- Description:	LISTAR COMBO RUBROS
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_ListarComboRubros]
@Opcion INT
AS
BEGIN
	IF (@Opcion = 1) BEGIN  -- LISTAR CON TODOS
		SELECT idRubro, Descripcion FROM ReportesApp_Mantenimiento_FallasMecanicas_AgendaRubro
	END

	IF (@Opcion = 2) BEGIN  -- LISTAR SIN TODOS
		SELECT idRubro, Descripcion FROM ReportesApp_Mantenimiento_FallasMecanicas_AgendaRubro
		WHERE idRubro IN (1,2,3)
	END
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 21-11-2023
-- Description:	INGRESAR CONTACTOS DE FALLAS
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_InsertarContacto]
@Opcion INT,
@idContacto INT,
@Nombre VARCHAR(300),
@Contacto VARCHAR(25),
@Ubicacion VARCHAR(300),
@idRubro INT,
@Usuario VARCHAR(20)
AS
DECLARE @exito VARCHAR(MAX)	
DECLARE @correlativo INT

SET @correlativo = (SELECT MAX(idContacto) FROM ReportesApp_Mantenimiento_FallasMecanicas_AgendaContactos)
SET @correlativo = ISNULL(@correlativo,0) + 1 

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- NUEVO CONTACTO
		INSERT INTO ReportesApp_Mantenimiento_FallasMecanicas_AgendaContactos(idContacto,Nombre,Contacto,Ubicacion,idRubro,UsuarioCreacion,FechaCreacion)
		VALUES(@correlativo, @Nombre, @Contacto, @Ubicacion, @idRubro, @Usuario, GETDATE())

		SET @exito = '0 = Contacto registrado.'
	END

	IF (@Opcion = 2) BEGIN		-- ACTUALIZAR CONTACTO
		UPDATE ReportesApp_Mantenimiento_FallasMecanicas_AgendaContactos
		SET Nombre = @Nombre, Contacto = @Contacto, Ubicacion = @Ubicacion, idRubro = @idRubro, UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()
		WHERE idContacto = @idContacto

		SET @exito = '0 = Contacto actualizado.'
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

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 21-11-2023
-- Description:	LISTAR CONTACTOS
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_ListarContactos]
@idRubro INT,
@Ubicacion VARCHAR(300)
AS
BEGIN
	IF (@idRubro = 0) BEGIN
		SELECT C.idContacto, C.Nombre, C.Contacto, C.Ubicacion, R.Descripcion, C.UsuarioCreacion, C.FechaCreacion, C.UsuarioModificacion, C.FechaModificacion
		FROM ReportesApp_Mantenimiento_FallasMecanicas_AgendaContactos C
		LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_AgendaRubro R ON R.idRubro = C.idRubro
		WHERE (C.Ubicacion IS NULL OR C.Ubicacion LIKE '%' + @Ubicacion + '%')
		ORDER BY C.idContacto DESC
	END
	ELSE BEGIN
		SELECT C.idContacto, C.Nombre, C.Contacto, C.Ubicacion, R.Descripcion, C.UsuarioCreacion, C.FechaCreacion, C.UsuarioModificacion, C.FechaModificacion
		FROM ReportesApp_Mantenimiento_FallasMecanicas_AgendaContactos C
		LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_AgendaRubro R ON R.idRubro = C.idRubro
		WHERE (C.Ubicacion IS NULL OR C.Ubicacion LIKE '%' + @Ubicacion + '%') AND (C.idRubro = @idRubro)
		ORDER BY C.idContacto DESC
	END
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 21-11-2023
-- Description:	BUSCAR CONTACTOS
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_BuscarContactos]
@idContacto INT
AS
BEGIN
	SELECT C.idContacto, C.Nombre, C.Contacto, C.Ubicacion, R.Descripcion, C.UsuarioCreacion, C.FechaCreacion, C.UsuarioModificacion, C.FechaModificacion
	FROM ReportesApp_Mantenimiento_FallasMecanicas_AgendaContactos C
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_AgendaRubro R ON R.idRubro = C.idRubro
	WHERE (C.idContacto = @idContacto)
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 21-11-2023
-- Description:	BUSCAR Y ELIMINAR CONTACTOS
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_EliminarContacto]
@idContacto INT
AS
DECLARE @exito VARCHAR(MAX)	

SET @exito = '0 = Contacto eliminado.'

BEGIN TRAN
BEGIN TRY
	DELETE FROM ReportesApp_Mantenimiento_FallasMecanicas_AgendaContactos
	WHERE idContacto = @idContacto
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




/*
SELECT * FROM OP_TR_SubTipoVehiculo


INSERT INTO OP_TR_PeajeCosto (IdPeaje, Linea, TipoVehiculo, SubTipoVehiculo, Factor, Estado, UsuarioCreacion,FechaCreacion,UsuarioModificacion,FechaModificacion)
VALUES(60, 3, 2, 2, 10.67, 2,'FRUIZ',GETDATE(),'FRUIZ',GETDATE())
INSERT INTO OP_TR_PeajeCosto (IdPeaje, Linea, TipoVehiculo, SubTipoVehiculo, Factor, Estado, UsuarioCreacion,FechaCreacion,UsuarioModificacion,FechaModificacion)
VALUES(60, 4, 2, 3, 10.67, 2,'FRUIZ',GETDATE(),'FRUIZ',GETDATE())
INSERT INTO OP_TR_PeajeCosto (IdPeaje, Linea, TipoVehiculo, SubTipoVehiculo, Factor, Estado, UsuarioCreacion,FechaCreacion,UsuarioModificacion,FechaModificacion)
VALUES(60, 5, 2, 5, 10.67, 2,'FRUIZ',GETDATE(),'FRUIZ',GETDATE())
INSERT INTO OP_TR_PeajeCosto (IdPeaje, Linea, TipoVehiculo, SubTipoVehiculo, Factor, Estado, UsuarioCreacion,FechaCreacion,UsuarioModificacion,FechaModificacion)
VALUES(60, 6, 1, 8, 10.67, 2,'FRUIZ',GETDATE(),'FRUIZ',GETDATE())
INSERT INTO OP_TR_PeajeCosto (IdPeaje, Linea, TipoVehiculo, SubTipoVehiculo, Factor, Estado, UsuarioCreacion,FechaCreacion,UsuarioModificacion,FechaModificacion)
VALUES(60, 7, 3, 7, 10.67, 2,'FRUIZ',GETDATE(),'FRUIZ',GETDATE())
INSERT INTO OP_TR_PeajeCosto (IdPeaje, Linea, TipoVehiculo, SubTipoVehiculo, Factor, Estado, UsuarioCreacion,FechaCreacion,UsuarioModificacion,FechaModificacion)
VALUES(60, 8, 2, 9, 10.67, 2,'FRUIZ',GETDATE(),'FRUIZ',GETDATE())

SELECT P.IdPeaje, P.Descripcion AS PEAJE, PC.Linea, PC.TipoVehiculo,
(CASE PC.TipoVehiculo
WHEN 1 THEN 'REMOLCADOR'
WHEN 2 THEN 'SEMIRREMOLQUE'
WHEN 3 THEN 'LIVIANOS'
ELSE 'NO IDENTIFICADO'
END) AS TIPO_VEHÍCULO, PC.SubTipoVehiculo,
SV.Descripcion AS SUB_TIPO_VEHÍCULO, PC.Factor, PC.Estado
FROM OP_TR_Peaje P
LEFT JOIN OP_TR_PeajeCosto PC ON P.IdPeaje = PC.IdPeaje
LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = PC.SubTipoVehiculo
WHERE P.Estado = 2 AND P.idPeaje = 60
*/

SELECT * FROM OP_TR_PeajeCosto
WHERE idPeaje = 60