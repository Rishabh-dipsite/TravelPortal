using SharedClassLibrary.Entities.Requests;

namespace ProviderService.DemoLogics
{
    public static class DummyValidation
    {
        public static string ValidateEntity(string entityId, EntityType entityType)
        {
            //Dummy logic
            if (entityType == EntityType.Flight && !entityId.ToLower().StartsWith("boeing"))
                return "No seats available, please check some other flight";
            if (entityType == EntityType.Hotel && !entityId.ToLower().StartsWith("lemon"))
                return "No rooms available, please check some other hotel";
            return String.Empty;
        }
    }
}
