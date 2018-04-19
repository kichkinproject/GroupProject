using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebScriptManager.Models.Repositories
{
    /// <summary>
    /// Репозиторий контроллеров, позволяющий выполнять различные операции над контроллерами в базе данных
    /// </summary>
    public class ControlBoxRepository
    {
        private ScriptModelContainer1 cont;
        static private ControlBoxRepository current = null;
        /// <summary>
        /// Создается экземпляр репозитория для контроллеров
        /// </summary>
        private ControlBoxRepository()
        {
            cont = ContainerSingleton.GetContainer();
        }
        /// <summary>
        /// Получение репозитория контроллеров, позволяющего взаимодействовать с контроллерами, хранящимися в базе данных
        /// </summary>
        /// <returns></returns>
        static public ControlBoxRepository GetRepository()
        {
            if (current == null)
                current = new ControlBoxRepository();
            return current;
        }
        /// <summary>
        /// Возвращение коллекции контроллеров, хранящихся в базе данных, отсортированных по имени
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ControlBox> Controllers()
        {
            return cont.ControlBoxSet.OrderBy(c => c.Name);   //коллекция отсортированная по ИД
        }
        /// <summary>
        /// Возвращение контроллера по идентификатору в базе данных
        /// </summary>
        /// <param name="id">Уникальный идентификатор контроллера в базе данных</param>
        /// <returns></returns>
        public ControlBox this[long id]
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
        /// <summary>
        /// Метод, позволяющий добавлять новые контроллеры в базу данных
        /// </summary>
        /// <param name="_name">Имя контроллера</param>
        /// <param name="_password">Пароль, по которому контроллер будет устанавливать соединения с датчиками и умными объектами</param>
        /// <param name="_stable">Состояние контроллера, является ли он стабильным, целым, работает без неисправностей</param>
        /// <param name="_group">Группа пользователей, которые пользуются контроллером</param>
        /// <returns></returns>
        public ControlBox AddControlBox(string _name, string _password, bool _stable, UserGroup _group)
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
        /// <summary>
        /// Метод, позволяющий редактировать информацию о контроллерах, изменять название, пароль, изменять состояние контроллера
        /// </summary>
        /// <param name="id">Уникальный идентификатор контроллера в базе данных</param>
        /// <param name="_name">Имя контроллера</param>
        /// <param name="_password">Пароль, по которому контроллер будет устанавливать соединения с датчиками и умными объектами</param>
        /// <param name="_stable">Состояние контроллера, является ли он стабильным, целым, работает без неисправностей</param>
        public void EditControlBox(long id, string _name, string _password, bool _stable)
        {
            ControlBox cb = this[id];   //поиск контроллера по ид
            cb.Name = _name;
            cb.Password = _password;
            cb.IsStable = _stable;
            cont.SaveChanges(); //сохранение изменений
        }
        /// <summary>
        /// Метод, позволяющий удалять контроллеры из базы данных
        /// </summary>
        /// <param name="id">Уникальный идентификатор контроллера в базе данных</param>
        public void DeleteControlBox(long id)
        {
            ControlBox cb = this[id];   //поиск контроллера по ид
            cb.UserGroup.ControlBoxes.Remove(cb);   //удаление контроллера из списка контроллеров группы пользователей
            if (cb.SmartThing.Count != 0)   //к контроллеру подключены умные объекты
            {
                SmartThingRepository smTRep = SmartThingRepository.GetRepository();
                for (int i = cb.SmartThing.Count - 1; i >= 0; i--)  //обход умных объектов
                {
                    smTRep.DeleteSmartThing(cb.SmartThing.ElementAt(i).Id); //удаление умного объекта из БД по ИД
                }
            }
            if (cb.Sensor.Count != 0)   //к контроллеру подключены датчики
            {
                SensorRepository senRep = SensorRepository.GetRepository();
                for (int i = cb.Sensor.Count - 1; i >= 0; i--)  //обход датчиков
                {
                    senRep.DeleteSensor(cb.Sensor.ElementAt(i).Id);// удаление датчика из базы данных
                }
            }
            cont.ControlBoxSet.Remove(cb);  //удаление контроллера из базы данных
            cont.SaveChanges(); //сохранение изменений
        }
    }
}