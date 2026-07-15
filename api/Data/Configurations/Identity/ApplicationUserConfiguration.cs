using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations.Identity
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            // Postgres naming change
            builder.ToTable("asp_net_users");

            builder.Property(u => u.DisplayName)
                .HasMaxLength(50);

            // Users have many claims
            builder.HasMany(e => e.Claims)
                .WithOne(uc => uc.User)
                .HasForeignKey(uc => uc.UserId)
                .IsRequired();

            // Each User can have many UserLogins
            builder.HasMany(e => e.UserLogins)
                .WithOne(ul => ul.User)
                .HasForeignKey(ul => ul.UserId)
                .IsRequired();

            // Each User can have many UserTokens
            builder.HasMany(e => e.UserTokens)
                .WithOne(ut => ut.User)
                .HasForeignKey(ut => ut.UserId)
                .IsRequired();

            // Each user can have many entries in the UserRole join table
            builder.HasMany(e => e.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        }
    }
}
