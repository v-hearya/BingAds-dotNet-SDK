//=====================================================================================================================================================
// Bing Ads .NET SDK ver. 11.12
// 
// Copyright (c) Microsoft Corporation
// 
// All rights reserved. 
// 
// MS-PL License
// 
// This license governs use of the accompanying software. If you use the software, you accept this license. 
//  If you do not accept the license, do not use the software.
// 
// 1. Definitions
// 
// The terms reproduce, reproduction, derivative works, and distribution have the same meaning here as under U.S. copyright law. 
//  A contribution is the original software, or any additions or changes to the software. 
//  A contributor is any person that distributes its contribution under this license. 
//  Licensed patents  are a contributor's patent claims that read directly on its contribution.
// 
// 2. Grant of Rights
// 
// (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, 
//  each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, 
//  prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
// 
// (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, 
//  each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, 
//  sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.
// 
// 3. Conditions and Limitations
// 
// (A) No Trademark License - This license does not grant you rights to use any contributors' name, logo, or trademarks.
// 
// (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, 
//  your patent license from such contributor to the software ends automatically.
// 
// (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, 
//  and attribution notices that are present in the software.
// 
// (D) If you distribute any portion of the software in source code form, 
//  you may do so only under this license by including a complete copy of this license with your distribution. 
//  If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
// 
// (E) The software is licensed *as-is.* You bear the risk of using it. The contributors give no express warranties, guarantees or conditions.
//  You may have additional consumer rights under your local laws which this license cannot change. 
//  To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, 
//  fitness for a particular purpose and non-infringement.
//=====================================================================================================================================================

using System;
using System.Collections.Generic;
using Microsoft.BingAds.V12.Internal.Bulk;
using Microsoft.BingAds.V12.Internal.Bulk.Mappings;
using Microsoft.BingAds.V12.Internal.Bulk.Entities;
using Microsoft.BingAds.V12.CampaignManagement;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V12.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents an expanded text ad. 
    /// This class exposes the <see cref="BulkExpandedTextAd.ExpandedTextAd"/> property that can be read and written as fields of the Expanded Text Ad record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Expanded Text Ad</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkExpandedTextAd : BulkAd<ExpandedTextAd>
    {
        /// <summary>
        /// <para>
        /// The expanded text ad. 
        /// </para>
        /// </summary>
        public ExpandedTextAd ExpandedTextAd
        {
            get { return Ad; }
            set { Ad = value; }
        }

        private static readonly IBulkMapping<BulkExpandedTextAd>[] Mappings =
        {            
            new SimpleBulkMapping<BulkExpandedTextAd>(StringTable.TitlePart1,
                c => c.ExpandedTextAd.TitlePart1,
                (v, c) => c.ExpandedTextAd.TitlePart1 = v
            ),

            new SimpleBulkMapping<BulkExpandedTextAd>(StringTable.TitlePart2,
                c => c.ExpandedTextAd.TitlePart2,
                (v, c) => c.ExpandedTextAd.TitlePart2 = v
            ),

            new SimpleBulkMapping<BulkExpandedTextAd>(StringTable.TitlePart3,
                c => c.ExpandedTextAd.TitlePart3.ToOptionalBulkString(),
                (v, c) => c.ExpandedTextAd.TitlePart3 = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkExpandedTextAd>(StringTable.Path1,
                c => c.ExpandedTextAd.Path1.ToOptionalBulkString(),
                (v, c) => c.ExpandedTextAd.Path1 = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkExpandedTextAd>(StringTable.Path2,
                c => c.ExpandedTextAd.Path2.ToOptionalBulkString(),
                (v, c) => c.ExpandedTextAd.Path2 = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkExpandedTextAd>(StringTable.Text,
                c => c.ExpandedTextAd.Text,
                (v, c) => c.ExpandedTextAd.Text = v
            ),

            new SimpleBulkMapping<BulkExpandedTextAd>(StringTable.TextPart2,
                c => c.ExpandedTextAd.TextPart2.ToOptionalBulkString(),
                (v, c) => c.ExpandedTextAd.TextPart2 = v.GetValueOrEmptyString()
            ),

            new SimpleBulkMapping<BulkExpandedTextAd>(StringTable.Domain,
                c => c.ExpandedTextAd.Domain.ToOptionalBulkString(),
                (v, c) => c.ExpandedTextAd.Domain = v.GetValueOrEmptyString()
            ),
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            ExpandedTextAd = new ExpandedTextAd { Type = AdType.ExpandedText };

            base.ProcessMappingsFromRowValues(values);

            values.ConvertToEntity(this, Mappings);
        }


        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(ExpandedTextAd, "ExpandedTextAd");

            base.ProcessMappingsToRowValues(values, excludeReadonlyData);

            this.ConvertToValues(values, Mappings);
        }
    }
}
