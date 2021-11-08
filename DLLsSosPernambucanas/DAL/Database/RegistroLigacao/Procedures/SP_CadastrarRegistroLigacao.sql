USE [DbSosPernambucanas]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_CadastrarRegistroLigacao]  	
	@Email varchar(200),
	@Numero int,
	@Descricao varchar(200)
AS
BEGIN
	IF NOT EXISTS (Select EMAIL From DbSosPernambucanas..Usuario With(nolock) Where EMAIL = @Email)
	BEGIN					
		Insert Into LogSistema Values ('Acesso Negado! Não é possível incluir um registro de ligação com usuário sem e-mail no sistema!', GETUTCDATE());
		Select Top 1 DESCRICAO From LogSistema Order By IdLOG Desc;	
	END	
	ELSE
	BEGIN
		declare @Usuario int = (Select IdUSUARIO From DbSosPernambucanas..Usuario With(nolock) Where EMAIL = @Email);		 		
		Insert Into DbSosPernambucanas..RegistroLigacao Values (@Usuario, @Email, @Numero, @Descricao, GETUTCDATE(), 1);
		
		Insert Into Ocorrencia
				Select top 1
					Denuncia.IdDENUNCIANTE,  
					Denuncia.NOME,
					Denuncia.TELEFONE,
					Ligacao.IdREGISTRoLIGACAO,
					Ligacao.NUMERO,
					Ligacao.DESCRICAO,
					Ligacao.DATaHORA,
					Ligacao.SITUACAO,
					NULL
				From RegistroLigacao As Ligacao
					Inner Join Denunciante As Denuncia On Ligacao.USUARIO = Denuncia.USUARIO
				Where Ligacao.USUARIO = @Usuario Order By Ligacao.IdREGISTRoLIGACAO Desc

		Insert Into LogSistema Values ('Registro de ligação para "' + @Descricao + '" incluído com sucesso.', GETUTCDATE());
		Insert Into LogSistema Values ('Ocorrencia da(o) denunciante "' + @Email + '" incluída com sucesso.', GETUTCDATE());		
	END
END
GO