﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagTool.Common;

namespace TagTool.Tags
{
    public class TagGroupNew : TagStructure, IEquatable<TagGroupNew>
    {
        public Tag Tag;
        public Tag ParentTag;
        public Tag GrandParentTag;

        public static readonly TagGroupNew None = new TagGroupNew(new Tag(-1), new Tag(-1), new Tag(-1));

        public TagGroupNew() : this(Tag.Null, Tag.Null, Tag.Null){}

        public TagGroupNew(Tag tag, Tag parentTag, Tag grandparentTag)
        {
            Tag = tag;
            ParentTag = parentTag;
            GrandParentTag = grandparentTag;
        }

        public TagGroupNew(Tag tag, Tag parentTag)
        {
            Tag = tag;
            ParentTag = parentTag;
            GrandParentTag = Tag.Null;
        }

        public TagGroupNew(Tag tag) 
        {
            Tag = tag;
            ParentTag = Tag.Null;
            GrandParentTag = Tag.Null;
        }

        /// <summary>
        /// Determines whether this group is a subgroup of another group.
        /// </summary>
        /// <param name="groups">The group to check.</param>
        /// <returns><c>true</c> if this group is a subgroup of the other group.</returns>
        public bool BelongsTo(params TagGroupNew[] groups)
        {
            return BelongsTo(groups.Select(group => group.Tag).ToArray());
        }

        /// <summary>
        /// Determines whether this group is a subgroup of another group.
        /// </summary>
        /// <param name="groupTags">The group tag to check, as a 4-character string.</param>
        /// <returns><c>true</c> if this group is a subgroup of the group tag.</returns>
        public bool BelongsTo(params string[] groupTags)
        {
            return BelongsTo(groupTags.Select(groupTag => new Tag(groupTag)).ToArray());
        }

        /// <summary>
        /// Determines whether this group is a subgroup of another group.
        /// </summary>
        /// <param name="groupTags">The group tag to check.</param>
        /// <returns><c>true</c> if this group is a subgroup of the group tag.</returns>
        public bool BelongsTo(params Tag[] groupTags)
        {
            foreach (var groupTag in groupTags)
                if (Tag.Equals(groupTag) || ParentTag.Equals(groupTag) || GrandParentTag.Equals(groupTag))
                    return true;

            return false;
        }

        public bool Equals(TagGroupNew other)
        {
            return Tag == other.Tag && ParentTag == other.ParentTag && GrandParentTag == other.GrandParentTag;
        }

        public override bool Equals(object obj)
        {
            return obj is TagGroupNew && Equals((TagGroupNew)obj);
        }

        public static bool operator ==(TagGroupNew lhs, TagGroupNew rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(TagGroupNew lhs, TagGroupNew rhs)
        {
            return !(lhs == rhs);
        }

        public override int GetHashCode()
        {
            var result = 13;
            result = result * 17 + Tag.GetHashCode();
            result = result * 17 + ParentTag.GetHashCode();
            result = result * 17 + GrandParentTag.GetHashCode();
            return result;
        }

        public override string ToString() => Tag.ToString();
    }
}
