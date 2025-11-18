namespace IGCSELearningHub.Application.DTOs.Analytics
{
    public class TimePointDTO
    {
        public int Year { get; set; }
        public int Month { get; set; }   // 1..12
        public int? Day { get; set; }    // null khi GroupBy=Month
        public decimal Value { get; set; }
    }
}
