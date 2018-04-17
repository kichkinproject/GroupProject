using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebScriptManager.Models.Repositories
{
    public class ControlBoxRepository //класс для работы с контроллерами из базы данных
    {
        private ScriptModelContainer1 cont;
        static private ControlBoxRepository current = null;
        private ControlBoxRepository()  //конструктор
        {
            cont = ContainerSingleton.GetContainer();
        }
        static public ControlBoxRepository GetRepository()
        {
            if (current == null)
                current = new ControlBoxRepository();
            return current;
        }
        public IEnumerable<ControlBox> Controllers()    //коллекция контроллеров
        {
            return cont.ControlBoxSet.OrderBy(c => c.Name);   //коллекция отсортированная по ИД
        }
        public ControlBox this[long id]   //возвращение контроллера по идентификатору
        {
            get
            {
                ControlBox cb = cont.ControlBoxSet.Find(id);    //контроллер ищется в БД
                if (cb == null)
                    throw new Exceptions.NoElementException("Контроллер");   //контроллер не найден
                else
                    return cb;  //найден
            }
        }
        public ControlBox AddControlBox(string _name, string _password, bool _stable, UserGroup _group) //добавление контроллера
        {
            ControlBox addController = new ControlBox   //создание нового контроллера
            {
                Name = _name,
                Password = _password,
                IsStable = _stable
            };
            _group.ControlBoxes.Add(addController); //связывание контроллера с группой пользователей
            cont.ControlBoxSet.Add(addController);  //добавление контроллера в базу данных
            cont.SaveChanges(); //сохранение изменений
            return addController;
        }
        public void EditControlBox(long id, string _name, string _password, bool _stable)   //редактирование информации о контроллере
        {
            ControlBox cb = this[id];   //поиск контроллера по ид
            cb.Name = _name;
            cb.Password = _password;
            cb.IsStable = _stable;
            cont.SaveChanges(); //сохранение изменений
        }
        public void DeleteControlBox(long id)   //удаление контроллера из базы данных
        {
            ControlBox cb = this[id];   //поиск контроллера по ид
            cb.UserGroup.ControlBoxes.Remove(cb);   //удаление контроллера из списка контроллеров группы пользователей
            if (cb.SmartThing.Count != 0)   //к контроллеру подключены умные объекты
            {
                for (int i = cb.SmartThing.Count - 1; i >= 0; i--)  //обход умных объектов
                {
                    //происходит удаление умного объекта из базы данных
                }
            }
            if (cb.Sensor.Count != 0)   //к контроллеру подключены датчики
            {
                for (int i = cb.Sensor.Count - 1; i >= 0; i--)  //обход датчиков
                {
                    //происходит удаление датчика из базы данных
                }
            }
            cont.ControlBoxSet.Remove(cb);  //удаление контроллера из базы данных
            cont.SaveChanges(); //сохранение изменений
        }
    }
}