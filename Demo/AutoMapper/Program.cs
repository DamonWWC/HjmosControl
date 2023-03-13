// See https://aka.ms/new-console-template for more information
using AutoMapper;
using AutoMapperDemo;


List<FloodInformation> FloodSource = new List<FloodInformation>
                    {
                new FloodInformation{StationName="谢家桥"},
                new FloodInformation{StationName ="象鼻窝"},
                new FloodInformation{StationName ="中塘",IsTransferStation=true,TransferLine="2"},
                new FloodInformation{StationName ="师范西校区"},
                new FloodInformation{StationName ="长庆" },
                new FloodInformation{StationName ="和馨园"},
                new FloodInformation{StationName="长丰"},
                new FloodInformation{StationName ="麓谷体育园"},
                new FloodInformation{StationName ="麓谷"},
                new FloodInformation{StationName ="涧塘"},
                new FloodInformation{StationName ="湖南工商大学" },
                new FloodInformation{StationName ="白鸽咀"},
                new FloodInformation{StationName ="湘雅三医院"},
                new FloodInformation{StationName ="六沟垅",IsTransferStation=true,TransferLine="4"},
                new FloodInformation{StationName ="文昌阁" ,IsTransferStation=true},
                new FloodInformation{StationName ="湘雅医院站"},
                new FloodInformation{StationName ="烈士公园南" },
                new FloodInformation{StationName ="迎宾路口",IsTransferStation=true,TransferLine="2"},
                new FloodInformation{StationName ="窖岭湘雅二医院"},
                new FloodInformation{StationName ="朝阳村",IsTransferStation=true,TransferLine="3"},
                new FloodInformation{StationName ="芙蓉区政府" ,IsTransferStation=true,TransferLine="5"},
                new FloodInformation{StationName ="人民东路",IsTransferStation=true,TransferLine="2"},
                new FloodInformation{StationName ="花轿" },
                new FloodInformation{StationName ="隆平水稻博物馆",HasFloodGate=true},
                new FloodInformation{StationName ="农科院农大"},
                new FloodInformation{StationName ="东湖"},
                new FloodInformation{StationName ="韶光" },
                new FloodInformation{StationName ="龙华"},
                new FloodInformation{StationName ="檀木桥" },
                new FloodInformation{StationName ="曹家坪"},
                new FloodInformation{StationName ="龙峰"},
                new FloodInformation{StationName ="大路村"},
                new FloodInformation{StationName ="木马塅" },
                new FloodInformation{StationName ="黄花机场"},
            };

var source = new Source
{
    Value1 = 5,
    Value2 = 7
};

var configuration = new MapperConfiguration(cfg =>
   cfg.CreateMap<Source, Destination>()
    );



var mapper = configuration.CreateMapper();
var result = mapper.Map<Source, Destination>(source);



var sss = source.MapTo<Destination, Source>();
var ss = FloodSource.MapToList<LineInformation1, FloodInformation>();


Console.WriteLine("Hello, World!");
