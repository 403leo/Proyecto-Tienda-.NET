﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodigoProgramado_Grupo4.Models
{
    public class Resena

    {
        public int Id { get; set; }
        public String comentario { get; set; }
        public int UsuarioId { get; set; }
        public virtual Usuario Usuarios { get; set; }
        public int ProductoId { get; set; }
        public virtual Producto Productos { get; set; }
    }
}