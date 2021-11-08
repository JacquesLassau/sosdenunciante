USE [DbSosPernambucanas]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_EditarDenunciante]  
	@IdDenunciante int,
	@Nome varchar(200),
	@Telefone varchar(200),	
	@Email varchar(200),
	@Senha varchar (200)
AS
BEGIN
	IF NOT EXISTS (Select Usu.EMAIL From DbSosPernambucanas..Usuario As Usu With(nolock) 
						  Inner Join DbSosPernambucanas..Denunciante As Denuncia With(nolock)
						  On Usu.IdUSUARIO = Denuncia.USUARIO
						  Where Usu.EMAIL = @Email and Denuncia.IdDENUNCIANTE = @IdDenunciante)
	BEGIN					
		Insert Into LogSistema Values ('Acesso Negado! Script maldoso tentou alterar o sistema. Denunciante não cadastrado!', GETUTCDATE())
		Select Top 1 DESCRICAO From LogSistema Order By IdLOG Desc;	
	END	
	ELSE
	BEGIN
		Update DbSosPernambucanas..Denunciante Set TELEFONE = @Telefone, NOME = @Nome Where IdDENUNCIANTE = @IdDenunciante  
		Update DbSosPernambucanas..Usuario Set SENHA = @Senha Where EMAIL = @Email  
		Insert Into LogSistema Values ('Denunciante "' + @Nome + '" alterado com sucesso.', GETUTCDATE())  
		Select Top 1 DESCRICAO From LogSistema Order By IdLOG Desc;		
	END
END
GO