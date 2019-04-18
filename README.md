# FriendsApp
ASP.NET Core Project using Angular and Entity Framework Core

## Commands to execute the project

### .NET Core Project
- dotnet watch run

### Angular Project
- npm install (for the first time, when download the project)
- ng serve


-----------------------------------------------------


## Entity Framework Commands

### Create Migrations
- dotnet ef migrations add InitialCreate

### Create and update database
- dotnet ef database update

Can use the "DB Browser for SQLite" software to access the database.


-----------------------------------------------------


### Photo storage
For photo storage using Cloudinary, needs to create an free account on Cloudinary and set the configuration with your account data on "appsettings.json" file. On "CloudinarySettings", inform your:
- CloudName
- ApiKey
- ApiSecret