
-- CREAR TABLA ReportesApp_Operaciones_ControlItems_BotiquinItems Y LLENARLA

-- CREAR TABLA ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Cabecera

-- CREAR TABLA ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Detalle

----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 29-04-2024
-- Description:	REGISTRAR ITEM DE BOTIQUIN
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_RegistrarItemBotiquin]
@Descripcion VARCHAR(250),
@Codigo VARCHAR(100),
@Cantidad INT,
@Duracion INT,
@Usuario VARCHAR(20)
AS
DECLARE @idItemBotiquin INT 
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Item Registrado Correctamente.'

BEGIN TRAN
BEGIN TRY
	IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ControlItems_BotiquinItems WHERE Codigo = LTRIM(RTRIM(@Codigo)))) BEGIN
		SET @Exito = '-1 = Este ítem ya se encuentra registrado en la lista.'
		ROLLBACK
		GOTO Terminar
	END
	
	SET @idItemBotiquin = (SELECT MAX(idItemBotiquin) FROM ReportesApp_Operaciones_ControlItems_BotiquinItems)
	SET @idItemBotiquin = ISNULL(@idItemBotiquin,0) + 1 
	
	INSERT INTO ReportesApp_Operaciones_ControlItems_BotiquinItems(idItemBotiquin,Codigo,Descripcion,Cantidad,DiasDuracion,UsuarioCrea,FechaCrea)
	SELECT @idItemBotiquin, LTRIM(RTRIM(@Codigo)), @Descripcion, @Cantidad, @Duracion, @Usuario, GETDATE()
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

----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 29-04-2024
-- Description:	LISTAR ITEMS ASIGNADOS
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_ListarItemsAsignados]
@Opcion INT,
@idVehiculo INT
AS
BEGIN
	IF (@Opcion = 1) BEGIN		-- LISTAR MAESTRO DE ITEMS
		SELECT BI.idItemBotiquin, BI.Codigo AS 'CODIGO', BI.Descripcion AS 'ITEM', LTRIM(RTRIM(WH.DescripcionLocal)) AS 'DESCRIPCION',
		BI.Cantidad AS 'CANTIDAD', BI.DiasDuracion AS 'DURACION (DIAS)'
		FROM ReportesApp_Operaciones_ControlItems_BotiquinItems BI
		LEFT JOIN WH_ItemMast WH ON LTRIM(RTRIM(WH.Item)) = BI.Codigo
	END

	IF (@Opcion = 2) BEGIN		-- LISTAR ITEMS COMBOBOX
		SELECT 0 AS 'idItemBotiquin', 'TODOS' AS 'ITEM'
		UNION
		SELECT BI.idItemBotiquin, BI.Descripcion AS 'ITEM'
		FROM ReportesApp_Operaciones_ControlItems_BotiquinItems BI
	END

	IF (@Opcion = 3) BEGIN		-- LISTAR ITEMS
		SELECT BI.idItemBotiquin, BI.Descripcion AS 'ITEM'
		FROM ReportesApp_Operaciones_ControlItems_BotiquinItems BI
		ORDER BY BI.idItemBotiquin
	END
END

--------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 29-04-2024
-- Description:	ELIMINAR ITEM DE BOTIQUIN
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_EliminarItemBotiquin]
@Opcion INT,
@idBotiquinUnidadC INT,
@idItemBotiquin INT
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Item Eliminado Correctamente.'

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- ELIMINAR DEL MAESTRO
		IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Detalle WHERE idItemBotiquin = @idItemBotiquin)) BEGIN
			SET @Exito = '-1 = Este ítem no se puede eliminar porque ya está asignado a las unidades.'
			ROLLBACK
			GOTO Terminar
		END
		ELSE BEGIN
			DELETE FROM ReportesApp_Operaciones_ControlItems_BotiquinItems
			WHERE idItemBotiquin = @idItemBotiquin
		END
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR DE BOTIQUIN DETALLE
		DELETE FROM ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Detalle
		WHERE idBotiquinUnidadC = @idBotiquinUnidadC AND idItemBotiquin = @idItemBotiquin
	END

	IF (@Opcion = 3) BEGIN		-- ELIMINAR BOTIQUIN
		DELETE FROM ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Cabecera
		WHERE idBotiquinUnidadC = @idBotiquinUnidadC

		DELETE FROM ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Detalle
		WHERE idBotiquinUnidadC = @idBotiquinUnidadC
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

----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 02-05-2024
-- Description:	ASIGNAR BOTIQUIN A UNIDAD
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_AsignarBotiquin]
@idVehiculo INT,
@Usuario VARCHAR(20)
AS
DECLARE @idBotiquinUnidadC INT 
DECLARE @idBotiquinUnidadD INT 
DECLARE @Contador INT 
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Botiquín asignado correctamente a la unidad.'

BEGIN TRAN
BEGIN TRY
	IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Cabecera WHERE IdTracto = @idVehiculo)) BEGIN
		SET @Exito = '-1 = Esta unidad ya tiene asignado un botiquín.'
		ROLLBACK
		GOTO Terminar
	END
	
	SET @idBotiquinUnidadC = (SELECT MAX(idBotiquinUnidadC) FROM ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Cabecera)
	SET @idBotiquinUnidadC = ISNULL(@idBotiquinUnidadC,0) + 1 
	
	INSERT INTO ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Cabecera(idBotiquinUnidadC,IdTracto,UsuarioCrea,FechaCrea,UsuarioModifica,FechaModifica)
	SELECT @idBotiquinUnidadC, @idVehiculo, @Usuario, GETDATE(), @Usuario, GETDATE()

	SET @Contador = 1
	WHILE (@Contador <= (SELECT MAX(idItemBotiquin) FROM ReportesApp_Operaciones_ControlItems_BotiquinItems)) BEGIN
		SET @idBotiquinUnidadD = (SELECT MAX(idBotiquinUnidadD) FROM ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Detalle WHERE idBotiquinUnidadC = @idBotiquinUnidadC)
		SET @idBotiquinUnidadD = ISNULL(@idBotiquinUnidadD,0) + 1
		
		INSERT INTO ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Detalle(idBotiquinUnidadD,idBotiquinUnidadC,idItemBotiquin,Cantidad,FechaVencimiento,Observacion,Estado)
		SELECT @idBotiquinUnidadD, @idBotiquinUnidadC, @Contador, B.Cantidad, DATEADD(DAY,B.DiasDuracion,GETDATE()),' ','CONFORME'
		FROM ReportesApp_Operaciones_ControlItems_BotiquinItems B
		WHERE B.idItemBotiquin = @Contador

		SET @Contador = @Contador + 1
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
-- Create date: 02-05-2024
-- Description:	LISTAR BOTIQUÍN DE UNIDAD
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_ListarBotiquines]
@Placa VARCHAR(50),
@Operacion VARCHAR(50),
@Item VARCHAR(150)
AS
BEGIN
	IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Detalle BD WHERE Cantidad = 0)) BEGIN
		UPDATE BD
		SET Estado = 'PENDIENTE'
		FROM ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Detalle BD
		WHERE Cantidad = 0
	END
	ELSE BEGIN
		UPDATE BD
		SET Estado = 'CONFORME'
		FROM ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Detalle BD
		WHERE Cantidad != 0
	END

	IF (EXISTS(SELECT BD.* FROM ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Detalle BD
	LEFT JOIN ReportesApp_Operaciones_ControlItems_BotiquinItems I ON BD.idItemBotiquin = I.idItemBotiquin
	WHERE DATEDIFF(DAY,GETDATE(),BD.FechaVencimiento) <= 30 AND I.DiasDuracion != 0 AND Estado != 'PENDIENTE')) BEGIN
		UPDATE BD
		SET Estado = 'POR VENCER'
		FROM ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Detalle BD
		LEFT JOIN ReportesApp_Operaciones_ControlItems_BotiquinItems I ON I.idItemBotiquin = BD.idItemBotiquin
		WHERE DATEDIFF(DAY,GETDATE(),BD.FechaVencimiento) <= 30 AND I.DiasDuracion != 0 AND Estado != 'PENDIENTE'
	END
	
	IF (EXISTS(SELECT BD.* FROM ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Detalle BD
	LEFT JOIN ReportesApp_Operaciones_ControlItems_BotiquinItems I ON BD.idItemBotiquin = I.idItemBotiquin
	WHERE CONVERT(DATE,GETDATE()) >= BD.FechaVencimiento AND I.DiasDuracion != 0 AND Estado != 'PENDIENTE')) BEGIN
		UPDATE BD
		SET Estado = 'VENCIDO'
		FROM ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Detalle BD
		LEFT JOIN ReportesApp_Operaciones_ControlItems_BotiquinItems I ON I.idItemBotiquin = BD.idItemBotiquin
		WHERE CONVERT(DATE,GETDATE()) >= BD.FechaVencimiento AND I.DiasDuracion != 0 AND Estado != 'PENDIENTE'
	END

	IF (@Item = 'TODOS') BEGIN
		IF (@Operacion = 'TODO') BEGIN
			SELECT BC.idBotiquinUnidadC, BD.idBotiquinUnidadD, V.NumeroPlaca AS 'UNIDAD', V.NumeroPlaca AS 'UNIDAD1',
			ISNULL(O.Descripcion,'SIN OPERACION') AS 'OPERACION', BD.idItemBotiquin, I.Descripcion AS 'ITEM',
			BD.Cantidad AS 'CANTIDAD', CONVERT(VARCHAR,BD.FechaVencimiento,103) AS 'FECHA_VENCIMIENTO',
			CASE WHEN (DATEDIFF(DAY,GETDATE(),BD.FechaVencimiento) <= 0) THEN 0 ELSE DATEDIFF(DAY,GETDATE(),BD.FechaVencimiento) END AS 'DIAS_RESTANTES',
			BD.Observacion AS 'OBSERVACION', BD.Estado AS 'ESTADO', BD.NroRequerimiento AS 'REQUERIMIENTO', WH.Estado AS 'ESTADO_RQ', BC.UsuarioCrea,
			BC.FechaCrea, BC.UsuarioModifica, BC.FechaModifica
			FROM ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Cabecera BC
			LEFT JOIN ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Detalle BD ON BD.idBotiquinUnidadC = BC.idBotiquinUnidadC
			LEFT JOIN ReportesApp_Operaciones_ControlItems_BotiquinItems I ON I.idItemBotiquin = BD.idItemBotiquin
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = BC.IdTracto AND V.Estado = 2
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = UC.IdProgramacion
			LEFT JOIN WH_Requisiciones WH ON LTRIM(RTRIM(WH.RequisicionNumero)) = LTRIM(RTRIM(BD.NroRequerimiento))
			WHERE (@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%')
			ORDER BY BC.idBotiquinUnidadC DESC, BD.idBotiquinUnidadD ASC
		END
		ELSE BEGIN
			SELECT BC.idBotiquinUnidadC, BD.idBotiquinUnidadD, V.NumeroPlaca AS 'UNIDAD', V.NumeroPlaca AS 'UNIDAD1',
			ISNULL(O.Descripcion,'SIN OPERACION') AS 'OPERACION', BD.idItemBotiquin, I.Descripcion AS 'ITEM',
			BD.Cantidad AS 'CANTIDAD', CONVERT(VARCHAR,BD.FechaVencimiento,103) AS 'FECHA_VENCIMIENTO',
			CASE WHEN (DATEDIFF(DAY,GETDATE(),BD.FechaVencimiento) <= 0) THEN 0 ELSE DATEDIFF(DAY,GETDATE(),BD.FechaVencimiento) END AS 'DIAS_RESTANTES',
			BD.Observacion AS 'OBSERVACION', BD.Estado AS 'ESTADO', BD.NroRequerimiento AS 'REQUERIMIENTO', WH.Estado AS 'ESTADO_RQ', BC.UsuarioCrea,
			BC.FechaCrea, BC.UsuarioModifica, BC.FechaModifica
			FROM ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Cabecera BC
			LEFT JOIN ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Detalle BD ON BD.idBotiquinUnidadC = BC.idBotiquinUnidadC
			LEFT JOIN ReportesApp_Operaciones_ControlItems_BotiquinItems I ON I.idItemBotiquin = BD.idItemBotiquin
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = BC.IdTracto AND V.Estado = 2
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = UC.IdProgramacion
			LEFT JOIN WH_Requisiciones WH ON LTRIM(RTRIM(WH.RequisicionNumero)) = LTRIM(RTRIM(BD.NroRequerimiento))
			WHERE (@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND (@Operacion = ISNULL(O.Descripcion,'SIN OPERACION'))
			ORDER BY BC.idBotiquinUnidadC DESC, BD.idBotiquinUnidadD ASC 
		END
	END
	ELSE BEGIN
		IF (@Operacion = 'TODO') BEGIN
			SELECT BC.idBotiquinUnidadC, BD.idBotiquinUnidadD, V.NumeroPlaca AS 'UNIDAD', V.NumeroPlaca AS 'UNIDAD1',
			ISNULL(O.Descripcion,'SIN OPERACION') AS 'OPERACION', BD.idItemBotiquin, I.Descripcion AS 'ITEM',
			BD.Cantidad AS 'CANTIDAD', CONVERT(VARCHAR,BD.FechaVencimiento,103) AS 'FECHA_VENCIMIENTO',
			CASE WHEN (DATEDIFF(DAY,GETDATE(),BD.FechaVencimiento) <= 0) THEN 0 ELSE DATEDIFF(DAY,GETDATE(),BD.FechaVencimiento) END AS 'DIAS_RESTANTES',
			BD.Observacion AS 'OBSERVACION', BD.Estado AS 'ESTADO', BD.NroRequerimiento AS 'REQUERIMIENTO', WH.Estado AS 'ESTADO_RQ', BC.UsuarioCrea,
			BC.FechaCrea, BC.UsuarioModifica, BC.FechaModifica
			FROM ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Cabecera BC
			LEFT JOIN ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Detalle BD ON BD.idBotiquinUnidadC = BC.idBotiquinUnidadC
			LEFT JOIN ReportesApp_Operaciones_ControlItems_BotiquinItems I ON I.idItemBotiquin = BD.idItemBotiquin
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = BC.IdTracto AND V.Estado = 2
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = UC.IdProgramacion
			LEFT JOIN WH_Requisiciones WH ON LTRIM(RTRIM(WH.RequisicionNumero)) = LTRIM(RTRIM(BD.NroRequerimiento))
			WHERE (@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND (@Item = I.Descripcion)
			ORDER BY BC.idBotiquinUnidadC DESC, BD.idBotiquinUnidadD ASC 
		END
		ELSE BEGIN
			SELECT BC.idBotiquinUnidadC, BD.idBotiquinUnidadD, V.NumeroPlaca AS 'UNIDAD', V.NumeroPlaca AS 'UNIDAD1',
			ISNULL(O.Descripcion,'SIN OPERACION') AS 'OPERACION', BD.idItemBotiquin, I.Descripcion AS 'ITEM',
			BD.Cantidad AS 'CANTIDAD', CONVERT(VARCHAR,BD.FechaVencimiento,103) AS 'FECHA_VENCIMIENTO',
			CASE WHEN (DATEDIFF(DAY,GETDATE(),BD.FechaVencimiento) <= 0) THEN 0 ELSE DATEDIFF(DAY,GETDATE(),BD.FechaVencimiento) END AS 'DIAS_RESTANTES',
			BD.Observacion AS 'OBSERVACION', BD.Estado AS 'ESTADO', BD.NroRequerimiento AS 'REQUERIMIENTO', WH.Estado AS 'ESTADO_RQ', BC.UsuarioCrea,
			BC.FechaCrea, BC.UsuarioModifica, BC.FechaModifica
			FROM ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Cabecera BC
			LEFT JOIN ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Detalle BD ON BD.idBotiquinUnidadC = BC.idBotiquinUnidadC
			LEFT JOIN ReportesApp_Operaciones_ControlItems_BotiquinItems I ON I.idItemBotiquin = BD.idItemBotiquin
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = BC.IdTracto AND V.Estado = 2
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = UC.IdProgramacion
			LEFT JOIN WH_Requisiciones WH ON LTRIM(RTRIM(WH.RequisicionNumero)) = LTRIM(RTRIM(BD.NroRequerimiento))
			WHERE (@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND (@Item = I.Descripcion) AND (@Operacion = ISNULL(O.Descripcion,'SIN OPERACION'))
			ORDER BY BC.idBotiquinUnidadC DESC, BD.idBotiquinUnidadD ASC 
		END
	END
END

------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 02-05-2024
-- Description:	ASIGNAR BOTIQUIN A UNIDAD
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_CrearModificarBotiquin]
@idBotiquinUnidadC INT,
@idItemBotiquin INT,
@Cantidad INT,
@FechaVencimiento DATE,
@Observacion VARCHAR(250),
@Usuario VARCHAR(20)
AS
DECLARE @idBotiquinUnidadD INT 
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@FechaVencimiento < CONVERT(DATE,GETDATE()) AND
	(SELECT DiasDuracion FROM ReportesApp_Operaciones_ControlItems_BotiquinItems WHERE idItemBotiquin = @idItemBotiquin) != 0) BEGIN
		SET @Exito = '-1 = No puede registrar un ítem con una fecha menor a la de hoy.'
		ROLLBACK
		GOTO Terminar
	END
	ELSE BEGIN
		IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Detalle WHERE idBotiquinUnidadC = @idBotiquinUnidadC AND idItemBotiquin = @idItemBotiquin)) BEGIN
			UPDATE ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Detalle
			SET Cantidad = @Cantidad, FechaVencimiento = @FechaVencimiento, Observacion = @Observacion, Estado = 'CONFORME'
			WHERE idBotiquinUnidadC = @idBotiquinUnidadC AND idItemBotiquin = @idItemBotiquin
		
			SET @Exito = '0 = El botiquín de la unidad se actualizó correctamente.'
		END
		ELSE BEGIN
			SET @idBotiquinUnidadD = (SELECT MAX(idBotiquinUnidadD) FROM ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Detalle WHERE idBotiquinUnidadC = @idBotiquinUnidadC)
			SET @idBotiquinUnidadD = ISNULL(@idBotiquinUnidadD,0) + 1

			INSERT INTO ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Detalle(idBotiquinUnidadD,idBotiquinUnidadC,idItemBotiquin,Cantidad,FechaVencimiento,Observacion,Estado)
			VALUES(@idBotiquinUnidadD, @idBotiquinUnidadC, @idItemBotiquin, @Cantidad, @FechaVencimiento, @Observacion, 'CONFORME')

			SET @Exito = '0 = El botiquín de la unidad se actualizó correctamente.'
		END
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

------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 20-05-2024
-- Description:	ALERTAR FECHA DE VENCIMIENTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_AlertarFechaBotiquin]
AS
BEGIN
	DECLARE @Mensaje AS VARCHAR(MAX), @Asunto VARCHAR(300)
	DECLARE @AlertaInventario VARCHAR(MAX) = ''

	SELECT @AlertaInventario = @AlertaInventario + '<tr>'
							   + '<td style="background-color: #EEE8AA">' + LTRIM(RTRIM(V.NumeroPlaca)) + '</td>'
							   + '<td style="background-color: #EEE8AA">' + LTRIM(RTRIM(ISNULL(O.Descripcion,'SIN OPERACION'))) + '</td>'
							   + '<td style="background-color: #EEE8AA">' + LTRIM(RTRIM(I.Descripcion)) + '</td>'
							   + '<td style="background-color: #EEE8AA">' + LTRIM(RTRIM(CONVERT(VARCHAR,BD.Cantidad))) + '</td>'
							   + CASE WHEN (BD.Estado = 'POR VENCER') THEN '<td style="background-color: #FFFF00">' ELSE '<td style="background-color: #F46D75">' END
							   + LTRIM(RTRIM(BD.Estado)) + '</td>'
							   + '<td style="background-color: #EEE8AA">' + LTRIM(RTRIM(CONVERT(VARCHAR,BD.FechaVencimiento,103))) + '</td>'
							   + '<td style="background-color: #EEE8AA">' + CONVERT(VARCHAR,CASE WHEN (DATEDIFF(DAY,GETDATE(),BD.FechaVencimiento) <= 0) THEN 0 ELSE DATEDIFF(DAY,GETDATE(),BD.FechaVencimiento) END) + '</td>'
							   + '</tr>'   
	FROM ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Cabecera BC
	LEFT JOIN ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Detalle BD ON BD.idBotiquinUnidadC = BC.idBotiquinUnidadC
	LEFT JOIN ReportesApp_Operaciones_ControlItems_BotiquinItems I ON I.idItemBotiquin = BD.idItemBotiquin
	LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = BC.IdTracto AND V.Estado = 2
	LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
	LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = UC.IdProgramacion 
	WHERE (BD.Estado IN ('POR VENCER','VENCIDO'))
	ORDER BY BD.Estado ASC, V.NumeroPlaca ASC, BD.idBotiquinUnidadD ASC
	
	SET @Asunto = 'ALERTAS DEL SISTEMA – IMPLEMENTOS DE FLOTA'

	SET @Mensaje = '<p><h2>LISTA DE ARTÍCULOS DE BOTIQUÍN POR VENCER</h2></p>'
				  +'<p>Estas son las unidades cuyos ítems de botiquín están próximos a vencer o ya se encuentran vencidos: </p>'
				  + '<p><table border="1" cellspacing="1" cellpadding="1" >
					    <tr>
						    <td align="center" style="background-color:red; color: white"><b>UNIDAD</b></td>
							<td align="center" style="background-color:red; color: white"><b>OPERACIÓN</b></td>
							<td align="center" style="background-color:red; color: white"><b>ÍTEM</b></td>
							<td align="center" style="background-color:red; color: white"><b>CANTIDAD</b></td>
							<td align="center" style="background-color:red; color: white"><b>ESTADO</b></td>
							<td align="center" style="background-color:red; color: white"><b>FECHA_VENCIMIENTO</b></td>
							<td align="center" style="background-color:red; color: white"><b>DÍAS_RESTANTES</b></td>
						</tr>
						<tbody>'
						+ ISNULL(@AlertaInventario, '') 	
						+'</tbody>
						</table>
					</p>'
				  +'<p>Fecha de envío: '+CONVERT(CHAR(10),GETDATE(),103)+RIGHT(RTRIM(CONVERT(CHAR(26),GETDATE(),22)),12)+'</p>'
				  +'<BR>'
				  +'<p><font face="verdana" size="2" color="DarkRed">Tecnologías de la Información - GRUPO TRANSPESA</font></p>'
	
	EXEC msdb.dbo.sp_send_dbmail 
		 @profile_name='AVISODESISTEMA',
		 @recipients = 'controlinventario@transpesa.com.pe;planneroperaciones@transpesa.com.pe;gerenciaoperaciones@transpesa.com.pe;supervisoroperaciones@TRANSPESA.COM.PE;analista.logistica@transpesa.com.pe;asistente.logistica@transpesa.com.pe;operacionestrujillo@transpesa.com.pe;programacionlindley@transpesa.com.pe;operacionestolvas3@transpesa.com.pe',
		 --@blind_copy_recipients = 'asistenteauditoria@transpesa.com.pe;jrojas@transpesa.com.pe;desarrollo@transpesa.com.pe;desarrollo2@transpesa.com.pe;' 
		 @subject = @Asunto,
		 @body_format = 'HTML',
		 @body = @Mensaje	
END

------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 21-05-2024
-- Description:	GENERAR REQUERIMIENTO DE BOTIQUIN
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_GenerarRequerimiento]
@idBotiquinUnidadC INT,
@idBotiquinUnidadD INT,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = El requierimiento ha sido generado exitosamente.'

BEGIN TRAN
BEGIN TRY
	DECLARE @Unidad VARCHAR(50) = (SELECT LTRIM(RTRIM(V.NumeroPlaca)) FROM ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Cabecera BC
								   LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = BC.IdTracto AND V.Estado = 2 WHERE BC.idBotiquinUnidadC = @idBotiquinUnidadC)
	DECLARE @Proyecto VARCHAR(15) = (SELECT LTRIM(RTRIM(V.Proyecto)) FROM ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Cabecera BC
								    LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = BC.IdTracto AND V.Estado = 2 WHERE BC.idBotiquinUnidadC = @idBotiquinUnidadC)
	DECLARE @CodigoItem VARCHAR(100) = (SELECT BI.Codigo FROM ReportesApp_Operaciones_ControlItems_BotiquinItems BI
	                                    LEFT JOIN ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Detalle BD ON BD.idItemBotiquin = BI.idItemBotiquin
										WHERE BD.idBotiquinUnidadD = @idBotiquinUnidadD AND BD.idBotiquinUnidadC = @idBotiquinUnidadC)
	DECLARE @ItemDescripcion VARCHAR(250) = (SELECT BI.Descripcion FROM ReportesApp_Operaciones_ControlItems_BotiquinItems BI
											 LEFT JOIN ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Detalle BD ON BD.idItemBotiquin = BI.idItemBotiquin
											 WHERE BD.idBotiquinUnidadD = @idBotiquinUnidadD AND BD.idBotiquinUnidadC = @idBotiquinUnidadC)
	DECLARE @Cantidad INT = (SELECT BI.Cantidad FROM ReportesApp_Operaciones_ControlItems_BotiquinItems BI
	                        LEFT JOIN ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Detalle BD ON BD.idItemBotiquin = BI.idItemBotiquin
							WHERE BD.idBotiquinUnidadD = @idBotiquinUnidadD AND BD.idBotiquinUnidadC = @idBotiquinUnidadC)
	DECLARE @StockActual INT = (SELECT L.StockActual FROM WH_ItemAlmacenLote L WHERE L.Item = @CodigoItem AND L.AlmacenCodigo = 'A001')
	DECLARE @NroUsuario INT = 22587
	DECLARE @UnidadCodigo CHAR(6) = (SELECT UnidadCodigo FROM WH_ItemMast WHERE Item = @CodigoItem)
	DECLARE @DescripcionCompleta VARCHAR(250) = (SELECT DescripcionLocal FROM WH_ItemMast WHERE Item = @CodigoItem)

	IF (@Cantidad > @StockActual) BEGIN
		SET @Exito = '-1 = No hay stock suficiente para este ítem en el almacén.'
		ROLLBACK
		GOTO Terminar
	END

	--ACTUALIZAR CORRELATIVO DE REQUISICIONES
	UPDATE CorrelativosMast
	SET CorrelativoNumero = (SELECT CorrelativoNumero + 1 FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='100000') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='WHRQ')),
		UltimaFechaModif = GETDATE()
	WHERE (CorrelativosMast.CompaniaCodigo ='100000') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='WHRQ') 
	
	DECLARE @Nro INT = (SELECT CorrelativoNumero FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='100000') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='WHRQ'))
	DECLARE @PrecioUnitario DECIMAL(10,2) = (SELECT TOP(1) WH_Cotizacion.PrecioUnitario											FROM WH_RequisicionCotizacion											INNER JOIN WH_RequisicionDetalle ON (WH_RequisicionCotizacion.CompaniaSocio = WH_RequisicionDetalle.CompaniaSocio AND											WH_RequisicionCotizacion.RequisicionNumero = WH_RequisicionDetalle.RequisicionNumero)											INNER JOIN WH_Cotizacion ON (WH_RequisicionCotizacion.CompaniaSocio = WH_Cotizacion.CompaniaSocio AND											WH_RequisicionCotizacion.CotizacionSecuencia = WH_Cotizacion.CotizacionSecuencia)											WHERE (WH_RequisicionCotizacion.Secuencia = WH_RequisicionDetalle.Secuencia) AND (WH_Cotizacion.CompaniaSocio = '10000000') 											AND WH_Cotizacion.PrecioUnitario > 0 AND WH_RequisicionDetalle.Item = @CodigoItem											ORDER BY WH_Cotizacion.FechaDocumento DESC)

	--INSERTAR INFORMACIÓN DE REQUERIMIENTOS
	INSERT INTO WH_Requisiciones(CompaniaSocio,RequisicionNumero,Clasificacion,ComprasAlmacenFlag,AlmacenCodigo,MonedaCodigo,FechaRequerida,FechaPreparacion,PreparadaPor,Departamento,PrecioTotal,
	PrioridadCodigo,DefaultPrime,DefaultAfe,CuantiaMonetariaPendienteFlag,UnidadNegocio,UnidadReplicacion,LocalForeignFlag,Comentarios,Estado,UltimoUsuario,UltimaFechaModif,UltimoUsuarioNumero,
	TransaccionOperacion,DefaultCampoReferencia,DireccionDestino,UnidadNegocioCompra,RevisionTecnicaPendienteFlag)
	VALUES('10000000', CONVERT(CHAR(20),RIGHT('0000000000'+LTRIM(RTRIM(@Nro)),10)), 'STO', 'A', 'A001', 'LO', GETDATE(), GETDATE(), @NroUsuario, 'OP', CAST(@Cantidad*@PrecioUnitario AS DECIMAL(10,2)),
	'1', '010201', @Proyecto, 'N', 'TRAN', 'TRUJ', 'L', 'UNIDAD: '+@Unidad+' / ITEM SOLICITADO: '+@ItemDescripcion, 'RV', @Usuario, GETDATE(), @NroUsuario, '999', '95', 'Areas', 'TRAN', 'N')

	INSERT INTO WH_RequisicionDetalle(CompaniaSocio,RequisicionNumero,Secuencia,Item,Condicion,UnidadCodigo,Descripcion,ComprasAlmacenFlag,RedefinidoFlag,CantidadPedida,CantidadOrdenCompra,
	CantidadRecibida,PrecioUnitario,PrecioxCantidad,CotizacionCantidad,CotizacionPrecioUnitario,CotizacionPrecioUnitarioconIGV,CotizacionProveedor,CotizacionRegistros,ControlPresupuestalFlag,
	Comentario,CentroCosto,Estado,UltimoUsuario,UltimaFechaModif,IGVExoneradoFlag,GenerarContratoFlag,CuentaContable,Afe)
	VALUES ('10000000', CONVERT(CHAR(20),RIGHT('0000000000'+LTRIM(RTRIM(@Nro)),10)), 1, @CodigoItem, '0', @UnidadCodigo, @DescripcionCompleta, 'A', 'N', @Cantidad, 0.00, 0.00,
	@PrecioUnitario, CAST(@Cantidad*@PrecioUnitario AS DECIMAL(10,2)), 0.00, 0.00, 0.00, 0, 0, 'S', '', '010201', 'PR', @Usuario, GETDATE(), 'N', 'N', '9400209', @Proyecto)

	INSERT INTO WH_RequisitionDistribucion(RequisicionNumero,Secuencia,Linea,Account,Afe,Monto,CompaniaSocio,Sucursal,CampoReferencia)
	VALUES (CONVERT(CHAR(20),RIGHT('0000000000'+LTRIM(RTRIM(@Nro)),10)), 1, 1, '9400209', @Proyecto, 100.0000, '10000000', 'BTRU', '95')

	--AGREGAR REQUERIMIENTO A TABLA DE BOTIQUIN
	UPDATE ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Detalle
	SET NroRequerimiento = CONVERT(VARCHAR(20),RIGHT('0000000000'+LTRIM(RTRIM(@Nro)),10))
	WHERE idBotiquinUnidadD = @idBotiquinUnidadD AND idBotiquinUnidadC = @idBotiquinUnidadC

	SET @Exito = '0 = El requierimiento '+CONVERT(VARCHAR(20),RIGHT('0000000000'+LTRIM(RTRIM(@Nro)),10))+' ha sido generado exitosamente.'
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

-----------------------------------------------------------------------------------
-----------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 16/07/2024
-- Description:	LISTAR KITS DE NEUMÁTICO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_ListarKitNeumatico]
@Opcion INT,
@Empleado VARCHAR(250),
@Herramienta VARCHAR(250)
AS
BEGIN
	IF (@Opcion = 1) BEGIN		-- LISTAR HERRAMIENTAS DISPONIBLES
		SELECT H.IdHerramienta, H.CodigoItemAlmacen AS 'COD_ALMACEN', CASE WHEN H.Estado = 0 THEN 'INACTIVO' ELSE 'ACTIVO' END 'ESTADO',
		(CASE WHEN H.Almacen = '001' THEN 'TRUJILLO' WHEN H.Almacen = '002' THEN 'LIMA' END) AS 'SUCURSAL', H.CodigoInterno AS 'COD_INTERNO',
		RTRIM(H.Descripcion) AS 'HERRAMIENTA', C.Descripcion AS 'CATEGORÍA'
		FROM ReportesApp_Mantenimiento_Herramientas H
		LEFT JOIN ReportesApp_Mantenimiento_HerramientaPersona HP ON HP.IdHerramienta = H.IdHerramienta
		LEFT JOIN ReportesApp_Mantenimiento_Herramientas_Categoria C ON C.IDCategoria = H.IDCategoria
		WHERE (HP.Persona IS NULL) AND (H.Almacen = '001') AND (H.CodMaleta = 'KN-01') AND
		(@Herramienta IS NULL OR RTRIM(H.Descripcion) LIKE '%' + @Herramienta + '%')
		ORDER BY H.Descripcion
	END

	IF (@Opcion = 2) BEGIN		-- LISTAR HERRAMIENTAS ASIGNADAS
		SELECT HP.Persona, HP.IDHerramienta, LTRIM(RTRIM(P.NombreCompleto)) AS 'EMPLEADO', H.CodigoItemAlmacen AS 'COD_ALMACEN',
		H.CodigoInterno AS 'COD_INTERNO', RTRIM(H.Descripcion) AS 'HERRAMIENTA', C.Descripcion AS 'CATEGORÍA', HP.FHoraRegistra AS 'FECHA_ENTREGA'
		FROM ReportesApp_Mantenimiento_HerramientaPersona HP
		INNER JOIN PersonaMast P ON P.Persona = HP.Persona
		INNER JOIN ReportesApp_Mantenimiento_Herramientas H ON H.IdHerramienta = HP.IdHerramienta
		LEFT JOIN ReportesApp_Mantenimiento_Herramientas_Categoria C ON C.IDCategoria = H.IDCategoria
		WHERE (H.Almacen = '001') AND (H.CodMaleta = 'KN-01') AND (@Herramienta IS NULL OR RTRIM(H.Descripcion) LIKE '%' + @Herramienta + '%')
		AND (@Empleado IS NULL OR LTRIM(RTRIM(P.NombreCompleto)) LIKE '%' + @Empleado + '%')
		ORDER BY HP.FHoraRegistra DESC
	END
END

-----------------------------------------------------------------------------------
-----------------------------------------------------------------------------------

-- CREAR TABLA ReportesApp_Operaciones_ControlItems_RegistroExtintores Y LLENARLA

-----------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 02-08-2024
-- Description:	REGISTRAR Y MODIFICAR EXTINTORES
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_IngresarModificarEliminarExtintor]
@Opcion INT,
@idExtintor INT,
@IdTracto INT,
@Codigo VARCHAR(20),
@FechaVenc DATETIME,
@Peso DECIMAL(10,2),
@UnidadPeso VARCHAR(10),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX) = '0 = '
DECLARE @Correlativo INT

BEGIN TRAN
BEGIN TRY
	DECLARE @Placa VARCHAR(20) = (SELECT NumeroPlaca FROM OP_TR_Vehiculo WHERE IdVehiculo = @IdTracto)
	
	IF (@Opcion = 1) BEGIN		-- INGRESAR EXTINTOR
		IF ((MONTH(@FechaVenc) < MONTH(GETDATE())) AND (YEAR(@FechaVenc) < YEAR(GETDATE()))) BEGIN
			SET @Exito = '-1 = No puede ingresar un extintor con una fecha vencida.'
			ROLLBACK
			GOTO Terminar
		END

		SET @Correlativo = (SELECT MAX(idExtintor) FROM ReportesApp_Operaciones_ControlItems_RegistroExtintores WHERE IdTracto = @IdTracto) 
		SET @Correlativo = ISNULL(@Correlativo,0) + 1

		INSERT INTO ReportesApp_Operaciones_ControlItems_RegistroExtintores(idExtintor,IdTracto,Codigo,FechaVenc,Peso,UnidadPeso,Estado,
		UsuarioCreacion,FechaCreacion,UsuarioModificacion,FechaModificacion)
		VALUES(@Correlativo,@IdTracto,@Codigo,@FechaVenc,@Peso,@UnidadPeso,'VIGENTE',@Usuario,GETDATE(),@Usuario,GETDATE())

		SET @Exito = '0 = El extintor fue asignado correctamente a la unidad ' + @Placa
	END

	IF (@Opcion = 2) BEGIN		-- MODIFICAR DATOS DE EXTINTOR
		IF ((MONTH(@FechaVenc) < MONTH(GETDATE())) AND (YEAR(@FechaVenc) < YEAR(GETDATE()))) BEGIN
			SET @Exito = '-2 = No puede ingresar un extintor con una fecha vencida.'
			ROLLBACK
			GOTO Terminar
		END

		UPDATE ReportesApp_Operaciones_ControlItems_RegistroExtintores
		SET Codigo = @Codigo, FechaVenc = @FechaVenc, Peso = @Peso, UnidadPeso = @UnidadPeso, UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()
		WHERE idExtintor = @idExtintor AND IdTracto = @IdTracto
		
		SET @Exito = '0 = El extintor de la unidad '+@Placa+' ha sido actualizado.'
	END

	IF (@Opcion = 3) BEGIN		-- ELIMINAR DATOS DE EXTINTOR
		DELETE FROM ReportesApp_Operaciones_ControlItems_RegistroExtintores
		WHERE idExtintor = @idExtintor AND IdTracto = @IdTracto

		SET @Exito = '0 = El extintor de la unidad '+@Placa+' ha sido eliminado.'
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

--------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 02-08-2024
-- Description:	LISTAR EXTINTORES DE UNIDADES
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_ListarExtintores]
@Placa VARCHAR(50),
@Estado VARCHAR(20),
@Operacion VARCHAR(50)
AS
BEGIN
	UPDATE RE
	SET Estado = 'VIGENTE'
	FROM ReportesApp_Operaciones_ControlItems_RegistroExtintores RE
	
	IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ControlItems_RegistroExtintores RE
	WHERE DATEDIFF(MONTH,GETDATE(),RE.FechaVenc) = 1)) BEGIN
		UPDATE RE
		SET Estado = 'POR VENCER'
		FROM ReportesApp_Operaciones_ControlItems_RegistroExtintores RE
		WHERE DATEDIFF(MONTH,GETDATE(),RE.FechaVenc) = 1
	END

	IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ControlItems_RegistroExtintores RE
	WHERE DATEDIFF(MONTH,GETDATE(),RE.FechaVenc) <= 0)) BEGIN
		UPDATE RE
		SET Estado = 'VENCIDO'
		FROM ReportesApp_Operaciones_ControlItems_RegistroExtintores RE
		WHERE DATEDIFF(MONTH,GETDATE(),RE.FechaVenc) <= 0
	END

	IF (@Estado = 'TODOS') BEGIN
		IF (@Operacion = 'TODO') BEGIN
			SELECT RE.idExtintor AS 'NRO', RE.IdTracto, V.NumeroPlaca AS 'UNIDAD', O.Descripcion AS 'OPERACION', RE.Codigo AS 'CODIGO',
			CONVERT(VARCHAR,RE.Peso) AS 'PESO', RE.UnidadPeso AS 'UNIDAD_PESO', CONVERT(VARCHAR,MONTH(RE.FechaVenc))+' / '+CONVERT(VARCHAR,YEAR(RE.FechaVenc))
			AS 'FECHA_VENCIMIENTO', DATEDIFF(DAY,GETDATE(),RE.FechaVenc) AS 'DÍAS_RESTANTES', RE.Estado AS 'ESTADO', RE.UsuarioCreacion, RE.FechaCreacion,
			RE.UsuarioModificacion, RE.FechaModificacion
			FROM ReportesApp_Operaciones_ControlItems_RegistroExtintores RE
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = RE.IdTracto AND V.Estado = 2
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON  O.IdOperacion = UC.IdProgramacion
			WHERE (@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%')
			ORDER BY V.NumeroPlaca, RE.idExtintor DESC
		END
		ELSE BEGIN
			SELECT RE.idExtintor AS 'NRO', RE.IdTracto, V.NumeroPlaca AS 'UNIDAD', O.Descripcion AS 'OPERACION', RE.Codigo AS 'CODIGO',
			CONVERT(VARCHAR,RE.Peso) AS 'PESO', RE.UnidadPeso AS 'UNIDAD_PESO', CONVERT(VARCHAR,MONTH(RE.FechaVenc))+' / '+CONVERT(VARCHAR,YEAR(RE.FechaVenc))
			AS 'FECHA_VENCIMIENTO', DATEDIFF(DAY,GETDATE(),RE.FechaVenc) AS 'DÍAS_RESTANTES', RE.Estado AS 'ESTADO', RE.UsuarioCreacion, RE.FechaCreacion,
			RE.UsuarioModificacion, RE.FechaModificacion
			FROM ReportesApp_Operaciones_ControlItems_RegistroExtintores RE
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = RE.IdTracto AND V.Estado = 2
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON  O.IdOperacion = UC.IdProgramacion
			WHERE (@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND (@Operacion = O.Descripcion)
			ORDER BY V.NumeroPlaca, RE.idExtintor DESC
		END
	END
	ELSE BEGIN
		IF (@Operacion = 'TODO') BEGIN
			SELECT RE.idExtintor AS 'NRO', RE.IdTracto, V.NumeroPlaca AS 'UNIDAD', O.Descripcion AS 'OPERACION', RE.Codigo AS 'CODIGO',
			CONVERT(VARCHAR,RE.Peso) AS 'PESO', RE.UnidadPeso AS 'UNIDAD_PESO', CONVERT(VARCHAR,MONTH(RE.FechaVenc))+' / '+CONVERT(VARCHAR,YEAR(RE.FechaVenc))
			AS 'FECHA_VENCIMIENTO', DATEDIFF(DAY,GETDATE(),RE.FechaVenc) AS 'DÍAS_RESTANTES', RE.Estado AS 'ESTADO', RE.UsuarioCreacion, RE.FechaCreacion,
			RE.UsuarioModificacion, RE.FechaModificacion
			FROM ReportesApp_Operaciones_ControlItems_RegistroExtintores RE
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = RE.IdTracto AND V.Estado = 2
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON  O.IdOperacion = UC.IdProgramacion
			WHERE (@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND (@Estado = RE.Estado)
			ORDER BY V.NumeroPlaca, RE.idExtintor DESC
		END
		ELSE BEGIN
			SELECT RE.idExtintor AS 'NRO', RE.IdTracto, V.NumeroPlaca AS 'UNIDAD', O.Descripcion AS 'OPERACION', RE.Codigo AS 'CODIGO',
			CONVERT(VARCHAR,RE.Peso) AS 'PESO', RE.UnidadPeso AS 'UNIDAD_PESO', CONVERT(VARCHAR,MONTH(RE.FechaVenc))+' / '+CONVERT(VARCHAR,YEAR(RE.FechaVenc))
			AS 'FECHA_VENCIMIENTO', DATEDIFF(DAY,GETDATE(),RE.FechaVenc) AS 'DÍAS_RESTANTES', RE.Estado AS 'ESTADO', RE.UsuarioCreacion, RE.FechaCreacion,
			RE.UsuarioModificacion, RE.FechaModificacion
			FROM ReportesApp_Operaciones_ControlItems_RegistroExtintores RE
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = RE.IdTracto AND V.Estado = 2
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON  O.IdOperacion = UC.IdProgramacion
			WHERE (@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND (@Estado = RE.Estado) AND (@Operacion = O.Descripcion)
			ORDER BY V.NumeroPlaca, RE.idExtintor DESC
		END
	END
END

--------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 02-08-2024
-- Description:	ALERTAR FECHA EXTINTORES
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_AlertarFechaExtintor]
AS
BEGIN
	DECLARE @Mensaje AS VARCHAR(MAX), @Asunto VARCHAR(300)
	DECLARE @AlertaInventario VARCHAR(MAX) = ''

	SELECT @AlertaInventario = @AlertaInventario + '<tr>'
							   + '<td style="background-color: #FFFACD">' + LTRIM(RTRIM(CONVERT(VARCHAR,RE.idExtintor))) + '</td>'
							   + '<td style="background-color: #FFFACD">' + LTRIM(RTRIM(V.NumeroPlaca)) + '</td>'
							   + '<td style="background-color: #FFFACD">' + LTRIM(RTRIM(ISNULL(O.Descripcion,'SIN OPERACION'))) + '</td>'
							   + '<td style="background-color: #FFFACD">' + LTRIM(RTRIM(RE.Codigo)) + '</td>'
							   + '<td style="background-color: #FFFACD">' + LTRIM(RTRIM(CONVERT(VARCHAR,RE.Peso)+' '+RE.UnidadPeso)) + '</td>'
							   + '<td style="background-color: #FFFACD">' + LTRIM(RTRIM(CONVERT(VARCHAR,MONTH(RE.FechaVenc))+' / '+CONVERT(VARCHAR,YEAR(RE.FechaVenc)))) + '</td>'
							   + '<td style="background-color: #FFFACD">' + CONVERT(VARCHAR,DATEDIFF(DAY,GETDATE(),RE.FechaVenc)) + '</td>'
							   + CASE WHEN (RE.Estado = 'POR VENCER') THEN '<td style="background-color: #FFFF00">' ELSE '<td style="background-color: #F46D75">' END
							   + LTRIM(RTRIM(RE.Estado)) + '</td>'
							   + '</tr>'
	FROM ReportesApp_Operaciones_ControlItems_RegistroExtintores RE
	LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = RE.IdTracto AND V.Estado = 2
	LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
	LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON  O.IdOperacion = UC.IdProgramacion
	WHERE (RE.Estado IN ('POR VENCER','VENCIDO'))
	ORDER BY V.NumeroPlaca, RE.idExtintor DESC
	
	SET @Asunto = 'ALERTAS DEL SISTEMA – CONTROL DE EXTINTORES'

	SET @Mensaje = '<p><h2>LISTA DE EXTINTORES POR VENCER</h2></p>'
				  +'<p>Estas son las unidades cuyos extintores están próximos a vencer o ya se encuentran vencidos: </p>'
				  + '<p><table border="1" cellspacing="1" cellpadding="1" >
					    <tr>
						    <td align="center" style="background-color:yellow; color: red"><b>NRO</b></td>
							<td align="center" style="background-color:yellow; color: red"><b>UNIDAD</b></td>
							<td align="center" style="background-color:yellow; color: red"><b>OPERACION</b></td>
							<td align="center" style="background-color:yellow; color: red"><b>CODIGO</b></td>
							<td align="center" style="background-color:yellow; color: red"><b>PESO</b></td>
							<td align="center" style="background-color:yellow; color: red"><b>FECHA_VENCIMIENTO</b></td>
							<td align="center" style="background-color:yellow; color: red"><b>DÍAS_RESTANTES</b></td>
							<td align="center" style="background-color:yellow; color: red"><b>ESTADO</b></td>
						</tr>
						<tbody>'
						+ ISNULL(@AlertaInventario, '') 	
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

SELECT TOP(25) * FROM [msdb].[dbo].[sysmail_allitems]
ORDER BY [send_request_date] DESC

-----------------------------------------------------------------------------------

/*
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 02-08-2024
-- Description:	GENERAR REQUERIMIENTO DE EXTINTOR
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_GenerarRequerimientoExtintor]
@idTracto INT,
@idExtintor INT,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = El requierimiento ha sido generado exitosamente.'

BEGIN TRAN
BEGIN TRY
	DECLARE @Unidad VARCHAR(50) = (SELECT LTRIM(RTRIM(V.NumeroPlaca)) FROM ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Cabecera BC
								   LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = BC.IdTracto AND V.Estado = 2 WHERE BC.idBotiquinUnidadC = @idBotiquinUnidadC)
	DECLARE @Proyecto VARCHAR(15) = (SELECT LTRIM(RTRIM(V.Proyecto)) FROM ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Cabecera BC
								    LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = BC.IdTracto AND V.Estado = 2 WHERE BC.idBotiquinUnidadC = @idBotiquinUnidadC)
	DECLARE @CodigoItem VARCHAR(100) = (SELECT BI.Codigo FROM ReportesApp_Operaciones_ControlItems_BotiquinItems BI
	                                    LEFT JOIN ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Detalle BD ON BD.idItemBotiquin = BI.idItemBotiquin
										WHERE BD.idBotiquinUnidadD = @idBotiquinUnidadD AND BD.idBotiquinUnidadC = @idBotiquinUnidadC)
	DECLARE @ItemDescripcion VARCHAR(250) = (SELECT BI.Descripcion FROM ReportesApp_Operaciones_ControlItems_BotiquinItems BI
											 LEFT JOIN ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Detalle BD ON BD.idItemBotiquin = BI.idItemBotiquin
											 WHERE BD.idBotiquinUnidadD = @idBotiquinUnidadD AND BD.idBotiquinUnidadC = @idBotiquinUnidadC)
	DECLARE @Cantidad INT = (SELECT BI.Cantidad FROM ReportesApp_Operaciones_ControlItems_BotiquinItems BI
	                        LEFT JOIN ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Detalle BD ON BD.idItemBotiquin = BI.idItemBotiquin
							WHERE BD.idBotiquinUnidadD = @idBotiquinUnidadD AND BD.idBotiquinUnidadC = @idBotiquinUnidadC)
	DECLARE @StockActual INT = (SELECT L.StockActual FROM WH_ItemAlmacenLote L WHERE L.Item = @CodigoItem AND L.AlmacenCodigo = 'A001')
	DECLARE @NroUsuario INT = 22587
	DECLARE @UnidadCodigo CHAR(6) = (SELECT UnidadCodigo FROM WH_ItemMast WHERE Item = @CodigoItem)
	DECLARE @DescripcionCompleta VARCHAR(250) = (SELECT DescripcionLocal FROM WH_ItemMast WHERE Item = @CodigoItem)

	IF (@Cantidad > @StockActual) BEGIN
		SET @Exito = '-1 = No hay stock suficiente para este ítem en el almacén.'
		ROLLBACK
		GOTO Terminar
	END

	--ACTUALIZAR CORRELATIVO DE REQUISICIONES
	UPDATE CorrelativosMast
	SET CorrelativoNumero = (SELECT CorrelativoNumero + 1 FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='100000') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='WHRQ')),
		UltimaFechaModif = GETDATE()
	WHERE (CorrelativosMast.CompaniaCodigo ='100000') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='WHRQ') 
	
	DECLARE @Nro INT = (SELECT CorrelativoNumero FROM CorrelativosMast WHERE (CorrelativosMast.CompaniaCodigo ='100000') AND (CorrelativosMast.TipoComprobante ='SY') AND (CorrelativosMast.Serie ='WHRQ'))
	DECLARE @PrecioUnitario DECIMAL(10,2) = (SELECT TOP(1) WH_Cotizacion.PrecioUnitario											FROM WH_RequisicionCotizacion											INNER JOIN WH_RequisicionDetalle ON (WH_RequisicionCotizacion.CompaniaSocio = WH_RequisicionDetalle.CompaniaSocio AND											WH_RequisicionCotizacion.RequisicionNumero = WH_RequisicionDetalle.RequisicionNumero)											INNER JOIN WH_Cotizacion ON (WH_RequisicionCotizacion.CompaniaSocio = WH_Cotizacion.CompaniaSocio AND											WH_RequisicionCotizacion.CotizacionSecuencia = WH_Cotizacion.CotizacionSecuencia)											WHERE (WH_RequisicionCotizacion.Secuencia = WH_RequisicionDetalle.Secuencia) AND (WH_Cotizacion.CompaniaSocio = '10000000') 											AND WH_Cotizacion.PrecioUnitario > 0 AND WH_RequisicionDetalle.Item = @CodigoItem											ORDER BY WH_Cotizacion.FechaDocumento DESC)

	--INSERTAR INFORMACIÓN DE REQUERIMIENTOS
	INSERT INTO WH_Requisiciones(CompaniaSocio,RequisicionNumero,Clasificacion,ComprasAlmacenFlag,AlmacenCodigo,MonedaCodigo,FechaRequerida,FechaPreparacion,PreparadaPor,Departamento,PrecioTotal,
	PrioridadCodigo,DefaultPrime,DefaultAfe,CuantiaMonetariaPendienteFlag,UnidadNegocio,UnidadReplicacion,LocalForeignFlag,Comentarios,Estado,UltimoUsuario,UltimaFechaModif,UltimoUsuarioNumero,
	TransaccionOperacion,DefaultCampoReferencia,DireccionDestino,UnidadNegocioCompra,RevisionTecnicaPendienteFlag)
	VALUES('10000000', CONVERT(CHAR(20),RIGHT('0000000000'+LTRIM(RTRIM(@Nro)),10)), 'STO', 'A', 'A001', 'LO', GETDATE(), GETDATE(), @NroUsuario, 'OP', CAST(@Cantidad*@PrecioUnitario AS DECIMAL(10,2)),
	'1', '010201', @Proyecto, 'N', 'TRAN', 'TRUJ', 'L', 'UNIDAD: '+@Unidad+' / ITEM SOLICITADO: '+@ItemDescripcion, 'RV', @Usuario, GETDATE(), @NroUsuario, '999', '95', 'Areas', 'TRAN', 'N')

	INSERT INTO WH_RequisicionDetalle(CompaniaSocio,RequisicionNumero,Secuencia,Item,Condicion,UnidadCodigo,Descripcion,ComprasAlmacenFlag,RedefinidoFlag,CantidadPedida,CantidadOrdenCompra,
	CantidadRecibida,PrecioUnitario,PrecioxCantidad,CotizacionCantidad,CotizacionPrecioUnitario,CotizacionPrecioUnitarioconIGV,CotizacionProveedor,CotizacionRegistros,ControlPresupuestalFlag,
	Comentario,CentroCosto,Estado,UltimoUsuario,UltimaFechaModif,IGVExoneradoFlag,GenerarContratoFlag,CuentaContable,Afe)
	VALUES ('10000000', CONVERT(CHAR(20),RIGHT('0000000000'+LTRIM(RTRIM(@Nro)),10)), 1, @CodigoItem, '0', @UnidadCodigo, @DescripcionCompleta, 'A', 'N', @Cantidad, 0.00, 0.00,
	@PrecioUnitario, CAST(@Cantidad*@PrecioUnitario AS DECIMAL(10,2)), 0.00, 0.00, 0.00, 0, 0, 'S', '', '010201', 'PR', @Usuario, GETDATE(), 'N', 'N', '9400209', @Proyecto)

	INSERT INTO WH_RequisitionDistribucion(RequisicionNumero,Secuencia,Linea,Account,Afe,Monto,CompaniaSocio,Sucursal,CampoReferencia)
	VALUES (CONVERT(CHAR(20),RIGHT('0000000000'+LTRIM(RTRIM(@Nro)),10)), 1, 1, '9400209', @Proyecto, 100.0000, '10000000', 'BTRU', '95')

	--AGREGAR REQUERIMIENTO A TABLA DE BOTIQUIN
	UPDATE ReportesApp_Operaciones_ControlItems_BotiquinUnidades_Detalle
	SET NroRequerimiento = CONVERT(VARCHAR(20),RIGHT('0000000000'+LTRIM(RTRIM(@Nro)),10))
	WHERE idBotiquinUnidadD = @idBotiquinUnidadD AND idBotiquinUnidadC = @idBotiquinUnidadC

	SET @Exito = '0 = El requierimiento '+CONVERT(VARCHAR(20),RIGHT('0000000000'+LTRIM(RTRIM(@Nro)),10))+' ha sido generado exitosamente.'
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
*/

-----------------------------------------------------------------------------------
-----------------------------------------------------------------------------------

-- CREAR TABLA ReportesApp_Operaciones_ControlItems_RegistroKitAntiderrame

-----------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 18-10-2024
-- Description:	REGISTRAR Y MODIFICAR KIT ANTIDERRAME
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_IngresarModificarEliminarKAD]
@Opcion INT,
@idKAD INT,
@IdTracto INT,
@KitAsignado VARCHAR(20),
@Observacion VARCHAR(250),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX) = '0 = '
DECLARE @Correlativo INT

BEGIN TRAN
BEGIN TRY
	DECLARE @Placa VARCHAR(20) = (SELECT NumeroPlaca FROM OP_TR_Vehiculo WHERE IdVehiculo = @IdTracto)
	
	IF (@Opcion = 1) BEGIN		-- INGRESAR KIT ANTIDERRAME
		IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ControlItems_RegistroKitAntiderrame WHERE IdTracto = @IdTracto)) BEGIN
			SET @Exito = '-2 = La unidad '+@Placa+' ya tiene un kit antiderrame asignado.'
			ROLLBACK
			GOTO Terminar
		END
		
		SET @Correlativo = (SELECT MAX(idKAD) FROM ReportesApp_Operaciones_ControlItems_RegistroKitAntiderrame) 
		SET @Correlativo = ISNULL(@Correlativo,0) + 1

		INSERT INTO ReportesApp_Operaciones_ControlItems_RegistroKitAntiderrame(idKAD,IdTracto,KitAsignado,Observacion,UsuarioCreacion,
		FechaCreacion,UsuarioModificacion,FechaModificacion)
		VALUES(@Correlativo,@IdTracto,@KitAsignado,@Observacion,@Usuario,GETDATE(),@Usuario,GETDATE())

		SET @Exito = '0 = El kit fue asignado correctamente a la unidad ' + @Placa
	END

	IF (@Opcion = 2) BEGIN		-- MODIFICAR KIT ANTIDERRAME
		UPDATE ReportesApp_Operaciones_ControlItems_RegistroKitAntiderrame
		SET KitAsignado = @KitAsignado, Observacion = @Observacion, UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()
		WHERE idKAD = @idKAD AND IdTracto = @IdTracto
		
		SET @Exito = '0 = El kit antiderrame de la unidad '+@Placa+' ha sido actualizado.'
	END

	IF (@Opcion = 3) BEGIN		-- ELIMINAR KIT ANTIDERRAME
		DELETE FROM ReportesApp_Operaciones_ControlItems_RegistroKitAntiderrame
		WHERE idKAD = @idKAD AND IdTracto = @IdTracto

		SET @Exito = '0 = El kit antiderrame de la unidad '+@Placa+' ha sido eliminado.'
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

-------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 18-10-2024
-- Description:	LISTAR KIT ANTIDERRAME DE UNIDADES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_ListarKAD]
@Placa VARCHAR(50),
@Estado VARCHAR(20),
@Operacion VARCHAR(50)
AS
BEGIN
	IF (@Estado = 'TODOS') BEGIN
		IF (@Operacion = 'TODO') BEGIN
			SELECT KAD.idKAD AS 'NRO', KAD.IdTracto, V.NumeroPlaca AS 'UNIDAD', O.Descripcion AS 'OPERACION', KAD.KitAsignado AS 'ESTADO',
			KAD.Observacion AS 'OBSERVACION', KAD.UsuarioCreacion, KAD.FechaCreacion, KAD.UsuarioModificacion, KAD.FechaModificacion
			FROM ReportesApp_Operaciones_ControlItems_RegistroKitAntiderrame KAD
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = KAD.IdTracto AND V.Estado = 2
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON  O.IdOperacion = UC.IdProgramacion
			WHERE (@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%')
			ORDER BY V.NumeroPlaca ASC
		END
		ELSE BEGIN
			SELECT KAD.idKAD AS 'NRO', KAD.IdTracto, V.NumeroPlaca AS 'UNIDAD', O.Descripcion AS 'OPERACION', KAD.KitAsignado AS 'ESTADO',
			KAD.Observacion AS 'OBSERVACION', KAD.UsuarioCreacion, KAD.FechaCreacion, KAD.UsuarioModificacion, KAD.FechaModificacion
			FROM ReportesApp_Operaciones_ControlItems_RegistroKitAntiderrame KAD
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = KAD.IdTracto AND V.Estado = 2
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON  O.IdOperacion = UC.IdProgramacion
			WHERE (@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND (@Operacion = O.Descripcion)
			ORDER BY V.NumeroPlaca ASC
		END
	END
	ELSE BEGIN
		IF (@Operacion = 'TODO') BEGIN
			SELECT KAD.idKAD AS 'NRO', KAD.IdTracto, V.NumeroPlaca AS 'UNIDAD', O.Descripcion AS 'OPERACION', KAD.KitAsignado AS 'ESTADO',
			KAD.Observacion AS 'OBSERVACION', KAD.UsuarioCreacion, KAD.FechaCreacion, KAD.UsuarioModificacion, KAD.FechaModificacion
			FROM ReportesApp_Operaciones_ControlItems_RegistroKitAntiderrame KAD
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = KAD.IdTracto AND V.Estado = 2
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON  O.IdOperacion = UC.IdProgramacion
			WHERE (@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND (@Estado = KAD.KitAsignado)
			ORDER BY V.NumeroPlaca ASC
		END
		ELSE BEGIN
			SELECT KAD.idKAD AS 'NRO', KAD.IdTracto, V.NumeroPlaca AS 'UNIDAD', O.Descripcion AS 'OPERACION', KAD.KitAsignado AS 'ESTADO',
			KAD.Observacion AS 'OBSERVACION', KAD.UsuarioCreacion, KAD.FechaCreacion, KAD.UsuarioModificacion, KAD.FechaModificacion
			FROM ReportesApp_Operaciones_ControlItems_RegistroKitAntiderrame KAD
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = KAD.IdTracto AND V.Estado = 2
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON  O.IdOperacion = UC.IdProgramacion
			WHERE (@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND (@Estado = KAD.KitAsignado) AND (@Operacion = O.Descripcion)
			ORDER BY V.NumeroPlaca ASC
		END
	END
END
