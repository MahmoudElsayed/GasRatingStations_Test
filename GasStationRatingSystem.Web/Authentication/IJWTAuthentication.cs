
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GasStationRatingSystem.Common;
using Microsoft.IdentityModel.Tokens;

namespace GasStationRatingSystem
{
    public interface IJWTAuthentication
    {
        public string Authenticate(string userName);
        public string SaveLanguage(string langCode);
    }

    public class JWTAuthentication : IJWTAuthentication
    {
        private readonly string _key;

        public JWTAuthentication(string key)
        {
            _key = key;
        }

        public string Authenticate(string userName)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDescritor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, userName) }),
                Expires = AppDateTime.Now.AddYears(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            return jwtSecurityTokenHandler.WriteToken(jwtSecurityTokenHandler.CreateToken(tokenDescritor));
        }
        public string SaveLanguage(string langCode)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDescritor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.System, langCode) }),
                Expires = AppDateTime.Now.AddYears(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            return jwtSecurityTokenHandler.WriteToken(jwtSecurityTokenHandler.CreateToken(tokenDescritor));
        }
    }
}
