using System;
using System.Collections.Generic;
using System.Text;

namespace SampleQuestions.Helpers
{
    public static class DynamicPage
    {
        public const string Header = 
            @"<?xml version='1.0' encoding='utf-8' ?>
             <ContentPage xmlns='http://xamarin.com/schemas/2014/forms'
             xmlns:flex='clr-namespace:Flex.Controls;assembly=Flex'
             xmlns:ui='clr-namespace:XF.Material.Forms.UI;assembly=XF.Material'
             xmlns:input='clr-namespace:Plugin.InputKit.Shared.Controls;assembly=Plugin.InputKit'
             xmlns:x='http://schemas.microsoft.com/winfx/2009/xaml'
             xmlns:d='http://xamarin.com/schemas/2014/forms/design'
             Title='Avaliações'
             Visual='Material'
             xmlns:mc='http://schemas.openxmlformats.org/markup-compatibility/2006'
             mc:Ignorable='d'  >
            <ScrollView>
                <StackLayout x:Name='StackLayoutRoot' HorizontalOptions='FillAndExpand' VerticalOptions='FillAndExpand' >";

        public const string Footer = @"</StackLayout>
                                        </ScrollView>
                                            </ContentPage>";
    }
}
