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
            loginPageObj.VerifyUserInHomePage();
            
        }
       

            // --- BEFORE SCENARIO HOOK ---

            [BeforeScenario(Order = 1)] // Run first to set up the state
            public void BeforeScenarioSetup()
            {
                // 1. Get the current feature's tags
                var featureTags = FeatureContext.Current.FeatureInfo.Tags;

                if (featureTags.Contains("Education"))
                {
                    // 2. Education Setup
                    var educationState = new EducationTestState();
                    // Use DI to register the state object
                    _container.RegisterInstanceAs(educationState);
                }
                else if (featureTags.Contains("Certifications"))
                {
                    // 2. Certifications Setup
                    var certificationState = new CertificationTestState();
                    _container.RegisterInstanceAs(certificationState);
                }
            else if (featureTags.Contains("ChangePassword"))
            {
                var changepasswordState = new ChangePasswordTestState();
                _container.RegisterInstanceAs(changepasswordState);
                
            }
            else if (featureTags.Contains("ProfileDescription"))
            {
                var profileDescriptionState = new ProfileDescriptionTestState();
                _container.RegisterInstanceAs(profileDescriptionState);
            }
            else if (featureTags.Contains("ManageListings"))
            {
                var manageListingsState = new ManageListingsTestState();
                _container.RegisterInstanceAs(manageListingsState);
            }
            else if (featureTags.Contains("ManageRequests"))
            {
                var manageRequestsState = new ManageRequestsTestState();
                _container.RegisterInstanceAs(manageRequestsState);
            }

                // You could also add common setup logic here (e.g., driver initialization)
            }

            // --- AFTER SCENARIO HOOK ---

            // Scoped to run only for scenarios tagged 'MultipleRecords' or 'SingleRecord'
            [AfterScenario("MultipleRecords", "SingleRecord", Order = 100)] // Run last for cleanup
            public void AfterRecordCleanup()
            {
                // 1. Get the current feature's tags
                var featureTags = FeatureContext.Current.FeatureInfo.Tags;

                if (featureTags.Contains("Education"))
                {
                    // 2. Education Cleanup
                    EducationPage educationPageObj = new EducationPage();
                    educationPageObj.DeleteAllEducationRecords();
                }
                else if (featureTags.Contains("Certifications"))
                {
                    // 2. Certification Cleanup
                    CertificationsPage certificationsPageObj = new CertificationsPage();
                    certificationsPageObj.DeleteAllCertificationsRecords();
                }
            
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
       