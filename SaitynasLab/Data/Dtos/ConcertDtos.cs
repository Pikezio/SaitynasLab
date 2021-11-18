using Microsoft.AspNetCore.Identity;
using SaitynasLab.Data.Entities;
using System;
using System.Collections.Generic;

namespace SaitynasLab.Data.Dtos
{
    public record ConcertDto(int Id, string Title, DateTime Date, string UserId );
    public record CreateConcertDto(string Title, DateTime Date);
    public record UpdateConcertDto(string Title, DateTime Date);
}
