using ShmffPortal.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShmffPortal.DAL
{
    public class RegisterSSNRepository
    {
        private SqlConnection con;
        //To Handle connection related activities    
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnectionSMS"].ToString();
            con = new SqlConnection(constr);

        }
        //To Add Employee details    
        public bool AddEmployee(RegisterSSN obj)
        {

            connection();
            SqlCommand com = new SqlCommand("pr_RegisterSSN_Insert", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@sFullName", obj.FullName);
            com.Parameters.AddWithValue("@sSSN", obj.SSN);
            com.Parameters.AddWithValue("@sMobile", obj.Mobile);
            com.Parameters.AddWithValue("@sPassword", obj.Password);
            com.Parameters.AddWithValue("@sVerificationCode", obj.VerificationCode);
            com.Parameters.AddWithValue("@bVerified", obj.Verified);
            com.Parameters.AddWithValue("@biSALT", obj.SALT);
            com.Parameters.AddWithValue("@sToken", obj.Token);
            com.Parameters.AddWithValue("@iVerifyCount", obj.VerifyCount);
            com.Parameters.AddWithValue("@daLastPasswordRestore", obj.LastPasswordRestore);
            com.Parameters.AddWithValue("@iPasswordRestoreNumber", obj.PasswordRestoreNumber);
            com.Parameters.AddWithValue("@daRegisterDate", obj.RegisterDate);
            com.Parameters.AddWithValue("@iGander", obj.Gander);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {

                return true;

            }
            else
            {

                return false;
            }


        }
        ////To view employee details with generic list     
        //public List<EmpModel> GetAllEmployees()
        //{
        //    connection();
        //    List<EmpModel> EmpList = new List<EmpModel>();


        //    SqlCommand com = new SqlCommand("GetEmployees", con);
        //    com.CommandType = CommandType.StoredProcedure;
        //    SqlDataAdapter da = new SqlDataAdapter(com);
        //    DataTable dt = new DataTable();

        //    con.Open();
        //    da.Fill(dt);
        //    con.Close();
        //    //Bind EmpModel generic list using dataRow     
        //    foreach (DataRow dr in dt.Rows)
        //    {

        //        EmpList.Add(

        //            new EmpModel
        //            {

        //                Empid = Convert.ToInt32(dr["Id"]),
        //                Name = Convert.ToString(dr["Name"]),
        //                City = Convert.ToString(dr["City"]),
        //                Address = Convert.ToString(dr["Address"])

        //            }
        //            );
        //    }

        //    return EmpList;
        //}
        ////To Update Employee details    
        //public bool UpdateEmployee(EmpModel obj)
        //{

        //    connection();
        //    SqlCommand com = new SqlCommand("UpdateEmpDetails", con);

        //    com.CommandType = CommandType.StoredProcedure;
        //    com.Parameters.AddWithValue("@EmpId", obj.Empid);
        //    com.Parameters.AddWithValue("@Name", obj.Name);
        //    com.Parameters.AddWithValue("@City", obj.City);
        //    com.Parameters.AddWithValue("@Address", obj.Address);
        //    con.Open();
        //    int i = com.ExecuteNonQuery();
        //    con.Close();
        //    if (i >= 1)
        //    {

        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        ////To delete Employee details    
        //public bool DeleteEmployee(int Id)
        //{

        //    connection();
        //    SqlCommand com = new SqlCommand("DeleteEmpById", con);

        //    com.CommandType = CommandType.StoredProcedure;
        //    com.Parameters.AddWithValue("@EmpId", Id);

        //    con.Open();
        //    int i = com.ExecuteNonQuery();
        //    con.Close();
        //    if (i >= 1)
        //    {
        //        return true;
        //    }
        //    else
        //    {

        //        return false;
        //    }
        //}

    }
}
