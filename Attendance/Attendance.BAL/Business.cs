using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Attendance.Entities;
using System.Net;
using System.Configuration;


namespace Attendance.BAL
{
    public class Business
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["AttendanceConn"].ToString();


        public DataSet GetLocationNameByIpAdress(string IPAddress)
        {
            try
            {


                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter daLocation = new SqlDataAdapter("USP_GetLocationByIPAdress", connectionString);
                daLocation.SelectCommand.CommandType = CommandType.StoredProcedure;
                daLocation.SelectCommand.Parameters.Add(new SqlParameter("@IPAddress", IPAddress));
                DataSet dsLocation = new DataSet();
                daLocation.Fill(dsLocation);
                return dsLocation;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public DataSet GetUTCDatetime()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter daDateTime = new SqlDataAdapter("USP_GetUTCDatetime", connectionString);
                daDateTime.SelectCommand.CommandType = CommandType.StoredProcedure;

                DataSet dsDateTime = new DataSet();
                daDateTime.Fill(dsDateTime);
                return dsDateTime;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LiginDet()
        {

        }
        public DataSet BindData(string LocationName)
        {
            try
            {

                //   SqlCommand cmd = new SqlCommand();
                DataSet dsImage = new DataSet();

                SqlDataAdapter daPhotos = new SqlDataAdapter("[USP_GetPhotoByIPAdressId1]", connectionString);
                //  cmd.Parameters.AddWithValue("@locationname", LocationName);
                daPhotos.SelectCommand.Parameters.Add(new SqlParameter("@locationname", LocationName));
                daPhotos.SelectCommand.CommandType = CommandType.StoredProcedure;

                daPhotos.Fill(dsImage);
                return dsImage;

                //dlEmp.DataSource = dsImage;
                //dlEmp.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BindLogin()
        {
            try
            {
                SqlDataAdapter daPhotos = new SqlDataAdapter("USP_GetPhotoByLogin1", connectionString);
                //daPhotos.SelectCommand.Parameters.Add(new SqlParameter("@UserID", entities.UserID));
                //daPhotos.SelectCommand.Parameters.Add(new SqlParameter("@LoginDate", Convert.ToDateTime((entities.LoginDate))));
                //daPhotos.SelectCommand.Parameters.Add(new SqlParameter("@LoginNotes", entities.LoginNotes));
                daPhotos.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataSet dsImage = new DataSet();
                daPhotos.Fill(dsImage);
                return dsImage;

                //dlEmp.DataSource = dsImage;
                //dlEmp.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BindLogout()
        {
            try
            {

                SqlDataAdapter daPhotos = new SqlDataAdapter("USP_GetPhotoByLogout1", connectionString);
                daPhotos.SelectCommand.CommandType = CommandType.StoredProcedure;

                DataSet dsImage = new DataSet();

                daPhotos.Fill(dsImage);
                return dsImage;

                //dlEmp.DataSource = dsImage;
                //dlEmp.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet SaveLogDetails(Attendance.Entities.Entities entities)
        {
            DataSet dsLoginDet = new DataSet();
            try
            {
                SqlDataAdapter daLogin = new SqlDataAdapter("[USP_AddLogin1]", connectionString);
                daLogin.SelectCommand.Parameters.Add(new SqlParameter("@UserID", entities.UserID));
                daLogin.SelectCommand.Parameters.Add(new SqlParameter("@passcode", entities.passcode));
                daLogin.SelectCommand.Parameters.Add(new SqlParameter("@LoginDate", Convert.ToDateTime((entities.LoginDate))));
                daLogin.SelectCommand.Parameters.Add(new SqlParameter("@LoginNotes", entities.LoginNotes));
                daLogin.SelectCommand.CommandType = CommandType.StoredProcedure;
                daLogin.Fill(dsLoginDet);
                return dsLoginDet;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsLoginDet;


        }
        public DataSet SaveLogDetails1(Attendance.Entities.Entities entities, int UserLogID)
        {
            DataSet dsLogoutDet = new DataSet();
            try
            {
                SqlDataAdapter daLogout = new SqlDataAdapter("[USP_AddLogout1]", connectionString);
                daLogout.SelectCommand.Parameters.Add(new SqlParameter("@UserID", entities.UserID));
                daLogout.SelectCommand.Parameters.Add(new SqlParameter("@loguserID", UserLogID));
                daLogout.SelectCommand.Parameters.Add(new SqlParameter("@passcode", entities.passcode));
                daLogout.SelectCommand.Parameters.Add(new SqlParameter("@LogOutDate", entities.LogOutDate));
                daLogout.SelectCommand.Parameters.Add(new SqlParameter("@LogOutNotes", entities.LogOutNotes));
                daLogout.SelectCommand.CommandType = CommandType.StoredProcedure;
                daLogout.Fill(dsLogoutDet);
                return dsLogoutDet;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        //public DataSet SaveLogoutDetails(Attendance.Entities.Entities objEntity, int UserLogID)
        //{
        //    DataSet dsLogoutDet = new DataSet();
        //    try
        //    {
        //        SqlDataAdapter daLogout = new SqlDataAdapter("[USP_AddLogout]", connectionString);
        //        daLogout.SelectCommand.Parameters.Add(new SqlParameter("@UserID", objEntity.UserID));
        //        daLogout.SelectCommand.Parameters.Add(new SqlParameter("@loguserID", UserLogID));
        //        daLogout.SelectCommand.Parameters.Add(new SqlParameter("@LogOutDate", objEntity.LogOutDate));
        //        daLogout.SelectCommand.Parameters.Add(new SqlParameter("@LogOutNotes", objEntity.LogOutNotes));
        //        daLogout.SelectCommand.CommandType = CommandType.StoredProcedure;
        //        daLogout.Fill(dsLogoutDet);
        //        return dsLogoutDet;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

    }
}
