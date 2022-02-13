using System;
using CommitMaster.Sirius.Domain.Entities.ValueObject;

namespace CommitMaster.Sirius.Domain.Entities
{
    public class Aluno : Base.Entity
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public DateTime DataAniversario { get; private set; }
        public Cpf Cpf { get; private set; }
        public Telefone Telefone { get; private set; }
       
        public Guid? AssinaturaId { get; private set; }
        public Assinatura Assinatura { get; private set; }

        
        //EF 
        protected Aluno() { }
        
        public Aluno(string nome, string email, DateTime dataAniversario, string cpf, string telefone)
        {
            Nome = nome;
            Email = email;
            DataAniversario = dataAniversario;
            Cpf = new Cpf(cpf);
            Telefone = new Telefone(telefone);
        }

        public bool AdicionaAssinatura(Assinatura assinatura)
        {
            if (AssinaturaId == null || AssinaturaId == Guid.Empty || (Assinatura != null && Assinatura.Invalida())) {
                Assinatura = assinatura;
                AssinaturaId = assinatura.Id;
                return true;
            }

            return false;
        }

        public (bool, string) VerificarAssinatura()
        {
            if (Assinatura == null) {
                return (false, string.Empty);
            }

            return (true, Assinatura.ObterStatusDaAssinatura());
        }


    }

}
