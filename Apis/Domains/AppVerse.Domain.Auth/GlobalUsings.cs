﻿global using AppVerse.Domain.Authentication.Commands;
global using AppVerse.Domain.Authentication.Queries;
global using AppVerse.Infrastructure.Commands;
global using AppVerse.Infrastructure.Events;
global using AppVerse.Infrastructure.Queries;
global using AppVerse.Service.Authentication;
global using AppVerse.Services;
global using FluentValidation;
global using FluentValidation.Results;
global using MediatR;
global using Microsoft.EntityFrameworkCore;
global using System.Text.RegularExpressions;
global using AppVerse.Conference.MsSql.Entity;
global using AppVerse.Models; 
global using AppVerse.Domain.Auth.Dto;