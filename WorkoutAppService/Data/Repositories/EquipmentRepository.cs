using System.Linq;
using WorkoutAppService.Entities;
using WorkoutAppService.Models.QueryParameters;

namespace WorkoutAppService.Data.Repositories
{
    public class EquipmentRepository : Repository<Equipment, EquipmentSearchParameters>
    {
        public EquipmentRepository(DataContext context) : base(context) { }

        protected override IQueryable<Equipment> AddWhereClauses(IQueryable<Equipment> query, EquipmentSearchParameters searchParams)
        {
            if (searchParams != null && searchParams.ExerciseIds != null && searchParams.ExerciseIds.Count > 0)
            {
                query = query.Where(equipment => equipment.Exercises.Any(exercise => searchParams.ExerciseIds.Contains(exercise.Id)));
            }

            return query;
        }
    }
}