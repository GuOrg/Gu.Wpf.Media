namespace Gu.Wpf.Media
{
    /// <summary>
    /// Helper for creating filter for <see cref="Microsoft.Win32.OpenFileDialog"/>
    /// </summary>
    public static class FileFormats
    {
        /// <summary>
        /// A filter string with some common video formats.
        /// https://support.microsoft.com/en-us/kb/316992
        /// </summary>
        public static string DefaultVideoFormats { get; } = "*.dat; *.wmv; *.3g2; *.3gp; *.3gp2; *.3gpp; *.amv; *.asf;  *.avi; *.bin; *.cue; *.divx; *.dv; *.flv; *.gxf; *.iso; *.m1v; *.m2v; *.m2t; *.m2ts; *.m4v; *.mkv; *.mov; *.mp2; *.mp2v; *.mp4; *.mp4v; *.mpa; *.mpe; *.mpeg; *.mpeg1; *.mpeg2; *.mpeg4; *.mpg; *.mpv2; *.mts; *.nsv; *.nuv; *.ogg; *.ogm; *.ogv; *.ogx; *.ps; *.rec; *.rm; *.rmvb; *.tod; *.ts; *.tts; *.vob; *.vro; *.webm";

        /// <summary>
        /// A filter string with some common audio formats.
        /// https://support.microsoft.com/en-us/kb/316992
        /// </summary>
        public static string DefaultAudioFormats { get; } = "*.mp3; *.wma; *.aac; *.adt; *.adts; *.m4a; *.wav; *.aif; *.aifc; *.aiff; *.cda";
    }
}
