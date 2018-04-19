using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebScriptManager.Models.Repositories
{
    /// <summary>
    /// Репозиторий умных мест, позволяющий выполнять различные операции над умными местами в базе данных
    /// </summary>
    public class SmartPlaceRepository
    {
        private ScriptModelContainer1 cont;
        static private SmartPlaceRepository current = null;
        /// <summary>
        /// Создается экземпляр репозитория для умных мест
        /// </summary>
        private SmartPlaceRepository()
        {
            cont = ContainerSingleton.GetContainer();
        }
        /// <summary>
        /// Получение репозитория для умных мест, позволяющего взаимодействовать со умными местами, хранящимися в базе данных
        /// </summary>
        /// <returns></returns>
        static public SmartPlaceRepository GetRepository()
        {
            if (current == null)
                current = new SmartPlaceRepository();
            return current;
        }
        /// <summary>
        /// Возвращение коллекции умных мест в базе данных, отсортированных по названию
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SmartPlace> SmartPlaces()
        {
            return cont.SmartPlaceSet.OrderBy(c => c.Name);   //коллекция отсортированная по ИД
        }
        /// <summary>
        /// Получение умного места из базы данных по ИД
        /// </summary>
        /// <param name="id">ИД умного места в базе данных</param>
        /// <returns></returns>
        public SmartPlace this[long id]
        {
            get
            {
                SmartPlace sp = cont.SmartPlaceSet.Find(id);    //умное место ищется в БД
                if (sp == null)
                    throw new Exceptions.NoElementException("Умное место");   //умное место не найден
                else
                    return sp;  //найден
            }
        }
        /// <summary>
        /// Метод, позволяющий добавлять новые умные места в базу данных
        /// </summary>
        /// <param name="_name">Название умного места</param>
        /// <param name="_group">Группа пользователей, которые взаимодействуют с данным умным местом</param>
        /// <returns></returns>
        public SmartPlace AddSmartPlace(string _name, UserGroup _group)
        {
            SmartPlace addPlace = new SmartPlace    //создание умного места
            {
                Name = _name
            };
            _group.SmartPlaces.Add(addPlace);   //добавление умного места в список умных мест группы пользователей
            cont.SmartPlaceSet.Add(addPlace);   //добавление умного места в БД
            cont.SaveChanges(); //сохранение изменений
            return addPlace;
        }
        /// <summary>
        /// Метод, позволяющий редактировать информацию об умных местах
        /// </summary>
        /// <param name="id">Идентификатор умного места</param>
        /// <param name="_name">Название умного места в системе</param>
        public void EditSmartPlace(long id, string _name)
        {
            SmartPlace sp = this[id];   //поиск умного места по ИД
            sp.Name = _name;
            cont.SaveChanges(); //сохранение изменений
        }
        /// <summary>
        /// Метод, позволяющий удалять умные места из базы данных
        /// </summary>
        /// <param name="id">Уникальный идентификатор умного места в базе данных</param>
        public void DeleteSmartPlace(long id)
        {
            SmartPlace sp = this[id];   //поиск умного места по ид
            sp.UserGroup.SmartPlaces.Remove(sp);   //удаление умного места из списка умных мест группы пользователей
            if (sp.SmartThings.Count != 0)   //к умному месту подключены умные объекты
            {
                SmartThingRepository smTRep = SmartThingRepository.GetRepository();
                for (int i = sp.SmartThings.Count - 1; i >= 0; i--)  //обход умных объектов
                {
                    smTRep.DeleteSmartThing(sp.SmartThings.ElementAt(i).Id); //удаление умного объекта из БД по ИД
                }
            }
            if (sp.Sensors.Count != 0)   //к умному месту подключены датчики
            {
                SensorRepository senRep = SensorRepository.GetRepository();
                for (int i = sp.Sensors.Count - 1; i >= 0; i--)  //обход датчиков
                {
                    senRep.DeleteSensor(sp.Sensors.ElementAt(i).Id);// удаление датчика из базы данных
                }
            }
            cont.SmartPlaceSet.Remove(sp);  //удаление умного места из базы данных
            cont.SaveChanges(); //сохранение изменений
        }
    }
}