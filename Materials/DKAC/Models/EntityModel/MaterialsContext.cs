namespace DKAC.Models.EntityModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using MySql.Data.Entity;

    //[DbConfigurationType(typeof(MySqlEFConfiguration))]
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
        public virtual DbSet<ChiTietCheBan> ChiTietCheBan { get; set; }
        public virtual DbSet<ChiTietDuToan> ChiTietDuToan { get; set; }
        public virtual DbSet<ChiTietIn> ChiTietIn { get; set; }
        public virtual DbSet<DonHang> DonHang { get; set; }
        public virtual DbSet<KhachHang> KhachHang { get; set; }
        public virtual DbSet<VatTu> VatTu { get; set; }
        public virtual DbSet<SanPham> SanPham { get; set; }
        public virtual DbSet<PPIn> PPIn { get; set; }

        //public MaterialsContext(string connectionString)
        //    : base(connectionString) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Action>()
                .Property(e => e.Description)
                .IsFixedLength();

            modelBuilder.Entity<Modul>()
                .Property(e => e.Description)
                .IsFixedLength();

            modelBuilder.Entity<Page>()
                .Property(e => e.Description)
                .IsFixedLength();

            modelBuilder.Entity<Role>()
                .Property(e => e.Description)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.FullName)
                .IsFixedLength();

            modelBuilder.Entity<UserRole>()
                .Property(e => e.Description)
                .IsFixedLength();

            modelBuilder.Entity<UserGroup>()
                .Property(e => e.UserGroupName)
                .IsFixedLength();

            modelBuilder.Entity<MaterialType>()
                .Property(e => e.MaterialTypeName)
                .IsFixedLength();

            modelBuilder.Entity<ChiTietCheBan>()
                .Property(e => e.ten_tay_in)
                .IsFixedLength();

            modelBuilder.Entity<ChiTietDuToan>()
                .Property(e => e.muc_dich_sd)
                .IsFixedLength();

            modelBuilder.Entity<ChiTietIn>()
                .Property(e => e.ten_loai)
                .IsFixedLength();

            modelBuilder.Entity<DonHang>()
                .Property(e => e.ten_san_pham)
                .IsFixedLength();

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.ten_cong_ty)
                .IsFixedLength();

            modelBuilder.Entity<VatTu>()
                .Property(e => e.loai_giay)
                .IsFixedLength();

            modelBuilder.Entity<SanPham>()
                .Property(e => e.ten_san_pham)
                .IsFixedLength();

            modelBuilder.Entity<PPIn>()
                .Property(e => e.Name)
                .IsFixedLength();

        }
    }
}
