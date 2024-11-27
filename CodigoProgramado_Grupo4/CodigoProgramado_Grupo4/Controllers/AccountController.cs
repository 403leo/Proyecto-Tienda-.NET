using CodigoProgramado_Grupo4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodigoProgramado_Grupo4.Controllers
{
    public class AccountController : Controller
    {
        private UsuarioPedidosDbContext db = new UsuarioPedidosDbContext();
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            foreach (Usuario user in db.Usuarios)
            {
                if (user.Password == password && user.Username == username)
                {
                    Usuario usuario = user;
                    Session["User"] = usuario;
                    return RedirectToAction("Index", "Home");
                }
            }
            return View("LogginError");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registro([Bind(Include = "Id,Nombre,Apellidos,ultimaFechaConexion,Username,Password,Estado,Role,isAuthenticated")] Usuario usuario)
        {
            foreach (Usuario user in db.Usuarios)
            {
                if (user.Username == usuario.Username)
                {
                    return View("RegisterError");
                }
            }
            usuario.isAuthenticated = true;
            usuario.Estado = true;
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                Session["User"] = new Usuario { Nombre = usuario.Nombre, Apellidos = usuario.Apellidos, ultimaFechaConexion = usuario.ultimaFechaConexion, Username = usuario.Username, Password = usuario.Password, Role = usuario.Role, Estado = true, isAuthenticated = true };
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");

        }

        public ActionResult AccessDenied() 
        { 
            return View();
        }
    }
}