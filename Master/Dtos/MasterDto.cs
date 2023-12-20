namespace Master.Dtos
{
    public class MasterDto
    {
        public record GetInterestDto(Guid Id, string InterestType, int InterestRate);
        public record CreateInterestDto(Guid Id, string InterestType, int InterestRate);
        public record UpdateInterestDto( string? InterestType, int InterestRate);
    }
}
