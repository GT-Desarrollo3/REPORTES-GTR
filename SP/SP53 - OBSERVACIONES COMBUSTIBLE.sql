
-- CREAR TABLA ReportesApp_Combustible_Observaciones

-- CREAR TABLA ReportesApp_Combustible_TicketObservacion

-----------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 10-09-2024
-- Description:	LISTAR OBSERVACIONES
-- =============================================
CREATE PROCEDURE [dbo].[ReportesApp_Combustible_Tickets_ListarObservacionCombustible]
AS
BEGIN
    SELECT idObservacion, Descripcion FROM ReportesApp_Combustible_Observaciones
END

-------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 10-09-2024
-- Description:	REGISTRAR OBSERVACIONES
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_Combustible_Tickets_RegistrarObservacion]
@CodViaje VARCHAR(50),
@idObservacion INT,
@Descripcion VARCHAR(250),
@Usuario VARCHAR(20)
AS
DECLARE @Exito VARCHAR(MAX)

SET @Exito = '0 = Observacion Registrada.'

BEGIN TRAN
BEGIN TRY
	INSERT INTO ReportesApp_Combustible_TicketObservacion(CodViaje,idObservacion,Descripcion,UsuarioCrea,FechaCrea)
	VALUES(@CodViaje, @idObservacion, @Descripcion, @Usuario, GETDATE())
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

