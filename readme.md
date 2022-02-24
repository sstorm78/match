# # Match Game

This project demonstrates an implementation of the required match game.

## Assumptions & Limitations


 - I've added all services and models into one project, to save time. Normally they all spit across multiple libraries for cross-reuse and different implementations.
 - The console app program.cs contains minimum business logic for the UI interaction. Not covered with tests to save time.
 - The unit tests written to cover the key functionality.

# Structure

**Match.App** is the main console app. All dependencies are injected using DI.

 - **MatchCheckers** is a collection of injectable match condition checkers. Allows fluent additions and injections.
 - **Models** is a set of models used in the application.
 - **Services** is a set of services we use to manage card packs and the game logic. **RandomService** is used for abstraction of randomize operations and predicted testibility.
 - **UiPresenters** contains ConsolePresenter which is an abstraction layer to console operations.

**Match.App.Tests** is a set of tests for the all testable classes.

Sergey