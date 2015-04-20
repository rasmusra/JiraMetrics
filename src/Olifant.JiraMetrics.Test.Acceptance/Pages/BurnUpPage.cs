using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using Olifant.JiraMetrics.Test.Utilities.Helpers;
using OpenQA.Selenium.Support.UI;

namespace Olifant.JiraMetrics.Test.Acceptance.Pages
{
    public class BurnUpPage 
    {
        public BurnUpPage(PhantomJsWrapper phantomWrapper)
        {
            PhantomWrapper = phantomWrapper;
        }

        public PhantomJsWrapper PhantomWrapper { get; private set; }

        public void NavigateTo()
        {
            var url = String.Format("http://localhost:{0}", IisExpressManager.Port);
           PhantomWrapper.Driver.Navigate().GoToUrl(url);
        }

        public bool ChartDivContains(string expectedText)
        {
            return PhantomWrapper.WaitForRendering(() =>
            {
                var chartDiv = FeatureWrapper.JQuery.Find("#chartDiv");
                return chartDiv.Text().Contains(expectedText);
            });
        }

        public bool ChartContainerContains(IEnumerable<string> expectedTexts)
        {
            return ChartContainerContains(expectedTexts.ToArray());
        }

        public bool ChartContainerContains(params string[] expectedTexts)
        {
            return PhantomWrapper.WaitForRendering(() =>
            {
                var chartContainer = FeatureWrapper.JQuery.Find("#chart_container");
                return expectedTexts.All(text => chartContainer.Text().Contains(text));
            });
        }

        public void SearchForProject(string project)
        {
            var projectDropdown = FeatureWrapper.PhantomJsDriver
                .FindElementById("ProjectList");

            new SelectElement(projectDropdown)
                .SelectByText(project);

            FeatureWrapper.JQuery.Find("#Search")
                .Click();
        }

        public string ProjectDropdown
        {
            get
            {
                return FeatureWrapper.JQuery.Find("#ProjectList").Text();
            }
        }
    }
}