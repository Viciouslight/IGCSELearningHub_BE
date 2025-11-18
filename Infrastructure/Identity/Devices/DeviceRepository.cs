using IGCSELearningHub.Application.IRepository;
using IGCSELearningHub.Application.Utils.Interfaces;
using IGCSELearningHub.Domain.Identity.Devices;
using IGCSELearningHub.Infrastructure.Repository;
using Infrastructure.Data;

namespace IGCSELearningHub.Infrastructure.Identity.Devices
{
    public class DeviceRepository : GenericRepository<Device>, IDeviceRepository
    {
        public DeviceRepository(AppDbContext dbContext, IDateTimeProvider clock) : base(dbContext, clock)
        {
        }
    }
}
