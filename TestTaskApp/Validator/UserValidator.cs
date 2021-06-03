using TestTaskApp.DTO;
using FluentValidation;
using TestTaskApp.Data;

namespace TestTaskApp.Validator
{
        public class UserValidator : AbstractValidator<RegisterDTO>
        {
            private readonly IUserRepository _userRepository ;

            public UserValidator(IUserRepository userRepository)
            {
            _userRepository = userRepository;

                RuleFor(x => x)
                   .Must(item => !_userRepository.ExistsByLogin(item.Login))
                   .WithMessage("Such login is already used");

            }
        }
    }
