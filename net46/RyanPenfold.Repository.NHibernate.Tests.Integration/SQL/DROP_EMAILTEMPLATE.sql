

IF EXISTS (SELECT 1 FROM sysobjects WHERE xtype='u' AND name='EmailTemplate') 
BEGIN
	DROP TABLE [dbo].[EmailTemplate];
END
