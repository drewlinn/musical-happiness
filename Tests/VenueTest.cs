using Xunit;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace BandTracker
{
  [Collection("BandTracker")]
  public class VenueTest : IDisposable
  {
    public VenueTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb; Initial Catalog=band_tracker_test; Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Venue.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Override_ObjectsAreEqual()
    {
      //Arrange, Act
      Venue Venue1 = new Venue("The House of Rock", "1234 Street");
      Venue Venue2 = new Venue("The House of Rock", "1234 Street");
      //Assert
      Assert.Equal(Venue1, Venue2);
    }

    [Fact]
     public void Test_Save_SavesToDatabase()
     {
      //Arrange
     Venue testVenue = new Venue("The Arena", "456 El Segundo Way");

      //Act
      testVenue.Save();
      List<Venue> result =Venue.GetAll();
      List<Venue> testList = new List<Venue>{testVenue};

      //Assert
      Assert.Equal(testList, result);
     }

     [Fact]
    public void Test_Find_FindVenueInDatabase()
    {
      //Arrange
      Venue testVenue = new Venue("Morrison Hotel", "123 Love Street");
      testVenue.Save();

      //Act
      Venue foundVenue = Venue.Find(testVenue.GetId());

      //Assert
      Assert.Equal(testVenue, foundVenue);
    }

    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }
  }
}
