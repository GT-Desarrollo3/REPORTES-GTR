
-- CREAR TABLA ReportesApp_Operaciones_EntregaUnidad_AsignacionUnidad

----------------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 24-01-2025
-- Description:	REGISTRAR CONSTANCIA DE ASIGNACION
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_EntregaUnidad_BuscarConductor]
@IdTracto INT
AS
BEGIN
	IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_EntregaUnidad_AsignacionUnidad WHERE IdTracto = @IdTracto)) BEGIN
		SELECT TOP(1) ISNULL(R.CodConstanciaA,0) AS 'NroTicket', ISNULL(R.IdConductorNuevo,-1) AS 'IdConductor',
		RTRIM(ISNULL(C.Nombre,'NO TIENE CONDUCTOR')) AS 'Nombre'
		FROM ReportesApp_Operaciones_EntregaUnidad_AsignacionUnidad R
		LEFT JOIN OP_TR_Conductor C ON C.IdConductor = R.IdConductorNuevo
		WHERE /*R.Anio >= 2024 AND*/ (R.Aprobado = 1) /*AND (R.TipoProgramacion IN (1,2,3,4,9,10))*/ AND (R.IdTracto = @IdTracto)
		ORDER BY R.FechaCreacion DESC
	END
	ELSE BEGIN
		SELECT '0' AS 'NroTicket', -1 AS 'IdConductor', 'NO TIENE CONDUCTOR' AS 'Nombre'
	END
END

----------------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 22-01-2025
-- Description:	REGISTRAR CONSTANCIA DE ASIGNACION
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_EntregaUnidad_RegistrarEditarAsignacion]
@Opcion INT,
@CodConstanciaA VARCHAR(50),
@IdTracto INT,
@UltimoPreviaje INT,
@IdConductorAnt INT,
@IdConductorNuevo INT,
@Observacion VARCHAR(300),
@Usuario VARCHAR(20)
AS
DECLARE @Contador INT
DECLARE @idConstancia INT
DECLARE @Exito VARCHAR(MAX) = '0 = '

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- REGISTRAR CONSTANCIA
		IF (@IdConductorAnt = @IdConductorNuevo) BEGIN
			SET @Exito = '-1 = No puede generar una constancia de asignación con el mismo conductor.'
			ROLLBACK
			GOTO Terminar
		END

		IF (@Observacion NOT IN ('VACACIONES','COMPENSACIÓN','UNIDAD EN MANTENIMIENTO') AND @IdConductorNuevo = -1) BEGIN
			SET @Exito = '-2 = Por favor, ingrese el nombre del conductor.'
			ROLLBACK
			GOTO Terminar
		END
		ELSE BEGIN
			SET @Contador = (SELECT MAX(idConstanciaA) FROM ReportesApp_Operaciones_EntregaUnidad_AsignacionUnidad WHERE Anio = YEAR(GETDATE())) 
			SET @Contador = ISNULL(@Contador,0) + 1

			INSERT INTO ReportesApp_Operaciones_EntregaUnidad_AsignacionUnidad(idConstanciaA,Anio,CodConstanciaA,UltimoPreviaje,IdTracto,IdConductorAnt,IdConductorNuevo,
						Observacion,Aprobado,UsuarioCreacion,FechaCreacion)
			VALUES(@Contador,YEAR(GETDATE()),SUBSTRING(CONVERT(VARCHAR(20),YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@Contador)),6)),
			@UltimoPreviaje,@IdTracto,@IdConductorAnt,@IdConductorNuevo,@Observacion,0,@Usuario,GETDATE())

			SET @Exito = SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@Contador)),6))
		END
	END 

	IF (@Opcion = 2) BEGIN		-- ELIMINAR CONSTANCIA
		IF ((SELECT Aprobado FROM ReportesApp_Operaciones_EntregaUnidad_AsignacionUnidad WHERE CodConstanciaA = @CodConstanciaA) = 1) BEGIN
			SET @Exito = '-1 = No puede eliminar una asignación que ya fue aprobada.'
			ROLLBACK
			GOTO Terminar
		END
		ELSE BEGIN
			DELETE FROM ReportesApp_Operaciones_EntregaUnidad_AsignacionUnidad
			WHERE CodConstanciaA = @CodConstanciaA

			SET @Exito = '0 = Eliminado Correctamente'
		END
	END

	IF (@Opcion = 3) BEGIN		-- APROBAR CONSTANCIA
		UPDATE ReportesApp_Operaciones_EntregaUnidad_AsignacionUnidad
		SET Aprobado = 1
		WHERE CodConstanciaA = @CodConstanciaA

		SET @idConstancia = (SELECT MAX(idConstancia) FROM ReportesApp_Operaciones_EntregaUnidad_Constancia WHERE Anio = YEAR(GETDATE())) 
		SET @idConstancia = ISNULL(@idConstancia,0) + 1

		SET @IdTracto = (SELECT IdTracto FROM ReportesApp_Operaciones_EntregaUnidad_AsignacionUnidad WHERE CodConstanciaA = @CodConstanciaA)
		DECLARE @IdCarreta INT = -1
		SET @IdConductorAnt = (SELECT IdConductorAnt FROM ReportesApp_Operaciones_EntregaUnidad_AsignacionUnidad WHERE CodConstanciaA = @CodConstanciaA) 
		SET @IdConductorNuevo = (SELECT IdConductorNuevo FROM ReportesApp_Operaciones_EntregaUnidad_AsignacionUnidad WHERE CodConstanciaA = @CodConstanciaA) 
		SET @Observacion = (SELECT Observacion FROM ReportesApp_Operaciones_EntregaUnidad_AsignacionUnidad WHERE CodConstanciaA = @CodConstanciaA) 

		INSERT INTO ReportesApp_Operaciones_EntregaUnidad_Constancia(idConstancia,Anio,CodConstancia,CodConstanciaA,IdTracto,IdCarreta,IdConductorAnt,
					IdConductorNuevo,FechaSolicitud,Motivo,Estado,UsuarioCreacion,FechaCreacion,UsuarioModificacion,FechaModificacion)
		VALUES(@idConstancia,YEAR(GETDATE()),SUBSTRING(CONVERT(VARCHAR(20),YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@idConstancia)),6)),
		@CodConstanciaA,@IdTracto,@IdCarreta,@IdConductorAnt,@IdConductorNuevo,GETDATE(),@Observacion,'PENDIENTE',@Usuario,GETDATE(),@Usuario,GETDATE())

		SET @Exito = '0 = Actualizado correctamente'
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
IF LEFT(@Exito,1)='-' OR cast(left(@Exito,1) AS INT) < 0 BEGIN
	DECLARE @storedProcedureName VARCHAR(100)
	SELECT @storedProcedureName = OBJECT_NAME(@@PROCID)
	SET @Exito = @Exito + CHAR(13)+CHAR(13)+'SP Name: '+@storedProcedureName 
END

SELECT @Exito exito

----------------------------------------------------------------------------------------------------

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

----------------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 17-05-2024
-- Description:	LISTAR CONSTANCIA DE ENTREGA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_EntregaUnidad_ListarAsignaciones]
@Conductor VARCHAR(300),
@Placa VARCHAR(50),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@Operacion VARCHAR(50)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@Operacion = 'TODO') BEGIN
		SELECT AU.CodConstanciaA AS 'CODIGO', CASE WHEN AU.Aprobado = 0 THEN 'PENDIENTE' ELSE 'APROBADA' END AS 'ESTADO', V.NumeroPlaca AS 'UNIDAD',
		O.Descripcion AS 'OPERACION', AU.UltimoPreviaje AS 'ULTIMA_ASIGNACION', CA.Nombre AS 'COND_ANTERIOR', CN.Nombre AS 'COND_NUEVO',
		CONVERT(VARCHAR,AU.FechaCreacion,103) + ' ' + CONVERT(VARCHAR,AU.FechaCreacion,24) AS 'FECHA_PROG', AU.Observacion AS 'OBSERVACION', AU.UsuarioCreacion
		FROM ReportesApp_Operaciones_EntregaUnidad_AsignacionUnidad AU
		LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = AU.IdTracto
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON  O.IdOperacion = UC.IdProgramacion 
		LEFT JOIN OP_TR_Conductor CA ON CA.IdConductor = AU.IdConductorAnt
		LEFT JOIN OP_TR_Conductor CN ON CN.IdConductor = AU.IdConductorNuevo
		WHERE (@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND ((@Conductor IS NULL OR CN.Nombre LIKE '%' + @Conductor + '%') OR
		(@Conductor IS NULL OR CA.Nombre LIKE '%' + @Conductor + '%')) AND
		(AU.FechaCreacion BETWEEN @FINICIO AND @FFIN)
		ORDER BY AU.CodConstanciaA DESC
	END
	ELSE BEGIN
		SELECT AU.CodConstanciaA AS 'CODIGO', CASE WHEN AU.Aprobado = 0 THEN 'PENDIENTE' ELSE 'APROBADA' END AS 'ESTADO', V.NumeroPlaca AS 'UNIDAD',
		O.Descripcion AS 'OPERACION', AU.UltimoPreviaje AS 'ULTIMA_ASIGNACION', CA.Nombre AS 'COND_ANTERIOR', CN.Nombre AS 'COND_NUEVO',
		CONVERT(VARCHAR,AU.FechaCreacion,103) + ' ' + CONVERT(VARCHAR,AU.FechaCreacion,24) AS 'FECHA_PROG', AU.Observacion AS 'OBSERVACION', AU.UsuarioCreacion
		FROM ReportesApp_Operaciones_EntregaUnidad_AsignacionUnidad AU
		LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = AU.IdTracto
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON  O.IdOperacion = UC.IdProgramacion 
		LEFT JOIN OP_TR_Conductor CA ON CA.IdConductor = AU.IdConductorAnt
		LEFT JOIN OP_TR_Conductor CN ON CN.IdConductor = AU.IdConductorNuevo
		WHERE (@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND ((@Conductor IS NULL OR CN.Nombre LIKE '%' + @Conductor + '%') OR
		(@Conductor IS NULL OR CA.Nombre LIKE '%' + @Conductor + '%')) AND (AU.FechaCreacion BETWEEN @FINICIO AND @FFIN) AND (@Operacion = O.Descripcion)
		ORDER BY AU.CodConstanciaA DESC
	END
END

--------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 16-01-2024
-- Description:	REVISAR UNIDAD ASIGNADA A CONDUCTOR
-- =============================================
/*
SELECT * FROM OP_TR_Vehiculo WHERE NumeroPlaca = 'T8C-802'		    -- 2190
SELECT * FROM OP_TR_Conductor WHERE Nombre LIKE '%PLASENCIA CH%'		-- 9783

EXEC ReportesApp_Operaciones_Programacion_RevisarAsignaciones @idTracto = 2190, @idConductor = 9783, @TipoOperacion = 2
*/
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Programacion_RevisarAsignaciones]
@idTracto INT,
@idConductor INT,
@TipoOperacion INT
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	DECLARE @Placa VARCHAR(10) = (SELECT NumeroPlaca FROM OP_TR_Vehiculo WHERE IdVehiculo = @idTracto)
	
	IF (CONVERT(DATE,GETDATE()) >= CONVERT(DATE,'02/01/2025')) BEGIN
		IF (@TipoOperacion IN (1,2,3,4,9,10)) BEGIN
			IF ((SELECT IndTercero FROM OP_TR_Vehiculo WHERE IdVehiculo = @idTracto) = 'P') BEGIN
				IF EXISTS(SELECT TOP(1) * FROM ReportesApp_Operaciones_EntregaUnidad_AsignacionUnidad WHERE IdTracto = @idTracto
				ORDER BY FechaCreacion DESC) BEGIN
					DECLARE @ConductorAsignado INT = (SELECT TOP(1) IdConductorNuevo FROM ReportesApp_Operaciones_EntregaUnidad_AsignacionUnidad WHERE IdTracto = @idTracto
													  ORDER BY FechaCreacion DESC)
					DECLARE @Aprobado INT = (SELECT TOP(1) Aprobado FROM ReportesApp_Operaciones_EntregaUnidad_AsignacionUnidad WHERE IdTracto = @idTracto
											 ORDER BY FechaCreacion DESC)
					DECLARE @Observacion VARCHAR(250) = (SELECT TOP(1) Observacion FROM ReportesApp_Operaciones_EntregaUnidad_AsignacionUnidad WHERE IdTracto = @idTracto
														 ORDER BY FechaCreacion DESC)

					IF (@Observacion IN ('VACACIONES','COMPENSACIÓN')) BEGIN
						IF (@Aprobado = 1) BEGIN
							SET @Exito = '0 = Esta unidad SÍ está asignada al conductor.'
						END
						ELSE BEGIN
							SET @Exito = '-2 = La asignación de la unidad a este conductor aún no ha sido aprobada. Favor de realizar la aprobación.'
						END
					END
					ELSE BEGIN
						IF (@idConductor = @ConductorAsignado) BEGIN
							IF (@Aprobado = 1) BEGIN
								SET @Exito = '0 = Esta unidad SÍ está asignada al conductor.'
							END
							ELSE BEGIN
								SET @Exito = '-2 = La asignación de la unidad a este conductor aún no ha sido aprobada. Favor de realizar la aprobación.'
							END
						END
						ELSE BEGIN
							DECLARE @Conductor VARCHAR(250) = (SELECT LTRIM(RTRIM(Nombre)) FROM OP_TR_Conductor WHERE IdConductor = @ConductorAsignado)
							DECLARE @CodConstancia VARCHAR(50) = (SELECT TOP(1) CodConstanciaA FROM ReportesApp_Operaciones_EntregaUnidad_AsignacionUnidad
														  WHERE IdConductorNuevo = @ConductorAsignado ORDER BY FechaCreacion DESC)
							DECLARE @UltimaFecha DATE = (SELECT TOP(1) CONVERT(DATE,FechaCreacion) FROM ReportesApp_Operaciones_EntregaUnidad_AsignacionUnidad
														 WHERE IdConductorNuevo = @ConductorAsignado ORDER BY FechaCreacion DESC)

							SET @Exito = '-1 = La unidad '+ @Placa +' fue asignada al conductor '+@Conductor+' en la constancia '+ @CodConstancia +
										 ' del día '+CONVERT(VARCHAR(40),@UltimaFecha,103) +'. Favor de realizar una nueva constancia de asignación.'
						END
					END
				END
				ELSE BEGIN
					IF (EXISTS(SELECT TOP(1) IdConductor FROM ReportesApp_Operacion_Previaje_Registros WHERE (Estado = 9) AND (IdTracto = @idTracto) AND
					YEAR(FechaProgramacion) >= 2024 ORDER BY FechaProgramacion DESC)) BEGIN
						DECLARE @UltimoConductor2 INT = (SELECT TOP(1) ISNULL(IdConductor,-1) FROM ReportesApp_Operacion_Previaje_Registros
														WHERE (Estado = 9) AND (IdTracto = @idTracto) AND YEAR(FechaProgramacion) >= 2024
														ORDER BY FechaProgramacion DESC)
						DECLARE @UltimaFecha2 DATE = (SELECT TOP(1) CONVERT(DATE,FechaProgramacion) FROM ReportesApp_Operacion_Previaje_Registros
													 WHERE (Estado = 9) AND (IdTracto = @idTracto) AND YEAR(FechaProgramacion) >= 2024
													 ORDER BY FechaProgramacion DESC)
						DECLARE @NroTicket2 INT = (SELECT TOP(1) NroTicket FROM ReportesApp_Operacion_Previaje_Registros
												  WHERE (Estado = 9) AND (idTracto = @idTracto) AND YEAR(FechaProgramacion) >= 2024
												  ORDER BY FechaProgramacion DESC)
						DECLARE @Conductor3 VARCHAR(250) = (SELECT LTRIM(RTRIM(Nombre)) FROM OP_TR_Conductor WHERE IdConductor = @UltimoConductor2)

						IF (@idConductor = @UltimoConductor2) BEGIN
							PRINT CONVERT(VARCHAR,@NroTicket2) + ' - ' + @Conductor3
							SET @Exito = '0 = Esta unidad SÍ está asignada al conductor.'
						END
						ELSE BEGIN
							SET @Exito = '-1 = La unidad '+ @Placa +' fue asignada al conductor '+@Conductor3+' en el previaje '+ CONVERT(VARCHAR,@NroTicket2) +
										 ' del día '+CONVERT(VARCHAR(40),@UltimaFecha2,103) +'. Favor de realizar la constancia de asignación.'
						END
					END
					ELSE BEGIN
						PRINT '0'
						SET @Exito = '0 = Esta unidad SÍ está asignada al conductor.'
					END
				END
			END
			ELSE BEGIN
				PRINT '1'
				SET @Exito = '0 = Esta unidad SÍ está asignada al conductor.'
			END
		END
		ELSE BEGIN
			PRINT '2'
			SET @Exito = '0 = Esta unidad SÍ está asignada al conductor.'
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
