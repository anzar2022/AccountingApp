﻿namespace TransactionApi.Dtos
{
    public class InterestEMIDto
    {
        public record GetInterestEMIDto(Guid Id, Guid TransactionId, double PrincipalAmount, double InterestRate, double InterestAmount);

    }
}
