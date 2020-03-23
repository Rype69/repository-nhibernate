-- =============================================
-- Author:		Ryan Penfold & Lukas Petersen
-- Create date: 14 November 2011
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[EscalateTickets]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    
	-- Declare variable to contain original data
	DECLARE @original_data TABLE (
		  ticket_id BIGINT
		, current_support_level TINYINT
		, LastEscalationTime DATETIME);

	-- Declare variable to contain updated data		
	DECLARE @updated_data TABLE (
		  ticket_id BIGINT
		, shortdesc NVARCHAR(255) NULL
		, fulldesc NVARCHAR(MAX) NULL
		, product NVARCHAR(50) NULL
		, trouble NVARCHAR(50) NULL
		, report NVARCHAR(50) NULL
		, severity TINYINT NULL
		, [priority] TINYINT NULL
		, current_status NVARCHAR(5) NULL
		, support_level TINYINT NULL
		, ticket_created_date DATETIME NULL
		, company NVARCHAR(50) NULL
		, caller_name NVARCHAR(255) NULL
		, caller_email NVARCHAR(255) NULL
		, last_escalation_time DATETIME NULL);

	-- Declare variable to contain the current date and time
	DECLARE @CurrentDateTime AS DATETIME;

	-- Set the current date time
	SET @CurrentDateTime = GETDATE();

	-- Populate the table with the current data
	INSERT INTO 
		@original_data 
	SELECT 
		 ticket_id
		,current_support_level
		,LastEscalationTime
	FROM
		[ticket];	

	--Increase current_support_level if outdated
	UPDATE 
		[ticket]
	SET 
		current_support_level = (current_support_level + (
		CASE
			WHEN (EscalationHML = 'H' and ((SELECT DATEDIFF(minute, LastEscalationTime , @CurrentDateTime)) >= (Select High from [escalation_rules] Where identifier = current_support_level)))
			  OR (EscalationHML = 'M' and ((SELECT DATEDIFF(minute, LastEscalationTime , @CurrentDateTime)) >= (Select Med from [escalation_rules] Where identifier = current_support_level)))
			  OR (EscalationHML = 'L' and ((SELECT DATEDIFF(minute, LastEscalationTime , @CurrentDateTime)) >= (Select Low from [escalation_rules] Where identifier = current_support_level)))
			THEN 1
			ELSE 0
		END));

	--Insert those records that have been updated into @updated_data
	INSERT INTO
		@updated_data
	SELECT 
		 [ticket].[ticket_id]
		,[ticket].[shortdesc]
		,[ticket].[fulldesc]
		,[ticket].[product]
		,[ticket].[trouble]
		,[ticket].[report]
		,[ticket].[EscalationSeverity]
		,[ticket].[EscalationPriority]
		,[ticket].[status]
		,[ticket].[current_support_level]
		,[ticket].[timesubmitted]
		,[ticket].[company]
		,[GetUserName]([ticket].[firstname], [ticket].[lastname])
		,[ticket].[email]
		,@CurrentDateTime
	FROM
		[ticket] 
		JOIN 
			@original_data O
		ON
			O.[ticket_id] = [ticket].[ticket_id]
	WHERE 
		O.current_support_level <> [ticket].current_support_level;

	--Update LastEscalationTime in the ticket table
	UPDATE 
		[ticket]
	SET 
		LastEscalationTime = @CurrentDateTime 
	WHERE 
		ticket_id IN 
			(SELECT 
				ticket_id
			FROM
				@updated_data);
	
	--Select the updated data
	SELECT 
		 [ticket_id] AS [TicketID]
		,[shortdesc] AS [ShortDesc]
		,[fulldesc] AS [FullDesc]
		,[product] AS [Product]
		,[trouble] AS [Trouble]
		,[report] AS [Report]
		,[severity] AS [Severity]
		,[priority] AS [Priority]
		,[current_status] AS [CurrentStatus]
		,[support_level] AS [SupportLevel]
		,[ticket_created_date] AS [TicketCreatedDate]
		,[company] AS [CompanyName]
		,[caller_name] AS [CallersName]
		,[caller_email] AS [Callersemail]
		,[last_escalation_time] AS [LastEscalationTime]
	FROM
		@updated_data;
	
END
