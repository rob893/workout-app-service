using System.Linq;
using WorkoutAppService.Entities;
using WorkoutAppService.Models.QueryParameters;

namespace WorkoutAppService.Data.Repositories
{
    public class ExerciseCategoryRepository : Repository<ExerciseCategory, ExerciseCategorySearchParameters>
    {
        public ExerciseCategoryRepository(DataContext context) : base(context) { }

        protected override IQueryable<ExerciseCategory> AddWhereClauses(IQueryable<ExerciseCategory> query, ExerciseCategorySearchParameters searchParams)
        {
            if (searchParams.ExerciseId != null && searchParams.ExerciseId.Count > 0)
            {
                query = query.Where(c => c.Exercises.Any(e => searchParams.ExerciseId.Contains(e.Id)));
            }

            return query;
        }
    }
}