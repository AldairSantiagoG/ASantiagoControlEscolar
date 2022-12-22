using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Materia
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ASantiagoControlEscolarEntities1 context = new DL.ASantiagoControlEscolarEntities1())
                {
                    var materiaGetAll = context.MateriaGetAll();

                    result.Objects = new List<object>();

                    if (materiaGetAll != null)
                    {
                        foreach (var obj in materiaGetAll)
                        {
                            ML.Materia materia = new ML.Materia();

                            materia.IdMateria = obj.IdMateria;
                            materia.Nombre = obj.Nombre;
                            materia.Costo = obj.Costo.Value;

                            result.Objects.Add(materia);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
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
        public static ML.Result Add(ML.Materia materia)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ASantiagoControlEscolarEntities1 context = new DL.ASantiagoControlEscolarEntities1())
                {
                    var materiaAdd = context.MateriaAdd(materia.Nombre, materia.Costo);

                    if (materiaAdd > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se inserto la materia";
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
        public static ML.Result GetById(int idMateria)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ASantiagoControlEscolarEntities1 context = new DL.ASantiagoControlEscolarEntities1())
                {
                    var materiaGetById = context.MateriaGetById(idMateria).FirstOrDefault();

                    result.Objects = new List<object>();

                    if (materiaGetById != null)
                    {
                        ML.Materia materia = new ML.Materia();

                        materia.IdMateria = materiaGetById.IdMateria;
                        materia.Nombre = materiaGetById.Nombre;
                        materia.Costo = materiaGetById.Costo.Value;

                        result.Object = materia;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
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

        public static ML.Result Update(ML.Materia materia)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ASantiagoControlEscolarEntities1 context = new DL.ASantiagoControlEscolarEntities1())
                {
                    var materiaAdd = context.MateriaUpdate(materia.IdMateria,materia.Nombre, materia.Costo);

                    if (materiaAdd > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se actualizo la materia";
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

        public static ML.Result Delete(int idMateria)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ASantiagoControlEscolarEntities1 context = new DL.ASantiagoControlEscolarEntities1())
                {
                    var materiaAdd = context.MateriaDelete(idMateria);

                    if (materiaAdd > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se elimino la materia";
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
