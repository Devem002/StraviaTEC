using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using StraviaSqlApi.DTOs.Requests;
using StraviaSqlApi.DTOs.Responses;

namespace StraviaSqlApi.DAL
{
    public static class RaceDAL
    {
        public static List<RaceResponseDto> GetRaces()
        {
            List<RaceResponseDto> races = new();
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnection()))
                {
                    string query = @"SELECT * FROM CARRERA;";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                RaceResponseDto race = new()
                                {
                                    Nombre = (string)sdr["Nombre"],
                                    Fecha = (DateTime)sdr["Fecha"],
                                    Hora = (DateTime)sdr["Hora"],
                                    Precio = (decimal)sdr["Precio"],
                                    Kms_total = (int)sdr["Kms_total"],
                                    Recorrido_gpx = (string)sdr["Recorrido_gpx"],
                                    Finalizado = (bool)sdr["Finalizado"],
                                    Clase_actividad = (string)sdr["Clase_actividad"]
                                };
                                races.Add(race);
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
            return races;
        }
        public static string RegisterRaceDB(RaceRegisterDto race)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnection()))
                {
                    string procedure = @"RegistrarCarrera";
                    using (SqlCommand cmd = new SqlCommand(procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", race.Nombre);
                        cmd.Parameters.AddWithValue("@Finalizado", race.Finalizado);
                        cmd.Parameters.AddWithValue("@Recorrido_gpx", race.Recorrido_gpx);
                        cmd.Parameters.AddWithValue("@Precio", race.Precio);
                        cmd.Parameters.AddWithValue("@Privado", race.Privado);
                        if (race.Privado)
                        {
                            cmd.Parameters.AddWithValue("@Grupo", race.Grupo);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Grupo", "");
                        }
                        cmd.Parameters.AddWithValue("@Fecha", race.Fecha.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@Clase_actividad", race.Clase_actividad);
                        cmd.Parameters.AddWithValue("@Hora", race.Hora.ToString("HH:MM:ss"));

                        con.Open();
                        int sdr = (int)cmd.ExecuteScalar();
                        con.Close();
                        if (sdr == -1)
                        {
                            return "Already Exists";
                        }
                        if (sdr == -2)
                        {
                            return "Activity Type not found";
                        }
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


        public static List<RaceVisibilityDto> GetRaceVisibility()
        {
            List<RaceVisibilityDto> races = new();
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnection()))
                {
                    string query = @"SELECT * FROM PRIV_CARRERA ;";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                RaceVisibilityDto race = new()
                                {
                                    Nmbr_Carrera = (string)sdr["Nmbr_Carrera"],
                                    Nmbr_Grupo = (string)sdr["Nmbr_Grupo"]
                                };
                                races.Add(race);
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
            return races;
        }

        public static List<RaceResponseDto> GetRacesByUser(string user)
        {
            List<RaceResponseDto> races = new();
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnection()))
                {
                    string query = 
                        @"SELECT *" +
                        "FROM CARRERA WHERE CARRERA.NOMBRE IN(SELECT Nmbr_Carrera FROM ACT_CARRERA WHERE "
                        + "\"ATLETA\"" +"='"+user+"');";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                RaceResponseDto race = new()
                                {
                                    Nombre = (string)sdr["Nombre"],
                                    Fecha=(DateTime)sdr["Fecha"],
                                    Hora = (DateTime)sdr["Hora"],
                                    Clase_actividad=(string)sdr["Clase_actividad"],
                                    Kms_total=(int)sdr["Kms_total"],
                                    Recorrido_gpx=(string)sdr["Recorrido_gpx"],
                                    Finalizado=(bool)sdr["Finalizado"],
                                    Precio=(decimal)sdr["Precio"]
                                };
                                races.Add(race);
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
            return races;
        }


        public static string RaceRegister(string User,int id, string optionselect)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnection()))
                {
                    string procedure = @"Register_in_Race";;
                    using (SqlCommand cmd = new SqlCommand(procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@User", User);
                        cmd.Parameters.AddWithValue("@RaceId", id);
                        cmd.Parameters.AddWithValue("@Category", optionselect);
                        con.Open();
                        int sdr = (int)cmd.ExecuteScalar();
                        con.Close();
                        if (sdr == -1)
                        {
                            return "Already Exists";
                        }
                        if (sdr == -2)
                        {
                            return "Activity Type not found";
                        }
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

        private static string GetConnection()
        {
            return "Server=tcp:straviatec-server.database.windows.net,1433;Initial Catalog=StraviaTecDB-Azure;Persist Security Info=False;User ID=sqladmin;Password=root1234!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }
    }
}