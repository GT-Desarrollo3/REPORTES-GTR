
-- CREAR TABLA ReportesApp_Mantenimiento_ControlOperativos_Solicitudes

-- CREAR TABLA ReportesApp_Mantenimiento_ControlOperativos_TicketsConductor

----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 20-01-2025
-- Description:	GENERAR CONTROL OPERATIVO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlOperativos_GenerarSolicitud]
@PSolicitud INT,
@FechaRequerida DATETIME,
@Direccion VARCHAR(300),
@Detalle VARCHAR(300),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

BEGIN TRAN
BEGIN TRY
	IF (@FechaRequerida < GETDATE()) BEGIN
		SET @Exito = '-1 = No puede generar una solicitud de operativos en una fecha menor a la actual.'
		ROLLBACK
		GOTO Terminar
	END
	ELSE BEGIN
		SET @correlativo = (SELECT MAX(idControlOperativo) FROM ReportesApp_Mantenimiento_ControlOperativos_Solicitudes WHERE Anio = YEAR(GETDATE()))
		SET @correlativo = ISNULL(@correlativo,0) + 1

		INSERT INTO ReportesApp_Mantenimiento_ControlOperativos_Solicitudes(idControlOperativo, Anio, CodOperativo, PSolicitud, FechaRequerida, Direccion, Detalle,
		Estado, UsuarioCrea, FechaCrea)
		VALUES(@correlativo, YEAR(GETDATE()), SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo)),6)),
		@PSolicitud, @FechaRequerida, @Direccion, @Detalle, 'PENDIENTE', @Usuario, GETDATE())

		SET @Exito = SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo)),6))
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

----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 20-01-2025
-- Description:	LISTAR CONTROL OPERATIVO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlOperativos_Listar]
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@Ticket VARCHAR(40),
@Conductor VARCHAR(250),
@Area VARCHAR(100),
@Estado VARCHAR(50)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@Estado = 'TODOS') BEGIN
		IF (@Area = 'TODAS') BEGIN
			SELECT CO.CodOperativo AS 'CODIGO', CO.Estado AS 'ESTADO', RTRIM(P.NombreCompleto) AS 'NOMBRE_USUARIO', RTRIM(A1.[description]) AS 'AREA',
			CO.FechaRequerida AS 'FECHA_REQUERIDA', CO.Direccion AS 'DIRECCION', CO.Detalle AS 'DETALLE', RTRIM(P2.NombreCompleto)
			AS 'CONDUCTOR_ASIGNADO', V.NumeroPlaca AS 'UNIDAD_ASIGNADA', CO.UsuarioCrea, CO.FechaCrea
			FROM ReportesApp_Mantenimiento_ControlOperativos_Solicitudes CO
			LEFT JOIN PersonaMast P ON P.Persona = CO.PSolicitud
			LEFT JOIN EmpleadoMast E ON P.Persona = E.Empleado AND E.Estado = 'A' AND E.CompaniaSocio = '10000000'
			LEFT JOIN departmentmst A1 ON A1.Department = E.DeptoOrganizacion
			LEFT JOIN PersonaMast P2 ON P2.Persona = CO.PConductor
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = CO.Unidad
			WHERE (CO.FechaRequerida BETWEEN @FINICIO AND @FFIN) AND (@Conductor IS NULL OR LTRIM(RTRIM(ISNULL(P2.NombreCompleto,''))) LIKE '%' + @Conductor + '%')
			AND (@Ticket IS NULL OR CO.CodOperativo LIKE '%' + @Ticket + '%')
			ORDER BY CO.CodOperativo DESC
		END
		ELSE BEGIN
			SELECT CO.CodOperativo AS 'CODIGO', CO.Estado AS 'ESTADO', RTRIM(P.NombreCompleto) AS 'NOMBRE_USUARIO', RTRIM(A1.[description]) AS 'AREA',
			CO.FechaRequerida AS 'FECHA_REQUERIDA', CO.Direccion AS 'DIRECCION', CO.Detalle AS 'DETALLE', RTRIM(P2.NombreCompleto)
			AS 'CONDUCTOR_ASIGNADO', V.NumeroPlaca AS 'UNIDAD_ASIGNADA', CO.UsuarioCrea, CO.FechaCrea
			FROM ReportesApp_Mantenimiento_ControlOperativos_Solicitudes CO
			LEFT JOIN PersonaMast P ON P.Persona = CO.PSolicitud
			LEFT JOIN EmpleadoMast E ON P.Persona = E.Empleado AND E.Estado = 'A' AND E.CompaniaSocio = '10000000'
			LEFT JOIN departmentmst A1 ON A1.Department = E.DeptoOrganizacion
			LEFT JOIN PersonaMast P2 ON P2.Persona = CO.PConductor
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = CO.Unidad
			WHERE (CO.FechaRequerida BETWEEN @FINICIO AND @FFIN) AND (@Conductor IS NULL OR LTRIM(RTRIM(ISNULL(P2.NombreCompleto,''))) LIKE '%' + @Conductor + '%')
			AND (@Ticket IS NULL OR CO.CodOperativo LIKE '%' + @Ticket + '%') AND (A1.[description] = @Area)
			ORDER BY CO.CodOperativo DESC
		END
	END
	ELSE BEGIN
		IF (@Area = 'TODAS') BEGIN
			SELECT CO.CodOperativo AS 'CODIGO', CO.Estado AS 'ESTADO', RTRIM(P.NombreCompleto) AS 'NOMBRE_USUARIO', RTRIM(A1.[description]) AS 'AREA',
			CO.FechaRequerida AS 'FECHA_REQUERIDA', CO.Direccion AS 'DIRECCION', CO.Detalle AS 'DETALLE', RTRIM(P2.NombreCompleto)
			AS 'CONDUCTOR_ASIGNADO', V.NumeroPlaca AS 'UNIDAD_ASIGNADA', CO.UsuarioCrea, CO.FechaCrea
			FROM ReportesApp_Mantenimiento_ControlOperativos_Solicitudes CO
			LEFT JOIN PersonaMast P ON P.Persona = CO.PSolicitud
			LEFT JOIN EmpleadoMast E ON P.Persona = E.Empleado AND E.Estado = 'A' AND E.CompaniaSocio = '10000000'
			LEFT JOIN departmentmst A1 ON A1.Department = E.DeptoOrganizacion
			LEFT JOIN PersonaMast P2 ON P2.Persona = CO.PConductor
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = CO.Unidad
			WHERE (CO.FechaRequerida BETWEEN @FINICIO AND @FFIN) AND (@Conductor IS NULL OR LTRIM(RTRIM(ISNULL(P2.NombreCompleto,''))) LIKE '%' + @Conductor + '%')
			AND (@Ticket IS NULL OR CO.CodOperativo LIKE '%' + @Ticket + '%') AND (CO.Estado = @Estado)
			ORDER BY CO.CodOperativo DESC
		END
		ELSE BEGIN
			SELECT CO.CodOperativo AS 'CODIGO', CO.Estado AS 'ESTADO', RTRIM(P.NombreCompleto) AS 'NOMBRE_USUARIO', RTRIM(A1.[description]) AS 'AREA',
			CO.FechaRequerida AS 'FECHA_REQUERIDA', CO.Direccion AS 'DIRECCION', CO.Detalle AS 'DETALLE', RTRIM(P2.NombreCompleto)
			AS 'CONDUCTOR_ASIGNADO', V.NumeroPlaca AS 'UNIDAD_ASIGNADA', CO.UsuarioCrea, CO.FechaCrea
			FROM ReportesApp_Mantenimiento_ControlOperativos_Solicitudes CO
			LEFT JOIN PersonaMast P ON P.Persona = CO.PSolicitud
			LEFT JOIN EmpleadoMast E ON P.Persona = E.Empleado AND E.Estado = 'A' AND E.CompaniaSocio = '10000000'
			LEFT JOIN departmentmst A1 ON A1.Department = E.DeptoOrganizacion
			LEFT JOIN PersonaMast P2 ON P2.Persona = CO.PConductor
			LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = CO.Unidad
			WHERE (CO.FechaRequerida BETWEEN @FINICIO AND @FFIN) AND (@Conductor IS NULL OR LTRIM(RTRIM(ISNULL(P2.NombreCompleto,''))) LIKE '%' + @Conductor + '%')
			AND (@Ticket IS NULL OR CO.CodOperativo LIKE '%' + @Ticket + '%') AND (CO.Estado = @Estado) AND (A1.[description] = @Area)
			ORDER BY CO.CodOperativo DESC
		END
	END
END

----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 20-01-2025
-- Description:	FILTRAR CONTROL OPERATIVO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlOperativos_Filtrar]
@CodOperativo VARCHAR(50)
AS
BEGIN
	SELECT CO.CodOperativo AS 'CODIGO', CO.Estado AS 'ESTADO', RTRIM(P.NombreCompleto) AS 'NOMBRE_USUARIO', RTRIM(A1.[description]) AS 'AREA',
	CONVERT(VARCHAR,CO.FechaRequerida,103)+' '+CONVERT(VARCHAR,CO.FechaRequerida,24) AS 'FECHA_REQUERIDA', CO.Direccion AS 'DIRECCION',
	CO.Detalle AS 'DETALLE', RTRIM(P2.NombreCompleto) AS 'CONDUCTOR_ASIGNADO', V.NumeroPlaca AS 'UNIDAD_ASIGNADA', CO.UsuarioCrea, CO.FechaCrea
	FROM ReportesApp_Mantenimiento_ControlOperativos_Solicitudes CO
	LEFT JOIN PersonaMast P ON P.Persona = CO.PSolicitud
	LEFT JOIN EmpleadoMast E ON P.Persona = E.Empleado AND E.Estado = 'A'
	LEFT JOIN departmentmst A1 ON A1.Department = E.DeptoOrganizacion
	LEFT JOIN PersonaMast P2 ON P2.Persona = CO.PConductor
	LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = CO.Unidad
	WHERE (CO.CodOperativo = @CodOperativo)
END

----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 22-01-2025
-- Description:	ELIMINAR CONTROL OPERATIVOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlOperativos_EliminarSolicitudes]
@Opcion INT,
@CodOperativo VARCHAR(50)
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- ANULAR SOLICITUD 
		UPDATE ReportesApp_Mantenimiento_ControlOperativos_Solicitudes
		SET Estado = 'ANULADO'
		WHERE CodOperativo = @CodOperativo

		SET @Exito = '0 = Solcitud Anulada Correctamente.'
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR SOLICITUD 
		DELETE FROM ReportesApp_Mantenimiento_ControlOperativos_Solicitudes
		WHERE CodOperativo = @CodOperativo

		SET @Exito = '0 = Solcitud Eliminada Correctamente.'
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

----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES HORNA
-- Create date: 23/01/2024
-- Description:	ASIGNAR CONDUCTOR A OPERATIVO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlOperativos_AsignarConductor]
@CodOperativo VARCHAR(50),
@PConductor INT,
@Unidad INT
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF ((SELECT Estado FROM ReportesApp_Mantenimiento_ControlOperativos_Solicitudes WHERE CodOperativo = @CodOperativo) = 'ANULADO') BEGIN
		SET @exito = '-1 = No puede asignarle un conductor a la solicitud ' + @CodOperativo + ' porque está anulada.'
		GOTO Terminar
	END
	ELSE BEGIN
		UPDATE ReportesApp_Mantenimiento_ControlOperativos_Solicitudes
		SET PConductor = @PConductor, Unidad = @Unidad, Estado = 'ASIGNADO'
		WHERE CodOperativo = @CodOperativo

		SET @Exito = '0 = Solcitud Asignada Correctamente.'
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

--------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 11-02-2025
-- Description:	CREAR TICKET
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlOperativos_CrearTicket]
@xmlTicket VARCHAR(MAX),
@PConductor INT,
@Unidad INT,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT
DECLARE @idoc INT
DECLARE @TEMP_TICKET TABLE(Codigo VARCHAR(350))

BEGIN TRAN
BEGIN TRY
	IF (@xmlTicket IS NOT NULL) BEGIN
		EXEC sp_xml_preparedocument @idoc OUTPUT, @xmlTicket
		INSERT INTO @TEMP_TICKET(Codigo)
		SELECT * FROM OPENXML(@idoc,N'/r/items')
		WITH (CODIGO VARCHAR(350));
		EXEC sp_xml_removedocument @idoc;
	END

	SET @correlativo = (SELECT MAX(idTicketC) FROM ReportesApp_Mantenimiento_ControlOperativos_TicketsConductor)
	SET @correlativo = ISNULL(@correlativo,0) + 1

	DECLARE @CodOperativos VARCHAR(350) = (SELECT LTRIM(RTRIM(STUFF((SELECT ', ' + CONVERT(VARCHAR,Codigo) FROM @TEMP_TICKET ORDER BY Codigo ASC
										   FOR XML PATH ('')),1,1,''))))

	INSERT INTO ReportesApp_Mantenimiento_ControlOperativos_TicketsConductor(idTicketC, CodOperativos, PConductor, Unidad, KMSalida, KMIngreso, UsuarioCrea, FechaCrea)
	VALUES(@correlativo, @CodOperativos, @PConductor, @Unidad, 0.00, 0.00, @Usuario, GETDATE())

	SET @Exito = '0 = Ticket creado correctamente.'
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

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 11-02-2025
-- Description:	LISTAR TICKETS CONDUCTOR
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlOperativos_ListarTickets]
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@Ticket VARCHAR(40),
@Conductor VARCHAR(250)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	SELECT TC.idTicketC AS 'NRO', TC.CodOperativos AS 'SOLICITUDES', RTRIM(P2.NombreCompleto) AS 'CONDUCTOR', V.NumeroPlaca AS 'UNIDAD',
	TC.KMSalida AS 'KM_SALIDA', TC.KMIngreso AS 'KM_INGRESO', (TC.KMIngreso - TC.KMSalida) AS 'RECORRIDO', TC.UsuarioCrea AS 'USUARIO_CREA',
	TC.FechaCrea AS 'FECHA_CREA'
	FROM ReportesApp_Mantenimiento_ControlOperativos_TicketsConductor TC
	LEFT JOIN PersonaMast P2 ON P2.Persona = TC.PConductor
	LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = TC.Unidad
	WHERE (TC.FechaCrea BETWEEN @FINICIO AND @FFIN) AND (@Conductor IS NULL OR LTRIM(RTRIM(ISNULL(P2.NombreCompleto,''))) LIKE '%' + @Conductor + '%')
	AND (@Ticket IS NULL OR TC.CodOperativos LIKE '%' + @Ticket + '%')
	ORDER BY TC.idTicketC DESC
END

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES HORNA
-- Create date: 11/02/2025
-- Description:	INGRESAR KMS A TICKETS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlOperativos_IngresarKMs]
@idTicketC INT,
@KMSalida DECIMAL(10,2),
@KMIngreso DECIMAL(10,2),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF ((@KMSalida > @KMIngreso) AND (@KMIngreso != 0.00)) BEGIN
		SET @exito = '-1 = El kilometraje de salida no puede ser mayor al kilometraje de ingreso.'
		GOTO Terminar
	END
	ELSE BEGIN
		UPDATE ReportesApp_Mantenimiento_ControlOperativos_TicketsConductor
		SET KMSalida = @KMSalida, KMIngreso = @KMIngreso, UsuarioCrea = @Usuario, FechaCrea = GETDATE()
		WHERE idTicketC = @idTicketC

		SET @Exito = '0 = Kilometrajes registrados correctamente.'
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

----------------------------------------------------------------------------------

SELECT * FROM ReportesApp_Mantenimiento_ControlOperativos_Solicitudes
SELECT * FROM ReportesApp_Mantenimiento_ControlOperativos_TicketsConductor

UPDATE ReportesApp_Mantenimiento_ControlOperativos_Solicitudes
SET PConductor = null, Unidad = null, Estado = 'PENDIENTE'

delete FROM ReportesApp_Mantenimiento_ControlOperativos_TicketsConductor