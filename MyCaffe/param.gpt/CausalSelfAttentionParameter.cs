﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using MyCaffe.basecode;

namespace MyCaffe.param.gpt
{
    /// <summary>
    /// Specifies the parameters for the CausalSelfAttentionLayer.
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class CausalSelfAttentionParameter : LayerParameterBase
    {
        uint m_nHeads = 6;
        uint m_nEmbed = 192;
        double m_dfAttnDropout;
        double m_dfResidDropout;
        uint m_nBlockSize = 128;
        uint m_nLayers = 6;

        /** @copydoc LayerParameterBase */
        public CausalSelfAttentionParameter()
        {
        }

        /// <summary>
        /// The number of layers (transformer blocks) used.
        /// </summary>
        [Description("Specifies number of layers (transformer blocks) used.")]
        public uint layers
        {
            get { return m_nLayers; }
            set { m_nLayers = value; }
        }

        /// <summary>
        /// The number of heads used.
        /// </summary>
        [Description("Specifies number of heads used.")]
        public uint heads
        {
            get { return m_nHeads; }
            set { m_nHeads = value; }
        }

        /// <summary>
        /// Specifies size of the embed.
        /// </summary>
        public uint embed
        {
            get { return m_nEmbed; }
            set { m_nEmbed = value; }
        }

        /// <summary>
        /// Specifies size of the block.
        /// </summary>
        public uint block_size
        {
            get { return m_nBlockSize; }
            set { m_nBlockSize = value; }
        }
        
        /// <summary>
        /// Specifies dropout probability used on the attention weights.
        /// </summary>
        public double attn_dropout
        {
            get { return m_dfAttnDropout; }
            set { m_dfAttnDropout = value; }
        }

        /// <summary>
        /// Specifies dropout probability used on the residual weights.
        /// </summary>
        public double resid_dropout
        {
            get { return m_dfResidDropout; }
            set { m_dfResidDropout = value; }
        }

        /** @copydoc LayerParameterBase::Load */
        public override object Load(System.IO.BinaryReader br, bool bNewInstance = true)
        {
            RawProto proto = RawProto.Parse(br.ReadString());
            CausalSelfAttentionParameter p = FromProto(proto);

            if (!bNewInstance)
                Copy(p);

            return p;
        }

        /** @copydoc LayerParameterBase::Copy */
        public override void Copy(LayerParameterBase src)
        {
            CausalSelfAttentionParameter p = (CausalSelfAttentionParameter)src;

            m_nLayers = p.layers;
            m_nHeads = p.heads;
            m_nEmbed = p.embed;
            m_nBlockSize = p.block_size;
            m_dfAttnDropout = p.attn_dropout;
            m_dfResidDropout = p.resid_dropout;
        }

        /** @copydoc LayerParameterBase::Clone */
        public override LayerParameterBase Clone()
        {
            CausalSelfAttentionParameter p = new CausalSelfAttentionParameter();
            p.Copy(this);
            return p;
        }

        /// <summary>
        /// Convert the parameter into a RawProto.
        /// </summary>
        /// <param name="strName">Specifies the name to associate with the RawProto.</param>
        /// <returns>The new RawProto is returned.</returns>
        public override RawProto ToProto(string strName)
        {
            RawProtoCollection rgChildren = new RawProtoCollection();

            rgChildren.Add("layers", layers.ToString());
            rgChildren.Add("heads", heads.ToString());
            rgChildren.Add("embed", embed.ToString());
            rgChildren.Add("block_size", block_size.ToString());
            rgChildren.Add("attn_dropout", attn_dropout.ToString());
            rgChildren.Add("resid_dropout", resid_dropout.ToString());

            return new RawProto(strName, "", rgChildren);
        }

        /// <summary>
        /// Parses the parameter from a RawProto.
        /// </summary>
        /// <param name="rp">Specifies the RawProto to parse.</param>
        /// <returns>A new instance of the parameter is returned.</returns>
        public static CausalSelfAttentionParameter FromProto(RawProto rp)
        {
            string strVal;
            CausalSelfAttentionParameter p = new CausalSelfAttentionParameter();

            if ((strVal = rp.FindValue("layers")) != null)
                p.layers = uint.Parse(strVal);
            
            if ((strVal = rp.FindValue("heads")) != null)
                p.heads = uint.Parse(strVal);
            
            if ((strVal = rp.FindValue("embed")) != null)
                p.embed = uint.Parse(strVal);

            if ((strVal = rp.FindValue("block_size")) != null)
                p.block_size = uint.Parse(strVal);

            if ((strVal = rp.FindValue("attn_dropout")) != null)
                p.attn_dropout = double.Parse(strVal);

            if ((strVal = rp.FindValue("resid_dropout")) != null)
                p.resid_dropout = double.Parse(strVal);

            return p;
        }
    }
}
