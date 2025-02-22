using CoverLetterGenerator.Models;
using System.Collections.Generic;
using System.Text;

namespace CoverLetterGenerator.Data
{
    public class DataDefault : IDataDefault
    {
        public byte ColumnCount => 6;

        public Skill[] Skills =>
        [
            new("C#", false),
            new(".NET Core", true),
            new("ASP.NET", true),
            new("MVC", false),
            new("REST API", true),
            new("Swagger", false),
            new("MS SQL Server", true),
            new("MS Azure SQL Database", false),
            new("PostgreSQL", false),
            new("NoSQL Redis", true),
            new("RabbitMQ", true),
            new("Docker", true),
            new("WSDL", false),
            new("XML", false),
            new("JSON", false),
            new("Dependency Injection", true),
            new("Unit testing", true),
            new("Git", true)
        ];

        public List<Position> Positions =>
        [
            new(".NET Developer"),
            new("Backend Developer"),
            new("Software Engineer"),
            new(".NET WPF Developer"),
            new("C# Developer"),
            new("Backend Engineer"),
            new("Software Developer")
        ];

        public string GenerateCoverLetterText(string position, IEnumerable<string> skills, bool university)
        {
            var text = new StringBuilder();

            text.AppendLine("Hello!");
            text.AppendLine($"I am a {position} with 4 years of experience. Proficient in designing and developing applications and databases, as well as having an experience in troubleshooting issues using C#.");
            text.Append("I have experience working with ");
            text.Append(string.Join(", ", skills));
            text.AppendLine(".");
            text.AppendLine("I worked according to the scrum methodology.");

            if (university)
            {
                text.AppendLine("Finally, I graduated from university, and I have a bachelor's degree and a master's degree.");
            }

            text.AppendLine("I will be glad to hear your answer!");
            text.Append("Thank you");

            return text.ToString();
        }
    }
}