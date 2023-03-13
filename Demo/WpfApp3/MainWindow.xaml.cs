using LiteDB;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CreateDB();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //using var db = new LiteDatabase(@"MyData.db");

            using var db = new LiteDatabase(new ConnectionString("Filename=MyData1.db;Password=1234"));
            // 获取 Customers 集合
            var col = db.GetCollection<Customer>("customers1");

            // 创建一个对象
            var customer = new Customer
            {
                Name = "John Doe",
                //Phones = new string[] { "8000-0000", "9000-0000" },
                //Age = 39,
                //IsActive = true
            };
           // Customer us=col.FindOne(x => x.Name == "John Doe");
            // 在 Name 字段上创建唯一索引
            //col.EnsureIndex(x => x.Name, true);

            // 数据插入
            col.Insert(customer);

            //// 数据查询 
            //List<Customer> list = col.Find(x => x.Age > 20).ToList();
            //Customer user = col.FindOne(x => x.Age > 20);

            //// 数据删除 
            //col.Delete(user.Id);
        }




        public void CreateDB()
        {
             //using var db = new LiteDatabase(new ConnectionString("Filename=HjmosConfig.db;Password=1234"));
            List<Address> addresses = new List<Address>();
            foreach (AddressConfigEnum item in Enum.GetValues(typeof(AddressConfigEnum)))
            {
                var value = item.GetValue();
                var key = item.ToString();

                Address address = new Address
                {
                    Key = key,
                    Value = value
                };
                addresses.Add(address);
            }

            //var col = db.GetCollection<Address>("HjmosAddress");
            //col.InsertBulk(addresses);
        }

        public  List<AddressConfigEnum> ToList()
        {
            var type = typeof(AddressConfigEnum);

            return (from int index
                    in Enum.GetValues(type)
                    select (AddressConfigEnum)Enum.Parse(type, index.ToString()))
                    .ToList();
        }

    }

    public class AddressConfig:ConfigurationSection
    {
        [ConfigurationProperty("addresses",IsDefaultCollection =true)]
        [ConfigurationCollection(typeof(ConfigSectionDatas), AddItemName = "address")]
        public ConfigSectionDatas Addresses
        {
            get
            {
                return (ConfigSectionDatas)base["addresses"];
            }
        }
    }
   public class ConfigSectionDatas : ConfigurationElementCollection
    {
        

        protected override ConfigurationElement CreateNewElement()
        {
            return new ConfigSectionData();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ConfigSectionData)element).Key;
        }
    }
    

    public class ConfigSectionData: ConfigurationElement
    {
        [ConfigurationProperty("key")]
        public string Key
        {
            get { return (string)this["key"]; }
            set { this["key"] = value; }
        }

        [ConfigurationProperty("value")]
        public string Value
        {
            get { return (string)this["value"]; }
            set { this["value"] = value; }
        }
        [ConfigurationProperty("description")]
        public string Description
        {
            get { return (string)this["description"]; }
            set { this["description"] = value; }
        }
    }



    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public string[] Phones { get; set; }
        //public bool IsActive { get; set; }
    }


    public class Address
    {
        public int Id { get; set; }

        public string Key { get; set; }
        public string Value { get; set; }
        
    }



    public enum AddressConfigEnum
    {
        NccRestfulUrl,
        WireGisAddress,
        WireNetWorkAddress,
        StationHeatMapAddress,
        WorkManagerAddress,
        PlanManagerAddress,
        VideoManagerAddress,
        VideoinspectionManagementAddress,
        ResourceManagerAddress,
        StationLayerManagerAddress,
        ISCSRestfulUrl,
        WSRealTimePushAddress,
        MQAddress,
        HoloceptionLinePassengerFlowAddress,
        DeviceManagerAddress,
        OpenCLoseStationAddress,
        GISStation3DModel,
        n8nVideoRestfulUrl,
        HoloceptionEnableLineNos,
        DeviceWarningMajor,
        ClientCredential,
        PlanWsAddress,
        ChatBoxAddress,
        EscsAddress,
        DataBoardAddress,
        MicsClientId,
        MicsClinetPath,
        OfflineMode,
        HMIAddress,
        Device3DAddress,
        OpenCLoseStationNodeRedAddress,
        StationECommandMap,
        UpgradeHost,
        UpgradeRemotePath,
        UpgradeUsername,
        UpgradePassword,
        IsShowAlarm,
        DeviceUSocket,
        DeviceLSocket,
        DeviceScreenId,
        FloodFightMap,
        RiskPositionElevationMap,
        StationStreetMap,
        StationDrainageMap,
        SectionDrainageMap,
        TrainSchedule,
        PatrolReport,
        UserFeedback,
        LineOd,
        StationOd,
        VideoMode,
        LineFangXunMap,
        AcsDeviceAddress,
        UniviewUsername,
        UniviewPassword,
        UniviewServerIp,
        EmergencyPlan,
        Version3D,
        LinePSD
    }

    public static class EnumExtension
    {
        private static readonly Dictionary<string, Configuration> Config = new();

        public static readonly string RemoteConfigFilePath = @"Hjmos_Ncc.config";
        public static readonly string LocalConfigFilePath = @"Config/Hjmos_Ncc_Local.config";
        public static readonly string RemoteConfigFullFilePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"Hjmos_Ncc.config");
        public static readonly string LocalConfigFullFilePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"Config/Hjmos_Ncc_Local.config");

        public static string GetValue(this AddressConfigEnum addressConfigEnum)
        {

            //var aa = Enum.GetValues(typeof(AddressConfigEnum));

            //var bb = EnumHelper.ToArray<AddressConfigEnum>();


            var remoteConfiguration = ConfigCommon(RemoteConfigFilePath);
            var localConfiguration = ConfigCommon(LocalConfigFilePath);
            var aa = remoteConfiguration.AppSettings.Settings[addressConfigEnum.ToString()].LockAttributes;
            return remoteConfiguration?.AppSettings.Settings[addressConfigEnum.ToString()]?.Value
                    ?? localConfiguration?.AppSettings.Settings[addressConfigEnum.ToString()]?.Value
                   ?? string.Empty;
        }

        public static void SetValue(this AddressConfigEnum addressConfigEnum, string value)
        {
            var remoteConfiguration = ConfigCommon(RemoteConfigFilePath);
            var localConfiguration = ConfigCommon(LocalConfigFilePath);

            var config = localConfiguration.AppSettings.Settings[addressConfigEnum.ToString()] ?? remoteConfiguration?.AppSettings.Settings[addressConfigEnum.ToString()];
            if (config != null)
            {
                config.Value = value;
            }
            remoteConfiguration?.Save(ConfigurationSaveMode.Modified);
            localConfiguration?.Save(ConfigurationSaveMode.Modified);
        }

        public static void AddOrUpdateLocalConfig(this AddressConfigEnum addressConfigEnum, string value)
        {
            var localConfiguration = ConfigCommon(LocalConfigFilePath);
            var config = localConfiguration.AppSettings.Settings[addressConfigEnum.ToString()];
            if (config == null)
            {
                localConfiguration.AppSettings.Settings.Add(new KeyValueConfigurationElement(addressConfigEnum.ToString(), value));
            }
            else
            {
                config.Value = value;
            }
            localConfiguration?.Save(ConfigurationSaveMode.Modified);
        }

        private static Configuration ConfigCommon(string path)
        {
            if (Config.TryGetValue(path, out Configuration configuration)) return configuration;
            string ConfigPath = $"{AppDomain.CurrentDomain.BaseDirectory}{path}";
            var isFileExist = File.Exists(ConfigPath);
            if (isFileExist)
            {
                ExeConfigurationFileMap configFile = new ExeConfigurationFileMap();
                configFile.ExeConfigFilename = ConfigPath;
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);

               // var bb = ConfigurationManager.GetSection("addressconfig");

                var cc = config.GetSection("addressconfig") as AddressConfig;
                //var aa = config.GetSection("addressconfig") as ConfigSectionData;
                Config.Add(path, config);
                return config;
            }
            else
            {
                //string msg = string.Format("{0}路径下的文件未找到配置文件 ", ConfigPath);
                //throw new FileNotFoundException(msg);
                return default(Configuration);
            }
        }

        public static void RefreshConfig() => Config.Clear();
    }



}
