﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVentas.Models.Dao;

namespace WSVentas.Services
{
    public interface IVentaService
    {
        public void Add(VentaDao dao);
    }
}
