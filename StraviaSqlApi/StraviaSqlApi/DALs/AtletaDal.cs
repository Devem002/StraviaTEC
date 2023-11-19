using System.Data;
using System.Data.SqlClient;
using StraviaSqlApi.Dtos;

namespace StraviaSqlApi.DALs;

public class AtletaDal
{
    public static string RegisterUserDB(AtletaDto user)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(GetConnection()))
            {
                string procedure = @"Registrar";
                using (SqlCommand cmd = new SqlCommand(procedure, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Usuario", user.Usuario);
                    cmd.Parameters.AddWithValue("@Nombre", user.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido_1", user.Apellido_1);
                    cmd.Parameters.AddWithValue("@Apellido_2", user.Apellido_2);
                    cmd.Parameters.AddWithValue("@Fecha_nacimiento", user.Fecha_Nacimiento.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@Contrasena", user.Contrasena);
                    cmd.Parameters.AddWithValue("@Foto", user.Foto);
                    cmd.Parameters.AddWithValue("@Nacionalidad", user.Nacionalidad);

                    con.Open();
                    int sdr = (int)cmd.ExecuteScalar();
                    con.Close();
                    if (sdr == -1)
                        return "Taken";
                }
            }
        }

        catch (Exception err)
        {
            Console.Write(err);
            return "Error";
        }
        return "Done";
    }

    public static string LoginUserDB(LoginAtletaDto user)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(GetConnection()))
            {
                string procedure = @"LoginAtleta";
                using (SqlCommand cmd = new SqlCommand(procedure, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Usuario", user.User);
                    cmd.Parameters.AddWithValue("@Contrasena", user.Password);

                    con.Open();
                    int sdr = (int)cmd.ExecuteScalar();
                    con.Close();
                    if (sdr == -1)
                        return "NotFound";
                    else if (sdr == -2)
                        return "WrongPass";
                }
            }
        }
        catch (Exception err)
        {
            Console.Write(err);
            return "Error";
        }
        return "Done";
    }

    public static List<AtletaDto> GetUsers()
    {
        List<AtletaDto> atletas = new();
        try
        {
            using (SqlConnection con = new SqlConnection(GetConnection()))
            {
                string query = @"SELECT * FROM " + "\"ATLETA\"" + ";";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            AtletaDto atleta = new()
                            {
                                Usuario = (string)sdr["Usuario"],
                                Nombre = (string)sdr["Nombre"],
                                Apellido_1 = (string)sdr["Apellido_1"],
                                Apellido_2 = (string)sdr["Apellido_2"],
                                Fecha_Nacimiento = (DateTime)sdr["Fecha_nacimiento"],
                                Foto = (string)sdr["Foto"],
                                Nacionalidad = (string)sdr["Nacionalidad"],
                                Clasificacion = (string)sdr["Clasificacion"]
                            };
                            atletas.Add(atleta);
                        }
                    }
                    con.Close();
                }
            }
        }
        catch (Exception err)
        {
            Console.Write(err);
            return null;
        }
        return atletas;
    }

    public static AtletaDto GetUser(string userId)
    {
        AtletaDto user = new() { Usuario = "" };
        try
        {
            using (SqlConnection con = new SqlConnection(GetConnection()))
            {
                string query = @"SELECT * FROM " + "\"ATLETA\"" +
                                " WHERE " + "\"Usuario\" = '" + userId + "';";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            user.Usuario = (string)sdr["Usuario"];
                            user.Nombre = (string)sdr["Nombre"];
                            user.Apellido_1 = (string)sdr["Apellido_1"];
                            user.Apellido_2 = (string)sdr["Apellido_2"];
                            user.Fecha_Nacimiento = (DateTime)sdr["Fecha_nacimiento"];
                            user.Foto = (string)sdr["Foto"];
                            user.Nacionalidad = (string)sdr["Nacionalidad"];
                        }
                    }
                    con.Close();
                }
            }
        }
        catch (Exception err)
        {
            Console.Write(err);
            return null;
        }
        return user;
    }

    public static string UpdateUser(AtletaDto user, string currenPassword)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(GetConnection()))
            {
                string procedure = @"UpdateAtleta";
                using (SqlCommand cmd = new SqlCommand(procedure, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Usuario", user.Usuario);
                    cmd.Parameters.AddWithValue("@Nombre", user.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido_1", user.Apellido_1);
                    cmd.Parameters.AddWithValue("@Apellido_2", user.Apellido_2);
                    cmd.Parameters.AddWithValue("@Fecha_nacimiento", user.Fecha_Nacimiento.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@Contrasena", currenPassword);
                    cmd.Parameters.AddWithValue("@Foto", user.Foto);
                    cmd.Parameters.AddWithValue("@Nacionalidad", user.Nacionalidad);

                    con.Open();
                    int sdr = (int)cmd.ExecuteScalar();
                    con.Close();
                    if (sdr == -1)
                        return "NotFound";
                    else if (sdr == -2)
                        return "WrongPass";
                }
            }
        }
        catch (Exception err)
        {
            Console.Write(err);
            return "error";
        }
        return "done";
    }

    public static string DeleteUser(string user)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(GetConnection()))
            {
                string query = @"DELETE FROM " + "\"ATLETA\" " +
                                "WHERE " + "\"Usuario\" = '" + user + "';";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    con.Close();
                }
            }
        }
        catch (Exception err)
        {
            Console.Write(err);
            return "Error";
        }
        return "Done";
    }

    public static string AddActivity(ActividadDto act)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(GetConnection()))
            {
                string procedure = @"AddActivity";
                using (SqlCommand cmd = new SqlCommand(procedure, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nmbr_Actividad", act.Nmbr_Actividad);
                    cmd.Parameters.AddWithValue("@Fecha", act.Fecha.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@Hora", act.Hora.ToString("HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@Kms_hechos", act.Kms);
                    cmd.Parameters.AddWithValue("@Duracion", act.Duracion.ToString("HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@Recorrido_gpx", act.Recorrido_gpx);
                    cmd.Parameters.AddWithValue("@Atleta", act.Atleta);
                    cmd.Parameters.AddWithValue("@Clase_actividad", act.Clase_actividad);
                    cmd.Parameters.AddWithValue("@Car_Ret", act.Atleta);
                    cmd.Parameters.AddWithValue("@Car_Ret_Nmbr", act.Clase_actividad);


                    con.Open();
                    int sdr = (int)cmd.ExecuteScalar();
                    con.Close();
                    if (sdr == -1)
                        return "Taken";
                    else if (sdr == -2)
                        return "CurrentDate";
                }
            }
        }
        catch (Exception err)
        {
            Console.Write(err);
            return "Error";
        }
        return "Done";
    }

    public static string GetConnection()
    {
        return
            "Server=tcp:straviatec-server.database.windows.net,1433;Initial Catalog=StraviaTECDB;Persist Security Info=False;User ID=sqladmin;Password=root1234!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    }
}