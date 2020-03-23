

CREATE TABLE [dbo].[escalation_rules](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Levels] [varchar](max) NULL,
	[High] [varchar](max) NULL,
	[Med] [varchar](max) NULL,
	[Low] [varchar](max) NULL
) ON [PRIMARY]

