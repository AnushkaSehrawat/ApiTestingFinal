using FluentAssertions;
using HotelManagementSystem.Models;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace HotelManagementSystem.Tests
{
    [Binding]
    public class AddHotelSteps
    {
        private Hotel hotel = new Hotel();
        private Hotel addHotelResponse;
        private List<Hotel> hotels = new List<Hotel>();
        List<int> IdList = new List<int>();
  

        [Given(@"User provided valid Id '(.*)'  and '(.*)'for hotel")]
        public void GivenUserProvidedValidIdAndForHotel(int id, string name)
        {
            hotel.Id = id;
            hotel.Name = name;
        }
        [Given(@"the user specifies specifies an '(.*)'")]
        public void GivenTheUserSpecifiesSpecifiesAn(int id)
        {
            //ScenarioContext.Current.Pending();
            hotel.Id = id;
        }


        [Given(@"Use has added required details for hotel")]
        public void GivenUseHasAddedRequiredDetailsForHotel()
        {
            SetHotelBasicDetails();
        }

        
        [Given(@"the user calls the hotel api")]
        [When(@"User calls AddHotel api")]
        public void WhenUserCallsAddHotelApi()
        {
            hotels = HotelsApiCaller.AddHotel(hotel);
            IdList.Add(hotel.Id);
        }
        [When(@"the user calls the hotel api")]
        public void WhenTheUserCallsTheHotelApi()
        {
            //ScenarioContext.Current.Pending();
            HotelsApiCaller.AddHotel(hotel);
            addHotelResponse = HotelsApiCaller.HotelById(hotel.Id);
        }


        [Then(@"Hotel with name '(.*)' should be present in the response")]
        public void ThenHotelWithNameShouldBePresentInTheResponse(string name)
        {
            hotel = hotels.Find(htl => htl.Name.ToLower().Equals(name.ToLower()));
            hotel.Should().NotBeNull(string.Format("Hotel with name {0} not found in response",name));
        }
        [Then(@"the hotel with specified hotel '(.*)' should be present in the response")]
        public void ThenTheHotelWithSpecifiedHotelShouldBePresentInTheResponse(int id)
        {
            //ScenarioContext.Current.Pending();
            addHotelResponse.Should().NotBeNull(string.Format("Hotel with specefied id {0} does not exists.",id));
        }
        [When(@"the user calls the api to check weather the enteries are present or not.")]
        [Then(@"the added hotel enteries should be present in the database.")]
        public void WhenTheUserCallsTheApiToCheckWeatherTheEnteriesArePresentOrNo_()
        {
            //ScenarioContext.Current.Pending();
           //hotels= HotelsApiCaller.AddHotel(hotel);
           foreach (var item in IdList)
            {
                var x = hotels.Find(ht=> ht.Id==item);
                x.Should().NotBeNull(string.Format("The id  was not added in the list."));
            }
           

       
        }
        //[Then(@"the added hotel enteries should be present in the database.")]
        //public void ThenTheAddedHotelEnteriesWithAllShouldBePresentInTheDatabase_()
        //{
        //    //ScenarioContext.Current.Pending();
        //    //var value = hotels.Find(hd => hd.Id==id);
        //    //value.Should().NotBeNull(string.Format("The hotel with id {0} was not added.",id));
            
        //}






        private void SetHotelBasicDetails()
        {
            hotel.ImageURLs = new List<string>() { "image1", "image2" };
            hotel.LocationCode = "Location";
            hotel.Rooms = new List<Room>() { new Room() { NoOfRoomsAvailable = 10, RoomType = "delux" } };
            hotel.Address = "Address1";
            hotel.Amenities = new List<string>() { "swimming pool", "gymnasium" };
        }
    }
}
