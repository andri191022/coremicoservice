namespace Mango.Services.ProductAPI.Model.Dto
{
    public class ResponseDto
    {
        public object? Result { get; set; }
        public bool IsSuccess { get; set; }
        public string Messages { get; set; } = "";
        
    }
}
