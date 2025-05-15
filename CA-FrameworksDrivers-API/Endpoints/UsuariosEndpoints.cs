using CA_ApplicationLayer.Usuarios;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Mappers.Contracts;
using CA_InterfaceAdapters_Mappers.Dtos.USUARIOS;
using CA_InterfaceAdapters_Models;

namespace CA_FrameworksDrivers_API.Endpoints
{
    public static class UsuariosEndpoints
    {
        public static void MapUsuariosEndpoints(this WebApplication app)
        {
            app.MapGet("Usuario/GetByPagination", async (int pageIndex, int pageSize, string? fullName, GetAllUsuariosUseCase<UsuarioResponseDTO, LstItemResponse<UsuarioResponseDTO>> useCase) =>
            {
                return await useCase.ExecuteAsync(pageIndex, pageSize, fullName);
            })
            .WithTags("Usuario")
            .WithName("GetAllUsuarios")
            .WithOpenApi();

            //app.MapPost("Usuario/Login", async (LoginRequestDTO request, LoginUsuarioUseCase<LoginRequestDTO, ItemResponse<LoginResponseDTO>, LoginResponseDTO> useCase) =>
            //{
            //    return await useCase.ExecuteAsync(request);
            //})
            //.WithTags("Usuario")
            //.WithName("LoginUser")
            //.WithOpenApi();

            app.MapPost("Usuario/Login", async (
    LoginRequestDTO request,
    LoginUsuarioUseCase<LoginRequestDTO> useCase
) =>
            {
                var usuarioLoginEntity = await useCase.ExecuteAsync(request);

                if (usuarioLoginEntity == null)
                    return Results.BadRequest(new
                    {
                        IsSuccess = false,
                        Errors = new List<string> { "Credenciales inválidas" }
                    });

                var responseDto = new LoginResponseDTO
                {
                    usuario = usuarioLoginEntity.nIdUsuario,
                    email = usuarioLoginEntity.sCorreoElectronico,
                    nombreCompleto = usuarioLoginEntity.sNombreCompleto,
                    permissions = usuarioLoginEntity.Permissions
                };

                return Results.Ok(new ItemResponse<LoginResponseDTO>
                {
                    Item = responseDto,
                    IsSuccess = true
                });
            });


            app.MapPost("Usuario/ChangePassAdmin", async (ChangePasswordRequestDTO request, ChangePasswordByAdminUseCase<ChangePasswordRequestDTO, ItemResponse<bool>> useCase) =>
            {
                return await useCase.ExecuteAsync(request);
            })
            .WithTags("Usuario")
            .WithName("ChangePassAdm")
            .WithOpenApi();

            app.MapPost("Usuario/Insert", async (UsuarioCreateDTO request, AddUsuarioUseCase<UsuarioCreateDTO, ItemResponse<int>> useCase) =>
            {
                return await useCase.ExecuteAsync(request);
            })
            .WithTags("Usuario")
            .WithName("InsertUsuario")
            .WithOpenApi();

            app.MapPut("Usuario/Update", async (UsuarioUpdateDTO request, EditUsuarioUseCase<UsuarioUpdateDTO, ItemResponse<bool>> useCase) =>
            {
                return await useCase.ExecuteAsync(request);
            })
            .WithTags("Usuario")
            .WithName("UpdateUsuario")
            .WithOpenApi();

            app.MapDelete("Usuario/Delete/{idUsuario}", async (int idUsuario, DeleteUsuarioUseCase<ItemResponse<bool>> useCase) =>
            {
                return await useCase.ExecuteAsync(idUsuario);
            })
            .WithTags("Usuario")
            .WithName("DeleteUsuario")
            .WithOpenApi();

            app.MapPut("Usuario/ChangePassword", async (UsuarioUpdatePasswordDTO request, ChangePasswordUseCase<UsuarioUpdatePasswordDTO, ItemResponse<bool>> useCase) =>
            {
                return await useCase.ExecuteAsync(request);
            })
            .WithTags("Usuario")
            .WithName("ChangePassword")
            .WithOpenApi();
        }
    }
}
