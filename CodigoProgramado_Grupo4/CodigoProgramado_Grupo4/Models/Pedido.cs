using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodigoProgramado_Grupo4.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime fecha { get; set; }
        public bool estado { get; set; }
        public int UsuarioId { get; set; }
        public virtual Usuario Usuarios { get; set; }

    }
}