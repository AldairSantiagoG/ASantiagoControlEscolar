using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class AlumnoMateria
    {
        public static ML.Result MateriasGetNoAsignadas(int idAlumno)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ASantiagoControlEscolarEntities1 context = new DL.ASantiagoControlEscolarEntities1())
                {
                    var getNoAsignadas = context.MateriasGetNoAsignadas(idAlumno);
                    //var getNoAsignadas = context.mate

                    result.Objects = new List<object>();

                    if (getNoAsignadas != null)
                    {
                        foreach (var obj in getNoAsignadas)
                        {
                            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
                            
                            alumnoMateria.Materia = new ML.Materia();
                            alumnoMateria.Materia.IdMateria = obj.IdMateria;
                            alumnoMateria.Materia.Nombre = obj.Nombre;
                            alumnoMateria.Materia.Costo = obj.Costo.Value;

                            result.Objects.Add(alumnoMateria);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros";
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result MateriasGetAsignadas(int idAlumno)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.ASantiagoControlEscolarEntities1 context = new DL.ASantiagoControlEscolarEntities1())
                {
                    var getAsignadas = context.MateriasGetAsignadas(idAlumno).ToList();

                    result.Objects = new List<object>();

                    if(getAsignadas.Count > 0)
                    {
                        foreach (var obj in getAsignadas)
                        {
                            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
                            alumnoMateria.IdAlumnoMateria = obj.IdAlumnoMateria;

                            alumnoMateria.Materia = new ML.Materia();
                            alumnoMateria.Materia.IdMateria = obj.IdMateria.Value;
                            alumnoMateria.Materia.Nombre = obj.Nombre;
                            alumnoMateria.Materia.Costo = obj.Costo.Value;
                          
                            result.Objects.Add(alumnoMateria);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct=false;
                        result.ErrorMessage = "No se encontraron registros";
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
            }

            return result;
        }
        public static ML.Result Add(ML.AlumnoMateria alumnoMateria)
        {
            ML.Result result = new ML.Result();

            try
            {
                using( DL.ASantiagoControlEscolarEntities1 context = new DL.ASantiagoControlEscolarEntities1() )
                {
                    var add = context.AlumnoMateriasAdd(alumnoMateria.Alumno.IdAlumno, alumnoMateria.Materia.IdMateria);

                    if(add > 0 )
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se asigno la materia al alumno";
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result Delete(int idAlumnoMateria)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.ASantiagoControlEscolarEntities1 context = new DL.ASantiagoControlEscolarEntities1())
                {
                    var delete = context.AlumnoMateriaDelete(idAlumnoMateria);

                    if(delete > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct= false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
            }
            
            return result;
        }

    }
}
