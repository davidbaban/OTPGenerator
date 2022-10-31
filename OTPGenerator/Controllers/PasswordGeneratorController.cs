using Microsoft.AspNetCore.Mvc;
using OTPGenerator.InputModels;
using OTPGenerator.Services;

namespace OTPGenerator.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PasswordGeneratorController : ControllerBase
    {
        public SecurityService SecurityService { get; set; }

        public PasswordGeneratorController()
        {
            SecurityService = new SecurityService();
        }

        [HttpGet(Name = "GetGeneratedOneTimePassword")]
        public string GetGeneratedOneTimePassword(string userId)
        {
            return SecurityService.GenerateOneTimePassword(userId, DateTime.Now);
        }

        [HttpPost(Name = "ValidateSecurityPassword")]
        public bool ValidateSecurityPassword(ValidateGeneratedPasswordRequest request)
        {
            var range = Enumerable.Range(0, 30);

            foreach (var interval in range)
            {
                var candidatePassword = SecurityService.GenerateOneTimePassword(request.UserId, DateTime.Now.AddSeconds(-1 * interval));
                if (candidatePassword == request.OneTimePassword)
                    return true;
            }

            return false;
        }
    }
}
