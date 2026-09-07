
-- CREAR TABLA ReportesApp_RRHH_Capacitaciones_Registros

----------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 21/05/2026
-- Description:	LISTAR EMPLEADOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_RRHH_Capacitaciones_ListarEmpleados]
@Opcion INT,
@Personal VARCHAR(300)
AS
BEGIN
	IF (@Opcion = 1) BEGIN		-- LISTAR EMPLEADOS
		SELECT DISTINCT TOP(20) P.Persona, RTRIM(P.NombreCompleto) AS 'EMPLEADO', RTRIM(P.Documento) AS 'DNI'
		FROM PersonaMast P
		LEFT JOIN EmpleadoMast E ON E.Empleado = P.Persona 
		WHERE (P.EsEmpleado = 'S') AND (P.Estado = 'A') AND (E.Estado = 'A') AND (E.CompaniaSocio = '10000000')
		AND (RTRIM(P.NombreCompleto) IS NULL OR RTRIM(P.NombreCompleto) LIKE '%' + @Personal + '%')
	END
	ELSE BEGIN
		SELECT DISTINCT TOP(20) P.Persona, RTRIM(ISNULL(P.NombreCompleto,P.Busqueda)) AS 'EMPLEADO',
		RTRIM(P.Documento) AS 'RUC' FROM PersonaMast P
		WHERE (P.EsProveedor = 'S') AND (P.Estado = 'A') AND
		(RTRIM(ISNULL(P.NombreCompleto,P.Busqueda)) IS NULL OR RTRIM(ISNULL(P.NombreCompleto,P.Busqueda)) LIKE '%' + @Personal + '%')
	END
END

----------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 22-05-2026
-- Description:	INSERTAR DOCUMENTOS CAPACITACION
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_RRHH_Capacitaciones_RegistrarEditarEliminarCapacitacion]
@Opcion INT,
@idCapacitacion INT,
@Persona INT,
@Proveedor INT,
@Archivo VARBINARY(MAX),
@Titulo VARCHAR(500),
@Extension VARCHAR(100),
@Duracion INT,
@FechaInicio DATE,
@Monto DECIMAL(10,2),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- REGISTRAR CAPACITACION
		SET @correlativo = (SELECT MAX(idCapacitacion) FROM ReportesApp_RRHH_Capacitaciones_Registros)
		SET @correlativo = ISNULL(@correlativo,0) + 1
		
		INSERT INTO ReportesApp_RRHH_Capacitaciones_Registros(idCapacitacion,Persona,Proveedor,Archivo,Titulo,Extension,Duracion,FechaInicio,
		FechaFin,Monto,UsuarioCreacion,FechaCreacion,UsuarioModificacion,FechaModificacion,Estado)
		VALUES (@correlativo,@Persona,@Proveedor,@Archivo,@Titulo,@Extension,@Duracion,@FechaInicio,DATEADD(MONTH,@Duracion,@FechaInicio),
		@Monto,@Usuario,GETDATE(),@Usuario,GETDATE(),1)

		SET @Exito = '0 = Convenio Registrado Correctamente.'
	END

	IF (@Opcion = 2) BEGIN		-- EDITAR CAPACITACION
		UPDATE ReportesApp_RRHH_Capacitaciones_Registros
		SET Persona = @Persona, Proveedor = @Proveedor, Archivo = @Archivo, Titulo = @Titulo, Extension = @Extension, Duracion = @Duracion,
		FechaInicio = @FechaInicio, FechaFin = DATEADD(MONTH,@Duracion,@FechaInicio), Monto = @Monto, UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()
		WHERE idCapacitacion = @idCapacitacion
		
		SET @Exito = '0 = Convenio Actualizado Correctamente.'
	END

	IF (@Opcion = 3) BEGIN		-- ANULAR CAPACITACION
		UPDATE ReportesApp_RRHH_Capacitaciones_Registros
		SET Estado = 0
		WHERE idCapacitacion = @idCapacitacion
		
		SET @Exito = '0 = Convenio Anulado Correctamente.'
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

----------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 22-05-2026
-- Description:	LISTAR DOCUMENTOS CAPACITACION
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_RRHH_Capacitaciones_ListarCapacitaciones]
@Empleado VARCHAR(500),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@Estado VARCHAR(20)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	SET NOCOUNT ON

	SELECT * FROM
	(SELECT R.idCapacitacion AS 'NRO', R.Persona, RTRIM(P.NombreCompleto) AS 'EMPLEADO', R.Proveedor, RTRIM(P2.NombreCompleto) AS 'NOMBRE_PROVEEDOR',
	R.Titulo AS 'DOCUMENTO', R.Duracion AS 'DURACION', R.FechaInicio AS 'INICIO', R.FechaFin AS 'FIN',
	CASE WHEN CONVERT(DATE,GETDATE()) < R.FechaFin THEN 'EN CURSO' WHEN  R.FechaFin >= CONVERT(DATE,GETDATE()) THEN 'CONCLUIDO'
	ELSE 'PENDIENTE' END AS 'ESTADO', R.Monto AS 'MONTO', R.Extension AS 'EXTENSION', 'Vista Previa' AS 'VISTA PREVIA', 'Descargar' AS 'DESCARGAR',
	R.UsuarioCreacion, R.FechaCreacion, R.UsuarioModificacion, R.FechaModificacion
	FROM ReportesApp_RRHH_Capacitaciones_Registros R
	LEFT JOIN PersonaMast P ON P.Persona = R.Persona
	LEFT JOIN PersonaMast P2 ON P2.Persona = R.Proveedor
	WHERE (R.Estado = 1)) X
	WHERE (X.EMPLEADO IS NULL OR X.EMPLEADO LIKE '%' + @Empleado + '%') AND (X.INICIO BETWEEN @FINICIO AND @FFIN) AND
	(@Estado = 'TODOS' OR X.ESTADO = @Estado)
	ORDER BY X.NRO DESC
END
