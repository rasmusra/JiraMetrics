namespace HM.JiraMetrics.Lib.Metrics.BurnUpGraph
{
    public class BurnUpValue
    {
        private readonly decimal storyPoint;

        public BurnUpValue(decimal storyPoint)
        {
            this.storyPoint = storyPoint;
        }

        public decimal StoryPoints
        {
            get
            {
                return this.storyPoint;
            }
        }
    }
}