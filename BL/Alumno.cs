using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace BL
{
    public class Alumno
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using(SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "AlumnoGetAll";
                    using(SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.Connection.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        result.Objects = new List<object>();
                        if (dr.FieldCount > 0)
                        {
                            while (dr.Read())
                            {
                                ML.Alumno alumno = new ML.Alumno();
                                alumno.IdAlumno = int.Parse(dr["IdAlumno"].ToString());
                                alumno.Nombre = dr["Nombre"].ToString();
                                alumno.ApellidoPaterno = dr["ApellidoPaterno"].ToString();
                                alumno.ApellidoMaterno = dr["ApellidoMaterno"].ToString();
                                result.Objects.Add(alumno);
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
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
            }

            return result;
        }
        public static ML.Result Add(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "AlumnoAdd @Nombre,@ApellidoPaterno,@ApellidoMaterno";
                    using(SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        cmd.Connection.Open();

                        SqlParameter[] collection = new SqlParameter[3];

                        collection[0] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[0].Value = alumno.Nombre;
                        collection[1] = new SqlParameter("@ApellidoPaterno", SqlDbType.VarChar);
                        collection[1].Value = alumno.ApellidoPaterno;
                        collection[2] = new SqlParameter("ApellidoMaterno", SqlDbType.VarChar);
                        collection[2].Value = alumno.ApellidoMaterno;

                       cmd.Parameters.AddRange(collection);

                        

                        int RowsAffected = cmd.ExecuteNonQuery();
                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct=false;
                            result.ErrorMessage = "No se inserto el resgristro alumno";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex=ex;
            }

            return result;
        }
        public static ML.Result GetById(int idAlumno)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "AlumnoGetById @IdAlumno";

                    using(SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        cmd.Connection.Open();

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdAlumno", SqlDbType.Int);
                        collection[0].Value = idAlumno;

                        cmd.Parameters.AddRange(collection);

                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            ML.Alumno alumno = new ML.Alumno();
                            alumno.IdAlumno = int.Parse(dr["IdAlumno"].ToString());
                            alumno.Nombre = dr["Nombre"].ToString();
                            alumno.ApellidoPaterno = dr["ApellidoPaterno"].ToString();
                            alumno.ApellidoMaterno = dr["ApellidoMaterno"].ToString();
                            result.Object = alumno;
                            result.Correct = true;
                        }else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se encontro el registro";
                        }

                        
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

        public static ML.Result Update(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();

            try
            {
                using( SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()) )
                {
                    string query = "AlumnoUpdate @IdAlumno,@Nombre,@ApellidoPaterno,@ApellidoMaterno";
                    using(SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        cmd.Connection.Open();

                        SqlParameter [] collection = new SqlParameter[4];
                        collection[0] = new SqlParameter("IdAlumno",SqlDbType.Int);
                        collection[0].Value = alumno.IdAlumno;
                        collection[1] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[1].Value = alumno.Nombre;
                        collection[2] = new SqlParameter("ApellidoPaterno", SqlDbType.VarChar);
                        collection[2].Value = alumno.ApellidoPaterno;
                        collection[3] = new SqlParameter("ApellidoMaterno", SqlDbType.VarChar);
                        collection[3].Value = alumno.ApellidoMaterno;

                        cmd.Parameters.AddRange(collection);

                        int RowsAffects = cmd.ExecuteNonQuery();

                        if( RowsAffects > 0 )
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct=false;
                            result.ErrorMessage = "Ocurrio un error al actualizar el alumno " + alumno.Nombre;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex=ex;
            }

            return result;
        }

        public static ML.Result Delete(int IdAlumno)
        {
            ML.Result result = new ML.Result();

            try
            {
                using( SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()) )
                {
                    string query = "AlumnoDelete @IdAlumno";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        cmd.Connection.Open();

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdAlumno", SqlDbType.Int);
                        collection[0].Value = IdAlumno;

                        cmd.Parameters.AddRange(collection);

                        int RowsAffects = cmd.ExecuteNonQuery();

                        if (RowsAffects > 0)
                        {
                            result.Correct = true;

                        }
                        else {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrio un error al eliminar el registro";
                        }
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
