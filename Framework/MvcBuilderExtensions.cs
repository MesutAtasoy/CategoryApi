using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Framework;

public static class MvcBuilderExtensions
{
    public static IMvcBuilder AddCustomFluentValidation(this  IMvcBuilder builder, Assembly assembly)
    {
        builder.AddFluentValidation(options =>
        {
            // Validate child properties and root collection elements
            options.ImplicitlyValidateChildProperties = true;
            options.ImplicitlyValidateRootCollectionElements = true;

            // Automatic registration of validators in assembly
            options.RegisterValidatorsFromAssembly(assembly);
        });

        return builder;
    }
}