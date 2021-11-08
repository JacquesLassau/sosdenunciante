USE [DbSosPernambucanas] 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_AcessoUsuario]
	@Email varchar(200),
	@Senha varchar(200), 
	@Tipo int
AS
BEGIN
	BEGIN TRAN 
		IF NOT EXISTS (Select EMAIL, SENHA From DbSosPernambucanas..Usuario With(nolock) Where EMAIL = @Email And SENHA = @Senha And SITUACAO <> 2 And TIPO = @Tipo)
		BEGIN			
			PRINT 'E-mail ou senha inválido.'
			ROLLBACK
		END	
		ELSE
		BEGIN
			Select IdUSUARIO, EMAIL, TIPO From DbSosPernambucanas..Usuario With(nolock) Where EMAIL = @Email And SENHA = @Senha And SITUACAO <> 2 And TIPO = @Tipo
			COMMIT
		END
END
GO