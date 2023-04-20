using CoffeeNext.Models;
using CoffeeNext.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[assembly: Xamarin.Forms.Dependency(typeof(CountryList))]
namespace CoffeeNext.Util
{
    public interface ICountryList<T>
    {
        Task<IEnumerable<Country>> GetCountriesList();
    }

    public class CountryList : ICountryList<Country>
    {
        readonly List<Country> countriesList;

        public CountryList()
        {
            countriesList = new List<Country>();
        }

        public async Task<IEnumerable<Country>> GetCountriesList()
        {
            int idd = 0;
            CultureInfo[] CultureInfosList = CultureInfo.GetCultures(CultureTypes.AllCultures & CultureTypes.SpecificCultures);
            // CultureTypes.SpecificCultures
            foreach (CultureInfo cultureInfo in CultureInfosList)
            {
                RegionInfo R = new RegionInfo(cultureInfo.Name);
                if (!(countriesList.Any(c => c.Name.ToLower() == R.EnglishName.ToLower())))
                {
                    if (!String.IsNullOrEmpty(R.EnglishName.ToLower()))
                        countriesList.Add(new Country() { Id = ++idd, Name = R.EnglishName });
                }
            }
            return await Task.FromResult(countriesList);
            //return countries.ToArray();
        }
    }

}
