

-- Drop procedure if it exists
IF EXISTS (SELECT [name] FROM sysobjects WHERE [name] = 'Select_TicketRecipients' AND [type] = 'P')
BEGIN 
	DROP PROCEDURE [Select_TicketRecipients]
END

