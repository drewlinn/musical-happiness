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

    [Fact]
    public void Test_Update_UpdatesVenueInDatabase()
    {
      //Arrange
      Venue testVenue = new Venue("Roseland Theatre", "789 Rose Street");
      testVenue.Save();
      string newAddress = "987 New Street";
      //Act
      testVenue.Update("Roseland Theatre", "987 New Street");
      string result = testVenue.GetAddress();

      //Assert
      Assert.Equal(newAddress, result);
    }

    [Fact]
    public void GetBands_ReturnsAllVenueBand_BandList()
    {
     //Arrange
     Venue testVenue = new Venue("Burt's Tiki Lounge", "1200 State St.");
     testVenue.Save();

     Band testBand1 = new Band("Townes Van Zandt", "Country/Blues");
     testBand1.Save();

     Band testBand2 = new Band("Nick Drake", "Folk/Blues");
     testBand2.Save();

     //Act
     testVenue.AddBand(testBand1);
     List<Band> savedBand = testVenue.GetBands();
     List<Band> testList = new List<Band> {testBand1};

     //Assert
     Assert.Equal(testList, savedBand);
    }

    [Fact]
    public void Test_AddBand_AddsBandToVenue()
    {
      //Arrange
      Venue testVenue = new Venue("The Doug Fir", "2014 W. Burnside");
      testVenue.Save();

      Band testBand = new Band("Nick Cave and the Bad Seeds", "Art Rock");
      testBand.Save();

      Band testBand2 = new Band("Washed Out", "Chill Wave");
      testBand2.Save();

      //Act
      testVenue.AddBand(testBand);
      testVenue.AddBand(testBand2);

      List<Band> result = testVenue.GetBands();
      List<Band> testList = new List<Band>{testBand, testBand2};

      //Assert
      Assert.Equal(testList, result);
    }


    [Fact]
    public void Delete_DeletesVenueAssociationsFromDatabase_VenueList()
    {
      //Arrange
      Band testBand = new Band("Explosions in the Sky", "Post-Rock");
      testBand.Save();

      Venue testVenue = new Venue("Skybar", "890 Sky Way");
      testVenue.Save();

      //Act
      testVenue.AddBand(testBand);
      testVenue.Delete();

      List<Venue> resultBandVenue = testBand.GetVenues();
      List<Venue> testBandVenue = new List<Venue> {};

      //Assert
      Assert.Equal(testBandVenue, resultBandVenue);
    }

    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
      Show.DeleteAll();
    }
  }
}
