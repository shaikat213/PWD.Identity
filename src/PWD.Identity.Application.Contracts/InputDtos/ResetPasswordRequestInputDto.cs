namespace PWD.Identity.InputDtos
{
    public class ResetPasswordRequestInputDto
    {
        public string EmailAddress { get; set; }
        public string ReturnUrl { get; set; }
    }
}
