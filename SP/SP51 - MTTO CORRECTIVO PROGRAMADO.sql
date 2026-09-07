
-- CREAR TABLA ReportesApp_Mantenimiento_MttoCorrectivo_Registro

-- CREAR TABLA ReportesApp_Mantenimiento_MttoCorrectivo_CalendarioView

-----------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 08-09-2023
-- Description:	INSERTAR SOLICITUD
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_Solicitudes_Insertar]
@idTracto INT,
@TipoOperacion INT,
@Kilometraje DECIMAL(16,2),
@idBase INT,
@idCisterna INT,
@UnidadFalla VARCHAR(20),
@Usuario VARCHAR(50)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT
DECLARE @correlativo2 INT

SET @Exito = '0 = Solicitud añadida.'

/* IF (EXISTS(SELECT NroTicket FROM ReportesApp_Mantenimiento_Solicitud_Registro WHERE NroTicket = @NroTicket)) BEGIN
	SET @Exito = '-1 = Esta placa ya ha sido registrada.'
	GOTO Terminar
END */

SET @correlativo = (SELECT MAX(idSolicitud) FROM ReportesApp_Mantenimiento_Solicitud_Registro)
SET @correlativo = ISNULL(@correlativo, 0) + 1

BEGIN TRAN
BEGIN TRY
	INSERT INTO ReportesApp_Mantenimiento_Solicitud_Registro(idSolicitud, idTracto, TipoOperacion, Kilometraje, idBase,
															 idCisterna, CodEstado, Estado, UnidadFalla, UsuarioCreacion, FechaCreacion)
	VALUES(@correlativo, @idTracto, @TipoOperacion, @Kilometraje, @idBase, @idCisterna, 'SM', 'SOLICITADO', @UnidadFalla, @Usuario, GETDATE())

	UPDATE ReportesApp_Mantenimiento_Solicitud_RegistroDetalle 
	SET idSolicitud = @correlativo
	WHERE idSolicitud IS NULL

	DECLARE @Contador INT = 1

	WHILE (@Contador <= (SELECT MAX(idSolicitudDetalle) FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle WHERE idSolicitud = @correlativo)) BEGIN
		SET @correlativo2 = (SELECT MAX(idMttoC) FROM ReportesApp_Mantenimiento_MttoCorrectivo_Registro)
		SET @correlativo2 = ISNULL(@correlativo2, 0) + 1

		DECLARE @Placa VARCHAR(20) = (SELECT NumeroPlaca FROM OP_TR_Vehiculo WHERE IdVehiculo = @idTracto)

		INSERT INTO ReportesApp_Mantenimiento_MttoCorrectivo_Registro(idMttoC, Placa, Operacion, Origen, FechaReportada, Sistema, Descripcion, Observacion,
		Estado, UsuarioCreacion, FechaCreacion, UsuarioModificacion, FechaModificacion)
		SELECT @correlativo2, @Placa, OP.Descripcion, 'SOLICITUD', RC.FechaCreacion, C1.Descripcion, CD.Descripcion, RD.Observacion, 'PENDIENTE',
		@Usuario, GETDATE(), @Usuario, GETDATE()
		FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle RD
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Registro RC ON RC.idSolicitud = RD.idSolicitud
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC WITH(NOLOCK) ON UC.IdUnidad = RC.idTracto
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = UC.IdProgramacion
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Componente C1 ON C1.idComponente = RD.idComponente
		LEFT JOIN ReportesApp_Mantenimiento_Solicitud_Componente_Detalle CD ON CD.idComponenteDetalle = RD.idComponenteDetalle
		WHERE RD.idSolicitud = @correlativo AND RD.idSolicitudDetalle = @Contador

		SET @Contador = @Contador + 1
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

-------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 06-09-2023
-- Description:	EDITAR SOLICITUDES DETALLES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_SolicitudDetalle_Editar]
@Opcion INT,
@idSolicitudDetalle INT,
@idSolicitud INT,
@OT VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Modificado correctamente.'

IF (@idSolicitudDetalle = 0) BEGIN
	SET @Exito = '-1 = La fila seleccionado no existe.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN  -- ELIMINAR
		IF @idSolicitud = 0 BEGIN
			DELETE FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle
			WHERE idSolicitudDetalle = @idSolicitudDetalle AND idSolicitud IS NULL
		END
		ELSE BEGIN
			DECLARE @idVehiculo3 INT = (SELECT idTracto FROM ReportesApp_Mantenimiento_Solicitud_Registro WHERE idSolicitud = @idSolicitud)
			DECLARE @Placa3 VARCHAR(20) = (SELECT NumeroPlaca FROM OP_TR_Vehiculo WHERE IdVehiculo = @idVehiculo3)
			DECLARE @Observacion3 VARCHAR(250) = (SELECT Observacion FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle WHERE idSolicitudDetalle = @idSolicitudDetalle AND idSolicitud = @idSolicitud)
			
			DELETE FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle
			WHERE idSolicitudDetalle = @idSolicitudDetalle AND idSolicitud = @idSolicitud

			DELETE FROM ReportesApp_Mantenimiento_MttoCorrectivo_Registro
			WHERE Placa = @Placa3 AND Observacion = @Observacion3 AND Origen = 'SOLICITUD'
		END
	END

	IF (@Opcion = 2) BEGIN  -- ASIGNAR OT
		DECLARE @idVehiculo2 INT = (SELECT idTracto FROM ReportesApp_Mantenimiento_Solicitud_Registro WHERE idSolicitud = @idSolicitud)
		DECLARE @Placa2 VARCHAR(20) = (SELECT NumeroPlaca FROM OP_TR_Vehiculo WHERE IdVehiculo = @idVehiculo2)
		DECLARE @Observacion2 VARCHAR(250) = (SELECT Observacion FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle WHERE idSolicitudDetalle = @idSolicitudDetalle AND idSolicitud = @idSolicitud)

		UPDATE ReportesApp_Mantenimiento_Solicitud_RegistroDetalle
		SET idOT = @OT, Estado = 'TERMINADO'
		WHERE idSolicitudDetalle = @idSolicitudDetalle AND idSolicitud = @idSolicitud

		UPDATE ReportesApp_Mantenimiento_MttoCorrectivo_Registro
		SET OT = @OT, Estado = 'TERMINADO'
		WHERE Placa = @Placa2 AND Observacion = @Observacion2 AND Origen = 'SOLICITUD'

		IF EXISTS(SELECT FechaProgramacion FROM ReportesApp_Mantenimiento_MttoCorrectivo_Registro WHERE Placa = @Placa2 AND Observacion = @Observacion2
		AND Origen = 'SOLICITUD') BEGIN
			DECLARE @FechaProg DATETIME = (SELECT FechaProgramacion FROM ReportesApp_Mantenimiento_MttoCorrectivo_Registro WHERE Placa = @Placa2 AND Observacion = @Observacion2 AND Origen = 'SOLICITUD')
			DECLARE @MTTOC TABLE (Numero INT, idMttoC INT, Placa VARCHAR(20), FechaProgramacion DATETIME)
			DECLARE @Contador INT = 1

			INSERT INTO @MTTOC(Numero, idMttoC, Placa, FechaProgramacion)
			SELECT ROW_NUMBER() OVER(ORDER BY idMttoC ASC) AS 'Numero', idMttoC, Placa, FechaProgramacion FROM ReportesApp_Mantenimiento_MttoCorrectivo_Registro
			WHERE Placa = @Placa2 AND YEAR(FechaProgramacion) = YEAR(@FechaProg) AND MONTH(FechaProgramacion) = MONTH(@FechaProg) AND Estado = 'PENDIENTE'

			UPDATE C
			SET C.D1 = NULL, C.D2 = NULL, C.D3 = NULL, C.D4 = NULL, C.D5 = NULL, C.D6 = NULL, C.D7 = NULL, C.D8 = NULL, C.D9 = NULL, C.D10 = NULL,
			C.D11 = NULL, C.D12 = NULL, C.D13 = NULL, C.D14 = NULL, C.D15 = NULL, C.D16 = NULL, C.D17 = NULL, C.D18 = NULL, C.D19 = NULL, C.D20 = NULL,
			C.D21 = NULL, C.D22 = NULL, C.D23 = NULL, C.D24 = NULL, C.D25 = NULL, C.D26 = NULL, C.D27 = NULL, C.D28 = NULL, C.D29 = NULL, C.D30 = NULL, C.D31 = NULL
			FROM ReportesApp_Mantenimiento_MttoCorrectivo_CalendarioView C
			WHERE C.Placa = @Placa2 AND C.Anio = YEAR(@FechaProg) AND C.Mes = MONTH(@FechaProg)

			WHILE (@Contador <= (SELECT COUNT(Numero) FROM @MTTOC)) BEGIN
				DECLARE @MPlaca VARCHAR(20) = (SELECT Placa FROM @MTTOC WHERE Numero = @Contador)
				DECLARE @MFechaProgramacion DATE = (SELECT CONVERT(DATE,FechaProgramacion) FROM @MTTOC WHERE Numero = @Contador)

				UPDATE C
				SET C.D1 = CASE WHEN DAY(@MFechaProgramacion) = 1 THEN ISNULL(C.D1,0) + 1 ELSE C.D1 END,
					C.D2 = CASE WHEN DAY(@MFechaProgramacion) = 2 THEN ISNULL(C.D2,0) + 1 ELSE C.D2 END,
					C.D3 = CASE WHEN DAY(@MFechaProgramacion) = 3 THEN ISNULL(C.D3,0) + 1 ELSE C.D3 END,
					C.D4 = CASE WHEN DAY(@MFechaProgramacion) = 4 THEN ISNULL(C.D4,0) + 1 ELSE C.D4 END,
					C.D5 = CASE WHEN DAY(@MFechaProgramacion) = 5 THEN ISNULL(C.D5,0) + 1 ELSE C.D5 END,
					C.D6 = CASE WHEN DAY(@MFechaProgramacion) = 6 THEN ISNULL(C.D6,0) + 1 ELSE C.D6 END,
					C.D7 = CASE WHEN DAY(@MFechaProgramacion) = 7 THEN ISNULL(C.D7,0) + 1 ELSE C.D7 END,
					C.D8 = CASE WHEN DAY(@MFechaProgramacion) = 8 THEN ISNULL(C.D8,0) + 1 ELSE C.D8 END,
					C.D9 = CASE WHEN DAY(@MFechaProgramacion) = 9 THEN ISNULL(C.D9,0) + 1 ELSE C.D9 END,
					C.D10 = CASE WHEN DAY(@MFechaProgramacion) = 10 THEN ISNULL(C.D10,0) + 1 ELSE C.D10 END,
					C.D11 = CASE WHEN DAY(@MFechaProgramacion) = 11 THEN ISNULL(C.D11,0) + 1 ELSE C.D11 END,
					C.D12 = CASE WHEN DAY(@MFechaProgramacion) = 12 THEN ISNULL(C.D12,0) + 1 ELSE C.D12 END,
					C.D13 = CASE WHEN DAY(@MFechaProgramacion) = 13 THEN ISNULL(C.D13,0) + 1 ELSE C.D13 END,
					C.D14 = CASE WHEN DAY(@MFechaProgramacion) = 14 THEN ISNULL(C.D14,0) + 1 ELSE C.D14 END,
					C.D15 = CASE WHEN DAY(@MFechaProgramacion) = 15 THEN ISNULL(C.D15,0) + 1 ELSE C.D15 END,
					C.D16 = CASE WHEN DAY(@MFechaProgramacion) = 16 THEN ISNULL(C.D16,0) + 1 ELSE C.D16 END,
					C.D17 = CASE WHEN DAY(@MFechaProgramacion) = 17 THEN ISNULL(C.D17,0) + 1 ELSE C.D17 END,
					C.D18 = CASE WHEN DAY(@MFechaProgramacion) = 18 THEN ISNULL(C.D18,0) + 1 ELSE C.D18 END,
					C.D19 = CASE WHEN DAY(@MFechaProgramacion) = 19 THEN ISNULL(C.D19,0) + 1 ELSE C.D19 END,
					C.D20 = CASE WHEN DAY(@MFechaProgramacion) = 20 THEN ISNULL(C.D20,0) + 1 ELSE C.D20 END,
					C.D21 = CASE WHEN DAY(@MFechaProgramacion) = 21 THEN ISNULL(C.D21,0) + 1 ELSE C.D21 END,
					C.D22 = CASE WHEN DAY(@MFechaProgramacion) = 22 THEN ISNULL(C.D22,0) + 1 ELSE C.D22 END,
					C.D23 = CASE WHEN DAY(@MFechaProgramacion) = 23 THEN ISNULL(C.D23,0) + 1 ELSE C.D23 END,
					C.D24 = CASE WHEN DAY(@MFechaProgramacion) = 24 THEN ISNULL(C.D24,0) + 1 ELSE C.D24 END,
					C.D25 = CASE WHEN DAY(@MFechaProgramacion) = 25 THEN ISNULL(C.D25,0) + 1 ELSE C.D25 END,
					C.D26 = CASE WHEN DAY(@MFechaProgramacion) = 26 THEN ISNULL(C.D26,0) + 1 ELSE C.D26 END,
					C.D27 = CASE WHEN DAY(@MFechaProgramacion) = 27 THEN ISNULL(C.D27,0) + 1 ELSE C.D27 END,
					C.D28 = CASE WHEN DAY(@MFechaProgramacion) = 28 THEN ISNULL(C.D28,0) + 1 ELSE C.D28 END,
					C.D29 = CASE WHEN DAY(@MFechaProgramacion) = 29 THEN ISNULL(C.D29,0) + 1 ELSE C.D29 END,
					C.D30 = CASE WHEN DAY(@MFechaProgramacion) = 30 THEN ISNULL(C.D30,0) + 1 ELSE C.D30 END,
					C.D31 = CASE WHEN DAY(@MFechaProgramacion) = 31 THEN ISNULL(C.D31,0) + 1 ELSE C.D31 END
				FROM ReportesApp_Mantenimiento_MttoCorrectivo_CalendarioView C
				WHERE C.Placa = @MPlaca AND C.Anio = YEAR(@MFechaProgramacion) AND C.Mes = MONTH(@MFechaProgramacion)

				SET @Contador = @Contador + 1
			END
		END
	END

	IF (@Opcion = 3) BEGIN  -- CAMBIAR ESTADO DETALLE
		DECLARE @idVehiculo INT = (SELECT idTracto FROM ReportesApp_Mantenimiento_Solicitud_Registro WHERE idSolicitud = @idSolicitud)
		DECLARE @Placa VARCHAR(20) = (SELECT NumeroPlaca FROM OP_TR_Vehiculo WHERE IdVehiculo = @idVehiculo)
		DECLARE @Observacion VARCHAR(250) = (SELECT Observacion FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle WHERE idSolicitudDetalle = @idSolicitudDetalle AND idSolicitud = @idSolicitud)
		
		IF ((SELECT Estado FROM ReportesApp_Mantenimiento_Solicitud_RegistroDetalle WHERE idSolicitudDetalle = @idSolicitudDetalle
		AND idSolicitud = @idSolicitud) = 'PENDIENTE') BEGIN
			UPDATE ReportesApp_Mantenimiento_Solicitud_RegistroDetalle
			SET Estado = 'TERMINADO'
			WHERE idSolicitudDetalle = @idSolicitudDetalle AND idSolicitud = @idSolicitud

			UPDATE ReportesApp_Mantenimiento_MttoCorrectivo_Registro
			SET Estado = 'TERMINADO'
			WHERE Placa = @Placa AND Observacion = @Observacion AND Origen = 'SOLICITUD'
		END
		ELSE BEGIN
			UPDATE ReportesApp_Mantenimiento_Solicitud_RegistroDetalle
			SET Estado = 'PENDIENTE'
			WHERE idSolicitudDetalle = @idSolicitudDetalle AND idSolicitud = @idSolicitud

			UPDATE ReportesApp_Mantenimiento_MttoCorrectivo_Registro
			SET Estado = 'PENDIENTE'
			WHERE Placa = @Placa AND Observacion = @Observacion AND Origen = 'SOLICITUD'
		END

		IF EXISTS(SELECT FechaProgramacion FROM ReportesApp_Mantenimiento_MttoCorrectivo_Registro WHERE Placa = @Placa AND Observacion = @Observacion
		AND Origen = 'SOLICITUD') BEGIN
			DECLARE @FechaProg2 DATETIME = (SELECT FechaProgramacion FROM ReportesApp_Mantenimiento_MttoCorrectivo_Registro WHERE Placa = @Placa AND Observacion = @Observacion AND Origen = 'SOLICITUD')
			DECLARE @MTTOC2 TABLE (Numero INT, idMttoC INT, Placa VARCHAR(20), FechaProgramacion DATETIME)
			DECLARE @Contador2 INT = 1

			INSERT INTO @MTTOC2(Numero, idMttoC, Placa, FechaProgramacion)
			SELECT ROW_NUMBER() OVER(ORDER BY idMttoC ASC) AS 'Numero', idMttoC, Placa, FechaProgramacion FROM ReportesApp_Mantenimiento_MttoCorrectivo_Registro
			WHERE Placa = @Placa AND YEAR(FechaProgramacion) = YEAR(@FechaProg2) AND MONTH(FechaProgramacion) = MONTH(@FechaProg2) AND Estado = 'PENDIENTE'

			UPDATE C
			SET C.D1 = NULL, C.D2 = NULL, C.D3 = NULL, C.D4 = NULL, C.D5 = NULL, C.D6 = NULL, C.D7 = NULL, C.D8 = NULL, C.D9 = NULL, C.D10 = NULL,
			C.D11 = NULL, C.D12 = NULL, C.D13 = NULL, C.D14 = NULL, C.D15 = NULL, C.D16 = NULL, C.D17 = NULL, C.D18 = NULL, C.D19 = NULL, C.D20 = NULL,
			C.D21 = NULL, C.D22 = NULL, C.D23 = NULL, C.D24 = NULL, C.D25 = NULL, C.D26 = NULL, C.D27 = NULL, C.D28 = NULL, C.D29 = NULL, C.D30 = NULL, C.D31 = NULL
			FROM ReportesApp_Mantenimiento_MttoCorrectivo_CalendarioView C
			WHERE C.Placa = @Placa AND C.Anio = YEAR(@FechaProg2) AND C.Mes = MONTH(@FechaProg2)

			WHILE (@Contador2 <= (SELECT COUNT(Numero) FROM @MTTOC2)) BEGIN
				DECLARE @MPlaca2 VARCHAR(20) = (SELECT Placa FROM @MTTOC2 WHERE Numero = @Contador2)
				DECLARE @MFechaProgramacion2 DATE = (SELECT CONVERT(DATE,FechaProgramacion) FROM @MTTOC2 WHERE Numero = @Contador2)

				UPDATE C
				SET C.D1 = CASE WHEN DAY(@MFechaProgramacion2) = 1 THEN ISNULL(C.D1,0) + 1 ELSE C.D1 END,
					C.D2 = CASE WHEN DAY(@MFechaProgramacion2) = 2 THEN ISNULL(C.D2,0) + 1 ELSE C.D2 END,
					C.D3 = CASE WHEN DAY(@MFechaProgramacion2) = 3 THEN ISNULL(C.D3,0) + 1 ELSE C.D3 END,
					C.D4 = CASE WHEN DAY(@MFechaProgramacion2) = 4 THEN ISNULL(C.D4,0) + 1 ELSE C.D4 END,
					C.D5 = CASE WHEN DAY(@MFechaProgramacion2) = 5 THEN ISNULL(C.D5,0) + 1 ELSE C.D5 END,
					C.D6 = CASE WHEN DAY(@MFechaProgramacion2) = 6 THEN ISNULL(C.D6,0) + 1 ELSE C.D6 END,
					C.D7 = CASE WHEN DAY(@MFechaProgramacion2) = 7 THEN ISNULL(C.D7,0) + 1 ELSE C.D7 END,
					C.D8 = CASE WHEN DAY(@MFechaProgramacion2) = 8 THEN ISNULL(C.D8,0) + 1 ELSE C.D8 END,
					C.D9 = CASE WHEN DAY(@MFechaProgramacion2) = 9 THEN ISNULL(C.D9,0) + 1 ELSE C.D9 END,
					C.D10 = CASE WHEN DAY(@MFechaProgramacion2) = 10 THEN ISNULL(C.D10,0) + 1 ELSE C.D10 END,
					C.D11 = CASE WHEN DAY(@MFechaProgramacion2) = 11 THEN ISNULL(C.D11,0) + 1 ELSE C.D11 END,
					C.D12 = CASE WHEN DAY(@MFechaProgramacion2) = 12 THEN ISNULL(C.D12,0) + 1 ELSE C.D12 END,
					C.D13 = CASE WHEN DAY(@MFechaProgramacion2) = 13 THEN ISNULL(C.D13,0) + 1 ELSE C.D13 END,
					C.D14 = CASE WHEN DAY(@MFechaProgramacion2) = 14 THEN ISNULL(C.D14,0) + 1 ELSE C.D14 END,
					C.D15 = CASE WHEN DAY(@MFechaProgramacion2) = 15 THEN ISNULL(C.D15,0) + 1 ELSE C.D15 END,
					C.D16 = CASE WHEN DAY(@MFechaProgramacion2) = 16 THEN ISNULL(C.D16,0) + 1 ELSE C.D16 END,
					C.D17 = CASE WHEN DAY(@MFechaProgramacion2) = 17 THEN ISNULL(C.D17,0) + 1 ELSE C.D17 END,
					C.D18 = CASE WHEN DAY(@MFechaProgramacion2) = 18 THEN ISNULL(C.D18,0) + 1 ELSE C.D18 END,
					C.D19 = CASE WHEN DAY(@MFechaProgramacion2) = 19 THEN ISNULL(C.D19,0) + 1 ELSE C.D19 END,
					C.D20 = CASE WHEN DAY(@MFechaProgramacion2) = 20 THEN ISNULL(C.D20,0) + 1 ELSE C.D20 END,
					C.D21 = CASE WHEN DAY(@MFechaProgramacion2) = 21 THEN ISNULL(C.D21,0) + 1 ELSE C.D21 END,
					C.D22 = CASE WHEN DAY(@MFechaProgramacion2) = 22 THEN ISNULL(C.D22,0) + 1 ELSE C.D22 END,
					C.D23 = CASE WHEN DAY(@MFechaProgramacion2) = 23 THEN ISNULL(C.D23,0) + 1 ELSE C.D23 END,
					C.D24 = CASE WHEN DAY(@MFechaProgramacion2) = 24 THEN ISNULL(C.D24,0) + 1 ELSE C.D24 END,
					C.D25 = CASE WHEN DAY(@MFechaProgramacion2) = 25 THEN ISNULL(C.D25,0) + 1 ELSE C.D25 END,
					C.D26 = CASE WHEN DAY(@MFechaProgramacion2) = 26 THEN ISNULL(C.D26,0) + 1 ELSE C.D26 END,
					C.D27 = CASE WHEN DAY(@MFechaProgramacion2) = 27 THEN ISNULL(C.D27,0) + 1 ELSE C.D27 END,
					C.D28 = CASE WHEN DAY(@MFechaProgramacion2) = 28 THEN ISNULL(C.D28,0) + 1 ELSE C.D28 END,
					C.D29 = CASE WHEN DAY(@MFechaProgramacion2) = 29 THEN ISNULL(C.D29,0) + 1 ELSE C.D29 END,
					C.D30 = CASE WHEN DAY(@MFechaProgramacion2) = 30 THEN ISNULL(C.D30,0) + 1 ELSE C.D30 END,
					C.D31 = CASE WHEN DAY(@MFechaProgramacion2) = 31 THEN ISNULL(C.D31,0) + 1 ELSE C.D31 END
				FROM ReportesApp_Mantenimiento_MttoCorrectivo_CalendarioView C
				WHERE C.Placa = @MPlaca2 AND C.Anio = YEAR(@MFechaProgramacion2) AND C.Mes = MONTH(@MFechaProgramacion2)

				SET @Contador2 = @Contador2 + 1
			END
		END
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

-------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 30-05-2023
-- Description:	AGREGAR SOLUCIÓN A FALLAS MECÁNICAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_InsertarSolucion]
@idFalla INT,
@NombreTercero VARCHAR(250),
@TelefonoTercero VARCHAR(20),
@idTipoRecibo INT,
@Comprobante VARCHAR(30),
@MontoComprobante DECIMAL(8,2),
@Tecnico VARCHAR(250),
@idPlaca VARCHAR(20),
@Galones DECIMAL(8,2),
@PrecioUnitario DECIMAL(8,2),
@PrecioTotal DECIMAL(8,2),
@Monto DECIMAL(8,2),
@idSistemaVehiculo INT,
@idSubSistema INT,
@Solucion VARCHAR(250),
@Usuario VARCHAR(20),
@MttoCorrectivo INT
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo2 INT

SET @Exito = '0 = Solución Añadida.'

IF (@idFalla = 0)
BEGIN
	SET @Exito = '-1 = La falla seleccionada no existe.'
	GOTO Terminar
END

IF((SELECT idEstadoFalla FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros WHERE idFalla = @idFalla) = 2)
BEGIN
	SET @Exito = '-3 = La falla seleccionada ya se encuentra solucionada.'
	GOTO Terminar
END

IF((SELECT FechaTermino FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros WHERE idFalla = @idFalla) IS NULL)
BEGIN
	SET @Exito = '-4 = Primero debe cerrar la falla mecánica.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	IF (@Tecnico = '') BEGIN
		UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
		SET idClaseServicio = 2
		WHERE idFalla = @idFalla
	END
	ELSE BEGIN
		IF (@Comprobante = '') BEGIN
		UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
		SET idClaseServicio = 1
		WHERE idFalla = @idFalla
		END
	END
	
	UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
	SET idEstadoFalla = 2,
	NombreTercero = @NombreTercero,
	TelefonoTercero = @TelefonoTercero,
	idTipoRecibo = @idTipoRecibo,
	Comprobante = @Comprobante,
	MontoComprobante = @MontoComprobante,
	Tecnico = @Tecnico,
	idPlaca = @idPlaca,
	Galones = @Galones,
	PrecioUnitario = @PrecioUnitario,
	PrecioTotal = @PrecioTotal,
	Monto = @Monto,
	idSistemaVehiculo = @idSistemaVehiculo,
	idSubSistema = @idSubSistema,
	Solucion = @Solucion,
	UltimoUsuario = @Usuario,
	UltimaModificacion = GETDATE()
	WHERE idFalla = @idFalla

	IF (@Monto = 0.00 AND @PrecioTotal = 0.00 AND (SELECT idClaseServicio FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros WHERE idFalla = @idFalla) = 1) BEGIN
		UPDATE ReportesApp_Mantenimiento_FallasMecanicas_Registros
		SET idEstadoLiquidacion = 2, FechaLiquidacion = GETDATE()
		WHERE idFalla = @idFalla
	END
	
	INSERT INTO ReportesApp_Mantenimiento_FallasMecanicas_ImportarWord (idFalla,CodFalla,Tipo,Estado,NroTicket,Programacion,NumeroPlaca,Semirremolque,
				FechaViaje,Conductor,Cliente,Ruta,TipoFalla,Motivo,Ubicacion,FechaInicio,HoraInicio,ClaseServicio,Tecnico,PlacaTecnico,Galones,
				PrecioTotal,Monto,NombreTercero,TelefonoTercero,TipoRecibo,Comprobante,MontoComprobante,Solucion,FechaTermino,HoraTermino,
				Duracion,FechaLlegada,HoraLlegada,FechaSalida,HoraSalida,SistemaVehiculo,SubSistemaVehiculo)
	SELECT FM.idFalla, FM.CodFalla, FM.TipoFalla, EF.Descripcion, FM.NroTicket, OP.Descripcion, V.NumeroPlaca, R.NumeroPlaca, RE.FechaProgramacion,
	LTRIM(RTRIM(C.Nombre)), LTRIM(RTRIM(CL.Busqueda)), RT.Descripcion, A.Descripcion, FM.Motivo, FM.Ubicacion, FM.FechaInicio, FM.HoraInicio,
	CS.Descripcion, FM.Tecnico, FM.idPlaca, FM.Galones, FM.PrecioTotal, FM.Monto, FM.NombreTercero, FM.TelefonoTercero,
	TR.Descripcion, FM.Comprobante, FM.MontoComprobante, FM.Solucion, FM.FechaTermino, FM.HoraTermino, FM.Duracion, FM.FechaLlegada, FM.HoraLlegada,
	FM.FechaSalida, FM.HoraSalida, SV.Descripcion, SSV.Descripcion FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros FM
	LEFT JOIN ReportesApp_Operacion_Previaje_Registros RE WITH(NOLOCK) ON FM.NroTicket = RE.NroTicket
	LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = RE.TipoProgramacion
    LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = ISNULL(FM.idTracto,RE.idTracto)
	LEFT JOIN OP_TR_Vehiculo R WITH(NOLOCK) ON R.IdVehiculo = ISNULL(FM.idCarreta,RE.idSemirremolque)
	LEFT JOIN OP_TR_Conductor C WITH(NOLOCK) ON C.IdConductor = RE.IdConductor
	LEFT JOIN PersonaMast CL WITH(NOLOCK) ON CL.Persona = RE.IdCliente
	LEFT JOIN OP_TR_RUTA RT WITH(NOLOCK) ON RT.IDRUTA = RE.IdRuta
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_TipoAuxilio A WITH(NOLOCK) ON A.idTipoAuxilio = FM.idTipoAuxilio
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_EstadoFalla EF WITH(NOLOCK) ON EF.idEstadoFalla = FM.idEstadoFalla
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_ClaseServicio CS WITH(NOLOCK) ON CS.idClaseServicio = FM.idClaseServicio
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_TipoRecibo TR WITH(NOLOCK) ON TR.idTipoRecibo = FM.idTipoRecibo
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_SistemaVehiculo SV WITH(NOLOCK) ON SV.idSistemaVehiculo = FM.idSistemaVehiculo
	LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_SubSistemaVehiculo SSV WITH(NOLOCK) ON SSV.idSubSistema = FM.idSubSistema
	WHERE(FM.idFalla = @idFalla)

	IF (@MttoCorrectivo = 1) BEGIN
		SET @correlativo2 = (SELECT MAX(idMttoC) FROM ReportesApp_Mantenimiento_MttoCorrectivo_Registro)
		SET @correlativo2 = ISNULL(@correlativo2, 0) + 1

		INSERT INTO ReportesApp_Mantenimiento_MttoCorrectivo_Registro(idMttoC, Placa, Operacion, Origen, FechaReportada, Sistema,
		Descripcion, Observacion, Estado, UsuarioCreacion, FechaCreacion, UsuarioModificacion, FechaModificacion)
		SELECT @correlativo2, V.NumeroPlaca, OP.Descripcion, 'AUXILIO', CONVERT(DATETIME,FM.FechaInicio), SV.Descripcion, SSV.Descripcion,
		FM.Motivo, 'PENDIENTE', @Usuario, GETDATE(), @Usuario, GETDATE()
		FROM ReportesApp_Mantenimiento_FallasMecanicas_Registros FM
		LEFT JOIN ReportesApp_Operacion_Previaje_Registros RE WITH(NOLOCK) ON FM.NroTicket = RE.NroTicket
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = ISNULL(FM.idTracto,RE.idTracto)
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones OP WITH(NOLOCK) ON OP.IdOperacion = RE.TipoProgramacion
		LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_SistemaVehiculo SV WITH(NOLOCK) ON SV.idSistemaVehiculo = FM.idSistemaVehiculo
		LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_SubSistemaVehiculo SSV WITH(NOLOCK) ON SSV.idSubSistema = FM.idSubSistema
		WHERE (FM.idFalla = @idFalla)
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

-------------------------------------------------------------------------------

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
		SET Mecanico = @Mecanico, Electrico = @Electrico, Neumatico = @Neumatico, FechaCrea = @FechaInspeccion, Activo = 1
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

---------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 03-09-2024
-- Description:	LISTAR MANTENIMIENTO CORRECTIVO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoCorrectivo_ListarRegistros]
@Placa VARCHAR(20),
@Descripcion VARCHAR(250),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@Origen VARCHAR(40),
@Estado VARCHAR(20)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@Origen = 'TODOS') BEGIN
		SELECT MC.idMttoC AS 'NRO', MC.Placa AS 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD', SV.Descripcion AS 'SUBTIPO_UNIDAD',
		MC.Operacion AS 'OPERACION', MC.Origen AS 'ORIGEN', MC.FechaReportada AS 'FECHA_REPORTADA', MC.Sistema AS 'SISTEMA', MC.Descripcion AS 'DESCRIPCION',
		MC.Observacion AS 'OBSERVACION', CONVERT(VARCHAR,MC.FechaProgramacion,103)+' '+CONVERT(VARCHAR,MC.FechaProgramacion,8) AS 'FECHA_PROGRAMACION',
		MC.OT AS 'OT', MC.Estado AS 'ESTADO', MC.UsuarioCreacion, MC.FechaCreacion, MC.UsuarioModificacion, MC.FechaModificacion
		FROM ReportesApp_Mantenimiento_MttoCorrectivo_Registro MC
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON MC.Placa = V.NumeroPlaca AND V.Estado = 2
		LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TipoVehiculo
		LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
		WHERE (MC.Placa IS NULL OR MC.Placa LIKE '%' + @Placa + '%') AND (MC.Observacion IS NULL OR MC.Observacion LIKE '%' + @Descripcion + '%')
		AND (MC.Estado = @Estado) AND (MC.FechaReportada BETWEEN @FINICIO AND @FFIN)
		ORDER BY MC.FechaReportada DESC
	END
	ELSE BEGIN
		SELECT MC.idMttoC AS 'NRO', MC.Placa AS 'PLACA', TV.Descripcion AS 'TIPO_UNIDAD', SV.Descripcion AS 'SUBTIPO_UNIDAD',
		MC.Operacion AS 'OPERACION', MC.Origen AS 'ORIGEN', MC.FechaReportada AS 'FECHA_REPORTADA', MC.Sistema AS 'SISTEMA', MC.Descripcion AS 'DESCRIPCION',
		MC.Observacion AS 'OBSERVACION', CONVERT(VARCHAR,MC.FechaProgramacion,103)+' '+CONVERT(VARCHAR,MC.FechaProgramacion,8) AS 'FECHA_PROGRAMACION',
		MC.OT AS 'OT', MC.Estado AS 'ESTADO', MC.UsuarioCreacion, MC.FechaCreacion, MC.UsuarioModificacion, MC.FechaModificacion
		FROM ReportesApp_Mantenimiento_MttoCorrectivo_Registro MC
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON MC.Placa = V.NumeroPlaca AND V.Estado = 2
		LEFT JOIN ReportesApp_Operaciones_ConductorUnidades_TipoVehiculo AS TV WITH(NOLOCK) ON TV.idTipoVehiculo = V.TipoVehiculo
		LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
		WHERE (MC.Placa IS NULL OR MC.Placa LIKE '%' + @Placa + '%') AND (MC.Observacion IS NULL OR MC.Observacion LIKE '%' + @Descripcion + '%')
		AND (MC.Estado = @Estado) AND (MC.Origen = @Origen) AND (MC.FechaReportada BETWEEN @FINICIO AND @FFIN)
		ORDER BY MC.FechaReportada DESC
	END
END

---------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 04-09-2024
-- Description:	LISTAR OT PROGRAMADAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoCorrectivo_ListarOTProgramadas]
@Placa VARCHAR(20),
@OT VARCHAR(30)
AS
BEGIN
	SELECT TOP(25) LTRIM(RTRIM(NumeroOrden)) AS 'OT', CONVERT(VARCHAR,FechaProgramada,103) AS 'FECHA_PROGRAMADA', Descripcion AS 'DESCRIPCION'
	FROM ME_OrdenTrabajo
	WHERE (CompaniaSocio = '10000000') AND (Estado = 'PG') AND (LTRIM(RTRIM(MaquinaCodigo)) = @Placa) AND (@OT IS NULL OR LTRIM(RTRIM(NumeroOrden)) LIKE '%' + @OT + '%')
	ORDER BY FechaProgramada DESC
END

---------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 04-09-2024
-- Description:	ACTUALIZAR ESTADO DE MTTO CORRECTIVO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoCorrectivo_ModificarMtto]
@Opcion INT,
@idMttoC INT,
@FechaProg DATETIME,
@OT VARCHAR(50),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- PROGRAMAR MTTO CORRECTIVO
		DECLARE @Placa VARCHAR(20) = (SELECT Placa FROM ReportesApp_Mantenimiento_MttoCorrectivo_Registro WHERE idMttoC = @idMttoC)
		DECLARE @Operacion VARCHAR(50) = (SELECT Operacion FROM ReportesApp_Mantenimiento_MttoCorrectivo_Registro WHERE idMttoC = @idMttoC)
		DECLARE @TipoUnidad VARCHAR(50) = (SELECT SV.Descripcion FROM OP_TR_Vehiculo V LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
										   WHERE V.NumeroPlaca = @Placa AND V.Estado = 2)

		DECLARE @Dia DATE = CONVERT(VARCHAR,YEAR(@FechaProg))+'-'+CONVERT(VARCHAR,RIGHT('00'+MONTH(@FechaProg),2))+'-01'
		DECLARE @FechaFin DATE = CAST(DATEADD(MONTH,DATEDIFF(MONTH,0,@Dia)+1,0)-1 AS DATE)
		DECLARE @UltimoDiaMes TINYINT = DAY(@FechaFin)

		DECLARE @FechaAnterior DATE = (SELECT FechaProgramacion FROM ReportesApp_Mantenimiento_MttoCorrectivo_Registro WHERE @idMttoC = idMttoC)

		UPDATE ReportesApp_Mantenimiento_MttoCorrectivo_Registro
		SET FechaProgramacion = @FechaProg, UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()
		WHERE @idMttoC = idMttoC

		IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_MttoCorrectivo_CalendarioView WHERE Placa = @Placa AND Anio = YEAR(@FechaProg) AND
		Mes = MONTH(@FechaProg))) BEGIN
			DECLARE @MTTOC TABLE (Numero INT, idMttoC INT, Placa VARCHAR(20), FechaProgramacion DATETIME)
			DECLARE @Contador INT = 1

			INSERT INTO @MTTOC(Numero, idMttoC, Placa, FechaProgramacion)
			SELECT ROW_NUMBER() OVER(ORDER BY idMttoC ASC) AS 'Numero', idMttoC, Placa, FechaProgramacion FROM ReportesApp_Mantenimiento_MttoCorrectivo_Registro
			WHERE Placa = @Placa AND YEAR(FechaProgramacion) = YEAR(@FechaProg) AND MONTH(FechaProgramacion) = MONTH(@FechaProg) AND Estado = 'PENDIENTE'

			UPDATE C
			SET C.D1 = NULL, C.D2 = NULL, C.D3 = NULL, C.D4 = NULL, C.D5 = NULL, C.D6 = NULL, C.D7 = NULL, C.D8 = NULL, C.D9 = NULL, C.D10 = NULL,
			C.D11 = NULL, C.D12 = NULL, C.D13 = NULL, C.D14 = NULL, C.D15 = NULL, C.D16 = NULL, C.D17 = NULL, C.D18 = NULL, C.D19 = NULL, C.D20 = NULL,
			C.D21 = NULL, C.D22 = NULL, C.D23 = NULL, C.D24 = NULL, C.D25 = NULL, C.D26 = NULL, C.D27 = NULL, C.D28 = NULL, C.D29 = NULL, C.D30 = NULL, C.D31 = NULL
			FROM ReportesApp_Mantenimiento_MttoCorrectivo_CalendarioView C
			WHERE C.Placa = @Placa AND C.Anio = YEAR(@FechaProg) AND C.Mes = MONTH(@FechaProg)

			WHILE (@Contador <= (SELECT COUNT(Numero) FROM @MTTOC)) BEGIN
				DECLARE @MPlaca VARCHAR(20) = (SELECT Placa FROM @MTTOC WHERE Numero = @Contador)
				DECLARE @MFechaProgramacion DATE = (SELECT CONVERT(DATE,FechaProgramacion) FROM @MTTOC WHERE Numero = @Contador)

				UPDATE C
				SET C.D1 = CASE WHEN DAY(@MFechaProgramacion) = 1 THEN ISNULL(C.D1,0) + 1 ELSE C.D1 END,
					C.D2 = CASE WHEN DAY(@MFechaProgramacion) = 2 THEN ISNULL(C.D2,0) + 1 ELSE C.D2 END,
					C.D3 = CASE WHEN DAY(@MFechaProgramacion) = 3 THEN ISNULL(C.D3,0) + 1 ELSE C.D3 END,
					C.D4 = CASE WHEN DAY(@MFechaProgramacion) = 4 THEN ISNULL(C.D4,0) + 1 ELSE C.D4 END,
					C.D5 = CASE WHEN DAY(@MFechaProgramacion) = 5 THEN ISNULL(C.D5,0) + 1 ELSE C.D5 END,
					C.D6 = CASE WHEN DAY(@MFechaProgramacion) = 6 THEN ISNULL(C.D6,0) + 1 ELSE C.D6 END,
					C.D7 = CASE WHEN DAY(@MFechaProgramacion) = 7 THEN ISNULL(C.D7,0) + 1 ELSE C.D7 END,
					C.D8 = CASE WHEN DAY(@MFechaProgramacion) = 8 THEN ISNULL(C.D8,0) + 1 ELSE C.D8 END,
					C.D9 = CASE WHEN DAY(@MFechaProgramacion) = 9 THEN ISNULL(C.D9,0) + 1 ELSE C.D9 END,
					C.D10 = CASE WHEN DAY(@MFechaProgramacion) = 10 THEN ISNULL(C.D10,0) + 1 ELSE C.D10 END,
					C.D11 = CASE WHEN DAY(@MFechaProgramacion) = 11 THEN ISNULL(C.D11,0) + 1 ELSE C.D11 END,
					C.D12 = CASE WHEN DAY(@MFechaProgramacion) = 12 THEN ISNULL(C.D12,0) + 1 ELSE C.D12 END,
					C.D13 = CASE WHEN DAY(@MFechaProgramacion) = 13 THEN ISNULL(C.D13,0) + 1 ELSE C.D13 END,
					C.D14 = CASE WHEN DAY(@MFechaProgramacion) = 14 THEN ISNULL(C.D14,0) + 1 ELSE C.D14 END,
					C.D15 = CASE WHEN DAY(@MFechaProgramacion) = 15 THEN ISNULL(C.D15,0) + 1 ELSE C.D15 END,
					C.D16 = CASE WHEN DAY(@MFechaProgramacion) = 16 THEN ISNULL(C.D16,0) + 1 ELSE C.D16 END,
					C.D17 = CASE WHEN DAY(@MFechaProgramacion) = 17 THEN ISNULL(C.D17,0) + 1 ELSE C.D17 END,
					C.D18 = CASE WHEN DAY(@MFechaProgramacion) = 18 THEN ISNULL(C.D18,0) + 1 ELSE C.D18 END,
					C.D19 = CASE WHEN DAY(@MFechaProgramacion) = 19 THEN ISNULL(C.D19,0) + 1 ELSE C.D19 END,
					C.D20 = CASE WHEN DAY(@MFechaProgramacion) = 20 THEN ISNULL(C.D20,0) + 1 ELSE C.D20 END,
					C.D21 = CASE WHEN DAY(@MFechaProgramacion) = 21 THEN ISNULL(C.D21,0) + 1 ELSE C.D21 END,
					C.D22 = CASE WHEN DAY(@MFechaProgramacion) = 22 THEN ISNULL(C.D22,0) + 1 ELSE C.D22 END,
					C.D23 = CASE WHEN DAY(@MFechaProgramacion) = 23 THEN ISNULL(C.D23,0) + 1 ELSE C.D23 END,
					C.D24 = CASE WHEN DAY(@MFechaProgramacion) = 24 THEN ISNULL(C.D24,0) + 1 ELSE C.D24 END,
					C.D25 = CASE WHEN DAY(@MFechaProgramacion) = 25 THEN ISNULL(C.D25,0) + 1 ELSE C.D25 END,
					C.D26 = CASE WHEN DAY(@MFechaProgramacion) = 26 THEN ISNULL(C.D26,0) + 1 ELSE C.D26 END,
					C.D27 = CASE WHEN DAY(@MFechaProgramacion) = 27 THEN ISNULL(C.D27,0) + 1 ELSE C.D27 END,
					C.D28 = CASE WHEN DAY(@MFechaProgramacion) = 28 THEN ISNULL(C.D28,0) + 1 ELSE C.D28 END,
					C.D29 = CASE WHEN DAY(@MFechaProgramacion) = 29 THEN ISNULL(C.D29,0) + 1 ELSE C.D29 END,
					C.D30 = CASE WHEN DAY(@MFechaProgramacion) = 30 THEN ISNULL(C.D30,0) + 1 ELSE C.D30 END,
					C.D31 = CASE WHEN DAY(@MFechaProgramacion) = 31 THEN ISNULL(C.D31,0) + 1 ELSE C.D31 END
				FROM ReportesApp_Mantenimiento_MttoCorrectivo_CalendarioView C
				WHERE C.Placa = @MPlaca AND C.Anio = YEAR(@MFechaProgramacion) AND C.Mes = MONTH(@MFechaProgramacion)

				SET @Contador = @Contador + 1
			END
		END
		ELSE BEGIN
			INSERT INTO ReportesApp_Mantenimiento_MttoCorrectivo_CalendarioView(Placa,TipoUnidad,Operacion,Anio,Mes,NroDias)
			VALUES(@Placa,@TipoUnidad,@Operacion,YEAR(@FechaProg),MONTH(@FechaProg),@UltimoDiaMes)

			UPDATE C
			SET C.D1 = CASE WHEN DAY(@FechaProg) = 1 THEN 1 ELSE C.D1 END,
				C.D2 = CASE WHEN DAY(@FechaProg) = 2 THEN 1 ELSE C.D2 END,
				C.D3 = CASE WHEN DAY(@FechaProg) = 3 THEN 1 ELSE C.D3 END,
				C.D4 = CASE WHEN DAY(@FechaProg) = 4 THEN 1 ELSE C.D4 END,
				C.D5 = CASE WHEN DAY(@FechaProg) = 5 THEN 1 ELSE C.D5 END,
				C.D6 = CASE WHEN DAY(@FechaProg) = 6 THEN 1 ELSE C.D6 END,
				C.D7 = CASE WHEN DAY(@FechaProg) = 7 THEN 1 ELSE C.D7 END,
				C.D8 = CASE WHEN DAY(@FechaProg) = 8 THEN 1 ELSE C.D8 END,
				C.D9 = CASE WHEN DAY(@FechaProg) = 9 THEN 1 ELSE C.D9 END,
				C.D10 = CASE WHEN DAY(@FechaProg) = 10 THEN 1 ELSE C.D10 END,
				C.D11 = CASE WHEN DAY(@FechaProg) = 11 THEN 1 ELSE C.D11 END,
				C.D12 = CASE WHEN DAY(@FechaProg) = 12 THEN 1 ELSE C.D12 END,
				C.D13 = CASE WHEN DAY(@FechaProg) = 13 THEN 1 ELSE C.D13 END,
				C.D14 = CASE WHEN DAY(@FechaProg) = 14 THEN 1 ELSE C.D14 END,
				C.D15 = CASE WHEN DAY(@FechaProg) = 15 THEN 1 ELSE C.D15 END,
				C.D16 = CASE WHEN DAY(@FechaProg) = 16 THEN 1 ELSE C.D16 END,
				C.D17 = CASE WHEN DAY(@FechaProg) = 17 THEN 1 ELSE C.D17 END,
				C.D18 = CASE WHEN DAY(@FechaProg) = 18 THEN 1 ELSE C.D18 END,
				C.D19 = CASE WHEN DAY(@FechaProg) = 19 THEN 1 ELSE C.D19 END,
				C.D20 = CASE WHEN DAY(@FechaProg) = 20 THEN 1 ELSE C.D20 END,
				C.D21 = CASE WHEN DAY(@FechaProg) = 21 THEN 1 ELSE C.D21 END,
				C.D22 = CASE WHEN DAY(@FechaProg) = 22 THEN 1 ELSE C.D22 END,
				C.D23 = CASE WHEN DAY(@FechaProg) = 23 THEN 1 ELSE C.D23 END,
				C.D24 = CASE WHEN DAY(@FechaProg) = 24 THEN 1 ELSE C.D24 END,
				C.D25 = CASE WHEN DAY(@FechaProg) = 25 THEN 1 ELSE C.D25 END,
				C.D26 = CASE WHEN DAY(@FechaProg) = 26 THEN 1 ELSE C.D26 END,
				C.D27 = CASE WHEN DAY(@FechaProg) = 27 THEN 1 ELSE C.D27 END,
				C.D28 = CASE WHEN DAY(@FechaProg) = 28 THEN 1 ELSE C.D28 END,
				C.D29 = CASE WHEN DAY(@FechaProg) = 29 THEN 1 ELSE C.D29 END,
				C.D30 = CASE WHEN DAY(@FechaProg) = 30 THEN 1 ELSE C.D30 END,
				C.D31 = CASE WHEN DAY(@FechaProg) = 31 THEN 1 ELSE C.D31 END
			FROM ReportesApp_Mantenimiento_MttoCorrectivo_CalendarioView C
			WHERE C.Placa = @Placa AND C.Anio = YEAR(@FechaProg) AND C.Mes = MONTH(@FechaProg)
		END

		SET @Exito = '0 = Mantenimiento actualizado correctamente.'
	END

	IF (@Opcion = 2) BEGIN		-- ACTUALIZAR ESTADO
		IF ((SELECT Estado FROM ReportesApp_Mantenimiento_MttoCorrectivo_Registro WHERE @idMttoC = idMttoC) = 'PENDIENTE') BEGIN
			UPDATE ReportesApp_Mantenimiento_MttoCorrectivo_Registro
			SET Estado = 'TERMINADO', OT = @OT, UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()
			WHERE @idMttoC = idMttoC
		END
		ELSE BEGIN
			UPDATE ReportesApp_Mantenimiento_MttoCorrectivo_Registro
			SET Estado = 'PENDIENTE', OT = @OT, UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()
			WHERE @idMttoC = idMttoC
		END

		/*
		DECLARE @FechaEditada DATE = (SELECT FechaProgramacion FROM ReportesApp_Mantenimiento_MttoCorrectivo_Registro WHERE idMttoC = @idMttoC)
		DECLARE @PlacaEditada VARCHAR(20) = (SELECT Placa FROM ReportesApp_Mantenimiento_MttoCorrectivo_Registro WHERE idMttoC = @idMttoC)
		DECLARE @MTTOCE TABLE (Numero INT, idMttoC INT, Placa VARCHAR(20), FechaProgramacion DATETIME)
		DECLARE @ContadorE INT = 1

		INSERT INTO @MTTOCE(Numero, idMttoC, Placa, FechaProgramacion)
		SELECT ROW_NUMBER() OVER(ORDER BY idMttoC ASC) AS 'Numero', idMttoC, Placa, FechaProgramacion FROM ReportesApp_Mantenimiento_MttoCorrectivo_Registro
		WHERE Placa = @PlacaEditada AND YEAR(FechaProgramacion) = YEAR(@FechaEditada) AND MONTH(FechaProgramacion) = MONTH(@FechaEditada) AND Estado = 'PENDIENTE'

		UPDATE C
		SET C.D1 = NULL, C.D2 = NULL, C.D3 = NULL, C.D4 = NULL, C.D5 = NULL, C.D6 = NULL, C.D7 = NULL, C.D8 = NULL, C.D9 = NULL, C.D10 = NULL,
		C.D11 = NULL, C.D12 = NULL, C.D13 = NULL, C.D14 = NULL, C.D15 = NULL, C.D16 = NULL, C.D17 = NULL, C.D18 = NULL, C.D19 = NULL, C.D20 = NULL,
		C.D21 = NULL, C.D22 = NULL, C.D23 = NULL, C.D24 = NULL, C.D25 = NULL, C.D26 = NULL, C.D27 = NULL, C.D28 = NULL, C.D29 = NULL, C.D30 = NULL, C.D31 = NULL
		FROM ReportesApp_Mantenimiento_MttoCorrectivo_CalendarioView C
		WHERE C.Placa = @PlacaEditada AND C.Anio = YEAR(@FechaEditada) AND C.Mes = MONTH(@FechaEditada)

		WHILE (@ContadorE <= (SELECT COUNT(Numero) FROM @MTTOCE)) BEGIN
			DECLARE @MEPlaca2 VARCHAR(20) = (SELECT Placa FROM @MTTOCE WHERE Numero = @ContadorE)
			DECLARE @MEFechaProgramacion2 DATE = (SELECT CONVERT(DATE,FechaProgramacion) FROM @MTTOCE WHERE Numero = @ContadorE)

			UPDATE C
			SET C.D1 = CASE WHEN DAY(@MEFechaProgramacion2) = 1 THEN ISNULL(C.D1,0) + 1 ELSE C.D1 END,
				C.D2 = CASE WHEN DAY(@MEFechaProgramacion2) = 2 THEN ISNULL(C.D2,0) + 1 ELSE C.D2 END,
				C.D3 = CASE WHEN DAY(@MEFechaProgramacion2) = 3 THEN ISNULL(C.D3,0) + 1 ELSE C.D3 END,
				C.D4 = CASE WHEN DAY(@MEFechaProgramacion2) = 4 THEN ISNULL(C.D4,0) + 1 ELSE C.D4 END,
				C.D5 = CASE WHEN DAY(@MEFechaProgramacion2) = 5 THEN ISNULL(C.D5,0) + 1 ELSE C.D5 END,
				C.D6 = CASE WHEN DAY(@MEFechaProgramacion2) = 6 THEN ISNULL(C.D6,0) + 1 ELSE C.D6 END,
				C.D7 = CASE WHEN DAY(@MEFechaProgramacion2) = 7 THEN ISNULL(C.D7,0) + 1 ELSE C.D7 END,
				C.D8 = CASE WHEN DAY(@MEFechaProgramacion2) = 8 THEN ISNULL(C.D8,0) + 1 ELSE C.D8 END,
				C.D9 = CASE WHEN DAY(@MEFechaProgramacion2) = 9 THEN ISNULL(C.D9,0) + 1 ELSE C.D9 END,
				C.D10 = CASE WHEN DAY(@MEFechaProgramacion2) = 10 THEN ISNULL(C.D10,0) + 1 ELSE C.D10 END,
				C.D11 = CASE WHEN DAY(@MEFechaProgramacion2) = 11 THEN ISNULL(C.D11,0) + 1 ELSE C.D11 END,
				C.D12 = CASE WHEN DAY(@MEFechaProgramacion2) = 12 THEN ISNULL(C.D12,0) + 1 ELSE C.D12 END,
				C.D13 = CASE WHEN DAY(@MEFechaProgramacion2) = 13 THEN ISNULL(C.D13,0) + 1 ELSE C.D13 END,
				C.D14 = CASE WHEN DAY(@MEFechaProgramacion2) = 14 THEN ISNULL(C.D14,0) + 1 ELSE C.D14 END,
				C.D15 = CASE WHEN DAY(@MEFechaProgramacion2) = 15 THEN ISNULL(C.D15,0) + 1 ELSE C.D15 END,
				C.D16 = CASE WHEN DAY(@MEFechaProgramacion2) = 16 THEN ISNULL(C.D16,0) + 1 ELSE C.D16 END,
				C.D17 = CASE WHEN DAY(@MEFechaProgramacion2) = 17 THEN ISNULL(C.D17,0) + 1 ELSE C.D17 END,
				C.D18 = CASE WHEN DAY(@MEFechaProgramacion2) = 18 THEN ISNULL(C.D18,0) + 1 ELSE C.D18 END,
				C.D19 = CASE WHEN DAY(@MEFechaProgramacion2) = 19 THEN ISNULL(C.D19,0) + 1 ELSE C.D19 END,
				C.D20 = CASE WHEN DAY(@MEFechaProgramacion2) = 20 THEN ISNULL(C.D20,0) + 1 ELSE C.D20 END,
				C.D21 = CASE WHEN DAY(@MEFechaProgramacion2) = 21 THEN ISNULL(C.D21,0) + 1 ELSE C.D21 END,
				C.D22 = CASE WHEN DAY(@MEFechaProgramacion2) = 22 THEN ISNULL(C.D22,0) + 1 ELSE C.D22 END,
				C.D23 = CASE WHEN DAY(@MEFechaProgramacion2) = 23 THEN ISNULL(C.D23,0) + 1 ELSE C.D23 END,
				C.D24 = CASE WHEN DAY(@MEFechaProgramacion2) = 24 THEN ISNULL(C.D24,0) + 1 ELSE C.D24 END,
				C.D25 = CASE WHEN DAY(@MEFechaProgramacion2) = 25 THEN ISNULL(C.D25,0) + 1 ELSE C.D25 END,
				C.D26 = CASE WHEN DAY(@MEFechaProgramacion2) = 26 THEN ISNULL(C.D26,0) + 1 ELSE C.D26 END,
				C.D27 = CASE WHEN DAY(@MEFechaProgramacion2) = 27 THEN ISNULL(C.D27,0) + 1 ELSE C.D27 END,
				C.D28 = CASE WHEN DAY(@MEFechaProgramacion2) = 28 THEN ISNULL(C.D28,0) + 1 ELSE C.D28 END,
				C.D29 = CASE WHEN DAY(@MEFechaProgramacion2) = 29 THEN ISNULL(C.D29,0) + 1 ELSE C.D29 END,
				C.D30 = CASE WHEN DAY(@MEFechaProgramacion2) = 30 THEN ISNULL(C.D30,0) + 1 ELSE C.D30 END,
				C.D31 = CASE WHEN DAY(@MEFechaProgramacion2) = 31 THEN ISNULL(C.D31,0) + 1 ELSE C.D31 END
			FROM ReportesApp_Mantenimiento_MttoCorrectivo_CalendarioView C
			WHERE C.Placa = @MEPlaca2 AND C.Anio = YEAR(@MEFechaProgramacion2) AND C.Mes = MONTH(@MEFechaProgramacion2)

			SET @ContadorE = @ContadorE + 1
		END
		*/

		SET @Exito = '0 = Mantenimiento actualizado correctamente.'
	END

	IF (@Opcion = 3) BEGIN		-- ELIMINAR MTTO CORRECTIVO
		DECLARE @FechaEliminada DATE = (SELECT FechaProgramacion FROM ReportesApp_Mantenimiento_MttoCorrectivo_Registro WHERE idMttoC = @idMttoC)
		DECLARE @PlacaEliminada VARCHAR(20) = (SELECT Placa FROM ReportesApp_Mantenimiento_MttoCorrectivo_Registro WHERE idMttoC = @idMttoC)

		DELETE FROM ReportesApp_Mantenimiento_MttoCorrectivo_Registro
		WHERE @idMttoC = idMttoC 

		DECLARE @MTTOCP TABLE (Numero INT, idMttoC INT, Placa VARCHAR(20), FechaProgramacion DATETIME)
		DECLARE @ContadorP INT = 1

		INSERT INTO @MTTOCP(Numero, idMttoC, Placa, FechaProgramacion)
		SELECT ROW_NUMBER() OVER(ORDER BY idMttoC ASC) AS 'Numero', idMttoC, Placa, FechaProgramacion FROM ReportesApp_Mantenimiento_MttoCorrectivo_Registro
		WHERE Placa = @PlacaEliminada AND YEAR(FechaProgramacion) = YEAR(@FechaEliminada) AND MONTH(FechaProgramacion) = MONTH(@FechaEliminada) AND Estado = 'PENDIENTE'

		UPDATE C
		SET C.D1 = NULL, C.D2 = NULL, C.D3 = NULL, C.D4 = NULL, C.D5 = NULL, C.D6 = NULL, C.D7 = NULL, C.D8 = NULL, C.D9 = NULL, C.D10 = NULL,
		C.D11 = NULL, C.D12 = NULL, C.D13 = NULL, C.D14 = NULL, C.D15 = NULL, C.D16 = NULL, C.D17 = NULL, C.D18 = NULL, C.D19 = NULL, C.D20 = NULL,
		C.D21 = NULL, C.D22 = NULL, C.D23 = NULL, C.D24 = NULL, C.D25 = NULL, C.D26 = NULL, C.D27 = NULL, C.D28 = NULL, C.D29 = NULL, C.D30 = NULL, C.D31 = NULL
		FROM ReportesApp_Mantenimiento_MttoCorrectivo_CalendarioView C
		WHERE C.Placa = @PlacaEliminada AND C.Anio = YEAR(@FechaEliminada) AND C.Mes = MONTH(@FechaEliminada)

		WHILE (@ContadorP <= (SELECT COUNT(Numero) FROM @MTTOCP)) BEGIN
			DECLARE @MEPlaca VARCHAR(20) = (SELECT Placa FROM @MTTOCP WHERE Numero = @ContadorP)
			DECLARE @MEFechaProgramacion DATE = (SELECT CONVERT(DATE,FechaProgramacion) FROM @MTTOCP WHERE Numero = @ContadorP)

			UPDATE C
			SET C.D1 = CASE WHEN DAY(@MEFechaProgramacion) = 1 THEN ISNULL(C.D1,0) + 1 ELSE C.D1 END,
				C.D2 = CASE WHEN DAY(@MEFechaProgramacion) = 2 THEN ISNULL(C.D2,0) + 1 ELSE C.D2 END,
				C.D3 = CASE WHEN DAY(@MEFechaProgramacion) = 3 THEN ISNULL(C.D3,0) + 1 ELSE C.D3 END,
				C.D4 = CASE WHEN DAY(@MEFechaProgramacion) = 4 THEN ISNULL(C.D4,0) + 1 ELSE C.D4 END,
				C.D5 = CASE WHEN DAY(@MEFechaProgramacion) = 5 THEN ISNULL(C.D5,0) + 1 ELSE C.D5 END,
				C.D6 = CASE WHEN DAY(@MEFechaProgramacion) = 6 THEN ISNULL(C.D6,0) + 1 ELSE C.D6 END,
				C.D7 = CASE WHEN DAY(@MEFechaProgramacion) = 7 THEN ISNULL(C.D7,0) + 1 ELSE C.D7 END,
				C.D8 = CASE WHEN DAY(@MEFechaProgramacion) = 8 THEN ISNULL(C.D8,0) + 1 ELSE C.D8 END,
				C.D9 = CASE WHEN DAY(@MEFechaProgramacion) = 9 THEN ISNULL(C.D9,0) + 1 ELSE C.D9 END,
				C.D10 = CASE WHEN DAY(@MEFechaProgramacion) = 10 THEN ISNULL(C.D10,0) + 1 ELSE C.D10 END,
				C.D11 = CASE WHEN DAY(@MEFechaProgramacion) = 11 THEN ISNULL(C.D11,0) + 1 ELSE C.D11 END,
				C.D12 = CASE WHEN DAY(@MEFechaProgramacion) = 12 THEN ISNULL(C.D12,0) + 1 ELSE C.D12 END,
				C.D13 = CASE WHEN DAY(@MEFechaProgramacion) = 13 THEN ISNULL(C.D13,0) + 1 ELSE C.D13 END,
				C.D14 = CASE WHEN DAY(@MEFechaProgramacion) = 14 THEN ISNULL(C.D14,0) + 1 ELSE C.D14 END,
				C.D15 = CASE WHEN DAY(@MEFechaProgramacion) = 15 THEN ISNULL(C.D15,0) + 1 ELSE C.D15 END,
				C.D16 = CASE WHEN DAY(@MEFechaProgramacion) = 16 THEN ISNULL(C.D16,0) + 1 ELSE C.D16 END,
				C.D17 = CASE WHEN DAY(@MEFechaProgramacion) = 17 THEN ISNULL(C.D17,0) + 1 ELSE C.D17 END,
				C.D18 = CASE WHEN DAY(@MEFechaProgramacion) = 18 THEN ISNULL(C.D18,0) + 1 ELSE C.D18 END,
				C.D19 = CASE WHEN DAY(@MEFechaProgramacion) = 19 THEN ISNULL(C.D19,0) + 1 ELSE C.D19 END,
				C.D20 = CASE WHEN DAY(@MEFechaProgramacion) = 20 THEN ISNULL(C.D20,0) + 1 ELSE C.D20 END,
				C.D21 = CASE WHEN DAY(@MEFechaProgramacion) = 21 THEN ISNULL(C.D21,0) + 1 ELSE C.D21 END,
				C.D22 = CASE WHEN DAY(@MEFechaProgramacion) = 22 THEN ISNULL(C.D22,0) + 1 ELSE C.D22 END,
				C.D23 = CASE WHEN DAY(@MEFechaProgramacion) = 23 THEN ISNULL(C.D23,0) + 1 ELSE C.D23 END,
				C.D24 = CASE WHEN DAY(@MEFechaProgramacion) = 24 THEN ISNULL(C.D24,0) + 1 ELSE C.D24 END,
				C.D25 = CASE WHEN DAY(@MEFechaProgramacion) = 25 THEN ISNULL(C.D25,0) + 1 ELSE C.D25 END,
				C.D26 = CASE WHEN DAY(@MEFechaProgramacion) = 26 THEN ISNULL(C.D26,0) + 1 ELSE C.D26 END,
				C.D27 = CASE WHEN DAY(@MEFechaProgramacion) = 27 THEN ISNULL(C.D27,0) + 1 ELSE C.D27 END,
				C.D28 = CASE WHEN DAY(@MEFechaProgramacion) = 28 THEN ISNULL(C.D28,0) + 1 ELSE C.D28 END,
				C.D29 = CASE WHEN DAY(@MEFechaProgramacion) = 29 THEN ISNULL(C.D29,0) + 1 ELSE C.D29 END,
				C.D30 = CASE WHEN DAY(@MEFechaProgramacion) = 30 THEN ISNULL(C.D30,0) + 1 ELSE C.D30 END,
				C.D31 = CASE WHEN DAY(@MEFechaProgramacion) = 31 THEN ISNULL(C.D31,0) + 1 ELSE C.D31 END
			FROM ReportesApp_Mantenimiento_MttoCorrectivo_CalendarioView C
			WHERE C.Placa = @MEPlaca AND C.Anio = YEAR(@MEFechaProgramacion) AND C.Mes = MONTH(@MEFechaProgramacion)

			SET @ContadorP = @ContadorP + 1
		END

		SET @Exito = '0 = Mantenimiento eliminado correctamente.'
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

------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 27-11-2024
-- Description: LISTAR CALENDARIO MTTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoCorrectivo_ListarCalendarioMtto]
@Periodo VARCHAR(6),
@Placa VARCHAR(30),
@Operacion VARCHAR(30)
AS
BEGIN
	DECLARE @Anio SMALLINT = RIGHT(@periodo,4)
	DECLARE @Mes INT = LEFT(@periodo,2)
	DECLARE @DiaInicio INT = 1
	DECLARE @CantDias INT = 0
	DECLARE @FechaIniArmada VARCHAR(10) = '01/'+LEFT(@Periodo,2)+'/'+RIGHT(@Periodo,4)
	DECLARE @FDesde DATE, @FHasta DATE

	SET @FDesde = @FechaIniArmada
	SET @FHasta = DATEADD(ms,-3,DATEADD(mm,0,DATEADD(mm,DATEDIFF(mm,0, @FDesde)+1,0)))

	IF (@FDesde IS NULL) BEGIN
		SET @DiaInicio = 27
	END
	ELSE BEGIN  
		SET @DiaInicio = DAY(@FDesde) 
		SET @CantDias = DATEDIFF(DAY,@FDesde,@FHasta)+1
	END

	DECLARE @T_Prueba TABLE (Placa VARCHAR(50), Operacion VARCHAR(50), TipoUnidad VARCHAR(250), D1 VARCHAR(15), D2 VARCHAR(15),
							 D3 VARCHAR(15), D4 VARCHAR(15), D5 VARCHAR(15), D6 VARCHAR(15), D7 VARCHAR(15), D8 VARCHAR(15), D9 VARCHAR(15),
							 D10 VARCHAR(15), D11 VARCHAR(15), D12 VARCHAR(15), D13 VARCHAR(15), D14 VARCHAR(15), D15 VARCHAR(15), D16 VARCHAR(15),
							 D17 VARCHAR(15), D18 VARCHAR(15), D19 VARCHAR(15), D20 VARCHAR(15), D21 VARCHAR(15), D22 VARCHAR(15), D23 VARCHAR(15),
							 D24 VARCHAR(15), D25 VARCHAR(15), D26 VARCHAR(15), D27 VARCHAR(15), D28 VARCHAR(15), D29 VARCHAR(15), D30 VARCHAR(15), D31 VARCHAR(15))

	DECLARE @D1 VARCHAR(15), @D2 VARCHAR(15), @D3 VARCHAR(15), @D4 VARCHAR(15), @D5 VARCHAR(15), @D6 VARCHAR(15), @D7 VARCHAR(15), @D8 VARCHAR(15),
			@D9 VARCHAR(15), @D10 VARCHAR(15), @D11 VARCHAR(15), @D12 VARCHAR(15), @D13 VARCHAR(15), @D14 VARCHAR(15), @D15 VARCHAR(15), @D16 VARCHAR(15),
			@D17 VARCHAR(15), @D18 VARCHAR(15), @D19 VARCHAR(15), @D20 VARCHAR(15), @D21 VARCHAR(15), @D22 VARCHAR(15), @D23 VARCHAR(15), @D24 VARCHAR(15),
			@D25 VARCHAR(15), @D26 VARCHAR(15), @D27 VARCHAR(15), @D28 VARCHAR(15), @D29 VARCHAR(15), @D30 VARCHAR(15), @D31 VARCHAR(15)

	INSERT INTO @T_Prueba (Placa, Operacion, TipoUnidad)
	VALUES ('<PLACA>', 'Operacion', 'TipoUnidad')

	DECLARE @FechaIni DATE, @FechaFin DATE, @FechaVer DATE

	IF (@FDesde IS NOT NULL) BEGIN 
		SET @FechaVer = CAST(CAST(@DiaInicio AS VARCHAR(2))+'/'+ RIGHT('0'+ CAST(month(@FDesde) AS VARCHAR(2)),2)+'/'+CAST(@Anio as VARCHAR(4)) AS DATE)
	END
	ELSE BEGIN
		SET @FechaVer = CAST(CAST(@DiaInicio AS VARCHAR(2))+'/'+ RIGHT('0'+ CAST(@Mes AS VARCHAR(2)),2)+'/'+CAST(@Anio as VARCHAR(4)) AS DATE)
	END

	SET @FechaIni = @FechaVer
	SET @FechaFin = DATEADD(D,-1, DATEADD(MONTH,1,@FechaVer))

	IF (@Operacion = 'TODO') BEGIN
		INSERT INTO @T_Prueba (Placa, Operacion, TipoUnidad)
		SELECT Placa, Operacion, TipoUnidad FROM ReportesApp_Mantenimiento_MttoCorrectivo_CalendarioView 
		WHERE Anio = YEAR(@FechaVer) AND Mes = MONTH(@FechaVer) AND (Placa IS NULL OR Placa LIKE '%' + @Placa + '%')
	END
	ELSE BEGIN
		INSERT INTO @T_Prueba (Placa, Operacion, TipoUnidad)
		SELECT Placa, Operacion, TipoUnidad FROM ReportesApp_Mantenimiento_MttoCorrectivo_CalendarioView 
		WHERE Anio = YEAR(@FechaVer) AND Mes = MONTH(@FechaVer) AND (Placa IS NULL OR Placa LIKE '%' + @Placa + '%') AND
		(Operacion IS NULL OR Operacion LIKE '%' + @Operacion + '%')
	END

	SET @FechaVer = @FechaIni

	DECLARE @NroDia INT
	DECLARE @DiaMes INT
	DECLARE @Concatenado VARCHAR(15)

	SET @NroDia = 1

	WHILE @FechaVer <= @FechaFin BEGIN
		SET @Concatenado = LEFT(UPPER(DATENAME(WEEKDAY, @FechaVer)), 3) + '' + RIGHT( '0' + CAST(DAY(@FechaVer) AS VARCHAR(2)),2)

		UPDATE @T_Prueba
		SET D1 = CASE WHEN @NroDia = 1 THEN @Concatenado ELSE D1 END,
			D2 = CASE WHEN @NroDia = 2 THEN @Concatenado ELSE D2 END,
			D3 = CASE WHEN @NroDia = 3 THEN @Concatenado ELSE D3 END,
			D4 = CASE WHEN @NroDia = 4 THEN @Concatenado ELSE D4 END,
			D5 = CASE WHEN @NroDia = 5 THEN @Concatenado ELSE D5 END,
			D6 = CASE WHEN @NroDia = 6 THEN @Concatenado ELSE D6 END,
			D7 = CASE WHEN @NroDia = 7 THEN @Concatenado ELSE D7 END,
			D8 = CASE WHEN @NroDia = 8 THEN @Concatenado ELSE D8 END,
			D9 = CASE WHEN @NroDia = 9 THEN @Concatenado ELSE D9 END,
			D10 = CASE WHEN @NroDia = 10 THEN @Concatenado ELSE D10 END,
			D11 = CASE WHEN @NroDia = 11 THEN @Concatenado ELSE D11 END,
			D12 = CASE WHEN @NroDia = 12 THEN @Concatenado ELSE D12 END,
			D13 = CASE WHEN @NroDia = 13 THEN @Concatenado ELSE D13 END,
			D14 = CASE WHEN @NroDia = 14 THEN @Concatenado ELSE D14 END,
			D15 = CASE WHEN @NroDia = 15 THEN @Concatenado ELSE D15 END,
			D16 = CASE WHEN @NroDia = 16 THEN @Concatenado ELSE D16 END,
			D17 = CASE WHEN @NroDia = 17 THEN @Concatenado ELSE D17 END,
			D18 = CASE WHEN @NroDia = 18 THEN @Concatenado ELSE D18 END,
			D19 = CASE WHEN @NroDia = 19 THEN @Concatenado ELSE D19 END,
			D20 = CASE WHEN @NroDia = 20 THEN @Concatenado ELSE D20 END,
			D21 = CASE WHEN @NroDia = 21 THEN @Concatenado ELSE D21 END,
			D22 = CASE WHEN @NroDia = 22 THEN @Concatenado ELSE D22 END,
			D23 = CASE WHEN @NroDia = 23 THEN @Concatenado ELSE D23 END,
			D24 = CASE WHEN @NroDia = 24 THEN @Concatenado ELSE D24 END,
			D25 = CASE WHEN @NroDia = 25 THEN @Concatenado ELSE D25 END,
			D26 = CASE WHEN @NroDia = 26 THEN @Concatenado ELSE D26 END,
			D27 = CASE WHEN @NroDia = 27 THEN @Concatenado ELSE D27 END,
			D28 = CASE WHEN @NroDia = 28 THEN @Concatenado ELSE D28 END,
			D29 = CASE WHEN @NroDia = 29 THEN @Concatenado ELSE D29 END,
			D30 = CASE WHEN @NroDia = 30 THEN @Concatenado ELSE D30 END,
			D31 = CASE WHEN @NroDia = 31 THEN @Concatenado ELSE D31 END
		WHERE Placa = '<PLACA>'

		SET @DiaMes = DAY(@FechaVer)

		UPDATE @T_Prueba
		SET D1 = CASE WHEN @NroDia = 1 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D1 END)
		ELSE T.D1 END,

		D2 = CASE WHEN @NroDia =2 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D2 END)
		ELSE T.D2 END,

		D3 = CASE WHEN @NroDia = 3 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D3 END)
		ELSE T.D3 END,

		D4 = CASE WHEN @NroDia = 4 THEN
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D4 END)
		ELSE T.D4 END,

		D5 = CASE WHEN @NroDia = 5 THEN  
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D5 END)
		ELSE T.D5 END,

		D6 = CASE WHEN @NroDia =6 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D6 END)
		ELSE T.D6 END,

		D7 = CASE WHEN @NroDia = 7 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D7 END)
		ELSE T.D7 END,

		D8 = CASE WHEN @NroDia = 8 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D8 END)
		ELSE T.D8 END,

		D9 = CASE WHEN @NroDia = 9 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D9 END)
		ELSE T.D9 END,

		D10 = CASE WHEN @NroDia = 10 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D10 END)
		ELSE T.D10 END,

		D11 = CASE WHEN @NroDia = 11 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D11 END)
		ELSE T.D11 END,

		D12 = CASE WHEN @NroDia = 12 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D12 END)
		ELSE T.D12 END,

		D13 = CASE WHEN @NroDia = 13 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D13 END)
		ELSE T.D13 END,

		D14 = CASE WHEN @NroDia = 14 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D14 END)
		ELSE T.D14 END,

		D15 = CASE WHEN @NroDia = 15 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D15 END)
		ELSE T.D15 END,

		D16 = CASE WHEN @NroDia = 16 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D16 END)
		ELSE T.D16 END,

		D17 = CASE WHEN @NroDia = 17 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D17 END)
		ELSE T.D17 END,

		D18 = CASE WHEN @NroDia = 18 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D18 END)
		ELSE T.D18 END,

		D19 = CASE WHEN @NroDia = 19 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D19 END)
		ELSE T.D19 END,

		D20 = CASE WHEN @NroDia = 20 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D20 END)
		ELSE T.D20 END,

		D21 = CASE WHEN @NroDia = 21 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D21 END)
		ELSE T.D21 END,

		D22 = CASE WHEN @NroDia = 22 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D22 END)
		ELSE T.D22 END,

		D23 = CASE WHEN @NroDia = 23 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D23 END)
		ELSE T.D23 END,

		D24 = CASE WHEN @NroDia = 24 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D24 END)
		ELSE T.D24 END,

		D25 = CASE WHEN @NroDia = 25 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D25 END)
		ELSE T.D25 END,

		D26 = CASE WHEN @NroDia = 26 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D26 END)
		ELSE T.D26 END,

		D27 = CASE WHEN @NroDia = 27 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D27 END)
		ELSE T.D27 END,

		D28 = CASE WHEN @NroDia = 28 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D28 END)
		ELSE T.D28 END,

		D29 = CASE WHEN @NroDia = 29 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D29 END)
		ELSE T.D29 END,

		D30 = CASE WHEN @NroDia = 30 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D30 END)
		ELSE T.D30 END,

		D31 = CASE WHEN @NroDia = 31 THEN 
		(CASE WHEN @DiaMes = 1 THEN A.D1 
				WHEN @DiaMes = 2 THEN A.D2 
				WHEN @DiaMes = 3 THEN A.D3 
				WHEN @DiaMes = 4 THEN A.D4 
				WHEN @DiaMes = 5 THEN A.D5 
				WHEN @DiaMes = 6 THEN A.D6 
				WHEN @DiaMes = 7 THEN A.D7 
				WHEN @DiaMes = 8 THEN A.D8 
				WHEN @DiaMes = 9 THEN A.D9 
				WHEN @DiaMes = 10 THEN A.D10 
				WHEN @DiaMes = 11 THEN A.D11 
				WHEN @DiaMes = 12 THEN A.D12 
				WHEN @DiaMes = 13 THEN A.D13 
				WHEN @DiaMes = 14 THEN A.D14 
				WHEN @DiaMes = 15 THEN A.D15 
				WHEN @DiaMes = 16 THEN A.D16 
				WHEN @DiaMes = 17 THEN A.D17 
				WHEN @DiaMes = 18 THEN A.D18 
				WHEN @DiaMes = 19 THEN A.D19 
				WHEN @DiaMes = 20 THEN A.D20 
				WHEN @DiaMes = 21 THEN A.D21 
				WHEN @DiaMes = 22 THEN A.D22 
				WHEN @DiaMes = 23 THEN A.D23 
				WHEN @DiaMes = 24 THEN A.D24 
				WHEN @DiaMes = 25 THEN A.D25 
				WHEN @DiaMes = 26 THEN A.D26 
				WHEN @DiaMes = 27 THEN A.D27 
				WHEN @DiaMes = 28 THEN A.D28 
				WHEN @DiaMes = 29 THEN A.D29 
				WHEN @DiaMes = 30 THEN A.D30 
				WHEN @DiaMes = 31 THEN A.D31 
		ELSE T.D31 END)
		ELSE T.D31 END
		FROM @T_Prueba AS T 
		LEFT JOIN ReportesApp_Mantenimiento_MttoCorrectivo_CalendarioView AS A WITH(NOLOCK) ON T.Placa = A.Placa
		WHERE A.Anio = YEAR(@FechaVer) AND A.Mes = MONTH(@FechaVer) 

		SET @FechaVer = DATEADD(D,1,@FechaVer)

		IF (@FDesde IS NOT NULL) AND (@NroDia = @CantDias OR @CantDias = 0)  BREAK;

		SET @NroDia = @NroDia + 1
	END

	IF (@CantDias = 28) BEGIN
		SELECT @D1=D1, @D2=D2, @D3=D3, @D4=D4, @D5=D5, @D6=D6, @D7=D7, @D8=D8, @D9=D9, @D10=D10, @D11=D11, @D12=D12, @D13=D13, @D14=D14,
		@D15=D15, @D16=D16, @D17=D17, @D18=D18, @D19=D19, @D20=D20, @D21=D21, @D22=D22, @D23=D23, @D24=D24, @D25=D25, @D26=D26, @D27=D27, @D28=D28
		FROM @T_Prueba WHERE Placa = '<PLACA>'
	END

	IF (@CantDias = 29) BEGIN
		SELECT @D1=D1, @D2=D2, @D3=D3, @D4=D4, @D5=D5, @D6=D6, @D7=D7, @D8=D8, @D9=D9, @D10=D10, @D11=D11, @D12=D12, @D13=D13, @D14=D14, @D15=D15,
		@D16=D16, @D17=D17, @D18=D18, @D19=D19, @D20=D20, @D21=D21, @D22=D22, @D23=D23, @D24=D24, @D25=D25, @D26=D26, @D27=D27, @D28=D28, @D29=D29
		FROM @T_Prueba WHERE Placa = '<PLACA>'
	END

	IF (@CantDias = 30) BEGIN
		SELECT @D1=D1, @D2=D2, @D3=D3, @D4=D4, @D5=D5, @D6=D6, @D7=D7, @D8=D8, @D9=D9, @D10=D10, @D11=D11, @D12=D12, @D13=D13, @D14=D14, @D15=D15,
		@D16=D16, @D17=D17, @D18=D18, @D19=D19, @D20=D20, @D21=D21, @D22=D22, @D23=D23, @D24=D24, @D25=D25, @D26=D26, @D27=D27, @D28=D28, @D29=D29, @D30=D30
		FROM @T_Prueba WHERE Placa = '<PLACA>'
	END

	IF (@CantDias = 31) BEGIN
		SELECT @D1=D1, @D2=D2, @D3=D3, @D4=D4, @D5=D5, @D6=D6, @D7=D7, @D8=D8, @D9=D9, @D10=D10, @D11=D11, @D12=D12, @D13=D13, @D14=D14, @D15=D15, @D16=D16, @D17=D17,
		@D18=D18, @D19=D19, @D20=D20, @D21=D21, @D22=D22, @D23=D23, @D24=D24, @D25=D25, @D26=D26, @D27=D27, @D28=D28, @D29=D29, @D30=D30, @D29=D29, @D30=D30, @D31=D31
		FROM @T_Prueba WHERE Placa = '<PLACA>'
	END

	CREATE TABLE #PruebaView (Placa VARCHAR(50), Operacion VARCHAR(50), TipoUnidad VARCHAR(250), D1 VARCHAR(15),D2 VARCHAR(15),D3 VARCHAR(15),
							  D4 VARCHAR(15),D5 VARCHAR(15),D6 VARCHAR(15),D7 VARCHAR(15),D8 VARCHAR(15),D9 VARCHAR(15),D10 VARCHAR(15),
							  D11 VARCHAR(15),D12 VARCHAR(15),D13 VARCHAR(15),D14 VARCHAR(15),D15 VARCHAR(15),D16 VARCHAR(15),D17 VARCHAR(15),
							  D18 VARCHAR(15),D19 VARCHAR(15),D20 VARCHAR(15),D21 VARCHAR(15),D22 VARCHAR(15),D23 VARCHAR(15),D24 VARCHAR(15),
							  D25 VARCHAR(15),D26 VARCHAR(15),D27 VARCHAR(15),D28 VARCHAR(15),D29 VARCHAR(15),D30 VARCHAR(15),D31 VARCHAR(15))

	DECLARE @SQL VARCHAR(5000)
	
	INSERT INTO #PruebaView
	SELECT * FROM @T_Prueba WHERE Placa <> '<PLACA>'

	IF @CantDias = 28 BEGIN
		SET @SQL='SELECT Placa AS PLACA, Operacion AS OPERACION, TipoUnidad AS TIPO_UNIDAD, D1 AS '+@D1+',D2 AS '+@D2+',D3 AS '+@D3+',D4 AS '+@D4+
		    ',D5 AS '+@D5+',D6 AS '+@D6+',D7 AS '+@D7+',D8 AS '+@D8+',D9 AS '+@D9+',D10 AS '+@D10+',D11 AS '+@D11+',D12 AS '+@D12+',D13 AS '+@D13+
			',D14 AS '+@D14+',D15 AS '+@D15+',D16 AS '+@D16+',D17 AS '+@D17+',D18 AS '+@D18+',D19 AS '+@D19+',D20 AS '+@D20+',D21 AS '+@D21+',D22 AS '+@D22+
			',D23 AS '+@D23+',D24 AS '+@D24+',D25 AS '+@D25+',D26 AS '+@D26+',D27 AS '+@D27+',D28 AS '+@D28+' FROM #PruebaView ORDER BY Placa'
		EXEC (@SQL)
		DROP TABLE #PruebaView
	END

	IF @CantDias = 29 BEGIN
		SET @SQL='SELECT Placa AS PLACA, Operacion AS OPERACION, TipoUnidad AS TIPO_UNIDAD, D1 AS '+@D1+',D2 AS '+@D2+',D3 AS '+@D3+',D4 AS '+@D4+
			',D5 AS '+@D5+',D6 AS '+@D6+',D7 AS '+@D7+',D8 AS '+@D8+',D9 AS '+@D9+',D10 AS '+@D10+',D11 AS '+@D11+',D12 AS '+@D12+',D13 AS '+@D13+
			',D14 AS '+@D14+',D15 AS '+@D15+',D16 AS '+@D16+',D17 AS '+@D17+',D18 AS '+@D18+',D19 AS '+@D19+',D20 AS '+@D20+',D21 AS '+@D21+',D22 AS '+@D22+
			',D23 AS '+@D23+',D24 AS '+@D24+',D25 AS '+@D25+',D26 AS '+@D26+',D27 AS '+@D27+',D28 AS '+@D28+',D29 AS '+@D29+' FROM #PruebaView ORDER BY Placa'
		EXEC (@SQL)
		DROP TABLE #PruebaView	
	END

	IF @CantDias = 30 BEGIN
		SET @SQL='SELECT Placa AS PLACA, Operacion AS OPERACION, TipoUnidad AS TIPO_UNIDAD, D1 AS '+@D1+',D2 AS '+@D2+',D3 AS '+@D3+',D4 AS '+@D4+
			',D5 AS '+@D5+',D6 AS '+@D6+',D7 AS '+@D7+',D8 AS '+@D8+',D9 AS '+@D9+',D10 AS '+@D10+',D11 AS '+@D11+',D12 AS '+@D12+',D13 AS '+@D13+
			',D14 AS '+@D14+',D15 AS '+@D15+',D16 AS '+@D16+',D17 AS '+@D17+',D18 AS '+@D18+',D19 AS '+@D19+',D20 AS '+@D20+',D21 AS '+@D21+',D22 AS '+@D22+
			',D23 AS '+@D23+',D24 AS '+@D24+',D25 AS '+@D25+',D26 AS '+@D26+',D27 AS '+@D27+',D28 AS '+@D28+',D29 AS '+@D29+',D30 AS '+@D30+
			' FROM #PruebaView ORDER BY Placa'
		EXEC (@SQL)
		DROP TABLE #PruebaView
	END

	IF @CantDias = 31 BEGIN
		SET @SQL='SELECT Placa AS PLACA, Operacion AS OPERACION, TipoUnidad AS TIPO_UNIDAD, D1 AS '+@D1+',D2 AS '+@D2+',D3 AS '+@D3+',D4 AS '+@D4+
			',D5 AS '+@D5+',D6 AS '+@D6+',D7 AS '+@D7+',D8 AS '+@D8+',D9 AS '+@D9+',D10 AS '+@D10+',D11 AS '+@D11+',D12 AS '+@D12+',D13 AS '+@D13+
			',D14 AS '+@D14+',D15 AS '+@D15+',D16 AS '+@D16+',D17 AS '+@D17+',D18 AS '+@D18+',D19 AS '+@D19+',D20 AS '+@D20+',D21 AS '+@D21+',D22 AS '+@D22+
			',D23 AS '+@D23+',D24 AS '+@D24+',D25 AS '+@D25+',D26 AS '+@D26+',D27 AS '+@D27+',D28 AS '+@D28+',D29 AS '+@D29+',D30 AS '+@D30+',D31 AS '+@D31+
			' FROM #PruebaView ORDER BY Placa'
		EXEC (@SQL)
		DROP TABLE #PruebaView
	END
END

-----------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 28-11-2024
-- Description:	LISTAR MTTOS PROGRAMADOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MttoCorrectivo_ListarMttoProgramado]
@Placa VARCHAR(30),
@FechaProg DATE
AS
BEGIN
	SELECT idMttoC, Placa, Descripcion AS 'DESCRIPCION', Observacion AS 'OBSERVACION'
	FROM ReportesApp_Mantenimiento_MttoCorrectivo_Registro
	WHERE (Placa = @Placa) AND (CONVERT(DATE,@FechaProg) = CONVERT(DATE,FechaProgramacion)) --AND (Estado = 'PENDIENTE')
END