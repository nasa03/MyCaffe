//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyCaffe.db.temporal
{
    using System;
    using System.Collections.Generic;
    
    public partial class ValueStream
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<byte> ValueTypeID { get; set; }
        public Nullable<byte> ClassTypeID { get; set; }
        public Nullable<short> Ordering { get; set; }
        public Nullable<int> SourceID { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<int> SecondsPerStep { get; set; }
        public Nullable<int> TotalSteps { get; set; }
    }
}
