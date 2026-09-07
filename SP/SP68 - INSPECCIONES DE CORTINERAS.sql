
-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_InspeccionProcesosCR Y LLENARLA

-- CREAR TABLA ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_DetalleCR

------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 15-08-2024
-- Description:	CREAR NUEVA INSPECCIÓN
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_CrearInspeccion]
@Placa VARCHAR(50),
@Tipo VARCHAR(250),
@SubTipo VARCHAR(250),
@Operacion VARCHAR(50),
@Marca VARCHAR(250),
@Modelo VARCHAR(250),
@FechaProyectada DATE,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

BEGIN TRAN
BEGIN TRY
	SET @correlativo = (SELECT MAX(idInspeccionC) FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera)
	SET @correlativo = ISNULL(@correlativo,0) + 1 

	INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera(idInspeccionC,Placa,Tipo,SubTipo,Operacion,Marca,Modelo,FechaProyectada,UsuarioCrea,FechaCrea,Ultimo,Activo)
	VALUES(@correlativo,@Placa,@Tipo,@SubTipo,@Operacion,@Marca,@Modelo,@FechaProyectada,@Usuario,GETDATE(),0,0)

	IF (@SubTipo = 'TRACTO') BEGIN
		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Detalle(idInspeccionD,idInspeccionC,idProceso)
		SELECT idProceso, @correlativo, idProceso FROM ReportesApp_Mantenimiento_MttoPreventivo_InspeccionProcesos
	END

	IF (@SubTipo = 'CORTINERA') BEGIN
		INSERT INTO ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_DetalleCR(idInspeccionD,idInspeccionC,idProcesoCR)
		SELECT idProcesoCR, @correlativo, idProcesoCR FROM ReportesApp_Mantenimiento_MttoPreventivo_InspeccionProcesosCR
	END

	SET @Exito = '0 = Inspeccion creada correctamente.'
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

------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 14-08-2024
-- Description:	LISTAR INSPECCIONES DE UNIDADES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarInspeccionesUnidad]
@Opcion INT,
@Placa VARCHAR(250),
@idInspeccionC INT
AS
BEGIN
	IF (@Opcion = 1) BEGIN		-- LISTAR MECÁNICOS CON TURNOS
		SELECT Persona, NombreCompleto AS 'MECÁNICO', Turno AS 'TURNO'
		FROM ReportesApp_Mantenimiento_AsignacionOT_Mecanicos
		WHERE (NombreCompleto LIKE '%' + @Placa + '%')
	END

	IF (@Opcion = 2) BEGIN		-- LISTAR INSPECCIONES POR UNIDAD
		SELECT ID.idInspeccionC, ID.idInspeccionD, IC.Placa, IP.TipoProceso AS 'COMPONENTE', IP.ParteTracto AS 'PROCESO',		ISNULL(IC.Mecanico,-1) AS 'Mecanico1', LTRIM(RTRIM(P1.NombreCompleto)) AS 'MECÁNICO', ISNULL(IC.Electrico,-1) AS 'Electrico2',		LTRIM(RTRIM(P2.NombreCompleto)) AS 'ELÉCTRICO', ISNULL(IC.Neumatico,-1) AS 'Neumatico3', LTRIM(RTRIM(P3.NombreCompleto)) AS 'NEUMÁTICO',		ISNULL(IC.Soldador,-1) AS 'Soldador4', LTRIM(RTRIM(P4.NombreCompleto)) AS 'SOLDADOR', ID.Estado AS 'ESTADO', ID.Observacion AS 'OBSERVACION'		FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Detalle ID		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera IC ON IC.idInspeccionC = ID.idInspeccionC		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_InspeccionProcesos IP ON IP.idProceso = ID.idProceso		LEFT JOIN PersonaMast P1 ON P1.Persona = IC.Mecanico		LEFT JOIN PersonaMast P2 ON P2.Persona = IC.Electrico		LEFT JOIN PersonaMast P3 ON P3.Persona = IC.Neumatico		LEFT JOIN PersonaMast P4 ON P4.Persona = IC.Soldador		WHERE (IC.Placa = @Placa) AND		ID.idInspeccionC = (SELECT MAX(idInspeccionC) FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera WHERE IC.Placa = @Placa)		ORDER BY ID.idInspeccionD ASC
	END

	IF (@Opcion = 3) BEGIN		-- BUSCAR INSPECCIONES POR ID
		SELECT ID.idInspeccionC, ID.idInspeccionD, IC.Placa, IP.TipoProceso AS 'COMPONENTE', IP.ParteTracto AS 'PROCESO', IC.FechaCrea AS 'FECHA_INSPECCION',		ISNULL(IC.Mecanico,-1) AS 'Mecanico1', LTRIM(RTRIM(P1.NombreCompleto)) AS 'MECÁNICO', ISNULL(IC.Electrico,-1) AS 'Electrico2',		LTRIM(RTRIM(P2.NombreCompleto)) AS 'ELÉCTRICO', ISNULL(IC.Neumatico,-1) AS 'Neumatico3', LTRIM(RTRIM(P3.NombreCompleto)) AS 'NEUMÁTICO',		ISNULL(IC.Soldador,-1) AS 'Soldador4', LTRIM(RTRIM(P4.NombreCompleto)) AS 'SOLDADOR', ID.Estado AS 'ESTADO', ID.Observacion AS 'OBSERVACION'		FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Detalle ID		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera IC ON IC.idInspeccionC = ID.idInspeccionC		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_InspeccionProcesos IP ON IP.idProceso = ID.idProceso		LEFT JOIN PersonaMast P1 ON P1.Persona = IC.Mecanico		LEFT JOIN PersonaMast P2 ON P2.Persona = IC.Electrico		LEFT JOIN PersonaMast P3 ON P3.Persona = IC.Neumatico		LEFT JOIN PersonaMast P4 ON P4.Persona = IC.Soldador		WHERE ID.idInspeccionC = @idInspeccionC		ORDER BY ID.idInspeccionD ASC
	END

	IF (@Opcion = 4) BEGIN		-- ELIMINAR INSPECCIONES
		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera
		SET Activo = 0
		WHERE idInspeccionC = @idInspeccionC

		DECLARE @exito VARCHAR(50) = '0 = Inspeccion eliminada correctamente.'

		SELECT @exito exito
	END

	IF (@Opcion = 5) BEGIN		-- LISTAR INSPECCIONES POR UNIDAD - CORTINERAS
		SELECT ID.idInspeccionC, ID.idInspeccionD, IC.Placa, IP.TipoProceso AS 'PROCESO',		ISNULL(IC.Mecanico,-1) AS 'Mecanico1', LTRIM(RTRIM(P1.NombreCompleto)) AS 'MECÁNICO', ISNULL(IC.Electrico,-1) AS 'Electrico2',		LTRIM(RTRIM(P2.NombreCompleto)) AS 'ELÉCTRICO', ISNULL(IC.Neumatico,-1) AS 'Neumatico3', LTRIM(RTRIM(P3.NombreCompleto)) AS 'NEUMÁTICO',		ISNULL(IC.Soldador,-1) AS 'Soldador4', LTRIM(RTRIM(P4.NombreCompleto)) AS 'SOLDADOR', ID.Estado AS 'ESTADO', ID.Observacion AS 'OBSERVACION'		FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_DetalleCR ID		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera IC ON IC.idInspeccionC = ID.idInspeccionC		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_InspeccionProcesosCR IP ON IP.idProcesoCR = ID.idProcesoCR		LEFT JOIN PersonaMast P1 ON P1.Persona = IC.Mecanico		LEFT JOIN PersonaMast P2 ON P2.Persona = IC.Electrico		LEFT JOIN PersonaMast P3 ON P3.Persona = IC.Neumatico		LEFT JOIN PersonaMast P4 ON P4.Persona = IC.Soldador		WHERE (IC.Placa = @Placa) AND		ID.idInspeccionC = (SELECT MAX(idInspeccionC) FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera WHERE IC.Placa = @Placa)		ORDER BY ID.idInspeccionD ASC
	END

	IF (@Opcion = 6) BEGIN		-- BUSCAR INSPECCIONES POR ID - CORTINERA
		SELECT ID.idInspeccionC, ID.idInspeccionD, IC.Placa, IP.TipoProceso AS 'PROCESO', IC.FechaCrea AS 'FECHA_INSPECCION',		ISNULL(IC.Mecanico,-1) AS 'Mecanico1', LTRIM(RTRIM(P1.NombreCompleto)) AS 'MECÁNICO', ISNULL(IC.Electrico,-1) AS 'Electrico2',		LTRIM(RTRIM(P2.NombreCompleto)) AS 'ELÉCTRICO', ISNULL(IC.Neumatico,-1) AS 'Neumatico3', LTRIM(RTRIM(P3.NombreCompleto)) AS 'NEUMÁTICO',		ISNULL(IC.Soldador,-1) AS 'Soldador4', LTRIM(RTRIM(P4.NombreCompleto)) AS 'SOLDADOR', ID.Estado AS 'ESTADO', ID.Observacion AS 'OBSERVACION'		FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_DetalleCR ID		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera IC ON IC.idInspeccionC = ID.idInspeccionC		LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_InspeccionProcesosCR IP ON IP.idProcesoCR = ID.idProcesoCR		LEFT JOIN PersonaMast P1 ON P1.Persona = IC.Mecanico		LEFT JOIN PersonaMast P2 ON P2.Persona = IC.Electrico		LEFT JOIN PersonaMast P3 ON P3.Persona = IC.Neumatico		LEFT JOIN PersonaMast P4 ON P4.Persona = IC.Soldador		WHERE ID.idInspeccionC = @idInspeccionC		ORDER BY ID.idInspeccionD ASC
	END
END

-------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 15-08-2024
-- Description:	ACTUALIZAR INSPECCIÓN DE UNIDAD
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ActualizarInspeccion]
@Opcion INT,
@idInspeccionC INT,
@idInspeccionD INT,
@Estado VARCHAR(30),
@Observacion VARCHAR(250),
@Mecanico INT,
@Electrico INT,
@Neumatico INT,
@Soldador INT,
@FechaInspeccion DATETIME
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo2 INT

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- AGREGAR ESTADO
		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Detalle
		SET Estado = @Estado, Observacion = @Observacion
		WHERE idInspeccionC = @idInspeccionC AND idInspeccionD = @idInspeccionD

		IF (@Estado = 'MALO') BEGIN
			DECLARE @NroPlaca VARCHAR(20) = (SELECT Placa FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera WHERE idInspeccionC = @idInspeccionC)
			
			SET @correlativo2 = (SELECT MAX(idMttoC) FROM ReportesApp_Mantenimiento_MttoCorrectivo_Registro)
			SET @correlativo2 = ISNULL(@correlativo2, 0) + 1

			INSERT INTO ReportesApp_Mantenimiento_MttoCorrectivo_Registro(idMttoC, Placa, Operacion, Origen, FechaReportada, Sistema,
			Descripcion, Observacion, Estado, UsuarioCreacion, FechaCreacion, UsuarioModificacion, FechaModificacion)
			SELECT @correlativo2, @NroPlaca, IC.Operacion, 'INSPECCION', IC.FechaCrea, IP.TipoProceso, IP.ParteTracto, ID.Observacion,
			'PENDIENTE', IC.UsuarioCrea, GETDATE(), IC.UsuarioCrea, GETDATE()
			FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Detalle ID
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera IC ON IC.idInspeccionC = ID.idInspeccionC
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_InspeccionProcesos IP ON IP.idProceso = ID.idProceso
			WHERE ID.idInspeccionC = @idInspeccionC AND ID.idInspeccionD = @idInspeccionD
		END

		SET @Exito = '0 = Inspeccion actualizada correctamente.'
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR ESTADO
		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Detalle
		SET Estado = NULL, Observacion = NULL
		WHERE idInspeccionC = @idInspeccionC AND idInspeccionD = @idInspeccionD

		SET @Exito = '0 = Inspeccion anulada correctamente.'
	END

	IF (@Opcion = 3) BEGIN		-- EDITAR INSPECCION
		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera
		SET Mecanico = @Mecanico, Electrico = @Electrico, Neumatico = @Neumatico, Soldador = @Soldador, FechaCrea = @FechaInspeccion, Activo = 1
		WHERE idInspeccionC = @idInspeccionC

		DECLARE @Placa VARCHAR(20) = (SELECT Placa FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera WHERE idInspeccionC = @idInspeccionC AND Activo = 1)
		DECLARE @UltimaFecha DATETIME = (SELECT MAX(FechaCrea) FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera WHERE Placa = @Placa AND Activo = 1)

		IF (@FechaInspeccion >= @UltimaFecha) BEGIN
			UPDATE ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera
			SET Ultimo = 1
			WHERE idInspeccionC = @idInspeccionC
		END
		ELSE BEGIN
			UPDATE ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera
			SET Ultimo = 0
			WHERE idInspeccionC = @idInspeccionC
		END

		SET @Exito = '0 = Inspeccion actualizada correctamente.'
	END

	IF (@Opcion = 4) BEGIN		-- ELIMINAR ESTADO - CORTINERA
		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_DetalleCR
		SET Estado = NULL, Observacion = NULL
		WHERE idInspeccionC = @idInspeccionC AND idInspeccionD = @idInspeccionD

		SET @Exito = '0 = Inspeccion anulada correctamente.'
	END

	IF (@Opcion = 5) BEGIN		-- AGREGAR ESTADO - CORTINERA
		UPDATE ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_DetalleCR
		SET Estado = @Estado, Observacion = @Observacion
		WHERE idInspeccionC = @idInspeccionC AND idInspeccionD = @idInspeccionD

		IF (@Estado = 'MALO') BEGIN
			DECLARE @NroPlaca2 VARCHAR(20) = (SELECT Placa FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera WHERE idInspeccionC = @idInspeccionC)
			
			SET @correlativo2 = (SELECT MAX(idMttoC) FROM ReportesApp_Mantenimiento_MttoCorrectivo_Registro)
			SET @correlativo2 = ISNULL(@correlativo2, 0) + 1

			INSERT INTO ReportesApp_Mantenimiento_MttoCorrectivo_Registro(idMttoC, Placa, Operacion, Origen, FechaReportada, Sistema,
			Descripcion, Observacion, Estado, UsuarioCreacion, FechaCreacion, UsuarioModificacion, FechaModificacion)
			SELECT @correlativo2, @NroPlaca2, IC.Operacion, 'INSPECCION', IC.FechaCrea, ' ', IP.TipoProceso, ID.Observacion,
			'PENDIENTE', IC.UsuarioCrea, GETDATE(), IC.UsuarioCrea, GETDATE()
			FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_DetalleCR ID
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera IC ON IC.idInspeccionC = ID.idInspeccionC
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_InspeccionProcesosCR IP ON IP.idProcesoCR = ID.idProcesoCR
			WHERE ID.idInspeccionC = @idInspeccionC AND ID.idInspeccionD = @idInspeccionD
		END

		SET @Exito = '0 = Inspeccion actualizada correctamente.'
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

-------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 14-08-2024
-- Description:	LISTAR INSPECCIONES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoPreventivo_ListarInspecciones]
@Opcion INT,
@Fecha VARCHAR(5),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@Placa VARCHAR(20),
@TipoMaquina VARCHAR(50)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@Opcion = 1) BEGIN		-- LISTAR ÚLTIMAS INSPECCIONES
		IF (@Fecha = 'FP') BEGIN
			IF (@TipoMaquina = 'TODOS') BEGIN
				SELECT IC.idInspeccionC AS 'NRO', IC.Placa AS 'PLACA', IC.Tipo AS 'TIPO_UNIDAD', IC.SubTipo AS 'SUBTIPO_UNIDAD', IC.Operacion AS 'OPERACION',
				IC.Marca AS 'MARCA', IC.Modelo AS 'MODELO', ISNULL(IC.Mecanico,-1) AS 'Mecanico1', LTRIM(RTRIM(P1.NombreCompleto)) AS 'MECÁNICO',				ISNULL(IC.Electrico,-1) AS 'Electrico2', LTRIM(RTRIM(P2.NombreCompleto)) AS 'ELÉCTRICO', ISNULL(IC.Neumatico,-1) AS 'Neumatico3',
				LTRIM(RTRIM(P3.NombreCompleto)) AS 'NEUMÁTICO', ISNULL(IC.Soldador,-1) AS 'Soldador4', LTRIM(RTRIM(P4.NombreCompleto)) AS 'SOLDADOR',
				DATEADD(DAY,-10,FechaProyectada) AS 'PROX_INSPECCION', FechaProyectada AS 'PROX_MTTO',
				IC.FechaCrea AS 'FECHA_INSPECCION', (CASE WHEN (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Detalle
				WHERE Observacion != '' AND idInspeccionC = IC.idInspeccionC)) THEN 'OBSERVADO' ELSE 'CONFORME' END) AS 'ESTADO',
				(CASE WHEN (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Pedidos WHERE idInspeccionC = IC.idInspeccionC))
				THEN 'PEDIDO' ELSE 'SIN PEDIR' END) AS 'ESTADO_PEDIDO'
				FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera IC
				LEFT JOIN PersonaMast P1 ON P1.Persona = IC.Mecanico				LEFT JOIN PersonaMast P2 ON P2.Persona = IC.Electrico				LEFT JOIN PersonaMast P3 ON P3.Persona = IC.Neumatico
				LEFT JOIN PersonaMast P4 ON P4.Persona = IC.Soldador
				WHERE (IC.Ultimo = 1) AND (IC.Activo = 1)
				AND (DATEADD(DAY,-10,FechaProyectada) BETWEEN @FINICIO AND @FFIN) AND (IC.Placa IS NULL OR IC.Placa LIKE '%' + @Placa + '%') 
			END
			ELSE BEGIN
				SELECT IC.idInspeccionC AS 'NRO', IC.Placa AS 'PLACA', IC.Tipo AS 'TIPO_UNIDAD', IC.SubTipo AS 'SUBTIPO_UNIDAD', IC.Operacion AS 'OPERACION',
				IC.Marca AS 'MARCA', IC.Modelo AS 'MODELO', ISNULL(IC.Mecanico,-1) AS 'Mecanico1', LTRIM(RTRIM(P1.NombreCompleto)) AS 'MECÁNICO',				ISNULL(IC.Electrico,-1) AS 'Electrico2', LTRIM(RTRIM(P2.NombreCompleto)) AS 'ELÉCTRICO', ISNULL(IC.Neumatico,-1) AS 'Neumatico3',
				LTRIM(RTRIM(P3.NombreCompleto)) AS 'NEUMÁTICO', ISNULL(IC.Soldador,-1) AS 'Soldador4', LTRIM(RTRIM(P4.NombreCompleto)) AS 'SOLDADOR',
				DATEADD(DAY,-10,FechaProyectada) AS 'PROX_INSPECCION', FechaProyectada AS 'PROX_MTTO',
				IC.FechaCrea AS 'FECHA_INSPECCION', (CASE WHEN (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Detalle
				WHERE Observacion != '' AND idInspeccionC = IC.idInspeccionC)) THEN 'OBSERVADO' ELSE 'CONFORME' END) AS 'ESTADO',
				(CASE WHEN (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Pedidos WHERE idInspeccionC = IC.idInspeccionC))
				THEN 'PEDIDO' ELSE 'SIN PEDIR' END) AS 'ESTADO_PEDIDO'
				FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera IC
				LEFT JOIN PersonaMast P1 ON P1.Persona = IC.Mecanico				LEFT JOIN PersonaMast P2 ON P2.Persona = IC.Electrico				LEFT JOIN PersonaMast P3 ON P3.Persona = IC.Neumatico
				LEFT JOIN PersonaMast P4 ON P4.Persona = IC.Soldador
				WHERE (IC.Ultimo = 1) AND (IC.Activo = 1)
				AND (DATEADD(DAY,-10,FechaProyectada) BETWEEN @FINICIO AND @FFIN) AND (IC.Placa IS NULL OR IC.Placa LIKE '%' + @Placa + '%')
				AND (IC.Tipo = @TipoMaquina)
			END
		END
		
		IF (@Fecha = 'FR') BEGIN
			IF (@TipoMaquina = 'TODOS') BEGIN
				SELECT IC.idInspeccionC AS 'NRO', IC.Placa AS 'PLACA', IC.Tipo AS 'TIPO_UNIDAD', IC.SubTipo AS 'SUBTIPO_UNIDAD', IC.Operacion AS 'OPERACION',
				IC.Marca AS 'MARCA', IC.Modelo AS 'MODELO', ISNULL(IC.Mecanico,-1) AS 'Mecanico1', LTRIM(RTRIM(P1.NombreCompleto)) AS 'MECÁNICO',				ISNULL(IC.Electrico,-1) AS 'Electrico2', LTRIM(RTRIM(P2.NombreCompleto)) AS 'ELÉCTRICO', ISNULL(IC.Neumatico,-1) AS 'Neumatico3',
				LTRIM(RTRIM(P3.NombreCompleto)) AS 'NEUMÁTICO', ISNULL(IC.Soldador,-1) AS 'Soldador4', LTRIM(RTRIM(P4.NombreCompleto)) AS 'SOLDADOR',
				DATEADD(DAY,-10,FechaProyectada) AS 'PROX_INSPECCION', FechaProyectada AS 'PROX_MTTO',
				IC.FechaCrea AS 'FECHA_INSPECCION', (CASE WHEN (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Detalle
				WHERE Observacion != '' AND idInspeccionC = IC.idInspeccionC)) THEN 'OBSERVADO' ELSE 'CONFORME' END) AS 'ESTADO',
				(CASE WHEN (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Pedidos WHERE idInspeccionC = IC.idInspeccionC))
				THEN 'PEDIDO' ELSE 'SIN PEDIR' END) AS 'ESTADO_PEDIDO'
				FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera IC
				LEFT JOIN PersonaMast P1 ON P1.Persona = IC.Mecanico				LEFT JOIN PersonaMast P2 ON P2.Persona = IC.Electrico				LEFT JOIN PersonaMast P3 ON P3.Persona = IC.Neumatico
				LEFT JOIN PersonaMast P4 ON P4.Persona = IC.Soldador
				WHERE (IC.Ultimo = 1) AND (IC.Activo = 1)
				AND (IC.FechaCrea BETWEEN @FINICIO AND @FFIN) AND (IC.Placa IS NULL OR IC.Placa LIKE '%' + @Placa + '%') 
			END
			ELSE BEGIN
				SELECT IC.idInspeccionC AS 'NRO', IC.Placa AS 'PLACA', IC.Tipo AS 'TIPO_UNIDAD', IC.SubTipo AS 'SUBTIPO_UNIDAD', IC.Operacion AS 'OPERACION',
				IC.Marca AS 'MARCA', IC.Modelo AS 'MODELO', ISNULL(IC.Mecanico,-1) AS 'Mecanico1', LTRIM(RTRIM(P1.NombreCompleto)) AS 'MECÁNICO',				ISNULL(IC.Electrico,-1) AS 'Electrico2', LTRIM(RTRIM(P2.NombreCompleto)) AS 'ELÉCTRICO', ISNULL(IC.Neumatico,-1) AS 'Neumatico3',
				LTRIM(RTRIM(P3.NombreCompleto)) AS 'NEUMÁTICO', ISNULL(IC.Soldador,-1) AS 'Soldador4', LTRIM(RTRIM(P4.NombreCompleto)) AS 'SOLDADOR',
				DATEADD(DAY,-10,FechaProyectada) AS 'PROX_INSPECCION', FechaProyectada AS 'PROX_MTTO',
				IC.FechaCrea AS 'FECHA_INSPECCION', (CASE WHEN (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Detalle
				WHERE Observacion != '' AND idInspeccionC = IC.idInspeccionC)) THEN 'OBSERVADO' ELSE 'CONFORME' END) AS 'ESTADO',
				(CASE WHEN (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Pedidos WHERE idInspeccionC = IC.idInspeccionC))
				THEN 'PEDIDO' ELSE 'SIN PEDIR' END) AS 'ESTADO_PEDIDO'
				FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera IC
				LEFT JOIN PersonaMast P1 ON P1.Persona = IC.Mecanico				LEFT JOIN PersonaMast P2 ON P2.Persona = IC.Electrico				LEFT JOIN PersonaMast P3 ON P3.Persona = IC.Neumatico
				LEFT JOIN PersonaMast P4 ON P4.Persona = IC.Soldador
				WHERE (IC.Ultimo = 1) AND (IC.Activo = 1)
				AND (IC.FechaCrea BETWEEN @FINICIO AND @FFIN) AND (IC.Placa IS NULL OR IC.Placa LIKE '%' + @Placa + '%')
				AND (IC.Tipo = @TipoMaquina)
			END
		END
	END

	IF (@Opcion = 2) BEGIN		-- LISTAR HISTORIAL DE INSPECCIONES
		IF (@TipoMaquina = 'TODOS') BEGIN
			SELECT IC.idInspeccionC AS 'NRO', IC.Placa AS 'PLACA', IC.Tipo AS 'TIPO_UNIDAD', IC.SubTipo AS 'SUBTIPO_UNIDAD', IC.Operacion AS 'OPERACION',
			IC.Marca AS 'MARCA', IC.Modelo AS 'MODELO', ISNULL(IC.Mecanico,-1) AS 'Mecanico1', LTRIM(RTRIM(P1.NombreCompleto)) AS 'MECÁNICO',			ISNULL(IC.Electrico,-1) AS 'Electrico2', LTRIM(RTRIM(P2.NombreCompleto)) AS 'ELÉCTRICO', ISNULL(IC.Neumatico,-1) AS 'Neumatico3',
			LTRIM(RTRIM(P3.NombreCompleto)) AS 'NEUMÁTICO', ISNULL(IC.Soldador,-1) AS 'Soldador4', LTRIM(RTRIM(P4.NombreCompleto)) AS 'SOLDADOR',
			DATEADD(DAY,-10,FechaProyectada) AS 'PROX_INSPECCION', FechaProyectada AS 'PROX_MTTO',
			IC.FechaCrea AS 'FECHA_INSPECCION', (CASE WHEN (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Detalle
			WHERE Observacion != '' AND idInspeccionC = IC.idInspeccionC)) THEN 'OBSERVADO' ELSE 'CONFORME' END) AS 'ESTADO',
			(CASE WHEN (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Pedidos WHERE idInspeccionC = IC.idInspeccionC))
			THEN 'PEDIDO' ELSE 'SIN PEDIR' END) AS 'ESTADO_PEDIDO'
			FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera IC
			LEFT JOIN PersonaMast P1 ON P1.Persona = IC.Mecanico			LEFT JOIN PersonaMast P2 ON P2.Persona = IC.Electrico			LEFT JOIN PersonaMast P3 ON P3.Persona = IC.Neumatico
			LEFT JOIN PersonaMast P4 ON P4.Persona = IC.Soldador
			WHERE (IC.Ultimo = 0) AND (IC.Activo = 1)
			AND (IC.FechaCrea BETWEEN @FINICIO AND @FFIN) AND (IC.Placa IS NULL OR IC.Placa LIKE '%' + @Placa + '%') 
		END
		ELSE BEGIN
			SELECT IC.idInspeccionC AS 'NRO', IC.Placa AS 'PLACA', IC.Tipo AS 'TIPO_UNIDAD', IC.SubTipo AS 'SUBTIPO_UNIDAD', IC.Operacion AS 'OPERACION',
			IC.Marca AS 'MARCA', IC.Modelo AS 'MODELO', ISNULL(IC.Mecanico,-1) AS 'Mecanico1', LTRIM(RTRIM(P1.NombreCompleto)) AS 'MECÁNICO',			ISNULL(IC.Electrico,-1) AS 'Electrico2', LTRIM(RTRIM(P2.NombreCompleto)) AS 'ELÉCTRICO', ISNULL(IC.Neumatico,-1) AS 'Neumatico3',
			LTRIM(RTRIM(P3.NombreCompleto)) AS 'NEUMÁTICO', ISNULL(IC.Soldador,-1) AS 'Soldador4', LTRIM(RTRIM(P4.NombreCompleto)) AS 'SOLDADOR',
			DATEADD(DAY,-10,FechaProyectada) AS 'PROX_INSPECCION', FechaProyectada AS 'PROX_MTTO', IC.FechaCrea AS 'FECHA_INSPECCION',
			(CASE WHEN (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Detalle
			WHERE Observacion != '' AND idInspeccionC = IC.idInspeccionC)) THEN 'OBSERVADO' ELSE 'CONFORME' END) AS 'ESTADO',
			(CASE WHEN (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Pedidos WHERE idInspeccionC = IC.idInspeccionC))
			THEN 'PEDIDO' ELSE 'SIN PEDIR' END) AS 'ESTADO_PEDIDO'
			FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera IC
			LEFT JOIN PersonaMast P1 ON P1.Persona = IC.Mecanico			LEFT JOIN PersonaMast P2 ON P2.Persona = IC.Electrico			LEFT JOIN PersonaMast P3 ON P3.Persona = IC.Neumatico
			LEFT JOIN PersonaMast P4 ON P4.Persona = IC.Soldador
			WHERE (IC.Ultimo = 0) AND (IC.Activo = 1)
			AND (IC.FechaCrea BETWEEN @FINICIO AND @FFIN) AND (IC.Placa IS NULL OR IC.Placa LIKE '%' + @Placa + '%')
			AND (IC.Tipo = @TipoMaquina)
		END
	END

	IF (@Opcion = 3) BEGIN		-- LISTAR OBSERVACIONES
		IF (@TipoMaquina = 'TODOS') BEGIN
			SELECT ID.idInspeccionD, ID.idInspeccionC, IC.Placa AS 'PLACA', IC.Tipo AS 'TIPO_UNIDAD', IC.SubTipo AS 'SUBTIPO_UNIDAD', IC.Operacion AS 'OPERACION',
			IC.Marca AS 'MARCA', IC.Modelo AS 'MODELO', IC.FechaCrea AS 'FECHA_INSPECCION', IP.TipoProceso AS 'COMPONENTE', IP.ParteTracto AS 'PROCESO',
			ID.Observacion AS 'OBSERVACION'
			FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Detalle ID
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera IC ON IC.idInspeccionC = ID.idInspeccionC
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_InspeccionProcesos IP ON IP.idProceso = ID.idProceso
			WHERE (ID.Observacion != '') AND (IC.Activo = 1) AND
			(IC.FechaCrea BETWEEN @FINICIO AND @FFIN) AND (IC.Placa IS NULL OR IC.Placa LIKE '%' + @Placa + '%')
			UNION
			SELECT ID.idInspeccionD, ID.idInspeccionC, IC.Placa AS 'PLACA', IC.Tipo AS 'TIPO_UNIDAD', IC.SubTipo AS 'SUBTIPO_UNIDAD', IC.Operacion AS 'OPERACION',
			IC.Marca AS 'MARCA', IC.Modelo AS 'MODELO', IC.FechaCrea AS 'FECHA_INSPECCION', ' ' AS 'COMPONENTE', IP.TipoProceso AS 'PROCESO',
			ID.Observacion AS 'OBSERVACION'
			FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_DetalleCR ID
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera IC ON IC.idInspeccionC = ID.idInspeccionC
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_InspeccionProcesosCR IP ON IP.idProcesoCR = ID.idProcesoCR
			WHERE (ID.Observacion != '') AND (IC.Activo = 1) AND
			(IC.FechaCrea BETWEEN @FINICIO AND @FFIN) AND (IC.Placa IS NULL OR IC.Placa LIKE '%' + @Placa + '%')
		END
		ELSE BEGIN
			SELECT ID.idInspeccionD, ID.idInspeccionC, IC.Placa AS 'PLACA', IC.Tipo AS 'TIPO_UNIDAD', IC.SubTipo AS 'SUBTIPO_UNIDAD', IC.Operacion AS 'OPERACION',
			IC.Marca AS 'MARCA', IC.Modelo AS 'MODELO', IC.FechaCrea AS 'FECHA_INSPECCION', IP.TipoProceso AS 'COMPONENTE', IP.ParteTracto AS 'PROCESO',
			ID.Observacion AS 'OBSERVACION'
			FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Detalle ID
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera IC ON IC.idInspeccionC = ID.idInspeccionC
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_InspeccionProcesos IP ON IP.idProceso = ID.idProceso
			WHERE (ID.Observacion != '') AND (IC.Activo = 1) AND
			(IC.FechaCrea BETWEEN @FINICIO AND @FFIN) AND (IC.Placa IS NULL OR IC.Placa LIKE '%' + @Placa + '%') AND (IC.Tipo = @TipoMaquina)
			UNION
			SELECT ID.idInspeccionD, ID.idInspeccionC, IC.Placa AS 'PLACA', IC.Tipo AS 'TIPO_UNIDAD', IC.SubTipo AS 'SUBTIPO_UNIDAD', IC.Operacion AS 'OPERACION',
			IC.Marca AS 'MARCA', IC.Modelo AS 'MODELO', IC.FechaCrea AS 'FECHA_INSPECCION', ' ' AS 'COMPONENTE', IP.TipoProceso AS 'PROCESO',
			ID.Observacion AS 'OBSERVACION'
			FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_DetalleCR ID
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera IC ON IC.idInspeccionC = ID.idInspeccionC
			LEFT JOIN ReportesApp_Mantenimiento_MttoPreventivo_InspeccionProcesosCR IP ON IP.idProcesoCR = ID.idProcesoCR
			WHERE (ID.Observacion != '') AND (IC.Activo = 1) AND
			(IC.FechaCrea BETWEEN @FINICIO AND @FFIN) AND (IC.Placa IS NULL OR IC.Placa LIKE '%' + @Placa + '%') AND (IC.Tipo = @TipoMaquina)
		END
	END
END

-------------------------------------------------------------------------------------


delete from ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Cabecera
where idInspeccionC IN (69,70,71)

delete FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_Detalle
where idInspeccionC IN (69,70,71)

delete FROM ReportesApp_Mantenimiento_MttoPreventivo_Inspeccion_DetalleCR
WHERE idInspeccionC IN (69,70,71)


