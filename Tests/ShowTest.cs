using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using BandTracker.Objects;

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
// //////////////////////////////////////////////////////////////////////////
    [Fact]
    public void GetBand_Returns_BandList()
    {
     //Arrange
     Band testBand = new Band("Townes Van Zandt", "Country/Blues");
     testBand.Save();
     Show testShow = new Show(testBand.GetId(), 3, new DateTime(2017, 04, 29));
     testShow.Save();

     //Act
     testShow.AddBand(testBand);
     List<Band> savedBand = testShow.GetBand();
     List<Band> testList = new List<Band> {testBand};

     //Assert
     Assert.Equal(testList, savedBand);
    }

    [Fact]
    public void Test_AddBand_AddsBandToShow()
    {
      //Arrange
      Band testBand = new Band("Nick Cave and the Bad Seeds", "Art Rock");
      testBand.Save();
      Show testShow = new Show(testBand.GetId(), 3, new DateTime(2017, 12, 30));
      testShow.Save();


      // Band testBand2 = new Band("Washed Out", "Chill Wave");
      // testBand2.Save();

      //Act
      testShow.AddBand(testBand);
      // testShow.AddBand(testBand2);

      List<Band> result = testShow.GetBand();
      List<Band> testList = new List<Band>{testBand};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void GetVenue_Returns_VenueList()
    {
      //Arrange
      Venue testVenue = new Venue("The Collosseum", "456 Great Big Way");
      testVenue.Save();
      Show testShow = new Show(17, testVenue.GetId(), new DateTime(2017, 08, 04));
      testShow.Save();

      //Act
      testShow.AddVenue(testVenue);
      List<Venue> result = testShow.GetVenue();
      List<Venue> testList = new List<Venue> {testVenue};

      //Assert
      Assert.Equal(testList, result);
    }

    // //
    [Fact]
    public void AddVenue_AddsVenuesToBand_VenuesList()
    {
      //Arrange
      Venue testVenue = new Venue("The Collosseum", "89 Out West Blvd");
      testVenue.Save();
      Show testShow = new Show(17, testVenue.GetId(), new DateTime(2017, 08, 04));
      testShow.Save();

      //Act
      testShow.AddVenue(testVenue);

      List<Venue> result = testShow.GetVenue();
      List<Venue> testList = new List<Venue>{testVenue};

      //Assert
      Assert.Equal(testList, result);
    }



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
// //////////////////////////////////////////////////////////////////////////

    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
      Show.DeleteAll();
    }
  }
}
