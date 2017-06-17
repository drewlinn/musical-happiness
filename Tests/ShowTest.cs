using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  [Collection("BandTracker")]
  public class ShowTest : IDisposable
  {
    public ShowTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb; Initial Catalog=band_tracker_test; Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
     //Arrange, Act
     int result = Show.GetAll().Count;

     //Assert
     Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Override_ObjectsAreEqual()
    {
      //Arrange, Act
      Show Show1 = new Show(1, 5, new DateTime(1851, 11, 14));
      Show Show2 = new Show(1, 5, new DateTime(1851, 11, 14));
      //Assert
      Assert.Equal(Show1, Show2);
    }

    [Fact]
    public void Test_Save_SavesToDatabase()
    {
     //Arrange
    Show testShow = new Show(7, 8, new DateTime(2016, 04, 04));

     //Act
     testShow.Save();
     List<Show> result = Show.GetAll();
     List<Show> testList = new List<Show>{testShow};

     //Assert
     Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Find_FindShowInDatabase()
    {
      //Arrange
      Show testShow = new Show(5, 6, new DateTime(2016, 04, 04));
      testShow.Save();

      //Act
      Show foundShow = Show.Find(testShow.GetId());

      //Assert
      Assert.Equal(testShow, foundShow);
    }

    [Fact]
    public void Test_Update_UpdatesShowInDatabase()
    {
      //Arrange
      Show testShow = new Show(25, 27, new DateTime(2016, 04, 23));
      testShow.Save();
      DateTime newDateTime = new DateTime(2016, 04, 30);
      //Act
      testShow.Update(25, 27, new DateTime(2016, 04, 30));
      DateTime result = testShow.GetDate();

      //Assert
      Assert.Equal(newDateTime, result);
    }

    // [Fact]
    // public void GetBands_ReturnsAllVenueBand_BandList()
    // {
    //  //Arrange
    //  Venue testVenue = new Venue("Burt's Tiki Lounge", "1200 State St.");
    //  testVenue.Save();
    //
    //  Band testBand1 = new Band("Townes Van Zandt", "Country/Blues");
    //  testBand1.Save();
    //
    //  Band testBand2 = new Band("Nick Drake", "Folk/Blues");
    //  testBand2.Save();
    //
    //  //Act
    //  testVenue.AddBand(testBand1);
    //  List<Band> savedBand = testVenue.GetBands();
    //  List<Band> testList = new List<Band> {testBand1};
    //
    //  //Assert
    //  Assert.Equal(testList, savedBand);
    // }
    // [Fact]
    // public void GetVenues_ReturnsAllBandVenues_VenueList()
    // {
    //   //Arrange
    //   Band testBand = new Band("Radiohead", "Experimental Rock");
    //   testBand.Save();
    //
    //   Venue testVenues1 = new Venue("The Collosseum", "456 Great Big Way");
    //   testVenues1.Save();
    //
    //   Venue testVenues2 = new Venue("ThunderDome", "129 BeyondThe Street");
    //   testVenues2.Save();
    //
    //   //Act
    //   testBand.AddVenue(testVenues1);
    //   List<Venue> result = testBand.GetVenues();
    //   List<Venue> testList = new List<Venue> {testVenues1};
    //
    //   //Assert
    //   Assert.Equal(testList, result);
    // }

    // [Fact]
    // public void Test_AddBand_AddsBandToShow()
    // {
    //   //Arrange
    //   Show testShow = new Show(1, 3, new DateTime(2017, 12, 30));
    //   testShow.Save();
    //
    //   Band testBand = new Band("Nick Cave and the Bad Seeds", "Art Rock");
    //   testBand.Save();
    //
    //   Band testBand2 = new Band("Washed Out", "Chill Wave");
    //   testBand2.Save();
    //
    //   //Act
    //   testShow.AddBand(testBand);
    //   testShow.AddBand(testBand2);
    //
    //   List<Band> result = testShow.GetBands();
    //   List<Band> testList = new List<Band>{testBand, testBand2};
    //
    //   //Assert
    //   Assert.Equal(testList, result);
    // }
    // //
    // [Fact]
    // public void AddVenue_AddsVenuesToBand_VenuesList()
    // {
    //   //Arrange
    //   Band testBand = new Band("Tom Waits", "Just the best... like... ever");
    //   testBand.Save();
    //
    //   Venue testVenues = new Venue("The Collosseum", "89 Out West Blvd");
    //   testVenues.Save();
    //
    //   //Act
    //   testBand.AddVenue(testVenues);
    //
    //   List<Venue> result = testBand.GetVenues();
    //   List<Venue> testList = new List<Venue>{testVenues};
    //
    //   //Assert
    //   Assert.Equal(testList, result);
    // }
    //
    //
    //
    // [Fact]
    // public void Delete_DeletesVenueAssociationsFromDatabase_VenueList()
    // {
    //   //Arrange
    //   Band testBand = new Band("Explosions in the Sky", "Post-Rock");
    //   testBand.Save();
    //
    //   Venue testVenue = new Venue("Skybar", "890 Sky Way");
    //   testVenue.Save();
    //
    //   //Act
    //   testVenue.AddBand(testBand);
    //   testVenue.Delete();
    //
    //   List<Venue> resultBandVenue = testBand.GetVenues();
    //   List<Venue> testBandVenue = new List<Venue> {};
    //
    //   //Assert
    //   Assert.Equal(testBandVenue, resultBandVenue);
    // }
    //
    // [Fact]
    // public void Delete_DeletesBandsAssociationsFromDatabase_BandsList()
    // {
    //   //Arrange
    //   Venue testVenue = new Venue("In The Venue", "39 Grand Central Way");
    //   testVenue.Save();
    //
    //   Band testBands = new Band("Pinback", "Indie Rock");
    //   testBands.Save();
    //
    //   //Act
    //   testBands.AddVenue(testVenue);
    //   testBands.Delete();
    //
    //   List<Band> resultVenueBands = testVenue.GetBands();
    //   List<Band> testVenueBands = new List<Band> {};
    //
    //   //Assert
    //   Assert.Equal(testVenueBands, resultVenueBands);
    // }


    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
      Show.DeleteAll();
    }
  }
}
