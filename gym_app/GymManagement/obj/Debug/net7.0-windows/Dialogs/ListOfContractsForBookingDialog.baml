   M S B A M L   `   `   `  ����H  DGymManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:GymManagement.DialogsGymManagement.Dialogs  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      C&clr-namespace:GymManagement.ConvertersGymManagement.Converters  5      _ [MaterialDesignThemes.Wpf, Version=1.0.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aeHclr-namespace:MaterialDesignThemes.Wpf;assembly=MaterialDesignThemes.WpfMaterialDesignThemes.Wpf 5
      ;    5GymManagement.Dialogs.ListOfContractsForBookingDialog   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:GymManagement.Dialogs     5      9system&clr-namespace:System;assembly=mscorlib     5      =
converters&clr-namespace:GymManagement.Converters     5      FmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	      RwpfHclr-namespace:MaterialDesignThemes.Wpf;assembly=MaterialDesignThemes.Wpf  5
      -   5          >�Name$  	ucBooking��5         
Page_Style#��[  5       � 	Resources 5         BooleanToVisibilityConverterB  <GymManagement.Converters.InverseBooleanToVisibilityConverter ' #InverseBooleanToVisibilityConverter��%:   &	       &	      �� 5   
   6N     5   
   69   5      .��5      �  � RowDefinitions 5   
   �� 5      6   �� 5      $
��0.2* �6   6,   .+�5   
   �� $��0��6r     MaterialDesignBackground#���  6   y�6   �� *	IsLoading+ �� 	Converter# [ ._�5      �� $��Danh sách hợp đồng��6     MaterialDesignGroupBox#���  63   $��16q�5       � UseLayoutRounding$
 True��6#    J� SnapsToDevicePixels$
 True��6<   .��5      &   System.Windows.Controls.DataGrid  
�������$��20q�6.     DataGridStyle#��[ 5          
IsReadOnly$
 True��5!   B    �� SelectedItem 5      �� *SelectedPtContract+ �� Mode$ TwoWay=�	 �� UpdateSourceTrigger$	 PropertyChanged=���5!      �� *PtContractList+
   Columns
 5"      4  .System.Windows.Controls.DataGridTemplateColumn  5$      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��61      
IsReadOnly$
 True��6=      CellTemplate 5%      �� 5&   "   .R�5'   &   �� $��Center=�60   ��6L   ��  �� Path$ 
ContractId�$	 PropertyChanged=�5(   '   5)   #   5+        5/      $ PtCourse��61    50      �� 51   "   .R�52   &   �� $��Center=�60   ��6L   �� $ Course.Name�$	 PropertyChanged=�53   '   54   #   56        58      $ Khách hàng��61    59      �� 5:   "   .R�5;   &   �� $��Center=�60   ��6L   �� $ Customer.Name�$	 PropertyChanged=�5<   '   5=   #   5?        5C      $ 
CreateDate��61    5D      �� 5E   "   .R�5F   &   �� $��Center=�61   ��5G   -   �� $ 
CreateDate� �� StringFormat$ D�� �� ConverterCulture$ vi��$	 PropertyChanged=�6�   5H   #   5J        5M      $ 
FinishDate��61    5N      �� 5O   "   .R�5P   &   �� $��Center=�61   ��5Q   -   �� $ 
FinishDate�$ D��$ vi��$	 PropertyChanged=�6�   5R   #   5T      5Y      5Z      5[      �� 5]   
   $��1��6   $��0,16,0,0q�5^       �� Orientation$ 
Horizontal=�5_      .+�5`      �� )  #MaterialDesignThemes.Wpf.DialogHost   CloseDialogCommand#��Z 6    �� 	IsDefault$
 True��5a        MaterialDesignFlatButton#��[ 5b      ��5c      ��5d      True��6+   .��5e   +   ĐẶT LỊCH5g      �� 5h      $��8,0,0,0q�6   #��Z 5i       �� IsCancel$
 True��5j      #��[ 5k      ��5l      ��5m      	False��6,   .��5n   +   
HỦY BỎ5p      5q      5r      5s      