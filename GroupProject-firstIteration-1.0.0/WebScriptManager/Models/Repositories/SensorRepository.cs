using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebScriptManager.Models.Repositories
{
    public class SensorRepository
    {
        private ScriptModelContainer1 cont;
        static private SensorRepository current = null;
        /// <summary>
        /// Создается экземпляр репозитория для сенсоров
        /// </summary>
        private SensorRepository()
        {
            cont = ContainerSingleton.GetContainer();
        }
        /// <summary>
        /// Получение репозитория для сенсоров, позволяющего взаимодействовать с сенсорами, хранящимися в базе данных
        /// </summary>
        /// <returns>созданный или найденый репозиторий</returns>
        static public SensorRepository GetRepository()
        {
            if (current == null)
                current = new SensorRepository();
            return current;
        }
        /// <summary>
        /// Возвращение коллекции сенсоров в базе данных
        /// </summary>
        /// <returns>коллекция сенсоров</returns>
        public IEnumerable<Sensor> Sensors()
        {
            return cont.SensorSet;  
        }
        /// <summary>
        /// Получение сенсора из базы данных по ИД
        /// </summary>
        /// <param name="id">ИД сенсора в базе данных</param>
        /// <returns>сенсор по соответствующему id</returns>
        /// <exception cref="Exceptions.NoElementException">вызывается, если элемента с таким индексом не существует в базе</exception>
        public Sensor this[long id]
        {
            get
            {
                Sensor sc = cont.SensorSet.Find(id);    //сценарий ищется в БД
                if (sc == null)
                    throw new Exceptions.NoElementException("Cценарий");   //сценарий не найден
                else
                    return sc;  //найден
            }
        }
        
        /// <summary>
        /// Добавление сенсора в базу данных
        /// </summary>
        /// <param name="_controlBox"><see cref="ControlBox"/> к которому подключен сенсор</param>
        /// <param name="_name">Название сенсора</param>
        /// <param name="_sensorType"><see cref="SensorType"/></param>
        /// <param name="_smartPlace"><see cref="SmartPlace"/>к которому относится сенсор</param>
        /// <returns>возвращает добавленный сенсор без учета Id</returns>
        /// <exception cref="Exceptions.CreatingException"> в ситуации, когда невозможно создать элемент</exception>
        public Sensor AddSensor(
            ControlBox _controlBox, string _name, SensorType _sensorType, SmartPlace _smartPlace)
        {
            if (_controlBox == null || _sensorType == null)
                throw new Exceptions.CreatingException("Сенсор");
            Sensor addingScenario = new Sensor()
            {
                ControlBox = _controlBox,
                Name = _name,
                SensorType = _sensorType,
                SmartPlace = _smartPlace              

            };
            cont.SensorSet.Add(addingScenario);
            cont.SaveChanges();
            return addingScenario;
        }
        
        /// <summary>
        /// корректировка сенсора по id
        /// </summary>
        /// <param name="id">Id корректируемого сенсора</param>
        /// <param name="_name">Новое название</param>
        /// <param name="_controlBox">Новый <see cref="ControlBox"/></param>
        /// <param name="_sensorType">Новый <see cref="SensorType"/></param>
        /// <param name="_smartPlace">Новое <see cref="SmartPlace"/></param>
        /// <exception cref="Exceptions.NoElementException"> в ситуации, когда не существует элемента с данным Id</exception>
        public void EditSensor(long id,  SmartPlace _smartPlace, string _name = null, ControlBox _controlBox = null, SensorType _sensorType = null)
        {

            Sensor sensor = this[id];   //получает сценарий по ид
            if(_name!=null)
            sensor.Name = _name;
            if(_controlBox!=null)
            sensor.ControlBox = _controlBox;
            if(_sensorType!=null)
            sensor.SensorType = _sensorType;

            sensor.SmartPlace = _smartPlace;
            cont.SaveChanges();
        }
        /// <summary>
        /// Метод, позволяющий удалять сенсор из базы данных
        /// </summary>
        /// <param name="id">ИД сенсора в БД</param>
        /// <exception cref="Exceptions.NoElementException"> в ситуации, когда не существует элемента с данным индексом</exception>
        public void DeleteSensor(long id)
        {
            Sensor sc = this[id]; //возвращение сценария по ид
            sc.ControlBox.Sensor.Remove(sc);
            sc.SensorType.Sensors.Remove(sc);
            sc.SmartPlace.Sensors.Remove(sc);
            cont.SensorSet.Remove(sc);
            cont.SaveChanges(); //сохранение изменений
        }

    }
}