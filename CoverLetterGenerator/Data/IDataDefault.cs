using CoverLetterGenerator.Models;
using System.Collections.Generic;

namespace CoverLetterGenerator.Data
{
    public interface IDataDefault
    {
        byte ColumnCount { get; }

        Skill[] Skills { get; }

        List<Position> Positions { get; }

        string GenerateCoverLetterText(string position, IEnumerable<string> skills, bool university);
    }
}