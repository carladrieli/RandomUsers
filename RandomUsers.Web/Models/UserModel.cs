using System.Text.Json.Serialization;

namespace RandomUsers.Web.Models
{
    public class Name
    {    
        [JsonPropertyName("first")]
        public string First { get; set; }

        [JsonPropertyName("last")]
        public string Last { get; set; }
    }

    public class Login
    {
        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }        
    }

    public class UserModel
    {
        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("name")]
        public Name Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("login")]
        public Login Login { get; set; }

        [JsonPropertyName("cell")]
        public string Cell { get; set; }
    }

    public class ApiResponse
    {
        [JsonPropertyName("results")]
        public List<UserModel> Results { get; set; }
    }
}
