using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CommerceRazorDemo
{
    public class MockAuthenticatedUser : AuthenticationHandler<AuthenticationSchemeOptions>
    {


        public MockAuthenticatedUser(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock) 
        {

        }

        public string Id { get; set; } = "1";
        public string Name { get; set; } = "jerry";
        public string Role { get; set; } = "Customer";
        public string Email { get; set; } = "jerry@seinfeld.com";

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = new[]
              {
          new Claim(ClaimTypes.NameIdentifier, Id),
          new Claim(ClaimTypes.Name, Name),
          new Claim(ClaimTypes.Role, Role),
          new Claim(ClaimTypes.Email, Email),
        };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return await Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}

