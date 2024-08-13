using System;
using System.Linq;
using System.Linq.Expressions;

public static class EnumFlags
{
    public static TEnum SetFlag<TEnum>(this TEnum value, TEnum flag, bool state) where TEnum : Enum
    {
        var underlyingType = Enum.GetUnderlyingType(typeof(TEnum));
        var left = Convert.ChangeType(value, underlyingType);
        var right = Convert.ChangeType(flag, underlyingType);

        dynamic result;

        if (state)
            result = (dynamic)left | (dynamic)right;
        else
            result = (dynamic)left & ~(dynamic)right;

        return (TEnum)Enum.ToObject(typeof(TEnum), result);
    }

    public static TEnum RaiseFlag<TEnum>(this TEnum value, TEnum flag) where TEnum : Enum
        => value.SetFlag(flag, true);

    public static TEnum LowerFlag<TEnum>(this TEnum value, TEnum flag) where TEnum : Enum
        => value.SetFlag(flag, false);

    public static TEnum ClearFlags<TEnum>(this TEnum value) where TEnum : Enum
        => default;

    public static TEnum GetHighestSetFlag<TEnum>(this TEnum value) where TEnum : Enum
    {
        var flags = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
        TEnum highestSetFlag = default;

        foreach (var flag in flags)
        {
            if (value.HasFlag(flag))
            {
                if (Convert.ToUInt64(flag) > Convert.ToUInt64(highestSetFlag))
                {
                    highestSetFlag = flag;
                }
            }
        }

        return highestSetFlag;
    }

    private static class Caster<TSource, TTarget>
    {
        public static readonly Func<TSource, TTarget> Cast = CreateConvertMethod();

        private static Func<TSource, TTarget> CreateConvertMethod()
        {
            var p = Expression.Parameter(typeof(TSource));
            var c = Expression.ConvertChecked(p, typeof(TTarget));
            return Expression.Lambda<Func<TSource, TTarget>>(c, p).Compile();
        }
    }
}
