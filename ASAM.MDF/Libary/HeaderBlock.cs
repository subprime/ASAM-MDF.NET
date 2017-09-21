using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace 

{
    public class HeaderBlock : Block
    {
        /// <summary>
        /// Gets the data groups.
        /// </summary>
        /// <value>
        /// The data groups.
        /// </value>
        public DataGroupCollection DataGroups { get; private set; }
        
        /// <summary>
        /// Gets the file comment.
        /// </summary>
        /// <value>
        /// The file comment.
        /// </value>
        public TextBlock FileComment { get; private set; }

        /// <summary>
        /// Gets the program block.
        /// </summary>
        /// <value>
        /// The program block.
        /// </value>
        public ProgramBlock ProgramBlock { get; private set; }

        /// <summary>
        /// Number of data groups (redundant information)
        /// </summary>
        /// <value>
        /// The data groups count.
        /// </value>
        public ushort DataGroupsCount { get; private set; }

        /// <summary>
        /// Gets the date at which the recording was started in "DD:MM:YYYY" format
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public string Date { get; private set; }

        /// <summary>
        /// Gets the time at which the recording was started in "HH:MM:SS" format 
        /// for locally displayed time, i.e. considering the daylight saving time (DST)
        /// Note: all time stamps in a time channel are only relative to the start 
        /// time of the measurement (see remark on time channel type in CNBLOCK)
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        public string Time { get; private set; }

        /// <summary>
        /// Gets the authors name.
        /// </summary>
        /// <value>
        /// The author.
        /// </value>
        public string Author { get; private set; }

        /// <summary>
        /// Gets the name of the organization or department
        /// </summary>
        /// <value>
        /// The organization or department.
        /// </value>
        public string Organization { get; private set; }

        /// <summary>
        /// Gets the project name.
        /// </summary>
        /// <value>
        /// The project.
        /// </value>
        public string Project { get; private set; }

        /// <summary>
        /// Gets the Subject / Measurement object, e.g. vehicle information
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public string Subject { get; private set; }

        /// <summary>
        /// Time stamp at which recording was started in nanoseconds. 
        /// Elapsed time since 00:00:00 01.01.1970 (local time) 
        /// (local time = UTC time + UTC time offset) 
        /// Note: the local time does not contain a daylight saving time (DST) 
        /// offset! Valid since version 3.20. Default value: 0 See <see cref="UTCTimeOffset"/>
        /// </summary>
        /// <value>
        /// The time stamp.
        /// </value>
        [MdfVersion(320, 0)]
        public ulong TimeStamp { get; private set; }

        /// <summary>
        /// UTC time offset in hours (= GMT time zone) For example 1 means 
        /// GMT+1 time zone = Central European Time (CET). The value must be
        /// in range [-12, 12], i.e. it can be negative! 
        /// Valid since version 3.20. 
        /// Default value: 0 (= GMT time)
        /// </summary>
        /// <value>
        /// The UTC time offset.
        /// </value>
        [MdfVersion(320, 0)]
        public short UTCTimeOffset { get; private set; }

        /// <summary>
        /// Time quality class
        /// 0 = local PC reference time (Default)
        /// 10 = external time source
        /// 16 = external absolute synchronized time
        /// Valid since version 3.20. Default value: 0
        /// </summary>
        /// <value>
        /// The time quality.
        /// </value>
        [MdfVersion(320, 0)]
        public TimeQuality TimeQuality { get; private set; }

        /// <summary>
        /// Timer identification (time source), e.g. "Local PC Reference Time" 
        /// or "GPS Reference Time". 
        /// Valid since version 3.20. 
        /// Default value: empty string
        /// </summary>
        /// <value>
        /// The timer identification.
        /// </value>
        [MdfVersion(320, "")]
        public string TimerIdentification { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderBlock" /> class.
        /// </summary>
        /// <param name="mdf">The MDF.</param>
        /// <exception cref="System.FormatException"></exception>
        public HeaderBlock(Mdf mdf)
            : base(mdf)
        {
            byte[] data = new byte[Size - 4];
            int read = Mdf.Data.Read(data, 0, data.Length);

            if (read != data.Length)
                throw new FormatException();

            FileComment = null;
            ProgramBlock = null;
            uint ptrFirstDataGroup = BitConverter.ToUInt32(data, 0);
            uint ptrTextBlock = BitConverter.ToUInt32(data, 4);
            uint ptrProgramBlock = BitConverter.ToUInt32(data, 8);
            DataGroupsCount = BitConverter.ToUInt16(data, 12);
            Date = UTF8Encoding.UTF8.GetString(data, 14, 10);
            Time = UTF8Encoding.UTF8.GetString(data, 24, 8);
            Author = UTF8Encoding.UTF8.GetString(data, 32, 32);
            Organization = UTF8Encoding.UTF8.GetString(data, 64, 32);
            Project = UTF8Encoding.UTF8.GetString(data, 96, 32);
            Subject = UTF8Encoding.UTF8.GetString(data, 128, 32);

            /// Get the current version of MDF file 
            /// and check if the property is avaible in this version
            var _RequiredVersionForTimeStamp = (MdfVersionAttribute)Attribute.GetCustomAttribute( typeof(HeaderBlock).GetProperty("TimeStamp"), typeof(MdfVersionAttribute));
            if (this.Mdf.IDBlock.Version >= _RequiredVersionForTimeStamp.Version)
                TimeStamp = BitConverter.ToUInt64(data, 160);
            else
                TimeStamp = Convert.ToUInt64(_RequiredVersionForTimeStamp.DefaultValue);

            /// Get the current version of MDF file 
            /// and check if the property is avaible in this version
            var _RequiredVersionForUTCTimeOffset = (MdfVersionAttribute)Attribute.GetCustomAttribute(typeof(HeaderBlock).GetProperty("UTCTimeOffset"), typeof(MdfVersionAttribute));
            if (this.Mdf.IDBlock.Version >= _RequiredVersionForUTCTimeOffset.Version)
                UTCTimeOffset = BitConverter.ToInt16(data, 168);
            else
                UTCTimeOffset = Convert.ToInt16(_RequiredVersionForUTCTimeOffset.DefaultValue);

            /// Get the current version of MDF file 
            /// and check if the property is avaible in this version
            var _RequiredVersionForTimeQuality = (MdfVersionAttribute)Attribute.GetCustomAttribute(typeof(HeaderBlock).GetProperty("TimeQuality"), typeof(MdfVersionAttribute));
            if (this.Mdf.IDBlock.Version >= _RequiredVersionForTimeQuality.Version)
                TimeQuality = (TimeQuality)BitConverter.ToUInt16(data, 170);
            else
                TimeQuality = (TimeQuality)Convert.ToUInt16(_RequiredVersionForTimeQuality.DefaultValue);

            /// Get the current version of MDF file 
            /// and check if the property is avaible in this version
            var _RequiredVersionForTimerIdentification = (MdfVersionAttribute)Attribute.GetCustomAttribute(typeof(HeaderBlock).GetProperty("TimerIdentification"), typeof(MdfVersionAttribute));
            if (this.Mdf.IDBlock.Version >= _RequiredVersionForTimerIdentification.Version)
                TimerIdentification = UTF8Encoding.UTF8.GetString(data, 172, 32);
            else
                TimerIdentification = _RequiredVersionForTimerIdentification.DefaultValue.ToString();

			/// Check if ptrTextBlock is null
            if (ptrTextBlock != 0)
            {
                Mdf.Data.Position = ptrTextBlock;
                FileComment = new TextBlock(mdf);
            }

			/// Check if ptrProgramBlock is null
            if (ptrProgramBlock != 0)
            {
                Mdf.Data.Position = ptrProgramBlock;
                ProgramBlock = new ProgramBlock(mdf);
            }

            /// Check if ptrFirstDataGroup is null
			if (ptrFirstDataGroup != 0)
			{
				Mdf.Data.Position = ptrFirstDataGroup;
				DataGroups = new DataGroupCollection(mdf, new DataGroupBlock(mdf));
			}
        }
    }
}
