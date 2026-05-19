using FluentValidation;

namespace FinTasker.Application.Features.Projects.Queries.GetByIdProject
{
    public class GetByIdProjectValidator : AbstractValidator<GetByIdProjectQuery>
    {
        public GetByIdProjectValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id cannot be empty.");
        }
    }
}

