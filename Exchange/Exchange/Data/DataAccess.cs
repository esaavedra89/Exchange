
namespace Exchange.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Exchange.Interfaces;
    using Exchange.Models;
    using SQLite.Net;
    using SQLiteNetExtensions.Extensions;
    using Xamarin.Forms;


    public class DataAccess : IDisposable
    {
        SQLiteConnection connection;
        public DataAccess()
        {
            //Pide la configuracion dependiendo de cada plataforma
            var config = DependencyService.Get<IConfig>();
            //estableemos conexion con la DB
            //config.Platform es lo que devuelve cada plataforma personalizada
            //combina directorio que devuelve la interfaz y se le pone el nombre de la DB
            connection = new SQLiteConnection(config.Platform,
                System.IO.Path.Combine(config.DirectoryDB, "Exchange.db3"));
            //Crea tabla con modelo Rate
            connection.CreateTable<Rate>();
        }

        //Insertamos T es el modelo
        public void Insert<T>(T model)
        {
            connection.Insert(model);
        }

        public void Update<T>(T model)
        {
            connection.Update(model);
        }

        public void Delete<T>(T model)
        {
            connection.Delete(model);
        }

        //_Firrst devuelve el primer registro de la tabla
        //WithChildren      
        public T First<T>(bool WithChildren) where T : class
        {
            if (WithChildren)
            {
                //para usar el se debe usar la version 
                //using SQLiteNetExtensions.Extensions 1.3;
                return connection.GetAllWithChildren<T>().FirstOrDefault();
            }
            else
            {
                return connection.Table<T>().FirstOrDefault();
            }
        }

        public List<T> GetList<T>(bool WithChildren) where T : class
        {
            if (WithChildren)
            {
                return connection.GetAllWithChildren<T>().ToList();
            }
            else
            {
                return connection.Table<T>().ToList();
            }
        }

        public T Find<T>(int pk, bool WithChildren) where T : class
        {
            if (WithChildren)
            {
                return connection.GetAllWithChildren<T>().FirstOrDefault(m => m.GetHashCode() == pk);
            }
            else
            {
                return connection.Table<T>().FirstOrDefault(m => m.GetHashCode() == pk);
            }
        }

        //Cerramos conexion
        public void Dispose()
        {
            connection.Dispose();
        }
    }

}
