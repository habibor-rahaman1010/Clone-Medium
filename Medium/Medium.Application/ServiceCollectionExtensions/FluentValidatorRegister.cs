using FluentValidation;
using FluentValidation.AspNetCore;
using Medium.Application.CommandValidators;
using Medium.Application.Features.Categories.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Medium.Application.ServiceCollectionExtensions
{
    public static class FluentValidatorRegister
    {
        public static IServiceCollection AddFluentValidatorRegister(this IServiceCollection services)
        {
            //Autometic process
            //services.AddValidatorsFromAssemblyContaining<CountryValidator>();
            //services.AddValidatorsFromAssemblyContaining<CountryUpdateValidator>();

            /*//Menual Process
            services.AddScoped<IValidator<CountryDto>, CountryValidator>();
            services.AddScoped<IValidator<UpdateDto>, CountryUpdateValidator>();*/

            //services.AddValidatorsFromAssembly(typeof(CreateCategoryCommandValidator).Assembly);

            services.AddValidatorsFromAssemblyContaining<CreateCategoryCommandValidator>();
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            return services;
        }
    }
}
