using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class AlumnoMateriaController : Controller
    {
        // GET: AlumnoMateria
        [HttpGet]
        public ActionResult GetAll()
        {
            Session["alumno"] = null;
            ML.Alumno alumno = new ML.Alumno();
            ML.Result resultAlumnoMateria = new ML.Result();
            resultAlumnoMateria.Objects = new List<object>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6645/");

                var responseTask = client.GetAsync("/AlumnoMateria/GetAll");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Alumno resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Alumno>(resultItem.ToString());
                        resultAlumnoMateria.Objects.Add(resultItemList);
                    }
                }
                alumno.Alumnos = resultAlumnoMateria.Objects;
            }
            return View(alumno);

        }
        [HttpGet]
        public ActionResult MateriasGetAsignadas(ML.Alumno alumno)
        {
            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
            ML.Result resultAsignadas = new ML.Result();
            resultAsignadas.Objects = new List<object>();
            alumnoMateria.Alumno = new ML.Alumno();
            alumnoMateria.Materia = new ML.Materia();
            alumnoMateria.Materia.Materias = new List<object>();

            if (Session["alumno"] == null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:6645/");


                    /*****/
                    var responseTaskAlumno = client.GetAsync("AlumnoMateria/Alumno/" + alumno.IdAlumno);
                    responseTaskAlumno.Wait();

                    var resultAlumno = responseTaskAlumno.Result;

                    if (resultAlumno.IsSuccessStatusCode)
                    {
                        var readTask = resultAlumno.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        var resultItem = readTask.Result.Object;

                        ML.Alumno resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Alumno>(resultItem.ToString());
                        resultAsignadas.Object = resultItemList;

                    }

                    /*****/
                    var responseTask = client.GetAsync("/AlumnoMateria/MateriasAsignadas/" + alumno.IdAlumno);
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.AlumnoMateria resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.AlumnoMateria>(resultItem.ToString());
                            resultAsignadas.Objects.Add(resultItemList);
                        }
                    }
                    alumnoMateria.Alumno = (ML.Alumno)resultAsignadas.Object;
                    alumnoMateria.Materia.Materias = resultAsignadas.Objects;
                    Session["alumno"] = alumnoMateria.Alumno;
                }
            }
            else
            {
                using (var client = new HttpClient())
                {
                    ML.Alumno alumon1 =(ML.Alumno)Session["alumno"];
                    int id = alumon1.IdAlumno;

                    client.BaseAddress = new Uri("http://localhost:6645/");
                    var responseTask = client.GetAsync("/AlumnoMateria/MateriasAsignadas/" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.AlumnoMateria resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.AlumnoMateria>(resultItem.ToString());
                            resultAsignadas.Objects.Add(resultItemList);
                        }
                    }
                    alumnoMateria.Alumno = (ML.Alumno)Session["alumno"];
                    alumnoMateria.Materia.Materias = resultAsignadas.Objects;
                    //Session["alumno"] = alumnoMateria.Alumno;
                }
            }
            return View(alumnoMateria);
        }

        [HttpGet]
        public ActionResult MateriasGetNoAsignadas(int idAlumno)
        {
            ML.Result result = BL.AlumnoMateria.MateriasGetNoAsignadas(idAlumno);
            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
            alumnoMateria.Alumno = new ML.Alumno();
            alumnoMateria.Materia = new ML.Materia();

            alumnoMateria.Alumno = (ML.Alumno)Session["alumno"];

            if(result.Correct)
            {
                alumnoMateria.Materia.Materias = result.Objects;
                return View(alumnoMateria);
            }
            
            return View(alumnoMateria);
        }

        [HttpPost]
        public ActionResult MateriasGetNoAsignadas(ML.AlumnoMateria alumnoMateria)
        {
           if( alumnoMateria.AlumnoMaterias != null)
            {
                foreach (string IdMateria in alumnoMateria.AlumnoMaterias)
                {
                    ML.AlumnoMateria alumnomateria = new ML.AlumnoMateria();

                    alumnomateria.Alumno = new ML.Alumno();
                    alumnomateria.Alumno.IdAlumno = alumnoMateria.Alumno.IdAlumno;

                    alumnomateria.Materia = new ML.Materia();
                    alumnomateria.Materia.IdMateria = int.Parse( IdMateria);

                    ML.Result result = BL.AlumnoMateria.Add(alumnomateria);
                }
            }
           else
            {
            }
           return RedirectToAction("MateriasGetAsignadas",alumnoMateria.Alumno);

        }
        [HttpGet]
        public ActionResult Delete(int idAlumnoMateria)
        {
            
             ML.Result result = BL.AlumnoMateria.Delete(idAlumnoMateria);
            if(result.Correct)
            {
                ViewBag.Message = "Se elimino correctamente la materia del alumno";
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al eliminar la materia del alumno";
            }
            
            return View("ValidationModal");
        }
    }
}