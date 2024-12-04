namespace BlazorAuthAPI.DTO
{
    public class ServiceResponseDto
    {
        //public record class GeneralResponse(bool Flag, string Message);
        //public record class LoginResponse(bool Flag, string Token, string Message);


        public class GeneralResponse
        {
            public bool Flag { get; set; }
            public string Message { get; set; }

            public GeneralResponse(bool flag, string message)
            {
                Flag = flag;
                Message = message;
            }
        }

        public class LoginResponse 
        {
            public bool Flag { get; set; }
            public string Message { get; set; }
            public string Token { get; set; }

            public LoginResponse(bool flag, string token, string message)
                
            {
                Flag = flag;
                Message = message;
                Token = token;
            }
        }
    }
}
