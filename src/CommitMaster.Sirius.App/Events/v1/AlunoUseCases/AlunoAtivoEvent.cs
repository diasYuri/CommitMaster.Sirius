using MediatR;

namespace CommitMaster.Sirius.App.Events.v1.AlunoUseCases
{
    public class AlunoAtivoEvent : INotification
    {
        public string Email { get; set; }
        public bool Ativo { get; set; }
    }
}
