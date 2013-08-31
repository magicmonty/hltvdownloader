using Pagansoft.Aria2.Options.Enums;

namespace Pagansoft.Aria2.Options
{
    /// <summary>
    /// Encapsulates FTP specific options
    /// </summary>
    public interface IFtpOptions
    {
        /// <summary>
        /// Set FTP password. This affects all URIs. 
        /// If user name is embedded but password is missing in URI, 
        /// aria2 tries to resolve password using .netrc. 
        /// If password is found in .netrc, then use it as password. 
        /// If not, use the password specified in this option. 
        /// Default: ARIA2USER@
        /// </summary>
        string Passwd { get; set; }

        /// <summary>
        /// Use the passive mode in FTP. 
        /// If false is given, the active mode will be used. 
        /// Default: true
        /// </summary>
        bool Pasv { get; set; }

        /// <summary>
        /// Use this proxy server for FTP. To erase previously defined proxy, use "". 
        /// <seealso cref="Options.AllProxy"/> option. 
        /// This affects all URIs. 
        /// The format is [http://][USER:PASSWORD@]HOST[:PORT]
        /// </summary>
        string Proxy { get; set; }

        /// <summary>
        /// Set password for <see cref="Proxy"/> option.
        /// </summary>
        string ProxyPasswd { get; set; }

        /// <summary>
        /// Set user for <see cref="Proxy"/> option.
        /// </summary>
        string ProxyUser { get; set; }

        /// <summary>
        /// Reuse connection in FTP. 
        /// Default: true
        /// </summary>
        bool ReuseConnection { get; set; }

        /// <summary>
        /// Set FTP transfer type. TYPE is either binary or ascii. 
        /// Default: binary
        /// </summary>
        FtpTransferTypeOption TransferType { get; set; }

        /// <summary>
        /// Set FTP user. This affects all URIs. 
        /// Default: anonymous
        /// </summary>
        string User { get; set; }
    }
}
