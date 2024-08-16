using FluentValidation;

namespace Restaurants.Application.Commands.Restaurants.Validators
{
    public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
    {
        public UpdateRestaurantCommandValidator()
        {
            RuleFor(dto => dto.Name)
                .NotEmpty()
                .Length(3, 100);

            RuleFor(dto => dto.Description)
                .NotEmpty().WithMessage("Description is required");

            RuleFor(dto => dto.HasDelivery)
                .NotNull();
        }
    }
}
