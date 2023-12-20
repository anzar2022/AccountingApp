namespace Interest.Dtos
{
    public class InterestDto
    {
        public record GetInterestDto(Guid Id, string InterestType, int InterestRate);
        public record CreateInterestDto(Guid Id, string InterestType, int InterestRate);
        public record UpdateInterestDto(string? InterestType, int InterestRate);
    }
}
