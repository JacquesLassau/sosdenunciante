USE [DbSosPernambucanas]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_ListarLocaisApoio]  

AS
BEGIN
	IF NOT EXISTS (Select * From DbSosPernambucanas..Mapa With(nolock))
	BEGIN					
		Insert Into LogSistema Values ('Não existem locais de apoio cadastrados no sistema.', GETUTCDATE());		
	END	
	ELSE
	BEGIN
		Select IdLOCAL,LATITUDE,LONGITUDE,LOGRADOURO,NUMERO,BAIRRO,CIDADE,ESTADO,CEP,TELEFONE From DbSosPernambucanas..Mapa With(nolock)
	END
END
GO