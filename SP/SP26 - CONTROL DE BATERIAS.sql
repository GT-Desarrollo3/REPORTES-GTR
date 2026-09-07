
-- CREAR TABLA ReportesApp_Mantenimiento_ControlBaterias_Registro

-- CREAR TABLA ReportesApp_Mantenimiento_ControlBaterias_Traspasos

------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 30-11-2023
-- Description:	BUSCAR TRACTOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlBaterias_BuscarTractos]
@Placa VARCHAR(20)
AS
BEGIN
	SELECT TOP(25) V.IdVehiculo, V.NumeroPlaca AS 'PLACA', SV.Descripcion AS 'TIPO_VEHICULO'
	FROM OP_TR_Vehiculo V
	LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
	WHERE (V.Estado = 2) AND (@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%')
END

------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 30-11-2023
-- Description:	CAMBIAR ESTADO DE BATERÍA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlBaterias_CambiarEstadoBateria]
@Opcion INT,
@idBateria INT,
@Motivo VARCHAR(250),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- DESACTIVAR BATERÍA
		UPDATE ReportesApp_Mantenimiento_ControlBaterias_Registro
		SET Estado = 0, UsuarioModificacion = @Usuario, FechaModificacion = GETDATE(), Motivo = @Motivo
		WHERE idBateria = @idBateria

		SET @Exito = '0 = La batería ahora está inactiva.'
	END
	
	IF (@Opcion = 2) BEGIN		-- ELIMINAR BATERÍA
		DELETE FROM ReportesApp_Mantenimiento_ControlBaterias_Registro
		WHERE idBateria = @idBateria

		SET @Exito = '0 = Batería eliminada.'
	END

	IF (@Opcion = 3) BEGIN		-- INSERTAR EN ALMACEN
		UPDATE ReportesApp_Mantenimiento_ControlBaterias_Registro
		SET Estado = 2, UsuarioModificacion = @Usuario, FechaModificacion = GETDATE(), Motivo = @Motivo
		WHERE idBateria = @idBateria

		SET @Exito = '0 = La batería ahora está en almacén.'
	END

	IF (@Opcion = 4) BEGIN		-- ACTIVAR BATERÍA
		UPDATE ReportesApp_Mantenimiento_ControlBaterias_Registro
		SET Estado = 2, UsuarioModificacion = @Usuario, FechaModificacion = GETDATE(), Motivo = @Motivo
		WHERE idBateria = @idBateria

		SET @Exito = '0 = La batería ahora está activa.'
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

------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 30-11-2023
-- Description:	INSERTAR BATERIA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlBaterias_RegistrarTraspaso]
@idBateria INT,
@idVehiculoAnt INT,
@idVehiculoAct INT,
@Motivo VARCHAR(250),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

/*
IF (@idVehiculoAnt = @idVehiculoAct)
BEGIN
	SET @Exito = '-1 = No puede traspasar baterías en una misma unidad.'
	GOTO Terminar
END
*/

SET @correlativo = (SELECT MAX(idTraspaso) FROM ReportesApp_Mantenimiento_ControlBaterias_Traspasos)
SET @correlativo = ISNULL(@correlativo,0) + 1

BEGIN TRAN
BEGIN TRY
	UPDATE ReportesApp_Mantenimiento_ControlBaterias_Registro
	SET idVehiculo = @idVehiculoAct, UsuarioModificacion = @Usuario, FechaModificacion = GETDATE(), Estado = 1
	WHERE idBateria = @idBateria

	INSERT INTO ReportesApp_Mantenimiento_ControlBaterias_Traspasos(idTraspaso, idBateria, idVehiculoAnt, idVehiculoAct, Motivo, FechaTraspaso, UsuarioTraspaso)
	VALUES(@correlativo, @idBateria, @idVehiculoAnt, @idVehiculoAct, @Motivo, GETDATE(), @Usuario)

	SET @Exito = '0 = Traspaso realizado exitosamente.'
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

------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 30-11-2023
-- Description:	LISTAR TRASPASOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlBaterias_ListarTraspasos]
@Opcion INT,
@CodBateria VARCHAR(50),
@Placa VARCHAR(50),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@Opcion = 1) BEGIN		-- HISTORIAL DE TRASPASOS
		SELECT B.CodBateria AS 'BATERIA', V1.NumeroPlaca AS 'VEHICULO_ANTERIOR', SV1.Descripcion AS 'TIPO_ANTERIOR',
		V2.NumeroPlaca AS 'VEHICULO_ACTUAL', SV2.Descripcion AS 'TIPO_ACTUAL', T.Motivo AS 'MOTIVO',
		T.FechaTraspaso AS 'FECHA', T.UsuarioTraspaso AS 'USUARIO_TRASPASO'
		FROM ReportesApp_Mantenimiento_ControlBaterias_Traspasos T
		LEFT JOIN ReportesApp_Mantenimiento_ControlBaterias_Registro B ON B.idBateria = T.idBateria
		LEFT JOIN OP_TR_Vehiculo V1 ON V1.IdVehiculo = T.idVehiculoAnt
		LEFT JOIN OP_TR_SubTipoVehiculo SV1 ON SV1.SubTipoVehiculo = V1.SubTipoVehiculo
		LEFT JOIN OP_TR_Vehiculo V2 ON V2.IdVehiculo = T.idVehiculoAct
		LEFT JOIN OP_TR_SubTipoVehiculo SV2 ON SV2.SubTipoVehiculo = V2.SubTipoVehiculo
		WHERE (@CodBateria IS NULL OR B.CodBateria LIKE '%' + @CodBateria + '%') AND
		(@Placa IS NULL OR V2.NumeroPlaca LIKE '%' + @Placa + '%')
		AND (T.FechaTraspaso BETWEEN @FINICIO AND @FFIN)
		ORDER BY T.idTraspaso DESC
	END

	IF (@Opcion = 2) BEGIN		-- REGISTRO DE INSPECCIONES
		SELECT I.idInspeccionB, I.idBateria, I.CodBateria AS 'BATERIA', B.Marca AS 'MARCA', B.Modelo AS 'MODELO / PLACA', V.NumeroPlaca AS 'VEHICULO',
		SV.Descripcion AS 'TIPO_VEHICULO', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'PROGRAMACION', I.FechaInspeccion AS 'FECHA_INSPECCION',
		CONVERT(VARCHAR,I.FechaCambio,103) AS 'FECHA_CAMBIO', I.Intervalo AS 'DIAS_FALTANTES', CONVERT(VARCHAR,ISNULL(I.NivelCarga,0.00)) + ' %' AS 'NIVEL_CARGA',
		CONVERT(VARCHAR,ISNULL(I.EstadoB,0.00)) + ' %' AS 'ESTADO_BATERIA'
		FROM ReportesApp_Mantenimiento_ControlBaterias_Inspecciones I
		LEFT JOIN ReportesApp_Mantenimiento_ControlBaterias_Registro B ON B.idBateria = I.idBateria
		LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = B.idVehiculo
		LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON V.IdVehiculo = UC.IdUnidad
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = UC.IdProgramacion
		WHERE (@CodBateria IS NULL OR B.CodBateria LIKE '%' + @CodBateria + '%') AND
		(@Placa IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%')
		AND (I.FechaInspeccion BETWEEN @FINICIO AND @FFIN)
		ORDER BY I.idInspeccionB DESC
	END
END

------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 30-11-2023
-- Description:	INSERTAR BATERIA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlBaterias_RegistrarModificarBaterias]
@Opcion INT,
@idBateria INT,
@CodBateria VARCHAR(50),
@Marca VARCHAR(100),
@Modelo VARCHAR(100),
@idVehiculo INT,
@FechaInicio DATETIME,
@DuracionBateria INT,
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT
DECLARE @DiasDuracion INT

SET @correlativo = (SELECT MAX(idBateria) FROM ReportesApp_Mantenimiento_ControlBaterias_Registro)
SET @correlativo = ISNULL(@correlativo,0) + 1

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- INSERTAR BATERÍA
		SET @DiasDuracion = DATEDIFF(DAY, @FechaInicio, GETDATE())

		INSERT INTO ReportesApp_Mantenimiento_ControlBaterias_Registro(idBateria, CodBateria, Marca, Modelo, idVehiculo, FechaInicio, DiasDuracion, DuracionBateria,
		FechaCambio, FechaInspeccion, Estado, UsuarioCreacion, FechaCreacion)
		VALUES(@correlativo, @CodBateria, @Marca, @Modelo, @idVehiculo, @FechaInicio, @DiasDuracion, @DuracionBateria, DATEADD(DAY,@DuracionBateria,@FechaInicio), GETDATE(), 1, @Usuario, GETDATE())

		SET @Exito = '0 = Batería Registrada.'
	END

	IF (@Opcion = 2) BEGIN		-- MODIFICAR BATERÍA
		SET @DiasDuracion = DATEDIFF(DAY, @FechaInicio, GETDATE())

		UPDATE ReportesApp_Mantenimiento_ControlBaterias_Registro
		SET CodBateria = @CodBateria, Marca = @Marca, Modelo = @Modelo, FechaInicio = @FechaInicio, DuracionBateria = @DuracionBateria, FechaCambio = DATEADD(DAY,@DuracionBateria,@FechaInicio),
		UsuarioModificacion = @Usuario, FechaModificacion = GETDATE() 
		WHERE idBateria = @idBateria

		SET @Exito = '0 = Batería Actualizada.'
	END

	IF (@Opcion = 3) BEGIN		-- INSERTAR BATERÍA A ALMACEN
		SET @DiasDuracion = DATEDIFF(DAY, @FechaInicio, GETDATE())

		INSERT INTO ReportesApp_Mantenimiento_ControlBaterias_Registro(idBateria, CodBateria, Marca, Modelo, idVehiculo, FechaInicio, DiasDuracion,
		DuracionBateria, Estado, UsuarioCreacion, FechaCreacion)
		VALUES(@correlativo, @CodBateria, @Marca, @Modelo, NULL, @FechaInicio, @DiasDuracion, @DuracionBateria, 2, @Usuario, GETDATE())

		SET @Exito = '0 = Batería Registrada.'
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

-----------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 30-11-2023
-- Description:	LISTAR BATERIAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlBaterias_ListarBaterias]
@FiltroFechas INT,
@TipoFecha VARCHAR(5),
@CodBateria VARCHAR(50),
@Placa VARCHAR(50),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@Estado INT
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	UPDATE B
	SET DiasDuracion = DATEDIFF(DAY,B.FechaInicio,GETDATE())
	FROM ReportesApp_Mantenimiento_ControlBaterias_Registro B
	WHERE B.Estado = 1

	IF (@FiltroFechas = 0) BEGIN
		SELECT B.idBateria, B.CodBateria AS 'CODIGO', B.Marca AS 'MARCA', B.Modelo AS 'MODELO / PLACA', ISNULL(B.idVehiculo,0) AS 'idVehiculo',
		V.NumeroPlaca AS 'VEHICULO', SV.Descripcion AS 'TIPO_VEHICULO', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'PROGRAMACION',
		CONVERT(VARCHAR,B.FechaInicio,103) AS 'FECHA_INICIO', CONVERT(VARCHAR,B.FechaInspeccion,103) AS 'FECHA_INSPECCION',
		CONVERT(VARCHAR,B.NivelCarga) + ' %' AS 'NIVEL_CARGA', CONVERT(VARCHAR,B.EstadoB) + ' %' AS 'ESTADO_BATERIA', ISNULL(B.DuracionBateria,0) AS 'INTERVALO_DIAS',
		ISNULL(B.DiasDuracion,0) AS 'CONTEO_DIAS', ISNULL(B.DuracionBateria,0) - ISNULL(B.DiasDuracion,0) AS 'DIAS_FALTANTES',
		CONVERT(VARCHAR,B.FechaCambio,103) AS 'FECHA_CAMBIO', CASE B.Estado WHEN 1 THEN 'ACTIVA' WHEN 0 THEN 'INACTIVA' ELSE 'EN ALMACEN' END AS 'ESTADO',
		B.Motivo AS 'MOTIVO', B.UsuarioCreacion, B.FechaCreacion, B.UsuarioModificacion, B.FechaModificacion
		FROM ReportesApp_Mantenimiento_ControlBaterias_Registro B
		LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = B.idVehiculo
		LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON V.IdVehiculo = UC.IdUnidad
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = UC.IdProgramacion
		WHERE (B.CodBateria IS NULL OR B.CodBateria LIKE '%' + @CodBateria + '%') AND (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%')
		AND (B.Estado = @Estado)
		ORDER BY B.idBateria DESC
	END
	
	IF (@FiltroFechas = 1 AND @TipoFecha = 'FI') BEGIN
		SELECT B.idBateria, B.CodBateria AS 'CODIGO', B.Marca AS 'MARCA', B.Modelo AS 'MODELO / PLACA', ISNULL(B.idVehiculo,0) AS 'idVehiculo',
		V.NumeroPlaca AS 'VEHICULO', SV.Descripcion AS 'TIPO_VEHICULO', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'PROGRAMACION',
		CONVERT(VARCHAR,B.FechaInicio,103) AS 'FECHA_INICIO', CONVERT(VARCHAR,B.FechaInspeccion,103) AS 'FECHA_INSPECCION',
		CONVERT(VARCHAR,B.NivelCarga) + ' %' AS 'NIVEL_CARGA', CONVERT(VARCHAR,B.EstadoB) + ' %' AS 'ESTADO_BATERIA', ISNULL(B.DuracionBateria,0) AS 'INTERVALO_DIAS',
		ISNULL(B.DiasDuracion,0) AS 'CONTEO_DIAS', ISNULL(B.DuracionBateria,0) - ISNULL(B.DiasDuracion,0) AS 'DIAS_FALTANTES',
		CONVERT(VARCHAR,B.FechaCambio,103) AS 'FECHA_CAMBIO', CASE B.Estado WHEN 1 THEN 'ACTIVA' WHEN 0 THEN 'INACTIVA' ELSE 'EN ALMACEN' END AS 'ESTADO',
		B.Motivo AS 'MOTIVO', B.UsuarioCreacion, B.FechaCreacion, B.UsuarioModificacion, B.FechaModificacion
		FROM ReportesApp_Mantenimiento_ControlBaterias_Registro B
		LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = B.idVehiculo
		LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON V.IdVehiculo = UC.IdUnidad
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = UC.IdProgramacion
		WHERE (B.CodBateria IS NULL OR B.CodBateria LIKE '%' + @CodBateria + '%') AND (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%')
		AND (B.FechaInspeccion BETWEEN @FINICIO AND @FFIN) AND (B.Estado = @Estado)
		ORDER BY B.idBateria DESC
	END

	IF (@FiltroFechas = 1 AND @TipoFecha = 'FC') BEGIN
		SELECT B.idBateria, B.CodBateria AS 'CODIGO', B.Marca AS 'MARCA', B.Modelo AS 'MODELO / PLACA', ISNULL(B.idVehiculo,0) AS 'idVehiculo',
		V.NumeroPlaca AS 'VEHICULO', SV.Descripcion AS 'TIPO_VEHICULO', CASE WHEN O.IdOperacion = 5 THEN 'SIN OPERACION' ELSE O.Descripcion END AS 'PROGRAMACION',
		CONVERT(VARCHAR,B.FechaInicio,103) AS 'FECHA_INICIO', CONVERT(VARCHAR,B.FechaInspeccion,103) AS 'FECHA_INSPECCION',
		CONVERT(VARCHAR,B.NivelCarga) + ' %' AS 'NIVEL_CARGA', CONVERT(VARCHAR,B.EstadoB) + ' %' AS 'ESTADO_BATERIA', ISNULL(B.DuracionBateria,0) AS 'INTERVALO_DIAS',
		ISNULL(B.DiasDuracion,0) AS 'CONTEO_DIAS', ISNULL(B.DuracionBateria,0) - ISNULL(B.DiasDuracion,0) AS 'DIAS_FALTANTES',
		CONVERT(VARCHAR,B.FechaCambio,103) AS 'FECHA_CAMBIO', CASE B.Estado WHEN 1 THEN 'ACTIVA' WHEN 0 THEN 'INACTIVA' ELSE 'EN ALMACEN' END AS 'ESTADO',
		B.Motivo AS 'MOTIVO', B.UsuarioCreacion, B.FechaCreacion, B.UsuarioModificacion, B.FechaModificacion
		FROM ReportesApp_Mantenimiento_ControlBaterias_Registro B
		LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = B.idVehiculo
		LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON V.IdVehiculo = UC.IdUnidad
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = UC.IdProgramacion
		WHERE (B.CodBateria IS NULL OR B.CodBateria LIKE '%' + @CodBateria + '%') AND (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%')
		AND (B.FechaCambio BETWEEN @FINICIO AND @FFIN) AND (B.Estado = @Estado)
		ORDER BY B.idBateria DESC
	END
END

------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 30-11-2023
-- Description:	FILTRAR BATERIAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlBaterias_FiltrarBaterias]
@idBateria INT
AS
BEGIN
	SELECT B.idBateria, B.CodBateria AS 'CODIGO', B.Marca AS 'MARCA', B.Modelo AS 'MODELO / PLACA', ISNULL(B.idVehiculo,0) AS 'idVehiculo',
	V.NumeroPlaca AS 'VEHICULO', SV.Descripcion AS 'TIPO_VEHICULO', CONVERT(VARCHAR,B.FechaInicio,103) AS 'FECHA_INICIO', B.FechaCambio AS 'FECHA_CAMBIO',
	B.FechaInspeccion AS 'FECHA_INSPECCION', ISNULL(B.DiasDuracion,0) AS 'DIAS_DURACION', B.NivelCarga AS 'NIVEL_CARGA', B.EstadoB AS 'ESTADO_BATERIA',
	CASE B.Estado WHEN 1 THEN 'ACTIVA' WHEN 0 THEN 'INACTIVA' ELSE 'EN ALMACEN' END AS 'ESTADO',
	B.Motivo AS 'MOTIVO', ISNULL(B.DuracionBateria,0) AS 'DURACION', B.UsuarioCreacion, B.FechaCreacion, B.UsuarioModificacion, B.FechaModificacion
	FROM ReportesApp_Mantenimiento_ControlBaterias_Registro B
	LEFT JOIN OP_TR_Vehiculo V ON V.IdVehiculo = B.idVehiculo
	LEFT JOIN OP_TR_SubTipoVehiculo SV ON SV.SubTipoVehiculo = V.SubTipoVehiculo
	WHERE (B.idBateria = @idBateria)
END

---------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 22-08-2024
-- Description:	GENERAR INSPECCION DE BATERIA
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_ControlBaterias_GenerarInspeccion]
@idBateria INT,
@CodBateria VARCHAR(50),
@Intervalo INT,
@FechaCambio DATETIME,
@FechaInspeccion DATETIME,
@NivelCarga DECIMAL(10,2),
@EstadoB DECIMAL(10,2),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

BEGIN TRAN
BEGIN TRY
	SET @correlativo = (SELECT MAX(idInspeccionB) FROM ReportesApp_Mantenimiento_ControlBaterias_Inspecciones)
	SET @correlativo = ISNULL(@correlativo,0) + 1

	DECLARE @UltFechaI DATETIME = (SELECT FechaInspeccion FROM ReportesApp_Mantenimiento_ControlBaterias_Registro WHERE idBateria = @idBateria)

	IF (@FechaInspeccion > @UltFechaI) BEGIN
		INSERT INTO ReportesApp_Mantenimiento_ControlBaterias_Inspecciones(idInspeccionB,idBateria,CodBateria,Intervalo,FechaCambio,FechaInspeccion,
																		   NivelCarga,EstadoB,UsuarioCrea,FechaCrea)
		SELECT @correlativo,idBateria,CodBateria,DuracionBateria,FechaCambio,FechaInspeccion,NivelCarga,EstadoB,@Usuario, GETDATE()
		FROM ReportesApp_Mantenimiento_ControlBaterias_Registro WHERE idBateria = @idBateria
		
		UPDATE ReportesApp_Mantenimiento_ControlBaterias_Registro
		SET DuracionBateria = @Intervalo, FechaCambio = @FechaCambio, FechaInspeccion = @FechaInspeccion,
		NivelCarga = @NivelCarga, EstadoB = @EstadoB, UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()
		WHERE idBateria = @idBateria
	END
	ELSE BEGIN
		INSERT INTO ReportesApp_Mantenimiento_ControlBaterias_Inspecciones(idInspeccionB,idBateria,CodBateria,Intervalo,FechaCambio,FechaInspeccion,
																		   NivelCarga,EstadoB,UsuarioCrea,FechaCrea)
		VALUES(@correlativo,@idBateria,@CodBateria,@Intervalo,@FechaCambio,@FechaInspeccion,@NivelCarga,@EstadoB,@Usuario,GETDATE())
	END
	
	SET @Exito = '0 = Inspección registrada exitosamente.'
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