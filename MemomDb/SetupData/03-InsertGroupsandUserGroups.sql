
GO
  
  Insert into [dbo].[Groups] 
  (ID, Tenant, Name, [Description], Created, LastUpdated) 
  Select NEWID(), 'default', 'Admin', 'Administrators for Site', GETDATE(), GETDATE()

GO

  Insert into [dbo].[UserGroups] (GroupKey, UserAccountKey, Created, LastUpdated) 
  Select 1, 1, GETDATE(), GETDATE()

  GO

  Insert into [dbo].[UserGroups] (GroupKey, UserAccountKey, Created, LastUpdated) 
  Select 1, 2, GETDATE(), GETDATE()