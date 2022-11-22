﻿using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LegalSystemCore.Infrastructure
{

    public interface IUserLoginDAO
    {
        int Save(UserLogin userLogin, DbConnection dbConnection);
        int Update(UserLogin userLogin, DbConnection dbConnection);
        List<UserLogin> GetUserLoginList(DbConnection dbConnection);
    }

    public class UserLoginSqlDAOImpl : IUserLoginDAO
    {
        public int Save(UserLogin userLogin, DbConnection dbConnection)
        {

            int output = 0;


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Insert into user_login (user_name,password,company_id,company_unit_id,user_role_id) " +
                                           "values (@UserName,@Password,@CompanyId,@CompanyUnitId,@UserRoleId) SELECT SCOPE_IDENTITY() ";


            //dbConnection.cmd.Parameters.AddWithValue("@CompanyId", company.CompanyId);
            dbConnection.cmd.Parameters.AddWithValue("@UserName", userLogin.UserName);
            dbConnection.cmd.Parameters.AddWithValue("@Password", userLogin.Password);
            dbConnection.cmd.Parameters.AddWithValue("@CompanyId", userLogin.CompanyId);
            dbConnection.cmd.Parameters.AddWithValue("@CompanyUnitId", userLogin.CompanyUnitId);
            dbConnection.cmd.Parameters.AddWithValue("@UserRoleId", userLogin.UserRoleId);


            output = Convert.ToInt32(dbConnection.cmd.ExecuteScalar());


            return output;
        }

        public int Update(UserLogin userLogin, DbConnection dbConnection)
        {

            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Update user_login set user_name = @UserName ,password = @Password, company_id= @CompanyId, company_unit_id = @CompanyUnitId, user_role_id = @UserRoleId WHERE user_id = @UserId ";


            dbConnection.cmd.Parameters.AddWithValue("@UserId", userLogin.UserId);
            dbConnection.cmd.Parameters.AddWithValue("@UserName", userLogin.UserName);
            dbConnection.cmd.Parameters.AddWithValue("@Password", userLogin.Password);
            dbConnection.cmd.Parameters.AddWithValue("@CompanyId", userLogin.CompanyId);
            dbConnection.cmd.Parameters.AddWithValue("@CompanyUnitId", userLogin.CompanyUnitId);
            dbConnection.cmd.Parameters.AddWithValue("@UserRoleId", userLogin.UserRoleId);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }

        public List<UserLogin> GetUserLoginList(DbConnection dbConnection)
        {
            List<UserLogin> listUserLogin = new List<UserLogin>();

            dbConnection.cmd.CommandText = "select * from user_login";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            listUserLogin = dataAccessObject.ReadCollection<UserLogin>(dbConnection.dr);
            dbConnection.dr.Close();


            return listUserLogin;
        }
    }

}