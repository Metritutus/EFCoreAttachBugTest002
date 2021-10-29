using EFCoreAttachBugTest002.DataSeeding;
using EFCoreAttachBugTest002.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreAttachBugTest002.ModelConfigurations
{
    public class MysteryTypeConfiguration : IEntityTypeConfiguration<MysteryType>
    {
        public void Configure(EntityTypeBuilder<MysteryType> builder)
        {
            builder.ToTable("MysteryType");
            builder.HasData(SeedData.MysteryTypes);
        }
    }
}
