using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using StraviaSqlApi.DTOs.Requests;
using StraviaSqlApi.DTOs.Responses;

namespace StraviaSqlApi.DAL
{
    /// <summary>
    /// DAL for Challenge related SQL requests. 
    /// </summary>
    public static class ChallengeDAL
    {
        /// <summary>
        /// Get all the challenges from database
        /// </summary>
        /// <returns><c>List</c> with the Challenges or an error message</returns>
        public static List<ChallengeResponseDto> GetChallenges()
        {
            List<ChallengeResponseDto> challenges = new();
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnection()))
                {
                    string query = @"SELECT * FROM RETOS;";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                ChallengeResponseDto challenge = new()
                                {
                                    Nombre = (string)sdr["Nombre"],
                                    Kms_total = (int)sdr["Kms"],
                                    Finalizado=(bool)sdr["Finalizado"],
                                    Fecha_inicio = (DateTime)sdr["Fecha_inicio"],
                                    Fecha_fin = (DateTime)sdr["Fecha_fin"],
                                    Fondo_altura = (string)sdr["Fondo_altura"],
                                    Clase_actividad = (string)sdr["Clase_actividad"],
                                };
                                challenges.Add(challenge);
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
            return challenges;
        }

        /// <summary>
        /// Register Challenge participation in the database
        /// </summary>
        /// <param name="user"><c>string</c>: User account</param>
        /// <param name="Nmbr_Reto"><c>int</c>: Challenge Id to participate on</param>
        /// <returns><c>string</c> with the participation result</returns>

        public static string GetInChallenge(string user, string Nmbr_Reto, string Nmbr_Actividad)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnection()))
                {
                    string query = @"INSERT INTO ACT_RETO (" + "\"ATLETA\"" + ", Nmbr_Reto, Nmbr_Actividad) " +
                                    "VALUES('" + user + "', '" + Nmbr_Reto + "', '" + Nmbr_Actividad + "');";
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

        /// <summary>
        /// Get the visibility of all the challenges from the database
        /// </summary>
        /// <returns><c>List</c> Challenges and their visibility or an error message</returns>
        public static List<ChallengeVisibilityDto> GetChallengeVisibility()
        {
            List<ChallengeVisibilityDto> challenges = new();
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnection()))
                {
                    string query = @"SELECT * PRIV_RETO ;";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                ChallengeVisibilityDto challenge = new()
                                {
                                    GroupId = (string)sdr["Nmbr_Grupo"],
                                    ChallengeId = (string)sdr["Nmbr_reto"]
                                };
                                challenges.Add(challenge);
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
            return challenges;
        }

        /// <summary>
        /// Register a new Challenge in the database
        /// </summary>
        /// <param name="challenge"><c>string</c>: New Challenge name</param>
        /// <returns><c>string</c> With the registration result</returns>
        public static string RegisterChallengeDB(ChallengeRegisterDto challenge)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnection()))
                {
                    string procedure = @"RegistrarReto";
                    using (SqlCommand cmd = new SqlCommand(procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", challenge.Nombre );
                        cmd.Parameters.AddWithValue("@Kms_total", challenge.Kms_total);
                        cmd.Parameters.AddWithValue("@Finalizado", challenge.Finalizado);
                        cmd.Parameters.AddWithValue("@Fondo_altura", challenge.Fondo_altura);
                        cmd.Parameters.AddWithValue("@Privado", challenge.Privado);
                        if (challenge.Privado)
                        {
                            cmd.Parameters.AddWithValue("@Grupo", challenge.Grupo);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Grupo", "");
                        }
                        cmd.Parameters.AddWithValue("@Fecha_inicio", challenge.Fecha_inicio.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@Fecha_fin", challenge.Fecha_fin.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@Clase_actividad", challenge.Clase_actividad);

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

        /// <summary>
        /// Gets all the challenges from the database to which the user is subscribed
        /// </summary>
        /// <param name="user"><c>string</c>: User account</param>
        /// <returns><c>List</c>: Challenges to which the user is subscribed</returns>
        public static List<ChallengeResponseDto> GetChallengesByUser(string user)
        {
            List<ChallengeResponseDto> challenges = new();
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnection()))
                {
                    string query =
                        @"SELECT *" +
                        "FROM RETOS WHERE RETOS.Nombre IN(SELECT Nmbr_Reto FROM ACT_RETO WHERE "
                        + "\"ATLETA\"" + "='" + user + "');";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                ChallengeResponseDto challenge = new()
                                {
                                    Nombre = (string)sdr["Nombre"],
                                    Kms_total=(int)sdr["Kms_total"],
                                    Finalizado = (bool)sdr["Finalizado"],
                                    Fecha_inicio=(DateTime)sdr["Fecha_inicio"],
                                    Fecha_fin=(DateTime)sdr["Fecha_fin"],
                                    Fondo_altura=(string)sdr["Fondo_altura"],
                                    Clase_actividad=(string)sdr["Clase_actividad"],

                                };
                                challenges.Add(challenge);
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
            return challenges;
        }

        /// <summary>
        /// Add an Activity to a Challenge in the database
        /// </summary>
        /// <param name="challengeId"><c>int</c>: Challenge Id</param>
        /// <param name="activityId"><c>int</c>: Activity Id</param>
        /// <returns><c>string</c> with the binding result</returns>
        public static string AddChallengeActivity(string user, string challengeId, string activityId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnection()))
                {
                    string query = @"INSERT INTO ACT_RETO (" + "\"ATLETA\"" + ", Nmbr_Reto, Nmbr_Actividad) " +
                                    "VALUES('" + user + "', '" + challengeId + "', '" + activityId + "');";
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



        private static string GetConnection()
        {
            return
                "Server=tcp:straviatec-server.database.windows.net,1433;Initial Catalog=StraviaTecDB-Azure;Persist Security Info=False;User ID=sqladmin;Password=root1234!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }
    }
}