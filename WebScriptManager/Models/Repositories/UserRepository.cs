using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebScriptManager.Models.Repositories
{
    public class UserRepository //класс для работы с интеграторами и пользователями из базы данных
    {
        private ScriptModelContainer1 cont;
        static private UserRepository current = null;
        private UserRepository()  //конструктор
        {
            cont = ContainerSingleton.GetContainer();
        }
        static public UserRepository GetRepository()
        {
            if (current == null)
                current = new UserRepository();
            return current;
        }
        public UserRepository(ScriptModelContainer1 _cont)  //конструктор
        {
            cont = _cont;
        }
        public IEnumerable<User> Integrators()  //коллекция интеграторов
        {
            return (from integr in cont.UserSet where integr.UserType == UserType.Integrator select integr).OrderBy(c => c.Login);  //пользователи типа интегратор, отсортированные по логинам
        }
        public IEnumerable<User> Users()    //коллекция пользователей
        {
            return (from us in cont.UserSet where us.UserType == UserType.Simple select us).OrderBy(c => c.Login);  //пользователи типа single, отсортированные по логинам
        }
        public User this[long id]   //поиск пользователя по ид
        {
            get
            {
                User user = cont.UserSet.Find(id);
                if (user != null) //пользователь найден
                    return user;
                else
                    throw new Exceptions.NoElementException("Пользователь");  //если нет, то ошибка
            }
        }
        public User AddUser(string _login, string _password, string _fio, UserType _type, UserGroup _group, string _phone = "", string _mail = "")  //добавление новых (пользователей || интеграторов)
        {
            User addedUser = new User   //создание пользователя
            {
                Login = _login,
                Password = _password,
                FIO = _fio,
                Phone = _phone, //необязательный параметр
                Mail = _mail,   //необязательный параметр
                UserType = _type
            };
            _group.Users.Add(addedUser);    //добавление пользователя в группу
            cont.UserSet.Add(addedUser);    //добавление в базу данных
            cont.SaveChanges(); //сохранение изменений
            return addedUser;
        }
        public void EditUser(long id, string _password, string _fio, UserType _type, string _phone = "", string _mail = "") //редактирование информации о (пользователе || интеграторе)
        {
            User user = this[id];  //получение пользователя
            user.Password = _password;  //внесение изменений
            user.FIO = _fio;
            user.Phone = _phone;
            user.Mail = _mail;
            user.UserType = _type;
            cont.SaveChanges(); //сохранение изменений
        }
        public void DeleteUser(long id) //удаление (пользователя || интегратора)
        {
            User user = this[id];   //получение пользователя
            user.UserGroup.Users.Remove(user);   //удаление пользователя из группы
            if (user.Scenaries.Count != 0)   //интегратор создавал сценарии
            {
                ScenarioRepository scenRep = ScenarioRepository.GetRepository();  //инструмент для удаления сценариев
                for (int i = user.Scenaries.Count - 1; i >= 0; i--)
                {
                    scenRep.DeleteScenario(user.Scenaries.ElementAt(i).Id);  //удаление сценария из БД по ид
                }
            }
            cont.UserSet.Remove(user);  //удаляется пользователь
            cont.SaveChanges(); //изменения сохраняются
        }
    }
}