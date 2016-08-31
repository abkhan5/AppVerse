CREATE TABLE [Patna].[ResidentHistory]
(
	[Id] INT NOT NULL ,
    [CreatedOn]  DATE          NOT NULL,
    [CreatedBy]  INT          NOT NULL,
	[ResidentId]  INT          NOT NULL,  
	PaymentAmount int not null,
	Comment nvarchar(max) ,
	CONSTRAINT [PK_ResidentHistory] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_ResidentHistory_Resident] FOREIGN KEY ([ResidentId]) REFERENCES [Patna].[Resident] ([ID])
)