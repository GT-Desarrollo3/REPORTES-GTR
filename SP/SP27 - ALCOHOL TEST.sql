
-- UPDATE Usuario SET Estado = 'A' WHERE Usuario = 'DEMO'
-- UPDATE PersonaMast SET Estado = 'A' WHERE Persona = 0

-----------------------------------------------------------

-- CREAR TABLA ReportesApp_Seguridad_AlcoholTest_Registro

-----------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 02-01-2024
-- Description:	BUSCAR SUCURSAL
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_AlcoholTest_BuscarSede]
@Usuario VARCHAR(20),
@Opcion INT
AS
BEGIN
	IF (@Opcion = 1) BEGIN		-- LISTAR SEDE
		SELECT TOP(1) P.Busqueda, E.CodigoUsuario, E.Empleado, LTRIM(RTRIM(E.Sucursal)) AS 'SEDE'
		FROM EmpleadoMast E
		LEFT JOIN PersonaMast P ON P.Persona = E.Empleado
		WHERE (E.Estado = 'A') AND (E.CodigoUsuario = @Usuario)
	END

	IF (@Opcion = 2) BEGIN		-- LISTAR SUCURSALES
		SELECT REPLACE(LTRIM(RTRIM(Sucursal)),'BTRU','AB') AS 'Sucursal', REPLACE(LTRIM(RTRIM(DescripcionLocal)),'Base Trujillo','Todas') AS 'Descripcion'
		FROM AC_Sucursal WHERE SucursalGrupo = '0001'
		ORDER BY REPLACE(LTRIM(RTRIM(Sucursal)),'BTRU','AB')
	END
END

-----------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 02-01-2024
-- Description:	BUSCAR PERSONAL
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_AlcoholTest_BuscarPersonal]
@DNI VARCHAR(20)
AS
BEGIN
	SELECT P.Persona, P.Documento, P.NombreCompleto, A1.Description AS 'Area', LTRIM(RTRIM(C1.Descripcion)) AS 'Cargo' 
	FROM PersonaMast P
	LEFT JOIN EmpleadoMast E ON P.Persona = E.Empleado
	LEFT JOIN departmentmst A1 ON A1.Department = E.DeptoOrganizacion
	LEFT JOIN hr_puestoempresa C1 ON C1.CodigoPuesto = E.CodigoCargo
	WHERE (P.Estado = 'A') AND (P.EsEmpleado = 'S') AND (E.Estado = 'A') AND (LTRIM(RTRIM(P.Documento)) = LTRIM(RTRIM(@DNI)))
END

-----------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 02-01-2024
-- Description:	REGISTRAR RESULTADOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_AlcoholTest_IngresarResultado]
@idPersona INT,
@ResultadoTest INT,
@Horario VARCHAR(20),
@Sede VARCHAR(4),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

/*
IF (EXISTS(SELECT * FROM ReportesApp_Seguridad_AlcoholTest_Registro WHERE (CONVERT(DATE,Fecha) = CONVERT(DATE,GETDATE())) AND (idPersona = @idPersona) AND (Sede = @Sede))) BEGIN
	SET @Exito = '-1 = Este usuario ya ha sido registrado el día de hoy.'
	GOTO Terminar
END
*/

SET @correlativo = (SELECT MAX(idAlcoholTest) FROM ReportesApp_Seguridad_AlcoholTest_Registro)
SET @correlativo = ISNULL(@correlativo,0) + 1

BEGIN TRAN
BEGIN TRY
	INSERT INTO ReportesApp_Seguridad_AlcoholTest_Registro(idAlcoholTest, idPersona, Fecha, Sede, ResultadoTest, Horario, UsuarioCreacion, FechaCreacion)
	VALUES(@correlativo, @idPersona, GETDATE(), @Sede, @ResultadoTest, @Horario, @Usuario, GETDATE())

	SET @Exito = '0 = Personal registrado correctamente.'
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

-----------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 02-01-2023
-- Description:	LISTAR RESULTADOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_AlcoholTest_ListarResultados]
@Sede CHAR(4),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@Sede = 'AB') BEGIN
		SELECT A.idAlcoholTest, P.Documento AS 'DNI', P.NombreCompleto AS 'NOMBRE', A1.Description AS 'AREA', LTRIM(RTRIM(C1.Descripcion)) AS 'CARGO',
		   (CASE WHEN C.CodigoEnapu = '1' THEN 'TOLVAS'
			     WHEN C.CodigoEnapu = '2' THEN 'LINDLEY'
			     WHEN C.CodigoEnapu = '3' THEN 'LIMAGAS'
				 WHEN C.CodigoEnapu = '4' THEN 'GENERAL'
				 WHEN C.CodigoEnapu = '5' /*OR C.CodigoEnapu IS NULL*/ THEN 'SIN OPERACION'
				 WHEN C.CodigoEnapu = '6' THEN 'MAQ/CAMIONETAS'
				 WHEN C.CodigoEnapu = '9' THEN 'LOCAL'
				 ELSE ' ' END) AS 'OPERACION',
		   (CASE WHEN A.Sede = 'BTRU' THEN 'TRUJILLO'
		         WHEN A.Sede = 'SAL' THEN 'SALAVERRY' 
				 WHEN A.Sede = 'BLIM' THEN 'LIMA'
				 WHEN A.Sede = 'ENCA' THEN 'LA ENCALADA'
				 WHEN A.Sede = 'LAR1' THEN 'LARREA 1'
				 ELSE 'LARREA 2' END) AS 'SEDE', CONVERT(VARCHAR,A.Fecha,103) AS 'FECHA',
		   (CASE WHEN A.ResultadoTest = 1 THEN 'POSITIVO' ELSE 'NEGATIVO' END) AS 'RESULTADO', A.Horario AS 'HORARIO', A.UsuarioCreacion, A.FechaCreacion
		FROM ReportesApp_Seguridad_AlcoholTest_Registro A
		LEFT JOIN PersonaMast P ON P.Persona = A.idPersona
		LEFT JOIN EmpleadoMast E ON P.Persona = E.Empleado
		LEFT JOIN OP_TR_Conductor C ON P.Persona = C.IdPersona
		LEFT JOIN departmentmst A1 ON A1.Department = E.DeptoOrganizacion
		LEFT JOIN hr_puestoempresa C1 ON C1.CodigoPuesto = E.CodigoCargo
		WHERE (E.Estado = 'A') AND (A.Fecha BETWEEN @FINICIO AND @FFIN)
		ORDER BY A.idAlcoholTest DESC
	END
	ELSE BEGIN
		SELECT A.idAlcoholTest, P.Documento AS 'DNI', P.NombreCompleto AS 'NOMBRE', A1.Description AS 'AREA', LTRIM(RTRIM(C1.Descripcion)) AS 'CARGO',
		   (CASE WHEN C.CodigoEnapu = '1' THEN 'TOLVAS'
			     WHEN C.CodigoEnapu = '2' THEN 'LINDLEY'
			     WHEN C.CodigoEnapu = '3' THEN 'LIMAGAS'
				 WHEN C.CodigoEnapu = '4' THEN 'GENERAL'
				 WHEN C.CodigoEnapu = '5' /*OR C.CodigoEnapu IS NULL*/ THEN 'SIN OPERACION'
				 WHEN C.CodigoEnapu = '6' THEN 'MAQ/CAMIONETAS'
				 WHEN C.CodigoEnapu = '9' THEN 'LOCAL'
				 ELSE ' ' END) AS 'OPERACION',
		   (CASE WHEN A.Sede = 'BTRU' THEN 'TRUJILLO'
		         WHEN A.Sede = 'SAL' THEN 'SALAVERRY' 
				 WHEN A.Sede = 'BLIM' THEN 'LIMA'
				 WHEN A.Sede = 'ENCA' THEN 'LA ENCALADA'
				 WHEN A.Sede = 'LAR1' THEN 'LARREA 1'
				 ELSE 'LARREA 2' END) AS 'SEDE', CONVERT(VARCHAR,A.Fecha,103) AS 'FECHA',
		   (CASE WHEN A.ResultadoTest = 1 THEN 'POSITIVO' ELSE 'NEGATIVO' END) AS 'RESULTADO', A.Horario AS 'HORARIO', A.UsuarioCreacion, A.FechaCreacion
		FROM ReportesApp_Seguridad_AlcoholTest_Registro A
		LEFT JOIN PersonaMast P ON P.Persona = A.idPersona
		LEFT JOIN EmpleadoMast E ON P.Persona = E.Empleado
		LEFT JOIN OP_TR_Conductor C ON P.Persona = C.IdPersona
		LEFT JOIN departmentmst A1 ON A1.Department = E.DeptoOrganizacion
		LEFT JOIN hr_puestoempresa C1 ON C1.CodigoPuesto = E.CodigoCargo
		WHERE (A.Sede = @Sede) AND (E.Estado = 'A') AND (A.Fecha BETWEEN @FINICIO AND @FFIN)
		ORDER BY A.idAlcoholTest DESC
	END
END

-----------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 02-01-2024
-- Description:	ELIMINAR RESULTADOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_AlcoholTest_EliminarResultados]
@Opcion INT,
@idAlcoholTest INT,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN
		DELETE FROM ReportesApp_Seguridad_AlcoholTest_Registro
		WHERE idAlcoholTest = @idAlcoholTest

		SET @Exito = '0 = Personal eliminado correctamente.'
	END
	
	IF (@Opcion = 2) BEGIN
		DECLARE @ResultadoTest INT
		SET @ResultadoTest = (SELECT ResultadoTest FROM ReportesApp_Seguridad_AlcoholTest_Registro WHERE idAlcoholTest = @idAlcoholTest)

		IF (@ResultadoTest = 0) BEGIN
			UPDATE ReportesApp_Seguridad_AlcoholTest_Registro
			SET ResultadoTest = 1, UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()
			WHERE idAlcoholTest = @idAlcoholTest
		END
		ELSE BEGIN
			UPDATE ReportesApp_Seguridad_AlcoholTest_Registro
			SET ResultadoTest = 0, UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()
			WHERE idAlcoholTest = @idAlcoholTest
		END

		SET @Exito = '0 = Estado actualizado.'
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

------------------------------------------------------

SELECT * FROM Usuario WHERE Estado = 'A'

--INSERT INTO Usuario (Usuario,UsuarioPerfil,Nombre,Clave,ExpirarPasswordFlag,Estado,UltimoUsuario,UltimaFechaModif,UsuarioRed)
--VALUES('VIGILA1','PE','VIGILANTE-LARREA1','QGUY68:','N','A','DEMO',GETDATE(),'123')
--VALUES('VIGILA2','PE','VIGILANTE-LARREA2','QGUY68:','N','A','DEMO',GETDATE(),'123')
--VALUES('VIGILA3','PE','VIGILANTE-SALAVERRY','QGUY68:','N','A','DEMO',GETDATE(),'123')
--VALUES('VIGILA4','PE','VIGILANTE-LIMA','QGUY68:','N','A','DEMO',GETDATE(),'123')
--VALUES('VIGILA5','PE','VIGILANTE-ENCALADA','QGUY68:','N','A','DEMO',GETDATE(),'123')

SELECT D.* FROM ReportesApp_DetalleUsuarioReporte D
LEFT JOIN Usuario U ON D.idUsuario = U.Usuario
WHERE U.Estado = 'A'
ORDER BY D.idUsuario

--INSERT INTO ReportesApp_DetalleUsuarioReporte(idUsuario,idReporte,Nuevo,Editar,Leer,Anular,PermisosEspeciales)
--VALUES('VIGILA1',194,1,0,0,0,'<d><r><idPermisoEspecial>1</idPermisoEspecial><NombrePermiso>Sucursal Larrea 1</NombrePermiso><Activo>1</Activo></r></d>')
--VALUES('VIGILA2',194,1,0,0,0,'<d><r><idPermisoEspecial>2</idPermisoEspecial><NombrePermiso>Sucursal Larrea 2</NombrePermiso><Activo>1</Activo></r></d>')
--VALUES('VIGILA3',194,1,0,0,0,'<d><r><idPermisoEspecial>3</idPermisoEspecial><NombrePermiso>Sucursal Salaverry</NombrePermiso><Activo>1</Activo></r></d>')
--VALUES('VIGILA4',194,1,0,0,0,'<d><r><idPermisoEspecial>4</idPermisoEspecial><NombrePermiso>Sucursal Lima</NombrePermiso><Activo>1</Activo></r></d>')
--VALUES('VIGILA5',194,1,0,0,0,'<d><r><idPermisoEspecial>5</idPermisoEspecial><NombrePermiso>Sucursal La Encalada</NombrePermiso><Activo>1</Activo></r></d>')
