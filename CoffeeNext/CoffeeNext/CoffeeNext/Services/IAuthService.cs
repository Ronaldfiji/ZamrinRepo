using CoffeeNext.Helper;
using CoffeeNext.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;


[assembly: Dependency(typeof(AuthService))]
namespace CoffeeNext.Services
{
    public  interface IAuthService 
    {
        Task SignOff();
        Task<ClaimsPrincipal> GetUserClaims();
        Task<bool> IsUserSessionValid();


    }
    public class AuthService : IAuthService
    {
        public Settings Settings { get; } = new Settings();

        public async Task<bool> IsUserSessionValid()
        {
            var claims = await GetUserClaims();            
            if (claims != null && claims.Claims.Any())  return true;         
                return false;
        }
        public async Task SignOff()
        {
            try
            {               
                
                Settings.Instance.IsUserLoggedInStatus = false;                          
                (Shell.Current as AppShell).LoginStatusDisplayFlyOut = false;
                SecureStorage.Remove("token");
                SecureStorage.Remove("refreshToken");
                SecureStorage.Remove("userId");
                Preferences.Clear();
                await Task.FromResult(true); 
                Application.Current.MainPage = new AppShell();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to signout from app: " + ex);
                throw ex;

            }
        }

        private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();      
        
        public async Task<ClaimsPrincipal> GetUserClaims()
        {
            try
            {
                string savedToken = await SecureStorage.GetAsync("token");
                if (savedToken == null) return null;
                JwtSecurityToken jwtToken = jwtSecurityTokenHandler.ReadJwtToken(savedToken);                
                string fn = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "FamilyName")?.Value;

                Debug.WriteLine("DebugRon.. fn................. : " + fn);
                foreach(var claim in jwtToken.Claims)
                {
                    Debug.WriteLine("Claims are : Type " + claim.Type);//+ " Value:  " + claim.Value + " Name subject; " + claim.Subject.Name); 
                }

                DateTime tokenExpiryDt = jwtToken.ValidTo;

                if (tokenExpiryDt < DateTime.UtcNow)
                {
                    SecureStorage.Remove("token");
                    Debug.WriteLine("DebugRon.. token expired and removed date token is : " + SecureStorage.GetAsync("token"));
                    return new ClaimsPrincipal();
                }
                var claims = GetAllClaims(jwtToken);
                var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
                return user;
            }
            catch(Exception ex)
            {                
                throw new Exception($"{typeof(IAuthService)} -> {nameof(GetUserClaims)} ,-> failed to extract OR validate" +
                    $" JWT token from Secure storage.." + ex);
            }
        }
        private IList<Claim> GetAllClaims(JwtSecurityToken jwtToken)
        {
            IList<Claim> claims = jwtToken.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, jwtToken.Subject));
            return claims;
        }

    }

}
