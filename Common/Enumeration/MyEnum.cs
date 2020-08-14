using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Enumeration
{
    class MyEnum
    {
    }
    public enum Region : byte
    {

    }
    public enum BanksName : byte
    {

    }
    public enum MessageType
    {
        Success,
        Danger,
        Warning,
        Info,
    }

    public enum FileType
    {
        Audio,
        Picture,
        Video,
        Document,
        Archive,
        Any
    }
    
    public enum HashAlorightm : byte
    {
        MD5,
        SHA1,
        SHA256,
        RFC_2898
    }

}
