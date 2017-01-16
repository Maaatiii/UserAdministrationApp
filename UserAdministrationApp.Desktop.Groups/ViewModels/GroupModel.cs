using System.ComponentModel;
using Microsoft.Practices.Prism.Mvvm;
using UserAdministrationApp.Desktop.Users.ViewModels;
using UserAdministrationApp.Model;

namespace UserAdministrationApp.Desktop.Groups
{
    public class GroupModel : BindableBase, IDataErrorInfo, IEntityModel
    {
        private string name;

        public GroupModel()
        {
            IsNew = true;
        }

        public GroupModel(Group group)
        {
            Name = group.Name;
            Id = group.Id;
        }

        public long Id { get; set; }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                base.OnPropertyChanged(nameof(Name));
            }
        }

        public bool IsNew { get; set; }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name")
                {
                    if (string.IsNullOrWhiteSpace(Name))
                    {
                        Error = "error";

                    }
                    else
                    {
                        Error = string.Empty;
                    }
                }


                return Error;
            }
        }

        public string Error { get; set; }
    }
}