using System;
using System.ComponentModel;
using Microsoft.Practices.Prism.Mvvm;
using UserAdministrationApp.Model;

namespace UserAdministrationApp.Desktop.Users.ViewModels
{
    public class UserModel : BindableBase, IDataErrorInfo
    {
        private string _name;

        public UserModel(User user)
        {
            Name = user.Name;
            Id = user.Id;
            Email = user.Email;
            CreatedOn = user.CreatedOn;
        }

        public UserModel()
        {
            IsNew = true;
        }

        public bool IsNew { get; private set; }

        public DateTime CreatedOn { get; set; }
        public string Email { get; set; }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                base.OnPropertyChanged(()=>Name);
            }
        }

        public long Id { get; set; }

        public string this[string columnName]
        {
            get {
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