using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Xml.Linq;

namespace Pustok.Areas.Admin.ViewModels.Category;

public class CategoryAddViewModel
{
   
    [Required(ErrorMessage = "This field cannot be empty! Please enter name...")]
    [StringLength(30, MinimumLength = 5, ErrorMessage = "Name must be between 5 and 50 characters!!!")]
    [ValidateCategoryName(ErrorMessage = "The category name cannot contain characters like: !\"#$%&'()*+,-./:;<=>?@[^_`{|}~ ")]
    public string Name { get; set; }
    [Required(ErrorMessage = "This field cannot be empty! Please enter description...")]
    [StringLength(200, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 200 characters!!!")]
    public string CategoryDescription { get; set; }

    public class ValidateCategoryNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {

               
                
                
                    if (IsValidCategoryName(value.ToString()))
                    {
                        return new ValidationResult(ErrorMessage);
                    }
                
            }

            return ValidationResult.Success;
        }

        private bool IsValidCategoryName(string text)
        {
            string symbol = "!\"#$%&'()*+,-./:;<=>?@[^_`{|}~";
            char[] symbols = symbol.ToCharArray();
            foreach (var item in symbols)
            {
               
                if (text.Contains(item))
                {
                    return true;    
                }

            }
            return false;
        }
    }

}


