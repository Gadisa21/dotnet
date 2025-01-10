# Pokedex API

This project is a simple Pokedex API built with ASP.NET Core. It allows you to manage a collection of Pokemon, including adding, updating, retrieving, and deleting Pokemon records. The data is stored in a MongoDB database.

## Features

- **Get All Pokemon**: Retrieve a list of all Pokemon in the database.
- **Get Pokemon by ID**: Retrieve a specific Pokemon by its unique ID.
- **Get Pokemon by Name**: Retrieve a specific Pokemon by its name.
- **Add Pokemon**: Add a new Pokemon to the database.
- **Update Pokemon**: Update an existing Pokemon's details.
- **Delete Pokemon**: Delete a Pokemon from the database.

## Endpoints

### Get All Pokemon

- **URL**: `/pokemon`
- **Method**: `GET`
- **Response**: A list of all Pokemon.

### Get Pokemon by ID

- **URL**: `/pokemon/{id}`
- **Method**: `GET`
- **Response**: The Pokemon with the specified ID.

### Get Pokemon by Name

- **URL**: `/pokemon/search/{name}`
- **Method**: `GET`
- **Response**: The Pokemon with the specified name.

### Add Pokemon

- **URL**: `/pokemon`
- **Method**: `POST`
- **Request Body**: JSON object representing the new Pokemon.
- **Response**: The created Pokemon with its assigned ID.

### Update Pokemon

- **URL**: `/pokemon/{id}`
- **Method**: `PUT`
- **Request Body**: JSON object representing the updated Pokemon details.
- **Response**: The updated Pokemon.

### Delete Pokemon

- **URL**: `/pokemon/{id}`
- **Method**: `DELETE`
- **Response**: No content if the deletion is successful.

## Models

### Pokemon

The `Pokemon` model represents a Pokemon entity with the following properties:

- **Id**: `string` - The unique identifier for the Pokemon.
- **Name**: `string` - The name of the Pokemon.
- **Type**: `string` - The type of the Pokemon (e.g., Electric, Fire).
- **Ability**: `string` - The ability of the Pokemon.
- **Level**: `int` - The level of the Pokemon.

