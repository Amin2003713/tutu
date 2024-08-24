using Application.Roles.Responses;

namespace Application.Roles.Interfaces;

public interface IRoleService
{
    Task<ApiResult> CreateRole(CreateRoleCommand command);
    Task<ApiResult> EditRole(EditRoleCommand command);
    Task<RoleDto?> GetRoleById(long roleId);
    Task<List<RoleDto>?> GetRoles();
}