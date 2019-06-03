using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Netflix.Helpers;

namespace Netflix.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FilmController : BaseController
    {
        private IServiceFilm _serviceFilm;

        public FilmController(IServiceFilm serviceFilm)
        {
            _serviceFilm = serviceFilm;
        }

        [HttpGet]
        [ValidateModel]
        public IActionResult GetAll()
        {
            try
            {
                List<Film> Liste = _serviceFilm.FindAll();
                return BuildJSonResponse(200, "Succès", Liste);

            }
            catch (Exception e)
            {

                return BuildJSonResponse(500, "Erreur serveur", null, e.Message);
            }


        }
        [HttpGet("{id}")]
        [ValidateModel]
        public IActionResult GetById(int id)
        {
            try
            {
                Film film = _serviceFilm.FindById(id);
                return BuildJSonResponse(200, "Succès", film);

            }
            catch (Exception e)
            {

                return BuildJSonResponse(500, "Erreur serveur", null, e.Message);
            }


        }

        [HttpPost]
        [ValidateModel]
        public IActionResult Save([FromBody] Film film)
        {
            try
            {
                if (_serviceFilm.Add(film))

                    return BuildJSonResponse(201, "Film enregistré avec succès", film);
                else
                    return BuildJSonResponse(400, "Une erreur est survenue lors de l'enregistrement");
            }
            catch (Exception e)
            {
                return BuildJSonResponse(500, "Erreur serveur", null, e.Message);
            }
        }

        [HttpPut]
        [ValidateModel]
        public IActionResult Update([FromBody] Film film)
        {
            try
            {
                if (_serviceFilm.Update(film))

                    return BuildJSonResponse(201, "Film enregistré avec succès", film);
                else
                    return BuildJSonResponse(400, "Une erreur est survenue lors de l'enregistrement");
            }
            catch (Exception e)
            {
                return BuildJSonResponse(500, "Erreur serveur", null, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_serviceFilm.Delete(id))

                    return BuildJSonResponse(201, "Film supprimé avec succès");
                else
                    return BuildJSonResponse(400, "Une erreur est survenue lors de la suppression");
            }
            catch (Exception e)
            {
                return BuildJSonResponse(500, "Erreur serveur", null, e.Message);
            }
        }
    }
}