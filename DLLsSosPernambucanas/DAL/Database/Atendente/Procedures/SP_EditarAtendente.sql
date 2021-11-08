USE [DbSosPernambucanas]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_EditarAtendente]  
	@IdAtendente int,
	@Matricula varchar(200),
	@Nome varchar(200),
	@Email varchar(200),
	@Senha varchar (200)
AS
BEGIN
	IF NOT EXISTS (Select Usu.EMAIL From DbSosPernambucanas..Usuario As Usu With(nolock) 
						  Inner Join DbSosPernambucanas..Atendente As Atende With(nolock)
						  On Usu.IdUSUARIO = Atende.USUARIO
						  Where Usu.EMAIL = @Email and Atende.IdATENDENTE = @IdAtendente)
	BEGIN					
		Insert Into LogSistema Values ('Acesso Negado! Script maldoso tentou alterar o sistema. Atendente não cadastrado!', GETUTCDATE())
		Select Top 1 DESCRICAO From LogSistema Order By IdLOG Desc;	
	END	
	ELSE
	BEGIN
		Update DbSosPernambucanas..Atendente Set MATRICULA = @Matricula, NOME = @Nome Where IdATENDENTE = @IdAtendente
		Update DbSosPernambucanas..Usuario Set SENHA = @Senha Where EMAIL = @Email
		Insert Into LogSistema Values ('Atendente "' + @Nome + '" alterado com sucesso.', GETUTCDATE())
		Select Top 1 DESCRICAO From LogSistema Order By IdLOG Desc;		
	END
END
GO