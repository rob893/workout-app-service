using System;

namespace WorkoutAppService.Entities
{
    public interface IOwnedByUser<TKey> where TKey : IEquatable<TKey>, IComparable<TKey>
    {
        TKey UserId { get; }
    }
}