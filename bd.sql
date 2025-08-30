USE [gerenciamento_escolar]
GO

/****** Object:  Table [dbo].[Administradores]    Script Date: 29/08/2025 22:10:11 ******/
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

/****** Object:  Table [dbo].[Aluno]    Script Date: 29/08/2025 22:10:27 ******/
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

/****** Object:  Table [dbo].[Categoria]    Script Date: 29/08/2025 22:10:35 ******/
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

/****** Object:  Table [dbo].[Curso]    Script Date: 29/08/2025 22:10:42 ******/
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

/****** Object:  Table [dbo].[Formacao]    Script Date: 29/08/2025 22:10:50 ******/
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

/****** Object:  Table [dbo].[Professor]    Script Date: 29/08/2025 22:10:59 ******/
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

