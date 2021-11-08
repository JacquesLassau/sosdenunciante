USE [DbSosPernambucanas]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_ListarDenunciantes]  

AS
BEGIN
	IF NOT EXISTS (Select Usu.EMAIL From DbSosPernambucanas..Usuario As Usu With(nolock) 
						  Inner Join DbSosPernambucanas..Denunciante As Denuncia With(nolock)
						  On Usu.IdUSUARIO = Denuncia.USUARIO
						  Where Usu.SITUACAO <> 2)
	BEGIN					
		Insert Into LogSistema Values ('Não foi possível listar atendentes. Não existe nenhum registro ativo na base de dados.', GETUTCDATE())		
	END	
	ELSE
	BEGIN
		Select Denuncia.IdDENUNCIANTE, Denuncia.NOME, Denuncia.TELEFONE, Usu.EMAIL, Usu.SENHA From DbSosPernambucanas..Usuario As Usu With(nolock) 
						  Inner Join DbSosPernambucanas..Denunciante As Denuncia With(nolock)
						  On Usu.IdUSUARIO = Denuncia.USUARIO
						  Where Usu.SITUACAO <> 2
	END
END
GO