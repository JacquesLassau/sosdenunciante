namespace DLLsSosPernambucanas.Infrastructure
{
    public static class Constantes
    {
        //Para cada usuário adicionado é necessário
        //um novo registro da tabela SituacaoUsuario
        #region Situações
        public enum SituacaoUsuario
        {
            ATIVO = 1,
            INATIVO = 2            
        }
        #endregion

        //Para cada usuário adicionado é necessário
        //um novo registro da tabela TipoUsuario
        #region Tipos de Usuários 
        public enum TipoUsuario
        {
            DENUNCIANTE = 1,
            ATENDENTE = 2
        }
        #endregion
                
        #region Situacao das Ligações nas Ocorrências
        public enum SituacaoLigacaoOcorrencia
        {
            EM_ESPERA = 1,
            EM_ANDAMENTO = 2,
            FINALIZADAS = 3
        }
        #endregion

        //Para cada número adicionado é necessário
        //um novo registro da tabela TipoLigacao
        #region Tipos de Ligações         
        public const string CENTRAL_ATENDIMENTO_MULHER = "180";
        public const string POLICIA = "190";
        public const string CENTRAL_ATENDIMENTO_MULHER_DESCRICAO = "Central de Atendimento a Mulher";
        public const string POLICIA_DESCRICAO = "Polícia";
        #endregion

        #region Alertas Controller
        public const string MENSAGEM_TEMP_DATA_TITULO = "Título do Alerta enviado pelo Servidor";
        public const string MENSAGEM_TEMP_DATA_DESCRICAO = "Descrição do Alerta enviado pelo Servidor";        
        #endregion

        #region Alertas Denunciante
        public const string CADASTRO_DENUNCIANTE_SUCESSO = "Denunciante cadastrada com sucesso!";
        public const string ALTERACAO_DENUNCIANTE_SUCESSO = "Denunciante alterada com sucesso!";
        public const string EXCLUSAO_DENUNCIANTE_SUCESSO = "Denunciante excluída com sucesso!";
        public const string CONSULTA_DENUNCIANTE_VAZIA = "Denunciante não encontrada!";
        public const string CONSULTA_DENUNCIANTE_VAZIA_DESCRICAO = "Por favor, preencha o código ou clique na lupa para lista de Denunciantes.";
        #endregion

        #region Alertas Atendente       
        public const string CADASTRO_ATENDENTE_SUCESSO = "Atendente cadastrado com sucesso!";
        public const string ALTERACAO_ATENDENTE_SUCESSO = "Atendente alterado com sucesso!";
        public const string EXCLUSAO_ATENDENTE_SUCESSO = "Atendente excluído com sucesso!";
        public const string CONSULTA_ATENDENTE_VAZIA = "Atendente não encontrado!";
        public const string CONSULTA_ATENDENTE_VAZIA_DESCRICAO = "Por favor, preencha o código ou clique na lupa para listar os atendentes.";
        #endregion

        #region Alertas Local de Apoio       
        public const string CADASTRO_LOCAL_APOIO_SUCESSO = "Local de apoio cadastrado com sucesso!";        
        public const string EXCLUSAO_LOCAL_APOIO_SUCESSO = "Local de apoio excluído com sucesso!";        
        #endregion

        #region Alertas Cadastro        
        public const string EMAIL_EXISTENTE_DENUNCIANTE = "Este e-mail já existe vinculado no sistema.";
        public const string EMAIL_EXISTENTE_DENUNCIANTE_DESCRICAO = "Você deve incluir outro e-mail ou realizar login com o existente.";
        #endregion

        #region Alertas Acesso        
        public const string FALHA_LOGIN = "E-mail ou senha inválidos!";
        public const string EMAIL_ENVIADO_SUCESSO = "E-mail enviado com sucesso!";
        public const string EMAIL_ENVIADO_SUCESSO_DESCRICAO = "Por favor, verifique a sua caixa de entrada e siga as instruções para redefinir a senha.";
        public const string NOVA_SENHA_SUCESSO = "Senha Alterada com Sucesso!";
        public const string NOVA_SENHA_SUCESSO_DESCRICAO = "Pressione o botão OK e realize o login novamente.";
        public const string EMAIL_NAO_ENCONTRADO = "E-mail não encontrado!";
        public const string EMAIL_NAO_ENCONTRADO_DESCRICAO = "Verifique o e-mail digitado e tente novamente.";
        public const string LINK_EXPIRADO = "O link expirou ou o token é inválido.";
        public const string LINK_EXPIRADO_DESCRICAO = "Cada link só pode ser usado uma vez. Solicite uma nova recuperação de senha.";
        public const string LINK_TOKEN_EMAIL_INVALIDOS = "Não existe nenhum e-mail vinculado a este token. Por favor, contate o suporte especializado.";
        #endregion

        #region Alertas Genericos        
        public const string TITULO_ATENCAO = "Atenção:";
        public const string REDIRECT = "Por favor, escolha uma das opções abaixo:";
        public const string FALHA_REDIRECT = "Houve um problema no redirecionamento! Tente novamente.";
        public const string NAO_VALIDO = "Preenchimento dos dados Inválidos!";
        public const string DESCRICAO_NAO_VALIDO = "Por favor, tente novamente.";
        public const string ERRO_NAO_TRATADO = "Ocorreu um erro no sistema. Por favor, contate o suporte especializado.";
        #endregion
    }
}
