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

    public override bool Equals(System.Object otherBand)
   {
    if(!(otherBand is Band))
    {
      return false;
    }
    else
     {
      Band newBand = (Band) otherBand;
      bool idEquality = (this.GetId() == newBand.GetId());
      bool nameEquality = (this.GetName() == newBand.GetName());
      bool genreEquality = (this.GetGenre() == newBand.GetGenre());
      return (idEquality && nameEquality && genreEquality);
     }
   }

   public override int GetHashCode()
   {
     return this.GetName().GetHashCode();
   }

   public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO bands (name, genre) OUTPUT INSERTED.id VALUES (@name, @genre);", conn);

      SqlParameter namePara = new SqlParameter("@name", this.GetName());
      SqlParameter genrePara = new SqlParameter("@genre", this.GetGenre());

      cmd.Parameters.Add(namePara);
      cmd.Parameters.Add(genrePara);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static Band Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM bands WHERE id = @id;", conn);
      SqlParameter idParameter = new SqlParameter("@id", id.ToString());

      cmd.Parameters.Add(idParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundId = 0;
      string name = null;
      string genre = null;

      while(rdr.Read())
      {
        foundId = rdr.GetInt32(0);
        name = rdr.GetString(1);
        genre = rdr.GetString(2);
      }
      Band foundBand = new Band(name, genre, foundId);
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundBand;
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
