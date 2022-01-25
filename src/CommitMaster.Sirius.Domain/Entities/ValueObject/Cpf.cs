using System.ComponentModel.DataAnnotations;

namespace CommitMaster.Sirius.Domain.Entities.ValueObject
{
    public class Cpf
    {
        public string Numero { get; set; }

        public Cpf(string numero)
        {
            Numero = numero;
        }

        public Cpf()
        {
            
        }
    }
}
