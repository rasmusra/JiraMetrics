using System.Web.Mvc;

using StructureMap;

namespace Olifant.JiraMetrics.Web
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            ControllerBuilder.Current
                .SetControllerFactory(new StructureMapControllerFactory());

            ObjectFactory.Initialize(x => x.AddConfigurationFromXmlFile("StructureMap.xml"));
        }
    }
}