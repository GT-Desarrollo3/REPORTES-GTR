
-- CREAR TABLA ReportesApp_Mantenimiento_ControlInventario_Sillas

-- CREAR TABLA ReportesApp_Mantenimiento_ControlInventario_Areas

-------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 13-03-2025
-- Description:	LISTAR SEDE Y AREAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlInventario_ListarSedeArea]
@Opcion INT,
@Sede VARCHAR(100)
AS
BEGIN
	IF (@Opcion = 1) BEGIN  -- LISTAR SEDES
		SELECT DISTINCT Sede
		FROM ReportesApp_Mantenimiento_ControlInventario_Areas
	END

	IF (@Opcion = 2) BEGIN  -- LISTAR AREAS
		SELECT idArea, Area
		FROM ReportesApp_Mantenimiento_ControlInventario_Areas
		WHERE Sede = @Sede
	END
END

-------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 12/03/2025
-- Description:	LISTAR INVENTARIO DE SILLAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlInventario_ListarSillas]
@Trabajador VARCHAR(250),
@Sede VARCHAR(50),
@Estado VARCHAR(50)
AS
BEGIN
	IF (@Sede = 'TODAS') BEGIN
		IF (@Estado = 'TODOS') BEGIN
			SELECT idTicket AS 'TICKET', TipoActivo AS 'ACTIVO', Sede AS 'SEDE', Area AS 'AREA', idPersona,
			CASE WHEN idPersona = -1 THEN 'LIBRE' WHEN idPersona = -2 THEN 'INACTIVO' ELSE 'OCUPADO' END AS 'ESTADO', NombrePersona AS 'TRABAJADOR',
			Observacion AS 'OBSERVACION', CodigoBarras, UsuarioCreacion, FechaCreacion, UsuarioModificacion, FechaModificacion
			FROM ReportesApp_Mantenimiento_ControlInventario_Sillas
			WHERE (NombrePersona IS NULL OR NombrePersona LIKE '%' + @Trabajador + '%')
			ORDER BY idTicket DESC
		END
		IF (@Estado = 'LIBRE') BEGIN
			SELECT idTicket AS 'TICKET', TipoActivo AS 'ACTIVO', Sede AS 'SEDE', Area AS 'AREA', idPersona,
			CASE WHEN idPersona = -1 THEN 'LIBRE' WHEN idPersona = -2 THEN 'INACTIVO' ELSE 'OCUPADO' END AS 'ESTADO', NombrePersona AS 'TRABAJADOR',
			Observacion AS 'OBSERVACION', CodigoBarras, UsuarioCreacion, FechaCreacion, UsuarioModificacion, FechaModificacion
			FROM ReportesApp_Mantenimiento_ControlInventario_Sillas
			WHERE (NombrePersona IS NULL OR NombrePersona LIKE '%' + @Trabajador + '%') AND (idPersona = -1)
			ORDER BY idTicket DESC
		END
		IF (@Estado = 'OCUPADO') BEGIN
			SELECT idTicket AS 'TICKET', TipoActivo AS 'ACTIVO', Sede AS 'SEDE', Area AS 'AREA', idPersona,
			CASE WHEN idPersona = -1 THEN 'LIBRE' WHEN idPersona = -2 THEN 'INACTIVO' ELSE 'OCUPADO' END AS 'ESTADO', NombrePersona AS 'TRABAJADOR',
			Observacion AS 'OBSERVACION', CodigoBarras, UsuarioCreacion, FechaCreacion, UsuarioModificacion, FechaModificacion
			FROM ReportesApp_Mantenimiento_ControlInventario_Sillas
			WHERE (NombrePersona IS NULL OR NombrePersona LIKE '%' + @Trabajador + '%') AND (idPersona NOT IN (-1,-2)) 
			ORDER BY idTicket DESC
		END
		IF (@Estado = 'INACTIVO') BEGIN
			SELECT idTicket AS 'TICKET', TipoActivo AS 'ACTIVO', Sede AS 'SEDE', Area AS 'AREA', idPersona,
			CASE WHEN idPersona = -1 THEN 'LIBRE' WHEN idPersona = -2 THEN 'INACTIVO' ELSE 'OCUPADO' END AS 'ESTADO', NombrePersona AS 'TRABAJADOR',
			Observacion AS 'OBSERVACION', CodigoBarras, UsuarioCreacion, FechaCreacion, UsuarioModificacion, FechaModificacion
			FROM ReportesApp_Mantenimiento_ControlInventario_Sillas
			WHERE (NombrePersona IS NULL OR NombrePersona LIKE '%' + @Trabajador + '%') AND (idPersona = -2)
			ORDER BY idTicket DESC
		END
	END
	ELSE BEGIN
		IF (@Estado = 'TODOS') BEGIN
			SELECT idTicket AS 'TICKET', TipoActivo AS 'ACTIVO', Sede AS 'SEDE', Area AS 'AREA', idPersona,
			CASE WHEN idPersona = -1 THEN 'LIBRE' WHEN idPersona = -2 THEN 'INACTIVO' ELSE 'OCUPADO' END AS 'ESTADO', NombrePersona AS 'TRABAJADOR',
			Observacion AS 'OBSERVACION', CodigoBarras, UsuarioCreacion, FechaCreacion, UsuarioModificacion, FechaModificacion
			FROM ReportesApp_Mantenimiento_ControlInventario_Sillas
			WHERE (NombrePersona IS NULL OR NombrePersona LIKE '%' + @Trabajador + '%') AND (Sede = @Sede)
			ORDER BY idTicket DESC
		END
		IF (@Estado = 'LIBRE') BEGIN
			SELECT idTicket AS 'TICKET', TipoActivo AS 'ACTIVO', Sede AS 'SEDE', Area AS 'AREA', idPersona,
			CASE WHEN idPersona = -1 THEN 'LIBRE' WHEN idPersona = -2 THEN 'INACTIVO' ELSE 'OCUPADO' END AS 'ESTADO', NombrePersona AS 'TRABAJADOR',
			Observacion AS 'OBSERVACION', CodigoBarras, UsuarioCreacion, FechaCreacion, UsuarioModificacion, FechaModificacion
			FROM ReportesApp_Mantenimiento_ControlInventario_Sillas
			WHERE (NombrePersona IS NULL OR NombrePersona LIKE '%' + @Trabajador + '%') AND (Sede = @Sede) AND (idPersona = -1)
			ORDER BY idTicket DESC
		END
		IF (@Estado = 'OCUPADO') BEGIN
			SELECT idTicket AS 'TICKET', TipoActivo AS 'ACTIVO', Sede AS 'SEDE', Area AS 'AREA', idPersona,
			CASE WHEN idPersona = -1 THEN 'LIBRE' WHEN idPersona = -2 THEN 'INACTIVO' ELSE 'OCUPADO' END AS 'ESTADO', NombrePersona AS 'TRABAJADOR',
			Observacion AS 'OBSERVACION', CodigoBarras, UsuarioCreacion, FechaCreacion, UsuarioModificacion, FechaModificacion
			FROM ReportesApp_Mantenimiento_ControlInventario_Sillas
			WHERE (NombrePersona IS NULL OR NombrePersona LIKE '%' + @Trabajador + '%') AND (Sede = @Sede) AND (idPersona NOT IN (-1,-2)) 
			ORDER BY idTicket DESC
		END
		IF (@Estado = 'INACTIVO') BEGIN
			SELECT idTicket AS 'TICKET', TipoActivo AS 'ACTIVO', Sede AS 'SEDE', Area AS 'AREA', idPersona,
			CASE WHEN idPersona = -1 THEN 'LIBRE' WHEN idPersona = -2 THEN 'INACTIVO' ELSE 'OCUPADO' END AS 'ESTADO', NombrePersona AS 'TRABAJADOR',
			Observacion AS 'OBSERVACION', CodigoBarras, UsuarioCreacion, FechaCreacion, UsuarioModificacion, FechaModificacion
			FROM ReportesApp_Mantenimiento_ControlInventario_Sillas
			WHERE (NombrePersona IS NULL OR NombrePersona LIKE '%' + @Trabajador + '%') AND (idPersona = -2)
			ORDER BY idTicket DESC
		END
	END
END

--------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 14-03-2025
-- Description:	INSERTAR INVENTARIO INCIDENCIAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlInventario_InsertarModificar]
@Opcion INT,
@idTicket INT,
@TipoActivo VARCHAR(250),
@Sede VARCHAR(250),
@Area VARCHAR(250),
@idPersona INT,
@NombrePersona VARCHAR(350),
@Observacion VARCHAR(350),
@CodigoBarras VARBINARY(MAX),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- INSERTAR INVENTARIO
		IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_ControlInventario_Sillas WHERE idTicket = @idTicket)) BEGIN
			SET @exito = '-1 = Este ticket ya fue ingresado en el inventario.'
			ROLLBACK
			GOTO Terminar
		END
		ELSE BEGIN
			INSERT INTO ReportesApp_Mantenimiento_ControlInventario_Sillas(idTicket,CodigoBarras,TipoActivo,Sede,Area,idPersona,NombrePersona,
			Observacion,UsuarioCreacion,FechaCreacion,UsuarioModificacion,FechaModificacion)
			VALUES(@idTicket,@CodigoBarras,@TipoActivo,@Sede,@Area,@idPersona,@NombrePersona,@Observacion,@Usuario,GETDATE(),@Usuario,GETDATE())

			SET @Exito = '0 = Ítem Registrado.'
		END
	END

	IF (@Opcion = 2) BEGIN		-- MODIFICAR INVENTARIO
		UPDATE ReportesApp_Mantenimiento_ControlInventario_Sillas
		SET idTicket = @idTicket, CodigoBarras = @CodigoBarras, TipoActivo = @TipoActivo, Sede = @Sede, Area = @Area, idPersona = @idPersona,
		NombrePersona = @NombrePersona, Observacion = @Observacion, UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()
		WHERE idTicket = @idTicket

		SET @Exito = '0 = Ítem Modificado.'
	END

	IF (@Opcion = 3) BEGIN		-- ELIMINAR INVENTARIO
		DELETE FROM ReportesApp_Mantenimiento_ControlInventario_Sillas
		WHERE idTicket = @idTicket

		SET @Exito = '0 = Ítem Eliminado.'
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