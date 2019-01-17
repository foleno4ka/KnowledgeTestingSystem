using Autofac;
using KnowledgeControlSystem.BLL.DTOs;
using KnowledgeControlSystem.BLL.Interfaces;
using KnowledgeControlSystem.BLL.Services;
using KnowledgeControlSystem.DAL.Autofac;
using KnowledgeControlSystem.DAL.Interfaces;
using KnowledgeControlSystem.DAL.Repositories;

namespace KnowledgeControlSystem.BLL.Autofac
{
    public class BllConfigModule : Module
    {
        public string Connection { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new DalConfigModule()
            {
                Connection = this.Connection
            });
            builder.RegisterType(typeof(StatisticService)).As(typeof(IStatisticService));
            builder.RegisterType(typeof(TestService)).As(typeof(ITestService));
            builder.RegisterType(typeof(TestResultService)).As(typeof(ITestResultService));
            builder.RegisterType(typeof(CategoryService)).As(typeof(IService<CategoryDTO>));
            builder.RegisterType(typeof(IRoleService)).As(typeof(IService<RoleDTO>));
            builder.RegisterType(typeof(UserService)).As(typeof(IUserService));

            builder.RegisterType(typeof(KnowledgeUnitOfWork)).As(typeof(IUnitOfWork)).InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}