using System;

namespace Flurl.Api.Dto
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}