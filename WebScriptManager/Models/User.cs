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
    
    public partial class User
    {
        public User()
        {
            this.Scenaries = new HashSet<Scenario>();
        }
    
        public long Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FIO { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public UserType UserType { get; set; }
    
        public virtual ICollection<Scenario> Scenaries { get; set; }
        public virtual UserGroup UserGroup { get; set; }
    }
}
