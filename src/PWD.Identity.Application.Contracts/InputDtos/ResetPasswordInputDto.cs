using System;

namespace PWD.Identity.InputDtos
{
    public class ResetPasswordInputDto
    {
        public string UserId { get; set; }
        public string NewPassword { get; set; }
    }
}
