namespace WorkoutAppService.Models.QueryParameters
{
    public record GymSearchParameters : CursorPaginationParameters
    {
        public int? UserId { get; set; }
    }
}