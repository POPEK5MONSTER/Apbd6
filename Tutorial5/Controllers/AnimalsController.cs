using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Tutorial5.Models;
using Tutorial5.Models.DTOs;
using Tutorial5.Repositories;

namespace Tutorial5.Controllers;

[ApiController]
//[Route("api/[controller]")]
//[Route("api/animals")]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalRepository _animalRepository;
    
    public AnimalsController(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }
    
    [HttpGet]
    [Route("api/animals")]
    public IActionResult GetAnimals(string? orderBY)
    {
        var animals = _animalRepository.GetAnimals(orderBY);

        return Ok(animals);
    }

    [HttpPost]
    [Route("api/animals")]
    public IActionResult AddAnimal(AddAnimal animal)
    {
        _animalRepository.AddAnimal(animal);
        
        return Created("", null);
    }
    
    
    [HttpPut]
    [Route("api/animals/{idAnimal:int}")]
    public IActionResult ChangeAnimal(int idAnimal, AddAnimal animal)
    {
        var result = _animalRepository.ChangeAnimal(idAnimal ,animal);
        if (result == 1)
        {
            return Ok("Updated Animal");
        }
        else
        {
            return NotFound("Error");
        } 
        
    }
    [HttpDelete]
    [Route("api/animals/{idAnimal:int}")]
    public IActionResult DeleteAnimal(int idAnimal)
    {
         var result = _animalRepository.DeleteAnimal(idAnimal);

         if (result == 1)
         {
             return Ok("Delete Animal");
         }
         else
         {
             return NotFound("Error");
         }

    }
}