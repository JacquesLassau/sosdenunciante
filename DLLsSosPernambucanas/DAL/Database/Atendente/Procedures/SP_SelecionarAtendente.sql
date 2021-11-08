USE [DbSosPernambucanas]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE PROCEDURE [dbo].[SP_SelecionarAtendente]
 @IdAtendente int  
AS  
BEGIN  
 DECLARE @IdUsuario INT = (Select USUARIO From DbSosPernambucanas..Atendente Where IdATENDENTE = @IdAtendente)
 IF NOT EXISTS (Select IdATENDENTE From DbSosPernambucanas..Atendente As Atende With(nolock) 
				Inner Join DbSosPernambucanas..Usuario As Usu With(nolock) On Atende.USUARIO = Usu.IdUSUARIO
				Where Atende.IdATENDENTE = @IdAtendente And Usu.SITUACAO In (Select SITUACAO From Usuario Where IdUSUARIO = @IdUsuario And SITUACAO <> 2))				
 BEGIN       
  Insert Into LogSistema Values ('Não existe Atendente para o código informado.', GETUTCDATE())     
 END   
 ELSE  
 BEGIN  
  Select Atende.IdATENDENTE, 
         Atende.NOME, 
         Atende.MATRICULA, 
         Usu.EMAIL,
         Usu.SENHA    
      From DbSosPernambucanas..Usuario As Usu With(nolock) Inner Join DbSosPernambucanas..Atendente As Atende With(nolock)  
      On Usu.IdUSUARIO = Atende.USUARIO  
      Where Atende.IdATENDENTE = @IdAtendente;  
 END  
END