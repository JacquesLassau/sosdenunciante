USE [DbSosPernambucanas]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_CadastrarAtendente]  
	@Matricula varchar(200),
	@Nome varchar(200),
	@Email varchar(200)
AS
BEGIN
	IF NOT EXISTS (Select EMAIL From DbSosPernambucanas..Usuario With(nolock) Where EMAIL = @Email)
	BEGIN					
		Insert Into LogSistema Values ('Acesso Negado! Não é possível incluir um atendente sem e-mail no sistema!', GETUTCDATE())
		Select Top 1 DESCRICAO From LogSistema Order By IdLOG Desc;	
	END	
	ELSE
	BEGIN
		declare @Usuario int = (Select IdUSUARIO From DbSosPernambucanas..Usuario With(nolock) Where EMAIL = @Email);		 		
		Insert Into DbSosPernambucanas..Atendente Values (@Usuario, @Matricula, @Nome);
		Insert Into LogSistema Values ('Atendente "' + @Nome + '" incluído com sucesso.', GETUTCDATE())
		Select Top 1 DESCRICAO From LogSistema Order By IdLOG Desc;		
	END
END
GO