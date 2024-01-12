using AbelloLLC.Business.DTOs.VehicleTypeDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.Validators.VehicleTypeValidators
{
    public class PutVehicleTypeValidator : AbstractValidator<PutVehicleTypeDTO>
    {
        public PutVehicleTypeValidator() 
        {
         RuleFor(vt=>vt.Name).NotNull().NotEmpty().MinimumLength(3).MaximumLength(128);
        }
    }
}
