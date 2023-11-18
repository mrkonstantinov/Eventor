using Eventor.Services.Meetuping.Domain.AggregatesModel.MeetupAggregate;
using Meetuping.Domain.AggregatesModel.MeetupAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Eventor.Services.Meetuping.Infrastructure.EntityConfigurations;

class MeetupEntityTypeConfiguration : IEntityTypeConfiguration<Meetup>
{
    public void Configure(EntityTypeBuilder<Meetup> meetupConfiguration)
    {
        meetupConfiguration.ToTable("meetups", MeetupingContext.DEFAULT_SCHEMA);

        meetupConfiguration.HasKey(o => o.Id);

        meetupConfiguration.Ignore(b => b.DomainEvents);

        meetupConfiguration.Property(o => o.Id)
            .UseHiLo("meetupseq", MeetupingContext.DEFAULT_SCHEMA);

        //Address value object persisted as owned entity type supported since EF Core 2.0
        meetupConfiguration
            .OwnsOne(o => o.Address, a =>
            {
                // Explicit configuration of the shadow key property in the owned type 
                // as a workaround for a documented issue in EF Core 5: https://github.com/dotnet/efcore/issues/20740
                a.Property<int>("MeetupId")
                .UseHiLo("meetupseq", MeetupingContext.DEFAULT_SCHEMA);
                a.WithOwner();
            });

        //Address value object persisted as owned entity type supported since EF Core 2.0
        meetupConfiguration
            .OwnsOne(o => o.Metadata, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.ToJson();
                ownedNavigationBuilder.OwnsMany(metadata => metadata.Tags);
            });

        meetupConfiguration
            .Property<Guid>("_ownerId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("OwnerId")
            .IsRequired();

        meetupConfiguration
            .Property<string>("_ownerName")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("OwnerName")
            .IsRequired();

        meetupConfiguration
            .Property<int?>("_age")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Age")
            .IsRequired(false);

        meetupConfiguration
            .Property<int?>("_gender")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Gender")
            .IsRequired(false);

        meetupConfiguration
            .Property<DateTime>("_meetupDate")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("MeetupDate")
            .IsRequired();

        meetupConfiguration
            .Property<int>("_meetupStatusId")
        // .HasField("_meetupStatusId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
        .HasColumnName("MeetupStatusId")
        .IsRequired();

        meetupConfiguration.Property<string>("Description").IsRequired(false);

        var navigation = meetupConfiguration.Metadata.FindNavigation(nameof(Meetup.Mates));
 
        // DDD Patterns comment:
        //Set as field (New since EF 1.1) to access the OrderItem collection property through its field
        navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

        meetupConfiguration.HasOne(o => o.MeetupStatus)
            .WithMany()
            // .HasForeignKey("MeetupStatusId");
            .HasForeignKey("_meetupStatusId");
    }
}
