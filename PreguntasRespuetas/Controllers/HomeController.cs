using DAL;
using EN;
using System;
using PreguntasRespuetas.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PreguntasRespuetas.Controllers
{
    public class HomeController : Controller
    {
        UsuarioDAL usuarioDAL = new UsuarioDAL();
        public ActionResult Index()
        {
            ViewBag.NumeroUsuarios = usuarioDAL.Consultar().Count;
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginDTO model)
        {
            UsuarioDAL usuarioDAL = new UsuarioDAL();
            RolDAL rolDAL = new RolDAL();

            if (ModelState.IsValid && ( usuarioDAL.Consultar().Count > 0) )
            {
                UsuarioEN usuario = new UsuarioEN();
                usuario.Usuario = model.usuario;
                usuario.Clave = model.clave;
                usuario.Fecha_Creacion = DateTime.Now;
                usuario.Rol = rolDAL.ConsultarPorNombre("admin");

                usuarioDAL.Insertar(usuario);

                return RedirectToAction("Index", "Admin");

            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginDTO model)
        {
            ViewBag.NumeroUsuarios = usuarioDAL.Consultar().Count;

            if (ModelState.IsValid)
            {
                UsuarioEN usuario = new UsuarioEN();
                usuario.Usuario = model.usuario;
                usuario.Clave = model.clave;

                UsuarioEN usuarioValidado = usuarioDAL.ValidarLogin(usuario);

                if (!string.IsNullOrEmpty(usuarioValidado.Clave))
                {
                    Session["idUsuario"]     = usuarioValidado.Id;
                    Session["usuario"]      = usuarioValidado.Usuario;
                    Session["rolUsuario"]   = usuarioValidado.Rol.Nom_Rol;


                    if (usuarioValidado.Rol.Nom_Rol == "admin")
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    return RedirectToAction("Index", "Pregunta");
                }
            }           

            ViewBag.ErrorLogin = "No valido";
            return View("Index");

        }

        public ActionResult CerrarSesion()
        {
            Session.Contents.RemoveAll();
            return RedirectToAction("Index","Home");
        }





    }
}