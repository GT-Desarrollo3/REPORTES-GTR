
-- CREAR TABLA ReportesApp_Seguridad_RegistroEMO_Registros Y LLENARLA

------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 12-09-2024
-- Description:	LISTAR PERSONAL
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_RegistroEMO_ListarPersonal]
@Filtro VARCHAR(250)
AS
BEGIN
	SELECT TOP(25) P.Persona, LTRIM(RTRIM(P.NombreCompleto)) AS 'EMPLEADO', D.description AS 'ÁREA',
	REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(PE.Descripcion,'Á','A'),'É','E'),'Í','I'),'Ó','O'),'Ú','U') AS 'PUESTO'
	FROM PersonaMast P WITH(NOLOCK)
	LEFT JOIN EmpleadoMast EM WITH(NOLOCK) ON EM.Empleado = P.Persona
	LEFT JOIN HR_PuestoEmpresa PE ON PE.CodigoPuesto = EM.CodigoCargo AND PE.CodigoPuesto IS NOT NULL
	LEFT JOIN departmentmst D ON D.department = EM.DeptoOrganizacion AND D.department IS NOT NULL
	WHERE ((P.EsEmpleado = 'S') AND (EM.Estado = 'A') AND (P.NombreCompleto LIKE '%' + @Filtro + '%'))
	ORDER BY Busqueda
END

------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 11-09-2024
-- Description:	LISTAR DOCUMENTOS EMO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_RegistroEMO_ListarEMO]
@Conductor VARCHAR(250),
@Estado VARCHAR(20)
AS
BEGIN
	IF (@Estado = 'TODOS') BEGIN
		SELECT * FROM
		(SELECT EMO.idEMO, EMO.idPersona, LTRIM(RTRIM(P.NombreCompleto)) AS 'EMPLEADO', D.description AS 'ÁREA',
		REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(PE.Descripcion,'Á','A'),'É','E'),'Í','I'),'Ó','O'),'Ú','U') AS 'PUESTO',
		CASE WHEN (C.CodigoEnapu = 5 OR C.CodigoEnapu IS NULL) THEN 'SIN OPERACION' ELSE O1.Descripcion END AS 'OPERACIÓN',
		EMO.TipoDocumento, TD.Descripcion AS 'TIPO_DOCUMENTO', EMO.FechaInicioValidez AS 'FECHA_INICIO_VALIDEZ', EMO.FechaFinValidez AS 'FECHA_FIN_VALIDEZ',
		(CASE WHEN EMO.FechaFinValidez IS NULL OR DATEDIFF (D, GETDATE(), EMO.FechaFinValidez) > TD.DiasAlerta THEN 'VIGENTE' 
		WHEN EMO.FechaFinValidez IS NOT NULL AND DATEDIFF (D, GETDATE(), EMO.FechaFinValidez) <= TD.DiasAlerta AND DATEDIFF(D, GETDATE(), EMO.FechaFinValidez) > 0
		THEN 'POR VENCER' ELSE 'VENCIDO' END) AS 'ESTADO', CASE WHEN EMO.Categoria = 1 THEN 'APTO' WHEN EMO.Categoria = 2 THEN 'APTO CON RESTRICCION'
		WHEN EMO.Categoria = 3 THEN 'NO APTO' ELSE '' END AS 'CATEGORIA', EMO.RutaLocal AS 'DIRECTORIO', EMO.Resultados, EMO.Resultados2, EMO.Resultados3
		FROM ReportesApp_Seguridad_RegistroEMO_Registros EMO
		LEFT JOIN PersonaMast P WITH(NOLOCK) ON P.Persona = EMO.idPersona
		LEFT JOIN EmpleadoMast EM WITH(NOLOCK) ON EM.Empleado = P.Persona
		LEFT JOIN OP_TR_Conductor C ON EMO.idPersona = C.IdPersona and C.Estado = 'A'
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O1 ON C.CodigoEnapu = O1.IdOperacion
		LEFT JOIN ReportesApp_Operacion_TiposDocumentos TD WITH(NOLOCK) ON TD.IdTipoDocumento = EMO.TipoDocumento
		LEFT JOIN HR_PuestoEmpresa PE ON PE.CodigoPuesto = EM.CodigoCargo
		LEFT JOIN departmentmst D ON D.department = EM.DeptoOrganizacion
		WHERE EMO.Estado = 'A' AND EM.estado = 'A' AND D.department IS NOT NULL AND PE.CodigoPuesto IS NOT NULL) X
		WHERE (@Conductor IS NULL OR X.EMPLEADO LIKE '%' + @Conductor + '%')
		ORDER BY X.EMPLEADO
	END
	ELSE BEGIN
		SELECT * FROM
		(SELECT EMO.idEMO, EMO.idPersona, LTRIM(RTRIM(P.NombreCompleto)) AS 'EMPLEADO', D.description AS 'ÁREA',
		REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(PE.Descripcion,'Á','A'),'É','E'),'Í','I'),'Ó','O'),'Ú','U') AS 'PUESTO',
		CASE WHEN (C.CodigoEnapu = 5 OR C.CodigoEnapu IS NULL) THEN 'SIN OPERACION' ELSE O1.Descripcion END AS 'OPERACIÓN',
		EMO.TipoDocumento, TD.Descripcion AS 'TIPO_DOCUMENTO', EMO.FechaInicioValidez AS 'FECHA_INICIO_VALIDEZ', EMO.FechaFinValidez AS 'FECHA_FIN_VALIDEZ',
		(CASE WHEN EMO.FechaFinValidez IS NULL OR DATEDIFF (D, GETDATE(), EMO.FechaFinValidez) > TD.DiasAlerta THEN 'VIGENTE' 
		WHEN EMO.FechaFinValidez IS NOT NULL AND DATEDIFF (D, GETDATE(), EMO.FechaFinValidez) <= TD.DiasAlerta AND DATEDIFF(D, GETDATE(), EMO.FechaFinValidez) > 0
		THEN 'POR VENCER' ELSE 'VENCIDO' END) AS 'ESTADO', CASE WHEN EMO.Categoria = 1 THEN 'APTO' WHEN EMO.Categoria = 2 THEN 'APTO CON RESTRICCION'
		WHEN EMO.Categoria = 3 THEN 'NO APTO' ELSE '' END AS 'CATEGORIA', EMO.RutaLocal AS 'DIRECTORIO', EMO.Resultados, EMO.Resultados2, EMO.Resultados3
		FROM ReportesApp_Seguridad_RegistroEMO_Registros EMO
		LEFT JOIN PersonaMast P WITH(NOLOCK) ON P.Persona = EMO.idPersona
		LEFT JOIN EmpleadoMast EM WITH(NOLOCK) ON EM.Empleado = P.Persona
		LEFT JOIN OP_TR_Conductor C ON EMO.idPersona = C.IdPersona and C.Estado = 'A'
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O1 ON C.CodigoEnapu = O1.IdOperacion
		LEFT JOIN ReportesApp_Operacion_TiposDocumentos TD WITH(NOLOCK) ON TD.IdTipoDocumento = EMO.TipoDocumento
		LEFT JOIN HR_PuestoEmpresa PE ON PE.CodigoPuesto = EM.CodigoCargo
		LEFT JOIN departmentmst D ON D.department = EM.DeptoOrganizacion
		WHERE EMO.Estado = 'A' AND EM.estado = 'A' AND D.department IS NOT NULL AND PE.CodigoPuesto IS NOT NULL) X
		WHERE (@Conductor IS NULL OR X.EMPLEADO LIKE '%' + @Conductor + '%') AND (X.ESTADO = @Estado)
		ORDER BY X.EMPLEADO
	END
END

---------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 12-09-2024
-- Description:	REGISTRAR EXAMEN MEDICO OCUPACIONAL
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_RegistroEMO_RegistrarEMO]
@Opcion INT,
@idPersona INT,
@FechaInicioValidez DATETIME,
@FechaFinValidez DATETIME,
@Categoria VARCHAR(120),
@RutaLocal VARCHAR(MAX),
@Usuario VARCHAR(20)
AS
DECLARE @Contador INT 
DECLARE @idCategoria INT
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	SET @idCategoria = (SELECT CASE WHEN @Categoria = 'APTO' THEN 1 WHEN @Categoria = 'APTO CON RESTRICCION' THEN 2
						WHEN @Categoria = 'NO APTO' THEN 3 ELSE 0 END)

	IF (CONVERT(DATE,@FechaInicioValidez) < CONVERT(DATE,GETDATE()) AND CONVERT(DATE,@FechaFinValidez) < CONVERT(DATE,GETDATE())) BEGIN
		SET @Exito = '-1 = No puede registrar un examen con una fecha anterior a la de hoy'
		ROLLBACK
		GOTO Terminar
	END
	ELSE BEGIN
		SET @Contador = (SELECT MAX(idEMO) FROM ReportesApp_Seguridad_RegistroEMO_Registros)
		SET @Contador = ISNULL(@Contador,0) + 1 
		
		IF (EXISTS(SELECT * FROM ReportesApp_Seguridad_RegistroEMO_Registros WHERE idPersona = @idPersona AND Estado = 'A')) BEGIN
			UPDATE ReportesApp_Seguridad_RegistroEMO_Registros
			SET Estado = 'I'
			WHERE idPersona = @idPersona AND Estado = 'A'

			INSERT INTO ReportesApp_Seguridad_RegistroEMO_Registros(idEMO,idPersona,TipoDocumento,FechaInicioValidez,FechaFinValidez,Categoria,Estado,
			RutaLocal,UsuarioCrea,FechaCrea,UsuarioModifica,FechaModifica)
			VALUES(@Contador, @idPersona, 22, @FechaInicioValidez, @FechaFinValidez, @idCategoria, 'A', @RutaLocal, @Usuario, GETDATE(), @Usuario, GETDATE())

			SET @Exito = '0 = El examen se actualizó exitosamente.'
		END
		ELSE BEGIN
			INSERT INTO ReportesApp_Seguridad_RegistroEMO_Registros(idEMO,idPersona,TipoDocumento,FechaInicioValidez,FechaFinValidez,Categoria,Estado,
			RutaLocal,UsuarioCrea,FechaCrea,UsuarioModifica,FechaModifica)
			VALUES(@Contador, @idPersona, 22, @FechaInicioValidez, @FechaFinValidez, @idCategoria, 'A', @RutaLocal, @Usuario, GETDATE(), @Usuario, GETDATE())

			SET @Exito = '0 = El examen se registró exitosamente.'
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

----------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 04-10-2024
-- Description:	REGISTRAR EXAMEN MEDICO OCUPACIONAL
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_RegistroEMO_RegistrarEMOResultado]
@Opcion INT,
@idPersona INT,
@FechaInicioValidez DATETIME,
@FechaFinValidez DATETIME,
@Categoria VARCHAR(120),
@RutaLocal VARCHAR(MAX),
@Resultados VARCHAR(MAX),
@Resultados2 VARCHAR(MAX),
@Resultados3 VARCHAR(MAX),
@Usuario VARCHAR(20)
AS
DECLARE @Contador INT 
DECLARE @idCategoria INT
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	SET @idCategoria = (SELECT CASE WHEN @Categoria = 'APTO' THEN 1 WHEN @Categoria = 'APTO CON RESTRICCION' THEN 2
						WHEN @Categoria = 'NO APTO' THEN 3 ELSE 0 END)

	IF (CONVERT(DATE,@FechaInicioValidez) < CONVERT(DATE,GETDATE()) AND CONVERT(DATE,@FechaFinValidez) < CONVERT(DATE,GETDATE())) BEGIN
		SET @Exito = '-1 = No puede registrar un examen con una fecha anterior a la de hoy'
		ROLLBACK
		GOTO Terminar
	END
	ELSE BEGIN
		SET @Contador = (SELECT MAX(idEMO) FROM ReportesApp_Seguridad_RegistroEMO_Registros)
		SET @Contador = ISNULL(@Contador,0) + 1 
		
		IF (EXISTS(SELECT * FROM ReportesApp_Seguridad_RegistroEMO_Registros WHERE idPersona = @idPersona AND Estado = 'A')) BEGIN
			UPDATE ReportesApp_Seguridad_RegistroEMO_Registros
			SET Estado = 'I'
			WHERE idPersona = @idPersona AND Estado = 'A'

			INSERT INTO ReportesApp_Seguridad_RegistroEMO_Registros(idEMO,idPersona,TipoDocumento,FechaInicioValidez,FechaFinValidez,Categoria,Estado,
			RutaLocal,Resultados,Resultados2,Resultados3,UsuarioCrea,FechaCrea,UsuarioModifica,FechaModifica)
			VALUES(@Contador, @idPersona, 22, @FechaInicioValidez, @FechaFinValidez, @idCategoria, 'A', @RutaLocal, @Resultados, @Resultados2, @Resultados3,
			@Usuario, GETDATE(), @Usuario, GETDATE())

			SET @Exito = '0 = El examen se actualizó exitosamente.'
		END
		ELSE BEGIN
			INSERT INTO ReportesApp_Seguridad_RegistroEMO_Registros(idEMO,idPersona,TipoDocumento,FechaInicioValidez,FechaFinValidez,Categoria,Estado,
			RutaLocal,Resultados,Resultados2,Resultados3,UsuarioCrea,FechaCrea,UsuarioModifica,FechaModifica)
			VALUES(@Contador, @idPersona, 22, @FechaInicioValidez, @FechaFinValidez, @idCategoria, 'A', @RutaLocal, @Resultados, @Resultados2, @Resultados3,
			@Usuario, GETDATE(), @Usuario, GETDATE())

			SET @Exito = '0 = El examen se registró exitosamente.'
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

----------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 12-09-2024
-- Description:	LISTAR HISTORIAL DOCUMENTOS EMO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_RegistroEMO_ListarHistorialEMO]
@Conductor VARCHAR(250),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	SELECT * FROM
	(SELECT EMO.idEMO, EMO.idPersona, LTRIM(RTRIM(P.NombreCompleto)) AS 'EMPLEADO', D.description AS 'ÁREA',
	REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(PE.Descripcion,'Á','A'),'É','E'),'Í','I'),'Ó','O'),'Ú','U') AS 'PUESTO',
	CASE WHEN (C.CodigoEnapu = 5 OR C.CodigoEnapu IS NULL) THEN 'SIN OPERACION' ELSE O1.Descripcion END AS 'OPERACIÓN',
	EMO.TipoDocumento, TD.Descripcion AS 'TIPO_DOCUMENTO', EMO.FechaInicioValidez AS 'FECHA_INICIO_VALIDEZ', EMO.FechaFinValidez AS 'FECHA_FIN_VALIDEZ',
	CASE WHEN EMO.Categoria = 1 THEN 'APTO' WHEN EMO.Categoria = 2 THEN 'APTO CON RESTRICCION'
	WHEN EMO.Categoria = 3 THEN 'NO APTO' ELSE '' END AS 'CATEGORIA', EMO.UsuarioCrea, EMO.FechaCrea, EMO.RutaLocal AS 'DIRECTORIO',
	EMO.Resultados, EMO.Resultados2, EMO.Resultados3
	FROM ReportesApp_Seguridad_RegistroEMO_Registros EMO
	LEFT JOIN PersonaMast P WITH(NOLOCK) ON P.Persona = EMO.idPersona
	LEFT JOIN EmpleadoMast EM WITH(NOLOCK) ON EM.Empleado = P.Persona
	LEFT JOIN OP_TR_Conductor C ON EMO.idPersona = C.IdPersona and C.Estado = 'A'
	LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O1 ON C.CodigoEnapu = O1.IdOperacion
	LEFT JOIN ReportesApp_Operacion_TiposDocumentos TD WITH(NOLOCK) ON TD.IdTipoDocumento = EMO.TipoDocumento
	LEFT JOIN HR_PuestoEmpresa PE ON PE.CodigoPuesto = EM.CodigoCargo
	LEFT JOIN departmentmst D ON D.department = EM.DeptoOrganizacion
	WHERE EMO.Estado = 'I' AND EM.estado = 'A' AND D.department IS NOT NULL AND PE.CodigoPuesto IS NOT NULL) X
	WHERE (@Conductor IS NULL OR X.EMPLEADO LIKE '%' + @Conductor + '%') AND (X.FechaCrea BETWEEN @FINICIO AND @FFIN)
	AND ((X.Resultados IS NOT NULL) OR (X.Resultados2 IS NOT NULL) OR (X.Resultados3 IS NOT NULL))
	ORDER BY X.EMPLEADO, X.FechaCrea DESC
END