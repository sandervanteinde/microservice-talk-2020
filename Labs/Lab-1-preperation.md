# Lab 1: Getting started

For the first lab we require the infrastructure for microservices.

This starter project has most of it already set up, but some essential infrastructure components are left for you to finish.

## Excercise 1: Getting the files:
1. On your computer, pull the files from the git repo:
`https://github.com/sandervanteinde/microservice-talk-2020.git`
2. Run `git checkout 1-starter`. This has all the code you need to start.


## Excercise 2: Ensuring peer dependencies are installed

Firstly, learn how to start the environment. The environment is configured in Docker so you don't have to setup the environment by yourself. In the root of this repository you will find a docker-compose.yml. Ensure the following prerequisites are met:
- Docker is installed on your machine and running (A Docker icon is available in your taskbar)
- Docker is set to use Linux containers (when you right-click) on the taskbar icon, a option `Switch to Windows containers` is available, indicating you are currently running Linux containers. If the option `Switch to Linux containers` is available, click it to make the switch
- You are sharing drives with Docker. To validate this, navigate to Docker settings by right-clicking the docker icon and pressing `Settings`. In the tab `Shared Drives`, one of your local drives is ticked.

When this is all properly setup, you can open a command prompt or powershell window in the root of this repository and run the command `docker-compose up -d`. This will launch the containers and start running your services. Give the services approximately 1 minute to spin up. To validate if all the containers are (still) running, run the command `docker-compose ps`. This should list 3 services, all having the state `Up`.

Finally, you should be able to see two websites being exposed on your local computer:
- RabbitMQ: [localhost:7100](http://localhost:7100)
- Seq: [localhost:7200](http://localhost:7200)