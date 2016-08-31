CREATE TABLE [Patna].[ContactUploads]
(
	[Id] INT NOT NULL ,
    [CreatedOn]  DATE          NOT NULL,
    [CreatedBy]  INT          NOT NULL,
	[ResidentId]  INT          NOT NULL,  
	[ContactScan] image not null,
    CONSTRAINT [PK_ContactUploads] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_ContactUpload_Resident] FOREIGN KEY ([ResidentId]) REFERENCES [Patna].[Resident] ([ID])
)
