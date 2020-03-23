

IF EXISTS (SELECT 1 FROM sysobjects WHERE xtype='u' AND name='ticket') 
BEGIN
	DROP TABLE [dbo].[ticket];
END

