﻿
namespace Exchange.Helpers
{
    using Xamarin.Forms;
    using Interfaces;
    using Resources;
    public static class Lenguages
    {
        static Lenguages()
        {
            //DependencyService nos sirve para tener los comportamientos en las plataformas
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            //Obtiene la configuracion del telefono y la establece como la configuracion 
            //de idioma, con el (ci) Culture Infor

            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Accept
        {
            get { return Resource.Accept; }
        }

        public static string AmountNumericValidation
        {
            get { return Resource.AmountNumericValidation; }
        }

        public static string AmountPlaceHolder
        {
            get { return Resource.AmountPlaceHolder; }
        }

        public static string AmountValidation
        {
            get { return Resource.AmountValidation; }
        }

        public static string Convert
        {
            get { return Resource.Convert; }
        }

        public static string Error
        {
            get { return Resource.Error; }
        }

        public static string Error_conecction
        {
            get { return Resource.Error_conecction; }
        }

        public static string Error_settings
        {
            get { return Resource.Error_settings; }
        }

        public static string Erro_internet_db
        {
            get { return Resource.Erro_internet_db; }
        }

        public static string Loading
        {
            get { return Resource.Loading; }
        }

        public static string Rate_loaded_internet
        {
            get { return Resource.Rate_loaded_internet; }
        }

        public static string Rate_loaded_localdata
        {
            get { return Resource.Rate_loaded_localdata; }
        }

        public static string Ready
        {
            get { return Resource.Ready; }
        }

        public static string SourceRateLabel
        {
            get { return Resource.SourceRateLabel; }
        }

        public static string SourceRateTitle
        {
            get { return Resource.SourceRateTitle; }
        }

        public static string SourceRateValidation
        {
            get { return Resource.SourceRateValidation; }
        }

        public static string TargetRateLabel
        {
            get { return Resource.TargetRateLabel; }
        }

        public static string TargetRateTitle
        {
            get { return Resource.TargetRateTitle; }
        }

        public static string TargetRateValidation
        {
            get { return Resource.TargetRateValidation; }
        }

        public static string Title
        {
            get { return Resource.Title; }
        }
    }
}
