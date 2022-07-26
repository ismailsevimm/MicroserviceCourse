using MSCourse.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;

namespace MSCourse.Services.Order.Domain.OrderAggregate
{
    public class Address : ValueObject
    {
        public Address(string province, string district, string street, string zipCode, string line)
        {
            Province = province;
            District = district;
            Street = street;
            ZipCode = SetZipCode(zipCode);
            Line = line;
        }

        public string SetZipCode(string zipCode)
        {
            if (zipCode.Length > 6)
            {
                throw new ArgumentOutOfRangeException(nameof(zipCode));
            }

            return zipCode;
        }

        public string Province { get; private set; }
        public string District { get; private set; }
        public string Street { get; private set; }
        public string ZipCode { get; private set; }
        public string Line { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Province;
            yield return District;
            yield return Street;
            yield return ZipCode;
            yield return Line;
        }
    }
}
