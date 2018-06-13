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

    public partial class Admin
    {
        public Admin()
        {
            this.Scenaries = new HashSet<Scenario>();
        }
    
        
        [HiddenInput(DisplayValue = false)]
        public long Id { get; set; }

        [StringLength(50, MinimumLength =3, ErrorMessage ="Длинна должна быть от 3 до 50 символов")]
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Не допускает пустое значение")]
        [RegularExpression("[A-Za-z0-9]*",ErrorMessage ="Введены не допустимые символы")]
        public string Login { get; set; }


        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 7, ErrorMessage = "Длинна должна быть от 7 до 50 символов")]
        [Required(ErrorMessage = "Не допускает пустое значение")]
        [Display(Name = "Пароль")]
        [RegularExpression("[A-Za-z0-9]*", ErrorMessage = "Введены не допустимые символы")]
        public string Password { get; set; }


        [RegularExpression("[A-Za-zА-Яа-я -]*", ErrorMessage = "Введены не допустимые символы")]
        [StringLength(100, ErrorMessage = "Длинна строки не может превосходить 100")]
        [Required(ErrorMessage = "Не допускает пустое значение")]
        [Display(Name = "Фамилия, имя, отчество")]
        public string FIO { get; set; }

        [RegularExpression(@"(\+[2-9]-\d{3}-\d{3}-\d{4}$)|(\+[2-9]-\d{3}-\d{3}-\d{2}-\d(2)$)", ErrorMessage = "Формат номера : +X-XXX-XXX-XXXX или +X-XXX-XXX-XX-XX")]
        [Required(ErrorMessage = "Не допускает пустое значение")]
        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }


        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Не корректный адрес электронной почты")]
        [Required(ErrorMessage = "Не допускает пустое значение")]
        [Display(Name = "Электронная почта")]
        public string Mail { get; set; }

        [Display(Name = "Отпечаток пальца")]
        public string Fingerprint { get; set; }
        
        public virtual ICollection<Scenario> Scenaries { get; set; }
    }
}
