using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.UserNS;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        public List<Trip> GetTripsByUserFromDB(User user)
        {
            List<Trip> tripList = new List<Trip>();
            User loggedUser = GetLoggedUser();
            bool isFriend = false;
            if (loggedUser != null)
            {
                var searchContext = new SearchContext(user, loggedUser, isFriend, tripList);
                return GetTripsByUserFromDB(searchContext);
            }
            throw new UserNotLoggedInException();
        }

        protected virtual User GetLoggedUser()
        {
            return UserSession.GetInstance().GetLoggedUser();
        }

        private  List<Trip> GetTripsByUserFromDB(SearchContext context)
        {
            foreach (User friend in context.User.GetFriends())
            {
                if (friend.Equals(context.LoggedUser))
                {
                    context.IsFriend = true;
                    break;
                }
            }
            if (context.IsFriend)
            {
                context.TripList = FindTripsByUser(context.User);
            }
            return context.TripList;
        }

        protected virtual List<Trip> FindTripsByUser(User user)
        {
            return TripDAO.FindTripsByUser(user);
        }
    }

    internal class SearchContext
    {
        private readonly User _user;
        private readonly User _loggedUser;
        private readonly bool _isFriend;
        private readonly List<Trip> _tripList;

        public User User;
        public User LoggedUser;
        public bool IsFriend;
        public List<Trip> TripList;

        public SearchContext(User user, User loggedUser, bool isFriend, List<Trip> tripList)
        {
            _user = user;
            _loggedUser = loggedUser;
            _isFriend = isFriend;
            _tripList = tripList;
        }
    }
}
