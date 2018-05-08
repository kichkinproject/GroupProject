using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebScriptManager.Models.Repositories
{
    /// <summary>
    /// Репозиторий групп пользователей, позволяющий выполнять различные операции над группами пользователей в базе данных
    /// </summary>
    public class UserGroupRepository
    {
        private ScriptModelContainer1 cont;
        static private UserGroupRepository current = null;
        /// <summary>
        /// Создается экземпляр репозитория для групп пользователей
        /// </summary>
        private UserGroupRepository()
        {
            cont = ContainerSingleton.GetContainer();
        }
        /// <summary>
        /// Получение репозитория групп пользователей, позволяющего взаимодействовать с группами пользователей, хранящимися в базе данных
        /// </summary>
        /// <returns></returns>
        static public UserGroupRepository GetRepository()
        {
            if (current == null)
                current = new UserGroupRepository();
            return current;
        }
        /// <summary>
        /// Возвращение коллекции групп пользователей, хранящихся в базе данных, отсортированных по имени
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserGroup> UserGroups()
        {
            return cont.UserGroupSet.OrderBy(c => c.Name); //коллекция отсортированная по именам групп пользователей
        }
        /// <summary>
        /// Возвращение группы пользователей по идентификатору в базе данных
        /// </summary>
        /// <param name="id">Уникальный идентификатор группы пользователей в базе данных</param>
        /// <returns></returns>
        public UserGroup this[long id]
        {
            get
            {
                UserGroup group = cont.UserGroupSet.Find(id);
                if (group != null) //пользователь найден
                    return group;
                else
                    throw new Exceptions.NoElementException("Группа пользователей");  //если нет, то ошибка
            }
        }
        /// <summary>
        /// Метод, позволяющий добавлять новые группы пользователей в базу данных
        /// </summary>
        /// <param name="_name">Имя группы пользователей</param>
        /// <param name="_licence">Лицензия, назначенная для данной группы пользователей</param>
        /// <param name="_parent">Родительская группа пользователей для данной группы пользователей</param>
        /// <returns></returns>
        public UserGroup AddGroup(string _name, Licence _licence, UserGroup _parent = null)
        {
            UserGroup group = new UserGroup //создание группы
            {
                Name = _name,
                Licence = _licence  //определение лицензии для группы пользователей
            };
            if (_parent != null)    //необязательный параметр, родитель группы пользователей
            {
                _parent.Children.Add(group);  //добавление потомственной группы (потомок и родитель перепутаны)
            }
            cont.UserGroupSet.Add(group);   //добавление записи в базу 
            cont.SaveChanges(); //сохранение изменений
            return group;
        }
        /// <summary>
        /// Метод, позволяющий редактировать информацию о группе пользователей
        /// </summary>
        /// <param name="id">Уникальный идентификатор группы пользователей в базе данных</param>
        /// <param name="_name">Имя группы пользователей</param>
        /// <param name="_licence">Лицензия, назначенная для данной группы пользователей</param>
        public void EditGroup(long id, string _name, Licence _licence)
        {
            UserGroup group = this[id]; //поиск группы пользователей по ид
            group.Name = _name;
            group.Licence = _licence;
            cont.SaveChanges(); //сохранение изменений
        }
        /// <summary>
        /// Метод, позволяющий удалять группы пользователей из базы данных
        /// </summary>
        /// <param name="id">Уникальный идентификатор группы пользователей в базе данных</param>
        public void DeleteGroup(long id)
        {
            UserGroup group = this[id]; //поиск группы пользователей по ид
            if (group.Children != null) //если у группы есть родитель 
            {
                group.Parent.Children.Remove(group);  //удаление текущей группы из ее родительской группы  
            }
            if (group.SmartPlaces.Count != 0)    //если для группы определены умные места
            {
                //удаление из базы данных умных мест, принадлежащих данной группе пользователей
            }
            if (group.ControlBoxes.Count != 0)    //если для группы определены контроллеры
            {
                ControlBoxRepository controllerRep = ControlBoxRepository.GetRepository();    //инструмент для удаления контроллеров из БД
                for (int i = group.ControlBoxes.Count - 1; i >= 0; i--)
                {
                    controllerRep.DeleteControlBox(group.ControlBoxes.ElementAt(i).Id); //удаление контроллера из БД по ИД
                }
            }
            if (group.Users.Count != 0)  //если для группы определены пользователи
            {
                UserRepository userRep = UserRepository.GetRepository();  //инструмент для удаления пользователей
                for (int i = group.Users.Count - 1; i >= 0; i--)
                {
                    userRep.DeleteUser(group.Users.ElementAt(i).Id); //удаление пользователя из БД по ИД
                }
            }
            if (group.Children.Count != 0)    //если для группы определены потомки
            {
                for (int i = group.Children.Count - 1; i >= 0; i--)
                {
                    this.DeleteGroup(group.Children.ElementAt(i).Id); //удаление потомственной группы пользователей из БД по ИД 
                }
            }
            cont.UserGroupSet.Remove(group);    //удаление текущей группы
            cont.SaveChanges(); //сохранение изменений
        }
    }
}