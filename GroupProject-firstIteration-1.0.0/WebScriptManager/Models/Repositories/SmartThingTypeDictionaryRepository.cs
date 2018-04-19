using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebScriptManager.Models.Repositories
{
    public class SmartThingTypeDictionaryRepository
    {
        private ScriptModelContainer1 cont;
        static private SmartThingTypeDictionaryRepository current = null;
        /// <summary>
        /// Создается экземпляр репозитория для <see cref="SmartThingTypeDictionary"/>
        /// </summary>
        private SmartThingTypeDictionaryRepository()
        {
            cont = ContainerSingleton.GetContainer();
        }
        /// <summary>
        /// Получение репозитория для <see cref="SmartThingTypeDictionary"/>
        /// </summary>
        /// <returns>соответствующий репозиторий</returns>
        static public SmartThingTypeDictionaryRepository GetRepository()
        {
            if (current == null)
                current = new SmartThingTypeDictionaryRepository();
            return current;
        }
        /// <summary>
        /// Возвращение коллекции <see cref="SmartThingTypeDictionary"/> в базе данных
        /// </summary>
        /// <returns>коллекция из БД</returns>
        public IEnumerable<SmartThingTypeDictionary> SmartThingTypeDictionaryRecords()
        {
            return cont.SmartThingTypeDictionarySet;
        }
        /// <summary>
        /// Получение <see cref="SmartThingTypeDictionary"/> из базы данных по ИД
        /// </summary>
        /// <param name="id">ИД <see cref="SmartThingTypeDictionary"/> в базе данных</param>
        /// <returns>найденый <see cref="SmartThing"/></returns>
        /// <exception cref="Exceptions.NoElementException"> в случае, если данной записи не существует</exception>
        public SmartThingTypeDictionary this[long id]
        {
            get
            {
                SmartThingTypeDictionary sc = cont.SmartThingTypeDictionarySet.Find(id);    //сценарий ищется в БД
                if (sc == null)
                    throw new Exceptions.NoElementException("Запись о типе умной вещи");   //сценарий не найден
                else
                    return sc;  //найден
                
            }
        }
        /// <summary>
        /// Получение <see cref="SmartThingTypeDictionary"/> из базы данных по <see cref="SmartThingType"/>
        /// </summary>
        /// <param name="_smartThingType">искомый <see cref="SmartThingType"/></param>
        /// <returns>найденный <see cref="SmartThingTypeDictionary"/></returns>
        /// <exception cref="Exceptions.NoElementException"> в ситуации, когда элемент не найден</exception>
        public SmartThingTypeDictionary this[SmartThingType _smartThingType]
        {
            get
            {
                var list = from c in cont.SmartThingTypeDictionarySet where c.SmartThingType == _smartThingType select c;
                if (list.Count()==0)
                    throw new Exceptions.NoElementException("Запись о типе умной вещи");   //сценарий не найден
                return list.First();

            }
        }
        /// <summary>
        /// добавляет новую запись в словарь типов умных вещей
        /// </summary>
        /// <param name="_smartThingType"></param>
        /// <param name="_lowerBound"></param>
        /// <param name="_upperBound"></param>
        /// <returns>добавляемую запись</returns>
        /// <exception cref="Exceptions.CreatingException"></exception>
        public SmartThingTypeDictionary AddSmartThingTypeDictionaryRecord(SmartThingType _smartThingType, double _lowerBound, double _upperBound)
        {
            if (_smartThingType == null||(from c in cont.SmartThingTypeDictionarySet where c.SmartThingType == _smartThingType select c).Count()>0 )
                throw new Exceptions.CreatingException("Запись в словаре типов умных вещей");
            var adding = new SmartThingTypeDictionary()
            {
                SmartThingType = _smartThingType, LowerBound = _lowerBound, UpperBound = _upperBound,
            };
            cont.SmartThingTypeDictionarySet.Add(adding);
            adding.SmartThingType.SmartThingTypeDictionary = adding;
            cont.SaveChanges();
            return adding;

        }
        /// <summary>
        /// исправляет данные о <see cref="SmartThingType"/> в словаре
        /// </summary>
        /// <param name="id">искомый Id</param>
        /// <param name="_lowerBound">новое значение нижней границы</param>
        /// <param name="_upperBound">новое значение верхней границы</param>
        /// <exception cref="Exceptions.NoElementException"> если элемента с таким Id не существует</exception>
        public void EditSmartThingDictionaryRecrod(long id, double _lowerBound, double _upperBound)
        {
            SmartThingTypeDictionary scenario = this[id];   //получает сценарий по ид
            scenario.LowerBound = _lowerBound;
            scenario.UpperBound = _upperBound;
            cont.SaveChanges();
        }
        /// <summary>
        /// Исправляет данные о <see cref="SmartThingType"/> в словаре
        /// </summary>
        /// <param name="id">искомый <see cref="SmartThingType"/></param>
        /// <param name="_lowerBound">новое значение нижней границы</param>
        /// <param name="_upperBound">новое значение верхней границы</param>
        /// <exception cref="Exceptions.NoElementException"> если элемента с таким <see cref="SmartThingType"/></exception>
        public void EditSmartThingDictionaryRecrod(SmartThingType id, double _lowerBound, double _upperBound)
        {
            SmartThingTypeDictionary scenario = this[id];   //получает сценарий по ид
            scenario.LowerBound = _lowerBound;
            scenario.UpperBound = _upperBound;
            cont.SaveChanges();
        }
        /// <summary>
        /// Метод, позволяющий удалять запись из базы данных
        /// </summary>
        /// <param name="id">ИД сценария в БД</param
        /// <exception cref="Exceptions.NoElementException"> если элемента с таким Id не существует</exception>
        public void DeleteSmartThingDictionaryRecord(long id)
        {
            SmartThingTypeDictionary sc = this[id]; //возвращение сценария по ид
            sc.SmartThingType.SmartThingTypeDictionary = null;
            cont.SmartThingTypeDictionarySet.Remove(sc);    //удаление сценария из бд
            cont.SaveChanges(); //сохранение изменений
        }
        /// <summary>
        /// Метод, позволяющий удалять запись из базы данных
        /// </summary>
        /// <param name="id"><see cref="SmartThingType"/> сценария в БД</param
        /// <exception cref="Exceptions.NoElementException"> если элемента с таким <see cref="SmartThingType"/> не существует</exception>
        public void DeleteSmartThingDictionaryRecord(SmartThingType id)
        {
            SmartThingTypeDictionary sc = this[id]; //возвращение сценария по ид
            sc.SmartThingType.SmartThingTypeDictionary = null;
            cont.SmartThingTypeDictionarySet.Remove(sc);    //удаление сценария из бд
            cont.SaveChanges(); //сохранение изменений
        }
    }
}