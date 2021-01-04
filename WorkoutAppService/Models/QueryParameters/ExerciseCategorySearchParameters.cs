using System.Collections.Generic;

namespace WorkoutAppService.Models.QueryParameters
{
    public record ExerciseCategorySearchParameters : CursorPaginationParameters
    {
        public List<int> ExerciseId { get; init; } = new List<int>();
    }
}