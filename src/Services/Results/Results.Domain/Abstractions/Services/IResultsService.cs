﻿using Results.Domain.Models;

namespace Results.Domain.Abstractions.Services
{
    public interface IResultsService
    {
        Task Create(Result result);
    }
}
