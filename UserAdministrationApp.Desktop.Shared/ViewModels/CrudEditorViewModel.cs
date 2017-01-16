using System.ComponentModel;
using Prism.Commands;
using Prism.Mvvm;
using UserAdministrationApp.Model;
using UserAdministrationApp.Services;

namespace UserAdministrationApp.Desktop.Users.ViewModels
{
    public abstract class CrudEditorViewModel<TModel, TEntity, TRepository> : BindableBase
        where TModel : IEntityModel, IDataErrorInfo
        where TRepository : IRepository<TEntity> 
        where TEntity : class, IEntity
    {
        protected readonly TRepository repository;
        private TModel item;

        public TModel Item
        {
            get { return item; }
            set
            {
                if (Item != null)
                {
                    Item.PropertyChanged -= OnSelectedItemOnPropertyChanged;
                }
                item = value;
                base.OnPropertyChanged(nameof(Item));

                SaveCommand.RaiseCanExecuteChanged();

                if (Item != null)
                {
                    Item.PropertyChanged += OnSelectedItemOnPropertyChanged;
                }
            }
        }

        private void OnSelectedItemOnPropertyChanged(object s, PropertyChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        public CrudEditorViewModel(TRepository repository)
        {
            this.repository = repository;
            SaveCommand = new DelegateCommand(Save, CanSave);
        }

        public DelegateCommand SaveCommand { get; set; }

        private bool CanSave()
        {
            if (Item != null)
            {
                return string.IsNullOrWhiteSpace(Item.Error);
            }

            return false;
        }

        private void Save()
        {
            var entity = CreateEmpty();

            if (Item.IsNew)
            {
                Create(entity);                
            }
            else
            {
                Update(entity);
            }
        }

        protected abstract void Create(TEntity entity);

        protected abstract void Update(TEntity entity);

        protected abstract TEntity CreateEmpty();

    }
}