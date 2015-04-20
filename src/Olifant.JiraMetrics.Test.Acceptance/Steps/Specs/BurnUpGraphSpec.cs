namespace Olifant.JiraMetrics.Test.Acceptance.Steps.Specs
{
    public class BurnUpGraphSpec
    {
        public string StartX { get; set; }
        public string EndX { get; set; }
        public string StartY { get; set; }
        public string EndY { get; set; }

        public string[] Fields
        {
            get { return new[] {StartX, StartY, EndX, EndY}; }
        }
    }
}