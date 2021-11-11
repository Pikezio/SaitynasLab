using System;

namespace SaitynasLab.Data.Dtos
{
    public record ConcertDto(int Id, string Title, DateTime Date);
    public record CreateConcertDto(string Title, DateTime Date);
    public record UpdateConcertDto(string Title, DateTime Date);
}
