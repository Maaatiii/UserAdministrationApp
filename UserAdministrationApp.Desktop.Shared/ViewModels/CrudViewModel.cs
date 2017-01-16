using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Mvvm;

namespace UserAdministrationApp.Desktop.Shared.ViewModels
{
    public abstract class CrudViewModel<T> : ViewModelBase
    {
        private ObservableCollection<T> items;
        private T selectedItem;

        public CrudViewModel()
        {
            DeleteCommand = new DelegateCommand(Delete);
            AddCommand = new DelegateCommand(Add);
        }

        public T SelectedItem { get; set; }

        public bool IsEditorVisible => SelectedItem != null;

        protected virtual void OnSelectedItemChanged()
        {
            base.OnPropertyChanged(nameof(IsEditorVisible));
        }

        public ObservableCollection<T> Items { get; set; }

        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }

        protected abstract void Load();

        protected abstract void Add();

        protected abstract void Delete();

        public override void OnLoaded()
        {
            base.OnLoaded();
            Load();
        }
    }
}