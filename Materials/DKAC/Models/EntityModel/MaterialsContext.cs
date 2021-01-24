namespace DKAC.Models.EntityModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MaterialsContext : DbContext
    {
        public MaterialsContext()
            : base("name=MaterialsContext")
        {
        }

        public virtual DbSet<Action> Actions { get; set; }
        public virtual DbSet<Modul> Moduls { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<PermissionAction> PermissionActions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<MaterialType> MaterialTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Action>()
                .Property(e => e.ActionName)
                .IsFixedLength();

            modelBuilder.Entity<Action>()
                .Property(e => e.Description)
                .IsFixedLength();

            modelBuilder.Entity<Modul>()
                .Property(e => e.ModulName)
                .IsFixedLength();

            modelBuilder.Entity<Modul>()
                .Property(e => e.Description)
                .IsFixedLength();

            modelBuilder.Entity<Page>()
                .Property(e => e.PageName)
                .IsFixedLength();

            modelBuilder.Entity<Page>()
                .Property(e => e.Description)
                .IsFixedLength();

            modelBuilder.Entity<Role>()
                .Property(e => e.RoleName)
                .IsFixedLength();

            modelBuilder.Entity<Role>()
                .Property(e => e.Description)
                .IsFixedLength();

            modelBuilder.Entity<UserRole>()
                .Property(e => e.Description)
                .IsFixedLength();

            modelBuilder.Entity<MaterialType>()
                .Property(e => e.MaterialTypeName)
                .IsFixedLength();
        }
    }
}
