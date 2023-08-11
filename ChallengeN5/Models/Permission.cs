namespace ChallengeN5.Models
{
    public class PermissionType
    {
        public int Id { get; set; }
        public string Description { get; set; }

    }
    public class Permission
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeLastName { get; set; }
        public int PermissionType { get; set;}
        public DateTime PermissionDate { get; set; }

    }

    public class UpdatePermissionDto
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeLastName { get; set; }
        public int PermissionType { get; set; }
        public DateTime PermissionDate { get; set; }
    }
}
