    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Linq;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.Security;
    using System.Web.SessionState;
    using System.Web.UI;

    namespace WebApplication1
    {
    
        public class User
        {
       
            public string SessionId { get; set; }
            public string UserName { get; set; }

            public User( string sessionId, string userName)
            {
          
                SessionId = sessionId;
                UserName = userName;
           
            }

    }


        public class Global : System.Web.HttpApplication
        {
            public static List<User> userList = new List<User>();
    
            protected void Application_Start(object sender, EventArgs e)
            {
           
            }

            protected void Session_Start(object sender, EventArgs e)
            {
            string sessionId = Session.SessionID;
            Debug.WriteLine("sesja startuje " + sessionId);

        }

            protected void Application_BeginRequest(object sender, EventArgs e)
            {

            }

            protected void Application_AuthenticateRequest(object sender, EventArgs e)
            {

            }

            protected void Application_Error(object sender, EventArgs e)
            {

            }


        protected void Session_End(object sender, EventArgs e)
        {
            string sessionId = Session.SessionID;
            Debug.WriteLine("Session ended: " + sessionId);
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\studiasemestr5programy\pai\lab1r\WebApplication1\WebApplication1\App_Data\Database1.mdf;Integrated Security=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string updateQuery = "UPDATE UserData1 SET Log = 0, SessionId = NULL WHERE SessionId = @SessionId";

                    SqlCommand command = new SqlCommand(updateQuery, connection);
                    command.Parameters.AddWithValue("@SessionId", sessionId);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    Debug.WriteLine("Rows affected: " + rowsAffected);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                // Handle the exception as needed - log, notify, etc.
            }
        }



        protected void Application_End(object sender, EventArgs e)
        {
            
        }


    }
}