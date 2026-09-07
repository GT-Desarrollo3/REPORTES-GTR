
-- CREAR TABLA ReportesApp_Mantenimiento_AsignacionOT_CapacitacionesC

-- CREAR TABLA ReportesApp_Mantenimiento_AsignacionOT_CapacitacionesD

---------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 29/05/2026
-- Description:	INSERTAR DETALLE CAPACITACION
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_AsignacionOT_RegistrarDetalleCapacitacion]
@Opcion INT,
@idCapacitacionD INT,
@idCapacitacionC INT,
@Estado VARCHAR(30),
@Persona INT,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

SET @Exito = '0 = Asistencia añadida.'

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- INSERTAR ASISTENCIA
		IF EXISTS(SELECT * FROM ReportesApp_Mantenimiento_AsignacionOT_CapacitacionesD WHERE idCapacitacionC = @idCapacitacionC AND Persona = @Persona) BEGIN
			SET @Exito = '-1 = Este empleado ya fue registrado.'
			ROLLBACK
			GOTO Terminar
		END

		SET @correlativo = (SELECT MAX(idCapacitacionD) FROM ReportesApp_Mantenimiento_AsignacionOT_CapacitacionesD WHERE idCapacitacionC = @idCapacitacionC)
		SET @correlativo = ISNULL(@correlativo,0) + 1
	
		INSERT INTO ReportesApp_Mantenimiento_AsignacionOT_CapacitacionesD(idCapacitacionD, idCapacitacionC, Persona, Estado, UsuarioCreacion, FechaCreacion)
		VALUES(@correlativo, @idCapacitacionC, @Persona, @Estado, @Usuario, GETDATE())
	END

	IF (@Opcion = 2) BEGIN		-- EDITAR ESTADO
		UPDATE ReportesApp_Mantenimiento_AsignacionOT_CapacitacionesD
		SET Estado = @Estado
		WHERE idCapacitacionD = @idCapacitacionD AND idCapacitacionC = @idCapacitacionC
	END

	IF (@Opcion = 3) BEGIN		-- ELIMINAR ASISTENCIA
		DELETE FROM ReportesApp_Mantenimiento_AsignacionOT_CapacitacionesD
		WHERE idCapacitacionD = @idCapacitacionD AND idCapacitacionC = @idCapacitacionC
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

---------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 30/05/2026
-- Description:	LISTAR ASISTENTES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_AsignacionOT_ListarAsistentes]
@idCapacitacionC INT
AS
BEGIN
	SELECT D.idCapacitacionD, D.idCapacitacionC, RTRIM(P.Documento) AS 'DNI', RTRIM(P.NombreCompleto) AS 'NOMBRE',
	RTRIM(PU.Descripcion) AS 'CARGO'
	FROM ReportesApp_Mantenimiento_AsignacionOT_CapacitacionesD D
	LEFT JOIN PersonaMast P ON P.Persona = D.Persona 
	INNER JOIN EmpleadoMast E ON E.Empleado = P.Persona
	LEFT JOIN hr_puestoempresa PU ON PU.CodigoPuesto = E.CodigoCargo
	WHERE E.Estado = 'A' AND P.Estado = 'A' AND D.idCapacitacionC = @idCapacitacionC
END

------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 22-05-2026
-- Description:	INSERTAR DOCUMENTOS CAPACITACION
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Mantenimiento_AsignacionOT_RegistrarCapacitacion]
@Opcion INT,
@idCapacitacionC INT,
@Empresa VARCHAR(300),
@Capacitador VARCHAR(300),
@Tema VARCHAR(300),
@FechaInicio DATE,
@FechaFin DATE,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- REGISTRAR CAPACITACION
		SET @correlativo = (SELECT MAX(idCapacitacionC) FROM ReportesApp_Mantenimiento_AsignacionOT_CapacitacionesC)
		SET @correlativo = ISNULL(@correlativo,0) + 1
		
		INSERT INTO ReportesApp_Mantenimiento_AsignacionOT_CapacitacionesC(idCapacitacionC,Empresa,Capacitador,Tema,FechaInicio,FechaFin,UsuarioCreacion,FechaCreacion)
		VALUES (@correlativo,@Empresa,@Capacitador,@Tema,@FechaInicio,@FechaFin,@Usuario,GETDATE())

		UPDATE ReportesApp_Mantenimiento_AsignacionOT_CapacitacionesD
		SET idCapacitacionC = @correlativo
		WHERE idCapacitacionC = 0 

		SET @Exito = '0 = Capacitación Registrada Correctamente.'
	END

	IF (@Opcion = 2) BEGIN		-- EDITAR CAPACITACION
		UPDATE ReportesApp_Mantenimiento_AsignacionOT_CapacitacionesC
		SET Empresa = @Empresa, Capacitador = @Capacitador, Tema = @Tema, FechaInicio = @FechaInicio, FechaFin = @FechaFin, UsuarioCreacion = @Usuario,
		FechaCreacion = GETDATE()
		WHERE idCapacitacionC = @idCapacitacionC
		
		SET @Exito = '0 = Capacitación Actualizada Correctamente.'
	END

	IF (@Opcion = 3) BEGIN		-- ANULAR CAPACITACION
		DELETE FROM ReportesApp_Mantenimiento_AsignacionOT_CapacitacionesC
		WHERE idCapacitacionC = @idCapacitacionC

		DELETE FROM ReportesApp_Mantenimiento_AsignacionOT_CapacitacionesD
		WHERE idCapacitacionC = @idCapacitacionC
		
		SET @Exito = '0 = Capacitación Anulada Correctamente.'
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

---------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 01/06/2026
-- Description:	LISTAR CAPACITACIONES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_AsignacionOT_ListarCapacitaciones]
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@Estado VARCHAR(20),
@Tema VARCHAR(300),
@Asistente VARCHAR(300)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	SET NOCOUNT ON
	
	-- LISTAR REGISTROS
	SELECT CC.idCapacitacionC AS 'NRO', CC.Empresa AS 'EMPRESA', CC.Capacitador AS 'CAPACITADOR',
	CC.Tema AS 'TEMA', CC.FechaInicio AS 'INICIO', CC.FechaFin AS 'FIN', COUNT(CD.idCapacitacionD) AS 'ASISTENTES',
	CC.UsuarioCreacion, CC.FechaCreacion
	FROM ReportesApp_Mantenimiento_AsignacionOT_CapacitacionesC CC
	LEFT JOIN ReportesApp_Mantenimiento_AsignacionOT_CapacitacionesD CD ON CD.idCapacitacionC = CC.idCapacitacionC
	WHERE (CC.FechaInicio BETWEEN @FINICIO AND @FFIN) AND (CC.Tema IS NULL OR CC.Tema LIKE '%' + @Tema + '%')
	GROUP BY CC.idCapacitacionC, CC.Empresa, CC.Capacitador, CC.Tema, CC.FechaInicio, CC.FechaFin, CC.UsuarioCreacion, CC.FechaCreacion
	ORDER BY CC.FechaInicio DESC

	-- LISTAR ASISTENCIAS
	SELECT CC.idCapacitacionC, CD.idCapacitacionD AS 'NRO', CC.Empresa AS 'EMPRESA', CC.Capacitador AS 'CAPACITADOR',
	CC.Tema AS 'TEMA', CD.Persona, RTRIM(P.Documento) AS 'DNI', RTRIM(P.NombreCompleto) AS 'ASISTENTE',
	RTRIM(PU.Descripcion) AS 'CARGO', CC.FechaInicio AS 'INICIO', CC.FechaFin AS 'FIN', CD.Estado AS 'ESTADO',
	CD.UsuarioCreacion, CD.FechaCreacion
	FROM ReportesApp_Mantenimiento_AsignacionOT_CapacitacionesD CD 
	LEFT JOIN ReportesApp_Mantenimiento_AsignacionOT_CapacitacionesC CC ON CD.idCapacitacionC = CC.idCapacitacionC
	LEFT JOIN PersonaMast P ON P.Persona = CD.Persona
	INNER JOIN EmpleadoMast E ON E.Empleado = P.Persona
	LEFT JOIN hr_puestoempresa PU ON PU.CodigoPuesto = E.CodigoCargo
	WHERE (CC.FechaInicio BETWEEN @FINICIO AND @FFIN) AND (CC.Tema IS NULL OR CC.Tema LIKE '%' + @Tema + '%') AND
	(@Estado = 'TODAS' OR CD.Estado = @Estado) AND (RTRIM(P.NombreCompleto) IS NULL OR RTRIM(P.NombreCompleto) LIKE '%' + @Asistente + '%')
	ORDER BY CC.FechaInicio DESC
END

---------------------------------------------------------------




