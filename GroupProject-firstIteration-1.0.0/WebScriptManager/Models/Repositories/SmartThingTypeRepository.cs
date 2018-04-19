using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebScriptManager.Models.Repositories
{
    /// <summary>
    /// Репозиторий типов умных объектов, позволяющий выполнять различные операции над типами датчиков в базе данных
    /// </summary>
    public class SmartThingTypeRepository
    {
        private ScriptModelContainer1 cont;
        static private SmartThingTypeRepository current = null;
        /// <summary>
        /// Создается экземпляр репозитория для типов умных объектов
        /// </summary>
        private SmartThingTypeRepository()
        {
            cont = ContainerSingleton.GetContainer();
        }
        /// <summary>
        /// Получение репозитория для типов умных объектов, позволяющего взаимодействовать со типами умных объектов, хранящимися в базе данных
        /// </summary>
        /// <returns></returns>
        static public SmartThingTypeRepository GetRepository()
        {
            if (current == null)
                current = new SmartThingTypeRepository();
            return current;
        }
        /// <summary>
        /// Возвращение коллекции типов умных объектов в базе данных, отсортированных по названиям
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SmartThingType> SmartThingTypes()
        {
            return cont.SmartThingTypeSet.OrderBy(c => c.Name);   //коллекция отсортированная по ИД
        }
        /// <summary>
        /// Получение типа умных объектов из базы данных по ИД
        /// </summary>
        /// <param name="id">ИД типа умных объектов в базе данных</param>
        /// <returns></returns>
        public SmartThingType this[long id]
        {
            get
            {
                SmartThingType stt = cont.SmartThingTypeSet.Find(id);    //тип умного объекта ищется в БД
                if (stt == null)
                    throw new Exceptions.NoElementException("Тип умного объекта");   //тип умного объекта не найден
                else
                    return stt;  //найден
            }
        }
        /// <summary>
        /// Метод, позволяющий добавлять новые типы умных объектов в базу данных / облако
        /// </summary>
        /// <param name="_name">Название типа умных объектов, отражающий их основное применение</param>
        /// <returns></returns>
        public SmartThingType AddSmartThingType(string _name)
        {
            SmartThingType addType = new SmartThingType
            {
                Name = _name
            };
            cont.SmartThingTypeSet.Add(addType);    //добавление в БД
            cont.SaveChanges(); //сохранение изменений
            return addType;
        }
    }
}