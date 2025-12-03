using AdvanceProjectMars_Task6.Pages;
using AdvanceProjectMars_Task6.State;
using AdvanceProjectMars_Task6.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll;
using Reqnroll.BoDi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceProjectMars_Task6.Hooks
{
    [Binding]
    public class Hooks : CommonDriver
    {
        private readonly IObjectContainer _container;
       


        public Hooks(IObjectContainer container)
        {
            _container = container;
            
        }
        [BeforeFeature()]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            IWebDriver driver = new ChromeDriver();
            CommonDriver.InitializeDriver(driver);
            LoginPage loginPageObj = new LoginPage();
            loginPageObj.LoginActions();

            
        }
       
        
       


        [AfterFeature()]
        public static void AfterFeature()
        {
           
            var driver = CommonDriver.Driver;
            if (driver != null)
            {
                driver.Quit();
            }
        }


    }


}
       