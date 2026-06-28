CREATE TABLE [dbo].[TBItemTarefa] (
    [Titulo]        NVARCHAR (100)   NOT NULL,
    [EstaConcluido] BIT              NOT NULL,
    [TarefaId]      UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_TBItemTarefa] PRIMARY KEY CLUSTERED ([TarefaId] ASC, [Titulo] ASC),
    CONSTRAINT [FK_TBItemTarefa_TBTarefa] FOREIGN KEY ([TarefaId]) REFERENCES [dbo].[TBTarefa] ([Id]) ON DELETE CASCADE
);
GO

