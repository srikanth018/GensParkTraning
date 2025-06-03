using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

public class ExperiencedDoctorHandler : AuthorizationHandler<ExperiencedDoctorRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ExperiencedDoctorRequirement requirement)
    {
        var role = context.User.FindFirstValue(ClaimTypes.Role);
        var experienceClaim = context.User.FindFirst("YearsOfExperience");

        if (role == "Doctor" && experienceClaim != null &&
            int.TryParse(experienceClaim.Value, out int years) &&
            years >= requirement.MinimumYears)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
