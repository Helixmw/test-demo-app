using System.Security.Claims;
using GolfingDataAccessLib.Data;
using GolfingDataAccessLib.DTOs.User;
using GolfingDataAccessLib.Enums;
using GolfingDataAccessLib.Exceptions;
using GolfingDataAccessLib.interfaces;

namespace GolfingDataAccessLib.Services;
/// <summary>
/// Handles Checking user credentials for derived services of BaseService
/// </summary>
/// <typeparam name="T"></typeparam>
public partial class BaseScheduleService<T>  where T : class, ISchedule
{
    protected void VerifyUserRole(ClaimsPrincipal user, Roles role){
        if(!user.IsInRole(role.ToString()))
        throw new AuthorizationException("You are not allowed to carry out such an operation");
    }

    protected void VerifyDTOUserRole(IUserDTO user, Roles role){
        if(user.Role != role.ToString())
        throw new AuthorizationException("You are not allowed to carry out such an operation");
    }

    
}