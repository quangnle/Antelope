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
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TargetConfig> TargetConfigs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<GeneralConfig> GeneralConfigs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(e => e.TargetConfigs)
                .WithRequired(e => e.Account)
                .HasForeignKey(e => e.IdAccount)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.TargetConfigs1)
                .WithRequired(e => e.Account1)
                .HasForeignKey(e => e.IdTarget)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bank>()
                .HasMany(e => e.Accounts)
                .WithRequired(e => e.Bank)
                .HasForeignKey(e => e.IdBank)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ExceedIncident>()
                .HasMany(e => e.Actions)
                .WithRequired(e => e.ExceedIncident)
                .HasForeignKey(e => e.IdExceedIncident);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Accessibilities)
                .WithRequired(e => e.Role)
                .HasForeignKey(e => e.IdRole)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role)
                .HasForeignKey(e => e.IdRole)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);
        }
    }
}
