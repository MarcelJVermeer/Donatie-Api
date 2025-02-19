using System.ComponentModel.DataAnnotations;
using Donatie.API.Repositories;
using Donatie.Applicatie.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Donatie.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonatieController(DonatieRepository donatieRepository) : ControllerBase
    {
        private readonly DonatieRepository donatieRepository = donatieRepository;

        [HttpGet("GetAllDonations")]
        public ActionResult<IEnumerable<DonatieItem>> GetDonaties()
        {
            return Ok(donatieRepository.GeefAlleDonaties());
        }

        [HttpGet("SearchAfzender/{afzender}")]
        public ActionResult<IEnumerable<DonatieItem>> GetAfzender(string afzender)
        {
            return Ok(donatieRepository.ZoekDonaties(afzender));
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<GeefDonatie>> GetDonatieById(int id)
        {
            var donatie = donatieRepository.GeefDonatie((int)id);
            if (donatie is null) return NotFound($"The id provide couldnt find a Donation. Id: {id}");
            return Ok(donatie);
        }

        [HttpPost("CreateDonation")]
        public ActionResult<IEnumerable<MaakDonatie>> PostCreateDonatie(MaakDonatie maak)
        {
            if (maak is null) return BadRequest("You need to fill in all the infaomtie to create a donation");
            var maakDonatie = donatieRepository.CreateDonatie(maak);
            return Ok(maakDonatie);
        }

        [HttpPut("UpdateDonatie/{id}")]
        public ActionResult<IEnumerable<GeefDonatie>> PutUpdateDonatie(int id, UpdateDonatie updateDonatie)
        {
            try
            {
                donatieRepository.UpdateDonatie(id, updateDonatie);
                return Ok(donatieRepository.GeefDonatie(id));
            }
            catch (ValidationException ex)
            {

                return BadRequest(ex.Message);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }


        }
        [HttpDelete("DeleteDonatie/{id}")]
        public ActionResult DeleteDonatie(int id)
        {
            try
            {
                donatieRepository.DeleteDonatie(id);
                return Ok($"Dit id is verwijderen: {id}");
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }

    }
}
