﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GeCO.ViewModels {
    public class PropertyChangedVM : INotifyPropertyChanged {
      
        public PropertyChangedVM() {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null )
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}