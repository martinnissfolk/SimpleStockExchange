# SimpleStockExchange
A demo in ASP.NET MVC Core showing a very basic stock exchange. The user can buy or sell shares of one single stock called Stock A.
Two test user accounts have been created before hand. 

    - username: bill / password: bill123
    - username: joe / password: joe123

## Features

* storing information with SQLite
* login/register different users

## Requierements
ASP.NET Core 2.2
Visual Studio 2017 or later

## Setup project
The SQLite database needs to be created with Migrations

Open up the Package Manager Console (PMC)

Enter these commands

    cd src\SimpleStockExchange.Domain
    dotnet ef database update