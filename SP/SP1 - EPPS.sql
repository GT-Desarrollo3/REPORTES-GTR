
--CREAR TABLA ReportesApp_Seguridad_EPPS (idEPPS INT, idTipoEPPS INT, CodInterno VARCHAR(10), NombreEPPS VARCHAR(250), Estado BIT, areaProceso VARCHAR(250), area VARCHAR(50), esActivo VARCHAR(250))

--CREAR TABLA ReportesApp_Seguridad_EPPSxPersona (idEPPSPersonal INT, idPersonal INT, idTipoEPPS INT, idEPPS INT, idVidaUtil INT, fechaRegistro DATETIME, fechaCaducidad DATETIME, diasRestantes INT, Puesto VARCHAR(250), AreaProceso VARCHAR(50), CodInterno VARCHAR(50), esActivo VARCHAR(250), idEstado INT, UsuarioCreacion VARCHAR(50), FechaCreacion DATETIME, UltimoUsuario VARCHAR(50), UltimaModificacion DATETIME)

--CREAR TABLA ReportesApp_Seguridad_RegistroEPPS (idEPPSPersonal_H INT, idEPPSPersonal INT, idPersonal INT, idTipoEPPS INT, idEPPS INT, idVidaUtil INT, fechaAsignacion DATE, fechaCaducidad DATETIME, CategoriaVidaUtil VARCHAR(250), AreaProceso VARCHAR(50), CodInterno VARCHAR(50), esActivo VARCHAR(250), idEstado INT, fechaDesvincular DATETIME, diasRestantes INT, UsuarioCreacion VARCHAR(50), FechaCreacion DATETIME, UltimoUsuario VARCHAR(50), UltimaModificacion DATETIME)

--CREAR TABLA ReportesApp_Seguridad_TiposEPPS (TipoEPPS INT, Estado BIT, Nombre NVARCHAR(50))

--CREAR TABLA ReportesApp_Seguridad_VidaUtil (idVidaUtil INT, TipoEPPS INT, puesto VARCHAR(250), areaProceso VARCHAR(50), cantidadMeses INT)

--CREAR TABLA ReportesApp_Seguridad_EstadoEPPS (idEstado INT, Descripcion VARCHAR(50))

UPDATE ReportesApp_Seguridad_EPPSxPersona
SET idEstado = 1
SELECT * FROM ReportesApp_Seguridad_EPPSxPersona

UPDATE ReportesApp_Seguridad_RegistroEPPS
SET idEstado = 2, esActivo = 'DEVUELTO'
WHERE esActivo = 'ENTREGADO'
SELECT * FROM ReportesApp_Seguridad_RegistroEPPS

UPDATE ReportesApp_Seguridad_RegistroEPPS
SET idEstado = 3
WHERE esActivo = 'VENCIDO'
SELECT * FROM ReportesApp_Seguridad_RegistroEPPS


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Garcia
-- Create date: 24-03-2023
-- Description:	Registra - Modifica - cambia de estado al tipo EPPS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_RegistrarTipoEPPS] 
@Nombre NVARCHAR(50),
@TipoEPPS INT,
@Estado BIT,
@Accion INT
AS	
DECLARE @exito VARCHAR(MAX)	

BEGIN TRAN
BEGIN TRY	
-------------------------------------------------------------------------------
IF @Accion = 1 --Modificar Nombre y Estado
BEGIN
	UPDATE ReportesApp_Seguridad_TiposEPPS
	SET Estado = @Estado, Nombre = @Nombre
	WHERE TipoEPPS = @TipoEPPS 

	SET @exito = '0 = Tipo de EPP Modificado.'
END
---------------------------------------------------------------------------------	
IF @Accion = 2 --Anular el TipoEPPS
BEGIN
	UPDATE ReportesApp_Seguridad_TiposEPPS 
	SET Estado = 0
	WHERE TipoEPPS = @TipoEPPS
		
	SET @exito = '0 = Tipo de EPP Anulado.'
END
---------------------------------------------------------------------------------
IF @Accion = 3 --Editar el estado del TipoEPPS
BEGIN
	UPDATE ReportesApp_Seguridad_TiposEPPS 
	SET Estado = 1
	WHERE TipoEPPS = @TipoEPPS
		
	SET @exito = '0 = Tipo de EPP Actualizado.'
END		
---------------------------------------------------------------------------------		
IF @Accion = 4 --Registra un nuevo TipoEPPS
BEGIN 
	IF EXISTS(SELECT * FROM ReportesApp_Seguridad_TiposEPPS WHERE Nombre = @Nombre)
	BEGIN
		SET @Exito = '-1 = El Tipo de EPP '+ @Nombre  + ' ya está registrado.'
		ROLLBACK
		GOTO Terminar
	END	
	
	INSERT ReportesApp_Seguridad_TiposEPPS(Nombre, Estado)
	VALUES (LTRIM(RTRIM(@Nombre)),1)
	
	SET @exito = '0 = TipoEPPS Registrado.'
END
----------------------------------------------------------------------------------		
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
IF LEFT(@Exito,1)='-' OR cast(left(@Exito,1) AS INT)>0 BEGIN
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
-- Create date: 12-04-2023
-- Description:	LISTAR TIPOS DE EPPS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_ListarTipoEPPS]
AS	
BEGIN
	SELECT * FROM ReportesApp_Seguridad_TiposEPPS
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Sem Chavez>
-- Create date: <24-03-2023>
-- Description:	<Listar combo de Tipos de EPPS>
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_ListarComboTiposEPPS]
AS
BEGIN
	SELECT TipoEPPS, nombre FROM ReportesApp_Seguridad_TiposEPPS
	WHERE Estado=1
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<GERARDO REYES>
-- Create date: <12-04-2023>
-- Description:	<Listar Puestos de Personal>
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_ListarPuestosPersonal]
AS
BEGIN
	SELECT CodigoPuesto, REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(Descripcion,'Á','A'),'É','E'),'Í','I'),'Ó','O'),'Ú','U') AS 'Descripcion' FROM HR_PuestoEmpresa
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<GERARDO REYES>
-- Create date: <12-04-2023>
-- Description:	<Ingresar Vida Util de EPPS>
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_IngresarVidaUtilEPPS]
@TipoEPPS INT,
@areaProceso VARCHAR(100),
@cantidadMeses INT,
@area VARCHAR(50)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Registrada Correctamente.'
	
IF (NOT EXISTS (SELECT REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(Descripcion,'Á','A'),'É','E'),'Í','I'),'Ó','O'),'Ú','U') FROM HR_PuestoEmpresa
				WHERE REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(Descripcion,'Á','A'),'É','E'),'Í','I'),'Ó','O'),'Ú','U') = @areaProceso)
	OR @TipoEPPS = 0)
BEGIN
	SET @Exito = '-1 = El valor ingresado no existe.'
	GOTO Terminar
END

IF (@TipoEPPS = (SELECT TipoEPPS FROM ReportesApp_Seguridad_VidaUtil WHERE puesto = @areaProceso AND TipoEPPS = @TipoEPPS)) BEGIN
	SET @Exito = '-2 = Este EPP ya fue asignado al área.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	INSERT INTO ReportesApp_Seguridad_VidaUtil(TipoEPPS, puesto, areaProceso, cantidadMeses)
	VALUES(@TipoEPPS, @areaProceso, @area, @cantidadMeses)
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
-- Author:		<GERARDO REYES>
-- Create date: <12-04-2023>
-- Description:	<Listar Vida Util>
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_ListarVidaUtilEPPS]
AS
BEGIN
	SELECT V.idVidaUtil AS 'Numero', T.TipoEPPS AS 'ID', V.Puesto AS 'CategoriaVidaUtil', V.AreaProceso AS 'AreaProceso', T.Nombre AS 'TipoEPP', V.cantidadMeses AS 'MesesVidaUtil'
	FROM ReportesApp_Seguridad_VidaUtil V LEFT JOIN ReportesApp_Seguridad_TiposEPPS T
	ON V.TipoEPPS = T.TipoEPPS
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<GERARDO REYES>
-- Create date: <12-04-2023>
-- Description:	<Editar Vida Util EPPS>
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_EditarVidaUtilEPPS]
@idVidaUtil INT,
@TipoEPPS INT,
@areaProceso VARCHAR(100),
@area VARCHAR(50),
@cantidadMeses INT
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Modificada Correctamente.'
	
IF (NOT EXISTS (SELECT REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(Descripcion,'Á','A'),'É','E'),'Í','I'),'Ó','O'),'Ú','U') FROM HR_PuestoEmpresa
				WHERE REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(Descripcion,'Á','A'),'É','E'),'Í','I'),'Ó','O'),'Ú','U') = @areaProceso)
	OR @TipoEPPS = 0)
BEGIN
	SET @Exito = '-1 = El valor ingresado no existe.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	UPDATE ReportesApp_Seguridad_VidaUtil
	SET TipoEPPS = @TipoEPPS,
		Puesto = @areaProceso,
		AreaProceso = @area,
		cantidadMeses = @cantidadMeses
	WHERE idVidaUtil = @idVidaUtil 
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
-- Author:		<GERARDO REYES>
-- Create date: <12-04-2023>
-- Description:	<Listar Empleados>
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_ListarEmpleados]
@filtroNombre VARCHAR(200)
AS
BEGIN
	SELECT 
		'DNI' = LTRIM(RTRIM(p.Documento)),
		'TIPOTRABAJADOR' = CASE WHEN EM.TipoTrabajador = '02' THEN 'OBRERO' ELSE 'EMPLEADO' END,
		P.persona AS 'ID',
		'NOMBRE' = LTRIM(RTRIM(p.NombreCompleto)),
		'PUESTO' = REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(PE.Descripcion,'Á','A'),'É','E'),'Í','I'),'Ó','O'),'Ú','U')
	FROM PersonaMast P LEFT JOIN  EmpleadoMast EM ON P.Persona = EM.Empleado
	LEFT JOIN HR_PuestoEmpresa PE ON PE.CodigoPuesto = EM.CodigoCargo
	WHERE EM.empleado = p.Persona AND EM.TipoTrabajador IN ('01','02') AND EM.estado='A' AND PE.Descripcion IS NOT NULL
	AND (@filtroNombre IS NULL OR p.NombreCompleto LIKE '%' + @filtroNombre + '%')
	ORDER BY EM.FechaInicioContrato DESC, EM.FechaIngreso DESC
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 25-05-2023
-- Description:	FILTRAR VIDA ÚTIL EPPS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_FiltrarVidaUtilEPPS]
@Puesto VARCHAR(MAX)
AS
BEGIN
	IF (EXISTS(SELECT TOP(1) AreaProceso FROM ReportesApp_Seguridad_VidaUtil WHERE Puesto = @Puesto)) BEGIN
		SELECT DISTINCT V.idVidaUtil AS 'Numero', T.TipoEPPS AS 'ID', V.Puesto AS 'CategoriaVidaUtil', V.AreaProceso AS 'AreaProceso', T.Nombre AS 'TipoEPP', V.cantidadMeses AS 'MesesVidaUtil'
		FROM ReportesApp_Seguridad_VidaUtil V LEFT JOIN ReportesApp_Seguridad_TiposEPPS T
		ON V.TipoEPPS = T.TipoEPPS
		WHERE V.puesto = @Puesto
	END
	ELSE BEGIN
		SELECT DISTINCT V.idVidaUtil AS 'Numero', T.TipoEPPS AS 'ID', @Puesto AS 'CategoriaVidaUtil', 'STAFF ADM' AS 'AreaProceso', T.Nombre AS 'TipoEPP', V.cantidadMeses AS 'MesesVidaUtil'
		FROM ReportesApp_Seguridad_VidaUtil V LEFT JOIN ReportesApp_Seguridad_TiposEPPS T
		ON V.TipoEPPS = T.TipoEPPS
		WHERE V.puesto = 'CONDUCTOR'
	END
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<GERARDO REYES>
-- Create date: <12-04-2023>
-- Description:	<Insertar EPPS x Persona>
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_InsertarEPPSPersonal]
@idPersonal INT,
@xml VARCHAR(MAX),
@Usuario VARCHAR(50)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

DECLARE @fechaCaducidad DATE
DECLARE @idoc INT
DECLARE @puestoEmpleado VARCHAR(250)
DECLARE @TEMP_EPPS TABLE(
		Marca BIT,
		Numero INT,
		ID INT,
		TipoEPP VARCHAR(250),
		CategoriaVidaUtil VARCHAR(250),
		AreaProceso VARCHAR(250),
		MesesVidaUtil INT)

SET @Exito = '0=EPP Asignado Correctamente.'
SET @correlativo = (SELECT COUNT(idTipoEPPS) FROM ReportesApp_Seguridad_EPPS LEFT JOIN @TEMP_EPPS E ON idTipoEPPS = E.ID WHERE E.ID = idTipoEPPS)
SET @correlativo = ISNULL(@correlativo,0)

IF (@idPersonal = 0 OR @idPersonal IS NULL)
BEGIN
	SET @Exito = '-1 = El Personal seleccionado no es válido.'
	GOTO Terminar
END

IF(@xml IS NOT NULL) BEGIN
	EXEC sp_xml_preparedocument @idoc OUTPUT, @xml
	
	INSERT INTO @TEMP_EPPS(Marca, Numero, ID, TipoEPP, CategoriaVidaUtil, AreaProceso, MesesVidaUtil)
		SELECT Marca, Numero, ID, TipoEPP, CategoriaVidaUtil, AreaProceso, MesesVidaUtil
		FROM OPENXML(@idoc,'/r/d',1)
		WITH (Marca BIT, Numero INT, ID INT, TipoEPP VARCHAR(250), CategoriaVidaUtil VARCHAR(250), AreaProceso VARCHAR(250), MesesVidaUtil INT)
END

BEGIN TRAN
BEGIN TRY
	SET @puestoEmpleado = (SELECT REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(PE.Descripcion,'Á','A'),'É','E'),'Í','I'),'Ó','O'),'Ú','U') FROM EmpleadoMast E INNER JOIN HR_PuestoEmpresa PE ON E.CodigoCargo = PE.CodigoPuesto WHERE E.Empleado = @idPersonal AND E.Estado = 'A')
		
	IF EXISTS(SELECT CategoriaVidaUtil FROM @TEMP_EPPS WHERE CategoriaVidaUtil NOT IN (@puestoEmpleado)) BEGIN
		SET @Exito = '-2=El puesto ' + (SELECT TOP 1 CategoriaVidaUtil FROM @TEMP_EPPS WHERE CategoriaVidaUtil NOT IN (@puestoEmpleado)) + ' es diferente al asignado.'
		ROLLBACK
		GOTO Terminar
	END
		
	IF NOT EXISTS (SELECT v.idVidaUtil FROM ReportesApp_Seguridad_VidaUtil v RIGHT JOIN @TEMP_EPPS p ON V.TipoEPPS = P.ID AND P.CategoriaVidaUtil = v.puesto AND p.AreaProceso = v.AreaProceso
					WHERE v.idVidaUtil IS NULL) BEGIN
		UPDATE p SET p.Numero = v.idVidaUtil FROM ReportesApp_Seguridad_VidaUtil v RIGHT JOIN @TEMP_EPPS p ON V.TipoEPPS = P.ID AND P.CategoriaVidaUtil = v.Puesto AND p.AreaProceso = v.AreaProceso 

		INSERT INTO ReportesApp_Seguridad_EPPS(idTipoEPPS, CodInterno, NombreEPPS, Estado, areaProceso, area)
		SELECT e.ID,SUBSTRING(e.TipoEPP,1,2)+'-'+convert(varchar(10),@correlativo + ROW_NUMBER() OVER(ORDER BY e.ID ASC)), e.TipoEPP, 1, e.CategoriaVidaUtil, e.AreaProceso 
		FROM @TEMP_EPPS e

		INSERT INTO ReportesApp_Seguridad_EPPSxPersona(idPersonal, idTipoEPPS, idEPPS, idVidaUtil, fechaRegistro, fechaCaducidad, diasRestantes, Puesto, AreaProceso, CodInterno, esActivo, idEstado, UsuarioCreacion, FechaCreacion, UltimoUsuario, UltimaModificacion, MesesVidaUtil)
		SELECT @idPersonal, ep.idTipoEPPS, ep.idEPPS, e.ID, GETDATE(), DATEADD(MONTH,e.MesesVidaUtil,GETDATE()), DATEDIFF(DAY,GETDATE(),DATEADD(MONTH,e.MesesVidaUtil,GETDATE())), ep.areaProceso, ep.area, ep.CodInterno, 'EN USO', 1, @Usuario, GETDATE(), @Usuario, GETDATE(), E.MesesVidaUtil
		FROM ReportesApp_Seguridad_EPPS ep INNER JOIN @TEMP_EPPS e ON ep.idTipoEPPS = e.ID AND ep.areaProceso = e.CategoriaVidaUtil  WHERE ep.esActivo IS NULL
			
		UPDATE ReportesApp_Seguridad_EPPS SET esActivo = 'EN USO' WHERE esActivo IS NULL
	END
	ELSE BEGIN
		UPDATE p SET p.Numero = v.idVidaUtil FROM ReportesApp_Seguridad_VidaUtil v RIGHT JOIN @TEMP_EPPS p ON V.TipoEPPS = P.ID AND P.CategoriaVidaUtil = 'CONDUCTOR' AND p.AreaProceso = 'OPERATIVO' 

		INSERT INTO ReportesApp_Seguridad_EPPS(idTipoEPPS, CodInterno, NombreEPPS, Estado, areaProceso, area)
		SELECT e.ID,SUBSTRING(e.TipoEPP,1,2)+'-'+convert(varchar(10),@correlativo + ROW_NUMBER() OVER(ORDER BY e.ID ASC)), e.TipoEPP, 1, e.CategoriaVidaUtil, e.AreaProceso 
		FROM @TEMP_EPPS e

		INSERT INTO ReportesApp_Seguridad_EPPSxPersona(idPersonal, idTipoEPPS, idEPPS, idVidaUtil, fechaRegistro, fechaCaducidad, diasRestantes, Puesto, AreaProceso, CodInterno, esActivo, idEstado, UsuarioCreacion, FechaCreacion, UltimoUsuario, UltimaModificacion, MesesVidaUtil)
		SELECT @idPersonal, ep.idTipoEPPS, ep.idEPPS, e.ID, GETDATE(), DATEADD(MONTH,e.MesesVidaUtil,GETDATE()), DATEDIFF(DAY,GETDATE(),DATEADD(MONTH,e.MesesVidaUtil,GETDATE())), ep.areaProceso, ep.area, ep.CodInterno, 'EN USO', 1, @Usuario, GETDATE(), @Usuario, GETDATE(), E.MesesVidaUtil
		FROM ReportesApp_Seguridad_EPPS ep INNER JOIN @TEMP_EPPS e ON ep.idTipoEPPS = e.ID AND ep.areaProceso = e.CategoriaVidaUtil  WHERE ep.esActivo IS NULL
			
		UPDATE ReportesApp_Seguridad_EPPS SET esActivo = 'EN USO' WHERE esActivo IS NULL
		/*																	
		SET @exito = '-3=Los ítems '  + (SELECT p.TipoEPP + '-' + p.CategoriaVidaUtil + '-' + p.AreaProceso 
		FROM ReportesApp_Seguridad_VidaUtil v right join @TEMP_EPPS p ON V.TipoEPPS = P.ID AND P.CategoriaVidaUtil = v.puesto and p.AreaProceso = v.AreaProceso 
		WHERE v.idVidaUtil IS NULL FOR XML PATH ('')) + ' no tienen asignado una vida útil.'
		*/
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
	IF LEFT(@Exito,1)='-' OR cast(left(@Exito,1) AS INT)<0 BEGIN
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
-- Author:		<GERARDO REYES>
-- Create date: <12-04-2023>
-- Description:	<Listar EPPS x Persona>
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_ListarEPPSxPersona]
@filtroNombre VARCHAR(200),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@idTipoEPPS INT
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	UPDATE ReportesApp_Seguridad_EPPSxPersona SET diasRestantes = DATEDIFF(DAY,GETDATE(),fechaCaducidad)
	
	UPDATE ReportesApp_Seguridad_EPPSxPersona SET idEstado = 3 WHERE diasRestantes <= 0 AND idEstado != 4
	
	INSERT INTO ReportesApp_Seguridad_RegistroEPPS(idEPPSPersonal, idPersonal, idTipoEPPS, idEPPS, idVidaUtil, fechaAsignacion, fechaCaducidad, CategoriaVidaUtil, AreaProceso, CodInterno, esActivo, idEstado, fechaDesvincular, diasRestantes, UsuarioCreacion, FechaCreacion, UltimoUsuario, UltimaModificacion, MesesVidaUtil)
    SELECT e.idEPPSPersonal, e.idPersonal, e.idTipoEPPS, e.idEPPS, e.idVidaUtil, e.fechaRegistro, e.fechaCaducidad, e.Puesto, e.AreaProceso, e.CodInterno, 'VENCIDO', 3, GETDATE(), e.diasRestantes, e.UsuarioCreacion, e.FechaCreacion, e.UltimoUsuario, e.UltimaModificacion, e.MesesVidaUtil
    FROM ReportesApp_Seguridad_EPPSxPersona e WHERE e.diasRestantes = 0 AND e.idTipoEPPS NOT IN (32, 33, 35)
    
	DELETE FROM ReportesApp_Seguridad_EPPSxPersona WHERE diasRestantes = 0 AND idTipoEPPS NOT IN (32, 33, 35)
	
	SELECT
	'idEPPSPersonal' = e.idEPPSPersonal,
	'NOMBRES' = LTRIM(RTRIM(p.NombreCompleto)),
	'CÓDIGO' = e.CodInterno,
	'EPP' = t.Nombre,
	'PUESTO' = ep.areaProceso,
    'ÁREA' = ep.area,
    'ESTADO' = ed.Descripcion,
	'VIDA ÚTIL (MESES)' = ISNULL(v.cantidadMeses, E.MesesVidaUtil),
    'FECHA ASIGNACIÓN' = e.fechaRegistro,
    'FECHA CADUCIDAD' = e.fechaCaducidad,
    'DÍAS RESTANTES' = E.diasRestantes,
    E.idEstado
	FROM ReportesApp_Seguridad_EPPSxPersona e LEFT JOIN PersonaMast p ON p.Persona = e.idPersonal
	LEFT JOIN ReportesApp_Seguridad_EPPS ep ON ep.idEPPS = e.idEPPS
	LEFT JOIN ReportesApp_Seguridad_TiposEPPS t ON t.TipoEPPS = e.idTipoEPPS
	LEFT JOIN ReportesApp_Seguridad_VidaUtil v ON v.TipoEPPS = e.idTipoEPPS AND V.puesto = E.Puesto
	LEFT JOIN ReportesApp_Seguridad_EstadoEPPS ed ON ed.idEstado = e.idEstado
	WHERE ((@filtroNombre IS NULL OR p.NombreCompleto LIKE '%'+ @filtroNombre + '%') AND (e.fechaRegistro IS NULL OR e.fechaRegistro BETWEEN @FINICIO AND @FFIN)
	AND (@idTipoEPPS = 0 OR e.idTipoEPPS = @idTipoEPPS))
	ORDER BY e.fechaRegistro DESC
END

SELECT * FROM ReportesApp_Seguridad_EPPSxPersona
SELECT * FROM ReportesApp_Seguridad_VidaUtil

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<GERARDO REYES>
-- Create date: <12-04-2023>
-- Description:	<Desvincular un EPPS x Persona>
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_EliminarEPPSxPersona]
@idEPPSPersonal INT,
@Usuario VARCHAR(50)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Desvinculado Correctamente.'
	
IF (@idEPPSPersonal = 0 OR @idEPPSPersonal IS NULL)
BEGIN
	SET @Exito = '-1 = El EPP seleccionado no existe.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	UPDATE e SET e.esActivo = 'DEVUELTO'
    FROM ReportesApp_Seguridad_EPPS e INNER JOIN ReportesApp_Seguridad_EPPSxPersona P ON e.idEPPS = P.idEPPS
    WHERE P.idEPPSPersonal = @idEPPSPersonal
    
    UPDATE ReportesApp_Seguridad_EPPSxPersona
    SET UltimoUsuario = @Usuario,
		UltimaModificacion = GETDATE()
	WHERE idEPPSPersonal = @idEPPSPersonal
			
    INSERT INTO ReportesApp_Seguridad_RegistroEPPS(idEPPSPersonal, idPersonal, idTipoEPPS, idEPPS, idVidaUtil, fechaAsignacion, fechaCaducidad, CategoriaVidaUtil, AreaProceso, CodInterno, esActivo, idEstado, fechaDesvincular, diasRestantes, UsuarioCreacion, FechaCreacion, UltimoUsuario, UltimaModificacion, MesesVidaUtil)
    SELECT idEPPSPersonal,idPersonal,idTipoEPPS,idEPPS,ISNULL(idVidaUtil,0),fechaRegistro,fechaCaducidad,Puesto,AreaProceso,CodInterno,'DEVUELTO', 2, GETDATE(), diasRestantes, UsuarioCreacion, FechaCreacion, UltimoUsuario, UltimaModificacion, MesesVidaUtil FROM ReportesApp_Seguridad_EPPSxPersona WHERE idEPPSPersonal = @idEPPSPersonal
    
	DELETE FROM ReportesApp_Seguridad_EPPSxPersona WHERE idEPPSPersonal = @idEPPSPersonal
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
-- Author:		<GERARDO REYES>
-- Create date: <25-05-2023>
-- Description:	<Suspender Temp. EPP por Vacaciones>
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_SuspenderTempEPPS]
@idEPPSPersonal INT,
@Usuario VARCHAR(50)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = EPP Suspendido Temporalmente.' 

IF (@idEPPSPersonal = 0)
BEGIN
	SET @Exito = '-1 = El EPP seleccionado no existe.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	IF((SELECT idEstado FROM ReportesApp_Seguridad_EPPSxPersona WHERE idEPPSPersonal = @idEPPSPersonal) = 1 OR (SELECT idEstado FROM ReportesApp_Seguridad_EPPSxPersona WHERE idEPPSPersonal = @idEPPSPersonal) = 3) BEGIN
		UPDATE ReportesApp_Seguridad_EPPSxPersona
		SET idEstado = 4, UltimoUsuario = @Usuario,UltimaModificacion = GETDATE()
		WHERE idEPPSPersonal = @idEPPSPersonal
	END
	ELSE BEGIN
		IF((SELECT idEstado FROM ReportesApp_Seguridad_EPPSxPersona WHERE idEPPSPersonal = @idEPPSPersonal) = 4 AND (SELECT diasRestantes FROM ReportesApp_Seguridad_EPPSxPersona WHERE idEPPSPersonal = @idEPPSPersonal) > 0) BEGIN
			UPDATE ReportesApp_Seguridad_EPPSxPersona
			SET idEstado = 1
			WHERE idEPPSPersonal = @idEPPSPersonal
		END
		ELSE BEGIN
			UPDATE ReportesApp_Seguridad_EPPSxPersona
			SET idEstado = 3
			WHERE idEPPSPersonal = @idEPPSPersonal
		END
		SET @Exito = '0 = Estado actualizado correctamente.'  
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
-- Author:		<GERARDO REYES>
-- Create date: <15-05-2023>
-- Description:	<Editar Fecha de Asignacion>
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_EditarFechaAsignacion]
@idEPPSPersonal INT,
@nuevaFecha DATE,
@Usuario VARCHAR(50)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Modificada Correctamente'
	
IF (@nuevaFecha > GETDATE())
BEGIN
	SET @Exito = '-1 = Solo puede elegir hasta el día de hoy.'
	GOTO Terminar
END

IF (@idEPPSPersonal = 0)
BEGIN
	SET @Exito = '-1 = El EPP seleccionado no existe.'
	GOTO Terminar
END

BEGIN TRAN
BEGIN TRY
	UPDATE ReportesApp_Seguridad_EPPSxPersona
	SET fechaRegistro = @nuevaFecha,
		fechaCaducidad = DATEADD(MONTH,V.cantidadMeses,@nuevaFecha),
		diasRestantes = DATEDIFF(DAY,@nuevaFecha,DATEADD(MONTH, V.cantidadMeses,GETDATE())),
		UltimoUsuario = @Usuario,
		UltimaModificacion = GETDATE()
	FROM ReportesApp_Seguridad_VidaUtil V RIGHT JOIN ReportesApp_Seguridad_EPPSxPersona P ON V.TipoEPPS = P.idTipoEPPS AND V.puesto = P.Puesto
	WHERE idEPPSPersonal = @idEPPSPersonal
	
	IF (@nuevaFecha < GETDATE())
	BEGIN
		UPDATE ReportesApp_Seguridad_EPPSxPersona SET idEstado = 1 WHERE idEPPSPersonal = @idEPPSPersonal
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
-- Author:		<GERARDO REYES>
-- Create date: <12-04-2023>
-- Description:	<Listar EPPS>
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_ListarEPPS]
@filtroNombre VARCHAR(200),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@idTipoEPPS INT
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	SELECT EP.CodInterno, EP.idTipoEPPS, LTRIM(RTRIM(P.NombreCompleto)) AS 'NOMBRES',
		   EP.CategoriaVidaUtil AS 'CATEGORÍA VIDA ÚTIL', EP.AreaProceso AS 'ÁREA PROCESO',
		   T.Nombre AS 'EPP', ED.Descripcion AS 'ESTADO', 'VIDA ÚTIL (MESES)' = ISNULL(v.cantidadMeses, EP.MesesVidaUtil),
		   EP.fechaAsignacion AS 'FECHA ASIGNACIÓN', EP.fechaDesvincular AS 'FECHA DESVINCULACIÓN',
		   EP.fechaCaducidad AS 'FECHA CADUCIDAD', EP.diasRestantes AS 'DÍAS RESTANTES',
		   EP.UsuarioCreacion AS 'USUARIO CREACIÓN', EP.FechaCreacion AS 'FECHA CREACIÓN',
		   EP.UltimoUsuario AS 'ÚLTIMO USUARIO', EP.UltimaModificacion AS 'ÚLTIMA MODIFICACIÓN'
	FROM ReportesApp_Seguridad_RegistroEPPS EP
	LEFT JOIN ReportesApp_Seguridad_TiposEPPS T ON EP.idTipoEPPS = T.TipoEPPS
	LEFT JOIN PersonaMast p ON p.Persona = EP.idPersonal
	LEFT JOIN ReportesApp_Seguridad_EstadoEPPS ED ON ED.idEstado = EP.idEstado
	LEFT JOIN ReportesApp_Seguridad_VidaUtil v ON v.TipoEPPS = EP.idTipoEPPS AND V.puesto = EP.CategoriaVidaUtil
	WHERE ((@filtroNombre IS NULL OR p.NombreCompleto LIKE '%'+ @filtroNombre + '%') AND (EP.fechaDesvincular IS NULL OR EP.fechaDesvincular BETWEEN @FINICIO AND @FFIN)
	AND (@idTipoEPPS = 0 OR EP.idTipoEPPS = @idTipoEPPS))
	ORDER BY EP.fechaDesvincular DESC
END

------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 26-03-2024
-- Description:	GENERAR CORREO DE EPPS POR VENCER
-- =============================================
/*
EXEC ReportesApp_Seguridad_RegistroEPPS_CorreoEPPSPorVencer 
*/
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_RegistroEPPS_CorreoEPPSPorVencer]
AS
BEGIN
	DECLARE @Mensaje AS VARCHAR(MAX), @Asunto VARCHAR(300), @Aviso AS VARCHAR(300)
	DECLARE @PorVencer1 VARCHAR(MAX) = ''
	DECLARE @PorVencer2 VARCHAR(MAX) = ''

	SELECT @PorVencer1 = @PorVencer1 + '<tr>'
						   + '<td style="background-color: #FFFACD">' + X.NOMBRE + '</td>'
						   + '<td style="background-color: #FFFACD">' + X.AREA + '</td>'
						   + '<td style="background-color: #FFFACD">' + X.PUESTO + '</td>'
						   + '<td style="background-color: #FFFACD">' + X.EPP + '</td>'
						   + '<td style="background-color: #FFFACD">' + X.FECHA_ASIGNACION + '</td>'
						   + '<td style="background-color: #FFFACD">' + X.FECHA_CADUCIDAD + '</td>'
						   + '<td style="background-color: #FFFACD">' + X.DIAS_RESTANTES + '</td>'
						   + '</tr>' 
	FROM (SELECT ROW_NUMBER() OVER (PARTITION BY P.NombreCompleto, T.Nombre ORDER BY E.diasRestantes DESC) AS 'NRO',
	LTRIM(RTRIM(P.NombreCompleto)) AS 'NOMBRE', EP.area AS 'AREA', EP.areaProceso AS 'PUESTO', T.Nombre AS 'EPP',
	CONVERT(CHAR(10),E.fechaRegistro,103) AS 'FECHA_ASIGNACION', CONVERT(CHAR(10),E.fechaCaducidad,103) AS 'FECHA_CADUCIDAD',
	CONVERT(VARCHAR,E.diasRestantes) AS 'DIAS_RESTANTES'
	FROM ReportesApp_Seguridad_EPPSxPersona E
	LEFT JOIN PersonaMast P ON P.Persona = E.idPersonal
	LEFT JOIN EmpleadoMast EM ON P.Persona = EM.Empleado 
	LEFT JOIN ReportesApp_Seguridad_EPPS EP ON EP.idEPPS = E.idEPPS
	LEFT JOIN ReportesApp_Seguridad_TiposEPPS T ON T.TipoEPPS = E.idTipoEPPS
	WHERE (EM.estado='A') AND YEAR(E.fechaRegistro) >= 2023) X
	WHERE X.NRO = 1 AND (X.DIAS_RESTANTES <= 30 AND X.DIAS_RESTANTES > 15)

	SELECT @PorVencer2 = @PorVencer2 + '<tr>'
						   + '<td style="background-color: #FFCBD1">' + X.NOMBRE + '</td>'
						   + '<td style="background-color: #FFCBD1">' + X.AREA + '</td>'
						   + '<td style="background-color: #FFCBD1">' + X.PUESTO + '</td>'
						   + '<td style="background-color: #FFCBD1">' + X.EPP + '</td>'
						   + '<td style="background-color: #FFCBD1">' + X.FECHA_ASIGNACION + '</td>'
						   + '<td style="background-color: #FFCBD1">' + X.FECHA_CADUCIDAD + '</td>'
						   + '<td style="background-color: #FFCBD1">' + X.DIAS_RESTANTES + '</td>'
						   + '</tr>' 
	FROM (SELECT ROW_NUMBER() OVER (PARTITION BY P.NombreCompleto, T.Nombre ORDER BY E.diasRestantes DESC) AS 'NRO',
	LTRIM(RTRIM(P.NombreCompleto)) AS 'NOMBRE', EP.area AS 'AREA', EP.areaProceso AS 'PUESTO', T.Nombre AS 'EPP',
	CONVERT(CHAR(10),E.fechaRegistro,103) AS 'FECHA_ASIGNACION', CONVERT(CHAR(10),E.fechaCaducidad,103) AS 'FECHA_CADUCIDAD',
	CONVERT(VARCHAR,E.diasRestantes) AS 'DIAS_RESTANTES'
	FROM ReportesApp_Seguridad_EPPSxPersona E
	LEFT JOIN PersonaMast P ON P.Persona = E.idPersonal
	LEFT JOIN EmpleadoMast EM ON P.Persona = EM.Empleado 
	LEFT JOIN ReportesApp_Seguridad_EPPS EP ON EP.idEPPS = E.idEPPS
	LEFT JOIN ReportesApp_Seguridad_TiposEPPS T ON T.TipoEPPS = E.idTipoEPPS
	WHERE (EM.estado='A') AND YEAR(E.fechaRegistro) >= 2023) X
	WHERE X.NRO = 1 AND X.DIAS_RESTANTES <= 15

	SET @Asunto = 'AVISO DE EPPS POR VENCER'

	SET @Mensaje = '<p><h2>LISTA DE EPPS PRÓXIMOS A VENCER</h2></p>'
				  +'<p>Estos son los EPPS prestados al personal que ya están cerca de su fecha de caducidad y tienen que ser devueltos: </p>'
				  + '<p><table border="1" cellspacing="1" cellpadding="1" >
					    <tr>
						    <td align="center" style="background-color:orange"><b>NOMBRE</b></td>
							<td align="center" style="background-color:orange"><b>ÁREA</b></td>
							<td align="center" style="background-color:orange"><b>PUESTO</b></td>
							<td align="center" style="background-color:orange"><b>EPP</b></td>
							<td align="center" style="background-color:orange"><b>FECHA DE ASIGNACIÓN</b></td>
							<td align="center" style="background-color:orange"><b>FECHA DE CADUCIDAD</b></td>
							<td align="center" style="background-color:orange"><b>DÍAS RESTANTES</b></td>
						</tr>
						<tbody>'
						+ ISNULL(@PorVencer2, '')
						+ ISNULL(@PorVencer1, '') 	
						+'</tbody>
						</table>
					</p>'
				  +'<p>Fecha de envío: '+CONVERT(CHAR(10),GETDATE(),103)+RIGHT(RTRIM(CONVERT(CHAR(26),GETDATE(),22)),12)+'</p>'
				  +'<BR>'
				  +'<p><font face="verdana" size="2" color="DarkRed">Tecnologías de la Información - GRUPO TRANSPESA</font></p>'
	
	EXEC msdb.dbo.sp_send_dbmail 
		 @profile_name='AVISODESISTEMA', -- REAL: @profile_name = 'AVISODESISTEMA',  -- PRUEBAS: @profile_name = 'AVISODELSISTEMA',
		 @recipients = 'seguridad3@transpesa.com.pe; seguridad1@transpesa.com.pe',
		 --@blind_copy_recipients = 'asistenteauditoria@transpesa.com.pe;jrojas@transpesa.com.pe;desarrollo@transpesa.com.pe;desarrollo2@transpesa.com.pe;' 
		 @subject = @Asunto,
		 @body_format = 'HTML',
		 @body = @Mensaje	
END

SELECT * FROM [msdb].[dbo].[sysmail_allitems]
ORDER BY [send_request_date] DESC