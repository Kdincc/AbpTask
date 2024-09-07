using SmartHall.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Domain.Common.ValueObjects
{
    public sealed class Cost : ValueObject
    {
        public decimal Value { get; private set; }

        private Cost(decimal value)
        {
            Value = value;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static Cost Create(decimal value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Cost cannot be negative");
            }

            return new Cost(value);
        }
    }
}
