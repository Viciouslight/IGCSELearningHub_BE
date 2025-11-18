using System.ComponentModel.DataAnnotations;

namespace IGCSELearningHub.Application.Identity.Devices.DTOs
{
    public class DeviceSyncRequest
    {
        [Required]
        public string DeviceToken { get; set; } = string.Empty;

        [Required]
        public string DeviceType { get; set; } = string.Empty;

        public string DeviceName { get; set; } = string.Empty;
        public string OsVersion { get; set; } = string.Empty;
        public string AppVersion { get; set; } = string.Empty;
    }
}
