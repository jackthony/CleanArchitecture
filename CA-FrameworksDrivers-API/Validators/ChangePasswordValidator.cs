using CA_InterfaceAdapters_Mappers.Dtos.Requests;
using CA_InterfaceAdapters_Mappers.Dtos.USUARIOS;
using FluentValidation;

namespace CA_FrameworksDrivers_API.Validators
{
    public class ChangePasswordValidator : AbstractValidator<UsuarioUpdatePasswordDTO>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.antiguaClave).NotEmpty().WithMessage("Ingrese la constraseña actual");
            RuleFor(x => x.nuevaClave)
            .Equal(x => x.repetirClave)
            .WithMessage("Las contraseñas no coinciden");
        }
    }
}
