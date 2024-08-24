using Application.Common;
using Application.Roles.CommandAndQueries;
using Application.Roles.Interfaces;
using Application.Roles.Responses;
using Domain.Common.Api;
using Infra.Utils;

namespace Infra.Roles.Implantations;

public class RoleService(IBaseHttpClient client) : IRoleService
{
    public async Task<ApiResult> CreateRole(CreateRoleCommand command)
    {
        var result = await client.PostAsync<CreateRoleCommand, ApiResult>(RoleRouts.CreateRole, command);
        return result;
    }

    public async Task<ApiResult> EditRole(EditRoleCommand command)
    {
        var result = await client.PutAsync<EditRoleCommand, ApiResult>(RoleRouts.CreateRole, command);
        return result;
    }

    public async Task<RoleDto?> GetRoleById(long roleId)
    {
        var result = await client.GetAsync<ApiResult<RoleDto>>(RoleRouts.GetRoleById.BuildRequestUrl([roleId])!);
        return result?.Data;
    }

    public async Task<List<RoleDto>?> GetRoles()
    {
        var result = await client.GetAsync<ApiResult<List<RoleDto>>>(RoleRouts.GetAllRoles!);
        return result?.Data;
    }
}