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

    public partial class LicenceDictionary
    {
        [HiddenInput(DisplayValue = false)]
        public long Id { get; set; }

        [Display(Name = "Тип лицензии")]

        [Required(ErrorMessage = "Не допускает пустое значение")]
        public Licence LicenceType { get; set; }

        [Display(Name = "Верхнее значение")]
        public double UpperBound { get; set; }
    }
}
