USE [DbSosPernambucanas] 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_BuscarValidadeToken]		
	@StrToken varchar(200)
AS
BEGIN	
	IF EXISTS (Select DATaACESSO From DbSosPernambucanas..Token With(nolock) Where STrTOKEN = @StrToken And DATaACESSO IS NOT NULL)
	BEGIN			
		Insert Into LogSistema Values ('Token expirou. Por favor, solicite novamente a recuperação de senha.', GETUTCDATE())		
	END	
	ELSE
	BEGIN
		Select STrTOKEN From DbSosPernambucanas..Token With(nolock) Where STrTOKEN = @StrToken
	END
END
GO
