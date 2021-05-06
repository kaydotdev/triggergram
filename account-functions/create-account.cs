using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AlbumOnFunctions.Account.CreateAccountTrigger
{
    public static class CreateAccount
    {
        [FunctionName("create_account")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route=null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("'Create account' requested.");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var accountOnCreate = JsonConvert.DeserializeObject<AccountOnCreate>(requestBody);
                return new OkObjectResult(accountOnCreate.Email);
            }
            catch (JsonException exception)
            {
                return new BadRequestObjectResult(exception.Message);
            }
        }

        public class AccountOnCreate {
            public string Email { get; set; }
            public string Nickname { get; set; }
            public string FullName { get; set; }
            
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
        }
    }
}
