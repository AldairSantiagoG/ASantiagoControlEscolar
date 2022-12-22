using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace SL_WebAPI.Controllers
{
   
    public class MateriaController : ApiController
    {
        // GET: Materia
        [System.Web.Http.HttpGet]
        [Route("Materia/GetAll")]
        public IHttpActionResult GetAll()
        {
            ML.Result result = BL.Materia.GetAll();
            if(result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: Materia/Details/5
        [System.Web.Http.HttpPost]
        [Route("Materia/Add")]

        public IHttpActionResult Add([FromBody] ML.AlumnoMateria alumnoMateria)
        {
            ML.Result result = BL.AlumnoMateria.Add(alumnoMateria);
            
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [System.Web.Http.HttpGet]
        [Route("Materia/GetById/{id}")]
        public IHttpActionResult GetById(int id)
        {
            ML.Result result = BL.Materia.GetById(id);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [System.Web.Http.HttpPut]
        [Route("Materia/Update/{id}")]
        public IHttpActionResult Update(int id,[FromBody] ML.Materia materia)
        {
            ML.Result result = BL.Materia.Update(materia);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [System.Web.Http.HttpDelete]
        [Route("Materia/Delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            ML.Result result = BL.Materia.Delete(id);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
