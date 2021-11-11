using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaitynasLab.Data.Dtos
{
    public record SongDto(int Id, string Title);
    public record CreateSongDto(string Title);
    public record UpdateSongDto(string Title);
}
