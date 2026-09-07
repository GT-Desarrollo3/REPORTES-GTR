
-- CREAR TABLA ReportesApp_Operaciones_ControlItems_ConosTacos

------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 11-11-2025
-- Description:	REGISTRAR Y EDITAR CONOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_RegistrarEditarCT]
@Opcion INT,
@idRegistroCT INT,
@idTracto INT,
@PersonaC INT,
@FechaRevision DATE,
@TipoCT VARCHAR(80),
@Cantidad INT,
@Estado VARCHAR(50),
@LugarRevision VARCHAR(350),
@PersonaR INT,
@ImagenHallazgo VARBINARY(MAX),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @Contador INT 

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- CREAR CONOS Y TACOS
		SET @Contador = (SELECT MAX(idRegistroCT) FROM ReportesApp_Operaciones_ControlItems_ConosTacos)
		SET @Contador = ISNULL(@Contador,0) + 1
		
		INSERT INTO ReportesApp_Operaciones_ControlItems_ConosTacos(idRegistroCT,EstadoCT,idTracto,PersonaC,FechaRevision,TipoCT,Cantidad,Estado,LugarRevision,
		PersonaR,ImagenHallazgo,UsuarioCreacion,FechaCreacion,UsuarioModificacion,FechaModificacion)
		VALUES(@Contador,'PENDIENTE',@idTracto,@PersonaC,@FechaRevision,@TipoCT,@Cantidad,@Estado,@LugarRevision,@PersonaR,@ImagenHallazgo,@Usuario,
		GETDATE(), @Usuario, GETDATE())
		
		SET @Exito = '0 = Revisión N° '+CONVERT(VARCHAR,@Contador)+' generada exitosamente.'
	END

	IF (@Opcion = 2) BEGIN		-- ACTUALIZAR CONOS Y TACOS
		UPDATE ReportesApp_Operaciones_ControlItems_ConosTacos
		SET idTracto = @idTracto, PersonaC = @PersonaC, FechaRevision = @FechaRevision, TipoCT = @TipoCT, Cantidad = @Cantidad, Estado = @Estado,
		LugarRevision = @LugarRevision, PersonaR = @PersonaR, ImagenHallazgo = @ImagenHallazgo, UsuarioModificacion = @Usuario,
		FechaModificacion = GETDATE()
		WHERE idRegistroCT = @idRegistroCT

		SET @Exito = '0 = Revisión N° '+CONVERT(VARCHAR,@idRegistroCT)+' actualizada exitosamente.'
	END

	IF (@Opcion = 3) BEGIN		-- ELIMINAR CONOS Y TACOS
		DELETE FROM ReportesApp_Operaciones_ControlItems_ConosTacos
		WHERE idRegistroCT = @idRegistroCT

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

------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 11-11-2025
-- Description:	AÑADIR REPARACION DE CONOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_RepararCT]
@idRegistroCT INT,
@FechaReparacion DATE,
@ImagenReparacion VARBINARY(MAX),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	DECLARE @FechaRevision DATE = (SELECT FechaRevision FROM ReportesApp_Operaciones_ControlItems_ConosTacos WHERE idRegistroCT = @idRegistroCT)
	
	IF (@FechaReparacion < @FechaRevision) BEGIN
		SET @Exito = '-1 = La fecha de reparación no puede ser menor a la fecha de revisión.'
		ROLLBACK
		GOTO Terminar
	END
	ELSE BEGIN
		UPDATE ReportesApp_Operaciones_ControlItems_ConosTacos
		SET FechaReparacion = @FechaReparacion, ImagenReparacion = @ImagenReparacion,
		EstadoCT = 'REVISADO', UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()
		WHERE idRegistroCT = @idRegistroCT
		
		SET @Exito = '0 = Revisión N° '+CONVERT(VARCHAR,@idRegistroCT)+' reparada exitosamente.'
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

------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 11-11-2025
-- Description:	LISTAR CONOS Y TACOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_ControlItems_ListarConosTacos]
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@Conductor VARCHAR(250),
@Placa VARCHAR(50),
@TipoCT VARCHAR(80),
@Estado VARCHAR(50)
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@TipoCT = 'TODOS' AND @Estado = 'TODOS') BEGIN
		SELECT CT.idRegistroCT AS 'NRO', CT.EstadoCT AS 'ESTADO', CT.idTracto, V.NumeroPlaca AS 'TRACTO', O.Descripcion AS 'OPERACION', CT.PersonaC,
		LTRIM(RTRIM(C.Busqueda)) AS 'CONDUCTOR', CT.LugarRevision AS 'LUGAR_REVISION', CONVERT(VARCHAR,CT.FechaRevision,103) AS 'FECHA_REVISION',
		CT.PersonaR, LTRIM(RTRIM(R.Busqueda)) AS 'RESPONSABLE', CT.TipoCT AS 'TIPO', CT.Cantidad AS 'CANTIDAD', CT.Estado AS 'ESTADO_ITEMS',
		CT.ImagenHallazgo, CONVERT(VARCHAR,CT.FechaReparacion,103) AS 'FECHA_REPARACION', CT.ImagenReparacion, CT.UsuarioCreacion, CT.FechaCreacion,
		CT.UsuarioModificacion, CT.FechaModificacion
		FROM ReportesApp_Operaciones_ControlItems_ConosTacos CT
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = CT.idTracto
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON  O.IdOperacion = UC.IdProgramacion 
		LEFT JOIN PersonaMast C WITH(NOLOCK) ON C.Persona = CT.PersonaC
		LEFT JOIN PersonaMast R WITH(NOLOCK) ON R.Persona = CT.PersonaR
		WHERE (LTRIM(RTRIM(C.Busqueda)) IS NULL OR LTRIM(RTRIM(C.Busqueda)) LIKE '%' + @Conductor + '%') AND (V.NumeroPlaca IS NULL OR
		V.NumeroPlaca LIKE '%' + @Placa + '%') AND (CT.FechaRevision BETWEEN @FINICIO AND @FFIN)
		ORDER BY CT.idRegistroCT DESC
	END

	IF (@TipoCT != 'TODOS' AND @Estado = 'TODOS') BEGIN
		SELECT CT.idRegistroCT AS 'NRO', CT.EstadoCT AS 'ESTADO', CT.idTracto, V.NumeroPlaca AS 'TRACTO', O.Descripcion AS 'OPERACION', CT.PersonaC,
		LTRIM(RTRIM(C.Busqueda)) AS 'CONDUCTOR', CT.LugarRevision AS 'LUGAR_REVISION', CONVERT(VARCHAR,CT.FechaRevision,103) AS 'FECHA_REVISION',
		CT.PersonaR, LTRIM(RTRIM(R.Busqueda)) AS 'RESPONSABLE', CT.TipoCT AS 'TIPO', CT.Cantidad AS 'CANTIDAD', CT.Estado AS 'ESTADO_ITEMS',
		CT.ImagenHallazgo, CONVERT(VARCHAR,CT.FechaReparacion,103) AS 'FECHA_REPARACION', CT.ImagenReparacion, CT.UsuarioCreacion, CT.FechaCreacion,
		CT.UsuarioModificacion, CT.FechaModificacion
		FROM ReportesApp_Operaciones_ControlItems_ConosTacos CT
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = CT.idTracto
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON  O.IdOperacion = UC.IdProgramacion 
		LEFT JOIN PersonaMast C WITH(NOLOCK) ON C.Persona = CT.PersonaC
		LEFT JOIN PersonaMast R WITH(NOLOCK) ON R.Persona = CT.PersonaR
		WHERE (LTRIM(RTRIM(C.Busqueda)) IS NULL OR LTRIM(RTRIM(C.Busqueda)) LIKE '%' + @Conductor + '%') AND (V.NumeroPlaca IS NULL OR
		V.NumeroPlaca LIKE '%' + @Placa + '%') AND (CT.FechaRevision BETWEEN @FINICIO AND @FFIN) AND (CT.TipoCT = @TipoCT)
		ORDER BY CT.idRegistroCT DESC
	END

	IF (@TipoCT = 'TODOS' AND @Estado != 'TODOS') BEGIN
		SELECT CT.idRegistroCT AS 'NRO', CT.EstadoCT AS 'ESTADO', CT.idTracto, V.NumeroPlaca AS 'TRACTO', O.Descripcion AS 'OPERACION', CT.PersonaC,
		LTRIM(RTRIM(C.Busqueda)) AS 'CONDUCTOR', CT.LugarRevision AS 'LUGAR_REVISION', CONVERT(VARCHAR,CT.FechaRevision,103) AS 'FECHA_REVISION',
		CT.PersonaR, LTRIM(RTRIM(R.Busqueda)) AS 'RESPONSABLE', CT.TipoCT AS 'TIPO', CT.Cantidad AS 'CANTIDAD', CT.Estado AS 'ESTADO_ITEMS',
		CT.ImagenHallazgo, CONVERT(VARCHAR,CT.FechaReparacion,103) AS 'FECHA_REPARACION', CT.ImagenReparacion, CT.UsuarioCreacion, CT.FechaCreacion,
		CT.UsuarioModificacion, CT.FechaModificacion
		FROM ReportesApp_Operaciones_ControlItems_ConosTacos CT
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = CT.idTracto
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON  O.IdOperacion = UC.IdProgramacion 
		LEFT JOIN PersonaMast C WITH(NOLOCK) ON C.Persona = CT.PersonaC
		LEFT JOIN PersonaMast R WITH(NOLOCK) ON R.Persona = CT.PersonaR
		WHERE (LTRIM(RTRIM(C.Busqueda)) IS NULL OR LTRIM(RTRIM(C.Busqueda)) LIKE '%' + @Conductor + '%') AND (V.NumeroPlaca IS NULL OR
		V.NumeroPlaca LIKE '%' + @Placa + '%') AND (CT.FechaRevision BETWEEN @FINICIO AND @FFIN) AND (CT.EstadoCT = @Estado)
		ORDER BY CT.idRegistroCT DESC
	END

	IF (@TipoCT != 'TODOS' AND @Estado != 'TODOS') BEGIN
		SELECT CT.idRegistroCT AS 'NRO', CT.EstadoCT AS 'ESTADO', CT.idTracto, V.NumeroPlaca AS 'TRACTO', O.Descripcion AS 'OPERACION', CT.PersonaC,
		LTRIM(RTRIM(C.Busqueda)) AS 'CONDUCTOR', CT.LugarRevision AS 'LUGAR_REVISION', CONVERT(VARCHAR,CT.FechaRevision,103) AS 'FECHA_REVISION',
		CT.PersonaR, LTRIM(RTRIM(R.Busqueda)) AS 'RESPONSABLE', CT.TipoCT AS 'TIPO', CT.Cantidad AS 'CANTIDAD', CT.Estado AS 'ESTADO_ITEMS',
		CT.ImagenHallazgo, CONVERT(VARCHAR,CT.FechaReparacion,103) AS 'FECHA_REPARACION', CT.ImagenReparacion, CT.UsuarioCreacion, CT.FechaCreacion,
		CT.UsuarioModificacion, CT.FechaModificacion
		FROM ReportesApp_Operaciones_ControlItems_ConosTacos CT
		LEFT JOIN OP_TR_Vehiculo V WITH(NOLOCK) ON V.IdVehiculo = CT.idTracto
		LEFT JOIN ReportesApp_Operacion_MaestroUnidadesConductor UC ON UC.IdUnidad = V.IdVehiculo
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON  O.IdOperacion = UC.IdProgramacion 
		LEFT JOIN PersonaMast C WITH(NOLOCK) ON C.Persona = CT.PersonaC
		LEFT JOIN PersonaMast R WITH(NOLOCK) ON R.Persona = CT.PersonaR
		WHERE (LTRIM(RTRIM(C.Busqueda)) IS NULL OR LTRIM(RTRIM(C.Busqueda)) LIKE '%' + @Conductor + '%') AND (V.NumeroPlaca IS NULL OR
		V.NumeroPlaca LIKE '%' + @Placa + '%') AND (CT.FechaRevision BETWEEN @FINICIO AND @FFIN) AND (CT.TipoCT = @TipoCT) AND (CT.EstadoCT = @Estado)
		ORDER BY CT.idRegistroCT DESC
	END
END







