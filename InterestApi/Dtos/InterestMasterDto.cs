namespace InterestMasterApi.Dtos
{
    public class InterestMasterDto
    {
        public record GetInterestDto(Guid Id, string InterestType, int InterestRate);

    }
}
