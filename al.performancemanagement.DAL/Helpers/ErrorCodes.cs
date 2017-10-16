namespace al.performancemanagement.DAL.Helpers
{
    public class ErrorCodes
    {
        public const string No_Error = "00000";
        public const string Exception_Error = "00001";
        public const string Timeout_Error = "01000";

        public const string General_Data_Error = "10000";
        public const string Database_Connection_Error = "30001";
        public const string Table_Not_Found_Error = "30002";
        public const string Field_Not_Found_Error = "30003";
        public const string Field_value_Is_Required_Error = "30004";
        public const string Invalid_Field_Value_Error = "30005";

        public const string Insert_Error = "30100";
        public const string Update_Error = "30200";
        public const string Delete_Error = "30300";
    }
}
