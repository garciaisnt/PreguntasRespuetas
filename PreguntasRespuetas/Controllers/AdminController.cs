using DAL;
using EN;
using PreguntasRespuetas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PreguntasRespuetas.Controllers
{
    public class AdminController : Controller
    {
        RolDAL rolDAL = new RolDAL();
        UsuarioDAL usuarioDAL = new UsuarioDAL();

        public ActionResult Index()
        {

            if(Session["usuario"] == null || (string)Session["rolUsuario"] != "admin")
            {
                return RedirectToAction("Index","Home");
            }
            UsuarioDAL usuario = new UsuarioDAL();

            return View(usuario.Consultar());
        }

        public ActionResult CrearUsuario()
        {
            if (Session["usuario"] == null || (string)Session["rolUsuario"] != "admin")
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.rolesList = rolDAL.Consultar();

            return View();
        }

        [HttpPost]
        public ActionResult CrearUsuario(RegistroUsuarioDTO model)
        {
            ViewBag.rolesList = rolDAL.Consultar();

            if (ModelState.IsValid)
            {

                UsuarioEN usuario = new UsuarioEN();
                usuario.Usuario = model.usuario;
                usuario.Clave = model.clave;
                usuario.Fecha_Creacion = DateTime.Now;
                usuario.Rol = rolDAL.ConsultarPorId(model.idRol);

                if (!usuarioDAL.UsuarioExistente(usuario))
                {
                    usuarioDAL.Insertar(usuario);
                    return RedirectToAction("Index", "Admin");
                }
                ViewBag.ErrorCrearUsuario = "Usuario existente.";
                return View();
            }
           
            return View();
        }

    }
}