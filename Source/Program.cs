using Microsoft.IdentityModel.Tokens;
using SoSystemUserService;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SuperOffice.DevNet.Online.SystemUser.NetCore.ConsoleApp
{
    public static class Constants
    {

        /**************************************************************************
        *** When migrating between SOD, STAGE and PRODUCTION **********************
        *** 1) Change ServiceConsoleCert.xml and SuperOfficeFederatedLogin.cert ***
        *** 2) Change the following constant values *******************************
        ************||||||||||||||||||||||||||||||||||||||||||||||||||*************
        ************\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/************/

        // set the environment system user endpoint

        public const string SystemUserEndpoint = "https://sod.superoffice.com/Login/services/PartnerSystemUserService.svc";

        // App Secret / Token

        public const string ApplicationToken = "YOUR_APPLICATION_TOKEN";

        // Application System User Token

        public const string SystemUserToken = "YOUR_APPLICATION_NAME-SomeRandomCode";

        // Customer ContextIdentifier

        public const string ContextIdentifier = "YOUR_TENANT_CONTEXT_IDENTIFIER";

        /*****^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^*******
        ******||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||*******
        ****** Change these values between SOD, Stage and Production **************
        **************************************************************************/
    }

    class Program
    {
        static void Main(string[] args)
        {
            var success = false;

            // read the private certificate file to sign the request to exchange token for ticket

            var certificate = File.ReadAllText(AppContext.BaseDirectory + "ServiceConsoleCert.xml");

            // sign the token

            var signedToken = Sign(certificate, Constants.SystemUserToken);

            // exchange the token for a system user ticket

            var response = Authenticate(Constants.ApplicationToken, Constants.ContextIdentifier, signedToken);

            if (response.IsSuccessful)
            {
                var superOfficeCertificate = new System.Security.Cryptography.X509Certificates.X509Certificate2(
                       AppContext.BaseDirectory + "SuperOfficeFederatedLogin.crt");

                // validate JWT token from SuperOffice

                var validToken = ValidateAndDecode(response.Token, new[] { new X509SecurityKey(superOfficeCertificate) });

                // validate response...
                // extract claims from the token
                var claims = validToken.Claims.Select(c => new Claim(c.Type, c.Value, ClaimValueTypes.String)).ToArray();

                // get the system user ticket (which is used in Http request header "SOTicket ${sysUserTicketValue}"

                var systemUserTicket = claims.FirstOrDefault(c => c.Type.Contains("ticket")).Value;

                // Online base REST endpoint url

                var webapi_url = claims.FirstOrDefault(c => c.Type.Contains("webapi")).Value;

                /* User systemUserTicket to create REST calls to the webapi_url...
                GET /Cust26759/api/v1/MDOList/category?flat=True HTTP/1.1
                    Host: sod.superoffice.com
                    accept: application/json
                    authorization: SOTICKET 7T:MAA3AGYAMQBlAGQAZAAxAGQAZQAwADgAYgAxAGEAYwBkADYAOAA0ADcAMQA2ADkAOQBhADQAZgBiADMAOQAyADsAMgAwADgANwAzADEANQAwADQAMwA7AEMAdQBzAHQAMgA2ADcANQA5AA==
                    accept-language: en
                    SO-AppToken: f2398a3a7wer3759d4b220e9a9c94321
                */
                success = true;

                Console.WriteLine($"Ticket: {systemUserTicket}");
                Console.WriteLine($"WebAPI URL: {webapi_url}");
            }

            Console.WriteLine(success ? "Success!" : "Failed!");
            Console.WriteLine("");
            Console.WriteLine("Press any key to quit...");
            Console.ReadLine();
        }

        private static string Sign(string privateKey, string token)
        {
            string str;
            string utcNow = DateTime.UtcNow.ToString("yyyyMMddHHmm");
            string signThis = string.Concat(token, ".", utcNow);
            using (RSACryptoServiceProvider rsaCryptoProvider = new RSACryptoServiceProvider())
            {
                rsaCryptoProvider.FromXmlString2(privateKey);
                byte[] signature = rsaCryptoProvider.SignData(Encoding.UTF8.GetBytes(signThis), "SHA256");
                str = string.Concat(signThis, ".", Convert.ToBase64String(signature));
            }
            return str;
        }

        public static AuthenticationResponse Authenticate(string appToken, string contextId, string signedToken)
        {
            AuthenticationRequest ar = new AuthenticationRequest(appToken, contextId, signedToken, TokenType.Jwt);

            var client = new PartnerSystemUserServiceClient(
                PartnerSystemUserServiceClient.EndpointConfiguration.BasicHttpBinding_IPartnerSystemUserService1);

            var result = client.AuthenticateAsync(ar);
            result.Wait();

            return result.Result;
        }

        private static JwtSecurityToken ValidateAndDecode(string jwt, IEnumerable<SecurityKey> signingKeys)
        {
            var validationParameters = new TokenValidationParameters
            {
                // Clock skew compensates for server time drift.
                // We recommend 5 minutes or less:
                ClockSkew = TimeSpan.FromMinutes(5),
                // Specify the key used to sign the token:
                IssuerSigningKeys = signingKeys,
                RequireSignedTokens = true,
                // Ensure the token hasn't expired:
                RequireExpirationTime = true,
                ValidateLifetime = true,
                // Ensure the token audience matches our audience value (default true):
                ValidateAudience = false,
                ValidAudience = "spn://db_serial",
                // Ensure the token was issued by a trusted authorization server (default true):
                ValidateIssuer = true,
                ValidIssuer = "SuperOffice AS"
            };

            try
            {
                var claimsPrincipal = new JwtSecurityTokenHandler()
                    .ValidateToken(jwt, validationParameters, out var rawValidatedToken);

                return (JwtSecurityToken)rawValidatedToken;
                // Or, you can return the ClaimsPrincipal
                // (which has the JWT properties automatically mapped to .NET claims)
            }
            catch (SecurityTokenValidationException stvex)
            {
                // The token failed validation!
                // TODO: Log it or display an error.
                throw new Exception($"Token failed validation: {stvex.Message}");
            }
            catch (ArgumentException argex)
            {
                // The token was not well-formed or was invalid for some other reason.
                // TODO: Log it or display an error.
                throw new Exception($"Token was invalid: {argex.Message}");
            }
        }
    }
}
