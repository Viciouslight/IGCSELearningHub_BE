using IGCSELearningHub.Application.Identity.Devices.DTOs;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Identity.Devices.Interfaces
{
    public interface IDeviceService
    {
        Task<ApiResult<DeviceDTO>> SyncAsync(int accountId, DeviceSyncRequest request, CancellationToken ct = default);
        Task<ApiResult<bool>> UnregisterAsync(int accountId, DeviceUnregisterRequest request, CancellationToken ct = default);
        Task<ApiResult<IEnumerable<DeviceDTO>>> GetMyDevicesAsync(int accountId);
        Task<IReadOnlyList<string>> GetActiveDeviceTokensAsync(int accountId, CancellationToken ct = default);
    }
}
