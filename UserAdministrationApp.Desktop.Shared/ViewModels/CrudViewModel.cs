using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Mvvm;

namespace UserAdministrationApp.Desktop.Shared.ViewModels
{
    public abstract class CrudViewModel<T> : BindableBase
    {
        private ObservableCollection<T> items;
        private T selectedItem;

        public CrudViewModel()
        {
            DeleteCommand = new DelegateCommand(Delete);
            AddCommand = new DelegateCommand(Add);
        }

        public T SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                base.OnPropertyChanged(nameof(SelectedItem));

                SelectedItemChanged();
            }
        }

        protected virtual void SelectedItemChanged()
        {
        }

        public ObservableCollection<T> Items
        {
            get { return items; }
            set
            {
                items = value;
                base.OnPropertyChanged(nameof(Items));
            }
        }

        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }

        protected abstract void Load();

        protected abstract void Add();

        protected abstract void Delete();
    }
}