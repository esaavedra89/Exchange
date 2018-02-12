/*
 Clase que su funcion ses proveer servicios de comunicaciones
 cada vez que se necesite consumir una API se llamara a ApiService
 */


namespace Exchange
{
    using Exchange.Helpers;
    using Exchange.Models;
    using Newtonsoft.Json;
    using Plugin.Connectivity;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class ApiService
    {
        
        public async Task<Response> CheckConnection()
        {
            //validamos conexion a internet del telefono
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Lenguages.Error_settings,

                };
            }

            //validamos conexion de internet
            var response = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!response)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Lenguages.Error_conecction,
                };
            }

            return new Response
            {
                IsSuccess = true
            };
        }


        public async Task<Response> GetList<T>(string urlBase, string controller)
        {
            try
            {
                //creamos objeto de Microsoft.net.http

                var cliente = new HttpClient();
                cliente.BaseAddress = new Uri(urlBase);
                //solicitamos informacion de la solicitud
                var response = await cliente.GetAsync(controller);
                //leemos el resultado
                var result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }
                //convertimos 
                var list = JsonConvert.DeserializeObject<List<T>>(result);

                return new Response
                {
                    IsSuccess = true,
                    Result = list,
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };

            }
        }
    }
}
