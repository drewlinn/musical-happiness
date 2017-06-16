using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace BandTracker
{
  public class Band
  {
    private int _id;
    private string _name;
    private string _genre;

    public Band(string name, string genre, int id = 0)
    {
      _name = name;
      _genre = genre;
      _id = id;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public string GetGenre()
    {
      return _genre;
    }

    public static List<Band> GetAll()
    {
      List<Band> AllBands = new List<Band>{};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM bands", conn);
      SqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string genre = rdr.GetString(2);
        Band newBand = new Band(name, genre, id);
        AllBands.Add(newBand);
      }
      if (rdr != null)
      {
       rdr.Close();
      }
      if (conn != null)
      {
       conn.Close();
      }
      return AllBands;
    }

    public static void DeleteAll()
    {
     SqlConnection conn = DB.Connection();
     conn.Open();
     SqlCommand cmd = new SqlCommand("DELETE FROM bands;", conn);
     cmd.ExecuteNonQuery();
     conn.Close();
    }
  }
}
