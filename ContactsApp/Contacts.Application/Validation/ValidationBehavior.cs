﻿using FluentValidation;
using MediatR;

namespace Contacts.Application.Validation;

public class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest> validator) :
    IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        return await next();
    }
}
