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
    
    public partial class ProjectHistory
    {
        public int ProjectId { get; set; }
        public Nullable<System.DateTime> ProjectStartDate { get; set; }
        public Nullable<System.DateTime> ProjectEndDate { get; set; }
        public int RowId { get; set; }
        public Nullable<System.DateTime> Timestamp { get; set; }
        public Nullable<System.DateTime> ActualProjectEndDate { get; set; }
        public string Status { get; set; }
        public string Progress { get; set; }
    }
}