

IF EXISTS (SELECT 1 FROM sysobjects WHERE xtype='u' AND name='ContainsGuid') 
BEGIN
	DROP TABLE [dbo].[ContainsGuid];
END
