using System;
using TaskManager.Infrastructure.Shared.Interfaces.Shared;

namespace TaskManager.Infrastructure.Shared.Services
{
    public class SystemDateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
