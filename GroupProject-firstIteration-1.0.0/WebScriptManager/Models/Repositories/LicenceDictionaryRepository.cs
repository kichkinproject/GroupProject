using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebScriptManager.Models.Repositories
{
    public class LicenceDictionaryRepository
    {
        private ScriptModelContainer1 cont;
        static private LicenceDictionaryRepository current = null;
        /// <summary>
        /// Создается экземпляр репозитория для <see cref="LicenceDictionary"/>
        /// </summary>
        private LicenceDictionaryRepository()
        {
            cont = ContainerSingleton.GetContainer();
        }
        /// <summary>
        /// Получение репозитория для <see cref="LicenceDictionary"/>, позволяющего взаимодействовать со сценариями, хранящимися в базе данных
        /// </summary>
        /// <returns></returns>
        static public LicenceDictionaryRepository GetRepository()
        {
            if (current == null)
                current = new LicenceDictionaryRepository();
            return current;
        }
        /// <summary>
        /// Возвращение коллекции лицензий с их ограничениями из базы данных
        /// </summary>
        /// <returns>коллекция</returns>
        public IEnumerable<LicenceDictionary> Licences()
        {
            return cont.LicenceDictionarySet;
            
        }
        /// <summary>
        /// Получение записи из словаря лицензий из базы данных по ИД
        /// </summary>
        /// <param name="id">ИД записи в базе данных</param>
        /// <returns>соответствующая запись</returns>
        public LicenceDictionary this[long id]
        {
            get
            {
                LicenceDictionary sc = cont.LicenceDictionarySet.Find(id);    //сценарий ищется в БД
                if (sc == null)
                    throw new Exceptions.NoElementException("Запись в словаре лицензий");   //сценарий не найден
                else
                    return sc;  //найден
            }
        }

        public LicenceDictionary this [Licence _licence]
        {
            get
            {
                var list = from c in cont.LicenceDictionarySet where c.LicenceType == _licence select c;
                if (list.Count() == 0)
                    throw new Exceptions.NoElementException("Запись в словаре лицензий");
                return list.First();
            }
        }
       
        /// <summary>
        /// добавление новой записи в словарь лицензий
        /// </summary>
        /// <param name="_licence">тип лицензии</param>
        /// <param name="_upperBound">верхнее ограничение</param>
        /// <returns>соответстующую запись</returns>
        /// <exception cref="Exceptions.CreatingException"> в ситуации, когда запись с данной лицензией уже существует</exception>
        public LicenceDictionary AddLicenceDictionary(Licence _licence, long _upperBound )
        {
            IEnumerable<LicenceDictionary> list = from c in cont.LicenceDictionarySet where c.LicenceType == _licence select c;
            if (list.Count() > 0)
                throw new Exceptions.CreatingException("Запись в словарь лицензий ");
            LicenceDictionary addingRecord = new LicenceDictionary()
            {
                LicenceType = _licence,
                UpperBound = _upperBound
            };
            cont.LicenceDictionarySet.Add(addingRecord);
            return addingRecord;
        }
        /// <summary>
        /// Позволяет корректировать запись о лицензии
        /// </summary>
        /// <param name="id">Id корректируемой лицензии</param>
        /// <param name="_licence">новое значение <see cref="Licence"/><Li</param>
        /// <param name="_upperBound">новое значение верхней границы</param>
        public void EditLicenceDictionary(long id, Licence _licence , long _upperBound)
        {
            LicenceDictionary sc = this[id];
            if (_licence != sc.LicenceType)
                if ((from c in cont.LicenceDictionarySet where c.LicenceType == _licence select c).ToList().Count() > 0)
                    throw new Exceptions.CreatingException("Запись в словарь лицензий");
            sc.LicenceType = _licence;
            sc.UpperBound = _upperBound;
            cont.SaveChanges();
        }
        /// <summary>
        /// исправление записи в словаре лицензий по <see cref="Licence"/>
        /// </summary>
        /// <param name="_licence">искомая <see cref="Licence"/></param>
        /// <param name="_upperBound">новое значение верхней границы</param>
        /// <exception cref="Exceptions.NoElementException"> в случае, если искомой лицензии нет в словаре</exception>
        public void EditLicenceDictionary(Licence _licence, long _upperBound)
        {
            LicenceDictionary sc = this[_licence];
            sc.UpperBound = _upperBound;
            cont.SaveChanges();
        }
        /// <summary>
        /// Метод, позволяющий удалять запись о <see cref="Licence"/> из базы данных
        /// </summary>
        /// <param name="id">ИД записи о лицензии в БД</param>
        /// <exception cref="Exceptions.NoElementException"> в ситуации, когда записи с данным Id не существует</exception>
        public void DeleteLicenceDictionary(long id)
        {
            LicenceDictionary sc = this[id]; //возвращение сценария по ид
            cont.LicenceDictionarySet.Remove(sc);    //удаление сценария из бд
            cont.SaveChanges(); //сохранение изменений
        }

        /// <summary>
        /// Метод, позволяющий удалять запись о <see cref="Licence"/> из базы данных
        /// </summary>
        /// <param name="_licence">искомая <see cref="Licence"/></param>
        /// <exception cref="Exceptions.NoElementException"> в ситуации, когда записи данной лицензии не существует</exception>
        public void DeleteLicenceDictionary(Licence _licence)
        {
            LicenceDictionary sc = this[_licence];
            cont.LicenceDictionarySet.Remove(sc);
            cont.SaveChanges();
        }
    }
}