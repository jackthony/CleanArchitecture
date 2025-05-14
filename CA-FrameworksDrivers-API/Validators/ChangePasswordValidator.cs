using CA_InterfaceAdapters_Mappers.Dtos.Requests;
using CA_InterfaceAdapters_Mappers.Dtos.USUARIOS;
using FluentValidation;

namespace CA_FrameworksDrivers_API.Validators
{
    public class ChangePasswordValidator : AbstractValidator<UsuarioUpdatePasswordDTO>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.nUsuarioModificacion)
            .GreaterThan(0)
            .WithMessage("El valor debe ser mayor que cero");

            RuleFor(x => x.nuevaClave)
                .NotEmpty().WithMessage("Ingrese la nueva contraseña")
                .MinimumLength(6).WithMessage("La nueva contraseña debe tener al menos 6 caracteres")
                .MaximumLength(32).WithMessage("La nueva contraseña no debe pasar los 32 caracteres")
                .Equal(x => x.repetirClave).WithMessage("Las contraseñas no coinciden");

            RuleFor(x => x.repetirClave)
                .NotEmpty().WithMessage("Repita la nueva contraseña");
        }

    }
}
