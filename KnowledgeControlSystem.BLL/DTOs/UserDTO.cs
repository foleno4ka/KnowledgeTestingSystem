﻿namespace KnowledgeControlSystem.BLL.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string[] Roles { get; set; }
    }
}
