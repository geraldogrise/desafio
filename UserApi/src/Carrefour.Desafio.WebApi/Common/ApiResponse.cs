﻿using Carrefour.Desafio.Common.Validation;

namespace Carrefour.Desafio.WebApi.Common;

public class ApiResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public IEnumerable<ValidationErrorDetail> Errors { get; set; } = [];
}
