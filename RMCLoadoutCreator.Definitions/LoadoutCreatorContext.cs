using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RMCLoadoutCreator.Definitions.Models;

namespace RMCLoadoutCreator.Definitions
{
    public class LoadoutCreatorContext : IdentityDbContext<IdentityUser>
    {
        public LoadoutCreatorContext(DbContextOptions<LoadoutCreatorContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Loadouts
            modelBuilder.Entity<Loadout>().HasOne(it => it.Role).WithMany().HasForeignKey(it => it.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Loadout>().HasOne(it => it.Version).WithMany().HasForeignKey(it => it.VersionId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Loadout>().HasMany(it => it.Slots).WithOne(it => it.Loadout).HasForeignKey(it => it.LoadoutId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            // Slots
            modelBuilder.Entity<Slot>().HasOne(it => it.Item).WithOne(it => it.Slot).HasForeignKey<Slot>(it => it.ItemId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);

            // Items
            modelBuilder.Entity<Item>().HasOne(it => it.Slot).WithOne(it => it.Item).HasForeignKey<Item>(it => it.SlotId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Item>().HasOne(it => it.Parent).WithMany().HasForeignKey(it => it.ParentId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);

            //Vendor
            modelBuilder.Entity<Vendor>().HasOne(it => it.Version).WithMany().HasForeignKey(it => it.VersionId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Role> RMCRoles { get; set; }

        public DbSet<Loadout> Loadouts { get; set; }

        public DbSet<Models.Version> Versions { get; set; }

        public DbSet<Slot> Slots { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Vendor> Vendors { get; set; }
    }

}