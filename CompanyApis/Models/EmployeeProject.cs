//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CompanyApis.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmployeeProject
    {
        public int Id { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<int> ProjectId { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Project Project { get; set; }
    }
}
