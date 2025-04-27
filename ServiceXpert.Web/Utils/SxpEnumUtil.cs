namespace ServiceXpert.Web.Utils;
public static class SxpEnumUtil
{
    /// <summary>
    /// Converts an enum into a dictionary where the key is the enum's integer value and the value is its name as a string.
    /// </summary>
    /// <typeparam name="T">The enum type to convert.</typeparam>
    /// <returns>A dictionary mapping enum values to their names.</returns>
    public static Dictionary<int, string> ToDictionary<T>() where T : struct, Enum
    {
        return Enum.GetValues(typeof(T)).Cast<T>().ToDictionary(e => Convert.ToInt32(e), e => e.ToString());
    }

    /// <summary>
    /// Converts an enum into a list of its names as strings.
    /// </summary>
    /// <typeparam name="T">The enum type to convert.</typeparam>
    /// <returns>A list of strings representing the names of the enum values.</returns>
    public static List<string> ToList<T>() where T : struct, Enum
    {
        return Enum.GetNames(typeof(T)).ToList();
    }
}
