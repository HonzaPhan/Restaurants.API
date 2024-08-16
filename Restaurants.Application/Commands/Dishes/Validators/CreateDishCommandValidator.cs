using FluentValidation;

namespace Restaurants.Application.Commands.Dishes.Validators
{
    public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
    {
        public CreateDishCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();

            RuleFor(x => x.Description).NotEmpty();

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price must be greater than or equal to 0");

            RuleFor(x => x.KiloCalories)
                .GreaterThanOrEqualTo(0)
                .WithMessage("KiloCalories must be greater than or equal to 0");
        }
    }
}
