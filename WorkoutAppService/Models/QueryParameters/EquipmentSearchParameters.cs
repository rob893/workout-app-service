using System.Collections.Generic;

namespace WorkoutAppService.Models.QueryParameters
{
    public record EquipmentSearchParameters : CursorPaginationParameters
    {
        public List<int> ExerciseIds { get; init; } = new List<int>();
    }
}