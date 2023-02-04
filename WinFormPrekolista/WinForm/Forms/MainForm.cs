using Accessibility;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using WinForm.Controls;
using WinForm.Engine;
using WinForm.GameRules;

namespace WinForm
{
    public partial class MainForm : Form
    {
        private int drawLines = 0;
        private bool xo = false;
        private List<GameButton> buttons = new();
        internal MainForm(UIApplicationContext context) : this()
        {
            UserNameLabel.Text = context.UserName;
        }
        public MainForm()
        {
            InitializeComponent();
            CreateField();
        }

        private void CreateField()
        {
            var lines = Enumerable
                .Range(0,8)
                .Select(x=> {

                    var line = new Line();
                    line.OnWin += GameWin;
                    line.OnDraw += GameDraw;
                    return line;
                    
                    })   
                .ToList();


            var dict = new Dictionary<int, int[]>
            {
                [0]=new int[] {0,3,6},
                [1]=new int[] {0,4},
                [2]=new int[] {5,0,7},
                [3]=new int[] {3,1},
                [4]=new int[] {4,1,6,7},
                [5]=new int[] {5,1},
                [6]=new int[] {2,3,7},
                [7]=new int[] {4,2},
                [8]=new int[] {5,2,6},
            };

            for (int i = 0, j = 0; i < 9; i++, j++)
            {
                if (j == 3)
                {
                    j = 0;
                }
                var x = 240 + j * 100;
                var y = 50 + (i / 3) * 100;

                var buttonLines = dict[i]
                    .Select(x => lines[x])
                    .ToList();


                var button = new GameButton(buttonLines);
                button.Location = new System.Drawing.Point(x, y);
                button.Name = $"button1{i}";
                button.Size = new System.Drawing.Size(100, 100);
                button.TabIndex = 1;
                button.Text = "";
                button.Font = new Font(FontFamily.GenericMonospace, 21, GraphicsUnit.Point);
                button.ForeColor = Color.Red;
                button.UseVisualStyleBackColor = true;
                button.Click += button_Click;
                drawLines = 0;
                xo = false;
                buttons.Add(button);
                Controls.Add(button);
            }
        }
        private void Restart()
        {
            foreach(var button in buttons )
            {
                Controls.Remove(button);
            }
            buttons.Clear();
            CreateField();
        }
            private void GameWin(GameElement gameElement)
        {
            MessageBox.Show($"Победил игрок {gameElement}");
            Restart();
        }
        private void GameDraw(GameElement gameElement)
        {
            drawLines += 1;
            if (drawLines == 8)
            {
                MessageBox.Show($"Галсутк");
                Restart();
            }
        }

        private void button_Click(object? sender, EventArgs e)
        {
            if (sender is not GameButton button)
            {
                return;
            }
            var value = xo 
                ? GameElement.Cross 
                : GameElement.Zero;
            button.Text = xo ? "X" : "O";
            xo = !xo;
            button.Click -= button_Click;
            button.AddValue(value);
        }
    }
}