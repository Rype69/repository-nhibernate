USE [RyanPenfold]
GO

INSERT INTO
	[CV].[TestTable]
(
	[CV].[TestTable].[Id]
   ,[CV].[TestTable].[BigIntColumn]
   ,[CV].[TestTable].[BinaryColumn]
   ,[CV].[TestTable].[BitColumn]
   ,[CV].[TestTable].[CharColumn]
   ,[CV].[TestTable].[DateColumn]
   ,[CV].[TestTable].[DateTimeColumn]
   ,[CV].[TestTable].[DateTime2Column]
   ,[CV].[TestTable].[DateTimeOffSetColumn]
   ,[CV].[TestTable].[DecimalColumn]
   ,[CV].[TestTable].[FloatColumn]
   ,[CV].[TestTable].[ImageColumn]
   ,[CV].[TestTable].[IntColumn]
   ,[CV].[TestTable].[MoneyColumn]
   ,[CV].[TestTable].[NCharColumn]
   ,[CV].[TestTable].[NTextColumn]
   ,[CV].[TestTable].[NumericColumn]
   ,[CV].[TestTable].[NVarCharColumn]
   ,[CV].[TestTable].[RealColumn]
   ,[CV].[TestTable].[SmallDateTimeColumn]
   ,[CV].[TestTable].[SmallIntColumn]
   ,[CV].[TestTable].[SmallMoneyColumn]
   ,[CV].[TestTable].[SqlVariantColumn]
   ,[CV].[TestTable].[TextColumn]
   ,[CV].[TestTable].[TimeColumn]
   ,[CV].[TestTable].[TinyIntColumn]
   ,[CV].[TestTable].[UniqueIdentifierColumn]
   ,[CV].[TestTable].[VarBinaryColumn]
   ,[CV].[TestTable].[VarCharColumn]
   ,[CV].[TestTable].[XmlColumn]
)
VALUES
(
	NEWID()
   ,444
   ,0x77
   ,0
   ,'R'
   ,'1979-09-28'
   ,'1979-09-28 15:35:00'
   ,'1979-09-28 15:35:00'
   ,'0001-01-01 00:00:00.0 +00:00'
   ,0.9
   ,1
   ,NULL
   ,1
   ,922337203685477.5807
   ,'1'
   ,'BLAH!'
   ,0.9
   ,'r'
   ,3.141592654
   ,'1979-09-28 15:35:00'
   ,1
   ,214748.3647
   ,'lorem'
   ,'ipsum'
   ,'00:00:00.0'
   ,1
   ,NEWID()
   ,0x77
   ,'R'
   ,'<blah></blah>'
);
