using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pokedex.Models;
using Microsoft.AspNetCore.Mvc;
using pokedex.Services;

namespace pokedex.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            // Inject the service into the controller
            _pokemonService = pokemonService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pokemon>>> Get()
        {
            var pokemons = await _pokemonService.GetPokemonsAsync();
            return Ok(pokemons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pokemon>> GetPokemon(string id)
        {
            try
            {
                var pokemon = await _pokemonService.GetPokemonByIdAsync(id);
                return Ok(pokemon);
            }
            catch (Exception)
            {
                return NotFound("Pokemon not found");
            }
        }

        [HttpGet("search/{name}")]
        public async Task<ActionResult<Pokemon>> GetPokemonByName(string name)
        {
            try
            {
                var pokemon = await _pokemonService.GetPokemonByNameAsync(name);
                return Ok(pokemon);
            }
            catch (Exception)
            {
                return NotFound("Pokemon not found");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Pokemon>> AddPokemon([FromBody] Pokemon newPokemon)
        {
            var createdPokemon = await _pokemonService.AddPokemonAsync(newPokemon);
            return CreatedAtAction(nameof(GetPokemon), new { id = createdPokemon.Id }, createdPokemon);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Pokemon>> UpdatePokemon(string id, [FromBody] Pokemon updatedPokemon)
        {
            try
            {
                var updated = await _pokemonService.UpdatePokemonAsync(id, updatedPokemon);
                return Ok(updated);
            }
            catch (Exception)
            {
                return NotFound("Pokemon not found");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePokemon(string id)
        {
            try
            {
                var success = await _pokemonService.DeletePokemonAsync(id);
                if (success)
                    return NoContent();  // 204 No Content
                return NotFound("Pokemon not found");
            }
            catch (Exception)
            {
                return NotFound("Pokemon not found");
            }
        }
    }
}
