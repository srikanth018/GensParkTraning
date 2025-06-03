using BankApp.Interfaces;
using BankApp.Misc;
using BankApp.Models;
using BankApp.Models.DTOs;
using BankApp.Repositories;

namespace BankApp.Services
{
    public class AccountHolderService : IAccountHolderService
    {
        private readonly IRepository<int, AccountHolder> _accountHolderRepository;
        private readonly IRepository<int, Address> _addressRepository;
        private readonly IRepository<int, AccountDetail> _accountDetailRepository;

        public AccountHolderService(IRepository<int, AccountHolder> accountHolderRepository,
            IRepository<int, Address> addressRepository,
            IRepository<int, AccountDetail> accountDetailRepository)
        {
            _accountHolderRepository = accountHolderRepository;
            _addressRepository = addressRepository;
            _accountDetailRepository = accountDetailRepository;
        }
        public async Task<AccountHolder> AddAccountHolderAsync(AddAccountHolderRequestDTO accountHolderDto)
        {
            if (accountHolderDto == null)
                throw new ArgumentNullException(nameof(accountHolderDto));

            if (accountHolderDto.Address == null || accountHolderDto.AccountDetail == null)
                throw new ArgumentException("Both Address and AccountDetail must be provided.");

            try
            {
                var newAddress = AccountHolderMapper.MapToAddress(accountHolderDto.Address);
                newAddress = await _addressRepository.AddAsync(newAddress);

                var newAccountDetail = AccountHolderMapper.MapToAccountDetail(accountHolderDto.AccountDetail);
                newAccountDetail = await _accountDetailRepository.AddAsync(newAccountDetail);

                var newAccountHolder = AccountHolderMapper.MapToAccountHolder(accountHolderDto);
                newAccountHolder.AddressId = newAddress.Id;
                newAccountHolder.AccountDetailId = newAccountDetail.Id;

                var result = await _accountHolderRepository.AddAsync(newAccountHolder);


                return result;
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while adding the account holder.", ex);
            }
        }

        public async Task<AccountHolder?> GetAccountHolderByIdAsync(int id)
        {
            var accountHolder = await _accountHolderRepository.GetByIdAsync(id);
            if (accountHolder == null)
            {
                return null;
            }

            if (accountHolder.AddressId != null)
            {
                accountHolder.Address = await _addressRepository.GetByIdAsync(accountHolder.AddressId);
            }
            if (accountHolder.AccountDetailId != null)
            {
                accountHolder.AccountDetail = await _accountDetailRepository.GetByIdAsync(accountHolder.AccountDetailId);
            }

            return accountHolder;
        }

        public Task<IEnumerable<AccountHolder>> GetAllAccountHoldersAsync()
        {
            throw new NotImplementedException();
        }
    }
}