USE [DbSosPernambucanas] 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Jacques de Lassau, Wanderley Marques, Jamilly Victória>
-- Create date: <12/08/2021>
-- Description:	<Inclusão de Novo Usuário>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CadastrarUsuario]	
	@Tipo int,
	@Situacao int,
	@Email varchar(200),
	@Senha varchar(200)		
AS
BEGIN	
	IF EXISTS (Select EMAIL From DbSosPernambucanas..Usuario With(nolock) Where EMAIL = @Email And SITUACAO <> 2)
	BEGIN			
		Insert Into LogSistema Values ('Já existe um usuário vinculado a este e-mail. Usuário não foi incluído.', GETUTCDATE())			
		Select Top 1 DESCRICAO From LogSistema Order By IdLOG Desc;
	END
	ELSE
	BEGIN
		Insert Into DbSosPernambucanas..Usuario Values (@Tipo, @Situacao, @Email, @Senha);		
		Insert Into LogSistema Values ('Usuário "' + @Email + '" incluído com sucesso.', GETUTCDATE())				
	END
END
GO