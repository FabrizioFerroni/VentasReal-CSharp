using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVentas.Models.Dto;
using WSVentas.Services;

namespace WSVentas.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {


        private IUserService _userService;

        public UsuarioController(IUserService userService)
        {
            _userService = userService;
        }


            [HttpPost("login")]
        public IActionResult Autentificar([FromBody] AuthDao dao)
        {
            Respuesta orsp = new Respuesta();
            var userdto = _userService.Auth(dao);

            if(userdto == null)
            {
                orsp.Status = 400;
                orsp.Mensaje = "Usuario o contraseña incorrecto";
                return BadRequest(orsp);
            }
           
            orsp.Status = 200;
            orsp.Mensaje = "Usario autenticado con éxito";
            orsp.Data = userdto;
            return Ok(orsp);
            
        }
    }
}
