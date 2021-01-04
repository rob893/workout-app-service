using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WorkoutAppService.Entities
{
    public class LinkedAccount : IIdentifiable<string>, IOwnedByUser<int>
    {
        public string Id { get; set; } = default!;
        [JsonConverter(typeof(StringEnumConverter))]
        public LinkedAccountType LinkedAccountType { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = default!;
    }
}