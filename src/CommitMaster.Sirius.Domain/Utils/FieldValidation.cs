using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CommitMaster.Sirius.Domain.Utils
{
    public class FieldValidation
    {

    }

    public class IsNumber : ValidationAttribute
    {
        public IsNumber()
        {

        }

        /// <summary>
        /// Validação 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return false;

            return value.ToString()!.All(i => char.IsDigit(i));
        }
    }
}
