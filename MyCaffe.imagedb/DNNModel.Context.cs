﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyCaffe.imagedb
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DNNEntities : DbContext
    {
        public DNNEntities()
            : base("name=DNNEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DatasetGroup> DatasetGroups { get; set; }
        public virtual DbSet<DatasetParameter> DatasetParameters { get; set; }
        public virtual DbSet<LabelBoost> LabelBoosts { get; set; }
        public virtual DbSet<Label> Labels { get; set; }
        public virtual DbSet<RawImageGroup> RawImageGroups { get; set; }
        public virtual DbSet<RawImageMean> RawImageMeans { get; set; }
        public virtual DbSet<RawImageParameter> RawImageParameters { get; set; }
        public virtual DbSet<RawImageResult> RawImageResults { get; set; }
        public virtual DbSet<SourceParameter> SourceParameters { get; set; }
        public virtual DbSet<Source> Sources { get; set; }
        public virtual DbSet<DatasetCreator> DatasetCreators { get; set; }
        public virtual DbSet<ModelGroup> ModelGroups { get; set; }
        public virtual DbSet<Dataset> Datasets { get; set; }
        public virtual DbSet<RawImage> RawImages { get; set; }
    }
}
