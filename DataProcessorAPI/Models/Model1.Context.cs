﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataProcessorAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<EOPF_AGENCY_DETAILS> EOPF_AGENCY_DETAILS { get; set; }
        public virtual DbSet<EOPF_AGENCY_INFO> EOPF_AGENCY_INFO { get; set; }
        public virtual DbSet<EOPF_DP_MAPPING> EOPF_DP_MAPPING { get; set; }
        public virtual DbSet<EOPF_DP_QUERY> EOPF_DP_QUERY { get; set; }
        public virtual DbSet<EOPF_DP_SUB_MAPPING> EOPF_DP_SUB_MAPPING { get; set; }
        public virtual DbSet<EOPF_DP_SETTING> EOPF_DP_SETTING { get; set; }
    }
}