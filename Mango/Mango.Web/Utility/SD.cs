namespace Mango.Web.Utility
{
    public class SD
    {
        public static string? AuthAPIBase { get; set; }  
        public static string? CouponAPIBase { get; set; }
<<<<<<< HEAD
        public static string? AuthAPIBase { get; set; }
        public const string RoleAdmin = "ADMIN";
        public const string RoleCustomer = "CUSTOMER";
        public const string TokenCookie = "JWTToken";
=======
>>>>>>> f66599d0f9c8502313793b58b26a28974a92a2b0
        public enum ApiType
        {
            GET, POST, PUT, DELETE
        }
    }
}
