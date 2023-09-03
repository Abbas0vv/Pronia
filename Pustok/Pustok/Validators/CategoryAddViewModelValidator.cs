using Pustok.Areas.Admin.ViewModels.Category;
using FluentValidation;

public class CategoryAddViewModelValidator : FluentValidation.AbstractValidator<CategoryAddViewModel>
{
    public CategoryAddViewModelValidator()
    {
        RuleFor(model => model.Name)
            .NotEmpty().WithMessage("Category Name is required.")
            .MaximumLength(30).WithMessage("Category Name cannot exceed 30 characters.")
            .Must(BeValidCategoryName).WithMessage("Category Name cannot contain special characters.");

        RuleFor(model => model.CategoryDescription)
            .NotEmpty().WithMessage("Category Description is required.")
            .MaximumLength(50).WithMessage("Category Description cannot exceed 50 characters.");
    }

    private bool BeValidCategoryName(string name)
    {
        string symbol = "!\"#$%&'()*+,-./:;<=>?@[^_`{|}~";
        char[] symbols = symbol.ToCharArray();
        foreach (var item in symbols)
        {
          return !name.Contains(item);

        }
        return true;
    }
}
