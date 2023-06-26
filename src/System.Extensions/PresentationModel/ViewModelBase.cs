using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System.PresentationModel;

public abstract class ViewModelBase : INotifyPropertyChanged, INotifyDataErrorInfo //, IDataErrorInfo
{
    /// <summary>
    /// Sets the property.
    /// </summary>
    /// <returns><c>true</c>, if property was set, <c>false</c> otherwise.</returns>
    /// <param name="backingStore">Backing store.</param>
    /// <param name="value">Value.</param>
    /// <param name="propertyName">Property name.</param>
    /// <param name="onChanged">On changed.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName]string propertyName = "", Action onChanged = null)
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value))
        {
            return false;
        }

        backingStore = value;

        onChanged?.Invoke();

        OnPropertyChanged(propertyName);

        return true;
    }

    /// <summary>
    /// Occurs when property changed.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Raises the property changed event.
    /// </summary>
    /// <param name="propertyName">Property name.</param>
    public void OnPropertyChanged([CallerMemberName]string propertyName = "")
    {
        var changed = PropertyChanged;

        if (changed == null)
        {
            return;
        }

        changed.Invoke(this, new PropertyChangedEventArgs(propertyName));

        //if (State == ObjectState.Unchanged)
        //{
        //    State = ObjectState.Modified;
        //}

        changed.Invoke(this, new PropertyChangedEventArgs("State"));
    }

    //public ObjectState State { get; internal protected set; }

    protected readonly Dictionary<string, IList<Exception>> validationErrors = new Dictionary<string, IList<Exception>>();

    protected void ClearErrors(string propertyName)
    {
        if (!validationErrors.ContainsKey(propertyName))
        {
            return;
        }

        validationErrors.Remove(propertyName);

        OnErrorsChanged(propertyName);
    }

    protected void RaiseErrorsChanged(string propertyName, Exception exception)
    {
        IList<Exception> errors;

        if (validationErrors.ContainsKey(propertyName))
        {
            errors = validationErrors[propertyName];
        }
        else
        {
            errors = new List<Exception>();

            validationErrors.Add(propertyName, errors);
        }

        errors.Add(exception);

        OnErrorsChanged(propertyName);
    }

    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

    protected virtual void OnErrorsChanged(string propertyName)
    {
        if (ErrorsChanged != null)
        {
            ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }

    public bool HasErrors
    {
        get { return validationErrors.Count > 0; }
    }

    public string Error => "teste";

    public string this[string columnName]
    {
        get
        {
            if (validationErrors.Count == 0)
            {
                return null;
            }

            if (validationErrors[columnName].Count > 0)
            {
                return validationErrors[columnName][0].Message;
            }
            else
            {
                return null;
            }
        }
    }

    public IEnumerable GetErrors(string propertyName)
    {
        if (string.IsNullOrEmpty(propertyName) || !validationErrors.ContainsKey(propertyName))
        {
            return null;
        }

        return validationErrors[propertyName];
    }
}
