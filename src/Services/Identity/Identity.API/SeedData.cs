namespace Eventor.Services.Identity.API;

public class SeedData
{
    public static async Task EnsureSeedData(IServiceScope scope, IConfiguration configuration, ILogger logger)
    {
        var retryPolicy = CreateRetryPolicy(configuration, logger);
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await retryPolicy.ExecuteAsync(async () =>
        {
            await context.Database.MigrateAsync();

            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var alice = await userMgr.FindByNameAsync("alice");

            if (alice == null)
            {
                alice = new ApplicationUser
                {
                    UserName = "alice",
                    Email = "AliceSmith@email.com",
                    EmailConfirmed = true,
                    City = "Redmond",
                    Birthdate = new DateTime(2001, 1, 1),
                    Gender =2,
                    Id = Guid.NewGuid().ToString(),
                    Name = "Alice",
                    PhoneNumber = "1234567890"                    
                };

                var result = userMgr.CreateAsync(alice, "Pass123$").Result;

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                logger.LogDebug("alice created");
            }
            else
            {
                logger.LogDebug("alice already exists");
            }

            var bob = await userMgr.FindByNameAsync("bob");

            if (bob == null)
            {
                bob = new ApplicationUser
                {
                    UserName = "bob",
                    Email = "BobSmith@email.com",
                    EmailConfirmed = true,
                    City = "Redmond",
                    Birthdate = new DateTime(2002, 2, 2),
                    Gender = 1,
                    Id = Guid.NewGuid().ToString(),
                    Name = "Bob",
                    PhoneNumber = "1234567890"                    
                };

                var result = await userMgr.CreateAsync(bob, "Pass123$");

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                logger.LogDebug("bob created");
            }
            else
            {
                logger.LogDebug("bob already exists");
            }
        });
    }

    private static AsyncPolicy CreateRetryPolicy(IConfiguration configuration, ILogger logger)
    {
        var retryMigrations = false;
        bool.TryParse(configuration["RetryMigrations"], out retryMigrations);

        // Only use a retry policy if configured to do so.
        // When running in an orchestrator/K8s, it will take care of restarting failed services.
        if (retryMigrations)
        {
            return Policy.Handle<Exception>().
                WaitAndRetryForeverAsync(
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, retry, timeSpan) => logger.LogWarning(exception, "Error migrating database (retry attempt {retry})", retry));
        }

        return Policy.NoOpAsync();
    }
}
