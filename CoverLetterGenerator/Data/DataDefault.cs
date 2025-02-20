using CoverLetterGenerator.Models;

namespace CoverLetterGenerator.Data
{
    public static class DataDefault
    {
        public const byte COLUMN_COUNT = 6;

        public static readonly Skill[] Skills =
        [
            new(".NET Core", true),
            new("ASP.NET", true),
            new("REST API", true),
            new("MS Azure SQL Database", false),
            new("NoSQL Redis", true),
            new("RabbitMQ", true),
            new("Docker", true),
            new("Dependency Injection", true),
            new("Unit testing", true),
            new("Git", true)
        ];

        public static readonly string[] Positions =
        [
            ".NET Developer",
            "Backend Developer"
        ];
    }
}