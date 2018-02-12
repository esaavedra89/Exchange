
namespace Exchange.Services
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;

    public class DataService
    {
        //borrar todo, dejar tabla limpia
        public bool DeleteAll<T>() where T : class
        {
            try
            {

                using (var da = new DataAccess())
                {
                    //de todos los registros dame la lista
                    var oldRecords = da.GetList<T>(false);
                    //por cada registro viejo 
                    foreach (var oldRecord in oldRecords)
                    {
                        //boore el registro
                        da.Delete(oldRecord);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

        //Boorar todo e insertar
        public T DeleteAllAndInsert<T>(T model) where T : class
        {
            try
            {
                using (var da = new DataAccess())
                {
                    //borra todo lo que hay
                    var oldRecords = da.GetList<T>(false);
                    foreach (var oldRecord in oldRecords)
                    {
                        da.Delete(oldRecord);
                    }

                    //e inserta el nuevo modelo
                    da.Insert(model);

                    return model;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return model;
            }
        }

        //insertar o actualizar
        public T InsertOrUpdate<T>(T model) where T : class
        {
            try
            {
                using (var da = new DataAccess())
                {
                    //buscar si encuentra el modelo lo actualiza
                    //Si no lo encuentra lo inserta
                    var oldRecord = da.Find<T>(model.GetHashCode(), false);
                    //Si hay un modelo
                    if (oldRecord != null)
                    {
                        //lo actualiza
                        da.Update(model);
                    }
                    //si es vacio (no hay lista)
                    else
                    {
                        //inserta un modelo
                        da.Insert(model);
                    }

                    return model;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return model;
            }
        }

        //inserta
        public T Insert<T>(T model)
        {
            using (var da = new DataAccess())
            {
                da.Insert(model);
                return model;
            }
        }

        //busca
        public T Find<T>(int pk, bool withChildren) where T : class
        {
            using (var da = new DataAccess())
            {
                return da.Find<T>(pk, withChildren);
            }
        }

        //devuelve el primero
        public T First<T>(bool withChildren) where T : class
        {
            using (var da = new DataAccess())
            {
                return da.GetList<T>(withChildren).FirstOrDefault();
            }
        }
        //obtene todos los registros
        public List<T> Get<T>(bool withChildren) where T : class
        {
            using (var da = new DataAccess())
            {
                return da.GetList<T>(withChildren).ToList();
            }
        }
        //actualiza
        public void Update<T>(T model)
        {
            using (var da = new DataAccess())
            {
                da.Update(model);
            }
        }
        //borra
        public void Delete<T>(T model)
        {
            using (var da = new DataAccess())
            {
                da.Delete(model);
            }
        }
        //Pasa una lista y graba todo
        public void Save<T>(List<T> list) where T : class
        {
            using (var da = new DataAccess())
            {
                foreach (var record in list)
                {
                    InsertOrUpdate(record);
                }
            }
        }
    }
}
