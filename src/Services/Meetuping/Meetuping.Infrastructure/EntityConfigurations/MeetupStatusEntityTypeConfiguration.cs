namespace Eventor.Services.Meetuping.Infrastructure.EntityConfigurations;

class MeetupStatusEntityTypeConfiguration
    : IEntityTypeConfiguration<MeetupStatus>
{
    public void Configure(EntityTypeBuilder<MeetupStatus> meetupStatusConfiguration)
    {
        meetupStatusConfiguration.ToTable("meetupstatus", MeetupingContext.DEFAULT_SCHEMA);

        meetupStatusConfiguration.HasKey(o => o.Id);

        meetupStatusConfiguration.Property(o => o.Id)
            .HasDefaultValue(1)
            .ValueGeneratedNever()
            .IsRequired();

        meetupStatusConfiguration.Property(o => o.Name)
            .HasMaxLength(200)
            .IsRequired();
    }
}
