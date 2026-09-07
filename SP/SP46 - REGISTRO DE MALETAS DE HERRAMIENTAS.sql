
-- CREAR TABLA ReportesApp_Mantenimiento_Herramientas_Maletas_Cabecera

-- CREAR TABLA ReportesApp_Mantenimiento_Herramientas_Maletas_Detalle

-- MODIFICAR TABLA ReportesApp_Mantenimiento_Herramientas

---------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 03-07-2024
-- Description:	REGISTRAR TICKET DE CONSTANCIA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_Herramientas_IngresarMaletas]
@Opcion INT,
@CodMaleta VARCHAR(30),
@TipoMaleta VARCHAR(5),
@idPersona INT,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX) = '0 = '
DECLARE @Correlativo INT

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- INGRESAR MALETA
		IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_Herramientas_Maletas_Cabecera WHERE CodMaleta = @CodMaleta)) BEGIN
			SET @Exito = '-1 = No puede ingresar esta maleta porque ya existe una con el código ' + @CodMaleta
			ROLLBACK
			GOTO Terminar
		END

		IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_Herramientas_Maletas_Cabecera WHERE idPersona = @idPersona) AND (@idPersona NOT IN (81,5462))) BEGIN
			SET @Exito = '-2 = Este trabajador ya tiene la maleta ' + (SELECT CodMaleta FROM ReportesApp_Mantenimiento_Herramientas_Maletas_Cabecera
						 WHERE idPersona = @idPersona) + '. Solo puede tener 1 maleta asignada.'
			ROLLBACK
			GOTO Terminar
		END

		SET @Correlativo = (SELECT MAX(idMaletaC) FROM ReportesApp_Mantenimiento_Herramientas_Maletas_Cabecera) 
		SET @Correlativo = ISNULL(@Correlativo,0) + 1

		INSERT INTO ReportesApp_Mantenimiento_Herramientas_Maletas_Cabecera(idMaletaC,CodMaleta,TipoMaleta,idPersona,Estado,UsuarioCrea,FechaCrea)
		VALUES(@Correlativo,@CodMaleta,@TipoMaleta,@idPersona,'DISPONIBLE',@Usuario,GETDATE())

		SET @Exito = '0 = La maleta '+@CodMaleta+' fue ingresada correctamente.'
	END

	IF (@Opcion = 2) BEGIN		-- MODIFICAR DUEÑO DE MALETA
		IF ((SELECT Estado FROM ReportesApp_Mantenimiento_Herramientas_Maletas_Cabecera WHERE CodMaleta = @CodMaleta) != 'DISPONIBLE') BEGIN
			SET @Exito = '-1 = No puede asignar esta maleta porque está en uso en este momento.'
			ROLLBACK
			GOTO Terminar
		END
		
		IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_Herramientas_Maletas_Cabecera WHERE idPersona = @idPersona) AND (@idPersona NOT IN (81,5462))) BEGIN
			SET @Exito = '-2 = Este trabajador ya tiene la maleta ' + (SELECT CodMaleta FROM ReportesApp_Mantenimiento_Herramientas_Maletas_Cabecera
						 WHERE idPersona = @idPersona) + '. Solo puede tener 1 maleta asignada.'
			ROLLBACK
			GOTO Terminar
		END

		UPDATE ReportesApp_Mantenimiento_Herramientas_Maletas_Cabecera
		SET idPersona = @idPersona
		WHERE CodMaleta = @CodMaleta

		SET @Exito = '0 = La maleta '+@CodMaleta+' fue asignada al empleado ' + (SELECT RTRIM(NombreCompleto) FROM PersonaMast WHERE Persona = @idPersona) + '.'
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

------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 03-07-2024
-- Description:	LISTAR MALETAS DE HERRAMIENTAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_Herramientas_ListarMaletas]
@Opcion INT,
@Empleado VARCHAR(250),
@idMaletaC INT
AS
BEGIN
	 IF (@Opcion = 1) BEGIN		-- LISTAR MALETAS CABECERA
		SELECT idMaletaC, CodMaleta FROM ReportesApp_Mantenimiento_Herramientas_Maletas_Cabecera
	 END

	 IF (@Opcion = 2) BEGIN		-- LISTAR HERRAMIENTAS EN MALETAS
		SELECT C.idMaletaC, C.CodMaleta AS 'CÓDIGO', CASE WHEN C.TipoMaleta = 'MH' THEN 'MALETA HERRAMIENTA' ELSE 'KIT NEUMÁTICO' END AS 'TIPO',
		C.Estado AS 'ESTADO', C.idPersona, RTRIM(P.NombreCompleto) AS 'EMPLEADO', ISNULL(COUNT(D.idMaletaD),0) AS 'NRO_HERRAMIENTAS',
		C.UsuarioCrea, C.FechaCrea FROM ReportesApp_Mantenimiento_Herramientas_Maletas_Cabecera C
		LEFT JOIN PersonaMast P ON P.Persona = C.idPersona
		LEFT JOIN ReportesApp_Mantenimiento_Herramientas_Maletas_Detalle D ON D.idMaletaC = C.idMaletaC
		WHERE (@Empleado IS NULL OR P.NombreCompleto LIKE '%' + @Empleado + '%')
		GROUP BY C.idMaletaC, C.CodMaleta, C.TipoMaleta, C.Estado, C.idPersona, P.NombreCompleto, C.UsuarioCrea, C.FechaCrea
		ORDER BY C.idMaletaC ASC
	 END

	 IF (@Opcion = 3) BEGIN
		SELECT D.idMaletaC, D.idMaletaD, H.IdHerramienta, RTRIM(P.NombreCompleto) AS 'EMPLEADO', D.CodMaleta AS 'CÓDIGO',
		H.CodigoItemAlmacen AS 'COD_ALMACÉN', RTRIM(H.Descripcion) AS 'HERRAMIENTA', H.CodigoInterno AS 'COD_HERRAMIENTA',
		CA.Descripcion AS 'CATEGORÍA', (CASE WHEN H.Almacen = '001' THEN 'TRUJILLO' WHEN H.Almacen = '002' THEN 'LIMA' END) AS 'SUCURSAL',
		D.UsuarioCrea, D.FechaCrea
		FROM ReportesApp_Mantenimiento_Herramientas_Maletas_Detalle D
		LEFT JOIN ReportesApp_Mantenimiento_Herramientas H ON D.IdHerramienta = H.IdHerramienta
		LEFT JOIN ReportesApp_Mantenimiento_Herramientas_Categoria CA ON CA.IDCategoria = H.IDCategoria
		LEFT JOIN ReportesApp_Mantenimiento_Herramientas_Maletas_Cabecera C ON D.idMaletaC = C.idMaletaC
		LEFT JOIN PersonaMast P ON P.Persona = C.idPersona
		WHERE (D.idMaletaC = @idMaletaC) AND (@Empleado IS NULL OR RTRIM(H.Descripcion) LIKE '%' + @Empleado + '%')
		ORDER BY H.Descripcion ASC
	 END
END

-----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ING. JOEL ROJAS RODRIGUEZ
-- Create date: 23/07/2021
-- Description:	LISTAR HERRAMIENTAS DE ALMACEN GENERAL
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlHerramientas_Herramientas_Listar]
@opcion int=1, --1) Solo Disponibles  2) Lista General
@Usuario VARCHAR(50)
AS
IF @opcion=1 BEGIN
	IF (@Usuario IN ('FLINARES','PNAVARRO','RCORZO','JALBAN')) BEGIN
		SELECT 
			Htas.IdHerramienta,
			(CASE WHEN Htas.Almacen = '001' THEN 'TRUJILLO' WHEN Htas.Almacen = '002' THEN 'LIMA' END) AS 'Sucursal',
			Htas.CodigoInterno,
			RTRIM(Htas.Descripcion)as 'Descripcion',
			cat.Descripcion Categoria,
			Htas.CodigoItemAlmacen,
			Htas.EsDeAlmacen,
		CASE WHEN Htas.Estado=0 THEN 'INACTIVO' ELSE 'ACTIVO' END Estado
		FROM ReportesApp_Mantenimiento_Herramientas Htas
		LEFT JOIN ReportesApp_Mantenimiento_HerramientaPersona HtaPer ON HtaPer.IdHerramienta=Htas.IdHerramienta
		LEFT JOIN ReportesApp_Mantenimiento_Herramientas_Categoria cat ON cat.IDCategoria=Htas.IDCategoria
		WHERE (HtaPer.Persona IS NULL) AND (Htas.Almacen = '002') AND (Htas.CodMaleta IS NULL)
		ORDER BY Descripcion
	END
	ELSE BEGIN
		SELECT 
			Htas.IdHerramienta,
			(CASE WHEN Htas.Almacen = '001' THEN 'TRUJILLO' WHEN Htas.Almacen = '002' THEN 'LIMA' END) AS 'Sucursal',
			Htas.CodigoInterno,
			RTRIM(Htas.Descripcion)as 'Descripcion',
			cat.Descripcion Categoria,
			Htas.CodigoItemAlmacen,
			Htas.EsDeAlmacen,
		CASE WHEN Htas.Estado=0 THEN 'INACTIVO' ELSE 'ACTIVO' END Estado
		FROM ReportesApp_Mantenimiento_Herramientas Htas
		LEFT JOIN ReportesApp_Mantenimiento_HerramientaPersona HtaPer ON HtaPer.IdHerramienta=Htas.IdHerramienta
		LEFT JOIN ReportesApp_Mantenimiento_Herramientas_Categoria cat ON cat.IDCategoria=Htas.IDCategoria
		WHERE (HtaPer.Persona IS NULL) AND (Htas.Almacen = '001') AND (Htas.CodMaleta IS NULL)
		ORDER BY Descripcion
	END
END

IF @opcion=2 BEGIN
	SELECT 
		ROW_NUMBER() OVER(ORDER BY cat.Descripcion ASC) AS Item,
		Htas.IdHerramienta,
		(CASE WHEN Htas.Almacen = '001' THEN 'TRUJILLO' WHEN Htas.Almacen = '002' THEN 'LIMA' END) AS 'Sucursal',
		Htas.CodigoInterno,
		RTRIM(Htas.Descripcion)as 'Descripcion',
		cat.Descripcion Categoria,
		Htas.CodigoItemAlmacen,
		CASE WHEN Htas.Estado=0 THEN 'INACTIVO' ELSE 'ACTIVO' END Estado,
		P.NombreCompleto Trabajador,
		HtaPer.FHoraRegistra FHPrestamo
	FROM ReportesApp_Mantenimiento_Herramientas Htas
	LEFT JOIN ReportesApp_Mantenimiento_HerramientaPersona HtaPer ON HtaPer.IdHerramienta=Htas.IdHerramienta
	LEFT JOIN PersonaMast P ON P.Persona=HtaPer.Persona
	LEFT JOIN ReportesApp_Mantenimiento_Herramientas_Categoria cat ON cat.IDCategoria=Htas.IDCategoria
	ORDER BY Categoria ASC, Descripcion
END

---------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ING. JOEL ROJAS RODRIGUEZ
-- Create date: 10/08/2021
-- Description:	REGISTRA VINCULO HERRAMIENTA > PERSONA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlHerramientas_HerramPersona_Vincular]
@IDHerramienta int,
@Persona int,
@Opcion int, --1=Vincula, 2=Desvincula
@UserRegistra VARCHAR(20)
AS
DECLARE @Almacen CHAR(3)
DECLARE @exito varchar(max)

IF @Opcion=1 BEGIN
	SET @exito='0=Registro Exitoso!'
END
ELSE BEGIN
	SET @exito='0=Desvinculación Exitosa!'
END

SET @Almacen = (SELECT Almacen FROM ReportesApp_Mantenimiento_Herramientas WHERE IdHerramienta=@IDHerramienta)
DECLARE @CodMaleta VARCHAR(30) = (SELECT CodMaleta FROM ReportesApp_Mantenimiento_Herramientas WHERE IdHerramienta = @IDHerramienta)

IF @Opcion=1 BEGIN
	IF EXISTS(SELECT TOP 1(1) FROM ReportesApp_Mantenimiento_HerramientaPersona WHERE IdHerramienta=@IDHerramienta) BEGIN
		SET @exito='-1=La herramienta ya se encuentra asignada, no puede asignarla a la Persona: '+CAST(@Persona AS VARCHAR)
		GOTO Terminar
	END	
END 

BEGIN TRAN
BEGIN TRY
	IF @Opcion=1 BEGIN
		IF ((@Almacen = '001' AND @Persona IN (64,20711,20712,9983)) OR (@Almacen = '002' AND @Persona NOT IN (64,20711,20712,9983))) BEGIN
			SET @exito='-2 = La persona seleccionada no es de esta sucursal.'
			ROLLBACK
			GOTO Terminar
		END
		
		INSERT INTO ReportesApp_Mantenimiento_HerramientaPersona(IdHerramienta,Persona ,UserRegistra,FHoraRegistra)
		VALUES(@IDHerramienta,@Persona,@UserRegistra,GETDATE())
	END
	ELSE BEGIN
		IF ((SELECT CodMaleta FROM ReportesApp_Mantenimiento_Herramientas WHERE IdHerramienta = @IDHerramienta AND CodMaleta NOT LIKE '%KN%') IS NOT NULL) BEGIN
			SET @exito = '-4 = Esta herramienta está contenida en en la maleta '+@CodMaleta+', se debe devolver desde el Registro de Maletas.'
			ROLLBACK
			GOTO Terminar
		END

		INSERT INTO ReportesApp_Mantenimiento_HerramientaPersona_Desvinculada(IdHerramienta,Persona ,UserRegistra,FHoraRegistra,UserDesvincula,FHoraDesvincula)
		SELECT IdHerramienta,Persona,UserRegistra,FHoraRegistra, @UserRegistra,GETDATE() FROM ReportesApp_Mantenimiento_HerramientaPersona  WHERE IdHerramienta=@IDHerramienta AND Persona=@Persona
			   
		DELETE FROM ReportesApp_Mantenimiento_HerramientaPersona WHERE IdHerramienta=@IDHerramienta AND Persona=@Persona
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

----------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 04/07/2023
-- Description:	ASIGNAR HERRAMIENTA A MALETA
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlHerramientas_AsignarEliminarMaletaHtta]
@Opcion INT,
@IDHerramienta INT,
@idMaletaC INT,
@CodMaleta VARCHAR(30),
@UserRegistra VARCHAR(20)
AS
DECLARE @correlativo INT
DECLARE @Exito VARCHAR(MAX) = '0 = Herramientas Asignadas a la maleta ' + @CodMaleta

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- ASIGNAR A MALETA
		IF (EXISTS(SELECT CodMaleta FROM ReportesApp_Mantenimiento_Herramientas_Maletas_Detalle WHERE IdHerramienta = @IDHerramienta)) BEGIN
			SET @exito = '-1 = No puede asignar esta herramienta porque ya pertenece a la maleta '+
						 (SELECT CodMaleta FROM ReportesApp_Mantenimiento_Herramientas WHERE IdHerramienta = @IDHerramienta)+'.'
			ROLLBACK
			GOTO Terminar
		END

		SET @correlativo = (SELECT MAX(idMaletaD) FROM ReportesApp_Mantenimiento_Herramientas_Maletas_Detalle WHERE idMaletaC = @idMaletaC)
		SET @correlativo = ISNULL(@correlativo,0) + 1
	
		INSERT INTO ReportesApp_Mantenimiento_Herramientas_Maletas_Detalle (idMaletaD,idMaletaC,CodMaleta,IdHerramienta,UsuarioCrea,FechaCrea)
		VALUES(@correlativo,@idMaletaC,@CodMaleta,@IDHerramienta,@UserRegistra,GETDATE())

		UPDATE ReportesApp_Mantenimiento_Herramientas
		SET CodMaleta = @CodMaleta
		WHERE IdHerramienta = @IDHerramienta

		SET @Exito = '0 = Herramientas Asignadas a la maleta ' + @CodMaleta
	END

	IF (@Opcion = 2) BEGIN		-- DESASIGNAR HERRAMIENTA
		IF (EXISTS(SELECT IdHerramienta FROM ReportesApp_Mantenimiento_HerramientaPersona WHERE IdHerramienta = @IDHerramienta)) BEGIN
			SET @exito = '-1 = No puede quitar esta herramienta porque ya ha sido asignada al empleado '+
						 (SELECT P.NombreCompleto FROM ReportesApp_Mantenimiento_HerramientaPersona HP
						  LEFT JOIN PersonaMast P ON P.Persona=HP.Persona
						  WHERE HP.IdHerramienta = @IDHerramienta)+'.'
			ROLLBACK
			GOTO Terminar
		END

		DELETE FROM ReportesApp_Mantenimiento_Herramientas_Maletas_Detalle
		WHERE IdHerramienta = @IDHerramienta

		UPDATE ReportesApp_Mantenimiento_Herramientas
		SET CodMaleta = NULL
		WHERE IdHerramienta = @IDHerramienta

		SET @Exito = '0 = La herramienta fue desvinculada de la maleta.'
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

--------------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
--------------------------------------------
CREADO POR: ING. JOEL ROJAS RODRIGUEZ
FECHA: 03/08/2021
DESC: ELIMINA  HERRAMIENTAS PARA CONTROL
-------------------------------------------
*/
ALTER  PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlHerramientas_HerramientaElimina]
@IDHerramienta INT,
@Usuario VARCHAR(15)
AS
DECLARE @CONT_C1 VARCHAR(MAX) = ''
DECLARE @Asunto VARCHAR(500) = ''
DECLARE @exito varchar(max) 
DECLARE @Mensaje VARCHAR(MAX) = ''
SET @exito='0=Eliminación Exitosa!'

BEGIN TRAN
BEGIN TRY
IF EXISTS(SELECT TOP 1(1) FROM ReportesApp_Mantenimiento_HerramientaPersona where IdHerramienta=@IDHerramienta) BEGIN
	  SET @exito='-1=No puede ser eliminada por que ya está asignada a un Trabajador!'
	  ROLLBACK
	  GOTO Terminar
END

IF EXISTS(SELECT TOP 1(1) FROM ReportesApp_Mantenimiento_HerramientaPersona_Desvinculada WHERE IdHerramienta=@IDHerramienta) BEGIN
	  SET @exito='-2=No puedes eliminar la herramienta (ID: '+CAST(@IDHerramienta AS VARCHAR)+ ' ya que tiene Historial!, consultar con area de sistemas.'
	  ROLLBACK
	  GOTO Terminar
END

IF EXISTS(SELECT * FROM ReportesApp_Mantenimiento_Herramientas_Maletas_Detalle WHERE IdHerramienta=@IDHerramienta) BEGIN
	  SET @exito='-3=No puedes eliminar la herramienta (ID: '+CAST(@IDHerramienta AS VARCHAR)+ ' porque está asignada a la maleta '+
				(SELECT CodMaleta FROM ReportesApp_Mantenimiento_Herramientas WHERE IdHerramienta = @IDHerramienta)+'.'
	  ROLLBACK
	  GOTO Terminar
END

IF @Usuario  IN ('','JROJAS','KZAVALETA','RESCALANTE') BEGIN
	DECLARE @NombrePersona VARCHAR(100) = (SELECT TOP 1 NombreCompleto FROM PersonaMast WHERE Persona IN (SELECT Persona FROM ReportesApp_Mantenimiento_Herramientas WHERE IdHerramienta=@IDHerramienta) AND Estado = 'A') 
	DECLARE @NombreHerramienta VARCHAR(150) = (SELECT TOP 1 Descripcion FROM ReportesApp_Mantenimiento_Herramientas WHERE IdHerramienta=@IDHerramienta)
	
	SELECT  @CONT_C1 = @CONT_C1 + '<tr style="background-color:#' + CONVERT(VARCHAR(06), ISNULL('F5F5DC', 'EAEDED')) + '">'
							+ '<td >' + UPPER(ISNULL(@NombrePersona,'')) + '</td>'
							+ '<td >' + UPPER(ISNULL(Descripcion,'')) + '</td>'
							+ '<td >' + CONVERT(CHAR(20),GETDATE(),103) + '</td>'
							+ '<td >' + UPPER(ISNULL(CodigoInterno,'')) + '</td>'
							+ '<td >' + UPPER(ISNULL(CONVERT(VARCHAR(10),CodigoItemAlmacen),'')) + '</td>'
							+ '<td >' + UPPER(ISNULL(@Usuario,'')) + '</td>'
						+ '</tr>'   
						FROM ReportesApp_Mantenimiento_Herramientas  
						WHERE IdHerramienta = @IDHerramienta

	DELETE  FROM ReportesApp_Mantenimiento_Herramientas where IdHerramienta=@IDHerramienta

			--AVISO DEL SISTEMA--
		--*****************--	
		   SET @Asunto = 'ALERTA SE ELIMINÓ LA HERRAMIENTA ' + CONVERT(VARCHAR(150),@NombreHerramienta) 

		   SET @Mensaje =	'<p><h2>ALERTA DE ELIMINACION DE LA HERRAMIENTA '+CONVERT(VARCHAR(10),@NombreHerramienta)+'</h2></p>'
							  + '<p>	
									<table border="1" cellspacing="1" cellpadding="1"  style="font-size: 10px;">
										<thead align="center" style="background-color:#98FB98; font-size: 13px;">
											<tr>
												<td align="center" width="200"><b>NOMBRE</b></td>
												<td align="center" width="110"><b>DESCRIPCION</b></td>												
												<td align="center" width="100"><b>FECHA ELIMINACION</b></td>
												<td align="center" width="100"><b>CODIGO INTERNO</b></td>
												<td align="center" width="100"><b>COD ITEM ALMACEN OT</b></td>
												<td align="center" width="100"><b>USUARIO</b></td>
											</tr>
										</thead>
										<tbody>'
										+ ISNULL(@CONT_C1, '') 	
								+'		</tbody>
									</table>
								</p>'	
						  +'<p> Fecha y Hora  : ' + CONVERT(CHAR(10),GETDATE(),103) + RIGHT(RTRIM(CONVERT(CHAR(26),GETDATE(),22)), 12) + '</p>'
						  +'<p><font face="verdana" size="2" color="DarkRed"> Tecnologías de la Información - GRUPO TRANSPESA</font></p>'

		    EXEC msdb.dbo.sp_send_dbmail 
			@profile_name = 'AVISODESISTEMA',
			@recipients = 'administracion@transpesa.com.pe;almacenherramientas@transpesa.com.pe;ti@transpesa.com.pe;desarrollo3@transpesa.com.pe;analistacostos@transpesa.com.pe'
			--@recipients = 'operacionestrujillo4@transpesa.com.pe; programacionlindley@transpesa.com.pe; operacionestrujillo3@transpesa.com.pe; operacionestrujillo@transpesa.com.pe; Operaciones@transpesa.com.pe; lleython@transpesa.com.pe; dpesantes@transpesa.com.pe; operacionestrujillo1@transpesa.com.pe'
			--,@blind_copy_recipients = 'lleython@transpesa.com.pe;auditoria@transpesa.com.pe;jrojas@transpesa.com.pe;desarrollo3@transpesa.com.pe;auditoria@transpesa.com.pe;gerenteoperaciones@transpesa.com.pe' 
			,@subject = @Asunto
			,@body_format = 'HTML' 
			,@body = @Mensaje
	                                                               
		--FIN AVISO DEL SISTEMA--
		--*********************--					
END	
ELSE BEGIN
  SET @exito='-1=Usted no tiene permiso para desvincular herramientas, comunicarse con Logistica'
  ROLLBACK
  GOTO Terminar
END

IF @Usuario IN ('') BEGIN
  SET @exito='-777=Test OK'
  ROLLBACK
  GOTO Terminar
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

------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ING. JOEL ROJAS RODRIGUEZ
-- Create date: 10/08/2021
-- Description:	LISTAR  VINCULO PERSONA - HERRAMIENTA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlHerramientas_PersonaHta_Listar]
@Persona INT,
@Usuario VARCHAR(15)
AS
IF (@Usuario IN ('FLINARES','PNAVARRO','RCORZO','JALBAN')) BEGIN
	SELECT HtaPer.Persona,HtaPer.IDHerramienta, Replace(P.NombreCompleto,',','') Trabajador, RTRIM(hta.Descripcion) Herramienta, Cat.Descripcion Categoria, hta.CodigoInterno, hta.CodigoItemAlmacen CodigoAlmacen, HtaPer.FHoraRegistra FH_Entrega  
	FROM ReportesApp_Mantenimiento_HerramientaPersona HtaPer
	INNER JOIN PersonaMast P ON P.Persona=HtaPer.Persona
	INNER JOIN ReportesApp_Mantenimiento_Herramientas Hta ON Hta.IdHerramienta = HtaPer.IdHerramienta
	LEFT JOIN ReportesApp_Mantenimiento_Herramientas_Categoria Cat ON Cat.IDCategoria=Hta.IDCategoria
	WHERE (Hta.CodMaleta IS NULL) AND (HtaPer.Persona = @Persona OR @Persona=0) AND ((Hta.Almacen = '002') OR (HtaPer.Persona IN (64,20711,20712,9983)))
	ORDER BY HtaPer.FHoraRegistra DESC
END
ELSE BEGIN
	SELECT HtaPer.Persona,HtaPer.IDHerramienta, Replace(P.NombreCompleto,',','')  Trabajador, RTRIM(hta.Descripcion) Herramienta, Cat.Descripcion Categoria, hta.CodigoInterno, hta.CodigoItemAlmacen CodigoAlmacen, HtaPer.FHoraRegistra FH_Entrega  
	FROM ReportesApp_Mantenimiento_HerramientaPersona HtaPer
	INNER JOIN PersonaMast P ON P.Persona=HtaPer.Persona
	INNER JOIN ReportesApp_Mantenimiento_Herramientas Hta ON Hta.IdHerramienta = HtaPer.IdHerramienta
	LEFT JOIN ReportesApp_Mantenimiento_Herramientas_Categoria Cat ON Cat.IDCategoria=Hta.IDCategoria
	WHERE (Hta.CodMaleta IS NULL) AND (HtaPer.Persona = @Persona OR @Persona=0) AND ((Hta.Almacen = '001') AND (HtaPer.Persona  NOT IN (64,20711,20712,9983)))
	ORDER BY HtaPer.FHoraRegistra DESC
END

--------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 06/07/2024
-- Description:	ASIGNAR MALETA A TRABAJADOR
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlHerramientas_AsignarDevolverMaleta]
@Opcion INT,
@idMaletaC INT,
@Usuario VARCHAR(20)
AS
DECLARE @exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	DECLARE @Persona INT = (SELECT idPersona FROM ReportesApp_Mantenimiento_Herramientas_Maletas_Cabecera WHERE idMaletaC = @idMaletaC)
	DECLARE @TEMP_MALETA TABLE (Contador INT, idMaletaC INT, IDHerramienta INT, Persona INT)

	INSERT INTO @TEMP_MALETA(Contador, idMaletaC, IDHerramienta, Persona)
	SELECT ROW_NUMBER() OVER(ORDER BY idMaletaC ASC), idMaletaC, IdHerramienta, @Persona
	FROM ReportesApp_Mantenimiento_Herramientas_Maletas_Detalle
	WHERE idMaletaC = @idMaletaC
	ORDER BY idMaletaD

	IF (@Opcion = 1) BEGIN		-- ASIGNAR MALETA A TRABAJADOR
		DECLARE @Contador INT
		SET @Contador = 1

		WHILE (@Contador <= (SELECT COUNT(Contador) FROM @TEMP_MALETA)) BEGIN
			DECLARE @CodHerramienta INT = (SELECT IdHerramienta FROM @TEMP_MALETA WHERE Contador = @Contador)
			
			EXEC ReportesApp_Mantenimiento_ControlHerramientas_HerramPersona_Vincular
			@IDHerramienta = @CodHerramienta,
			@Persona = @Persona,
			@Opcion = 1,
			@UserRegistra = @Usuario

			SET @Contador = @Contador + 1
		END

		UPDATE ReportesApp_Mantenimiento_Herramientas_Maletas_Cabecera
		SET Estado = 'ENTREGADO'
		WHERE idMaletaC = @idMaletaC

		SET @exito = '0 = Maleta asignada correctamente.'
	END

	IF (@Opcion = 2) BEGIN		-- DEVOLVER MALETA A ALMACÉN
		DECLARE @Contador2 INT
		SET @Contador2 = 1

		WHILE (@Contador2 <= (SELECT COUNT(Contador) FROM @TEMP_MALETA)) BEGIN
			DECLARE @CodHerramienta2 INT = (SELECT IdHerramienta FROM @TEMP_MALETA WHERE Contador = @Contador2)

			INSERT INTO ReportesApp_Mantenimiento_HerramientaPersona_Desvinculada(IdHerramienta,Persona,UserRegistra,FHoraRegistra,UserDesvincula,FHoraDesvincula)
			SELECT HP.IdHerramienta, HP.Persona, HP.UserRegistra, HP.FHoraRegistra, @Usuario, GETDATE()
			FROM ReportesApp_Mantenimiento_HerramientaPersona HP
			WHERE HP.IdHerramienta=@CodHerramienta2 AND HP.Persona = @Persona
			   
			DELETE FROM ReportesApp_Mantenimiento_HerramientaPersona WHERE IdHerramienta=@CodHerramienta2 AND Persona=@Persona

			SET @Contador2 = @Contador2 + 1
		END

		UPDATE ReportesApp_Mantenimiento_Herramientas_Maletas_Cabecera
		SET Estado = 'DISPONIBLE'
		WHERE idMaletaC = @idMaletaC

		SET @exito = '0 = Maleta devuelta al almacén.'
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
