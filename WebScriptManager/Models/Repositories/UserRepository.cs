using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebScriptManager.Models.Repositories
{
    /// <summary>
    /// Пользовательский репозиторий, позволяющий выполнять различные операции над пользователями и интеграторами в базе данных
    /// </summary>
    public class UserRepository
    {
        private ScriptModelContainer1 cont;
        static private UserRepository current = null;
        /// <summary>
        /// Создается экземпляр пользовательского репозитория
        /// </summary>
        private UserRepository()
        {
            cont = ContainerSingleton.GetContainer();
        }
        /// <summary>
        /// Получение пользовательского репозитория, позволяющего взаимодействовать с пользователями и интеграторами, хранящимися в базе данных
        /// </summary>
        /// <returns></returns>
        static public UserRepository GetRepository()
        {
            if (current == null)
                current = new UserRepository();
            return current;
        }
        /// <summary>
        /// Возвращение коллекции интеграторов, хранящихся в базе данных, отсортированных по логину
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> Integrators()
        {
            return (from integr in cont.UserSet where integr.UserType == UserType.Integrator select integr).OrderBy(c => c.Login);  //пользователи типа интегратор, отсортированные по логинам
        }
        /// <summary>
        /// Возвращение коллекции пользователей, хранящихся в базе данных, отсортированных по логину
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> Users()
        {
            return (from us in cont.UserSet where us.UserType == UserType.Simple select us).OrderBy(c => c.Login);  //пользователи типа single, отсортированные по логинам
        }
        /// <summary>
        /// Возвращение пользователя или интегратора по идентификатору в базе данных
        /// </summary>
        /// <param name="id">ИД пользователя в базе данных</param>
        /// <returns></returns>
        public User this[long id]
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
        /// <summary>
        /// Метод, позволяющий добавлять новых пользователей или интеграторов в базу данных
        /// </summary>
        /// <param name="_login">Логин пользователя, под которым он будет идентифицироваться в системе</param>
        /// <param name="_password">Пароль пользователя, для прохождения идентификации</param>
        /// <param name="_fio">Фамилия Имя Отчество пользователя</param>
        /// <param name="_type">Тип пользователя, определяющий, является ли данный пользователь интегратором или простым пользователем</param>
        /// <param name="_group">Группа, к которой принадлежит данный пользователь</param>
        /// <param name="_phone">Телефон пользователя. В дальнейшем можно реализовать расширенную аутентификацию с отправлением СМС на указанный номер</param>
        /// <param name="_mail">Электронная почта пользователя. В дальнейшем можно реализовать расширенную аутентификацию с отправлением кода на почту</param>
        /// <returns></returns>
        public User AddUser(string _login, string _password, string _fio, UserType _type, UserGroup _group, string _phone = "", string _mail = "")
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
        /// <summary>
        /// Метод, позволяющий редактировать информацию об пользователе
        /// </summary>
        /// <param name="id">ИД пользователя в базе данных</param>
        /// <param name="_password">Пароль пользователя, для прохождения идентификации</param>
        /// <param name="_fio">Фамилия Имя Отчество пользователя</param>
        /// <param name="_type">Тип пользователя, определяющий, является ли данный пользователь интегратором или простым пользователем</param>
        /// <param name="_phone">Телефон пользователя. В дальнейшем можно реализовать расширенную аутентификацию с отправлением СМС на указанный номер</param>
        /// <param name="_mail">Электронная почта пользователя. В дальнейшем можно реализовать расширенную аутентификацию с отправлением кода на почту</param>
        public void EditUser(long id, string _password, string _fio, UserType _type, string _phone = "", string _mail = "")
        {
            User user = this[id];  //получение пользователя
            user.Password = _password;  //внесение изменений
            user.FIO = _fio;
            user.Phone = _phone;
            user.Mail = _mail;
            user.UserType = _type;
            cont.SaveChanges(); //сохранение изменений
        }
        /// <summary>
        /// Метод, позволяющий удалять пользователей из базы данных
        /// </summary>
        /// <param name="id">ИД пользователя в базе данных</param>
        public void DeleteUser(long id)
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