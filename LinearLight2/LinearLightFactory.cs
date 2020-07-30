using System;
using LinearLight2.Light;
using LinearLight2.Light.Utils;
using LinearLight2.Modbus;

namespace LinearLight2
{
    public class LinearLightFactory
    {
        private readonly IModbusMaster master;

        private ILinearLightV101 identificationLiLi;

        private ILinearLightV101 IdentificationLiLi =>
            identificationLiLi ?? (identificationLiLi = new LinearLightV101(master, 1));

        public LinearLightFactory(IModbusMaster master)
        {
            this.master = master;
        }

        public T CreateLinearLight<T>() where T : class, ILinearLight
        {
            var segments = GetSegmentCount();
            return CreateLinearLight<T>(segments);
        }

        private T CreateLinearLight<T>(int segments) where T : class, ILinearLight
        {
            var protocol = GetProtocol();
            T returnLight;
            switch (protocol)
            {
                case LinearLightV101.CompatibleProtocolVersion:
                    returnLight = new LinearLightV101(master, segments) as T ??
                                  throw new InvalidCastException(
                                      $"{typeof(LinearLightV101)} does not implement {typeof(T)}.");
                    break;
                case LinearLightV102.CompatibleProtocolVersion:
                    returnLight = new LinearLightV102(master, segments) as T ??
                                  throw new InvalidCastException(
                                      $"{typeof(LinearLightV102)} does not implement {typeof(T)}.");
                    break;
                case LinearLightV103.CompatibleProtocolVersion:
                    returnLight = new LinearLightV103(master, segments) as T ??
                                  throw new InvalidCastException(
                                      $"{typeof(LinearLightV103)} does not implement {typeof(T)}.");
                    break;
                default:
                    if (MajorVersion(protocol) != MajorVersion(LinearLightV103.CompatibleProtocolVersion))
                        throw new Exception("Unsupported protocol.");
                    returnLight = new LinearLightV103(master, segments) as T ??
                                  throw new InvalidCastException(
                                      $"{typeof(LinearLightV103)} does not implement {typeof(T)}.");
                    break;
            }

            master.MillisecondsDelayBetweenTransmits = returnLight.MillisecondsBetweenTransmits;
            return returnLight;
        }

        private string MajorVersion(string protocolVersion)
        {
            return protocolVersion.Split('.')[0];
        }

        private int GetSegmentCount()
        {
            var productNumber = IdentificationLiLi.Segments[0].ProductNumber;
            var segmentCount = ProductNumberUtils.GetSegmentCount(productNumber);
            return segmentCount;
        }

        private string GetProtocol()
        {
            var protocolVersion = IdentificationLiLi.Segments[0].ProtocolVersion;
            return protocolVersion;
        }
    }
}