

namespace GeCO.Data
{
    /// <summary>
    /// Interface necessária como intermediário entre o código partilhado e o código específico de cada plataforma. 
    /// </summary>
    public interface ISQLiteHelper {
        /// <summary>
        /// Função que retorna o path no dispositivo onde está guardada a base de dados.
        /// </summary>
        string GetLocalPath(string fileName);
    }
}
