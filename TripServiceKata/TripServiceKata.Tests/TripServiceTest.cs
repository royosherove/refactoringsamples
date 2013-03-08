using System;
using NUnit.Framework;
using TripServiceKata.Exception;
using TripServiceKata.Trip;
using TripServiceKata.UserNS;

namespace TripServiceKata.Tests
{
    [TestFixture]
    public class TripServiceTest
    {
        [Test]
        public void GetTripsByUser_UserNotLoggedIn_ThrowsException()
        {
            TripService ts = new TestableTripService();

            Assert.Throws<UserNotLoggedInException>(() => 
                 ts.GetTripsByUser(new User()));
        }




    }


    class TestableTripService:TripService
    {
        protected override User GetLoggedUser()
        {
            return null;
        }

    }
}
