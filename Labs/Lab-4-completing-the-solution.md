 # Lab 4 - Completing the solution

 Now with the knowledge of the labs before, we can finish the microservice solution by implementing the ItemService.

You should have learned all you need to perform this lab by yourself, therefore I will just give you the steps you should follow:

## Excercise 1: The ItemService

1. Create the endpoints in the `ItemService` that the `IItemService` in the frontend should connect to.
2. Connect the `ItemService.cs` class in the frontend to the newly created endpoints.
3. Add the `ItemService` to your startup projects
4. Run the application to validate everything is still working as intended
5. You will notice that buying and selling items is now no longer adding/removing money from the user. Create events with the appropriate information and handle them in the appropriate service. 

&ast; *don't forget to follow the steps in Lab 3 - Excercise 2 for creating the queue and hooking it up to the exchange*

## Excersie 2: The achievement service

All of you should probably be familiar with the achievements that you can get in games. These vary from doing hard to reach goals, to logging in for the first time. In this excersise it is up to you to create achievements.

1. Create the endpoints in the `AchievementService` that the `IAchievementService `in the frontend should connect to.
2. Connect the `AchievementService.cs` class in the frontend to the newly created endpoints.
3. Hook the `AchievementsService` up to `RabbitMQ`.
4. Make handlers for whatever achievement you would like to make, this is entirely up to you what you will implement! Some things you could do is: Completing a quest, Buying an item, Selling an item, reaching level 2, etc. 

*Challenge yourself even more by adding a database that keeps track of statistics like: Amount of gold spend in the shop, and granting achievements based on that.*


