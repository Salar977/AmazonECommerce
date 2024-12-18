﻿namespace AmazonECommerce.Application.DTOs.Identity;

public class UserRequest
{
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string ConfirmPassword { get; set; }
}
