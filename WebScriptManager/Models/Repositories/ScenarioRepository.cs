using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WebScriptManager.Models.Repositories
{
    public class ScenarioRepository    //класс для работы со сценариями из базы данных
    {
        private ScriptModelContainer1 cont;
        static private ScenarioRepository current = null;
        private ScenarioRepository()
        {
            cont = ContainerSingleton.GetContainer();
        }
        static public ScenarioRepository GetRepository()
        {
            if (current == null)
                current = new ScenarioRepository();
            return current;
        }
        public IEnumerable<Scenario> Scenarios()    //коллекция сценариев
        {
            return cont.ScenarioSet.OrderByDescending(c => c.LastUpdate);   //коллекция отсортированная по дате последнего обновления в порядке убывания
        }
        public Scenario this[long id]   //возвращение сценария по идентификатору
        {
            get
            {
                Scenario sc = cont.ScenarioSet.Find(id);    //сценарий ищется в БД
                if (sc == null)
                    throw new Exceptions.NoElementException("Cценарий");   //сценарий не найден
                else
                    return sc;  //найден
            }
        }
        public Scenario AddScenario(
            string _name, string _script, Modificator _mod, IEnumerable<ControlBox> _controllers = null,
            IEnumerable<SensorType> _sensors = null, IEnumerable<SmartThingType> _things = null, string _description = "", Admin _admin = null, User _integrator = null
            )   //добавление сценариев в БД
        {
            if (_admin != null && _integrator != null)
            {
                Scenario addScenario = new Scenario //создание нового сценария
                {
                    Name = _name,
                    Description = _description, //по умолчанию описание отсутствует, иначе передаваемое значение
                    ScriptFile = _script,
                    Access = _mod,
                    LastUpdate = new DateTime() //устанавливает текущие дату и время
                };
                if (_controllers != null)  //если для сценария определены контроллеры
                {
                    foreach (ControlBox cb in _controllers) //обход контроллеров
                    { //создается связь между контроллером и сценарием
                        cb.Scenaries.Add(addScenario);
                        addScenario.ControlBoxes.Add(cb);
                    }
                }
                if (_sensors != null)
                {
                    foreach (SensorType st in _sensors) //обход типов датчиков
                    {   //создается связь между типом датчиков и сценарием
                        st.Scenaries.Add(addScenario);
                        addScenario.SensorTypes.Add(st);
                    }
                }
                if (_things != null)
                {
                    foreach (SmartThingType stt in _things) //обход типов умных объектов
                    {   //создание связи между типом умных объектов и сценарием
                        stt.Scenaries.Add(addScenario);
                        addScenario.SmartThingTypes.Add(stt);
                    }
                }
                if (_admin != null) //создатель администратор
                    _admin.Scenaries.Add(addScenario);
                if (_integrator != null && _integrator.UserType == UserType.Integrator) //создатель интегратор
                    _integrator.Scenaries.Add(addScenario);
                cont.ScenarioSet.Add(addScenario);  //запись нового сценария в БД
                cont.SaveChanges(); //сохранение изменений
                return addScenario;
            }
            else throw new Exceptions.CreatingException("Сценарий"); //нужно создать ошибку создания (CreatingException) и заменить возвращение null на нее
        }
        public void EditScenario(long id, string _name, string _script, Modificator _mod, string _description = "")
        {
            Scenario scenario = this[id];   //получает сценарий по ид
            scenario.Name = _name;
            scenario.Description = _description;
            scenario.ScriptFile = _script;
            scenario.Access = _mod;
            scenario.LastUpdate = new DateTime();   //устанавливает текущие дату и время
            cont.SaveChanges();
        }
        public void DeleteScenario(long id)
        {
            Scenario sc = this[id]; //возвращение сценария по ид
            for (int i = sc.ControlBoxes.Count - 1; i >= 0; i--)    //обход контроллеров, удаление связей со сценарием
            {
                ControlBox cb = sc.ControlBoxes.ElementAt(i);
                cb.Scenaries.Remove(sc);
                sc.ControlBoxes.Remove(cb);
            }
            for (int i = sc.SmartThingTypes.Count - 1; i >= 0; i--)  //обход типов умных объектов, удаление связей со сценарием
            {
                SmartThingType stt = sc.SmartThingTypes.ElementAt(i);
                stt.Scenaries.Remove(sc);
                sc.SmartThingTypes.Remove(stt);
            }
            for (int i = sc.SensorTypes.Count - 1; i >= 0; i--) //обход типов датчиков, удаление связей со сценарием
            {
                SensorType st = sc.SensorTypes.ElementAt(i);
                st.Scenaries.Remove(sc);
                sc.SensorTypes.Remove(st);
            }
            if (sc.Admin != null)   //если создатель - админ, удаление связи со сценарием
            {
                sc.Admin.Scenaries.Remove(sc);
            }
            if (sc.User != null)    //если создатель - интегратор, удаление связи со сценарием
            {
                sc.User.Scenaries.Remove(sc);
            }
            cont.ScenarioSet.Remove(sc);    //удаление сценария из бд
            cont.SaveChanges(); //сохранение изменений
        }
    }
}