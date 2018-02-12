/* Es la interfaz que permite obtener el lenguje del dispositivo
atravez de las clases que son llamadas desde aqui en cada solucion nativa */
namespace Exchange.Interfaces
{
    using System.Globalization;

    public interface ILocalize
    {
        //obtiene informacion del telefono
        CultureInfo GetCurrentCultureInfo();
        //
        void SetLocale(CultureInfo ci);
    }
}
