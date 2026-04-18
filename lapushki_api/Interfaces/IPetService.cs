using lapushki_api.Requests;
using Microsoft.AspNetCore.Mvc;

namespace lapushki_api.Interfaces
{
    public interface IPetService
    {
        Task<IActionResult> GetAllPets();
        Task<IActionResult> GetAllPetsByUser(int user_id);
        Task<IActionResult> AddPet(PetModel petModel);
        Task<IActionResult> UpdatePet(PetModel petModel);
        Task<IActionResult> DeletePet(int pet_id);

    }
}
