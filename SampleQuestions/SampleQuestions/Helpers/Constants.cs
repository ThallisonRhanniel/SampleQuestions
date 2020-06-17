using System;
using System.Collections.Generic;
using System.Text;

namespace SampleQuestions.Helpers
{
    public static class FakeData
    {
        public const string Questions = @"{
        'formularioId': '1e57c32e-8819-4c71-9269-abc2018879de',
        'tipoFormularioId': '212a3e92-3b89-49f0-8943-ab9100b9b226',
        'descricao': 'Pesquisa de Satisfação',
        'dataCadastro': '2020-05-21T23:48:57.6810462',
        'areasFormulario': [
            {
                'formularioId': '1e57c32e-8819-4c71-9269-abc2018879de',
                'formularioAreaId': '25173f8f-8b5d-415b-91dd-abc2018879df',
                'descricao': 'Feedback',
                'questoes': [
                    {
                        'formularioAreaId': '25173f8f-8b5d-415b-91dd-abc2018879df',
                        'tipoResposta': 7,
                        'item': 1,
                        'descricao': 'Qual sua avaliação em relação a Steven?',
                        'identificador': 'qual-sua-avaliacao-em-relacao-a-pontualidade',
                        'expressaoCalculo': '',
                        'expressaoExibicao': '',
                        'expressaoCalculoMobile': null,
                        'listaRespostas': [
                            'Ruim',
                            'Bom',
                            'Ótimo',
                            'Perfeito'
                        ]
                    }
                ]
            },
            {
                'formularioId': 'fcb44c77-322d-48d9-8659-aba700ece239',
                'formularioAreaId': '25173f8f-8b5d-415b-91dd-abc2018879df',
                'descricao': 'Outro campo de pergunta',
                'questoes': [
                    {
                        'formularioAreaId': '25173f8f-8b5d-415b-91dd-abc2018879df',
                        'tipoResposta': 4,
                        'item': 1,
                        'descricao': 'IMC',
                        'identificador': 'imc',
                        'expressaoCalculo': '',
                        'expressaoExibicao': '',
                        'expressaoCalculoMobile': '[peso] / ( [altura] * [altura] )',
                        'listaRespostas': []
                    },
                    {
                        'formularioAreaId': '25173f8f-8b5d-415b-91dd-abc2018879df',
                        'tipoResposta': 4,
                        'item': 1,
                        'descricao': 'Peso',
                        'identificador': 'peso',
                        'expressaoCalculo': '',
                        'expressaoExibicao': '',
                        'expressaoCalculoMobile': '',
                        'listaRespostas': []
                    },
                    {
                        'formularioAreaId': '25173f8f-8b5d-415b-91dd-abc2018879df',
                        'tipoResposta': 4,
                        'item': 1,
                        'descricao': 'Altura',
                        'identificador': 'altura',
                        'expressaoCalculo': '',
                        'expressaoExibicao': '',
                        'expressaoCalculoMobile': '',
                        'listaRespostas': []
                    }
                ]
            }
        ]
    }";
    }

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


        public static string StartCardView = 
            @"<ui:MaterialCard 
            Margin='0,20,0,0'
            CornerRadius='2'  
            BorderColor='LightGray'
            VerticalOptions='Fill'
            HorizontalOptions='FillAndExpand' >
            <StackLayout> ";

        public static string EndCardView = 
            @"</StackLayout>                  
            </ui:MaterialCard>";

        public static string StartLabelTitleQuestion = 
            @"<Label TextColor='Black' 
            FontSize='Medium'
            FontFamily='muli.ttf#muli'
            Text='";

        public static string EndLabelTitleQuestion = "'/>";

        public static string StartGroupRadioButtonP1 = @"<input:RadioButtonGroupView x:Name='";

        public static string StartGroupRadioButtonP2 = @"'>";

        public static string EndGroupRadioButton = "</input:RadioButtonGroupView>";


        public static string StartRadioButton = 
            @"<input:RadioButtonGroupView >
            <input:RadioButton Margin='0,8' x:Name='";

        public static string EndRadioButton = 
            @"' FontFamily='muli.ttf#muli' TextFontSize='17' />
            </input:RadioButtonGroupView>";

        public static string StartLabelTitleForms = 
            @"<Label TextColor='White'
            FontSize='Medium'
            FontFamily='rimouski.ttf#rimouski'
            BackgroundColor='#4db6ac'
            Padding='20'
            Text='";

        public static string EndLabelTitleForms = "'/>";


        public static string StartDecimalTextField = 
            @"<Entry TextColor='DimGray' 
            BackgroundColor='Transparent'
            Keyboard='Numeric'
            HorizontalOptions='FillAndExpand' 
            x:Name='";

        public static string EndDecimalTextField = @"' />";





        public const string Footer = @"</StackLayout>
                                        </ScrollView>
                                            </ContentPage>";
    }
}
