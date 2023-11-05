using PWD.Identity.DtoModels;
using PWD.Identity.InputDtos;
using PWD.Identity.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Emailing;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;

namespace PWD.Identity
{
    public class AccountAppService : ApplicationService, IAccountAppService
    {
        private readonly IdentityUserManager _userManager;
        private readonly IEmailSender _emailSender;

        public AccountAppService(IdentityUserManager userManager,
                                 IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }



        // POST /api/account/reset-password
        public virtual async Task ResetPassword(ResetPasswordInputDto inputDto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(inputDto.UserId);
                //var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                //await _userManager.ResetPasswordAsync(user, resetToken, inputDto.NewPassword);
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, inputDto.NewPassword);
                await _userManager.UpdateAsync(user);
            }
            catch (Exception)
            {
                throw new UserFriendlyException("Password reset not successfull.");
            }
        }

        // POST /api/account/reset-password-request
        public async Task ResetPasswordRequest(ResetPasswordRequestInputDto input)
        {
            var user = await _userManager.FindByEmailAsync(input.EmailAddress);
            if (user == null)
            {
                return;
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var body = L[
                "ResetPasswordRequest:EmailBody",
                user.Name,
                input.ReturnUrl.RemovePostFix("/"),
                System.Web.HttpUtility.UrlEncode(resetToken),
                user.TenantId,
                user.Id,
                System.Web.HttpUtility.UrlEncode(user.Email)
            ];

            try
            {
                await _emailSender.SendAsync(user.Email, L["ResetPasswordRequest:EmailSubject"], body);
            }
            catch (Exception)
            {
                throw new UserFriendlyException("Unable to process your request, please try again later.");
            }

            await _userManager.UpdateAsync(user);
        }

        public async Task<List<OrganizationUnitDto>> Offices(ChangePass input)
        {
            var user = await _userManager.FindByNameAsync(input.UserName);
            var units = await _userManager.GetOrganizationUnitsAsync(user);
            return ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(units);
        }
    }
}
