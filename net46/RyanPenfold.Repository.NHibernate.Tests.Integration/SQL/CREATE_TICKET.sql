
CREATE TABLE [dbo].[ticket](
	[ticket_id] [bigint] IDENTITY(1,1) NOT NULL,
	[firstname] [nvarchar](50) NULL,
	[lastname] [nvarchar](50) NULL,
	[company] [nvarchar](50) NULL,
	[address1] [nvarchar](255) NULL,
	[address2] [nvarchar](255) NULL,
	[town] [nvarchar](255) NULL,
	[district] [nvarchar](255) NULL,
	[country] [nvarchar](50) NULL,
	[postcode] [nvarchar](50) NULL,
	[email] [nvarchar](50) NULL,
	[phone] [nvarchar](50) NULL,
	[fax] [nvarchar](50) NULL,
	[alternate] [nvarchar](255) NULL,
	[alternate_name] [nvarchar](255) NULL,
	[alternate_email] [nvarchar](255) NULL,
	[alternate_phone] [nvarchar](255) NULL,
	[product] [nvarchar](50) NULL,
	[trouble] [nvarchar](50) NULL,
	[report] [nvarchar](50) NULL,
	[shortdesc] [nvarchar](255) NULL,
	[fulldesc] [nvarchar](max) NULL,
	[status] [nvarchar](5) NULL,
	[contract] [nvarchar](5) NULL,
	[timesubmitted] [datetime] NULL,
	[level] [nvarchar](5) NULL,
	[viewedby] [nvarchar](50) NULL,
	[timeviewed] [datetime] NULL,
	[user_id] [int] NULL,
	[timeclosed] [datetime] NULL,
	[userdetails] [nvarchar](50) NULL,
	[pin] [int] NULL,
	[emergency] [nvarchar](5) NULL,
	[ticket_file] [nvarchar](255) NULL,
	[ticket_file2] [nvarchar](255) NULL,
	[ticket_file3] [nvarchar](255) NULL,
	[oemuser] [int] NULL,
	[timelastaction] [datetime] NULL,
	[serialNo] [nvarchar](100) NULL,
	[qi] [nvarchar](100) NULL,
	[email1_sent] [bit] NOT NULL,
	[email2_sent] [bit] NOT NULL,
	[observeremail] [nvarchar](max) NULL,
	[observerstate] [nvarchar](max) NULL,
	[CRnum] [nvarchar](max) NULL,
	[CRurl] [nvarchar](max) NULL,
	[QInum] [nvarchar](max) NULL,
	[QIurl] [nvarchar](max) NULL,
	[INTnum] [nvarchar](max) NULL,
	[INTurl] [nvarchar](max) NULL,
	[SPTnum] [nvarchar](max) NULL,
	[SPTurl] [nvarchar](max) NULL,
	[EXTnum] [nvarchar](max) NULL,
	[EXTurl] [nvarchar](max) NULL,
	[severity] [nvarchar](max) NULL,
	[customemotion] [nvarchar](max) NULL,
	[cust_coop] [nvarchar](max) NULL,
	[current_support_level] [nvarchar](max) NULL,
	[RaisedByOEMReseller] [nvarchar](max) NULL,
	[OEM_reseller_userID] [nvarchar](max) NULL,
	[EscalationHML] [nvarchar](max) NULL,
	[LastEscalationTime] [datetime] NULL,
	[SendToJira] [nvarchar](max) NULL,
	[EscalationSeverity] [tinyint] NULL,
	[EscalationPriority] [tinyint] NULL
) ON [PRIMARY]

