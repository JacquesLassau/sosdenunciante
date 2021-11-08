USE [DbSosPernambucanas] 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Jacques de Lassau, Wanderley Marques, Jamilly Victória>
-- Create date: <17/08/2021>
-- Description:	<Alterar senha de Usuário>
-- =============================================
CREATE PROCEDURE [dbo].[SP_AlterarSenhaUsuario]		
	@Email varchar(200),
	@NovaSenha varchar(200),
	@Tipo int
AS
BEGIN	
	IF NOT EXISTS (Select EMAIL From DbSosPernambucanas..Usuario With(nolock) Where EMAIL = @Email And SITUACAO <> 2 And TIPO = @Tipo)
	BEGIN			
		Insert Into LogSistema Values ('Não existe um usuário vinculado a este e-mail. Nova senha não foi incluída.', GETUTCDATE())
		Select Top 1 DESCRICAO From LogSistema Order By IdLOG Desc;		
	END
	ELSE
	BEGIN
		Update DbSosPernambucanas..Usuario Set SENHA = @NovaSenha Where EMAIL = @Email And TIPO = @Tipo
		Insert Into LogSistema Values ('Senha do usuário "' + @Email + '" foi alterada com sucesso.', GETUTCDATE())
		Select Top 1 DESCRICAO From LogSistema Order By IdLOG Desc;		
	END
END
GO
