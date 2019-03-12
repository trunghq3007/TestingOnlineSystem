namespace TestingSystem.Common
{
    /// <summary>
    /// Defines the <see cref="Base64" />
    /// </summary>
    public class Base64
    {
        /// <summary>
        /// The Encode
        /// </summary>
        /// <param name="input">The input<see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string Encode(string input)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(input);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// The Decode
        /// </summary>
        /// <param name="input">The input<see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string Decode(string input)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(input);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
