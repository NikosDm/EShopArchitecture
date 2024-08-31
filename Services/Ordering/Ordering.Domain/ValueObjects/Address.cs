namespace Ordering.Domain.ValueObjects
{
    //In terms of comparing and removing, record is better than class
    public record Address
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string EmailAddress { get; }
        public string AddressLine { get; }
        public string Country { get; }
        public string State { get; }
        public string ZipCode { get; }

        protected Address() { }

        private Address(string firstName, string lastName, string emailAddress, string addressLine, string country, string state, string zipCode)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            AddressLine = addressLine;
            Country = country;
            State = state;
            ZipCode = zipCode;
        }

        public static Address Of(string firstName, string lastName, string emailAddress, string addressLine, string country, string state, string zipCode)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(emailAddress);
            ArgumentException.ThrowIfNullOrWhiteSpace(addressLine);

            return new Address(firstName, lastName, emailAddress, addressLine, country, state, zipCode);
        }
    }
}