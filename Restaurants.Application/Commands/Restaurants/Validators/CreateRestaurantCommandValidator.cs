using FluentValidation;

namespace Restaurants.Application.Commands.Restaurants.Validators
{
    public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
    {
        private readonly List<string> validCategories = ["Fast Food", "Traditional", "Vegetarian", "Vegan", "Asian", "Italian", "Mexican", "American", "Other"];
        public CreateRestaurantCommandValidator()
        {
            RuleFor(dto => dto.Name)
                .NotEmpty()
                .Length(3, 100);

            RuleFor(dto => dto.Description)
                .NotEmpty().WithMessage("Description is required");

            RuleFor(dto => dto.Category)
                .Must(validCategories.Contains)
                .WithMessage("Invalid category. Please choose from the valid categories.");

            RuleFor(dto => dto.ContactEmail)
                .EmailAddress().WithMessage("Please provide a valid email address");

            RuleFor(dto => dto.ContactPhone)
                .Matches(@"^(\+420)?\s?[0-9]{3}\s?[0-9]{3}\s?[0-9]{3}$")
                .WithMessage("Please provide a valid phone number in the format '+420123456789'");

            RuleFor(dto => dto.ZipCode)
                .Matches(@"^\d{5}$")
                .WithMessage("Please provide a valid zip code in the format '12345'");
        }
    }
}
