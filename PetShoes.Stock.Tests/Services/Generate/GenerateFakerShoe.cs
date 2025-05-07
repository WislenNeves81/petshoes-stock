using Bogus;

namespace PetShoes.Stock.Tests.Services.Generate
{
    public class GenerateFakerShoe
    {
        private const string UtfType = "pt_BR";
        private const int IntOne = 1;
        private const int IntTen = 10;
        private const int Int30 = 30;
        private const int Int50 = 50;
        private const int Int100 = 100;

        public static Api.Core.Domain.Entities.Stock CreateShoeObject(Guid id)
        {
            var faker = new Faker<Api.Core.Domain.Entities.Stock>(UtfType)
                                .StrictMode(true)
                                .RuleFor(c => c.Id, id)
                                .RuleFor(c => c.Size, faker => faker.Random.Int(Int30, Int50))
                                .RuleFor(c => c.Quantity, faker => faker.Random.Int(IntOne, Int100))
                                .RuleFor(c => c.ProductId, id)
                                .RuleFor(c => c.Active, faker => faker.Random.Bool())
                                .RuleFor(c => c.CreatedAt, faker => faker.Date.Past())
                                .RuleFor(c => c.UpdatedAt, faker => faker.Date.Past());
            return faker.Generate();

        }
    }
}
