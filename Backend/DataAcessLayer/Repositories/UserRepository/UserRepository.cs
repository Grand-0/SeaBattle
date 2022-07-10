using DataAcessLayer.Models.UserModels;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAcessLayer.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private string _sqlConnectionString { get; }
        public UserRepository(string connectionString)
        {
            _sqlConnectionString = connectionString;
        }

        public bool isUserExist(string email)
        {
            object dataID;

            using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("CheckEmail", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter emailParam = new SqlParameter
                {
                    ParameterName = "@Email",
                    Value = email
                };

                cmd.Parameters.Add(emailParam);

                SqlParameter IdParam = new SqlParameter
                {
                    ParameterName = "@ID",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };

                cmd.Parameters.Add(IdParam);

                cmd.ExecuteNonQuery();

                dataID = cmd.Parameters["@ID"].Value;
            }

            if (dataID == DBNull.Value)
            {
                return false;
            }

            return true;
        }

        public bool isUserExistLogin(string login)
        {
            object dataID;

            using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("CheckLogin", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter loginParameter = new SqlParameter
                {
                    ParameterName = "@Login",
                    Value = login
                };

                cmd.Parameters.Add(loginParameter);

                SqlParameter IdParam = new SqlParameter
                {
                    ParameterName = "@ID",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };

                cmd.Parameters.Add(IdParam);

                cmd.ExecuteNonQuery();

                dataID = cmd.Parameters["@ID"].Value;
            }

            if (dataID == DBNull.Value)
            {
                return false;
            }

            return true;
        }

        public ReducedUser GetUserById(int id)
        {
            ReducedUser user = null;

            using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetUserByID", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter idParameter = new SqlParameter
                {
                    ParameterName = "@ID",
                    Value = id
                };

                cmd.Parameters.Add(idParameter);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            user = new ReducedUser
                            {
                                UserId = (int?)reader.GetValue(0),
                                Login = (string)reader.GetValue(1),
                                PasswordHash = (string)reader.GetValue(2),
                                PasswordSalt = (Guid)reader.GetValue(3),
                                Email = (string)reader.GetValue(4)
                            };

                            if (reader.GetValue(5) != DBNull.Value)
                            {
                                user.UserLogo = (string)reader.GetValue(5);
                            }
                        }
                    }
                }
            }

            if (user == null)
            {
                throw new Exception("SqlException!");
            }

            return user;
        }

        public UserWithStatistic GetUserWithStatisticById(int id)
        {
            UserWithStatistic user = null;

            using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetFullUserByID", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter idParameter = new SqlParameter
                {
                    ParameterName = "@ID",
                    Value = id
                };

                cmd.Parameters.Add(idParameter);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            user = new UserWithStatistic
                            {
                                UserId = (int?)reader.GetValue(0),
                                Login = (string)reader.GetValue(1),
                                PasswordHash = (string)reader.GetValue(2),
                                PasswordSalt = (Guid)reader.GetValue(3),
                                Email = (string)reader.GetValue(4),
                                Battles = (int)reader.GetValue(6),
                                WinBattles = (int)reader.GetValue(7),
                                WinRate = (float)reader.GetValue(8)
                            };

                            if (reader.GetValue(5) != DBNull.Value)
                            {
                                user.UserLogo = (string)reader.GetValue(5);
                            }
                        }
                    }
                }
            }

            if (user == null)
            {
                throw new Exception("SqlException!");
            }

            return user;
        }

        public void ChangePassword(int id, string passwordHash, Guid individualSalt)
        {
            using (SqlConnection connection =  new SqlConnection(_sqlConnectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("ChangePassword", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddRange(new SqlParameter[] 
                {
                    new SqlParameter
                    {
                        ParameterName = "@ID",
                        Value = id
                    },
                    new SqlParameter
                    {
                        ParameterName = "@PasswordHash",
                        Value = passwordHash
                    },
                    new SqlParameter
                    {
                        ParameterName = "@PasswordSalt",
                        Value = individualSalt
                    }
                });

                cmd.ExecuteNonQuery();
            }
        }

        public void CreateUser(ReducedUser user)
        {
            using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("InsertNewUser", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddRange(new SqlParameter[]
                {
                    new SqlParameter
                    {
                        ParameterName = "@Login",
                        Value = user.Login
                    },
                    new SqlParameter
                    {
                        ParameterName = "@PasswordHash",
                        Value = user.PasswordHash
                    },
                    new SqlParameter
                    {
                        ParameterName = "@PasswordSalt",
                        Value = user.PasswordSalt
                    },
                    new SqlParameter
                    {
                        ParameterName = "@Email",
                        Value = user.Email
                    }
                });

                cmd.ExecuteNonQuery();
            }
        }

        public ReducedUser GetUserByLogin(string login)
        {
            ReducedUser user = null;

            using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetUserByLogin", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter loginParameter = new SqlParameter
                {
                    ParameterName = "@Login",
                    Value = login
                };

                cmd.Parameters.Add(loginParameter);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            user = new ReducedUser
                            {
                                UserId = (int?)reader.GetValue(0),
                                Login = (string)reader.GetValue(1),
                                PasswordHash = (string)reader.GetValue(2),
                                PasswordSalt = (Guid)reader.GetValue(3),
                                Email = (string)reader.GetValue(4),
                            };

                            if (reader.GetValue(5) != DBNull.Value)
                            {
                                user.UserLogo = (string)reader.GetValue(5);
                            }
                        }
                    }
                }
            }

            return user;
        }

        public int? GetUserIdByLogin(string login)
        {
            int? userId = null;

            using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetUserIdByLogin", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter loginParameter = new SqlParameter
                {
                    ParameterName = "@Login",
                    Value = login,
                };

                cmd.Parameters.Add(loginParameter);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            userId = (int?)reader.GetValue(0);
                        }
                    }
                }
            }

            return userId;
        }

        public void UpdateFullUser(UpdatedFullUser user)
        {
            using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("UpdateFullUser", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddRange(new SqlParameter[]
                {
                    new SqlParameter
                    {
                        ParameterName = "@ID",
                        Value = user.UserId,
                    },
                    new SqlParameter
                    {
                        ParameterName = "@Login",
                        Value = user.Login,
                    },
                    new SqlParameter
                    {
                        ParameterName = "@Email",
                        Value = user.Email,
                    },
                    new SqlParameter
                    {
                        ParameterName = "@PathToLogo",
                        Value = user.PathToLogo
                    }
                });


                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateLoginAndEmail(int id, string login, string email)
        {
            using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("UpdateUserLoginAndEmail", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddRange(new SqlParameter[] {
                    new SqlParameter
                    {
                        ParameterName = "@ID",
                        Value = id,
                    },
                    new SqlParameter
                    {
                        ParameterName = "@Login",
                        Value = login,
                    },
                    new SqlParameter
                    {
                        ParameterName = "@Email",
                        Value = email,
                    }
                });

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateLoginAndLogo(int id, string login, string pathToLogo)
        {
            using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("UpdateUserLoginAndLogo", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddRange(new SqlParameter[] {
                    new SqlParameter
                    {
                        ParameterName = "@ID",
                        Value = id,
                    },
                    new SqlParameter
                    {
                        ParameterName = "@Login",
                        Value = login,
                    },
                    new SqlParameter
                    {
                        ParameterName = "@PathToLogo",
                        Value = pathToLogo,
                    }
                });

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateEmailAndLogo(int id, string email, string pathToLogo)
        {
            using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("UpdateUserEmailAndLogo", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddRange(new SqlParameter[] {
                    new SqlParameter
                    {
                        ParameterName = "@ID",
                        Value = id,
                    },
                    new SqlParameter
                    {
                        ParameterName = "@Email",
                        Value = email,
                    },
                    new SqlParameter
                    {
                        ParameterName = "@PathToLogo",
                        Value = pathToLogo,
                    }
                });

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateLogin(int Id, string login)
        {
            using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("UpdateUserLogin", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddRange(new SqlParameter[] {
                    new SqlParameter
                    {
                        ParameterName = "@ID",
                        Value = Id,
                    },
                    new SqlParameter
                    {
                        ParameterName = "@Login",
                        Value = login,
                    }
                });

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateEmail(int Id, string email)
        {
            using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("UpdateUserEmail", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddRange(new SqlParameter[] {
                    new SqlParameter
                    {
                        ParameterName = "@ID",
                        Value = Id
                    },
                    new SqlParameter
                    {
                        ParameterName = "@Email",
                        Value = email
                    }
                });

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateLogo(int Id, string pathToLogo)
        {
            using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("UpdateUserLogo", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddRange(new SqlParameter[] {
                    new SqlParameter
                    {
                        ParameterName = "@ID",
                        Value = Id
                    },
                    new SqlParameter
                    {
                        ParameterName = "@PathToLogo",
                        Value = pathToLogo
                    }
                });

                cmd.ExecuteNonQuery();
            }
        }
    }
}
