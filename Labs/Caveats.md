# Caveats

The labs in this repository are meant to teach you about the challenges that Microservices bring when choosing it as an architecture. Therefore some concerns that you should have with a production environment are not taken into account.

This includes, but is not limited to:
- Security: if the microservices are running on the public web, there should be some form of backend to backend authorization.
- Database: This application runs in one database engine with multiple databases in it, creating a single point of failure. A true microservice solution should have its own database engine so that if one engine fails, the other services can still run.
- Shared: For the ease of the training, there is a Shared project with shared functionality. To some extend, this is okay. However do note that the sharing of models between microservice is an anti-pattern!
- Database models in API: In the solution, I expose the database models directily in the Controller, this was done for easy of implementation. In a production environment you would always seperate this in a data layer and you should always model  your database model to a seperate web api model!
