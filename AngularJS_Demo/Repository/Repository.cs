using AngularJS_Demo.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AngularJS_Demo.Repository
{
    public class Repository : IRepository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString; // "Data Source=LAPTOP-JD2CG4N8;Initial Catalog=EmployeeDB;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";

        public string AddEmployee(EmployeeModel empDetails)
        {
            string response = "";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("InsertEmployee", conn);
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = empDetails.Name;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = empDetails.Email;
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                cmd.ExecuteNonQuery();
                response = "Success";
                conn.Close();
            }
            return response;
        }

        public string deleteEmployee(int id)
        {
            string response = "";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DeleteEmployee", conn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                cmd.ExecuteNonQuery();
                response = "Success";
                conn.Close();
            }
            return response;
        }

        public List<EmployeeModel> GetEmployees()
        {
            List<EmployeeModel> list = new List<EmployeeModel>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.SelectEmployee", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            list.Add(new EmployeeModel
                            {
                                Name = row["Name"].ToString().Trim(),
                                Email = row["Email"].ToString().Trim(),
                                ID = int.Parse(row["ID"].ToString())

                            });
                        }
                    }
                }

            }
            return list;
        }

        public string updateEmployee(EmployeeModel empdetails)
        {
            string response = "";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UpdateEmployee", conn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = empdetails.ID;
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = empdetails.Name;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = empdetails.Email;
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                cmd.ExecuteNonQuery();
                response = "Success";
                conn.Close();
            }
            return response;
        }
    }
}