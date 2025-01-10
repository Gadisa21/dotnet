using MongoDB.Driver;
using pokedex.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pokedex.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IMongoCollection<Pokemon> _pokemons;

        public PokemonService(IConfiguration config)
        {
            var client = new MongoClient(config.GetSection("MongoDB:ConnectionString").Value);
            var database = client.GetDatabase(config.GetSection("MongoDB:DatabaseName").Value);
            _pokemons = database.GetCollection<Pokemon>(config.GetSection("MongoDB:CollectionName").Value);
        }

        public async Task<List<Pokemon>> GetPokemonsAsync()
        {
            return await _pokemons.Find(pokemon => true).ToListAsync();
        }

        public async Task<Pokemon> GetPokemonByIdAsync(string id)
        {
            return await _pokemons.Find(pokemon => pokemon.Id == id).FirstOrDefaultAsync() 
                   ?? throw new Exception("Pokemon not found");
        }

        public async Task<Pokemon> GetPokemonByNameAsync(string name)
{
    // Use a case-insensitive search for the Pokémon name
    return await _pokemons.Find(pokemon => pokemon.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).FirstOrDefaultAsync()
           ?? throw new Exception("Pokemon not found");
}


        public async Task<Pokemon> AddPokemonAsync(Pokemon newPokemon)
        {
            newPokemon.Id = Guid.NewGuid().ToString();
            await _pokemons.InsertOneAsync(newPokemon);
            return newPokemon;
        }

        public async Task<Pokemon> UpdatePokemonAsync(string id, Pokemon updatedPokemon)
        {
            var result = await _pokemons.ReplaceOneAsync(pokemon => pokemon.Id == id, updatedPokemon);
            if (result.MatchedCount == 0)
                throw new Exception("Pokemon not found");

            return updatedPokemon;
        }

        public async Task<bool> DeletePokemonAsync(string id)
        {
            var result = await _pokemons.DeleteOneAsync(pokemon => pokemon.Id == id);
            if (result.DeletedCount == 0)
                throw new Exception("Pokemon not found");

            return true;
        }
    }
}
