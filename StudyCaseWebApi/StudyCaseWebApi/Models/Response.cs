namespace StudyCaseWebApi.Models;

public record struct Response(bool ResponseResult, string Content, string ResponseMessage);