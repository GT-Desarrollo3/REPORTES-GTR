
-- CREAR TABLA ReportesApp_Operaciones_ControlItems_RegistroKitNeumaticos

-------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES HORNA
-- Create date: 18/12/2024
-- Description:	REGISTRAR KIT DE NEUMATICO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_AsignarKitNeumatico]
@Opcion INT,
@IDHerramienta INT,
@Persona INT,
@Tracto INT,
@UserRegistra VARCHAR(20)
AS
DECLARE @Almacen CHAR(3)
DECLARE @exito VARCHAR(MAX)

IF (@Opcion = 1) BEGIN
	SET @exito = '0 = ¡Registro Exitoso!'
END
ELSE BEGIN
	SET @exito = '0 = ¡Desvinculación Exitosa!'
END

SET @Almacen = (SELECT Almacen FROM ReportesApp_Mantenimiento_Herramientas WHERE IdHerramienta = @IDHerramienta)
DECLARE @CodMaleta VARCHAR(30) = (SELECT CodMaleta FROM ReportesApp_Mantenimiento_Herramientas WHERE IdHerramienta = @IDHerramienta)
DECLARE @Placa VARCHAR(30) = (SELECT NumeroPlaca FROM OP_TR_Vehiculo WHERE IdVehiculo = @Tracto)
DECLARE @Conductor VARCHAR(30) = (SELECT NombreCompleto FROM PersonaMast WHERE Persona = @Persona)

IF (@Opcion = 1) BEGIN
	IF EXISTS(SELECT TOP 1(1) FROM ReportesApp_Operaciones_ControlItems_RegistroKitNeumaticos WHERE Persona != @Persona AND idTracto = @Tracto) BEGIN
		SET @exito='-1 = No puede asignarle herramientas a la unidad ' + @Placa + ' porque está asignada a otro conductor.'
		GOTO Terminar
	END
END

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN
		IF ((@Almacen = '001' AND @Persona IN (20711,20712,9983)) OR (@Almacen = '002' AND @Persona NOT IN (20711,20712,9983))) BEGIN
			SET @exito = '-2 = La persona seleccionada no es de esta sucursal.'
			ROLLBACK
			GOTO Terminar
		END

		INSERT INTO ReportesApp_Operaciones_ControlItems_RegistroKitNeumaticos(idHerramienta, idTracto, Persona, UsuarioRegistra, FHoraRegistra)
		VALUES(@IDHerramienta, @Tracto, @Persona, @UserRegistra, GETDATE())
	END
	ELSE BEGIN
		IF ((SELECT CodMaleta FROM ReportesApp_Mantenimiento_Herramientas WHERE IdHerramienta = @IDHerramienta AND CodMaleta NOT LIKE '%KN%') IS NOT NULL) BEGIN
			SET @exito = '-4 = Esta herramienta está contenida en la maleta '+@CodMaleta+', se debe devolver desde el Registro de Maletas.'
			ROLLBACK
			GOTO Terminar
		END

		DELETE FROM ReportesApp_Operaciones_ControlItems_RegistroKitNeumaticos WHERE IdHerramienta = @IDHerramienta AND idTracto = @Tracto
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

-------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 16/07/2024
-- Description:	LISTAR KITS DE NEUMÁTICO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_ListarKitNeumatico]
@Opcion INT,
@Empleado VARCHAR(250),
@Herramienta VARCHAR(250),
@Tracto VARCHAR(250)
AS
BEGIN
	IF (@Opcion = 1) BEGIN		-- LISTAR HERRAMIENTAS DISPONIBLES
		DECLARE @TEMP_ULT TABLE (IdHerramienta INT, UserDesvincula VARCHAR(30), FHoraDesvincula DATETIME)
		
		INSERT INTO @TEMP_ULT
		SELECT T.IdHerramienta, T.UserDesvincula, T.FHoraDesvincula
		FROM (SELECT *, ROW_NUMBER() OVER (PARTITION BY IdHerramienta ORDER BY FHoraDesvincula DESC) rn
			  FROM ReportesApp_Mantenimiento_HerramientaPersona_Desvinculada) T
		INNER JOIN ReportesApp_Mantenimiento_Herramientas H ON H.IdHerramienta = T.IdHerramienta
		WHERE (T.rn = 1) AND (H.CodMaleta = 'KN-01')

		SELECT DISTINCT H.IdHerramienta, H.CodigoItemAlmacen AS 'COD_ALMACEN', CASE WHEN H.Estado = 0 THEN 'INACTIVO' ELSE 'ACTIVO' END 'ESTADO',
		(CASE WHEN H.Almacen = '001' THEN 'TRUJILLO' WHEN H.Almacen = '002' THEN 'LIMA' END) AS 'SUCURSAL', H.CodigoInterno AS 'COD_INTERNO',
		RTRIM(H.Descripcion) AS 'HERRAMIENTA', C.Descripcion AS 'CATEGORÍA', U.UserDesvincula AS 'ULTIMO_USUARIO',
		CONVERT(VARCHAR,U.FHoraDesvincula,103)+' '+CONVERT(VARCHAR,U.FHoraDesvincula,8) AS 'ULTIMA_ASIGNACION'
		FROM ReportesApp_Mantenimiento_Herramientas H
		LEFT JOIN ReportesApp_Mantenimiento_HerramientaPersona HP ON HP.IdHerramienta = H.IdHerramienta
		LEFT JOIN ReportesApp_Mantenimiento_Herramientas_Categoria C ON C.IDCategoria = H.IDCategoria
		LEFT JOIN ReportesApp_Mantenimiento_Herramientas_Maletas_Detalle MD ON H.CodMaleta = MD.CodMaleta
		LEFT JOIN @TEMP_ULT U ON U.IdHerramienta = H.IdHerramienta 
		WHERE (HP.Persona IS NULL) AND (H.Almacen = '001') AND (H.CodMaleta = 'KN-01') AND
		(@Herramienta IS NULL OR RTRIM(H.Descripcion) LIKE '%' + @Herramienta + '%')
		ORDER BY RTRIM(H.Descripcion)
	END

	IF (@Opcion = 2) BEGIN		-- LISTAR HERRAMIENTAS ASIGNADAS
		SELECT KN.Persona, KN.idHerramienta, ISNULL(KN.idTracto,-1) AS 'idTracto', ISNULL(V.NumeroPlaca,'NO TIENE') AS 'TRACTO',
		LTRIM(RTRIM(P.NombreCompleto)) AS 'EMPLEADO', H.CodigoItemAlmacen AS 'COD_ALMACEN', H.CodigoInterno AS 'COD_INTERNO',
		RTRIM(H.Descripcion) AS 'HERRAMIENTA', C.Descripcion AS 'CATEGORÍA', KN.UsuarioRegistra AS 'USUARIO_ENTREGA', KN.FHoraRegistra AS 'FECHA_ENTREGA'
		FROM ReportesApp_Operaciones_ControlItems_RegistroKitNeumaticos KN
		LEFT JOIN ReportesApp_Mantenimiento_HerramientaPersona HP ON KN.Persona = HP.Persona AND KN.idHerramienta = HP.IdHerramienta
		INNER JOIN PersonaMast P ON P.Persona = KN.Persona
		INNER JOIN ReportesApp_Mantenimiento_Herramientas H ON H.IdHerramienta = KN.IdHerramienta
		LEFT JOIN ReportesApp_Mantenimiento_Herramientas_Categoria C ON C.IDCategoria = H.IDCategoria
		LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = KN.idTracto
		WHERE (H.Almacen = '001') AND (H.CodMaleta = 'KN-01') AND (@Herramienta IS NULL OR RTRIM(H.Descripcion) LIKE '%' + @Herramienta + '%')
		AND (@Empleado IS NULL OR LTRIM(RTRIM(P.NombreCompleto)) LIKE '%' + @Empleado + '%') AND (@Tracto IS NULL OR ISNULL(V.NumeroPlaca,'NO TIENE') LIKE '%' + @Tracto + '%')
		ORDER BY KN.FHoraRegistra DESC
	END
END

-------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 19-12-2024
-- Description:	MODIFICAR CONDUCTOR EN KIT DE NEUMATICOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_ModificarKitNeumatico]
@PersonaAnterior INT,
@PersonaNueva INT,
@Tracto INT,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	DECLARE @Placa VARCHAR(30) = (SELECT NumeroPlaca FROM OP_TR_Vehiculo WHERE IdVehiculo = @Tracto)
	DECLARE @Conductor VARCHAR(250) = (SELECT LTRIM(RTRIM(NombreCompleto)) FROM PersonaMast WHERE Persona = @PersonaNueva)

	IF EXISTS(SELECT TOP 1(1) FROM ReportesApp_Operaciones_ControlItems_RegistroKitNeumaticos WHERE Persona = @PersonaNueva AND idTracto != @Tracto) BEGIN
		SET @exito='-1 = No puede asignarle herramientas al conductor ' + @Conductor + ' porque está asignado a otra unidad.'
		GOTO Terminar
	END
	ELSE BEGIN
		UPDATE HP
		SET HP.Persona = @PersonaNueva
		FROM ReportesApp_Mantenimiento_HerramientaPersona HP
		LEFT JOIN ReportesApp_Operaciones_ControlItems_RegistroKitNeumaticos KN ON HP.Persona = KN.Persona AND KN.idHerramienta = HP.IdHerramienta
		WHERE (HP.Persona = @PersonaAnterior) AND (KN.idHerramienta = HP.IdHerramienta)

		UPDATE ReportesApp_Operaciones_ControlItems_RegistroKitNeumaticos
		SET Persona = @PersonaNueva
		WHERE idTracto = @Tracto

		/*
		SELECT * FROM ReportesApp_Mantenimiento_HerramientaPersona
		WHERE Persona = 23751
		
		SELECT * FROM ReportesApp_Operaciones_ControlItems_RegistroKitNeumaticos
		*/

		SET @Exito = '0 = Conductor asignado correctamente.'
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

-------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 09/01/2024
-- Description:	REVISAR KIT DE NEUMATICO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_RevisarKitNeumatico]
@idTracto INT
AS
BEGIN
	IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ControlItems_RegistroKitNeumaticos WHERE idTracto = @idTracto)) BEGIN
		SELECT TOP(1) idTracto, CONVERT(VARCHAR,FHoraRegistra,103)+' '+CONVERT(VARCHAR,FHoraRegistra,8) AS 'Fecha'
		FROM ReportesApp_Operaciones_ControlItems_RegistroKitNeumaticos
		WHERE idTracto = @idTracto
	END
	ELSE BEGIN
		SELECT -1 AS 'idTracto', 'NO ASIGNADA' AS 'Fecha'
	END
END

-------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 28-04-2025
-- Description:	ALERTAR KIT DE NEUMÁTICOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_AlertarKitNeumatico]
@idTracto INT,
@idRuta INT,
@Programacion VARCHAR(30)
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	DECLARE @NumeroPlaca VARCHAR(30) = (SELECT RTRIM(NumeroPlaca) FROM OP_TR_Vehiculo WHERE IdVehiculo = @idTracto)

	IF (@Programacion IN ('LINDLEY','LIMAGAS','TOLVAS','GENERAL')) BEGIN
		IF (@idRuta IN (334, 333, 335, 231, 1137, 230, 1190, 330, 1090, 186, 1174, 119, 236, 468, 835, 1242, 1243, 1269, 566, 184, 1191, 1214, 1231)) BEGIN
			IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_ControlItems_RegistroKitNeumaticos WHERE idTracto = @idTracto)) BEGIN
				SET @Exito = '0 = La unidad SÍ cuenta con un Kit de Neumáticos.'
			END
			ELSE BEGIN
				SET @Exito = '-1 = La unidad '+@NumeroPlaca+' NO puede ser programada para este viaje porque no cuenta con un Kit de Neumáticos asignado. '
								   +'Favor de hacer la solicitud en el almacén de herramientas.'
			END
		END
		ELSE BEGIN
			SET @Exito = '0 = La unidad SÍ cuenta con un Kit de Neumáticos.'
		END
	END
	ELSE BEGIN
		SET @Exito = '0 = La unidad SÍ cuenta con un Kit de Neumáticos.'
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

-------------------------------------------------------------------------------

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
		SELECT C.idMaletaC, C.CodMaleta AS 'CÓDIGO', CASE WHEN C.TipoMaleta = 'MH' THEN 'MALETA HERRAMIENTA' WHEN C.TipoMaleta = 'KN' THEN 'KIT NEUMÁTICO'
		ELSE ' ' END AS 'TIPO', C.Estado AS 'ESTADO', C.idPersona, RTRIM(P.NombreCompleto) AS 'EMPLEADO', ISNULL(COUNT(D.idMaletaD),0) AS 'NRO_HERRAMIENTAS',
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
		D.UsuarioCrea, D.FechaCrea FROM ReportesApp_Mantenimiento_Herramientas_Maletas_Detalle D
		LEFT JOIN ReportesApp_Mantenimiento_Herramientas H ON D.IdHerramienta = H.IdHerramienta
		LEFT JOIN ReportesApp_Mantenimiento_Herramientas_Categoria CA ON CA.IDCategoria = H.IDCategoria
		LEFT JOIN ReportesApp_Mantenimiento_Herramientas_Maletas_Cabecera C ON D.idMaletaC = C.idMaletaC
		LEFT JOIN PersonaMast P ON P.Persona = C.idPersona
		WHERE (D.idMaletaC = @idMaletaC) AND (@Empleado IS NULL OR RTRIM(H.Descripcion) LIKE '%' + @Empleado + '%')
		ORDER BY H.Descripcion ASC
	 END
END

-------------------------------------------------------------------------------

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

		IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_Herramientas_Maletas_Cabecera WHERE idPersona = @idPersona) AND (@idPersona NOT IN (81,5462,22687))) BEGIN
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
		
		IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_Herramientas_Maletas_Cabecera WHERE idPersona = @idPersona) AND (@idPersona NOT IN (81,5462,22687))) BEGIN
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

-------------------------------------------------------------------------------

SELECT TOP 1000 [idHerramienta]
      ,[idTracto]
      ,[Persona]
      ,[FHoraRegistra]
      ,[UsuarioRegistra]
  FROM [spring].[dbo].[ReportesApp_Operaciones_ControlItems_RegistroKitNeumaticos]

  SELECT * FROM OP_TR_Vehiculo
  WHERE Estado = 2
