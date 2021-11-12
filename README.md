# RESTful API .Net5

[here](https://youtu.be/ZXdFisA_hOY?t=4104)

## Tech

- Docker [install here](https://docs.docker.com/get-docker/)
- Postman
- .NET 5
- MongoDB 

Install MongoDB inside the project in a terminal
``` CLI
dotnet add package MongoDB.Driver
```



## Run and setup our Docker image for MongoDB

### Mongo DB 

1. Install it on our server / pc
2. Run it in Docker, which I will be doing

### Getting the public Docker image for MongoDB

1. open terminal

2. Run the follow to create our MongoDB container and run it 
```bash
docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db mongo
```

3. Now we can see our Docker container running by running 
```bash
docker ps
```
