using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class AlumnoController : Controller
    {
        // GET: Alumno
        public ActionResult GetAll()
        {
            ML.Alumno alumno = new ML.Alumno();

            AlumnoService.AlumnoClient alumnoClient = new AlumnoService.AlumnoClient();
            var alumnoSL = alumnoClient.GetAll();

            ML.Result result= new ML.Result();
            result.Objects = new List<object>();
            result.Objects = alumnoSL.Objects.ToList();

            alumno.Alumnos = result.Objects.ToList();
            if (alumnoSL != null)
            {
                return View(alumno);
            }
            return View(alumno);
        }
        [HttpGet]
        public ActionResult Form(int? idAlumno)
        {
            ML.Alumno alumno = new ML.Alumno();
            if (idAlumno == null)
            {
                return View(alumno);
            }
            else
            {
                AlumnoService.AlumnoClient alumnoClient = new AlumnoService.AlumnoClient();
                

                ML.Result result = new ML.Result();
                

                result.Object = alumnoClient.GetById(idAlumno.Value).Object; 
                alumno = (ML.Alumno)(ML.Alumno)result.Object;
                if(result.Object != null)
                {
                    return View(alumno);
                }
                else
                {
                    ViewBag.Message = "Ubo un problema al recuperar la informacion ";
                }
            }
            return View("ValidationModal");
        }

        [HttpPost]
        public ActionResult Form(ML.Alumno alumno)
        {
            if(alumno.IdAlumno == 0)
            {
                AlumnoService.AlumnoClient alumnoClient = new AlumnoService.AlumnoClient();
                var alumnoSL = alumnoClient.Add(alumno);
                if (alumnoSL.Correct)//alumnoSL)
                {
                    ViewBag.Message = "Se inserto correctamente el alumno";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al insertar el alumno " + alumnoSL.ErrorMessage;
                }
            }
            else
            {
                AlumnoService.AlumnoClient alumnoClient = new AlumnoService.AlumnoClient();
                var alumnoSL = alumnoClient.Update(alumno);
                if(alumnoSL.Correct)
                {
                    ViewBag.Message = "Se actualizo correctamente el alumno";
                }
                else
                {
                    ViewBag.Message = "No se actualizo el alumno";
                }

            }
            
            return View("ValidationModal");

        }
        public ActionResult Delete(int idAlumno)
        {
            AlumnoService.AlumnoClient alumnoClient = new AlumnoService.AlumnoClient();
            ML.Result result = new ML.Result();

            result.Correct = alumnoClient.Delete(idAlumno).Correct;

            if(result.Correct)
            {
                ViewBag.Message = "Registro elimnado correctamente";
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al eliminar el registro";
            }
            return View("ValidationModal");
        }

    }
}