﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServices.Services.Models;
using System.Configuration;
using UserServices.Services.Utilities;

namespace UserServices.Services
{
    public class DepartmentService
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["UserManagement"].ConnectionString;

        public List<DropDownInfo> GetAllForDropDown()
        {
            var dropDownInfos = new List<DropDownInfo>();

            using (var connection = new SqlConnection(WebConfigHelper.ConnectionString))
            {
                const string cmdText = "Departments_GetAllForDropDown";

                using (var command = new SqlCommand(cmdText, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var dropDownInfo = new DropDownInfo
                            {
                                Value = (int)reader["ID"],
                                Text = reader["Name"].GetDataFromDb<string>()
                            };

                            dropDownInfos.Add(dropDownInfo);
                        }
                    }
                }
            }
            return dropDownInfos;
        }

        public List<Department> GetAll()
        {
            var departments = new List<Department>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                const string cmdText = "Department_GetAll";

                using (var command = new SqlCommand(cmdText, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var department = new Department
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                Description = (string)reader["Description"]
                            };

                            departments.Add(department);
                        }
                    }
                }
            }

            return departments;
        }

        public void Add(Department department)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                const string cmdText = "Department_Add";

                using (var command = new SqlCommand(cmdText, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Name", department.Name);
                    command.Parameters.AddWithValue("@Description", department.Description);

                    connection.Open();

                    var rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected != 1)
                        throw new Exception("Add returned 0 rows affected. Expecting 1 rows affected");
                }
            }
        }

        public void Update(Department department)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                const string cmdText = "Department_Update";

                using (var command = new SqlCommand(cmdText, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", department.Id);
                    command.Parameters.AddWithValue("@Name", department.Name);
                    command.Parameters.AddWithValue("@Description", department.Description);

                    connection.Open();

                    var rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected != 1)
                        throw new Exception("Add returned 0 rows affected. Expecting 1 rows affected");
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                const string cmdText = "Department_Delete";

                using (var command = new SqlCommand(cmdText, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();

                    var rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected != 1)
                        throw new Exception("Add returned 0 rows affected. Expecting 1 rows affected");
                }
            }
        }

        public Department GetById(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                const string cmdText = "Department_GetById";

                using (var command = new SqlCommand(cmdText, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var department = new Department
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                Description = (string)reader["Description"]
                            };
                            return department;
                        }
                    }
                }
            }

            return null;
        }
    }
}
