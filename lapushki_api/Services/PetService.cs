using lapushki_api.Data;
using lapushki_api.Interfaces;
using lapushki_api.Models;
using lapushki_api.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lapushki_api.Services
{
    public class PetService : IPetService
    {
        private readonly ContextDb _ContextDb;

        public PetService(ContextDb ContextDb)
        {
            _ContextDb = ContextDb;
        }
        public async Task<IActionResult> GetAllPets()
        {
            var list = await _ContextDb.Pets.ToListAsync();
            return new OkObjectResult(new { status = true, list });
        }
        public async Task<IActionResult> GetAllPetsByUser(int user_id)
        {
            var list = await _ContextDb.Pets.Where(x=>x.user_id == user_id).ToListAsync();
            return new OkObjectResult(new { status = true, list });
        }
        public async Task<IActionResult> AddPet(PetModel petModel)
        {
            var newPet = new Pet()
            {
                name = petModel.name,
                breed = petModel.breed,
                species = petModel.species,
                description = petModel.description,
                gender = petModel.gender,
                date_of_birth = petModel.date_of_birth,
                user_id = petModel.user_id
            };

            await _ContextDb.AddAsync(newPet);
            await _ContextDb.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true,
                message = "Питомец зарегистрирован"
            });
        }
        public async Task<IActionResult> UpdatePet(PetModel petModel)
        {
            var pet = await _ContextDb.Pets.FirstOrDefaultAsync(x => x.id_pet == petModel.id_pet);
            if (pet == null)
                return new OkObjectResult(new { status = false, message = "Питомец не найден" });

            pet.name = petModel.name;
            pet.breed = petModel.breed;
            pet.species = petModel.species;
            pet.description = petModel.description;
            pet.gender = petModel.gender;
            pet.date_of_birth = petModel.date_of_birth;
            pet.user_id = petModel.user_id;
            pet.image = petModel.image;

            await _ContextDb.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true,
                message = "Данные о питомце обновлены"
            });
        }
        public async Task<IActionResult> DeletePet(int pet_id)
        {
            var appointments = _ContextDb.Appointments.Where(x=>x.pet_id == pet_id).ToList();

            foreach (var appointment in appointments)
            {
                _ContextDb.Remove(appointment);
            }

            var pet = await _ContextDb.Pets.FirstOrDefaultAsync(x => x.id_pet == pet_id);

            if (pet == null)
                return new OkObjectResult(new { status = false, message = "Питомец не найден" });

            _ContextDb.Remove(pet);
            await _ContextDb.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true,
                message = "Удаление успешно"
            });
        }


        
    }
}
