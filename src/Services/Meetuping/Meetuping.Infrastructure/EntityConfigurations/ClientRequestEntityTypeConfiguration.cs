namespace Eventor.Services.Meetuping.Infrastructure.EntityConfigurations;

class ClientRequestEntityTypeConfiguration
    : IEntityTypeConfiguration<ClientRequest>
{
    public void Configure(EntityTypeBuilder<ClientRequest> requestConfiguration)
    {
        requestConfiguration.ToTable("requests", MeetupingContext.DEFAULT_SCHEMA);
        requestConfiguration.HasKey(cr => cr.Id);
        requestConfiguration.Property(cr => cr.Name).IsRequired();
        requestConfiguration.Property(cr => cr.Time).IsRequired();
    }
}
