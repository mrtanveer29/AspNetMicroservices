using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public ValidationException() : base("One or more validation failures have occurred.")
        {

            Errors = new Dictionary<string, string[]>();
        }
        public ValidationException(IEnumerable<ValidationFailure> failure)
        {
            Errors = failure.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(group => group.Key, group => group.ToArray());
        }
        public IDictionary<string, string[]> Errors { get; }
    }
 }
