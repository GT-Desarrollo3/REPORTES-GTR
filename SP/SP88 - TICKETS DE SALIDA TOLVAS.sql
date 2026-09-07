
-- CREAR TABLA ReportesApp_Operaciones_Previajes_TicketDespacho

-----------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 31-01-2023
-- Description:	REGISTRAR PLANILLA - TOLVAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_RegistrarPlanillaTolvas]
@idConductor INT,
@idTracto INT,
@idCarreta INT,
@idRuta INT,
@idGastoxRutaC INT,
@TotalEntregado DECIMAL(10,2),
@FechaViaje DATETIME,
@TotalDias INT,
@Usuario VARCHAR(50)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT
DECLARE @correlativo2 INT
DECLARE @Persona INT
DECLARE @NombreCompleto VARCHAR(350)
DECLARE @Contador INT
DECLARE @TEMP_ADELANTOS TABLE(Nro INT, NumeroAdelanto INT, Planilla VARCHAR(20), IdProgramacion INT, Sucursal VARCHAR(10), TotalDias INT)
DECLARE @i INT

SET @Exito = '0 = Planilla Registrada.'

SET @Persona = (SELECT P.Persona FROM OP_TR_Conductor C LEFT JOIN PersonaMast P ON P.Persona = C.IdPersona WHERE C.IdConductor = @idConductor)
SET @NombreCompleto = (SELECT LTRIM(RTRIM(P.NombreCompleto)) FROM OP_TR_Conductor C LEFT JOIN PersonaMast P ON P.Persona = C.IdPersona WHERE C.IdConductor = @idConductor)
SET @Contador = (SELECT COUNT(A.NumeroAdelanto) FROM AP_GastoAdelanto A				LEFT JOIN PersonaMast P ON (A.Persona = P.Persona)				LEFT JOIN AP_CajaChica C ON LTRIM(RTRIM(C.NumeroDocumentoInterno)) = LTRIM(RTRIM(A.NumeroDocumentoInterno))				LEFT JOIN ReportesApp_Operaciones_TicketGasto_Registro TG ON TG.CodGasto = LTRIM(RTRIM(A.NumeroDocumentoInterno))				LEFT JOIN ReportesApp_Operacion_Previaje_Registros R ON R.NroTicket = TG.NroTicket				WHERE (A.TipoAdelanto = 'E') AND (A.UnidadNegocio IN ('0001','0002','TRAN')) AND YEAR(A.FechaDocumento)>'2023' AND (A.Estado = 'PA')				AND (TG.EstadoLiquidacion = 0) AND (C.CajaChicaNumero IS NULL) AND (A.Descripcion LIKE '%' + 'GASTOS DE VIAJE' + '%') AND (A.Persona = @Persona))

IF (@Contador >= 2) BEGIN
	IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_TicketGasto_MaestroDesbloqueo WHERE idConductor = @idConductor AND FechaCompromiso > CONVERT(DATE,GETDATE()))) BEGIN
		SET @Exito = '0 = No hay planillas pendientes.'
	END
	ELSE BEGIN
		SET @Exito = '-1 = El conductor ' + @NombreCompleto +' tiene ' + CONVERT(VARCHAR,@Contador) + ' planillas pendientes de rendir, no puede emitir gasto. Política G.G. Debe pasar a liquidar.'
		GOTO Terminar
	END
	
	-- SET @Exito = '-1 = El conductor ' + @NombreCompleto +' tiene ' + CONVERT(VARCHAR,@Contador) + ' planillas pendientes de rendir, no puede emitir gasto. Política G.G. Debe pasar a liquidar.'
	-- GOTO Terminar
	
END

IF (@Contador = 1) BEGIN
	IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_TicketGasto_MaestroDesbloqueo WHERE idConductor = @idConductor AND FechaCompromiso > CONVERT(DATE,GETDATE()))) BEGIN
		SET @Exito = '0 = No hay planillas pendientes.'
	END	ELSE BEGIN
		INSERT INTO @TEMP_ADELANTOS		SELECT ROW_NUMBER() OVER(ORDER BY A.NumeroAdelanto ASC), A.NumeroAdelanto, LTRIM(RTRIM(A.NumeroDocumentoInterno)), TG.IdOperacion, 'TRUJILLO',		DATEDIFF(DAY,A.FechaDocumento,GETDATE())		FROM AP_GastoAdelanto A		LEFT JOIN PersonaMast P ON (A.Persona = P.Persona)		LEFT JOIN AP_CajaChica C ON LTRIM(RTRIM(C.NumeroDocumentoInterno)) = LTRIM(RTRIM(A.NumeroDocumentoInterno))		LEFT JOIN ReportesApp_Operaciones_TicketGasto_Registro TG ON TG.CodGasto = LTRIM(RTRIM(A.NumeroDocumentoInterno))		LEFT JOIN ReportesApp_Operacion_Previaje_Registros R ON R.NroTicket = TG.NroTicket		WHERE (A.TipoAdelanto = 'E') AND (A.UnidadNegocio IN ('0001','0002','TRAN')) AND YEAR(A.FechaDocumento)>'2023' AND (A.Estado = 'PA')		AND (TG.EstadoLiquidacion = 0) AND (C.CajaChicaNumero IS NULL) AND (A.Descripcion LIKE '%' + 'GASTOS DE VIAJE' + '%') AND (A.Persona = @Persona)
		ORDER BY A.NumeroAdelanto

		SET @i = 1
		WHILE(@i <= (SELECT COUNT(Nro) FROM @TEMP_ADELANTOS)) BEGIN
			IF ((SELECT IdProgramacion FROM @TEMP_ADELANTOS WHERE Nro = @i) = 1) BEGIN		-- TOLVAS
				IF ((SELECT TotalDias FROM @TEMP_ADELANTOS WHERE Nro = @i) >= 4) BEGIN
					SET @Exito = '-2 = El conductor ' + @NombreCompleto +' tiene la planilla ' + (SELECT Planilla FROM @TEMP_ADELANTOS WHERE Nro = @i)
						+ ' pendiente de rendir con más de 4 días de antiguedad, no puede emitir gasto. Política G.G. Debe pasar a liquidar.'
					GOTO Terminar
				END
				ELSE BEGIN
					SET @Exito = '0 = No hay planillas pendientes.' 
				END
			END

			IF ((SELECT IdProgramacion FROM @TEMP_ADELANTOS WHERE Nro = @i) = 2) BEGIN		-- LINDLEY
				IF ((SELECT TotalDias FROM @TEMP_ADELANTOS WHERE Nro = @i) >= 10) BEGIN
					SET @Exito = '-2 = El conductor ' + @NombreCompleto +' tiene la planilla ' + (SELECT Planilla FROM @TEMP_ADELANTOS WHERE Nro = @i)
						+' pendiente de rendir con más de 10 días de antiguedad, no puede emitir gasto. Política G.G. Debe pasar a liquidar.'
					GOTO Terminar
				END
				ELSE BEGIN
					SET @Exito = '0 = No hay planillas pendientes.' 
				END
			END

			IF ((SELECT IdProgramacion FROM @TEMP_ADELANTOS WHERE Nro = @i) = 4) BEGIN		-- GENERAL
				IF ((SELECT TotalDias FROM @TEMP_ADELANTOS WHERE Nro = @i) >= 10) BEGIN
					SET @Exito = '-2 = El conductor ' + @NombreCompleto +' tiene la planilla ' + (SELECT Planilla FROM @TEMP_ADELANTOS WHERE Nro = @i)
						+' pendiente de rendir con más de 10 días de antiguedad, no puede emitir gasto. Política G.G. Debe pasar a liquidar.'
					GOTO Terminar
				END
				ELSE BEGIN
					SET @Exito = '0 = No hay planillas pendientes.' 
				END
			END

			IF ((SELECT IdProgramacion FROM @TEMP_ADELANTOS WHERE Nro = @i) = 3) BEGIN		-- LIMAGAS - TRUJILLO
				IF (((SELECT TotalDias FROM @TEMP_ADELANTOS WHERE Nro = @i) >= 7) AND ((SELECT Sucursal FROM @TEMP_ADELANTOS WHERE Nro = @i) = 'TRUJILLO')) BEGIN
					SET @Exito = '-2 = El conductor ' + @NombreCompleto +' tiene la planilla ' + (SELECT Planilla FROM @TEMP_ADELANTOS WHERE Nro = @i)
						+' pendiente de rendir con más de 7 días de antiguedad, no puede emitir gasto. Política G.G. Debe pasar a liquidar.'
					GOTO Terminar
				END
				ELSE BEGIN
					SET @Exito = '0 = No hay planillas pendientes.' 
				END
			END

			IF ((SELECT IdProgramacion FROM @TEMP_ADELANTOS WHERE Nro = @i) = 3) BEGIN		-- LIMAGAS - LIMA
				IF (((SELECT TotalDias FROM @TEMP_ADELANTOS WHERE Nro = @i) >= 15) AND ((SELECT Sucursal FROM @TEMP_ADELANTOS WHERE Nro = @i) = 'LIMA')) BEGIN
					SET @Exito = '-2 = El conductor ' + @NombreCompleto +' tiene la planilla ' + (SELECT Planilla FROM @TEMP_ADELANTOS WHERE Nro = @i)
						+' pendiente de rendir con más de 15 días de antiguedad, no puede emitir gasto. Política G.G. Debe pasar a liquidar.'
					GOTO Terminar
				END
				ELSE BEGIN
					SET @Exito = '0 = No hay planillas pendientes.' 
				END
			END

			SET @i = @i + 1
		END
	END
END

SET @correlativo = (SELECT MAX(idTicketGasto) FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE Anio = YEAR(GETDATE()))
SET @correlativo = ISNULL(@correlativo,0) + 1

BEGIN TRAN
BEGIN TRY
	INSERT INTO ReportesApp_Operaciones_TicketGasto_Registro(idTicketGasto, Anio, CodGasto, IdRuta, IdOperacion, IdTracto, IdCarreta, FechaViaje, TotalDias,
															 ViConductor, idGastoXRutaC, TotalEntregado, FechaCreacion, UsuarioCreacion,EstadoLiquidacion,Permiso)
	VALUES(@correlativo, YEAR(GETDATE()), SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo)),6)),
		   @idRuta, 1, @idTracto, @idCarreta, CONVERT(DATE,@FechaViaje), @TotalDias, @idConductor, @idGastoxRutaC, @TotalEntregado, GETDATE(), @Usuario, 0, 0)

	SET @correlativo2 = (SELECT MAX(idTicketDespacho) FROM ReportesApp_Operaciones_Previajes_TicketDespacho)
	SET @correlativo2 = ISNULL(@correlativo2,0) + 1

	INSERT INTO ReportesApp_Operaciones_Previajes_TicketDespacho(idTicketDespacho,CodigoPreviaje,Fecha,UsuarioCrea,FechaCrea)
	VALUES(@correlativo2,CONVERT(INT,SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo)),6))),
	GETDATE(),@Usuario,GETDATE())

	SET @Exito = '0 = Planilla Registrada. PL - ' + SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo)),6))
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

-----------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 21-07-2023
-- Description:	INSERTAR TICKET DE GASTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_RegistrarTicketGasto]
@NroTicket INT,
@idGastoxRutaC INT,
@TotalEntregado DECIMAL(10,2),
@Usuario VARCHAR(50)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT
DECLARE @correlativo2 INT
DECLARE @IdConductor INT
DECLARE @Contador INT
DECLARE @idOperacion INT

SET @Exito = '0 = Planilla Registrada.'

SET @IdConductor = (SELECT IdConductor FROM ReportesApp_Operacion_Previaje_Registros WHERE NroTicket = @NroTicket)
SET @Contador = (SELECT COUNT(TG.ViConductor) FROM ReportesApp_Operaciones_TicketGasto_Registro TG WHERE TG.EstadoLiquidacion = 0 AND TG.ViConductor = @IdConductor)

/*
IF (@Contador >= 4)
BEGIN
	SET @Exito = '-1 = Este conductor aún tiene 4 planillas por liquidar.'
	GOTO Terminar
END
*/

SET @correlativo = (SELECT MAX(idTicketGasto) FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE Anio = YEAR(GETDATE()))
SET @correlativo = ISNULL(@correlativo,0) + 1


BEGIN TRAN
BEGIN TRY
	INSERT INTO ReportesApp_Operaciones_TicketGasto_Registro(idTicketGasto, Anio, CodGasto, NroTicket, idGastoXRutaC, TotalEntregado, FechaCreacion, UsuarioCreacion,EstadoLiquidacion,Permiso)
	VALUES(@correlativo, YEAR(GETDATE()), SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo)),6)), @NroTicket, @idGastoxRutaC, @TotalEntregado, GETDATE(), @Usuario, 0, 0)

	UPDATE ReportesApp_Operaciones_TicketGasto_Registro
	SET ViConductor = (SELECT IdConductor FROM ReportesApp_Operacion_Previaje_Registros WHERE NroTicket = @NroTicket), IdOperacion = (SELECT TipoProgramacion FROM ReportesApp_Operacion_Previaje_Registros WHERE NroTicket = @NroTicket)
	WHERE CodGasto = SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo)),6))
	
	UPDATE ReportesApp_Operacion_Previaje_Registros
	SET Planilla = SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo)),6))
	WHERE NroTicket = @NroTicket

	SET @correlativo2 = (SELECT MAX(idTicketDespacho) FROM ReportesApp_Operaciones_Previajes_TicketDespacho)
	SET @correlativo2 = ISNULL(@correlativo2,0) + 1

	INSERT INTO ReportesApp_Operaciones_Previajes_TicketDespacho(idTicketDespacho,CodigoPreviaje,Fecha,UsuarioCrea,FechaCrea)
	VALUES(@correlativo2,@NroTicket,GETDATE(),@Usuario,GETDATE())
	
	SET @Exito = '0 = Planilla Registrada. PL - ' + SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo)),6))
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

-----------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 12-05-2025
-- Description:	GENERAR PLANILLA EVENTO
-- =============================================
/*
EXEC ReportesApp_Operaciones_TicketGasto_GenerarPlanillaEvento
@Planilla = '25008850',
@NroTicket = 2515228,
@TipoProgramacion = 2,
@idRuta = 333,
@IdConductor = 11829,
@TotalEntregado = 1090.00,
@Usuario = 'LQUEZADA'

SELECT * FROM OP_TR_CONDUCTOR WHERE Nombre LIKE '%OLAYA%'
SELECT * FROM OP_TR_RUTA WHERE Descripcion LIKE '%YURIMAGUAS%'
*/
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_GenerarPlanillaEvento]
@Planilla VARCHAR(30),
@NroTicket INT,
@TipoProgramacion INT,
@idRuta INT,
@IdConductor INT,
@TotalEntregado DECIMAL(10,2),
@Usuario VARCHAR(50)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT
DECLARE @correlativo2 INT
DECLARE @idGastoxRutaC INT

SET @Exito = '0 = Planilla Registrada.'

SET @correlativo = (SELECT MAX(idTicketGasto) FROM ReportesApp_Operaciones_TicketGasto_Registro WHERE Anio = YEAR(GETDATE()))
SET @correlativo = ISNULL(@correlativo,0) + 1

BEGIN TRAN
BEGIN TRY
	SET @idGastoxRutaC = (SELECT idGastoxRutaC FROM ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera WHERE IdRuta = @idRuta AND IdOperacion = @TipoProgramacion)

	INSERT INTO ReportesApp_Operaciones_TicketGasto_Registro(idTicketGasto, Anio, CodGasto, NroTicket, idGastoXRutaC, TotalEntregado, FechaCreacion,
	UsuarioCreacion,EstadoLiquidacion,Permiso,Observacion)
	VALUES(@correlativo, YEAR(GETDATE()), SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo)),6)),
	@NroTicket, @idGastoxRutaC, @TotalEntregado, GETDATE(), @Usuario, 0, 0, 'PLANILLA EVENTO')

	UPDATE ReportesApp_Operaciones_TicketGasto_Registro
	SET ViConductor = @IdConductor, IdOperacion = @TipoProgramacion
	WHERE CodGasto = SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo)),6))

	UPDATE ReportesApp_Operacion_Previaje_Registros
	SET Planilla = SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo)),6)),
	Viaticos = @TotalEntregado
	WHERE NroTicket = @NroTicket

	SET @correlativo2 = (SELECT MAX(idTicketDespacho) FROM ReportesApp_Operaciones_Previajes_TicketDespacho)
	SET @correlativo2 = ISNULL(@correlativo2,0) + 1

	INSERT INTO ReportesApp_Operaciones_Previajes_TicketDespacho(idTicketDespacho,CodigoPreviaje,Fecha,UsuarioCrea,FechaCrea)
	VALUES(@correlativo2,@NroTicket,GETDATE(),@Usuario,GETDATE())
	
	SET @Exito = '0 = Planilla de Evento Registrada. PLE - ' + SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo)),6))
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

-----------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 01-02-2023
-- Description:	LISTAR PLANILLAS TOLVAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_TicketGasto_ListarPlanillasTolvas]
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@NombreConductor VARCHAR(250),
@Planilla VARCHAR(50),
@Pendientes INT
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF @Pendientes = 0 BEGIN
		SELECT TG.idTicketGasto, TG.IdRuta, TG.IdOperacion AS 'TipoProgramacion', TG.IdTracto, TG.ViConductor AS 'IdConductor', TG.idGastoXRutaC,
		CONVERT(VARCHAR,TG.FechaViaje,103) AS 'FECHA_VIAJE', TG.CodGasto AS 'PLANILLA', ISNULL((SELECT TOP 1 CASE WHEN NumeroDocumentoInterno IS NOT NULL THEN ' ' ELSE 'PENDIENTE' END
		FROM AP_GastoAdelanto WHERE NumeroDocumentoInterno = TG.CodGasto AND (Estado = 'TR' OR Estado = 'PA') ORDER BY NumeroAdelanto),'') AS 'CAJA',
		C.Nombre AS 'CONDUCTOR', V.NumeroPlaca AS 'TRACTO', V2.NumeroPlaca AS 'CARRETA', VJ.Descripcion AS 'RUTA', TG.TotalDias AS 'DÍAS',
		TG.TotalEntregado - ISNULL(TG.GastoDiferencialV,0) - ISNULL(TG.GastoDiferencial,0) AS 'IMPORTE_TOTAL',
		ISNULL(TG.GastoDiferencialV,0) AS 'ADICIONAL', ISNULL(TG.GastoDiferencial,0) AS 'CAMBIO_RUTA', TG.UsuarioCreacion AS 'USUARIO'
		FROM ReportesApp_Operaciones_TicketGasto_Registro TG
		LEFT JOIN OP_TR_Ruta VJ ON VJ.IdRuta = TG.IdRuta
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = TG.IdTracto
		LEFT JOIN OP_TR_Vehiculo V2 WITH(NOLOCK) ON V2.IdVehiculo = TG.IdCarreta
		LEFT JOIN OP_TR_Conductor C ON C.IdConductor = TG.ViConductor
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera GC ON GC.idGastoXRutaC = TG.idGastoXRutaC
		WHERE (TG.EstadoLiquidacion = 0) AND (TG.IdOperacion = 1) AND (TG.FechaViaje BETWEEN @FINICIO AND @FFIN) AND (TG.CodGasto IS NULL OR TG.CodGasto LIKE '%' + @Planilla + '%')
		AND (C.Nombre IS NULL OR C.Nombre LIKE '%' + @NombreConductor + '%')
		ORDER BY idTicketGasto DESC
	END

	IF @Pendientes = 1 BEGIN
		SELECT TG.idTicketGasto, TG.IdRuta, TG.IdOperacion AS 'TipoProgramacion', TG.IdTracto, TG.ViConductor AS 'IdConductor', TG.idGastoXRutaC,
		CONVERT(VARCHAR,TG.FechaViaje,103) AS 'FECHA_VIAJE', TG.CodGasto AS 'PLANILLA', ISNULL((SELECT TOP 1 CASE WHEN NumeroDocumentoInterno IS NOT NULL THEN ' ' ELSE 'PENDIENTE' END
		FROM AP_GastoAdelanto WHERE NumeroDocumentoInterno = TG.CodGasto AND (Estado = 'TR' OR Estado = 'PA') ORDER BY NumeroAdelanto),'') AS 'CAJA',
		C.Nombre AS 'CONDUCTOR', V.NumeroPlaca AS 'TRACTO', V2.NumeroPlaca AS 'CARRETA', VJ.Descripcion AS 'RUTA', TG.TotalDias AS 'DÍAS',
		TG.TotalEntregado - ISNULL(TG.GastoDiferencialV,0) - ISNULL(TG.GastoDiferencial,0) AS 'IMPORTE_TOTAL',
		ISNULL(TG.GastoDiferencialV,0) AS 'ADICIONAL', ISNULL(TG.GastoDiferencial,0) AS 'CAMBIO_RUTA', TG.UsuarioCreacion AS 'USUARIO'
		FROM ReportesApp_Operaciones_TicketGasto_Registro TG
		LEFT JOIN OP_TR_Ruta VJ ON VJ.IdRuta = TG.IdRuta
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = TG.IdTracto
		LEFT JOIN OP_TR_Vehiculo V2 WITH(NOLOCK) ON V2.IdVehiculo = TG.IdCarreta
		LEFT JOIN OP_TR_Conductor C ON C.IdConductor = TG.ViConductor
		LEFT JOIN ReportesApp_Operaciones_TicketGasto_GastoXRutaCabecera GC ON GC.idGastoXRutaC = TG.idGastoXRutaC
		WHERE (TG.IdOperacion = 1) AND (TG.EstadoLiquidacion = 0) AND (TG.FechaViaje BETWEEN @FINICIO AND @FFIN) AND (TG.CodGasto IS NULL OR TG.CodGasto LIKE '%' + @Planilla + '%')
		AND (C.Nombre IS NULL OR C.Nombre LIKE '%' + @NombreConductor + '%')
		ORDER BY idTicketGasto DESC
	END
END

-----------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 07-10-2024
-- Description:	REGISTRAR TICKET DE DESPACHO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Previajes_RegistrarDespacho]
@CodigoPreviaje INT,
@Fecha DATE,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

BEGIN TRAN
BEGIN TRY
	IF (@Fecha > (SELECT TOP(1) Fecha FROM ReportesApp_Operaciones_Previajes_TicketDespacho WHERE CodigoPreviaje = @CodigoPreviaje)) BEGIN
		SET @Exito = '-2 = No puede imprimir un ticket en una fecha mayor a la fecha de registro del viaje.'
		ROLLBACK
		GOTO Terminar
	END

	IF (@Fecha < (SELECT TOP(1) Fecha FROM ReportesApp_Operaciones_Previajes_TicketDespacho WHERE CodigoPreviaje = @CodigoPreviaje)) BEGIN
		SET @Exito = '-1 = Este ticket ya fue impreso. No puede volver a imprimirlo.'
		ROLLBACK
		GOTO Terminar
	END

	SET @correlativo = (SELECT MAX(idTicketDespacho) FROM ReportesApp_Operaciones_Previajes_TicketDespacho)
	SET @correlativo = ISNULL(@correlativo,0) + 1

	INSERT INTO ReportesApp_Operaciones_Previajes_TicketDespacho(idTicketDespacho,CodigoPreviaje,Fecha,UsuarioCrea,FechaCrea)
	VALUES(@correlativo,@CodigoPreviaje,@Fecha,@Usuario,GETDATE())

	SET @Exito = '0 = Ticket de despacho registrado.'
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

---------------------------------------------------------------
