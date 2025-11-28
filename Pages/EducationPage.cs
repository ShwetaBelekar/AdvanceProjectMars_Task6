using AdvanceProjectMars_Task6.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceProjectMars_Task6.Pages
{
    public class EducationPage : CommonDriver
    {
        private IWebElement AddNewButton => Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/thead/tr/th[6]/div"));

        private IWebElement CollegeUniversityNameTextbox => Driver.FindElement(By.XPath("//input[@placeholder='College/University Name']"));

        private IWebElement CountryOfCollegeUniversityDropdownbox => Driver.FindElement(By.XPath("//select[@name='country']"));

        private IWebElement TitleDropdownbox => Driver.FindElement(By.XPath("//select[@name='title']"));

        private IWebElement DegreeTextbox => Driver.FindElement(By.XPath("//input[@placeholder=\"Degree\"]"));

        private IWebElement YearOfGraduationDropdownbox => Driver.FindElement(By.XPath("//select[@name='yearOfGraduation']"));

        private IWebElement AddButton => Driver.FindElement(By.XPath("//input[@value='Add']"));

        private IWebElement EditButton => Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody[last()]/tr/td[6]/span[1]/i"));

        private IWebElement UpdateButton => Driver.FindElement(By.XPath("//input[@value='Update']"));

        private IWebElement CancelButton => Driver.FindElement(By.XPath("//input[@value='Cancel']"));

        private IWebElement DeleteButton => Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody[last()]/tr/td[6]/span[2]/i"));
        private IList<IWebElement> DeleteButtons => Driver.FindElements(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody/tr/td[6]/span[2]/i"));
        public void CreateEducationRecord(string CollegeUniversityName, string CountryofCollegeUniversity, string Title, string Degree, string YearofGraduation)
        {
            Thread.Sleep(5000);
            AddNewButton.Click();
            CollegeUniversityNameTextbox.SendKeys(CollegeUniversityName);
            CountryOfCollegeUniversityDropdownbox.SendKeys(CountryofCollegeUniversity);
            TitleDropdownbox.SendKeys(Title);
            DegreeTextbox.SendKeys(Degree);
            YearOfGraduationDropdownbox.SendKeys(YearofGraduation);
            AddButton.Click();
            Thread.Sleep(5000);
        }
        public void DeleteAllEducationRecords()
        {
            HomeToEducationPage homeToEducationPageObj = new HomeToEducationPage();
            homeToEducationPageObj.NavigateToEducation();

            var deleteButtons = Driver.FindElements(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody/tr/td[6]/span[2]/i"));
            if (deleteButtons.Count > 0)
            {
                for (int i = deleteButtons.Count - 1; i >= 0; i--)
                {
                    deleteButtons[i].Click();
                    Thread.Sleep(2000); // Add a wait to ensure the delete operation is complete
                }
            }
        }
    }
     
    }
