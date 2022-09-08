using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVentas.Models;
using WSVentas.Models.Dao;
using WSVentas.Models.Dto;
using WSVentas.Services;

namespace WSVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VentaController : ControllerBase
    {

        private IVentaService _venta;

        public VentaController(IVentaService venta)
        {
            this._venta = venta;
        }
        [HttpPost("nuevo")]
        public IActionResult AddVenta(VentaDao dao)
        {
            Respuesta orsp = new Respuesta();
            try
            {
                _venta.Add(dao);
                orsp.Status = 201;
                orsp.Mensaje = "Venta registrada con éxito";
            }
            catch (Exception ex)
            {
                orsp.Status = 400;
                orsp.Mensaje = "No se pudo agregar la venta: " + ex.Message;
                return BadRequest(orsp);
            }

            return Ok(orsp);
        }
    }
}
