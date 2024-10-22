using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorClass.DTO
{
    public class ServiceResponseDto
    {
        public record class GeneralResponse(bool Flag, string Message);
        public record class LoginResponse(bool Flag,string Token, string Message);

    }
}
