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
    
    public partial class SmartThingType
    {
        public SmartThingType()
        {
            this.Scenaries = new HashSet<Scenario>();
            this.SmartThings = new HashSet<SmartThing>();
        }
    
        public long Id { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Scenario> Scenaries { get; set; }
        public virtual SmartThingTypeDictionary SmartThingTypeDictionary { get; set; }
        public virtual ICollection<SmartThing> SmartThings { get; set; }
    }
}
