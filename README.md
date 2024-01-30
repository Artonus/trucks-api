# trucks-api
Trucks Api is a simple API that allows to save the list of trucks and manage their statuses. This is not a full application, rather MVP. 

API allows to perform:
* CRUD operations 
* filter and order results
* change Truck statuses

Each truck has one of the following statuses:
* Out Of Service
* Loading
* To Job
* At Job
* Returning

The change of truck statuses can happen with the following conditions:

1. "Out Of Service" status can be set regardless of the current status of the Truck
2. each status can be set if the current status of the Truck is "Out of service"
3. the remaining statuses can only be changed in the following order: Loading -> To Job -> At Job -> Returning
4. when Truck has "Returning" status it can start "Loading" again.

## Technologies
The application is written in `.NET 7` as REST API and uses MSSQL as a database. 
Both are configured to run using `docker compose`.

## Running the aplication

First start with specyfing your own password for database. In order to do so, set `MSSQL_SA_PASSWORD` env variable in `docker-compose.yml` file.
Now it is set to example value `yourStrong(!)Password`. When changed, adjust the password in connection string in `src/TrucksApi/appsettings.json` file.

Alternatively you can run the application with default passsword, altough it is not recommended.

To start the application use attached `docker-compose` files:

```bash
docker compose  -f docker-compose.yml -f docker-compose.override.yml up  -d
```

After it starts you can open your browser and go to `http://localhost:8080/swagger/index.html`

Alternatively, you can use OpenApi 3.0 specification to import API definition to API client like Postman. Specification is in `openApiSepc.json` file.

## Stopping application
To stop running application use:

```
docker compose down
```
## Default data
By default API creates 15 example truck records, all of which have "Out Of Service" status. 


## Testing api

To run the tests included in the solution use the following command:

```
dotnet test .\TrucksManagement.sln
```

## Future app expansion
This application should be viewed as MVP. Because of this, the app would benefit from the following extensions in the future:

* Extending CI/CD outside of just building and testing application
* Integration testing and extended unit testing (currently unit tests cover the most important logic of the application)
* Adding logging and telemetry for better visibility
* Automate configuration using tools like secret management

And more :)