namespace OnlineLibary.Managers.Models
{
    public class ServerValidation
    {
        public List<(string key, string error)> ErrorMessages { get; set; }
        public bool IsValid { get; set; } = true;
    }
}
