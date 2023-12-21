﻿// Anyone is free to copy, modify, use, compile, or distribute this software,
// either in source code form or as a compiled binary, for any non-commercial
// purpose.
//
// If modified, please retain this copyright header, and consider telling us
// about your changes.  We're always glad to see how people use our code!
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND.
// We do not accept any liability for damage caused by executing
// or modifying this code.

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CSharp.Utils.ViewModels;

public class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected void RaiseAllPropertyChanged()
    {
        foreach (var propertyInfo in GetType().GetProperties())
        {
            // Check if the property is of type ViewModelBase or inherits from it
            if (propertyInfo.PropertyType.IsSubclassOf(typeof(ViewModelBase)) ||
                propertyInfo.PropertyType == typeof(ViewModelBase))
            {
                // Get the value of the property (which should be a ViewModelBase instance)
                var viewModelBaseProperty = propertyInfo.GetValue(this) as ViewModelBase;

                // Call RaiseAllPropertyChanged on the property if it's not null
                viewModelBaseProperty?.RaiseAllPropertyChanged();
            }

            OnPropertyChanged(propertyInfo.Name);
        }
    }
    
    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}