using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace CommerceRazorDemo
{
    public class MockAuthenticatedUser : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        const string userId = "1";
        const string userName = "Jerry Seinfeld";
        const string userRole = "Customer";

        public MockAuthenticatedUser(
          IOptionsMonitor<AuthenticationSchemeOptions> options,
          ILoggerFactory logger,
          UrlEncoder encoder,
          ISystemClock clock)
          : base(options, logger, encoder, clock) { }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, userRole),
                    new Claim(ClaimTypes.Email, "jerry@seinfeld.com"),
                };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return await Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
