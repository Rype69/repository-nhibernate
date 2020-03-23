

CREATE TABLE [dbo].[ticket_recipient](
	[ticket_recipient_id] [bigint] IDENTITY(1,1) NOT NULL,
	[ticket_id] [bigint] NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[email_address] [nvarchar](max) NOT NULL,
	[type] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ticket_recipient] PRIMARY KEY CLUSTERED 
(
	[ticket_recipient_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY];
