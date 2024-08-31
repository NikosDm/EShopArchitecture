using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Application.DTOs
{
    public record PaymentDTO(string CardName, string CardNumber, string Expiration, string Cvv, int PaymentMethod);
}