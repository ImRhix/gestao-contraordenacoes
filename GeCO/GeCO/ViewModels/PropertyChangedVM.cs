using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
<<<<<<< HEAD

namespace GeCO.ViewModels {
    public class PropertyChangedVM : INotifyPropertyChanged {
      
        public PropertyChangedVM() {
=======
namespace GeCO.ViewModels
{
    public class PropertyChangedVM : INotifyPropertyChanged
    {
        public PropertyChangedVM()
        {
>>>>>>> parent of 62eeb5a... bugfixing
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null )
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}