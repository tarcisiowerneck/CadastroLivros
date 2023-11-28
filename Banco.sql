
-- Criar banco de dados Books
CREATE DATABASE Books;
GO

-- Usar o banco de dados Books
USE Books;
GO

-- Tabela Livro
CREATE TABLE Livro (
    Cod INTEGER PRIMARY KEY IDENTITY(1,1),
    Titulo VARCHAR(40),
    Editora VARCHAR(40),
    Edicao INTEGER,
    AnoPublicacao VARCHAR(4),
    Preco DECIMAL(15,2)
);
GO

-- Tabela Autor
CREATE TABLE Autor (
    CodAu INTEGER PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(40)
);
GO

-- Tabela Livro_Autor
CREATE TABLE Livro_Autor (
    Livro_Cod INTEGER,
    Autor_CodAu INTEGER,
    PRIMARY KEY (Livro_Cod, Autor_CodAu),
    FOREIGN KEY (Livro_Cod) REFERENCES Livro(Cod),
    FOREIGN KEY (Autor_CodAu) REFERENCES Autor(CodAu)
);
GO

-- Tabela Assunto
CREATE TABLE Assunto (
    CodAs INTEGER PRIMARY KEY IDENTITY(1,1),
    Descricao VARCHAR(20)
);
GO

-- Tabela Livro_Assunto
CREATE TABLE Livro_Assunto (
    Livro_Cod INTEGER,
    Assunto_CodAs INTEGER,
    PRIMARY KEY (Livro_Cod, Assunto_CodAs),
    FOREIGN KEY (Livro_Cod) REFERENCES Livro(Cod),
    FOREIGN KEY (Assunto_CodAs) REFERENCES Assunto(CodAs)
);
GO

-- Criar a view de relat�rio de livros
CREATE VIEW RelatorioLivros AS
SELECT
    L.Cod AS LivroCod,
    L.Titulo AS TituloLivro,
    L.Editora,
    L.Edicao,
    L.AnoPublicacao,
    L.Preco,
    Autores = STUFF((
        SELECT '; ' + A.Nome
        FROM Livro_Autor LA
        JOIN Autor A ON LA.Autor_CodAu = A.CodAu
        WHERE LA.Livro_Cod = L.Cod
        FOR XML PATH(''), TYPE
    ).value('.', 'VARCHAR(MAX)'), 1, 2, ''),
    Assuntos = STUFF((
        SELECT '; ' + ASU.Descricao
        FROM Livro_Assunto LASU
        JOIN Assunto ASU ON LASU.Assunto_CodAs = ASU.CodAs
        WHERE LASU.Livro_Cod = L.Cod
        FOR XML PATH(''), TYPE
    ).value('.', 'VARCHAR(MAX)'), 1, 2, '')
FROM
    Livro L;
GO