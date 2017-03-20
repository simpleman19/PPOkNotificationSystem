﻿using PPOk_Notifications.Service;

namespace PPOk_Notifications.Models
{
    public class User
    {
		[Column(Name = "user_lname")] public string LastName { get; set; }

        [Column(Name = "user_fname")] public string FirstName { get; set; }

        [Column(Name = "user_id")] public long IdNumber { get; set; }

		[Column(Name = "user_phone")] public string Phone { get; set; }

		[Column(Name = "user_email")] public string Email { get; set; }

		[Column(Name = "user_type")] public char Type { get; set; }

		[Column(Name = "object_active")] public bool Enabled { get; set; }

		public Login UserLogin { get; set; }
    }
}