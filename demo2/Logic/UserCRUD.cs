using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using System.Data;
using System.Configuration;
using demo2.Models;

namespace demo2.Logic
{
    public class UserCRUD
    {
        private SqlConnection con;

        private void Connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            con = new SqlConnection(constr);
        }

        private String Query;
        public string AddUser(UserModel userModel)
        {
            try
            {
                Connection();
                con.Open();
                Query = "Insert into [User] (UserName, Password, Email, IsVerified) values (@UserName, @Password, @Email, @IsVerified)";
                userModel.IsVerified = false;
                var result = con.Execute(Query, new { UserName = userModel.UserName, Password = userModel.Password, Email = userModel.Email, IsVerified = userModel.IsVerified });
                return result.ToString();
            }
            catch (Exception ex)
            {
                string erMessage = ex.Message;
                return erMessage;
            }
            finally
            {
                con.Close();
            }
        }

        public List<UserModel> GetUserList()
        {
            try
            {
                Connection();
                con.Open();
                IList<UserModel> userlist = con.Query<UserModel>("Select * from [User]").ToList();
                return userlist.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public void UpdateUser(int Id, UserModel userModel)
        {
            try
            {
                Connection();
                con.Open();
                Query = "Update [User] set UserName = @UserName, Password = @Password, Email = @Email, IsVerified = @IsVerified where UserID = @UserID";
                con.Execute(Query, new { UserId = Id, UserName = userModel.UserName, Password = userModel.Password, Email = userModel.Email, IsVerified = userModel.IsVerified });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public bool DeleteUser(int Id)
        {
            bool flag = false;
            try
            {
                Connection();
                con.Open();
                Query = "Delete from [User] where UserID = @UserID";
                int result = con.Execute(Query, new { UserID = Id });
                if (result == 1)
                {
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return flag;
        }

    }
}