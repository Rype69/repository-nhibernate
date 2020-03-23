

IF EXISTS (SELECT 1 FROM sysobjects WHERE xtype='u' AND name='escalation_rules') 
BEGIN
	DROP TABLE [dbo].[escalation_rules];
END
