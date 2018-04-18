using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebScriptManager.Models.Repositories;

namespace WebScriptManager.Models
{
    /// <summary>
    /// Контейнер, позволяющий работать со всеми репозиториями, реализующими работу с базой данных
    /// </summary>
    public class ContainerSingleton
    {
        static private ScriptModelContainer1 cont = null;
        /// <summary>
        /// Метод, возвращающий контейнер базы данных
        /// </summary>
        /// <returns></returns>
        static public ScriptModelContainer1 GetContainer()
        {
            if (cont == null)
                cont = new ScriptModelContainer1();
            return cont;
        }
        static private AdminRepository adminRepository = AdminRepository.GetRepository();
        /// <summary>
        /// Получение администраторского репозитория для выполнения операций над адмиистраторами
        /// </summary>
        static public AdminRepository AdminRepository
        {
            get
            {
                return adminRepository;
            }
        }
        static private ControlBoxRepository controlBoxRepository = ControlBoxRepository.GetRepository();
        /// <summary>
        /// Получение репозитория контроллеров для выполнения операций над контроллерами
        /// </summary>
        static public ControlBoxRepository GetControlBoxRepository
        {
            get
            {
                return controlBoxRepository;
            }
        }
        static private ScenarioRepository scenarioRepository = ScenarioRepository.GetRepository();
        /// <summary>
        /// Получение репозитория сценариев для выполнения операций над сценариями
        /// </summary>
        static public ScenarioRepository ScenarioRepository
        {
            get
            {
                return scenarioRepository;
            }
        }
        static private SensorTypeRepository sensorTypeRepository = SensorTypeRepository.GetRepository();
        /// <summary>
        /// Получение репозитория типов датчиков для выполнения операций над типами датчиков
        /// </summary>
        static public SensorTypeRepository SensorTypeRepository
        {
            get
            {
                return sensorTypeRepository;
            }
        }
        static private SmartPlaceRepository smartPlaceRepository = SmartPlaceRepository.GetRepository();
        /// <summary>
        /// Получение репозитория умных мест для выполнения операций над умными местами
        /// </summary>
        static public SmartPlaceRepository SmartPlaceRepository
        {
            get
            {
                return smartPlaceRepository;
            }
        }
        static private SmartThingTypeRepository smartThingTypeRepository = SmartThingTypeRepository.GetRepository();
        /// <summary>
        /// Получение репозитория типов умных объектов для выполнения операций над типами умных объектов
        /// </summary>
        static public SmartThingTypeRepository SmartThingTypeRepository
        {
            get
            {
                return smartThingTypeRepository;
            }
        }
        static private UserGroupRepository userGroupRepository = UserGroupRepository.GetRepository();
        /// <summary>
        /// Получение репозитория групп пользователей для выполнения операций над группами пользователей
        /// </summary>
        static public UserGroupRepository UserGroupRepository
        {
            get
            {
                return userGroupRepository;
            }
        }
        static private UserRepository userRepository = UserRepository.GetRepository();
        /// <summary>
        /// Получение пользовательского репозитория для выполнения операций над пользователями и администраторами
        /// </summary>
        static public UserRepository UserRepository
        {
            get
            {
                return userRepository;
            }
        }

    }
}