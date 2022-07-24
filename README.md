# Kirav.ru - Server App. 
This is a backend part of the personal website [kirav.ru](https://kirav.ru/). 

![screenshot of Kirav.ru](https://kirav.ru/images/articles/images_for_github/kiravru/20220723070133screen_kirav_ru-min.jpg)

Project uses the following technologies:
- [ ] Frontend: React + Redux, Thunk, TypeScript, Webpack
- [x] Backend: ASP.NET Core 3.1, Entity Framework
- [ ] Database: PostgreeSQL

- [x] - current repository

Also you can find Dockerfile in the project. Use it in your case, it saves you a lot of time.
This repository combines a number of other projects using docker-compose. List of other repositories:
1. [KiravRu_ClientApp](https://github.com/NikitaKirav/KiravRu_ClientApp).
2. [Messenger_Client](https://github.com/NikitaKirav/Messenger_Client).
3. [Messenger_WebAPI](https://github.com/NikitaKirav/Messenger_WebAPI). 

Here you can find the following important files:
1. docker-compose.yml, docker-compose.override.yml - are docker compose files with images, volumes, ports and other settings of the project.
2. nginx.conf - the main configuration file of NGINX server. This is settings of 'nginx' Docker image.
3. init-letsencrypt.sh - script for initialize SSL certificate for kirav.ru. This script runs one time in a process of deployment. (Full instruction how use it is here: [https://kirav.ru/notes/133](https://kirav.ru/notes/133)).
4. updateAll.sh - script for deployment automation. This script copies repositories from Github to the server, installs packages (npm, NuGet) and run 'docker-compose up'.  

## Docker Images of the project
***You can find all images in details in docker-compose.yml.***

1. backend - is this github repository. Technologies Used: ASP.NET Core 3.1 (WebApi). Image depends on PostgreeSQL (postgres_db).
2. postgres_db - PostgreeSQL.
3. client - frontend part. Repository is [here](https://github.com/NikitaKirav/KiravRu_ClientApp). Technologies Used: React, Redux, Thunk.
4. messanger_backend - server part of Messenger. Repository is [here](https://github.com/NikitaKirav/Messenger_WebAPI). Technologies Used: NodeJS, express, mongoose.
5. messanger_client - client part of Messenger. Repository is [here](https://github.com/NikitaKirav/Messenger_Client). This is a separate project of the main site. But it works on the same domen as the main site thanks to the settings of NGINX. 
Technologies Used: React, Redux, Saga, Websocket, Unit and integrated Tests (Jest).
6. messanger_db - Database for messenger (MongoDB). 
7. nginx - NGINX server for all project.
8. certbot - bot for automation updating SSL certificate.

