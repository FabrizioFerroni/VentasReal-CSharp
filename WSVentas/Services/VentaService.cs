using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVentas.Models;
using WSVentas.Models.Dao;

namespace WSVentas.Services
{
    public class VentaService : IVentaService
    {
        public void Add(VentaDao dao)
        {

            using (VentasRealContext db = new VentasRealContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var venta = new Venta();
                        venta.Total = dao.Conceptos.Sum(d => d.Cantidad * d.PrecioUnitario);
                        venta.Fecha = DateTime.Now;
                        venta.IdCliente = dao.IdCliente;
                        db.Venta.Add(venta);
                        db.SaveChanges();

                        foreach (var concepto in dao.Conceptos)
                        {
                            var conc = new Models.Concepto();
                            conc.Cantidad = concepto.Cantidad;
                            conc.IdProducto = concepto.IdProducto;
                            conc.PrecioUnitario = concepto.PrecioUnitario;
                            conc.Importe = concepto.Importe;
                            conc.IdVenta = venta.Id;
                            db.Concepto.Add(conc);
                            db.SaveChanges();
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw new Exception("Ocurrio un error en la inserción");
                    }
                }
            }

        }
    }
}
