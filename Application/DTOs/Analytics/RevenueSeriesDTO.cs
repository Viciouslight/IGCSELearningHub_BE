namespace IGCSELearningHub.Application.DTOs.Analytics
{
    public class RevenueSeriesDTO
    {
        public string SeriesName { get; set; } = "Revenue";
        public List<TimePointDTO> Points { get; set; } = new();
    }
}
