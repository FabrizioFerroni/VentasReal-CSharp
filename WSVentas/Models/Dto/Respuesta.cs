using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSVentas.Models.Dto
{
    public class Respuesta
    {
        public int Status { get; set; }
        public string Mensaje { get; set; }
        public object Data { get; set; }

        public Respuesta()
        {
            this.Status = 404;
        }
    }
}
