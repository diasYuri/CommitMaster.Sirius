using CommitMaster.Sirius.PaymentService;
using CommitMaster.Sirius.PaymentService.Infra;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddDependencies(hostContext.Configuration);

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
