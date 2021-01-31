using System.Collections.Generic;

namespace WorkoutAppService.Models.QueryParameters
{
    public record ExerciseSearchParameters : CursorPaginationParameters
    {
        public List<int> ExerciseCategoryId { get; init; } = new List<int>();
        public List<int> EquipmentId { get; init; } = new List<int>();
    }
}