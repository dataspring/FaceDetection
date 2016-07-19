--ALTER ROLE [db_owner] ADD MEMBER [memomuser];

EXECUTE sp_addrolemember @rolename = N'db_owner', @membername = N'meme';


GO


