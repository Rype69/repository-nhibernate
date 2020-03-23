

IF EXISTS(SELECT * FROM sys.views WHERE name = 'TicketSummary')
BEGIN
	DROP VIEW [dbo].[TicketSummary];
END

