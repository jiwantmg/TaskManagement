using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace TaskManagement.Domain.Exceptions
{
    public class ValidationException: Exception
    {
        public ValidationException(): base("One or more validation failures have occured")
        {
            Failures = new Dictionary<string, string[]>();
        }

        public ValidationException(List<ValidationFailure> failures): this()
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailure = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();
                
                Failures.Add(propertyName, propertyFailure);
            }
        }

        public IDictionary<string, string[]> Failures { get; set; }
    }
}