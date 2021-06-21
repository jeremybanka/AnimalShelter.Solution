### AnimalShelter.Solution

_By Jeremy Banka_

## Technologies Used

- 🎵 C# / .NET 5 Framework
- 🔥 Blazor WebAssembly Frontend
- 🛂 JWT Session Authentication
- 🎛️ ASP.NET Core Server
- 👇 Entity Framework Core
- 🍋 Pomelo MySQL interface
- 🧮 MySQL Database
- 🏴‍☠️ Swagger API Documentation Framework
- 💅 SCSS to CSS via Ritwick's Live Sass Compiler
- 🛠️ Tooling: Omnisharp
- 🅰️ Font: Palatino by Hermann Zapf (PBUH)

## Description

This is a project to create a simple web api and client.

- The api allows basic CRUD functionality for various pet-themed database records, as well as basic user auth.
- ASP.Net Identity was _not_ used in this production. Cryptography was implemented manually.
- You will not get owned. User passwords are salted 🎲 and hashed ♻️♻️♻️ for privacy.
- Which currently means, _don't forget your password_. It doesn't keep it; there's no recovery mechanism 😁.
- Single-page client is written in C# Blazor (NO JS 🤯). The C# compiles directly to WebAssembly.

## Setup/Installation Requirements

### Getting Started

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

  and instead of `!!!!!!!!!!!!!!!!!!!!!!!!!!!!!` put a (very) long string. This will be used to create JWT tokens.

- Compile and run the API server as you save changes: `$ dotnet watch run` in `WebApi/` (This command will also install necessary dependencies.) It ought to prompt Swagger to open in your browser. But if it doesn't, just go to `http://localhost:6144/swagger`.
- Once the server's running, do a `$ dotnet watch run` in `Blazor/`. Again, this will install the dependencies needed.
- The API server runs on :6144; the client, on :5000. The client's already pointing at the correct port.

### Authentication

The api routes in this application are not currently restricted to authorized users. This means you don't actually need an account to use the api, just to see certain pages in the client. If you want an account, there are two ways.

**UI Way** The UI is total crap, but it does work, for auth at least 😅.

- Go to `http://localhost:5000/register`
- Enter in your details, ensuring the passwords match. There are no restrictions set on password length or security!
- Submit the form, then log in using the creds you created.
- This will permit you to access the other client routes, which _don't_ do much of anything execpt withhold themselves from unauthenticated users.

**Cool Behind-the-Scenes Way**

- Go to `http://localhost:6144/swagger`
- Click into "POST /Users/register" and "Try it out"
- Execute with details you will remember, ensuring the passwords match.
- Click into "POST /Users/login" and "Try it out"
- Execute with the details from before. The response will contain your details (except password, which the server doesn't know), as well as a 'token', which is like a temporary password.

### Using API Endpoints

- Go to `http://localhost:6144/swagger`
- To see all the cats and dogs at the shelter, click into "GET /api", "Try it out", then "Execute"
- No animals will be harmed. Instead, you'll see all the animals that are here!
- Which is none, unless you made some already.
- To make a cat, for example, click into "POST /api/cats/", "Try it out", and "Execute", adding a value for "name" you think would befit a cat.
- Making a dog works exactly the same.
- To delete, view, or make changes to a particular pet, request all, as above, copy the "id" of the pet in question, and supply it to the http method of your choice in the "id" field.

## Known Bugs

- none identified

## License

Gnu Public License ^3.0

## Contact Information

hello at jeremybanka dot com
