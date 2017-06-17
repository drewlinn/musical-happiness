using Xunit;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace BandTracker
{
  [Collection("BandTracker")]
  public class BandTest : IDisposable
  {
    public BandTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb; Initial Catalog=band_tracker_test; Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Band.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Override_ObjectsAreEqual()
    {
      //Arrange, Act
      Band Band1 = new Band("Black Sabbath", "Metal");
      Band Band2 = new Band("Black Sabbath", "Metal");
      //Assert
      Assert.Equal(Band1, Band2);
    }

    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      //Arrange
      Band testBand = new Band("A Tribe Called Quest", "Hip-Hop");

      //Act
      testBand.Save();
      List<Band> result =Band.GetAll();
      List<Band> testList = new List<Band>{testBand};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
      public void Test_Find_FindBandInDatabase()
      {
      //Arrange
      Band testBand = new Band("The Doors", "Classic Rock");
      testBand.Save();

      //Act
      Band foundBand = Band.Find(testBand.GetId());

      //Assert
      Assert.Equal(testBand, foundBand);
    }

    [Fact]
    public void Test_Update_UpdatesBandInDatabase()
    {
      //Arrange
      Band testBand = new Band("Animal Collective", "Experimental");
      testBand.Save();
      string newGenre = "Freak Folk";
      //Act
      testBand.Update("Animal Collective", "Freak Folk");
      string result = testBand.GetGenre();

      //Assert
      Assert.Equal(newGenre, result);
    }

    [Fact]
    public void GetVenues_ReturnsAllBandVenues_VenueList()
    {
      //Arrange
      Band testBand = new Band("Radiohead", "Experimental Rock");
      testBand.Save();

      Venue testVenues1 = new Venue("The Collosseum", "456 Great Big Way");
      testVenues1.Save();

      Venue testVenues2 = new Venue("ThunderDome", "129 BeyondThe Street");
      testVenues2.Save();

      //Act
      testBand.AddVenue(testVenues1);
      List<Venue> result = testBand.GetVenues();
      List<Venue> testList = new List<Venue> {testVenues1};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void AddVenue_AddsVenuesToBand_VenuesList()
    {
      //Arrange
      Band testBand = new Band("Tom Waits", "Just the best... like... ever");
      testBand.Save();

      Venue testVenues = new Venue("The Collosseum", "89 Out West Blvd");
      testVenues.Save();

      //Act
      testBand.AddVenue(testVenues);

      List<Venue> result = testBand.GetVenues();
      List<Venue> testList = new List<Venue>{testVenues};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Delete_DeletesBandsAssociationsFromDatabase_BandsList()
    {
      //Arrange
      Venue testVenue = new Venue("In The Venue", "39 Grand Central Way");
      testVenue.Save();

      Band testBands = new Band("Pinback", "Indie Rock");
      testBands.Save();

      //Act
      testBands.AddVenue(testVenue);
      testBands.Delete();

      List<Band> resultVenueBands = testVenue.GetBands();
      List<Band> testVenueBands = new List<Band> {};

      //Assert
      Assert.Equal(testVenueBands, resultVenueBands);
    }

    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
      Show.DeleteAll();
    }
  }
}
