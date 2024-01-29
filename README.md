# trucks-api
Trucks Api is a simple api that allows to save the list of trucks and manage their statuses.

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

## Running the aplication

To start the application use attached `docker-compose` files:

```bash
docker compose  -f docker-compose.yml -f docker-compose.override.yml up  -d
```

After it starts you can open your browser and go to `http://localhost:8080/swagger/index.html`

Alternatively, you can use OpenApi 3.0 specification to import API definition to API client like Postman. Specification is in `openApiSepc.json` file.
## Default data
By default API creates 15 example truck records, all of which have "Out Of Service" status. 


## Testing api

To run the tests included in the solution use the following command:

```
dotnet test .\TrucksManagement.sln
```