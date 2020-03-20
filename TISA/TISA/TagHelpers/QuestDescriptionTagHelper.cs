using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using TISA.Models;

namespace TISA.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("quest-description")]
    public class QuestDescriptionTagHelper : TagHelper
    {
        public Quest Quest { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";
            output.Content.Append($"{Quest.Name}");
            var rewardsAsString = new LinkedList<string>();
            if(Quest.GoldReward > 0)
            {
                rewardsAsString.AddLast($"{Quest.GoldReward} gold");
            }

            if (Quest.ExperienceReward > 0)
            {
                rewardsAsString.AddLast($"{Quest.ExperienceReward} EXP");
            }

            if(rewardsAsString.Count > 0)
            {
                output.Content.Append($" (rewards: {string.Join(", ", rewardsAsString)})");
            }
        }
    }
}
