

IF EXISTS (SELECT 1 FROM sysobjects WHERE xtype='u' AND name='ticket_recipient') 
BEGIN
	DROP TABLE [dbo].[ticket_recipient];
END

