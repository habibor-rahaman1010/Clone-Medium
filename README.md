# Clone Medium (Use Clean Architecture And Repository Pattern, UnitOfWork Pattern, CQRS and Mediator Design Patterns)

This repository contains a Medium clone application built using Clean Architecture principles.

## Project Description

This project aims to replicate the core functionalities of Medium, a popular online publishing platform. It serves as a practical demonstration of implementing Clean Architecture in a real-world application.

## Architecture

The project follows the Clean Architecture, which promotes separation of concerns and testability. The architecture is structured into the following layers:

* **Domain:** Contains the core business logic and entities. It is independent of any external frameworks or libraries.
* **Application (Use Cases):** Contains the application-specific business rules and use cases. It orchestrates the flow of data between the Domain and Infrastructure layers.
* **Infrastructure:** Contains the implementation details of external dependencies, such as databases, network services, and UI frameworks.
* **Presentation (UI):** Contains the user interface and handles user interactions.