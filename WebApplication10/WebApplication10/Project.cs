//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication10
{
    using System;
    using System.Collections.Generic;
    
    public partial class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public Nullable<System.DateTime> ProjectStartDate { get; set; }
        public Nullable<System.DateTime> ProjectEndDate { get; set; }
        public string CreatorEmail { get; set; }
        public Nullable<System.DateTime> ActualProjectEndDate { get; set; }
        public Nullable<int> Progress { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> TimestampCreated { get; set; }
    }
}
