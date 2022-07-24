using DataAcessLayer.Models.NationModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAcessLayer.Repositories.NationRepository
{
    public class NationRepository : INationRepository
    {
        private string _sqlConnectionString { get; }
        public NationRepository(string connectionString)
        {
            _sqlConnectionString = connectionString;
        }

        public Nation GetNationById(int id)
        {
            Nation nation = null;

            using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetNationById", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@ID",
                    Value = id
                });

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            nation = new Nation
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                PathToLogo = reader.GetString(2)
                            };
                        }
                    }
                }
            }

            return nation;
        }

        public List<Nation> GetAllNations()
        {
            List<Nation> nations = new List<Nation>();

            using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetAllNations", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            nations.Add(new Nation
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                PathToLogo = reader.GetString(2),
                            });
                        }
                    }
                }
            }

            return nations;
        }
    }
}
