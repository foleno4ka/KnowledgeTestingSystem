﻿using System;
using System.Data.Entity;
using KnowledgeControlSystem.DAL.Enitties;
using KnowledgeControlSystem.DAL.Enitties.IdentityEntities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KnowledgeControlSystem.DAL.EF
{
    public class KnowledgeDBContext : IdentityDbContext<IdentityUserEntity, IdentityRoleEntity, int,
        IdentityUserLoginEntity, IdentityUserRoleEntity, IdentityUserClaimEntity>
    {
        public KnowledgeDBContext(string connection)
            : base(connection)
        {
            Database.SetInitializer(new KnowledgeDbContextInitializer());
            Database.Log = Console.Write;
        }

        public virtual DbSet<TestEntity> Tests { get; set; }
        public virtual DbSet<QuestionEntity> Questions { get; set; }
        public virtual DbSet<AnswerEntity> Answers { get; set; }
        public virtual DbSet<TestResultEntity> TestResults { get; set; }
        public virtual DbSet<CategoryEntity> Categories { get; set; }
    }
}