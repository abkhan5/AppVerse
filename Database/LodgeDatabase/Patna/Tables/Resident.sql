CREATE TABLE [Patna].[Resident] (
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]  NVARCHAR (50) NOT NULL,
    [LastName]   NVARCHAR (50) NOT NULL,
    [MiddleName] NVARCHAR (50) NOT NULL,
    [DOB]        DATE          NOT NULL,
    [CreatedOn]  DATE          NOT NULL,
    [CreatedBy]  INT           NULL,
    CONSTRAINT [PK_Resident] PRIMARY KEY CLUSTERED ([ID] ASC)
);

