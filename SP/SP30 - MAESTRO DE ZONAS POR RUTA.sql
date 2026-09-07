
-- CREAR TABLA ReportesApp_Operaciones_RutaZona_Zonas Y LLENARLA

-- CREAR TABLA ReportesApp_Operaciones_RutaZona_Registro

----------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 25-01-2024
-- Description:	LISTAR ZONAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_RutaZona_ListarZonas]
AS
BEGIN
    SELECT idZona, Descripcion FROM ReportesApp_Operaciones_RutaZona_Zonas
END

----------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 25-01-2024
-- Description:	ASIGNAR ZONAS A RUTA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_RutaZona_AsignarEditarZonas]
@Opcion INT,
@IdRuta INT,
@IdOperacion INT,
@idZona INT,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN
		IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_RutaZona_Registro WHERE IdRuta = @IdRuta AND IdOperacion = @IdOperacion)) BEGIN
			SET @Exito = '-1 = Esta ruta ya tiene una zona registrada.'
			ROLLBACK
			GOTO Terminar
		END

		SET @correlativo = (SELECT MAX(idRegistroZona) FROM ReportesApp_Operaciones_RutaZona_Registro)
		SET @correlativo = ISNULL(@correlativo,0) + 1

		INSERT INTO ReportesApp_Operaciones_RutaZona_Registro(idRegistroZona,IdRuta,IdOperacion,idZona,UsuarioCreacion,FechaCreacion)
		VALUES(@correlativo,@IdRuta,@IdOperacion,@idZona,@Usuario,GETDATE())

		SET @Exito = '0 = Zona Asignada Correctamente.'
	END

	IF (@Opcion = 2) BEGIN
		IF (EXISTS(SELECT * FROM ReportesApp_Operaciones_RutaZona_Registro WHERE IdRuta = @IdRuta AND IdOperacion = @IdOperacion)) BEGIN
			UPDATE ReportesApp_Operaciones_RutaZona_Registro
			SET idZona = @idZona, UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()
			WHERE IdRuta = @IdRuta AND IdOperacion = @IdOperacion
		END
		ELSE BEGIN
			SET @correlativo = (SELECT MAX(idRegistroZona) FROM ReportesApp_Operaciones_RutaZona_Registro)
			SET @correlativo = ISNULL(@correlativo,0) + 1

			INSERT INTO ReportesApp_Operaciones_RutaZona_Registro(idRegistroZona,IdRuta,IdOperacion,idZona,UsuarioCreacion,FechaCreacion)
			VALUES(@correlativo,@IdRuta,@IdOperacion,@idZona,@Usuario,GETDATE())
		END

		SET @Exito = '0 = Zona Actualizada Correctamente.'
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

----------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 25-01-2024
-- Description:	LISTAR ZONAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_RutaZona_ListarRegistroZonas]
@IdOperacion INT,
@Ruta VARCHAR(120)
AS
BEGIN
    IF (@IdOperacion = 5) BEGIN
		SELECT DISTINCT R.IdRuta, RT.Descripcion AS 'RUTA', R.TipoProgramacion AS 'IdOperacion', O.Descripcion AS 'OPERACION',
		RZ.idZona, Z.Descripcion AS 'ZONA'
		FROM ReportesApp_Operacion_Previaje_Registros R
		LEFT JOIN OP_TR_Ruta RT ON RT.IdRuta = R.IdRuta
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = R.TipoProgramacion
		LEFT JOIN ReportesApp_Operaciones_RutaZona_Registro RZ ON RZ.IdRuta = R.IdRuta AND RZ.IdOperacion = R.TipoProgramacion
		LEFT JOIN ReportesApp_Operaciones_RutaZona_Zonas Z ON Z.idZona = RZ.idZona
		WHERE (R.FechaInicio BETWEEN '01/01/2023' AND GETDATE()) AND (R.IdRuta NOT IN (0,-1)) AND
		(R.TipoProgramacion NOT IN (8,9,6)) AND (RT.Descripcion IS NULL OR RT.Descripcion LIKE '%' + @Ruta + '%')
		
		/*
		SELECT RZ.idRegistroZona AS 'N°', RZ.IdRuta, R.Descripcion AS 'RUTA', RZ.IdOperacion, O.Descripcion AS 'OPERACION',
		RZ.idZona, Z.Descripcion AS 'ZONA'
		FROM ReportesApp_Operaciones_RutaZona_Registro RZ
		LEFT JOIN OP_TR_Ruta R ON R.IdRuta = RZ.IdRuta
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = RZ.IdOperacion
		LEFT JOIN ReportesApp_Operaciones_RutaZona_Zonas Z ON Z.idZona = RZ.idZona
		WHERE (R.Descripcion IS NULL OR R.Descripcion LIKE '%' + @Ruta + '%') --AND (RZ.IdOperacion = @IdOperacion)
		ORDER BY RZ.idRegistroZona DESC
		*/
	END
	ELSE BEGIN
		SELECT DISTINCT R.IdRuta, RT.Descripcion AS 'RUTA', R.TipoProgramacion AS 'IdOperacion', O.Descripcion AS 'OPERACION',
		RZ.idZona, Z.Descripcion AS 'ZONA'
		FROM ReportesApp_Operacion_Previaje_Registros R
		LEFT JOIN OP_TR_Ruta RT ON RT.IdRuta = R.IdRuta
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = R.TipoProgramacion
		LEFT JOIN ReportesApp_Operaciones_RutaZona_Registro RZ ON RZ.IdRuta = R.IdRuta AND RZ.IdOperacion = R.TipoProgramacion
		LEFT JOIN ReportesApp_Operaciones_RutaZona_Zonas Z ON Z.idZona = RZ.idZona
		WHERE (R.FechaInicio BETWEEN '01/01/2023' AND GETDATE()) AND (R.IdRuta NOT IN (0,-1)) AND
		(R.TipoProgramacion = @IdOperacion) AND (RT.Descripcion IS NULL OR RT.Descripcion LIKE '%' + @Ruta + '%')

		/*
		SELECT RZ.idRegistroZona AS 'N°', RZ.IdRuta, R.Descripcion AS 'RUTA', RZ.IdOperacion, O.Descripcion AS 'OPERACION',
		RZ.idZona, Z.Descripcion AS 'ZONA'
		FROM ReportesApp_Operaciones_RutaZona_Registro RZ
		LEFT JOIN OP_TR_Ruta R ON R.IdRuta = RZ.IdRuta
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = RZ.IdOperacion
		LEFT JOIN ReportesApp_Operaciones_RutaZona_Zonas Z ON Z.idZona = RZ.idZona
		WHERE (R.Descripcion IS NULL OR R.Descripcion LIKE '%' + @Ruta + '%') AND (RZ.IdOperacion = @IdOperacion)
		ORDER BY RZ.idRegistroZona DESC
		*/
	END
END
