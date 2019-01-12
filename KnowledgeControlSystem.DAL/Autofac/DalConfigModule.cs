using Autofac;
using KnowledgeControlSystem.DAL.EF;
using KnowledgeControlSystem.DAL.Enitties;
using KnowledgeControlSystem.DAL.Interfaces;
using KnowledgeControlSystem.DAL.Repositories;

namespace KnowledgeControlSystem.DAL.Autofac
{
    public class DalConfigModule : Module
    {
        public string Connection;

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(KnowledgeDBContext)).AsSelf().WithParameter("connection", this.Connection).InstancePerLifetimeScope();

            builder.RegisterType(typeof(TestRepository)).As(typeof(GenericRepository<TestEntity>));
            builder.RegisterType(typeof(GenericRepository<QuestionEntity>)).As(typeof(IGenericRepository<QuestionEntity>));
            builder.RegisterType(typeof(GenericRepository<AnswerEntity>)).As(typeof(IGenericRepository<AnswerEntity>));
            builder.RegisterType(typeof(UserRepository)).As(typeof(IUserRepository));
            builder.RegisterType(typeof(RoleRepository)).As(typeof(IRoleRepository));
            builder.RegisterType(typeof(GenericRepository<TestResultEntity>)).As(typeof(IGenericRepository<TestResultEntity>));
            base.Load(builder);
        }
    }
}
