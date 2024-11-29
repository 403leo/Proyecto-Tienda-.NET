using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodigoProgramado_Grupo4.Models
{
    public class Suscripcion
    {
        public int Id { get; set; }
        public string Codigo_subs { get; set; }
        public string Tipo_subs { get; set; }
        public int UsuarioId { get; set; }
        public virtual Usuario Usuarios { get; set; }

    }
}