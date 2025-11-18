namespace IGCSELearningHub.Application.DTOs.Analytics
{
    public class CountSeriesDTO
    {
        public string SeriesName { get; set; } = "Count";
        public List<CountPointDTO> Points { get; set; } = new();
    }
}
