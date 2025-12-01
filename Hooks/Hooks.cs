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
        [BeforeScenario()]
        public void BeforeScenario()
        {
            var educationState = new EducationState();
            ScenarioContext.Current.Set(educationState, "EducationState");
        }
        [AfterScenario("MultipleRecords")]
        public void AfterMultipleRecordsScenario()
        {
            EducationPage educationPageObj = new EducationPage();
            educationPageObj.DeleteAllEducationRecords();
        }

        [AfterScenario("SingleRecord")]
        public void AfterSingleRecordScenario()
        {
            EducationPage educationPageObj = new EducationPage();
            educationPageObj.DeleteAllEducationRecords();
        }

        //[BeforeScenario(Order = 1)]
        //public void FirstBeforeScenario()
        //{
        //    IWebDriver driver = new ChromeDriver();

        //    _container.RegisterInstanceAs<IWebDriver>(driver);
        //    CommonDriver.InitializeDriver(driver);
        //    LoginPage loginPageObj = new LoginPage();
        //    loginPageObj.LoginActions();
        //}


        //[AfterScenario()]
        //public void CleanUp(FeatureContext featureContext)
        //{
        //    if (featureContext.FeatureInfo.Tags.Contains(""))
        //    {
        //        EducationPage educationPageObj = new EducationPage();
        //        educationPageObj.DeleteAllEducationRecords();
        //    }


        //}

        [AfterFeature()]
        public static void AfterFeature()
        {
            //if (featureContext.FeatureInfo.Tags.Contains("EducationDataDriven"))
            //{
            //    EducationPage educationPageObj = new EducationPage();
            //    educationPageObj.DeleteAllEducationRecords();
            //}
            var driver = CommonDriver.Driver;
            if (driver != null)
            {
                driver.Quit();
            }
        }


    }


}
       