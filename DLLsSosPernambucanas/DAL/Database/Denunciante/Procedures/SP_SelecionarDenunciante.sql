USE [DbSosPernambucanas]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_SelecionarDenunciante]
 @IdDenunciante int  
AS  
BEGIN  
 DECLARE @IdUsuario INT = (Select USUARIO From DbSosPernambucanas..Denunciante Where IdDENUNCIANTE = @IdDenunciante)
 IF NOT EXISTS (Select IdDENUNCIANTE From DbSosPernambucanas..Denunciante As Denuncia With(nolock) 
				Inner Join DbSosPernambucanas..Usuario As Usu With(nolock) On Denuncia.USUARIO = Usu.IdUSUARIO
				Where Denuncia.IdDENUNCIANTE = @IdDenunciante And Usu.SITUACAO In (Select SITUACAO From Usuario Where IdUSUARIO = @IdUsuario And SITUACAO <> 2))				
 BEGIN       
  Insert Into LogSistema Values ('Não existe Denunciante para o código informado.', GETUTCDATE())     
 END   
 ELSE  
 BEGIN  
  Select       
      Denuncia.IdDENUNCIANTE,  
      Denuncia.NOME,
      Denuncia.TELEFONE,        
      Usu.EMAIL,
      Usu.SENHA
      From DbSosPernambucanas..Usuario As Usu With(nolock) Inner Join DbSosPernambucanas..Denunciante As Denuncia With(nolock)  
      On Usu.IdUSUARIO = Denuncia.USUARIO  
      Where Denuncia.IdDENUNCIANTE = @IdDenunciante;  
 END  
END