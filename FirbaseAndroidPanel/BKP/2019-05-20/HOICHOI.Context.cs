﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FirbaseAndroidPanel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class HoiChoiEntities : DbContext
    {
        public HoiChoiEntities()
            : base("name=HoiChoiEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tbl_html5Games> tbl_html5Games { get; set; }
        public virtual DbSet<tbl_html5Games_vendor> tbl_html5Games_vendor { get; set; }
        public virtual DbSet<tbl_html5GamesMap> tbl_html5GamesMap { get; set; }
    
        public virtual ObjectResult<sp_GetGameData_Result> sp_GetGameData()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetGameData_Result>("sp_GetGameData");
        }
    }
}
