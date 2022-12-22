using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class MateriaController : Controller
    {
      
        // GET: Materia
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Materia materia = new ML.Materia();
            ML.Result resultMateria = new ML.Result();
            resultMateria.Objects = new List<object>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6645/");

                var responseTask = client.GetAsync("/Materia/GetAll");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Materia resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Materia>(resultItem.ToString());
                        resultMateria.Objects.Add(resultItemList);
                    }
                }
                materia.Materias = resultMateria.Objects;
            }
            return View(materia);
        }
        [HttpGet]
        public ActionResult Form(int? idMateria)
        {
            ML.Materia materia = new ML.Materia();
            ML.Result resultMateria = new ML.Result();

            if(idMateria == null)
            {
                return View(materia);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:6645/");

                    var responseTask = client.GetAsync("Materia/GetById/" + idMateria);
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        var resultItem = readTask.Result.Object;

                        ML.Materia resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Materia>(resultItem.ToString());
                        resultMateria.Object = resultItemList;

                    }
                    materia = (ML.Materia)resultMateria.Object;
                    return View(materia);
                }
            }
            
        }
        [HttpPost]
        public ActionResult Form(ML.Materia materia)
        {
            if(ModelState.IsValid)
            {
                if (materia.IdMateria == 0)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:6645/");

                        //HTTP POST
                        var postTask = client.PostAsJsonAsync<ML.Materia>("Materia/Add", materia);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            //return RedirectToAction("GetAll");
                            ViewBag.Message = "La materia se agrego correctamente";
                        }
                        else
                        {
                            ViewBag.Message = "Ocurrio un error al agregar materia" + result.RequestMessage;
                        }
                    }
                }
                else
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:6645/");

                        //HTTP POST
                        var postTask = client.PutAsJsonAsync<ML.Materia>("Materia/Update/" + materia.IdMateria, materia);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {

                            ViewBag.Message = "La materia se actualizo correctamente";
                        }
                        else
                        {
                            ViewBag.Message = "Ocurrio un error al actualizar la materia";
                        }


                    }
                }
            }
           
            return View("ValidationModal");
        }
        [HttpGet]
        public ActionResult Delete(int idMateria)
        {
            ML.Result resultList = new ML.Result();
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:6645/");

                //HTTP POST
                var postTask = client.DeleteAsync("Materia/Delete/" + idMateria);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Materia eliminada correctamente";
                }
                else
                {
                    ViewBag.Message = "Error al elminar materia";
                }
            }
            return PartialView("ValidationModal");
        }

    }
}