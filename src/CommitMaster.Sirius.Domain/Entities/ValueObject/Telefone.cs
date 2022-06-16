namespace CommitMaster.Sirius.Domain.Entities.ValueObject
{
    public class Telefone
    {
        public string Numero { get; set; }

        public Telefone()
        {

        }

        public Telefone(string numero)
        {
            Numero = numero;
        }
    }
}
