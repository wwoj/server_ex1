using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DataLayer.Models;

namespace DataLayer
{
    public class TasksRepository
    {
        private const string CONNECTION_STRING = "Data Source=DESKTOP-47LTS4F\\SQLEXPRESS;Initial Catalog=ToDoApp;Integrated Security=True";

        public ToDoTask GetTask()
        {
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = @"SELECT TOP (1) [Id]
      ,[Name]
      ,[State]
        FROM[ToDoApp].[dbo].[Tasks]";
                command.Connection = conn;
                conn.Open();

                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                var taskResult = new ToDoTask();
                taskResult.Id = (int)reader["id"];
                taskResult.Name = (string)reader["name"];
                taskResult.Status = (string)reader["state"];
                return taskResult;
            }
        }

        public List<ToDoTask> GetAllTasks()
        {
            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = @"SELECT *
                                        FROM[ToDoApp].[dbo].[Tasks]";
                command.Connection = con;
                con.Open();

                SqlDataReader reader = command.ExecuteReader();

                var result = new List<ToDoTask>();
                while (reader.Read())
                {
                    var taskResult = new ToDoTask();
                    taskResult.Id = (int)reader["id"];
                    taskResult.Name = (string)reader["name"];
                    taskResult.Status = (string)reader["state"];
                    result.Add(taskResult);
                }

                return result;
            }

        }
        public int AddTask(string name, string description)
        {
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "INSERT INTO Tasks(Name, State)VALUES(@name,@description)";
                command.Connection = conn;
                conn.Open();

                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@description", description);
                int result = command.ExecuteNonQuery();
                return result;
            }
            
        }

        public int EditTask(int id,string name, string description)
        {
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "UPDATE Tasks SET Name = @name, State = @description WHERE Id = @id; ";
                command.Connection = conn;
                conn.Open();

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@description", description);
                int result = command.ExecuteNonQuery();
                return result;
            }

        }
        public int DeleteTask(int id)
        {
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "DELETE FROM Tasks WHERE Id=@id;";
                command.Connection = conn;
                conn.Open();

                command.Parameters.AddWithValue("@id", id);
                int result = command.ExecuteNonQuery();
                return result;
            }

        }


    }
}
