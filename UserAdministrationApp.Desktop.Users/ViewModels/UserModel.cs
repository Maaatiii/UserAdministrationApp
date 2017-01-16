using System;
using System.ComponentModel;
using Microsoft.Practices.Prism.Mvvm;
using UserAdministrationApp.Model;

namespace UserAdministrationApp.Desktop.Users.ViewModels
{
    public class UserModel : BindableBase, IDataErrorInfo, IEntityModel
    {
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

        public long Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public string this[string columnName]
        {
            get
            {               
                if (columnName == nameof(Name))
                {
                    if (string.IsNullOrWhiteSpace(Name))
                    {
                        return "Field is required";
                    }
                }
                
                return null;
            }
        }

        public string Error
        {
            get
            {
                IDataErrorInfo me = (IDataErrorInfo)this;
                string error =
                    me[nameof(Name)];
                    
                if (!string.IsNullOrEmpty(error))
                    return "Please check inputted data.";
                return null;
            }
        }

        public bool IsNew { get; }
    }
}