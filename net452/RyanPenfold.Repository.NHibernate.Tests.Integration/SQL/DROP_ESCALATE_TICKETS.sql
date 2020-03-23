
-- Drop procedure if it exists
IF EXISTS (SELECT [name] FROM sysobjects WHERE [name] = 'dbo.EscalateTickets' AND [type] = 'P')
BEGIN 
	DROP PROCEDURE [dbo.EscalateTickets]
END
GO
