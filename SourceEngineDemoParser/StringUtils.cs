using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SourceEngineDemoParser
{
    public static class StringUtils
    {
        private const char NULL_TERMINATE = '\0';
        public static string ReadNullTerminatedString(BinaryReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            StringBuilder stringBuilder = new StringBuilder();
            char readedChar = NULL_TERMINATE;
            while ((readedChar = reader.ReadChar()) != NULL_TERMINATE)
            {
                stringBuilder.Append(readedChar);
            }

            return stringBuilder.ToString();
        }

        public static string ReadSizedNullString(BinaryReader reader, int size)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));
            if (size < 1)
                throw new ArgumentNullException(nameof(size));
            StringBuilder stringBuilder = new StringBuilder();
            char readedChar = NULL_TERMINATE;

            for (int i = 0; i < size; i++)
            {
                readedChar = reader.ReadChar();
                if (readedChar != NULL_TERMINATE)
                    stringBuilder.Append(readedChar);
            }

            return stringBuilder.ToString();
        }
    }
}
