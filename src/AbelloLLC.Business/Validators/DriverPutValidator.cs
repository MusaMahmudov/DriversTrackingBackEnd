using AbelloLLC.Business.DTOs.DriverDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.Validators
{
    public class DriverPutValidator : AbstractValidator<PutDriverDTO>
    {
        public DriverPutValidator() 
        {
            RuleFor(d => d.Name).NotNull().NotEmpty().MinimumLength(3).MaximumLength(128);
            RuleFor(d => d.Owner).NotNull().NotEmpty().MinimumLength(3).MaximumLength(128);
            RuleFor(d => d.Phone).NotNull().NotEmpty().MinimumLength(3).MaximumLength(128);
            RuleFor(d => d.Dimensions).NotNull().NotEmpty().MinimumLength(3).MaximumLength(128);
            RuleFor(d => d.Name).NotNull().NotEmpty().MinimumLength(3).MaximumLength(128);
            RuleFor(d => d.Name).NotNull().NotEmpty().MinimumLength(3).MaximumLength(128);
            RuleFor(d => d.Capacity).NotNull().NotEmpty();
            RuleFor(d => d.VehicleTypeId).NotNull().NotEmpty();

        }
    }
}
