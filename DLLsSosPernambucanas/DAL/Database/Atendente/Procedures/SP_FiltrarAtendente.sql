USE [DbSosPernambucanas]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_FiltrarAtendentes]
 @IdAtendente int,
 @NomeAtendente varchar(200)
AS  
BEGIN   
 DECLARE @IdUsuario INT = (Select USUARIO From DbSosPernambucanas..Atendente Where IdATENDENTE = @IdAtendente)
 IF NOT EXISTS (Select Atende.IdATENDENTE From DbSosPernambucanas..Atendente As Atende With(nolock) 
				Inner Join DbSosPernambucanas..Usuario As Usu With(nolock) On Atende.USUARIO = Usu.IdUSUARIO
				Where (Atende.IdATENDENTE = @IdAtendente Or Atende.NOME Like '%' + @NomeAtendente + '%') And Usu.SITUACAO In (Select SITUACAO From Usuario Where SITUACAO <> 2))
 BEGIN       
  Insert Into LogSistema Values ('Não existe Atendente(s) para o código ou nome informado(s).', GETUTCDATE())     
 END   
 ELSE  
 BEGIN  
  Select Atende.IdATENDENTE, Atende.MATRICULA, Atende.NOME From DbSosPernambucanas..Atendente As Atende With(nolock) 
				Inner Join DbSosPernambucanas..Usuario As Usu With(nolock) On Atende.USUARIO = Usu.IdUSUARIO
				Where (Atende.IdATENDENTE = @IdAtendente Or Atende.NOME Like '%' + @NomeAtendente + '%') And Usu.SITUACAO In (Select SITUACAO From Usuario Where SITUACAO <> 2)  
 END  
END