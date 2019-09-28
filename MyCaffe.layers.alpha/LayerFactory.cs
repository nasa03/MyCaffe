﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCaffe.basecode;
using MyCaffe.common;
using MyCaffe.db.image;
using MyCaffe.param;

/// <summary>
/// The MyCaffe.layers.alpha namespace contains all experimental layers that have a fluid and changing code base.
/// </summary>
namespace MyCaffe.layers.alpha
{
    /// <summary>
    /// The LayerFactor is responsible for creating all layers implemented in the MyCaffe.layers.ssd namespace.
    /// </summary>
    public class LayerFactory : ILayerCreator
    {
        /// <summary>
        /// Create the layers when using the <i>double</i> base type.
        /// </summary>
        /// <param name="cuda">Specifies the connection to the low-level CUDA interfaces.</param>
        /// <param name="log">Specifies the output log.</param>
        /// <param name="p">Specifies the layer parameter.</param>
        /// <param name="evtCancel">Specifies the cancellation event.</param>
        /// <param name="imgDb">Specifies an interface to the image database, who's use is optional.</param>
        /// <returns>If supported, the layer is returned, otherwise <i>null</i> is returned.</returns>
        public Layer<double> CreateDouble(CudaDnn<double> cuda, Log log, LayerParameter p, CancelEvent evtCancel, IXImageDatabase imgDb)
        {
            switch (p.type)
            {
                case LayerParameter.LayerType.BINARYHASH:
                    return new BinaryHashLayer<double>(cuda, log, p);

                case LayerParameter.LayerType.TRIPLET_LOSS_SIMPLE:
                    return new TripletLossSimpleLayer<double>(cuda, log, p);

                case LayerParameter.LayerType.TRIPLET_LOSS:
                    return new TripletLossLayer<double>(cuda, log, p);

                case LayerParameter.LayerType.TRIPLET_SELECT:
                    return new TripletSelectLayer<double>(cuda, log, p);

                case LayerParameter.LayerType.TRIPLET_DATA:
                    return new TripletDataLayer<double>(cuda, log, p, imgDb, evtCancel);

                default:
                    return null;
            }
        }

        /// <summary>
        /// Create the layers when using the <i>float</i> base type.
        /// </summary>
        /// <param name="cuda">Specifies the connection to the low-level CUDA interfaces.</param>
        /// <param name="log">Specifies the output log.</param>
        /// <param name="p">Specifies the layer parameter.</param>
        /// <param name="evtCancel">Specifies the cancellation event.</param>
        /// <param name="imgDb">Specifies an interface to the image database, who's use is optional.</param>
        /// <returns>If supported, the layer is returned, otherwise <i>null</i> is returned.</returns>
        public Layer<float> CreateSingle(CudaDnn<float> cuda, Log log, LayerParameter p, CancelEvent evtCancel, IXImageDatabase imgDb)
        {
            switch (p.type)
            {
                case LayerParameter.LayerType.BINARYHASH:
                    return new BinaryHashLayer<float>(cuda, log, p);

                case LayerParameter.LayerType.TRIPLET_LOSS_SIMPLE:
                    return new TripletLossSimpleLayer<float>(cuda, log, p);

                case LayerParameter.LayerType.TRIPLET_LOSS:
                    return new TripletLossLayer<float>(cuda, log, p);

                case LayerParameter.LayerType.TRIPLET_SELECT:
                    return new TripletSelectLayer<float>(cuda, log, p);

                case LayerParameter.LayerType.TRIPLET_DATA:
                    return new TripletDataLayer<float>(cuda, log, p, imgDb, evtCancel);

                default:
                    return null;
            }
        }
    }
}
