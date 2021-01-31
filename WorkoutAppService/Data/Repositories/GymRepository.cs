using System.Linq;
using WorkoutAppService.Entities;
using WorkoutAppService.Models.QueryParameters;

namespace WorkoutAppService.Data.Repositories
{
    public class GymRepository : Repository<Gym, GymSearchParameters>
    {
        public GymRepository(DataContext context) : base(context) { }

        protected override IQueryable<Gym> AddWhereClauses(IQueryable<Gym> query, GymSearchParameters searchParams)
        {
            if (searchParams.UserId != null)
            {
                query = query.Where(gym => gym.UserId == searchParams.UserId);
            }

            return query;
        }
    }
}