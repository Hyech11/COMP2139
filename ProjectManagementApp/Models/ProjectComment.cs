using System;
using ProjectManagementApp.Models;

public class ProjectComment
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string Content { get; set; }
    public string Author { get; set; }
    public DateTime CreatedAt { get; set; }

    // 네비게이션
    public Project Project { get; set; }
}