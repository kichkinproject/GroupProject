using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebScriptManager.Models.Repositories;

namespace WebScriptManager.Models
{
    public class ContainerSingleton
    {
        static private ScriptModelContainer1 cont = null;
        static public ScriptModelContainer1 GetContainer()
        {
            if (cont == null)
                cont = new ScriptModelContainer1();
            return cont;
        }
        static private AdminRepository adminRepository = AdminRepository.GetRepository();
        static public AdminRepository AdminRepository
        {
            get
            {
                return adminRepository;
            }
        }
        static private ControlBoxRepository controlBoxRepository = ControlBoxRepository.GetRepository();
        static public ControlBoxRepository GetControlBoxRepository
        {
            get
            {
                return controlBoxRepository;
            }
        }
        static private ScenarioRepository scenarioRepository = ScenarioRepository.GetRepository();
        static public ScenarioRepository ScenarioRepository
        {
            get
            {
                return scenarioRepository;
            }
        }
        static private SensorTypeRepository sensorTypeRepository = SensorTypeRepository.GetRepository();
        static public SensorTypeRepository SensorTypeRepository
        {
            get
            {
                return sensorTypeRepository;
            }
        }
        static private SmartPlaceRepository smartPlaceRepository = SmartPlaceRepository.GetRepository();
        static public SmartPlaceRepository SmartPlaceRepository
        {
            get
            {
                return smartPlaceRepository;
            }
        }
        static private SmartThingTypeRepository smartThingTypeRepository = SmartThingTypeRepository.GetRepository();
        static public SmartThingTypeRepository SmartThingTypeRepository
        {
            get
            {
                return smartThingTypeRepository;
            }
        }
        static private UserGroupRepository userGroupRepository = UserGroupRepository.GetRepository();
        static public UserGroupRepository UserGroupRepository
        {
            get
            {
                return userGroupRepository;
            }
        }
        static private UserRepository userRepository = UserRepository.GetRepository();
        static public UserRepository UserRepository
        {
            get
            {
                return userRepository;
            }
        }
    }
}