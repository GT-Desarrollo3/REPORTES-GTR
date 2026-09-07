
-- UPDATE Usuario SET Estado = 'A' WHERE Usuario = 'DEMO'

-- CREAR TABLA ReportesApp_RRHH_ControlUniformes_Uniformes Y LLENARLA

-- CREAR TABLA ReportesApp_RRHH_ControlUniformes_VidaUtil

-- CREAR TABLA ReportesApp_RRHH_ControlUniformes_Registro

-- CREAR TABLA ReportesApp_RRHH_ControlUniformes_Historial

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 27-10-2023
-- Description:	LISTAR UNIFORMES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_RRHH_ControlUniformes_ListarUniformes]
@Opcion INT
AS
BEGIN
	IF (@Opcion = 1) BEGIN  -- LISTAR UNIFORMES - SELECCIONAR
		SELECT idUniforme, NombreUniforme FROM ReportesApp_RRHH_ControlUniformes_Uniformes
		WHERE idUniforme != 0
	END

	IF (@Opcion = 2) BEGIN  -- LISTAR UNIFORMES - BUSCAR
		SELECT idUniforme, NombreUniforme FROM ReportesApp_RRHH_ControlUniformes_Uniformes
	END
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 27-10-2023
-- Description:	LISTAR EMPLEADOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_RRHH_ControlUniformes_ListarEmpleados]
@filtroNombre VARCHAR(200)
AS
BEGIN
	SELECT P.persona AS 'ID', LTRIM(RTRIM(p.NombreCompleto)) AS 'NOMBRE', D.department, D.description AS 'AREA',
	PE.CodigoPuesto, REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(PE.Descripcion,'Á','A'),'É','E'),'Í','I'),'Ó','O'),'Ú','U') AS 'PUESTO'
	FROM PersonaMast P LEFT JOIN EmpleadoMast EM ON P.Persona = EM.Empleado
	LEFT JOIN HR_PuestoEmpresa PE ON PE.CodigoPuesto = EM.CodigoCargo
	LEFT JOIN departmentmst D ON D.department = EM.DeptoOrganizacion
	WHERE EM.empleado = p.Persona AND EM.estado='A' AND D.department IS NOT NULL AND PE.CodigoPuesto IS NOT NULL
	AND (@filtroNombre IS NULL OR p.NombreCompleto LIKE '%' + @filtroNombre + '%')
	ORDER BY p.NombreCompleto
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 27-10-2023
-- Description:	LISTAR EMPLEADOS POR PUESTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_RRHH_ControlUniformes_ListarEmpleadosPuesto]
@filtroPuesto VARCHAR(200)
AS
BEGIN
	SELECT P.persona AS 'ID', LTRIM(RTRIM(p.NombreCompleto)) AS 'NOMBRE', D.department, D.description AS 'AREA',
	PE.CodigoPuesto, REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(PE.Descripcion,'Á','A'),'É','E'),'Í','I'),'Ó','O'),'Ú','U') AS 'PUESTO'
	FROM PersonaMast P LEFT JOIN EmpleadoMast EM ON P.Persona = EM.Empleado
	LEFT JOIN HR_PuestoEmpresa PE ON PE.CodigoPuesto = EM.CodigoCargo
	LEFT JOIN departmentmst D ON D.department = EM.DeptoOrganizacion
	WHERE EM.empleado = p.Persona AND EM.estado='A' AND D.department IS NOT NULL AND PE.CodigoPuesto IS NOT NULL
	AND (@filtroPuesto IS NULL OR PE.Descripcion LIKE '%' + @filtroPuesto + '%')
	ORDER BY p.NombreCompleto
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 28/10/2023
-- Description:	LISTAR VIDA UTIL DE UNIFORME POR AREA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_RRHH_ControlUniformes_ListarVidaUtil]
@idArea CHAR(3),
@idCargo INT,
@idUniforme INT
AS
BEGIN
	SELECT ISNULL(VidaUtil,0) AS VidaUtil FROM ReportesApp_RRHH_ControlUniformes_VidaUtil
	WHERE idArea = @idArea AND idCargo = @idCargo AND idUniforme = @idUniforme
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 27/10/2023
-- Description:	ASIGNAR UNIFORMES AL PERSONAL
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_RRHH_ControlUniformes_AsignarUniformes]
@idUniforme INT,
@idPersonal INT,
@idArea CHAR(3),
@idCargo INT,
@VidaUtil INT,
@Talla VARCHAR(10),
@Cantidad INT,
@Usuario VARCHAR(20)
AS
DECLARE @exito VARCHAR(MAX)
DECLARE @correlativo INT
DECLARE @correlativo2 INT

SET @exito = '0 = Uniforme asignado al personal.'

BEGIN TRAN
BEGIN TRY
	SET @correlativo = (SELECT MAX(idAsignarUniforme) FROM ReportesApp_RRHH_ControlUniformes_Registro)
	SET @correlativo = ISNULL(@correlativo,0) + 1
	SET @correlativo2 = (SELECT MAX(idVidaUtil) FROM ReportesApp_RRHH_ControlUniformes_VidaUtil)
	SET @correlativo2 = ISNULL(@correlativo2,0) + 1

	IF (EXISTS(SELECT * FROM ReportesApp_RRHH_ControlUniformes_VidaUtil WHERE idArea = @idArea AND idCargo = @idCargo AND idUniforme = @idUniforme)) BEGIN
		UPDATE ReportesApp_RRHH_ControlUniformes_VidaUtil
		SET VidaUtil = @VidaUtil
		WHERE idArea = @idArea AND idCargo = @idCargo AND idUniforme = @idUniforme
	END
	ELSE BEGIN
		INSERT INTO ReportesApp_RRHH_ControlUniformes_VidaUtil (idVidaUtil, idUniforme, idArea, idCargo, VidaUtil)
		VALUES (@correlativo2, @idUniforme, @idArea, @idCargo, @VidaUtil)
	END

	DECLARE @FechaCaducidad DATETIME
	DECLARE @DiasRestantes INT

	SET @FechaCaducidad = DATEADD(MONTH,@VidaUtil,GETDATE())
	SET @DiasRestantes = DATEDIFF(DAY,GETDATE(),@FechaCaducidad)

	INSERT INTO ReportesApp_RRHH_ControlUniformes_Registro (idAsignarUniforme,idUniforme,VidaUtil,idPersonal,Talla,Estado,FechaAsignacion,FechaCaducidad,DiasRestantes,Cantidad,UsuarioCreacion)
	VALUES (@correlativo, @idUniforme, @VidaUtil, @idPersonal, @Talla, 'EN USO', GETDATE(), @FechaCaducidad, @DiasRestantes, @Cantidad, @Usuario)
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
-- Create date: 31-10-2023
-- Description:	LISTAR REGISTRO DE UNIFORMES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_RRHH_ControlUniformes_ListarRegistros]
@Personal VARCHAR(200),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@idUniforme INT
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	UPDATE R 
	SET R.DiasRestantes = DATEDIFF(DAY,GETDATE(),R.FechaCaducidad)
	FROM ReportesApp_RRHH_ControlUniformes_Registro R
	WHERE R.Estado = 'EN USO'

	IF (@idUniforme = 0) BEGIN
		SELECT ROW_NUMBER() OVER(ORDER BY AU.idAsignarUniforme ASC) AS '#', AU.idAsignarUniforme, AU.Estado, AU.idPersonal, LTRIM(RTRIM(P.NombreCompleto)) AS 'Personal',
		D.department, D.description AS 'Área', PE.CodigoPuesto, REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(PE.Descripcion,'Á','A'),'É','E'),'Í','I'),'Ó','O'),'Ú','U') AS 'Puesto',
		AU.idUniforme, U.NombreUniforme AS 'Uniforme', AU.Talla, AU.Cantidad, AU.VidaUtil AS 'Meses', CONVERT(VARCHAR,AU.FechaAsignacion,103) AS 'FechaAsignación', CONVERT(VARCHAR,AU.FechaCaducidad,103)
		AS 'FechaCaducidad', AU.DiasRestantes
		FROM ReportesApp_RRHH_ControlUniformes_Registro AU
		LEFT JOIN ReportesApp_RRHH_ControlUniformes_Uniformes U ON U.idUniforme = AU.idUniforme
		LEFT JOIN PersonaMast P ON P.Persona = AU.idPersonal
		LEFT JOIN EmpleadoMast EM ON P.Persona = EM.Empleado
		LEFT JOIN HR_PuestoEmpresa PE ON PE.CodigoPuesto = EM.CodigoCargo
		LEFT JOIN departmentmst D ON D.department = EM.DeptoOrganizacion
		WHERE (@Personal IS NULL OR P.NombreCompleto LIKE '%' + @Personal + '%') AND (AU.FechaAsignacion IS NULL OR AU.FechaAsignacion BETWEEN @FINICIO AND @FFIN)
		AND (AU.Estado = 'EN USO')
		ORDER BY AU.idAsignarUniforme DESC
	END
	ELSE BEGIN
		SELECT ROW_NUMBER() OVER(ORDER BY AU.idAsignarUniforme ASC) AS '#', AU.idAsignarUniforme, AU.Estado, AU.idPersonal, LTRIM(RTRIM(P.NombreCompleto)) AS 'Personal',
		D.department, D.description AS 'Área', PE.CodigoPuesto, REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(PE.Descripcion,'Á','A'),'É','E'),'Í','I'),'Ó','O'),'Ú','U') AS 'Puesto',
		AU.idUniforme, U.NombreUniforme AS 'Uniforme', AU.Talla, AU.Cantidad, AU.VidaUtil AS 'Meses', CONVERT(VARCHAR,AU.FechaAsignacion,103) AS 'FechaAsignación', CONVERT(VARCHAR,AU.FechaCaducidad,103)
		AS 'FechaCaducidad', AU.DiasRestantes
		FROM ReportesApp_RRHH_ControlUniformes_Registro AU
		LEFT JOIN ReportesApp_RRHH_ControlUniformes_Uniformes U ON U.idUniforme = AU.idUniforme
		LEFT JOIN PersonaMast P ON P.Persona = AU.idPersonal
		LEFT JOIN EmpleadoMast EM ON P.Persona = EM.Empleado
		LEFT JOIN HR_PuestoEmpresa PE ON PE.CodigoPuesto = EM.CodigoCargo
		LEFT JOIN departmentmst D ON D.department = EM.DeptoOrganizacion
		WHERE (@Personal IS NULL OR P.NombreCompleto LIKE '%' + @Personal + '%') AND (AU.FechaAsignacion IS NULL OR AU.FechaAsignacion BETWEEN @FINICIO AND @FFIN)
		AND (@idUniforme = AU.idUniforme) AND (AU.Estado = 'EN USO')
		ORDER BY AU.idAsignarUniforme DESC
	END
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 02-11-2023
-- Description:	EDITAR FECHA DE ENTREGA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_RRHH_ControlUniformes_EditarUniformeAsignado]
@Opcion INT,
@idAsignarUniforme INT,
@VidaUtil INT,
@NuevaFecha DATE,
@Cantidad INT,
@Usuario VARCHAR(50)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

SET @Exito = '0 = '

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- MODIFICAR FECHA DE ENTREGA
		IF (@nuevaFecha > GETDATE()) BEGIN
			SET @Exito = '-1 = Solo puede elegir hasta el día de hoy.'
			ROLLBACK
			GOTO Terminar
		END

		UPDATE ReportesApp_RRHH_ControlUniformes_Registro
		SET FechaAsignacion = @NuevaFecha,
			FechaCaducidad = DATEADD(MONTH,@VidaUtil,@NuevaFecha),
			DiasRestantes = DATEDIFF(DAY,@NuevaFecha,DATEADD(MONTH,@VidaUtil,GETDATE())),
			UsuarioModificacion = @Usuario,
			FechaModificacion = GETDATE()
		WHERE idAsignarUniforme = @idAsignarUniforme

		SET @Exito = '0 = Fecha de Entrega Modificada.'
	END

	IF (@Opcion = 2) BEGIN		-- MODIFICAR ESTADO
		DECLARE @CantidadTotal INT = (SELECT Cantidad FROM ReportesApp_RRHH_ControlUniformes_Registro WHERE idAsignarUniforme = @idAsignarUniforme)
		DECLARE @DiasRestantes INT = (SELECT DiasRestantes FROM ReportesApp_RRHH_ControlUniformes_Registro WHERE idAsignarUniforme = @idAsignarUniforme)

		IF (@Cantidad > @CantidadTotal) BEGIN
			SET @Exito = '-1 = No puede devolver una cantidad mayor a la registrada. Ingresar otra vez.'
			ROLLBACK
			GOTO Terminar
		END

		IF (@Cantidad <= @CantidadTotal) BEGIN
			SET @correlativo = (SELECT MAX(idHistorial) FROM ReportesApp_RRHH_ControlUniformes_Historial)
			SET @correlativo = ISNULL(@correlativo,0) + 1

			INSERT INTO ReportesApp_RRHH_ControlUniformes_Historial
			VALUES(@correlativo,@idAsignarUniforme,@Cantidad,'DEVUELTO',@DiasRestantes,@Usuario,GETDATE())
			
			IF (@Cantidad = @CantidadTotal) BEGIN
				UPDATE ReportesApp_RRHH_ControlUniformes_Registro
				SET Estado = 'DEVUELTO', UsuarioModificacion = @Usuario, Cantidad = Cantidad - @Cantidad, FechaModificacion = GETDATE()
				WHERE idAsignarUniforme = @idAsignarUniforme
			END
			ELSE BEGIN
				UPDATE ReportesApp_RRHH_ControlUniformes_Registro
				SET UsuarioModificacion = @Usuario, Cantidad = Cantidad - @Cantidad, FechaModificacion = GETDATE()
				WHERE idAsignarUniforme = @idAsignarUniforme
			END

			SET @Exito = '0 = Uniforme devuelto. Revisar Historial.'
		END
	END

	IF (@Opcion = 3) BEGIN		-- ELIMINAR
		DELETE FROM ReportesApp_RRHH_ControlUniformes_Registro
		WHERE idAsignarUniforme = @idAsignarUniforme

		SET @Exito = '0 = Eliminado correctamente.'
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
-- Create date: 03-11-2023
-- Description:	LISTAR HISTORIAL DE REGISTROS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_RRHH_ControlUniformes_ListarHistorial]
@Personal VARCHAR(200),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@idUniforme INT
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@idUniforme = 0) BEGIN
		SELECT ROW_NUMBER() OVER(ORDER BY AU.idAsignarUniforme ASC) AS '#', AU.idAsignarUniforme, H.Estado, AU.idPersonal, LTRIM(RTRIM(P.NombreCompleto)) AS 'Personal',
		D.department, D.description AS 'Área', PE.CodigoPuesto, REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(PE.Descripcion,'Á','A'),'É','E'),'Í','I'),'Ó','O'),'Ú','U') AS 'Puesto',
		AU.idUniforme, U.NombreUniforme AS 'Uniforme', AU.Talla, H.Cantidad, AU.VidaUtil AS 'Meses', CONVERT(VARCHAR,AU.FechaAsignacion,103) AS 'FechaAsignación',
		CONVERT(VARCHAR,H.FechaDevolucion,103) AS 'FechaDevolución', CONVERT(VARCHAR,AU.FechaCaducidad,103) AS 'FechaCaducidad', H.DiasRestantes, H.UsuarioDevolucion
		FROM ReportesApp_RRHH_ControlUniformes_Historial H
		LEFT JOIN ReportesApp_RRHH_ControlUniformes_Registro AU ON AU.idAsignarUniforme = H.idAsignarUniforme
		LEFT JOIN ReportesApp_RRHH_ControlUniformes_Uniformes U ON U.idUniforme = AU.idUniforme
		LEFT JOIN PersonaMast P ON P.Persona = AU.idPersonal
		LEFT JOIN EmpleadoMast EM ON P.Persona = EM.Empleado
		LEFT JOIN HR_PuestoEmpresa PE ON PE.CodigoPuesto = EM.CodigoCargo
		LEFT JOIN departmentmst D ON D.department = EM.DeptoOrganizacion
		WHERE (@Personal IS NULL OR P.NombreCompleto LIKE '%' + @Personal + '%') AND (AU.FechaModificacion IS NULL OR AU.FechaModificacion BETWEEN @FINICIO AND @FFIN)
		ORDER BY AU.idAsignarUniforme DESC
	END
	ELSE BEGIN
		SELECT ROW_NUMBER() OVER(ORDER BY AU.idAsignarUniforme ASC) AS '#', AU.idAsignarUniforme, H.Estado, AU.idPersonal, LTRIM(RTRIM(P.NombreCompleto)) AS 'Personal',
		D.department, D.description AS 'Área', PE.CodigoPuesto, REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(PE.Descripcion,'Á','A'),'É','E'),'Í','I'),'Ó','O'),'Ú','U') AS 'Puesto',
		AU.idUniforme, U.NombreUniforme AS 'Uniforme', AU.Talla, H.Cantidad, AU.VidaUtil AS 'Meses', CONVERT(VARCHAR,AU.FechaAsignacion,103) AS 'FechaAsignación',
		CONVERT(VARCHAR,AU.FechaModificacion,103) AS 'FechaDevolución', CONVERT(VARCHAR,AU.FechaCaducidad,103) AS 'FechaCaducidad', H.DiasRestantes, H.UsuarioDevolucion
		FROM ReportesApp_RRHH_ControlUniformes_Historial H
		LEFT JOIN ReportesApp_RRHH_ControlUniformes_Registro AU ON AU.idAsignarUniforme = H.idAsignarUniforme
		LEFT JOIN ReportesApp_RRHH_ControlUniformes_Uniformes U ON U.idUniforme = AU.idUniforme
		LEFT JOIN PersonaMast P ON P.Persona = AU.idPersonal
		LEFT JOIN EmpleadoMast EM ON P.Persona = EM.Empleado
		LEFT JOIN HR_PuestoEmpresa PE ON PE.CodigoPuesto = EM.CodigoCargo
		LEFT JOIN departmentmst D ON D.department = EM.DeptoOrganizacion
		WHERE (@Personal IS NULL OR P.NombreCompleto LIKE '%' + @Personal + '%') AND (AU.FechaModificacion IS NULL OR AU.FechaModificacion BETWEEN @FINICIO AND @FFIN)
		AND (@idUniforme = AU.idUniforme)
		ORDER BY AU.idAsignarUniforme DESC
	END
END

-----------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 03-11-2023
-- Description:	GENERAR LISTA DE ASIGNACIONES POR VENCER
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_RRHH_ControlUniformes_CorreoUniformesPendientes]
AS
BEGIN
	DECLARE @Mensaje AS VARCHAR(MAX), @Asunto VARCHAR(300), @Aviso AS VARCHAR(300)
	DECLARE @Asignaciones VARCHAR(MAX) = ''

	SELECT @Asignaciones = @Asignaciones + '<tr>'
						   + '<td style="background-color: white">' + LTRIM(RTRIM(P.NombreCompleto)) + '</td>'
						   + '<td style="background-color: white">' + D.description + '</td>'
						   + '<td style="background-color: white">' + REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(PE.Descripcion,'Á','A'),'É','E'),'Í','I'),'Ó','O'),'Ú','U') + '</td>'
						   + '<td style="background-color: white">' + U.NombreUniforme + '</td>'
						   + '<td style="background-color: white">' + CONVERT(CHAR(10),AU.FechaAsignacion,103) + '</td>'
						   + '<td style="background-color: white">' + CONVERT(CHAR(10),AU.FechaCaducidad,103) + '</td>'
						   + '<td style="background-color: white">' + CONVERT(VARCHAR,AU.DiasRestantes) + '</td>'
						   + '</tr>' 
	FROM ReportesApp_RRHH_ControlUniformes_Registro AU
	LEFT JOIN ReportesApp_RRHH_ControlUniformes_Uniformes U ON U.idUniforme = AU.idUniforme
	LEFT JOIN PersonaMast P ON P.Persona = AU.idPersonal
	LEFT JOIN EmpleadoMast EM ON P.Persona = EM.Empleado
	LEFT JOIN HR_PuestoEmpresa PE ON PE.CodigoPuesto = EM.CodigoCargo
	LEFT JOIN departmentmst D ON D.department = EM.DeptoOrganizacion
	WHERE (AU.Estado = 'EN USO') AND (AU.DiasRestantes <= 5)
	ORDER BY LTRIM(RTRIM(P.NombreCompleto))
	
	SET @Asunto = 'AVISO DE UNIFORMES A DEVOLVER'

	SET @Mensaje = '<p><h2>LISTA DE UNIFORMES A DEVOLVER</h2></p>'
				  +'<p>Estos son los uniformes prestados al personal que ya están cerca de su fecha de caducidad y/o están vencidos: </p>'
				  + '<p><table border="1" cellspacing="1" cellpadding="1" >
					    <tr>
						    <td align="center" style="background-color:orange"><b>NOMBRE</b></td>
							<td align="center" style="background-color:orange"><b>ÁREA</b></td>
							<td align="center" style="background-color:orange"><b>PUESTO</b></td>
							<td align="center" style="background-color:orange"><b>UNIFORME</b></td>
							<td align="center" style="background-color:orange"><b>FECHA DE ASIGNACIÓN</b></td>
							<td align="center" style="background-color:orange"><b>FECHA DE CADUCIDAD</b></td>
							<td align="center" style="background-color:orange"><b>DÍAS RESTANTES</b></td>
						</tr>
						<tbody>'
						+ ISNULL(@Asignaciones, '') 	
						+'</tbody>
						</table>
					</p>'
				  +'<p>Fecha de envío: '+CONVERT(CHAR(10),GETDATE(),103)+RIGHT(RTRIM(CONVERT(CHAR(26),GETDATE(),22)),12)+'</p>'
				  +'<BR>'
				  +'<p><font face="verdana" size="2" color="DarkRed">Tecnologías de la Información - GRUPO TRANSPESA</font></p>'
	
	EXEC msdb.dbo.sp_send_dbmail 
		 @profile_name='AVISODESISTEMA', -- REAL: @profile_name = 'AVISODESISTEMA',  -- PRUEBAS: @profile_name = 'AVISODELSISTEMA',
		 @recipients = 'talentohumano@transpesa.com.pe; asistente.gth@transpesa.com.pe',
		 --@blind_copy_recipients = 'asistenteauditoria@transpesa.com.pe;jrojas@transpesa.com.pe;desarrollo@transpesa.com.pe;desarrollo2@transpesa.com.pe;' 
		 @subject = @Asunto,
		 @body_format = 'HTML',
		 @body = @Mensaje	
END