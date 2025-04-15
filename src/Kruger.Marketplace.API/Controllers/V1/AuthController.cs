using Asp.Versioning;
using Kruger.Marketplace.Business.Interfaces.Notificador;
using Kruger.Marketplace.Business.Interfaces.Services.CadastroBasico;
using Kruger.Marketplace.Business.Models.CadastroBasico;
using Kruger.Marketplace.Business.Models.Settings;
using Kruger.Marketplace.Application.App;
using Kruger.Marketplace.Application.ViewModels.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Kruger.Marketplace.API.Controllers.V1
{
    [ApiVersion(1.0)]
    [Route("api/v{version:apiversion}/[controller]")]
    public class AuthController(INotificador notificador,
                                IAppIdentityUser user,
                                IOptions<AppSettings> appSettings,
                                IVendedorService vendedorService,
                                SignInManager<IdentityUser> signInManager,
                                UserManager<IdentityUser> userManager) : MainController(notificador, user)
    {
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly AppSettings _appSettings = appSettings.Value;
        private readonly IVendedorService _vendedorService = vendedorService;

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterViewModel usuarioRegistro)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = usuarioRegistro.Email,
                Email = usuarioRegistro.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, usuarioRegistro.Password);

            if (!result.Succeeded)
            {
                NotificarInvalidModelStateError(result);
                return CustomResponse(HttpStatusCode.BadRequest);
            }

            if (!await _vendedorService.Add(new Vendedor(Guid.Parse(user.Id), user.UserName, user.Email, usuarioRegistro.Password)))
                return CustomResponse(HttpStatusCode.BadRequest, "Falha ao cadastrar vendedor.");

            await _signInManager.SignInAsync(user, false);

            return CustomResponse(HttpStatusCode.OK, await GerarJWT(user.Email));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginViewModel login)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, true);

            if (!result.Succeeded)
                return CustomResponse(HttpStatusCode.BadRequest, "Usuário ou senha incorretos.");

            return CustomResponse(HttpStatusCode.OK, await GerarJWT(login.Email));
        }

        private async Task<string> GerarJWT(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var date = DateTime.Now;
            var claims = await GetUserClaims(email);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _appSettings.Emissor,
                Claims = new Dictionary<string, object>
                {
                    { JwtRegisteredClaimNames.Aud, _appSettings.ValidoEm }
                },
                Expires = date.AddMinutes(_appSettings.ExpiracaoTokenMinutos),
                NotBefore = date,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Jwt)),
                                                                SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private async Task<List<Claim>> GetUserClaims(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(user);
            
            var claims = new List<Claim>() { new(ClaimTypes.Name, user.UserName) };

            foreach (var role in roles)
                claims.Add(new(ClaimTypes.Role, role));

            return claims;
        }
    }
}