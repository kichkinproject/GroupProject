using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebScriptManager.Models.Repositories
{
    public class SmartThingTypeRepository
    {
        private ScriptModelContainer1 cont;
        static private SmartThingTypeRepository current = null;
        private SmartThingTypeRepository()  //конструктор
        {
            cont = ContainerSingleton.GetContainer();
        }
        static public SmartThingTypeRepository GetRepository()
        {
            if (current == null)
                current = new SmartThingTypeRepository();
            return current;
        }
        public SmartThingTypeRepository(ScriptModelContainer1 _cont)   //конструктор
        {
            cont = _cont;
        }
        public IEnumerable<SmartThingType> SmartPlaces()    //коллекция тип умного объекта
        {
            return cont.SmartThingTypeSet.OrderBy(c => c.Name);   //коллекция отсортированная по ИД
        }
        public SmartThingType this[long id]   //возвращение типа умного объекта по идентификатору
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
    }
}