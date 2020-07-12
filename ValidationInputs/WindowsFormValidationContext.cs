using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationInputs
{
    public class WindowsFormValidationContext
    {

        public static Tuple<bool,List<ValidationResult>> Validated<T>(T entity)
        {
            ValidationContext context = new ValidationContext(entity,null,null);
            List<ValidationResult> errors = new List<ValidationResult>();
            if (!Validator.TryValidateObject(entity, context, errors, true))
                return new Tuple<bool, List<ValidationResult>>(false, errors);
            else
                return new Tuple<bool, List<ValidationResult>>(true, null);
           
        }
    }
}
