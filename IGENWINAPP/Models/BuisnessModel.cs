using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace IGENWINAPP.Models
{
    public class BuisnessModel
    {
        #region Login

        public List<Security202008> SelectUsers()
        {
            List<Security202008> Osecurity202008s = new List<Security202008>();

            using (SqlConnection con = new SqlConnection(GlobalData.ConString))
            {
                string query = "select * from Security202008";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Security202008 security202008 = new Security202008();
                    security202008.column85 = Convert.ToString(rdr["column85"]);
                    security202008.column87 = Convert.ToString(rdr["column87"]);
                    security202008.column88 = Convert.ToString(rdr["column88"]);
                    Osecurity202008s.Add(security202008);
                }
            }

            return Osecurity202008s;
        }

        public bool checkUserExists(string username, string password)
        {
            bool result = false;
            using (SqlConnection con = new SqlConnection(GlobalData.ConString))
            {
                string query = "select column85,column88 from Security202008 where column85 = @column85 and column88 = @column88";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("column85", username);
                cmd.Parameters.AddWithValue("column88", password);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                    result = true;
            }

            return result;
        }

        #endregion Login

        #region UserReport

        public List<UserReports> UserReports(string userId)
        {
            List<UserReports> uReports = new List<UserReports>();
            using (SqlConnection con = new SqlConnection(GlobalData.ConString))
            {
                
                string query = "select * from UserReports where UserId = @UserId";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("UserId", userId);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while(rdr.Read())
                {
                    UserReports userReport = new UserReports();

                    userReport.UserId = Convert.ToString(rdr["UserId"]);
                    userReport.ReportId = Convert.ToInt32(rdr["ReportId"]);

                    uReports.Add(userReport);
                }
                
            }
            return uReports;
        }

        #endregion UserReport

        #region setCustomMenuItem

        public List<CustomMenuItem> customMenuItems(string userId)
        {
            List<CustomMenuItem> customMenuItems = new List<CustomMenuItem>();
            using (SqlConnection con = new SqlConnection(GlobalData.ConString))
            {

                string query = "select A.[CategoryId],A.[CategoryName],B.[ReportId],B.[ReportName],C.[UserId] from [dbo].[UserReports] as C,[dbo].[ReportCategory] as A,[dbo].[Reports] as B where C.[ReportId] = B.[ReportId] and A.[CategoryId] = B.[CategoryId] and C.[UserId] = @UserId";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("UserId", userId);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    CustomMenuItem customMenuItem = new CustomMenuItem();


                    customMenuItem.CategoryId = Convert.ToInt32(rdr["CategoryId"]);
                    customMenuItem.CategoryName = Convert.ToString(rdr["CategoryName"]);
                    customMenuItem.ReportName = Convert.ToString(rdr["ReportName"]);
                    customMenuItem.UserId = Convert.ToString(rdr["UserId"]);
                    customMenuItem.ReportId = Convert.ToInt32(rdr["ReportId"]);

                    customMenuItems.Add(customMenuItem);
                }

            }
            return customMenuItems;
        }

        #endregion setCustomMenuItem

        #region getReportColoumn

        public List<ReportColoumnCustomModel> reportColoumnCustomModel(int reportID, int categoryID)
        {
            List<ReportColoumnCustomModel> reportColoumnCustomModels = new List<ReportColoumnCustomModel>();

            using (SqlConnection con = new SqlConnection(GlobalData.ConString))
            {
                string query = "select A.ReportID,A.CategoryId,A.ReportName,B.ViewId,C.ColumnName from [dbo].[Reports] as A, [dbo].[ReportViewColumns] as B,[dbo].[ViewColumns] as c where A.ReportId = @ReportId and A.CategoryId = @CategoryId and B.ReportId = A.ReportId and b.ColumnId = c.ColumnId";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("ReportId", reportID);
                cmd.Parameters.AddWithValue("CategoryId", categoryID);
                SqlDataReader rdr = cmd.ExecuteReader();

                while(rdr.Read())
                {
                    ReportColoumnCustomModel reportColoumnCustomModel = new ReportColoumnCustomModel();

                    reportColoumnCustomModel.ReportID = Convert.ToInt32(rdr["ReportID"]);
                    reportColoumnCustomModel.CategoryId = Convert.ToInt32(rdr["CategoryId"]);
                    reportColoumnCustomModel.ReportName = Convert.ToString(rdr["ReportName"]);
                    reportColoumnCustomModel.ViewId = Convert.ToInt32(rdr["ViewId"]);
                    reportColoumnCustomModel.ColumnName = Convert.ToString(rdr["ColumnName"]);

                    reportColoumnCustomModels.Add(reportColoumnCustomModel);

                }

            }

            return reportColoumnCustomModels;
        }

        #endregion getReportColoumn
    }
}