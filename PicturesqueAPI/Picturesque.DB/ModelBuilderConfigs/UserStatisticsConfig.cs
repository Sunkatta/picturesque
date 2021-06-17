using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Picturesque.Domain;
using System;

namespace Picturesque.DB.ModelBuilderConfigs
{
    internal sealed class UserStatisticsConfig : IEntityTypeConfiguration<UserStatistics>
    {
        public void Configure(EntityTypeBuilder<UserStatistics> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasOne(x => x.User)
                .WithMany(u => u.UserStatistics)
                .HasForeignKey(us => us.UserId);
        }
    }
}
