﻿using Application.Interfaces.Identity;
using Infrastructure.DataAccess;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services.Identity
{
    public class AuthServices(SignInManager<IdentityUser> signInManager) : IAuth
    {
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;

        public async Task<SignInResult> LoginAsync(string Username, string Password) => await _signInManager.PasswordSignInAsync(Username, Password, isPersistent: false, lockoutOnFailure: false);
    }
}
