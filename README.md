# OnlineLibary
First start =>
1)  "SiteFilesPath": "D:\\Repos\\SiteFiles\\", create directory for site file
2)  "DefaultConnection": "Server=localhost;Database=OnlineLibary;Trusted_Connection=True;MultipleActiveResultSets=true" configure db string
3)  Add User Roles to db :
   -------------------------
        //            Id = 1, Name = "GlobalAdmin",
        //            NormalizedName = "GLOBALADMIN",
        //            ConcurrencyStamp = "5c3dcebe-5788-4322-9dc3-12f8e32e5934",
        //            UserRoleType = 0,
        //            Description = "Allowed to view and edit users"
    -------------------------
        //            Id = 2, Name = "User",
        //            NormalizedName = "USER",
        //            ConcurrencyStamp = "3e661345-4f50-4684-91eb-d80f0caba702",
        //            UserRoleType = 10,
        //            Description = "View only"
    -------------------------
        //            Id = 3, Name = "Editor",
        //            NormalizedName = "EDITOR",
        //            ConcurrencyStamp = "976ef246-a185-4e6e-a31c-eeee18c491ff",
        //            UserRoleType = 20,
        //            Description = "Can manage books"
    -------------------------
4) Update-Database
