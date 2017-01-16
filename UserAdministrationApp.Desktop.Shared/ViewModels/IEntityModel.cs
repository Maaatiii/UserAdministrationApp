using System.ComponentModel;

namespace UserAdministrationApp.Desktop.Users.ViewModels
{
    public interface IEntityModel : INotifyPropertyChanged
    {
        bool IsNew { get; }
    }
}