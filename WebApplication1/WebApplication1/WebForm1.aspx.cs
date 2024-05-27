using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1;

namespace WebApplication2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        new List<String> userNamesFromDatabase = new List<string>();
        int Active = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
          
         
            if (!IsPostBack)
               {
                string sessionId = Session.SessionID;

                // Znajdź użytkownika dla bieżącej sesji
                User currentUser = Global.userList.FirstOrDefault(u => u.SessionId == sessionId);

                if (currentUser != null)
                {
                    // Ustawienie wartości wybranego użytkownika w usersDropDownList
                    usersDropDownList.DataSource = Global.userList;
                    usersDropDownList.DataTextField = "UserName";
                    usersDropDownList.DataBind();

                    // Wybierz nazwę aktualnie zalogowanego użytkownika
                    usersDropDownList.Items.FindByValue(currentUser.UserName).Selected = true;
                }


                time.Visible = false;
                RadioList.SelectedValue = "mały";
                rozmiarDropDownList.SelectedValue = "5";
                i1.Visible = true;
                i2.Visible = false;
                i3.Visible = false;
                i4.Visible = false;
                i5.Visible = true;
                i6.Visible = false;
                i7.Visible = false;
                i8.Visible = false;
                i9.Visible = true;
                RFS();
               
                time.Text = DateTime.Now.ToString(daytimeformat);
                strona.Visible = false;
         
            }
        }
        protected string daytimeformat = "HH:mm:ss.fffffff";
        protected void ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            UPD(); 
            string sessionId = Session.SessionID;

          
                if (((ImageButton)sender).ImageUrl == "zdjecia/SLN.jpeg")
                {
                    string connectionString1 = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\studiasemestr5programy\pai\lab1r\WebApplication1\WebApplication1\App_Data\Database1.mdf;Integrated Security=True"; ; // Zastąp odpowiednimi danymi dostępowymi do twojej bazy danych

                    using (SqlConnection connection = new SqlConnection(connectionString1))
                    {
                        string updateQuery = "UPDATE UserData1 SET ImageClicks = ImageClicks + 1 WHERE SessionId = @SessionId";

                        SqlCommand command = new SqlCommand(updateQuery, connection);

                        command.Parameters.AddWithValue("@SessionId", sessionId);
                   

                        try
                        {
                            connection.Open();

                            command.ExecuteNonQuery();

                           
                        }
                        catch (Exception ex)
                        {
                        Debug.WriteLine(ex.Message);
                    }
                    }
                }
            

            if (((ImageButton)sender).ImageUrl != "zdjecia/SLN.jpeg")
                return;

            RFS();
            string postbacktime = DateTime.Now.ToString(daytimeformat);
            DateTime zeroTime = DateTime.ParseExact("00:00:00.0000000", daytimeformat, null);

            TimeSpan ts_postbacktime = DateTime.ParseExact(postbacktime, daytimeformat, null) - zeroTime;
            TimeSpan ts_loadtime = DateTime.ParseExact(time.Text, daytimeformat, null) - zeroTime;
            TimeSpan ts_mintime = DateTime.ParseExact(min.Text, daytimeformat, null) - zeroTime;
            TimeSpan ts_maxtime = DateTime.ParseExact(max.Text, daytimeformat, null) - zeroTime;

            TimeSpan diff = (ts_postbacktime - ts_loadtime);

            ts_mintime = diff < ts_mintime ? diff : ts_mintime;
            ts_maxtime = diff > ts_maxtime ? diff : ts_maxtime;

            min.Text = ts_mintime.ToString();
            max.Text = ts_maxtime.ToString();
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\studiasemestr5programy\pai\lab1r\WebApplication1\WebApplication1\App_Data\Database1.mdf;Integrated Security=True"; ; // Zastąp odpowiednimi danymi dostępowymi do twojej bazy danych // Zastąp odpowiednimi danymi dostępowymi

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string selectQuery = "SELECT * FROM UserData1 WHERE SessionId = @SessionId"; // Zmodyfikuj zapytanie do swojej bazy danych

                SqlCommand command = new SqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@SessionId", sessionId);

                try
                {
                    connection.Open();
                    SqlDataAdapter reader = new SqlDataAdapter();
                    reader.SelectCommand = command;
                    DataTable tab = new DataTable();
                    reader.Fill(tab);
                    connection.Close();
                    TimeSpan ts_mintime1 = DateTime.ParseExact(tab.Rows[0]["MinTime"].ToString().Trim(), daytimeformat, null) - zeroTime;
                    TimeSpan ts_maxtime1= DateTime.ParseExact(tab.Rows[0]["MaxTime"].ToString().Trim(),daytimeformat, null) - zeroTime;

                    ts_mintime = ts_mintime1 < ts_mintime ? ts_mintime1 : ts_mintime;
                    ts_maxtime = ts_maxtime1 > ts_maxtime ? ts_maxtime1 : ts_maxtime;






                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            time.Text = DateTime.Now.ToString(daytimeformat);
            string connectionString2 = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\studiasemestr5programy\pai\lab1r\WebApplication1\WebApplication1\App_Data\Database1.mdf;Integrated Security=True"; // Zastąp odpowiednimi danymi dostępowymi do twojej bazy danych
            // Aktualizacja maksymalnego i minimalnego czasu w bazie danych
            string updateMinMaxQuery = "UPDATE UserData1 SET MaxTime = @MaxTime, MinTime = @MinTime WHERE SessionId = @SessionId";

            using (SqlConnection connection = new SqlConnection(connectionString2))
            {
                SqlCommand command = new SqlCommand(updateMinMaxQuery, connection);

                command.Parameters.AddWithValue("@SessionId", sessionId);

                command.Parameters.AddWithValue("@MaxTime", ts_maxtime.ToString());
                command.Parameters.AddWithValue("@MinTime", ts_mintime.ToString());

                try
                {
                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Pomyślnie zaktualizowano wartości MaxTime i MinTime w bazie danych dla użytkownika
                    }
                    else
                    {
                        // Błąd aktualizacji wartości MaxTime i MinTime
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }




        protected void RadioList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UPD();

            string selectedValue = RadioList.SelectedValue;
            string sessionId = Session.SessionID;

            // Znajdź użytkownika dla bieżącej sesji
            //User currentUser = Global.userList.FirstOrDefault(u => u.SessionId == sessionId);
       
            

           
                // Zwiększ wartość SizeChanges dla bieżącego użytkownika sesji o 1 w bazie danych

                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\studiasemestr5programy\pai\lab1r\WebApplication1\WebApplication1\App_Data\Database1.mdf;Integrated Security=True"; // Zastąp odpowiednimi danymi dostępowymi do twojej bazy danych

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string updateQuery = "UPDATE UserData1 SET SizeChanges = SizeChanges + 1 WHERE SessionId = @SessionId";

                    SqlCommand command = new SqlCommand(updateQuery, connection);

                    command.Parameters.AddWithValue("@SessionId", sessionId);
            

                    try
                    {
                        connection.Open();

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Pomyślnie zaktualizowano wartość SizeChanges w bazie danych dla użytkownika
                        }
                        else
                        {
                            // Błąd aktualizacji wartości SizeChanges
                        }
                    }
                    catch (Exception ex)
                    {
                    Debug.WriteLine(ex.Message);
                }
                }
            

            if (selectedValue == "mały")
            {
                for (int i = 1; i <= 9; i++)
                {
                    string controlName = "i" + i.ToString();
                    Control control = FindControl(controlName);

                    // Sprawdź typ kontrolki i zmień jej rozmiar w zależności od typu
                    if (control is Image)
                    {
                        Image img = (Image)control;
                        img.Width = 50;
                        img.Height = 50;
        
                    }
                    else if (control is Panel)
                    {
                        Panel panel = (Panel)control;
                        panel.Width = 50;
                        panel.Height = 50;
                      
                    }
                    // Dodaj więcej warunków dla innych typów kontrolek, jeśli są używane
                }
            }

            else if (selectedValue == "średni")
            {
                for (int i = 1; i <= 9; i++)
                {
                    string controlName = "i" + i.ToString();
                    Control control = FindControl(controlName);

                    // Sprawdź typ kontrolki i zmień jej rozmiar w zależności od typu
                    if (control is Image)
                    {
                        Image img = (Image)control;
                        img.Width = 75;
                        img.Height = 75;
                       
                    }
                    else if (control is Panel)
                    {
                        Panel panel = (Panel)control;
                        panel.Width = 75;
                        panel.Height = 75;
                  
                    }
                    // Dodaj więcej warunków dla innych typów kontrolek, jeśli są używane
                }
            }
            else if (selectedValue == "duży")
            {
                for (int i = 1; i <= 9; i++)
                {
                    string controlName = "i" + i.ToString();
                    Control control = FindControl(controlName);

                    // Sprawdź typ kontrolki i zmień jej rozmiar w zależności od typu
                    if (control is Image)
                    {
                        Image img = (Image)control;
                        img.Width = 100;
                        img.Height = 100;
                      
                    }
                    else if (control is Panel)
                    {
                        Panel panel = (Panel)control;
                        panel.Width = 100;
                        panel.Height = 100;
                       
                    }

                  
                }
            }
            min.Text = "23:59:59.9999999";
            max.Text = "00:00:00.0000000";
        }
        protected void RFS()
        {
            string selectedValue = rozmiarDropDownList.SelectedValue;
            

            List<Image> pictures = new List<Image> { i1, i2, i3, i4, i5, i6, i7, i8, i9 };


            List<int> indicesToShow = new List<int>();

            if (selectedValue == "1")
            {
                indicesToShow.AddRange(new List<int> { 1, 4, 7 });

            }
            else if (selectedValue == "2")
            {
                indicesToShow.AddRange(new List<int> { 3, 4, 5 });

            }
            else if (selectedValue == "3")
            {
                indicesToShow.AddRange(new List<int> { 2, 4, 6 });

            }
            else if (selectedValue == "4")
            {
                indicesToShow.AddRange(new List<int> { 0, 4, 8 });

            }
            else if (selectedValue == "5")
            {
                indicesToShow.AddRange(new List<int> { 0, 2, 4, 6, 8 });

            }
            else { return; }

            foreach (Image element in pictures)
            {
                element.Visible = false;
                element.ImageUrl = "zdjecia/moon.jpg";
            };
            Random rnd = new Random();
            int t1;
            int t2;
            int t3;
            pictures[t3=indicesToShow[t2=rnd.Next(t1=indicesToShow.Count)]].ImageUrl = "zdjecia/SLN.jpeg";
            Debug.WriteLine($"{t1}---------{t2}---------{t3}") ;
            // Resetowanie widoczności przed wyświetleniem nowych elementów

            foreach (int index in indicesToShow)
            {
                pictures[index].Visible = true;
            }
  
        }
        protected void rozmiarDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UPD();
            RFS();
            min.Text = "23:59:59.9999999";
            max.Text = "00:00:00.0000000";

            string sessionId = Session.SessionID;
            

            // Znajdź użytkownika dla bieżącej sesji
            //User currentUser = Global.userList.FirstOrDefault(u => u.SessionId == sessionId);
         
                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\studiasemestr5programy\pai\lab1r\WebApplication1\WebApplication1\App_Data\Database1.mdf;Integrated Security=True"; // Zastąp odpowiednimi danymi dostępowymi do twojej bazy danych
                                                                                                                                                                                                                      // Zastąp odpowiednimi danymi dostępowymi do twojej bazy danych

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string updateQuery = "UPDATE UserData1 SET PatternChange = PatternChange + 1 WHERE SessionId = @SessionId";

                    SqlCommand command = new SqlCommand(updateQuery, connection);

                    command.Parameters.AddWithValue("@SessionId", sessionId);

                    try
                    {
                        connection.Open();

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Pomyślnie zaktualizowano wartość SizeChanges w bazie danych dla użytkownika
                        }
                        else
                        {
                            // Błąd aktualizacji wartości SizeChanges
                        }
                    }
                    catch (Exception ex)
                    {
                    Debug.WriteLine(ex.Message);
                }
                }

            

        }
        protected void rejestrButton_Click(object sender, EventArgs e)
        {
            UPD();

            string userName = HttpContext.Current.Request.Form["dane"];
            string sessionId = Session.SessionID;
            userlab.Text = userName;

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\studiasemestr5programy\pai\lab1r\WebApplication1\WebApplication1\App_Data\Database1.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string selectQuery = "SELECT Log FROM UserData1 WHERE UserName = @UserName";

                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@UserName", userName);

                try
                {
                    connection.Open();

                   Object userCount = (object)selectCommand.ExecuteScalar();

                    if (userCount == null)
                    {
                        log.Visible = false;
                        strona.Visible = true;

                        string insertQuery = "INSERT INTO UserData1 (UserName, SessionId, ImageClicks, SizeChanges, PatternChange, Log, MaxTime, MinTime) " +
                                             "VALUES (@UserName, @SessionId, @ImageClicks, @SizeChanges, @PatternChange, @Log, @MaxTime, @MinTime)";

                        SqlCommand insertCommand = new SqlCommand(insertQuery, connection);

                        insertCommand.Parameters.AddWithValue("@UserName", userName);
                        insertCommand.Parameters.AddWithValue("@SessionId", sessionId);
                        insertCommand.Parameters.AddWithValue("@ImageClicks", 0);
                        insertCommand.Parameters.AddWithValue("@SizeChanges", 0);
                        insertCommand.Parameters.AddWithValue("@PatternChange", 0);
                        insertCommand.Parameters.AddWithValue("@Log", 1);
                        insertCommand.Parameters.AddWithValue("@MaxTime", "23:59:59.9999999");
                        insertCommand.Parameters.AddWithValue("@MinTime", "00:00:00.0000000");

                        insertCommand.ExecuteNonQuery();
                        connection.Close();
                    }
                    else if((int)userCount == 0)
                    {
                        log.Visible = false;
                        strona.Visible = true;

                        string insertQuery = "UPDATE UserData1 SET SessionId = @SessionId, Log = 1 WHERE UserName = @UserName"; 
                                             

                        SqlCommand insertCommand = new SqlCommand(insertQuery, connection);

                        insertCommand.Parameters.AddWithValue("@UserName", userName);
                        insertCommand.Parameters.AddWithValue("@SessionId", sessionId);
          

                        insertCommand.ExecuteNonQuery();
                        connection.Close();
                    }
                    else
                    {
                        error1.IsValid = false;
                    }
                
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                
            }
          
        }
   
       

        protected void usersDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UPD();
          
        }
        protected void DTVUpdate()
        {
            string actual = usersDropDownList.SelectedValue;

            // Pobierz dane użytkownika z bazy danych na podstawie wybranej nazwy
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\studiasemestr5programy\pai\lab1r\WebApplication1\WebApplication1\App_Data\Database1.mdf;Integrated Security=True"; ; // Zastąp odpowiednimi danymi dostępowymi do twojej bazy danych // Zastąp odpowiednimi danymi dostępowymi

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string selectQuery = "SELECT * FROM UserData1 WHERE UserName = @UserName"; // Zmodyfikuj zapytanie do swojej bazy danych

                SqlCommand command = new SqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@UserName", actual);

                try
                {
                    connection.Open();
                    SqlDataAdapter reader = new SqlDataAdapter();
                    reader.SelectCommand = command;
                    DataTable tab = new DataTable();
                    tab.Columns.Add("zalogowani");
                    reader.Fill(tab);
                    tab.Columns.Remove("Id");
                    tab.Columns.Remove("UserName");
                    tab.Columns.Remove("SessionId");
                    tab.Columns.Remove("Log");
                    tab.Rows[0]["zalogowani"] = Active;
                   
                    

               
                    
                        userDetailsView.DataSource = tab;
                        userDetailsView.DataBind();
                    

                
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            UPD();    
        }

        protected void UPD()
        {
          
            string poprzednioWybrany = usersDropDownList.SelectedValue;

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\studiasemestr5programy\pai\lab1r\WebApplication1\WebApplication1\App_Data\Database1.mdf;Integrated Security=True";  // Zastąp odpowiednimi danymi dostępowymi do twojej bazy danych // Zastąp odpowiednimi danymi dostępowymi

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string selectQuery = "SELECT UserName FROM UserData1 WHERE Log = 1";

                SqlCommand command = new SqlCommand(selectQuery, connection);

                try
                {
                    connection.Open();
                    SqlDataAdapter reader = new SqlDataAdapter();
                    reader.SelectCommand= command;
                    DataTable dane = new DataTable();
                    reader.Fill(dane);
                    Active = dane.Rows.Count;
                    
                    foreach(DataRow r in  dane.Rows)
                    {
                        string userName = r["UserName"].ToString().Trim();
                        userNamesFromDatabase.Add(userName);
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            // Odśwież listę użytkowników w DropDownLiscie
            usersDropDownList.DataSource = userNamesFromDatabase;
            usersDropDownList.DataBind();

            // Ponownie wybierz zapisaną wartość
            if (!string.IsNullOrEmpty(poprzednioWybrany))
            {
                ListItem item = usersDropDownList.Items.FindByText(poprzednioWybrany);
                if (item != null)
                {
                    item.Selected = true;
                }
            }
            DTVUpdate();
        }

        protected void resetButton_Click(object sender, EventArgs e)
        {
            // Zerowanie danych użytkownika, z wyjątkiem nazwy użytkownika i ID sesji
            string sessionId = Session.SessionID;


            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\studiasemestr5programy\pai\lab1r\WebApplication1\WebApplication1\App_Data\Database1.mdf;Integrated Security=True";  // Zastąp odpowiednimi danymi dostępowymi do twojej bazy danych // Zastąp odpowiednimi danymi dostępowymi

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string updateQuery = "UPDATE UserData1 SET ImageClicks = 0, SizeChanges = 0, PatternChange = 0, Log = 1, MaxTime = @MaxTime, MinTime = @MinTime WHERE SessionId = @SessionId";

                SqlCommand command = new SqlCommand(updateQuery, connection);

                command.Parameters.AddWithValue("@SessionId", sessionId);
                command.Parameters.AddWithValue("@MaxTime", TimeSpan.Zero);
                command.Parameters.AddWithValue("@MinTime", TimeSpan.Zero);

                try
                {
                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Pomyślnie zresetowano dane użytkownika (poza nazwą użytkownika i ID sesji)
                    }
                    else
                    {
                        // Błąd przy zerowaniu danych użytkownika
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }




    }
}