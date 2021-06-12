### AnimalShelter.Solution

_By Jeremy Banka_

## Technologies Used

- 🎵 C# / .NET 5 Framework
- 🔥 Blazor WebAssembly Frontend
- 🛂 JWT Auth
- 🎛️ ASP.NET Core Server
- 👇 Entity Framework Core
- 🍋 Pomelo MySQL interface
- 🧮 MySQL Database
- 🏴‍☠️ Swagger API Documentation Framework
- 💅 SCSS to CSS via Ritwick's Live Sass Compiler
- 🛠️ Tooling: Omnisharp
- 🅰️ Font: Palatino by Hermann Zapf (PBUH)

## Description

This is an exercise in implementing user authentication and a simple authorization protocol using Microsoft AspNetCore.

## Setup/Installation Requirements

- Get the source code: `$ git clone https://github.com/jeremybanka/AnimalShelter.Solution`
- Set up necessary database schema
  - Install Entity Framework CLI: `$ dotnet tool install --global dotnet-ef`
  - Build your database: in `WebApi/`, run `dotnet ef database update`
- Add `appsettings.json` in `WebApi/` and paste in the following text:

  ```json
  {
    "AppSettings": {
      "Secret": "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!"
    },
    "Logging": {
      "LogLevel": {
        "Default": "Information",
        "Microsoft": "Warning",
        "Microsoft.Hosting.Lifetime": "Information"
      }
    },
    "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Port=3306;database=animal_shelter;uid=root;pwd=************;"
    }
  }
  ```

  except, instead of `************` put your personal password for MySQL.

  and instead of `!!!!!!!!!!!!!!!!!!!!!!!!!!!!!` put a long string. This will be used to create JWT tokens.

- Compile and run the API server as you save changes: `$ dotnet watch run` in `WebApi/` (This command will also install necessary dependencies.) It will also prompt Swagger to open in your browser.
- Once the server's running, do a `$ dotnet watch run` in `Blazor/`. Again, this will install the dependencies needed.
- The API server runs on :6144; the client, on :5000. The client's already pointing at the correct port.

## Known Bugs

- none identified

## License

Gnu Public License ^3.0

## Contact Information

hello at jeremybanka dot com
