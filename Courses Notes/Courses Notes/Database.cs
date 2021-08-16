using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace Courses_Notes
{
    static class Database
    {
        static public Course Current_course;
        static private SQLiteConnection sql_con;
        static private SQLiteCommand sql_cmd;
        static private SQLiteDataAdapter DB;
        static private DataSet DS = new DataSet();

        static public void CreateTabels()
        {
            try { 
                ExecuteQuery("CREATE TABLE \"Courses\" (\"Id\"INTEGER NOT NULL UNIQUE, \"Name\"  TEXT NOT NULL, \"StartDate\" TEXT, \"EndDate\"   TEXT, \"Link\"  TEXT, \"Strike\"    INTEGER DEFAULT 0,PRIMARY KEY(\"Id\" AUTOINCREMENT) )\")");
                ExecuteQuery("CREATE TABLE \"learn\" (\"CourseId\"  INTEGER NOT NULL,\"Date\"  TEXT NOT NULL)");
            }
            catch {}
            
        }

        static public void setConnection()
        {
            sql_con = new SQLiteConnection
                ("Data Source=db.db;Version=3;New=False;Compress=True");
        }

        static public void ExecuteQuery(string txtQuery) // insert / delete etc..
        {
            setConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }

        static public DataTable loadData(string cmd) //Get the data table from any "select" query
        {
            DataTable DT = new DataTable();
            setConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            DB = new SQLiteDataAdapter(cmd, sql_con);
            DS.Reset();
            DB.Fill(DS);
            DT = DS.Tables[0];
            sql_con.Close();
            return DT;
        }
    }
}
