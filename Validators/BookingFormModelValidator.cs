using BeautySalonApp.ViewModels;
using FluentValidation;

namespace BeautySalonApp.Validators;

public class BookingFormModelValidator : AbstractValidator<BookingFormModel>
{
    public BookingFormModelValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Введите ФИО клиента.")
            .MinimumLength(3).WithMessage("ФИО должно содержать не менее 3 символов.")
            .MaximumLength(120).WithMessage("ФИО слишком длинное.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Введите email.")
            .EmailAddress().WithMessage("Введите корректный email.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Введите телефон.")
            .MinimumLength(5).WithMessage("Телефон слишком короткий.");

        RuleFor(x => x.MasterId)
            .GreaterThan(0).WithMessage("Выберите мастера.");

        RuleFor(x => x.SelectedServiceIds)
            .NotEmpty().WithMessage("Выберите хотя бы одну услугу.");

        RuleFor(x => x.AppointmentDate.Date)
            .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Дата записи не может быть в прошлом.");

        RuleFor(x => x.AppointmentTime)
            .Must(x => x >= new TimeSpan(9, 0, 0) && x <= new TimeSpan(20, 0, 0))
            .WithMessage("Время записи должно быть в пределах рабочего дня с 09:00 до 20:00.");
    }
}
