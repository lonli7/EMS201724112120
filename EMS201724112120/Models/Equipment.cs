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
    
    public partial class Equipment
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string specs { get; set; }
        public byte[] image { get; set; }
        public Nullable<double> price { get; set; }
        public Nullable<System.DateTime> datatime { get; set; }
        public string location { get; set; }
        public Nullable<int> manager { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}
