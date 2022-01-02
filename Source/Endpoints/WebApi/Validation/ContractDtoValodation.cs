using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using WebApi.DTOs;

namespace WebApi.Validation
{
    public class ContractDtoValodation : AbstractValidator<ContractDto>
    {
        public ContractDtoValodation()
        {
            RuleFor(x => x.CustomerName).NotNull().NotEmpty().WithMessage("{PropertyName} should be not empty. NEVER!")
                .MaximumLength(50).MinimumLength(3);

            RuleFor(x => x.CustomerAddress).NotNull().MaximumLength(50).MinimumLength(3);

            RuleFor(x => x.EmailAddress).EmailAddress().Null();

        }

    }
}
