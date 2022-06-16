using System;
using CommitMaster.Sirius.Domain.Entities.Base;
using CommitMaster.Sirius.Domain.UseCases.Autorization;

namespace CommitMaster.Sirius.Domain.Entities
{
    public class Usuario : Entity
    {
        public string Email { get; private set; }
        public string Senha { get; private set; }

        public DateTime DataExpiracao { get; private set; }

        public Aluno Aluno { get; private set; }
        public Guid AlunoId { get; private set; }

        public Usuario(string email, string senha, Guid alunoId)
        {
            Email = email;
            Senha = senha;
            AlunoId = alunoId;
        }



        public RolesEnum ValidaAssinatura()
        {
            if (Aluno.Assinatura == null)
            {
                return RolesEnum.SemAssinatura;
            }

            if (Aluno.Assinatura.DataExpiracao < DateTime.UtcNow)
            {
                return RolesEnum.AssinaturaExpirada;
            }

            return RolesEnum.AssinaturaValida;
        }
    }
}
