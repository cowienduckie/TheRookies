# BOOK LIBRARY - THE ROOKIES MID ASSIGNMENT

## Server

Requirements:

- .NET 6 SDK
- SQL Server

Change the connection string before running lines of code below

```shell
cd Server
dotnet build
cd BookLibrary.Data
dotnet ef database update 
cd ../BookLibrary.WebApi
dotnet run
```

## Client

Requirements:

- Node JS 18

```shell
cd Client
npm install
npm run dev
```
