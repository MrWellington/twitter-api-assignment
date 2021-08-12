# twitter-api-assignment

## Overview

This repository is for a programming assignment with the goal of connecting to the [Twitter Streaming API](https://developer.twitter.com/en/docs/twitter-api/tweets/sampled-stream/introduction) and processing some basic statistics on the Tweets received. The code is written with C# targeting .NET 5. In addition to the goals outlined in the assignment, I had an additional design goal to utilize [Clean Architecture principles](https://www.freecodecamp.org/news/a-quick-introduction-to-clean-architecture-990c014448d2/) as outlined in this image:

![CA image](https://cdn-media-1.freecodecamp.org/images/oVVbTLR5gXHgP8Ehlz1qzRm5LLjX9kv2Zri6)

The solution is composed of the following projects, listed below along with a brief descriptions outlining the intended organization:

* src
  * API
    * **Vanacorps.TwitterClient.API**: ASP.NET Core API project acting as the application host and exposing an API controller to retrieve the tweet report.
  * Core
    * **Vanacorps.TwitterClient.Domain**: Collection of important business entities written as POCOs. Per clean architecture guidance all dependencies point inward towards this project.
    * **Vanacorps.TwitterClient.Application**: Contains all business logic for the application, largely utilizing a basic [CQRS](https://martinfowler.com/bliki/CQRS.html) pattern implementation. Also contains contracts to invert dependencies from outer layers such as the infrastructure and API.
  * Infrastructure
    * **Vanacorps.TwitterClient.Persistence**: Encapsulates concerns related to data persistence. Utilizes EF Core, currently configured to use an in-memory database.
    * **Vanacorps.TwitterClient.HttpClient**: Infrastructure project to encapsulate the HTTP client responsible for receiving tweets and placing them on a messaging queue. Utilizes MassTransit library as an abstraction around message sending, currently using an in-memory bus. Intended to be host-able separately from the app for scaling purposes.
    * **Vanacorps.TwitterClient.TweetProcessor**: Infrastructure project to encapsulate the processing required to receive tweets from a messaging queue, process them, and send them to the data store. Utilizes MassTransit library as an abstraction around message consuming, currently using an in-memory bus. Intended to be host-able separately from the app for scaling purposes.
* test
  * **Vanacorps.TwitterClient.Application.UnitTests**: xUnit test project covering business logic in the application project.

## Running locally

### Prerequisites

The only requirements to run this application are the .NET 5 runtime/sdk and a Twitter API bearer token that is authorized to access the streaming API.

### 

Before running the application, the bearer token must be configured as the `BEARER_TOKEN` environment variable - this can be done in Vanacorps.TwitterClient.API/Properties/launchSettings.json. Replace the text `NOT_SET` with the value of the token.

Running the application in Visual Studio or VS Code should launch an external browser window and issue at GET request to the single TweetReport endpoint. This will bring back an empty tweet report generated before the streaming client is initialized. Refreshing the page will get the latest data.
