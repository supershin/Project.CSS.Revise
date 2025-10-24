using Project.CSS.Revise.Web.Models.Pages.Project;
using Project.CSS.Revise.Web.Respositories;

namespace Project.CSS.Revise.Web.Service
{
    public interface IProjectService
    {
        public List<ProjectSettingModel.ListProjectItem> GetlistProjectTable(ProjectSettingModel.ProjectFilter filter);
        public ProjectSettingModel.ReturnMessage SaveEditProject(ProjectSettingModel.DataProjectIUD Model);
        public ProjectSettingModel.ReturnMessage SaveUpdateUnitViewTempBlk(string projectID, int UserID);
    }
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepo _projectRepo;

        public ProjectService(IProjectRepo ProjectRepo)
        {
            _projectRepo = ProjectRepo;
        }
        public List<ProjectSettingModel.ListProjectItem> GetlistProjectTable(ProjectSettingModel.ProjectFilter filter)
        {
            return _projectRepo.GetlistProjectTable(filter);
        }

        public ProjectSettingModel.ReturnMessage SaveEditProject(ProjectSettingModel.DataProjectIUD Model)
        {
            return _projectRepo.SaveEditProject(Model);
        }

        public ProjectSettingModel.ReturnMessage SaveUpdateUnitViewTempBlk(string projectID , int UserID)
        {
            return _projectRepo.SaveUpdateUnitViewTempBlk(projectID, UserID);
        }
    }
}
