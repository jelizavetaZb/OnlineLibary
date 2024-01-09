namespace OnlineLibary.Managers.Models
{
    public class ResponseResult
    {
        public List<(string Key, string Error)> Errors { get; set; }
        public int UpdatedId { get; set; }
        public bool IsSuccess { get; set; }
        public string SuccessText {  get; set; }

        public ResponseResult()
        {
            Errors = new List<(string Key, string Error)>();
        }

        public void AddError(string key, string error)
        {
            Errors.Add(new(key, error));
        }
    }
}
