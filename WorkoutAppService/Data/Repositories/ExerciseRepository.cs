using System.Linq;
using Microsoft.EntityFrameworkCore;
using WorkoutAppService.Entities;
using WorkoutAppService.Models.QueryParameters;

namespace WorkoutAppService.Data.Repositories
{
    public class ExerciseRepository : Repository<Exercise, ExerciseSearchParameters>
    {
        public ExerciseRepository(DataContext context) : base(context) { }

        protected override IQueryable<Exercise> AddIncludes(IQueryable<Exercise> query)
        {
            return query.Include(e => e.ExerciseSteps);
        }

        protected override IQueryable<Exercise> AddWhereClauses(IQueryable<Exercise> query, ExerciseSearchParameters searchParams)
        {
            if (searchParams.EquipmentId != null && searchParams.EquipmentId.Count > 0)
            {
                query = query.Where(e => e.Equipment.Any(eq => searchParams.EquipmentId.Contains(eq.Id)));
            }

            if (searchParams.ExerciseCategoryId != null && searchParams.ExerciseCategoryId.Count > 0)
            {
                query = query.Where(e => e.ExerciseCategorys.Any(ec => searchParams.ExerciseCategoryId.Contains(ec.Id)));
            }

            if (searchParams.PrimaryMuscleId != null && searchParams.PrimaryMuscleId.Count > 0)
            {
                query = query.Where(e => e.PrimaryMuscle != null && searchParams.PrimaryMuscleId.Contains(e.PrimaryMuscle.Id));
            }

            if (searchParams.SecondaryMuscleId != null && searchParams.SecondaryMuscleId.Count > 0)
            {
                query = query.Where(e => e.SecondaryMuscle != null && searchParams.SecondaryMuscleId.Contains(e.SecondaryMuscle.Id));
            }

            return query;
        }
    }
}