using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ErpApi.Logic
{
    public class JwtTokenService
    {
        private readonly JwtSettings _jwtSettings;

        public JwtTokenService(IOptions<JwtSettings> jwtOptions)
        {
            _jwtSettings = jwtOptions.Value;
        }

     
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;

        public ValidateTokenResponse ValidateToken(string token)
        {
            ValidateTokenResponse response = new();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretKey);

            try
            {
                var validationParams = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _issuer,
                    ValidateAudience = true,
                    ValidAudience = _audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero // no tolerance for expired tokens
                };

                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(token, validationParams, out validatedToken);
                if (principal != null)
                {
                    response.IsValid = true;
                    response.StatusCode = 0;
                    response.StatusDescription = "Token is valid.";
                }

            }
            catch (SecurityTokenExpiredException)
            {
                response.IsValid = false;
                response.StatusCode = 99;
                response.StatusDescription = "Token has expired.";
            }
            catch (SecurityTokenException ex)
            {
                response.IsValid = false;
                response.StatusCode = 99;
                response.StatusDescription = ex.Message+" - "+ex.InnerException;
            }
            catch (Exception ex)
            {
                response.IsValid = false;
                response.StatusCode = 99;
                response.StatusDescription = ex.Message + " - " + ex.InnerException;
            }
            return response;
        }

       


        public GenerateTokenResponse GenerateJwtToken(string username, string role)
        {
            GenerateTokenResponse response = new();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                // 👇 Expire in minutes, not hours
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = creds
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            if (token != null) {
                response.Token = tokenHandler.WriteToken(token);
                response.StatusCode = 0;
                response.StatusDescription = "Success";
            }
            else
            {
                response.StatusCode = 99;
                response.StatusDescription = "Failed to generate token.";
            }
            return response;
        }

        public bool ValidateJwtToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return false;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = _jwtSettings.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero // no tolerance for expired tokens
            };

            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                // Optional: ensure algorithm consistency
                if (validatedToken is JwtSecurityToken jwtToken &&
                    !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase))
                {
                    return false;
                }

                return true; //Token is valid
            }
            catch
            {
                return false; //Invalid or expired
            }
        }

    }
    //public class JwtTokenService
    //{
    //    private readonly string _key;
    //    private readonly string _issuer;
    //    private readonly string _audience;

    //    public JwtTokenService(string key, string issuer, string audience)
    //    {
    //        _key = key;
    //        _issuer = issuer;
    //        _audience = audience;
    //    }

    //    public string GenerateJwtToken(string username, string role)
    //    {
    //        var tokenHandler = new JwtSecurityTokenHandler();
    //        var key = Encoding.ASCII.GetBytes(_key);

    //        var claims = new[]
    //        {
    //        new Claim(ClaimTypes.Name, username),
    //        new Claim(ClaimTypes.Role, role),
    //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Unique ID for token
    //        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.Integer64)
    //    };

    //        var tokenDescriptor = new SecurityTokenDescriptor
    //        {
    //            Subject = new ClaimsIdentity(claims),
    //            Expires = DateTime.UtcNow.AddHours(2), // Token expiry time
    //            Issuer = _issuer,
    //            Audience = _audience,
    //            SigningCredentials = new SigningCredentials(
    //                new SymmetricSecurityKey(key),
    //                SecurityAlgorithms.HmacSha256Signature
    //            )
    //        };

    //        var token = tokenHandler.CreateToken(tokenDescriptor);
    //        return tokenHandler.WriteToken(token);
    //    }
    //}
}
