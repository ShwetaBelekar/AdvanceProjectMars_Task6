using AdvanceProjectMars_Task6.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceProjectMars_Task6.Pages
{
    public class CertificationsPage : CommonDriver
    {
        private IWebElement addNewButton => Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[5]/div[1]/div[2]/div/table/thead/tr/th[4]/div"));
        private IWebElement certificationOrAwardTextbox => Driver.FindElement(By.XPath("//input[@placeholder='Certificate or Award']"));

        private IWebElement certifiedFromTextbox => Driver.FindElement(By.XPath("//input[@name='certificationFrom']"));
        private IWebElement yearDropdownButton => Driver.FindElement(By.XPath("//select[@name='certificationYear']"));
        private IWebElement addButton => Driver.FindElement(By.XPath("//input[@value='Add']"));

        private IWebElement cancelButton => Driver.FindElement(By.XPath("//input[@value='Cancel']"));

        private IWebElement editButton => Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[5]/div[1]/div[2]/div/table/tbody[last()]/tr/td[4]/span[1]/i"));

        private IWebElement updateButton => Driver.FindElement(By.XPath("//input[@value='Update']"));

        private IWebElement deleteButton => Driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[5]/div[1]/div[2]/div/table/tbody[last()]/tr/td[4]/span[2]/i"));
    }
}
