﻿using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Infrastructure
{
    public interface ICourtDAO
    {
        int Save(Court court, DbConnection dbConnection);
        int Update(Court court, DbConnection dbConnection);
        List<Court> GetCourtList(DbConnection dbConnection);
    }
    public class CourtSqlDAOImpl : ICourtDAO
    {
        public int Save(Court court, DbConnection dbConnection)
        {

            int output = 0;


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Insert into court (court_name) " +
                                           "values (@court_name) SELECT SCOPE_IDENTITY() ";


            //dbConnection.cmd.Parameters.AddWithValue("@CompanyId", company.CompanyId);
            dbConnection.cmd.Parameters.AddWithValue("@court_id", court.CourtId);
            dbConnection.cmd.Parameters.AddWithValue("@court_name", court.CourtName);


            output = Convert.ToInt32(dbConnection.cmd.ExecuteScalar());


            return output;
        }

        public int Update(Court court, DbConnection dbConnection)
        {

            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Update court set court_name = @court WHERE court_id = @courtId ";


            dbConnection.cmd.Parameters.AddWithValue("@courtId", court.CourtId);
            dbConnection.cmd.Parameters.AddWithValue("@court", court.CourtName);


            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }

        public List<Court> GetCourtList(DbConnection dbConnection)
        {
            List<Court> listCourt = new List<Court>();

            dbConnection.cmd.CommandText = "select * from court";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            listCourt = dataAccessObject.ReadCollection<Court>(dbConnection.dr);
            dbConnection.dr.Close();


            return listCourt;
        }
    }
}