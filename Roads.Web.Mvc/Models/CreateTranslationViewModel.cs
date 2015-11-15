using System.Collections.Generic;

namespace Roads.Web.Mvc.Models
{
    public class CreateTranslationViewModel
    {
        public CreateTranslationViewModel()
        {
            Translations = new List<TranslationViewModel>();
        }

        public List<TranslationViewModel> Translations { get; set; }
    }
}