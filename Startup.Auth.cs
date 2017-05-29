using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.Extensions.Options;

namespace AuthWebAPI
{
    public partial class Startup
    {
        // symmetrical key to sign and validate JWTs
        public SymmetricSecurityKey signingKey;

        private void ConfigureAuth(IApplicationBuilder app)
        {
            // get the secret passphrase from config
            string secretKey = Configuration.GetSection("TokenAuthentication:SecretKey").Value;

            // create the symmetrical key to sign and validate JWTs
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            // create the token validation parameters
            var tokenValidationParameters = new TokenValidationParameters {
                // The signing key must match!
                ValidateIssuerSigningKey = true,                
                IssuerSigningKey = signingKey,
                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = Configuration.GetSection("TokenAuthentication:Issuer").Value,
                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = Configuration.GetSection("TokenAuthentication:Audience").Value,
                // Validate the token expiry
                ValidateLifetime = true,
                // If you want to allow a certain amount of clock drift, set that here
                ClockSkew = TimeSpan.Zero
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions{
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });


            // Add JWT generation endpoint

            var tokenProviderOptions = new Middleware.TokenProviderOptions(){
                Path = Configuration.GetSection("TokenAuthentication:TokenPath").Value,
                Audience = Configuration.GetSection("TokenAuthentication:Audience").Value,
                Issuer = Configuration.GetSection("TokenAuthentication:Issuer").Value,
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
            };

            app.UseMiddleware<Middleware.TokenProviderMiddleware>(Options.Create(tokenProviderOptions));
        }

    }
}
