using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommitMaster.Sirius.Domain.Contracts
{
    public interface ICommandValidable
    {

        bool IsValid(out List<ValidationResult> errors);
    }
}
