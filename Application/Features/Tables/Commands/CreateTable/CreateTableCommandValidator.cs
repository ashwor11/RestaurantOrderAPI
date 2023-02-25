using FluentValidation;

namespace Application.Features.Tables.Commands.CreateTable;

public class CreateTableCommandValidator : AbstractValidator<CreateTableCommand>
{
    public CreateTableCommandValidator()
    {
        RuleFor(x => x.OwnerId).NotEmpty().WithMessage("You must be logged in for this operation.");
        RuleFor(x => x.CreateTableDto).NotEmpty();
    }
}