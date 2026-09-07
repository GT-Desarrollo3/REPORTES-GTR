-- CREAR TABLA: ReportesApp_Operaciones_Previajes_Tolvas_Cabecera
-- CREAR TABLA: ReportesApp_Operaciones_Previajes_Tolvas_Detalle

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 19-06-2023
-- Description:	INSERTAR TOLVAS DE PREVIAJES
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Operaciones_Previajes_Tolvas_Insertar]
@IdOT INT,
@Tarifa DECIMAL(16,6),
@idRemitente INT,
@Remitente VARCHAR(150),
@idPartida INT,
@DireccionPartida VARCHAR(200),
@idDestinatario INT,
@Destinatario VARCHAR(150),
@idDestino INT,
@DireccionDestino VARCHAR(200),
@FechaTraslado DATE,
@Producto VARCHAR(200),
@Ruta VARCHAR(200),
@xmlPlacaConductor VARCHAR(MAX),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT
DECLARE @UltimaOT INT
DECLARE @idoc INT
DECLARE @TEMP_EPPS TABLE(
		idPlaca INT,
		Placa VARCHAR(20),
		idCarreta INT,
		Carreta VARCHAR(20),
		idConductor INT,
		Conductor VARCHAR(250))

SET @Exito = '0 = Tolva añadida.'
SET @UltimaOT = (SELECT TOP 1 IdOT FROM ReportesApp_Operaciones_Previajes_Tolvas_Cabecera ORDER BY FechaCreacion DESC)

IF(@xmlPlacaConductor IS NOT NULL) BEGIN
	EXEC sp_xml_preparedocument @idoc OUTPUT, @xmlPlacaConductor
	INSERT INTO @TEMP_EPPS(idPlaca, Placa, idCarreta, Carreta, idConductor, Conductor)
	SELECT * FROM OPENXML(@idoc,'/r/d',1)
	WITH (idPlaca INT, Placa VARCHAR(20), idCarreta INT, Carreta VARCHAR(20), idConductor INT, Conductor VARCHAR(250));
	EXEC sp_xml_removedocument @idoc;
END

IF(@IdOT != @UltimaOT) BEGIN
	SET @correlativo = (SELECT MAX(idPreviajeTolvas) FROM ReportesApp_Operaciones_Previajes_Tolvas_Cabecera WHERE Anio = YEAR(GETDATE()))
	SET @correlativo = @correlativo + 1 
END
ELSE BEGIN
	SET @correlativo = (SELECT idPreviajeTolvas FROM ReportesApp_Operaciones_Previajes_Tolvas_Cabecera WHERE idPreviajeTolvas = @correlativo)
	IF (@correlativo IS NULL) BEGIN
		SET @correlativo = ISNULL(@correlativo,1)
	END
END

BEGIN TRAN
BEGIN TRY
	INSERT INTO ReportesApp_Operaciones_Previajes_Tolvas_Cabecera(idPreviajeTolvas, Anio, IdOT, Tarifa, idRemitente, Remitente, idPartida, DireccionPartida, idDestinatario,
				Destinatario, idDestino, DireccionDestino, FechaTraslado, Producto, Ruta, Estado, UsuarioCreacion, FechaCreacion, UltimoUsuario, UltimaModificacion)
	VALUES(@correlativo, YEAR(GETDATE()), @IdOT, @Tarifa, @idRemitente, @Remitente, @idPartida, @DireccionPartida, @idDestinatario, @Destinatario, @idDestino,
		   @DireccionDestino, @FechaTraslado, @Producto, @Ruta, 'PROGRAMADO', @Usuario, GETDATE(), @Usuario, GETDATE())
		   
	INSERT INTO ReportesApp_Operaciones_Previajes_Tolvas_Detalle(idPreviajeTolvas, Anio, idPlaca, Placa, idCarreta, Carreta, idConductor, Conductor)
	SELECT @correlativo, YEAR(GETDATE()), E.idPlaca, E.Placa, E.idCarreta, E.Carreta, E.idConductor, E.Conductor FROM @TEMP_EPPS E 
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

/*
exec ReportesApp_Operaciones_Previajes_Tolvas_Insertar @IdOT=22508,@Tarifa=2518.540000,@idRemitente=20323,@Remitente=N'AC LOGISTICA DEL PERU S.A.C',@idPartida=1,@DireccionPartida=N'CALLE LINDLEY #200 SANTA ROSA',@idDestinatario=1553,@Destinatario=N'GRUPO TRANSPESA S.A.C',@idDestino=2,@DireccionDestino=N'PARCELA RUSTICA U.C. 4808',@Producto=N'PTER /ENVASES/CAJAS/PARIHUELAS',@Ruta=N'TRUJILLO - COMAS',@xmlPlacaConductor=N'<r>
  <d idPlaca="4967" Placa="T5A979" idConductor="11390" Conductor="GERMAN REYES, ROSMEL MELVIN" />
  <d idPlaca="2939" Placa="T5G845" idConductor="9371" Conductor="GERONIMO RODRIGUEZ RAMON DAVID" />
</r>',@Usuario=N'GREYES'
*/

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 19-06-2023
-- Description:	LISTAR TOLVAS DE PREVIAJES
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Operaciones_Previajes_Tolvas_Listar]
@CodPreviaje VARCHAR(10),
@Remitente VARCHAR(150),
@FechaInicio VARCHAR(30),
@FechaFin VARCHAR(30)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'	
BEGIN
	SELECT PT.idPreviajeTolvas, PT.Anio, SUBSTRING(CONVERT(VARCHAR(20), PT.Anio),3,2) + CONVERT(VARCHAR(20),PT.idPreviajeTolvas) AS 'CODIGO',
	PT.IdOT AS 'OT', PT.Tarifa AS 'TARIFA', PT.Remitente AS 'REMITENTE', PT.DireccionPartida AS 'DIRECCION_PARTIDA', PT.Destinatario AS 'DESTINATARIO',
	PT.DireccionDestino AS 'DIRECCION_DESTINO', PT.FechaTraslado AS 'FECHA_TRASLADO', PT.Estado AS 'ESTADO_OPERACIÓN', PT.EstadoViaje AS 'ESTADO_VIAJE',
	PT.Producto AS 'PRODUCTO', PT.Ruta AS 'RUTA', PT.UsuarioCreacion, PT.FechaCreacion, PT.UltimoUsuario, PT.UltimaModificacion
	FROM ReportesApp_Operaciones_Previajes_Tolvas_Cabecera PT WHERE (((PT.Remitente LIKE '%' + @Remitente + '%') AND
	((SUBSTRING(CONVERT(VARCHAR(20),PT.Anio),3,2) + CONVERT(VARCHAR(20),PT.idPreviajeTolvas)) LIKE '%' + @CodPreviaje + '%')) AND
	(PT.FechaCreacion BETWEEN @FINICIO AND @FFIN)) ORDER BY PT.idPreviajeTolvas DESC
END

/*
exec ReportesApp_Operaciones_Previajes_Tolvas_Listar @CodPreviaje=N'',@Remitente=N'',@FechaInicio=N'20/06/2023',@FechaFin=N'21/06/2023'
*/

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 20-06-2023
-- Description:	SELECCIONAR TOLVAS DE PREVIAJES
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Operaciones_Previajes_Tolvas_SeleccionarPreviaje]
@idPreviajeTolvas INT,
@Anio INT
AS
BEGIN
	SELECT PT.idPreviajeTolvas, PT.Anio, PT.IdOT, PT.Tarifa, PT.idRemitente, PT.Remitente, PT.idPartida, PT.DireccionPartida, PT.idDestinatario,
	PT.Destinatario, PT.idDestino, PT.DireccionDestino, PT.FechaTraslado, PT.Producto, PT.Ruta, PD.idPreviajeTolvas, PD.Anio, PD.idPlaca, PD.Placa,
	PD.idCarreta, PD.Carreta, PD.idConductor, PD.Conductor FROM ReportesApp_Operaciones_Previajes_Tolvas_Cabecera PT
	LEFT JOIN ReportesApp_Operaciones_Previajes_Tolvas_Detalle PD ON (PT.idPreviajeTolvas = PD.idPreviajeTolvas AND PT.Anio = PD.Anio)
	WHERE PT.idPreviajeTolvas = @idPreviajeTolvas AND PT.Anio = @Anio
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 20-06-2023
-- Description:	CERRAR TOLVAS DE PREVIAJES
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Operaciones_Previajes_Tolvas_CerrarOperacion]
@idPreviajeTolvas INT,
@Anio INT,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0=Estado Modificado.'

BEGIN TRAN
BEGIN TRY
	IF ((SELECT Estado FROM ReportesApp_Operaciones_Previajes_Tolvas_Cabecera WHERE idPreviajeTolvas = @idPreviajeTolvas AND Anio = @Anio) = 'PROGRAMADO') BEGIN
		UPDATE ReportesApp_Operaciones_Previajes_Tolvas_Cabecera
		SET Estado = 'ATENDIDO',
		UltimoUsuario = @Usuario,
		UltimaModificacion = GETDATE()
		WHERE (idPreviajeTolvas = @idPreviajeTolvas AND Anio = @Anio)
	END
	ELSE BEGIN
		IF ((SELECT Estado FROM ReportesApp_Operaciones_Previajes_Tolvas_Cabecera WHERE idPreviajeTolvas = @idPreviajeTolvas AND Anio = @Anio) = 'ATENDIDO') BEGIN
			UPDATE ReportesApp_Operaciones_Previajes_Tolvas_Cabecera
			SET Estado = 'PROGRAMADO',
			UltimoUsuario = @Usuario,
			UltimaModificacion = GETDATE()
			WHERE (idPreviajeTolvas = @idPreviajeTolvas AND Anio = @Anio)
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

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 21-06-2023
-- Description:	MODIFICAR TOLVAS DE PREVIAJES
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Operaciones_Previajes_Tolvas_Modificar]
@idPreviajeTolvas INT,
@Anio INT,
@FechaTraslado DATE,
@xmlPlacaConductor VARCHAR(MAX),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @idoc INT
DECLARE @TEMP_EPPS TABLE(
		idPlaca INT,
		Placa VARCHAR(20),
		idCarreta INT,
		Carreta VARCHAR(20),
		idConductor INT,
		Conductor VARCHAR(250))

SET @Exito = '0=Operación Modificada.'

IF(@xmlPlacaConductor IS NOT NULL) BEGIN
	EXEC sp_xml_preparedocument @idoc OUTPUT, @xmlPlacaConductor
	INSERT INTO @TEMP_EPPS(idPlaca, Placa, idCarreta, Carreta, idConductor, Conductor)
	SELECT * FROM OPENXML(@idoc,'/r/d',1)
	WITH (idPlaca INT, Placa VARCHAR(20), idCarreta INT, Carreta VARCHAR(20), idConductor INT, Conductor VARCHAR(250));
	EXEC sp_xml_removedocument @idoc;
END

BEGIN TRAN
BEGIN TRY
	UPDATE ReportesApp_Operaciones_Previajes_Tolvas_Cabecera
	SET FechaTraslado = @FechaTraslado,
	UltimoUsuario = @Usuario,
	UltimaModificacion = GETDATE()
	WHERE (idPreviajeTolvas = @idPreviajeTolvas AND Anio = @Anio)
	
	DELETE FROM ReportesApp_Operaciones_Previajes_Tolvas_Detalle
	WHERE (idPreviajeTolvas = @idPreviajeTolvas AND Anio = @Anio)
	
	INSERT INTO ReportesApp_Operaciones_Previajes_Tolvas_Detalle(idPreviajeTolvas, Anio, idPlaca, Placa, idCarreta, Carreta, idConductor, Conductor)
	SELECT @idPreviajeTolvas, @Anio, E.idPlaca, E.Placa, E.idCarreta, E.Carreta, E.idConductor, E.Conductor FROM @TEMP_EPPS E 
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