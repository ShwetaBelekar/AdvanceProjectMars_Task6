using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceProjectMars_Task6.Utilities
{
    public class CommonDriver
    {
        private static readonly ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();

        public static IWebDriver Driver => driver.Value;

        public static void InitializeDriver(IWebDriver webDriver)
        {
            driver.Value = webDriver;
            driver.Value.Manage().Window.Maximize();
        }
    }
}
