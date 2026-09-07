
-- CREAR TABLA ReportesApp_Operaciones_EntregaUnidad_Constancia

-- CREAR TABLA ReportesApp_Operaciones_EntregaUnidad_Motivos Y LLENARLA

--------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 14-05-2024
-- Description:	LISTAR MOTIVOS CONSTANCIA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_EntregaUnidad_ListarMotivos]
@Opcion INT,
@Conductor VARCHAR(250)
AS
BEGIN
	 IF (@Opcion = 1) BEGIN		-- LISTAR CONDUCTORES
		SELECT TOP(15) C.IdConductor, C.Nombre FROM OP_TR_Conductor C
		LEFT JOIN PersonaMast P ON P.Persona = C.IdPersona
		WHERE (P.Estado = 'A') AND (C.Nombre LIKE '%' + @Conductor + '%')
	 END

	 IF (@Opcion = 2) BEGIN		-- LISTAR MOTIVOS
		SELECT idMotivo, Descripcion FROM ReportesApp_Operaciones_EntregaUnidad_Motivos
	 END
END

--------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 14-05-2024
-- Description:	REGISTRAR TICKET DE CONSTANCIA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_EntregaUnidad_RegistrarEditarConstancia]
@Opcion INT,
@idConstancia INT,
@IdTracto INT,
@IdCarreta INT,
@IdConductorAnt INT,
@IdConductorNuevo INT,
@FechaSolicitud VARCHAR(50),
@HoraSolicitud VARCHAR(50),
@Motivo VARCHAR(100),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX) = '0 = '

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- REGISTRAR CONSTANCIA
		IF (CONVERT(DATE, @FechaSolicitud) < CONVERT(DATE,GETDATE())) BEGIN
			SET @Exito = '-1 = No puede generar una constancia en una fecha menor a la actual.'
			ROLLBACK
			GOTO Terminar
		END

		SET @idConstancia = (SELECT MAX(idConstancia) FROM ReportesApp_Operaciones_EntregaUnidad_Constancia WHERE Anio = YEAR(GETDATE())) 
		SET @idConstancia = ISNULL(@idConstancia,0) + 1

		INSERT INTO ReportesApp_Operaciones_EntregaUnidad_Constancia(idConstancia,Anio,CodConstancia,IdTracto,IdCarreta,IdConductorAnt,IdConductorNuevo,FechaSolicitud,
					Motivo,Estado,UsuarioCreacion,FechaCreacion,UsuarioModificacion,FechaModificacion)
		VALUES(@idConstancia,YEAR(GETDATE()),SUBSTRING(CONVERT(VARCHAR(20),YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@idConstancia)),6)),
		@IdTracto,@IdCarreta,@IdConductorAnt,@IdConductorNuevo,CONVERT(DATETIME,@FechaSolicitud+' '+@HoraSolicitud),@Motivo,'PENDIENTE',@Usuario,GETDATE(),@Usuario,GETDATE())

		SET @Exito = SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@idConstancia)),6))
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
-- Create date: 16-05-2024
-- Description:	LISTAR CONSTANCIA UNIDAD
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_EntregaUnidad_ListarTicket]
@CodConstancia VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT EU.CodConstancia, V.NumeroPlaca AS 'TRACTO', S.NumeroPlaca AS 'CARRETA', CA.Nombre AS 'COND_ANT', CN.Nombre AS 'COND_NUEVO',
	CONVERT(VARCHAR,EU.FechaSolicitud,103) + ' ' + CONVERT(VARCHAR,EU.FechaSolicitud,24) AS 'FECHA', EU.Motivo AS 'MOTIVO'
	FROM ReportesApp_Operaciones_EntregaUnidad_Constancia EU
	LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = EU.IdTracto
	LEFT JOIN OP_TR_Vehiculo S ON S.IdVehiculo = EU.IdCarreta
	LEFT JOIN OP_TR_Conductor CA ON CA.IdConductor = EU.IdConductorAnt
	LEFT JOIN OP_TR_Conductor CN ON CN.IdConductor = EU.IdConductorNuevo
	WHERE (EU.CodConstancia = @CodConstancia)
END

--------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 17-05-2024
-- Description:	LISTAR CONSTANCIA DE ENTREGA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_EntregaUnidad_ListarConstancia]
@CodConstancia VARCHAR(50),
@Placa VARCHAR(50),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@Estado VARCHAR(20),
@Operacion VARCHAR(50)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@Estado = 'TODOS') BEGIN
		IF (@Operacion = 'TODO') BEGIN
			SELECT EU.CodConstancia AS 'CODIGO', V.NumeroPlaca AS 'TRACTO', S.NumeroPlaca AS 'CARRETA', O.Descripcion AS 'OPERACION',
			CA.Nombre AS 'COND_ANTERIOR', CN.Nombre AS 'COND_NUEVO', CONVERT(VARCHAR,EU.FechaSolicitud,103) + ' ' + CONVERT(VARCHAR,EU.FechaSolicitud,24) AS 'FECHA_PROG',
			EU.Motivo AS 'MOTIVO', EU.Estado AS 'ESTADO', ISNULL(CONVERT(VARCHAR,EU.HoraInicio,24),'') AS 'HORA_INICIO', ISNULL(CONVERT(VARCHAR,EU.HoraFin,24),'') AS 'HORA_FIN',
			EU.Observacion AS 'OBSERVACION', EU.UsuarioCreacion, EU.FechaCreacion, EU.UsuarioModificacion, EU.FechaModificacion
			FROM ReportesApp_Operaciones_EntregaUnidad_Constancia EU
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = EU.IdTracto
			LEFT JOIN OP_TR_Vehiculo S ON S.IdVehiculo = EU.IdCarreta
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON  O.IdOperacion = UC.IdProgramacion 
			LEFT JOIN OP_TR_Conductor CA ON CA.IdConductor = EU.IdConductorAnt
			LEFT JOIN OP_TR_Conductor CN ON CN.IdConductor = EU.IdConductorNuevo
			WHERE (@CodConstancia IS NULL OR EU.CodConstancia LIKE '%' + @CodConstancia + '%') AND (@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%')
			AND (EU.FechaSolicitud BETWEEN @FINICIO AND @FFIN)
			ORDER BY EU.CodConstancia DESC
		END
		ELSE BEGIN
			SELECT EU.CodConstancia AS 'CODIGO', V.NumeroPlaca AS 'TRACTO', S.NumeroPlaca AS 'CARRETA', O.Descripcion AS 'OPERACION',
			CA.Nombre AS 'COND_ANTERIOR', CN.Nombre AS 'COND_NUEVO', CONVERT(VARCHAR,EU.FechaSolicitud,103) + ' ' + CONVERT(VARCHAR,EU.FechaSolicitud,24) AS 'FECHA_PROG',
			EU.Motivo AS 'MOTIVO', EU.Estado AS 'ESTADO', ISNULL(CONVERT(VARCHAR,EU.HoraInicio,24),'') AS 'HORA_INICIO', ISNULL(CONVERT(VARCHAR,EU.HoraFin,24),'') AS 'HORA_FIN',
			EU.Observacion AS 'OBSERVACION', EU.UsuarioCreacion, EU.FechaCreacion, EU.UsuarioModificacion, EU.FechaModificacion
			FROM ReportesApp_Operaciones_EntregaUnidad_Constancia EU
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = EU.IdTracto
			LEFT JOIN OP_TR_Vehiculo S ON S.IdVehiculo = EU.IdCarreta
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON  O.IdOperacion = UC.IdProgramacion 
			LEFT JOIN OP_TR_Conductor CA ON CA.IdConductor = EU.IdConductorAnt
			LEFT JOIN OP_TR_Conductor CN ON CN.IdConductor = EU.IdConductorNuevo
			WHERE (@CodConstancia IS NULL OR EU.CodConstancia LIKE '%' + @CodConstancia + '%') AND (@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%')
			AND (EU.FechaSolicitud BETWEEN @FINICIO AND @FFIN) AND (@Operacion = O.Descripcion)
			ORDER BY EU.CodConstancia DESC
		END
	END
	ELSE BEGIN
		IF (@Operacion = 'TODO') BEGIN
			SELECT EU.CodConstancia AS 'CODIGO', V.NumeroPlaca AS 'TRACTO', S.NumeroPlaca AS 'CARRETA', O.Descripcion AS 'OPERACION',
			CA.Nombre AS 'COND_ANTERIOR', CN.Nombre AS 'COND_NUEVO', CONVERT(VARCHAR,EU.FechaSolicitud,103) + ' ' + CONVERT(VARCHAR,EU.FechaSolicitud,24) AS 'FECHA_PROG',
			EU.Motivo AS 'MOTIVO', EU.Estado AS 'ESTADO', ISNULL(CONVERT(VARCHAR,EU.HoraInicio,24),'') AS 'HORA_INICIO', ISNULL(CONVERT(VARCHAR,EU.HoraFin,24),'') AS 'HORA_FIN',
			EU.Observacion AS 'OBSERVACION', EU.UsuarioCreacion, EU.FechaCreacion, EU.UsuarioModificacion, EU.FechaModificacion
			FROM ReportesApp_Operaciones_EntregaUnidad_Constancia EU
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = EU.IdTracto
			LEFT JOIN OP_TR_Vehiculo S ON S.IdVehiculo = EU.IdCarreta
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON  O.IdOperacion = UC.IdProgramacion 
			LEFT JOIN OP_TR_Conductor CA ON CA.IdConductor = EU.IdConductorAnt
			LEFT JOIN OP_TR_Conductor CN ON CN.IdConductor = EU.IdConductorNuevo
			WHERE (@CodConstancia IS NULL OR EU.CodConstancia LIKE '%' + @CodConstancia + '%') AND (@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%')
			AND (EU.FechaSolicitud BETWEEN @FINICIO AND @FFIN) AND (@Estado = EU.Estado)
			ORDER BY EU.CodConstancia DESC
		END
		ELSE BEGIN
			SELECT EU.CodConstancia AS 'CODIGO', V.NumeroPlaca AS 'TRACTO', S.NumeroPlaca AS 'CARRETA', O.Descripcion AS 'OPERACION',
			CA.Nombre AS 'COND_ANTERIOR', CN.Nombre AS 'COND_NUEVO', CONVERT(VARCHAR,EU.FechaSolicitud,103) + ' ' + CONVERT(VARCHAR,EU.FechaSolicitud,24) AS 'FECHA_PROG',
			EU.Motivo AS 'MOTIVO', EU.Estado AS 'ESTADO', ISNULL(CONVERT(VARCHAR,EU.HoraInicio,24),'') AS 'HORA_INICIO', ISNULL(CONVERT(VARCHAR,EU.HoraFin,24),'') AS 'HORA_FIN',
			EU.Observacion AS 'OBSERVACION', EU.UsuarioCreacion, EU.FechaCreacion, EU.UsuarioModificacion, EU.FechaModificacion
			FROM ReportesApp_Operaciones_EntregaUnidad_Constancia EU
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = EU.IdTracto
			LEFT JOIN OP_TR_Vehiculo S ON S.IdVehiculo = EU.IdCarreta
			LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
			LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON  O.IdOperacion = UC.IdProgramacion 
			LEFT JOIN OP_TR_Conductor CA ON CA.IdConductor = EU.IdConductorAnt
			LEFT JOIN OP_TR_Conductor CN ON CN.IdConductor = EU.IdConductorNuevo
			WHERE (@CodConstancia IS NULL OR EU.CodConstancia LIKE '%' + @CodConstancia + '%') AND (@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%')
			AND (EU.FechaSolicitud BETWEEN @FINICIO AND @FFIN) AND (@Estado = EU.Estado) AND (@Operacion = O.Descripcion)
			ORDER BY EU.CodConstancia DESC
		END
	END
END

-------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 18-05-2024
-- Description:	MODIFICAR CONSTANCIAS DE ENTREGA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_EntregaUnidad_ModificarEliminarConstancia]
@Opcion INT,
@CodConstancia VARCHAR(50),
@HoraInicio DATETIME,
@HoraFin DATETIME,
@Observacion VARCHAR(300),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX) = '0 = Constancia Actualizada'

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- APROBAR CONSTANCIA
		DECLARE @Estado VARCHAR(40) = (SELECT Estado FROM ReportesApp_Operaciones_EntregaUnidad_Constancia WHERE CodConstancia = @CodConstancia)

		IF (@Estado = 'PENDIENTE') BEGIN
			UPDATE ReportesApp_Operaciones_EntregaUnidad_Constancia
			SET Estado = 'APROBADA', UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()
			WHERE CodConstancia = @CodConstancia
		END
		ELSE BEGIN
			UPDATE ReportesApp_Operaciones_EntregaUnidad_Constancia
			SET Estado = 'PENDIENTE', UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()
			WHERE CodConstancia = @CodConstancia
		END
	END

	IF (@Opcion = 2) BEGIN		-- INGRESAR HORA INICIO
		UPDATE ReportesApp_Operaciones_EntregaUnidad_Constancia
		SET HoraInicio = GETDATE(), UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()
		WHERE CodConstancia = @CodConstancia
	END

	IF (@Opcion = 3) BEGIN		-- INGRESAR HORA FIN
		UPDATE ReportesApp_Operaciones_EntregaUnidad_Constancia
		SET HoraFin = GETDATE(), UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()
		WHERE CodConstancia = @CodConstancia
	END

	IF (@Opcion = 4) BEGIN		-- MODIFICAR Y AÑADIR OBSERVACION
		UPDATE ReportesApp_Operaciones_EntregaUnidad_Constancia
		SET HoraInicio = @HoraInicio, HoraFin = @HoraFin, Observacion = @Observacion, UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()
		WHERE CodConstancia = @CodConstancia
	END

	IF (@Opcion = 5) BEGIN		-- ELIMINAR CONSTANCIA
		DELETE FROM ReportesApp_Operaciones_EntregaUnidad_Constancia
		WHERE CodConstancia = @CodConstancia
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