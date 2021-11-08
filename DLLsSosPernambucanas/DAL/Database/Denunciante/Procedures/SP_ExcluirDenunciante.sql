USE [DbSosPernambucanas]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_ExcluirDenunciante]
	@IdDenunciante int,	
	@Email varchar(200),
	@Situacao int
AS
BEGIN
	IF NOT EXISTS (Select Usu.EMAIL From DbSosPernambucanas..Usuario As Usu With(nolock) 
						  Inner Join DbSosPernambucanas..Denunciante As Denuncia With(nolock)
						  On Usu.IdUSUARIO = Denuncia.USUARIO
						  Where Usu.EMAIL = @Email and Denuncia.IdDENUNCIANTE = @IdDenunciante)
	BEGIN					
		Insert Into LogSistema Values ('Acesso Negado! Script maldoso tentou alterar o sistema. Denunciante não excluído!', GETUTCDATE())
		Select Top 1 DESCRICAO From LogSistema Order By IdLOG Desc;	
	END	
	ELSE
	BEGIN		
		declare @Nome varchar(200) = (Select NOME From DbSosPernambucanas..Denunciante Where IdDENUNCIANTE = @IdDenunciante)
		Update DbSosPernambucanas..Usuario Set SITUACAO = @Situacao Where EMAIL = @Email
		Insert Into LogSistema Values ('Denunciante "' + @Nome + '" excluído com sucesso.', GETUTCDATE())
		Select Top 1 DESCRICAO From LogSistema Order By IdLOG Desc;		
	END
END
GO