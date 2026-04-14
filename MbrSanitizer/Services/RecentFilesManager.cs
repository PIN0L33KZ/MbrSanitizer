using MbrSanitizer.Data;
using Newtonsoft.Json;

namespace MbrSanitizer.Services;

internal static class RecentFilesManager
{
    public static List<Project> GetRecentProjects()
    {
        // Return the current list of recent projects, or an empty list if there are none
        List<Project>? recentFiles = JsonConvert.DeserializeObject<List<Project>>(Application.Settings.Default.RecentProjects);

        return recentFiles ?? [];
    }

    public static void AddToRecentProjects(Project project)
    {
        // Check if project object is null
        if(project == null)
            return;

        // Get the current list of recent projects from application settings
        List<Project> recentProjects = GetRecentProjects();

        // Check if project is already in the list of recent projects
        if(recentProjects.Contains(project))
            return;

        // Add the project to the list of recent projects
        recentProjects.Add(project);

        // Serialize the updated list and save it back to settings
        Application.Settings.Default.RecentProjects = JsonConvert.SerializeObject(recentProjects);
        Application.Settings.Default.Save();
    }

    public static void DeleteFromRecentProjects(Project project)
    {
        // Check if project object is null
        if(project == null)
            return;

        // Get the current list of recent projects from application settings
        List<Project> recentProjects = GetRecentProjects();

        // Check if project is not in the list of recent projects
        if(!recentProjects.Contains(project))
            return;

        // Remove the project from the list of recent projects
        _ = recentProjects.Remove(project);

        // Serialize the updated list and save it back to settings
        Application.Settings.Default.RecentProjects = JsonConvert.SerializeObject(recentProjects);
        Application.Settings.Default.Save();
    }

    public static void ClearRecentProjects()
    {
        // Get the current list of recent projects
        List<Project> currentRecentProjects = GetRecentProjects();

        // Create a modified list of recent projects
        List<Project> modifiedRecentProjects = [.. currentRecentProjects];

        // Process each project
        foreach(Project project in currentRecentProjects)
        {
            // Remove project from list if the file does not exist
            if(!FileManagerService.DoesFileExist(project.Path))
                _ = modifiedRecentProjects.Remove(project);
        }

        // Remove dubplicate projects from the list
        modifiedRecentProjects = modifiedRecentProjects
            .DistinctBy(p => p.Path)
            .ToList();

        // Serialize the updated list and save it back to settings
        Application.Settings.Default.RecentProjects = JsonConvert.SerializeObject(modifiedRecentProjects);
        Application.Settings.Default.Save();
    }
}