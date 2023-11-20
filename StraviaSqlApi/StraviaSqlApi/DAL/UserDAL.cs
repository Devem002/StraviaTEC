using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using StraviaSqlApi.DTOs;
using StraviaSqlApi.DTOs.Requests;
using StraviaSqlApi.DTOs.Responses;

namespace StraviaSqlApi.DAL
{
    public static class UserDAL
    {
        public static List<UserResponseDto> GetUsers()
        {
            List<UserResponseDto> users = new();
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
                                UserResponseDto user = new()
                                {
                                    User = (string)sdr["Usuario"],
                                    FirstName = (string)sdr["Nombre"],
                                    LastName1 = (string)sdr["Apellido_1"],
                                    LastName2 = (string)sdr["Apellido_2"],
                                    BirthDate = (DateTime)sdr["Fecha_nacimiento"],
                                    Picture = (string)sdr["Picture"],
                                    Nationality = (string)sdr["Nacionalidad"],
                                    Clasificacion = (string)sdr["Clasificacion"]
                                };
                                users.Add(user);
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
            return users;
        }

        public static UserResponseDto GetUser(string userId)
        {
            UserResponseDto user = new() { User = "" };
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
                                user.User = (string)sdr["Usuario"];
                                user.FirstName = (string)sdr["Nombre"];
                                user.LastName1 = (string)sdr["Apellido_1"];
                                user.LastName2 = (string)sdr["Apellido_2"];
                                user.BirthDate = (DateTime)sdr["Fecha_nacimiento"];
                                user.Picture = (string)sdr["Foto"];
                                user.Nationality = (string)sdr["Nacionalidad"];
                                user.Clasificacion = (string)sdr["Clasificacion"];
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

        public static string UpdateUser(UserRegisterDto user, string currenPassword)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnection()))
                {
                    string procedure = @"UpdateAtleta";
                    using (SqlCommand cmd = new SqlCommand(procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Usuario", user.User);
                        cmd.Parameters.AddWithValue("@Nombre", user.FirstName);
                        cmd.Parameters.AddWithValue("@Apellido_1", user.LastName1);
                        cmd.Parameters.AddWithValue("@Apellido_2", user.LastName2);
                        cmd.Parameters.AddWithValue("@Fecha_nacimiento", user.BirthDate.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@Contrasena", currenPassword);
                        cmd.Parameters.AddWithValue("@NewPassword", user.Password);
                        cmd.Parameters.AddWithValue("@Foto", user.Picture);
                        cmd.Parameters.AddWithValue("@Nacionalidad", user.Nationality);
                        cmd.Parameters.AddWithValue("@Clasificacion", user.Clasificacion);
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

        public static List<FriendsFrontPage> GetFriendsFrontPageDB(string user)
        {
            List<FriendsFrontPage> friends = new();
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnection()))
                {
                    string procedure = @"ActividadRecienteAmigos";
                    using (SqlCommand cmd = new SqlCommand(procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Usuario", user);
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                FriendsFrontPage friend = new()
                                {
                                    Usuario = (string)sdr["Usuario"],
                                    Nombre = (string)sdr["Nombre"],
                                    Apellido_1 = (string)sdr["Apellido_1"],
                                    Apellido_2 = (string)sdr["Apellido_2"],
                                    Nmbr_Actividad = (string)sdr["Nmbr_Actividad"],
                                    Clase_Actividad = (string)sdr["Clase_Actividad"],
                                    Hora = (DateTime)sdr["Hora"],
                                    Recorrido_gpx = (string)sdr["Recorrido_gpx"],
                                    Kms_hechos = (int)sdr["Kms_hechos"]
                                };
                                friends.Add(friend);
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
            return friends;
        }

        public static List<UserResponseDto> GetFriendsAvailable(string myuser)
        {
            List<UserResponseDto> users = new();
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnection()))
                {
                    string procedure = @"AmigosDisponibles";
                    using (SqlCommand cmd = new SqlCommand(procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Usuario", myuser);
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                UserResponseDto user = new()
                                {
                                    User = (string)sdr["Usuario"],
                                    FirstName = (string)sdr["Nombre"],
                                    LastName1 = (string)sdr["Apellido_1"],
                                    BirthDate = (DateTime)sdr["Fecha_nacimiento"],
                                    Picture = (string)sdr["Foto"]
                                };
                                users.Add(user);
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
            return users;
        }


        public static List<UserResponseDto> GetFriends(string myuser)
        {
            List<UserResponseDto> users = new();
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnection()))
                {
                    string procedure = @"getAmigos";
                    using (SqlCommand cmd = new SqlCommand(procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Usuario", myuser);
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                UserResponseDto user = new()
                                {
                                    User = (string)sdr["Usuario"],
                                    FirstName = (string)sdr["Nombre"],
                                    LastName1 = (string)sdr["Apellido_1"],
                                    BirthDate = (DateTime)sdr["Fecha_nacimiento"],
                                    Picture = (string)sdr["Foto"],
                                };
                                users.Add(user);
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
            return users;
        }

        public static Boolean DeleteFriend(string user, string friend)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnection()))
                {
                    string query = @"DELETE FROM AMIGOS WHERE " + "\"Usuario\" = '" + user + "' AND Amigo='" + friend + "';";
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
                return false;
            }
            return true;
        }

        public static List<GroupDTO> GetGroupsAvailable(string user)
        {
            List<GroupDTO> groups = new();
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnection()))
                {
                    string procedure = @"GruposDisponibles";
                    using (SqlCommand cmd = new SqlCommand(procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Usuario", user);
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                GroupDTO group = new()
                                {
                                    AdminUser = (string)sdr["Atleta_admin"],
                                    Name = (string)sdr["Nombre"]
                                };
                                groups.Add(group);
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
            return groups;
        }


        public static List<GroupDTO> GetGroups(string user)
        {
            List<GroupDTO> groups = new();
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnection()))
                {
                    string procedure = @"getGrupos";
                    using (SqlCommand cmd = new SqlCommand(procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Usuario" +
                            "", user);
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                GroupDTO group = new()
                                {
                                    AdminUser = (string)sdr["Atleta_admin"],
                                    Name = (string)sdr["Nombre"]
                                };
                                groups.Add(group);
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
            return groups;
        }




        public static string RegisterUserDB(UserRegisterDto user)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnection()))
                {
                    string procedure = @"Registrar";
                    using (SqlCommand cmd = new SqlCommand(procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Usuario", user.User);
                        cmd.Parameters.AddWithValue("@Contrasena", user.Password);
                        cmd.Parameters.AddWithValue("@Foto", user.Picture);
                        cmd.Parameters.AddWithValue("@Nombre", user.FirstName);
                        cmd.Parameters.AddWithValue("@Apellido_1", user.LastName1);
                        cmd.Parameters.AddWithValue("@Apellido_2", user.LastName2);
                        cmd.Parameters.AddWithValue("@Fecha_nacimiento", user.BirthDate);
                        cmd.Parameters.AddWithValue("@Nacionalidad", user.Nationality);
                        cmd.Parameters.AddWithValue("@Clasificacion", user.Clasificacion);
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

        public static string LoginUserDB(UserLoginDto user)
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

        public static string AddActivity(ActivityDto act)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnection()))
                {
                    string procedure = @"AddActividad";
                    using (SqlCommand cmd = new SqlCommand(procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nmbr_Actividad", act.UserId);
                        cmd.Parameters.AddWithValue("@Fecha", act.Distance);
                        cmd.Parameters.AddWithValue("@Hora", act.Duration.ToString("HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@Kms_hechos", act.Route);
                        cmd.Parameters.AddWithValue("@Duracion", act.Start.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@Recorrido_gpx", act.Type);
                        cmd.Parameters.AddWithValue("@Atleta", act.RoC);
                        cmd.Parameters.AddWithValue("@Clase_actividad", act.RoC);
                        cmd.Parameters.AddWithValue("@Car_Ret", act.RoC);
                        cmd.Parameters.AddWithValue("@Car_Ret_Nmbr", act.RoCName);

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
        /**/

        public static List<ActivityResponseDto> GetuserActivities(string name)
        {
            List<ActivityResponseDto> Activities = new();
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnection()))
                {
                    string query = @"SELECT * FROM ACTIVIDAD WHERE Atleta ='" + name + "';";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        SqlDataReader sdr = cmd.ExecuteReader();
                        while (sdr.Read())
                        {
                            ActivityResponseDto activity = new()
                            {
                                ActivityId= (int)sdr["Nmbr_Actividad"],
                                UserId = (string)sdr["Fecha"],
                                Distance = (Decimal)sdr["Hora"],
                                Duration = (TimeSpan)sdr["Kms_hechos"],
                                Route = (string)sdr["Duracion"],
                                Start = (DateTime)sdr["Recorrido_gpx"],
                                Type = (string)sdr["Atleta"],
                                Clase_actividad = (string)sdr["Clase_actividad"]
                            };
                            Activities.Add(activity);
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
            return Activities;
        }

        public static string AddFriend(string user, string friend)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnection()))
                {
                    string query = @"INSERT INTO AMIGOS(" + "\"Usuario\"" + ", Amigo) " +
                                    "VALUES('" + user + "', '" + friend + "');";
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

        public static string CreateGroup(string admin, string name)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnection()))
                {
                    string query = @"INSERT INTO GRUPOS(Atleta_admin, " + "\"Nombre\"" + ") " +
                                    "VALUES('" + admin + "', '" + name + "');";
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

        public static string JoinGroup(string id, string user)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnection()))
                {
                    string query = @"INSERT INTO INTEGRANTES(Nmbr_grupo, " + "\"Integrante\"" + ") " +
                                    "VALUES(" + id + ", '" + user + "');";
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

        public static string QuitGroup(int id, string user)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnection()))
                {
                    string query = @"DELETE FROM INTEGRANTES WHERE" + "\"Integrante\"" + "='" + user +
                                    "'AND Nmbr_grupo=" + id + ";";
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

        public static string UpdateGroup(GroupDTO group, string newNombre)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnection()))
                {
                    string query = @"UPDATE GRUPOS SET Atleta_admin = '" + group.AdminUser +
                                    "', " + "\"Nombre\"" + "= '" + newNombre +
                                    "' WHERE Id = " + group.Name + ";";
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


        private static string GetConnection()
        {
            return "Server=tcp:straviatec-server.database.windows.net,1433;Initial Catalog=StraviaTECDB;Persist Security Info=False;User ID=sqladmin;Password=root1234!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }
    }
}