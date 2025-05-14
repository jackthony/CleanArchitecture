using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.Usuarios
{
    public interface IUsuarioRepository : IRepository<UsuarioEntity, UsuarioModel>
    {
        Task<bool> ChangePassword(UsuarioEntity entity);
        Task<UsuarioModel> GetByEmailAsync(UsuarioEntity entity);
        Task<bool> ChangePasswordAdminAsync(UsuarioEntity entity);
    }
}
