namespace TaskManager.Tests.Integration;

[Collection("App")]
public class IntegrationTest(AppFixture app) : IClassFixture<AppFixture>
{
    private const string Uri = "http://localhost/";

    [Fact]
    public async Task Post_project_deve_retornar_sucesso()
    {
        var project = new ProjectInsertDto() { Title = "Test Project", Description = "Test Project Description", };
        var result = await Call<ResponseDto<ProjectResponseDto>>(CreateRequest(HttpMethod.Post, Uri + "api/project", project, headers: new() { { "userId", "1" }, }));

        result.Data.Id.Should().Be(2);
    }

    [Fact]
    public async Task Get_project_deve_retornar_sucesso()
    {
        var project = new ProjectInsertDto() { Title = "Test Project", Description = "Test Project Description", };
        await Call<ResponseDto<ProjectResponseDto>>(CreateRequest(HttpMethod.Post, Uri + "api/project", project, headers: new() { { "userId", "1" }, }));
        var result = await Call<ResponseDto<List<ProjectResponseDto>>>(CreateRequest(HttpMethod.Get, Uri + "api/project", headers: new() { { "userId", "1" }, }));

        result.Data.Should().HaveCount(7);
    }

    [Fact]
    public async Task Get_Project_deve_retornar_erro_autenticacao()
    {
        var result = await Call<ResponseDto<List<ProjectResponseDto>>>(CreateRequest(HttpMethod.Get, Uri + "api/project"));

        result.Error.Should().BeEquivalentTo("Usuario deve estar autenticado para essa solicitação.");
    }

    [Fact]
    public async Task Delete_project_deve_retornar_sucesso()
    {
        var result = await Call<ResponseDto>(CreateRequest(HttpMethod.Delete, Uri + "api/project/1", headers: new() { { "userId", "1" }, }));

        result?.Error?.Should().BeNull();
    }

    [Fact]
    public async Task Delete_project_deve_retornar_erro()
    {
        var project = new ProjectInsertDto() { Title = "Test Project", Description = "Test Project Description", };
        var projectRS = await Call<ResponseDto<ProjectResponseDto>>(CreateRequest(HttpMethod.Post, Uri + "api/project", project, headers: new() { { "userId", "1" }, }));
        var task = new TaskInsertDto() { Title = "Task test", Description = "Task description", Priority = 0, ProjectId = projectRS.Data.Id };
        await Call<ResponseDto>(CreateRequest(HttpMethod.Post, Uri + "api/task", task, headers: new() { { "userId", "1" }, }));
        var result = await Call<ResponseDto>(CreateRequest(HttpMethod.Delete, Uri + $"api/project/{projectRS.Data.Id}", headers: new() { { "userId", "1" }, }));

        result?.Error?.Should().BeEquivalentTo("Não é possivel remover projetos com tarefas pendentes, conclua ou cancele todas as tarefas.");
    }

    [Fact]
    public async Task Post_task_deve_retornar_sucesso()
    {
        var project = new ProjectInsertDto() { Title = "Test Project", Description = "Test Project Description", };
        var projectRS = await Call<ResponseDto<ProjectResponseDto>>(CreateRequest(HttpMethod.Post, Uri + "api/project", project, headers: new() { { "userId", "1" }, }));
        var task = new TaskInsertDto() { Title = "Task Ok", Description = "Task description", Priority = 0, ProjectId = projectRS.Data.Id };
        var taskRS = await Call<ResponseDto<TaskResponseDto>>(CreateRequest(HttpMethod.Post, Uri + "api/task", task, headers: new() { { "userId", "1" }, }));

        taskRS?.Data?.Title.Should().BeEquivalentTo(task.Title);
    }

    [Fact]
    public async Task Update_task_deve_retornar_sucesso()
    {
        var project = new ProjectInsertDto() { Title = "Test Project", Description = "Test Project Description", };
        var projectRS = await Call<ResponseDto<ProjectResponseDto>>(CreateRequest(HttpMethod.Post, Uri + "api/project", project, headers: new() { { "userId", "1" }, }));
        var task = new TaskInsertDto() { Title = "Task Ok", Description = "Task description", Priority = 0, ProjectId = projectRS.Data.Id };
        var taskRS = await Call<ResponseDto<TaskResponseDto>>(CreateRequest(HttpMethod.Post, Uri + "api/task", task, headers: new() { { "userId", "1" }, }));
        var taskUpdate = new TaskUpdateDto() { Id = taskRS.Data.Id, Title = "Task Update Ok", Description = "Task description", Status = TaskStatusEnum.Concluded };
        var taskUpdateRS = await Call<ResponseDto<TaskResponseDto>>(CreateRequest(HttpMethod.Put, Uri + "api/task", taskUpdate, headers: new() { { "userId", "1" }, }));

        taskUpdateRS?.Data?.Title.Should().BeEquivalentTo(taskUpdate.Title);
    }

    [Fact]
    public async Task Get_tasks_deve_retornar_sucesso()
    {
        var project = new ProjectInsertDto() { Title = "Test Project", Description = "Test Project Description", };
        var projectRS = await Call<ResponseDto<ProjectResponseDto>>(CreateRequest(HttpMethod.Post, Uri + "api/project", project, headers: new() { { "userId", "1" }, }));
        var task = new TaskInsertDto() { Title = "Task Ok", Description = "Task description", Priority = 0, ProjectId = projectRS.Data.Id };
        await Call<ResponseDto<TaskResponseDto>>(CreateRequest(HttpMethod.Post, Uri + "api/task", task, headers: new() { { "userId", "1" }, }));
        var taskGetRS = await Call<ResponseDto<List<TaskResponseDto>>>(CreateRequest(HttpMethod.Get, Uri + $"api/task/{projectRS.Data.Id}", headers: new() { { "userId", "1" }, }));

        taskGetRS?.Data?.Should().HaveCount(1);
    }

    [Fact]
    public async Task Delete_task_deve_retornar_sucesso()
    {
        var project = new ProjectInsertDto() { Title = "Test Project", Description = "Test Project Description", };
        var projectRS = await Call<ResponseDto<ProjectResponseDto>>(CreateRequest(HttpMethod.Post, Uri + "api/project", project, headers: new() { { "userId", "1" }, }));
        var task = new TaskInsertDto() { Title = "Task Ok", Description = "Task description", Priority = 0, ProjectId = projectRS.Data.Id };
        var taskRS = await Call<ResponseDto<TaskResponseDto>>(CreateRequest(HttpMethod.Post, Uri + "api/task", task, headers: new() { { "userId", "1" }, }));
        var taskDeleteRS = await Call<ResponseDto<List<TaskResponseDto>>>(CreateRequest(HttpMethod.Delete, Uri + $"api/task/{taskRS.Data.Id}", headers: new() { { "userId", "1" }, }));

        taskDeleteRS?.Error?.Should().BeNull();
    }

    [Fact]
    public async Task Post_comment_deve_retornar_sucesso()
    {
        var project = new ProjectInsertDto() { Title = "Test Project", Description = "Test Project Description", };
        var projectRS = await Call<ResponseDto<ProjectResponseDto>>(CreateRequest(HttpMethod.Post, Uri + "api/project", project, headers: new() { { "userId", "1" }, }));
        var task = new TaskInsertDto() { Title = "Task Ok", Description = "Task description", Priority = 0, ProjectId = projectRS.Data.Id };
        var taskRS = await Call<ResponseDto<TaskResponseDto>>(CreateRequest(HttpMethod.Post, Uri + "api/task", task, headers: new() { { "userId", "1" }, }));
        var comment = new CommentInsertDto() { Value = "Comment Test" };
        var commentRS = await Call<ResponseDto<CommentResponseDto>>(CreateRequest(HttpMethod.Post, Uri + $"api/task/{taskRS.Data.Id}/comment", comment, headers: new() { { "userId", "1" }, }));

        commentRS?.Data?.Value.Should().BeEquivalentTo(comment.Value);
    }

    [Fact]
    public async Task Update_comment_deve_retornar_sucesso()
    {
        var project = new ProjectInsertDto() { Title = "Test Project", Description = "Test Project Description", };
        var projectRS = await Call<ResponseDto<ProjectResponseDto>>(CreateRequest(HttpMethod.Post, Uri + "api/project", project, headers: new() { { "userId", "1" }, }));
        var task = new TaskInsertDto() { Title = "Task Ok", Description = "Task description", Priority = 0, ProjectId = projectRS.Data.Id };
        var taskRS = await Call<ResponseDto<TaskResponseDto>>(CreateRequest(HttpMethod.Post, Uri + "api/task", task, headers: new() { { "userId", "1" }, }));
        var comment = new CommentInsertDto() { Value = "Comment Test" };
        var commentRS = await Call<ResponseDto<CommentResponseDto>>(CreateRequest(HttpMethod.Post, Uri + $"api/task/{taskRS.Data.Id}/comment", comment, headers: new() { { "userId", "1" }, }));
        var commentUpdate = new CommentUpdateDto() { Id = commentRS.Data.Id, Value = "Comment update test" };
        var commentUpdateRS = await Call<ResponseDto<CommentResponseDto>>(CreateRequest(HttpMethod.Put, Uri + $"api/task/{taskRS.Data.Id}/comment", commentUpdate, headers: new() { { "userId", "1" }, }));

        commentUpdateRS?.Data?.Value.Should().BeEquivalentTo(commentUpdate.Value);
    }

    [Fact]
    public async Task Delete_comment_deve_retornar_sucesso()
    {
        var project = new ProjectInsertDto() { Title = "Test Project", Description = "Test Project Description", };
        var projectRS = await Call<ResponseDto<ProjectResponseDto>>(CreateRequest(HttpMethod.Post, Uri + "api/project", project, headers: new() { { "userId", "1" }, }));
        var task = new TaskInsertDto() { Title = "Task Ok", Description = "Task description", Priority = 0, ProjectId = projectRS.Data.Id };
        var taskRS = await Call<ResponseDto<TaskResponseDto>>(CreateRequest(HttpMethod.Post, Uri + "api/task", task, headers: new() { { "userId", "1" }, }));
        var comment = new CommentInsertDto() { Value = "Comment Test" };
        var commentRS = await Call<ResponseDto<CommentResponseDto>>(CreateRequest(HttpMethod.Post, Uri + $"api/task/{taskRS.Data.Id}/comment", comment, headers: new() { { "userId", "1" }, }));
        var commentDeleteRS = await Call<ResponseDto<CommentResponseDto>>(CreateRequest(HttpMethod.Delete, Uri + $"api/task/comment{commentRS.Data.Id}", headers: new() { { "userId", "1" }, }));

        commentDeleteRS?.Error?.Should().BeNull();
    }

    [Fact]
    public async Task Get_Report_deve_retornar_sucesso()
    {
        var project = new ProjectInsertDto() { Title = "Test Project", Description = "Test Project Description", };
        var projectRS = await Call<ResponseDto<ProjectResponseDto>>(CreateRequest(HttpMethod.Post, Uri + "api/project", project, headers: new() { { "userId", "1" }, }));
        var task = new TaskInsertDto() { Title = "Task Ok", Description = "Task description", Priority = 0, ProjectId = projectRS.Data.Id };
        var taskRS = await Call<ResponseDto<TaskResponseDto>>(CreateRequest(HttpMethod.Post, Uri + "api/task", task, headers: new() { { "userId", "1" }, }));
        var taskUpdate = new TaskUpdateDto() { Id = taskRS.Data.Id, Title = "Task Update Ok", Description = "Task description", Status = TaskStatusEnum.Concluded };
        await Call<ResponseDto<TaskResponseDto>>(CreateRequest(HttpMethod.Put, Uri + "api/task", taskUpdate, headers: new() { { "userId", "1" }, }));
        var report = await Call<ResponseDto<List<ReportEntity>>>(CreateRequest(HttpMethod.Get, Uri + $"api/report", headers: new() { { "userId", "1" }, }));

        report?.Data?.Should().HaveCount(1);
    }

    private async Task<T> Call<T>(HttpRequestMessage request)
    {
        var httpClient = app.CreateClient();
        var response = await httpClient.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(result);
    }

    private HttpRequestMessage CreateRequest(HttpMethod method, string uri, object request = null, Dictionary<string, string> headers = null)
    {
        var content = request != null ? new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json") : new StringContent(string.Empty);
        if (headers != null)
        {
            foreach (var header in headers)
                content.Headers.Add(header.Key, header.Value);
        }

        HttpRequestMessage httpRequest = new()
        {
            RequestUri = new Uri(uri),
            Method = method,
            Content = content,
        };
        return httpRequest;
    }
}
