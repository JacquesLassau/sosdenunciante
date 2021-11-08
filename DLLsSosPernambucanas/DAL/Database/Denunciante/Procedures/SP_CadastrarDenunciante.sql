USE [DbSosPernambucanas]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_CadastrarDenunciante]  	
	@Email varchar(200),
	@Nome varchar(200),
	@Telefone varchar(200)
AS
BEGIN
	IF NOT EXISTS (Select top 1 EMAIL From DbSosPernambucanas..Usuario With(nolock) Where EMAIL = @Email)
	BEGIN					
		Insert Into LogSistema Values ('Acesso Negado! Não é possível incluir um denunciante sem e-mail no sistema!', GETUTCDATE())
		Select Top 1 DESCRICAO From LogSistema Order By IdLOG Desc;	
	END	
	ELSE
	BEGIN
		declare @Usuario int = (Select top 1 IdUSUARIO From DbSosPernambucanas..Usuario With(nolock) Where EMAIL = @Email And TIPO = 1);		 		
		Insert Into DbSosPernambucanas..Denunciante Values (@Usuario, @Nome, @Telefone);
		Insert Into LogSistema Values ('Denunciante "' + @Nome + '" incluído com sucesso.', GETUTCDATE())
		Select USUARIO, NOME, TELEFONE, EMAIL, SENHA from Denunciante Inner Join Usuario on Denunciante.USUARIO = Usuario.IdUSUARIO where USUARIO = @Usuario
	END
END
GO