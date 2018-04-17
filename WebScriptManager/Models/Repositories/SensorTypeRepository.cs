using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebScriptManager.Models.Repositories
{
    public class SensorTypeRepository
    {
        private ScriptModelContainer1 cont;
        static private SensorTypeRepository current = null;
        private SensorTypeRepository()  //конструктор
        {
            cont = ContainerSingleton.GetContainer();
        }
        static public SensorTypeRepository GetRepository()
        {
            if (current == null)
                current = new SensorTypeRepository();
            return current;
        }
        public IEnumerable<SensorType> SmartPlaces()    //коллекция типов датчиков
        {
            return cont.SensorTypeSet.OrderBy(c => c.Name);   //коллекция отсортированная по ИД
        }
        public SensorType this[long id]   //возвращение типа датчика по идентификатору
        {
            get
            {
                SensorType st = cont.SensorTypeSet.Find(id);    //типа датчика ищется в БД
                if (st == null)
                    throw new Exceptions.NoElementException("Тип датчика");   //тип датчика не найден
                else
                    return st;  //найден
            }
        }
    }
}