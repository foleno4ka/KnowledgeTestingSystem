using Autofac;
using KnowledgeControlSystem.DAL.EF;
using KnowledgeControlSystem.DAL.Interfaces;
using KnowledgeControlSystem.DAL.Repositories;

namespace KnowledgeControlSystem.DAL.Autofac
{
    public class DalConfigModule : Module
    {
        public string Connection;

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(KnowledgeDBContext)).AsSelf().WithParameter("connection", this.Connection)
                .InstancePerLifetimeScope();

            builder.RegisterType(typeof(TestRepository)).As(typeof(ITestRepository));
            builder.RegisterType(typeof(UserRepository)).As(typeof(IUserRepository));
            builder.RegisterType(typeof(RoleRepository)).As(typeof(IRoleRepository));
            builder.RegisterType(typeof(TestResultRepository)).As(typeof(ITestResultRepository));
            builder.RegisterType(typeof(CategoryRepository)).As(typeof(ICategoryRepository));
            base.Load(builder);
        }
    }
}