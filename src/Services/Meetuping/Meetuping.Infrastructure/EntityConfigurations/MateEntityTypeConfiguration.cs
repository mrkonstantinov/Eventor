using Meetuping.Domain.AggregatesModel.MeetupAggregate;

namespace Eventor.Services.Meetuping.Infrastructure.EntityConfigurations;

class MateEntityTypeConfiguration
    : IEntityTypeConfiguration<Mate>
{
    public void Configure(EntityTypeBuilder<Mate> mateConfiguration)
    {
        mateConfiguration.ToTable("mates", MeetupingContext.DEFAULT_SCHEMA);

        mateConfiguration.HasKey(b => b.Id);

        mateConfiguration.Ignore(b => b.DomainEvents);

        mateConfiguration.Property(b => b.Id)
            .UseHiLo("mateseq", MeetupingContext.DEFAULT_SCHEMA);

        mateConfiguration.Property<int>("MeetupId")
            .IsRequired();

        mateConfiguration.Property<Guid>("UserId")
            .IsRequired();

        mateConfiguration
            .Property<string>("_mateName")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("MateName")
            .IsRequired();

        mateConfiguration
            .Property<int?>("_age")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Age")
            .IsRequired(false);

        mateConfiguration
            .Property<int?>("_gender")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Gender")
            .IsRequired(true);

        mateConfiguration
            .Property<string>("_pictureUrl")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("PictureUrl")
            .IsRequired(false);

        mateConfiguration
            .Property<DateTime?>("_approvalAt")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("ApprovalAt")
            .IsRequired(false);
    }
}
