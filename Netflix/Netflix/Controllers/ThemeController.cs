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

    public class ThemeController : BaseController
    {
        private IServiceTheme _serviceTheme;

        public ThemeController(IServiceTheme serviceTheme)
        {
            _serviceTheme = serviceTheme;
        }

        [HttpGet]
        [ValidateModel]
        public IActionResult GetAll()
        {
            try
            {
                List<Theme> Liste = _serviceTheme.FindAll();
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
                Theme theme = _serviceTheme.FindById(id);
                return BuildJSonResponse(200, "Succès", theme);

            }
            catch (Exception e)
            {

                return BuildJSonResponse(500, "Erreur serveur", null, e.Message);
            }


        }

        [HttpPost]
        [ValidateModel]
        public IActionResult Save([FromBody] Theme theme)
        {
            try
            {
                if (_serviceTheme.Add(theme))

                    return BuildJSonResponse(201, "Thème enregistré avec succès", theme);
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
        public IActionResult Update([FromBody] Theme theme)
        {
            try
            {
                if (_serviceTheme.Update(theme))

                    return BuildJSonResponse(201, "Thème enregistré avec succès", theme);
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
                if (_serviceTheme.Delete(id))

                    return BuildJSonResponse(201, "Thème supprimé avec succès");
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