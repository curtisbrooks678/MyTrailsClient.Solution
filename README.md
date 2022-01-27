# _My Trails Client Solution_

#### By: _*Curtis Brooks, Kim Brannian, Jeff Lai, Katie Pundt, and Liz Thomas*_

#### _A web application for users to find and journal about hiking trails._

## Technologies Used
* C#
* .NET5
* ASP.NET Core MVC
* Razor
* NuGet
* Entity Framework Core
* Pomelo
* MySql
* Identity
* Bootstrap
* HTML
* CSS


## Description
_A MVC web application where users can find trails from the MyTrails API and can create their own accounts to keep a journal of their own hiking and foraging adventures. You can find the MyTrails API here:_ 
https://github.com/kpundt93/MyTrails.Solution

## System Requirements
* Download and install [.NET5](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)
* Download and install [MySql](https://www.mysql.com/downloads/) and connect to your local server
* A code editor, such as [VS Code](https://code.visualstudio.com/)


## Setup/Installation Requirements
* To set up the API, Navigate to https://github.com/kpundt93/MyTrails.Solution and follow the README instructions there to get that up and running on your local server.
* After the MyTrails API is set up, Navigate to https://github.com/kpundt93/MyTrailsClient.Solution
* Click on the green "Code" button and copy the repository URL or click on the   
  copy button
* Open the terminal on your desktop
* Once in the terminal, use it to navigate to your desktop folder
* Once inside your desktop folder, use the command `git clone https://github.com/kpundt93/MyTrails.Solution`
* After cloning the project, navigate into it using the command `cd MyTrailsClient.Solution
* Create a .gitignore file in the root directory and add: 
  
  */obj/
  */bin/
  */appsettings.json

* Create an appsettings.json file in the root directory and add:
```
  {
  "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Port=3306;database=my_trails;uid=root;pwd=[your-mysql-password];"
    }
  }

```
* Run the command `git init`
* Run the command `cd MyTrailsClient` to navigate the `MyTrailsClient.Solutions\MyTrails` main project folder  
* Run the command `dotnet restore` to install project dependencies
* Run the command `dotnet ef database update`
* Run the command `dotnet run` to run the project in the browser

## Known Bugs
* _There might be bugs. Let us know!_

## License
_MIT License: https://opensource.org/licenses/MIT_

Copyright (c) _2021_ _Curtis Brooks, Kim Brannian, Jeff Lai, Katie Pundt, and Liz Thomas_