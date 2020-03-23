using Microsoft.AspNetCore.Razor.TagHelpers;
using TISA.Services;

namespace TISA.TagHelpers
{
    /// <summary>
    /// This tag helper automatically makes a span with the player-name in it, if logged in.
    /// </summary>
    [HtmlTargetElement("player-name", TagStructure = TagStructure.WithoutEndTag)]
    public class PlayerNameTagHelper : TagHelper
    {
        private readonly IPlayerService _playerService;

        public PlayerNameTagHelper(IPlayerService playerService)
        {
            _playerService = playerService;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!_playerService.IsPlayerDefined)
            {
                output.SuppressOutput();
                return;
            }
            output.TagName = "span";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.Append(_playerService.Player.Name);
            base.Process(context, output);
        }
    }
}
