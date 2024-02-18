# Grocery store application

This project is created using React for fronted, ASP .NET 6 Core Web API for Backend and SQL server database.

## Getting started with project

I have added admin users and sample product items to the system using seed method. So, after running migrations please run command to execute the Seed.cs file.

For running migrations, open nuget package manager console and run the following commands.

1. Add Migration 

```bash
Add-Migration "Initial Migration"
```
2. Update Database

```bash
Update-Database
```
Now open the terminal go to the project directory where Seed.cs file is present. Then run the following command.

```bash
dotnet run seeddata
```

Server is listening at https://localhost:7272 and frontend is running at http://localhost:3000.

### `Controllers`

I have created five controllers Admin, Cart, Products, Orders and user authentication respectively. 

### `JWT tokens`

I have used JWT tokens for role based authentication and authorization to make our APIs secure.

### `React Redux toolkit`

In frontend, I used Redux to create centralized data by creating a 'UserAuth' slice where I stored the Jwt token, role, email of logged in person received from server and managed to state efficiently accross the components. The value of centralized data is passed to the App.js component so that components whose parent is App.js can utilize this data.

### `React Icons`

Used to fetch various types of icons and use it in our frontend application.

### `React hot toast`

Used to display toast after performing any operations.

### `React Router`

In frontend, I used React router to create multiple routes for my web app.

### `npm start`

Runs the app in the development mode.\
Open [http://localhost:3000](http://localhost:3000) to view it in your browser.

The page will reload when you make changes.\
You may also see any lint errors in the console.

