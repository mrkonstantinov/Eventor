namespace Eventor.Services.Meetuping.API.Infrastructure.Factories
{
    public class MeetupingDbContextFactory : IDesignTimeDbContextFactory<MeetupingContext>
    {
        public MeetupingContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
               .AddJsonFile("appsettings.json")
               .AddEnvironmentVariables()
               .Build();

            var optionsBuilder = new DbContextOptionsBuilder<MeetupingContext>();

            optionsBuilder.UseSqlServer(config["ConnectionString"], sqlServerOptionsAction: o => o.MigrationsAssembly("Meetuping.API"));

            return new MeetupingContext(optionsBuilder.Options);
        }
    }
}