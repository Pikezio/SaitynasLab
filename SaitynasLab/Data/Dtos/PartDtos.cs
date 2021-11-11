using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaitynasLab.Data.Dtos
{
    public record PartDto (int Id, string Instrument);
    public record CreatePartDto (string Instrument);
    public record UpdatePartDto (string Instrument);
}
