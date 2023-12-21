﻿using static Common.Dtos.Interest;

namespace Common.Services
{
    public interface IInterestService
    {
        Task<IEnumerable<GetInterestDto>> GetInterestsAsync();
        Task<GetInterestDto> GetInterestByIdAsync(Guid Id);
        Task<CreateInterestDto> CreateInterestAsync(CreateInterestDto interestDto);
        Task<UpdateInterestDto> UpdateInterestAsync(Guid Id, UpdateInterestDto interest);
        Task<bool> DeleteInterestAsync(Guid Id);
    }
}