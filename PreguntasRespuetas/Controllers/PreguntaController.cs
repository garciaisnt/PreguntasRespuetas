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
    public class PreguntaController : Controller
    {

        PreguntaDAL preguntaDAL = new PreguntaDAL();
        RespuestaDAL respuestaDAL = new RespuestaDAL();
        EstadoDAL estadoDAL = new EstadoDAL();
        UsuarioDAL usuarioDAL = new UsuarioDAL();

        
        public ActionResult Index()
        {
            if(Session.Keys.Count == 0)
            {
                return RedirectToAction("Index","Home");
            }

            return View(preguntaDAL.Consultar());
        }

        public ActionResult Crear()
        {
            if (Session.Keys.Count > 0)
            {
                return View();
            }
            return RedirectToAction("Index","Home");               
        }

        [HttpPost]
        public ActionResult Crear(PreguntaDTO model)
        {
            PreguntaEN preguntaEN = new PreguntaEN();

            if (ModelState.IsValid)
            {
                preguntaEN.Pregunta = model.pregunta;
                preguntaEN.Fecha_Creacion = DateTime.Now;
                preguntaEN.Estado = estadoDAL.ConsultarPorNombre("abierto");
                preguntaEN.Usuario = usuarioDAL.ConsultarPorId((long)Session["idUsuario"]);

                preguntaDAL.Insertar(preguntaEN);

                return RedirectToAction("Index", "Pregunta");


            }
            return View();
        }

        public ActionResult Respuestas(long Id)
        {
            if (Session.Keys.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Pregunta = preguntaDAL.ConsultarPorId(Id);
            ViewBag.Respuestas = respuestaDAL.ConsultarPorIdPregunta(Id);
            
            return View();
        }

        public ActionResult Responder(long Id)
        {
            if (Session.Keys.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.IdPregunta = Id;
            return View();

        }
        [HttpPost]
        public ActionResult Responder(RespuestaDTO model)
        {
            RespuestaEN respuestaEN = new RespuestaEN();

            if (ModelState.IsValid)
            {
                respuestaEN.Respuesta = model.respuesta;
                respuestaEN.Fecha_Creacion = DateTime.Now;
                respuestaEN.Usuario = usuarioDAL.ConsultarPorId((long)Session["idUsuario"]);
                respuestaEN.Pregunta = preguntaDAL.ConsultarPorId(model.IdPregunta);

                respuestaDAL.Insertar(respuestaEN);

                return RedirectToAction("Respuestas","Pregunta", new { Id = model.IdPregunta});

            }
            return View();

        }

        public ActionResult CerrarPregunta(long Id)
        {
            PreguntaEN pregunta = new PreguntaEN();
            pregunta.Id = Id;
            pregunta.Estado = estadoDAL.ConsultarPorNombre("cerrado");

            preguntaDAL.CambiarEstadoPregunta(pregunta);
            
            return RedirectToAction("Index", "Pregunta");           
                
        }


    }
}