using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebScriptManager.Models.Repositories;

namespace WebScriptManager.Models
{
    public class ContainerSingleton //контейнер для получения всех репозиториев
    {
        static private ScriptModelContainer1 cont = null;
        static public ScriptModelContainer1 GetContainer()  //паттерн - синглтон
        {
            if (cont == null)
                cont = new ScriptModelContainer1();
            return cont;
        }
        static private AdminRepository adminRepository = AdminRepository.GetRepository();
        static public AdminRepository AdminRepository   //получение администраторского репозитория
        {
            get
            {
                return adminRepository;
            }
        }
        static private ControlBoxRepository controlBoxRepository = ControlBoxRepository.GetRepository();
        static public ControlBoxRepository GetControlBoxRepository  //получение контроллерского репозитория
        {
            get
            {
                return controlBoxRepository;
            }
        }
        static private ScenarioRepository scenarioRepository = ScenarioRepository.GetRepository();
        static public ScenarioRepository ScenarioRepository //получение репозитория для сценариев
        {
            get
            {
                return scenarioRepository;
            }
        }
        static private SensorTypeRepository sensorTypeRepository = SensorTypeRepository.GetRepository();
        static public SensorTypeRepository SensorTypeRepository //получение репозитория для типов датчиков
        {
            get
            {
                return sensorTypeRepository;
            }
        }
        static private SmartPlaceRepository smartPlaceRepository = SmartPlaceRepository.GetRepository();
        static public SmartPlaceRepository SmartPlaceRepository //получение репозитория для умного места
        {
            get
            {
                return smartPlaceRepository;
            }
        }
        static private SmartThingTypeRepository smartThingTypeRepository = SmartThingTypeRepository.GetRepository();
        static public SmartThingTypeRepository SmartThingTypeRepository //получение репозитория для типа умного объекта
        {
            get
            {
                return smartThingTypeRepository;
            }
        }
        static private UserGroupRepository userGroupRepository = UserGroupRepository.GetRepository();
        static public UserGroupRepository UserGroupRepository   //получение репозитория для группы пользователей
        {
            get
            {
                return userGroupRepository;
            }
        }
        static private UserRepository userRepository = UserRepository.GetRepository();
        static public UserRepository UserRepository //получение репозитория для пользователей
        {
            get
            {
                return userRepository;
            }
        }
    }
}