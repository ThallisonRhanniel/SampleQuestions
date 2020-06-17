using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Text.RegularExpressions;
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

        public string DynamicExpression(object[] parameters, string exp)
        {
            var e = DynamicExpressionParser.ParseLambda(null, exp, parameters);

            var result = e.Compile().DynamicInvoke();

            return result.ToString();
        }

        private static List<string> EverythingBetween(string source, string start, string end)
        {
            var results = new List<string>();

            string pattern = string.Format(
                "{0}({1}){2}",
                Regex.Escape(start),
                ".+?",
                Regex.Escape(end));

            foreach (Match m in Regex.Matches(source, pattern))
            {
                results.Add(m.Groups[1].Value);
            }



            return results;
        }




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

                        case (int)ResponseTypes.Decimal:
                            xaml += CreateDecimalTextFields(questoes);
                            break;

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

        #region TextField

        private string CreateDecimalTextFields(Questao questao)
        {
            string xaml = null;

            xaml += $"{DynamicPage.StartCardView}" +
                    $"{DynamicPage.StartLabelTitleQuestion}{questao.Descricao}{DynamicPage.EndLabelTitleQuestion}" +
                    $"{DynamicPage.StartDecimalTextField}{questao.FormularioAreaId}_{questao.Identificador}{DynamicPage.EndDecimalTextField}" +
                    $"{DynamicPage.EndCardView}";

            return xaml;
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

                StackLayout stackLayoutRoot = page.FindByName<StackLayout>("StackLayoutRoot");

                foreach (var forms in fakeQuestion.AreasFormulario)
                {
                    foreach (var question in forms.Questoes)
                    {

                        switch (question.TipoResposta)
                        {
                            case (int)ResponseTypes.CaixaSelecao:
                                foreach (var answer in question.ListaRespostas)
                                {
                                    var raddioButton = stackLayoutRoot.FindByName<RadioButton>($"{question.FormularioAreaId}_{question.Identificador}_{answer}");
                                    raddioButton.Text = answer;
                                }
                                break;
                            case (int)ResponseTypes.Decimal:
                                if (!string.IsNullOrEmpty(question.ExpressaoCalculoMobile))
                                {
                                    var allWorlds = EverythingBetween(question.ExpressaoCalculoMobile, "[", "]").Distinct().ToList();
                                    var allAnswers = new List<KeyValuePair<string, object>>();

                                    var entryCalculable = stackLayoutRoot.FindByName<Entry>($"{question.FormularioAreaId}_{question.Identificador}");
                                    entryCalculable.IsReadOnly = true;

                                    for (int i = 0; i < allWorlds.Count; i++)
                                    {
                                        var entryThatInsertsValue = stackLayoutRoot.FindByName<Entry>($"{question.FormularioAreaId}_{allWorlds[i]}");
                                        entryThatInsertsValue.StyleId = $"{allWorlds[i]}";
                                        // Este valor  é adicionado na lista previamente para que cada campo tenha seu indice pre-definido e eu possa resgata-lo futuramente usando o index of.
                                        allAnswers.Add(new KeyValuePair<string, object>($"{allWorlds[i]}", null));


                                        entryThatInsertsValue.TextChanged += (sender, args) =>
                                        {
                                            try
                                            {
                                                var index = allAnswers.FindIndex(pair => pair.Key == entryThatInsertsValue.StyleId);
                                                allAnswers.RemoveAt(index);
                                                allAnswers.Insert(index, new KeyValuePair<string, object>($"{entryThatInsertsValue.StyleId}", args.NewTextValue));
                                                if (allAnswers.All(pair => pair.Value != null))
                                                {
                                                    if (allAnswers.All(pair => pair.Value.ToString() != ""))
                                                    {
                                                        var answers = new object[allWorlds.Count];
                                                        for (int j = 0; j < allWorlds.Count; j++)
                                                        {
                                                            //Index referente a cada campo nescessário para o calculo do método.
                                                            var newindex = allAnswers.FindIndex(pair => pair.Key == allWorlds[j]);
                                                            answers.SetValue(decimal.Parse(allAnswers[newindex].Value.ToString()), j);
                                                        }

                                                        var calculationExpression = question.ExpressaoCalculoMobile.Replace("[", "").Replace("]", "");
                                                        //Substituição das palavras
                                                        for (int j = 0; j < allWorlds.Count; j++)
                                                        {
                                                            //Procuro a palavra dentro da string e adiciono a pocição dela com @, o @ é necessário pro método entender a posição do item
                                                            calculationExpression = calculationExpression.Replace($"{allWorlds[j]}", $"@{j}");
                                                        }

                                                        entryCalculable.Text = DynamicExpression(answers, calculationExpression);

                                                    }
                                                    else
                                                        entryCalculable.Text = string.Empty;
                                                    
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine(e.Message);
                                            }
                                        };
                                    }
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
