using System;

namespace TaskManager.Infrastructure.Shared.Interfaces.Shared
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
    }
}
