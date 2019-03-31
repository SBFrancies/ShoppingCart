# ShoppingCart

## Description
A small API to allow users to access and update a shopping cart as well as a client library to call the API. The project has been built using .Net Core 2.2 with Entity Framework Core. It uses an SQL Server back end.

## Requirements to run

1) .Net Core 2.2
2) SQL Server

# ShoppingCartApi

## Projects

### ShoppingCartApi.Access

This project contains classes that deal with the basic CRUDL operations required by the Main API.

### ShoppingCartApi.Api

This project is the public facing Web API project.

### ShoppingCartApi.Common

This project contains helper methods and classes to be used by the other projects.

### ShoppingCartApi.Data

This project contains the Entity Framework class definitions as well as the migrations.

### ShoppingCartApi.IntegrationTests

This project contains the integration tests.

### ShoppingCartApi.UnitTests

This project contains the unit tests.

# ShoppingCartClient

## Projects

### ShoppingCartClient.Client

This project contains the API client.

### ShoppingCartClient.UnitTests

This project contains the unit tests.

## If I had more time / Future changes

1) Implement some form of authentication / CORS support.

2) Add StyleCop or some other static code analysis tool.

3) Populate the test data separately from the main migrations - currently the tests are very brittle.

