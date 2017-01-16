using System.Collections.Generic;

namespace UserAdministrationApp.Model
{
    public class Group : IEntity
    {
        public Group()
        {
            Users = new HashSet<User>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}