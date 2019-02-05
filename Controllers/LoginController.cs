using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly Settings _settings;
        private readonly SigningConfigurations _signingConfigurations;
        private readonly TokenConfigurations _tokenConfigurations;

        public LoginController(Settings settings, SigningConfigurations signingConfigurations, 
            TokenConfigurations tokenConfigurations)
        {
            _settings = settings;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody]User user)
        {
            try
            {
                bool validCredentials = false;
                if (user != null && !string.IsNullOrWhiteSpace(user.UserId))
                {
                    validCredentials = (user.UserId == _settings.TestUserId) && (user.AccessKey == _settings.TestAccessKey);
                }

                if (validCredentials)
                {
                    ClaimsIdentity identity = new ClaimsIdentity(
                        new GenericIdentity(user.UserId, "Login"),
                        new[] {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserId)
                        }
                    );

                    DateTime creationDate = DateTime.Now;
                    DateTime expirationDate = creationDate + TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

                    var handler = new JwtSecurityTokenHandler();

                    var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                    {
                        Issuer = _tokenConfigurations.Issuer,
                        Audience = _tokenConfigurations.Audience,
                        SigningCredentials = _signingConfigurations.SigningCredentials,
                        NotBefore = creationDate,
                        Expires = expirationDate
                    });

                    var token = handler.WriteToken(securityToken);

                    return Ok(new Token
                    {
                        Autheticated = true,
                        Created = creationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                        Expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                        AccessToken = token,
                        Message = "Ok"
                    });
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
} 