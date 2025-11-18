namespace IGCSELearningHub.Application.DTOs.Analytics
{
    public class TopLivestreamRevenueItemDTO
    {
        public int LivestreamId { get; set; }
        public string Title { get; set; } = null!;
        public decimal Revenue { get; set; }
        public int Orders { get; set; }
    }
}
