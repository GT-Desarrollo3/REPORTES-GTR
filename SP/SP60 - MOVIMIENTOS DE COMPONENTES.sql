
-- CREAR TABLA ReportesApp_Mantenimiento_MovimientoC_Registro

----------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 29-10-2024
-- Description:	REGISTRAR Y EDITAR MOVIMIENTOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MovimientoC_RegistrarEditarComponentes]
@Opcion INT,
@idMovimientoC INT,
@idSistemaVehiculo INT,
@idSubSistema INT,
@Descripcion VARCHAR(250),
@idUnidadProcedencia INT,
@idUnidadDestino INT, 
@FechaEjecucion DATETIME,
@Motivo VARCHAR(250),
@PersonaAutorizada INT,
@Requerimiento VARCHAR(250),
@Usuario VARCHAR(20)
AS
DECLARE @Contador INT 
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@Opcion IN (1,2) AND @idUnidadProcedencia = @idUnidadDestino) BEGIN
		SET @Exito = '-1 = No puede mover un componente en la misma unidad.'
		ROLLBACK
		GOTO Terminar
	END

	IF (@Opcion = 1) BEGIN		-- CREAR MOVIMIENTO
		SET @Contador = (SELECT MAX(idMovimientoC) FROM ReportesApp_Mantenimiento_MovimientoC_Registro)
		SET @Contador = ISNULL(@Contador,0) + 1
		
		INSERT INTO ReportesApp_Mantenimiento_MovimientoC_Registro(idMovimientoC,idSistemaVehiculo,idSubSistema,Descripcion,idUnidadProcedencia,idUnidadDestino,
																   FechaEjecucion,Motivo,PersonaAutorizada,Requerimiento,UsuarioCrea,FechaCrea,UsuarioModifica,FechaModifica)
		VALUES(@Contador, @idSistemaVehiculo, @idSubSistema, @Descripcion, @idUnidadProcedencia, @idUnidadDestino, @FechaEjecucion, @Motivo, @PersonaAutorizada,
			   @Requerimiento, @Usuario, GETDATE(), @Usuario, GETDATE())
		
		SET @Exito = '0 = El componente fue trasladado correctamente a la unidad ' + (SELECT NumeroPlaca FROM OP_TR_Vehiculo WHERE IdVehiculo = @idUnidadDestino)
	END

	IF (@Opcion = 2) BEGIN		-- EDITAR PROGRAMACION
		UPDATE ReportesApp_Mantenimiento_MovimientoC_Registro
		SET idSistemaVehiculo = @idSistemaVehiculo, idSubSistema = @idSubSistema, Descripcion = @Descripcion, idUnidadProcedencia = @idUnidadProcedencia,
		idUnidadDestino = @idUnidadDestino, FechaEjecucion = @FechaEjecucion, Motivo = @Motivo, PersonaAutorizada = @PersonaAutorizada, Requerimiento = @Requerimiento,
		UsuarioModifica = @Usuario, FechaModifica = GETDATE()
		WHERE idMovimientoC = @idMovimientoC

		SET @Exito = '0 = Movimiento actualizado.'
	END

	IF (@Opcion = 3) BEGIN		-- ELIMINAR MOVIMIENTO
		DELETE FROM ReportesApp_Mantenimiento_MovimientoC_Registro
		WHERE idMovimientoC = @idMovimientoC

		SET @Exito = '0 = Movimiento eliminado correctamente.'
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

--------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 31-10-2024
-- Description:	LISTAR REGISTRO DE MOVIMIENTOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_MovimientoC_ListarComponentes]
@FiltroS INT,
@NumeroPlaca VARCHAR(20),
@FechaInicio VARCHAR(11),
@FechaFin VARCHAR(11),
@idSistema INT,
@idSubSistema INT
AS
DECLARE @FINICIO DATETIME = @FechaInicio
DECLARE @FFIN DATETIME = @FechaFin + ' 23:59'
BEGIN
	IF (@FiltroS = 0) BEGIN
		SELECT MC.idMovimientoC AS 'NRO', MC.FechaEjecucion AS 'FECHA_EJECUCION', MC.idUnidadProcedencia, VP.NumeroPlaca AS 'UNIDAD_PROCEDENCIA', MC.idUnidadDestino,
		VD.NumeroPlaca AS 'UNIDAD_DESTINO', MC.idSistemaVehiculo, SV.Descripcion AS 'SISTEMA', MC.idSubSistema, SSV.Descripcion AS 'SUBSISTEMA',
		MC.Descripcion AS 'COMPONENTE', MC.Motivo AS 'MOTIVO', MC.Requerimiento AS 'NRO_REQUERIMIENTO', LTRIM(RTRIM(R.Comentarios)) AS 'REQUERIMIENTO',
		MC.PersonaAutorizada, LTRIM(RTRIM(P.NombreCompleto)) AS 'AUTORIZADO_POR', MC.UsuarioCrea, MC.FechaCrea, MC.UsuarioModifica, MC.FechaModifica
		FROM ReportesApp_Mantenimiento_MovimientoC_Registro MC
		LEFT JOIN OP_TR_Vehiculo VP WITH(NOLOCK) ON VP.IdVehiculo = MC.idUnidadProcedencia
		LEFT JOIN OP_TR_Vehiculo VD WITH(NOLOCK) ON VD.IdVehiculo = MC.idUnidadDestino
		LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_SistemaVehiculo SV WITH(NOLOCK) ON SV.idSistemaVehiculo = MC.idSistemaVehiculo
		LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_SubSistemaVehiculo SSV WITH(NOLOCK) ON SSV.idSubSistema = MC.idSubSistema
		LEFT JOIN WH_Requisiciones R WITH(NOLOCK) ON LTRIM(RTRIM(R.RequisicionNumero)) = MC.Requerimiento AND R.CompaniaSocio = '10000000' AND R.Estado = 'AP'
		LEFT JOIN PersonaMast P WITH(NOLOCK) ON P.Persona = MC.PersonaAutorizada
		WHERE ((@NumeroPlaca IS NULL OR VP.NumeroPlaca LIKE '%' + @NumeroPlaca + '%') OR (@NumeroPlaca IS NULL OR VD.NumeroPlaca LIKE '%' + @NumeroPlaca + '%'))
		AND (MC.FechaEjecucion BETWEEN @FINICIO AND @FFIN)
		ORDER BY MC.FechaEjecucion DESC
	END
	ELSE BEGIN
		SELECT MC.idMovimientoC AS 'NRO', MC.FechaEjecucion AS 'FECHA_EJECUCION', MC.idUnidadProcedencia, VP.NumeroPlaca AS 'UNIDAD_PROCEDENCIA', MC.idUnidadDestino,
		VD.NumeroPlaca AS 'UNIDAD_DESTINO', MC.idSistemaVehiculo, SV.Descripcion AS 'SISTEMA', MC.idSubSistema, SSV.Descripcion AS 'SUBSISTEMA',
		MC.Descripcion AS 'COMPONENTE', MC.Motivo AS 'MOTIVO', MC.Requerimiento AS 'NRO_REQUERIMIENTO', LTRIM(RTRIM(R.Comentarios)) AS 'REQUERIMIENTO',
		MC.PersonaAutorizada, LTRIM(RTRIM(P.NombreCompleto)) AS 'AUTORIZADO_POR', MC.UsuarioCrea, MC.FechaCrea, MC.UsuarioModifica, MC.FechaModifica
		FROM ReportesApp_Mantenimiento_MovimientoC_Registro MC
		LEFT JOIN OP_TR_Vehiculo VP WITH(NOLOCK) ON VP.IdVehiculo = MC.idUnidadProcedencia
		LEFT JOIN OP_TR_Vehiculo VD WITH(NOLOCK) ON VD.IdVehiculo = MC.idUnidadDestino
		LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_SistemaVehiculo SV WITH(NOLOCK) ON SV.idSistemaVehiculo = MC.idSistemaVehiculo
		LEFT JOIN ReportesApp_Mantenimiento_FallasMecanicas_SubSistemaVehiculo SSV WITH(NOLOCK) ON SSV.idSubSistema = MC.idSubSistema
		LEFT JOIN WH_Requisiciones R WITH(NOLOCK) ON LTRIM(RTRIM(R.RequisicionNumero)) = MC.Requerimiento AND R.CompaniaSocio = '10000000' AND R.Estado = 'AP'
		LEFT JOIN PersonaMast P WITH(NOLOCK) ON P.Persona = MC.PersonaAutorizada
		WHERE ((@NumeroPlaca IS NULL OR VP.NumeroPlaca LIKE '%' + @NumeroPlaca + '%') OR (@NumeroPlaca IS NULL OR VD.NumeroPlaca LIKE '%' + @NumeroPlaca + '%'))
		AND (@idSistema = MC.idSistemaVehiculo) AND (@idSubSistema = MC.idSubSistema) AND (MC.FechaEjecucion BETWEEN @FINICIO AND @FFIN)
		ORDER BY MC.FechaEjecucion DESC
	END
END

--------------------------------------------------------------------------------------

SELECT * FROM ReportesApp_Mantenimiento_MovimientoC_Registro

SELECT * FROM ReportesApp_Mantenimiento_FallasMecanicas_SistemaVehiculo
SELECT * FROM ReportesApp_Mantenimiento_FallasMecanicas_SubSistemaVehiculo