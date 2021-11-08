USE [DbSosPernambucanas]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_CadastrarLocalApoio] 
	@Latitude varchar(200),
	@Longitude varchar(200),
	@Logradouro varchar(200),
	@Numero varchar(200) = NULL,
	@Bairro varchar(200),
	@Cidade varchar(200),
	@Estado char(200),
	@Cep varchar(9),
	@Telefone varchar(200)
AS
BEGIN
	Insert Into DbSosPernambucanas..Mapa Values (@Latitude, @Longitude, @Logradouro, @Numero, @Bairro, @Cidade, @Estado, @Cep, @Telefone)
	Insert Into LogSistema Values ('Local de Apoio com as coordenadas  "' + @Latitude + '" e "' + @Longitude + '" incluído com sucesso.', GETUTCDATE())			
END
GO