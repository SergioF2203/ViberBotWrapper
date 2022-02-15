using System.Collections.Generic;

namespace Models
{
    public class Keyboard
    {
        public string Type { get; set; } = "keyboard";
        public bool DefaultHeight { get; set; }
        public List<Button> Buttons { get; set; }

        public Keyboard(params Button[] buttons)
        {
            Buttons = new();

            foreach (var button in buttons)
                Buttons.Add(button);
        }
    }
}
