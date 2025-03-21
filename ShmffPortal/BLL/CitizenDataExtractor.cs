using ShmffPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ShmffPortal.BLL
{
    public class CitizenDataExtractor
    {
        public static CitizenData GetCitizenData(string id)
        {
            if (!Regex.IsMatch(id, "^(\\d){14}$"))
            {
                throw new ArgumentException("National ID must be a string of 14 digits.", nameof(id));
            }

            var governorate = ((Governorates)Convert.ToByte(id.Substring(7, 2))).ToString();
 

            var year = 1700 + (int)(100 * char.GetNumericValue(id[0])) + Convert.ToByte(id.Substring(1, 2));
            var month = Convert.ToByte(id.Substring(3, 2));
            var day = Convert.ToByte(id.Substring(5, 2));

            var gender = ((byte)char.GetNumericValue(id[12]) % 2 == 1) ? "Male" : "Female";
            return new CitizenData(year, month, day, governorate, gender);
        }

    }
}
