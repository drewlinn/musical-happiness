using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace BandTracker
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };
      Get["/bands"] = _ =>  {
        List<Band> AllBands = Band.GetAll();
        return View["bands.cshtml", AllBands];
      };
      Get["/venues"] = _ => {
        List<Venue> AllVenues = Venue.GetAll();
        return View["venues.cshtml", AllVenues];
      };
      //CREATE
      Get["/band/new"] = _ =>  {
        List<Venue> AllVenue = Venue.GetAll();
        return View["/band_add.cshtml", AllVenue];
      };
      Get["/venue/new"] = _ =>  {
        return View["/venue_add.cshtml"];
      };
      Post["/bands"]= _ =>  {
        Band newBand = new Band(Request.Form["name"], Request.Form["genre"]);
        newBand.Save();
        return View["success.cshtml", newBand];
      };
      Post["/venues"]= _ => {
        Venue newVenue = new Venue(Request.Form["name"], Request.Form["address"]);
        newVenue.Save();
        return View["success.cshtml", newVenue];
      };
      //READ
      Get["/band/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var selectedVenue = Venue.Find(parameters.id);
        var selectedBand = Band.Find(parameters.id);
        model.Add("venue", selectedVenue);
        model.Add("band", selectedBand);
        return View["band.cshtml", model];
      };
      Get["/venue/{id}"]= parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var selectedVenue = Venue.Find(parameters.id);
        var BandsVenue = selectedVenue.GetBands();
        model.Add("venue", selectedVenue);
        model.Add("bands", BandsVenue);
        return View["venue.cshtml", model];
      };
      //UPDATE
      Get["/venue/edit/{id}"] = parameters => {
        Venue SelectedVenue = Venue.Find(parameters.id);
        return View["venue_edit.cshtml", SelectedVenue];
      };
      Patch["/venue/edit/{id}"] = parameters =>{
        Venue SelectedVenue = Venue.Find(parameters.id);
        SelectedVenue.Update(Request.Form["name"], Request.Form["address"]);
        return View["success.cshtml"];
      };
      Get["/band/edit/{id}"] = parameters => {
        Band selectedBand = Band.Find(parameters.id);
        return View["band_edit.cshtml", selectedBand];
      };
      Patch["/band/edit/{id}"] = parameters =>{
        Band SelectedBand = Band.Find(parameters.id);
        SelectedBand.Update(Request.Form["name"], Request.Form["genre"]);
        return View["success.cshtml"];
      };
      //DESTROY
      Get["venue/delete/{id}"] = parameters => {
        Venue SelectedVenue = Venue.Find(parameters.id);
        return View["venue_delete.cshtml", SelectedVenue];
      };
      Delete["venue/delete/{id}"] = parameters => {
        Venue SelectedVenue = Venue.Find(parameters.id);
        SelectedVenue.Delete();
        return View["success.cshtml"];
      };
      Get["band/delete/{id}"] = parameters => {
        Band SelectedBand = Band.Find(parameters.id);
        return View["band_delete.cshtml", SelectedBand];
      };
      Delete["band/delete/{id}"] = parameters => {
        Band SelectedBand = Band.Find(parameters.id);
        SelectedBand.Delete();
        return View["success.cshtml"];
      };
    }
  }
}
