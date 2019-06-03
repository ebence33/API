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

    public class UserController : BaseController
    {
        private IServiceUser _serviceUser;

        public UserController(IServiceUser serviceUser)
        {
            _serviceUser = serviceUser;
        }

        [HttpGet]
        [ValidateModel]
        public IActionResult GetAll()
        {
            try
            {
                List<User> Liste = _serviceUser.FindAll();
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
                User user = _serviceUser.FindById(id);
                return BuildJSonResponse(200, "Succès", user);

            }
            catch (Exception e)
            {

                return BuildJSonResponse(500, "Erreur serveur", null, e.Message);
            }


        }

        [HttpPost]
        [ValidateModel]
        public IActionResult Save([FromBody] User user)
        {
            try
            {
                if (_serviceUser.Add(user))

                    return BuildJSonResponse(201, "Utilisateur enregistré avec succès", user);
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
        public IActionResult Update([FromBody] User user)
        {
            try
            {
                if (_serviceUser.Update(user))

                    return BuildJSonResponse(201, "Utilisateur enregistré avec succès", user);
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
                if (_serviceUser.Delete(id))

                    return BuildJSonResponse(201, "Utilisateur supprimé avec succès");
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