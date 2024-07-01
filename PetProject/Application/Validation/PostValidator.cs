using Domain.Models;
using FluentValidation;

namespace Application.Validation;

public class PostValidator : AbstractValidator<Post>
{
    public PostValidator()
    {
        RuleFor(customer => customer.PostName)
            .NotNull().MinimumLength(1).MaximumLength(20);
        RuleFor(customer => customer.PostDescription)
            .MinimumLength(10).MaximumLength(200);

    }
}
