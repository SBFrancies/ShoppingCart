using System;
using System.Collections.Generic;
using System.Text;
using ShoppingCartClient.Client.ApiClient.Models;

namespace ShoppingCartClient.Client.Exceptions
{
    public class ProblemDetailsException : Exception
    {
        public ProblemDetails ProblemDetails { get; }

        public ProblemDetailsException(ProblemDetails problem)
        {
            ProblemDetails = problem;
        }
    }
}
