### Using EF Core 2

We are targeting an existing database that we wish to scaffold from. If the "Data" project was created as a .NET Standard Class Library then and additional executable project will need to be added to the project. .NET Standard projects cannot be executed directly. Therefore, the Web App must be create in the solution and set as the startup project for the scaffolding described below will work. Refer to the master solution readme.md to create the Web App if not already created.

Install the package into the Data project via Package Manager Console:

`PM> Install-Package Microsoft.EntityFrameworkCore.SqlServer`

This will bring in all necessary dependent packages. Some dependent packages may be out of date. To bring them up to date run:

`PM> Update-Package`

By default this command will update **ALL** project packages.

Then scaffold an available database with the following command:

`PM> Scaffold-DbContext -Provider Microsoft.EntityFrameworkCore.SqlServer -Connection "Data Source=R2D2;Initial Catalog=Northwind;Integrated Security=False;User ID=sa;Password=lexie07;" -OutputDir Domain -Context NorthwindContext`

If this command results in an error regarding .NET Standard then besure to create a separate executable application and make it the startup up project.

#### To enable EF Core Code First Migration support install these tools

`PM> Install-Package Microsoft.EntityFrameworkCore.Tools`

Then perform a code first migration using the command:

`PM> Add-Migration initial`

Then run the database command to apply the changes:

`PM> Update-Database -Verbose`

If these commands result in a errors then be sure to install the following package:
`PM> Install-Package Microsoft.EntityFrameworkCore.Design`

If errors continue then change then set the data project as the startup project.

#### Moving Configuration Info Out of DbContext

In the file `NorthwindContext.cs` replace the `OnConfiguring()` method with a constructor that takes a `DbContextOptions` parameter that lets the config info be passed in upon construction. See the Web App Startup.cs for usage.

#### Add EF Logging

In the data project add the `Microsoft.Extensions.Logging.Console` nuget package.

Inside the DbContext class create a new LoggerFactory and supporting code for EF Core 2 sql logging. See source for details.