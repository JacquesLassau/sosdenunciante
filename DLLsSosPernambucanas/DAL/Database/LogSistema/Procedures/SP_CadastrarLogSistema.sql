USE [DbSosPernambucanas]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_CadastrarLogSistema]
	@Descricao varchar(200)
AS
BEGIN			 		
	Insert Into DbSosPernambucanas..LogSistema Values (@Descricao, GETUTCDATE());
	Select Top 1 DESCRICAO From LogSistema Order By IdLOG Desc;
END
GO
