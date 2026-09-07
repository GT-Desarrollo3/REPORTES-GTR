
-- CREAR TABLA ReportesApp_Mantenimiento_AsignacionOT_Mecanicos Y LLENARLA

-- CREAR TABLA ReportesApp_Mantenimiento_AsignacionOT_Registro

-- CREAR TABLA ReportesApp_Mantenimiento_AsignacionOT_Historial

-- CREAR TABLA ReportesApp_Mantenimiento_AsignacionOT_Compensaciones

-- CREAR TABLA ReportesApp_Mantenimiento_AsignacionOT_Requerimientos

-------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 18-09-2024
-- Description:	LISTAR REQUERIMIENTOS APROBADOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_AsignacionOT_ListarRequerimientos]
@NroReq VARCHAR(30)
AS
BEGIN 
	SELECT TOP(25) LTRIM(RTRIM(R.RequisicionNumero)) AS 'REQUERIMIENTO', LTRIM(RTRIM(R.Comentarios)) AS 'DESCRIPCION',
	LTRIM(RTRIM(R.DefaultPrime)) AS 'DefaultPrime', LTRIM(RTRIM(A.LocalName)) AS 'CENTRO_COSTO', LTRIM(RTRIM(R.DefaultAfe)) AS 'DefaultAfe',
	LTRIM(RTRIM(F.localname)) AS 'PROYECTO'
	FROM WH_Requisiciones R
	LEFT JOIN AC_CostCenterMst A ON (R.DefaultPrime = A.CostCenter)
	LEFT JOIN afemst F ON (F.afe = R.DefaultAfe)
	WHERE (R.CompaniaSocio = '10000000') AND (R.Estado = 'AP')
	AND (@NroReq IS NULL OR LTRIM(RTRIM(R.RequisicionNumero)) LIKE '%' + @NroReq + '%')
END

-------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 18-03-2024
-- Description:	LISTAR OT PROGRAMADAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_AsignacionOT_ListarOTProgramadas]
@Placa VARCHAR(20),
@Descripcion VARCHAR(250),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	SELECT X.* FROM
	(SELECT NumeroOrden AS 'OT', CONVERT(VARCHAR,FechaProgramada,103) AS 'FECHA_PROGRAMADA', MaquinaCodigo AS 'PLACA', Descripcion AS 'DESCRIPCION'
	FROM ME_OrdenTrabajo
	WHERE (CompaniaSocio = '10000000') AND (Estado = 'PG') AND
	(@Placa IS NULL OR LTRIM(RTRIM(REPLACE(REPLACE(MaquinaCodigo,'.',''),'-',''))) LIKE '%' + REPLACE(REPLACE(@Placa,'.',''),'-','') + '%')
	AND (FechaProgramada BETWEEN @FINICIO AND @FFIN) AND (@Descripcion IS NULL OR Descripcion LIKE '%' + @Descripcion + '%')
	UNION
	SELECT CodigoReq AS 'OT', CONVERT(VARCHAR,FechaProgramada,103) AS 'FECHA_PROGRAMADA', '' AS 'PLACA', Descripcion AS 'DESCRIPCION'
	FROM ReportesApp_Mantenimiento_AsignacionOT_Requerimientos
	WHERE (FechaProgramada BETWEEN @FINICIO AND @FFIN) AND (@Descripcion IS NULL OR Descripcion LIKE '%' + @Descripcion + '%')) X
	ORDER BY X.FECHA_PROGRAMADA DESC, X.OT DESC
END

-------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 19-03-2024
-- Description:	GENERAR GESTIÓN DE ACTIVIDADES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_AsignacionOT_InsertarMecanico]
@Opcion INT,
@Persona INT,
@Codigo VARCHAR(30),
@NombreCompleto VARCHAR(350),
@Turno VARCHAR(30),
@Compania VARCHAR(250)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @Correlativo INT

SET @Exito = '0 = Mecánico Registrado Correctamente.'

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- INSERTAR MECÁNICO
		IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_AsignacionOT_Mecanicos WHERE Persona = @Persona)) BEGIN
			SET @Exito = '-1 = Este mecánico ya ha sido registrado en la lista.'
			ROLLBACK
			GOTO Terminar
		END

		IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_AsignacionOT_Mecanicos WHERE Codigo = @Codigo)) BEGIN
			SET @Exito = '-1 = Este código ya ha sido asignado al mecánico ' +
						 (SELECT NombreCompleto FROM ReportesApp_Mantenimiento_AsignacionOT_Mecanicos WHERE Codigo = @Codigo)
			ROLLBACK
			GOTO Terminar
		END
		
		INSERT INTO ReportesApp_Mantenimiento_AsignacionOT_Mecanicos (Persona, Codigo, NombreCompleto, Estado, Turno, Compania)
		VALUES (@Persona, @Codigo, @NombreCompleto, 'DISPONIBLE', @Turno, @Compania)
	END
	
	IF (@Opcion = 2) BEGIN		-- ELIMINAR MECÁNICO
		DELETE FROM ReportesApp_Mantenimiento_AsignacionOT_Mecanicos
		WHERE Persona = @Persona

		DELETE FROM ReportesApp_Mantenimiento_AsignacionOT_Registro
		WHERE Persona = @Persona
	END

	IF (@Opcion = 3) BEGIN		-- MODIFICAR TURNO
		UPDATE ReportesApp_Mantenimiento_AsignacionOT_Mecanicos
		SET Turno = @Turno
		WHERE Persona = @Persona

		SET @Exito = '0 = Turno asignado.'
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

-------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 19-03-2024
-- Description:	LISTAR MECÁNICOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_AsignacionOT_ListarMecanico]
@Opcion INT,
@NumeroOrden VARCHAR(20)
AS
BEGIN
	IF (@Opcion = 1) BEGIN		-- LISTAR MECÁNICOS
		SELECT MC.Persona, MC.Codigo AS 'CODIGO', MC.Compania AS 'COMPAÑÍA', MC.NombreCompleto AS 'NOMBRE',
		CASE WHEN MC.Turno = 'NINGUNO' THEN ' ' ELSE MC.Turno END AS 'TURNO', MC.Estado AS 'ESTADO',
		LTRIM(RTRIM(R.NumeroOrden)) AS 'OT', ME.MaquinaCodigo AS 'PLACA'
		FROM ReportesApp_Mantenimiento_AsignacionOT_Mecanicos MC
		LEFT JOIN ReportesApp_Mantenimiento_AsignacionOT_Registro R ON R.Persona = MC.Persona AND R.Estado = 0
		LEFT JOIN ME_OrdenTrabajo ME ON LTRIM(RTRIM(ME.NumeroOrden)) = LTRIM(RTRIM(R.NumeroOrden))
	END
	
	IF (@Opcion = 2) BEGIN		-- FILTRAR MECÁNICO
		SELECT TOP(1) MC.Persona, MC.Codigo AS 'CODIGO', MC.NombreCompleto AS 'NOMBRE', MC.Turno AS 'TURNO'
		FROM ReportesApp_Mantenimiento_AsignacionOT_Mecanicos MC
		WHERE MC.Codigo LIKE @NumeroOrden + '%'
	END

	IF (@Opcion = 3) BEGIN		-- LISTAR ASIGNACIONES
		SELECT R.Persona, M.Codigo AS 'CODIGO', M.NombreCompleto AS 'NOMBRE', R.FechaInicio AS 'INICIO', R.FechaFin AS 'FIN',
		R.Estado, R.MotivoPausa AS 'MOTIVO'
		FROM ReportesApp_Mantenimiento_AsignacionOT_Registro R
		LEFT JOIN ReportesApp_Mantenimiento_AsignacionOT_Mecanicos M ON M.Persona = R.Persona
		WHERE LTRIM(RTRIM(R.NumeroOrden)) = @NumeroOrden
		ORDER BY R.FechaInicio DESC
	END

	IF (@Opcion = 4) BEGIN		-- LISTAR COMPAÑIAS
		SELECT CompaniaCodigo, DescripcionCorta FROM CompaniaMast
		WHERE Estado = 'A'
	END
END

------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 20-03-2024
-- Description:	LISTAR HISTORIAL MECÁNICOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_AsignacionOT_ListarHistorial]
@Nombre VARCHAR(250),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@HorasExtra INT,
@Estado VARCHAR(20)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@HorasExtra = 0) BEGIN
		SELECT H.idHistorial, LTRIM(RTRIM(H.NumeroOrden)) AS 'OT', ME.MaquinaCodigo AS 'PLACA', LTRIM(RTRIM(ISNULL(ME.Descripcion,R.Descripcion))) AS 'DESCRIPCION',
		M.Codigo AS 'CÓDIGO', LTRIM(RTRIM(P.NombreCompleto)) AS 'NOMBRE', LTRIM(RTRIM(C1.Descripcion)) AS 'CARGO', H.Turno AS 'TURNO', H.FechaInicio AS 'INICIO',
		H.FechaFin AS 'FIN', H.Motivo AS 'MOTIVO', CASE WHEN CONVERT(TIME,H.FechaInicio) < '13:00:00' AND CONVERT(TIME,H.FechaFin) > '14:30:00'
		THEN CONVERT(VARCHAR,CONVERT(TIME,DATEADD(MINUTE,-90,H.FechaFin - H.FechaInicio)),108)
		ELSE CONVERT(VARCHAR,CONVERT(TIME,H.FechaFin - H.FechaInicio),108) END AS 'TIEMPO', H.Estado AS 'ESTADO', 0 AS 'TotalHoras'
		FROM ReportesApp_Mantenimiento_AsignacionOT_Historial H
		LEFT JOIN ME_OrdenTrabajo ME ON LTRIM(RTRIM(ME.NumeroOrden)) = LTRIM(RTRIM(H.NumeroOrden))
		LEFT JOIN ReportesApp_Mantenimiento_AsignacionOT_Requerimientos R ON R.CodigoReq = LTRIM(RTRIM(H.NumeroOrden))
		LEFT JOIN PersonaMast P ON P.Persona = H.Persona
		LEFT JOIN EmpleadoMast E ON P.Persona = E.Empleado
		LEFT JOIN departmentmst A1 ON A1.Department = E.DeptoOrganizacion
		LEFT JOIN hr_puestoempresa C1 ON C1.CodigoPuesto = E.CodigoCargo
		LEFT JOIN ReportesApp_Mantenimiento_AsignacionOT_Mecanicos M ON M.Persona = P.Persona
		WHERE (C1.Estado = 'A') AND (E.Estado = 'A') AND (@Nombre IS NULL OR LTRIM(RTRIM(P.NombreCompleto)) LIKE '%' + @Nombre + '%')
		AND (H.FechaInicio BETWEEN @FINICIO AND @FFIN) 
		ORDER BY H.FechaFin DESC
	END

	IF (@HorasExtra = 1) BEGIN
		DECLARE @TotalHoras INT
		
		IF (@Estado = 'TODOS') BEGIN
			SET @TotalHoras = (SELECT SUM(DATEDIFF(SS, '00:00:00.000', CONVERT(TIME,X.TIEMPO)))
			FROM (SELECT H.idHistorial, LTRIM(RTRIM(H.NumeroOrden)) AS 'OT', ME.MaquinaCodigo AS 'PLACA', LTRIM(RTRIM(ISNULL(ME.Descripcion,R.Descripcion))) AS 'DESCRIPCION',
			M.Codigo AS 'CÓDIGO', LTRIM(RTRIM(P.NombreCompleto)) AS 'NOMBRE', LTRIM(RTRIM(C1.Descripcion)) AS 'CARGO', H.Turno AS 'TURNO', H.FechaInicio AS 'INICIO',
			H.FechaFin AS 'FIN', H.Motivo AS 'MOTIVO', CASE WHEN CONVERT(TIME,H.FechaInicio) < '13:00:00' AND CONVERT(TIME,H.FechaFin) > '14:30:00'
			THEN CONVERT(VARCHAR,CONVERT(TIME,DATEADD(MINUTE,-90,H.FechaFin - H.FechaInicio)),108)
			ELSE CONVERT(VARCHAR,CONVERT(TIME,H.FechaFin - H.FechaInicio),108) END AS 'TIEMPO', H.Estado AS 'ESTADO'
			FROM ReportesApp_Mantenimiento_AsignacionOT_Historial H
			LEFT JOIN ME_OrdenTrabajo ME ON LTRIM(RTRIM(ME.NumeroOrden)) = LTRIM(RTRIM(H.NumeroOrden))
			LEFT JOIN ReportesApp_Mantenimiento_AsignacionOT_Requerimientos R ON R.CodigoReq = LTRIM(RTRIM(H.NumeroOrden))
			LEFT JOIN PersonaMast P ON P.Persona = H.Persona
			LEFT JOIN EmpleadoMast E ON P.Persona = E.Empleado
			LEFT JOIN departmentmst A1 ON A1.Department = E.DeptoOrganizacion
			LEFT JOIN hr_puestoempresa C1 ON C1.CodigoPuesto = E.CodigoCargo
			LEFT JOIN ReportesApp_Mantenimiento_AsignacionOT_Mecanicos M ON M.Persona = P.Persona
			WHERE (C1.Estado = 'A') AND (E.Estado = 'A') AND (H.Motivo = 'TIEMPO EXTRA')
			AND (@Nombre IS NULL OR LTRIM(RTRIM(P.NombreCompleto)) LIKE '%' + @Nombre + '%') AND (H.FechaInicio BETWEEN @FINICIO AND @FFIN)) X)	
		
			SELECT H.idHistorial, LTRIM(RTRIM(H.NumeroOrden)) AS 'OT', ME.MaquinaCodigo AS 'PLACA', LTRIM(RTRIM(ISNULL(ME.Descripcion,R.Descripcion))) AS 'DESCRIPCION',
			M.Codigo AS 'CÓDIGO', LTRIM(RTRIM(P.NombreCompleto)) AS 'NOMBRE', LTRIM(RTRIM(C1.Descripcion)) AS 'CARGO', H.Turno AS 'TURNO', H.FechaInicio AS 'INICIO',
			H.FechaFin AS 'FIN', H.Motivo AS 'MOTIVO', CASE WHEN CONVERT(TIME,H.FechaInicio) < '13:00:00' AND CONVERT(TIME,H.FechaFin) > '14:30:00'
			THEN CONVERT(VARCHAR,CONVERT(TIME,DATEADD(MINUTE,-90,H.FechaFin - H.FechaInicio)),108)
			ELSE CONVERT(VARCHAR,CONVERT(TIME,H.FechaFin - H.FechaInicio),108) END AS 'TIEMPO', H.Estado AS 'ESTADO',
			RIGHT('0' + CAST(@TotalHoras / 3600 AS VARCHAR(2)),2) + ':' +
			RIGHT('0' + CAST(@TotalHoras % 3600 / 60 AS VARCHAR(2)),2) + ':' +
			RIGHT('0' + CAST(@TotalHoras % 60 AS VARCHAR(2)),2) as 'TotalHoras'
			FROM ReportesApp_Mantenimiento_AsignacionOT_Historial H
			LEFT JOIN ME_OrdenTrabajo ME ON LTRIM(RTRIM(ME.NumeroOrden)) = LTRIM(RTRIM(H.NumeroOrden))
			LEFT JOIN ReportesApp_Mantenimiento_AsignacionOT_Requerimientos R ON R.CodigoReq = LTRIM(RTRIM(H.NumeroOrden))
			LEFT JOIN PersonaMast P ON P.Persona = H.Persona
			LEFT JOIN EmpleadoMast E ON P.Persona = E.Empleado
			LEFT JOIN departmentmst A1 ON A1.Department = E.DeptoOrganizacion
			LEFT JOIN hr_puestoempresa C1 ON C1.CodigoPuesto = E.CodigoCargo
			LEFT JOIN ReportesApp_Mantenimiento_AsignacionOT_Mecanicos M ON M.Persona = P.Persona
			WHERE (C1.Estado = 'A') AND (E.Estado = 'A') AND (H.Motivo = 'TIEMPO EXTRA')
			AND (@Nombre IS NULL OR LTRIM(RTRIM(P.NombreCompleto)) LIKE '%' + @Nombre + '%') AND (H.FechaInicio BETWEEN @FINICIO AND @FFIN) 
			ORDER BY H.FechaFin DESC
		END
		ELSE BEGIN
			SET @TotalHoras = (SELECT SUM(DATEDIFF(SS, '00:00:00.000', CONVERT(TIME,X.TIEMPO)))
			FROM (SELECT H.idHistorial, LTRIM(RTRIM(H.NumeroOrden)) AS 'OT', ME.MaquinaCodigo AS 'PLACA', LTRIM(RTRIM(ISNULL(ME.Descripcion,R.Descripcion))) AS 'DESCRIPCION',
			M.Codigo AS 'CÓDIGO', LTRIM(RTRIM(P.NombreCompleto)) AS 'NOMBRE', LTRIM(RTRIM(C1.Descripcion)) AS 'CARGO', H.Turno AS 'TURNO', H.FechaInicio AS 'INICIO',
			H.FechaFin AS 'FIN', H.Motivo AS 'MOTIVO', CASE WHEN CONVERT(TIME,H.FechaInicio) < '13:00:00' AND CONVERT(TIME,H.FechaFin) > '14:30:00'
			THEN CONVERT(VARCHAR,CONVERT(TIME,DATEADD(MINUTE,-90,H.FechaFin - H.FechaInicio)),108)
			ELSE CONVERT(VARCHAR,CONVERT(TIME,H.FechaFin - H.FechaInicio),108) END AS 'TIEMPO', H.Estado AS 'ESTADO'
			FROM ReportesApp_Mantenimiento_AsignacionOT_Historial H
			LEFT JOIN ME_OrdenTrabajo ME ON LTRIM(RTRIM(ME.NumeroOrden)) = LTRIM(RTRIM(H.NumeroOrden))
			LEFT JOIN ReportesApp_Mantenimiento_AsignacionOT_Requerimientos R ON R.CodigoReq = LTRIM(RTRIM(H.NumeroOrden))
			LEFT JOIN PersonaMast P ON P.Persona = H.Persona
			LEFT JOIN EmpleadoMast E ON P.Persona = E.Empleado
			LEFT JOIN departmentmst A1 ON A1.Department = E.DeptoOrganizacion
			LEFT JOIN hr_puestoempresa C1 ON C1.CodigoPuesto = E.CodigoCargo
			LEFT JOIN ReportesApp_Mantenimiento_AsignacionOT_Mecanicos M ON M.Persona = P.Persona
			WHERE (C1.Estado = 'A') AND (E.Estado = 'A') AND (H.Motivo = 'TIEMPO EXTRA') AND (H.Estado = @Estado)
			AND (@Nombre IS NULL OR LTRIM(RTRIM(P.NombreCompleto)) LIKE '%' + @Nombre + '%') AND (H.FechaInicio BETWEEN @FINICIO AND @FFIN)) X)	
		
			SELECT H.idHistorial, LTRIM(RTRIM(H.NumeroOrden)) AS 'OT', ME.MaquinaCodigo AS 'PLACA', LTRIM(RTRIM(ISNULL(ME.Descripcion,R.Descripcion))) AS 'DESCRIPCION',
			M.Codigo AS 'CÓDIGO', LTRIM(RTRIM(P.NombreCompleto)) AS 'NOMBRE', LTRIM(RTRIM(C1.Descripcion)) AS 'CARGO', H.Turno AS 'TURNO', H.FechaInicio AS 'INICIO',
			H.FechaFin AS 'FIN', H.Motivo AS 'MOTIVO', CASE WHEN CONVERT(TIME,H.FechaInicio) < '13:00:00' AND CONVERT(TIME,H.FechaFin) > '14:30:00'
			THEN CONVERT(VARCHAR,CONVERT(TIME,DATEADD(MINUTE,-90,H.FechaFin - H.FechaInicio)),108)
			ELSE CONVERT(VARCHAR,CONVERT(TIME,H.FechaFin - H.FechaInicio),108) END AS 'TIEMPO', H.Estado AS 'ESTADO',
			RIGHT('0' + CAST(@TotalHoras / 3600 AS VARCHAR(2)),2) + ':' +
			RIGHT('0' + CAST(@TotalHoras % 3600 / 60 AS VARCHAR(2)),2) + ':' +
			RIGHT('0' + CAST(@TotalHoras % 60 AS VARCHAR(2)),2) as 'TotalHoras'
			FROM ReportesApp_Mantenimiento_AsignacionOT_Historial H
			LEFT JOIN ME_OrdenTrabajo ME ON LTRIM(RTRIM(ME.NumeroOrden)) = LTRIM(RTRIM(H.NumeroOrden))
			LEFT JOIN ReportesApp_Mantenimiento_AsignacionOT_Requerimientos R ON R.CodigoReq = LTRIM(RTRIM(H.NumeroOrden))
			LEFT JOIN PersonaMast P ON P.Persona = H.Persona
			LEFT JOIN EmpleadoMast E ON P.Persona = E.Empleado
			LEFT JOIN departmentmst A1 ON A1.Department = E.DeptoOrganizacion
			LEFT JOIN hr_puestoempresa C1 ON C1.CodigoPuesto = E.CodigoCargo
			LEFT JOIN ReportesApp_Mantenimiento_AsignacionOT_Mecanicos M ON M.Persona = P.Persona
			WHERE (C1.Estado = 'A') AND (E.Estado = 'A') AND (H.Motivo = 'TIEMPO EXTRA') AND (H.Estado = @Estado)
			AND (@Nombre IS NULL OR LTRIM(RTRIM(P.NombreCompleto)) LIKE '%' + @Nombre + '%') AND (H.FechaInicio BETWEEN @FINICIO AND @FFIN) 
			ORDER BY H.FechaFin DESC
		END
	END
END

------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 19-03-2024
-- Description:	ASIGNAR MECÁNICO A ORDEN TRABAJO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_AsignacionOT_AsignarMecanico]
@Opcion INT,
@Persona INT,
@NumeroOrden CHAR(10)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @Correlativo INT

SET @Exito = '0 = Mecánico Asignado Correctamente.'

BEGIN TRAN
BEGIN TRY
	DECLARE @Turno VARCHAR(30) = (SELECT Turno FROM ReportesApp_Mantenimiento_AsignacionOT_Mecanicos WHERE Persona = @Persona)

	IF (@Opcion = 1) BEGIN		-- ASIGNAR MECÁNICO
		IF (@Turno = 'NINGUNO') BEGIN
			SET @Exito = '-2 = Este mecánico no tiene un turno asignado y no se le puede asignar una OT.'
			ROLLBACK
			GOTO Terminar
		END

		IF (@Turno = 'MAÑANA' AND CONVERT(TIME,GETDATE()) > CONVERT(TIME,'18:00:00')) BEGIN
			SET @Exito = '-3 = No puede asignar una OT porque ya terminó su turno de trabajo.'
			ROLLBACK
			GOTO Terminar
		END

		IF (@Turno = 'TARDE' AND CONVERT(TIME,GETDATE()) > CONVERT(TIME,'21:00:00')) BEGIN
			SET @Exito = '-3 = No puede asignar una OT porque ya terminó su turno de trabajo.'
			ROLLBACK
			GOTO Terminar
		END

		IF (@Turno = 'NOCHE' AND CONVERT(TIME,GETDATE()) > CONVERT(TIME,'06:00:00')) BEGIN
			SET @Exito = '-3 = No puede asignar una OT porque ya terminó su turno de trabajo.'
			ROLLBACK
			GOTO Terminar
		END

		IF (@Turno = 'DOMINGO' AND CONVERT(TIME,GETDATE()) > CONVERT(TIME,'17:00:00')) BEGIN
			SET @Exito = '-3 = No puede asignar una OT porque ya terminó su turno de trabajo.'
			ROLLBACK
			GOTO Terminar
		END
		
		IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_AsignacionOT_Mecanicos WHERE Persona = @Persona AND Estado = 'OCUPADO')) BEGIN
			SET @Exito = '-1 = Esta mecánico ya se encuentra asignado a la OT - ' +
						 (SELECT TOP(1) LTRIM(RTRIM(NumeroOrden)) FROM ReportesApp_Mantenimiento_AsignacionOT_Registro WHERE Persona = @Persona ORDER BY FechaInicio DESC)
			ROLLBACK
			GOTO Terminar
		END
		
		INSERT INTO ReportesApp_Mantenimiento_AsignacionOT_Registro (NumeroOrden, Persona, FechaInicio, Estado)
		VALUES(@NumeroOrden, @Persona, GETDATE(), 0) 

		UPDATE ReportesApp_Mantenimiento_AsignacionOT_Mecanicos
		SET Estado = 'OCUPADO'
		WHERE Persona = @Persona
	END

	IF (@Opcion = 2) BEGIN		-- TERMINAR TRABAJO
		SET @Correlativo = (SELECT MAX(idHistorial) FROM ReportesApp_Mantenimiento_AsignacionOT_Historial)
		SET @Correlativo = ISNULL(@Correlativo,0) + 1

		INSERT INTO ReportesApp_Mantenimiento_AsignacionOT_Historial (idHistorial, NumeroOrden, Persona, FechaInicio, FechaFin, Turno, Motivo)
		SELECT @Correlativo, R.NumeroOrden, R.Persona, R.FechaInicio, GETDATE(), @Turno, 'TRABAJO TERMINADO'
		FROM ReportesApp_Mantenimiento_AsignacionOT_Registro R
		WHERE Persona = @Persona AND LTRIM(RTRIM(NumeroOrden)) = LTRIM(RTRIM(@NumeroOrden)) AND Estado = 0
		
		UPDATE ReportesApp_Mantenimiento_AsignacionOT_Registro
		SET FechaFin = GETDATE(), Estado = 1, MotivoPausa = 'TRABAJO TERMINADO'
		WHERE Persona = @Persona AND LTRIM(RTRIM(NumeroOrden)) = LTRIM(RTRIM(@NumeroOrden)) AND Estado = 0

		UPDATE ReportesApp_Mantenimiento_AsignacionOT_Mecanicos
		SET Estado = 'DISPONIBLE'
		WHERE Persona = @Persona
	END

	IF (@Opcion = 3) BEGIN		-- ELIMINAR MECÁNICO
		DELETE FROM ReportesApp_Mantenimiento_AsignacionOT_Registro
		WHERE Persona = @Persona AND LTRIM(RTRIM(NumeroOrden)) = LTRIM(RTRIM(@NumeroOrden)) AND Estado = 0

		UPDATE ReportesApp_Mantenimiento_AsignacionOT_Mecanicos
		SET Estado = 'DISPONIBLE'
		WHERE Persona = @Persona
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
-- Create date: 02-04-2024
-- Description:	PAUSAR OT DE MECANICO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_AsignacionOT_PausarOTMecanico]
@Persona INT,
@NumeroOrden CHAR(10),
@MotivoPausa VARCHAR(350)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @Correlativo INT

SET @Exito = '0 = El trabajo del mecánico en esta OT se ha detenido.'

BEGIN TRAN
BEGIN TRY
	SET @Correlativo = (SELECT MAX(idHistorial) FROM ReportesApp_Mantenimiento_AsignacionOT_Historial)
	SET @Correlativo = ISNULL(@Correlativo,0) + 1

	DECLARE @Turno VARCHAR(30) = (SELECT Turno FROM ReportesApp_Mantenimiento_AsignacionOT_Mecanicos WHERE Persona = @Persona)

	INSERT INTO ReportesApp_Mantenimiento_AsignacionOT_Historial (idHistorial, NumeroOrden, Persona, FechaInicio, FechaFin, Turno, Motivo)
	SELECT @Correlativo, R.NumeroOrden, R.Persona, R.FechaInicio, GETDATE(), @Turno, @MotivoPausa
	FROM ReportesApp_Mantenimiento_AsignacionOT_Registro R
	WHERE Persona = @Persona AND LTRIM(RTRIM(NumeroOrden)) = LTRIM(RTRIM(@NumeroOrden)) AND Estado = 0
		
	UPDATE ReportesApp_Mantenimiento_AsignacionOT_Registro
	SET FechaFin = GETDATE(), Estado = 1, MotivoPausa = @MotivoPausa
	WHERE Persona = @Persona AND LTRIM(RTRIM(NumeroOrden)) = LTRIM(RTRIM(@NumeroOrden)) AND Estado = 0

	UPDATE ReportesApp_Mantenimiento_AsignacionOT_Mecanicos
	SET Estado = 'DISPONIBLE'
	WHERE Persona = @Persona
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
-- Create date: 16-04-2024
-- Description:	ASIGNAR TIEMPO EXTRA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_AsignacionOT_AsignarTiempoExtra]
@Persona INT,
@NumeroOrden CHAR(10),
@FechaActual DATETIME
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @Correlativo INT

SET @Exito = '0 = Horas Extras Asignadas.'

BEGIN TRAN
BEGIN TRY
	SET @Correlativo = (SELECT MAX(idHistorial) FROM ReportesApp_Mantenimiento_AsignacionOT_Historial)
	SET @Correlativo = ISNULL(@Correlativo,0) + 1

	DECLARE @Turno VARCHAR(30) = (SELECT Turno FROM ReportesApp_Mantenimiento_AsignacionOT_Mecanicos WHERE Persona = @Persona)

	IF (@Turno = 'NINGUNO') BEGIN
		SET @Exito = '-2 = Este mecánico no tiene un turno asignado y no se le puede asignar una OT.'
		ROLLBACK
		GOTO Terminar
	END
		
	IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_AsignacionOT_Mecanicos WHERE Persona = @Persona AND Estado = 'OCUPADO')) BEGIN
		SET @Exito = '-1 = Esta mecánico ya se encuentra asignado a la OT - ' +
					 (SELECT TOP(1) LTRIM(RTRIM(NumeroOrden)) FROM ReportesApp_Mantenimiento_AsignacionOT_Registro WHERE Persona = @Persona ORDER BY FechaInicio DESC)
		ROLLBACK
		GOTO Terminar
	END

	IF (@Turno = 'MAÑANA' AND CONVERT(TIME,@FechaActual) < '18:00:00') BEGIN
		SET @Exito = '-3 = No puede asignar horas extras porque aún no se ha terminado el turno.'
		ROLLBACK
		GOTO Terminar
	END

	IF (@Turno = 'TARDE' AND CONVERT(TIME,@FechaActual) < '21:00:00') BEGIN
		SET @Exito = '-3 = No puede asignar horas extras porque aún no se ha terminado el turno.'
		ROLLBACK
		GOTO Terminar
	END

	IF (@Turno = 'NOCHE' AND CONVERT(TIME,@FechaActual) < '6:00:00') BEGIN
		SET @Exito = '-3 = No puede asignar horas extras porque aún no se ha terminado el turno.'
		ROLLBACK
		GOTO Terminar
	END

	IF (@Turno = 'DOMINGO' AND CONVERT(TIME,@FechaActual) < '17:00:00') BEGIN
		SET @Exito = '-3 = No puede asignar horas extras porque aún no se ha terminado el turno.'
		ROLLBACK
		GOTO Terminar
	END
		
	DECLARE @FechaM VARCHAR(100) = (SELECT CONVERT(VARCHAR,DAY(GETDATE()))+'/'+CONVERT(VARCHAR,MONTH(GETDATE()))+'/'+CONVERT(VARCHAR,YEAR(GETDATE()))+' 18:00:00')
	DECLARE @FechaT VARCHAR(100) = (SELECT CONVERT(VARCHAR,DAY(GETDATE()))+'/'+CONVERT(VARCHAR,MONTH(GETDATE()))+'/'+CONVERT(VARCHAR,YEAR(GETDATE()))+' 21:00:00')
	DECLARE @FechaN VARCHAR(100) = (SELECT CONVERT(VARCHAR,DAY(GETDATE()))+'/'+CONVERT(VARCHAR,MONTH(GETDATE()))+'/'+CONVERT(VARCHAR,YEAR(GETDATE()))+' 06:00:00')
	DECLARE @FechaD VARCHAR(100) = (SELECT CONVERT(VARCHAR,DAY(GETDATE()))+'/'+CONVERT(VARCHAR,MONTH(GETDATE()))+'/'+CONVERT(VARCHAR,YEAR(GETDATE()))+' 17:00:00')
	
	INSERT INTO ReportesApp_Mantenimiento_AsignacionOT_Registro (NumeroOrden, Persona, Estado, FechaInicio, FechaFin, MotivoPausa)
	SELECT @NumeroOrden, @Persona, 1,
	CASE WHEN @Turno = 'MAÑANA' THEN CONVERT(DATETIME,@FechaM)
		 WHEN @Turno = 'TARDE' THEN CONVERT(DATETIME,@FechaT)
		 WHEN @Turno = 'NOCHE' THEN CONVERT(DATETIME,@FechaN)
		 WHEN @Turno = 'DOMINGO' THEN CONVERT(DATETIME,@FechaD)
	END, GETDATE(), 'TIEMPO EXTRA'

	INSERT INTO ReportesApp_Mantenimiento_AsignacionOT_Historial (idHistorial, NumeroOrden, Persona, Turno, FechaInicio, FechaFin, Motivo, Estado)
	SELECT @Correlativo, @NumeroOrden, @Persona, @Turno,
	CASE WHEN @Turno = 'MAÑANA' THEN CONVERT(DATETIME,@FechaM)
		 WHEN @Turno = 'TARDE' THEN CONVERT(DATETIME,@FechaT)
		 WHEN @Turno = 'NOCHE' THEN CONVERT(DATETIME,@FechaN)
		 WHEN @Turno = 'DOMINGO' THEN CONVERT(DATETIME,@FechaD)
	END, GETDATE(), 'TIEMPO EXTRA', 'PENDIENTE'
END TRY

BEGIN CATCH
	SET @exito = RTRIM(LTRIM(STR(ERROR_NUMBER()))) + '=' + ERROR_MESSAGE()
	ROLLBACK
	GOTO Terminar
END CATCH

IF @@TRANCOUNT > 0
	--ROLLBACK
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
-- Create date: 17-04-2024
-- Description:	APROBAR HORAS EXTRA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_AsignacionOT_AprobarHorasExtra]
@idHistorial INT,
@Estado INT,
@NFechaIni VARCHAR(20),
@NHoraIni VARCHAR(20),
@NFechaFin VARCHAR(20),
@NHoraFin VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @FechaIni VARCHAR(100)
DECLARE @FechaFin VARCHAR(100)

SET @Exito = '0 = Estado Actualizado.'

BEGIN TRAN
BEGIN TRY
	IF (@Estado = 1) BEGIN
		SET @FechaIni = @NFechaIni+' '+@NHoraIni
		SET @FechaFin = @NFechaFin+' '+@NHoraFin
	
		UPDATE ReportesApp_Mantenimiento_AsignacionOT_Historial
		SET FechaInicio = CONVERT(DATETIME,@FechaIni), FechaFin = CONVERT(DATETIME,@FechaFin), Estado = 'APROBADO'
		WHERE idHistorial = @idHistorial
	END
	
	IF (@Estado = 0) BEGIN
		UPDATE ReportesApp_Mantenimiento_AsignacionOT_Historial
		SET Estado = 'RECHAZADO'
		WHERE idHistorial = @idHistorial
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
-- Create date: 20-04-2024
-- Description:	ASIGNAR HORAS EXTRA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_AsignacionOT_AsignarHorasExtra]
@Persona INT,
@NumeroOrden VARCHAR(10),
@Turno VARCHAR(30),
@NFechaIni VARCHAR(20),
@NHoraIni VARCHAR(20),
@NFechaFin VARCHAR(20),
@NHoraFin VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @FechaIni VARCHAR(100)
DECLARE @FechaFin VARCHAR(100)
DECLARE @Correlativo INT

SET @Exito = '0 = Horas Extras Asignadas.'

BEGIN TRAN
BEGIN TRY
	SET @Correlativo = (SELECT MAX(idHistorial) FROM ReportesApp_Mantenimiento_AsignacionOT_Historial)
	SET @Correlativo = ISNULL(@Correlativo,0) + 1

	SET @FechaIni = @NFechaIni+' '+@NHoraIni
	SET @FechaFin = @NFechaFin+' '+@NHoraFin

	IF (CONVERT(DATETIME,@FechaIni) > CONVERT(DATETIME,@FechaFin)) BEGIN
		SET @Exito = '-1 = La fecha de inicio no puede ser mayor a la fecha fin.'
		ROLLBACK
		GOTO Terminar
	END
	ELSE BEGIN
		INSERT INTO ReportesApp_Mantenimiento_AsignacionOT_Historial (idHistorial,NumeroOrden,Persona,Turno,FechaInicio,FechaFin,Motivo,Estado)
		SELECT @Correlativo, @NumeroOrden, @Persona, @Turno, CONVERT(DATETIME,@FechaIni), CONVERT(DATETIME,@FechaFin), 'TIEMPO EXTRA', 'APROBADO'
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
--------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 21-06-2024
-- Description:	LISTAR HORAS EXTRA MECANICO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_AsignacionOT_ListarHEMecanico]
@Nombre VARCHAR(250)
AS
BEGIN
	DECLARE @TEMP_HE TABLE(PERSONA INT, CÓDIGO VARCHAR(50), NOMBRE VARCHAR(300), CARGO VARCHAR(200), SEGUNDOS_EXTRA INT, SEGUNDOS_COMP INT)
	DECLARE @TEMP_HC TABLE(PERSONA INT, CÓDIGO VARCHAR(50), NOMBRE VARCHAR(300), CARGO VARCHAR(200), SEGUNDOS_COMP INT)

	INSERT INTO @TEMP_HC(PERSONA,CÓDIGO,NOMBRE,CARGO,SEGUNDOS_COMP)
	SELECT X.PERSONA, X.CÓDIGO, X.NOMBRE, X.CARGO, X.SEGUNDOS_COMP
	FROM (SELECT DISTINCT P.Persona AS 'PERSONA', M.Codigo AS 'CÓDIGO', LTRIM(RTRIM(P.NombreCompleto)) AS 'NOMBRE', LTRIM(RTRIM(C1.Descripcion)) AS 'CARGO',
	SUM(DATEDIFF(SS, '00:00:00.000',CONVERT(TIME, CONVERT(VARCHAR,CONVERT(TIME,C.TiempoComp),108)))) AS 'SEGUNDOS_COMP'
	FROM ReportesApp_Mantenimiento_AsignacionOT_Compensaciones C
	LEFT JOIN PersonaMast P ON P.Persona = C.Persona
	LEFT JOIN EmpleadoMast E ON P.Persona = E.Empleado AND E.Estado = 'A'
	LEFT JOIN hr_puestoempresa C1 ON C1.CodigoPuesto = E.CodigoCargo
	LEFT JOIN ReportesApp_Mantenimiento_AsignacionOT_Mecanicos M ON M.Persona = P.Persona
	WHERE (@Nombre IS NULL OR LTRIM(RTRIM(P.NombreCompleto)) LIKE '%' + @Nombre + '%')
	GROUP BY P.Persona, M.Codigo, LTRIM(RTRIM(P.NombreCompleto)), LTRIM(RTRIM(C1.Descripcion))) X

	INSERT INTO @TEMP_HE(PERSONA,CÓDIGO,NOMBRE,CARGO,SEGUNDOS_EXTRA,SEGUNDOS_COMP)
	SELECT X.PERSONA, X.CÓDIGO, X.NOMBRE, X.CARGO, X.SEGUNDOS_EXTRA - ISNULL(HC.SEGUNDOS_COMP,0), HC.SEGUNDOS_COMP
	FROM (SELECT DISTINCT P.Persona AS 'PERSONA', M.Codigo AS 'CÓDIGO', LTRIM(RTRIM(P.NombreCompleto)) AS 'NOMBRE', LTRIM(RTRIM(C1.Descripcion)) AS 'CARGO',
	SUM(DATEDIFF(SS, '00:00:00.000', CONVERT(TIME,CASE WHEN CONVERT(TIME,H.FechaInicio) < '13:00:00' AND CONVERT(TIME,H.FechaFin) > '14:30:00'
	THEN CONVERT(VARCHAR,CONVERT(TIME,DATEADD(MINUTE,-90,H.FechaFin - H.FechaInicio)),108)
	ELSE CONVERT(VARCHAR,CONVERT(TIME,H.FechaFin - H.FechaInicio),108) END))) AS 'SEGUNDOS_EXTRA'
	FROM ReportesApp_Mantenimiento_AsignacionOT_Historial H
	LEFT JOIN PersonaMast P ON P.Persona = H.Persona
	LEFT JOIN EmpleadoMast E ON P.Persona = E.Empleado AND E.Estado = 'A'
	LEFT JOIN hr_puestoempresa C1 ON C1.CodigoPuesto = E.CodigoCargo
	LEFT JOIN ReportesApp_Mantenimiento_AsignacionOT_Mecanicos M ON M.Persona = P.Persona
	WHERE (C1.Estado = 'A') AND (E.Estado = 'A') AND (H.Motivo = 'TIEMPO EXTRA') AND (H.Estado = 'APROBADO')
	AND (@Nombre IS NULL OR LTRIM(RTRIM(P.NombreCompleto)) LIKE '%' + @Nombre + '%')
	GROUP BY P.Persona, M.Codigo, LTRIM(RTRIM(P.NombreCompleto)), LTRIM(RTRIM(C1.Descripcion))) X
	LEFT JOIN @TEMP_HC HC ON X.PERSONA = HC.PERSONA

	SELECT X.PERSONA, X.CÓDIGO, X.NOMBRE, X.CARGO,
	ISNULL(RIGHT('0' + CAST(X.SEGUNDOS_EXTRA / 3600 AS VARCHAR(2)),2) + ':' +
	RIGHT('0' + CAST(X.SEGUNDOS_EXTRA % 3600 / 60 AS VARCHAR(2)),2) + ':' +
	RIGHT('0' + CAST(X.SEGUNDOS_EXTRA % 60 AS VARCHAR(2)),2),'00:00:00') AS 'HORAS_EXTRA',
	ISNULL(RIGHT('0' + CAST(X.SEGUNDOS_COMP / 3600 AS VARCHAR(2)),2) + ':' +
	RIGHT('0' + CAST(X.SEGUNDOS_COMP % 3600 / 60 AS VARCHAR(2)),2) + ':' +
	RIGHT('0' + CAST(X.SEGUNDOS_COMP % 60 AS VARCHAR(2)),2),'00:00:00') AS 'HORAS_COMP'
	FROM @TEMP_HE X
END

--------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 21-06-2024
-- Description:	INGRESAR Y ELIMINAR COMPENSACIONES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_AsignacionOT_IngresarEliminarComp]
@Opcion INT,
@CodigoComp VARCHAR(250),
@Persona INT,
@CFechaIni VARCHAR(20),
@CHoraIni VARCHAR(20),
@CFechaFin VARCHAR(20),
@CHoraFin VARCHAR(20),
@TotalHE VARCHAR(30),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @FechaIni DATETIME
DECLARE @FechaFin DATETIME
DECLARE @Correlativo INT

SET @Exito = '0 = Horas Extras Compensadas.'

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- INGRESAR COMPENSACION
		SET @Correlativo = (SELECT MAX(idCompensar) FROM ReportesApp_Mantenimiento_AsignacionOT_Compensaciones WHERE Anio = YEAR(GETDATE()))
		SET @Correlativo = ISNULL(@Correlativo,0) + 1

		SET @FechaIni = @CFechaIni+' '+@CHoraIni
		SET @FechaFin = @CFechaFin+' '+@CHoraFin

		IF (CONVERT(DATETIME,@FechaIni) > CONVERT(DATETIME,@FechaFin)) BEGIN
			SET @Exito = '-1 = La fecha de inicio no puede ser mayor a la fecha fin.'
			ROLLBACK
			GOTO Terminar
		END
		ELSE BEGIN
			INSERT INTO ReportesApp_Mantenimiento_AsignacionOT_Compensaciones(idCompensar,Anio,CodigoComp,Persona,TotalHE,FechaInicio,FechaFin,TiempoComp,UsuarioRegistro,FechaRegistro)
			VALUES(@Correlativo,YEAR(GETDATE()),CONVERT(VARCHAR,YEAR(GETDATE()))+'-'+CONVERT(VARCHAR,@Correlativo),@Persona,@TotalHE,@FechaIni,@FechaFin,
			CONVERT(VARCHAR,CONVERT(TIME,@FechaFin - @FechaIni),108),@Usuario,GETDATE())
		END
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR COMPENSACIÓN
		DELETE FROM ReportesApp_Mantenimiento_AsignacionOT_Compensaciones
		WHERE CodigoComp = @CodigoComp AND Persona = @Persona
		
		SET @Exito = '0 = Compensación eliminada.'
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
-- Create date: 21-06-2024
-- Description:	LISTAR COMPENSACIONES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_AsignacionOT_ListarCompensaciones]
@Persona INT
AS
BEGIN
	DECLARE @Segundos INT = (SELECT SUM(DATEDIFF(SS, '00:00:00.000', CONVERT(TIME,C.FechaFin - C.FechaInicio)))
	FROM ReportesApp_Mantenimiento_AsignacionOT_Compensaciones C WHERE C.Persona = @Persona)

	SELECT C.CodigoComp AS 'CÓDIGO', CONVERT(VARCHAR,C.FechaInicio,103)+' '+CONVERT(VARCHAR,C.FechaInicio,8) AS 'FECHA_INICIO',
	CONVERT(VARCHAR,C.FechaFin,103)+' '+CONVERT(VARCHAR,C.FechaFin,8) AS 'FECHA_FIN', C.TiempoComp AS 'HORAS_COMP', C.UsuarioRegistro, C.FechaRegistro,
	RIGHT('0' + CAST(@Segundos / 3600 AS VARCHAR(2)),2) + ':' +
	RIGHT('0' + CAST(@Segundos % 3600 / 60 AS VARCHAR(2)),2) + ':' +
	RIGHT('0' + CAST(@Segundos % 60 AS VARCHAR(2)),2) AS 'TOTAL_COMP'
	FROM ReportesApp_Mantenimiento_AsignacionOT_Compensaciones C
	LEFT JOIN ReportesApp_Mantenimiento_AsignacionOT_Mecanicos M ON M.Persona = C.Persona
	WHERE C.Persona = @Persona
END

----------------------------------------------------------------------------
----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 18-09-2024
-- Description:	LISTAR REQUERIMIENTOS APROBADOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_AsignacionOT_ListarRequerimientos]
@NroReq VARCHAR(30)
AS
BEGIN 
	SELECT TOP(25) LTRIM(RTRIM(R.RequisicionNumero)) AS 'REQUERIMIENTO', LTRIM(RTRIM(R.Comentarios)) AS 'DESCRIPCION',
	LTRIM(RTRIM(R.DefaultPrime)) AS 'DefaultPrime', LTRIM(RTRIM(A.LocalName)) AS 'CENTRO_COSTO', LTRIM(RTRIM(R.DefaultAfe)) AS 'DefaultAfe',
	LTRIM(RTRIM(F.localname)) AS 'PROYECTO'
	FROM WH_Requisiciones R
	LEFT JOIN AC_CostCenterMst A ON (R.DefaultPrime = A.CostCenter)
	LEFT JOIN afemst F ON (F.afe = R.DefaultAfe)
	WHERE (R.CompaniaSocio = '10000000') AND (R.Estado = 'AP')
	AND (@NroReq IS NULL OR LTRIM(RTRIM(R.RequisicionNumero)) LIKE '%' + @NroReq + '%')
END

--------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 18-09-2024
-- Description:	INSERTAR REQUERIMIENTOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_AsignacionOT_IngresarRequerimientos]
@NroReq VARCHAR(50),
@CentroCosto VARCHAR(50),
@Proyecto VARCHAR(50),
@Descripcion VARCHAR(350),
@FechaProgramada DATETIME,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

SET @Exito = '0 = Requerimiento Registrado.'

BEGIN TRAN
BEGIN TRY
	IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_AsignacionOT_Requerimientos WHERE NroRequerimiento = @NroReq)) BEGIN
		UPDATE ReportesApp_Mantenimiento_AsignacionOT_Requerimientos
		SET CentroCosto = @CentroCosto, Proyecto = @Proyecto, Descripcion = @Descripcion, FechaProgramada = @FechaProgramada,
		UsuarioCrea = @Usuario, FechaCrea = GETDATE()
		WHERE NroRequerimiento = @NroReq
	END
	ELSE BEGIN
		SET @correlativo = (SELECT MAX(idReq) FROM ReportesApp_Mantenimiento_AsignacionOT_Requerimientos WHERE Anio = YEAR(GETDATE()))
		SET @correlativo = ISNULL(@correlativo,0) + 1

		INSERT INTO ReportesApp_Mantenimiento_AsignacionOT_Requerimientos (idReq,Anio,CodigoReq,NroRequerimiento,CentroCosto,Proyecto,Descripcion,
		FechaProgramada,UsuarioCrea,FechaCrea)
		VALUES(@correlativo, YEAR(GETDATE()), SUBSTRING(CONVERT(VARCHAR(20), YEAR(GETDATE())),3,2) + CONVERT(VARCHAR(20),RIGHT('000000'+LTRIM(RTRIM(@correlativo)),6)),
		@NroReq,@CentroCosto,@Proyecto,@Descripcion,@FechaProgramada,@Usuario,GETDATE())
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

--------------------------------------------------------------------
--------------------------------------------------------------------

-- LIMPIAR TURNOS A LAS 0:00
/*
UPDATE ReportesApp_Mantenimiento_AsignacionOT_Mecanicos
SET Turno = 'NINGUNO'
WHERE Turno != 'NOCHE'
*/

--------------------------------------------------------------------

-- DESPROGRAMAR AUTOMÁTICAMENTE A MECÁNICOS DE OT DESPUÉS DE SU TURNO
-- MAÑANA
/*
INSERT INTO ReportesApp_Mantenimiento_AsignacionOT_Historial (NumeroOrden, Persona, FechaInicio, FechaFin, Turno, Motivo)
SELECT R.NumeroOrden, R.Persona, R.FechaInicio, GETDATE(), M.Turno, 'CIERRE AUTOMÁTICO A LAS 18:00'
FROM ReportesApp_Mantenimiento_AsignacionOT_Registro R
LEFT JOIN ReportesApp_Mantenimiento_AsignacionOT_Mecanicos M ON M.Persona = R.Persona
WHERE R.Estado = 0 AND M.Turno = 'MAÑANA'

UPDATE R
SET R.FechaFin = GETDATE(), R.Estado = 1, R.MotivoPausa = 'CIERRE AUTOMÁTICO A LAS 18:00'
FROM ReportesApp_Mantenimiento_AsignacionOT_Registro R
LEFT JOIN ReportesApp_Mantenimiento_AsignacionOT_Mecanicos M ON M.Persona = R.Persona
WHERE R.Estado = 0 AND M.Turno = 'MAÑANA'

UPDATE ReportesApp_Mantenimiento_AsignacionOT_Mecanicos
SET Estado = 'DISPONIBLE'
WHERE Estado = 'OCUPADO' AND Turno = 'MAÑANA'
*/

-- TARDE
/*
INSERT INTO ReportesApp_Mantenimiento_AsignacionOT_Historial (NumeroOrden, Persona, FechaInicio, FechaFin, Turno, Motivo)
SELECT R.NumeroOrden, R.Persona, R.FechaInicio, GETDATE(), M.Turno, 'CIERRE AUTOMÁTICO A LAS 21:00'
FROM ReportesApp_Mantenimiento_AsignacionOT_Registro R
LEFT JOIN ReportesApp_Mantenimiento_AsignacionOT_Mecanicos M ON M.Persona = R.Persona
WHERE R.Estado = 0 AND M.Turno = 'TARDE'

UPDATE R
SET R.FechaFin = GETDATE(), R.Estado = 1, R.MotivoPausa = 'CIERRE AUTOMÁTICO A LAS 21:00'
FROM ReportesApp_Mantenimiento_AsignacionOT_Registro R
LEFT JOIN ReportesApp_Mantenimiento_AsignacionOT_Mecanicos M ON M.Persona = R.Persona
WHERE R.Estado = 0 AND M.Turno = 'TARDE'

UPDATE ReportesApp_Mantenimiento_AsignacionOT_Mecanicos
SET Estado = 'DISPONIBLE'
WHERE Estado = 'OCUPADO' AND Turno = 'TARDE'
*/

-- NOCHE
/*
INSERT INTO ReportesApp_Mantenimiento_AsignacionOT_Historial (NumeroOrden, Persona, FechaInicio, FechaFin, Turno, Motivo)
SELECT R.NumeroOrden, R.Persona, R.FechaInicio, GETDATE(), M.Turno, 'CIERRE AUTOMÁTICO A LAS 6:00'
FROM ReportesApp_Mantenimiento_AsignacionOT_Registro R
LEFT JOIN ReportesApp_Mantenimiento_AsignacionOT_Mecanicos M ON M.Persona = R.Persona
WHERE R.Estado = 0 AND M.Turno = 'NOCHE'

UPDATE R
SET R.FechaFin = GETDATE(), R.Estado = 1, R.MotivoPausa = 'CIERRE AUTOMÁTICO A LAS 6:00'
FROM ReportesApp_Mantenimiento_AsignacionOT_Registro R
LEFT JOIN ReportesApp_Mantenimiento_AsignacionOT_Mecanicos M ON M.Persona = R.Persona
WHERE R.Estado = 0 AND M.Turno = 'NOCHE'

UPDATE ReportesApp_Mantenimiento_AsignacionOT_Mecanicos
SET Estado = 'DISPONIBLE'
WHERE Estado = 'OCUPADO' AND Turno = 'NOCHE'
*/

-- DOMINGO
/*
INSERT INTO ReportesApp_Mantenimiento_AsignacionOT_Historial (NumeroOrden, Persona, FechaInicio, FechaFin, Turno, Motivo)
SELECT R.NumeroOrden, R.Persona, R.FechaInicio, GETDATE(), M.Turno, 'CIERRE AUTOMÁTICO A LAS 17:00'
FROM ReportesApp_Mantenimiento_AsignacionOT_Registro R
LEFT JOIN ReportesApp_Mantenimiento_AsignacionOT_Mecanicos M ON M.Persona = R.Persona
WHERE R.Estado = 0 AND M.Turno = 'DOMINGO'

UPDATE R
SET R.FechaFin = GETDATE(), R.Estado = 1, R.MotivoPausa = 'CIERRE AUTOMÁTICO A LAS 17:00'
FROM ReportesApp_Mantenimiento_AsignacionOT_Registro R
LEFT JOIN ReportesApp_Mantenimiento_AsignacionOT_Mecanicos M ON M.Persona = R.Persona
WHERE R.Estado = 0 AND M.Turno = 'DOMINGO'

UPDATE ReportesApp_Mantenimiento_AsignacionOT_Mecanicos
SET Estado = 'DISPONIBLE'
WHERE Estado = 'OCUPADO' AND Turno = 'DOMINGO'
*/

