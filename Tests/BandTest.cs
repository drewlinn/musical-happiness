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

    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }
  }
}
