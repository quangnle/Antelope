namespace Antelope.Data.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MainModel : DbContext
    {
        public MainModel()
            : base("name=MainModel")
        {
        }

        public virtual DbSet<Accessibility> Accessibilities { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountConfig> AccountConfigs { get; set; }
        public virtual DbSet<Action> Actions { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<ExceedIncident> ExceedIncidents { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<TargetConfig> TargetConfigs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<GeneralConfig> GeneralConfigs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);
        }
    }
}
