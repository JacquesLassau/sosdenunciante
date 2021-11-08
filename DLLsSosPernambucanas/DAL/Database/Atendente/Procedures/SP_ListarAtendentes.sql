USE [DbSosPernambucanas]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_ListarAtendentes]  

AS
BEGIN
	IF NOT EXISTS (Select Usu.EMAIL From DbSosPernambucanas..Usuario As Usu With(nolock) 
						  Inner Join DbSosPernambucanas..Atendente As Atende With(nolock)
						  On Usu.IdUSUARIO = Atende.USUARIO
						  Where Usu.SITUACAO <> 2)
	BEGIN					
		Insert Into LogSistema Values ('Não foi possível listar atendentes. Não existe nenhum registro ativo na base de dados.', GETUTCDATE())		
	END	
	ELSE
	BEGIN
		Select Atende.IdATENDENTE, Atende.MATRICULA, Atende.NOME, Usu.EMAIL, Usu.SENHA From DbSosPernambucanas..Usuario As Usu With(nolock) 
						  Inner Join DbSosPernambucanas..Atendente As Atende With(nolock)
						  On Usu.IdUSUARIO = Atende.USUARIO
						  Where Usu.SITUACAO <> 2
	END
END
GO