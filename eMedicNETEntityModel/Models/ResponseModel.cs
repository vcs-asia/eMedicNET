namespace eMedicNETEntityModel.Models
{
    public class ResponseModel
    {
        public string Message { get; set; } = null!;
        public bool Flag { get; set; }
        public object? Data { get; set; }
        public string? Token { get; set; }
    }
}
