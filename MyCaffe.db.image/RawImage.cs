//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyCaffe.db.image
{
    using System;
    using System.Collections.Generic;
    
    public partial class RawImage
    {
        public int ID { get; set; }
        public Nullable<int> Height { get; set; }
        public Nullable<int> Width { get; set; }
        public Nullable<int> Channels { get; set; }
        public byte[] Data { get; set; }
        public byte[] DebugData { get; set; }
        public Nullable<System.DateTime> TimeStamp { get; set; }
        public Nullable<bool> Encoded { get; set; }
        public Nullable<int> SourceID { get; set; }
        public Nullable<int> Idx { get; set; }
        public Nullable<int> GroupID { get; set; }
        public Nullable<short> OriginalBoost { get; set; }
        public Nullable<short> ActiveBoost { get; set; }
        public Nullable<bool> AutoLabel { get; set; }
        public Nullable<int> VirtualID { get; set; }
        public byte[] RawData { get; set; }
        public byte[] DataCriteria { get; set; }
        public Nullable<int> OriginalLabel { get; set; }
        public Nullable<int> ActiveLabel { get; set; }
        public Nullable<bool> Active { get; set; }
        public string Description { get; set; }
        public Nullable<byte> DebugDataFormatID { get; set; }
        public Nullable<byte> DataCriteriaFormatID { get; set; }
        public Nullable<int> OriginalSourceID { get; set; }
    }
}
