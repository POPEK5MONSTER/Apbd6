using Tutorial5.Models;
using Tutorial5.Models.DTOs;

namespace Tutorial5.Repositories;

public interface IAnimalRepository
{
    IEnumerable<Animal> GetAnimals(string? orderBY);
    void AddAnimal(AddAnimal animal);
    int ChangeAnimal(int idAnimal, AddAnimal animal);
    int DeleteAnimal(int idAnimal);
}