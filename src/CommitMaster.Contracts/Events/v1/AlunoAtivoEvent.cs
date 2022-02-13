using MediatR;

namespace CommitMaster.Contracts.Events.v1
{
    public class AlunoAtivoEvent : INotification
    {
        public string Email { get; set; }
        public bool Ativo { get; set; }
    }
}
