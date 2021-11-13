# RESTful API .Net5

- A RESTful API, using MongoDB
- C# .NET 5 API

- built around the concept of items in a RPG
- Docs built using Swagger

## Tech

- VSCode, the IDE I am using [get it here](https://code.visualstudio.com/)
- Docker, for Running MongoDB inside [get it here](https://docs.docker.com/get-docker/)
- Postman, Testing APIs [About](https://www.postman.com/home)
- .NET 5, Framework [Docs](https://docs.microsoft.com/en-us/dotnet/core/whats-new/dotnet-5)
- C#, Language [Docs](https://docs.microsoft.com/en-us/dotnet/csharp/)
- MongoDB, NoSQL Database [Docs](https://docs.mongodb.com/manual/)
- Swagger, Docs for REST APIs [Docs](https://swagger.io/tools/swagger-ui/)

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
2. Run it in Docker, which I will be doing

### Getting the public Docker image for MongoDB

1. open terminal, I am using the terminal built into VSCode

2. Creating the MongoDB Docker Container 
```bash
docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db mongo
```

3. Now we can see our Docker container running by running 
```bash
docker ps
```

4. to see the items in our Database (MongoDB) we are going to install an extension for VScode called`MongoDB`