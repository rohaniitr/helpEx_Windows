using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace PhoneApp1
{
    
    public static class NavigationExtensions
    {
        // Store parameters to be passed. 
        private static object _navigationData = null;


        /// <summary> 
        /// Set data. 
        /// </summary> 
        /// <param name="service">NavigationService</param> 
        /// <param name="page">Target page.</param> 
        /// <param name="data">Parameter data.</param> 
        public static void Navigate(this NavigationService service, string page, object data)
        {
            _navigationData = data;
            try
            {
                service.Navigate(new Uri(page, UriKind.Relative));
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary> 
        /// Get data. 
        /// </summary> 
        /// <param name="service">NavigationService</param> 
        /// <returns></returns> 
        public static object GetLastNavigationData(this NavigationService service)
        {
            object data = _navigationData;
            // Reset 
            _navigationData = null;
            return data;
        }
    } 
}
