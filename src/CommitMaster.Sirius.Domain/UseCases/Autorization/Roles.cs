namespace CommitMaster.Sirius.Domain.UseCases.Autorization
{
    public static class Roles
    {
        public static string AssinaturaValida => "Assinatura Valida";
        public static string AssinaturaExpirada => "Assinatura Expirada";
    }

    public enum RolesEnum
    {
        AssinaturaValida,
        AssinaturaExpirada,
        SemAssinatura
    }
}
