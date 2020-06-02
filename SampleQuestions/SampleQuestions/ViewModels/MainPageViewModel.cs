using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Plugin.InputKit.Shared.Controls;
using SampleQuestions.Helpers;
using SampleQuestions.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleQuestions.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {

        #region Properties

        public enum ResponseTypes
        {
            Cpf = 1,
            Cnpj = 2,
            Numerico = 3,
            Decimal = 4,
            RespostaCurta = 5,
            MultiplaEscolha = 6,
            CaixaSelecao = 7,
            ListaSuspensa = 8,
            Paragrafo = 9,
            Data = 10,
            Hora = 11,
            Upload = 12
        }


        #endregion


        #region Commands
        public DelegateCommand OpenFormCommand { get; set; }
        #endregion


        #region Methods

        private string CreateWidgets(FormsModel formsModel)
        {
            string xaml = null;
            foreach (var areasFormulario in formsModel.AreasFormulario)
            {
                //Titulo do formulário (Fundo colorido)
                xaml += $"{DynamicPage.StartLabelTitleForms}{areasFormulario.Descricao}{DynamicPage.EndLabelTitleForms}";
                foreach (var questoes in areasFormulario.Questoes)
                {
                    switch (questoes.TipoResposta)
                    {
                        case (int)ResponseTypes.CaixaSelecao:
                            xaml += CreateRadioButtons(questoes);
                            break;
                       
                    }
                }
            }
            return xaml;
        }


        #region Widgets


        #region RadioButton

        private string CreateRadioButtons(Questao questao)
        {

            string xaml = null;

            //checkBoxGroup = checkBoxGroup + CreateCheckBoxe(questoes);
            xaml = xaml +
                   $"{DynamicPage.StartCardView}" +
                   $"{DynamicPage.StartLabelTitleQuestion}{questao.Descricao}{DynamicPage.EndLabelTitleQuestion}" +
                   $"{DynamicPage.StartGroupRadioButtonP1}{questao.FormularioAreaId}_{questao.Identificador}{DynamicPage.StartGroupRadioButtonP2}" +
                   $"{CreateRadioButton(questao)}" +
                   $"{DynamicPage.EndGroupRadioButton}" +
                   $"{DynamicPage.EndCardView}";

            return xaml;
        }



        private string CreateRadioButton(Questao questao)
        {
            string resposta = null;

            foreach (var question in questao.ListaRespostas)
            {
                //Item == QuestãoID
                resposta = resposta +
                           $"{DynamicPage.StartRadioButton}" +
                           $"{questao.FormularioAreaId}_{questao.Identificador}_{question}" +
                           $"{DynamicPage.EndRadioButton} \n";
            }
            return resposta;
        }


        #endregion


        #endregion

        #endregion

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";

            OpenFormCommand = new DelegateCommand(async () =>
            {

                var fakeQuestion = JsonConvert.DeserializeObject<FormsModel>(FakeData.Questions);


                string xaml = $"{DynamicPage.Header}" +
                              $"{CreateWidgets(fakeQuestion)}" +
                              $"{DynamicPage.Footer}";

                ContentPage page = new ContentPage().LoadFromXaml(xaml);

                StackLayout StackLayoutRoot = page.FindByName<StackLayout>("StackLayoutRoot");

                foreach (var areasFormulario in fakeQuestion.AreasFormulario)
                {
                    foreach (var questoes in areasFormulario.Questoes)
                    {

                        switch (questoes.TipoResposta)
                        {
                            case (int)ResponseTypes.CaixaSelecao:
                                foreach (var questoesListaResposta in questoes.ListaRespostas)
                                {
                                    var raddioButton = StackLayoutRoot.FindByName<RadioButton>($"{questoes.FormularioAreaId}_{questoes.Identificador}_{questoesListaResposta}");
                                    raddioButton.Text = questoesListaResposta;
                                }
                                break;
                        }
                    }
                }

                await Prism.PrismApplicationBase.Current.MainPage.Navigation.PushAsync(page);
            });
        }
    }
}
