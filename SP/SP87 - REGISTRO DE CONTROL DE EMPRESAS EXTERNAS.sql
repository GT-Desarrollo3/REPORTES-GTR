
-- CREAR TABLA ReportesApp_Seguridad_PersonalExterno_Nombres

-- CREAR TABLA ReportesApp_Seguridad_PersonalExterno_Registro

---------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 06-10-2025
-- Description:	INSERTAR DATOS EXTERNOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_PersonalExterno_InsertarEliminarNombres]
@Opcion INT,
@Nro INT,
@Apellidos VARCHAR(250),
@Nombres VARCHAR(250),
@DNI VARCHAR(250)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- INSERTAR NOMBRES
		SET @correlativo = (SELECT MAX(Nro) FROM ReportesApp_Seguridad_PersonalExterno_Nombres WHERE idRegistroExt IS NULL)
		SET @correlativo = ISNULL(@correlativo,0) + 1

		INSERT INTO ReportesApp_Seguridad_PersonalExterno_Nombres(Nro,Apellidos,Nombres,DNI)
		VALUES (@correlativo,@Apellidos,@Nombres,@DNI)

		SET @Exito = '0 = Nombres registrados correctamente.'
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR NOMBRES
		DELETE FROM ReportesApp_Seguridad_PersonalExterno_Nombres
		WHERE idRegistroExt IS NULL AND Nro = @Nro

		UPDATE ReportesApp_Seguridad_PersonalExterno_Nombres
		SET Nro = Nro - 1
		WHERE idRegistroExt IS NULL AND Nro > @Nro 

		SET @Exito = '0 = Nombres eliminados correctamente.'
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
-- Create date: 06-10-2025
-- Description:	LISTAR PERSONAL EXTERNO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_PersonalExterno_ListarNombres]
AS
BEGIN
	SELECT Nro AS 'NRO', Apellidos AS 'APELLIDOS', Nombres AS 'NOMBRES', DNI
	FROM ReportesApp_Seguridad_PersonalExterno_Nombres
	WHERE idRegistroExt IS NULL
	ORDER BY Nro DESC
END

-------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 07-10-2025
-- Description:	INSERTAR REGISTRO EXTERNO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_PersonalExterno_RegistrarExterno]
@Opcion INT,
@EmpresaExt VARCHAR(500),
@Archivo VARBINARY(MAX),
@TituloArchivo VARCHAR(500),
@ExtensionArchivo VARCHAR(20),
@PersonaResp INT,
@Area VARCHAR(100),
@Motivo VARCHAR(MAX),
@FechaIngreso DATETIME,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- REGISTRAR EXTERNO
		SET @correlativo = (SELECT MAX(idRegistroExt) FROM ReportesApp_Seguridad_PersonalExterno_Registro)
		SET @correlativo = ISNULL(@correlativo,0) + 1
		
		INSERT INTO ReportesApp_Seguridad_PersonalExterno_Registro(idRegistroExt,EmpresaExt,Archivo,TituloArchivo,ExtensionArchivo,Validacion,
		PersonaResp,Area,Motivo,FechaIngreso,UsuarioCrea,FechaCrea,UsuarioModifica,FechaModifica)
		VALUES (@correlativo,@EmpresaExt,@Archivo,@TituloArchivo,@ExtensionArchivo,'NO',@PersonaResp,@Area,@Motivo,@FechaIngreso,@Usuario,GETDATE(),@Usuario,GETDATE())
		
		UPDATE ReportesApp_Seguridad_PersonalExterno_Nombres
		SET idRegistroExt = @correlativo
		WHERE idRegistroExt IS NULL

		SET @Exito = '0 = Personal externo registrado.'
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

-----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 16-10-2025
-- Description:	LISTAR REGISTROS EXTERNO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_PersonalExterno_ListarExterno]
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@Area VARCHAR(50),
@PersonalExt VARCHAR(250),
@EmpresaExt VARCHAR(250)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@Area = 'TODAS') BEGIN
		SELECT * FROM
		(SELECT R.idRegistroExt AS 'NRO', CONVERT(VARCHAR,R.FechaEntrada,103)+' '+CONVERT(VARCHAR,R.FechaEntrada,8) AS 'FECHA_ENTRADA',
		CONVERT(VARCHAR,R.FechaSalida,103)+' '+CONVERT(VARCHAR,R.FechaSalida,8) AS 'FECHA_SALIDA', R.EmpresaExt AS 'EMPRESA_EXT',
		(LTRIM(RTRIM(STUFF((SELECT ', ' + (CONVERT(VARCHAR,Nombres) + ' ' + CONVERT(VARCHAR,Apellidos) + ' (' + CONVERT(VARCHAR,DNI) + ')')
		FROM ReportesApp_Seguridad_PersonalExterno_Nombres WHERE idRegistroExt = R.idRegistroExt ORDER BY Nro ASC FOR XML PATH ('')),1,1,'')))) AS 'LISTA_PERSONAS',
		R.TituloArchivo AS 'DOCUMENTO', R.ExtensionArchivo AS 'EXTENSION', R.PersonaResp, RTRIM(P.NombreCompleto) AS 'RESPONSABLE',
		R.Area AS 'AREA', R.Motivo AS 'MOTIVO', R.FechaIngreso AS 'FECHA_INGRESO', R.Validacion AS 'VALIDACION', R.UsuarioCrea, R.FechaCrea,
		R.UsuarioModifica, R.FechaModifica
		FROM ReportesApp_Seguridad_PersonalExterno_Registro R
		LEFT JOIN PersonaMast P ON P.Persona = R.PersonaResp) X
		WHERE (X.LISTA_PERSONAS IS NULL OR X.LISTA_PERSONAS LIKE '%' + @PersonalExt + '%') AND (X.EMPRESA_EXT IS NULL OR X.EMPRESA_EXT LIKE '%' + @EmpresaExt + '%')
		AND (X.FechaCrea BETWEEN @FINICIO AND @FFIN) 
	END
	ELSE BEGIN
		SELECT * FROM
		(SELECT R.idRegistroExt AS 'NRO', CONVERT(VARCHAR,R.FechaEntrada,103)+' '+CONVERT(VARCHAR,R.FechaEntrada,8) AS 'FECHA_ENTRADA',
		CONVERT(VARCHAR,R.FechaSalida,103)+' '+CONVERT(VARCHAR,R.FechaSalida,8) AS 'FECHA_SALIDA', R.EmpresaExt AS 'EMPRESA_EXT',
		(LTRIM(RTRIM(STUFF((SELECT ', ' + (CONVERT(VARCHAR,Nombres) + ' ' + CONVERT(VARCHAR,Apellidos) + ' (' + CONVERT(VARCHAR,DNI) + ')')
		FROM ReportesApp_Seguridad_PersonalExterno_Nombres WHERE idRegistroExt = R.idRegistroExt ORDER BY Nro ASC FOR XML PATH ('')),1,1,'')))) AS 'LISTA_PERSONAS',
		R.TituloArchivo AS 'DOCUMENTO', R.ExtensionArchivo AS 'EXTENSION', R.PersonaResp, RTRIM(P.NombreCompleto) AS 'RESPONSABLE',
		R.Area AS 'AREA', R.Motivo AS 'MOTIVO', R.FechaIngreso AS 'FECHA_INGRESO', R.Validacion AS 'VALIDACION', R.UsuarioCrea, R.FechaCrea,
		R.UsuarioModifica, R.FechaModifica
		FROM ReportesApp_Seguridad_PersonalExterno_Registro R
		LEFT JOIN PersonaMast P ON P.Persona = R.PersonaResp) X
		WHERE (X.LISTA_PERSONAS IS NULL OR X.LISTA_PERSONAS LIKE '%' + @PersonalExt + '%') AND (X.EMPRESA_EXT IS NULL OR X.EMPRESA_EXT LIKE '%' + @EmpresaExt + '%')
		AND (X.FechaCrea BETWEEN @FINICIO AND @FFIN) AND (X.AREA = @Area)
	END
END

-----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 16-10-2025
-- Description:	BUSCAR DOCUMENTOS EXTERNOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_PersonalExterno_BuscarDocumentos]
@Opcion INT,
@idRegistroExt INT,
@Usuario VARCHAR(20)
AS
BEGIN
	IF (@Opcion = 1) BEGIN		-- BUSCAR DOCUMENTO
		SELECT Archivo, ExtensionArchivo FROM ReportesApp_Seguridad_PersonalExterno_Registro
		WHERE idRegistroExt = @idRegistroExt
	END

	IF (@Opcion = 2) BEGIN		-- VALIDAR ACCESO 
		DECLARE @Exito VARCHAR(MAX) = '0 = Ingreso aprobado.'
		
		UPDATE ReportesApp_Seguridad_PersonalExterno_Registro
		SET Validacion = 'SÍ', UsuarioModifica = @Usuario, FechaModifica = GETDATE()
		WHERE idRegistroExt = @idRegistroExt

		SELECT @Exito exito
	END

	IF (@Opcion = 3) BEGIN		-- ELIMINAR ACCESO 
		DECLARE @Exito2 VARCHAR(MAX) = '0 = Ingreso eliminado.'
		
		DELETE FROM ReportesApp_Seguridad_PersonalExterno_Registro
		WHERE idRegistroExt = @idRegistroExt

		DELETE FROM ReportesApp_Seguridad_PersonalExterno_Nombres
		WHERE idRegistroExt = @idRegistroExt

		SELECT @Exito2 exito
	END
END

-----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 17-10-2025
-- Description:	INGRESAR TIEMPOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Seguridad_PersonalExterno_IngresarTiempos]
@idRegistroExt INT,
@FechaIngreso DATETIME,
@FechaSalida DATETIME,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

BEGIN TRAN
BEGIN TRY
	/*
	IF (@FechaSalida <= @FechaIngreso) BEGIN	
		SET @Exito = '-1 = La fecha de salida no puede ser menor a la fecha de ingreso.'
		ROLLBACK
		GOTO Terminar
	END
	*/

	IF (CONVERT(VARCHAR,@FechaIngreso,8) = '00:00:00') BEGIN
		SET @FechaIngreso = NULL
	END

	IF (CONVERT(VARCHAR,@FechaSalida,8) = '00:00:00') BEGIN
		SET @FechaSalida = NULL
	END

	UPDATE ReportesApp_Seguridad_PersonalExterno_Registro
	SET FechaEntrada = @FechaIngreso, FechaSalida = @FechaSalida, UsuarioModifica = @Usuario, FechaModifica = GETDATE()
	WHERE idRegistroExt = @idRegistroExt

	SET @Exito = '0 = Fechas registradas correctamente.'
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

-----------------------------------------------------------------------------

SELECT * FROM ReportesApp_Seguridad_PersonalExterno_Registro

SELECT * FROM ReportesApp_Seguridad_PersonalExterno_Nombres
