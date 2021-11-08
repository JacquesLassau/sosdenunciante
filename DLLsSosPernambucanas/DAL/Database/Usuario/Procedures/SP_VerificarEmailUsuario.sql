USE [DbSosPernambucanas] 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Jacques de Lassau, Wanderley Marques, Jamilly Victoria>
-- Create date: <12/08/2021>
-- Description:	<Verifica e-mail Usuário>
-- =============================================
CREATE PROCEDURE [dbo].[SP_VerificarEmailUsuario]		 	
	@Tipo int,
	@Email varchar(200)
AS
BEGIN 	
	IF EXISTS (Select EMAIL From DbSosPernambucanas..Usuario With(nolock) Where EMAIL = @Email And SITUACAO <> 2 And TIPO = @Tipo)
	BEGIN			
		Insert Into LogSistema Values ('Existe um usuário vinculado a este e-mail.', GETUTCDATE())
		Select Top 1 DESCRICAO From LogSistema Order By IdLOG Desc;		
	END
	IF NOT EXISTS (Select EMAIL From DbSosPernambucanas..Usuario With(nolock) Where EMAIL = @Email)
	BEGIN			
		Insert Into LogSistema Values ('Email não existe.', GETUTCDATE())			
	END
END
GO