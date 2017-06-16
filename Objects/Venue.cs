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

   public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO venues (name, address) OUTPUT INSERTED.id VALUES (@name, @address);", conn);

      SqlParameter namePara = new SqlParameter("@name", this.GetName());
      SqlParameter addressPara = new SqlParameter("@address", this.GetAddress());

      cmd.Parameters.Add(namePara);
      cmd.Parameters.Add(addressPara);

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

    public static Venue Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues WHERE id = @id;", conn);
      SqlParameter idParameter = new SqlParameter("@id", id.ToString());

      cmd.Parameters.Add(idParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundId = 0;
      string name = null;
      string address = null;

      while(rdr.Read())
      {
        foundId = rdr.GetInt32(0);
        name = rdr.GetString(1);
        address = rdr.GetString(2);
      }
      Venue foundVenue = new Venue(name, address, foundId);
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundVenue;
    }

    public void Update(string name, string address)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE venues SET name = @name, address = @address WHERE id = @Id;", conn);

      SqlParameter namePara = new SqlParameter("@name", name);
      SqlParameter addressPara = new SqlParameter("@address", address);
      SqlParameter idPara = new SqlParameter("@Id", this.GetId());

      cmd.Parameters.Add(namePara);
      cmd.Parameters.Add(addressPara);
      cmd.Parameters.Add(idPara);

      this._name = name;
      this._address = address;
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public List<Band> GetBands()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT bands.* FROM venues JOIN venues_bands ON (venues.id = venues_bands.venue_id) JOIN bands ON (venues_bands.band_id = bands.id) WHERE venues.id = @venueId;", conn);
      SqlParameter venueIdParam = new SqlParameter("@venueId", this.GetId().ToString());

      cmd.Parameters.Add(venueIdParam);

      SqlDataReader rdr = cmd.ExecuteReader();

      List<Band> bands = new List<Band>{};

      while(rdr.Read())
      {
        int bandId = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string genre = rdr.GetString(2);
        Band newBand = new Band(name, genre, bandId);
        bands.Add(newBand);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return bands;
    }

    //Add band's id and venue's id to venues_bands table
    public void AddBand(Band newBand)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO venues_bands (band_id, venue_id) VALUES (@BandId, @VenueId);", conn);

      SqlParameter bandIdParameter = new SqlParameter( "@BandId", newBand.GetId());
      SqlParameter venueIdParameter = new SqlParameter("@VenueId", this.GetId());

      cmd.Parameters.Add(bandIdParameter);
      cmd.Parameters.Add(venueIdParameter);
      cmd.ExecuteNonQuery();
      if (conn != null)
      {
        conn.Close();
      }
    }

    // public void Delete()
    // {
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //
    //   SqlCommand cmd = new SqlCommand("DELETE FROM venues WHERE id = @Id; DELETE FROM venues_bands WHERE venue_id = @Id;", conn);
    //   SqlParameter IdParameter = new SqlParameter("@Id", this.GetId());
    //
    //   cmd.Parameters.Add(IdParameter);
    //   cmd.ExecuteNonQuery();
    //
    //   if (conn != null)
    //   {
    //     conn.Close();
    //   }
    // }

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
