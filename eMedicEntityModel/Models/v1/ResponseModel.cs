namespace eMedicEntityModel.Models.v1
{
    public class ResponseModel
    {
        public string Message { get; set; } = string.Empty;
        public bool Flag { get; set; }
        public object? Data { get; set; }
        public string? Token { get; set; }
    }
}
