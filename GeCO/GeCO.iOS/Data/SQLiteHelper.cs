using System;
using GeCO.Data;
using GeCO.iOS.Data;
using Xamarin.Forms;
using System.IO;

[assembly: Dependency(typeof(SQLiteHelper))]
namespace GeCO.iOS.Data
{
    public class SQLiteHelper : ISQLiteHelper
    {
        /// <summary>
        /// Quando a aplicação inicia, corre o ficheiro App.xaml.cs que cria uma BD com um certo nome (ou abre a BD já existente).
        /// Através da interface ISQLiteHelper transmite-se esse mesmo nome aos projetos específicos para se descobrir o path no dispositvo.
        /// </summary>
        public string GetLocalPath(string fileName)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder)){
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, fileName);
        }
    }
}
