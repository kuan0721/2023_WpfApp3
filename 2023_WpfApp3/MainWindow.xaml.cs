using System;  // 匯入基礎的System命名空間，提供基本的類型和方法
using System.Linq;  // 匯入Linq，一種數據查詢語法，用於查詢、過濾和排序數據
using System.Windows;  // 匯入WPF的基礎命名空間，提供Window、Application等基礎類型
using System.Windows.Controls;  // 匯入WPF控制項，例如Button、Canvas、TextBox等
using System.Windows.Input;  // 匯入與輸入相關的類型，例如MouseEventArgs、Keyboard等
using System.Windows.Media;  // 匯入繪圖和多媒體相關的類型，例如Color、Brush等
using System.Windows.Shapes;  // 匯入用於繪製幾何形狀的類型，例如Line、Rectangle、Ellipse等

namespace _2023_WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
   // MainWindow類別繼承自WPF的Window類別，用於GUI交互
public partial class MainWindow : Window
{
    // 宣告和初始化繪圖屬性變數
    String shapeType = "Line";  // 表示當前選擇的圖形類型，預設為“Line”（即直線）
    Color strokeColor = Colors.Blue;  // 表示線條的顏色，預設為藍色
    Color fillColor = Colors.Red;  // 表示填充顏色，預設為紅色
    int strokeThickness = 1;  // 表示線條粗細，預設值為1

    Point start, dest;  // 表示繪圖的起點和終點座標

    // 主窗口的建構函數
    public MainWindow()
    {
        InitializeComponent();  // 初始化XAML組件和控制項
        strokeColorPicker.SelectedColor = strokeColor;  // 設置顏色選擇器的預設選項為strokeColor
        fillColorPicker.SelectedColor = fillColor;  // 設置顏色選擇器的預設選項為fillColor
        }

    // 當使用者選擇繪圖形狀時觸發的事件
    private void ShapeButton_Click(object sender, RoutedEventArgs e)
    {
        var targetRadioButton = sender as RadioButton;  // 取得觸發此事件的RadioButton物件
        shapeType = targetRadioButton.Tag.ToString();  // 將該物件的Tag屬性轉為字串，並設定為當前選擇的圖形類型
    }

    // 當使用者移動畫筆粗細的滑塊時觸發的事件
    private void strokeThicknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        strokeThickness = Convert.ToInt32(strokeThicknessSlider.Value);  // 從滑塊取得值，轉為整數，並設定為當前線條粗細
    }

    // 當滑鼠在Canvas組件上移動時觸發的事件
    private void myCanvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
    {
        dest = e.GetPosition(myCanvas);  // 取得滑鼠在Canvas上的當前座標，並設定為終點座標
        DisplayStatus();  // 此函數未給出，可能用於在界面上顯示當前狀態或座標等資訊

        // 檢查滑鼠左鍵是否被按下，若是，則進行繪圖操作
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            Point origin = new Point  // 計算圖形的起點座標
            {
                X = Math.Min(start.X, dest.X),
                Y = Math.Min(start.Y, dest.Y)
            };
            double width = Math.Abs(dest.X - start.X);  // 計算圖形的寬度
            double height = Math.Abs(dest.Y - start.Y);  // 計算圖形的高度

            // 根據所選圖形類型進行不同的繪圖操作
            switch (shapeType)
            {
                case "Line":  // 若為直線
                    var line = myCanvas.Children.OfType<Line>().LastOrDefault();  // 在Canvas的子物件中找到最後一個Line物件
                    line.X2 = dest.X;  // 設定該Line物件的終點x座標
                    line.Y2 = dest.Y;  // 設定該Line物件的終點y座標
                    break;

                case "Rectangle":  // 若為矩形
                    var rect = myCanvas.Children.OfType<Rectangle>().LastOrDefault();  // 在Canvas的子物件中找到最後一個Rectangle物件
                    rect.Width = width;  // 設定該Rectangle物件的寬度
                    rect.Height = height;  // 設定該Rectangle物件的高度
                    rect.SetValue(Canvas.LeftProperty, origin.X);  // 設定該Rectangle物件的起點x座標
                    rect.SetValue(Canvas.TopProperty, origin.Y);  // 設定該Rectangle物件的起點y座標
                    break;

                case "Ellipse":  // 若為橢圓
                    var ellipse = myCanvas.Children.OfType<Ellipse>().LastOrDefault();  // 在Canvas的子物件中找到最後一個Ellipse物件
                    ellipse.Width = width;  // 設定該Ellipse物件的寬度
                    ellipse.Height = height;  // 設定該Ellipse物件的高度
                    ellipse.SetValue(Canvas.LeftProperty, origin.X);  // 設定該Ellipse物件的起點x座標
                    ellipse.SetValue(Canvas.TopProperty, origin.Y);  // 設定該Ellipse物件的起點y座標
                    break;
            }
        }
    }
    


        // 定義一個私有方法，這個方法會在Canvas上的鼠標左鍵按下事件觸發時被調用。
        private void myCanvas_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
{
    start = e.GetPosition(myCanvas);  // 取得滑鼠在Canvas上的起始點座標，並儲存到start變數
    myCanvas.Cursor = Cursors.Cross;  // 將滑鼠指標變更為十字形

    switch (shapeType)  // 根據當前選擇的圖形類型（shapeType變數）來決定要繪製哪一種圖形
    {
        case "Line":  // 如果選擇的是直線
            var line = new Line  // 創建一個新的Line物件
            {
                Stroke = Brushes.Gray,  // 設定線條顏色為灰色
                StrokeThickness = 1,  // 設定線條粗細為1
                X1 = start.X,  // 設定起始點的X座標
                Y1 = start.Y,  // 設定起始點的Y座標
                X2 = dest.X,  // 設定終點的X座標（此時還未繪圖，這個座標會在後續更新）
                Y2 = dest.Y   // 設定終點的Y座標（此時還未繪圖，這個座標會在後續更新）
            };
            myCanvas.Children.Add(line);  // 將新創建的Line物件加入到Canvas的子物件集合中
            break;
        case "Rectangle":  // 如果選擇的是矩形
            var rect = new Rectangle  // 創建一個新的Rectangle物件
            {
                Stroke = Brushes.Gray,  // 設定線條顏色為灰色
                StrokeThickness = 1,  // 設定線條粗細為1
                Fill = Brushes.LightGray,  // 設定填充顏色為淺灰色
            };
            myCanvas.Children.Add(rect);  // 將新創建的Rectangle物件加入到Canvas的子物件集合中
            rect.SetValue(Canvas.LeftProperty, start.X);  // 設定矩形的左上角X座標
            rect.SetValue(Canvas.TopProperty, start.Y);  // 設定矩形的左上角Y座標
            break;
        case "Ellipse":  // 如果選擇的是橢圓
            var ellipse = new Ellipse  // 創建一個新的Ellipse物件
            {
                Stroke = Brushes.Gray,  // 設定線條顏色為灰色
                StrokeThickness = 1,  // 設定線條粗細為1
                Fill = Brushes.LightGray,  // 設定填充顏色為淺灰色
            };
            myCanvas.Children.Add(ellipse);  // 將新創建的Ellipse物件加入到Canvas的子物件集合中
            ellipse.SetValue(Canvas.LeftProperty, start.X);  // 設定橢圓的左上角X座標
            ellipse.SetValue(Canvas.TopProperty, start.Y);  // 設定橢圓的左上角Y座標
            break;
    }
    DisplayStatus();  // 更新顯示狀態（具體實作未給出，可能用於更新界面上的相關狀態或座標資訊）
}

        // 定義一個私有方法名為DisplayStatus
        // 這個方法用於顯示或更新某種狀態（例如UI元件的狀態，或者是應用程式的內部狀態）
        private void DisplayStatus()
{
    // 使用LINQ查詢從Canvas的Children集合中計數Line型別的物件數量
    int lineCount = myCanvas.Children.OfType<Line>().Count();  // 統計Canvas中Line物件的數量
    // 使用LINQ查詢從Canvas的Children集合中計數Rectangle型別的物件數量
    int rectCount = myCanvas.Children.OfType<Rectangle>().Count();  // 統計Canvas中Rectangle物件的數量
    // 使用LINQ查詢從Canvas的Children集合中計數Ellipse型別的物件數量
    int ellipseCount = myCanvas.Children.OfType<Ellipse>().Count();  // 統計Canvas中Ellipse物件的數量
    
    // 更新座標標籤的內容，顯示起始和終點座標，座標值經過四捨五入
    coordinateLabel.Content = $"座標點: ({Math.Round(start.X)}, {Math.Round(start.Y)}) : ({Math.Round(dest.X)}, {Math.Round(dest.Y)})";  // 更新座標信息到界面
                                                                                                                                      // 更新形狀數量標籤的內容
            shapeLabel.Content = $"Line: {lineCount}, Rectangle: {rectCount}, Ellipse: {ellipseCount}";  // 更新形狀數量信息到界面
        }

        // 這是一個私有方法，會在顏色選擇器控件中選定的顏色發生變化時觸發。
        // 這個方法是特定於某個提供'SelectedColorChanged'事件的UI框架中的顏色選擇器控件。
        private void strokeColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
{
    // 從顏色選擇器中獲取選擇的顏色，並儲存到strokeColor變量中
    strokeColor = (Color)strokeColorPicker.SelectedColor;  // 更新線條顏色
}

        // 這是一個私有方法，將在顏色選擇器控件的選定顏色更改時被觸發。
        // 這個方法通常與某個UI框架的顏色選擇器控件的'SelectedColorChanged'事件相關聯。
        private void fillColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            // 從顏色選擇器中獲取選擇的顏色，並儲存到fillColor變量中
            fillColor = (Color)fillColorPicker.SelectedColor;// 更新填充顏色
        }

        // 這是一個私有方法，將在點擊名為"clearMenuItem"的選單項目時被觸發。
        // 此方法用於處理選單項目的點擊事件。
        private void clearMenuItem_Click(object sender, RoutedEventArgs e)
{
    // 清空Canvas的Children集合，即移除畫布上的所有物件
    myCanvas.Children.Clear();  // 清除所有形狀
    // 調用DisplayStatus()更新界面上的統計數據
    DisplayStatus();  // 更新界面信息
}

        // 定義一個私有方法，這個方法會在Canvas上的鼠標左鍵釋放事件觸發時被調用。
        // 這通常用於結束一個拖拽操作、完成繪圖或其他與鼠標互動相關的活動。
        private void myCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
{
    // 根據先前選擇的形狀類型（shapeType），進行相對應的操作
    switch (shapeType)
    {
        // 如果選擇的是線條（Line）
        case "Line":
            // 從畫布中找出最後添加的Line對象
            var line = myCanvas.Children.OfType<Line>().LastOrDefault();
            // 設定線條的顏色
            line.Stroke = new SolidColorBrush(strokeColor);
            // 設定線條的寬度
            line.StrokeThickness = strokeThickness;
            break;

        // 如果選擇的是矩形（Rectangle）
        case "Rectangle":
            // 從畫布中找出最後添加的Rectangle對象
            var rect = myCanvas.Children.OfType<Rectangle>().LastOrDefault();
            // 設定矩形邊框的顏色
            rect.Stroke = new SolidColorBrush(strokeColor);
            // 設定矩形的填充顏色
            rect.Fill = new SolidColorBrush(fillColor);
            // 設定矩形邊框的寬度
            rect.StrokeThickness = strokeThickness;
            break;

        // 如果選擇的是橢圓（Ellipse）
        case "Ellipse":
            // 從畫布中找出最後添加的Ellipse對象
            var ellipse = myCanvas.Children.OfType<Ellipse>().LastOrDefault();
            // 設定橢圓邊框的顏色
            ellipse.Stroke = new SolidColorBrush(strokeColor);
            // 設定橢圓的填充顏色
            ellipse.Fill = new SolidColorBrush(fillColor);
            // 設定橢圓邊框的寬度
            ellipse.StrokeThickness = strokeThickness;
            break;
    }
    // 將畫布的游標設定回箭頭形狀（預設形狀）
    myCanvas.Cursor = Cursors.Arrow;
}

    }
}
