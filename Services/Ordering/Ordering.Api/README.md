### Ordering API

For Ordering API microservice, we use Clean Architecture using Domain Driven Design.
The application is divided into 4 layers:

- `Domain Layer`
- `Infrastructure Layer`
- `Application Layer`
- `Presentation / API Layer`

#### Domain Layer

This is where base entities that are being used are stored. For entities we use Domain Driven Design. That being said it will include
all value objects and aggregate roots as well as domain events.

#### Infrastructure Layer

This is where communcation with database (in our case SQL server) will take place. It follows the repository pattern
and we follow a code first approach using EF Core for ORM. In addition this is where events will be raised and dispatched.

#### Application Layer

We will CQRS and CQS pattern here in order to communicate with Infrastructure.

#### Presentation / API Layer

This is where our resources will be exposed. We will use minimal APIs to expose our data.
