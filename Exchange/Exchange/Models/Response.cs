/*
  esta clase se encargara de deirnos si se pudo o no
  obtener la comunicacion con la API
 */
namespace Exchange.Models
{
    public class Response
    {

        //Si se realizo exitosa la conexion
        public bool IsSuccess
        {
            get;
            set;
        }

        //Mensaje obtenido
        public string Message
        {
            get;
            set;
        }

        //Resultado obtenido
        public object Result
        {
            get;
            set;
        }

    }
}
