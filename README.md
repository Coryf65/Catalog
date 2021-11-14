# RESTful API .Net5

- A RESTful API, using MongoDB
- C# .NET 5 API

- built around the concept of items in a RPG
- Docs built using Swagger

![swaggerDocsDisplay](https://user-images.githubusercontent.com/20805058/141695609-180b5d79-06b7-43d5-84f8-5d63570e7c2f.png)

![swaggerDocsDisplay2](https://user-images.githubusercontent.com/20805058/141695615-70607fee-5633-4ce0-bbee-1a81c2b095de.png)

![healthCheck](https://user-images.githubusercontent.com/20805058/141695619-c097bc84-9ff0-4232-9437-5e255e1a8362.png)

## Tech

- VSCode, the IDE I am using [get it here](https://code.visualstudio.com/)
- Docker, for Running MongoDB inside [get it here](https://docs.docker.com/get-docker/)
- Postman, Testing APIs [About](https://www.postman.com/home)
- .NET 5, Framework [Docs](https://docs.microsoft.com/en-us/dotnet/core/whats-new/dotnet-5)
- C#, Language [Docs](https://docs.microsoft.com/en-us/dotnet/csharp/)
- MongoDB, NoSQL Database [Docs](https://docs.mongodb.com/manual/)
- Swagger, Docs for REST APIs [Docs](https://swagger.io/tools/swagger-ui/)
- API Health status built in using `AspNetCore.HealthChecks.MongoDb` [Docs](https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks)

Install MongoDB inside the project in a terminal
```bash
dotnet add package MongoDB.Driver
```

## Getting Started

### Starting Project

1. Clone, Get all the files from this project

<details><summary><b>Show instructions</b></summary>

1. Run in the terminal of your choice:

    ```bash
    git clone https://github.com/Coryf65/Catalog.git
    ```

> Easy way: run inside VSCode Terminal open the termial using `control + ~`

2. 

</details>

### Setting up Mongo DB 

1. Install it on a server / pc
2. Run it in Docker, which I will be doing here

### Setup MongoDB for Development, in Docker (no auth)

1. open terminal, I am using the terminal built into VSCode

> Note: We will need Docker installed on our pc here. See setup [here](https://docs.docker.com/get-docker/)

2. Creating the MongoDB Docker Container, No auth for Development use
```bash
docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db mongo
```

3. Now we can see our Docker container running by running 
```bash
docker ps
```

4. You can stop that container by name, we named it `mongo`

```bash
docker stop mongo
```

5. to see the items in our Database (MongoDB) we are going to install an extension for VScode called `MongoDB`

- it appears on the left of vscode as a leaf image
- goto /items /Documents and select an entry and we can see our data here

### Setup MongoDB for production, in Docker (using authentication)

- stop continer if running
`docker stop mongo`

- finding volume storage
`docker volume ls`

- removing volume
`docker volume rm mongodbdata`

- creating the new docker image of mongodb with Auth, using enviroment variables, username and password for this db
`docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db -e MONGO_INITDB_ROOT_USERNAME=anyusername -e MONGO_INITDB_ROOT_PASSWORD=somepasswordyouchose mongo`

- add the new username into appsettings.json

- adding the secrets we are passing into Docker/Mongodb container

- we are using terminal, setup use of secrets for our project
`dotnet user-secrets init`

- this will add an id into our .csproj file

- creating a secret, matching the format of our app settings format. in appsettings.json
`dotnet user-secrets set MongoDbSettings:Password somepasswordyouchose`
