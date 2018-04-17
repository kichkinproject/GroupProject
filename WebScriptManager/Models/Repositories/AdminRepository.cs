using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebScriptManager.Models.Repositories
{
    public class AdminRepository    //класс для работы с администраторами из базы данных
    {
        private ScriptModelContainer1 cont;
        static private AdminRepository current = null;
        private AdminRepository()  //конструктор
        {
            cont = ContainerSingleton.GetContainer();
        }
        static public AdminRepository GetRepository()
        {
            if (current == null)
                current = new AdminRepository();
            return current;
        }
        public IEnumerable<Admin> Admins()  //коллекция администраторов в системе
        {
            return cont.AdminSet.OrderBy(c => c.Login); //коллекция отсортированная по логинам
        }
        
        public Admin this[long id]  //возвращение администратора по ид
        {
            get
            {
                Admin ad = cont.AdminSet.Find(id);  
                if (ad != null) //администратор найден
                    return ad;
                else
                    throw new Exceptions.NoElementException("Администратор");  //если нет, то ошибка
            }
        }
        public Admin this[string _login]  //возвращение администратора по логину
        {
            get
            {
                foreach (Admin ad in cont.AdminSet)
                    if (ad.Login == _login)  //админ с логином найден
                        return ad;
                throw new Exceptions.NoElementException("Администратор");  //админ с логином не найден
            }
        }
        public Admin AddAdmin(string _login, string _password, string _fio, string _phone = "", string _mail = "", string _finger = "") //добавление администратора
        {
            Admin admin = new Admin //создание нового администратора
            {
                Login = _login,
                Password = _password,
                FIO = _fio,
                Phone = _phone,
                Mail = _mail,
                Fingerprint = _finger    //по умолчанию "", если значение передается - записывается значение
            };
            cont.AdminSet.Add(admin);   //запись в базу данных
            cont.SaveChanges(); //сохранение изменений
            return admin;   //возвращение нового администратора
        }
        public bool IsAdmin(string _login, string _password)  //проверка поданных логина и пароля для идентификации
        {
            Admin admin = this[_login];   //нахождение по логину
            if (admin != null)
            {   //логин совпал
                if (admin.Password == _password)
                    return true;    //логин и пароль совпали
            }
            return false;   //логин или пароль не совпали
        }
        public void EditAdmin(long id, string _password, string _fio, string _phone, string _mail, string _finger = "")   //изменение информации об администраторе
        {
            Admin admin = this[id]; //получение администратора по ид
            admin.Password = _password;
            admin.FIO = _fio;
            admin.Phone = _phone;
            admin.Mail = _mail;
            admin.Fingerprint = _finger;
            cont.SaveChanges(); //сохранение изменений
        }
    }
}