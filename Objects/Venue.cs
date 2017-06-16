using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace BandTracker
{
  public class Venue
  {
    private int _id;
    private string _name;
    private string _address;

    public Venue (string name, string address, int id = 0)
    {
      _name = name;
      _address = address;
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
    public string GetAddress()
    {
      return _address;
    }

    public static List<Venue> GetAll()
    {
      List<Venue> AllVenues = new List<Venue>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string address = rdr.GetString(2);
        Venue newVenue = new Venue(name, address, id);
        AllVenues.Add(newVenue);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return AllVenues;
    }

    public override bool Equals(System.Object otherVenue)
   {
    if(!(otherVenue is Venue))
    {
      return false;
    }
    else
     {
      Venue newVenue = (Venue) otherVenue;
      bool idEquality = (this.GetId() == newVenue.GetId());
      bool nameEquality = (this.GetName() == newVenue.GetName());
      bool addressEquality = (this.GetAddress() == newVenue.GetAddress());
      return (idEquality && nameEquality);
     }
   }

   public override int GetHashCode()
   {
     return this.GetName().GetHashCode();
   }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM venues;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
