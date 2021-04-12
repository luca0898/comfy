# Comfy

This project is designed to test my level of code design for an API

## Let's run

1. Install the [Dotnet SDK](https://dotnet.microsoft.com/download)
2. You will need the docker installed in your environment, you can get help [here](https://docs.docker.com/get-docker/)
3. Set up the development environment using the [docker-compose](docker-compose.yml) file. Here is the [official documentation](https://docs.docker.com/compose/)
4. Setup Keycloak:
   1. Now you need create a new comfy client in the [Keycloak Administration Console - Clients](http://localhost:8080/auth/admin/master/console/#/realms/master/clients).  Don't forget to set the Access Type field to **'confidential'** to generate a secret in the _"Credentials"_ tab. Keep the client_id and secret you just created handy because you will need it later.
   2. Get the `x5c` key in the [Keycloak Certificates](http://localhost:8080/auth/realms/master/protocol/openid-connect/certs)
   3. Create a appsettings.json file in the `./backend/Comfy.API` folder and fill the `"authenticationSettings.KeycloakSettings"` with the **secret** and **client_id**
   4. Overwride the `RSAKeyValue.Modules` property of the [API Certificate file](./backend/Comfy.API/Cert/comfy.xml) with the 5° step certifate data

5. Setup the database:
   1. Install the [Entity Framework Core-CLI do .NET Core](https://docs.microsoft.com/pt-br/ef/core/cli/dotnet#installing-the-tools)
   2. Run the following command in the `./backend/Comfy.Repositories/Comfy.Repository.Db.SQL.Migrations` directory :
   ```
   $ dotnet ef database update
   ```
6. Run the API by typing the following command:
   ```
   $ dotnet run --project ./backend/Comfy.API/Comfy.API.csproj
   ```
7. Open a new tab in your browser and type http://localhost:5000/swagger/index.html and authorize using the admin:admin default user
8. Send a request using the POST /v1/schedule swagger item