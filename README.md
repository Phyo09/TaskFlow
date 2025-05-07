## 🔧 Project Setup

- Install .NET SDK 8.x from https://dotnet.microsoft.com/en-us/download  
- Install SQLite: https://www.sqlite.org/download.html (optional for GUI)  
- Clone the GitHub repo and navigate to the folder  
- Run `dotnet restore` to install dependencies  

## 🚀 Run the App

- Use `dotnet run` to launch the app  
- Navigate to `http://localhost:XXXX` (port shown in terminal)  
- You will be redirected to login before accessing task list  

## 🔐 Authentication (ASP.NET Identity)

- Register and log in via `/Identity/Account/Register`  
- Login required to access `/Task/*` pages  
- Tasks are filtered by the current logged-in user only  

## 🧱 Features Implemented

- ✅ Full CRUD for tasks (Create, Read, Update, Delete)  
- ✅ Bootstrap 5 styling with responsive layout  
- ✅ Filtering by complete/incomplete status  
- ✅ Sorting by due date or title  
- ✅ Identity-based authentication and task isolation  

## 💡 Next Improvements (Optional Ideas)

- 🔄 Mark tasks complete via AJAX (toggle without full page reload)  
- 🗓️ Add task calendar view  
- 🎨 Improve UI with Bootstrap cards, icons, or dark mode  
- ☁️ Deploy to Render.com or Azure App Service  


# TaskFlow

**TaskFlow** is an ASP.NET Core MVC web application for managing tasks. It uses Entity Framework Core with SQL Server LocalDB for data persistence and is ideal for learning CRUD operations with .NET 8.

---

## 🚀 Getting Started

### ✅ Prerequisites

- [.NET SDK 8.0+](https://dotnet.microsoft.com/en-us/download)
- Visual Studio Code
- GitHub + GitHub Desktop or Git CLI
- EF Core CLI tools (install using):
  ```bash
  dotnet tool install --global dotnet-ef


* Download .NET SDK
* Add PATH to Environment Variables (Press Win + R, type sysdm.cpl, Advanced tab -> Encironment Varaibles, under System Varaibles, Path -> Add C:\Program Files\dotnet\)
* Make sure there is no other dotnet path
* Make sure there is only one runtime (Control Panel > Programs > Programs and Features)

Run below code
* dotnet --version
* where dotnet

## STEP 1: Setup Instructions
1. Clone the Repo
  ```bash
  git clone https://github.com/yourusername/TaskFlow.git
  cd TaskFlow
2. Create the MVC Project (create templates of .NET Core MVC app)
  ```bash
  dotnet new mvc

3. Run the Application
  ```bash
  dotnet run

## STEP 2: Add Entity Framework Core
4. Add the Task Model
5. Add the EF Core NuGet Packages (Add Entity Framework and a database)
  ```bash
#dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Tools

6. Create a DbContext
Create a new folder Data, and inside it a file TaskFlowContext.cs:

7. Register the Database Context in program.cs
  ```bash
using Microsoft.EntityFrameworkCore;
using TaskFlow.Data;
builder.Services.AddDbContext<TaskFlowContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
#builder.Services.AddDbContext<TaskFlowContext>(options =>
#    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

8. Add a Connection String in appsettings.json
"ConnectionStrings": {
    "DefaultConnection": "Data Source=TaskFlow.db"
  #"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TaskFlowDb;Trusted_Connection=True;"
}

 9. Set Up EF Core Migrations and Create the Database
 1) Add EF Core CLI Tools (if not already) 
  ```bash
  dotnet tool install --global dotnet-ef
 Then confirm it's installed: -> dotnet ef --version
2) Create Your First Migration
  ```bash
  rm -r Migrations
  dotnet ef migrations add InitialCreate
3) Apply the Migration to Create the Database
  ```bash
  dotnet ef database update

## STEP 3: Add Login, Register, Logout Functionality
10. Add Identity Support
  ```bash
  dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 8.0.4
  dotnet add package Microsoft.AspNetCore.Identity.UI --version 8.0.4

11. Update Data/TaskFlowContect.cs, Program.cs

12. Enable Identity UI
  ```bash
  dotnet aspnet-codegenerator identity -dc TaskFlow.Data.TaskFlowContext --files "Account.Login;Account.Register"
If you get a missing tool error, install:

  ```bash
  dotnet tool install -g dotnet-aspnet-codegenerator
  dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design --version 8.0.4


# Project Structure
TaskFlow/
├── Controllers/
├── Models/
│   └── TaskItem.cs
├── Views/
├── Data/
│   └── TaskFlowContext.cs
├── appsettings.json
└── Program.cs
