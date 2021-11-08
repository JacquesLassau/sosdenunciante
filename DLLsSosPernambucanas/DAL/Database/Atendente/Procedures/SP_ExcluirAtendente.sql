USE [DbSosPernambucanas]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_ExcluirAtendente]
	@IdAtendente int,	
	@Email varchar(200),
	@Situacao int
AS
BEGIN
	IF NOT EXISTS (Select Usu.EMAIL From DbSosPernambucanas..Usuario As Usu With(nolock) 
						  Inner Join DbSosPernambucanas..Atendente As Atende With(nolock)
						  On Usu.IdUSUARIO = Atende.USUARIO
						  Where Usu.EMAIL = @Email and Atende.IdATENDENTE = @IdAtendente)
	BEGIN					
		Insert Into LogSistema Values ('Acesso Negado! Script maldoso tentou alterar o sistema. Atendente não excluído!', GETUTCDATE())
		Select Top 1 DESCRICAO From LogSistema Order By IdLOG Desc;	
	END	
	ELSE
	BEGIN		
		declare @Nome varchar(200) = (Select NOME From DbSosPernambucanas..Atendente Where IdATENDENTE = @IdAtendente)
		Update DbSosPernambucanas..Usuario Set SITUACAO = @Situacao Where EMAIL = @Email
		Insert Into LogSistema Values ('Atendente "' + @Nome + '" excluído com sucesso.', GETUTCDATE())
		Select Top 1 DESCRICAO From LogSistema Order By IdLOG Desc;		
	END
END
GO