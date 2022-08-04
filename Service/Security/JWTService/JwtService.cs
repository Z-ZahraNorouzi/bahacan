using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

using Service.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Action;
using BusinessModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using StaticData;
using Common.DTO;

namespace Service.Security.JWTService
{
    public class JwtService 
    {
    }
        /*
        public class JwtService : BaseApiController, IJwtService
        {
            private readonly JwtSettings jwtSettings;
            public JwtService(IOptionsSnapshot<JwtSettings> settings)
            {
                jwtSettings = settings.Value;
            }
            public async Task<string> Generate(UserInfoBusinessModel user)
            {
                var secretKey = Encoding.UTF8.GetBytes(jwtSettings.SecretKey); // longer than 6 character
                var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256);

                var encryptionkey = Encoding.UTF8.GetBytes(jwtSettings.Encryptkey); //must be 16 character
                var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

                var claims = await getClaims(user);
                var describtor = new SecurityTokenDescriptor
                {
                    Issuer = jwtSettings.Issuer,
                    Audience = jwtSettings.Audience,
                    IssuedAt = DateTime.Now,
                    NotBefore = DateTime.Now,
                    Expires = DateTime.Now.AddDays(jwtSettings.ExpiresTime),
                    SigningCredentials = signingCredentials,
                    EncryptingCredentials = encryptingCredentials,
                    Subject = new ClaimsIdentity(claims)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateJwtSecurityToken(describtor);
                var jwt = tokenHandler.WriteToken(securityToken);
                return jwt;
            }

            private async Task<IEnumerable<Claim>> getClaims(UserInfoBusinessModel currentUser)
            {

                var claimList = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,currentUser.UserName),
                    new Claim(ClaimTypes.NameIdentifier,currentUser.UserId.ToString())

                };


                //UserInfoAction userAction = new UserInfoAction();
                //var roles= await userAction.GetPermission(currentUser.UserId.Value);


                //foreach (var role in roles)
                //    claimList.Add(new Claim(ClaimTypes.Role, role.Controller));

                return claimList;
            }


        }  */
    }
