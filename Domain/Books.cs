//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class Books
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string IsEnable { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string Creater { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string Updater { get; set; }
        public Nullable<System.DateTime> PublishDate { get; set; }
        public string Publisher { get; set; }
    }
}