using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebScriptManager.Models.Repositories
{
    public class UserGroupRepository    //класс для работы с группами пользователей из базы данных
    {
        private ScriptModelContainer1 cont;
        static private UserGroupRepository current = null;
        private UserGroupRepository()  //конструктор
        {
            cont = ContainerSingleton.GetContainer();
        }
        static public UserGroupRepository GetRepository()
        {
            if (current == null)
                current = new UserGroupRepository();
            return current;
        }
        public IEnumerable<UserGroup> UserGroups()  //возвращение списка групп пользователей
        {
            return cont.UserGroupSet.OrderBy(c => c.Name); //коллекция отсортированная по именам групп пользователей
        }
        public UserGroup this[long id]  //получение группы пользователей по ид
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
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_licence"></param>
        /// <param name="_parent"></param>
        /// <returns></returns>
       
        public UserGroup AddGroup(string _name, Licence _licence, UserGroup _parent = null) //добавление новой группы пользователей
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
        public void EditGroup(long id, string _name, Licence _licence)  //редактирование группы пользователей
        {
            UserGroup group = this[id]; //поиск группы пользователей по ид
            group.Name = _name;
            group.Licence = _licence;
            cont.SaveChanges(); //сохранение изменений
        }
        public void DeleteGroup(long id)    //удаление группы из бд
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
                UserRepository userRep = new UserRepository(cont);  //инструмент для удаления пользователей
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