   M S B A M L   `   `   `  ����H  DGymManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:GymManagement.ResourceXAMLGymManagement.ResourceXAML  5   	   -clr-namespace:GymManagementGymManagement  5   	   C&clr-namespace:GymManagement.ViewModelsGymManagement.ViewModels  5	   	   9!clr-namespace:GymManagement.ViewsGymManagement.Views  5   	   C&clr-namespace:GymManagement.ConvertersGymManagement.Converters  5   	       GymManagement.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=1.0.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   3Menu(clr-namespace:GymManagement.ResourceXAML  5   	   'localclr-namespace:GymManagement  5   	   0vms&clr-namespace:GymManagement.ViewModels  5	   	   -views!clr-namespace:GymManagement.Views  5   	   7
converters&clr-namespace:GymManagement.Converters  5   	     �Language$  vi�5   )   -   5         �Name$ 
mainscreen��5   	      MaterialDesignBody#���   5   	   $��Regular"�5   	   $��13(�5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	    � WindowStyle$
 None=�5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	   
������  � AllowsTransparency$
 True��5   "     MaterialDesignFont#���  5   	    � Title$ 
MainWindow��5   	   $	��768��6   $
��1440��6)   	 � 	Resources	 5         BooleanToVisibilityConverterB  <GymManagement.Converters.InverseBooleanToVisibilityConverter ' #InverseBooleanToVisibilityConverter��%:   &	       &	      �� 5   
   6N     5   
   69   5      .��5      )  #MaterialDesignThemes.Wpf.DialogHost  
   DialogTheme$
 Inherit=�6       
Identifier$ 
RootDialog��5       .��5    
   ��   BorderWindow#���  6   ���#fafafa._�5!      � .+�5%      '   !GymManagement.Views.WindowTaskBar  $��50��6&   $	��Top=�62   6L   � 5&        PrimaryHueDarkBrush#��[ 6z    � IsIndeterminate$
 True��5'      y�5&      �� *	IsLoading+ �� 	Converter# [ 5'      � 5*      y�6   �� *	IsLoading+# [  � ColumnDefinitions 5-      �� 5.      $	��240 �6+   69   �� 5/      6.   .+�54      �� $��8q�6    " PrimaryHueLightForegroundBrush#��[ 6)   $��1q�6g    �� CornerRadius$ 30��55      $��0��6+   
�������._�56      �� .+�57      �� $��/Images/logo_app.png��6$   $��150px��6B   $��	0,50,0,20q�6P   6e   $   GymManagement.ResourceXAML.Btn  58        BtnStyle#��[ 6'    d� 	IsChecked$
 trueY�6�   ��6I   �� *HomeNavigationCommand+��6s   ��  �� ElementName$ 
mainscreen��.��59   "   � .+�5:   &   '  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 Home=�6>     IconMenu#��[ 6J   �� 5;   &    	 
Text_Style#��[	 5<      Trang chủ5>   '   5?   #   5@        5B      #��[ 6'   ��6I   �� *CustomerNavigationCommand+��6w   �� $ 
mainscreen��.��5C   "   � .+�5D   &     $ Account=�6>   #��[ 6M   �� 5E   &   #��[	 5F      Khách hàng5H   '   5I   #   5J        5L      #��[ 6'   ��6I   �� *StaffNavigationCommand+��6t   �� $ 
mainscreen��.��5M   "   � .+�5N   &     $ CardAccountDetailsOutline=�6>   #��[ 6_   �� 5O   &   #��[	 5P      Nhân viên5R   '   5S   #   5T        5U      #��[ 6'   ��6I   �� *CourseNavigationCommand+��6u   �� $ 
mainscreen��.��5V   "   � .+�5W   &     $ 
Gymnastics=�6>   #��[ 6P   �� 5X   &   #��[	 5Y      
Gói tập5[   '   5\   #   5]        5_      #��[ 6'   ��6I   �� *FacilityNavigationCommand+��6w   �� $ 
mainscreen��.��5`   "   � .+�5a   &     $ Dumbbell=�6>   #��[ 6N   �� 5b   &   #��[	 5c      	Vật tư5e   '   5f   #   5g        5h      #��[ 6'   ��6I   �� *ContractNavigationCommand+��6w   �� $ 
mainscreen��.��5i   "   � .+�5j   &     $ FileDocumentEdit=�6>   #��[ 6V   �� 5k   &   #��[	 5l      Hợp đồng5n   '   5o   #   5p        5s      #��[ 6'   ��6I   �� *BookingNavigationCommand+��6v   �� $ 
mainscreen��.��5t   "   � .+�5u   &     $	 Pen=�6>   #��[ 6I   �� 5v   &   #��[	 5w      Đặt lịch5y   '   5z   #   5{      � 5�      $��0 20 0 0q�6$   .+�5�   "   �� #��[	 5�      $|�False��6A   Tùy chọn5�   #   '  !MaterialDesignThemes.Wpf.PopupBox  5�   "     
 PrimaryHueMidForegroundBrush#��[
 5�   !    - )MaterialDesignMultiFloatingActionPopupBox#���  6\      PlacementMode$ BottomAndAlignCentres=�6�    ^� 	Placement$ Right=�6�    � ToolTip$? 9PopupBox, Style MaterialDesignMultiFloatingActionPopupBox��5�   !   $��30��6e   $	��150��6q   $��30,0,0,0q�6}      PopupContent. 5�   &   �� .+�5�   *   ]� .+�5�   .   �� $ Cài đặt ứng dụng��65   #��[
 6Q   ��6�   �� *NotSupportedCommand+.��6�   Cài đặt5�   /   5�   +   ]� 5�   *   .+�5�   .   �� $' !Liên hệ với chủ sở hữu��65   #��[
 6W   ��6�   �� *NotSupportedCommand+.��6�   
Hỗ trợ5�   /   5�   +   ]� 5�   *   .+�5�   .   �� $( "Đăng xuất khỏi ứng dụng ��66   #��[
 6[   ��6�   �� *BacktoLoginCommand+��6�   �� $ 
mainscreen��.��6�   Đăng xuất5�   /   5�   +   5�   '   5�   #   5�      5�      5�      �� 5�      $��1��6   $ 	0 15 15 0��6-   ._�5�      �� -   5         ��Name$ Pages��5�   )   $��5q�68   ��5�      �� *CurrentViewModel+5�      5�      5�      5�      5�      5�      5�      