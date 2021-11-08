USE [DbSosPernambucanas] 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GravarAcessoToken]		
	@StrToken varchar(200)
AS
BEGIN	
	IF NOT EXISTS (Select EMAIL From DbSosPernambucanas..Token With(nolock) Where STrTOKEN = @StrToken)
	BEGIN			
		Insert Into LogSistema Values ('Não existe usuário para este token. Não será possível alterar a senha.', GETUTCDATE())
		Select Top 1 DESCRICAO From LogSistema Order By IdLOG Desc;		
	END
	IF EXISTS (Select DATaACESSO From DbSosPernambucanas..Token With(nolock) Where STrTOKEN = @StrToken And DATaACESSO IS NOT NULL)
	BEGIN			
		Insert Into LogSistema Values ('Token inválido. Usuário já utilizou o link de acesso. Deve solicitar um novo link.', GETUTCDATE())
		Select Top 1 DESCRICAO From LogSistema Order By IdLOG Desc;		
	END
	ELSE
	BEGIN
		Declare @Usuario Int = (Select USUARIO From DbSosPernambucanas..Token With(nolock) Where STrTOKEN = @StrToken)		
		Update DbSosPernambucanas..Token Set DATaACESSO = GETUTCDATE() Where STrTOKEN =  @StrToken And USUARIO = @Usuario
		Insert Into LogSistema Values ('Token "' + @StrToken + '" utilizado com sucesso.', GETUTCDATE())
		Select Top 1 DESCRICAO From LogSistema Order By IdLOG Desc;		
	END
END
GO