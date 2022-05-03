using DataAcessLayer.Models.UserModels;
using System;
using System.Collections.Generic;
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

            var id = (int?)dataID;

            if (id == null)
                return false;

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

            var id = (int?)dataID;

            if (id == null)
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
                                PasswordHash = (byte[])reader.GetValue(2),
                                PasswordSalt = (Guid)reader.GetValue(3),
                                Email = (string)reader.GetValue(4),
                                UserLogo = (string)reader.GetValue(5)
                            };
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
                                PasswordHash = (byte[])reader.GetValue(2),
                                PasswordSalt = (Guid)reader.GetValue(3),
                                Email = (string)reader.GetValue(4),
                                UserLogo = (string)reader.GetValue(5),
                                Battles = (int)reader.GetValue(6),
                                WinBattles = (int)reader.GetValue(7),
                                WinRate = (float)reader.GetValue(8)
                            };
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

        public void ChangePassword(int id, byte[] passwordHash, Guid individualSalt)
        {
            using (SqlConnection connection =  new SqlConnection(_sqlConnectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("ChangePassword", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddRange(new List<SqlParameter> 
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
                }
                .ToArray());

                cmd.ExecuteNonQuery();
            }
        }
    }
}
