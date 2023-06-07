using Microsoft.EntityFrameworkCore;

namespace NutriTEC_API.Models
{
    [Keyless]
    public class AssignEatingPlanClient
    {
        public int assign_eating_plan_to_client { get; set; } = 0;
    }
}
