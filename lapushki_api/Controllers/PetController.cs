using lapushki_api.Interfaces;
using lapushki_api.Requests;
using Microsoft.AspNetCore.Mvc;

namespace lapushki_api.Controllers
{
    public class PetController : Controller
    {
        private readonly IPetService _petService;
        public PetController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpGet]
        [Route("getAllPets")]
        public async Task<IActionResult> GetAllPets()
        {
            return await _petService.GetAllPets();
        }

        [HttpGet]
        [Route("getAllPetsByUser")]
        public async Task<IActionResult> GetAllPetsByUser(int user_id)
        {
            return await _petService.GetAllPetsByUser(user_id);
        }

        [HttpPost]
        [Route("addPet")]
        public async Task<IActionResult> AddPet([FromBody] PetModel petModel)
        {
            return await _petService.AddPet(petModel);
        }

        [HttpPut]
        [Route("updatePet")]
        public async Task<IActionResult> UpdatePet([FromBody] PetModel petModel)
        {
            return await _petService.UpdatePet(petModel);
        }

        [HttpDelete]
        [Route("deletePet")]
        public async Task<IActionResult> DeletePet(int pet_id)
        {
            return await _petService.DeletePet(pet_id);
        }
    }
}
