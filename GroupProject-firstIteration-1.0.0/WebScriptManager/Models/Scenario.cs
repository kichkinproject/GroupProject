//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebScriptManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public partial class Scenario
    {
        public Scenario()
        {
            this.SensorTypes = new HashSet<SensorType>();
            this.SmartThingTypes = new HashSet<SmartThingType>();
            this.ControlBoxes = new HashSet<ControlBox>();
        }


        [HiddenInput(DisplayValue = false)]
        public long Id { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длинна должна быть от 3 до 50 символов")]
        [Required(ErrorMessage = "Не допускает пустое значение")]
        [RegularExpression(@"^(.*[<>].*)", ErrorMessage = "Введены не допустимые символы")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        [RegularExpression(@"^(.*[<>].*)", ErrorMessage = "Введены не допустимые символы")]
        [StringLength(255, ErrorMessage ="Длинна должна быть до 255 символов")]
        public string Description { get; set; }


        [RegularExpression(@"^(.*[<>].*)", ErrorMessage = "Введены не допустимые символы")]
        [Required(ErrorMessage = "Не допускает пустое значение")]
        [StringLength(1000, ErrorMessage ="длинна не может превосходить 1000")]
        [Display(Name = "Адрес сценария")]
        public string ScriptFile { get; set; }

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
