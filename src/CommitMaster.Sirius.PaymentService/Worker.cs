using CommitMaster.Contracts.Events.v1;
using CommitMaster.Sirius.Domain.Contracts.v1.Bus;


namespace CommitMaster.Sirius.PaymentService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IMessageBus _messageBus;

    public Worker(ILogger<Worker> logger, IMessageBus messageBus)
    {
        _logger = logger;
        _messageBus = messageBus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        
        
        
        _messageBus.SubscribeAsync<SolicitaPagamentoEvent>("ProcessadorPagamento", 
            async request => await ProcessarPagamento(request));
    }

    private async Task ProcessarPagamento(SolicitaPagamentoEvent message)
    {
        _logger.LogInformation("Nova solicitacao de pagamento, hora: {time}", DateTimeOffset.Now);
        var confirmacaoPagamentoEvent = new ConfirmacaoPagamentoEvent {
            AssinaturaId = message.SubscriptionId,
            PagamentoComSucesso = true,
            DataPagamento = DateTime.UtcNow
        };
        
        await _messageBus.PublishAsync(confirmacaoPagamentoEvent);
        
    }
}
