
-- CREAR TABLA ReportesApp_Mantenimiento_ControlCanaletas_Registro Y LLENARLA

-- CREAR TABLA ReportesApp_Mantenimiento_ControlCanaletas_Programaciones

-- CREAR TABLA ReportesApp_Mantenimiento_ControlCanaletas_Cambios

-- CREAR TABLA ReportesApp_Mantenimiento_ControlCanaletas_CambiosDetalle

---------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 26-08-2024
-- Description:	LISTAR CONTROL DE CANALETAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlCanaletas_ListarCanaletas]
@Opcion INT,
@Sucursal VARCHAR(100)
AS
BEGIN
	IF (@Opcion = 1) BEGIN		-- LISTAR MAESTRO DE CANALETAS
		SELECT idCanaleta, Sucursal AS 'SUCURSAL', Descripcion AS 'CANALETA', Longitud AS 'LONGITUD'
		FROM ReportesApp_Mantenimiento_ControlCanaletas_Registro
		WHERE Sucursal = @Sucursal
	END
END

---------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 26-08-2024
-- Description:	REGISTRAR Y EDITAR PROGRAMACIONES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlCanaletas_RegistrarEditarProgramacion]
@Opcion INT,
@idCanaletaProg INT,
@idCanaleta INT,
@FechaProg DATE,
@Estado VARCHAR(20),
@Observacion VARCHAR(250),
@Usuario VARCHAR(20)
AS
DECLARE @Contador INT 
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- CREAR PROGRAMACION
		IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_ControlCanaletas_Programacion WHERE idCanaleta = @idCanaleta AND FechaProg = @FechaProg)) BEGIN
			SET @Exito = '-1 = Esta canaleta ya tiene programada una limpieza para esta fecha.'
			ROLLBACK
			GOTO Terminar
		END

		SET @Contador = (SELECT MAX(idCanaletaProg) FROM ReportesApp_Mantenimiento_ControlCanaletas_Programacion)
		SET @Contador = ISNULL(@Contador,0) + 1 
	
		INSERT INTO ReportesApp_Mantenimiento_ControlCanaletas_Programacion(idCanaletaProg,idCanaleta,FechaProyectada,DiasRestantes,Observacion,Estado,UsuarioCrea,FechaCrea)
		SELECT @Contador, @idCanaleta, @FechaProg, 0, '', 'PROGRAMADO', @Usuario, GETDATE()

		SET @Exito = '0 = Programación de limpieza completada.'
	END

	IF (@Opcion = 2) BEGIN		-- EDITAR PROGRAMACION
		/*
		IF (@FechaProg <= (SELECT FechaProg FROM ReportesApp_Mantenimiento_ControlCanaletas_Programacion WHERE idCanaletaProg = @idCanaletaProg)) BEGIN
			SET @Exito = '-1 = No puede programar una fecha anterior a la ya ingresada.'
			ROLLBACK
			GOTO Terminar
		END
		*/

		IF (@Estado = 'PROGRAMADO') BEGIN
			UPDATE ReportesApp_Mantenimiento_ControlCanaletas_Programacion
			SET FechaProyectada = @FechaProg, Observacion = @Observacion, Estado = @Estado
			WHERE idCanaletaProg = @idCanaletaProg
		END
		
		IF (@Estado = 'COMPLETADO') BEGIN
			UPDATE ReportesApp_Mantenimiento_ControlCanaletas_Programacion
			SET FechaProg = @FechaProg, Observacion = @Observacion, Estado = @Estado
			WHERE idCanaletaProg = @idCanaletaProg
		END

		SET @Exito = '0 = Programación de limpieza actualizada.'
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

---------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 27-08-2024
-- Description:	LISTAR PROGRAMACION DE CANALETAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlCanaletas_ListarProgramacion]
@Sucursal VARCHAR(100),
@Estado VARCHAR(20),
@Canaleta VARCHAR(250),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	UPDATE ReportesApp_Mantenimiento_ControlCanaletas_Programacion
	SET DiasRestantes = DATEDIFF(DAY, GETDATE(), FechaProyectada)
	WHERE (Estado != 'COMPLETADO')

	UPDATE ReportesApp_Mantenimiento_ControlCanaletas_Programacion
	SET DiasRestantes = DATEDIFF(DAY, FechaProg, FechaProyectada)
	WHERE (Estado = 'COMPLETADO')

	UPDATE ReportesApp_Mantenimiento_ControlCanaletas_Programacion
	SET Estado = 'VENCIDO'
	WHERE DATEDIFF(DAY, GETDATE(), FechaProyectada) <= 0 AND (Estado != 'COMPLETADO')

	IF (@Estado = 'TODOS') BEGIN
		SELECT CP.idCanaletaProg, CR.Sucursal AS 'SUCURSAL', CR.Descripcion AS 'CANALETA', CR.Longitud AS 'LONGITUD (M)',
		CONVERT(VARCHAR,CP.FechaProyectada,103) AS 'FECHA_PROGRAMADA', CONVERT(VARCHAR,CP.FechaProg,103) AS 'FECHA_LIMPIEZA',
		CP.DiasRestantes AS 'DIAS_RESTANTES', CP.Estado AS 'ESTADO', CP.Observacion AS 'OBSERVACION', CP.UsuarioCrea, CP.FechaCrea
		FROM ReportesApp_Mantenimiento_ControlCanaletas_Programacion CP
		LEFT JOIN ReportesApp_Mantenimiento_ControlCanaletas_Registro CR ON CR.idCanaleta = CP.idCanaleta
		WHERE (CR.Sucursal = @Sucursal) AND (CP.FechaProyectada BETWEEN @FINICIO AND @FFIN) AND (@Canaleta IS NULL OR CR.Descripcion LIKE '%' + @Canaleta + '%')
		ORDER BY CP.FechaProyectada DESC
	END

	IF (@Estado = 'PROGRAMADO') BEGIN
		SELECT CP.idCanaletaProg, CR.Sucursal AS 'SUCURSAL', CR.Descripcion AS 'CANALETA', CR.Longitud AS 'LONGITUD (M)',
		CONVERT(VARCHAR,CP.FechaProyectada,103) AS 'FECHA_PROGRAMADA', CONVERT(VARCHAR,CP.FechaProg,103) AS 'FECHA_LIMPIEZA',
		CP.DiasRestantes AS 'DIAS_RESTANTES', CP.Estado AS 'ESTADO', CP.Observacion AS 'OBSERVACION', CP.UsuarioCrea, CP.FechaCrea
		FROM ReportesApp_Mantenimiento_ControlCanaletas_Programacion CP
		LEFT JOIN ReportesApp_Mantenimiento_ControlCanaletas_Registro CR ON CR.idCanaleta = CP.idCanaleta
		WHERE (CR.Sucursal = @Sucursal) AND (CP.FechaProyectada BETWEEN @FINICIO AND @FFIN) AND (@Canaleta IS NULL OR CR.Descripcion LIKE '%' + @Canaleta + '%')
		AND (CP.Estado IN ('PROGRAMADO','VENCIDO'))
		ORDER BY CP.FechaProyectada DESC
	END
	ELSE BEGIN
		SELECT CP.idCanaletaProg, CR.Sucursal AS 'SUCURSAL', CR.Descripcion AS 'CANALETA', CR.Longitud AS 'LONGITUD (M)',
		CONVERT(VARCHAR,CP.FechaProyectada,103) AS 'FECHA_PROGRAMADA', CONVERT(VARCHAR,CP.FechaProg,103) AS 'FECHA_LIMPIEZA',
		CP.DiasRestantes AS 'DIAS_RESTANTES', CP.Estado AS 'ESTADO', CP.Observacion AS 'OBSERVACION', CP.UsuarioCrea, CP.FechaCrea
		FROM ReportesApp_Mantenimiento_ControlCanaletas_Programacion CP
		LEFT JOIN ReportesApp_Mantenimiento_ControlCanaletas_Registro CR ON CR.idCanaleta = CP.idCanaleta
		WHERE (CR.Sucursal = @Sucursal) AND (CP.FechaProyectada BETWEEN @FINICIO AND @FFIN) AND (@Canaleta IS NULL OR CR.Descripcion LIKE '%' + @Canaleta + '%')
		AND (CP.Estado = @Estado)
		ORDER BY CP.FechaProyectada DESC
	END
END

------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 26-08-2024
-- Description:	REGISTRAR Y EDITAR CAMBIOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlCanaletas_RegistrarEditarCambios]
@Opcion INT,
@idCCambio INT,
@idCanaleta INT,
@FechaProg DATE,
@Observacion VARCHAR(250),
@Usuario VARCHAR(20)
AS
DECLARE @Contador INT 
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- CREAR CAMBIO
		IF (EXISTS(SELECT * FROM ReportesApp_Mantenimiento_ControlCanaletas_Cambios WHERE idCanaleta = @idCanaleta AND FechaProg = @FechaProg)) BEGIN
			SET @Exito = '-1 = Esta canaleta ya tiene programada un cambio para esta fecha.'
			ROLLBACK
			GOTO Terminar
		END

		SET @Contador = (SELECT MAX(idCCambio) FROM ReportesApp_Mantenimiento_ControlCanaletas_Cambios)
		SET @Contador = ISNULL(@Contador,0) + 1 

		DECLARE @Longitud DECIMAL(10,2) = (SELECT Longitud FROM ReportesApp_Mantenimiento_ControlCanaletas_Registro WHERE idCanaleta = @idCanaleta)
	
		INSERT INTO ReportesApp_Mantenimiento_ControlCanaletas_Cambios(idCCambio,idCanaleta,FechaProg,LongitudRestante,DiasRestantes,UsuarioCrea,FechaCrea)
		SELECT @Contador, @idCanaleta, @FechaProg, @Longitud, 0, @Usuario, GETDATE()

		SET @Exito = '0 = Programación de cambio completada.'
	END

	IF (@Opcion = 2) BEGIN		-- EDITAR PROGRAMACION
		UPDATE ReportesApp_Mantenimiento_ControlCanaletas_Cambios
		SET FechaProg = @FechaProg, Observacion = @Observacion, UsuarioCrea = @Usuario, FechaCrea = GETDATE()
		WHERE idCCambio = @idCCambio

		SET @Exito = '0 = Programación de cambio actualizada.'
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

-------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 10-01-2025
-- Description:	LISTAR CAMBIOS DE CANALETAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlCanaletas_ListarCambios]
@Sucursal VARCHAR(100),
@Canaleta VARCHAR(250),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	UPDATE ReportesApp_Mantenimiento_ControlCanaletas_Cambios
	SET DiasRestantes = DATEDIFF(DAY, GETDATE(), FechaProg)
	WHERE (LongitudRestante != 0.00)

	SELECT C.idCCambio, CR.Sucursal AS 'SUCURSAL', CR.Descripcion AS 'CANALETA', CR.Longitud AS 'LONGITUD (M)',
	CONVERT(VARCHAR,C.FechaProg,103) AS 'FECHA_PROGRAMADA', CONVERT(VARCHAR,C.UFechaCambio,103) AS 'ULTIMO_CAMBIO',
	C.DiasRestantes AS 'DIAS_RESTANTES', C.LongitudRestante,
	CONVERT(DECIMAL(10,2),ROUND(((CR.Longitud - C.LongitudRestante) * 100)/CR.Longitud,2)) AS 'PORCENTAJE (%)',
	C.Observacion AS 'OBSERVACION', C.UsuarioCrea, C.FechaCrea
	FROM ReportesApp_Mantenimiento_ControlCanaletas_Cambios C
	LEFT JOIN ReportesApp_Mantenimiento_ControlCanaletas_Registro CR ON CR.idCanaleta = C.idCanaleta
	WHERE (CR.Sucursal = @Sucursal) AND (C.FechaProg BETWEEN @FINICIO AND @FFIN) AND (@Canaleta IS NULL OR CR.Descripcion LIKE '%' + @Canaleta + '%')
	ORDER BY C.FechaProg DESC
END

-------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 26-08-2024
-- Description:	ELIMINAR PROGRAMACIONES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlCanaletas_EliminarProgramacion]
@Opcion INT,
@idCanaletaProg INT
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- ELIMINAR PROGRAMACION	
		DELETE FROM ReportesApp_Mantenimiento_ControlCanaletas_Programacion
		WHERE idCanaletaProg = @idCanaletaProg

		SET @Exito = '0 = Programación de limpieza eliminada.'
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR CAMBIOS	
		DELETE FROM ReportesApp_Mantenimiento_ControlCanaletas_Cambios
		WHERE idCCambio = @idCanaletaProg

		DELETE FROM ReportesApp_Mantenimiento_ControlCanaletas_CambiosDetalle
		WHERE idCCambio = @idCanaletaProg

		SET @Exito = '0 = Programación de cambio eliminada.'
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

-------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 11-01-2024
-- Description:	REGISTRAR Y ELIMINAR CAMBIO DETALLE
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlCanaletas_RegistrarCambioDetalle]
@Opcion INT,
@idCCambio INT,
@idCCambioDetalle INT,
@FechaCambio DATE,
@LongitudCambio DECIMAL(10,2)
AS
DECLARE @Contador INT 
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- CREAR CAMBIO
		DECLARE @LongitudRestante DECIMAL(10,2) = (SELECT LongitudRestante FROM ReportesApp_Mantenimiento_ControlCanaletas_Cambios WHERE idCCambio = @idCCambio)
		DECLARE @Longitud DECIMAL(10,2) = (SELECT CR.Longitud FROM ReportesApp_Mantenimiento_ControlCanaletas_Cambios C
										   LEFT JOIN ReportesApp_Mantenimiento_ControlCanaletas_Registro CR ON CR.idCanaleta = C.idCanaleta
										   WHERE C.idCCambio = @idCCambio)

		IF (@LongitudCambio > @LongitudRestante) BEGIN
			SET @Exito = '-1 = No puede ingresar una longitud mayor a la de la canaleta.'
			ROLLBACK
			GOTO Terminar
		END
		ELSE BEGIN
			SET @Contador = (SELECT MAX(idCCambioDetalle) FROM ReportesApp_Mantenimiento_ControlCanaletas_CambiosDetalle WHERE idCCambio = @idCCambio)
			SET @Contador = ISNULL(@Contador,0) + 1

			INSERT INTO ReportesApp_Mantenimiento_ControlCanaletas_CambiosDetalle(idCCambioDetalle,idCCambio,FechaCambio,Longitud)
			SELECT @Contador, @idCCambio, @FechaCambio, @LongitudCambio

			UPDATE ReportesApp_Mantenimiento_ControlCanaletas_Cambios
			SET UFechaCambio = @FechaCambio, LongitudRestante = LongitudRestante - @LongitudCambio
			WHERE idCCambio = @idCCambio

			SET @Exito = '0 = Fecha de cambio registrada.'
		END
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR CAMBIO
		DECLARE @UltimaLongitud DECIMAL(10,2) = (SELECT Longitud FROM ReportesApp_Mantenimiento_ControlCanaletas_CambiosDetalle
												WHERE (idCCambioDetalle = @idCCambioDetalle) AND (idCCambio = @idCCambio))

		DELETE FROM ReportesApp_Mantenimiento_ControlCanaletas_CambiosDetalle
		WHERE (idCCambioDetalle = @idCCambioDetalle) AND (idCCambio = @idCCambio)

		DECLARE @UltimoCambio DATE = (SELECT TOP(1) ISNULL(CD.FechaCambio,C.FechaProg) FROM ReportesApp_Mantenimiento_ControlCanaletas_CambiosDetalle CD
									  LEFT JOIN ReportesApp_Mantenimiento_ControlCanaletas_Cambios C ON C.idCCambio = CD.idCCambio
									  WHERE (CD.idCCambio = @idCCambio) ORDER BY CD.FechaCambio DESC)

		UPDATE ReportesApp_Mantenimiento_ControlCanaletas_Cambios
		SET UFechaCambio = @UltimoCambio, LongitudRestante = LongitudRestante + @UltimaLongitud
		WHERE idCCambio = @idCCambio

		UPDATE ReportesApp_Mantenimiento_ControlCanaletas_CambiosDetalle
		SET idCCambioDetalle = idCCambioDetalle - 1
		WHERE (idCCambio = @idCCambio) AND (idCCambioDetalle > @idCCambioDetalle)

		SET @Exito = '0 = Fecha de cambio eliminada.'
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

-------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 11-01-2025
-- Description:	LISTAR CAMBIOS DETALLE
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlCanaletas_ListarCambiosDetalle]
@idCCambio INT
AS
BEGIN
	SELECT idCCambioDetalle, CONVERT(VARCHAR,FechaCambio,103) AS 'FECHA_CAMBIO', Longitud AS 'LONGITUD'
	FROM ReportesApp_Mantenimiento_ControlCanaletas_CambiosDetalle
	WHERE idCCambio = @idCCambio
	ORDER BY FechaCambio DESC
END



select * from ReportesApp_Mantenimiento_ControlCanaletas_Cambios
select * from ReportesApp_Mantenimiento_ControlCanaletas_CambiosDetalle