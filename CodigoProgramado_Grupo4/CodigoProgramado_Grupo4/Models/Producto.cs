﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodigoProgramado_Grupo4.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public decimal Precio { get; set; }
        public bool disponibilidadInventario { get; set; }
        public bool estado { get; set; }
        public string rutaImagen { get; set; }
        public virtual ICollection<Catalogo> Catalogos { get; set; }
        public virtual ICollection<CarritoProducto> CarritoProductos { get; set; }
        public virtual ICollection<CodigoDescuento> CodigoDescuentos { get; set; }
        public virtual ICollection<Resena> Resenas { get; set; }
        public virtual ICollection<FiltroBusqueda> FiltroBusquedas { get; set; }
        public virtual ICollection<ListaDeseos> ListaDeseos { get; set; }
    }
}