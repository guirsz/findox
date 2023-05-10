# findox

###### Docker compose
To create database, tables and functions, run the docker-compose.yml file using the command bellow in the same directory from that file.
```
docker-compose up
```

###### Default available users by default
When starting the application, the following users will be created:
**Admin**
Email: brian@findox.com
Password: brian.bentow

**Manager**
Email: guilherme@findox.com
Password: guilherme.souza

**Regular User**
Email: kimberly.owen@missionresourcing.com
Password: kimberly.owen

###### Postman collection 
Import and use postman collection file in root directory to facilitate your calls
- Findox API.postman_collection.json

###### Knowledge presented
- Authentication JWT 
- Authorization
- Password hashing accordingly to OWASP Top 10
- Dependency Injection
- Streaming upload
- Docker compose
- Aws S3
- Database modeling
- PostgreSQL
- Stored Procedures and Functions
- Mapper
- Dapper
- Tests using xUnit
