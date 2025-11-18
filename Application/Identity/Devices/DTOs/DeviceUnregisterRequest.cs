using System.ComponentModel.DataAnnotations;

namespace IGCSELearningHub.Application.Identity.Devices.DTOs
{
    public class DeviceUnregisterRequest
    {
        [Required]
        public string DeviceToken { get; set; } = string.Empty;
    }
}
