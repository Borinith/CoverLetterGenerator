using CoverLetterGenerator.Data;
using System.Threading.Tasks;

namespace CoverLetterGenerator.Tests
{
    public class DataDefaultTests
    {
        private readonly DataDefault _data = new();

        [Test]
        public async Task Includes_Position_Name()
        {
            var result = _data.GenerateCoverLetterText("Software Engineer", [], false);

            await Assert.That(result).Contains("Software Engineer");
        }

        [Test]
        public async Task Lists_Selected_Skills()
        {
            var result = _data.GenerateCoverLetterText("Backend Developer", ["C#", "Docker"], false);

            await Assert.That(result).Contains("I have experience working with C#, Docker.");
        }

        [Test]
        public async Task Omits_Skills_Sentence_When_No_Skills_Are_Selected()
        {
            var result = _data.GenerateCoverLetterText("Backend Developer", [], false);

            await Assert.That(result).DoesNotContain("I have experience working with");
        }

        [Test]
        public async Task Mentions_Degree_When_University_Is_True()
        {
            var result = _data.GenerateCoverLetterText("Backend Developer", [], true);

            await Assert.That(result).Contains("graduated from university");
        }

        [Test]
        public async Task Omits_Degree_When_University_Is_False()
        {
            var result = _data.GenerateCoverLetterText("Backend Developer", [], false);

            await Assert.That(result).DoesNotContain("graduated from university");
        }
    }
}