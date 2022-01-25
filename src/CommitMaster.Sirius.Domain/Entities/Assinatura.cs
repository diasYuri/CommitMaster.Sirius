using System;

namespace CommitMaster.Sirius.Domain.Entities
{
    public class Assinatura : Base.Entity
    {
        public DateTime DataExpiracao { get; private set; }
        public EstadoAssinaturaEnum EstadoAssinatura { get; private set; }
        public bool Ativa { get; private set; }

        //Aluno
        public Aluno Aluno { get; private set; }
        public Guid AlunoId { get; private set; }


        //plano        
        public Plano Plano { get; private set; }
        public Guid PlanId { get; private set; }


        protected Assinatura()
        { }

        public Assinatura(Aluno aluno, Plano plano)
        {
            Plano = plano;
            Aluno = aluno;
            AlunoId = aluno.Id;
            PlanId = plano.Id;

            DataExpiracao = DateTime.Today.AddMonths(plano.Duracao);
            EstadoAssinatura = EstadoAssinaturaEnum.AguardadoAtivacao;

            Ativa = false;
        }


        public bool Ativar()
        {
            if (EstadoAssinatura != EstadoAssinaturaEnum.Ativa) {
                EstadoAssinatura = EstadoAssinaturaEnum.Ativa;
                return true;
            }
           
            return false;
            
        }
        
        public bool PagamentoRejeitado()
        {
            if (EstadoAssinatura != EstadoAssinaturaEnum.PagamentoRejeitado) {
                EstadoAssinatura = EstadoAssinaturaEnum.PagamentoRejeitado;
                return true;
            }
           
            return false;
        }

        public bool Expirada()
        {
            return DataExpiracao.Date < DateTime.Today.AddDays(1);
        }

        public bool Invalida()
        {
            return Expirada() || EstadoAssinatura == EstadoAssinaturaEnum.PagamentoRejeitado;
        }

        public string ObterStatusDaAssinatura() {
            return EstadoAssinatura switch {
                EstadoAssinaturaEnum.AguardadoAtivacao => "Assinatura aguardando pagamento",
                EstadoAssinaturaEnum.Ativa => "Assinatura Ativa",
                EstadoAssinaturaEnum.Expirada => "Assinatura Expirada",
                EstadoAssinaturaEnum.PagamentoRejeitado => "O pagamento da assinatura foi rejeitado",
                _ =>  string.Empty
            };
            
        }

}

    public enum EstadoAssinaturaEnum
    {
        AguardadoAtivacao = 0,
        Ativa = 1,
        Expirada = 2,
        PagamentoRejeitado = 3
    }
}
