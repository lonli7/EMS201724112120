//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace EMS201724112120.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Department
    {
        public Department()
        {
            this.Employees = new HashSet<Employee>();
        }
    
        public int Id { get; set; }
        public string name { get; set; }
        public Nullable<int> admin { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}