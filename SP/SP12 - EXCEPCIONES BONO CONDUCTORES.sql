
-- Crear tabla ReportesApp_RRHH_BonoCondutores_Motivos y llenarla

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GERARDO REYES
-- Create date: 01-08-2023
-- Description:	LISTAR MOTIVO DE BONOS
-- =============================================
ALTER PROCEDURE [dbo].[ReportesApp_RRHH_BonoConductores_ListarMotivos]
@TipoBono VARCHAR(50)
AS
BEGIN
	IF (@TipoBono = 'BONO COMBUSTIBLE') BEGIN
		SELECT * FROM ReportesApp_RRHH_BonoCondutores_Motivos WHERE idMotivoBono BETWEEN 1 AND 4
	END
	
	IF (@TipoBono = 'BONO SEGURIDAD') BEGIN
		SELECT * FROM ReportesApp_RRHH_BonoCondutores_Motivos
	END
END

-- Modificar ReportesApp_RRHH_BonoConductores_RegistrarExcepciones

-- Modificar ReportesApp_RRHH_BonoConductores_ListarExcepciones

-- Modificar ReportesApp_RRHH_BonoConductores_Listar

-- Modificar ReportesApp_RRHH_BonoConductores_ListaPeriodosCerradosDetalle