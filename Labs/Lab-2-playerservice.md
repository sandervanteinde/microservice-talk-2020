# Lab 2: The Player Service

In this lab we will focus on the player service. You might ask yourself: Why are we foucssing in one service. Because many companies are running a big monolith piece of software, you want to use the (Strangler Pattern)[https://docs.microsoft.com/en-us/azure/architecture/patterns/strangler] to slowly migrate the monolith to the microservice architecture.

Therefore, we shall start with the Player Service, and we will start simple: We will just implement a web API that the frontend can communicate to for the player data.
Our microservice will follow [REST guidelines](https://en.wikipedia.org/wiki/Representational_state_transfer).

During the labs we will work in many different microservices. If you are getting overwhelmed, adding some logging might assist you in your development! Just implement `ILogger<ClassName>` in any file, and log what you would like to know. You can navigate to (Seq)[http://localhost:7200] to view the logs from **all** applications.

## Excercise 1: The PlayerController
- Open the PlayerService part of the solution
- Navigate to the `PlayerController` class.
- In this class, implement two endpoints:
- You can choose how to save the player data. An EntityFramework Context class is available for you and already setup to connect to a database that is run in your Docker environment.
- When you run the application, you can navigate to [localhost:7400](http://localhost:7400). A browser is **not** opened automatically. On this page you can view and test if your API works.


```CSharp
public class PlayerController 
{
    // This should return a Player object by its player id, or NotFound when null
    [HttpGet("{playerId}")]
    public Task<ActionResult<Player>> GetPlayer(Guid playerId) { }

    [HttpPost]
    public Task<IActionResult> CreateNewPlayer([FromBody] string playerName) 
    { 
        // creation logic
        return Created($"/Player/{idOfNewPlayer}")
    }
}
```

The player model should look equivalent to the one in the frontend:
```CSharp
public class Player
{
    // This is the primary key if you use a database.
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Experience { get; set; }
    public int Level { get; set; }
    public int Gold { get; set; }
}
```
*note: For this lab we should not focus on things like validation of the model. In normal production-like environments this is ofcourse common practice!*

## Excercise 2: Connecting the WebApi to the player service
Now that the player service is up and running, we should connect the player service to the web api. For this, we should use the `TISA.Services` project, within there, we shall modify the `PlayerService` class.

Clear the entire implementation of `PlayerService`. Visual studio will now complain that we do not meet the requirements of the interface.
- Implement the interface
- Implement the service by making api calls to `https://localhost:7401/Player` and `https://localhost:7401/{playerId}` 

*Note: I have added [Flurl](https://flurl.dev/docs/fluent-http/) as a library to assist in easily making API calls. An example to make a call with Flurl is as follows:*

 ```CSharp
public async Task<Guid> CreatePlayerNameAsync(string playerName)
{
    var response = await "https://localhost:7401/Player".PostJsonAsync(playerName);
    if(response.StatusCode != System.Net.HttpStatusCode.Created)
    {
        throw new InvalidOperationException("Something went wrong trying to create the player");
    }

    var player = await $"https://localhost:7401{response.Headers.Location}".GetJsonAsync<Player>();
    return player.Id;
}
 ```

 ## Excercise 3
 Now that we have modified the PlayerService to hook up to our new microservice, we should test if it works.

 We will now run both projects at the same time, this can be done by right-clicking in the top of your solution explorer on `Solution 'TISA'` and clicking the context menu option `Set startup projects`.

 In the dialog that appeared, choose the option `Multiple startup projects` and pick `TISA` and `PlayerService`.

 Now we can run the application and see if we can still create a player. We can set breakpoints in the `PlayerService` project to validate that it is indeed being invoked.

 ## Reminder:

At this point of the lab, you might wonder: Why am I doing all of this just to get some simple player information to the client?

The implementation we have now already opens up a couple of new possibilities that we didn't have before:
- The player service is now standalone, other applications can now use the same player base
- The team working on the website no longer has to think about business logic involving the player, this is now handled in the team that is developing the PlayerService.

