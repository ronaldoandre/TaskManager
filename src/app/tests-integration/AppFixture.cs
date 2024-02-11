namespace TaskManager.Tests.Integration;

public class AppFixture : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder
            .ConfigureAppConfiguration(config => config.AddUserSecrets<AppFixture>(optional: true))
            .ConfigureTestServices(services =>
            {
                services.Replace(ServiceDescriptor.Scoped(s => new TaskManagerContext(
                    new DbContextOptionsBuilder<TaskManagerContext>()
                    .UseInMemoryDatabase(databaseName: "TaskManagerDatabase")
                    .Options)));
            });
    }
}
