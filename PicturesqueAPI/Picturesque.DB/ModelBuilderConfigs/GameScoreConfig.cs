using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Picturesque.Domain;

namespace Picturesque.DB.ModelBuilderConfigs
{
    internal sealed class GameScoreConfig : IEntityTypeConfiguration<GameScore>
    {
        public void Configure(EntityTypeBuilder<GameScore> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasOne(x => x.User)
                .WithMany(c => c.GameScores)
                .HasForeignKey(x => x.UserId);
        }
    }
}
