using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;

namespace FirbaseAndroidPanel.Models
{
    public class MysqlConnector
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        private string query;
        DataTable data;
        public MysqlConnector()
        {
            Initialize();
        }


        private void Initialize()
        {
            server = "35.247.130.118";
            database = "vumate";
            uid = "song";
            password = "VuS@ngstar";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }


        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
              
                return false;
            }
        }


        public DataTable GetData(string ownerName, string songName)
        {
           
          
            if (ownerName != null && songName != null)
            {
                 query = "SELECT * FROM tbl_song song INNER JOIN tbl_owner ow ON song.OwnerId = ow.Id WHERE LCASE(ow.OwnerName) LIKE '%" + ownerName + "%' AND LCASE(song.SongName) LIKE '%" + songName + "%'";
            }

            else if (ownerName != null && songName == null)
            {
                query = "SELECT * FROM tbl_song song INNER JOIN tbl_owner ow ON song.OwnerId = ow.Id WHERE LCASE(ow.OwnerName) LIKE '%" + ownerName + "%'";
            }

            else if (ownerName == null && songName != null)
            {
                query = "SELECT * FROM tbl_song song INNER JOIN tbl_owner ow ON song.OwnerId = ow.Id WHERE LCASE(song.SongName) LIKE '%" + songName + "%'";
            }


            //Create a list to store the result
            string name="";
            //Open connection
            if (this.OpenConnection() == true)
            {

                MySqlCommand cmd = new MySqlCommand(query, connection);

                //  MySqlDataReader dataReader = cmd.ExecuteReader();

                try
                {
                    MySqlDataAdapter adr = new MySqlDataAdapter(cmd);
                    adr.SelectCommand.CommandType = CommandType.Text;
                    DataTable dt = new DataTable();
                    adr.Fill(dt); //opens and closes the DB connection automatically !! (fetches from pool)
                    name = dt.Rows[0][0].ToString();
                    data = dt;
                }
                catch (Exception ex) { }




                DataSet ds = new DataSet();
                //SqlDataAdapter da = new SqlDataAdapter();
                //da.Fill(ds);
                //name = dataReader["Name"].ToString();



                //dataReader.Close();


                this.CloseConnection();


                return data;
            }
            else {

                return data;

            }
           
        }


    }
}