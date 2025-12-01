using HotelManagementSystem.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        // Define DbSets for your entities here
        // public DbSet<YourEntity> YourEntities { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<AccountPermission> AccountPermissions { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<ReservationDetail> ReservationDetails { get; set; }
        public DbSet<RoomAmenity> RoomAmenities { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceUsage> ServiceUsages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConfigureAccountPermission(modelBuilder);
            ConfigureRoomAmenity(modelBuilder);
            ConfigureCustomer(modelBuilder);
            ConfigureEmployee(modelBuilder);
            ConfigureReservation(modelBuilder);
            ConfigureReservationDetail(modelBuilder);
            ConfigureInvoice(modelBuilder);
            ConfigureServiceUsage(modelBuilder);
            ConfigureRooom(modelBuilder);

        }
        private void ConfigureAccountPermission(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountPermission>()
                .HasKey(ap => new { ap.AccountId, ap.PermissionId });
            modelBuilder.Entity<AccountPermission>()
                .HasOne(ap => ap.Account)
                .WithMany(a => a.AccountPermissions)
                .HasForeignKey(ap => ap.AccountId);
            modelBuilder.Entity<AccountPermission>()
                .HasOne(ap => ap.Permission)
                .WithMany(p => p.AccountPermissions)
                .HasForeignKey(ap => ap.PermissionId);
        }
        private void ConfigureRoomAmenity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoomAmenity>()
                .HasKey(ra => new { ra.RoomId, ra.AmenityId });
            modelBuilder.Entity<RoomAmenity>()
                .HasOne(ra => ra.Room)
                .WithMany(r => r.RoomAmenities)
                .HasForeignKey(ra => ra.RoomId);
            modelBuilder.Entity<RoomAmenity>()
                .HasOne(ra => ra.Amenity)
                .WithMany(a => a.RoomAmenities)
                .HasForeignKey(ra => ra.AmenityId);
        }
        private void ConfigureCustomer(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Email)
                .IsUnique();
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.PhoneNumber)
                .IsUnique();
            modelBuilder.Entity<Customer>()
                .HasOne(cu => cu.Account)
                .WithOne()
                .HasForeignKey<Customer>(cu => cu.AccountId)
                .IsRequired(false);

        }
        private void ConfigureEmployee(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Email)
                .IsUnique();
            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.PhoneNumber)
                .IsUnique();
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Account)
                .WithOne()
                .HasForeignKey<Employee>(e => e.AccountId)
                .IsRequired();
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Branch)
                .WithMany(b => b.Employees)
                .HasForeignKey(e => e.BranchId)
                .IsRequired();

        }
        private void ConfigureReservation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.CustomerId)
                .IsRequired();
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.CheckinEmployee)
                .WithMany()
                .HasForeignKey(r => r.CheckinEmployeeId)
                .IsRequired(false);
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.CheckoutEmployee)
                .WithMany()
                .HasForeignKey(r => r.CheckoutEmployeeId)
                .IsRequired(false);


        }
        private void ConfigureReservationDetail(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReservationDetail>()
                .HasOne(rd => rd.Reservation)
                .WithMany(r => r.ReservationDetails)
                .HasForeignKey(rd => rd.ReservationId);
            modelBuilder.Entity<ReservationDetail>()
                .HasOne(rd => rd.Room)
                .WithMany(r => r.ReservationDetails)
                .HasForeignKey(rd => rd.RoomId);
            modelBuilder.Entity<ReservationDetail>()
                .HasMany(rd => rd.GuestProfiles)
                .WithOne(gp => gp.ReservationDetail)
                .HasForeignKey(gp => gp.ReservationDetailId);

        }
        private void ConfigureInvoice(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Reservation)
                .WithOne(r => r.Invoice)
                .HasForeignKey<Invoice>(i => i.ReservationId)
                .IsRequired();
            modelBuilder.Entity<Invoice>()
                .HasMany(i => i.Payments)
                .WithOne(p => p.Invoice)
                .HasForeignKey(p => p.InvoiceId);
        }
        private void ConfigureServiceUsage(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServiceUsage>()
                .HasOne(su => su.Service)
                .WithMany(s => s.ServiceUsages)
                .HasForeignKey(su => su.ServiceId);
            modelBuilder.Entity<ServiceUsage>()
                .HasOne(su => su.ReservationDetail)
                .WithMany(rd => rd.ServiceUsages)
                .HasForeignKey(su => su.ReservationDetailId);
        }
        private void ConfigureRooom(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>()
                .HasOne(r => r.RoomType)
                .WithMany(rt => rt.Rooms)
                .HasForeignKey(r => r.RoomTypeId);
            modelBuilder.Entity<Room>()
                .HasOne(r => r.Branch)
                .WithMany(b => b.Rooms)
                .HasForeignKey(r => r.BranchId);
        }
        
    }
}