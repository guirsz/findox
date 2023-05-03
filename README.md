# findox

###### Commands used to run PostgreSQL locally in a docker container
```
docker pull postgres
docker run -d -p 5432:5432 --name container-postgresdb -e POSTGRES_PASSWORD=containerPGdb postgres
docker exec -it container-postgresdb bash
psql -U postgres
CREATE DATABASE findoxdb;
```
###### Default available users
**Admin**
Email: brian@findox.com
Password: brian.bentow

**Manager**
Email: guilherme@findox.com
Password: guilherme.souza

**Regular User**
Email: kimberly.owen@missionresourcing.com
Password: kimberly.owen