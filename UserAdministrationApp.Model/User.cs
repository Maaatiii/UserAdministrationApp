using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAdministrationApp.Model
{
    public interface IEntity
    {
        long Id { get;}
    }

    public class User : IEntity
    {
        public User()
        {
            Groups = new HashSet<Group>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
    }
}
