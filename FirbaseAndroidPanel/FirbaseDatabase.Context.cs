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
    
    public partial class FirebaseEntities : DbContext
    {
        public FirebaseEntities()
            : base("name=FirebaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<FailedLog> FailedLogs { get; set; }
        public virtual DbSet<Firebase_TokenInfo_ForAllApps> Firebase_TokenInfo_ForAllApps { get; set; }
        public virtual DbSet<ImageUpload> ImageUploads { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<SendLogTable> SendLogTables { get; set; }
        public virtual DbSet<tbl_AccessLogGlobal> tbl_AccessLogGlobal { get; set; }
        public virtual DbSet<tbl_FirebaseSendDesktopNotification> tbl_FirebaseSendDesktopNotification { get; set; }
        public virtual DbSet<tbl_PushMessage> tbl_PushMessage { get; set; }
        public virtual DbSet<TokenInfois> TokenInfois { get; set; }
        public virtual DbSet<UrlClickLog> UrlClickLogs { get; set; }
        public virtual DbSet<UrlManage> UrlManages { get; set; }
        public virtual DbSet<tbl_PushMessage_ForAllApps> tbl_PushMessage_ForAllApps { get; set; }
        public virtual DbSet<UrlClickLogs_ForAllApps> UrlClickLogs_ForAllApps { get; set; }
        public virtual DbSet<FailedLogs_ForAllApps> FailedLogs_ForAllApps { get; set; }
        public virtual DbSet<SendLogTables_ForAllApps> SendLogTables_ForAllApps { get; set; }
        public virtual DbSet<tbl_AdAgencyLog> tbl_AdAgencyLog { get; set; }
        public virtual DbSet<tbl_FirebaseSendDesktopNotification_ForAllApps> tbl_FirebaseSendDesktopNotification_ForAllApps { get; set; }
    
        public virtual ObjectResult<string> sp_InsertToken_ForAllApps(string msisdn, string serviceName, string token, string deviceManufacture, string deviceModel)
        {
            var msisdnParameter = msisdn != null ?
                new ObjectParameter("msisdn", msisdn) :
                new ObjectParameter("msisdn", typeof(string));
    
            var serviceNameParameter = serviceName != null ?
                new ObjectParameter("serviceName", serviceName) :
                new ObjectParameter("serviceName", typeof(string));
    
            var tokenParameter = token != null ?
                new ObjectParameter("token", token) :
                new ObjectParameter("token", typeof(string));
    
            var deviceManufactureParameter = deviceManufacture != null ?
                new ObjectParameter("DeviceManufacture", deviceManufacture) :
                new ObjectParameter("DeviceManufacture", typeof(string));
    
            var deviceModelParameter = deviceModel != null ?
                new ObjectParameter("DeviceModel", deviceModel) :
                new ObjectParameter("DeviceModel", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("sp_InsertToken_ForAllApps", msisdnParameter, serviceNameParameter, tokenParameter, deviceManufactureParameter, deviceModelParameter);
        }
    }
}
