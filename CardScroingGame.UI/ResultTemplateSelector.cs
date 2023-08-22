using CardScoringGame.Model;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace CardScroingGame.UI
{
    public class ResultTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NoneTemplate { get; set; }
        public DataTemplate NoMatchTemplate { get; set; }
        public DataTemplate ScoredTemplate { get; set; }
        public DataTemplate EndResultTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {            
            if (item is MatchResult matchResult)
            {
                return  matchResult.Type == ResultType.NoMatch ? NoMatchTemplate : matchResult.Type == ResultType.Scored ? ScoredTemplate : NoneTemplate;
            }

            if(item is EndResult) 
            { 
                return EndResultTemplate; 
            }

            return base.SelectTemplateCore(item, container);
        }
    }
}
