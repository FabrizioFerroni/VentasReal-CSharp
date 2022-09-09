using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WSVentas.Models.Dao
{
    public class VentaDao
    {
        [Required]
        [Range(1, Double.MaxValue, ErrorMessage = "El valor del IdCliente debe ser mayor que 0")]
        [ExisteCliente(ErrorMessage = "El cliente no éxiste")]
        public int IdClienExisteClienteAttributete { get; set; }

        [Required]
        [MinLength(1,ErrorMessage = "Deben existir conceptos")]
        public List<Concepto> Conceptos { get; set; }

        public VentaDao()
        {
            this.Conceptos = new List<Concepto>();
        }
    }

    public class Concepto
    {
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Importe { get; set; }
        public int IdProducto { get; set; }
    }
    #region Validaciones
    public class ExisteClienteAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int idClt = (int)value;
            using (var db = new Models.VentasRealContext())
            {
                if (db.Cliente.Find(idClt) == null) return false;
            }
            return true;
        }
    }
    #endregion
}
