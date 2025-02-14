using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PXDemo.Infrastructure.Models;

namespace PXDemo.Infrastructure.Configuration
{
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.ToTable("Devices", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasOne(x => x.DeviceType)
                .WithMany()
                .HasForeignKey(x => x.DeviceTypeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
