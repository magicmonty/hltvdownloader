namespace Pagansoft.Aria2.Options
{
    /// <summary>
    /// Encapsulates HTTPS specific options
    /// </summary>
    public interface IHttpsOptions
    {
        /// <summary>
        /// Use this proxy server for HTTPS. 
        /// To erase previously defined proxy, use "". 
        /// <seealso cref="Options.AllProxy"/> option. 
        /// This affects all URIs. 
        /// The is [http://][USER:PASSWORD@]HOST[:PORT]
        /// </summary>
        string Proxy { get; set; }

        /// <summary>
        /// Set password for <see cref="Proxy"/>> option.
        /// </summary>
        string ProxyPasswd { get; set; }

        /// <summary>
        /// Set user for <see cref="Proxy"/>> option.
        /// </summary>
        string ProxyUser { get; set; }
    }
}
