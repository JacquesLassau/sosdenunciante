Use [DbSosPernambucanas]

IF EXISTS (Select O.name From SYSOBJECTS O with(nolock) where name = 'SituacaoUsuario')
BEGIN
	PRINT 'Tabela já existe na base de dados. Não foi possível criar a tabela SituacaoUsuario.';
END
ELSE
BEGIN 
	CREATE TABLE SituacaoUsuario(
		IdSITUACAoUSUARIO int Identity NOT NULL,
		DESCRICAO varchar(200) NOT NULL
		CONSTRAINT SituacaoUsuario_PK PRIMARY KEY CLUSTERED (IdSITUACAoUSUARIO)
	)
	INSERT INTO SituacaoUsuario Values ('Ativo')
	INSERT INTO SituacaoUsuario Values ('Inativo')
	PRINT 'A tabela SituacaoUsuario foi criada com sucesso!';
END

IF EXISTS (Select O.name From SYSOBJECTS O with(nolock) where name = 'TipoUsuario')
BEGIN
	PRINT 'Tabela já existe na base de dados. Não foi possível criar a tabela TipoUsuario.';
END
ELSE
BEGIN
	CREATE TABLE TipoUsuario(
		IdTIPoUSUARIO int Identity NOT NULL,
		DESCRICAO varchar(200) NOT NULL
		CONSTRAINT TipoUsuario_PK PRIMARY KEY CLUSTERED (IdTIPoUSUARIO)
	)
	INSERT INTO TipoUsuario Values ('Denunciante')
	INSERT INTO TipoUsuario Values ('Atendente')	
	PRINT 'A tabela TipoUsuario foi criada com sucesso!';
END

IF EXISTS (Select O.name From SYSOBJECTS O with(nolock) where name = 'Usuario')
BEGIN			
	PRINT 'Tabela já existe na base de dados. Não foi possível criar a tabela Usuario.'	;
END
ELSE
BEGIN
	CREATE TABLE Usuario(	
		IdUSUARIO int IDENTITY NOT NULL,
		TIPO int NOT NULL,
	    SITUACAO int NOT NULL,    
		EMAIL varchar(200) NOT NULL, 
	    SENHA varchar(200) NOT NULL,	    
		CONSTRAINT Usuario_PK PRIMARY KEY CLUSTERED (IdUSUARIO),
        CONSTRAINT Usuario_FK_01 FOREIGN KEY (TIPO) REFERENCES TipoUsuario(IdTIPoUSUARIO),
        CONSTRAINT Usuario_FK_02 FOREIGN KEY (SITUACAO) REFERENCES SituacaoUsuario(IdSITUACAoUSUARIO)				    
	)
	PRINT 'A tabela Usuario foi criada com sucesso!';
END

IF EXISTS (Select O.name From SYSOBJECTS O with(nolock) where name = 'Token')
BEGIN			
	PRINT 'Tabela já existe na base de dados. Não foi possível criar a tabela Token.';
END
ELSE
BEGIN
	CREATE TABLE Token(			
		USUARIO int NOT NULL, 
	    EMAIL varchar(200) NOT NULL,	    
		URLBASE varchar(200) NOT NULL,
		STrTOKEN varchar(200) NOT NULL,	        
		DATaACESSO DATETIME NULL		
        CONSTRAINT Token_FK_01 FOREIGN KEY (USUARIO) REFERENCES Usuario(IdUSUARIO)        			    
	)
	PRINT 'A tabela Token foi criada com sucesso!';
END

IF EXISTS (Select O.name From SYSOBJECTS O with(nolock) where name = 'Denunciante')
BEGIN			
	PRINT 'Tabela já existe na base de dados. Não foi possível criar a tabela Denunciante.';
END
ELSE
BEGIN
	CREATE TABLE Denunciante(	
		IdDENUNCIANTE int IDENTITY NOT NULL,			       
		USUARIO int NOT NULL, 	    
        NOME varchar(200) NOT NULL,
		TELEFONE varchar(200) NULL,		
		CONSTRAINT Denunciante_PK PRIMARY KEY CLUSTERED (IdDENUNCIANTE),          
        CONSTRAINT Denunciante_FK_01 FOREIGN KEY (USUARIO) REFERENCES Usuario(IdUSUARIO)        		    
	)
	PRINT 'A tabela Denunciante foi criada com sucesso!';
END

IF EXISTS (Select O.name From SYSOBJECTS O with(nolock) where name = 'Atendente')
BEGIN			
	PRINT 'Tabela já existe na base de dados. Não foi possível criar a tabela Atendente.';
END
ELSE
BEGIN
	CREATE TABLE Atendente(	
		IdATENDENTE int IDENTITY NOT NULL,			       
		USUARIO int NOT NULL, 	    
        MATRICULA varchar(200) NULL,
		NOME varchar(200) NOT NULL,
		CONSTRAINT Atendente_PK PRIMARY KEY CLUSTERED (IdATENDENTE),          
        CONSTRAINT Atendente_FK_01 FOREIGN KEY (USUARIO) REFERENCES Usuario(IdUSUARIO)        		    
	)
	PRINT 'A tabela Atendente foi criada com sucesso!';
END

IF EXISTS (Select O.name From SYSOBJECTS O with(nolock) where name = 'Mapa')
BEGIN			
	PRINT 'Tabela já existe na base de dados. Não foi possível criar a tabela Mapa.';
END
ELSE
BEGIN
	CREATE TABLE Mapa(	
		IdLOCAL int IDENTITY NOT NULL,
		LATITUDE varchar(200) NOT NULL, 	    
        LONGITUDE varchar(200) NOT NULL,
		LOGRADOURO varchar(200) NOT NULL,
		NUMERO varchar(200) NULL,
		BAIRRO varchar(200) NOT NULL,
		CIDADE varchar(200) NOT NULL,
		ESTADO varchar(200) NOT NULL, 
		CEP varchar(200) NOT NULL,
		TELEFONE varchar(200) NULL
		CONSTRAINT Mapa_PK PRIMARY KEY CLUSTERED (IdLOCAL),
	)
	PRINT 'A tabela Mapa foi criada com sucesso!';
END

IF EXISTS (Select O.name From SYSOBJECTS O with(nolock) where name = 'TipoLigacao')
BEGIN
	PRINT 'Tabela já existe na base de dados. Não foi possível criar a tabela TipoLigacao.';
END
ELSE
BEGIN
	CREATE TABLE TipoLigacao(
		NUMERO varchar(200) NOT NULL,
		DESCRICAO varchar(200) NOT NULL		
	)
	INSERT INTO TipoLigacao Values ('180','Central de Atendimento a Mulher')
	INSERT INTO TipoLigacao Values ('190','Polícia')	
	PRINT 'A tabela TipoLigacao foi criada com sucesso!';
END

IF EXISTS (Select O.name From SYSOBJECTS O with(nolock) where name = 'RegistroLigacao')
BEGIN			
	PRINT 'Tabela já existe na base de dados. Não foi possível criar a tabela RegistroLigacao.';
END
ELSE
BEGIN
	CREATE TABLE RegistroLigacao(	
		IdREGISTRoLIGACAO int IDENTITY NOT NULL,			       		    
        USUARIO int NOT NULL,
        EMAIL varchar(200) NOT NULL,        
		NUMERO varchar(200) NOT NULL,
		DESCRICAO varchar(200) NOT NULL,
		DATaHORA DATETIME NOT NULL,  
		SITUACAO int NOT NULL,
		CONSTRAINT RegistroLigacao_PK PRIMARY KEY CLUSTERED (IdREGISTRoLIGACAO),
		CONSTRAINT RegistroLigacao_FK_01 FOREIGN KEY (USUARIO) REFERENCES Usuario(IdUSUARIO)              			    
	)
	PRINT 'A tabela RegistroLigacao foi criada com sucesso!';
END

IF EXISTS (Select O.name From SYSOBJECTS O with(nolock) where name = 'Ocorrencia')
BEGIN			
	PRINT 'Tabela já existe na base de dados. Não foi possível criar a tabela Ocorrencia.';
END
ELSE
BEGIN
	CREATE TABLE Ocorrencia(	
		IdOCORRENCIA int IDENTITY NOT NULL,			       		    
		DENUNCIANTE int NOT NULL,
        NOMeDENUNCIANTE varchar(200) NOT NULL,
        TELEFONeDENUNCIANTE varchar(200) NOT NULL,        		
		REGISTRoLIGACAO int NOT NULL,
		NUMERoTELEFONeLIGACAO varchar(200) NOT NULL,
		DESCRICAoTELEFONeLIGACAO varchar(200) NOT NULL,		
		DATaHORaLIGACAO DATETIME NOT NULL,  
		SITUACAoLIGACAO int NOT NULL,
		DESCRICAoOCORRENCIaLIGACAO varchar(max) NULL
		CONSTRAINT Ocorrencia_PK PRIMARY KEY CLUSTERED (IdOCORRENCIA),
		CONSTRAINT Ocorrencia_FK_01 FOREIGN KEY (DENUNCIANTE) REFERENCES Denunciante(IdDENUNCIANTE),
		CONSTRAINT Ocorrencia_FK_02 FOREIGN KEY (REGISTRoLIGACAO) REFERENCES RegistroLigacao(IdREGISTRoLIGACAO)              			    
	)
	PRINT 'A tabela Ocorrencia foi criada com sucesso!';
END

IF EXISTS (Select O.name From SYSOBJECTS O with(nolock) where name = 'LogSistema')
BEGIN			
	PRINT 'Tabela já existe na base de dados. Não foi possível criar a tabela LogSistema.';
END
ELSE
BEGIN
	CREATE TABLE LogSistema(	
		IdLOG int IDENTITY NOT NULL,			       		    
        DESCRICAO varchar(200) NOT NULL,
        DATaHORA DATETIME NOT NULL,        
		CONSTRAINT LogSistema_PK PRIMARY KEY CLUSTERED (IdLOG)             			    
	)
	PRINT 'A tabela LogSistema foi criada com sucesso!';
END