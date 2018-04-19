using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebScriptManager.Models.Repositories
{
    public class SmartThingRepository
    {
        private ScriptModelContainer1 cont;
        static private SmartThingRepository current = null;
        /// <summary>
        /// Создается экземпляр репозитория для сценариев
        /// </summary>
        private SmartThingRepository()
        {
            cont = ContainerSingleton.GetContainer();
        }
        /// <summary>
        /// Получение репозитория для <see cref="SmartThing"/>, позволяющего взаимодействовать с <see cref="SmartThing"/>, хранящимися в базе данных
        /// </summary>
        /// <returns>репозиторий</returns>
        static public SmartThingRepository GetRepository()
        {
            if (current == null)
                current = new SmartThingRepository();
            return current;
        }
        /// <summary>
        /// Возвращение коллекции<see cref="SmartThing"/>в базе данных
        /// </summary>
        /// <returns>коллекция <see cref="SmartThing"/></returns>
        public IEnumerable<SmartThing> SmartThings()
        {
            return cont.SmartThingSet;   //коллекция отсортированная по дате последнего обновления в порядке убывания
        }
        /// <summary>
        /// Получение<see cref="SmartThing"/>из базы данных по ИД
        /// </summary>
        /// <param name="id">ИД<see cref="SmartThing"/>в базе данных</param>
        /// <returns>соответствующий <see cref="SmartThing"/></returns>
        public SmartThing this[long id]
        {
            get
            {
                SmartThing sc = cont.SmartThingSet.Find(id);    //сценарий ищется в БД
                if (sc == null)
                    throw new Exceptions.NoElementException("Умная вещь");   //сценарий не найден
                else
                    return sc;  //найден
            }
        }

        /// <summary>
        /// для добавления<see cref="SmartThing"/>в бд
        /// </summary>
        /// <param name="_controlBox"><see cref="ControlBox"/>, к которому присоединена <see cref="SmartThing"/></param>
        /// <param name="_name">название <see cref="SmartThing"/></param>
        /// <param name="_smartThingType">тип <see cref="SmartThing"/></param>
        /// <param name="_smartPlace"><see cref="SmartPlace"/> которому принадлежит <see cref="SmartPlace"/></param>
        /// <returns>добавленная <see cref="SmartThing"/></returns>
        /// <exception cref="Exceptions.CreatingException"> в ситуации, когда некоторые поля заданы не верно</exception>
        public SmartThing AddSmartThing(ControlBox _controlBox, string _name, SmartThingType _smartThingType, SmartPlace _smartPlace=null)
        {
            if (_smartThingType == null || _name == null || _controlBox == null)
                throw new Exceptions.CreatingException("Умная вещь");
            SmartThing addingSmartThing = new SmartThing
            {
                ControlBox = _controlBox,
                Name = _name,
                SmartPlace = _smartPlace,
                SmartThingType = _smartThingType
            };
            return addingSmartThing;
        }

        /// <summary>
        /// исправление записи<see cref="SmartThing"/>в БД по Id
        /// </summary>
        /// <param name="id">Id исправляемой записи</param>
        /// <param name="_smartPlace">новое значение <see cref="SmartPlace"/></param>
        /// <param name="_controlBox">новое значение <see cref="ControlBox"/></param>
        /// <param name="_name">новое значение названия <see cref="SmartThing"/></param>
        /// <param name="_smartThingType">новое значение <see cref="SmartThingType"/></param>
        /// <exception cref="Exceptions.NoElementException"> если не существует элемента с указанным Id</exception>
        public void EditSmartThing(long id,  SmartPlace _smartPlace, ControlBox _controlBox=null, string _name=null, SmartThingType _smartThingType=null)
        {
            
            SmartThing scenario = this[id];   //получает сценарий по ид
           if(_name!=null) scenario.Name = _name;
           if(_controlBox!= null) scenario.ControlBox = _controlBox;
            scenario.SmartThingType = _smartThingType;
            scenario.SmartPlace = _smartPlace;
          
            cont.SaveChanges();
        }
        /// <summary>
        /// Метод, позволяющий удалять<see cref="SmartThing"/>из базы данных по Id
        /// </summary>
        /// <param name="id">ИД <see cref="SmartThing"/> в БД</param>
        /// <exception cref="Exceptions.NoElementException"> в ситуации,когда элемента с данным ИД не существует</exception>
        public void DeleteSmartThing(long id)
        {
            SmartThing sc = this[id]; //возвращение сценария по ид бд
            sc.ControlBox.SmartThing.Remove(sc);
            sc.SmartPlace.SmartThings.Remove(sc);
            sc.SmartThingType.SmartThings.Remove(sc);           

            cont.SmartThingSet.Remove(sc);
            cont.SaveChanges(); //сохранение изменений
        }
    }
}
