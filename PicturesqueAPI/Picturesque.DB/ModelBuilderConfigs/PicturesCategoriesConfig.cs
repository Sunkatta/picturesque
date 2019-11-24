using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Picturesque.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Picturesque.DB.ModelBuilderConfigs
{
    internal sealed class PicturesCategoriesConfig : IEntityTypeConfiguration<PicturesCategories>
    {
        public void Configure(EntityTypeBuilder<PicturesCategories> builder)
        {
            builder
                .HasKey(pc => new { pc.CategoryId, pc.PictureId });

            builder
                .HasOne(pc => pc.Category)
                .WithMany(c => c.Pictures)
                .HasForeignKey(uc => uc.CategoryId);

            builder
                .HasOne(pc => pc.Picture)
                .WithMany(p => p.Categories)
                .HasForeignKey(pc => pc.PictureId);
        }
    }
}
