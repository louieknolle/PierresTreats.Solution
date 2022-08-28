# Pierre's Sweet and Savory Treats

#### By _**Louie Knolle**_

#### _This is a web-based application that allows user to view treats and associated flavors from Pierre's bakery and gives functionality to authenticated users._

## Technologies Used

- _C#_
- _.NET 5_
- _ASP.NET Core MVC_
- _MySQL_
- _Entity Framework Core_
- _MS Identity Authentication_
- _HTML_
- _CSS_
- _Bootstrap CSS_

## Description

Users can view treats and their associated flavors currently available at Pierre's bakery.  Authenticated users can create, delete and update both items. 


## Setup/Installation Requirements
* _Open the terminal on your computer_
* _If they do not currently exist in your system, install [C#](https://docs.microsoft.com/en-us/dotnet/csharp/) and [.NET](https://docs.microsoft.com/en-us/dotnet/). You can follow the instructions found in this [lesson](https://www.learnhowtoprogram.com/c-and-net-part-time-c-and-react-track/getting-started-with-c/installing-and-configuring-mysql)_
* _Install Entity Framework Core to your system with this command in your terminal `dotnet tool install --global dotnet-ef --version 5.0.1`_
* _Clone this repository onto your computer by running this command in your terminal `https://github.com/louieknolle/PierresTreats.Solution`_
* _In your terminal, navigate to the root directory with the command `$ cd PierresTreats.Solution`_
* _In your terminal, run these two commands to install the Entity Framework Core Packages_
  * _`dotnet add package Microsoft.EntityFrameworkCore -v 5.0.0`_
  * _`dotnet add package Pomelo.EntityFrameworkCore.MySql -v 5.0.0-alpha.2`_
* _In your terminal, issue command `$ cd Sweetshop` to enter the project folder and create a file within to store your connection string for connecting the project to the database with the command `touch appsettings.json`_
* _In your text editor add the following code to the newly created appsettings.json file. *Note that uid and pwd may vary depending on your local MySql configurations. Database name can be whatever you choose, I used 'Factory' for this project._

```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;database=[DATABASE-NAME-HERE];uid=root;pwd=[YOUR-PASSWORD-HERE];"
  }
}
```
* _Issue command `$dotnet build` in the project folder to launch the developer environment and install required dependencies_
* _And then use the command `dotnet ef migrations add Initial` to create the first migration to the databse and `dotnet ef database update` to apply the migration and create the database on mySQL_

* _Next run the program with `$dotnet run` and a server will be launched in your default browser_
* _Register for a new account and login to be able to use app_

## Known Bugs

_none_

## License

_MIT_

**Copyright (c) _August 2022_ _Louie Knolle_**
**Please contact me for any contribution or questions: [email](mailto:knollelw@gmail.com)**