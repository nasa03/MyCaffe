﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyCaffe.basecode;
using System.ComponentModel;

/// <summary>
/// The MyCaffe.param.beta parameters are used by the MyCaffe.layer.beta layers.
/// </summary>
/// <remarks>
/// Using parameters within the MyCaffe.layer.beta namespace are used by layers that require the MyCaffe.layers.beta.dll.
/// 
/// Centroids:
/// @see [A New Loss Function for CNN Classifier Based on Pre-defined Evenly-Distributed Class Centroids](https://arxiv.org/abs/1904.06008) by Qiuyu Zhu, Pengju Zhang, and Xin Ye, arXiv:1904.06008, 2019.
/// 
/// KNN:
/// @see [Constellation Loss: Improving the efficiency of deep metric learning loss functions for optimal embedding](https://arxiv.org/abs/1905.10675) by Alfonso Medela and Artzai Picon, arXiv:1905.10675, 2019
/// </remarks>
namespace MyCaffe.param.beta
{
    /// <summary>
    /// Specifies the parameters for the DecodeLayer and the AccuracyEncodingLayer.
    /// </summary>
    public class DecodeParameter : LayerParameterBase 
    {
        int m_nTargetIterationStart = 300;
        int m_nTargetIterationEnd = 500;
        bool m_bOutputCentroids = false;
        int m_nActiveLabelCount = 0;
        TARGET m_target = TARGET.CENTROID;
        int m_nK = 5;

        /// <summary>
        /// Defines the target type.
        /// </summary>
        public enum TARGET
        {
            /// <summary>
            /// Specifies to use the centroid as the target.
            /// </summary>
            CENTROID,
            /// <summary>
            /// Specifies to use the k-nearest neighbor as the target.
            /// </summary>
            KNN
        }

        /** @copydoc LayerParameterBase */
        public DecodeParameter()
        {
        }

        /// <summary>
        /// Specifies the starting iteration where observed items are used to calculate the centroid for each label, before this value, the targets should not be used for their calculation is not complete (default = 300).
        /// </summary>
        [Description("Specifies the starting iteration where observed items are used to calculate the target for each label, before this value, the targets are set to 0 and should not be used (default = 300).")]
        public int target_iteration_start
        {
            get { return m_nTargetIterationStart; }
            set { m_nTargetIterationStart = value; }
        }

        /// <summary>
        /// Specifies the ending iteration where observed items are used to calculate the centroid for each label, after this value, the previously calculated targets are returned (default = 500).
        /// </summary>
        [Description("Specifies the ending iteration where observed items are used to calculate the target for each label, after this value, the previously calculated targets are returned (default = 500).")]
        public int target_iteration_end
        {
            get { return m_nTargetIterationEnd; }
            set { m_nTargetIterationEnd = value; }
        }

        /// <summary>
        /// Optionally, specifies to output the centroids in top[1] (default = false).
        /// </summary>
        [Description("Optionally, specifies to output the centroids in top[1] (default = false).")]
        public bool output_centroids
        {
            get { return m_bOutputCentroids; }
            set { m_bOutputCentroids = value; }
        }

        /// <summary>
        /// Optionally, specifies a number of active labels that are less than the actual label count - this is used when only a subset of the labels within the label range are actually used (default = 0, which then expects all labels).
        /// </summary>
        [Description("Optionally, specifies a number of active labels that are less than the actual label count - this is used when only a subset of the labels within the label range are actually used (default = 0, which then expects all labels).")]
        public int active_label_count
        {
            get { return m_nActiveLabelCount; }
            set { m_nActiveLabelCount = value; }
        }

        /// <summary>
        /// Optionally, specifies the target type to use (default = CENTROID).
        /// </summary>
        [Description("Optionally, specifies the target type to use (default = CENTROID).")]
        public TARGET target
        {
            get { return m_target; }
            set { m_target = value; }
        }

        /// <summary>
        /// Optionally, specifies the K value to use with the KNN target (default = 5).
        /// </summary>
        [Description("Optionally, specifies the K value to use with the KNN target (default = 5).")]
        public int k
        {
            get { return m_nK; }
            set { m_nK = value; }
        }

        /** @copydoc LayerParameterBase::Load */
        public override object Load(System.IO.BinaryReader br, bool bNewInstance = true)
        {
            RawProto proto = RawProto.Parse(br.ReadString());
            DecodeParameter p = FromProto(proto);

            if (!bNewInstance)
                Copy(p);

            return p;
        }

        /** @copydoc LayerParameterBase::Copy */
        public override void Copy(LayerParameterBase src)
        {
            DecodeParameter p = (DecodeParameter)src;
            m_nTargetIterationStart = p.m_nTargetIterationStart;
            m_nTargetIterationEnd = p.m_nTargetIterationEnd;
            m_bOutputCentroids = p.m_bOutputCentroids;
            m_nActiveLabelCount = p.m_nActiveLabelCount;
            m_target = p.m_target;
            m_nK = p.m_nK;
        }

        /** @copydoc LayerParameterBase::Clone */
        public override LayerParameterBase Clone()
        {
            DecodeParameter p = new DecodeParameter();
            p.Copy(this);
            return p;
        }

        /** @copydoc LayerParameterBase::ToProto */
        public override RawProto ToProto(string strName)
        {
            RawProtoCollection rgChildren = new RawProtoCollection();

            rgChildren.Add("target_iteration_start", target_iteration_start.ToString());
            rgChildren.Add("target_iteration_end", target_iteration_end.ToString());
            rgChildren.Add("output_centroids", output_centroids.ToString());
            rgChildren.Add("target", target.ToString());

            if (active_label_count > 0)
                rgChildren.Add("active_label_count", active_label_count.ToString());

            if (target == TARGET.KNN)
                rgChildren.Add("k", k.ToString());

            return new RawProto(strName, "", rgChildren);
        }

        /// <summary>
        /// Parses the parameter from a RawProto.
        /// </summary>
        /// <param name="rp">Specifies the RawProto to parse.</param>
        /// <returns>A new instance of the parameter is returned.</returns>
        public static DecodeParameter FromProto(RawProto rp)
        {
            string strVal;
            DecodeParameter p = new DecodeParameter();

            if ((strVal = rp.FindValue("target_iteration_start")) != null)
                p.target_iteration_start = int.Parse(strVal);

            if ((strVal = rp.FindValue("target_iteration_end")) != null)
                p.target_iteration_end = int.Parse(strVal);

            if ((strVal = rp.FindValue("output_centroids")) != null)
                p.output_centroids = bool.Parse(strVal);

            if ((strVal = rp.FindValue("active_label_count")) != null)
                p.active_label_count = int.Parse(strVal);

            if ((strVal = rp.FindValue("target")) != null)
            {
                if (strVal == TARGET.KNN.ToString())
                    p.target = TARGET.KNN;
            }

            if ((strVal = rp.FindValue("k")) != null)
                p.k = int.Parse(strVal);

            return p;
        }
    }
}
