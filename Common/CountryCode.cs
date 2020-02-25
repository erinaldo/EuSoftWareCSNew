using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EuSoft.Common
{
    public class CountryCode
    {
        /// <summary>
        /// 国家代码
        /// </summary>
        private string Code { get; set; }
        /// <summary>
        /// 国家名称
        /// </summary>
        private string Name { get; set; }
        /// <summary>
        /// 国家代码列表
        /// </summary>
        private static List<CountryCode> List
        {
            get { return CountryList(); }
        }

        /// <summary>
        /// 根据国家名称获取代码
        /// </summary>
        /// <param name="countryname"></param>
        /// <returns></returns>
        public static string GetCountryCodeByName(string countryname)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(countryname))
            {
                CountryCode con = List.FirstOrDefault(s => s.Name.ToUpper() == countryname.ToUpper());
                if (con != null)
                {
                    result = con.Code;
                }
            }
            return result;
        }

        #region 国家代码表
        /// <summary>
        /// 国家代码表
        /// </summary>
        /// <returns></returns>
        private static List<CountryCode> CountryList()
        {
            List<CountryCode> countries = new List<CountryCode>();
            countries.Add(new CountryCode { Code = "AL", Name = "Albania" });
            countries.Add(new CountryCode { Code = "GA", Name = "Gabon" });
            countries.Add(new CountryCode { Code = "NE", Name = "Niger" });
            countries.Add(new CountryCode { Code = "DZ", Name = "Algeria" });
            countries.Add(new CountryCode { Code = "GM", Name = "Gambia" });
            countries.Add(new CountryCode { Code = "NG", Name = "Nigeria" });
            countries.Add(new CountryCode { Code = "AS", Name = "American Samoa" });
            countries.Add(new CountryCode { Code = "GE", Name = "Georgia" });
            countries.Add(new CountryCode { Code = "NO", Name = "Norway" });
            countries.Add(new CountryCode { Code = "AD", Name = "Andorra" });
            countries.Add(new CountryCode { Code = "DE", Name = "Germany" });
            countries.Add(new CountryCode { Code = "OM", Name = "Oman" });
            countries.Add(new CountryCode { Code = "AO", Name = "Angola" });
            countries.Add(new CountryCode { Code = "GH", Name = "Ghana" });
            countries.Add(new CountryCode { Code = "PK", Name = "Pakistan" });
            countries.Add(new CountryCode { Code = "AI", Name = "Anguilla" });
            countries.Add(new CountryCode { Code = "GI", Name = "Gibraltar" });
            countries.Add(new CountryCode { Code = "PW", Name = "Palau" });
            countries.Add(new CountryCode { Code = "AG", Name = "Antigua" });
            countries.Add(new CountryCode { Code = "GR", Name = "Greece" });
            countries.Add(new CountryCode { Code = "PA", Name = "Panama" });
            countries.Add(new CountryCode { Code = "AR", Name = "Argentina" });
            countries.Add(new CountryCode { Code = "GL", Name = "Greenland" });
            countries.Add(new CountryCode { Code = "PG", Name = "Papua New Guinea" });
            countries.Add(new CountryCode { Code = "AM", Name = "Armenia" });
            countries.Add(new CountryCode { Code = "GD", Name = "Grenada" });
            countries.Add(new CountryCode { Code = "PY", Name = "Paraguay" });
            countries.Add(new CountryCode { Code = "AW", Name = "Aruba" });
            countries.Add(new CountryCode { Code = "GP", Name = "Guadeloupe" });
            countries.Add(new CountryCode { Code = "PE", Name = "Peru" });
            countries.Add(new CountryCode { Code = "AU", Name = "Australia" });
            countries.Add(new CountryCode { Code = "GU", Name = "Guam" });
            countries.Add(new CountryCode { Code = "PH", Name = "Philippines" });
            countries.Add(new CountryCode { Code = "AT", Name = "Austria" });
            countries.Add(new CountryCode { Code = "GT", Name = "Guatemala" });
            countries.Add(new CountryCode { Code = "PL", Name = "Poland" });
            countries.Add(new CountryCode { Code = "AZ", Name = "Azerbaijan" });
            countries.Add(new CountryCode { Code = "GN", Name = "Guinea" });
            countries.Add(new CountryCode { Code = "PT", Name = "Portugal" });
            countries.Add(new CountryCode { Code = "BS", Name = "Bahamas" });
            countries.Add(new CountryCode { Code = "GW", Name = "Guinea-Bissau" });
            countries.Add(new CountryCode { Code = "US", Name = "Puerto Rico" });
            countries.Add(new CountryCode { Code = "BH", Name = "Bahrain" });
            countries.Add(new CountryCode { Code = "GY", Name = "Guyana" });
            countries.Add(new CountryCode { Code = "QA", Name = "Qatar" });
            countries.Add(new CountryCode { Code = "BD", Name = "Bangladesh" });
            countries.Add(new CountryCode { Code = "HT", Name = "Haiti" });
            countries.Add(new CountryCode { Code = "RE", Name = "Reunion Island" });
            countries.Add(new CountryCode { Code = "BB", Name = "Barbados" });
            countries.Add(new CountryCode { Code = "HN", Name = "Honduras" });
            countries.Add(new CountryCode { Code = "RO", Name = "Romania" });
            countries.Add(new CountryCode { Code = "BY", Name = "Belarus" });
            countries.Add(new CountryCode { Code = "HK", Name = "Hong Kong" });
            countries.Add(new CountryCode { Code = "RU", Name = "Russia" });
            countries.Add(new CountryCode { Code = "BE", Name = "Belgium" });
            countries.Add(new CountryCode { Code = "HU", Name = "Hungary" });
            countries.Add(new CountryCode { Code = "RW", Name = "Rwanda" });
            countries.Add(new CountryCode { Code = "BZ", Name = "Belize" });
            countries.Add(new CountryCode { Code = "IS", Name = "Iceland" });
            countries.Add(new CountryCode { Code = "MP", Name = "Saipan" });
            countries.Add(new CountryCode { Code = "BJ", Name = "Republic of Benin" });
            countries.Add(new CountryCode { Code = "IN", Name = "India" });
            countries.Add(new CountryCode { Code = "SM", Name = "San Marino" });
            countries.Add(new CountryCode { Code = "BM", Name = "Bermuda" });
            countries.Add(new CountryCode { Code = "ID", Name = "Indonesia" });
            countries.Add(new CountryCode { Code = "SA", Name = "Saudi Arabia" });
            countries.Add(new CountryCode { Code = "BT", Name = "Bhutan" });
            countries.Add(new CountryCode { Code = "IE", Name = "Ireland" });
            countries.Add(new CountryCode { Code = "SN", Name = "Senegal" });
            countries.Add(new CountryCode { Code = "BO", Name = "Bolivia" });
            countries.Add(new CountryCode { Code = "IL", Name = "Israel" });
            countries.Add(new CountryCode { Code = "SC", Name = "Seychelles" });
            countries.Add(new CountryCode { Code = "BW", Name = "Botswana" });
            countries.Add(new CountryCode { Code = "IT", Name = "Italy" });
            countries.Add(new CountryCode { Code = "SL", Name = "Sierra Leone" });
            countries.Add(new CountryCode { Code = "BR", Name = "Brazil" });
            countries.Add(new CountryCode { Code = "CI", Name = "Ivory Coast" });
            countries.Add(new CountryCode { Code = "SG", Name = "Singapore" });
            countries.Add(new CountryCode { Code = "VG", Name = "British Virgin Is." });
            countries.Add(new CountryCode { Code = "JM", Name = "Jamaica" });
            countries.Add(new CountryCode { Code = "SK", Name = "Slovak" });
            countries.Add(new CountryCode { Code = "BN", Name = "Brunei" });
            countries.Add(new CountryCode { Code = "JP", Name = "Japan" });
            countries.Add(new CountryCode { Code = "SI", Name = "Slovenia" });
            countries.Add(new CountryCode { Code = "BG", Name = "Bulgaria" });
            countries.Add(new CountryCode { Code = "JO", Name = "Jordan" });
            countries.Add(new CountryCode { Code = "ZA", Name = "South Africa" });
            countries.Add(new CountryCode { Code = "BF", Name = "Burkino Faso" });
            countries.Add(new CountryCode { Code = "KZ", Name = "Kazakhstan" });
            countries.Add(new CountryCode { Code = "KR", Name = "Korea South" });
            countries.Add(new CountryCode { Code = "MM", Name = "Burma" });
            countries.Add(new CountryCode { Code = "KE", Name = "Kenya" });
            countries.Add(new CountryCode { Code = "ES", Name = "Spain" });
            countries.Add(new CountryCode { Code = "BI", Name = "Burundi" });
            countries.Add(new CountryCode { Code = "KW", Name = "Kuwait" });
            countries.Add(new CountryCode { Code = "LK", Name = "Sri Lanka" });
            countries.Add(new CountryCode { Code = "KH", Name = "Cambodia" });
            countries.Add(new CountryCode { Code = "KG", Name = "Kyrgyzstan" });
            countries.Add(new CountryCode { Code = "KN", Name = "SSt. Kitts & Nevis" });
            countries.Add(new CountryCode { Code = "CM", Name = "Cameroon" });
            countries.Add(new CountryCode { Code = "LV", Name = "Latvia" });
            countries.Add(new CountryCode { Code = "LC", Name = "St. Lucia" });
            countries.Add(new CountryCode { Code = "CA", Name = "Canada" });
            countries.Add(new CountryCode { Code = "LB", Name = "Lebanon" });
            countries.Add(new CountryCode { Code = "VC", Name = "St. Vincent" });
            countries.Add(new CountryCode { Code = "CV", Name = "Cape Verde" });
            countries.Add(new CountryCode { Code = "LS", Name = "Lesotho" });
            countries.Add(new CountryCode { Code = "SR", Name = "Suriname" });
            countries.Add(new CountryCode { Code = "KY", Name = "Cayman Islands" });
            countries.Add(new CountryCode { Code = "LI", Name = "Liechtenstein" });
            countries.Add(new CountryCode { Code = "SZ", Name = "Swaziland" });
            countries.Add(new CountryCode { Code = "CF", Name = "Central African" });
            countries.Add(new CountryCode { Code = "LT", Name = "Lithuania" });
            countries.Add(new CountryCode { Code = "SE", Name = "Sweden" });
            countries.Add(new CountryCode { Code = "TD", Name = "Chad" });
            countries.Add(new CountryCode { Code = "LU", Name = "Luxembourg" });
            countries.Add(new CountryCode { Code = "CH", Name = "Switzerland" });
            countries.Add(new CountryCode { Code = "CL", Name = "Chile" });
            countries.Add(new CountryCode { Code = "MO", Name = "Macau" });
            countries.Add(new CountryCode { Code = "SY", Name = "Syria" });
            countries.Add(new CountryCode { Code = "CN", Name = "China" });
            countries.Add(new CountryCode { Code = "MK", Name = "Macedonia" });
            countries.Add(new CountryCode { Code = "TW", Name = "Taiwan" });
            countries.Add(new CountryCode { Code = "CO", Name = "Colombia" });
            countries.Add(new CountryCode { Code = "MG", Name = "Madagascar" });
            countries.Add(new CountryCode { Code = "TZ", Name = "Tanzania" });
            countries.Add(new CountryCode { Code = "CG", Name = "Congo" });
            countries.Add(new CountryCode { Code = "MW", Name = "Malawi" });
            countries.Add(new CountryCode { Code = "TH", Name = "Thailand" });
            countries.Add(new CountryCode { Code = "CD", Name = "Congo, The Republic of" });
            countries.Add(new CountryCode { Code = "MY", Name = "Malaysia" });
            countries.Add(new CountryCode { Code = "TG", Name = "Togo" });
            countries.Add(new CountryCode { Code = "CK", Name = "Cook Islands" });
            countries.Add(new CountryCode { Code = "MV", Name = "Maldives" });
            countries.Add(new CountryCode { Code = "TT", Name = "Trinidad & Tobago" });
            countries.Add(new CountryCode { Code = "CR", Name = "Costa Rica" });
            countries.Add(new CountryCode { Code = "ML", Name = "Mali" });
            countries.Add(new CountryCode { Code = "TN", Name = "Tunisia" });
            countries.Add(new CountryCode { Code = "CI", Name = "Cote D'Ivoire" });
            countries.Add(new CountryCode { Code = "MT", Name = "Malta" });
            countries.Add(new CountryCode { Code = "TR", Name = "Turkey" });
            countries.Add(new CountryCode { Code = "HR", Name = "Croatia" });
            countries.Add(new CountryCode { Code = "MH", Name = "Marshall Islands" });
            countries.Add(new CountryCode { Code = "TM", Name = "Turkmenistan, Republic of" });
            countries.Add(new CountryCode { Code = "CY", Name = "Cyprus" });
            countries.Add(new CountryCode { Code = "MQ", Name = "Martinique" });
            countries.Add(new CountryCode { Code = "TC", Name = "Turks & Caicos Is." });
            countries.Add(new CountryCode { Code = "CZ", Name = "Czech Republic" });
            countries.Add(new CountryCode { Code = "MR", Name = "Mauritania" });
            countries.Add(new CountryCode { Code = "MU", Name = "Mauritius" });
            countries.Add(new CountryCode { Code = "AE", Name = "United Arab Emirates" });
            countries.Add(new CountryCode { Code = "DJ", Name = "Djibouti" });
            countries.Add(new CountryCode { Code = "MX", Name = "Mexico" });
            countries.Add(new CountryCode { Code = "US", Name = "United States" });
            countries.Add(new CountryCode { Code = "DM", Name = "Dominica" });
            countries.Add(new CountryCode { Code = "FM", Name = "Micronesia" });
            countries.Add(new CountryCode { Code = "UG", Name = "Uganda" });
            countries.Add(new CountryCode { Code = "DM", Name = "Dominican Republic" });
            countries.Add(new CountryCode { Code = "MD", Name = "Moldova" });
            countries.Add(new CountryCode { Code = "UA", Name = "Ukraine" });
            countries.Add(new CountryCode { Code = "EC", Name = "Ecuador" });
            countries.Add(new CountryCode { Code = "MC", Name = "Monaco" });
            countries.Add(new CountryCode { Code = "GB", Name = "United Kingdom" });
            countries.Add(new CountryCode { Code = "EG", Name = "Egypt" });
            countries.Add(new CountryCode { Code = "MN", Name = "Mongolia" });
            countries.Add(new CountryCode { Code = "UY", Name = "Uruguay" });
            countries.Add(new CountryCode { Code = "SV", Name = "El Salvador" });
            countries.Add(new CountryCode { Code = "MS", Name = "Montserrat" });
            countries.Add(new CountryCode { Code = "UZ", Name = "Uzbekistan" });
            countries.Add(new CountryCode { Code = "GQ", Name = "Equatorial Guinea" });
            countries.Add(new CountryCode { Code = "MA", Name = "Morocco" });
            countries.Add(new CountryCode { Code = "VU", Name = "Vanuatu" });
            countries.Add(new CountryCode { Code = "ER", Name = "Eritrea" });
            countries.Add(new CountryCode { Code = "MZ", Name = "Mozambique" });
            countries.Add(new CountryCode { Code = "VA", Name = "Vatican City" });
            countries.Add(new CountryCode { Code = "EE", Name = "Estonia" });
            countries.Add(new CountryCode { Code = "MM", Name = "Myanmar" });
            countries.Add(new CountryCode { Code = "VE", Name = "Venezuela" });
            countries.Add(new CountryCode { Code = "ET", Name = "Ethiopia" });
            countries.Add(new CountryCode { Code = "NA", Name = "Namibia" });
            countries.Add(new CountryCode { Code = "VN", Name = "Vietnam" });
            countries.Add(new CountryCode { Code = "ET", Name = "Ethiopia" });
            countries.Add(new CountryCode { Code = "NA", Name = "Namibia" });
            countries.Add(new CountryCode { Code = "VN", Name = "Vietnam" });
            countries.Add(new CountryCode { Code = "FO", Name = "Faeroe Islands" });
            countries.Add(new CountryCode { Code = "NP", Name = "Nepal" });
            countries.Add(new CountryCode { Code = "WF", Name = "Wallis & Futuna Islands" });
            countries.Add(new CountryCode { Code = "FJ", Name = "Fiji" });
            countries.Add(new CountryCode { Code = "NL", Name = "Nepal" });
            countries.Add(new CountryCode { Code = "YE", Name = "Yemen" });
            countries.Add(new CountryCode { Code = "FI", Name = "Finland" });
            countries.Add(new CountryCode { Code = "AN", Name = "Netherlands Antilles" });
            countries.Add(new CountryCode { Code = "ZM", Name = "Zambia" });
            countries.Add(new CountryCode { Code = "FR", Name = "France" });
            countries.Add(new CountryCode { Code = "NC", Name = "New Caledonia" });
            countries.Add(new CountryCode { Code = "ZW", Name = "Zimbabwe" });
            countries.Add(new CountryCode { Code = "GF", Name = "French Guiana" });
            countries.Add(new CountryCode { Code = "NZ", Name = "New Zealand" });
            countries.Add(new CountryCode { Code = "PF", Name = "French Polynesia" });
            countries.Add(new CountryCode { Code = "NI", Name = "Nicaragua" });
            countries.Add(new CountryCode { Code = "BA", Name = "Herzegovina" });
            countries.Add(new CountryCode { Code = "DK", Name = "Denmark" });
            countries.Add(new CountryCode { Code = "IR", Name = "Iran" });
            countries.Add(new CountryCode { Code = "IQ", Name = "Iraq" });
            countries.Add(new CountryCode { Code = "LY", Name = "Libya" });
            countries.Add(new CountryCode { Code = "ME", Name = "Montenegro" });
            countries.Add(new CountryCode { Code = "NL", Name = "Netherlands" });
            countries.Add(new CountryCode { Code = "RS", Name = "Serbia" });

            return countries;

        }
        #endregion
    }
}
