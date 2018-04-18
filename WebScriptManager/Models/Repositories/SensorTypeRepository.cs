using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebScriptManager.Models.Repositories
{
    /// <summary>
    /// Репозиторий типов датчиков, позволяющий выполнять различные операции над типами датчиков в базе данных
    /// </summary>
    public class SensorTypeRepository
    {
        private ScriptModelContainer1 cont;
        static private SensorTypeRepository current = null;
        /// <summary>
        /// Создается экземпляр репозитория для типов датчиков
        /// </summary>
        private SensorTypeRepository()
        {
            cont = ContainerSingleton.GetContainer();
        }
        /// <summary>
        /// Получение репозитория для типов датчиков, позволяющего взаимодействовать со типами датчиков, хранящимися в базе данных
        /// </summary>
        /// <returns></returns>
        static public SensorTypeRepository GetRepository()
        {
            if (current == null)
                current = new SensorTypeRepository();
            return current;
        }
        /// <summary>
        /// Возвращение коллекции типов датчиков в базе данных, отсортированных по названиям
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SensorType> SensorTypes()
        {
            return cont.SensorTypeSet.OrderBy(c => c.Name);   //коллекция отсортированная по ИД
        }
        /// <summary>
        /// Получение типа датчиков из базы данных по ИД
        /// </summary>
        /// <param name="id">ИД типа датчиков в базе данных</param>
        /// <returns></returns>
        public SensorType this[long id]
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
        /// <summary>
        /// Метод, позволяющий добавлять новые типы датчиков в базу данных / облако
        /// </summary>
        /// <param name="_name">Название типа датчиков, отражающий его основное применение</param>
        /// <returns></returns>
        public SensorType AddSensorType(string _name)
        {
            SensorType addType = new SensorType //создание типа датчика
            {
                Name = _name
            };
            cont.SensorTypeSet.Add(addType);    //добавление в БД
            cont.SaveChanges(); //сохранение изменений
            return addType;
        }
    }
}