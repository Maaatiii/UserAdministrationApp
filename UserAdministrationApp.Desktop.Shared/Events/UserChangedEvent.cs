using Prism.Events;

namespace UserAdministrationApp.Desktop.Shared.Events
{
    public class UserChangedEvent  : PubSubEvent<UserChangedParams>
    {
        
    }

    public class UserChangedParams
    {
        public UserChangedParams(long? userId)
        {
            UserId = userId;
        }

        public long? UserId { get; set; }
    }
}