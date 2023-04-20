using CoffeeNext.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CoffeeNext.Services
{
    public class AppAuthenticationStateProvider
    {
        private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
      
        public async Task<string> GetAllClaims()
        {
            string savedToken = await SecureStorage.GetAsync("token");
            Debug.WriteLine("DebugRon.. Saved token is : " + savedToken);
            if (string.IsNullOrWhiteSpace(savedToken)) return null;

            JwtSecurityToken jwtSecurityToken = jwtSecurityTokenHandler.ReadJwtToken(savedToken);
            DateTime dtExpiry = jwtSecurityToken.ValidTo;
          
            if (dtExpiry < DateTime.UtcNow)
            {
                SecureStorage.Remove("token");
                Debug.WriteLine("DebugRon.. token expired and removed date token is : " + SecureStorage.GetAsync("token"));
                return null;
            }

            //var Email = jwtSecurityToken.Claims.First(claim => claim.Type == "Email")?.Value;
            string ee = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type.Equals("Email", StringComparison.OrdinalIgnoreCase))?.Value;
            string id = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == "Id")?.Value;
            string fn = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == "FirstName")?.Value;
          

            Debug.WriteLine("Email is " + ee + "   " +id  + " FN   " + fn);//+ "  userid is  " + User_Id+ "   UserFN  " + User_FN + "    user Name " + Name_);
            var claims = GetAllClaims(jwtSecurityToken);

            /*string email = claims.FindFirst(c => c.Type.Equals("email", StringComparison.OrdinalIgnoreCase))?.Value;
            string firstName = claims.FindFirst(c => c.Type.Equals("firstname", StringComparison.OrdinalIgnoreCase))?.Value;
            string lastName = claims.FindFirst(c => c.Type.Equals("lastname", StringComparison.OrdinalIgnoreCase))?.Value;
            string dob = claims.FindFirst(c => c.Type.Equals("dob", StringComparison.OrdinalIgnoreCase))?.Value;*/


            foreach (var claim in claims)
            {
                Debug.WriteLine("DebugRon.. Claims is ........" + claim.Type + " Value..is . " + claim.Value + " subject  is.. " + claim.Subject);
              
            }
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims,"jwt"));

            string e = user.FindFirst(ClaimTypes.Email)?.Value;

            string a = user.FindFirst("Email")?.Value;
            string b = user.FindFirst("LastName")?.Value;
            Debug.WriteLine("Email found is .............................."+ a+ "last name "  +b);
            return user.Claims.FirstOrDefault()?.Value;

        }
        private  IList<Claim> GetAllClaims(JwtSecurityToken jwtToken)
        {
            IList<Claim> claims = jwtToken.Claims.ToList();           
            claims.Add(new Claim(ClaimTypes.Name, jwtToken.Subject));
            return claims;
        } 
    }
}
