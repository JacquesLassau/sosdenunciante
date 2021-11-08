USE [DbSosPernambucanas]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_ExcluirLocalApoio] 
	@IdLocal int
AS
BEGIN
	Delete From DbSosPernambucanas..Mapa Where IdLocal = @IdLocal
	Insert Into LogSistema Values ('Local de Apoio com ID ' + CONVERT(varchar(10), @IdLocal) + ' excluído fisicamente com sucesso.', GETUTCDATE())			
END
GO