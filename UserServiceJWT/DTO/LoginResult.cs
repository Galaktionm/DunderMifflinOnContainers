namespace UserServiceJWT.DTO
{
    public class LoginResult
    {

        public bool Success { get; set; }

        public string Message { get; set; }

        public string? Token { get; set; }

        public string UserId { get; set; }


    }
}