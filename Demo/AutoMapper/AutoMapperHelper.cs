using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapperDemo
{
    public static class AutoMapperHelper
    {
        public static TDestination MapTo<TDestination, TSource>(this TSource source)
        {
            if (source == null) return default(TDestination);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource,TDestination>());
           
            var mapper = config.CreateMapper();
            return mapper.Map<TDestination>(source);
        }

        public static IEnumerable<TDestination> MapToList<TDestination, TSource>(this IEnumerable<TSource> sources) where TDestination : class where TSource : class
        {
            if (sources == null) return new List<TDestination>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource,TDestination >());
            var mapper = config.CreateMapper();
            return mapper.Map<List<TDestination>>(sources);
        }
    }



    public class Source
    {
        public int Value1 { get; set; }
        public int Value2 { get; set; }
    }

    public class Destination
    {
        public int Total { get; set; }
        public int Value1 { get; set; }
    }


    public interface IValueResolver<in TSource, in TDestination, TDestMember>
    {
        TDestMember Resolve(TSource source, TDestination destination, TDestMember destMember, ResolutionContext context);
    }

    public class CustomResolver : IValueResolver<Source, Destination, int>
    {
        public int Resolve(Source source, Destination destination, int member, ResolutionContext context)
        {
            return source.Value1 + source.Value2;
        }
    }

    public class FloodInformation 
    {
        public string StationName { get; set; }
        public bool IsTransferStation { get; set; }
        public string TransferLine { get; set; }
        public bool HasFloodGate { get; set; }
        public bool IsLast { get; set; }

     
    }
    public class LineInformation1
    {
        /// <summary>
        /// 线路编号
        /// </summary>

        public string LineNo { get; set; }

        /// <summary>
        /// 线路名称
        /// </summary>

        public string LineName { get; set; }

        /// <summary>
        /// 车站编码（每条线路唯一车站编码）
        /// </summary>

        public string StationCode { get; set; }

        /// <summary>
        /// 车站编码(换乘站该编码相同)
        /// </summary>

        public string NccStationCode { get; set; }

        /// <summary>
        /// 车站名称
        /// </summary>

        public string StationName { get; set; }

        /// <summary>
        /// 车站排序号
        /// </summary>

        public string StationNum { get; set; }

        /// <summary>
        /// 综合监控端的线路Id
        /// </summary>

        public string DpLineId { get; set; }


        /// <summary>
        /// 综合监控端的车站Id
        /// </summary>

        public string DpLocationId { get; set; }

        /// <summary>
        /// 车站所在区
        /// </summary>

        public string RegionName { get; set; }

        /// <summary>
        /// 车站类型
        /// </summary>

        public string StationType { get; set; }

        /// <summary>
        /// 换乘站线路编号
        /// </summary>

        public string TransferDpLineId { get; set; }

    }

}
