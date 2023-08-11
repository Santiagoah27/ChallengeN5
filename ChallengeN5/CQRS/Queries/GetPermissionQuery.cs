
namespace ChallengeN5.CQRS.Queries
{
    public class GetPermissionQuery
    {
        public int Id { get; set; }

        public GetPermissionQuery(int id)
        {
            Id = id;
        }
    }
}