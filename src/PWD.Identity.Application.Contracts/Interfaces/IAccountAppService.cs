using PWD.Identity.InputDtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace PWD.Identity.Interfaces
{
    public interface IAccountAppService : IApplicationService
    {
        Task ResetPassword(ResetPasswordInputDto input);
        Task ResetPasswordRequest(ResetPasswordRequestInputDto input);
    }
}
