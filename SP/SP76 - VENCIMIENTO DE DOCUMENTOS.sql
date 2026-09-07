
-- CREAR TABLA ReportesApp_Operacion_ControlDocumentos_MaestroRelaciones Y LLENARLA

-- CREAR TABLA ReportesApp_Operacion_ControlDocumentos_Vencimiento

----------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 01-04-2025
-- Description:	LISTAR RELACIONES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operacion_ControlDocumentos_ListarRelaciones]
@Opcion INT,
@Operacion INT
AS
BEGIN
	IF (@Opcion = 1) BEGIN  -- LISTAR OPERACIONES
		SELECT IdOperacion, Descripcion FROM ReportesApp_Operacion_Previaje_Operaciones
		WHERE IdOperacion <= 6 AND IdOperacion != 5
	END

	IF (@Opcion = 2) BEGIN  -- LISTAR OPERACIONES - TODOS
		SELECT IdOperacion, Descripcion FROM ReportesApp_Operacion_Previaje_Operaciones
		WHERE IdOperacion <= 6
	END

	IF (@Opcion = 3) BEGIN  -- LISTAR TIPOS DE RELACIONES
		SELECT idMRelacion, TipoRelacion FROM ReportesApp_Operacion_ControlDocumentos_MaestroRelaciones
		WHERE idOperacion = @Operacion
		ORDER BY idMRelacion
	END

	IF (@Opcion = 4) BEGIN  -- LISTAR AREAS RESPONSABLES
		SELECT department, RTRIM([description]) AS 'DESCRIPCION' FROM departmentmst
		WHERE department != '999'
	END

	IF (@Opcion = 5) BEGIN  -- LISTAR TIPO DE DOCUMENTOS
		IF (@Operacion = 1) BEGIN
			SELECT IdTipoDocumento, RTRIM(Descripcion) AS 'DESCRIPCION' FROM ReportesApp_Operacion_TiposDocumentos
			WHERE IdTipoDocumento != 0 AND TipoRelacion = 'CD'
		END
		ELSE BEGIN
			SELECT IdTipoDocumento, RTRIM(Descripcion) AS 'DESCRIPCION' FROM ReportesApp_Operacion_TiposDocumentos
			WHERE IdTipoDocumento != 0 AND TipoRelacion = 'VH'
		END
	END
END

------------------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 01/04/2025
-- Description:	REGISTRAR VENCIMIENTO
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operacion_ControlDocumentos_InsertarEliminarVencimiento]
@Opcion INT,
@idVencimiento INT,
@idMRelacion INT,
@TipoDocumento INT,
@Vencimiento INT,
@Area VARCHAR(10),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)
DECLARE @correlativo INT

SET @correlativo = (SELECT MAX(idVencimiento) FROM ReportesApp_Operacion_ControlDocumentos_Vencimiento)
SET @correlativo = ISNULL(@correlativo,0) + 1

BEGIN TRAN
BEGIN TRY
	IF (@Opcion = 1) BEGIN		-- INSERTAR
		IF (EXISTS(SELECT * FROM ReportesApp_Operacion_ControlDocumentos_Vencimiento WHERE idMRelacion = @idMRelacion AND TipoDocumento = @TipoDocumento))
		BEGIN
			SET @Exito = '-1 = Este documento ya tiene un vencimiento registrado.'
			ROLLBACK
			GOTO Terminar
		END
		ELSE BEGIN
			INSERT INTO ReportesApp_Operacion_ControlDocumentos_Vencimiento(idVencimiento,idMRelacion,TipoDocumento,Vencimiento,Area,UsuarioCreacion,FechaCreacion)
			VALUES(@correlativo, @idMRelacion, @TipoDocumento, @Vencimiento, @Area, @Usuario, GETDATE())

			SET @Exito = '0 = Vencimiento añadido.'
		END
	END

	IF (@Opcion = 2) BEGIN		-- ELIMINAR
		DELETE FROM ReportesApp_Operacion_ControlDocumentos_Vencimiento
		WHERE idVencimiento = @idVencimiento

		SET @Exito = '0 = Vencimiento eliminado.'
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

------------------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 02/04/2025
-- Description:	LISTAR VENCIMIENTOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operacion_ControlDocumentos_ListarVencimientos]
@Operacion VARCHAR(50)
AS
BEGIN
	IF (@Operacion = 'TODO') BEGIN
		SELECT V.idVencimiento AS 'NRO', O.Descripcion AS 'OPERACION', MR.TipoRelacion AS 'TIPO_RELACION', TD.Descripcion AS 'TIPO_DESCRIPCION',
		V.Vencimiento AS 'MESES_VENCIMIENTO', RTRIM(D.[description]) AS 'RESPONSABLES', V.UsuarioCreacion, V.FechaCreacion
		FROM ReportesApp_Operacion_ControlDocumentos_Vencimiento V
		LEFT JOIN ReportesApp_Operacion_ControlDocumentos_MaestroRelaciones MR ON MR.idMRelacion = V.idMRelacion
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = MR.idOperacion
		LEFT JOIN ReportesApp_Operacion_TiposDocumentos TD ON TD.IdTipoDocumento = V.TipoDocumento
		LEFT JOIN departmentmst D ON D.department = V.Area
		ORDER BY V.idVencimiento DESC
	END
	ELSE BEGIN
		SELECT V.idVencimiento AS 'NRO', O.Descripcion AS 'OPERACION', MR.TipoRelacion AS 'TIPO_RELACION', TD.Descripcion AS 'TIPO_DESCRIPCION',
		V.Vencimiento AS 'MESES_VENCIMIENTO', RTRIM(D.[description]) AS 'RESPONSABLES', V.UsuarioCreacion, V.FechaCreacion
		FROM ReportesApp_Operacion_ControlDocumentos_Vencimiento V
		LEFT JOIN ReportesApp_Operacion_ControlDocumentos_MaestroRelaciones MR ON MR.idMRelacion = V.idMRelacion
		LEFT JOIN ReportesApp_Operacion_Previaje_Operaciones O ON O.IdOperacion = MR.idOperacion
		LEFT JOIN ReportesApp_Operacion_TiposDocumentos TD ON TD.IdTipoDocumento = V.TipoDocumento
		LEFT JOIN departmentmst D ON D.department = V.Area
		WHERE O.Descripcion = @Operacion
		ORDER BY V.idVencimiento DESC
	END
END

------------------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 03/04/2025
-- Description:	LISTAR DOCUMENTOS DE UNIDAD
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Operacion_ControlDocumentos_ListarDocumentos]
@Opcion INT,
@idRelacion INT,
@idOperacion INT
AS
BEGIN
	IF (@Opcion = 1) BEGIN		-- DOCUMENTOS DE UNIDADES
		DECLARE @TipoV INT = (SELECT RTRIM(TipoVehiculo) FROM OP_TR_VEHICULO WHERE IdVehiculo = @idRelacion)
		DECLARE @SubTipoV INT = (SELECT RTRIM(SubTipoVehiculo) FROM OP_TR_VEHICULO WHERE IdVehiculo = @idRelacion)
		DECLARE @SubTipo VARCHAR(100) = (SELECT Descripcion FROM OP_TR_SubTipoVehiculo WHERE SubTipoVehiculo = @SubTipoV AND TipoVehiculo = @TipoV)
		
		DECLARE @idMRelacion INT = (SELECT idMRelacion FROM ReportesApp_Operacion_ControlDocumentos_MaestroRelaciones WHERE idOperacion = @idOperacion AND TipoRelacion = @SubTipo)

		SELECT * FROM
		(SELECT TD.Descripcion AS 'DOCUMENTO', ISNULL((SELECT Codigo FROM ReportesApp_Operacion_ControlDocumentos WHERE Relacion = 'VH'
		AND IdRelacion = @idRelacion AND TipoDocumento = v.TipoDocumento),'NO REGISTRADO') AS 'CODIGO', (CASE WHEN V.Vencimiento = 0 THEN 'NO' ELSE 'SÍ' END) AS 'VENCIMIENTO',
		(SELECT CASE WHEN EsAfectoValidacion = 1 THEN 'SÍ' ELSE 'NO' END FROM ReportesApp_Operacion_ControlDocumentos WHERE Relacion = 'VH' AND IdRelacion = @idRelacion
					   AND TipoDocumento = V.TipoDocumento) AS 'VALIDACION_PROG',
		(SELECT ISNULL(CONVERT(VARCHAR,FECHAFINVALIDEZ,103),'')
		FROM ReportesApp_Operacion_ControlDocumentos WHERE Relacion = 'VH' AND IdRelacion = @idRelacion AND TipoDocumento = v.TipoDocumento) AS 'FECHA_VENCIMIENTO',
		ISNULL((SELECT CASE WHEN FECHAFINVALIDEZ IS NULL OR DATEDIFF (D, GETDATE(), FECHAFINVALIDEZ) > TD.DiasAlerta THEN 'VIGENTE' 
					   WHEN FECHAFINVALIDEZ IS NOT NULL AND DATEDIFF (D, GETDATE(), FECHAFINVALIDEZ ) <= TD.DiasAlerta AND
					   DATEDIFF (D, GETDATE(), FECHAFINVALIDEZ ) > 0 THEN 'POR VENCER' ELSE 'NO VIGENTE' END
					   FROM ReportesApp_Operacion_ControlDocumentos WHERE Relacion = 'VH' AND IdRelacion = @idRelacion
					   AND TipoDocumento = V.TipoDocumento),' ') AS 'ESTADO'
		FROM ReportesApp_Operacion_ControlDocumentos_Vencimiento V
		LEFT JOIN ReportesApp_Operacion_TiposDocumentos TD ON TD.IdTipoDocumento = V.TipoDocumento
		WHERE V.idMRelacion = @idMRelacion) X
	END

	IF (@Opcion = 2) BEGIN
		DECLARE @Persona INT = (SELECT IdPersona FROM OP_TR_Conductor WHERE IdConductor = @idRelacion)
		DECLARE @idMRelacion2 INT = (SELECT idMRelacion FROM ReportesApp_Operacion_ControlDocumentos_MaestroRelaciones WHERE idOperacion = @idOperacion AND TipoRelacion = 'CONDUCTOR')

		SELECT * FROM
		(SELECT TD.Descripcion AS 'DOCUMENTO', ISNULL((SELECT Codigo FROM ReportesApp_Operacion_ControlDocumentos WHERE Relacion = 'CD'
		AND IdRelacion = @Persona AND TipoDocumento = v.TipoDocumento),'NO REGISTRADO') AS 'CODIGO', (CASE WHEN V.Vencimiento = 0 THEN 'NO' ELSE 'SÍ' END) AS 'VENCIMIENTO',
		(SELECT CASE WHEN EsAfectoValidacion = 1 THEN 'SÍ' ELSE 'NO' END FROM ReportesApp_Operacion_ControlDocumentos WHERE Relacion = 'CD' AND IdRelacion = @Persona
					   AND TipoDocumento = V.TipoDocumento) AS 'VALIDACION_PROG',
		(SELECT ISNULL(CONVERT(VARCHAR,FECHAFINVALIDEZ,103),'')
		FROM ReportesApp_Operacion_ControlDocumentos WHERE Relacion = 'CD' AND IdRelacion = @Persona AND TipoDocumento = v.TipoDocumento) AS 'FECHA_VENCIMIENTO',
		ISNULL((SELECT CASE WHEN FECHAFINVALIDEZ IS NULL OR DATEDIFF (D, GETDATE(), FECHAFINVALIDEZ) > TD.DiasAlerta THEN 'VIGENTE' 
					   WHEN FECHAFINVALIDEZ IS NOT NULL AND DATEDIFF (D, GETDATE(), FECHAFINVALIDEZ ) <= TD.DiasAlerta AND
					   DATEDIFF (D, GETDATE(), FECHAFINVALIDEZ ) > 0 THEN 'POR VENCER' ELSE 'NO VIGENTE' END
					   FROM ReportesApp_Operacion_ControlDocumentos WHERE Relacion = 'CD' AND IdRelacion = @Persona
					   AND TipoDocumento = V.TipoDocumento),' ') AS 'ESTADO'
		FROM ReportesApp_Operacion_ControlDocumentos_Vencimiento V
		LEFT JOIN ReportesApp_Operacion_TiposDocumentos TD ON TD.IdTipoDocumento = V.TipoDocumento
		WHERE V.idMRelacion = @idMRelacion2) X
	END
END

-----------------------------------------------------------------------------------------------------


DECLARE @OP_PERSONAL TABLE (Numero INT, Persona INT, Operacion INT, TipoDocumento INT, Vencimiento INT)
DECLARE @Contador INT = 1
DECLARE @Persona INT, @TipoDocumento INT, @Vencimiento INT

INSERT INTO @OP_PERSONAL(Numero, Persona, Operacion, TipoDocumento, Vencimiento)
SELECT ROW_NUMBER() OVER(ORDER BY X.PERSONA ASC) AS 'NRO',  X.PERSONA, X.OPERACION, V.TipoDocumento, V.Vencimiento
FROM (SELECT C.IdPersona AS 'PERSONA', C.Nombre AS 'CONDUCTOR', C.CodigoEnapu AS 'OPERACION',
(SELECT COUNT(*) FROM ReportesApp_Operacion_ControlDocumentos WHERE Relacion = 'CD' AND IdRelacion = C.IdPersona) AS 'CONTADOR'
FROM OP_TR_Conductor C
LEFT JOIN EmpleadoMast E ON E.Empleado = C.IdPersona
LEFT JOIN hr_puestoempresa puesto on puesto.CodigoPuesto = e.CodigoCargo
WHERE E.Estado = 'A' AND C.Estado = 'A' AND LTRIM(RTRIM(puesto.Descripcion))='CONDUCTOR') X
LEFT JOIN ReportesApp_Operacion_ControlDocumentos_Vencimiento V ON V.idMRelacion = 1
WHERE X.CONTADOR = 0 AND X.OPERACION = 2

WHILE (@Contador <= (SELECT COUNT(Numero) FROM @OP_PERSONAL)) BEGIN
	SET @Persona = (SELECT Persona FROM @OP_PERSONAL WHERE Numero = @Contador)
	SET @TipoDocumento = (SELECT TipoDocumento FROM @OP_PERSONAL WHERE Numero = @Contador)
	SET @Vencimiento = (SELECT Vencimiento FROM @OP_PERSONAL WHERE Numero = @Contador)

	DECLARE @NroDocumento INT = (SELECT MAX(IdDocumento) FROM ReportesApp_Operacion_ControlDocumentos)
	SET @NroDocumento = ISNULL(@NroDocumento, 0) + 1
	DECLARE @TipoCambio DECIMAL(10,2) = (SELECT FactorVenta FROM TipoCambioMast WITH(NOLOCK) WHERE CONVERT(DATE,FechaCambio) = CONVERT(DATE,GETDATE()))	
	SET @TipoCambio = ISNULL(@TipoCambio, 0.00)

	IF (@TipoDocumento = 1) BEGIN		-- LICENCIA DE CONDUCIR
		DECLARE @Brevete VARCHAR(100) = (SELECT Brevete FROM OP_TR_Conductor WHERE IdPersona = @Persona)

		INSERT INTO ReportesApp_Operacion_ControlDocumentos(IdDocumento, Compania, Sucursal, Relacion, IdRelacion, TipoDocumento, Codigo,
															CentroCosto, SituacionRenovacion, FechaEmision,	FechaInicioValidez,	FechaFinValidez, EsVencimiento,
															Moneda,	TipoCambio, MontoLN, MontoDolares, Anulado, EsAfectoValidacion, UsuarioCrea, FechaCrea)
		VALUES(@NroDocumento, '100000', 'BTRU', 'CD', @Persona, @TipoDocumento, @Brevete, '010201', 'VI', CONVERT(DATETIME,'01/01/2000'),
		CONVERT(DATETIME,'01/01/2000'), CONVERT(DATETIME,'01/01/1900'),1,'LO', @TipoCambio, 0.00, 0.00, 0, 1, 'CBELTRAN', GETDATE())
	END

	IF (@TipoDocumento = 2) BEGIN		-- DNI
		DECLARE @DNI VARCHAR(100) = (SELECT Documento FROM OP_TR_Conductor WHERE IdPersona = @Persona)

		INSERT INTO ReportesApp_Operacion_ControlDocumentos(IdDocumento, Compania, Sucursal, Relacion, IdRelacion, TipoDocumento, Codigo,
															CentroCosto, SituacionRenovacion, FechaEmision,	FechaInicioValidez,	FechaFinValidez, EsVencimiento,
															Moneda,	TipoCambio, MontoLN, MontoDolares, Anulado, EsAfectoValidacion, UsuarioCrea, FechaCrea)
		VALUES(@NroDocumento, '100000', 'BTRU', 'CD', @Persona, @TipoDocumento, @DNI, '010201', 'VI', CONVERT(DATETIME,'01/01/2000'),
		CONVERT(DATETIME,'01/01/2000'), CONVERT(DATETIME,'01/01/1900'),1,'LO', @TipoCambio, 0.00, 0.00, 0, 1, 'CBELTRAN', GETDATE())
	END

	IF (@TipoDocumento NOT IN (1,2)) BEGIN
		INSERT INTO ReportesApp_Operacion_ControlDocumentos(IdDocumento, Compania, Sucursal, Relacion, IdRelacion, TipoDocumento, Codigo,
															CentroCosto, SituacionRenovacion, FechaEmision,	FechaInicioValidez,	FechaFinValidez, EsVencimiento,
															Moneda,	TipoCambio, MontoLN, MontoDolares, Anulado, EsAfectoValidacion, UsuarioCrea, FechaCrea)
		VALUES(@NroDocumento, '100000', 'BTRU', 'CD', @Persona, @TipoDocumento, NULL, '010201', 'VI', CONVERT(DATETIME,'01/01/2000'),
		CONVERT(DATETIME,'01/01/2000'), CONVERT(DATETIME,'01/01/1900'),1,'LO', @TipoCambio, 0.00, 0.00, 0, 1, 'CBELTRAN', GETDATE())
	
		IF (@TipoDocumento = 22) BEGIN
			DECLARE @idCategoria VARCHAR(120) = 'APTO'
			DECLARE @FIV DATETIME = (SELECT CONVERT(DATETIME,'01/01/2000'))
			DECLARE @FFV DATETIME = (SELECT CONVERT(DATETIME,'01/01/1900'))

			EXEC ReportesApp_Seguridad_RegistroEMO_RegistrarEMO @Opcion = 1, @idPersona = @Persona, @FechaInicioValidez = @FIV, @FechaFinValidez = @FFV,
			@Categoria = @idCategoria, @RutaLocal = ' ', @Usuario = 'CBELTRAN'
		END
	END

	SET @Contador = @Contador + 1
END

DELETE FROM @OP_PERSONAL
SET @Contador = 1

INSERT INTO @OP_PERSONAL(Numero, Persona, Operacion, TipoDocumento, Vencimiento)
SELECT ROW_NUMBER() OVER(ORDER BY X.PERSONA ASC) AS 'NRO', X.PERSONA, X.OPERACION, V.TipoDocumento, V.Vencimiento
FROM (SELECT C.IdPersona AS 'PERSONA', C.Nombre AS 'CONDUCTOR', C.CodigoEnapu AS 'OPERACION',
(SELECT COUNT(*) FROM ReportesApp_Operacion_ControlDocumentos WHERE Relacion = 'CD' AND IdRelacion = C.IdPersona) AS 'CONTADOR'
FROM OP_TR_Conductor C
LEFT JOIN EmpleadoMast E ON E.Empleado = C.IdPersona
LEFT JOIN hr_puestoempresa puesto on puesto.CodigoPuesto = e.CodigoCargo
WHERE E.Estado = 'A' AND C.Estado = 'A' AND LTRIM(RTRIM(puesto.Descripcion))='CONDUCTOR') X
LEFT JOIN ReportesApp_Operacion_ControlDocumentos_Vencimiento V ON V.idMRelacion = 4
WHERE X.CONTADOR = 0 AND X.OPERACION = 3

WHILE (@Contador <= (SELECT COUNT(Numero) FROM @OP_PERSONAL)) BEGIN
	SET @Persona = (SELECT Persona FROM @OP_PERSONAL WHERE Numero = @Contador)
	SET @TipoDocumento = (SELECT TipoDocumento FROM @OP_PERSONAL WHERE Numero = @Contador)
	SET @Vencimiento = (SELECT Vencimiento FROM @OP_PERSONAL WHERE Numero = @Contador)

	DECLARE @NroDocumento2 INT = (SELECT MAX(IdDocumento) FROM ReportesApp_Operacion_ControlDocumentos)
	SET @NroDocumento2 = ISNULL(@NroDocumento2, 0) + 1
	DECLARE @TipoCambio2 DECIMAL(10,2) = (SELECT FactorVenta FROM TipoCambioMast WITH(NOLOCK) WHERE CONVERT(DATE,FechaCambio) = CONVERT(DATE,GETDATE()))	
	SET @TipoCambio2 = ISNULL(@TipoCambio2, 0.00)

	IF (@TipoDocumento = 1) BEGIN		-- LICENCIA DE CONDUCIR
		DECLARE @Brevete2 VARCHAR(100) = (SELECT Brevete FROM OP_TR_Conductor WHERE IdPersona = @Persona)

		INSERT INTO ReportesApp_Operacion_ControlDocumentos(IdDocumento, Compania, Sucursal, Relacion, IdRelacion, TipoDocumento, Codigo,
															CentroCosto, SituacionRenovacion, FechaEmision,	FechaInicioValidez,	FechaFinValidez, EsVencimiento,
															Moneda,	TipoCambio, MontoLN, MontoDolares, Anulado, EsAfectoValidacion, UsuarioCrea, FechaCrea)
		VALUES(@NroDocumento2, '100000', 'BTRU', 'CD', @Persona, @TipoDocumento, @Brevete2, '010201', 'VI', CONVERT(DATETIME,'01/01/2000'),
		CONVERT(DATETIME,'01/01/2000'),CONVERT(DATETIME,'01/01/1900'),1,'LO', @TipoCambio2, 0.00, 0.00, 0, 1, 'CBELTRAN', GETDATE())
	END

	IF (@TipoDocumento = 2) BEGIN		-- DNI
		DECLARE @DNI2 VARCHAR(100) = (SELECT Documento FROM OP_TR_Conductor WHERE IdPersona = @Persona)

		INSERT INTO ReportesApp_Operacion_ControlDocumentos(IdDocumento, Compania, Sucursal, Relacion, IdRelacion, TipoDocumento, Codigo,
															CentroCosto, SituacionRenovacion, FechaEmision,	FechaInicioValidez,	FechaFinValidez, EsVencimiento,
															Moneda,	TipoCambio, MontoLN, MontoDolares, Anulado, EsAfectoValidacion, UsuarioCrea, FechaCrea)
		VALUES(@NroDocumento2, '100000', 'BTRU', 'CD', @Persona, @TipoDocumento, @DNI2, '010201', 'VI', CONVERT(DATETIME,'01/01/2000'),
		CONVERT(DATETIME,'01/01/2000'),CONVERT(DATETIME,'01/01/1900'),1,'LO', @TipoCambio2, 0.00, 0.00, 0, 1, 'CBELTRAN', GETDATE())
	END

	IF (@TipoDocumento NOT IN (1,2)) BEGIN
		INSERT INTO ReportesApp_Operacion_ControlDocumentos(IdDocumento, Compania, Sucursal, Relacion, IdRelacion, TipoDocumento, Codigo,
															CentroCosto, SituacionRenovacion, FechaEmision,	FechaInicioValidez,	FechaFinValidez, EsVencimiento,
															Moneda,	TipoCambio, MontoLN, MontoDolares, Anulado, EsAfectoValidacion, UsuarioCrea, FechaCrea)
		VALUES(@NroDocumento2, '100000', 'BTRU', 'CD', @Persona, @TipoDocumento, NULL, '010201', 'VI', CONVERT(DATETIME,'01/01/2000'),
		CONVERT(DATETIME,'01/01/2000'), CONVERT(DATETIME,'01/01/1900'),1,'LO', @TipoCambio2, 0.00, 0.00, 0, 1, 'CBELTRAN', GETDATE())
	
		IF (@TipoDocumento = 22) BEGIN
			DECLARE @idCategoria2 VARCHAR(120) = 'APTO'
			DECLARE @FIV2 DATETIME = (SELECT CONVERT(DATETIME,'01/01/2000'))
			DECLARE @FFV2 DATETIME = (SELECT CONVERT(DATETIME,'01/01/1900'))

			EXEC ReportesApp_Seguridad_RegistroEMO_RegistrarEMO @Opcion = 1, @idPersona = @Persona, @FechaInicioValidez = @FIV2, @FechaFinValidez = @FFV2,
			@Categoria = @idCategoria2, @RutaLocal = ' ', @Usuario = 'CBELTRAN'
		END
	END

	SET @Contador = @Contador + 1
END

DELETE FROM @OP_PERSONAL
SET @Contador = 1

INSERT INTO @OP_PERSONAL(Numero, Persona, Operacion, TipoDocumento, Vencimiento)
SELECT ROW_NUMBER() OVER(ORDER BY X.PERSONA ASC) AS 'NRO', X.PERSONA, X.OPERACION, V.TipoDocumento, V.Vencimiento
FROM (SELECT C.IdPersona AS 'PERSONA', C.Nombre AS 'CONDUCTOR', C.CodigoEnapu AS 'OPERACION',
(SELECT COUNT(*) FROM ReportesApp_Operacion_ControlDocumentos WHERE Relacion = 'CD' AND IdRelacion = C.IdPersona) AS 'CONTADOR'
FROM OP_TR_Conductor C
LEFT JOIN EmpleadoMast E ON E.Empleado = C.IdPersona
LEFT JOIN hr_puestoempresa puesto on puesto.CodigoPuesto = e.CodigoCargo
WHERE E.Estado = 'A' AND C.Estado = 'A' AND LTRIM(RTRIM(puesto.Descripcion))='CONDUCTOR') X
LEFT JOIN ReportesApp_Operacion_ControlDocumentos_Vencimiento V ON V.idMRelacion = 7
WHERE X.CONTADOR = 0 AND X.OPERACION = 1

WHILE (@Contador <= (SELECT COUNT(Numero) FROM @OP_PERSONAL)) BEGIN
	SET @Persona = (SELECT Persona FROM @OP_PERSONAL WHERE Numero = @Contador)
	SET @TipoDocumento = (SELECT TipoDocumento FROM @OP_PERSONAL WHERE Numero = @Contador)
	SET @Vencimiento = (SELECT Vencimiento FROM @OP_PERSONAL WHERE Numero = @Contador)

	DECLARE @NroDocumento3 INT = (SELECT MAX(IdDocumento) FROM ReportesApp_Operacion_ControlDocumentos)
	SET @NroDocumento3 = ISNULL(@NroDocumento3, 0) + 1
	DECLARE @TipoCambio3 DECIMAL(10,2) = (SELECT FactorVenta FROM TipoCambioMast WITH(NOLOCK) WHERE CONVERT(DATE,FechaCambio) = CONVERT(DATE,GETDATE()))	
	SET @TipoCambio3 = ISNULL(@TipoCambio3, 0.00)

	IF (@TipoDocumento = 1) BEGIN		-- LICENCIA DE CONDUCIR
		DECLARE @Brevete3 VARCHAR(100) = (SELECT Brevete FROM OP_TR_Conductor WHERE IdPersona = @Persona)

		INSERT INTO ReportesApp_Operacion_ControlDocumentos(IdDocumento, Compania, Sucursal, Relacion, IdRelacion, TipoDocumento, Codigo,
															CentroCosto, SituacionRenovacion, FechaEmision,	FechaInicioValidez,	FechaFinValidez, EsVencimiento,
															Moneda,	TipoCambio, MontoLN, MontoDolares, Anulado, EsAfectoValidacion, UsuarioCrea, FechaCrea)
		VALUES(@NroDocumento3, '100000', 'BTRU', 'CD', @Persona, @TipoDocumento, @Brevete3, '010201', 'VI', CONVERT(DATETIME,'01/01/2000'),
		CONVERT(DATETIME,'01/01/2000'),CONVERT(DATETIME,'01/01/1900'),1,'LO', @TipoCambio3, 0.00, 0.00, 0, 1, 'CBELTRAN', GETDATE())
	END

	IF (@TipoDocumento = 2) BEGIN		-- DNI
		DECLARE @DNI3 VARCHAR(100) = (SELECT Documento FROM OP_TR_Conductor WHERE IdPersona = @Persona)

		INSERT INTO ReportesApp_Operacion_ControlDocumentos(IdDocumento, Compania, Sucursal, Relacion, IdRelacion, TipoDocumento, Codigo,
															CentroCosto, SituacionRenovacion, FechaEmision,	FechaInicioValidez,	FechaFinValidez, EsVencimiento,
															Moneda,	TipoCambio, MontoLN, MontoDolares, Anulado, EsAfectoValidacion, UsuarioCrea, FechaCrea)
		VALUES(@NroDocumento3, '100000', 'BTRU', 'CD', @Persona, @TipoDocumento, @DNI3, '010201', 'VI', CONVERT(DATETIME,'01/01/2000'),
		CONVERT(DATETIME,'01/01/2000'),CONVERT(DATETIME,'01/01/1900'),1,'LO', @TipoCambio3, 0.00, 0.00, 0, 1, 'CBELTRAN', GETDATE())
	END

	IF (@TipoDocumento NOT IN (1,2)) BEGIN
		INSERT INTO ReportesApp_Operacion_ControlDocumentos(IdDocumento, Compania, Sucursal, Relacion, IdRelacion, TipoDocumento, Codigo,
															CentroCosto, SituacionRenovacion, FechaEmision,	FechaInicioValidez,	FechaFinValidez, EsVencimiento,
															Moneda,	TipoCambio, MontoLN, MontoDolares, Anulado, EsAfectoValidacion, UsuarioCrea, FechaCrea)
		VALUES(@NroDocumento3, '100000', 'BTRU', 'CD', @Persona, @TipoDocumento, NULL, '010201', 'VI', CONVERT(DATETIME,'01/01/2000'),
		CONVERT(DATETIME,'01/01/2000'), CONVERT(DATETIME,'01/01/1900'),1,'LO', @TipoCambio3, 0.00, 0.00, 0, 1, 'CBELTRAN', GETDATE())
	
		IF (@TipoDocumento = 22) BEGIN
			DECLARE @idCategoria3 VARCHAR(120) = 'APTO'
			DECLARE @FIV3 DATETIME = (SELECT CONVERT(DATETIME,'01/01/2000'))
			DECLARE @FFV3 DATETIME = (SELECT CONVERT(DATETIME,'01/01/1900'))

			EXEC ReportesApp_Seguridad_RegistroEMO_RegistrarEMO @Opcion = 1, @idPersona = @Persona, @FechaInicioValidez = @FIV3, @FechaFinValidez = @FFV3,
			@Categoria = @idCategoria3, @RutaLocal = ' ', @Usuario = 'CBELTRAN'
		END
	END

	SET @Contador = @Contador + 1
END




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 08/04/2025
-- Description:	INSERTAR ÁNALISIS DE FALLAS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Mantenimiento_FallasMecanicas_InsertarAnalisisFallas]
@idFalla INT,
@Contador INT,
@Respuesta VARCHAR(350),
@Usuario VARCHAR(30)
AS
DECLARE @Exito VARCHAR(MAX)

BEGIN TRAN
BEGIN TRY
	IF (@Contador = 1) BEGIN
		UPDATE ReportesApp_Mantenimiento_FallasMecanicas_AnalisisFallas
		SET Respuesta1 = @Respuesta, UsuarioModifica = @Usuario, FechaModifica = GETDATE()
		WHERE idFalla = @idFalla

		SET @Exito = '0 = Respuesta añadida.'
	END

	IF (@Contador = 2) BEGIN
		UPDATE ReportesApp_Mantenimiento_FallasMecanicas_AnalisisFallas
		SET Respuesta2 = @Respuesta, UsuarioModifica = @Usuario, FechaModifica = GETDATE()
		WHERE idFalla = @idFalla

		SET @Exito = '0 = Respuesta añadida.'
	END

	IF (@Contador = 3) BEGIN
		UPDATE ReportesApp_Mantenimiento_FallasMecanicas_AnalisisFallas
		SET Respuesta3 = @Respuesta, UsuarioModifica = @Usuario, FechaModifica = GETDATE()
		WHERE idFalla = @idFalla

		SET @Exito = '0 = Respuesta añadida.'
	END

	IF (@Contador = 4) BEGIN
		UPDATE ReportesApp_Mantenimiento_FallasMecanicas_AnalisisFallas
		SET Respuesta4 = @Respuesta, UsuarioModifica = @Usuario, FechaModifica = GETDATE()
		WHERE idFalla = @idFalla

		SET @Exito = '0 = Respuesta añadida.'
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