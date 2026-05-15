-- =============================================
-- Criação do banco de dados GestaoPedidos
-- Executa apenas se o banco ainda não existir
-- =============================================

IF NOT EXISTS (
    SELECT 1
    FROM sys.databases
    WHERE name = 'GestaoPedidos'
)
BEGIN
    CREATE DATABASE GestaoPedidos;
END
GO

USE GestaoPedidos;
GO

-- =============================================
-- TABELA CLIENTE
-- =============================================

IF OBJECT_ID('dbo.CLIENTE', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.CLIENTE
    (
        Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Nome NVARCHAR(200) NOT NULL,
        Email NVARCHAR(255) NOT NULL,
        Documento NVARCHAR(50) NOT NULL,
        Ativo BIT NOT NULL CONSTRAINT DF_CLIENTE_Ativo DEFAULT(1),
        DataCriacao DATETIMEOFFSET NOT NULL,
        DataAtualizacao DATETIMEOFFSET NULL
    );
END
GO

-- =============================================
-- TABELA PRODUTO
-- =============================================

IF OBJECT_ID('dbo.PRODUTO', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.PRODUTO
    (
        Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Nome NVARCHAR(200) NOT NULL,
        Descricao NVARCHAR(1000) NULL,
        Preco DECIMAL(18,2) NOT NULL,
        EstoqueDisponivel INT NOT NULL,
        Ativo BIT NOT NULL CONSTRAINT DF_PRODUTO_Ativo DEFAULT(1),
        DataCriacao DATETIMEOFFSET NOT NULL,
        DataAtualizacao DATETIMEOFFSET NULL
    );
END
GO

-- =============================================
-- TABELA PEDIDO
-- =============================================

IF OBJECT_ID('dbo.PEDIDO', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.PEDIDO
    (
        Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        ClienteId INT NOT NULL,
        DataCriacao DATETIMEOFFSET NOT NULL,
        Status INT NOT NULL,
        ValorTotal DECIMAL(18,2) NOT NULL CONSTRAINT DF_PEDIDO_ValorTotal DEFAULT(0),

        CONSTRAINT FK_PEDIDO_CLIENTE
            FOREIGN KEY (ClienteId)
            REFERENCES dbo.CLIENTE(Id)
    );
END
GO

-- =============================================
-- TABELA PEDIDO_ITEM
-- =============================================

IF OBJECT_ID('dbo.PEDIDO_ITEM', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.PEDIDO_ITEM
    (
        Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        PedidoId INT NOT NULL,
        ProdutoId INT NOT NULL,
        Quantidade INT NOT NULL,
        PrecoUnitario DECIMAL(18,2) NOT NULL,
        ValorTotalItem DECIMAL(18,2) NOT NULL,

        CONSTRAINT FK_PEDIDO_ITEM_PEDIDO
            FOREIGN KEY (PedidoId)
            REFERENCES dbo.PEDIDO(Id),

        CONSTRAINT FK_PEDIDO_ITEM_PRODUTO
            FOREIGN KEY (ProdutoId)
            REFERENCES dbo.PRODUTO(Id)
    );
END
GO

-- =============================================
-- TABELA PEDIDO_HISTORICO
-- =============================================

IF OBJECT_ID('dbo.PEDIDO_HISTORICO', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.PEDIDO_HISTORICO
    (
        Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        PedidoId INT NOT NULL,
        StatusAnterior INT NULL,
        NovoStatus INT NOT NULL,
        DataHoraAlteracao DATETIMEOFFSET NOT NULL,
        Motivo NVARCHAR(1000) NULL,

        CONSTRAINT FK_PEDIDO_HISTORICO_PEDIDO
            FOREIGN KEY (PedidoId)
            REFERENCES dbo.PEDIDO(Id)
    );
END
GO

-- =============================================
-- ÍNDICES
-- =============================================

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_PEDIDO_ClienteId'
)
BEGIN
    CREATE INDEX IX_PEDIDO_ClienteId
        ON dbo.PEDIDO(ClienteId);
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_PEDIDO_ITEM_PedidoId'
)
BEGIN
    CREATE INDEX IX_PEDIDO_ITEM_PedidoId
        ON dbo.PEDIDO_ITEM(PedidoId);
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_PEDIDO_ITEM_ProdutoId'
)
BEGIN
    CREATE INDEX IX_PEDIDO_ITEM_ProdutoId
        ON dbo.PEDIDO_ITEM(ProdutoId);
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_PEDIDO_HISTORICO_PedidoId'
)
BEGIN
    CREATE INDEX IX_PEDIDO_HISTORICO_PedidoId
        ON dbo.PEDIDO_HISTORICO(PedidoId);
END
GO