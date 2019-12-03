using System;
using System.Collections.Generic;
using System.Configuration;

namespace JobPool
{

    public class JobParamConfig : ConfigurationElement
    {
        [ConfigurationProperty("key", IsRequired = true)]
        public string Key { get { return (string)this["key"]; } set { this["key"] = value; } }

        [ConfigurationProperty("value", IsRequired = true)]
        public string Value { get { return (string)this["value"]; } set { this["value"] = value; } }

    }

    public class JobConfig : ConfigurationElementCollection
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name { get { return (string)this["name"]; } set { this["name"] = value; } }

        [ConfigurationProperty("type", IsRequired = true)]
        public string Type { get { return (string)this["type"]; } set { this["type"] = value; } }

        [ConfigurationProperty("interval", DefaultValue = 5, IsRequired = false)]
        [IntegerValidator(MinValue = 1, MaxValue = 100)]
        public int Interval { get { return (int)this["interval"]; } set { this["interval"] = value; } }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new JobParamConfig();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((JobParamConfig)element).Key;
        }
    }

    public class JobsCollectionConfig : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new JobConfig();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((JobConfig)element).Type;
        }        
    }

    public class JobPoolConfig : ConfigurationSection
    {

        private static JobPoolConfig settings = ConfigurationManager.GetSection("JobPoolSettings") as JobPoolConfig;
        public static JobPoolConfig Settings
        {
            get
            {
                return settings;
            }
        }

        [ConfigurationProperty("Jobs", IsDefaultCollection=false)]
        public JobsCollectionConfig Jobs
        {
            get
            {
                return (JobsCollectionConfig)this["Jobs"];
            }
        }
    }

}
