using WebApi.Data.Models;
using WebApi.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/profil")]
    [ApiController]
    public class ProfilController : ControllerBase
    {
        private readonly IProfilRepository _ProfilRepo;

        public ProfilController(IProfilRepository ProfilRepo)
        {
            _ProfilRepo = ProfilRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfil()
        {
            try
            {
                var people = await _ProfilRepo.GetProfil();
                return Ok(people);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,new ResponseModel { StatusCode=500,Message="Terdapat Kesalahan !"});
            }
        }

        [HttpPost]
        // api/people (post)

        public async Task<IActionResult> CreateProfil([FromBody] Profil Profil)
        {
            if (Profil.StatusPerkawinan == "")
            {
                ModelState.AddModelError("StatusPerkawinan", "Status Perkawinan tidak boleh kosong !");
            }
            else if (Profil.StatusPerkawinan != "BelumKawin" && Profil.StatusPerkawinan != "Kawin")
            {
                ModelState.AddModelError("StatusPerkawinan", "Status Perkawinan hanya bisa diisi BelumKawin atau Kawin !");
            }
            // check the validation
            if (!ModelState.IsValid)
            {
                // validation failed
                //return BadRequest(new ResponseModel { StatusCode = 400, Message = "Please pass all the required field and valid data" });
                // 422 
                return UnprocessableEntity(ModelState);
            }
            try
            {
                await _ProfilRepo.AddProfil(Profil);
                return CreatedAtAction(nameof(CreateProfil), Profil);
            }
            catch (Exception ex)
            {
                // log exception here
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { StatusCode = 500, Message = "Terdapat Kesalahan !" });
            }

        }


        [HttpPut]
        public async Task<IActionResult> UpdateProfil(Profil Profil)
        {
            if (Profil.StatusPerkawinan == "")
            {
                ModelState.AddModelError("StatusPerkawinan", "Status Perkawinan tidak boleh kosong !");
            }
            else if (Profil.StatusPerkawinan != "BelumKawin" && Profil.StatusPerkawinan != "Kawin")
            {
                ModelState.AddModelError("StatusPerkawinan", "Status Perkawinan hanya bisa diisi BelumKawin atau Kawin !");
            }
            // check the validation
            if (!ModelState.IsValid)
            {
                // validation failed
                return UnprocessableEntity(ModelState);
            }
            try
            {
                var existingProfil = await _ProfilRepo.GetProfilById(Profil.Id);
                if (existingProfil == null)
                {
                    return NotFound(new ResponseModel { StatusCode = 404, Message = "Profil Tidak Ditemukan" });
                }
                await _ProfilRepo.UpdateProfil(Profil);
                return Ok(Profil);
            }
            catch (Exception ex)
            {
                // log exception here
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { StatusCode = 500, Message = "Terdapat Kesalahan !" });
            }

        }

        [HttpGet("{id}")]
        //api/people/{id}

        public async Task<IActionResult> GetProfil(int id)
        {
            try
            {
                var Profil = await _ProfilRepo.GetProfilById(id);
                if (Profil == null)
                {
                    return NotFound(new ResponseModel { StatusCode = 404, Message = "Profil Tidak Ditemukan" });
                }
                return Ok(Profil);
            }
            catch (Exception ex)
            {
                // log exception here
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { StatusCode = 500, Message = "Terdapat Kesalahan !" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfil(int id)
        {
            try
            {
                var Profil = await _ProfilRepo.GetProfilById(id);
                if (Profil == null)
                {
                    return NotFound(new ResponseModel { StatusCode = 404, Message = "Profil Tidak Ditemukan" });
                }
                await _ProfilRepo.DeleteProfil(id);
                return Ok(new ResponseModel { StatusCode=200,Message="Berhasil dihapus"});
            }
            catch (Exception ex)
            {
                // log exception here
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { StatusCode = 500, Message = "Terdapat Kesalahan !" });
            }
        }
    }
}
