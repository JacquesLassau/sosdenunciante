USE [DbSosPernambucanas] 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_BuscarEmailToken]		
	@StrToken varchar(200)
AS
BEGIN	
	IF NOT EXISTS (Select EMAIL From DbSosPernambucanas..Token With(nolock) Where STrTOKEN = @StrToken)
	BEGIN			
		Insert Into LogSistema Values ('Não existe usuário para este token. Não será possível alterar a senha.', GETUTCDATE())
		Select Top 1 DESCRICAO From LogSistema Order By IdLOG Desc;		
	END	
	ELSE
	BEGIN
		Select EMAIL From DbSosPernambucanas..Token With(nolock) Where STrTOKEN = @StrToken
	END
END
GO
