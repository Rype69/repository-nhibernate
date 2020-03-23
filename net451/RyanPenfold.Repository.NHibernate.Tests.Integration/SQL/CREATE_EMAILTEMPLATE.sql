

CREATE TABLE [dbo].[EmailTemplate](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[EmailName] [varchar](max) NULL,
	[EmailBody] [varchar](max) NULL
) ON [PRIMARY]
