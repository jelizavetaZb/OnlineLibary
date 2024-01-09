# OnlineLibary
First start =>
1)  "SiteFilesPath": "D:\\Repos\\SiteFiles\\", create directory for site file
2)  "DefaultConnection": "Server=localhost;Database=OnlineLibary;Trusted_Connection=True;MultipleActiveResultSets=true" configure db string
3)  "ConnectionStrings": {
  "MigrationsDbContext": "Server=(LocalDb)\\MSSQLLocalDB;Database=OnlineLibary;Trusted_Connection=True;MultipleActiveResultSets=true"  configure db string
4)  Add User Roles to db :
```
USE [OnlineLibary]
GO

INSERT INTO [dbo].[UserRoles] VALUES (0,'Allowed to view and edit users','User Manager','USERMANAGER' ,'5c3dcebe-5788-4322-9dc3-12f8e32e5934')
INSERT INTO [dbo].[UserRoles] VALUES (10,'Can read books','Reader','READER' ,'3e661345-4f50-4684-91eb-d80f0caba702')
INSERT INTO [dbo].[UserRoles] VALUES (20,'Can manage books','Editor','EDITOR' ,'976ef246-a185-4e6e-a31c-eeee18c491ff"')
GO
```
4) Update-Database
