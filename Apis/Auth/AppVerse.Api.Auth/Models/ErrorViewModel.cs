﻿namespace AppVerse.Api.Authentication.Models;

public record ErrorViewModel
{
    public string RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}