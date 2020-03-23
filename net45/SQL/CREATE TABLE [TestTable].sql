/****** Object:  Table [CV].[TestTable]    Script Date: 05/07/2016 19:44:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CV].[TestTable]') AND type in (N'U'))
DROP TABLE [CV].[TestTable]
GO

/****** Object:  Table [CV].[TestTable]    Script Date: 05/07/2016 19:44:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CV].[TestTable]') AND type in (N'U'))
BEGIN
CREATE TABLE [CV].[TestTable](
	[Id] [uniqueidentifier] NOT NULL,
	[BigIntColumn] [bigint] NULL,
	[BinaryColumn] [binary] NULL,
	[BitColumn] [bit] NULL,
	[CharColumn] [char] NULL,
	[DateColumn] [date] NULL,
	[DateTimeColumn] [datetime] NULL,
	[DateTime2Column] [datetime2] NULL,
	[DateTimeOffSetColumn] [datetimeoffset] NULL,
	[DecimalColumn] [decimal] NULL,
	[FloatColumn] [float] NULL,
	[ImageColumn] [image] NULL,
	[IntColumn] [int] NULL,
	[MoneyColumn] [money] NULL,
	[NCharColumn] [nchar] NULL,
	[NTextColumn] [ntext] NULL,
	[NumericColumn] [numeric] NULL,
	[NVarCharColumn] [nvarchar] NULL,
	[RealColumn] [real] NULL,
	[SmallDateTimeColumn] [smalldatetime] NULL,
	[SmallIntColumn] [smallint] NULL,
	[SmallMoneyColumn] [smallmoney] NULL,
	[SqlVariantColumn] [sql_variant] NULL,
	[TextColumn] [text] NULL,
	[TimeColumn] [time] NULL,
	[TimestampColumn] [timestamp] NULL,
	[TinyIntColumn] [tinyint] NULL,
	[UniqueIdentifierColumn] [uniqueidentifier] NULL,
	[VarBinaryColumn] [varbinary] NULL,
	[VarCharColumn] [varchar] NULL,
	[XmlColumn] [xml] NULL,
 CONSTRAINT [PK_TestTable] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO


