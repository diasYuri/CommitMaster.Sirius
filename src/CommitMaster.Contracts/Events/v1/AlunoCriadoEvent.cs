using System;
using MediatR;

namespace CommitMaster.Contracts.Events.v1
{
    public class AlunoCriadoEvent : INotification
    {
        public string Nome { get; set; }

        public DateTime DataAniversario { get; set; }

        public string Cpf { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public DateTime DataCriacao { get; set; }
    }
}
