using System.Data.SqlClient;
using StraviaSqlApi.Dtos;

namespace StraviaSqlApi.DALs;

public class AtletaDal
{
    public static List<AtletaDto> GetUsers()
    {
        List<AtletaDto> atletas = new();
        try
        {
            using (SqlConnection con = new SqlConnection(GetConn()))
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
    
    public static string GetConn()
    {
        return
            "Server=tcp:straviatec-server.database.windows.net,1433;Initial Catalog=StraviaTECDB;Persist Security Info=False;User ID=sqladmin;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    }
}