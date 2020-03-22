 # Lab 3 - QuestService
 Now we've come to a more complex part of the microservice. We will simply do the same as we've done before with the PlayerService. Remove the in memory implementation of the frontend and replace it with the backend.

 Provided for you are: 
 - `QuestDbContext`: A databse object that is preconfigured for use
 - `QuestDbContext.GetAvailableQuestsForPlayerIdAsync(Guid playerId)`: A method that helps you build a suitable query for retrieving the available quests for a player.

 ## Excercise 1: Implement the endpoints

 Implement the following endpoints, connect them up with the frontend as you've done in Lab 2. The endpoints should be seperated over two controllers.

```CSharp
// Handle quest operations from the Admin section
[Route("[controller]")]
[ApiController]
public class QuestController : ControllerBase
{
    [HttpGet]
    public Task<ActionResult<IEnumerable<Quest>>> GetQuests() { }

    [HttpPost]
    public Task<IActionResult> CreateQuest(Quest quest) { }

    [HttpGet("{questId}")]
    public Task<ActionResult<Quest>> GetQuestById(Guid questId) { }

    [HttpDelete("{questId}")]
    public Task<IActionResult> DeleteQuest(Guid questId) { }

    [HttpPut("{questId}")]
    public Task<IActionResult> UpdateQuest(Guid questId, Quest quest) { }
}
```
```CSharp
// handles player specific operations
[Route("[controller]/{playerId}")]
[ApiController]
public class PlayerQuestController : ControllerBase
{
    [HttpGet]
    public Task<ActionResult<IEnumerable<Quest>>> GetActiveQuests(Guid playerId) { }

    [HttpPost]
    public Task<IActionResult> CompleteQuest(Guid playerId, [FromBody] Guid questId) { }
}
```

After you've completed the implementation, add the QuestService to your startup projects and run all 3 services at once and see the magic happen.

##  Notifying other services

Maybe you've already noticed it while making the implementation, but with this setup we have actually broken something!

The player is no longer getting gold and experience after completing a quest. Go ahead and try it out by yourself, complete a quest and go to the player information page, you will see that the player still has 0 EXP.

The reason this happens is that the PlayerService is now responsbile for holding player data and the quest service is responsible for quest data. We will fix this by adding a message broker in our solution that will look like this:

![Diagram showing infrastructure](https://www.plantuml.com/plantuml/img/SoWkIImgAStDuGeiJIqk2KujAijCJbNGjLC8IanAoYpn3RHI0CieEEVd5kIabgIcS745v2HMfXPXLHkKcfW2qb2QoWKJLD05ga3HWc6aWczgSN5YUYgcoMZw0ehoau5ASUftICrB0Ne60000)

## Excercise 2 - Implementing the infrastructure
First, we shall configure RabbitMQ so that it can support this infrastructure. Start by going to [localhost:7100](http://localhost:7100) and logging in using the default credentials `guest` / `guest`.

First we will add an exchange. An exchange functions as a distributor of messages. There are several types of exchanges that do different things, the exchange we will make is a `fanout` exchange, which distributes a copy of a message to all queue's connected to the exchange.

### Adding the exchange
Navigate to `Exchanges`. Find the `Add a new exchange` section and setup a new exchange with these parameters. When done, press `Add exchange`.
| Parameter Name | Value |
| --- | --- |
| Name | **TISA** |
| type | **fanout** |
| Durability | Durable |
| Auto delete | No |
| Internal | No |
| Arguments | *(leave empty)* |

### Adding the queues
Next up, we will add a queue for the `QuestService` and `PlayerService`.

Navigate to the tab `Queues` and find the `Add a new queue` section.

Setup two queues with the following parameters, when done configuring, hit the `Add queue` button. **Do this for both services.**

| Parameter Name | Value |
| --- | --- |
| Type | Classic |
| Name | QuestService / PlayerService |
| Durability | Durable |
| Auto Delete | No |
| Arguments | *(leave empty)* |


### Connecting them
For both services, perform this step.

1. When on the queue tab, click on the created Queue.
2. Find the `Bindings` sections
3. Find the `Add binding to this queue` section
4. Fill the `From exchange` field with the name of the exchange: `TISA`.
5. Hit the `Bind` button.
6. The table in the `Bindings` section should now show:

| From | Routing Key | Arguments | |
| --- | --- | --- | --- |
| TISA | | | Unbind |
With an arrow pointing downwards to `This queue`.

**Perform this step for the QuestService and the PlayerService queue**

## Excercise 4: Sending a message to the queue from the QuestService
With the infrastructure in place, we can start connecting our microservice to the message broker. For this step, I've preconfigured the connection to the RabbitMQ broker in the `Shared/Messaging` folder.

*&ast; If you find this kind of code interesting, take some of your spare time after this course to look through the code*

For now, we only have to make a few modifications in the `QuestService` to get things ready.
1. In your `startup.cs` navigate to the `ConfigureServices()` method.
2. Add the following code. This adds the `IMessagePublisher` interface that allows you to easily send messages.
```CSharp
// Note that "QuestServices" must match the configured queue name in Excercise 3
// This might require you to add a `using Shared.Messaging` directive.
services.AddMessagePublishing("QuestService");
```
3. Navigate to your implementation of CompleteQuest.
4. Inject `IMessagePublisher` in your implementation
5. Use the `Task IMessagePublisher.PublishMessageAsync<T>(string messageType, T value);` method to broadcast a message to the queue*. The `messageType` parameter should be a unique name for this message that the `PlayerService` can use to identify this message. The `T value` can be any C# object that you would like to send.
6. Run the code and complete a quest
7. Navigate to [localhost:7100](http://localhost:7100) and go to the PlayerService queue.
8. In the graph at the top of this page, it should now show that there is one (or more depending on how many quests you completed) message in the queue. Our code is working!

## Excercise 5: Reading the message in the PlayerService
Now that there is a message waiting for us in the queue, we should implement the reading part of the solution.

### Adding the message handler
First we will implement a class that handles this message
1. In the PlayerService, create a directory named `MessageHandlers`.
2. In the newly created directory, create a `QuestCompletedMessageHandler` class.
3. Extend the interface `IMessageHandler<T>` *&ast;Note that T should be the message type, thus it should be equivalent to the message you created in Excercise 4.*
4. Implement the `HandleMessageAsync` method by doing the necesary mutations on the player object.
5. Navigate to the `startup.cs` of the `PlayerService`
6. Add the following code snippet to the `ConfigureServices` method, again, you might need the `using Shared.Messaging;` directive.
```CSharp
services.AddMessagePublishing("QuestService", builder => {
    // This tells the application that when it receives a `QuestCompleted` message, it should handle it using the `QuestCompletedMessageHandler`.
    builder.WithHandler<QuestCompletedMessageHandler>("QuestCompleted");
});
```
&ast; This implementation of the message handler automatically ignores messages that do not have a message handler associated to it.

7. Run the application, complete quest, and check if the Player Information has updated with the correct information.

&ast; *Is your application showing weird exceptions? Did you accidentally queue a malformed message in your queue? Navigate to your queue in RabbitMQ, and choose the `Purge messages` option. This will delete all the messages from the queue.*


## Completed
With this, we have made the first step to creating microservices.

There is an *optional* lab 4 where we will complete the solution by making more microservices and more command and events.

It will also guide you to the `AchievementService`, which is pretty much a sandbox where you can do whatever you like 

