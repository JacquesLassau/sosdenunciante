USE [DbSosPernambucanas] 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_CadastrarToken]		
	@Email varchar(200),
	@UrlBase varchar(200),
	@StrToken varchar(200)
AS
BEGIN	
	IF NOT EXISTS (Select EMAIL From DbSosPernambucanas..Usuario With(nolock) Where EMAIL = @Email And SITUACAO <> 2)
	BEGIN			
		Insert Into LogSistema Values ('Não existe um token vinculado a este e-mail. Token não foi incluído.', GETUTCDATE())
		Select Top 1 DESCRICAO From LogSistema Order By IdLOG Desc;		
	END
	ELSE
	BEGIN
		Declare @Usuario Int = (Select IdUSUARIO From DbSosPernambucanas..Usuario With(nolock) Where EMAIL = @Email And SITUACAO <> 2 And Tipo = 1)
		Insert Into DbSosPernambucanas..Token Values (@Usuario, @Email, @UrlBase, @StrToken, NULL);		
		Insert Into LogSistema Values ('Token "' + @StrToken + '" incluído com sucesso.', GETUTCDATE())
		Select Top 1 DESCRICAO From LogSistema Order By IdLOG Desc;		
	END
END
GO