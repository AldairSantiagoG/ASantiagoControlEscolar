using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WebAPI.Controllers
{
    public class AlumnoMateriaController : ApiController
    {
        // GET: api/AlumnoMateria
        [HttpGet]
        [Route("AlumnoMateria/GetAll")]
        public IHttpActionResult GetAll()
        {
            ML.Result result = BL.Alumno.GetAll();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: api/AlumnoMateria/5
        [HttpGet]
        [Route("AlumnoMateria/MateriasAsignadas/{id}")]
        public IHttpActionResult Get(int id)
        {
            ML.Result result = BL.AlumnoMateria.MateriasGetAsignadas(id);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("AlumnoMateria/Alumno/{id}")]
        public IHttpActionResult GetAlumno(int id)
        {
            ML.Result result = BL.Alumno.GetById(id);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        // POST: api/AlumnoMateria
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/AlumnoMateria/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/AlumnoMateria/5
        public void Delete(int id)
        {
        }
    }
}
