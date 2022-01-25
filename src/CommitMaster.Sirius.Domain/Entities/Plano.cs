namespace CommitMaster.Sirius.Domain.Entities
{
    public class Plano : Base.Entity
    {
        public string Nome { get; set; }
        /// <summary>
        /// Duração em meses
        /// </summary>
        public int Duracao { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
    }
}
