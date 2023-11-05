using Microsoft.AspNetCore.Mvc;
using PWD.Identity.DtoModels;
using System.Threading.Tasks;

namespace PWD.Identity
{
    //[Route("api/permission-map")]
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionMapController : ControllerBase
    {
        private readonly PermissionMapService _permissionMapService;

        public PermissionMapController(PermissionMapService permissionMapService)
        {
            _permissionMapService = permissionMapService;
        }

        [HttpGet]
        [Route("permissions")]
        public async Task<ActionResult<PermissionDto>> GetPermissions(string providerName, string[] providerKeys, string permissionGroupKey)
        {
            return await _permissionMapService.GetAsync(providerName, providerKeys, permissionGroupKey);
        }

        [HttpGet]
        [Route("is-granted")]
        public async Task<ActionResult<bool>> IsGranted(string providerName, string[] providerKeys, string permissionKey, string permissionGroupKey)
        {
            return await _permissionMapService.IsGranted(providerName, providerKeys, permissionKey, permissionGroupKey);
        }

        //[HttpGet]
        //[Route("permissions2")]
        //public async Task<ActionResult<PermissionDto>> GetPermissions2(string providerName, string providerKey)
        //{
        //    return await _permissionMapService.GetAsync(providerName, providerKey);
        //}

        //[HttpGet]
        //[Route("is-granted2")]
        //public async Task<ActionResult<bool>> IsGranted2(string providerName, string providerKey, string permissionKey)
        //{
        //    return await _permissionMapService.IsGranted(providerName, providerKey, permissionKey);
        //}
    }
}
