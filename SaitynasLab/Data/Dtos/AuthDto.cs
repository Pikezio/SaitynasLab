using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaitynasLab.Data.Dtos
{
    public record RegisterUserDto([Required] string Username, [EmailAddress][Required] string Email, [Required]string Password);
    public record LoginUserDto([Required] string Username, [Required]string Password);
    public record UserDto(string Id, string Username, string Email);
    public record SuccessfulLoginDto(string AccessToken);

}
