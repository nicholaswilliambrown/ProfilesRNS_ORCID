using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Configuration;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data.Common;
using System.Web;

using Profiles.Framework.Utilities;

namespace Profiles.ORCID.Utilities
{
    public class DataIO : Profiles.Framework.Utilities.DataIO
    {

        public static int GetCommandTimeout()
        {
            return Convert.ToInt32(ConfigurationSettings.AppSettings["COMMANDTIMEOUT"]);

        }

        public SqlDataReader GetPublications(RDFTriple request)
        {
            SessionManagement sm = new SessionManagement();

            string connstr = ConfigurationManager.ConnectionStrings["ProfilesDB"].ConnectionString;
            SqlConnection dbconnection = new SqlConnection(connstr);
            SqlCommand dbcommand = new SqlCommand("[Profile.Module].[CustomViewAuthorInAuthorshipForORCID.GetList]");

            SqlDataReader dbreader;
            dbconnection.Open();
            dbcommand.CommandType = CommandType.StoredProcedure;
            dbcommand.CommandTimeout = base.GetCommandTimeout();
            dbcommand.Parameters.Add(new SqlParameter("@nodeid", request.Subject));
            dbcommand.Parameters.Add(new SqlParameter("@sessionid", sm.Session().SessionID));
            dbcommand.Connection = dbconnection;
            dbreader = dbcommand.ExecuteReader(CommandBehavior.CloseConnection);

            return dbreader;
        }
        public  string GetInternalUserID()
        {
            SessionManagement sm = new SessionManagement();

            string connstr = ConfigurationManager.ConnectionStrings["ProfilesDB"].ConnectionString;
            SqlConnection dbconnection = new SqlConnection(connstr);
            SqlCommand dbcommand = new SqlCommand("SELECT UserID, InternalUserName FROM [User.Account].[User] WHERE (UserID = @userid)");

            SqlDataReader dbreader;
            dbconnection.Open();
            dbcommand.CommandType = CommandType.Text;
            dbcommand.CommandTimeout = GetCommandTimeout();
            dbcommand.Parameters.Add(new SqlParameter("@userid", sm.Session().UserID));
            dbcommand.Connection = dbconnection;
            dbreader = dbcommand.ExecuteReader(CommandBehavior.CloseConnection);

            while (dbreader.Read())
            {
                ORCIDPublication pub = new ORCIDPublication();
                if (dbreader["InternalUserName"] != null)
                {
                    return dbreader["InternalUserName"].ToString();
                }
            }
            throw new Exception("Unable to find Internal Username");
        }
        public string GetInternalUserIDFromSubjectID()
        {
            SessionManagement sm = new SessionManagement();

            string connstr = ConfigurationManager.ConnectionStrings["ProfilesDB"].ConnectionString;
            SqlConnection dbconnection = new SqlConnection(connstr);
            SqlCommand dbcommand = new SqlCommand("SELECT UserID, InternalUserName FROM [User.Account].[User] WHERE (UserID = @userid)");

            SqlDataReader dbreader;
            dbconnection.Open();
            dbcommand.CommandType = CommandType.Text;
            dbcommand.CommandTimeout = base.GetCommandTimeout();
            dbcommand.Parameters.Add(new SqlParameter("@userid", sm.Session().UserID));
            dbcommand.Connection = dbconnection;
            dbreader = dbcommand.ExecuteReader(CommandBehavior.CloseConnection);

            while (dbreader.Read())
            {
                ORCIDPublication pub = new ORCIDPublication();
                if (dbreader["InternalUserName"] != null)
                {
                    return dbreader["InternalUserName"].ToString();
                }
            }
            throw new Exception("Unable to find Internal Username");
        }

        public  bool hasOrcid(string internalusername)
        {
            string connstr = ConfigurationManager.ConnectionStrings["ProfilesDB"].ConnectionString;
            SqlConnection dbconnection = new SqlConnection(connstr);
            SqlCommand dbcommand = new SqlCommand("SELECT ORCID FROM [ORCID].[Person] WHERE internalusername = \'" + internalusername + "\'");

            SqlDataReader dbreader;
            dbconnection.Open();
            dbcommand.CommandType = CommandType.Text;
            dbcommand.CommandTimeout = GetCommandTimeout();
            dbcommand.Connection = dbconnection;
            dbreader = dbcommand.ExecuteReader(CommandBehavior.CloseConnection);

            while (dbreader.Read())
            {
                ORCIDPublication pub = new ORCIDPublication();
                if (dbreader["ORCID"] != null)
                {
                    return true;
                }
            }
            return false;
        }

        public static string getOrcid(string internalusername)
        {
            string connstr = ConfigurationManager.ConnectionStrings["ProfilesDB"].ConnectionString;
            SqlConnection dbconnection = new SqlConnection(connstr);
            SqlCommand dbcommand = new SqlCommand("SELECT ORCID FROM [ORCID].[Person] WHERE internalusername = \'" + internalusername + "\'");

            SqlDataReader dbreader;
            dbconnection.Open();
            dbcommand.CommandType = CommandType.Text;
            dbcommand.CommandTimeout = GetCommandTimeout();
            dbcommand.Connection = dbconnection;
            dbreader = dbcommand.ExecuteReader(CommandBehavior.CloseConnection);

            while (dbreader.Read())
            {
                ORCIDPublication pub = new ORCIDPublication();
                if (dbreader["ORCID"] != null)
                {
                    return dbreader["ORCID"].ToString();
                }
            }
            return null;
        }


        public static bool AssociateORCIDWithOrganizationID(string internalusername, string orcid)
        {
            ProfilesRNSDLL.BO.ORCID.Person person = null;
            person.ORCID = orcid;
            person.PersonStatusTypeID = (int)ProfilesRNSDLL.BO.ORCID.REFPersonStatusType.REFPersonStatusTypes.ORCID_Provided;
            person.ORCIDRecorded = DateTime.Now;
             // PersonBLL.Save(person);


            string connstr = ConfigurationManager.ConnectionStrings["ProfilesDB"].ConnectionString;
            SqlConnection dbconnection = new SqlConnection(connstr);
            SqlCommand dbcommand = new SqlCommand("SELECT ORCID FROM [ORCID].[Person] WHERE internalusername = \'" + internalusername + "\'");

            SqlDataReader dbreader;
            dbconnection.Open();
            dbcommand.CommandType = CommandType.Text;
            dbcommand.CommandTimeout = GetCommandTimeout();
            dbcommand.Connection = dbconnection;
            dbreader = dbcommand.ExecuteReader(CommandBehavior.CloseConnection);

            while (dbreader.Read())
            {
                ORCIDPublication pub = new ORCIDPublication();
                if (dbreader["ORCID"] != null)
                {
                    //return dbreader["ORCID"].ToString();
                }
            }
            //return null;
            return true;
        }

    }
}