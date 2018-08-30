using System;
using GeCO.Data;
using System.IO;
using Xamarin.Forms;
using GeCO.Droid.Data;

[assembly: Dependency(typeof(SQLiteHelper))]
namespace GeCO.Droid.Data
{
    public class SQLiteHelper : ISQLiteHelper
    {
        /// <summary>
        /// Quando a aplicação inicia ou quando se precisa de aceder À BD, corre-se o ficheiro App.xaml.cs
        /// que cria uma BD com um certo nome (ou abre a BD já existente).
        /// Através da interface ISQLiteHelper transmite-se esse mesmo nome aos projetos específicos para se descobrir o path no dispositvo.
        /// </summary>
        public string GetLocalPath(string fileName){
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, fileName);
        }
    }
}
