CREATE TABLE [Patna].[ContactNumber] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [NumberType]  INT           NOT NULL,
    [PhoneNumber] INT           NOT NULL,
    [CreatedOn]   DATE          NOT NULL,
    [CreatedBy]   INT           NOT NULL,
    [ResidentId]  INT           NOT NULL,
    [Comment]     NVARCHAR (50) NULL,
    CONSTRAINT [PK_ContactNumber] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ContactNumer_Resident] FOREIGN KEY ([ResidentId]) REFERENCES [Patna].[Resident] ([ID])
);

