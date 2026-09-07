
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ====================================================
-- Author:		GERARDO REYES
-- Create date: 24-09-2025
-- Description:	REGISTRAR ASISTENCIA EXTENDIDA
-- ====================================================
ALTER PROCEDURE [dbo].[ReportesApp_RRHH_Asistencias_AsistenciaExtendida]
@Periodo VARCHAR(6),
@xml_Asistencias XML,
@Usuario VARCHAR(15)
AS
DECLARE @Exito VARCHAR(MAX)

DECLARE @Asistencias Table (Numero INT IDENTITY(1,1), IdPersona INT, Operacion VARCHAR(100), Fecha DATE, IdTipoAsist INT, Letra VARCHAR(3))
DECLARE @Fechas TABLE (PK INT IDENTITY(1,1) PRIMARY KEY, fecha DATE)
DECLARE @IdTipoAsist INT = 32
DECLARE @LetraAsistencia VARCHAR(3) = 'AE'
DECLARE @idact INT
  
IF @xml_Asistencias IS NOT NULL BEGIN  
	EXEC sp_xml_preparedocument @idact out, @xml_Asistencias 
	 
	INSERT INTO @Asistencias(IdPersona,Operacion,Fecha,IdTipoAsist,Letra)  
	SELECT IDPersona, Operacion, Fecha, @IdTipoAsist, @LetraAsistencia
	FROM OPENXML(@idact, '/r/d',3) WITH(IDPersona INT, Operacion VARCHAR(100), Fecha DATE)       
	EXEC sp_xml_removedocument @idact  

	INSERT INTO @Fechas (fecha)
	SELECT DISTINCT Fecha FROM @Asistencias
END

IF @Usuario NOT IN('LLEYTHON','SESCOBEDO','CGUZMAN','SCHAVEZ','GREYES','MARANDAR','AACHAVEZ','LVILLANUEVA','CHIDALGO','EPIZAN','RAZARE','HMINCHOLA',
'JHERRERAI','LLEYTHON','CGUZMAN')
BEGIN
	SET @exito = '-1 = Usted no está autorizado para registrar esta asistencia.'
	GOTO Terminar
END

IF EXISTS(SELECT TOP(1) * FROM @Asistencias WHERE Fecha > CAST(GETDATE() AS DATE)) BEGIN 
	DECLARE @x_FechaSuperior AS DATE
	SELECT TOP 1 @x_FechaSuperior = Fecha FROM @Asistencias where Fecha>CAST(GETDATE() AS DATE)
		
	SET @exito = '-2 = La fecha '+ CONVERT(VARCHAR(10),@x_FechaSuperior,103)+' no es válida. No puede registrar en fechas superiores a la actual.'
	GOTO Terminar
END

IF EXISTS(SELECT TOP(1) * FROM @Asistencias WHERE Operacion != 'VOLCAN') BEGIN 
	SET @exito = '-3 = Solo puede registrar asistencias extendidas a conductores que pertenezcan a la operación Volcan.'
	GOTO Terminar
END

DECLARE @x_Trabajador VARCHAR(100), @x_FechaIngreso DATE, @x_FechaAsistencia DATE

IF EXISTS(SELECT TOP 1(1) FROM @Asistencias A INNER JOIN empleadomast E ON E.Empleado = A.IdPersona WHERE a.Fecha < e.FechaIngreso) BEGIN
	SELECT TOP 1 @x_Trabajador = P.NombreCompleto, @x_FechaIngreso=E.FechaIngreso, @x_FechaAsistencia=A.Fecha 
	FROM @Asistencias A
	INNER JOIN empleadomast E ON E.Empleado=A.IdPersona
	INNER JOIN PersonaMast P ON P.Persona=E.Empleado
	WHERE a.Fecha < e.FechaIngreso
			
	SET @exito = '-4 = Error con Trabajador: '+@x_Trabajador+CHAR(13)
				+'F.Asis: '+CAST(@x_FechaAsistencia AS VARCHAR(11))+' Es Menor que su F.Ingreso:'+CAST(@x_FechaIngreso AS VARCHAR(11))
	GOTO Terminar					
END

IF EXISTS(SELECT TOP 1(1) FROM @Asistencias A INNER JOIN ReportesApp_RRHH_Asistencia ASIS ON ASIS.IDPersona=A.IdPersona AND ASIS.Fecha=A.Fecha
AND ASIS.IDTipoAsist != 32) BEGIN
	SELECT TOP 1 @x_Trabajador=P.NombreCompleto,@x_FechaAsistencia=A.Fecha  FROM @Asistencias A
	INNER JOIN ReportesApp_RRHH_Asistencia ASIS ON ASIS.IDPersona=A.IdPersona AND ASIS.Fecha=A.Fecha and ASIS.IDTipoAsist != 32
	INNER JOIN PersonaMast P ON P.Persona=ASIS.IDPersona
			
	SET @exito='-5=Error con Trabajador: '+@x_Trabajador+CHAR(13)
				+'F.Asis: '+CAST(@x_FechaAsistencia AS VARCHAR(11))+'. Ya tiene Registro. No puede Modificar. Debe Informar al área CONTROL GESTIÓN.'
	GOTO Terminar					
END

-- VERIFICAMOS QUE EN LA FECHA NO HAYA, LICENCIAS DESDE RRHH
DECLARE @Tmp_Personas_Con_Licencias TABLE(IDPersona INT, Nombre VARCHAR(100), TipoLicencia CHAR(2), DescripcionLicencia VARCHAR(50), FInicio datetime,
FFinal datetime, dias int)

INSERT INTO @Tmp_Personas_Con_Licencias(IDPersona,Nombre,TipoLicencia,DescripcionLicencia, FInicio,FFinal,dias)
SELECT HR_LICENCIAS.EMPLEADO, NOMBRECOMPLETO, TIPOLICENCIA, DescripcionLocal, FECHAINICIO, FECHAFINAL, datediff(day,FECHAINICIO,FECHAfinal) + 1 as dias
FROM HR_LICENCIAS, PERSONAMAST, AS_CARNETIDENTIFICACION, MA_MiscelaneosDetalle, EMPLEADOMAST
LEFT JOIN AC_COSTCENTERMST ON EMPLEADOMAST.CENTROCOSTOS = AC_COSTCENTERMST.COSTCENTER
WHERE HR_LICENCIAS.EMPLEADO=PERSONAMAST.PERSONA AND EMPLEADOMAST.EMPLEADO=PERSONAMAST.PERSONA AND EMPLEADOMAST.ESTADO='A'
AND AS_CARNETIDENTIFICACION.Empleado = Empleadomast.empleado AND MA_MiscelaneosDetalle.CodigoElemento=HR_LICENCIAS.TipoLicencia
AND MA_MiscelaneosDetalle.AplicacionCodigo = 'HR' AND  MA_MiscelaneosDetalle.CodigoTabla = 'LICENCIA'  AND  MA_MiscelaneosDetalle.Compania = '999999'     
AND (EmpleadoMast.CompaniaSocio IN ('10000000')) AND (EmpleadoMast.TipoPlanilla IN ('EM','OB','PR')) AND (EmpleadoMast.LocaciondePago = 'S' OR 'S' = 'S') 
AND (YEAR(HR_LICENCIAS.FechaInicio)=RIGHT(@Periodo,4) AND MONTH(HR_LICENCIAS.FechaInicio)=LEFT(@Periodo,2))

IF EXISTS(SELECT TOP 1 (1) FROM @Asistencias A INNER JOIN @Tmp_Personas_Con_Licencias L ON L.IDPersona=A.IdPersona WHERE A.Fecha BETWEEN L.FInicio AND L.FFinal)
BEGIN
	DECLARE @x_FechaModifLic VARCHAR(30), @x_ConductorLic VARCHAR(100),@x_MotivoLic VARCHAR(50), @x_FechasLic VARCHAR(50), @x_diasLic CHAR(2)

	SELECT TOP 1 @x_ConductorLic=L.Nombre
				,@x_FechaModifLic=CAST(A.Fecha AS VARCHAR(11))
		        ,@x_MotivoLic=L.DescripcionLicencia 
		        ,@x_FechasLic=CAST(L.FInicio AS VARCHAR(11))+ ' AL '+CAST(L.FFinal AS VARCHAR(11))
		        ,@x_diasLic=CAST(L.dias as VARCHAR)
		        FROM @Asistencias A 
	INNER JOIN @Tmp_Personas_Con_Licencias L ON L.IDPersona=A.IdPersona
	WHERE A.Fecha BETWEEN L.FInicio AND L.FFinal
		
	SET @exito='-7=No puede Modificar la Asistencia: '+ @x_FechaModifLic +CHAR(13)
				+'Conductor:'+@x_ConductorLic+CHAR(13)+
				+'Motivo:'+@x_MotivoLic+CHAR(13)+
				+'Fecha:'+@x_FechasLic+CHAR(13)+
				+'Cant.:'+@x_diasLic+' Dias.'
	GOTO Terminar
END

-- CARGA DE VACACIONES
DECLARE @Tmp_Vacaciones TABLE(PK int IDENTITY(1,1),IDPersona int, Trabajador VARCHAR(100),FInicio DATETIME, FFinal DATETIME, dias INT,
CompaniaSocio VARCHAR(8),ConceptoAcceso VARCHAR(5), IdTipoAsist int, CodTipo CHAR(1))

INSERT INTO @Tmp_Vacaciones(IDPersona, Trabajador,FInicio , FFinal , dias,CompaniaSocio,ConceptoAcceso, IdTipoAsist, CodTipo)
SELECT Empleado IDPersona,P.NombreCompleto,v.FechaInicio,v.FechaFin,v.DiasUtilizacion,CompaniaSocio,'VACA',41,'V'  
FROM PR_VacacionUtilizacion V
LEFT JOIN PersonaMast P ON P.Persona=V.Empleado 
WHERE  ( YEAR(V.FechaInicio)=RIGHT(@periodo,4) AND MONTH(V.FechaInicio)=LEFT(@periodo,2))
AND TipoUtilizacion='GOC'

IF EXISTS(SELECT TOP 1(1) FROM @Asistencias A INNER JOIN @Tmp_Vacaciones V ON V.IDPersona=A.IdPersona WHERE A.Fecha BETWEEN V.FInicio AND V.FFinal)
BEGIN	
	SELECT TOP 1 @x_ConductorLic=L.Trabajador,@x_FechaModifLic=CAST(A.Fecha AS VARCHAR(11)),@x_MotivoLic=' Vacaciones',
	@x_FechasLic=CAST(L.FInicio AS VARCHAR(11))+ ' AL '+CAST(L.FFinal AS VARCHAR(11)),@x_diasLic=CAST(L.dias as VARCHAR)
	FROM @Asistencias A 
	INNER JOIN @Tmp_Vacaciones L ON L.IDPersona=A.IdPersona
	WHERE A.Fecha BETWEEN L.FInicio AND L.FFinal
		
	SET @exito='-8=Existe Conflicto Persona: ('+CAST(@x_ConductorLic AS VARCHAR)+')'+CHAR(13)+@x_FechaModifLic+' , Vacac. de RRHH.'
	GOTO Terminar
END


IF EXISTS(SELECT TOP 1 ad.FEcha FROM ReportesApp_RRHH_Asistencia_CompensacionesAdelantadas AD INNER JOIN @Asistencias A ON A.IdPersona=AD.IDPERSONA AND A.Fecha=AD.FECHA)
BEGIN
	SET @exito = '-10 = Se ha encontrado Fechas vinculadas a Compensación Adelantadas.'+char(13)+' No pueden ser Modificadas.Las debe Liberar antes con (Control Gestión)'
	GOTO Terminar
END

DECLARE @I INT=1
DECLARE @MAX INT=(SELECT MAX(PK) FROM @Fechas)
DECLARE @x_FechaSelec DATE, @x_DiaSelec INT

BEGIN TRAN
BEGIN TRY
	IF EXISTS(SELECT TOP 1(1) FROM ReportesApp_RRHH_Asistencia_Compensaciones C
	INNER JOIN @Asistencias A ON A.IdPersona=C.IDPersona AND (A.Fecha=C.FechaCompensa OR A.Fecha=C.FechaTrabajada))
	BEGIN
		SET @exito='-11=Se ha encontrado esta fecha vinculada a Compensaciones Día.'+CHAR(13)
		+' No pueden ser Modificadas.Las debe Liberar antes.'
		ROLLBACK
		GOTO Terminar
	END
	
	IF EXISTS(SELECT TOP 1(1) FROM ReportesApp_RRHH_Asistencia_Noches  C
	INNER JOIN @Asistencias A ON A.IdPersona=C.IDPersona AND A.Fecha=C.FechaCompensa)
	BEGIN
		SET @exito='-12=Se ha encontrado Fechas vinculadas a Compensación Noches.'+CHAR(13)
		+' No pueden ser Modificadas.Las debe Liberar antes.'
		ROLLBACK
		GOTO Terminar
	END

	DECLARE @Contador INT = 1

	WHILE (@Contador <= (SELECT COUNT(Numero) FROM @Asistencias)) BEGIN
		DECLARE @IdPersona INT = (SELECT IdPersona FROM @Asistencias WHERE Numero = @Contador)
		DECLARE @Fecha DATE = (SELECT Fecha FROM @Asistencias WHERE Numero = @Contador)

		-- SE ACTUALIZAN LAS ASISTENCIAS EXTENDIDAS
		IF (EXISTS(SELECT * FROM ReportesApp_RRHH_Asistencia WHERE (IDPersona = @IdPersona) AND (Fecha = @Fecha) AND (IDTipoAsist = 32)
		AND (AsisExtendida IS NULL))) BEGIN
			UPDATE ReportesApp_RRHH_Asistencia
			SET AsisExtendida = 1
			WHERE (IDTipoAsist = 32) AND (AsisExtendida IS NULL) AND (IDPersona = @IdPersona) AND (Fecha = @Fecha)

			UPDATE V 
	        SET V.D1 = CASE WHEN DAY(@Fecha) = 1 THEN @LetraAsistencia ELSE V.D1 END,
				V.D2 = CASE WHEN DAY(@Fecha) = 2 THEN @LetraAsistencia ELSE V.D2 END,
				V.D3 = CASE WHEN DAY(@Fecha) = 3 THEN @LetraAsistencia ELSE V.D3 END,
				V.D4 = CASE WHEN DAY(@Fecha) = 4 THEN @LetraAsistencia ELSE V.D4 END,
				V.D5 = CASE WHEN DAY(@Fecha) = 5 THEN @LetraAsistencia ELSE V.D5 END,
				V.D6 = CASE WHEN DAY(@Fecha) = 6 THEN @LetraAsistencia ELSE V.D6 END,
				V.D7 = CASE WHEN DAY(@Fecha) = 7 THEN @LetraAsistencia ELSE V.D7 END,
				V.D8 = CASE WHEN DAY(@Fecha) = 8 THEN @LetraAsistencia ELSE V.D8 END,
				V.D9 = CASE WHEN DAY(@Fecha) = 9 THEN @LetraAsistencia ELSE V.D9 END,
				V.D10 = CASE WHEN DAY(@Fecha) = 10 THEN @LetraAsistencia ELSE V.D10 END,
				V.D11 = CASE WHEN DAY(@Fecha) = 11 THEN @LetraAsistencia ELSE V.D11 END,
				V.D12 = CASE WHEN DAY(@Fecha) = 12 THEN @LetraAsistencia ELSE V.D12 END,
				V.D13 = CASE WHEN DAY(@Fecha) = 13 THEN @LetraAsistencia ELSE V.D13 END,
				V.D14 = CASE WHEN DAY(@Fecha) = 14 THEN @LetraAsistencia ELSE V.D14 END,
				V.D15 = CASE WHEN DAY(@Fecha) = 15 THEN @LetraAsistencia ELSE V.D15 END,
				V.D16 = CASE WHEN DAY(@Fecha) = 16 THEN @LetraAsistencia ELSE V.D16 END,
				V.D17 = CASE WHEN DAY(@Fecha) = 17 THEN @LetraAsistencia ELSE V.D17 END,
				V.D18 = CASE WHEN DAY(@Fecha) = 18 THEN @LetraAsistencia ELSE V.D18 END,
				V.D19 = CASE WHEN DAY(@Fecha) = 19 THEN @LetraAsistencia ELSE V.D19 END,
				V.D20 = CASE WHEN DAY(@Fecha) = 20 THEN @LetraAsistencia ELSE V.D20 END,
				V.D21 = CASE WHEN DAY(@Fecha) = 21 THEN @LetraAsistencia ELSE V.D21 END,
				V.D22 = CASE WHEN DAY(@Fecha) = 22 THEN @LetraAsistencia ELSE V.D22 END,
				V.D23 = CASE WHEN DAY(@Fecha) = 23 THEN @LetraAsistencia ELSE V.D23 END,
				V.D24 = CASE WHEN DAY(@Fecha) = 24 THEN @LetraAsistencia ELSE V.D24 END,
				V.D25 = CASE WHEN DAY(@Fecha) = 25 THEN @LetraAsistencia ELSE V.D25 END,
				V.D26 = CASE WHEN DAY(@Fecha) = 26 THEN @LetraAsistencia ELSE V.D26 END,
				V.D27 = CASE WHEN DAY(@Fecha) = 27 THEN @LetraAsistencia ELSE V.D27 END,
				V.D28 = CASE WHEN DAY(@Fecha) = 28 THEN @LetraAsistencia ELSE V.D28 END,
				V.D29 = CASE WHEN DAY(@Fecha) = 29 THEN @LetraAsistencia ELSE V.D29 END,
				V.D30 = CASE WHEN DAY(@Fecha) = 30 THEN @LetraAsistencia ELSE V.D30 END,
				V.D31 = CASE WHEN DAY(@Fecha) = 31 THEN @LetraAsistencia ELSE V.D31 END
			FROM ReportesApp_RRHH_AsistenciaView V
		    WHERE V.Anio = RIGHT(@Periodo,4) AND V.Mes = LEFT(@periodo,2) AND V.CodPlanilla = 'CD' AND V.IDPersona = @IdPersona
		END
		ELSE BEGIN
			-- SE REVIERTEN LAS ASISTENCIAS EXTENDIDAS YA REGISTRADAS
			UPDATE ReportesApp_RRHH_Asistencia
			SET AsisExtendida = NULL
			WHERE (IDTipoAsist = 32) AND (AsisExtendida = 1) AND (IDPersona = @IdPersona) AND (Fecha = @Fecha)

			UPDATE V 
	        SET V.D1 = CASE WHEN DAY(@Fecha) = 1 THEN 'A' ELSE V.D1 END,
				V.D2 = CASE WHEN DAY(@Fecha) = 2 THEN 'A' ELSE V.D2 END,
				V.D3 = CASE WHEN DAY(@Fecha) = 3 THEN 'A' ELSE V.D3 END,
				V.D4 = CASE WHEN DAY(@Fecha) = 4 THEN 'A' ELSE V.D4 END,
				V.D5 = CASE WHEN DAY(@Fecha) = 5 THEN 'A' ELSE V.D5 END,
				V.D6 = CASE WHEN DAY(@Fecha) = 6 THEN 'A' ELSE V.D6 END,
				V.D7 = CASE WHEN DAY(@Fecha) = 7 THEN 'A' ELSE V.D7 END,
				V.D8 = CASE WHEN DAY(@Fecha) = 8 THEN 'A' ELSE V.D8 END,
				V.D9 = CASE WHEN DAY(@Fecha) = 9 THEN 'A' ELSE V.D9 END,
				V.D10 = CASE WHEN DAY(@Fecha) = 10 THEN 'A' ELSE V.D10 END,
				V.D11 = CASE WHEN DAY(@Fecha) = 11 THEN 'A' ELSE V.D11 END,
				V.D12 = CASE WHEN DAY(@Fecha) = 12 THEN 'A' ELSE V.D12 END,
				V.D13 = CASE WHEN DAY(@Fecha) = 13 THEN 'A' ELSE V.D13 END,
				V.D14 = CASE WHEN DAY(@Fecha) = 14 THEN 'A' ELSE V.D14 END,
				V.D15 = CASE WHEN DAY(@Fecha) = 15 THEN 'A' ELSE V.D15 END,
				V.D16 = CASE WHEN DAY(@Fecha) = 16 THEN 'A' ELSE V.D16 END,
				V.D17 = CASE WHEN DAY(@Fecha) = 17 THEN 'A' ELSE V.D17 END,
				V.D18 = CASE WHEN DAY(@Fecha) = 18 THEN 'A' ELSE V.D18 END,
				V.D19 = CASE WHEN DAY(@Fecha) = 19 THEN 'A' ELSE V.D19 END,
				V.D20 = CASE WHEN DAY(@Fecha) = 20 THEN 'A' ELSE V.D20 END,
				V.D21 = CASE WHEN DAY(@Fecha) = 21 THEN 'A' ELSE V.D21 END,
				V.D22 = CASE WHEN DAY(@Fecha) = 22 THEN 'A' ELSE V.D22 END,
				V.D23 = CASE WHEN DAY(@Fecha) = 23 THEN 'A' ELSE V.D23 END,
				V.D24 = CASE WHEN DAY(@Fecha) = 24 THEN 'A' ELSE V.D24 END,
				V.D25 = CASE WHEN DAY(@Fecha) = 25 THEN 'A' ELSE V.D25 END,
				V.D26 = CASE WHEN DAY(@Fecha) = 26 THEN 'A' ELSE V.D26 END,
				V.D27 = CASE WHEN DAY(@Fecha) = 27 THEN 'A' ELSE V.D27 END,
				V.D28 = CASE WHEN DAY(@Fecha) = 28 THEN 'A' ELSE V.D28 END,
				V.D29 = CASE WHEN DAY(@Fecha) = 29 THEN 'A' ELSE V.D29 END,
				V.D30 = CASE WHEN DAY(@Fecha) = 30 THEN 'A' ELSE V.D30 END,
				V.D31 = CASE WHEN DAY(@Fecha) = 31 THEN 'A' ELSE V.D31 END
			FROM ReportesApp_RRHH_AsistenciaView V
		    WHERE V.Anio = RIGHT(@Periodo,4) AND V.Mes = LEFT(@periodo,2) AND V.CodPlanilla = 'CD' AND V.IDPersona = @IdPersona
		END

		SET @Contador = @Contador + 1
	END

	SET @Exito = '0 = Asistencia actualizada correctamente.'
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
-- Create date: 19-08-2025
-- Description:	CONTAR COMPENSACIONES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_RRHH_Asistencias_ContarCompensaciones]
@idPersona INT
AS
BEGIN
	DECLARE @CD_VOLCAN TABLE (Numero INT, Persona INT, CompPendientes INT)
	DECLARE @TotalAsistencia INT, @CantidadComp INT, @TotalCompensaciones INT, @CompPendientes INT 
	DECLARE @Contador INT = 1

	INSERT INTO @CD_VOLCAN (Numero, Persona)
	SELECT ROW_NUMBER() OVER(ORDER BY IdPersona ASC) AS 'NRO', IdPersona
	FROM OP_TR_Conductor
	WHERE CodigoEnapu = 10 AND Estado = 'A'

	WHILE (@Contador <= (SELECT COUNT(Numero) FROM @CD_VOLCAN)) BEGIN
		DECLARE @Persona INT = (SELECT Persona FROM @CD_VOLCAN WHERE Numero = @Contador)
			
		IF (@Persona IN (24975,24874)) BEGIN
			SET @TotalAsistencia = (SELECT COUNT(*) FROM ReportesApp_RRHH_Asistencia WHERE IDTipoAsist = 32 AND IDPersona = @Persona
									AND AsisExtendida IS NULL AND Fecha >= '19/05/2025' AND Fecha <= CONVERT(DATE,GETDATE()))
			SET @CantidadComp = (SELECT COUNT(*) FROM ReportesApp_RRHH_Asistencia WHERE IDTipoAsist IN (55,57) AND IDPersona = @Persona 
								 AND Fecha >= '19/05/2025')
		END
		ELSE BEGIN
			IF (@Persona = 25168) BEGIN
				SET @TotalAsistencia = (SELECT COUNT(*) FROM ReportesApp_RRHH_Asistencia WHERE IDTipoAsist = 32 AND IDPersona = @Persona
										AND AsisExtendida IS NULL AND Fecha >= '26/06/2025' AND Fecha <= CONVERT(DATE,GETDATE()))
				SET @CantidadComp = (SELECT COUNT(*) FROM ReportesApp_RRHH_Asistencia WHERE IDTipoAsist IN (55,57) AND IDPersona = @Persona 
									 AND Fecha >= '26/06/2025')
			END
			ELSE BEGIN
				SET @TotalAsistencia = (SELECT COUNT(*) FROM ReportesApp_RRHH_Asistencia WHERE IDTipoAsist = 32 AND IDPersona = @Persona
										AND AsisExtendida IS NULL AND Fecha <= CONVERT(DATE,GETDATE()))
				SET @CantidadComp = (SELECT COUNT(*) FROM ReportesApp_RRHH_Asistencia WHERE IDTipoAsist IN (55,57) AND IDPersona = @Persona)
			END
		END

		SET @TotalCompensaciones = @TotalAsistencia / 3
		SET @CompPendientes = @TotalCompensaciones - @CantidadComp

		IF (EXISTS(SELECT * FROM ReportesApp_RRHH_Asistencias_CompensacionesVolcan WHERE idPersona = @Persona)) BEGIN
			UPDATE ReportesApp_RRHH_Asistencias_CompensacionesVolcan
			SET CompPendientes = @CompPendientes
			WHERE idPersona = @Persona
		END
		ELSE BEGIN
			INSERT INTO ReportesApp_RRHH_Asistencias_CompensacionesVolcan
			SELECT @Persona, @CompPendientes
		END

		SET @Contador = @Contador + 1
	END
	
	SELECT ISNULL(CompPendientes,0) AS 'COMP_PENDIENTES'
	FROM ReportesApp_RRHH_Asistencias_CompensacionesVolcan
	WHERE idPersona = @idPersona
END

-------------------------------------------------------------------------------