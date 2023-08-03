using System;
using System.IO;

namespace CrystalEditor.Core.Utility
{
    /// <inheritdoc cref="BinaryReader"/>
    /// <summary>
    /// Reads big endian values from a data stream.
    /// </summary>
    public class BigEndianBinaryReader : BinaryReader
    {
        public BigEndianBinaryReader(Stream stream) : base(stream) { }

        public override short ReadInt16() => base.ReadInt16().Reverse();
        public override ushort ReadUInt16() => base.ReadUInt16().Reverse();
        public override int ReadInt32() => base.ReadInt32().Reverse();
        public override uint ReadUInt32() => base.ReadUInt32().Reverse();
        public override long ReadInt64() => base.ReadInt64().Reverse();
        public override ulong ReadUInt64() => base.ReadUInt64().Reverse();
        public void Seek(long offset, SeekOrigin origin = SeekOrigin.Begin) => base.BaseStream.Seek(offset, origin);
    }
}
