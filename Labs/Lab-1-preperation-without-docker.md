# Lab 1: Getting started

For the first lab we require the infrastructure for microservices.

This starter project has most of it already set up, but some essential infrastructure components are left for you to finish.

## Excercise 1: Getting the files:
1. On your computer, pull the files from the git repo:
`https://github.com/sandervanteinde/microservice-talk-2020.git`
2. Run `git checkout 1-starter-without-docker`. This has all the code you need to start.


## Excercise 2: Ensuring peer dependencies are installed

### Database
The solution runs on Microsoft Entity Framework, thus you should have a Database which is compatible with this.

In the starter are 4 database context files, these should all point to a different database. Ensure that the 

If you are running a Microsft Sql server, I recommend using that.

Find the following `DbContext` files in the solution, and update it's `OnConfiguring()` method to point to the right database.

- AchievementDbContext
- ItemDbContext
- QuestDbContext
- PlayerDbContext

### Message Broker
Our message broker in this solutiono is RabbitMQ.

RabbitMQ requires [erlang](https://www.erlang.org) to run. You should install this before running by downloading the Windows 64-bit Binary file from [their download page](https://www.erlang.org/downloads)

Next up, download the message broker on [the offical site of RabbitMQ](https://www.rabbitmq.com/install-windows.html#installer)

RabbitMQ can be installed with all its default settings.

### Enabling the management interface for RabbitMQ

Open the `RabbitMQ command prompt` as an **administrator** (this is available in your startup menu, just start typing its name) 

Run the following command:

`rabbitmq-plugins enable rabbitmq_management`.

You can validate if this works by visting http://localhost:15672

### Seq

For logging we are using Seq. This centralises our logging in a web interface.

You can download Seq from [their official website](https://datalust.co/download)

Use the default port (5341) in the configuration.

When the `Browse Seq` button appears, it is running correctly.
