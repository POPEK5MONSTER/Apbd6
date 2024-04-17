using Microsoft.Data.SqlClient;
using Tutorial5.Models;
using Tutorial5.Models.DTOs;

namespace Tutorial5.Repositories;

public class AnimalRepository : IAnimalRepository
{
    private readonly IConfiguration _configuration;

    public AnimalRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IEnumerable<Animal> GetAnimals(string? orderBy)
    {
        // Otwieramy połączenie do bazy danych
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        // Definicja commanda
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        if (orderBy == null)
        {
            command.CommandText = "SELECT * FROM Animal ORDER BY Name ASC";
        }
        else
        {
            command.CommandText = $"SELECT * FROM Animal ORDER BY {orderBy} ASC";
        }

        // Wykonanie commanda
        var reader = command.ExecuteReader();

        var animals = new List<Animal>();

        int idAnimalOrdinal = reader.GetOrdinal("IdAnimal");
        int nameOrdinal = reader.GetOrdinal("Name");
        int dercriptionOrdinal = reader.GetOrdinal("Description");
        int categoryOrdinal = reader.GetOrdinal("Category");
        int areaOrdinal = reader.GetOrdinal("Area");

        while (reader.Read())
        {
            animals.Add(new Animal()
            {
                IdAnimal = reader.GetInt32(idAnimalOrdinal),
                Name = reader.GetString(nameOrdinal),
                Description = reader.GetString(dercriptionOrdinal),
                Category = reader.GetString(categoryOrdinal),
                Area = reader.GetString(areaOrdinal)
            });
        }

        return animals;
    }

    public void AddAnimal(AddAnimal animal)
    {
        
        // Otwieramy połączenie do bazy danych
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        // Definicja commanda
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText =
            "INSERT INTO Animal VALUES(@animalName, @animalDercription, @animalCategory, @animalArea)";
        command.Parameters.AddWithValue("@animalName", animal.Name);
        command.Parameters.AddWithValue("@animalDercription", animal.Description);
        command.Parameters.AddWithValue("@animalCategory", animal.Category);
        command.Parameters.AddWithValue("@animalArea", animal.Area);

        // Wykonanie commanda
        command.ExecuteNonQuery();
    }

    public int ChangeAnimal(int idAnimal, AddAnimal animal)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        // Definicja commanda
        
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        
        
        command.CommandText = "UPDATE Animal SET Name=@animalName, Description = @animalDercription, Category = @animalCategory, Area = @animalArea WHERE IdAnimal = @animalIdAnimal";
        
        command.Parameters.AddWithValue("@animalIdAnimal", idAnimal);
        command.Parameters.AddWithValue("@animalName", animal.Name);
        command.Parameters.AddWithValue("@animalDercription", animal.Description);
        command.Parameters.AddWithValue("@animalCategory", animal.Category);
        command.Parameters.AddWithValue("@animalArea", animal.Area);
        
        // Wykonanie commanda
        var result = command.ExecuteNonQuery();
        return result;
    }

    public int DeleteAnimal(int idAnimal)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        // Definicja commanda
        
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText =
            "DELETE FROM Animal WHERE IdAnimal = @idAnimal";
        command.Parameters.AddWithValue("@idAnimal", idAnimal);
        
        
        // Wykonanie commanda
        var result = command.ExecuteNonQuery();
        return result;
    }
}