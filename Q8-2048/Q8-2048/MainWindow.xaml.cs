using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Q8_2048
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Grid gameGrid;
        private LogicGrid logicGrid;

        
        private int size = 3;
        public MainWindow()
        {

            
            InitializeComponent();
            CreateGameGrid();
            logicGrid = new LogicGrid(size);
            logicGrid.AddBlockToRandom();
            UpdateByLogicGrid();

        }
        private void CreateGameGrid()
        {
            Grid gameGrid = new Grid();
            
           
            for (int i = 0; i < size; i++)
            {
                gameGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < size; i++)
            {
                gameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
           
            parentGrid.Children.Add(gameGrid);
            this.gameGrid = gameGrid;
        }
        private void UpdateByLogicGrid()
        {
            gameGrid.Children.Clear();
            List<List<int>> map = logicGrid.GetGrid();
            for (int row = 0; row < size; row++)
            {
                for(int col = 0; col < size; col++)
                {
                    if(map[row][col] != 0)
                    {
                        Button btn = new Button();
                        btn.Style = (Style)Application.Current.Resources["GameSquare"];
                        btn.IsHitTestVisible = false;
                        btn.Content = map[row][col];
                        gameGrid.Children.Add(btn);

                        Grid.SetRow(btn,row);
                        Grid.SetColumn(btn,col);
                    }
                    
                }
            }
        }
        private void RaiseSwipeEvent(object sender, KeyEventArgs ke)
        {

            if (ke.Key == Key.W)
            {
                logicGrid.SwipeUp();
                Console.WriteLine("W");
            }
            else if (ke.Key == Key.S)
            {
                logicGrid.SwipeDown();
                Console.WriteLine("S");
            }
            else if (ke.Key == Key.A)
            {
                logicGrid.SwipeLeft();
                Console.WriteLine("A");
            }
            else
            {
                logicGrid.SwipeRight();
                Console.WriteLine("D");
            }
            logicGrid.AddBlockToRandom();
            UpdateByLogicGrid();
            if (logicGrid.Contains2048())
            {
                MessageBox.Show("Vyhrál jsi dosažením hodnoty 2048!");
                Environment.Exit(0);
            }
            else if(logicGrid.GetFreeIndexes().Count == 0 && !logicGrid.HasAdjacentValues())
            {
                MessageBox.Show("Prohrál jsi!");
                Environment.Exit(0);
            }


        }




    }
}
