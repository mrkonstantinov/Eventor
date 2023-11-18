using Microsoft.EntityFrameworkCore.Migrations;
using Meetup = Eventor.Services.Meetuping.Domain.AggregatesModel.MeetupAggregate.Meetup;

namespace Eventor.Services.Meetuping.API.Infrastructure;

//Add-Migration InitialCreate -Context MeetupingContext -OutputDir Infrastructure\Migrations
//Update-Database -Context MeetupingContext -Connection "Server=tcp:127.0.0.1,7433;Initial Catalog=Eventor.Services.MeetupingDb;User Id=sa;Password=Pass@word;Encrypt=false"

public class MeetupingContextSeed
{
    public async Task SeedAsync(MeetupingContext context, IWebHostEnvironment env, IOptions<MeetupingSettings> settings, ILogger<MeetupingContextSeed> logger)
    {
        var policy = CreatePolicy(logger, nameof(MeetupingContextSeed));

        await policy.ExecuteAsync(async () =>
        {

            var useCustomizationData = settings.Value
            .UseCustomizationData;

            var contentRootPath = env.ContentRootPath;


            using (context)
            {
                context.Database.Migrate();

                //if (!context.CardTypes.Any())
                //{
                //    context.CardTypes.AddRange(useCustomizationData
                //                            ? GetCardTypesFromFile(contentRootPath, logger)
                //                            : GetPredefinedCardTypes());

                //    await context.SaveChangesAsync();
                //}

                if (!context.MeetupStatus.Any())
                {
                    context.MeetupStatus.AddRange(useCustomizationData
                                            ? GetMeetupStatusFromFile(contentRootPath, logger)
                                            : GetPredefinedMeetupStatus());
                }

                if (!context.Meetups.Any())
                {
                    context.Meetups.AddRange(GetTestEvents());
                }

                await context.SaveChangesAsync();
            }
        });
    }

    //private IEnumerable<CardType> GetCardTypesFromFile(string contentRootPath, ILogger<MeetupingContextSeed> log)
    //{
    //    string csvFileCardTypes = Path.Combine(contentRootPath, "Setup", "CardTypes.csv");

    //    if (!File.Exists(csvFileCardTypes))
    //    {
    //        return GetPredefinedCardTypes();
    //    }

    //    string[] csvheaders;
    //    try
    //    {
    //        string[] requiredHeaders = { "CardType" };
    //        csvheaders = GetHeaders(requiredHeaders, csvFileCardTypes);
    //    }
    //    catch (Exception ex)
    //    {
    //        log.LogError(ex, "Error reading CSV headers");
    //        return GetPredefinedCardTypes();
    //    }

    //    int id = 1;
    //    return File.ReadAllLines(csvFileCardTypes)
    //                                .Skip(1) // skip header column
    //                                .SelectTry(x => CreateCardType(x, ref id))
    //                                .OnCaughtException(ex => { log.LogError(ex, "Error creating card while seeding database"); return null; })
    //                                .Where(x => x != null);
    //}

    //private CardType CreateCardType(string value, ref int id)
    //{
    //    if (string.IsNullOrEmpty(value))
    //    {
    //        throw new Exception("Orderstatus is null or empty");
    //    }

    //    return new CardType(id++, value.Trim('"').Trim());
    //}

    //private IEnumerable<CardType> GetPredefinedCardTypes()
    //{
    //    return Enumeration.GetAll<CardType>();
    //}

    private IEnumerable<MeetupStatus> GetMeetupStatusFromFile(string contentRootPath, ILogger<MeetupingContextSeed> log)
    {
        string csvFileMeetupStatus = Path.Combine(contentRootPath, "Setup", "MeetupStatus.csv");

        if (!File.Exists(csvFileMeetupStatus))
        {
            return GetPredefinedMeetupStatus();
        }

        string[] csvheaders;
        try
        {
            string[] requiredHeaders = { "MeetupStatus" };
            csvheaders = GetHeaders(requiredHeaders, csvFileMeetupStatus);
        }
        catch (Exception ex)
        {
            log.LogError(ex, "Error reading CSV headers");
            return GetPredefinedMeetupStatus();
        }

        int id = 1;
        return File.ReadAllLines(csvFileMeetupStatus)
                                    .Skip(1) // skip header row
                                    .SelectTry(x => CreateMeetupStatus(x, ref id))
                                    .OnCaughtException(ex => { log.LogError(ex, "Error creating order status while seeding database"); return null; })
                                    .Where(x => x != null);
    }

    private MeetupStatus CreateMeetupStatus(string value, ref int id)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new Exception("MeetupStatus is null or empty");
        }

        return new MeetupStatus(id++, value.Trim('"').Trim().ToLowerInvariant());
    }

    private IEnumerable<MeetupStatus> GetPredefinedMeetupStatus()
    {
        return new List<MeetupStatus>()
        {
            MeetupStatus.Submitted,
            MeetupStatus.AwaitingValidation,
            MeetupStatus.StockConfirmed,
            MeetupStatus.Paid,
            MeetupStatus.Shipped,
            MeetupStatus.Cancelled
        };
    }

    private IEnumerable<Meetup> GetTestEvents()
    {
        return new List<Meetup>()
        {
            new Meetup(Guid.NewGuid(), "ownerName", 33, 1, DateTime.Now, new Address(), new List<string>() { "sport", "football"})
        };
    }

    private string[] GetHeaders(string[] requiredHeaders, string csvfile)
    {
        string[] csvheaders = File.ReadLines(csvfile).First().ToLowerInvariant().Split(',');

        if (csvheaders.Count() != requiredHeaders.Count())
        {
            throw new Exception($"requiredHeader count '{requiredHeaders.Count()}' is different then read header '{csvheaders.Count()}'");
        }

        foreach (var requiredHeader in requiredHeaders)
        {
            if (!csvheaders.Contains(requiredHeader))
            {
                throw new Exception($"does not contain required header '{requiredHeader}'");
            }
        }

        return csvheaders;
    }


    private AsyncRetryPolicy CreatePolicy(ILogger<MeetupingContextSeed> logger, string prefix, int retries = 3)
    {
        return Policy.Handle<SqlException>().
            WaitAndRetryAsync(
                retryCount: retries,
                sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                onRetry: (exception, timeSpan, retry, ctx) =>
                {
                    logger.LogWarning(exception, "[{prefix}] Error seeding database (attempt {retry} of {retries})", prefix, retry, retries);
                }
            );
    }
}
