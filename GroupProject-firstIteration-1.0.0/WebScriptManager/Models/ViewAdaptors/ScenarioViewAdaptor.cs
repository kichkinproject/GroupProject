﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebScriptManager.Models.ViewAdaptors
{
    public class ScenarioViewAdaptor
    {
        public ScenarioViewAdaptor()
        {
            this.SensorTypes = new HashSet<SensorType>();
            this.SmartThingTypes = new HashSet<SmartThingType>();
            this.ControlBoxes = new HashSet<ControlBox>();
        }

        public ScenarioViewAdaptor(Scenario input)
        {
            Name = input.Name;
            Id = input.Id;
            Description = input.Description;
            ScriptFile = System.IO.File.ReadAllLines(Controllers.ScenariosController.path+Id);            
            Access = input.Access;
            LastUpdate = input.LastUpdate;
            Admin = input.Admin;
            User = input.User;
            SensorTypes = input.SensorTypes;
            SmartThingTypes = input.SmartThingTypes;
            ControlBoxes = input.ControlBoxes;

        }

        [HiddenInput(DisplayValue = false)]
        public long Id { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длинна должна быть от 3 до 50 символов")]
        [Required(ErrorMessage = "Не допускает пустое значение")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Не допускает пустое значение")]
        [Display(Name = "Код сценария")]
        public IEnumerable<string> ScriptFile { get; set; }

        [Display(Name = "Доступ")]
        [Required(ErrorMessage = "Не допускает пустое значение")]
        public Modificator Access { get; set; }


        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Последнее обновление")]
        public System.DateTime LastUpdate { get; set; }


        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Администратор")]
        public virtual Admin Admin { get; set; }


        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Автор")]
        public virtual User User { get; set; }
        public virtual ICollection<SensorType> SensorTypes { get; set; }
        public virtual ICollection<SmartThingType> SmartThingTypes { get; set; }
        public virtual ICollection<ControlBox> ControlBoxes { get; set; }
    }
}