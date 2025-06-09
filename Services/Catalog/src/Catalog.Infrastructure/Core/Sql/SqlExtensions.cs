namespace Catalog.Infrastructure.Core.Sql;

public static class SqlExtensions
{
    /// <summary>
    /// Add output parameter to DynamicParameters
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parameters"></param>
    /// <param name="name"></param>
    /// <param name="size" optional="true"></param>
    /// <returns></returns>
    public static DynamicParameters AddOutput<T>(this DynamicParameters parameters, string name, int? size = null)
    {
        if (size.HasValue)
            parameters.Add(name, dbType: GetDbType<T>(), direction: ParameterDirection.Output, size: size);
        else
            parameters.Add(name, dbType: GetDbType<T>(), direction: ParameterDirection.Output);
        return parameters;
    }
    /// <summary>
    /// Add return parameter to DynamicParameters
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public static DynamicParameters AddReturn(this DynamicParameters parameters)
    {
        parameters.Add("return", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
        return parameters;
    }

    /// <summary>
    /// Get output return from DynamicParameters
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public static int GetReturn(this DynamicParameters parameters)
    {
        return parameters.Get<int>("return");
    }

    private static DbType GetDbType<T>()
    {
        return Type.GetTypeCode(typeof(T)) switch
        {
            TypeCode.Boolean => DbType.Boolean,
            TypeCode.Byte => DbType.Byte,
            TypeCode.DateTime => DbType.DateTime,
            TypeCode.Decimal => DbType.Decimal,
            TypeCode.Double => DbType.Double,
            TypeCode.Int16 => DbType.Int16,
            TypeCode.Int32 => DbType.Int32,
            TypeCode.Int64 => DbType.Int64,
            TypeCode.Object => DbType.Object,
            TypeCode.Single => DbType.Single,
            TypeCode.String => DbType.String,
            _ => DbType.Object,
        };
    }
}