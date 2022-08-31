﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVentas.Models;
using WSVentas.Models.Dto;
namespace WSVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        
        [HttpGet]
        public IActionResult Get()
        {

                Respuesta orsp = new Respuesta();
            try
            {
                using (VentasRealContext db = new VentasRealContext())
                {
                    var lst = db.Cliente.ToList();
                    orsp.Status = 200;
                    orsp.Mensaje = "Ok";
                    orsp.Data = lst;
                }
            }
            catch (Exception e)
            {
                orsp.Mensaje = "Hubo un error: " + e.Message;
            }
                    return Ok(orsp);
        }

        [HttpPost("nuevo")]
        public IActionResult AddClient(ClienteDto dto)
        {

            Respuesta orsp = new Respuesta();
            try
            {
                using (VentasRealContext db = new VentasRealContext())
                {
                    Cliente oClient = new Cliente();
                    oClient.Nombre = dto.Nombre;
                    db.Cliente.Add(oClient);
                    db.SaveChanges();
                    orsp.Mensaje = "Ok";
                    orsp.Status = 201;
                    orsp.Data = oClient;

                }

            } catch(Exception e)
            {
                orsp.Mensaje = "Hubo un error: " + e.Message;
            }

            return Ok(orsp);
        }


        [HttpPut("{Id}/editar")]
        public IActionResult EditClient(long Id, ClienteDto dto)
        {

            Respuesta orsp = new Respuesta();
            try
            {
                using (VentasRealContext db = new VentasRealContext())
                {
                    Cliente oClient = db.Cliente.Find(Id);
                    oClient.Nombre = dto.Nombre;
                    db.Entry(oClient).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    orsp.Mensaje = "Ok";
                    orsp.Status = 201;
                    orsp.Data = oClient;

                }

            }
            catch (Exception e)
            {
                orsp.Mensaje = "Hubo un error: " + e.Message;
            }

            return Ok(orsp);
        }


        [HttpDelete("{Id}/borrar")]
        public IActionResult DeleteClient(long Id)
        {

            Respuesta orsp = new Respuesta();
            try
            {
                using (VentasRealContext db = new VentasRealContext())
                {
                    Cliente oClient = db.Cliente.Find(Id);
                    db.Remove(oClient);
                    db.SaveChanges();
                    orsp.Mensaje = "Ok";
                    orsp.Status = 201;
                    orsp.Data = oClient;

                }

            }
            catch (Exception e)
            {
                orsp.Mensaje = "Hubo un error: " + e.Message;
            }

            return Ok(orsp);
        }
    }
}
