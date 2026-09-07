
--------CREAR TABLA ReportesApp_Operacion_ConductoresBloqueados_Motivo (IdMotivo INT, Descripcion VARCHAR(100))

--------CREAR TABLA ReportesApp_Operacion_ConductoresBloqueados_EstadoDesbloq (IdDesbloqueo INT, Descripcion VARCHAR(100))

--------AÑADIR IdMotivo y IdDesbloqueo A TABLAS ReportesApp_Operacion_ConductoresBloqueados Y ReportesApp_Operacion_ConductoresBloqueados_Historial

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<GERARDO REYES>
-- Create date: <04-05-2023>
-- Description:	<Listar combo de Motivos de Bloqueo>
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Operaciones_ListarMotivoBloqueo]
@User VARCHAR(20)
AS
BEGIN
	SELECT IdMotivo, Descripcion FROM ReportesApp_Operacion_ConductoresBloqueados_Motivo
END

-----------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<GERARDO REYES>
-- Create date: <04-05-2023>
-- Description:	<Listar combo de Estados de Desbloqueo>
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Operaciones_ListarEstadoDesbloqueo]
AS
BEGIN
	SELECT IdDesbloqueo, Descripcion FROM ReportesApp_Operacion_ConductoresBloqueados_EstadoDesbloq
END

-----------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<GERARDO REYES>
-- Create date: <04-05-2023>
-- Description:	<Listar Conductores Bloqueados>
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Conductor_Bloqueados_Listar]
AS
BEGIN
	SELECT BL.IDBLOQUEO, BL.IDPERSONA, PU.Descripcion CARGO, BL.IDConductor, P.NombreCompleto NOMBRE, BM.Descripcion MotivoBloqueo, BL.MotivoBloqueo Descripcion, BL.UsuarioBloquea, 
	CONVERT(CHAR(10), BL.FHBloquea,103) + ' ' + RIGHT(CONVERT(VARCHAR(26), BL.FHBloquea,109),14) FHBloquea, --BL.FHBloquea FHBloquea --
	ED.Descripcion EstadoDesbloq
	FROM ReportesApp_Operacion_ConductoresBloqueados BL WITH(NOLOCK)
		LEFT JOIN PersonaMast P WITH(NOLOCK) ON P.Persona = BL.IDPersona 
        CROSS APPLY (SELECT TOP 1 * FROM EmpleadoMast WHERE empleado = P.Persona and estado='A'
                     ORDER BY FechaInicioContrato DESC,FechaIngreso DESC)e
        LEFT JOIN HR_PuestoEmpresa PU WITH(NOLOCK) ON PU.CodigoPuesto = E.CodigoCargo      
		LEFT JOIN ReportesApp_Operacion_ConductoresBloqueados_Motivo BM WITH(NOLOCK) ON BL.IdMotivo = BM.IdMotivo
		LEFT JOIN ReportesApp_Operacion_ConductoresBloqueados_EstadoDesbloq ED WITH(NOLOCK) ON BL.IdDesbloqueo = ED.IdDesbloqueo
END

--------EDITAR ReportesApp_Operaciones_Conductor_BloquearDesbloquear
@IdMotivo INT,
@IdDesbloqueo INT
AS

IF @Opcion=1 --BLOQUEAR
BEGIN
	--BLOQUEA PARA DESPACHOS:
	-------------------------
	INSERT INTO ReportesApp_Operacion_ConductoresBloqueados(IDPersona, IDConductor, MotivoBloqueo, UsuarioBloquea, FHBloquea, IdMotivo, IdDesbloqueo)
	VALUES(@IDPersona,@IDConductor,@Motivo,@User,GETDATE(),@IdMotivo,1)	
END

IF @Opcion=2 --DESBLOQUEAR
BEGIN
	--DESBLOQUEA PARA DESPACHOS:
	----------------------------
	SELECT @MotivoFueBloqueo=ISNULL(MotivoBloqueo,''), @IDPersona=IDPersona FROM ReportesApp_Operacion_ConductoresBloqueados where IDBloqueo=@IDBloqueo
			
			IF @IdDesbloqueo = 2
			BEGIN
				UPDATE ReportesApp_Operacion_ConductoresBloqueados
				SET IdDesbloqueo = @IdDesbloqueo WHERE IDBloqueo=@IDBloqueo
			END
			ELSE
			BEGIN
				INSERT INTO ReportesApp_Operacion_ConductoresBloqueados_Historial(IDPersona, IDConductor, MotivoBloquea, UsuarioBloquea,FHBloquea, MotivoDesbloqueo, UsuarioDesbloquea, FHDesbloquea, IdMotivo, IdDesbloqueo)
				SELECT IDPersona, IDConductor, MotivoBloqueo, UsuarioBloquea,FHBloquea, @Motivo, @User, GETDATE(), IdMotivo, @IdDesbloqueo FROM ReportesApp_Operacion_ConductoresBloqueados where IDBloqueo=@IDBloqueo
			
				DELETE FROM ReportesApp_Operacion_ConductoresBloqueados WHERE IDBloqueo=@IDBloqueo
			END
END

-----------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES HORNA
-- Create date: 5/5/2023
-- Description:	HISTORIAL DE PERSONAS BLOQUEADAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operaciones_Persona_RptListaHistoricaBloqueosPersona]
@FechaIni DATE = NULL,
@FechaFin Date = null,
@Persona VARCHAR(200) = NULL,
@Filtro INT = 0, -- 0:Todos 1: Bloqueados 2: Desbloqueados
@IdMotivo INT = 0
AS
BEGIN
	IF @Persona IS NULL 
	BEGIN
		SET @Persona  =''
	END
	
	IF @IdMotivo = 0
	BEGIN
	SELECT PERSONA, ID_MOTIVO, MOTIVO_BLOQUEO, DESCRIPCION_BLOQUEO, USUARIO_BLOQUEA, FH_BLOQUEA, ID_DESBLOQUEO, ESTADO_DESBLOQ, MOTIVO_DESBLOQUEO, USUARIO_DESBLOQUEA, FH_DESBLOQUEA--ES_BLOQUEO
	FROM (
			SELECT RTRIM(P.NombreCompleto) PERSONA, H.IdMotivo ID_MOTIVO, BC.Descripcion MOTIVO_BLOQUEO, H.MotivoBloquea AS DESCRIPCION_BLOQUEO, 
				H.UsuarioBloquea USUARIO_BLOQUEA, H.FHBloquea FH_BLOQUEA, H.IdDesbloqueo ID_DESBLOQUEO, CD.Descripcion ESTADO_DESBLOQ, H.MotivoDesbloqueo AS MOTIVO_DESBLOQUEO,
				H.UsuarioDesbloquea AS USUARIO_DESBLOQUEA, H.FHDesbloquea AS FH_DESBLOQUEA, 
				CASE WHEN PB.IDPersona IS NULL THEN 0 ELSE 1 END ES_BLOQUEO
			FROM ReportesApp_Operacion_ConductoresBloqueados_Historial H WITH (NOLOCK)
					INNER JOIN PersonaMast P WITH (NOLOCK) ON Persona = H.IDPersona	
					LEFT JOIN ReportesApp_Operacion_ConductoresBloqueados PB WITH (NOLOCK) ON PB.IDPersona = H.IDPersona --AND PB.FHBloquea = H.FHBloquea
					LEFT JOIN ReportesApp_Operacion_ConductoresBloqueados_Motivo BC WITH (NOLOCK) ON BC.IdMotivo = H.IdMotivo
					LEFT JOIN ReportesApp_Operacion_ConductoresBloqueados_EstadoDesbloq CD WITH (NOLOCK) ON CD.IdDesbloqueo = H.IdDesbloqueo
			WHERE P.Busqueda LIKE @Persona +'%'
		) X 
	WHERE (@Filtro = 0 OR (@Filtro = 1 AND ES_BLOQUEO = 1) OR (@Filtro = 2 AND ES_BLOQUEO = 0))
	--AND (X.ID_MOTIVO = @IdMotivo)
	AND (CONVERT(DATE,X.FH_DESBLOQUEA) BETWEEN CONVERT(DATE,@FechaIni) AND CONVERT(DATE,@FechaFin))
	ORDER BY X.FH_DESBLOQUEA DESC
	END
	ELSE
	BEGIN
	SELECT PERSONA, ID_MOTIVO, MOTIVO_BLOQUEO, DESCRIPCION_BLOQUEO, USUARIO_BLOQUEA, FH_BLOQUEA, ID_DESBLOQUEO, ESTADO_DESBLOQ, MOTIVO_DESBLOQUEO, USUARIO_DESBLOQUEA, FH_DESBLOQUEA--ES_BLOQUEO
	FROM (
			SELECT RTRIM(P.NombreCompleto) PERSONA, H.IdMotivo ID_MOTIVO, BC.Descripcion MOTIVO_BLOQUEO, H.MotivoBloquea AS DESCRIPCION_BLOQUEO, 
				H.UsuarioBloquea USUARIO_BLOQUEA, H.FHBloquea FH_BLOQUEA, H.IdDesbloqueo ID_DESBLOQUEO, CD.Descripcion ESTADO_DESBLOQ, H.MotivoDesbloqueo AS MOTIVO_DESBLOQUEO,
				H.UsuarioDesbloquea AS USUARIO_DESBLOQUEA, H.FHDesbloquea AS FH_DESBLOQUEA, 
				CASE WHEN PB.IDPersona IS NULL THEN 0 ELSE 1 END ES_BLOQUEO
			FROM ReportesApp_Operacion_ConductoresBloqueados_Historial H WITH (NOLOCK)
					INNER JOIN PersonaMast P WITH (NOLOCK) ON Persona = H.IDPersona	
					LEFT JOIN ReportesApp_Operacion_ConductoresBloqueados PB WITH (NOLOCK) ON PB.IDPersona = H.IDPersona --AND PB.FHBloquea = H.FHBloquea
					LEFT JOIN ReportesApp_Operacion_ConductoresBloqueados_Motivo BC WITH (NOLOCK) ON BC.IdMotivo = H.IdMotivo
					LEFT JOIN ReportesApp_Operacion_ConductoresBloqueados_EstadoDesbloq CD WITH (NOLOCK) ON CD.IdDesbloqueo = H.IdDesbloqueo
			WHERE P.Busqueda LIKE @Persona +'%'
		) X 
	WHERE (@Filtro = 0 OR (@Filtro = 1 AND ES_BLOQUEO = 1) OR (@Filtro = 2 AND ES_BLOQUEO = 0))
	AND (X.ID_MOTIVO = @IdMotivo)
	AND (CONVERT(DATE,X.FH_DESBLOQUEA) BETWEEN CONVERT(DATE,@FechaIni) AND CONVERT(DATE,@FechaFin))
	ORDER BY X.FH_DESBLOQUEA DESC
	END
END
