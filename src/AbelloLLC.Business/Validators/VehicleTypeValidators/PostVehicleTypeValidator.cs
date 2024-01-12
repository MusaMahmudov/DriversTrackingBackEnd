using AbelloLLC.Business.DTOs.VehicleTypeDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.Validators.VehicleTypeValidators
{
    public class PostVehicleTypeValidator : AbstractValidator<PostVehicleTypeDTO>
    {
        public PostVehicleTypeValidator() 
        {
        RuleFor(vt=>vt.Name).NotEmpty().NotNull().MinimumLength(3).MaximumLength(128);

        }
    }
}
