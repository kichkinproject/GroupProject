using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebScriptManager.Models.Repositories
{
    public class SmartPlaceRepository //класс для работы с умными местами из базы данных
    {
        private ScriptModelContainer1 cont;
        static private SmartPlaceRepository current = null;
        private SmartPlaceRepository()  //конструктор
        {
            cont = ContainerSingleton.GetContainer();
        }
        static public SmartPlaceRepository GetRepository()
        {
            if (current == null)
                current = new SmartPlaceRepository();
            return current;
        }
        public IEnumerable<SmartPlace> SmartPlaces()    //коллекция умных мест
        {
            return cont.SmartPlaceSet.OrderBy(c => c.Name);   //коллекция отсортированная по ИД
        }
        public SmartPlace this[long id]   //возвращение умного места по идентификатору
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
        public SmartPlace AddSmartPlace(string _name, UserGroup _group) //добавление умного места
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
        public void EditSmartPlace(long id, string _name)   //редактирование умного места
        {
            SmartPlace sp = this[id];   //поиск умного места по ИД
            sp.Name = _name;
            cont.SaveChanges(); //сохранение изменений
        }
        public void DeleteSmartPlace(long id)   //удаление умного места из БД
        {
            SmartPlace sp = this[id];   //поиск умного места по ид
            sp.UserGroup.SmartPlaces.Remove(sp);   //удаление умного места из списка умных мест группы пользователей
            if (sp.SmartThings.Count != 0)   //к умному месту подключены умные объекты
            {
                for (int i = sp.SmartThings.Count - 1; i >= 0; i--)  //обход умных объектов
                {
                    //происходит удаление умного объекта из базы данных
                }
            }
            if (sp.Sensors.Count != 0)   //к умному месту подключены датчики
            {
                for (int i = sp.Sensors.Count - 1; i >= 0; i--)  //обход датчиков
                {
                    //происходит удаление датчика из базы данных
                }
            }
            cont.SmartPlaceSet.Remove(sp);  //удаление умного места из базы данных
            cont.SaveChanges(); //сохранение изменений
        }
    }
}