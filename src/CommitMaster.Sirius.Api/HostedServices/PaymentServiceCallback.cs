using System;
using System.Threading;
using System.Threading.Tasks;
using CommitMaster.Contracts.Events.v1;
using CommitMaster.Sirius.App.UseCases.v1.AtivarAssinatura;
using CommitMaster.Sirius.Domain.Contracts.v1.Bus;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CommitMaster.Sirius.Api.HostedServices;


public class PaymentServiceCallback : BackgroundService
{
    private readonly IMessageBus _bus;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<PaymentServiceCallback> _logger;

    public PaymentServiceCallback(IMessageBus bus, IServiceProvider serviceProvider, ILogger<PaymentServiceCallback> logger)
    {
        _bus = bus;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _bus.SubscribeAsync<ConfirmacaoPagamentoEvent>("PedidoCancelado",
            async request => await PaymentCallback(request));

        return Task.CompletedTask;
    }

    private async Task PaymentCallback(ConfirmacaoPagamentoEvent message)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            _logger.LogInformation("Nova resposta de solicitacao de pagamento, id: {id}", message.AssinaturaId);
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var command = new AtivarAssinaturaCommand
            {
                AssinaturaId = message.AssinaturaId,
                PagamentoComSucesso = message.PagamentoComSucesso
            };

            var response = await mediator.Send(command);

            _logger.LogInformation("Resultado callback paymentService, sucesso: {status}", response.Success);
        }
    }
}

