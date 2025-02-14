using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PXDemo.Infrastructure.Models;

namespace PXDemo.Infrastructure.Configuration
{
    public class DeviceStatusConfiguration : IEntityTypeConfiguration<DeviceStatus>
    {
        public void Configure(EntityTypeBuilder<DeviceStatus> builder)
        {
            builder.ToTable("DeviceStatuses", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
