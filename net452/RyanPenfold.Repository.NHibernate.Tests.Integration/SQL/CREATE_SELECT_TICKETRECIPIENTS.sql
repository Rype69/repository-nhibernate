
-- =============================================
-- Author:		Ryan Penfold
-- Create date: 21 November 2011
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[Select_TicketRecipients]

	@ticket_id AS BIGINT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    
    -- Insert statements for procedure here
	SELECT 
	    [dbo].[ticket_recipient].[ticket_recipient_id]
	   ,[dbo].[ticket_recipient].[ticket_id]
	   ,[dbo].[ticket_recipient].[name]
	   ,[dbo].[ticket_recipient].[email_address]
	   ,[dbo].[ticket_recipient].[type]
	FROM		
		[dbo].[ticket_recipient] 
	WHERE
		[dbo].[ticket_recipient].[ticket_id] = @ticket_id;
			
END
