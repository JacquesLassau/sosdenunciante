USE [DbSosPernambucanas]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_AlterarSituacaoLigacao]  
	@IdOcorrencia int,
	@SituacaoLigacao int
AS
BEGIN
	IF NOT EXISTS (Select IdOCORRENCIA From DbSosPernambucanas..Ocorrencia With(nolock) Where IdOCORRENCIA = @IdOcorrencia)
	BEGIN					
		Insert Into LogSistema Values ('Acesso Negado! Script maldoso tentou alterar o sistema. Ligação não modificada!', GETUTCDATE());	
	END	
	ELSE
	BEGIN
		declare @idRegistroLigacao int = (Select REGISTRoLIGACAO From DbSosPernambucanas..Ocorrencia Where IdOCORRENCIA = @IdOcorrencia);
		Update DbSosPernambucanas..Ocorrencia Set SITUACAoLIGACAO = @SituacaoLigacao Where IdOCORRENCIA = @IdOcorrencia;
		Update DbSosPernambucanas..RegistroLigacao Set SITUACAO = @SituacaoLigacao Where IdREGISTRoLIGACAO = @idRegistroLigacao;
		Select IdOCORRENCIA From Ocorrencia Where IdOCORRENCIA = @IdOcorrencia;
	END
END
GO