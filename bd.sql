USE [gerenciamento_escolar]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Administradores](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[SenhaHash] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



USE [gerenciamento_escolar]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Aluno](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NOT NULL,
	[Sobrenome] [nvarchar](100) NOT NULL,
	[DataDeNascimento] [date] NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[Telefone] [nvarchar](20) NULL,
	[DataMatricula] [date] NOT NULL,
	[Ativo] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [gerenciamento_escolar]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Categoria](
	[Id] [int] NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [gerenciamento_escolar]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Curso](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NOT NULL,
	[Descricao] [nvarchar](255) NULL,
	[DataCriacao] [date] NOT NULL,
	[Categoria] [int] NOT NULL,
	[Valor] [decimal](10, 2) NOT NULL,
	[CargaHoraria] [int] NOT NULL,
	[Ativo] [bit] NOT NULL,
	[ProfessorId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Curso]  WITH CHECK ADD FOREIGN KEY([ProfessorId])
REFERENCES [dbo].[Professor] ([Id])
GO

USE [gerenciamento_escolar]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Formacao](
	[Id] [int] NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [gerenciamento_escolar]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Professor](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NOT NULL,
	[Sobrenome] [nvarchar](100) NOT NULL,
	[DataDeNascimento] [date] NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[Telefone] [nvarchar](20) NULL,
	[Formacao] [int] NOT NULL,
	[DataContratacao] [date] NOT NULL,
	[Ativo] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE Matriculas (
    AlunoId BIGINT NOT NULL,
    CursoId BIGINT NOT NULL,
    DataMatricula DATETIME NOT NULL,
    PRIMARY KEY (AlunoId, CursoId), 
    FOREIGN KEY (AlunoId) REFERENCES Aluno(Id),
    FOREIGN KEY (CursoId) REFERENCES Curso(Id)
);


INSERT INTO Curso (Nome, Descricao, DataCriacao, Categoria, Valor, CargaHoraria, Ativo, ProfessorId)
VALUES
('Introdução à Programação', 'Curso básico de lógica de programação', '2020-01-15', 1, 500.00, 40, 1, 30005),
('Banco de Dados SQL', 'Fundamentos de SQL Server', '2020-05-20', 1, 600.00, 60, 1, 30021),
('Redes de Computadores', 'Configuração e manutenção de redes', '2021-03-10', 2, 750.00, 80, 1, 30010),
('Desenvolvimento Web', 'HTML, CSS e JavaScript', '2021-07-01', 1, 900.00, 100, 1, 30016),
('C# Avançado', 'Programação com C# e .NET', '2021-09-15', 1, 1200.00, 120, 1, 30007),
('Segurança da Informação', 'Princípios de cibersegurança', '2020-11-22', 2, 800.00, 60, 1, 30011),
('Administração de Sistemas', 'Gerenciamento de servidores Windows/Linux', '2019-02-18', 2, 950.00, 90, 1, 30006),
('Desenvolvimento Mobile', 'Criação de apps para Android e iOS', '2022-01-12', 1, 1100.00, 100, 1, 30015),
('Gestão de Projetos', 'Metodologias ágeis e tradicionais', '2020-04-05', 3, 700.00, 50, 1, 30020),
('Design de Interfaces', 'Princípios de UI/UX', '2021-06-25', 1, 650.00, 60, 1, 30003),
('Python para Data Science', 'Introdução à ciência de dados', '2022-02-14', 1, 1300.00, 120, 1, 30014),
('Machine Learning', 'Conceitos de aprendizado de máquina', '2022-05-10', 1, 1500.00, 140, 1, 30018),
('DevOps', 'Integração contínua e entrega contínua', '2021-10-30', 2, 1000.00, 100, 1, 30012),
('Administração de Banco de Dados', 'SQL Server e MySQL', '2019-12-11', 1, 850.00, 70, 1, 30017),
('Linux Essentials', 'Uso e administração do Linux', '2020-03-22', 2, 600.00, 60, 1, 30002),
('Java Avançado', 'Programação orientada a objetos com Java', '2022-06-01', 1, 1400.00, 130, 1, 30013),
('Engenharia de Software', 'Práticas de análise e design de sistemas', '2021-08-19', 3, 950.00, 90, 1, 30019),
('Cloud Computing', 'Computação em nuvem AWS e Azure', '2020-09-07', 2, 1600.00, 150, 1, 30004),
('Inteligência Artificial', 'Fundamentos de IA', '2022-03-17', 1, 1800.00, 160, 1, 10002),
('Ética e Tecnologia', 'Impactos sociais da tecnologia', '2019-05-13', 3, 500.00, 40, 1, 10002);


INSERT INTO Aluno (Nome, Sobrenome, DataDeNascimento, Email, Telefone, DataMatricula, Ativo)
VALUES
('João', 'Ferreira', '2002-05-10', 'joao.ferreira@aluno.com', '51988880001', '2021-02-01', 1),
('Ana', 'Silva', '2001-08-15', 'ana.silva@aluno.com', '51988880002', '2021-03-12', 1),
('Pedro', 'Souza', '2000-11-20', 'pedro.souza@aluno.com', '51988880003', '2020-07-05', 1),
('Mariana', 'Oliveira', '2003-03-25', 'mariana.oliveira@aluno.com', '51988880004', '2022-01-10', 1),
('Lucas', 'Pereira', '1999-12-05', 'lucas.pereira@aluno.com', '51988880005', '2019-08-21', 1),
('Fernanda', 'Costa', '2002-07-14', 'fernanda.costa@aluno.com', '51988880006', '2021-09-13', 1),
('Bruno', 'Mendes', '2001-02-17', 'bruno.mendes@aluno.com', '51988880007', '2020-05-30', 1),
('Carla', 'Ramos', '2000-04-28', 'carla.ramos@aluno.com', '51988880008', '2019-06-17', 1),
('Felipe', 'Almeida', '2002-09-09', 'felipe.almeida@aluno.com', '51988880009', '2021-11-19', 1),
('Juliana', 'Martins', '2003-01-13', 'juliana.martins@aluno.com', '51988880010', '2022-03-01', 1),
('Gabriel', 'Rodrigues', '2001-06-30', 'gabriel.rodrigues@aluno.com', '51988880011', '2020-10-10', 1),
('Larissa', 'Dias', '1999-10-21', 'larissa.dias@aluno.com', '51988880012', '2019-09-15', 1),
('Diego', 'Barbosa', '2002-12-01', 'diego.barbosa@aluno.com', '51988880013', '2021-07-28', 1),
('Beatriz', 'Teixeira', '2000-08-18', 'beatriz.teixeira@aluno.com', '51988880014', '2020-04-22', 1),
('André', 'Cardoso', '2003-11-02', 'andre.cardoso@aluno.com', '51988880015', '2022-02-07', 1),
('Rafaela', 'Campos', '2001-05-06', 'rafaela.campos@aluno.com', '51988880016', '2020-12-03', 1),
('Thiago', 'Azevedo', '2002-01-19', 'thiago.azevedo@aluno.com', '51988880017', '2021-06-29', 1),
('Camila', 'Pinto', '1999-07-27', 'camila.pinto@aluno.com', '51988880018', '2019-11-08', 1),
('Rodrigo', 'Lima', '2000-09-12', 'rodrigo.lima@aluno.com', '51988880019', '2019-02-14', 1),
('Patrícia', 'Moreira', '2003-04-04', 'patricia.moreira@aluno.com', '51988880020', '2022-04-01', 1);


INSERT INTO Professor (Nome, Sobrenome, DataDeNascimento, Email, Telefone, Formacao, DataContratacao, Ativo)
VALUES
('Carlos', 'Silva', '1980-05-12', 'carlos.silva@senac.com', '51999990001', 1, '2015-02-01', 1),
('Fernanda', 'Souza', '1975-11-23', 'fernanda.souza@senac.com', '51999990002', 2, '2010-03-15', 1),
('Roberto', 'Pereira', '1982-07-09', 'roberto.pereira@senac.com', '51999990003', 1, '2017-05-20', 1),
('Juliana', 'Almeida', '1990-01-30', 'juliana.almeida@senac.com', '51999990004', 3, '2019-08-12', 1),
('Paulo', 'Mendes', '1985-12-11', 'paulo.mendes@senac.com', '51999990005', 2, '2014-06-01', 1),
('Mariana', 'Costa', '1978-03-22', 'mariana.costa@senac.com', '51999990006', 1, '2009-09-10', 1),
('André', 'Ramos', '1988-06-19', 'andre.ramos@senac.com', '51999990007', 2, '2016-11-21', 1),
('Beatriz', 'Oliveira', '1992-08-05', 'beatriz.oliveira@senac.com', '51999990008', 3, '2020-04-25', 1),
('Lucas', 'Martins', '1984-04-14', 'lucas.martins@senac.com', '51999990009', 1, '2013-07-30', 1),
('Patrícia', 'Rodrigues', '1979-09-07', 'patricia.rodrigues@senac.com', '51999990010', 2, '2011-12-02', 1),
('Daniel', 'Ferreira', '1986-02-28', 'daniel.ferreira@senac.com', '51999990011', 3, '2018-10-18', 1),
('Camila', 'Barbosa', '1991-07-15', 'camila.barbosa@senac.com', '51999990012', 1, '2019-01-05', 1),
('Fábio', 'Teixeira', '1983-03-10', 'fabio.teixeira@senac.com', '51999990013', 2, '2014-11-09', 1),
('Rafaela', 'Dias', '1987-05-27', 'rafaela.dias@senac.com', '51999990014', 1, '2016-06-30', 1),
('Marcelo', 'Azevedo', '1981-10-01', 'marcelo.azevedo@senac.com', '51999990015', 2, '2012-08-19', 1),
('Cláudia', 'Pinto', '1993-12-25', 'claudia.pinto@senac.com', '51999990016', 3, '2021-03-14', 1),
('Eduardo', 'Lima', '1989-11-18', 'eduardo.lima@senac.com', '51999990017', 1, '2017-09-22', 1),
('Tatiane', 'Cardoso', '1980-06-07', 'tatiane.cardoso@senac.com', '51999990018', 2, '2010-05-11', 1),
('Ricardo', 'Moreira', '1977-01-16', 'ricardo.moreira@senac.com', '51999990019', 1, '2008-04-04', 1),
('Larissa', 'Campos', '1995-09-03', 'larissa.campos@senac.com', '51999990020', 3, '2022-02-20', 1);
