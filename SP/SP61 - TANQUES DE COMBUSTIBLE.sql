
-- CREAR TABLA ReportesApp_Operaciones_ControlItems_TanquesCombustible

-- CREAR TABLA ReportesApp_Operaciones_ControlItems_TanquesCombustible_Observaciones

-- CREAR TABLA ReportesApp_Operaciones_ControlItems_TimonesAsientos

----------------------------------------------------------------------
----------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 14-07-2024
-- Description:	LISTAR OBSERVACIONES INSPECCION
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_ListarObservacionesI]
@Opcion INT
AS
BEGIN
	IF (@Opcion = 1) BEGIN		-- LISTAR OBSERVACIONES DE COMBUSTIBLE
		SELECT idObservacion, Descripcion
		FROM ReportesApp_Operaciones_ControlItems_TanquesCombustible_Observaciones
		ORDER BY idObservacion
	END
END

----------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 12-11-2024
-- Description:	REGISTRAR Y EDITAR MOVIMIENTOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_RegistrarEditarTanques]
@Opcion INT,
@idRegistroTC INT,
@Programacion VARCHAR(30),
@FechaRevision DATE,
@idTracto INT,
@idCarreta INT,
@PersonaConductor INT,
@LugarInspeccion VARCHAR(350),
@PersonaInspector INT,
@Observacion VARCHAR(100),
@ImagenHallazgo VARBINARY(MAX),
@ImagenHallazgo2 VARBINARY(MAX),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @Contador INT 

BEGIN TRAN
BEGIN TRY
	IF (@Opcion IN (1,2) AND @idTracto = @idCarreta) BEGIN
		SET @Exito = '-1 = No puede asignar un tracto y un semirremolque a la vez.'
		ROLLBACK
		GOTO Terminar
	END

	IF (@Opcion = 1) BEGIN		-- CREAR TANQUE DE COMBUSTIBLE
		SET @Contador = (SELECT MAX(idRegistroTC) FROM ReportesApp_Operaciones_ControlItems_TanquesCombustible)
		SET @Contador = ISNULL(@Contador,0) + 1
		
		INSERT INTO ReportesApp_Operaciones_ControlItems_TanquesCombustible(idRegistroTC,Programacion,FechaRevision,idTracto,idCarreta,PersonaConductor,
		LugarInspeccion,PersonaInspector,Observacion,ImagenHallazgo,ImagenHallazgo2,Estado,UsuarioCreacion,FechaCreacion,UsuarioModificacion,FechaModificacion)
		VALUES(@Contador,@Programacion,@FechaRevision,@idTracto,@idCarreta,@PersonaConductor,@LugarInspeccion,@PersonaInspector,@Observacion,@ImagenHallazgo,
		@ImagenHallazgo2,'REVISADO',@Usuario, GETDATE(), @Usuario, GETDATE())
		
		SET @Exito = '0 = Revisión N° '+CONVERT(VARCHAR,@Contador)+' generada exitosamente.'
	END

	IF (@Opcion = 2) BEGIN		-- ACTUALIZAR TANQUE DE COMBUSTIBLE
		UPDATE ReportesApp_Operaciones_ControlItems_TanquesCombustible
		SET Programacion = @Programacion, FechaRevision = @FechaRevision, idTracto = @idTracto, idCarreta = @idCarreta, PersonaConductor = @PersonaConductor,
		LugarInspeccion = @LugarInspeccion, PersonaInspector = @PersonaInspector, Observacion = @Observacion, ImagenHallazgo = @ImagenHallazgo,
		ImagenHallazgo2 = @ImagenHallazgo2, UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()
		WHERE idRegistroTC = @idRegistroTC

		SET @Exito = '0 = Revisión N° '+CONVERT(VARCHAR,@idRegistroTC)+' actualizada exitosamente.'
	END

	IF (@Opcion = 3) BEGIN		-- ELIMINAR TANQUE DE COMBUSTIBLE
		DELETE FROM ReportesApp_Operaciones_ControlItems_TanquesCombustible
		WHERE idRegistroTC = @idRegistroTC

		SET @Exito = '0 = Revisión eliminada.'
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

----------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 12-11-2024
-- Description:	LISTAR TANQUES COMBUSTIBLE
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_ListarTanquesCombustible]
@Placa VARCHAR(50),
@Operacion VARCHAR(50),
@Conductor VARCHAR(250),
@Estado VARCHAR(50),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@Operacion = 'TODO' AND @Estado = 'TODOS') BEGIN
		SELECT TC.idRegistroTC AS 'ITEM', TC.Estado AS 'ESTADO', TC.Programacion AS 'OPERACION', TC.idTracto, V.NumeroPlaca AS 'TRACTO', TC.idCarreta,
		R.NumeroPlaca AS 'CARRETA', TC.PersonaConductor, LTRIM(RTRIM(C.Busqueda)) AS 'CONDUCTOR', TC.Observacion AS 'OBSERVACION',
		TC.LugarInspeccion AS 'LUGAR_INSPECCION', CONVERT(VARCHAR,TC.FechaRevision,103) AS 'FECHA_REVISION', TC.PersonaInspector,
		LTRIM(RTRIM(I.Busqueda)) AS 'INSPECTOR', TC.ImagenHallazgo, TC.ImagenHallazgo2, CONVERT(VARCHAR,TC.FechaReparacion,103) AS 'FECHA_REPARACION',
		TC.PersonaTecnico, LTRIM(RTRIM(T.Busqueda)) AS 'TECNICO', TC.ImagenReparacion, TC.PersonaSeguimiento, LTRIM(RTRIM(RS.Busqueda)) AS 'SEGUIMIENTO',
		TC.UsuarioCreacion, TC.FechaCreacion, TC.UsuarioModificacion, TC.FechaModificacion
		FROM ReportesApp_Operaciones_ControlItems_TanquesCombustible TC
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = TC.idTracto
		LEFT JOIN OP_TR_Vehiculo R WITH(NOLOCK) ON R.IdVehiculo = TC.idCarreta
		LEFT JOIN PersonaMast C WITH(NOLOCK) ON C.Persona = TC.PersonaConductor
		LEFT JOIN PersonaMast I WITH(NOLOCK) ON I.Persona = TC.PersonaInspector
		LEFT JOIN PersonaMast T WITH(NOLOCK) ON T.Persona = TC.PersonaTecnico
		LEFT JOIN PersonaMast RS WITH(NOLOCK) ON RS.Persona = TC.PersonaSeguimiento
		WHERE ((V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') OR (R.NumeroPlaca IS NULL OR R.NumeroPlaca LIKE '%' + @Placa + '%'))
		AND (LTRIM(RTRIM(C.Busqueda)) IS NULL OR LTRIM(RTRIM(C.Busqueda)) LIKE '%' + @Conductor + '%') AND (TC.FechaRevision BETWEEN @FINICIO AND @FFIN)
		ORDER BY TC.idRegistroTC DESC
	END

	IF (@Operacion = 'TODO' AND @Estado != 'TODOS') BEGIN
		SELECT TC.idRegistroTC AS 'ITEM', TC.Estado AS 'ESTADO', TC.Programacion AS 'OPERACION', TC.idTracto, V.NumeroPlaca AS 'TRACTO', TC.idCarreta,
		R.NumeroPlaca AS 'CARRETA', TC.PersonaConductor, LTRIM(RTRIM(C.Busqueda)) AS 'CONDUCTOR', TC.Observacion AS 'OBSERVACION',
		TC.LugarInspeccion AS 'LUGAR_INSPECCION', CONVERT(VARCHAR,TC.FechaRevision,103) AS 'FECHA_REVISION', TC.PersonaInspector,
		LTRIM(RTRIM(I.Busqueda)) AS 'INSPECTOR', TC.ImagenHallazgo, TC.ImagenHallazgo2, CONVERT(VARCHAR,TC.FechaReparacion,103) AS 'FECHA_REPARACION',
		TC.PersonaTecnico, LTRIM(RTRIM(T.Busqueda)) AS 'TECNICO', TC.ImagenReparacion, TC.PersonaSeguimiento, LTRIM(RTRIM(RS.Busqueda)) AS 'SEGUIMIENTO',
		TC.UsuarioCreacion, TC.FechaCreacion, TC.UsuarioModificacion, TC.FechaModificacion
		FROM ReportesApp_Operaciones_ControlItems_TanquesCombustible TC
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = TC.idTracto
		LEFT JOIN OP_TR_Vehiculo R WITH(NOLOCK) ON R.IdVehiculo = TC.idCarreta
		LEFT JOIN PersonaMast C WITH(NOLOCK) ON C.Persona = TC.PersonaConductor
		LEFT JOIN PersonaMast I WITH(NOLOCK) ON I.Persona = TC.PersonaInspector
		LEFT JOIN PersonaMast T WITH(NOLOCK) ON T.Persona = TC.PersonaTecnico
		LEFT JOIN PersonaMast RS WITH(NOLOCK) ON RS.Persona = TC.PersonaSeguimiento
		WHERE ((V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') OR (R.NumeroPlaca IS NULL OR R.NumeroPlaca LIKE '%' + @Placa + '%')) AND
		(LTRIM(RTRIM(C.Busqueda)) IS NULL OR LTRIM(RTRIM(C.Busqueda)) LIKE '%' + @Conductor + '%') AND (TC.FechaRevision BETWEEN @FINICIO AND @FFIN) AND
		(TC.Estado = @Estado)
		ORDER BY TC.idRegistroTC DESC
	END

	IF (@Operacion != 'TODO' AND @Estado = 'TODOS') BEGIN
		SELECT TC.idRegistroTC AS 'ITEM', TC.Estado AS 'ESTADO', TC.Programacion AS 'OPERACION', TC.idTracto, V.NumeroPlaca AS 'TRACTO', TC.idCarreta,
		R.NumeroPlaca AS 'CARRETA', TC.PersonaConductor, LTRIM(RTRIM(C.Busqueda)) AS 'CONDUCTOR', TC.Observacion AS 'OBSERVACION',
		TC.LugarInspeccion AS 'LUGAR_INSPECCION', CONVERT(VARCHAR,TC.FechaRevision,103) AS 'FECHA_REVISION', TC.PersonaInspector,
		LTRIM(RTRIM(I.Busqueda)) AS 'INSPECTOR', TC.ImagenHallazgo, TC.ImagenHallazgo2, CONVERT(VARCHAR,TC.FechaReparacion,103) AS 'FECHA_REPARACION',
		TC.PersonaTecnico, LTRIM(RTRIM(T.Busqueda)) AS 'TECNICO', TC.ImagenReparacion, TC.PersonaSeguimiento, LTRIM(RTRIM(RS.Busqueda)) AS 'SEGUIMIENTO',
		TC.UsuarioCreacion, TC.FechaCreacion, TC.UsuarioModificacion, TC.FechaModificacion
		FROM ReportesApp_Operaciones_ControlItems_TanquesCombustible TC
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = TC.idTracto
		LEFT JOIN OP_TR_Vehiculo R WITH(NOLOCK) ON R.IdVehiculo = TC.idCarreta
		LEFT JOIN PersonaMast C WITH(NOLOCK) ON C.Persona = TC.PersonaConductor
		LEFT JOIN PersonaMast I WITH(NOLOCK) ON I.Persona = TC.PersonaInspector
		LEFT JOIN PersonaMast T WITH(NOLOCK) ON T.Persona = TC.PersonaTecnico
		LEFT JOIN PersonaMast RS WITH(NOLOCK) ON RS.Persona = TC.PersonaSeguimiento
		WHERE ((V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') OR (R.NumeroPlaca IS NULL OR R.NumeroPlaca LIKE '%' + @Placa + '%')) AND
		(LTRIM(RTRIM(C.Busqueda)) IS NULL OR LTRIM(RTRIM(C.Busqueda)) LIKE '%' + @Conductor + '%') AND (TC.FechaRevision BETWEEN @FINICIO AND @FFIN) AND
		(TC.Programacion = @Operacion)
		ORDER BY TC.idRegistroTC DESC
	END

	IF (@Operacion != 'TODO' AND @Estado != 'TODOS') BEGIN
		SELECT TC.idRegistroTC AS 'ITEM', TC.Estado AS 'ESTADO', TC.Programacion AS 'OPERACION', TC.idTracto, V.NumeroPlaca AS 'TRACTO', TC.idCarreta,
		R.NumeroPlaca AS 'CARRETA', TC.PersonaConductor, LTRIM(RTRIM(C.Busqueda)) AS 'CONDUCTOR', TC.Observacion AS 'OBSERVACION',
		TC.LugarInspeccion AS 'LUGAR_INSPECCION', CONVERT(VARCHAR,TC.FechaRevision,103) AS 'FECHA_REVISION', TC.PersonaInspector,
		LTRIM(RTRIM(I.Busqueda)) AS 'INSPECTOR', TC.ImagenHallazgo, TC.ImagenHallazgo2, CONVERT(VARCHAR,TC.FechaReparacion,103) AS 'FECHA_REPARACION',
		TC.PersonaTecnico, LTRIM(RTRIM(T.Busqueda)) AS 'TECNICO', TC.ImagenReparacion, TC.PersonaSeguimiento, LTRIM(RTRIM(RS.Busqueda)) AS 'SEGUIMIENTO',
		TC.UsuarioCreacion, TC.FechaCreacion, TC.UsuarioModificacion, TC.FechaModificacion
		FROM ReportesApp_Operaciones_ControlItems_TanquesCombustible TC
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = TC.idTracto
		LEFT JOIN OP_TR_Vehiculo R WITH(NOLOCK) ON R.IdVehiculo = TC.idCarreta
		LEFT JOIN PersonaMast C WITH(NOLOCK) ON C.Persona = TC.PersonaConductor
		LEFT JOIN PersonaMast I WITH(NOLOCK) ON I.Persona = TC.PersonaInspector
		LEFT JOIN PersonaMast T WITH(NOLOCK) ON T.Persona = TC.PersonaTecnico
		LEFT JOIN PersonaMast RS WITH(NOLOCK) ON RS.Persona = TC.PersonaSeguimiento
		WHERE ((V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') OR (R.NumeroPlaca IS NULL OR R.NumeroPlaca LIKE '%' + @Placa + '%')) AND
		(LTRIM(RTRIM(C.Busqueda)) IS NULL OR LTRIM(RTRIM(C.Busqueda)) LIKE '%' + @Conductor + '%') AND (TC.FechaRevision BETWEEN @FINICIO AND @FFIN) AND
		(TC.Programacion = @Operacion) AND (TC.Estado = @Estado)
		ORDER BY TC.idRegistroTC DESC
	END
END

----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 16-11-2024
-- Description:	AÑADIR REPARACION DE TANQUES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_RegistrarReparacion]
@idRegistroTC INT,
@PersonaTecnico INT,
@PersonaSeguimiento INT,
@FechaReparacion DATE,
@ImagenReparacion VARBINARY(MAX),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	DECLARE @FechaRevision DATE = (SELECT FechaRevision FROM ReportesApp_Operaciones_ControlItems_TanquesCombustible WHERE idRegistroTC = @idRegistroTC)
	
	IF (@FechaReparacion < @FechaRevision) BEGIN
		SET @Exito = '-1 = La fecha de reparación no puede ser menor a la fecha de revisión.'
		ROLLBACK
		GOTO Terminar
	END
	ELSE BEGIN
		UPDATE ReportesApp_Operaciones_ControlItems_TanquesCombustible
		SET PersonaTecnico = @PersonaTecnico, PersonaSeguimiento = @PersonaSeguimiento, FechaReparacion = @FechaReparacion, ImagenReparacion = @ImagenReparacion,
		Estado = 'REPARADO', UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()
		WHERE idRegistroTC = @idRegistroTC
		
		SET @Exito = '0 = Revisión N° '+CONVERT(VARCHAR,@idRegistroTC)+' reparada exitosamente.'
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

----------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 21-11-2024
-- Description:	REGISTRAR Y EDITAR TIMONES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_RegistrarEditarAsientos]
@Opcion INT,
@idRegistroTA INT,
@Programacion VARCHAR(30),
@TipoAT VARCHAR(80),
@FechaRevision DATE,
@idTracto INT,
@PersonaConductor INT,
@LugarInspeccion VARCHAR(350),
@PersonaInspector INT,
@ImagenHallazgo VARBINARY(MAX),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @Contador INT 

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- CREAR ASIENTO Y TIMON
		SET @Contador = (SELECT MAX(idRegistroTA) FROM ReportesApp_Operaciones_ControlItems_TimonesAsientos)
		SET @Contador = ISNULL(@Contador,0) + 1
		
		INSERT INTO ReportesApp_Operaciones_ControlItems_TimonesAsientos(idRegistroTA,Programacion,TipoAT,FechaRevision,idTracto,PersonaConductor,
		LugarInspeccion,PersonaInspector,ImagenHallazgo,Estado,UsuarioCreacion,FechaCreacion,UsuarioModificacion,FechaModificacion)
		VALUES(@Contador,@Programacion,@TipoAT,@FechaRevision,@idTracto,@PersonaConductor,@LugarInspeccion,@PersonaInspector,@ImagenHallazgo,'REVISADO',
		@Usuario, GETDATE(), @Usuario, GETDATE())
		
		SET @Exito = '0 = Revisión N° '+CONVERT(VARCHAR,@Contador)+' generada exitosamente.'
	END

	IF (@Opcion = 2) BEGIN		-- ACTUALIZAR ASIENTO Y TIMON
		UPDATE ReportesApp_Operaciones_ControlItems_TimonesAsientos
		SET Programacion = @Programacion, TipoAT = @TipoAT, FechaRevision = @FechaRevision, idTracto = @idTracto, PersonaConductor = @PersonaConductor,
		LugarInspeccion = @LugarInspeccion, PersonaInspector = @PersonaInspector, ImagenHallazgo = @ImagenHallazgo, UsuarioModificacion = @Usuario,
		FechaModificacion = GETDATE()
		WHERE idRegistroTA = @idRegistroTA

		SET @Exito = '0 = Revisión N° '+CONVERT(VARCHAR,@idRegistroTA)+' actualizada exitosamente.'
	END

	IF (@Opcion = 3) BEGIN		-- ELIMINAR ASIENTO Y TIMON
		DELETE FROM ReportesApp_Operaciones_ControlItems_TimonesAsientos
		WHERE idRegistroTA = @idRegistroTA

		SET @Exito = '0 = Revisión eliminada.'
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

--------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 21-11-2024
-- Description:	LISTAR TIMONES Y ASIENTOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_ListarAsientosTimones]
@TipoAT VARCHAR(80),
@Placa VARCHAR(50),
@Operacion VARCHAR(50),
@Conductor VARCHAR(250),
@Estado VARCHAR(50),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@TipoAT = 'TODOS' AND @Operacion = 'TODO' AND @Estado = 'TODOS') BEGIN
		SELECT TA.idRegistroTA AS 'ITEM', TA.Estado AS 'ESTADO', TA.TipoAT AS 'TIPO', TA.Programacion AS 'OPERACION', TA.idTracto, V.NumeroPlaca AS 'TRACTO',
		TA.PersonaConductor, LTRIM(RTRIM(C.Busqueda)) AS 'CONDUCTOR', TA.LugarInspeccion AS 'LUGAR_INSPECCION', CONVERT(VARCHAR,TA.FechaRevision,103) AS 'FECHA_REVISION',
		TA.PersonaInspector, LTRIM(RTRIM(I.Busqueda)) AS 'INSPECTOR', TA.ImagenHallazgo, CONVERT(VARCHAR,TA.FechaReparacion,103) AS 'FECHA_REPARACION',
		TA.PersonaProveedor, LTRIM(RTRIM(P.Busqueda)) AS 'PROVEEDOR', TA.ImagenReparacion, TA.PersonaSeguimiento, LTRIM(RTRIM(RS.Busqueda)) AS 'SEGUIMIENTO',
		TA.UsuarioCreacion, TA.FechaCreacion, TA.UsuarioModificacion, TA.FechaModificacion
		FROM ReportesApp_Operaciones_ControlItems_TimonesAsientos TA
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = TA.idTracto
		LEFT JOIN PersonaMast C WITH(NOLOCK) ON C.Persona = TA.PersonaConductor
		LEFT JOIN PersonaMast I WITH(NOLOCK) ON I.Persona = TA.PersonaInspector
		LEFT JOIN PersonaMast P WITH(NOLOCK) ON P.Persona = TA.PersonaProveedor
		LEFT JOIN PersonaMast RS WITH(NOLOCK) ON RS.Persona = TA.PersonaSeguimiento
		WHERE (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND
		(LTRIM(RTRIM(C.Busqueda)) IS NULL OR LTRIM(RTRIM(C.Busqueda)) LIKE '%' + @Conductor + '%') AND (TA.FechaRevision BETWEEN @FINICIO AND @FFIN)
		ORDER BY TA.idRegistroTA DESC
	END

	IF (@TipoAT = 'TODOS' AND @Operacion = 'TODO' AND @Estado != 'TODOS') BEGIN
		SELECT TA.idRegistroTA AS 'ITEM', TA.Estado AS 'ESTADO', TA.TipoAT AS 'TIPO', TA.Programacion AS 'OPERACION', TA.idTracto, V.NumeroPlaca AS 'TRACTO',
		TA.PersonaConductor, LTRIM(RTRIM(C.Busqueda)) AS 'CONDUCTOR', TA.LugarInspeccion AS 'LUGAR_INSPECCION', CONVERT(VARCHAR,TA.FechaRevision,103) AS 'FECHA_REVISION',
		TA.PersonaInspector, LTRIM(RTRIM(I.Busqueda)) AS 'INSPECTOR', TA.ImagenHallazgo, CONVERT(VARCHAR,TA.FechaReparacion,103) AS 'FECHA_REPARACION',
		TA.PersonaProveedor, LTRIM(RTRIM(P.Busqueda)) AS 'PROVEEDOR', TA.ImagenReparacion, TA.PersonaSeguimiento, LTRIM(RTRIM(RS.Busqueda)) AS 'SEGUIMIENTO',
		TA.UsuarioCreacion, TA.FechaCreacion, TA.UsuarioModificacion, TA.FechaModificacion
		FROM ReportesApp_Operaciones_ControlItems_TimonesAsientos TA
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = TA.idTracto
		LEFT JOIN PersonaMast C WITH(NOLOCK) ON C.Persona = TA.PersonaConductor
		LEFT JOIN PersonaMast I WITH(NOLOCK) ON I.Persona = TA.PersonaInspector
		LEFT JOIN PersonaMast P WITH(NOLOCK) ON P.Persona = TA.PersonaProveedor
		LEFT JOIN PersonaMast RS WITH(NOLOCK) ON RS.Persona = TA.PersonaSeguimiento
		WHERE (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND (TA.Estado = @Estado) AND
		(LTRIM(RTRIM(C.Busqueda)) IS NULL OR LTRIM(RTRIM(C.Busqueda)) LIKE '%' + @Conductor + '%') AND (TA.FechaRevision BETWEEN @FINICIO AND @FFIN)
		ORDER BY TA.idRegistroTA DESC
	END

	IF (@TipoAT = 'TODOS' AND @Operacion != 'TODO' AND @Estado = 'TODOS') BEGIN
		SELECT TA.idRegistroTA AS 'ITEM', TA.Estado AS 'ESTADO', TA.TipoAT AS 'TIPO', TA.Programacion AS 'OPERACION', TA.idTracto, V.NumeroPlaca AS 'TRACTO',
		TA.PersonaConductor, LTRIM(RTRIM(C.Busqueda)) AS 'CONDUCTOR', TA.LugarInspeccion AS 'LUGAR_INSPECCION', CONVERT(VARCHAR,TA.FechaRevision,103) AS 'FECHA_REVISION',
		TA.PersonaInspector, LTRIM(RTRIM(I.Busqueda)) AS 'INSPECTOR', TA.ImagenHallazgo, CONVERT(VARCHAR,TA.FechaReparacion,103) AS 'FECHA_REPARACION',
		TA.PersonaProveedor, LTRIM(RTRIM(P.Busqueda)) AS 'PROVEEDOR', TA.ImagenReparacion, TA.PersonaSeguimiento, LTRIM(RTRIM(RS.Busqueda)) AS 'SEGUIMIENTO',
		TA.UsuarioCreacion, TA.FechaCreacion, TA.UsuarioModificacion, TA.FechaModificacion
		FROM ReportesApp_Operaciones_ControlItems_TimonesAsientos TA
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = TA.idTracto
		LEFT JOIN PersonaMast C WITH(NOLOCK) ON C.Persona = TA.PersonaConductor
		LEFT JOIN PersonaMast I WITH(NOLOCK) ON I.Persona = TA.PersonaInspector
		LEFT JOIN PersonaMast P WITH(NOLOCK) ON P.Persona = TA.PersonaProveedor
		LEFT JOIN PersonaMast RS WITH(NOLOCK) ON RS.Persona = TA.PersonaSeguimiento
		WHERE (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND (TA.Programacion = @Operacion) AND 
		(LTRIM(RTRIM(C.Busqueda)) IS NULL OR LTRIM(RTRIM(C.Busqueda)) LIKE '%' + @Conductor + '%') AND (TA.FechaRevision BETWEEN @FINICIO AND @FFIN)
		ORDER BY TA.idRegistroTA DESC
	END

	IF (@TipoAT = 'TODOS' AND @Operacion != 'TODO' AND @Estado != 'TODOS') BEGIN
		SELECT TA.idRegistroTA AS 'ITEM', TA.Estado AS 'ESTADO', TA.TipoAT AS 'TIPO', TA.Programacion AS 'OPERACION', TA.idTracto, V.NumeroPlaca AS 'TRACTO',
		TA.PersonaConductor, LTRIM(RTRIM(C.Busqueda)) AS 'CONDUCTOR', TA.LugarInspeccion AS 'LUGAR_INSPECCION', CONVERT(VARCHAR,TA.FechaRevision,103) AS 'FECHA_REVISION',
		TA.PersonaInspector, LTRIM(RTRIM(I.Busqueda)) AS 'INSPECTOR', TA.ImagenHallazgo, CONVERT(VARCHAR,TA.FechaReparacion,103) AS 'FECHA_REPARACION',
		TA.PersonaProveedor, LTRIM(RTRIM(P.Busqueda)) AS 'PROVEEDOR', TA.ImagenReparacion, TA.PersonaSeguimiento, LTRIM(RTRIM(RS.Busqueda)) AS 'SEGUIMIENTO',
		TA.UsuarioCreacion, TA.FechaCreacion, TA.UsuarioModificacion, TA.FechaModificacion
		FROM ReportesApp_Operaciones_ControlItems_TimonesAsientos TA
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = TA.idTracto
		LEFT JOIN PersonaMast C WITH(NOLOCK) ON C.Persona = TA.PersonaConductor
		LEFT JOIN PersonaMast I WITH(NOLOCK) ON I.Persona = TA.PersonaInspector
		LEFT JOIN PersonaMast P WITH(NOLOCK) ON P.Persona = TA.PersonaProveedor
		LEFT JOIN PersonaMast RS WITH(NOLOCK) ON RS.Persona = TA.PersonaSeguimiento
		WHERE (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND (TA.Estado = @Estado) AND (TA.Programacion = @Operacion) AND 
		(LTRIM(RTRIM(C.Busqueda)) IS NULL OR LTRIM(RTRIM(C.Busqueda)) LIKE '%' + @Conductor + '%') AND (TA.FechaRevision BETWEEN @FINICIO AND @FFIN)
		ORDER BY TA.idRegistroTA DESC
	END

	IF (@TipoAT != 'TODOS' AND @Operacion = 'TODO' AND @Estado = 'TODOS') BEGIN
		SELECT TA.idRegistroTA AS 'ITEM', TA.Estado AS 'ESTADO', TA.TipoAT AS 'TIPO', TA.Programacion AS 'OPERACION', TA.idTracto, V.NumeroPlaca AS 'TRACTO',
		TA.PersonaConductor, LTRIM(RTRIM(C.Busqueda)) AS 'CONDUCTOR', TA.LugarInspeccion AS 'LUGAR_INSPECCION', CONVERT(VARCHAR,TA.FechaRevision,103) AS 'FECHA_REVISION',
		TA.PersonaInspector, LTRIM(RTRIM(I.Busqueda)) AS 'INSPECTOR', TA.ImagenHallazgo, CONVERT(VARCHAR,TA.FechaReparacion,103) AS 'FECHA_REPARACION',
		TA.PersonaProveedor, LTRIM(RTRIM(P.Busqueda)) AS 'PROVEEDOR', TA.ImagenReparacion, TA.PersonaSeguimiento, LTRIM(RTRIM(RS.Busqueda)) AS 'SEGUIMIENTO',
		TA.UsuarioCreacion, TA.FechaCreacion, TA.UsuarioModificacion, TA.FechaModificacion
		FROM ReportesApp_Operaciones_ControlItems_TimonesAsientos TA
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = TA.idTracto
		LEFT JOIN PersonaMast C WITH(NOLOCK) ON C.Persona = TA.PersonaConductor
		LEFT JOIN PersonaMast I WITH(NOLOCK) ON I.Persona = TA.PersonaInspector
		LEFT JOIN PersonaMast P WITH(NOLOCK) ON P.Persona = TA.PersonaProveedor
		LEFT JOIN PersonaMast RS WITH(NOLOCK) ON RS.Persona = TA.PersonaSeguimiento
		WHERE (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND (TA.TipoAT = @TipoAT) AND 
		(LTRIM(RTRIM(C.Busqueda)) IS NULL OR LTRIM(RTRIM(C.Busqueda)) LIKE '%' + @Conductor + '%') AND (TA.FechaRevision BETWEEN @FINICIO AND @FFIN)
		ORDER BY TA.idRegistroTA DESC
	END

	IF (@TipoAT != 'TODOS' AND @Operacion = 'TODO' AND @Estado != 'TODOS') BEGIN
		SELECT TA.idRegistroTA AS 'ITEM', TA.Estado AS 'ESTADO', TA.TipoAT AS 'TIPO', TA.Programacion AS 'OPERACION', TA.idTracto, V.NumeroPlaca AS 'TRACTO',
		TA.PersonaConductor, LTRIM(RTRIM(C.Busqueda)) AS 'CONDUCTOR', TA.LugarInspeccion AS 'LUGAR_INSPECCION', CONVERT(VARCHAR,TA.FechaRevision,103) AS 'FECHA_REVISION',
		TA.PersonaInspector, LTRIM(RTRIM(I.Busqueda)) AS 'INSPECTOR', TA.ImagenHallazgo, CONVERT(VARCHAR,TA.FechaReparacion,103) AS 'FECHA_REPARACION',
		TA.PersonaProveedor, LTRIM(RTRIM(P.Busqueda)) AS 'PROVEEDOR', TA.ImagenReparacion, TA.PersonaSeguimiento, LTRIM(RTRIM(RS.Busqueda)) AS 'SEGUIMIENTO',
		TA.UsuarioCreacion, TA.FechaCreacion, TA.UsuarioModificacion, TA.FechaModificacion
		FROM ReportesApp_Operaciones_ControlItems_TimonesAsientos TA
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = TA.idTracto
		LEFT JOIN PersonaMast C WITH(NOLOCK) ON C.Persona = TA.PersonaConductor
		LEFT JOIN PersonaMast I WITH(NOLOCK) ON I.Persona = TA.PersonaInspector
		LEFT JOIN PersonaMast P WITH(NOLOCK) ON P.Persona = TA.PersonaProveedor
		LEFT JOIN PersonaMast RS WITH(NOLOCK) ON RS.Persona = TA.PersonaSeguimiento
		WHERE (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND (TA.TipoAT = @TipoAT) AND (TA.Estado = @Estado) AND
		(LTRIM(RTRIM(C.Busqueda)) IS NULL OR LTRIM(RTRIM(C.Busqueda)) LIKE '%' + @Conductor + '%') AND (TA.FechaRevision BETWEEN @FINICIO AND @FFIN)
		ORDER BY TA.idRegistroTA DESC
	END

	IF (@TipoAT != 'TODOS' AND @Operacion != 'TODO' AND @Estado = 'TODOS') BEGIN
		SELECT TA.idRegistroTA AS 'ITEM', TA.Estado AS 'ESTADO', TA.TipoAT AS 'TIPO', TA.Programacion AS 'OPERACION', TA.idTracto, V.NumeroPlaca AS 'TRACTO',
		TA.PersonaConductor, LTRIM(RTRIM(C.Busqueda)) AS 'CONDUCTOR', TA.LugarInspeccion AS 'LUGAR_INSPECCION', CONVERT(VARCHAR,TA.FechaRevision,103) AS 'FECHA_REVISION',
		TA.PersonaInspector, LTRIM(RTRIM(I.Busqueda)) AS 'INSPECTOR', TA.ImagenHallazgo, CONVERT(VARCHAR,TA.FechaReparacion,103) AS 'FECHA_REPARACION',
		TA.PersonaProveedor, LTRIM(RTRIM(P.Busqueda)) AS 'PROVEEDOR', TA.ImagenReparacion, TA.PersonaSeguimiento, LTRIM(RTRIM(RS.Busqueda)) AS 'SEGUIMIENTO',
		TA.UsuarioCreacion, TA.FechaCreacion, TA.UsuarioModificacion, TA.FechaModificacion
		FROM ReportesApp_Operaciones_ControlItems_TimonesAsientos TA
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = TA.idTracto
		LEFT JOIN PersonaMast C WITH(NOLOCK) ON C.Persona = TA.PersonaConductor
		LEFT JOIN PersonaMast I WITH(NOLOCK) ON I.Persona = TA.PersonaInspector
		LEFT JOIN PersonaMast P WITH(NOLOCK) ON P.Persona = TA.PersonaProveedor
		LEFT JOIN PersonaMast RS WITH(NOLOCK) ON RS.Persona = TA.PersonaSeguimiento
		WHERE (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND (TA.TipoAT = @TipoAT) AND (TA.Programacion = @Operacion) AND 
		(LTRIM(RTRIM(C.Busqueda)) IS NULL OR LTRIM(RTRIM(C.Busqueda)) LIKE '%' + @Conductor + '%') AND (TA.FechaRevision BETWEEN @FINICIO AND @FFIN)
		ORDER BY TA.idRegistroTA DESC
	END

	IF (@TipoAT != 'TODOS' AND @Operacion != 'TODO' AND @Estado != 'TODOS') BEGIN
		SELECT TA.idRegistroTA AS 'ITEM', TA.Estado AS 'ESTADO', TA.TipoAT AS 'TIPO', TA.Programacion AS 'OPERACION', TA.idTracto, V.NumeroPlaca AS 'TRACTO',
		TA.PersonaConductor, LTRIM(RTRIM(C.Busqueda)) AS 'CONDUCTOR', TA.LugarInspeccion AS 'LUGAR_INSPECCION', CONVERT(VARCHAR,TA.FechaRevision,103) AS 'FECHA_REVISION',
		TA.PersonaInspector, LTRIM(RTRIM(I.Busqueda)) AS 'INSPECTOR', TA.ImagenHallazgo, CONVERT(VARCHAR,TA.FechaReparacion,103) AS 'FECHA_REPARACION',
		TA.PersonaProveedor, LTRIM(RTRIM(P.Busqueda)) AS 'PROVEEDOR', TA.ImagenReparacion, TA.PersonaSeguimiento, LTRIM(RTRIM(RS.Busqueda)) AS 'SEGUIMIENTO',
		TA.UsuarioCreacion, TA.FechaCreacion, TA.UsuarioModificacion, TA.FechaModificacion
		FROM ReportesApp_Operaciones_ControlItems_TimonesAsientos TA
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = TA.idTracto
		LEFT JOIN PersonaMast C WITH(NOLOCK) ON C.Persona = TA.PersonaConductor
		LEFT JOIN PersonaMast I WITH(NOLOCK) ON I.Persona = TA.PersonaInspector
		LEFT JOIN PersonaMast P WITH(NOLOCK) ON P.Persona = TA.PersonaProveedor
		LEFT JOIN PersonaMast RS WITH(NOLOCK) ON RS.Persona = TA.PersonaSeguimiento
		WHERE (V.NumeroPlaca IS NULL OR V.NumeroPlaca LIKE '%' + @Placa + '%') AND (TA.TipoAT = @TipoAT) AND (TA.Estado = @Estado) AND (TA.Programacion = @Operacion)
		AND (LTRIM(RTRIM(C.Busqueda)) IS NULL OR LTRIM(RTRIM(C.Busqueda)) LIKE '%' + @Conductor + '%') AND (TA.FechaRevision BETWEEN @FINICIO AND @FFIN)
		ORDER BY TA.idRegistroTA DESC
	END
END

-----------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 22-11-2024
-- Description:	AÑADIR REPARACION DE ASIENTOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_RepararAsientos]
@idRegistroTA INT,
@PersonaProveedor INT,
@PersonaSeguimiento INT,
@FechaReparacion DATE,
@ImagenReparacion VARBINARY(MAX),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	DECLARE @FechaRevision DATE = (SELECT FechaRevision FROM ReportesApp_Operaciones_ControlItems_TimonesAsientos WHERE idRegistroTA = @idRegistroTA)
	
	IF (@FechaReparacion < @FechaRevision) BEGIN
		SET @Exito = '-1 = La fecha de reparación no puede ser menor a la fecha de revisión.'
		ROLLBACK
		GOTO Terminar
	END
	ELSE BEGIN
		UPDATE ReportesApp_Operaciones_ControlItems_TimonesAsientos
		SET PersonaProveedor = @PersonaProveedor, PersonaSeguimiento = @PersonaSeguimiento, FechaReparacion = @FechaReparacion, ImagenReparacion = @ImagenReparacion,
		Estado = 'REPARADO', UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()
		WHERE idRegistroTA = @idRegistroTA
		
		SET @Exito = '0 = Revisión N° '+CONVERT(VARCHAR,@idRegistroTA)+' reparada exitosamente.'
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