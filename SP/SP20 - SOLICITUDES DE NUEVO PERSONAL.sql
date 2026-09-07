
-- UPDATE Usuario SET Estado = 'A' WHERE Usuario = 'DEMO'

-- CREAR TABLA ReportesApp_RRHH_SolicitudesPersonal_Areas Y LLENARLA

-- CREAR TABLA ReportesApp_RRHH_SolicitudesPersonal_TipoSolicitud Y LLENARLA

-- CREAR TABLA ReportesApp_RRHH_SolicitudesPersonal_EstadoSolicitud Y LLENARLA

-- CREAR TABLA ReportesApp_RRHH_SolicitudesPersonal_Registro

-- CREAR TABLA ReportesApp_RRHH_SolicitudesPersonal_Candidatos

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 20-10-2023
-- Description:	LISTAR TABLAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_RRHH_SolicitudesPersonal_ListarTablas]
@Opcion INT
AS
BEGIN
	IF (@Opcion = 1) BEGIN  -- LISTAR ÁREAS - SELECCIONAR
		SELECT CodAreaSpring, Nombre FROM ReportesApp_RRHH_SolicitudesPersonal_Areas
		WHERE CodAreaSpring != 0
	END

	IF (@Opcion = 2) BEGIN  -- LISTAR ÁREAS - BUSCAR
		SELECT CodAreaSpring, Nombre FROM ReportesApp_RRHH_SolicitudesPersonal_Areas
	END

	IF (@Opcion = 3) BEGIN  -- LISTAR TIPOS DE SOLICITUD
		SELECT idTipo, NombreTipo FROM ReportesApp_RRHH_SolicitudesPersonal_TipoSolicitud
	END

	IF (@Opcion = 4) BEGIN  -- LISTAR ESTADO - SELECCIONAR
		SELECT idEstadoSolicitud, NombreEstado FROM ReportesApp_RRHH_SolicitudesPersonal_EstadoSolicitud
		WHERE idEstadoSolicitud IN (3,4)
	END

	IF (@Opcion = 5) BEGIN  -- LISTAR ESTADO - BUSCAR
		SELECT idEstadoSolicitud, NombreEstado FROM ReportesApp_RRHH_SolicitudesPersonal_EstadoSolicitud
		WHERE idEstadoSolicitud != 0
	END
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 21/10/2023
-- Description:	REGISTRAR SOLICITUDES DE PERSONAL
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_RRHH_SolicitudesPersonal_RegistrarSolicitudPersonal]
@CodAreaSpring INT,
@CodigoPuesto INT,
@NroVacantes INT,
@idTipo INT,
@CodReemplazo INT,
@Observacion VARCHAR(300),
@Usuario VARCHAR(20)
AS
DECLARE @exito VARCHAR(MAX)
DECLARE @correlativo INT

SET @exito = '0 = Solicitud registrada.'

SET @correlativo = (SELECT MAX(idSolicitudPersonal) FROM ReportesApp_RRHH_SolicitudesPersonal_Registro)
SET @correlativo = ISNULL(@correlativo,0) + 1 

BEGIN TRAN
BEGIN TRY
	IF (@idTipo = 1) BEGIN
		INSERT INTO ReportesApp_RRHH_SolicitudesPersonal_Registro(idSolicitudPersonal,CodAreaSpring,CodigoPuesto,NroVacantes,idTipo,Observacion,FechaSolicitud,idEstadoSolicitud,UsuarioRegistra)
		VALUES(@correlativo,@CodAreaSpring,@CodigoPuesto,@NroVacantes,@idTipo,@Observacion,GETDATE(),2,@Usuario)
	END
	ELSE BEGIN
		INSERT INTO ReportesApp_RRHH_SolicitudesPersonal_Registro(idSolicitudPersonal,CodAreaSpring,CodigoPuesto,NroVacantes,idTipo,CodReemplazo,Observacion,FechaSolicitud,idEstadoSolicitud,UsuarioRegistra)
		VALUES(@correlativo,@CodAreaSpring,@CodigoPuesto,@NroVacantes,@idTipo,@CodReemplazo,@Observacion,GETDATE(),2,@Usuario)
	END

	DECLARE @Mensaje AS VARCHAR(MAX),@Asunto VARCHAR(300),@destinatarios VARCHAR(500)
	DECLARE @Area VARCHAR(150), @NombreUsuario VARCHAR(300), @NombreTipo VARCHAR(150), @NombrePuesto VARCHAR(300)

	SET @Area = (SELECT Nombre FROM ReportesApp_RRHH_SolicitudesPersonal_Areas WHERE CodAreaSpring = @CodAreaSpring)
	SET @NombreUsuario = (SELECT U.Nombre FROM Usuario U LEFT JOIN EmpleadoMast E ON U.Usuario = E.CodigoUsuario WHERE U.Usuario = @Usuario)
	SET @NombreTipo = (SELECT NombreTipo FROM ReportesApp_RRHH_SolicitudesPersonal_TipoSolicitud WHERE idTipo = @idTipo)
	SET @NombrePuesto = (SELECT REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(Descripcion,'Á','A'),'É','E'),'Í','I'),'Ó','O'),'Ú','U') FROM HR_PuestoEmpresa WHERE CodigoPuesto = @CodigoPuesto)
				
	-- GENERAR CORREO
	SET @Asunto='SOLICITUD DE PERSONAL'
	SET @Mensaje =  '<p><h2>SOLICITUD DE PERSONAL</h2></p>'
					+'<p>Buenos días, estimado(a).</p>'
					+'<p>El encargado del área de '
					+'<b>'+@Area+'</b>'+', '
					+'<b>'+@NombreUsuario+'</b>'
					+' ha solicitado un '
					+'<b>'+@NombreTipo+'</b>'
					+' para el cargo de '
					+'<b>'+@NombrePuesto+'</b>'
					+'.'+'</p>'
					+'<p>Para más detalle, revisar la lista de solicitudes.</p>'
					+'<p>Fecha de envío de la solicitud: '+CONVERT(CHAR(10),GETDATE(),103)+RIGHT(RTRIM(CONVERT(CHAR(26),GETDATE(),22)),12)+'</p>'
					+'<BR>'
					+'<p><font face="verdana" size="2" color="DarkRed">Tecnologías de la Información - GRUPO TRANSPESA</font></p>'

	SET @destinatarios = 'talentohumano@transpesa.com.pe; seleccion@transpesa.com.pe'				

	EXEC msdb.dbo.sp_send_dbmail 
	@profile_name='AVISODESISTEMA', -- REAL: @profile_name = 'AVISODESISTEMA',  -- PRUEBAS: @profile_name = 'AVISODELSISTEMA',
	@recipients='desarrollo2@transpesa.com.pe',
	--@blind_copy_recipients='desarrollo2@transpesa.com.pe', 
	@subject=@Asunto,
	@body_format = 'HTML', 
	@body=@Mensaje
END TRY

BEGIN CATCH
	SET @exito = RTRIM(LTRIM(STR(ERROR_NUMBER()))) + '=' + ERROR_MESSAGE()
	ROLLBACK
	GOTO Terminar		
END CATCH

IF @@TRANCOUNT > 0
	COMMIT;

TERMINAR:
IF LEFT(@Exito,1)='-' OR cast(left(@Exito,1) as int)>0 BEGIN
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
-- Create date: 23/10/2023
-- Description:	LISTAR SOLICITUDES DE PERSONAL
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_RRHH_SolicitudesPersonal_ListarSolicitudesPersonal]
@CodAreaSpring INT,
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@idEstadoSolicitud INT
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	UPDATE R 
	SET R.DiasRestantes = DATEDIFF(DAY,GETDATE(),R.FechaEntrega)
	FROM ReportesApp_RRHH_SolicitudesPersonal_Registro R
	WHERE R.idEstadoSolicitud = 5

	IF (@CodAreaSpring = 0 AND @idEstadoSolicitud = 1) BEGIN
		SELECT S.idSolicitudPersonal AS 'Codigo', E.NombreEstado AS 'Estado', S.CodAreaSpring, A.Nombre AS 'Area', S.idTipo, T.NombreTipo AS 'Solicitud',
		S.CodigoPuesto, P.Descripcion AS 'Puesto', CONVERT(VARCHAR,S.FechaSolicitud,103) AS 'FechaSolicitud', CONVERT(VARCHAR,S.FechaAprobacion,103) AS 'FechaAprobacion',
		CONVERT(VARCHAR,S.FechaRegistros,103)+' '+RIGHT(RTRIM(CONVERT(VARCHAR,S.FechaRegistros,8)),12) AS 'FechaRegistroCandidatos',
		CONVERT(VARCHAR,S.FechaEntrega,103) AS 'FechaEntrega', S.DiasRestantes, S.FechaTermino, S.UsuarioRegistra, S.FechaSolicitud AS 'FechaRegistra', S.UsuarioModifica, S.FechaModifica
		FROM ReportesApp_RRHH_SolicitudesPersonal_Registro S
		LEFT JOIN ReportesApp_RRHH_SolicitudesPersonal_Areas A WITH(NOLOCK) ON A.CodAreaSpring = S.CodAreaSpring
		LEFT JOIN HR_PuestoEmpresa P WITH(NOLOCK) ON P.CodigoPuesto = S.CodigoPuesto
		LEFT JOIN ReportesApp_RRHH_SolicitudesPersonal_TipoSolicitud T WITH(NOLOCK) ON T.idTipo = S.idTipo
		LEFT JOIN ReportesApp_RRHH_SolicitudesPersonal_EstadoSolicitud E WITH(NOLOCK) ON E.idEstadoSolicitud = S.idEstadoSolicitud
		WHERE (S.FechaSolicitud IS NULL OR S.FechaSolicitud BETWEEN @FINICIO AND @FFIN)
		ORDER BY S.idSolicitudPersonal DESC
	END
	ELSE BEGIN
		IF (@CodAreaSpring = 0) BEGIN
			SELECT S.idSolicitudPersonal AS 'Codigo', E.NombreEstado AS 'Estado', S.CodAreaSpring, A.Nombre AS 'Area', S.idTipo, T.NombreTipo AS 'Solicitud',
			S.CodigoPuesto, P.Descripcion AS 'Puesto', CONVERT(VARCHAR,S.FechaSolicitud,103) AS 'FechaSolicitud', CONVERT(VARCHAR,S.FechaAprobacion,103) AS 'FechaAprobacion',
			CONVERT(VARCHAR,S.FechaRegistros,103)+' '+RIGHT(RTRIM(CONVERT(VARCHAR,S.FechaRegistros,8)),12) AS 'FechaRegistroCandidatos',
			CONVERT(VARCHAR,S.FechaEntrega,103) AS 'FechaEntrega', S.DiasRestantes, S.FechaTermino, S.UsuarioRegistra, S.FechaSolicitud AS 'FechaRegistra', S.UsuarioModifica, S.FechaModifica
			FROM ReportesApp_RRHH_SolicitudesPersonal_Registro S
			LEFT JOIN ReportesApp_RRHH_SolicitudesPersonal_Areas A WITH(NOLOCK) ON A.CodAreaSpring = S.CodAreaSpring
			LEFT JOIN HR_PuestoEmpresa P WITH(NOLOCK) ON P.CodigoPuesto = S.CodigoPuesto
			LEFT JOIN ReportesApp_RRHH_SolicitudesPersonal_TipoSolicitud T WITH(NOLOCK) ON T.idTipo = S.idTipo
			LEFT JOIN ReportesApp_RRHH_SolicitudesPersonal_EstadoSolicitud E WITH(NOLOCK) ON E.idEstadoSolicitud = S.idEstadoSolicitud
			WHERE (S.idEstadoSolicitud = @idEstadoSolicitud) AND (S.FechaSolicitud IS NULL OR S.FechaSolicitud BETWEEN @FINICIO AND @FFIN)
			ORDER BY S.idSolicitudPersonal DESC
		END
		ELSE BEGIN
			SELECT S.idSolicitudPersonal AS 'Codigo', E.NombreEstado AS 'Estado', S.CodAreaSpring, A.Nombre AS 'Area', S.idTipo, T.NombreTipo AS 'Solicitud',
			S.CodigoPuesto, P.Descripcion AS 'Puesto', CONVERT(VARCHAR,S.FechaSolicitud,103) AS 'FechaSolicitud', CONVERT(VARCHAR,S.FechaAprobacion,103) AS 'FechaAprobacion',
			CONVERT(VARCHAR,S.FechaRegistros,103)+' '+RIGHT(RTRIM(CONVERT(VARCHAR,S.FechaRegistros,8)),12) AS 'FechaRegistroCandidatos',
			CONVERT(VARCHAR,S.FechaEntrega,103) AS 'FechaEntrega', S.DiasRestantes, S.FechaTermino, S.UsuarioRegistra, S.FechaSolicitud AS 'FechaRegistra', S.UsuarioModifica, S.FechaModifica
			FROM ReportesApp_RRHH_SolicitudesPersonal_Registro S
			LEFT JOIN ReportesApp_RRHH_SolicitudesPersonal_Areas A WITH(NOLOCK) ON A.CodAreaSpring = S.CodAreaSpring
			LEFT JOIN HR_PuestoEmpresa P WITH(NOLOCK) ON P.CodigoPuesto = S.CodigoPuesto
			LEFT JOIN ReportesApp_RRHH_SolicitudesPersonal_TipoSolicitud T WITH(NOLOCK) ON T.idTipo = S.idTipo
			LEFT JOIN ReportesApp_RRHH_SolicitudesPersonal_EstadoSolicitud E WITH(NOLOCK) ON E.idEstadoSolicitud = S.idEstadoSolicitud
			WHERE (S.CodAreaSpring = @CodAreaSpring) AND (S.FechaSolicitud IS NULL OR S.FechaSolicitud BETWEEN @FINICIO AND @FFIN)
			ORDER BY S.idSolicitudPersonal DESC
		END
	END
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 23/10/2023
-- Description:	LISTAR SOLICITUD DE PERSONAL
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_RRHH_SolicitudesPersonal_ListarSolicitud]
@idSolicitudPersonal INT
AS
BEGIN
	SELECT A.Nombre AS 'Area',P.Descripcion AS 'Puesto',S.NroVacantes,T.NombreTipo AS 'Solicitud',RTRIM(LTRIM(H.NombreCompleto)) AS 'Personal',
	S.Observacion,E.NombreEstado AS 'Estado',ISNULL(FechaEntrega,GETDATE()) AS 'FechaEntrega'
	FROM ReportesApp_RRHH_SolicitudesPersonal_Registro S
	LEFT JOIN ReportesApp_RRHH_SolicitudesPersonal_Areas A WITH(NOLOCK) ON A.CodAreaSpring = S.CodAreaSpring
	LEFT JOIN HR_PuestoEmpresa P WITH(NOLOCK) ON P.CodigoPuesto = S.CodigoPuesto
	LEFT JOIN ReportesApp_RRHH_SolicitudesPersonal_TipoSolicitud T WITH(NOLOCK) ON T.idTipo = S.idTipo
	LEFT JOIN ReportesApp_RRHH_SolicitudesPersonal_EstadoSolicitud E WITH(NOLOCK) ON E.idEstadoSolicitud = S.idEstadoSolicitud
	LEFT JOIN PersonaMast H WITH(NOLOCK) ON H.Persona = S.CodReemplazo
	WHERE idSolicitudPersonal = @idSolicitudPersonal
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 23/10/2023
-- Description:	LISTAR SOLICITUD DE PERSONAL
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_RRHH_SolicitudesPersonal_EditarSolicitud]
@Opcion INT,
@idSolicitudPersonal INT,
@idEstadoSolicitud INT,
@FechaEntrega DATETIME,
@Usuario VARCHAR(20)
AS
DECLARE @exito VARCHAR(MAX)
SET @exito = '0 = Solicitud.'

BEGIN TRAN
BEGIN TRY
	DECLARE @Mensaje AS VARCHAR(MAX),@Asunto VARCHAR(300),@destinatarios VARCHAR(500)
	DECLARE @Area VARCHAR(150), @Estado VARCHAR(300), @NombreTipo VARCHAR(150), @NombrePuesto VARCHAR(300), @CorreoArea VARCHAR(300)

	SET @Area = (SELECT Nombre FROM ReportesApp_RRHH_SolicitudesPersonal_Areas
				WHERE CodAreaSpring = (SELECT CodAreaSpring FROM ReportesApp_RRHH_SolicitudesPersonal_Registro WHERE idSolicitudPersonal = @idSolicitudPersonal))
	SET @CorreoArea = (SELECT CorreoArea FROM ReportesApp_RRHH_SolicitudesPersonal_Areas
				WHERE CodAreaSpring = (SELECT CodAreaSpring FROM ReportesApp_RRHH_SolicitudesPersonal_Registro WHERE idSolicitudPersonal = @idSolicitudPersonal))
	SET @NombreTipo = (SELECT NombreTipo FROM ReportesApp_RRHH_SolicitudesPersonal_TipoSolicitud
					   WHERE idTipo = (SELECT idTipo FROM ReportesApp_RRHH_SolicitudesPersonal_Registro WHERE idSolicitudPersonal = @idSolicitudPersonal))
	SET @NombrePuesto = (SELECT REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(Descripcion,'Á','A'),'É','E'),'Í','I'),'Ó','O'),'Ú','U') FROM HR_PuestoEmpresa
						WHERE CodigoPuesto = (SELECT CodigoPuesto FROM ReportesApp_RRHH_SolicitudesPersonal_Registro WHERE idSolicitudPersonal = @idSolicitudPersonal))

	IF (@Opcion = 1) BEGIN
		UPDATE ReportesApp_RRHH_SolicitudesPersonal_Registro
		SET idEstadoSolicitud = @idEstadoSolicitud, FechaAprobacion = GETDATE(), UsuarioModifica = @Usuario, FechaModifica = GETDATE()
		WHERE idSolicitudPersonal = @idSolicitudPersonal

		-- GENERAR CORREO
		SET @Estado = (SELECT NombreEstado FROM ReportesApp_RRHH_SolicitudesPersonal_EstadoSolicitud
		   		  WHERE idEstadoSolicitud = (SELECT idEstadoSolicitud FROM ReportesApp_RRHH_SolicitudesPersonal_Registro WHERE idSolicitudPersonal = @idSolicitudPersonal))

		SET @Asunto='ACTUALIZACIÓN DE SOLICITUD'
		SET @Mensaje = '<p><h2>SOLICITUD DE PERSONAL</h2></p>'
					   +'<p>Buenos días, estimado(a).</p>'
					   +'<p>Se ha evaluado la solicitud del área de '
					   +'<b>'+@Area+'</b>'
			    	   +' para un '
					   +'<b>'+@NombreTipo+'</b>'
					   +' en el cargo de '
					   +'<b>'+@NombrePuesto+'</b>'
					   +'.'+'</p>'
					   +'<p>Dicha solicitud ha sido '
					   +'<b>'+@Estado+'</b>'
					   +' por el área de RR.HH.</p>'
					   +'<p>Fecha de aprobación de la solicitud: '+CONVERT(CHAR(10),GETDATE(),103)+RIGHT(RTRIM(CONVERT(CHAR(26),GETDATE(),22)),12)+'</p>'
					   +'<BR>'
					   +'<p><font face="verdana" size="2" color="DarkRed">Tecnologías de la Información - GRUPO TRANSPESA</font></p>'

		SET @destinatarios = 'seleccion@transpesa.com.pe; ' + @CorreoArea				

		EXEC msdb.dbo.sp_send_dbmail 
		@profile_name='AVISODESISTEMA', -- REAL: @profile_name = 'AVISODESISTEMA',  -- PRUEBAS: @profile_name = 'AVISODELSISTEMA',
		@recipients='desarrollo2@transpesa.com.pe',
		--@blind_copy_recipients='desarrollo2@transpesa.com.pe', 
		@subject=@Asunto,
		@body_format = 'HTML', 
		@body=@Mensaje

		SET @exito = '0 = Estado actualizado.'
	END
	
	IF (@Opcion = 2) BEGIN
		IF (@FechaEntrega < GETDATE()) BEGIN
			SET @Exito = '-1 = La Fecha ingresada es menor a la actual.'
			ROLLBACK
			GOTO Terminar
		END
		ELSE BEGIN
			UPDATE ReportesApp_RRHH_SolicitudesPersonal_Registro
			SET FechaEntrega = @FechaEntrega, DiasRestantes = DATEDIFF(DAY, GETDATE(), @FechaEntrega), idEstadoSolicitud = 5, UsuarioModifica = @Usuario, FechaModifica = GETDATE()
			WHERE idSolicitudPersonal = @idSolicitudPersonal

			-- GENERAR CORREO
			DECLARE @TablaHTML VARCHAR(MAX)=''
			DECLARE @TablaCabHTML VARCHAR(MAX)='', @TablaCuerpoHTML VARCHAR(MAX)=''

			SET @TablaCabHTML =
				'<TABLE BORDER=''1''>'
				+'<TR>'
				+'<TD style="background-color:red; color: white">N°</TD>'
				+'<TD style="background-color:red; color: white">PUESTO</TD>'
				+'<TD style="background-color:red; color: white">SOLICITUD</TD>'
				+'<TD style="background-color:red; color: white">FECHA DE APROBACIÓN</TD>'
				+'<TD style="background-color:red; color: white">FECHA DE ENTREGA</TD></TR>'

			SET @TablaCuerpoHTML = @TablaCuerpoHTML + (SELECT '<TR>'
								   +'<TD style="background-color: #BBBBBB">'+CONVERT(VARCHAR,idSolicitudPersonal)+'</TD>'
								   +'<TD style="background-color: #BBBBBB">'+@NombrePuesto+'</TD>'
								   +'<TD style="background-color: #BBBBBB">'+@NombreTipo+'</TD>'
								   +'<TD style="background-color: #BBBBBB">'+CONVERT(VARCHAR,FechaAprobacion,103)+'</TD>'
								   +'<TD style="background-color: #BBBBBB">'+CONVERT(VARCHAR,@FechaEntrega,103)+'</TD>'
								   +'</TR>'
								   FROM ReportesApp_RRHH_SolicitudesPersonal_Registro
								   WHERE idSolicitudPersonal = @idSolicitudPersonal)

			SET @TablaHTML = @TablaHTML + @TablaCabHTML + @TablaCuerpoHTML+'</TABLE>'

			SET @Asunto='ATENCIÓN DE SOLICITUD DE PERSONAL'
			SET @Mensaje = '<p><h2>SOLICITUD DE PERSONAL</h2></p>'
						  +'<p>Buenos días, estimado(a).</p>'
						  +'<p>El área de RR.HH. le informa que la solicitud de personal del área de '
						  +'<b>'+@Area+'</b>'
					      +' será atendida según el siguiente cronograma: '
						  +'<BR>'
						  +'<p>'+@TablaHTML+'</p>'
					      +'<p>Fecha de envío: '+CONVERT(CHAR(10),GETDATE(),103)+RIGHT(RTRIM(CONVERT(CHAR(26),GETDATE(),22)),12)+'</p>'
					      +'<BR>'
					      +'<p><font face="verdana" size="2" color="DarkRed">Tecnologías de la Información - GRUPO TRANSPESA</font></p>'

			SET @destinatarios = @CorreoArea + '; talentohumano@transpesa.com.pe; enrique.pesantes@transpesa.pe; seleccion@transpesa.com.pe'				

			EXEC msdb.dbo.sp_send_dbmail 
			@profile_name='AVISODESISTEMA', -- REAL: @profile_name = 'AVISODESISTEMA',  -- PRUEBAS: @profile_name = 'AVISODELSISTEMA',
			@recipients='desarrollo2@transpesa.com.pe',
			--@blind_copy_recipients='desarrollo2@transpesa.com.pe', 
			@subject=@Asunto,
			@body_format = 'HTML', 
			@body=@Mensaje

			SET @exito = '0 = Solicitud actualizada.'
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

TERMINAR:
IF LEFT(@Exito,1)='-' OR cast(left(@Exito,1) as int)>0 BEGIN
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
-- Create date: 24-10-2023
-- Description:	AÑADIR, MODIFICAR Y ELIMINAR CANDIDATOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_RRHH_SolicitudesPersonal_AgregaModificaCandidato]
@Accion INT,
@idCandidato INT,
@idSolicitudPersonal INT,
@DNI VARCHAR(10),
@Nombre VARCHAR(250),
@FechaEntrevista DATE,
@Estado VARCHAR(30),
@Observacion VARCHAR(250),
@Usuario VARCHAR(20)
AS	
DECLARE @exito VARCHAR(MAX)
BEGIN TRAN
BEGIN TRY
	IF (@Accion = 1) BEGIN   -- AGREGAR NUEVO CANDIDATO
		DECLARE @correlativo INT 
		SET @correlativo = (SELECT MAX(idCandidato) FROM ReportesApp_RRHH_SolicitudesPersonal_Candidatos WHERE idSolicitudPersonal = @idSolicitudPersonal)
		SET @correlativo = ISNULL(@correlativo,0) + 1

		INSERT INTO ReportesApp_RRHH_SolicitudesPersonal_Candidatos(idCandidato,idSolicitudPersonal,Estado,UsuarioRegistra,FechaRegistra)
		VALUES (@correlativo,@idSolicitudPersonal,' ',@Usuario,GETDATE())

		UPDATE ReportesApp_RRHH_SolicitudesPersonal_Registro
		SET FechaRegistros = GETDATE()
		WHERE idSolicitudPersonal = @idSolicitudPersonal

		SET @exito = '0 = Candidato Registrado'
	END

	IF (@Accion = 2) BEGIN   -- MODIFICAR CANDIDATO
		IF (@FechaEntrevista < GETDATE()) BEGIN
			SET @Exito = '-1 = No puede elegir una fecha pasada.'
			ROLLBACK
			GOTO Terminar
		END
		ELSE BEGIN
			UPDATE ReportesApp_RRHH_SolicitudesPersonal_Candidatos
			SET DNI = @DNI, Nombre = @Nombre, FechaEntrevista = @FechaEntrevista, Observacion = @Observacion, UsuarioModifica = @Usuario, FechaModifica = GETDATE()
			WHERE idCandidato = @idCandidato AND idSolicitudPersonal = @idSolicitudPersonal
		END
		
		SET @exito = '0 = Candidato(s) Registrado(s)'
	END
	
	IF (@Accion = 3) BEGIN   -- ELIMINAR CANDIDATO  
		DELETE ReportesApp_RRHH_SolicitudesPersonal_Candidatos 
		WHERE idCandidato = @idCandidato AND idSolicitudPersonal = @idSolicitudPersonal

		UPDATE ReportesApp_RRHH_SolicitudesPersonal_Candidatos
		SET idCandidato = idCandidato - 1
		WHERE idCandidato > @idCandidato AND idSolicitudPersonal = @idSolicitudPersonal	
		
		SET @exito = '0 = Candidato Removido'
	END
	
	IF (@Accion = 4) BEGIN   -- SELECCIONAR CANDIDATO  
		DECLARE @Contador INT, @NroVacantes INT

		SET @NroVacantes = (SELECT NroVacantes FROM ReportesApp_RRHH_SolicitudesPersonal_Registro WHERE idSolicitudPersonal = @idSolicitudPersonal)

		UPDATE ReportesApp_RRHH_SolicitudesPersonal_Candidatos
		SET Estado = 'SELECCIONADO', UsuarioModifica = @Usuario, FechaModifica = GETDATE()
		WHERE idCandidato = @idCandidato AND idSolicitudPersonal = @idSolicitudPersonal
		
		UPDATE ReportesApp_RRHH_SolicitudesPersonal_Candidatos
		SET Estado = ' '
		WHERE idCandidato != @idCandidato AND idSolicitudPersonal = @idSolicitudPersonal

		SET @Contador = (SELECT COUNT(idCandidato) FROM ReportesApp_RRHH_SolicitudesPersonal_Candidatos WHERE idSolicitudPersonal = @idSolicitudPersonal AND Estado = 'SELECCIONADO')
		
		IF(@Contador < @NroVacantes) BEGIN
			UPDATE ReportesApp_RRHH_SolicitudesPersonal_Registro
			SET idEstadoSolicitud = 5, UsuarioModifica = @Usuario, FechaModifica = GETDATE()
			WHERE idSolicitudPersonal = @idSolicitudPersonal
		END
		ELSE BEGIN
			IF(@Contador >= @NroVacantes) BEGIN
				UPDATE ReportesApp_RRHH_SolicitudesPersonal_Registro
				SET FechaTermino = GETDATE(), idEstadoSolicitud = 6, UsuarioModifica = @Usuario, FechaModifica = GETDATE()
				WHERE idSolicitudPersonal = @idSolicitudPersonal
			END
		END

		-- GENERAR CORREO
		DECLARE @Mensaje AS VARCHAR(MAX),@Asunto VARCHAR(300),@destinatarios VARCHAR(500)
		DECLARE @NombreCandidato VARCHAR(200), @NombreTipo VARCHAR(200), @NombrePuesto VARCHAR(200), @CorreoArea VARCHAR(300)

		SET @NombreCandidato = (SELECT Nombre FROM ReportesApp_RRHH_SolicitudesPersonal_Candidatos WHERE idCandidato = @idCandidato AND idSolicitudPersonal = @idSolicitudPersonal)
		SET @NombreTipo = (SELECT NombreTipo FROM ReportesApp_RRHH_SolicitudesPersonal_TipoSolicitud
					   WHERE idTipo = (SELECT idTipo FROM ReportesApp_RRHH_SolicitudesPersonal_Registro WHERE idSolicitudPersonal = @idSolicitudPersonal))
		SET @NombrePuesto = (SELECT REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(Descripcion,'Á','A'),'É','E'),'Í','I'),'Ó','O'),'Ú','U') FROM HR_PuestoEmpresa
						WHERE CodigoPuesto = (SELECT CodigoPuesto FROM ReportesApp_RRHH_SolicitudesPersonal_Registro WHERE idSolicitudPersonal = @idSolicitudPersonal))
		SET @CorreoArea = (SELECT CorreoArea FROM ReportesApp_RRHH_SolicitudesPersonal_Areas
						WHERE CodAreaSpring = (SELECT CodAreaSpring FROM ReportesApp_RRHH_SolicitudesPersonal_Registro WHERE idSolicitudPersonal = @idSolicitudPersonal))

		SET @Asunto='ACTUALIZACIÓN DE SOLICITUD DE PERSONAL'
		SET @Mensaje = '<p><h2>SELECCIÓN DE CANDIDATO PARA SOLICITUD</h2></p>'
					   +'<p>Buenos días, estimado(a)s.</p>'
					   +'<p>Se ha seleccionado al candidato '
					   +'<b>'+@NombreCandidato+'</b>'
			    	   +' para la solicitud de '
					   +'<b>'+@NombreTipo+'</b>'
					   +' en el cargo de '
					   +'<b>'+@NombrePuesto+'</b>'
					   +'.'+'</p>'
					   +'<p>Fecha y hora de envío: '+CONVERT(CHAR(10),GETDATE(),103)+RIGHT(RTRIM(CONVERT(CHAR(26),GETDATE(),22)),12)+'</p>'
					   +'<BR>'
					   +'<p><font face="verdana" size="2" color="DarkRed">Tecnologías de la Información - GRUPO TRANSPESA</font></p>'

		SET @destinatarios = @CorreoArea + '; talentohumano@transpesa.com.pe; enrique.pesantes@transpesa.pe; seleccion@transpesa.com.pe'

		EXEC msdb.dbo.sp_send_dbmail 
		@profile_name='AVISODESISTEMA', -- REAL: @profile_name = 'AVISODESISTEMA',  -- PRUEBAS: @profile_name = 'AVISODELSISTEMA',
		@recipients='desarrollo2@transpesa.com.pe',
		--@blind_copy_recipients='desarrollo2@transpesa.com.pe', 
		@subject=@Asunto,
		@body_format = 'HTML', 
		@body=@Mensaje

		SET @exito = '0 = '
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

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 24/10/2023
-- Description:	LISTAR CANDIDATOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_RRHH_SolicitudesPersonal_ListarCandidatos]
@idSolicitudPersonal INT
AS
BEGIN
	SELECT idCandidato AS 'N°', Estado, idSolicitudPersonal, DNI, Nombre, CONVERT(VARCHAR,FechaRegistra,103) AS 'FechaRegistro', FechaEntrevista, Observacion
	FROM ReportesApp_RRHH_SolicitudesPersonal_Candidatos
	WHERE idSolicitudPersonal = @idSolicitudPersonal
END