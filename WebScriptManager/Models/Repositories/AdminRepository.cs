﻿using System;
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
        /// <summary>
        /// Получение администраторского репозитория, позволяющего взаимодействовать с администраторами, хранящимися в базе данных
        /// </summary>
        /// <returns></returns>
        static public AdminRepository GetRepository()
        {
            if (current == null)
                current = new AdminRepository();
            return current;
        }
        /// <summary>
        /// Возвращение коллекции администраторов в базе данных, отсортированных по администраторским логинам
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Admin> Admins()  //коллекция администраторов в системе
        {
            return cont.AdminSet.OrderBy(c => c.Login); //коллекция отсортированная по логинам
        }
        /// <summary>
        /// Получение администратора из базы данных по ИД
        /// </summary>
        /// <param name="id">ИД администратора в базе данных</param>
        /// <returns></returns>
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
        /// <summary>
        /// Получение администратора из базы данных по логину
        /// </summary>
        /// <param name="_login">Логин администратора, по которому будет вестись поиск</param>
        /// <returns></returns>
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
        /// <summary>
        /// Добавление нового администратора в базу данных / облако
        /// </summary>
        /// <param name="_login">Логин администратора в системе</param>
        /// <param name="_password">Пароль, соответствующий администратору с указанным логином</param>
        /// <param name="_fio">Фамилия, имя и отчество зарегестрированного администратора</param>
        /// <param name="_phone">Телефон администратора, необязательный параметр. В дальнейшем можно реализовать расширенную аутентификацию с отправлением СМС на указанный номер</param>
        /// <param name="_mail">Электронная почта администратора, необязательный параметр. В дальнейшем можно реализовать расширенную аутентификацию с отправлением кода на почту</param>
        /// <param name="_finger">Отпечаток пальца, необязательный параметр. Можно использовать для ускоренной идентификации администратора, чтобы не возникло ситуации, что администратора взломают</param>
        /// <returns></returns>
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
        /// <summary>
        /// Данная функция сообщает, будет ли идентифицирован администратор (по логину и паролю)
        /// </summary>
        /// <param name="_login">Логин администратора</param>
        /// <param name="_password">Пароль для администратора</param>
        /// <returns></returns>
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
        /// <summary>
        /// Данная функция позволяет редактировать информацию об администраторе
        /// </summary>
        /// <param name="id">ИД администратора в БД</param>
        /// <param name="_password">Измененный пароль</param>
        /// <param name="_fio">Измененный ФИО</param>
        /// <param name="_phone">Измененный телефон</param>
        /// <param name="_mail">Измененная электронная почта</param>
        /// <param name="_finger">Измененный отпечаток пальца</param>
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