using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace BandTracker
{
  public class Show
  {
    private int _id;
    private int _band_id;
    private int _venue_id;
    private DateTime _date;

    public Show(int band_id, int venue_id, DateTime date, int id = 0)
    {
      _band_id = band_id;
      _venue_id = venue_id;
      _date = date;
      _id = id;
    }

    public int GetId()
    {
      return _id;
    }
    public int GetBandId()
    {
      return _band_id;
    }
    public int GetVenueId()
    {
      return _venue_id;
    }
    public DateTime GetDate()
    {
      return _date;
    }

    public override bool Equals(System.Object otherShow)
    {
      if(!(otherShow is Show))
      {
        return false;
      }
      else
      {
        Show newShow = (Show) otherShow;
        bool idEquality = (this.GetId() == newShow.GetId());
        bool bandIdEquality = (this.GetBandId() == newShow.GetBandId());
        bool venueIdEquality = (this.GetVenueId() == newShow.GetVenueId());
        bool dateEquality = (this.GetDate() == newShow.GetDate());
        return (idEquality && bandIdEquality && venueIdEquality && dateEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetHashCode();
    }

    public static List<Show> GetAll()
    {
      List<Show> AllShows = new List<Show>{};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM shows", conn);
      SqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        int band_id = rdr.GetInt32(1);
        int venue_id = rdr.GetInt32(2);
        DateTime date = rdr.GetDateTime(3);
        Show newShow = new Show(band_id, venue_id, date, id);
        AllShows.Add(newShow);
      }
      if (rdr != null)
      {
       rdr.Close();
      }
      if (conn != null)
      {
       conn.Close();
      }
      return AllShows;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO shows (band_id, venue_id, date) OUTPUT INSERTED.id VALUES (@bandId, @venueId, @date);", conn);

      SqlParameter bandIdPara = new SqlParameter("@bandId", this.GetBandId());
      SqlParameter venueIdPara = new SqlParameter("@venueId", this.GetVenueId());
      SqlParameter datePara = new SqlParameter("@date", this.GetDate());

      cmd.Parameters.Add(bandIdPara);
      cmd.Parameters.Add(venueIdPara);
      cmd.Parameters.Add(datePara);

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

    public static Show Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM shows WHERE id = @id;", conn);
      SqlParameter idParameter = new SqlParameter("@id", id.ToString());

      cmd.Parameters.Add(idParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundId = 0;
      int bandId = 0;
      int venueId = 0;
      DateTime date = new DateTime();

      while(rdr.Read())
      {
        foundId = rdr.GetInt32(0);
        bandId = rdr.GetInt32(1);
        venueId = rdr.GetInt32(2);
        date = rdr.GetDateTime(3);
      }
      Show foundShow = new Show(bandId, venueId, date, foundId);
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundShow;
    }
    //
    public void Update(int bandId, int venueId, DateTime date)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE shows SET band_id = @bandId WHERE id = @Id; UPDATE shows SET venue_id = @venueId WHERE id = @Id; UPDATE shows SET date = @date WHERE id = @Id;", conn);

      SqlParameter bandIdPara = new SqlParameter("@bandId", bandId);
      SqlParameter venueIdPara = new SqlParameter("@venueId", venueId);
      SqlParameter datePara = new SqlParameter("@date", date);
      SqlParameter idPara = new SqlParameter("@Id", this.GetId());

      cmd.Parameters.Add(bandIdPara);
      cmd.Parameters.Add(venueIdPara);
      cmd.Parameters.Add(datePara);
      cmd.Parameters.Add(idPara);

      this._band_id = bandId;
      this._venue_id = venueId;
      this._date = date;
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    // public List<Band> GetBands()
    // {
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //
    //   SqlCommand cmd = new SqlCommand("SELECT bands.* FROM venues JOIN shows ON (venues.id = shows.venue_id) JOIN bands ON (shows.band_id = bands.id) WHERE venues.id = @venueId;", conn);
    //   SqlParameter venueIdParam = new SqlParameter("@venueId", this.GetId().ToString());
    //
    //   cmd.Parameters.Add(venueIdParam);
    //
    //   SqlDataReader rdr = cmd.ExecuteReader();
    //
    //   List<Band> bands = new List<Band>{};
    //
    //   while(rdr.Read())
    //   {
    //     int bandId = rdr.GetInt32(0);
    //     string name = rdr.GetString(1);
    //     string genre = rdr.GetString(2);
    //     Band newBand = new Band(name, genre, bandId);
    //     bands.Add(newBand);
    //   }
    //
    //   if (rdr != null)
    //   {
    //     rdr.Close();
    //   }
    //   if (conn != null)
    //   {
    //     conn.Close();
    //   }
    //   return bands;
    // }
    //
    // //Add band's id and venue's id to shows table
    // public void AddBand(Band newBand)
    // {
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //
    //   SqlCommand cmd = new SqlCommand("INSERT INTO shows (band_id, venue_id) VALUES (@BandId, @VenueId);", conn);
    //
    //   SqlParameter bandIdParameter = new SqlParameter( "@BandId", newBand.GetId());
    //   SqlParameter venueIdParameter = new SqlParameter("@VenueId", this.GetId());
    //
    //   cmd.Parameters.Add(bandIdParameter);
    //   cmd.Parameters.Add(venueIdParameter);
    //   cmd.ExecuteNonQuery();
    //   if (conn != null)
    //   {
    //     conn.Close();
    //   }
    // }
    //
    // public List<Venue> GetVenues()
    // {
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //
    //   SqlCommand cmd = new SqlCommand("SELECT venues.* FROM bands JOIN shows ON (bands.id = shows.band_id) JOIN venues ON (shows.venue_id = venues.id) WHERE bands.id = @BandsId;", conn);
    //   SqlParameter bandsIdParam = new SqlParameter("@BandsId", this.GetId().ToString());
    //
    //   cmd.Parameters.Add(bandsIdParam);
    //
    //   SqlDataReader rdr = cmd.ExecuteReader();
    //
    //   List<Venue> venues = new List<Venue>{};
    //
    //   while(rdr.Read())
    //   {
    //     int venueId = rdr.GetInt32(0);
    //     string name = rdr.GetString(1);
    //     string address = rdr.GetString(2);
    //     Venue newVenue = new Venue(name, address, venueId);
    //     venues.Add(newVenue);
    //   }
    //
    //   if (rdr != null)
    //   {
    //     rdr.Close();
    //   }
    //   if (conn != null)
    //   {
    //     conn.Close();
    //   }
    //   return venues;
    // }
    //
    // //Add band's id and venue's id to shows table
    // public void AddVenue(Venue newVenue)
    // {
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //
    //   SqlCommand cmd = new SqlCommand("INSERT INTO shows (band_id, venue_id) VALUES (@BandId, @VenueId);", conn);
    //
    //   SqlParameter bandIdParameter = new SqlParameter("@BandId", this.GetId());
    //   SqlParameter venueIdParameter = new SqlParameter( "@VenueId", newVenue.GetId());
    //
    //   cmd.Parameters.Add(bandIdParameter);
    //   cmd.Parameters.Add(venueIdParameter);
    //   cmd.ExecuteNonQuery();
    //   if (conn != null)
    //   {
    //     conn.Close();
    //   }
    // }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM shows WHERE id = @Id;", conn);
      SqlParameter idParameter = new SqlParameter("@Id", this.GetId());

      cmd.Parameters.Add(idParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
       conn.Close();
      }
    }


    public static void DeleteAll()
    {
     SqlConnection conn = DB.Connection();
     conn.Open();
     SqlCommand cmd = new SqlCommand("DELETE FROM shows;", conn);
     cmd.ExecuteNonQuery();
     conn.Close();
    }
  }
}
