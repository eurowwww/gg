using WinForm.GameRules;

namespace WinForm.Controls
{
    public class GameButton : Button
    {
        private ICollection<Line> lines;
        public GameButton(ICollection<Line> lines)
            : base()
        {
            this.lines = lines;
        }

        public void AddValue(GameElement gameElement)
        {
            foreach(var line in lines)
            {
                line.AddElement(gameElement);
            }
        }
    }
}