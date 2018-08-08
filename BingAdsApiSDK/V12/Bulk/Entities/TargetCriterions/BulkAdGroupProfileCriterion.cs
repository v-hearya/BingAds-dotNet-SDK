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
using Microsoft.BingAds.V12.CampaignManagement;
using Microsoft.BingAds.V12.Internal.Bulk;
using Microsoft.BingAds.V12.Internal.Bulk.Entities;
using Microsoft.BingAds.V12.Internal.Bulk.Mappings;

namespace Microsoft.BingAds.V12.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// This class exposes the <see cref="BiddableAdGroupCriterion"/> property with ProfileCriterion that can be read and written in a bulk file.
    /// </para>
    /// <para>For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Bulk File Schema</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public abstract class BulkAdGroupProfileCriterion : SingleRecordBulkEntity
    {
        /// <summary>
        /// Defines a Biddable Ad Group Criterion.
        /// </summary>
        public BiddableAdGroupCriterion BiddableAdGroupCriterion { get; set; }

        /// <summary>
        /// The name of the campaign that contains the ad group.
        /// Corresponds to the 'Campaign' field in the bulk file. 
        /// </summary>
        public string CampaignName { get; set; }

        /// <summary>
        /// The name of the ad group that contains the criterion.
        /// Corresponds to the 'Ad Group' field in the bulk file.
        /// </summary>
        public string AdGroupName { get; set; }

        /// <summary>
        /// The display name of the profile.
        /// Corresponds to the 'Profile' field in the bulk file.
        /// </summary>
        public string ProfileName { get; set; }

        private static readonly IBulkMapping<BulkAdGroupProfileCriterion>[] Mappings =
        {
            new SimpleBulkMapping<BulkAdGroupProfileCriterion>(StringTable.Status,
                c => c.BiddableAdGroupCriterion.Status.ToBulkString(),
                (v, c) => c.BiddableAdGroupCriterion.Status = v.ParseOptional<AdGroupCriterionStatus>()
            ),

            new SimpleBulkMapping<BulkAdGroupProfileCriterion>(StringTable.Id,
                c => c.BiddableAdGroupCriterion.Id.ToBulkString(),
                (v, c) => c.BiddableAdGroupCriterion.Id = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkAdGroupProfileCriterion>(StringTable.ParentId,
                c => c.BiddableAdGroupCriterion.AdGroupId.ToBulkString(true),
                (v, c) => c.BiddableAdGroupCriterion.AdGroupId = v.Parse<long>()
            ),

            new SimpleBulkMapping<BulkAdGroupProfileCriterion>(StringTable.Campaign,
                c => c.CampaignName,
                (v, c) => c.CampaignName = v
            ),

            new SimpleBulkMapping<BulkAdGroupProfileCriterion>(StringTable.AdGroup,
                c => c.AdGroupName,
                (v, c) => c.AdGroupName = v
            ),

            new SimpleBulkMapping<BulkAdGroupProfileCriterion>(StringTable.Profile,
                c => c.ProfileName,
                (v, c) => c.ProfileName = v
            ),

            new SimpleBulkMapping<BulkAdGroupProfileCriterion>(StringTable.BidAdjustment,
                c =>
                {
                    var criterion = c.BiddableAdGroupCriterion as BiddableAdGroupCriterion;

                    if (criterion == null) return null;

                    var multiplicativeBid = criterion.CriterionBid as BidMultiplier;

                    return multiplicativeBid?.Multiplier.ToBulkString();
                },
                (v, c) =>
                {
                    var criterion = c.BiddableAdGroupCriterion as BiddableAdGroupCriterion;

                    if (criterion == null) return;

                    double? multiplier = v.ParseOptional<double>();
                    if (multiplier != null)
                    {
                        ((BidMultiplier) criterion.CriterionBid).Multiplier = multiplier.Value;
                    }
                    else
                    {
                        criterion.CriterionBid = null;
                    }
                }
            ),

            new SimpleBulkMapping<BulkAdGroupProfileCriterion>(StringTable.ProfileId,
                c =>
                {
                    var profileCriterion = c.BiddableAdGroupCriterion.Criterion as ProfileCriterion;

                    return profileCriterion?.ProfileId.ToBulkString();
                },
                (v, c) =>
                {
                    var profileCriterion = c.BiddableAdGroupCriterion.Criterion as ProfileCriterion;

                    if (profileCriterion != null && v.ParseOptional<long>() != null)
                    {
                        profileCriterion.ProfileId = v.Parse<long>();
                    }
                }
            ),
        };

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(BiddableAdGroupCriterion, typeof(BiddableAdGroupCriterion).Name);

            this.ConvertToValues(values, Mappings);
        }

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            BiddableAdGroupCriterion = new BiddableAdGroupCriterion
            {
                Criterion = new ProfileCriterion()
                {
                    Type = typeof(ProfileCriterion).Name,
                    ProfileType = GetProfileType()
                },
                CriterionBid = new BidMultiplier
                {
                    Type = typeof(BidMultiplier).Name,
                },
                Type = typeof(BiddableAdGroupCriterion).Name
            };

            values.ConvertToEntity(this, Mappings);
        }

        protected virtual ProfileType GetProfileType()
        {
            throw new NotImplementedException();
        }
    }
}