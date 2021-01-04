using WorkoutAppService.Entities;
using WorkoutAppService.Models.QueryParameters;

namespace WorkoutAppService.Data.Repositories
{
    public class MuscleRepository : Repository<Muscle, CursorPaginationParameters>
    {
        public MuscleRepository(DataContext context) : base(context) { }
    }
}