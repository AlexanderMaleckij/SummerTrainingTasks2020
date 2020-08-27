using System.ComponentModel;

namespace ORM
{
    /// <summary>
    /// Base class for all models
    /// </summary>
    public abstract class ModelBase : INotifyPropertyChanged
    {
        private int id;
        public int Id 
        {
            get => id;
            internal set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
