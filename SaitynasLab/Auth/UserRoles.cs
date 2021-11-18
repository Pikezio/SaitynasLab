using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaitynasLab.Auth
{
    public class UserRoles
    {
        public const string Admin = nameof(Admin);
        // ● Peržiūrėti registruotų naudotojų sąrašą
        // ● Ištrinti naudotojus

        public const string Musician = nameof(Musician);
        // ● Peržiūrėti koncertus
        // ● Peržiūrėti kūrinius
        //   ○ Peržiūrėti kūrinio partijas
        //     ■ Peržiūrėti specifinę partiją

        public const string Creator = nameof(Creator);
        //  ● Pridėti kūrinį
        //  ● Ištrinti kūrinį
        //  ● Atnaujinti kūrinį
        //  ● Peržiūrėti kūrinių sąrašą
        //      ○ Peržiūrėti kūrinį
        //      ○ Peržiūrėti partijų sąrašą
        //          ■ Peržiūrėti partiją
        //          ■ Pridėti partiją
        //          ■ Ištrinti partiją
        //          ■ Atnaujinti partiją
        //          ■ Spausdinti, parsisiųsti partijas
        //          ■ Priskirti muzikantams partijas
        //  ● Pridėti koncertą
        //  ● Ištrinti koncertą
        //  ● Redaguoti koncertą
        //  ● Peržiūrėti koncertų sąrašą
        //      ○ Peržiūrėti koncertą

        public static readonly IReadOnlyCollection<string> All = new[] { Admin, Musician, Creator };
    }
}
