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
    //
    // [Fact]
    // public void Test_Find_FindShowInDatabase()
    // {
    //   //Arrange
    //   Show testShow = new Show(5, 6, new DateTime(2016, 04, 04));
    //   testShow.Save();
    //
    //   //Act
    //   Show foundShow = Show.Find(16, 20, testShow.GetId());
    //
    //   //Assert
    //   Assert.Equal(testShow, foundShow);
    // }
    //
    // [Fact]
    // public void Test_Update_UpdatesShowInDatabase()
    // {
    //   //Arrange
    //   Show testShow = new Show(25, 27, new DateTime(2016, 04, 23));
    //   testShow.Save();
    //   DateTime newDateTime = new DateTime(2016, 04, 30);
    //   //Act
    //   testShow.Update(25, 27, new DateTime(2016, 04, 30));
    //   string result = testShow.GetDateTime();
    //
    //   //Assert
    //   Assert.Equal(newDateTime, result);
    // }

    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
      Show.DeleteAll();
    }
  }
}
